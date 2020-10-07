
Public Class ClaimFulfillmentOrderDetailDAL
    Inherits OracleDALBase

#Region "Constants"
    Private Const supportChangesFilter As DataRowState = DataRowState.Added Or DataRowState.Modified
    Public Const TABLE_NAME As String = "ELP_CF_ORDER_DETAIL"
    Public Const TABLE_KEY_NAME As String = COL_NAME_CF_ORDER_DETAIL_ID

    Public Const COL_NAME_CF_ORDER_DETAIL_ID As String = "cf_order_detail_id"
    Public Const COL_NAME_CF_ORDER_CODE As String = "code"
    Public Const COL_NAME_CF_ORDER_DESCRIPTION As String = "description"
    Public Const COL_NAME_CF_ORDER_HEADER_ID As String = "cf_order_header_id"
    Public Const COL_NAME_PRICE_LIST_SOURCE_XCD As String = "price_list_source_xcd"
    Public Const COL_NAME_PRICE_LIST_SOURCE As String = "price_list_source"
    Public Const COL_NAME_COUNTRY_ID As String = "country_id"
    Public Const COL_NAME_COUNTRY As String = "country"
    Public Const COL_NAME_PRICE_LIST_CODE As String = "price_list_code"
    Public Const COL_NAME_EQUIPMENT_TYPE_XCD As String = "equipment_type_xcd"
    Public Const COL_NAME_EQUIPMENT_TYPE As String = "equipment_type"
    Public Const COL_NAME_SERVICE_CLASS_XCD As String = "service_class_xcd"
    Public Const COL_NAME_SERVICE_TYPE_XCD As String = "service_type_xcd"
    Public Const COL_NAME_SERVICE_LEVEL_XCD As String = "service_level_xcd"
    Public Const COL_NAME_STOCK_ITEM_TYPE_XCD As String = "stock_item_type_xcd"

    Public Const PAR_I_NAME_CF_ORDER_DETAIL_ID As String = "pi_cf_order_detail_id"
    Public Const PAR_I_NAME_CF_ORDER_HEADER_ID As String = "pi_cf_order_header_id"
    Public Const PAR_I_NAME_PRICE_LIST_SOURCE_XCD As String = "pi_price_list_source_xcd"
    Public Const PAR_I_NAME_COUNTRY_ID As String = "pi_country_id"
    Public Const PAR_I_NAME_PRICE_LIST_CODE As String = "pi_price_list_code"
    Public Const PAR_I_NAME_EQUIPMENT_TYPE_XCD As String = "pi_equipment_type_xcd"
    Public Const PAR_I_NAME_SERVICE_CLASS_XCD As String = "pi_service_class_xcd"
    Public Const PAR_I_NAME_SERVICE_TYPE_XCD As String = "pi_service_type_xcd"
    Public Const PAR_I_NAME_SERVICE_LEVEL_XCD As String = "pi_service_level_xcd"
    Public Const PAR_I_NAME_STOCK_ITEM_TYPE_XCD As String = "pi_stock_item_type_xcd"
    Public Const PAR_I_NAME_LANGUAGE_ID As String = "pi_langugage_id"
    Public Const PAR_I_NAME_CODE As String = "pi_code"
    Public Const PAR_I_NAME_DESCRIPTION As String = "pi_description"

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
        Try
            Using cmd As OracleCommand = CreateCommand(Config("/SQL/LOAD"))
                cmd.AddParameter(TABLE_KEY_NAME, OracleDbType.Raw, id.ToByteArray())
                cmd.AddParameter(PAR_O_NAME_RESULTCURSOR, OracleDbType.RefCursor, direction:=ParameterDirection.Output)
                Fetch(cmd, TABLE_NAME, familyDS)
            End Using
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadList(Code As String, Description As String, PriceListSource As String, language_id As Guid) As DataSet
        Try
            Using cmd As OracleCommand = CreateCommand(Config("/SQL/LOAD_LIST"))
                cmd.AddParameter(PAR_I_NAME_CODE, OracleDbType.Varchar2, Code, direction:=ParameterDirection.Input)
                cmd.AddParameter(PAR_I_NAME_DESCRIPTION, OracleDbType.Varchar2, Description, direction:=ParameterDirection.Input)
                cmd.AddParameter(PAR_I_NAME_PRICE_LIST_SOURCE_XCD, OracleDbType.Varchar2, PriceListSource, direction:=ParameterDirection.Input)
                cmd.AddParameter(PAR_I_NAME_LANGUAGE_ID, OracleDbType.Raw, language_id.ToByteArray, direction:=ParameterDirection.Input)
                cmd.AddParameter(PAR_O_NAME_RESULTCURSOR, OracleDbType.RefCursor, direction:=ParameterDirection.Output)
                Return Fetch(cmd, TABLE_NAME)
            End Using
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function


#End Region

