Imports System.Numerics
Imports FarseerPhysics.Dynamics
Imports Nukepayload2.N2Engine.UI.Elements

Public Class StageOneGrid
    Public ReadOnly Property LayoutRoot As SpriteEntityGrid

    Sub New()
        Dim spriteSheets = {New Uri("n2-res-emb:///Images/DemoTexture.png")}
        Dim gravity As New Vector2(0F, 9.8F)
        Dim world As New World(gravity)
        Dim 草中 = (0, 0)
        Dim 草右 = (0, 1)
        Dim 草左 = (0, 2)
        Dim 土壤 = (0, 3)
        Dim 铁块 = (0, 4)
        Dim 空气 As Tile = Nothing
        Dim tiles As Tile(,) = {
            {空气, 铁块, 空气, 空气, 空气, 铁块, 空气, 空气, 铁块, 铁块, 空气, 空气, 空气, 空气, 空气, 空气, 空气},
            {空气, 铁块, 铁块, 空气, 空气, 铁块, 空气, 铁块, 空气, 空气, 铁块, 空气, 空气, 空气, 空气, 空气, 空气},
            {空气, 铁块, 空气, 铁块, 空气, 铁块, 空气, 空气, 空气, 铁块, 空气, 空气, 空气, 空气, 空气, 空气, 空气},
            {空气, 铁块, 空气, 空气, 铁块, 铁块, 空气, 空气, 铁块, 空气, 空气, 空气, 空气, 空气, 空气, 空气, 空气},
            {空气, 铁块, 空气, 空气, 空气, 铁块, 空气, 铁块, 铁块, 铁块, 铁块, 空气, 空气, 空气, 空气, 空气, 空气},
            {空气, 空气, 空气, 空气, 空气, 空气, 空气, 空气, 空气, 空气, 空气, 空气, 空气, 空气, 空气, 空气, 空气},
            {草左, 草中, 草中, 草中, 草中, 草中, 草中, 草中, 草中, 草中, 草中, 草中, 草中, 草中, 草中, 草中, 草右},
            {土壤, 土壤, 土壤, 土壤, 土壤, 土壤, 土壤, 土壤, 土壤, 土壤, 土壤, 土壤, 土壤, 土壤, 土壤, 土壤, 土壤}
        }
        LayoutRoot = New SpriteEntityGrid(world, spriteSheets, tiles, New Vector2(64, 64))
    End Sub
End Class
