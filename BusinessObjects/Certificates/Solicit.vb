Imports Newtonsoft.Json
Public Class Solicit
    Inherits BusinessObjectBase



    Public Class Address
        Public Property address1 As String
        Public Property address2 As String
        Public Property address3 As String
        Public Property regionCode As String
        Public Property city As String
        Public Property zipCode As String
        Public Property countryCode As String
    End Class

    Public Class Organization
        Public Property code As String
        Public Property name As String
        Public Property address As Address
        Public Property workPhoneNumber As String
    End Class

    Public Class Owner
        Public Property organization As Organization
        Public Property serviceLineNumber As String
    End Class

    Public Class Customer
        Public Property id As String
        Public Property firstName As String
        Public Property lastName As String
        Public Property firstNameKana As String
        Public Property lastNameKana As String
        Public Property dateOfBirth As DateTime?
        Public Property workPhoneNumber As String
        Public Property homePhoneNumber As String
        Public Property cellPhoneNumber As String
        Public Property emailAddress As String
        Public Property address As Address
    End Class

    Public Class Origin
        Public Property organization As Organization
        Public Property channelCode As String
        Public Property salesOrderNumber As String
        Public Property disconnectionReason As String
    End Class

    Public Class SolicitDetails
        Public Property campaignCode As String
        Public Property creationSourceType As String
        Public Property creationSourceName As String
        Public Property owner As Owner
        Public Property status As String
        Public Property id As String
        Public Property customer As Customer
        Public Property origin As Origin
        Public Property effectiveDate As DateTime?
        Public Property conversionDate As DateTime?
        Public Property expirationDate As DateTime?
    End Class
End Class
