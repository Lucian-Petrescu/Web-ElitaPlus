Public Class CommPlanDistributionDAL
    Inherits DALBase

#Region "Constants"
    Public Const TABLE_NAME As String = "elp_comm_plan_distribution"
    Public Const TABLE_KEY_NAME As String = "COMM_PLAN_DISTRIBUTION_ID"
    Public Const COL_NAME_COMM_PLAN_DIST_ID As String = "COMM_PLAN_DISTRIBUTION_ID"
    Public Const COL_NAME_COMM_PLAN_ID As String = "COMMISSION_PLAN_ID"
    Public Const COL_NAME_COMM_AMOUNT As String = "COMMISSION_AMOUNT"
    Public Const COL_NAME_COMMISSION_PERCENT As String = "COMMISSION_PERCENTAGE"
    Public Const COL_NAME_POSITION As String = "POSITION"
    Public Const COL_NAME_ENTITY_NAME As String = "ENTITY_NAME"
    Public Const COL_NAME_ENTITY_ID As String = "ENTITY_ID"
    Public Const COL_NAME_PAYEE_TYPE_XCD As String = "PAYEE_TYPE_XCD"
    
    Public Const COL_NAME_CREATED_BY As String = "CREATED_BY"
    Public Const COL_NAME_CREATED_DATE As String = "CREATED_DATE"
    Public Const COL_NAME_COMM_MODIFIED_BY As String = "MODIFIED_BY"
    Public Const COL_NAME_COMM_MODIFIED_DATE As String = "MODIFIED_DATE"
    Public Const COL_NAME_COMMISSION_SOURCE_XCD As String = "COMMISSIONS_SOURCE_XCD"
    Public Const COL_NAME_ACT_ENT_SOURCE_XCD As String = "ACCT_FIELD_TYPE_XCD"
    
    Private Const DSNAME As String = "LIST"

#End Region

#Region "Constructors"
    Public Sub New()
        MyBase.new()
    End Sub

#End Region

#Region "Load Methods"

    Public Sub LoadSchema(ds As DataSet)
        Load(ds, Guid.Empty)
    End Sub

    Public Sub Load(familyDS As DataSet, id As Guid)
        Dim selectStmt As String = Config("/SQL/LOAD")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("comm_plan_ext_distribution_id", id.ToByteArray)}
        Dim outParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("po_resultcursor", GetType(DataSet))}
        Try
            DBHelper.FetchSp(selectStmt, parameters, outParameters, familyDS, TABLE_NAME)

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadAssociateByCommissions(id As Guid) As DataSet
        Try
            Dim selectStmt As String = Config("/SQL/LOAD")

            Dim inparameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("pi_comm_plan_dist_id", id.ToByteArray)}
            Dim outParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("po_resultcursor", GetType(DataSet))}

            Dim ds As New DataSet
            Dim tbl As String = TABLE_NAME

            DBHelper.FetchSp(selectStmt, inparameters, outParameters, ds, tbl)
            Return ds

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function LoadList(commPlanId As Guid) As DataSet
        Try
            Dim selectStmt As String = Config("/SQL/LOAD_LIST_COMM_PLAN_DIST_BY_PLAN")

            Dim inparameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("pi_comm_plan_id", commPlanId.ToByteArray)}
            Dim outParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("po_resultcursor", GetType(DataSet))}

            Dim ds As New DataSet
            Dim tbl As String = TABLE_NAME

            DBHelper.FetchSp(selectStmt, inparameters, outParameters, ds, tbl)
            Return ds

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function LoadListPlan(commPlanId As Guid) As DataSet
        Try
            Dim selectStmt As String = Config("/SQL/LOAD_LIST_COMM_PLAN_DIST_BY_PLAN_ID")

            Dim inparameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("pi_comm_plan_id", commPlanId.ToByteArray)}
            Dim outParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("po_resultcursor", GetType(DataSet))}

            Dim ds As New DataSet
            Dim tbl As String = TABLE_NAME

            DBHelper.FetchSp(selectStmt, inparameters, outParameters, ds, tbl)
            Return ds

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Sub LoadList(commPlanId As Guid, familyDS As DataSet)
        Dim selectStmt As String = Config("/SQL/LOAD_LIST_COMM_PLAN_DIST_BY_PLAN_ID")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("pi_comm_plan_id", commPlanId.ToByteArray)}
        Dim outParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("po_resultcursor", GetType(DataSet))}
        Try
            DBHelper.FetchSp(selectStmt, parameters, outParameters, familyDS, TABLE_NAME)

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function CommPaymentExist(pi_commmission_plan_id As Guid) As String
        Try
            Dim selectStmt As String = Config("/SQL/COMM_PAYMENT_EXIST")

            Dim inparameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("pi_Commmission_Plan_id", pi_commmission_plan_id.ToByteArray)}
            Dim outParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("po_status", GetType(String))}

            DBHelper.ExecuteSp(selectStmt, inparameters, outParameters)
            Return outParameters(0).Value.ToString()

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    'US - 565821 - UI - Commission fields check null and position validation
    Public Function CheckPositionExist(pi_position As Integer, pi_comm_plan_dist_id As Guid, pi_commmission_plan_id As Guid) As String
        Try
            Dim selectStmt As String = Config("/SQL/CHECK_FOR_POSITION_EXIST")

            Dim inparameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("pi_position", pi_position),
                                                                                                 New DBHelper.DBHelperParameter("pi_comm_plan_dist_id", pi_comm_plan_dist_id.ToByteArray),
                                                                                                 New DBHelper.DBHelperParameter("pi_Commmission_Plan_id", pi_commmission_plan_id.ToByteArray)}

            Dim outParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("po_status", GetType(String))}

            DBHelper.ExecuteSp(selectStmt, inparameters, outParameters)
            Return outParameters(0).Value.ToString()

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function    

