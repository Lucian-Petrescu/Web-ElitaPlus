Public Class commonUpload
    Inherits BusinessObjectBase

    Public Shared Sub DumpFileToTable(strUploadType As String, fileLines As System.Collections.Generic.List(Of String))
        Dim dal As New commonUploadDAL
        dal.InsertUploadFileLines(strUploadType, fileLines)
    End Sub

    Public Shared Sub DumpFileToTableNew(strUploadType As String, fileLines As System.Collections.Generic.List(Of String), fileName As String)
        Dim dal As New commonUploadDAL
        'dal.InsertUploadFileLinesNew(strUploadType, fileLines, fileName)
        'Improve performance by inserting lines in bulk.
        dal.InsertUploadFileLinesBulk(strUploadType, fileLines, fileName, ElitaPlusIdentity.Current.ActiveUser.NetworkId)

    End Sub

    Public Shared Function InitUpload(strFileName As String, strUploadType As String, strErrMsg As String) As String
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

    Public Shared Sub ProcessUploadedFile(strUploadType As String, strCompanyGroupCode As String, strUserEmailAddress As String, strUser As String)
        Dim dal As New commonUploadDAL
        dal.ProcessFile(strUploadType, Guid.Empty, strCompanyGroupCode, strUserEmailAddress, strUser)
    End Sub
    Public Shared Sub ProcessUploadedFile(strUploadType As String, guidStatusID As Guid, strCompanyGroupCode As String, strUserEmailAddress As String, strUser As String)
        Dim dal As New commonUploadDAL
        dal.ProcessFile(strUploadType, guidStatusID, strCompanyGroupCode, strUserEmailAddress, strUser)
    End Sub
    Public Shared Sub ProcessUploadedFileAsync(strUploadType As String, guidStatusID As Guid, strCompanyGroupCode As String, strUserEmailAddress As String, strUser As String)
        Dim dal As New commonUploadDAL
        dal.ProcessFileAsync(strUploadType, guidStatusID, strCompanyGroupCode, strUserEmailAddress, strUser)
    End Sub

    Public Shared Function GetProcessingError(strUploadType As String) As DataView
        Dim dal As New commonUploadDAL
        Return dal.LoadProcessingError(strUploadType).Tables(0).DefaultView
    End Function

    Public Shared Sub ExtractReport(strUploadType As String, strUserEmailAddress As String, strCompanyGroupCode As String, extractFile As String)
        Dim dal As New commonUploadDAL
        dal.ExtractReportFile(strUploadType, strUserEmailAddress, strCompanyGroupCode, extractFile)

    End Sub
    Public Shared Function getScreenHelp(FormName As String)
        Dim dal As New commonUploadDAL
        Dim helpData As String

        helpData = dal.getScreenHelpData(FormName)

        If String.IsNullOrEmpty(helpData) OrElse helpData.Equals(Codes.ERROR_FLAG) Then
            helpData = Codes.NO_HELP_COMTS_FOUND
        End If

        Return helpData
    End Function
End Class
