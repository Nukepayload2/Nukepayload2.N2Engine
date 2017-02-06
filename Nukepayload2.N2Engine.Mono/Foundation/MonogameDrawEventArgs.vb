Imports Microsoft.Xna.Framework
Imports Microsoft.Xna.Framework.Graphics
Imports RaisingStudio.Xna.Graphics
''' <summary>
''' Mono Game 平台上绘制图形必要的参数。
''' </summary>
Public Class MonogameDrawEventArgs
    Inherits EventArgs

    Sub New(spriteBatch As SpriteBatch, timing As GameTime)
        Me.SpriteBatch = spriteBatch
        Me.Timing = timing
    End Sub
    ''' <summary>
    ''' 容器控件用这个属性给子元素创建绘图上下文。注意：元素不要直接在上面绘制。
    ''' </summary>
    Public ReadOnly Property SpriteBatch As SpriteBatch
    ''' <summary>
    ''' 游戏的统计时间
    ''' </summary>
    Public ReadOnly Property Timing As GameTime
    ''' <summary>
    ''' 当前容器控件创建的绘图上下文。
    ''' </summary>
    Public Property DrawingContext As DrawingContext
End Class