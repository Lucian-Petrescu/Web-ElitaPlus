'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (4/7/2010)********************


Public Class CreditCardFormatDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_CREDIT_CARD_FORMAT"
    Public Const TABLE_KEY_NAME As String = "credit_card_format_id"

    Public Const COL_NAME_CREDIT_CARD_FORMAT_ID As String = "credit_card_format_id"
    Public Const COL_NAME_REGULAR_EXPRESSION_ID As String = "regular_expression_id"

    Public Const COL_NAME_CREDIT_CARD_TYPE_CODE As String = "credit_card_type_code"
    Public Const COL_NAME_CREDIT_CARD_TYPE_ID As String = "credit_card_type_id"


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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("credit_card_format_id", id.ToByteArray)}
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


#End Region

#Region "Overloaded Methods"
    Public Overloads Sub Update(ds As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing, Optional ByVal changesFilter As DataRowState = Nothing)
        If ds Is Nothing Then
            Return
        End If
        If ds.Tables(TABLE_NAME) IsNot Nothing Then
            MyBase.Update(ds.Tables(TABLE_NAME), Transaction, changesFilter)
        End If
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

#Region "CRUD Methods"

    Public Function LoadList(CreditCardTypeId As Guid, languageId As Guid) As DataSet

        Dim selectStmt As String = Config("/SQL/LOAD_LIST")
        Dim whereClauseConditions As String = ""

        If Not CreditCardTypeId.Equals(Guid.Empty) Then
            whereClauseConditions &= Environment.NewLine & "where CCF.credit_card_type_id = " & MiscUtil.GetDbStringFromGuid(CreditCardTypeId)
        End If

        If Not whereClauseConditions = "" Then
            selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
        Else
            selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, "")

        End If

        Dim parameters() As OracleParameter
        parameters = New OracleParameter() {New OracleParameter(COL_NAME_LANGUAGE_ID, languageId.ToByteArray)}

        Try
            Return (DBHelper.Fetch(selectStmt, DSNAME, TABLE_NAME, parameters))
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Function LoadByCode(CreditCardTypeCode As String, languageId As Guid) As DataSet

        Dim selectStmt As String = Config("/SQL/LOAD_BY_CODE")


        Dim parameters() As OracleParameter
        parameters = New OracleParameter() {New OracleParameter(COL_NAME_CREDIT_CARD_TYPE_CODE, CreditCardTypeCode), _
                                            New OracleParameter(COL_NAME_LANGUAGE_ID, languageId.ToByteArray)}

        Try
            Return (DBHelper.Fetch(selectStmt, DSNAME, TABLE_NAME, parameters))
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function
#End Region
End Class


