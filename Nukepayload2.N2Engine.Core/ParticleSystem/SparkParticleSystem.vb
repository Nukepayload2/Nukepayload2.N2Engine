Option Strict Off
Imports Nukepayload2.N2Engine.Core.Numerics
Namespace ParticleSystem
    ''' <summary>
    ''' 火花粒子系统
    ''' </summary>
    Public Class SparkParticleSystem
        Inherits DynamicParticleSystem(Of SparkParticle)

        Public Sub New(spawnCount As Integer, spawnDuration As Integer, spawnInterval As Integer, particleLife As Integer, removeFromGameCanvasCallback As PropertyBinder(Of Action))
            MyBase.New(spawnCount, spawnDuration, spawnInterval, removeFromGameCanvasCallback)
            Me.ParticleLife = particleLife
            For i = 0 To spawnCount - 1
                Dim p = CreateParticle()
                p.Age = i
                p.Location = Location
                Particles.Enqueue(p)
            Next
        End Sub
        Public Property ParticleLife As Integer

        Protected Shared Rnd As New Random
        Protected Shared Directions() As Vector2 = {New Vector2(0, 1), New Vector2(0, -1), New Vector2(-1, 0), New Vector2(1, 0), New Vector2(0.7, 0.7), New Vector2(0.7, -0.7), New Vector2(-0.7, 0.7), New Vector2(-0.7, -0.7)}

        Protected Overrides Function CreateParticle() As SparkParticle
            Dim colData(2) As Byte
            Rnd.NextBytes(colData)
            Dim s As New SparkParticle(Directions(Rnd.Next(8)).RotateNew(Rnd.Next(60)) * Rnd.NextDouble, ParticleLife, Location, New Vector2) With
            {
                .Age = Rnd.NextDouble * 80,
                .SparkSize = 1 + Rnd.NextDouble * 3,
                .SparkColor = Color.FromArgb(255, colData(0), colData(1), colData(2))
            }
            Return s
        End Function
    End Class
End Namespace