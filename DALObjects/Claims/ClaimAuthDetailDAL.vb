'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (7/23/2007)********************


Public Class ClaimAuthDetailDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_CLAIM_AUTH_DETAIL"
    Public Const TABLE_KEY_NAME As String = "claim_auth_detail_id"

    Public Const COL_NAME_CLAIM_AUTH_DETAIL_ID As String = "claim_auth_detail_id"
    Public Const COL_NAME_CLAIM_ID As String = "claim_id"
    Public Const COL_NAME_LABOR_AMOUNT As String = "labor_amount"
    Public Const COL_NAME_PART_AMOUNT As String = "part_amount"
    Public Const COL_NAME_SERVICE_CHARGE As String = "service_charge"
    Public Const COL_NAME_TRIP_AMOUNT As String = "trip_amount"
    Public Const COL_NAME_SHIPPING_AMOUNT As String = "shipping_amount"
    Public Const COL_NAME_OTHER_AMOUNT As String = "other_amount"
    Public Const COL_NAME_OTHER_EXPLANATION As String = "other_explanation"
    Public Const COL_NAME_DISPOSITION_AMOUNT As String = "disposition_amount"
    Public Const COL_NAME_DIAGNOSTICS_AMOUNT As String = "diagnostics_amount"
    Public Const COL_NAME_TOTAL_TAX_AMOUNT As String = "total_tax_amount"

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

    Public Sub Load(ByVal familyDS As DataSet, ByVal id As Guid, Optional ByVal blnLoadByClaimID As Boolean = False)
        Dim selectStmt As String
        Dim parameters() As DBHelper.DBHelperParameter
        If blnLoadByClaimID Then
            selectStmt = Me.Config("/SQL/LOAD_BY_CLAIM_ID")
            parameters = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("claim_id", id.ToByteArray)}
        Else
            selectStmt = Me.Config("/SQL/LOAD")
            parameters = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("claim_auth_detail_id", id.ToByteArray)}
        End If
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
        Dim oPartsInfoDAL As New PartsInfoDAL
        Dim oClaimDAL As New ClaimDAL

        Dim tr As IDbTransaction = Transaction
        If tr Is Nothing Then
            tr = DBHelper.GetNewTransaction
        End If
        Try
            'First Pass updates Deletions
            'to be used by maintain invoice use case
            oPartsInfoDAL.Update(familyDataset, tr, DataRowState.Deleted)
            'no changes for claimAuthDetail DAL
            Me.Update(familyDataset.Tables(Me.TABLE_NAME), tr, DataRowState.Deleted)
            oClaimDAL.Update(familyDataset, tr, DataRowState.Deleted)

            If Not familyDataset.Tables(ClaimStatusDAL.TABLE_NAME) Is Nothing AndAlso familyDataset.Tables(ClaimStatusDAL.TABLE_NAME).Rows.Count > 0 Then
                Dim oClaimStatusDAL As New ClaimStatusDAL
                oClaimStatusDAL.Update(familyDataset.Tables(ClaimStatusDAL.TABLE_NAME), tr, DataRowState.Added Or DataRowState.Modified)
            End If

            'Second Pass updates additions and changes
            Me.Update(familyDataset.Tables(Me.TABLE_NAME), tr, DataRowState.Added Or DataRowState.Modified)
            oPartsInfoDAL.Update(familyDataset, tr, DataRowState.Added Or DataRowState.Modified)
            oClaimDAL.Update(familyDataset, tr, DataRowState.Added Or DataRowState.Modified)

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


