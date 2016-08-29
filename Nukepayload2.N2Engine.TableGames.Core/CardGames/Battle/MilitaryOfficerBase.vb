Imports Nukepayload2.N2Engine.Foundation

Namespace Battle

    Public MustInherit Class MilitaryOfficerBase
        Inherits CardCommandBase
        Implements IMilitaryOfficer
        ''' <summary>
        ''' MilitaryOfficerAttack时的Sound。如果只有一个Sound，命Name规则为: *Attack。Enabled多Sound支持时末尾附加FromValue1Start的数字。
        ''' </summary>
        ''' <returns></returns>
        Public Overridable ReadOnly Property UseSound As IEnumerable(Of Stream) Implements ICard.UseSound
            Get
                Return LoadSounds("Attack")
            End Get
        End Property

        Public Overridable ReadOnly Property Cursor As Uri Implements ICard.Cursor
            Get
                Return Nothing
            End Get
        End Property
        Public Overridable ReadOnly Property Name As String = [GetType].Name Implements ICard.Name

        ''' <summary>
        ''' MilitaryOfficer缩略Icon。命Name规则为: *Icon_s
        ''' </summary>
        ''' <returns></returns>
        Public Overridable ReadOnly Property Icon As Uri Implements ICard.Icon
            Get
                Return LoadImage("Icon_s")
            End Get
        End Property
        ''' <summary>
        ''' MilitaryOfficerIcon。命Name规则为: *Icon
        ''' </summary>
        ''' <returns></returns>
        Public Overridable ReadOnly Property LargeIcon As Uri Implements ICard.LargeIcon
            Get
                Return LoadImage("Icon")
            End Get
        End Property
        Public Overridable ReadOnly Property IsModCard As Boolean = False Implements ICard.IsModCard
        ''' <summary>
        ''' MilitaryOfficerDeath时的Sound。如果只有一个Sound，命Name规则为: *Die。Enabled多Sound支持时末尾附加FromValue1Start的数字。
        ''' </summary>
        ''' <returns></returns>
        Public Overridable ReadOnly Property DeathSound As IEnumerable(Of Stream) Implements ICard.DeathSound
            Get
                Return LoadSounds("Die")
            End Get
        End Property

        Public Property DisposeResourcesAbilities As IList(Of IMilitaryOfficerAbility) = New List(Of IMilitaryOfficerAbility) Implements IMilitaryOfficer.DisposeResourcesAbilities

        Public Overridable ReadOnly Property BriefDescription As String Implements ICard.BriefDescription
            Get
                Return GetString(NameOf(BriefDescription))
            End Get
        End Property
        ''' <summary>
        ''' MilitaryOfficerBeSelect时的Sound。如果只有一个Sound，命Name规则为: *Select。Enabled多Sound支持时末尾附加FromValue1Start的数字。
        ''' </summary>
        ''' <returns></returns>
        Public Overridable ReadOnly Property SelectSound As IEnumerable(Of Stream) Implements ICard.SelectSound
            Get
                Return LoadSounds("Select")
            End Get
        End Property
        Public Overridable ReadOnly Property ExtendedDescription As String Implements ICard.ExtendedDescription
            Get
                Return GetString(NameOf(ExtendedDescription))
            End Get
        End Property
        Public MustOverride Property Camp As ICamp Implements IMilitaryOfficer.Camp
        Public ReadOnly Property MarkAera As IMarkAera Implements IMilitaryOfficer.MarkAera
        Public ReadOnly Property HealthBar As IHealthBarControl Implements IMilitaryOfficer.HealthBar
        Public ReadOnly Property PowerBar As IPowerBar Implements IMilitaryOfficer.PowerBar
        Public ReadOnly Property NameLabel As INameLabel Implements IMilitaryOfficer.NameLabel

        Public Overloads Sub EnabledCommand(MarkAera As IMarkAera, HealthBar As IHealthBarControl, PowerBar As IPowerBar, NameLabel As INameLabel,
                           BackSurfaceUp As PropertyBinder(Of Boolean),
                           BeSelected As PropertyBinder(Of Boolean),
                           Enabled As PropertyBinder(Of Boolean),
                           OnClicked As Func(Of Task)) Implements IMilitaryOfficerCommand.EnabledCommand
            _MarkAera = MarkAera
            _HealthBar = HealthBar
            _PowerBar = PowerBar
            _NameLabel = NameLabel
            MyBase.EnabledCommand(BackSurfaceUp, BeSelected, Enabled, OnClicked)
        End Sub
        Sub New(DefaultAbility As IEnumerable(Of IMilitaryOfficerAbility))
            For Each s In DefaultAbility
                DisposeResourcesAbilities.Add(s)
            Next
        End Sub

        Public Overridable Async Function Initialized() As Task Implements ICard.Initialized
            Await DoNothingTask()
        End Function

        Public Overridable Async Function DisposeResources() As Task Implements ICard.DisposeResources
            Await DoNothingTask()
        End Function

    End Class
End Namespace