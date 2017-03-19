Public Class SparksViewModel
    Inherits SingleInstance(Of SparksViewModel)

    Public ReadOnly Property JumpLogicStatusText As String = $"获取中。"

    Public Property ScrollingViewer As New ScrollingViewer

    Public ReadOnly Property CharacterSheet As New CharacterSheet

    Public Property ButtonStatus As New ButtonStatus

End Class