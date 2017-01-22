Namespace UI.Text
    ''' <summary>
    ''' 游戏中的矢量字体
    ''' </summary>
    Public Class GameFont
        ''' <summary>
        ''' 字体的名称。
        ''' </summary>
        Public Property Name As String
        ''' <summary>
        ''' 字体大小。
        ''' </summary>
        Public Property FontSize As Single
        ''' <summary>
        ''' 指示字体是哪个系列的。
        ''' </summary>
        Public Property FontFamily As String
        ''' <summary>
        ''' 描述与某个字体与该字体的正常纵横比相比的拉伸程度。
        ''' </summary>
        Public Property FontStretch As FontStretch
        ''' <summary>
        ''' 字体笔画的粗细。
        ''' </summary>
        Public Property FontWeight As FontWeight
        ''' <summary>
        ''' 表示字体的样式（例如普通或斜体）。
        ''' </summary>
        Public Property FontStyle As FontStyle
        ''' <summary>
        ''' 是否使用彩色字体。
        ''' </summary>
        Public Property IsColorFontEnabled As Boolean
    End Class
End Namespace
