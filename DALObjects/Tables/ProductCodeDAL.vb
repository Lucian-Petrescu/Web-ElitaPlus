'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (8/3/2006)********************

Imports Assurant.ElitaPlus.DALObjects.DBHelper
Public Class ProductCodeDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_PRODUCT_CODE"
    Public Const TABLE_KEY_NAME As String = "product_code_id"

    Public Const COL_NAME_PRODUCT_CODE_ID As String = "product_code_id"
    Public Const COL_NAME_DEALER_ID As String = "dealer_id"
    Public Const COL_NAME_PRODUCT_CODE As String = "product_code"
    Public Const COL_NAME_CERTIFICATE_DURATION As String = "certificate_duration"
    Public Const COL_NAME_EXTERNAL_PRODUCT_CODE As String = "external_product_code"
    Public Const COL_NAME_BUNDLED_FLAG As String = "bundled_flag"



    Public Const COL_NAME_RISK_GROUP_ID As String = "risk_group_id"
    Public Const COL_NAME_PRICE_MATRIX_ID As String = "price_matrix_id"
    Public Const COL_NAME_PERCENT_OF_RETAIL As String = "percent_of_retail"
    Public Const COL_NAME_DESCRIPTION As String = "description"
    Public Const COL_NAME_METHOD_OF_REPAIR_ID As String = "method_of_repair_id"
    Public Const COL_NAME_TYPE_OF_EQUIPMENT_ID As String = "type_of_equipment_id"
    Public Const COL_NAME_USE_DEPRECIATION As String = "use_depreciation"
    Public Const COL_NAME_COMPANY_ID As String = "company_id"
    Public Const COL_NAME_LANGUAGE_ID As String = "language_id"
    Public Const COL_NAME_BUNDLED_ITEM_ID As String = "bundled_item_id"
    Public Const COL_NAME_WARRANTY_SALES_DATE As String = "warranty_sales_date"
    Public Const COL_NAME_METHOD_OF_REPAIR_BY_PRICE_ID As String = "method_of_repair_by_price_id"
    Public Const COL_NAME_BILLING_FREQUENCY_ID As String = "billing_frequency_id"
    Public Const COL_NAME_NUMBER_OF_INSTALLMENTS As String = "number_of_installments"
    Public Const COL_NAME_NUM_OF_CLAIMS As String = "num_of_claims"
    '   Public Const COL_NAME_UPFRONT_COMM_ID As String= "upfront_comm_id"
    Public Const COL_NAME_SPLIT_WARRANTY_ID As String = "split_warranty_id"
    Public Const COL_NAME_CLAIM_WAITING_PERIOD As String = "claim_waiting_period"
    Public Const COL_NAME_FULL_REFUND_DAYS As String = "full_refund_days"
    Public Const COL_NAME_UPG_FINANCE_BAL_COMP_METH_ID As String = "upg_finance_bal_comp_meth_id"
    Public Const COL_NAME_UPGRADE_PROGRAM_ID As String = "upgrade_program_id"
    Public Const COL_NAME_COMMENTS As String = "comments"
    Public Const COL_NAME_SPECIAL_SERVICE As String = "special_service"
    Public Const COL_NAME_IGNORE_WAITING_PERIOD_WSD_PSD As String = "ignore_waiting_period_wsd_psd"
    Public Const COL_NAME_PROD_LIABILITY_LIMIT_BASE_ID As String = "prod_liability_limit_base_id"
    Public Const COL_NAME_PROD_LIABILITY_LIMIT_POLICY_ID As String = "prod_liability_limit_policy_id"
    Public Const COL_NAME_PROD_LIABILITY_LIMIT As String = "prod_liability_limit"
    Public Const COL_NAME_PROD_LIABILITY_LIMIT_PERCENT As String = "prod_liability_limit_percent"
    Public Const COL_NAME_CLAIM_AUTO_APPROVE_PSP As String = "claim_auto_approve_psp"

    Public Const COL_NAME_BENEFIT_ELIGIBLE_ACTION_XCD As String = "benefit_eligible_action_xcd"
    Public Const COL_NAME_BENEFIT_ELIGIBLE_XCD As String = "benefit_eligible_xcd"
    Public Const COL_NAME_CALC_COVG_END_DATE_BASED_ON_XCD As String = "calc_cvg_end_date_based_on_xcd"

    Public Const COL_NAME_NUM_OF_REPAIR_CLAIMS As String = "num_of_repair_claims"
    Public Const COL_NAME_NUM_OF_REPLACEMENT_CLAIMS As String = "num_of_replacement_claims"
    Public Const COL_IS_REINSURED_ID As String = "is_reinsured_id"

    'pavan REQ-5733
    Public Const COL_NAME_ANALYSIS_CODE_1 As String = "analysis_code_1"
    Public Const COL_NAME_ANALYSIS_CODE_2 As String = "analysis_code_2"
    Public Const COL_NAME_ANALYSIS_CODE_3 As String = "analysis_code_3"
    Public Const COL_NAME_ANALYSIS_CODE_4 As String = "analysis_code_4"
    Public Const COL_NAME_ANALYSIS_CODE_5 As String = "analysis_code_5"
    Public Const COL_NAME_ANALYSIS_CODE_6 As String = "analysis_code_6"
    Public Const COL_NAME_ANALYSIS_CODE_7 As String = "analysis_code_7"
    Public Const COL_NAME_ANALYSIS_CODE_8 As String = "analysis_code_8"
    Public Const COL_NAME_ANALYSIS_CODE_9 As String = "analysis_code_9"
    Public Const COL_NAME_ANALYSIS_CODE_10 As String = "analysis_code_10"

    Public Const COL_NAME_UPG_FINANCE_INFO_REQUIRE_ID As String = "upg_finance_info_require_id"
    Public Const COL_NAME_UPGRADE_TERM_UOM_ID As String = "upgrade_term_uom_id"
    Public Const COL_NAME_UPGRADE_TERM_FROM As String = "upgrade_term_from"
    Public Const COL_NAME_UPGRADE_TERM_TO As String = "upgrade_term_to"
    Public Const COL_NAME_UPGRADE_FIXED_TERM As String = "upgrade_fixed_term"
    Public Const COL_NAME_INSTALLMENT_REPRICABLE_ID As String = "Installment_repricable_id"
    Public Const COL_NAME_INUSEFLAG As String = "inuseflag"
    Public Const COL_NAME_BILLING_CRITERIA_ID As String = "billing_criteria_id"
    Public Const COL_NAME_CNL_DEPENDENCY_ID As String = "cnl_dependency_id"
    Public Const COL_NAME_POST_PRE_PAID_ID As String = "post_pre_paid_id"
    Public Const COL_NAME_CNL_LUMPSUM_BILLING_ID As String = "cnl_lumpsum_billing_id"
    Public Const COL_NAME_IS_PARENT_PRODUCT = "is_parent_product" 'REQ-5980
    Public Const COL_NAME_IS_PRODUCT_EQUIPMENT_VALIDATION = "product_equipment_validation"
    Public Const COL_NAME_UPGRADE_FEE = "upgrade_fee"
    Public Const COL_NAME_ALLOW_REGISTERED_ITEMS = "allow_registered_items"
    Public Const COL_NAME_MAX_AGE_OF_REGISTERED_ITEM = "max_age_of_registered_item"
    Public Const COL_NAME_MAX_REGISTRATIONS_ALLOWED = "max_registrations_allowed"
    Public Const COL_NAME_LIST_FOR_DEVICE_GROUPS = "list_for_device_group"
    Public Const COL_NAME_LIST_FOR_DEVICE_GROUP_CODE = "list_for_device_group_code"

    'REQ-6289
    Public Const COL_NAME_PROD_LIMIT_APPLICABLE_TO_XCD = "prod_limit_applicable_to_xcd"
    'REQ-6289-END
    'REQ-6002
    Public Const COL_NAME_UPDATE_REPLACE_REG_ITEMS_ID = "UPDATE_REPLACE_REG_ITEMS_ID"
    Public Const COL_NAME_REGISTERED_ITEMS_LIMIT = "REGISTERED_ITEMS_LIMIT"
    Public Const COL_NAME_CANCELLATION_WITHIN_TERM_XCD = "Cancellation_Within_Term_xcd"
    Public Const COL_NAME_EXPIRATION_NOTIFICATION_DAYS = "Expiration_Notification_days"
    'REQ- 6156
    Public Const ColNameFulfillmentReimThreshold = "fulfillment_reim_threshold"

    Public Const TOTAL_PARAM = 1 '2
    Public Const COMPANY_CODE = 0
    Public Const TODAY_DATE = 1
    Public Const DEALER_ID = 0

    Public Const COL_NAME_CLAIM_LIMIT_PER_REG_ITEM = "CLAIM_LIMIT_PER_REG_ITEM"

    Public Const ColNameClaimProfileCode = "claim_profile_code"
    Public Const COL_NAME_PER_INCIDENT_LIABILITY_LIMIT_CAP As String = "liability_limit_per_incident"
    Public Const COL_NAME_PRICE_MATRIX_USES_WP_XCD = "price_matrix_uses_wp_xcd"
    Public Const COL_NAME_EXPECTED_PREMIUM_IS_WP_XCD = "expected_premium_is_wp_xcd"

