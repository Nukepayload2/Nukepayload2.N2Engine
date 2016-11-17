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
        CreateRenderTarget(sender)
    End Sub

    Private Sub CreateRenderTarget(sender As Game)
        Dim device = GraphicsDeviceManagerExtension.SharedDevice
        Dim size = device.PresentationParameters
        RenderTarget = New RenderTarget2D(sender.GraphicsDevice, size.BackBufferWidth, size.BackBufferHeight, False, SurfaceFormat.Color, Nothing)
    End Sub

    Friend Overrides Sub OnDraw(sender As Game, args As MonogameDrawEventArgs)
        Dim device = GraphicsDeviceManagerExtension.SharedDevice
        Dim dc = args.DrawingContext
        device.SetRenderTarget(RenderTarget)
        dc.Begin()
        For Each child In DirectCast(View, GameVisualContainer).Children
            CType(child.Renderer, MonoGameRenderer).OnDraw(sender, args)
        Next
        dc.End()
        ApplyEffect(sender, args)
        DrawOnParent(device, dc)
        ClearRenderTarget(device)
    End Sub

    Protected Overridable Sub DrawOnParent(device As GraphicsDevice, dc As RaisingStudio.Xna.Graphics.DrawingContext)
        Dim parent = View.Parent
        If parent IsNot Nothing Then
            Dim parentRT = DirectCast(parent.Renderer, GameCanvasContainerRenderer).RenderTarget
            device.SetRenderTarget(parentRT)
            dc.Begin()
            Dim loc = View.Location.Value
            Dim drawRect As New Rectangle(loc.X, loc.Y, parentRT.Width, parentRT.Height)
            dc.DrawTexture(RenderTarget, drawRect, Color.White)
            dc.End()
        End If
    End Sub

    Private Sub ClearRenderTarget(device As GraphicsDevice)
        device.SetRenderTarget(RenderTarget)
        device.Clear(Color.Transparent)
    End Sub

    ''' <summary>
    ''' 元素绘制后，向父级 <see cref="RenderTarget"/> 绘制之前调用此过程。通常用于对当前 <see cref="RenderTarget"/> 进行修改，以便附加着色器效果。
    ''' </summary>
    Protected Overridable Sub ApplyEffect(sender As Game, args As MonogameDrawEventArgs)

    End Sub

    Public Overrides Sub DisposeResources()
        RenderTarget?.Dispose()
    End Sub
End Class