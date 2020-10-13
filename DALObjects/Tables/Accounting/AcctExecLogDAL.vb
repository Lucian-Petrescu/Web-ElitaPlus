'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (3/6/2008)********************


Public Class AcctExecLogDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_ACCT_EXEC_LOG"
    Public Const TABLE_KEY_NAME As String = "acct_exec_log_id"

    Public Const COL_NAME_ACCT_EXEC_LOG_ID As String = "acct_exec_log_id"
    Public Const COL_NAME_COMPANY_ID As String = "company_id"
    Public Const COL_NAME_ACCT_EVENT_ID As String = "acct_event_id"
    Public Const COL_NAME_LAST_RUN_DATE As String = "last_run_date"
    Public Const COL_NAME_PREVIOUS_RUN_DATE As String = "previous_run_date"
    Public Const COL_NAME_STATUS As String = "status"

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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("acct_exec_log_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadByEvent(CompanyId As Guid, AcctEventId As Guid) As Guid
        Dim selectStmt As String = Config("/SQL/GET_EVENT")
        Dim whereClauseConditions As String = ""
        Dim ret As Object

        Try

            whereClauseConditions += " AND " + COL_NAME_COMPANY_ID + " = HEXTORAW('" + GuidControl.GuidToHexString(CompanyId) + "') "
            whereClauseConditions += " AND " + COL_NAME_ACCT_EVENT_ID + " = HEXTORAW('" + GuidControl.GuidToHexString(AcctEventId) + "') "

            If Not whereClauseConditions = "" Then
                selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
            Else
                selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, "")
            End If

            ret = DBHelper.ExecuteScalar(selectStmt)

            If ret IsNot Nothing Then
                Return GuidControl.ByteArrayToGuid(ret)
            Else
                Return Guid.Empty
            End If


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

#Region "Static Methods"

   
#End Region


End Class


