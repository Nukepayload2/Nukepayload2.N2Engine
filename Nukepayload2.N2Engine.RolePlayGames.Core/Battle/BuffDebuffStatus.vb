Imports Nukepayload2.N2Engine.Core

Public Class BuffDebuffStatus
    Inherits GameResourceModelBase

    Public Property IsActive As Boolean
    Public Property IsEnabled As Boolean
    Public Property Duration As Date
    Public Property Priority As Integer
    Public Property IntervalMillsec As Integer
    Public Property TimeElapsed As Date
End Class