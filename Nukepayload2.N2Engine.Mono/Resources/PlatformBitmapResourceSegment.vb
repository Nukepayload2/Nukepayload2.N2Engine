Imports Microsoft.Xna.Framework.Graphics
Imports Nukepayload2.N2Engine.Platform

<RegistrationIgnore>
Friend Class PlatformBitmapResourceSegment
    Inherits PlatformBitmapResource

    Public Sub New(parent As PlatformBitmapResource)
        MyBase.New(parent.UriPath)
        _parent = parent
    End Sub

    Dim _parent As PlatformBitmapResource

    Public Overrides Property Texture As Texture2D
        Get
            Return _parent.Texture
        End Get
        Set(value As Texture2D)
            _parent.Texture = value
        End Set
    End Property
End Class
