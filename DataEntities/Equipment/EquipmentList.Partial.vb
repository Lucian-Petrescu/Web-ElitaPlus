Imports Assurant.ElitaPlus.DataEntities

Partial Public Class EquipmentList
    Implements IEffectiveExpiration

    Public ReadOnly Property EffectiveDate As Date Implements IEffectiveExpiration.EffectiveDate
        Get
            Return Effective
        End Get
    End Property

    Public ReadOnly Property ExpirationDate As Date Implements IEffectiveExpiration.ExpirationDate
        Get
            Return Expiration
        End Get
    End Property

    Public Shared Widening Operator CType(v As Boolean) As EquipmentList
        Throw New NotImplementedException()
    End Operator
End Class
