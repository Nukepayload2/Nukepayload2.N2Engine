Namespace UI.Controls

    Public Class GameButtonContentTemplate
        Implements IControlTemplate

        Public Function CreateContent() As GameButtonContent
            Return New GameButtonContent
        End Function

        Public Function IControlTemplate_CreateContent() As GameTemplatedContent Implements IControlTemplate.CreateContent
            Return CreateContent()
        End Function
    End Class
End Namespace
