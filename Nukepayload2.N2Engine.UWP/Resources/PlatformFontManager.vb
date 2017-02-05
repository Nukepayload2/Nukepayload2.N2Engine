Imports Nukepayload2.N2Engine.Resources
Imports Nukepayload2.N2Engine.UI.Text

Friend Class PlatformFontLoader
    ' 在 Win2D 会自动加载字体
    Public Async Function LoadAsync(font As GameFont) As Task Implements IFontLoader.LoadAsync
        Await Task.Delay(0)
    End Function
End Class
