Imports System.Collections.Generic
Imports Newtonsoft.Json
Public Class Solicit
    Inherits BusinessObjectBase



    Public Class Address
        <JsonProperty("address1")>
        Public Property address1 As String
        <JsonProperty("address2")>
        Public Property address2 As String
        <JsonProperty("address3")>
        Public Property address3 As String
        <JsonProperty("regionCode")>
        Public Property regionCode As String
        <JsonProperty("city")>
        Public Property city As String
        <JsonProperty("zipCode")>
        Public Property zipCode As String
        <JsonProperty("countryCode")>
        Public Property countryCode As String
    End Class

    Public Class Organization
        <JsonProperty("code")>
        Public Property code As String
        <JsonProperty("name")>
        Public Property name As String
        <JsonProperty("address")>
        Public Property address As Address
        <JsonProperty("workPhoneNumber")>
        Public Property workPhoneNumber As String
    End Class

    Public Class Owner
        <JsonProperty("organization")>
        Public Property organization As Organization
        <JsonProperty("serviceLineNumber")>
        Public Property serviceLineNumber As String
    End Class

    Public Class Customer
        <JsonProperty("id")>
        Public Property id As String
        <JsonProperty("firstName")>
        Public Property firstName As String
        <JsonProperty("lastName")>
        Public Property lastName As String
        <JsonProperty("firstNameKana")>
        Public Property firstNameKana As String
        <JsonProperty("lastNameKana")>
        Public Property lastNameKana As String
        <JsonProperty("dateOfBirth")>
        Public Property dateOfBirth As DateTime?
        <JsonProperty("workPhoneNumber")>
        Public Property workPhoneNumber As String
        <JsonProperty("homePhoneNumber")>
        Public Property homePhoneNumber As String
        <JsonProperty("cellPhoneNumber")>
        Public Property cellPhoneNumber As String
        <JsonProperty("emailAddress")>
        Public Property emailAddress As String
        <JsonProperty("address")>
        Public Property address As Address
    End Class

    Public Class Origin
        <JsonProperty("organization")>
        Public Property organization As Organization
        Public Property channelCode As String
        <JsonProperty("salesOrderNumber")>
        Public Property salesOrderNumber As String
        <JsonProperty("disconnectionReason")>
        Public Property disconnectionReason As String
    End Class

    Public Class SolicitDetails
        <JsonProperty("campaignCode")>
        Public Property campaignCode As String
        <JsonProperty("creationSourceType")>
        Public Property creationSourceType As String
        <JsonProperty("creationSourceName")>
        Public Property creationSourceName As String
        <JsonProperty("owner")>
        Public Property owner As Owner
        <JsonProperty("status")>
        Public Property status As String
        <JsonProperty("id")>
        Public Property id As String
        <JsonProperty("customer")>
        Public Property customer As Customer
        <JsonProperty("origin")>
        Public Property origin As Origin
        <JsonProperty("effectiveDate")>
        Public Property effectiveDate As DateTime?
        <JsonProperty("conversionDate")>
        Public Property conversionDate As DateTime?
        <JsonProperty("expirationDate")>
        Public Property expirationDate As DateTime?
    End Class
    Public Class SolicitSearch
        <JsonProperty("ownerOrganizationCode", Required:=Required.Always)>
        Public Property OwnerOrganizationCode As String
        <JsonProperty("locatorProperties", Required:=Required.Always)>
        Public Property LocatorProperties As Dictionary(Of String, String)
    End Class
End Class
