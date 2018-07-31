Imports atcData
Imports atcUtility
Imports MapWinUtility
Imports D4EM.Geo

Partial Public Class NWIS

    ''' <summary>
    ''' Get discharge values from the Instantaneous Data Archive
    ''' </summary>
    ''' <param name="aProject">Project folder and cache settings. Region will be used if stations file is not found.</param>
    ''' <param name="aSaveFolder">Sub-folder within project folder or full path of folder to save in (if nothing or empty string, will save in aProject.ProjectFolder)</param>
    ''' <param name="aStationIDs">List of stations to get data from</param>
    ''' <param name="aStartDate">YYYY-MM-DD</param>
    ''' <param name="aEndDate">YYYY-MM-DD</param>
    ''' <param name="aWDMFilename">If specified, data will be added to the named file, otherwise data will be saved in native .rdb files</param>
    ''' <param name="aIdaBaseUrl">This parameter does not need to be specified for normal use.</param>
    ''' <returns>XML describing success or errors</returns>
    ''' <remarks>
    ''' Instantaneous data is different from daily values. It is not always constant interval.
    ''' Saving these values in WDM can lead to some date confusion, 
    ''' so it is recommended that this data be left in its native .rdb format.
    ''' </remarks>
    Public Shared Function GetIDADischarge(ByVal aProject As Project,
                                           ByVal aSaveFolder As String,
                                           ByVal aStationIDs As Generic.List(Of String),
                                  Optional ByVal aStartDate As String = "1880-01-01",
                                  Optional ByVal aEndDate As String = "2100-01-01",
                                  Optional ByVal aWDMFilename As String = "",
                                  Optional ByVal aIdaBaseUrl As String = "http://ida.water.usgs.gov/ida/") As String

        Dim lSaveIn As String = aProject.ProjectFolder
        If aSaveFolder IsNot Nothing AndAlso aSaveFolder.Length > 0 Then lSaveIn = IO.Path.Combine(lSaveIn, aSaveFolder)
        Dim lResults As String = ""
        Dim lNumNotDownloaded As Integer = 0
        Dim lStartDate As Date = Date.Parse(aStartDate)
        Dim lEndDate As Date = Date.Parse(aEndDate)
        Dim lStationIndex As Integer = 1

        Dim lFromDateFormat As New atcUtility.atcDateFormat
        lFromDateFormat.IncludeHours = False
        lFromDateFormat.IncludeMinutes = False
        lFromDateFormat.DateSeparator = "-"
        lFromDateFormat.Midnight24 = False

        Dim lToDateFormat As New atcUtility.atcDateFormat
        lToDateFormat.IncludeHours = False
        lToDateFormat.IncludeMinutes = False
        lToDateFormat.DateSeparator = "-"

        IO.Directory.CreateDirectory(lSaveIn)

        Dim lStationFilename As String = IO.Path.Combine(lSaveIn, pDefaultStationsBaseFilename) & "_discharge.rdb"
        If aStationIDs.Count = 0 Then
            lResults &= "<message>NWIS IDA Discharge: Stations must be selected before NWIS IDA retrieval</message>"
            lResults &= "<select_layer>NWIS IDA Discharge Stations</select_layer>" & vbCrLf
        Else
            Dim lStationsRDB As New atcTableRDB
            Dim lField_site_no As Integer = -1
            If Not IO.File.Exists(lStationFilename) Then
                GetStationsInRegion(aProject.Region, lStationFilename, NWIS.LayerSpecifications.Discharge)
            End If
            If IO.File.Exists(lStationFilename) Then
                With lStationsRDB
                    If .OpenFile(lStationFilename) Then
                        lField_site_no = .FieldNumber("site_no")
                        If aStationIDs.Count = 0 Then
                            For lStationIndex = 1 To .NumRecords
                                .CurrentRecord = lStationIndex
                                aStationIDs.Add(.Value(lField_site_no))
                            Next
                        End If
                    End If
                End With
            End If

            Dim lWDM As atcWDM.atcDataSourceWDM = Nothing

            Dim lWDMAddCount As Integer = 0
            For lStationIndex = 0 To aStationIDs.Count - 1
                Dim lStationID As String = aStationIDs(lStationIndex)
                Dim lBaseFilename As String = "NWIS_IDA_" & lStationID & ".rdb"
                Dim lCacheFilename As String = IO.Path.Combine(IO.Path.Combine(aProject.CacheFolder, "NWIS"), lBaseFilename & ".zip")
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
                            Logger.Progress("Requesting NWIS IDA data for " & lStationID, lStationIndex, aStationIDs.Count)
                            D4EM.Data.Download.SetSecurityProtocol()

                            Dim lRequestCookies As System.Net.CookieContainer = Nothing
                            Dim lAvailableRecordsResponse As System.Net.HttpWebResponse = Nothing
                            Dim lResponseCookies As System.Net.CookieCollection = Nothing
                            Dim lAvailableRecordsURL As String = aIdaBaseUrl & "available_records.cfm?sn=" & lStationID
                            Dim lAvailableRecordsRequest As System.Net.HttpWebRequest = System.Net.WebRequest.Create(lAvailableRecordsURL)
                            If RequestWithHeadersAndCookies(lAvailableRecordsRequest, lAvailableRecordsResponse) Then
                                Dim lAvailableRecordsFilename As String = GetTemporaryFileName("AvailableRecords", "htm")
                                SaveResponse(lAvailableRecordsResponse, lAvailableRecordsFilename)
                                Dim lAvailableRecords As String = IO.File.ReadAllText(lAvailableRecordsFilename)
                                With lAvailableRecords
                                    Dim lMinDateTimeIndex As Integer = .IndexOf("mindatetime")
                                    Dim lMaxDateTimeIndex As Integer = .IndexOf("maxdatetime")
                                    If lMaxDateTimeIndex > 0 AndAlso lMaxDateTimeIndex > 0 Then
                                        Dim lMinDateString As String = .Substring(.IndexOf("value", lMinDateTimeIndex) + 7, 21)
                                        Dim lMaxDateString As String = .Substring(.IndexOf("value", lMaxDateTimeIndex) + 7, 21)
                                        Dim lMinDate As Date = Date.Parse(lMinDateString)
                                        Dim lMaxDate As Date = Date.Parse(lMaxDateString)

                                        Dim lFromDateString As String
                                        Dim lToDateString As String
                                        If lMinDate > lStartDate Then
                                            lFromDateString = lFromDateFormat.JDateToString(lMinDate.ToOADate)
                                        Else
                                            lFromDateString = lFromDateFormat.JDateToString(lStartDate.ToOADate)
                                        End If

                                        If lMaxDate < lEndDate Then
                                            lToDateString = lToDateFormat.JDateToString(lMaxDate.ToOADate)
                                        Else
                                            lToDateString = lToDateFormat.JDateToString(lEndDate.ToOADate)
                                        End If
                                        Dim lAvailableRecordsProcessURL As String = aIdaBaseUrl & "available_records_process.cfm"
                                        Dim lAvailableRecordsProcessArgs As String = _
                                              "fromdate=" & lFromDateString _
                                            & "&todate=" & lToDateString _
                                            & "&mindatetime=" & lMinDateString _
                                            & "&maxdatetime=" & lMaxDateString _
                                            & "&site_no=" & lStationID _
                                            & "&rtype=2&submit1=Retrieve+Data"
                                        'rtype: "1" Save to file, "2" Save to compressed file, 3 Display in browser
                                        lAvailableRecordsProcessArgs = lAvailableRecordsProcessArgs.Replace(" ", "+")
                                        lAvailableRecordsProcessArgs = lAvailableRecordsProcessArgs.Replace(":", "%3A")
                                        Dim lAvailableRecordsProcessRequest As System.Net.HttpWebRequest = System.Net.WebRequest.Create(lAvailableRecordsProcessURL)

                                        Dim lCookiesHeader As String = lAvailableRecordsResponse.Headers.Item("Set-Cookie")

                                        With lAvailableRecordsProcessRequest.Headers
                                            lAvailableRecordsProcessRequest.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8"
                                            .Add(System.Net.HttpRequestHeader.AcceptLanguage, "en-us,en;q=0.5")
                                            .Add(System.Net.HttpRequestHeader.AcceptEncoding, "gzip,deflate")
                                            .Add(System.Net.HttpRequestHeader.AcceptCharset, "ISO-8859-1,utf-8;q=0.7,*;q=0.7")
                                            lAvailableRecordsProcessRequest.Referer = lAvailableRecordsURL
                                            .Add(System.Net.HttpRequestHeader.Cookie, lCookiesHeader)
                                        End With

                                        Dim lAvailableRecordsProcessResponse As System.Net.HttpWebResponse = Nothing

                                        If RequestWithHeadersAndCookies(lAvailableRecordsProcessRequest, _
                                                                        lAvailableRecordsProcessResponse, _
                                                                        lAvailableRecordsProcessArgs) Then
                                            If lAvailableRecordsProcessResponse.ContentType.Contains("text") Then
                                                lNumNotDownloaded += 1
                                                If Logger.Msg("View error message?", MsgBoxStyle.YesNo, "IDA unable to find data for " & lStationID) = MsgBoxResult.Yes Then
                                                    Dim lErrorFilename As String = GetTemporaryFileName("IDA_Error", "html")
                                                    SaveResponse(lAvailableRecordsProcessResponse, lErrorFilename)
                                                    OpenFile(lErrorFilename)
                                                End If
                                            Else
                                                SaveResponse(lAvailableRecordsProcessResponse, lCacheFilename)
                                            End If
                                        End If

                                    ElseIf .Contains("not available in the Instantaneous Data Archive") Then
                                        lNumNotDownloaded += 1
                                        Logger.Dbg("IDA does not contain data for " & lStationID)
                                    Else
                                        If Logger.Msg("View error message?", MsgBoxStyle.YesNo, "IDA unable to find date range for " & lStationID) = MsgBoxResult.Yes Then
                                            OpenFile(lAvailableRecordsFilename)
                                        End If
                                    End If
                                End With
                                TryDelete(lAvailableRecordsFilename)
                            End If
                            '    IO.File.WriteAllText(lCacheFilename, "# " & sURL & vbCrLf _
                            '           & "# download_date " & Format(Now, "yyyy-MM-dd HH:mm") & vbCrLf _
                            '           & lSiteHeader & lSiteData)
                            'End If
                        Catch e As Exception
                            Logger.Msg("Error getting data for NWIS station '" & lStationID & "'" & vbCrLf & e.Message, "NWIS Discharge")
                        End Try
                    End If
                End If

                If IO.File.Exists(lCacheFilename) Then
                    If Not aProject.CacheOnly Then

                        'Unzip to a temporary folder
                        Dim lUnzipFolder As String = atcUtility.NewTempDir(IO.Path.Combine(aProject.CacheFolder, "IDA_Unzip"))
                        Try
                            Zipper.UnzipFile(lCacheFilename, lUnzipFolder)
                        Catch exZip As Exception 'TODO: handle bad zip files uniformly in UnzipFile
                            lNumNotDownloaded += 1
                            TryMove(lCacheFilename, lCacheFilename & ".bad")
                            TryDelete(lUnzipFolder)
                            Logger.Dbg("Could not unzip IDA download '" & lCacheFilename & "': " & exZip.Message)
                            Continue For 'Stop trying to process this station, move on to next one
                        End Try

                        Try
                            Dim lFilenames As New Collections.Specialized.NameValueCollection
                            AddFilesInDir(lFilenames, lUnzipFolder, True)
                            If lFilenames.Count < 1 Then
                                lNumNotDownloaded += 1
                                lResults &= "<message>No file found in IDA download " & lCacheFilename & "</message>"
                            Else
                                For Each lUnzippedRDBFilename As String In lFilenames

                                    If aWDMFilename.Length > 0 Then
                                        Dim lRDB As New atcTimeseriesRDB.atcTimeseriesRDB
                                        If Not lRDB.Open(lUnzippedRDBFilename) Then
                                            Logger.Dbg("Unable to open RDB file '" & lUnzippedRDBFilename & "' as timeseries so not adding IDA data from it to WDM.")
                                        ElseIf lRDB.DataSets.Count > 0 Then

                                            If lWDM Is Nothing Then 'Open WDM file now that we are sure we have at least one dataset to add
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

                                            If lWDM IsNot Nothing Then 'Add RDB datasets to WDM
                                                For Each lTimeseries As atcData.atcTimeseries In lRDB.DataSets
                                                    If lWDM.AddDataSet(lTimeseries, atcData.atcDataSource.EnumExistAction.ExistRenumber) Then
                                                        lWDMAddCount += 1
                                                    Else
                                                        Logger.Dbg("AddDataset failed when adding IDA data " & lTimeseries.ToString)
                                                    End If
                                                Next
                                            End If

                                        End If
                                    Else
                                        If TryCopy(lUnzippedRDBFilename, lSaveAs) Then
                                            lResults &= "<add_data type='USGS RDB' subtype='IDA discharge'>" & lSaveAs & "</add_data>" & vbCrLf
                                        Else
                                            lNumNotDownloaded += 1
                                            Logger.Dbg("Unable to copy RDB file '" & lUnzippedRDBFilename & "' to '" & lSaveAs & "'")
                                        End If
                                    End If
                                    TryDelete(lUnzippedRDBFilename)
                                Next
                            End If
                        Catch exPost As Exception
                            Logger.Dbg("Exception processing downloaded file '" & lCacheFilename & ": " & exPost.ToString)
                        End Try
                        TryDelete(lUnzipFolder)
                    End If
                End If
            Next
        End If
        Logger.Progress("", 0, 0)
        Dim lMessage As String
        If lNumNotDownloaded = 0 Then
            If aStationIDs.Count = 1 Then
                lMessage = "IDA data found for requested station: " & aStationIDs.Item(0)
            Else
                lMessage = "IDA data found for all " & aStationIDs.Count & " stations."
            End If
        ElseIf lNumNotDownloaded = aStationIDs.Count Then
            If aStationIDs.Count = 1 Then
                lMessage = "IDA data not available for the requested station: " & aStationIDs.Item(0)
            Else
                lMessage = "IDA data not available for the requested " & aStationIDs.Count & " stations."
            End If
        Else
            lMessage = "IDA data is not yet available for all discharge stations." & vbCrLf & " IDA data found for " & aStationIDs.Count - lNumNotDownloaded & " of " & aStationIDs.Count & " stations. "
        End If
        Logger.Dbg(lMessage)
        lResults &= "<message>" & lMessage & "</message>"
        Return lResults
    End Function

    Private Shared Function RequestWithHeadersAndCookies(ByVal Request As System.Net.HttpWebRequest, _
                                                  ByRef aResponse As System.Net.HttpWebResponse, _
                                         Optional ByVal aPost As String = Nothing) As Boolean
        Dim lError As Boolean = False
        Dim lSkipped As Boolean = False
        Try
            If Request IsNot Nothing Then
                Request.AllowAutoRedirect = True
                Request.Proxy.Credentials = System.Net.CredentialCache.DefaultCredentials
                If Not String.IsNullOrEmpty(aPost) Then
                    Request.Method = System.Net.WebRequestMethods.Http.Post
                    Request.ContentType = "application/x-www-form-urlencoded"
                    Dim lPostWriter As New IO.StreamWriter(Request.GetRequestStream())
                    lPostWriter.Write(aPost)
                    lPostWriter.Close()
                    Request.Expect = Nothing
                End If

                aResponse = Request.GetResponse()
                Dim lContentLength As String = aResponse.Headers.Item("Content-Length")
                If lContentLength = "0" Then ' Nothing can be ok, but zero means there is no tile
                    lError = True
                End If
            End If
        Catch ex As Exception
            If ex.Message.IndexOf("(304) Not Modified") > -1 Then
                'Not an error, this means we don't need to download new file
                lSkipped = True
            Else
                lError = True
                Logger.Dbg("Error downloading '" & Request.Address.ToString & "' " & ex.Message)
            End If
        End Try
