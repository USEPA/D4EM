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

    Public Function FilesInUCI(aIncludeWDM As Boolean, aIncludeHBN As Boolean, aIncludeECH As Boolean, aIncludeOUT As Boolean) As List(Of String)
        Dim lFiles As New List(Of String)
        Dim lFilesBlock As HspfFilesBlk = UCIFile.FilesBlock
        Dim lUciFolder As String = IO.Path.GetDirectoryName(UCIFilename)
        For lIndex As Integer = 1 To lFilesBlock.Count
            Dim lFileName As String = lFilesBlock.Value(lIndex).Name.Trim
            Dim lExtension As String = IO.Path.GetExtension(lFileName).ToLowerInvariant()
            Dim lFullPathFromUCI As String = atcUtility.modFile.AbsolutePath(lFileName, lUciFolder)
            Select Case lExtension
                Case ".wdm"
                    If aIncludeWDM Then lFiles.Add(lFullPathFromUCI)
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

End Module
