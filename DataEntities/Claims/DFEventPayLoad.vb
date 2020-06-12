Public Class DFEventPayLoad
    Private AuthorizationIdField As String
    Public Property AuthorizationId() As String
        Get
            Return AuthorizationIdField
        End Get
        Set(ByVal value As String)
            AuthorizationIdField = value
        End Set
    End Property
    Private FulfillmentOptionField As String
    Public Property FulfillmentOption() As String
        Get
            Return FulfillmentOptionField
        End Get
        Set(ByVal value As String)
            FulfillmentOptionField = value
        End Set
    End Property
    Private IsLegacyField As String
    Public Property IsLegacy() As String
        Get
            Return IsLegacyField
        End Get
        Set(ByVal value As String)
            IsLegacyField = value
        End Set
    End Property
    Private OfferIdField As String
    Public Property OfferId() As String
        Get
            Return OfferIdField
        End Get
        Set(ByVal value As String)
            OfferIdField = value
        End Set
    End Property
End Class
