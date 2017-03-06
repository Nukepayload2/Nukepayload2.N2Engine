' https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x804 上介绍了“空白页”项模板

Imports Nukepayload2.N2Engine.Shell.ViewModels
Imports Windows.Storage

Namespace Views
    ''' <summary>
    ''' 可用于自身或导航至 Frame 内部的空白页。
    ''' </summary>
    Public NotInheritable Class MainPage
        Inherits Page

        Private Async Sub WelcomePage_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
            Dim vm = WelcomeViewModel.Current
            AdjustSize()
            Await vm.LoadAsync
            LoadProgress.IsActive = False
        End Sub

        Private Async Sub RecentListView_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles RecentListView.SelectionChanged
            If RecentListView.SelectedItem IsNot Nothing Then
                LoadProgress.IsActive = True
                Await Task.Delay(10)
                Dim f = DirectCast(RecentListView.SelectedItem, StorageFile)
                Await DirectCast(App.Current, App).LaunchFromFileAsync(f)
                LoadProgress.IsActive = False
            End If
        End Sub

        Private Sub BtnClose_Click(sender As Object, e As RoutedEventArgs) Handles BtnClose.Click
            App.Current.Exit()
        End Sub

        Enum ViewWidths
            Narrow
            Medium
            Wide
        End Enum

        Dim ViewWidth As ViewWidths = ViewWidths.Wide
        Private Sub WelcomePage_SizeChanged(sender As Object, e As SizeChangedEventArgs) Handles Me.SizeChanged
            AdjustSize()
        End Sub

        Private Sub AdjustSize()
            If ActualWidth < 640 Then
                If ViewWidth <> ViewWidths.Narrow Then
                    ViewWidth = ViewWidths.Narrow
                    Debug.WriteLine("窄视图")
                    StkHoriScreen.Visibility = Visibility.Visible
                    LayoutRoot.ColumnDefinitions(0).Width = New GridLength(1, GridUnitType.Star)
                    LayoutRoot.ColumnDefinitions(1).Width = New GridLength(0, GridUnitType.Pixel)
                End If
            ElseIf ActualWidth < 870 Then
                If ViewWidth <> ViewWidths.Medium Then
                    ViewWidth = ViewWidths.Medium
                    Debug.WriteLine("中视图")
                    StkHoriScreen.Visibility = Visibility.Collapsed
                    LayoutRoot.ColumnDefinitions(0).Width = New GridLength(1, GridUnitType.Star)
                    LayoutRoot.ColumnDefinitions(1).Width = New GridLength(1, GridUnitType.Star)
                End If
            Else
                If ViewWidth <> ViewWidths.Wide Then
                    ViewWidth = ViewWidths.Wide
                    Debug.WriteLine("宽视图")
                    StkHoriScreen.Visibility = Visibility.Collapsed
                    LayoutRoot.ColumnDefinitions(0).Width = New GridLength(2, GridUnitType.Star)
                    LayoutRoot.ColumnDefinitions(1).Width = New GridLength(3, GridUnitType.Star)
                End If
            End If
        End Sub

        Private Sub BtnAbout_Click(sender As Object, e As RoutedEventArgs)
            Services.NavigationService.Navigate(Services.Pages.AboutPage)
        End Sub
    End Class
End Namespace