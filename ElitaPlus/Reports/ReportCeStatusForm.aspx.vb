Public Partial Class ReportCeStatusForm
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Response.ContentType = "text/xml"
        If Not Session("ReportRunning") Is Nothing AndAlso Session("ReportRunning").ToString = "OK" Then
            If Session("ReportRunning").ToString = "OK" Then Session("ReportRunning") = ""

            'Dim rpturl As String = "<![CDATA[http:" + Session("ReportLink").ToString + "]]>"
            'Dim rptname As String = "<![CDATA[http:" + Session("ReportName").ToString + "]]>"            
            'Dim strurl As String = "<?xml version='1.0' encoding='utf-8' ?><data><result>OK</result><message>" + rpturl + "</message></data>"
            Dim rpturl As String = "<![CDATA[" + Session("ReportLink").ToString + "]]>"
            Dim rptname As String = Session("ReportName").ToString
            Dim rpttype As String = Session("RptType").ToString
            Dim strurl As String = "<?xml version='1.0' encoding='utf-8' ?><data><result>OK</result><url>" + rpturl + "</url><name>" + rptname + "</name><type>" + rpttype + "</type></data>"
            Response.Write(strurl)
        Else
            'Response.Write("<?xml version='1.0' encoding='utf-8' ?><data><result>NOT OK</result><message>Report NOT ready!</message></data>")
            Response.Write("<?xml version='1.0' encoding='utf-8' ?><data><result>NOT OK</result><url>1</url><name>Report NOT ready!</name><type></type></data>")
        End If
    End Sub

End Class