Namespace Resources
    ''' <summary>
    ''' 代表位图资源
    ''' </summary>
    Public MustInherit Class BitmapResource
        Inherits GameResourceBase

        Sub New(uriPath As Uri)
            Me.UriPath = uriPath
        End Sub
        ''' <summary>
        ''' 位图的路径
        ''' </summary>
        Public Property UriPath As Uri

    End Class
End Namespace