Imports Nukepayload2.N2Engine.Animations

Namespace ParticleSystems

    Public Class SmokeParticleSystem
        Inherits DynamicParticleSystem(Of SpriteParticle)
        Implements ICommonSpriteParticleSystem

        Public Sub New(spawnCount As Integer, spawnDuration As Integer, spawnInterval As Integer,
                       direction As Vector2, wind As Vector2, particleLife As Integer, imageList As BitmapDiscreteAnimation)
            MyBase.New(spawnCount, spawnDuration, spawnInterval)
            Me.Direction = direction
            Me.Wind = wind
            Me.ParticleLife = particleLife
            Me.ImageList = imageList
        End Sub

        ''' <summary>
        ''' 烟雾的方向
        ''' </summary>
        Public Property Direction As Vector2
        ''' <summary>
        ''' 风力对烟雾造成的加速度
        ''' </summary>
        Public Property Wind As Vector2
        ''' <summary>
        ''' 烟雾的延迟时间
        ''' </summary>
        Public Property ParticleLife As Integer
        ''' <summary>
        ''' 位图动画
        ''' </summary>
        Public Property ImageList As BitmapDiscreteAnimation Implements ICommonSpriteParticleSystem.ImageList

        Protected Overrides Function CreateParticle() As SpriteParticle
            Return New SpriteParticle(Wind, ParticleLife, Location, Direction, ImageList)
        End Function
    End Class

End Namespace