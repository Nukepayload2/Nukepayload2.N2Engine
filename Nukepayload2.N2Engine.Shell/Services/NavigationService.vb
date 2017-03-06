Imports Nukepayload2.N2Engine.Shell.Views

Namespace Services
    Public Enum Pages
        MainPage
        DesignerPage
        AboutPage
        SettingsPage
    End Enum
    Public Class NavigationService
        Shared pageTypes As New List(Of Type) From {
            GetType(MainPage), GetType(DesignerPage),
            GetType(AboutPage), GetType(SettingsPage)
        }
        Public Shared Sub Navigate(pageType As Pages)
            GetCurrentFrame().Navigate(pageTypes(pageType))
        End Sub
        Public Shared Sub Navigate(pageType As Pages, param As Object)
            GetCurrentFrame().Navigate(pageTypes(pageType), param)
        End Sub
        Private Shared Function GetCurrentFrame() As Frame
            Return DirectCast(Window.Current.Content, Frame)
        End Function
    End Class

End Namespace