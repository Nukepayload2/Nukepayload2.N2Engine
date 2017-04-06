Imports Nukepayload2.N2Engine.ActionGames.UWP.Designer.ViewModels

Namespace Commands

    Public Class DeleteSelectedTileCommand
        Implements ICommand

        Public Event CanExecuteChanged As EventHandler Implements ICommand.CanExecuteChanged

        Public Sub ExecuteAsync(parameter As Object) Implements ICommand.Execute
            Dim model = StageViewModel.Current.StageData
            If model.Tiles.InRange(model.SelectedTileX, model.SelectedTileY) Then
                model.Tiles(model.SelectedTileX, model.SelectedTileY) = Nothing
            End If
        End Sub

        Public Function CanExecute(parameter As Object) As Boolean Implements ICommand.CanExecute
            Return True
        End Function
    End Class

End Namespace