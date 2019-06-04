'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (12/29/2016)********************


Public Class CertImageDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_CERT_IMAGE"
    Public Const TABLE_KEY_NAME As String = "cert_image_id"

    Public Const COL_NAME_CERT_IMAGE_ID As String = "cert_image_id"
    Public Const COL_NAME_CERT_ID As String = "cert_id"
    Public Const COL_NAME_IMAGE_ID As String = "image_id"
    Public Const COL_NAME_DOCUMENT_TYPE_ID As String = "document_type_id"
    Public Const COL_NAME_SCAN_DATE As String = "scan_date"
    Public Const COL_NAME_FILE_NAME As String = "file_name"
    Public Const COL_NAME_FILE_SIZE_BYTES As String = "file_size_bytes"
    Public Const COL_NAME_COMMENTS As String = "comments"
    Public Const COL_NAME_USER_NAME As String = "user_name"

#End Region

#Region "Constructors"
    Public Sub New()
        MyBase.New()
    End Sub

#End Region

#Region "Load Methods"

    Public Sub LoadSchema(ByVal ds As DataSet)
        Load(ds, Guid.Empty)
    End Sub

    Public Sub Load(ByVal familyDS As DataSet, ByVal id As Guid)
        Dim selectStmt As String = Me.Config("/SQL/LOAD")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("cert_image_id", id.ToByteArray)}
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

    Public Function LoadList(ByVal familyDS As DataSet, ByVal id As Guid) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST_CERT_ID")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("cert_id", id.ToByteArray)}
        Return DBHelper.Fetch(familyDS, selectStmt, Me.TABLE_NAME, parameters)
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


