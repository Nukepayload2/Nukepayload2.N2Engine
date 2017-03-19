Imports Microsoft.Xna.Framework.Input
Imports Nukepayload2.N2Engine.Input
Imports Nukepayload2.N2Engine.UI
Imports Nukepayload2.N2Engine.UI.Elements

Public Class MonoGameKeyboardEventMediator
    Implements IKeyboardEventMediator

    ReadOnly _keyStates As PhysicalKeyStatus() = KeyboardStateManager.PrimaryKeyboard.WritableKeyState

    Sub New(attachedView As GameCanvas)
        Me.AttachedView = attachedView
        For i = 0 To 255
            _keyStates(i).ScanCode = i
        Next
        _keys = Aggregate v As Integer In [Enum].GetValues(GetType(Keys)) Select v Into ToArray
    End Sub

    Dim _keys As Integer()

    Public Property AttachedView As GameCanvas Implements IKeyboardEventMediator.AttachedView

    ''' <summary>
    ''' 查询当前全部可能的按键的状态
    ''' </summary>
    Public Function GetKeyStates() As IReadOnlyList(Of PhysicalKeyStatus) Implements IKeyboardEventMediator.GetKeyStates
        Return _keyStates
    End Function
    ''' <summary>
    ''' 查询当前按下了哪些虚拟键
    ''' </summary>
    Public Function GetVirtualKeyModifiers() As VirtualKeyModifiers Implements IKeyboardEventMediator.GetVirtualKeyModifiers
        Dim keyModifiers As VirtualKeyModifiers
        If _keyStates(Key.LeftControl).RepeatCount = 1 OrElse _keyStates(Key.RightControl).RepeatCount = 1 Then
            keyModifiers = keyModifiers Or VirtualKeyModifiers.Control
        End If
        If _keyStates(Key.LeftMenu).RepeatCount = 1 OrElse _keyStates(Key.RightMenu).RepeatCount = 1 Then
            keyModifiers = keyModifiers Or VirtualKeyModifiers.Menu
        End If
        If _keyStates(Key.LeftShift).RepeatCount = 1 OrElse _keyStates(Key.RightShift).RepeatCount = 1 Then
            keyModifiers = keyModifiers Or VirtualKeyModifiers.Shift
        End If
        If _keyStates(Key.LeftWindows).RepeatCount = 1 OrElse _keyStates(Key.RightWindows).RepeatCount = 1 Then
            keyModifiers = keyModifiers Or VirtualKeyModifiers.Windows
        End If
        Return keyModifiers
    End Function

    Public Sub TryRaiseEvent() Implements IKeyboardEventMediator.TryRaiseEvent
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
                    AttachedView.RaiseKeyDown(New GameKeyboardRoutedEventArgs(k, stat))
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
                    AttachedView.RaiseKeyUp(New GameKeyboardRoutedEventArgs(k, stat))
                Else
                    ' (松开 -> 松开) -> 松开
                End If
            End If
            _keyStates(k) = stat
        Next
    End Sub
End Class
