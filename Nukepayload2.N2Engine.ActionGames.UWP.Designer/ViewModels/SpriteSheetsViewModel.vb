Imports Nukepayload2.N2Engine.ActionGames.UWP.Designer.Models

Namespace ViewModels

    Public Class SpriteSheetsViewModel
        Inherits SingleInstance(Of SpriteSheetsViewModel)

        Public ReadOnly Property SpriteSheets As New ObservableCollection(Of EditableSpriteSheet)
        Public ReadOnly Property EntitySheetFactory As New EntitySheetFactory
        Public ReadOnly Property WorldFactory As New WorldFactory
    End Class

End Namespace