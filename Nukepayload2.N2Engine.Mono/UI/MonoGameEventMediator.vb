Imports Microsoft.Xna.Framework.Input
Imports Nukepayload2.N2Engine.Input
Imports Nukepayload2.N2Engine.UI.Elements

Public Class MonoGameEventMediator
    Sub New(attachedView As GameCanvas)
        Me.AttachedView = attachedView
        For i = 0 To 255
            _keyStates(i).ScanCode = i
        Next
        _keys = Aggregate v As Integer In [Enum].GetValues(GetType(Keys)) Select v Into ToArray
    End Sub

    Public Property AttachedView As GameCanvas

    Dim _keyStates(255) As PhysicalKeyStatus
    Dim _keys As Integer()

    Public Sub TryRaiseEvent()
        Dim keyboardState = Keyboard.GetState
        For Each k In _keys
            Dim pressed = keyboardState.IsKeyDown(k)
            Dim stat = _keyStates(k)
            If pressed Then
                ' 现在是按下状态
                If stat.RepeatCount = 0 Then
                    ' 松开 -> 按下
                    stat.RepeatCount = 1
                    stat.WasKeyDown = False
                    stat.IsKeyReleased = False
                    AttachedView.RaiseKeyDown(New GameKeyboardEventArgs(k, stat))
                Else
                    ' 按下 -> 按下
                    stat.WasKeyDown = True
                End If
            Else
                ' 现在是松开状态
                If stat.WasKeyDown AndAlso stat.IsKeyReleased Then
                    ' (按下 -> 松开) -> 松开
                    stat.RepeatCount = 0
                    stat.WasKeyDown = False
                    stat.IsKeyReleased = False
                ElseIf stat.RepeatCount = 1 Then
                    ' 按下 -> 松开
                    stat.WasKeyDown = True
                    stat.IsKeyReleased = True
                    AttachedView.RaiseKeyUp(New GameKeyboardEventArgs(k, stat))
                Else
                    ' (松开 -> 松开) -> 松开
                End If
            End If
            _keyStates(k) = stat
        Next
    End Sub
End Class
