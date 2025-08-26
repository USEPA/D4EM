Imports MapWinUtility
Imports atcUtility
Imports D4EM.Data
Imports D4EM.Data.LayerSpecification
Imports D4EM.Geo

Public Class BASINS

    Public Class LayerSpecifications
        Public Class Census
            Public Shared all As New LayerSpecification(Name:="Census", Tag:="census")
            Public Shared county1990 As New LayerSpecification(Name:="Census 1990 Counties", FilePattern:="*_co90.shp", Role:=Roles.County, IdFieldName:="FIPS", Source:=GetType(BASINS))
            Public Shared county2000 As New LayerSpecification(Name:="Census 2000 Counties", FilePattern:="*_co00.shp", Role:=Roles.County, IdFieldName:="FIPS", Source:=GetType(BASINS))
            Public Shared landmark2002 As New LayerSpecification(Name:="Census 2002 Landmarks", FilePattern:="*_tgr_d.shp", Source:=GetType(BASINS))
            Public Shared place1990 As New LayerSpecification(Name:="Census 1990 Places", FilePattern:="*_pl90.shp", Role:=Roles.Unknown, IdFieldName:="PLC", Source:=GetType(BASINS))
            Public Shared place2000 As New LayerSpecification(Name:="Census 2000 Places", FilePattern:="*_pl00.shp", Role:=Roles.Unknown, IdFieldName:="PLC", Source:=GetType(BASINS))
            Public Shared railroad2002 As New LayerSpecification(Name:="Census 2002 Railroads", FilePattern:="*_tgr_b.shp", Role:=Roles.Railroad, IdFieldName:="TLID", Source:=GetType(BASINS))
            Public Shared road2002 As New LayerSpecification(Name:="Census 2002 Roads", FilePattern:="*_tgr_a.shp", Role:=Roles.Road, IdFieldName:="TLID", Source:=GetType(BASINS))
            Public Shared tract1990 As New LayerSpecification(Name:="Census 1990 Tracts", FilePattern:="*_tr90.shp", Role:=Roles.OtherBoundary, IdFieldName:="NAME", Source:=GetType(BASINS))
            Public Shared tract2000 As New LayerSpecification(Name:="Census 2000 Tracts", FilePattern:="*_tr00.shp", Role:=Roles.OtherBoundary, IdFieldName:="NAME", Source:=GetType(BASINS))
            Public Shared zipcode2000 As New LayerSpecification(Name:="Census 2000 ZIP", FilePattern:="*_zt00.shp", Role:=Roles.ZIP, IdFieldName:="NAME", Source:=GetType(BASINS))
            'Public Shared bg1990 As New LayerSpecification(Name:="Census 1990 Block Groups", FilePattern:="*_bg90.shp", Role:=Roles.OtherBoundary, IdFieldName:="NAME", Source:=GetType(BASINS))
            'Public Shared bg2000 As New LayerSpecification(Name:="Census 2000 Block Groups", FilePattern:="*_bg00.shp", Role:=Roles.OtherBoundary, IdFieldName:="NAME", Source:=GetType(BASINS))
            'Public Shared tigerc2002 As New LayerSpecification(Name:="Census 2002 Roads", FilePattern:="*_tgr_a.shp", Role:=Roles.Road, IdFieldName:="TLID", Source:=GetType(BASINS))
            'TODO: add other Census layers
        End Class

        Public Class core31
            Public Shared all As New LayerSpecification(Name:="core31", Tag:="core31")
            Public Shared acc As New LayerSpecification(
                FilePattern:="acc.shp", Tag:="core31.acc", Role:=Roles.SubBasin, IdFieldName:="ACC_ID", Name:="Accounting Unit Boundaries", Source:=GetType(BASINS))
            Public Shared bac_stat As New LayerSpecification(
                FilePattern:="bac_stat.shp", Tag:="core31.bac_stat", Role:=Roles.Station, IdFieldName:="ID", Name:="Bacteria Stations", Source:=GetType(BASINS))
            Public Shared cat As New LayerSpecification(
                FilePattern:="cat.shp", Tag:="core31.cat", Role:=Roles.SubBasin, IdFieldName:="CU", Name:="Cataloging Unit Boundaries", Source:=GetType(BASINS))
            Public Shared catpt As New LayerSpecification(
                FilePattern:="catpt.shp", Tag:="core31.catpt", Role:=Roles.Unknown, IdFieldName:="CU", Name:="Cataloging Unit Code", Source:=GetType(BASINS))
            Public Shared cnty As New LayerSpecification(
                FilePattern:="cnty.shp", Tag:="core31.cnty", Role:=Roles.County, IdFieldName:="FIPS", Name:="County Boundaries", Source:=GetType(BASINS))
            Public Shared cntypt As New LayerSpecification(
                FilePattern:="cntypt.shp", Tag:="core31.cntypt", Role:=Roles.Unknown, IdFieldName:="FIPS", Name:="County Names", Source:=GetType(BASINS))
            Public Shared ecoreg As New LayerSpecification(
                FilePattern:="ecoreg.shp", Tag:="core31.ecoreg", Role:=Roles.OtherBoundary, IdFieldName:="ECOREG_ID", Name:="Ecoregions (Level III)", Source:=GetType(BASINS))
            Public Shared epa_reg As New LayerSpecification(
                FilePattern:="epa_reg.shp", Tag:="core31.epa_reg", Role:=Roles.OtherBoundary, IdFieldName:="EPA_REG_ID", Name:="EPA Region Boundaries", Source:=GetType(BASINS))
            Public Shared fhards As New LayerSpecification(
                FilePattern:="fhards.shp", Tag:="core31.fhards", Role:=Roles.Road, IdFieldName:="RECID", Name:="Major Roads", Source:=GetType(BASINS))
            Public Shared lulcndx As New LayerSpecification(
                FilePattern:="lulcndx.shp", Tag:="core31.lulcndx", Role:=Roles.Unknown, IdFieldName:="COVNAME", Name:="Land Use Index", Source:=GetType(BASINS))
            Public Shared mad As New LayerSpecification(
                FilePattern:="mad.shp", Tag:="core31.mad", Role:=Roles.OtherBoundary, IdFieldName:="SITE_CODE", Name:="Managed Area Database", Source:=GetType(BASINS))
            Public Shared nawqa As New LayerSpecification(
                FilePattern:="nawqa.shp", Tag:="core31.nawqa", Role:=Roles.OtherBoundary, IdFieldName:="NAWQA", Name:="NAWQA Study Area Unit Boundaries", Source:=GetType(BASINS))
            Public Shared pcs3 As New LayerSpecification(
                FilePattern:="pcs3.shp", Tag:="core31.pcs3", Role:=Roles.Station, IdFieldName:="NPDES", Name:="Permit Compliance System", Source:=GetType(BASINS))
            Public Shared rf1 As New LayerSpecification(
                FilePattern:="rf1.shp", Tag:="core31.rf1", Role:=Roles.Hydrography, IdFieldName:="RIVRCH", Name:="Reach File, V1", Source:=GetType(BASINS))
            Public Shared st As New LayerSpecification(
                FilePattern:="st.shp", Tag:="core31.st", Role:=Roles.State, IdFieldName:="ST", Name:="State Boundaries", Source:=GetType(BASINS))
            Public Shared statsgo As New LayerSpecification(
                FilePattern:="statsgo.shp", Tag:="core31.statsgo", Role:=Roles.Soil, IdFieldName:="MUID", Name:="State Soil", Source:=GetType(BASINS))
            Public Shared urban As New LayerSpecification(
                FilePattern:="urban.shp", Tag:="core31.urban", Role:=Roles.OtherBoundary, IdFieldName:="URBAN_ID", Name:="Urban Area Boundaries", Source:=GetType(BASINS))
            Public Shared urban_nm As New LayerSpecification(
                FilePattern:="urban_nm.shp", Tag:="core31.urban_nm", Role:=Roles.Unknown, IdFieldName:="UA_CODE", Name:="Urban Area Name", Source:=GetType(BASINS))
            'old Types, not in use
            '("ifd", "NPD") 'Industrial Facilities Discharge 
            '("nsi", "NSI_STAT") 'National Sediment Inventory Stations
            '("303d", "") '303d
            '("tri", "ID") 'Toxic Release Inventory 
        End Class

        Public Shared dem As New LayerSpecification(Name:="DEM Shape", Tag:="dem", FilePattern:="\\dem\\[0-9]+.shp", Role:=Roles.Elevation, Source:=GetType(BASINS))
        Public Shared DEMG As New LayerSpecification(Name:="DEM Grid", Tag:="DEMG", FilePattern:="demg.tif", Role:=Roles.Elevation, Source:=GetType(BASINS))
        Public Shared giras As New LayerSpecification(Name:="GIRAS", Tag:="giras", FilePattern:="l_.*.shp", Role:=Roles.LandUse, Source:=GetType(BASINS))
        Public Shared lstoret As New LayerSpecification(Name:="Legacy STORET", Tag:="lstoret", FilePattern:="_lstoret.shp", Source:=GetType(BASINS))
        Public Shared NED As New LayerSpecification(Name:="NED", Tag:="NED", FilePattern:="ned.tif", Role:=Roles.Elevation, Source:=GetType(BASINS))
        Public Shared nhd As New LayerSpecification(Name:="NHD", Tag:="nhd", FilePattern:="\\nhd\\[0-9]+.shp", Role:=Roles.Hydrography, Source:=GetType(BASINS))
        Public Shared pcs3 As New LayerSpecification(Name:="PCS3", Tag:="pcs3", FilePattern:="pcs3.shp", Role:=Roles.Station, Source:=GetType(BASINS))
        'Public Shared d303 As New LayerSpecification(Name:="303(d)", Tag:="303d", Role:=Roles.Station, Source:=GetType(BASINS))
        Public Shared huc12 As New LayerSpecification(Name:="HUC-12", Tag:="huc12", FilePattern:="huc12.shp", Role:=Roles.SubBasin, IdFieldName:="HUC_12", Source:=GetType(BASINS))
        Public Shared MetStation As New LayerSpecification(Name:="BASINS Met Station", Tag:="MetStation", FilePattern:="met.shp", Role:=Roles.MetStation, Source:=GetType(BASINS))
    End Class

    'Public Shared BASINSDataTypeName As String() = {"census", "core31", "dem", "DEMG", "giras", "lstoret", "NED", "nhd", "pcs3", "303d", "huc12"}
    'Public Enum BASINSDataType
    '    census = 0 : core31 = 1 : dem = 2 : DEMG = 3 : giras = 4 : lstoret = 5 : NED = 6 : nhd = 7 : pcs3 = 8 : d303 = 9 : huc12 = 10
    'End Enum

    Private Shared pNativeProjection As DotSpatial.Projections.ProjectionInfo = Globals.GeographicProjection

    'Private Shared pBaseURL As String = "http://www.epa.gov/waterscience/ftp/basins/gis_data/huc/"
    'Private Shared pBaseMetURL As String = "http://www.epa.gov/waterscience/ftp/basins/met_data/"
    'Private Shared pBaseURL As String = "http://www3.epa.gov/ceampubl/basins/gis_data/huc/"
    'Private Shared pBaseURLnew As String = "ftp://newftp.epa.gov/exposure/BasinsData/BasinsCoreData/"
    'Private Shared pBaseMetURL As String = "http://www3.epa.gov/ceampubl/basins/met_data/"
    Private Shared pBaseURLga As String = "https://gaftp.epa.gov/Exposure/BasinsData/BasinsCoreData/"
    Private Shared pBaseURL As String = "https://usgs.osn.mghpcc.org/mdmf/epa_basins/BasinsCoreData/"
    Private Shared pBaseMetURLnew As String = "ftp://newftp.epa.gov/Exposure/BasinsData/met_data/"
    Private Shared pBaseMetURLga As String = "https://gaftp.epa.gov/Exposure/BasinsData/met_data/"

    Public Shared MinRequiredConstituents() As String = {"PREC", "PEVT"}
    Public Shared MaxRequiredConstituents() As String = {"ATEM", "WIND", "SOLR", "DEWP", "CLOU", "PREC", "PEVT"} 'TODO: set based on HSPF model requirements

    Private Shared Function GetMetStationList(ByRef lDSNField As Integer, _
                                              ByRef lLatitudeField As Integer, _
                                              ByRef lLongitudeField As Integer, _
                                              ByRef lLocationField As Integer, _
                                              ByRef lStartDateField As Integer, _
                                              ByRef lEndDateField As Integer) As atcTableDBF
        Dim lAllStationsFilename As String = FindFile("Met Data Station List", "MetStations.dbf")
        If IO.File.Exists(lAllStationsFilename) Then
            Dim lAllStations As New atcTableDBF
            If lAllStations.OpenFile(lAllStationsFilename) Then
                lDSNField = lAllStations.FieldNumber("DSN")
                lLatitudeField = lAllStations.FieldNumber("LATITUDE")
                lLongitudeField = lAllStations.FieldNumber("LONGITUDE")
                lLocationField = lAllStations.FieldNumber("LOCATION")
                lStartDateField = lAllStations.FieldNumber("STARTDATE")
                If lStartDateField = 0 Then Logger.Dbg("No STARTDATE field in " & lAllStationsFilename)
                lEndDateField = lAllStations.FieldNumber("ENDDATE")
                If lEndDateField = 0 Then Logger.Dbg("No ENDDATE field in " & lAllStationsFilename)

                If lDSNField = 0 Then
                    Logger.Dbg("No DSN field in " & lAllStationsFilename)
                ElseIf lLatitudeField = 0 Then
                    Logger.Dbg("No LATITUDE field in " & lAllStationsFilename)
                ElseIf lLongitudeField = 0 Then
                    Logger.Dbg("No LONGITUDE field in " & lAllStationsFilename)
                ElseIf lLocationField = 0 Then
                    Logger.Dbg("No LOCATION field in " & lAllStationsFilename)
                Else
                    Return lAllStations
                End If
            Else
                Logger.Dbg("Unable to open MetStations list: " & lAllStationsFilename)
            End If
        Else
            Logger.Dbg("Unable to find Met Data Station List MetStations.dbf")
        End If
        Return Nothing
    End Function

    ''' <summary>
    ''' Get BASINS Meterologic stations and/or data
    ''' </summary>
    ''' <param name="aProject">project to add data to</param>
    ''' <param name="aStationIDs">
    ''' Input:
    ''' If aStationIDs contains values, only those stations will be checked for being in the region,
    ''' If aStationIDs is Nothing or .Count = 0, then all stations will be searched for ones in the region
    ''' Output: Station IDs found in project region</param>
    ''' <param name="aCreateShapefile">True to create a point shapefile of met stations</param>
    ''' <param name="aStationsShapeFilename">If creating shapefile, can optionally provide full path of file to create.
    ''' If not set, defaults to aProject.ProjectFolder / met / met.shp</param>
    ''' <returns>XML describing success or error</returns>
    Public Shared Function GetMetStations(ByVal aProject As D4EM.Data.Project, _
                                          ByRef aStationIDs As Generic.List(Of String), _
                                          ByVal aCreateShapefile As Boolean, _
                                          ByVal aStationsShapeFilename As String, _
                                          ByVal aCatchmentsLayer As D4EM.Data.Layer, _
                                          ByVal aMethod As String, _
                                 Optional ByVal aStartDate As Date = #1/1/1000#, _
                                 Optional ByVal aEndDate As Date = #1/1/1000#) As String
        Dim lResults As String = ""
        Dim lCacheFilename As String = ""
        Dim lDataTypeString As String = "met"

        Dim lDSNField As Integer
        Dim lLatitudeField As Integer
        Dim lLongitudeField As Integer
        Dim lLocationField As Integer
        Dim lStartDateField As Integer
        Dim lEndDateField As Integer
        Dim lAllStations As atcTableDBF = GetMetStationList(lDSNField, lLatitudeField, lLongitudeField, lLocationField, lStartDateField, lEndDateField)
        If lAllStations IsNot Nothing Then
            Dim lLocation As String
            Dim lStationsInRegion As atcTableDBF = Nothing
            Dim lFirstNewShape As Integer = 1 'Default: all shapes are new
            Dim lLocationProgress As Integer = 0

            Dim lCacheFolder As String = IO.Path.Combine(aProject.CacheFolder, "clsBASINS" & g_PathChar & lDataTypeString & g_PathChar)
            IO.Directory.CreateDirectory(lCacheFolder)
            Dim lSaveIn As String = IO.Path.Combine(aProject.ProjectFolder, lDataTypeString) & g_PathChar
            IO.Directory.CreateDirectory(lSaveIn)

            If String.IsNullOrEmpty(aStationsShapeFilename) Then
                aStationsShapeFilename = "met.shp"
            End If
            If Not IO.Path.IsPathRooted(aStationsShapeFilename) Then
                aStationsShapeFilename = IO.Path.Combine(lSaveIn, aStationsShapeFilename)
            End If
            Dim lLocationDBFFilename As String = IO.Path.ChangeExtension(aStationsShapeFilename, ".dbf")

            Dim lStationsLayer As DotSpatial.Data.PointShapefile = Nothing
            If aCreateShapefile Then
                If IO.File.Exists(aStationsShapeFilename) Then
                    Try
                        'atcMwGisUtility.GisUtil.RemoveLayer(atcMwGisUtility.GisUtil.LayerIndex(lStationsShapeFilename))
                    Catch 'Ignore exception if layer was not on map
                    End Try
                    lStationsInRegion = New atcTableDBF
                    lStationsInRegion.OpenFile(lLocationDBFFilename)
                    lFirstNewShape = lStationsInRegion.NumRecords + 1 'Only shapes added after these are new
                    lStationsLayer = DotSpatial.Data.PointShapefile.Open(aStationsShapeFilename)
                Else
                    lStationsLayer = New DotSpatial.Data.PointShapefile(aStationsShapeFilename)
                    lStationsLayer.FilePath = aStationsShapeFilename.ToString()
                    lStationsLayer.Filename = aStationsShapeFilename.ToString() ' lSaveIn.ToString()
                    lStationsLayer.SaveAs(aStationsShapeFilename, True)
                    lStationsLayer.Close()
                    lStationsLayer = DotSpatial.Data.PointShapefile.Open(aStationsShapeFilename)
                End If
                'lStationsLayer.StartEditingShapes()
            End If

            If aStationIDs Is Nothing Then
                aStationIDs = New Generic.List(Of String)
            End If
            If aStationIDs.Count > 0 Then
                GetStationsInList(lAllStations, lLocationField, aStationIDs, lStationsInRegion)
            Else
                If aCatchmentsLayer IsNot Nothing Then 'Look for met station closest to each catchment with at least MinRequiredConstituents
                    Dim lMetStationFieldIndex As Integer = NetworkOperations.FieldIndexes.EnsureFieldExists(aCatchmentsLayer.AsFeatureSet, "ModelSeg", GetType(String))
                    For Each lCatchment As DotSpatial.Data.Feature In aCatchmentsLayer.AsFeatureSet.Features
                        Dim lCatchmentID As String = lCatchment.DataRow(aCatchmentsLayer.IdFieldIndex)
                        Logger.Dbg("Finding met station for catchment " & lCatchmentID)
                        Dim lCatchmentRegion As New Region(aCatchmentsLayer, lCatchmentID)
                        Dim lStationID As String = GetStationsInRegion(lAllStations, _
                                                                       lCatchmentRegion, _
                                                                       aMethod, lLocationField, lLatitudeField, lLongitudeField, lStartDateField, lEndDateField, _
                                                                       aStartDate, aEndDate, _
                                                                       MinRequiredConstituents, _
                                                                       lStationsInRegion)
                        lCatchment.DataRow(lMetStationFieldIndex) = lStationID
                    Next
                End If

                'Find one met station that has all required constituents (will not add again if already found for a catchment)
                Logger.Dbg("Finding met station with all constituents")
                GetStationsInRegion(lAllStations, aProject.Region, aMethod, _
                                    lLocationField, lLatitudeField, lLongitudeField, _
                                    lStartDateField, lEndDateField, aStartDate, aEndDate, MaxRequiredConstituents, _
                                    lStationsInRegion)

                If aCatchmentsLayer IsNot Nothing Then aCatchmentsLayer.AsFeatureSet.Save() 'Save updated column with met station IDs
                With lStationsInRegion
                    If .NumRecords > 0 Then
                        .CurrentRecord = 1
                        Do
                            lLocation = .Value(lLocationField)
                            If Not aStationIDs.Contains(lLocation) Then
                                aStationIDs.Add(lLocation)
                            End If
                            If .CurrentRecord < .NumRecords Then
                                .CurrentRecord += 1
                            Else
                                Exit Do
                            End If
                        Loop
                    End If
                End With
            End If

            With lStationsInRegion
                If aStationIDs.Count > 0 Then
                    If aCreateShapefile Then
                        Dim lNextDestinationDSN As Integer = 1
                        For Each lLocation In aStationIDs
                            lLocationProgress += 1
                            'Add new location to shape file
                            If .FindFirst(lLocationField, lLocation, lFirstNewShape) Then
                                Do
                                    Dim lPoint As New DotSpatial.Topology.Coordinate(.Value(lLongitudeField), .Value(lLatitudeField))
                                    Dim lShape As New DotSpatial.Data.Shape(lPoint)
                                    DotSpatial.Projections.Reproject.ReprojectPoints(lShape.Vertices, lShape.Z, Globals.GeographicProjection, aProject.DesiredProjection, 0, 1)
                                    lStationsLayer.AddShape(lShape)
                                    If lDSNField > 0 AndAlso lNextDestinationDSN > 1 Then
                                        .Value(lDSNField) = .Value(lDSNField) + lNextDestinationDSN - 1
                                    End If
                                    If .CurrentRecord = .NumRecords Then Exit Do
                                    .CurrentRecord += 1
                                Loop While .Value(lLocationField) = lLocation
                            End If
                        Next

                        lStationsLayer.Projection = aProject.DesiredProjection
                        lStationsLayer.SaveAs(aStationsShapeFilename, True)

                        .WriteFile(lLocationDBFFilename)
                        Dim lLayer As New Layer(aStationsShapeFilename, BASINS.LayerSpecifications.MetStation, False)
                        aProject.Layers.Add(lLayer)
                        lLayer.AddProcessStep("Created shapefile of met data stations")
                        lResults &= "<add_shape>" & aStationsShapeFilename & "</add_shape>" & vbCrLf
                        'TODO: aProject.Layers.Add(lStationsShapeFilename)
                    End If
                Else
                    lResults &= "<message>BASINS Meterologic: No stations found in region</message>"
                End If
            End With
        End If
        Return lResults
    End Function


    ''' <summary>
    ''' Get BASINS Meterologic data for the given stations
    ''' </summary>
    ''' <param name="aProject">project to add data to</param>
    ''' <param name="aStationIDs">Station ID numbers to get data from</param>
    ''' <param name="aMetWDM">Save met data in this WDM file</param>
    ''' <returns>XML describing success or error</returns>
    Public Shared Function GetMetData(ByVal aProject As D4EM.Data.Project, _
                                      ByVal aStationIDs As Generic.List(Of String), _
                             Optional ByVal aMetWDM As String = Nothing) As String
        Dim lResults As String = ""
        Dim lCreateWDM As Boolean = Not String.IsNullOrEmpty(aMetWDM)
        Dim lCacheFilename As String = ""
        Dim lDataTypeString As String = "met"

        Dim lCacheFolder As String = IO.Path.Combine(aProject.CacheFolder, "clsBASINS" & g_PathChar & lDataTypeString & g_PathChar)
        IO.Directory.CreateDirectory(lCacheFolder)
        Dim lSaveIn As String = IO.Path.Combine(aProject.ProjectFolder, lDataTypeString) & g_PathChar
        IO.Directory.CreateDirectory(lSaveIn)
        If lCreateWDM AndAlso IO.Path.GetDirectoryName(aMetWDM).Length = 0 Then
            aMetWDM = IO.Path.Combine(lSaveIn, aMetWDM)
        End If

        Dim lDSNField As Integer
        Dim lLatitudeField As Integer
        Dim lLongitudeField As Integer
        Dim lLocationField As Integer
        Dim lAllStations As atcTableDBF = GetMetStationList(lDSNField, lLatitudeField, lLongitudeField, lLocationField, 0, 0)
        If lAllStations IsNot Nothing Then
            Dim lLocation As String
            Dim lStationsInRegion As atcTableDBF = Nothing
            Dim lLocationProgress As Integer = 0

            If aStationIDs.Count > 0 Then
                Dim lDestinationWDM As atcWDM.atcDataSourceWDM = Nothing
                Dim lNextDestinationDSN As Integer = 1
                Dim lShowProgress As Boolean = (aStationIDs.Count > 1)
                If lShowProgress Then Logger.Progress(lLocationProgress, aStationIDs.Count)
                For Each lLocation In aStationIDs
                    lLocationProgress += 1
                    If lShowProgress Then Logger.Progress(lLocation, lLocationProgress, aStationIDs.Count)
                    Using lLevel As New ProgressLevel(False, Not lShowProgress)
                        lLevel.Reset()
                        lCacheFilename = IO.Path.Combine(lCacheFolder, lLocation & ".wdm")
                        Dim lCacheMetadataFilename As String = lCacheFilename & ".xml"
                        Dim lZipFilename As String = IO.Path.ChangeExtension(lCacheFilename, ".zip")

                        If aProject.GetEvenIfCached Then
                            TryDelete(lCacheFilename)
                            TryDelete(lCacheMetadataFilename)
                            TryDelete(lZipFilename)
                        End If

                        If FileExists(lCacheFilename) Then
                            Logger.Dbg("Using cached '" & lCacheFilename & "'")
                        Else
                            If Not FileExists(lZipFilename) Then
                                Dim lURL As String = pBaseMetURLnew & lLocation & ".zip"
                                If Not aProject.GetEvenIfCached Then
                                    Select Case My.Computer.Name.ToUpper 'Developer machines get from local server
                                        Case "WIZ", "RUNNER", "ZORRO", "XOR", "HOUSE", "TONGWORKSTATION", "ZAP"
                                            lURL = lURL.Replace(pBaseMetURLnew, "http://hspf.com/BasinsMet/")
                                    End Select
                                End If
                                If Not D4EM.Data.Download.DownloadURL(lURL, lZipFilename) Then
                                    Logger.Dbg("failed epa ceam '" & lURL)
                                    lURL = pBaseMetURLga & lLocation & ".zip"
                                    If Not D4EM.Data.Download.DownloadURL(lURL, lZipFilename) Then
                                        Logger.Dbg("failed gaftp '" & lURL)
                                    End If
                                End If
                            End If
                            'extract compressed download
                            If FileExists(lZipFilename) Then
                                Try
                                    Zipper.UnzipFile(lZipFilename, IO.Path.GetDirectoryName(lCacheFilename))
                                Catch ex As Exception
                                    Logger.Dbg("Exception unzipping '" & lZipFilename & "': " & ex.Message)
                                End Try
                            Else
                                Logger.Dbg("No zip file found after download at '" & lZipFilename & "'")
                            End If
                        End If
                        If Not lCreateWDM AndAlso Not aProject.CacheOnly AndAlso FileExists(lCacheFilename) Then
                            ' copy individual WDM files to project folder and add to project
                            Dim lDestFileName As String = IO.Path.Combine(lSaveIn, IO.Path.GetFileName(lCacheFilename))
                            If Not FileExists(lDestFileName, True) Then
                                If TryCopy(lCacheFilename, lDestFileName) Then
                                    TryCopy(lCacheFilename & ".xml", lDestFileName & ".xml")
                                    Dim lNewResult As String = "<add_data type='Timeseries::WDM'>" & lDestFileName & "</add_data>" & vbCrLf
                                    If Not lResults.Contains(lNewResult) Then lResults &= lNewResult
                                    Dim lDestWDM As New atcWDM.atcDataSourceWDM
                                    If lDestWDM.Open(lDestFileName) Then
                                        aProject.TimeseriesSources.Add(lDestWDM)
                                    End If
                                End If
                            End If
                        End If
                    End Using
                Next
                If lCreateWDM Then
                    Logger.Status("Saving in '" & aMetWDM & "'", True)
                    lLocationProgress = 0

                    lDestinationWDM = aProject.TimeseriesSourceBySpecification(aMetWDM)
                    If lDestinationWDM Is Nothing Then
                        MkDirPath(IO.Path.GetDirectoryName(aMetWDM))
                        If atcData.atcDataManager.OpenDataSource(aMetWDM) Then
                            lDestinationWDM = atcData.atcDataManager.DataSourceBySpecification(aMetWDM)
                        End If
                    End If

                    'If IO.File.Exists(aMetWDM) Then
                    '    lDestinationWDM = atcData.atcDataManager.DataSourceBySpecification(aMetWDM)
                    'End If
                    If lDestinationWDM Is Nothing Then
                        'lDestinationWDM = New atcWDM.atcDataSourceWDM
                        'If Not lDestinationWDM.Open(aMetWDM) Then
                        lResults &= "<message>BASINS Meterologic: Could not open or create WDM file '" & aMetWDM & "'</message>"
                        '    lDestinationWDM = Nothing
                        'End If
                    End If
                    If lDestinationWDM IsNot Nothing Then
                        'Skip any existing data sets when numbering new ones
                        If lDestinationWDM.DataSets.Count > 0 Then
                            Dim lLastDSN As Integer = 0
                            For Each lDataSet As atcData.atcDataSet In lDestinationWDM.DataSets
                                lLastDSN = Math.Max(lLastDSN, lDataSet.Attributes.GetValue("ID"))
                            Next
                            While lLastDSN >= lNextDestinationDSN
                                lNextDestinationDSN += 10
                            End While
                        End If
                        For Each lLocation In aStationIDs
                            lLocationProgress += 1
                            If lShowProgress Then Logger.Progress(lLocation, lLocationProgress, aStationIDs.Count)
                            Using lLevel As New ProgressLevel(False, Not lShowProgress)
                                'import only locations not already in this WDM
                                If lDestinationWDM.DataSets.FindData("Location", lLocation, 1).Count = 0 Then
                                    lCacheFilename = IO.Path.Combine(lCacheFolder, lLocation & ".wdm")
                                    If FileExists(lCacheFilename) Then
                                        Dim lCacheWDM As New atcWDM.atcDataSourceWDM
                                        Logger.Dbg("Opening '" & lCacheFilename & "'")
                                        lCacheWDM.Open(lCacheFilename)
                                        For lCacheDSN As Integer = 1 To 10
                                            If lCacheWDM.DataSets.Keys.Contains(lCacheDSN) Then
                                                Dim lDataSet As atcData.atcTimeseries = lCacheWDM.DataSets.ItemByKey(lCacheDSN)
                                                lDataSet.EnsureValuesRead()
                                                lDataSet.Attributes.SetValue("id", lNextDestinationDSN)

                                                'Should never have existing dataset in lDestinationWDM, so ExistAction should not matter
                                                If lDestinationWDM.AddDataset(lDataSet, atcData.atcDataSource.EnumExistAction.ExistNoAction) Then
                                                    Logger.Dbg("GetBASINSMeterologic:Added DSN " & lCacheDSN & " from " & lCacheFilename & " as DSN " & lNextDestinationDSN)
                                                Else
                                                    Logger.Msg("Failed to add DSN " & lCacheDSN & " from " & lCacheFilename & " as DSN " & lNextDestinationDSN, "GetBASINSMeterologic")
                                                End If
                                                'lDataSet.numValues = 0
                                                lDataSet.ValuesNeedToBeRead = True
                                            End If
                                            lNextDestinationDSN += 1
                                        Next
                                    Else
                                        Logger.Dbg("WDM not found: '" & lCacheFilename & "'")
                                    End If
                                End If
                            End Using
                        Next
                        If FileExists(aMetWDM) Then
                            Dim lNewResult As String = "<add_data type='Timeseries::WDM'>" & aMetWDM & "</add_data>" & vbCrLf
                            If Not lResults.Contains(lNewResult) Then lResults &= lNewResult
                            aProject.TimeseriesSources.Add(lDestinationWDM)
                        End If
                        'Used to clear to minimize memory use, but now we are saving the data in aProject
                        'lDestinationWDM.Clear()
                    End If
                End If
                Logger.Progress("", 1, 1)
            Else
                lResults &= "<message>BASINS Meterologic: No stations to get data from</message>"
            End If
        End If
        Return lResults
    End Function

    ''' <summary>
    ''' Retrieve and self-extract STATSGO data for each state in the region
    ''' </summary>
    ''' <param name="aProject">project to add data to</param>
    ''' <returns>XML string describing success</returns>
    ''' <remarks>First checks for states in BASINS layer st.shp in project folder. 
    ''' If not found, looks for national st.shp and computes states overlapping project region.\
    ''' TODO: use Region to determine states</remarks>
    Public Shared Function GetBASINSSTATSGO(ByVal aProject As D4EM.Data.Project) As String
        Dim lCacheFilename As String = ""
        Dim lCacheMetadataFilename As String
        Dim lDataTypeString As String = "STATSGO"
        Dim lGetData As Boolean = True

        GetBASINSSTATSGO = ""

        Dim lCacheFolder As String = IO.Path.Combine(aProject.CacheFolder, "clsBASINS" & g_PathChar & lDataTypeString & g_PathChar)
        IO.Directory.CreateDirectory(lCacheFolder)

        Dim lStatesOverlapping As New Generic.List(Of String) '2-letter abbreviations for the states overlapping our region

        'Cheat by checking for state layer in the project
        Dim lStateFilename As String = IO.Path.Combine(aProject.ProjectFolder, "st.dbf")
        If IO.File.Exists(lStateFilename) Then
            Dim lDBF As New atcTableDBF
            If lDBF.OpenFile(lStateFilename) Then
                Dim lStateField As Integer = lDBF.FieldNumber("ST")
                If lStateField > 0 Then
                    For lRecord As Integer = 1 To lDBF.NumRecords
                        lDBF.CurrentRecord = lRecord
                        lStatesOverlapping.Add(lDBF.Value(lStateField))
                    Next
                End If
            End If
        End If

        If lStatesOverlapping.Count = 0 Then
            lStateFilename = National.ShapeFilename(National.LayerSpecifications.state) ' FindFile("Locate State Shape File", g_PathChar & "basins\data\national\st.shp")
            If IO.File.Exists(lStateFilename) Then
                lStatesOverlapping = aProject.Region.GetKeysOfOverlappingShapes(New D4EM.Data.Layer(lStateFilename, National.LayerSpecifications.state))
            End If
        End If

        For Each lState As String In lStatesOverlapping
            Dim lFilenameonly As String = lState.ToLower & "_stsgo.exe"
            lCacheFilename = IO.Path.Combine(aProject.CacheFolder, lFilenameonly)
            lCacheMetadataFilename = lCacheFilename & ".xml"

            If aProject.GetEvenIfCached Then
                TryDelete(lCacheFilename)
                TryDelete(lCacheMetadataFilename)
            End If

            If FileExists(lCacheFilename) Then
                Logger.Dbg("Using cached '" & lCacheFilename & "'")
            Else
                D4EM.Data.Download.DownloadURL("http://www.epa.gov/waterscience/ftp/basins/statsgo/" & lFilenameonly, lCacheFilename)
            End If

            If FileExists(lCacheFilename) Then 'extract compressed download
                Zipper.UnzipFile(lCacheFilename, aProject.ProjectFolder)
                TryCopy(lCacheMetadataFilename, IO.Path.Combine(aProject.ProjectFolder, lFilenameonly & ".xml"))
                GetBASINSSTATSGO &= "<message>STATSGO: Unpacked data from " & lState & " into " & aProject.ProjectFolder & "</message>"
            Else
                Logger.Dbg("No zip file found after download at '" & lCacheFilename & "'")
            End If
        Next
    End Function

    ''' <summary>
    ''' Search through aAllStations to find ones with IDs in aStationIDs.
    ''' Add the records of matching stations to aStationsInRegion, if they are not already in aStationsInRegion.
    ''' </summary>
    ''' <param name="aAllStations">The set of all available stations with DBF records</param>
    ''' <param name="aKeyField">Field index to search in aAllStations</param>
    ''' <param name="aStationIDs">IDs to search for in aKeyField of aAllStations</param>
    ''' <param name="aStationsInRegion">DBF records from aAllStations are added to this for stations whose IDs are in aStationIDs</param>
    ''' <remarks></remarks>
    Private Shared Sub GetStationsInList(ByVal aAllStations As atcTableDBF, _
                                         ByVal aKeyField As Integer, _
                                         ByVal aStationIDs As Generic.List(Of String), _
                                         ByRef aStationsInRegion As atcTableDBF)
        With aAllStations
            Dim lKey As String
            Dim lInside As Boolean = False
            Dim lLastKey As String = ""
            Dim lLastInside As Boolean = False
            Dim lNumOriginalRecords As Integer = 0
            Dim lFieldNums() As Integer
            Dim lOperations() As String
            Dim lValues() As String : ReDim lValues(1)

            ReDim lFieldNums(1)
            ReDim lOperations(1)

            If aStationsInRegion Is Nothing Then
                aStationsInRegion = .Cousin
                aStationsInRegion.InitData()
            Else
                lNumOriginalRecords = aStationsInRegion.NumRecords
                lFieldNums(0) = aKeyField
                lFieldNums(1) = aStationsInRegion.FieldNumber("CONSTITUENT")
                lOperations(0) = "="
                lOperations(1) = "="
            End If
            For lCurrentRecord As Integer = 1 To .NumRecords
                .CurrentRecord = lCurrentRecord
                lKey = .Value(aKeyField)

                If aStationIDs.Contains(lKey) Then
                    Dim lIsNew As Boolean = False
                    If lNumOriginalRecords = 0 Then
                        lIsNew = True
                    Else
                        lValues(0) = lKey
                        lValues(1) = .Value(lFieldNums(1))
                        lIsNew = Not aStationsInRegion.FindMatch(lFieldNums, lOperations, lValues, False, 1, lNumOriginalRecords)
                    End If
                    If lIsNew Then
                        aStationsInRegion.CurrentRecord = aStationsInRegion.NumRecords + 1
                        aStationsInRegion.RawRecord = .RawRecord
                    End If
                End If
            Next
        End With
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="aAllStations">Table of all stations to search through</param>
    ''' <param name="aRegion">Region to find stations close to</param>
    ''' <param name="aMethod">If "closest" only find the closest, 
    ''' otherwise include all in bounding box of aRegion</param>
    ''' <param name="aKeyField">Field index of met station key field. In BASINS, field is labeled "LOCATION"</param>
    ''' <param name="aLatitudeField">Field index of LATITUDE in aAllStations</param>
    ''' <param name="aLongitudeField">Field index of LONGITUDE in aAllStations</param>
    ''' <param name="aStartDateField">Field index of STARTDATE in aAllStations</param>
    ''' <param name="aEndDateField">Field index of ENDDATE in aAllStations</param>
    ''' <param name="aMaxStartDate">Latest acceptable starting date for all aRequiredConstituents</param>
    ''' <param name="aMinEndDate">Earliest acceptable ending date for all aRequiredConstituents</param>
    ''' <param name="aRequiredConstituents">Array of constituent names.
    ''' Any station not containing all of these will be skipped. 
    ''' Expected names are: "PREC", "ATEM", "SOLR", "WIND", "DEWP", "PEVT"
    ''' MinRequiredConstituents and MaxRequiredConstituents can be customized and used for aRequiredConstituents</param>
    ''' <param name="aStationsInRegion">Table that will be updated to include 
    ''' records copied from aAllStations for the station(s) found</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function GetStationsInRegion(ByVal aAllStations As atcTableDBF, _
                                                ByVal aRegion As Region, _
                                                ByVal aMethod As String, _
                                                ByVal aKeyField As Integer, _
                                                ByVal aLatitudeField As Integer, _
                                                ByVal aLongitudeField As Integer, _
                                                ByVal aStartDateField As Integer, _
                                                ByVal aEndDateField As Integer, _
                                                ByVal aMaxStartDate As Date, _
                                                ByVal aMinEndDate As Date, _
                                                ByVal aRequiredConstituents() As String, _
                                                ByRef aStationsInRegion As atcTableDBF) As String
        Dim lNorth As Double, lSouth As Double, lEast As Double, lWest As Double
        aRegion.GetBounds(lNorth, lSouth, lWest, lEast, pNativeProjection)
        With aAllStations
            Dim lLongitude As Double
            Dim lLatitude As Double
            Dim lKey As String = ""
            Dim lInside As Boolean = False
            Dim lLastKey As String = ""
            Dim lLastInside As Boolean = False
            Dim lNumOriginalRecords As Integer = 0
            Dim lFieldNums() As Integer
            Dim lOperations() As String
            Dim lValues() As String : ReDim lValues(1)

            ReDim lFieldNums(1)
            ReDim lOperations(1)

            If aStationsInRegion Is Nothing Then
                aStationsInRegion = .Cousin
                aStationsInRegion.InitData()
            Else
                lNumOriginalRecords = aStationsInRegion.NumRecords
                lFieldNums(0) = aKeyField
                lFieldNums(1) = aStationsInRegion.FieldNumber("CONSTITUENT")
                lOperations(0) = "="
                lOperations(1) = "="
            End If
            If aMethod = "closest" Then 'Find closest station with basic constituents
                Dim lConstituentField As Integer = aAllStations.FieldNumber("CONSTITUEN")
                Dim lClosestRecord As Integer = 0
                Dim lClosestDistance As Double = GetMaxValue()
                Dim lCenterLat As Double = (lNorth + lSouth) / 2
                Dim lCenterLon As Double = (lEast + lWest) / 2
                Try
                    Dim lCentroid As DotSpatial.Topology.Point = aRegion.ToShape(D4EM.Data.Globals.GeographicProjection).ToGeometry.Centroid
                    If lCentroid.X > lWest AndAlso lCentroid.X < lEast AndAlso lCentroid.Y > lSouth AndAlso lCentroid.Y < lNorth Then
                        lCenterLat = lCentroid.Y
                        lCenterLon = lCentroid.X
                    End If
                Catch
                End Try
                Dim lHasRequiredField(aRequiredConstituents.GetUpperBound(0)) As Boolean
                For lCurrentRecord As Integer = 1 To .NumRecords + 1
                    If lCurrentRecord <= .NumRecords Then
                        .CurrentRecord = lCurrentRecord
                        lKey = .Value(aKeyField)
                    Else
                        lKey = "PastLastStation"
                    End If
                    If Not lKey.Equals(lLastKey) Then 'We have reached the end of records about one station
                        For Each lRequiredField As Boolean In lHasRequiredField
                            If lRequiredField = False Then GoTo MissingRequirement
                        Next
                        .CurrentRecord = lCurrentRecord - 1

                        Dim lDistance = (.Value(aLatitudeField) - lCenterLat) ^ 2 + (.Value(aLongitudeField) - lCenterLon) ^ 2
                        If lDistance < lClosestDistance Then 'This station is the closest so far
                            lClosestDistance = lDistance
                            lClosestRecord = .CurrentRecord
                        End If
MissingRequirement:
                        ReDim lHasRequiredField(aRequiredConstituents.GetUpperBound(0)) 'Set all back to False
                        If lCurrentRecord <= .NumRecords Then .CurrentRecord = lCurrentRecord
                        lLastKey = lKey
                    End If

                    Dim lRequiredIndex As Integer = Array.IndexOf(aRequiredConstituents, .Value(lConstituentField))
                    If lRequiredIndex >= 0 Then 'Check whether period of record of this required constituent covers required dates
                        If ConstituentDatesCover(aAllStations, aMaxStartDate, aMinEndDate, aStartDateField, aEndDateField) Then
                            lHasRequiredField(lRequiredIndex) = True
                        End If
                    End If
                Next
                If lClosestRecord > 0 Then 'Found a closest station
                    .CurrentRecord = lClosestRecord
                    lKey = .Value(aKeyField)

                    If aStationsInRegion.FindFirst(aKeyField, lKey) Then
                        Logger.Dbg("Met station already added: " & lKey)
                    Else
                        Logger.Dbg("Adding met station: " & lKey)
                        While .CurrentRecord > 1 AndAlso .Value(aKeyField) = lKey
                            .CurrentRecord -= 1 'Find first constituent from this station
                        End While
                        If .Value(aKeyField) <> lKey Then
                            .CurrentRecord += 1
                        End If
                        While .CurrentRecord <= lClosestRecord 'Copy record for each constituent
                            aStationsInRegion.CurrentRecord = aStationsInRegion.NumRecords + 1
                            aStationsInRegion.RawRecord = .RawRecord
                            .CurrentRecord += 1
                        End While
                    End If
                End If
            Else 'Find all stations in region bounding box
                For lCurrentRecord As Integer = 1 To .NumRecords
                    .CurrentRecord = lCurrentRecord
                    lKey = .Value(aKeyField)
                    If lKey.Equals(lLastKey) Then
                        lInside = lLastInside
                    Else
                        lInside = False
                        lLongitude = .Value(aLongitudeField)
                        If lLongitude >= lWest AndAlso lLongitude <= lEast Then
                            lLatitude = .Value(aLatitudeField)
                            If lLatitude >= lSouth AndAlso lLatitude <= lNorth Then
                                lInside = True
                            End If
                        End If
                        lLastKey = lKey
                        lLastInside = lInside
                    End If

                    If lInside Then
                        Dim lIsNew As Boolean = False
                        If lNumOriginalRecords = 0 Then
                            lIsNew = True
                        Else
                            lValues(0) = lKey
                            lValues(1) = .Value(lFieldNums(1))
                            lIsNew = Not aStationsInRegion.FindMatch(lFieldNums, lOperations, lValues, False, 1, lNumOriginalRecords)
                        End If
                        If lIsNew Then
                            aStationsInRegion.CurrentRecord = aStationsInRegion.NumRecords + 1
                            aStationsInRegion.RawRecord = .RawRecord
                        End If
                    End If
                Next
            End If
            Return lKey
        End With
    End Function

    Private Shared Function ConstituentDatesCover(ByVal aAllStations As atcTableDBF, _
                                                  ByVal aMaxStartDate As Date, ByVal aMinEndDate As Date, _
                                                  ByVal aStartDateField As Integer, aEndDateField As Integer) As Boolean
        If aStartDateField > 0 AndAlso aMaxStartDate.Year > 1000 Then
            Try 'If date cannot be parsed, skip this test
                If aAllStations.ValueAsDate(aStartDateField) > aMaxStartDate Then
                    Return False
                End If
            Catch
            End Try
        End If

        If aEndDateField > 0 AndAlso aMinEndDate.Year > 1000 Then
            Try 'If date cannot be parsed, skip this test
                If aAllStations.ValueAsDate(aEndDateField) < aMinEndDate Then
                    Return False
                End If
            Catch
            End Try
        End If

        Return True
    End Function

    Private Shared Function BaseDataType(ByVal aDataType As LayerSpecification) As LayerSpecification
        Dim lBaseDataType = aDataType
        Select Case aDataType
            Case LayerSpecifications.core31.acc,
                 LayerSpecifications.core31.all,
                 LayerSpecifications.core31.bac_stat,
                 LayerSpecifications.core31.cat,
                 LayerSpecifications.core31.catpt,
                 LayerSpecifications.core31.cnty,
                 LayerSpecifications.core31.cntypt,
                 LayerSpecifications.core31.ecoreg,
                 LayerSpecifications.core31.epa_reg,
                 LayerSpecifications.core31.fhards,
                 LayerSpecifications.core31.lulcndx,
                 LayerSpecifications.core31.mad,
                 LayerSpecifications.core31.nawqa,
                 LayerSpecifications.core31.pcs3,
                 LayerSpecifications.core31.rf1,
                 LayerSpecifications.core31.st,
                 LayerSpecifications.core31.statsgo,
                 LayerSpecifications.core31.urban,
                 LayerSpecifications.core31.urban_nm
                lBaseDataType = LayerSpecifications.core31.all
            Case LayerSpecifications.dem,
                 LayerSpecifications.DEMG,
                 LayerSpecifications.giras,
                 LayerSpecifications.huc12,
                 LayerSpecifications.lstoret,
                 LayerSpecifications.MetStation,
                 LayerSpecifications.NED,
                 LayerSpecifications.nhd,
                 LayerSpecifications.pcs3
                lBaseDataType = aDataType
            Case LayerSpecifications.Census.all,
                LayerSpecifications.Census.county1990,
                LayerSpecifications.Census.county2000,
                LayerSpecifications.Census.landmark2002,
                LayerSpecifications.Census.place1990,
                LayerSpecifications.Census.place2000,
                LayerSpecifications.Census.railroad2002,
                LayerSpecifications.Census.road2002,
                LayerSpecifications.Census.tract1990,
                LayerSpecifications.Census.tract2000,
                LayerSpecifications.Census.zipcode2000
                lBaseDataType = LayerSpecifications.Census.all
            Case Else
                Logger.Dbg("BaseDataType: unknown layer specification " & aDataType.ToString)
        End Select
        Return lBaseDataType
    End Function

    ''' <summary>
    ''' Get BASINS data
    ''' </summary>
    ''' <param name="aProject">project to add data to</param>
    ''' <param name="aSaveFolder">Sub-folder within project folder (e.g. "BASINSData") or full path of folder to save in (e.g. "C:\BASINSData").
    '''  If nothing or empty string, will save in aProject.ProjectFolder (plus data type subfolder for most types)</param>
    ''' <param name="aHUC8">8-digit hydrologic unit code to download</param>
    ''' <param name="aDataType">Type of data to download. Valid values are in BASINS.LayerSpecifications.</param>
    ''' <returns>XML describing success or error</returns>
    Public Shared Function GetBASINS(ByVal aProject As D4EM.Data.Project,
                                     ByVal aSaveFolder As String,
                                     ByVal aHUC8 As String,
                                     ByVal aDataType As LayerSpecification) As String

        Dim lBaseDataType As LayerSpecification = BaseDataType(aDataType)
        Dim lCacheFilename As String = ""
        Dim lFileNameOnly As String = aHUC8 & "_" & lBaseDataType.Tag
        Select Case lBaseDataType
            Case LayerSpecifications.DEMG, LayerSpecifications.NED : lFileNameOnly &= "gtif"
        End Select
        If lBaseDataType = LayerSpecifications.huc12 Then
            lFileNameOnly &= ".zip"
        Else
            lFileNameOnly &= ".exe"
        End If

        Dim lCacheFolder As String = IO.Path.Combine(aProject.CacheFolder, "clsBASINS" & g_PathChar)
        IO.Directory.CreateDirectory(lCacheFolder)

        lCacheFilename = IO.Path.Combine(lCacheFolder, lFileNameOnly)
        Dim lCacheMetadataFilename As String = IO.Path.Combine(lCacheFolder, "metadata_" & lBaseDataType.Tag & ".exe")
        If aProject.GetEvenIfCached Then
            TryDelete(lCacheFilename)
        End If
