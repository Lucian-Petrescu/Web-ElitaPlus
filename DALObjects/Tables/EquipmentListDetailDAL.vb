
'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (2/28/2012)********************


Public Class EquipmentListDetailDAL
    Inherits DALBase

#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_EQUIPMENT_LIST_DETAIL"
    Public Const TABLE_KEY_NAME As String = "equipment_list_detail_id"

    Public Const COL_NAME_EQUIPMENT_LIST_DETAIL_ID As String = "equipment_list_detail_id"
    Public Const COL_NAME_EQUIPMENT_ID As String = "equipment_id"
    Public Const COL_NAME_EQUIPMENT_LIST_ID As String = "equipment_list_id"
    Public Const COL_NAME_EFFECTIVE As String = "effective"
    Public Const COL_NAME_EXPIRATION As String = "expiration"

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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("equipment_list_detail_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Sub LoadList(familyDS As DataSet, id As Guid)
        Dim selectStmt As String = Config("/SQL/LOAD_LIST")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("equipment_list_detail_id", id.ToByteArray)}
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
    Public Function IsChild(equipmentListId As Guid, equipmentId As Guid, companyIds As ArrayList, languageId As Guid) As DataSet

        Dim selectStmt As String = Config("/SQL/IS_CHILD")
        Dim whereClauseConditions As String = String.Empty
        Dim ds As New DataSet
        Dim EquipmentIdParam As DBHelper.DBHelperParameter
        Dim EquipmentListIdParam As DBHelper.DBHelperParameter
        Dim bIsLikeClause As Boolean = False
        Dim bIsWhereClause As Boolean = False

        Try

            Dim params() As DBHelper.DBHelperParameter = {New DBHelper.DBHelperParameter(COL_NAME_EQUIPMENT_ID, equipmentId.ToByteArray), _
                                                          New DBHelper.DBHelperParameter(COL_NAME_EQUIPMENT_LIST_ID, equipmentListId.ToByteArray)}
            Return DBHelper.Fetch(ds, selectStmt, TABLE_NAME, params)

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function GetEquipmentsInList(equipmentListId As Guid, companyIds As ArrayList, languageId As Guid) As DataSet

        Dim selectStmt As String = Config("/SQL/EQUIPMENT_LIST")
        Dim whereClauseConditions As String = String.Empty
        Dim ds As New DataSet
        Dim EquipmentListIdParam As DBHelper.DBHelperParameter
        Dim bIsLikeClause As Boolean = False
        Dim bIsWhereClause As Boolean = False

        Try
            EquipmentListIdParam = New DBHelper.DBHelperParameter(COL_NAME_EQUIPMENT_LIST_ID, equipmentListId.ToByteArray)
            Return DBHelper.Fetch(ds, selectStmt, TABLE_NAME, New DBHelper.DBHelperParameter() {EquipmentListIdParam})
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function


    Public Function GetEquipmentExpiration(equipmentId As Guid, companyIds As ArrayList, languageId As Guid) As DataSet

        Dim selectStmt As String = Config("/SQL/EQUIPMENT_EXPIRATION")
        Dim whereClauseConditions As String = String.Empty
        Dim ds As New DataSet
        Dim EquipmentIdParam As DBHelper.DBHelperParameter
        Dim bIsLikeClause As Boolean = False
        Dim bIsWhereClause As Boolean = False

        Try
            EquipmentIdParam = New DBHelper.DBHelperParameter(COL_NAME_EQUIPMENT_ID, equipmentId)
            Return DBHelper.Fetch(ds, selectStmt, TABLE_NAME, New DBHelper.DBHelperParameter() {EquipmentIdParam})
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function GetEquipmentEffective(equipmentId As Guid, companyIds As ArrayList, languageId As Guid) As DataSet

        Dim selectStmt As String = Config("/SQL/EQUIPMENT_EFFECTIVE")
        Dim whereClauseConditions As String = String.Empty
        Dim ds As New DataSet
        Dim EquipmentIdParam As DBHelper.DBHelperParameter
        Dim bIsLikeClause As Boolean = False
        Dim bIsWhereClause As Boolean = False

        Try
            EquipmentIdParam = New DBHelper.DBHelperParameter(COL_NAME_EQUIPMENT_ID, equipmentId)
            Return DBHelper.Fetch(ds, selectStmt, TABLE_NAME, New DBHelper.DBHelperParameter() {EquipmentIdParam})
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function GetCurrentDateTime() As DateTime

        Dim selectStmt As String = Config("/SQL/CURRENT_TIME_STAMP")
        Dim whereClauseConditions As String = String.Empty
        Dim ds As New DataSet
        Dim EquipmentIdParam As DBHelper.DBHelperParameter

        Try
            Return (DBHelper.Fetch(ds, selectStmt, TABLE_NAME, New DBHelper.DBHelperParameter() {}).Tables(0).Rows(0)("SYSDATE"))
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function
#End Region

End Class



