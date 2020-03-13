'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (7/31/2012)********************
Imports System.Collections.Generic
Imports System.Linq
Imports System.Data

Public Class PriceListDetailDAL
    Inherits DALBase

#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_PRICE_LIST_DETAIL"
    Public Const TABLE_KEY_NAME As String = "price_list_detail_id"

    Public Const COL_NAME_PRICE_LIST_DETAIL_ID As String = "price_list_detail_id"
    Public Const COL_NAME_STATUS_XCD As String = "status_xcd"
    Public Const COL_NAME_STATUS As String = "status"
    Public Const COL_NAME_STATUS_BY As String = "status_by"
    Public Const COL_NAME_REQUESTED_BY As String = "requested_by"
    Public Const COL_NAME_REQUESTED_DATE As String = "requested_date"
    Public Const COL_NAME_STATUS_DATE As String = "status_date"
    Public Const COL_NAME_PRICE_LIST_ID As String = "price_list_id"
    Public Const COL_NAME_SERVICE_CLASS_ID As String = "service_class_id"
    Public Const COL_NAME_SERVICE_TYPE_ID As String = "service_type_id"
    Public Const COL_NAME_SERVICE_LEVEL_ID As String = "service_Level_id"
    Public Const COL_NAME_SERVICE_CLASS_CODE As String = "service_class_Code"
    Public Const COL_NAME_SERVICE_TYPE_CODE As String = "service_type_Code"
    Public Const COL_NAME_SERVICE_TYPE_DESC As String = "service_type_desc"
    Public Const COL_NAME_SERVICE_LEVEL_CODE As String = "service_Level_Code"
    Public Const COL_NAME_RISK_TYPE_CODE As String = "risk_type_Code"
    Public Const COL_NAME_VENDOR_SKU As String = "vendor_sku"
    Public Const COL_NAME_VENDOR_SKU_DESCRIPTION As String = "vendor_sku_description"
    Public Const COL_NAME_EQUIPMENT_CLASS_ID As String = "equipment_class_id"
    Public Const COL_NAME_EQUIPMENT_ID As String = "equipment_id"
    Public Const COL_NAME_EQUIPMENT_CODE As String = "equipment_code"
    Public Const COL_NAME_CONDITION_ID As String = "condition_id"
    Public Const COL_NAME_RISK_TYPE_ID As String = "risk_type_id"
    Public Const COL_NAME_PRICE As String = "price"
    Public Const COL_NAME_EFFECTIVE As String = "effective"
    Public Const COL_NAME_EXPIRATION As String = "expiration"
    Public Const COL_NAME_IS_DEDUCTIBLE_ID As String = "is_deductible_id"
    Public Const COL_NAME_IS_DEDUCTIBLE_CODE As String = "is_deductible_code"
    Public Const COL_NAME_IS_STANDARD_ID As String = "is_standard_id"
    Public Const COL_NAME_IS_STANDARD_CODE As String = "is_standard_code"
    Public Const COL_NAME_CONTAINS_DEDUCTIBLE_ID As String = "contains_deductible_id"
    Public Const COL_NAME_CONTAINS_DEDUCTIBLE_CODE As String = "contains_deductible_code"
    Public Const COL_NAME_MAKE_ID As String = "make_id"
    Public Const COL_NAME_MAKE As String = "make"
    Public Const COL_NAME_MODEL_ID As String = "model_id"
    Public Const COL_NAME_MODEL As String = "model"
    Public Const COL_NAME_CURRENCY As String = "currency_id"
    Public Const COL_NAME_PRICE_LIST_DETAIL_TYPE As String = "price_list_detail_type_id"
    Public Const COL_NAME_CALCULATION_PERCENTAGE As String = "calculation_percentage"
    Public Const COL_NAME_CURRENCY_SYMBOL As String = "price_with_symbol"
    Public Const COL_NAME_PRICE_LOW_RANGE_WITH_SYMBOL As String = "price_low_range_with_symbol"
    Public Const COL_NAME_PRICE_HIGH_RANGE_WITH_SYMBOL As String = "price_high_range_with_symbol"

    Public Const COL_NAME_CONDITION_TYPE_CODE As String = "condition_type_code"
    Public Const COL_NAME_VENDOR_QUANTITY As String = "vendor_quantity"

    'stored procedure parameter names
    Public Const PAR_NAME_COMPANY As String = "p_company_id"
    Public Const PAR_NAME_SERVICE_CENTER_CODE As String = "p_service_center_code"
    Public Const PAR_NAME_RISK_TYPE_ID As String = "p_risk_type_id"
    Public Const PAR_NAME_EFFECTIVE_DATE As String = "p_effective_date"
    Public Const PAR_NAME_SALES_PRICE As String = "p_sales_price"

    Public Const PAR_NAME_EQUIP_CLASS_ID As String = "p_equip_class_id"
    Public Const PAR_NAME_EQUIPMENT_ID As String = "p_equipment_id"
    Public Const PAR_NAME_CONDITION_ID As String = "p_condition_id"
    Public Const PAR_NAME_DEALER_ID As String = "p_dealer_id"
    Public Const PAR_NAME_SERVICE_LEVEL_CODE As String = "p_svc_lvl_code"
    Public Const PAR_NAME_SERVICE_CLASS_CODE As String = "p_svc_cls_code"
    Public Const PAR_NAME_SERVICE_TYPE_CODE As String = "p_svc_typ_code"
    Public Const PAR_NAME_METHOD_OF_REPAIR_ID As String = "p_method_of_repair_id"

    Public Const PAR_NAME_RETURN_CODE As String = "p_return_code"

    Public Const COL_NAME_PRICE_BAND_RANGE_FROM As String = "price_band_range_from"
    Public Const COL_NAME_PRICE_BAND_RANGE_TO As String = "price_band_range_to"

    Public Const COL_NAME_SERVICE_CLASS_CODE_DV As String = "svc_class_code"
    Public Const COL_NAME_SERVICE_TYPE_CODE_DV As String = "svc_type_code"
    Public Const COL_NAME_SERVICE_LEVEL_CODE_DV As String = "svc_level_code"
    Public Const COL_NAME_REPLACEMENT_TAX_TYPE As String = "replacement_tax_type"

    Public Const PAR_NAME_IN_FORCE_DATE As String = "p_in_force_date"
    Public Const PAR_NAME_COUNTRY_ID As String = "p_country_id"
    Public Const PAR_NAME_CLAIM_NUMBER As String = "p_claim_number"
    Public Const PAR_NAME_COMPANY_CODE As String = "p_company_code"
    Public Const PAR_NAME_RISK_TYPE_CODE As String = "p_risk_type_code"
    Public Const PAR_NAME_EQUIPMENT_CLASS_CODE As String = "p_equip_class_code"
    Public Const PAR_NAME_DEALER_CODE As String = "p_dealer_code"
    Public Const PAR_NAME_MAKE As String = "p_make"
    Public Const PAR_NAME_MODEL As String = "p_model"
    Public Const PAR_NAME_LOW_PRICE As String = "p_low_price"
    Public Const PAR_NAME_HIGH_PRICE As String = "p_high_price"

    'US 224101 
    'Public Const COL_NAME_MANUFACTURER_ID As String = "manufacturer_id"
    Public Const COL_NAME_PART_ID As String = "part_id"
    Public Const COL_NAME_PART_CODE As String = "part_code"
    Public Const COL_NAME_PART_DESC As String = "part_description"
    Public Const COL_NAME_MANUFACTURER_ORIGIN As String = "manufacturer_origin_xcd"
    Public Const COL_NAME_MANUFACTURER_ORIGIN_DESC As String = "manufacturer_origin_desc"
    Public Const COL_NAME_STOCK_ITEM_TYPE As String = "stock_item_type_xcd"

    Public Const PAR_OUT_NAME_RETURN_CODE As String = "po_return_code"

    Public Const PAR_IN_NAME_PRICE_LIST_DETAIL_ID As String = "pi_price_list_detail_id"
    Public Const PAR_IN_NAME_LANGUAGE_ID As String = "pi_language_id"
    Public Const PAR_IN_NAME_PRICE_LIST_ID As String = "pi_price_list_id"
    Public Const PAR_IN_NAME_SERVICE_CLASS_ID As String = "pi_service_class_id"
    Public Const PAR_IN_NAME_SERVICE_TYPE_ID As String = "pi_service_type_id"
    Public Const PAR_IN_NAME_SERVICE_LEVEL_ID As String = "pi_service_level_id"
    Public Const PAR_IN_NAME_EFFECTIVE As String = "pi_effective"
    Public Const PAR_IN_NAME_RISK_TYPE_ID As String = "pi_risk_type_id"
    Public Const PAR_IN_NAME_EQUIPMENT_ID As String = "pi_equipment_id"
    Public Const PAR_IN_NAME_EQUIPMENT_CLASS_ID As String = "pi_equipment_class_id"
    Public Const PAR_IN_NAME_RFERENCE_ID As String = "pi_reference_id"
    Public Const PAR_IN_NAME_EXPIRATION As String = "pi_expiration"
    Public Const PAR_IN_NAME_CONDITION_ID As String = "pi_condition_id"
    Public Const PAR_IN_NAME_USER_ID As String = "pi_user_id"
    Public Const PAR_IN_NAME_COMPANY_GROUP_ID As String = "pi_company_group_id"
    Public Const PAR_IN_NAME_MANUFACTURER_ID As String = "pi_manufacturer_id"
    Public Const PAR_IN_NAME_VENDOR_SKU As String = "pi_vendor_sku"
    Public Const PAR_IN_NAME_VENDOR_SKU_DESCRIPTION As String = "pi_vendor_sku_description"
    Public Const PAR_IN_NAME_PRICE = "pi_price"
    Public Const PAR_IN_NAME_CREATED_BY As String = "pi_created_by"
    Public Const PAR_IN_NAME_MODIFIED_BY As String = "pi_modified_by"
    Public Const PAR_IN_NAME_PRICE_BAND_RANGE_FROM As String = "pi_price_band_range_from"
    Public Const PAR_IN_NAME_PRICE_BAND_RANGE_TO As String = "pi_price_band_range_to"
    Public Const PAR_IN_NAME_REPLACEMENT_TAX_TYPE As String = "pi_replacement_tax_type"
    Public Const PAR_IN_NAME_CURRENCY_ID As String = "pi_currency_id"
    Public Const PAR_IN_NAME_PRICE_LIST_DETAIL_TYPE_ID As String = "pi_price_list_detail_type_id"
    Public Const PAR_IN_NAME_PRICE_LIST_CALCULATION_PERCENTAGE As String = "pi_calculation_percentage"
    Public Const PAR_IN_NAME_PART_ID As String = "pi_part_id"
    Public Const PAR_IN_NAME_MANUFACTURER_ORIGIN As String = "pi_manufacturer_origin_xcd"
    Public Const PAR_IN_NAME_STOCK_ITEM_TYPE As String = "pi_stock_item_type_xcd"
    Public Const PAR_IN_NAME_STATUS_XCD As String = "pi_status_xcd"
    Public Const PAR_IN_NAME_REQUESTED_BY As String = "pi_requested_by"
    Public Const PAR_IN_NAME_REQUESTED_DATE As String = "requested_date"

    'US 224089 
    Public Const PAR_IN_NAME_PARTS_LIST As String = "pi_parts_list"
    Public Const PAR_IN_NAME_CLAIM_ID As String = "pi_claim_id"
    'Public Const PAR_IN_NAME_LANGUAGE_ID As String = "pi_language_id"

    'US 255424
    Public Const COL_NAME_PARENT_EQUIPMENT_ID As String = "parent_equipment_id"
    Public Const COL_NAME_PARENT_EQUIPMENT_CODE As String = "parent_equipment_code"
    Public Const COL_NAME_PARENT_CONDITION_ID As String = "parent_condition_id"
    Public Const COL_NAME_PARENT_CONDITION_TYPE As String = "parent_condition_type"
    Public Const COL_NAME_PARENT_CONDITION_TYPE_CODE As String = "parent_condition_type_code"
    Public Const COL_NAME_PARENT_MAKE_ID As String = "parent_make_id"
    Public Const COL_NAME_PARENT_MAKE As String = "parent_make"
    Public Const COL_NAME_PARENT_MODEL_ID As String = "parent_model_id"
    Public Const COL_NAME_PARENT_MODEL As String = "parent_model"

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
        'Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("price_list_detail_id", id.ToByteArray)}
        'Try
        '    DBHelper.Fetch(familyDS, selectStmt, Me.TABLE_NAME, parameters)
        'Catch ex As Exception
        '    Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        'End Try

        Dim parameters() As OracleParameter = New OracleParameter() {New OracleParameter(Me.PAR_IN_NAME_PRICE_LIST_DETAIL_ID, OracleDbType.Raw, id.ToByteArray, ParameterDirection.Input),
                                                                     New OracleParameter("po_price_table", OracleDbType.RefCursor, ParameterDirection.Output),
                                                                     New OracleParameter(Me.PAR_OUT_NAME_RETURN_CODE, OracleDbType.Int32, ParameterDirection.Output)}
        FetchStoredProcedure("Load",
                                    selectStmt,
                                    parameters,
                                    familyDS)

    End Sub


    Public Function Load(ByVal id As Guid) As DataSet

        'Dim selectStmt As String = Me.Config("/SQL/LOAD")
        'Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("price_list_detail_id", id.ToByteArray)}
        'Dim ds As New DataSet
        'Try
        '    DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)
        '    Return ds
        'Catch ex As Exception
        '    Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        'End Try

        'US 224101 - Replacing code to call stored proc
        Dim selectStmt As String = Me.Config("/SQL/LOAD")

        Dim ds As New DataSet

        Dim parameters() As OracleParameter = New OracleParameter() {New OracleParameter(Me.PAR_IN_NAME_PRICE_LIST_DETAIL_ID, OracleDbType.Raw, id.ToByteArray, ParameterDirection.Input),
                                                                     New OracleParameter("po_price_table", OracleDbType.RefCursor, ParameterDirection.Output),
                                                                     New OracleParameter(Me.PAR_OUT_NAME_RETURN_CODE, OracleDbType.Int16, ParameterDirection.Output)}


        FetchStoredProcedure("Load",
                                    selectStmt,
                                    parameters,
                                    ds)

        Return ds
    End Function

    Public Function LoadList(ByVal id As Guid, ByVal languageId As Guid) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST")
        ''Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("language_id", languageId.ToByteArray), _
        ''                                                                                   New DBHelper.DBHelperParameter("price_list_id", id.ToByteArray)}

        ''Try
        ''    Return DBHelper.Fetch(New DataSet, selectStmt, Me.TABLE_NAME, parameters)
        ''Catch ex As Exception
        ''    Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        ''End Try

        'US 224101 - Replacing code to call stored proc
        Dim parameters() As OracleParameter = New OracleParameter() {New OracleParameter(Me.PAR_IN_NAME_PRICE_LIST_DETAIL_ID, OracleDbType.Raw, id.ToByteArray, ParameterDirection.Input),
                                                                     New OracleParameter("po_price_table", OracleDbType.RefCursor, ParameterDirection.Output),
                                                                     New OracleParameter(Me.PAR_OUT_NAME_RETURN_CODE, OracleDbType.Int16, ParameterDirection.Output)}

        'Dim inParameters(1) As DBHelper.DBHelperParameter


        'inParameters(0) = New DBHelper.DBHelperParameter(Me.PAR_IN_NAME_PRICE_LIST_DETAIL_ID, id.ToByteArray)
        'inParameters(1) = New DBHelper.DBHelperParameter(Me.PAR_IN_NAME_LANGUAGE_ID, languageId.ToByteArray)

        'Dim outParameters(1) As DBHelper.DBHelperParameter
        'outParameters(0) = New DBHelper.DBHelperParameter("po_price_table", GetType(DataSet))
        'outParameters(1) = New DBHelper.DBHelperParameter(Me.PAR_OUT_NAME_RETURN_CODE, GetType(Integer))

        Return FetchStoredProcedure("LoadList",
                                         selectStmt,
                                         parameters)
    End Function

    Public Function GetPriceBandList(ByVal priceListId As Guid, ByVal riskTypeId As Guid, ByVal equipmentclassId As Guid, ByVal equipmentId As Guid,
                                     ByVal conditionId As Guid, ByVal effectiveDate As DateTime, ByVal serviceClassId As Guid, ByVal serviceTypeId As Guid) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/LOAD_PRICEBAND_LIST")
        'Dim ds As New DataSet
        'Dim parameters() As DBHelper.DBHelperParameter

        'parameters = New DBHelper.DBHelperParameter() {
        '                      New DBHelper.DBHelperParameter(COL_NAME_PRICE_LIST_ID, priceListId.ToByteArray),
        '                      New DBHelper.DBHelperParameter(COL_NAME_SERVICE_CLASS_ID, serviceClassId.ToByteArray),
        '                      New DBHelper.DBHelperParameter(COL_NAME_SERVICE_TYPE_ID, serviceTypeId.ToByteArray),
        '                      New DBHelper.DBHelperParameter(COL_NAME_EFFECTIVE, effectiveDate),
        '                      New DBHelper.DBHelperParameter(COL_NAME_RISK_TYPE_ID, riskTypeId.ToByteArray),
        '                      New DBHelper.DBHelperParameter(COL_NAME_EQUIPMENT_ID, equipmentId.ToByteArray),
        '                      New DBHelper.DBHelperParameter(COL_NAME_EQUIPMENT_ID, equipmentId.ToByteArray),
        '                      New DBHelper.DBHelperParameter(COL_NAME_CONDITION_ID, conditionId.ToByteArray),
        '                      New DBHelper.DBHelperParameter(COL_NAME_EQUIPMENT_CLASS_ID, equipmentclassId.ToByteArray)}


        'Return DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)

        'US 224101 - Replacing code to call stored proc
        Dim parameters() As OracleParameter
        parameters = New OracleParameter() {
                              New OracleParameter(PAR_IN_NAME_PRICE_LIST_ID, OracleDbType.Raw, priceListId.ToByteArray, ParameterDirection.Input),
                              New OracleParameter(PAR_IN_NAME_EFFECTIVE, OracleDbType.Varchar2, effectiveDate.ToShortDateString(), ParameterDirection.Input),
                              New OracleParameter(PAR_IN_NAME_SERVICE_CLASS_ID, OracleDbType.Raw, If(IsNothing(serviceClassId) OrElse serviceClassId.Equals(Guid.Empty), Nothing, serviceClassId.ToByteArray), ParameterDirection.Input),
                              New OracleParameter(PAR_IN_NAME_SERVICE_TYPE_ID, OracleDbType.Raw, If(IsNothing(serviceTypeId) OrElse serviceTypeId.Equals(Guid.Empty), Nothing, serviceTypeId.ToByteArray), ParameterDirection.Input),
                              New OracleParameter(PAR_IN_NAME_EQUIPMENT_CLASS_ID, OracleDbType.Raw, OracleDbType.Raw, If(IsNothing(equipmentclassId) OrElse equipmentclassId.Equals(Guid.Empty), Nothing, equipmentclassId.ToByteArray), ParameterDirection.Input),
                              New OracleParameter(PAR_IN_NAME_EQUIPMENT_ID, OracleDbType.Raw, If(IsNothing(equipmentId) OrElse equipmentId.Equals(Guid.Empty), Nothing, equipmentId.ToByteArray), ParameterDirection.Input),
                              New OracleParameter(PAR_IN_NAME_CONDITION_ID, OracleDbType.Raw, If(IsNothing(conditionId) OrElse conditionId.Equals(Guid.Empty), Nothing, conditionId.ToByteArray), ParameterDirection.Input),
                              New OracleParameter(PAR_IN_NAME_RISK_TYPE_ID, OracleDbType.Raw, If(IsNothing(riskTypeId) OrElse riskTypeId.Equals(Guid.Empty), Nothing, riskTypeId.ToByteArray), ParameterDirection.Input),
                              New OracleParameter("po_price_band_table", OracleDbType.RefCursor, ParameterDirection.Output),
                              New OracleParameter(PAR_OUT_NAME_RETURN_CODE, OracleDbType.Int16, ParameterDirection.Output)}

        Return FetchStoredProcedure("GetPriceBandList",
                                         selectStmt,
                                         parameters)
    End Function


    Private Function IsThereALikeClause(ByVal description As String, ByVal code As String) As Boolean
        Dim bIsLikeClause As Boolean

        bIsLikeClause = Me.IsLikeClause(description) OrElse Me.IsLikeClause(code)

        Return bIsLikeClause
    End Function

    Public Function GetMakeByModel(ByVal EquipmentId As Guid) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/GETMAKE")
        Dim ds As New DataSet
        'Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("equipment_id", EquipmentId.ToByteArray())}
        'Try
        '    Return DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)
        'Catch ex As Exception
        '    Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        'End Try

        'US 224101 - Replacing code to call stored proc
        Dim parameters() As OracleParameter = New OracleParameter() {
                                                                        New OracleParameter(PAR_IN_NAME_EQUIPMENT_ID, OracleDbType.Raw, EquipmentId.ToByteArray, ParameterDirection.Input),
                                                                        New OracleParameter("po_make_table", OracleDbType.RefCursor, ParameterDirection.Output),
                                                                        New OracleParameter(PAR_OUT_NAME_RETURN_CODE, OracleDbType.Int16, ParameterDirection.Output)}

        FetchStoredProcedure("GetMakeByModel",
                                    selectStmt,
                                    parameters,
                                    ds)
        Return ds

    End Function

    Public Function GetVendorQuantiy(ByVal id As Guid) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/GETVENDORQUANTITY")
        'Dim ds As New DataSet
        ''Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("reference_id", id.ToByteArray())}
        'Try
        '    Return DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)
        'Catch ex As Exception
        '    Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        'End Try

        'US 224101 - Replacing code to call stored proc
        Dim parameters() As OracleParameter = New OracleParameter() {
                                                                        New OracleParameter(PAR_IN_NAME_RFERENCE_ID, OracleDbType.Raw, id.ToByteArray, ParameterDirection.Input),
                                                                        New OracleParameter("po_vendor_quantity_table", OracleDbType.RefCursor, ParameterDirection.Output),
                                                                        New OracleParameter(PAR_OUT_NAME_RETURN_CODE, OracleDbType.Int16, ParameterDirection.Output)}

        Return FetchStoredProcedure("GetVendorQuantiy",
                                         selectStmt,
                                         parameters)
    End Function

    Public Function GetMaxExpirationMinEffectiveDateForPriceList(ByVal Price_list_Id As Guid, ByVal Price_list_detail_id As Guid) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/GET_MAX_EXP_MIN_EFF_DATES")
        'Dim ds As New DataSet
        'Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("price_list_id", Price_list_Id.ToByteArray), _
        '                                                                                  New DBHelper.DBHelperParameter("price_list_detail_id", Price_list_detail_id.ToByteArray)}

        'Try
        '    Return DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)
        'Catch ex As Exception
        '    Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        'End Try


        'Dim outParameters(1) As DBHelper.DBHelperParameter
        'outParameters(0) = New DBHelper.DBHelperParameter("po_dates_table", GetType(DataSet))
        'outParameters(1) = New DBHelper.DBHelperParameter(Me.PAR_OUT_NAME_RETURN_CODE, GetType(Integer))

        'US 224101 - Replacing code to call stored proc
        Dim parameters() As OracleParameter = New OracleParameter() {New OracleParameter(PAR_IN_NAME_PRICE_LIST_ID, OracleDbType.Raw, Price_list_Id.ToByteArray, ParameterDirection.Input),
                                                                     New OracleParameter(PAR_IN_NAME_PRICE_LIST_DETAIL_ID, OracleDbType.Raw, Price_list_detail_id.ToByteArray, ParameterDirection.Input),
                                                                     New OracleParameter("po_dates_table", OracleDbType.RefCursor, ParameterDirection.Output),
                                                                     New OracleParameter(PAR_OUT_NAME_RETURN_CODE, OracleDbType.Int16, ParameterDirection.Output)}

        Return FetchStoredProcedure("GetMaxExpirationMinEffectiveDateForPriceList",
                                         selectStmt,
                                         parameters)
    End Function

    Public Function GetOverlap(ByVal EquipmentClassId As Guid, ByVal EquipmentId As Guid, ByVal ConditionId As Guid, ByVal risktypeId As Guid,
                               ByVal ServiceClassId As Guid, ByVal ServiceTypeId As Guid, ByVal Sku As String, ByVal PriceListId As Guid,
                               ByVal languageId As Guid, ByVal PriceListDetailId As Guid,
                               ByVal PartId As Guid, ByVal MakeId As Guid, ByVal ManufacturerOrigin As String,
                               ByVal expiration As String, ByVal effective As String) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/GETOVERLAP")
        'Dim whereClauseConditions As String = " expiration > SYSDATE "

        'If Not whereClauseConditions.Equals(String.Empty) Then
        '    whereClauseConditions &= Environment.NewLine & " AND "
        'End If
        'If EquipmentId <> Guid.Empty Then
        '    whereClauseConditions &= Environment.NewLine & " " & Me.COL_NAME_EQUIPMENT_ID & " = " & MiscUtil.GetDbStringFromGuid(EquipmentId, True)
        'Else
        '    whereClauseConditions &= Environment.NewLine & " " & Me.COL_NAME_EQUIPMENT_ID & "  is null "
        'End If


        'If Not whereClauseConditions.Equals(String.Empty) Then
        '    whereClauseConditions &= Environment.NewLine & " AND "
        'End If
        'If ConditionId <> Guid.Empty Then
        '    whereClauseConditions &= Environment.NewLine & " " & Me.COL_NAME_CONDITION_ID & " = " & MiscUtil.GetDbStringFromGuid(ConditionId, True)
        'Else
        '    whereClauseConditions &= Environment.NewLine & " " & Me.COL_NAME_CONDITION_ID & " is null "
        'End If

        'If Not whereClauseConditions.Equals(String.Empty) Then
        '    whereClauseConditions &= Environment.NewLine & " AND "
        'End If
        'If EquipmentClassId <> Guid.Empty Then
        '    whereClauseConditions &= Environment.NewLine & " " & Me.COL_NAME_EQUIPMENT_CLASS_ID & " = " & MiscUtil.GetDbStringFromGuid(EquipmentClassId, True)
        'Else
        '    whereClauseConditions &= Environment.NewLine & " " & Me.COL_NAME_EQUIPMENT_CLASS_ID & " is null "
        'End If

        'If Not whereClauseConditions.Equals(String.Empty) Then
        '    whereClauseConditions &= Environment.NewLine & " AND "
        'End If
        'If risktypeId <> Guid.Empty Then
        '    whereClauseConditions &= Environment.NewLine & " " & Me.COL_NAME_RISK_TYPE_ID & " = " & MiscUtil.GetDbStringFromGuid(risktypeId, True)
        'Else
        '    whereClauseConditions &= Environment.NewLine & " " & Me.COL_NAME_RISK_TYPE_ID & " is null "
        'End If


        'If Not whereClauseConditions.Equals(String.Empty) Then
        '    whereClauseConditions &= Environment.NewLine & " AND "
        'End If
        'If ServiceClassId <> Guid.Empty Then
        '    whereClauseConditions &= Environment.NewLine & " " & Me.COL_NAME_SERVICE_CLASS_ID & " = " & MiscUtil.GetDbStringFromGuid(ServiceClassId, True)
        'Else
        '    whereClauseConditions &= Environment.NewLine & " " & Me.COL_NAME_SERVICE_CLASS_ID & " is null  "
        'End If


        'If Not whereClauseConditions.Equals(String.Empty) Then
        '    whereClauseConditions &= Environment.NewLine & " AND "
        'End If
        'If ServiceTypeId <> Guid.Empty Then
        '    whereClauseConditions &= Environment.NewLine & " " & Me.COL_NAME_SERVICE_TYPE_ID & " = " & MiscUtil.GetDbStringFromGuid(ServiceTypeId, True)
        'Else
        '    whereClauseConditions &= Environment.NewLine & " " & Me.COL_NAME_SERVICE_TYPE_ID & " is null  "
        'End If


        'If Not whereClauseConditions.Equals(String.Empty) Then
        '    whereClauseConditions &= Environment.NewLine & " AND "
        'End If
        ''whereClauseConditions &= Environment.NewLine & " UPPER(" & Me.COL_NAME_VENDOR_SKU & ") = '" & Sku.ToUpper & "' "
        'whereClauseConditions &= "((trunc(" & Me.COL_NAME_EFFECTIVE & ") <= to_date('" & effective & "','MM/dd/yyyy') AND trunc(" & Me.COL_NAME_EXPIRATION & ") >= to_date('" & effective & "','MM/dd/yyyy'))"
        'whereClauseConditions &= Environment.NewLine & " OR "
        'whereClauseConditions &= "(trunc(" & Me.COL_NAME_EFFECTIVE & ") <= to_date('" & expiration & "','MM/dd/yyyy') AND trunc(" & Me.COL_NAME_EXPIRATION & ") >= to_date('" & expiration & "','MM/dd/yyyy')))"

        'whereClauseConditions &= Environment.NewLine & " AND "
        'whereClauseConditions &= Environment.NewLine & " " & Me.COL_NAME_PRICE_LIST_ID & " = " & MiscUtil.GetDbStringFromGuid(PriceListId, True)

        'If Not (PriceListDetailId.Equals(Nothing)) Then
        '    whereClauseConditions &= Environment.NewLine & " AND " & Me.COL_NAME_PRICE_LIST_DETAIL_ID & " != " & MiscUtil.GetDbStringFromGuid(PriceListDetailId, True)
        'End If

        'If Not whereClauseConditions.Equals(String.Empty) Then
        '    whereClauseConditions = " WHERE " & whereClauseConditions
        '    selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
        'Else
        '    selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, "")
        'End If


        'Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() { _
        '         New DBHelper.DBHelperParameter("languageId", languageId.ToByteArray)}


        'Dim ds As New DataSet
        'Try
        '    DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)
        '    Return ds
        'Catch ex As Exception
        '    Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        'End Try

        'US 224101 - Replacing code to call stored proc
        'pi_price_list_id            in  elp_price_list.price_list_id%type,
        'pi_effective                In  varchar2,
        'pi_expiration               in  varchar2,
        'pi_price_list_detail_id     In  elp_price_list_detail.price_list_detail_id%type Default Null,
        'pi_service_class_id         in  raw default Null,
        'pi_service_type_id          In  raw Default Null,
        'pi_equipment_class_id       in  raw default Null,
        'pi_equipment_id             In  elp_equipment.equipment_id%type Default Null,
        'pi_condition_id             in  raw default Null,
        'pi_risk_type_id             In  raw Default Null,

        Dim parameters() As OracleParameter
        parameters = New OracleParameter() {
                              New OracleParameter(PAR_IN_NAME_PRICE_LIST_ID, OracleDbType.Raw, PriceListId.ToByteArray, ParameterDirection.Input),
                              New OracleParameter(PAR_IN_NAME_EFFECTIVE, OracleDbType.Varchar2, effective, ParameterDirection.Input),
                              New OracleParameter(PAR_IN_NAME_EXPIRATION, OracleDbType.Varchar2, expiration, ParameterDirection.Input),
                              New OracleParameter(PAR_IN_NAME_PRICE_LIST_DETAIL_ID, OracleDbType.Raw, If(Not PriceListDetailId.Equals(Nothing) AndAlso PriceListDetailId <> Guid.Empty, PriceListDetailId.ToByteArray, Nothing), ParameterDirection.Input),
                              New OracleParameter(PAR_IN_NAME_SERVICE_CLASS_ID, OracleDbType.Raw, If(IsNothing(ServiceClassId) OrElse ServiceClassId.Equals(Guid.Empty), Nothing, ServiceClassId.ToByteArray), ParameterDirection.Input),
                              New OracleParameter(PAR_IN_NAME_SERVICE_TYPE_ID, OracleDbType.Raw, If(IsNothing(ServiceTypeId) OrElse ServiceTypeId.Equals(Guid.Empty), Nothing, ServiceTypeId.ToByteArray), ParameterDirection.Input),
                              New OracleParameter(PAR_IN_NAME_EQUIPMENT_CLASS_ID, OracleDbType.Raw, OracleDbType.Raw, If(IsNothing(EquipmentClassId) OrElse EquipmentClassId.Equals(Guid.Empty), Nothing, EquipmentClassId.ToByteArray), ParameterDirection.Input),
                              New OracleParameter(PAR_IN_NAME_EQUIPMENT_ID, OracleDbType.Raw, If(IsNothing(EquipmentId) OrElse EquipmentId.Equals(Guid.Empty), Nothing, EquipmentId.ToByteArray), ParameterDirection.Input),
                              New OracleParameter(PAR_IN_NAME_CONDITION_ID, OracleDbType.Raw, If(IsNothing(ConditionId) OrElse ConditionId.Equals(Guid.Empty), Nothing, ConditionId.ToByteArray), ParameterDirection.Input),
                              New OracleParameter(PAR_IN_NAME_RISK_TYPE_ID, OracleDbType.Raw, If(IsNothing(risktypeId) OrElse risktypeId.Equals(Guid.Empty), Nothing, risktypeId.ToByteArray), ParameterDirection.Input),
                              New OracleParameter(PAR_IN_NAME_PART_ID, OracleDbType.Raw, If(IsNothing(PartId) OrElse PartId.Equals(Guid.Empty), Nothing, PartId.ToByteArray), ParameterDirection.Input),
                              New OracleParameter(PAR_IN_NAME_MANUFACTURER_ID, OracleDbType.Raw, If(IsNothing(MakeId) OrElse MakeId.Equals(Guid.Empty), Nothing, MakeId.ToByteArray), ParameterDirection.Input),
                              New OracleParameter(PAR_IN_NAME_MANUFACTURER_ORIGIN, OracleDbType.Varchar2, If(String.IsNullOrEmpty(ManufacturerOrigin), Nothing, ManufacturerOrigin), ParameterDirection.Input),
                              New OracleParameter(PAR_IN_NAME_VENDOR_SKU, OracleDbType.Varchar2, If(String.IsNullOrEmpty(Sku), Nothing, Sku), ParameterDirection.Input),
                              New OracleParameter("po_price_list_detail_table", OracleDbType.RefCursor, ParameterDirection.Output),
                              New OracleParameter(PAR_OUT_NAME_RETURN_CODE, OracleDbType.Int16, ParameterDirection.Output)}

        Return FetchStoredProcedure("GetOverlap",
                                         selectStmt,
                                         parameters)
    End Function

    Public Function FormPriceRangeQuery(ByVal EquipmentClassId As Guid, ByVal EquipmentId As Guid, ByVal ConditionId As Guid, ByVal risktypeId As Guid,
                                        ByVal ServiceClassId As Guid, ByVal ServiceTypeId As Guid, ByVal PriceListId As Guid, ByVal languageId As Guid,
                                        ByVal EffectiveDate As String) As DataView
        Dim selectStmt As String = Me.Config("/SQL/LOAD_PRICEBAND_LIST")

        'Dim whereClauseConditions As String = "trunc(Effective) = to_date('" & EffectiveDate & "','MM/dd/yyyy')"

        'If Not whereClauseConditions.Equals(String.Empty) Then
        '    whereClauseConditions &= Environment.NewLine & " AND "
        'End If
        'If EquipmentId <> Guid.Empty Then
        '    whereClauseConditions &= Environment.NewLine & " " & Me.COL_NAME_EQUIPMENT_ID & " = " & MiscUtil.GetDbStringFromGuid(EquipmentId, True)
        'Else
        '    whereClauseConditions &= Environment.NewLine & " " & Me.COL_NAME_EQUIPMENT_ID & "  is null "
        'End If


        'If Not whereClauseConditions.Equals(String.Empty) Then
        '    whereClauseConditions &= Environment.NewLine & " AND "
        'End If
        'If ConditionId <> Guid.Empty Then
        '    whereClauseConditions &= Environment.NewLine & " " & Me.COL_NAME_CONDITION_ID & " = " & MiscUtil.GetDbStringFromGuid(ConditionId, True)
        'Else
        '    whereClauseConditions &= Environment.NewLine & " " & Me.COL_NAME_CONDITION_ID & " is null "
        'End If

        'If Not whereClauseConditions.Equals(String.Empty) Then
        '    whereClauseConditions &= Environment.NewLine & " AND "
        'End If
        'If EquipmentClassId <> Guid.Empty Then
        '    whereClauseConditions &= Environment.NewLine & " " & Me.COL_NAME_EQUIPMENT_CLASS_ID & " = " & MiscUtil.GetDbStringFromGuid(EquipmentClassId, True)
        'Else
        '    whereClauseConditions &= Environment.NewLine & " " & Me.COL_NAME_EQUIPMENT_CLASS_ID & " is null "
        'End If

        'If Not whereClauseConditions.Equals(String.Empty) Then
        '    whereClauseConditions &= Environment.NewLine & " AND "
        'End If
        'If risktypeId <> Guid.Empty Then
        '    whereClauseConditions &= Environment.NewLine & " " & Me.COL_NAME_RISK_TYPE_ID & " = " & MiscUtil.GetDbStringFromGuid(risktypeId, True)
        'Else
        '    whereClauseConditions &= Environment.NewLine & " " & Me.COL_NAME_RISK_TYPE_ID & " is null "
        'End If


        'If Not whereClauseConditions.Equals(String.Empty) Then
        '    whereClauseConditions &= Environment.NewLine & " AND "
        'End If
        'If ServiceClassId <> Guid.Empty Then
        '    whereClauseConditions &= Environment.NewLine & " " & Me.COL_NAME_SERVICE_CLASS_ID & " = " & MiscUtil.GetDbStringFromGuid(ServiceClassId, True)
        'Else
        '    whereClauseConditions &= Environment.NewLine & " " & Me.COL_NAME_SERVICE_CLASS_ID & " is null  "
        'End If


        'If Not whereClauseConditions.Equals(String.Empty) Then
        '    whereClauseConditions &= Environment.NewLine & " AND "
        'End If
        'If ServiceTypeId <> Guid.Empty Then
        '    whereClauseConditions &= Environment.NewLine & " " & Me.COL_NAME_SERVICE_TYPE_ID & " = " & MiscUtil.GetDbStringFromGuid(ServiceTypeId, True)
        'Else
        '    whereClauseConditions &= Environment.NewLine & " " & Me.COL_NAME_SERVICE_TYPE_ID & " is null  "
        'End If


        'If Not whereClauseConditions.Equals(String.Empty) Then
        '    whereClauseConditions &= Environment.NewLine & " AND "
        'End If

        'whereClauseConditions &= Environment.NewLine & " " & Me.COL_NAME_PRICE_LIST_ID & " = " & MiscUtil.GetDbStringFromGuid(PriceListId, True)


        'If Not whereClauseConditions.Equals(String.Empty) Then
        '    whereClauseConditions = " WHERE " & whereClauseConditions
        '    selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
        'Else
        '    selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, "")
        'End If


        ''Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() { _
        ''        New DBHelper.DBHelperParameter("languageId", languageId.ToByteArray)}


        'Dim ds As New DataSet
        'Try
        '    ds = DBHelper.Fetch(selectStmt, Me.TABLE_NAME)
        '    If Not ds Is Nothing Then
        '        Return ds.Tables(0).DefaultView
        '    End If
        '    'Return ds
        'Catch ex As Exception
        '    Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        'End Try



        'US 224101 - Replacing code to call stored proc
        'pi_price_list_id            in  elp_price_list.price_list_id%type,
        'pi_effective                In  varchar2,
        'pi_service_class_id         in  raw default Null,
        'pi_service_type_id          In  raw Default Null,
        'pi_equipment_class_id       in  raw default Null,
        'pi_equipment_id             In  elp_equipment.equipment_id%type Default Null,
        'pi_condition_id             in  raw default Null,
        'pi_risk_type_id             In  raw Default Null

        Dim parameters() As OracleParameter
        parameters = New OracleParameter() {
                              New OracleParameter(PAR_IN_NAME_PRICE_LIST_ID, OracleDbType.Raw, PriceListId.ToByteArray, ParameterDirection.Input),
                              New OracleParameter(PAR_IN_NAME_EFFECTIVE, OracleDbType.Varchar2, EffectiveDate, ParameterDirection.Input),
                              New OracleParameter(PAR_IN_NAME_SERVICE_CLASS_ID, OracleDbType.Raw, If(IsNothing(ServiceClassId) OrElse ServiceClassId.Equals(Guid.Empty), Nothing, ServiceClassId.ToByteArray), ParameterDirection.Input),
                              New OracleParameter(PAR_IN_NAME_SERVICE_TYPE_ID, OracleDbType.Raw, If(IsNothing(ServiceTypeId) OrElse ServiceTypeId.Equals(Guid.Empty), Nothing, ServiceTypeId.ToByteArray), ParameterDirection.Input),
                              New OracleParameter(PAR_IN_NAME_EQUIPMENT_CLASS_ID, OracleDbType.Raw, OracleDbType.Raw, If(IsNothing(EquipmentClassId) OrElse EquipmentClassId.Equals(Guid.Empty), Nothing, EquipmentClassId.ToByteArray), ParameterDirection.Input),
                              New OracleParameter(PAR_IN_NAME_EQUIPMENT_ID, OracleDbType.Raw, If(IsNothing(EquipmentId) OrElse EquipmentId.Equals(Guid.Empty), Nothing, EquipmentId.ToByteArray), ParameterDirection.Input),
                              New OracleParameter(PAR_IN_NAME_CONDITION_ID, OracleDbType.Raw, If(IsNothing(ConditionId) OrElse ConditionId.Equals(Guid.Empty), Nothing, ConditionId.ToByteArray), ParameterDirection.Input),
                              New OracleParameter(PAR_IN_NAME_RISK_TYPE_ID, OracleDbType.Raw, If(IsNothing(risktypeId) OrElse risktypeId.Equals(Guid.Empty), Nothing, risktypeId.ToByteArray), ParameterDirection.Input),
                              New OracleParameter("po_price_band_table", OracleDbType.RefCursor, ParameterDirection.Output),
                              New OracleParameter(PAR_OUT_NAME_RETURN_CODE, OracleDbType.Int16, ParameterDirection.Output)}

        Dim ds As DataSet = FetchStoredProcedure("FormPriceRangeQuery",
                                         selectStmt,
                                         parameters)

        If Not ds Is Nothing Then
            Return ds.Tables(0).DefaultView
        End If

        Return Nothing
    End Function
