'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (10/22/2004)********************


Public Class ZipDistrictDetailDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_ZIP_DISTRICT_DETAIL"
    Public Const TABLE_KEY_NAME As String = "zip_district_detail_id"

    Public Const COL_NAME_ZIP_DISTRICT_DETAIL_ID As String = "zip_district_detail_id"
    Public Const COL_NAME_ZIP_DISTRICT_ID As String = "zip_district_id"
    Public Const COL_NAME_ZIP_CODE As String = "zip_code"
    Public Const COL_NAME_COUNTRY_ID As String = "country_id"

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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("zip_district_detail_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, Me.TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Sub LoadList(ByVal familyDs As DataSet, ByVal zipDistrictId As Guid)
        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST")
        Dim zipDistrictParam As New DBHelper.DBHelperParameter(Me.COL_NAME_ZIP_DISTRICT_ID, zipDistrictId)
        DBHelper.Fetch(familyDs, selectStmt, Me.TABLE_NAME, New DBHelper.DBHelperParameter() {zipDistrictParam})
    End Sub

    Public Function LoadNegativeList(ByVal countryId As Guid, ByVal zipDistrictId As Guid) As DataSet
        Dim ds As New DataSet
        Dim selectStmt As String = Me.Config("/SQL/LOAD_NEGATIVE_LIST")
        Dim zipDistrictParam As New DBHelper.DBHelperParameter(Me.COL_NAME_ZIP_DISTRICT_ID, zipDistrictId)
        Dim compParam As New DBHelper.DBHelperParameter(Me.COL_NAME_COUNTRY_ID, countryId)
        DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, New DBHelper.DBHelperParameter() {compParam, zipDistrictParam})
        Return ds
    End Function

#End Region

#Region "Overloaded Methods"
    Public Overloads Sub Update(ByVal ds As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing, Optional ByVal changesFilter As DataRowState = Nothing)
        If Not ds.Tables(Me.TABLE_NAME) Is Nothing Then
            MyBase.Update(ds.Tables(Me.TABLE_NAME), Transaction, changesFilter)
        End If
    End Sub
#End Region


End Class


