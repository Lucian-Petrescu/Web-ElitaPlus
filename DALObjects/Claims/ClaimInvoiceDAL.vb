'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (12/15/2004)********************
Imports Assurant.ElitaPlus.DALObjects.DBHelper


Public Class ClaimInvoiceDAL
    Inherits DALBase

#Region "GetTaxRateData"

    Public Class TaxRateData

        Public countryID As Guid
        Public taxtypeID As Guid
        Public regionID As Guid
        Public salesDate As Date
        Public taxRate As Decimal
        Public dealerID As Guid
    End Class

    Public Class ClaimTaxRatesData

        Public countryID As Guid
        Public regionID As Guid
        Public salesDate As Date
        Public dealerID As Guid
        Public claim_type As String
        Public taxRateClaimSuperDefault As Decimal
        Public taxRateClaimDefault As Decimal
        Public taxRateClaimDiagnostics As Decimal
        Public taxRateClaimOther As Decimal
        Public taxRateClaimDisposition As Decimal
        Public taxRateClaimLabor As Decimal
        Public taxRateClaimParts As Decimal
        Public taxRateClaimShipping As Decimal
        Public taxRateClaimService As Decimal
        Public taxRateClaimTrip As Decimal

        Public computeMethodCodeClaimSuperDefault As String
        Public computeMethodCodeClaimDefault As String
        Public computeMethodCodeClaimDiagnostics As String
        Public computeMethodCodeClaimOther As String
        Public computeMethodCodeClaimDisposition As String
        Public computeMethodCodeClaimLabor As String
        Public computeMethodCodeClaimParts As String
        Public computeMethodCodeClaimShipping As String
        Public computeMethodCodeClaimService As String
        Public computeMethodCodeClaimTrip As String

        Public applyWithholdingFlagClaimSuperDefault As String
        Public applyWithholdingFlagClaimDefault As String
        Public applyWithholdingFlagClaimDiagnostics As String
        Public applyWithholdingFlagClaimOther As String
        Public applyWithholdingFlagClaimDisposition As String
        Public applyWithholdingFlagClaimLabor As String
        Public applyWithholdingFlagClaimParts As String
        Public applyWithholdingFlagClaimShipping As String
        Public applyWithholdingFlagClaimService As String
        Public applyWithholdingFlagClaimTrip As String

    End Class

#End Region

