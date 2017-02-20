Imports Nukepayload2.N2Engine.UI.Elements

Namespace Behaviors

    Public Class GameBehavior
        Implements IGameBehavior

        Public Overridable Sub Attach(visual As GameVisual) Implements IGameBehavior.Attach
            visual.AddBehavior(Me)
        End Sub

        Public Overridable Sub Remove(visual As GameVisual) Implements IGameBehavior.Remove
            visual.RemoveBehavior(Me)
        End Sub
    End Class

End Namespace