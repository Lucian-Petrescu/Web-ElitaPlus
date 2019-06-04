'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (9/16/2008)********************

'Namespace Table
Public Class ProductGroupDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_PRODUCT_GROUP_NAME As String = "ELP_PRODUCT_GROUP"
    Public Const TABLE_PRODUCT_GROUP_DETAIL_NAME As String = "ELP_PRODUCT_GROUP_DETAIL"
    Public Const TABLE_KEY_NAME As String = "product_group_id"

    Public Const COL_NAME_PRODUCT_GROUP_ID As String = "product_group_id"
    Public Const COL_NAME_PRODUCT_GROUP_NAME As String = "product_group_name"
    Public Const COL_NAME_DEALER_ID As String = "dealer_id"
    Public Const COL_NAME_DEALER As String = "dealer"
    Public Const COL_NAME_DEALER_NAME As String = "dealer_name"
    Public Const COL_NAME_COMPANY_ID = "company_id"



    Public Const DSNAME As String = "LIST"
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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("product_group_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, Me.TABLE_PRODUCT_GROUP_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadList(ByVal compIds As ArrayList, ByVal dealerId As Guid, ByVal groupName As String, ByVal productCodeId As String, ByVal riskTypeId As String) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST")
        Dim inClausecondition As String = ""
        Dim whereClauseConditions As String = ""
        Dim bIsLikeClause As Boolean = False


        If (Not productCodeId.Equals(String.Empty) AndAlso Not New Guid(productCodeId).Equals(Guid.Empty)) And (Not riskTypeId.Equals(String.Empty) AndAlso Not New Guid(riskTypeId).Equals(Guid.Empty)) Then
            inClausecondition &= Environment.NewLine & " inner join elp_product_code pc on pg.dealer_id = pc.dealer_id" & _
                                                    " inner join elp_product_group_detail pgd" & _
                                                    " on pg.product_group_id = pgd.product_group_id and pgd.product_code_id = pc.product_code_id" & _
                                                    " inner join elp_item i on pgd.product_code_id = i.product_code_id" & _
                                                    " inner join elp_risk_type rt on i.risk_type_id = rt.risk_type_id and i.item_number = 1"
        Else
            If (Not productCodeId.Equals(String.Empty) AndAlso Not New Guid(productCodeId).Equals(Guid.Empty)) Then

                inClausecondition &= Environment.NewLine & " inner join elp_product_code pc on pg.dealer_id = pc.dealer_id" & _
                                                    " inner join elp_product_group_detail pgd" & _
                                                    " on pg.product_group_id = pgd.product_group_id and pgd.product_code_id = pc.product_code_id"
            End If

        End If

        If Not inClausecondition = "" Then
            selectStmt = selectStmt.Replace(Me.DYNAMIC_IN_CLAUSE_PLACE_HOLDER, inClausecondition)
        Else
            selectStmt = selectStmt.Replace(Me.DYNAMIC_IN_CLAUSE_PLACE_HOLDER, "")
        End If

        groupName = groupName.Trim()
        If (Not groupName.Equals(String.Empty) Or Not groupName = "") AndAlso (Me.FormatSearchMask(groupName)) Then
            whereClauseConditions &= Environment.NewLine & "AND " & "Upper(pg.PRODUCT_GROUP_NAME)" & groupName.ToUpper
        End If

        If Not dealerId.Equals(Guid.Empty) Then
            whereClauseConditions &= Environment.NewLine & "AND " & "edealer.DEALER_ID = " & MiscUtil.GetDbStringFromGuid(dealerId)
        End If

        If Not productCodeId.Equals(String.Empty) AndAlso Not New Guid(productCodeId).Equals(Guid.Empty) Then
            whereClauseConditions &= Environment.NewLine & "AND " & "pgd.PRODUCT_CODE_ID = " & MiscUtil.GetDbStringFromGuid(New Guid(productCodeId))
        End If

        If Not riskTypeId.Equals(String.Empty) AndAlso Not New Guid(riskTypeId).Equals(Guid.Empty) Then
            whereClauseConditions &= Environment.NewLine & "AND " & "i.RISK_TYPE_ID = " & MiscUtil.GetDbStringFromGuid(New Guid(riskTypeId))
        End If

        whereClauseConditions &= MiscUtil.BuildListForSql(" AND c." & "company_id", compIds, False)

        If Not whereClauseConditions = "" Then
            selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
        Else
            selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, "")
        End If

        Dim ds As New DataSet
        Try
            Return DBHelper.Fetch(selectStmt, Me.TABLE_PRODUCT_GROUP_NAME)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Function LoadList() As DataSet
        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST")
        Return DBHelper.Fetch(selectStmt, Me.TABLE_PRODUCT_GROUP_NAME)
    End Function

    Private Function IsThereALikeClause(ByVal descriptionMask As String, ByVal codeMask As String) As Boolean
        Dim bIsLikeClause As Boolean

        bIsLikeClause = Me.IsLikeClause(descriptionMask) OrElse Me.IsLikeClause(codeMask)
        Return bIsLikeClause
    End Function

    Public Function LoadProductCode(ByVal oDealerIds As ArrayList) As DataSet

        Dim selectStmt As String = Me.Config("/SQL/LOAD_PRODUCT_CODES")
        Dim parameters() As OracleParameter

        Dim whereClauseConditions As String = ""

        whereClauseConditions &= MiscUtil.BuildListForSql("sc." & Me.COL_NAME_DEALER_ID, oDealerIds, False)
        selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
        'parameters = New OracleParameter() _
        '                            {New OracleParameter(COL_NAME_COUNTRY_ID, oCountryIds)}
        Try
            Return (DBHelper.Fetch(selectStmt, Me.DSNAME))
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Sub LoadSelectedProductCodes(ByVal ds As DataSet, ByVal dealerID As Guid)

        Dim selectStmt As String = Me.Config("/SQL/LOAD_SELECTED_PRODUCTCODES_LIST")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(Me.COL_NAME_DEALER_ID, dealerID)}
        DBHelper.Fetch(ds, selectStmt, Me.TABLE_PRODUCT_GROUP_NAME, parameters)

    End Sub

    Public Sub LoadAvailableProductCodes(ByVal ds As DataSet, ByVal dealerID As Guid)
        Dim selectStmt As String = Me.Config("/SQL/LOAD_AVAILABLE_PRODUCTCODES_LIST")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(Me.COL_NAME_DEALER_ID, dealerID)}
        DBHelper.Fetch(ds, selectStmt, Me.TABLE_PRODUCT_GROUP_NAME, parameters)
    End Sub

