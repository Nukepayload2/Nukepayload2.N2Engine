Imports Nukepayload2.N2Engine.Foundation

Namespace UI.Effects
    ''' <summary>
    ''' (仅 Win2D) 高斯模糊效果
    ''' </summary>
    Public Class GaussianBlurEffect
        Inherits GameEffect
        ''' <summary>
        ''' 模糊半径
        ''' </summary>
        Public Property BlurAmount As PropertyBinder(Of Single) = New ManualPropertyBinder(Of Single)
        ''' <summary>
        ''' 这个效果没有子效果源。
        ''' </summary>
        Public Overrides Function GetChildEffectSources() As IEnumerable(Of IGameEffectSource)
            Return Nothing
        End Function
    End Class
End Namespace
