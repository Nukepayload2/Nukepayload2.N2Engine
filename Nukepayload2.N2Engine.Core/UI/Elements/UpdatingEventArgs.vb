Namespace UI.Elements
    Public Class UpdatingEventArgs
        Inherits EventArgs

        Sub New(elapsedTime As TimeSpan, totalTime As TimeSpan, isRunningSlowly As Boolean)
            Me.ElapsedTime = elapsedTime
            Me.TotalTime = totalTime
            Me.IsRunningSlowly = isRunningSlowly
        End Sub

        ''' <summary>
        ''' 上次更新到这次的时间
        ''' </summary>
        Public ReadOnly Property ElapsedTime As TimeSpan
        ''' <summary>
        ''' 总的游戏运行时间
        ''' </summary>
        Public ReadOnly Property TotalTime As TimeSpan
        ''' <summary>
        ''' 是否比设定的时间缓慢
        ''' </summary>
        Public ReadOnly Property IsRunningSlowly As Boolean

    End Class
End Namespace
