'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (10/7/2004)********************
Public Class ServiceCenterDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_SERVICE_CENTER"
    Public Const TABLE_NAME_WS As String = "SERVICE_CENTER"
    Public Const TABLE_KEY_NAME As String = "service_center_id"
    Public Const COUNTRY_TABLE_NAME As String = "ELP_COUNTRY"

    Public Const COL_NAME_SERVICE_CENTER_ID As String = "service_center_id"
    Public Const COL_NAME_COUNTRY_ID As String = "country_id"
    Public Const COL_NAME_COUNTRY_DESC As String = "country_description"
    Public Const COL_NAME_ADDRESS_ID As String = "address_id"
    'Public Const COL_NAME_PRICE_GROUP_ID As String = "price_group_id"
    Public Const COL_NAME_SERVICE_GROUP_ID As String = "service_group_id"
    'Public Const COL_NAME_SERVICE_TYPE_ID As String = "service_type_id"
    Public Const COL_NAME_LOANER_CENTER_ID As String = "loaner_center_id"
    Public Const COL_NAME_MASTER_CENTER_ID As String = "master_center_id"
    Public Const COL_NAME_CODE As String = "code"
    Public Const COL_NAME_DESCRIPTION As String = "description"
    Public Const COL_NAME_RATING_CODE As String = "rating_code"
    Public Const COL_NAME_CONTACT_NAME As String = "contact_name"
    Public Const COL_NAME_OWNER_NAME As String = "owner_name"
    Public Const COL_NAME_PHONE1 As String = "phone1"
    Public Const COL_NAME_PHONE2 As String = "phone2"
    Public Const COL_NAME_FAX As String = "fax"
    Public Const COL_NAME_EMAIL As String = "email"
    Public Const COL_NAME_CC_EMAIL As String = "cc_email"
    Public Const COL_NAME_FTP_ADDRESS As String = "ftp_address"
    Public Const COL_NAME_TAX_ID As String = "tax_id"
    Public Const COL_NAME_SERVICE_WARRANTY_DAYS As String = "service_warranty_days"
    Public Const COL_NAME_STATUS_CODE As String = "status_code"
    Public Const COL_NAME_BUSINESS_HOURS As String = "business_hours"
    Public Const COL_NAME_MASTER_FLAG As String = "master_flag"
    Public Const COL_NAME_LOANER_FLAG As String = "loaner_flag"
    Public Const COL_NAME_PAY_MASTER As String = "pay_master"
    Public Const COL_NAME_DEFAULT_TO_EMAIL_FLAG As String = "default_to_email_flag"
    Public Const COL_NAME_IVA_RESPONSIBLE_FLAG As String = "iva_responsible_flag"
    Public Const COL_NAME_FREE_ZONE_FLAG As String = "free_zone_flag"
    Public Const COL_NAME_INTEGRATED_WITH_ID As String = "integrated_with_id"
    Public Const COL_NAME_REVERSE_LOGISTICS_ID As String = "reverse_logistics_id"

    Public Const COL_NAME_CITY As String = "city" '               addr.city,
    Public Const COL_NAME_ADDRESS1 As String = "address1" 'addr.address1,
    Public Const COL_NAME_ADDRESS2 As String = "address2" 'addr.address2,
    Public Const COL_NAME_ZIP As String = "postal_code" 'addr.postal_code,
    Public Const COL_NAME_ZIP_LOCATOR As String = "zip_locator" 'addr.zip_locator,
    Public Const COL_NAME_DEALER_PREF_FLAG As String = "dealer_pref_flag" 'decode( sc_dp.dealer_id, null, 'n', 'y') dealer_pref_flag,
    Public Const COL_NAME_MAN_AUTH_FLAG As String = "man_auth_flag" 'decode( sc_mfa.manufacturer_id, null, 'n', 'y') man_auth_flag,
    Public Const COL_NAME_COVER_ZIP_CODE_FLAG As String = "cover_zip_code_flag" 'decode( zd.zip_code, null, 'n', 'y' ) cover_zip_code_flag,
    Public Const COL_NAME_COVER_ITEM_FLAG As String = "cover_item_flag" 'decode( sg.service_group_id, null, 'n', 'y') cover_item_flag
    Public Const COL_NAME_DEALERS_SVC_FLAG As String = "dealers_SVC_flag" '0 for ESC, 1 for VSC if there is a Dealer's Service Center for the dealer of the cert
    Public Const COL_NAME_COMMENTS As String = "comments"
    Public Const COL_NAME_PAYMENT_METHOD_ID As String = "payment_method_id"
    Public Const COL_NAME_BANKINFO_ID As String = "bank_info_id"
    Public Const COL_NAME_ROUTE_ID As String = "route_id"

    Public Const COL_NAME_SHIPPING As String = "shipping"
    Public Const COL_NAME_PROCESSING_FEE As String = "processing_fee"
    Public Const COL_NAME_ORIGINAL_DEALER As String = "ORIGINALDEALERID"
    Public Const COL_NAME_INTEGRATED_AS_OF As String = "integrated_as_of"
    'Public Const COL_NAME_SERVICE_LEVEL_GROUP_ID As String = "service_level_group_id"
    Public Const COL_NAME_DISTRIBUTION_METHOD_ID As String = "distribution_method_id"
    Public Const COL_NAME_FULFILLMENT_TIME_ZONE_ID As String = "fulfillment_time_zone_id"
    Public Const COL_NAME_PRICE_LIST_CODE As String = "PRICE_LIST_CODE"


    Public Const COL_NAME_PRICE_LIST_CODE_INPROGRESS As String = "PRICE_LIST_CODE_INPROGRESS"
    Public Const COL_NAME_PRICE_LIST_CODE_INPROGRESS_STATUS As String = "PL_CODE_INPROGRESS_STATUS"


    Public Const COL_NAME_DISCOUNT_PCT As String = "discount_pct"
    Public Const COL_NAME_DISCOUNT_DAYS As String = "discount_days"
    Public Const COL_NAME_NET_DAYS As String = "net_days"

    Public Const COL_NAME_WITHHOLDING_RATE As String = "withholding_rate"

    'SHIPPING_ID
    Public Const DYNAMIC_COUNTRY_LOAD As String = "--dynamic_Country_Load"
    Public Const VSCDealerTypeCode As String = "2"
    Public Const COL_NAME_SERVICE_GROUP_DESC As String = "Service_Group_Desc"
    Public Const ROW_MAX As String = "101"

    Public Const TABLE_NAME_PRICE_LIST_WS As String = "PRICE_LIST"

    Public Const COL_NAME_COMPANY_CODE As String = "company_code"
    Public Const COL_NAME_SERVICE_CENTER_CODE As String = "service_center_code"
    Public Const COL_NAME_PRE_INVOICE_ID As String = "pre_invoice_id"
    Public Const COL_NAME_AUTO_PROCESS_INV_FILE_XCD As String = "auto_process_inv_file_xcd"

    'US 224101 
    Public Const PAR_OUT_NAME_RETURN_CODE As String = "po_return_code"

    Public Const PAR_IN_NAME_SERVICE_CENTER_ID As String = "pi_service_center_id"
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

        Dim parameters() As DBHelper.DBHelperParameter
        parameters = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("service_center_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, Me.TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Sub Load(ByVal familyDS As DataSet, ByVal code As String, ByVal oCountryIds As ArrayList)
        Dim selectStmt As String = Me.Config("/SQL/GET_SERVICE_CENTER_BY_CODE")

        Dim whereClauseConditions As String = ""
        whereClauseConditions &= Environment.NewLine & " AND " & MiscUtil.BuildListForSql(Me.COL_NAME_COUNTRY_ID, oCountryIds, False)

        selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("code", code)}

        Try
            DBHelper.Fetch(familyDS, selectStmt, Me.TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Sub GetServiceCenterID(ByVal familyDS As DataSet, ByVal code As String, ByVal oCountryIds As ArrayList)
        Dim selectStmt As String = Me.Config("/SQL/GET_SERVICE_CENTER_ID")

        Dim whereClauseConditions As String = ""
        whereClauseConditions &= Environment.NewLine & " AND " & MiscUtil.BuildListForSql(Me.COL_NAME_COUNTRY_ID, oCountryIds, False)

        selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("code", code)}

        Try
            DBHelper.Fetch(familyDS, selectStmt, Me.TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    'Mannually Coded Method
    'If code, description, address, city and zip are ALL empty Guid it will return All Service Centers for the specified Countries
    Public Function LoadList(ByVal oCountryIds As ArrayList, ByVal code As String, _
                             ByVal description As String, ByVal address As String, _
                             ByVal city As String, ByVal zip As String) As DataSet

        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST")

        Dim whereClauseConditions As String = ""

        whereClauseConditions &= Environment.NewLine & " AND " & MiscUtil.BuildListForSql("c." & Me.COL_NAME_COUNTRY_ID, oCountryIds, False)
        If Me.FormatSearchMask(code) Then
            whereClauseConditions &= " AND " & Environment.NewLine & "UPPER(sc." & Me.COL_NAME_CODE & ") " & code.ToUpper
        End If
        If Me.FormatSearchMask(description) Then
            whereClauseConditions &= " AND " & Environment.NewLine & "UPPER(sc." & Me.COL_NAME_DESCRIPTION & ") " & description.ToUpper
        End If
        If Me.FormatSearchMask(address) Then
            whereClauseConditions &= " AND " & Environment.NewLine & "NVL(UPPER(a." & Me.COL_NAME_ADDRESS1 & "),' ') " & address.ToUpper
        End If
        If Me.FormatSearchMask(city) Then
            whereClauseConditions &= " AND " & Environment.NewLine & "NVL(UPPER(a." & Me.COL_NAME_CITY & "),' ') " & city.ToUpper
        End If
        If Me.FormatSearchMask(zip) Then
            whereClauseConditions &= " AND " & Environment.NewLine & "NVL(UPPER(a." & Me.COL_NAME_ZIP & "),' ') " & zip.ToUpper
        End If

        whereClauseConditions &= " AND " & Environment.NewLine & "ROWNUM < " & ROW_MAX

        If Not whereClauseConditions = "" Then
            selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
        Else
            selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, "")
        End If

        Try
            Dim ds As New DataSet
            ds = DBHelper.Fetch(selectStmt, Me.TABLE_NAME)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

