Imports System.Numerics
Imports Nukepayload2.N2Engine.Foundation
Imports Nukepayload2.N2Engine.ParticleSystem

Public Class SparksViewModel
    Public Property SparkSys As New SparkParticleSystem(1000, Integer.MaxValue, 30, 150) With {.Location = New Vector2(150, 150)}
    Public Property RedCircle As New ColorizedBound With {
        .Position = New Vector2(130, 200),
        .Color = New Color(ColorValues.Red),
        .Size = New Vector2(100, 100)
    }
    Public Property GreenRectangle As New ColorizedBound With {
        .Position = New Vector2(330, 200),
        .Color = New Color(ColorValues.Green),
        .Size = New Vector2(233, 100)
    }
End Class
