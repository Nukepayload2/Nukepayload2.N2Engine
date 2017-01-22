Imports Nukepayload2.N2Engine.Foundation

Namespace UI.Effects
    Public Class GaussianBlurEffect
        Inherits GameEffect

        Public ReadOnly Property BlurAmount As New PropertyBinder(Of Single)

        Public Overrides Function GetChildEffectSources() As IEnumerable(Of IGameEffectSource)
            Return Nothing
        End Function
    End Class
End Namespace
