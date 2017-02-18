Imports System.Threading
Imports Nukepayload2.N2Engine.UI
Imports Nukepayload2.N2Engine.UI.Elements

Namespace Triggers
    ''' <summary>
    ''' 在指定的时间后执行某个动作。附加到目标对象后开始计时。
    ''' </summary>
    Public MustInherit Class TimerTrigger
        Implements IGameTrigger

        Sub New(timeAction As TimeAction)
            Me.TimeAction = timeAction
        End Sub

        Public ReadOnly Property TimeAction As TimeAction

        Public Sub Attach(visual As GameVisual) Implements IGameTrigger.Attach
            visual.AddTrigger(Me)
            TimeAction.BeginAsync()
        End Sub

        Public Sub Detach(visual As GameVisual) Implements IGameTrigger.Detach
            TimeAction.Cancel()
            visual.RemoveTrigger(Me)
        End Sub

    End Class

End Namespace
