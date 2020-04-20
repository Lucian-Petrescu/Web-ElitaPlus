'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (4/19/2007)********************


Public Class AcctSettingDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_ACCT_SETTINGS"
    Public Const TABLE_KEY_NAME As String = "acct_settings_id"

    Public Const COL_NAME_ACCT_SETTINGS_ID As String = "acct_settings_id"
    Public Const COL_NAME_ACCT_COMPANY_ID As String = "acct_company_id"
    Public Const COL_NAME_SERVICE_CENTER_ID As String = "service_center_id"
    Public Const COL_NAME_DEALER_ID As String = "dealer_id"
    Public Const COL_NAME_DEALER_GROUP_ID As String = "dealer_group_id"
    Public Const COL_NAME_ACCOUNT_CODE As String = "account_code"
    Public Const COL_NAME_ADDRESS_LOOKUP_CODE As String = "address_lookup_code"
    Public Const COL_NAME_ADDRESS_SEQUENCE_NUMBER As String = "address_sequence_number"
    Public Const COL_NAME_ADDRESS_STATUS As String = "address_status"
    Public Const COL_NAME_ACCOUNT_TYPE As String = "account_type"
    Public Const COL_NAME_BALANCE_TYPE As String = "balance_type"
    Public Const COL_NAME_CONVERSION_CODE_CONTROL As String = "conversion_code_control"
    Public Const COL_NAME_ACCOUNT_ANALYSIS_1 As String = "account_analysis_1"
    Public Const COL_NAME_ACCOUNT_ANALYSIS_2 As String = "account_analysis_2"
    Public Const COL_NAME_ACCOUNT_ANALYSIS_3 As String = "account_analysis_3"
    Public Const COL_NAME_ACCOUNT_ANALYSIS_4 As String = "account_analysis_4"
    Public Const COL_NAME_ACCOUNT_ANALYSIS_5 As String = "account_analysis_5"
    Public Const COL_NAME_ACCOUNT_ANALYSIS_6 As String = "account_analysis_6"
    Public Const COL_NAME_ACCOUNT_ANALYSIS_7 As String = "account_analysis_7"
    Public Const COL_NAME_ACCOUNT_ANALYSIS_8 As String = "account_analysis_8"
    Public Const COL_NAME_ACCOUNT_ANALYSIS_9 As String = "account_analysis_9"
    Public Const COL_NAME_ACCOUNT_ANALYSIS_10 As String = "account_analysis_10"
    Public Const COL_NAME_ACCOUNT_ANALYSIS_CODE_1 As String = "account_analysis_code_1"
    Public Const COL_NAME_ACCOUNT_ANALYSIS_CODE_2 As String = "account_analysis_code_2"
    Public Const COL_NAME_ACCOUNT_ANALYSIS_CODE_3 As String = "account_analysis_code_3"
    Public Const COL_NAME_ACCOUNT_ANALYSIS_CODE_4 As String = "account_analysis_code_4"
    Public Const COL_NAME_ACCOUNT_ANALYSIS_CODE_5 As String = "account_analysis_code_5"
    Public Const COL_NAME_ACCOUNT_ANALYSIS_CODE_6 As String = "account_analysis_code_6"
    Public Const COL_NAME_ACCOUNT_ANALYSIS_CODE_7 As String = "account_analysis_code_7"
    Public Const COL_NAME_ACCOUNT_ANALYSIS_CODE_8 As String = "account_analysis_code_8"
    Public Const COL_NAME_ACCOUNT_ANALYSIS_CODE_9 As String = "account_analysis_code_9"
    Public Const COL_NAME_ACCOUNT_ANALYSIS_CODE_10 As String = "account_analysis_code_10"
    Public Const COL_NAME_REPORT_CONVERSION_CONTROL As String = "report_conversion_control"
    Public Const COL_NAME_DATA_ACCESS_GROUP_CODE As String = "data_access_group_code"
    Public Const COL_NAME_DEFAULT_CURRENCY_CODE As String = "default_currency_code"
    Public Const COL_NAME_ACCOUNT_STATUS As String = "account_status"
    Public Const COL_NAME_SUPPRESS_REVALUATION As String = "suppress_revaluation"
    Public Const COL_NAME_PAY_AS_PAID_ACCOUNT_TYPE As String = "pay_as_paid_account_type"
    Public Const COL_NAME_SUPPLIER_LOOKUP_CODE As String = "supplier_lookup_code"
    Public Const COL_NAME_PAYMENT_METHOD As String = "payment_method"
    Public Const COL_NAME_SUPPLIER_STATUS As String = "supplier_status"
    Public Const COL_NAME_SUPPLIER_ANALYSIS_CODE_1 As String = "supplier_analysis_code_1"
    Public Const COL_NAME_SUPPLIER_ANALYSIS_CODE_2 As String = "supplier_analysis_code_2"
    Public Const COL_NAME_SUPPLIER_ANALYSIS_CODE_3 As String = "supplier_analysis_code_3"
    Public Const COL_NAME_SUPPLIER_ANALYSIS_CODE_4 As String = "supplier_analysis_code_4"
    Public Const COL_NAME_SUPPLIER_ANALYSIS_CODE_5 As String = "supplier_analysis_code_5"
    Public Const COL_NAME_SUPPLIER_ANALYSIS_CODE_6 As String = "supplier_analysis_code_6"
    Public Const COL_NAME_SUPPLIER_ANALYSIS_CODE_7 As String = "supplier_analysis_code_7"
    Public Const COL_NAME_SUPPLIER_ANALYSIS_CODE_8 As String = "supplier_analysis_code_8"
    Public Const COL_NAME_SUPPLIER_ANALYSIS_CODE_9 As String = "supplier_analysis_code_9"
    Public Const COL_NAME_SUPPLIER_ANALYSIS_CODE_10 As String = "supplier_analysis_code_10"
    Public Const COL_NAME_BRANCH_ID As String = "branch_id"

    Public Const COL_NAME_PAYMENT_TERMS_ID As String = "payment_terms_id"

    Public Const COL_NAME_ACCOUNT_ANALYSIS_A_1 As String = "account_analysis_a_1"
    Public Const COL_NAME_ACCOUNT_ANALYSIS_A_2 As String = "account_analysis_a_2"
    Public Const COL_NAME_ACCOUNT_ANALYSIS_A_3 As String = "account_analysis_a_3"
    Public Const COL_NAME_ACCOUNT_ANALYSIS_A_4 As String = "account_analysis_a_4"
    Public Const COL_NAME_ACCOUNT_ANALYSIS_A_5 As String = "account_analysis_a_5"
    Public Const COL_NAME_ACCOUNT_ANALYSIS_A_6 As String = "account_analysis_a_6"
    Public Const COL_NAME_ACCOUNT_ANALYSIS_A_7 As String = "account_analysis_a_7"
    Public Const COL_NAME_ACCOUNT_ANALYSIS_A_8 As String = "account_analysis_a_8"
    Public Const COL_NAME_ACCOUNT_ANALYSIS_A_9 As String = "account_analysis_a_9"
    Public Const COL_NAME_ACCOUNT_ANALYSIS_A_10 As String = "account_analysis_a_10"

    Public Const COL_NAME_USER_AREA As String = "user_area"
    Public Const COL_NAME_COMMISSION_ENTITY_ID As String = "commission_entity_id"
    Public Const COL_NAME_DESCRIPTION As String = "description"
    Public Const COL_NAME_DEFAULT_BANK_SUB_CODE As String = "default_bank_sub_code"

    Public Enum VendorType As Integer
        Dealer
        ServiceCenter
        Branch
        DealerGroup
        CommissionEntity
    End Enum


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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("acct_settings_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, Me.TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Sub LoadByServiceCenter(ByVal familyDS As DataSet, ByVal ServiceCenterId As Guid, ByVal AcctCompanyId As Guid)

        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {
                                                                                      New DBHelper.DBHelperParameter(Me.COL_NAME_ACCT_COMPANY_ID, AcctCompanyId.ToByteArray),
                                                                                      New DBHelper.DBHelperParameter(Me.PAR_NAME_ROW_NUMBER, 1)}
        Dim whereClauseConditions As String = ""

        Try

            whereClauseConditions = " service_center_id = " + Common.GuidControl.GuidToHexString(ServiceCenterId)

            If Not whereClauseConditions = "" Then
                selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
            Else
                selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, "")
            End If

            DBHelper.Fetch(familyDS, selectStmt, Me.TABLE_NAME, parameters)

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try


    End Sub

    Public Function LoadByVendor(ByVal Code As String, ByVal AcctCompanyId As Guid, ByVal VendType As VendorType, ByVal AcctType As String, Optional ByVal BranchCode As String = "") As Guid

        Dim selectStmt As String = Me.Config("/SQL/GET_VENDOR_BY_CODE")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {
                                                                                      New DBHelper.DBHelperParameter(Me.COL_NAME_ACCT_COMPANY_ID, AcctCompanyId.ToByteArray)}
        Dim whereClauseConditions As String = ""
        Dim ret As Object

        Try

            Select Case VendType

                Case VendorType.Dealer, VendorType.Branch

                    whereClauseConditions = " AND UPPER(ELP_DEALER.DEALER) = '" + Code.ToUpper + "' "
                    If BranchCode.Length > 0 Then
                        whereClauseConditions += " AND UPPER(ELP_BRANCH.BRANCH_CODE) = '" + BranchCode.ToUpper + "' "
                    Else
                        whereClauseConditions += " AND ELP_ACCT_SETTINGS.BRANCH_ID IS NULL "
                    End If

                Case VendorType.ServiceCenter

                    whereClauseConditions = " AND UPPER(ELP_SERVICE_CENTER.CODE) = '" + Code.ToUpper + "' "

                Case VendorType.DealerGroup

                    whereClauseConditions = " AND UPPER(ELP_DEALER_GROUP.CODE) = '" + Code.ToUpper + "' "

                Case VendorType.CommissionEntity

                    whereClauseConditions = " AND UPPER(ELP_COMMISSION_ENTITY.ENTITY_NAME) = '" + Code.ToUpper + "' "

            End Select

            whereClauseConditions += " AND ELP_ACCT_SETTINGS.ACCT_COMPANY_ID = HEXTORAW('" + GuidControl.GuidToHexString(AcctCompanyId) + "') "
            whereClauseConditions += " AND ELP_ACCT_SETTINGS.ACCOUNT_TYPE = '" + AcctType + "' "

            If Not whereClauseConditions = "" Then
                selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
            Else
                selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, "")
            End If

            ret = DBHelper.ExecuteScalar(selectStmt)

            If Not ret Is Nothing Then
                Return GuidControl.ByteArrayToGuid(ret)
            Else
                Return Guid.Empty
            End If

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try


    End Function

    Public Sub LoadByDealer(ByVal familyDS As DataSet, ByVal DealerId As Guid, ByVal AcctCompanyId As Guid)

        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {
                                                                                       New DBHelper.DBHelperParameter(Me.COL_NAME_ACCT_COMPANY_ID, AcctCompanyId.ToByteArray),
                                                                                       New DBHelper.DBHelperParameter(Me.PAR_NAME_ROW_NUMBER, 1)}
        Dim whereClauseConditions As String = ""

        Try

            whereClauseConditions = " dealer_id = " + Common.GuidControl.GuidToHexString(DealerId)

            If Not whereClauseConditions = "" Then
                selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
            Else
                selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, "")
            End If

            DBHelper.Fetch(familyDS, selectStmt, Me.TABLE_NAME, parameters)

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Sub

    Public Function LoadDealers(ByVal strDealerName As String, ByVal strDealerCode As String, ByVal oCompanyIds As ArrayList, Optional ByVal HasAccountingInformation As Boolean = True) As DataSet

        Dim selectStmt As String = Me.Config("/SQL/LOAD_DEALER_SETTINGS")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {
                                                                  New DBHelper.DBHelperParameter(Me.PAR_NAME_ROW_NUMBER, Me.MAX_NUMBER_OF_ROWS)}
        Dim ds As New DataSet
        Dim whereClauseConditions As String = ""
        Try

            whereClauseConditions &= Environment.NewLine & " AND " & MiscUtil.BuildListForSql("elp_dealer." & DealerDAL.COL_NAME_COMPANY_ID, oCompanyIds, False)

            If Not strDealerName = String.Empty AndAlso Not strDealerName.Trim.Length = 0 Then
                whereClauseConditions &= " and upper(elp_dealer." & DealerDAL.COL_NAME_DEALER_NAME & ") like '" + strDealerName.ToUpper + "'"
                whereClauseConditions = whereClauseConditions.Replace("*", "%")
            Else
                whereClauseConditions &= " and elp_dealer." & DealerDAL.COL_NAME_DEALER & " like '" + strDealerCode.ToUpper + "'"
                whereClauseConditions = whereClauseConditions.Replace("*", "%")
            End If

            If Not whereClauseConditions = "" Then
                selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
            Else
                selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, "")
            End If

            selectStmt = selectStmt.Replace(Me.DYNAMIC_ORDER_BY_CLAUSE_PLACE_HOLDER, "ORDER BY upper(elp_dealer." & DealerDAL.COL_NAME_DEALER_NAME & ")")

            DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)
            Return ds

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Function GetDealersForAcctSetting(ByVal oCompanyIds As ArrayList) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/GET_DEALER_ACCT_SETTINGS")
        Dim whereClauseConditions As String = ""
        Dim inCausecondition As String = ""
        Dim bIsLikeClause As Boolean = False
        whereClauseConditions &= Environment.NewLine & " AND " & MiscUtil.BuildListForSql("elp_dealer." & DealerDAL.COL_NAME_COMPANY_ID, oCompanyIds, False)

        selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
        selectStmt = selectStmt.Replace(Me.DYNAMIC_ORDER_BY_CLAUSE_PLACE_HOLDER, "ORDER BY upper(elp_dealer." & DealerDAL.COL_NAME_DEALER_NAME & ")")

        Try
            Return (DBHelper.Fetch(selectStmt, Me.TABLE_NAME))
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function



    Public Function GetServiceCentersForAcctSetting(ByVal oCountryIds As ArrayList) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/GET_SERVICE_CENTER_ACCT_SETTINGS")
        Dim whereClauseConditions As String = ""
        Dim inCausecondition As String = ""
        Dim bIsLikeClause As Boolean = False
        whereClauseConditions &= Environment.NewLine & " AND " & MiscUtil.BuildListForSql("elp_service_center." & ServiceCenterDAL.COL_NAME_COUNTRY_ID, oCountryIds, False)
        selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
        selectStmt = selectStmt.Replace(Me.DYNAMIC_ORDER_BY_CLAUSE_PLACE_HOLDER, "ORDER BY upper(elp_service_center." & ServiceCenterDAL.COL_NAME_DESCRIPTION & ")")

        Try
            Return (DBHelper.Fetch(selectStmt, Me.TABLE_NAME))
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function LoadServiceCenters(ByVal strSCName As String, ByVal strSCCode As String, ByVal oCountryIds As ArrayList, Optional ByVal HasAccountingInformation As Boolean = True) As DataSet

        Dim selectStmt As String = Me.Config("/SQL/LOAD_SERVICE_CENTER_SETTINGS")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() { _
                                                                                       New DBHelper.DBHelperParameter(Me.PAR_NAME_ROW_NUMBER, Me.MAX_NUMBER_OF_ROWS)}
        Dim ds As New DataSet
        Dim whereClauseConditions As String = ""

        Try

            whereClauseConditions &= Environment.NewLine & " AND " & MiscUtil.BuildListForSql("elp_service_center." & ServiceCenterDAL.COL_NAME_COUNTRY_ID, oCountryIds, False)


            If Not strSCName = String.Empty AndAlso Not strSCName.Trim.Length = 0 Then
                whereClauseConditions &= Environment.NewLine & " and upper(elp_service_center." & ServiceCenterDAL.COL_NAME_DESCRIPTION & ") like '" + strSCName.ToUpper + "'"
                whereClauseConditions = whereClauseConditions.Replace("*", "%")
            Else
                whereClauseConditions &= " and elp_service_center." & ServiceCenterDAL.COL_NAME_CODE & " like '" + strSCCode.ToUpper + "'"
                whereClauseConditions = whereClauseConditions.Replace("*", "%")
            End If

            If Not whereClauseConditions = "" Then
                selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
            Else
                selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, "")
            End If

            selectStmt = selectStmt.Replace(Me.DYNAMIC_ORDER_BY_CLAUSE_PLACE_HOLDER, "ORDER BY upper(elp_service_center." & ServiceCenterDAL.COL_NAME_DESCRIPTION & ")")
            DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)
            Return ds

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function LoadCounterPartById(ByVal AcctSettingsId As Guid) As Guid
        Dim selectStmt As String = Me.Config("/SQL/GET_COUNTERPART_BY_ID")

        'Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() { _
        '                            New DBHelper.DBHelperParameter(Me.COL_NAME_ACCT_SETTINGS_ID, AcctSettingsId.ToByteArray)}
        selectStmt = selectStmt.Replace(":acct_settings_id", MiscUtil.GetDbStringFromGuid(AcctSettingsId, True))
        Dim ret As Object
        Try
            ret = DBHelper.ExecuteScalar(selectStmt)
            If Not ret Is Nothing Then
                Return GuidControl.ByteArrayToGuid(ret)
            Else
                Return Guid.Empty
            End If

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function GetServiceCentersByAcctCompanies(ByVal oAcctCompanyIds As ArrayList, Optional ByVal oSVCIDs As Generic.List(Of Guid) = Nothing) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/GET_SERVICE_CENTERS_BY_ACCOUNTING_COMPANIES")
        Dim whereClauseConditions As String = "", ds As DataSet = New DataSet

        whereClauseConditions &= Environment.NewLine & " AND " & MiscUtil.BuildListForSql(Me.COL_NAME_ACCT_COMPANY_ID, oAcctCompanyIds, False)

        Dim strSVCIDs As String, sbBuilder As System.Text.StringBuilder, blnStartPos As Boolean = True
        Dim I As Integer, BATCH_SIZE As Integer = 50, SVCId As Guid, intBatch As Integer = 1

        If (Not oSVCIDs Is Nothing) AndAlso (oSVCIDs.Count > 0) Then
            sbBuilder = New Text.StringBuilder
            For Each SVCId In oSVCIDs
                If blnStartPos Then
                    sbBuilder.Append("'")
                    blnStartPos = False
                Else
                    sbBuilder.Append(",'")
                End If
                sbBuilder.Append(GuidControl.GuidToHexString(SVCId))
                sbBuilder.Append("'")
                I += 1
                If I Mod BATCH_SIZE = 0 Then
                    strSVCIDs = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions & Environment.NewLine & String.Format(" AND service_center_ID in ({0})", sbBuilder.ToString()))
                    Try
                        DBHelper.Fetch(ds, strSVCIDs, "SVCList" & intBatch.ToString, New DBHelper.DBHelperParameter() {})
                    Catch ex As Exception
                        Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
                    End Try
                    strSVCIDs = ""
                    sbBuilder.Remove(0, sbBuilder.Length)
                    blnStartPos = True
                    intBatch += 1
                End If
            Next

            If sbBuilder.Length > 0 Then
                strSVCIDs = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions & Environment.NewLine & String.Format(" AND service_center_ID in ({0})", sbBuilder.ToString()))
                Try
                    DBHelper.Fetch(ds, strSVCIDs, "SVCList" & intBatch.ToString, New DBHelper.DBHelperParameter() {})
                Catch ex As Exception
                    Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
                End Try
            End If
        Else
            selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)

            Try
                ds = DBHelper.Fetch(selectStmt, "ServiceCenters")
            Catch ex As Exception
                Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
            End Try
        End If

        Return ds

    End Function

    Public Function GetServiceCentersByCountries(ByVal oCountryIds As ArrayList) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/GET_SERVICE_CENTERS_BY_Countries")
        Dim whereClauseConditions As String = ""
        whereClauseConditions &= Environment.NewLine & " AND " & MiscUtil.BuildListForSql("s.Country_id", oCountryIds, False)

        selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)

        Dim ds As DataSet
        Try
            ds = DBHelper.Fetch(selectStmt, "ServiceCenters")
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function
#Region "Dealer Group"
    Public Function LoadDealerGroups(ByVal strDealerGroupName As String, ByVal strDealerGroupCode As String, ByVal oCompanyGroupId As Guid) As DataSet

        Dim selectStmt As String = Me.Config("/SQL/LOAD_DEALER_GROUP_SETTINGS")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() { _
                                                                  New DBHelper.DBHelperParameter("company_group_id", oCompanyGroupId.ToByteArray), _
                                                                  New DBHelper.DBHelperParameter(Me.PAR_NAME_ROW_NUMBER, Me.MAX_NUMBER_OF_ROWS)}
        Dim ds As New DataSet
        Dim whereClauseConditions As String = ""
        Try
            If strDealerGroupName.Trim.Length > 0 Then
                whereClauseConditions &= " and upper(elp_dealer_group." & DealerGroupDAL.COL_NAME_DESCRIPTION & ") like '" + strDealerGroupName.ToUpper.Replace("*", "%") + "'"
            End If
            If strDealerGroupCode.Trim.Length > 0 Then
                whereClauseConditions &= " and upper(elp_dealer_group." & DealerGroupDAL.COL_NAME_CODE & ") like '" + strDealerGroupCode.ToUpper.Replace("*", "%") + "'"
            End If

            selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)

            selectStmt = selectStmt.Replace(Me.DYNAMIC_ORDER_BY_CLAUSE_PLACE_HOLDER, "ORDER BY upper(elp_dealer_group." & DealerGroupDAL.COL_NAME_DESCRIPTION & ")")

            DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)
            Return ds

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Function GetDealerGroupForAcctSetting(ByVal oCompanyGroupId As Guid) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/GET_DEALER_GROUP_ACCT_SETTINGS")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() { _
                    New DBHelper.DBHelperParameter("company_group_id", oCompanyGroupId.ToByteArray)}
        Dim ds As New DataSet
        Try
            DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function