#End Region

#Region "Overloaded Methods"
    Public Overloads Sub Update(ByVal ds As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing, Optional ByVal changesFilter As DataRowState = Nothing)
        If ds Is Nothing Then
            Return
        End If
        'If Not ds.Tables(Me.TABLE_NAME) Is Nothing Then
        '    MyBase.Update(ds.Tables(Me.TABLE_NAME), Transaction, changesFilter)
        'End If
        'If ds Is Nothing Then
        '    Return
        'End If

        'US 224101 - Replacing code to call stored proc
        If ds.Tables(Me.TABLE_NAME) IsNot Nothing Then
            Dim tbl As DataTable = ds.Tables(Me.TABLE_NAME)

            'If (Not tbl.Columns.Contains(COL_NAME_MANUFACTURER_ID)) Then
            '    tbl.Columns.Add(COL_NAME_MANUFACTURER_ID, GetType(Guid))
            'End If

            If (Not tbl.Columns.Contains(COL_NAME_MANUFACTURER_ORIGIN)) Then
                tbl.Columns.Add(COL_NAME_MANUFACTURER_ORIGIN, GetType(System.String))
            End If

            If (Not tbl.Columns.Contains(COL_NAME_STOCK_ITEM_TYPE)) Then
                tbl.Columns.Add(COL_NAME_STOCK_ITEM_TYPE, GetType(Guid))
            End If

            If (Not tbl.Columns.Contains(COL_NAME_PART_ID)) Then
                tbl.Columns.Add(COL_NAME_PART_ID, GetType(Guid))
            End If

            Try
                MyBase.UpdateFromSP(ds.Tables(Me.TABLE_NAME), Transaction, changesFilter)
            Catch dbEx As DataBaseAccessException
                If (dbEx.Code.Equals("200")) Then
                    Throw New ElitaPlusException("PriceListDetail - " + dbEx.Message, Common.ErrorCodes.DB_READ_ERROR)
                ElseIf (dbEx.Code.Equals("100")) Then
                    Throw New ElitaPlusException("PriceListDetail - " + dbEx.Message, Common.ErrorCodes.NO_DATA)
                End If

                Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, dbEx)

            Catch ex As Exception
                Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
            End Try


        End If
    End Sub

    Public Overloads Sub UpdateFamily(ByVal familyDataset As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing)
        Dim VendorQty As New VendorQuantityDAL

        Dim tr As IDbTransaction = Transaction
        If tr Is Nothing Then
            tr = DBHelper.GetNewTransaction
        End If
        Try
            'First Pass updates Deletions
            VendorQty.Update(familyDataset, tr, DataRowState.Deleted)
            'Update(familyDataset.Tables(Me.TABLE_NAME), tr, DataRowState.Deleted)

            'US 224101 - Replacing code to call stored proc
            UpdateFromSP(familyDataset.Tables(Me.TABLE_NAME), tr, DataRowState.Deleted)

            'Second Pass updates additions and changes
            VendorQty.Update(familyDataset, tr, DataRowState.Added Or DataRowState.Modified)
            'Update(familyDataset.Tables(Me.TABLE_NAME), tr, DataRowState.Added Or DataRowState.Modified)

            'US 224101 - Replacing code to call stored proc
            UpdateFromSP(familyDataset.Tables(Me.TABLE_NAME), tr, DataRowState.Added Or DataRowState.Modified)

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


    Public Function GetCurrentDateTime() As DateTime
        Dim selectStmt As String = Me.Config("/SQL/CURRENT_TIME_STAMP")
        'Dim whereClauseConditions As String = String.Empty
        'Dim ds As New DataSet
        'Dim EquipmentIdParam As DBHelper.DBHelperParameter
        'Try
        '    Return (DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, New DBHelper.DBHelperParameter() {}).Tables(0).Rows(0)("SYSDATE"))
        'Catch ex As Exception
        '    Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        'End Try

        'US 224101 - Replacing code to call stored proc
        Dim inParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {}

        Dim parameters() As OracleParameter
        parameters = New OracleParameter() {
                              New OracleParameter("po_current_date", OracleDbType.Date, ParameterDirection.Output),
                              New OracleParameter(PAR_OUT_NAME_RETURN_CODE, OracleDbType.Int16, ParameterDirection.Output)}

        FetchStoredProcedure("GetCurrentDateTime",
                                        selectStmt,
                                        parameters)
        If Not parameters(0).Value Is Nothing Then
            Return DateTime.Parse(CType(CType(parameters(0), OracleParameter).Value, Oracle.ManagedDataAccess.Types.OracleDate))
        End If

        Return Nothing
    End Function

    Public Function GetRepairPrices(ByVal companyId As Guid, ByVal ServiceCenterCode As String, ByVal EffectiveDate As Date,
                                    ByVal SalesPrice As Decimal, ByVal RiskTypeId As Guid, ByVal EquipClassId As Guid,
                                    ByVal EquipmentID As Guid, ByVal ConditionId As Guid, ByVal DealerId As Guid, ByVal serviceLevelCode As String) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/FIND_REPAIR_PRICE")
        Dim inParameters(9) As DBHelper.DBHelperParameter


        inParameters(0) = New DBHelper.DBHelperParameter(Me.PAR_NAME_COMPANY, companyId.ToByteArray)
        inParameters(1) = New DBHelper.DBHelperParameter(Me.PAR_NAME_SERVICE_CENTER_CODE, ServiceCenterCode)
        inParameters(2) = New DBHelper.DBHelperParameter(Me.PAR_NAME_EFFECTIVE_DATE, EffectiveDate)
        inParameters(3) = New DBHelper.DBHelperParameter(Me.PAR_NAME_SALES_PRICE, SalesPrice)
        inParameters(4) = New DBHelper.DBHelperParameter(Me.PAR_NAME_RISK_TYPE_ID, RiskTypeId.ToByteArray)
        inParameters(5) = New DBHelper.DBHelperParameter(Me.PAR_NAME_EQUIP_CLASS_ID, EquipClassId.ToByteArray)
        inParameters(6) = New DBHelper.DBHelperParameter(Me.PAR_NAME_EQUIPMENT_ID, EquipmentID.ToByteArray)
        inParameters(7) = New DBHelper.DBHelperParameter(Me.PAR_NAME_CONDITION_ID, ConditionId.ToByteArray)

        ''''send null if dealer is guid.empty
        If Not DealerId.Equals(Guid.Empty) Then
            inParameters(8) = New DBHelper.DBHelperParameter(Me.PAR_NAME_DEALER_ID, DealerId.ToByteArray)
        Else
            inParameters(8) = New DBHelper.DBHelperParameter(Me.PAR_NAME_DEALER_ID, Nothing)
        End If

        inParameters(9) = New DBHelper.DBHelperParameter(Me.PAR_NAME_SERVICE_LEVEL_CODE, serviceLevelCode)

        Dim outParameters(1) As DBHelper.DBHelperParameter
        outParameters(0) = New DBHelper.DBHelperParameter("P_PriceListSVCLvlCur", GetType(DataSet))
        outParameters(1) = New DBHelper.DBHelperParameter(Me.PAR_NAME_RETURN_CODE, GetType(Integer))

        Dim ds As New DataSet
        Dim tbl As String = Me.TABLE_NAME

        ' Call DBHelper Store Procedure
        DBHelper.FetchSp(selectStmt, inParameters, outParameters, ds, tbl)
        Return ds

    End Function


#Region "Private Methods"

    Function SetParameter(ByVal name As String, ByVal value As Object) As DBHelper.DBHelperParameter

        name = name.Trim
        If value Is Nothing Then value = DBNull.Value
        If Not value Is Nothing AndAlso value.GetType Is GetType(String) Then value = DirectCast(value, String).Trim

        Return New DBHelper.DBHelperParameter(name, value, value.GetType)

    End Function

#End Region

    Public Function GetPriceForMethodofRepair(ByVal MethodofRepairId As Guid, ByVal companyId As Guid, ByVal ServiceCenterCode As String,
                                              ByVal EffectiveDate As Date, ByVal SalesPrice As Decimal, ByVal RiskTypeId As Guid,
                                              ByVal EquipClassId As Guid, ByVal EquipmentID As Guid, ByVal ConditionId As Guid,
                                              ByVal DealerId As Guid, ByVal ServiceLevelCode As String) As DataSet
        Try


            Dim selectStmt As String = Me.Config("/SQL/GET_METHOD_OF_REPAIR_PRICE_LIST")
            Dim inParameters(10) As DBHelper.DBHelperParameter

            Dim dealerBytes As Byte()
            ''''send null if dealer is guid.empty
            If Not DealerId.Equals(Guid.Empty) Then
                dealerBytes = DealerId.ToByteArray
            Else
                dealerBytes = Nothing
            End If
            '''''

            Dim inputParameters() As DBHelper.DBHelperParameter
            inputParameters = New DBHelper.DBHelperParameter() _
                   {SetParameter(Me.PAR_NAME_METHOD_OF_REPAIR_ID, MethodofRepairId.ToByteArray),
                    SetParameter(Me.PAR_NAME_COMPANY, companyId.ToByteArray),
                    SetParameter(Me.PAR_NAME_SERVICE_CENTER_CODE, ServiceCenterCode),
                    SetParameter(Me.PAR_NAME_EFFECTIVE_DATE, EffectiveDate),
                    SetParameter(Me.PAR_NAME_SALES_PRICE, SalesPrice),
                    SetParameter(Me.PAR_NAME_RISK_TYPE_ID, RiskTypeId.ToByteArray),
                    SetParameter(Me.PAR_NAME_EQUIP_CLASS_ID, EquipClassId.ToByteArray),
                    SetParameter(Me.PAR_NAME_EQUIPMENT_ID, EquipmentID.ToByteArray),
                    SetParameter(Me.PAR_NAME_CONDITION_ID, ConditionId.ToByteArray),
                    SetParameter(Me.PAR_NAME_DEALER_ID, dealerBytes),
                    SetParameter(Me.PAR_NAME_SERVICE_LEVEL_CODE, ServiceLevelCode)}


            Dim outParameters(1) As DBHelper.DBHelperParameter
            outParameters(0) = New DBHelper.DBHelperParameter("P_SVC_TABLE_CUR", GetType(DataSet))
            outParameters(1) = New DBHelper.DBHelperParameter(PAR_NAME_RETURN_CODE, GetType(Integer))

            Dim ds As New DataSet
            Dim tbl As String = Me.TABLE_NAME

            ' Call DBHelper Store Procedure
            DBHelper.FetchSp(selectStmt, inputParameters, outParameters, ds, tbl)

            If outParameters(1).Value = 200 Then ''''if the price list is not configured to the service center
                Return Nothing
            ElseIf outParameters(1).Value = 100 Then  '''exception in the procedure
                Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr)
            End If

            Return ds

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Sub LoadPriceListDetailsForPriceList(ByVal familyDS As DataSet, ByVal id As Guid, ByVal languageId As Guid, ByVal User_id As Guid)

        ''Try

        ''    Dim selectStmt As String = Me.Config("/SQL/LOAD_PRICE_LIST_LIST")
        ''    Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {
        ''    New DBHelper.DBHelperParameter(LanguageDAL.COL_NAME_LANGUAGE_ID, languageId.ToByteArray),
        ''    New DBHelper.DBHelperParameter(LanguageDAL.COL_NAME_LANGUAGE_ID, languageId.ToByteArray),
        ''    New DBHelper.DBHelperParameter(LanguageDAL.COL_NAME_LANGUAGE_ID, languageId.ToByteArray),
        ''    New DBHelper.DBHelperParameter(LanguageDAL.COL_NAME_LANGUAGE_ID, languageId.ToByteArray),
        ''    New DBHelper.DBHelperParameter("User_id", User_id.ToByteArray),
        ''    New DBHelper.DBHelperParameter(COL_NAME_PRICE_LIST_ID, id.ToByteArray)}

        ''    DBHelper.Fetch(familyDS, selectStmt, Me.TABLE_NAME, parameters)

        ''Catch ex As Exception
        ''    Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        ''End Try

        'US 224101 - Replacing code to call stored proc
        Dim selectStmt As String = Me.Config("/SQL/LOAD_PRICE_LIST_LIST")
        Dim parameters() As OracleParameter
        parameters = New OracleParameter() {
                              New OracleParameter(PAR_IN_NAME_LANGUAGE_ID, OracleDbType.Raw, languageId.ToByteArray, ParameterDirection.Input),
                              New OracleParameter(PAR_IN_NAME_USER_ID, OracleDbType.Raw, User_id.ToByteArray, ParameterDirection.Input),
                              New OracleParameter(PAR_IN_NAME_PRICE_LIST_ID, OracleDbType.Raw, id.ToByteArray, ParameterDirection.Input),
                              New OracleParameter("po_price_table", OracleDbType.RefCursor, ParameterDirection.Output),
                              New OracleParameter(PAR_OUT_NAME_RETURN_CODE, OracleDbType.Int16, ParameterDirection.Output)}

        FetchStoredProcedure("LoadPriceListDetailsForPriceList",
                                    selectStmt,
                                    parameters,
                                    familyDS)
    End Sub

    Public Function ViewPriceListDetailHistory(ByVal Pricelistdetaild As Guid, ByVal languageId As Guid, ByVal familyDS As DataSet) As DataSet
        'Dim pricelistdetailidlist As Guid
        ''For Each r As DataRow In familyDS.Tables(2).Rows
        ''    pricelistdetailidlist += r("PRICE_LIST_DETAIL_ID")
        ''Next

        Dim selectStmt As String = Me.Config("/SQL/LOAD_PRICE_LIST_DETAIL_HISTORY")
        Dim parameters() As OracleParameter
        parameters = New OracleParameter() {
                              New OracleParameter("pi_price_list_detail_id", OracleDbType.Raw, Pricelistdetaild.ToByteArray, ParameterDirection.Input),
                              New OracleParameter(PAR_IN_NAME_LANGUAGE_ID, OracleDbType.Raw, languageId.ToByteArray, ParameterDirection.Input),
                              New OracleParameter("po_price_list_detail_hist", OracleDbType.RefCursor, ParameterDirection.Output),
                              New OracleParameter(PAR_OUT_NAME_RETURN_CODE, OracleDbType.Int16, ParameterDirection.Output)}

        FetchStoredProcedure("ViewPriceListDetailsHistory",
                                    selectStmt,
                                    parameters,
                                    familyDS)
    End Function

    Public Function GetMakeModelByEquipmentId(ByVal Equipmentid As Guid, ByVal CompanyGroupId As Guid) As DataSet
        'Dim ds As New DataSet
        'Try

        '    Dim selectStmt As String = Me.Config("/SQL/FIND_MAKE_MODEL_BY_EQUIPMENT_ID")
        '    Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(COL_NAME_EQUIPMENT_ID, Equipmentid.ToByteArray),
        '                                                                                 New DBHelper.DBHelperParameter("company_group_id", CompanyGroupId.ToByteArray)}

        '    Return DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)

        'Catch ex As Exception
        '    Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        'End Try

        'US 224101 - Replacing code to call stored proc
        Dim selectStmt As String = Me.Config("/SQL/FIND_MAKE_MODEL_BY_EQUIPMENT_ID")
        Dim parameters() As OracleParameter
        parameters = New OracleParameter() {
                              New OracleParameter(PAR_IN_NAME_EQUIPMENT_ID, OracleDbType.Raw, Equipmentid.ToByteArray, ParameterDirection.Input),
                              New OracleParameter(PAR_IN_NAME_COMPANY_GROUP_ID, OracleDbType.Raw, CompanyGroupId.ToByteArray, ParameterDirection.Input),
                              New OracleParameter("po_make_model_table", OracleDbType.RefCursor, ParameterDirection.Output),
                              New OracleParameter(PAR_OUT_NAME_RETURN_CODE, OracleDbType.Int16, ParameterDirection.Output)}

        Return FetchStoredProcedure("GetMakeModelByEquipmentId",
                                        selectStmt,
                                        parameters)
    End Function

    Public Function GetModelsByMake(ByVal Manufacturerid As Guid) As DataSet
        'Dim ds As New DataSet
        'Try

        '    Dim selectStmt As String = Me.Config("/SQL/FIND_MODELS_BY_MAKE")
        '    Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("manufacturer_id", Manufacturerid.ToByteArray)}

        '    Return DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)

        'Catch ex As Exception
        '    Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        'End Try


        'US 224101 - Replacing code to call stored proc
        Dim selectStmt As String = Me.Config("/SQL/FIND_MODELS_BY_MAKE")
        Dim parameters() As OracleParameter
        parameters = New OracleParameter() {
                              New OracleParameter(PAR_IN_NAME_MANUFACTURER_ID, OracleDbType.Raw, Manufacturerid.ToByteArray, ParameterDirection.Input),
                              New OracleParameter("po_model_table", OracleDbType.RefCursor, ParameterDirection.Output),
                              New OracleParameter(PAR_OUT_NAME_RETURN_CODE, OracleDbType.Int16, ParameterDirection.Output)}

        Return FetchStoredProcedure("GetModelsByMake",
                                        selectStmt,
                                        parameters)
    End Function

    Public Function GetPriceList(ByVal InForceDate As Date, ByVal ClaimNumber As String, ByVal CompanyCode As String, ByVal ServiceCenterCode As String, ByVal RiskTypeCode As String,
                                 ByVal EquipmentClassCode As String, ByVal DealerCode As String, ByVal ServiceClassCode As String, ByVal ServiceTypeCode As String, ByVal Make As String,
                                 ByVal Model As String, ByVal LowPrice As String, ByVal HighPrice As String, ByVal ServiceLevelCode As String) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/GET_PRICE_LIST")
        Dim inParameters(15) As DBHelper.DBHelperParameter

        inParameters(0) = New DBHelper.DBHelperParameter(Me.PAR_NAME_IN_FORCE_DATE, InForceDate)
        inParameters(1) = New DBHelper.DBHelperParameter(Me.PAR_NAME_CLAIM_NUMBER, ClaimNumber)
        inParameters(2) = New DBHelper.DBHelperParameter(Me.PAR_NAME_COMPANY_CODE, CompanyCode)
        inParameters(3) = New DBHelper.DBHelperParameter(Me.PAR_NAME_SERVICE_CENTER_CODE, ServiceCenterCode)
        inParameters(4) = New DBHelper.DBHelperParameter(Me.PAR_NAME_RISK_TYPE_CODE, RiskTypeCode)
        inParameters(5) = New DBHelper.DBHelperParameter(Me.PAR_NAME_EQUIPMENT_CLASS_CODE, EquipmentClassCode)
        inParameters(6) = New DBHelper.DBHelperParameter(Me.PAR_NAME_EQUIPMENT_ID, Nothing)
        inParameters(7) = New DBHelper.DBHelperParameter(Me.PAR_NAME_CONDITION_ID, Nothing)
        inParameters(8) = New DBHelper.DBHelperParameter(Me.PAR_NAME_DEALER_CODE, DealerCode)
        inParameters(9) = New DBHelper.DBHelperParameter(Me.PAR_NAME_SERVICE_CLASS_CODE, ServiceClassCode)
        inParameters(10) = New DBHelper.DBHelperParameter(Me.PAR_NAME_SERVICE_TYPE_CODE, ServiceTypeCode)
        inParameters(11) = New DBHelper.DBHelperParameter(Me.PAR_NAME_MAKE, Make)
        inParameters(12) = New DBHelper.DBHelperParameter(Me.PAR_NAME_MODEL, Model)
        inParameters(13) = New DBHelper.DBHelperParameter(Me.PAR_NAME_LOW_PRICE, LowPrice)
        inParameters(14) = New DBHelper.DBHelperParameter(Me.PAR_NAME_HIGH_PRICE, HighPrice)
        inParameters(15) = New DBHelper.DBHelperParameter(Me.PAR_NAME_SERVICE_LEVEL_CODE, ServiceLevelCode)

        Dim outParameters(1) As DBHelper.DBHelperParameter
        outParameters(0) = New DBHelper.DBHelperParameter("p_price_table", GetType(DataSet))
        outParameters(1) = New DBHelper.DBHelperParameter(PAR_NAME_RETURN_CODE, GetType(Integer))

        Dim ds As New DataSet
        Dim tbl As String = "GET_PRICE_LIST"

        ' Call DBHelper Store Procedure
        DBHelper.FetchSp(selectStmt, inParameters, outParameters, ds, tbl)

        If outParameters(1).Value = 100 Then  '''exception in the procedure
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr)
        End If

        Return ds

    End Function

    Public Function GetPriceListForParts(ByVal claimId As Guid, ByVal listOfParts As String) As DataSet

        Dim selectStmt As String = Me.Config("/SQL/GET_PARTS_PRICE_LIST")
        Dim inParameters(1) As DBHelper.DBHelperParameter

        inParameters(0) = New DBHelper.DBHelperParameter(Me.PAR_IN_NAME_CLAIM_ID, claimId)
        inParameters(1) = New DBHelper.DBHelperParameter(Me.PAR_IN_NAME_PARTS_LIST, listOfParts)


        Dim outParameters(1) As DBHelper.DBHelperParameter
        outParameters(0) = New DBHelper.DBHelperParameter("po_parts_prices", GetType(DataSet))
        outParameters(1) = New DBHelper.DBHelperParameter(PAR_OUT_NAME_RETURN_CODE, GetType(Integer))

        Dim ds As New DataSet
        Dim tbl As String = "GET_PRICE_LIST"

        ' Call DBHelper Store Procedure
        DBHelper.FetchSp(selectStmt, inParameters, outParameters, ds, tbl)

        If outParameters(1).Value > 0 Then
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr)
        End If

        Return ds
    End Function

    'US 224101 - Common call to stored procedures
    Private Function FetchStoredProcedure(methodName As String, storedProc As String, parameters() As OracleParameter, Optional familyDS As DataSet = Nothing) As DataSet
        Dim ds As DataSet = If(familyDS Is Nothing, New DataSet(), familyDS)
        Dim tbl As String = Me.TABLE_NAME

        If (ds.Tables Is Nothing OrElse ds.Tables.Count = 0) Then
            ds.Tables.Add(tbl)
        End If


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
            If (Not par Is Nothing And par.Value = 200) Then
                Throw New ElitaPlusException("PriceListDetail - " + methodName, Common.ErrorCodes.DB_READ_ERROR)
            End If
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try


        Return ds
    End Function

    Protected Overrides Sub ConfigureDeleteCommand(ByRef command As OracleCommand, ByVal tableName As String)
        command.AddParameter(PAR_IN_NAME_PRICE_LIST_DETAIL_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_PRICE_LIST_DETAIL_ID)
        command.AddParameter(PAR_OUT_NAME_RETURN_CODE, OracleDbType.Int32, ParameterDirection.Output)
    End Sub

    Protected Overrides Sub ConfigureInsertCommand(ByRef command As OracleCommand, ByVal tableName As String)

        With command
            .AddParameter(PAR_IN_NAME_PRICE_LIST_DETAIL_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_PRICE_LIST_DETAIL_ID)
            .AddParameter(PAR_IN_NAME_PRICE_LIST_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_PRICE_LIST_ID)
            .AddParameter(PAR_IN_NAME_SERVICE_CLASS_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_SERVICE_CLASS_ID)
            .AddParameter(PAR_IN_NAME_SERVICE_TYPE_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_SERVICE_TYPE_ID)
            .AddParameter(PAR_IN_NAME_VENDOR_SKU, OracleDbType.Varchar2, sourceColumn:=COL_NAME_VENDOR_SKU)
            .AddParameter(PAR_IN_NAME_VENDOR_SKU_DESCRIPTION, OracleDbType.Varchar2, sourceColumn:=COL_NAME_VENDOR_SKU_DESCRIPTION)
            .AddParameter(PAR_IN_NAME_EQUIPMENT_CLASS_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_EQUIPMENT_CLASS_ID)
            .AddParameter(PAR_IN_NAME_EQUIPMENT_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_EQUIPMENT_ID)
            .AddParameter(PAR_IN_NAME_CONDITION_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_CONDITION_ID)
            .AddParameter(PAR_IN_NAME_RISK_TYPE_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_RISK_TYPE_ID)
            .AddParameter(PAR_IN_NAME_PRICE, OracleDbType.Decimal, sourceColumn:=COL_NAME_PRICE)
            .AddParameter(PAR_IN_NAME_EFFECTIVE, OracleDbType.Date, sourceColumn:=COL_NAME_EFFECTIVE)
            .AddParameter(PAR_IN_NAME_EXPIRATION, OracleDbType.Date, sourceColumn:=COL_NAME_EXPIRATION)
            .AddParameter(PAR_IN_NAME_REQUESTED_BY, OracleDbType.Varchar2, sourceColumn:=COL_NAME_CREATED_BY)
            .AddParameter(PAR_IN_NAME_SERVICE_LEVEL_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_SERVICE_LEVEL_ID)
            .AddParameter(PAR_IN_NAME_PRICE_BAND_RANGE_FROM, OracleDbType.Double, sourceColumn:=COL_NAME_PRICE_BAND_RANGE_FROM)
            .AddParameter(PAR_IN_NAME_PRICE_BAND_RANGE_TO, OracleDbType.Double, sourceColumn:=COL_NAME_PRICE_BAND_RANGE_TO)
            .AddParameter(PAR_IN_NAME_REPLACEMENT_TAX_TYPE, OracleDbType.Raw, sourceColumn:=COL_NAME_REPLACEMENT_TAX_TYPE)
            .AddParameter(PAR_IN_NAME_CURRENCY_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_CURRENCY)
            .AddParameter(PAR_IN_NAME_PRICE_LIST_DETAIL_TYPE_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_PRICE_LIST_DETAIL_TYPE)
            .AddParameter(PAR_IN_NAME_PRICE_LIST_CALCULATION_PERCENTAGE, OracleDbType.Decimal, sourceColumn:=COL_NAME_CALCULATION_PERCENTAGE)
            .AddParameter(PAR_IN_NAME_MANUFACTURER_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_MAKE_ID)
            .AddParameter(PAR_IN_NAME_PART_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_PART_ID)
            .AddParameter(PAR_IN_NAME_MANUFACTURER_ORIGIN, OracleDbType.Varchar2, sourceColumn:=COL_NAME_MANUFACTURER_ORIGIN)
            .AddParameter(PAR_IN_NAME_STOCK_ITEM_TYPE, OracleDbType.Varchar2, sourceColumn:=COL_NAME_STOCK_ITEM_TYPE)

            .AddParameter(PAR_OUT_NAME_RETURN_CODE, OracleDbType.Int32, ParameterDirection.Output)
        End With

    End Sub

    Protected Overrides Sub ConfigureUpdateCommand(ByRef command As OracleCommand, ByVal tableName As String)

        With command
            .AddParameter(PAR_IN_NAME_PRICE_LIST_DETAIL_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_PRICE_LIST_DETAIL_ID)
            .AddParameter(PAR_IN_NAME_PRICE_LIST_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_PRICE_LIST_ID)
            .AddParameter(PAR_IN_NAME_SERVICE_CLASS_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_SERVICE_CLASS_ID)
            .AddParameter(PAR_IN_NAME_SERVICE_TYPE_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_SERVICE_TYPE_ID)
            .AddParameter(PAR_IN_NAME_VENDOR_SKU, OracleDbType.Varchar2, sourceColumn:=COL_NAME_VENDOR_SKU)
            .AddParameter(PAR_IN_NAME_VENDOR_SKU_DESCRIPTION, OracleDbType.Varchar2, sourceColumn:=COL_NAME_VENDOR_SKU_DESCRIPTION)
            .AddParameter(PAR_IN_NAME_EQUIPMENT_CLASS_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_EQUIPMENT_CLASS_ID)
            .AddParameter(PAR_IN_NAME_EQUIPMENT_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_EQUIPMENT_ID)
            .AddParameter(PAR_IN_NAME_CONDITION_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_CONDITION_ID)
            .AddParameter(PAR_IN_NAME_RISK_TYPE_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_RISK_TYPE_ID)
            .AddParameter(PAR_IN_NAME_PRICE, OracleDbType.Decimal, sourceColumn:=COL_NAME_PRICE)
            .AddParameter(PAR_IN_NAME_EFFECTIVE, OracleDbType.Date, sourceColumn:=COL_NAME_EFFECTIVE)
            .AddParameter(PAR_IN_NAME_EXPIRATION, OracleDbType.Date, sourceColumn:=COL_NAME_EXPIRATION)
            .AddParameter(PAR_IN_NAME_REQUESTED_BY, OracleDbType.Varchar2, sourceColumn:=COL_NAME_MODIFIED_BY)
            .AddParameter(PAR_IN_NAME_SERVICE_LEVEL_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_SERVICE_LEVEL_ID)
            .AddParameter(PAR_IN_NAME_PRICE_BAND_RANGE_FROM, OracleDbType.Double, sourceColumn:=COL_NAME_PRICE_BAND_RANGE_FROM)
            .AddParameter(PAR_IN_NAME_PRICE_BAND_RANGE_TO, OracleDbType.Double, sourceColumn:=COL_NAME_PRICE_BAND_RANGE_TO)
            .AddParameter(PAR_IN_NAME_REPLACEMENT_TAX_TYPE, OracleDbType.Raw, sourceColumn:=COL_NAME_REPLACEMENT_TAX_TYPE)
            .AddParameter(PAR_IN_NAME_CURRENCY_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_CURRENCY)
            .AddParameter(PAR_IN_NAME_PRICE_LIST_DETAIL_TYPE_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_PRICE_LIST_DETAIL_TYPE)
            .AddParameter(PAR_IN_NAME_PRICE_LIST_CALCULATION_PERCENTAGE, OracleDbType.Decimal, sourceColumn:=COL_NAME_CALCULATION_PERCENTAGE)
            .AddParameter(PAR_IN_NAME_MANUFACTURER_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_MAKE_ID)
            .AddParameter(PAR_IN_NAME_PART_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_PART_ID)
            .AddParameter(PAR_IN_NAME_MANUFACTURER_ORIGIN, OracleDbType.Varchar2, sourceColumn:=COL_NAME_MANUFACTURER_ORIGIN)
            .AddParameter(PAR_IN_NAME_STOCK_ITEM_TYPE, OracleDbType.Varchar2, sourceColumn:=COL_NAME_STOCK_ITEM_TYPE)

            .AddParameter(PAR_OUT_NAME_RETURN_CODE, OracleDbType.Int32, ParameterDirection.Output)
        End With

    End Sub

End Class



