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
        ' 对象可见性支持
        If View.IsVisible.CanRead Then
            If Not View.IsVisible.Value Then
                Return
            End If
        End If
        ' 准备绘制
        Dim device = GraphicsDeviceManagerExtension.SharedDevice
        device.SetRenderTarget(RenderTarget)
        device.Clear(Color.Transparent)
        Dim children = DirectCast(View, GameVisualContainer).Children
        Dim containers As New List(Of GameVisualContainer)
        ' 初始化 DC
        Static dc As New DrawingContext(args.SpriteBatch, RenderTarget)
        args.DrawingContext = dc
        Dim groupedChildren = From ch In children Group By ch.Transform Into Group
        For Each k In groupedChildren
            dc.Begin()
            If k.Transform Is Nothing Then
                dc.Transform = Nothing
            Else
                Dim mat = k.Transform.GetTransformMatrix
                dc.Transform = New Transform2D(mat)
            End If
            For Each child In children
                If ShouldVirtualize(child) Then
                    Continue For
                End If
                Dim cont = TryCast(child, GameVisualContainer)
                If cont IsNot Nothing Then
                    containers.Add(cont)
                Else
                    ' 绘制子元素
                    DirectCast(child.Renderer, MonoGameRenderer).OnDraw(sender, args)
                End If
            Next
            dc.End()
        Next
        args.DrawingContext = Nothing
        For Each cont In containers
            ' 绘制容器类型
            DirectCast(cont.Renderer, MonoGameRenderer).OnDraw(sender, args)
        Next
        CommitRenderTargetToParent(device, args.SpriteBatch)
    End Sub

    ''' <summary>
    ''' 判断指定的可见对象是否超出了应该绘制的范围。超出范围的对象在拥有虚拟化功能的容器中不会被绘制。
    ''' </summary>
    ''' <param name="visual">要判断的可见对象。</param>
    ''' <returns>如果返回假，则不会虚拟化这个子可见对象。</returns>
    Protected Overridable Function ShouldVirtualize(visual As GameVisual) As Boolean
        Return False
    End Function

    Protected Overridable Sub CommitRenderTargetToParent(device As GraphicsDevice, dc As SpriteBatch)
        Dim parent = View.Parent
        Dim parentRenderer = parent.Renderer
        Dim parentRT = DirectCast(parentRenderer, GameVisualContainerRenderer).RenderTarget
        device.SetRenderTarget(parentRT)
        If View.Transform Is Nothing Then
            dc.Begin()
        Else
            With View.Transform.GetTransformMatrix
                dc.Begin(SpriteSortMode.Deferred, Nothing, Nothing, Nothing, Nothing, Nothing,
                         New Matrix(.M11, .M12, 0F, 0F,
                                    .M21, .M22, 0F, 0F,
                                    0F, 0F, 1.0F, 0F,
                                    .M31, .M32, 0F, 1.0F))
            End With
        End If
        Dim loc = View.Location.Value
        Dim drawSize As New Rectangle(loc.X, loc.Y, RenderTarget.Width, RenderTarget.Height)
        Dim effectedImage = ApplyEffect(RenderTarget)
        DrawOnParent(dc, drawSize, effectedImage)
        dc.End()
    End Sub

    ''' <summary>
    ''' 将当前缓存的已经被特效处理的图像绘制到上级图像缓冲区。
    ''' </summary>
    ''' <param name="dc">上级图像缓冲区绘图的会话</param>
    ''' <param name="drawSize">绘制的大小</param>
    ''' <param name="effectedImage">经过特效处理的缓存的图像</param>
    Protected Overridable Sub DrawOnParent(dc As SpriteBatch, drawSize As Rectangle, effectedImage As Texture2D)
        dc.Draw(effectedImage, drawSize, Color.White)
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