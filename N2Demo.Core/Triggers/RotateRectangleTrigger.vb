Imports System.Numerics
Imports Nukepayload2.N2Engine.Input
Imports Nukepayload2.N2Engine.Triggers
Imports Nukepayload2.N2Engine.UI.Elements

Public Class RotateRectangleTrigger
    Inherits GameEventTrigger(Of GameVisual, GameKeyboardRoutedEventArgs)

    Sub New()
        MyBase.New(NameOf(GameVisual.KeyDown))
    End Sub

    WithEvents Target As GameVisual

    Dim rotate As Single
    Dim skew As Vector2
    Dim scale As Vector2

    Public Overrides Sub Action(sender As GameVisual, e As GameKeyboardRoutedEventArgs)
        If Target Is Nothing Then
            Target = sender
        End If
        Dim vm = SparksViewModel.Current
        Dim shakeValue As Single = 0.0F
        Select Case e.KeyCode
            Case Key.Q
                rotate += 0.01F
            Case Key.E
                rotate -= 0.01F
            Case Key.Number1, Key.NumberPad1
                skew += New Vector2(0.01F, 0F)
            Case Key.Number2, Key.NumberPad2
                skew += New Vector2(-0.01F, 0F)
            Case Key.Number3, Key.NumberPad3
                skew += New Vector2(0F, 0.01F)
            Case Key.Number4, Key.NumberPad4
                skew += New Vector2(0F, -0.01F)
            Case Key.Number5, Key.NumberPad5
                scale += New Vector2(0.01F, 0F)
            Case Key.Number6, Key.NumberPad6
                scale += New Vector2(-0.01F, 0F)
            Case Key.Number7, Key.NumberPad7
                scale += New Vector2(0F, 0.01F)
            Case Key.Number8, Key.NumberPad8
                scale += New Vector2(0F, -0.01F)
        End Select
        vm.ShakingViewer.ShakeY = shakeValue
    End Sub

    ' TODO: 使用动画代替订阅 Updating 事件的实现。
    Private Sub Target_Updating(sender As GameVisual, e As EventArgs) Handles Target.Updating
        Dim vm = SparksViewModel.Current
        vm.GreenRectangle.Rotate += rotate
        vm.GreenRectangle.Skew += skew
        vm.GreenRectangle.Scale += scale
    End Sub
End Class
