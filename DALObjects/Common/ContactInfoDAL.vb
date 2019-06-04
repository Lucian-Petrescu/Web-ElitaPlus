'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (11/2/2011)********************


Public Class ContactInfoDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_CONTACT_INFO"
    Public Const TABLE_KEY_NAME As String = "contact_info_id"

    Public Const COL_NAME_CONTACT_INFO_ID As String = "contact_info_id"
    Public Const COL_NAME_ADDRESS_TYPE_ID As String = "address_type_id"
    Public Const COL_NAME_ADDRESS_ID As String = "address_id"
    Public Const COL_NAME_SALUTATION_ID As String = "salutation_id"
    Public Const COL_NAME_NAME As String = "name"
    Public Const COL_NAME_HOME_PHONE As String = "home_phone"
    Public Const COL_NAME_WORK_PHONE As String = "work_phone"
    Public Const COL_NAME_EMAIL As String = "email"
    Public Const COL_NAME_CELL_PHONE As String = "cell_phone"
    Public Const COL_NAME_COMPANY As String = "company"
    Public Const COL_NAME_JOB_TITLE As String = "job_title"
    Public Const COL_NAME_EFFECTIVE As String = "effective"
    Public Const COL_NAME_EXPIRATION As String = "expiration"
    Public Const COL_NAME_FIRST_NAME As String = "first_name"
    Public Const COL_NAME_LAST_NAME As String = "last_name"
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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("contact_info_id", id.ToByteArray)}
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

    Public Sub LoadList(ByVal familyDS As DataSet, ByVal id As Guid)
        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(Me.COL_NAME_CONTACT_INFO_ID, id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, Me.TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function GetAddressView(ByVal AddressId As Guid) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/LOAD_ADDRESS_LIST")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(Me.COL_NAME_ADDRESS_ID, AddressId.ToByteArray)}
        Try
            Dim ds As New DataSet
            DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function
#End Region

#Region "Overloaded Methods"
    Public Overloads Sub Update(ByVal ds As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing, Optional ByVal changesFilter As DataRowState = Nothing)
        If ds Is Nothing Then
            Return
        End If
        If Not ds.Tables(Me.TABLE_NAME) Is Nothing Then
            MyBase.Update(ds.Tables(Me.TABLE_NAME), Transaction, changesFilter)
        End If
    End Sub
#End Region


End Class


