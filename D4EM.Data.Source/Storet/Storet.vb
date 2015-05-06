Imports System.IO
Imports System.Reflection
Imports System.Xml
Imports atcUtility
Imports MapWinUtility
Imports D4EM.Geo

Public Class Storet
    Public Shared StoretStation As New LayerSpecification(Tag:="storet", Role:=D4EM.Data.LayerSpecification.Roles.Station, Source:=GetType(Storet))

    <CLSCompliant(False)> _
    Public Function GetSTORET(ByVal aDataTypes As ArrayList, _
                              ByVal aStationIDs As atcCollection, _
                              ByVal aDesiredProjection As DotSpatial.Projections.ProjectionInfo, _
                              ByVal aCacheFolder As String, _
                              ByVal aSaveIn As String, _
                              ByVal aRegion As Region, _
                              ByVal aClip As Boolean, _
                              ByVal aGetEvenIfCached As Boolean) As String


        If Not aSaveIn.ToUpper.Contains(pName) Then aSaveIn = IO.Path.Combine(aSaveIn, pName)

        GetSTORET = ""
        If aDataTypes.Contains("stations") OrElse _
          (aDataTypes.Contains("results") AndAlso aStationIDs.Count = 0) Then
            GetSTORET &= GetSTORETstations(aStationIDs, aDesiredProjection, aCacheFolder, aSaveIn, aRegion, aClip, aGetEvenIfCached)
        End If

        If aDataTypes.Contains("results") Then
            GetSTORET &= GetSTORETresults(aStationIDs, aDesiredProjection, aCacheFolder, aSaveIn, aRegion, aClip, aGetEvenIfCached)
        End If

        Logger.Status("")

    End Function

    Private Function GetSTORETstations( _
                              ByVal aStationIDs As atcCollection, _
                              ByVal aDesiredProjection As DotSpatial.Projections.ProjectionInfo, _
                              ByVal aCacheFolder As String, _
                              ByVal aSaveIn As String, _
                              ByVal aRegion As Region, _
                              ByVal aClip As Boolean, _
                              ByVal aGetEvenIfCached As Boolean) As String
        Dim lAddStations As Boolean = False '(aStationIDs.Count = 0) 'Setting this True when no stations were selected selects all stations. Disabled because too many stations, too slow, unable to abort

        Dim lNorth, lSouth, lWest, lEast As Double
        aRegion.GetBounds(lNorth, lSouth, lWest, lEast, D4EM.Data.Globals.GeographicProjection)
        Dim lBaseFilename As String = pName & "stations" _
                                    & "_N_" & DoubleToString(lNorth) _
                                    & "_S_" & DoubleToString(lSouth) _
                                    & "_W_" & DoubleToString(lWest) _
                                    & "_E_" & DoubleToString(lEast)

        Dim lCacheStations As String = IO.Path.Combine(IO.Path.Combine(aCacheFolder, pName), lBaseFilename)
        Dim lTempFolder As String = NewTempDir(pName & "_Temp")
        Dim lShapeFilename As String = lTempFolder & pName & ".shp"
        Dim lStationsNode As XmlNode
        Dim lNode As XmlNode
        Dim lNodeChild As XmlNode

        If aGetEvenIfCached OrElse FileExists(lCacheStations) AndAlso FileLen(lCacheStations) < 500 Then
            TryDelete(lCacheStations)
        End If

        Dim lStationsText As String
        If FileExists(lCacheStations) Then
            lStationsText = IO.File.ReadAllText(lCacheStations)
        Else
            Logger.Status("Retrieving " & pName & " stations", True)
            Dim lStationService As New StationService.StationServiceClient
            lStationsText = lStationService.getStationsForMap(lSouth, lNorth, lWest, lEast)
            SaveFileString(lCacheStations, lStationsText)
            Dim lMetadata As New Metadata(lCacheStations & ".xml")
            lMetadata.AddProcessStep("Downloaded from STORET via getStationsForMap(" & lSouth & ", " & lNorth & ", " & lWest & ", " & lEast & ")")
            lMetadata.Save()
        End If

        Dim lDocument As New XmlDocument
        lDocument.LoadXml(lStationsText)
        lStationsNode = lDocument.ChildNodes(1)

        MkDirPath(PathNameOnly(lShapeFilename))
        TryDeleteShapefile(lShapeFilename)

        Dim lNewShapefile As New DotSpatial.Data.FeatureSet(DotSpatial.Topology.FeatureType.Point)
        If IO.File.Exists(lCacheStations & ".xml") Then
            IO.File.Copy(lCacheStations & ".xml", lShapeFilename & ".xml")
        End If
        lNewShapefile.Projection = D4EM.Data.Globals.GeographicProjection

        Dim lLastField As Integer = pShapeFieldnames.GetUpperBound(0)
        Dim lCurField As Integer
        For lCurField = 0 To lLastField
            Dim lNewField As DotSpatial.Data.Field
            If pShapeFieldnames(lCurField).EndsWith("itude") Then
                lNewField = New DotSpatial.Data.Field(pShapeFieldnames(lCurField), DotSpatial.Data.FieldDataType.Double)
            Else
                lNewField = New DotSpatial.Data.Field(pShapeFieldnames(lCurField), DotSpatial.Data.FieldDataType.String)
            End If
            lNewField.Length = pShapeFieldWidths(lCurField)
            lNewShapefile.DataTable.Columns.Add(lNewField)
        Next

        Logger.Status("Creating STORET station layer")

        For Each lOrganizationNode As XmlNode In lStationsNode.ChildNodes
            Dim lOrganizationIdentifier As String = ""
            Dim lOrganizationFormalName As String = ""
            For Each lOrgChild As XmlNode In lOrganizationNode.ChildNodes
                Select Case lOrgChild.Name
                    Case "OrganizationDescription"
                        For Each lNode In lOrgChild.ChildNodes
                            Select Case lNode.Name
                                Case "OrganizationIdentifier" : lOrganizationIdentifier = lNode.InnerText.Trim
                                Case "OrganizationFormalName" : lOrganizationFormalName = lNode.InnerText.Trim
                            End Select
                        Next

                    Case "MonitoringLocation"
                        Dim lMonitoringLocationIdentifier As String = ""
                        Dim lMonitoringLocationName As String = ""
                        Dim lMonitoringLocationTypeName As String = ""
                        Dim lLatitudeMeasure As String = ""
                        Dim lLongitudeMeasure As String = ""
                        For Each lNode In lOrgChild.ChildNodes
                            For Each lNodeChild In lNode.ChildNodes
                                Select Case lNodeChild.Name
                                    Case "MonitoringLocationIdentifier" : lMonitoringLocationIdentifier = lNodeChild.InnerText.Trim
                                    Case "MonitoringLocationName" : lMonitoringLocationName = lNodeChild.InnerText.Trim
                                    Case "MonitoringLocationTypeName" : lMonitoringLocationTypeName = lNodeChild.InnerText.Trim
                                    Case "LatitudeMeasure" : lLatitudeMeasure = lNodeChild.InnerText.Trim
                                    Case "LongitudeMeasure" : lLongitudeMeasure = lNodeChild.InnerText.Trim
                                End Select
                            Next
                        Next
                        If IsNumeric(lLatitudeMeasure) AndAlso IsNumeric(lLongitudeMeasure) Then
                            Dim lCoordinate As New DotSpatial.Topology.Coordinate(Double.Parse(lLongitudeMeasure), Double.Parse(lLatitudeMeasure))
                            Dim lPoint As New DotSpatial.Topology.Point(lCoordinate)
                            Dim lFeature As DotSpatial.Data.IFeature = lNewShapefile.AddFeature(lPoint)

                            lCurField = 0
                            lFeature.DataRow(lCurField) = lOrganizationIdentifier : lCurField += 1
                            lFeature.DataRow(lCurField) = lOrganizationFormalName : lCurField += 1
                            lFeature.DataRow(lCurField) = lMonitoringLocationIdentifier : lCurField += 1
                            lFeature.DataRow(lCurField) = lMonitoringLocationName : lCurField += 1
                            lFeature.DataRow(lCurField) = lMonitoringLocationTypeName : lCurField += 1
                            lFeature.DataRow(lCurField) = lLatitudeMeasure : lCurField += 1
                            lFeature.DataRow(lCurField) = lLongitudeMeasure : lCurField += 1

                            If lAddStations Then aStationIDs.Add(lMonitoringLocationIdentifier)
                        End If
                End Select
            Next
        Next

        lNewShapefile.Filename = lShapeFilename
        lNewShapefile.Save()
        lNewShapefile.Close()
        Dim lLayer As New D4EM.Data.Layer(lShapeFilename, StoretStation, False)
        If aClip AndAlso aRegion IsNot Nothing Then
            Dim lClipFolder As String = ""
            lClipFolder = IO.Path.Combine(IO.Path.GetTempPath, pName & "_clip")
            TryDelete(lClipFolder)
            SpatialOperations.ProjectAndClipShapeLayer(lShapeFilename, StoretStation, D4EM.Data.Globals.GeographicProjection, aDesiredProjection, aRegion, lClipFolder)
            GetSTORETstations = SpatialOperations.MergeLayers(New Generic.List(Of Layer) From {lLayer}, lClipFolder, aSaveIn)
            TryDelete(lClipFolder)
        Else
            SpatialOperations.ProjectAndClipShapeLayer(lShapeFilename, StoretStation, D4EM.Data.Globals.GeographicProjection, aDesiredProjection, Nothing, Nothing)
            GetSTORETstations = SpatialOperations.MergeLayers(New Generic.List(Of Layer) From {lLayer}, lTempFolder, aSaveIn)
        End If
        TryDelete(lTempFolder)

    End Function

    Private Function GetSTORETresults( _
                              ByVal aStationIDs As atcCollection, _
                              ByVal aDesiredProjection As DotSpatial.Projections.ProjectionInfo, _
                              ByVal aCacheFolder As String, _
                              ByVal aSaveIn As String, _
                              ByVal aRegion As Region, _
                              ByVal aClip As Boolean, _
                              ByVal aGetEvenIfCached As Boolean) As String
        GetSTORETresults = ""
        Dim lNumRetrieved As Integer = 0
        If aStationIDs.Count = 0 Then
            GetSTORETresults &= "<message>Could not download STORET Results: STORET Stations must be selected before results retrieval</message>" & vbCrLf
            GetSTORETresults &= "<select_layer>STORET Stations</select_layer>" & vbCrLf
        Else
            Dim lStationsDBF As New atcTableDBF
            If lStationsDBF.OpenFile(IO.Path.Combine(aSaveIn, pName & ".dbf")) Then
                Dim lResultService As New StoretResultService.StoretResultServiceClient
                Dim lOrgIdfield As Integer = lStationsDBF.FieldNumber("OrgId")
                Dim lLocIdfield As Integer = lStationsDBF.FieldNumber("LocId")
                Dim lStationIDfield As Integer = lStationsDBF.FieldNumber("LocId")
                Dim lResultType As String = "regular" ' possible values are "regular", "biological" or "habitat"
                Dim lResultsNode As XmlNode = Nothing

                Dim lLocationIndex As Integer = 0
                Dim lLastIndex As Integer = aStationIDs.Count

                Logger.Status("Retrieving " & lLastIndex & " " & pName & " results", True)

                For Each lLocationID As String In aStationIDs
                    lLocationIndex += 1
                    Logger.Progress(lLocationIndex, lLastIndex)

                    Dim lCacheFilename As String = IO.Path.Combine(IO.Path.Combine(aCacheFolder, pName), lLocationID & "." & pName & "results")
                    Dim lSaveAs As String = IO.Path.Combine(aSaveIn, IO.Path.GetFileName(lCacheFilename))
                    If aGetEvenIfCached OrElse FileExists(lCacheFilename) AndAlso FileLen(lCacheFilename) < 500 Then
                        TryDelete(lCacheFilename)
                        TryDelete(lSaveAs)
                    End If
                    If Not FileExists(lSaveAs) Then
                        If Not FileExists(lCacheFilename) Then
                            If lStationsDBF.FindFirst(lLocIdfield, lLocationID) Then
                                Logger.Status("Retrieving " & pName & " " & lLocationID & " results", True)
                                Dim lOrgId As String = lStationsDBF.Value(lOrgIdfield)
                                Dim lResultsString As String = lResultService.getResults(lOrgId, lLocationID, "", "", "", "", "", "", "", "", "", lResultType)
                                SaveFileString(lCacheFilename, lResultsString)
                                Dim lMetadata As New Metadata(lCacheFilename & ".xml")
                                lMetadata.AddProcessStep("Downloaded from STORET via getResults(" & lOrgId & ", " & lLocationID & ",,,,,,,,,," & lResultType & ")")
                                lMetadata.Save()
                            Else
                                Logger.Dbg("Did not find station '" & lLocationID & "' in '" & lStationsDBF.FileName & "'")
                            End If
                        End If
                        If FileExists(lCacheFilename) Then
                            IO.File.Copy(lCacheFilename, lSaveAs)
                            IO.File.Copy(lCacheFilename & ".xml", lSaveAs & ".xml")
                        End If
                    End If
                    If IO.File.Exists(lSaveAs) Then
                        lNumRetrieved += 1
                        GetSTORETresults &= "<add_data type='STORET' subtype='results:" & lResultType & "'>" & lSaveAs & "</add_data>" & vbCrLf
                        '    Dim lDocument As New XmlDocument
                        '    lResultsNode = lDocument.ReadNode(New XmlTextReader(New IO.FileStream(lSaveAs, IO.FileMode.Open)))
                    End If
                Next
                'GetSTORETresults &= "<message>" & pName & ":Results from " & lNumRetrieved & "  " & pName & " Stations in '" & IO.Path.Combine(aCacheFolder, pName) & "'</message>" & vbCrLf
            Else
                GetSTORETresults &= "<message>" & pName & ":Could not open " & pName & " Stations '" & lStationsDBF.FileName & "'</message>" & vbCrLf
            End If
        End If
        'Dim lResultService As New StoretResultService.StoretResultService
        'For Each lLocationNode As XmlNode In lStationsNode.SelectNodes("MonitoringLocation")
        'Array.IndexOf(lShapeFieldnames, "OrgId")
        'Dim lResultNodes As XmlNode() = lResultService.getResults(ByVal OrganizationId As String, ByVal MonitoringLocationId As String, ByVal MonitoringLocationType As String, ByVal MinimumActivityStartDate As String, ByVal MaximumActivityStartDate As String, ByVal MinimumLatitude As String, ByVal MaximumLatitude As String, ByVal MinimumLongitude As String, ByVal MaximumLongitude As String, ByVal CharacteristicType As String, ByVal CharacteristicName As String, ByVal ResultType As String) As Object
        'GetSTORET &= 
        'Next

    End Function


    ''' <summary>
    ''' Download Storet stations within the specified region and save to a text RDB file
    ''' </summary>
    ''' <param name="aRegion"></param>
    ''' <param name="aSaveAs">File name to save downloaded station information in</param>
    ''' <param name="aParamList">field containing data to find stations</param>
    ''' <returns>True on successful download</returns>
    ''' <remarks></remarks>
    Public Shared Function GetStations(ByVal aRegion As Region, _
                                       ByVal aSaveAs As String, _
                                       ByVal aParamList As String, _
                                       ByVal aFileExt As String) As Boolean

        Dim mimeTypeVal As String = "mimeType=" & aFileExt

        Dim lURL As String = "http://storetnwis.epa.gov/storetqw/Station/search?" & aParamList & "&" & mimeTypeVal

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
                'If Not lStationsText.StartsWith("#") Then GoTo BadFile
                'IO.File.WriteAllText(aSaveAs, "# " & lURL & vbCrLf & lStationsText)
                If (aFileExt = "tab") Then
                    aFileExt = "tsv"
                End If
                IO.File.WriteAllText(aSaveAs & "." & aFileExt, lStationsText)
                'Layer.AddProcessStepToFile("Downloaded from " & lURL, aSaveAs)
            End If
            TryDelete(lSaveAsTemp)
            Return True
