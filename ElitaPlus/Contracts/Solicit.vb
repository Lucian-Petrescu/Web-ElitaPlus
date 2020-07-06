Imports System.Collections.Generic
Imports Newtonsoft.Json
Public Class Solicit


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
        <JsonProperty("labelTranslation")>
        Public Property labelTranslation As SolicitLabelTranslation
    End Class
    Public Class SolicitSearch
        <JsonProperty("ownerOrganizationCode", Required:=Required.Always)>
        Public Property OwnerOrganizationCode As String
        <JsonProperty("locatorProperties", Required:=Required.Always)>
        Public Property LocatorProperties As Dictionary(Of String, String)
    End Class
    Public Class SolicitLabelTranslation
        <JsonProperty("initialSalesOrder")>
        Public Property INITIAL_SALES_ORDER As String
        <JsonProperty("customerId")>
        Public Property CUSTOMER_ID As String
        <JsonProperty("applyDate")>
        Public Property APPY_DATE_SOLICITATION_DATE As String
        <JsonProperty("openDateFromLeadFile")>
        Public Property OPEN_DATE_FROM_LEAD_FILE As String
        <JsonProperty("source")>
        Public Property SOURCE As String
        <JsonProperty("leadRecordStatus")>
        Public Property LEAD_RECORD_STATUS As String
        <JsonProperty("expirationDate")>
        Public Property EXPIRATION_DATE As String
        <JsonProperty("customerLastName")>
        Public Property CUSTOMER_LAST_NAME As String
        <JsonProperty("customerFirstName")>
        Public Property CUSTOMER_FIRST_NAME As String
        <JsonProperty("lastNameKana")>
        Public Property LAST_NAME_KANA As String
        <JsonProperty("firstNameKana")>
        Public Property FIRST_NAME_KANA As String
        <JsonProperty("customerBirthDate")>
        Public Property CUSTOMER_BIRTH_DATE As String
        <JsonProperty("address")>
        Public Property ADDRESS As String
        <JsonProperty("postalCode")>
        Public Property POSTAL_CODE As String
        <JsonProperty("email")>
        Public Property E_MAIL As String
        <JsonProperty("telMobPhoneNumber")>
        Public Property TEL_MOB_PHONE_NUMBER As String
        <JsonProperty("simMHomePhoneNumber")>
        Public Property SIM_HOME_PHONE_NUMBER As String
        <JsonProperty("shopId")>
        Public Property SHOP_ID As String
        <JsonProperty("shopChannel")>
        Public Property SALES_CHANNEL As String
        <JsonProperty("shopName")>
        Public Property SHOP_NAME As String
        <JsonProperty("shopZipCode")>
        Public Property SHOP_ZIP_CODE As String
        <JsonProperty("shopAddress")>
        Public Property SHOP_ADDRESS As String
        <JsonProperty("shopTelephoneNumber")>
        Public Property SHOP_TELE_PHONE_NUMBER As String

    End Class
End Class
