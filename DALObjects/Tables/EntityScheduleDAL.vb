'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (10/15/2012)********************


Public Class EntityScheduleDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_ENTITY_SCHEDULE"
    Public Const TABLE_KEY_NAME As String = "entity_schedule_id"

    Public Const COL_NAME_ENTITY_SCHEDULE_ID As String = "entity_schedule_id"
    Public Const COL_NAME_ENTITY As String = "entity"
    Public Const COL_NAME_ENTITY_ID As String = "entity_id"
    Public Const COL_NAME_SCHEDULE_ID As String = "schedule_id"
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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("entity_schedule_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, Me.TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadList(ByVal familyDS As DataSet, ByVal tableName As String, ByVal id As Guid) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(COL_NAME_ENTITY_ID, id.ToByteArray), New DBHelper.DBHelperParameter(COL_NAME_ENTITY, tableName)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, Me.TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function LoadList(ByVal familyDS As DataSet, ByVal scheduleId As Guid, Optional ByVal tableName As String = Nothing) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST_BY_SCHEDULE_ID")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
                                                         {New DBHelper.DBHelperParameter(COL_NAME_SCHEDULE_ID, scheduleId.ToByteArray), _
                                                          New DBHelper.DBHelperParameter("UtcSysDate", DateTime.UtcNow), _
                                                          New DBHelper.DBHelperParameter(COL_NAME_ENTITY, tableName), _
                                                          New DBHelper.DBHelperParameter(COL_NAME_ENTITY, tableName)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, Me.TABLE_NAME, parameters)
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


