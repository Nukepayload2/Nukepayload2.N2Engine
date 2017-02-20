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

        WithEvents Target As GameVisual

        Public Property MaxSpeed As Single

        Public Property SpeedMultiple As Single = 1.0F

        Sub New(joystick As VirtualJoystick)
            Me.Joystick = joystick
        End Sub

        Public Overrides Sub Attach(visual As GameVisual)
            If Target IsNot Nothing Then
                Remove(Target)
            End If
            Target = visual
            MyBase.Attach(visual)
        End Sub

        Public Overrides Sub Remove(visual As GameVisual)
            Target = Nothing
            MyBase.Remove(visual)
        End Sub

        Private Sub Joystick_VirtualJoystickDragMoved(sender As VirtualJoystick, e As VirtualJoystickDragEventArgs) Handles Joystick.VirtualJoystickDragging
            Dim direction = (e.EndPoint - e.StartPoint) * SpeedMultiple
            direction.LimitMag(MaxSpeed)
            Target.Location.Value += direction
        End Sub
    End Class

End Namespace