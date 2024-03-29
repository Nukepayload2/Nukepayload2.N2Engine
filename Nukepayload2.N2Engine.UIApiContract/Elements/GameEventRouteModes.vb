﻿Namespace UI.Elements
    ''' <summary>
    ''' 事件路由模式
    ''' </summary>
    Public Enum GameEventRouteModes
        ''' <summary>
        ''' 事件不进行路由，导致事件仅在 <see cref="GameCanvas"/> 可用。元素上关联的事件触发器也会失效。
        ''' </summary>
        Disabled
        ''' <summary>
        ''' 如果 <see cref="GameRoutedEventArgs.Handled"/> 为真，则事件不再传播。
        ''' </summary>
        NonHandledEvents
        ''' <summary>
        ''' 即使 <see cref="GameRoutedEventArgs.Handled"/> 为真，也继续传播。
        ''' </summary>
        HandledEventsToo
        ''' <summary>
        ''' (未实施) 显式指定事件要路由到哪个可见对象。
        ''' </summary>
        Explicit
    End Enum
End Namespace
