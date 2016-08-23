
Namespace Battle
    Public MustInherit Class BasicMarkAbility
        Inherits EffectHookBase
        Implements IMarkAbility
        Sub New()

        End Sub
        Public Property Owner As IMark Implements IMarkAbility.Owner
        Sub New(owner As IMark)
            owner = owner
        End Sub
        Public MustOverride Async Function Decrement(Player As IPlayer) As Task Implements IMarkAbility.Decrement

        Public MustOverride Async Function Increment(Player As IPlayer) As Task Implements IMarkAbility.Increment

    End Class
End Namespace