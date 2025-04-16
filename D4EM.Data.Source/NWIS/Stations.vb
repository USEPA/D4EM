Imports NetTopologySuite.Geometries
Imports atcUtility
Imports MapWinUtility
Imports D4EM.Geo

Partial Public Class NWIS

    Public Const pDefaultStationsBaseFilename As String = "NWIS_Stations"

    Private Shared SiteColumns As String =
              "&column_name=agency_cd" _
            + "&column_name=site_no" _
            + "&column_name=station_nm" _
            + "&column_name=site_tp_cd" _
            + "&column_name=dec_lat_va" _
            + "&column_name=dec_long_va" _
            + "&column_name=dec_coord_datum_cd" _
            + "&column_name=district_cd" _
            + "&column_name=state_cd" _
            + "&column_name=county_cd" _
            + "&column_name=country_cd" _
            + "&column_name=land_net_ds" _
            + "&column_name=map_nm" _
            + "&column_name=map_scale_fc" _
            + "&column_name=alt_va" _
            + "&column_name=alt_meth_cd" _
            + "&column_name=alt_acy_va" _
            + "&column_name=alt_datum_cd" _
            + "&column_name=huc_cd" _
            + "&column_name=basin_cd" _
            + "&column_name=topo_cd" _
            + "&column_name=data_types_cd" _
            + "&column_name=instruments_cd" _
            + "&column_name=construction_dt" _
            + "&column_name=inventory_dt" _
            + "&column_name=drain_area_va" _
            + "&column_name=contrib_drain_area_va" _
            + "&column_name=tz_cd" _
            + "&column_name=local_time_fg" _
            + "&column_name=reliability_cd" _
            + "&column_name=gw_file_cd" _
            + "&column_name=nat_aqfr_cd" _
            + "&column_name=aqfr_cd" _
            + "&column_name=aqfr_type_cd" _
            + "&column_name=well_depth_va" _
            + "&column_name=hole_depth_va" _
            + "&column_name=depth_src_cd" _
            + "&column_name=project_no" _
            + "&column_name=rt_bol" _
            + "&column_name=peak_begin_date" _
            + "&column_name=peak_end_date" _
            + "&column_name=peak_count_nu" _
            + "&column_name=qw_begin_date" _
            + "&column_name=qw_end_date" _
            + "&column_name=qw_count_nu" _
            + "&column_name=gw_begin_date" _
            + "&column_name=gw_end_date" _
            + "&column_name=gw_count_nu" _
            + "&column_name=sv_begin_date" _
            + "&column_name=sv_end_date" _
            + "&column_name=sv_count_nu"

    Friend Shared Function GetAndMakeShape(ByVal aProject As Project,
                                           ByVal aStationDataType As LayerSpecification,
                                  Optional ByVal aSaveAsBaseFilename As String = Nothing,
                                  Optional ByVal aMakeShape As Boolean = True) As String
        Dim lReturnValue As String = ""
        Dim lSaveAs As String = aSaveAsBaseFilename & "_" & aStationDataType.Tag & ".rdb"
        Dim lHaveStations As Boolean = IO.File.Exists(lSaveAs)
        If Not lHaveStations Then
