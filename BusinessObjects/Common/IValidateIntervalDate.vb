Public Interface IValidateIntervalDate

    ReadOnly Property IEffective() As DateType
    ReadOnly Property IExpiration() As DateType
    ReadOnly Property IMaxExpiration() As DateType
    ReadOnly Property IIsNew() As Boolean
    ReadOnly Property IIsDeleted() As Boolean

End Interface
