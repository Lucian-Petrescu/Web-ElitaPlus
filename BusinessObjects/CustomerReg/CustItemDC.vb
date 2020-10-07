Imports System.Collections.Generic
Imports System.Linq
Imports System.Text.RegularExpressions
Imports System.ServiceModel
Imports System.Runtime.Serialization

<DataContract(Name:="CustItem")> _
Public Class CustItemDC

#Region "Private Members"

    Private __token As String
    Private __dealer_Code As String
    Private __email_Id As String
    Private __registration_Date As Date
    Private __registration_Status As String
    Private __imei_Number As String
    Private __make As String
    Private __model As String
    Private __item_Name As String
    Private __coverage As String
    Private __order_Reference_Number As String
    Private __product_Key As String
    Private __country_Code As String
    Private __cellPhone As String

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

    <DataMember(Name:="RegistrationDate", IsRequired:=True)> _
    Public Property RegistrationDate As Date
        Get
            Return __registration_Date
        End Get
        Set
            __registration_Date = value
        End Set
    End Property

    <DataMember(Name:="IMEINumber", IsRequired:=True)> _
    Public Property IMEINumber As String
        Get
            Return __imei_Number
        End Get
        Set
            __imei_Number = value
        End Set
    End Property

    <DataMember(Name:="Make", IsRequired:=True)> _
    Public Property Make As String
        Get
            Return __make
        End Get
        Set
            __make = value
        End Set
    End Property

    <DataMember(Name:="Model", IsRequired:=True)> _
    Public Property Model As String
        Get
            Return __model
        End Get
        Set
            __model = value
        End Set
    End Property

    <DataMember(Name:="ItemName")> _
    Public Property ItemName As String
        Get
            Return __item_Name
        End Get
        Set
            __item_Name = value
        End Set
    End Property

    <DataMember(Name:="RegistrationStatus")> _
    Public Property RegistrationStatus As String
        Get
            Return __registration_Status
        End Get
        Set
            __registration_Status = value
        End Set
    End Property

    <DataMember(Name:="Coverage")> _
    Public Property Coverage As String
        Get
            Return __coverage
        End Get
        Set
            __coverage = value
        End Set
    End Property

    <DataMember(Name:="OrderPartnerReference")> _
    Public Property OrderReferenceNumber As String
        Get
            Return __order_Reference_Number
        End Get
        Set
            __order_Reference_Number = value
        End Set
    End Property

    <DataMember(Name:="ProductKey")> _
    Public Property ProductKey As String
        Get
            Return __product_Key
        End Get
        Set
            __product_Key = value
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

    <DataMember(Name:="CellPhone")> _
    Public Property CellPhone As String
        Get
            Return __cellPhone
        End Get
        Set
            __cellPhone = value
        End Set
    End Property

#End Region

End Class
