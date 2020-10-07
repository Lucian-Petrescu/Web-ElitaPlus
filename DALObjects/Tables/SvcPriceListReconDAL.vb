'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (7/31/2012)********************
Imports System.Collections.Generic
Imports System.Linq

Public Class SvcPriceListReconDAL
    Inherits DALBase

#Region "Constants"
    Public Const TABLE_NAME As String = "elp_Svc_Price_List_Recon"
    Public Const TABLE_KEY_NAME As String = "Svc_Price_List_Recon_id"

    Public Const COL_NAME_SVC_PRICE_LIST_RECON_ID As String = "svc_price_list_recon_id"
    Public Const COL_NAME_SERVICE_CENTER_ID As String = "service_center_id"
    Public Const COL_NAME_SERVICE_CENTER_CODE As String = "code"
    Public Const COL_NAME_PRICE_LIST_CODE As String = "code"
    Public Const COL_NAME_PRICE_LIST_ID As String = "price_list_id"
    Public Const COL_NAME_STATUS_XCD As String = "status_xcd"
    Public Const COL_NAME_STATUS_DATE As String = "status_date"
    Public Const COL_NAME_REQUESTED_DATE As String = "requested_date"
    Public Const COL_NAME_REQUESTED_BY As String = "requested_by"
    Public Const COL_NAME_STATUS_BY As String = "status_by"
    Public Const COL_NAME_PRICE_LIST_DESC As String = "description"

    Public Const PAR_IN_NAME_SVC_PRICE_LIST_RECON_ID As String = "pi_svc_price_list_recon_id"
    Public Const PAR_IN_NAME_SERVICE_CENTER_ID As String = "pi_service_center_id"
    Public Const PAR_IN_NAME_SERVICE_CENTER_CODE As String = "pi_service_center_code"
    Public Const PAR_IN_NAME_PRICE_LIST_CODE As String = "pi_price_list_code"
    Public Const PAR_IN_NAME_PRICE_LIST_ID As String = "pi_price_list_id"
    Public Const PAR_IN_NAME_STATUS_XCD As String = "pi_status_xcd"
    Public Const PAR_IN_COL_NAME_STATUS_DATE As String = "pi_status_date"
    Public Const PAR_IN_COL_NAME_REQUESTED_BY As String = "pi_requested_by"
    Public Const PAR_IN_NAME_COUNTRY_ID As String = "pi_country_id"


    'stored procedure parameter names

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

        Dim parameters() As OracleParameter = New OracleParameter() {New OracleParameter(PAR_IN_NAME_SVC_PRICE_LIST_RECON_ID, OracleDbType.Raw, id.ToByteArray, ParameterDirection.Input),
                                                                     New OracleParameter("po_resultcursor", OracleDbType.RefCursor, ParameterDirection.Output),
                                                                     New OracleParameter(PAR_OUT_NAME_RETURN_CODE, OracleDbType.Int32, ParameterDirection.Output)}

        Try
            FetchStoredProcedure("Load",
                                  selectStmt,
                                  parameters,
                                  familyDS)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function Load(id As Guid) As DataSet

        Dim selectStmt As String = Config("/SQL/LOAD")

        Dim ds As New DataSet

        Dim parameters() As OracleParameter = New OracleParameter() {New OracleParameter(PAR_IN_NAME_SVC_PRICE_LIST_RECON_ID, OracleDbType.Raw, id.ToByteArray, ParameterDirection.Input),
                                                                     New OracleParameter("po_resultcursor", OracleDbType.RefCursor, ParameterDirection.Output),
                                                                     New OracleParameter(PAR_OUT_NAME_RETURN_CODE, OracleDbType.Int16, ParameterDirection.Output)}


        FetchStoredProcedure("Load",
                             selectStmt,
                             parameters,
                             ds)

        Return ds
    End Function

    Public Sub LoadListByServiceCenter(familyDS As DataSet, servicecenterid As Guid)
        Dim selectStmt As String = Config("/SQL/LOAD_LIST_SVC")
        Dim ds As New DataSet
        Dim parameters() As OracleParameter = New OracleParameter() {New OracleParameter(PAR_IN_NAME_SERVICE_CENTER_ID, OracleDbType.Raw, servicecenterid.ToByteArray, ParameterDirection.Input),
                                                                     New OracleParameter("po_svc_pl_recon_table", OracleDbType.RefCursor, ParameterDirection.Output),
                                                                     New OracleParameter(PAR_OUT_NAME_RETURN_CODE, OracleDbType.Int32, ParameterDirection.Output)}
        FetchStoredProcedure("Load",
                              selectStmt,
                              parameters,
                              familyDS)

    End Sub

    Public Function LoadList(ServiceCenterCode As String, PriceListCode As String, CountryID As Guid) As DataSet

        Dim selectStmt As String = Config("/SQL/LOAD_LIST")

        Dim parameters() As OracleParameter = New OracleParameter() {New OracleParameter(PAR_IN_NAME_SERVICE_CENTER_CODE, OracleDbType.Varchar2, ParameterDirection.Input),
                                                                     New OracleParameter(PAR_IN_NAME_PRICE_LIST_CODE, OracleDbType.Varchar2, ParameterDirection.Input),
                                                                     New OracleParameter(PAR_IN_NAME_COUNTRY_ID, OracleDbType.Raw, CountryID.ToByteArray, ParameterDirection.Input),
                                                                     New OracleParameter("po_resultcursor", OracleDbType.RefCursor, ParameterDirection.Output),
                                                                     New OracleParameter(PAR_OUT_NAME_RETURN_CODE, OracleDbType.Int16, ParameterDirection.Output)}

        Return FetchStoredProcedure("LoadList",
                                    selectStmt,
                                    parameters)

    End Function

    Public Function LoadListBySvc(servicecenterid As Guid)
        Dim selectStmt As String = Config("/SQL/LOAD_LIST_SVC")
        Dim ds As New DataSet
        Dim parameters() As OracleParameter = New OracleParameter() {New OracleParameter(PAR_IN_NAME_SERVICE_CENTER_ID, OracleDbType.Raw, servicecenterid.ToByteArray, ParameterDirection.Input),
                                                                     New OracleParameter("po_resultcursor", OracleDbType.RefCursor, ParameterDirection.Output),
                                                                     New OracleParameter(PAR_OUT_NAME_RETURN_CODE, OracleDbType.Int32, ParameterDirection.Output)}
        FetchStoredProcedure("loadListBySVC",
                              selectStmt,
                              parameters,
                              ds)
        Return ds

    End Function

    Public Function GetLatestStatus(servicecenterid As Guid)
        Dim selectStmt As String = Config("/SQL/LOAD_LATEST_STATUS")
        ' Dim ds As New DataSet
        Dim parameters() As OracleParameter = New OracleParameter() {New OracleParameter(PAR_IN_NAME_SERVICE_CENTER_ID, OracleDbType.Raw, servicecenterid.ToByteArray, ParameterDirection.Input),
                                                                     New OracleParameter("po_resultcursor", OracleDbType.RefCursor, ParameterDirection.Output),
                                                                     New OracleParameter(PAR_OUT_NAME_RETURN_CODE, OracleDbType.Int32, ParameterDirection.Output)}
        Dim ds As New DataSet
        FetchStoredProcedure("Load",
                              selectStmt,
                              parameters,
                              ds)
        Return ds
    End Function

