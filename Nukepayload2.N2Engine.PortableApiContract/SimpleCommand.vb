Namespace Input
    Public Class SimpleCommand
        Implements IGameCommand

        Sub New(exec As Action)
            Me.Exec = exec
        End Sub

        Public ReadOnly Property Exec As Action

        Public Event CanExecuteChanged As EventHandler Implements IGameCommand.CanExecuteChanged

        Public Sub Execute() Implements IGameCommand.Execute
            Exec.Invoke
        End Sub

        Public Function CanExecute() As Boolean Implements IGameCommand.CanExecute
            Return Exec IsNot Nothing
        End Function

    End Class
End Namespace