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
                G.ConsoleRedirector(Environment.GetCommandLineArgs()(1), G.PassArguments(2))
            Case Is = 2
                G.ConsoleRedirector(Environment.GetCommandLineArgs()(1))
            Case Else
                G.ConsoleRedirector()
        End Select

    End Sub

End Module
