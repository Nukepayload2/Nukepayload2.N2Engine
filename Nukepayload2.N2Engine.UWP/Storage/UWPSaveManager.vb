﻿Imports Nukepayload2.N2Engine.Core

<PlatformImpl(GetType(SaveManager))>
Friend Class UWPSaveManager
    Inherits PlatformSaveManagerBase

    Public Overrides ReadOnly Property LocalMasterData As PlatformSaveDirectoryBase
        Get
            Throw New NotImplementedException()
        End Get
    End Property

    Public Overrides ReadOnly Property LocalPartialData As PlatformSaveDirectoryBase
        Get
            Throw New NotImplementedException()
        End Get
    End Property

    Public Overrides ReadOnly Property RoamingData As PlatformSaveDirectoryBase
        Get
            Throw New NotImplementedException()
        End Get
    End Property

    Public Overrides ReadOnly Property VendorSharedData As PlatformSaveDirectoryBase
        Get
            Throw New NotImplementedException()
        End Get
    End Property
End Class