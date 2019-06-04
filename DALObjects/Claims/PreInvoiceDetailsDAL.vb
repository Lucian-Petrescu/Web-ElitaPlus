'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (2/17/2015)********************


Public Class PreInvoiceDetailsDAL
    Inherits DALBase


#Region "Constants"
	Public Const TABLE_NAME As String = "ELP_PRE_INVOICE_DETAILS"
	Public Const TABLE_KEY_NAME As String = "pre_invoice_details_id"
	
	Public Const COL_NAME_PRE_INVOICE_DETAILS_ID As String = "pre_invoice_details_id"
	Public Const COL_NAME_PRE_INVOICE_ID As String = "pre_invoice_id"
    Public Const COL_NAME_CLAIM_ID As String = "claim_id"
    Public Const COL_BATCH_NUMBER As String = "batch_Number"
    Public Const COL_STATUS As String = "status"
    Public Const COL_CREATED_DATE As String = "created_date"
    Public Const COL_DISPLAY_DATE As String = "display_date"
    Public Const COL_CLAIMS As String = "claims_count"
    Public Const COL_TOTAL_AMOUNT As String = "total_amount"
    Public Const PAR_NAME_CLAIM_IDs As String = "pi_claim_ids"
    Public Const PAR_NAME_COMPANY_ID As String = "pi_company_id"
    Public Const PAR_NAME_COMMENTS As String = "pi_comments"
    Public Const COL_LANGUAGE_ID As String = "language_id"

    Public Const PAR_NAME_EXCEPTION_MSG As String = "po_exception_msg"
    Public Const PAR_NAME_RETURN As String = "po_return"
    Public Const PAR_CLAIM_ID As String = "claim_id"
    Public Const PAR_NAME_PRE_INVOICE_ID As String = "pre_invoice_id"
    Public Const PAR_NAME_TOTAL_AMOUNT As String = "total_amount"
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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("pre_invoice_details_id", id.ToByteArray)}
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

    Public Function FetchPreInvoiceClaims(ByVal preInvoiceId As Guid) As DataSet
        Try
            Dim selectStmt As String = Me.Config("/SQL/PRE_INVOICE_CLAIMS")
            Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("pre_invoice_id", preInvoiceId.ToByteArray)}

            Dim ds As New DataSet
            ds = DBHelper.Fetch(selectStmt, Me.TABLE_NAME)

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function
    Public Function LoadPreInvoiceProcessClaims(ByVal preInvoiceID As Guid, ByVal serviceCenterId As Guid, ByVal MasterCenterId As Guid, ByVal languageid As Guid) As DataSet

        Try
            Dim selectStmt As String = Me.Config("/SQL/LOAD_PRE_INVOICE_LIST")
            Dim whereClauseConditions As String = ""

            Dim parameters() As DBHelper.DBHelperParameter = _
            New DBHelper.DBHelperParameter() _
            { _
                New DBHelper.DBHelperParameter(COL_NAME_LANGUAGE_ID, languageid.ToByteArray()), _
                New DBHelper.DBHelperParameter(Me.COL_NAME_PRE_INVOICE_ID, preInvoiceID.ToByteArray())}


            If Not (serviceCenterId = Guid.Empty) Then
                whereClauseConditions &= " AND " & Environment.NewLine & "sc." & "service_center_id" & "= " & MiscUtil.GetDbStringFromGuid(serviceCenterId)
            End If

            If Not (MasterCenterId = Guid.Empty) Then
                whereClauseConditions &= " AND " & Environment.NewLine & "sc." & "service_center_id" & "= " & MiscUtil.GetDbStringFromGuid(MasterCenterId)
            End If

            'If Me.FormatSearchMask(serviceCenterName) Then
            '    whereClauseConditions &= " AND " & Environment.NewLine & "UPPER(sc." & "code" & ") " & serviceCenterName.ToUpper
            'End If

            'If Me.FormatSearchMask(MasterCenterName) Then
            '    whereClauseConditions &= " AND " & Environment.NewLine & "UPPER(sc." & "description" & ") " & MasterCenterName.ToUpper
            'End If

            If Not whereClauseConditions = "" Then
                selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
            Else
                selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, "")
            End If

            Dim ds As New DataSet
            Return DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function ApprovePreInvoice(ByVal company_id As Guid, ByVal pre_invoice_id As Guid) As DBHelper.DBHelperParameter()
        Dim selectStmt As String = Me.Config("/SQL/APPROVE_PRE_INVOICE")
        Dim inputParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() { _
                            New DBHelper.DBHelperParameter(Me.PAR_NAME_COMPANY_ID, company_id.ToByteArray), _
                            New DBHelper.DBHelperParameter(Me.COL_NAME_PRE_INVOICE_ID, pre_invoice_id.ToByteArray)}
        Dim outputParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() { _
                            New DBHelper.DBHelperParameter(Me.PAR_NAME_RETURN, GetType(Integer)), _
                            New DBHelper.DBHelperParameter(Me.PAR_NAME_EXCEPTION_MSG, GetType(String), 500)}

        Try

            ' Call DBHelper Store Procedure
            DBHelper.ExecuteSp(selectStmt, inputParameters, outputParameters)

            Return outputParameters
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function RejectPreInvoiceClaims(ByVal company_id As Guid, ByVal claimIDs As String, ByVal pre_invoice_id As Guid, ByVal Comments As String) As DBHelper.DBHelperParameter()
        Dim selectStmt As String = Me.Config("/SQL/REJECT_PRE_INVOICE_CLAIMS")
        Dim inputParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() { _
                            New DBHelper.DBHelperParameter(Me.COL_NAME_PRE_INVOICE_ID, pre_invoice_id.ToByteArray), _
                            New DBHelper.DBHelperParameter(Me.PAR_NAME_COMPANY_ID, company_id.ToByteArray), _
                            New DBHelper.DBHelperParameter(Me.PAR_NAME_CLAIM_IDs, claimIDs), _
                            New DBHelper.DBHelperParameter(Me.PAR_NAME_COMMENTS, Comments)}
        Dim outputParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() { _
                            New DBHelper.DBHelperParameter(Me.PAR_NAME_RETURN, GetType(Integer)), _
                            New DBHelper.DBHelperParameter(Me.PAR_NAME_EXCEPTION_MSG, GetType(String), 500)}

        Try

            ' Call DBHelper Store Procedure
            DBHelper.ExecuteSp(selectStmt, inputParameters, outputParameters)

            Return outputParameters
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function CheckClaimInPreInvoice(ByVal Claim_Id As Guid) As Integer
        Dim selectStmt As String = Me.Config("/SQL/CHECK_FOR_CLAIM_IN_PRE_INVOICE")
        Dim inParameters(0) As DBHelper.DBHelperParameter
        Dim count As Object

        inParameters(0) = New DBHelper.DBHelperParameter(Me.PAR_CLAIM_ID, Claim_Id.ToByteArray)

        count = DBHelper.ExecuteScalar(selectStmt, inParameters)

        Return CInt(count)

    End Function

    Public Function UpdatePreInvoiceTotal(ByVal PreInvoiceId As Guid, ByVal totalAmount As Decimal) As Integer
        Dim selectStmt As String = Me.Config("/SQL/UPDATE_PRE_INVOICE_TOTAL")
        Dim inParameters(1) As DBHelper.DBHelperParameter
        Dim count As Object

        inParameters(0) = New DBHelper.DBHelperParameter(Me.PAR_NAME_TOTAL_AMOUNT, totalAmount)
        inParameters(1) = New DBHelper.DBHelperParameter(Me.PAR_NAME_PRE_INVOICE_ID, PreInvoiceId.ToByteArray)

        count = DBHelper.ExecuteScalar(selectStmt, inParameters)

        Return CInt(count)

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


