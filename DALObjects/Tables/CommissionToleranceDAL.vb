'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (6/12/2007)********************
#Region "CommissionBreakdownData"

Public Class CommissionToleraneData

    Public commissionPeriodId As Guid

End Class

#End Region

Public Class CommissionToleranceDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_COMMISSION_TOLERANCE"
    Public Const TABLE_KEY_NAME As String = "commission_tolerance_id"

    Public Const COL_NAME_COMMISSION_TOLERANCE_ID As String = "commission_tolerance_id"
    Public Const COL_NAME_COMMISSION_PERIOD_ID As String = "commission_period_id"
    Public Const COL_NAME_ALLOWED_MARKUP_PCT As String = "allowed_markup_pct"
    Public Const COL_NAME_TOLERANCE As String = "tolerance"
    Public Const COL_NAME_DEALER_MARKUP_PCT As String = "dealer_markup_pct"
    Public Const COL_NAME_DEALER_COMM_PCT As String = "dealer_comm_pct"
    Public Const COL_NAME_LANGUAGE_ID As String = "language_id"
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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("commission_tolerance_id", id.ToByteArray)}
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

    Public Function LoadList(ByVal periodId As Guid) As DataSet
        Dim parameters() As OracleParameter
        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST")
        Dim ds As New DataSet
        parameters = New OracleParameter() {New OracleParameter(Me.COL_NAME_COMMISSION_PERIOD_ID, periodId.ToByteArray)}
        Return DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)
    End Function

    Public Function LoadList(ByVal periodId As Guid, ByVal AllowedMarkup As DecimalType) As DataSet
        Dim parameters() As OracleParameter
        Dim selectStmt As String = Me.Config("/SQL/LOAD_MARKUP_LIST")
        Dim ds As New DataSet
        parameters = New OracleParameter() {New OracleParameter(Me.COL_NAME_COMMISSION_PERIOD_ID, periodId.ToByteArray), _
                                            New OracleParameter(Me.COL_NAME_ALLOWED_MARKUP_PCT, AllowedMarkup.ToString)}
        Return DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)
    End Function


    Public Sub LoadList(ByVal toleranceId As Guid, ByVal familyDataset As DataSet)
        Dim parameters() As OracleParameter
        Dim selectStmt As String = Me.Config("/SQL/LOAD_ALL_PERIOD_TOLERANCES")
        parameters = New OracleParameter() {New OracleParameter(Me.COL_NAME_COMMISSION_TOLERANCE_ID, toleranceId.ToByteArray)}
        DBHelper.Fetch(familyDataset, selectStmt, Me.TABLE_NAME, parameters)
    End Sub
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


