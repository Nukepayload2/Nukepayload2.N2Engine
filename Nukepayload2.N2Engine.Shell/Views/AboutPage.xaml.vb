' https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

Namespace Views

    ''' <summary>
    ''' 可用于自身或导航至 Frame 内部的空白页。
    ''' </summary>
    Public NotInheritable Class AboutPage
        Inherits Page

        Private Sub BtnBack_Click(sender As Object, e As RoutedEventArgs)
            If Frame.CanGoBack Then
                Frame.GoBack()
            Else
                App.Current.Exit()
            End If
        End Sub
    End Class

End Namespace