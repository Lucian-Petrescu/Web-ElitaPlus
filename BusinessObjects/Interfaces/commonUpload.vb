Public Class commonUpload
    Inherits BusinessObjectBase

    Public Shared Sub DumpFileToTable(ByVal strUploadType As String, ByVal fileLines As System.Collections.Generic.List(Of String))
        Dim dal As New commonUploadDAL
        dal.InsertUploadFileLines(strUploadType, fileLines)
    End Sub

    Public Shared Sub DumpFileToTableNew(ByVal strUploadType As String, ByVal fileLines As System.Collections.Generic.List(Of String), ByVal fileName As String)
        Dim dal As New commonUploadDAL
        'dal.InsertUploadFileLinesNew(strUploadType, fileLines, fileName)
        'Improve performance by inserting lines in bulk.
        dal.InsertUploadFileLinesBulk(strUploadType, fileLines, fileName)
        
    End Sub

    Public Shared Function InitUpload(ByVal strFileName As String, ByVal strUploadType As String, ByVal strErrMsg As String) As String
        Dim dal As New commonUploadDAL
        Dim strResult As String
        dal.InitUpload(strFileName, strUploadType, ElitaPlusIdentity.Current.ActiveUser.NetworkId, strResult, strErrMsg)
        'If strResult = "S" Then
        '    Return True
        'Else
        '    Return False
        'End If

        Return strResult
    End Function

    Public Shared Sub ProcessUploadedFile(ByVal strUploadType As String, ByVal strCompanyGroupCode As String, ByVal strUserEmailAddress As String, ByVal strUser As String)
        Dim dal As New commonUploadDAL
        dal.ProcessFile(strUploadType, Guid.Empty, strCompanyGroupCode, strUserEmailAddress, strUser)
    End Sub
    Public Shared Sub ProcessUploadedFile(ByVal strUploadType As String, ByVal guidStatusID As Guid, ByVal strCompanyGroupCode As String, ByVal strUserEmailAddress As String, ByVal strUser As String)
        Dim dal As New commonUploadDAL
        dal.ProcessFile(strUploadType, guidStatusID, strCompanyGroupCode, strUserEmailAddress, strUser)
    End Sub
    Public Shared Sub ProcessUploadedFileAsync(ByVal strUploadType As String, ByVal guidStatusID As Guid, ByVal strCompanyGroupCode As String, ByVal strUserEmailAddress As String, ByVal strUser As String)
        Dim dal As New commonUploadDAL
        dal.ProcessFileAsync(strUploadType, guidStatusID, strCompanyGroupCode, strUserEmailAddress, strUser)
    End Sub

    Public Shared Function GetProcessingError(ByVal strUploadType As String) As DataView
        Dim dal As New commonUploadDAL
        Return dal.LoadProcessingError(strUploadType).Tables(0).DefaultView
    End Function

    Public Shared Sub ExtractReport(strUploadType As String, ByVal strUserEmailAddress As String, ByVal strCompanyGroupCode As String, ByVal extractFile As String)
        Dim dal As New commonUploadDAL
        dal.ExtractReportFile(strUploadType, strUserEmailAddress, strCompanyGroupCode, extractFile)

    End Sub
End Class
