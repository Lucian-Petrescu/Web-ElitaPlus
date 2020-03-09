Imports Assurant.ElitaPlus.DALObjects
Public Class SvcPriceListReconDAL
    Inherits DALBase

#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_SVC_PRICE_LIST_RECON"
    Public Const TABLE_KEY_NAME As String = "svc_price_list_recon_id"
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

#End Region

#Region "Constructors"

    Public Sub New()
        MyBase.New()
    End Sub

#End Region

#Region "Load Methods"

    Public Sub LoadSchema(ByVal ds As DataSet)
        Load(ds, Guid.Empty)
    End Sub

    Public Sub Load(ByVal familyDS As DataSet, ByVal id As Guid)
        Dim selectStmt As String = Me.Config("/SQL/LOAD")

        Dim parameters() As OracleParameter = New OracleParameter() {New OracleParameter(Me.PAR_IN_NAME_SVC_PRICE_LIST_RECON_ID, OracleDbType.Raw, id.ToByteArray, ParameterDirection.Input),
                                                                     New OracleParameter("po_resultcursor", OracleDbType.RefCursor, ParameterDirection.Output),
                                                                     New OracleParameter(Me.PAR_OUT_NAME_RETURN_CODE, OracleDbType.Int32, ParameterDirection.Output)}
        FetchStoredProcedure("Load",
                              selectStmt,
                              parameters,
                              familyDS)
    End Sub

    Public Function Load(ByVal id As Guid) As DataSet

        Dim selectStmt As String = Me.Config("/SQL/LOAD")

        Dim ds As New DataSet

        Dim parameters() As OracleParameter = New OracleParameter() {New OracleParameter(Me.PAR_IN_NAME_SVC_PRICE_LIST_RECON_ID, OracleDbType.Raw, id.ToByteArray, ParameterDirection.Input),
                                                                     New OracleParameter("po_resultcursor", OracleDbType.RefCursor, ParameterDirection.Output),
                                                                     New OracleParameter(Me.PAR_OUT_NAME_RETURN_CODE, OracleDbType.Int16, ParameterDirection.Output)}


        FetchStoredProcedure("Load",
                             selectStmt,
                             parameters,
                             ds)

        Return ds
    End Function

    Public Function LoadList(ByVal ServiceCenterCode As String, ByVal PriceListCode As String, ByVal CountryID As Guid) As DataSet

        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST")

        Dim parameters() As OracleParameter = New OracleParameter() {New OracleParameter(Me.PAR_IN_NAME_SERVICE_CENTER_CODE, OracleDbType.Varchar2, ParameterDirection.Input),
                                                                     New OracleParameter(Me.PAR_IN_NAME_PRICE_LIST_CODE, OracleDbType.Varchar2, ParameterDirection.Input),
                                                                     New OracleParameter(Me.PAR_IN_NAME_COUNTRY_ID, OracleDbType.Raw, CountryID.ToByteArray, ParameterDirection.Input),
                                                                     New OracleParameter("po_resultcursor", OracleDbType.RefCursor, ParameterDirection.Output),
                                                                     New OracleParameter(Me.PAR_OUT_NAME_RETURN_CODE, OracleDbType.Int16, ParameterDirection.Output)}

        Return FetchStoredProcedure("LoadList",
                                    selectStmt,
                                    parameters)

    End Function

    Public Function LoadListBySvc(ByVal ServiceCenterID As Guid) As DataSet

        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST_BY_SVC")

        Dim parameters() As OracleParameter = New OracleParameter() {New OracleParameter(Me.PAR_IN_NAME_SERVICE_CENTER_ID, OracleDbType.Raw, ServiceCenterID.ToByteArray, ParameterDirection.Input),
                                                                     New OracleParameter("po_resultcursor", OracleDbType.RefCursor, ParameterDirection.Output),
                                                                     New OracleParameter(Me.PAR_OUT_NAME_RETURN_CODE, OracleDbType.Int16, ParameterDirection.Output)}

        Return FetchStoredProcedure("loadListBySVC",
                                    selectStmt,
                                    parameters)

    End Function


#End Region

#Region "Private Methods"

    Function SetParameter(ByVal name As String, ByVal value As Object) As DBHelper.DBHelperParameter

        name = name.Trim
        If value Is Nothing Then value = DBNull.Value
        If Not value Is Nothing AndAlso value.GetType Is GetType(String) Then value = DirectCast(value, String).Trim

        Return New DBHelper.DBHelperParameter(name, value, value.GetType)

    End Function
