Imports Nukepayload2.N2Engine.Platform

Namespace Resources

    Public Class FontLoaderFactory

        Public Shared Function Create() As IFontLoader
            Return PlatformActivator.CreateBaseInstance(Of IFontLoader)
        End Function

    End Class
End Namespace
