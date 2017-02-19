﻿Imports Nukepayload2.N2Engine.Foundation
Imports Nukepayload2.N2Engine.UI.Elements

Namespace UI.Controls
    ''' <summary>
    ''' 单点触摸的虚拟摇杆。一个虚拟摇杆操作开始到完成之间会排除其它触摸点对于虚拟摇杆的控制。
    ''' </summary>
    Public Class VirtualJoystick
        Inherits GameContentControl(Of VirtualJoystickContentTemplate)

        Dim _startPoint, _endPoint As Vector2
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

        Sub New(isTouchPointInVirtualJoystickRange As Func(Of Vector2, Boolean))
            MyBase.New(New VirtualJoystickContentTemplate)
            With Content
                .StartPoint.Bind(Function() _startPoint)
                .EndPoint.Bind(Function() _endPoint)
                .IsVisible.Bind(Function() State = VirtualJoystickState.Drag)
            End With
            isTouchPointInVirtualJoystickRange = Function() True
        End Sub

        Private Sub VirtualJoystick_TouchDown(sender As GameVisual, e As GameTouchRoutedEventArgs) Handles Me.TouchDown
            If _startTouchId = GameTouchRoutedEventArgs.InvalidTouchId Then
                _startPoint = e.Position
                _endPoint = _startPoint
                If IsTouchPointInVirtualJoystickRange(_startPoint) Then
                    _startTouchId = e.PointerId
                    _State = VirtualJoystickState.Drag
                End If
            End If
        End Sub

        Private Sub VirtualJoystick_TouchMove(sender As GameVisual, e As GameTouchRoutedEventArgs) Handles Me.TouchMove
            If _startTouchId = e.PointerId Then
                _endPoint = e.Position
            End If
        End Sub

        Private Sub VirtualJoystick_TouchUp(sender As GameVisual, e As GameTouchRoutedEventArgs) Handles Me.TouchUp
            If _startTouchId = e.PointerId Then
                _endPoint = _startPoint
                _State = VirtualJoystickState.Idle
                _startTouchId = GameTouchRoutedEventArgs.InvalidTouchId
            End If
        End Sub
    End Class
End Namespace