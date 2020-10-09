'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (1/18/2005)********************


Public Class PartsInfoDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_PARTS_INFO"
    Public Const TABLE_NAME_WS As String = "CLAIM_PARTS_INFO"
    Public Const TABLE_KEY_NAME As String = "parts_info_id"

    Public Const COL_NAME_PARTS_INFO_ID As String = "parts_info_id"
    Public Const COL_NAME_CLAIM_ID As String = "claim_id"
    Public Const COL_COMPANY_ID As String = "company_id"
    Public Const COL_NAME_PARTS_DESCRIPTION_ID As String = "parts_description_id"
    Public Const COL_NAME_IN_STOCK_ID As String = "in_stock_id"
    Public Const COL_NAME_COST As String = "cost"
    Public Const COL_NAME_PARTS_DESCRIPTION As String = "description"
    Public Const COL_NAME_RISK_GROUP_ID As String = "risk_group_id"
    Public Const FILD_NAME_COMPANY_GROUP_ID As String = "company_group_id"
    Public Const COL_NAME_LANGUAGE_ID As String = "language_id"

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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("parts_info_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadList() As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_LIST")
        Return DBHelper.Fetch(selectStmt, TABLE_NAME)
    End Function

    Public Function LoadSelectedList(claimID As Guid, languageId As Guid) As DataSet

        Dim selectStmt As String = Config("/SQL/LOAD_LIST")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(COL_NAME_LANGUAGE_ID, languageId.ToByteArray), _
                                                                                            New DBHelper.DBHelperParameter(COL_NAME_CLAIM_ID, claimID.ToByteArray)}
        Try
            Dim ds = New DataSet
            Return (DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters))
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Function LoadAvailList(riskGroupID As Guid, claimID As Guid, companyGrpID As Guid) As DataSet

        Dim selectStmt As String = Config("/SQL/LOAD_AVAIL_LIST")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(COL_NAME_RISK_GROUP_ID, riskGroupID.ToByteArray), _
                                                         New DBHelper.DBHelperParameter(FILD_NAME_COMPANY_GROUP_ID, companyGrpID.ToByteArray), _
                                                         New DBHelper.DBHelperParameter(COL_NAME_CLAIM_ID, claimID.ToByteArray)}
        Try
            Dim ds = New DataSet
            Return (DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters))
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Function LoadAvailListWithCurrentPart(riskGroupID As Guid, claimID As Guid, partsDescID As Guid, companyGrpID As Guid) As DataSet

        Dim selectStmt As String = Config("/SQL/LOAD_AVAIL_LIST_WITH_PART")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(COL_NAME_RISK_GROUP_ID, riskGroupID.ToByteArray), _
                                                                                           New DBHelper.DBHelperParameter(FILD_NAME_COMPANY_GROUP_ID, companyGrpID.ToByteArray), _
                                                                                           New DBHelper.DBHelperParameter(COL_NAME_CLAIM_ID, claimID.ToByteArray), _
                                                                                           New DBHelper.DBHelperParameter(COL_NAME_PARTS_DESCRIPTION_ID, partsDescID.ToByteArray)}
        Try
            Dim ds = New DataSet
            Return (DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters))
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

#End Region

#Region "Overloaded Methods"
    Public Overloads Sub Update(ds As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing, Optional ByVal changesFilter As DataRowState = Nothing)
        If Not ds.Tables(TABLE_NAME) Is Nothing Then
            MyBase.Update(ds.Tables(TABLE_NAME), Transaction, changesFilter)
        End If
    End Sub
#End Region


End Class


