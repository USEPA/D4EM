Imports MapWinUtility
Imports atcUtility

Partial Class NLDAS
    Inherits SourceBase



    Public Overrides ReadOnly Property Name() As String
        Get
            Return "D4EM Data Download::NLDAS"
        End Get
    End Property

    Public Overrides ReadOnly Property Description() As String
        Get
            Return "Retrieve NLDAS grid locations and data"
        End Get
    End Property

    Public Overrides ReadOnly Property QuerySchema() As String
        Get
            Dim lFileName As String = "NLDASQuerySchema.xml"
            Dim lQuerySchmea As String = GetEmbeddedFileAsString(lFileName, "D4EM.Data.Source." & lFileName)
            Return lQuerySchmea.Replace("DefaultStationsFilename", pDefaultStationsBaseFilename)
        End Get
    End Property

    Public Overrides Function Execute(ByVal aQuery As String) As String
        Dim lError As String = ""
        Dim lResult As String = ""
        Try
            Dim lFunctionName As String
            Dim lQuery As New Xml.XmlDocument
            Dim lNode As Xml.XmlNode
            lQuery.LoadXml(aQuery)
            lNode = lQuery.FirstChild
            If lNode.Name.ToLower = "function" Then
                lFunctionName = lNode.Attributes.GetNamedItem("name").Value.ToLower
                Select Case lFunctionName
                    Case "getnldasparameter"
                        lResult &= GetParameter(lNode.FirstChild)
                    Case "getnldasgrid"
                        lResult &= GetGridCells(lNode.FirstChild)
                    Case Else
                        lError &= "Unknown function '" & lFunctionName & "'"
                End Select
            Else
                lError &= "Cannot yet handle query that does not start with a function"
            End If

        Catch ex As Exception
            lError = ex.Message
        End Try
        If lError.Length = 0 Then
            If lResult.Length > 0 Then
                If lResult.Contains("<error>") Then
                    Return lResult
                Else
                    Return "<success>" & lResult & "</success>"
                End If
            Else
                Return "<success />"
            End If
        Else
            Return "<error>" & lError & "</error>"
        End If

    End Function

    Public Shared Function GetGridCells(ByVal aArgs As Xml.XmlNode) As String
        Dim lSaveIn As String = ""
        Dim lRegion As Region = Nothing
        Dim lMakeShape As Boolean = True
        Dim lDesiredProjection As DotSpatial.Projections.ProjectionInfo = Globals.GeographicProjection
        Dim lStationsRDB As New atcTableRDB

        Dim lArg As Xml.XmlNode = aArgs.FirstChild

        While Not lArg Is Nothing
            Dim lArgName As String = lArg.Name
            Select Case lArgName.ToLower
                Case "region"
                    Try
                        lRegion = New Region(aArgs)
                    Catch e As Exception
                        Logger.Dbg("Exception reading Region from query: " & e.Message)
                    End Try
                Case "desiredprojection" : lDesiredProjection = Globals.FromProj4(lArg.InnerText)
                Case "savein" : lSaveIn = lArg.InnerText
                Case "makeshape" : If lArg.InnerText.ToLower = "false" Then lMakeShape = False
            End Select
            lArg = lArg.NextSibling
        End While

        Dim lSaveAsBase As String = lSaveIn
        'If lSaveAsBase.Length = 0 OrElse IO.Directory.Exists(lSaveAsBase) Then
        '    lSaveAsBase = IO.Path.Combine(lSaveAsBase, pDefaultStationsBaseFilename)
        'End If

        Dim lProject As New Project(lDesiredProjection, "", lSaveAsBase, lRegion, False, False)

        Return GetLocations(lProject, "NLDAS")
    End Function

    Public Function GetParameter(ByVal aArgs As Xml.XmlNode) As String
        Dim lResults As String = ""
        Dim lStartDate As Date = pFirstAvailableDate
        Dim lEndDate As Date = pDefaultEndDate
        Dim lCells As New Generic.List(Of NLDASGridCoords)
        Dim lCellIndex As Integer = -1
        Dim lCacheFolder As String = IO.Path.GetTempPath
        Dim lGetEvenIfCached As Boolean = False
        Dim lSaveIn As String = ""
        Dim lDataType As String = Nothing
        Dim lCacheOnly As Boolean = False
        Dim lWDMFilename As String = ""

        Dim lArg As Xml.XmlNode = aArgs.FirstChild

        While Not lArg Is Nothing
            Dim lArgName As String = lArg.Name
            Try
                Select Case lArgName.ToLower
                    Case "stationid" : lCells.Add(New NLDASGridCoords(lArg.InnerText))
                    Case "startdate" : lStartDate = Date.Parse(lArg.InnerText)
                    Case "enddate" : lEndDate = Date.Parse(lArg.InnerText)
                    Case "datatype" : lDataType = lArg.InnerText
                    Case "cachefolder" : lCacheFolder = lArg.InnerText
                    Case "cacheonly" : If Not lArg.InnerText.ToLower.Contains("false") Then lCacheOnly = True
                    Case "getevenifcached" : If Not lArg.InnerText.ToLower.Contains("false") Then lGetEvenIfCached = True
                    Case "savein" : lSaveIn = lArg.InnerText
                    Case "savewdm" : lWDMFilename = lArg.InnerText
                End Select
            Catch e As Exception
                Logger.Dbg("Exception reading argument from query: " & lArg.OuterXml & vbCrLf & vbCrLf & e.Message)
            End Try
            lArg = lArg.NextSibling
        End While
        Dim lProject As New Project(D4EM.Data.Globals.GeographicProjection,
                                    lCacheFolder, lSaveIn, Nothing, False, False,
                                    lGetEvenIfCached, lCacheOnly)
        Dim lDataTypes() As String = {lDataType}
        If lDataType Is Nothing Then
            lDataTypes = DefaultParameters
        End If
        For Each lDataType In lDataTypes
            lResults &= GetParameter(lProject, "NLDAS", lCells, lDataType, lStartDate, lEndDate, lWDMFilename) & vbCrLf
        Next
        Return lResults
    End Function

End Class
