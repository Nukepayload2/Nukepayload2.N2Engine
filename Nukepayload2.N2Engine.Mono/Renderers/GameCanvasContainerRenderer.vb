Imports Microsoft.Xna.Framework
Imports Microsoft.Xna.Framework.Graphics
Imports Nukepayload2.N2Engine.UI.Elements

Public MustInherit Class GameCanvasContainerRenderer
    Inherits MonoGameRenderer
    Sub New(view As GameVisualContainer)
        MyBase.New(view)
    End Sub

    ''' <summary>
    ''' 绘制目标缓存每次的绘制结果以便父级元素对其进行绘制和变换。
    ''' </summary>
    Public Property RenderTarget As RenderTarget2D

    Friend Overrides Sub OnCreateResources(sender As Game, args As MonogameCreateResourcesEventArgs)
        MyBase.OnCreateResources(sender, args)
        Dim device = GraphicsDeviceManagerExtension.SharedDevice
        Dim size = device.PresentationParameters
        RenderTarget = New RenderTarget2D(sender.GraphicsDevice, size.BackBufferWidth, size.BackBufferHeight, False, SurfaceFormat.Color, Nothing)
    End Sub

    Friend Overrides Sub OnDraw(sender As Game, args As MonogameDrawEventArgs)
        Dim device = GraphicsDeviceManagerExtension.SharedDevice
        device.SetRenderTarget(RenderTarget)
        args.DrawingContext.Begin()
        For Each child In DirectCast(View, GameVisualContainer).Children
            CType(child.Renderer, MonoGameRenderer).OnDraw(sender, args)
        Next
        args.DrawingContext.End()
        Dim parent = View.Parent
        If parent IsNot Nothing Then
            Dim parentRT = DirectCast(parent.Renderer, GameCanvasContainerRenderer).RenderTarget
            device.SetRenderTarget(parentRT)
            args.DrawingContext.Begin()
            args.DrawingContext.DrawTexture(RenderTarget, New Rectangle(0, 0, parentRT.Width, parentRT.Height), Color.White)
            args.DrawingContext.End()
        End If
    End Sub

    Public Overrides Sub DisposeResources()
        RenderTarget?.Dispose()
    End Sub
End Class