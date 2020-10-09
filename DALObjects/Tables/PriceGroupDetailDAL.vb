'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (10/13/2004)********************


Public Class PriceGroupDetailDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_PRICE_GROUP_DETAIL"
    Public Const TABLE_KEY_NAME As String = "price_group_detail_id"

    Public Const COL_NAME_PRICE_GROUP_DETAIL_ID As String = "price_group_detail_id"
    Public Const COL_NAME_PRICE_GROUP_ID As String = "price_group_id"
    Public Const COL_NAME_RISK_TYPE_ID As String = "risk_type_id"
    Public Const COL_NAME_RISK_TYPE_DESC As String = "risk_type_desc"
    Public Const COL_NAME_EFFECTIVE_DATE As String = "effective_date"
    Public Const COL_NAME_HOME_PRICE As String = "home_price"
    Public Const COL_NAME_CARRY_IN_PRICE As String = "carry_in_price"
    Public Const COL_NAME_SEND_IN_PRICE As String = "send_in_price"
    Public Const COL_NAME_PICK_UP_PRICE As String = "pick_up_price"
    Public Const COL_NAME_CLEANING_PRICE As String = "cleaning_price"
    Public Const COL_NAME_HOURLY_RATE As String = "hourly_rate"
    Public Const COL_NAME_ESTIMATE_PRICE As String = "estimate_price"
    Public Const COL_NAME_REPLACEMENT_PRICE As String = "replacement_price"
    Public Const COL_NAME_COMPANY_GROUP_ID As String = "company_group_id"
    Public Const COL_NAME_PRICE_BAND_RANGE_FROM As String = "price_band_range_from"
    Public Const COL_NAME_PRICE_BAND_RANGE_TO As String = "price_band_range_to"
    Public Const COL_NAME_REPLACEMENT_TAX_TYPE As String = "replacement_tax_type"
    Public Const COL_NAME_DISCOUNTED_PRICE As String = "discounted_price"

    Public Const PRICE_GROUP_ID = 0
    Public Const RISKTYPE_ID = 1
    Public Const COMPANYGROUP_ID = 1
    Public Const TOTAL_RISK_PARAM = 1 '2
#End Region

#Region "Constructors"
    Public Sub New()
        MyBase.new()
    End Sub

#End Region

#Region "Load Methods"

    Public Sub LoadSchema(ds As DataSet)
        Load(ds, Guid.Empty)
    End Sub

    Public Sub Load(familyDS As DataSet, id As Guid)
        Dim selectStmt As String = Config("/SQL/LOAD")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("price_group_detail_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadList(familyDs As DataSet, priceGroupId As Guid)
        Dim selectStmt As String = Config("/SQL/LOAD_LIST")
        Dim priceGroupParam As New DBHelper.DBHelperParameter(COL_NAME_PRICE_GROUP_ID, priceGroupId)
        DBHelper.Fetch(familyDs, selectStmt, TABLE_NAME, New DBHelper.DBHelperParameter() {priceGroupParam})
    End Function

    Public Function LoadList(familyDs As DataSet, priceGroupId As Guid, riskTypeId As Guid)
        Dim selectStmt As String = Config("/SQL/LOAD_RISKTYPE_LIST")
        Dim parameters(TOTAL_RISK_PARAM) As DBHelper.DBHelperParameter

        parameters(PRICE_GROUP_ID) = New DBHelper.DBHelperParameter(COL_NAME_PRICE_GROUP_ID, priceGroupId.ToByteArray)
        parameters(RISKTYPE_ID) = New DBHelper.DBHelperParameter(COL_NAME_RISK_TYPE_ID, riskTypeId.ToByteArray)

        DBHelper.Fetch(familyDs, selectStmt, TABLE_NAME, parameters)
    End Function

    Public Function LoadList(familyDs As DataSet, priceGroupId As Guid, companygroupid As Guid, flag As Boolean)
        Dim selectStmt As String = Config("/SQL/LOAD_RISKTYPE_COMPANYGROUP_LIST")
        Dim parameters(TOTAL_RISK_PARAM) As DBHelper.DBHelperParameter

        parameters(PRICE_GROUP_ID) = New DBHelper.DBHelperParameter(COL_NAME_PRICE_GROUP_ID, priceGroupId.ToByteArray)
        parameters(COMPANYGROUP_ID) = New DBHelper.DBHelperParameter(COL_NAME_COMPANY_GROUP_ID, companygroupid.ToByteArray)

        DBHelper.Fetch(familyDs, selectStmt, TABLE_NAME, parameters)
    End Function

    Public Function GetList(priceGroupId As Guid, riskTypeId As Guid, effectiveDate As Date) As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_PRICEBAND_LIST")
        Dim ds As New DataSet
        Dim parameters() As DBHelper.DBHelperParameter
        parameters = New DBHelper.DBHelperParameter() { _
                              New DBHelper.DBHelperParameter(COL_NAME_PRICE_GROUP_ID, priceGroupId.ToByteArray), _
                              New DBHelper.DBHelperParameter(COL_NAME_RISK_TYPE_ID, riskTypeId.ToByteArray), _
                              New DBHelper.DBHelperParameter(COL_NAME_EFFECTIVE_DATE, effectiveDate)}

        Return DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
    End Function
#End Region

#Region "Overloaded Methods"
    Public Overloads Sub Update(ds As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing, Optional ByVal changesFilter As DataRowState = Nothing)
        If Not ds.Tables(TABLE_NAME) Is Nothing Then
            MyBase.Update(ds.Tables(TABLE_NAME), Transaction, changesFilter)
        End If
    End Sub
#End Region


End Class



