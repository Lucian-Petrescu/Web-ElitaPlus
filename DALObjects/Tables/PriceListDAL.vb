'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (7/31/2012)********************


Public Class PriceListDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_PRICE_LIST_HEADER_RECON"
    Public Const TABLE_KEY_NAME As String = "PRICE_LIST_ID"
    Public Const COL_NAME_PRICE_LIST_ID As String = "PRICE_LIST_ID"
    Public Const COL_NAME_COUNTRY_ID As String = "COUNTRY_ID"
    Public Const COL_NAME_DEFAULT_CURRENCY_ID As String = "DEFAULT_CURRENCY_ID"

    Public Const COL_NAME_CODE As String = "CODE"
    Public Const COL_NAME_DESCRIPTION As String = "DESCRIPTION"
    Public Const COL_NAME_MANAGE_INVENTORY_ID As String = "MANAGE_INVENTORY_ID"
    Public Const COL_NAME_EFFECTIVE As String = "EFFECTIVE"
    Public Const COL_NAME_STATUS As String = "STATUS"
    Public Const COL_NAME_EXPIRATION As String = "EXPIRATION"

    Public Const COL_NAME_SERVICE_TYPE_ID = "pld.service_type_id"
    Public Const COL_NAME_SERVICE_CENTER_DESC = "sc.description"

    'US 224101 
    Public Const PAR_OUT_NAME_RETURN_CODE As String = "po_return_code"

    Public Const PAR_IN_NAME_LANGUAGE_ID As String = "pi_language_id"
    Public Const PAR_IN_NAME_SERVICE_TYPE_ID As String = "pi_service_type_id"
    Public Const PAR_IN_NAME_CODE As String = "pi_code"
    Public Const PAR_IN_NAME_DESCRIPTION As String = "pi_description"
    Public Const PAR_IN_NAME_COUNTRY_LIST As String = "pi_country_id_list"
    Public Const PAR_IN_NAME_COUNTRY_ID As String = "pi_country_id"
    Public Const PAR_IN_NAME_SERVICE_CENTER_DESCRIPTION As String = "pi_sc_description"
    Public Const PAR_IN_NAME_ACTIVATE_ON As String = "pi_activeon"
    Public Const PAR_IN_NAME_PRICE_LIST_ID As String = "pi_price_list_id"
    Public Const PAR_IN_NAME_MANAGE_INVENTORY_ID As String = "pi_manage_inventory_id"
    Public Const PAR_IN_NAME_STATUS_XCD As String = "pi_status_xcd"
    Public Const PAR_IN_NAME_PRICE_LIST_DETAIL_ID As String = "pi_price_list_detail_id"

    Public Const PAR_IN_NAME_EFFECTIVE As String = "pi_effective"
    Public Const PAR_IN_NAME_EXPIRATION As String = "pi_expiration"
    Public Const PAR_IN_NAME_CREATED_BY As String = "pi_created_by"
    Public Const PAR_IN_NAME_MODIFIED_BY As String = "pi_modified_by"
    Public Const PAR_IN_NAME_DEFAULT_CURRENCY_ID As String = "pi_default_currency_id"
    Public Const NULL_VALUE As String = "NULL"

    Public Const PAR_IN_NAME_STATUS_DATE As String = "status_date"
    Public Const PAR_IN_NAME_STATUS_BY As String = "pi_status_by"
    Public Const PAR_IN_NAME_PRICE_LIST_DETAIL_ID_LIST As String = "pi_price_list_detail_id_list"

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

        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("pi_price_list_id", id.ToByteArray)}
        Dim outputParameter(1) As DBHelper.DBHelperParameter
        outputParameter(0) = New DBHelper.DBHelperParameter("po_price_list", GetType(DataSet))
        outputParameter(1) = New DBHelper.DBHelperParameter(PAR_OUT_NAME_RETURN_CODE, GetType(Integer))
        Try
            DBHelper.FetchSp(selectStmt, parameters, outputParameter, familyDS, Me.TABLE_NAME)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadList(ByVal code As String, _
                             ByVal description As String, _
                             ByVal serviceType As Guid, _
                             ByVal countryList As String, _
                             ByVal serviceCenter As String, _
                             ByVal activeOn As DateType, _
                             ByVal languageId As Guid) As DataSet

        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST_SEARCH")
        Dim parameters() As OracleParameter

        'Deleting NULL string wherever it's found
        If (Not String.IsNullOrEmpty(countryList)) Then
            countryList = countryList.Replace("'", String.Empty)

            Dim nullIndex As Integer = countryList.IndexOf(NULL_VALUE, StringComparison.InvariantCultureIgnoreCase)
            Dim length = NULL_VALUE.Length

            If (nullIndex <> -1) Then
                If (countryList.Length > length) Then
                    length = length + 1
                End If

                If (nullIndex > 0) Then
                    nullIndex = nullIndex - 1
                End If

                countryList = countryList.Replace(countryList.Substring(nullIndex, length), String.Empty).Replace("'", String.Empty)
            End If
        End If


        parameters = New OracleParameter() {
                              New OracleParameter(PAR_IN_NAME_LANGUAGE_ID, OracleDbType.Raw, languageId.ToByteArray, ParameterDirection.Input),
                              New OracleParameter(PAR_IN_NAME_CODE, OracleDbType.Varchar2, If(Not String.IsNullOrEmpty(code), code.ToUpper(), Nothing), ParameterDirection.Input),
                              New OracleParameter(PAR_IN_NAME_DESCRIPTION, OracleDbType.Varchar2, If(Not String.IsNullOrEmpty(description), description.ToUpper, Nothing), ParameterDirection.Input),
                              New OracleParameter(PAR_IN_NAME_SERVICE_TYPE_ID, OracleDbType.Raw, If(IsNothing(serviceType) OrElse serviceType.Equals(Guid.Empty), Nothing, serviceType.ToByteArray), ParameterDirection.Input),
                              New OracleParameter(PAR_IN_NAME_COUNTRY_LIST, OracleDbType.Varchar2, If(String.IsNullOrEmpty(countryList), Nothing, countryList), ParameterDirection.Input),
                              New OracleParameter(PAR_IN_NAME_SERVICE_CENTER_DESCRIPTION, OracleDbType.Varchar2, If(String.IsNullOrEmpty(serviceCenter), serviceCenter, Nothing), ParameterDirection.Input),
                              New OracleParameter(PAR_IN_NAME_ACTIVATE_ON, OracleDbType.Varchar2, If(activeOn Is Nothing, Nothing, DateTime.Parse(activeOn).ToString("MM/dd/yyyy HH:mm:ss")), ParameterDirection.Input),
                              New OracleParameter("po_all_price_list", OracleDbType.RefCursor, ParameterDirection.Output),
                              New OracleParameter(PAR_OUT_NAME_RETURN_CODE, OracleDbType.Int16, ParameterDirection.Output)}

        Return FetchStoredProcedure("LoadList",
                                         selectStmt,
                                         parameters)

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

    Public Overloads Sub UpdateFamily(ByVal familyDataset As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing)

        Dim ServiceCenter As New ServiceCenterDAL
        Dim PriceListDetails As New PriceListDetailDAL
        Dim VendorQty As New VendorQuantityDAL

        Dim tr As IDbTransaction = Transaction
        If tr Is Nothing Then
            tr = DBHelper.GetNewTransaction
        End If
        Try
            'First Pass updates Deletions
            PriceListDetails.Update(familyDataset, tr, DataRowState.Deleted)
            ServiceCenter.Update(familyDataset, tr, DataRowState.Deleted)
            VendorQty.Update(familyDataset, tr, DataRowState.Deleted)
            Update(familyDataset.Tables(Me.TABLE_NAME), tr, DataRowState.Deleted)

            'Second Pass updates additions and changes
            UpdateFromSP(familyDataset.Tables(Me.TABLE_NAME), tr, DataRowState.Added Or DataRowState.Modified)
            PriceListDetails.Update(familyDataset, tr, DataRowState.Added Or DataRowState.Modified)
            'PriceListDetails.Update(familyDataset, tr, DataRowState.Modified)
            'PriceListDetails.Update(familyDataset, tr, DataRowState.Added)
            ServiceCenter.Update(familyDataset, tr, DataRowState.Added Or DataRowState.Modified)
            VendorQty.Update(familyDataset, tr, DataRowState.Added Or DataRowState.Modified)

            If Transaction Is Nothing Then
                'We are the creator of the transaction we shoul commit it  and close the connection
                DBHelper.Commit(tr)
            End If
        Catch ex As Exception
            If Transaction Is Nothing Then
                'We are the creator of the transaction we shoul commit it  and close the connection
                DBHelper.RollBack(tr)
            End If
            Throw ex
        End Try
    End Sub
    Protected Overrides Sub ConfigureInsertCommand(ByRef command As OracleCommand, ByVal tableName As String)

        With command
            .AddParameter(PAR_IN_NAME_PRICE_LIST_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_PRICE_LIST_ID)
            .AddParameter(PAR_IN_NAME_CODE, OracleDbType.Varchar2, sourceColumn:=COL_NAME_CODE)
            .AddParameter(PAR_IN_NAME_DESCRIPTION, OracleDbType.Varchar2, sourceColumn:=COL_NAME_DESCRIPTION)
            .AddParameter(PAR_IN_NAME_COUNTRY_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_COUNTRY_ID)
            .AddParameter(PAR_IN_NAME_MANAGE_INVENTORY_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_MANAGE_INVENTORY_ID)
            .AddParameter(PAR_IN_NAME_EFFECTIVE, OracleDbType.Date, sourceColumn:=COL_NAME_EFFECTIVE)
            .AddParameter(PAR_IN_NAME_EXPIRATION, OracleDbType.Date, sourceColumn:=COL_NAME_EXPIRATION)
            .AddParameter(PAR_IN_NAME_CREATED_BY, OracleDbType.Varchar2, sourceColumn:=DALBase.COL_NAME_CREATED_BY)
            .AddParameter(PAR_IN_NAME_DEFAULT_CURRENCY_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_DEFAULT_CURRENCY_ID)


            .AddParameter(PAR_OUT_NAME_RETURN_CODE, OracleDbType.Int32, ParameterDirection.Output)
        End With

    End Sub

    Protected Overrides Sub ConfigureUpdateCommand(ByRef command As OracleCommand, ByVal tableName As String)

        With command
            .AddParameter(PAR_IN_NAME_PRICE_LIST_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_PRICE_LIST_ID)
            .AddParameter(PAR_IN_NAME_DESCRIPTION, OracleDbType.Varchar2, sourceColumn:=COL_NAME_DESCRIPTION)
            .AddParameter(PAR_IN_NAME_COUNTRY_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_COUNTRY_ID)
            .AddParameter(PAR_IN_NAME_EFFECTIVE, OracleDbType.Date, sourceColumn:=COL_NAME_EFFECTIVE)
            .AddParameter(PAR_IN_NAME_EXPIRATION, OracleDbType.Date, sourceColumn:=COL_NAME_EXPIRATION)
            .AddParameter(PAR_IN_NAME_MODIFIED_BY, OracleDbType.Varchar2, sourceColumn:=DALBase.COL_NAME_MODIFIED_BY)
            .AddParameter(PAR_IN_NAME_DEFAULT_CURRENCY_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_DEFAULT_CURRENCY_ID)


            .AddParameter(PAR_OUT_NAME_RETURN_CODE, OracleDbType.Int32, ParameterDirection.Output)
        End With

    End Sub
    'US 224101 - Common call to stored procedures
    Private Function FetchStoredProcedure(methodName As String, storedProc As String, parameters() As OracleParameter) As DataSet
        Dim ds As New DataSet
        Dim tbl As String = Me.TABLE_NAME

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
            Dim par = parameters.FirstOrDefault(Function(p As OracleParameter) p.ParameterName.Equals(Me.PAR_OUT_NAME_RETURN_CODE))
            If (Not par Is Nothing AndAlso par.Value = 200) Then
                Throw New ElitaPlusException("PriceList - " + methodName, Common.ErrorCodes.DB_READ_ERROR)
            End If
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

        Return ds
    End Function

