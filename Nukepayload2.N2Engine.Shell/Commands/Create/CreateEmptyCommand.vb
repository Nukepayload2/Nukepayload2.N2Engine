Imports Nukepayload2.N2Engine.Shell.Services

Namespace Commands
    Public Class CreateEmptyCommand
        Implements ICommand

        Public Event CanExecuteChanged As EventHandler Implements ICommand.CanExecuteChanged

        Public Sub ExecuteAsync(parameter As Object) Implements ICommand.Execute
            NavigationService.Navigate(Pages.DesignerPage)
        End Sub

        Public Function CanExecute(parameter As Object) As Boolean Implements ICommand.CanExecute
            Return True
        End Function
    End Class
End Namespace
