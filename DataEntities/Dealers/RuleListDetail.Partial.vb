Partial Public Class RuleListDetail
    Implements IEffectiveExpiration

    Private ReadOnly Property EffectiveDate As Date Implements IEffectiveExpiration.EffectiveDate
        Get
            Return Me.Effective
        End Get
    End Property

    Private ReadOnly Property ExpirationDate As Date Implements IEffectiveExpiration.ExpirationDate
        Get
            Return Me.Expiration
        End Get
    End Property
End Class