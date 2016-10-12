Namespace UI
    ''' <summary>
    ''' 在启动后等待 BeginTime 指定的时间之后
    ''' </summary>
    Public Interface ITimeAction
        Property BeginTime As TimeSpan
        Sub Begin()
    End Interface
End Namespace
