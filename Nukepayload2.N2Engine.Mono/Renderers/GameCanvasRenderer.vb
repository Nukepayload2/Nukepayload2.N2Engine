Imports System.Collections.Specialized
Imports Microsoft.Xna.Framework
Imports Nukepayload2.N2Engine.Renderers
Imports Nukepayload2.N2Engine.UI.Elements

Public Class GameCanvasRenderer
    Inherits RendererBase(Of GameCanvas)

    Public WithEvents Game As MonoGameHandler
    Sub New(view As GameCanvas, game As MonoGameHandler)
        MyBase.New(view)
        Me.Game = game
        HandleNewElements(view)
        AddHandler view.Children.CollectionChanged, AddressOf OnChildrenChanged
    End Sub

    ''' <summary>
    ''' 更新Renderer的事件订阅
    ''' </summary>
    Protected Overridable Sub OnChildrenChanged(sender As Object, e As NotifyCollectionChangedEventArgs)
        If e.NewItems IsNot Nothing Then
            HandleNewElements(View)
        End If
        If e.OldItems IsNot Nothing Then
            For Each oldItems As GameElement In e.OldItems
                oldItems.UnloadRenderer(Me)
            Next
        End If
    End Sub

    Private Sub HandleNewElements(view As GameCanvas)
        For Each newItems As GameElement In view.Children
            newItems.HandleRenderer(Me)
        Next
    End Sub

    Private Sub Game_GameLoopEnded(sender As Game, args As Object) Handles Game.GameLoopEnded
        View.Children.Clear()
    End Sub

    Public Overrides Sub DisposeResources()

    End Sub
End Class
