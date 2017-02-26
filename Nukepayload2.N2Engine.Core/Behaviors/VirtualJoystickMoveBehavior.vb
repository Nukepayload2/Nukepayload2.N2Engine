Imports Nukepayload2.N2Engine.N2Math
Imports Nukepayload2.N2Engine.UI.Controls
Imports Nukepayload2.N2Engine.UI.Elements

Namespace Behaviors
    ''' <summary>
    ''' 随着摇杆自由移动
    ''' </summary>
    Public Class VirtualJoystickMoveBehavior
        Inherits GameBehavior

        WithEvents Joystick As VirtualJoystick

        Dim _target As GameVisual
        ''' <summary>
        ''' 最大速度。默认是 5。
        ''' </summary>
        Public Property MaxSpeed As Single = 5.0F
        ''' <summary>
        ''' 移动一个像素增加多少速度。默认是 0.1。
        ''' </summary>
        ''' <returns></returns>
        Public Property SpeedMultiple As Single = 0.1F

        Sub New(joystick As VirtualJoystick)
            Me.Joystick = joystick
        End Sub

        Public Overrides Sub Attach(visual As GameVisual)
            If _target IsNot Nothing Then
                Remove(_target)
            End If
            _target = visual
            MyBase.Attach(visual)
        End Sub

        Public Overrides Sub Remove(visual As GameVisual)
            _target = Nothing
            MyBase.Remove(visual)
        End Sub

        Private Sub Joystick_VirtualJoystickDragging(sender As VirtualJoystick, e As VirtualJoystickDragEventArgs) Handles Joystick.VirtualJoystickDragging
            Dim direction = (e.EndPoint - e.StartPoint) * SpeedMultiple
            direction.LimitLength(MaxSpeed)
            _target.Location.Value += direction
        End Sub
    End Class

End Namespace