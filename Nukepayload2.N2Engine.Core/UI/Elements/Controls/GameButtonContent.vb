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
        Public ReadOnly Property BorderColor As PropertyBinder(Of Color)
            Get
                Return _rect.Stroke
            End Get
        End Property

        ''' <summary>
        ''' 按钮背景色
        ''' </summary>
        Public ReadOnly Property Background As PropertyBinder(Of Color)
            Get
                Return _rect.Fill
            End Get
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
        Public ReadOnly Property TextOffset As New PropertyBinder(Of Vector2)

        ''' <summary>
        ''' 按钮中的文字
        ''' </summary>
        Public ReadOnly Property Text As PropertyBinder(Of String)
            Get
                Return _txtText.Text
            End Get
        End Property

        Sub New()
            _rect.Location.Bind(Location)
            _rect.Size.Bind(Size)
            _txtText.Location.Bind(Function() Location.Value + TextOffset.Value)
            _txtText.Size.Bind(Size)
            With Children
                .Add(_rect)
                .Add(_txtText)
            End With
        End Sub

    End Class
End Namespace
