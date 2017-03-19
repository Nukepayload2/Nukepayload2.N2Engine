Imports System.Runtime.InteropServices
Imports Nukepayload2.N2Engine.Input
Imports Windows.UI.Core

Namespace Marshal

    Public Module KeyStateMarshal
        <StructLayout(LayoutKind.Explicit)>
        Private Structure KeyStateConverter
            <FieldOffset(0)>
            Dim N2KeyState As PhysicalKeyStatus?
            <FieldOffset(0)>
            Dim WinRTKeyState As CorePhysicalKeyStatus?
        End Structure
        ''' <summary>
        ''' 将 Windows 运行时 按键状态转换成 N2 引擎 按键状态
        ''' </summary>
        ''' <param name="WinRTKeyStatus">Windows 运行时 按键状态</param>
        ''' <returns>N2 引擎按键状态</returns>
        <Extension>
        Public Function AsN2KeyStatus(WinRTKeyStatus As CorePhysicalKeyStatus) As PhysicalKeyStatus
            Return (New KeyStateConverter With {.WinRTKeyState = WinRTKeyStatus}).N2KeyState.Value
        End Function
        ''' <summary>
        ''' 将 Windows 运行时 按键状态转换成 N2 引擎 按键状态
        ''' </summary>
        ''' <param name="N2KeyStatus">N2 引擎 按键状态</param>
        ''' <returns>N2 引擎按键状态</returns>
        <Extension>
        Public Function AsWinRTKeyStatus(N2KeyStatus As PhysicalKeyStatus) As CorePhysicalKeyStatus
            Return (New KeyStateConverter With {.N2KeyState = N2KeyStatus}).WinRTKeyState.Value
        End Function
    End Module

End Namespace