Imports System.IO
Imports System.Xml
Imports System.Xml.Xsl


Partial Class ServiceOrderPreview
    Inherits System.Web.UI.Page

#Region "Constants"
    Public Const URL As String = "ServiceOrderPreview.aspx"

    Private Const DEFAULT_SERVICE_ORDER As String = "ServiceOrder_DEFAULT.xslt"
    Private Const EXTENSION_XSLT As String = ".xslt"
    Private Const EXTENSION_RPT As String = ".rpt"

#End Region

#Region "Constructors"
    'Public Sub New()
    '    MyBase.New(True)
    'End Sub
#End Region

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(sender As System.Object, e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Page Events"

    Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Dim oServiceOrder As Assurant.ElitaPlus.BusinessObjectsNew.ServiceOrder
        'this mechanism is used when there are two serviceorders and we want to show the first one in the
        'back ground if we need to show the email prompt
        Dim nvc As New ElitaPlusPage
        If nvc.NavController.FlowSession(FlowSessionKeys.SESSION_PREV_SERVICEORDER) Is Nothing Then
            oServiceOrder = CType(nvc.NavController.FlowSession(FlowSessionKeys.SESSION_NEXT_SERVICEORDER), Assurant.ElitaPlus.BusinessObjectsNew.ServiceOrder)
        Else
            oServiceOrder = CType(nvc.NavController.FlowSession(FlowSessionKeys.SESSION_PREV_SERVICEORDER), Assurant.ElitaPlus.BusinessObjectsNew.ServiceOrder)
            nvc.NavController.FlowSession(FlowSessionKeys.SESSION_PREV_SERVICEORDER) = Nothing
        End If

        If oServiceOrder IsNot Nothing Then
            '08/24/2006 - ALR - Added to create the PDF if it does not exist
            Dim pdfImg() As Byte
            Dim soController As New ServiceOrderController
            Dim strReportName As String = soController.GenerateReportName(oServiceOrder.ClaimId, oServiceOrder.ClaimAuthorizationId) + EXTENSION_XSLT

            If oServiceOrder.ServiceOrderImage Is Nothing AndAlso System.IO.File.Exists(Server.MapPath(strReportName)) Then 'XSLT Report exists 

                Dim xdoc As New System.Xml.XmlDocument
                xdoc.LoadXml(oServiceOrder.ServiceOrderImageData)
                xmlSource.TransformSource = strReportName
                xmlSource.Document = xdoc

            ElseIf oServiceOrder.ServiceOrderImage IsNot Nothing Then 'If the pdf image exists, then print it directly to the screen

                Controls.Remove(xmlSource)

                pdfImg = oServiceOrder.ServiceOrderImage
                Response.ClearHeaders()
                Response.ClearContent()
                Response.Cache.SetMaxAge(New System.TimeSpan(0))
                Response.AddHeader("Content-Type", "application/pdf")
                Response.BinaryWrite(pdfImg)
                Response.End()

            Else 'ALR-- Added default Service Order to catch all others.

                Dim xdoc As New System.Xml.XmlDocument
                xdoc.LoadXml(oServiceOrder.ServiceOrderImageData)
                xmlSource.TransformSource = DEFAULT_SERVICE_ORDER
                xmlSource.Document = xdoc

            End If

        Else
            Response.Write("Invalid ServiceOrder Object")
        End If
    End Sub


#End Region

End Class
