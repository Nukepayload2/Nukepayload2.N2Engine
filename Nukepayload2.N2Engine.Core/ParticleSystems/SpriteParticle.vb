Imports Newtonsoft.Json
Imports Nukepayload2.N2Engine.Animations
Imports Nukepayload2.N2Engine.Resources

Namespace ParticleSystems

    Public Class SpriteParticle
        Inherits Particle
        Sub New(acceleration As Vector2, lifeTime As Integer, location As Vector2, velocity As Vector2, imagelist As BitmapDiscreteAnimation)
            MyBase.New(acceleration, lifeTime, location, velocity)
            If imagelist Is Nothing Then
                Throw New ArgumentNullException(NameOf(imagelist))
            End If
            Me.ImageList = imagelist
        End Sub
        ''' <summary>
        ''' 已经解析过的位图动画
        ''' </summary>
        <JsonIgnore>
        Public Property ImageList As BitmapDiscreteAnimation

        Dim _ImageEnumStatus As IEnumerator(Of BitmapResource)
        ''' <summary>
        ''' 动画的播放进度
        ''' </summary>
        <JsonIgnore>
        Public ReadOnly Property ImageEnumStatus As IEnumerator(Of BitmapResource)
            Get
                If _ImageEnumStatus Is Nothing Then
                    _ImageEnumStatus = ImageList.GetEnumerator
                End If
                Return _ImageEnumStatus
            End Get
        End Property
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