#End Region

#Region "Private Methods"

    Function SetParameter(name As String, value As Object) As DBHelper.DBHelperParameter

        name = name.Trim
        If value Is Nothing Then value = DBNull.Value
        If Not value Is Nothing AndAlso value.GetType Is GetType(String) Then value = DirectCast(value, String).Trim

        Return New DBHelper.DBHelperParameter(name, value, value.GetType)

    End Function
#End Region


#Region "Public Members"

    Public Function Add(svc_price_list_recon_id As Guid, servicenterId As Guid, price_list_id As Guid, status_xcd As String, Requested_By As String) As Integer
        Dim selectStmt As String = Config("/SQL/INSERT")

        Dim inputParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {
                            New DBHelper.DBHelperParameter("pi_svc_price_list_recon_id", svc_price_list_recon_id.ToByteArray),
                            New DBHelper.DBHelperParameter("pi_service_center_id", servicenterId.ToByteArray),
                            New DBHelper.DBHelperParameter("pi_price_list_id", price_list_id.ToByteArray),
                            New DBHelper.DBHelperParameter("pi_status_xcd", status_xcd),
                            New DBHelper.DBHelperParameter("pi_requested_by", Requested_By)}
        Dim outputParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {
                            New DBHelper.DBHelperParameter("po_return_code", GetType(Integer))}

        DBHelper.ExecuteSp(selectStmt, inputParameters, outputParameters)

        Dim retVal As Integer
        retVal = CType(outputParameters(0).Value, Integer)

        Return retVal
    End Function

    Public Function LoadList(ParentProductCodeId As Guid, familyDS As DataSet)

        Dim selectStmt As String = Config("/SQL/ChildListByParentID")
        ' Dim ds As DataSet
        Dim dcPk As DataColumnCollection

        Dim parameters() As OracleParameter
        parameters = New OracleParameter() {New OracleParameter(COL_NAME_SERVICE_CENTER_ID, ParentProductCodeId.ToByteArray)}

        Try

            DBHelper.Fetch(familyDS, selectStmt, TABLE_NAME, parameters)

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function
    Public Overloads Function Update_Status(svc_price_list_recon_id As Guid, status_xcd As String, status_changed_by As String) As Integer
        Dim selectStmt As String = Config("/SQL/UPDATE_STATUS")

        Dim inputParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {
                            New DBHelper.DBHelperParameter("pi_svc_price_list_recon_id", svc_price_list_recon_id.ToByteArray),
                            New DBHelper.DBHelperParameter("pi_status_xcd", status_xcd),
                            New DBHelper.DBHelperParameter("pi_status_changed_by", status_changed_by)}
        Dim outputParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {
                            New DBHelper.DBHelperParameter("po_return_code", GetType(Integer))}


        DBHelper.ExecuteSp(selectStmt, inputParameters, outputParameters)

        Dim retVal As Integer
        retVal = CType(outputParameters(0).Value, Integer)

        Return retVal
    End Function

