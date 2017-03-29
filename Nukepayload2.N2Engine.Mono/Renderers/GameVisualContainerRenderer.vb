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
        Dim view = DirectCast(Me.View, GameVisualContainer)
        Dim renderSize = view.RenderSize
        Debug.WriteLine($"为 {Me.GetType.Name} 分配缓存纹理。")
        RenderTarget = New RenderTarget2D(sender.GraphicsDevice, renderSize.X, renderSize.Y, False, SurfaceFormat.Color, Nothing, 1, RenderTargetUsage.PreserveContents)
    End Sub

    Dim _cachedRenderTargets As New List(Of Tuple(Of RenderTarget2D, DrawingContext))

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
        Dim children = View.GetSubNodes
        Dim containers As New List(Of GameVisualContainer)
        Dim groupedChildren = Aggregate ch In children Group By ch.Transform Into Group Into ToArray
        Dim size = device.PresentationParameters
        Dim outterDc = args.SpriteBatch
        Do While groupedChildren.Length > _cachedRenderTargets.Count
            Dim trt As New RenderTarget2D(sender.GraphicsDevice, size.BackBufferWidth, size.BackBufferHeight, False, SurfaceFormat.Color, Nothing, 1, RenderTargetUsage.PreserveContents)
            Dim tdc As New DrawingContext(args.SpriteBatch, trt)
            _cachedRenderTargets.Add(New Tuple(Of RenderTarget2D, DrawingContext)(trt, tdc))
        Loop
        For i = 0 To groupedChildren.Length - 1
            Dim k = groupedChildren(i)
            Dim cachedGroup = _cachedRenderTargets(i)
            Dim tempRt = cachedGroup.Item1
            Dim dc = cachedGroup.Item2
            device.SetRenderTarget(tempRt)
            device.Clear(Color.Transparent)
            dc.Begin()
            args.DrawingContext = dc
            For Each child In k.Group
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
            dc = Nothing
            ' 向外部 DC 绘制
            device.SetRenderTarget(RenderTarget)
            If k.Transform Is Nothing Then
                outterDc.Begin()
            Else
                Dim mat = k.Transform.GetTransformMatrix
                outterDc.Begin(SpriteSortMode.Deferred, Nothing, Nothing, Nothing, Nothing, Nothing,
                           New Matrix(mat.M11, mat.M12, 0F, 0F,
                                      mat.M21, mat.M22, 0F, 0F,
                                      0F, 0F, 1.0F, 0F,
                                      mat.M31, mat.M32, 0F, 1.0F))
            End If
            outterDc.Draw(tempRt, New Vector2, Color.White)
            outterDc.End()
        Next
        args.DrawingContext = Nothing
        For Each cont In containers
            ' 绘制容器类型
            DirectCast(cont.Renderer, MonoGameRenderer).OnDraw(sender, args)
        Next
        CommitRenderTargetToParent(device, args.SpriteBatch)
    End Sub

    Protected Overridable Sub OnBackgroundDraw(sb As SpriteBatch)

    End Sub

    ''' <summary>
    ''' 判断指定的可见对象是否超出了应该绘制的范围。超出范围的对象在拥有虚拟化功能的容器中不会被绘制。
    ''' </summary>
    ''' <param name="visual">要判断的可见对象。</param>
    ''' <returns>如果返回假，则不会虚拟化这个子可见对象。</returns>
    Protected Overridable Function ShouldVirtualize(visual As GameVisual) As Boolean
        Return False
    End Function

    Protected Overridable Sub CommitRenderTargetToParent(device As GraphicsDevice, sb As SpriteBatch)
        Dim view = Me.View
        Dim parent = view.Parent
        Dim parentRenderer = parent.Renderer
        Dim parentRT As RenderTarget2D

        ' 模板化支持
        Do Until TypeOf parentRenderer Is GameVisualContainerRenderer
            view = parentRenderer.View.Parent
            parentRenderer = view.Renderer
        Loop

        parentRT = DirectCast(parentRenderer, GameVisualContainerRenderer).RenderTarget

        device.SetRenderTarget(parentRT)
        If view.Transform Is Nothing Then
            sb.Begin()
        Else
            With view.Transform.GetTransformMatrix
                sb.Begin(SpriteSortMode.Deferred, Nothing, Nothing, Nothing, Nothing, Nothing,
                         New Matrix(.M11, .M12, 0F, 0F,
                                    .M21, .M22, 0F, 0F,
                                    0F, 0F, 1.0F, 0F,
                                    .M31, .M32, 0F, 1.0F))
            End With
        End If
        Dim loc = view.Location.Value
        Dim drawSize As New Rectangle(loc.X, loc.Y, RenderTarget.Width, RenderTarget.Height)
        OnBackgroundDraw(sb)
        Dim effectedImage = ApplyEffect(RenderTarget)
        DrawOnParent(sb, drawSize, effectedImage)
        sb.End()
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
        For Each ele In _cachedRenderTargets
            ele.Item1.Dispose()
            ' TODO: 释放 Item2 所占的内存
        Next
        _cachedRenderTargets.Clear()
    End Sub
End Class