'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (10/13/2004)********************


Public Class PriceGroupDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_PRICE_GROUP"
    Public Const TABLE_KEY_NAME As String = "price_group_id"

    Public Const COL_NAME_PRICE_GROUP_ID As String = "price_group_id"
    Public Const COL_NAME_COMPANY_ID_NotUsed As String = "company_id"
    Public Const COL_NAME_COUNTRY_ID As String = "country_id"
    Public Const COL_NAME_SHORT_DESC As String = "short_desc"
    Public Const COL_NAME_DESCRIPTION As String = "description"
    Public Const COL_NAME_COUNTRY_DESC As String = "country_description"

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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("price_group_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadList(oCountryIds As ArrayList, searchCode As String, searchDesc As String) As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_LIST")
        Dim whereClauseConditions As String = ""

        whereClauseConditions &= Environment.NewLine & " AND " & MiscUtil.BuildListForSql("c." & COL_NAME_COUNTRY_ID, oCountryIds, False)
        If FormatSearchMask(searchCode) Then
            whereClauseConditions &= " AND " & Environment.NewLine & "UPPER(pg." & COL_NAME_SHORT_DESC & ") " & searchCode.ToUpper
        End If
        If FormatSearchMask(searchDesc) Then
            whereClauseConditions &= " AND " & Environment.NewLine & "UPPER(pg." & COL_NAME_DESCRIPTION & ") " & searchDesc.ToUpper
        End If

        If Not whereClauseConditions = "" Then
            selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
        Else
            selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, "")
        End If

        Try
            Dim ds As New DataSet
            ds = DBHelper.Fetch(selectStmt, TABLE_NAME)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function


#End Region

#Region "Overloaded Methods"
    'This method was added manually to accommodate BO families Save
    Public Overloads Sub UpdateFamily(familyDataset As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing)
        Dim detailDAL As New PriceGroupDetailDAL
        Dim tr As IDbTransaction = Transaction
        If tr Is Nothing Then
            tr = DBHelper.GetNewTransaction
        End If
        Try
            'First Pass updates Deletions
            detailDAL.Update(familyDataset, tr, DataRowState.Deleted)
            MyBase.Update(familyDataset.Tables(TABLE_NAME), tr, DataRowState.Deleted)

            'Second Pass updates additions and changes
            Update(familyDataset.Tables(TABLE_NAME), tr, DataRowState.Added Or DataRowState.Modified)
            detailDAL.Update(familyDataset, tr, DataRowState.Added Or DataRowState.Modified)

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


