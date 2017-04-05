Imports Nukepayload2.N2Engine.ActionGames.UWP.Designer.Commands
Imports Nukepayload2.N2Engine.ActionGames.UWP.Designer.Models

Namespace ViewModels

    Public Class SpritesViewModel
        Inherits SingleInstance(Of SpritesViewModel)
        Implements INotifyPropertyChanged

        ''' <summary>
        ''' 项目中的贴图包
        ''' </summary>
        Public ReadOnly Property Sprites As New SpriteCollection

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

        Dim _SelectedSprite As SpritePreview
        ''' <summary>
        ''' 被选中的贴图
        ''' </summary>
        Public Property SelectedSprite As SpritePreview
            Get
                Return _SelectedSprite
            End Get
            Set(value As SpritePreview)
                _SelectedSprite = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(SelectedSprite)))
            End Set
        End Property

        ''' <summary>
        ''' 包含新的贴图到项目内
        ''' </summary>
        Public ReadOnly Property Include As New IncludeSpriteCommand
        ''' <summary>
        ''' 排除已存在的贴图到项目外
        ''' </summary>
        Public ReadOnly Property Exclude As New IncludeSpriteCommand

        Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged
    End Class

End Namespace