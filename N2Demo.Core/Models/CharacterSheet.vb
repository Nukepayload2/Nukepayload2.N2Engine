Imports System.Numerics
Imports Newtonsoft.Json
Imports Nukepayload2.N2Engine.Foundation
Imports Nukepayload2.N2Engine.Models

<JsonObject(MemberSerialization.OptIn)>
Public Class CharacterSheet
    Implements ICustomSpriteSheetItem
    Public Property Location As New Vector2(200, 100)
    Public Property SpriteSize As New SizeInInteger(96, 256) Implements ICustomSpriteSheetItem.SpriteSize
    Public Property Size As New Vector2(32, 64) Implements ICustomSpriteSheetItem.Size
    Public Property Source As New Uri("n2-res-emb:///Images/Flame3.png") Implements ICustomSpriteSheetItem.Source
    ''' <summary>
    ''' 行列排列了多少位图
    ''' </summary>
    Public Property GridSize As New SizeInInteger(3, 4) Implements ICustomSpriteSheetItem.GridSize
End Class