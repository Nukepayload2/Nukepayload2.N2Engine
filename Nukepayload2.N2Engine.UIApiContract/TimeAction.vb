Imports System.Threading

Namespace UI
    ''' <summary>
    ''' 延时执行的动作的基类
    ''' </summary>
    Public MustInherit Class TimeAction
        Inherits GameObject
        Implements ITimeAction

        Dim _delayComplete As Boolean

        Dim _cancelTokenSource As New CancellationTokenSource

        Sub New(beginTime As TimeSpan)
            Me.BeginTime = beginTime
        End Sub

        Public Property BeginTime As TimeSpan Implements ITimeAction.BeginTime
        ''' <summary>
        ''' 延迟时间之后执行的命令。
        ''' </summary>
        Public MustOverride Sub Action() Implements ITimeAction.Action

        ''' <summary>
        ''' 在 BeginTime 之后会执行 Action。
        ''' </summary>
        Public Async Sub BeginAsync() Implements ITimeAction.BeginAsync
            Try
                Await Task.Delay(BeginTime, _cancelTokenSource.Token)
                _delayComplete = True
                Action()
            Catch ex As TaskCanceledException
                _delayComplete = True
            End Try
        End Sub

        Public Sub Cancel() Implements ITimeAction.Cancel
            If Not _delayComplete Then
                _cancelTokenSource.Cancel()
            End If
        End Sub
    End Class
End Namespace