Imports System.ComponentModel

Public Class SparksViewModel
    Inherits SingleInstance(Of SparksViewModel)
    Implements INotifyPropertyChanged

    Private _JumpLogicStatusText As String = "获取中。"

    Public Property JumpLogicStatusText As String
        Get
            Return _JumpLogicStatusText
        End Get
        Set(value As String)
            _JumpLogicStatusText = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(JumpLogicStatusText)))
        End Set
    End Property

    Public Property ScrollingViewer As New ScrollingViewer

    Public ReadOnly Property CharacterSheet As New CharacterSheet

    Public ReadOnly Property ButtonStatus As New ButtonStatus

    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged
End Class