Retry:
        If Not FileExists(lCacheFilename) Then
            Dim lURL As String = pBaseURL & lFileNameOnly
            If lBaseDataType = LayerSpecifications.huc12 Then
                'hspf.com is down for good, huc12 boundaries now found on epa ftp server
                'lURL = "http://hspf.com/cgi-bin/finddata.pl?url=" & lURL
                lURL = "ftp://newftp.epa.gov/exposure/NHDV1/HUC12_Boundries/" & aHUC8 & ".zip"
            End If
            If Not D4EM.Data.Download.DownloadURL(lURL, lCacheFilename) Then
                lURL = pBaseURLga & aHUC8 & "/" & lFileNameOnly
                If Not D4EM.Data.Download.DownloadURL(lURL, lCacheFilename) Then
                    Throw New ApplicationException("Could not download BASINS " & lBaseDataType.Tag & " data for " & aHUC8 & " from " & vbCrLf & lURL & vbCrLf & " to " & lCacheFilename)
                End If
            End If
        End If

        Dim lTempFolder As String = NewTempDir(IO.Path.Combine(lCacheFolder, "D4EM_unpack_BASINS"))
        Dim lUnpackFolder As String = IO.Path.Combine(lTempFolder, lBaseDataType.Tag & g_PathChar)

        'Move this until after full path is build
        'Unpack into temporary folder
        'TryDelete(lUnpackFolder)
        'MkDirPath(lUnpackFolder)
        'Zipper.UnzipFile(lCacheFilename, lUnpackFolder)

        Dim lNativeProjection As DotSpatial.Projections.ProjectionInfo = pNativeProjection

        Dim lSaveIn As String = aProject.ProjectFolder

        Select Case lBaseDataType
            Case LayerSpecifications.core31.all
                UnpackToTempFolder(lUnpackFolder, lCacheFilename)
                lUnpackFolder &= aHUC8 & g_PathChar

                'Remove some obsolete layers
                TryDeleteShapefile(lUnpackFolder & "gage.shp")     'replaced by NWIS data download 
                TryDeleteShapefile(lUnpackFolder & "wdm.shp")      'replaced by BASINS met data download
                TryDeleteShapefile(lUnpackFolder & "met_stat.shp") 'replaced by BASINS met data download
                TryDeleteShapefile(lUnpackFolder & "metpt.shp")    'replaced by BASINS met data download
                TryDeleteShapefile(lUnpackFolder & "wq_stat.shp")  'obsolete water quality station layer
                TryDeleteShapefile(lUnpackFolder & "wqobs.shp")    'obsolete water quality layer
                Dim lFilename As String
                For Each lFilename In IO.Directory.GetFiles(lUnpackFolder, "wq_*.dbf")
                    TryDelete(lFilename)                           'obsolete water quality
                Next
                TryDelete(lUnpackFolder & "wqobs")                 'obsolete water quality
            Case LayerSpecifications.huc12
                lUnpackFolder &= aHUC8 & g_PathChar
                UnpackToTempFolder(lUnpackFolder, lCacheFilename)

                lNativeProjection = Globals.WebMercatorProjection
            Case LayerSpecifications.giras
                UnpackToTempFolder(lUnpackFolder, lCacheFilename)
                lUnpackFolder &= aHUC8 & g_PathChar & "landuse"

                lSaveIn = IO.Path.Combine(lSaveIn, "landuse")
            Case LayerSpecifications.Census.all
                UnpackToTempFolder(lUnpackFolder, lCacheFilename)
                lSaveIn = IO.Path.Combine(lSaveIn, "census")
            Case LayerSpecifications.dem, LayerSpecifications.DEMG
                UnpackToTempFolder(lUnpackFolder, lCacheFilename)
                lSaveIn = IO.Path.Combine(lSaveIn, "dem")
            Case LayerSpecifications.nhd
                UnpackToTempFolder(lUnpackFolder, lCacheFilename)
                lSaveIn = IO.Path.Combine(lSaveIn, "nhd")
            Case LayerSpecifications.lstoret
                UnpackToTempFolder(lUnpackFolder, lCacheFilename)
                lSaveIn = IO.Path.Combine(lSaveIn, "STORET-Legacy")
            Case LayerSpecifications.NED
                UnpackToTempFolder(lUnpackFolder, lCacheFilename)
                lSaveIn = IO.Path.Combine(lSaveIn, "ned")
            Case LayerSpecifications.pcs3
                UnpackToTempFolder(lUnpackFolder, lCacheFilename)
                lSaveIn = IO.Path.Combine(lSaveIn, "pcs3")
                'Case LayerSpecifications.d303
                '    lSaveIn = IO.Path.Combine(lSaveIn, "303d")
        End Select

        'Use aDataType to filter the results in cases where lBaseDataType is one of the kinds that includes more layers than the given aDataType
        '      (i.e. LayerSpecifications.core31.all or LayerSpecifications.Census.all)
        If lBaseDataType <> aDataType Then
            If Not String.IsNullOrEmpty(aDataType.FilePattern) Then
                Dim lBaseFilename As String = IO.Path.GetFileNameWithoutExtension(aDataType.FilePattern.ToLower.Replace("*", ""))
                For Each lUnpackedFileName As String In IO.Directory.GetFiles(lUnpackFolder)
                    If Not IO.Path.GetFileNameWithoutExtension(lUnpackedFileName.ToLower).EndsWith(lBaseFilename) Then
                        TryDelete(lUnpackedFileName)
                    End If
                Next
            End If
        End If

        'aSaveFolder overrides our default subfolders assigned above
        If Not String.IsNullOrEmpty(aSaveFolder) Then lSaveIn = IO.Path.Combine(aProject.ProjectFolder, aSaveFolder)
        If Not lSaveIn.EndsWith(g_PathChar) Then lSaveIn &= g_PathChar

        MkDirPath(PathNameOnly(lSaveIn))

        If IO.File.Exists(lCacheMetadataFilename) Then
            Zipper.UnzipFile(lCacheMetadataFilename, lUnpackFolder)
        End If

        If lBaseDataType = LayerSpecifications.lstoret Then
            CreateLegacySTORETshape(lUnpackFolder)
        End If

        Dim lPrjFileContents As String = lNativeProjection.ToEsriString
        For Each lShapeFilename As String In IO.Directory.GetFiles(lUnpackFolder, "*.shp") 'TODO:  IO.SearchOption.AllDirectories)
            Layer.CopyProcStepsFromCachedFile(lCacheFilename, lShapeFilename)
            Dim lProjectionFilename As String = IO.Path.ChangeExtension(lShapeFilename, ".prj")
            If Not IO.File.Exists(lProjectionFilename) Then
                IO.File.WriteAllText(lProjectionFilename, lPrjFileContents)
            End If
        Next

        Dim lLayers As New Generic.List(Of Layer)
        If aProject.Clip Then
            lLayers.AddRange(SpatialOperations.ProjectAndClipShapeLayers(lUnpackFolder, lNativeProjection, aProject.DesiredProjection, aProject.Region, GetType(BASINS.LayerSpecifications)))
            SpatialOperations.ProjectAndClipGridLayers(lUnpackFolder, lNativeProjection, aProject.DesiredProjection, aProject.Region)
        Else
            lLayers.AddRange(SpatialOperations.ProjectAndClipShapeLayers(lUnpackFolder, lNativeProjection, aProject.DesiredProjection, Nothing, GetType(BASINS.LayerSpecifications)))
            SpatialOperations.ProjectAndClipGridLayers(lUnpackFolder, lNativeProjection, aProject.DesiredProjection, Nothing)
        End If

        If aProject.Merge Then
            'TODO: merge layers with HUC number in folder or file name, MergeLayers below takes care of layers that do not have HUC in name
        End If

        'Move files into place
        Dim AllFilesToMove() As String = IO.Directory.GetFiles(lUnpackFolder, "*", IO.SearchOption.AllDirectories)
        For Each lFileName As String In AllFilesToMove
            Dim lFound As Boolean = False
            For Each lLayer As Layer In lLayers
                If lLayer.FileName.ToLower.Equals(lFileName.ToLower) Then lFound = True : Exit For
            Next
            If Not lFound Then
                Dim lLayerSpecfication As LayerSpecification = LayerSpecification.FromFilename(lFileName, GetType(BASINS.LayerSpecifications))
                If lLayerSpecfication IsNot Nothing Then
                    lLayers.Add(New Layer(lFileName, lLayerSpecfication, False))
                Else
                    Select Case IO.Path.GetExtension(lFileName).ToLower
                        Case ".shp", ".tif"
                            If lFileName.Contains("census") Then
                                'Skip adding the obsure census layers with no specification
                                Logger.Dbg("No layer specification found for " & lFileName.Substring(lTempFolder.Length))
                            Else
                                Logger.Msg("No layer specification found for " & lFileName.Substring(lTempFolder.Length))
                            End If
                    End Select
                End If
            End If
        Next

        Dim lReturn As String = SpatialOperations.MergeLayers(lLayers, lUnpackFolder, lSaveIn)
        For Each lLayer As Layer In lLayers
            'Add layers that are not already in aProject. (If we touch more than one HUC-8, some layers will be merged so they do not have to be added again.) 
            If aProject.LayerFromFileName(lLayer.FileName) Is Nothing Then
                aProject.Layers.Add(lLayer)
            End If
        Next
        Logger.Status("Moving non-layer BASINS files") 'and layers that are not found by LayerSpecification.FromFilename

        For Each lFileName As String In AllFilesToMove
            If IO.File.Exists(lFileName) Then 'Only files not already moved above
                'Select Case IO.Path.GetExtension(lFileName).ToLower
                '    Case ".shp", ".shx", ".spx", ".sbn", ".tif"
                '        'These should already be gone after move/merge above, try again to delete if they are still present because they could not be deleted above
                '        TryDelete(lFileName)
                '    Case Else
                Dim lFromFolderLength As Integer = lUnpackFolder.TrimEnd(g_PathChar).Length + 1
                Dim lDestinationFilename As String = IO.Path.Combine(lSaveIn, lFileName.Substring(lFromFolderLength))
                SpatialOperations.CopyMoveMergeFile(lFileName, lDestinationFilename, Nothing)
                'End Select
            End If
        Next
        Logger.Status("Removing temporary folder " & lTempFolder, True)

        TryDelete(lTempFolder)
        Logger.Status("", False)
        Return lReturn
    End Function

    'TODO: remove this version of GetBASINS, replace its use with creating a project and calling other GetBASINS
    Public Shared Function GetBASINS(ByVal aCacheFolder As String,
                                     ByVal aProjectFolder As String,
                                     ByVal aSaveFolder As String,
                                     ByVal aHUC8 As String,
                                     ByVal aDataType As LayerSpecification,
                                     ByVal aRegion As Region,
                                     ByVal aDesiredProjection As DotSpatial.Projections.ProjectionInfo,
                                     ByVal aClip As Boolean) As String
        Return GetBASINS(New Project(aDesiredProjection, aCacheFolder, aProjectFolder, aRegion, aClip, False), aSaveFolder, aHUC8, aDataType)
    End Function

    Private Shared Sub CreateLegacySTORETshape(ByVal aFolder As String)
        Dim lNativeStationFiles As New Collections.Specialized.NameValueCollection
        atcUtility.AddFilesInDir(lNativeStationFiles, aFolder, True, "*.sta")
        For Each lLayerFilename As String In lNativeStationFiles
            Dim lNativeStations As New atcTableDelimited
            lNativeStations.Delimiter = vbTab
            If lNativeStations.OpenFile(lLayerFilename) Then
                Dim lLongField As Integer = lNativeStations.FieldNumber("Longitude")
                Dim lLatField As Integer = lNativeStations.FieldNumber("Latitude")
                If lLongField < 1 Then
                    Throw New ApplicationException("Legacy STORET table missing Longitude field, cannot create shape file from " & lLayerFilename)
                ElseIf lLatField < 1 Then
                    Throw New ApplicationException("Legacy STORET table missing Latitude field, cannot create shape file from " & lLayerFilename)
                Else
                    Dim lShapeFilename As String = lLayerFilename.Substring(0, lLayerFilename.Length - 4) & "_lstoret.shp"

                    'Dim lNewFeatureSet As New DotSpatial.Data.FeatureSet(DotSpatial.Topology.FeatureType.Point)
                    'lNewFeatureSet.Projection = New DotSpatial.Projections.ProjectionInfo(Globals.GeographicProjection)
                    Dim lNewShapefile As New DotSpatial.Data.PointShapefile
                    lNewShapefile.Projection = Globals.GeographicProjection
                    Dim lNativeStationsFieldLengths() As Integer = lNativeStations.ComputeFieldLengths()
                    For lCurField As Integer = 1 To lNativeStations.NumFields
                        Dim lNewField As New DotSpatial.Data.Field(lNativeStations.FieldName(lCurField).Replace("discharge", "q"), DotSpatial.Data.FieldDataType.String)
                        lNewField.Length = Math.Min(lNativeStationsFieldLengths(lCurField), 255)
                        lNewShapefile.DataTable.Columns.Add(lNewField)
                        'lNewFeatureSet.DataTable.Columns.Add(lNewField)
                    Next

                    Dim lShapeIndex As Integer = -1
                    Dim lLastStation As Integer = lNativeStations.NumRecords
                    Logger.Dbg("Importing " & Format(lLastStation, "#,###") & " legacy STORET stations into shapes from " & IO.Path.GetFileNameWithoutExtension(lLayerFilename))

                    For lStationIndex As Integer = 1 To lLastStation
                        lNativeStations.CurrentRecord = lStationIndex
                        Dim lX As Double
                        Dim lY As Double
                        If Double.TryParse(lNativeStations.Value(lLongField), lX) AndAlso Double.TryParse(lNativeStations.Value(lLatField), lY) Then
                            Dim lCoordinate As New DotSpatial.Topology.Coordinate(lX, lY)
                            Dim lPoint As New DotSpatial.Topology.Point(lCoordinate)
                            Dim lFeature As DotSpatial.Data.IFeature = lNewShapefile.AddFeature(lPoint)
                            'Dim lFeature As DotSpatial.Data.IFeature = lNewFeatureSet.AddFeature(lPoint)
                            For lCurField As Integer = 1 To lNativeStations.NumFields
                                lFeature.DataRow(lCurField - 1) = SafeSubstring(lNativeStations.Value(lCurField), 0, 255)
                            Next
                        Else
                            Logger.Dbg("Skipping station with bad lat/lon: " & lNativeStations.CurrentRecordAsDelimitedString)
                        End If
                        Logger.Progress(lStationIndex, lLastStation)
                    Next

                    'lNewFeatureSet.SaveAs(lShapeFilename, True)
                    lNewShapefile.UpdateExtent()
                    lNewShapefile.SaveAs(lShapeFilename, True)
                End If
            Else
                Logger.Dbg("Unable to open legacy STORET station file " & lLayerFilename)
            End If
        Next
    End Sub

    Private Sub OnDownloadProgess(ByVal bytesRead As Integer, ByVal totalBytes As Integer)
        Logger.Progress("Downloading", bytesRead, totalBytes)
    End Sub

    Private Shared Sub UnpackToTempFolder(ByVal lUnpackFolder As String, ByVal lCacheFilename As String)

        'Unpack into temporary folder
        TryDelete(lUnpackFolder)
        MkDirPath(lUnpackFolder)
        Zipper.UnzipFile(lCacheFilename, lUnpackFolder)
    End Sub

End Class
