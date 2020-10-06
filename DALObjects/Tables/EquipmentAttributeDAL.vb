Public Class EquipmentAttributeDAL
    Inherits DALBase
#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_EQUIPMENT_ATTRIBUTE"
    Public Const TABLE_KEY_NAME As String = "equipment_attribute_id"

    Public Const COL_NAME_EQUIPMENT_ID As String = "equipment_id"
    Public Const COL_NAME_EQUIPMENT_ATTRIBUTE_ID As String = "equipment_attribute_id"
    Public Const COL_NAME_ATTRIBUTE_ID As String = "attribute_id"
    Public Const COL_NAME_VALUE As String = "value"
    Public Const COL_NAME_EFFECTIVE As String = "effective"
    Public Const COL_NAME_EXPIRATION As String = "expiration"
    Public Const COL_NAME_CREATED_DATE As String = "created_date"
    Public Const COL_NAME_MODIFIED_DATE As String = "modified_date"
    Public Const COL_NAME_CREATED_BY As String = "created_by"
    Public Const COL_NAME_MODIFIED_BY As String = "modified_by"
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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(COL_NAME_EQUIPMENT_ATTRIBUTE_ID, id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub
#End Region
End Class
