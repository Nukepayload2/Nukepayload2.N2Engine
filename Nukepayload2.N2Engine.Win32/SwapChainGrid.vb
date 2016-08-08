Imports System.Windows.Controls
Imports System.Windows.Forms.Integration

''' <summary>
''' 自带呈现 MonoGame 交换链的 WPF 控件。拥有与 <see cref="Grid"/> 类似的行为。
''' </summary>
Public Class SwapChainGrid
    Inherits Grid

    Sub New()
        SetZIndex(WinformHost, Integer.MinValue)
        SetRowSpan(WinformHost, Integer.MaxValue)
        SetColumnSpan(WinformHost, Integer.MaxValue)
        Children.Add(WinformHost)
    End Sub

    Public ReadOnly Property WinformHost As New WindowsFormsHost
End Class
