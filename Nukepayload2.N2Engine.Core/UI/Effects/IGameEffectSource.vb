Namespace UI.Effects
    ''' <summary>
    ''' 游戏效果资源
    ''' </summary>
    Public Interface IGameEffectSource
        ''' <summary>
        ''' 获取当前效果源的子效果源，不包括父级元素。可以设置为 Nothing (在 Visual C# 为 null) 代表这个节点没有子效果源。
        ''' </summary>
        Function GetChildEffectSources() As IEnumerable(Of IGameEffectSource)
    End Interface

End Namespace