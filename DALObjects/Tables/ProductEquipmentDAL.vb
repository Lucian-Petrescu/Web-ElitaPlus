Public Class ProductEquipmentDAL
    Inherits DALBase

#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_PRD_ITEM_MANUF_EQUIPMENT" ' Table name
    Public Const TABLE_NAME_BENEFITS As String = "ELP_PRD_ITEM_MANUF_EQUIPMENT_AS_BENEFITS" ' Table name
    Public Const TABLE_KEY_NAME As String = COL_NAME_PROD_ITEM_MANUF_EQUIP_ID ' Table key

    ' Table Column name
    Public Const COL_NAME_PROD_ITEM_MANUF_EQUIP_ID As String = "prod_item_manuf_equip_id"
    Public Const COL_NAME_PRODUCT_CODE_ID As String = "product_code_id"
    Public Const COL_NAME_EFFECTIVE_DATE_PRODUCT_EQUIP As String = "effective_date_product_equip"
    Public Const COL_NAME_EXPIRATION_DATE_PRODUCT_EQUIP As String = "expiration_date_product_equip"
    Public Const COL_NAME_CREATE_MODIFY_DATE_PRODUCT_EQUIP As String = "create_modify_date_product_equip"
    Public Const COL_NAME_ITEM_ID As String = "item_id"
    Public Const COL_NAME_MANUFACTURER_ID As String = "manufacturer_id"
    Public Const COL_NAME_EQUIPMENT_ID As String = "equipment_id"
    Public Const COL_NAME_DEVICE_TYPE_ID As String = "device_type_id"
    Public Const COL_NAME_METHOD_OF_REPAIR_XCD As String = "method_of_repair_xcd"
    Public Const COL_NAME_CONFIG_PURPOSE_XCD As String = "config_purpose_xcd"
    Public Const COL_NAME_EQUIPMENT_MAKE As String = "equipment_make"
    Public Const COL_NAME_EQUIPMENT_MODEL As String = "equipment_model"

    Public Const BENEFITS_PURPOSE As String = "PRDITEMCONFIGPURPOSE-BENEF"
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
        Dim inputparameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(TABLE_KEY_NAME, id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, TABLE_NAME, inputparameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub
    Public Function LoadList(ProductCodeId As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_LIST")
        Dim ds As New DataSet
        Dim inputparameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(COL_NAME_PRODUCT_CODE_ID, ProductCodeId.ToByteArray)}

        DBHelper.Fetch(ds, selectStmt, TABLE_NAME, inputparameters)
        Return ds
    End Function
    Public Function LoadBenefitsList(familyDS As DataSet, ProductCodeId As Guid)
        Dim selectStmt As String = Config("/SQL/LOAD_BENEFITS_LIST")
        Dim inputparameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(COL_NAME_PRODUCT_CODE_ID, ProductCodeId.ToByteArray)}

        DBHelper.Fetch(familyDS, selectStmt, TABLE_NAME_BENEFITS, inputparameters)

    End Function
    Public Function LoadList(ds As DataSet, ProductCodeId As Guid)
        Dim selectStmt As String = Config("/SQL/LOAD_LIST")
        Dim whereClauseConditions As String = ""
        whereClauseConditions &= Environment.NewLine & " AND trunc(expiration_date_product_equip) > trunc(sysdate) "
        selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
        Dim inputparameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(COL_NAME_PRODUCT_CODE_ID, ProductCodeId.ToByteArray)}

        DBHelper.Fetch(ds, selectStmt, TABLE_NAME, inputparameters)

    End Function
    Public Function LoadProdManuEquipList(ProductCodeId As Guid, ManufacturerId As Guid, EquipmentId As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_PROD_MANU_EQUIP_LIST")
        Dim ds As New DataSet
        Dim inputparameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
                                                                {New DBHelper.DBHelperParameter(COL_NAME_PRODUCT_CODE_ID, ProductCodeId.ToByteArray),
                                                                 New DBHelper.DBHelperParameter(COL_NAME_MANUFACTURER_ID, ManufacturerId.ToByteArray),
                                                                 New DBHelper.DBHelperParameter(COL_NAME_EQUIPMENT_ID, EquipmentId.ToByteArray)
                                                                }

        DBHelper.Fetch(ds, selectStmt, TABLE_NAME, inputparameters)
        Return ds
    End Function
#End Region

#Region "Overloaded Methods"
    Public Overloads Sub Update(ds As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing, Optional ByVal changesFilter As DataRowState = Nothing)
        If ds Is Nothing Then
            Return
        End If
        If Not ds.Tables(TABLE_NAME) Is Nothing Then
            MyBase.Update(ds.Tables(TABLE_NAME), Transaction, changesFilter)
        End If
        If Not ds.Tables(TABLE_NAME_BENEFITS) Is Nothing Then
            MyBase.Update(ds.Tables(TABLE_NAME_BENEFITS), Transaction, changesFilter)
        End If
    End Sub
#End Region
End Class
