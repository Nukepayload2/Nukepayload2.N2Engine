Namespace Input
    ''' <summary>
    ''' 管理键盘状态。各个平台实现需要同步键盘状态。
    ''' </summary>
    Public Class KeyboardStateManager

        Shared _PrimaryKeyboard As KeyboardStateManager
        ''' <summary>
        ''' 主键盘的按键状态管理器。
        ''' </summary>
        Public Shared ReadOnly Property PrimaryKeyboard As KeyboardStateManager
            Get
                If _PrimaryKeyboard Is Nothing Then
                    _PrimaryKeyboard = New KeyboardStateManager
                End If
                Return _PrimaryKeyboard
            End Get
        End Property

        Dim _keyStates(255) As PhysicalKeyStatus
        ''' <summary>
        ''' 获取当前按键状态。
        ''' </summary>
        ''' <param name="key">按键的虚拟键码。</param>
        Public ReadOnly Property KeyState(key As Key) As PhysicalKeyStatus
            Get
                Return _keyStates(key)
            End Get
        End Property
        ''' <summary>
        ''' 平台特定获取对按键状态值的引用，可以设置按键状态。
        ''' </summary>
        Friend ReadOnly Property WritableKeyState As PhysicalKeyStatus()
            Get
                Return _keyStates
            End Get
        End Property
        ''' <summary>
        ''' 判断某个按键是否按下
        ''' </summary>
        Public ReadOnly Property IsKeyDown(key As Key) As Boolean
            Get
                Return Not _keyStates(key).IsKeyReleased AndAlso _keyStates(key).RepeatCount > 0
            End Get
        End Property
        ''' <summary>
        ''' 查询按下的按键。
        ''' </summary>
        ''' <returns></returns>
        Public Iterator Function GetPressedKeys() As IEnumerable(Of Key)
            For i = 0 To 255
                Dim k = CType(i, Key)
                If IsKeyDown(k) Then
                    Yield k
                End If
            Next
        End Function
    End Class

End Namespace