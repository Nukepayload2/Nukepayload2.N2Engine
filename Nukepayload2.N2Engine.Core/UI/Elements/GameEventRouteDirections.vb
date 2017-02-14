Namespace UI.Elements
    ''' <summary>
    ''' (未实施) 表示输入事件路由的方向
    ''' </summary>
    Public Enum GameEventRouteDirections
        ''' <summary>
        ''' (未实施) 从 <see cref="GameCanvas"/> 路由到元素。
        ''' </summary>
        TopDown
        ''' <summary>
        ''' (未实施) 从元素路由到 <see cref="GameCanvas"/> 。
        ''' </summary>
        BottomUp
        ''' <summary>
        ''' (未实施) 从元素路由到 <see cref="GameCanvas"/> 。与 <see cref="BottomUp"/> 没区别，只是为了方便移植某些游戏而存在。
        ''' </summary>
        Preview = BottomUp
    End Enum
End Namespace
