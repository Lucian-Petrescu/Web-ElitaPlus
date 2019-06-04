'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (7/16/2008)********************


Public Class RouteDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_ROUTE"
    Public Const TABLE_KEY_NAME As String = "route_id"
    Public Const TABLE_NAME_ROUTE_SC As String = "ServiceCeter"
    Public Const COL_NAME_ROUTE_ID As String = "route_id"
    Public Const COL_NAME_DESCRIPTION As String = "description"
    Public Const COL_NAME_CODE As String = "code"
    'Public Const COL_NAME_COUNTRY_ID As String = "country_id"
    Public Const COL_NAME_SERVICE_NETWORK_ID = "service_network_id"
    Public Const COL_NAME_SERVICE_NETWORK_CODE = "service_network_code"
    Public Const COL_NAME_COMPANY_GROUP_ID = "company_group_id"
    Public Const COL_NAME_ROUTE_CODE = "code"

    Public Const COL_NAME_SHORT_DESC As String = "short_desc"
    Public Const DSNAME As String = "LIST"
    Public Const WILDCARD As Char = "%"


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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("route_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, Me.TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadList(ByVal companyGroupId As Guid, ByVal RouteId As Guid, ByVal serviceNetworkId As Guid) As DataSet


        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST")
        Dim parameters() As OracleParameter
        Dim whereClausecondition As String = ""


        Try
            Dim ds As New DataSet
            Dim parameter As DBHelper.DBHelperParameter

            If serviceNetworkId.Equals(Guid.Empty) AndAlso RouteId.Equals(Guid.Empty) Then
                parameters = New OracleParameter() _
                                    {New OracleParameter(ServiceNetworkDAL.COL_NAME_COMPANY_GROUP_ID, companyGroupId.ToByteArray), _
                                     New OracleParameter(COL_NAME_ROUTE_ID, GenericConstants.WILDCARD), _
                                     New OracleParameter(COL_NAME_SERVICE_NETWORK_ID, GenericConstants.WILDCARD)}
            ElseIf Not serviceNetworkId.Equals(Guid.Empty) AndAlso RouteId.Equals(Guid.Empty) Then
                parameters = New OracleParameter() _
                                    {New OracleParameter(ServiceNetworkDAL.COL_NAME_COMPANY_GROUP_ID, companyGroupId.ToByteArray), _
                                     New OracleParameter(COL_NAME_ROUTE_ID, GenericConstants.WILDCARD), _
                                     New OracleParameter(COL_NAME_SERVICE_NETWORK_ID, serviceNetworkId.ToByteArray)}
            ElseIf serviceNetworkId.Equals(Guid.Empty) AndAlso Not RouteId.Equals(Guid.Empty) Then
                parameters = New OracleParameter() _
                                    {New OracleParameter(ServiceNetworkDAL.COL_NAME_COMPANY_GROUP_ID, companyGroupId.ToByteArray), _
                                     New OracleParameter(COL_NAME_ROUTE_ID, RouteId.ToByteArray), _
                                     New OracleParameter(COL_NAME_SERVICE_NETWORK_ID, GenericConstants.WILDCARD)}
            Else
                parameters = New OracleParameter() _
                                    {New OracleParameter(ServiceNetworkDAL.COL_NAME_COMPANY_GROUP_ID, companyGroupId.ToByteArray), _
                                     New OracleParameter(COL_NAME_ROUTE_ID, RouteId.ToByteArray), _
                                     New OracleParameter(COL_NAME_SERVICE_NETWORK_ID, serviceNetworkId.ToByteArray)}
            End If
            Return (DBHelper.Fetch(selectStmt, DSNAME, Me.TABLE_NAME, parameters))
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
        'parameters = New OracleParameter() _
        '                    {New OracleParameter(ServiceNetworkDAL.COL_NAME_COMPANY_GROUP_ID, companyGroupId.ToByteArray), _
        '                     New OracleParameter(COL_NAME_ROUTE_ID, stRouteId), _
        '                     New OracleParameter(COL_NAME_SERVICE_NETWORK_ID, stSrvNwId)}
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

    'This method was added manually to accommodate BO families Save
    Public Overloads Sub UpdateFamily(ByVal familyDataset As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing)

        Dim scDal As new ServiceCenterDAL
        Dim tr As IDbTransaction = Transaction
        If tr Is Nothing Then
            tr = DBHelper.GetNewTransaction
        End If
        Try

            'First Pass updates Deletions
            'scDal.Update(familyDataset.GetChanges(DataRowState.Deleted), tr, DataRowState.Deleted)
            MyBase.Update(familyDataset.Tables(Me.TABLE_NAME).GetChanges(DataRowState.Deleted), tr, DataRowState.Deleted)

            'Second Pass updates additions and changes
            Update(familyDataset.Tables(Me.TABLE_NAME).GetChanges(DataRowState.Added Or DataRowState.Modified), tr, DataRowState.Added Or DataRowState.Modified)

            If familyDataset.Tables.Count > 1 Then
                scDal.Update(familyDataset.GetChanges(DataRowState.Added Or DataRowState.Modified), tr, DataRowState.Added Or DataRowState.Modified)
            End If

            If Transaction Is Nothing Then
                'We are the creator of the transaction we shoul commit it  and close the connection
                DBHelper.Commit(tr)
                familyDataset.AcceptChanges()
            End If
        Catch ex As Exception
            If Transaction Is Nothing Then
                'We are the creator of the transaction we shoul commit it  and close the connection
                DBHelper.RollBack(tr)
            End If
            Throw ex
        End Try
    End Sub

#Region "Public Methods"

    Public Sub LoadSelectedSCs(ByVal ds As DataSet, ByVal routeID As Guid)

        Dim selectStmt As String = Me.Config("/SQL/LOAD_SELECTED_SERVICE_CENTER_LIST")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(Me.COL_NAME_ROUTE_ID, routeID.ToByteArray)}
        DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME_ROUTE_SC, parameters)

    End Sub

    Public Sub LoadAvailableSCs(ByVal ds As DataSet, ByVal scNetworkId As Guid)

        Dim selectStmt As String = Me.Config("/SQL/LOAD_AVAILABLE_SERVICE_CENTER_LIST")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(Me.COL_NAME_SERVICE_NETWORK_ID, scNetworkId.ToByteArray)}
        DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME_ROUTE_SC, parameters)

    End Sub

    Public Function LoadList(ByVal scNetworkId As Guid) As DataSet

        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST_DYNAMIC")
        Dim parameters() As OracleParameter
        parameters = New OracleParameter() _
                            {New OracleParameter(COL_NAME_SERVICE_NETWORK_ID, scNetworkId.ToByteArray)}
        Try
            Return (DBHelper.Fetch(selectStmt, DSNAME, Me.TABLE_NAME, parameters))
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Function GetRouteByCode(ByVal companyGroupId As Guid, ByVal routeCode As String) As DataSet

        Dim selectStmt As String = Me.Config("/SQL/GET_ROUTE_BY_CODE")
        Dim parameters() As OracleParameter
        parameters = New OracleParameter() _
                            {New OracleParameter(COL_NAME_COMPANY_GROUP_ID, companyGroupId.ToByteArray), _
                             New OracleParameter(COL_NAME_ROUTE_CODE, routeCode)}
        Try
            Return (DBHelper.Fetch(selectStmt, DSNAME, Me.TABLE_NAME, parameters))
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function


#End Region
End Class


