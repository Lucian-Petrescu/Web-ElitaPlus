'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (9/24/2012)********************


Public Class ClaimImageDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_CLAIM_IMAGE"
    Public Const TABLE_KEY_NAME As String = "claim_image_id"

    Public Const COL_NAME_CLAIM_IMAGE_ID As String = "claim_image_id"
    Public Const COL_NAME_CLAIM_ID As String = "claim_id"
    Public Const COL_NAME_IMAGE_ID As String = "image_id"
    Public Const COL_NAME_DOCUMENT_TYPE_ID As String = "document_type_id"
    Public Const COL_NAME_IMAGE_STATUS_ID As String = "image_status_id"
    Public Const COL_NAME_IS_LOCAL_REPOSITORY As String = "is_local_repository"
    Public Const COL_NAME_SCAN_DATE As String = "scan_date"
    Public Const COL_NAME_FILE_NAME As String = "file_name"
    Public Const COL_NAME_FILE_SIZE_BYTES As String = "file_size_bytes"
    Public Const COL_NAME_COMMENTS As String = "comments"
    Public Const COL_NAME_USER_NAME As String = "user_name"
    Public Const COL_NAME_DELETE_FLAG As String = "delete_flag"

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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("claim_image_id", id.ToByteArray)}
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
    
    Public Function LoadList(familyDS As DataSet, id As Guid, Optional ByVal loadAllFiles As Boolean = False) As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_LIST_CLAIM_ID")                
        Dim loadAllFilesStr As String
        
        if loadAllFiles
            loadAllFilesStr = "Y"
        Else
            loadAllFilesStr = "N"
        End If

        Dim inputParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() { _
            New DBHelper.DBHelperParameter("pi_reference_id", id.ToByteArray),
            New DBHelper.DBHelperParameter("pi_referenceType", TABLE_NAME),
            New DBHelper.DBHelperParameter("pi_LoadAllFiles", loadAllFilesStr)
            }

        Dim outputParameter() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("po_files_list", GetType(DataSet))}
        DBHelper.FetchSp(selectStmt, inputParameters, outputParameter, familyDS, TABLE_NAME)    

    End Function

    Public Sub UpdateDocumentDeleteFlag(imageId As Guid, deleteFlag As String, modifiedById As String)
        Dim selectStmt As String = Config("/SQL/UPDATE_DOCUMENT_DELETE_FLAG")
        
        Dim inputParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("pi_document_id", imageId.ToByteArray),
                                                                                                New DBHelper.DBHelperParameter("pi_modified_by", modifiedById),
                                                                                                New DBHelper.DBHelperParameter("pi_deleteFlag", deleteFlag)}                   
        DBHelper.ExecuteSp(selectStmt, inputParameters,Nothing)
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


