Imports Microsoft.Xna.Framework
Imports Nukepayload2.N2Engine.Renderers
Imports Nukepayload2.N2Engine.UI.Elements

Public MustInherit Class GameElementRenderer(Of T As GameElement)
    Inherits RendererBase(Of T)

    Public Sub New(view As T)
        MyBase.New(view)
        AddHandler view.HandleRendererRequested, AddressOf OnHandleRendererRequested
        AddHandler view.RendererUnloadRequested, AddressOf OnRendererUnhandleRequested
    End Sub

    Private Sub OnRendererUnhandleRequested(sender As Object, e As RendererRegistrationRequestedEventArgs)
        With DirectCast(e.ParentRenderer, GameCanvasRenderer).Game
            RemoveHandler .CreateResources, AddressOf OnCreateResources
            RemoveHandler .Drawing, AddressOf OnDraw
            RemoveHandler .GameLoopStarting, AddressOf OnGameLoopStarting
            RemoveHandler .GameLoopEnded, AddressOf OnGameLoopStopped
            RemoveHandler .Updating, AddressOf OnUpdate
        End With
    End Sub

    Protected MustOverride Sub OnGameLoopStarting(sender As Game, args As Object)
    Protected MustOverride Sub OnDraw(sender As Game, args As MonogameDrawEventArgs)
    Protected MustOverride Sub OnUpdate(sender As Game, args As MonogameUpdateEventArgs)
    Protected MustOverride Sub OnGameLoopStopped(sender As Game, args As Object)
    Protected MustOverride Sub OnCreateResources(sender As Game, args As MonogameCreateResourcesEventArgs)

    Private Sub OnHandleRendererRequested(sender As Object, e As RendererRegistrationRequestedEventArgs)
        With DirectCast(e.ParentRenderer, GameCanvasRenderer).Game
            AddHandler .CreateResources, AddressOf OnCreateResources
            AddHandler .Drawing, AddressOf OnDraw
            AddHandler .GameLoopStarting, AddressOf OnGameLoopStarting
            AddHandler .GameLoopEnded, AddressOf OnGameLoopStopped
            AddHandler .Updating, AddressOf OnUpdate
        End With
    End Sub

End Class
