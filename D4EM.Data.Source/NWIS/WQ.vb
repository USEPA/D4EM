Imports atcUtility
Imports MapWinUtility
Imports D4EM.Geo

Partial Public Class NWIS

    ''' <summary>
    ''' Get water quality values for the given stations
    ''' </summary>
    ''' <param name="aProject">Project folder and cache settings. Region will be used if stations file is not found.</param>
    ''' <param name="aSaveFolder">Sub-folder within project folder or full path of folder to save in (if nothing or empty string, will save in aProject.ProjectFolder)</param>
    ''' <param name="aStationIDs">List of stations to get data from</param>
    ''' <param name="aStartDate">YYYY-MM-DD</param>
    ''' <param name="aEndDate">YYYY-MM-DD</param>
    ''' <returns>XML describing success or errors</returns>
    Public Shared Function GetWQ(ByVal aProject As Project,
                                 ByVal aSaveFolder As String,
                                 ByVal aStationIDs As Generic.List(Of String),
                        Optional ByVal aStartDate As String = "1880-01-01",
                        Optional ByVal aEndDate As String = "2100-01-01") As String

        Dim lSaveIn As String = aProject.ProjectFolder
        If aSaveFolder IsNot Nothing AndAlso aSaveFolder.Length > 0 Then lSaveIn = IO.Path.Combine(lSaveIn, aSaveFolder)
        Dim lResults As String = ""
        Dim lNumNotDownloaded As Integer = 0
        Dim lStationIndex As Integer = 1
        Dim lDataType As String = "qw"

        IO.Directory.CreateDirectory(lSaveIn)
        Dim lStationFilename As String = IO.Path.Combine(lSaveIn, pDefaultStationsBaseFilename) & "_" & lDataType & ".rdb"
        If Not IO.File.Exists(lStationFilename) Then
            GetStationsInRegion(aProject.Region, lStationFilename, NWIS.LayerSpecifications.WaterQuality)
        End If

        If aStationIDs.Count = 0 Then
            lResults &= "<message>NWIS Water Quality: Stations must be selected before NWIS Water Quality retrieval</message>"
            lResults &= "<select_layer>NWIS Water Quality Stations</select_layer>" & vbCrLf
        Else
            Dim lStationsRDB As New atcTableRDB
            With lStationsRDB
                If .OpenFile(lStationFilename) Then
                    Dim lField_site_no As Integer = .FieldNumber("site_no")
                    If lField_site_no < 1 Then Throw New ApplicationException("Station file '" & lStationFilename & "'  lacks field 'site_no' - probably was not downloaded successfully or download format has changed")
                    Dim lField_state_cd As Integer = .FieldNumber("state_cd")
                    If lField_site_no < 1 Then Throw New ApplicationException("Station file '" & lStationFilename & "'  lacks field 'state_cd' - probably was not downloaded successfully or download format has changed")

                    Dim lSiteData As String
                    For lStationIndex = 0 To aStationIDs.Count - 1
                        Dim lStationID As String = aStationIDs(lStationIndex)
                        Logger.Progress("Getting NWIS WQ for " & lStationID, lStationIndex, aStationIDs.Count)

                        Dim lBaseFilename As String = "NWIS_wq_" & lStationID & ".rdb"
                        Dim lCacheFilename As String = IO.Path.Combine(IO.Path.Combine(aProject.CacheFolder, "NWIS"), lBaseFilename)
                        Dim lSaveAs As String = IO.Path.Combine(lSaveIn, lBaseFilename)

                        If aProject.GetEvenIfCached AndAlso IO.File.Exists(lSaveAs) Then TryDelete(lSaveAs)
                        If IO.File.Exists(lSaveAs) Then
                            Logger.Dbg("Already have '" & lSaveAs & "'")
                        Else
                            If aProject.GetEvenIfCached AndAlso IO.File.Exists(lCacheFilename) Then TryDelete(lCacheFilename)
                            If IO.File.Exists(lCacheFilename) Then
                                Logger.Dbg("Using cached '" & lCacheFilename & "'")
                            Else
                                Try
                                    If .FindFirst(lField_site_no, lStationID) Then
                                        Dim sURL As String = GetWQURL(lStationID, .Value(lField_state_cd), aStartDate, aEndDate)
                                        lSiteData = DownloadURL(sURL)

                                        If lSiteData.StartsWith("No sites/data found") Then
                                            lNumNotDownloaded += 1
                                            Logger.Dbg("NWIS: No data found for site '" & lStationID & "'")
                                        ElseIf Not lSiteData.StartsWith("#") Then
                                            Select Case Logger.Msg("Data not downloaded for '" & lStationID & "' - View message?", MsgBoxStyle.YesNoCancel)
                                                Case MsgBoxResult.Yes
                                                    Dim lErrorFile As String = GetTemporaryFileName("NWISerror", "html")
                                                    IO.File.WriteAllText(lErrorFile, lSiteData)
                                                    OpenFile(lErrorFile)
                                                Case MsgBoxResult.Cancel
                                                    Return lResults
                                            End Select
                                        Else
                                            IO.Directory.CreateDirectory(IO.Path.GetDirectoryName(lCacheFilename))
                                            IO.File.WriteAllText(lCacheFilename, "# " & sURL & vbCrLf & RecordAsRDBheader(lStationsRDB) & lSiteData)
                                            Logger.Dbg("Downloaded " & Format(lSiteData.Length, "#,###") & " bytes for site " & lStationID)
                                        End If
                                    End If
                                Catch e As Exception
                                    Logger.Msg("Error getting data for NWIS station '" & lStationID & "'" & vbCrLf & e.Message, "NWIS WQ")
                                End Try
                            End If
                            If IO.File.Exists(lCacheFilename) Then
                                IO.File.Copy(lCacheFilename, lSaveAs)
                                lResults &= "<add_data type='USGS RDB' subtype='water quality'>" & lSaveAs & "</add_data>" & vbCrLf
                                'TODO: aProject.TimeseriesSources.Add(RDB file)
                            End If
                        End If
                    Next
                End If
            End With
        End If
        Logger.Progress("", 0, 0)
        If lNumNotDownloaded > 0 Then
            lResults &= "<message>NWIS " & lDataType & ": " & lNumNotDownloaded & " of " & aStationIDs.Count & " stations did not have data available</message>"
        End If
        Return lResults
    End Function

    Private Shared Function GetWQURL(ByVal aStationNumber As String, _
                              ByVal aStateFIPS As String, _
                              ByVal aStartDate As String, _
                              ByVal aEndDate As String) As String
        Dim lDatePortion As String = "&begin_date="
        If Not aStartDate Is Nothing AndAlso aStartDate.Length > 0 Then
            lDatePortion &= Format(CDate(aStartDate), "yyyy-MM-dd")
        End If
        lDatePortion &= "&end_date="
        If Not aEndDate Is Nothing AndAlso aEndDate.Length > 0 Then
            lDatePortion &= Format(CDate(aEndDate), "yyyy-MM-dd")
        End If

        'http://nwis.waterdata.usgs.gov/usa/nwis/qwdata?site_no=06892500&agency_cd=USGS&begin_date=&end_date=&format=rdb&date_format=MM/DD/YYYY&submitted_form=brief_list
        Return "http://nwis.waterdata.usgs.gov/" & FIPStoStateAbbrev(aStateFIPS) _
             & "/nwis/qwdata?site_no=" & aStationNumber _
             & "&agency_cd=USGS" & lDatePortion & "&format=rdb&date_format=YYYY-MM-DD&submitted_form=brief_list"

        'Return "http://nwis.waterdata.usgs.gov/" & FIPStoStateAbbrev(aStateFIPS) _
        '     & "/nwis/qwdata?search_site_no=" & aStationNumber _
        '     & "&search_site_no_match_type=exact&sort_key=site_no&group_key=NONE" _
        '     & "&sitefile_output_format=html_table&column_name=agency_cd&column_name=site_no&column_name=station_nm" _
        '     & lDatePortion _
        '     & "&TZoutput=0&qw_attributes=0&inventory_output=0&rdb_inventory_output=file&format=rdb" _
        '     & "&qw_sample_wide=separated_wide&rdb_qw_attributes=0&date_format=YYYY-MM-DD&rdb_compression=value" _
        '     & "&list_of_search_criteria=search_site_no"

        '"&search_site_no_match_type=exact&sort_key=site_no&group_key=NONE" & _
        '"&sitefile_output_format=html_table&column_name=agency_cd&column_name=site_no" & _
        '"&column_name=station_nm&column_name=lat_va&column_name=long_va&column_name=state_cd" & _
        '"&column_name=county_cd&column_name=alt_va&column_name=huc_cd&" & _
        '"begin_date=" & Format(CDate(StartDate), "yyyy-MM-dd") & "&end_date=" & Format(CDate(EndDate), "yyyy-MM-dd") & _
        '"&inventory_output=0&rdb_inventory_output=file&format=rdb&date_format=YYYY-MM-DD" & _
        '"&rdb_compression=value&qw_sample_wide=0&list_of_search_criteria=search_site_no&parameter_cd=" & ParamCode


    End Function

    'Given a USGS station number, a USGS parameter code, a start date, and an end date, 
    '       this method returns an ArrayList with the water quality values.
    Friend Shared Function GetWQValues(ByVal aStationNumber As String, _
                                ByVal aParamCode As String, _
                                ByVal aStartDate As String, _
                                ByVal aEndDate As String) As ArrayList

        GetWQValues = New ArrayList

        Dim stringReader As IO.StreamReader = GetHTTPStreamReader(GetWQURL(aStationNumber, aParamCode, aStartDate, aEndDate))
        Dim line As String = stringReader.ReadLine

        If ((line Is Nothing) OrElse (Not line.StartsWith("#"))) Then
            Return GetWQValues '"No stations found that match request."
        End If

        Do While line.Substring(0, 1) = "#"
            line = stringReader.ReadLine
        Loop

        Dim lResultPos As Integer = line.IndexOf("result_va")
        Dim lTabsToResult As Integer = line.Substring(0, lResultPos).Split(vbTab).Length

        line = stringReader.ReadLine
        line = stringReader.ReadLine

        Dim values() As String
        Dim lValue As String
        Do Until line Is Nothing
            values = line.Split()
            lValue = values(lTabsToResult - 1)
            If IsNumeric(lValue) Then
                GetWQValues.Add(lValue)
            End If
            line = stringReader.ReadLine
        Loop

    End Function

    Private Shared Function FIPStoStateAbbrev(ByVal aFIPS As Integer) As String
        Select Case aFIPS
            Case 1 : Return "al"
            Case 2 : Return "ak"
            Case 4 : Return "az"
            Case 5 : Return "ar"
            Case 6 : Return "ca"
            Case 8 : Return "co"
            Case 9 : Return "ct"
            Case 10 : Return "de"
            Case 11 : Return "dc"
            Case 12 : Return "fl"
            Case 13 : Return "ga"
            Case 15 : Return "hi"
            Case 16 : Return "id"
            Case 17 : Return "il"
            Case 18 : Return "in"
            Case 19 : Return "ia"
            Case 20 : Return "ks"
            Case 21 : Return "ky"
            Case 22 : Return "la"
            Case 23 : Return "me"
            Case 24 : Return "md"
            Case 25 : Return "ma"
            Case 26 : Return "mi"
            Case 27 : Return "mn"
            Case 28 : Return "ms"
            Case 29 : Return "mo"
            Case 30 : Return "mt"
            Case 31 : Return "ne"
            Case 32 : Return "nv"
            Case 33 : Return "nh"
            Case 34 : Return "nj"
            Case 35 : Return "nm"
            Case 36 : Return "ny"
            Case 37 : Return "nc"
            Case 38 : Return "nd"
            Case 39 : Return "oh"
            Case 40 : Return "ok"
            Case 41 : Return "or"
            Case 42 : Return "pa"
            Case 44 : Return "ri"
            Case 45 : Return "sc"
            Case 46 : Return "sd"
            Case 47 : Return "tn"
            Case 48 : Return "tx"
            Case 49 : Return "ut"
            Case 50 : Return "vt"
            Case 51 : Return "va"
            Case 53 : Return "wa"
            Case 54 : Return "wv"
            Case 55 : Return "wi"
            Case 56 : Return "wy"
            Case 72 : Return "pr"
            Case 78 : Return "vi"
            Case Else : Return aFIPS
        End Select
    End Function

End Class
