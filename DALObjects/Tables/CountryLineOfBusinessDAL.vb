'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (10/17/2018)********************


Public Class CountryLineOfBusinessDAL
    Inherits DALBase


#Region "Constants"
	Public Const TABLE_NAME As String = "ELP_COUNTRY_LINE_OF_BUSINESS"
	Public Const TABLE_KEY_NAME As String = "country_line_of_business_id"
	
	Public Const COL_NAME_COUNTRY_LINE_OF_BUSINESS_ID As String = "country_line_of_business_id"
	Public Const COL_NAME_COUNTRY_ID As String = "country_id"
	Public Const COL_NAME_CODE As String = "code"
	Public Const COL_NAME_DESCRIPTION As String = "description"
	Public Const COL_NAME_LINE_OF_BUSINESS_ID As String = "line_of_business_id"
    Public Const COL_NAME_IN_USE As String = "in_use"
    Public Const COL_NAME_BUSINESS_TYPE As String = "businesstype"

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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("country_line_of_business_id", id.ToByteArray)}
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

    Public Function LoadList(countryId As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_LIST")
        Dim ds As New DataSet
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(COL_NAME_COUNTRY_ID, countryId.ToByteArray)}
        Try
            DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
            Return (ds)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Function GetLineOfBusinessUsedInContracts(countryId As Guid) As DataSet

        Dim selectStmt As String = Config("/SQL/CHECK_LINEOFBUSINESS_EXISTING_CONTRACT")
        Dim ds As New DataSet
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(COL_NAME_COUNTRY_ID, countryId.ToByteArray)}

        Try
            DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
            Return (ds)

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


