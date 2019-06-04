Imports Assurant.ElitaPlus.DataEntities

Partial Public Class EquipmentListDetail
    Implements IEffectiveExpiration

    Public ReadOnly Property EffectiveDate As Date Implements IEffectiveExpiration.EffectiveDate
        Get
            Return Me.Effective
        End Get
    End Property

    Public ReadOnly Property ExpirationDate As Date Implements IEffectiveExpiration.ExpirationDate
        Get
            Return Me.Expiration
        End Get
    End Property
End Class
