'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (2/28/2012)********************


Public Class ServiceLevelGroupDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_SERVICE_LEVEL_GROUP"
    Public Const TABLE_KEY_NAME As String = "service_level_group_id"

    Public Const COL_NAME_SERVICE_LEVEL_GROUP_ID As String = "service_level_group_id"
    Public Const COL_NAME_CODE As String = "code"
    Public Const COL_NAME_DESCRIPTION As String = "description"
    Public Const COL_NAME_COUNTRY_ID As String = "country_id"
    Public Const COL_NAME_COUNTRY_DESC As String = "country_desc"

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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("service_level_group_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, Me.TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadList(ByVal oCountryIds As ArrayList, ByVal searchCode As String, ByVal searchDesc As String, ByVal fromDate As String, ByVal toDate As String) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST")

        Dim whereClauseConditions As String = ""
        whereClauseConditions &= Environment.NewLine & " AND " & MiscUtil.BuildListForSql("c." & Me.COL_NAME_COUNTRY_ID, oCountryIds, False)
        If Me.FormatSearchMask(searchCode) Then
            whereClauseConditions &= " AND " & Environment.NewLine & "UPPER(slg." & Me.COL_NAME_CODE & ") " & searchCode.ToUpper
        End If
        If Me.FormatSearchMask(searchDesc) Then
            whereClauseConditions &= " AND " & Environment.NewLine & "UPPER(slg." & Me.COL_NAME_DESCRIPTION & ") " & searchDesc.ToUpper
        End If

        If ((Not fromDate Is Nothing) AndAlso (Not fromDate.Equals(String.Empty))) And ((Not fromDate Is Nothing) AndAlso (Not toDate.Equals(String.Empty))) Then
            whereClauseConditions &= Environment.NewLine & "and sld.effective_date between to_Date('" & fromDate & "', 'YYYYMMDD') and  to_Date('" & toDate & "', 'YYYYMMDD')"
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



