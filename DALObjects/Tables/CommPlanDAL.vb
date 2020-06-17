'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (11/18/2004)********************

#Region "CommissionPeriodData"

Public Class CommPlanData

    Public dealerId As Guid
    Public companyIds As ArrayList

End Class
#End Region

Public Class CommPlanDAL
    Inherits DALBase

#Region "Constants"
    Public Const TABLE_NAME As String = "elp_commission_plan"
    Public Const TABLE_KEY_NAME As String = "commission_plan_id"
    '  Public Const DSNAME As String = "LIST"

    Public Const COL_NAME_COMM_PLAN_ID As String = "commission_plan_id"
    
    Public Const COL_NAME_DEALER_ID As String = "reference_id"
    Public Const COL_NAME_RFERENCE_SOURCE As String = "reference"
    Public Const COL_NAME_COMPANY_CODE As String = "company_code"
    Public Const COL_NAME_CODE As String = "code"
    Public Const COL_NAME_DESCRIPTION As String = "description"
    Public Const COL_NAME_EFFECTIVE_DATE As String = "effective_date"
    Public Const COL_NAME_EXPIRATION_DATE As String = "expiration_date"
    Public Const COL_NAME_COMPUTE_METHOD_ID As String = "compute_method_id"
    Public Const COL_NAME_DEALER_NAME As String = "dealer_name"
    

    Public Const COL_NAME_COMPANY_ID = "company_id"
    Public Const COL_NAME_DEALER_ID0 As String = "dealer_id0"
    Public Const COL_NAME_DEALER_ID1 As String = "dealer_id1"

    Public Const DEALER_ID = 0

    Public Const PRODUCT_ID = 1

    Public Const DEALER_ID0 = 0
    Public Const DEALER_ID1 = 1
    Public Const USER_ID = 2

    Public Const TOTAL_PARAM = 0
    Public Const TOTAL_PARAM_A = 1
    Public Const TOTAL_PARAM_B = 2

    ' Expiration
    Public Const COL_NAME_MAX_EXPIRATION As String = "expiration"
    Public Const COL_NAME_EXPIRATION_COUNT As String = "expiration_count"

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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("commission_plan_id", id.ToByteArray)}
        Dim outParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("po_resultcursor", GetType(DataSet))}
        Try
            DBHelper.FetchSp(selectStmt, parameters, outParameters, familyDS, Me.TABLE_NAME)

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadList(ByVal oCommissionPeriodData As CommPlanData, ByVal activeUserId As Guid) As DataSet
        Try
            Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST")
            Dim inparameters() As DBHelper.DBHelperParameter

            With oCommissionPeriodData

                If .dealerId.Equals(Guid.Empty) Then
                    inparameters = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(COL_NAME_DEALER_ID, GenericConstants.WILDCARD), 
                                                                     New DBHelper.DBHelperParameter("user_id", activeUserId.ToByteArray)}
                Else
                    inparameters = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(COL_NAME_DEALER_ID, .dealerId.ToByteArray),
                                                                     New DBHelper.DBHelperParameter("user_id", activeUserId.ToByteArray)}
                End If
                
            End With

            Dim outParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("po_resultcursor", GetType(DataSet))}

            Dim ds As New DataSet
            Dim tbl As String = Me.TABLE_NAME

            DBHelper.FetchSp(selectStmt, inparameters, outParameters, ds, tbl)
            Return ds

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function LoadExpiration(ByVal oCommissionPeriodData As CommPlanData) As DataSet
        Try
            Dim selectStmt As String = Me.Config("/SQL/MAX_EXPIRATION")
            Dim inparameters() As DBHelper.DBHelperParameter

            With oCommissionPeriodData
                inparameters = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(COL_NAME_DEALER_ID0, .dealerId.ToByteArray),
                                                                 New DBHelper.DBHelperParameter(COL_NAME_DEALER_ID1, .dealerId.ToByteArray)}
            End With

            Dim outParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("po_resultcursor", GetType(DataSet))}

            Dim ds As New DataSet
            Dim tbl As String = Me.TABLE_NAME

            DBHelper.FetchSp(selectStmt, inparameters, outParameters, ds, tbl)
            Return ds

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

#End Region

