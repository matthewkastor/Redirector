' Extends GeckoLib with specific functions for 
' launching SlimerJs
' Author: Matthew Christopher Kastor-Inare III
' matthewkastor@gmail.com
' Atropa Inc. Intl.
' License: GPLv3

Imports System.IO
Imports GeckoLib

Class SlimerJsLib
    Inherits GeckoLib.GeckoLib

    Public Sub Run()

        ' help just prints a message and exits this check needs to be first
        ' so it can fire the action if necessary.
        Me.CheckHelpSwitch()

        Dim applicationIni As String = Environment.CurrentDirectory & "/../src/application.ini"
        Dim argsin As Array = Environment.GetCommandLineArgs()
        Dim dropArgs As Integer = 1
        Dim defaultArgs As String = "-app """ & applicationIni & """ -purgecaches -envs """ & Me.ListEnvironmentVariables() & """ "

        If Not File.Exists(applicationIni) Then
            Me.bail("Error! Can not find application.ini at " & applicationIni)
            Exit Sub
        End If

        If Not (argsin.Length > 1) Then
            ' argsin will always have 1 argument, the calling script or executable
            Me.bail("Error! Specify a script to run!")
        End If

        Select Case argsin(1)
            Case Is = "/initialtests"
                dropArgs = dropArgs + 1
                defaultArgs = defaultArgs & "../test/initial-tests.js"
            Case Is = "/maintests"
                dropArgs = dropArgs + 1
                defaultArgs = defaultArgs & "../test/main-tests.js"
            Case Else
                If Not File.Exists(argsin(1)) Then
                    ' this makes sure the application to run on slimerjs exists
                    Me.Help()
                    Environment.Exit(0)
                End If
        End Select

        Me.ConsoleRedirector(Me.VerifySLIMERJSLAUNCHER(), defaultArgs & Me.PassArguments(dropArgs))

    End Sub

    Public Function ListEnvironmentVariables() As String
        Dim dictionaryEntry As System.Collections.DictionaryEntry
        Dim listVars As String = String.Empty
        For Each dictionaryEntry In Environment.GetEnvironmentVariables()
            listVars += dictionaryEntry.Key.ToString() & ","
        Next
        listVars = Left(listVars, listVars.Length - 1)
        Return listVars
    End Function

    Public Function VerifySLIMERJSLAUNCHER() As String
        Dim out As String = Environment.GetEnvironmentVariable("SLIMERJSLAUNCHER")

        If String.IsNullOrEmpty(out) Then
            Console.WriteLine("SLIMERJSLAUNCHER environment variable is not set.")
            out = String.Empty
        End If

        If Not File.Exists(out) Then
            Console.WriteLine("Could not find a file at SLIMERJSLAUNCHER : " _
                                & System.Environment.NewLine & out)
            out = String.Empty
        End If

        Return out

    End Function

End Class
