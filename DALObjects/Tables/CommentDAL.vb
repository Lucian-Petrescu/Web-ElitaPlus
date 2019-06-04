'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (1/3/2005)********************

#Region "CommentData"

Public Class CommentData

    Public Callername, Comment As String
    Public CommentTypeId, CommentId As Guid

End Class

#End Region
Public Class CommentDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_COMMENT"
    Public Const TABLE_NAME_WS As String = "CLAIM_COMMENT"
    Public Const TABLE_KEY_NAME As String = "comment_id"
    Public Const EXT_TABLE_NAME As String = "ELP_CLAIM_STATUS"

    Public Const COL_NAME_COMMENT_ID As String = "comment_id"
    Public Const COL_NAME_EXT_STATUS_ID As String = "claim_status_id"
    Public Const COL_NAME_CERT_ID As String = "cert_id"
    Public Const COL_NAME_CLAIM_ID As String = "claim_id"
    Public Const COL_NAME_ADDED_BY As String = "added_by"
    Public Const COL_NAME_CALLER_NAME As String = "caller_name"
    Public Const COL_NAME_COMMENT_TYPE_ID As String = "comment_type_id"
    Public Const COL_NAME_COMMENTS As String = "comments"
    Public Const COL_NAME_FORGOT_REQUEST_ID As String = "gdpr_forgot_request_id"

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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("comment_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, Me.TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub


    Public Function LoadList() As DataSet
        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST")
        Return DBHelper.Fetch(selectStmt, Me.TABLE_NAME)
    End Function

    Public Function LoadList(ByVal certId As Guid) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("cert_id", certId.ToByteArray)}
        Try
            Dim ds = New DataSet
            Return (DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters))
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function LoadExtendedList(ByVal claimId As Guid) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/EXT_LOAD_LIST")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("claim_id", claimId.ToByteArray)}
        Try
            Dim ds = New DataSet
            Return (DBHelper.Fetch(ds, selectStmt, Me.EXT_TABLE_NAME, parameters))
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function LoadListForClaim(ByVal claimId As Guid, Optional ByVal familyDataset As DataSet = Nothing) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/LOAD_COMMENTS_FOR_CLAIM")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("cert_id", claimId)}
        Try
            Dim ds = familyDataset
            If ds Is Nothing Then
                ds = New DataSet
            End If
            Return (DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters))
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

#End Region

#Region "Overloaded Methods"
    Public Overloads Sub Update(ByVal ds As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing, Optional ByVal changesFilter As DataRowState = Nothing)
        If ds Is Nothing Then
            Return
        End If
        If Not ds.Tables(Me.TABLE_NAME) Is Nothing Then
            MyBase.Update(ds.Tables(Me.TABLE_NAME), Transaction, changesFilter)
        End If
    End Sub

    Public Overloads Sub UpdateFamily(ByVal familyDataset As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing)
        Dim tr As IDbTransaction = Transaction
        If tr Is Nothing Then
            tr = DBHelper.GetNewTransaction
        End If
        Try
            Me.Update(familyDataset, tr, DataRowState.Deleted)
            Me.Update(familyDataset, tr, DataRowState.Added Or DataRowState.Modified)

            If Not familyDataset.Tables(TransactionLogHeaderDAL.TABLE_NAME) Is Nothing AndAlso familyDataset.Tables(TransactionLogHeaderDAL.TABLE_NAME).Rows.Count > 0 Then
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


