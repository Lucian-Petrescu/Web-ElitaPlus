'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (4/12/2006)********************


Public Class CoverageDAL
    Inherits DALBase

#Region "Constants"

    'Public Const COL_NAME_RISK_TYPE_ID As String = "risk_type_id"
    'Public Const COL_NAME_MAX_REPLACEMENT_COST As String = "max_replacement_cost"
    'Public Const COL_NAME_DEALER_ID As String = "dealer_id"
    'Public Const COL_NAME_DEALER_NAME As String = "dealer_name"

#End Region

#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_COVERAGE"
    Public Const TABLE_KEY_NAME As String = "coverage_id"
    Public Const COL_NAME_COVERAGE_ID As String = "coverage_id"
    Public Const COL_NAME_COVERAGE_TYPE As String = "coverage_type"
    Public Const COL_NAME_COVERAGE_TYPE_ID As String = "coverage_type_id"
    Public Const COL_NAME_ITEM_ID As String = "item_id"
    Public Const COL_NAME_METHOD_OF_REPAIR_ID As String = "method_of_repair_id"
    Public Const COL_NAME_FULFILLMENT_PROFILE_CODE As String = "fulfillment_profile_code"
    Public Const COL_NAME_FULFILLMENT_PROVIDER_XCD As String = "fulfillment_provider_xcd"
    Public Const COL_NAME_PRODUCT_ITEM_ID As String = "product_item_id"
    Public Const COL_NAME_CERTIFICATE_DURATION As String = "certificate_duration"
    Public Const COL_NAME_COVERAGE_DURATION As String = "coverage_duration"
    Public Const COL_NAME_EFFECTIVE As String = "effective"
    Public Const COL_NAME_EXPIRATION As String = "expiration"
    Public Const COL_NAME_OFFSET_METHOD_ID As String = "offset_method_id"
    Public Const COL_NAME_OFFSET_METHOD As String = "offset_method"
    Public Const COL_NAME_OFFSET_TO_START As String = "offset_to_start"
    Public Const COL_NAME_OFFSET_TO_START_DAYS As String = "offset_to_start_days"
    Public Const COL_NAME_OPTIONAL_ID As String = "optional_id"
    Public Const COL_NAME_LIABILITY_LIMIT As String = "liability_limit"
    Public Const COL_NAME_LIABILITY_LIMIT_PERCENT As String = "liability_limit_percent"
    Public Const COL_NAME_DEDUCTIBLE As String = "deductible"
    Public Const COL_NAME_ITEM_NUMBER As String = "item_number"
    Public Const COL_NAME_DEALER As String = "dealer"
    Public Const COL_NAME_DEALER_ID As String = "dealer_id"
    Public Const COL_NAME_WARRANTY_SALES_DATE As String = "warranty_sales_date"
    Public Const COL_NAME_PRODUCT_CODE As String = "product_code"
    Public Const COL_NAME_PRODUCT_CODE_ID As String = "product_code_id"
    Public Const COL_NAME_RISK_TYPE As String = "risk_type"
    Public Const COL_NAME_RISK_TYPE_ID As String = "risk_type_id"
    Public Const COL_NAME_COMPANY_ID As String = "company_id"
    Public Const COL_NAME_LANGUAGE_ID As String = "language_id"
    Public Const COL_NAME_EARNING_CODE_ID As String = "earning_code_id"
    Public Const COL_NAME_DEDUCTIBLE_PERCENT As String = "deductible_percent"
    Public Const COL_NAME_REPAIR_DISCOUNT_PCT As String = "repair_discount_pct"
    Public Const COL_NAME_REPLACEMENT_DISCOUNT_PCT As String = "replacement_discount_pct"
    Public Const COL_NAME_IS_CLAIM_ALLOWED_ID As String = "is_claim_allowed_id"
    Public Const COL_NAME_USE_COVERAGE_START_DATE_ID As String = "use_coverage_start_date_id"
    Public Const COL_NAME_DEDUCTIBLE_BASED_ON_ID As String = "deductible_based_on_id"
    Public Const COL_NAME_AGENT_CODE As String = "agent_code"
    Public Const COL_NAME_NUMBER_OF_CERTIFICATES As String = "Associated_Cert_count"
    Public Const COL_NAME_MARKUP_DISTRIBUTION_PERCENT As String = "markup_distribution_percent"
    Public Const COL_NAME_COVERAGE_LIABILITY_LIMIT As String = "cov_liability_limit"
    Public Const COL_NAME_COVERAGE_LIABILITY_LIMIT_PERCENT As String = "cov_liability_limit_percent"
    Public Const COL_NAME_PROD_LIABILITY_LIMIT_BASE_ID As String = "prod_liability_limit_base_id"
    Public Const COL_NAME_RECOVER_DEVICE_ID As String = "recover_device_id"
    Public Const PROD_TABLE_NAME As String = "ELP_PRODUCT_CODE"
    Public Const COL_NAME_DEDUCTIBLE_EXPRESSION_ID As String = "deductible_expression_id"
    Public Const COL_NAME_IS_REINSURED_ID As String = "is_reinsured_id"
    Public Const COL_NAME_INUSEFLAG As String = "inuseflag"
    Public Const COL_NAME_COVERAGE_CLAIM_LIMIT As String = "cov_claim_limit"
    Public Const COL_NAME_PER_INCIDENT_LIABILITY_LIMIT_CAP As String = "liability_limit_per_incident"
    Public Const COL_NAME_TAX_TYPE_XCD As String = "tax_type_XCD"
    Public Const COL_NAME_DEALER_MARKUP As String = "dealer_markup_id"
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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("coverage_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, Me.TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadList(ByVal familyDs As DataSet, ByVal coverageId As Guid)
        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST")
        Dim covergeParam As New DBHelper.DBHelperParameter(Me.COL_NAME_COVERAGE_ID, coverageId)
        DBHelper.Fetch(familyDs, selectStmt, Me.TABLE_NAME, New DBHelper.DBHelperParameter() {covergeParam})
    End Function

    Public Function LoadList(ByVal compIds As ArrayList, ByVal dealerId As Guid, ByVal productCodeId As Guid,
                             ByVal itemId As Guid, ByVal coverageTypeId As Guid,
                             ByVal certificateDuration As Assurant.Common.Types.LongType,
                             ByVal coverageDuration As Assurant.Common.Types.LongType, ByVal LanguageId As Guid) As DataSet

        Try
            Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST")
            Dim parameters() As DBHelper.DBHelperParameter
            Dim inClauseCondition As String
            Dim whereClauseConditions As String = ""
            Dim ds As New DataSet

            inClauseCondition &= " AND edealer." & MiscUtil.BuildListForSql(Me.COL_NAME_COMPANY_ID, compIds, True)

            If Not dealerId.Equals(Guid.Empty) Then
                whereClauseConditions &= Environment.NewLine & "AND " & "EDEALER.DEALER_ID = " & MiscUtil.GetDbStringFromGuid(dealerId)
            End If

            If Not productCodeId.Equals(Guid.Empty) Then
                whereClauseConditions &= Environment.NewLine & "AND " & "PC.PRODUCT_CODE_ID = " & MiscUtil.GetDbStringFromGuid(productCodeId)
            End If

            If Not itemId.Equals(Guid.Empty) Then
                whereClauseConditions &= Environment.NewLine & "AND " & "ITEM.ITEM_ID = " & MiscUtil.GetDbStringFromGuid(itemId)
            End If

            If Not coverageTypeId.Equals(Guid.Empty) Then
                whereClauseConditions &= Environment.NewLine & "AND " & "COVERAGE_TYPE_ID = " & MiscUtil.GetDbStringFromGuid(coverageTypeId)
            End If

            If Not certificateDuration Is Nothing Then
                whereClauseConditions &= Environment.NewLine & "AND " & "CERTIFICATE_DURATION = " & certificateDuration.ToString
            End If

            If Not coverageDuration Is Nothing Then
                whereClauseConditions &= Environment.NewLine & "AND " & "COVERAGE_DURATION = " & coverageDuration.ToString
            End If

            If Not inClauseCondition = "" Then
                selectStmt = selectStmt.Replace(Me.DYNAMIC_IN_CLAUSE_PLACE_HOLDER, inClauseCondition)
            Else
                selectStmt = selectStmt.Replace(Me.DYNAMIC_IN_CLAUSE_PLACE_HOLDER, "")
            End If

            If Not whereClauseConditions = "" Then
                selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
            Else
                selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, "")

            End If
            selectStmt = selectStmt.Replace(Me.DYNAMIC_ORDER_BY_CLAUSE_PLACE_HOLDER,
                                    Environment.NewLine & "ORDER BY " & Environment.NewLine &
                                    "UPPER(" & Me.COL_NAME_DEALER & "), UPPER(" &
                                    Me.COL_NAME_PRODUCT_CODE & "), UPPER(" & Me.COL_NAME_RISK_TYPE &
                                    "), " & COL_NAME_ITEM_NUMBER & ", UPPER(" & Me.COL_NAME_COVERAGE_TYPE &
                                    "), " & COL_NAME_CERTIFICATE_DURATION & "," & COL_NAME_COVERAGE_DURATION &
                                    "," & COL_NAME_EFFECTIVE & " DESC, " & COL_NAME_EXPIRATION)
            ' ORDER BY
            ' UPPER(DEALER), UPPER(PRODUCT_CODE), UPPER(RISK_TYPE), ITEM_NUMBER, UPPER(COVERAGE_TYPE),
            ' CERTIFICATE_DURATION, COVERAGE_DURATION, EFFECTIVE desc, EXPIRATION

            parameters = New DBHelper.DBHelperParameter() _
                                        {New DBHelper.DBHelperParameter(COL_NAME_LANGUAGE_ID, LanguageId.ToByteArray)}

            DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function
    Public Function MaxExpiration(ByVal itemId As Guid, ByVal coverageTypeId As Guid, _
                                    ByVal certificateDuration As Assurant.Common.Types.LongType, _
                                    ByVal coverageDuration As Assurant.Common.Types.LongType)
        Try
            Dim selectStmt As String = Me.Config("/SQL/MAX_EXPIRATION")
            Dim parameters() As DBHelper.DBHelperParameter
            Dim ds As New DataSet
            parameters = New DBHelper.DBHelperParameter() _
                                        {New DBHelper.DBHelperParameter(COL_NAME_ITEM_ID, itemId.ToByteArray), _
                                         New DBHelper.DBHelperParameter(COL_NAME_COVERAGE_TYPE_ID, coverageTypeId.ToByteArray), _
                                         New DBHelper.DBHelperParameter(COL_NAME_CERTIFICATE_DURATION, certificateDuration.Value), _
                                         New DBHelper.DBHelperParameter(COL_NAME_COVERAGE_DURATION, coverageDuration.Value)}

            DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function GetCoverageList(ByVal dealerId As Guid, ByVal productCodeId As Guid, ByVal riskTypeId As Guid, _
                                    ByVal itemId As Guid, ByVal coverageTypeId As Guid, _
                                    ByVal certificateDuration As Assurant.Common.Types.LongType, _
                                    ByVal coverageDuration As Assurant.Common.Types.LongType, _
                                    ByVal effectiveDate As Assurant.Common.Types.DateType, _
                                    ByVal expirationDate As Assurant.Common.Types.DateType, _
                                    ByVal excludeCoverageID As Guid)
        Try
            Dim selectStmt As String = Me.Config("/SQL/COVERAGE_LIST")
            Dim parameters() As DBHelper.DBHelperParameter
            Dim ds As New DataSet
            parameters = New DBHelper.DBHelperParameter() _
                                        {New DBHelper.DBHelperParameter(COL_NAME_EFFECTIVE, effectiveDate.Value), _
                                         New DBHelper.DBHelperParameter(COL_NAME_EXPIRATION, expirationDate.Value), _
                                         New DBHelper.DBHelperParameter(COL_NAME_DEALER_ID, dealerId.ToByteArray), _
                                         New DBHelper.DBHelperParameter(COL_NAME_PRODUCT_CODE_ID, productCodeId.ToByteArray), _
                                         New DBHelper.DBHelperParameter(COL_NAME_RISK_TYPE_ID, riskTypeId.ToByteArray), _
                                         New DBHelper.DBHelperParameter(COL_NAME_ITEM_ID, itemId.ToByteArray), _
                                         New DBHelper.DBHelperParameter(COL_NAME_COVERAGE_TYPE_ID, coverageTypeId.ToByteArray), _
                                         New DBHelper.DBHelperParameter(COL_NAME_CERTIFICATE_DURATION, certificateDuration.Value), _
                                         New DBHelper.DBHelperParameter(COL_NAME_COVERAGE_DURATION, coverageDuration.Value), _
                                         New DBHelper.DBHelperParameter(COL_NAME_CERTIFICATE_DURATION, certificateDuration.Value), _
                                         New DBHelper.DBHelperParameter(COL_NAME_COVERAGE_ID, excludeCoverageID)}

            DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function GetCurrencyOfCoverage(ByVal coverageId As Guid)
        Try
            Dim selectStmt As String = Me.Config("/SQL/CURRENCY_OF_COVERAGE")
            Dim ds As New DataSet
            Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("coverage_id", coverageId.ToByteArray)}

            DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function GetProductLiabilityLimitBaseid(ByVal productId As Guid)
        Try
            Dim selectStmt As String = Me.Config("/SQL/PRODUCT_LIABILITY_LIMIT_BASE")
            Dim ds As New DataSet
            Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("product_code_id", productId.ToByteArray)}

            DBHelper.Fetch(ds, selectStmt, Me.PROD_TABLE_NAME, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function GetCoverageDeductible(ByVal dealerId As Guid, ByVal effectiveDate As String, ByVal languageId As Guid)
        Try
            Dim selectStmt As String = Me.Config("/SQL/GET_COVERAGE_DEDUCTIBLE_FROM_CONTRACT")
            Dim ds As New DataSet
            Dim whereClauseConditions As String = ""
            Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("effective_date", effectiveDate)}

            whereClauseConditions &= "   and ELP_LANGUAGE.LANGUAGE_ID = '" & Me.GuidToSQLString(languageId) & "'"
            whereClauseConditions &= "  and c.dealer_id= '" & Me.GuidToSQLString(dealerId) & "'"
            If Not whereClauseConditions = "" Then
                selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
            Else
                selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, "")
            End If

            DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function LoadDealerCoveragesInfo(ByRef ds As DataSet, ByVal dealerId As Guid, ByVal WarrSalesDate As Date) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/LOAD_DEALER_COVERAGES_INFO_FOR_WS")
        Dim parameters() As DBHelper.DBHelperParameter
        Dim whereClauseConditions As String = ""
        Dim OrderByClause As String = ""
        Dim strWarrSalesDate As String = WarrSalesDate.ToString("MM/dd/yyyy")

        parameters = New DBHelper.DBHelperParameter() _
                {New DBHelper.DBHelperParameter(COL_NAME_DEALER_ID, dealerId.ToByteArray), _
                New DBHelper.DBHelperParameter(COL_NAME_WARRANTY_SALES_DATE, WarrSalesDate, GetType(Date)) _
               }

        If ds Is Nothing Then ds = New DataSet
        Try
            DBHelper.Fetch(ds, selectStmt, "COVERAGES", parameters)

            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Sub ExpireCoverages(ByVal dealerId As Guid, ByVal expirationDate As Date, ByVal effectiveDate As Date, Optional ByVal Transaction As IDbTransaction = Nothing)
        Dim updateStmt As String = Me.Config("/SQL/EXPIRE_COVERAGES")
        Dim parameters() As DBHelper.DBHelperParameter
        
        Dim tr As IDbTransaction = Transaction
        If tr Is Nothing Then
            tr = DBHelper.GetNewTransaction
        End If

        parameters = New DBHelper.DBHelperParameter() _
                {New DBHelper.DBHelperParameter(COL_NAME_EXPIRATION, expirationDate), _
                 New DBHelper.DBHelperParameter(COL_NAME_DEALER_ID, dealerId.ToByteArray), _
                 New DBHelper.DBHelperParameter(COL_NAME_EFFECTIVE, effectiveDate), _
                 New DBHelper.DBHelperParameter(COL_NAME_EXPIRATION, expirationDate)}

        Try
            DBHelper.Execute(updateStmt, parameters, tr, True)
        
            If Transaction Is Nothing Then
                'We are the creator of the transaction we shoul commit it  and close the connection
                DBHelper.Commit(tr)
            End If
        Catch ex As Exception
            If Transaction Is Nothing Then
                'We are the creator of the transaction we shoul commit it  and close the connection
                DBHelper.RollBack(tr)
            End If
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function GetAssociatedCertificateCount(ByVal coverageId As Guid)
        Try
            Dim selectStmt As String = Me.Config("/SQL/GET_ASSOCIATED_CERT_COUNT")
            Dim ds As New DataSet
            Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("coverage_id", coverageId.ToByteArray)}
            DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

