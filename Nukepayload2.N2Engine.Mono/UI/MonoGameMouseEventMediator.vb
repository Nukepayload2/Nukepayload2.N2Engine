Imports Microsoft.Xna.Framework.Input
Imports Nukepayload2.N2Engine.UI
Imports Nukepayload2.N2Engine.UI.Elements

Public Class MonoGameMouseEventMediator
    Implements IEventMediator
    Implements IMouseEventMediator

    Public Property AttachedView As GameCanvas Implements IMouseEventMediator.AttachedView
    Dim _cursorX, _cursorY As Integer

    Sub New(keyboardEventMediator As MonoGameKeyboardEventMediator)
        Me.KeyboardEventMediator = keyboardEventMediator
        AttachedView = keyboardEventMediator.AttachedView
    End Sub

    Public ReadOnly Property KeyboardEventMediator As MonoGameKeyboardEventMediator

    ' 鼠标按键的状态。第一个是 None，不要使用。
    Dim _mouseKeyStatus(5) As ButtonState

    Public ReadOnly Property MouseKeyStatus As IReadOnlyList(Of Input.ButtonState) Implements IMouseEventMediator.MouseKeyStatus
        Get
            Return _mouseKeyStatus.Select(Function(i) CType(i, Input.ButtonState))
        End Get
    End Property

    ' 鼠标滚轮从游戏启动以来的基类值
    Public ReadOnly Property WheelValue As Integer Implements IMouseEventMediator.WheelValue

    Public Sub TryRaiseEvent() Implements IMouseEventMediator.TryRaiseEvent
        Dim mouseState = Mouse.GetState()
        Dim keyModifiers = KeyboardEventMediator.GetVirtualKeyModifiers()
        ' 鼠标移动
        If _cursorX <> mouseState.X OrElse _cursorY <> mouseState.Y Then
            _cursorX = mouseState.X
            _cursorY = mouseState.Y
            AttachedView.RaiseMouseMove(New GameMouseRoutedEventArgs(keyModifiers, New System.Numerics.Vector2(_cursorX, _cursorY)))
        End If
        ' 鼠标点击
        Dim curKeyStat(5) As ButtonState
        curKeyStat(1) = mouseState.LeftButton
        curKeyStat(2) = mouseState.RightButton
        curKeyStat(3) = mouseState.MiddleButton
        curKeyStat(4) = mouseState.XButton1
        curKeyStat(5) = mouseState.XButton2
        For i = 1 To 5
            Dim curStat = curKeyStat(i)
            If _mouseKeyStatus(i) <> curStat Then
                _mouseKeyStatus(i) = curStat
                Dim args As New GameMouseRoutedEventArgs(keyModifiers, New System.Numerics.Vector2(_cursorX, _cursorY), CType(i, Input.MouseKeys))
                If curStat = ButtonState.Pressed Then
                    AttachedView.RaiseMouseButtonDown(args)
                Else
                    AttachedView.RaiseMouseButtonUp(args)
                End If
            End If
        Next
        ' 鼠标滚轮滚动
        If _WheelValue <> mouseState.ScrollWheelValue Then
            AttachedView.RaiseMouseWheelChanged(New GameMouseRoutedEventArgs(keyModifiers, New System.Numerics.Vector2(_cursorX, _cursorY), mouseState.ScrollWheelValue - _WheelValue))
            _WheelValue = mouseState.ScrollWheelValue
        End If
    End Sub

End Class
