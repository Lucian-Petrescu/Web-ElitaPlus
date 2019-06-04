'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (7/21/2014)********************


Public Class AcctEventDetailIncexcDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_ACCT_EVENT_DETAIL_INCEXC"
    Public Const TABLE_KEY_NAME As String = "acct_event_detail_incexc_id"

    Public Const COL_NAME_ACCT_EVENT_DETAIL_INCEXC_ID As String = "acct_event_detail_incexc_id"
    Public Const COL_NAME_ACCT_EVENT_DETAIL_ID As String = "acct_event_detail_id"
    Public Const COL_NAME_DEALER_ID As String = "dealer_id"
    Public Const COL_NAME_COVERAGE_TYPE_ID As String = "coverage_type_id"

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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("acct_event_detail_incexc_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, Me.TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadListByAcctEventDetailID(ByVal AcctEventDetailID As Guid) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("acct_event_detail_id", AcctEventDetailID.ToByteArray)}
        Dim ds As New DataSet
        DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)
        Return ds
    End Function

    Public Function LoadDealerListByAcctEventDetailID(ByVal AcctEventID As Guid, ByVal userID As Guid) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/LOAD_DEALER_LIST_BY_LINE_ITEM")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("acct_event_id", AcctEventID.ToByteArray) _
                                                                                          , New DBHelper.DBHelperParameter("user_id", userID.ToByteArray)}
        Dim ds As New DataSet
        DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)
        Return ds
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


