Imports System.Numerics
Imports Nukepayload2.N2Engine.Foundation
Imports Nukepayload2.N2Engine.ParticleSystem

Public Class SparksViewModel
    Public ReadOnly Property RemoveFromVisualTree As New PropertyBinder(Of Action)
    Public Property SparkSys As New SparkParticleSystem(1000, Integer.MaxValue, 30, 150, RemoveFromVisualTree) With {.Location = New Vector2(150, 150)}
End Class
