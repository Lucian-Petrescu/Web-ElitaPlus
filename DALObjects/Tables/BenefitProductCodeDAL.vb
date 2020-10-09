Public Class BenefitProductCodeDAL
    Inherits DALBase
#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_BEN_PRODUCT_CODE"
    Public Const TABLE_KEY_NAME As String = "ben_product_code_id"



    Public Const COL_NAME_BEN_PRODUCT_CODE_ID As String = "ben_product_code_id"
    'Public Const COL_NAME_PRODUCT_CODE_ID As String = "product_code_id"
    Public Const COL_NAME_DEALER_ID As String = "dealer_id"
    Public Const COL_NAME_VENDOR_ID As String = "vendor_id"
    Public Const COL_NAME_BEN_PRODUCT_CODE As String = "ben_product_code"
    Public Const COL_NAME_CURRENCY_ISO_CODE As String = "currency_iso_code"
    Public Const COL_NAME_PRICE_UOM As String = "price_uom"
    Public Const COL_NAME_NET_PRICE As String = "net_price"
    Public Const COL_NAME_TAX_TYPE_XCD As String = "tax_type_xcd"
    Public Const COL_NAME_DURATIONINMONTH As String = "durationinmonth"
    Public Const COL_NAME_EFFECTIVE_DATE As String = "effective_date"
    Public Const COL_NAME_EXPIRATION_DATE As String = "expiration_date"
    Public Const COL_NAME_VENDOR_BILLABLE_PART_NUM As String = "vendor_billable_part_num"
    Public Const COL_NAME_DAYSTOEXPIREAFTERENDDAY As String = "daystoexpireafterendday"
    Public Const COL_NAME_CREATED_DATE As String = "created_date"
    Public Const COL_NAME_MODIFIED_DATE As String = "modified_date"
    Public Const COL_NAME_CREATED_BY As String = "created_by"
    Public Const COL_NAME_MODIFIED_BY As String = "modified_by"

    Public Const COL_NAME_COMPANY_ID As String = "company_id"
#End Region
#Region "Constructors"
    Public Sub New()
        MyBase.New()
    End Sub
#End Region
#Region "Load Methods"
    Public Sub LoadSchema(ByRef ds As DataSet)
        Load(ds, Guid.Empty)
    End Sub
    Public Sub Load(familyDS As DataSet, id As Guid)
        Dim selectStmt As String = Config("/SQL/LOAD")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(COL_NAME_BEN_PRODUCT_CODE_ID, id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadList(compIds As ArrayList, dealerId As Guid,
                             benefitProductCodeMask As String, LanguageId As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_LIST")
        Dim parameters() As DBHelper.DBHelperParameter
        Dim inClausecondition As String = ""
        Dim whereClauseConditions As String = ""
        Dim bIsLikeClause As Boolean = False

        inClausecondition &= "And edealer." & MiscUtil.BuildListForSql(COL_NAME_COMPANY_ID, compIds, False)

        benefitProductCodeMask = benefitProductCodeMask.Trim()
        If (Not benefitProductCodeMask.Equals(String.Empty) Or Not benefitProductCodeMask = "") AndAlso (FormatSearchMask(benefitProductCodeMask)) Then
            whereClauseConditions &= Environment.NewLine & "AND " & "Upper(pc.ben_product_code)" & benefitProductCodeMask.ToUpper
        End If

        If Not dealerId.Equals(Guid.Empty) Then
            whereClauseConditions &= Environment.NewLine & "AND " & "edealer.dealer_id = " & MiscUtil.GetDbStringFromGuid(dealerId)
        End If

        parameters = New DBHelper.DBHelperParameter() _
                                    {New DBHelper.DBHelperParameter(COL_NAME_LANGUAGE_ID, LanguageId.ToByteArray)}


        If Not inClausecondition = "" Then
            selectStmt = selectStmt.Replace(DYNAMIC_IN_CLAUSE_PLACE_HOLDER, inClausecondition)
        Else
            selectStmt = selectStmt.Replace(DYNAMIC_IN_CLAUSE_PLACE_HOLDER, "")
        End If

        If Not whereClauseConditions = "" Then
            selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
        Else
            selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, "")
        End If

        Dim ds As New DataSet
        Try
            DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function
    Public Sub LoadByDealerProduct(familyDS As DataSet, dealerId As Guid, productCode As String)
        Dim selectStmt As String = Config("/SQL/LOAD_BY_DEALER_PRODUCT")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
                                                         {
                                                             New DBHelper.DBHelperParameter("dealer_id", dealerId.ToByteArray),
                                                             New DBHelper.DBHelperParameter("productCode", productCode)
                                                         }
        Try
            DBHelper.Fetch(familyDS, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadList(companyIds As ArrayList, Effective_On_Date As Date, languageId As Guid) As DataSet

        Dim selectStmt As String = Config("/SQL/LOAD_LIST")

        Dim whereClauseConditions As String = ""
        Dim ds As New DataSet
        Dim languageIdParam As DBHelper.DBHelperParameter
        Dim bIsLikeClause As Boolean = False


        whereClauseConditions &= Environment.NewLine & MiscUtil.BuildListForSql("M." & ManufacturerDAL.COL_NAME_COMPANY_GROUP_ID, companyIds, True) & " AND"

        If Not whereClauseConditions = "" Then
            selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
        Else
            selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, "")
        End If

        Try
            DBHelper.Fetch(ds, selectStmt, TABLE_NAME,
                            New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("EFF_DATE", Effective_On_Date)})

            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function LoadList(dealerId As Guid, benefitProductCode As String, vendorBillablePartNum As String, ben_product_code_id As Guid) As DataSet

        Dim selectStmt As String = Config("/SQL/LOAD_BY_UNIQUE_KEY")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
                {
                    New DBHelper.DBHelperParameter("dealer_id", dealerId.ToByteArray),
                    New DBHelper.DBHelperParameter("ben_product_code", benefitProductCode),
                    New DBHelper.DBHelperParameter("vendor_billable_part_num", vendorBillablePartNum),
                    New DBHelper.DBHelperParameter("ben_product_code_id", ben_product_code_id)
                }
        Dim ds As New DataSet
        Try
            DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)

            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Private Function IsThereALikeClause(description As String, model As String,
                               manufacturerName As String, SKU As String) As Boolean
        Dim bIsLikeClause As Boolean

        bIsLikeClause = IsLikeClause(description) OrElse IsLikeClause(model) OrElse
                            IsLikeClause(manufacturerName) OrElse IsLikeClause(SKU)
        Return bIsLikeClause
    End Function
