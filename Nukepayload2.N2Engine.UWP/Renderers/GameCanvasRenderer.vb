Imports Microsoft.Graphics.Canvas
Imports Microsoft.Graphics.Canvas.UI
Imports Microsoft.Graphics.Canvas.UI.Xaml
Imports Nukepayload2.N2Engine.Foundation
Imports Nukepayload2.N2Engine.Information
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

    Shared WithEvents DisplayInfo As DisplayInformation

    Public Shared Sub SynchronizeBackBufferInformation(host As CanvasAnimatedControl)
        DisplayInfo = DisplayInformation.GetForCurrentView
        BackBufferInformation.NotifyDpiChanged(DisplayInfo.LogicalDpi)
        Dim size = host.RenderSize
        Dim dpiScale = DisplayInfo.RawPixelsPerViewPixel
        BackBufferInformation.ScreenSize = New SizeInInteger(CUInt(DisplayInfo.ScreenWidthInRawPixels / dpiScale),
                                                             CUInt(DisplayInfo.ScreenHeightInRawPixels / dpiScale))
        BackBufferInformation.NotifyViewPortSizeChanged(New SizeInInteger(CUInt(size.Width), CUInt(size.Height)))
    End Sub

    ''' <summary>
    ''' 释放画布渲染器级别的资源
    ''' </summary>
    Public Overrides Sub DisposeResources()
        MyBase.DisposeResources()
        DirectCast(View, GameCanvas).Children.Clear()
        _Win2DCanvas = Nothing
        DisplayInfo = Nothing
    End Sub

    Private Sub DoCanvasOperation(act As Action(Of Win2DRenderer))
        For Each vie In View.HierarchyWalk(Function(node) node.GetSubNodes)
            act(DirectCast(vie.Renderer, Win2DRenderer))
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
        Dim asyncLoadTasks As New List(Of Task)
        DoCanvasOperation(Sub(renderer)
                              renderer.OnCreateResources(sender, args)
                              Dim createAsync = renderer.CreateResourceAsync(sender, args)
                              If createAsync IsNot Nothing Then
                                  asyncLoadTasks.Add(createAsync)
                              End If
                          End Sub)
        If asyncLoadTasks.Any Then
            args.TrackAsyncAction(Task.WhenAll(asyncLoadTasks).AsAsyncAction)
        End If
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

    Private Sub Win2DCanvas_SizeChanged(sender As Object, e As SizeChangedEventArgs) Handles Win2DCanvas.SizeChanged
        Dim size = Win2DCanvas.RenderSize
        BackBufferInformation.NotifyViewPortSizeChanged(New SizeInInteger(CInt(size.Width), CInt(size.Height)))
    End Sub

    Private Shared Sub DisplayInfo_DpiChanged(sender As DisplayInformation, args As Object) Handles DisplayInfo.DpiChanged
        BackBufferInformation.NotifyDpiChanged(sender.LogicalDpi)
    End Sub
End Class