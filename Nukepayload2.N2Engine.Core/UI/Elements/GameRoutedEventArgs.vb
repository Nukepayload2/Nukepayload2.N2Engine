Namespace UI.Elements
    ''' <summary>
    ''' 游戏中的路由事件
    ''' </summary>
    Public Class GameRoutedEventArgs
        Inherits EventArgs

        ''' <summary>
        ''' 初始化路由的起点发生的事件附带的参数
        ''' </summary>
        Sub New()

        End Sub
        ''' <summary>
        ''' 初始化路由事件附带的参数
        ''' </summary>
        Sub New(originalSource As GameObject)
            Me.OriginalSource = originalSource
        End Sub

        ''' <summary>
        ''' 引发这个事件的源头。对于有模板的对象（例如 <see cref="GameScene"/>），通常源头会与模板相关。路由的起点此属性返回空。
        ''' </summary>
        ''' <returns>引发事件的游戏对象</returns>
        Public ReadOnly Property OriginalSource As GameObject

        ''' <summary>
        ''' 这个路由事件是否已经处理过。可以选择性地忽略已经处理过的事件。
        ''' </summary>
        ''' <returns>是否已经处理过这个事件。</returns>
        Public Property Handled As Boolean
    End Class

End Namespace