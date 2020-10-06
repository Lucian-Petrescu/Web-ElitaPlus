'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (9/26/2014)********************


Public Class NotificationDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_NOTIFICATION"
    Public Const TABLE_KEY_NAME As String = "notification_id"

    Public Const COL_NAME_NOTIFICATION_ID As String = "notification_id"
    Public Const COL_NAME_NOTIFICATION_TYPE_ID As String = "notification_type_id"
    Public Const COL_NAME_NOTIFICATION_TYPE As String = "notification_type"
    Public Const COL_NAME_NOTIFICATION_BEGIN_DATE As String = "notification_begin_date"
    Public Const COL_NAME_NOTIFICATION_END_DATE As String = "notification_end_date"
    Public Const COL_NAME_AUDIANCE_TYPE_ID As String = "audiance_type_id"
    Public Const COL_NAME_NOTIFICATION_NAME As String = "notification_name"
    Public Const COL_NAME_NOTIFICATION_DETAILS As String = "notification_details"
    Public Const COL_NAME_OUTAGE_BEGIN_DATE As String = "outage_begin_date"
    Public Const COL_NAME_OUTAGE_END_DATE As String = "outage_end_date"
    Public Const COL_NAME_CONTACT_INFO As String = "contact_info"
    Public Const COL_NAME_SERIAL_NO As String = "serial_no"
    Public Const COL_NAME_ENABLED As String = "enabled"

    Public Const SORT_BY_NOTIFICATION_TYPE As String = "NOTIFICATION_TYPE"
    Public Const SORT_ORDER_ASC As String = "ASC"
    Public Const SORT_ORDER_DESC As String = "DESC"

    Public Const COL_NAME_USER_ID As String = "user_id"
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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("notification_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadList(languageId As Guid, _
                             NotificationNameMask As String, _
                             NotificationDetailMask As String, _
                             NotificationTypeId As Guid, _
                             AudianceTypeId As Guid, _
                             BeginDate As String, _
                             EndDate As String, _
                             BeginDateOutage As String, _
                             EndDateOutage As String, _
                             IncludeDisabled As Boolean, _
                             ExternalUser As String, _
                             AudianceTypeExternalId As Guid, AudianceTypeInternalId As Guid, AudianceTypeAllId As Guid, _
                             LimitResultset As Integer, sortOrder As String, sortBy As String,
                             userType As String) As DataSet

        Dim ds As New DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_LIST")
        Dim whereClauseConditions As String = ""
        Dim languageId1Param As DBHelper.DBHelperParameter
        Dim languageId2Param As DBHelper.DBHelperParameter
        Dim rowNumParam As DBHelper.DBHelperParameter
        Dim DateParam As Date
        Dim DateParam1 As Date

        If ((Not (NotificationNameMask Is Nothing)) AndAlso (FormatSearchMask(NotificationNameMask))) Then
            whereClauseConditions &= Environment.NewLine & "AND UPPER(note.notification_name)" & NotificationNameMask.ToUpper
        End If

        If ((Not (NotificationDetailMask Is Nothing)) AndAlso (FormatSearchMask(NotificationDetailMask))) Then
            whereClauseConditions &= Environment.NewLine & "AND UPPER(note.notification_details)" & NotificationDetailMask.ToUpper
        End If

        If NotificationTypeId <> Guid.Empty Then
            whereClauseConditions &= Environment.NewLine & " AND note.notification_type_id = " & "hextoraw( '" & GuidToSQLString(NotificationTypeId) & "')"
        End If

        If AudianceTypeId <> Guid.Empty Then
            whereClauseConditions &= Environment.NewLine & " AND note.audiance_type_id = " & "hextoraw( '" & GuidToSQLString(AudianceTypeId) & "')"
        End If

        If userType.ToUpper.Equals("NON_ADMIN") Then
            If ExternalUser.Equals("Y") Then
                whereClauseConditions &= " AND (note.AUDIANCE_TYPE_ID = " & "hextoraw( '" & GuidToSQLString(AudianceTypeExternalId) & "') OR note.AUDIANCE_TYPE_ID = " & "hextoraw( '" & GuidToSQLString(AudianceTypeAllId) & "'))"
            Else
                whereClauseConditions &= " AND (note.AUDIANCE_TYPE_ID = " & "hextoraw( '" & GuidToSQLString(AudianceTypeInternalId) & "') OR note.AUDIANCE_TYPE_ID = " & "hextoraw( '" & GuidToSQLString(AudianceTypeAllId) & "'))"
            End If
        End If

        ''Noteification Date
        'If BeginDate <> String.Empty Then
        '    DateParam = CDate(BeginDate)
        '    whereClauseConditions &= " AND note.NOTIFICATION_BEGIN_DATE >= to_date('" & DateParam.ToString("MM/dd/yyyy HH:mm:ss") & "', 'mm-dd-yyyy hh24:mi:ss')"
        'End If

        'If EndDate <> String.Empty Then
        '    DateParam = CDate(EndDate)
        '    whereClauseConditions &= Environment.NewLine & " AND note.NOTIFICATION_END_DATE <= to_date('" & DateParam.ToString("MM/dd/yyyy HH:mm:ss") & "', 'mm-dd-yyyy hh24:mi:ss')"
        'End If

        'NOTIFICATION DATE RANGE SEARCH
        If BeginDate <> String.Empty And EndDate = String.Empty Then

            DateParam = CDate(BeginDate)
            whereClauseConditions &= Environment.NewLine & " AND ((to_date('" & DateParam.ToString("MM-dd-yyyy HH:mm:ss") & "', 'mm-dd-yyyy hh24:mi:ss') between note.NOTIFICATION_BEGIN_DATE" _
                & Environment.NewLine & " and note.NOTIFICATION_END_DATE )" & Environment.NewLine & _
                "or (note.NOTIFICATION_BEGIN_DATE >=to_date('" & DateParam.ToString("MM-dd-yyyy HH:mm:ss") & "', 'mm-dd-yyyy hh24:mi:ss')))"

        ElseIf EndDate <> String.Empty And BeginDate = String.Empty Then

            DateParam = CDate(EndDate)
            whereClauseConditions &= Environment.NewLine & " AND ((to_date('" & DateParam.ToString("MM-dd-yyyy HH:mm:ss") & "', 'mm-dd-yyyy hh24:mi:ss') between note.NOTIFICATION_BEGIN_DATE" _
                & Environment.NewLine & " and note.NOTIFICATION_END_DATE )" & Environment.NewLine & _
                "or (note.NOTIFICATION_END_DATE <= to_date('" & DateParam.ToString("MM-dd-yyyy HH:mm:ss") & "', 'mm-dd-yyyy hh24:mi:ss')))"

        ElseIf EndDate <> String.Empty And BeginDate <> String.Empty Then
            DateParam1 = CDate(BeginDate)
            DateParam = CDate(EndDate)
            whereClauseConditions &= Environment.NewLine & " AND ((to_date('" & DateParam1.ToString("MM-dd-yyyy HH:mm:ss") & "','mm-dd-yyyy hh24:mi:ss')" _
                & " between note.NOTIFICATION_BEGIN_DATE AND note.NOTIFICATION_END_DATE)" & Environment.NewLine & _
                "OR (to_date('" & DateParam.ToString("MM-dd-yyyy HH:mm:ss") & "','mm-dd-yyyy hh24:mi:ss')" _
                & "BETWEEN note.NOTIFICATION_BEGIN_DATE AND note.NOTIFICATION_END_DATE)" _
                & Environment.NewLine & " or ((note.NOTIFICATION_BEGIN_DATE between to_date('" & DateParam1.ToString("MM-dd-yyyy HH:mm:ss") & "','mm-dd-yyyy hh24:mi:ss') " _
                & Environment.NewLine & "and to_date('" & DateParam.ToString("MM-dd-yyyy HH:mm:ss") & "','mm-dd-yyyy hh24:mi:ss'))" _
                & Environment.NewLine & " or (note.NOTIFICATION_END_DATE between to_date('" & DateParam1.ToString("MM-dd-yyyy HH:mm:ss") & "','mm-dd-yyyy hh24:mi:ss') " _
                & Environment.NewLine & " and to_date('" & DateParam.ToString("MM-dd-yyyy HH:mm:ss") & "','mm-dd-yyyy hh24:mi:ss')))) "
            
        End If

        'OUTAGE DATE RANGE SEARCH
        If BeginDateOutage <> String.Empty And EndDateOutage = String.Empty Then

            DateParam = CDate(BeginDateOutage)
            whereClauseConditions &= Environment.NewLine & " AND ((to_date('" & DateParam.ToString("MM-dd-yyyy HH:mm:ss") & "', 'mm-dd-yyyy hh24:mi:ss') between note.OUTAGE_BEGIN_DATE" _
                & Environment.NewLine & " and note.OUTAGE_END_DATE )" & Environment.NewLine & _
                "or (note.OUTAGE_BEGIN_DATE >=to_date('" & DateParam.ToString("MM-dd-yyyy HH:mm:ss") & "', 'mm-dd-yyyy hh24:mi:ss')))"

        ElseIf EndDateOutage <> String.Empty And BeginDateOutage = String.Empty Then

            DateParam = CDate(EndDateOutage)
            whereClauseConditions &= Environment.NewLine & " AND ((to_date('" & DateParam.ToString("MM-dd-yyyy HH:mm:ss") & "', 'mm-dd-yyyy hh24:mi:ss') between note.OUTAGE_BEGIN_DATE" _
                & Environment.NewLine & " and note.OUTAGE_END_DATE )" & Environment.NewLine & _
                "or (note.OUTAGE_END_DATE <= to_date('" & DateParam.ToString("MM-dd-yyyy HH:mm:ss") & "', 'mm-dd-yyyy hh24:mi:ss')))"

        ElseIf EndDateOutage <> String.Empty And BeginDateOutage <> String.Empty Then

            DateParam = CDate(EndDateOutage)
            DateParam1 = CDate(BeginDateOutage)
            whereClauseConditions &= Environment.NewLine & " AND ((to_date('" & DateParam1.ToString("MM-dd-yyyy HH:mm:ss") & "','mm-dd-yyyy hh24:mi:ss')" _
                & " between note.OUTAGE_BEGIN_DATE AND note.OUTAGE_END_DATE)" & Environment.NewLine & _
                "OR (to_date('" & DateParam.ToString("MM-dd-yyyy HH:mm:ss") & "','mm-dd-yyyy hh24:mi:ss')" _
                & "BETWEEN note.OUTAGE_BEGIN_DATE AND note.OUTAGE_END_DATE)" _
                & Environment.NewLine & " or ((note.OUTAGE_BEGIN_DATE between to_date('" & DateParam1.ToString("MM-dd-yyyy HH:mm:ss") & "','mm-dd-yyyy hh24:mi:ss') " _
                & Environment.NewLine & "and to_date('" & DateParam.ToString("MM-dd-yyyy HH:mm:ss") & "','mm-dd-yyyy hh24:mi:ss'))" _
                & Environment.NewLine & " or (note.OUTAGE_END_DATE between to_date('" & DateParam1.ToString("MM-dd-yyyy HH:mm:ss") & "','mm-dd-yyyy hh24:mi:ss') " _
                & Environment.NewLine & " and to_date('" & DateParam.ToString("MM-dd-yyyy HH:mm:ss") & "','mm-dd-yyyy hh24:mi:ss')))) "
            
        End If

        ''Outage Date
        'If BeginDateOutage <> String.Empty Then
        '    DateParam = CDate(BeginDateOutage)
        '    whereClauseConditions &= " AND (note.OUTAGE_BEGIN_DATE >= to_date('" & DateParam.ToString("MM-dd-yyyy HH:mm:ss") & "', 'mm-dd-yyyy hh24:mi:ss') OR note.OUTAGE_END_DATE >= to_date('" & DateParam.ToString("MM-dd-yyyy HH:mm:ss") & "', 'mm-dd-yyyy hh24:mi:ss'))"
        'End If

        'If EndDateOutage <> String.Empty Then
        '    DateParam = CDate(EndDateOutage)
        '    whereClauseConditions &= Environment.NewLine & " AND (note.OUTAGE_END_DATE <= to_date('" & DateParam.ToString("MM-dd-yyyy HH:mm:ss") & "', 'mm-dd-yyyy hh24:mi:ss') OR note.OUTAGE_END_DATE >= to_date('" & DateParam.ToString("MM-dd-yyyy HH:mm:ss") & "','mm-dd-yyyy hh24:mi:ss'))"
        'End If

        If Not IncludeDisabled Then
            whereClauseConditions &= Environment.NewLine & " AND UPPER(note.ENABLED) = 'Y'"
        End If


        If Not whereClauseConditions = "" Then
            selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
        Else
            selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, "")
        End If


        If Not IsNothing(sortBy) Then
            Dim sortByColumnName As String
            Select Case sortBy
                Case "NT"
                    sortByColumnName = "NOTIFICATION_TYPE"
                Case "NDR"
                    sortByColumnName = "NOTIFICATION_BEGIN_DATE"
                Case "AT"
                    sortByColumnName = "AUDIANCE_TYPE"
                Case "ODR"
                    sortByColumnName = "OUTAGE_BEGIN_DATE"
            End Select

            selectStmt = selectStmt.Replace(DYNAMIC_ORDER_BY_CLAUSE_PLACE_HOLDER, _
                                            Environment.NewLine & "ORDER BY " & Environment.NewLine & sortByColumnName & "  " & sortOrder)
        Else
            selectStmt = selectStmt.Replace(DYNAMIC_ORDER_BY_CLAUSE_PLACE_HOLDER, "")
        End If

        Try
            languageId1Param = New DBHelper.DBHelperParameter(COL_NAME_LANGUAGE_ID, languageId.ToByteArray())
            languageId2Param = New DBHelper.DBHelperParameter(COL_NAME_LANGUAGE_ID, languageId.ToByteArray())

            rowNumParam = New DBHelper.DBHelperParameter(PAR_NAME_ROW_NUMBER, LimitResultset)

            DBHelper.Fetch(ds, selectStmt, TABLE_NAME, _
                            New DBHelper.DBHelperParameter() {languageId1Param, languageId2Param, rowNumParam})
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Function LoadListForUser(userId As Guid, languageId As Guid, ExternalUser As String, _
                                    AudianceTypeExternalId As Guid, AudianceTypeInternalId As Guid, AudianceTypeAllId As Guid, _
                                    LimitResultset As Integer, sortOrder As String, sortBy As String) As DataSet


        Dim ds As New DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_LIST_FOR_USER")
        Dim whereClauseConditions As String = ""
        Dim languageId1Param As DBHelper.DBHelperParameter
        Dim languageId2Param As DBHelper.DBHelperParameter
        Dim userParam As DBHelper.DBHelperParameter
        Dim rowNumParam As DBHelper.DBHelperParameter
        Dim DateParam As Date

        If ExternalUser.Equals("Y") Then
            whereClauseConditions &= " AND (N.AUDIANCE_TYPE_ID = " & "hextoraw( '" & GuidToSQLString(AudianceTypeExternalId) & "') OR N.AUDIANCE_TYPE_ID = " & "hextoraw( '" & GuidToSQLString(AudianceTypeAllId) & "'))"
        Else
            whereClauseConditions &= " AND (N.AUDIANCE_TYPE_ID = " & "hextoraw( '" & GuidToSQLString(AudianceTypeInternalId) & "') OR N.AUDIANCE_TYPE_ID = " & "hextoraw( '" & GuidToSQLString(AudianceTypeAllId) & "'))"
        End If

        If Not whereClauseConditions = "" Then
            selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
        Else
            selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, "")
        End If


        Try
            languageId1Param = New DBHelper.DBHelperParameter(COL_NAME_LANGUAGE_ID, languageId.ToByteArray())
            languageId2Param = New DBHelper.DBHelperParameter(COL_NAME_LANGUAGE_ID, languageId.ToByteArray())
            userParam = New DBHelper.DBHelperParameter(COL_NAME_USER_ID, userId.ToByteArray())

            rowNumParam = New DBHelper.DBHelperParameter(PAR_NAME_ROW_NUMBER, LimitResultset)

            DBHelper.Fetch(ds, selectStmt, TABLE_NAME, _
                            New DBHelper.DBHelperParameter() {languageId1Param, languageId2Param, userParam, rowNumParam})
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
        If Not ds.Tables(TABLE_NAME) Is Nothing Then
            MyBase.Update(ds.Tables(TABLE_NAME), Transaction, changesFilter)
        End If
    End Sub
#End Region


End Class


