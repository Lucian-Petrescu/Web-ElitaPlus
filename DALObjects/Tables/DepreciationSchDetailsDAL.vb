Public Class DepreciationSchDetailsDal
    Inherits DALBase

#Region "Constants"
    Public Const HeaderInfo As String = "HEADER_INFO"
    Public Const TableName As String = "ELP_DEPRECIATION_SCH_DETAILS"
    Public Const TableKeyName As String = "depreciation_schedule_item_id"

    Public Const ColNameDepreciationScheduleItemId As String = "depreciation_schedule_item_id"
    Public Const ColNameDepreciationScheduleId As String = "depreciation_schedule_id"
    Public Const ColNameLowMonth As String = "low_month"
    Public Const ColNameHighMonth As String = "high_month"
    Public Const ColNamePercent As String = "percent"
    Public Const ColNameAmount As String = "amount"
    Public Const ColNameEffective As String = "effective"
    Public Const ColNameExpiration As String = "expiration"

    Private Const Dsname As String = "LIST"

#End Region

#Region "Constructors"
    Public Sub New()
        MyBase.New()
    End Sub

#End Region

#Region "Load Methods"

    Public Sub LoadSchema(ByVal ds As DataSet)
        Load(ds, Guid.Empty)
    End Sub

    Public Sub Load(ByVal familyDs As DataSet, ByVal id As Guid)
        Dim selectStmt As String = Config("/SQL/LOAD")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(ColNameDepreciationScheduleItemId, id.ToByteArray)}
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

    Public Function LoadList(ByVal depreciationScheduleId As Guid, ByVal languageId As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_LIST")
        Dim parameters() As OracleParameter

        parameters = New OracleParameter() _
                                    {New OracleParameter(ColNameDepreciationScheduleId, depreciationScheduleId.ToByteArray)}

        Try
            Return (DBHelper.Fetch(selectStmt, Dsname, TableName, parameters))
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function
#End Region

#Region "Overloaded Methods"
    Public Overloads Sub Update(ByVal ds As DataSet, Optional ByVal transaction As IDbTransaction = Nothing, Optional ByVal changesFilter As DataRowState = Nothing)
        If ds Is Nothing Then
            Return
        End If
        If Not ds.Tables(TableName) Is Nothing Then
            Update(ds.Tables(TableName), transaction, changesFilter)
        End If
    End Sub
#End Region


End Class


