Imports Nukepayload2.N2Engine.UI.Text

Namespace UI.Text

    Public Interface IFontLoader
        Function LoadAsync(font As GameFont) As Task
    End Interface

End Namespace
