Imports atcData
Imports atcUtility
Imports MapWinUtility

Module modNCDC

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
    Private Function GetMetStations(ByVal aProject As D4EM.Data.Project,
                                    aStationSpecification As D4EM.Data.LayerSpecification,
                                          ByRef aStationIDs As Generic.List(Of String),
                                          ByVal aCreateShapefile As Boolean,
                                          ByVal aStationsShapeFilename As String,
                                          ByVal aMethod As String,
                                 Optional ByVal aStartDate As Date = #1/1/1000#,
                                 Optional ByVal aEndDate As Date = #1/1/1000#) As String
        Dim lResults As String = ""
        Dim lCacheFilename As String = ""

        Dim lLatitudeField As Integer = 5
        Dim lLongitudeField As Integer = 6
        Dim lLocationField As Integer = 2
        Dim lStartDateField As Integer = 9
        Dim lEndDateField As Integer = 10

        Dim lCachefolder As String = IO.Path.Combine(aProject.CacheFolder, "NCDC") & g_PathChar
        Dim lAllStationsDBFFileName As String = IO.Path.Combine(lCachefolder, "All_" + aStationSpecification.Tag + ".dbf")
        Dim lAllStationsDBF As atcTableDBF = Nothing
        If IO.File.Exists(lAllStationsDBFFileName) Then
            lAllStationsDBF = New atcTableDBF
            lAllStationsDBF.OpenFile(lAllStationsDBFFileName)
        Else
            Dim lAllStationsCSVFileName As String = IO.Path.ChangeExtension(lAllStationsDBFFileName, ".csv")
            If D4EM.Data.Source.NCDC.GetAllSitesCSV(lAllStationsCSVFileName, aStationSpecification) Then
                Dim lAllStationsCSV As New atcTableDelimited
                lAllStationsCSV.Delimiter = ","
                lAllStationsCSV.NumFieldNameRows = 1
                lAllStationsCSV.OpenFile(lAllStationsCSVFileName)

                lAllStationsDBF = New atcTableDBF
                With lAllStationsDBF
                    .NumFields = lAllStationsCSV.NumFields

                    For lField As Integer = 1 To .NumFields
                        .FieldName(lField) = lAllStationsCSV.FieldName(lField)
                        .FieldLength(lField) = lAllStationsCSV.FieldLength(lField)
                    Next
                    .NumRecords = lAllStationsCSV.NumRecords
                    .InitData()
                    For lRecordNum As Integer = 1 To .NumRecords
                        .CurrentRecord = lRecordNum
                        lAllStationsCSV.CurrentRecord = lRecordNum
                        For lField As Integer = 1 To .NumFields
                            .Value(lField) = lAllStationsCSV.Value(lField)
                        Next
                    Next
                    .WriteFile(lAllStationsDBFFileName)
                End With
            End If
        End If

        If lAllStationsDBF IsNot Nothing Then
            Dim lLocationProgress As Integer = 0

            Dim lSaveIn As String = IO.Path.Combine(aProject.ProjectFolder, "NCDC") & g_PathChar
            IO.Directory.CreateDirectory(lSaveIn)

            If String.IsNullOrEmpty(aStationsShapeFilename) Then
                aStationsShapeFilename = "ish.shp"
            End If
            If Not IO.Path.IsPathRooted(aStationsShapeFilename) Then
                aStationsShapeFilename = IO.Path.Combine(lSaveIn, aStationsShapeFilename)
            End If
            Dim lLocationDBFFilename As String = IO.Path.ChangeExtension(aStationsShapeFilename, ".dbf")

            Dim lStationsLayer As DotSpatial.Data.PointShapefile = Nothing
            If aCreateShapefile Then
                If Not TryDeleteShapefile(aStationsShapeFilename) Then
                    aStationsShapeFilename = GetTemporaryFileName(IO.Path.ChangeExtension(aStationsShapeFilename, "").TrimEnd("."), ".shp")
                End If

                lStationsLayer = New DotSpatial.Data.PointShapefile(aStationsShapeFilename)
                lStationsLayer.FilePath = aStationsShapeFilename.ToString()
                lStationsLayer.Filename = aStationsShapeFilename.ToString() ' lSaveIn.ToString()
                lStationsLayer.SaveAs(aStationsShapeFilename, True)
                lStationsLayer.Close()
                lStationsLayer = DotSpatial.Data.PointShapefile.Open(aStationsShapeFilename)

                Dim lStationDBF As atcTableDBF = lAllStationsDBF.Cousin
                lStationDBF.NumRecords = lAllStationsDBF.NumRecords + 1
                lStationDBF.InitData()
                With lAllStationsDBF
                    .CurrentRecord = 1
                    Do
                        Try
                            Dim lPoint As New DotSpatial.Topology.Coordinate(.Value(lLongitudeField), .Value(lLatitudeField))
                            Dim lShape As New DotSpatial.Data.Shape(lPoint)
                            DotSpatial.Projections.Reproject.ReprojectPoints(lShape.Vertices, lShape.Z, D4EM.Data.Globals.GeographicProjection, aProject.DesiredProjection, 0, 1)
                            lStationsLayer.AddShape(lShape)
                            lStationDBF.RawRecord = .RawRecord
                            lStationDBF.CurrentRecord += 1
                        Catch
                        End Try
                        If .CurrentRecord = .NumRecords Then Exit Do
                        .CurrentRecord += 1
                    Loop
                    lStationDBF.NumRecords = lStationDBF.CurrentRecord - 1
                    lStationsLayer.Projection = aProject.DesiredProjection
                    lStationsLayer.SaveAs(aStationsShapeFilename, True)
                    lStationsLayer.Dispose()
                    .WriteFile(IO.Path.ChangeExtension(aStationsShapeFilename, ".dbf"))
                End With
            End If



            'Dim lLayer As New Layer(aStationsShapeFilename, BASINS.LayerSpecifications.MetStation, False)
            'aProject.Layers.Add(lLayer)
            '    lLayer.AddProcessStep("Created shapefile of met data stations")
            lResults &= "<add_shape>" & aStationsShapeFilename & "</add_shape>" & vbCrLf
        End If
        Return lResults
    End Function


    Private Function GetMetData(ByVal aProject As D4EM.Data.Project, aStationIDs As Generic.List(Of String), aMetWDM As String) As String
        Dim lResults As String = "<error>Did not complete download from NCDC</error>"
        Dim lToken As String = ""
        If Not D4EM.Data.Source.NCDC.HasToken() Then
            lToken = GetSetting(g_AppNameLong, "NCDC", "token", "")
            If Not String.IsNullOrEmpty(lToken) Then
                D4EM.Data.Source.NCDC.token = lToken
            End If
        End If
        Try
            lToken = D4EM.Data.Source.NCDC.token
            MapWinUtility.Logger.Dbg("Specified NCDC token '" & lToken & "'")
        Catch ex As Exception
            Dim lLinkStart As Integer = ex.Message.IndexOf("http:")
            If lLinkStart > 0 Then
                Dim lWebsiteButton As String = "Visit NCDC Token Website"
                If MapWinUtility.Logger.MsgCustom(ex.Message, "NCDC Token", "Ok", lWebsiteButton) = lWebsiteButton Then
                    OpenFile(ex.Message.Substring(lLinkStart))
                End If
                'Else
                '    MapWinUtility.Logger.Msg("NCDC token must be specified to use NCDC met data" & vbCrLf & ex.ToString, MsgBoxStyle.Exclamation, "NCDC Token")
            End If
            lToken = InputBox("Enter NCDC Token")
            If Not String.IsNullOrEmpty(lToken) Then
                D4EM.Data.Source.NCDC.token = lToken
            End If
        End Try

        If D4EM.Data.Source.NCDC.HasToken() Then
            Logger.Status("Getting NCDC Met Data", True)
            Dim NCDCconstituents As New Generic.List(Of String)
            NCDCconstituents.AddRange("AA1 TMP DEW GF1 WND".Split(" "))

            For Each lNCDCStationID As String In aStationIDs
TryNCDCvalues:
                Try
                    Dim lMetDataFolder As String = IO.Path.Combine(aProject.ProjectFolder, "met")
                    If String.IsNullOrEmpty(aMetWDM) Then
                        aMetWDM = IO.Path.Combine(lMetDataFolder, "met.wdm")
                    End If

                    lResults = D4EM.Data.Source.NCDC.GetClosestSitesWithData(aProject, D4EM.Data.Source.NCDC.LayerSpecifications.ISH, NCDCconstituents,
                                              New Date(1800, 1, 1),
                                              New Date(2100, 12, 31), 1)

                    'lResults = D4EM.Data.Source.NCDC.GetDataFromStation(aProject, lNCDCStationID, NCDCconstituents,
                    '                          New Date(1800, 1, 1),
                    '                          New Date(2100, 12, 31))

                    ''Read in the NCDC data
                    'Logger.Status("Reading NCDC data")
                    'Dim lRawDataGroup As atcTimeseriesGroup = D4EM.Data.MetCmp.ReadData(lMetDataFolder, 1)
                    'If lRawDataGroup.Count > 0 Then
                    '    Dim lPREC As atcTimeseries = Nothing
                    '    Dim lATEM As atcTimeseries = Nothing
                    '    Dim lSOLR As atcTimeseries = Nothing
                    '    Dim lWIND As atcTimeseries = Nothing
                    '    Dim lPEVT As atcTimeseries = Nothing
                    '    Dim lDEWP As atcTimeseries = Nothing
                    '    Dim lCLOU As atcTimeseries = Nothing

                    '    'Process NCDC data: fill missing, generate constistuents, etc.
                    '    Logger.Status("Processing NCDC data")
                    '    D4EM.Data.MetCmp.MetDataProcess(lRawDataGroup, lMetDataFolder, lPREC, lATEM, lWIND, lSOLR, lPEVT, lDEWP, lCLOU)

                    '    aProject.TimeseriesSources.Add(D4EM.Model.HSPF.CreateMetWDM(aMetWDM, lPREC, lATEM, lWIND, lSOLR, lPEVT, lDEWP, lCLOU))
                    '    lResults = "<add_data type='Timeseries::WDM'>" & aMetWDM & "</add_data>"
                    'End If
                Catch ex As ApplicationException
                    If ex.Message = "Retry Query" Then
                        GoTo TryNCDCvalues
                    Else
                        Throw ex
                    End If
                End Try
            Next
        End If
        Return lResults
    End Function

    Public Function ExecuteNCDC(ByVal aQuery As String) As String
        Dim lFunctionName As String
        Dim lQuery As New Xml.XmlDocument
        Dim lNode As Xml.XmlNode
        Dim lError As String = ""
        Dim lResult As String = ""
        Try
            lQuery.LoadXml(aQuery)
            lNode = lQuery.FirstChild
            If lNode.Name.ToLower = "function" Then
                lFunctionName = lNode.Attributes.GetNamedItem("name").Value.ToLower
                Select Case lFunctionName
                    Case "getncdc"
                        Dim lDataTypeString As String = ""
                        Dim lDataTypes As Generic.List(Of D4EM.Data.LayerSpecification) = Nothing
                        Dim lStationIDs As Generic.List(Of String) = Nothing
                        Dim lCacheFolder As String = ""
                        Dim lSaveIn As String = ""
                        Dim lMetWDM As String = ""
                        Dim lRegion As D4EM.Data.Region = Nothing
                        Dim lClip As Boolean = False
                        Dim lMerge As Boolean = False
                        Dim lGetEvenIfCached As Boolean = False
                        Dim lCacheOnly As Boolean = False
                        Dim lGetMetStations As Boolean = False
                        Dim lGetMetData As Boolean = False

                        If GetLayerArgs(lNode.FirstChild,
                                        lDataTypes,
                                        lStationIDs,
                                        lGetMetStations, lGetMetData,
                                        lCacheFolder, lSaveIn, lMetWDM,
                                        lRegion,
                                        lClip,
                                        lMerge,
                                        lGetEvenIfCached,
                                        lCacheOnly) Then
                            Dim lProject As New D4EM.Data.Project(D4EM.Data.Globals.GeographicProjection,
                                                                  lCacheFolder,
                                                                  lSaveIn,
                                                                  lRegion,
                                                                  lClip, lMerge,
                                                                  lGetEvenIfCached,
                                                                  lCacheOnly)
                            If lGetMetStations Then
                                lResult &= GetMetStations(lProject, D4EM.Data.Source.NCDC.LayerSpecifications.ISH, lStationIDs, True, Nothing, Nothing, Nothing)
                            End If
                            If lGetMetData Then
                                lResult &= GetMetData(lProject, lStationIDs, lMetWDM)
                            End If
                        End If
                    Case Else
                        lError = "Unknown function: " & lFunctionName
                End Select
            Else
                lError = "Cannot yet handle query that does not start with a function"
            End If
        Catch ex As Exception
            lError = ex.Message
        End Try
        If lError.Length = 0 Then
            If lResult.Length > 0 Then
                Return "<success>" & lResult & "</success>"
            Else
                Return "<success />"
            End If
        Else
            Logger.Dbg("Error downloading BASINS", aQuery, lError)
            Return "<error>" & lError & "</error>"
        End If
    End Function

    Private Function GetLayerArgs(ByVal aArgs As Xml.XmlNode,
                                  ByRef aDataTypes As Generic.List(Of D4EM.Data.LayerSpecification),
                                  ByRef aStationIDs As Generic.List(Of String),
                                  ByRef aGetMetStations As Boolean,
                                  ByRef aGetMetData As Boolean,
                                  ByRef aCacheFolder As String,
                                  ByRef aSaveIn As String,
                                  ByRef aMetWDM As String,
                                  ByRef aRegion As D4EM.Data.Region,
                                  ByRef aClip As Boolean,
                                  ByVal aMerge As Boolean,
                                  ByRef aGetEvenIfCached As Boolean,
                                  ByRef aCacheOnly As Boolean) As Boolean

        Dim lArg As Xml.XmlNode = aArgs.FirstChild

        aStationIDs = New Generic.List(Of String)
        aDataTypes = New Generic.List(Of D4EM.Data.LayerSpecification)

        While Not lArg Is Nothing
            Dim lArgName As String = lArg.Name
            Select Case lArgName.ToLower
                Case "region"
                    Try
                        aRegion = New D4EM.Data.Region(lArg)
                    Catch e As Exception
                        Logger.Dbg("Exception reading Region from query: " & e.Message)
                    End Try
                Case "cachefolder" : aCacheFolder = lArg.InnerText
                Case "getevenifcached" : If Not lArg.InnerText.ToLower.Contains("false") Then aGetEvenIfCached = True
                Case "cacheonly" : If Not lArg.InnerText.ToLower.Contains("false") Then aCacheOnly = True
                Case "stationid" : If Not aStationIDs.Contains(lArg.InnerText) Then aStationIDs.Add(lArg.InnerText)
                Case "savein" : aSaveIn = lArg.InnerText
                Case "savewdm" : aMetWDM = lArg.InnerText
                Case "datatype"
                    Dim lDataTypeString As String = lArg.InnerText.ToLower
                    Select Case lDataTypeString
                        Case "metstations" : aGetMetStations = True
                        Case "metdata" : aGetMetData = True
                    End Select
            End Select
            lArg = lArg.NextSibling
        End While

        If aSaveIn.Length = 0 AndAlso aMetWDM.Length = 0 Then
            Throw New Exception("BASINS data: No destination to save as")
        End If

        Return True
    End Function

End Module
