'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (4/20/2007)********************


Public Class CommissionEntityDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_COMMISSION_ENTITY"
    Public Const TABLE_KEY_NAME As String = "entity_id"

    Public Const COL_NAME_ENTITY_ID As String = "entity_id"
    Public Const COL_NAME_ENTITY_NAME As String = "entity_name"
    Public Const COL_NAME_PHONE As String = "phone"
    Public Const COL_NAME_EMAIL As String = "email"
    Public Const COL_NAME_PAYMENT_METHOD_ID As String = "payment_method_id"
    Public Const COL_NAME_BANK_INFO_ID As String = "bank_info_id"
    Public Const COL_NAME_COMPANY_GROUP_ID As String = "company_group_id"
    Public Const COL_NAME_ADDRESS1 As String = "address1"
    Public Const COL_NAME_ADDRESS2 As String = "address2"
    Public Const COL_NAME_CITY As String = "city"
    Public Const COL_NAME_REGION_ID As String = "region_id"
    Public Const COL_NAME_POSTAL_CODE As String = "postal_code"
    Public Const COL_NAME_COUNTRY_ID As String = "country_id"
    Public Const COL_NAME_DISPLAY_ID As String = "display_id"
    Public Const COL_NAME_TAX_ID As String = "tax_id"
    Public Const COL_NAME_COMMISSION_ENTITY_TYPE_ID As String = "commission_entity_type_id"

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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("entity_id", id.ToByteArray)}
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

    Public Function LoadList(descriptionMask As String, phoneMask As String, CompanyGroupIdMask As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_LIST")
        Dim whereClauseConditions As String = ""
        Dim inCausecondition As String = ""
        Dim bIsLikeClause As Boolean = False

        bIsLikeClause = IsThereALikeClause(descriptionMask, phoneMask)

        whereClauseConditions &= " WHERE UPPER(" & COL_NAME_COMPANY_GROUP_ID & ") ='" & GuidToSQLString(CompanyGroupIdMask) & "'"

        If ((Not (descriptionMask Is Nothing)) AndAlso (FormatSearchMask(descriptionMask))) Then
            whereClauseConditions &= " AND UPPER(" & COL_NAME_ENTITY_NAME & ")" & descriptionMask.ToUpper
        End If

        If ((Not (phoneMask Is Nothing)) AndAlso (FormatSearchMask(phoneMask))) Then
            whereClauseConditions &= Environment.NewLine & " AND UPPER(" & COL_NAME_PHONE & ")" & phoneMask.ToUpper
        End If

        selectStmt = selectStmt.Replace(DYNAMIC_IN_CLAUSE_PLACE_HOLDER, inCausecondition)

        If Not whereClauseConditions = "" Then
            selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
        Else
            selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, "")
        End If

        selectStmt = selectStmt.Replace(DYNAMIC_ORDER_BY_CLAUSE_PLACE_HOLDER, "ORDER BY " & COL_NAME_ENTITY_NAME & "")
        Try
            'Dim ds = New DataSet
            Return (DBHelper.Fetch(selectStmt, TABLE_NAME))
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try


        Return DBHelper.Fetch(selectStmt, TABLE_NAME)
    End Function

    Private Function IsThereALikeClause(descriptionMask As String, codeMask As String) As Boolean
        Dim bIsLikeClause As Boolean

        bIsLikeClause = IsLikeClause(descriptionMask) OrElse IsLikeClause(codeMask)
        Return bIsLikeClause
    End Function
#End Region

#Region "Overloaded Methods"

    'This method was added manually to accommodate BO families Save
    Public Overloads Sub UpdateFamily(familyDataset As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing, Optional ByVal blnBankInfoSave As Boolean = False, Optional ByVal isBankInfoNeedDeletion As Boolean = False)
        Dim BankInfoDAL As New BankInfoDAL

        Dim tr As IDbTransaction = Transaction
        If tr Is Nothing Then
            tr = DBHelper.GetNewTransaction
        End If
        Try
            'First Pass updates Deletions
            MyBase.Update(familyDataset.Tables(TABLE_NAME).GetChanges(DataRowState.Deleted), tr, DataRowState.Deleted)

            'Second Pass updates additions and changes
            If blnBankInfoSave Then
                BankInfoDAL.Update(familyDataset.GetChanges(DataRowState.Added Or DataRowState.Modified), tr, DataRowState.Added Or DataRowState.Modified)
            End If
            Update(familyDataset.Tables(TABLE_NAME).GetChanges(DataRowState.Added Or DataRowState.Modified), tr, DataRowState.Added Or DataRowState.Modified)

            BankInfoDAL.Update(familyDataset.GetChanges(DataRowState.Deleted), tr, DataRowState.Deleted)

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



