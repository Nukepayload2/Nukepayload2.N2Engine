Imports Nukepayload2.N2Engine.Platform
Imports Nukepayload2.N2Engine.UI.Elements
''' <summary>
''' 单个图片的渲染器
''' </summary>
<PlatformImpl(GetType(SpriteElement))>
Partial Friend Class GameSpriteElementRenderer
    Inherits GameElementRenderer(Of SpriteElement)

    Public Overrides Sub DisposeResources()
        MyBase.DisposeResources()
        DirectCast(View.Sprite.Value, PlatformBitmapResource).Dispose()
    End Sub
End Class
