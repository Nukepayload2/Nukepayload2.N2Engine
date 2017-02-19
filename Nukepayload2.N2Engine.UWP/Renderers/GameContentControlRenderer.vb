Imports Microsoft.Graphics.Canvas.UI.Xaml
Imports Nukepayload2.N2Engine.UI.Controls

Friend Class GameContentControlRenderer

    Friend Overrides Sub OnDraw(sender As ICanvasAnimatedControl, args As CanvasAnimatedDrawEventArgs)
        Dim content = DirectCast(View, IGameContentControl).GetContent()
        Dim renderer = DirectCast(content.Renderer, Win2DRenderer)
        renderer.OnDraw(sender, args)
    End Sub

End Class
