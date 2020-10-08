Public Class PriceListSearchByClaim
    Inherits BusinessObjectBase
    Implements IPriceListSearch

#Region "Constructors"

    Public Sub New()
        MyBase.New()
    End Sub
    Public Sub New(oSearch As PriceListSearchDC)
        MyBase.New()
        InForceDate = oSearch.In_Force_Date
        ClaimNumber = oSearch.Claim_Number
        CompanyCode = oSearch.Company_Code
        ServiceClassCode = oSearch.Service_Class_Code
        ServiceTypeCode = oSearch.Service_Type_Code
        Make = oSearch.Make
        Model = oSearch.Model
        LowPrice = oSearch.Low_Price
        HighPrice = oSearch.High_Price
        ServiceLevelCode = oSearch.Service_Level_Code
        Validate()
    End Sub

#End Region

#Region "Public Members"

    Public Sub Validate()
        If InForceDate = Nothing Or CompanyCode = Nothing Or ClaimNumber = Nothing Then
            Throw New BOValidationException("GetPriceList Error: Must provide In Force Date, Company Code and Claim Number", Assurant.ElitaPlus.Common.ErrorCodes.WS_PRICELIST_INVALID_CLAIM_DTLS_INPUT)
        End If
        If GetCompanyId(CompanyCode).Equals(Guid.Empty) Then
            Throw New BOValidationException("GetPriceList Error: Invalid Company Code ", Assurant.ElitaPlus.Common.ErrorCodes.WS_INVALID_COMPANY_CODE)
        End If
    End Sub

    Public Shared Function GetCompanyId(Companycode As String) As Guid
        Dim oUser As New User(ElitaPlusIdentity.Current.ActiveUser.NetworkId)
        Dim userAssignedCompaniesDv As DataView = oUser.GetSelectedAssignedCompanies(ElitaPlusIdentity.Current.ActiveUser.Id)
        Dim companyId As System.Guid = Guid.Empty

        For i = 0 To userAssignedCompaniesDv.Count - 1
            Dim oCompanyId As New Guid(CType(userAssignedCompaniesDv.Table.Rows(i)("COMPANY_ID"), Byte()))
            If Not oCompanyId = Nothing AndAlso userAssignedCompaniesDv.Table.Rows(i)("CODE").Equals(Companycode.ToUpper) Then
                companyId = oCompanyId
                Exit For
            End If
        Next
        If companyId.Equals(Guid.Empty) Then
            Return Guid.Empty
        Else
            Return companyId
        End If
    End Function

    Public Function GetPriceList(oPriceListSearch As PriceListSearchDC) As DataSet Implements IPriceListSearch.GetPriceList
        Try
            Dim dal As New PriceListDetailDAL
            Return dal.GetPriceList(InForceDate, ClaimNumber, CompanyCode, "", "", "", "", ServiceClassCode, ServiceTypeCode, Make, Model, LowPrice, HighPrice, ServiceLevelCode)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

#End Region
    
#Region "Properties"
    Private _InForceDate As DateTime
    Public Property InForceDate As DateTime
        Get
            Return _InForceDate
        End Get
        Set
            _InForceDate = value
        End Set
    End Property

    Private _ClaimNumber As String
    Public Property ClaimNumber As String
        Get
            Return _ClaimNumber
        End Get
        Set
            _ClaimNumber = value
        End Set
    End Property

    Private _CompanyCode As String
    Public Property CompanyCode As String
        Get
            Return _CompanyCode
        End Get
        Set
            _CompanyCode = value
        End Set
    End Property

    Private _ServiceClassCode As String
    Public Property ServiceClassCode As String
        Get
            Return _ServiceClassCode
        End Get
        Set
            _ServiceClassCode = value
        End Set
    End Property

    Private _ServiceTypeCode As String
    Public Property ServiceTypeCode As String
        Get
            Return _ServiceTypeCode
        End Get
        Set
            _ServiceTypeCode = value
        End Set
    End Property

    Private _Make As String
    Public Property Make As String
        Get
            Return _Make
        End Get
        Set
            _Make = value
        End Set
    End Property

    Private _Model As String
    Public Property Model As String
        Get
            Return _Model
        End Get
        Set
            _Model = value
        End Set
    End Property

    Private _LowPrice As String
    Public Property LowPrice As String
        Get
            Return _LowPrice
        End Get
        Set
            _LowPrice = value
        End Set
    End Property

    Private _HighPrice As String
    Public Property HighPrice As String
        Get
            Return _HighPrice
        End Get
        Set
            _HighPrice = value
        End Set
    End Property

    Private _ServiceLevelCode As String
    <ValidStringLength("", Max:=50)> _
    Public Property ServiceLevelCode As String
        Get
            Return _ServiceLevelCode
        End Get
        Set
            _ServiceLevelCode = value
        End Set
    End Property


#End Region

End Class
