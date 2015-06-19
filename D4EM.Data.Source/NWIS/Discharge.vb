Imports atcData
Imports atcUtility
Imports MapWinUtility
Imports D4EM.Geo

Partial Class NWIS
    ''' <summary>
    ''' Get daily discharge values for the given stations
    ''' </summary>
    ''' <param name="aProject">Project folder and cache settings. Region will be used if stations file is not found.</param>
    ''' <param name="aSaveFolder">Sub-folder within project folder or full path of folder to save in (if nothing or empty string, will save in aProject.ProjectFolder)</param>
    ''' <param name="aStationIDs">List of stations to get data from</param>
    ''' <param name="aStartDate">YYYY-MM-DD</param>
    ''' <param name="aEndDate">YYYY-MM-DD</param>
    ''' <param name="aWDMFilename">If specified, data will be added to the named file, otherwise data will be saved in native .rdb files</param>
    ''' <returns>XML describing success or errors</returns>
    Public Shared Function GetDailyDischarge(ByVal aProject As Project,
                                             ByVal aSaveFolder As String,
                                             ByVal aStationIDs As Generic.List(Of String),
                                    Optional ByVal aStartDate As String = "1880-01-01",
                                    Optional ByVal aEndDate As String = "2100-01-01",
                                    Optional ByVal aWDMFilename As String = "") As String

        Dim lSaveIn As String = aProject.ProjectFolder
        If aSaveFolder IsNot Nothing AndAlso aSaveFolder.Length > 0 Then lSaveIn = IO.Path.Combine(lSaveIn, aSaveFolder)
        Dim lStationIndex As Integer = 1
        Dim lDataType As String = "discharge"

        IO.Directory.CreateDirectory(lSaveIn)

        Dim lResults As String = ""
        Dim lNumNotDownloaded As Integer = 0

        Dim lStationFilename As String = IO.Path.Combine(lSaveIn, pDefaultStationsBaseFilename) & "_" & lDataType & ".rdb"
        If aStationIDs.Count = 0 Then
            lResults &= "<message>NWIS Daily Discharge: Stations must be selected before NWIS Daily Discharge retrieval</message>"
        Else
            Dim lStationsRDB As atcTableRDB = Nothing
            Dim lField_site_no As Integer = -1
            If Not IO.File.Exists(lStationFilename) Then
                GetStationsInRegion(aProject.Region, lStationFilename, NWIS.LayerSpecifications.Discharge)
            End If
            If IO.File.Exists(lStationFilename) Then
                lStationsRDB = New atcTableRDB
                If lStationsRDB.OpenFile(lStationFilename) Then
                    lField_site_no = lStationsRDB.FieldNumber("site_no")
                Else
                    lStationsRDB = Nothing
                End If
            End If

            Dim lWDM As atcWDM.atcDataSourceWDM = Nothing
            If Not String.IsNullOrEmpty(aWDMFilename) Then
                lWDM = atcDataManager.DataSourceBySpecification(aWDMFilename)
                If lWDM Is Nothing Then
                    lWDM = New atcWDM.atcDataSourceWDM
                    If lWDM.Open(aWDMFilename) Then
                        lResults &= "<add_data type='WDM'>" & aWDMFilename & "</add_data>" & vbCrLf
                    Else
                        lWDM = Nothing
                        Logger.Dbg("Unable to open WDM file '" & aWDMFilename & "' so not adding discharge data to it.")
                        aWDMFilename = ""
                    End If
                End If
            End If

            Dim lSiteData As String
            Dim lWDMAddCount As Integer = 0
            For lStationIndex = 0 To aStationIDs.Count - 1
                Dim lStationID As String = aStationIDs(lStationIndex)
                Dim lBaseFilename As String = "NWIS_discharge_" & lStationID & ".rdb"
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
                            Logger.Progress("Getting NWIS Discharge data for " & lStationID, lStationIndex, aStationIDs.Count)

                            Dim sURL As String = "http://waterdata.usgs.gov/nwis/dv?cb_00060=on&format=rdb" _
                                               & "&begin_date=" & Format(CDate(aStartDate), "yyyy-MM-dd") _
                                               & "&end_date=" & Format(CDate(aEndDate), "yyyy-MM-dd") _
                                               & "&site_no=" & lStationID
