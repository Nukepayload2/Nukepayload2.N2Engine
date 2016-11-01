Imports Nukepayload2.N2Engine.Platform

Namespace Resources
    ''' <summary>
    ''' 代表平台特定的位图资源
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
        ''' <summary>
        ''' 从 Uri 创建平台特定的位图资源
        ''' </summary>
        Public Shared Function Create(uriPath As Uri) As BitmapResource
            Return PlatformActivator.CreateBaseInstance(Of BitmapResource)(uriPath)
        End Function
    End Class
End Namespace