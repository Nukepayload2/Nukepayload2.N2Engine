Imports Nukepayload2.N2Engine.Foundation
Imports Nukepayload2.N2Engine.UI.Elements
Imports Nukepayload2.N2Engine.UI.Text

Namespace UI.Controls

    Public Class GameButtonContent
        Inherits GameTemplatedContent

        Dim _rect As New RectangleElement
        Dim _txtText As New GameTextBlock

        ''' <summary>
        ''' 按钮边框的颜色
        ''' </summary>
        Public Property BorderColor As PropertyBinder(Of Color)
            Get
                Return _rect.Stroke
            End Get
            Set(value As PropertyBinder(Of Color))
                _rect.Stroke = value
            End Set
        End Property

        ''' <summary>
        ''' 按钮背景色
        ''' </summary>
        Public Property Background As PropertyBinder(Of Color)
            Get
                Return _rect.Fill
            End Get
            Set(value As PropertyBinder(Of Color))
                _rect.Fill = value
            End Set
        End Property

        ''' <summary>
        ''' 按钮上文字的字体
        ''' </summary>
        Public Property Font As GameFont
            Get
                Return _txtText.Font
            End Get
            Set(value As GameFont)
                _txtText.Font = value
            End Set
        End Property

        ''' <summary>
        ''' 按钮上文字的位移
        ''' </summary>
        Public Property TextOffset As PropertyBinder(Of Vector2)
            Get
                Return _txtText.Location
            End Get
            Set(value As PropertyBinder(Of Vector2))
                _txtText.Location = value
            End Set
        End Property
        ''' <summary>
        ''' 按钮中的文字
        ''' </summary>
        Public Property Text As PropertyBinder(Of String)
            Get
                Return _txtText.Text
            End Get
            Set(value As PropertyBinder(Of String))
                _txtText.Text = value
            End Set
        End Property

        Public Overrides Property Size As PropertyBinder(Of Vector2)
            Get
                Return MyBase.Size
            End Get
            Set(value As PropertyBinder(Of Vector2))
                MyBase.Size = value
                If _rect IsNot Nothing Then
                    _rect.Size = New CachedPropertyBinder(Of Vector2)(value)
                    _txtText.Size = New CachedPropertyBinder(Of Vector2)(value)
                End If
            End Set
        End Property

        Sub New()
            _rect.Size = New CachedPropertyBinder(Of Vector2)(Size)
            _txtText.Size = New CachedPropertyBinder(Of Vector2)(Size)
            With Children
                .Add(_rect)
                .Add(_txtText)
            End With
        End Sub

    End Class
End Namespace
