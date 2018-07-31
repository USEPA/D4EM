Imports atcData
Imports atcUtility
Imports MapWinUtility
Imports D4EM.Geo

Partial Class NWIS
    Private Const EarliestDate As String = "1880-01-01"
    Private Const LatestDate As String = "2100-01-01"

    ''' <summary>Get daily groundwater or precipitation values for the given stations</summary>
    ''' <param name="aProject">Project folder and cache settings.</param>
    ''' <param name="aSaveFolder">Sub-folder within project folder or full path of folder to save in (if nothing or empty string, will save in aProject.ProjectFolder)</param>
    ''' <param name="aStationIDs">List of stations to get data from</param>
    ''' <param name="aStartDate">YYYY-MM-DD</param>
    ''' <param name="aEndDate">YYYY-MM-DD</param>
    ''' <param name="aWDMFilename">If specified, data will be added to the named WDM file, otherwise data will be saved in native .rdb files</param>
    ''' <param name="aDataType">Precip or groundwater ("precip" vs "gw")</param>
    ''' <returns>XML describing success or errors</returns>
    Public Shared Function GetDailyGroundwater(ByVal aProject As Project,
                                               ByVal aSaveFolder As String,
                                               ByVal aStationIDs As Generic.List(Of String),
                                      Optional ByVal aStartDate As String = EarliestDate,
                                      Optional ByVal aEndDate As String = LatestDate,
                                      Optional ByVal aWDMFilename As String = "",
                                      Optional ByVal aDataType As String = "gw") As String

        Dim lDataTypeLabel As String = "NWIS Daily " & aDataType.ToUpper()
        Dim lSaveIn As String = aProject.ProjectFolder
        If aSaveFolder IsNot Nothing AndAlso aSaveFolder.Length > 0 Then lSaveIn = IO.Path.Combine(lSaveIn, aSaveFolder)
        Dim lStationIndex As Integer = 1

        IO.Directory.CreateDirectory(lSaveIn)

        Dim lResults As String = ""
        Dim lNumNotDownloaded As Integer = 0

        Dim lStationFilename As String = IO.Path.Combine(lSaveIn, pDefaultStationsBaseFilename) & "_" & aDataType & "_daily.rdb"
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
            Dim lNewDataCount As Integer = 0
            Dim lWDMAddCount As Integer = 0
            For lStationIndex = 0 To aStationIDs.Count - 1
                Dim lStationID As String = aStationIDs(lStationIndex)
                Dim lBaseFilename As String = "NWIS_" & aDataType & "_daily_" & lStationID & ".rdb"
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
                            D4EM.Data.Download.SetSecurityProtocol()
                            Logger.Progress("Getting " & lDataTypeLabel & " data for " & lStationID, lStationIndex, aStationIDs.Count)

                            Dim sURL As String = "http://waterservices.usgs.gov/nwis/dv?format=rdb" _
                                               & "&sites=" & lStationID _
                                               & "&startDT=" & Format(CDate(aStartDate), "yyyy-MM-dd") _
                                               & "&endDT=" & Format(CDate(aEndDate), "yyyy-MM-dd") '& "&parameterCd=72019"

                            If aDataType = "precip" Then
                                sURL &= "&parameterCd=00045"
                            End If
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
                            Logger.Dbg(lDataTypeLabel & " did not get data for station '" & lStationID & "':" & e.Message)
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
                                lResults &= "<add_data type='USGS RDB' subtype='daily " & aDataType & "'>" & lSaveAs & "</add_data>" & vbCrLf
                                aProject.TimeseriesSources.Add(lRDB)
                                lNewDataCount += 1
                            Else
                                For Each lTimeseries As atcData.atcTimeseries In lRDB.DataSets
                                    If lWDM.AddDataSet(lTimeseries, atcData.atcDataSource.EnumExistAction.ExistRenumber) Then
                                        lNewDataCount += 1
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
            If lNewDataCount > 0 Then
                If lNewDataCount = 1 Then
                    lResults &= "<message>One " & lDataTypeLabel & " dataset added.</message>"
                Else
                    lResults &= "<message>" & lNewDataCount & " " & lDataTypeLabel & " datasets added.</message>"
                End If
                'If lWDM IsNot Nothing Then aProject.TimeseriesSources.Add(lWDM)
            ElseIf lWDM Is Nothing Then
                lResults = "<message>" & lDataTypeLabel & " data not found.</message>"
            Else
                If lWDM.DataSets.Count = 0 Then
                    TryDelete(aWDMFilename)
                End If
                If IO.File.Exists(aWDMFilename) Then
                    lResults = "<message>" & lDataTypeLabel & " data not found.</message>"
                Else
                    lResults = "<message>" & lDataTypeLabel & " data not found, '" & aWDMFilename & "' not created</message>"
                End If
            End If
            If lWDMAddCount > 0 Then
                lResults &= "<message>" & lWDMAddCount & " WDM Datasets added</message>"
                aProject.TimeseriesSources.Add(lWDM)
            End If
        End If
        Logger.Progress("", 0, 0)
        Logger.Dbg(lDataTypeLabel & " data found for " & aStationIDs.Count - lNumNotDownloaded & " of " & aStationIDs.Count & " stations.")
        Return lResults
    End Function

    ''' <summary>
    ''' Retrieve non-daily groundwater data values by parsing XML arguments and calling multi-argument version of this function
    ''' </summary>
    ''' <param name="aArgs">XML-formatted arguments</param>
    ''' <returns>XML describing success or errors</returns>
    Public Function GetPeriodicGroundwater(ByVal aArgs As Xml.XmlNode) As String
        Dim lStartDate As String = EarliestDate
        Dim lEndDate As String = LatestDate
        Dim lStationIDs As New Generic.List(Of String)
        Dim lCacheFolder As String = IO.Path.GetTempPath
        Dim lGetEvenIfCached As Boolean = False
        Dim lCacheOnly As Boolean = False
        Dim lSaveIn As String = ""
        Dim lStationIndex As Integer = 1
        Dim lWDMFilename As String = ""

        Dim lArg As Xml.XmlNode = aArgs.FirstChild

        While Not lArg Is Nothing
            Select Case lArg.Name.ToLower
                Case "startdate" : lStartDate = lArg.InnerText
                Case "enddate" : lEndDate = lArg.InnerText
                Case "stationid" : If Not lStationIDs.Contains(lArg.InnerText) Then lStationIDs.Add(lArg.InnerText)
                Case "cachefolder" : lCacheFolder = lArg.InnerText
                Case "cacheonly" : lCacheOnly = True
                Case "getevenifcached" : If Not lArg.InnerText.ToLower.Contains("false") Then lGetEvenIfCached = True
                Case "savein" : lSaveIn = lArg.InnerText
                Case "savewdm" : lWDMFilename = lArg.InnerText
            End Select
            lArg = lArg.NextSibling
        End While
        Return GetPeriodicGroundwater(lSaveIn, "NWIS", lCacheFolder, lCacheOnly, lGetEvenIfCached, lStationIDs, lStartDate, lEndDate, lWDMFilename)
    End Function

    ''' <summary>Get periodic groundwater values for the given stations</summary>
    ''' <param name="aProjectFolder">Project folder</param>
    ''' <param name="aSaveFolder">Sub-folder within project folder or full path of folder to save in (if nothing or empty string, will save in aProjectFolder)</param>
    ''' <param name="aStationIDs">List of stations to get data from</param>
    ''' <param name="aStartDate">YYYY-MM-DD</param>
    ''' <param name="aEndDate">YYYY-MM-DD</param>
    ''' <param name="aWDMFilename">If specified, data will be added to the named WDM file, otherwise data will be saved in native .rdb files</param>
    ''' <returns>XML describing success or errors</returns>
    Public Function GetPeriodicGroundwater(ByVal aProjectFolder As String,
                                        ByVal aSaveFolder As String,
                                        ByVal aCacheFolder As String,
                                        ByVal aCacheOnly As Boolean,
                                        ByVal aGetEvenIfCached As Boolean,
                                        ByVal aStationIDs As Generic.List(Of String),
                               Optional ByVal aStartDate As String = "1880-01-01",
                               Optional ByVal aEndDate As String = LatestDate,
                               Optional ByVal aWDMFilename As String = "") As String

        Dim lDataTypeLabel As String = "NWIS Periodic Groundwater"
        Dim lSaveIn As String = aProjectFolder
        If aSaveFolder IsNot Nothing AndAlso aSaveFolder.Length > 0 Then lSaveIn = IO.Path.Combine(lSaveIn, aSaveFolder)
        Dim lStationIndex As Integer = 1

        IO.Directory.CreateDirectory(lSaveIn)

        Dim lResults As String = ""
        Dim lNumNotDownloaded As Integer = 0

        Dim lStationFilename As String = IO.Path.Combine(lSaveIn, pDefaultStationsBaseFilename) & "_gw_periodic.rdb"
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
            Dim lNewDataCount As Integer = 0
            For lStationIndex = 0 To aStationIDs.Count - 1
                Dim lStationID As String = aStationIDs(lStationIndex)
                Dim lBaseFilename As String = "NWIS_gw_periodic_" & lStationID & ".rdb"
                Dim lCacheFilename As String = IO.Path.Combine(IO.Path.Combine(aCacheFolder, "NWIS"), lBaseFilename)
                Dim lSaveAs As String = IO.Path.Combine(lSaveIn, lBaseFilename)
                If aGetEvenIfCached AndAlso IO.File.Exists(lSaveAs) Then TryDelete(lSaveAs)
                If IO.File.Exists(lSaveAs) Then
                    Logger.Dbg("Already have '" & lSaveAs & "'")
                Else
                    If aGetEvenIfCached AndAlso IO.File.Exists(lCacheFilename) Then TryDelete(lCacheFilename)
                    If IO.File.Exists(lCacheFilename) Then
                        Logger.Dbg("Using cached '" & lCacheFilename & "'")
                    Else
                        Try
                            Logger.Progress("Getting " & lDataTypeLabel & " data for " & lStationID, lStationIndex, aStationIDs.Count)
                            'http://nwis.waterdata.usgs.gov/nwis/gwlevels?site_no=410947071344803&agency_cd=USGS&format=rdb

                            Dim sURL As String = "http://nwis.waterdata.usgs.gov/nwis/gwlevels?site_no=" & lStationID _
                                               & "&agency_cd=USGS&format=rdb"
                            If aStartDate <> EarliestDate Then sURL &= "&startDT=" & Format(CDate(aStartDate), "yyyy-MM-dd")
                            If aEndDate <> LatestDate Then sURL &= "&endDT=" & Format(CDate(aEndDate), "yyyy-MM-dd")
                            Logger.Dbg(sURL)
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
                            Logger.Dbg(lDataTypeLabel & " did not get data for station '" & lStationID & "':" & e.Message)
                        End Try
                    End If
                End If

                If IO.File.Exists(lCacheFilename) Then
                    If Not aCacheOnly Then
                        Dim lRDB As New atcTimeseriesRDB.atcTimeseriesRDB
                        If lWDM Is Nothing Then
                            TryCopy(lCacheFilename, lSaveAs)
                            lCacheFilename = lSaveAs
                        End If
                        'TODO: make sure atcTimeseriesRDB.Open does not read whole file so we don't run out of memory when downloading a lot
                        If lRDB.Open(lCacheFilename) Then
                            If lWDM Is Nothing Then
                                lResults &= "<add_data type='USGS RDB' subtype='daily groundwater'>" & lSaveAs & "</add_data>" & vbCrLf
                                'aProject.TimeseriesSources.Add(lRDB)
                                lNewDataCount += 1
                            Else
                                For Each lTimeseries As atcData.atcTimeseries In lRDB.DataSets
                                    If lWDM.AddDataSet(lTimeseries, atcData.atcDataSource.EnumExistAction.ExistRenumber) Then
                                        lNewDataCount += 1
                                    Else
                                        Logger.Dbg("AddDataset failed when adding " & lDataTypeLabel & " data " & lTimeseries.ToString)
                                    End If
                                Next
                                'lRDB.Clear()
                            End If
                        Else
                            Logger.Dbg("Unable to open RDB file '" & lCacheFilename & "' as timeseries so not adding " & lDataTypeLabel & " data from it.")
                        End If
                    End If
                End If
            Next
            If lNewDataCount > 0 Then
                If lNewDataCount = 1 Then
                    lResults &= "<message>One " & lDataTypeLabel & " dataset added.</message>"
                Else
                    lResults &= "<message>" & lNewDataCount & " " & lDataTypeLabel & " datasets added.</message>"
                End If
                'If lWDM IsNot Nothing Then aProject.TimeseriesSources.Add(lWDM)
            ElseIf lWDM Is Nothing Then
                lResults = "<message>" & lDataTypeLabel & " data not found.</message>"
            Else
                If lWDM.DataSets.Count = 0 Then
                    TryDelete(aWDMFilename)
                End If
                If IO.File.Exists(aWDMFilename) Then
                    lResults = "<message>" & lDataTypeLabel & " data not found.</message>"
                Else
                    lResults = "<message>" & lDataTypeLabel & " data not found, '" & aWDMFilename & "' not created</message>"
                End If
            End If
        End If
        Logger.Progress("", 0, 0)
        Logger.Dbg(lDataTypeLabel & " data found for " & aStationIDs.Count - lNumNotDownloaded & " of " & aStationIDs.Count & " stations.")
        Return lResults
    End Function

End Class
