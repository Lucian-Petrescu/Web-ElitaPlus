Imports System.Collections.Generic
Imports System.Linq
Imports System.Text.RegularExpressions
Imports System.ServiceModel
Imports System.Runtime.Serialization

<DataContract(Name:="CustRegistrationItemSearch")> _
Public Class CustRegItemSearchDC

#Region "Private Members"

    Private __token As String
    Private __dealer_Code As String
    Private __email_Id As String
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
