Imports Nukepayload2.N2Engine.UI.Elements

Namespace Triggers
    ''' <summary>
    ''' 对全部可见游戏对象通用的触发器
    ''' </summary>
    Public Interface IGameTrigger
        Sub Attach(visual As GameVisual)
        Sub Detach(visual As GameVisual)
    End Interface
End Namespace