#End Region

#Region "Branch"

    Public Function LoadBranches(ByVal strBranchName As String, ByVal strBranchCode As String, ByVal oCompanyIds As ArrayList) As DataSet

        Dim selectStmt As String = Me.Config("/SQL/LOAD_BRANCH_SETTINGS")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() { _
                                                                  New DBHelper.DBHelperParameter(Me.PAR_NAME_ROW_NUMBER, Me.MAX_NUMBER_OF_ROWS)}
        Dim ds As New DataSet
        Dim whereClauseConditions As String = ""
        Try

            whereClauseConditions &= Environment.NewLine & " AND " & MiscUtil.BuildListForSql("elp_dealer." & DealerDAL.COL_NAME_COMPANY_ID, oCompanyIds, False)

            If Not strBranchName = String.Empty AndAlso Not strBranchName.Trim.Length = 0 Then
                whereClauseConditions &= " and upper(elp_branch." & BranchDAL.COL_NAME_BRANCH_NAME & ") like '" + strBranchName.ToUpper + "'"
                whereClauseConditions = whereClauseConditions.Replace("*", "%")
            Else
                whereClauseConditions &= " and elp_branch." & BranchDAL.COL_NAME_BRANCH_CODE & " like '" + strBranchCode.ToUpper + "'"
                whereClauseConditions = whereClauseConditions.Replace("*", "%")
            End If

            If Not whereClauseConditions = "" Then
                selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
            Else
                selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, "")
            End If

            selectStmt = selectStmt.Replace(Me.DYNAMIC_ORDER_BY_CLAUSE_PLACE_HOLDER, _
                "ORDER BY upper(elp_dealer." & DealerDAL.COL_NAME_DEALER_NAME & _
                        "), upper(elp_branch." & BranchDAL.COL_NAME_BRANCH_NAME & _
                        "), elp_branch." & BranchDAL.COL_NAME_BRANCH_CODE)

            DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)
            Return ds

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Function GetDealersForBranch(ByVal oCompanyIds As ArrayList) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/GET_DEALER_BRANCH")
        Dim whereClauseConditions As String = ""
        Dim inCausecondition As String = ""
        Dim bIsLikeClause As Boolean = False
        whereClauseConditions &= Environment.NewLine & " AND " & MiscUtil.BuildListForSql("elp_dealer." & DealerDAL.COL_NAME_COMPANY_ID, oCompanyIds, False)

        selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
        selectStmt = selectStmt.Replace(Me.DYNAMIC_ORDER_BY_CLAUSE_PLACE_HOLDER, "ORDER BY upper(elp_dealer." & DealerDAL.COL_NAME_DEALER_NAME & ")")

        Try
            Return (DBHelper.Fetch(selectStmt, Me.TABLE_NAME))
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function GetBranchesForAcctSetting(ByVal oDealerId As Guid) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/GET_BRANCH_ACCT_SETTINGS")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() { _
                        New DBHelper.DBHelperParameter(Me.COL_NAME_DEALER_ID, oDealerId.ToByteArray), _
                        New DBHelper.DBHelperParameter(Me.COL_NAME_DEALER_ID, oDealerId.ToByteArray)}
        Dim ds As New DataSet
        Try
            DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function


