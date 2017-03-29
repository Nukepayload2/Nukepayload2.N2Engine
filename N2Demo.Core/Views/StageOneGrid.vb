Imports System.Numerics
Imports FarseerPhysics.Dynamics
Imports Nukepayload2.N2Engine.UI.Elements

Public Class StageOneGrid
    Public ReadOnly Property LayoutRoot As SpriteEntityGrid
    Dim _vm As New StageOneViewModel

    Sub New()
        Dim world As New World(_vm.Gravity)
        LayoutRoot = New SpriteEntityGrid(world, _vm.SpriteSheets, _vm.Tiles, New Vector2(64, 64))
    End Sub
End Class