AfterDownload:
        Return Not lError
    End Function

    ''' <summary>
    ''' Save the response stream to a file
    ''' </summary>
    ''' <param name="lResponse">Response containing stream to save</param>
    ''' <param name="aFilename">Save the stream to this file</param>
    ''' <returns>True on success, False on failure</returns>
    Private Shared Function SaveResponse(ByVal lResponse As System.Net.HttpWebResponse, ByVal aFilename As String) As Boolean
        Dim lError As Boolean = False
        Dim lInput As IO.Stream = Nothing
        Dim lOutput As IO.FileStream = Nothing
        Try
            lInput = lResponse.GetResponseStream()
            If lInput IsNot Nothing Then
                Dim lBufferSize As Long = 128 * 1024 ' 128k at a time
                Dim lBytesRead As Long
                Dim lBuffer(lBufferSize - 1) As Byte
                IO.Directory.CreateDirectory(IO.Path.GetDirectoryName(aFilename))
                lOutput = New IO.FileStream(aFilename, IO.FileMode.Create)
                If lOutput IsNot Nothing Then
                    Do
                        lBytesRead = lInput.Read(lBuffer, 0, lBufferSize)
                        If lBytesRead = 0 Then Exit Do 'finished download
                        lOutput.Write(lBuffer, 0, lBytesRead)
                    Loop
                End If
            End If
        Catch ex As Exception
            lError = True
            Logger.Dbg("Error downloading: " & ex.Message)
        End Try
AfterDownload:
        If lInput IsNot Nothing Then
            Try
                lInput.Close()
            Catch exInput As Exception
            End Try
        End If
        If lOutput IsNot Nothing Then
            Try
                lOutput.Close()
            Catch exOutput As Exception
            End Try
        End If

        Return Not lError

    End Function

End Class
