Imports Nukepayload2.N2Engine.Platform
Imports Nukepayload2.N2Engine.UI.Text

Namespace Resources

    Public Class FontLoaderFactory

        Public Shared Function Create() As IFontLoader
            Return PlatformActivator.CreateBaseInstance(Of IFontLoader)
        End Function

    End Class
End Namespace
