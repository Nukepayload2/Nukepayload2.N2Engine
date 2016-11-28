Imports Microsoft.Graphics.Canvas
Imports Microsoft.Graphics.Canvas.UI
Imports Microsoft.Graphics.Canvas.UI.Xaml
Imports Nukepayload2.N2Engine.UI.Elements
Imports Windows.UI

Public MustInherit Class GameVisualContainerRenderer
    Inherits UWPRenderer
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

    Friend Overrides Sub OnDraw(sender As ICanvasAnimatedControl, args As CanvasAnimatedDrawEventArgs)
        Dim children = DirectCast(View, GameVisualContainer).Children
        Dim containers As New List(Of GameVisualContainer)
        Using ds = RenderTarget.CreateDrawingSession
            ds.Clear(Colors.Transparent)
            For Each child In children
                Dim cont = TryCast(child, GameVisualContainer)
                If cont IsNot Nothing Then
                    containers.Add(cont)
                Else
                    DirectCast(child.Renderer, UWPRenderer).OnDraw(sender, New CanvasAnimatedDrawEventArgs(ds, args.Timing))
                End If
            Next
        End Using
        For Each cont In containers
            DirectCast(cont.Renderer, GameVisualContainerRenderer).OnDraw(sender, args)
        Next
        CommitRenderTargetToParent(args.DrawingSession)
    End Sub

    ''' <summary>
    ''' 向上一级父级 <see cref="RenderTarget"/> 绘制当前缓存的 <see cref="RenderTarget"/> 。
    ''' </summary>
    Protected Overridable Sub CommitRenderTargetToParent(backBuffer As CanvasDrawingSession)
        Dim parent = View.Parent
        Dim parentRenderer = parent.Renderer
        Dim parentRT = DirectCast(parentRenderer, GameVisualContainerRenderer).RenderTarget
        Using ds = parentRT.CreateDrawingSession
            Dim loc = View.Location.Value
            ds.DrawImage(ApplyEffect(RenderTarget), loc.X, loc.Y)
        End Using
    End Sub

    ''' <summary>
    ''' 元素绘制后，向父级 <see cref="RenderTarget"/> 绘制之前调用此过程。通常用当前 <see cref="RenderTarget"/> 作为着色器效果的输入，返回着色器效果。
    ''' </summary>
    Protected Overridable Function ApplyEffect(source As CanvasRenderTarget) As ICanvasImage
        Return source
    End Function

    Public Overrides Sub DisposeResources()
        MyBase.DisposeResources()
        RenderTarget?.Dispose()
    End Sub
End Class