#End Region
#Region "Load Commission entity"
    Public Function LoadCommissionEntities(ByVal strCommissionEntityName As String, ByVal oCompanyGroupId As Guid) As DataSet

        Dim selectStmt As String = Me.Config("/SQL/LOAD_COMMISSION_ENTITY_SETTINGS")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() { _
                                                                  New DBHelper.DBHelperParameter("company_group_id", oCompanyGroupId.ToByteArray), _
                                                                  New DBHelper.DBHelperParameter(Me.PAR_NAME_ROW_NUMBER, Me.MAX_NUMBER_OF_ROWS)}
        Dim ds As New DataSet
        Dim whereClauseConditions As String = ""
        Try
            If strCommissionEntityName.Trim.Length > 0 Then
                whereClauseConditions &= " and upper(c." & CommissionEntityDAL.COL_NAME_ENTITY_NAME & ") like '" + strCommissionEntityName.ToUpper.Replace("*", "%") + "'"
            End If

            selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)

            selectStmt = selectStmt.Replace(Me.DYNAMIC_ORDER_BY_CLAUSE_PLACE_HOLDER, "ORDER BY upper(c." & CommissionEntityDAL.COL_NAME_ENTITY_NAME & ")")

            DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)
            Return ds

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Function GetCommissionEntityListForAcctSetting(ByVal oCompanyGroupId As Guid) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/GET_COMMISION_ENTITY_FOR_ACCT_SETTINGS")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {
                    New DBHelper.DBHelperParameter("company_group_id", oCompanyGroupId.ToByteArray)}
        Dim ds As New DataSet
        Try
            DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function IsIDXAcctSettingAndCode(accountCompanyId As Guid, accountCode As String) As Boolean
        Dim selectStmt As String = Me.Config("/SQL/GET_COUNT_BY_ACCOUNTING_COMPANY_AND_CODE")

        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("acct_company_id", accountCompanyId.ToByteArray),
                                                                                           New DBHelper.DBHelperParameter("account_code", accountCode)}
        Dim count As Integer

        Try
            count = DBHelper.ExecuteScalar(selectStmt, parameters)
            If count > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function
#End Region
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


