Namespace UI
    Public Interface IGameAction
        ''' <summary>
        ''' 开始执行无条件的动作
        ''' </summary>
        Sub BeginAsync()
    End Interface
    ''' <summary>
    ''' 在启动后等待 BeginTime 指定的时间之后执行
    ''' </summary>
    Public Interface ITimeAction
        Inherits IGameAction
        Property BeginTime As TimeSpan
        Sub Action()
        Sub Cancel()
    End Interface
End Namespace
