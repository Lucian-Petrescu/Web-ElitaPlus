'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (8/17/2007)********************
Imports System.Globalization

Public Class CountryTaxDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_COUNTRY_TAX"
    Public Const TABLE_KEY_NAME As String = "country_tax_id"

    Public Const COL_NAME_COUNTRY_TAX_ID As String = "country_tax_id"
    Public Const COL_NAME_COUNTRY_ID As String = "country_id"
    Public Const COL_NAME_TAX_TYPE_ID As String = "tax_type_id"
    Public Const COL_NAME_EFFECTIVE_DATE As String = "effective_date"
    Public Const COL_NAME_EXPIRATION_DATE As String = "expiration_date"
    Public Const COL_NAME_TAX1_DESCRIPTION As String = "tax1_description"
    Public Const COL_NAME_TAX1_PERCENT_FLAG_ID As String = "tax1_percent_flag_id"
    Public Const COL_NAME_TAX1_COMPUTE_METHOD_ID As String = "tax1_compute_method_id"
    Public Const COL_NAME_TAX1_PERCENT As String = "tax1_percent"
    Public Const COL_NAME_TAX2_DESCRIPTION As String = "tax2_description"
    Public Const COL_NAME_TAX2_COMPUTE_METHOD_ID As String = "tax2_compute_method_id"
    Public Const COL_NAME_TAX2_PERCENT_FLAG_ID As String = "tax2_percent_flag_id"
    Public Const COL_NAME_TAX2_PERCENT As String = "tax2_percent"
    Public Const COL_NAME_TAX3_DESCRIPTION As String = "tax3_description"
    Public Const COL_NAME_TAX3_COMPUTE_METHOD_ID As String = "tax3_compute_method_id"
    Public Const COL_NAME_TAX3_PERCENT_FLAG_ID As String = "tax3_percent_flag_id"
    Public Const COL_NAME_TAX3_PERCENT As String = "tax3_percent"
    Public Const COL_NAME_TAX4_DESCRIPTION As String = "tax4_description"
    Public Const COL_NAME_TAX4_COMPUTE_METHOD_ID As String = "tax4_compute_method_id"
    Public Const COL_NAME_TAX4_PERCENT_FLAG_ID As String = "tax4_percent_flag_id"
    Public Const COL_NAME_TAX4_PERCENT As String = "tax4_percent"
    Public Const COL_NAME_TAX5_DESCRIPTION As String = "tax5_description"
    Public Const COL_NAME_TAX5_COMPUTE_METHOD_ID As String = "tax5_compute_method_id"
    Public Const COL_NAME_TAX5_PERCENT_FLAG_ID As String = "tax5_percent_flag_id"
    Public Const COL_NAME_TAX5_PERCENT As String = "tax5_percent"
    Public Const COL_NAME_TAX6_DESCRIPTION As String = "tax6_description"
    Public Const COL_NAME_TAX6_COMPUTE_METHOD_ID As String = "tax6_compute_method_id"
    Public Const COL_NAME_TAX6_PERCENT_FLAG_ID As String = "tax6_percent_flag_id"
    Public Const COL_NAME_TAX6_PERCENT As String = "tax6_percent"
    Public Const COL_NAME_COMPANY_TYPE_ID As String = "company_type_id"
    Public Const COL_NAME_PRODUCT_TAX_TYPE_ID As String = "product_tax_type_id"
    Public Const COL_NAME_COMPANY_ID As String = "company_id"
    Public Const COL_NAME_APPLY_WITHHOLDING_FLAG As String = "apply_withholding_flag"

    Public Const COL_NAME_LIST_TAX_CODE As String = "tax_code"

    Private Const COL_NAME_RECORDCOUNT As String = "RECORD_COUNT"
    'Infinite date constants
    Public Const INFINITE_DATE_STR As String = "12/31/2999"

    'Get tax rate in params
    Public Const P_COUNTRY_ID = 0
    Public Const P_TAXTYPE_ID = 1
    Public Const P_REGION_ID = 2
    Public Const P_SALES_DATE = 3

    'Get tax rate out params
    Public Const P_SALES_TAX_RATE = 0
    Public Const P_RETURN = 1
    Public Const COL_NAME_P_COUNTRY_ID As String = "p_country_id"
    Public Const COL_NAME_P_TAXTYPE_ID As String = "p_tax_type_id"
    Public Const COL_NAME_P_REGION_ID As String = "p_state_id"
    Public Const COL_NAME_P_SALES_DATE As String = "p_sales_date"
    Public Const COL_NAME_P_SALES_TAX_RATE As String = "p_sales_tax_rate"
    Public Const COL_NAME_P_RETURN As String = "p_return"
    'REQ-1150
    Public Const COL_NAME_DEALER_ID As String = "dealer_id"
    Public Const P_DEALER_ID = 4
    Public Const COL_NAME_P_DEALER_ID As String = "p_dealer_id"
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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("country_tax_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadList(oCountryIds As ArrayList, oCompanyIds As ArrayList, LanguageID As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_LIST")
        Dim whereClauseConditions As String = ""
        Dim inClauseCondition As String = ""

        whereClauseConditions &= Environment.NewLine & " AND " & MiscUtil.BuildListForSql("ct." & COL_NAME_COUNTRY_ID, oCountryIds, False)
        inClauseCondition &= Environment.NewLine & MiscUtil.BuildListForSql(COL_NAME_COMPANY_ID, oCompanyIds, False)
        
        selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
        selectStmt = selectStmt.Replace(DYNAMIC_IN_CLAUSE_PLACE_HOLDER, inClauseCondition)

        Dim parameters As DBHelper.DBHelperParameter()

        parameters = New DBHelper.DBHelperParameter() { _
            New DBHelper.DBHelperParameter("language_id", LanguageID.ToByteArray)}

        Dim ds As New DataSet

        Try
            DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Function LoadMaxExpirationDate(Countryid As Guid, Tax_Type_id As Guid, _
                                        Company_Type_id As Guid, UseCurrentDate As Boolean, _
                                        oProductTaxTypeId As Guid, _
                                        oDealerId As Guid) As Date
        Dim selectStmt As String
        selectStmt = Config("/SQL/MAX_EXPIRATION_DATE")

        Dim DealerIds As New ArrayList()
        DealerIds.Add(oDealerId)

        Dim whereClauseConditions As String = ""
        If Not oDealerId = Guid.Empty Then
            whereClauseConditions &= Environment.NewLine & " AND " & MiscUtil.BuildListForSql(COL_NAME_DEALER_ID, DealerIds, False)
        Else
            whereClauseConditions &= Environment.NewLine & " AND " & COL_NAME_DEALER_ID & " IS NULL"
        End If

        If Not whereClauseConditions = "" Then
            selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
        Else
            selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, "")
        End If

        Dim parameters As DBHelper.DBHelperParameter()

        parameters = New DBHelper.DBHelperParameter() { _
            New DBHelper.DBHelperParameter("COUNTRY_ID", Countryid.ToByteArray), _
            New DBHelper.DBHelperParameter("TAX_TYPE_ID", Tax_Type_id.ToByteArray), _
            New DBHelper.DBHelperParameter("COMPANY_TYPE_ID", Company_Type_id.ToByteArray), _
            New DBHelper.DBHelperParameter("PRODUCT_TAX_TYPE_ID", oProductTaxTypeId.ToByteArray)}

        Dim ds As New DataSet

        Try
            DBHelper.Fetch(ds, selectStmt, "MaxExpDate", parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

        Dim row As DataRow

        If ds.Tables(0).Rows.Count > 0 Then
            row = ds.Tables(0).Rows(0)
            If Not row.Item(COL_NAME_EXPIRATION_DATE) Is DBNull.Value Then
                Return CType(row.Item(COL_NAME_EXPIRATION_DATE), Date)
            Else
                If UseCurrentDate Then
                    Return Date.Now
                Else
                    Return Date.Parse(INFINITE_DATE_STR, CultureInfo.InvariantCulture)
                End If
            End If
        End If


    End Function

    Public Sub LoadMinEffDateMaxExpDate(ByRef MinEffDate As Date, ByRef MaxExpDate As Date, ByRef RcdCount As Integer, _
                                        Countryid As Guid, Tax_Type_id As Guid, _
                                        Company_Type_id As Guid, oProductTaxTypeId As Guid, oDealerId As Guid)
        RcdCount = 0

        Dim selectStmt As String
        selectStmt = Config("/SQL/MINEFFDATE_MAXEXPDATE")

        Dim DealerIds As New ArrayList()
        DealerIds.Add(oDealerId)

        Dim whereClauseConditions As String = ""
        If Not oDealerId = Guid.Empty Then
            whereClauseConditions &= Environment.NewLine & " AND " & MiscUtil.BuildListForSql(COL_NAME_DEALER_ID, DealerIds, False)
        Else
            whereClauseConditions &= Environment.NewLine & " AND " & COL_NAME_DEALER_ID & " IS NULL"
        End If

        If Not whereClauseConditions = "" Then
            selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
        Else
            selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, "")
        End If

        Dim parameters As DBHelper.DBHelperParameter()

        parameters = New DBHelper.DBHelperParameter() { _
            New DBHelper.DBHelperParameter("COUNTRY_ID", Countryid.ToByteArray), _
            New DBHelper.DBHelperParameter("TAX_TYPE_ID", Tax_Type_id.ToByteArray), _
            New DBHelper.DBHelperParameter("COMPANY_TYPE_ID", Company_Type_id.ToByteArray), _
            New DBHelper.DBHelperParameter("PRODUCT_TAX_TYPE_ID", oProductTaxTypeId.ToByteArray)}

        Dim ds As New DataSet

        Try
            DBHelper.Fetch(ds, selectStmt, "DateDetails", parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

        Dim row As DataRow

        If ds.Tables(0).Rows.Count > 0 Then
            row = ds.Tables(0).Rows(0)
            RcdCount = row.Item(COL_NAME_RECORDCOUNT)
            If RcdCount > 0 Then
                If Not row.Item(COL_NAME_EFFECTIVE_DATE) Is DBNull.Value Then
                    MinEffDate = CType(row.Item(COL_NAME_EFFECTIVE_DATE), Date)
                End If
                If Not row.Item(COL_NAME_EXPIRATION_DATE) Is DBNull.Value Then
                    MaxExpDate = CType(row.Item(COL_NAME_EXPIRATION_DATE), Date)
                End If
            End If
        End If

    End Sub

    Public Function LoadTaxRate(guidcountryID As Guid, guidTaxTypeID As Guid, guidRegionID As Guid, dtEffectiveDate As Date, guiddealerID As Guid) As Decimal
        Dim selectStmt As String = Config("/SQL/GET_TAX_RATE")
        Dim inputParameters(4) As DBHelper.DBHelperParameter
        Dim outputParameter(1) As DBHelper.DBHelperParameter
        Dim dTaxRate As Decimal = 0D

        inputParameters(P_COUNTRY_ID) = New DBHelper.DBHelperParameter(COL_NAME_P_COUNTRY_ID, guidcountryID.ToByteArray)
        inputParameters(P_TAXTYPE_ID) = New DBHelper.DBHelperParameter(COL_NAME_P_TAXTYPE_ID, guidTaxTypeID.ToByteArray)
        inputParameters(P_REGION_ID) = New DBHelper.DBHelperParameter(COL_NAME_P_REGION_ID, guidRegionID.ToByteArray)
        inputParameters(P_SALES_DATE) = New DBHelper.DBHelperParameter(COL_NAME_P_SALES_DATE, dtEffectiveDate)
        inputParameters(P_DEALER_ID) = New DBHelper.DBHelperParameter(COL_NAME_DEALER_ID, guiddealerID)

        outputParameter(P_SALES_TAX_RATE) = New DBHelper.DBHelperParameter(COL_NAME_P_SALES_TAX_RATE, GetType(Decimal))
        outputParameter(P_RETURN) = New DBHelper.DBHelperParameter(COL_NAME_P_RETURN, GetType(Integer))

        ' Call DBHelper Store Procedure
        DBHelper.ExecuteSp(selectStmt, inputParameters, outputParameter)
        If outputParameter(P_RETURN).Value <> 0 Then

            '''Throw ex
            Dim strErrorMsg As String
            If outputParameter(P_RETURN).Value = 100 Then
                strErrorMsg = ErrorCodes.INVALID_TAXRATE_BO_ERR
            Else
                strErrorMsg = ErrorCodes.INVALID_TAXRATE_EXCEPTION
            End If

            Throw New StoredProcedureGeneratedException("Data Base Generated Error: ", strErrorMsg)

        Else
            dTaxRate = outputParameter(P_SALES_TAX_RATE).Value / 100
        End If

        Return dTaxRate
    End Function

    Public Function LoadManualTaxes(guidcountryID As Guid, guidTaxTypeCode As String, dtEffectiveDate As Date, guiddealerID As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/GET_MANUAL_TAXES_BY_TAX_TYPE")

        Dim DealerIds As New ArrayList()
        DealerIds.Add(guiddealerID)

        Dim whereClauseConditions As String = ""
        If Not guiddealerID = Guid.Empty Then
            whereClauseConditions &= Environment.NewLine & " AND " & MiscUtil.BuildListForSql(COL_NAME_DEALER_ID, DealerIds, False)
        Else
            whereClauseConditions &= Environment.NewLine & " AND " & COL_NAME_DEALER_ID & " IS NULL"
        End If

        If Not whereClauseConditions = "" Then
            selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
        Else
            selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, "")
        End If

        Dim parameters As DBHelper.DBHelperParameter()
        parameters = New DBHelper.DBHelperParameter() { _
            New DBHelper.DBHelperParameter("COUNTRY_ID", guidcountryID.ToByteArray), _
            New DBHelper.DBHelperParameter("EFFCT_DATE", dtEffectiveDate), _
            New DBHelper.DBHelperParameter("EFFCT_DATE", dtEffectiveDate), _
            New DBHelper.DBHelperParameter("TAX_TYPE_CODE", guidTaxTypeCode)}
        Try
            Dim ds As New DataSet
            DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function
#End Region

#Region "Overloaded Methods"
    Public Overloads Sub Update(ds As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing, Optional ByVal changesFilter As DataRowState = Nothing)
        If ds Is Nothing Then
            Return
        End If
        If Not ds.Tables(TABLE_NAME) Is Nothing Then
            MyBase.Update(ds.Tables(TABLE_NAME), Transaction, changesFilter)
        End If
    End Sub
#End Region

    'Added for Def - 809
#Region "Region Tax Methods"
    Public Function GetRegionTaxCount(guidcountryID As Guid, guidtaxtypeID As Guid, guidproducttaxtypeID As Guid, guiddealerID As Guid) As Integer
        Dim selectStmt As String = Config("/SQL/GET_REGION_TAX_FROM_COUNTRY_TAX")

        Dim parameters As DBHelper.DBHelperParameter()
        parameters = New DBHelper.DBHelperParameter() { _
            New DBHelper.DBHelperParameter("COUNTRY_ID", guidcountryID.ToByteArray), _
            New DBHelper.DBHelperParameter("TAX_TYPE_ID", guidtaxtypeID.ToByteArray), _
            New DBHelper.DBHelperParameter("PRODUCT_TAX_TYPE_ID", guidproducttaxtypeID.ToByteArray), _
            New DBHelper.DBHelperParameter("DEALER_ID", guiddealerID.ToByteArray)}
        Try
            Dim ds As New DataSet
            DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
            If Not ds Is Nothing And ds.Tables(0).Rows.Count > 0 Then
                Return ds.Tables(0).Rows(0).Item(COL_NAME_RECORDCOUNT)
            Else
                Return 0
            End If
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function
#End Region

End Class


