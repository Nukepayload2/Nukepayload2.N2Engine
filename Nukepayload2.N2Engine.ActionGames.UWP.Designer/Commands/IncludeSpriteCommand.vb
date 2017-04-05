Imports Nukepayload2.N2Engine.ActionGames.UWP.Designer.ViewModels
Imports Windows.Storage.Pickers

Namespace Commands

    Public Class IncludeSpriteCommand
        Implements ICommand

        Public Event CanExecuteChanged As EventHandler Implements ICommand.CanExecuteChanged

        Public Async Sub ExecuteAsync(parameter As Object) Implements ICommand.Execute
            Dim filePicker As New FileOpenPicker
            filePicker.FileTypeFilter.Add(".jpg")
            filePicker.FileTypeFilter.Add(".png")
            Dim vm = SpritesViewModel.Current
            vm.IsBusy = True
            Dim file = Await filePicker.PickMultipleFilesAsync
            If file IsNot Nothing Then
                Await vm.Sprites.LoadAsync(file)
            End If
            vm.IsBusy = False
        End Sub

        Public Function CanExecute(parameter As Object) As Boolean Implements ICommand.CanExecute
            ' TODO: 繁忙状态
            Return True
        End Function
    End Class

End Namespace