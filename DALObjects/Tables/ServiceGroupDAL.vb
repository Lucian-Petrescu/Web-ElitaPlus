'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (9/20/2004)********************


Public Class ServiceGroupDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_SERVICE_GROUP"
    Public Const TABLE_KEY_NAME As String = "service_group_id"
    Public Const COL_NAME_SERVICE_GROUP_ID = "service_group_id"
    Public Const COL_NAME_COUNTRY_ID = "country_id"
    Public Const COL_NAME_COUNTRY_DESC As String = "country_description"
    Public Const COL_NAME_SHORT_DESC = "short_desc"
    Public Const COL_NAME_DESCRIPTION = "description"
    Public Const WILDCARD As Char = "%"

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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("service_group_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, Me.TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadList(ByVal oCountryIds As ArrayList, ByVal searchCode As String, ByVal searchDesc As String) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST")

        Dim whereClauseConditions As String = ""
        whereClauseConditions &= Environment.NewLine & " AND " & MiscUtil.BuildListForSql("c." & Me.COL_NAME_COUNTRY_ID, oCountryIds, False)
        If Me.FormatSearchMask(searchCode) Then
            whereClauseConditions &= " AND " & Environment.NewLine & "UPPER(sg." & Me.COL_NAME_SHORT_DESC & ") " & searchCode.ToUpper
        End If
        If Me.FormatSearchMask(searchDesc) Then
            whereClauseConditions &= " AND " & Environment.NewLine & "UPPER(sg." & Me.COL_NAME_DESCRIPTION & ") " & searchDesc.ToUpper
        End If
        If Not whereClauseConditions = "" Then
            selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
        Else
            selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, "")
        End If

        Try
            Dim ds As New DataSet
            ds = DBHelper.Fetch(selectStmt, Me.TABLE_NAME)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function


#End Region


#Region "Overloaded Methods"

    'This method was added manually to accommodate BO families Save
    Public Overloads Sub UpdateFamily(ByVal familyDataset As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing)
        Dim ServGrpRiskTypeDal As New ServiceGroupRiskTypeDAL
        Dim ServGrpRiskTypeManufacturerDal As New SgRtManufacturerDAL
        Dim tr As IDbTransaction = Transaction
        If tr Is Nothing Then
            tr = DBHelper.GetNewTransaction
        End If
        Try
            'First Pass updates Deletions
            ServGrpRiskTypeManufacturerDal.Update(familyDataset, tr, DataRowState.Deleted)
            ServGrpRiskTypeDal.Update(familyDataset, tr, DataRowState.Deleted)
            MyBase.Update(familyDataset.Tables(Me.TABLE_NAME), tr, DataRowState.Deleted)

            'Second Pass updates additions and changes
            Update(familyDataset.Tables(Me.TABLE_NAME), tr, DataRowState.Added Or DataRowState.Modified)
            ServGrpRiskTypeDal.Update(familyDataset, tr, DataRowState.Added Or DataRowState.Modified)
            ServGrpRiskTypeManufacturerDal.Update(familyDataset, tr, DataRowState.Added Or DataRowState.Modified)
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

    Public Overloads Sub Update(ByVal ds As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing, Optional ByVal changesFilter As DataRowState = Nothing)
        If Not ds.Tables(Me.TABLE_NAME) Is Nothing Then
            MyBase.Update(ds.Tables(Me.TABLE_NAME), Transaction, changesFilter)
        End If
    End Sub
#End Region

End Class



