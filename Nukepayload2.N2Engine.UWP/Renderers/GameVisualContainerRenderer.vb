Imports System.Numerics
Imports Microsoft.Graphics.Canvas
Imports Microsoft.Graphics.Canvas.UI
Imports Microsoft.Graphics.Canvas.UI.Xaml
Imports Nukepayload2.N2Engine.UI.Elements
Imports Windows.UI

Public MustInherit Class GameVisualContainerRenderer
    Inherits Win2DRenderer
    Sub New(container As GameVisualContainer)
        MyBase.New(container)
    End Sub
    ''' <summary>
    ''' 绘制目标缓存每次的绘制结果以便父级元素对其进行绘制和变换。
    ''' </summary>
    Public Property RenderTarget As CanvasRenderTarget

    Friend Overrides Sub OnCreateResources(sender As CanvasAnimatedControl, args As CanvasCreateResourcesEventArgs)
        MyBase.OnCreateResources(sender, args)
        RenderTarget = New CanvasRenderTarget(sender, sender.Size)
    End Sub

    Dim _cachedRenderTargetDrawingSession As CanvasDrawingSession

    Friend Overrides Sub OnDraw(sender As ICanvasAnimatedControl, args As CanvasAnimatedDrawEventArgs)
        ' 对象可见性支持
        If View.IsVisible.CanRead Then
            If Not View.IsVisible.Value Then
                Return
            End If
        End If
        ' 绘制
        Dim children = View.GetSubNodes
        Dim containers As New List(Of GameVisualContainer)
        Using ds = RenderTarget.CreateDrawingSession
            _cachedRenderTargetDrawingSession = ds
            ds.Clear(Colors.Transparent)
            OnBackgroundDraw(ds)
            For Each child In children
                If ShouldVirtualize(child) Then
                    Continue For
                End If
                Dim cont = TryCast(child, GameVisualContainer)
                If cont IsNot Nothing Then
                    containers.Add(cont)
                Else
                    ' 绘制子元素
                    If child.Transform IsNot Nothing Then
                        ds.Transform = child.Transform.GetTransformMatrix
                    Else
                        ds.Transform = Matrix3x2.Identity
                    End If
                    DirectCast(child.Renderer, Win2DRenderer).OnDraw(sender, New CanvasAnimatedDrawEventArgs(ds, args.Timing))
                End If
            Next
            _cachedRenderTargetDrawingSession = Nothing
        End Using
        ' 下一层绘制
        For Each cont In containers
            DirectCast(cont.Renderer, GameVisualContainerRenderer).OnDraw(sender, args)
        Next
        CommitRenderTargetToParent(args.DrawingSession)
    End Sub

    ''' <summary>
    ''' 判断指定的可见对象是否超出了应该绘制的范围。超出范围的对象在拥有虚拟化功能的容器中不会被绘制。
    ''' </summary>
    ''' <param name="visual">要判断的可见对象。</param>
    ''' <returns>如果返回假，则不会虚拟化这个子可见对象。</returns>
    Protected Overridable Function ShouldVirtualize(visual As GameVisual) As Boolean
        Return False
    End Function

    ''' <summary>
    ''' 向父级 <see cref="RenderTarget"/> 绘制当前缓存的 <see cref="RenderTarget"/> 。如果没有上一级，则向 <paramref name="backBuffer"/> 绘制。
    ''' </summary>
    ''' <param name="backBuffer">整个游戏画布的后缓冲区</param>
    Protected Overridable Sub CommitRenderTargetToParent(backBuffer As CanvasDrawingSession)
        Dim view = Me.View
        Dim parent = view.Parent
        Dim parentRenderer = parent.Renderer
        ' 模板化支持
        Do Until TypeOf parentRenderer Is GameVisualContainerRenderer
            view = parentRenderer.View.Parent
            parentRenderer = view.Renderer
        Loop

        Dim parentContainerRenderer = DirectCast(parentRenderer, GameVisualContainerRenderer)
        Dim parentRT = parentContainerRenderer.RenderTarget
        Dim loc = view.Location.Value

        Dim effectedImage = ApplyEffect(RenderTarget)
        Dim rtSize = RenderTarget.Size

        If TypeOf Me Is GameCanvasRenderer Then
            If Me.View.Transform IsNot Nothing Then
                backBuffer.Transform = Me.View.Transform.GetTransformMatrix
            End If
            DrawOnParent(backBuffer, loc, effectedImage)
        ElseIf parentContainerRenderer._cachedRenderTargetDrawingSession IsNot Nothing Then
            Dim ds = parentContainerRenderer._cachedRenderTargetDrawingSession
            If view.Transform IsNot Nothing Then
                ds.Transform = view.Transform.GetTransformMatrix
            End If
            DrawOnParent(ds, loc, effectedImage)
        Else
            Using ds = parentRT.CreateDrawingSession
                If view.Transform IsNot Nothing Then
                    ds.Transform = view.Transform.GetTransformMatrix
                End If
                DrawOnParent(ds, loc, effectedImage)
            End Using
        End If
    End Sub

    Protected Overridable Sub OnBackgroundDraw(ds As CanvasDrawingSession)

    End Sub

    ''' <summary>
    ''' 将当前缓存的已经被特效处理的图像绘制到上级图像缓冲区。
    ''' </summary>
    ''' <param name="ds">上级图像缓冲区绘图的会话</param>
    ''' <param name="loc">原本要绘制到的位置</param>
    ''' <param name="effectedImage">经过特效处理的缓存的图像</param>
    Protected Overridable Sub DrawOnParent(ds As CanvasDrawingSession, loc As System.Numerics.Vector2, effectedImage As ICanvasImage)
        ds.DrawImage(effectedImage, loc.X, loc.Y)
    End Sub

    ''' <summary>
    ''' 元素绘制后，向父级 <see cref="RenderTarget"/> 绘制之前调用此过程。通常用当前 <see cref="RenderTarget"/> 作为着色器效果的输入，返回着色器效果。
    ''' </summary>
    Protected Overridable Function ApplyEffect(source As CanvasRenderTarget) As ICanvasImage
        If View.Effect Is Nothing Then
            Return source
        End If
        Return source
    End Function

    Public Overrides Sub DisposeResources()
        MyBase.DisposeResources()
        RenderTarget?.Dispose()
    End Sub
End Class
