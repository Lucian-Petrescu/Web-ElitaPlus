'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (2/1/2010)********************
#Region "CommissionPeriodData"

Public Class CommPCodeData

    Public dealerId As Guid
    Public productCodeId As Guid
    Public companyIds As ArrayList

End Class

#End Region

Public Class CommPCodeDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_COMM_P_CODE"
    Public Const TABLE_KEY_NAME As String = "comm_p_code_id"

    Public Const COL_NAME_COMM_P_CODE_ID As String = "comm_p_code_id"
    Public Const COL_NAME_PRODUCT_CODE_ID As String = "product_code_id"
    Public Const COL_NAME_EFFECTIVE_DATE As String = "effective_date"
    Public Const COL_NAME_EXPIRATION_DATE As String = "expiration_date"

    Public Const COL_NAME_EFFECTIVE As String = "effective"
    Public Const COL_NAME_EXPIRATION As String = "expiration"
    Public Const COL_NAME_COMPANY_CODE As String = "company_code"
    Public Const COL_NAME_DEALER_NAME As String = "dealer_name"
    Public Const COL_NAME_PRODUCT_CODE As String = "product_code"

    Public Const COL_NAME_COMPANY_ID = "company_id"
    Public Const COL_NAME_DEALER_ID As String = "dealer_id"

    Public Const COL_NAME_PRODUCT_CODE_ID0 As String = "product_code_id0"
    Public Const COL_NAME_PRODUCT_CODE_ID1 As String = "product_code_id1"

    Public Const DEALER_ID = 0

    Public Const PRODUCT_CODE_ID0 = 0
    Public Const PRODUCT_CODE_ID1 = 1
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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("comm_p_code_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, Me.TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadList(ByVal oCommPCodeData As CommPCodeData) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST")
        Dim parameters(TOTAL_PARAM) As DBHelper.DBHelperParameter
        Dim inCausecondition As String

        With oCommPCodeData
            inCausecondition &= MiscUtil.BuildListForSql("AND d." & Me.COL_NAME_COMPANY_ID, oCommPCodeData.companyIds, True)

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

    Public Function LoadExpiration(ByVal oCommPCodeData As CommPCodeData) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/MAX_EXPIRATION")
        Dim parameters(TOTAL_PARAM_A) As DBHelper.DBHelperParameter

        With oCommPCodeData
            parameters(PRODUCT_CODE_ID0) = New DBHelper.DBHelperParameter(COL_NAME_PRODUCT_CODE_ID0, .productCodeId.ToByteArray)
            parameters(PRODUCT_CODE_ID1) = New DBHelper.DBHelperParameter(COL_NAME_PRODUCT_CODE_ID1, .productCodeId.ToByteArray)
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
    Public Overloads Sub Update(ByVal ds As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing, Optional ByVal changesFilter As DataRowState = Nothing)
        If ds Is Nothing Then
            Return
        End If
        If Not ds.Tables(Me.TABLE_NAME) Is Nothing Then
            MyBase.Update(ds.Tables(Me.TABLE_NAME), Transaction, changesFilter)
        End If
    End Sub

    'This method was added manually to accommodate BO families Save
    Public Overloads Sub UpdateFamily(ByVal familyDataset As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing)
        Dim entityDAL As New CommPCodeEntityDAL
        Dim tr As IDbTransaction = Transaction
        If tr Is Nothing Then
            tr = DBHelper.GetNewTransaction
        End If
        Try
            'First Pass updates Deletions
            entityDAL.Update(familyDataset, tr, DataRowState.Deleted)
            Me.Update(familyDataset, tr, DataRowState.Deleted)

            'Second Pass updates additions and changes
            Me.Update(familyDataset, tr, DataRowState.Added Or DataRowState.Modified)

            entityDAL.Update(familyDataset, tr, DataRowState.Added Or DataRowState.Modified)
            
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

#End Region


End Class


