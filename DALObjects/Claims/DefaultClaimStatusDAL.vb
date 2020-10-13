'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (2/2/2015)********************


Public Class DefaultClaimStatusDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_DEFAULT_CLAIM_STATUS"
    Public Const TABLE_KEY_NAME As String = "default_claim_status_id"

    Public Const COL_NAME_DEFAULT_CLAIM_STATUS_ID As String = "default_claim_status_id"
    Public Const COL_NAME_CLAIM_STATUS_BY_GROUP_ID As String = "claim_status_by_group_id"
    Public Const COL_NAME_DEFAULT_TYPE_ID As String = "default_type_id"
    Public Const COL_NAME_METHOD_OF_REPAIR_ID As String = "method_of_repair_id"

    Public Const COL_NAME_COMPANY_GROUP_ID As String = "company_group_id"
    Public Const COL_NAME_LANGUAGE_ID1 As String = "language_id1"
    Public Const COL_NAME_LANGUAGE_ID2 As String = "language_id2"
    Public Const COL_NAME_LANGUAGE_ID3 As String = "language_id3"

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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("default_claim_status_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadList(CompanyGroupId As Guid, languageId As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_LIST")


        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() { _
         New DBHelper.DBHelperParameter(COL_NAME_LANGUAGE_ID1, languageId.ToByteArray), _
         New DBHelper.DBHelperParameter(COL_NAME_COMPANY_GROUP_ID, CompanyGroupId.ToByteArray), _
         New DBHelper.DBHelperParameter(COL_NAME_LANGUAGE_ID2, languageId.ToByteArray), _
         New DBHelper.DBHelperParameter(COL_NAME_LANGUAGE_ID3, languageId.ToByteArray)}

        Try
            Dim ds As New DataSet
            DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
            Return ds
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
        If ds.Tables(TABLE_NAME) IsNot Nothing Then
            MyBase.Update(ds.Tables(TABLE_NAME), Transaction, changesFilter)
        End If
    End Sub
#End Region


End Class


