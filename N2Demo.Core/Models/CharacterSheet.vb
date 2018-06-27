Imports System.ComponentModel
Imports System.Numerics
Imports Newtonsoft.Json
Imports Nukepayload2.N2Engine.Foundation
Imports Nukepayload2.N2Engine.Models

<JsonObject(MemberSerialization.OptIn)>
Public Class CharacterSheet
    Implements ICustomSpriteSheetItem, INotifyPropertyChanged

    Private _Size As New Vector2(32, 64)
    Public Property Location As New Vector2(100, 200)
    Public Property SpriteSize As New SizeInInteger(96, 256) Implements ICustomSpriteSheetItem.SpriteSize
    Public Property Size As Vector2 Implements ICustomSpriteSheetItem.Size
        Get
            Return _Size
        End Get
        Set
            _Size = Value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(Size)))
        End Set
    End Property

    Public Property Source As New Uri("n2-res-emb:///Images/Flame3.png") Implements ICustomSpriteSheetItem.Source
    ''' <summary>
    ''' 行列排列了多少位图
    ''' </summary>
    Public Property GridSize As New SizeInInteger(3, 4) Implements ICustomSpriteSheetItem.GridSize

    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged
End Class