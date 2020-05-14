'Imports Assurant.Common.CrystalEnterpriseHelper
'Imports Assurant.ElitaPlus.ElitaPlusWebApp
'Imports System.Collections.Generic

Namespace Reports

    Partial Class ReportCeProgressForm
       Inherits System.Web.UI.Page

'#Region " Web Form Designer Generated Code "

'        'This call is required by the Web Form Designer.
'        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

'        End Sub

'        'NOTE: The following placeholder declaration is required by the Web Form Designer.
'        'Do not delete or move it.
'        Private designerPlaceholderDeclaration As System.Object

'        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
'            'CODEGEN: This method call is required by the Web Form Designer
'            'Do not modify it using the code editor.
'            InitializeComponent()
'        End Sub

'#End Region

'#Region "Constants"
'#End Region

'        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
'            'Put user code to initialize the page here
'            If Not Page.IsPostBack Then
'                If Request.QueryString("Message") Is Nothing Then
'                    lblMessage.Text = "Performing Action..."                    
'                Else
'                    lblMessage.Text = Request.QueryString("Message")
'                    lblStatus.Text = ""
'                    If Not Session("ReportErrorButton") Is Nothing Then
'                        moReportErrorButton.Value = Session("ReportErrorButton").ToString
'                        moReportStatus.Value = Session("StatusControl").ToString
'                        moReportErrorMsg.Value = Session("ControlErrMsg").ToString
'                    Else
'                        moReportErrorButton.Value = ""
'                        moReportStatus.Value = ""
'                        moReportErrorMsg.Value = ""
'                    End If
'                End If
'                StartProgressBar()
'            End If

'        End Sub

'        'Private Sub BtnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnCancel.Click
'        '    'Dim moStatus As CEHelper.RptStatus
'        '    'moStatus = CEHelper.RptStatus.SUCCESS
'        '    'CloseCeProgressBarParent(moStatus.GetName(GetType(CEHelper.RptStatus), moStatus))
'        '    Dim oCEHelper As CEHelper

'        '    oCEHelper = ReportCeBase.GetCEHelper()
'        '    oCEHelper.Status = CEHelper.RptStatus.CANCEL
'        'End Sub

'        'Protected Sub CloseCeProgressBarParent(ByVal statusMsg As String)
'        '    Dim sJavaScript As String
'        '    '   Dim transMsg As String = TranslateLabelOrMessage(statusMsg)
'        '    Dim transMsg As String = statusMsg

'        '    sJavaScript = "<SCRIPT>" & Environment.NewLine
'        '    sJavaScript &= "parent.document.all('moReportCeInputControl_moReportCeStatus').value = '" & transMsg & "';" & Environment.NewLine
'        '    '   sJavaScript &= "buttonClick();" & Environment.NewLine
'        '    sJavaScript &= "</SCRIPT>" & Environment.NewLine
'        '    Me.RegisterStartupScript("CloseCeProgressBarParent", sJavaScript)
'        'End Sub

'        '<System.Web.Services.WebMethod()> _
'        'Public Shared Function GetRptInstanceStatus() As List(Of String)

'        '        Dim lstStatus As New List(Of String)
'        '        Dim cebase As New Reports.ReportCeBase
'        '        Dim session_param As Reports.ReportCeBase.Params          
'        '        Dim RPT_REQUEST As String = "Request Submitted"
'        '        Dim RPT_PENDING As String = "Pending"
'        '        Dim RPT_SUCCESS As String = "Success"
'        '        Dim RPT_FAILURE As String = "Failure"
'        '        Dim RPT_VIEW_HISTORY As String = "ViewHistory"
'        '        Dim instanceId As Long
'        '        Dim oViewer As String
'        '        Dim MAX_SCHED_POLL As Integer = 15000

'        '        Try                
'        '            If Not HttpContext.Current.Session(cebase.SESSION_PARAMETERS_KEY) Is Nothing Then
'        '                session_param = CType(HttpContext.Current.Session(cebase.SESSION_PARAMETERS_KEY), Reports.ReportCeBase.Params)
'        '                If (session_param.moAction = Reports.ReportCeBase.RptAction.SCHEDULE_VIEW OrElse _
'        '                    session_param.moAction = Reports.ReportCeBase.RptAction.SCHEDULE) Then
'        '                    instanceId = CType(session_param.instanceId, Long)
'        '                    'instanceId = CType(HttpContext.Current.Session(cebase.SESSION_INSTANCE_ID), Long)
'        '                    If (session_param.moRptFormat = ReportCeBaseForm.RptFormat.TEXT_CSV) Then
'        '                        oViewer = "IFRAME"
'        '                    Else
'        '                        oViewer = "WINDOWOPEN"
'        '                    End If

'        '                    If instanceId > 0 Then

'        '                        lstStatus = Reports.ReportCeBase.ReportInstanceStatus(instanceId)
'        '                        If lstStatus.Count > 0 Then
'        '                            lstStatus.Insert(4, session_param.msRptWindowName)
'        '                            lstStatus.Insert(5, oViewer)
'        '                            lstStatus.Insert(6, [Enum].GetName(GetType(ReportCeBase.RptAction), _
'        '                                                               session_param.moAction))
'        '                            lstStatus.Insert(7, ReportCeBase.GetRptFtp(session_param))
'        '                            If session_param.moAction = ReportCeBase.RptAction.SCHEDULE_VIEW Then
'        '                                If (lstStatus(0).ToString <> RPT_SUCCESS AndAlso _
'        '                                    lstStatus(0).ToString <> RPT_FAILURE) Then
'        '                                    System.Threading.Thread.CurrentThread.Sleep(MAX_SCHED_POLL)
'        '                                End If
'        '                            ElseIf session_param.moAction = ReportCeBase.RptAction.SCHEDULE Then
'        '                                If (lstStatus(0).ToString <> RPT_PENDING AndAlso _
'        '                                    lstStatus(0).ToString <> RPT_SUCCESS AndAlso _
'        '                                    lstStatus(0).ToString <> RPT_FAILURE) Then
'        '                                    System.Threading.Thread.CurrentThread.Sleep(MAX_SCHED_POLL)
'        '                                End If
'        '                            End If

'        '                            Return lstStatus
'        '                        End If
'        '                    Else
'        '                        lstStatus.Add(RPT_REQUEST)
'        '                        Return lstStatus
'        '                    End If
'        '                ElseIf session_param.moAction = Reports.ReportCeBase.RptAction.VIEW Then
'        '                    lstStatus.Add(RPT_VIEW_HISTORY)
'        '                    Return lstStatus
'        '                End If
'        '            End If

'        '            Return lstStatus

'        '            'Catch exT As TimeoutException
'        '        Catch exT As System.Threading.ThreadAbortException
'        '        Catch ex As Exception
'        '            System.Threading.Thread.CurrentThread.Sleep(MAX_SCHED_POLL)
'        '            lstStatus.Add("Failed " + ex.Message)
'        '            Return lstStatus
'        '        End Try

'        '    End Function

'        Protected Sub StartProgressBar()
'            Dim sJavaScript As String

'            sJavaScript = "<SCRIPT>" & Environment.NewLine
'            sJavaScript &= "LoadWaitMsg();" & Environment.NewLine
'            sJavaScript &= "ShowReportCeContainer();" & Environment.NewLine
'            sJavaScript &= "</SCRIPT>" & Environment.NewLine
'            Me.RegisterStartupScript("StartProgressBar", sJavaScript)
'        End Sub
    End Class

End Namespace