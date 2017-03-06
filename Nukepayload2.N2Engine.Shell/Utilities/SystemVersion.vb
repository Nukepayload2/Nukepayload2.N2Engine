Imports System.Runtime.InteropServices
Imports Windows.System.Profile.AnalyticsInfo

Friend Module SystemVersion

    <StructLayout(LayoutKind.Explicit)>
    Private Structure PackageVersionULong
        <FieldOffset(0)>
        Dim PkgVersion As PackageVersion
        <FieldOffset(0)>
        Dim ULongValue As ULong
        Sub New(uLongValue As ULong)
            Me.ULongValue = uLongValue
        End Sub
    End Structure

    <Extension>
    Function ToDisplayStringRev$(ver As PackageVersion)
        Return $"{ver.Major}.{ver.Minor}.{ver.Build}.{ver.Revision}"
    End Function
    <Extension>
    Function ToDisplayString$(ver As PackageVersion)
        Return $"{ver.Revision}.{ver.Build}.{ver.Minor}.{ver.Major}"
    End Function
    Friend Function GetWindowsVersion$()
        Return New PackageVersionULong(CULng(VersionInfo.DeviceFamilyVersion)).PkgVersion.ToDisplayString
    End Function
End Module