DownloadIt:
                            lSiteData = DownloadURL(sURL)

                            If lSiteData.StartsWith("No sites/data found") Then
                                lNumNotDownloaded += 1
                                Logger.Dbg("NWIS: No daily discharge data found for site '" & lStationID & "'")
                            ElseIf Not lSiteData.StartsWith("#") Then
                                If lSiteData.Contains("SYSTEM BUSY") Then
                                    Logger.Status("NWIS Database busy, retrying...", True)
                                    Threading.Thread.Sleep(30000)
                                    GoTo DownloadIt
                                End If
                                Select Case Logger.Msg("Data not downloaded for '" & lStationID & "' - View message?", MsgBoxStyle.YesNoCancel)
                                    Case MsgBoxResult.Yes
                                        Dim lErrorFile As String = GetTemporaryFileName("NWISerror", "html")
                                        IO.File.WriteAllText(lErrorFile, lSiteData)
                                        OpenFile(lErrorFile)
                                    Case MsgBoxResult.Cancel
                                        Return lResults
                                End Select
                            Else
                                Dim lSiteHeader As String = ""
                                If lStationsRDB Is Nothing Then
                                    'Try to get individual site header
                                    Dim lOneStationFilename As String = IO.Path.Combine(lSaveIn, pDefaultStationsBaseFilename) & "_" & lStationID & ".rdb"
                                    If Not IO.File.Exists(lOneStationFilename) Then
                                        GetStation(lStationID, lOneStationFilename)
                                    End If
                                    If IO.File.Exists(lOneStationFilename) Then
                                        lStationsRDB = New atcTableRDB
                                        If lStationsRDB.OpenFile(lOneStationFilename) Then
                                            lField_site_no = lStationsRDB.FieldNumber("site_no")
                                            If lField_site_no > 0 Then
                                                If lStationsRDB.FindFirst(lField_site_no, lStationID) Then
                                                    lSiteHeader = RecordAsRDBheader(lStationsRDB)
                                                End If
                                            End If
                                        End If
                                        lStationsRDB = Nothing
                                        lField_site_no = -1
                                    End If
                                ElseIf lField_site_no > 0 Then
                                    If lStationsRDB.FindFirst(lField_site_no, lStationID) Then
                                        lSiteHeader = RecordAsRDBheader(lStationsRDB)
                                    End If
                                End If

                                Logger.Dbg("Downloaded " & Format(lSiteData.Length, "#,###") & " bytes for site " & lStationID)
                                IO.Directory.CreateDirectory(IO.Path.GetDirectoryName(lCacheFilename))
                                IO.File.WriteAllText(lCacheFilename, "# " & sURL & vbCrLf _
                                                                   & "# download_date " & Format(Now, "yyyy-MM-dd HH:mm") & vbCrLf _
                                                                   & lSiteHeader & lSiteData)
                            End If
                        Catch e As Exception
                            Logger.Msg("Error getting data for NWIS station '" & lStationID & "'" & vbCrLf & e.Message, "NWIS Discharge")
                        End Try
                    End If
                End If

                If IO.File.Exists(lCacheFilename) Then
                    If Not aProject.CacheOnly Then
                        Dim lRDB As New atcTimeseriesRDB.atcTimeseriesRDB
                        If lWDM Is Nothing Then
                            TryCopy(lCacheFilename, lSaveAs)
                            lCacheFilename = lSaveAs
                        End If
                        'TODO: make sure atcTimeseriesRDB.Open does not read whole file so we don't run out of memory when downloading a lot
                        If lRDB.Open(lCacheFilename) Then
                            If lWDM Is Nothing Then
                                lResults &= "<add_data type='USGS RDB' subtype='daily discharge'>" & lSaveAs & "</add_data>" & vbCrLf
                                aProject.TimeseriesSources.Add(lRDB)
                            Else
                                For Each lTimeseries As atcData.atcTimeseries In lRDB.DataSets
                                    If lWDM.AddDataSet(lTimeseries, atcData.atcDataSource.EnumExistAction.ExistRenumber) Then
                                        lWDMAddCount += 1
                                    Else
                                        Logger.Dbg("AddDataset failed when adding discharge data " & lTimeseries.ToString)
                                    End If
                                Next
                                lRDB.Clear()
                            End If
                        Else
                            Logger.Dbg("Unable to open RDB file '" & lCacheFilename & "' as timeseries so not adding discharge data from it.")
                        End If
                    End If
                End If
            Next
            If lWDMAddCount > 0 Then
                lResults &= "<message>" & lWDMAddCount & " WDM Datasets added</message>"
                aProject.TimeseriesSources.Add(lWDM)
            End If
        End If
        Logger.Progress("", 0, 0)
        'lResults &= "<message>NWIS Daily Discharge data found for " & lStationIDs.Count - lNumNotDownloaded & " stations.</message>"
        Logger.Dbg("NWIS Daily Discharge data found for " & aStationIDs.Count - lNumNotDownloaded & " of " & aStationIDs.Count & " stations.")
        Return lResults
    End Function

    Friend Shared Function RecordAsRDBheader(ByVal aRDB As atcTable) As String
        Dim lSB As New Text.StringBuilder
        For lFieldIndex As Integer = 1 To aRDB.NumFields
            lSB.AppendLine("# " & aRDB.FieldName(lFieldIndex) & Space(48 - aRDB.FieldName(lFieldIndex).Length) & aRDB.Value(lFieldIndex))
        Next
        Return lSB.ToString
    End Function

    ''' <summary>
    ''' Get streamflow measurement data
    ''' </summary>
    ''' <param name="aProject">Project folder and cache settings. Region will be used if stations file is not found.</param>
    ''' <param name="aSaveFolder">Sub-folder within project folder or full path of folder to save in (if nothing or empty string, will save in aProject.ProjectFolder)</param>
    ''' <param name="aStationIDs">List of stations to get data from</param>
    ''' <param name="aStartDate">YYYY-MM-DD</param>
    ''' <param name="aEndDate">YYYY-MM-DD</param>
    ''' <returns></returns>
    ''' <remarks>Measurements are occasional manual observations, not automated/summarized</remarks>
    Public Shared Function GetMeasurements(ByVal aProject As Project,
                                           ByVal aSaveFolder As String,
                                           ByVal aStationIDs As Generic.List(Of String),
                                  Optional ByVal aStartDate As String = "1880-01-01",
                                  Optional ByVal aEndDate As String = "2100-01-01") As String
        Dim lSaveIn As String = aProject.ProjectFolder
        If aSaveFolder IsNot Nothing AndAlso aSaveFolder.Length > 0 Then lSaveIn = IO.Path.Combine(lSaveIn, aSaveFolder)
        Dim lResults As String = ""
        Dim lNumNotDownloaded As Integer = 0
        Dim lStationIndex As Integer = 1
        Dim lDataType As String = "measurements"

        IO.Directory.CreateDirectory(lSaveIn)
        Dim lStationFilename As String = IO.Path.Combine(lSaveIn, pDefaultStationsBaseFilename) & "_" & lDataType & ".rdb"
        Dim lStationsRDB As New atcTableRDB
        Dim lField_site_no As Integer = -1
        If Not IO.File.Exists(lStationFilename) Then 'Download list of measurement stations if not already present
            GetStationsInRegion(aProject.Region, lStationFilename, NWIS.LayerSpecifications.Measurement)
        End If
        If IO.File.Exists(lStationFilename) Then
            With lStationsRDB
                If .OpenFile(lStationFilename) Then
                    lField_site_no = .FieldNumber("site_no")
                    If aStationIDs.Count = 0 Then 'Select all measurement stations in area if none were selected in query
                        For lStationIndex = 1 To .NumRecords
                            .CurrentRecord = lStationIndex
                            aStationIDs.Add(.Value(lField_site_no))
                        Next
                    End If
                End If
            End With
        End If

        Dim lSiteData As String
        For lStationIndex = 0 To aStationIDs.Count - 1
            Dim lStationID As String = aStationIDs(lStationIndex)
            Dim lSaveAs As String = IO.Path.Combine(lSaveIn, "NWIS_measurements_" & lStationID & ".rdb")
            Try
                Logger.Progress("Getting NWIS Measurement data for " & lStationID, lStationIndex, aStationIDs.Count)

                Dim sURL As String = "http://nwis.waterdata.usgs.gov/nwis/measurements?search_site_no=" & lStationID _
                                   & "&search_site_no_match_type=exact&sort_key=site_no&group_key=NONE&sitefile_output_format=rdb" _
                                   & "&column_name=agency_cd&column_name=site_no&column_name=station_nm" _
                                   & "&begin_date=" & Format(CDate(aStartDate), "yyyy-MM-dd") _
                                   & "&end_date=" & Format(CDate(aEndDate), "yyyy-MM-dd") _
                                   & "&TZoutput=0&set_logscale_y=1&format=rdb&date_format=YYYY-MM-DD&rdb_compression=value&list_of_search_criteria=search_site_no"
                lSiteData = DownloadURL(sURL)

                If lSiteData.StartsWith("No sites/data found") Then
                    lNumNotDownloaded += 1
                    Logger.Dbg("NWIS: No " & lDataType & " found for site '" & lStationID & "'")
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
                    Dim lSiteHeader As String = ""
                    If lField_site_no > 0 Then
                        If lStationsRDB.FindFirst(lField_site_no, lStationID) Then
                            lSiteHeader = RecordAsRDBheader(lStationsRDB)
                        Else
                            lSiteHeader = ""
                        End If
                    End If
                    IO.File.WriteAllText(lSaveAs, "# " & sURL & vbCrLf & lSiteHeader & lSiteData)
                    Logger.Dbg("Downloaded " & Format(lSiteData.Length, "#,###") & " bytes for site " & lStationID)
                    lResults &= "<add_data type='USGS RDB' subtype='measurements'>" & lSaveAs & "</add_data>" & vbCrLf
                End If
            Catch e As Exception
                Logger.Msg("Error getting data for NWIS station '" & lStationID & "'" & vbCrLf & e.Message, "NWIS Measurements")
            End Try
            lStationIndex += 1
        Next
        Logger.Progress("", 0, 0)
        If lNumNotDownloaded > 0 Then
            lResults &= "<message>NWIS " & lDataType & ": " & lNumNotDownloaded & " of " & aStationIDs.Count & " stations did not have data available</message>"
        End If
        Return lResults
    End Function

End Class
