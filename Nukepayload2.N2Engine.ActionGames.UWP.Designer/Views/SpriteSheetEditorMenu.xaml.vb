' The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

Imports Nukepayload2.N2Engine.ActionGames.UWP.Designer.Models
Imports Nukepayload2.N2Engine.UWP.Designer.Attributes
Imports Nukepayload2.N2Engine.UWP.Designer.Models

<MenuTemplate>
Public NotInheritable Class SpriteSheetEditorMenu
    Inherits UserControl

    Public Shared ReadOnly Property Information As New MenuDataTemplateInformation(MenuDataTemplateNames.SpriteSheetEditorMenu, "编辑某张贴图表的信息", "SpriteSheetEditorMenuTemplate", "ms-appx:///Nukepayload2.N2Engine.ActionGames.UWP.Designer/Themes/DesignerTemplates.xaml")
End Class
