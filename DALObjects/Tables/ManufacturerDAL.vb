'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (8/30/2004)********************


Public Class ManufacturerDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_MANUFACTURER"
    Public Const TABLE_NAME_MAKES_AND_MODEL_INFO As String = "MakesAndModelInformation"
    Public Const TABLE_KEY_NAME As String = "manufacturer_id"
    Public Const DATASET_NAME__GET_MAKE_AND_MODEL_RESPONSE As String = "GetMakesAndModelResponse"
    Public Const COL_NAME_MANUFACTURER_ID = "manufacturer_id"
    Public Const COL_NAME_DESCRIPTION = "description"
    Public Const COL_NAME_COMPANY_GROUP_ID = "company_group_id"
    Public Const COL_NAME_DEALER_ID = "dealer_id"
    Private Const DSNAME As String = "VSCMakes"
    Public Const WILDCARD As Char = "%"

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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("manufacturer_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Sub Load(familyDS As DataSet, sDesc As String, CompanyGroupID As Guid)
        Dim selectStmt As String = Config("/SQL/LOADFROMDESCANDCOMPGROUP")
        Dim parameters() As OracleParameter
        sDesc = GetFormattedSearchStringForSQL(sDesc)
        parameters = New OracleParameter() _
                                    {New OracleParameter(COL_NAME_COMPANY_GROUP_ID, CompanyGroupID.ToByteArray), _
                                     New OracleParameter(COL_NAME_DESCRIPTION, sDesc)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadList() As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_LIST")
        Try
            Return DBHelper.Fetch(selectStmt, TABLE_NAME)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function LoadList(description As String, companyGroupId As Guid) As DataSet

        Dim selectStmt As String = Config("/SQL/LOAD_LIST")
        Dim parameters() As OracleParameter
        'description &= WILDCARD
        description = GetFormattedSearchStringForSQL(description)
        parameters = New OracleParameter() _
                                    {New OracleParameter(COL_NAME_COMPANY_GROUP_ID, companyGroupId.ToByteArray), _
                                     New OracleParameter(COL_NAME_DESCRIPTION, description)}
        Try
            Return (DBHelper.Fetch(selectStmt, DSNAME, TABLE_NAME, parameters))
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function
    Public Function LoadVSCMakes(companyGroupId As Guid) As DataSet

        Dim selectStmt As String = Config("/SQL/LOAD_VSC_MAKES")
        Dim parameters() As OracleParameter

        parameters = New OracleParameter() _
                                    {New OracleParameter(COL_NAME_COMPANY_GROUP_ID, companyGroupId.ToByteArray)}
        Try
            Return (DBHelper.Fetch(selectStmt, DSNAME, TABLE_NAME, parameters))
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Function LoadMakesForWS(companyGroupId As Guid) As DataSet

        Dim selectStmt As String = Config("/SQL/LOAD_MAKES_FOR_WS")
        Dim parameters() As OracleParameter

        parameters = New OracleParameter() _
                                    {New OracleParameter(COL_NAME_COMPANY_GROUP_ID, companyGroupId.ToByteArray)}
        Try
            Return (DBHelper.Fetch(selectStmt, DATASET_NAME__GET_MAKE_AND_MODEL_RESPONSE, TABLE_NAME_MAKES_AND_MODEL_INFO, parameters))
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Function GetMakesForWSByWarrantyMaster(dealerId As Guid, companyGroupId As Guid) As DataSet

        Dim selectStmt As String = Config("/SQL/LOAD_MAKES_FOR_WS_BY_WARRANTY_MASTER")
        Dim parameters() As OracleParameter

        parameters = New OracleParameter() _
                                    {New OracleParameter(COL_NAME_COMPANY_GROUP_ID, companyGroupId.ToByteArray), _
                                     New OracleParameter(COL_NAME_DEALER_ID, dealerId.ToByteArray)}
        Try
            Return (DBHelper.Fetch(selectStmt, DATASET_NAME__GET_MAKE_AND_MODEL_RESPONSE, TABLE_NAME_MAKES_AND_MODEL_INFO, parameters))
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Overloads Sub Update(ds As DataSet, Optional ByVal transaction As IDbTransaction = Nothing)
        DBHelper.Execute(ds.Tables(TABLE_NAME), Config("/SQL/INSERT"), Config("/SQL/UPDATE"), Config("/SQL/DELETE"), Nothing, transaction)
    End Sub

    Public Overloads Function ResolveManufacturer(manufacturerName As String, companyGroupId As Guid) As Guid
        Try
            Dim selectStmt As String = Config("/SQL/RESOLVE_MANUFACTURER")
            Dim parameters() As DBHelper.DBHelperParameter
            Dim result As Object
            parameters = New DBHelper.DBHelperParameter() _
                            {New DBHelper.DBHelperParameter("p_manufacturer_name", manufacturerName), _
                             New DBHelper.DBHelperParameter("p_company_group_id", companyGroupId.ToByteArray), _
                             New DBHelper.DBHelperParameter("p_company_group_code", DBNull.Value)}
            result = DBHelper.ExecuteScalar(selectStmt, parameters)
            If (result Is Nothing) Then
                Return Guid.Empty
            Else
                Return New Guid(DirectCast(result, Byte()))
            End If
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

#End Region

End Class