#End Region

#Region "Overloaded Methods"

    'This method was added manually to accommodate BO families Save
    Public Overloads Sub UpdateFamily(ByVal familyDataset As DataSet, ByVal companyId As Guid, Optional ByVal Transaction As IDbTransaction = Nothing, Optional ByVal blnBankInfoSave As Boolean = False _
                                      )
        Dim svcCtrMfrDal As New ServiceCenterManufacturerDAL
        Dim svcCtrDlrDal As New ServiceCenterDealerDAL
        Dim svcCtrZipDstDal As New ServiceCenterZipDistrictDAL
        Dim svcCtrNetworkDal As New ServiceNetworkSvcDAL
        Dim svcCtrMorDal As New ServCenterMethRepairDAL
        Dim AddressDAL As New AddressDAL
        Dim BankInfoDAL As New BankInfoDAL

        Dim VendorQuantityDAL As New VendorQuantityDAL
        Dim ContactInfoDAL As New ContactInfoDAL
        Dim VendorContactDAL As New VendorContactDAL
        Dim ServiceScheduleDAL As New ServiceScheduleDAL
        Dim ScheduleDAL As New ScheduleDAL
        Dim ScheduleDetailDAL As New ScheduleDetailDAL
        Dim oAttributeValueDAL As New AttributeValueDAL

        Dim oSVCPlRecon As New SvcPriceListReconDAL

        Dim tr As IDbTransaction = Transaction
        If tr Is Nothing Then
            tr = DBHelper.GetNewTransaction
        End If
        Try
            'First Pass updates Deletions

            svcCtrMfrDal.Update(familyDataset, tr, DataRowState.Deleted)
            svcCtrDlrDal.Update(familyDataset, tr, DataRowState.Deleted)
            svcCtrZipDstDal.Update(familyDataset, tr, DataRowState.Deleted)
            svcCtrNetworkDal.Update(familyDataset, tr, DataRowState.Deleted)
            svcCtrMorDal.Update(familyDataset, tr, DataRowState.Deleted)
            VendorQuantityDAL.Update(familyDataset, tr, DataRowState.Deleted)
            VendorContactDAL.Update(familyDataset, tr, DataRowState.Deleted)
            ContactInfoDAL.Update(familyDataset, tr, DataRowState.Deleted)
            ServiceScheduleDAL.Update(familyDataset, tr, DataRowState.Deleted)
            ScheduleDAL.Update(familyDataset, tr, DataRowState.Deleted)
            ScheduleDetailDAL.Update(familyDataset, tr, DataRowState.Deleted)

            'oSVCPlRecon.Update(familyDataset.Tables(Me.TABLE_NAME), tr, DataRowState.Deleted)

            MyBase.Update(familyDataset.Tables(Me.TABLE_NAME).GetChanges(DataRowState.Deleted), tr, DataRowState.Deleted)

            'Second Pass updates additions And changes
            oAttributeValueDAL.Update(familyDataset.GetChanges(), tr)
            AddressDAL.Update(familyDataset.GetChanges(DataRowState.Added Or DataRowState.Modified), tr, DataRowState.Added Or DataRowState.Modified)
            If blnBankInfoSave Then
                BankInfoDAL.Update(familyDataset.GetChanges(DataRowState.Added Or DataRowState.Modified), tr, DataRowState.Added Or DataRowState.Modified)
            End If


            Update(familyDataset.Tables(Me.TABLE_NAME).GetChanges(DataRowState.Added Or DataRowState.Modified), tr, DataRowState.Added Or DataRowState.Modified)
            svcCtrMfrDal.Update(familyDataset.GetChanges(DataRowState.Added Or DataRowState.Modified), tr, DataRowState.Added Or DataRowState.Modified)
            svcCtrDlrDal.Update(familyDataset.GetChanges(DataRowState.Added Or DataRowState.Modified), tr, DataRowState.Added Or DataRowState.Modified)
            svcCtrZipDstDal.Update(familyDataset.GetChanges(DataRowState.Added Or DataRowState.Modified), tr, DataRowState.Added Or DataRowState.Modified)
            svcCtrNetworkDal.Update(familyDataset.GetChanges(DataRowState.Added Or DataRowState.Modified), tr, DataRowState.Added Or DataRowState.Modified)
            svcCtrMorDal.Update(familyDataset.GetChanges(DataRowState.Added Or DataRowState.Modified), tr, DataRowState.Added Or DataRowState.Modified)
            VendorQuantityDAL.UpdateVendorQuantityForServiceCenter(familyDataset.GetChanges(DataRowState.Added Or DataRowState.Modified), tr, DataRowState.Added Or DataRowState.Modified)
            ContactInfoDAL.Update(familyDataset.GetChanges(DataRowState.Added Or DataRowState.Modified), tr, DataRowState.Added Or DataRowState.Modified)
            VendorContactDAL.Update(familyDataset.GetChanges(DataRowState.Added Or DataRowState.Modified), tr, DataRowState.Added Or DataRowState.Modified)
            ServiceScheduleDAL.Update(familyDataset.GetChanges(DataRowState.Added Or DataRowState.Modified), tr, DataRowState.Added Or DataRowState.Modified)
            oSVCPlRecon.Update(familyDataset, tr, DataRowState.Added Or DataRowState.Modified)


            'At the end delete the Address
            AddressDAL.Update(familyDataset.GetChanges(DataRowState.Deleted), tr, DataRowState.Deleted)
            BankInfoDAL.Update(familyDataset.GetChanges(DataRowState.Deleted), tr, DataRowState.Deleted)
            If Not familyDataset.Tables(TransactionLogHeaderDAL.TABLE_NAME) Is Nothing AndAlso familyDataset.Tables(TransactionLogHeaderDAL.TABLE_NAME).Rows.Count > 0 Then
                Dim oTransactionLogHeaderDAL As New TransactionLogHeaderDAL
                oTransactionLogHeaderDAL.Update(familyDataset.Tables(TransactionLogHeaderDAL.TABLE_NAME), tr, DataRowState.Added Or DataRowState.Modified)
            End If

            If Transaction Is Nothing Then
                'We are the creator of the transaction we should commit it  and close the connection
                DBHelper.Commit(tr)
                'ALR- 06/19/2007 - Added the accept changes call to update the row states in the dataset
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


    Public Overloads Sub Update(ByVal ds As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing, Optional ByVal changesFilter As DataRowState = Nothing)
        If Not ds.Tables(Me.TABLE_NAME) Is Nothing Then
            MyBase.Update(ds.Tables(Me.TABLE_NAME), Transaction, changesFilter)
        End If
    End Sub


