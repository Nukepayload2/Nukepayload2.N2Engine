
Imports Nukepayload2.N2Engine.UI
Imports Nukepayload2.N2Engine.UI.Elements

Namespace Triggers
    Public MustInherit Class TimerTrigger(Of T As GameVisual)
        Inherits GameTriggerBase(Of T)
        Implements ITimeAction

        Public Property BeginTime As TimeSpan Implements ITimeAction.BeginTime

        Public Overrides Sub Attach(visual As T)
            MyBase.Attach(visual)
        End Sub

        Public MustOverride Sub Begin() Implements ITimeAction.Begin

        Public Overrides Sub Detach(visual As T)
            MyBase.Detach(visual)
        End Sub
    End Class

End Namespace
