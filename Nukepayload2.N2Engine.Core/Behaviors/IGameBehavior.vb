Imports Nukepayload2.N2Engine.UI.Elements

Namespace Behaviors
    ''' <summary>
    ''' 游戏中可见元素的行为，让可见元素有规定的表现。与触发器不同的是，触发器主要是为了让处理数据的逻辑更有条理。
    ''' </summary>
    Public Interface IGameBehavior
        Sub Attach(visual As GameVisual)
        Sub Remove(visual As GameVisual)
    End Interface
End Namespace