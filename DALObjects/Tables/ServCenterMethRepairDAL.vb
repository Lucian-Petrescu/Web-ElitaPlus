'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (5/26/2009)********************


Public Class ServCenterMethRepairDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_SERV_CENTER_METH_REPAIR"
    Public Const TABLE_KEY_NAME As String = "serv_center_meth_repair_id"

    Public Const COL_NAME_SERV_CENTER_MOR_ID As String = "serv_center_mor_id"
    Public Const COL_NAME_SERVICE_CENTER_ID As String = "service_center_id"
    Public Const COL_NAME_SERV_CENTER_METH_REPAIR_ID As String = "serv_center_meth_repair_id"
    Public Const ColNameServCenterServiceWarrantyDays As String = "service_warranty_days"


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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("serv_center_meth_repair_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, Me.TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    'Public Function LoadList() As DataSet
    '    Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST")
    '    Return DBHelper.Fetch(selectStmt, Me.TABLE_NAME)
    'End Function

    ' This Method's body was added manually
    Public Sub LoadList(ByVal ds As DataSet, ByVal serviceCenterMethodId As Guid)
        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST")
        Dim serviceCenterMethodParam As New DBHelper.DBHelperParameter("service_center_id", serviceCenterMethodId.ToByteArray)
        DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, New DBHelper.DBHelperParameter() {serviceCenterMethodParam})
    End Sub

    Public Function GetSelectedListMor(ByVal serviceCenterId As Guid) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/GET_SELECTED_LIST_MOR")
        Dim ds As New DataSet
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("service_center_id", serviceCenterId.ToByteArray)}
        Try
            DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
            Return (ds)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function
#End Region

#Region "Overloaded Methods"
    Public Overloads Sub Update(ByVal ds As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing, Optional ByVal changesFilter As DataRowState = Nothing)
        If ds Is Nothing Then
            Return
        End If
        If Not ds.Tables(Me.TABLE_NAME) Is Nothing Then
            MyBase.Update(ds.Tables(Me.TABLE_NAME), Transaction, changesFilter)
        End If
    End Sub
#End Region


End Class


