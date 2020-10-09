'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (11/16/2007)********************
Public Class AcctPremInvoiceDAL
    Inherits DALBase

#Region "Delegate"
    Public Delegate Sub AsyncCaller(DealerID As Guid, userNetworkId As String, sqlStmt As String)
#End Region

#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_ACCT_PREM_INVOICE"
    Public Const TABLE_KEY_NAME As String = "acct_prem_invoice_id"

    Public Const COL_NAME_ACCT_PREM_INVOICE_ID As String = "acct_prem_invoice_id"
    Public Const COL_NAME_COMPANY_ID As String = "company_id"
    Public Const COL_NAME_DEALER_ID As String = "dealer_id"
    Public Const COL_NAME_BRANCH_ID As String = "branch_id"
    Public Const COL_NAME_INVOICE_NUMBER As String = "invoice_number"
    Public Const COL_NAME_CREDIT_NOTE_NUMBER As String = "credit_note_number"
    Public Const COL_NAME_PREVIOUS_INVOICE_DATE As String = "previous_invoice_date"
    Public Const COL_NAME_NEW_TOTAL_CERT As String = "new_total_cert"
    Public Const COL_NAME_NEW_GROSS_AMT_RECVD As String = "new_gross_amt_recvd"
    Public Const COL_NAME_NEW_PREMIUM_WRITTEN As String = "new_premium_written"
    Public Const COL_NAME_NEW_COMMISSION As String = "new_commission"
    Public Const COL_NAME_NEW_TAX1 As String = "new_tax1"
    Public Const COL_NAME_NEW_TAX2 As String = "new_tax2"
    Public Const COL_NAME_NEW_TAX3 As String = "new_tax3"
    Public Const COL_NAME_NEW_TAX4 As String = "new_tax4"
    Public Const COL_NAME_NEW_TAX5 As String = "new_tax5"
    Public Const COL_NAME_NEW_TAX6 As String = "new_tax6"
    Public Const COL_NAME_NEW_PREMIUM_TOTAL As String = "new_premium_total"
    Public Const COL_NAME_CANCEL_TOTAL_CERT As String = "cancel_total_cert"
    Public Const COL_NAME_CANCEL_GROSS_AMT_RECVD As String = "cancel_gross_amt_recvd"
    Public Const COL_NAME_CANCEL_PREMIUM_WRITTEN As String = "cancel_premium_written"
    Public Const COL_NAME_CANCEL_COMMISSION As String = "cancel_commission"
    Public Const COL_NAME_CANCEL_TAX1 As String = "cancel_tax1"
    Public Const COL_NAME_CANCEL_TAX2 As String = "cancel_tax2"
    Public Const COL_NAME_CANCEL_TAX3 As String = "cancel_tax3"
    Public Const COL_NAME_CANCEL_TAX4 As String = "cancel_tax4"
    Public Const COL_NAME_CANCEL_TAX5 As String = "cancel_tax5"
    Public Const COL_NAME_CANCEL_TAX6 As String = "cancel_tax6"
    Public Const COL_NAME_CANCEL_PREMIUM_TOTAL As String = "cancel_premium_total"

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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("acct_prem_invoice_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadList(companyIds As ArrayList, DealerID As Guid, InvNum As String, _
                                BeginDate As Date, EndDate As Date) As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_LIST")
        Dim whereClauseConditions As String = ""

        whereClauseConditions = MiscUtil.BuildListForSql("i." & COL_NAME_COMPANY_ID, companyIds, False)

        If DealerID <> Guid.Empty Then
            whereClauseConditions &= Environment.NewLine & " AND i." & COL_NAME_DEALER_ID & " = '" & GuidToSQLString(DealerID) & "'"
        End If

        If InvNum <> "" AndAlso FormatSearchMask(InvNum) Then
            whereClauseConditions &= Environment.NewLine & " AND (Upper(i." & COL_NAME_INVOICE_NUMBER & ") " & InvNum.ToUpper _
                                        & " or Upper(i." & COL_NAME_CREDIT_NOTE_NUMBER & ") " & InvNum.ToUpper & ")"
        End If

        If BeginDate > Date.MinValue Then
            whereClauseConditions &= Environment.NewLine & " AND TRUNC(i." & COL_NAME_CREATED_DATE & ") >= TO_DATE('" & BeginDate.ToString("MM/dd/yyyy") & "','mm/dd/yyyy')"
        End If

        If EndDate > Date.MinValue Then
            whereClauseConditions &= Environment.NewLine & " AND TRUNC(i." & COL_NAME_CREATED_DATE & ") <= TO_DATE('" & EndDate.ToString("MM/dd/yyyy") & "','mm/dd/yyyy')"
        End If

        selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)

        Try
            Return DBHelper.Fetch(selectStmt, TABLE_NAME)
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

#Region "Create Invoice"
    'Private Sub AsyncExecuteSPCreateInvoice(ByVal DealerID As Guid, ByVal userNetworkId As String, ByVal sqlStmt As String)
    '    Dim inParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("v_dealer_id", DealerID.ToByteArray), New DBHelper.DBHelperParameter("v_network_id", userNetworkId)}
    '    Dim outParameters() As DBHelper.DBHelperParameter
    '    DBHelper.ExecuteSp(sqlStmt, inParameters, outParameters)
    'End Sub

    'Private Sub ExecuteSPCreateInvoice(ByVal DealerID As Guid, ByVal userNetworkId As String, ByVal sqlStmt As String)
    '    Dim aSyncHandler As New AsyncCaller(AddressOf AsyncExecuteSPCreateInvoice)
    '    aSyncHandler.BeginInvoke(DealerID, userNetworkId, sqlStmt, Nothing, Nothing)
    'End Sub

    Public Sub CreateInvoice(DealerID As Guid, userNetworkId As String)
        Dim sqlStmt As String
        sqlStmt = Config("/SQL/CREATE_INVOICE")
        Try
            Dim inParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("v_dealer_id", DealerID.ToByteArray), New DBHelper.DBHelperParameter("v_network_id", userNetworkId)}
            Dim outParameters() As DBHelper.DBHelperParameter
            DBHelper.ExecuteSp(sqlStmt, inParameters, outParameters)
            'ExecuteSPCreateInvoice(DealerID, userNetworkId, sqlStmt)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub
#End Region
End Class


