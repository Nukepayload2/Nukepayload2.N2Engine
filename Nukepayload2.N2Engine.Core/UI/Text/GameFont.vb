Imports Nukepayload2.N2Engine.Foundation
Imports Nukepayload2.UI.SpriteFonts

Namespace UI.Text
    ''' <summary>
    ''' 游戏中的字体
    ''' </summary>
    Public Class GameFont
        Sub New(name As String, fontSize As Single, fontFamily As String, fontStretch As FontStretch, fontWeight As FontWeight, fontStyle As FontStyle, isColorFontEnabled As Boolean, spriteFontResourceId As Uri, color As Color)
            Me.Name = name
            Me.FontSize = fontSize
            Me.FontFamily = fontFamily
            Me.FontStretch = fontStretch
            Me.FontWeight = fontWeight
            Me.FontStyle = fontStyle
            Me.IsColorFontEnabled = isColorFontEnabled
            Me.SpriteFontResourceId = spriteFontResourceId
            Me.Color = color
        End Sub
        ''' <summary>
        ''' 字体的颜色
        ''' </summary>
        Public ReadOnly Property Color As Color
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
        ''' 贴图字体的资源 Id。在 MonoGame 实现必须使用贴图字体，而发行到 Win2D 则没有这个限制。
        ''' </summary>
        Public ReadOnly Property SpriteFontResourceId As Uri
        ''' <summary>
        ''' (仅 Mono Game) 贴图字体数据。
        ''' </summary>
        Public Property SpriteData As N2SpriteFont
        ''' <summary>
        ''' 还原成字体工厂，以便创建相似的字体。
        ''' </summary>
        Public Function Uncraft() As GameFontFactory
            Return New GameFontFactory With {
                .Color = Color,
                .FontFamily = FontFamily,
                .FontSize = FontSize,
                .FontStretch = FontStretch,
                .FontStyle = FontStyle,
                .FontWeight = FontWeight,
                .IsColorFontEnabled = IsColorFontEnabled,
                .Name = Name,
                .SpriteFontResourceId = SpriteFontResourceId.ToString
            }
        End Function
    End Class
End Namespace
