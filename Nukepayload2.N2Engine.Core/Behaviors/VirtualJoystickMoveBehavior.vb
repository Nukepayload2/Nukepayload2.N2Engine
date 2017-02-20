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

        Public Property MaxSpeed As Single

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

        Private Sub Joystick_VirtualJoystickDragMoved(sender As VirtualJoystick, e As VirtualJoystickDragEventArgs) Handles Joystick.VirtualJoystickDragMoved
            Dim direction = e.EndPoint - e.StartPoint
            direction.LimitMag(MaxSpeed)
            _target.Location.Value += direction
        End Sub
    End Class

End Namespace