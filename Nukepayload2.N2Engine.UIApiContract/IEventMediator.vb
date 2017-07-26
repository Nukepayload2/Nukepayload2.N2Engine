Imports Nukepayload2.N2Engine.UI.Elements
Namespace UI
    ''' <summary>
    ''' 检测输入设备状态，并引发输入事件。
    ''' </summary>
    Public Interface IEventMediator
        ''' <summary>
        ''' 检测的是哪个画布。
        ''' </summary>
        Property AttachedView As GameCanvas
        ''' <summary>
        ''' 如果状态变化，则引发对应的输入事件。
        ''' </summary>
        Sub TryRaiseEvent()
    End Interface

End Namespace
