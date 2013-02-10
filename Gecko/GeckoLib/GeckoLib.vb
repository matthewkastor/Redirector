' Extends Redirector with specific functions 
' for launching Gecko based applications like
' Firefox, whose -console option doesn't quite
' do what's expected. This library fixes that.
' Note that you can build the Console Redirector
' for a ready made executable that uses this lib.
'
' Author: Matthew Christopher Kastor-Inare III
' matthewkastor@gmail.com
' Atropa Inc. Intl.
' License: GPLv3
Imports System.IO
Imports Redirector

Public Class GeckoLib
    Inherits Redirector.Redirector

    ' If GeckoRuntimeLocation is not specified then
    ' we will try to find the Firefox executable
    Public Sub ConsoleRedirector(Optional ByVal GeckoRuntimeLocation As String = "", Optional ByVal args As String = "")
        Me.CheckHelpSwitch()

        Dim geckoRuntime As String = GetGeckoRuntimeLocation(GeckoRuntimeLocation)
        If geckoRuntime = String.Empty Then
            Me.bail()
            Exit Sub
        End If

        Me.RedirectStdOutErr(geckoRuntime, args)
    End Sub

    Public Function FindFirefox() As String
        Dim ffloc As String = String.Empty
        Try
            ffloc = Me.GetFirefoxLocation(Environment.GetEnvironmentVariable("programfiles"))
        Catch ex As Exception
            Try
                ffloc = Me.GetFirefoxLocation(Environment.GetEnvironmentVariable("programfiles(x86)"))
            Catch exc As Exception
                Console.WriteLine("Could not find Firefox on your system!")
                Return String.Empty
            End Try
        End Try
        Return ffloc
    End Function

    Public Function GetFirefoxLocation(ProgramFilesLocation As String) As String
        Dim FirefoxLocation As String = String.Empty
        FirefoxLocation = ProgramFilesLocation & "\Mozilla Firefox\firefox.exe"
        If Not File.Exists(FirefoxLocation) Then
            Console.WriteLine("Firefox not found at " & FirefoxLocation)
            Throw New Exception("Firefox not found in " & ProgramFilesLocation)
        End If
        FirefoxLocation = """" & FirefoxLocation & """"
        Console.WriteLine("Using Firefox located at: " & FirefoxLocation)
        Return FirefoxLocation
    End Function

    Public Function GetGeckoRuntimeLocation(Optional ByVal pathToGeckoRuntime As String = "") As String
        Console.WriteLine("Searching for Gecko runtime")
        ' if the given pathToGeckoRuntime does not exist set
        ' pathToGeckoRuntime as if it were unspecified
        If Not File.Exists(pathToGeckoRuntime) Then
            pathToGeckoRuntime = String.Empty
        End If
        ' if the path to the Gecko runtime is not specified
        ' then we will look for firefox.exe in its default
        ' location
        If pathToGeckoRuntime = String.Empty Then
            pathToGeckoRuntime = Me.FindFirefox()
            If pathToGeckoRuntime = String.Empty Then
                Console.WriteLine("No suitable Gecko runtime found!")
            End If
        End If
        Return pathToGeckoRuntime
    End Function

End Class
