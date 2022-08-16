Imports atcData
Imports atcUtility
Imports MapWinUtility

Module modMain
    Friend Const g_AppNameLong As String = "D4EM Data Download"

    'TODO: add NCDC
    Dim AllSources() As D4EM.Data.SourceBase = {New D4EM.Data.Source.BASINS(), New D4EM.Data.Source.NHDPlus(), New D4EM.Data.Source.NLDAS(), New D4EM.Data.Source.NWIS(), New D4EM.Data.Source.Storet(), New D4EM.Data.Source.USGS_Seamless()}
    Friend g_ProgramDir As String = ""
    Private pStatusMonitor As MonitorProgressStatus

    Sub Main()
        Dim lResultFilename As String
        Dim lInstructionsFilename As String
        If My.Application.CommandLineArgs.Count < 2 Then
            lResultFilename = InputBox("Write results to: ", g_AppNameLong)
            lInstructionsFilename = InputBox("File containing XML instructions: ", g_AppNameLong)
        Else
            lResultFilename = My.Application.CommandLineArgs(0)
            lInstructionsFilename = My.Application.CommandLineArgs(1)
        End If

        Try
            If My.Application.CommandLineArgs.Count > 2 Then
                If MsgBox("Writing Results into: " & lResultFilename & vbCrLf _
                        & "Reading Instructions: " & lInstructionsFilename & vbCrLf _
                        & "Proceed with download?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                    Exit Sub
                End If
            End If
            D4EM.Data.Globals.Initialize()
            InitStatusAndManager()
            lResultFilename = lResultFilename.Replace("""", "")
            lInstructionsFilename = lInstructionsFilename.Replace("""", "")
            Dim aQuery As String = System.IO.File.ReadAllText(lInstructionsFilename)
            Dim lResult As String = ""
            Dim lFunctionName As String = ""
            Dim lQueryDoc As New Xml.XmlDocument
            Dim lQueries As New Generic.List(Of String)
            Dim lEndFunction As Integer = aQuery.ToLower.IndexOf("</function>")
            While lEndFunction > 0
                lQueries.Add(aQuery.Substring(0, lEndFunction + 11))
                aQuery = aQuery.Substring(lEndFunction + 11)
                lEndFunction = aQuery.ToLower.IndexOf("</function>")
            End While
            Dim lSingleQuery As Boolean = True
            If lQueries.Count > 1 Then
                lSingleQuery = False
                Logger.Progress(0, lQueries.Count)
            End If
            For Each lQuery As String In lQueries
                lQueryDoc.LoadXml(lQuery)
                Dim lFunction As Xml.XmlNode = lQueryDoc.FirstChild
                If lFunction.Name.ToLower = "function" Then
                    lFunctionName = lFunction.Attributes.GetNamedItem("name").Value
                    Logger.Dbg("Function " & lFunctionName)
                    If lFunctionName = "GetNHDplus2" Then
                        lFunctionName = "GetNHDplus"
                    End If
                    Dim lSource As D4EM.Data.SourceBase = GetSourceSupportingFunction(lFunctionName)
                    'Select Case lFunctionName
                    '    Case "GetBASINS" : lSource = New D4EM.Data.Source.BASINS()
                    '    Case "GetNHDPlus" : lSource = New D4EM.Data.Source.NHDPlus()
                    '    Case "GetNCDC" 'TODO: implement Execute for NCDC : lSource = New D4EM.Data.Source.NCDC()
                    '    Case "GetNLDASParameter", "GetNLDASGrid" : lSource = New D4EM.Data.Source.NLDAS()
                    '    Case "GetNRCS" 'TODO: implement Execute for NCDC : lSource = New D4EM.Data.Source.NRCS_Soil()
                    '    Case "GetBASINS" : lSource = New D4EM.Data.Source.BASINS()
                    '    Case Else
                    '        Logger.Msg("Cannot find extension for function '" & lFunctionName & "'" & vbCrLf & vbCrLf & lQuery,  g_AppNameLong)

                    'End Select
                    'Dim lExtension As IDataExtension = GetSourceSupportingFunction(lFunctionName)
                    If lSource IsNot Nothing Then
                        Logger.Dbg("Source " & lSource.Name)
                        Logger.Dbg("Query: " & lQuery)
                        'TODO: how do defaults from lExtension.QuerySchema into query?
                        Logger.Status(lFunctionName)
                        Using lLevel As New ProgressLevel(Not lSingleQuery, lSingleQuery)
                            lResult &= lSource.Execute(lQuery)
                        End Using
                    ElseIf lFunctionName.StartsWith("GetNCDC") Then
                        Using lLevel As New ProgressLevel(Not lSingleQuery, lSingleQuery)
                            lResult &= ExecuteNCDC(lQuery)
                        End Using
                    ElseIf lFunctionName.StartsWith("GetSoils") Then
                        Using lLevel As New ProgressLevel(Not lSingleQuery, lSingleQuery)
                            lResult &= ExecuteNRCS_Soils(lQuery)
                        End Using
                    Else
                        Logger.Msg("Cannot find extension for function '" & lFunctionName & "'" & vbCrLf & vbCrLf & lQuery, g_AppNameLong)
                    End If
                Else
                    Logger.Msg(lQuery, "Query does not start with function tag", g_AppNameLong)
                    'TODO: handle queries that specify the end result by finding function(s) that can produce it
                End If
            Next
            IO.File.WriteAllText(lResultFilename, lResult)
            If pStatusMonitor IsNot Nothing Then
                pStatusMonitor.StopMonitor()
            End If
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical, g_AppNameLong)
        End Try
    End Sub

    Private Sub InitStatusAndManager()
        g_ProgramDir = PathNameOnly(Reflection.Assembly.GetEntryAssembly.Location)
        Try
            Environment.SetEnvironmentVariable("PATH", g_ProgramDir & ";" & Environment.GetEnvironmentVariable("PATH"))
        Catch eEnv As Exception
        End Try
        If g_ProgramDir.EndsWith("bin") Then g_ProgramDir = PathNameOnly(g_ProgramDir)
        g_ProgramDir &= g_PathChar

        Dim lLogFolder As String = g_ProgramDir & "cache"
        If IO.Directory.Exists(lLogFolder) Then
            lLogFolder = lLogFolder & g_PathChar & "log" & g_PathChar
        Else
            lLogFolder = IO.Path.Combine(My.Computer.FileSystem.SpecialDirectories.MyDocuments, "log") & g_PathChar
        End If
        'Logger.StartToFile(lLogFolder & Format(Now, "yyyy-MM-dd") & "at" & Format(Now, "HH-mm") & "-" & g_AppNameShort & ".log")
        'Logger.Icon = Me.Icon

        If Logger.ProgressStatus Is Nothing OrElse Not (TypeOf (Logger.ProgressStatus) Is MonitorProgressStatus) Then
            'Start running status monitor to give better progress and status indication during long-running processes
            pStatusMonitor = New MonitorProgressStatus
            If pStatusMonitor.StartMonitor(FindFile("Find Status Monitor", "StatusMonitor.exe"),
                                            g_ProgramDir,
                                            System.Diagnostics.Process.GetCurrentProcess.Id) Then
                pStatusMonitor.InnerProgressStatus = Logger.ProgressStatus
                Logger.ProgressStatus = pStatusMonitor
                Logger.Status("LABEL TITLE " & g_AppNameLong & " Status")
                Logger.Status("PROGRESS TIME ON") 'Enable time-to-completion estimation
                Logger.Status("")
            Else
                pStatusMonitor.StopMonitor()
                pStatusMonitor = Nothing
            End If
        End If

        atcDataManager.Clear()
        With atcDataManager.DataPlugins
            '.Add(New atcHspfBinOut.atcTimeseriesFileHspfBinOut)
            '.Add(New atcWdmVb.atcWDMfile)
            .Add(New atcWDM.atcDataSourceWDM)
            '.Add(New atcTimeseriesScript.atcTimeseriesScriptPlugin)
        End With

        atcTimeseriesStatistics.atcTimeseriesStatistics.InitializeShared()
    End Sub

    Private Function GetSourceSupportingFunction(ByVal aFunctionName As String) As D4EM.Data.SourceBase
        aFunctionName = aFunctionName.ToLower
        For Each lSource In AllSources
            Try
                Dim lSchema As New Xml.XmlDocument
                lSchema.LoadXml(lSource.QuerySchema)
                Dim lNode As Xml.XmlNode = lSchema.FirstChild.FirstChild
                While lNode IsNot Nothing
                    If lNode.Attributes.GetNamedItem("name").Value.ToLower = aFunctionName Then
                        Return lSource
                    End If
                    lNode = lNode.NextSibling
                End While
            Catch
            End Try
        Next
        'Dim lLastPlugIn As Integer = pMapWin.Plugins.Count() - 1
        'For iPlugin As Integer = 0 To lLastPlugIn
        '  Dim lCurPlugin As MapWindow.Interfaces.IPlugin = pMapWin.Plugins.Item(iPlugin)
        '  If Not lCurPlugin Is Nothing AndAlso TypeOf (lCurPlugin) Is IDataExtension Then
        '    Dim lExtension As IDataExtension = lCurPlugin
        '    Dim lSchema As New Chilkat.Xml
        '    lSchema.LoadXml(lExtension.QuerySchema)
        '    lSchema.FirstChild2()
        '    While Not lSchema Is Nothing
        '      If lSchema.GetAttrValue("name").ToLower = aFunctionName Then Return lExtension
        '      If Not lSchema.NextSibling2() Then Exit While
        '    End While
        '  End If
        'Next
        Return Nothing
    End Function

End Module
