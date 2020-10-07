Imports System.IO
Imports System.Xml
Imports System.Xml.Xsl

Public Class DealerInvoicePreview
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Dim strQSValue As String, dealerID As Guid, strXSLTName As String
        strQSValue = Request.QueryString("DealerID").Trim
        If strQSValue <> String.Empty Then
            dealerID = New Guid(strQSValue)
            Dim objDealer As Dealer = New Dealer(dealerID)
            strXSLTName = "InvoiceTemplate_" & objDealer.Dealer & ".xslt"

            strQSValue = Request.QueryString("RptType").Trim
            Dim isDetailedReport As Boolean
            If strQSValue = "S" Then
                isDetailedReport = False
            ElseIf strQSValue = "D" Then
                isDetailedReport = True
            End If


            If isDetailedReport = True AndAlso objDealer.AttributeValues.Contains(Codes.DLR_ATTRBT__USE_DETAILED_INV_RPT) Then
                If objDealer.AttributeValues.Value(Codes.DLR_ATTRBT__USE_DETAILED_INV_RPT) = Codes.YESNO_Y Then
                    strXSLTName = "InvoiceTemplate_" & objDealer.Dealer & "_CSV.xslt"
                End If
            End If


            strQSValue = Request.QueryString("InvoiceMonth").Trim
            Dim objInv As AfaInvoiceData = New AfaInvoiceData(dealerID, strQSValue)


            Dim strXML As String, strHTML As String, strCSV As String
            If objInv.InvoiceXmlData Is Nothing Then
                strXML = String.Empty
            Else
                strXML = objInv.InvoiceXmlData.ToString
            End If

            If objInv.InvoiceHtml Is Nothing Then
                strHTML = String.Empty
            Else
                strHTML = objInv.InvoiceHtml
            End If

            If objInv.InvoiceCSV Is Nothing Then
                strCSV = String.Empty
            Else
                strCSV = objInv.InvoiceCSV
            End If

            'strXML = AfaInvoiceData.GetDealerInvoiceData(dealerID, strQSValue)
            If strXML.Trim = String.Empty Then
                Controls.Remove(xmlSource)
                literalNotFound.Text = "<h2>Dealer " & objDealer.DealerName & " (" & objDealer.Dealer & ") has no invoice available for accounting period of " & strQSValue & "</h2>"
            ElseIf isDetailedReport = False AndAlso strHTML.Trim <> String.Empty Then
                Controls.Remove(xmlSource)
                literalNotFound.Text = strHTML
            ElseIf isDetailedReport = True AndAlso strCSV.Trim <> String.Empty Then
                Controls.Remove(xmlSource)
                literalNotFound.Text = strCSV
            Else
                Dim xdoc As New System.Xml.XmlDocument
                xdoc.LoadXml(strXML)
                xmlSource.TransformSource = strXSLTName
                xmlSource.Document = xdoc
                Controls.Remove(literalNotFound)

                Dim objXSLTransform As New XslTransform
                objXSLTransform.Load(Server.MapPath(strXSLTName))

                Using writerOutput = New StringWriter()
                    objXSLTransform.Transform(xdoc, Nothing, writerOutput)

                    If isDetailedReport = True AndAlso objDealer.AttributeValues.Contains(Codes.DLR_ATTRBT__USE_DETAILED_INV_RPT) Then
                        AfaInvoiceData.SaveInvoiceCSV(objInv.Id, writerOutput.ToString())
                    Else
                        AfaInvoiceData.SaveInvoiceHTML(objInv.Id, writerOutput.ToString())
                    End If

                    'Download("myInv.csv", writerOutput)

                End Using

            End If

        End If
    End Sub

    Private Sub Download(sFileName As String, sWriter As StringWriter)

        Dim oMemoryStream As MemoryStream = New MemoryStream()
        Dim encoding As New System.Text.UTF8Encoding()
        Dim byteArray As Byte() = encoding.GetBytes(sWriter.ToString())

        oMemoryStream.Write(byteArray, 0, byteArray.GetLength(0))
        oMemoryStream.Seek(0, SeekOrigin.Begin)

        Response.Clear()
        'Response.ContentType = "APPLICATION/OCTET-STREAM"
        Response.ContentType = "TEXT/PLAIN"
        Dim Header As String = "Attachment; Filename=" & sFileName & ";"
        Response.AppendHeader("Content-Disposition", Header)

        Response.OutputStream.Write(oMemoryStream.GetBuffer(), 0, Convert.ToInt32(oMemoryStream.GetBuffer().Length))

        Response.OutputStream.Flush()
        Response.Buffer = True
        Response.OutputStream.Close()

    End Sub


End Class