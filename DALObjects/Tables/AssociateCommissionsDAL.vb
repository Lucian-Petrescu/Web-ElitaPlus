'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (9/10/2007)********************


Public Class AssociateCommissionsDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_ASSOCIATE_COMMISSIONS"
    Public Const TABLE_KEY_NAME As String = "associate_commissions_id"

    Public Const COL_NAME_ASSOCIATE_COMMISSIONS_ID As String = "associate_commissions_id"
    Public Const COL_NAME_COMMISSION_TOLERANCE_ID As String = "commission_tolerance_id"
    Public Const COL_NAME_MARKUP_PERCENT As String = "markup_percent"
    Public Const COL_NAME_COMMISSION_PERCENT As String = "commission_percent"
    Public Const COL_NAME_POSITION As String = "position"
    Public Const COL_NAME_ENTITY_NAME As String = "entity_name"
    Public Const COL_NAME_PAYEE_TYPE_ID As String = "payee_type_id"

    Private Const DSNAME As String = "LIST"

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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("associate_commissions_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, Me.TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    'Public Function LoadList() As DataSet
    '    Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST")
    '    Return DBHelper.Fetch(selectStmt, Me.TABLE_NAME)
    'End Function

    Public Function LoadList(ByVal toleranceId As Guid) As DataSet
        Dim ds As DataSet
        Dim parameters() As OracleParameter
        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST")
        parameters = New OracleParameter() {New OracleParameter(Me.COL_NAME_COMMISSION_TOLERANCE_ID, toleranceId.ToByteArray)}
        'DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)
        Return (DBHelper.Fetch(selectStmt, DSNAME, Me.TABLE_NAME, parameters))
    End Function

    Public Sub LoadList(ByVal toleranceId As Guid, ByVal familyDataset As DataSet)
        Dim parameters() As OracleParameter
        Dim selectStmt As String = Me.Config("/SQL/LOAD_ALL_TOLERANCES_ASSOC_COMM")
        parameters = New OracleParameter() {New OracleParameter(Me.COL_NAME_COMMISSION_TOLERANCE_ID, toleranceId.ToByteArray)}
        DBHelper.Fetch(familyDataset, selectStmt, Me.TABLE_NAME, parameters)
    End Sub

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






