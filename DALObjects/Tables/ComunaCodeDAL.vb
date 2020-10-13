'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (10/8/2009)********************

Public Class ComunaCodeDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_COMUNA_CODE"
    Public Const TABLE_KEY_NAME As String = "comuna_code_id"

    Public Const COL_NAME_COMUNA_CODE_ID As String = "comuna_code_id"
    Public Const COL_NAME_REGION_ID As String = "region_id"
    Public Const COL_NAME_REGION_DESC As String = "region_dscription"
    Public Const COL_NAME_COMUNA As String = "comuna"
    Public Const COL_NAME_POSTALCODE As String = "postalcode"
    Public Const COL_NAME_USER_ID As String = "user_id"
    Public Const COL_NAME_COUNTRY_ID = "country_id"

    Private Const DSNAME As String = "LIST"
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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("comuna_code_id", id.ToByteArray)}
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

    Public Function LoadList(ComunaMask As String, PostalCodeMask As String, RegionIdMask As Guid, UserId As Guid) As DataSet

        Dim selectStmt As String = Config("/SQL/LOAD_LIST")
        Dim parameters() As OracleParameter
        Dim whereClauseConditions As String = ""
        Dim ds As New DataSet

        ComunaMask = GetFormattedSearchStringForSQL(ComunaMask)
        PostalCodeMask = GetFormattedSearchStringForSQL(PostalCodeMask)
        If Not RegionIdMask.Equals(Guid.Empty) Then
            whereClauseConditions &= Environment.NewLine & "AND " & "c.region_id = " & MiscUtil.GetDbStringFromGuid(RegionIdMask)
        End If

        If Not whereClauseConditions = "" Then
            selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
        Else
            selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, "")
        End If

        parameters = New OracleParameter() _
                                    {New OracleParameter(COL_NAME_COMUNA, ComunaMask), _
                                     New OracleParameter(COL_NAME_POSTALCODE, PostalCodeMask), _
                                     New OracleParameter(COL_NAME_USER_ID, UserId.ToByteArray)}
        Try
            DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Function LoadList(countryID As Guid) As DataSet

        Dim selectStmt As String = Config("/SQL/LOAD_LIST_DYNAMIC")
        Dim parameters() As OracleParameter
        parameters = New OracleParameter() _
                            {New OracleParameter(COL_NAME_COUNTRY_ID, countryID.ToByteArray)}
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
        If ds.Tables(TABLE_NAME) IsNot Nothing Then
            MyBase.Update(ds.Tables(TABLE_NAME), Transaction, changesFilter)
        End If
    End Sub
#End Region


End Class


