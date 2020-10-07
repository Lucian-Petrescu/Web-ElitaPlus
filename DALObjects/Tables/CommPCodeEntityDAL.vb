'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (2/1/2010)********************


Public Class CommPCodeEntityDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_COMM_P_CODE_ENTITY"
    Public Const TABLE_KEY_NAME As String = "comm_p_code_entity_id"

    Public Const COL_NAME_COMM_P_CODE_ENTITY_ID As String = "comm_p_code_entity_id"
    Public Const COL_NAME_COMM_P_CODE_ID As String = "comm_p_code_id"
    Public Const COL_NAME_PAYEE_TYPE_ID As String = "payee_type_id"
    Public Const COL_NAME_ENTITY_ID As String = "entity_id"
    Public Const COL_NAME_IS_COMM_FIXED_ID As String = "is_comm_fixed_id"
    Public Const COL_NAME_IS_COMM_FIXED_CODE As String = "is_comm_fixed_code"
    Public Const COL_NAME_COMMISSION_AMOUNT As String = "commission_amount"
    Public Const COL_NAME_MARKUP_AMOUNT As String = "markup_amount"
    Public Const COL_NAME_COMM_SCHEDULE_ID As String = "comm_schedule_id"
    Public Const COL_NAME_LANGUAGE_ID As String = "language_id"

    'REQ-976
    Public Const COL_NAME_DAYS_TO_CLAWBACK As String = "number_of_days_clawback"
    Public Const COL_NAME_BRANCH_ID As String = "branch_id"
    'End REQ-976

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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("comm_p_code_entity_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function FindRecord(id As Guid) As Boolean
        Dim found As Boolean = False
        Dim oCounter As Integer
        Dim oDataSet As DataSet
        Dim oRow As DataRow
        Dim selectStmt As String = Config("/SQL/FIND")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("comm_p_code_entity_id", id.ToByteArray)}
        Try
            oDataSet = New DataSet
            DBHelper.Fetch(oDataSet, selectStmt, TABLE_NAME, parameters)
            oRow = oDataSet.Tables(CommPCodeEntityDAL.TABLE_NAME).Rows(0)
            oCounter = CType(oRow("entityRecords"), Integer)
            If oCounter > 0 Then
                found = True
            End If
            'If ((oDataSet.Tables.Contains(CommPCodeEntityDAL.TABLE_NAME) = True) AndAlso _
            '    (oDataSet.Tables(CommPCodeEntityDAL.TABLE_NAME).Rows.Count > 0)) Then
            '    found = True
            'End If
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
        Return found
    End Function

    Public Sub LoadList(commPCodeId As Guid, familyDataset As DataSet)
        ' Dim parameters() As OracleParameter
        Dim selectStmt As String = Config("/SQL/LOAD_BY_PERIOD_ID")
        '   parameters = New OracleParameter() {New OracleParameter(Me.COL_NAME_COMMISSION_PERIOD_ID, commissionPeriodId.ToByteArray)}
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
            {New DBHelper.DBHelperParameter(COL_NAME_COMM_P_CODE_ID, commPCodeId.ToByteArray)}
        DBHelper.Fetch(familyDataset, selectStmt, TABLE_NAME, parameters)
    End Sub

    Public Function LoadList(commPCodeId As Guid, oLanguageId As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_LIST")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
            {New DBHelper.DBHelperParameter(COL_NAME_COMM_P_CODE_ID, commPCodeId.ToByteArray), _
             New DBHelper.DBHelperParameter(COL_NAME_LANGUAGE_ID, oLanguageId.ToByteArray), _
             New DBHelper.DBHelperParameter(COL_NAME_LANGUAGE_ID, oLanguageId.ToByteArray)}
        ', _
        'New DBHelper.DBHelperParameter(COL_NAME_LANGUAGE_ID, oLanguageId.ToByteArray)}
        Dim ds As New DataSet
        Return DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
    End Function

    Public Function LoadEntities(oDataSet As DataSet, commPCodeId As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_ENTITIES")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
            {New DBHelper.DBHelperParameter(COL_NAME_COMM_P_CODE_ID, commPCodeId.ToByteArray)}
        '  Dim ds As New DataSet
        Return DBHelper.Fetch(oDataSet, selectStmt, TABLE_NAME, parameters)
    End Function

#End Region

#Region "Overloaded Methods"
    Public Overloads Sub Update(ds As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing, Optional ByVal changesFilter As DataRowState = Nothing)
        If ds Is Nothing Then
            Return
        End If
        If Not ds.Tables(TABLE_NAME) Is Nothing Then
            MyBase.Update(ds.Tables(TABLE_NAME), Transaction, changesFilter)
        End If
    End Sub
#End Region


End Class


