'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (2/21/2013)********************


Public Class ClaimAuthorizationDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_CLAIM_AUTHORIZATION"
    Public Const TABLE_KEY_NAME As String = "claim_authorization_id"

    Public Const COL_NAME_CLAIM_AUTHORIZATION_ID As String = "claim_authorization_id"
    Public Const COL_NAME_CLAIM_ID As String = "claim_id"
    Public Const COL_NAME_AUTHORIZATION_NUMBER As String = "authorization_number"
    Public Const COL_NAME_SERVICE_CENTER_ID As String = "service_center_id"
    Public Const COL_NAME_SERVICE_LEVEL_ID As String = "service_level_id"
    Public Const COL_NAME_CLAIM_AUTHORIZATION_STATUS_ID As String = "claim_authorization_status_id"
    Public Const COL_NAME_SPECIAL_INSTRUCTION As String = "special_instruction"
    Public Const COL_NAME_VISIT_DATE As String = "visit_date"
    Public Const COL_NAME_DEVICE_RECEPTION_DATE As String = "device_reception_date"
    Public Const COL_NAME_EXPECTED_REPAIR_DATE As String = "expected_repair_date"
    Public Const COL_NAME_REPAIR_DATE As String = "repair_date"
    Public Const COL_NAME_PICK_UP_DATE As String = "pick_up_date"
    Public Const COL_NAME_DELIVERY_DATE As String = "delivery_date"
    Public Const COL_NAME_WHO_PAYS_ID As String = "who_pays_id"
    Public Const COL_NAME_DEFECT_REASON As String = "defect_reason"
    Public Const COL_NAME_TECHNICAL_REPORT As String = "technical_report"
    Public Const COL_NAME_BATCH_NUMBER As String = "batch_number"
    Public Const COL_NAME_SVC_REFERENCE_NUMBER As String = "svc_reference_number"
    Public Const COL_NAME_EXTERNAL_CREATED_DATE As String = "external_created_date"
    Public Const COL_NAME_IS_SPECIAL_SERVICE_ID As String = "is_special_service_id"
    Public Const COL_NAME_REVERSE_LOGISTICS_ID As String = "reverse_logistics_id"
    Public Const COL_NAME_PROBLEM_FOUND As String = "problem_found"
    Public Const COL_NAME_SOURCE As String = "source"
    Public Const COL_NAME_CONTAINS_DEDUCTIBLE_ID = "contains_deductible_id"
    Public Const COL_NAME_AUTH_FULFILLMENT_TYPE_XCD As String = "auth_fulfillment_type_xcd"
    Public Const COL_NAME_CASH_PYMT_METHOD_XCD As String = "cash_pymt_method_xcd"
    Public Const COL_NAME_AUTH_TYPE_XCD As String = "auth_type_xcd"
    Public Const COL_NAME_PARTY_TYPE_XCD As String = "party_type_xcd"
    Public Const COL_NAME_PARTY_REFERENCE_ID As String = "party_reference_id"
    Public Const COL_NAME_REFUND_METHOD_XCD As String = "refund_method_xcd"

    Public Const PAR_NAME_COMPANY As String = "p_company_id"
    Public Const PAR_NAME_COVERAGE_CODE_INPUT As String = "p_coverage_code_input"
    Public Const PAR_NAME_UNIT_NUMBER_INPUT As String = "p_unit_number_input"
    Public Const PAR_NAME_CLAIM_AUTH_NUMBER As String = "p_claim_auth_number"
    Public Const PAR_NAME_RETURN As String = "p_return"
    Public Const PAR_NAME_EXCEPTION_MSG As String = "p_exception_msg"
    Public Const COL_NAME_VERIFICATION_NUMBER As String = "verification_number"

    'US 224101 
    Public Const PAR_OUT_NAME_RETURN_CODE As String = "po_return_code"

    Public Const PAR_IN_NAME_SERVICE_CENTER_ID As String = "pi_service_center_id"
    Public Const PAR_IN_NAME_PRICE_LIST_DETAIL_ID As String = "pi_price_list_detail_id"
    Public Const PAR_IN_NAME_LANGUAGE_ID As String = "pi_language_id"
    Public Const PAR_IN_NAME_SERVICE_CLASS_ID As String = "pi_service_class_id"
    Public Const PAR_IN_NAME_SERVICE_TYPE_ID As String = "pi_service_type_id"
    Public Const PAR_IN_NAME_SERVICE_LEVEL_ID As String = "pi_service_level_id"
    Public Const PAR_IN_NAME_EFFECTIVE As String = "pi_effective"
    Public Const PAR_IN_NAME_RISK_TYPE_ID As String = "pi_risk_type_id"
    Public Const PAR_IN_NAME_EQUIPMENT_ID As String = "pi_equipment_id"
    Public Const PAR_IN_NAME_EQUIPMENT_CLASS_ID As String = "pi_equipment_class_id"
    Public Const PAR_IN_NAME_CONDITION_ID As String = "pi_condition_id"
    Public Const PAR_IN_NAME_VENDOR_SKU As String = "pi_vendor_sku"
    Public Const PAR_IN_NAME_VENDOR_SKU_DESCRIPTION As String = "pi_vendor_sku_description"
    Public Const PAR_IN_NAME_LOOKUP_DATE As String = "pi_lookup_date"
    Public Const PAR_IN_ROW_NUM As String = "pi_row_num"
    'KDDI Changes
    Public Const COL_SUB_STATUS_REASON As String = "sub_status_reason"
    Public Const COL_LINKED_CLAIM_AUTH_ID As String = "linked_claim_auth_id"
    Public Const COL_NAME_SUB_STATUS_XCD As String = "sub_status_xcd"
    Public Const COL_NAME_AUTHORIZAION_AMOUNT As String = "authorization_amount"
    Public Const COL_NAME_SERVICE_ORDER_TYPE As String = "service_order_type"
    Public Const COL_NAME_LOCATOR As String = "locator"

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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("claim_authorization_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, Me.TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadList(ByVal familyDS As DataSet, ByVal claimId As Guid) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST")
        Dim dynamicWhereCondition As String

        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("claim_id", claimId.ToByteArray)}
        Try
            dynamicWhereCondition = familyDS.ToLoadExclusionClause("ca", Me.TABLE_KEY_NAME, Me.TABLE_NAME, Me.COL_NAME_CLAIM_ID, claimId, parameters)
            selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, dynamicWhereCondition)
            DBHelper.Fetch(familyDS, selectStmt, Me.TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function LoadListByInvoiceId(ByVal familyDS As DataSet, ByVal invoiceId As Guid) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST_BY_INVOICE_ID")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("invoice_id", invoiceId.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, Me.TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function LoadPriceListDetails(ByVal serviceCenterId As Guid, _
                                         ByVal lookupDate As Date, _
                                         ByVal languageId As Guid, _
                                         ByVal serviceClassId As Guid, _
                                         ByVal serviceTypeId As Guid, _
                                         ByVal riskTypeId As Guid, _
                                         ByVal equipmentClassId As Guid, _
                                         ByVal equipmentId As Guid, _
                                         ByVal conditionId As Guid, _
                                         ByVal sku As String, _
                                         ByVal skuDescription As String, _
                                         Optional ByVal LimitResultset As Integer = 100) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/LOAD_PRICE_LIST_DETAILS")
        'Dim whereClauseConditions As String = ""
        'Dim ds As New DataSet
        'Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
        '    {New DBHelper.DBHelperParameter(LanguageDAL.COL_NAME_LANGUAGE_ID, languageId.ToByteArray), _
        '     New DBHelper.DBHelperParameter(LanguageDAL.COL_NAME_LANGUAGE_ID, languageId.ToByteArray), _
        '     New DBHelper.DBHelperParameter(LanguageDAL.COL_NAME_LANGUAGE_ID, languageId.ToByteArray), _
        '     New DBHelper.DBHelperParameter(LanguageDAL.COL_NAME_LANGUAGE_ID, languageId.ToByteArray), _
        '     New DBHelper.DBHelperParameter(ServiceCenterDAL.COL_NAME_SERVICE_CENTER_ID, serviceCenterId.ToByteArray), _
        '     New DBHelper.DBHelperParameter("lookup_date", lookupDate), _
        '     New DBHelper.DBHelperParameter("lookup_date", lookupDate), _
        '     New DBHelper.DBHelperParameter(PAR_NAME_ROW_NUMBER, LimitResultset)}
        'Try
        '    If (Not serviceClassId.Equals(Guid.Empty)) Then
        '        whereClauseConditions &= Environment.NewLine & " and elp_price_list_detail.service_class_id = hextoraw(:" & PriceListDetailDAL.COL_NAME_SERVICE_CLASS_ID & ")"
        '        ReDim Preserve parameters(parameters.Count())
        '        parameters(parameters.Count() - 1) = New DBHelper.DBHelperParameter(PriceListDetailDAL.COL_NAME_SERVICE_CLASS_ID, serviceClassId.ToByteArray())
        '    End If

        '    If (Not serviceTypeId.Equals(Guid.Empty)) Then
        '        whereClauseConditions &= Environment.NewLine & " and elp_price_list_detail.service_type_id = hextoraw(:" & PriceListDetailDAL.COL_NAME_SERVICE_TYPE_ID & ")"
        '        ReDim Preserve parameters(parameters.Count())
        '        parameters(parameters.Count() - 1) = New DBHelper.DBHelperParameter(PriceListDetailDAL.COL_NAME_SERVICE_TYPE_ID, serviceTypeId.ToByteArray())
        '    End If

        '    If (Not riskTypeId.Equals(Guid.Empty)) Then
        '        whereClauseConditions &= Environment.NewLine & " and elp_price_list_detail.risk_type_id = hextoraw(:" & PriceListDetailDAL.COL_NAME_RISK_TYPE_ID & ")"
        '        ReDim Preserve parameters(parameters.Count())
        '        parameters(parameters.Count() - 1) = New DBHelper.DBHelperParameter(PriceListDetailDAL.COL_NAME_RISK_TYPE_ID, riskTypeId.ToByteArray())
        '    End If

        '    If (Not equipmentClassId.Equals(Guid.Empty)) Then
        '        whereClauseConditions &= Environment.NewLine & " and elp_price_list_detail.equipment_class_id = hextoraw(:" & PriceListDetailDAL.COL_NAME_EQUIPMENT_CLASS_ID & ")"
        '        ReDim Preserve parameters(parameters.Count())
        '        parameters(parameters.Count() - 1) = New DBHelper.DBHelperParameter(PriceListDetailDAL.COL_NAME_EQUIPMENT_CLASS_ID, equipmentClassId.ToByteArray())
        '    End If

        '    If (Not equipmentId.Equals(Guid.Empty)) Then
        '        whereClauseConditions &= Environment.NewLine & " and elp_price_list_detail.equipment_id = hextoraw(:" & PriceListDetailDAL.COL_NAME_EQUIPMENT_ID & ")"
        '        ReDim Preserve parameters(parameters.Count())
        '        parameters(parameters.Count() - 1) = New DBHelper.DBHelperParameter(PriceListDetailDAL.COL_NAME_EQUIPMENT_ID, equipmentId.ToByteArray())
        '    End If

        '    If (Not conditionId.Equals(Guid.Empty)) Then
        '        whereClauseConditions &= Environment.NewLine & " and elp_price_list_detail.condition_id = hextoraw(:" & PriceListDetailDAL.COL_NAME_CONDITION_ID & ")"
        '        ReDim Preserve parameters(parameters.Count())
        '        parameters(parameters.Count() - 1) = New DBHelper.DBHelperParameter(PriceListDetailDAL.COL_NAME_CONDITION_ID, conditionId.ToByteArray())
        '    End If

        '    If ((Not (sku Is Nothing)) AndAlso (Me.FormatSearchMask(sku))) Then
        '        whereClauseConditions &= Environment.NewLine & " and upper(elp_price_list_detail." & PriceListDetailDAL.COL_NAME_VENDOR_SKU & ")" & sku.ToUpper
        '    End If

        '    If ((Not (skuDescription Is Nothing)) AndAlso (Me.FormatSearchMask(skuDescription))) Then
        '        whereClauseConditions &= Environment.NewLine & " and upper(elp_price_list_detail." & PriceListDetailDAL.COL_NAME_VENDOR_SKU_DESCRIPTION & ")" & skuDescription.ToUpper
        '    End If

        '    If Not whereClauseConditions = "" Then
        '        selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
        '    Else
        '        selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, "")
        '    End If

        '    DBHelper.Fetch(ds, selectStmt, PriceListDetailDAL.TABLE_NAME, parameters)
        '    Return ds
        'Catch ex As Exception
        '    Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        'End Try

        'US 224101 - Replacing code to call stored proc
        'pi_lookup_date              in  date,
        'pi_language_id              In  elp_language.language_id%type,
        'pi_service_center_id        in  elp_service_center.service_center_id%type,
        'pi_row_num                  In  number Default 100,
        'pi_service_class_id         in  raw default Null,
        'pi_service_type_id          In  raw Default Null,
        'pi_equipment_class_id       in  raw default Null,
        'pi_equipment_id             In  elp_equipment.equipment_id%type Default Null,
        'pi_condition_id             in  raw default Null,
        'pi_risk_type_id             In  raw Default Null,
        'pi_vendor_sku               in  elp_price_list_detail.vendor_sku%type default null,
        'pi_vendor_sku_description   In  elp_price_list_detail.vendor_sku_description%type Default null,
        'po_price_list_detail_table  out sys_refcursor,
        'po_return_code              out number

        Dim parameters() As OracleParameter
        parameters = New OracleParameter() {
                              New OracleParameter(PAR_IN_NAME_LOOKUP_DATE, OracleDbType.Date, lookupDate, ParameterDirection.Input),
                              New OracleParameter(PAR_IN_NAME_LANGUAGE_ID, OracleDbType.Raw, languageId.ToByteArray, ParameterDirection.Input),
                              New OracleParameter(PAR_IN_NAME_SERVICE_CENTER_ID, OracleDbType.Raw, serviceCenterId.ToByteArray, ParameterDirection.Input),
                              New OracleParameter(PAR_IN_ROW_NUM, OracleDbType.Int16, LimitResultset, ParameterDirection.Input),
                              New OracleParameter(PAR_IN_NAME_SERVICE_CLASS_ID, OracleDbType.Raw, If(IsNothing(serviceClassId) OrElse serviceClassId.Equals(Guid.Empty), Nothing, serviceClassId.ToByteArray), ParameterDirection.Input),
                              New OracleParameter(PAR_IN_NAME_SERVICE_TYPE_ID, OracleDbType.Raw, If(IsNothing(serviceTypeId) OrElse serviceTypeId.Equals(Guid.Empty), Nothing, serviceTypeId.ToByteArray), ParameterDirection.Input),
                              New OracleParameter(PAR_IN_NAME_EQUIPMENT_CLASS_ID, OracleDbType.Raw, If(IsNothing(equipmentClassId) OrElse equipmentClassId.Equals(Guid.Empty), Nothing, equipmentClassId.ToByteArray), ParameterDirection.Input),
                              New OracleParameter(PAR_IN_NAME_EQUIPMENT_ID, OracleDbType.Raw, If(IsNothing(equipmentId) OrElse equipmentId.Equals(Guid.Empty), Nothing, equipmentId.ToByteArray), ParameterDirection.Input),
                              New OracleParameter(PAR_IN_NAME_CONDITION_ID, OracleDbType.Raw, If(IsNothing(conditionId) OrElse conditionId.Equals(Guid.Empty), Nothing, conditionId.ToByteArray), ParameterDirection.Input),
                              New OracleParameter(PAR_IN_NAME_RISK_TYPE_ID, OracleDbType.Raw, If(IsNothing(riskTypeId) OrElse riskTypeId.Equals(Guid.Empty), Nothing, riskTypeId.ToByteArray), ParameterDirection.Input),
                              New OracleParameter(PAR_IN_NAME_VENDOR_SKU, OracleDbType.Varchar2, If(Not String.IsNullOrEmpty(sku), sku.ToUpper, Nothing), ParameterDirection.Input),
                              New OracleParameter(PAR_IN_NAME_VENDOR_SKU_DESCRIPTION, OracleDbType.Varchar2, If(Not String.IsNullOrEmpty(skuDescription), skuDescription.ToUpper, Nothing), ParameterDirection.Input),
                              New OracleParameter("po_price_list_detail_table", OracleDbType.RefCursor, ParameterDirection.Output),
                              New OracleParameter(PAR_OUT_NAME_RETURN_CODE, OracleDbType.Int16, ParameterDirection.Output)}

        Return FetchStoredProcedure("LoadPriceListDetails",
                                         selectStmt,
                                         parameters)
    End Function
