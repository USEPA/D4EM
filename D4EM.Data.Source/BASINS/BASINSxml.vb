Imports MapWinUtility
Imports atcUtility
Imports D4EM.Data
Imports D4EM.Data.LayerSpecification
Imports D4EM.Geo

Partial Class BASINS
    Inherits SourceBase

    Public Overrides ReadOnly Property Name() As String
        Get
            Return "D4EM Data Download::BASINS"
        End Get
    End Property

    Public Overrides ReadOnly Property Description() As String
        Get
            Return "Retrieve BASINS data"
        End Get
    End Property

    Public Overrides ReadOnly Property QuerySchema() As String
        Get
            Dim lFileName As String = "BASINSQuerySchema.xml"
            Return GetEmbeddedFileAsString(lFileName, "D4EM.Data.Source." & lFileName)
        End Get
    End Property

    Public Overrides Function Execute(ByVal aQuery As String) As String
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
                    Case "getbasins"
                        Dim lHUC8s As Generic.List(Of String) = Nothing
                        Dim lDataTypeString As String = ""
                        Dim lDataTypes As Generic.List(Of D4EM.Data.LayerSpecification) = Nothing
                        Dim lStationIDs As Generic.List(Of String) = Nothing
                        Dim lDesiredProjection As String = ""
                        Dim lCacheFolder As String = ""
                        Dim lSaveIn As String = ""
                        Dim lMetWDM As String = ""
                        'Dim lClip As MapWinGIS.Shape = Nothing
                        Dim lRegion As D4EM.Data.Region = Nothing
                        Dim lClip As Boolean = False
                        Dim lMerge As Boolean = False
                        Dim lGetEvenIfCached As Boolean = False
                        Dim lCacheOnly As Boolean = False
                        Dim lGetMetStations As Boolean = False
                        Dim lGetMetData As Boolean = False
                        Dim lGetSTATSGO As Boolean = False

                        If GetLayerArgs(lNode.FirstChild, _
                                        lHUC8s, lDataTypes, lStationIDs, lGetMetStations, lGetMetData, lGetSTATSGO, _
                                        lDesiredProjection, _
                                        lCacheFolder, lSaveIn, lMetWDM, _
                                        lRegion, _
                                        lClip, _
                                        lMerge, _
                                        lGetEvenIfCached, _
                                        lCacheOnly) Then
                            Dim lProject As New D4EM.Data.Project(Globals.FromProj4(lDesiredProjection), _
                                                                  lCacheFolder, _
                                                                  lSaveIn, _
                                                                  lRegion, _
                                                                  lClip, lMerge,
                                                                  lGetEvenIfCached, _
                                                                  lCacheOnly)
                            Dim lNumDone As Integer = 0
                            Dim lNumToDo As Integer = lHUC8s.Count * lDataTypes.Count
                            If lGetMetStations OrElse lGetMetData Then lNumToDo += 1
                            If lGetSTATSGO Then lNumToDo += 1
                            Dim lMultipleItems As Boolean = (lNumToDo > 1)
                            If lMultipleItems Then
                                Logger.Status("Getting " & lNumToDo & " BASINS Data Sets")
                                Logger.Progress(lNumDone, lNumToDo)
                            End If
                            If lGetMetStations OrElse lGetMetData Then
                                If lMultipleItems Then Logger.Status("Label Middle BASINS Meterologic")
                                Using lLevel As New ProgressLevel(lMultipleItems, Not lMultipleItems)
                                    If lMultipleItems Then Logger.Status("BASINS Meterologic")
                                    If lGetMetStations Then
                                        lResult &= GetMetStations(lProject, lStationIDs, True, Nothing, Nothing, Nothing)
                                    End If
                                    If lGetMetData Then
                                        lResult &= GetMetData(lProject, lStationIDs, lMetWDM)
                                    End If

                                End Using
                            End If
                            If lGetSTATSGO Then
                                If lMultipleItems Then Logger.Status("Label Middle STATSGO")
                                Using lLevel As New ProgressLevel(lMultipleItems, Not lMultipleItems)
                                    If lMultipleItems Then Logger.Status("STATSGO")
                                    lResult &= GetBASINSSTATSGO(lProject)
                                End Using
                            End If
                            For Each lHUC8 As String In lHUC8s
                                For Each lDataType As LayerSpecification In lDataTypes
                                    If lMultipleItems Then Logger.Status("Label Middle " & lHUC8 & " " & lDataType.ToString)
                                    Using lLevel As New ProgressLevel(lMultipleItems, Not lMultipleItems)
                                        If lMultipleItems Then Logger.Status(lHUC8 & " " & lDataType.ToString)
                                        lResult &= GetBASINS(lProject, Nothing, lHUC8, lDataType)
                                    End Using
                                Next
                            Next
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

    Private Function GetLayerArgs(ByVal aArgs As Xml.XmlNode, _
                                  ByRef aHUC8s As Generic.List(Of String), _
                                  ByRef aDataTypes As Generic.List(Of D4EM.Data.LayerSpecification), _
                                  ByRef aStationIDs As Generic.List(Of String), _
                                  ByRef aGetMetStations As Boolean, _
                                  ByRef aGetMetData As Boolean, _
                                  ByRef aGetSTATSGO As Boolean, _
                                  ByRef aDesiredProjection As String, _
                                  ByRef aCacheFolder As String, _
                                  ByRef aSaveIn As String, _
                                  ByRef aMetWDM As String, _
                                  ByRef aRegion As Region, _
                                  ByRef aClip As Boolean, _
                                  ByVal aMerge As Boolean, _
                                  ByRef aGetEvenIfCached As Boolean, _
                                  ByRef aCacheOnly As Boolean) As Boolean

        Dim lArg As Xml.XmlNode = aArgs.FirstChild

        aStationIDs = New Generic.List(Of String)
        aHUC8s = New Generic.List(Of String)
        aDataTypes = New Generic.List(Of D4EM.Data.LayerSpecification)

        While Not lArg Is Nothing
            Dim lArgName As String = lArg.Name
            'Dim lNameAttribute As Xml.XmlAttribute = lArg.Attributes.GetNamedItem("name")
            'If Not lNameAttribute Is Nothing Then lArgName = lNameAttribute.Value
            Select Case lArgName.ToLower
                Case "region"
                    Try
                        aRegion = New Region(lArg)
                    Catch e As Exception
                        Logger.Dbg("Exception reading Region from query: " & e.Message)
                    End Try
                Case "huc8" : aHUC8s.Add(lArg.InnerText)
                Case "datatype"
                    Dim lDataTypeString As String = lArg.InnerText.ToLower
                    Select Case lDataTypeString
                        Case "met" : aGetMetStations = True : aGetMetData = True
                        Case "metstations" : aGetMetStations = True
                        Case "metdata" : aGetMetData = True
                        Case "statsgo" : aGetSTATSGO = True
                        Case Else
                            Select Case lArg.InnerText.ToLower
                                Case LayerSpecifications.Census.all.Tag.ToLower : aDataTypes.Add(LayerSpecifications.Census.all)
                                Case LayerSpecifications.core31.all.Tag.ToLower : aDataTypes.Add(LayerSpecifications.core31.all)
                                Case LayerSpecifications.dem.Tag.ToLower : aDataTypes.Add(LayerSpecifications.dem)
                                Case LayerSpecifications.DEMG.Tag.ToLower : aDataTypes.Add(LayerSpecifications.DEMG)
                                Case LayerSpecifications.giras.Tag.ToLower : aDataTypes.Add(LayerSpecifications.giras)
                                Case LayerSpecifications.lstoret.Tag.ToLower : aDataTypes.Add(LayerSpecifications.lstoret)
                                Case LayerSpecifications.NED.Tag.ToLower : aDataTypes.Add(LayerSpecifications.NED)
                                Case LayerSpecifications.nhd.Tag.ToLower : aDataTypes.Add(LayerSpecifications.nhd)
                                Case LayerSpecifications.pcs3.Tag.ToLower : aDataTypes.Add(LayerSpecifications.pcs3)
                                    'Case LayerSpecifications.d303.Tag.ToLower : aDataTypes.Add(LayerSpecifications.d303)
                                Case LayerSpecifications.huc12.Tag.ToLower : aDataTypes.Add(LayerSpecifications.huc12)
                            End Select
                    End Select
                Case "desiredprojection" : aDesiredProjection = lArg.InnerText
                Case "cachefolder" : aCacheFolder = lArg.InnerText
                Case "getevenifcached" : If Not lArg.InnerText.ToLower.Contains("false") Then aGetEvenIfCached = True
                Case "cacheonly" : If Not lArg.InnerText.ToLower.Contains("false") Then aCacheOnly = True
                Case "stationid" : If Not aStationIDs.Contains(lArg.InnerText) Then aStationIDs.Add(lArg.InnerText)
                Case "savein" : aSaveIn = lArg.InnerText
                Case "savewdm" : aMetWDM = lArg.InnerText
                Case "clip" : If Not lArg.InnerText.ToLower.Contains("false") Then aClip = True
                Case "merge" : If Not lArg.InnerText.ToLower.Contains("false") Then aMerge = True
            End Select
            lArg = lArg.NextSibling
        End While

        If Not aGetMetStations AndAlso Not aGetMetData AndAlso Not aGetSTATSGO AndAlso aDataTypes.Count = 0 Then
            aDataTypes.Add(LayerSpecifications.core31.all)
            Logger.Dbg("Did not find matching type, defaulting to " & LayerSpecifications.core31.all.Name)
        End If

        If aDataTypes.Count > 0 AndAlso aHUC8s.Count < 1 Then

            'First check for a cat layer that contains the list of HUC-8s
            Dim lCatDbfName As String = IO.Path.Combine(aSaveIn, "cat.dbf")
            If FileExists(lCatDbfName) Then
                Dim lCatDbf As New atcUtility.atcTableDBF
                lCatDbf.OpenFile(lCatDbfName)
                Dim lHucField As Integer = lCatDbf.FieldNumber("CU")
                If lHucField > 0 Then
                    For lRecord As Integer = 1 To lCatDbf.NumRecords
                        lCatDbf.CurrentRecord = lRecord
                        aHUC8s.Add(lCatDbf.Value(lHucField))
                    Next
                End If
            End If

            'If we still didn't find any HUC-8, overlay Region with HUC-8 layer to find HUC-8s
            If aHUC8s.Count < 1 AndAlso Not aRegion Is Nothing Then
                aHUC8s = aRegion.GetKeys(Region.RegionTypes.huc8)
            End If
        End If

        If aDataTypes.Count > 0 Then 'Must have HUC(s) for regular data types
            If aHUC8s.Count < 1 Then
                Throw New Exception("HUC 8 required but not found")
            ElseIf aHUC8s(0).Length <> 8 Then
                Throw New Exception("HUC 8 invalid value: '" & aHUC8s(0) & "'")
            End If
        End If

        If aSaveIn.Length = 0 AndAlso aMetWDM.Length = 0 Then
            Throw New Exception("BASINS data: No destination to save as")
        End If

        Return True
    End Function
End Class
