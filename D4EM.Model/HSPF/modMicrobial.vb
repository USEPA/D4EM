Imports atcUCI
Imports atcUtility
Imports MapWinUtility
Imports MapWinUtility.Strings
Imports System.Text
Imports System.IO

Module modMicrobial
    ''' <summary>
    ''' Update HSPF UCI for pathogen simulations
    ''' </summary>
    ''' <param name="aUci">HSPF UCI object</param>
    ''' <remarks></remarks>
    Public Sub AddMicrobialSimulation(ByVal aUci As HspfUci)
        'perlnd tables
        For Each lOpn As HspfOperation In aUci.OpnBlks("PERLND").Ids
            lOpn.Tables.Item("ACTIVITY").ParmValue("PQALFG") = 1
            lOpn.Tables.Item("ACTIVITY").ParmValue("AIRTFG") = 1
            lOpn.Tables.Item("ACTIVITY").ParmValue("PSTFG") = 1
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
            'nquals table
            If Not lOpn.TableExists("NQUALS") Then
                aUci.OpnBlks("PERLND").AddTableForAll("NQUALS", "PERLND")
            End If
            lOpn.Tables.Item("NQUALS").ParmValue("NQUAL") = 1
            'qual-props table
            If Not lOpn.TableExists("QUAL-PROPS") Then
                aUci.OpnBlks("PERLND").AddTableForAll("QUAL-PROPS", "PERLND")
            End If
            With lOpn.Tables.Item("QUAL-PROPS")
                .Parms(0).Value = "Microbe"
                .Parms(1).Value = "#ORG"
                .Parms(2).Value = 0
                .Parms(3).Value = 0
                .Parms(4).Value = 0
                .Parms(5).Value = 1
                .Parms(6).Value = 1
                .Parms(7).Value = 1
                .Parms(8).Value = 0
                .Parms(9).Value = 1
                .Parms(10).Value = 0
            End With
            'qual-input table
            If Not lOpn.TableExists("QUAL-INPUT") Then
                aUci.OpnBlks("PERLND").AddTableForAll("QUAL-INPUT", "PERLND")
            End If
            With lOpn.Tables.Item("QUAL-INPUT")
                .Parms(0).Value = 0.0
                .Parms(1).Value = 0.0
                .Parms(2).Value = 0.0
                .Parms(3).Value = 10000.0
                .Parms(4).Value = 100000.0
                .Parms(5).Value = 1.64
                .Parms(6).Value = 0.0
                .Parms(7).Value = 0.0
            End With
            'mon-accum table
            If Not lOpn.TableExists("MON-ACCUM") Then
                aUci.OpnBlks("PERLND").AddTableForAll("MON-ACCUM", "PERLND")
            End If
            With lOpn.Tables.Item("MON-ACCUM")
                .Parms(0).Value = 10000.0
                .Parms(1).Value = 10000.0
                .Parms(2).Value = 10000.0
                .Parms(3).Value = 100000.0
                .Parms(4).Value = 100000.0
                .Parms(5).Value = 100000.0
                .Parms(6).Value = 100000.0
                .Parms(7).Value = 100000.0
                .Parms(8).Value = 100000.0
                .Parms(9).Value = 10000.0
                .Parms(10).Value = 10000.0
                .Parms(11).Value = 10000.0
            End With
            'mon-sqolim table
            If Not lOpn.TableExists("MON-SQOLIM") Then
                aUci.OpnBlks("PERLND").AddTableForAll("MON-SQOLIM", "PERLND")
            End If
            With lOpn.Tables.Item("MON-SQOLIM")
                .Parms(0).Value = 100000.0
                .Parms(1).Value = 100000.0
                .Parms(2).Value = 100000.0
                .Parms(3).Value = 1000000.0
                .Parms(4).Value = 1000000.0
                .Parms(5).Value = 1000000.0
                .Parms(6).Value = 1000000.0
                .Parms(7).Value = 1000000.0
                .Parms(8).Value = 1000000.0
                .Parms(9).Value = 100000.0
                .Parms(10).Value = 100000.0
                .Parms(11).Value = 100000.0
            End With
        Next
        'implnd tables
        For Each lOpn As HspfOperation In aUci.OpnBlks("IMPLND").Ids
            lOpn.Tables.Item("ACTIVITY").ParmValue("IQALFG") = 1
            lOpn.Tables.Item("ACTIVITY").ParmValue("ATMPFG") = 1
            'atemp-dat table
            If Not lOpn.TableExists("ATEMP-DAT") Then
                aUci.OpnBlks("IMPLND").AddTableForAll("ATEMP-DAT", "IMPLND")
            End If
            'nquals table
            If Not lOpn.TableExists("NQUALS") Then
                aUci.OpnBlks("IMPLND").AddTableForAll("NQUALS", "IMPLND")
            End If
            lOpn.Tables.Item("NQUALS").ParmValue("NQUAL") = 1
            'qual-props table
            If Not lOpn.TableExists("QUAL-PROPS") Then
                aUci.OpnBlks("IMPLND").AddTableForAll("QUAL-PROPS", "IMPLND")
            End If
            With lOpn.Tables.Item("QUAL-PROPS")
                .Parms(0).Value = "Microbe"
                .Parms(1).Value = "#ORG"
                .Parms(2).Value = 0
                .Parms(3).Value = 0
                .Parms(4).Value = 1
                .Parms(5).Value = 0
            End With
            'qual-input table
            If Not lOpn.TableExists("QUAL-INPUT") Then
                aUci.OpnBlks("IMPLND").AddTableForAll("QUAL-INPUT", "IMPLND")
            End If
            With lOpn.Tables.Item("QUAL-INPUT")
                .Parms(0).Value = 0.0
                .Parms(1).Value = 0.0
                .Parms(2).Value = 10000.0
                .Parms(3).Value = 100000.0
                .Parms(4).Value = 1.64
            End With
        Next
        'rchres tables
        For Each lOpn As HspfOperation In aUci.OpnBlks("RCHRES").Ids
            lOpn.Tables.Item("ACTIVITY").ParmValue("GQALFG") = 1
            lOpn.Tables.Item("ACTIVITY").ParmValue("ADFG") = 1
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
            'gq-gendata table
            If Not lOpn.TableExists("GQ-GENDATA") Then
                aUci.OpnBlks("RCHRES").AddTableForAll("GQ-GENDATA", "RCHRES")
            End If
            lOpn.Tables.Item("GQ-GENDATA").Parms(0).Value = 1
            lOpn.Tables.Item("GQ-GENDATA").Parms(1).Value = 1
            'gq-qaldata
            If Not lOpn.TableExists("GQ-QALDATA") Then
                aUci.OpnBlks("RCHRES").AddTableForAll("GQ-QALDATA", "RCHRES")
            End If
            With lOpn.Tables.Item("GQ-QALDATA")
                .Parms(0).Value = "Microbe"
                .Parms(1).Value = 100.0
                .Parms(2).Value = "OR/L"
                .Parms(3).Value = 0.0353
                .Parms(4).Value = "#ORG"
            End With
            'gq-qalfg
            If Not lOpn.TableExists("GQ-QALFG") Then
                aUci.OpnBlks("RCHRES").AddTableForAll("GQ-QALFG", "RCHRES")
            End If
            With lOpn.Tables.Item("GQ-QALFG")
                .Parms(0).Value = 0
                .Parms(1).Value = 0
                .Parms(2).Value = 0
                .Parms(3).Value = 0
                .Parms(4).Value = 0
                .Parms(5).Value = 1
                .Parms(6).Value = 0
            End With
            'gq-gendecay
            If Not lOpn.TableExists("GQ-GENDECAY") Then
                aUci.OpnBlks("RCHRES").AddTableForAll("GQ-GENDECAY", "RCHRES")
            End If
            With lOpn.Tables.Item("GQ-GENDECAY")
                .Parms(0).Value = 1
                .Parms(1).Value = 1.1
            End With
        Next
    End Sub

    Public Sub AddMicrobialOutputToWDM(ByVal aUci As HspfUci, ByVal aDataSource As atcWDM.atcDataSourceWDM, ByVal aOutputInterval As HspfOutputInterval)

        Dim lTu As Integer = aOutputInterval
        Dim lScenario As String = System.IO.Path.GetFileNameWithoutExtension(aUci.Name)
        Dim lBaseDsn As Integer = 100
        Dim lDsn As Integer = 0

        'write PWATER PERO
        'write PQUAL POQC
        For Each lOpn As HspfOperation In aUci.OpnBlks("PERLND").Ids
            AddOutputWDMDataSet(aDataSource, lScenario, "PER" & lOpn.Id.ToString, "PERO", lBaseDsn, 1, lTu, "", lDsn)
            aUci.AddExtTarget("PERLND", lOpn.Id, "PWATER", "PERO", 1, 1, 1.0#, "", "WDM1", lDsn, "PERO", 1, "ENGL", "AGGR", "REPL")
            AddOutputWDMDataSet(aDataSource, lScenario, "PER" & lOpn.Id.ToString, "POQC", lBaseDsn, 1, lTu, "", lDsn)
            aUci.AddExtTarget("PERLND", lOpn.Id, "PQUAL", "POQC", 1, 1, 1.0#, "", "WDM1", lDsn, "POQC", 1, "ENGL", "AGGR", "REPL")
        Next

        'write IWATER SURO
        'write IQUAL SOQC
        For Each lOpn As HspfOperation In aUci.OpnBlks("IMPLND").Ids
            AddOutputWDMDataSet(aDataSource, lScenario, "IMP" & lOpn.Id.ToString, "SURO", lBaseDsn, 1, lTu, "", lDsn)
            aUci.AddExtTarget("IMPLND", lOpn.Id, "IWATER", "SURO", 1, 1, 1.0#, "", "WDM1", lDsn, "SURO", 1, "ENGL", "AGGR", "REPL")
            AddOutputWDMDataSet(aDataSource, lScenario, "IMP" & lOpn.Id.ToString, "SOQC", lBaseDsn, 1, lTu, "", lDsn)
            aUci.AddExtTarget("IMPLND", lOpn.Id, "IQUAL", "SOQC", 1, 1, 1.0#, "", "WDM1", lDsn, "SOQC", 1, "ENGL", "AGGR", "REPL")
        Next

        'write HYDR RO
        'write GQUAL DQAL
        For Each lOpn As HspfOperation In aUci.OpnBlks("RCHRES").Ids
            AddOutputWDMDataSet(aDataSource, lScenario, "RCH" & lOpn.Id.ToString, "RO", lBaseDsn, 1, lTu, "", lDsn)
            aUci.AddExtTarget("RCHRES", lOpn.Id, "HYDR", "RO", 1, 1, 1.0#, "", "WDM1", lDsn, "RO", 1, "ENGL", "AGGR", "REPL")
            AddOutputWDMDataSet(aDataSource, lScenario, "RCH" & lOpn.Id.ToString, "DQAL", lBaseDsn, 1, lTu, "", lDsn)
            aUci.AddExtTarget("RCHRES", lOpn.Id, "GQUAL", "DQAL", 1, 1, 1.0#, "", "WDM1", lDsn, "DQAL", 1, "ENGL", "AGGR", "REPL")
        Next

    End Sub

    Public Sub AddOutputWDMDataSet(ByRef aDataSource As atcWDM.atcDataSourceWDM, _
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
        Dim lTsDate As atcData.atcTimeseries = New atcData.atcTimeseries(Nothing)
        lGenericTs.Dates = lTsDate

        Dim lAddedDsn As Boolean = aDataSource.AddDataSet(lGenericTs, 0)
        aDsn = lDsn
    End Sub

    Private Function FindFreeDSN(ByRef aDataSource As atcWDM.atcDataSourceWDM, ByVal aStartDSN As Integer) As Integer
        Dim lFreeDsn As Integer = aStartDSN + 1
        While Not GetDataSetFromDsn(aDataSource, lFreeDsn) Is Nothing
            lFreeDsn += 1
        End While
        Return lFreeDsn
    End Function

    Private Function GetDataSetFromDsn(ByRef aDataSource As atcWDM.atcDataSourceWDM, ByRef lDsn As Integer) As atcData.atcTimeseries
        If Not aDataSource Is Nothing Then
            For Each lDataSet As atcData.atcTimeseries In aDataSource.DataSets
                If lDsn = lDataSet.Attributes.GetValue("ID") Then
                    Return lDataSet
                End If
            Next
        End If
        Return Nothing
    End Function

    Public Sub RunMicrobialSourceModule(ByRef aUci As HspfUci, ByVal aUciName As String)
        'TODO: look in project folder, not assume this older folder structure
        Dim lFolder As String = IO.Path.GetDirectoryName(IO.Path.GetDirectoryName(aUciName)) & IO.Path.DirectorySeparatorChar
        '- SDMPB writes the table with columns “Sub-watershedID, CroplandAcres, ForestAcres, PastureAcres, BuiltupAcres” 
        'to a comma-delimited file 

        Dim animalFile As String = "AnimalLL.csv"
        Dim lAnimalLLFileName As String

        Dim projFolder As String = IO.Directory.GetParent(lFolder).FullName

        lAnimalLLFileName = IO.Path.Combine(projFolder, "LocalData", animalFile)
        'If Not FileExists(lAnimalLLFileName) Then
        '    Dim lTxtFileName As String = IO.Path.ChangeExtension(lAnimalLLFileName, ".txt")
        '    If FileExists(lTxtFileName) Then
        '        lAnimalLLFileName = lTxtFileName
        '    End If
        'End If
        'If Not FileExists(lAnimalLLFileName) Then
        '    lAnimalLLFileName = FindFile("Please locate AnimalLL", lAnimalLLFileName, aUserVerifyFileName:=True)
        'End If

        If Not IO.File.Exists(lAnimalLLFileName) Then
            Exit Sub
        End If

        lFolder = IO.Path.GetDirectoryName(lAnimalLLFileName) & IO.Path.DirectorySeparatorChar

        Dim lAreaFileName As String = lFolder & "Area.csv"
        WriteMSMAreaFile(aUci, lAreaFileName)

        '- SDMPB looks/prompts for a file of animal numbers by lat/long; rewrites with subwatershed ids.  
        'SDMPB needs to perform some aggregation here.  Suppose more than one rows in the user supplied file 
        'fall in a single catchment then SDMPB would sum of number of animals in the rows and make a single row (single catchment).  
        'In other words, the file produced by SDMPB has number of rows equal to or less than the number of rows in the user supplied file. 

        Dim lAnimalSubFileName As String = lFolder & "AnimalSub.csv"
        Dim lSepticsLLFileName As String = lFolder & "SepticsLL.csv"
        Dim lSepticsSubFileName As String = lFolder & "SepticsSub.csv"
        If GisUtil.IsLayer("NHDPlus Catchment Polygons") Then
            Dim lSubbasinLayerIndex As Integer = GisUtil.LayerIndex("NHDPlus Catchment Polygons")
            Dim lSubbasinSf As D4EM.Data.Layer = GisUtil.Layers(lSubbasinLayerIndex)
            RewriteLatLongAsSubbasins(lAnimalLLFileName, lAnimalSubFileName, lSubbasinSf)
            If IO.File.Exists(lSepticsLLFileName) Then
                RewriteLatLongAsSubbasins(lSepticsLLFileName, lSepticsSubFileName, lSubbasinSf)
            End If
        End If

        '- Then SDMPB invokes the Microbial Module as a function call with fixed file names

        'Microbial module is a .NET library, so it is function call.  
        'For now the user is not going to manipulate input terms, so no GUI for now, may be later.  
        'They can do this by making changes in the user supplied animal data file.  For now the files names are going to fixed.

        'Invoke the Microbial Source Module 
        Dim lErrorMsg As String = ""
        Dim lFCRatesFileName As String = lFolder & "FCProdRates.csv"
        Dim lDensitiesFileName As String = lFolder & "WildlifeDensities.csv"
        Dim lManureFileName As String = lFolder & "ManureApplication.csv"
        Dim lGrazingFileName As String = lFolder & "GrazingDays.csv"
        'Dim lSepticsFileName As String = lFolder & "SepticsData.csv"
        Dim lSepticsFileName As String = lFolder & "SepticsDataWatershed.csv"
        Dim lMonthlyFirstOrderDieOffRateConstantsFileName As String = lFolder & "MonthlyFirstOrderDieOffRateConstants.csv"
        Dim ldtBuiltup As New System.Data.DataTable
        Dim ldtCropland As New System.Data.DataTable
        Dim ldtPasture As New System.Data.DataTable
        Dim ldtForest As New System.Data.DataTable
        Dim ldtDirect As New System.Data.DataTable
        lErrorMsg = MSM.Watershed.getMicrobialLoadings(lAreaFileName, lFCRatesFileName, lAnimalSubFileName,
                                                       lDensitiesFileName, lManureFileName, lGrazingFileName,
                                                       lSepticsFileName, lSepticsSubFileName, lMonthlyFirstOrderDieOffRateConstantsFileName,
                                                       ldtBuiltup, ldtCropland, ldtPasture, ldtForest, ldtDirect)

        '- the Microbial Module writes a table for each land use category containing:
        'Sub-watershedID, 12 columns for monthly accumulation rates, 12 columns for monthly storage limits
        'and
        'Sub-watershedID, 12 columns for monthly direct instream contribution from cattle-in-stream and leaky septics

        If lErrorMsg.Length = 0 Then
            'SDMPB populates the UCI accordingly.
            Dim dieoff As Array = GetDieOff(lMonthlyFirstOrderDieOffRateConstantsFileName)
            PopulateMicrobeParms(aUci, ldtCropland, ldtForest, ldtPasture, ldtBuiltup, ldtDirect, dieoff)
        Else
            Logger.Msg("Warning: " & lErrorMsg)
        End If

        'Notes
        'HSPF cannot do anything with separate loads by animal type, so they can be all lumped together.  
        'HSPF may or may not be able to handle input parameters by sub-watershedID, depending upon options selected in SDMPB.   
        ' SDMPB can be responsible for aggregating the sub-watershed output into the input parameters as needed by HSPF.  

    End Sub

    Public Sub WriteMSMAreaFile(ByVal aUCI As HspfUci, ByVal aAreaFileName As String)
        Dim lSB As New StringBuilder
        lSB.AppendLine("SubWatershedID,CroplandAcres,ForestAcres,PastureAcres,BuiltupAcres,FractionOfBuiltup_CommercialAndServices,FractionOfBuiltup_MxdUrban,FractionOfBuiltup_Residential,FractionOfBuiltup_Transport_Comunication_Utilities")
        Dim lCropAcres As Double = 0.0
        Dim lForestAcres As Double = 0.0
        Dim lPastureAcres As Double = 0.0
        Dim lBuiltAcres As Double = 0.0
        Dim lTotalAcres As Double = 0.0
        Dim lLU As String = ""

        Dim lOpnBlk As HspfOpnBlk = aUCI.OpnBlks("RCHRES")
        For Each lRchOper As HspfOperation In aUCI.OpnBlks("RCHRES").Ids
            If Not lRchOper Is Nothing Then
                'loop through all connections looking for this oper as target
                lCropAcres = 0.0
                lForestAcres = 0.0
                lPastureAcres = 0.0
                lBuiltAcres = 0.0
                For Each lConn As HspfConnection In aUCI.Connections
                    If lConn.Typ = 3 Then 'schematic
                        If Not lConn.Target.Opn Is Nothing Then
                            If lConn.Target.Opn.Id = lRchOper.Id And _
                               lConn.Target.Opn.Name = lRchOper.Name Then
                                'found a source to this rchres
                                If lConn.Source.VolName = "PERLND" Or lConn.Source.VolName = "IMPLND" Then
                                    If Not lConn.Source.Opn Is Nothing Then
                                        lLU = lConn.Source.Opn.Description.ToUpper
                                        If lLU.Contains("URBAN") Then
                                            lBuiltAcres += lConn.MFact
                                            lTotalAcres += lConn.MFact
                                        ElseIf lLU.Contains("FOREST") Then
                                            lForestAcres += lConn.MFact
                                            lTotalAcres += lConn.MFact
                                        ElseIf lLU.Contains("PASTU") Then
                                            lPastureAcres += lConn.MFact
                                            lTotalAcres += lConn.MFact
                                        ElseIf lLU.Contains("CROP") Then
                                            lCropAcres += lConn.MFact
                                            lTotalAcres += lConn.MFact
                                        End If
                                    End If
                                End If
                            End If
                        End If
                    End If
                Next
                lSB.AppendLine(lRchOper.Id.ToString & "," & lCropAcres.ToString & "," & lForestAcres.ToString & "," & lPastureAcres.ToString & "," & lBuiltAcres.ToString & ",0.25,0.25,0.25,0.25")
            End If
        Next

        SaveFileString(aAreaFileName, lSB.ToString)
    End Sub

    Public Sub RewriteLatLongAsSubbasins(ByVal aLatLonFileName As String, ByVal aSubFileName As String, ByVal aSubbasinsLayer As D4EM.Data.Layer)
        If System.IO.File.Exists(aLatLonFileName) Then

            Dim lDelim As String = ","
            Dim lQuote As String = """"
            Dim lLat As Double = 0.0
            Dim lLong As Double = 0.0
            Dim lPolyIndex As Integer = 0
            Dim lRestOfLine As String = ""
            Dim lCurrentRecord As String
            Dim lStreamReader As New IO.StreamReader(aLatLonFileName)
            Dim lFirstTime As Boolean = True
            Dim lHeader As String = ""
            Dim lSaveHeader As String = ""
            Dim lNumCols As Integer = 0
            Dim lNumRows As Integer = 0
            Dim lPolys() As Integer = {0}
            Dim lLines() As String = {""}
            Dim lSubbasinFieldIndex As Integer = aSubbasinsLayer.FieldIndex("SUBBASIN")

            Try
                Do
                    lCurrentRecord = lStreamReader.ReadLine
                    If lCurrentRecord Is Nothing Then
                        Exit Do
                    Else
                        If lFirstTime Then
                            'this is the header
                            lHeader = StrSplit(lCurrentRecord, lDelim, lQuote)
                            lHeader = StrSplit(lCurrentRecord, lDelim, lQuote)
                            lHeader = lCurrentRecord
                            lSaveHeader = lCurrentRecord
                            Do While lCurrentRecord.Length > 0
                                lNumCols += 1
                                lHeader = StrSplit(lCurrentRecord, lDelim, lQuote)
                            Loop
                            lFirstTime = False
                        Else
                            'regular line
                            lLat = StrSplit(lCurrentRecord, lDelim, lQuote)
                            lLong = StrSplit(lCurrentRecord, lDelim, lQuote)
                            lRestOfLine = lCurrentRecord
                            D4EM.Geo.SpatialOperations.ProjectPoint(lLong, lLat, DotSpatial.Projections.KnownCoordinateSystems.Geographic.World.WGS1984, aSubbasinsLayer.Projection)
                            lPolyIndex = aSubbasinsLayer.CoordinatesInShapefile(lLong, lLat)
                            ReDim Preserve lPolys(lNumRows + 1)
                            ReDim Preserve lLines(lNumRows + 1)
                            If lPolyIndex > -1 And lSubbasinFieldIndex > -1 Then
                                'need to translate index into subbasin id
                                lPolyIndex = aSubbasinsLayer.CellValue(lSubbasinFieldIndex, lPolyIndex)
                            Else
                                lPolyIndex = -1
                            End If
                            lPolys(lNumRows) = lPolyIndex
                            lLines(lNumRows) = lRestOfLine
                            lNumRows += 1
                        End If
                    End If
                Loop
            Catch e As ApplicationException
                'Logger.Msg("Problem reading file " & lAnimalLLFileName & vbCrLf & e.Message, "File Problem")
            End Try

            'parse remaining data into array
            Dim lData(,) As Integer
            If lNumCols > 0 Then
                lSaveHeader = "BeefCowCount,SwineCount,DairyCowCount,ChickenCount,HorseCount,SheepCount,OtherAgAnimalCount"
                ReDim lData(lNumRows, lNumCols + 1)
            Else 'Only have lat and long columns, assume each row is one instance to be counted
                lSaveHeader = "SepticsCount"
                ReDim lData(lNumRows, 2)
            End If
            For lIndex As Integer = 0 To lNumRows - 1
                lData(lIndex, 0) = lPolys(lIndex)
                If lNumCols > 0 Then
                    lRestOfLine = lLines(lIndex)
                    For lCol As Integer = 1 To lNumCols
                        If Not Integer.TryParse(StrSplit(lRestOfLine, lDelim, lQuote), lData(lIndex, lCol)) Then
                            Logger.Msg("Could not parse value in line '" & lLines(lIndex) & "' #" & lIndex & " in file " & aLatLonFileName, "RewriteLatLongAsSubbasins")
                        End If
                    Next
                End If
            Next

            'combine rows of same subbasin id
            Dim lThisSubbasin As Integer = -1
            For lIndex As Integer = 0 To lNumRows - 1
                lThisSubbasin = lData(lIndex, 0)
                If lThisSubbasin > -1 Then
                    If lNumCols = 0 Then 'Start count at one for the subbasin this row is in
                        lData(lIndex, 1) = 1
                    End If
                    For lSearchIndex As Integer = lIndex + 1 To lNumRows - 1
                        If lData(lSearchIndex, 0) = lThisSubbasin Then
                            'found another row from this subbasin at lSearchIndex, add its values into row lIndex
                            lData(lSearchIndex, 0) = -1 'clear the subbasin column in the found row to mark it as found
                            If lNumCols > 0 Then
                                For lCol As Integer = 1 To lNumCols
                                    lData(lIndex, lCol) += lData(lSearchIndex, lCol)
                                Next
                            Else 'No data columns to add, so add one to count column instead
                                lData(lIndex, 1) += 1
                                Logger.Dbg("Add count(" & lThisSubbasin & ") = " & lData(lIndex, 1))
                            End If
                        End If
                    Next
                End If
            Next

            'now write out by subbasin
            Dim lSB2 As New StringBuilder
            lSB2.AppendLine("SubWatershedID," & lSaveHeader)
            Dim lThisSubbasinId As Integer = 0
            Dim lFound As Boolean = False
            For lPolyIndex = 1 To aSubbasinsLayer.AsFeatureSet.NumRows
                lThisSubbasinId = aSubbasinsLayer.CellValue(lSubbasinFieldIndex, lPolyIndex - 1)
                lRestOfLine = lThisSubbasinId
                lFound = False
                For lSearchIndex As Integer = 0 To lNumRows - 1
                    If lThisSubbasinId = lData(lSearchIndex, 0) Then
                        'found data for this one, write it
                        If lNumCols > 0 Then
                            For lCol As Integer = 1 To lNumCols
                                lRestOfLine &= "," & lData(lSearchIndex, lCol)
                            Next
                        Else
                            lRestOfLine &= "," & lData(lSearchIndex, 1)
                        End If
                        lSB2.AppendLine(lRestOfLine)
                        lFound = True
                    End If
                Next
                If Not lFound Then
                    If lNumCols > 0 Then
                        For lCol As Integer = 1 To lNumCols
                            lRestOfLine &= "," & "0"
                        Next
                    Else
                        lRestOfLine &= "," & "0"
                    End If
                    lSB2.AppendLine(lRestOfLine)
                End If
            Next
            SaveFileString(aSubFileName, lSB2.ToString)
        Else
            Throw New Exception("Missing " & aLatLonFileName)
        End If
    End Sub

    Public Sub ReadMSMData(ByVal aFileName As String, ByRef aData(,) As Double)
        'read output file from microbial source module
        If System.IO.File.Exists(aFileName) Then
            Dim lMSMStreamReader As New IO.StreamReader(aFileName)
            Dim lFirstTime As Boolean = True
            Dim lNumRows As Integer = 0
            Dim lDelim As String = ","
            Dim lQuote As String = """"
            Dim lCurrentRecord As String
            Try
                Do
                    lCurrentRecord = lMSMStreamReader.ReadLine
                    If lCurrentRecord Is Nothing Then
                        Exit Do
                    Else
                        If lFirstTime Then
                            'this is the header
                            lFirstTime = False
                        Else
                            'regular line
                            lNumRows += 1
                        End If
                    End If
                Loop
            Catch e As ApplicationException
                'Logger.Msg("Problem reading file " & aFileName & vbCrLf & e.Message, "File Problem")
            End Try
            lMSMStreamReader.Close()

            'now read the file again populating the data array
            Dim lMSMStreamReader2 As New IO.StreamReader(aFileName)
            lFirstTime = True
            Dim lIndex As Integer = 0
            Dim lSub As Integer = 0
            Dim lVals(24) As Double
            ReDim aData(lNumRows, 25)
            lNumRows = 0
            Try
                Do
                    lCurrentRecord = lMSMStreamReader2.ReadLine
                    If lCurrentRecord Is Nothing Then
                        Exit Do
                    Else
                        If lFirstTime Then
                            'this is the header
                            lFirstTime = False
                        Else
                            'regular line
                            aData(lNumRows, 0) = StrSplit(lCurrentRecord, lDelim, lQuote)
                            Dim lNumCols = 0
                            Do While lCurrentRecord.Length > 0
                                lNumCols += 1
                                aData(lNumRows, lNumCols) = StrSplit(lCurrentRecord, lDelim, lQuote)
                            Loop
                            lNumRows += 1
                        End If
                    End If
                Loop
            Catch e As ApplicationException
                'Logger.Msg("Problem reading file " & aFileName & vbCrLf & e.Message, "File Problem")
            End Try
            lMSMStreamReader2.Close()
        End If
    End Sub

    Public Sub PopulateMicrobeParms(ByVal aUci As HspfUci, ByVal aCropData As System.Data.DataTable, ByVal aForestData As System.Data.DataTable, ByVal aPastureData As System.Data.DataTable, ByVal aBuiltupData As System.Data.DataTable, ByVal aDirectData As System.Data.DataTable, ByRef dieoff As Array)
        'get these values from microbial source module

        With aUci
            'set accumulation rates and storage limits for perlnds 
            Dim lPerlndOperations As HspfOperations = .OpnBlks("PERLND").Ids
            For lPerlndIndex As Integer = 1 To lPerlndOperations.Count
                Dim lHspfOperation As HspfOperation = lPerlndOperations(lPerlndIndex - 1)
                PopulateAccumSqolim(lHspfOperation, aCropData, aForestData, aPastureData, aBuiltupData, aDirectData, dieoff)
            Next

            'set accumulation rates and storage limits for implnds 
            Dim lImplndOperations As HspfOperations = aUci.OpnBlks("IMPLND").Ids
            For lImplndIndex As Integer = 1 To lImplndOperations.Count
                Dim lHspfOperation As HspfOperation = lImplndOperations(lImplndIndex - 1)
                PopulateAccumSqolim(lHspfOperation, aCropData, aForestData, aPastureData, aBuiltupData, aDirectData, dieoff)
            Next

            'set direct deposition to rchres operations
            Dim lRchresOperations As HspfOperations = .OpnBlks("RCHRES").Ids
            'bacteria uses atmospheric dry deposition of counts
            For lRchresIndex As Integer = 1 To lRchresOperations.Count
                Dim lRchresOperation As HspfOperation = lRchresOperations(lRchresIndex - 1)
                Dim lRowToUse As Integer = -1
                If Not aDirectData Is Nothing Then
                    For lRow As Integer = 0 To aDirectData.Rows.Count - 1
                        If aDirectData.Rows(lRow)(0) = lRchresOperation.Id Then
                            lRowToUse = lRow
                            Exit For
                        End If
                    Next
                    If lRowToUse > -1 Then
                        'add month data table to represent direct deposition to stream
                        Dim lMonthDataTable As New HspfMonthDataTable
                        With lMonthDataTable
                            For lMonthIndex As Integer = 1 To 12
                                .MonthValue(lMonthIndex) = aDirectData.Rows(lRowToUse)(lMonthIndex)
                            Next
                            .Comment = "***  atmospheric dry deposition fluxes" & vbCrLf & _
                                       "***  used to input loads from cattle in stream and leaky septics (counts/day)" & vbCrLf & _
                                       "<val-><val-><val-><val-><val-><val-><val-><val-><val-><val-><val-><val->***"
                        End With
                        If .MonthData Is Nothing Then
                            lMonthDataTable.Id = 1
                            Dim lMonthData As New HspfMonthData
                            lMonthData.Uci = aUci
                        Else
                            lMonthDataTable.Id = .MonthData.MonthDataTables.Count + 1
                        End If
                        .MonthData.MonthDataTables.Add(lMonthDataTable)
                        'point to month data table from ad flags 
                        Dim lGQTable As HspfTable
                        If Not lRchresOperation.TableExists("GQ-AD-FLAGS") Then
                            aUci.OpnBlks("RCHRES").AddTableForAll("GQ-AD-FLAGS", "RCHRES")
                        End If
                        lGQTable = lRchresOperation.Tables("GQ-AD-FLAGS")
                        lGQTable.ParmValue("GQADFG(1)") = .MonthData.MonthDataTables.Count
                    End If
                End If
            Next
        End With
    End Sub

    Public Sub PopulateAccumSqolim(ByVal aOper As HspfOperation, ByVal aCropData As System.Data.DataTable, ByVal aForestData As System.Data.DataTable, ByVal aPastureData As System.Data.DataTable, ByVal aBuiltupData As System.Data.DataTable, ByVal aDirectData As System.Data.DataTable, ByRef dieoff As Array)
        Dim lLU As String = ""
        Dim lAccumulationRate(12) As Double
        Dim lStorageLimit(12) As Double

        'which subbasin/subbasins does this perlnd contribute to?
        Dim lContributingSubbasins As New atcCollection
        Dim lContributingAreasBySubbasin As New atcCollection
        For Each lConn As HspfConnection In aOper.Uci.Connections
            If lConn.Typ = 3 Then 'schematic
                If lConn.Source.Opn.Id = aOper.Id And _
                   lConn.Source.Opn.Name = aOper.Name Then
                    'found a target from this perlnd
                    If lConn.Target.VolName = "RCHRES" Then
                        lContributingSubbasins.Add(lConn.Target.VolId)
                        lContributingAreasBySubbasin.Add(lConn.Target.VolId, lConn.MFact)
                    End If
                End If
            End If
        Next

        'if this perlnd contributes to multiple subbasins, we use an areal weighted average
        'of the accumulation rate and storage limit.

        'which land use is this perlnd?
        lLU = aOper.Description.ToUpper
        Dim lRowsToUse As New atcCollection  'may be multiple subbasins, get averages
        Dim lTotalArea As Double = 0.0
        If lLU.Contains("URBAN") Then
            If Not aBuiltupData Is Nothing Then
                For Each lSubbasin As Integer In lContributingSubbasins
                    For lRow As Integer = 0 To aBuiltupData.Rows.Count - 1
                        If aBuiltupData.Rows(lRow)(0) = lSubbasin Then
                            lRowsToUse.Add(lRow)
                        End If
                    Next
                Next
                For Each lRow As Integer In lRowsToUse
                    Dim lArea As Double = 1.0
                    If Not lContributingAreasBySubbasin.ItemByKey(CInt(aBuiltupData.Rows(lRow)(0))) Is Nothing Then
                        lArea = lContributingAreasBySubbasin.ItemByKey(CInt(aBuiltupData.Rows(lRow)(0)))
                    End If
                    lTotalArea += lArea
                    For lMonth As Integer = 1 To 12
                        lAccumulationRate(lMonth) += (aBuiltupData.Rows(lRow)(lMonth) * lArea)
                        lStorageLimit(lMonth) += (aBuiltupData.Rows(lRow)(lMonth + 12) * lArea)
                    Next
                Next
            End If
        ElseIf lLU.Contains("FOREST") Then
            If Not aForestData Is Nothing Then
                For Each lSubbasin As Integer In lContributingSubbasins
                    For lRow As Integer = 0 To aForestData.Rows.Count - 1
                        If aForestData.Rows(lRow)(0) = lSubbasin Then
                            lRowsToUse.Add(lRow)
                        End If
                    Next
                Next
                For Each lRow As Integer In lRowsToUse
                    Dim lArea As Double = 1.0
                    If Not lContributingAreasBySubbasin.ItemByKey(CInt(aForestData.Rows(lRow)(0))) Is Nothing Then
                        lArea = lContributingAreasBySubbasin.ItemByKey(CInt(aForestData.Rows(lRow)(0)))
                    End If
                    lTotalArea += lArea
                    For lMonth As Integer = 1 To 12
                        lAccumulationRate(lMonth) += (aForestData.Rows(lRow)(lMonth) * lArea)
                        lStorageLimit(lMonth) += (aForestData.Rows(lRow)(lMonth + 12) * lArea)
                    Next
                Next
            End If
        ElseIf lLU.Contains("PASTU") Then
            If Not aPastureData Is Nothing Then
                For Each lSubbasin As Integer In lContributingSubbasins
                    For lRow As Integer = 0 To aPastureData.Rows.Count - 1
                        If aPastureData.Rows(lRow)(0) = lSubbasin Then
                            lRowsToUse.Add(lRow)
                        End If
                    Next
                Next
                For Each lRow As Integer In lRowsToUse
                    Dim lArea As Double = 1.0
                    If Not lContributingAreasBySubbasin.ItemByKey(CInt(aPastureData.Rows(lRow)(0))) Is Nothing Then
                        lArea = lContributingAreasBySubbasin.ItemByKey(CInt(aPastureData.Rows(lRow)(0)))
                    End If
                    lTotalArea += lArea
                    For lMonth As Integer = 1 To 12
                        lAccumulationRate(lMonth) += (aPastureData.Rows(lRow)(lMonth) * lArea)
                        lStorageLimit(lMonth) += (aPastureData.Rows(lRow)(lMonth + 12) * lArea)
                    Next
                Next
            End If
        ElseIf lLU.Contains("CROP") Then
            If Not aCropData Is Nothing Then
                For Each lSubbasin As Integer In lContributingSubbasins
                    For lRow As Integer = 0 To aCropData.Rows.Count - 1
                        If aCropData.Rows(lRow)(0) = lSubbasin Then
                            lRowsToUse.Add(lRow)
                        End If
                    Next
                Next
                For Each lRow As Integer In lRowsToUse
                    Dim lArea As Double = 1.0
                    If Not lContributingAreasBySubbasin.ItemByKey(CInt(aCropData.Rows(lRow)(0))) Is Nothing Then
                        lArea = lContributingAreasBySubbasin.ItemByKey(CInt(aCropData.Rows(lRow)(0)))
                    End If
                    lTotalArea += lArea
                    For lMonth As Integer = 1 To 12
                        lAccumulationRate(lMonth) += (aCropData.Rows(lRow)(lMonth) * lArea)
                        lStorageLimit(lMonth) += (aCropData.Rows(lRow)(lMonth + 12) * lArea)
                    Next
                Next
            End If
        End If

        If lRowsToUse.Count > 0 And lTotalArea > 0.0 Then
            'get areal weighted averages
            For lMonth As Integer = 1 To 12
                lAccumulationRate(lMonth) = lAccumulationRate(lMonth) / lTotalArea
                lStorageLimit(lMonth) = lStorageLimit(lMonth) / lTotalArea
            Next
        End If

        If lRowsToUse.Count > 0 Then
            If aOper.Name = "PERLND" Then
                Dim lHspfTable As HspfTable
                lHspfTable = aOper.Tables("MON-ACCUM")
                For lMonthIndex As Integer = 1 To 12
                    lHspfTable.Parms.Item(lMonthIndex - 1).Value = lAccumulationRate(lMonthIndex)
                    If lAccumulationRate(lMonthIndex) = 0 Then
                        lHspfTable.Parms.Item(lMonthIndex - 1).Value = 1.0
                        If dieoff(lMonthIndex - 1) > 10 ^ -1 Then
                            lStorageLimit(lMonthIndex) = 1.0 / (2.303 * dieoff(lMonthIndex - 1))
                        ElseIf dieoff(lMonthIndex - 1) < 10 ^ -6 Then
                            lStorageLimit(lMonthIndex) = 1.0 * Date.DaysInMonth(Today.Year, lMonthIndex)
                        Else
                            lStorageLimit(lMonthIndex) = (1.0 / (2.303 * dieoff(lMonthIndex - 1))) * (1 - 10 ^ -(Date.DaysInMonth(Today.Year, lMonthIndex) * dieoff(lMonthIndex - 1)))
                        End If

                        'lStorageLimit(lMonthIndex) = 1.0 * Date.DaysInMonth(Today.Year, lMonthIndex)
                        'If DieOFF < 0.0001 Then
                        '    lStorageLimit(lMonthIndex) = 1.0 / (2.303 * DieOFF)
                        'End If
                    End If
                Next
                lHspfTable = aOper.Tables("MON-SQOLIM")
                For lMonthIndex As Integer = 1 To 12
                    lHspfTable.Parms.Item(lMonthIndex - 1).Value = lStorageLimit(lMonthIndex)
                    If lAccumulationRate(lMonthIndex) > lStorageLimit(lMonthIndex) Then
                        'catch case where accum rate is greater than storage limit, use accum rate as storage limit
                        lHspfTable.Parms.Item(lMonthIndex - 1).Value = lAccumulationRate(lMonthIndex)
                    End If
                Next
            ElseIf aOper.Name = "IMPLND" Then
                Dim lHspfTable As HspfTable
                lHspfTable = aOper.Tables("QUAL-INPUT")
                lHspfTable.Parms.Item(2).Value = lAccumulationRate(1)
                lHspfTable.Parms.Item(3).Value = lStorageLimit(1)
                If lAccumulationRate(1) > lStorageLimit(1) Then
                    'catch case where accum rate is greater than storage limit, use accum rate as storage limit
                    lHspfTable.Parms.Item(3).Value = lAccumulationRate(1)
                End If
            End If
        End If
    End Sub

    Private Function GetDieOff(ByRef file As String) As Array
        Using fr As New FileIO.TextFieldParser(file)
            fr.TextFieldType = FileIO.FieldType.Delimited
            fr.SetDelimiters(",")
            Dim currentRow As String()
            Dim rndx As Int16 = 0
            Dim dieoff As Double() = New Double(11) {}
            While Not fr.EndOfData
                Try
                    currentRow = fr.ReadFields()
                    Dim currentField As String
                    Dim fndx As Int16 = 0
                    For Each currentField In currentRow
                        'MsgBox(currentField)
                        If fndx > 0 Then
                            If rndx > 0 Then
                                dieoff(rndx - 1) = CDbl(currentField)
                            End If
                        End If
                        fndx = fndx + 1
                    Next
                    rndx = rndx + 1
                Catch ex As FileIO.MalformedLineException
                    MsgBox("MSM DieOff file line " & ex.Message & "is not valid and will be skipped.")
                End Try
            End While
            Return dieoff
        End Using

    End Function

End Module

