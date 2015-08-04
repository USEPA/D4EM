Imports atcData
Imports atcUCI
Imports atcUtility
Imports MapWinUtility

Module modPublishHSPF

    Public UCIFilename As String
    Private pUCI As atcUCI.HspfUci
    Private pMsg As atcUCI.HspfMsg

    Public Function UCIFile() As atcUCI.HspfUci
        If pUCI Is Nothing OrElse UCIFilename <> pUCI.Name Then
            If UCIFilename Is Nothing OrElse Not IO.File.Exists(UCIFilename) Then
                Do
                    Dim lCdlg As New OpenFileDialog
                    With lCdlg
                        .Title = "Select UCI file to open"
                        .Filter = "UCI files|*.uci"
                        .FilterIndex = 0
                        .CheckFileExists = True
                        Select Case .ShowDialog()
                            Case DialogResult.OK
                                UCIFilename = .FileName
                                Logger.Dbg("User specified file '" & UCIFilename & "'")
                            Case DialogResult.Cancel
                                Return Nothing
                        End Select
                    End With
                Loop While Not IO.File.Exists(UCIFilename)
            End If
            Logger.Status("Opening " & UCIFilename)
            pUCI = New atcUCI.HspfUci
            pUCI.FastReadUciForStarter(MsgFile, UCIFilename)
            MetadataInfo.ModelStartDate = Date.FromOADate(pUCI.GlobalBlock.SDateJ)
            MetadataInfo.ModelEndDate = Date.FromOADate(pUCI.GlobalBlock.EdateJ)
            Logger.Status("")
        End If
        Return pUCI
    End Function

    Public Property MsgFile As atcUCI.HspfMsg
        Get
            If pMsg Is Nothing Then
                pMsg = New atcUCI.HspfMsg("hspfmsg.wdm")
            End If
            Return pMsg
        End Get
        Set(ByVal value As atcUCI.HspfMsg)
            pMsg = value
        End Set
    End Property

    Public Function FilesInUCI(aIncludeWDM As Boolean, aIncludeHBN As Boolean, aIncludeECH As Boolean, aIncludeOUT As Boolean, aOutputWDMOnly As Boolean) As List(Of String)
        Dim lFiles As New List(Of String)
        Dim lFilesBlock As HspfFilesBlk = UCIFile.FilesBlock
        Dim lUciFolder As String = IO.Path.GetDirectoryName(UCIFilename)
        For lIndex As Integer = 1 To lFilesBlock.Count
            Dim lFileName As String = lFilesBlock.Value(lIndex).Name.Trim
            Dim lExtension As String = IO.Path.GetExtension(lFileName).ToLowerInvariant()
            Dim lFullPathFromUCI As String = atcUtility.modFile.AbsolutePath(lFileName, lUciFolder)
            Select Case lExtension
                Case ".wdm"
                    If aIncludeWDM Then
                        Dim lIncludeThisOne As Boolean = False
                        If aOutputWDMOnly Then
                            Dim lThisVolName As String = lFilesBlock.Value(lIndex).Typ
                            For Each lOper As HspfOperation In UCIFile.OpnBlks("RCHRES").Ids
                                For Each lConn As HspfConnection In lOper.Targets
                                    If lConn.Target.VolName = lThisVolName Then
                                        lIncludeThisOne = True
                                        GoTo AddWDM
                                    End If
                                Next
                            Next
                        Else
                            lIncludeThisOne = True
                        End If
                        If lIncludeThisOne Then
