Imports Nukepayload2.N2Engine.Input
Imports Nukepayload2.N2Engine.Triggers
Imports Nukepayload2.N2Engine.UI.Elements

Public Class VerticalShakeTrigger
    Inherits GameEventTrigger(Of GameVisual, GameKeyboardRoutedEventArgs)

    Sub New()
        MyBase.New(NameOf(GameVisual.KeyDown))
    End Sub

    Public Overrides Sub Action(sender As GameVisual, e As GameKeyboardRoutedEventArgs)
        Dim vm = SparksViewModel.Current
        Dim shakeValue As Single = 0.0F
        Select Case e.KeyCode
            Case Key.Space
                shakeValue = 100
            Case Key.W, Key.A, Key.S, Key.D
                shakeValue = 25
        End Select
        vm.ShakingViewer.ShakeY = shakeValue
    End Sub
End Class
