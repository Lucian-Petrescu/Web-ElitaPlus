Public Class SearchConfigAssignmentDal
    Inherits DALBase


#Region "Constants"
    Public Const TableName As String = "ELP_SEARCH_CONFIG_ASSIGNMENT"
    Public Const TableKeyName As String = "search_config_assignment_id"

    Public Const ColNameSearchConfigAssignmentId As String = "search_config_assignment_id"
    Public Const ColNameSearchConfigId As String = "search_config_id"
    Public Const ColNameCompanyId As String = "company_id"
    Public Const ColNameDealerId As String = "dealer_id"

    Public Const ParameterNameLanguageCode As String = "pi_lang_code"
    Public Const ParameterNameCompanyId As String = "pi_company_id"
    Public Const ParameterNameDealerId As String = "pi_dealer_id"
    Public Const ParameterNameSearchType As String = "pi_search_type"


#End Region

#Region "Constructors"
    Public Sub New()
        MyBase.New()
    End Sub

#End Region

#Region "Load Methods"

    Public Sub LoadSchema(ds As DataSet)
        Load(ds, Guid.Empty)
    End Sub

    Public Sub Load(familyDs As DataSet, id As Guid)
        Dim selectStmt As String = Config("/SQL/LOAD")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("search_config_assignment_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDs, selectStmt, TableName, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadList() As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_LIST")
        Return DBHelper.Fetch(selectStmt, TableName)
    End Function

    Public Function LoadSearchCriteriaFields(companyId As Guid, dealerId As Guid, languageCode As String, searchType As String) As DataSet

        Dim selectStmt As String = Config("/SQL/LOAD_SEARCH_CRITERIA_FIELDS")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(ParameterNameCompanyId, companyId.ToByteArray),
                                                                                           New DBHelper.DBHelperParameter(ParameterNameDealerId, If(dealerId.Equals(Guid.Empty), DBNull.Value, dealerId.ToByteArray)),
                                                                                           New DBHelper.DBHelperParameter(ParameterNameLanguageCode, languageCode),
                                                                                           New DBHelper.DBHelperParameter(ParameterNameSearchType, searchType)}
        Dim outputParameter(0) As DBHelper.DBHelperParameter
        outputParameter(0) = New DBHelper.DBHelperParameter("po_search_criteria_fields", GetType(DataSet))
        Dim ds As DataSet = New DataSet(TableName)

        Try
            DBHelper.FetchSp(selectStmt, parameters, outputParameter, ds, TableName)
            ds.Tables(0).TableName = TableName
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

        Return ds
    End Function
#End Region

#Region "Overloaded Methods"
    Public Overloads Sub Update(ds As DataSet, Optional ByVal transaction As IDbTransaction = Nothing, Optional ByVal changesFilter As DataRowState = Nothing)
        If ds Is Nothing Then
            Return
        End If
        If Not ds.Tables(TableName) Is Nothing Then
            Update(ds.Tables(TableName), transaction, changesFilter)
        End If
    End Sub
#End Region


End Class


