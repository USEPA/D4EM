Imports atcData
Imports atcUtility
Imports MapWinUtility

Module modNRCS

    ''' <summary>
    ''' Get NRCS Soils data
    ''' </summary>
    ''' <param name="aProject">project to add data to</param>
    ''' <returns>XML describing success or error</returns>
    Public Function ExecuteNRCS_Soils(ByVal aQuery As String) As String
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
                    Case "getsoils"
                        Dim lCacheFolder As String = ""
                        Dim lDesiredProjection As String = ""
                        Dim lSaveIn As String = ""
                        Dim lRegion As D4EM.Data.Region = Nothing
                        Dim lClip As Boolean = False
                        Dim lMerge As Boolean = False
                        Dim lGetEvenIfCached As Boolean = False
                        Dim lCacheOnly As Boolean = False

                        If GetLayerArgs(lNode.FirstChild,
                                        lDesiredProjection,
                                        lCacheFolder, lSaveIn,
                                        lRegion,
                                        lClip,
                                        lMerge,
                                        lGetEvenIfCached,
                                        lCacheOnly) Then
                            Dim lProject As New D4EM.Data.Project(D4EM.Data.Globals.FromProj4(lDesiredProjection),
                                                                  lCacheFolder,
                                                                  lSaveIn,
                                                                  lRegion,
                                                                  lClip, lMerge,
                                                                  lGetEvenIfCached,
                                                                  lCacheOnly)
                            Dim lSoils As List(Of D4EM.Data.Source.NRCS_Soil.SoilLocation.Soil) = Nothing
                            lSoils = D4EM.Data.Source.NRCS_Soil.SoilLocation.GetSoils(lProject, "Soils")
                            Dim lSaveAs As String = IO.Path.Combine(lSaveIn, "Soils")
                            lSaveAs = IO.Path.Combine(lSaveAs, "SSURGO.shp")
                            If IO.File.Exists(lSaveAs) Then
                                lResult = "<add_shape>" & lSaveAs & "</add_shape>" & vbCrLf
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
                                  ByRef aDesiredProjection As String,
                                  ByRef aCacheFolder As String,
                                  ByRef aSaveIn As String,
                                  ByRef aRegion As D4EM.Data.Region,
                                  ByRef aClip As Boolean,
                                  ByVal aMerge As Boolean,
                                  ByRef aGetEvenIfCached As Boolean,
                                  ByRef aCacheOnly As Boolean) As Boolean

        Dim lArg As Xml.XmlNode = aArgs.FirstChild

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
                Case "desiredprojection" : aDesiredProjection = lArg.InnerText
                Case "getevenifcached" : If Not lArg.InnerText.ToLower.Contains("false") Then aGetEvenIfCached = True
                Case "cacheonly" : If Not lArg.InnerText.ToLower.Contains("false") Then aCacheOnly = True
                Case "savein" : aSaveIn = lArg.InnerText
            End Select
            lArg = lArg.NextSibling
        End While

        Return True
    End Function

End Module
