Public Class SearchConfigDal
    Inherits DALBase


#Region "Constants"
    Public Const TableName As String = "ELP_SEARCH_CONFIG"
    Public Const TableKeyName As String = "search_config_id"
	
    Public Const ColNameSearchConfigId As String = "search_config_id"
    Public Const ColNameCode As String = "code"
    Public Const ColNameDescription As String = "description"
    Public Const ColNameSearchTypeXcd As String = "search_type_xcd"

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

    Public Sub Load(ByVal familyDs As DataSet, ByVal id As Guid)
        Dim selectStmt As String = Config("/SQL/LOAD")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("search_config_id", id.ToByteArray)}
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
    Public Overloads Sub Update(ByVal ds As DataSet, Optional ByVal transaction As IDbTransaction = Nothing, Optional ByVal changesFilter As DataRowState = Nothing)
        If ds Is Nothing Then
            Return
        End If
        If Not ds.Tables(TableName) Is Nothing Then
            Update(ds.Tables(TableName), Transaction, changesFilter)
        End If
    End Sub
#End Region


End Class


