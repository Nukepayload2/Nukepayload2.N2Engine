Namespace Threading
    ''' <summary>
    ''' 提供将一段代码运行在UI线程的能力
    ''' </summary>
    Public MustInherit Class GameDispatcher
        ''' <summary>
        ''' 让代码运行在UI线程，然后等待这个操作完成，再返回。
        ''' </summary>
        Public MustOverride Sub Invoke(callback As Action)
        ''' <summary>
        ''' 让代码运行在UI线程，并返回一个代表执行状态的任务。
        ''' </summary>
        Public MustOverride Function RunAsync(agileCallback As Action) As Task
    End Class
End Namespace