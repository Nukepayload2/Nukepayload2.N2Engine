Imports Windows.Storage
Namespace Utilities

    Public Class CompareStorageFile
        Inherits EqualityComparer(Of StorageFile)

        Public Overrides Function Equals(x As StorageFile, y As StorageFile) As Boolean
            Return x.Path.ToLower = y.Path.ToLower
        End Function

        Public Overrides Function GetHashCode(obj As StorageFile) As Integer
            Return obj.Path.ToLower.GetHashCode
        End Function
    End Class

End Namespace