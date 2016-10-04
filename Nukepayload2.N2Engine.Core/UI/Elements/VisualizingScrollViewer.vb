Namespace UI.Elements
    ''' <summary>
    ''' 用于滚动查看游戏对象。如果不使用场景，则建议使用此元素作为游戏对象树的根。
    ''' </summary>
    Public Class VisualizingScrollViewer
        Inherits GameVisualContainter

        Public ReadOnly Property Offset As Vector2
        Public ReadOnly Property Zoom As Single = 1
        Public ReadOnly Property Perspective As Matrix4x4?

        Public Sub SetCurrentView(offset As Vector2?, zoom As Single?, perspective As Matrix4x4?)
            If offset IsNot Nothing Then _Offset = offset.Value
            If zoom IsNot Nothing Then _Zoom = zoom.Value
            If perspective IsNot Nothing Then _Perspective = perspective.Value
            RaiseEvent Scrolling(Me, EventArgs.Empty)
        End Sub

        Public Event Scrolling As GameObjectEventHandler(Of VisualizingScrollViewer, EventArgs)
    End Class

End Namespace
