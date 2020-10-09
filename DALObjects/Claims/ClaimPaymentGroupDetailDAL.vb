'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (3/19/2013)********************


Public Class ClaimPaymentGroupDetailDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_PAYMENT_GROUP_DETAIL"
    Public Const TABLE_KEY_NAME As String = "payment_group_detail_id"

    Public Const COL_NAME_PAYMENT_GROUP_DETAIL_ID As String = "payment_group_detail_id"
    Public Const COL_NAME_PAYMENT_GROUP_ID As String = "payment_group_id"
    Public Const COL_NAME_INVOICE_RECONCILIATION_ID As String = "invoice_reconciliation_id"

    Public Const COL_NAME_SERVICE_CENTER_CODE As String = "service_center_code"
    Public Const COL_NAME_CLAIM_NUMBER As String = "claim_number"
    Public Const COL_NAME_AUTHORIZATION_NUMBER As String = "authorization_number"
    Public Const COL_NAME_AUTHORIZATION_ID As String = "claim_authorization_id"
    Public Const COL_NAME_EXCLUDE_DEDUCTIBLE As String = "exclude_deductible"
    Public Const COL_NAME_INVOICE_NUMBER As String = "invoice_number"
    Public Const COL_NAME_INVOICE_DATE As String = "invoice_date"
    Public Const COL_NAME_RECONCILED_AMOUNT As String = "reconciled_amount"
    Public Const COL_NAME_DUE_DATE As String = "due_date"
    Public Const COL_NAME_COUNT As String = "count"
    Public Const COL_NAME_MOBILE_NUMBER As String = "work_phone"
    Public Const COL_NAME_ACCOUNT_NUMBER As String = "membership_number"
    Public Const COL_NAME_COMPANY_ID As String = "company_id"


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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("payment_group_detail_id", id.ToByteArray)}
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

    Public Function GetPaymentGroupDetail(pymntGroupId As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_PAYMENT_GROUP_DETAIL")
        Try
            Dim ds As New DataSet
            Dim pymntGrpIdPar As New DBHelper.DBHelperParameter(COL_NAME_PAYMENT_GROUP_ID, pymntGroupId.ToByteArray)
            Dim rowNum As New DBHelper.DBHelperParameter(PAR_NAME_ROW_NUMBER, MAX_NUMBER_OF_ROWS)

            DBHelper.Fetch(ds, selectStmt, TABLE_NAME, New DBHelper.DBHelperParameter() {pymntGrpIdPar, rowNum})
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Function GetPaymentGroupDetail(familyDS As DataSet, pymntGroupId As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_PYMNTGRP_DETAIL")
        Dim parameters() As OracleParameter

        parameters = New OracleParameter() {New OracleParameter(COL_NAME_PAYMENT_GROUP_ID, pymntGroupId.ToByteArray)}

        Try
            Return (DBHelper.Fetch(familyDS, selectStmt, TABLE_NAME, parameters))
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function GetClaimAuthorizationsToBePaid(pymntGroupId As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_CLAIM_AUTHORIZATIONS_TO_BE_PAID")
        Try
            Dim ds As New DataSet
            Dim pymntGrpIdPar As New DBHelper.DBHelperParameter(COL_NAME_PAYMENT_GROUP_ID, pymntGroupId.ToByteArray)

            DBHelper.Fetch(ds, selectStmt, TABLE_NAME, New DBHelper.DBHelperParameter() {pymntGrpIdPar})
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Function GetClaimAuthLineItems(claimAuthId As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_CLAIM_AUTH_LINE_ITEM_AMOUNTS")
        Try
            Dim ds As New DataSet
            Dim claimAuthIdPar As New DBHelper.DBHelperParameter(COL_NAME_AUTHORIZATION_ID, claimAuthId.ToByteArray)

            DBHelper.Fetch(ds, selectStmt, TABLE_NAME, New DBHelper.DBHelperParameter() {claimAuthIdPar})
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Function SelectPayables(compIds As ArrayList, claimNumber As String, InvGrpNumber As String, _
                                   InvNumber As String, mobileNumber As String, _
                                   invoiceDateRange As SearchCriteriaStructType(Of Date), _
                                   accountNumber As String, _
                                   serviceCenterName As String, authorizationNumber As String, _
                                   sortBy As String) As DataSet

        Dim selectStmt As String = Config("/SQL/SELECT_PAYABLES_LIST")

        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() { _
             New DBHelper.DBHelperParameter(PAR_NAME_ROW_NUMBER, MAX_NUMBER_OF_ROWS)}

        Dim whereClauseConditions As String = ""

        If FormatSearchMask(claimNumber) Then
            whereClauseConditions &= " AND " & Environment.NewLine & "mv." & COL_NAME_CLAIM_NUMBER & " " & claimNumber
        End If

        If FormatSearchMask(InvGrpNumber) Then
            whereClauseConditions &= " AND mv.INVOICE_RECONCILIATION_ID IN (select invoice_reconciliation_id from elp_invoice_group_detail " & _
                " where invoice_group_id IN (select invoice_group_id from elp_invoice_group where invoice_group_number " & _
                InvGrpNumber & "))"
        End If

        If FormatSearchMask(InvNumber) Then
            whereClauseConditions &= " AND " & Environment.NewLine & "mv." & COL_NAME_INVOICE_NUMBER & " " & InvNumber
        End If

        whereClauseConditions &= invoiceDateRange.ToSqlString("mv", COL_NAME_INVOICE_DATE, parameters)

        If FormatSearchMask(mobileNumber) Then
            whereClauseConditions &= " AND " & Environment.NewLine & "cert." & COL_NAME_MOBILE_NUMBER & " " & mobileNumber
        End If

        If FormatSearchMask(accountNumber) Then
            whereClauseConditions &= " AND " & Environment.NewLine & "cert." & COL_NAME_ACCOUNT_NUMBER & " " & accountNumber
        End If

        If FormatSearchMask(serviceCenterName) Then
            whereClauseConditions &= " AND " & Environment.NewLine & "UPPER(sc." & COL_NAME_DESCRIPTION & ") " & serviceCenterName
        End If

        If FormatSearchMask(authorizationNumber) Then
            whereClauseConditions &= " AND " & Environment.NewLine & "UPPER(mv." & COL_NAME_AUTHORIZATION_NUMBER & ") " & authorizationNumber.ToUpper
        End If

        whereClauseConditions &= Environment.NewLine & " AND " & MiscUtil.BuildListForSql("mv." & COL_NAME_COMPANY_ID, compIds, False)

        selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)

        If Not IsNothing(sortBy) Then
            selectStmt = selectStmt.Replace(DYNAMIC_ORDER_BY_CLAUSE_PLACE_HOLDER, _
                                            Environment.NewLine & "ORDER BY " & Environment.NewLine & sortBy)
        Else
            selectStmt = selectStmt.Replace(DYNAMIC_ORDER_BY_CLAUSE_PLACE_HOLDER, "")
        End If

        Try
            Dim ds As New DataSet
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
        If ds.Tables(TABLE_NAME) IsNot Nothing Then
            MyBase.Update(ds.Tables(TABLE_NAME), Transaction, changesFilter)
        End If
    End Sub
#End Region




End Class
