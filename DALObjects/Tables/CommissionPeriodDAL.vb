'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (11/18/2004)********************

#Region "CommissionPeriodData"

Public Class CommissionPeriodData
    
    Public dealerId As Guid
    Public companyIds As ArrayList

End Class

Public Class CommPrdData

    Public dealerId As Guid
    Public ProductCodeId As Guid
    Public companyIds As ArrayList

End Class
#End Region

Public Class CommissionPeriodDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_COMMISSION_PERIOD"
    Public Const TABLE_KEY_NAME As String = "commission_period_id"
    '  Public Const DSNAME As String = "LIST"

    Public Const COL_NAME_COMMISSION_PERIOD_ID As String = "commission_period_id"
    Public Const COL_NAME_COMM_P_CODE_ID As String = "comm_p_code_id"
    Public Const COL_NAME_DEALER_ID As String = "dealer_id"
    Public Const COL_NAME_COMPANY_CODE As String = "company_code"
    Public Const COL_NAME_EFFECTIVE_DATE As String = "effective_date"
    Public Const COL_NAME_EXPIRATION_DATE As String = "expiration_date"
    Public Const COL_NAME_COMPUTE_METHOD_ID As String = "compute_method_id"
    Public Const COL_NAME_EFFECTIVE As String = "effective"
    Public Const COL_NAME_EXPIRATION As String = "expiration"
    Public Const COL_NAME_DEALER_NAME As String = "dealer_name"
    Public Const COL_NAME_PRODUCT_CODE As String = "product_code"
    Public Const COL_NAME_PRODUCT_CODE_ID As String = "product_code_id"

    Public Const COL_NAME_COMPANY_ID = "company_id"
    Public Const COL_NAME_DEALER_ID0 As String = "dealer_id0"
    Public Const COL_NAME_DEALER_ID1 As String = "dealer_id1"

    Public Const DEALER_ID = 0

    Public Const PRODUCT_ID = 1

    Public Const DEALER_ID0 = 0
    Public Const DEALER_ID1 = 1

    Public Const TOTAL_PARAM = 0
    Public Const TOTAL_PARAM_A = 1

    ' Expiration
    Public Const COL_NAME_MAX_EXPIRATION As String = "expiration"
    Public Const COL_NAME_EXPIRATION_COUNT As String = "expiration_count"

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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("commission_period_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, Me.TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub


    Public Function LoadList(ByVal oCommissionPeriodData As CommissionPeriodData) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST")
        Dim parameters(TOTAL_PARAM) As DBHelper.DBHelperParameter
        Dim inCausecondition As String

        With oCommissionPeriodData
            inCausecondition &= MiscUtil.BuildListForSql("AND D." & Me.COL_NAME_COMPANY_ID, oCommissionPeriodData.companyIds, True)

            If .dealerId.Equals(Guid.Empty) Then
                parameters(DEALER_ID) = New DBHelper.DBHelperParameter(COL_NAME_DEALER_ID, GenericConstants.WILDCARD)
            Else
                parameters(DEALER_ID) = New DBHelper.DBHelperParameter(COL_NAME_DEALER_ID, .dealerId.ToByteArray)
            End If
            selectStmt = selectStmt.Replace(Me.DYNAMIC_IN_CLAUSE_PLACE_HOLDER, inCausecondition)
        End With

        Try
            Dim ds As New DataSet
            DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function LoadListCommPrd(ByVal oCommPrdData As CommPrdData) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/LOAD_COMMPRD_LIST")
        Dim parameters(TOTAL_PARAM_A) As DBHelper.DBHelperParameter
        Dim inCausecondition As String

        With oCommPrdData
            inCausecondition &= MiscUtil.BuildListForSql("AND D." & Me.COL_NAME_COMPANY_ID, oCommPrdData.companyIds, True)
            If .dealerId.Equals(Guid.Empty) Then
                parameters(DEALER_ID) = New DBHelper.DBHelperParameter(COL_NAME_DEALER_ID, GenericConstants.WILDCARD)
            Else
                parameters(DEALER_ID) = New DBHelper.DBHelperParameter(COL_NAME_DEALER_ID, .dealerId.ToByteArray)
            End If
            If .ProductCodeId.Equals(Guid.Empty) Then
                parameters(PRODUCT_ID) = New DBHelper.DBHelperParameter(COL_NAME_PRODUCT_CODE_ID, GenericConstants.WILDCARD)
            Else
                parameters(PRODUCT_ID) = New DBHelper.DBHelperParameter(COL_NAME_PRODUCT_CODE_ID, .ProductCodeId.ToByteArray)
            End If
            selectStmt = selectStmt.Replace(Me.DYNAMIC_IN_CLAUSE_PLACE_HOLDER, inCausecondition)
        End With

        Try
            Dim ds As New DataSet
            DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function LoadExpiration(ByVal oCommissionPeriodData As CommissionPeriodData) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/MAX_EXPIRATION")
        Dim parameters(TOTAL_PARAM_A) As DBHelper.DBHelperParameter

        With oCommissionPeriodData
            parameters(DEALER_ID0) = New DBHelper.DBHelperParameter(COL_NAME_DEALER_ID0, .dealerId.ToByteArray)
            parameters(DEALER_ID1) = New DBHelper.DBHelperParameter(COL_NAME_DEALER_ID1, .dealerId.ToByteArray)
        End With

        Try
            Dim ds As New DataSet
            DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)
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
            commPeriodEntityDAL.Update(familyDataset.GetChanges(DataRowState.Deleted), tr, DataRowState.Deleted)
            assocCommDAL.Update(familyDataset.GetChanges(DataRowState.Deleted), tr, DataRowState.Deleted)
            commToleranceDAL.Update(familyDataset.GetChanges(DataRowState.Deleted), tr, DataRowState.Deleted)
            MyBase.Update(familyDataset.Tables(Me.TABLE_NAME).GetChanges(DataRowState.Deleted), tr, DataRowState.Deleted)

            'Second Pass updates additions and changes
            'addressDAL.Update(familyDataset.GetChanges(DataRowState.Added Or DataRowState.Modified), tr, DataRowState.Added Or DataRowState.Modified)
            Update(familyDataset.Tables(Me.TABLE_NAME).GetChanges(DataRowState.Added Or DataRowState.Modified), tr, DataRowState.Added Or DataRowState.Modified)
            commPeriodEntityDAL.Update(familyDataset.GetChanges(DataRowState.Added Or DataRowState.Modified), tr, DataRowState.Added Or DataRowState.Modified)
            commToleranceDAL.Update(familyDataset.GetChanges(DataRowState.Added Or DataRowState.Modified), tr, DataRowState.Added Or DataRowState.Modified)
            assocCommDAL.Update(familyDataset.GetChanges(DataRowState.Added Or DataRowState.Modified), tr, DataRowState.Added Or DataRowState.Modified)

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


End Class



