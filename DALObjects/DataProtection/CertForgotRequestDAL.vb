'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (7/10/2018)********************


Public Class CertForgotRequestDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_GDPR_FORGOT_REQUEST"
    Public Const TABLE_KEY_NAME As String = "gdpr_forgot_request_id"

    Public Const COL_NAME_CERT_FORGOT_REQUEST_ID As String = "gdpr_forgot_request_id"
    Public Const COL_NAME_CERT_ID As String = "cert_id"
	Public Const COL_NAME_DEALER_ID As String = "dealer_id"
	Public Const COL_NAME_CERT_NUMBER As String = "cert_number"
	Public Const COL_NAME_IS_FORGOTTEN As String = "is_forgotten"
	Public Const COL_NAME_DELETED_DATE As String = "deleted_date"
    Public Const COL_NAME_REQUEST_ID As String = "request_id"


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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("gdpr_forgot_request_id", id.ToByteArray)}
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
        Dim oCommentDAL As New CommentDAL
        Dim oCertificateDAL As New CertificateDAL

        Dim tr As IDbTransaction = Transaction
        If tr Is Nothing Then
            tr = DBHelper.GetNewTransaction
        End If
        Try
            'Generic family update
            Me.Update(familyDataset.Tables(Me.TABLE_NAME), tr, DataRowState.Added)

            'First Pass updates Deletions            
            oCommentDAL.Update(familyDataset, tr, DataRowState.Deleted)
            Me.Update(familyDataset.Tables(Me.TABLE_NAME), tr, DataRowState.Deleted)
            'Second Pass updates additions and changes
            oCommentDAL.Update(familyDataset, tr, DataRowState.Added)
            oCertificateDAL.Update(familyDataset,tr,DataRowState.Modified)

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

