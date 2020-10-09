Partial Public Class RuleListDetail
    Implements IEffectiveExpiration

    Private ReadOnly Property EffectiveDate As Date Implements IEffectiveExpiration.EffectiveDate
        Get
            Return Effective
        End Get
    End Property

    Private ReadOnly Property ExpirationDate As Date Implements IEffectiveExpiration.ExpirationDate
        Get
            Return Expiration
        End Get
    End Property
End Class