Imports Nukepayload2.N2Engine.UI.Elements

Namespace Triggers
    ''' <summary>
    ''' 对全部可见物体通用的触发器
    ''' </summary>
    Public Interface IGameTrigger
        Sub Attach(visual As GameVisual)
        Sub Detach(visual As GameVisual)
    End Interface
    ''' <summary>
    ''' 对一类可见物体区分对待的触发器。这是应该广泛使用的。
    ''' </summary>
    Public Interface IGameTrigger(Of T As GameVisual)
        Inherits IGameTrigger
        Overloads Sub Attach(visual As T)
        Overloads Sub Detach(visual As T)
    End Interface
End Namespace