Namespace UI.Text
    ''' <summary>
    ''' 游戏中的字体
    ''' </summary>
    Public MustInherit Class GameFont
        ''' <summary>
        ''' 字体的名称。
        ''' </summary>
        Public ReadOnly Property Name As String
        ''' <summary>
        ''' 字体大小。
        ''' </summary>
        Public ReadOnly Property FontSize As Single
        ''' <summary>
        ''' 指示字体是哪个系列的。
        ''' </summary>
        Public ReadOnly Property FontFamily As String
        ''' <summary>
        ''' 描述与某个字体与该字体的正常纵横比相比的拉伸程度。
        ''' </summary>
        Public ReadOnly Property FontStretch As FontStretch
        ''' <summary>
        ''' 字体笔画的粗细。
        ''' </summary>
        Public ReadOnly Property FontWeight As FontWeight
        ''' <summary>
        ''' 表示字体的样式（例如普通或斜体）。
        ''' </summary>
        Public ReadOnly Property FontStyle As FontStyle
        ''' <summary>
        ''' 是否使用彩色字体。
        ''' </summary>
        Public ReadOnly Property IsColorFontEnabled As Boolean
        ''' <summary>
        ''' 是不是位图字体。在 MonoGame 实现必须使用位图字体，而发行到 Win2D 则没有这个限制。
        ''' </summary>
        Public ReadOnly Property IsBitmapFont As Boolean
    End Class
End Namespace
