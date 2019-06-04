Public Class OcMessageDAL
    Inherits DALBase

#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_OC_MESSAGE"
    Public Const TABLE_KEY_NAME As String = "oc_message_id"

    Public Const COL_NAME_OC_MESSAGE_ID As String = "oc_message_id"
    Public Const COL_NAME_OC_TEMPLATE_ID As String = "oc_template_id"

    Public Const COL_NAME_TEMPLATE_CODE As String = "template_code"
    Public Const COL_NAME_TEMPLATE_DESCRIPTION As String = "template_description"
    Public Const COL_NAME_SENDER_REASON As String = "sender_reason"
    Public Const COL_NAME_RECIPIENT_ADDRESS As String = "recipient_address"
    Public Const COL_NAME_LAST_ATTEMPTED_ON As String = "last_attempted_on"
    Public Const COL_NAME_LAST_ATTEMPTED_STATUS As String = "last_attempted_status"

    Public Const COL_NAME_CERT_NUMBER As String = "cert_number"
    Public Const COL_NAME_CLAIM_NUMBER As String = "claim_number"
    Public Const COL_NAME_CASE_NUMBER As String = "case_number"

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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("oc_message_id", id.ToByteArray)}
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

    Public Function LoadList(ByVal dealerId As Guid,
                             ByVal searchBy As String,
                             ByVal conditionMask As String,
                             ByVal languageid As Guid) As DataSet

        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST_BY_DEALER_AND_CONDITION")
        Dim inClausecondition As String = String.Empty
        Dim whereClauseConditions As String = String.Empty
        Dim dealerWhereClauseConditions As String = String.Empty

        If (Not String.IsNullOrEmpty(searchBy)) And (Not String.IsNullOrEmpty(conditionMask)) Then
            conditionMask = conditionMask.Trim()
            If Me.FormatSearchMask(conditionMask) Then
                whereClauseConditions &= Environment.NewLine & "and UPPER(" & searchBy & ") " & conditionMask.ToUpper
            End If
        End If

        If Not dealerId.Equals(Guid.Empty) Then
            dealerWhereClauseConditions &= Environment.NewLine & "And " & "d.DEALER_ID = " & MiscUtil.GetDbStringFromGuid(dealerId)
        End If

        If Not String.IsNullOrEmpty(dealerWhereClauseConditions) Then
            selectStmt = selectStmt.Replace("--dynamic_where_clause_dealer", dealerWhereClauseConditions)
        Else
            selectStmt = selectStmt.Replace("--dynamic_where_clause_dealer", String.Empty)
        End If

        If Not String.IsNullOrEmpty(whereClauseConditions) Then
            selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
        Else
            selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, String.Empty)
        End If

        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("language_id", languageid.ToByteArray)}

        Try
            Dim ds As DataSet = New DataSet()
            DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Sub SendAdhocMessage(ByVal dealer_id As Guid,
                            ByVal msg_for As String,
                            ByVal id As Guid,
                            ByVal template_code As String,
                            ByVal std_recipient As String,
                            ByVal cst_recipient As String,
                            ByVal std_parameter As String,
                            ByVal cst_parameter As String,
                            ByVal sender As String,
                            ByRef message_id As Guid,
                            ByRef err_no As Integer,
                            ByRef err_msg As String)

        Dim selectStmt As String = Me.Config("/SQL/SEND_ADHOC_MESSAGE")
        Dim inputParameters() As DBHelper.DBHelperParameter
        Dim outputParameter(2) As DBHelper.DBHelperParameter

        inputParameters = New DBHelper.DBHelperParameter() _
        {SetParameter("pi_dealer_id", dealer_id.ToByteArray),
         SetParameter("pi_msg_for", msg_for),
         SetParameter("pi_id", id.ToByteArray),
         SetParameter("pi_oc_template_code", template_code),
         SetParameter("pi_std_recipient", std_recipient),
         SetParameter("pi_cst_recipient", cst_recipient),
         SetParameter("pi_std_parameter", std_parameter),
         SetParameter("pi_cst_parameter", cst_parameter),
         SetParameter("pi_sender", sender)}

        outputParameter(0) = New DBHelper.DBHelperParameter("po_oc_message_id", GetType(Guid))
        outputParameter(1) = New DBHelper.DBHelperParameter("po_err_code", GetType(Integer))
        outputParameter(2) = New DBHelper.DBHelperParameter("po_err_msg", GetType(String), 5000)

        Try
            DBHelper.ExecuteSpParamBindByName(selectStmt, inputParameters, outputParameter)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

        message_id = CType(outputParameter(0).Value, Guid)
        err_no = CType(outputParameter(1).Value, Integer)
        err_msg = CType(outputParameter(2).Value, String)

    End Sub

    Function SetParameter(ByVal name As String, ByVal value As Object) As DBHelper.DBHelperParameter
        name = name.Trim
        If value Is Nothing Then value = DBNull.Value
        If value.GetType Is GetType(String) Then value = DirectCast(value, String).Trim

        Return New DBHelper.DBHelperParameter(name, value, value.GetType)
    End Function

#End Region

End Class
