Namespace Input
    ''' <summary>
    ''' 指定用于修改另一个键的虚拟键。可以强制转换成 Windows.System.VirtualKeyModifiers。
    ''' </summary>
    <Flags>
    Public Enum VirtualKeyModifiers
        None
        Control
        Menu
        Shift = 4
        Windows = 8
    End Enum

End Namespace