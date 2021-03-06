﻿'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (3/14/2017)********************


Public Class AuditSecurityLogsDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_AUDIT_SECURITY_LOGS"
    Public Const TABLE_KEY_NAME As String = "audit_security_log_id"

    Public Const COL_NAME_AUDIT_SECURITY_LOG_ID As String = "audit_security_log_id"
    Public Const COL_NAME_AUDIT_DATE As String = "audit_date"
    Public Const COL_NAME_LOG_SOURCE As String = "log_source"
    Public Const COL_NAME_AUDIT_SECURITY_TYPE_CODE As String = "audit_security_type_code"
    Public Const COL_NAME_CLIENT_IP_ADDRESS As String = "client_ip_address"
    Public Const COL_NAME_IP_ADDRESS_CHAIN As String = "ip_address_chain"
    Public Const COL_NAME_X509_CERTIFICATE As String = "x509_certificate"
    Public Const COL_NAME_USER_NAME As String = "user_name"
    Public Const COL_NAME_REQUEST_URL As String = "request_url"
    Public Const COL_NAME_ACTION_NAME As String = "action_name"
    Public Const COL_NAME_LANGUAGE_ID1 As String = "LANGUAGE_ID1"
    Public Const COL_NAME_LANGUAGE_ID2 As String = "LANGUAGE_ID2"

#End Region

#Region "Constructors"
    Public Sub New()
        MyBase.New()
    End Sub

#End Region

#Region "Load Methods"

    Public Sub LoadSchema(ByVal ds As DataSet)
        Load(ds, Guid.Empty)
    End Sub

    Public Sub Load(ByVal familyDS As DataSet, ByVal id As Guid)
        Dim selectStmt As String = Me.Config("/SQL/LOAD")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("audit_security_log_id", id.ToByteArray)}
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

    Public Function GetAuditLogsList(ByVal AuditBeginDate As String,
                                     ByVal AuditEndDate As String,
                                     ByVal AuditSource As String,
                                     ByVal AuditSecurityTypeCode As String,
                                     ByVal IPAddress As String,
                                     ByVal UserName As String,
                                     ByVal languageId As Guid)

        Dim selectStmt As String = String.Empty
        selectStmt = Me.Config("/SQL/LOAD_LIST")
        Dim BeginDateParam As Date
        Dim EndDateParam As Date

        'Dim parameters() As OracleParameter

        'parameters = New OracleParameter() _
        '                            {New OracleParameter(COL_NAME_LANGUAGE_ID, languageId.ToByteArray),
        '                             New OracleParameter(COL_NAME_LANGUAGE_ID1, languageId.ToByteArray)}

        Dim inParameters As New Generic.List(Of DBHelper.DBHelperParameter)
        Dim param As DBHelper.DBHelperParameter

        param = New DBHelper.DBHelperParameter(COL_NAME_LANGUAGE_ID, languageId.ToByteArray)
        inParameters.Add(param)

        param = New DBHelper.DBHelperParameter(COL_NAME_LANGUAGE_ID1, languageId.ToByteArray)
        inParameters.Add(param)


        Dim whereClauseConditions As String = String.Empty

        BeginDateParam = CDate(AuditBeginDate)
        EndDateParam = CDate(AuditEndDate)
        whereClauseConditions = COL_NAME_AUDIT_DATE & " >= to_date('" & BeginDateParam.ToString("MM-dd-yyyy HH:mm:ss") & "', 'mm-dd-yyyy hh24:mi:ss') AND " &
        COL_NAME_AUDIT_DATE & "<= to_date('" & EndDateParam.ToString("MM-dd-yyyy HH:mm:ss") & "', 'mm-dd-yyyy hh24:mi:ss')"

        If AuditSource <> String.Empty Then
            whereClauseConditions &= Environment.NewLine & "AND UPPER(" & Me.COL_NAME_LOG_SOURCE & ") = '" & AuditSource.ToUpper & "'"
        End If

        If AuditSecurityTypeCode <> String.Empty AndAlso (Me.FormatSearchMask(AuditSecurityTypeCode)) Then
            whereClauseConditions &= Environment.NewLine & "AND UPPER(Elita.Getdescriptionfromitemextcode(" & Me.COL_NAME_AUDIT_SECURITY_TYPE_CODE & ", :language_id2)) " & AuditSecurityTypeCode.ToUpper
            param = New DBHelper.DBHelperParameter(COL_NAME_LANGUAGE_ID2, languageId.ToByteArray)
            inParameters.Add(param)

        End If

        If IPAddress <> String.Empty Then
            whereClauseConditions &= Environment.NewLine & "AND UPPER(" & Me.COL_NAME_CLIENT_IP_ADDRESS & ") = '" & IPAddress.ToUpper & "'"
        End If

        If UserName <> String.Empty Then
            whereClauseConditions &= Environment.NewLine & " AND UPPER(" & Me.COL_NAME_USER_NAME & ") ='" & UserName.ToUpper & "'"
        End If

        If Not whereClauseConditions.Equals(String.Empty) Then
            whereClauseConditions = whereClauseConditions
            selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
        Else
            selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, "")
        End If




        Dim ds As New DataSet
        Try
            Return DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, inParameters.ToArray)
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


