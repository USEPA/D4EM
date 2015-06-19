Imports System.Collections.ObjectModel
Imports atcData
Imports atcUCI
Imports atcUtility
Imports System.Text
Imports System.IO
Imports MapWinUtility.Strings

Module modIntermediatePoints  

    Public Sub AddIntermediateOutputToWDM(ByVal aRchID As Integer, _
                                          ByVal aUci As HspfUci, _
                                          ByVal aDataSource As atcWDM.atcDataSourceWDM, _
                                          ByVal aOutputInterval As Integer)
        'ByVal aOutputInterval As HspfOutputInterval

        Dim lTu As Integer = aOutputInterval
        Dim lScenario As String = System.IO.Path.GetFileNameWithoutExtension(aUci.Name)
        Dim lBaseDsn As Integer = 100
        Dim lDsn As Integer = 0

        'this is the way the outlet flow is added
        If aRchID > 0 Then 'add output at this location
            AddOutputWDMDataSet(aDataSource, lScenario, "RCH" & aRchID.ToString.Trim, "FLOW", lBaseDsn, 1, lTu, "", lDsn)
            aUci.AddExtTarget("RCHRES", aRchID.ToString.Trim, "HYDR", "RO", 1, 1, 1.0#, "AVER", "WDM1", lDsn, "FLOW", 1, "ENGL", "AGGR", "REPL")
        End If

    End Sub

    Public Sub AddUpstreamInflow(ByVal aRchID As Integer, _
                                 ByVal aUci As HspfUci, _
                                 ByVal aDataSource As atcWDM.atcDataSourceWDM, _
                                 ByVal aOutputInterval As Integer, _
                                 ByVal aBacterialOption As Boolean, _
                                 ByVal aChemicalOption As Boolean)
        'ByVal aOutputInterval As HspfOutputInterval

        Dim lTu As Integer = aOutputInterval
        Dim lScenario As String = System.IO.Path.GetFileNameWithoutExtension(aUci.Name)
        Dim lBaseDsn As Integer = 100
        Dim lDsn As Integer = 0

        '*** HYDR needs inflow in ac-ft/ivld, assume input as cfs (MFact = 0.0826)
        'WDM1  1010 FLOW     ENGL              SAME RCHRES  2      INFLOW IVOL  
        AddInputWDMDataSet(aUci, aDataSource, lScenario, "RCH" & aRchID.ToString.Trim, "FLOW", lBaseDsn, 1, lTu, "", lDsn)
        AddInputExtSource(aUci, lDsn, 1, "FLOW", 0.0826, "SAME", aRchID, "INFLOW", "IVOL", 0, 0)

        'if htrch is on, btu/ivld
        If aUci.OpnBlks("RCHRES").OperFromID(aRchID).Tables.Item("ACTIVITY").ParmValue("HTFG") = 1 Then
            'WDM1  1011 HEAT     ENGL              SAME RCHRES  2      INFLOW IHEAT  1
            AddInputWDMDataSet(aUci, aDataSource, lScenario, "RCH" & aRchID.ToString.Trim, "HEAT", lBaseDsn, 1, lTu, "", lDsn)
            AddInputExtSource(aUci, lDsn, 1, "HEAT", 1.0, "SAME", aRchID, "INFLOW", "IHEAT", 1, 0)
        End If

        'if sedmnt is on, ton/ivld
        If aUci.OpnBlks("RCHRES").OperFromID(aRchID).Tables.Item("ACTIVITY").ParmValue("SEDFG") = 1 Then
            'WDM1  1012 SED1     ENGL              SAME RCHRES  2      INFLOW ISED   1 1
            AddInputWDMDataSet(aUci, aDataSource, lScenario, "RCH" & aRchID.ToString.Trim, "SED1", lBaseDsn, 1, lTu, "", lDsn)
            AddInputExtSource(aUci, lDsn, 1, "SED1", 1.0, "SAME", aRchID, "INFLOW", "ISED", 1, 1)
            'WDM1  1013 SED2     ENGL              SAME RCHRES  2      INFLOW ISED   2 1
            AddInputWDMDataSet(aUci, aDataSource, lScenario, "RCH" & aRchID.ToString.Trim, "SED2", lBaseDsn, 1, lTu, "", lDsn)
            AddInputExtSource(aUci, lDsn, 1, "SED2", 1.0, "SAME", aRchID, "INFLOW", "ISED", 2, 1)
            'WDM1  1014 SED3     ENGL              SAME RCHRES  2      INFLOW ISED   3 1
            AddInputWDMDataSet(aUci, aDataSource, lScenario, "RCH" & aRchID.ToString.Trim, "SED3", lBaseDsn, 1, lTu, "", lDsn)
            AddInputExtSource(aUci, lDsn, 1, "SED3", 1.0, "SAME", aRchID, "INFLOW", "ISED", 3, 1)
        End If

        'if microbes simulated, org/ivld
        If aBacterialOption Then
            'WDM1  1015 MICR     ENGL              SAME RCHRES  2      INFLOW IDQAL  1 1
            AddInputWDMDataSet(aUci, aDataSource, lScenario, "RCH" & aRchID.ToString.Trim, "MICR", lBaseDsn, 1, lTu, "", lDsn)
            AddInputExtSource(aUci, lDsn, 1, "MICR", 1.0, "SAME", aRchID, "INFLOW", "IDQAL", 1, 1)
        End If

        'if land-applied chemical, lbs/ivld
        If aChemicalOption Then
            Dim lGQCount As Integer = 1
            If aBacterialOption Then lGQCount = 2 'both land-applied chemical and microbes
            'WDM1  1016 PSTD     ENGL              SAME RCHRES  2      INFLOW IDQAL  2 1
            AddInputWDMDataSet(aUci, aDataSource, lScenario, "RCH" & aRchID.ToString.Trim, "PSTD", lBaseDsn, 1, lTu, "", lDsn)
            AddInputExtSource(aUci, lDsn, 1, "PSTD", 1.0, "SAME", aRchID, "INFLOW", "IDQAL", lGQCount, 1)
            'WDM1  1017 PSTS1    ENGL              SAME RCHRES  2      INFLOW ISQAL  1 2
            AddInputWDMDataSet(aUci, aDataSource, lScenario, "RCH" & aRchID.ToString.Trim, "PSTS1", lBaseDsn, 1, lTu, "", lDsn)
            AddInputExtSource(aUci, lDsn, 1, "PSTS1", 1.0, "SAME", aRchID, "INFLOW", "ISQAL", 1, lGQCount)
            'WDM1  1018 PSTS2    ENGL              SAME RCHRES  2      INFLOW ISQAL  2 2
            AddInputWDMDataSet(aUci, aDataSource, lScenario, "RCH" & aRchID.ToString.Trim, "PSTS2", lBaseDsn, 1, lTu, "", lDsn)
            AddInputExtSource(aUci, lDsn, 1, "PSTS2", 1.0, "SAME", aRchID, "INFLOW", "ISQAL", 2, lGQCount)
            'WDM1  1019 PSTS3    ENGL              SAME RCHRES  2      INFLOW ISQAL  3 2
            AddInputWDMDataSet(aUci, aDataSource, lScenario, "RCH" & aRchID.ToString.Trim, "PSTS3", lBaseDsn, 1, lTu, "", lDsn)
            AddInputExtSource(aUci, lDsn, 1, "PSTS3", 1.0, "SAME", aRchID, "INFLOW", "ISQAL", 3, lGQCount)
        End If

    End Sub

    Public Sub AddInputExtSource(ByVal aHspfUci As HspfUci, _
                                 ByVal aDsn As Integer, _
                                 ByVal aWdmId As Integer, _
                                 ByVal aSourceMember As String, _
                                 ByVal aMfact As Double, _
                                 ByVal aTran As String, _
                                 ByVal aTargetId As Integer, _
                                 ByVal aTargetGroup As String, _
                                 ByVal aTargetMember As String, _
                                 ByVal aTargetSub1 As Integer, _
                                 ByVal aTargetSub2 As Integer)

        Dim lOpn As HspfOperation = aHspfUci.OpnBlks("RCHRES").OperFromID(aTargetId)
        Dim lConn As New HspfConnection
        lConn.Uci = aHspfUci
        lConn.Typ = 1
        lConn.Source.VolName = "WDM" & aWdmId.ToString
        lConn.Source.VolId = aDsn
        lConn.Source.Member = aSourceMember
        lConn.Source.MemSub1 = 1
        lConn.Ssystem = "ENGL"
        lConn.Sgapstrg = ""
        lConn.MFact = aMfact
        lConn.Tran = aTran
        lConn.Target.VolName = "RCHRES"
        lConn.Target.VolId = aTargetId
        lConn.Target.Group = aTargetGroup
        lConn.Target.Member = aTargetMember
        lConn.Target.MemSub1 = aTargetSub1
        lConn.Target.MemSub2 = aTargetSub2
        lConn.Comment = ""
        lConn.Target.Opn = lOpn
        aHspfUci.Connections.Add(lConn)
        lOpn.Sources.Add(lConn)
    End Sub

    Public Sub AddInputWDMDataSet(ByVal aUci As HspfUci, _
                                  ByRef aDataSource As atcDataSource, _
                                  ByRef aScenario As String, ByRef aLocation As String, ByRef aConstituent As String, _
                                  ByRef aBaseDsn As Integer, ByRef aWdmId As Integer, _
                                  ByRef aTUnit As Integer, ByRef aDescription As String, _
                                  ByRef aDsn As Integer)

        Dim lDsn As Integer = FindFreeDSN(aDataSource, aBaseDsn)
        Dim lGenericTs As New atcData.atcTimeseries(Nothing)
        With lGenericTs.Attributes
            .SetValue("ID", lDsn)
            .SetValue("Scenario", aScenario.ToUpper)
            .SetValue("Constituent", aConstituent.ToUpper)
            .SetValue("Location", aLocation.ToUpper)
            .SetValue("Description", aDescription)
            .SetValue("TU", aTUnit)
            .SetValue("TS", 1)
            .SetValue("TSTYPE", aConstituent.ToUpper)
            .SetValue("Data Source", aDataSource.Specification)
        End With

        'set the dates
        Dim lTsDate As atcData.atcTimeseries = New atcData.atcTimeseries(Nothing)
        Dim lNvals As Double
        Dim lSJDate As Double = aUci.GlobalBlock.SDateJ
        Dim lEJDate As Double = aUci.GlobalBlock.EdateJ
        lNvals = lEJDate - lSJDate
        Dim lDates(lNvals) As Double
        For lDateIndex As Integer = 0 To lNvals
            lDates(lDateIndex) = lSJDate + lDateIndex
        Next
        lTsDate.Values = lDates
        lGenericTs.Dates = lTsDate

        'now fill in the values with zeros
        Dim lValues(lNvals) As Double
        For lValueIndex As Integer = 0 To lNvals
            lValues(lValueIndex) = 0.0
        Next

        lGenericTs.Values = lValues
        Dim lAddedDsn As Boolean = aDataSource.AddDataSet(lGenericTs, 0)
        aDsn = lDsn
    End Sub

    Private Function FindFreeDSN(ByRef aDataSource As atcDataSource, ByVal aStartDSN As Integer) As Integer
        Dim lFreeDsn As Integer = aStartDSN + 1
        While Not GetDataSetFromDsn(aDataSource, lFreeDsn) Is Nothing
            lFreeDsn += 1
        End While
        Return lFreeDsn
    End Function

    Private Function GetDataSetFromDsn(ByRef aDataSource As atcDataSource, ByRef lDsn As Integer) As atcData.atcTimeseries
        If Not aDataSource Is Nothing Then
            For Each lDataSet As atcData.atcTimeseries In aDataSource.DataSets
                If lDsn = lDataSet.Attributes.GetValue("ID") Then
                    Return lDataSet
                End If
            Next
        End If
        Return Nothing
    End Function
End Module
