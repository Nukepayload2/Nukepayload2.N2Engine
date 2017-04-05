Imports Nukepayload2.N2Engine.ActionGames.UWP.Designer.ViewModels

Namespace Commands

    Public Class ExcludedSpriteCommand
        Implements ICommand

        Public Event CanExecuteChanged As EventHandler Implements ICommand.CanExecuteChanged

        Public Sub ExecuteAsync(parameter As Object) Implements ICommand.Execute
            Dim vm = SpritesViewModel.Current
            If vm.SelectedSprite IsNot Nothing Then
                vm.Sprites.Remove(vm.SelectedSprite)
            End If
        End Sub

        Public Function CanExecute(parameter As Object) As Boolean Implements ICommand.CanExecute
            Return True
        End Function
    End Class

End Namespace