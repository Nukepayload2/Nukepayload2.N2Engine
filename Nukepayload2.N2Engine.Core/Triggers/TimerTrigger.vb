Imports Nukepayload2.N2Engine.UI
Imports Nukepayload2.N2Engine.UI.Elements

Namespace Triggers
    Public MustInherit Class TimerTrigger(Of T As GameVisual)
        Implements ITimeAction, IGameTrigger

        Public Property BeginTime As TimeSpan Implements ITimeAction.BeginTime
        ''' <summary>
        ''' 延迟时间之后执行的命令。这可能在另一个线程执行。
        ''' </summary>
        Public MustOverride Sub Action() Implements ITimeAction.Action

        Public Sub Attach(visual As GameVisual) Implements IGameTrigger.Attach
            visual.AddTrigger(Me)
        End Sub
        ''' <summary>
        ''' 在 BeginTime 之后会执行 Action。
        ''' </summary>
        Public Async Sub BeginAsync() Implements ITimeAction.BeginAsync
            Await Task.Delay(BeginTime)
            Action()
        End Sub

        Public Sub Detach(visual As GameVisual) Implements IGameTrigger.Detach
            visual.AddTrigger(Me)
        End Sub

    End Class

End Namespace
