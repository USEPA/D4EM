Imports atcUtility
Imports MapWinUtility
Imports System.Xml

Partial Class Storet
    Inherits SourceBase

    Private Const pName As String = "STORET"
    'Private pXMLFieldnames() As String = {"OrganizationIdentifier", "OrganizationFormalName", "MonitoringLocationIdentifier", "MonitoringLocationName", "MonitoringLocationTypeName", "LatitudeMeasure", "LongitudeMeasure"}
    Private pShapeFieldnames() As String = {"OrgId", "OrgName", "LocId", "LocName", "LocType", "Latitude", "Longitude"}
    Private pShapeFieldWidths() As Integer = {20, 255, 20, 255, 20, 8, 8}

    Public Overrides ReadOnly Property Name() As String
        Get
            Return "D4EM Data Download::" & pName
        End Get
    End Property

    Public Overrides ReadOnly Property Description() As String
        Get
            Return "Retrieve Modernized STORET data"
        End Get
    End Property

    Public Overrides Function Execute(ByVal aQuery As String) As String
        Dim lFunctionName As String
        Dim lQuery As New XmlDocument
        Dim lNode As XmlNode
        Dim lError As String = ""
        Dim lResult As String = ""
        Try
            lQuery.LoadXml(aQuery)
            lNode = lQuery.FirstChild
            If lNode.Name.ToLower = "function" Then
                lFunctionName = lNode.Attributes.GetNamedItem("name").Value.ToLower
                Select Case lFunctionName
                    Case "getstoret"
                        Dim lDataTypes As New ArrayList
                        Dim lStationIDs As New atcCollection
                        Dim lDesiredProjection As String = ""
                        Dim lCacheFolder As String = ""
                        Dim lSaveIn As String = ""
                        Dim lRegion As Region = Nothing
                        Dim lClip As Boolean = False
                        Dim lMerge As Boolean = False
                        Dim lGetEvenIfCached As Boolean = False

                        If GetLayerArgs(lNode.FirstChild, lDataTypes, lStationIDs, _
                                        lDesiredProjection, lCacheFolder, lSaveIn, _
                                        lRegion, _
                                        lClip, _
                                        lGetEvenIfCached) Then
                            lResult &= GetSTORET(lDataTypes, lStationIDs,
                                                 Globals.FromProj4(lDesiredProjection), lCacheFolder, lSaveIn,
                                                 lRegion,
                                                 lClip,
                                                 lGetEvenIfCached)
                        End If
                    Case Else
                        lError = "Unknown function: " & lFunctionName
                End Select
            Else
                lError = "Cannot handle query that does not start with a function" & vbCrLf & _
                         "Supplied query started with '" & lNode.Name & "'"
            End If
        Catch ex As Exception
            Logger.Dbg("Error Stack Trace: " & ex.StackTrace)
            lError = ex.Message
        End Try
        If lError.Length = 0 Then
            If lResult.Length > 0 Then
                Return "<success>" & lResult & "</success>"
            Else
                Return "<success />"
            End If
        Else
            Logger.Dbg(aQuery, lError)
            Return "<error>" & lError & "</error>"
        End If
    End Function

    Public Overrides ReadOnly Property QuerySchema() As String
        Get
            Return _
            "<DataExtension name='" & pName & "'>" & _
            "  <function name='getstoret' label='" & Description & "'>" & _
            "    <arguments>" & _
            "      <region            format='Region' label='Region to download' />" & _
            "      <DataType          format='String' label='Type of data to get' valid='stations,results' optional='true' default='stations' />" & _
            "      <DesiredProjection format='String' label='PROJ 4 string describing desired projection' optional='true' />" & _
            "      <SaveIn            format='folder' label='Save data as' filter='JPEG Files (*.jpg)|*.jpg' />" & _
            "      <CacheFolder       format='folder' label='Where to cache downloaded file' optional='true' />" & _
            "      <Clip              format='boolean' label='Clip to Region' optional='true' default='false' />" & _
            "    </arguments>" & _
            "    <returns>" & _
            "    </returns>" & _
            "  </function>" & _
            "</DataExtension>"
        End Get
    End Property

    Function GetLayerArgs(ByVal aArgs As XmlNode, _
                          ByRef aDataTypes As ArrayList, _
                          ByRef aStationIDs As atcCollection, _
                          ByRef aDesiredProjection As String, _
                          ByRef aCacheFolder As String, _
                          ByRef aSaveIn As String, _
                          ByRef aRegion As Region, _
                          ByRef aClip As Boolean, _
                          ByRef aGetEvenIfCached As Boolean) As Boolean

        Dim FoundType As Boolean = False
        Dim lArg As XmlNode = aArgs.FirstChild
        aClip = False
        aDataTypes = New ArrayList

        While Not lArg Is Nothing
            Dim lArgName As String = lArg.Name
            Dim lNameAttribute As XmlAttribute = lArg.Attributes.GetNamedItem("name")
            If Not lNameAttribute Is Nothing Then lArgName = lNameAttribute.Value
            Select Case lArgName.ToLower
                Case "region"
                    Try
                        aRegion = New Region(lArg)
                    Catch e As Exception
                        Logger.Dbg("Exception reading Region from query: " & e.Message)
                    End Try
                Case "desiredprojection" : aDesiredProjection = lArg.InnerText
                Case "datatype"
                    If Not lArg.InnerText.ToLower.Equals("all") Then
                        aDataTypes.Add(lArg.InnerText.ToLower)
                    End If
                Case "cachefolder" : aCacheFolder = lArg.InnerText
                Case "getevenifcached" : If Not lArg.InnerText.ToLower.Contains("false") Then aGetEvenIfCached = True
                Case "stationid" : aStationIDs.Add(lArg.InnerText)
                Case "savein", "saveas" : aSaveIn = lArg.InnerText
                Case "clip" : If Not lArg.InnerText.ToLower.Contains("false") Then aClip = True
            End Select
            lArg = lArg.NextSibling
        End While

        If aSaveIn.Length = 0 Then
            Throw New Exception("No destination to save in")
        End If
        Return True
    End Function

End Class
