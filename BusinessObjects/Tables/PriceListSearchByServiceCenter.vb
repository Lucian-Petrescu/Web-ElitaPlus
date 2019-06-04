Public Class PriceListSearchByServiceCenter
    Inherits BusinessObjectBase
    Implements IPriceListSearch

#Region "Constructors"

    Public Sub New()
        MyBase.New()
    End Sub
    Public Sub New(ByVal oSearch As PriceListSearchDC)
        MyBase.New()
        Me.InForceDate = oSearch.In_Force_Date
        Me.ServiceCenterCode = oSearch.Service_Center_Code
        Me.EquipmentClassCode = oSearch.Equipment_Class_Code
        Me.RiskTypeCode = oSearch.Risk_Type_Code
        Me.DealerCode = oSearch.Dealer_Code
        Me.CompanyCode = oSearch.Company_Code
        Me.ServiceClassCode = oSearch.Service_Class_Code
        Me.ServiceTypeCode = oSearch.Service_Type_Code
        Me.Make = oSearch.Make
        Me.Model = oSearch.Model
        Me.LowPrice = oSearch.Low_Price
        Me.HighPrice = oSearch.High_Price
        Me.ServiceLevelCode = oSearch.Service_Level_Code
        Me.Validate()
    End Sub

#End Region

#Region "Public Members"

    Public Sub Validate()
        If (Me.InForceDate = Nothing Or Me.ServiceCenterCode = Nothing Or Me.CompanyCode = Nothing) And _
            (Me.RiskTypeCode = Nothing And Me.EquipmentClassCode = Nothing) Then
            Throw New BOValidationException("GetPriceList Error: Must provide In Force Date, Service Center Code and Company Code along with Risk Type Code or Equipment Class Code", Assurant.ElitaPlus.Common.ErrorCodes.WS_PRICELIST_INVALID_SERVICE_CENTER_DTLS_INPUT)
        End If

        Dim oServiceCenter As New ServiceCenter(Me.ServiceCenterCode)
        If oServiceCenter Is Nothing Then
            Throw New BOValidationException("GetPriceList Error: Invalid Service Center Code ", Common.ErrorCodes.INVALID_SERVICE_CENTER_CODE)
        End If

        Dim companyId As Guid = GetCompanyId(Me.CompanyCode)
        If companyId.Equals(Guid.Empty) Then
            Throw New BOValidationException("GetPriceList Error: Invalid Company Code ", Assurant.ElitaPlus.Common.ErrorCodes.WS_INVALID_COMPANY_CODE)
        End If

        If (Not Me.DealerCode = Nothing) And (GetDealerID(companyId, Me.DealerCode).Equals(Guid.Empty)) Then
            Throw New BOValidationException("GetPriceList Error: Invalid Dealer Code ", Assurant.ElitaPlus.Common.ErrorCodes.INVALID_DEALER_CODE)
        End If

    End Sub

    Public Shared Function GetCompanyId(ByVal Companycode As String) As Guid
        Dim companyId As System.Guid = Guid.Empty
        Dim oUser As New User(ElitaPlusIdentity.Current.ActiveUser.NetworkId)
        Dim userAssignedCompaniesDv As DataView = oUser.GetSelectedAssignedCompanies(ElitaPlusIdentity.Current.ActiveUser.Id)

        For i = 0 To userAssignedCompaniesDv.Count - 1
            Dim oCompanyId As New Guid(CType(userAssignedCompaniesDv.Table.Rows(i)("COMPANY_ID"), Byte()))
            If Not oCompanyId = Nothing AndAlso userAssignedCompaniesDv.Table.Rows(i)("CODE").Equals(Companycode.ToUpper) Then
                companyId = oCompanyId
                Exit For
            End If
        Next
        Return companyId

    End Function

    Public Shared Function GetDealerID(ByVal companyId As Guid, ByVal dealerCode As String) As Guid
        Dim dealerId As Guid = Guid.Empty
        Dim ds As DataSet
        
        ds = Company.GetDealerFromCompany(companyId, dealerCode)
        If (ds.Tables(0).Rows.Count = 0) Then
            Return Guid.Empty
        Else
            Return New Guid(CType(ds.Tables(0).Rows(0)("DEALER_ID"), Byte()))
        End If

    End Function

    Public Function GetPriceList(ByVal oPriceListSearch As PriceListSearchDC) As DataSet Implements IPriceListSearch.GetPriceList
        Try
            Dim dal As New PriceListDetailDAL
            Return dal.GetPriceList(Me.InForceDate, "", Me.CompanyCode, Me.ServiceCenterCode, Me.RiskTypeCode, Me.EquipmentClassCode, Me.DealerCode, Me.ServiceClassCode, Me.ServiceTypeCode, Make, Model, LowPrice, HighPrice, ServiceLevelCode)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function
#End Region

#Region "Properties"
    Private _InForceDate As DateTime
    Public Property InForceDate() As DateTime
        Get
            Return _InForceDate
        End Get
        Set(ByVal value As DateTime)
            _InForceDate = value
        End Set
    End Property

    Private _ServiceCenterCode As String
    Public Property ServiceCenterCode() As String
        Get
            Return _ServiceCenterCode
        End Get
        Set(ByVal value As String)
            _ServiceCenterCode = value
        End Set
    End Property

    Private _Risk_Type_Code As String
    Public Property RiskTypeCode() As String
        Get
            Return _Risk_Type_Code
        End Get
        Set(ByVal value As String)
            _Risk_Type_Code = value
        End Set
    End Property


    Private _Equipment_Class_Code As String
    Public Property EquipmentClassCode() As String
        Get
            Return _Equipment_Class_Code
        End Get
        Set(ByVal value As String)
            _Equipment_Class_Code = value
        End Set
    End Property

    Private _CompanyCode As String
    Public Property CompanyCode() As String
        Get
            Return _CompanyCode
        End Get
        Set(ByVal value As String)
            _CompanyCode = value
        End Set
    End Property

    Private _DealerCode As String
    Public Property DealerCode() As String
        Get
            Return _DealerCode
        End Get
        Set(ByVal value As String)
            _DealerCode = value
        End Set
    End Property


    Private _ServiceClassCode As String
    Public Property ServiceClassCode() As String
        Get
            Return _ServiceClassCode
        End Get
        Set(ByVal value As String)
            _ServiceClassCode = value
        End Set
    End Property

    Private _ServiceTypeCode As String
    Public Property ServiceTypeCode() As String
        Get
            Return _ServiceTypeCode
        End Get
        Set(ByVal value As String)
            _ServiceTypeCode = value
        End Set
    End Property

    Private _Make As String
    Public Property Make() As String
        Get
            Return _Make
        End Get
        Set(ByVal value As String)
            _Make = value
        End Set
    End Property

    Private _Model As String
    Public Property Model() As String
        Get
            Return _Model
        End Get
        Set(ByVal value As String)
            _Model = value
        End Set
    End Property

    Private _LowPrice As String
    Public Property LowPrice() As String
        Get
            Return _LowPrice
        End Get
        Set(ByVal value As String)
            _LowPrice = value
        End Set
    End Property

    Private _HighPrice As String
    Public Property HighPrice() As String
        Get
            Return _HighPrice
        End Get
        Set(ByVal value As String)
            _HighPrice = value
        End Set
    End Property

    Private _ServiceLevelCode As String
    <ValidStringLength("", Max:=50)> _
    Public Property ServiceLevelCode() As String
        Get
            Return _ServiceLevelCode
        End Get
        Set(ByVal value As String)
            _ServiceLevelCode = value
        End Set
    End Property


#End Region

End Class