TryDownload:
            If aProject.Region Is Nothing Then
                Throw New ApplicationException("NWIS: Region not specified")
            End If
            lHaveStations = GetStationsInRegion(aProject.Region, lSaveAs, aStationDataType)
        End If
        If lHaveStations AndAlso aMakeShape Then
            Dim lStationsRDB As New atcTableRDB
            Dim lShapeFilename As String = IO.Path.ChangeExtension(lSaveAs, "shp")
            If lStationsRDB.OpenFile(lSaveAs) Then
                lReturnValue = MakeStationShapefile(aProject, aStationDataType, lStationsRDB, lShapeFilename)
            Else
                If Logger.Msg("Could not open downloaded stations to make point layer" & vbCrLf & lSaveAs & "Try downloading again?", Microsoft.VisualBasic.MsgBoxStyle.YesNo, "Trouble creating station layer") = MsgBoxResult.Yes Then
                    TryDelete(lSaveAs)
                    GoTo TryDownload
                End If
                TryDelete(lSaveAs)
            End If
        End If

        Return lReturnValue
    End Function

    'http://nwis.waterdata.usgs.gov/nwis/measurements?nw_longitude_va=-85&nw_latitude_va=34&se_longitude_va=-84&se_latitude_va=33&coordinate_format=decimal_degrees&sort_key=site_no&group_key=NONE&format=sitefile_output&sitefile_output_format=rdb&column_name=agency_cd&column_name=site_no&column_name=station_nm&column_name=dec_lat_va&column_name=dec_long_va&column_name=district_cd&column_name=state_cd&column_name=county_cd&column_name=country_cd&column_name=land_net_ds&column_name=map_nm&column_name=map_scale_fc&column_name=alt_va&column_name=huc_cd&column_name=basin_cd&column_name=station_type_cd&column_name=agency_use_cd&column_name=data_types_cd&column_name=instruments_cd&column_name=construction_dt&column_name=inventory_dt&column_name=drain_area_va&column_name=contrib_drain_area_va&column_name=gw_type_cd&begin_date=&end_date=&TZoutput=0&set_logscale_y=1&date_format=YYYY-MM-DD&rdb_compression=file&list_of_search_criteria=lat_long_bounding_box
    'http://waterservices.usgs.gov/nwis/site?format=rdb&bBox=-83.000000,36.500000,-81.000000,38.500000&siteOutput=expanded
    'http://waterservices.usgs.gov/nwis/site?format=rdb&bBox=-83.000000,34.500000,-81.000000,37.000000&seriesCatalogOutput=true&hasDataTypeCd=dv&siteType=GW
    ''' <summary>
    ''' Download NWIS stations within the specified region and save to a text RDB file
    ''' </summary>
    ''' <param name="aRegion"></param>
    ''' <param name="aSaveAs">File name to save downloaded station information in</param>
    ''' <param name="aStationDataType">field from NWIS.LayerSpecifications specifying which type of data to find stations for</param>
    ''' <returns>True on successful download</returns>
    ''' <remarks></remarks>
    Public Shared Function GetStationsInRegion(ByVal aRegion As Region,
                                               ByVal aSaveAs As String,
                                               ByVal aStationDataType As LayerSpecification) As Boolean
        Dim lWestLongitude As Double
        Dim lNorthLatitude As Double
        Dim lSouthLatitude As Double
        Dim lEastLongitude As Double
        Dim lDataType As String = ""
        Dim lScript As String
        Dim list_of_search_criteria As String = "&list_of_search_criteria=lat_long_bounding_box"
        Dim lSelection As String = ""
        D4EM.Data.Download.SetSecurityProtocol()
        Select Case aStationDataType
            Case NWIS.LayerSpecifications.Measurement
                lScript = "measurements"
                list_of_search_criteria = "&begin_date=&end_date=&TZoutput=0&set_logscale_y=1&date_format=YYYY-MM-DD&rdb_compression=file" & list_of_search_criteria
            Case NWIS.LayerSpecifications.Discharge
                lScript = "dv"
                lDataType = "&index_pmcode_00060=1"
            'http://waterdata.usgs.gov/nwis/dv?nw_longitude_va=-84.673834&nw_latitude_va=33&se_longitude_va=-84.5&se_latitude_va=32.4669703694319&coordinate_format=decimal_degrees
            '&index_pmcode_00060=1&sort_key=site_no&group_key=NONE&format=sitefile_output&sitefile_output_format=rdb&column_name=agency_cd&column_name=site_no&column_name=station_nm&column_name=site_tp_cd&column_name=dec_lat_va&column_name=dec_long_va&column_name=coord_datum_cd&column_name=dec_coord_datum_cd&column_name=district_cd&column_name=state_cd&column_name=county_cd&column_name=country_cd&column_name=land_net_ds&column_name=map_nm&column_name=map_scale_fc&column_name=alt_va&column_name=alt_meth_cd&column_name=alt_acy_va&column_name=alt_datum_cd&column_name=huc_cd&column_name=basin_cd&column_name=topo_cd&column_name=data_types_cd&column_name=instruments_cd&column_name=construction_dt&column_name=inventory_dt&column_name=drain_area_va&column_name=contrib_drain_area_va&column_name=tz_cd&column_name=local_time_fg&column_name=reliability_cd&column_name=gw_file_cd&column_name=nat_aqfr_cd&column_name=aqfr_cd&column_name=aqfr_type_cd&column_name=well_depth_va&column_name=hole_depth_va&column_name=depth_src_cd&column_name=project_no&column_name=rt_bol&column_name=peak_begin_date&column_name=peak_end_date&column_name=peak_count_nu&column_name=qw_begin_date&column_name=qw_end_date&column_name=qw_count_nu&column_name=gw_begin_date&column_name=gw_end_date&column_name=gw_count_nu&column_name=sv_begin_date&column_name=sv_end_date&column_name=sv_count_nu&range_selection=days&period=365&date_format=YYYY-MM-DD&rdb_compression=file&list_of_search_criteria=lat_long_bounding_box
            Case NWIS.LayerSpecifications.GroundwaterDaily
                lScript = "inventory"
                lDataType = "&data_type=" & aStationDataType.Tag
                list_of_search_criteria &= ",data_type"
                lSelection = "&hasDataTypeCd=dv&siteType=GW"
            Case Else
                lScript = "inventory"
                lDataType = "&data_type=" & aStationDataType.Tag
                list_of_search_criteria &= ",data_type"
        End Select

        aRegion.GetBounds(lNorthLatitude, lSouthLatitude, lWestLongitude, lEastLongitude, D4EM.Data.Globals.GeographicProjection)

        'Common.validateBoundingBox(WestLongitude, NorthLatitude, SouthLatitude, EastLongitude)
        Dim lURL As String
        If String.IsNullOrEmpty(lSelection) Then
            lURL = "http://waterdata.usgs.gov/nwis/" & lScript & "?" _
                + "nw_longitude_va=" + Math.Abs(lWestLongitude).ToString() _
                + "&nw_latitude_va=" + lNorthLatitude.ToString() _
                + "&se_longitude_va=" + Math.Abs(lEastLongitude).ToString() _
                + "&se_latitude_va=" + lSouthLatitude.ToString() _
                + "&coordinate_format=decimal_degrees" _
                + lDataType _
                + "&sort_key=site_no" _
                + "&group_key=NONE" _
                + "&format=sitefile_output" _
                + "&sitefile_output_format=rdb" _
                + SiteColumns _
                + list_of_search_criteria
        Else
            lURL = "http://waterservices.usgs.gov/nwis/site?format=rdb&bBox=" _
                     & DoubleToString(lWestLongitude) & "," & DoubleToString(lSouthLatitude) & "," _
                     & DoubleToString(lEastLongitude) & "," & DoubleToString(lNorthLatitude) _
                     & "&siteOutput=expanded" & lSelection
        End If
        Dim lSaveAsTemp As String = aSaveAs & ".html"
        If D4EM.Data.Download.DownloadURL(lURL, lSaveAsTemp) Then
            If aRegion.RegionSpecification = Region.RegionTypes.huc8 Then 'Only keep lines in selected HUCs
                Dim lStationsText As IEnumerable = LinesInFile(lSaveAsTemp)
                Dim lSection As Integer = -1 ' -1 = start of file, 0=any header starting with #, 1=column headers, 2=data
                Dim lFieldNames As String()
                Dim lFieldValues As String()
                Dim lHuc As String
                Dim lHucField As Integer = -1
                Dim lTotalKept As Integer = 0
                Dim lTotalDownloaded As Integer = 0
                For Each lLine As String In lStationsText
                    Try
                        Select Case lSection
                            Case -1 'First line of file
                                If Not lLine.StartsWith("#") Then GoTo BadFile
                                IO.File.WriteAllText(aSaveAs, "# " & lURL & vbCrLf)
                                IO.File.AppendAllText(aSaveAs, lLine & vbCrLf)
                                lSection = 0
                            Case 0 'Comment line near top or column names line
                                IO.File.AppendAllText(aSaveAs, lLine & vbCrLf)
                                If Not lLine.StartsWith("#") Then 'Column names line
                                    lFieldNames = lLine.Split(Chr(9))
                                    lHucField = Array.IndexOf(lFieldNames, "huc_cd")
                                    lSection += 1
                                End If
                            Case 1 'Column types line
                                IO.File.AppendAllText(aSaveAs, lLine & vbCrLf)
                                lSection += 1
                            Case Else 'Data line
                                lTotalDownloaded += 1
                                lFieldValues = lLine.Split(Chr(9))
                                lHuc = lFieldValues(lHucField)
                                If lHuc.Trim.Length = 0 OrElse aRegion.GetKeys(Region.RegionTypes.huc8).Contains(lHuc) Then
                                    IO.File.AppendAllText(aSaveAs, lLine & vbCrLf)
                                    lTotalKept += 1
                                End If
                        End Select
                    Catch e As Exception
                        Logger.Dbg("Exception reading station file record #" & lSection & ": " & lLine)
                    End Try
                Next
                Logger.Dbg("Kept " & lTotalKept & " in HUC out of " & lTotalDownloaded & " in box")
            Else
                Dim lStationsText As String = IO.File.ReadAllText(lSaveAsTemp)
                If Not lStationsText.StartsWith("#") Then GoTo BadFile
                IO.File.WriteAllText(aSaveAs, "# " & lURL & vbCrLf & lStationsText)
                Layer.AddProcessStepToFile("Downloaded from " & lURL, aSaveAs)
            End If
            TryDelete(lSaveAsTemp)
            Return True
