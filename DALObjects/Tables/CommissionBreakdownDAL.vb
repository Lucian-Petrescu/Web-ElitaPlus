'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (11/22/2004)********************

#Region "CommissionBreakdownData"

Public Class CommissionBreakdownData

    Public commissionPeriodId As Guid

End Class

#End Region

Public Class CommissionBreakdownDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_COMMISSION_BREAKDOWN"
    Public Const TABLE_KEY_NAME As String = "commission_breakdown_id"

    Public Const COL_NAME_COMMISSION_BREAKDOWN_ID As String = "commission_breakdown_id"
    Public Const COL_NAME_COMMISSION_PERIOD_ID As String = "commission_period_id"
    Public Const COL_NAME_ALLOWED_MARKUP_PCT As String = "allowed_markup_pct"
    Public Const COL_NAME_TOLERANCE As String = "tolerance"
    Public Const COL_NAME_DEALER_MARKUP_PCT As String = "dealer_markup_pct"
    Public Const COL_NAME_DEALER_COMM_PCT As String = "dealer_comm_pct"
    Public Const COL_NAME_BROKER_MARKUP_PCT As String = "broker_markup_pct"
    Public Const COL_NAME_BROKER_COMM_PCT As String = "broker_comm_pct"
    Public Const COL_NAME_BROKER2_MARKUP_PCT As String = "broker2_markup_pct"
    Public Const COL_NAME_BROKER2_COMM_PCT As String = "broker2_comm_pct"
    Public Const COL_NAME_BROKER3_MARKUP_PCT As String = "broker3_markup_pct"
    Public Const COL_NAME_BROKER3_COMM_PCT As String = "broker3_comm_pct"
    Public Const COL_NAME_BROKER4_MARKUP_PCT As String = "broker4_markup_pct"
    Public Const COL_NAME_BROKER4_COMM_PCT As String = "broker4_comm_pct"

    Public Const COMMISSION_PERIOD_ID = 0

    Public Const TOTAL_PARAM = 0 '1
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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("commission_breakdown_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    'Public Function LoadList() As DataSet
    '    Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST")
    '    Return DBHelper.Fetch(selectStmt, Me.TABLE_NAME)
    'End Function

    Public Function LoadList(oCommissionBreakdownData As CommissionBreakdownData) As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_LIST")
        Dim parameters(TOTAL_PARAM) As DBHelper.DBHelperParameter

        With oCommissionBreakdownData

            parameters(COMMISSION_PERIOD_ID) = New DBHelper.DBHelperParameter(COL_NAME_COMMISSION_PERIOD_ID, _
                                                            .commissionPeriodId.ToByteArray)
        End With

        Try
            Dim ds As New DataSet
            DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

#End Region

#Region "Overloaded Methods"
    Public Overloads Sub Update(ds As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing, Optional ByVal changesFilter As DataRowState = Nothing)
        If ds.Tables(TABLE_NAME) IsNot Nothing Then
            MyBase.Update(ds.Tables(TABLE_NAME), Transaction, changesFilter)
        End If
    End Sub
#End Region


End Class



