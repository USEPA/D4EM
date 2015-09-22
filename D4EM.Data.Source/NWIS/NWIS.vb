Imports atcUtility
Imports MapWinUtility
Imports D4EM.Data.LayerSpecification

Public Class NWIS
    Public Class LayerSpecifications
        Public Shared Measurement As New LayerSpecification(Tag:="measurements", Role:=Roles.Station, Source:=GetType(NWIS))
        Public Shared Discharge As New LayerSpecification(Tag:="discharge", Role:=Roles.Station, Source:=GetType(NWIS))
        Public Shared WaterQuality As New LayerSpecification(Tag:="qw", Role:=Roles.Station, Source:=GetType(NWIS))
        Public Shared GroundwaterDaily As New LayerSpecification(Tag:="gw_daily", Role:=Roles.Station, Source:=GetType(NWIS))
        Public Shared GroundwaterPeriodic As New LayerSpecification(Tag:="gw_periodic", Role:=Roles.Station, Source:=GetType(NWIS))
        Public Shared Peak As New LayerSpecification(Tag:="peak", Role:=Roles.Station, Source:=GetType(NWIS))
        Public Shared Precipitation As New LayerSpecification(Tag:="precipitation", Role:=Roles.Station, Source:=GetType(NWIS))

        'Public Shared StreamOrRiver As New LayerSpecification(Tag:="D4EM.Data.NWIS.StreamOrRiver", Role:=Roles.Station, Source:=GetType(NWIS))
        'Public Shared LakeOrReservoir As New LayerSpecification(Tag:="D4EM.Data.NWIS.LakeOrReservoir", Role:=Roles.Station, Source:=GetType(NWIS))
        'Public Shared Groundwater As New LayerSpecification(Tag:="D4EM.Data.NWIS.Groundwater", Role:=Roles.Station, Source:=GetType(NWIS))
        'Public Shared Estuary As New LayerSpecification(Tag:="D4EM.Data.NWIS.Estuary", Role:=Roles.Station, Source:=GetType(NWIS))
        'Public Shared Spring As New LayerSpecification(Tag:="D4EM.Data.NWIS.Spring", Role:=Roles.Station, Source:=GetType(NWIS))
        'Public Shared Meteorological As New LayerSpecification(Tag:="D4EM.Data.NWIS.Meteorological", Role:=Roles.MetStation, Source:=GetType(NWIS))
    End Class

    Public Enum StationType
        StreamOrRiver = 0
        LakeOrReservoir = 1
        Groundwater = 2
        Estuary = 3
        Spring = 4
        Meteorological = 5
    End Enum

    Private Shared Function StationTypeName(ByVal aStationType As StationType) As String
        Dim lStr As String = System.Enum.GetName(aStationType.GetType, aStationType)
        Dim lIndex As Integer = 1
        While lIndex < lStr.Length - 1
            If Char.IsUpper(lStr.Chars(lIndex)) Then lStr.Insert(lIndex, " ")
            lIndex += 1
        End While
        Return lStr
    End Function

    Private Shared StationCode As String() = { _
        "Y______", _
        "_Y_____", _
        "_____Y_", _
        "__Y____", _
        "____Y__", _
        "______Y"}

    'Private Shared pParameterName As String() = {"Temperature C", "Temperature F", "pH"}
    'Private Shared pParameterCode As String() = {"00010", "00011", "00400"}

    'water quality parameters 
    '   > Temperature (\260C)
    '   > DOC (dissolved organic carbon) (mg/L)
    '   > pH
    '   > TOC (total organic carbon) mg/L
    '   > TSS (total suspended solids) mg/L
    '   > Hardness (mg CaCO3 eq/L)
    'statistics:
    '   > .mean
    '   > .median (50th percentile)
    '   > .95 (95th percentile)
    '   > .05 (5th percentile)
    '   > .npts (number of data points used in calculation)
    '   > .id (ID of hydrologic unit containing data points used in calculation)
    '   > .nsta (number of stations used in the calculation)

    ''' <summary>
    ''' Retrieve data from NWIS and compute statistics
    ''' </summary>
    ''' <param name="aHUC">USGS Hydrologic Unit Code (HUC2 or HUC8)</param>
    ''' <param name="aParameterCode">USGS Water Quality Parameter Code</param>
    ''' <param name="aStationType">USGS Station Type</param>
    ''' <returns>string containing Parameter values</returns>
    ''' <remarks></remarks>
    <CLSCompliant(False)> _
    Public Function GetNWISStatistics( _
                    ByVal aHUC As String, _
                    ByVal aParameterCode As String, _
                    ByVal aStationType As StationType) As String

        Dim lCacheFilename As String = IO.Path.Combine(IO.Path.GetTempPath, "NWIS_" & aHUC & "_" & aParameterCode & "_" & System.Enum.GetName(aStationType.GetType, aStationType) & ".txt")

        If Not IO.File.Exists(lCacheFilename) Then
            Dim lStations As ArrayList = GetStationsWithWQParameter(aHUC, aParameterCode, StationCode(aStationType))
            Dim myXML As String = ""

            If lStations.Count = 0 Then Return GetNWISStatsEmptyXML(aHUC)

            Dim results As New ArrayList
            Dim lNumStationsWithResults As Integer = 0

            Dim lCount As Integer = results.Count
            For Each lStation As String In lStations
                results.AddRange(GetWQValues(lStation, aParameterCode, "01-01-1900", DateTime.Now().Date().ToString()))
                If results.Count > lCount Then
                    lNumStationsWithResults += 1
                    lCount = results.Count
                End If
            Next

            lCount = results.Count
            If lCount = 0 Then Return GetNWISStatsEmptyXML(aHUC)

            Dim arValues As System.Array = System.Array.CreateInstance(GetType(Double), lCount)
            Dim sum As Double = 0
            Dim idx As Integer = 0
            For Each lResultString As String In results
                arValues(idx) = Convert.ToDouble(lResultString)
                sum += arValues(idx)
                idx += 1
            Next
            Array.Sort(arValues)
            Dim avgValue As Double = sum / lCount
            Dim Percentile95 As Double = CDbl(arValues.GetValue(CInt(lCount * 0.95)))
            Dim Percentile5 As Double = CDbl(arValues.GetValue(CInt(lCount * 0.05)))
            Dim median As Double = CDbl(arValues.GetValue(CInt(lCount * 0.5)))

            Dim units As String = GetWQParamUnits(aParameterCode)
            IO.File.WriteAllText(lCacheFilename, "<HUC>" & aHUC & "</HUC>" _
                 & "<NumStations>" & lNumStationsWithResults & "</NumStations>" _
                 & "<Units>" & units & "</Units>" _
                 & "<Statistics>" _
                 & "<Count>" & lCount & "</Count>" _
                 & "<Maximum>" & arValues.GetValue(lCount - 1).ToString() & "</Maximum>" _
                 & "<Minimum>" & arValues.GetValue(0).ToString() & "</Minimum>" _
                 & "<Mean>" & avgValue & "</Mean>" _
                 & "<Median>" & median & "</Median>" _
                 & "<Percentile5>" & Percentile5 & "</Percentile5>" _
                 & "<Percentile95>" & Percentile95 & "</Percentile95>" _
                 & "</Statistics>")
        End If
        Return IO.File.ReadAllText(lCacheFilename)
    End Function

    Private Function GetNWISStatsEmptyXML(ByVal HUC As String) As String
        Return "<HUC>" & HUC & "</HUC>" _
             & "<NumStations>" & "0" & "</NumStations>" _
             & "<Units>" & "unknown" & "</Units>" _
             & "<Statistics>" _
             & "<Count>" & "0" & "</Count>" _
             & "<Maximum>" & "0" & "</Maximum>" _
             & "<Minimum>" & "0" & "</Minimum>" _
             & "<Mean>" & "0" & "</Mean>" _
             & "<Median>" & "0" & "</Median>" _
             & "<Percentile5>" & "0" & "</Percentile5>" _
             & "<Percentile95>" & "0" & "</Percentile95>" _
             & "</Statistics>"
    End Function

