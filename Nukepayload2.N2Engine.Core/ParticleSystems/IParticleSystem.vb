Namespace ParticleSystems
    Public Interface IParticleSystem
        ''' <summary>
        ''' 每次释放多少个粒子
        ''' </summary>
        ReadOnly Property SpawnCount%
        ''' <summary>
        ''' 释放多少帧
        ''' </summary>
        Property SpawnDuration%
        ''' <summary>
        ''' 更新粒子系统
        ''' </summary>
        Sub Update()
    End Interface
    Public Interface IParticleSystem(Of TParticle As IParticle)
        Inherits IParticleSystem

        Sub UpdateParticle(particle As TParticle)
    End Interface
End Namespace