﻿Imports Nukepayload2.N2Engine.Platform
Imports Nukepayload2.N2Engine.UI.Elements
''' <summary>
''' 单个图片的渲染器
''' </summary>
<PlatformImpl(GetType(SpriteElement))>
Partial Friend Class GameSpriteElementRenderer
    Inherits GameElementRenderer

    Public Overrides Sub DisposeResources()
#If Not WIN2D Then
        MyBase.DisposeResources()
#End If
        DirectCast(DirectCast(View, SpriteElement).Sprite.Value, PlatformBitmapResource).Dispose()
    End Sub
End Class
