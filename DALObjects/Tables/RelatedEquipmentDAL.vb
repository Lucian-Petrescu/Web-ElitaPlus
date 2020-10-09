'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (2/9/2012)********************


Public Class RelatedEquipmentDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_RELATED_EQUIPMENT"
    Public Const TABLE_KEY_NAME As String = "related_equipment_id"

    Public Const COL_NAME_RELATED_EQUIPMENT_ID As String = "related_equipment_id"
    Public Const COL_NAME_EQUIPMENT_ID As String = "equipment_id"

    Public Const COL_NAME_CHILD_EQUIPMENT_ID As String = "child_equipment_id"
    Public Const COL_NAME_IS_IN_OEM_BOX_ID As String = "is_in_oem_box_id"
    Public Const COL_NAME_IS_COVERED_ID As String = "is_covered_id"
    Public Const COL_NAME_EQUIPMENT_MANUFACTURER_ID As String = "equipment_mfg_id"
    Public Const COL_NAME_EQUIPMENT_TYPE_ID As String = "equipment_type_id"

    Public Const COL_NAME_EQUIPMENT_TYPE As String = "equipment_type"
    Public Const COL_NAME_EQUIPMENT_DESCRIPTION As String = "equipment_description"
    Public Const COL_NAME_EQUIPMENT_MODEL As String = "equipment_model"
    Public Const COL_NAME_IS_IN_OEM_BOX As String = "In_oem_box"
    Public Const COL_NAME_IS_COVERED As String = "Is_Covered"
    Public Const COL_NAME_EQUIPMENT_MANUFACTURER As String = "equipment_mfg"


#End Region

#Region "Constructors"
    Public Sub New()
        MyBase.new()
    End Sub

#End Region

#Region "Load Methods"

    Public Sub LoadSchema(ByRef ds As DataSet)
        Load(ds, Guid.Empty)
    End Sub

    Public Sub Load(familyDS As DataSet, id As Guid)
        Dim selectStmt As String = Config("/SQL/LOAD")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(COL_NAME_RELATED_EQUIPMENT_ID, id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Sub LoadList(familyDS As DataSet, EquipmentId As Guid)
        Dim selectStmt As String = Config("/SQL/LOAD_LIST")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(COL_NAME_EQUIPMENT_ID, EquipmentId.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, TABLE_NAME, parameters)
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
        If ds.Tables(TABLE_NAME) IsNot Nothing Then
            MyBase.Update(ds.Tables(TABLE_NAME), Transaction, changesFilter)
        End If
    End Sub
#End Region

#Region "Public Methods"
    Public Function GetRelatedEquipmentList(equipmentId As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/GETRELATEDEQUIPMENT")
        Dim ds As New DataSet
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(COL_NAME_EQUIPMENT_ID, equipmentId.ToByteArray)}
        Try
            Return (DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters))
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function
#End Region




End Class
