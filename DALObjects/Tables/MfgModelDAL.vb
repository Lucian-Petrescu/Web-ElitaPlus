'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (9/17/2004)********************


Public Class MfgModelDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_MFG_MODEL"
    Public Const TABLE_KEY_NAME As String = "mfg_model_id"

    Public Const COL_NAME_MFG_MODEL_ID = "mfg_model_id"
    Public Const COL_NAME_DEALER_ID = "dealer_id"
    Public Const COL_NAME_MANUFACTURER_ID = "manufacturer_id"
    Public Const COL_NAME_DESCRIPTION = "description"
    'Public Const COL_NAME_COMPANY_ID = "company_id"
    Public Const COL_NAME_COMPANY_GROUP_ID = "company_group_id"

    Private Const DSNAME As String = "LIST"
    Public Const WILDCARD As Char = "%"

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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("mfg_model_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, Me.TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadList() As DataSet
        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST")
        Return DBHelper.Fetch(selectStmt, Me.TABLE_NAME)
    End Function

    Public Function LoadList(ByVal description As String, ByVal dealerId As Guid, _
                             ByVal manufacturerId As Guid, ByVal CompanyGroupId As Guid) As DataSet

        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST")
        Dim parameters() As OracleParameter

        description = GetFormattedSearchStringForSQL(description)

        If ((Not dealerId.Equals(Guid.Empty)) AndAlso (Not manufacturerId.Equals(Guid.Empty))) Then
            parameters = New OracleParameter() _
                                            {New OracleParameter(COL_NAME_DEALER_ID, dealerId.ToByteArray), _
                                             New OracleParameter(COL_NAME_MANUFACTURER_ID, manufacturerId.ToByteArray), _
                                             New OracleParameter(COL_NAME_DESCRIPTION, description), _
                                             New OracleParameter(COL_NAME_COMPANY_GROUP_ID, CompanyGroupId.ToByteArray)}
        ElseIf ((dealerId.Equals(Guid.Empty)) AndAlso (manufacturerId.Equals(Guid.Empty))) Then
            parameters = New OracleParameter() _
                                            {New OracleParameter(COL_NAME_DEALER_ID, WILDCARD), _
                                             New OracleParameter(COL_NAME_MANUFACTURER_ID, WILDCARD), _
                                             New OracleParameter(COL_NAME_DESCRIPTION, description), _
                                             New OracleParameter(COL_NAME_COMPANY_GROUP_ID, CompanyGroupId.ToByteArray)}
        ElseIf (dealerId.Equals(Guid.Empty)) Then
            parameters = New OracleParameter() _
                                            {New OracleParameter(COL_NAME_DEALER_ID, WILDCARD), _
                                             New OracleParameter(COL_NAME_MANUFACTURER_ID, manufacturerId.ToByteArray), _
                                             New OracleParameter(COL_NAME_DESCRIPTION, description), _
                                             New OracleParameter(COL_NAME_COMPANY_GROUP_ID, CompanyGroupId.ToByteArray)}
        ElseIf (manufacturerId.Equals(Guid.Empty)) Then
            parameters = New OracleParameter() _
                                            {New OracleParameter(COL_NAME_DEALER_ID, dealerId.ToByteArray), _
                                             New OracleParameter(COL_NAME_MANUFACTURER_ID, WILDCARD), _
                                             New OracleParameter(COL_NAME_DESCRIPTION, description), _
                                             New OracleParameter(COL_NAME_COMPANY_GROUP_ID, CompanyGroupId.ToByteArray)}
        End If

        Try
            Return (DBHelper.Fetch(selectStmt, DSNAME, Me.TABLE_NAME, parameters))
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Overloads Sub Update(ByVal ds As DataSet, Optional ByVal transaction As IDbTransaction = Nothing)
        DBHelper.Execute(ds.Tables(Me.TABLE_NAME), Config("/SQL/INSERT"), Config("/SQL/UPDATE"), Config("/SQL/DELETE"), Nothing, transaction)
    End Sub

    Public Function GetMakeAndModelForDealer(ByVal ManufacturerId As Guid, Model As String, ByVal DealerID As Guid) As DataSet

        Dim ds As New DataSet
        Dim parameters() As OracleParameter
        Dim selectStmt As String = Me.Config("/SQL/WS_GETMAKEMODELFORDEALER")

        parameters = New OracleParameter() {New OracleParameter(Me.COL_NAME_MANUFACTURER_ID, ManufacturerId.ToByteArray), _
                                            New OracleParameter(COL_NAME_DEALER_ID, DealerID.ToByteArray), _
                                            New OracleParameter(Me.COL_NAME_DESCRIPTION, Model)}
        Return DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)
    End Function
#End Region

End Class