#End Region

#Region "Properties"

    Public ReadOnly Property IsNew(ByVal Row As DataRow) As Boolean
        Get
            Return (Row.RowState = DataRowState.Added)
        End Get
    End Property

    Public Property ClaimAuthNumber(ByVal Row As DataRow) As String
        Get
            If Row(ClaimAuthorizationDAL.COL_NAME_AUTHORIZATION_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimAuthorizationDAL.COL_NAME_AUTHORIZATION_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            Me.SetValue(Row, ClaimAuthorizationDAL.COL_NAME_AUTHORIZATION_NUMBER, Value)
        End Set
    End Property

#End Region

#Region "Overloaded Methods"
    Public Overloads Sub Update(ByVal ds As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing, Optional ByVal changesFilter As DataRowState = Nothing)
        If ds Is Nothing Then
            Return
        End If
        If Not ds.Tables(Me.TABLE_NAME) Is Nothing AndAlso ds.Tables(Me.TABLE_NAME).Rows.Count > 0 Then
            MyBase.Update(ds.Tables(Me.TABLE_NAME), Transaction, changesFilter)
        End If
    End Sub

    Public Overloads Sub UpdateFamily(ByVal familyDataset As DataSet, ByVal companyId As Guid, Optional ByVal Transaction As IDbTransaction = Nothing)
        Dim claimAuthItemDAL As New ClaimAuthItemDAL
        Dim tr As IDbTransaction = Transaction
        If tr Is Nothing Then
            tr = DBHelper.GetNewTransaction
        End If

        Try
            ObtainAndAssignClaimAuthNumber(familyDataset, companyId)
            MyBase.Update(familyDataset.Tables(Me.TABLE_NAME), tr, DataRowState.Added Or DataRowState.Modified)
            claimAuthItemDAL.Update(familyDataset, tr, DataRowState.Added Or DataRowState.Modified Or DataRowState.Deleted)

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

#Region "Private Methods"
    Private Sub ObtainAndAssignClaimAuthNumber(ByVal familyDataset As DataSet, ByVal companyId As Guid)
        Dim dal As New ClaimAuthorizationDAL
        Dim oRow As DataRow

        For Each oRow In familyDataset.Tables(Me.TABLE_NAME).Rows
            If Me.IsNew(oRow) Then
                Me.ClaimAuthNumber(oRow) = dal.GetClaimAuthNumber(companyId)
            End If
        Next
    End Sub

    Protected Sub SetValue(ByVal Row As DataRow, ByVal columnName As String, ByVal newValue As Object)
        If Not newValue Is Nothing And Row(columnName) Is DBNull.Value Then
            'new value is something and old value is DBNULL
            If newValue.GetType Is GetType(BooleanType) Then
                '- BooleanType, special case - convert to string Y or N
                If CType(newValue, BooleanType).Value Then
                    Row(columnName) = "Y"
                Else
                    Row(columnName) = "N"
                End If
            ElseIf newValue.GetType Is GetType(Guid) Then
                'ElseIf newValue.GetType Is GetType(Guid) Then
                If Not newValue.Equals(Guid.Empty) Then
                    Row(columnName) = CType(newValue, Guid).ToByteArray
                End If
            ElseIf newValue.GetType Is GetType(Byte()) Then
                Row(columnName) = CType(newValue, Byte())
            ElseIf newValue.GetType Is GetType(DateType) Then
                Row(columnName) = CType(newValue.ToString, DateTime)
            ElseIf newValue.GetType Is GetType(Double) Then
                Row(columnName) = CType(newValue, Double)
            ElseIf newValue.GetType Is GetType(Decimal) Then
                Row(columnName) = CType(newValue, Decimal)
            Else
                '- DateType, DecimalType, etc... all our other custome types
                '- see if 'newValue Type' has a Value property (only our custom types do)
                Dim propInfo As System.Reflection.PropertyInfo = newValue.GetType.GetProperty("Value")
                If Not (propInfo Is Nothing) Then
                    '- call the Value property to extract the native .NET type (double, decimal, etc...)
                    newValue = propInfo.GetValue(newValue, Nothing)
                End If

                '- let the DataColumn convert the value to its internal data type
                Row(columnName) = newValue
            End If
        ElseIf Not newValue Is Nothing Then
            'new value is something and old value is also something
            '- convert current value to a string
            Dim currentValue As Object = Row(columnName)
            If newValue.GetType Is GetType(Guid) Then
                currentValue = New Guid(CType(currentValue, Byte()))
            ElseIf newValue.GetType Is GetType(Byte()) Then
                currentValue = CType(currentValue, Byte())
            Else
                currentValue = currentValue.ToString
                '- create an array of types containing one type, the String type
                Dim types() As Type = {GetType(String)}
                '- see if the 'newValue Type' has a 'Parse(String)' method taking a String parameter
                Dim miMethodInfo As System.Reflection.MethodInfo = newValue.GetType.GetMethod("Parse", types)
                If Not miMethodInfo Is Nothing Then
                    '- it does have a Parse method, newValue must be a number type.
                    '- extract the current value as a string
                    Dim args() As Object = {Row(columnName).ToString}
                    '- call the Parse method to convert the currentValue string to a number
                    currentValue = miMethodInfo.Invoke(newValue, args)
                End If
            End If
            '- only dirty the BO if new value is different from old value
            If Not newValue.Equals(currentValue) Then
                If newValue.GetType Is GetType(BooleanType) Then
                    '- BooleanType, special case - convert to string Y or N
                    If CType(newValue, BooleanType).Value Then
                        newValue = "Y"
                    Else
                        newValue = "N"
                    End If
                ElseIf newValue.GetType Is GetType(Byte()) Then
                    newValue = CType(newValue, Byte())
                Else
                    '- DateType, DecimalType, etc... all our other custome types
                    '- see if 'newValue Type' has a Value property (only our custom types do)
                    Dim propInfo As System.Reflection.PropertyInfo = newValue.GetType.GetProperty("Value")
                    If Not (propInfo Is Nothing) Then
                        '- call the Value property to extract the native .NET type (double, decimal, etc...)
                        newValue = propInfo.GetValue(newValue, Nothing)
                    End If
                End If
                '- at this point, newValue has a native .NET type
                If newValue.GetType Is GetType(Guid) Then
                    If newValue.Equals(Guid.Empty) Then
                        newValue = DBNull.Value
                    Else
                        newValue = CType(newValue, Guid).ToByteArray
                    End If
                End If
                Row(columnName) = newValue
            End If
        ElseIf newValue Is Nothing And Not Row(columnName) Is DBNull.Value Then
            Row(columnName) = DBNull.Value
        End If
    End Sub

    Private Function GetClaimAuthNumber(ByVal companyId As Guid) As String
        Dim selectStmt As String = Me.Config("/SQL/GET_NEXT_CLAIM_AUTH_NUMBER_SP")
        Dim inputParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {
                            New DBHelper.DBHelperParameter(Me.PAR_NAME_COMPANY, companyId.ToByteArray)}

        Dim outputParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {
                            New DBHelper.DBHelperParameter(Me.PAR_NAME_CLAIM_AUTH_NUMBER, GetType(String)),
                            New DBHelper.DBHelperParameter(Me.PAR_NAME_RETURN, GetType(Integer)),
                            New DBHelper.DBHelperParameter(Me.PAR_NAME_EXCEPTION_MSG, GetType(String), 50)}

        ' Call DBHelper Store Procedure
        DBHelper.ExecuteSp(selectStmt, inputParameters, outputParameters)
        If CType(outputParameters(2).Value, Integer) <> 0 Then
            If CType(outputParameters(2).Value, Integer) = 300 Then
                Throw New StoredProcedureGeneratedException("Claim Auth Insert Generated Error: ", outputParameters(PAR_NAME_EXCEPTION_MSG).Value)
            Else
                Dim e As New ApplicationException("Return Value = " & outputParameters(1).Value)
                Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, e)
            End If
        Else
            Return CType(outputParameters(0).Value, String)
        End If
    End Function

    'US 224101 - Common call to stored procedures
    Private Function FetchStoredProcedure(methodName As String, storedProc As String, parameters() As OracleParameter, Optional familyDS As DataSet = Nothing) As DataSet

        Dim ds As DataSet = If(familyDS Is Nothing, New DataSet(), familyDS)
        Dim tbl As String = Me.TABLE_NAME

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
                Throw New ElitaPlusException("ClaimAuthorization - " + methodName, Common.ErrorCodes.DB_READ_ERROR)
            End If
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

        Return ds
    End Function

