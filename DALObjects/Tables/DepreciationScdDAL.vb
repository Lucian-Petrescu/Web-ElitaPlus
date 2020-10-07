'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (10/2/2017)********************
Public Class DepreciationScdDal
    Inherits DALBase


#Region "Constants"
    Public Const TableName As String = "ELP_DEPRECIATION_SCD"
    Public Const TableKeyName As String = "depreciation_schedule_id"

    Public Const ColNameDepreciationScheduleId As String = "depreciation_schedule_id"
    Public Const ColNameCompanyId As String = "company_id"
    Public Const ColNameCode As String = "code"
    Public Const ColNameDescription As String = "description"
    Public Const ColNameActiveXcd As String = "active_xcd"
    Public Const ColNameActive As String = "active"

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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("depreciation_schedule_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDs, selectStmt, TableName, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadList(companyId As Guid, langId As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_LIST")
        Dim ds As New DataSet
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {
                                                                New DBHelper.DBHelperParameter("langId", langId.ToByteArray),
                                                                New DBHelper.DBHelperParameter(ColNameCompanyId, companyId.ToByteArray)}
        Try
            DBHelper.Fetch(ds, selectStmt, TableName, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
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


