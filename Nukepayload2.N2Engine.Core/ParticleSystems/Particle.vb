Namespace ParticleSystems
    ''' <summary>
    ''' 引擎实现的粒子一定是继承这个类的，但外部实现的不一定。
    ''' </summary>
    Public Class Particle
        Implements IParticle
        Public Sub New(acceleration As Vector2, lifeTime As Integer, location As Vector2, velocity As Vector2)
            Me.Acceleration = acceleration
            Me.LifeTime = lifeTime
            Me.Location = location
            Me.Velocity = velocity
        End Sub
        Public Property Acceleration As Vector2 Implements IParticle.Acceleration
        Public Property Age As Integer Implements IParticle.Age
        Public ReadOnly Property LifeTime As Integer Implements IParticle.LifeTime
        Public Property Location As Vector2 Implements IParticle.Location
        Public Property Velocity As Vector2 Implements IParticle.Velocity

        Public Overridable Sub Update() Implements IParticle.Update
            Age += 1
            Location += Velocity
            Velocity += Acceleration
        End Sub
    End Class

End Namespace