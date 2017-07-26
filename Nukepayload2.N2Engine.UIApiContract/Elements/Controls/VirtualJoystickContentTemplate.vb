Namespace UI.Controls
    ''' <summary>
    ''' 虚拟摇杆内容的模板
    ''' </summary>
    Public Class VirtualJoystickContentTemplate
        Implements IControlTemplate

        Public Function CreateContent() As VirtualJoystickContent
            Return New VirtualJoystickContent
        End Function

        Public Function IControlTemplate_CreateContent() As GameTemplatedContent Implements IControlTemplate.CreateContent
            Return CreateContent()
        End Function
    End Class

End Namespace
