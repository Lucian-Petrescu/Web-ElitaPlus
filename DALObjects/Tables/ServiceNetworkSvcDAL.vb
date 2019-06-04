Public Class ServiceNetworkSvcDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_SERVICE_NETWORK_SVC"
    Public Const TABLE_KEY_NAME As String = "service_network_svc_id"

    Public Const COL_NAME_SERVICE_NETWORK_SVC_ID As String = "service_network_svc_id"
    Public Const COL_NAME_SERVICE_NETWORK_ID As String = "service_network_id"
    Public Const COL_NAME_SERVICE_CENTER_ID As String = "service_center_id"
    Private Const DSNAME As String = "LIST"

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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(COL_NAME_SERVICE_NETWORK_SVC_ID, id.ToByteArray)}
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

    'This Method's body was added manually
    Public Sub LoadList(ByVal ds As DataSet, ByVal ServiceNetworkSvcId As Guid)
        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST")
        Dim parameters As New DBHelper.DBHelperParameter(TABLE_KEY_NAME, ServiceNetworkSvcId.ToByteArray)
        DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, New DBHelper.DBHelperParameter() {parameters})
    End Sub

    Public Sub LoadNedtworkList(ByVal ds As DataSet, ByVal ServiceCenterId As Guid)
        Dim selectStmt As String = Me.Config("/SQL/LOAD_NETWORK_LIST")
        Dim parameters As New DBHelper.DBHelperParameter(TABLE_KEY_NAME, ServiceCenterId.ToByteArray)
        DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, New DBHelper.DBHelperParameter() {parameters})
    End Sub

    Public Sub LoadByUserIdCompanyID(ByVal familyDS As DataSet, ByVal svrNetId As Guid, ByVal servCentId As Guid)
        Dim selectStmt As String = Me.Config("/SQL/LOAD_BY_SERVNETID_SERVCENTID")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() { _
                    New DBHelper.DBHelperParameter(Me.COL_NAME_SERVICE_NETWORK_ID, svrNetId.ToByteArray), _
                    New DBHelper.DBHelperParameter(Me.COL_NAME_SERVICE_CENTER_ID, servCentId.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, Me.TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadNetworkServicenterIDs(ByVal ServiceNetworkId As Guid) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/LOAD_NETWORK_SERVICE_CENTER_IDs")
        Dim parameters() As OracleParameter

        parameters = New OracleParameter() _
                                    {New OracleParameter(COL_NAME_SERVICE_NETWORK_ID, ServiceNetworkId.ToByteArray)} ', _
        ' New OracleParameter("server_center_meth_repair_id", oMethodRepairId.ToByteArray)}
        Try
            Return (DBHelper.Fetch(selectStmt, DSNAME, Me.TABLE_NAME, parameters))
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function LoadAllNetworkServicenterIDs(ByVal companyGroupID As Guid) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/LOAD_ALL_NETWORK_SERVICE_CENTER_IDs")
        Dim parameters() As OracleParameter

        parameters = New OracleParameter() _
                                      {New OracleParameter("company_group_id", companyGroupID.ToByteArray)} ', _
        '   New OracleParameter("server_center_meth_repair_id", oMethodRepairId.ToByteArray)}
        Try
            Return (DBHelper.Fetch(selectStmt, DSNAME, Me.TABLE_NAME, parameters))
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

