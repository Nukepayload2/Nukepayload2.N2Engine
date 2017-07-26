Namespace UI
    Public Enum GameCompositeModes
        ''' <summary>
        ''' 使用父级定义的值
        ''' </summary>
        Inherit
        ''' <summary>
        ''' 常规混合模式。按Z序列叠加。
        ''' </summary>
        SourceOver
        ''' <summary>
        ''' 与背景的颜色相加
        ''' </summary>
        AddBlend
        ''' <summary>
        ''' 与背景的颜色相减
        ''' </summary>
        MinBlend
        ''' <summary>
        ''' 反色，然后叠加。
        ''' </summary>
        Invert
    End Enum
End Namespace
