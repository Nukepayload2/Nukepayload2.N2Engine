Imports Nukepayload2.N2Engine.Foundation
Imports Nukepayload2.N2Engine.Resources

Public Class FakeBitmapResource
    Inherits BitmapResource

    Public Property Texture As Object

    Sub New(uriPath As Uri, fakeSize As SizeInInteger)
        MyBase.New(uriPath)
        PixelSize = fakeSize
    End Sub

    Public Sub Load()
        Texture = New Object
    End Sub

    Protected Overrides Function ClonePreserveTexture() As BitmapResource
        Return New FakeBitmapResource(UriPath, PixelSize) With {.Texture = Texture}
    End Function

    Public Overrides ReadOnly Property IsLoaded As Boolean
        Get
            Return Texture IsNot Nothing
        End Get
    End Property

    Public Overrides ReadOnly Property PixelSize As SizeInInteger
End Class
