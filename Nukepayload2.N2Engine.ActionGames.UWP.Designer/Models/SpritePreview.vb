Imports Windows.Storage

Namespace Models

    Public Class SpritePreview
        Sub New()

        End Sub

        Sub New(thumbnail As ImageSource, file As StorageFile)
            Me.Thumbnail = thumbnail
            Me.File = file
        End Sub

        Public Property Thumbnail As ImageSource
        Public Property File As StorageFile
    End Class
End Namespace