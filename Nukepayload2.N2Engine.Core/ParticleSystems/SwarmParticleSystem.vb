Imports Nukepayload2.N2Engine.Animations
Imports Nukepayload2.N2Engine.Foundation
Imports Nukepayload2.N2Engine.N2Math

Namespace ParticleSystems

    Public Class SwarmParticleSystem
        Inherits FixedCapacityParticleSystem(Of SpriteParticle)
        Implements ICommonSpriteParticleSystem

        Public Sub New(count As Integer, duration As Integer,
                       twinkleStep As Single, imageList As BitmapDiscreteAnimation, bounds As SizeInInteger)
            MyBase.New(count, duration)
            Me.TwinkleStep = twinkleStep
            Me.ImageList = imageList
            Me.Bounds = bounds
            CreateParticles()
        End Sub

        ''' <summary>
        ''' 每帧闪烁的步幅
        ''' </summary>
        Public Property TwinkleStep As Single
        ''' <summary>
        ''' 位图动画
        ''' </summary>
        Public Property ImageList As BitmapDiscreteAnimation Implements ICommonSpriteParticleSystem.ImageList
        ''' <summary>
        ''' 边界
        ''' </summary>
        Public Property Bounds As SizeInInteger
        ''' <summary>
        ''' 虫子的最大速度
        ''' </summary>
        Public Property MaxSpeed As Single = 5.0F

        Public Overrides Sub UpdateParticle(particle As SpriteParticle)
            Dim rndVel = RandomGenerator.RandomVector2
            With particle
                Dim ub! = CSng(Math.Max(0, 1 - .Age / .LifeTime))
                If .Opacity <= 0 OrElse .Opacity > ub Then
                    .OpacityStep = - .OpacityStep
                    If .Opacity < 0 Then
                        .Opacity = 0
                    ElseIf .Opacity > ub Then
                        .Opacity = ub
                    End If
                End If
                .Velocity += New Vector2(RandomGenerator.RandomSingle - 0.5F, RandomGenerator.RandomSingle - 0.5F).WithLength(0.5F)
                Const bound = 20
                Dim lx = .Location.X, ly = .Location.Y, vx = .Velocity.X, vy = .Velocity.Y
                If lx < -bound Then
                    lx = -bound
                    vx = -vx
                ElseIf lx - bound > Bounds.Width Then
                    lx = Bounds.Width + bound
                    vx = -vx
                End If
                If ly < -bound Then
                    ly = -bound
                    vy = -vy
                ElseIf ly - bound > Bounds.Height Then
                    ly = Bounds.Height + bound
                    vy = -vy
                End If
                .Location = New Vector2(lx, ly)
                .Velocity = New Vector2(vx, vy).WithLength(MaxSpeed)
                .Update()
            End With
        End Sub

        Protected Overrides Function CreateParticle() As SpriteParticle
            Return New SpriteParticle(New Vector2, Integer.MaxValue,
                                      RandomGenerator.RandomVector2Positive(Bounds.Width, Bounds.Height),
                                      New Vector2(RandomGenerator.RandomSingle - 0.5F, RandomGenerator.RandomSingle - 0.5F).WithLength(0.5F), ImageList) With {.OpacityStep = TwinkleStep, .Opacity = RandomGenerator.RandomSingle}
        End Function
    End Class

End Namespace