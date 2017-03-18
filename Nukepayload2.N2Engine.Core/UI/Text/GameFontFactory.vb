Imports Nukepayload2.N2Engine.Foundation

Namespace UI.Text

    Public Class GameFontFactory

        Public Function Create() As GameFont
            Return New GameFont(Name, FontSize, FontFamily, FontStretch, FontWeight, FontStyle,
                                IsColorFontEnabled, New Uri(SpriteFontResourceId), Color)
        End Function

        Public Property IsColorFontEnabled As Boolean
        Public Property FontStyle As FontStyle
        Public Property FontWeight As FontWeight
        Public Property FontStretch As FontStretch
        Public Property Name As String
        Public Property FontSize As Single
        Public Property FontFamily As String
        Public Property SpriteFontResourceId As String
        Public Property Color As Color

    End Class

End Namespace