Imports atcData
Imports atcUtility
Imports MapWinUtility
Imports D4EM.Geo

Partial Class NWIS
    ''' <summary>Get daily groundwater values for the given stations</summary>
    ''' <param name="aProject">Project folder and cache settings.</param>
    ''' <param name="aSaveFolder">Sub-folder within project folder or full path of folder to save in (if nothing or empty string, will save in aProject.ProjectFolder)</param>
    ''' <param name="aStationIDs">List of stations to get data from</param>
    ''' <param name="aStartDate">YYYY-MM-DD</param>
    ''' <param name="aEndDate">YYYY-MM-DD</param>
    ''' <param name="aWDMFilename">If specified, data will be added to the named WDM file, otherwise data will be saved in native .rdb files</param>
    ''' <returns>XML describing success or errors</returns>
    Public Shared Function GetDailyGroundwater(ByVal aProject As Project,
                                               ByVal aSaveFolder As String,
                                               ByVal aStationIDs As Generic.List(Of String),
                                      Optional ByVal aStartDate As String = "1880-01-01",
                                      Optional ByVal aEndDate As String = "2100-01-01",
                                      Optional ByVal aWDMFilename As String = "") As String
        Dim lDataTypeLabel As String = "NWIS Daily Groundwater"
        Dim lSaveIn As String = aProject.ProjectFolder
        If aSaveFolder IsNot Nothing AndAlso aSaveFolder.Length > 0 Then lSaveIn = IO.Path.Combine(lSaveIn, aSaveFolder)
        Dim lStationIndex As Integer = 1

        IO.Directory.CreateDirectory(lSaveIn)

        Dim lResults As String = ""
        Dim lNumNotDownloaded As Integer = 0

        Dim lStationFilename As String = IO.Path.Combine(lSaveIn, pDefaultStationsBaseFilename) & "_gw.rdb"
        If aStationIDs.Count = 0 Then
            lResults &= "<message>" & lDataTypeLabel & ": Stations must be selected before " & lDataTypeLabel & " retrieval</message>"
        Else
            Dim lStationsRDB As atcTableRDB = Nothing
            Dim lField_site_no As Integer = -1
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
                        Logger.Dbg("Unable to open WDM file '" & aWDMFilename & "' so not adding data to it.")
                        aWDMFilename = ""
                    End If
                End If
            End If

            Dim lSiteData As String
            Dim lWDMAddCount As Integer = 0
            For lStationIndex = 0 To aStationIDs.Count - 1
                Dim lStationID As String = aStationIDs(lStationIndex)
                Dim lBaseFilename As String = "NWIS_gw_" & lStationID & ".rdb"
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
                            Logger.Progress("Getting " & lDataTypeLabel & " data for " & lStationID, lStationIndex, aStationIDs.Count)

                            Dim sURL As String = "http://waterservices.usgs.gov/nwis/dv?format=rdb" _
                                               & "&sites=" & lStationID _
                                               & "&startDT=" & Format(CDate(aStartDate), "yyyy-MM-dd") _
                                               & "&endDT=" & Format(CDate(aEndDate), "yyyy-MM-dd") _
                                               & "&parameterCd=72019"
DownloadIt:
                            lSiteData = DownloadURL(sURL)

                            If lSiteData.StartsWith("No sites/data found") Then
                                lNumNotDownloaded += 1
                                Logger.Dbg("NWIS: No " & lDataTypeLabel & " data found for site '" & lStationID & "'")
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
                            Logger.Msg("Error getting data for NWIS station '" & lStationID & "'" & vbCrLf & e.Message, lDataTypeLabel)
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
                                lResults &= "<add_data type='USGS RDB' subtype='daily groundwater'>" & lSaveAs & "</add_data>" & vbCrLf
                                aProject.TimeseriesSources.Add(lRDB)
                            Else
                                For Each lTimeseries As atcData.atcTimeseries In lRDB.DataSets
                                    If lWDM.AddDataSet(lTimeseries, atcData.atcDataSource.EnumExistAction.ExistRenumber) Then
                                        lWDMAddCount += 1
                                    Else
                                        Logger.Dbg("AddDataset failed when adding " & lDataTypeLabel & " data " & lTimeseries.ToString)
                                    End If
                                Next
                                lRDB.Clear()
                            End If
                        Else
                            Logger.Dbg("Unable to open RDB file '" & lCacheFilename & "' as timeseries so not adding " & lDataTypeLabel & " data from it.")
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
        Logger.Dbg(lDataTypeLabel & " data found for " & aStationIDs.Count - lNumNotDownloaded & " of " & aStationIDs.Count & " stations.")
        Return lResults
    End Function

End Class
