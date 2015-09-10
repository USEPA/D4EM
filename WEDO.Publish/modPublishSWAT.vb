Imports atcUtility
Imports atcData
Imports MapWinUtility

Module modPublishSWAT
    Public CioFilename As String

    Public Function GetSwatInputFiles() As List(Of String)
        Dim lInputFiles As New List(Of String)
        For Each lFileName As String In IO.Directory.GetFiles(IO.Path.GetDirectoryName(CioFilename))
            Select Case IO.Path.GetFileName(lFileName).ToLowerInvariant()
                Case "output.rch", "output.sub", "output.hru", "output.hrux",
                     "hyd.out", "output.sed", "output.std", "tempvel.out",
                     "watout.dat", "watqual.dat", "chan.deg", "hru.dat", "input.std", "output.rsv", "output.wtr", "rch.dat", "rsv.dat", "sub.dat", "fort.1112"
                    'Skip output files
                Case Else
                    lInputFiles.Add(lFileName)
            End Select
        Next
        Return lInputFiles
    End Function

    Public Function GetSwatTimeseriesOutputFiles() As List(Of String)
        Dim lOutputFiles As New List(Of String)
        For Each lFileName As String In IO.Directory.GetFiles(IO.Path.GetDirectoryName(CioFilename))
            Select Case IO.Path.GetFileName(lFileName).ToLowerInvariant()
                Case "output.rch" ', "output.sub", "output.hru" ', "output.hrux"
                    lOutputFiles.Add(lFileName)
            End Select
        Next
        Return lOutputFiles
    End Function

    Public Sub OpenSwatOutput(aDataFileName As String)
        Dim lDataSource As New atcTimeseriesSWAT.atcTimeseriesSWAT()
        atcData.atcDataManager.OpenDataSource(lDataSource, aDataFileName, Nothing)
    End Sub

    Public Function OutputDatasets(aOutputFile As atcTimeseriesSWAT.atcTimeseriesSWAT, aOnlyDatasetsForWEDO As Boolean) As atcData.atcTimeseriesGroup
        Dim lOutputDatasets As New atcData.atcTimeseriesGroup
        'Choose datasets of interest by constituent name
        For Each lTs As atcTimeseries In aOutputFile.DataSets
            Debug.WriteLine(lTs.Attributes.GetValue("Constituent"))
            Dim lConstituentIndex As Integer = Array.IndexOf(g_RchConstituentsOfInterest, lTs.Attributes.GetValue("Constituent"))
            If lConstituentIndex >= 0 Then
                lTs.Attributes.SetValue("Constituent", g_ConstituentsOfInterest(lConstituentIndex))
                lOutputDatasets.Add(lTs)
            ElseIf Not aOnlyDatasetsForWEDO Then
                lOutputDatasets.Add(lTs)
            End If
        Next
        Return lOutputDatasets
    End Function


End Module
