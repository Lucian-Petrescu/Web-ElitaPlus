'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (4/4/2012)********************


Public Class RepairAndLogisticsDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "REPAIR_AND_LOGISTICS"
    Public Const TABLE_KEY_NAME As String = "id"

    Public Const COL_NAME_CUSTOMER_NAME As String = "customer_name"
    Public Const COL_NAME_COVERAGE_TYPE As String = "coverage_type"
    Public Const COL_NAME_PRODUCT_CODE As String = "product_code"
    Public Const COL_NAME_CLAIM_NUMBER As String = "claim_number"
    Public Const COL_NAME_CLAIM_STATUS As String = "claim_status"
    Public Const COL_NAME_DATE_OF_CLAIM As String = "date_of_claim"
    Public Const COL_NAME_POS As String = "pos"
    Public Const COL_NAME_SERVICE_CENTER As String = "service_center"
    Public Const COL_NAME_CLAIMED_DEVICE_MANUFACTURER As String = "claimed_device_manufacturer"
    Public Const COL_NAME_CLAIMED_DEVICE_MODEL As String = "claimed_device_model"
    Public Const COL_NAME_CLAIMED_DEVICE_SKU As String = "claimed_device_sku"
    Public Const COL_NAME_CLAIMED_DEVICE_SERIAL_NUMBER As String = "claimed_device_serial_number"
    Public Const COL_NAME_CLAIMED_DEVICE_IMEI_NUMBER As String = "claimed_device_imei_number"
    Public Const COL_NAME_PROBLEM_DESCRIPTION As String = "problem_description"
    Public Const COL_NAME_DEVICE_RECEPTION_DATE As String = "device_reception_date"
    Public Const COL_NAME_REPLACEMENT_TYPE As String = "replacement_type"
    Public Const COL_NAME_REPLACED_DEVICE_MANUFACTURER As String = "replaced_device_manufacturer"
    Public Const COL_NAME_REPLACED_DEVICE_MODEL As String = "replaced_device_model"
    Public Const COL_NAME_REPLACED_DEVICE_SERIAL_NUMBER As String = "replaced_device_serial_number"
    Public Const COL_NAME_REPLACED_DEVICE_IMEI_NUMBER As String = "replaced_device_imei_number"
    Public Const COL_NAME_REPLACED_DEVICE_SKU As String = "replaced_device_sku"
    Public Const COL_NAME_LABOR_AMOUNT As String = "labor_amount"
    Public Const COL_NAME_PART_AMOUNT As String = "part_amount"
    Public Const COL_NAME_SERVICE_CHARGE As String = "service_charge"
    Public Const COL_NAME_SHIPPING_AMOUNT As String = "shipping_amount"
    Public Const COL_NAME_AUTHORIZED_AMOUNT As String = "authorized_amount"
    Public Const COL_NAME_SERVICE_LEVEL As String = "service_level"
    Public Const COL_NAME_PROBLEM_FOUND As String = "problem_found"
    Public Const COL_NAME_ID As String = "id"
    Public Const COL_NAME_VERIFICATION_NUMBER As String = "verification_number"
    Public Const COL_NAME_COMPANY As String = "company"
    Public Const COL_NAME_CLAIM_VERIFICATION_NUM_LENGTH As String = "claim_verification_num_length"
    Public Const COL_NAME_USE_CLAIM_AUTHORIZATION_ID As String = "use_claim_authorization_id"



    Public Const COL_NAME_TAX_ID As String = "tax_id"
    Public Const COL_NAME_CELL_PHONE_NUMBER As String = "cell_phone_number"
    Public Const COL_NAME_AUTHORIZATION_NUMBER As String = "authorization_number"
    Public Const COL_NAME_CLAIM_AUTHORIZATION_NUMBER As String = "authorization_number"
    Public Const COL_NAME_SERIAL_NUMBER As String = "serial_number"
    Public Const COL_NAME_SERVICE_CENTER_ID As String = "service_center_id"

#End Region

#Region "Constructors"
    Public Sub New()
        MyBase.new()
    End Sub

#End Region