#Region "Overloaded Methods"
    'This method was added manually to accommodate BO families Save
    Public Overloads Sub UpdateFamily(ByVal familyDataset As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing)

        'Dim addressDAL As New addressDAL
        Dim commPeriodEntityDAL As New CommissionPeriodEntityDAL
        Dim assocCommDAL As New AssociateCommissionsDAL
        Dim commToleranceDAL As New CommissionToleranceDAL

        Dim tr As IDbTransaction = Transaction
        If tr Is Nothing Then
            tr = DBHelper.GetNewTransaction
        End If
        Try
            'First Pass updates Deletions
            'commPeriodEntityDAL.Update(familyDataset.GetChanges(DataRowState.Deleted), tr, DataRowState.Deleted)
            'assocCommDAL.Update(familyDataset.GetChanges(DataRowState.Deleted), tr, DataRowState.Deleted)
            'commToleranceDAL.Update(familyDataset.GetChanges(DataRowState.Deleted), tr, DataRowState.Deleted)
            'MyBase.Update(familyDataset.Tables(Me.TABLE_NAME).GetChanges(DataRowState.Deleted), tr, DataRowState.Deleted)
            UpdateFromSP(familyDataset.Tables(Me.TABLE_NAME), tr, DataRowState.Deleted)
            'Second Pass updates additions and changes
            'addressDAL.Update(familyDataset.GetChanges(DataRowState.Added Or DataRowState.Modified), tr, DataRowState.Added Or DataRowState.Modified)
            'Update(familyDataset.Tables(Me.TABLE_NAME).GetChanges(DataRowState.Added Or DataRowState.Modified), tr, DataRowState.Added Or DataRowState.Modified)
            UpdateFromSP(familyDataset.Tables(Me.TABLE_NAME), tr, DataRowState.Added Or DataRowState.Modified)
            'commPeriodEntityDAL.Update(familyDataset.GetChanges(DataRowState.Added Or DataRowState.Modified), tr, DataRowState.Added Or DataRowState.Modified)
            'commToleranceDAL.Update(familyDataset.GetChanges(DataRowState.Added Or DataRowState.Modified), tr, DataRowState.Added Or DataRowState.Modified)
            'assocCommDAL.Update(familyDataset.GetChanges(DataRowState.Added Or DataRowState.Modified), tr, DataRowState.Added Or DataRowState.Modified)

            'At the end delete the Address
            'addressDAL.Update(familyDataset.GetChanges(DataRowState.Deleted), tr, DataRowState.Deleted)

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



        'Public Overloads Sub Update(ByVal ds As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing, Optional ByVal changesFilter As DataRowState = Nothing)
        '    If Not ds.Tables(Me.TABLE_NAME) Is Nothing Then
        '        MyBase.Update(ds.Tables(Me.TABLE_NAME), Transaction, changesFilter)
        '    End If
        'End Sub
    End Sub
#End Region

    Public Overloads Sub Update(ByVal ds As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing, Optional ByVal changesFilter As DataRowState = Nothing)
        If ds Is Nothing Then
            Return
        End If
        If Not ds.Tables(Me.TABLE_NAME) Is Nothing Then
            'MyBase.Update(ds.Tables(Me.TABLE_NAME), Transaction, changesFilter)
            MyBase.UpdateFromSP(ds.Tables(Me.TABLE_NAME), Transaction, changesFilter)
            'MyBase.UpdateWithParam(ds.Tables(Me.TABLE_NAME), Transaction, changesFilter)
        End If
    End Sub

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
                Throw New ElitaPlusException("ELP_COMMISSION_PLAN - " + methodName, Common.ErrorCodes.DB_READ_ERROR)
            End If
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

        Return ds
    End Function

    Protected Overrides Sub ConfigureDeleteCommand(ByRef command As OracleCommand, ByVal tableName As String)
        command.AddParameter("pi_commission_plan_id", OracleDbType.Raw, sourceColumn:=TABLE_KEY_NAME)
        command.AddParameter("po_exec_status", OracleDbType.Int32, ParameterDirection.Output)
    End Sub

    Protected Overrides Sub ConfigureInsertCommand(ByRef command As OracleCommand, ByVal tableName As String)
        With command
            .AddParameter("pi_dealer_id", OracleDbType.Raw, sourceColumn:=COL_NAME_DEALER_ID)
            .AddParameter("pi_code", OracleDbType.Varchar2, sourceColumn:=COL_NAME_CODE)
            .AddParameter("pi_description", OracleDbType.Varchar2, sourceColumn:=COL_NAME_DESCRIPTION)            
            .AddParameter("pi_effective_date", OracleDbType.Date, sourceColumn:=COL_NAME_EFFECTIVE_DATE)
            .AddParameter("pi_expiration_date", OracleDbType.Date, sourceColumn:=COL_NAME_EXPIRATION_DATE)
            .AddParameter("pi_created_by", OracleDbType.Varchar2, sourceColumn:=COL_NAME_CREATED_BY)
            .AddParameter("pi_commission_plan_id", OracleDbType.Raw, sourceColumn:=TABLE_KEY_NAME)
            .AddParameter("pi_reference", OracleDbType.Varchar2, sourceColumn:=COL_NAME_RFERENCE_SOURCE)            
            .AddParameter("po_exec_status", OracleDbType.Int32, ParameterDirection.Output)
        End With
    End Sub

    Protected Overrides Sub ConfigureUpdateCommand(ByRef command As OracleCommand, ByVal tableName As String)
        With command
            .AddParameter("pi_dealer_id", OracleDbType.Raw, sourceColumn:=COL_NAME_DEALER_ID)
            .AddParameter("pi_code", OracleDbType.Varchar2, sourceColumn:=COL_NAME_CODE)
            .AddParameter("pi_description", OracleDbType.Varchar2, sourceColumn:=COL_NAME_DESCRIPTION)            
            .AddParameter("pi_effective_date", OracleDbType.Date, sourceColumn:=COL_NAME_EFFECTIVE_DATE)
            .AddParameter("pi_expiration_date", OracleDbType.Date, sourceColumn:=COL_NAME_EXPIRATION_DATE)
            .AddParameter("pi_modified_by", OracleDbType.Varchar2, sourceColumn:=COL_NAME_MODIFIED_BY)
            .AddParameter("pi_commission_plan_id", OracleDbType.Raw, sourceColumn:=TABLE_KEY_NAME)
            .AddParameter("pi_reference", OracleDbType.Varchar2, sourceColumn:=COL_NAME_RFERENCE_SOURCE)            
            .AddParameter("po_exec_status", OracleDbType.Int32, ParameterDirection.Output)
        End With
    End Sub
End Class