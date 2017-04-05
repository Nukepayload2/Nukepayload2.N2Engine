Imports Nukepayload2.N2Engine.ActionGames.UWP.Designer.ViewModels

Namespace Commands
    ''' <summary>
    ''' 将选中的图块表设置为主要图块表
    ''' </summary>
    Public Class SetPrimarySpriteSheetCommand
        Implements ICommand

        Public Event CanExecuteChanged As EventHandler Implements ICommand.CanExecuteChanged

        Public Sub ExecuteAsync(parameter As Object) Implements ICommand.Execute
            Dim vm = StageViewModel.Current
            vm.PrimarySpriteSheet = vm.SelectedSpriteSheet
        End Sub

        Public Function CanExecute(parameter As Object) As Boolean Implements ICommand.CanExecute
            Return True
        End Function
    End Class

End Namespace