Imports Nukepayload2.N2Engine.Core

Namespace Battle
    Public MustInherit Class CampBase
        Inherits UseDefaultResourcePackObject
        Implements ICamp

        Public Overridable ReadOnly Property Name As String Implements ICamp.Name
            Get
                Return Me.GetType.Name
            End Get
        End Property
        Public MustOverride ReadOnly Property DefaultAbility As IList(Of IMilitaryOfficerAbility) Implements ICamp.DefaultAbility
        Public Overridable ReadOnly Property LargeIcon As Uri Implements ICamp.LargeIcon
            Get
                Return LoadImage("Icon")
            End Get
        End Property
        Public Overridable ReadOnly Property ThumbnailIcon As Uri Implements ICamp.ThumbnailIcon
            Get
                Return LoadImage("Flag")
            End Get
        End Property
        Dim _Cards As New List(Of IHandCard)
        Public ReadOnly Property OwnedHandCard As IList(Of IHandCard) = _Cards Implements ICamp.OwnedHandCard
        Public MustOverride ReadOnly Property BriefDescription As String Implements ICamp.BriefDescription
        Public MustOverride ReadOnly Property ExtendedDescription As String Implements ICamp.ExtendedDescription
        Public MustOverride ReadOnly Property DefaultCampColorArgb As Integer Implements ICamp.DefaultCampColorArgb
        Public Overridable ReadOnly Property SkirmishAvailable As Boolean = True Implements ICamp.SkirmishAvailable
        Dim _Commanders As New List(Of IMilitaryOfficer)
        Public ReadOnly Property OwnedMilitaryOfficer As IList(Of IMilitaryOfficer) = _Commanders Implements ICamp.OwnedMilitaryOfficer
    End Class

    Public Class 平民
        Inherits CampBase
        Sub New()
            With OwnedHandCard
                .Add(New Dodge)
                .Add(New Repair)
                .Add(New Engineer)
                .Add(New AoeInvide)
                .Add(New AoeAttak)
            End With
        End Sub
        Sub New(ovld As Boolean)
        End Sub
        Public Overrides ReadOnly Property Uid As String = "Civilian"
        Public Overrides ReadOnly Property SkirmishAvailable As Boolean = False
        Public Overrides ReadOnly Property DefaultAbility As IList(Of IMilitaryOfficerAbility) = {}
        Public Overrides ReadOnly Property BriefDescription As String = "平民CampUse所有CommonCard"
        Public Overrides ReadOnly Property ExtendedDescription As String = "平民CampUse所有CommonCard，通常UnableTo再SkirmishUse。"
        Public Overrides ReadOnly Property DefaultCampColorArgb As Integer
            Get
                Return ColorValues.Gray
            End Get
        End Property
    End Class
End Namespace
