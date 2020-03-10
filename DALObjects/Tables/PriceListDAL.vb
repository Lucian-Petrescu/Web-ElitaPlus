'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (7/31/2012)********************


Public Class PriceListDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_PRICE_LIST_HEADER_RECON"
    Public Const TABLE_KEY_NAME As String = "price_list_id"
    Public Const COL_NAME_PRICE_LIST_ID As String = "price_list_id"
    Public Const COL_NAME_COUNTRY_ID As String = "country_id"
    Public Const COL_NAME_DEFAULT_CURRENCY_ID As String = "default_currency_id"

    Public Const COL_NAME_CODE As String = "code"
    Public Const COL_NAME_DESCRIPTION As String = "description"
    Public Const COL_NAME_MANAGE_INVENTORY_ID As String = "manage_inventory_id"
    Public Const COL_NAME_EFFECTIVE As String = "effective"
    Public Const COL_NAME_STATUS As String = "status"
    Public Const COL_NAME_EXPIRATION As String = "expiration"

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
    Public Const PAR_IN_NAME_DEFAULT_CURRENCY_ID As String = "pi_default_currency_id"
    Public Const NULL_VALUE As String = "NULL"

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
        'Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("price_list_id", id.ToByteArray)}
        Try
            'Dim oraParms(parameters.Length - 1) As OracleParameter

            'Dim i As Integer

            'For i = 0 To parameters.Length - 1
            '    oraParms(i) = New OracleParameter(parameters(i).Name, parameters(i).Value)
            'Next
            Dim params As OracleParameter()
            params = New OracleParameter() {
                              New OracleParameter("pi_price_list_id", OracleDbType.Raw, id.ToByteArray, ParameterDirection.Input),
                              New OracleParameter("po_price_list", OracleDbType.RefCursor, ParameterDirection.Output),
                              New OracleParameter(PAR_OUT_NAME_RETURN_CODE, OracleDbType.Int16, ParameterDirection.Output)}

            DBHelper.InternalFetchbySP(familyDS, selectStmt, Me.TABLE_NAME, params)


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

        'Dim whereClauseConditions As String = String.Empty

        'If Me.FormatSearchMask(code) Then
        '    whereClauseConditions &= Environment.NewLine & " UPPER(pl." & Me.COL_NAME_CODE & ") " & code.ToUpper
        'End If

        'If Me.FormatSearchMask(description) Then
        '    If Not whereClauseConditions.Equals(String.Empty) Then
        '        whereClauseConditions &= Environment.NewLine & " AND "
        '    End If
        '    whereClauseConditions &= Environment.NewLine & " UPPER(pl." & Me.COL_NAME_DESCRIPTION & ") " & description.ToUpper
        'End If

        'If serviceType <> Guid.Empty Then
        '    If Not whereClauseConditions.Equals(String.Empty) Then
        '        whereClauseConditions &= Environment.NewLine & " AND "
        '    End If
        '    whereClauseConditions &= Environment.NewLine & "" & Me.COL_NAME_SERVICE_TYPE_ID & " = " & MiscUtil.GetDbStringFromGuid(serviceType, True)
        'End If

        ''If country <> Guid.Empty Then
        ''    If Not whereClauseConditions.Equals(String.Empty) Then
        ''        whereClauseConditions &= Environment.NewLine & " AND "
        ''    End If
        ''    whereClauseConditions &= Environment.NewLine & " pl." & Me.COL_NAME_COUNTRY_ID & " = " & MiscUtil.GetDbStringFromGuid(country, True)
        ''End If
        'If countryList <> "" Then
        '    If Not whereClauseConditions.Equals(String.Empty) Then
        '        whereClauseConditions &= Environment.NewLine & " AND "
        '    End If
        '    whereClauseConditions &= Environment.NewLine & " pl." & Me.COL_NAME_COUNTRY_ID & " IN (" & countryList & ")"
        'End If

        'If Me.FormatSearchMask(serviceCenter) Then
        '    If Not whereClauseConditions.Equals(String.Empty) Then
        '        whereClauseConditions &= Environment.NewLine & " AND "
        '    End If
        '    whereClauseConditions &= Environment.NewLine & " UPPER(" & Me.COL_NAME_SERVICE_CENTER_DESC & ") " & serviceCenter.ToUpper
        'End If

        'If Not activeOn = Nothing Then
        '    If Not whereClauseConditions.Equals(String.Empty) Then
        '        whereClauseConditions &= Environment.NewLine & " AND "
        '    End If
        '    whereClauseConditions &= " " & Environment.NewLine & " trunc(to_date('" & Date.Parse(activeOn).ToString("MM/dd/yyyy HH:mm:ss") _
        '                  & "', 'mm-dd-yyyy hh24:mi:ss')) BETWEEN trunc(pl." & Me.COL_NAME_EFFECTIVE & ")" & " AND trunc(pl." & Me.COL_NAME_EXPIRATION & ")" & ""
        'End If

        'If Not whereClauseConditions.Equals(String.Empty) Then
        '    whereClauseConditions = " WHERE " & whereClauseConditions
        '    selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
        'Else
        '    selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, "")
        'End If


        'Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() { _
        '         New DBHelper.DBHelperParameter("languageId", languageId.ToByteArray)}


        'Dim ds As New DataSet
        'Try
        '    Return DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)
        'Catch ex As Exception
        '    Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        'End Try

        'US 224101 - Replacing code to call stored proc
        'pi_language_id              in  elp_language.language_id%type,
        'pi_code                     In  elp_price_list.code%type Default null,
        'pi_description              in  elp_price_list.description%type default null,
        'pi_service_type_id          In  raw Default null,
        'pi_country_id_list          in  varchar2 default null,
        'pi_sc_description           In  elp_service_center.description%type Default null,
        'pi_activeon                 in  varchar2 default null,
        'po_price_table              out sys_refcursor,
        'po_return_code              out number
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



    Public Function InsertPriceListRecon(ByVal pricelistid As Guid,
                             ByVal namecode As String,
                             ByVal namedescription As String,
                             ByVal countryid As String,
                             ByVal createdby As String,
                             ByVal manageinventoryid As String,
                             ByVal effective As String,
                             ByVal expiration As String,
                             ByVal defaultcurrencyid As String) As DataSet

        Dim selectStmt As String = Me.Config("/SQL/insert_price_list_recon")

        Dim parameters() As OracleParameter

        'Deleting NULL string wherever it's found
        'If (Not String.IsNullOrEmpty(countryList)) Then
        '    countryList = countryList.Replace("'", String.Empty)

        '    Dim nullIndex As Integer = countryList.IndexOf(NULL_VALUE, StringComparison.InvariantCultureIgnoreCase)
        '    Dim length = NULL_VALUE.Length

        '    If (nullIndex <> -1) Then
        '        If (countryList.Length > length) Then
        '            length = length + 1
        '        End If

        '        If (nullIndex > 0) Then
        '            nullIndex = nullIndex - 1
        '        End If

        '        countryList = countryList.Replace(countryList.Substring(nullIndex, length), String.Empty).Replace("'", String.Empty)
        '    End If
        'End If


        parameters = New OracleParameter() {
                              New OracleParameter(PAR_IN_NAME_PRICE_LIST_ID, OracleDbType.Raw, pricelistid.ToByteArray, ParameterDirection.Input),
                              New OracleParameter(PAR_IN_NAME_CODE, OracleDbType.Varchar2, If(Not String.IsNullOrEmpty(namecode), namecode.ToUpper(), Nothing), ParameterDirection.Input),
                              New OracleParameter(PAR_IN_NAME_DESCRIPTION, OracleDbType.Varchar2, If(Not String.IsNullOrEmpty(namedescription), namedescription.ToUpper, Nothing), ParameterDirection.Input),
                              New OracleParameter(PAR_IN_NAME_COUNTRY_ID, OracleDbType.Varchar2, If(String.IsNullOrEmpty(countryid), Nothing, countryid), ParameterDirection.Input),
                              New OracleParameter(PAR_IN_NAME_MANAGE_INVENTORY_ID, OracleDbType.Varchar2, If(String.IsNullOrEmpty(manageinventoryid), manageinventoryid, Nothing), ParameterDirection.Input),
                              New OracleParameter(PAR_IN_NAME_EFFECTIVE, OracleDbType.Varchar2, If(effective Is Nothing, Nothing, DateTime.Parse(effective).ToString("MM/dd/yyyy HH:mm:ss")), ParameterDirection.Input),
                              New OracleParameter(PAR_IN_NAME_EXPIRATION, OracleDbType.Varchar2, If(expiration Is Nothing, Nothing, DateTime.Parse(expiration).ToString("MM/dd/yyyy HH:mm:ss")), ParameterDirection.Input),
                              New OracleParameter(PAR_IN_NAME_CREATED_BY, OracleDbType.Varchar2, OracleDbType.Varchar2, If(String.IsNullOrEmpty(createdby), createdby, Nothing), ParameterDirection.Input),
                              New OracleParameter(PAR_IN_NAME_DEFAULT_CURRENCY_ID, OracleDbType.Varchar2, If(String.IsNullOrEmpty(createdby), createdby, Nothing), ParameterDirection.Input),
                              New OracleParameter("po_return_code", OracleDbType.Long, ParameterDirection.Output)
                              }

        Return FetchStoredProcedure("InsertPriceList",
                                         selectStmt,
                                         parameters)

    End Function

    Public Function UpdatePriceListRecon(ByVal pricelistid As Guid,
                             ByVal namecode As String,
                             ByVal namedescription As String,
                             ByVal countryid As String,
                             ByVal createdby As String,
                             ByVal manageinventoryid As String,
                             ByVal effective As String,
                             ByVal expiration As String,
                             ByVal defaultcurrencyid As String) As DataSet

        Dim selectStmt As String = Me.Config("/SQL/update_price_list_recon")

        Dim parameters() As OracleParameter

        'Deleting NULL string wherever it's found
        'If (Not String.IsNullOrEmpty(countryList)) Then
        '    countryList = countryList.Replace("'", String.Empty)

        '    Dim nullIndex As Integer = countryList.IndexOf(NULL_VALUE, StringComparison.InvariantCultureIgnoreCase)
        '    Dim length = NULL_VALUE.Length

        '    If (nullIndex <> -1) Then
        '        If (countryList.Length > length) Then
        '            length = length + 1
        '        End If

        '        If (nullIndex > 0) Then
        '            nullIndex = nullIndex - 1
        '        End If

        '        countryList = countryList.Replace(countryList.Substring(nullIndex, length), String.Empty).Replace("'", String.Empty)
        '    End If
        'End If


        parameters = New OracleParameter() {
                              New OracleParameter(PAR_IN_NAME_PRICE_LIST_ID, OracleDbType.Raw, pricelistid.ToByteArray, ParameterDirection.Input),
                              New OracleParameter(PAR_IN_NAME_CODE, OracleDbType.Varchar2, If(Not String.IsNullOrEmpty(namecode), namecode.ToUpper(), Nothing), ParameterDirection.Input),
                              New OracleParameter(PAR_IN_NAME_DESCRIPTION, OracleDbType.Varchar2, If(Not String.IsNullOrEmpty(namedescription), namedescription.ToUpper, Nothing), ParameterDirection.Input),
                              New OracleParameter(PAR_IN_NAME_COUNTRY_ID, OracleDbType.Varchar2, If(String.IsNullOrEmpty(countryid), Nothing, countryid), ParameterDirection.Input),
                              New OracleParameter(PAR_IN_NAME_MANAGE_INVENTORY_ID, OracleDbType.Varchar2, If(String.IsNullOrEmpty(manageinventoryid), manageinventoryid, Nothing), ParameterDirection.Input),
                              New OracleParameter(PAR_IN_NAME_EFFECTIVE, OracleDbType.Varchar2, If(effective Is Nothing, Nothing, DateTime.Parse(effective).ToString("MM/dd/yyyy HH:mm:ss")), ParameterDirection.Input),
                              New OracleParameter(PAR_IN_NAME_EXPIRATION, OracleDbType.Varchar2, If(expiration Is Nothing, Nothing, DateTime.Parse(expiration).ToString("MM/dd/yyyy HH:mm:ss")), ParameterDirection.Input),
                              New OracleParameter(PAR_IN_NAME_CREATED_BY, OracleDbType.Varchar2, OracleDbType.Varchar2, If(String.IsNullOrEmpty(createdby), createdby, Nothing), ParameterDirection.Input),
                              New OracleParameter(PAR_IN_NAME_DEFAULT_CURRENCY_ID, OracleDbType.Varchar2, If(String.IsNullOrEmpty(createdby), createdby, Nothing), ParameterDirection.Input),
                              New OracleParameter("po_return_code", OracleDbType.Long, ParameterDirection.Output)
                              }

        Return FetchStoredProcedure("UpdatePriceList",
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

    Public Overloads Sub SubmitforApproval(ByVal familyDataset As DataSet, ByVal flag As String)
        Dim PriceListDetails As New PriceListDetailDAL
        PriceListDetails.SubmitForApproval(familyDataset, flag)
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
            Update(familyDataset.Tables(Me.TABLE_NAME), tr, DataRowState.Added Or DataRowState.Modified)
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
End Class


