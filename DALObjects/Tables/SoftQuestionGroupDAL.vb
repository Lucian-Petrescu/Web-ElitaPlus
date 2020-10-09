'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (11/11/2004)********************


Public Class SoftQuestionGroupDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_SOFT_QUESTION_GROUP"
    Public Const TABLE_KEY_NAME As String = "soft_question_group_id"

    Public Const COL_NAME_SOFT_QUESTION_GROUP_ID As String = "soft_question_group_id"
    'Public Const COL_NAME_COMPANY_ID As String = "company_id"
    Public Const COL_NAME_DESCRIPTION As String = "description"
    Public Const COL_NAME_COMPANY_GROUP_ID As String = "company_group_id"
    Private Const DSNAME As String = "LIST"
    Public Const WILDCARD As Char = "%"
    

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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("soft_question_group_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadList() As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_LIST")
        Return DBHelper.Fetch(selectStmt, TABLE_NAME)
    End Function

    Public Function LoadList(description As String, companyGroupId As Guid) As DataSet

        Dim selectStmt As String = Config("/SQL/LOAD_LIST")
        Dim parameters() As DBHelper.DBHelperParameter
        description = GetFormattedSearchStringForSQL(description)
        parameters = New DBHelper.DBHelperParameter() _
                                    {New DBHelper.DBHelperParameter(COL_NAME_DESCRIPTION, description), _
                                     New DBHelper.DBHelperParameter(COL_NAME_COMPANY_GROUP_ID, companyGroupId.ToByteArray())}
        Try
            Dim ds As New DataSet
            DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function


#End Region

#Region "Overloaded Methods"

    'This method was added manually to accommodate BO families Save
    Public Overloads Sub UpdateFamily(familyDataset As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing)
        Dim tr As IDbTransaction = Transaction
        If tr Is Nothing Then
            tr = DBHelper.GetNewTransaction
        End If
        Try
            Dim riskTypeDAL As New riskTypeDAL

            'if we need to delete soft questions group, then risk type must be modified first.
            'if we need to add a risk type to a softquestions group, then risk type must be modified later
            'because if it is a new softquestions group, we might get foreign key violation.
            'so to achieve this,we process deletes later....

            Update(familyDataset.Tables(TABLE_NAME), tr, DataRowState.Added Or DataRowState.Modified)
            riskTypeDAL.Update(familyDataset, tr, DataRowState.Added Or DataRowState.Modified)
            Update(familyDataset.Tables(TABLE_NAME), tr, DataRowState.Deleted)

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

    Public Overloads Sub Update(ds As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing, Optional ByVal changesFilter As DataRowState = Nothing)
        If ds.Tables(TABLE_NAME) IsNot Nothing Then
            MyBase.Update(ds.Tables(TABLE_NAME), Transaction, changesFilter)
        End If
    End Sub
#End Region


End Class



