'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (5/29/2012)********************


Public Class DealerRuleListDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_DEALER_RULE_LIST"
    Public Const TABLE_KEY_NAME As String = "dealer_rule_list_id"

    Public Const COL_NAME_DEALER_RULE_LIST_ID As String = "dealer_rule_list_id"
    Public Const COL_NAME_RULE_LIST_ID As String = "rule_list_id"
    Public Const COL_NAME_DEALER_ID As String = "dealer_id"
    Public Const COL_NAME_EFFECTIVE As String = "effective"
    Public Const COL_NAME_EXPIRATION As String = "expiration"

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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("dealer_rule_list_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Sub LoadList(familyDS As DataSet, RuleListId As Guid, CurrentDate As DateTime)
        Dim selectStmt As String = Config("/SQL/LOAD_LIST")
        Dim parameters() As OracleParameter = New OracleParameter() _
                                              {New OracleParameter(COL_NAME_RULE_LIST_ID, OracleDbType.Raw, 16)}
        'New OracleParameter("current_date", OracleDbType.Date)}
        Try
            parameters(0).Value = RuleListId.ToByteArray
            'parameters(1).Value = CurrentDate

            DBHelper.Fetch(familyDS, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function getAvailableDealers(compIds As ArrayList) As DataView
        Dim selectStmt As String = Config("/SQL/LOAD_DEALER_LIST")        
        Try
            selectStmt &= MiscUtil.BuildListForSql(" WHERE EC." & CertificateDAL.COL_NAME_COMPANY_ID, compIds, True)
            Return DBHelper.Fetch(selectStmt, TABLE_NAME).Tables(0).DefaultView

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function getAvailableDealers() As DataView
        Dim selectStmt As String = Config("/SQL/LOAD_DEALER_LIST")
        Try            
            Return DBHelper.Fetch(selectStmt, TABLE_NAME).Tables(0).DefaultView
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
#End Region




End Class


