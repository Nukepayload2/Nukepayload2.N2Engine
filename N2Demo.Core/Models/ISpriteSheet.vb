Imports System.Numerics
Imports Nukepayload2.N2Engine.Foundation

Public Interface ISpriteSheet
    ReadOnly Property GridSize As SizeInInteger
    ReadOnly Property Size As Vector2
    ReadOnly Property Source As Uri
    ReadOnly Property SpriteSize As SizeInInteger
End Interface
