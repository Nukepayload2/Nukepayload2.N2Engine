Namespace UI
    Public Interface IGameAction
        Sub Begin()
    End Interface
    ''' <summary>
    ''' 在启动后等待 BeginTime 指定的时间之后
    ''' </summary>
    Public Interface ITimeAction
        Inherits IGameAction
        Property BeginTime As TimeSpan
    End Interface
End Namespace
