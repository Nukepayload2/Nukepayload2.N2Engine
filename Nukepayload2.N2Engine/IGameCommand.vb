Namespace Foundation
    ''' <summary>
    ''' 无需参数的命令。在模型中定义，绑定到视图上。
    ''' </summary>
    Public Interface IGameCommand
        Sub Execute()
    End Interface
    ''' <summary>
    ''' 带有1个参数的命令。在模型中定义，绑定到视图上。
    ''' </summary>
    Public Interface IGameCommand(Of T1)
        Sub Execute(arg1 As T1)
    End Interface
    ''' <summary>
    ''' 带有2个参数的命令。在模型中定义，绑定到视图上。
    ''' </summary>
    Public Interface IGameCommand(Of T1, T2)
        Sub Execute(arg1 As T1, arg2 As T2)
    End Interface
    ''' <summary>
    ''' 带有3个参数的命令。在模型中定义，绑定到视图上。
    ''' </summary>
    Public Interface IGameCommand(Of T1, T2, T3)
        Sub Execute(arg1 As T1, arg2 As T2, arg3 As T3)
    End Interface
    ''' <summary>
    ''' 带有4个参数的命令。在模型中定义，绑定到视图上。
    ''' </summary>
    Public Interface IGameCommand(Of T1, T2, T3, T4)
        Sub Execute(arg1 As T1, arg2 As T2, arg3 As T3, arg4 As T4)
    End Interface
    ''' <summary>
    ''' 带有5个参数的命令。在模型中定义，绑定到视图上。
    ''' </summary>
    Public Interface IGameCommand(Of T1, T2, T3, T4, T5)
        Sub Execute(arg1 As T1, arg2 As T2, arg3 As T3, arg4 As T4, arg5 As T5)
    End Interface
    ''' <summary>
    ''' 带有6个参数的命令。在模型中定义，绑定到视图上。
    ''' </summary>
    Public Interface IGameCommand(Of T1, T2, T3, T4, T5, T6)
        Sub Execute(arg1 As T1, arg2 As T2, arg3 As T3, arg4 As T4, arg5 As T5, arg6 As T6)
    End Interface
    ''' <summary>
    ''' 带有7个参数的命令。在模型中定义，绑定到视图上。
    ''' </summary>
    Public Interface IGameCommand(Of T1, T2, T3, T4, T5, T6, T7)
        Sub Execute(arg1 As T1, arg2 As T2, arg3 As T3, arg4 As T4, arg5 As T5, arg6 As T6, arg7 As T7)
    End Interface
    ''' <summary>
    ''' 带有8个参数的命令。在模型中定义，绑定到视图上。
    ''' </summary>
    Public Interface IGameCommand(Of T1, T2, T3, T4, T5, T6, T7, T8)
        Sub Execute(arg1 As T1, arg2 As T2, arg3 As T3, arg4 As T4, arg5 As T5, arg6 As T6, arg7 As T7, arg8 As T8)
    End Interface
End Namespace