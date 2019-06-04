Public Interface IExpirable
    Inherits IElement
    Property Effective As DateTimeType
    Property Expiration As DateTimeType
    Property Code As String
    Property parent_id As Guid
End Interface