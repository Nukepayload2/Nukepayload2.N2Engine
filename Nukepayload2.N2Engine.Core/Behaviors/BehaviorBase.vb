Imports Nukepayload2.N2Engine.UI.Elements

Namespace Behaviors

    Public MustInherit Class BehaviorBase(Of T As GameVisual)
        Protected MustOverride Sub OnAttached(visual As T)
    End Class
End Namespace