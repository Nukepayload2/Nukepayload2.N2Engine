Imports Nukepayload2.N2Engine.Resources
Imports Nukepayload2.N2Engine.UI.Text

Public Class FontManager
    Dim fontLoader As IFontLoader

    Sub New()
        fontLoader = FontLoaderFactory.Create
        SegoeUI14Black = New GameFont("SegoeUI14Black", 14.0F, "Segoe UI", FontStretch.Normal, FontWeight.Normal,
                                      FontStyle.Normal, True, New Uri("n2-res-emb:///Fonts/SegoeUI14.n2fnt"),
                                      New Nukepayload2.N2Engine.Foundation.Color(0, 0, 0))
    End Sub

    Public Async Function LoadAsync() As Task
        Await fontLoader.LoadAsync(SegoeUI14Black)
    End Function

    Public ReadOnly Property SegoeUI14Black As GameFont
End Class
