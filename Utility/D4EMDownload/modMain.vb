Module modMain
    Sub Main()
        If My.Application.CommandLineArgs.Count > 0 Then
            Dim lInstructionsFilename As String = My.Application.CommandLineArgs(0)
            D4EM.Data.Globals.Initialize()
        Else
            MsgBox("Command line was empty, expected XML instruction file name.")
        End If
    End Sub
End Module
