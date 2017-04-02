Imports Nukepayload2.N2Engine.ActionGames.UWP.Designer.Models

Namespace ViewModels

    Public Class StageViewModel
        Public ReadOnly Property SpriteSheets As New ObservableCollection(Of EditableSpriteSheet)
        Public ReadOnly Property SelectedSpriteSheet As EditableSpriteSheet
        Public ReadOnly Property Settings As New TileSettings
        Public Property StageData As StageModel
    End Class

End Namespace