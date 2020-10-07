'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (3/21/2018)********************


Public Class DataProtectionHistoryDAL
    Inherits DALBase


#Region "Constants"
	Public Const TABLE_NAME As String = "ELP_DATA_PROTECTION_HISTORY"
    Public Const TABLE_KEY_NAME As String = "data_protection_history_id"
    Public Const COL_NAME_DATA_PROTECTION_HISTORY_ID As String = "data_protection_history_id"
	Public Const COL_NAME_ENTITY_TYPE As String = "entity_type"
	Public Const COL_NAME_ENTITY_ID As String = "entity_id"
	Public Const COL_NAME_REQUEST_ID As String = "request_id"
	Public Const COL_NAME_COMMENT_ID As String = "comment_id"
	Public Const COL_NAME_STATUS As String = "status"
	Public Const COL_NAME_START_DATE As String = "start_date"
    Public Const COL_NAME_END_DATE As String = "end_date"
    Public Const COL_NAME_CREATED_BY As String = "created_by"
    Public Const COL_NAME_MODIFIED_BY As String = "modified_by"
    Public Const COL_NAME_COMMENTS As String = "comments"

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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("data_protection_history_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadList(certId As Guid, languageId As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_LIST")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() { New DBHelper.DBHelperParameter("language_id", languageid.ToByteArray),New DBHelper.DBHelperParameter(COL_NAME_ENTITY_ID, certId.ToByteArray)}
        Try
            Dim ds = New DataSet
            Return (DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters))
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

    Public Overloads Sub UpdateFamily(familyDataset As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing)
        Dim oCommentDAL As New CommentDAL
        Dim oCertificateDAL As New CertificateDAL

        Dim tr As IDbTransaction = Transaction
        If tr Is Nothing Then
            tr = DBHelper.GetNewTransaction
        End If
        Try
            'First Pass updates Deletions            
            oCommentDAL.Update(familyDataset, tr, DataRowState.Deleted)
            Update(familyDataset.Tables(TABLE_NAME), tr, DataRowState.Deleted)
            'Second Pass updates additions and changes
            oCommentDAL.Update(familyDataset, tr, DataRowState.Added)
            oCertificateDAL.Update(familyDataset, tr, DataRowState.Modified)
            'Generic family update
            Update(familyDataset.Tables(TABLE_NAME), tr, DataRowState.Added)
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

    Public Function GetRequestIdUsedInfo(requestId As String, Optional ByVal restrictionCode As String = Nothing, Optional ByVal certId As Guid = Nothing) As Boolean
        Dim selectStmt As String = Config("/SQL/REQUEST_ID_INFO")
        Dim requestIdAlreadyUsed As Boolean = False
        Try
            Dim inputParameters(2) As DBHelper.DBHelperParameter
            Dim outputParameter(0) As DBHelper.DBHelperParameter
            inputParameters(0) = New DBHelper.DBHelperParameter("pi_request_id", requestId, GetType(String))
            inputParameters(1) = New DBHelper.DBHelperParameter("pi_cert_id", certId.ToByteArray())
            inputParameters(2) = New DBHelper.DBHelperParameter("pi_restriction_code", restrictionCode)
            outputParameter(0) = New DBHelper.DBHelperParameter("po_status_return", GetType(String), 32)
            ' Call DBHelper Store Procedure
            DBHelper.ExecuteSp(selectStmt, inputParameters, outputParameter)
            If (Not outputParameter(0).Value Is Nothing) Then
                If (outputParameter(0).Value = "Y") Then
                    requestIdAlreadyUsed = True
                End If
            End If
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
        Return requestIdAlreadyUsed
    End Function
#End Region

End Class