#Region "Overloaded Methods"
    Public Overloads Sub Update(ds As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing, Optional ByVal changesFilter As DataRowState = supportChangesFilter)
        If ds Is Nothing Then
            Return
        End If
        If (changesFilter Or (supportChangesFilter)) <> (supportChangesFilter) Then
            Throw New NotSupportedException()
        End If
        If Not ds.Tables(TABLE_NAME) Is Nothing Then
            MyBase.UpdateFromSP(ds.Tables(TABLE_NAME), Transaction, changesFilter)
        End If
    End Sub

    Protected Overrides Sub ConfigureDeleteCommand(ByRef command As OracleCommand, tableName As String)
        With command
            .AddParameter(PAR_I_NAME_CF_ORDER_DETAIL_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_CF_ORDER_DETAIL_ID)
            .AddParameter(PAR_I_NAME_CF_ORDER_HEADER_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_CF_ORDER_HEADER_ID)
        End With
    End Sub

    Protected Overrides Sub ConfigureInsertCommand(ByRef command As OracleCommand, tableName As String)
        With command
            .AddParameter(PAR_I_NAME_CF_ORDER_DETAIL_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_CF_ORDER_DETAIL_ID)
            .AddParameter(PAR_I_NAME_CODE, OracleDbType.Varchar2, sourceColumn:=COL_NAME_CF_ORDER_CODE)
            .AddParameter(PAR_I_NAME_DESCRIPTION, OracleDbType.Varchar2, sourceColumn:=COL_NAME_CF_ORDER_DESCRIPTION)
            .AddParameter(PAR_I_NAME_PRICE_LIST_SOURCE_XCD, OracleDbType.Varchar2, sourceColumn:=COL_NAME_PRICE_LIST_SOURCE_XCD)
            .AddParameter(PAR_I_NAME_COUNTRY_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_COUNTRY_ID)
            .AddParameter(PAR_I_NAME_PRICE_LIST_CODE, OracleDbType.Varchar2, sourceColumn:=COL_NAME_PRICE_LIST_CODE)
            .AddParameter(PAR_I_NAME_EQUIPMENT_TYPE_XCD, OracleDbType.Varchar2, sourceColumn:=COL_NAME_EQUIPMENT_TYPE_XCD)
            .AddParameter(PAR_I_NAME_SERVICE_CLASS_XCD, OracleDbType.Varchar2, sourceColumn:=COL_NAME_SERVICE_CLASS_XCD)
            .AddParameter(PAR_I_NAME_SERVICE_TYPE_XCD, OracleDbType.Varchar2, sourceColumn:=COL_NAME_SERVICE_TYPE_XCD)
            .AddParameter(PAR_I_NAME_SERVICE_LEVEL_XCD, OracleDbType.Varchar2, sourceColumn:=COL_NAME_SERVICE_LEVEL_XCD)
            .AddParameter(PAR_I_NAME_STOCK_ITEM_TYPE_XCD, OracleDbType.Varchar2, sourceColumn:=COL_NAME_STOCK_ITEM_TYPE_XCD)
            .AddParameter(PAR_I_NAME_CREATED_BY, OracleDbType.Varchar2, sourceColumn:=COL_NAME_CREATED_BY)
            .AddParameter(PAR_I_NAME_CREATED_DATE, OracleDbType.Date, sourceColumn:=COL_NAME_CREATED_DATE)
        End With

    End Sub

    Protected Overrides Sub ConfigureUpdateCommand(ByRef command As OracleCommand, tableName As String)
        With command
            .AddParameter(PAR_I_NAME_CF_ORDER_DETAIL_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_CF_ORDER_DETAIL_ID)
            .AddParameter(PAR_I_NAME_DESCRIPTION, OracleDbType.Varchar2, sourceColumn:=COL_NAME_DESCRIPTION)
            .AddParameter(PAR_I_NAME_EQUIPMENT_TYPE_XCD, OracleDbType.Varchar2, sourceColumn:=COL_NAME_EQUIPMENT_TYPE_XCD)
            .AddParameter(PAR_I_NAME_SERVICE_CLASS_XCD, OracleDbType.Varchar2, sourceColumn:=COL_NAME_SERVICE_CLASS_XCD)
            .AddParameter(PAR_I_NAME_SERVICE_TYPE_XCD, OracleDbType.Varchar2, sourceColumn:=COL_NAME_SERVICE_TYPE_XCD)
            .AddParameter(PAR_I_NAME_SERVICE_LEVEL_XCD, OracleDbType.Varchar2, sourceColumn:=COL_NAME_SERVICE_LEVEL_XCD)
            .AddParameter(PAR_I_NAME_STOCK_ITEM_TYPE_XCD, OracleDbType.Varchar2, sourceColumn:=COL_NAME_STOCK_ITEM_TYPE_XCD)
            .AddParameter(PAR_I_NAME_PRICE_LIST_SOURCE_XCD, OracleDbType.Varchar2, sourceColumn:=COL_NAME_PRICE_LIST_SOURCE_XCD)
            .AddParameter(PAR_I_NAME_COUNTRY_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_COUNTRY_ID)
            .AddParameter(PAR_I_NAME_PRICE_LIST_CODE, OracleDbType.Varchar2, sourceColumn:=COL_NAME_PRICE_LIST_CODE)
            .AddParameter(PAR_I_NAME_MODIFIED_BY, OracleDbType.Varchar2, sourceColumn:=COL_NAME_MODIFIED_BY)
            .AddParameter(PAR_I_NAME_MODIFIED_DATE, OracleDbType.Date, sourceColumn:=COL_NAME_MODIFIED_DATE)
        End With

    End Sub

    Public Function CFCodeExists(CfCode As String) As Boolean
        Dim selectStmt As String = Config("/SQL/CODE_UNIQUE")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {
                                    New DBHelper.DBHelperParameter("pi_code", CfCode)}
        Dim ds As New DataSet
        Try
            Dim bExists As Boolean = True
            DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
            If CType(ds.Tables(0).Rows(0).Item(0), Integer) = 0 Then
                bExists = False
            End If
            Return bExists
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function
#End Region

End Class


