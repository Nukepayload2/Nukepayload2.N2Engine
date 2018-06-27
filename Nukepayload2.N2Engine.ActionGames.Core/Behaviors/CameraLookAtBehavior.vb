Imports System.Numerics
Imports Nukepayload2.N2Engine.Behaviors
Imports Nukepayload2.N2Engine.Foundation
Imports Nukepayload2.N2Engine.UI.Elements

Public Class CameraLookAtBehavior
    Inherits GameBehavior

    Dim _scrollViewer As GameVirtualizingScrollViewer
    Dim _primaryCharacter As GameEntity
    Dim _oldBinder As PropertyBinder(Of Vector2)

    Public Sub New(scrollViewer As GameVirtualizingScrollViewer)
        _scrollViewer = scrollViewer
    End Sub

    Public Overloads Sub Attach(visual As GameEntity)
        MyBase.Attach(visual)
        _primaryCharacter = visual
        _oldBinder = _scrollViewer.Offset
        _scrollViewer.Offset = New RelayPropertyBinder(Of Vector2)(
            Function()
                Dim loc = _primaryCharacter.Location.Value
                Dim screenSize = Information.BackBufferInformation.ViewPortSize.ToVector2
                Dim center = screenSize / 2
                Return loc - center
            End Function)
    End Sub

    Public Overrides Sub Remove(visual As GameVisual)
        MyBase.Remove(visual)
        _scrollViewer.Offset = _oldBinder
    End Sub
End Class
