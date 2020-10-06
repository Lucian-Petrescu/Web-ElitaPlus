'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (9/23/2004)********************


Public Class PostalCodeFormatDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_POSTAL_CODE_FORMAT"
    Public Const TABLE_KEY_NAME As String = "postal_code_format_id"

    Public Const COL_NAME_POSTAL_CODE_FORMAT_ID = "postal_code_format_id"
    Public Const COL_NAME_REGULAR_EXPRESSION_ID As String = "regular_expression_id"
    Public Const COL_NAME_DESCRIPTION = "description"
    Public Const COL_NAME_LOCATOR_START_POSITION = "locator_start_position"
    Public Const COL_NAME_LOCATOR_LENGTH = "locator_length"
    Public Const COL_NAME_REFORMAT_FILE_INPUT_FLAG = "reformat_file_input_flag"
    Public Const COL_NAME_COMUNA_ENABLED = "comuna_enabled"
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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("postal_code_format_id", id.ToByteArray)}
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

    Public Function LoadList(description As String) As DataSet

        Dim selectStmt As String = Config("/SQL/LOAD_LIST")
        Dim parameters() As OracleParameter
        description = GetFormattedSearchStringForSQL(description)
        parameters = New OracleParameter() _
                                    {New OracleParameter(COL_NAME_DESCRIPTION, description)}
        Try
            Return (DBHelper.Fetch(selectStmt, DSNAME, TABLE_NAME, parameters))
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function
#End Region

#Region "Overloaded Methods"
    Public Overloads Sub Update(ds As DataSet, Optional ByVal transaction As IDbTransaction = Nothing)
        DBHelper.Execute(ds.Tables(TABLE_NAME), Config("/SQL/INSERT"), Config("/SQL/UPDATE"), Config("/SQL/DELETE"), Nothing, transaction)
    End Sub

    'This method was added manually to accommodate BO families Save
    Public Overloads Sub UpdateFamily(familyDataset As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing)
        Dim RegularExpressionDAL As New RegularExpressionDAL

        Dim tr As IDbTransaction = Transaction
        If tr Is Nothing Then
            tr = DBHelper.GetNewTransaction
        End If
        Try
            'First Pass updates Deletions
            'to be used by maintain invoice use case
            RegularExpressionDAL.Update(familyDataset, tr, DataRowState.Deleted)
            Update(familyDataset.Tables(TABLE_NAME), tr, DataRowState.Deleted)

            'Second Pass updates additions and changes
            RegularExpressionDAL.Update(familyDataset.Tables(RegularExpressionDAL.TABLE_NAME), tr, DataRowState.Added Or DataRowState.Modified)
            Update(familyDataset.Tables(TABLE_NAME), tr, DataRowState.Added Or DataRowState.Modified)

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


