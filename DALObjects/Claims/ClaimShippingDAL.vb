'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject v2.cst (9/22/2015)********************


Public Class ClaimShippingDAL
    Inherits OracleDALBase

#Region "Constants"
    Private Const supportChangesFilter As DataRowState = DataRowState.Added  Or DataRowState.Modified 
    Public Const TABLE_NAME As String = "ELP_CLAIM_SHIPPING"
    Public Const TABLE_KEY_NAME As String = COL_NAME_CLAIM_SHIPPING_ID
	
    Public Const COL_NAME_CLAIM_SHIPPING_ID As String = "claim_shipping_id"
    Public Const COL_NAME_CLAIM_ID As String = "claim_id"
    Public Const COL_NAME_SHIPPING_TYPE_ID As String = "shipping_type_id"
    Public Const COL_NAME_SHIPPING_DATE As String = "shipping_date"
    Public Const COL_NAME_TRACKING_NUMBER As String = "tracking_number"
    Public Const COL_NAME_RECEIVED_DATE As String = "received_date"
    
    Public Const PAR_I_NAME_CLAIM_SHIPPING_ID As String = "pi_claim_shipping_id"
    Public Const PAR_I_NAME_CLAIM_ID As String = "pi_claim_id"
    Public Const PAR_I_NAME_SHIPPING_TYPE_ID As String = "pi_shipping_type_id"
    Public Const PAR_I_NAME_SHIPPING_DATE As String = "pi_shipping_date"
    Public Const PAR_I_NAME_TRACKING_NUMBER As String = "pi_tracking_number"
    Public Const PAR_I_NAME_RECEIVED_DATE As String = "pi_received_date"
    
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
        Try
            Using cmd As OracleCommand = OracleDbHelper.CreateCommand(Me.Config("/SQL/LOAD"))
                cmd.AddParameter(PAR_I_NAME_CLAIM_SHIPPING_ID, OracleDbType.Raw, id.ToByteArray())
                cmd.AddParameter(PAR_O_NAME_RESULTCURSOR, OracleDbType.RefCursor, direction:=ParameterDirection.Output)
                OracleDbHelper.Fetch(cmd, Me.TABLE_NAME, familyDS)
            End Using
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadList() As DataSet
        Try
            Using cmd As OracleCommand = OracleDbHelper.CreateCommand(Me.Config("/SQL/LOAD_LIST"))
                cmd.AddParameter(PAR_O_NAME_RESULTCURSOR, OracleDbType.RefCursor, direction:=ParameterDirection.Output)
                Return OracleDbHelper.Fetch(cmd, Me.TABLE_NAME)
            End Using
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function


    Public Sub LoadList(ByVal familyDS As DataSet, ByVal claimId As Guid)

        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(COL_NAME_CLAIM_ID, claimId.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, Me.TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

#End Region

#Region "Overloaded Methods"
    Public Overloads Sub Update(ByVal ds As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing, Optional ByVal changesFilter As DataRowState = supportChangesFilter)
		If ds Is Nothing Then
            Return
        End If
        If (changesFilter Or (supportChangesFilter)) <> (supportChangesFilter) Then
            Throw New NotSupportedException()
        End If
        If Not ds.Tables(Me.TABLE_NAME) Is Nothing Then
            MyBase.Update(ds.Tables(Me.TABLE_NAME), Transaction, changesFilter)
        End If
    End Sub

    Protected Overrides Sub ConfigureDeleteCommand(ByRef command As OracleCommand, tableName As String)
        Throw New NotSupportedException()
    End Sub

    Protected Overrides Sub ConfigureInsertCommand(ByRef command As OracleCommand, tableName As String)
        With command
            .AddParameter(PAR_I_NAME_CLAIM_SHIPPING_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_CLAIM_SHIPPING_ID)
            .AddParameter(PAR_I_NAME_CLAIM_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_CLAIM_ID)
            .AddParameter(PAR_I_NAME_SHIPPING_TYPE_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_SHIPPING_TYPE_ID)
            .AddParameter(PAR_I_NAME_SHIPPING_DATE, OracleDbType.Date, sourceColumn:=COL_NAME_SHIPPING_DATE)
            .AddParameter(PAR_I_NAME_TRACKING_NUMBER, OracleDbType.Varchar2, sourceColumn:=COL_NAME_TRACKING_NUMBER)
            .AddParameter(PAR_I_NAME_RECEIVED_DATE, OracleDbType.Date, sourceColumn:=COL_NAME_RECEIVED_DATE)
            .AddParameter(PAR_I_NAME_CREATED_BY, OracleDbType.Varchar2, sourceColumn:=COL_NAME_CREATED_BY)
            .AddParameter(PAR_I_NAME_CREATED_DATE, OracleDbType.Date, sourceColumn:=COL_NAME_CREATED_DATE)

        End With

    End Sub

    Protected Overrides Sub ConfigureUpdateCommand(ByRef command As OracleCommand, tableName As String)
        With command
            .AddParameter(PAR_I_NAME_CLAIM_SHIPPING_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_CLAIM_SHIPPING_ID)
            .AddParameter(PAR_I_NAME_CLAIM_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_CLAIM_ID)
            .AddParameter(PAR_I_NAME_SHIPPING_TYPE_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_SHIPPING_TYPE_ID)
            .AddParameter(PAR_I_NAME_SHIPPING_DATE, OracleDbType.Date, sourceColumn:=COL_NAME_SHIPPING_DATE)
            .AddParameter(PAR_I_NAME_TRACKING_NUMBER, OracleDbType.Varchar2, sourceColumn:=COL_NAME_TRACKING_NUMBER)
            .AddParameter(PAR_I_NAME_RECEIVED_DATE, OracleDbType.Date, sourceColumn:=COL_NAME_RECEIVED_DATE)
            .AddParameter(PAR_I_NAME_MODIFIED_BY, OracleDbType.Varchar2, sourceColumn:=COL_NAME_MODIFIED_BY)
            .AddParameter(PAR_I_NAME_MODIFIED_DATE, OracleDbType.Date, sourceColumn:=COL_NAME_MODIFIED_DATE)
        End With

    End Sub
#End Region

    Public Function LoadClaimShippingData(claimId As Guid) As DataSet
        Try
            Dim ds As New DataSet
            Using cmd As OracleCommand = OracleDbHelper.CreateCommand(Me.Config("/SQL/GET_CLAIM_SHIPPING_INFO"))
                cmd.CommandType = CommandType.StoredProcedure
                cmd.AddParameter(PAR_I_NAME_CLAIM_ID, OracleDbType.Raw, claimId.ToByteArray())
                cmd.AddParameter(PAR_O_NAME_RESULTCURSOR, OracleDbType.RefCursor, direction:=ParameterDirection.Output)
                OracleDbHelper.Fetch(cmd, Me.TABLE_NAME, ds)
                Return ds
            End Using
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function GetLatestClaimShippingInfo(claimId As Guid, shippingTypeId As Guid) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/GET_LATEST_CLAIM_SHIPPING_INFO")
        Dim ds As New DataSet
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(COL_NAME_CLAIM_ID, claimId.ToByteArray),
                                                                                           New DBHelper.DBHelperParameter(COL_NAME_SHIPPING_TYPE_ID, shippingTypeId.ToByteArray)}
        Try
            DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function


    Public Sub UpdateClaimShippingInfo(ByVal claimShippingId As Guid, ByVal trackingNumber As String)
        Dim selectStmt As String
        Dim parameters() As DBHelper.DBHelperParameter 
        selectStmt = Me.Config("/SQL/UPDATE_CLAIM_SHIPPING_INFO")
        parameters = New DBHelper.DBHelperParameter() { _
                                                          New DBHelper.DBHelperParameter(Me.COL_NAME_TRACKING_NUMBER, trackingNumber), _
                                                          New DBHelper.DBHelperParameter(Me.COL_NAME_CLAIM_SHIPPING_ID, claimShippingId.ToByteArray)}
        Try
            DBHelper.ExecuteWithParam(selectStmt, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub
End Class


