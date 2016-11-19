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
        Debug.WriteLine($"为 {Me.GetType.Name} 分配缓存纹理。")
        RenderTarget = New RenderTarget2D(sender.GraphicsDevice, size.BackBufferWidth, size.BackBufferHeight)
    End Sub

    Friend Overrides Sub OnDraw(sender As Game, args As MonogameDrawEventArgs)
        Dim device = GraphicsDeviceManagerExtension.SharedDevice
        Debug.WriteLine($"设置绘制目标为：{Me.GetType.Name} 的缓存纹理。")
        device.SetRenderTarget(RenderTarget)
        Debug.WriteLine("清屏: 透明色。" + Me.GetType.Name)
        device.Clear(Color.Transparent)
        Debug.WriteLine("开始绘制元素。" + Me.GetType.Name)
        Dim dc = args.DrawingContext
        dc.Begin()
        Dim children = DirectCast(View, GameVisualContainer).Children
        Dim containers As New List(Of GameVisualContainer)
        For Each child In children
            Dim cont = TryCast(child, GameVisualContainer)
            If cont IsNot Nothing Then
                containers.Add(child)
            Else
                Debug.WriteLine($"绘制元素: {Me.GetType.Name} ---> {child.GetType.Name}")
                CType(child.Renderer, MonoGameRenderer).OnDraw(sender, args)
            End If
        Next
        dc.End()
        Debug.WriteLine("结束绘制元素。" + Me.GetType.Name)
        For Each cont In containers
            Debug.WriteLine($"处理元素容器: {Me.GetType.Name} ---> {cont.GetType.Name}")
            CType(cont.Renderer, MonoGameRenderer).OnDraw(sender, args)
        Next
        ApplyEffect(sender, args)
        CommitRenderTargetToParent(device, dc)
    End Sub

    Protected Overridable Sub CommitRenderTargetToParent(device As GraphicsDevice, dc As RaisingStudio.Xna.Graphics.DrawingContext)
        Dim parent = View.Parent
        Dim parentRenderer = parent.Renderer
        Dim parentRT = DirectCast(parentRenderer, GameCanvasContainerRenderer).RenderTarget
        Debug.WriteLine($"设置绘制目标为：{parentRenderer.GetType.Name} 的缓存纹理。")
        device.SetRenderTarget(parentRT)
        device.Clear(Color.White)
        dc.Begin()
        Dim loc = View.Location.Value
        Dim drawRect As New Rectangle(loc.X, loc.Y, parentRT.Width, parentRT.Height)
        Debug.WriteLine($"提交绘制结果: {parentRenderer.GetType.Name} <--- {Me.GetType.Name}")
        dc.DrawTexture(RenderTarget, drawRect, Color.White)
        dc.End()
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