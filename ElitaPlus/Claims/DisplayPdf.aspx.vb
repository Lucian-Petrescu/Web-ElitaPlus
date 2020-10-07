Imports System.IO
Imports iTextSharp.text
Imports iTextSharp.text.pdf

Public Class DisplayPdf
    Inherits ServerViewStatePage

#Region "CONST"
    Public Const IMAGE_ID As String = "ImageId"
    Public Const CLAIM_ID As String = "ClaimId"
    Public Const CERTIFICATE_ID As String = "CertificateId"
    Public Const CONTENT_TYPE As String = "application/pdf"
#End Region

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Dim oDoc As Doc.Document = New Doc.Document
        Dim iImage As iTextSharp.text.Image
        Dim iDocument As iTextSharp.text.Document
        Dim oMemoryStream As MemoryStream = New MemoryStream()
        Dim oPdfWriter As iTextSharp.text.pdf.PdfWriter

        Dim ImageId As Guid
        If Request.QueryString(IMAGE_ID) IsNot Nothing Then
            ImageId = New Guid(Request.QueryString(IMAGE_ID).ToString)
        Else
            Return
        End If

        If Request.QueryString(CLAIM_ID) IsNot Nothing Then
            Dim claimId As Guid
            claimId = New Guid(Request.QueryString(CLAIM_ID).ToString)
            Dim oClaim As ClaimBase = ClaimFacade.Instance.GetClaim(Of ClaimBase)(claimId)
            For Each ci As ClaimImage In oClaim.ClaimImagesList
                If (ci.ImageId = ImageId) Then

                    Try
                        Response.Clear()
                        Response.ContentType = Documents.DocumentManager.Current.FileTypes.Where(Function(ft) ft.Extension = ci.FileName.Split(".".ToCharArray()).Last().ToUpper()).First().MimeType
                        Response.OutputStream.Write(oClaim.Company.GetClaimImageRepository().Download(ImageId).Data, 0, oClaim.Company.GetClaimImageRepository().Download(ImageId).Data.Length)
                        Response.OutputStream.Flush()
                        Response.Buffer = True
                        Response.OutputStream.Close()
                    Catch ex As Exception
                        Throw ex
                    Finally
                        oMemoryStream.Flush()
                    End Try
                End If
            Next
        ElseIf Request.QueryString(CERTIFICATE_ID) IsNot Nothing Then
            Dim certificateId As Guid
            certificateId = New Guid(Request.QueryString(CERTIFICATE_ID).ToString)
            Dim oCertificate As Certificate = New Certificate(certificateId)
            For Each ci As CertImage In oCertificate.CertificateImagesList
                If (ci.ImageId = ImageId) Then

                    Try
                        Response.Clear()
                        Response.ContentType = Documents.DocumentManager.Current.FileTypes.Where(Function(ft) ft.Extension = ci.FileName.Split(".".ToCharArray()).Last().ToUpper()).First().MimeType
                        Response.OutputStream.Write(oCertificate.Company.GetCertificateImageRepository().Download(ImageId).Data, 0, oCertificate.Company.GetCertificateImageRepository().Download(ImageId).Data.Length)
                        Response.OutputStream.Flush()
                        Response.Buffer = True
                        Response.OutputStream.Close()
                    Catch ex As Exception
                        Throw ex
                    Finally
                        oMemoryStream.Flush()
                    End Try
                End If
            Next
        Else

            Try
                oDoc = DocumentImaging.Doc.DownloadDocument(ImageId)
                iImage = iTextSharp.text.Image.GetInstance(oDoc.Data)

                Dim memStream As New MemoryStream(oDoc.Data, 0, oDoc.Data.Length, False)

                Dim tif As System.Drawing.Image = System.Drawing.Image.FromStream(memStream)
                If tif IsNot Nothing Then 'check if its a Valid Image
                    Dim hresolution As Single = tif.Width
                    Dim vresolution As Single = tif.Height
                    tif.Dispose()

                    iDocument = New iTextSharp.text.Document(New Rectangle(0, 0, hresolution, vresolution)) ' '(PageSize.A6)
                    iDocument.SetMargins(0, 0, 0, 0)
                    oMemoryStream = New MemoryStream()

                    oPdfWriter = PdfWriter.GetInstance(iDocument, oMemoryStream)
                    iDocument.Open()
                    iDocument.Add(iImage)
                    iDocument.Close()
                    oPdfWriter.Close()
                End If
            Catch ex As Exception
                Throw ex
            Finally
                If (iDocument IsNot Nothing) Then
                    If iDocument.IsOpen Then
                        iDocument.Close()
                    End If
                End If
            End Try

            Try
                Response.Clear()
                Response.ContentType = CONTENT_TYPE
                Response.OutputStream.Write(oMemoryStream.GetBuffer(), 0, Convert.ToInt32(oMemoryStream.GetBuffer().Length))
                Response.OutputStream.Flush()
                Response.Buffer = True
                Response.OutputStream.Close()
            Catch ex As Exception
                Throw ex
            Finally
                oMemoryStream.Flush()
            End Try
        End If
    End Sub

End Class