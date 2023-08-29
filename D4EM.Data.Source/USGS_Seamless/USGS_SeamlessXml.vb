Imports atcUtility
Imports MapWinUtility

Partial Class USGS_Seamless
    Inherits SourceBase

    Public Overloads Overrides ReadOnly Property Name() As String
        Get
            Return "D4EM Data Download::USGS_Seamless"
        End Get
    End Property

    Public Overrides ReadOnly Property Description() As String
        Get
            Return "Retrieve NLCD and other data from USGS Seamless"
        End Get
    End Property

    Public Overloads Overrides ReadOnly Property Author() As String
        Get
            Return "U.S. Environmental Protection Agency" + Environment.NewLine _
                 & "Office of Research and Development" + Environment.NewLine _
                 & "Ecosystems Research Division"
        End Get
    End Property

    Public Overrides ReadOnly Property QuerySchema() As String
        Get
            Dim lFileName As String = "USGS_SeamlessQuerySchema.xml"
            Return GetEmbeddedFileAsString(lFileName, "D4EM.Data.Source." & lFileName)
        End Get
    End Property

    Private Function GetNodeText(ByVal aDoc As System.Xml.XmlDocument, ByVal aQuery As String, Optional ByVal aDefaultText As String = "") As String
        Dim lNode As System.Xml.XmlNode = aDoc.SelectSingleNode(aQuery)
        If lNode Is Nothing Then
            Return aDefaultText
        Else
            Return lNode.InnerText
        End If
    End Function

    ''' <summary>
    ''' Download NLCD 2001 or 1992 layer(s)
    ''' </summary>
    ''' <param name="aQuery">XML following QuerySchema describing data to download</param>
    ''' <returns>XML describing success or error</returns>
    Public Overloads Overrides Function Execute(ByVal aQuery As String) As String
        'Set Args from XML query
        Dim xmlDoc As New System.Xml.XmlDocument()
        xmlDoc.LoadXml(aQuery)

        Dim lRegionProjection As String = ""
        Dim lRegion As Region = Nothing

        Dim lDefaultString As String = "NLCD"

        Dim lSaveFolder As String = lDefaultString
        Dim lCacheFolder As String = IO.Path.GetTempPath
        Dim lDesiredProjection As DotSpatial.Projections.ProjectionInfo = D4EM.Data.Globals.AlbersProjection
        Dim lCacheOnly As Boolean = False
        Dim lGetEvenIfCached As Boolean = False
        Dim lClip As Boolean = False
        Dim lDataTypes As New Generic.List(Of LayerSpecification)
        Dim lArg As Xml.XmlNode = xmlDoc.FirstChild.FirstChild.FirstChild
        While Not lArg Is Nothing
            Dim lArgName As String = lArg.Name
            Select Case lArgName.ToLower
                Case "region"
                    Try
                        lRegion = New Region(lArg)
                    Catch e As Exception
                        Logger.Dbg("Exception reading Region from query: " & e.Message)
                    End Try
                Case "datatype"
                    'GPF 7/25/2019 added 2016
                    'GPF 8/22/2023 added 2021
                    For Each lLayerSpec As LayerSpecification In {
                        LayerSpecifications.NLCD1992.LandCover,
                        LayerSpecifications.NLCD2001.LandCover,
                        LayerSpecifications.NLCD2001.Impervious,
                        LayerSpecifications.NLCD2001.Canopy,
                        LayerSpecifications.NLCD2006.LandCover,
                        LayerSpecifications.NLCD2006.Impervious,
                        LayerSpecifications.NLCD2011.LandCover,
                        LayerSpecifications.NLCD2011.Impervious,
                        LayerSpecifications.NLCD2016.LandCover,
                        LayerSpecifications.NLCD2016.Impervious,
                        LayerSpecifications.NLCD2019.LandCover,
                        LayerSpecifications.NLCD2019.Impervious,
                        LayerSpecifications.NLCD2021.LandCover,
                        LayerSpecifications.NLCD2021.Impervious,
                        LayerSpecifications.NLCD2008.LandCover,
                        LayerSpecifications.NLCD2013.LandCover,
                        LayerSpecifications.NLCD2004.LandCover}

                        If lLayerSpec.Tag.ToLowerInvariant().Replace("_", ".") = lArg.InnerText.ToLowerInvariant().Replace("_", ".") Then
                            lDataTypes.Add(lLayerSpec)
                        End If
                    Next
                Case "desiredprojection" : lDesiredProjection = Globals.FromProj4(lArg.InnerText)
                Case "cachefolder" : lCacheFolder = lArg.InnerText
                Case "cacheonly" : lCacheOnly = True
                Case "getevenifcached" : If Not lArg.InnerText.ToLower.Contains("false") Then lGetEvenIfCached = True
                Case "clip" : If Not lArg.InnerText.ToLower.Contains("false") Then lClip = True
                Case "savein" : lSaveFolder = IO.Path.Combine(lArg.InnerText, lSaveFolder)
            End Select
            lArg = lArg.NextSibling
        End While
        Dim lResult As String = ""
        Dim lProject As New D4EM.Data.Project(lDesiredProjection, lCacheFolder, lSaveFolder, lRegion, lClip, False, lGetEvenIfCached, lCacheOnly)
        If lDataTypes.Count = 0 Then
            lDataTypes.Add(LayerSpecifications.NLCD2011.LandCover)
        End If
        For Each lDataType As LayerSpecification In lDataTypes
            'TODO: determine how to best combine XML result from multiple data types
            lResult &= Execute(aProject:=lProject, aSaveFolder:=Nothing, aDataType:=lDataType)
        Next
        Return lResult
    End Function
End Class
