'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (9/4/2012)********************


Public Class DealerRejectCodeDAL
    Inherits DALBase


#Region "Constants"
	Public Const TABLE_NAME As String = "ELP_DEALER_REJECT_CODE"
	Public Const TABLE_KEY_NAME As String = "dealer_reject_code_id"
	
	Public Const COL_NAME_DEALER_REJECT_CODE_ID As String = "dealer_reject_code_id"
	Public Const COL_NAME_DEALER_ID As String = "dealer_id"
	Public Const COL_NAME_MSG_CODE_ID As String = "msg_code_id"
    Public Const COL_NAME_RECORD_TYPE_ID As String = "record_type_id"
    Public Const COL_NAME_LANGUAGE_ID As String = "language_id"
    Public Const COL_NAME_REJECT_CODE As String = "REJECT_CODE"
    Public Const COL_NAME_REJECT_REASON As String = "REJECT_REASON"

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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("dealer_reject_code_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub


    Public Function LoadList(languageId As Guid,
                                recordTypeId As Guid,
                                dealerId As Guid,
                                rejectCodeMask As String,
                                rejectReasonMask As String,
                                rejectMsgTypeId As Guid) As DataSet

        Dim selectStmt As String = Config("/SQL/LOAD_LIST")
        Dim whereClauseConditions As String = ""

        ' hextoraw        

        If Not IsNothing(languageId) Then
            whereClauseConditions &= " AND dt.language_id = '" & GuidToSQLString(languageId) & "'"
        End If

        '' ''If Not Me.IsNothing(recordTypeId) Then
        '' ''    whereClauseConditions &= " AND li.list_item_id = '" & Me.GuidToSQLString(recordTypeId) & "'"
        '' ''End If


        If ((Not (rejectCodeMask Is Nothing)) AndAlso (FormatSearchMask(rejectCodeMask))) Then
            whereClauseConditions &= Environment.NewLine & " AND UPPER(mc.msg_code) " & rejectCodeMask.ToUpper
        End If

        If ((Not (rejectReasonMask Is Nothing)) AndAlso (FormatSearchMask(rejectReasonMask))) Then
            whereClauseConditions &= Environment.NewLine & " AND UPPER(dt.translation) " & rejectReasonMask.ToUpper
        End If

        If Not IsNothing(rejectMsgTypeId) Then
            whereClauseConditions &= " AND mc.msg_type = '" & GuidToSQLString(rejectMsgTypeId) & "'"
        End If

        If Not whereClauseConditions = "" Then
            selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
        Else
            selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, "")
        End If

        selectStmt = selectStmt.Replace(DYNAMIC_ORDER_BY_CLAUSE_PLACE_HOLDER, " ORDER BY mc.msg_code")
        Try
            Dim ds As New DataSet
            Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(COL_NAME_RECORD_TYPE_ID, recordTypeId.ToByteArray),
                                                                                               New DBHelper.DBHelperParameter(COL_NAME_DEALER_ID, dealerId.ToByteArray),
                                                                                               New DBHelper.DBHelperParameter(COL_NAME_RECORD_TYPE_ID, recordTypeId.ToByteArray)}

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
        If ds.Tables(TABLE_NAME) IsNot Nothing Then
            MyBase.Update(ds.Tables(TABLE_NAME), Transaction, changesFilter)
        End If
    End Sub

    Public Overloads Sub UpdateFamily(familyDataset As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing)
        Dim tr As IDbTransaction = Transaction
        If tr Is Nothing Then
            tr = DBHelper.GetNewTransaction
        End If
        Try
            Update(familyDataset, tr, DataRowState.Deleted)
            Update(familyDataset, tr, DataRowState.Added Or DataRowState.Modified)

            If familyDataset.Tables(TransactionLogHeaderDAL.TABLE_NAME) IsNot Nothing AndAlso familyDataset.Tables(TransactionLogHeaderDAL.TABLE_NAME).Rows.Count > 0 Then
                Dim oTransactionLogHeaderDAL As New TransactionLogHeaderDAL
                oTransactionLogHeaderDAL.Update(familyDataset.Tables(TransactionLogHeaderDAL.TABLE_NAME), tr, DataRowState.Added Or DataRowState.Modified)
            End If
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
#End Region


End Class


