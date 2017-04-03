Imports Windows.Storage

Namespace Models

    Public Class SpritePreview
        Sub New()

        End Sub

        Sub New(thumbnail As BitmapImage, file As StorageFile)
            Me.Thumbnail = thumbnail
            Me.File = file
        End Sub

        Public Property Thumbnail As BitmapImage
        Public Property File As StorageFile
    End Class
End Namespace