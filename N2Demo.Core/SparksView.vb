Imports System.Numerics
Imports Nukepayload2.N2Engine.UI.Elements
Imports Nukepayload2.N2Engine.UI.Views

Public Class SparksView
    Inherits GameCanvas

    Dim redBlock As New EllipseElement
    Dim greenEllipse As New RectangleElement
    Dim sparks As New SparkParticleSystemView
    Public Property SparksData As New SparksViewModel

    Sub New()
        sparks.Data.Bind(Function() SparksData.SparkSys)
        Add(sparks)

        redBlock.Fill.Bind(Function() SparksData.RedCircle.Color)
        redBlock.Location.Bind(Function() SparksData.RedCircle.Position)
        redBlock.Size.Bind(Function() SparksData.RedCircle.Size)
        Add(redBlock)

        greenEllipse.Stroke.Bind(Function() SparksData.GreenRectangle.Color)
        greenEllipse.Location.Bind(Function() SparksData.GreenRectangle.Position)
        greenEllipse.Size.Bind(Function() SparksData.GreenRectangle.Size)
        Add(greenEllipse)
    End Sub
    ''' <summary>
    ''' 平台特定实现点击此视图时，调用此方法
    ''' </summary>
    Public Sub OnTapped(pos As Vector2)
        SparksData.SparkSys.Location = pos
    End Sub
End Class