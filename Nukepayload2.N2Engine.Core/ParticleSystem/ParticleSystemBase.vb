Imports Nukepayload2.N2Engine.Foundation
Imports Nukepayload2.N2Engine.UI
Imports Nukepayload2.N2Engine.UI.Elements

Namespace ParticleSystem
    ''' <summary>
    ''' 固定数量粒子的粒子系统。在开始的时候释放粒子。对于老的粒子实行回收和重生。
    ''' </summary>
    ''' <typeparam name="TParticle"></typeparam>
    Public MustInherit Class ParticleSystemBase(Of TParticle As IParticle)
        Implements IParticleSystem(Of TParticle)

        Sub New(spawnCount As Integer, spawnDuration As Integer)
            Me.SpawnCount = spawnCount
            Me.SpawnDuration = spawnDuration
        End Sub
        ''' <summary>
        ''' 一次释放粒子的数量
        ''' </summary>
        Public Overridable ReadOnly Property SpawnCount As Integer Implements IParticleSystem(Of TParticle).SpawnCount
        ''' <summary>
        ''' 这个动画多少帧之后会移除
        ''' </summary>
        Public Property SpawnDuration As Integer Implements IParticleSystem(Of TParticle).SpawnDuration
        ''' <summary>
        ''' 粒子系统的原点
        ''' </summary>
        Public Property Location As Vector2
        ''' <summary>
        ''' 注册一个回调，处理与它对应的 <see cref="GameElement"/> 从 <see cref="GameCanvas"/>移除这个过程。 
        ''' </summary>
        Public ReadOnly Property RemoveFromGameCanvasCallback As New PropertyBinder(Of Action)
        ''' <summary>
        ''' 新建一个粒子
        ''' </summary>
        Protected MustOverride Function CreateParticle() As TParticle
        ''' <summary>
        ''' 更新粒子系统
        ''' </summary>
        Public MustOverride Sub Update() Implements IParticleSystem(Of TParticle).Update
    End Class
End Namespace