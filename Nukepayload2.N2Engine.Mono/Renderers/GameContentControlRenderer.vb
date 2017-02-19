Imports Microsoft.Xna.Framework
Imports Nukepayload2.N2Engine.UI.Controls

Friend Class GameContentControlRenderer
    Friend Overrides Sub OnDraw(sender As Game, args As MonogameDrawEventArgs)
        Dim content = DirectCast(View, IGameContentControl).GetContent()
        Dim renderer = DirectCast(content.Renderer, MonoGameRenderer)
        renderer.OnDraw(sender, args)
    End Sub
End Class
