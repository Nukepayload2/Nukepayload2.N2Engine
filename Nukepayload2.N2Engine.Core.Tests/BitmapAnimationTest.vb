Imports Nukepayload2.N2Engine.Animations
Imports Nukepayload2.N2Engine.Foundation
Imports Nukepayload2.N2Engine.Resources

<TestClass()>
Public Class BitmapAnimationTest

    Const ImageSize = 256
    Const BlockSize = 64

    Dim nukeBallSource As New FakeBitmapResource(New Uri("n2-test:///nukeball.png"), New SizeInInteger(ImageSize, ImageSize))
    Dim nukeBalls As IEnumerable(Of BitmapResource) = nukeBallSource.Split(BlockSize, BlockSize)

    <TestMethod()>
    Public Sub TestEnumerable()
        Dim anim As New BitmapDiscreteAnimation(nukeBalls)
        Assert.AreEqual(anim.Count, CInt((ImageSize / BlockSize) ^ 2))
    End Sub

End Class