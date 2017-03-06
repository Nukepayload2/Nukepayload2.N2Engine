Namespace Models

    Public Class ProjectTemplate
        Sub New(name As String, icon As String, description As String, prepareCommand As ICommand)
            Me.Name = name
            Me.Icon = New BitmapImage(New Uri("ms-appx:///Assets/" + icon))
            Me.Description = description
            Me.PrepareCommand = prepareCommand
        End Sub

        Public Property Name$
        Public Property Icon As ImageSource
        Public Property Description$
        Public Property PrepareCommand As ICommand
    End Class

End Namespace