#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_CLAIM_INVOICE"
    Public Const TABLE_NAME_CLAIM_TAX_RATES As String = "CLAIM_TAX_RATES"
    Public Const TABLE_KEY_NAME As String = "claim_invoice_id"

    Public Const COL_NAME_CLAIM_INVOICE_ID As String = "claim_invoice_id"
    Public Const COL_NAME_COMPANY_ID As String = "company_id"
    Public Const COL_NAME_CLAIM_ID As String = "claim_id"
    Public Const COL_NAME_REPAIR_CODE_ID As String = "repair_code_id"
    Public Const COL_NAME_CLAIM_NUMBER As String = "claim_number"
    Public Const COL_NAME_SVC_CONTROL_NUMBER As String = "svc_control_number"
    Public Const COL_NAME_RECORD_COUNT As String = "record_count"
    Public Const COL_NAME_BATCH_NUMBER As String = "batch_number"
    Public Const COL_NAME_CAUSE_OF_LOSS_ID As String = "cause_of_loss_id"
    Public Const COL_NAME_REPAIR_DATE As String = "repair_date"
    Public Const COL_NAME_INVOICE_DATE As String = "invoice_date"
    Public Const COL_NAME_REPAIR_ESTIMATE As String = "repair_estimate"
    Public Const COL_NAME_LABOR_AMT As String = "labor_amt"
    Public Const COL_NAME_LABOR_TAX As String = "labor_tax"
    Public Const COL_NAME_PART_AMOUNT As String = "part_amount"
    Public Const COL_NAME_PART_TAX As String = "part_tax"
    Public Const COL_NAME_SERVICE_CHARGE As String = "service_charge"
    Public Const COL_NAME_TRIP_AMOUNT As String = "trip_amount"
    Public Const COL_NAME_SHIPPING_AMOUNT As String = "shipping_amount"
    Public Const COL_NAME_OTHER_AMOUNT As String = "other_amount"
    Public Const COL_NAME_OTHER_EXPLANATION As String = "other_explanation"
    Public Const COL_NAME_IVA_AMOUNT As String = "iva_amount"
    Public Const COL_NAME_DEDUCTIBLE_AMOUNT As String = "deductible_amount"
    Public Const COL_NAME_DEDUCTIBLE_TAX_AMOUNT As String = "deductible_tax_amount"
    Public Const COL_NAME_AMOUNT As String = "amount"
    Public Const COL_NAME_DISBURSEMENT_ID As String = "disbursement_id"
    Public Const COL_NAME_REJECT_REASON As String = "reject_reason"
    Public Const COL_NAME_AUTHORIZATION_NUMBER As String = "authorization_number"
    Public Const COL_NAME_SOURCE As String = "source"
    Public Const COL_NAME_REGION_ID As String = "region_id"
    Public Const COL_NAME_CLAIM_AUTHORIZATION_ID As String = "claim_authorization_id"
    Public Const COL_NAME_BONUS As String = "bonus"
    Public Const COL_NAME_BONUS_TAX As String = "bonus_tax"
    Public Const COL_NAME_DISPOSITION_AMOUNT As String = "disposition_amount"
    Public Const COL_NAME_DIAGNOSTICS_AMOUNT As String = "diagnostics_amount"
    Public Const COL_NAME_WITHHOLDING_AMOUNT As String = "withholding_amount"
    Public Const COL_NAME_TOTAL_TAX_AMOUNT As String = "total_tax_amount"
    Public Const COL_NAME_PAY_TO_CUST_AMOUNT As String = "pay_to_cust_amount"

    Public Const TOTAL_INPUT_PARAM_CANCEL = 4
    Public Const TOTAL_OUTPUT_PARAM_CANCEL = 1

    Public Const TOTAL_INPUT_PARAM_CLAIMTAXRATE = 3
    Public Const TOTAL_OUTPUT_PARAM_CLAIMTAXRATE = 1

    Public Const P_COUNTRY_ID = 0
    Public Const P_TAXTYPE_ID = 1
    Public Const P_REGION_ID = 2
    Public Const P_SALES_DATE = 3

    Public Const P_SALES_TAX_RATE = 0
    Public Const P_RETURN = 1

    Public Const COL_NAME_P_COUNTRY_ID As String = "p_country_id"
    Public Const COL_NAME_P_TAXTYPE_ID As String = "p_tax_type_id"
    Public Const COL_NAME_P_REGION_ID As String = "p_state_id"
    Public Const COL_NAME_P_SALES_DATE As String = "p_sales_date"
    Public Const COL_NAME_P_SALES_TAX_RATE As String = "p_sales_tax_rate"
    Public Const COL_NAME_P_RETURN As String = "p_return"
    Public Const COL_NAME_V_CLAIM_TAX_RATES As String = "v_claim_tax_rates"

    Public Const COMPANY_ID = 0

    Public Const TOTAL_PARAM = 0

    'REQ-1150
    Public Const COL_NAME_DEALER_ID As String = "dealer_id"
    Public Const COL_NAME_P_CLAIM_TYPE As String = "p_claim_type"

    Public Const P_DEALER_ID = 4
    Public Const COL_NAME_P_DEALER_ID As String = "p_dealer_id"

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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("claim_invoice_id", id.ToByteArray)}
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

    'Public Function LoadList(ByVal compId As Guid, ByVal invoiceNumber As String, ByVal payee As String, _
    '                         ByVal claimNumber As String, ByVal createdDate As String, ByVal invoiceAmount As String, ByVal sortBy As String) As DataSet

    '    Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST_DYNAMIC")

    '    'since in our SQL we already have where clause..
    '    selectStmt &= Environment.NewLine & " and "
    '    selectStmt &= Environment.NewLine & "inv.company_id = '" & Me.GuidToSQLString(compId) & "'"

    '    If ((Not (invoiceNumber Is Nothing)) AndAlso (Me.FormatSearchMask(invoiceNumber))) Then
    '        selectStmt &= Environment.NewLine & "AND inv.svc_control_number" & invoiceNumber
    '    End If

    '    If ((Not (payee Is Nothing)) AndAlso (Me.FormatSearchMask(payee))) Then
    '        selectStmt &= Environment.NewLine & "AND UPPER(dis.payee)" & payee.ToUpper
    '    End If

    '    If ((Not (claimNumber Is Nothing)) AndAlso (Me.FormatSearchMask(claimNumber))) Then
    '        selectStmt &= Environment.NewLine & "AND inv.claim_number" & claimNumber
    '    End If

    '    'If ((Not (createdDate Is Nothing)) AndAlso (Me.FormatSearchMask(createdDate))) Then
    '    '    selectStmt &= Environment.NewLine & "AND to_char(inv.created_date,'mmddyyyy')" & createdDate
    '    'End If

    '    If ((Not (createdDate Is Nothing)) AndAlso (Me.FormatSearchMask(createdDate))) Then
    '        selectStmt &= Environment.NewLine & "AND " & GetOracleDate("inv.created_date") & createdDate
    '    End If

    '    If ((Not (invoiceAmount Is Nothing)) AndAlso (Me.FormatSearchMask(invoiceAmount))) Then
    '        selectStmt &= Environment.NewLine & "AND inv.amount" & invoiceAmount
    '    End If

    '    selectStmt &= Environment.NewLine & "AND ROWNUM <= 1001"
    '    selectStmt &= Environment.NewLine & "ORDER BY " & sortBy
    '    Try
    '        Return (DBHelper.Fetch(selectStmt, Me.TABLE_NAME))
    '    Catch ex As Exception
    '        Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
    '    End Try

    'End Function

    Public Function LoadList(compIds As ArrayList, invoiceNumber As String, payee As String, _
                             claimNumber As String, createdDate As String, invoiceAmount As String, sortBy As String) As DataSet

        Dim selectStmt As String = Config("/SQL/LOAD_LIST_DYNAMIC")

        'since in our SQL we already have where clause..
        selectStmt &= Environment.NewLine & " and "
        'selectStmt &= Environment.NewLine & "inv.company_id = '" & Me.GuidToSQLString(compIds) & "'"
        selectStmt &= Environment.NewLine & MiscUtil.BuildListForSql("inv." & COL_NAME_COMPANY_ID, compIds, False)

        If ((Not (invoiceNumber Is Nothing)) AndAlso (FormatSearchMask(invoiceNumber))) Then
            selectStmt &= Environment.NewLine & "AND UPPER(inv.svc_control_number)" & invoiceNumber.ToUpper
        End If

        If ((Not (payee Is Nothing)) AndAlso (FormatSearchMask(payee))) Then
            selectStmt &= Environment.NewLine & "AND UPPER(dis.payee)" & payee.ToUpper
        End If

        If ((Not (claimNumber Is Nothing)) AndAlso (FormatSearchMask(claimNumber))) Then
            selectStmt &= Environment.NewLine & "AND inv.claim_number" & claimNumber
        End If

        'If ((Not (createdDate Is Nothing)) AndAlso (Me.FormatSearchMask(createdDate))) Then
        '    selectStmt &= Environment.NewLine & "AND to_char(inv.created_date,'mmddyyyy')" & createdDate
        'End If

        If ((Not (createdDate Is Nothing)) AndAlso (FormatSearchMask(createdDate))) Then
            selectStmt &= Environment.NewLine & "AND " & GetOracleDate("inv.created_date") & createdDate
        End If

        If ((Not (invoiceAmount Is Nothing)) AndAlso (FormatSearchMask(invoiceAmount))) Then
            selectStmt &= Environment.NewLine & "AND inv.amount" & invoiceAmount
        End If

        selectStmt &= Environment.NewLine & "AND ROWNUM < " & MAX_NUMBER_OF_ROWS
        selectStmt &= Environment.NewLine & "ORDER BY " & sortBy
        Try
            Return (DBHelper.Fetch(selectStmt, TABLE_NAME))
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Function GetClaimSumOfDeductibles(companyId As Guid, claimID As Guid) As DecimalType
        Dim selectStmt As String = Config("/SQL/GET_SUM_OF_INVOICES")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(COL_NAME_CLAIM_ID, claimID.ToByteArray), New DBHelper.DBHelperParameter(COL_NAME_COMPANY_ID, companyId.ToByteArray)}
        Dim ds As DataSet = New DataSet
        Try
            DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
            If ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
                If ds.Tables(0).Rows(0)(2) Is DBNull.Value Then
                    Return New DecimalType(0D)
                Else
                    Return New DecimalType(CType(ds.Tables(0).Rows(0)(2), Decimal))
                End If
            Else
                Return New DecimalType(0D)
            End If
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Function GetClaimSumOfInvoices(companyId As Guid, claimID As Guid) As DecimalType
        Dim selectStmt As String = Config("/SQL/GET_SUM_OF_INVOICES")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(COL_NAME_CLAIM_ID, claimID.ToByteArray), New DBHelper.DBHelperParameter(COL_NAME_COMPANY_ID, companyId.ToByteArray)}
        Dim ds As DataSet = New DataSet
        Try
            DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
            If ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
                If ds.Tables(0).Rows(0)(0) Is DBNull.Value Then
                    Return New DecimalType(0D)
                Else
                    Return New DecimalType(CType(ds.Tables(0).Rows(0)(0) + CType(ds.Tables(0).Rows(0)(2), Decimal), Decimal))
                End If
            Else
                Return New DecimalType(0D)
            End If
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function
    Public Sub GetReplacementTaxType(ServiceCenterID As Guid, RiskTypeID As Guid, _
                                     EffectiveDate As Date, ProductPrice As Decimal, _
                                     ByRef ReplacementTaxTypeID As Guid)
        Try
            Dim selectStmt As String = Config("/SQL/GET_REPLACEMENT_TAX_TYPE")
            Dim inputParameters(3) As DBHelperParameter
            Dim outputParameter(1) As DBHelperParameter

            inputParameters(0) = New DBHelperParameter("V_ServiceCenterID", ServiceCenterID.ToByteArray)
            inputParameters(1) = New DBHelperParameter("V_RiskTypeID", RiskTypeID.ToByteArray)
            inputParameters(2) = New DBHelperParameter("V_EffectiveDate", EffectiveDate)
            inputParameters(3) = New DBHelperParameter("V_ProductPrice", ProductPrice)

            outputParameter(0) = New DBHelperParameter("p_ReplacementTaxType", ReplacementTaxTypeID.ToByteArray.GetType)
            outputParameter(1) = New DBHelperParameter(COL_NAME_P_RETURN, GetType(Integer))

            DBHelper.ExecuteSp(selectStmt, inputParameters, outputParameter)
            If outputParameter(P_RETURN).Value <> 0 Then
                ReplacementTaxTypeID = Guid.Empty
                Throw New StoredProcedureGeneratedException("Data Base Generated Error: ", ErrorCodes.INVALID_TAXRATE_EXCEPTION)
            Else
                ReplacementTaxTypeID = outputParameter(0).Value
            End If
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub
    'REQ 1150 Added dealer_id
    Public Function GetTaxRate(oTaxRateData As TaxRateData) As TaxRateData
        Dim selectStmt As String = Config("/SQL/GET_TAX_RATE")
        Dim inputParameters(TOTAL_INPUT_PARAM_CANCEL) As DBHelperParameter
        Dim outputParameter(TOTAL_OUTPUT_PARAM_CANCEL) As DBHelperParameter

        With oTaxRateData
            inputParameters(P_COUNTRY_ID) = New DBHelperParameter(COL_NAME_P_COUNTRY_ID, .countryID.ToByteArray)
            inputParameters(P_TAXTYPE_ID) = New DBHelperParameter(COL_NAME_P_TAXTYPE_ID, .taxtypeID.ToByteArray)
            inputParameters(P_REGION_ID) = New DBHelperParameter(COL_NAME_P_REGION_ID, .regionID.ToByteArray)
            inputParameters(P_SALES_DATE) = New DBHelperParameter(COL_NAME_P_SALES_DATE, .salesDate)
            'REQ 1150 Added dealer_id
            inputParameters(P_DEALER_ID) = New DBHelper.DBHelperParameter(COL_NAME_DEALER_ID, .dealerID.ToByteArray)

            outputParameter(P_SALES_TAX_RATE) = New DBHelperParameter(COL_NAME_P_SALES_TAX_RATE, GetType(Decimal))
            outputParameter(P_RETURN) = New DBHelperParameter(COL_NAME_P_RETURN, GetType(Integer))

        End With

        ' Call DBHelper Store Procedure
        DBHelper.ExecuteSp(selectStmt, inputParameters, outputParameter)
        If outputParameter(P_RETURN).Value <> 0 Then
            '''Dim ex As ApplicationException
            '''If outputParameter(Me.P_RETURN).Value = 100 Then
            '''    ex = New ApplicationException(ErrorCodes.INVALID_TAXRATE_BO_ERR)
            '''Else
            '''    ex = New ApplicationException(ErrorCodes.INVALID_TAXRATE_EXCEPTION)
            '''End If

            '''Throw ex
            Dim strErrorMsg As String
            If outputParameter(P_RETURN).Value = 100 Then
                strErrorMsg = ErrorCodes.INVALID_TAXRATE_BO_ERR
            Else
                strErrorMsg = ErrorCodes.INVALID_TAXRATE_EXCEPTION
            End If

            Throw New StoredProcedureGeneratedException("Data Base Generated Error: ", strErrorMsg)

        Else

            With oTaxRateData
                .taxRate = outputParameter(P_SALES_TAX_RATE).Value
            End With
        End If

        Return oTaxRateData

    End Function

    Public Function GetClaimTaxRates(oClaimTaxRatesData As ClaimTaxRatesData) As ClaimTaxRatesData

        With oClaimTaxRatesData
            Dim selectStmt As String = Config("/SQL/GET_CLAIM_TAX_RATES")


            Dim inputParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {
                        New DBHelper.DBHelperParameter(COL_NAME_P_COUNTRY_ID, .countryID.ToByteArray),
                        New DBHelper.DBHelperParameter(COL_NAME_P_REGION_ID, .regionID.ToByteArray),
                        New DBHelper.DBHelperParameter(COL_NAME_P_SALES_DATE, .salesDate),
                        New DBHelper.DBHelperParameter(COL_NAME_DEALER_ID, .dealerID.ToByteArray),
                        New DBHelper.DBHelperParameter(COL_NAME_P_CLAIM_TYPE, .claim_type)}

            Dim outputParameter() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {
                        New DBHelper.DBHelperParameter(COL_NAME_P_RETURN, GetType(Integer)),
                        New DBHelper.DBHelperParameter(COL_NAME_V_CLAIM_TAX_RATES, GetType(DataSet))}



            Dim ds As New DataSet
            Dim tbl As String = TABLE_NAME_CLAIM_TAX_RATES

            ' Call DBHelper Store Procedure
            DBHelper.FetchSp(selectStmt, inputParameters, outputParameter, ds, tbl)

            If outputParameter(P_RETURN - 1).Value <> 0 Then

                Dim strErrorMsg As String
                If outputParameter(P_RETURN - 1).Value = 100 Then
                    strErrorMsg = ErrorCodes.INVALID_TAXRATE_BO_ERR
                Else
                    strErrorMsg = ErrorCodes.INVALID_TAXRATE_EXCEPTION
                End If

                Throw New StoredProcedureGeneratedException("Data Base Generated Error: ", strErrorMsg)

            Else
                If Not ds Is Nothing AndAlso Not ds.Tables(0) Is Nothing AndAlso ds.Tables(0).Rows.Count > 0 Then

                    .taxRateClaimSuperDefault = GetDecimalValue(ds.Tables(0).Rows(0).Item(0))
                    .computeMethodCodeClaimSuperDefault = ds.Tables(0).Rows(0).Item(1).ToString
                    .applyWithholdingFlagClaimSuperDefault = ds.Tables(0).Rows(0).Item(2).ToString

                    .taxRateClaimDefault = GetDecimalValue(ds.Tables(0).Rows(0).Item(3))
                    .computeMethodCodeClaimDefault = ds.Tables(0).Rows(0).Item(4).ToString
                    .applyWithholdingFlagClaimDefault = ds.Tables(0).Rows(0).Item(5).ToString

                    .taxRateClaimDiagnostics = GetDecimalValue(ds.Tables(0).Rows(0).Item(6))
                    .computeMethodCodeClaimDiagnostics = ds.Tables(0).Rows(0).Item(7).ToString
                    .applyWithholdingFlagClaimDiagnostics = ds.Tables(0).Rows(0).Item(8).ToString

                    .taxRateClaimOther = GetDecimalValue(ds.Tables(0).Rows(0).Item(9))
                    .computeMethodCodeClaimOther = ds.Tables(0).Rows(0).Item(10).ToString
                    .applyWithholdingFlagClaimOther = ds.Tables(0).Rows(0).Item(11).ToString

                    .taxRateClaimDisposition = GetDecimalValue(ds.Tables(0).Rows(0).Item(12))
                    .computeMethodCodeClaimDisposition = ds.Tables(0).Rows(0).Item(13).ToString
                    .applyWithholdingFlagClaimDisposition = ds.Tables(0).Rows(0).Item(14).ToString

                    .taxRateClaimLabor = GetDecimalValue(ds.Tables(0).Rows(0).Item(15))
                    .computeMethodCodeClaimLabor = ds.Tables(0).Rows(0).Item(16).ToString
                    .applyWithholdingFlagClaimLabor = ds.Tables(0).Rows(0).Item(17).ToString

                    .taxRateClaimParts = GetDecimalValue(ds.Tables(0).Rows(0).Item(18))
                    .computeMethodCodeClaimParts = ds.Tables(0).Rows(0).Item(19).ToString
                    .applyWithholdingFlagClaimParts = ds.Tables(0).Rows(0).Item(20).ToString

                    .taxRateClaimShipping = GetDecimalValue(ds.Tables(0).Rows(0).Item(21))
                    .computeMethodCodeClaimShipping = ds.Tables(0).Rows(0).Item(22).ToString
                    .applyWithholdingFlagClaimShipping = ds.Tables(0).Rows(0).Item(23).ToString

                    .taxRateClaimService = GetDecimalValue(ds.Tables(0).Rows(0).Item(24))
                    .computeMethodCodeClaimService = ds.Tables(0).Rows(0).Item(25).ToString
                    .applyWithholdingFlagClaimService = ds.Tables(0).Rows(0).Item(26).ToString

                    .taxRateClaimTrip = GetDecimalValue(ds.Tables(0).Rows(0).Item(27))
                    .computeMethodCodeClaimTrip = ds.Tables(0).Rows(0).Item(28).ToString
                    .applyWithholdingFlagClaimTrip = ds.Tables(0).Rows(0).Item(29).ToString

                End If

            End If
        End With

        Return oClaimTaxRatesData

    End Function

    Private Function GetDecimalValue(obj As Object) As DecimalType
        Try

            If obj Is DBNull.Value Then
                Return New DecimalType(0D)
            Else
                Return New DecimalType(CType(obj, Decimal))
            End If

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Private Function GetStringValue(obj As Object) As String
        Try
            If obj Is DBNull.Value Then
                Return String.Empty
            Else
                Return CType(obj, String)
            End If

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function
    Public Function GetInvoiceTaxType(oTaxRateData As TaxRateData) As Boolean
        Dim selectStmt As String = Config("/SQL/GET_MANUAL_TAX")
        'Dim Parameters(Me.TOTAL_INPUT_PARAM_CANCEL) As DBHelperParameter
        Dim parameters() As DBHelper.DBHelperParameter
        Dim ds As DataSet = New DataSet
        With oTaxRateData
            parameters = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(COL_NAME_P_COUNTRY_ID, .countryID.ToByteArray), _
                                                                                               New DBHelper.DBHelperParameter(COL_NAME_P_TAXTYPE_ID, .taxtypeID.ToByteArray), _
                                                                                               New DBHelper.DBHelperParameter(COL_NAME_P_SALES_DATE, .salesDate), _
                                                                                               New DBHelper.DBHelperParameter(COL_NAME_P_DEALER_ID, .dealerID)}
        End With

        Try
            DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
            If ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Function LoadPaymentsList(companyId As Guid, claimNumber As String) As DataSet

        Dim selectStmt As String = Config("/SQL/LOAD_PAYMENTS_LIST_DYNAMIC")

        'since in our SQL we already have where clause..

        selectStmt &= Environment.NewLine & "AND inv.claim_number ='" & claimNumber & "'"

        selectStmt &= Environment.NewLine & "ORDER BY INAMT Desc) ORDER BY DTCR Desc, INAMT Desc"

        Try
            Dim parameters(TOTAL_PARAM) As DBHelper.DBHelperParameter
            parameters(COMPANY_ID) = New DBHelper.DBHelperParameter(COL_NAME_COMPANY_ID, _
                                                companyId.ToByteArray)

            Dim ds As New DataSet
            DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
            Return ds

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Function LoadByClaimAuthId(familyDS As DataSet, claimId As Guid, claimAuthId As Guid) As DataRow
        Dim selectStmt As String = Config("/SQL/LOAD_BY_CLAIM_AUTH_ID")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("claim_id", claimId.ToByteArray), _
                                                                                           New DBHelper.DBHelperParameter("claim_authorization_id", claimAuthId.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

#End Region

#Region "Overloaded Methods"
    'Public Overloads Sub Update(ByVal ds As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing, Optional ByVal changesFilter As DataRowState = Nothing)
    '    If Not ds.Tables(Me.TABLE_NAME) Is Nothing Then
    '        MyBase.Update(ds.Tables(Me.TABLE_NAME), Transaction, changesFilter)
    '    End If
    'End Sub

    'This method was added manually to accommodate BO families Save
    Public Overloads Sub UpdateFamily(familyDataset As DataSet, oCancelCertificateData As CertCancellationData, Optional ByVal isPaymentAdjustmentOrReversal As Boolean = False, Optional ByVal Transaction As IDbTransaction = Nothing)
        Dim oDisbursementDAL As New DisbursementDAL
        Dim oCommentDAL As New CommentDAL
        Dim oclaimtaxDAL As New ClaimTaxDAL

        Dim tr As IDbTransaction = Transaction
        If tr Is Nothing Then
            tr = DBHelper.GetNewTransaction
        End If
        Try
            'First Pass updates Deletions
            'to be used by maintain invoice use case
            oDisbursementDAL.Update(familyDataset, tr, DataRowState.Deleted)
            oclaimtaxDAL.Update(familyDataset, tr, DataRowState.Deleted)
            'no changes for claim DAL
            Update(familyDataset.Tables(TABLE_NAME), tr, DataRowState.Deleted)

            'comment if payment adjustment.
            If isPaymentAdjustmentOrReversal Then
                oCommentDAL.Update(familyDataset, tr, DataRowState.Deleted)
            End If

            'Second Pass updates additions and changes
            Update(familyDataset.Tables(TABLE_NAME), tr, DataRowState.Added Or DataRowState.Modified)
            oDisbursementDAL.Update(familyDataset, tr, DataRowState.Added Or DataRowState.Modified)
            oclaimtaxDAL.Update(familyDataset, tr, DataRowState.Added Or DataRowState.Modified)

            Dim claimDAL As New ClaimDAL
            claimDAL.Update(familyDataset, tr, DataRowState.Added Or DataRowState.Modified)

            Dim certItemDAL As New certItemDAL
            certItemDAL.Update(familyDataset, tr, DataRowState.Added Or DataRowState.Modified)

            'comment if payment adjustment.
            If isPaymentAdjustmentOrReversal Then
                oCommentDAL.Update(familyDataset, tr, DataRowState.Added Or DataRowState.Modified)
            End If

            'Claim Ext. status if one is added.
            If Not familyDataset.Tables(ClaimStatusDAL.TABLE_NAME) Is Nothing AndAlso familyDataset.Tables(ClaimStatusDAL.TABLE_NAME).Rows.Count > 0 Then
                Dim oClaimStatusDAL As New ClaimStatusDAL
                oClaimStatusDAL.Update(familyDataset.Tables(ClaimStatusDAL.TABLE_NAME), tr, DataRowState.Added Or DataRowState.Modified)
            End If

            'GVS Transactions if one is added.
            If Not familyDataset.Tables(TransactionLogHeaderDAL.TABLE_NAME) Is Nothing AndAlso familyDataset.Tables(TransactionLogHeaderDAL.TABLE_NAME).Rows.Count > 0 Then
                Dim oTransactionLogHeaderDAL As New TransactionLogHeaderDAL
                oTransactionLogHeaderDAL.Update(familyDataset.Tables(TransactionLogHeaderDAL.TABLE_NAME), tr, DataRowState.Added Or DataRowState.Modified)
            End If

            'Cancel Certificate if needed
            If Not oCancelCertificateData Is Nothing Then
                If oCancelCertificateData.certificatestatus = "A" Then
                    Dim dal As New CertCancellationDAL
                    dal.ExecuteCancelSP(oCancelCertificateData)
                End If
            End If
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

#Region "StoreProcedures Control"

    ' Execute Store Procedure
    Public Function ExecuteSP(docType As String, IdentificationNumber As String) As String
        Dim dal As New CertificateDAL
        Return dal.ExecuteSP(docType, IdentificationNumber)
    End Function

#End Region
End Class


