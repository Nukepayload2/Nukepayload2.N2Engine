Imports System.ComponentModel
Imports System.Numerics
Imports Nukepayload2.N2Engine.Foundation
Imports Nukepayload2.N2Engine.Information

Public Class ButtonStatus
    Implements INotifyPropertyChanged

    Public ReadOnly Property Background As New Color(&H7FEFEF00)
    Public ReadOnly Property PointerOverBackground As New Color(&H7FFFFF00)
    Public ReadOnly Property PressedBorderColor As New Color(&H7F000000)
    Public ReadOnly Property BorderColor As New Color(&H7F7F7F00)

    Public Property Text As String
        Get
            Return _Text
        End Get
        Set(value As String)
            _Text = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(Text)))
        End Set
    End Property

    Public Property Position As Vector2
        Get
            Dim bbSize = BackBufferInformation.ViewPortSize
            Return New Vector2(bbSize.Width - Size.X - _Margin.Z, bbSize.Height - Size.Y - _Margin.W)
        End Get
        Set(value As Vector2)
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(Position)))
        End Set
    End Property

    Public Property Size As Vector2
        Get
            Return _Size
        End Get
        Set(value As Vector2)
            _Size = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(Size)))
        End Set
    End Property

    Public Property TextOffset As Vector2
        Get
            Return _TextOffset
        End Get
        Set(value As Vector2)
            _TextOffset = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(TextOffset)))
        End Set
    End Property

    ReadOnly _Margin As New Vector4(0F, 0F, 24.0F, 24.0F)
    Private _Text As String = "跳"
    Private _Size As New Vector2(120.0F, 120.0F)
    Private _TextOffset As New Vector2(53.0F, 53.0F)
    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged
End Class