#End Region

#Region "Overloaded Methods"
    Public Overloads Sub Update(ds As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing, Optional ByVal changesFilter As DataRowState = Nothing)
        If ds Is Nothing Then
            Return
        End If
        If Not ds.Tables(TABLE_NAME) Is Nothing Then
            MyBase.UpdateFromSP(ds.Tables(TABLE_NAME), Transaction, changesFilter)
        End If
    End Sub

#End Region

    Private Function FetchStoredProcedure(methodName As String, storedProc As String, parameters() As OracleParameter, Optional familyDS As DataSet = Nothing) As DataSet
        Dim tbl As String = TABLE_NAME
        Dim ds As DataSet = If(familyDS Is Nothing, New DataSet(), familyDS)

        ds.Tables.Add(tbl)

        ' Call DBHelper Store Procedure
        Try
            Using connection As New OracleConnection(DBHelper.ConnectString)
                Using cmd As OracleCommand = OracleDbHelper.CreateCommand(storedProc, CommandType.StoredProcedure, connection)
                    cmd.BindByName = True
                    cmd.Parameters.AddRange(parameters)
                    OracleDbHelper.Fetch(cmd, tbl, ds)
                End Using
            End Using
            Dim par = parameters.FirstOrDefault(Function(p As OracleParameter) p.ParameterName.Equals(PAR_OUT_NAME_RETURN_CODE))
            If (Not par Is Nothing AndAlso par.Value = 200) Then
                Throw New ElitaPlusException("SVC_PL_RECON - " + methodName, Common.ErrorCodes.DB_READ_ERROR)
            End If
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

        Return ds
    End Function

    Protected Overrides Sub ConfigureDeleteCommand(ByRef command As OracleCommand, tableName As String)
        command.AddParameter("pi_svc_price_list_recon_id", OracleDbType.Raw, sourceColumn:=TABLE_KEY_NAME)
        command.AddParameter(PAR_OUT_NAME_RETURN_CODE, OracleDbType.Int32, ParameterDirection.Output)
    End Sub


    Protected Overrides Sub ConfigureInsertCommand(ByRef command As OracleCommand, tableName As String)
        With command
            .AddParameter("pi_svc_price_list_recon_id", OracleDbType.Raw, sourceColumn:=TABLE_KEY_NAME)
            .AddParameter("pi_service_center_id", OracleDbType.Raw, sourceColumn:=COL_NAME_SERVICE_CENTER_ID)
            .AddParameter("pi_price_list_id", OracleDbType.Raw, sourceColumn:=COL_NAME_PRICE_LIST_ID)
            .AddParameter("pi_status_xcd", OracleDbType.Varchar2, sourceColumn:=COL_NAME_STATUS_XCD)
            .AddParameter("pi_requested_by", OracleDbType.Varchar2, sourceColumn:=COL_NAME_REQUESTED_BY)
            .AddParameter(PAR_OUT_NAME_RETURN_CODE, OracleDbType.Int32, ParameterDirection.Output)

        End With
    End Sub

    Protected Overrides Sub ConfigureUpdateCommand(ByRef command As OracleCommand, tableName As String)
        With command

            .AddParameter("pi_svc_price_list_recon_id", OracleDbType.Raw, sourceColumn:=TABLE_KEY_NAME)
            .AddParameter("pi_price_list_id", OracleDbType.Raw, sourceColumn:=COL_NAME_PRICE_LIST_ID)
            .AddParameter("pi_status_xcd", OracleDbType.Varchar2, sourceColumn:=COL_NAME_STATUS_XCD)
            .AddParameter("pi_requested_by", OracleDbType.Varchar2, sourceColumn:=COL_NAME_REQUESTED_BY)
            .AddParameter(PAR_OUT_NAME_RETURN_CODE, OracleDbType.Int32, ParameterDirection.Output)
        End With
    End Sub


End Class




