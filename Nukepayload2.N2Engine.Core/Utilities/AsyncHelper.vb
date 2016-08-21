Public Module AsyncHelper
    ''' <summary>
    ''' 追踪此任务，等待直到它运行完毕。
    ''' </summary>
    <Extension>
    Public Function Track(Of TResult)(operation As Task(Of TResult)) As TResult
        Dim value As TResult
        operation.ContinueWith(Function(p)
                                   value = p.Result
                                   Return True
                               End Function).Wait()
        Return value
    End Function
    ''' <summary>
    ''' 追踪此任务，等待直到它运行完毕。
    ''' </summary>
    <Extension>
    Public Sub Track(operation As Task)
        operation.ContinueWith(Function(p) True).Wait()
    End Sub
End Module