Imports Nukepayload2.N2Engine.ActionGames.UWP.Designer.Utilities
Imports Windows.Storage

Namespace Models

    Public Class SpriteCollection
        Inherits ObservableCollection(Of SpritePreview)

        Public Async Function LoadAsync(assets As String()) As Task
            Dim picturesTask = From f In assets Select AccessCache.StorageApplicationPermissions.FutureAccessList.GetFileAsync(f).AsTask
            Dim pictures = Await Task.WhenAll(picturesTask)
            Dim thumbnailGenerator As New ThumbnailGenerator(300)
            For Each f In pictures
                Dim thumbnail = Await thumbnailGenerator.GenerateThumbnailAsync(f)
                Add(New SpritePreview(thumbnail, f))
            Next
        End Function

    End Class

End Namespace