Imports Microsoft.Graphics.Canvas
Imports Nukepayload2.N2Engine.Platform

<RegistrationIgnore>
Friend Class PlatformBitmapResourceSegment
    Inherits PlatformBitmapResource

    Public Sub New(parent As PlatformBitmapResource)
        MyBase.New(parent.UriPath)
        _parent = parent
    End Sub

    Dim _parent As PlatformBitmapResource

    Public Overrides Property Texture As CanvasBitmap
        Get
            Return _parent.Texture
        End Get
        Set(value As CanvasBitmap)
            _parent.Texture = value
        End Set
    End Property
End Class
