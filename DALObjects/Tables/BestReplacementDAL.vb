Public Class BestReplacementDAL
    Inherits DALBase

#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_BEST_REPLACEMENT"
    Public Const TABLE_KEY_NAME As String = COL_NAME_BEST_REPLACEMENT_ID

    Public Const COL_NAME_BEST_REPLACEMENT_ID As String = "best_replacement_id"
    Public Const COL_NAME_MIGRATION_PATH_ID As String = "migration_path_id"
    Public Const COL_NAME_EQUIPMENT_ID As String = "equipment_id"
    Public Const COL_NAME_NUMBER_OF_REPLACEMENTS As String = "number_of_replacements"
    Public Const COL_NAME_REPLACEMENT_EQUIPMENT_ID As String = "replacement_equipment_id"
    Public Const COL_NAME_PRIORITY As String = "priority"
    Public Const COL_NAME_EQUIPMENT_MANUFACTURER As String = "equipment_mfg"
    Public Const COL_NAME_EQUIPMENT_MODEL As String = "equipment_model"
    Public Const COL_NAME_REPLACEMENT_EQUIPMENT_MANUFACTURER As String = "replacement_equipment_mfg"
    Public Const COL_NAME_REPLACEMENT_EQUIPMENT_MODEL As String = "replacement_equipment_model"
    Public Const COL_NAME_EQUIPMENT_MANUFACTURER_ID As String = "equipment_mfg_id"
    Public Const COL_NAME_REPLACEMENT_EQUIPMENT_MANUFACTURER_ID As String = "replacement_equipment_mfg_id"
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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(COL_NAME_BEST_REPLACEMENT_ID, id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadList(familyDs As DataSet, migrationPathId As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_LIST")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(COL_NAME_MIGRATION_PATH_ID, migrationPathId.ToByteArray)}

        Try
            Return (DBHelper.Fetch(familyDs, selectStmt, TABLE_NAME, parameters))
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function GetReplacementEquipments(migrationPathId As Guid, equipmentId As Guid, numberOfreplacements As Integer) As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_BEST_REPLACEMENT")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
            {New DBHelper.DBHelperParameter(COL_NAME_MIGRATION_PATH_ID, migrationPathId.ToByteArray), _
             New DBHelper.DBHelperParameter(COL_NAME_EQUIPMENT_ID, equipmentId.ToByteArray), _
             New DBHelper.DBHelperParameter(COL_NAME_NUMBER_OF_REPLACEMENTS, numberOfreplacements)}
        Try
            Dim ds As New DataSet
            DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function
#End Region

#Region "Overloaded Methods"
    Public Overloads Sub Update(ds As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing, Optional ByVal changesFilter As DataRowState = Nothing)
        If Not ds.Tables(TABLE_NAME) Is Nothing Then
            MyBase.Update(ds.Tables(TABLE_NAME), Transaction, changesFilter)
        End If
    End Sub
#End Region

End Class
