'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (8/21/2012)********************


Public Class ScheduleDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_SCHEDULE"
    Public Const TABLE_KEY_NAME As String = "schedule_id"

    Public Const COL_NAME_SCHEDULE_ID As String = "schedule_id"
    Public Const COL_NAME_CODE As String = "code"
    Public Const COL_NAME_DESCRIPTION As String = "description"
    Public Const PAR_NAME_LANGUAGE_ID As String = "language_id"

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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("schedule_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadList() As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_LIST")
        Return DBHelper.Fetch(selectStmt, TABLE_NAME)
    End Function

    Public Sub LoadList(familyDS As DataSet, id As Guid)
        Dim selectStmt As String = Config("/SQL/LOAD_LIST_SCHEDULE")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(COL_NAME_SCHEDULE_ID, id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function GetSchedulesList(scheduleCode As String, scheduleDescription As String) As DataSet

        Dim selectStmt As String = Config("/SQL/LOAD_LIST")
        Dim whereClauseConditions As String = ""
        Dim rowNumParam As DBHelper.DBHelperParameter

        If FormatSearchMask(scheduleCode) Then
            whereClauseConditions &= " AND " & Environment.NewLine & "UPPER(" & COL_NAME_CODE & ") " & scheduleCode
        End If

        If FormatSearchMask(scheduleDescription) Then
            whereClauseConditions &= " AND " & Environment.NewLine & "UPPER(" & COL_NAME_DESCRIPTION & ") " & scheduleDescription
        End If

        selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)

        Try
            Dim ds As New DataSet
            'Dim LangIdPar As New DBHelper.DBHelperParameter(Me.PAR_NAME_LANGUAGE_ID, languageId.ToByteArray)
            Dim rowNum As New DBHelper.DBHelperParameter(PAR_NAME_ROW_NUMBER, MAX_NUMBER_OF_ROWS)

            'DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, _
            ' New DBHelper.DBHelperParameter() {LangIdPar, rowNum})
            DBHelper.Fetch(ds, selectStmt, TABLE_NAME, New DBHelper.DBHelperParameter() {rowNum})
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try


    End Function

    Public Function GetScheduleDetailView(ScheduleId As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/GET_SERVICE_CENTER_SCHEDULE_DETAIL")

        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() { _
                   New DBHelper.DBHelperParameter(COL_NAME_SCHEDULE_ID, ScheduleId.ToByteArray)}
        Try
            Dim ds As New DataSet
            DBHelper.Fetch(ds, selectStmt, "ELP_SCHEDULE_DETAIL", parameters)
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

    'This method was added manually to accommodate BO families Save
    Public Overloads Sub UpdateFamily(familyDataset As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing)
        Dim schDetail As New ScheduleDetailDAL

        Dim tr As IDbTransaction = Transaction
        If tr Is Nothing Then
            tr = DBHelper.GetNewTransaction
        End If
        Try
            'First Pass updates Deletions
            schDetail.Update(familyDataset, tr, DataRowState.Deleted)
            MyBase.Update(familyDataset.Tables(TABLE_NAME), tr, DataRowState.Deleted)

            'Second Pass updates additions and changes
            Update(familyDataset.Tables(TABLE_NAME), tr, DataRowState.Added Or DataRowState.Modified)
            schDetail.Update(familyDataset, tr, DataRowState.Added Or DataRowState.Modified)

            If Transaction Is Nothing Then
                'We are the creator of the transaction we shoul commit it  and close the connection
                DBHelper.Commit(tr)
            End If
        Catch ex As Exception
            If Transaction Is Nothing Then
                'We are the creator of the transaction we shoul commit it  and close the connection
                DBHelper.RollBack(tr)
            End If
            Throw ex
        End Try
    End Sub

#End Region

#Region "CRUD"
    Public Function GetScheduleDetailCount(scheduleId As Guid) As Integer
        Dim selectStmt As String = Config("/SQL/GetScheduleDetailCount")

        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() { _
                   New DBHelper.DBHelperParameter(COL_NAME_SCHEDULE_ID, scheduleId.ToByteArray)}
        Try
            Dim ds As New DataSet
            DBHelper.Fetch(ds, selectStmt, "elp_schedule_detail", parameters)
            Return ds.Tables(0).Rows.Count
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function GetScheduleDetailInfo(scheduleId As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/GetScheduleDetailInfo")

        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() { _
                   New DBHelper.DBHelperParameter(COL_NAME_SCHEDULE_ID, scheduleId.ToByteArray)}
        Try
            Dim ds As New DataSet
            DBHelper.Fetch(ds, selectStmt, "elp_schedule_detail", parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function GetScheduleInfo(scheduleId As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/GetScheduleInfo")

        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() { _
                   New DBHelper.DBHelperParameter(COL_NAME_SCHEDULE_ID, scheduleId.ToByteArray)}
        Try
            Dim ds As New DataSet
            DBHelper.Fetch(ds, selectStmt, COL_NAME_SCHEDULE_ID, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function
#End Region


    Public Function IsScheduleCodeUnique(Code As String, scheduleId As Guid) As Boolean
        Dim selectStmt As String = Config("/SQL/GET_DUPLICATE_SCHEDULE_COUNT")
        Dim parameters() As DBHelper.DBHelperParameter = _
            New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(COL_NAME_CODE, Code.ToUpper), _
                                              New DBHelper.DBHelperParameter(COL_NAME_SCHEDULE_ID, scheduleId.ToByteArray)}

        Dim count As Integer

        Try
            count = DBHelper.ExecuteScalar(selectStmt, parameters)
            If count > 0 Then
                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

End Class