#End Region

#Region "Constructors"
    Public Sub New()
        MyBase.New()
    End Sub

#End Region

#Region "Load Methods"

    Public Sub LoadSchema(ds As DataSet)
        Load(ds, Guid.Empty)
    End Sub

    Public Sub LoadByDealerProduct(familyDS As DataSet, dealerId As Guid, productCode As String)
        Dim selectStmt As String = Config("/SQL/LOAD_BY_DEALER_PRODUCT")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
                                                         {
                                                             New DBHelper.DBHelperParameter("dealer_id", dealerId.ToByteArray),
                                                             New DBHelper.DBHelperParameter("productCode", productCode)
                                                         }
        Try
            DBHelper.Fetch(familyDS, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Sub Load(familyDS As DataSet, id As Guid)
        Dim selectStmt As String = Config("/SQL/LOAD")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("product_code_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub
    Private Function IsThereALikeClause(descriptionMask As String) As Boolean
        Dim bIsLikeClause As Boolean

        bIsLikeClause = IsLikeClause(descriptionMask)
        Return bIsLikeClause
    End Function

    Public Function LoadList(compIds As ArrayList, dealerId As Guid,
                             RiskGroupId As Guid, productCodeMask As String, LanguageId As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_LIST")
        Dim parameters() As DBHelper.DBHelperParameter
        Dim inClausecondition As String = ""
        Dim whereClauseConditions As String = ""
        Dim bIsLikeClause As Boolean = False

        inClausecondition &= "And edealer." & MiscUtil.BuildListForSql(COL_NAME_COMPANY_ID, compIds, False)

        productCodeMask = productCodeMask.Trim()
        If (Not productCodeMask.Equals(String.Empty) OrElse Not productCodeMask = "") AndAlso (FormatSearchMask(productCodeMask)) Then
            whereClauseConditions &= Environment.NewLine & "AND " & "Upper(pc.PRODUCT_CODE)" & productCodeMask.ToUpper
        End If

        If Not dealerId.Equals(Guid.Empty) Then
            whereClauseConditions &= Environment.NewLine & "AND " & "edealer.DEALER_ID = " & MiscUtil.GetDbStringFromGuid(dealerId)
        End If

        If Not RiskGroupId.Equals(Guid.Empty) Then
            whereClauseConditions &= Environment.NewLine & "AND " & "RISK_GROUP_ID = " & MiscUtil.GetDbStringFromGuid(RiskGroupId)
        End If

        parameters = New DBHelper.DBHelperParameter() _
                                    {New DBHelper.DBHelperParameter(COL_NAME_LANGUAGE_ID, LanguageId.ToByteArray)}


        If Not inClausecondition = "" Then
            selectStmt = selectStmt.Replace(DYNAMIC_IN_CLAUSE_PLACE_HOLDER, inClausecondition)
        Else
            selectStmt = selectStmt.Replace(DYNAMIC_IN_CLAUSE_PLACE_HOLDER, "")
        End If

        If Not whereClauseConditions = "" Then
            selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
        Else
            selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, "")
        End If

        Dim ds As New DataSet
        Try
            DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Function LoadProductCodeIDs(dealerId As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_PRODUCT_CODE_IDS")
        Dim parameters() As DBHelper.DBHelperParameter

        parameters = New DBHelper.DBHelperParameter() _
                                    {New DBHelper.DBHelperParameter(COL_NAME_DEALER_ID, dealerId.ToByteArray)}

        Dim ds As New DataSet
        Try
            DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Function LoadListByDealer(dealerId As Guid, languageId As Guid, RiskGroupId As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_LIST_BY_DEALER")
        Dim parameters() As DBHelper.DBHelperParameter
        Dim whereClauseConditions As String = ""

        If Not RiskGroupId.Equals(Guid.Empty) Then
            whereClauseConditions &= Environment.NewLine & "AND " & "RISK_GROUP_ID = " & MiscUtil.GetDbStringFromGuid(RiskGroupId)
        End If
        selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)

        parameters = New DBHelper.DBHelperParameter() _
                                    {New DBHelper.DBHelperParameter(COL_NAME_DEALER_ID, dealerId.ToByteArray),
                                    New DBHelper.DBHelperParameter(COL_NAME_LANGUAGE_ID, languageId.ToByteArray)}

        Dim ds As New DataSet
        Try
            DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function LoadListByDealer(dealerId As Guid, languageId As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_LIST_BY_DEALER_FILTERING")
        Dim parameters() As DBHelper.DBHelperParameter
        Dim whereClauseConditions As String = ""


        parameters = New DBHelper.DBHelperParameter() _
                                    {New DBHelper.DBHelperParameter(COL_NAME_DEALER_ID, dealerId.ToByteArray),
                                    New DBHelper.DBHelperParameter(COL_NAME_LANGUAGE_ID, languageId.ToByteArray)}

        Dim ds As New DataSet
        Try
            DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function LoadListByDealerForWS(dealerId As Guid, WarrSalesDate As Date,
            sort_by As Integer, asc_desc_order As String, productClassCode As String) As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_LIST_BY_DEALER_FOR_WS")
        Dim parameters() As DBHelper.DBHelperParameter
        Dim whereClauseConditions As String = ""
        Dim OrderByClause As String = ""

        Dim strWarrSalesDate As String = WarrSalesDate.ToString("MM/dd/yyyy")

        If productClassCode IsNot Nothing Then
            whereClauseConditions = "and SUBSTRC(pc.PRODUCT_CODE, 4, 1) = '" & productClassCode & "'"
        End If
        selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)

        Select Case sort_by
            Case 1
                OrderByClause = "ORDER BY UPPER(" & COL_NAME_DESCRIPTION & ") " & asc_desc_order
                Exit Select
            Case 2
                OrderByClause = "ORDER BY " & COL_NAME_PRODUCT_CODE & " " & asc_desc_order
                Exit Select
            Case 3
                OrderByClause = "ORDER BY " & COL_NAME_CERTIFICATE_DURATION & " " & asc_desc_order
                Exit Select
            Case 4
                OrderByClause = "ORDER BY " & COL_NAME_BUNDLED_FLAG & " " & asc_desc_order
                Exit Select
            Case 5
                OrderByClause = "ORDER BY " & COL_NAME_PERCENT_OF_RETAIL & " " & asc_desc_order
                Exit Select
            Case 6
                OrderByClause = "ORDER BY " & COL_NAME_NUMBER_OF_INSTALLMENTS & " " & asc_desc_order
                Exit Select
        End Select

        selectStmt = selectStmt.Replace(DYNAMIC_ORDER_BY_CLAUSE_PLACE_HOLDER, OrderByClause)

        parameters = New DBHelper.DBHelperParameter() _
                    {New DBHelper.DBHelperParameter(COL_NAME_DEALER_ID, dealerId.ToByteArray),
                    New DBHelper.DBHelperParameter(COL_NAME_WARRANTY_SALES_DATE, WarrSalesDate, GetType(Date))
                   }

        Dim ds As New DataSet
        Try
            DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function LoadListByDealerForWSWithConversion(dealerId As Guid, WarrSalesDate As Date,
                    sort_by As Integer, asc_desc_order As String, productClassCode As String) As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_LIST_BY_DEALER_FOR_WS_WITH_CONVERSION")
        Dim parameters() As DBHelper.DBHelperParameter
        Dim whereClauseConditions As String = ""
        Dim OrderByClause As String = ""

        Dim strWarrSalesDate As String = WarrSalesDate.ToString("MM/dd/yyyy")
        If productClassCode IsNot Nothing Then
            whereClauseConditions = "and SUBSTRC(pc.PRODUCT_CODE, 4, 1) = '" & productClassCode & "'"
        End If
        selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)

        Select Case sort_by
            Case 1
                OrderByClause = "ORDER BY UPPER(" & COL_NAME_DESCRIPTION & ") " & asc_desc_order
                Exit Select
            Case 2
                OrderByClause = "ORDER BY " & COL_NAME_PRODUCT_CODE & " " & asc_desc_order
                Exit Select
            Case 3
                OrderByClause = "ORDER BY " & COL_NAME_CERTIFICATE_DURATION & " " & asc_desc_order
                Exit Select
            Case 4
                OrderByClause = "ORDER BY " & COL_NAME_BUNDLED_FLAG & " " & asc_desc_order
                Exit Select
            Case 5
                OrderByClause = "ORDER BY " & COL_NAME_PERCENT_OF_RETAIL & " " & asc_desc_order
                Exit Select
            Case 6
                OrderByClause = "ORDER BY " & COL_NAME_NUMBER_OF_INSTALLMENTS & " " & asc_desc_order
                Exit Select
            Case 7
                OrderByClause = "ORDER BY " & COL_NAME_EXTERNAL_PRODUCT_CODE & " " & asc_desc_order
                Exit Select
        End Select

        selectStmt = selectStmt.Replace(DYNAMIC_ORDER_BY_CLAUSE_PLACE_HOLDER, OrderByClause)

        parameters = New DBHelper.DBHelperParameter() _
                {New DBHelper.DBHelperParameter(COL_NAME_DEALER_ID, dealerId.ToByteArray),
                New DBHelper.DBHelperParameter(COL_NAME_WARRANTY_SALES_DATE, WarrSalesDate, GetType(Date))
               }

        Dim ds As New DataSet
        Try
            DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function LoadDealerProductsInfo(ByRef ds As DataSet, dealerId As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_DEALER_PRODUCTS_INFO_FOR_WS")
        Dim parameters() As DBHelper.DBHelperParameter
        Dim whereClauseConditions As String = ""
        Dim OrderByClause As String = ""

        parameters = New DBHelper.DBHelperParameter() _
                {New DBHelper.DBHelperParameter(COL_NAME_DEALER_ID, dealerId.ToByteArray)}

        If ds Is Nothing Then ds = New DataSet
        Try
            DBHelper.Fetch(ds, selectStmt, "PRODUCT_CODE_INFO", parameters)

            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function LoadDealerProductsInfoWithConversion(ByRef ds As DataSet, dealerId As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_DEALER_PRODUCTS_INFO_FOR_WS_WITH_CONVERSION")
        Dim parameters() As DBHelper.DBHelperParameter
        Dim whereClauseConditions As String = ""
        Dim OrderByClause As String = ""


        parameters = New DBHelper.DBHelperParameter() _
                {New DBHelper.DBHelperParameter(COL_NAME_DEALER_ID, dealerId.ToByteArray)}

        If ds Is Nothing Then ds = New DataSet
        Try
            DBHelper.Fetch(ds, selectStmt, "PRODUCT_CODES", parameters)


            Return ds

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function GetProductCodeId(dealerId As Guid, product_code As String) As DataSet
        Dim selectStmt As String = Config("/SQL/GET_PRODUCT_CODE_ID")
        Dim parameters() As DBHelper.DBHelperParameter

        parameters = New DBHelper.DBHelperParameter() _
                                    {New DBHelper.DBHelperParameter(COL_NAME_DEALER_ID, dealerId.ToByteArray),
                                     New DBHelper.DBHelperParameter(COL_NAME_PRODUCT_CODE, product_code)}
        Dim ds As New DataSet
        Try
            DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Function MethodOfRepairByPriceRecords(ProductCodeId As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/METHOD_OF_REPAIR_BY_PRICE_RECORDS")
        Dim parameters() As DBHelper.DBHelperParameter

        parameters = New DBHelper.DBHelperParameter() _
                                    {New DBHelper.DBHelperParameter(COL_NAME_PRODUCT_CODE_ID, ProductCodeId.ToByteArray)}


        Dim ds As New DataSet
        Try
            DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Sub UpdateCoverageLiability(ProductCodeId As Guid)
        Dim updateStmt As String = Config("/SQL/UPDATE_COVERAGE_LIABILITY")
        Dim inputParameters(0) As DBHelperParameter
        Dim outputParameter(0) As DBHelperParameter

        inputParameters(0) = New DBHelperParameter("pi_product_code_id", ProductCodeId.ToByteArray)
        outputParameter(0) = New DBHelperParameter("po_return", GetType(Integer))
        Try
            ' Call DBHelper Store Procedure
            DBHelper.ExecuteSp(updateStmt, inputParameters, outputParameter)
            If outputParameter(0).Value <> 0 Then
                Dim e As New ApplicationException("Return Value = " & outputParameter(0).Value)
                Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, e)
            End If
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Sub
    Public Function UpdateCoverageReinsurance(ProductCodeId As Guid, Optional ByVal Mode As String = Nothing) As Integer
        Dim updateStmt As String = Config("/SQL/GET_COVERAGE_REINSURANCE")
        Dim inputParameters(1) As DBHelperParameter
        Dim outputParameter(0) As DBHelperParameter

        inputParameters(0) = New DBHelperParameter("pi_product_code_id", ProductCodeId.ToByteArray)
        inputParameters(1) = New DBHelperParameter("pi_mode", Mode)
        outputParameter(0) = New DBHelperParameter("po_return", GetType(Integer))
        Try
            ' Call DBHelper Store Procedure
            DBHelper.ExecuteSp(updateStmt, inputParameters, outputParameter)
            If outputParameter(0).Value <> 0 Then
                Dim e As New ApplicationException("Return Value = " & outputParameter(0).Value)
                Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, e)
            Else
                Return 0
            End If
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
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


    Public Overloads Sub UpdateFamily(familyDataset As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing)
        Dim PrdRegionDal As New ProductRegionDAL
        Dim oProdPolicyDAL As New ProductPolicyDAL
        Dim oParentProduct As New ProductCodeParentDAL
        Dim oAttributeValueDAL As New AttributeValueDAL
        Dim oProdRewardsDAL As New ProductRewardsDAL
        Dim oProdEquipDAL As New ProductEquipmentDAL
        Dim oProdDepSchDal As New DepreciationScdRelationDal

        Dim tr As IDbTransaction = Transaction
        If tr Is Nothing Then
            tr = DBHelper.GetNewTransaction
        End If
        Try
            'First Pass updates Deletions           
            PrdRegionDal.Update(familyDataset, tr, DataRowState.Deleted)

            ' oParentProduct.Update(familyDataset, tr, DataRowState.Deleted)
            ' MyBase.Update(familyDataset.Tables(Me.TABLE_NAME).GetChanges(DataRowState.Deleted), tr, DataRowState.Deleted)

            oProdPolicyDAL.Update(familyDataset, tr, DataRowState.Deleted)
            'MyBase.Update(familyDataset.Tables(Me.TABLE_NAME).GetChanges(DataRowState.Deleted), tr, DataRowState.Deleted)

            oProdRewardsDAL.Update(familyDataset, tr, DataRowState.Deleted)
            oProdEquipDAL.Update(familyDataset, tr, DataRowState.Deleted)
            oProdDepSchDal.Update(familyDataset, tr, DataRowState.Deleted)
            MyBase.Update(familyDataset.Tables(TABLE_NAME).GetChanges(DataRowState.Deleted), tr, DataRowState.Deleted)


            'Second Pass updates additions and changes   
            oAttributeValueDAL.Update(familyDataset.GetChanges(), tr)
            Update(familyDataset.Tables(TABLE_NAME).GetChanges(DataRowState.Added Or DataRowState.Modified), tr, DataRowState.Added Or DataRowState.Modified)

            PrdRegionDal.Update(familyDataset.GetChanges(DataRowState.Added Or DataRowState.Modified), tr, DataRowState.Added Or DataRowState.Modified)

            'If ProdPolicyDataHasChanged Then            
            oProdPolicyDAL.Update(familyDataset.GetChanges(DataRowState.Added Or DataRowState.Modified), tr, DataRowState.Added Or DataRowState.Modified)
            oProdRewardsDAL.Update(familyDataset.GetChanges(DataRowState.Added Or DataRowState.Modified), tr, DataRowState.Added Or DataRowState.Modified)
            oProdEquipDAL.Update(familyDataset.GetChanges(DataRowState.Added Or DataRowState.Modified), tr, DataRowState.Added Or DataRowState.Modified)
            oProdDepSchDal.Update(familyDataset.GetChanges(DataRowState.Added Or DataRowState.Modified), tr, DataRowState.Added Or DataRowState.Modified)
            'End If
            ' oParentProduct.Update(familyDataset.GetChanges(DataRowState.Added Or DataRowState.Modified), tr, DataRowState.Added Or DataRowState.Modified)
            'At the end delete the Address
            If familyDataset.Tables(TransactionLogHeaderDAL.TABLE_NAME) IsNot Nothing AndAlso familyDataset.Tables(TransactionLogHeaderDAL.TABLE_NAME).Rows.Count > 0 Then
                Dim oTransactionLogHeaderDAL As New TransactionLogHeaderDAL
                oTransactionLogHeaderDAL.Update(familyDataset.Tables(TransactionLogHeaderDAL.TABLE_NAME), tr, DataRowState.Added Or DataRowState.Modified)
            End If

            If Transaction Is Nothing Then
                'We are the creator of the transaction we should commit it  and close the connection
                DBHelper.Commit(tr)
                familyDataset.AcceptChanges()
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





