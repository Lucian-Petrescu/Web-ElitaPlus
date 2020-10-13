'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (8/29/2012)********************


Public Class VendorContactDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_VENDOR_CONTACT"
    Public Const TABLE_KEY_NAME As String = "vendor_contact_id"

    Public Const COL_NAME_VENDOR_CONTACT_ID As String = "vendor_contact_id"
    Public Const COL_NAME_SERVICE_CENTER_ID As String = "service_center_id"
    Public Const COL_NAME_CONTACT_INFO_ID As String = "contact_info_id"
    Public Const COL_NAME_EFFECTIVE As String = "effective"
    Public Const COL_NAME_EXPIRATION As String = "expiration"

    Public Const COL_NAME_NAME As String = "Name"
    Public Const COL_NAME_JOB_TITLE As String = "Job_Title"
    Public Const COL_NAME_COMPANY As String = "Company"
    Public Const COL_NAME_EMAIL As String = "Email"
    Public Const COL_NAME_ADDRESS_TYPE_ID As String = "Address_type_id"

    Public Const COL_NAME_ADDRESS_ID As String = "address_id"
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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(COL_NAME_SERVICE_CENTER_ID, id.ToByteArray)}
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

    Public Sub LoadList(familyDS As DataSet, id As Guid)
        Dim selectStmt As String = Config("/SQL/LOAD_LIST")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(COL_NAME_SERVICE_CENTER_ID, id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function GetContactInfoView(ContactInfoId As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_CONTACT_INFO_LIST")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(COL_NAME_CONTACT_INFO_ID, ContactInfoId.ToByteArray)}
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


