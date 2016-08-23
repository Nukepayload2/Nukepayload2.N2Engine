
Imports Nukepayload2.N2Engine.Core

Namespace Battle
    Public MustInherit Class MarkBase
        Inherits UseDefaultResourcePackObject
        Implements IMark

        Public Property Count As Integer = 1 Implements IMark.Count
        Public ReadOnly Property Abilities As IList(Of IMarkAbility) =
            New List(Of IMarkAbility) Implements IMark.Abilities
        Public Overridable ReadOnly Property Content As String Implements IMark.Content
            Get
                Return Me.GetType.ToString.Chars(0)
            End Get
        End Property
        Public Overridable Property Description As String = Me.GetType.Name.Chars(0) Implements IMark.Description
        Public Overridable ReadOnly Property Icon As Uri Implements IMark.Icon
            Get
                Return LoadImage("Icon_s")
            End Get
        End Property
        Public ReadOnly Property Enabled As New PropertyBinder(Of Boolean) Implements ICustomCommand.Enabled
        Public Overridable Sub EnabledCommand(Enabled As PropertyBinder(Of Boolean))
            _Enabled = Enabled
        End Sub
        Sub New()

        End Sub
        Sub New(Ability As IEnumerable(Of IMarkAbility))
            For Each tj In Ability
                tj.Owner = Me
                Abilities.Add(tj)
            Next
        End Sub
    End Class
End Namespace
