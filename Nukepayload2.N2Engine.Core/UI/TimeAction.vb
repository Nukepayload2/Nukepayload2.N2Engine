Namespace UI
    Public MustInherit Class TimeAction
        Inherits GameObject
        Implements ITimeAction

        ''' <summary>
        ''' 开始的时间
        ''' </summary>
        Public Property BeginTime As TimeSpan Implements ITimeAction.BeginTime
        ''' <summary>
        ''' 开始此动作
        ''' </summary>
        Public MustOverride Sub Begin() Implements ITimeAction.Begin
    End Class
End Namespace