'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (8/15/2017)********************

Public Class OcTemplateDAL
    Inherits DALBase

#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_OC_TEMPLATE"
    Public Const TABLE_KEY_NAME As String = "oc_template_id"

    Public Const COL_NAME_OC_TEMPLATE_ID As String = "oc_template_id"
    Public Const COL_NAME_OC_TEMPLATE_GROUP_ID As String = "oc_template_group_id"
    Public Const COL_NAME_TEMPLATE_CODE As String = "template_code"
    Public Const COL_NAME_DESCRIPTION As String = "description"
    Public Const COL_NAME_HAS_CUSTOMIZED_PARAMS_XCD As String = "has_customized_params_xcd"
    Public Const COL_NAME_ALLOW_MANUAL_USE_XCD As String = "allow_manual_use_xcd"
    Public Const COL_NAME_ALLOW_MANUAL_RESEND_XCD As String = "allow_manual_resend_xcd"
    Public Const COL_NAME_EFFECTIVE_DATE As String = "effective_date"
    Public Const COL_NAME_EXPIRATION_DATE As String = "expiration_date"

    Public Const COL_NAME_TEMPLATE_TYPE_XCD As String = "template_type_xcd"
	Public Const COL_NAME_SMS_APP_KEY As String = "sms_app_key"
	Public Const COL_NAME_SMS_SHORT_CODE As String = "sms_short_code"
	Public Const COL_NAME_SMS_TRIGGER_ID As String = "sms_trigger_id"

    Public Const COL_NAME_COMPANY_ID As String = "company_id"
    Public Const COL_NAME_NUMBER_OF_MESSAGES As String = "number_of_messages"
    Public Const COL_NAME_NUMBER_OF_TEMPLATES As String = "number_of_templates"
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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("oc_template_id", id.ToByteArray)}
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

    Public Function LoadList(companyList As ArrayList,
                             dealerId As Guid,
                             templateGroupCodeMask As String,
                             templateCodeMask As String) As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_LIST_BY_DEALERS_AND_TEMPLATE_GROUP")
        Dim inClausecondition As String = ""
        Dim whereClauseConditions As String = ""
        Dim dealerWhereClauseConditions As String = ""

        inClausecondition &= "and d." & MiscUtil.BuildListForSql(COL_NAME_COMPANY_ID, companyList, True)

        If (Not String.IsNullOrEmpty(templateGroupCodeMask)) Then
            templateGroupCodeMask = templateGroupCodeMask.Trim()

            If FormatSearchMask(templateGroupCodeMask) Then
                whereClauseConditions &= Environment.NewLine & "and " & "UPPER(tg.CODE)" & templateGroupCodeMask.ToUpper
            End If
        End If

        If (Not String.IsNullOrEmpty(templateCodeMask)) Then
            templateCodeMask = templateCodeMask.Trim()

            If FormatSearchMask(templateCodeMask) Then
                whereClauseConditions &= Environment.NewLine & "and " & "UPPER(t.TEMPLATE_CODE)" & templateCodeMask.ToUpper
            End If
        End If

        If Not dealerId.Equals(Guid.Empty) Then
            dealerWhereClauseConditions &= Environment.NewLine & "and " & "d.DEALER_ID = " & MiscUtil.GetDbStringFromGuid(dealerId)
        End If

        selectStmt = selectStmt.Replace(DYNAMIC_IN_CLAUSE_PLACE_HOLDER, inClausecondition)

        If Not dealerWhereClauseConditions = "" Then
            selectStmt = selectStmt.Replace("--dynamic_where_clause_dealer", dealerWhereClauseConditions)
        Else
            selectStmt = selectStmt.Replace("--dynamic_where_clause_dealer", String.Empty)
        End If

        If Not whereClauseConditions = "" Then
            selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
        Else
            selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, String.Empty)
        End If

        Try
            Return DBHelper.Fetch(selectStmt, TABLE_NAME)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function GetCountOfTemplatesByCodeAndGroupExludingTemplateId(templateCode As String, id As Guid, templateGroupId As Guid) As DataSet
        Try
            Dim selectStmt As String = Config("/SQL/GET_COUNT_BY_CODE_EXCLUDING_TEMPLATE_ID")
            Dim ds As New DataSet
            Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("template_code", templateCode), New DBHelper.DBHelperParameter("oc_template_id", id.ToByteArray), New DBHelper.DBHelperParameter("oc_template_group_id", templateGroupId.ToByteArray)}
            DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Sub LoadList(ds As DataSet, templateGroupId As Guid)
        Dim selectStmt As String = Config("/SQL/LOAD_LIST_BY_TEMPLATE_GROUP_ID")
        DBHelper.Fetch(ds, selectStmt, TABLE_NAME, New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(COL_NAME_OC_TEMPLATE_GROUP_ID, templateGroupId.ToByteArray)})
    End Sub

    Public Function GetAssociatedMessageCount(templateId As Guid)
        Try
            Dim selectStmt As String = Config("/SQL/GET_ASSOCIATED_MESSAGE_COUNT")
            Dim ds As New DataSet
            Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("oc_template_id", templateId.ToByteArray)}
            DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
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

    Public Sub UpdateFamily(dataset As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing)
        Dim tr As IDbTransaction = Transaction

        If tr Is Nothing Then
            tr = DBHelper.GetNewTransaction
        End If

        Dim templateParamsDAL As New OcTemplateParamsDAL
        Dim templateRecipientDAL As New OcTemplateRecipientDAL

        Try
            'First Pass updates Deletions           
            templateParamsDAL.Update(dataset, tr, DataRowState.Deleted)
            templateRecipientDAL.Update(dataset, tr, DataRowState.Deleted)
            MyBase.Update(dataset.Tables(TABLE_NAME), tr, DataRowState.Deleted)

            'Second Pass updates additions and changes 
            Update(dataset.Tables(TABLE_NAME), tr, DataRowState.Added Or DataRowState.Modified)
            templateParamsDAL.Update(dataset, tr, DataRowState.Added Or DataRowState.Modified)
            templateRecipientDAL.Update(dataset, tr, DataRowState.Added Or DataRowState.Modified)

            If Not dataset.Tables(TransactionLogHeaderDAL.TABLE_NAME) Is Nothing AndAlso dataset.Tables(TransactionLogHeaderDAL.TABLE_NAME).Rows.Count > 0 Then
                Dim oTransactionLogHeaderDAL As New TransactionLogHeaderDAL
                oTransactionLogHeaderDAL.Update(dataset, tr, DataRowState.Added Or DataRowState.Modified)
            End If

            If Transaction Is Nothing Then
                'We are the creator of the transaction we should commit it  and close the connection
                DBHelper.Commit(tr)
                dataset.AcceptChanges()
            End If
        Catch ex As Exception
            If Transaction Is Nothing Then
                'We are the creator of the transaction we should commit it  and close the connection
                DBHelper.RollBack(tr)
            End If
            Throw ex
        End Try
    End Sub
#End Region
End Class


