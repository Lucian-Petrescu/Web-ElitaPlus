'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (5/23/2012)********************


Public Class ClaimStatusActionDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_CLAIM_STATUS_ACTION"
    Public Const TABLE_KEY_NAME As String = "claim_status_action_id"
    Private Const DSNAME As String = "CLAIM_STATUS_ACTION"

    Public Const COL_NAME_CLAIM_STATUS_ACTION_ID As String = "claim_status_action_id"
    Public Const COL_NAME_COMPANY_GROUP_ID As String = "company_group_id"
    Public Const COL_NAME_ACTION_ID As String = "action_id"
    Public Const COL_NAME_CURRENT_STATUS_ID As String = "current_status_id"
    Public Const COL_NAME_NEXT_STATUS_ID As String = "next_status_id"

    Public Const COL_NAME_ACTION As String = "action"
    Public Const COL_NAME_CURRENT_STATUS As String = "current_status"
    Public Const COL_NAME_NEXT_STATUS As String = "next_status"
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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("claim_status_action_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadList(companyGroupId As Guid, languageId As Guid) As DataSet

        Dim selectStmt As String = Config("/SQL/LOAD_LIST")
        Dim parameters() As OracleParameter
        parameters = New OracleParameter() _
                                    {New OracleParameter(COL_NAME_LANGUAGE_ID, languageId.ToByteArray), _
                                     New OracleParameter(COL_NAME_LANGUAGE_ID, languageId.ToByteArray), _
                                     New OracleParameter(COL_NAME_LANGUAGE_ID, languageId.ToByteArray), _
                                     New OracleParameter(COL_NAME_COMPANY_GROUP_ID, companyGroupId.ToByteArray)}
        Try
            Return (DBHelper.Fetch(selectStmt, DSNAME, TABLE_NAME, parameters))
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

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


