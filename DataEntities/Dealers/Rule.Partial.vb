Partial Public Class Rule
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
End Class