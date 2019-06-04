'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (3/26/2007)********************


Public Class PoliceReportDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_POLICE_REPORT"
    Public Const TABLE_KEY_NAME As String = "police_report_id"

    Public Const COL_NAME_POLICE_REPORT_ID As String = "police_report_id"
    Public Const COL_NAME_POLICE_STATION_ID As String = "police_station_id"
    Public Const COL_NAME_CLAIM_ID As String = "claim_id"
    Public Const COL_NAME_REPORT_NUMBER As String = "report_number"
    Public Const COL_NAME_OFFICER_NAME As String = "officer_name"

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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("police_report_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, Me.TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub
    Public Sub LoadbyClaimid(ByVal familyDS As DataSet, ByVal claimid As Guid)
        Dim selectStmt As String = Me.Config("/SQL/LOADBYCLAIMID")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("claim_id", claimid.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, Me.TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadList() As DataSet
        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST")
        Return DBHelper.Fetch(selectStmt, Me.TABLE_NAME)
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

#Region "Public methods"
    Public Function GetClaimsByPoliceRptNumber(ByVal PoliceStationID As Guid, ByVal ReportNumber As String) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/LOADCLAIMSBYREPORTNUM")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() { _
                                        New DBHelper.DBHelperParameter(COL_NAME_POLICE_STATION_ID, PoliceStationID.ToByteArray), _
                                        New DBHelper.DBHelperParameter(COL_NAME_REPORT_NUMBER, ReportNumber.Trim.ToUpper)}
        Try
            Dim ds As New DataSet
            DBHelper.Fetch(ds, selectStmt, "Claims", parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function
#End Region
End Class


