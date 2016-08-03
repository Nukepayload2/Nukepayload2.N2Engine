Imports Nukepayload2.N2Engine.Core.Animations

Namespace ParticleSystem

    Public Class ImageParticle
        Inherits Particle
        Sub New(acceleration As Vector2, lifeTime As Integer, location As Vector2, velocity As Vector2, imagelist As BitmapAnimation)
            MyBase.New(acceleration, lifeTime, location, velocity)
            Foreground = imagelist
        End Sub
        ''' <summary>
        ''' 已经解析过的图像
        ''' </summary>
        Public Property Foreground As BitmapAnimation
        ''' <summary>
        ''' 计算当前所处的下标
        ''' </summary>
        ''' <returns></returns>
        Public ReadOnly Property ImageIndex%
            Get
                Return Foreground.GetImageIndex(Age)
            End Get
        End Property
    End Class
End Namespace