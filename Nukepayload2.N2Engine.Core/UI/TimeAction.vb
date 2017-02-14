Namespace UI
    Public MustInherit Class TimeAction
        Inherits GameObject
        Implements ITimeAction

        ''' <summary>
        ''' 开始的时间
        ''' </summary>
        Public Property BeginTime As TimeSpan Implements ITimeAction.BeginTime
        ''' <summary>
        ''' 开始后的动作
        ''' </summary>
        Public MustOverride Sub Action() Implements ITimeAction.Action
        ''' <summary>
        ''' 开始此动作
        ''' </summary>
        Public Async Sub BeginAsync() Implements ITimeAction.BeginAsync
            Await Task.Delay(BeginTime)
            Action()
        End Sub
    End Class
End Namespace