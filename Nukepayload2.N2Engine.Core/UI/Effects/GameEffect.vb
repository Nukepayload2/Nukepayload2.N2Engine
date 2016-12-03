Namespace UI.Effects
    ''' <summary>
    ''' 需要平台实现的图像特效
    ''' </summary>
    Public MustInherit Class GameEffect
        Inherits GameObject
        Implements IGameEffectSource
        ''' <summary>
        ''' 枚举这个效果子效果源，不包括父级元素。
        ''' </summary>
        Public MustOverride Function GetChildEffectSources() As IEnumerable(Of IGameEffectSource) Implements IGameEffectSource.GetChildEffectSources
    End Class
End Namespace