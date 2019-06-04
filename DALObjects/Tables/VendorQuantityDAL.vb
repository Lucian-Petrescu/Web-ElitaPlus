'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (8/27/2012)********************


Public Class VendorQuantityDAL
    Inherits DALBase

    ' Part of req 858

#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_VENDOR_QUANTITY"
    Public Const TABLE_KEY_NAME As String = "vendor_quantity_id"

    Public Const COL_NAME_VENDOR_QUANTITY_ID As String = "vendor_quantity_id"
    Public Const COL_NAME_REFERENCE_ID As String = "reference_id"
    Public Const COL_NAME_TABLE_NAME As String = "table_name"
    Public Const COL_NAME_VENDOR_SKU As String = "vendor_sku"
    Public Const COL_NAME_VENDOR_SKU_DESCRIPTION As String = "vendor_sku_description"
    Public Const COL_NAME_QUANTITY As String = "quantity"

    Public Const COL_NAME_EQUIPMENT_TYPE_ID As String = "equipment_type_id"
    Public Const COL_NAME_MANUFACTURER_ID As String = "manufacturer_id"
    Public Const COL_NAME_MANUFACTURER_NAME As String = "manufacturer_name"
    Public Const COL_NAME_JOB_MODEL As String = "job_model"
    Public Const COL_NAME_PRICE As String = "price"
    Public Const COL_NAME_CONDITION_ID As String = "condition_id"
    Public Const COL_NAME_EFFECTIVE As String = "effective"
    Public Const COL_NAME_EXPIRATION As String = "expiration"
    Public Const COL_NAME_PRICE_LIST_DETAIL_ID As String = "PRICE_LIST_DETAIL_ID"
    Public Const COL_NAME_VENDOR_QUANTITY_AVALIABLE As String = "VENDOR_QUANTITY_AVALIABLE"

    'Public Const COL_NAME_PRICE_LIST_DETAIL_ID As String = "price_list_detail_id"

    'US 224101 
    Public Const PAR_OUT_NAME_RETURN_CODE As String = "po_return_code"

    Public Const PAR_IN_NAME_SERVICE_CENTER_ID As String = "pi_service_center_id"

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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("vendor_quantity_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, Me.TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadList() As DataSet
        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST")
        Return DBHelper.Fetch(selectStmt, Me.TABLE_NAME)
    End Function

    Public Sub LoadList(ByVal familyDS As DataSet, ByVal id As Guid)
        Dim selectStmt As String = Me.Config("/SQL/LOAD_SERVICE_CENTER_QUANTITY_LIST")
        'Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(Me.COL_NAME_REFERENCE_ID, id.ToByteArray)}
        'Try
        '    DBHelper.Fetch(familyDS, selectStmt, Me.TABLE_NAME, parameters)
        'Catch ex As Exception
        '    Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        'End Try

        'US 224101 - Replacing code to call stored proc
        'pi_service_center_id       in  elp_service_center.service_center_id%type,
        'po_vendor_quantity_table   out sys_refcursor,
        'po_return_code             out number

        Dim parameters() As OracleParameter
        parameters = New OracleParameter() {
                              New OracleParameter(PAR_IN_NAME_SERVICE_CENTER_ID, OracleDbType.Raw, id.ToByteArray, ParameterDirection.Input),
                              New OracleParameter("po_vendor_quantity_table", OracleDbType.RefCursor, ParameterDirection.Output),
                              New OracleParameter(PAR_OUT_NAME_RETURN_CODE, OracleDbType.Int16, ParameterDirection.Output)}

        FetchStoredProcedure("LoadList",
                             selectStmt,
                             parameters,
                             familyDS)
    End Sub
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

#Region "Methods"
    Public Sub UpdateVendorQuantityForServiceCenter(ByVal ds As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing, Optional ByVal changesFilter As DataRowState = Nothing)
        If ds Is Nothing Then
            Return
        End If
        If Not ds.Tables(Me.TABLE_NAME) Is Nothing Then
            DBHelper.Execute(ds.Tables(Me.TABLE_NAME), Config("/SQL/INSERT_QUANTITY_FOR_SERVICE_CENTER"), Config("/SQL/UPDATE"), Config("/SQL/DELETE"), Nothing, Transaction, changesFilter)
            LookupListCache.ClearFromCache(Me.GetType.ToString)
        End If
    End Sub

#End Region

    'US 224101 - Common call to stored procedures
    Private Function FetchStoredProcedure(methodName As String, storedProc As String, parameters() As OracleParameter, Optional familyDS As DataSet = Nothing) As DataSet
        Dim tbl As String = Me.TABLE_NAME
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
            Dim par = parameters.FirstOrDefault(Function(p As OracleParameter) p.ParameterName.Equals(Me.PAR_OUT_NAME_RETURN_CODE))
            If (Not par Is Nothing AndAlso par.Value = 200) Then
                Throw New ElitaPlusException("VendorQuantity - " + methodName, Common.ErrorCodes.DB_READ_ERROR)
            End If
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

        Return ds
    End Function
End Class



