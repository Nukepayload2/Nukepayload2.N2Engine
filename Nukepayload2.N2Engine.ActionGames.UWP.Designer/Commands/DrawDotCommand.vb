Imports Nukepayload2.N2Engine.ActionGames.UWP.Designer.ViewModels

Namespace Commands

    Public Class DrawDotCommand
        Implements ICommand

        Public Event CanExecuteChanged As EventHandler Implements ICommand.CanExecuteChanged

        Public Sub Execute(parameter As Object) Implements ICommand.Execute
            Dim vm = StageViewModel.Current
            Dim sprites = SpritesViewModel.Current
            Dim spriteProp = SpriteSheetsViewModel.Current
            Dim stageData = vm.StageData
            If stageData.Tiles.InRange(stageData.SelectedTileX, stageData.SelectedTileY) Then
                Dim newTile As New Models.EditableTile With {
                    .Sprite = vm.SelectedSpriteSheet.SelectedTileSprite,
                    .Collider = spriteProp.DefaultCollider
                }
                stageData.Tiles(stageData.SelectedTileX, stageData.SelectedTileY) = newTile
            End If
        End Sub

        Public Function CanExecute(parameter As Object) As Boolean Implements ICommand.CanExecute
            ' TODO: 判断有没有选中图块数据源
            Return True
        End Function
    End Class

End Namespace