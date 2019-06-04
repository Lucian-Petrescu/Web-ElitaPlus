'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (4/12/2010)********************


Public Class CompanyCreditCardDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_COMPANY_CREDIT_CARD"
    Public Const TABLE_KEY_NAME As String = "company_credit_card_id"

    Public Const COL_NAME_COMPANY_CREDIT_CARD_ID As String = "company_credit_card_id"
    Public Const COL_NAME_COMPANY_ID As String = "company_id"
    Public Const COL_NAME_CREDIT_CARD_FORMAT_ID As String = "credit_card_format_id"
    Public Const COL_NAME_CREDIT_CARD_TYPE As String = "credit_card_type"
    Public Const COL_NAME_BILLING_DATE As String = "billing_date"

    Private Const DSNAME As String = "LIST"
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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("company_credit_card_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, Me.TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Sub Load(ByVal familyDS As DataSet, ByVal CreditCardFormatId As Guid, ByVal CompanyId As Guid)
        Dim selectStmt As String = Me.Config("/SQL/LOAD_BY_CREDIT_CARD_FORMAT")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("credit_card_format_id", CreditCardFormatId.ToByteArray), _
                                            New DBHelper.DBHelperParameter(COL_NAME_COMPANY_ID, CompanyId.ToByteArray)}
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
#End Region

#Region "CRUD Methods"

    Public Function LoadList(ByVal languageId As Guid, ByVal compIds As ArrayList) As DataSet

        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST")
        Dim inCausecondition As String = ""

        inCausecondition &= MiscUtil.BuildListForSql("And CCC." & Me.COL_NAME_COMPANY_ID, compIds, False)

        Dim parameters() As OracleParameter
        parameters = New OracleParameter() {New OracleParameter(COL_NAME_LANGUAGE_ID, languageId.ToByteArray)}

        selectStmt = selectStmt.Replace(Me.DYNAMIC_IN_CLAUSE_PLACE_HOLDER, inCausecondition)

        Try
            Return (DBHelper.Fetch(selectStmt, DSNAME, Me.TABLE_NAME, parameters))
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function
#End Region
End Class


