Imports Nukepayload2.N2Engine.UI.Elements

Namespace Behaviors

    Public MustInherit Class BehaviorBase(Of T As GameVisual)
        Implements IGameBehavior(Of T)

        Public Sub OnRemoved(visual As GameVisual) Implements IGameBehavior.OnRemoved
            OnRemoved(DirectCast(visual, T))
        End Sub

        Public Sub OnRemoved(visual As T) Implements IGameBehavior(Of T).OnRemoved
            visual.RemoveBehavior(Me)
        End Sub

        Protected Overridable Sub OnAttached(visual As GameVisual) Implements IGameBehavior.OnAttached
            OnAttached(DirectCast(visual, T))
        End Sub
        Protected Overridable Sub OnAttached(visual As T) Implements IGameBehavior(Of T).OnAttached
            visual.AddBehavior(Me)
        End Sub
    End Class
End Namespace