#End Region

    Public Function LoadList(ByVal ParentProductCodeId As Guid, ByVal familyDS As DataSet)

        Dim selectStmt As String = Me.Config("/SQL/ChildListByParentID")
        ' Dim ds As DataSet
        Dim dcPk As DataColumnCollection

        Dim parameters() As OracleParameter
        parameters = New OracleParameter() {New OracleParameter(COL_NAME_SERVICE_CENTER_ID, ParentProductCodeId.ToByteArray)}

        Try

            DBHelper.Fetch(familyDS, selectStmt, Me.TABLE_NAME, parameters)

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function
#Region "Overloaded Methods"
    Public Overloads Sub Update(ByVal ds As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing, Optional ByVal changesFilter As DataRowState = Nothing)
        If ds Is Nothing Then
            Return
        End If
        If Not ds.Tables(Me.TABLE_NAME) Is Nothing Then
            MyBase.UpdateFromSP(ds.Tables(Me.TABLE_NAME), Transaction, changesFilter)
        End If
    End Sub
#End Region
    Private Function FetchStoredProcedure(methodName As String, storedProc As String, parameters() As OracleParameter, Optional familyDS As DataSet = Nothing) As DataSet
        Dim ds As DataSet = If(familyDS Is Nothing, New DataSet(), familyDS)
        Dim tbl As String = Me.TABLE_NAME

        If (ds.Tables Is Nothing OrElse ds.Tables.Count = 0) Then
            ds.Tables.Add(tbl)
        End If


        ' Call DBHelper Store Procedure
        Try
            Using connection As New OracleConnection(DBHelper.ConnectString)
                Using cmd As OracleCommand = OracleDbHelper.CreateCommand(storedProc, CommandType.StoredProcedure, connection)
                    cmd.BindByName = True
                    cmd.Parameters.AddRange(parameters)
                    OracleDbHelper.Fetch(cmd, tbl, ds)
                End Using
            End Using
            Dim par = parameters.FirstOrDefault(Function(p As OracleParameter) p.ParameterName.Equals(Me.PAR_OUT_NAME_RETURN_CODE))
            If (Not par Is Nothing And par.Value = 200) Then
                Throw New ElitaPlusException("SvcPriceListRecon - " + methodName, Common.ErrorCodes.DB_READ_ERROR)
            End If
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try


        Return ds
    End Function

    Protected Overrides Sub ConfigureInsertCommand(ByRef command As OracleCommand, ByVal tableName As String)
        With command
            .AddParameter("pi_svc_price_list_recon_id", OracleDbType.Raw, sourceColumn:=TABLE_KEY_NAME)
            .AddParameter("pi_service_center_id", OracleDbType.Raw, sourceColumn:=COL_NAME_SERVICE_CENTER_ID)
            .AddParameter("pi_price_list_id", OracleDbType.Raw, sourceColumn:=COL_NAME_PRICE_LIST_ID)
            .AddParameter("pi_status_xcd", OracleDbType.Varchar2, sourceColumn:=COL_NAME_STATUS_XCD)
            .AddParameter("pi_requested_by", OracleDbType.Varchar2, sourceColumn:=COL_NAME_REQUESTED_BY)
            .AddParameter(PAR_OUT_NAME_RETURN_CODE, OracleDbType.Int32, ParameterDirection.Output)

        End With
    End Sub

    Protected Overrides Sub ConfigureUpdateCommand(ByRef command As OracleCommand, ByVal tableName As String)
        'SVC_PRICE_LIST_RECON_ID N	RAW(16)	        N			N		
        'SERVICE_CENTER_ID       N	RAW(16)	        N			N		
        'PRICE_LIST_ID           N	RAW(16)	        N			N		
        'CREATED_BY              N	VARCHAR2(120)	N			N		
        'MODIFIED_BY             N	VARCHAR2(120)	Y			N		
        'CREATED_DATE            N	DATE	        N			N		
        'MODIFIED_DATE           N	DATE	        Y			N		
        'STATUS_XCD              N	VARCHAR2(120)	N			N		
        'STATUS_DATE             N	DATE	        N			N		
        'REQUESTED_BY            N	VARCHAR2(120)	N			N		
        'REQUESTED_DATE          N	DATE	        N	

        With command
            .AddParameter(PAR_IN_NAME_SVC_PRICE_LIST_RECON_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_SVC_PRICE_LIST_RECON_ID)
            .AddParameter(PAR_IN_NAME_PRICE_LIST_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_PRICE_LIST_ID)
            .AddParameter(PAR_IN_NAME_STATUS_XCD, OracleDbType.Varchar2, sourceColumn:=COL_NAME_STATUS_XCD)
            .AddParameter(PAR_IN_COL_NAME_REQUESTED_BY, OracleDbType.Varchar2, sourceColumn:=COL_NAME_REQUESTED_BY)
            .AddParameter(PAR_OUT_NAME_RETURN_CODE, OracleDbType.Int32, ParameterDirection.Output)
        End With

    End Sub

    Protected Overrides Sub ConfigureDeleteCommand(ByRef command As OracleCommand, ByVal tableName As String)
        command.AddParameter("pi_svc_price_list_recon_id", OracleDbType.Raw, sourceColumn:=TABLE_KEY_NAME)
        command.AddParameter(PAR_OUT_NAME_RETURN_CODE, OracleDbType.Int32, ParameterDirection.Output)
    End Sub
End Class
