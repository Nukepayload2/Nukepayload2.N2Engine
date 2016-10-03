Imports Nukepayload2.N2Engine.UI.Elements

Namespace Behaviors
    Public Interface IGameBehavior
        Sub OnAttached(visual As GameVisual)
        Sub OnRemoved(visual As GameVisual)
    End Interface
    Public Interface IGameBehavior(Of In T As GameVisual)
        Inherits IGameBehavior
        Overloads Sub OnAttached(visual As T)
        Overloads Sub OnRemoved(visual As T)
    End Interface
End Namespace