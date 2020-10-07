Public Class SearchConfigCriteriaDal
    Inherits DALBase


#Region "Constants"
    Public Const TableName As String = "ELP_SEARCH_CONFIG_CRITERIA"
    Public Const TableKeyName As String = "search_config_criteria_id"
	
    Public Const ColNameSearchConfigCriteriaId As String = "search_config_criteria_id"
    Public Const ColNameSearchConfigId As String = "search_config_id"
    Public Const ColNameFieldNameXcd As String = "field_name_xcd"
    Public Const ColNameSequenceNumber As String = "sequence_number"

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

    Public Sub Load(familyDs As DataSet, id As Guid)
        Dim selectStmt As String = Config("/SQL/LOAD")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("search_config_criteria_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, TableName, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadList() As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_LIST")
        Return DBHelper.Fetch(selectStmt, TableName)
    End Function    
    

#End Region

#Region "Overloaded Methods"
    Public Overloads Sub Update(ds As DataSet, Optional ByVal transaction As IDbTransaction = Nothing, Optional ByVal changesFilter As DataRowState = Nothing)
        If ds Is Nothing Then
            Return
        End If
        If Not ds.Tables(TableName) Is Nothing Then
            Update(ds.Tables(TableName), Transaction, changesFilter)
        End If
    End Sub
#End Region


End Class


