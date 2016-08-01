Namespace Media
    Public Module AsyncHelper
        ''' <summary>
        ''' 追踪此异步操作，等待直到它运行完毕。
        ''' </summary>
        <Extension>
        Public Function Track(Of TResult)(operation As IAsyncOperation(Of TResult)) As TResult
            Return operation.AsTask().Track
        End Function
        ''' <summary>
        ''' 追踪此异步操作，等待直到它运行完毕。
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
        ''' 追踪此异步操作，等待直到它运行完毕。
        ''' </summary>
        <Extension>
        Public Sub Track(operation As IAsyncAction)
            operation.AsTask().Track
        End Sub
        ''' <summary>
        ''' 追踪此异步操作，等待直到它运行完毕。
        ''' </summary>
        <Extension>
        Public Sub Track(operation As Task)
            operation.ContinueWith(Function(p) True).Wait()
        End Sub
    End Module
End Namespace