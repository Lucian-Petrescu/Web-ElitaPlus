﻿'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (8/24/2012)********************


Public Class ScheduleDetailDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_SCHEDULE_DETAIL"
    Public Const TABLE_KEY_NAME As String = "schedule_detail_id"

    Public Const COL_NAME_SCHEDULE_DETAIL_ID As String = "schedule_detail_id"
    Public Const COL_NAME_SCHEDULE_ID As String = "schedule_id"
    Public Const COL_NAME_DAY_OF_WEEK_ID As String = "day_of_week_id"
    Public Const COL_NAME_FROM_TIME As String = "from_time"
    Public Const COL_NAME_TO_TIME As String = "to_time"
    Public Const PAR_NAME_LANGUAGE_ID As String = "language_id"

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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("schedule_detail_id", id.ToByteArray)}
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

    Public Sub LoadList(ByVal familyDS As DataSet, ByVal id As Guid, ByVal VSid As Guid)
        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(Me.COL_NAME_SCHEDULE_ID, id.ToByteArray), _
                                                                                           New DBHelper.DBHelperParameter(Me.COL_NAME_SCHEDULE_ID, VSid.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, Me.TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadScheduleDetail(ByVal familyDS As DataSet, ByVal ScheduleId As Guid) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/LOAD_SCHEDULE_DETAIL")
        Dim parameters() As OracleParameter

        parameters = New OracleParameter() {New OracleParameter(COL_NAME_SCHEDULE_ID, ScheduleId.ToByteArray)}

        Try
            Return (DBHelper.Fetch(familyDS, selectStmt, Me.TABLE_NAME, parameters))
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function LoadScheduleDetail(ByVal ScheduleId As Guid) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/LOAD_SCHEDULE_DETAIL")
        Dim parameters() As OracleParameter

        parameters = New OracleParameter() {New OracleParameter(COL_NAME_SCHEDULE_ID, ScheduleId.ToByteArray)}

        Try
            Return (DBHelper.Fetch(selectStmt, Me.TABLE_NAME, Me.TABLE_NAME, parameters))
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