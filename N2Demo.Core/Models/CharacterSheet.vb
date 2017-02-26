Imports System.Numerics
Imports Newtonsoft.Json
Imports Nukepayload2.N2Engine.Foundation

<JsonObject(MemberSerialization.OptIn)>
Public Class CharacterSheet
    <JsonProperty>
    Public Property Location As New Vector2(200, 200)
    Public Property SpriteSize As New SizeInInteger(192, 128)
    Public Property Size As New Vector2(16, 16)
    Public Property Source As New Uri("n2-res-emb:///Images/CharacterSheet.png")
    ''' <summary>
    ''' 行列排列了多少位图
    ''' </summary>
    Public Property GridSize As New SizeInInteger(12, 8)
End Class