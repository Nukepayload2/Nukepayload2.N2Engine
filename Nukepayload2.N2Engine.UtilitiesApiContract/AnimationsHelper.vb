Imports Nukepayload2.N2Engine.Animations
Imports Nukepayload2.N2Engine.Models
Imports Nukepayload2.N2Engine.Resources

Namespace Utilities

    Public Module AnimationsHelper

        Public Function MakeAnimation(spriteSheet As ICustomSpriteSheetItem, bmp As BitmapResource,
             filter As Func(Of IEnumerable(Of BitmapResource), IEnumerable(Of BitmapResource))) As BitmapDiscreteAnimation
            Dim spriteSize = spriteSheet.SpriteSize
            Dim gridSize = spriteSheet.GridSize
            Return New BitmapDiscreteAnimation(filter(
                   bmp.Split(spriteSize.Width \ gridSize.Width,
                   spriteSize.Height \ gridSize.Height,
                   gridSize.Width, gridSize.Height)))
        End Function

    End Module
End Namespace
