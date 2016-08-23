Namespace Battle
    Public Class MilitaryOfficerCampPicker
        Implements ICampManger
        Dim _RegisteredCamp As New List(Of ICamp)
        Sub New()
            _RegisteredCamp.AddRange({New 平民})
        End Sub
        Public ReadOnly Property RegisteredCamp As IList(Of ICamp) Implements ICampManger.RegisteredCamp
            Get
                Return _RegisteredCamp
            End Get
        End Property
        Public ReadOnly Property SkirmishCamp As IEnumerable(Of ICamp) Implements ICampManger.SkirmishCamp
            Get
                Return From c In RegisteredCamp Where c.SkirmishAvailable
            End Get
        End Property
    End Class

End Namespace
