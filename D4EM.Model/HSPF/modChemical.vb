Imports atcUCI
Imports atcUtility
Imports MapWinUtility
Imports MapWinUtility.Strings
Imports System.Text

Module modChemical
    ''' <summary>
    ''' Update HSPF UCI for land-applied chemical simulations
    ''' </summary>
    ''' <param name="aUci">HSPF UCI object</param>
    ''' <remarks></remarks>
    Public Sub AddChemicalSimulation(ByVal aUci As HspfUci, ByVal aChemicalName As String, ByVal aMaximumSolubility As Double, _
                                     ByVal aPartitionCoeff() As Double, aFreundlichExp() As Double, ByVal aDegradationRate() As Double)
        'perlnd tables
        For Each lOpn As HspfOperation In aUci.OpnBlks("PERLND").Ids
            lOpn.Tables.Item("ACTIVITY").ParmValue("AIRTFG") = 1
            lOpn.Tables.Item("ACTIVITY").ParmValue("PSTFG") = 1
            lOpn.Tables.Item("ACTIVITY").ParmValue("SEDFG") = 1
            lOpn.Tables.Item("ACTIVITY").ParmValue("MSTLFG") = 1
            lOpn.Tables.Item("ACTIVITY").ParmValue("PESTFG") = 1
            'atemp-dat table
            If Not lOpn.TableExists("ATEMP-DAT") Then
                aUci.OpnBlks("PERLND").AddTableForAll("ATEMP-DAT", "PERLND")
            End If
            'pstemp-parm1 table
            If Not lOpn.TableExists("PSTEMP-PARM1") Then
                aUci.OpnBlks("PERLND").AddTableForAll("PSTEMP-PARM1", "PERLND")
            End If
            With lOpn.Tables.Item("PSTEMP-PARM1")
                .Parms(0).Value = 1
                .Parms(1).Value = 1
                .Parms(2).Value = 1
                .Parms(3).Value = 1
            End With
            'pstemp-parm2 table
            If Not lOpn.TableExists("PSTEMP-PARM2") Then
                aUci.OpnBlks("PERLND").AddTableForAll("PSTEMP-PARM2", "PERLND")
            End If
            With lOpn.Tables.Item("PSTEMP-PARM2")
                .Parms(0).Value = 24
                .Parms(1).Value = 0.5
                .Parms(2).Value = 24
                .Parms(3).Value = 0.5
                .Parms(4).Value = 40.0
                .Parms(5).Value = 0.0
            End With
            'mon-aslt table
            If Not lOpn.TableExists("MON-ASLT") Then
                aUci.OpnBlks("PERLND").AddTableForAll("MON-ASLT", "PERLND")
            End If
            With lOpn.Tables.Item("MON-ASLT")
                .Parms(0).Value = 33.9
                .Parms(1).Value = 33.9
                .Parms(2).Value = 37.5
                .Parms(3).Value = 43.0
                .Parms(4).Value = 49.6
                .Parms(5).Value = 55.8
                .Parms(6).Value = 59.8
                .Parms(7).Value = 59.8
                .Parms(8).Value = 53.5
                .Parms(9).Value = 44.8
                .Parms(10).Value = 40.0
                .Parms(11).Value = 35.7
            End With
            'mon-bslt table
            If Not lOpn.TableExists("MON-BSLT") Then
                aUci.OpnBlks("PERLND").AddTableForAll("MON-BSLT", "PERLND")
            End If
            With lOpn.Tables.Item("MON-BSLT")
                For i As Integer = 0 To 11
                    .Parms(i).Value = 0.5
                Next i
            End With
            'mon-ultp1 table
            If Not lOpn.TableExists("MON-ULTP1") Then
                aUci.OpnBlks("PERLND").AddTableForAll("MON-ULTP1", "PERLND")
            End If
            With lOpn.Tables.Item("MON-ULTP1")
                .Parms(0).Value = 34.6
                .Parms(1).Value = 34.6
                .Parms(2).Value = 40.7
                .Parms(3).Value = 50.7
                .Parms(4).Value = 62.4
                .Parms(5).Value = 74.0
                .Parms(6).Value = 81.2
                .Parms(7).Value = 81.2
                .Parms(8).Value = 69.7
                .Parms(9).Value = 53.2
                .Parms(10).Value = 45.2
                .Parms(11).Value = 37.7
            End With
            'mon-ultp2 table
            If Not lOpn.TableExists("MON-ULTP2") Then
                aUci.OpnBlks("PERLND").AddTableForAll("MON-ULTP2", "PERLND")
            End If
            With lOpn.Tables.Item("MON-ULTP2")
                For i As Integer = 0 To 11
                    .Parms(i).Value = 0.1
                Next i
            End With
            'mon-lgtp1 table
            If Not lOpn.TableExists("MON-LGTP1") Then
                aUci.OpnBlks("PERLND").AddTableForAll("MON-LGTP1", "PERLND")
            End If
            With lOpn.Tables.Item("MON-LGTP1")
                .Parms(0).Value = 50.0
                .Parms(1).Value = 50.0
                .Parms(2).Value = 55.0
                .Parms(3).Value = 60.0
                .Parms(4).Value = 60.0
                .Parms(5).Value = 65.0
                .Parms(6).Value = 65.0
                .Parms(7).Value = 65.0
                .Parms(8).Value = 60.0
                .Parms(9).Value = 55.0
                .Parms(10).Value = 55.0
                .Parms(11).Value = 50.0
            End With
            'section sedmnt
            If Not lOpn.TableExists("SED-PARM2") Then
                aUci.OpnBlks("PERLND").AddTableForAll("SED-PARM2", "PERLND")
            End If
            With lOpn.Tables.Item("SED-PARM2")
                .Parms(2).Value = 2.2
            End With
            If Not lOpn.TableExists("SED-PARM3") Then
                aUci.OpnBlks("PERLND").AddTableForAll("SED-PARM3", "PERLND")
            End If
            With lOpn.Tables.Item("SED-PARM3")
                .Parms(1).Value = 2.0
                .Parms(3).Value = 1.0
            End With
            'section mstlay tables optional
            'section pest
            If Not lOpn.TableExists("PEST-FLAGS") Then
                aUci.OpnBlks("PERLND").AddTableForAll("PEST-FLAGS", "PERLND")
            End If
            With lOpn.Tables.Item("PEST-FLAGS")
                .Parms(0).Value = 1  'npest
                .Parms(4).Value = 2  'adopfg(1)  adsorp/desorp
            End With
            If Not lOpn.TableExists("SOIL-DATA") Then
                aUci.OpnBlks("PERLND").AddTableForAll("SOIL-DATA", "PERLND")
            End If
            With lOpn.Tables.Item("SOIL-DATA")
                .Parms(0).Value = 0.25
                .Parms(1).Value = 5.71
                .Parms(2).Value = 41.3
                .Parms(3).Value = 60.0
                .Parms(4).Value = 62.4
                .Parms(5).Value = 79.2
                .Parms(6).Value = 81.7
                .Parms(7).Value = 85.5
            End With
            If Not lOpn.TableExists("PEST-ID") Then
                aUci.OpnBlks("PERLND").AddTableForAll("PEST-ID", "PERLND")
            End If
            lOpn.Tables.Item("PEST-ID").Parms(0).Value = aChemicalName
            If Not lOpn.TableExists("PEST-CMAX") Then
                aUci.OpnBlks("PERLND").AddTableForAll("PEST-CMAX", "PERLND")
            End If
            lOpn.Tables.Item("PEST-CMAX").Parms(0).Value = aMaximumSolubility   '242
            If Not lOpn.TableExists("PEST-SVALPM") Then
                aUci.OpnBlks("PERLND").AddTableForAll("PEST-SVALPM", "PERLND")
            End If
            With lOpn.Tables.Item("PEST-SVALPM")
                .Parms(0).Value = 0.0
                .Parms(1).Value = aPartitionCoeff(0) '4.0 
                .Parms(2).Value = aFreundlichExp(0)
            End With
            If Not lOpn.TableExists("PEST-SVALPM:2") Then
                aUci.OpnBlks("PERLND").AddTableForAll("PEST-SVALPM:2", "PERLND")
            End If
            With lOpn.Tables.Item("PEST-SVALPM:2")
                .Parms(0).Value = 0.0
                .Parms(1).Value = aPartitionCoeff(1) '4.0
                .Parms(2).Value = aFreundlichExp(1)
            End With
            If Not lOpn.TableExists("PEST-SVALPM:3") Then
                aUci.OpnBlks("PERLND").AddTableForAll("PEST-SVALPM:3", "PERLND")
            End If
            With lOpn.Tables.Item("PEST-SVALPM:3")
                .Parms(0).Value = 0.0
                .Parms(1).Value = aPartitionCoeff(2) '2.0
                .Parms(2).Value = aFreundlichExp(2)
            End With
            If Not lOpn.TableExists("PEST-SVALPM:4") Then
                aUci.OpnBlks("PERLND").AddTableForAll("PEST-SVALPM:4", "PERLND")
            End If
            With lOpn.Tables.Item("PEST-SVALPM:4")
                .Parms(0).Value = 0.0
                .Parms(1).Value = aPartitionCoeff(3) '2.0
                .Parms(2).Value = aFreundlichExp(3)
            End With
            If Not lOpn.TableExists("PEST-DEGRAD") Then
                aUci.OpnBlks("PERLND").AddTableForAll("PEST-DEGRAD", "PERLND")
            End If
            With lOpn.Tables.Item("PEST-DEGRAD")
                .Parms(0).Value = aDegradationRate(0) '0.12
                .Parms(1).Value = aDegradationRate(1) '0.045
                .Parms(2).Value = aDegradationRate(2) '0.04
                .Parms(3).Value = aDegradationRate(3) '0.04
            End With
            If Not lOpn.TableExists("PEST-STOR1") Then
                aUci.OpnBlks("PERLND").AddTableForAll("PEST-STOR1", "PERLND")
            End If
            If Not lOpn.TableExists("PEST-STOR1:2") Then
                aUci.OpnBlks("PERLND").AddTableForAll("PEST-STOR1:2", "PERLND")
            End If
            If Not lOpn.TableExists("PEST-STOR1:3") Then
                aUci.OpnBlks("PERLND").AddTableForAll("PEST-STOR1:3", "PERLND")
            End If
            If Not lOpn.TableExists("PEST-STOR1:4") Then
                aUci.OpnBlks("PERLND").AddTableForAll("PEST-STOR1:4", "PERLND")
            End If
        Next
        'implnd tables
        For Each lOpn As HspfOperation In aUci.OpnBlks("IMPLND").Ids
            lOpn.Tables.Item("ACTIVITY").ParmValue("ATMPFG") = 1
            'atemp-dat table
            If Not lOpn.TableExists("ATEMP-DAT") Then
                aUci.OpnBlks("IMPLND").AddTableForAll("ATEMP-DAT", "IMPLND")
            End If
        Next
        'rchres tables

        'get number of quals right outside opn loop
        'section gqual
        Dim lnGqual As Integer = 0
        If Not aUci.OpnBlks("RCHRES").Ids(0).TableExists("GQ-GENDATA") Then
            aUci.OpnBlks("RCHRES").AddTableForAll("GQ-GENDATA", "RCHRES")
            lnGqual = 1
        Else
            With aUci.OpnBlks("RCHRES").Ids(0).Tables.Item("GQ-GENDATA")
                lnGqual = .ParmValue("NGQUAL")
                lnGqual += 1
            End With
        End If

        For Each lOpn As HspfOperation In aUci.OpnBlks("RCHRES").Ids
            lOpn.Tables.Item("ACTIVITY").ParmValue("ADFG") = 1
            lOpn.Tables.Item("ACTIVITY").ParmValue("SEDFG") = 1
            lOpn.Tables.Item("ACTIVITY").ParmValue("GQALFG") = 1
            lOpn.Tables.Item("ACTIVITY").ParmValue("HTFG") = 1
            'ht-bed-flags table
            If Not lOpn.TableExists("HT-BED-FLAGS") Then
                aUci.OpnBlks("RCHRES").AddTableForAll("HT-BED-FLAGS", "RCHRES")
            End If
            With lOpn.Tables.Item("HT-BED-FLAGS")
                .Parms(0).Value = 1
                .Parms(1).Value = 3
                .Parms(2).Value = 55
            End With
            'heat-parm table
            If Not lOpn.TableExists("HEAT-PARM") Then
                aUci.OpnBlks("RCHRES").AddTableForAll("HEAT-PARM", "RCHRES")
            End If
            'ht-bed-parm table
            If Not lOpn.TableExists("HT-BED-PARM") Then
                aUci.OpnBlks("RCHRES").AddTableForAll("HT-BED-PARM", "RCHRES")
            End If
            'mon-ht-tgrnd table
            If Not lOpn.TableExists("MON-HT-TGRND") Then
                aUci.OpnBlks("RCHRES").AddTableForAll("MON-HT-TGRND", "RCHRES")
            End If
            With lOpn.Tables.Item("MON-HT-TGRND")
                .Parms(0).Value = 43.0
                .Parms(1).Value = 46.0
                .Parms(2).Value = 53.0
                .Parms(3).Value = 62.0
                .Parms(4).Value = 70.0
                .Parms(5).Value = 77.0
                .Parms(6).Value = 79.0
                .Parms(7).Value = 79.0
                .Parms(8).Value = 73.0
                .Parms(9).Value = 63.0
                .Parms(10).Value = 53.0
                .Parms(11).Value = 45.0
            End With
            'heat-init table
            If Not lOpn.TableExists("HEAT-INIT") Then
                aUci.OpnBlks("RCHRES").AddTableForAll("HEAT-INIT", "RCHRES")
            End If
            'section sedtrn
            If Not lOpn.TableExists("SED-GENPARM") Then
                aUci.OpnBlks("RCHRES").AddTableForAll("SED-GENPARM", "RCHRES")
            End If
            With lOpn.Tables.Item("SED-GENPARM")
                .Parms(0).Value = 100.0
            End With
            If Not lOpn.TableExists("SAND-PM") Then
                aUci.OpnBlks("RCHRES").AddTableForAll("SAND-PM", "RCHRES")
            End If
            With lOpn.Tables.Item("SAND-PM")
                .Parms(0).Value = 0.014
                .Parms(1).Value = 1.5
            End With
            If Not lOpn.TableExists("SILT-CLAY-PM") Then
                aUci.OpnBlks("RCHRES").AddTableForAll("SILT-CLAY-PM", "RCHRES")
            End If
            With lOpn.Tables.Item("SILT-CLAY-PM")
                .Parms(0).Value = 0.00063
                .Parms(1).Value = 0.003
                .Parms(2).Value = 2.2
                .Parms(3).Value = 0.03
                .Parms(4).Value = 0.07
                .Parms(5).Value = 0.03
            End With
            If Not lOpn.TableExists("SILT-CLAY-PM:2") Then
                aUci.OpnBlks("RCHRES").AddTableForAll("SILT-CLAY-PM:2", "RCHRES")
            End If
            With lOpn.Tables.Item("SILT-CLAY-PM:2")
                .Parms(0).Value = 0.000055
                .Parms(1).Value = 0.000022
                .Parms(2).Value = 2.0
                .Parms(3).Value = 0.02
                .Parms(4).Value = 0.05
                .Parms(5).Value = 0.045
            End With
            'section gqual
            lOpn.Tables.Item("GQ-GENDATA").Parms(0).Value = lnGqual
            lOpn.Tables.Item("GQ-GENDATA").Parms(1).Value = 1
            'gq-qaldata
            Dim lTableName As String = "GQ-QALDATA"
            If lnGqual > 1 Then lTableName = lTableName & ":" & lnGqual
            If Not lOpn.TableExists(lTableName) Then
                aUci.OpnBlks("RCHRES").AddTableForAll(lTableName, "RCHRES")
            End If
            With lOpn.Tables.Item(lTableName)
                .Parms(0).Value = aChemicalName
                .Parms(1).Value = 0.0
                .Parms(2).Value = "mg/l"
                .Parms(3).Value = 16017.0
                .Parms(4).Value = "lb"
            End With
            'gq-qalfg
            lTableName = "GQ-QALFG"
            If lnGqual > 1 Then lTableName = lTableName & ":" & lnGqual
            If Not lOpn.TableExists(lTableName) Then
                aUci.OpnBlks("RCHRES").AddTableForAll(lTableName, "RCHRES")
            End If
            With lOpn.Tables.Item(lTableName)
                .Parms(5).Value = 1
                .Parms(6).Value = 1
            End With
            'gq-gendecay
            lTableName = "GQ-GENDECAY"
            If lnGqual > 1 Then lTableName = lTableName & ":" & lnGqual
            If Not lOpn.TableExists(lTableName) Then
                aUci.OpnBlks("RCHRES").AddTableForAll(lTableName, "RCHRES")
                aUci.OpnBlks("RCHRES").Tables(lTableName).OccurIndex = lnGqual
            End If
            With lOpn.Tables.Item(lTableName)
                .Parms(0).Value = 0.004
                .Parms(1).Value = 1.07
            End With
            'gq-seddecay
            If Not lOpn.TableExists("GQ-SEDDECAY") Then
                aUci.OpnBlks("RCHRES").AddTableForAll("GQ-SEDDECAY", "RCHRES")
                aUci.OpnBlks("RCHRES").Tables("GQ-SEDDECAY").OccurIndex = lnGqual
            End If
            'gq-kd
            If Not lOpn.TableExists("GQ-KD") Then
                aUci.OpnBlks("RCHRES").AddTableForAll("GQ-KD", "RCHRES")
                aUci.OpnBlks("RCHRES").Tables("GQ-KD").OccurIndex = lnGqual
            End If
            With lOpn.Tables.Item("GQ-KD")
                .Parms(0).Value = 0.0000032
                .Parms(1).Value = 0.0000095
                .Parms(2).Value = 0.000019
                .Parms(3).Value = 0.0000032
                .Parms(4).Value = 0.0000095
                .Parms(5).Value = 0.000019
            End With
            'gq-adrate
            If Not lOpn.TableExists("GQ-ADRATE") Then
                aUci.OpnBlks("RCHRES").AddTableForAll("GQ-ADRATE", "RCHRES")
                aUci.OpnBlks("RCHRES").Tables("GQ-ADRATE").OccurIndex = lnGqual
            End If
            With lOpn.Tables.Item("GQ-ADRATE")
                .Parms(0).Value = 36.0
                .Parms(1).Value = 36.0
                .Parms(2).Value = 36.0
                .Parms(3).Value = 0.00001
                .Parms(4).Value = 0.00001
                .Parms(5).Value = 0.00001
            End With
            'gq-adtheta
            If Not lOpn.TableExists("GQ-ADTHETA") Then
                aUci.OpnBlks("RCHRES").AddTableForAll("GQ-ADTHETA", "RCHRES")
                aUci.OpnBlks("RCHRES").Tables("GQ-ADTHETA").OccurIndex = lnGqual
            End If
            'gq-sedconc
            If Not lOpn.TableExists("GQ-SEDCONC") Then
                aUci.OpnBlks("RCHRES").AddTableForAll("GQ-SEDCONC", "RCHRES")
                aUci.OpnBlks("RCHRES").Tables("GQ-SEDCONC").OccurIndex = lnGqual
            End If
        Next
        'mass-links
        If lnGqual > 1 Then
            'have to change default masslinks if doing both microbes and chemical
            For Each lMasslink As HspfMassLink In aUci.MassLinks
                With lMasslink
                    If .Source.VolName = "PERLND" And .Source.Group = "PEST" And _
                       .Target.VolName = "RCHRES" And .Target.Group = "INFLOW" Then
                        If .Target.Member = "IDQAL" Then
                            .Target.MemSub1 = lnGqual
                        ElseIf .Target.Member = "ISQAL" Then
                            .Target.MemSub2 = lnGqual
                        End If
                    End If
                End With
            Next
        End If

        'pesticide application through special actions
        '*** PESTICIDE APPLICATION OF 3.2 LB/AC APPLIED TO SURFACE ADSORBED STORAGE
        '*** (TOTAL LOAD DISTRIBUTED OVER THREE APPLICATIONS: 25%, 50%, 25%) 
        'PERLND  1         1969/05/25 12      3  SPS     2  1     2     0.800
        'PERLND  1         1969/06/02 12      3  SPS     2  1     2      1.60
        'PERLND  1         1969/06/09 12      3  SPS     2  1     2     0.800
        Dim lStartYear As String = aUci.GlobalBlock.SDate(0).ToString
        Dim lSAB As New HspfSpecialActionBlk
        aUci.SpecialActionBlk = lSAB
        For Each lOpn As HspfOperation In aUci.OpnBlks("PERLND").Ids
            If lOpn.Description.StartsWith("Agric") Then
                Dim lOpnId As String = lOpn.Id.ToString
                If lOpnId.Length = 1 Then lOpnId = "  " & lOpnId
                If lOpnId.Length = 2 Then lOpnId = " " & lOpnId
                Dim lSA1 As New HspfSpecialRecord
                lSA1.SpecType = HspfData.HspfSpecialRecordType.hAction
                lSA1.Text = "  PERLND" & lOpnId & "         " & lStartYear & "/05/25 12      3  SPS     2  1     2     1.000"
                aUci.SpecialActionBlk.Records.Add(lSA1)
                'Dim lSA2 As New HspfSpecialRecord
                'lSA2.SpecType = HspfData.HspfSpecialRecordType.hAction
                'lSA2.Text = "  PERLND" & lOpnId & "         " & lStartYear & "/06/02 12      3  SPS     2  1     2     1.600"
                'aUci.SpecialActionBlk.Records.Add(lSA2)
                'Dim lSA3 As New HspfSpecialRecord
                'lSA3.SpecType = HspfData.HspfSpecialRecordType.hAction
                'lSA3.Text = "  PERLND" & lOpnId & "         " & lStartYear & "/06/09 12      3  SPS     2  1     2     0.800"
                'aUci.SpecialActionBlk.Records.Add(lSA3)
            End If
        Next
    End Sub

    Public Sub AddChemicalOutputToWDM(ByVal aUci As HspfUci, ByVal aDataSource As atcWDM.atcDataSourceWDM, ByVal aOutputInterval As HspfOutputInterval)

        Dim lTu As Integer = aOutputInterval
        Dim lScenario As String = System.IO.Path.GetFileNameWithoutExtension(aUci.Name)
        Dim lBaseDsn As Integer = 100
        Dim lDsn As Integer = 0

        'write PEST TOPST -- total outflow of pesticide in lbs/ac/ivld
        For Each lOpn As HspfOperation In aUci.OpnBlks("PERLND").Ids
            AddOutputWDMDataSet(aDataSource, lScenario, "PER" & lOpn.Id.ToString, "TOPST", lBaseDsn, 1, lTu, "total outflow of chemical in lbs/ac/ivld", lDsn)
            aUci.AddExtTarget("PERLND", lOpn.Id, "PEST", "TOPST", 1, 1, 1.0#, "", "WDM1", lDsn, "TOPST", 1, "ENGL", "AGGR", "REPL")
        Next

        'write GQUAL DQAL -- dissolved conc of chemical in mg/l
        For Each lOpn As HspfOperation In aUci.OpnBlks("RCHRES").Ids
            'which gqual is this?
            Dim lnGqual As Integer = 1
            If lOpn.TableExists("GQ-GENDATA") Then
                With lOpn.Tables.Item("GQ-GENDATA")
                    lnGqual = .ParmValue("NGQUAL")
                End With
            End If
            AddOutputWDMDataSet(aDataSource, lScenario, "RCH" & lOpn.Id.ToString, "DQAL", lBaseDsn, 1, lTu, "dissolved conc of chemical in mg/l", lDsn)
            aUci.AddExtTarget("RCHRES", lOpn.Id, "GQUAL", "DQAL", lnGqual, 1, 1.0#, "", "WDM1", lDsn, "DQAL", 1, "ENGL", "AGGR", "REPL")
        Next

    End Sub
End Module
