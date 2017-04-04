Namespace Commands

    Public Class ImportSpriteCommand
        Implements ICommand

        Public Event CanExecuteChanged As EventHandler Implements ICommand.CanExecuteChanged

        Public Sub Execute(parameter As Object) Implements ICommand.Execute
            Throw New NotImplementedException()
        End Sub

        Public Function CanExecute(parameter As Object) As Boolean Implements ICommand.CanExecute
            Throw New NotImplementedException()
        End Function
    End Class

End Namespace