Imports System.ComponentModel.DataAnnotations

Namespace ViewModels

    Public Class AboutViewModel
        Inherits SingleInstance(Of AboutViewModel)

        Sub New()
            Dim ver = Windows.System.Profile.AnalyticsInfo.VersionInfo
            With Package.Current
                DisplayName = .DisplayName
                Description = .Description
                PublisherName = .PublisherDisplayName
                OSVersion = GetWindowsVersion()
                DeviceFamily = ver.DeviceFamily
                Version = .Id.Version.ToDisplayStringRev
            End With
        End Sub

        <Display(Name:="显示名称")>
        Public ReadOnly Property DisplayName As String
        <Display(Name:="描述")>
        Public ReadOnly Property Description As String
        <Display(Name:="发布者名称")>
        Public ReadOnly Property PublisherName As String
        <Display(Name:="操作系统")>
        Public ReadOnly Property OSVersion As String
        <Display(Name:="设备家族")>
        Public ReadOnly Property DeviceFamily As String
        <Display(Name:="软件包版本")>
        Public ReadOnly Property Version As String
        <Display(Name:="非运行库Nuget包")>
        Public ReadOnly Property ExternalLibraries As String() = {
            "Newtonsoft.Json", "Microsoft.Toolkit.Uwp.UI.Controls", "Win2D.uwp"
        }

    End Class

End Namespace