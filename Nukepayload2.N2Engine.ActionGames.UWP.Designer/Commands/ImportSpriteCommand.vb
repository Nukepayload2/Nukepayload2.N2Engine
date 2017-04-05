Imports Nukepayload2.N2Engine.ActionGames.UWP.Designer.Models
Imports Nukepayload2.N2Engine.ActionGames.UWP.Designer.ViewModels

Namespace Commands

    Public Class ImportSpriteCommand
        Implements ICommand

        Public Event CanExecuteChanged As EventHandler Implements ICommand.CanExecuteChanged

        Public Async Sub ExecuteAsync(parameter As Object) Implements ICommand.Execute
            Dim vm = StageViewModel.Current
            Dim sprites = SpritesViewModel.Current
            Dim spriteProp = SpriteSheetsViewModel.Current
            If sprites.SelectedSprite IsNot Nothing Then
                Dim tileSize = spriteProp.TileSize
                vm.IsBusy = True
                Dim sprite = Await ImportedSpriteSheet.CreateAsync(sprites.SelectedSprite, tileSize.X, tileSize.Y)
                vm.IsBusy = False
                vm.SpriteSheets.Add(sprite)
            End If
        End Sub

        Public Function CanExecute(parameter As Object) As Boolean Implements ICommand.CanExecute
            ' TODO: UI 繁忙状态
            Return True
        End Function
    End Class

End Namespace