#End Region

#Region "Overloaded Methods"
    Public Overloads Sub UpdateFamily(ByVal familyDataset As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing)

        Dim prodGrpDetDAL As New ProductGroupPrcDAL

        Dim tr As IDbTransaction = Transaction
        If tr Is Nothing Then
            tr = DBHelper.GetNewTransaction
        End If
        Try
            'First Pass updates Deletions
            prodGrpDetDAL.Update(familyDataset.GetChanges(DataRowState.Deleted), tr, DataRowState.Deleted)
            MyBase.Update(familyDataset.Tables(Me.TABLE_PRODUCT_GROUP_NAME).GetChanges(DataRowState.Deleted), tr, DataRowState.Deleted)

            'Second Pass updates additions and changes
            Update(familyDataset.Tables(Me.TABLE_PRODUCT_GROUP_NAME).GetChanges(DataRowState.Added Or DataRowState.Modified), tr, DataRowState.Added Or DataRowState.Modified)
            prodGrpDetDAL.Update(familyDataset.GetChanges(DataRowState.Added Or DataRowState.Modified), tr, DataRowState.Added Or DataRowState.Modified)


            If Transaction Is Nothing Then
                'We are the creator of the transaction we shoul commit it  and close the connection
                DBHelper.Commit(tr)
                familyDataset.AcceptChanges()
            End If
        Catch ex As Exception
            If Transaction Is Nothing Then
                'We are the creator of the transaction we shoul commit it  and close the connection
                DBHelper.RollBack(tr)
            End If
            Throw ex
        End Try

    End Sub

    Public Overloads Sub Update(ByVal ds As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing, Optional ByVal changesFilter As DataRowState = Nothing)
        If ds Is Nothing Then
            Return
        End If
        If Not ds.Tables(Me.TABLE_PRODUCT_GROUP_NAME) Is Nothing Then
            MyBase.Update(ds.Tables(Me.TABLE_PRODUCT_GROUP_NAME), Transaction, changesFilter)
        End If
    End Sub
#End Region


End Class

'End Namespace

