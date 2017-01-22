Imports Microsoft.Graphics.Canvas
Imports Windows.Graphics.Effects

Public Interface IWin2DEffect
    Inherits IDisposable
    ''' <summary>
    ''' 向纹理应用一个特效，返回加过特效的纹理。
    ''' </summary>
    Function ApplyEffect(source As IGraphicsEffectSource) As ICanvasImage
    Sub CreateResource(device As ICanvasResourceCreator)
End Interface