#Region "Load Methods"

    Public Sub LoadSchema(ds As DataSet)
        Load(ds, Guid.Empty, Guid.Empty)
    End Sub

    Public Sub Load(familyDS As DataSet, id As Guid, languageid As Guid)
        Dim selectStmt As String = Config("/SQL/LOAD")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("language_id1", languageid.ToByteArray), _
        New DBHelper.DBHelperParameter("language_id2", languageid.ToByteArray), _
        New DBHelper.DBHelperParameter("language_id3", languageid.ToByteArray), _
        New DBHelper.DBHelperParameter("claim_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadList(compIds As ArrayList, claimNumber As String, serialNumber As String, _
                             customerName As String, taxID As String, _
                             authorizationNumber As String, cellPhoneNumber As String, sortBy As String, _
                             externalUserServiceCenterIds As ArrayList, serviceCenterIds As ArrayList, claimAuthorizationNumber As String, _
                             dealerId As Guid, Optional ByVal dealerGroupCode As String = "") As DataSet

        Dim selectStmt As String = Config("/SQL/LOAD_LIST_DYNAMIC")

        Dim whereClauseConditions As String = ""

        If FormatSearchMask(claimNumber) Then
            whereClauseConditions &= " AND " & Environment.NewLine & "cview." & COL_NAME_CLAIM_NUMBER & " " & claimNumber
        End If

        If FormatSearchMask(serialNumber) Then
            whereClauseConditions &= " AND " & Environment.NewLine & "UPPER(ce." & COL_NAME_SERIAL_NUMBER & ") " & serialNumber
        End If

        If FormatSearchMask(customerName) Then
            whereClauseConditions &= " AND " & Environment.NewLine & "UPPER(cview.cust_name) " & customerName
        End If

        If FormatSearchMask(taxID) Then
            whereClauseConditions &= " AND " & Environment.NewLine & "UPPER(c.identification_number) " & taxID
        End If

        If (MiscUtil.IsCriteriaSelected(externalUserServiceCenterIds) = True) Then
            whereClauseConditions &= " AND " & Environment.NewLine & MiscUtil.BuildListForSql("COALESCE(cview." & COL_NAME_SERVICE_CENTER_ID & ", ca." & COL_NAME_SERVICE_CENTER_ID & ")", externalUserServiceCenterIds)
        End If


        If (MiscUtil.IsCriteriaSelected(serviceCenterIds) = True) Then


            whereClauseConditions &= " AND " & Environment.NewLine & MiscUtil.BuildListForSql("COALESCE(cview." & COL_NAME_SERVICE_CENTER_ID & ", ca." & COL_NAME_SERVICE_CENTER_ID & ")", serviceCenterIds)
        End If


        If FormatSearchMask(authorizationNumber) Then
            whereClauseConditions &= " AND " & Environment.NewLine & "UPPER(cview." & COL_NAME_AUTHORIZATION_NUMBER & ") " & authorizationNumber.ToUpper
        End If

        If FormatSearchMask(claimAuthorizationNumber) Then
            whereClauseConditions &= " AND " & Environment.NewLine & "UPPER(ca." & COL_NAME_CLAIM_AUTHORIZATION_NUMBER & ") " & claimAuthorizationNumber.ToUpper
        End If


        If FormatSearchMask(cellPhoneNumber) Then
            whereClauseConditions &= " AND " & Environment.NewLine & "cview.cust_work_phone " & cellPhoneNumber
        End If


        'if dealer id present
        If Not ((dealerId = Guid.Empty) OrElse (dealerId = Nothing)) Then
            whereClauseConditions &= Environment.NewLine & " AND cview.dealer_id = hextoraw(" & MiscUtil.GetDbStringFromGuid(dealerId) & ")"
        End If

        If (dealerGroupCode <> String.Empty AndAlso (FormatSearchMask(dealerGroupCode))) Then
            whereClauseConditions &= Environment.NewLine & "AND UPPER(dg.code) " & dealerGroupCode.ToUpper & ""
        End If

        ' not HextoRaw
        whereClauseConditions &= Environment.NewLine & " AND " & MiscUtil.BuildListForSql("cview.company_id", compIds, True)

        selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)

        If Not IsNothing(sortBy) Then
            selectStmt = selectStmt.Replace(DYNAMIC_ORDER_BY_CLAUSE_PLACE_HOLDER, _
                                            Environment.NewLine & "ORDER BY " & Environment.NewLine & sortBy)
        Else
            selectStmt = selectStmt.Replace(DYNAMIC_ORDER_BY_CLAUSE_PLACE_HOLDER, "")
        End If

        Try
            Dim ds As New DataSet
            Dim rowNum As New DBHelper.DBHelperParameter(PAR_NAME_ROW_NUMBER, MAX_NUMBER_OF_ROWS)

            DBHelper.Fetch(ds, selectStmt, TABLE_NAME, _
                            New DBHelper.DBHelperParameter() {rowNum})
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Function GetReplacementParts(claimId As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/REPLACEMENT_PARTS")
        Dim parameters() As OracleParameter = New OracleParameter() _
                                           {New OracleParameter("claim_id", claimId.ToByteArray)}

        Try

            Return DBHelper.Fetch(selectStmt, "REPLACEMENT_PARTS", "REPLACEMENT_PARTS", parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Function GetServiceCenters(claimId As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/SERVICE_CENTERS")
        Dim parameters() As OracleParameter = New OracleParameter() _
                                           {New OracleParameter("claim_id", claimId.ToByteArray)}

        Try

            Return DBHelper.Fetch(selectStmt, "SERVICE_CENTERS", "SERVICE_CENTERS", parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function
    Public Function UpdateVerificationNumber(strVerificationNumber As String, claimId As Guid) As Boolean
        Dim sqlStmt As String
        Dim parameters() As DBHelper.DBHelperParameter

        sqlStmt = Config("/SQL/UPDATE_VERIFICATION_NUMBER")
        parameters = New DBHelper.DBHelperParameter() { _
                        New DBHelper.DBHelperParameter("VERIFICATION_NUMBER", strVerificationNumber), _
                        New DBHelper.DBHelperParameter("CLAIM_ID", claimId.ToByteArray)}

        Try

            DBHelper.ExecuteWithParam(sqlStmt, parameters)
            Return True

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



