' Builds the executable utilizing GeckoLib
'
' Author: Matthew Christopher Kastor-Inare III
' matthewkastor@gmail.com
' Atropa Inc. Intl.
' License: GPLv3
Module ConsoleRedirector

    Sub Main()
        Dim G As New GeckoLib.GeckoLib()

        G.HelpMessage = My.Resources.help_en

        Select Case Environment.GetCommandLineArgs().Count
            Case Is >= 3
                ' if there are arguments then pass them, minus the first two
                ' arg 0 is the full path to ConsoleRedirector.exe
                G.ConsoleRedirector(Environment.GetCommandLineArgs()(1), G.PassArguments(2))
            Case Is = 2
                ' if all that was supplied is a gecko runtime location
                ' then there are no arguments to pass and we can leave
                ' the optional argument out
                G.ConsoleRedirector(Environment.GetCommandLineArgs()(1))
            Case Else
                G.ConsoleRedirector()
        End Select

    End Sub

End Module
