Imports Nukepayload2.N2Engine.ActionGames.UWP.Designer.Commands
Imports Nukepayload2.N2Engine.ActionGames.UWP.Designer.Models

Namespace ViewModels
    ''' <summary>
    ''' 正在被编辑的关卡
    ''' </summary>
    Public Class StageViewModel
        Inherits SingleInstance(Of StageViewModel)
        Implements INotifyPropertyChanged
#Region "图块表管理"
        ''' <summary>
        ''' 导入的贴图表
        ''' </summary>
        Public ReadOnly Property SpriteSheets As New ObservableCollection(Of ImportedSpriteSheet)

        Dim _SelectedSpriteSheet As ImportedSpriteSheet
        ''' <summary>
        ''' 当前选中的贴图表
        ''' </summary>
        Public Property SelectedSpriteSheet As ImportedSpriteSheet
            Get
                Return _SelectedSpriteSheet
            End Get
            Set(value As ImportedSpriteSheet)
                _SelectedSpriteSheet = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(SelectedSpriteSheet)))
            End Set
        End Property

        Dim _PrimarySpriteSheet As ImportedSpriteSheet
        ''' <summary>
        ''' 主要贴图表
        ''' </summary>
        Public Property PrimarySpriteSheet As ImportedSpriteSheet
            Get
                Return _PrimarySpriteSheet
            End Get
            Set(value As ImportedSpriteSheet)
                _PrimarySpriteSheet = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(PrimarySpriteSheet)))
            End Set
        End Property

        ''' <summary>
        ''' 导入贴图表
        ''' </summary>
        Public ReadOnly Property ImportSpriteSheet As New ImportSpriteCommand
        ''' <summary>
        ''' 移除贴图表
        ''' </summary>
        Public ReadOnly Property RemoveSpriteSheet As New RemoveSpriteCommand
        ''' <summary>
        ''' 将选定的贴图表设为主要贴图表
        ''' </summary>
        Public ReadOnly Property SetPrimarySpriteSheet As New SetPrimarySpriteSheetCommand
#End Region
        ''' <summary>
        ''' 生成图块用的设置
        ''' </summary>
        Public ReadOnly Property MapSettings As New TilesGridSettings
        ''' <summary>
        ''' 被编辑的关卡包含的信息。
        ''' </summary>
        Public ReadOnly Property StageData As New StageModel

        Dim _IsBusy As Boolean
        Public Property IsBusy As Boolean
            Get
                Return _IsBusy
            End Get
            Set(value As Boolean)
                _IsBusy = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(IsBusy)))
            End Set
        End Property

        Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged
    End Class

End Namespace