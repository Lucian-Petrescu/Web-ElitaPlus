Imports System.Runtime.Serialization

<DataContract(Name:="PreInvoiceSearch", Namespace:="http://elita.assurant.com/SNMPortal")> _
Public Class PreInvoiceSearchDC

#Region "DataMember"

    Private _Token As String
    <DataMember(EmitDefaultValue:=True, IsRequired:=True, Name:="Token", Order:=0)> _
    Public Property Token As String
        Get
            Return _Token
        End Get
        Set
            _Token = value
        End Set
    End Property

    Private _CompanyCode As String
    <DataMember(EmitDefaultValue:=True, IsRequired:=True, Name:="Company_Code", Order:=1)> _
    Public Property Company_Code As String
        Get
            Return _CompanyCode
        End Get
        Set
            _CompanyCode = value
        End Set
    End Property

    Private _ServiceCenterCode As String
    <DataMember(EmitDefaultValue:=True, IsRequired:=True, Name:="Service_Center_Code", Order:=2)> _
    Public Property Service_Center_Code As String
        Get
            Return _ServiceCenterCode
        End Get
        Set
            _ServiceCenterCode = value
        End Set
    End Property

    Private _SCPreInvoiceDateFrom As DateTime
    <DataMember(EmitDefaultValue:=True, IsRequired:=False, Name:="SC_PreInvoice_Date_From", order:=3)> _
    Public Property SC_PreInvoice_Date_From As DateTime
        Get
            Return _SCPreInvoiceDateFrom
        End Get
        Set
            _SCPreInvoiceDateFrom = value
        End Set
    End Property

    Private _SCPreInvoiceDateTo As DateTime
    <DataMember(EmitDefaultValue:=True, IsRequired:=False, Name:="SC_PreInvoice_Date_To", order:=4)> _
    Public Property SC_PreInvoice_Date_To As DateTime
        Get
            Return _SCPreInvoiceDateTo
        End Get
        Set
            _SCPreInvoiceDateTo = value
        End Set
    End Property

#End Region

End Class
