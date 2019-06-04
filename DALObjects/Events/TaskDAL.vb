'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (12/20/2012)********************


Public Class TaskDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_TASK"
    Public Const TABLE_KEY_NAME As String = "task_id"

    Public Const COL_NAME_TASK_ID As String = "task_id"
    Public Const COL_NAME_CODE As String = "code"
    Public Const COL_NAME_DESCRIPTION As String = "description"
    Public Const COL_NAME_RETRY_COUNT As String = "retry_count"
    Public Const COL_NAME_RETRY_DELAY_SECONDS As String = "retry_delay_seconds"
    Public Const COL_NAME_TIMEOUT_SECONDS As String = "timeout_seconds"
    Public Const COL_NAME_TASK_PARAMETERS As String = "task_parameters"

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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("task_id", id.ToByteArray)}
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

    Public Function LoadList(ByVal strCode As String, ByVal strDesc As String) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST")
        Dim whereClauseConditions As String = "", strTemp As String

        If Me.FormatSearchMask(strCode) Then
            whereClauseConditions &= Environment.NewLine & " and UPPER(t.CODE) " & strCode.ToUpper.Trim
        End If

        If Me.FormatSearchMask(strDesc) Then
            whereClauseConditions &= Environment.NewLine & " and UPPER(t.description) " & strDesc.ToUpper.Trim
        End If

        selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)

        Try
            Return DBHelper.Fetch(selectStmt, Me.TABLE_NAME)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

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
#End Region


End Class


