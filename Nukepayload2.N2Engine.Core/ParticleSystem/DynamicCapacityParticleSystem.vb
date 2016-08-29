Imports Nukepayload2.N2Engine.Foundation

Namespace ParticleSystem
    ''' <summary>
    ''' 持续释放和销毁的粒子系统
    ''' </summary>
    ''' <typeparam name="TParticle"></typeparam>
    Public MustInherit Class DynamicParticleSystem(Of TParticle As IParticle)
        Inherits ParticleSystemBase(Of TParticle)

        Dim lock As New Object

        Sub New(spawnCount As Integer, spawnDuration As Integer, spawnInterval As Integer, removeFromGameCanvasCallback As PropertyBinder(Of Action))
            MyBase.New(spawnCount, spawnDuration)
            Me.SpawnInterval = spawnInterval
            Me.RemoveFromGameCanvasCallback = removeFromGameCanvasCallback
        End Sub
        Public Property Particles As New Queue(Of TParticle)
        ''' <summary>
        ''' 释放粒子的间隔
        ''' </summary>
        Public Property SpawnInterval As Integer
        ''' <summary>
        ''' 更新粒子系统
        ''' </summary>
        Public Overrides Sub Update()
            If SpawnDuration >= 0 Then
                If SpawnInterval <= 1 OrElse (SpawnDuration Mod (SpawnInterval - 1) <= 0) Then
                    SyncLock lock
                        For i = 1 To SpawnCount
                            Particles.Enqueue(CreateParticle())
                        Next
                    End SyncLock
                End If
                SpawnDuration -= 1
            Else
                If Particles.Count = 0 Then
                    RemoveFromGameCanvasCallback.Value.Invoke
                End If
            End If
            Dim deq = 0
            For Each par In Particles
                par.Update()
                If par.Age >= par.LifeTime Then
                    deq += 1
                End If
            Next
            SyncLock lock
                For i = 1 To deq
                    OnParticleRemoved(Particles.Dequeue())
                Next
            End SyncLock
        End Sub
        Protected Overridable Sub OnParticleRemoved(particle As TParticle)

        End Sub
    End Class
End Namespace