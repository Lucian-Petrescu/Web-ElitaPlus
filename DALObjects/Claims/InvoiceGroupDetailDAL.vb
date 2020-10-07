
Imports Assurant.ElitaPlus.DALObjects.DBHelper

'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (4/6/2013)********************

Public Class InvoiceGroupDetailDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_INVOICE_GROUP_DETAIL"
    Public Const TABLE_KEY_NAME As String = "invoice_group_detail_id"

    Public Const COL_NAME_INVOICE_GROUP_DETAIL_ID As String = "invoice_group_detail_id"
    Public Const COL_NAME_INVOICE_GROUP_ID As String = "invoice_group_id"
    Public Const COL_NAME_INVOICE_RECONCILIATION_ID As String = "invoice_reconciliation_id"

    Public Const COL_NAME_INVOICE_AMOUNT As String = "invoice_amount"
    Public Const COL_NAME_INVOICE_NUMBER As String = "invoice_number"
    Public Const COL_NAME_INVOICE_STATUS_ID As String = "invoice_status_id"
    Public Const COL_NAME_INVOICE_DATE As String = "invoice_date"
    Public Const COL_NAME_COMPANY_ID As String = "company_id"

    Public Const COL_NAME_LINE_ITEM_TYPE As String = "line_item_type"
    Public Const Col_NAME_LINE_ITEM_DESCRIPTION As String = "line_item_description"

    Public Const COL_NAME_AUTHORIZATION_NUMBER As String = "authorization_number"
    Public Const COL_NAME_CLAIM_NUMBER As String = "claim_number"
    Public Const COL_NAME_AUTHORIZATION_ID As String = "claim_authorization_id"
    Public Const COL_NAME_CLAIM_ID As String = "claim_id"
    Public Const COL_NAME_CERT_ITEM_ID As String = "cert_item_id"
    Public Const COL_NAME_CERT_ID As String = "cert_id"
    Public Const COL_NAME_LINE_ITEM_NUMBER As String = "line_item_number"


    Public Const PAR_NAME_COMPANY_GROUP_ID As String = "p_company_group_id"
    Public Const PAR_INV_GRP_NUMBER As String = "p_invoice_group_number "
    Public Const PAR_NAME_RETURN As String = " p_return"
    Public Const PAR_NAME_EXCEPTION_MSG As String = "p_exception_msg"


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
        Dim parameters() As DBHelperParameter = New DBHelperParameter() {New DBHelperParameter("invoice_group_detail_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadList(invgrpid As Guid, languageid As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_LIST")
        Dim parameters = New OracleParameter() {New OracleParameter("language_id", languageid.ToByteArray), _
                                                    New OracleParameter("invoice_group_id", invgrpid.ToByteArray)}

        Dim ds As New DataSet
        Try
            ds = DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
        Return DBHelper.Fetch(selectStmt, TABLE_NAME)
    End Function
  
    Public Function LoadReconciledInvoiceslist(compids As ArrayList, servicecenterid As Guid, Invnumber As String, Invamount As String, InvoiceDate As String, _
                                   Invoicestatusid As Guid, languageid As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_INVOICE_RECONCILIATION_RECORDS")
        Dim parameters = New OracleParameter() {New OracleParameter("language_id", languageid.ToByteArray)}
        Dim whereClauseConditions As String = ""

        If Not (servicecenterid = Guid.Empty) Then
            whereClauseConditions &= " AND " & Environment.NewLine & "i.Service_center_id =" & MiscUtil.GetDbStringFromGuid(servicecenterid)

        End If

        If FormatSearchMask(Invnumber) Then
            whereClauseConditions &= " AND " & Environment.NewLine & COL_NAME_INVOICE_NUMBER & " " & Invnumber
        End If

        If FormatSearchMask(Invamount) Then
            whereClauseConditions &= " AND " & Environment.NewLine & "UPPER(" & COL_NAME_INVOICE_AMOUNT & ") " & Invamount
        End If

        If Not (InvoiceDate = Nothing) Then
            whereClauseConditions &= " AND " & Environment.NewLine & "trunc(i." & COL_NAME_INVOICE_DATE & ") = '" & InvoiceDate & "'"
        End If

      
        whereClauseConditions &= Environment.NewLine & " AND " & MiscUtil.BuildListForSql("cl." & COL_NAME_COMPANY_ID, compids, False)

        selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)

        Try
            Dim ds As New DataSet

            ds = DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)

            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Function LoadlineitemRecords(Invoiceid As Guid, languageid As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_LINE_ITEM_RECORDS")


        Dim parameters = New OracleParameter() {New OracleParameter("language_id", languageid.ToByteArray), _
                                                 New OracleParameter("invoice_id", Invoiceid.ToByteArray)}


        Dim ds As New DataSet
        Try
            DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)

            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function getinvoicegrpnumber(company_group_id As Guid) As DBHelperParameter()
        Dim selectStmt As String = Config("/SQL/GET_INVOICE_GROUP_NUMBER")
        Dim inputParameters() As DBHelperParameter = New DBHelperParameter() { _
                                     New DBHelperParameter(PAR_NAME_COMPANY_GROUP_ID, company_group_id.ToByteArray)}
        Dim outputParameters() As DBHelperParameter = New DBHelperParameter() { _
                            New DBHelperParameter(PAR_INV_GRP_NUMBER, GetType(String)), _
                            New DBHelperParameter(PAR_NAME_RETURN, GetType(Integer)), _
                            New DBHelperParameter(PAR_NAME_EXCEPTION_MSG, GetType(String), 500)}

        Try

            ' Call DBHelper Store Procedure
            ExecuteSp(selectStmt, inputParameters, outputParameters)

            Return outputParameters
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function GetStandardlineitems(languageid As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/GET_STANDARD_LINE_ITEMS")

        Dim ds As New DataSet
        Try
            ds = DBHelper.Fetch(selectStmt, TABLE_NAME)

            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function getlineitemvalues(servicecentercode As String) As DataSet
        Try
            Dim selectStmt As String = Config("/SQL/GET_CLAIM_AUTH_NUMBERS")

            Dim parameters = New OracleParameter() {New OracleParameter("code", servicecentercode)}

            Dim whereClauseConditions As String = ""
           

            Dim ds As New DataSet
            DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
            
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Function getmaxlineitemnumber(invoiceid As Guid) As DataSet

        Dim selectStmt As String = Config("/SQL/GET_MAX_LINE_ITEM_NUMBER")
        Dim parameters = New OracleParameter() {New OracleParameter("invoice_id", invoiceid.ToByteArray)}

        Try
            Dim ds As New DataSet
            DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try



    End Function

    Public Function getinvoicereconids(invoiceid As Guid) As DataSet

        Dim selectStmt As String = Config("/SQL/GET_INVOICE_RECON_ID")
        Dim parameters = New OracleParameter() {New OracleParameter("invoice_id", invoiceid.ToByteArray)}

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
        If Not ds.Tables(TABLE_NAME) Is Nothing Then
            MyBase.Update(ds.Tables(TABLE_NAME), Transaction, changesFilter)
        End If
    End Sub
#End Region


End Class


