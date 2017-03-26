Imports Nukepayload2.N2Engine.Foundation

Namespace Models

    Public Interface ICustomSpriteSheetItem
        ReadOnly Property GridSize As SizeInInteger
        ReadOnly Property Size As Vector2
        ReadOnly Property Source As Uri
        ReadOnly Property SpriteSize As SizeInInteger
    End Interface

End Namespace