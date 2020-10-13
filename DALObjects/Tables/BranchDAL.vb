'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (3/19/2007)********************


Public Class BranchDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_BRANCH"
    Public Const TABLE_KEY_NAME As String = "branch_id"

    Public Const COL_NAME_BRANCH_ID As String = "branch_id"
    Public Const COL_NAME_BRANCH_CODE As String = "branch_code"
    Public Const COL_NAME_DEALER_ID As String = "dealer_id"
    Public Const COL_NAME_ADDRESS1 As String = "address1"
    Public Const COL_NAME_ADDRESS2 As String = "address2"
    'Added for Def - 1574
    Public Const COL_NAME_ADDRESS3 As String = "address3"
    Public Const COL_NAME_CITY As String = "city"
    Public Const COL_NAME_REGION_ID As String = "region_id"
    Public Const COL_NAME_COUNTRY_ID As String = "country_id"
    Public Const COL_NAME_BRANCH_NAME As String = "branch_name"
    Public Const COL_NAME_POSTAL_CODE As String = "postal_code"
    Public Const COL_NAME_CONTACT_PHONE As String = "contact_phone"
    Public Const COL_NAME_CONTACT_EXT As String = "contact_ext"
    Public Const COL_NAME_CONTACT_FAX As String = "contact_fax"
    Public Const COL_NAME_CONTACT_EMAIL As String = "contact_email"
    Public Const COL_NAME_BANK_INFO_ID As String = "bank_info_id"
    Public Const COL_NAME_MARKET As String = "market"
    Public Const COL_NAME_BRANCH_TYPE_ID As String = "branch_type_id"
    Public Const COL_NAME_STORE_MANAGER As String = "store_manager"
    Public Const COL_NAME_MARKETING_REGION As String = "marketing_region"
    Public Const COL_NAME_OPEN_DATE As String = "open_date"
    Public Const COL_NAME_CLOSE_DATE As String = "close_date"

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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("branch_id", id.ToByteArray)}
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

    Public Function LoadList(descriptionMask As String, codeMask As String, DealerMask As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_LIST")
        Dim whereClauseConditions As String = ""
        Dim inCausecondition As String = ""
        Dim bIsLikeClause As Boolean = False

        bIsLikeClause = IsThereALikeClause(descriptionMask, codeMask)

        whereClauseConditions &= " WHERE UPPER(" & COL_NAME_DEALER_ID & ") ='" & GuidToSQLString(DealerMask) & "'"

        If ((Not (descriptionMask Is Nothing)) AndAlso (FormatSearchMask(descriptionMask))) Then
            whereClauseConditions &= " AND UPPER(" & COL_NAME_BRANCH_NAME & ")" & descriptionMask.ToUpper
        End If

        If ((Not (codeMask Is Nothing)) AndAlso (FormatSearchMask(codeMask))) Then
            whereClauseConditions &= Environment.NewLine & " AND UPPER(" & COL_NAME_BRANCH_CODE & ")" & codeMask.ToUpper
        End If

        selectStmt = selectStmt.Replace(DYNAMIC_IN_CLAUSE_PLACE_HOLDER, inCausecondition)

        If Not whereClauseConditions = "" Then
            selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
        Else
            selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, "")
        End If

        selectStmt = selectStmt.Replace(DYNAMIC_ORDER_BY_CLAUSE_PLACE_HOLDER, "ORDER BY " & COL_NAME_BRANCH_NAME & ", " & COL_NAME_BRANCH_CODE)
        Try
            'Dim ds = New DataSet
            Return (DBHelper.Fetch(selectStmt, TABLE_NAME))
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try


        Return DBHelper.Fetch(selectStmt, TABLE_NAME)
    End Function

    Public Function LoadListByDealer(DealerId As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_LIST_BY_DEALER")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("dealer_id", DealerId.ToByteArray)}
        Try
            Dim ds = New DataSet
            DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function
    Public Function LoadListFromBranchStandardizationByDealerForWS(DealerId As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_LIST_BY_DEALER_FOR_WS_BS_TABLE")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("dealer_id", DealerId.ToByteArray)}
        Try
            Dim ds = New DataSet
            DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function
    '
    Private Function IsThereALikeClause(descriptionMask As String, codeMask As String) As Boolean
        Dim bIsLikeClause As Boolean

        bIsLikeClause = IsLikeClause(descriptionMask) OrElse IsLikeClause(codeMask)
        Return bIsLikeClause
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



