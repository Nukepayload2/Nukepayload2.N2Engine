Imports Windows.Storage

Namespace Models

    Public Class SpritePreview
        Sub New()

        End Sub

        Sub New(thumbnail As WriteableBitmap, file As StorageFile)
            Me.Thumbnail = thumbnail
            Me.File = file
        End Sub

        Public Property Thumbnail As WriteableBitmap
        Public Property File As StorageFile
    End Class
End Namespace