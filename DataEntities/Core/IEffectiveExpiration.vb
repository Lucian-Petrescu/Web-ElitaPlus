Imports System.Runtime.CompilerServices

Public Interface IEffectiveExpiration
    ReadOnly Property EffectiveDate As Date
    ReadOnly Property ExpirationDate As Date
End Interface

Public Module IEffectiveExpirationExtensions
    <Extension()>
    Public Function IsEffective(ByVal pObject As IEffectiveExpiration, ByVal pDate As Date)
        Return ((pObject.EffectiveDate <= pDate) AndAlso (pObject.ExpirationDate >= pDate))
    End Function

    <Extension()>
    Public Function IsEffective(ByVal pObject As IEffectiveExpiration)
        Return pObject.IsEffective(DateTime.Today)
    End Function

End Module
