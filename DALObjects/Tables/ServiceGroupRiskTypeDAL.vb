'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (9/27/2004)********************


Public Class ServiceGroupRiskTypeDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_SERVICE_GROUP_RISK_TYPE"
    Public Const TABLE_KEY_NAME As String = "service_group_risk_type_id"

    Public Const COL_NAME_SERVICE_GROUP_RISK_TYPE_ID As String = "service_group_risk_type_id"
    Public Const COL_NAME_SERVICE_GROUP_ID As String = "service_group_id"
    Public Const COL_NAME_RISK_TYPE_ID As String = "risk_type_id"
    Public Const COL_NAME_RISK_TYPE_DESCRIPTION As String = "risk_type_description"

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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("service_group_risk_type_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, Me.TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Sub LoadList(ByVal ds As DataSet, ByVal servGrpId As Guid)
        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST")
        Dim serviceGroupParam As New DBHelper.DBHelperParameter("service_group_id", servGrpId.ToByteArray)
        DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, New DBHelper.DBHelperParameter() {serviceGroupParam})
    End Sub


#End Region

#Region "CRUD METHODS"
    Public Overloads Sub Update(ByVal ds As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing, Optional ByVal changesFilter As DataRowState = Nothing)
        If Not ds.Tables.IndexOf(Me.TABLE_NAME) < 0 Then
            MyBase.Update(ds.Tables(Me.TABLE_NAME), Transaction, changesFilter)
        End If
    End Sub
#End Region



End Class


