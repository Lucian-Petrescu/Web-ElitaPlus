'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (4/8/2014)********************


Public Class ExcludeListitemByRoleDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_EXCLUDE_LISTITEM_BY_ROLE"
    Public Const TABLE_KEY_NAME As String = "exclude_listitem_role_id"

    Public Const COL_NAME_EXCLUDE_LISTITEM_ROLE_ID As String = "exclude_listitem_role_id"
    Public Const COL_NAME_LIST_ITEM_ID As String = "list_item_id"
    Public Const COL_NAME_COMPANY_ID As String = "company_id"
    Public Const COL_NAME_LIST_ID As String = "list_id"

    Public Const COL_NAME_LANGUAGE_ID_1 As String = "LANGUAGE_ID1"

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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("exclude_listitem_role_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, Me.TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadList(ByVal compId As Guid, ByVal ListId As Guid, _
                         ByVal ListItemId As Guid, ByVal RoleId As Guid, ByVal LanguageId As Guid) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST")
        Dim parameters() As DBHelper.DBHelperParameter     
        Dim whereClauseConditions As String = ""

        If Not compId.Equals(Guid.Empty) Then
            whereClauseConditions &= "And c.company_id = " & MiscUtil.GetDbStringFromGuid(compId)
        End If

        If Not ListId.Equals(Guid.Empty) Then
            whereClauseConditions &= Environment.NewLine & "AND " & "l.LIST_ID = " & MiscUtil.GetDbStringFromGuid(ListId)
        End If

        If Not ListItemId.Equals(Guid.Empty) Then
            whereClauseConditions &= Environment.NewLine & "AND " & "li.LIST_ITEM_ID = " & MiscUtil.GetDbStringFromGuid(ListItemId)
        End If

        If Not RoleId.Equals(Guid.Empty) Then
            whereClauseConditions &= Environment.NewLine & "AND " & "r.Role_Id = " & MiscUtil.GetDbStringFromGuid(RoleId)
        End If

        parameters = New DBHelper.DBHelperParameter() _
                                    {New DBHelper.DBHelperParameter(COL_NAME_LANGUAGE_ID, LanguageId.ToByteArray), _
                                     New DBHelper.DBHelperParameter(COL_NAME_LANGUAGE_ID_1, LanguageId.ToByteArray)}

        If Not whereClauseConditions = "" Then
            selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
        Else
            selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, "")
        End If

        Dim ds As New DataSet
        Try
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

    Public Overloads Sub UpdateFamily(ByVal familyDataset As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing)
        Dim ExcludedLiRolesDal As New ExcludedLiRolesDAL

        Dim tr As IDbTransaction = Transaction
        If tr Is Nothing Then
            tr = DBHelper.GetNewTransaction
        End If
        Try
            'First Pass updates Deletions           
            ExcludedLiRolesDal.Update(familyDataset, tr, DataRowState.Deleted)

            MyBase.Update(familyDataset.Tables(Me.TABLE_NAME).GetChanges(DataRowState.Deleted), tr, DataRowState.Deleted)

            'Second Pass updates additions and changes            
            Update(familyDataset.Tables(Me.TABLE_NAME).GetChanges(DataRowState.Added Or DataRowState.Modified), tr, DataRowState.Added Or DataRowState.Modified)

            ExcludedLiRolesDal.Update(familyDataset.GetChanges(DataRowState.Added Or DataRowState.Modified), tr, DataRowState.Added Or DataRowState.Modified)

            'At the end delete the Address
            If Not familyDataset.Tables(TransactionLogHeaderDAL.TABLE_NAME) Is Nothing AndAlso familyDataset.Tables(TransactionLogHeaderDAL.TABLE_NAME).Rows.Count > 0 Then
                Dim oTransactionLogHeaderDAL As New TransactionLogHeaderDAL
                oTransactionLogHeaderDAL.Update(familyDataset.Tables(TransactionLogHeaderDAL.TABLE_NAME), tr, DataRowState.Added Or DataRowState.Modified)
            End If

            If Transaction Is Nothing Then
                'We are the creator of the transaction we should commit it  and close the connection
                DBHelper.Commit(tr)
                familyDataset.AcceptChanges()
            End If
        Catch ex As Exception
            If Transaction Is Nothing Then
                'We are the creator of the transaction we should commit it  and close the connection
                DBHelper.RollBack(tr)
            End If
            Throw ex
        End Try
    End Sub


#End Region


End Class


