Namespace Input
    ''' <summary>
    ''' 物理按键状态。可以利用指针强制转换成 Windows.UI.Core.CorePhysicalKeyStatus。
    ''' </summary>
    Public Structure PhysicalKeyStatus
        ''' <summary>
        ''' 曾被按下的次数。
        ''' </summary>
        Public RepeatCount As UInteger
        ''' <summary>
        ''' 扫描代码。
        ''' </summary>
        Public ScanCode As UInteger
        ''' <summary>
        ''' 是否映射到扩展的 ASCII 字符。
        ''' </summary>
        Public IsExtendedKey As Boolean
        ''' <summary>
        ''' 菜单键是否处于按下状态。
        ''' </summary>
        Public IsMenuKeyDown As Boolean
        ''' <summary>
        ''' 键当前是否处于按下状态。
        ''' </summary>
        Public WasKeyDown As Boolean
        ''' <summary>
        ''' 键是否从按下转为释放状态。
        ''' </summary>
        Public IsKeyReleased As Boolean
    End Structure
End Namespace