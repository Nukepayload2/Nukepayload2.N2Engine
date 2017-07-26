Imports Nukepayload2.N2Engine.UI.Effects

Namespace UI.Elements
    ''' <summary>
    ''' 在<see cref="GameCanvas"/>中的元素 
    ''' </summary>
    Public MustInherit Class GameElement
        Inherits GameVisual
        ''' <summary>
        ''' 这个类型默认没有附加的元素作为效果源。
        ''' </summary>
        Public Overrides Function GetChildEffectSources() As IEnumerable(Of IGameEffectSource)
            Return Nothing
        End Function
    End Class
End Namespace