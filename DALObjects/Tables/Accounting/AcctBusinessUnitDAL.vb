'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (4/19/2007)********************


Public Class AcctBusinessUnitDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_ACCT_BUSINESS_UNIT"
    Public Const TABLE_KEY_NAME As String = "acct_business_unit_id"

    Public Const COL_NAME_ACCT_BUSINESS_UNIT_ID As String = "acct_business_unit_id"
    Public Const COL_NAME_ACCT_COMPANY_ID As String = "acct_company_id"
    Public Const COL_NAME_ACCT_COMPANY_ID_ALIAS As String = "a.acct_company_id"
    Public Const COL_NAME_ACCT_COMPANY_DESCRIPTION As String = "description"
    Public Const COL_NAME_BUSINESS_UNIT As String = "business_unit"
    Public Const COL_NAME_CODE As String = "code"
    Public Const COL_NAME_SUPPRESS_VENDORS As String = "suppress_vendors"
    Public Const COL_NAME_PAYMENT_METHOD_ID As String = "payment_method_id"

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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("acct_business_unit_id", id.ToByteArray)}
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

    Public Function LoadList(ByVal BusinessUnitNameMask As String, ByVal AcctcompanyMask As Guid, Optional ByVal myAcctCompany As ArrayList = Nothing) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST")
        Dim whereClauseConditions As String = ""
        Dim inCausecondition As String = ""
        Dim bIsLikeClause As Boolean = False
        Dim strAcctcompanyMask As String = ""

        If (Not (AcctcompanyMask.Equals(Guid.Empty))) Then
            strAcctcompanyMask = Me.GuidToSQLString(AcctcompanyMask)
        End If

        If ((Not (BusinessUnitNameMask Is Nothing)) AndAlso (Me.FormatSearchMask(BusinessUnitNameMask))) Then
            whereClauseConditions &= " AND UPPER(" & Me.COL_NAME_BUSINESS_UNIT & ")" & BusinessUnitNameMask.ToUpper
        End If

        If ((Not (strAcctcompanyMask Is Nothing)) AndAlso (Me.FormatSearchMask(strAcctcompanyMask))) Then
            whereClauseConditions &= " AND UPPER(" & Me.COL_NAME_ACCT_COMPANY_ID_ALIAS & ")" & strAcctcompanyMask.ToUpper
        End If

        If Not myAcctCompany Is Nothing Then
            whereClauseConditions &= Environment.NewLine & " AND " & MiscUtil.BuildListForSql(AcctBusinessUnitDAL.COL_NAME_ACCT_COMPANY_ID_ALIAS, myAcctCompany, False)
        End If

        If (whereClauseConditions <> "") Then
            selectStmt &= whereClauseConditions
        End If

        selectStmt &= " ORDER BY UPPER(a.business_unit)"

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
#End Region


End Class


