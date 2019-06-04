'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (9/22/2004)********************

Public Class ProductConversionData

    Public companyIds As ArrayList
    Public dealerId As Guid
    Public externalProductCode As String
    Public productCodeId As Guid

End Class


Public Class ProductCodeConversionDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_PRODUCT_CONVERSION"
    Public Const TABLE_KEY_NAME As String = "product_conversion_id"
    Public Const DSNAME As String = "LIST"

    Public Const COL_NAME_PRODUCT_CONVERSION_ID As String = "product_conversion_id"
    Public Const COL_NAME_DEALER_ID As String = "dealer_id"
    Public Const COL_NAME_EXTERNAL_PROD_CODE As String = "external_prod_code"
    Public Const COL_NAME_PRODUCT_CODE_ID As String = "product_code_id"
    Public Const COL_NAME_COMPANY_ID As String = "company_id"
    Public Const COL_NAME_PRODUCT_CODE As String = "product_code"
    Public Const COL_NAME_DEALER_NAME As String = "dealer_name"
    Public Const COL_NAME_CERTIFICATE_DURATION As String = "certificate_duration"
    Public Const COL_NAME_MANUFACTURER_WARRANTY As String = "manufacturer_warranty"
    Public Const COL_NAME_GROSS_AMOUNT As String = "gross_amount"
    Public Const COL_NAME_MANUFACTURER As String = "manufacturer"
    Public Const COL_NAME_MODEL As String = "model"
    Public Const COL_NAME_SALES_PRICE As String = "sales_price"


    Public Const DEALER_ID = 0
    Public Const PRODUCT_CODE_ID = 1
    Public Const EXTERNAL_PROD_CODE_ID = 2
    Public Const TOTAL_PARAM = 2 '3 '4

    Public Const WILDCARD As Char = "%"

#End Region

#Region "Constructors"
    Public Sub New()
        MyBase.new()
    End Sub

#End Region

#Region "Load Methods"

    Public Sub LoadSchema(ByVal ds As DataSet)
        Load(ds, Guid.Empty)
    End Sub

    Public Sub Load(ByVal familyDS As DataSet, ByVal id As Guid)
        Dim selectStmt As String = Me.Config("/SQL/LOAD")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("product_conversion_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, Me.TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    'Public Function LoadList(ByVal ds As DataSet) As DataSet
    '    Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST")
    '    Return DBHelper.Fetch(selectStmt, Me.TABLE_NAME)
    'End Function

    Public Function LoadList(ByVal oProductConversionData As ProductConversionData) As DataSet
        Dim selectStmt As String
        If oProductConversionData.companyIds.Count > 1 Then
            selectStmt = Me.Config("/SQL/LOAD_LIST_MULTIPLE_COMPANIES")
        Else
            selectStmt = Me.Config("/SQL/LOAD_LIST")
        End If

        Dim parameters(TOTAL_PARAM) As OracleParameter

        With oProductConversionData

            'parameters(COMPANY_ID) = New OracleParameter(COL_NAME_COMPANY_ID, .companyId.ToByteArray)
            If .dealerId.Equals(Guid.Empty) Then
                parameters(DEALER_ID) = New OracleParameter(COL_NAME_DEALER_ID, WILDCARD)
            Else
                parameters(DEALER_ID) = New OracleParameter(COL_NAME_DEALER_ID, .dealerId.ToByteArray)
            End If

            If .productCodeId.Equals(Guid.Empty) Then
                parameters(PRODUCT_CODE_ID) = New OracleParameter(COL_NAME_PRODUCT_CODE_ID, WILDCARD)
            Else
                parameters(PRODUCT_CODE_ID) = New OracleParameter(COL_NAME_PRODUCT_CODE_ID, .productCodeId.ToByteArray)
            End If

            '.externalProductCode &= WILDCARD
            Dim extProdCode As String = .externalProductCode
            .externalProductCode = GetFormattedSearchStringForSQL(extProdCode)
            parameters(EXTERNAL_PROD_CODE_ID) = New OracleParameter(COL_NAME_EXTERNAL_PROD_CODE, .externalProductCode)

            Dim inClauseCondition As String = MiscUtil.BuildListForSql("AND d." & Me.COL_NAME_COMPANY_ID, .companyIds, True)
            selectStmt = selectStmt.Replace(Me.DYNAMIC_IN_CLAUSE_PLACE_HOLDER, inClauseCondition)

        End With

        Try
            Return (DBHelper.Fetch(selectStmt, DSNAME, Me.TABLE_NAME, parameters))
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function LoadListWithDescByDealer(ByVal DealerID As Guid) As DataSet
        Dim selectStmt As String
        selectStmt = Me.Config("/SQL/LOAD_LIST_WITH_DESC")
        Dim ds As DataSet = New DataSet
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("dealer_id", DealerID.ToByteArray)}
        Try
            DBHelper.Fetch(ds, selectStmt, "ProductCode", parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function CheckForDealerProdCodeMfgCombination(ByVal DealerID As Guid, ByVal ExtProdCode As String, ByVal Manufacturer As String, _
                                                         ByVal ProductConversionId As Guid) As DataSet
        Dim selectStmt As String
        Dim whereClauseConditions As String = ""
       
        selectStmt = Me.Config("/SQL/CHK_DEALER_PRODCODE_MFG_COMBINATION")
        Dim ds As DataSet = New DataSet
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("dealer_id", DealerID.ToByteArray), _
                                                                                           New DBHelper.DBHelperParameter("external_prod_code", ExtProdCode), _                                                                                           
                                                                                           New DBHelper.DBHelperParameter("product_conversion_id", ProductConversionId.ToByteArray)}

        'New DBHelper.DBHelperParameter("manufacturer", Manufacturer), _
        If Not (Manufacturer Is Nothing OrElse Manufacturer.Equals(String.Empty)) Then
            whereClauseConditions &= Environment.NewLine & " AND manufacturer = '" & Manufacturer & "'"
        End If

        If Not whereClauseConditions = "" Then
            selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
        Else
            selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, "")
        End If

        Try
            DBHelper.Fetch(ds, selectStmt, "ProductCodeConv", parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

#End Region


    Public Overloads Sub Update(ByVal ds As DataSet, Optional ByVal transaction As IDbTransaction = Nothing)
        DBHelper.Execute(ds.Tables(Me.TABLE_NAME), Config("/SQL/INSERT"), Config("/SQL/UPDATE"), Config("/SQL/DELETE"), Nothing, transaction)
    End Sub

End Class