BadFile:
            If Logger.Msg("NWIS Stations not downloaded - View error message?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                OpenFile(lSaveAsTemp)
            Else
                TryDelete(lSaveAsTemp)
            End If
        End If
        Return False
    End Function

    Public Shared Function GetStation(ByVal aStationID As String,
                                      ByVal aSaveAs As String) As Boolean

        Dim lURL As String = "http://waterdata.usgs.gov/nwis/inventory?search_site_no=" & aStationID & "&search_site_no_match_type=exact" _
                           + "&sort_key=site_no" _
                           + "&group_key=NONE" _
                           + "&format=sitefile_output" _
                           + "&sitefile_output_format=rdb" _
                           + SiteColumns _
                           + "&list_of_search_criteria=search_site_no"
        Dim lSaveAsTemp As String = aSaveAs & ".html"
        If D4EM.Data.Download.DownloadURL(lURL, lSaveAsTemp) Then
            Dim lStationsText As String = IO.File.ReadAllText(lSaveAsTemp)
            If Not lStationsText.StartsWith("#") Then GoTo BadFile
            IO.File.WriteAllText(aSaveAs, "# " & lURL & vbCrLf & lStationsText)
            Layer.AddProcessStepToFile("Downloaded from " & lURL, aSaveAs)
            TryDelete(lSaveAsTemp)
            Return True
BadFile:
            If Logger.Msg("NWIS Stations not downloaded - View error message?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                OpenFile(lSaveAsTemp)
            Else
                TryDelete(lSaveAsTemp)
            End If
        End If
        Return False
    End Function

    '(Description:="Given a lat/long box, a begin date, an end date, a USGS parameter code, and a minimum count, " & _
    ' "this method returns a string where each record is a new line, and each record contains " & _
    '   "(1) the USGS station number, " & _
    '   "(2) the number of observations, " & _
    '   "(3) the date of the first observation, and " & _
    '   "(4) the date of the last observation of the given parameter, for that station.")> _
    Private Shared Function GetStationsWithWQParameter(
        ByVal aNWLat As String,
        ByVal aNWLong As String,
        ByVal aSELat As String,
        ByVal aSELong As String,
        ByVal aStartDate As String,
        ByVal aEndDate As String,
        ByVal aParameterCode As String,
        Optional ByVal MinCount As Integer = 1) As String

        '---------------------------------------------------------------------------------------------------
        ' GET WATER QUALITY INVENTORY 
        '---------------------------------------------------------------------------------------------------

        'SAMPLE URL
        'http://nwis.waterdata.usgs.gov/nwis/qwdata
        '?nw_longitude_va=79.5&nw_latitude_va=36.5&se_longitude_va=76.3&se_latitude_va=34.5&coordinate_format=decimal_degrees
        '&sort_key=site_no&group_key=NONE&sitefile_output_format=rdb&column_name=agency_cd&column_name=site_no&column_name=station_nm
        '&column_name=lat_va&column_name=long_va&column_name=state_cd&column_name=county_cd&column_name=alt_va&column_name=huc_cd
        '&begin_date=1%2F1%2F1990&end_date=1%2F1%2F1991&inventory_output=0&format=rdb_inventory&rdb_inventory_output=value&date_format=YYYY-MM-DD
        '&rdb_compression=file&qw_sample_wide=0&list_of_search_criteria=lat_long_bounding_box

        Dim sURL As String = "http://nwis.waterdata.usgs.gov/nwis/qwdata" _
                           & "?nw_longitude_va=" & aNWLong _
                           & "&nw_latitude_va=" & aNWLat _
                           & "&se_longitude_va=" & aSELong _
                           & "&se_latitude_va=" & aSELat _
                           & "&coordinate_format=decimal_degrees" _
                           & "&column_name=lat_va&column_name=long_va&column_name=state_cd&column_name=county_cd&column_name=alt_va&column_name=huc_cd" _
                           & "&begin_date=" & Format(CDate(aStartDate), "yyyy-MM-dd") _
                           & "&end_date=" & Format(CDate(aEndDate), "yyyy-MM-dd") _
                           & "&inventory_output=0&format=rdb_inventory&rdb_inventory_output=value&date_format=YYYY-MM-DD&rdb_compression=file&qw_sample_wide=0&list_of_search_criteria=lat_long_bounding_box"

        Dim sResult As String = DownloadURL(sURL)

        If sResult.StartsWith("#") Then
            Return "# " & sURL & vbCrLf & sResult 'Return result with URL prepended as first line of header
        Else
            Return "No stations found that match request." & vbCrLf _
                 & "Request was '" & sURL & "'" & vbCrLf _
                 & "Result was:" & vbCrLf & sResult
        End If
        'Dim StreamReader As IO.StreamReader = GetHTTPStreamReader(sURL)
        'Dim Line As String = StreamReader.ReadLine

        'If ((Line Is Nothing) OrElse (Not Line.StartsWith("#"))) Then
        '    Return "No stations found that match request."
        'End If

        ''get past header lines
        'Do Until Line.Substring(0, 1) <> "#"
        '    Line = StreamReader.ReadLine
        'Loop

        ''read in column header lines (there are 2)
        'Line = StreamReader.ReadLine()
        'Line = StreamReader.ReadLine()

        'Dim Columns() As String
        'Dim Stations(3, -1) As String
        'Dim NumOfStations As Long = 0
        'Do Until Line Is Nothing
        '    Columns = Line.Split(vbTab)
        '    If Columns(2) = aParameterCode Then
        '        ReDim Preserve Stations(3, NumOfStations)
        '        Stations(0, NumOfStations) = Columns(1) ' station number
        '        Stations(1, NumOfStations) = Columns(3) ' number of observations
        '        Stations(2, NumOfStations) = Columns(4) ' start date
        '        Stations(3, NumOfStations) = Columns(5) ' end date
        '        NumOfStations += 1
        '    End If
        '    Line = StreamReader.ReadLine
        'Loop
        'If Stations.Length = 0 Then Return Nothing

        'Dim i As Long
        'Dim result As String = Nothing
        'For i = 0 To NumOfStations - 1
        '    If Stations(1, i) >= CInt(MinCount) Then
        '        result = result & Stations(0, i) & vbTab & Stations(1, i) & vbTab _
        '        & Stations(2, i) & vbTab & Stations(3, i) & vbNewLine
        '    End If
        'Next
        'If result = Nothing Then Return "No stations found that match request."
        'Return result
        '---------------------------------------------------------------------------------------------------
        ' GET Info about Stations
        '---------------------------------------------------------------------------------------------------

        'Dim i As Long
        'Dim StationIDs() As String : ReDim StationIDs(NumOfStations - 2)
        'For i = 0 To NumOfStations - 2
        '    StationIDs(i) = Stations(0, i)
        'Next

        'Dim myXPathNavigator As Xml.XPath.XPathNavigator
        'Dim XmlTextWriter As Xml.XmlTextWriter
        'GetGageInfo(StationIDs, myXPathNavigator, XmlTextWriter)

        'myXPathNavigator.MoveToRoot() ' Initialise the myXPathNavigator to start at the root

        ''Find the price of the first book. Start at the root node
        'Console.WriteLine()
        'Console.WriteLine("Find the price of the first book by navigating nodes ...")
        'myXPathNavigator.MoveToRoot()
        'myXPathNavigator.MoveToFirstChild()
        'myXPathNavigator.MoveToFirstChild()
        'XmlTextWriter.WriteElementString("val_num", "hi there")
        'Debug.WriteLine(myXPathNavigator.Value())
        'myXPathNavigator.MoveToNext()
        'Debug.WriteLine(myXPathNavigator.Value())

        ''DisplayNode(True, myXPathNavigator)  ' root node
        ''DisplayNode(myXPathNavigator.MoveToFirstChild(), myXPathNavigator)  ' ?xml version


        'XmlTextWriter.Flush()
        'XmlTextWriter.Close()
    End Function

    Friend Shared Function MakeStationShapefile(ByVal aProject As Project,
                                         ByVal aStationDataType As LayerSpecification,
                                         ByVal aAllStations As atcTableRDB,
                                         ByVal aSaveAs As String) As String
        Try
            Dim lLongField As Integer = aAllStations.FieldNumber("dec_long_va")
            Dim lLatField As Integer = aAllStations.FieldNumber("dec_lat_va")
            If lLongField < 1 Then
                Throw New ApplicationException("Station table missing dec_long_va field, cannot create shape file")
            ElseIf lLatField < 1 Then
                Throw New ApplicationException("Station table missing dec_lat_va field, cannot create shape file")
            Else
                Dim aFieldNameNonZero As String = aStationDataType.Tag & "_count_nu"
                Dim lFieldNonZero As Integer = aAllStations.FieldNumber(aFieldNameNonZero)
                Dim lLastField As Integer = aAllStations.NumFields - 1
                Dim lCurField As Integer
                Dim lCurFieldValue As String
                MkDirPath(PathNameOnly(aSaveAs))
                TryDeleteShapefile(aSaveAs)

                Dim lNewShapefile As New DotSpatial.Data.FeatureSet(DotSpatial.Data.FeatureType.Point)

                lNewShapefile.Projection = D4EM.Data.Globals.GeographicProjection
                For lCurField = 0 To lLastField
                    Dim lNewField As New DotSpatial.Data.Field(aAllStations.FieldName(lCurField + 1).Replace("discharge", "q"), DotSpatial.Data.FieldDataType.String) 'MapWinGIS.Field
                    lNewField.Length = aAllStations.FieldLength(lCurField + 1)
                    lNewShapefile.DataTable.Columns.Add(lNewField)
                Next
                Dim lShapeIndex As Integer = -1
                Dim lLastStation As Integer = aAllStations.NumRecords

                'Make sure there is at least one station with the desired nonzero field
                'If not, skip checking for nonzero field
                If lFieldNonZero > 0 Then
                    Dim lFoundStation As Boolean = False
                    For iStation As Integer = 1 To lLastStation
                        aAllStations.CurrentRecord = iStation
                        lCurFieldValue = aAllStations.Value(lFieldNonZero)
                        If IsNumeric(lCurFieldValue) AndAlso CInt(lCurFieldValue) > 0 Then
                            lFoundStation = True
                            Exit For
                        End If
                    Next
                    If Not lFoundStation Then
                        Logger.Dbg("No stations found with nonzero field " & aFieldNameNonZero & ", including all stations")
                        lFieldNonZero = 0
                    End If
                End If

                'Include all stations if we don't have a field to check
                Dim lIncludeThisStation As Boolean = (lFieldNonZero < 1)

                Logger.Status("Scanning " & Format(lLastStation, "#,###") & " stations for " & IO.Path.GetFileNameWithoutExtension(aSaveAs), True)
                Dim lNumExceptions As Integer = 0
                For iStation As Integer = 1 To lLastStation
                    aAllStations.CurrentRecord = iStation
                    If lFieldNonZero > 0 Then
                        lCurFieldValue = aAllStations.Value(lFieldNonZero)
                        lIncludeThisStation = IsNumeric(lCurFieldValue) AndAlso CInt(lCurFieldValue) > 0
                    End If
                    If lIncludeThisStation Then
                        Try
                            Dim lX As Double = aAllStations.Value(lLongField)
                            Dim lY As Double = aAllStations.Value(lLatField)
                            Dim lCoordinate As New NetTopologySuite.Geometries.Coordinate(lX, lY)
                            Dim lPoint As New NetTopologySuite.Geometries.Point(lCoordinate)
                            Dim lFeature As DotSpatial.Data.IFeature = lNewShapefile.AddFeature(lPoint)
                            For lCurField = 0 To lLastField
                                'Debug.Print(lCurField & " " & lNewShapefile.Field(lCurField).Name & " " & lNewShapefile.Field(lCurField).Width & " " & aAllStations.Value(lCurField + 1).Length & " " & aAllStations.Value(lCurField + 1))
                                lFeature.DataRow(lCurField) = SafeSubstring(aAllStations.Value(lCurField + 1), 0, 255)
                            Next
                        Catch exStation As Exception
                            lNumExceptions += 1
                            If lNumExceptions < 5 Then
                                Logger.Dbg("Exception adding station to shapefile, row " & iStation & " of " & aAllStations.FileName & " " & exStation.Message)
                            End If
                        End Try
                    End If
                    Logger.Progress(iStation, lLastStation)
                Next
                If lNumExceptions > 4 Then
                    Logger.Dbg("Exceptions occurred when adding " & Format(lNumExceptions, "#,###") & " stations out of " & Format(lLastStation, "#,###"))
                End If
                lNewShapefile.Filename = aSaveAs
                lNewShapefile.Save()
                Logger.Status("")

                If IO.File.Exists(aSaveAs) Then
                    Dim lLayer As New Layer(lNewShapefile, aStationDataType)
                    'aProject.Layers.Add(lLayer)
                    lLayer.CopyProcStepsFromCachedFile(aAllStations.FileName)
                    lLayer.AddProcessStep("Created shapefile of NWIS stations")
                    lLayer.Reproject(aProject.DesiredProjection)
                    aProject.Layers.Add(lLayer)
                    Return "<add_shape>" & aSaveAs & "</add_shape>" & vbCrLf
                Else
                    lNewShapefile.Close()
                    Logger.Dbg("No shape file created for '" & aSaveAs & "'")
                End If
            End If
        Catch e As Exception
            Logger.Dbg("Exception creating NWIS shape file '" & aSaveAs & "' " & e.Message & vbCrLf & e.StackTrace)
        End Try
        Return ""
    End Function

End Class
