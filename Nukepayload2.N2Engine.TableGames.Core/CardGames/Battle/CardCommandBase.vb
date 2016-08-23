Imports Nukepayload2.N2Engine.Core

Namespace Battle
    Public MustInherit Class CardCommandBase
        Inherits UseDefaultResourcePackObject
        Implements ICardCommand
        Public ReadOnly Property BackSurfaceUp As New PropertyBinder(Of Boolean) Implements ICardCommand.BackSurfaceUp
        Public ReadOnly Property BeSelected As New PropertyBinder(Of Boolean) Implements ICardCommand.BeSelected
        Public ReadOnly Property Enabled As New PropertyBinder(Of Boolean) Implements ICustomCommand.Enabled
        Public ReadOnly Property OnClicked As Func(Of Task) Implements ICardCommand.OnClicked
        Public Overridable Sub EnabledCommand(BackSurfaceUp As PropertyBinder(Of Boolean),
                                    BeSelected As PropertyBinder(Of Boolean),
                                    Enabled As PropertyBinder(Of Boolean),
                                    OnClicked As Func(Of Task)) Implements ICardCommand.EnabledCommand
            _BackSurfaceUp = BackSurfaceUp
            _BeSelected = BeSelected
            _Enabled = Enabled
            _OnClicked = OnClicked
        End Sub
    End Class
End Namespace
