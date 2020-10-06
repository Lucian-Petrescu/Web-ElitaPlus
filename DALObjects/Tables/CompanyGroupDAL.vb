'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (5/8/2006)********************


Public Class CompanyGroupDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_COMPANY_GROUP"
    Public Const TABLE_KEY_NAME As String = "company_group_id"

    Public Const COL_NAME_COMPANY_GROUP_ID As String = "company_group_id"
    Public Const COL_NAME_DESCRIPTION As String = "description"
    Public Const COL_NAME_CODE As String = "code"
    Public Const COL_NAME_CLAIM_NUMBERING_BY_ID As String = "claim_numbering_by_id"
    Public Const COL_NAME_LANGUAGE_ID As String = "language_id"
    Public Const COL_NAME_ACCT_BY_COMPANY As String = "acct_by_company"
    Public Const COL_NAME_INVOICE_NUMBERING_BY_ID As String = "invoice_numbering_by_id"
    Public Const COL_NAME_FTP_SITE_ID As String = "ftp_site_id"
    'REQ-1142
    Public Const COL_NAME_INACTIVE_USED_VEHICLES_Older_THAN = "years_to_inactive_usedvehicles"
    Public Const COL_NAME_INACTIVE_NEW_VEHICLES_BASED_ON = "inactive_newvehicles_based_on"
    'REQ-1142 End
    Private Const DSNAME As String = "LIST"
    Public Const WILDCARD As Char = "%"
    'REQ-863
    Public Const COL_NAME_INVOICE_GROUP_NUMBERING_BY_ID As String = "invoice_group_numbering_by_id"
    Public Const COL_NAME_AUTHORIZATION_NUMBERING_BY_ID As String = "authorization_numbering_by_id"
    Public Const COL_NAME_PAYMENT_GROUP_NUMBERING_BY_ID As String = "payment_group_numbering_by_id"
    'REQ-863 End

    'REQ 5547
    Public Const COL_NAME_CLAIM_FAST_APPROVAL_ID As String = "claim_fast_approval_id"
    'REQ-5773
    Public Const COL_NAME_USE_COMM_ENTITY_TYPE_ID As String = "use_comm_entity_type_id"

    Public Const COL_NAME_CASE_NUMBERING_BY_XCD As String = "case_numbering_by_xcd"
    Public Const COL_NAME_INTERACTION_NUMBERING_BY_XCD As String = "interaction_numbering_by_xcd"

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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("company_group_id", id.ToByteArray)}
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

    Public Function LoadList(description As String, code As String, languageId As Guid, Optional ByVal getCovTypeChidrens As Boolean = False) As DataSet

        Dim selectStmt As String = Config("/SQL/LOAD_LIST")
        If getCovTypeChidrens Then
            selectStmt = Config("/SQL/LOAD_LIST_OF_CHIDRES_COVERAGE_TYPE")
        End If
        Dim parameters() As OracleParameter
        description = GetFormattedSearchStringForSQL(description)
        code = GetFormattedSearchStringForSQL(code)

        If Not getCovTypeChidrens Then
            parameters = New OracleParameter() _
                                        {New OracleParameter(COL_NAME_LANGUAGE_ID, languageId.ToByteArray), _
                                        New OracleParameter(COL_NAME_LANGUAGE_ID, languageId.ToByteArray), _
                                         New OracleParameter(COL_NAME_LANGUAGE_ID, languageId.ToByteArray), _
                                         New OracleParameter(COL_NAME_CODE, code), _
                                         New OracleParameter(COL_NAME_DESCRIPTION, description)}
        Else
            parameters = New OracleParameter() _
                                        {New OracleParameter(COL_NAME_CODE, code), _
                                         New OracleParameter(COL_NAME_DESCRIPTION, description)}
        End If
        Try
            Return (DBHelper.Fetch(selectStmt, DSNAME, TABLE_NAME, parameters))
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

#End Region

#Region "Overloaded Methods"

    Public Overloads Sub UpdateFamily(familyDataset As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing)
        Dim CoverageCompGrpCntl As New CoverageByCompanyGroupDAL

        Dim tr As IDbTransaction = Transaction
        If tr Is Nothing Then
            tr = DBHelper.GetNewTransaction
        End If
        Try
            'First Pass updates Deletions
            CoverageCompGrpCntl.Update(familyDataset, tr, DataRowState.Deleted)

            MyBase.Update(familyDataset.Tables(TABLE_NAME), tr, DataRowState.Deleted)

            'Second Pass updates additions and changes
            Update(familyDataset.Tables(TABLE_NAME), tr, DataRowState.Added Or DataRowState.Modified)

            CoverageCompGrpCntl.Update(familyDataset, tr, DataRowState.Added Or DataRowState.Modified)

            If Transaction Is Nothing Then
                'We are the creator of the transaction we should commit it  and close the connection
                DBHelper.Commit(tr)
            End If
        Catch ex As Exception
            If Transaction Is Nothing Then
                'We are the creator of the transaction we should commit it  and close the connection
                DBHelper.RollBack(tr)
            End If
            Throw ex
        End Try
    End Sub

    Public Overloads Sub Update(ds As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing, Optional ByVal changesFilter As DataRowState = Nothing)
        If ds Is Nothing Then
            Return
        End If
        If Not ds.Tables(TABLE_NAME) Is Nothing Then
            MyBase.Update(ds.Tables(TABLE_NAME), Transaction, changesFilter)
        End If
    End Sub
#End Region


End Class



