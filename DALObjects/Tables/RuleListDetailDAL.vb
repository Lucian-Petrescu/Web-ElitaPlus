'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (5/25/2012)********************


Public Class RuleListDetailDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_RULE_LIST_DETAIL"
    Public Const TABLE_KEY_NAME As String = "rule_list_detail_id"

    Public Const COL_NAME_RULE_LIST_DETAIL_ID As String = "rule_list_detail_id"
    Public Const COL_NAME_RULE_LIST_ID As String = "rule_list_id"
    Public Const COL_NAME_RULE_ID As String = "rule_id"
    Public Const COL_NAME_EFFECTIVE As String = "effective"
    Public Const COL_NAME_EXPIRATION As String = "expiration"

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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("rule_list_detail_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, Me.TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Sub LoadList(ByVal familyDS As DataSet, ByVal RuleListId As Guid, ByVal CurrentDate As DateTime)
        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST")
        Dim parameters() As OracleParameter = New OracleParameter() _
                                              {New OracleParameter(COL_NAME_RULE_LIST_ID, OracleDbType.Raw, 16)} ', _
        'New OracleParameter("current_date", OracleDbType.Date)}
        Try
            parameters(0).Value = RuleListId.ToByteArray
            'parameters(1).Value = CurrentDate

            DBHelper.Fetch(familyDS, selectStmt, Me.TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub


    Public Function GetAvailableRules(ByVal ActiveOn As Date) As DataView
        Dim selectStmt As String = Me.Config("/SQL/LOAD_RULE_LIST")
        Try
            'DEF-2368 - Added new parameter Date
            Dim ds As New DataSet
            Dim parameters() As OracleParameter = New OracleParameter() _
                                              {New OracleParameter("ACTIVE_ON", OracleDbType.Date)}

            'Return DBHelper.Fetch(selectStmt, Me.TABLE_NAME).Tables(0).DefaultView

            parameters(0).Value = ActiveOn

            DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)
            Return ds.Tables(Me.TABLE_NAME).DefaultView

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
#End Region



End Class


