Imports System.Numerics
Imports Nukepayload2.N2Engine.Foundation

Public Class GhostFlameSpriteSheet
    Implements ISpriteSheet

    Public ReadOnly Property GridSize As New SizeInInteger(3, 4) Implements ISpriteSheet.GridSize

    Public ReadOnly Property Size As New Vector2(32, 64) Implements ISpriteSheet.Size

    Public ReadOnly Property SpriteSize As New SizeInInteger(96, 256) Implements ISpriteSheet.SpriteSize

    Public ReadOnly Property Source As New Uri("n2-res-emb:///Images/Flame3.png") Implements ISpriteSheet.Source
End Class
