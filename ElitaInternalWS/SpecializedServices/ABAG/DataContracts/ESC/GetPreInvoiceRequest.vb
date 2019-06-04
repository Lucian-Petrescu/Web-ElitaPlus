Imports System.Runtime.Serialization


Namespace SpecializedServices.Abag

    <DataContract(Namespace:="http://elita.assurant.com/SpecializedServices/Abag/GetPreInvoiceRequest", Name:="GetPreInvoiceRequest")>
    Public Class GetPreInvoiceRequest

        Private _CompanyCode As String
    <DataMember(EmitDefaultValue:=True, IsRequired:=True, Name:="CompanyCode", Order:=0)>
    Public Property CompanyCode() As String
        Get
            Return _CompanyCode
        End Get
        Set(ByVal value As String)
            _CompanyCode = value
        End Set
    End Property

    Private _ServiceCenterCode As String
    <DataMember(EmitDefaultValue:=True, IsRequired:=True, Name:="ServiceCenterCode", Order:=1)>
    Public Property ServiceCenterCode() As String
        Get
            Return _ServiceCenterCode
        End Get
        Set(ByVal value As String)
            _ServiceCenterCode = value
        End Set
    End Property

    Private _SCPreInvoiceDateFrom As DateTime
    <DataMember(EmitDefaultValue:=True, IsRequired:=False, Name:="SCPreInvoiceDateFrom", Order:=2)>
    Public Property SCPreInvoiceDateFrom() As DateTime
        Get
            Return _SCPreInvoiceDateFrom
        End Get
        Set(ByVal value As DateTime)
            _SCPreInvoiceDateFrom = value
        End Set
    End Property

    Private _SCPreInvoiceDateTo As DateTime
    <DataMember(EmitDefaultValue:=True, IsRequired:=False, Name:="SCPreInvoiceDateTo", Order:=3)>
    Public Property SCPreInvoiceDateTo() As DateTime
        Get
            Return _SCPreInvoiceDateTo
        End Get
        Set(ByVal value As DateTime)
            _SCPreInvoiceDateTo = value
        End Set
    End Property
    End Class
End Namespace

