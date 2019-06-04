Imports System.IO
Imports System.Xml
Imports System.Xml.Xsl
Imports Assurant.Common.Ftp

Public Class InvoiceXmlDownload
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim strDealerId As String = Request.QueryString("DealerID").Trim
        Dim strInvoiceMonth As String = Request.QueryString("InvoiceMonth").Trim
        Dim strTest As String = Request.QueryString("Test").Trim

        If strDealerId <> String.Empty Then
            Dim dealerID As Guid = New Guid(strDealerId)
            Dim blTest As Boolean
            If Boolean.TryParse(strTest, blTest) Then

                If blTest Then
                    TestDownLoadXML(dealerID, strInvoiceMonth)
                Else
                    DownLoadXML(dealerID, strInvoiceMonth)
                End If
            End If
        End If
    End Sub
    Private Sub TestDownLoadXML(ByVal dealerId As Guid, ByVal periodMonth As String)

        Try

            Dim afaInvoiceData As New AfaInvoiceData(dealerId, periodMonth)

            Response.Clear()
            Response.ClearContent()

            If Not afaInvoiceData Is Nothing AndAlso
                Not String.IsNullOrEmpty(afaInvoiceData.DirectoryName) AndAlso
                Not String.IsNullOrEmpty(afaInvoiceData.Filename) Then

                Response.StatusCode = 200

            Else
                Response.StatusCode = 404
                Response.Write(TranslationBase.TranslateLabelOrMessage(Assurant.ElitaPlus.Common.ErrorCodes.ACCT_ATTACHMENT_NOT_FOUND))
            End If


        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception

        End Try



    End Sub

    Private Sub DownLoadXML(ByVal dealerId As Guid, ByVal periodMonth As String)

        Try

            Dim afaInvoiceData As New AfaInvoiceData(dealerId, periodMonth)

            If Not afaInvoiceData Is Nothing AndAlso
                Not String.IsNullOrEmpty(afaInvoiceData.DirectoryName) AndAlso
                Not String.IsNullOrEmpty(afaInvoiceData.Filename) Then

                TransferFilesUnixWebServer(afaInvoiceData.Filename, afaInvoiceData.DirectoryName)
            Else
                Throw New GUIException(Assurant.ElitaPlus.Common.ErrorCodes.ACCT_ATTACHMENT_NOT_FOUND, Assurant.ElitaPlus.Common.ErrorCodes.ACCT_ATTACHMENT_NOT_FOUND)
            End If

        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception

        End Try



    End Sub

    Private Sub TransferFilesUnixWebServer(ByVal fileName As String, Optional ByVal directoryName As String = "afa_data")

        Dim objUnixFTP As New sFtp(AppConfig.UnixServer.HostName, AppConfig.UnixServer.FtpDirectory.Replace("/ftp", $"/{directoryName}"), AppConfig.UnixServer.UserId, AppConfig.UnixServer.Password)

        Try

            Dim userPathWebServer As String = MiscUtil.GetUniqueDirectory(AppConfig.UnixServer.InterfaceDirectory, ElitaPlusPrincipal.Current.Identity.Name)
            MiscUtil.CreateFolder(userPathWebServer)

            Dim downloadFileName As String = Path.Combine(userPathWebServer, fileName)
            objUnixFTP.DownloadFile(fileName, downloadFileName)

            Response.Clear()
            Response.ClearContent()
            Response.ClearHeaders()
            Response.ContentType = "text/csv"
            Response.AddHeader("Content-Disposition", "attachment; filename=" & downloadFileName)
            Response.BinaryWrite(File.ReadAllBytes(downloadFileName))
            Response.Flush()
            Response.End()
        Finally

        End Try
    End Sub
End Class