

Namespace Battle
    Public MustInherit Class EffectHookBase
        Implements IEffectHook

        Public Overridable ReadOnly Property Name As String Implements IEffectHook.Name
            Get
                Return Me.GetType.Name
            End Get
        End Property

        Public Property IsEnabled As Boolean = True Implements IEffectHook.IsEnabled

        Public MustOverride ReadOnly Property Description As String Implements IEffectHook.Description

        Public Overridable ReadOnly Property Resources As IEnumerable(Of KeyValuePair(Of String, String)) Implements IEffectHook.Resources
            Get
                Return {}
            End Get
        End Property

        Public Overridable ReadOnly Property InlineResources As IEnumerable(Of KeyValuePair(Of String, Stream)) Implements IEffectHook.InlineResources
            Get
                Return {}
            End Get
        End Property

        Public Overridable Async Function ResponseCardEndedAsync(Target As IPlayer, Source As IPlayer, Card As IHandCard, SuccessResponse As Boolean) As Task(Of Boolean) Implements IEffectHook.ResponseCardEndedAsync
            Return Await ResTrueTask()
        End Function

        Public Overridable Async Function RoundIncrementAsync() As Task Implements IEffectHook.RoundIncrementAsync
            Await DoNothingTask()
        End Function

        Public Overridable Async Function RoundStartingAsync(AttachedPlayer As IPlayer) As Task Implements IEffectHook.RoundStartingAsync
            Await DoNothingTask()
        End Function

        Public Overridable Async Function PlayerDefeatedAsync(Player As IPlayer, Defeated方 As IPlayer) As Task Implements IEffectHook.PlayerDefeatedAsync
            Await DoNothingTask()
        End Function

        Public Overridable Async Function RoundEndingAsync(AttachedPlayer As IPlayer) As Task Implements IEffectHook.RoundEndingAsync
            Await DoNothingTask()
        End Function

        Public Overridable Async Function BeforeLeadAsync(Player As IPlayer) As Task(Of Boolean) Implements IEffectHook.BeforeLeadAsync
            Return Await ResTrueTask()
        End Function

        Public Overridable Async Function WillDisplayHandCardAsync(Player As IPlayer, DisplaySuccess As StrongBox(Of Boolean)) As Task(Of Boolean) Implements IEffectHook.WillDisplayHandCardAsync
            Return Await ResTrueTask()
        End Function

        Public Overridable Async Function DyingAsync(Player As IPlayer) As Task(Of Boolean) Implements IEffectHook.DyingAsync
            Return Await ResTrueTask()
        End Function

        Public Overridable Async Function ResponseCardAsync(Target As IPlayer, Source As IPlayer, Card As IHandCard) As Task(Of Boolean) Implements IEffectHook.ResponseCardAsync
            Return Await ResTrueTask()
        End Function

        Public Overridable Async Function HandCardDecrementAsync(Player As IPlayer, Card As IHandCard) As Task(Of Boolean) Implements IEffectHook.HandCardDecrementAsync
            Return Await ResTrueTask()
        End Function

        Public Overridable Async Function BeforeSendCardsAsync(Player As IPlayer) As Task(Of Boolean) Implements IEffectHook.BeforeSendCardsAsync
            Return Await ResTrueTask()
        End Function

        Public Overridable Async Function HandCardIncrementAsync(Player As IPlayer, Card As IHandCard) As Task(Of Boolean) Implements IEffectHook.HandCardIncrementAsync
            Return Await ResTrueTask()
        End Function

        Public Overridable Async Function UseCardAsync(Target As IPlayer, Source As IPlayer, Card As IHandCard) As Task(Of Boolean) Implements IEffectHook.UseCardAsync
            Return Await ResTrueTask()
        End Function

        Public Overridable Async Function MarkLosingAsync(Player As IPlayer, Source As IPlayer, 变的Mark As IEnumerable(Of IMark)) As Task(Of Boolean) Implements IEffectHook.MarkLosingAsync
            Return Await ResTrueTask()
        End Function

        Public Overridable Async Function CallHelpAsync(Player As IPlayer) As Task(Of Boolean) Implements IEffectHook.CallHelpAsync
            Return Await ResTrueTask()
        End Function

        Public Overridable Async Function HealthUpperBoundChangingAsync(Player As IPlayer, Source As IPlayer, 新的Health As Integer) As Task(Of Boolean) Implements IEffectHook.HealthUpperBoundChangingAsync
            Return Await ResTrueTask()
        End Function

        Public Overridable Async Function HealthChangingAsync(Player As IPlayer, Source As IPlayer, 新的Health As Integer) As Task(Of Boolean) Implements IEffectHook.HealthChangingAsync
            Return Await ResTrueTask()
        End Function

        Public Overridable Async Function PowerChangingAsync(Player As IPlayer, Source As IPlayer, 新的PowerValue值 As Integer) As Task(Of Boolean) Implements IEffectHook.PowerChangingAsync
            Return Await ResTrueTask()
        End Function

        Public Overridable Async Function BeforeDiscardAsync(Player As IPlayer) As Task(Of Boolean) Implements IEffectHook.BeforeDiscardAsync
            Return Await ResTrueTask()
        End Function

        Public Overridable Async Function RoundEndedAsync(Player As IPlayer) As Task(Of Boolean) Implements IEffectHook.RoundEndedAsync
            Return Await ResTrueTask()
        End Function

        Public Overridable Async Function MarkAddedAsync(Player As IPlayer, Source As IPlayer, 变的Mark As IEnumerable(Of IMark)) As Task(Of Boolean) Implements IEffectHook.MarkAddedAsync
            Return Await ResTrueTask()
        End Function
    End Class

    Public MustInherit Class MilitaryOfficerAbilityHookBase
        Inherits EffectHookBase
        Implements IMilitaryOfficerAbility

        Public MustOverride ReadOnly Property IsCampAbility As Boolean Implements IMilitaryOfficerAbility.IsCampAbility

    End Class

End Namespace
