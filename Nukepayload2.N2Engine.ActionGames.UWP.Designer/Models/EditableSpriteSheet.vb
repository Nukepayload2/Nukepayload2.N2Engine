Namespace Models
    ''' <summary>
    ''' 表示一张图块表。
    ''' </summary>
    Public Class EditableSpriteSheet
        ''' <summary>
        ''' 基于的图片。
        ''' </summary>
        Public Property SpriteFile As SpritePreview
        ''' <summary>
        ''' 这张图块表包含的图块。
        ''' </summary>
        Public Property Tiles As EditableTile(,)
    End Class
End Namespace
