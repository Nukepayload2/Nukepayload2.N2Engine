Imports Nukepayload2.N2Engine.Renderers
Imports Nukepayload2.N2Engine.UI.Effects

Namespace UI.Elements
    ''' <summary>
    ''' 在<see cref="GameCanvas"/>中的元素 
    ''' </summary>
    Public MustInherit Class GameElement
        Inherits GameVisual
        Public Overrides Function GetChildEffectSources() As IEnumerable(Of IGameEffectSource)
            Return Nothing
        End Function
    End Class
End Namespace