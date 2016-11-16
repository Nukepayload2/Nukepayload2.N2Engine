Imports Microsoft.Graphics.Canvas.UI
Imports Microsoft.Graphics.Canvas.UI.Xaml
Imports Nukepayload2.N2Engine.UI.Elements
''' <summary>
''' UWP的画布渲染器的基类。将N2引擎的视图与UWP的视图连接起来。
''' </summary>
Public MustInherit Class CanvasRendererBase
    Inherits UWPRenderer
    ''' <summary>
    ''' 用于呈现即时2D动画的Xaml控件
    ''' </summary>
    Public ReadOnly Property Win2DCanvas As CanvasAnimatedControl
    ''' <summary>
    ''' 初始化UWP渲染器
    ''' </summary>
    ''' <param name="view">N2引擎的视图</param>
    ''' <param name="win2DCanvas">UWP的画布</param>
    Sub New(view As GameVisual, win2DCanvas As CanvasAnimatedControl)
        MyBase.New(view)
        Me.Win2DCanvas = win2DCanvas
    End Sub
    ''' <summary>
    ''' 释放资源。这里是手动解除对 Win2DCanvas 的引用
    ''' </summary>
    Public Overrides Sub DisposeResources()
        _Win2DCanvas = Nothing
    End Sub
    ''' <summary>
    ''' 创建游戏中所需的平台特定的资源
    ''' </summary>
    Public Custom Event CreateResources As TypedEventHandler(Of CanvasAnimatedControl, CanvasCreateResourcesEventArgs)
        AddHandler(value As TypedEventHandler(Of CanvasAnimatedControl, CanvasCreateResourcesEventArgs))
            AddHandler Win2DCanvas.CreateResources, value
        End AddHandler
        RemoveHandler(value As TypedEventHandler(Of CanvasAnimatedControl, CanvasCreateResourcesEventArgs))
            RemoveHandler Win2DCanvas.CreateResources, value
        End RemoveHandler
        RaiseEvent(sender As CanvasAnimatedControl, args As CanvasCreateResourcesEventArgs)
            OnCreateResources(sender, args)
        End RaiseEvent
    End Event
    ''' <summary>
    ''' 给附加的画布绘制内容
    ''' </summary>
    Public Custom Event Draw As TypedEventHandler(Of ICanvasAnimatedControl, CanvasAnimatedDrawEventArgs)
        AddHandler(value As TypedEventHandler(Of ICanvasAnimatedControl, CanvasAnimatedDrawEventArgs))
            AddHandler Win2DCanvas.Draw, value
        End AddHandler
        RemoveHandler(value As TypedEventHandler(Of ICanvasAnimatedControl, CanvasAnimatedDrawEventArgs))
            RemoveHandler Win2DCanvas.Draw, value
        End RemoveHandler
        RaiseEvent(sender As ICanvasAnimatedControl, args As CanvasAnimatedDrawEventArgs)
            OnDraw(sender, args)
        End RaiseEvent
    End Event

    ''' <summary>
    ''' 游戏循环开始之前引发的事件
    ''' </summary>
    Public Custom Event GameLoopStarting As TypedEventHandler(Of ICanvasAnimatedControl, Object)
        AddHandler(value As TypedEventHandler(Of ICanvasAnimatedControl, Object))
            AddHandler Win2DCanvas.GameLoopStarting, value
        End AddHandler
        RemoveHandler(value As TypedEventHandler(Of ICanvasAnimatedControl, Object))
            RemoveHandler Win2DCanvas.GameLoopStarting, value
        End RemoveHandler
        RaiseEvent(sender As ICanvasAnimatedControl, args As Object)
            OnGameLoopStarting(sender, args)
        End RaiseEvent
    End Event
    ''' <summary>
    ''' 游戏循环结束之后引发的事件
    ''' </summary>
    Public Custom Event GameLoopStopped As TypedEventHandler(Of ICanvasAnimatedControl, Object)
        AddHandler(value As TypedEventHandler(Of ICanvasAnimatedControl, Object))
            AddHandler Win2DCanvas.GameLoopStopped, value
        End AddHandler
        RemoveHandler(value As TypedEventHandler(Of ICanvasAnimatedControl, Object))
            RemoveHandler Win2DCanvas.GameLoopStopped, value
        End RemoveHandler
        RaiseEvent(sender As ICanvasAnimatedControl, args As Object)
            OnGameLoopStopped(sender, args)
        End RaiseEvent
    End Event

    ''' <summary>
    ''' 在这里为动画更新数据
    ''' </summary>
    Public Custom Event Update As TypedEventHandler(Of ICanvasAnimatedControl, CanvasAnimatedUpdateEventArgs)
        AddHandler(value As TypedEventHandler(Of ICanvasAnimatedControl, CanvasAnimatedUpdateEventArgs))
            AddHandler Win2DCanvas.Update, value
        End AddHandler
        RemoveHandler(value As TypedEventHandler(Of ICanvasAnimatedControl, CanvasAnimatedUpdateEventArgs))
            RemoveHandler Win2DCanvas.Update, value
        End RemoveHandler
        RaiseEvent(sender As ICanvasAnimatedControl, args As CanvasAnimatedUpdateEventArgs)
            OnUpdate(sender, args)
        End RaiseEvent
    End Event
End Class