Imports Microsoft.Graphics.Canvas
Imports Microsoft.Graphics.Canvas.UI
Imports Microsoft.Graphics.Canvas.UI.Xaml
Imports Nukepayload2.N2Engine.Linq
Imports Nukepayload2.N2Engine.UI.Elements

Public Class GameCanvasRenderer
    Inherits GameVisualContainerRenderer

    ''' <summary>
    ''' 用于呈现即时2D动画的Xaml控件
    ''' </summary>
    Public WithEvents Win2DCanvas As CanvasAnimatedControl

    Dim _eventMediator As UWPEventMediator
    ''' <summary>
    ''' 初始化总的渲染器
    ''' </summary>
    ''' <param name="view">N2引擎的视图</param>
    ''' <param name="win2DCanvas">UWP的画布</param>
    Sub New(view As GameCanvas, win2DCanvas As CanvasAnimatedControl)
        MyBase.New(view)
        view.Renderer = Me
        Me.Win2DCanvas = win2DCanvas
        _eventMediator = New UWPEventMediator(view)
    End Sub

    ''' <summary>
    ''' 释放画布渲染器级别的资源
    ''' </summary>
    Public Overrides Sub DisposeResources()
        MyBase.DisposeResources()
        DirectCast(View, GameCanvas).Children.Clear()
        _Win2DCanvas = Nothing
    End Sub

    Private Sub DoCanvasOperation(act As Action(Of UWPRenderer))
        For Each vie In View.HierarchyWalk(Function(node) TryCast(node, GameVisualContainer)?.Children)
            act(DirectCast(vie.Renderer, UWPRenderer))
        Next
    End Sub

    ''' <summary>
    ''' 处理游戏循环结束
    ''' </summary>
    Private Sub Game_GameLoopStopped(sender As ICanvasAnimatedControl, args As Object) Handles Win2DCanvas.GameLoopStopped
        DoCanvasOperation(Sub(renderer) renderer.OnGameLoopStopped(sender, args))
    End Sub

    ''' <summary>
    ''' 创建画布渲染器级别的资源
    ''' </summary>
    Private Sub Game_CreateResources(sender As CanvasAnimatedControl, args As CanvasCreateResourcesEventArgs) Handles Win2DCanvas.CreateResources
        DoCanvasOperation(Sub(renderer) renderer.OnCreateResources(sender, args))
    End Sub
    ''' <summary>
    ''' 绘制水印
    ''' </summary>
    Private Sub Game_Draw(sender As ICanvasAnimatedControl, args As CanvasAnimatedDrawEventArgs) Handles Win2DCanvas.Draw
        OnDraw(sender, args)
    End Sub

    Protected Overrides Sub CommitRenderTargetToParent(backBuffer As CanvasDrawingSession)
        Dim loc = View.Location.Value
        Dim rtSize = RenderTarget.Size
        If View.Transform IsNot Nothing Then
            DrawImageWithTransform2D(loc, rtSize.Width, rtSize.Height, backBuffer, RenderTarget)
        Else
            backBuffer.DrawImage(RenderTarget)
        End If
    End Sub

    ''' <summary>
    ''' 处理开始游戏
    ''' </summary>
    Private Sub Game_GameLoopStarting(sender As ICanvasAnimatedControl, args As Object) Handles Win2DCanvas.GameLoopStarting
        DoCanvasOperation(Sub(renderer) renderer.OnGameLoopStarting(sender, args))
    End Sub
    ''' <summary>
    ''' 处理视图和水印的更新
    ''' </summary>
    Private Sub Game_Update(sender As ICanvasAnimatedControl, args As CanvasAnimatedUpdateEventArgs) Handles Win2DCanvas.Update
        Dim isFrozen = View.IsFrozen
        If isFrozen.CanRead AndAlso isFrozen.Value Then
            Return
        End If
        DoCanvasOperation(Sub(renderer)
                              Dim frz = renderer.View.IsFrozen
                              If frz.CanRead AndAlso frz.Value Then
                                  Return
                              End If
                              renderer.OnUpdate(sender, args)
                          End Sub)
    End Sub
End Class