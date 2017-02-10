Namespace UI
    ''' <summary>
    ''' 提供当前按下的触摸点的状态
    ''' </summary>
    Public Interface ITouchStatusProvider
        ReadOnly Property Points As IReadOnlyDictionary(Of UInteger, Vector2)

    End Interface

    Public Interface ITouchEventMediator
        Inherits IEventMediator, ITouchStatusProvider

    End Interface
End Namespace
