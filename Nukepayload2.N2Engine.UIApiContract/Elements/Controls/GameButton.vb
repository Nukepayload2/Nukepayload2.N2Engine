Imports Nukepayload2.N2Engine.Foundation
Imports Nukepayload2.N2Engine.UI.Elements
Imports Nukepayload2.N2Engine.UI.Text

Namespace UI.Controls
    ''' <summary>
    ''' 游戏中的按钮。在每一个平台有统一的外观。
    ''' </summary>
    Public Class GameButton
        Inherits GameContentControl(Of GameButtonContent)

        ''' <summary>
        ''' 按钮边框的颜色
        ''' </summary>
        Public Property BorderColor As PropertyBinder(Of Color)
            Get
                Return Content.BorderColor
            End Get
            Set(value As PropertyBinder(Of Color))
                Content.BorderColor = value
            End Set
        End Property

        ''' <summary>
        ''' 按钮背景色
        ''' </summary>
        Public Property Background As PropertyBinder(Of Color)
            Get
                Return Content.Background
            End Get
            Set(value As PropertyBinder(Of Color))
                Content.Background = value
            End Set
        End Property

        ''' <summary>
        ''' 按钮上文字的字体
        ''' </summary>
        Public Property Font As GameFont
            Get
                Return Content.Font
            End Get
            Set(value As GameFont)
                Content.Font = value
            End Set
        End Property

        ''' <summary>
        ''' 按钮上文字的位移
        ''' </summary>
        Public Property TextOffset As PropertyBinder(Of Vector2)
            Get
                Return Content.TextOffset
            End Get
            Set(value As PropertyBinder(Of Vector2))
                Content.TextOffset = value
            End Set
        End Property

        ''' <summary>
        ''' 按钮中的文字
        ''' </summary>
        Public Property Text As PropertyBinder(Of String)
            Get
                Return Content.Text
            End Get
            Set(value As PropertyBinder(Of String))
                Content.Text = value
            End Set
        End Property

        Public Overrides Property Size As PropertyBinder(Of Vector2)
            Get
                Return Content.Size
            End Get
            Set(value As PropertyBinder(Of Vector2))
                If Content IsNot Nothing Then Content.Size = value
            End Set
        End Property

        Public Overrides Property Location As PropertyBinder(Of Vector2)
            Get
                Return Content.Location
            End Get
            Set(value As PropertyBinder(Of Vector2))
                If Content IsNot Nothing Then Content.Location = value
            End Set
        End Property

        Sub New()
            MyBase.New((New GameButtonContentTemplate).CreateContent)
        End Sub

        ''' <summary>
        ''' 鼠标或电磁笔是否指向按钮
        ''' </summary>
        Public ReadOnly Property IsPointerOver As Boolean
        ''' <summary>
        ''' 是否按下了按钮但是没松开
        ''' </summary>
        Public ReadOnly Property IsPressed As Boolean
        ''' <summary>
        ''' 在哪种情况下会引发点击事件
        ''' </summary>
        Public Property ClickMode As ClickModes

        ''' <summary>
        ''' 当按钮被点击时引发此事件。鼠标左键点击和触摸屏轻扫都可能引发此事件。
        ''' </summary>
        Public Event Click As GameObjectEventHandler(Of GameButton, EventArgs)

        Private Sub GameButton_MouseButtonDown(sender As GameVisual, e As GameMouseRoutedEventArgs) Handles Me.MouseButtonDown
            If HitTest(e.Position) Then
                e.Handled = True
                _IsPressed = True
                _IsPointerOver = True
                If ClickMode = ClickModes.Press Then
                    RaiseEvent Click(Me, EventArgs.Empty)
                End If
            End If
        End Sub

        Private Sub GameButton_MouseButtonUp(sender As GameVisual, e As GameMouseRoutedEventArgs) Handles Me.MouseButtonUp
            If IsPressed AndAlso HitTest(e.Position) AndAlso ClickMode = ClickModes.Release Then
                e.Handled = True
                RaiseEvent Click(Me, EventArgs.Empty)
            End If
            _IsPressed = False
        End Sub

        Dim _lastTouchPoint As UInteger

        Private Sub GameButton_TouchDown(sender As GameVisual, e As GameTouchRoutedEventArgs) Handles Me.TouchDown
            If _lastTouchPoint = GameTouchRoutedEventArgs.InvalidTouchId AndAlso HitTest(e.Position) Then
                e.Handled = True
                _lastTouchPoint = e.PointerId
                _IsPointerOver = True
                _IsPressed = True
                If ClickMode = ClickModes.Press Then
                    RaiseEvent Click(Me, EventArgs.Empty)
                End If
            End If
        End Sub

        Private Sub GameButton_TouchUp(sender As GameVisual, e As GameTouchRoutedEventArgs) Handles Me.TouchUp
            If _lastTouchPoint = e.PointerId Then
                _lastTouchPoint = GameTouchRoutedEventArgs.InvalidTouchId
                If HitTest(e.Position) AndAlso ClickMode = ClickModes.Release Then
                    e.Handled = True
                    RaiseEvent Click(Me, EventArgs.Empty)
                End If
                _IsPointerOver = False
                _IsPressed = False
            End If
        End Sub

        Protected Overridable Function HitTest(point As Vector2) As Boolean
            Return GameVisualTreeHelper.HitTest(Me, point)
        End Function

        Private Sub GameButton_MouseMove(sender As GameVisual, e As GameMouseRoutedEventArgs) Handles Me.MouseMove
            _IsPointerOver = HitTest(e.Position)
            If _IsPointerOver Then
                e.Handled = True
            End If
        End Sub
    End Class

End Namespace