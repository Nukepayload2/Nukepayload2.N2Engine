Imports Nukepayload2.N2Engine.Animations
Imports Nukepayload2.N2Engine.Linq

Namespace UI.Elements
    ''' <summary>
    ''' 游戏的画布，是全部可见元素的父级。
    ''' </summary>
    Public Class GameCanvas
        Inherits GameVisualContainer
        ''' <summary>
        ''' (未实施) 默认的场景导航动画表
        ''' </summary>
        Public Property ContentTransitions As IList(Of TransitionAnimation)
        ''' <summary>
        ''' 这个类型不自动创建渲染器
        ''' </summary>
        Protected Overrides Sub CreateRenderer()

        End Sub
        ''' <summary>
        ''' (未实施) 事件路由的方向
        ''' </summary>
        Public Property EventRouteDirection As GameEventRouteDirections
        ''' <summary>
        ''' 事件路由的模式
        ''' </summary>
        Public Property EventRouteMode As GameEventRouteModes = GameEventRouteModes.NonHandledEvents
        ''' <summary>
        ''' (未实施) 在画布大小变更时引发此事件
        ''' </summary>
        Public Event SizeChanged As GameObjectEventHandler(Of GameCanvas, EventArgs)
        ''' <summary>
        ''' 引发画布大小变更事件
        ''' </summary>
        Public Overridable Sub RaiseSizeChanged()
            RaiseEvent SizeChanged(Me, EventArgs.Empty)
        End Sub
        ''' <summary>
        ''' 将指定的事件路由到子节点
        ''' </summary>
        ''' <typeparam name="TEventArgs">事件数据的类型</typeparam>
        ''' <param name="originalEvent">实际引发这个事件的对象</param>
        ''' <param name="raise">引发事件</param>
        Protected Overridable Sub RouteEvents(Of TEventArgs As GameRoutedEventArgs)(originalEvent As TEventArgs, raise As Action(Of GameVisual, TEventArgs))
            originalEvent.OriginalSource = Me
            Dim subTreeNodes = DirectCast(Me, GameVisual).HierarchyWalk(Function(node) node.GetSubNodes).Skip(1)
            Select Case EventRouteMode
                Case GameEventRouteModes.Disabled
                Case GameEventRouteModes.HandledEventsToo
                    For Each vie In subTreeNodes
                        raise(vie, originalEvent)
                    Next
                Case GameEventRouteModes.NonHandledEvents
                    For Each vie In subTreeNodes
                        If originalEvent.Handled Then
                            Exit For
                        Else
                            raise(vie, originalEvent)
                        End If
                    Next
            End Select
        End Sub

        Private Sub GameCanvas_KeyDown(sender As GameVisual, e As GameKeyboardRoutedEventArgs) Handles Me.KeyDown
            RouteEvents(e, Sub(visual, evt) visual.RaiseKeyDown(evt))
        End Sub

        Private Sub GameCanvas_KeyUp(sender As GameVisual, e As GameKeyboardRoutedEventArgs) Handles Me.KeyUp
            RouteEvents(e, Sub(visual, evt) visual.RaiseKeyUp(evt))
        End Sub

        Private Sub GameCanvas_MouseButtonDown(sender As GameVisual, e As GameMouseRoutedEventArgs) Handles Me.MouseButtonDown
            RouteEvents(e, Sub(visual, evt) visual.RaiseMouseButtonDown(evt))
        End Sub

        Private Sub GameCanvas_MouseButtonUp(sender As GameVisual, e As GameMouseRoutedEventArgs) Handles Me.MouseButtonUp
            RouteEvents(e, Sub(visual, evt) visual.RaiseMouseButtonUp(evt))
        End Sub

        Private Sub GameCanvas_MouseMove(sender As GameVisual, e As GameMouseRoutedEventArgs) Handles Me.MouseMove
            RouteEvents(e, Sub(visual, evt) visual.RaiseMouseMove(evt))
        End Sub

        Private Sub GameCanvas_MouseWheelChanged(sender As GameVisual, e As GameMouseRoutedEventArgs) Handles Me.MouseWheelChanged
            RouteEvents(e, Sub(visual, evt) visual.RaiseMouseWheelChanged(evt))
        End Sub

        Private Sub GameCanvas_TouchDown(sender As GameVisual, e As GameTouchRoutedEventArgs) Handles Me.TouchDown
            RouteEvents(e, Sub(visual, evt) visual.RaiseTouchDown(evt))
        End Sub

        Private Sub GameCanvas_TouchMove(sender As GameVisual, e As GameTouchRoutedEventArgs) Handles Me.TouchMove
            RouteEvents(e, Sub(visual, evt) visual.RaiseTouchMove(evt))
        End Sub

        Private Sub GameCanvas_TouchUp(sender As GameVisual, e As GameTouchRoutedEventArgs) Handles Me.TouchUp
            RouteEvents(e, Sub(visual, evt) visual.RaiseTouchUp(evt))
        End Sub
    End Class
End Namespace