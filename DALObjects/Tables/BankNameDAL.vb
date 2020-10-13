'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (3/19/2007)********************


Public Class BankNameDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_BANK"
    Public Const TABLE_KEY_NAME As String = "bank_id"

    Public Const COL_NAME_BANK_ID As String = "bank_id"
    Public Const COL_NAME_CODE As String = "code"
    Public Const COL_NAME_DESCRIPTION As String = "description"
    Public Const COL_NAME_COUNTRY_ID As String = "country_id"
    Public Const COL_NAME_CREATED_BY As String = "created_by"
    Public Const COL_NAME_CREATED_DATE As String = "created_date"
    Public Const COL_NAME_MODIFIED_BY As String = "modified_by"
    Public Const COL_NAME_MODIFIED_DATE As String = "modified_date"
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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("bank_id", id.ToByteArray)}
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

    Public Function LoadList(BankName As String, codeMask As String, CountryID As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_LIST")
        Dim whereClauseConditions As String = ""
        Dim inCausecondition As String = ""
        Dim bIsLikeClause As Boolean = False

        bIsLikeClause = IsLikeClause(codeMask)

        whereClauseConditions &= " WHERE UPPER(" & COL_NAME_COUNTRY_ID & ") = UPPER('" & GuidToSQLString(CountryID) & "')"

        If Not String.IsNullOrEmpty(BankName) Then
            whereClauseConditions &= " AND UPPER(" & COL_NAME_DESCRIPTION & ") = UPPER('" & BankName & "')"
        End If

        If ((Not (codeMask Is Nothing)) AndAlso (FormatSearchMask(codeMask))) Then
            whereClauseConditions &= " AND UPPER(" & COL_NAME_CODE & ")" & codeMask.ToUpper
        End If

        selectStmt = selectStmt.Replace(DYNAMIC_IN_CLAUSE_PLACE_HOLDER, inCausecondition)

        If Not whereClauseConditions = "" Then
            selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
        Else
            selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, "")
        End If

        selectStmt = selectStmt.Replace(DYNAMIC_ORDER_BY_CLAUSE_PLACE_HOLDER, " ORDER BY " & COL_NAME_DESCRIPTION & ", " & COL_NAME_CODE)
        Try
            'Dim ds = New DataSet
            Return (DBHelper.Fetch(selectStmt, TABLE_NAME))
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try


        Return DBHelper.Fetch(selectStmt, TABLE_NAME)
    End Function

    Public Function LoadBankNameByCountry(CountryID As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_BANK_NAME_BY_COUNTRY")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("country_id", CountryID.ToByteArray)}
        Try
            Dim ds = New DataSet
            DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

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
#End Region


End Class



