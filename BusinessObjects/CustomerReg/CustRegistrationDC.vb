Imports System.Collections.Generic
Imports System.Linq
Imports System.Text.RegularExpressions
Imports System.ServiceModel
Imports System.Runtime.Serialization

<DataContract(Name:="CustRegistration")> _
Public Class CustRegistrationDC

#Region "Private Members"

    Private __token As String
    Private __dealer_Code As String
    Private __email_Id As String
    Private __updated_Email_id As String
    Private __tax_Id As String
    Private __first_Name As String
    Private __last_Name As String
    Private __address1 As String
    Private __address2 As String
    Private __city As String
    Private __state As String
    Private __postal_Code As String
    Private __phone As String
    Private __country_Code As String

#End Region

#Region "DataMembers"

    <DataMember(Name:="Token", IsRequired:=True)> _
    Public Property Token As String
        Get
            Return __token
        End Get
        Set
            __token = value
        End Set
    End Property

    <DataMember(Name:="DealerCode", IsRequired:=True)> _
    Public Property DealerCode As String
        Get
            Return __dealer_Code
        End Get
        Set
            __dealer_Code = value
        End Set
    End Property

    <DataMember(Name:="EmailID", IsRequired:=True)> _
    Public Property EmailID As String
        Get
            Return __email_Id
        End Get
        Set
            __email_Id = value
        End Set
    End Property

    <DataMember(Name:="UpdatedEmailID")> _
    Public Property UpdatedEmailID As String
        Get
            Return __updated_Email_id
        End Get
        Set
            __updated_Email_id = value
        End Set
    End Property

    <DataMember(Name:="TaxID", IsRequired:=True)> _
    Public Property TaxID As String
        Get
            Return __tax_Id
        End Get
        Set
            __tax_Id = value
        End Set
    End Property

    <DataMember(Name:="FirstName", IsRequired:=True)> _
    Public Property FirstName As String
        Get
            Return __first_Name
        End Get
        Set
            __first_Name = value
        End Set
    End Property

    <DataMember(Name:="LastName", IsRequired:=True)> _
    Public Property LastName As String
        Get
            Return __last_Name
        End Get
        Set
            __last_Name = value
        End Set
    End Property

    <DataMember(Name:="Address1")> _
    Public Property Address1 As String
        Get
            Return __address1
        End Get
        Set
            __address1 = value
        End Set
    End Property

    <DataMember(Name:="Address2")> _
    Public Property Address2 As String
        Get
            Return __address2
        End Get
        Set
            __address2 = value
        End Set
    End Property

    <DataMember(Name:="City")> _
    Public Property City As String
        Get
            Return __city
        End Get
        Set
            __city = value
        End Set
    End Property

    <DataMember(Name:="State")> _
    Public Property State As String
        Get
            Return __state
        End Get
        Set
            __state = value
        End Set
    End Property

    <DataMember(Name:="PostalCode")> _
    Public Property PostalCode As String
        Get
            Return __postal_Code
        End Get
        Set
            __postal_Code = value
        End Set
    End Property

    <DataMember(Name:="Phone")> _
    Public Property Phone As String
        Get
            Return __phone
        End Get
        Set
            __phone = value
        End Set
    End Property

    <DataMember(Name:="CountryCode", IsRequired:=True)> _
    Public Property CountryCode As String
        Get
            Return __country_Code
        End Get
        Set
            __country_Code = value
        End Set
    End Property

#End Region
End Class
