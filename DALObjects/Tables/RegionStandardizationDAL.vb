'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (2/17/2005)********************


Public Class RegionStandardizationDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_REGION_STANDARDIZATION"
    Public Const TABLE_KEY_NAME As String = "region_standardization_id"

    Public Const COL_NAME_REGION_STANDARDIZATION_ID As String = "region_standardization_id"
    Public Const COL_NAME_DESCRIPTION As String = "description"
    Public Const COL_NAME_COUNTRY_ID As String = "country_id"
    Public Const COL_NAME_COUNTRY_NAME = "country_name"
    Public Const COL_NAME_REGION As String = "region_description"
    Public Const COL_NAME_REGION_ID As String = "region_id"


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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("region_standardization_id", id.ToByteArray)}
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

    Public Function GetRegionAliasList(description As String, _
                                    regionId As Guid, _
                                    countryId As Guid) As DataSet

        Dim selectStmt As String = Config("/SQL/LOAD_LIST")
        Dim ds As New DataSet

        description = description.ToUpper
        If (Not (description.Equals(String.Empty))) AndAlso (FormatSearchMask(description)) Then
            selectStmt &= Environment.NewLine & "AND UPPER(rgnstand.DESCRIPTION) " & description
        End If

        If Not regionId.Equals(Guid.Empty) Then
            selectStmt &= Environment.NewLine & "AND rgnstand.region_id = '" & GuidToSQLString(regionId) & "'"
        End If

        selectStmt &= Environment.NewLine & "ORDER BY UPPER(rgnstand.DESCRIPTION)"

        'Return (DBHelper.Fetch(selectStmt, Me.RISK_TYPE_LIST))
        Dim param As New DBHelper.DBHelperParameter("country_id", GuidToSQLString(countryId))

        DBHelper.Fetch(ds, selectStmt, TABLE_NAME, _
                        New DBHelper.DBHelperParameter() {param})
        Return ds

    End Function

    Public Function GetRegionAliasList(description As String, regionId As Guid, userCountries As ArrayList) As DataSet

        Dim selectStmt As String = Config("/SQL/LOAD_LIST_FOR_USER_COUNTRIES")
        Dim inClausecondition As String = ""
        inClausecondition &= "rgnstand." & MiscUtil.BuildListForSql(COL_NAME_COUNTRY_ID, userCountries, False)

        description = description.ToUpper
        If (Not (description.Equals(String.Empty))) AndAlso (FormatSearchMask(description)) Then
            selectStmt &= Environment.NewLine & "AND UPPER(rgnstand.DESCRIPTION) " & description
        End If

        If Not regionId.Equals(Guid.Empty) Then
            selectStmt &= Environment.NewLine & "AND rgnstand.region_id = '" & GuidToSQLString(regionId) & "'"
        End If

        selectStmt &= Environment.NewLine & "ORDER BY UPPER(c.description), UPPER(rgnstand.description)"

        If Not inClausecondition = "" Then
            selectStmt = selectStmt.Replace(DYNAMIC_IN_CLAUSE_PLACE_HOLDER, inClausecondition)
        Else
            selectStmt = selectStmt.Replace(DYNAMIC_IN_CLAUSE_PLACE_HOLDER, "")
        End If

        Try
            Return (DBHelper.Fetch(selectStmt, TABLE_NAME))
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