BadFile:
            If Logger.Msg("Storet Stations not downloaded - View error message?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                OpenFile(lSaveAsTemp)
            Else
                TryDelete(lSaveAsTemp)
            End If
        End If
        Return False
    End Function

    ''' <summary>
    ''' Download Storet stations within the specified region and save to a text RDB file
    ''' </summary>
    ''' <param name="aRegion"></param>
    ''' <param name="aSaveAs">File name to save downloaded station information in</param>
    ''' <param name="aParamList">field containing data to find stations</param>
    ''' <returns>True on successful download</returns>
    ''' <remarks></remarks>
    Public Shared Function GetResults(ByVal aRegion As Region, _
                                      ByVal aSaveAs As String, _
                                      ByVal aParamList As String, _
                                      ByVal aFileExt As String) As Boolean

        Dim mimeTypeVal As String = "mimeType=" & aFileExt

        Dim lURL As String = "http://storetnwis.epa.gov/storetqw/Result/search?" & aParamList & "&" & mimeTypeVal

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
                        Logger.Dbg("Exception reading result file record #" & lSection & ": " & lLine)
                    End Try
                Next
                Logger.Dbg("Kept " & lTotalKept & " in HUC out of " & lTotalDownloaded & " in box")
            Else
                Dim lStationsText As String = IO.File.ReadAllText(lSaveAsTemp)
                'If Not lStationsText.StartsWith("#") Then GoTo BadFile
                'IO.File.WriteAllText(aSaveAs, "# " & lURL & vbCrLf & lStationsText)
                If (aFileExt = "tab") Then
                    aFileExt = "tsv"
                End If
                IO.File.WriteAllText(aSaveAs & "." & aFileExt, lStationsText)
                'Layer.AddProcessStepToFile("Downloaded from " & lURL, aSaveAs)
            End If
            TryDelete(lSaveAsTemp)
            Return True
BadFile:
            If Logger.Msg("Storet Results not downloaded - View error message?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                OpenFile(lSaveAsTemp)
            Else
                TryDelete(lSaveAsTemp)
            End If
        End If
        Return False
    End Function

End Class
