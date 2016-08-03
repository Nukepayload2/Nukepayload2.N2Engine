Namespace ParticleSystem

    ''' <summary>
    ''' 基本的粒子
    ''' </summary>
    Public Interface IParticle
        ''' <summary>
        ''' 位置
        ''' </summary>
        Property Location As Vector2
        ''' <summary>
        ''' 速度
        ''' </summary>
        Property Velocity As Vector2
        ''' <summary>
        ''' 加速度
        ''' </summary>
        Property Acceleration As Vector2
        ''' <summary>
        ''' 已经存在了多少帧了
        ''' </summary>
        Property Age%
        ''' <summary>
        ''' 最大生存时间
        ''' </summary>
        ReadOnly Property LifeTime%
        ''' <summary>
        ''' 更新数据
        ''' </summary>
        Sub Update()
    End Interface
End Namespace