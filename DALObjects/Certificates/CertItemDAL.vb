'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (11/8/2004)********************


Public Class CertItemDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_CERT_ITEM"
    Public Const TABLE_CERT_ITEM_COV As String = "ELP_CERT_ITEM_COVERAGE"
    Public Const TABLE_KEY_NAME As String = "cert_item_id"
    Public Const TABLE_CERTIFICATES As String = "ELP_CERT"
    Public Const TABLE_NAME_MFG_DEDUCT As String = "ELP_MFG_DEDUCT"

    'Public Const TABLE_USER_ROLES As String = "user_roles"
    Public Const COL_NAME_MFG_DEDUCTIBLE As String = "deductible"
    Public Const COL_NAME_CERT_ITEM_ID As String = "cert_item_id"
    Public Const COL_NAME_CERT_ID As String = "cert_id"
    Public Const COL_NAME_ITEM_NUMBER As String = "item_number"
    Public Const COL_NAME_RISK_TYPE_ID As String = "risk_type_id"
    Public Const COL_NAME_MANUFACTURER_ID As String = "manufacturer_id"
    Public Const COL_NAME_EQUIPMENT_ID As String = "equipment_id"
    Public Const COL_NAME_MAX_REPLACEMENT_COST As String = "max_replacement_cost"
    Public Const COL_NAME_SERIAL_NUMBER As String = "serial_number"
    Public Const COL_NAME_IMEI_NUMBER As String = "imei_number"
    Public Const COL_NAME_MODEL As String = "model"
    Public Const COL_NAME_COMPANY_ID As String = "company_id"
    Public Const COL_NAME_ITEM_DESCRIPTION As String = "item_description"
    Public Const COL_NAME_ITEM_CODE As String = "item_code"
    Public Const COL_NAME_ITEM_RETAIL_PRICE As String = "item_retail_price"
    Public Const COL_NAME_ITEM_REPLACE_RETURN_DATE As String = "item_replace_return_date"
    Public Const COL_NAME_CERT_NUMBER As String = "cert_number"
    Public Const COL_NAME_PRODUCT_CODE As String = "product_code"
    Public Const COL_NAME_ORIGINAL_RETAIL_PRICE As String = "original_retail_price"
    Public Const COL_NAME_MOBILE_TYPE As String = "mobile_type"
    Public Const COL_NAME_FIRST_USE_DATE As String = "first_use_date"
    Public Const COL_NAME_LAST_USE_DATE As String = "last_use_date"
    Public Const COL_NAME_SIM_CARD_NUMBER As String = "sim_card_number"
    Public Const COL_NAME_SKU_NUMBER As String = "sku_number"
    Public Const COL_NAME_EFFECTIVE_DATE As String = "effective_date"
    Public Const COL_NAME_EXPIRATION_DATE As String = "expiration_date"
    Public Const COL_NAME_CERT_PROD_CODE As String = "cert_product_code"
    Public Const COL_NAME_ALLOWED_EVENTS As String = "allowed_events"
    Public Const COL_NAME_MAX_INSURED_AMOUNT As String = "max_insured_amount"
    Public Const COL_NAME_IMEI_UPDATE_SOURCE As String = "imei_update_source"
    Public Const COL_NAME_BENEFIT_STATUS As String = "benefit_status"
    Public Const COL_NAME_INELIGIBILITY_REASON As String = "ineligibility_reason"

    Public Const ROW_COUNT As String = "row_count"

    'Public Const COL_NAME_NETWORK_ID As String = "network_id"

    Public Const PARAM_NAME_SERIAL_NUMBER As String = "serial_number"
    Public Const PARAM_NAME_CERT_NUMBER As String = "cert_number"
    Public Const PARAM_NAME_COMPANY_GROUP_ID As String = "company_group_id"
    Public Const PARAM_NAME_PO_VIN As String = "po_VINEntries"

    Public Const PARAM_NAME_RECON_WRK As String = "p_dealer_recon_wrk"
    Public Const PARAM_NAME_DEALER_ID As String = "p_dealer_id"
    Public Const PARAM_NAME_VAL_SERIAL_NUMBER As String = "p_serial_number"
    Public Const PARAM_NAME_CERT_ITEM_ID As String = "p_cert_item_id"
    Public Const PARAM_NAME_IS_VALID As String = "p_isValid"
    Public Const PARAM_EQUIPMENTID As String = "p_equipmentId"



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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("cert_item_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub
    Public Sub LoadMfgDeductible(familyDS As DataSet, certItemId As Guid, oContractId As Guid)
        Dim selectStmt As String = Config("/SQL/LOAD_MFG_DEDUCTIBLE")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("cert_item_id", certItemId.ToByteArray), _
                                                                                           New DBHelper.DBHelperParameter("contract_id", oContractId.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, TABLE_NAME_MFG_DEDUCT, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub
    Public Function LoadList() As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_LIST")
        Return DBHelper.Fetch(selectStmt, TABLE_NAME)
    End Function

    Public Function LoadList(certid As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_BY_CERT_ID")
        Dim ds As New DataSet
        Dim parameters() As OracleParameter
        parameters = New OracleParameter() _
                                    {New OracleParameter(COL_NAME_CERT_ID, certid.ToByteArray)}

        Return DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
    End Function
    Public Function LoadRegItemsList(certid As Guid, langId As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_REG_ITEMS_BY_CERT_ID")
        Dim ds As New DataSet
        Dim parameters() As OracleParameter
        parameters = New OracleParameter() _
            {New OracleParameter(COL_NAME_LANGUAGE_ID, langId.ToByteArray),
            New OracleParameter(COL_NAME_CERT_ID, certid.ToByteArray)}

        Return DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
    End Function
    Public Function LoadList(certIDs As ArrayList) As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_LIST_BY_CERT_IDS")
        Dim ds As New DataSet

        Dim inClauseCondition As String = MiscUtil.BuildListForSql("and c." & COL_NAME_CERT_ID, certIDs, False)
        selectStmt = selectStmt.Replace(DYNAMIC_IN_CLAUSE_PLACE_HOLDER, inClauseCondition)

        Return DBHelper.Fetch(selectStmt, TABLE_NAME)
    End Function

    Public Function GetMaxItemNumber(certid As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/GET_MAX_ITEM_NUMBER")
        Dim ds As New DataSet
        Dim parameters() As OracleParameter
        parameters = New OracleParameter() _
                                    {New OracleParameter(COL_NAME_CERT_ID, certid.ToByteArray)}

        Return DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
    End Function

    Public Sub LoadAllItemsForCertificate(certId As Guid, familyDataset As DataSet)
        Dim ds As DataSet = familyDataset
        Dim parameters() As OracleParameter
        Dim selectStmt As String = Config("/SQL/LOAD_LIST_FOR_CERTIFICATE")
        parameters = New OracleParameter() {New OracleParameter(COL_NAME_CERT_ID, certId.ToByteArray)}
        DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
    End Sub
#End Region

#Region "Overloaded Methods"
    Public Overloads Sub Update(ds As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing, Optional ByVal changesFilter As DataRowState = Nothing)
        If ds.Tables(TABLE_NAME) IsNot Nothing Then
            MyBase.Update(ds.Tables(TABLE_NAME), Transaction, changesFilter)
        End If
    End Sub
#End Region

#Region "Overloaded Methods Family"
    'This method was added manually to accommodate BO families Save
    Public Overloads Sub UpdateFamily(familyDataset As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing)
        Dim certDAL As New CertificateDAL
        Dim tr As IDbTransaction = Transaction
        If tr Is Nothing Then
            tr = DBHelper.GetNewTransaction
        End If
        Try
            'Updates
            Update(familyDataset, tr, DataRowState.Modified)
            'certDAL.Update(familyDataset, tr, DataRowState.Modified)
            certDAL.UpdateFamily(familyDataset, tr)

            If familyDataset.Tables(ClaimStatusDAL.TABLE_NAME) IsNot Nothing AndAlso familyDataset.Tables(ClaimStatusDAL.TABLE_NAME).Rows.Count > 0 Then
                Dim ClaimStatusDAL As New ClaimStatusDAL
                ClaimStatusDAL.Update(familyDataset, tr, DataRowState.Added Or DataRowState.Modified)
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

    Public Overloads Sub UpdateCertItemAndCov(familyDataset As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing)
        Dim certItemCoverageDAL As New CertItemCoverageDAL
        Dim tr As IDbTransaction = Transaction
        If tr Is Nothing Then
            tr = DBHelper.GetNewTransaction
        End If
        Try
            If familyDataset.Tables(TABLE_NAME) IsNot Nothing Then
                MyBase.Update(familyDataset.Tables(TABLE_NAME), tr, DataRowState.Added)
            End If
            If familyDataset.Tables(TABLE_CERT_ITEM_COV) IsNot Nothing Then
                certItemCoverageDAL.Update(familyDataset.Tables(TABLE_CERT_ITEM_COV), tr, DataRowState.Added)
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

    '#Region "User Methods"

    '    Public Function LoadUserRoles(ByVal networkId As String) As DataSet
    '        Dim ds As New DataSet
    '        Dim parameters() As OracleParameter
    '        Dim selectStmt As String = Me.Config("/SQL/GET_USER_ROLES")

    '        parameters = New OracleParameter() {New OracleParameter(COL_NAME_NETWORK_ID, networkId)}
    '        Return DBHelper.Fetch(ds, selectStmt, Me.TABLE_USER_ROLES, parameters)
    '    End Function

    '#End Region

#Region "Functions"

    Public Function ValidateSerialNumber(SerialNumber As String, CertNumber As String, CompanyGroupId As Guid) As DataSet
        Dim ds As New DataSet
        Dim selectStmt As String = Config("/SQL/VALIDATE_VIN_NUMBER")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
                    {New DBHelper.DBHelperParameter("pi_cert_number", CertNumber),
                     New DBHelper.DBHelperParameter("pi_serial_number", SerialNumber),
                     New DBHelper.DBHelperParameter("pi_company_group_id", DALBase.GuidToSQLString(CompanyGroupId))}

        Dim outputParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {
                            New DBHelper.DBHelperParameter("po_VINEntries", GetType(DataSet))}
        Try
            DBHelper.FetchSp(selectStmt, parameters, outputParameters, ds, TABLE_NAME)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Function IsSerialNumberUnique(dealerID As Guid, certItemID As Guid, serialNumber As String) As Boolean
        Dim selectStmt As String = Config("/SQL/IS_SERIAL_NUMBER_UNIQUE")
        Dim inputParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() { _
                                                              New DBHelper.DBHelperParameter(PARAM_NAME_DEALER_ID, dealerID.ToByteArray), _
                                                              New DBHelper.DBHelperParameter(PARAM_NAME_CERT_ITEM_ID, certItemID.ToByteArray), _
                                                              New DBHelper.DBHelperParameter(PARAM_NAME_VAL_SERIAL_NUMBER, serialNumber)}

        Dim outputParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() { _
                            New DBHelper.DBHelperParameter(PARAM_NAME_IS_VALID, GetType(String))}
        'New DBHelper.DBHelperParameter(PARAM_NAME_RECON_WRK, DBNull.Value), _
        Try
            ' Call DBHelper Store Procedure
            DBHelper.ExecuteSp(selectStmt, inputParameters, outputParameters)

            If CType(outputParameters(0).Value, String).Trim = "N" Then
                Return False
            ElseIf CType(outputParameters(0).Value, String).Trim = "Y" Then
                Return True
            End If
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Function LoadSKUs(equipmentId As Guid, dealerId As Guid) As DataView

        Dim ds As New DataSet
        Dim selectStmt As String = Config("/SQL/LoadSKUs")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
                    {New DBHelper.DBHelperParameter(PARAM_NAME_DEALER_ID, DALBase.GuidToSQLString(dealerId)), _
                     New DBHelper.DBHelperParameter(PARAM_EQUIPMENTID, DALBase.GuidToSQLString(equipmentId))}
        Try
            DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
            If ds IsNot Nothing AndAlso ds.Tables(TABLE_NAME) IsNot Nothing AndAlso ds.Tables(TABLE_NAME).Rows.Count > 0 Then
                Return New DataView(ds.Tables(TABLE_NAME))
            Else
                Return Nothing
            End If
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

#End Region

#Region "Stored Procedures Calls"

    Public Sub ProcessAppleCareEnrolledItem(certItemID As Guid, attValue As String, ByRef strErrMsg As String)
        Dim selectStmt As String = Config("/SQL/PROCESS_APPLECARE_ITEM_ENROLL")

        Using connection As New OracleConnection(DBHelper.ConnectString)
            Using command As OracleCommand = OracleDbHelper.CreateCommand(selectStmt, CommandType.StoredProcedure, connection)
                command.BindByName = True
                command.AddParameter("pi_cert_item_id", OracleDbType.Raw, 16, certItemID.ToByteArray, ParameterDirection.Input)
                command.AddParameter("pi_attribute_value", OracleDbType.Varchar2, 255, attValue, ParameterDirection.Input)
                command.AddParameter("po_error_msg", OracleDbType.Varchar2, 3000, Nothing, ParameterDirection.Output)

                Try
                    connection.Open()
                    command.ExecuteNonQuery()
                    strErrMsg = command.Parameters("po_error_msg").Value.ToString()
                    connection.Close()
                Catch ex As Exception
                    Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
                End Try
            End Using
        End Using
    End Sub

#End Region

End Class


