Imports Nukepayload2.N2Engine.Resources

Namespace Extensibility
    Public Interface IGameExtensionInfo

        ReadOnly Property Description As String

        ReadOnly Property DisplayName As String

        ReadOnly Property ExtensionMinPreviewLevel As ExtensionPreviewLevels

        ReadOnly Property ExtensionStage As ExtensionStages

        ReadOnly Property ExtensionVersion As Version

        ReadOnly Property ClientVersion As Version

        ReadOnly Property Icon As BitmapResource

        ReadOnly Property VendorInfo As String

        ReadOnly Property Website As Uri

    End Interface
End Namespace
