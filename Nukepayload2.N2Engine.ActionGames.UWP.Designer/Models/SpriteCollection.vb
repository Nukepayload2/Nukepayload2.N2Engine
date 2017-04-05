Imports Nukepayload2.N2Engine.ActionGames.UWP.Designer.Utilities
Imports Windows.Storage

Namespace Models

    Public Class SpriteCollection
        Inherits ObservableCollection(Of SpritePreview)

        Public Async Function LoadAsync(assets As String()) As Task
            Dim picturesTask = From f In assets Select AccessCache.StorageApplicationPermissions.FutureAccessList.GetFileAsync(f).AsTask
            Await LoadAsync(Await Task.WhenAll(picturesTask))
        End Function

        Public Async Function LoadAsync(files As IEnumerable(Of StorageFile)) As Task
            Dim thumbnailGenerator As New ThumbnailGenerator(300)
            For Each f In files
                Dim thumbnail = Await thumbnailGenerator.GenerateThumbnailAsync(f)
                Add(New SpritePreview(thumbnail, f))
            Next
        End Function
    End Class

End Namespace