Imports System.Runtime.Serialization
Imports Assurant.ElitaPlus.BusinessObjectsNew
Imports Assurant.Common

Namespace SpecializedServices.Abag

    <DataContract(Namespace:="http://elita.assurant.com/SpecializedServices/Abag/GetPriceList", Name:="GetPriceListDetailRequest")>
    Public Class GetPriceListDetailRequest

#Region "DataMember"

        Private _InForceDate As DateTime
        <DataMember(EmitDefaultValue:=True, IsRequired:=True, Name:="InForceDate", Order:=1)>
        Public Property InForceDate() As DateTime
            Get
                Return _InForceDate
            End Get
            Set(value As DateTime)
                _InForceDate = value
            End Set
        End Property

        Private _ClaimNumber As String
        <DataMember(EmitDefaultValue:=True, IsRequired:=False, Name:="ClaimNumber", Order:=2)>
        Public Property ClaimNumber() As String
            Get
                Return _ClaimNumber
            End Get
            Set(value As String)
                _ClaimNumber = value
            End Set
        End Property

        Private _ServiceCenterCode As String
        <DataMember(EmitDefaultValue:=True, IsRequired:=False, Name:="ServiceCenterCode", Order:=3)>
        Public Property ServiceCenterCode() As String
            Get
                Return _ServiceCenterCode
            End Get
            Set(value As String)
                _ServiceCenterCode = value
            End Set
        End Property

        Private _RiskTypeCode As String
        <DataMember(EmitDefaultValue:=True, IsRequired:=False, Name:="RiskTypeCode", Order:=4)>
        Public Property RiskTypeCode() As String
            Get
                Return _RiskTypeCode
            End Get
            Set(value As String)
                _RiskTypeCode = value
            End Set
        End Property

        Private _EquipmentClassCode As String
        <DataMember(EmitDefaultValue:=True, IsRequired:=False, Name:="EquipmentClassCode", Order:=5)>
        Public Property EquipmentClassCode() As String
            Get
                Return _EquipmentClassCode
            End Get
            Set(value As String)
                _EquipmentClassCode = value
            End Set
        End Property

        Private _CompanyCode As String
        <DataMember(EmitDefaultValue:=True, IsRequired:=False, Name:="CompanyCode", Order:=6)>
        Public Property CompanyCode() As String
            Get
                Return _CompanyCode
            End Get
            Set(value As String)
                _CompanyCode = value
            End Set
        End Property

        Private _DealerCode As String
        <DataMember(EmitDefaultValue:=True, IsRequired:=False, Name:="DealerCode", Order:=7)>
        Public Property DealerCode() As String
            Get
                Return _DealerCode
            End Get
            Set(value As String)
                _DealerCode = value
            End Set
        End Property

        Private _ServiceClassCode As String
        <DataMember(EmitDefaultValue:=True, IsRequired:=False, Name:="ServiceClassCode", Order:=8)>
        Public Property ServiceClassCode() As String
            Get
                Return _ServiceClassCode
            End Get
            Set(value As String)
                _ServiceClassCode = value
            End Set
        End Property

        Private _ServiceTypeCode As String
        <DataMember(EmitDefaultValue:=True, IsRequired:=False, Name:="ServiceTypeCode", Order:=9)>
        Public Property ServiceTypeCode() As String
            Get
                Return _ServiceTypeCode
            End Get
            Set(value As String)
                _ServiceTypeCode = value
            End Set
        End Property

        Private _Make As String
        <DataMember(EmitDefaultValue:=True, IsRequired:=False, Name:="Make", Order:=10)>
        Public Property Make() As String
            Get
                Return _Make
            End Get
            Set(value As String)
                _Make = value
            End Set
        End Property

        Private _Model As String
        <DataMember(EmitDefaultValue:=True, IsRequired:=False, Name:="Model", Order:=11)>
        Public Property Model() As String
            Get
                Return _Model
            End Get
            Set(value As String)
                _Model = value
            End Set
        End Property

        Private _LowPrice As String
        <DataMember(EmitDefaultValue:=True, IsRequired:=False, Name:="LowPrice", Order:=12)>
        Public Property LowPrice() As String
            Get
                Return _LowPrice
            End Get
            Set(value As String)
                _LowPrice = value
            End Set
        End Property

        Private _HighPrice As String
        <DataMember(EmitDefaultValue:=True, IsRequired:=False, Name:="HighPrice", Order:=13)>
        Public Property HighPrice() As String
            Get
                Return _HighPrice
            End Get
            Set(value As String)
                _HighPrice = value
            End Set
        End Property

        Private _ServiceLevelCode As String
        <DataMember(EmitDefaultValue:=True, IsRequired:=False, Name:="ServiceLevelCode", Order:=14)>
        Public Property ServiceLevelCode() As String
            Get
                Return _ServiceLevelCode
            End Get
            Set(value As String)
                _ServiceLevelCode = value
            End Set
        End Property

#End Region

    End Class
End Namespace

