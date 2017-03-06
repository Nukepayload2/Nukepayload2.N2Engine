Imports Nukepayload2.N2Engine.Shell.Models
Imports Nukepayload2.N2Engine.Shell.Services
Imports Nukepayload2.N2Engine.Shell.Utilities

Namespace Commands
    Public Class ProjectOpenCommand
        Implements ICommand

        WithEvents Model As N2EngineProject

        Public Event CanExecuteChanged As EventHandler Implements ICommand.CanExecuteChanged

        Public Async Sub ExecuteAsync(parameter As Object) Implements ICommand.Execute
            Try
                N2EngineProject.ActiveProject = Await N2EngineProjectIO.OpenAsync()
                NavigationService.Navigate(Pages.DesignerPage)
            Catch ex As Exception
                If Model IsNot Nothing Then
                    Model.ErrorMessage = ex.Message
                End If
            End Try
        End Sub

        Private Sub Model_PropertyChanged(sender As Object, e As PropertyChangedEventArgs) Handles Model.PropertyChanged
            If e.PropertyName = NameOf(Model.IsBusy) Then
                RaiseEvent CanExecuteChanged(Me, e)
            End If
        End Sub

        Public Function CanExecute(parameter As Object) As Boolean Implements ICommand.CanExecute
            Model = N2EngineProject.ActiveProject
            Return Model Is Nothing OrElse Not Model.IsBusy
        End Function
    End Class
End Namespace
