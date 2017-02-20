Imports Nukepayload2.N2Engine.UI.Elements

Namespace UI.Controls
    ''' <summary>
    ''' 控件的模板。适用于创建控件逻辑复杂的情况（例如，控件有复杂的初始化方式，批量创建控件）。
    ''' </summary>
    Public Interface IControlTemplate
        Function CreateContent() As GameVisual
    End Interface

End Namespace
