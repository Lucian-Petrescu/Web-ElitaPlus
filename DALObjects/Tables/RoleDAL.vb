'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (3/13/2009)********************


Public Class RoleDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_ROLE"
    Public Const TABLE_KEY_NAME As String = "role_id"

    Public Const COL_NAME_ROLE_ID As String = "role_id"
    Public Const COL_NAME_CODE As String = "code"
    Public Const COL_NAME_DESCRIPTION As String = "description"
    Public Const COL_NAME_IHQ_ONLY As String = "ihq_only"
    Public Const COL_NAME_ROLE_PROVIDER_ID As String = "role_provider_id"
    Public Const COL_NAME_REMOTE_ROLE_ID As String = "remote_role_id"

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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("role_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, Me.TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadList(ByVal strCode As String, ByVal strDec As String) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST")
        Dim whereClauseConditions As String = ""
        Dim ds As New DataSet

        If Me.FormatSearchMask(strCode) Then
            whereClauseConditions &= " WHERE " & Environment.NewLine & "UPPER(" & Me.COL_NAME_CODE & ") " & strCode.ToUpper
        End If


        If Me.FormatSearchMask(strDec) Then
            If whereClauseConditions = "" Then
                whereClauseConditions &= " WHERE " & Environment.NewLine & "UPPER(" & Me.COL_NAME_DESCRIPTION & ") " & strDec.ToUpper
            Else
                whereClauseConditions &= Environment.NewLine & "AND  UPPER(" & Me.COL_NAME_DESCRIPTION & ") " & strDec.ToUpper
            End If
        End If

        If Not whereClauseConditions = "" Then
            selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
        Else
            selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, "")
        End If

        Try
            ds = DBHelper.Fetch(selectStmt, Me.TABLE_NAME)
            AddSequenceColumn(ds.Tables(TABLE_NAME))
            Return ds

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

        Return DBHelper.Fetch(selectStmt, Me.TABLE_NAME)
    End Function

    Public Function LoadList() As DataSet
        Dim selectStmt As String = Me.Config("/SQL/LOAD_ROLES_LIST")
        Dim whereClauseConditions As String = ""

        Try
            Return (DBHelper.Fetch(selectStmt, Me.TABLE_NAME))
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

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

    Public Overloads Sub UpdateFamily(ByVal familyDataset As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing)
        Dim oRolePermissionDal As New RolePermissionDAL

        Dim tr As IDbTransaction = Transaction
        If tr Is Nothing Then
            tr = DBHelper.GetNewTransaction
        End If
        Try
            'First Pass updates Deletions
            oRolePermissionDal.Update(familyDataset.GetChanges(DataRowState.Deleted), tr, DataRowState.Deleted)

            MyBase.Update(familyDataset.Tables(Me.TABLE_NAME).GetChanges(DataRowState.Deleted), tr, DataRowState.Deleted)

            'Second Pass updates additions and changes
            MyBase.Update(familyDataset.Tables(Me.TABLE_NAME).GetChanges(DataRowState.Added Or DataRowState.Modified), tr, DataRowState.Added Or DataRowState.Modified)

            oRolePermissionDal.Update(familyDataset.GetChanges(DataRowState.Added Or DataRowState.Modified), tr, DataRowState.Added Or DataRowState.Modified)

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