#End Region

#Region "Overloaded Methods"
    Public Overloads Sub Update(ByVal ds As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing, Optional ByVal changesFilter As DataRowState = Nothing)
        Dim oAttributeValueDAL As New AttributeValueDAL
        If ds Is Nothing Then
            Return
        End If
        If Not ds.Tables(Me.TABLE_NAME) Is Nothing Then
            MyBase.Update(ds.Tables(Me.TABLE_NAME), Transaction, changesFilter)
            oAttributeValueDAL.Update(ds.GetChanges(), Transaction)
        End If
    End Sub

    Public Overloads Sub UpdateFamily(ByVal familyDataset As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing)
        Dim oProdDepSchDal As New DepreciationScdRelationDal
        Dim oAttributeValueDAL As New AttributeValueDAL
        Dim tr As IDbTransaction = Transaction
        If tr Is Nothing Then
            tr = DBHelper.GetNewTransaction
        End If
        Try
             oProdDepSchDal.Update(familyDataset, tr, DataRowState.Deleted)
            MyBase.Update(familyDataset.Tables(Me.TABLE_NAME).GetChanges(DataRowState.Deleted), tr, DataRowState.Deleted)
            Update(familyDataset.Tables(Me.TABLE_NAME).GetChanges(DataRowState.Added Or DataRowState.Modified), tr, DataRowState.Added Or DataRowState.Modified)

            oAttributeValueDAL.Update(familyDataset.GetChanges(), tr)

            oProdDepSchDal.Update(familyDataset.GetChanges(DataRowState.Added Or DataRowState.Modified), tr, DataRowState.Added Or DataRowState.Modified)
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


