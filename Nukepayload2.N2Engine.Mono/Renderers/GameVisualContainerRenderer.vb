Imports Microsoft.Xna.Framework
Imports Microsoft.Xna.Framework.Graphics
Imports Nukepayload2.N2Engine.UI.Elements
Imports RaisingStudio.Xna.Graphics

Public MustInherit Class GameVisualContainerRenderer
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
        RenderTarget = New RenderTarget2D(sender.GraphicsDevice, size.BackBufferWidth, size.BackBufferHeight, False, SurfaceFormat.Color, Nothing, 1, RenderTargetUsage.PreserveContents)
    End Sub

    Friend Overrides Sub OnDraw(sender As Game, args As MonogameDrawEventArgs)
        Dim device = GraphicsDeviceManagerExtension.SharedDevice
        'Debug.WriteLine($"设置绘制目标为：{Me.GetType.Name} 的缓存纹理。")
        device.SetRenderTarget(RenderTarget)
        'Debug.WriteLine("清屏: 透明色。" + Me.GetType.Name)
        device.Clear(Color.Transparent)
        'Debug.WriteLine("开始绘制元素。" + Me.GetType.Name)
        Dim children = DirectCast(View, GameVisualContainer).Children
        Dim containers As New List(Of GameVisualContainer)
        Static dc As New DrawingContext(args.SpriteBatch, RenderTarget)
        args.DrawingContext = dc
        dc.Begin()
        For Each child In children
            Dim cont = TryCast(child, GameVisualContainer)
            If cont IsNot Nothing Then
                containers.Add(cont)
            Else
                'Debug.WriteLine($"绘制元素: {Me.GetType.Name} ---> {child.GetType.Name}")
                DirectCast(child.Renderer, MonoGameRenderer).OnDraw(sender, args)
            End If
        Next
        dc.End()
        args.DrawingContext = Nothing
        'Debug.WriteLine("结束绘制元素。" + Me.GetType.Name)
        For Each cont In containers
            'Debug.WriteLine($"处理元素容器: {Me.GetType.Name} ---> {cont.GetType.Name}")
            DirectCast(cont.Renderer, MonoGameRenderer).OnDraw(sender, args)
        Next
        CommitRenderTargetToParent(device, args.SpriteBatch)
    End Sub

    Protected Overridable Sub CommitRenderTargetToParent(device As GraphicsDevice, dc As SpriteBatch)
        Dim parent = View.Parent
        Dim parentRenderer = parent.Renderer
        Dim parentRT = DirectCast(parentRenderer, GameVisualContainerRenderer).RenderTarget
        'Debug.WriteLine($"设置绘制目标为：{parentRenderer.GetType.Name} 的缓存纹理。")
        device.SetRenderTarget(parentRT)
        dc.Begin()
        Dim loc = View.Location.Value
        'Debug.WriteLine($"提交绘制结果: {parentRenderer.GetType.Name} <--- {Me.GetType.Name}")
        dc.Draw(ApplyEffect(RenderTarget), New Rectangle(loc.X, loc.Y, parentRT.Width, parentRT.Height), Color.White)
        dc.End()
    End Sub

    ''' <summary>
    ''' 元素绘制后，向父级 <see cref="RenderTarget"/> 绘制之前调用此过程。通常用当前 <see cref="RenderTarget"/> 作为着色器效果的输入，返回着色器效果。
    ''' </summary>
    Protected Overridable Function ApplyEffect(source As RenderTarget2D) As Texture2D
        Return source
    End Function

    Public Overrides Sub DisposeResources()
        RenderTarget?.Dispose()
    End Sub
End Class