#End Region

    Public Function GetLocateServiceCenterDetails(ByVal serviceCenterIds As ArrayList, ByVal dealerId As Guid, ByVal manufacturerId As Guid) As DataSet
        Dim selectStmt As String
        Dim whereClauseConditions As String = String.Empty
        selectStmt = Me.Config("/SQL/GET_SERVICE_CENTER_DETAILS")
        If (Not serviceCenterIds Is Nothing) Then
            whereClauseConditions &= Environment.NewLine & " AND " & MiscUtil.BuildListForSql("elp_service_center." & Me.COL_NAME_SERVICE_CENTER_ID, serviceCenterIds, False)
        End If
        selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
        Try
            Dim ds As New DataSet
            Dim parameters() As DBHelper.DBHelperParameter
            parameters = New DBHelper.DBHelperParameter() { _
                            New DBHelper.DBHelperParameter("dealer_id", dealerId), _
                            New DBHelper.DBHelperParameter("manufacturer_id", manufacturerId)}
            DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function


    'zipLocator = Nothing will retrieve all the SC regardless of their Address Zip Locator
    'city = Nothing will retrieve all the SC regardless of their Address city
    'The other parameters are required (i.e. they cannot be nothing)
    Public Function LocateServiceCenter(ByVal oCountryIds As ArrayList, _
                                                    ByVal dealerId As Guid, _
                                                    ByVal zipLocator As String, _
                                                    ByVal city As String, _
                                                    ByVal riskTypeId As Guid, _
                                                    ByVal manufacturerId As Guid, _
                                                    ByVal ServiceNetWrkID As Guid, _
                                                    ByVal isNetwork As Boolean, _
                                                    ByVal MethodOfRepairID As Guid, _
                                                    Optional ByVal isSearchByCity As Boolean = False, _
                                                    Optional ByVal UseZipDistrict As Boolean = True, _
                                                    Optional ByVal dealerType As String = "", _
                                                    Optional ByVal FlagMethodOfRepairRecovery As Boolean = False, _
                                                    Optional ByVal blnCheckAcctSetting As Boolean = False) As DataSet

        Dim selectStmt As String
        If dealerType = VSCDealerTypeCode Then
            selectStmt = Me.Config("/SQL/LOCATE_SERVICE_CENTER_DEALERS_SVC")
        Else
            selectStmt = Me.Config("/SQL/LOCATE_SERVICE_CENTER")
        End If

        If Not UseZipDistrict Then
            If dealerType = VSCDealerTypeCode Then
                selectStmt = Me.Config("/SQL/LOCATE_SERVICE_CENTER_WITH_NO_ZIP_DISTRICT_DEALERS_SVC")
            Else
                selectStmt = Me.Config("/SQL/LOCATE_SERVICE_CENTER_WITH_NO_ZIP_DISTRICT")
            End If
        End If
        Dim whereClauseConditions As String = ""
        Dim inClauseConditions As String = ""

        'If city Is Nothing OrElse city.Trim.Length = 0 Then city = ""
        If isSearchByCity Then
            If (Not city Is Nothing) AndAlso (city.Trim.Length > 0) Then
                city = city.Trim("*").ToUpper & IIf(city.EndsWith("*"), "%", "")
            Else
                city = ""
            End If
        Else
            city = ""
            city &= DALBase.WILDCARD_CHAR
        End If
        'city &= DALBase.WILDCARD_CHAR

        Dim zipLocatorParameter As DBHelper.DBHelperParameter
        If zipLocator Is Nothing OrElse zipLocator.Trim.Length = 0 Then
            zipLocatorParameter = New DBHelper.DBHelperParameter("zip_code", DBNull.Value)
        Else
            zipLocatorParameter = New DBHelper.DBHelperParameter("zip_code", zipLocator)
        End If

        whereClauseConditions &= Environment.NewLine & " AND " & MiscUtil.BuildListForSql("sc." & Me.COL_NAME_COUNTRY_ID, oCountryIds, False)

        If dealerType = VSCDealerTypeCode Then
            inClauseConditions &= Environment.NewLine & " AND " & MiscUtil.BuildListForSql("sc." & Me.COL_NAME_COUNTRY_ID, oCountryIds, False)
        End If

        If FlagMethodOfRepairRecovery = False Then
            'If srvCenterIDs.Count > 0 Then
            If isNetwork Then
                'whereClauseConditions &= Environment.NewLine & " AND " & MiscUtil.BuildListForSql("sc." & Me.COL_NAME_SERVICE_CENTER_ID, srvCenterIDs, False)
                whereClauseConditions &= Environment.NewLine & " AND sc." & Me.COL_NAME_SERVICE_CENTER_ID & " in (select snsc.service_center_id from elp_service_network_svc snsc where service_network_id  = " & MiscUtil.GetDbStringFromGuid(ServiceNetWrkID, True) & ") "
            Else
                'whereClauseConditions &= Environment.NewLine & " AND " & MiscUtil.BuildNotInListForSql("sc." & Me.COL_NAME_SERVICE_CENTER_ID, srvCenterIDs, False)
                whereClauseConditions &= Environment.NewLine & " AND sc." & Me.COL_NAME_SERVICE_CENTER_ID & " not in (select snsc.service_center_id from elp_service_network sn, elp_service_network_svc snsc where sn.service_network_id  = snsc.service_network_id) "
            End If
            'End If
        End If

        If blnCheckAcctSetting Then
            whereClauseConditions &= Environment.NewLine & " and ( exists(select null from elp_acct_settings a WHERE sc.service_center_id = a.service_center_id) OR "
            whereClauseConditions &= " exists(select null from elp_acct_settings a WHERE sc.master_center_id = a.service_center_id and not sc.master_center_id is null) ) "
        End If

        If Not whereClauseConditions = "" Then
            selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
        Else
            selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, "")
        End If

        If Not inClauseConditions = "" Then
            selectStmt = selectStmt.Replace(Me.DYNAMIC_IN_CLAUSE_PLACE_HOLDER, inClauseConditions)
        Else
            selectStmt = selectStmt.Replace(Me.DYNAMIC_IN_CLAUSE_PLACE_HOLDER, "")
        End If

        Dim parameters() As DBHelper.DBHelperParameter

        If dealerType = VSCDealerTypeCode Then
            ' add extra parameters for VSC in the exact same order they are in the sql statement of xml
            parameters = New DBHelper.DBHelperParameter() { _
                                   New DBHelper.DBHelperParameter("dealer_id", dealerId), _
                                   New DBHelper.DBHelperParameter("manufacturer_id", manufacturerId), _
                                   New DBHelper.DBHelperParameter("dealer_id", dealerId), _
                                   New DBHelper.DBHelperParameter("dealer_id", dealerId), _
                                   New DBHelper.DBHelperParameter("manufacturer_id", manufacturerId), _
                                   New DBHelper.DBHelperParameter("risk_type_id", riskTypeId), _
                                   New DBHelper.DBHelperParameter("manufacturer_id", manufacturerId), _
                                   zipLocatorParameter, _
                                   New DBHelper.DBHelperParameter("city", city), _
            New DBHelper.DBHelperParameter(" :method_repair_id", MethodOfRepairID)}

        Else
            parameters = New DBHelper.DBHelperParameter() { _
                            New DBHelper.DBHelperParameter("dealer_id", dealerId), _
                            New DBHelper.DBHelperParameter("manufacturer_id", manufacturerId), _
                            New DBHelper.DBHelperParameter("risk_type_id", riskTypeId), _
                            New DBHelper.DBHelperParameter("manufacturer_id", manufacturerId), _
                            zipLocatorParameter, _
                            New DBHelper.DBHelperParameter("city", city), _
                            New DBHelper.DBHelperParameter(" :method_repair_id", MethodOfRepairID)}
        End If

        Try
            Dim ds As New DataSet
            DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Function LocateServiceCenterbyId(ByVal dealerId As Guid, _
                                            ByVal zipLocator As String, _
                                            ByVal riskTypeId As Guid, _
                                            ByVal manufacturerId As Guid, _
                                            ByVal MethodOfRepairID As Guid, _
                                            ByVal ServiceCenterId As Guid) As DataSet

        Dim parameters() As DBHelper.DBHelperParameter, selectStmt As String

        Dim zipLocatorParameter As DBHelper.DBHelperParameter
        If zipLocator Is Nothing OrElse zipLocator.Trim.Length = 0 Then
            zipLocatorParameter = New DBHelper.DBHelperParameter("zip_code", DBNull.Value)
        Else
            zipLocatorParameter = New DBHelper.DBHelperParameter("zip_code", zipLocator)
        End If

        parameters = New DBHelper.DBHelperParameter() { _
                                   New DBHelper.DBHelperParameter("dealer_id", dealerId), _
                                   New DBHelper.DBHelperParameter("manufacturer_id", manufacturerId), _
                                   New DBHelper.DBHelperParameter("risk_type_id", riskTypeId), _
                                   New DBHelper.DBHelperParameter("manufacturer_id", manufacturerId), _
                                   zipLocatorParameter, _
                                   New DBHelper.DBHelperParameter("service_Center_id", ServiceCenterId)}

        Try
            Dim ds As New DataSet
            DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function


    Public Function LoadAllServiceCenter(ByVal oCountryIds As ArrayList, ByVal blnMethodOfRepairRLG As Boolean, _
                                         ByVal guidMethodOfRepair As Guid, Optional ByVal blnCheckAcctSetting As Boolean = False) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/GET_SERVICE_CENTER_LIST")
        Dim whereClauseConditions As String = ""
        whereClauseConditions &= Environment.NewLine & " AND " & MiscUtil.BuildListForSql("sc." & Me.COL_NAME_COUNTRY_ID, oCountryIds, False)
        If blnMethodOfRepairRLG Then
            whereClauseConditions &= Environment.NewLine & " AND scmr.serv_center_mor_id =" & MiscUtil.GetDbStringFromGuid(guidMethodOfRepair, True)
        Else 'exclude recovery/legal/general expense
            whereClauseConditions &= Environment.NewLine & " AND scmr.serv_center_mor_id not in (select  li.LIST_ITEM_ID from ELP_LIST l INNER JOIN  ELP_LIST_ITEM li ON l.LIST_ID = li.LIST_ID where L.CODE = 'METHR' and li.code in ('RC', 'G', 'L'))"
        End If
        If blnCheckAcctSetting Then
            whereClauseConditions &= Environment.NewLine & " and ( exists(select null from elp_acct_settings a WHERE sc.service_center_id = a.service_center_id) OR "
            whereClauseConditions &= " exists(select null from elp_acct_settings a WHERE sc.master_center_id = a.service_center_id and not sc.master_center_id is null) ) "

        End If
        selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
        Try
            Dim ds As New DataSet
            ds = DBHelper.Fetch(selectStmt, Me.TABLE_NAME)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function LoadServiceCenterForCountry(ByVal oCountryId As Guid) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/GET_SERVICE_CENTERS_FOR_COUNTRY")
        'Dim whereClauseConditions As String = ""
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() { _
                   New DBHelper.DBHelperParameter("country_id", oCountryId)}

        'If Not oCountryId.Equals(Guid.Empty) Then
        '    whereClauseConditions &= Environment.NewLine & " AND sc.country_id =" & MiscUtil.GetDbStringFromGuid(oCountryId, True)
        'End If
        'selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
        Try
            Dim ds As New DataSet
            DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function GetServiceCenterForWS(ByVal ServiceCenterCode As String, ByVal oCountryId As Guid) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/GET_SERVICE_CENTER_LIST_FOR_WS")

        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() { _
                   New DBHelper.DBHelperParameter("country_id", oCountryId.ToByteArray), _
                   New DBHelper.DBHelperParameter("code", ServiceCenterCode)}
        Try
            Dim ds As New DataSet
            'ds = DBHelper.Fetch(selectStmt, Me.TABLE_NAME)
            DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME_WS, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function GetUserCustCountries(ByVal UserId As Guid, ByVal CountryId As Guid) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/GET_USER_CUST_COUNTRIES")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() { _
                   New DBHelper.DBHelperParameter("user_id", UserId), _
                   New DBHelper.DBHelperParameter("country_id", CountryId)}

        Try
            Dim ds = New DataSet
            DBHelper.Fetch(ds, selectStmt, Me.COUNTRY_TABLE_NAME, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
        Return DBHelper.Fetch(selectStmt, Me.COUNTRY_TABLE_NAME)
    End Function

    Public Function LoadSVCentersBySVNetwork(ByVal svnetworkid As Guid, ByVal CountryId As Guid) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/GET_SERVICE_CENTER_BY_SERVICE_NETWORK")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() { _
                   New DBHelper.DBHelperParameter("sv_network_id", svnetworkid)}
        Dim whereClauseConditions As String = ""

        If Not CountryId.Equals(Guid.Empty) Then
            whereClauseConditions &= Environment.NewLine & " AND sc.country_id =" & MiscUtil.GetDbStringFromGuid(CountryId, True)
        End If
        selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
        Try
            Dim ds = New DataSet
            DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
        Return DBHelper.Fetch(selectStmt, Me.TABLE_NAME)
    End Function

