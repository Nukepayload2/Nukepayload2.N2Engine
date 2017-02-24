Namespace ParticleSystems
    ''' <summary>
    ''' 固定数量粒子的粒子系统。在开始的时候释放粒子。对于老的粒子实行回收和重生。
    ''' </summary>
    ''' <typeparam name="TParticle"></typeparam>
    Public MustInherit Class FixedCapacityParticleSystem(Of TParticle As IParticle)
        Inherits ParticleSystemBase(Of TParticle)

        ''' <summary>
        ''' 给定粒子的数量，创建固定数量粒子的粒子系统。
        ''' </summary>
        ''' <param name="count">粒子数量。必须大于0</param>
        Sub New(count As Integer, duration As Integer)
            MyBase.New(count, duration)
            If count <= 0 Then
                Throw New ArgumentOutOfRangeException(NameOf(count))
            End If
            ReDim Particles(count - 1)
            For i = 0 To Particles.Length - 1
                Particles(i) = CreateParticle()
            Next
        End Sub

        Public Property Particles As TParticle()

        Public Overrides Function GetParticles() As IEnumerable(Of TParticle)
            Return Particles
        End Function

        ''' <summary>
        ''' 一次释放粒子的数量
        ''' </summary>
        Public Overrides ReadOnly Property SpawnCount As Integer
            Get
                Return Particles.Length
            End Get
        End Property

        ''' <summary>
        ''' 更新粒子系统
        ''' </summary>
        Public Overrides Sub Update()
            If SpawnDuration >= 0 Then
                For Each par In Particles
                    UpdateParticle(par)
                    If par.Age >= par.LifeTime Then
                        RecycleParticle(par)
                    End If
                Next
                SpawnDuration -= 1
            Else
                RemoveFromGameCanvasCallback.Invoke
            End If
        End Sub
        ''' <summary>
        ''' 回收一个粒子，导致它的状态回归到初始。默认导致需要回收的粒子的<see cref="IParticle.Age"/>归零。 
        ''' </summary>
        ''' <param name="particle">需要回收的粒子</param>
        Protected Overridable Sub RecycleParticle(particle As TParticle)
            particle.Age = 0
        End Sub
    End Class
End Namespace