Imports System.IO


Namespace DownLoad

Public Class DownLoadBase
        Inherits System.Web.UI.Page

#Region "Parameters"

        Public Structure DownLoadParams

            Public Enum DownLoadTypeCode
                FILE
                GRID
            End Enum

            Public downLoadCode As DownLoadTypeCode
            Public fileName, layout As String
            Public data As DataSet
            Public DeleteFileAfterDownload As Boolean
        End Structure

#End Region

#Region "Constants"

        Public Const SESSION_PARAMETERS_DOWNLOAD_KEY As String = "DOWNLOAD_BASE_SESSION_PARAMETERS_KEY"
        Private FILE_NAME_INVALID_CHARACTERS As String() = {"\", "/", ":", "*", "?", """", "<", ">", "|"} 'Operating system invalid characters in a file name.

#End Region

#Region "Process"
        Public Sub SendFile(ByVal sourceFileName As String, ByVal deleteFileAfterDownload As Boolean)
            Dim fInfo As FileInfo = New FileInfo(sourceFileName)

            Response.ClearContent()
            Response.ClearHeaders()
            ' Response.ContentType = "application/x-download"
            Response.ContentType = "application/zip"
            Response.AddHeader("Content-Disposition", "attachment;filename=" & fInfo.Name)
            Response.AddHeader("Content-Length", fInfo.Length.ToString)
            Response.TransmitFile(sourceFileName)
            Response.Flush()

            If deleteFileAfterDownload Then
                fInfo.Delete()
            End If

            Response.End()

        End Sub

        Public Sub SendFile(ByVal sourceFileName As String)
            SendFile(sourceFileName, False)
        End Sub

        Public Sub CreateExcelHeader(ByVal sourceFileName As String)
            Response.ClearContent()
            Response.ClearHeaders()
            Response.ContentType = "application/vnd.ms-excel"
            Response.AddHeader("Content-Disposition", "Attachment;filename=" + sourceFileName)
        End Sub

#End Region
    End Class

End Namespace