#Region " NWIS Web Data "

    Private Function GetSiteInfo(ByVal SiteCode As String) As String

        'SAMPLE URL
        'http://waterdata.usgs.gov/nwis/inventory?search_site_no=02084160
        '&search_site_no_match_type=exact&sort_key=site_no&group_key=NONE&format=sitefile_output&sitefile_output_format=xml&
        'column_name=agency_cd&column_name=site_no&column_name=station_nm&column_name=dec_lat_va&column_name=dec_long_va&
        'column_name=coord_datum_cd&column_name=discharge_begin_date&column_name=discharge_end_date&column_name=discharge_count_nu
        '&column_name=peak_begin_date&column_name=peak_end_date&column_name=peak_count_nu&column_name=qw_begin_date
        '&column_name=qw_end_date&column_name=qw_count_nu&column_name=gw_begin_date&column_name=gw_end_date
        '&column_name=gw_count_nu&list_of_search_criteria=search_site_no


        Dim sURL As String = "http://waterdata.usgs.gov/nwis/inventory?search_site_no=" & SiteCode & _
            "&search_site_no_match_type=exact&sort_key=site_no&group_key=NONE&format=sitefile_output&sitefile_output_format=rdb&" & _
            "column_name=agency_cd&column_name=site_no&column_name=station_nm&column_name=dec_lat_va&column_name=dec_long_va&" & _
            "column_name=coord_datum_cd&column_name=discharge_begin_date&column_name=discharge_end_date&column_name=discharge_count_nu" & _
            "&column_name=peak_begin_date&column_name=peak_end_date&column_name=peak_count_nu&column_name=qw_begin_date" & _
            "&column_name=qw_end_date&column_name=qw_count_nu&column_name=gw_begin_date&column_name=gw_end_date" & _
            "&column_name=gw_count_nu&list_of_search_criteria=search_site_no"

        Dim sr As IO.StreamReader = D4EM.Data.Download.GetHTTPStreamReader(sURL)
        Dim line As String = sr.ReadLine

        Do While line.Substring(0, 1) = "#"
            line = sr.ReadLine
        Loop

        line = sr.ReadLine
        line = sr.ReadLine

        Dim values() As String
        values = line.Split(vbTab)

        Dim lXMLsb As New Text.StringBuilder
        With lXMLsb
            .Append("<SiteInfo>" & vbNewLine)
            .Append("   <Name>" & values(2) & "</Name>" & vbNewLine)
            .Append("   <Latitude>" & values(3) & "</Latitude>" & vbNewLine)
            .Append("   <Longitude>" & values(4) & "</Longitude>" & vbNewLine)
            .Append("   <Parameters>" & vbNewLine)

            .Append("      <Parameter ParameterCode='Daily Streamflow'>" & vbNewLine)
            .Append("         <StartTSDate>" & values(7) & "</StartTSDate>" & vbNewLine)
            .Append("         <EndTSDate>" & values(8) & "</EndTSDate>" & vbNewLine)
            .Append("         <Count>" & values(9) & "</Count>" & vbNewLine)
            .Append("      </Parameter>" & vbNewLine)

            .Append("      <Parameter ParamterCode='Groundwater Levels'>" & vbNewLine)
            .Append("         <StartTSDate>" & values(16) & "</StartTSDate>" & vbNewLine)
            .Append("         <EndTSDate>" & values(17) & "</EndTSDate>" & vbNewLine)
            .Append("         <Count>" & values(18) & "</Count>" & vbNewLine)
            .Append("      </Parameter>" & vbNewLine)

            Dim StartDate As Date = #1/1/1850#
            Dim EndDate As Date = Today

            sURL = "http://nwis.waterdata.usgs.gov/nwis/qwdata?search_site_no=" + SiteCode + _
                        "&search_site_no_match_type=exact&sort_key=site_no&group_key=NONE&sitefile_output_format=html_table&column_name=agency_cd" + _
                        "&begin_date=" + Format(CDate(StartDate), "yyyy-MM-dd") + "&end_date=" + Format(CDate(EndDate), "yyyy-MM-dd") + _
                        "&inventory_output=0&format=rdb_inventory&rdb_inventory_output=value&date_format=YYYY-MM-DD&rdb_compression=file" + _
                        "&qw_sample_wide=0&list_of_search_criteria=search_site_no"

            Dim sr2 As IO.StreamReader = GetHTTPStreamReader(sURL)
            line = sr2.ReadLine

            If Not line = Nothing Then

                Do While line.Substring(0, 1) = "#"
                    line = sr2.ReadLine
                Loop

                line = sr2.ReadLine
                line = sr2.ReadLine

                Do Until line Is Nothing
                    values = line.Split()
                    .Append("      <Parameter ParameterCode='" + values(2) + "'>" & vbNewLine)
                    .Append("         <StartTSDate>" & values(4) & "</StartTSDate>" & vbNewLine)
                    .Append("         <EndTSDate>" & values(5) & "</EndTSDate>" & vbNewLine)
                    .Append("         <Count>" & values(3) & "</Count>" & vbNewLine)
                    .Append("      </Parameter>" & vbNewLine)
                    line = sr2.ReadLine
                Loop
            End If

            .Append("   </Parameters>" & vbNewLine)
            .Append("</SiteInfo>")

            'pass back rest of response
            Return .ToString
        End With
    End Function

    '    <WebMethod(Description:="Given a USGS station number, this method returns a string with " & _
    '   "the parameter code, " & _
    '   "the number of observations, " & _
    '   "the date/time of the first observation, and " & _
    '   "the date/time of the last observation for that station.")> _
    'Private Function GetParametersForStation( _
    '   ByVal StationNumber As String, _
    '   ByVal StartDate As String, _
    '   ByVal EndDate As String) As String

    '        Dim sURL As String
    '        Dim stringReader As IO.StringReader
    '        Dim line As String
    '        Dim Result As String, values() As String

    '        'If DailyStreamflow.ToUpper = "TRUE" Then
    '        '    sURL = "http://waterdata.usgs.gov/nwis/inventory?search_site_no=" + StationNumber + _
    '        '    "&search_site_no_match_type=exact&sort_key=site_no&group_key=NONE&format=" + _
    '        '    "sitefile_output&sitefile_output_format=xml&column_name=discharge_begin_date" + _
    '        '    "&column_name=discharge_end_date&column_name=discharge_count_nu&" + _
    '        '    "list_of_search_criteria=search_site_no"

    '        '    Dim xmlreader As New Xml.XmlTextReader(sURL)
    '        '    Dim i As Integer = 0
    '        '    Dim FirstDate As String
    '        '    Dim LastDate As String
    '        '    Dim Count As String
    '        '    Do While xmlreader.Read
    '        '        Select Case i
    '        '            Case 7
    '        '                FirstDate = xmlreader.Value
    '        '            Case 11
    '        '                LastDate = xmlreader.Value
    '        '            Case 15
    '        '                Count = xmlreader.Value
    '        '        End Select
    '        '        i = i + 1
    '        '    Loop
    '        '    Result = Result + "DailyStreamflow" + "," + Count + "," + FirstDate + "," + LastDate + vbNewLine
    '        'End If

    '        'If WaterQuality.ToUpper = "TRUE" Then
    '        sURL = "http://nwis.waterdata.usgs.gov/nwis/qwdata?search_site_no=" + StationNumber + _
    '            "&search_site_no_match_type=exact&sort_key=site_no&group_key=NONE&sitefile_output_format=html_table&column_name=agency_cd" + _
    '            "&begin_date=" + Format(CDate(StartDate), "yyyy-MM-dd") + "&end_date=" + Format(CDate(EndDate), "yyyy-MM-dd") + _
    '            "&inventory_output=0&format=rdb_inventory&rdb_inventory_output=value&date_format=YYYY-MM-DD&rdb_compression=file" + _
    '            "&qw_sample_wide=0&list_of_search_criteria=search_site_no"

    '        stringReader = New IO.StringReader(GetHTTPFile(sURL))
    '        line = stringReader.ReadLine

    '        If Not line = Nothing Then

    '            Do While line.Substring(0, 1) = "#"
    '                line = stringReader.ReadLine
    '            Loop

    '            line = stringReader.ReadLine
    '            line = stringReader.ReadLine
    '            Result = "<usgs_nwis>" + vbNewLine + _
    '            "   <site>" + vbNewLine + _
    '            "      <site_no> " + StationNumber + "</site_no>"
    '            Do Until line Is Nothing
    '                values = line.Split()
    '                Result = Result + vbNewLine + _
    '                "      <record>" + vbNewLine + _
    '                "         <parameter_cd>" + values(2) + "</parameter_cd>" + vbNewLine + _
    '                "         <count_nu>" + values(3) + "</count_nu>" + vbNewLine + _
    '                "         <begin_date>" + values(4) + "</begin_date>" + vbNewLine + _
    '                "         <end_date>" + values(5) + "</end_date>" + vbNewLine + _
    '                "      </record>"
    '                line = stringReader.ReadLine
    '            Loop
    '            Result = Result + vbNewLine + _
    '            "   </site>" + _
    '            "</usgs_nwis>"
    '        End If

    '        Return Result

    '    End Function

    ''Description:="Given a USGS station number, a paremeter code, a start date, and an end date, " & _
    ''    " this method returns a time series.")> _
    'Private Function GetValues( _
    '                    ByVal SiteCode As String, _
    '                    ByVal ParameterCode As String, _
    '                    ByVal StartDate As String, _
    '                    ByVal EndDate As String) As String

    '    Dim StationsList As String
    '    Dim WaterQualityList As String
    '    Dim WaterQualityRequest As Boolean = False
    '    Dim DischargeRequest As Boolean = False
    '    Dim GroundwaterRequest As Boolean = False
    '    Dim HistoricalDataRequest As Boolean = False
    '    Dim RealTimeDataRequest As Boolean = False
    '    Dim URL As String
    '    Dim result As String
    '    Dim i As Integer

    '    'HISTORICAL OR REAL-TIME DATA REQUEST (OR BOTH)?
    '    Dim timespan As TimeSpan = New TimeSpan(31, 0, 0, 0, 0) 'real-time data is available for previous 31 days only
    '    Dim StartRealTime As Date = System.DateTime.Today.Subtract(timespan)
    '    If EndDate < StartRealTime Then ' interested in historical only
    '        HistoricalDataRequest = True
    '    End If
    '    If StartDate >= StartRealTime Then ' interested in real-time only
    '        RealTimeDataRequest = True
    '    End If
    '    If EndDate >= StartRealTime And StartDate <= StartRealTime Then ' interested in both realtime and historical
    '        HistoricalDataRequest = True
    '        RealTimeDataRequest = True
    '    End If

    '    'BUILD STATIONS LIST
    '    'StationsList = "site_no=" + SiteCode(0)
    '    'For i = 1 To SiteCode.Length - 1
    '    '    StationsList = StationsList + "&site_no=" & SiteCode(i)
    '    'Next
    '    StationsList = "site_no=" + SiteCode

    '    'BUILD WATER QUALITY PARAMETER LIST
    '    'For i = 0 To ParameterCodes.Length - 1
    '    '    If ParameterCodes(i) <> "00060" And ParameterCodes(i) <> "72019" Then
    '    '        WaterQualityList = WaterQualityList + "&parameter_cd=" + ParameterCodes(i)
    '    '        WaterQualityRequest = True
    '    '    Else
    '    '        If ParameterCodes(i) = "00060" Then
    '    '            WaterQualityList = WaterQualityList + "&parameter_cd=" + "00060"
    '    '            DischargeRequest = True
    '    '        End If
    '    '        If ParameterCodes(i) = "72019" Then
    '    '            WaterQualityList = WaterQualityList + "&parameter_cd=" + "72019"
    '    '            GroundwaterRequest = True
    '    '        End If
    '    '    End If
    '    'Next


    '    'DOES USER WANT RT and Historic data?
    '    If ParameterCode.ToUpper = "DAILY STREAMFLOW" Then
    '        WaterQualityList = ""
    '        DischargeRequest = True
    '    ElseIf ParameterCode.ToUpper = "GROUNDWATER LEVELS" Then
    '        WaterQualityList = ""
    '        GroundwaterRequest = True
    '    ElseIf ParameterCode = Nothing Then
    '        'skip
    '    Else
    '        WaterQualityList = "&parameter_cd=" + ParameterCode
    '        WaterQualityRequest = True
    '    End If

    '    'GENERATE START OF XML FILE
    '    result = "<usgs_nwis>" + vbNewLine + _
    '    "   <site>" + vbNewLine + _
    '    "      <site_no>" + SiteCode + "</site_no>" + vbNewLine + _
    '    "         <timeseries>"

    '    If HistoricalDataRequest Then
    '        'REQUEST WATER QUALITY VALUES
    '        If WaterQualityRequest = True Then
    '            URL = "http://nwis.waterdata.usgs.gov/nwis/qwdata?" + _
    '            StationsList + _
    '            WaterQualityList + _
    '            "&agency_cd=USGS" + _
    '            "&begin_date=" + Format(CDate(StartDate), "yyyy-MM-dd") + "&end_date=" & Format(CDate(EndDate), "yyyy-MM-dd") + _
    '            "&set_logscale_y=1&format=rdb&date_format=YYYY-MM-DD&rdb_compression=value&submitted_form=brief_list"

    '            result = result + GetValuesFromNWISWebpage(URL, ParameterCode)
    '        End If

    '        'REQUEST GROUND WATER VALUES
    '        If GroundwaterRequest = True Then 'translate to 72019
    '            URL = "http://nwis.waterdata.usgs.gov/nwis/gwlevels?" + _
    '           StationsList + _
    '            "&agency_cd=USGS" + _
    '            "&begin_date=" + Format(CDate(StartDate), "yyyy-MM-dd") + "&end_date=" & Format(CDate(EndDate), "yyyy-MM-dd") + _
    '            "&set_logscale_y=1&format=rdb&date_format=YYYY-MM-DD&rdb_compression=value&submitted_form=brief_list"

    '            result = result + GetValuesFromNWISWebpage(URL)
    '        End If

    '        'REQUEST DISCHARGE VALUES
    '        If DischargeRequest = True Then 'translate to 00060
    '            URL = "http://nwis.waterdata.usgs.gov/nwis/discharge?" + _
    '            StationsList + _
    '            "&agency_cd=USGS" + _
    '            "&begin_date=" + Format(CDate(StartDate), "yyyy-MM-dd") + "&end_date=" & Format(CDate(EndDate), "yyyy-MM-dd") + _
    '            "&set_logscale_y=1&format=rdb&date_format=YYYY-MM-DD&rdb_compression=value&submitted_form=brief_list"

    '            result = result + GetValuesFromNWISWebpage(URL)
    '        End If
    '    End If

    '    'REQUEST REAL-TIME VALUES
    '    If RealTimeDataRequest Then
    '        URL = "http://nwis.waterdata.usgs.gov/nwis/uv?" + _
    '        "format=rdb&period=31" + _
    '        "&" + StationsList + _
    '        WaterQualityList

    '        result = result + GetValuesFromNWISWebpage(URL, ParameterCode, StartDate, EndDate)
    '    End If


    '    'CLOSE XML TAGS
    '    result = result + vbNewLine + _
    '    "      </timeseries>" + vbNewLine + _
    '    "   </site>" + vbNewLine + _
    '    "</usgs_nwis>" + vbNewLine

    '    Return result

    'End Function



    ' Description:="Given a Hydrologivc Unit (HUC8) Code, a USGS parameter code, and USGS 
    ' Station Type
    ' this method returns a string where each record is a new line, and each record contains " & _
    ' the USGS station number
    Private Function GetStationsWithWQParameter( _
                    ByVal HUC As String, _
                    ByVal ParameterCode As String, _
                    ByVal StationCode As String) As ArrayList

        '---------------------------------------------------------------------------------------------------
        ' GET WATER QUALITY INVENTORY 
        '---------------------------------------------------------------------------------------------------

        'SAMPLE URL
        'http://waterdata.usgs.gov/nwis/dv?referred_module=qw&huc2_cd=01&station_type_cd=Y______&index_pmcode_00010=1&sort_key=site_no&group_key=NONE&format=sitefile_output&sitefile_output_format=rdb&column_name=site_no&range_selection=days&period=55555&begin_date=&end_date=&date_format=YYYY-MM-DD&rdb_compression=file&survey_email_address=&list_of_search_criteria=huc2_cd%2Cstation_type_cd%2Crealtime_parameter_selection
        Dim sURL As String = "http://waterdata.usgs.gov/nwis/dv?referred_module=qw"
        Dim sHUC As String = ""
        If (HUC.Length = 2) Then
            sHUC = "huc2_cd"
        ElseIf (HUC.Length = 8) Then
            sHUC = "huc_cd"
        End If
        sURL &= "&" & sHUC & "=" & HUC
        sURL &= "&station_type_cd=" + StationCode & _
        "&index_pmcode_" & ParameterCode & "=1" & _
        "&sort_key=site_no&group_key=NONE&format=sitefile_output&sitefile_output_format=rdb&column_name=site_no&column_name=huc_cd&range_selection=days&period=55555&begin_date=&end_date=&date_format=YYYY-MM-DD&rdb_compression=file&survey_email_address=&list_of_search_criteria="
        sURL &= sHUC & "%2Cstation_type_cd%2Crealtime_parameter_selection"
        'Pipe the stream to a higher level stream reader with the required encoding format. 
        Dim encode As Text.Encoding = System.Text.Encoding.GetEncoding("utf-8")
        Dim StreamReader As IO.StreamReader = GetHTTPStreamReader(sURL)
        Dim Line As String = StreamReader.ReadLine

        GetStationsWithWQParameter = New ArrayList

        If ((Line Is Nothing) OrElse (Not Line.StartsWith("#"))) Then
            Return GetStationsWithWQParameter '"No stations found that match request."
        End If

        'get past header lines
        Do Until Line.Substring(0, 1) <> "#"
            Line = StreamReader.ReadLine
        Loop

        'read in column header lines (there are 2)
        Line = StreamReader.ReadLine()
        Line = StreamReader.ReadLine()

        Dim Columns() As String
        Do Until Line Is Nothing
            Columns = Line.Split(vbTab)
            GetStationsWithWQParameter.Add(Columns(0)) ' station number
            Line = StreamReader.ReadLine
        Loop

    End Function

    ''Description:=Given a USGS  Hyrologic Unit (HUC2 or HUC8) ID, 
    '' a USGS Station Type Code (such as Y_______), and 
    '' a USGS Water Quality Parameter Code
    '' This method returns a string containing Parameter values
    'Private Function GetWQValuesForAllStations( _
    '                ByVal HUC As String, _
    '                ByVal ParameterCode As String, _
    '                ByVal StationCode As String) As String
    '    Dim stations As String = GetStationsWithWQParameter(HUC, ParameterCode, StationCode)
    '    If (stations = "No stations found that match request.") Then
    '        Return stations
    '    End If
    '    Dim station As String = stations
    '    Dim idx As Integer = 0
    '    Dim results As String = ""
    '    While (stations.IndexOf(vbCr) >= 0)
    '        idx = stations.IndexOf(vbCr)
    '        station = stations.Substring(0, idx + 2)
    '        stations = stations.Substring((idx + 2), (stations.Length - (idx + 2)))
    '        idx = station.IndexOf(vbCr)
    '        station = station.Remove(idx, 2)
    '        results += GetWQValues(station, ParameterCode, "01-01-1900", DateTime.Now().Date().ToString())
    '    End While
    '    Return results
    'End Function

    '(Description:="Given a USGS station number, a parameter code, a start date, and an end date " & _
    '   ", this method returns the url for a graph image of the parameter for that station.")> _
    'Private Function GetChart( _
    'ByVal SiteCode As String, _
    'ByVal ParameterCode As String, _
    'ByVal StartDate As String, _
    'ByVal EndDate As String) As String

    'Dim result As String = GetValues(SiteCode, ParameterCode, StartDate, EndDate)

    'Dim doc As New Xml.XmlDocument
    'doc.LoadXml(result)
    'Dim Dates(-1) As Date
    'Dim Values(-1) As Double
    'Dim count As Long = 0
    'Dim reader As New Xml.XmlNodeReader(doc)
    'Dim continue As Boolean = True
    'If reader.Read = False Then
    '    continue = False
    'End If
    'While continue
    '    If reader.IsStartElement Then
    '        If reader.Name = "Date" Then
    '            ReDim Preserve Dates(count)
    '            Try
    '                Dates(count) = Convert.ToDateTime(reader.ReadInnerXml())
    '            Catch
    '                Dates(count) = Nothing
    '            End Try
    '        ElseIf reader.Name = "Value" Then
    '            ReDim Preserve Values(count)
    '            Try
    '                Values(count) = Convert.ToDouble(reader.ReadInnerXml())
    '            Catch
    '                Values(count) = -888
    '            End Try
    '            count = count + 1
    '        Else
    '            continue = reader.Read()
    '        End If
    '    Else
    '        continue = reader.Read()
    '    End If
    'End While

    'If Dates.Length <= 0 Then Return "<Error>Unable to make chart.  No values where returned</Error>"

    ''make the tsobject
    'Dim ts As New GeospatialTimeSeries

    'Select Case ParameterCode.ToUpper
    '    Case "DAILY STREAMFLOW"
    '        With ts
    '            .DataType = GeospatialTimeSeries.DataTypeEnum.Average
    '            .TSIntervalUnit = GeospatialTimeSeries.TSIntervalEnum.Day
    '            .TSIntervalLength = 1
    '            .Units = "cfs"
    '            .IsRegular = True
    '            .TSValues = Values
    '            .TSDateTimes = Dates
    '            'ts.Shape = Nothing
    '            ts.FeatureID = -1
    '            ts.HydroCode = SiteCode
    '            ts.Origin = "http://nwis.waterdata.usgs.gov/"
    '        End With
    '    Case "GROUNDWATER LEVELS"
    '        With ts
    '            .DataType = GeospatialTimeSeries.DataTypeEnum.Instantaneous
    '            .TSIntervalUnit = -1
    '            .TSIntervalLength = -1
    '            .Units = "ft below land surface"
    '            .IsRegular = True
    '            .TSValues = Values
    '            .TSDateTimes = Dates
    '            'ts.Shape = Nothing
    '            ts.FeatureID = -1
    '            ts.HydroCode = SiteCode
    '            ts.Origin = "http://nwis.waterdata.usgs.gov/"
    '        End With
    '    Case Else ' assume this is a water quality parameter
    '        With ts
    '            .DataType = GeospatialTimeSeries.DataTypeEnum.Instantaneous
    '            .TSIntervalUnit = -1
    '            .TSIntervalLength = -1
    '            .Units = GetWQParamUnits(ParameterCode) 'this will take a while because it has to go over the web. should change asap.
    '            .IsRegular = True
    '            .TSValues = Values
    '            .TSDateTimes = Dates
    '            'ts.Shape = Nothing
    '            ts.FeatureID = -1
    '            ts.HydroCode = SiteCode
    '            ts.Origin = "http://nwis.waterdata.usgs.gov/"
    '        End With
    'End Select

    ''create a chart for the ts object
    'Dim ChartSpace As New OWC10.ChartSpace
    'ts.AddToChartSpace(ChartSpace)

    ''get a unique image name
    '' Reza Modified June 10, 11 PM
    ''  Dim ImageName As String = StationNumber & "_" & Format(CDate(StartDate), "MMddyyyy") & ".gif"
    'Dim ImageName As String = SiteCode & "_" & DateTime.Now.Millisecond.ToString() & ".gif"
    ''make an image of the chart
    'ChartSpace.ExportPicture("D:\ArcIMS\Output\" & ImageName, , "640", "322") ' "860" - 22 * 10, "366" - 22 * 2)

    'return link to image
    ' Return "<ChartURL>http://water.sdsc.edu" & "/Output/" & ImageName & "</ChartURL>"
    ' End Function

    '(Description:="Given a USGS station number, a start date, and an end date, " & _
    '     " this method returns a string with the daily averaged discharge time series date/times and values.")> _

    ''' <summary>
    ''' Return the USGS RDB format string containing daily discharge values for the given station during the given date range
    ''' </summary>
    ''' <param name="aStationNumber">Station to retrieve values from</param>
    ''' <param name="aStartDate">First date to get values from</param>
    ''' <param name="aEndDate">Last date to get values from</param>
    ''' <returns>RDB format string</returns>
    Private Function getDischargeValues(ByVal aStationNumber As String, _
                                        ByVal aStartDate As String, _
                                        ByVal aEndDate As String) As String
        'Build the URL
        'EXAMPLE:
        'http://nwis.waterdata.usgs.gov/nwis/discharge?site_no=02087500&agency_cd=USGS&begin_date=1%2F1%2F1990&end_date=1%2F1%2F2000
        '&set_logscale_y=1&format=rdb&date_format=YYYY-MM-DD&rdb_compression=value&submitted_form=brief_list

        'http://waterdata.usgs.gov/nwis/dv?cb_00060=on&format=rdb&begin_date=1800-01-01&end_date=2100-01-01&site_no=02223248&referred_module=sw

        'TODO: request with &rdb_compression=gz to save download time, use BinaryReader instead of StringReader below

        Dim sURL As String = "http://waterdata.usgs.gov/nwis/dv?cb_00060=on&format=rdb" _
                           & "&begin_date=" & Format(CDate(aStartDate), "yyyy-MM-dd") _
                           & "&end_date=" & Format(CDate(aEndDate), "yyyy-MM-dd") _
                           & "&site_no=" & aStationNumber

        Return DownloadURL(sURL)

        'Dim stringReader As New IO.StringReader(GetHTTPFile(sURL))
        'Dim lCurLine As String = stringReader.ReadLine

        'If lCurLine = Nothing Then Return Nothing

        'Do While line.Substring(0, 1) = "#"
        '    lCurLine = stringReader.ReadLine
        'Loop

        'lCurLine = stringReader.ReadLine
        'lCurLine = stringReader.ReadLine

        'Dim lResult As String = ""
        'Dim lValues() As String

        'Do Until lCurLine Is Nothing
        '    'lValues = line.Split()
        '    'lResult &= lValues(2) + "," + lValues(3) + vbNewLine
        '    getDischargeValues &= lCurLine
        '    lCurLine = stringReader.ReadLine
        'Loop
        'stringReader.Close()
    End Function

    ''(Description:="Given a USGS station number, this method returns an array with " & _
    ''"(1) the number of discharge observations, " & _
    ''"(2) the date/time of the first discharge observtion, and " & _
    ''"(3) the date/time of the last discharge observation for that station.")> _
    'Private Function getDischargeInfo( _
    '  ByVal StationID As String) As ArrayList

    '    'Sample url
    '    'http://nwis.waterdata.usgs.gov/nwis/qwdata?search_site_no=02089500
    '    '&search_site_no_match_type=exact&sort_key=site_no&group_key=NONE
    '    '&format=sitefile_output&sitefile_output_format=rdb&column_name=discharge_begin_date
    '    '&column_name=discharge_end_date&column_name=discharge_count_nu&begin_date=
    '    '&end_date=&inventory_output=0&rdb_inventory_output=value&date_format=
    '    'YYYY-MM-DD&rdb_compression=file&qw_sample_wide=0&list_of_search_criteria=search_site_no

    '    ' Hi Jon!,  I (Reza) changed this code Friday june 10 , 2005 , time = 3.00 pm
    '    ' Changed : "?search_site_no=02089500" to  "?search_site_no=" & StationID & _"

    '    Dim sURL As String = "http://nwis.waterdata.usgs.gov/nwis/qwdata?search_site_no=" & StationID & _
    '        "&search_site_no_match_type=exact&sort_key=site_no&group_key=NONE" & _
    '        "&format=sitefile_output&sitefile_output_format=rdb&column_name=discharge_begin_date" & _
    '        "&column_name=discharge_end_date&column_name=discharge_count_nu&begin_date=" & _
    '        "&end_date=&inventory_output=0&rdb_inventory_output=value&date_format=" & _
    '        "YYYY-MM-DD&rdb_compression=file&qw_sample_wide=0&list_of_search_criteria=search_site_no"

    '    Dim stringReader As IO.StreamReader = GetHTTPStreamReader(sURL)

    '    Dim line As String = stringReader.ReadLine
    '    Do While Not line Is Nothing
    '        If Not line.Substring(0, 1) = "#" Then
    '            line = stringReader.ReadLine
    '            line = stringReader.ReadLine
    '            Dim values As String() = line.Split(vbTab)
    '            Dim arraylist As New ArrayList
    '            arraylist.Add(values(2)) 'Count
    '            arraylist.Add(values(0)) 'BeginDate
    '            arraylist.Add(values(1)) 'EndDate
    '            Return arraylist
    '        End If
    '        line = stringReader.ReadLine
    '    Loop

    '    Return Nothing
    'End Function

    ''(Description:="Given a USGS well number, a start date, and an end date, " & _
    ''     " this method returns a string with the groundwater level time series (date/times and values).")> _
    'Private Function getGWLevelValues( _
    'ByVal WellNumber As String, _
    'ByVal StartDate As String, _
    'ByVal EndDate As String) As String

    '    'REMOVE THIS LINE ONCE YOU HAVE MADE SERVICE.
    '    'Return "To come ..."

    '    'Build the URL
    '    'EXAMPLE:
    '    'http://nwis.waterdata.usgs.gov/usa/nwis/gwlevels/?search_site
    '    '_no=355345078254701&search_site_no_match_type=exact&sort_key=site_no&group_key=NONE&
    '    'sitefile_output_format=html_table&column_name=agency_cd&column_name=site_no
    '    '&column_name=station_nm&column_name=lat_va&column_name=long_va&column_name=state_cd
    '    '&column_name=county_cd&column_name=alt_va&column_name=huc_cd&begin_date=1968-01-01
    '    '&end_date=1969-01-01&format=rdb&date_format=YYYY-MM-DD&rdb_compression=value&list_of_search_criteria=search_site_no

    '    Dim sURL As String
    '    'sURL = "http://nwis.waterdata.usgs.gov/nwis/discharge?site_no=" + WellNumber + _
    '    '       "&agency_cd=USGS&begin_date=" + Format(CDate(StartDate), "yyyy-MM-dd") + _
    '    '       "&end_date=" + Format(CDate(EndDate), "yyyy-MM-dd") + _
    '    '       "&set_logscale_y=1&format=rdb&date_format=YYYY-MM-DD&rdb_compression=&submitted_form=brief_list"
    '    sURL = "http://nwis.waterdata.usgs.gov/usa/nwis/gwlevels/?search_site_no=" + WellNumber + _
    '    "&search_site_no_match_type=exact&sort_key=site_no&group_key=NONE" + _
    '    "&sitefile_output_format=html_table&column_name=agency_cd&column_name=site_no" + _
    '    "&column_name=station_nm&column_name=lat_va&column_name=long_va&column_name=state_cd" + _
    '    "&column_name=county_cd&column_name=alt_va&column_name=huc_cd&begin_date=" + Format(CDate(StartDate), "yyyy-MM-dd") + _
    '    "&end_date=" + Format(CDate(EndDate), "yyyy-MM-dd") + _
    '    "&format=rdb&date_format=YYYY-MM-DD&rdb_compression=value&list_of_search_criteria=search_site_no"

    '    Dim stringReader As IO.StreamReader = GetHTTPStreamReader(sURL)
    '    Dim line As String = stringReader.ReadLine

    '    If line = Nothing Then Return Nothing

    '    Do While line.Substring(0, 1) = "#"
    '        line = stringReader.ReadLine
    '    Loop

    '    line = stringReader.ReadLine
    '    line = stringReader.ReadLine

    '    Dim Result As String = ""
    '    Dim values() As String
    '    Do Until line Is Nothing
    '        values = line.Split()
    '        Result = Result + values(2) + "," + values(4) + vbNewLine
    '        line = stringReader.ReadLine
    '    Loop

    '    Return Result

    'End Function

    '(Description:="Given a USGS parameter code, " & _
    '         " this method returns the parameter's measurement units.")> _
    Private Function GetWQParamUnits( _
    ByVal ParamCode As String) As String

        'Sample URL
        'http://nwis.waterdata.usgs.gov/nwis/pmcodes/pmcodes?pm_group=ALL&pm_search=
        '00615&format=rdb&show=parameter_cd&show=parameter_group_nm&show=parameter_nm

        Dim sURL As String = "http://nwis.waterdata.usgs.gov/nwis/pmcodes/pmcodes?pm_group=ALL&pm_search=" & _
        ParamCode & "&format=rdb&show=parameter_cd&show=parameter_group_nm&show=parameter_nm"

        Dim streamreader As IO.StreamReader = GetHTTPStreamReader(sURL)
        Dim line As String = streamreader.ReadLine
        line = streamreader.ReadLine
        Do Until Not line.StartsWith("#")
            line = streamreader.ReadLine
        Loop
        line = streamreader.ReadLine
        line = streamreader.ReadLine

        Dim ParamDescription As String = line.Split(vbTab)(2)
        Dim lLastComma As Long = ParamDescription.LastIndexOf(",")

        Return ParamDescription.Substring(lLastComma + 2)

    End Function

    '(Description:="Given a USGS parameter code, " & _
    '         " this method returns the parameter's name.")> _
    Private Function GetParameterInfo( _
    ByVal ParameterCode As String) As String

        'Sample URL
        'http://nwis.waterdata.usgs.gov/nwis/pmcodes/pmcodes?pm_group=ALL&pm_search=
        '00615&format=rdb&show=parameter_cd&show=parameter_group_nm&show=parameter_nm

        Dim sURL As String = "http://nwis.waterdata.usgs.gov/nwis/pmcodes/pmcodes?pm_group=ALL&pm_search=" & _
        ParameterCode & "&format=rdb&show=parameter_cd&show=parameter_group_nm&show=parameter_nm"

        Dim streamreader As IO.StreamReader = GetHTTPStreamReader(sURL)
        Dim line As String = streamreader.ReadLine
        Do Until line.Substring(0, 1) <> "#"
            line = streamreader.ReadLine
        Loop
        line = streamreader.ReadLine
        line = streamreader.ReadLine

        Dim ParamDescription As String = line.Split(vbTab)(2)
        Dim lLastComma As Long = ParamDescription.LastIndexOf(",")

        Dim Response As String = _
        "<usgs_nwis>" + vbNewLine + _
        "  <Name>" + ParamDescription.Substring(0, lLastComma) + "</Name>" + vbNewLine + _
        "  <Units>" + ParamDescription.Substring(lLastComma + 2) + "</Units>" + vbNewLine + _
        "  <Description>" + ParamDescription.Substring(0, lLastComma) + "</Description>" + vbNewLine + _
        "</usgs_nwis>"
        Return Response

    End Function

    '(Description:="Creates a KML file that can be used to view stations in Google Earth.")> _
    Private Function MakeKMLFileForNeuse( _
    ByVal ParameterCode As String) As String

        Return "In development"

    End Function


#End Region

#Region " Routines for reading NWIS "

    Private Function GetValuesFromNWISWebpage(ByVal url As String, Optional ByVal Variable As String = "", Optional ByVal StartDate As Date = #1/1/2000#, Optional ByVal EndDate As Date = #1/1/2000#) As String

        Dim stringReader As IO.StreamReader = GetHTTPStreamReader(url)
        Dim line As String = stringReader.ReadLine

        Dim Result As New Text.StringBuilder, values() As String

        'if nothing is returned then give this message
        If line = Nothing Then
            If line = Nothing Then
                Result.Append(vbNewLine + _
                       "            <record data = 'none'></record>")
            End If
        End If

        Do While line.Substring(0, 1) = "#"
            line = stringReader.ReadLine
        Loop

        'for real-time, need to read this header line
        Dim Header() As String = line.Split()

        line = stringReader.ReadLine
        line = stringReader.ReadLine

        'test if no records are returned
        If line = Nothing Then
            Result.Append(vbNewLine + _
                   "            <record data = 'none'></record>")
        End If

        Select Case url.Substring(36, 2)
            Case "di"  'discharge
                Result.Append(vbNewLine + "            <parameter_cd>Daily Streamflow</parameter_cd>" + vbNewLine + _
                "            <real_time>FALSE</real_time>")
                Do Until line Is Nothing
                    values = line.Split()
                    Result.Append(vbNewLine + _
                    "            <record>" + vbNewLine + _
                    "               <Date>" + values(2) + "</Date>" + vbNewLine + _
                    "               <Value>" + values(3) + "</Value>" + vbNewLine + _
                    "            </record>")
                    line = stringReader.ReadLine
                Loop
            Case "qw" 'water quality
                Result.Append(vbNewLine + "            <parameter_cd>" + Variable + "</parameter_cd>" + vbNewLine + _
                "            <real_time>FALSE</real_time>")
                Do Until line Is Nothing
                    values = line.Split()
                    Result.Append(vbNewLine + _
                    "            <record>" + vbNewLine + _
                    "               <Date>" + values(2) + " " + values(3) + "</Date>" + vbNewLine + _
                    "               <Value>" + values(5) + "</Value>" + vbNewLine + _
                    "            </record>")
                    line = stringReader.ReadLine
                Loop
            Case "gw" 'groundwater
                Result.Append(vbNewLine + "            <parameter_cd>Groundwater Levels</parameter_cd>" + vbNewLine + _
                "            <real_time>FALSE</real_time>")
                Do Until line Is Nothing
                    values = line.Split()
                    'Result = Result + values(1) + "," + values(2) + "," + values(4) + "," + "72019" + vbNewLine
                    Result.Append(vbNewLine + _
                    "            <record>" + vbNewLine + _
                    "               <Date>" + values(2) + "</Date>" + vbNewLine + _
                    "               <Value>" + values(4) + "</Value>" + vbNewLine + _
                    "            </record>")
                    line = stringReader.ReadLine
                Loop
            Case "uv" 'realtime
                Result.Append(vbNewLine + "            <parameter_cd>" + Variable + "</parameter_cd>" + vbNewLine + _
                "            <real_time>TRUE</real_time>")
                Do Until line Is Nothing
                    values = line.Split()
                    Dim i As Integer
                    For i = 4 To values.Length - 1
                        'Result = Result + values(1) + "," + values(2) + " " + values(3) + "," + values(4) + "," + Header(i - 1).Substring(3) + vbNewLine
                        If values(2) >= StartDate And values(2) < EndDate Then
                            Result.Append(vbNewLine + _
                                                "            <record>" + vbNewLine + _
                                                "               <Date>" + values(2) + " " + values(3) + "</Date>" + vbNewLine + _
                                                "               <Value>" + values(4) + "</Value>" + vbNewLine + _
                                                "            </record>")
                        End If
                    Next
                    line = stringReader.ReadLine
                Loop
        End Select

        Return Result.ToString

    End Function

    'Private Sub GetGageInfo(ByVal GageIDs() As String, ByRef XPathNavigator As Xml.XPath.XPathNavigator, ByRef XmlTextWriter As Xml.XmlTextWriter)
    '    Try
    '        If GageIDs.Length = 0 Then Exit Sub

    '        'build string of gageids
    '        Dim i As Long, sGageIDs As String
    '        sGageIDs = GageIDs(0)
    '        For i = 1 To GageIDs.Length - 1
    '            sGageIDs = sGageIDs & "%0D%0A" & GageIDs(i)
    '        Next

    '        'build url
    '        'http://waterdata.usgs.gov/nwis/inventory?multiple_site_no=0208397626%0D%0A0208397625%0D%0A0208397624%0D%0A0208397623&search_site_no_match_type=exact&sort_key=site_no&group_key=NONE&format=sitefile_output&sitefile_output_format=xml&column_name=agency_cd&column_name=site_no&column_name=station_nm&column_name=lat_va&column_name=long_va&column_name=state_cd&column_name=county_cd&column_name=alt_va&column_name=huc_cd&list_of_search_criteria=multiple_site_no
    '        'http://waterdata.usgs.gov/nwis/inventory?multiple_site_no=0208397626%0D%0A0208397625%0D%0A0208397624%0D%0A0208397623&search_site_no_match_type=exact&sort_key=site_no&group_key=NONE&format=sitefile_output&sitefile_output_format=rdb&column_name=agency_cd&column_name=site_no&column_name=station_nm&column_name=lat_va&column_name=long_va&column_name=state_cd&column_name=county_cd&column_name=alt_va&column_name=huc_cd&list_of_search_criteria=multiple_site_no
    '        Dim sURL As String = "http://waterdata.usgs.gov/nwis/inventory?multiple_site_no=" & _
    '        sGageIDs & _
    '        "&search_site_no_match_type=exact&sort_key=site_no&group_key=NONE&format=sitefile_" & _
    '        "output&sitefile_output_format=xml&column_name=agency_cd&column_name=site_no&column_name=station_nm" & _
    '        "&column_name=lat_va&column_name=long_va&column_name=state_cd&column_name=county_cd&column_" & _
    '        "name=alt_va&column_name=huc_cd&list_of_search_criteria=multiple_site_no"

    '        'get the website text
    '        Dim myWebRequest As Net.WebRequest = Net.WebRequest.Create(sURL)

    '        'Send the 'WebRequest' and wait for response. 
    '        myWebRequest.Timeout = 10000 'give it 10 seconds to respond
    '        Dim myWebResponse As Net.WebResponse
    '        myWebResponse = myWebRequest.GetResponse

    '        'Obtain a 'Stream' object associated with the response object. 
    '        Dim ReceiveStream As IO.Stream = myWebResponse.GetResponseStream

    '        Dim myXPathDocument As Xml.xpath.XPathDocument = New Xml.xpath.XPathDocument(ReceiveStream)
    '        Dim TextReader As New IO.StreamReader(recievestream)
    '        Dim s As String = TextReader.ReadToEnd
    '        Dim stream As IO.Stream = s
    '        Dim textwriter As IO.TextWriter = New IO.TextWriter
    '        XmlTextWriter = New Xml.XmlTextWriter(textwriter)
    '        XPathNavigator = myXPathDocument.CreateNavigator()

    '    Catch ex As Exception
    '        Throw New Exception("Error reading NWIS station attributes from web: " & ex.Message)
    '    End Try
    'End Sub

    'Will retrieve USGS streamflow data from NWIS site and return as arrays
    'Private Sub GetUSGSDailyStreamflow(ByVal GageID As String, ByVal StartDate As Date, ByVal EndDate As Date, _
    ' ByRef sDates() As String, ByRef sValues() As String)
    '    'Build the URL
    '    Dim sURL As String
    '    'sURL = "http://nwis.waterdata.usgs.gov/nwis/discharge?site_no=" & _
    '    '       GageID & _
    '    '       "&agency_cd=USGS&begin_date=" & _
    '    '       Format(StartDate, "yyyy-MM-dd") & _
    '    '       "&end_date=" & _
    '    '       Format(EndDate, "yyyy-MM-dd") & _
    '    '       "&set_logscale_y=1&format=rdb&date_format=YYYY-MM-DD&rdb_compression=&submitted_form=brief_list"

    '    'http://waterdata.usgs.gov/nwis/dv?cb_00060=on&format=rdb&begin_date=1800-01-01&end_date=2100-01-01&site_no=01591000&referred_module=sw

    '    sURL = "http://waterdata.usgs.gov/nwis/dv?cb_00060=on&format=rdb&date_format=YYYY-MM-DD" & _
    '           "&begin_date=" & Format(StartDate, "yyyy-MM-dd") & _
    '           "&end_date=" & Format(EndDate, "yyyy-MM-dd") & _
    '           "&site_no=" & GageID

    '    'Get the data from the web
    '    Dim sWebText As String
    '    Dim sWebLines() As String
    '    sWebText = GetHTTPString(sURL, 10)
    '    If Left(sWebText, 1) <> "#" Then 'could not download file
    '        Throw New Exception("Could not download data from NWIS")
    '    End If
    '    sWebLines = Split(sWebText, vbCrLf) 'Split the text from the into each line

    '    'Skip the header
    '    Dim lLBound As Long, lUBound As Long
    '    Dim i As Long
    '    Dim sLine As String
    '    lLBound = LBound(sWebLines)
    '    lUBound = UBound(sWebLines)
    '    If lUBound < lLBound Then
    '        Throw New Exception("No daily stream discharge values at station " & GageID & _
    '        " from " & StartDate & " to " & EndDate & ".")
    '    End If
    '    For i = lLBound To lUBound
    '        sLine = sWebLines(i)
    '        If Left(sLine, 1) <> "#" Then
    '            i = i + 2 'Skip two more lines to get past the header
    '            If i > lUBound Then
    '                Throw New Exception("No daily stream discharge values at station " & GageID & _
    '                " from " & StartDate & " to " & EndDate & ".")
    '            Else
    '                lLBound = i
    '                Exit For
    '            End If
    '        End If
    '    Next

    '    'Parse the lines
    '    Dim sDate As String, sValue As String
    '    Dim j As Long
    '    j = 0
    '    For i = lLBound To lUBound
    '        sLine = sWebLines(i)
    '        If Len(sLine) < 3 Then Exit For 'End of file
    '        sDate = ParseByToken(sLine, vbTab, 2)
    '        sValue = ParseByToken(sLine, vbTab, 3)
    '        ReDim Preserve sDates(j)
    '        ReDim Preserve sValues(j)
    '        sDates(j) = sDate
    '        sValues(j) = sValue
    '        j = j + 1
    '    Next

    '    If sDates Is Nothing Then
    '        Throw New Exception("No daily stream discharge values at station " & GageID & _
    '        " from " & StartDate & " to " & EndDate & ".")
    '    End If
    'End Sub

#End Region

#Region " Utility Routines "

    'Private Function ParseByToken(ByVal InputString As String, ByVal Token As String, ByVal TokenIndex As Long) As String
    '    'Returns string between token at the TokenIndex and the next token, or the end of the string
    '    'Returns empty string if item not found, or if Token or InputString are empty
    '    Dim i As Long, j As Long
    '    Dim lCount As Long
    '    Dim lPos As Long
    '    'Capture the string after the token at the given index
    '    For i = 1 To TokenIndex
    '        lPos = InStr(InputString, Token)
    '        If lPos = 0 Then
    '            'Either String1 is zero-length, or token was not found
    '            ParseByToken = ""
    '            Exit Function
    '        End If
    '        InputString = Right(InputString, Len(InputString) - lPos)
    '    Next i
    '    'Trim off the remaining part of the string at the next token or end of string
    '    lPos = InStr(InputString, Token)
    '    If lPos = 0 Then
    '        'No more tokens found, assume we're at end of string
    '        ParseByToken = InputString
    '    Else
    '        ParseByToken = Left(InputString, lPos - 1)
    '    End If

    'End Function

#End Region
End Class