#End Region
#Region "Overloaled Methods"
    Public Overloads Sub UpdateFamily(familyDataset As DataSet, familyDatasetClone As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing)

        Dim tr As IDbTransaction = Transaction
        If tr Is Nothing Then
            tr = DBHelper.GetNewTransaction
        End If
        Try
            'First Pass updates Deletions

            'Second Pass updates additions and changes
            Update(familyDataset.Tables(TABLE_NAME), tr, DataRowState.Added Or DataRowState.Modified Or DataRowState.Deleted)
            Update(familyDatasetClone.Tables(TABLE_NAME), tr, DataRowState.Added Or DataRowState.Modified Or DataRowState.Deleted)

            If Transaction Is Nothing Then
                'We are the creator of the transaction we shoul commit it  and close the connection
                DBHelper.Commit(tr)
            End If
        Catch ex As Exception
            If Transaction Is Nothing Then
                'We are the creator of the transaction we shoul commit it  and close the connection
                DBHelper.RollBack(tr)
            End If
            Throw ex
        End Try
    End Sub
    Public Overloads Sub UpdateFamily(familyDataset As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing)

        Dim tr As IDbTransaction = Transaction
        If tr Is Nothing Then
            tr = DBHelper.GetNewTransaction
        End If
        Try
            'First Pass updates Deletions

            'Second Pass updates additions and changes
            Update(familyDataset.Tables(TABLE_NAME), tr, DataRowState.Added Or DataRowState.Modified Or DataRowState.Deleted)

            If Transaction Is Nothing Then
                'We are the creator of the transaction we shoul commit it  and close the connection
                DBHelper.Commit(tr)
            End If
        Catch ex As Exception
            If Transaction Is Nothing Then
                'We are the creator of the transaction we shoul commit it  and close the connection
                DBHelper.RollBack(tr)
            End If
            Throw ex
        End Try
    End Sub
    Public Overloads Sub Update(ds As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing, Optional ByVal changesFilter As DataRowState = Nothing)
        If Not ds.Tables(TABLE_NAME) Is Nothing Then
            MyBase.Update(ds.Tables(TABLE_NAME), Transaction, changesFilter)
        End If
    End Sub
#End Region
#Region "Public Methods"
#End Region
End Class
