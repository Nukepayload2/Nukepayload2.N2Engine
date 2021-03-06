﻿Imports Newtonsoft.Json
Imports Nukepayload2.N2Engine.UI.Elements

Namespace ParticleSystems
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
        <JsonIgnore>
        Public Property RemoveFromGameCanvasCallback As Action
        ''' <summary>
        ''' 新建一个粒子
        ''' </summary>
        Protected MustOverride Function CreateParticle() As TParticle
        ''' <summary>
        ''' 更新粒子系统
        ''' </summary>
        Public MustOverride Sub Update() Implements IParticleSystem(Of TParticle).Update
        ''' <summary>
        ''' 更新粒子
        ''' </summary>
        ''' <param name="particle">要更新的粒子</param>
        Public Overridable Sub UpdateParticle(particle As TParticle) Implements IParticleSystem(Of TParticle).UpdateParticle
            particle.Update()
        End Sub
        ''' <summary>
        ''' 枚举粒子系统包含的粒子
        ''' </summary>
        Public MustOverride Function GetParticles() As IEnumerable(Of TParticle) Implements IParticleSystem(Of TParticle).GetParticles
    End Class
End Namespace