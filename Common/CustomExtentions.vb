Imports System.Runtime.CompilerServices

Public Module CustomExtentions
    <Extension()>
    Public Function IsEmpty(value As Guid) As Boolean
        Return value = Guid.Empty
    End Function

End Module