#Region "VendorManagement"

    Public Function GetContactsView(ByVal ServiceCenterId As Guid) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/GET_SERVICE_CENTER_CONTACT_INFO")

        'Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() { _
        '           New DBHelper.DBHelperParameter("Service_center_id", ServiceCenterId.ToByteArray)}
        'Try
        '    Dim ds As New DataSet
        '    DBHelper.Fetch(ds, selectStmt, "elp_contact_info", parameters)
        '    Return ds
        'Catch ex As Exception
        '    Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        'End Try
    End Function

    Public Function GetQuantityView(ByVal ServiceCenterId As Guid) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/GET_SERVICE_CENTER_QUANTITY")

        'Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() { _
        '           New DBHelper.DBHelperParameter("Service_center_id", ServiceCenterId.ToByteArray)}
        'Try
        '    Dim ds As New DataSet
        '    DBHelper.Fetch(ds, selectStmt, "elp_vendor_quantity", parameters)
        '    Return ds
        'Catch ex As Exception
        '    Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        'End Try

        'US 224101 - Replacing code to call stored proc
        'pi_service_center_id        in  elp_service_center.service_center_id%type,
        'po_service_center_table     out sys_refcursor,
        'po_return_code              out number
        Dim parameters() As OracleParameter
        parameters = New OracleParameter() {
                              New OracleParameter(PAR_IN_NAME_SERVICE_CENTER_ID, OracleDbType.Raw, ServiceCenterId.ToByteArray, ParameterDirection.Input),
                              New OracleParameter("po_service_center_table", OracleDbType.RefCursor, ParameterDirection.Output),
                              New OracleParameter(PAR_OUT_NAME_RETURN_CODE, OracleDbType.Int16, ParameterDirection.Output)}

        Return FetchStoredProcedure("GetQuantityView",
                                         selectStmt,
                                         parameters)
    End Function

    Public Function GetScheduleView(ByVal ServiceCenterId As Guid) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/GET_SERVICE_CENTER_SCHEDULE")

        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() { _
                   New DBHelper.DBHelperParameter("Service_center_id", ServiceCenterId.ToByteArray)}
        Try
            Dim ds As New DataSet
            DBHelper.Fetch(ds, selectStmt, "elp_service_schedule", parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function CheckIfServiceCenterIsMaster(ByVal ServiceCenterId As Guid) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/CHECK_IF_SERVICE_CENTER_IS_MASTER_CENTER")

        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() { _
                   New DBHelper.DBHelperParameter("Service_center_id", ServiceCenterId.ToByteArray), _
                   New DBHelper.DBHelperParameter("Service_center_id", ServiceCenterId.ToByteArray)}
        Try
            Dim ds As New DataSet
            DBHelper.Fetch(ds, selectStmt, "SC_IS_MASTER_CENTER", parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function GetAttributeView(ByVal ServiceCenterId As Guid, ByVal LanguageId As Guid) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/LOAD_AVAILABLE_ATRIBUTES_BY_SERVICE_CENTER_ID")

        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() { _
                   New DBHelper.DBHelperParameter("Service_center_id", ServiceCenterId.ToByteArray), _
                   New DBHelper.DBHelperParameter("language_id", LanguageId)}
        Try
            Dim ds As New DataSet
            DBHelper.Fetch(ds, selectStmt, "elp_attribute_value", parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function



#End Region

#Region "Price List"
    Public Function IsPriceListAssignedtoServiceIssue(ByVal PriceListCode As String) As Boolean
        Dim selectStmt As String = Me.Config("/SQL/IsPriceListAssignedToServiceCenter")
        Dim ds As New DataSet
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("price_list_code", PriceListCode)}
        Try
            DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)
            If ds.Tables(Me.TABLE_NAME).Rows.Count > 0 Then Return True Else Return False
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function GetAvailableServiceCenters(ByVal countryId As ArrayList) As DataView
        Dim selectStmt As String = Me.Config("/SQL/LOAD_SERVICE_CENTER_LIST")
        Try
            selectStmt &= MiscUtil.BuildListForSql(" WHERE (price_list_code is null) or (price_list_code = '') and " & ServiceCenterDAL.COL_NAME_COUNTRY_ID, countryId, True)
            Return DBHelper.Fetch(selectStmt, Me.TABLE_NAME).Tables(0).DefaultView

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Sub LoadPriceListServiceCenters(ByVal familyDS As DataSet, ByVal priceListCode As String)

        Try

            Dim selectStmt As String = Me.Config("/SQL/LOAD_PRICE_LIST_LIST")
            Dim parameters() As OracleParameter = New OracleParameter() {New OracleParameter(COL_NAME_PRICE_LIST_CODE, priceListCode.ToString().Trim())}
            DBHelper.Fetch(familyDS, selectStmt, Me.TABLE_NAME, parameters)

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

#End Region

    Public Function GetServicecenterDescBytes(ByVal SVCDescription As String) As Integer
        Dim ds As New DataSet
        Dim result As Integer
        Dim selectStmt As String = Me.Config("/SQL/GET_STRING_BYTES")

        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
        {New DBHelper.DBHelperParameter("svc_desc", SVCDescription)}

        Try
            DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)
            Integer.TryParse(ds.Tables(0).Rows(0)(0).ToString(), result)
            Return result
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    'US 224101 - Common call to stored procedures
    Private Function FetchStoredProcedure(methodName As String, storedProc As String, parameters() As OracleParameter, Optional familyDS As DataSet = Nothing) As DataSet
        Dim tbl As String = Me.TABLE_NAME
        Dim ds As DataSet = If(familyDS Is Nothing, New DataSet(), familyDS)

        ds.Tables.Add(tbl)

        ' Call DBHelper Store Procedure
        Try
            Using connection As New OracleConnection(DBHelper.ConnectString)
                Using cmd As OracleCommand = OracleDbHelper.CreateCommand(storedProc, CommandType.StoredProcedure, connection)
                    cmd.BindByName = True
                    cmd.Parameters.AddRange(parameters)
                    OracleDbHelper.Fetch(cmd, tbl, ds)
                End Using
            End Using
            Dim par = parameters.FirstOrDefault(Function(p As OracleParameter) p.ParameterName.Equals(Me.PAR_OUT_NAME_RETURN_CODE))
            If (Not par Is Nothing AndAlso par.Value = 200) Then
                Throw New ElitaPlusException("ServiceCenter - " + methodName, Common.ErrorCodes.DB_READ_ERROR)
            End If
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

        Return ds
    End Function
End Class



