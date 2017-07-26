Imports Nukepayload2.N2Engine.Foundation
Imports Nukepayload2.N2Engine.N2Math

Namespace ParticleSystems

    Public Class RailgunParticleSystem
        Inherits DynamicParticleSystem(Of PointParticle)

        Public Sub New(spawnCount As Integer, spawnDuration As Integer, spawnInterval As Integer, color As Color, target As Vector2)
            MyBase.New(spawnCount, spawnDuration, spawnInterval)
            Me.Color = color
            Me.Target = target
        End Sub

        Public Property Color As Color

        Public Property Target As Vector2

        Dim progress% = 0

        Protected Overrides Function CreateParticle() As PointParticle
            progress += 1
            Dim InitLoc As Vector2 = If(RandomGenerator.RandomSingle < 0.4F, New Vector2,
                New Vector2(CSng(RandomGenerator.RandomSingle * 6 + 5 * Math.Cos(progress / 4)) - 5.5F,
                            CSng(RandomGenerator.RandomSingle * 6 + 5 * Math.Sin(progress / 4)) - 5.5F))
            Return New PointParticle(New Vector2(RandomGenerator.RandomSingle / 200.0F - 0.0025F,
                                                 RandomGenerator.RandomSingle / 200.0F - 0.0025F),
                                     CInt(RandomGenerator.RandomSingle * 10) + 110,
                                     InitLoc + Location + (Target - Location) * CSng(progress / SpawnCount), New Vector2)

        End Function
    End Class

End Namespace