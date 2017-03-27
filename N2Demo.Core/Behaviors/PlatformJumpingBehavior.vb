Imports Nukepayload2.N2Engine.Behaviors
Imports Nukepayload2.N2Engine.Input
Imports Nukepayload2.N2Engine.UI.Controls
Imports Nukepayload2.N2Engine.UI.Elements

Public Class PlatformJumpingBehavior
    Inherits GameBehavior

    WithEvents Chara As GameEntity
    WithEvents JumpTouchButton As GameButton
    WithEvents Joystick As VirtualJoystick

    Sub New(jumpTouchButton As GameButton, joystick As VirtualJoystick)
        Me.JumpTouchButton = jumpTouchButton
        Me.Joystick = joystick
    End Sub
    ''' <summary>
    ''' 附加到 <see cref="GameEntity"/> 上。
    ''' </summary>
    Public Overloads Sub Attach(entity As GameEntity)
        Chara = entity
        MyBase.Attach(entity)
    End Sub
    ''' <summary>
    ''' 附加到 <see cref="GameEntity"/> 上。
    ''' </summary>
    Public Overrides Sub Attach(visual As GameVisual)
        Chara = DirectCast(visual, GameEntity)
        MyBase.Attach(visual)
    End Sub

    Public Overrides Sub Remove(visual As GameVisual)
        Chara = Nothing
        MyBase.Remove(visual)
    End Sub

    Private Sub Chara_Updating(sender As GameVisual, e As UpdatingEventArgs) Handles Chara.Updating
        Dim keyboard = KeyboardStateManager.PrimaryKeyboard
        Dim vel = Chara.Body.LinearVelocity
        If keyboard.IsKeyDown(Key.A) Then
            vel.X = -2
        ElseIf keyboard.IsKeyDown(Key.D) Then
            vel.X = 2
        End If
        Chara.Body.LinearVelocity = vel
    End Sub

    Private Sub Chara_KeyDown(sender As GameVisual, e As GameKeyboardRoutedEventArgs) Handles Chara.KeyDown
        If e.KeyCode = Key.W Then
            Jump()
        End If
    End Sub

    Private Sub Jump()
        Dim vel = Chara.Body.LinearVelocity
        vel.Y -= 5.0F
        Chara.Body.LinearVelocity = vel
    End Sub

    Private Sub JumpTouchButton_Click(sender As GameButton, e As EventArgs) Handles JumpTouchButton.Click
        Jump()
    End Sub

    Private Sub Joystick_VirtualJoystickDragging(sender As VirtualJoystick, e As VirtualJoystickDragEventArgs) Handles Joystick.VirtualJoystickDragging
        Dim vel = Chara.Body.LinearVelocity
        Dim direction = e.EndPoint - e.StartPoint
        If direction.X < -1 Then
            vel.X = -2
        ElseIf direction.X > 1 Then
            vel.X = 2
        End If
        Chara.Body.LinearVelocity = vel
    End Sub
End Class
