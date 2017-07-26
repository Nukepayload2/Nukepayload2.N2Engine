Namespace UI.Text
    ''' <summary>
    ''' 描述与某个字体与该字体的正常纵横比相比的拉伸程度。可以强制转换成 Windows.UI.Text.FontStretch。
    ''' </summary>
    Public Enum FontStretch
        ''' <summary>
        ''' 没有定义字型自动缩放
        ''' </summary>
        Undefined = 0
        ''' <summary>
        ''' 压缩到 50%
        ''' </summary>
        UltraCondensed = 1
        ''' <summary>
        ''' 压缩到 62.5%
        ''' </summary>
        ExtraCondensed = 2
        ''' <summary>
        ''' 压缩到 75%
        ''' </summary>
        Condensed = 3
        ''' <summary>
        ''' 压缩到 87.5%
        ''' </summary>
        SemiCondensed = 4
        ''' <summary>
        ''' 正常字型缩放
        ''' </summary>
        Normal = 5
        ''' <summary>
        ''' 拉伸到 112.5%
        ''' </summary>
        SemiExpanded = 6
        ''' <summary>
        ''' 拉伸到 125%
        ''' </summary>
        Expanded = 7
        ''' <summary>
        ''' 拉伸到 150%
        ''' </summary>
        ExtraExpanded = 8
        ''' <summary>
        ''' 拉伸到 200%
        ''' </summary>
        UltraExpanded = 9
    End Enum

End Namespace
