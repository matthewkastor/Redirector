' A generic redirector for stderr and stdout
' Extend this class and override the StdoutHandler
' and Stderr handler to do whatever you want.
' Author: Matthew Christopher Kastor-Inare III
' matthewkastor@gmail.com
' Atropa Inc. Intl.
' License: GPLv3
Public Class Redirector
    Inherits Process

    Public HelpMessage As String = My.Resources.help_en

    Public Sub bail(Optional ByVal message As String = "")
        Console.WriteLine(message)
        Console.WriteLine("Press any key to exit...")
        Console.ReadKey(True)
        Environment.Exit(1)
    End Sub

    Public Overridable Sub StdoutHandler(sendingProcess As Object, _
                outLine As DataReceivedEventArgs)
        Console.Out.WriteLineAsync(outLine.Data)
    End Sub

    Public Overridable Sub StderrHandler(sendingProcess As Object, _
                outLine As DataReceivedEventArgs)
        Console.Error.WriteLineAsync(outLine.Data)
    End Sub

    Public Function PassArguments(Optional ByVal dropLeading As Integer = 1) As String
        Dim argz As IEnumerable(Of String) = Environment.GetCommandLineArgs().Skip(dropLeading)
        If argz.Count > 0 Then
            ' puts quotes between each argument
            Dim argstr As String = String.Join(""" """, argz)
            ' adds the leading and trailing quotes to the entire string
            Return ("""" & argstr & """")
        Else
            Return (String.Empty)
        End If
    End Function

    Public Function WantsHelp() As Boolean
        Dim helpWanted As Boolean = False
        If 2 = Environment.GetCommandLineArgs().Count Then
            Select Case Environment.GetCommandLineArgs()(1)
                Case "/?", "-h", "--help"
                    helpWanted = True
                Case Else
                    helpWanted = False
            End Select
        End If
        Return helpWanted
    End Function

    Public Sub Help(Optional ByVal message As String = "default")
        If message = "default" Then
            message = Me.HelpMessage
        End If
        Console.Write(message)
    End Sub

    Public Sub CheckHelpSwitch()
        If Me.WantsHelp() = True Then
            Me.Help()
            Environment.Exit(0)
            Exit Sub
        End If
    End Sub
    

    Public Sub RedirectStdOutErr(ByVal fileName As String, ByVal args As String)
        Me.StartInfo.FileName = fileName
        Me.StartInfo.Arguments = args
        Me.StartInfo.UseShellExecute = False
        Me.StartInfo.RedirectStandardError = True
        Me.StartInfo.RedirectStandardOutput = True
        AddHandler Me.OutputDataReceived, AddressOf Me.StdoutHandler
        AddHandler Me.ErrorDataReceived, AddressOf Me.StderrHandler
        Me.Start()
        Me.BeginOutputReadLine()
        Me.BeginErrorReadLine()
        Me.WaitForExit()
        Me.Close()
    End Sub
End Class
