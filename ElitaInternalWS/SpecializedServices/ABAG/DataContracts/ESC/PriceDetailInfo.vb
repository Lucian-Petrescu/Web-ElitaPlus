Imports System.Runtime.Serialization

Namespace SpecializedServices.Abag

    <DataContract(Namespace:="http://elita.assurant.com/SpecializedServices/Abag/PriceDetailInfo", Name:="PriceDetailInfo")>
    Public Class PriceDetailInfo


#Region "DataMember"

        Private _ServiceCenterCode As String
        <DataMember(EmitDefaultValue:=True, IsRequired:=True, Name:="ServiceCenterCode", Order:=1)>
        Public Property Service_Center_Code() As String
            Get
                Return _ServiceCenterCode
            End Get
            Set(ByVal value As String)
                _ServiceCenterCode = value
            End Set
        End Property

        Private _RiskTypeCode As String
        <DataMember(EmitDefaultValue:=True, IsRequired:=True, Name:="RiskTypeCode", Order:=2)>
        Public Property Risk_Type_Code() As String
            Get
                Return _RiskTypeCode
            End Get
            Set(ByVal value As String)
                _RiskTypeCode = value
            End Set
        End Property

        Private _MethodOfRepairCode As String
        <DataMember(EmitDefaultValue:=True, IsRequired:=True, Name:="MethodOfRepairCode", Order:=3)>
        Public Property Method_Of_Repair_Code() As String
            Get
                Return _MethodOfRepairCode
            End Get
            Set(ByVal value As String)
                _MethodOfRepairCode = value
            End Set
        End Property

        Private _ServiceClassCode As String
        <DataMember(EmitDefaultValue:=True, IsRequired:=True, Name:="ServiceClassCode", Order:=4)>
        Public Property Service_Class_Code() As String
            Get
                Return _ServiceClassCode
            End Get
            Set(ByVal value As String)
                _ServiceClassCode = value
            End Set
        End Property

        Private _ServiceClassTranslation As String
        <DataMember(EmitDefaultValue:=True, IsRequired:=True, Name:="ServiceClassTranslation", Order:=5)>
        Public Property Service_Class_Translation() As String
            Get
                Return _ServiceClassTranslation
            End Get
            Set(ByVal value As String)
                _ServiceClassTranslation = value
            End Set
        End Property

        Private _ServiceTypeCode As String
        <DataMember(EmitDefaultValue:=True, IsRequired:=True, Name:="ServiceTypeCode", Order:=6)>
        Public Property Service_Type_Code() As String
            Get
                Return _ServiceTypeCode
            End Get
            Set(ByVal value As String)
                _ServiceTypeCode = value
            End Set
        End Property

        Private _ServiceTypeTranslation As String
        <DataMember(EmitDefaultValue:=True, IsRequired:=True, Name:="ServiceTypeTranslation", Order:=7)>
        Public Property Service_Type_Translation() As String
            Get
                Return _ServiceTypeTranslation
            End Get
            Set(ByVal value As String)
                _ServiceTypeTranslation = value
            End Set
        End Property

        Private _ServiceLevelCode As String
        <DataMember(EmitDefaultValue:=True, IsRequired:=True, Name:="ServiceLevelCode", Order:=8)>
        Public Property Service_Level_Code() As String
            Get
                Return _ServiceLevelCode
            End Get
            Set(ByVal value As String)
                _ServiceLevelCode = value
            End Set
        End Property

        Private _ServiceLevelTranslation As String
        <DataMember(EmitDefaultValue:=True, IsRequired:=True, Name:="ServiceLevelTranslation", Order:=9)>
        Public Property Service_Level_Translation() As String
            Get
                Return _ServiceLevelTranslation
            End Get
            Set(ByVal value As String)
                _ServiceLevelTranslation = value
            End Set
        End Property

        Private _LowPrice As Decimal
        <DataMember(EmitDefaultValue:=True, IsRequired:=True, Name:="LowPrice", Order:=10)>
        Public Property Low_Price() As Decimal
            Get
                Return _LowPrice
            End Get
            Set(ByVal value As Decimal)
                _LowPrice = value
            End Set
        End Property

        Private _HighPrice As Decimal
        <DataMember(EmitDefaultValue:=True, IsRequired:=True, Name:="HighPrice", Order:=11)>
        Public Property High_Price() As Decimal
            Get
                Return _HighPrice
            End Get
            Set(ByVal value As Decimal)
                _HighPrice = value
            End Set
        End Property

        Private _Price As Decimal
        <DataMember(EmitDefaultValue:=True, IsRequired:=True, Name:="Price", Order:=12)>
        Public Property Price() As Decimal
            Get
                Return _Price
            End Get
            Set(ByVal value As Decimal)
                _Price = value
            End Set
        End Property

#End Region


    End Class

End Namespace

