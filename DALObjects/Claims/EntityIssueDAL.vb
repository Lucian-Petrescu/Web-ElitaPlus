'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (7/26/2012)********************


Public Class EntityIssueDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_ENTITY_ISSUE"
    Public Const TABLE_KEY_NAME As String = "entity_issue_id"

    Public Const COL_NAME_ENTITY_ISSUE_ID As String = "entity_issue_id"
    Public Const COL_NAME_ENTITY As String = "entity"
    Public Const COL_NAME_ENTITY_ID As String = "entity_id"
    Public Const COL_NAME_ISSUE_ID As String = "issue_id"
    Public Const COL_NAME_WORKQUEUE_ITEM_CREATED_ID As String = "workqueue_item_created_id"
    Public Const COL_NAME_ENTITY_ISSUE_DATA As String = "entity_issue_data"

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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("entity_issue_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadList(familyDS As DataSet, id As Guid)
        Dim selectStmt As String = Config("/SQL/LOAD_LIST")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("claim_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function LoadClaimIssuesByIssueType(issueTypeId As Guid, userId As Guid) As DataSet
        Dim ds as New DataSet
        Dim selectStmt As String = Config("/SQL/GetClaimIssueListByIssueType")
        

        Dim inParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter(){New DBHelper.DBHelperParameter("pi_user_id", userId.ToByteArray()),
                                                                                            New DBHelper.DBHelperParameter("pi_issue_Type_Id", issueTypeId.ToByteArray())
                                                                                           }
        
        Dim outParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("po_resultcursor", GetType(DataSet))}
        
        Try
            DBHelper.FetchSp(selectStmt, inParameters, outParameters, ds, "Issues", true)            
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

    
#End Region


End Class


