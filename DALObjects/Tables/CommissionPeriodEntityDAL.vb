'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (9/5/2007)********************


Public Class CommissionPeriodEntityDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_COMMISSION_PERIOD_ENTITY"
    Public Const TABLE_KEY_NAME As String = "commission_period_entity_id"

    Public Const COL_NAME_COMMISSION_PERIOD_ENTITY_ID As String = "commission_period_entity_id"
    Public Const COL_NAME_COMMISSION_PERIOD_ID As String = "commission_period_id"
    Public Const COL_NAME_ENTITY_ID As String = "entity_id"
    Public Const COL_NAME_POSITION As String = "position"
    Public Const COL_NAME_ENTITY_NAME As String = "entity_name"
    Public Const COL_NAME_PAYEE_TYPE_ID As String = "payee_type_id"


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

    'Public Sub Load(ByVal familyDS As DataSet, ByVal id As Guid)
    '    Dim selectStmt As String = Me.Config("/SQL/LOAD")
    '    Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("commission_period_entity_id", id.ToByteArray)}
    '    Try
    '        DBHelper.Fetch(familyDS, selectStmt, Me.TABLE_NAME, parameters)
    '    Catch ex As Exception
    '        Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
    '    End Try
    'End Sub

    Public Sub Load(familyDS As DataSet, id As Guid, Optional ByVal blnLoadByPeriodID As Boolean = False)
        Dim selectStmt As String
        Dim parameters() As DBHelper.DBHelperParameter
        If blnLoadByPeriodID Then
            selectStmt = Config("/SQL/LOAD_BY_PERIOD_ID")
            parameters = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(COL_NAME_COMMISSION_PERIOD_ID, id.ToByteArray)}
        Else
            selectStmt = Config("/SQL/LOAD")
            parameters = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(COL_NAME_COMMISSION_PERIOD_ENTITY_ID, id.ToByteArray)}
        End If
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



    Public Sub LoadList(commissionPeriodId As Guid, familyDataset As DataSet)
        Dim parameters() As OracleParameter
        Dim selectStmt As String = Config("/SQL/LOAD_BY_PERIOD_ID")
        parameters = New OracleParameter() {New OracleParameter(COL_NAME_COMMISSION_PERIOD_ID, commissionPeriodId.ToByteArray)}
        DBHelper.Fetch(familyDataset, selectStmt, TABLE_NAME, parameters)
    End Sub

    Public Function LoadList(commissionPeriodId As Guid) As DataSet
        Dim parameters() As OracleParameter
        Dim selectStmt As String = Config("/SQL/LOAD_LIST")
        parameters = New OracleParameter() {New OracleParameter(COL_NAME_COMMISSION_PERIOD_ID, commissionPeriodId.ToByteArray)}
        DBHelper.Fetch(selectStmt, TABLE_NAME)
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


