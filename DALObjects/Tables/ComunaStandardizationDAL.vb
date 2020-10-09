'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (10/8/2009)********************


Public Class ComunaStandardizationDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_COMUNA_STANDARDIZATION"
    Public Const TABLE_KEY_NAME As String = "comuna_alias_id"

    Public Const COL_NAME_COMUNA_ALIAS_ID As String = "comuna_alias_id"
    Public Const COL_NAME_COMUNA_ALIAS As String = "comuna_alias"
    Public Const COL_NAME_COMUNA_CODE_ID As String = "comuna_code_id"
    Public Const COL_NAME_COMUNA As String = "comuna"
    Public Const COL_NAME_USER_ID As String = "user_id"
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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("comuna_alias_id", id.ToByteArray)}
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

    Public Function LoadList(ComunaAliasMask As String, ComunaMask As String, UserId As Guid) As DataSet

        Dim selectStmt As String = Config("/SQL/LOAD_LIST")
        Dim parameters() As OracleParameter
        Dim ds As New DataSet

        ComunaMask = GetFormattedSearchStringForSQL(ComunaMask)
        ComunaAliasMask = GetFormattedSearchStringForSQL(ComunaAliasMask)

        parameters = New OracleParameter() _
                                    {New OracleParameter(COL_NAME_COMUNA_ALIAS, ComunaAliasMask), _
                                     New OracleParameter(COL_NAME_COMUNA, ComunaMask), _
                                     New OracleParameter(COL_NAME_USER_ID, UserId.ToByteArray)}
        Try
            DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Function GetComunaList(UserId As Guid) As DataSet

        Dim selectStmt As String = Config("/SQL/GET_COMUNA_LIST")
        Dim parameters() As OracleParameter
        Dim ds As New DataSet

        parameters = New OracleParameter() {New OracleParameter(COL_NAME_USER_ID, UserId.ToByteArray)}
        Try
            DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Function GetComunaStanderization(UserId As Guid, ComunaAliasMask As String) As DataSet

        Dim selectStmt As String = Config("/SQL/GET_COMUNA_STANDARDIZATION_LIST")
        Dim parameters() As OracleParameter
        Dim ds As New DataSet

        parameters = New OracleParameter() _
                                    {New OracleParameter(COL_NAME_USER_ID, UserId.ToByteArray), _
                                     New OracleParameter(COL_NAME_COMUNA_ALIAS, ComunaAliasMask)}

        Try
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
        If Not ds.Tables(TABLE_NAME) Is Nothing Then
            MyBase.Update(ds.Tables(TABLE_NAME), Transaction, changesFilter)
        End If
    End Sub
#End Region


End Class


