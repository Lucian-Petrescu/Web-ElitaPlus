'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (9/26/2014)********************


Public Class UserNotificationDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_USER_NOTIFICATION"
    Public Const TABLE_KEY_NAME As String = "user_notification_id"

    Public Const COL_NAME_USER_NOTIFICATION_ID As String = "user_notification_id"
    Public Const COL_NAME_USER_ID As String = "user_id"
    Public Const COL_NAME_NOTIFICATION_ID As String = "notification_id"

    Public Const PAR_NAME_USER_ID As String = "p_User_ID"
    Public Const PAR_NAME_P_RETURN As String = "p_return"
    Public Const PAR_NAME_P_EXCEPTION_MSG As String = "p_exception_msg"

    Public Const PAR_NAME_NOTIFICATION_ID As String = "p_Notification_ID"


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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("user_notification_id", id.ToByteArray)}
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

    Public Function UserHasNotifications(userId As Guid, ExternalUser As String, _
                                    ByVal AudianceTypeExternalId As Guid, ByVal AudianceTypeInternalId As Guid, ByVal AudianceTypeAllId As Guid) As Boolean
        Dim selectStmt As String = Me.Config("/SQL/USER_NOTIFICATIONS_COUNT")
        Dim whereClauseConditions As String = ""
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("user_id", userId.ToByteArray)}
        Dim count As Integer

        If ExternalUser.Equals("Y") Then
            whereClauseConditions &= " AND (N.AUDIANCE_TYPE_ID = " & "hextoraw( '" & Me.GuidToSQLString(AudianceTypeExternalId) & "') OR N.AUDIANCE_TYPE_ID = " & "hextoraw( '" & Me.GuidToSQLString(AudianceTypeAllId) & "'))"
        Else
            whereClauseConditions &= " AND (N.AUDIANCE_TYPE_ID = " & "hextoraw( '" & Me.GuidToSQLString(AudianceTypeInternalId) & "') OR N.AUDIANCE_TYPE_ID = " & "hextoraw( '" & Me.GuidToSQLString(AudianceTypeAllId) & "'))"
        End If

        If Not whereClauseConditions = "" Then
            selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
        Else
            selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, "")
        End If

        Try
            count = DBHelper.ExecuteScalar(selectStmt, parameters)
            If count > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function


    Public Function InsertUserNotifications(ByVal userId As Guid) As Boolean
        Dim selectStmt As String = Me.Config("/SQL/INSERT_USER_NOTIFICATIONS")
        Dim inputParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(Me.PAR_NAME_USER_ID, userId.ToByteArray)}

        Dim outputParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() { _
                                                        New DBHelper.DBHelperParameter(Me.PAR_NAME_P_RETURN, GetType(Integer)), _
                                                        New DBHelper.DBHelperParameter(Me.PAR_NAME_P_EXCEPTION_MSG, GetType(String), 50)}

        ' Call DBHelper Store Procedure
        DBHelper.ExecuteSp(selectStmt, inputParameters, outputParameters)

        If CType(outputParameters(0).Value, Integer) <> 0 Then
            'Throw New StoredProcedureGeneratedException("Claim Payment Generated Error: ", outputParameters(2).Value)
            Dim e As New ApplicationException("Return Value = " & outputParameters(0).Value)
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.BusinessErr, e, outputParameters(1).Value)
        End If

        Return True

    End Function


    Public Function DeleteUserNotifications(ByVal NotificationId As Guid) As Boolean
        Dim selectStmt As String = Me.Config("/SQL/DELETE_USER_NOTIFICATIONS")
        Dim inputParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(Me.PAR_NAME_NOTIFICATION_ID, NotificationId.ToByteArray)}

        Dim outputParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() { _
                                                        New DBHelper.DBHelperParameter(Me.PAR_NAME_P_RETURN, GetType(Integer)), _
                                                        New DBHelper.DBHelperParameter(Me.PAR_NAME_P_EXCEPTION_MSG, GetType(String), 50)}

        ' Call DBHelper Store Procedure
        DBHelper.ExecuteSp(selectStmt, inputParameters, outputParameters)

        If CType(outputParameters(0).Value, Integer) <> 0 Then
            'Throw New StoredProcedureGeneratedException("Claim Payment Generated Error: ", outputParameters(2).Value)
            Dim e As New ApplicationException("Return Value = " & outputParameters(0).Value)
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.BusinessErr, e, outputParameters(1).Value)
        End If

        Return True

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