AddWDM:                     lFiles.Add(lFullPathFromUCI)
                        End If
                    End If
                Case ".hbn"
                    If aIncludeHBN Then lFiles.Add(lFullPathFromUCI)
                Case ".ech"
                    If aIncludeECH Then lFiles.Add(lFullPathFromUCI)
                Case ".out"
                    If aIncludeOUT Then lFiles.Add(lFullPathFromUCI)
                Case Else
                    Logger.Dbg("Adding file from UCI: " & lFileName)
                    lFiles.Add(lFullPathFromUCI)
            End Select
        Next
        Return lFiles
    End Function

    Public Function OutputDatasetsInWDM(aWDM As atcWDM.atcDataSourceWDM, aOnlyDatasetsForWEDO As Boolean) As atcData.atcTimeseriesGroup
        Dim lOutputDatasets As New atcData.atcTimeseriesGroup
        Dim lFilesBlock As HspfFilesBlk = UCIFile.FilesBlock
        Dim lUciFolder As String = IO.Path.GetDirectoryName(UCIFilename)
        For lIndex As Integer = 1 To lFilesBlock.Count
            Dim lFileName As String = lFilesBlock.Value(lIndex).Name.Trim
            Dim lExtension As String = IO.Path.GetExtension(lFileName).ToLowerInvariant()
            Dim lFullPathFromUCI As String = atcUtility.modFile.AbsolutePath(lFileName, lUciFolder)
            If lFullPathFromUCI.ToLowerInvariant() = aWDM.Specification.ToLowerInvariant() Then
                Dim lThisVolName As String = lFilesBlock.Value(lIndex).Typ
                For Each lOper As HspfOperation In UCIFile.OpnBlks("RCHRES").Ids
                    For Each lConn As HspfConnection In lOper.Targets
                        If lConn.Target.VolName = lThisVolName Then
                            Dim lIncludeThisOne As Boolean = False
                            Dim lNewConstituentName As String = Nothing
                            Dim lWdmDsn As Integer = lConn.Target.VolId
                            Dim lReachID As Integer = lOper.Id
                            If aOnlyDatasetsForWEDO Then
                                Dim lConIndex As Integer = -1
                                For Each lConstituentOfInterest In g_ConstituentsOfInterest
                                    lConIndex += 1
                                    Dim lUciConstituentName As String = g_UciConstituentsOfInterest(lConIndex)
                                    Dim lMemSub1 As Integer = 0
                                    Dim lColonIndex As Integer = lUciConstituentName.IndexOf(":")
                                    If lColonIndex >= 0 Then
                                        lMemSub1 = Integer.Parse(lUciConstituentName.Substring(lColonIndex + 1))
                                        lUciConstituentName = lUciConstituentName.Substring(0, lColonIndex)
                                    End If
                                    If lConn.Source.Member = lUciConstituentName Then
                                        If lMemSub1 = 0 OrElse lMemSub1 = lConn.Source.MemSub1 Then
                                            lNewConstituentName = lConstituentOfInterest
                                            Exit For
                                        End If
                                    End If
                                Next

                                'Select Case lConn.Source.Member
                                '    Case "RO"
                                '        lNewConstituentName = g_ConstituentsOfInterest(0) ' Flow
                                '    Case "SSED"
                                '        If lConn.Source.MemSub1 = 4 Then
                                '            lNewConstituentName = g_ConstituentsOfInterest(1) 'TSS
                                '        End If
                                '    Case "PKST4"
                                '        If lConn.Source.MemSub1 = 1 Then
                                '            lNewConstituentName = g_ConstituentsOfInterest(2) 'TKN
                                '        End If
                                '    Case "DNUST"
                                '        Select Case lConn.Source.MemSub1
                                '            Case 1 : lNewConstituentName = g_ConstituentsOfInterest(4) 'NO3-N
                                '            Case 2 : lNewConstituentName = g_ConstituentsOfInterest(3) 'NH3-N
                                '            Case 3 : lNewConstituentName = g_ConstituentsOfInterest(5) 'NO2-N
                                '        End Select
                                '    Case "PKST3"
                                '        If lConn.Source.MemSub1 = 4 Then
                                '            lNewConstituentName = g_ConstituentsOfInterest(6) 'ORGN
                                '        End If
                                '    Case "PKST4"
                                '        If lConn.Source.MemSub1 = 3 Then
                                '            lNewConstituentName = g_ConstituentsOfInterest(7) 'P
                                '        End If
                                '    Case "DNUST"
                                '        If lConn.Source.MemSub1 = 4 Then
                                '            lNewConstituentName = g_ConstituentsOfInterest(8) 'PO4-P
                                '        End If
                                'End Select
                                lIncludeThisOne = (lNewConstituentName IsNot Nothing)
                            Else
                                lIncludeThisOne = True
                            End If
                            If lIncludeThisOne Then
                                Dim lFoundTsg As atcData.atcTimeseriesGroup = aWDM.DataSets.FindData("ID", lWdmDsn, 1)
                                If lFoundTsg.Count > 0 Then
                                    If (lNewConstituentName IsNot Nothing) Then
                                        lFoundTsg(0).Attributes.SetValue("Constituent", lNewConstituentName)
                                    End If
                                    lOutputDatasets.Add(lFoundTsg(0))
                                End If
                            End If
                        End If
                    Next
                Next
            End If
        Next
        Return lOutputDatasets
    End Function

    Public Function OutputDatasetsInHBN(aHBN As atcHspfBinOut.atcTimeseriesFileHspfBinOut, aOnlyDatasetsForWEDO As Boolean) As atcData.atcTimeseriesGroup
        Dim lOutputDatasets As New atcData.atcTimeseriesGroup
        'Choose datasets of interest by constituent name
        For Each lTs As atcTimeseries In aHBN.DataSets
            Dim lConstituentIndex As Integer = Array.IndexOf(g_HbnConstituentsOfInterest, lTs.Attributes.GetValue("Constituent"))
            If lConstituentIndex >= 0 Then
                lTs.Attributes.SetValue("Constituent", g_ConstituentsOfInterest(lConstituentIndex))
                lOutputDatasets.Add(lTs)
            End If
        Next
        Return lOutputDatasets
    End Function

End Module
