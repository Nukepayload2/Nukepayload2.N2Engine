Namespace Battle
    Module CommonEvent
        Event PlayerOut(PlayerOrderId As Integer)
        Public Sub RaisePlayerOut(Player As IPlayer)
            RaiseEvent PlayerOut(Player.OrderId)
        End Sub
    End Module

    Public Class PlayerDeathJudgementEventArgs
        Inherits EventArgs
        Public ReadOnly NeedCure As Boolean, UnableToSave As Boolean
        Sub New(Optional NeedCure As Boolean = False, Optional UnableToSave As Boolean = False)
            Me.NeedCure = NeedCure
            Me.UnableToSave = UnableToSave
        End Sub
    End Class
    Public Class CardResponseEventArgs
        Inherits EventArgs
        Public Process, IsHumanPlayer, Timeout As Boolean
        Sub New(_Process As Boolean, _IsHumanPlayer As Boolean, Optional _Timeout As Boolean = False)
            Process = _Process
            IsHumanPlayer = _IsHumanPlayer
            Timeout = _Timeout
        End Sub
    End Class
End Namespace
