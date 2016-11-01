Imports System.Numerics
Imports Nukepayload2.N2Engine.Resources
Imports Nukepayload2.N2Engine.UI.Elements
Imports Nukepayload2.N2Engine.UI.Views

Public Class SparksView
    Inherits GameCanvas

    Dim redBlock As New EllipseElement
    Dim greenEllipse As New RectangleElement
    Dim sparks As New SparkParticleSystemView
    Dim charaSheet As New SpriteElement
    Dim sparksData As New SparksViewModel
    Dim characterSheetSprite As BitmapResource

    Sub New()
        ApplyRoute()

        sparks.Data.Bind(Function() sparksData.SparkSys)
        AddVisual(sparks)

        redBlock.Fill.Bind(Function() sparksData.RedCircle.Color)
        redBlock.Location.Bind(Function() sparksData.RedCircle.Position)
        redBlock.Size.Bind(Function() sparksData.RedCircle.Size)
        AddVisual(redBlock)

        greenEllipse.Stroke.Bind(Function() sparksData.GreenRectangle.Color)
        greenEllipse.Location.Bind(Function() sparksData.GreenRectangle.Position)
        greenEllipse.Size.Bind(Function() sparksData.GreenRectangle.Size)
        AddVisual(greenEllipse)

        characterSheetSprite = BitmapResource.Create(sparksData.CharacterSheet.Source)
        charaSheet.Sprite.Bind(Function() characterSheetSprite)
        charaSheet.Size.Bind(Function() sparksData.CharacterSheet.Size)
        charaSheet.Location.Bind(Function() sparksData.CharacterSheet.Location)
        AddVisual(charaSheet)
    End Sub
    ''' <summary>
    ''' 平台特定实现点击此视图时，调用此方法。通用的输入事件完成后，此方法将过时。
    ''' </summary>
    Public Sub OnTapped(pos As Vector2)
        sparksData.SparkSys.Location = pos
    End Sub
End Class