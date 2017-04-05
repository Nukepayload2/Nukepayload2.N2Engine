Imports Newtonsoft.Json
Imports Windows.Storage

Namespace Models

    <JsonObject(MemberSerialization.OptIn)>
    Public Class SpritePreview
        Sub New()

        End Sub

        Sub New(thumbnail As WriteableBitmap, file As StorageFile)
            Me.Thumbnail = thumbnail
            Me.File = file
        End Sub

        <JsonProperty>
        Public Property TileWidth As Integer = 64
        <JsonProperty>
        Public Property TileHeight As Integer = 64

        Public Property Thumbnail As WriteableBitmap
        Public Property File As StorageFile
    End Class
End Namespace