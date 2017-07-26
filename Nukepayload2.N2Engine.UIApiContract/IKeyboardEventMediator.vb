Imports Nukepayload2.N2Engine.Input
Namespace UI
    ''' <summary>
    ''' 提供当前检测到的键盘状态
    ''' </summary>
    Public Interface IKeyboardStatusProvider
        Function GetKeyStates() As IReadOnlyList(Of PhysicalKeyStatus)
        Function GetVirtualKeyModifiers() As VirtualKeyModifiers
    End Interface
    ''' <summary>
    ''' 检测键盘状态，并引发键盘事件。
    ''' </summary>
    Public Interface IKeyboardEventMediator
        Inherits IEventMediator, IKeyboardStatusProvider

    End Interface

End Namespace
