Imports Nukepayload2.Collections.Specialized
Imports Nukepayload2.N2Engine.ActionGames.UWP.Designer.Commands

Namespace Models
    Public Class StageModel
        ''' <summary>
        ''' 一个关卡内的所有图块
        ''' </summary>
        Public ReadOnly Property Tiles As New ObservableFixedArray2D(Of EditableTile)
        ''' <summary>
        ''' 被编辑器选中的图块的 X 坐标
        ''' </summary>
        Public Property SelectedTileX As Integer
        ''' <summary>
        ''' 被编辑器选中的图块的 Y 坐标
        ''' </summary>
        Public Property SelectedTileY As Integer
        ''' <summary>
        ''' 删除选定的图块
        ''' </summary>
        Public ReadOnly Property DeleteSelectedTile As New DeleteSelectedTileCommand

        ''' <summary>
        ''' 向选中的图块绘制
        ''' </summary>
        Public ReadOnly Property DrawDotCommand As New DrawDotCommand
    End Class
End Namespace