#End Region

#Region "Public Methods"
    Public Function IsAddedToInvoiceGroup(ByVal claimAuthorizationId As Guid) As Boolean
        Dim selectStmt As String = Me.Config("/SQL/ADDED_TO_INVOICE_GROUP")
        Dim returnValue As Integer
        Dim returnObject As Object

        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("claim_authorization_id", claimAuthorizationId.ToByteArray)}
        Try
            returnObject = DBHelper.ExecuteScalar(selectStmt, parameters)
            If (returnObject Is Nothing) Then
                Return False
            Else
                returnValue = DirectCast(returnObject, Decimal)
                If (returnValue = 0) Then
                    Return False
                Else
                    Return True
                End If
            End If
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function CancelShipmentRequest(ByVal claimAuthorizationId As Guid) As Boolean
        Dim selectStmt As String = Me.Config("/SQL/CANCEL_SHIPMENT_REQUEST")
        Dim requestSuccess As Boolean = False
        Try
            Dim inputParameters(0) As DBHelper.DBHelperParameter
            Dim outputParameter(0) As DBHelper.DBHelperParameter
            inputParameters(0) = New DBHelper.DBHelperParameter("pi_claim_authorization_id", claimAuthorizationId.ToByteArray)
            outputParameter(0) = New DBHelper.DBHelperParameter("po_status_return", GetType(String), 32)
            ' Call DBHelper Store Procedure
            DBHelper.ExecuteSp(selectStmt, inputParameters, outputParameter)
            If (Not outputParameter(0).Value Is Nothing) Then
                If (outputParameter(0).Value = "Y") Then
                    requestSuccess = True
                End If
            End If
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
        Return requestSuccess
    End Function

    Public Function ReShipmentProcessRequest(ByVal claimAuthorizationId As Guid, ByVal cancelStatusReason As String) As Boolean
        Dim selectStmt As String = Me.Config("/SQL/RESHIPMENT")
        Dim requestSuccess As Boolean = False
        Try
            Dim inputParameters(1) As DBHelper.DBHelperParameter
            Dim outputParameter(0) As DBHelper.DBHelperParameter
            inputParameters(0) = New DBHelper.DBHelperParameter("pi_claim_authorization_id", claimAuthorizationId.ToByteArray)
            inputParameters(1) = New DBHelper.DBHelperParameter("pi_sub_status_reason", cancelStatusReason)
            outputParameter(0) = New DBHelper.DBHelperParameter("po_status_return", GetType(String), 32)
            ' Call DBHelper Store Procedure
            DBHelper.ExecuteSp(selectStmt, inputParameters, outputParameter)
            If (Not outputParameter(0).Value Is Nothing) Then
                If (outputParameter(0).Value = "Y") Then
                    requestSuccess = True
                End If
            End If
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
        Return requestSuccess
    End Function
     Public Function ManualCashpayRequest(ByVal claimAuthorizationId As Guid, ByVal bankInfoId As Guid, byref errCode as integer, byref errMsg as string) As Boolean
        Dim selectStmt As String = Me.Config("/SQL/MANUALCASHPAY")
        Dim requestSuccess As Boolean = False
        Try
            Dim inputParameters(1) As DBHelper.DBHelperParameter
            Dim outputParameter(1) As DBHelper.DBHelperParameter
            inputParameters(0) = New DBHelper.DBHelperParameter("pi_auth_id", claimAuthorizationId.ToByteArray)
            inputParameters(1) = New DBHelper.DBHelperParameter("pi_bank_info_id", bankInfoId.ToByteArray)

            outputParameter(0) = New DBHelper.DBHelperParameter("po_return_code", GetType(Int32))
            outputParameter(1) = New DBHelper.DBHelperParameter("po_error_msg", GetType(String), 2000)
            ' Call DBHelper Store Procedure
            DBHelper.ExecuteSp(selectStmt, inputParameters, outputParameter)
            If (Not outputParameter(0).Value Is Nothing) Then
                errCode = outputParameter(0).Value
                errMsg = outputParameter(1).Value
                If errCode = 0 Then
                    requestSuccess = True
                End If
            End If
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
        Return requestSuccess
    End Function

    Public Function CheckLinkedAuthItem(ByVal claimAuthorizationId As Guid) As Boolean
        Dim selectStmt As String = Me.Config("/SQL/CHECK_LINKED_AUTH_ITEM")
        Dim requestSuccess As Boolean = False
        Try
            Dim inputParameters(0) As DBHelper.DBHelperParameter
            Dim outputParameter(0) As DBHelper.DBHelperParameter
            inputParameters(0) = New DBHelper.DBHelperParameter("pi_claim_authorization_id", claimAuthorizationId.ToByteArray)
            outputParameter(0) = New DBHelper.DBHelperParameter("po_status_return", GetType(String), 32)
            ' Call DBHelper Store Procedure
            DBHelper.ExecuteSp(selectStmt, inputParameters, outputParameter)
            If (Not outputParameter(0).Value Is Nothing) Then
                If (outputParameter(0).Value = "Y") Then
                    requestSuccess = True
                End If
            End If
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
        Return requestSuccess
    End Function

    Public Function RefundFee(ByVal claimAuthorizationId As Guid, byval refundReasonId As Guid,  byval claimAuthItemId As Guid, byref errCode as integer, byref errMsg as string) As Boolean

        Dim selectStmt As String = Me.Config("/SQL/REFUND_FEE")
        Dim requestSuccess As Boolean = False
        Try
            Dim inputParameters(2) As DBHelper.DBHelperParameter
            Dim outputParameter(1) As DBHelper.DBHelperParameter
            inputParameters(0) = New DBHelper.DBHelperParameter("pi_claim_authorization_id", claimAuthorizationId.ToByteArray)
            inputParameters(1) = New DBHelper.DBHelperParameter("pi_refund_reason_id", refundReasonId.ToByteArray)
            inputParameters(2) = New DBHelper.DBHelperParameter("pi_claim_auth_item_id", claimAuthItemId.ToByteArray)
            

            outputParameter(0) = New DBHelper.DBHelperParameter("po_return_code", GetType(Int32))
            outputParameter(1) = New DBHelper.DBHelperParameter("po_error_msg", GetType(String), 2000)
            ' Call DBHelper Store Procedure
            DBHelper.ExecuteSp(selectStmt, inputParameters, outputParameter)
            If (Not outputParameter(0).Value Is Nothing) Then
                errCode = outputParameter(0).Value
                errMsg = outputParameter(1).Value
                If errCode = 0 Then
                    requestSuccess = True
                End If
            End If          
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
        Return requestSuccess
        
    End Function


