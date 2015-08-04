Imports atcData
Imports atcUtility
Imports MapWinUtility

Module modPublishGlobal
    Public g_ProgramDir As String = ""
    Public Const g_AppNameShort As String = "WEDOPublish"
    Public Const g_AppNameLong As String = "WEDO Publish"

    Public Structure MetdataStruct
        Dim ModelType As String
        Dim ModelVersion As String
        Dim ModelRunDate As Date
        Dim ModelStartDate As Date
        Dim ModelEndDate As Date
    End Structure

    Public MetadataInfo As MetdataStruct

    'Note: keep these arrays in sync!
    Public g_ConstituentsOfInterest() As String = {"FLOW", "TSS", "TKN", "NH3-N", "NO3-N", "NO2-N", "ORGN", "P", "PO4-P"}
    Public g_UciConstituentsOfInterest() As String = {"RO", "SSED:4", "PKST4:1", "DNUST:2", "DNUST:1", "DNUST:3", "PKST3:4", "PKST4:2", "DNUST:4"}
    Public g_HbnConstituentsOfInterest() As String = {"RO", "SSED-TOT", "N-TOT-CONC", "TAM-CONCDIS", "NO3-CONCDIS", "NO2-CONCDIS", "N-TOTORG-CONC", "P-TOT-CONC", "PO4-CONCDIS"}

    Public Sub WriteAttributes(ByVal aDataSet As atcData.atcDataSet, lWriter As IO.StreamWriter)
        Dim lValue As String
        lWriter.WriteLine("<attributes>")
        Dim lAllAttributes As SortedList = aDataSet.Attributes.ValuesSortedByName()
        For Each lAttName In lAllAttributes.Keys
            Dim lAttribute As atcDefinedValue = aDataSet.Attributes.GetDefinedValue(lAttName)
            If Not lAttribute.Definition.Calculated Then
                Dim lName As String = lAttribute.Definition.Name
                Select Case lName
                    Case "ID", "Key", "Data Source", "TGROUP", "COMPFG", "Point", "TSFORM", "VBTIME", "TSBYR", "DCODE", "HeaderComplete"
                    Case Else
                        Dim lType As String = ""
                        Select Case lAttribute.Definition.TypeString
                            Case "String" : lType = "Str"
                            Case "Integer" : lType = "Int"
                            Case "Single" : lType = "Sgl"
                            Case "Double" : lType = "Dbl"
                            Case "atcTimeUnit" : lType = "Str"
                            Case Else
                                Logger.Dbg("AttributeTypeNotDefined:" & lAttribute.Definition.TypeString)
                        End Select
                        If lType.Length > 0 Then
                            lValue = lAttribute.Value.ToString.TrimEnd
                            'lWriter.WriteLine("<att name=""" & lName & """ type=" & lType & " len=" & lValue.Length & ">" & lValue & "</att>")
                            'lWriter.WriteLine("<att name=""" & lName & """ type=" & lType & ">" & lValue & "</att>")
                            lWriter.WriteLine(lName & ", " & lValue)
                        End If
                End Select
            End If
        Next

        Dim lTimeseries As atcTimeseries = aDataSet

        lValue = lTimeseries.numValues
        'lWriter.WriteLine("<att name=""NumValues"" type=Int len=" & lValue.Length & ">" & lValue & "</att>")
        'lWriter.WriteLine("<att name=""NumValues"" type=Int>" & lValue & "</att>")
        lWriter.WriteLine("NumValues, " & lValue)

        lValue = Format(Date.FromOADate(lTimeseries.Dates.Value(0)), "yyyy/MM/dd HH:mm")
        'lWriter.WriteLine("<att name=""Start Date"" type=Str>" & lValue & "</att>")
        lWriter.WriteLine("Start Date, " & lValue)

        lWriter.WriteLine("</attributes>")
    End Sub

    Public Sub WriteValues(ByVal aTimeseries As atcData.atcTimeseries, lWriter As IO.StreamWriter)
        lWriter.WriteLine("<values>")
        For lIndex As Integer = 1 To aTimeseries.numValues
            'lWriter.WriteLine(Format(Date.FromOADate(aTimeseries.Dates.Value(lIndex)), "yyyy-MM-dd hh:mm") & vbTab & DoubleToString(aTimeseries.Value(lIndex)))
            lWriter.WriteLine(Format(Date.FromOADate(aTimeseries.Dates.Value(lIndex)), "yyyy-MM-dd HH:mm") & ", " & aTimeseries.Value(lIndex).ToString())
        Next
        lWriter.WriteLine("</values>")
    End Sub
End Module
