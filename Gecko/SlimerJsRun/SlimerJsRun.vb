' Builds the executable part of the SlimerJsLauncher
' of course you could put the lib with this in the 
' same project and not end up with a dll.
Module SlimerJsRun
    Public Sub Main()
        Dim slimer As New SlimerJsLib()
        slimer.HelpMessage = My.Resources.help_en
        slimer.Run()
    End Sub
End Module
