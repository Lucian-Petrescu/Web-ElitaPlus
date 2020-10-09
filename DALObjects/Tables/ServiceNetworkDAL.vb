'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (8/15/2006)********************

'Namespace Table
Public Class ServiceNetworkDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_SERVICE_NETWORK"
    Public Const TABLE_KEY_NAME As String = "service_network_id"

    Public Const COL_NAME_SERVICE_NETWORK_ID As String = "service_network_id"
    Public Const COL_NAME_COMPANY_GROUP_ID As String = "company_group_id"
    Public Const COL_NAME_SHORT_DESC As String = "short_desc"
    Public Const COL_NAME_DESCRIPTION As String = "description"

    Public Const COL_NAME_COUNTRY_ID As String = "country_id"
    Public Const DSNAME As String = "LIST"
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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("service_network_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadList(CompanyGroupId As Guid, codeMask As String, descriptionMask As String) As DataSet

        'Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST")
        Dim parameters() As OracleParameter

        'descriptionMask = GetFormattedSearchStringForSQL(descriptionMask)
        'codeMask = GetFormattedSearchStringForSQL(codeMask)
        parameters = New OracleParameter() _
                                    {New OracleParameter(COL_NAME_COMPANY_GROUP_ID, CompanyGroupId.ToByteArray)}
        '                             New OracleParameter(COL_NAME_SHORT_DESC, codeMask), _
        '                             New OracleParameter(COL_NAME_DESCRIPTION, descriptionMask)}

        'Try
        '    Return (DBHelper.Fetch(selectStmt, DSNAME, Me.TABLE_NAME, parameters))
        'Catch ex As Exception
        '    Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        'End Try


        Dim selectStmt As String = Config("/SQL/LOAD_LIST")

        Dim whereClauseConditions As String = ""

        'whereClauseConditions &= Environment.NewLine & " AND " & MiscUtil.BuildListForSql("c." & Me.COL_NAME_COMPANY_GROUP_ID, CompanyGroupId, False)
        If FormatSearchMask(codeMask) Then
            whereClauseConditions &= " AND " & Environment.NewLine & COL_NAME_SHORT_DESC & codeMask.ToUpper
        End If
        If FormatSearchMask(descriptionMask) Then
            whereClauseConditions &= " AND " & Environment.NewLine & COL_NAME_DESCRIPTION & descriptionMask.ToUpper
        End If

        If Not whereClauseConditions = "" Then
            selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
        Else
            selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, "")
        End If

        Try
            Dim ds As New DataSet
            ds = DBHelper.Fetch(selectStmt, DSNAME, TABLE_NAME, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Function LoadList() As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_LIST")
        Return DBHelper.Fetch(selectStmt, TABLE_NAME)
    End Function

    Private Function IsThereALikeClause(descriptionMask As String, codeMask As String) As Boolean
        Dim bIsLikeClause As Boolean

        bIsLikeClause = IsLikeClause(descriptionMask) OrElse IsLikeClause(codeMask)
        Return bIsLikeClause
    End Function

    Public Function LoadServiceCenter(oCountryIds As ArrayList) As DataSet

        Dim selectStmt As String = Config("/SQL/LOAD_SERVICE_CENTERS")
        Dim parameters() As OracleParameter

        Dim whereClauseConditions As String = ""

        whereClauseConditions &= MiscUtil.BuildListForSql("sc." & COL_NAME_COUNTRY_ID, oCountryIds, False)
        selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
        'parameters = New OracleParameter() _
        '                            {New OracleParameter(COL_NAME_COUNTRY_ID, oCountryIds)}
        Try
            Return (DBHelper.Fetch(selectStmt, DSNAME))
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

#End Region

#Region "Overloaded Methods"
    Public Overloads Sub UpdateFamily(familyDataset As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing)
        Dim svcNetSrvCntl As New ServiceNetworkSvcDAL
        Dim srvcenterDal As New ServiceCenterDAL

        Dim tr As IDbTransaction = Transaction
        If tr Is Nothing Then
            tr = DBHelper.GetNewTransaction
        End If
        Try
            'First Pass updates Deletions
            svcNetSrvCntl.Update(familyDataset, tr, DataRowState.Deleted)

            MyBase.Update(familyDataset.Tables(TABLE_NAME), tr, DataRowState.Deleted)

            'Second Pass updates additions and changes
            Update(familyDataset.Tables(TABLE_NAME), tr, DataRowState.Added Or DataRowState.Modified)

            svcNetSrvCntl.Update(familyDataset, tr, DataRowState.Added Or DataRowState.Modified)
            srvcenterDal.Update(familyDataset, tr, DataRowState.Added Or DataRowState.Modified)

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
        If ds.Tables(TABLE_NAME) IsNot Nothing Then
            MyBase.Update(ds.Tables(TABLE_NAME), Transaction, changesFilter)
        End If
    End Sub
#End Region


End Class

'End Namespace

