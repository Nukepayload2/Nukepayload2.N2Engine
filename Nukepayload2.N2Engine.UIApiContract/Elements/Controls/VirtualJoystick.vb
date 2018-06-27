Imports Nukepayload2.N2Engine.Foundation
Imports Nukepayload2.N2Engine.UI.Elements

Namespace UI.Controls
    ''' <summary>
    ''' 单点触摸的虚拟摇杆。一个虚拟摇杆操作开始到完成之间会排除其它触摸点对于虚拟摇杆的控制。
    ''' </summary>
    Public Class VirtualJoystick
        Inherits GameContentControl(Of VirtualJoystickContent)

        Dim _startTouchId As UInteger

        ''' <summary>
        ''' 摇杆边框的颜色
        ''' </summary>
        Public ReadOnly Property Stroke As PropertyBinder(Of Color)
            Get
                Return Content.Stroke
            End Get
        End Property

        ''' <summary>
        ''' 摇杆实心部分的颜色
        ''' </summary>
        Public ReadOnly Property Fill As PropertyBinder(Of Color)
            Get
                Return Content.Fill
            End Get
        End Property

        ''' <summary>
        ''' 摇杆边框的大小
        ''' </summary>
        Public ReadOnly Property StrokeSize As PropertyBinder(Of Vector2)
            Get
                Return Content.StrokeSize
            End Get
        End Property

        ''' <summary>
        ''' 摇杆实心部分的大小
        ''' </summary>
        Public ReadOnly Property FillSize As PropertyBinder(Of Vector2)
            Get
                Return Content.FillSize
            End Get
        End Property

        ''' <summary>
        ''' 判断当前触摸点是否在虚拟摇杆的触摸范围内。
        ''' </summary>
        Public Property IsTouchPointInVirtualJoystickRange As Func(Of Vector2, Boolean)

        ''' <summary>
        ''' 虚拟摇杆状态
        ''' </summary>
        Public ReadOnly Property State As VirtualJoystickState

#Region "虚拟摇杆事件"
        ''' <summary>
        ''' 指示虚拟摇杆拖动正在开始。
        ''' </summary>
        Public Event VirtualJoystickDragStarting As GameObjectEventHandler(Of VirtualJoystick, VirtualJoystickDragEventArgs)
        ''' <summary>
        ''' 指示虚拟摇杆拖动。
        ''' </summary>
        Public Event VirtualJoystickDragging As GameObjectEventHandler(Of VirtualJoystick, VirtualJoystickDragEventArgs)
        ''' <summary>
        ''' 虚拟摇杆松开。
        ''' </summary>
        Public Event VirtualJoystickReleased As GameObjectEventHandler(Of VirtualJoystick, VirtualJoystickDragEventArgs)

#End Region

        Sub New(isTouchPointInVirtualJoystickRange As Func(Of Vector2, Boolean))
            MyBase.New((New VirtualJoystickContentTemplate).CreateContent())
            Initialize(isTouchPointInVirtualJoystickRange)
        End Sub

        Private Sub Initialize(isTouchPointInVirtualJoystickRange As Func(Of Vector2, Boolean))
            Me.IsTouchPointInVirtualJoystickRange = isTouchPointInVirtualJoystickRange
        End Sub

        Private Sub VirtualJoystick_TouchDown(sender As GameVisual, e As GameTouchRoutedEventArgs) Handles Me.TouchDown
            If _startTouchId = GameTouchRoutedEventArgs.InvalidTouchId Then
                Dim startPoint = e.Position
                Dim endPoint = startPoint
                Content.StartPoint.Value = startPoint
                Content.EndPoint.Value = endPoint
                If IsTouchPointInVirtualJoystickRange(startPoint) Then
                    _startTouchId = e.PointerId
                    _State = VirtualJoystickState.Drag
                    IsVisible.Value = True
                    RaiseEvent VirtualJoystickDragStarting(Me, New VirtualJoystickDragEventArgs(startPoint, endPoint))
                End If
            End If
        End Sub

        Private Sub VirtualJoystick_TouchMove(sender As GameVisual, e As GameTouchRoutedEventArgs) Handles Me.TouchMove
            If _startTouchId = e.PointerId Then
                Content.EndPoint.Value = e.Position
            End If
        End Sub

        Private Sub VirtualJoystick_TouchUp(sender As GameVisual, e As GameTouchRoutedEventArgs) Handles Me.TouchUp
            If _startTouchId = e.PointerId Then
                Dim startPoint = Content.StartPoint.Value
                Content.EndPoint.Value = startPoint
                _State = VirtualJoystickState.Idle
                IsVisible.Value = False
                _startTouchId = GameTouchRoutedEventArgs.InvalidTouchId
                RaiseEvent VirtualJoystickReleased(Me, New VirtualJoystickDragEventArgs(startPoint, startPoint))
            End If
        End Sub

        Private Sub VirtualJoystick_Updating(sender As GameVisual, e As EventArgs) Handles Me.Updating
            If _startTouchId <> GameTouchRoutedEventArgs.InvalidTouchId Then
                RaiseEvent VirtualJoystickDragging(Me, New VirtualJoystickDragEventArgs(Content.StartPoint.Value, Content.EndPoint.Value))
            End If
        End Sub
    End Class
End Namespace