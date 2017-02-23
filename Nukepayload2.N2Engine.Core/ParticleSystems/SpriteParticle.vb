Imports Nukepayload2.N2Engine.Animations

Namespace ParticleSystems

    Public Class SpriteParticle
        Inherits Particle
        Sub New(acceleration As Vector2, lifeTime As Integer, location As Vector2, velocity As Vector2, imagelist As BitmapDiscreteAnimation)
            MyBase.New(acceleration, lifeTime, location, velocity)
            Me.ImageList = imagelist
        End Sub
        ''' <summary>
        ''' 已经解析过的位图动画
        ''' </summary>
        Public Property ImageList As BitmapDiscreteAnimation
        ''' <summary>
        ''' 这个粒子的透明度
        ''' </summary>
        Public Property Opacity As Single
        ''' <summary>
        ''' 透明度变化系数
        ''' </summary>
        Public Property OpacityStep As Single

        Public Overrides Sub Update()
            MyBase.Update()
            Opacity += OpacityStep
        End Sub
    End Class
End Namespace