
'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (10/26/2004)********************


Public Class ServiceCenterZipDistrictDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_SERVICE_CENTER_ZIP_DST"
    Public Const TABLE_KEY_NAME As String = "service_center_zip_dst_id"

    Public Const COL_NAME_SERVICE_CENTER_ZIP_DST_ID As String = "service_center_zip_dst_id"
    Public Const COL_NAME_SERVICE_CENTER_ID As String = "service_center_id"
    Public Const COL_NAME_ZIP_DISTRICT_ID As String = "zip_district_id"

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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("service_center_zip_dst_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    'Public Function LoadList() As DataSet
    '    Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST")
    '    Return DBHelper.Fetch(selectStmt, Me.TABLE_NAME)
    'End Function

    'This Method's body was added manually
    Public Sub LoadList(ds As DataSet, serviceCenterZipDistrictId As Guid)
        Dim selectStmt As String = Config("/SQL/LOAD_LIST")
        Dim serviceCenterZipDistrictParam As New DBHelper.DBHelperParameter("service_center_zip_dst_id", serviceCenterZipDistrictId.ToByteArray)
        DBHelper.Fetch(ds, selectStmt, TABLE_NAME, New DBHelper.DBHelperParameter() {serviceCenterZipDistrictParam})
    End Sub

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



