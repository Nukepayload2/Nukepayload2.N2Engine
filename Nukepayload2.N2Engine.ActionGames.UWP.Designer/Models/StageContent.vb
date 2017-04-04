Namespace Models
    Public Class StageModel
        ''' <summary>
        ''' 一个关卡内的所有图块
        ''' </summary>
        ''' <returns></returns>
        Public Property Tiles As EditableTile(,)
        ''' <summary>
        ''' 被编辑器选中的图块
        ''' </summary>
        Public Property SelectedTile As EditableTile
    End Class
End Namespace
