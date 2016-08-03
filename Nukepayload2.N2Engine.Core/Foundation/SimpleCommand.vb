Imports System.Windows.Input

Public Class SimpleCommand
    Implements ICommand, IGameCommand

    Sub New(exec As Action)
        Me.Exec = exec
    End Sub

    Public ReadOnly Property Exec As Action

    Public Event CanExecuteChanged As EventHandler Implements ICommand.CanExecuteChanged

    Public Sub Execute() Implements IGameCommand.Execute
        Exec.Invoke
    End Sub

    Public Sub Execute(parameter As Object) Implements ICommand.Execute
        Execute()
    End Sub

    Public Function CanExecute(parameter As Object) As Boolean Implements ICommand.CanExecute
        Return Exec IsNot Nothing
    End Function
End Class
