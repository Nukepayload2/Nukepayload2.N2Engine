Namespace Commands

    Public Class RemoveSpriteCommand
        Implements ICommand

        Public Event CanExecuteChanged As EventHandler Implements ICommand.CanExecuteChanged

        Public Sub ExecuteAsync(parameter As Object) Implements ICommand.Execute
            Dim vm = ViewModels.StageViewModel.Current
            vm.SpriteSheets.Remove(vm.SelectedSpriteSheet)
            If vm.SelectedSpriteSheet Is vm.PrimarySpriteSheet Then
                vm.PrimarySpriteSheet = Nothing
            End If
            vm.SelectedSpriteSheet = Nothing
        End Sub

        Public Function CanExecute(parameter As Object) As Boolean Implements ICommand.CanExecute
            Return True
        End Function
    End Class

End Namespace