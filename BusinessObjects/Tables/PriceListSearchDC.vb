Imports System.Runtime.Serialization
Imports Assurant.ElitaPlus.BusinessObjectsNew
Imports Assurant.Common

<DataContract(Name:="PriceListSearch", Namespace:="http://elita.assurant.com/SNMPortal")> _
Public Class PriceListSearchDC

#Region "DataMember"

    Private _Token As String
    <DataMember(EmitDefaultValue:=True, IsRequired:=True, Name:="Token", Order:=0)> _
    Public Property Token() As String
        Get
            Return _Token
        End Get
        Set(ByVal value As String)
            _Token = value
        End Set
    End Property

    Private _InForceDate As DateTime
    <DataMember(EmitDefaultValue:=True, IsRequired:=True, Name:="In_Force_Date", Order:=1)> _
    Public Property In_Force_Date() As DateTime
        Get
            Return _InForceDate
        End Get
        Set(ByVal value As DateTime)
            _InForceDate = value
        End Set
    End Property

    Private _ClaimNumber As String
    <DataMember(EmitDefaultValue:=True, IsRequired:=False, Name:="Claim_Number", Order:=2)> _
    Public Property Claim_Number() As String
        Get
            Return _ClaimNumber
        End Get
        Set(ByVal value As String)
            _ClaimNumber = value
        End Set
    End Property

    Private _ServiceCenterCode As String
    <DataMember(EmitDefaultValue:=True, IsRequired:=False, Name:="Service_Center_Code", Order:=3)> _
    Public Property Service_Center_Code() As String
        Get
            Return _ServiceCenterCode
        End Get
        Set(ByVal value As String)
            _ServiceCenterCode = value
        End Set
    End Property

    Private _RiskTypeCode As String
    <DataMember(EmitDefaultValue:=True, IsRequired:=False, Name:="Risk_Type_Code", Order:=4)> _
    Public Property Risk_Type_Code() As String
        Get
            Return _RiskTypeCode
        End Get
        Set(ByVal value As String)
            _RiskTypeCode = value
        End Set
    End Property

    Private _EquipmentClassCode As String
    <DataMember(EmitDefaultValue:=True, IsRequired:=False, Name:="Equipment_Class_Code", Order:=5)> _
    Public Property Equipment_Class_Code() As String
        Get
            Return _EquipmentClassCode
        End Get
        Set(ByVal value As String)
            _EquipmentClassCode = value
        End Set
    End Property

    Private _CompanyCode As String
    <DataMember(EmitDefaultValue:=True, IsRequired:=False, Name:="Company_Code", Order:=6)> _
    Public Property Company_Code() As String
        Get
            Return _CompanyCode
        End Get
        Set(ByVal value As String)
            _CompanyCode = value
        End Set
    End Property

    Private _DealerCode As String
    <DataMember(EmitDefaultValue:=True, IsRequired:=False, Name:="Dealer_Code", Order:=7)> _
    Public Property Dealer_Code() As String
        Get
            Return _DealerCode
        End Get
        Set(ByVal value As String)
            _DealerCode = value
        End Set
    End Property

    Private _ServiceClassCode As String
    <DataMember(EmitDefaultValue:=True, IsRequired:=False, Name:="Service_Class_Code", Order:=8)> _
    Public Property Service_Class_Code() As String
        Get
            Return _ServiceClassCode
        End Get
        Set(ByVal value As String)
            _ServiceClassCode = value
        End Set
    End Property

    Private _ServiceTypeCode As String
    <DataMember(EmitDefaultValue:=True, IsRequired:=False, Name:="Service_Type_Code", Order:=9)> _
    Public Property Service_Type_Code() As String
        Get
            Return _ServiceTypeCode
        End Get
        Set(ByVal value As String)
            _ServiceTypeCode = value
        End Set
    End Property

    Private _Make As String
    <DataMember(EmitDefaultValue:=True, IsRequired:=False, Name:="Make", Order:=10)> _
    Public Property Make() As String
        Get
            Return _Make
        End Get
        Set(ByVal value As String)
            _Make = value
        End Set
    End Property

    Private _Model As String
    <DataMember(EmitDefaultValue:=True, IsRequired:=False, Name:="Model", Order:=11)> _
    Public Property Model() As String
        Get
            Return _Model
        End Get
        Set(ByVal value As String)
            _Model = value
        End Set
    End Property

    Private _LowPrice As String
    <DataMember(EmitDefaultValue:=True, IsRequired:=False, Name:="Low_Price", Order:=12)> _
    Public Property Low_Price() As String
        Get
            Return _LowPrice
        End Get
        Set(ByVal value As String)
            _LowPrice = value
        End Set
    End Property

    Private _HighPrice As String
    <DataMember(EmitDefaultValue:=True, IsRequired:=False, Name:="High_Price", Order:=13)> _
    Public Property High_Price() As String
        Get
            Return _HighPrice
        End Get
        Set(ByVal value As String)
            _HighPrice = value
        End Set
    End Property

    Private _ServiceLevelCode As String
    <DataMember(EmitDefaultValue:=True, IsRequired:=False, Name:="Service_Level_Code", Order:=14)> _
    Public Property Service_Level_Code() As String
        Get
            Return _ServiceLevelCode
        End Get
        Set(ByVal value As String)
            _ServiceLevelCode = value
        End Set
    End Property

#End Region

End Class
