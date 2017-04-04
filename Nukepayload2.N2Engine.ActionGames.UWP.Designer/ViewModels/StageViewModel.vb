Imports Nukepayload2.N2Engine.ActionGames.UWP.Designer.Models

Namespace ViewModels
    ''' <summary>
    ''' 正在被编辑的关卡
    ''' </summary>
    Public Class StageViewModel
        Inherits SingleInstance(Of StageViewModel)
        Implements INotifyPropertyChanged
        ''' <summary>
        ''' 导入的贴图表
        ''' </summary>
        Public ReadOnly Property SpriteSheets As New ObservableCollection(Of EditableSpriteSheet)
        ''' <summary>
        ''' 生成图块用的设置
        ''' </summary>
        Public ReadOnly Property Settings As New TilesGridSettings
        ''' <summary>
        ''' 被编辑的关卡包含的信息。
        ''' </summary>
        Public ReadOnly Property StageData As New StageModel

        Dim _SelectedSpriteSheetIndex As Integer = -1
        ''' <summary>
        ''' 被选中的贴图表的下标。当贴图表集合
        ''' </summary>
        Public Property SelectedSpriteSheetIndex As Integer
            Get
                Return _SelectedSpriteSheetIndex
            End Get
            Set(value As Integer)
                _SelectedSpriteSheetIndex = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(SelectedSpriteSheetIndex)))
            End Set
        End Property

        Dim _SelectedTile As EditableTile
        ''' <summary>
        ''' 当前选中的图块。
        ''' </summary>
        Public Property SelectedTile As EditableTile
            Get
                Return _SelectedTile
            End Get
            Set(value As EditableTile)
                _SelectedTile = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(SelectedTile)))
            End Set
        End Property

        Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged
    End Class

End Namespace