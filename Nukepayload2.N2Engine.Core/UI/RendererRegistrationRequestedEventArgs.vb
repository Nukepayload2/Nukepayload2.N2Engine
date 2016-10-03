Imports Nukepayload2.N2Engine.Renderers

Namespace UI

    ''' <summary>
    ''' 渲染器请求注册
    ''' </summary>
    Public Class RendererRegistrationRequestedEventArgs
        Inherits EventArgs

        Sub New(parentRenderer As RendererBase)
            Me.ParentRenderer = parentRenderer
        End Sub

        Public ReadOnly Property ParentRenderer As RendererBase
    End Class
End Namespace