#Region "SaveClaimReplaceOptions"
    Public Sub SaveClaimReplaceOptions(claimId As Guid, eqipId As Guid,
                                  priority As String, vendorSKU As String, reserveInventory As String,
                                  inventoryId As Guid, createdBy As String, claimAuthorizationId As Guid)
        Dim selectStmt As String = Me.Config("/SQL/SAVE_CLAIM_REPLACE_OPTIONS")
        Dim strTemp As String

        Using connection As New OracleConnection(DBHelper.ConnectString)
            Using command As OracleCommand = OracleDbHelper.CreateCommand(selectStmt, CommandType.StoredProcedure, connection)
                command.BindByName = True
                command.AddParameter("pi_claim_id", OracleDbType.Raw, 16, claimId.ToByteArray, ParameterDirection.Input)
                command.AddParameter("pi_equipment_id", OracleDbType.Raw, 16, eqipId.ToByteArray, ParameterDirection.Input)
                command.AddParameter("pi_priority", OracleDbType.Varchar2, 10, priority, ParameterDirection.Input)
                command.AddParameter("pi_vendorSku", OracleDbType.Varchar2, 100, vendorSKU, ParameterDirection.Input)
                command.AddParameter("pi_reserveInventory", OracleDbType.Varchar2, 1, reserveInventory, ParameterDirection.Input)
                command.AddParameter("pi_inventoryId", OracleDbType.Raw, 16, inventoryId.ToByteArray, ParameterDirection.Input)
                command.AddParameter("pi_created_by", OracleDbType.Varchar2, 120, createdBy, ParameterDirection.Input)
                command.AddParameter("pi_claim_authorization", OracleDbType.Raw, 16, claimAuthorizationId, ParameterDirection.Input)
                Try
                    connection.Open()
                    command.ExecuteNonQuery()
                    connection.Close()
                Catch ex As Exception
                    Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
                End Try
            End Using
        End Using
    End Sub
#End Region

#End Region

End Class


