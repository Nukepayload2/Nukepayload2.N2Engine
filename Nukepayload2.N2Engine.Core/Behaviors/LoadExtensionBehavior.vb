Public Class LoadExtensionBehavior(Of TExtendableVisual As {GameVisual, IExtendable}, TExtension As IGameExtensionInfo)
    Inherits BehaviorBase(Of TExtendableVisual)

    Sub New(extension As TExtension)
        Me.Extension = extension
    End Sub

    Public Property Extension As TExtension

    Protected Overrides Sub OnAttached(visual As TExtendableVisual)
        visual.Extensions.Add(Extension)
    End Sub
End Class
