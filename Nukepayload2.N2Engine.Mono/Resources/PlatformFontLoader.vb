Imports Nukepayload2.N2Engine.Resources
Imports Nukepayload2.N2Engine.UI.Text
Imports Nukepayload2.UI.SpriteFonts

Friend Class PlatformFontLoader

    Public Async Function LoadAsync(font As GameFont) As Task Implements IFontLoader.LoadAsync
#If STRICT_ARG_CHECK Then
        If font Is Nothing Then
            Throw New ArgumentNullException(NameOf(font))
        End If
        If font.SpriteFontResourceId Is Nothing Then
            Throw New ArgumentNullException(NameOf(font.SpriteFontResourceId))
        End If
#End If
        Dim fnt As New N2SpriteFont
        Await Task.Run(Sub() fnt.Load(Function()
                                          Dim res = ResourceLoader.GetForCurrentView
                                          Dim strm = res.GetResourceEmbeddedStream(font.SpriteFontResourceId)
                                          Return strm
                                      End Function, Function(t) New N2FontTileMonoGame(t)))
        font.SpriteData = fnt
    End Function
End Class
