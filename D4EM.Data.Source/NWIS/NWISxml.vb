Imports atcUtility
Imports MapWinUtility

Partial Class NWIS
    Inherits SourceBase

    Public Overrides ReadOnly Property Name() As String
        Get
            Return "D4EM Data Download::NWIS"
        End Get
    End Property

    Public Overrides ReadOnly Property Description() As String
        Get
            Return "Retrieve NWIS station information and data"
        End Get
    End Property

    Public Overrides ReadOnly Property QuerySchema() As String
        Get
            Dim lFileName As String = "NWISQuerySchema.xml"
            Return GetEmbeddedFileAsString(lFileName, "D4EM.Data.Source." & lFileName).Replace("DefaultStationsFilename", pDefaultStationsBaseFilename)
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
                    Case "getnwisdailydischarge"
                        lResult &= GetDailyDischarge(lNode.FirstChild)
                    Case "getnwisdailygw"
                        lResult &= GetDailyGroundwater("gw", lNode.FirstChild)
                    Case "getnwisprecipitation"
                        lResult &= GetDailyGroundwater("precip", lNode.FirstChild)
                    Case "getnwisperiodicgw"
                        lResult &= GetPeriodicGroundwater(lNode.FirstChild)
                    Case "getnwisidadischarge"
                        lResult &= GetIDADischarge(lNode.FirstChild)
                    Case "getnwismeasurements"
                        lResult &= GetMeasurements(lNode.FirstChild)
                    Case "getnwisstations"
                        lResult &= GetStations(lNode.FirstChild)
                    Case "getnwiswq"
                        lResult &= GetWQ(lNode.FirstChild)
                    Case "getnwisstatistics"
                        lResult &= GetNWISStatistics("03070103", "00010", StationType.StreamOrRiver)
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
                Return "<success>" & lResult & "</success>"
            Else
                Return "<success />"
            End If
        Else
            Return "<error>" & lError & "</error>"
        End If

    End Function

    Public Shared Function GetStations(ByVal aArgs As Xml.XmlNode) As String
        Dim lResults As String = ""
        Dim want_measurements As Boolean = False
        Dim want_discharge As Boolean = False
        Dim want_qw As Boolean = False
        Dim want_gw As Boolean = False
        Dim want_rt As Boolean = False
        Dim want_peak As Boolean = False
        Dim lStartDate As String = "1/1/1900"
        Dim lEndDate As String = "1/1/2100"
        Dim lMinCount As Integer = 0
        Dim lSaveIn As String = ""
        Dim lRegion As Region = Nothing
        Dim lMakeShape As Boolean = True
        Dim lDesiredProjection As DotSpatial.Projections.ProjectionInfo = D4EM.Data.Globals.GeographicProjection
        Dim lStationsRDB As New atcTableRDB

        Dim lArg As Xml.XmlNode = aArgs.FirstChild

        While Not lArg Is Nothing
            Dim lArgName As String = lArg.Name
            Select Case lArgName.ToLower
                Case "datatype"
                    Select Case lArg.InnerText.ToLower
                        Case "measurement", "measurements" : want_measurements = True
                        Case "discharge" : want_discharge = True
                        Case "qw" : want_qw = True
                        Case "gw" : want_gw = True
                        Case "rt" : want_rt = True
                        Case "peak" : want_peak = True
                    End Select
                Case "region"
                    Try
                        lRegion = New Region(aArgs)
                    Catch e As Exception
                        Logger.Dbg("Exception reading Region from query: " & e.Message)
                    End Try
                Case "desiredprojection" : lDesiredProjection = Globals.FromProj4(lArg.InnerText)
                Case "startdate" : lStartDate = lArg.InnerText
                Case "enddate" : lEndDate = lArg.InnerText
                Case "mincount" : lMinCount = CInt(lArg.InnerText)
                Case "savein" : lSaveIn = lArg.InnerText
                Case "makeshape" : If lArg.InnerText.ToLower = "false" Then lMakeShape = False
            End Select
            lArg = lArg.NextSibling
        End While

        If Not (want_discharge OrElse want_measurements OrElse want_qw OrElse want_gw OrElse want_peak) Then
            want_discharge = True 'Default station type to get if none specified
        End If

        If Not lSaveIn.ToLower.Contains("nwis") Then lSaveIn = IO.Path.Combine(lSaveIn, "NWIS")
        IO.Directory.CreateDirectory(lSaveIn)

        Dim lSaveAsBase As String = lSaveIn
        If lSaveAsBase.Length = 0 OrElse IO.Directory.Exists(lSaveAsBase) Then
            lSaveAsBase = IO.Path.Combine(lSaveAsBase, pDefaultStationsBaseFilename)
        End If

        Dim lProject As New Project(lDesiredProjection, Nothing, lSaveAsBase, lRegion, False, True)

        If want_measurements Then lResults &= GetAndMakeShape(lProject, NWIS.LayerSpecifications.Measurement, lSaveAsBase, lMakeShape)
        If want_discharge Then lResults &= GetAndMakeShape(lProject, NWIS.LayerSpecifications.Discharge, lSaveAsBase, lMakeShape)
        If want_qw Then lResults &= GetAndMakeShape(lProject, NWIS.LayerSpecifications.WaterQuality, lSaveAsBase, lMakeShape)
        If want_gw Then lResults &= GetAndMakeShape(lProject, NWIS.LayerSpecifications.Groundwater, lSaveAsBase, lMakeShape)
        If want_peak Then lResults &= GetAndMakeShape(lProject, NWIS.LayerSpecifications.Peak, lSaveAsBase, lMakeShape)

        Return lResults
    End Function

    Public Shared Function GetDailyDischarge(ByVal aArgs As Xml.XmlNode) As String
        Dim lStartDate As String = "1880-01-01"
        Dim lEndDate As String = "2100-01-01"
        Dim lStationIDs As New Generic.List(Of String)
        Dim lCacheFolder As String = IO.Path.GetTempPath
        Dim lGetEvenIfCached As Boolean = False
        Dim lCacheOnly As Boolean = False
        Dim lSaveIn As String = ""
        Dim lRegion As Region = Nothing
        Dim lStationIndex As Integer = 1
        Dim lWDMFilename As String = ""

        Dim lArg As Xml.XmlNode = aArgs.FirstChild

        While Not lArg Is Nothing
            Select Case lArg.Name.ToLower
                Case "region"
                    Try
                        lRegion = New Region(aArgs)
                    Catch e As Exception
                        Logger.Dbg("Exception reading Region from query: " & e.Message)
                    End Try
                Case "startdate" : lStartDate = lArg.InnerText
                Case "enddate" : lEndDate = lArg.InnerText
                Case "stationid" : lStationIDs.Add(lArg.InnerText)
                Case "cachefolder" : lCacheFolder = lArg.InnerText
                Case "cacheonly" : lCacheOnly = True
                Case "getevenifcached" : If Not lArg.InnerText.ToLower.Contains("false") Then lGetEvenIfCached = True
                Case "savein" : lSaveIn = lArg.InnerText
                Case "savewdm" : lWDMFilename = lArg.InnerText
            End Select
            lArg = lArg.NextSibling
        End While
        Return GetDailyDischarge(New Project(D4EM.Data.Globals.GeographicProjection,
                                             lCacheFolder, lSaveIn, lRegion, False, False,
                                             lGetEvenIfCached, lCacheOnly),
                                 "NWIS", lStationIDs, lStartDate, lEndDate, lWDMFilename)
    End Function

    Public Shared Function GetMeasurements(ByVal aArgs As Xml.XmlNode) As String
        Dim lResults As String = ""
        Dim lNumNotDownloaded As Integer = 0
        Dim lStartDate As String = "1880-01-01"
        Dim lEndDate As String = "2100-01-01"
        Dim lStationIDs As New Generic.List(Of String)
        Dim lCacheFolder As String = IO.Path.GetTempPath
        Dim lGetEvenIfCached As Boolean = False
        Dim lCacheOnly As Boolean = False
        Dim lSaveIn As String = ""
        Dim lRegion As Region = Nothing
        Dim lStationIndex As Integer = 1

        Dim lArg As Xml.XmlNode = aArgs.FirstChild

        While Not lArg Is Nothing
            Select Case lArg.Name.ToLower
                Case "region"
                    Try
                        lRegion = New Region(aArgs)
                    Catch e As Exception
                        Logger.Dbg("Exception reading Region from query: " & e.Message)
                    End Try
                Case "startdate" : lStartDate = lArg.InnerText
                Case "enddate" : lEndDate = lArg.InnerText
                Case "stationid" : lStationIDs.Add(lArg.InnerText)
                Case "cachefolder" : lCacheFolder = lArg.InnerText
                Case "cacheonly" : lCacheOnly = True
                Case "getevenifcached" : If Not lArg.InnerText.ToLower.Contains("false") Then lGetEvenIfCached = True
                Case "savein" : lSaveIn = lArg.InnerText
            End Select
            lArg = lArg.NextSibling
        End While
        Return GetMeasurements(New Project(D4EM.Data.Globals.GeographicProjection,
                                           lCacheFolder, lSaveIn, lRegion, False, False,
                                           lGetEvenIfCached, lCacheOnly),
                               "NWIS", lStationIDs, lStartDate, lEndDate)
    End Function

    Public Shared Function GetIDADischarge(ByVal aArgs As Xml.XmlNode) As String
        Dim lNumNotDownloaded As Integer = 0
        Dim lStartDate As Date = New Date(1880, 1, 1)
        Dim lEndDate As Date = New Date(2100, 1, 1)
        Dim lIdaBaseUrl As String = "http://ida.water.usgs.gov/ida/"
        Dim lStationIDs As New Generic.List(Of String)
        Dim lCacheFolder As String = IO.Path.GetTempPath
        Dim lGetEvenIfCached As Boolean = False
        Dim lCacheOnly As Boolean = False
        Dim lSaveIn As String = ""
        Dim lRegion As Region = Nothing
        Dim lStationIndex As Integer = 1
        Dim lWDMFilename As String = ""

        Dim lFromDateFormat As New atcUtility.atcDateFormat
        lFromDateFormat.IncludeHours = False
        lFromDateFormat.IncludeMinutes = False
        lFromDateFormat.DateSeparator = "-"
        lFromDateFormat.Midnight24 = False

        Dim lToDateFormat As New atcUtility.atcDateFormat
        lToDateFormat.IncludeHours = False
        lToDateFormat.IncludeMinutes = False
        lToDateFormat.DateSeparator = "-"

        Dim lArg As Xml.XmlNode = aArgs.FirstChild

        While Not lArg Is Nothing
            Select Case lArg.Name.ToLower
                Case "region"
                    Try
                        lRegion = New Region(aArgs)
                    Catch e As Exception
                        Logger.Dbg("Exception reading Region from query: " & e.Message)
                    End Try
                Case "startdate" : lStartDate = Date.Parse(lArg.InnerText)
                Case "enddate" : lEndDate = Date.Parse(lArg.InnerText)
                Case "stationid" : lStationIDs.Add(lArg.InnerText)
                Case "cachefolder" : lCacheFolder = lArg.InnerText
                Case "getevenifcached" : If Not lArg.InnerText.ToLower.Contains("false") Then lGetEvenIfCached = True
                Case "cacheonly" : lCacheOnly = True
                Case "savein" : lSaveIn = lArg.InnerText
                Case "savewdm" : lWDMFilename = lArg.InnerText
                Case "lIdaBaseUrl" : lIdaBaseUrl = lArg.InnerText
            End Select
            lArg = lArg.NextSibling
        End While
        Return GetIDADischarge(New Project(D4EM.Data.Globals.GeographicProjection,
                                           lCacheFolder, lSaveIn, lRegion, False, False,
                                           lGetEvenIfCached, lCacheOnly),
                               aSaveFolder:="NWIS",
                               aStationIDs:=lStationIDs,
                               aStartDate:=lStartDate,
                               aEndDate:=lEndDate,
                               aWDMFilename:=lWDMFilename,
                               aIdaBaseUrl:=lIdaBaseUrl)
    End Function


    Public Shared Function GetWQ(ByVal aArgs As Xml.XmlNode) As String
        Dim lStartDate As String = "" '"1880-01-01"
        Dim lEndDate As String = "" '"2100-01-01"
        Dim lStationIDs As New Generic.List(Of String)
        Dim lSaveIn As String = ""
        Dim lCacheFolder As String = IO.Path.GetTempPath
        Dim lGetEvenIfCached As Boolean = False
        Dim lCacheOnly As Boolean = False
        Dim lRegion As Region = Nothing

        Dim lArg As Xml.XmlNode = aArgs.FirstChild

        While Not lArg Is Nothing
            Select Case lArg.Name.ToLower
                Case "region"
                    Try
                        lRegion = New Region(aArgs)
                    Catch e As Exception
                        Logger.Dbg("Exception reading Region from query: " & e.Message)
                    End Try
                Case "startdate" : lStartDate = lArg.InnerText
                Case "enddate" : lEndDate = lArg.InnerText
                Case "stationid" : lStationIDs.Add(lArg.InnerText)
                Case "cachefolder" : lCacheFolder = lArg.InnerText
                Case "getevenifcached" : If Not lArg.InnerText.ToLower.Contains("false") Then lGetEvenIfCached = True
                Case "cacheonly" : lCacheOnly = True
                Case "savein" : lSaveIn = lArg.InnerText
            End Select
            lArg = lArg.NextSibling
        End While
        Return GetWQ(New Project(D4EM.Data.Globals.GeographicProjection,
                                 lCacheFolder, lSaveIn, lRegion, False, False,
                                 lGetEvenIfCached, lCacheOnly),
                     aSaveFolder:="NWIS",
                     aStationIDs:=lStationIDs,
                     aStartDate:=lStartDate,
                     aEndDate:=lEndDate)
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="aDataType">Precip or groundwater ("precip" vs "gw")</param>
    ''' <param name="aArgs"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetDailyGroundwater(ByVal aDataType As String, ByVal aArgs As Xml.XmlNode) As String
        Dim lStartDate As String = EarliestDate
        Dim lEndDate As String = LatestDate
        Dim lStationIDs As New Generic.List(Of String)
        Dim lCacheFolder As String = IO.Path.GetTempPath
        Dim lGetEvenIfCached As Boolean = False
        Dim lCacheOnly As Boolean = False
        Dim lSaveIn As String = ""
        Dim lStationIndex As Integer = 1
        Dim lWDMFilename As String = ""
        Dim lRegion As Region = Nothing

        Dim lArg As Xml.XmlNode = aArgs.FirstChild

        While Not lArg Is Nothing
            Select Case lArg.Name.ToLower
                Case "region"
                    Try
                        lRegion = New Region(aArgs)
                    Catch e As Exception
                        Logger.Dbg("Exception reading Region from query: " & e.Message)
                    End Try
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
        Return GetDailyGroundwater(New Project(D4EM.Data.Globals.GeographicProjection,
                                 lCacheFolder, lSaveIn, lRegion, False, False,
                                 lGetEvenIfCached, lCacheOnly), lSaveIn, lStationIDs, lStartDate, lEndDate, lWDMFilename, aDataType)
    End Function

End Class
