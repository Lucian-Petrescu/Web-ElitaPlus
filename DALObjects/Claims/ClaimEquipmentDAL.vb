
Imports System.Collections.Generic

Public Class ClaimEquipmentDAL
    Inherits DALBase

#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_CLAIM_EQUIPMENT"
    Public Const TABLE_KEY_NAME As String = "claim_equipment_id"

    Public Const COL_NAME_CLAIM_EQUIPMENT_ID As String = "claim_equipment_id"
    Public Const COL_NAME_CLAIM_EQUIPMENT_DATE As String = "claim_equipment_date"
    Public Const COL_NAME_CLAIM_ID As String = "claim_id"
    Public Const COL_NAME_MANUFACTURER As String = "manufacturer"
    Public Const COL_NAME_MANUFACTURER_ID As String = "manufacturer_id"
    Public Const COL_NAME_MODEL As String = "model"
    Public Const COL_NAME_COLOR As String = "color"
    Public Const COL_NAME_MEMORY As String = "memory"
    Public Const COL_NAME_SKU As String = "sku"
    Public Const COL_NAME_SHIPPINGFROMNAME As String = "shippingfromname"
    Public Const COL_NAME_SHIPPINGFROMDESCRIPTION As String = "shippingfromdescription"
    <Obsolete("This is not in any of SQL in DAL, who's using it? There are references in code")>
    Public Const COL_NAME_CLAIM_EQUIPMENT_TYPE As String = "claim_equipment_type"
    Public Const COL_NAME_PRICE As String = "price"
    Public Const COL_NAME_SERIAL_NUMBER As String = "serial_number"
    Public Const COL_NAME_IMEI_NUMBER As String = "imei_number"
    Public Const COL_NAME_EQUIPMENT_ID As String = "equipment_id"
    Public Const COL_NAME_CLAIM_EQUIPMENT_TYPE_ID As String = "claim_equipment_type_id"
    Public Const COL_NAME_PRIORITY As String = "PRIORITY"
    Public Const COL_NAME_COMMENTS As String = "COMMENTS"
    Public Const COL_NAME_DEVICE_TYPE_ID As String = "DEVICE_TYPE_ID"
    Public Const COL_NAME_CLAIM_AUTHORIZATION_ID As String = "CLAIM_AUTHORIZATION_ID"
    Public Const PO_CURSOR_Equipment = 0
    Public Const COL_NAME_LANGUAGE_ID As String = "pi_language_id"
    Public Const COL_NAME_DEVICE_TYPE As String = "Device_Type"
    Public Const COL_NAME_EQUIPMENT_TYPE As String = "Equipment_Type"
    Public Const COL_NAME_PURCHASED_DATE As String = "Purchased_Date"
    Public Const COL_NAME_PURCHASE_PRICE As String = "Purchase_Price"
    Public Const COL_NAME_REGISTERED_ITEM_NAME As String = "Registered_Item_Name"
    'Public Const COL_NAME_CARRIER As String = "Carrier"

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

    Public Sub Load(familyDS As DataSet, id As Guid)
        Dim selectStmt As String = Config("/SQL/LOAD")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("claim_history_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Sub LoadList(familyDS As DataSet, claimId As Guid)
        Dim selectStmt As String = Config("/SQL/LOAD_LIST")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(COL_NAME_CLAIM_ID, claimId.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Sub LoadDeviceInfoList(familyDS As DataSet, claimId As Guid, languageId As Guid)
        Dim selectStmt As String = Config("/SQL/LOAD_DEVICE_INFO_LIST")
        Dim ds As DataSet = New DataSet
        Dim outputParameter(0) As DBHelper.DBHelperParameter
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(COL_NAME_CLAIM_ID, claimId.ToByteArray),
                                                                                           New DBHelper.DBHelperParameter(COL_NAME_LANGUAGE_ID, languageId)}
        outputParameter(PO_CURSOR_Equipment) = New DBHelper.DBHelperParameter("po_device_info_cursor", GetType(DataSet))

        Try

            DBHelper.FetchSp(selectStmt, parameters, outputParameter, familyDS, TABLE_NAME)
            'ds.Tables(0).TableName = Me.TABLE_NAME

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub
#End Region

#Region "Overloaded Methods"
    Public Overloads Sub Update(ds As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing, Optional ByVal changesFilter As DataRowState = Nothing)
        If ds Is Nothing Then
            Return
        End If
        If Not ds.Tables(TABLE_NAME) Is Nothing Then
            MyBase.Update(ds.Tables(TABLE_NAME), Transaction, changesFilter)
        End If
    End Sub
#End Region

    Public Function GetLatestClaimEquipmentInfo(claimId As Guid, claimEquipmentTypeId As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/GET_LATEST_REFURB_REPLACE_EQUIP_INFO")
        Dim ds As New DataSet
            
        Dim parameters = New OracleParameter() {New OracleParameter(COL_NAME_CLAIM_EQUIPMENT_TYPE_ID, claimEquipmentTypeId.ToByteArray),
                                                New OracleParameter(COL_NAME_CLAIM_ID, claimId.ToByteArray)}
        Try
            DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Sub UpdateClaimEquipmentInfo(claimEquipmentId As Guid, comments As String)
        Dim selectStmt As String
        Dim parameters() As DBHelper.DBHelperParameter
        selectStmt = Config("/SQL/UPDATE_CLAIM_EQUIPMENT_INFO")
        parameters = New DBHelper.DBHelperParameter() {
                                                          New DBHelper.DBHelperParameter(COL_NAME_COMMENTS, comments),
                                                          New DBHelper.DBHelperParameter(COL_NAME_CLAIM_EQUIPMENT_ID, claimEquipmentId.ToByteArray)}
        Try
            DBHelper.ExecuteWithParam(selectStmt, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub



    Public Function GetReplacementItemInfo(claimId As Guid) As DataSet

        Dim selectStmt As String = Config("/SQL/REPLACEMENT_ITEM")
        Dim ds As DataSet = New DataSet
        Dim outputParameter(0) As DBHelper.DBHelperParameter
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("pi_claim_id", claimId.ToByteArray)}
        outputParameter(PO_CURSOR_Equipment) = New DBHelper.DBHelperParameter("po_cursor_result", GetType(DataSet))

        Try

            DBHelper.FetchSp(selectStmt, parameters, outputParameter, ds, "GetReplacementItemData")
            ds.Tables(0).TableName = "GetReplacementItemData"

            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try



    End Function

    Public Function GetReplacementItemStatus(claimId As Guid, claimEquipmentId As Guid) As DataSet

        Dim selectStmt As String = Config("/SQL/REPLACEMENT_ITEM_STATUS")
        Dim outParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("po_cursor_Result", GetType(DataSet))}
        Dim ds As New DataSet
        Dim tbl As String = "ReplacementStatus"
        Dim inParameters As New List(Of DBHelper.DBHelperParameter)
        Dim param As DBHelper.DBHelperParameter

        param = New DBHelper.DBHelperParameter("pi_claim_id", claimId.ToByteArray)
        inParameters.Add(param)
        param = New DBHelper.DBHelperParameter("pi_claim_equipment_id", claimEquipmentId.ToByteArray)
        inParameters.Add(param)
        Try
            DBHelper.FetchSp(selectStmt, inParameters.ToArray, outParameters, ds, tbl, True)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function


End Class
