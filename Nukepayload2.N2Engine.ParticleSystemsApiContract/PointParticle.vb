Namespace ParticleSystems
    ''' <summary>
    ''' 小到用一个点就能描述的粒子
    ''' </summary>
    Public Class PointParticle
        Inherits Particle

        Public Sub New(acceleration As Vector2, lifeTime As Integer, location As Vector2, velocity As Vector2)
            MyBase.New(acceleration, lifeTime, location, velocity)
        End Sub
    End Class
End Namespace