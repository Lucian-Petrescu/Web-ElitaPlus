Public Class ListPriceDAL
    Inherits DALBase

#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_LIST_PRICE"
    Public Const TABLE_KEY_NAME As String = "list_price_id"

    Public Const COL_NAME_LIST_PRICE_ID As String = "list_price_id"
    Public Const COL_NAME_WARRANTY_MASTER_ID As String = "warranty_master_id"
    'Public Const COL_NAME_PRICE As String = "price"
    Public Const COL_NAME_AMOUNT As String = "amount"
    Public Const COL_NAME_AMOUNT_TYPE_ID As String = "amount_type_id"
    Public Const COL_NAME_EFFECTIVE As String = "effective"
    Public Const COL_NAME_EXPIRATION As String = "expiration"
    Public Const COL_NAME_AMOUNT_TYPE_DESC As String = "amount_type_desc"

    Public Const COL_NAME_DEALER_ID As String = "dealer_id"
    Public Const COL_NAME_DEALER_NAME As String = "dealer_name"
    Public Const COL_NAME_DEALER_CODE As String = "dealer_code"
    Public Const COL_NAME_SKU_NUMBER As String = "sku_number"
    Public Const COL_NAME_MODEL_NUMBER As String = "model_number"
    Public Const COL_NAME_MANUFACTURER_NAME As String = "manufacturer_name"

    Public Const COL_NAME_DATE_OF_LOSS As String = "date_of_loss"
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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(COL_NAME_LIST_PRICE_ID, id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, Me.TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadList() As DataSet
        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST_ALL")
        Return DBHelper.Fetch(selectStmt, Me.TABLE_NAME)
    End Function

    Public Function LoadPriceList(ByVal dealerId As Guid, ByVal strManufacturerName As String, _
                             ByVal strModelNumber As String, ByVal strSku As String, ByVal fromDate As String, _
                             ByVal toDate As String) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST_PRICE")
        'Me.Config("/SQL/LOAD_LIST")
        Dim whereClauseConditions As String = ""

        whereClauseConditions &= Environment.NewLine & "AND d.dealer_id = " & MiscUtil.GetDbStringFromGuid(dealerId, True)

        If Me.FormatSearchMask(strSku) Then
            whereClauseConditions &= Environment.NewLine & "AND  UPPER(" & Me.COL_NAME_SKU_NUMBER & ") " & strSku.ToUpper
        End If

        If Me.FormatSearchMask(strManufacturerName) Then
            whereClauseConditions &= Environment.NewLine & "AND  UPPER(" & Me.COL_NAME_MANUFACTURER_NAME & ") " & strManufacturerName.ToUpper
        End If

        If Me.FormatSearchMask(strModelNumber) Then
            whereClauseConditions &= Environment.NewLine & "AND  UPPER(" & Me.COL_NAME_MODEL_NUMBER & ") " & strModelNumber.ToUpper
        End If

        If (Not (fromDate.Equals(String.Empty))) AndAlso (Not (toDate.Equals(String.Empty))) Then
            whereClauseConditions &= Environment.NewLine & " AND (NOT(lp.effective > to_date(" & toDate & ", 'YYYYMMDD') OR " & _
                "lp.expiration < to_date(" & fromDate & ", 'YYYYMMDD')))"
        End If

        If Not whereClauseConditions = "" Then
            selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
        Else
            selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, "")
        End If

        Try
            Return (DBHelper.Fetch(selectStmt, Me.TABLE_NAME))
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

        Return DBHelper.Fetch(selectStmt, Me.TABLE_NAME)
    End Function

    Public Function LoadRepairAuthAmount(ByVal dealerID As Guid, ByVal strSKUNum As String, ByVal dtEffective As Date) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/LOAD_REPAIR_AUTH_AMT")
        Dim ds As New DataSet
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() { _
            New DBHelper.DBHelperParameter("dealer_id", dealerID.ToByteArray), _
            New DBHelper.DBHelperParameter("sku_number", strSKUNum), _
            New DBHelper.DBHelperParameter("effective", dtEffective.ToString("yyyyMMdd"))}
        Try
            DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function LoadListAll(ByVal languageId As Guid, ByVal dealerId As Guid, ByVal strManufacturerName As String, _
                             ByVal strModelNumber As String, ByVal strSku As String, ByVal fromDate As String, _
                             ByVal toDate As String, ByVal AmtTypeID As Guid) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST_ALL")
        Dim whereClauseConditions As String = ""

        whereClauseConditions &= Environment.NewLine & "AND d.dealer_id = " & MiscUtil.GetDbStringFromGuid(dealerId, True)

        If Me.FormatSearchMask(strSku) Then
            whereClauseConditions &= Environment.NewLine & "AND  UPPER(" & Me.COL_NAME_SKU_NUMBER & ") " & strSku.ToUpper
        End If

        If Me.FormatSearchMask(strManufacturerName) Then
            whereClauseConditions &= Environment.NewLine & "AND  UPPER(" & Me.COL_NAME_MANUFACTURER_NAME & ") " & strManufacturerName.ToUpper
        End If

        If Me.FormatSearchMask(strModelNumber) Then
            whereClauseConditions &= Environment.NewLine & "AND  UPPER(" & Me.COL_NAME_MODEL_NUMBER & ") " & strModelNumber.ToUpper
        End If

        If (Not (fromDate.Equals(String.Empty))) AndAlso (Not (toDate.Equals(String.Empty))) Then
            whereClauseConditions &= Environment.NewLine & " AND (NOT(lp.effective > to_date(" & toDate & ", 'YYYYMMDD') OR " & _
                "lp.expiration < to_date(" & fromDate & ", 'YYYYMMDD')))"
        End If

        If AmtTypeID <> Guid.Empty Then
            whereClauseConditions &= Environment.NewLine & "AND lp.amount_type_id = " & MiscUtil.GetDbStringFromGuid(AmtTypeID, True)
        End If

        If Not whereClauseConditions = "" Then
            selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
        Else
            selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, "")
        End If

        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() { _
            New DBHelper.DBHelperParameter("languageId", languageId.ToByteArray)}

        Dim ds As New DataSet
        Try
            'Return (DBHelper.Fetch(selectStmt, Me.TABLE_NAME))
            DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

        Return DBHelper.Fetch(selectStmt, Me.TABLE_NAME)
    End Function
#End Region
End Class
