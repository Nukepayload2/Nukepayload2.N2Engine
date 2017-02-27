Imports System.Numerics
Imports Newtonsoft.Json
Imports Nukepayload2.N2Engine.Foundation

<JsonObject(MemberSerialization.OptIn)>
Public Class CharacterSheet
    Implements ISpriteSheet
    <JsonProperty>
    Public Property Location As New Vector2(200, 200)
    Public Property SpriteSize As New SizeInInteger(192, 128) Implements ISpriteSheet.SpriteSize
    Public Property Size As New Vector2(16, 16) Implements ISpriteSheet.Size
    Public Property Source As New Uri("n2-res-emb:///Images/CharacterSheet.png") Implements ISpriteSheet.Source
    ''' <summary>
    ''' 行列排列了多少位图
    ''' </summary>
    Public Property GridSize As New SizeInInteger(12, 8) Implements ISpriteSheet.GridSize
End Class