#End Region
    Public Sub ProcessPriceListByStatus(ByVal PriceListID As Guid, ByVal PriceListDetailIDList As String, ByVal userNetworkID As String, ByVal status_xcd As String)
        Dim selectStmt As String = Me.Config("/SQL/PROCESS_PRICE_LIST_BY_STATUS")
        Dim inputParameters() As DBHelper.DBHelperParameter
        Dim outputParameter(0) As DBHelper.DBHelperParameter

        If (Not String.IsNullOrEmpty(PriceListDetailIDList)) Then
            PriceListDetailIDList = PriceListDetailIDList.Replace("'", String.Empty)
        End If

        inputParameters = New DBHelper.DBHelperParameter() _
        {SetParameter(PAR_IN_NAME_PRICE_LIST_ID, PriceListID),
         SetParameter(PAR_IN_NAME_PRICE_LIST_DETAIL_ID_LIST, PriceListDetailIDList),
         SetParameter(PAR_IN_NAME_STATUS_XCD, status_xcd),
         SetParameter(PAR_IN_NAME_STATUS_BY, userNetworkID)}

        outputParameter(0) = New DBHelper.DBHelperParameter(PAR_OUT_NAME_RETURN_CODE, GetType(Integer))

        Try
            DBHelper.ExecuteSpParamBindByName(selectStmt, inputParameters, outputParameter)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub
    Function SetParameter(ByVal name As String, ByVal value As Object) As DBHelper.DBHelperParameter
        name = name.Trim
        If value Is Nothing Then value = DBNull.Value
        If value.GetType Is GetType(String) Then value = DirectCast(value, String).Trim

        Return New DBHelper.DBHelperParameter(name, value, value.GetType)
    End Function
End Class


