'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (8/17/2007)********************
Imports System.Globalization

Public Class RegionTaxDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_REGION_TAX"
    Public Const TABLE_KEY_NAME As String = "region_tax_id"

    Public Const COL_NAME_REGION_TAX_ID As String = "region_tax_id"
    Public Const COL_NAME_REGION_ID As String = "region_id"
    Public Const COL_NAME_TAX_TYPE_ID As String = "tax_type_id"
    Public Const COL_NAME_EFFECTIVE_DATE As String = "effective_date"
    Public Const COL_NAME_EXPIRATION_DATE As String = "expiration_date"
    Public Const COL_NAME_DESCRIPTION As String = "description"
    Public Const COL_NAME_PRODUCT_TAX_TYPE_ID As String = "product_tax_type_id"
    Public Const COL_NAME_COMPANY_TYPE_XCD As String = "company_type_xcd"


    Private Const COL_NAME_RECORDCOUNT As String = "RECORD_COUNT"

    'REQ-1150
    Public Const COL_NAME_DEALER_ID As String = "dealer_id"

    Private Const INFINITE_DATE_CONS As String = "12/31/2999"
    Public Shared ReadOnly INFINITE_DATE As Date = Date.Parse(INFINITE_DATE_CONS, CultureInfo.InvariantCulture)
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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("region_tax_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadList(RegionId As Guid, TaxTypeID As Guid, oProductTaxTypeId As Guid, DealerId As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_LIST")

        Dim DealerIds As New ArrayList()
        DealerIds.Add(DealerId)

        Dim whereClauseConditions As String = ""
        If Not DealerId = Guid.Empty Then
            whereClauseConditions &= Environment.NewLine & " AND " & MiscUtil.BuildListForSql(COL_NAME_DEALER_ID, DealerIds, False)
        Else
            whereClauseConditions &= Environment.NewLine & " AND " & COL_NAME_DEALER_ID & " IS NULL"
        End If

        If Not whereClauseConditions = "" Then
            selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
        Else
            selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, "")
        End If

        Dim parameters As DBHelper.DBHelperParameter()

        parameters = New DBHelper.DBHelperParameter() { _
            New DBHelper.DBHelperParameter("REGION_ID", RegionId.ToByteArray), _
            New DBHelper.DBHelperParameter("TAX_TYPE_ID", TaxTypeID.ToByteArray), _
            New DBHelper.DBHelperParameter("PRODUCT_TAX_TYPE_ID", oProductTaxTypeId.ToByteArray)}

        Dim ds As New DataSet

        Try
            DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Sub LoadMinEffDateMaxExpDate(ByRef MinEffDate As Date, ByRef MaxExpDate As Date, ByRef RcdCount As Integer, _
                                            RegionId As Guid, TaxTypeId As Guid, oProductTaxTypeId As Guid, oDealerId As Guid)
        RcdCount = 0

        Dim selectStmt As String
        selectStmt = Config("/SQL/LOAD_MINEFFECTIVE_MAXEXPIRATION_DATES")

        Dim DealerIds As New ArrayList()
        DealerIds.Add(oDealerId)

        Dim whereClauseConditions As String = ""
        If Not oDealerId = Guid.Empty Then
            whereClauseConditions &= Environment.NewLine & " AND " & MiscUtil.BuildListForSql(COL_NAME_DEALER_ID, DealerIds, False)
        Else
            whereClauseConditions &= Environment.NewLine & " AND " & COL_NAME_DEALER_ID & " IS NULL"
        End If

        If Not whereClauseConditions = "" Then
            selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
        Else
            selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, "")
        End If

        Dim parameters As DBHelper.DBHelperParameter()

        parameters = New DBHelper.DBHelperParameter() { _
            New DBHelper.DBHelperParameter("COUNTRY_ID", RegionId.ToByteArray), _
            New DBHelper.DBHelperParameter("TAX_TYPE_ID", TaxTypeId.ToByteArray), _
            New DBHelper.DBHelperParameter("PRODUCT_TAX_TYPE_ID", oProductTaxTypeId.ToByteArray)}

        Dim ds As New DataSet
        Try
            DBHelper.Fetch(ds, selectStmt, "DateDetails", parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

        Dim row As DataRow

        If ds.Tables(0).Rows.Count > 0 Then
            row = ds.Tables(0).Rows(0)
            RcdCount = row.Item(COL_NAME_RECORDCOUNT)
            If RcdCount > 0 Then
                If Not row.Item(COL_NAME_EFFECTIVE_DATE) Is DBNull.Value Then
                    MinEffDate = CType(row.Item(COL_NAME_EFFECTIVE_DATE), Date)
                End If
                If Not row.Item(COL_NAME_EXPIRATION_DATE) Is DBNull.Value Then
                    MaxExpDate = CType(row.Item(COL_NAME_EXPIRATION_DATE), Date)
                End If
            Else 'no existing record
                MinEffDate = Date.Now
                MaxExpDate = INFINITE_DATE
            End If
        End If
    End Sub

#End Region

#Region "Overloaded Methods"
    Public Overloads Sub Update(ds As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing, Optional ByVal changesFilter As DataRowState = Nothing)
        If ds Is Nothing Then
            Return
        End If
        If Not ds.Tables(TABLE_NAME) Is Nothing Then
            MyBase.Update(ds.Tables(TABLE_NAME), Transaction, changesFilter)
        End If
    End Sub

    Public Overloads Sub UpdateFamily(familyDataset As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing)

        'Dim addressDAL As New addressDAL
        Dim regionTaxDetailDAL As New RegionTaxDetailDAL

        Dim tr As IDbTransaction = Transaction
        If tr Is Nothing Then
            tr = DBHelper.GetNewTransaction
        End If
        Try
            'First Pass updates Deletions, delete children first then the parent
            regionTaxDetailDAL.Update(familyDataset.GetChanges(DataRowState.Deleted), tr, DataRowState.Deleted)
            MyBase.Update(familyDataset.Tables(TABLE_NAME).GetChanges(DataRowState.Deleted), tr, DataRowState.Deleted)

            'Second Pass updates additions and changes
            Update(familyDataset.Tables(TABLE_NAME).GetChanges(DataRowState.Added Or DataRowState.Modified), tr, DataRowState.Added Or DataRowState.Modified)
            regionTaxDetailDAL.Update(familyDataset.GetChanges(DataRowState.Added Or DataRowState.Modified), tr, DataRowState.Added Or DataRowState.Modified)

            If Transaction Is Nothing Then
                'We are the creator of the transaction we shoul commit it  and close the connection
                DBHelper.Commit(tr)
                familyDataset.AcceptChanges()
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


