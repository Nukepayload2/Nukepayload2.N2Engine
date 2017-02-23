Imports Nukepayload2.N2Engine.Platform
Imports Nukepayload2.N2Engine.Resources

<PlatformImpl(GetType(BitmapResource))>
Partial Friend Class PlatformBitmapResource
    Inherits BitmapResource
    Implements IDisposable

    Sub New(uriPath As Uri)
        MyBase.New(uriPath)
    End Sub

    Partial Private Sub ReleaseBitmap()

    End Sub

    Protected Overrides Function ClonePreserveTexture() As BitmapResource
        Return New PlatformBitmapResource(UriPath) With {.Texture = Texture}
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                ReleaseBitmap()
            End If
        End If
        disposedValue = True
    End Sub
    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
    End Sub
#End Region
End Class