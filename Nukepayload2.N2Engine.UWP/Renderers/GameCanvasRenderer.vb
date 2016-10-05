Imports System.Collections.Specialized
Imports Microsoft.Graphics.Canvas.UI
Imports Microsoft.Graphics.Canvas.UI.Xaml
Imports Nukepayload2.N2Engine.UI.Elements

Public Class GameCanvasRenderer
    Inherits CanvasRendererBase(Of GameCanvas)

    ''' <summary>
    ''' 初始化总的渲染器
    ''' </summary>
    ''' <param name="view">N2引擎的视图</param>
    ''' <param name="win2DCanvas">UWP的画布</param>
    Sub New(view As GameCanvas, win2DCanvas As CanvasAnimatedControl)
        MyBase.New(view, win2DCanvas)
        HandleNewElements(view)
        view.RegisterOnChildrenChanged(AddressOf OnChildrenChanged)
    End Sub

    Private Sub HandleNewElements(view As GameCanvas)
        For Each newItems As GameElement In view.Children
            newItems.HandleRenderer(Me)
        Next
    End Sub

    ''' <summary>
    ''' 释放画布渲染器级别的资源
    ''' </summary>
    Public Overrides Sub DisposeResources()
        MyBase.DisposeResources()
        View.Clear()
    End Sub
    ''' <summary>
    ''' 更新Renderer的事件订阅
    ''' </summary>
    Protected Overridable Sub OnChildrenChanged(sender As Object, e As NotifyCollectionChangedEventArgs)
        If e.NewItems IsNot Nothing Then
            HandleNewElements(View)
        End If
        If e.OldItems IsNot Nothing Then
            Dim result = Win2DCanvas.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal,
            Sub()
                For Each oldItems As GameElement In e.OldItems
                    oldItems.UnloadRenderer(Me)
                Next
            End Sub)
        End If
    End Sub
    ''' <summary>
    ''' 创建画布渲染器级别的资源
    ''' </summary>
    Protected Overrides Sub OnCreateResources(sender As CanvasAnimatedControl, args As CanvasCreateResourcesEventArgs) Handles MyBase.CreateResources

    End Sub
    ''' <summary>
    ''' 绘制水印
    ''' </summary>
    Protected Overrides Sub OnDraw(sender As ICanvasAnimatedControl, args As CanvasAnimatedDrawEventArgs) Handles MyBase.Draw

    End Sub
    ''' <summary>
    ''' 处理开始游戏
    ''' </summary>
    Protected Overrides Sub OnGameLoopStarting(sender As ICanvasAnimatedControl, args As Object) Handles MyBase.GameLoopStarting

    End Sub
    ''' <summary>
    ''' 处理游戏循环结束
    ''' </summary>
    Protected Overrides Sub OnGameLoopStopped(sender As ICanvasAnimatedControl, args As Object) Handles MyBase.GameLoopStopped

    End Sub
    ''' <summary>
    ''' 处理水印的更新
    ''' </summary>
    Protected Overrides Sub OnUpdate(sender As ICanvasAnimatedControl, args As CanvasAnimatedUpdateEventArgs) Handles MyBase.Update

    End Sub
End Class