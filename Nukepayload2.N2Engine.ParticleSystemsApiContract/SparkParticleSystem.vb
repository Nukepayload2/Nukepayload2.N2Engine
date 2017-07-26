Option Strict Off
Imports Newtonsoft.Json
Imports Nukepayload2.N2Engine.Foundation
Imports Nukepayload2.N2Engine.N2Math

Namespace ParticleSystems
    ''' <summary>
    ''' 火花粒子系统
    ''' </summary>
    Public Class SparkParticleSystem
        Inherits DynamicParticleSystem(Of SparkParticle)

        Public Sub New(spawnCount As Integer, spawnDuration As Integer, spawnInterval As Integer, particleLife As Integer)
            MyBase.New(spawnCount, spawnDuration, spawnInterval)
            Me.ParticleLife = particleLife
            For i = 0 To spawnCount - 1
                Dim p = CreateParticle()
                p.Age = i
                p.Location = Location
                Particles.Enqueue(p)
            Next
        End Sub

        Public Property ParticleLife As Integer

        <JsonIgnore>
        Public Property GenerateColor As Func(Of Color) =
            Function()
                Dim colData(2) As Byte
                Dim Rnd = RandomGenerator.Rand
                Rnd.NextBytes(colData)
                Return Color.FromArgb(255, colData(0), colData(1), colData(2))
            End Function

        Protected Overrides Function CreateParticle() As SparkParticle
            Dim s As New SparkParticle(RandomGenerator.RandomVector2, ParticleLife, Location, New Vector2) With
            {
                .Age = RandomGenerator.RandomSingle * 80,
                .SparkSize = 1 + RandomGenerator.RandomSingle * 3,
                .SparkColor = GenerateColor.Invoke
            }
            Return s
        End Function
    End Class
End Namespace