#End Region

#Region "Overloaded Methods"
    #Region "Overloaded Methods"
    'This method was added manually to accommodate BO families Save
    Public Overloads Sub UpdateFamily(familyDataset As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing)

        Dim commPlanDistDAL As New CommPlanDistributionDAL  
        
        Dim tr As IDbTransaction = Transaction
        If tr Is Nothing Then
            tr = DBHelper.GetNewTransaction
        End If
        Try
            'First Pass updates Deletions
            commPlanDistDAL.Update(familyDataset.GetChanges(DataRowState.Deleted), tr, DataRowState.Deleted)
            'MyBase.Update(familyDataset.Tables(Me.TABLE_NAME).GetChanges(DataRowState.Deleted), tr, DataRowState.Deleted)
            'UpdateFromSP(familyDataset.Tables(Me.TABLE_NAME), tr, DataRowState.Deleted)
            'Second Pass updates additions and changes
            'Update(familyDataset.Tables(Me.TABLE_NAME).GetChanges(DataRowState.Added Or DataRowState.Modified), tr, DataRowState.Added Or DataRowState.Modified)
            'UpdateFromSP(familyDataset.Tables(Me.TABLE_NAME), tr, DataRowState.Added Or DataRowState.Modified)
            commPlanDistDAL.Update(familyDataset.GetChanges(DataRowState.Added Or DataRowState.Modified), tr, DataRowState.Added Or DataRowState.Modified)

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

    Public Overloads Sub Update(ds As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing, Optional ByVal changesFilter As DataRowState = Nothing)
        If ds Is Nothing Then
            Return
        End If
        If Not ds.Tables(TABLE_NAME) Is Nothing Then
            'MyBase.Update(ds.Tables(Me.TABLE_NAME), Transaction, changesFilter)
            MyBase.UpdateFromSP(ds.Tables(TABLE_NAME), Transaction, changesFilter)
            'MyBase.UpdateWithParam(ds.Tables(Me.TABLE_NAME), Transaction, changesFilter)
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
                Using cmd As OracleCommand = CreateCommand(storedProc, CommandType.StoredProcedure, connection)
                    cmd.BindByName = True
                    cmd.Parameters.AddRange(parameters)
                    Fetch(cmd, tbl, ds)
                End Using
            End Using
            Dim par = parameters.FirstOrDefault(Function(p As OracleParameter) p.ParameterName.Equals(PAR_OUT_NAME_RETURN_CODE))
            If (Not par Is Nothing AndAlso par.Value = 200) Then
                Throw New ElitaPlusException("ELP_COMM_PLAN_DISTRIBUTION - " + methodName, ErrorCodes.DB_READ_ERROR)
            End If
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

        Return ds
    End Function

    Protected Overrides Sub ConfigureDeleteCommand(ByRef command As OracleCommand, tableName As String)
        command.AddParameter("pi_comm_plan_dist_id", OracleDbType.Raw, sourceColumn:=TABLE_KEY_NAME)
        command.AddParameter("po_exec_status", OracleDbType.Int32, ParameterDirection.Output)
    End Sub

    Protected Overrides Sub ConfigureInsertCommand(ByRef command As OracleCommand, tableName As String)
        With command
            .AddParameter("pi_comm_plan_dist_id", OracleDbType.Raw, sourceColumn:=TABLE_KEY_NAME)
            .AddParameter("pi_comm_plan_id", OracleDbType.Raw, sourceColumn:=COL_NAME_COMM_PLAN_ID)
            .AddParameter("pi_commission_amount", OracleDbType.Varchar2, sourceColumn:=COL_NAME_COMM_AMOUNT)
            .AddParameter("pi_commission_percent", OracleDbType.Varchar2, sourceColumn:=COL_NAME_COMMISSION_PERCENT)
            .AddParameter("pi_position", OracleDbType.Varchar2, sourceColumn:=COL_NAME_POSITION)
            .AddParameter("pi_entity_id", OracleDbType.Raw, sourceColumn:=COL_NAME_ENTITY_ID)
            .AddParameter("pi_payee_type_xcd", OracleDbType.Varchar2, sourceColumn:=COL_NAME_PAYEE_TYPE_XCD)
            .AddParameter("pi_created_by", OracleDbType.Varchar2, sourceColumn:=COL_NAME_CREATED_BY)    
            .AddParameter("pi_comm_source_xcd", OracleDbType.Varchar2, sourceColumn:=COL_NAME_COMMISSION_SOURCE_XCD)
            .AddParameter("pi_act_ent_source_xcd", OracleDbType.Varchar2, sourceColumn:=COL_NAME_ACT_ENT_SOURCE_XCD)
            .AddParameter("po_exec_status", OracleDbType.Int32, ParameterDirection.Output)           
        End With
    End Sub

    Protected Overrides Sub ConfigureUpdateCommand(ByRef command As OracleCommand, tableName As String)
        With command
            .AddParameter("pi_comm_plan_dist_id", OracleDbType.Raw, sourceColumn:=TABLE_KEY_NAME)
            .AddParameter("pi_comm_plan_id", OracleDbType.Raw, sourceColumn:=COL_NAME_COMM_PLAN_ID)
            .AddParameter("pi_commission_amount", OracleDbType.Varchar2, sourceColumn:=COL_NAME_COMM_AMOUNT)
            .AddParameter("pi_commission_percent", OracleDbType.Varchar2, sourceColumn:=COL_NAME_COMMISSION_PERCENT)
            .AddParameter("pi_position", OracleDbType.Varchar2, sourceColumn:=COL_NAME_POSITION)
            .AddParameter("pi_entity_id", OracleDbType.Raw, sourceColumn:=COL_NAME_ENTITY_ID)
            .AddParameter("pi_payee_type_xcd", OracleDbType.Varchar2, sourceColumn:=COL_NAME_PAYEE_TYPE_XCD)
            .AddParameter("pi_modified_by", OracleDbType.Varchar2, sourceColumn:=COL_NAME_COMM_MODIFIED_BY)
            .AddParameter("pi_comm_source_xcd", OracleDbType.Varchar2, sourceColumn:=COL_NAME_COMMISSION_SOURCE_XCD)
            .AddParameter("pi_act_ent_source_xcd", OracleDbType.Varchar2, sourceColumn:=COL_NAME_ACT_ENT_SOURCE_XCD)
            .AddParameter("po_exec_status", OracleDbType.Int32, ParameterDirection.Output)
        End With
    End Sub

End Class






