Imports MapWinUtility
Imports atcUtility

Partial Class NHDPlus
    Inherits SourceBase

    Public Overrides ReadOnly Property Name() As String
        Get
            Return "D4EM Data Download::NHDPlus"
        End Get
    End Property

    Public Overrides ReadOnly Property Description() As String
        Get
            Return "Retrieve NHD Plus data"
        End Get
    End Property

    Public Overrides ReadOnly Property QuerySchema() As String
        Get
            Dim lFileName As String = "NHDPlusQuerySchema.xml"
            Return GetEmbeddedFileAsString(lFileName, "D4EM.Data.Source." & lFileName)
          End Get
    End Property

    ''' <summary>
    ''' Download NHDPlus data
    ''' </summary>
    ''' <param name="aQuery">XML following QuerySchema describing data to download</param>
    ''' <returns>XML describing success or error</returns>
    Public Overrides Function Execute(ByVal aQuery As String) As String
        Dim lFunctionName As String
        Dim lQuery As New Xml.XmlDocument
        Dim lNode As Xml.XmlNode
        Dim lError As String = ""
        Dim lResult As String = ""
        Dim lVersion As Integer = 1
        Try
            lQuery.LoadXml(aQuery)
            lNode = lQuery.FirstChild
            If lNode.Name.ToLower = "function" Then
                lFunctionName = lNode.Attributes.GetNamedItem("name").Value.ToLower
                If lFunctionName = "getnhdplus2" Then
                    lFunctionName = "getnhdplus"
                    lVersion = 2
                End If
                Select Case lFunctionName
                    Case "getnhdplus"
                        Dim lHUC8s As Generic.List(Of String) = Nothing
                        Dim lDataTypes() As LayerSpecification = Nothing
                        Dim lDesiredProjection As String = ""
                        Dim lCacheFolder As String = ""
                        Dim lSaveIn As String = ""
                        Dim lRegion As Region = Nothing
                        Dim lClip As Boolean = False
                        Dim lMerge As Boolean = False
                        Dim lMergeAddedAttributes As Boolean = False
                        Dim lGetEvenIfCached As Boolean = False

                        If GetLayerArgs(lNode.FirstChild, lHUC8s, lDataTypes, _
                                        lDesiredProjection, lCacheFolder, lSaveIn, _
                                        lRegion, _
                                        lClip, _
                                        lMerge, _
                                        lMergeAddedAttributes, _
                                        lGetEvenIfCached) Then

                            For Each lHUC8 As String In lHUC8s
                                Dim lTempD4EMProject = New D4EM.Data.Project(Globals.FromProj4(lDesiredProjection),
                                                                            lCacheFolder, lSaveIn,
                                                                            lRegion,
                                                                            lClip,
                                                                            lMerge,
                                                                            lGetEvenIfCached)
                                If lVersion = 1 Then
                                    lResult &= GetNHDPlus(lTempD4EMProject,
                                                      "NHDPlus",
                                                      lHUC8,
                                                      lMergeAddedAttributes,
                                                      lDataTypes)
                                ElseIf lVersion = 2 Then
                                    lResult &= GetNHDPlus2(lTempD4EMProject,
                                                      "NHDPlus2",
                                                      lHUC8,
                                                      lMergeAddedAttributes,
                                                      lDataTypes)
                                End If
                            Next
                        End If
                    Case Else
                        lError = "Unknown function: " & lFunctionName
                End Select
            Else
                lError = "Cannot yet handle query that does not start with a function" & vbCrLf & _
                         "Supplied query started with '" & lNode.Name & "'"
            End If
        Catch ex As Exception
            Dim lErrMsg As String = "Exception: " & ex.Message
            Try
                lErrMsg &= " Stack Trace: " & ex.StackTrace
            Catch
            End Try
            Logger.Dbg(lErrMsg)
            lError = lErrMsg
        End Try
        If lError.Length = 0 Then
            If lResult.Length > 0 Then
                Return "<success>" & lResult & "</success>"
            Else
                Return "<success />"
            End If
        Else
            Logger.Dbg("Error downloading NHDPlus", aQuery, lError)
            Return "<error>" & lError & "</error>"
        End If
    End Function

    Private Function GetLayerArgs(ByVal aArgs As Xml.XmlNode, _
                                  ByRef aHUCs As Generic.List(Of String), _
                                  ByRef aDataTypes() As LayerSpecification, _
                                  ByRef aDesiredProjection As String, _
                                  ByRef aCacheFolder As String, _
                                  ByRef aSaveIn As String, _
                                  ByRef aRegion As Region, _
                                  ByRef aClip As Boolean, _
                                  ByRef aMerge As Boolean, _
                                  ByRef aJoinAttributes As Boolean, _
                                  ByRef aGetEvenIfCached As Boolean) As Boolean

        Dim FoundType As Boolean = False
        Dim lArg As Xml.XmlNode = aArgs.FirstChild
        aClip = False
        aHUCs = New Generic.List(Of String)
        Dim lDataTypes As New Generic.List(Of LayerSpecification)

        While Not lArg Is Nothing
            Dim lArgName As String = lArg.Name
            Dim lNameAttribute As Xml.XmlAttribute = lArg.Attributes.GetNamedItem("name")
            If Not lNameAttribute Is Nothing Then lArgName = lNameAttribute.Value
            Select Case lArgName.ToLower
                Case "region"
                    Try
                        aRegion = New Region(lArg)
                    Catch e As Exception
                        Logger.Dbg("Exception reading Region from query: " & e.Message)
                    End Try
                Case "huc8" : aHUCs.Add(lArg.InnerText)
                Case "desiredprojection" : aDesiredProjection = lArg.InnerText
                Case "datatype"
                    Dim lDataTypeString As String = lArg.InnerText.ToLower
                    Select Case lDataTypeString
                        Case "hydrography"
                            lDataTypes.Add(LayerSpecifications.Hydrography.Area)
                            lDataTypes.Add(LayerSpecifications.Hydrography.Flowline)
                            lDataTypes.Add(LayerSpecifications.Hydrography.Line)
                            lDataTypes.Add(LayerSpecifications.Hydrography.Point)
                            lDataTypes.Add(LayerSpecifications.Hydrography.Waterbody)
                        Case Else
                            Dim lLayerSpec = LayerSpecification.FromTag(lDataTypeString, GetType(LayerSpecifications))
                            If lLayerSpec IsNot Nothing Then
                                lDataTypes.Add(lLayerSpec)
                            Else
                                Logger.Dbg(Name & " requested data type not recognized: " & lArg.InnerText)
                            End If
                    End Select
                    '                    Select Case lDataTypeString
                    '                        Case "all" : lDataTypes.Add(LayerSpecifications.all)
                    '                        Case "allgrid"
                    '                            lDataTypes.Add(LayerSpecifications.ElevationGrid)
                    '                            lDataTypes.Add(LayerSpecifications.SlopeGrid)
                    '                            lDataTypes.Add(LayerSpecifications.FlowAccumulationGrid)
                    '                            lDataTypes.Add(LayerSpecifications.FlowDirectionGrid)
                    '                            lDataTypes.Add(LayerSpecifications.CatchmentGrid)
                    '                        Case "elev_cm" : lDataTypes.Add(LayerSpecifications.ElevationGrid)
                    '                        Case "fac" : lDataTypes.Add(LayerSpecifications.FlowAccumulationGrid)
                    '                        Case "fdr" : lDataTypes.Add(LayerSpecifications.FlowDirectionGrid)
                    '                            'Case "allshape"
                    '                            'Case "basin" : lDataTypes.Add(LayerSpecifications.)
                    '                        Case "catchment" : lDataTypes.Add(LayerSpecifications.CatchmentGrid)
                    '                        Case "nhdarea" : lDataTypes.Add(LayerSpecifications.Hydrography.Area)
                    '|NHDFlowline|NHDLine|NHDPoint|NHDWaterbody|Region|streamgageevent|SubBasin|Subregion|Subwatershed|Watershed|Drainage|hydrography|hydrologicunits" : lDataTypes.Add(LayerSpecifications.all)

                    '                        Case "" : lDataTypes.Add(LayerSpecifications.Hydrography.Area)
                    '                                Name:="NHDPlus Area", FilePattern:="nhdarea.shp", Tag:="nhdarea", Role:=Roles.Hydrography, IdFieldName:="COMID", Source:=GetType(NHDPlus))
                    '                        Case "" : lDataTypes.Add(LayerSpecifications.Hydrography.Flowline)
                    '                                Name:="NHDPlus Flowline", FilePattern:="nhdflowline.shp", Tag:="nhdflowline", Role:=Roles.Hydrography, IdFieldName:="COMID", Source:=GetType(NHDPlus))
                    '                        Case "" : lDataTypes.Add(LayerSpecifications.Hydrography.Line)
                    '                                Name:="NHDPlus Line", FilePattern:="nhdline.shp", Tag:="nhdline", Role:=Roles.Hydrography, IdFieldName:="COMID", Source:=GetType(NHDPlus))
                    '                        Case "" : lDataTypes.Add(LayerSpecifications.Hydrography.Point)
                    '                                Name:="NHDPlus Point", FilePattern:="nhdpoint.shp", Tag:="nhdpoint", Role:=Roles.Hydrography, IdFieldName:="COMID", Source:=GetType(NHDPlus))
                    '                        Case "" : lDataTypes.Add(Hydrography.Waterbody)
                    '                                Name:="NHDPlus Waterbody", FilePattern:="nhdwaterbody.shp", Tag:="nhdwaterbody", Role:=Roles.Hydrography, IdFieldName:="COMID", Source:=GetType(NHDPlus))
                    '                        Case "" : lDataTypes.Add(HydrologicUnits.RegionPolygons)
                    '        Name:="NHDPlus Region", FilePattern:="region.shp", Tag:="region", Role:=Roles.SubBasin, Source:=Reflection.MethodInfo.GetCurrentMethod.DeclaringType)
                    '                        Case "" : lDataTypes.Add(HydrologicUnits.SubBasinPolygons)
                    '        Name:="NHDPlus Subbasin", FilePattern:="subbasin.shp", Tag:="subbasin", Role:=Roles.SubBasin, Source:=GetType(NHDPlus))
                    '                        Case "" : lDataTypes.Add(HydrologicUnits.SubWatershedPolygons)
                    '        Name:="NHDPlus Subwatershed", FilePattern:="subwatershed.shp", Tag:="subwatershed", Role:=Roles.SubBasin, Source:=GetType(NHDPlus))
                    '                        Case "" : lDataTypes.Add(HydrologicUnits.WatershedPolygons)
                    '        Name:="NHDPlus Watershed", FilePattern:="watershed.shp", Tag:="watershed", Role:=Roles.SubBasin, Source:=GetType(NHDPlus))

                    'Public Shared CatchmentPolygons As LayerSpecification = D4EM.Data.Region.RegionTypes.catchment
                    'Public Shared CatchmentGrid As New LayerSpecification(
                    '    Name:="NHDPlus Catalog Unit Grid", FilePattern:="cat.tif", Tag:="cat", Role:=Roles.SubBasin, Source:=GetType(NHDPlus))
                    'Public Shared ElevationGrid As New LayerSpecification(
                    '    Name:="NHDPlus Elevation", FilePattern:="elev_cm.tif", Tag:="elev_cm", Role:=Roles.Elevation, Source:=GetType(NHDPlus))
                    'Public Shared SlopeGrid As New LayerSpecification(
                    '    Name:="NHDPlus Slope Grid", FilePattern:="slope.tif", Tag:="slope", Role:=Roles.Slope, Source:=GetType(NHDPlus))
                    'Public Shared FlowAccumulationGrid As New LayerSpecification(
                    '    Name:="NHDPlus Flow Accumulation Grid", FilePattern:="fac.tif", Tag:="fac", Source:=GetType(NHDPlus))
                    'Public Shared FlowDirectionGrid As New LayerSpecification(
                    '    Name:="NHDPlus Flow Direction Grid", FilePattern:="fdr.tif", Tag:="fdr", Source:=GetType(NHDPlus))
                    'End Select
                Case "cachefolder" : aCacheFolder = lArg.InnerText
                Case "getevenifcached" : If Not lArg.InnerText.ToLower.Contains("false") Then aGetEvenIfCached = True
                Case "savein", "saveas" : aSaveIn = lArg.InnerText
                Case "clip" : If Not lArg.InnerText.ToLower.Contains("false") Then aClip = True
                Case "merge" : If Not lArg.InnerText.ToLower.Contains("false") Then aMerge = True
                Case "joinattributes" : If Not lArg.InnerText.ToLower.Contains("false") Then aJoinAttributes = True
            End Select
            lArg = lArg.NextSibling
        End While

        If aHUCs.Count = 0 Then
            aHUCs = aRegion.GetKeys(Region.RegionTypes.huc8)
        End If

        If aSaveIn.Length = 0 Then
            Throw New Exception("No destination to save in")
        End If
        If lDataTypes.Count > 0 Then
            aDataTypes = lDataTypes.ToArray()
        End If
        Return True
    End Function

End Class
