Public MustInherit Class GameTriggerBase(Of T As GameVisual)
    Implements IGameTrigger(Of T)
    ''' <summary>
    ''' 附加在任何可见元素上。默认行为是强制调用 <see cref="Attach(T)"/> 
    ''' </summary>
    Public Overridable Sub Attach(visual As GameVisual) Implements IGameTrigger.Attach
        Attach(DirectCast(visual, T))
    End Sub

    Public MustOverride Sub Attach(visual As T) Implements IGameTrigger(Of T).Attach
    ''' <summary>
    ''' 任何可见元素上解除附加。默认行为是强制调用 <see cref="Detach(T)"/> 
    ''' </summary>
    Public Overridable Sub Detach(visual As GameVisual) Implements IGameTrigger.Detach
        Detach(DirectCast(visual, T))
    End Sub

    Public MustOverride Sub Detach(visual As T) Implements IGameTrigger(Of T).Detach
End Class