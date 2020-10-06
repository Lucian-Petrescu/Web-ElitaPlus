'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (10/22/2004)********************


Public Class ServiceCenterDealerDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_SERVICE_CENTER_DEALER"
    Public Const TABLE_KEY_NAME As String = "service_center_dealer_id"

    Public Const COL_NAME_SERVICE_CENTER_DEALER_ID As String = "service_center_dealer_id"
    Public Const COL_NAME_SERVICE_CENTER_ID As String = "service_center_id"
    Public Const COL_NAME_DEALER_ID As String = "dealer_id"
    Public Const COL_NAME_PREFERRED_DEALER_FLAG As String = "preferred_dealer_flag"

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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("service_center_dealer_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    'Public Function LoadList() As DataSet
    '    Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST")
    '    Return DBHelper.Fetch(selectStmt, Me.TABLE_NAME)
    'End Function

    'This Method's body was added manually
    Public Sub LoadList(ds As DataSet, serviceCenterDealerId As Guid)
        Dim selectStmt As String = Config("/SQL/LOAD_LIST")
        Dim serviceCenterDealerParam As New DBHelper.DBHelperParameter("service_center_dealer_id", serviceCenterDealerId.ToByteArray)
        DBHelper.Fetch(ds, selectStmt, TABLE_NAME, New DBHelper.DBHelperParameter() {serviceCenterDealerParam})
    End Sub

#End Region

#Region "CRUD Methods"
    Public Overloads Sub Update(ds As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing, Optional ByVal changesFilter As DataRowState = Nothing)
        If ds Is Nothing Then
            Return
        End If
        If Not ds.Tables.IndexOf(TABLE_NAME) < 0 Then
            MyBase.Update(ds.Tables(TABLE_NAME), Transaction, changesFilter)
        End If
    End Sub
#End Region


End Class



