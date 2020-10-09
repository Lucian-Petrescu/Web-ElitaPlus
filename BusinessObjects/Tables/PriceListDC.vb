Imports System.Runtime.Serialization

<DataContract(Name:="PriceList")> _
Public Class PriceListDC

#Region "DataMember"

    Private _ServiceCenterCode As String
    <DataMember(EmitDefaultValue:=True, IsRequired:=True, Name:="Service_Center_Code", Order:=1)> _
    Public Property Service_Center_Code As String
        Get
            Return _ServiceCenterCode
        End Get
        Set
            _ServiceCenterCode = value
        End Set
    End Property

    Private _RiskTypeCode As String
    <DataMember(EmitDefaultValue:=True, IsRequired:=True, Name:="Risk_Type_Code", Order:=2)> _
    Public Property Risk_Type_Code As String
        Get
            Return _RiskTypeCode
        End Get
        Set
            _RiskTypeCode = value
        End Set
    End Property

    Private _MethodOfRepairCode As String
    <DataMember(EmitDefaultValue:=True, IsRequired:=True, Name:="Method_Of_Repair_Code", Order:=3)> _
    Public Property Method_Of_Repair_Code As String
        Get
            Return _MethodOfRepairCode
        End Get
        Set
            _MethodOfRepairCode = value
        End Set
    End Property

    Private _ServiceClassCode As String
    <DataMember(EmitDefaultValue:=True, IsRequired:=True, Name:="Service_Class_Code", Order:=4)> _
    Public Property Service_Class_Code As String
        Get
            Return _ServiceClassCode
        End Get
        Set
            _ServiceClassCode = value
        End Set
    End Property

    Private _ServiceClassTranslation As String
    <DataMember(EmitDefaultValue:=True, IsRequired:=True, Name:="Service_Class_Translation", Order:=5)> _
    Public Property Service_Class_Translation As String
        Get
            Return _ServiceClassTranslation
        End Get
        Set
            _ServiceClassTranslation = value
        End Set
    End Property

    Private _ServiceTypeCode As String
    <DataMember(EmitDefaultValue:=True, IsRequired:=True, Name:="Service_Type_Code", Order:=6)> _
    Public Property Service_Type_Code As String
        Get
            Return _ServiceTypeCode
        End Get
        Set
            _ServiceTypeCode = value
        End Set
    End Property

    Private _ServiceTypeTranslation As String
    <DataMember(EmitDefaultValue:=True, IsRequired:=True, Name:="Service_Type_Translation", Order:=7)> _
    Public Property Service_Type_Translation As String
        Get
            Return _ServiceTypeTranslation
        End Get
        Set
            _ServiceTypeTranslation = value
        End Set
    End Property

    Private _ServiceLevelCode As String
    <DataMember(EmitDefaultValue:=True, IsRequired:=True, Name:="Service_Level_Code", Order:=8)> _
    Public Property Service_Level_Code As String
        Get
            Return _ServiceLevelCode
        End Get
        Set
            _ServiceLevelCode = value
        End Set
    End Property

    Private _ServiceLevelTranslation As String
    <DataMember(EmitDefaultValue:=True, IsRequired:=True, Name:="ServiceLevelTranslation", Order:=9)> _
    Public Property Service_Level_Translation As String
        Get
            Return _ServiceLevelTranslation
        End Get
        Set
            _ServiceLevelTranslation = value
        End Set
    End Property

    Private _LowPrice As Decimal
    <DataMember(EmitDefaultValue:=True, IsRequired:=True, Name:="Low_Price", Order:=10)> _
    Public Property Low_Price As Decimal
        Get
            Return _LowPrice
        End Get
        Set
            _LowPrice = value
        End Set
    End Property

    Private _HighPrice As Decimal
    <DataMember(EmitDefaultValue:=True, IsRequired:=True, Name:="High_Price", Order:=11)> _
    Public Property High_Price As Decimal
        Get
            Return _HighPrice
        End Get
        Set
            _HighPrice = value
        End Set
    End Property

    Private _Price As Decimal
    <DataMember(EmitDefaultValue:=True, IsRequired:=True, Name:="Price", Order:=12)> _
    Public Property Price As Decimal
        Get
            Return _Price
        End Get
        Set
            _Price = value
        End Set
    End Property

#End Region

End Class
