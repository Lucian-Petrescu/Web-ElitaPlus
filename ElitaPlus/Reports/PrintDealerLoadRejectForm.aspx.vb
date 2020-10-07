Imports Assurant.ElitaPlus.ElitaPlusWebApp.Generic
Imports Assurant.ElitaPlus.ElitaPlusWebApp.Interfaces
Imports Assurant.ElitaPlus.DALObjects
Imports System.Threading
Imports Assurant.ElitaPlus.Security

Namespace Reports

    Partial Class PrintDealerLoadRejectForm
        Inherits ElitaPlusPage

#Region "Constants"
        Private Const RPT_FILENAME_WINDOW_PAYMENTS As String = "Rejected Payments"
        Private Const RPT_FILENAME_WINDOW_PROCESSED_PAYMENTS As String = "Processed Payments"

        Private Const RPT_FILENAME_WINDOW_CERTIFICATES As String = "Rejected Certificates"
        Private Const RPT_FILENAME_WINDOW_CERTIFICATES_ERROR As String = "Rejected Certificates Error Export"
        Private Const RPT_FILENAME_WINDOW_PROCESSED_CERTIFICATES As String = "Processed Certificates"

        Private Const RPT_REJECTED_PAYMENTS_FILENAME As String = "RejectedPayments"

        Private Const RPT_REJECTED_PAYMENTS_FILENAME_EXP As String = "RejectedPayments-Exp"
        Private Const RPT_PROCESSED_PAYMENTS_FILENAME_EXP As String = "ProcessedPayments-Exp"

        Private Const RPT_REJECTED_CERTIFICATES_FILENAME As String = "RejectedCertificates"
        Private Const RPT_REJECTED_CERTIFICATES_FILENAME_EXP As String = "RejectedCertificates-Exp"

        Private Const RPT_REJECTED_CERTIFICATES_ERROR_FILENAME As String = "RejectedCertificates_ErrorExp"
        Private Const RPT_PROCESSED_CERTIFICATES_FILENAME_EXP As String = "ProcessedCertificates-Exp"
        'DEF-1680
        Private Const RPT_REJECTED_PAYMENTS_ERROR_EXP As String = "RejectedPayments_ErrorExp"
        Private Const RPT_FILENAME_WINDOW_PAYMENTS_ERROR As String = "Rejected Payments Error Export"
        'End DEF-1680

        Private Const RPT_FILENAME_EXPORT As String = "RejectedNewClaims_Exp"
        Private Const TOTAL_PARAMS As Integer = 7 ' 8 Elements
        Private Const TOTAL_EXP_PARAMS As Integer = 3 ' 4 Elements
        Private Const PARAMS_PER_REPORT As Integer = 4 ' 4 Elements
        Private Const ALL As String = "*"
        Private Const SP_DATE_FORMAT As String = "yyyyMMdd"
        Private Const YES As String = "Y"
        Private Const NO As String = "N"
        Public Shared URL As String = ELPWebConstants.APPLICATION_PATH & "/Reports/PrintDealerLoadRejectForm.aspx"

        Public Const REJECT_REPORT As Integer = 0
        Public Const ERROR_EXPORT As Integer = 1
        Public Const PROCESSED_EXPORT As Integer = 2
        Public Const DealerType_VSC As String = "VSC"
        Public Const OPTION_FTP As String = "FTP"

        Public Const CERT_PAGETITLE As String = "DEALER_FILE"
        Public Const PYMT_PAGETITLE As String = "DEALER_PAYMENT"
        Public Const PAGETAB As String = "REPORTS"
        Public Const TAB_NAME_REJECT_REPORT As String = "Reject Report"
        Public Const TAB_NAME_ERROR_EXPORT As String = "Error Export"
        Public Const TAB_NAME_PROCESSED_EXPORT As String = "Processed Export"

#End Region

#Region "variables"
        Private reportFormat As ReportCeBaseForm.RptFormat
        Private reportName As String
        Private reportFileNameWindow As String
        Public culturevalue As String

        Public Structure ReportParams
            Public claimfile_processed_id As Guid
        End Structure

#End Region

#Region "Page State"

        Private Class PageStatus
            Public Sub New()
            End Sub
        End Class

        Class MyState
            Public DealerfileProcessedId As Guid
            Public moInterfaceTypeCode As DealerFileProcessedData.InterfaceTypeCode
            Public reportType As Integer
            Public iscultureImplemented As Boolean
            Public SelectionCode As String
            Public dealertype As String
            Public isParentFile As String
            Public parentFileName As String
            Public MyBO As ReportRequests
            Public ForEdit As Boolean = False
            Public IsNew As Boolean = False
            Public IsACopy As Boolean
            Public HasDataChanged As Boolean
        End Class

        Public Sub New()
            MyBase.New(New MyState)
        End Sub

        Protected Shadows ReadOnly Property State() As MyState
            Get
                Return CType(MyBase.State, MyState)
            End Get
        End Property

        Private Sub SetStateProperties()
            'Dim oDealerFileProcessed As DealerFileProcessed
            State.DealerfileProcessedId = CType(CType(CallingParameters, MyState).DealerfileProcessedId, Guid)
            State.moInterfaceTypeCode = CType(CType(CallingParameters, MyState).moInterfaceTypeCode, DealerFileProcessedData.InterfaceTypeCode)
            State.reportType = CType(CallingParameters, MyState).reportType
            State.dealertype = CType(CallingParameters, MyState).dealertype
            State.SelectionCode = CType(CallingParameters, MyState).SelectionCode
            State.isParentFile = CType(CallingParameters, MyState).isParentFile
            State.parentFileName = CType(CallingParameters, MyState).parentFileName
        End Sub

#End Region

#Region "Attributes"
        Private moFormatToPrintMenus As FormatToPrint
        Private moAryTrans As ArrayList
#End Region

#Region "Properties"

        Public ReadOnly Property FormatToPrintMenus() As FormatToPrint
            Get
                If moFormatToPrintMenus Is Nothing Then
                    moFormatToPrintMenus = CType(FindControl("moFormatToPrint"), FormatToPrint)
                End If
                Return moFormatToPrintMenus
            End Get
        End Property

#End Region

#Region "Handlers"

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub
        Protected WithEvents Label7 As System.Web.UI.WebControls.Label
        Protected WithEvents DealerLabel As System.Web.UI.WebControls.Label
        Protected WithEvents moReportCeInputControl As ReportExtractInputControl

        'NOTE: The following placeholder declaration is required by the Web Form Designer.
        'Do not delete or move it.

        'NOTE: The following placeholder declaration is required by the Web Form Designer.
        'Do not delete or move it.
        Private designerPlaceholderDeclaration As System.Object

        Private Sub Page_Init(sender As System.Object, e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

#Region "Handlers-Init"

        Private Sub InitializeForm()

            SetStateProperties()
            Dim InterfaceTypeCode As DealerFileProcessedData.InterfaceTypeCode = State.moInterfaceTypeCode

            lblEntireRecord.Visible = False
            moEntireRecordCheck.Visible = False
            lblIncludeBypassedRecords.Visible = False
            moInclBypassedRecCheck.Visible = False
            TheReportExtractInputControl.SetExportOnly()
            TheReportExtractInputControl.DestinationVisible = False
            If State.reportType.Equals(ERROR_EXPORT) Then

                If State.dealertype <> DealerType_VSC Then
                    lblEntireRecord.Visible = True
                    moEntireRecordCheck.Visible = True
                    lblIncludeBypassedRecords.Visible = True
                    moInclBypassedRecCheck.Visible = True
                    If (InterfaceTypeCode = DealerFileProcessedData.InterfaceTypeCode.PAYM) Then
                        UpdateBreadCrum(TAB_NAME_ERROR_EXPORT, PYMT_PAGETITLE)
                    Else
                        moInclBypassedRecCheck.Checked = True
                        UpdateBreadCrum(TAB_NAME_ERROR_EXPORT, CERT_PAGETITLE)
                    End If
                End If
            ElseIf State.reportType.Equals(PROCESSED_EXPORT) Then
                Select Case (InterfaceTypeCode)
                    Case DealerFileProcessedData.InterfaceTypeCode.PAYM
                        UpdateBreadCrum(TAB_NAME_PROCESSED_EXPORT, PYMT_PAGETITLE)
                    Case DealerFileProcessedData.InterfaceTypeCode.CERT
                        UpdateBreadCrum(TAB_NAME_PROCESSED_EXPORT, CERT_PAGETITLE)

                End Select
            Else
                Select Case InterfaceTypeCode
                    Case DealerFileProcessedData.InterfaceTypeCode.PAYM
                        UpdateBreadCrum(TAB_NAME_REJECT_REPORT, PYMT_PAGETITLE)
                        If State.dealertype <> DealerType_VSC Then
                            lblIncludeBypassedRecords.Visible = True
                            moInclBypassedRecCheck.Visible = True
                        End If
                    Case DealerFileProcessedData.InterfaceTypeCode.CERT
                        UpdateBreadCrum(TAB_NAME_REJECT_REPORT, CERT_PAGETITLE)
                        If State.dealertype <> DealerType_VSC Then
                            lblIncludeBypassedRecords.Visible = True
                            moInclBypassedRecCheck.Visible = True
                            moInclBypassedRecCheck.Checked = True
                            lblEntireRecord.Visible = True
                            moEntireRecordCheck.Visible = True
                        End If

                End Select
            End If
        End Sub

        Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
            Try
                MasterPage.MessageController.Clear_Hide()
                If Not IsPostBack Then
                    InitializeForm()
                End If
                InstallDisplayNewReportProgressBar()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
            ShowMissingTranslations(MasterPage.MessageController)
        End Sub

        Private Sub UpdateBreadCrum(TabName As String, Title As String)
            MasterPage.PageTab = TabName & ":"
            MasterPage.BreadCrum = MasterPage.PageTab & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage(Title)
            MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage(Title)

        End Sub
#End Region

#Region "Handlers-Buttons"

        Private Sub btnGenRpt_Click(sender As System.Object, e As System.EventArgs) Handles btnGenRpt.Click
            Try
                GenerateReport()
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnBack_Click(sender As System.Object, e As System.EventArgs) Handles btnBack.Click
            Dim retType As New DealerFileProcessedController_New.ReturnType(ElitaPlusPage.DetailPageCommand.Back, State.DealerfileProcessedId)
            ReturnToCallingPage(retType)
        End Sub

#End Region

#Region "Handlers-DroDown"
        Protected Sub OnFromDrop_Changed(sender As Object, e As System.EventArgs) _
                     Handles moReportCeInputControl.SelectedDestOptionChanged

            If moReportCeInputControl.DestinationCodeSelected = OPTION_FTP And State.dealertype = DealerType_VSC _
                And State.reportType.Equals(REJECT_REPORT) And Me.State.moInterfaceTypeCode = DealerFileProcessedData.InterfaceTypeCode.CERT Then
                moReportCeInputControl.UpdateFileNameControlVisible(True)
            Else
                moReportCeInputControl.UpdateFileNameControlVisible(False)
                moReportCeInputControl.ModifyFileNameChecked = False
            End If
        End Sub
#End Region

#End Region

#Region "Crystal Enterprise"

        Function GetReportParameters(DealerfileProcessedId As Guid) As ReportCeBaseForm.Params
            Dim reportFormat As ReportCeBaseForm.RptFormat = ReportCeBase.GetReportFormat(Me)
            Dim reportName As String = GetReportName(reportFormat)

            Dim isExportRptFormat As Boolean = (reportFormat = ReportCeBase.RptFormat.TEXT_TAB) OrElse (reportFormat = ReportCeBase.RptFormat.TEXT_CSV)
            Dim InterfaceTypeCode As DealerFileProcessedData.InterfaceTypeCode = State.moInterfaceTypeCode

            Dim params As New ReportCeBaseForm.Params
            Dim repParams() As ReportCeBaseForm.RptParam

            If reportFormat = ReportCeBase.RptFormat.TEXT_CSV Or reportFormat = ReportCeBase.RptFormat.TEXT_TAB Then
                culturevalue = MasterPage.ReportCeInputControl.getCultureValue(True)
            Else
                culturevalue = MasterPage.ReportCeInputControl.getCultureValue(False)
            End If

            Dim sIncludeByPassRecs As String = "Y"
            If moInclBypassedRecCheck.Visible Then
                If Not moInclBypassedRecCheck.Checked Then
                    sIncludeByPassRecs = "N"
                End If
            End If

            Select Case InterfaceTypeCode
                Case DealerFileProcessedData.InterfaceTypeCode.PAYM
                    If isExportRptFormat Then
                        Select Case State.reportType
                            Case REJECT_REPORT
                                'RejectedPayments-Exp_EN
                                repParams = New ReportCeBaseForm.RptParam() {
                                    New ReportCeBaseForm.RptParam("V_DEALERFILE_PROCESSED_ID", DALBase.GuidToSQLString(DealerfileProcessedId)),
                                    New ReportCeBaseForm.RptParam("V_LANGUAGE_ID", DALBase.GuidToSQLString(Thread.CurrentPrincipal.GetLanguageId())),
                                    New ReportCeBaseForm.RptParam("LANG_CULTURE_VALUE", culturevalue),
                                    New ReportCeBaseForm.RptParam("V_PARENT_FILE", State.isParentFile),
                                    New ReportCeBaseForm.RptParam("V_FILE_NAME", State.parentFileName),
                                    New ReportCeBaseForm.RptParam("V_INCLUDE_BYPASS", sIncludeByPassRecs)}
                            Case ERROR_EXPORT
                                'RejectedPayments_ErrorExp                                
                                repParams = New ReportCeBaseForm.RptParam() {
                                    New ReportCeBaseForm.RptParam("V_DEALERFILE_PROCESSED_ID", DALBase.GuidToSQLString(DealerfileProcessedId)),
                                    New ReportCeBaseForm.RptParam("V_LANGUAGE_ID", DALBase.GuidToSQLString(Thread.CurrentPrincipal.GetLanguageId())),
                                    New ReportCeBaseForm.RptParam("LANG_CULTURE_VALUE", culturevalue),
                                    New ReportCeBaseForm.RptParam("V_PARENT_FILE", State.isParentFile),
                                    New ReportCeBaseForm.RptParam("V_FILE_NAME", State.parentFileName),
                                    New ReportCeBaseForm.RptParam("V_INCLUDE_BYPASS", sIncludeByPassRecs)}
                            Case PROCESSED_EXPORT
                                'ProcessedPayments-Exp_EN
                                repParams = New ReportCeBaseForm.RptParam() {
                                    New ReportCeBaseForm.RptParam("LANG_CULTURE_VALUE", culturevalue),
                                    New ReportCeBaseForm.RptParam("V_DEALERFILE_PROCESSED_ID", DALBase.GuidToSQLString(DealerfileProcessedId)),
                                    New ReportCeBaseForm.RptParam("V_PARENT_FILE", State.isParentFile),
                                    New ReportCeBaseForm.RptParam("V_FILE_NAME", State.parentFileName)}
                                ',New ReportCeBaseForm.RptParam("V_INCLUDE_BYPASS", sIncludeByPassRecs)
                        End Select
                    Else
                        'RejectedPayments_EN
                        repParams = New ReportCeBaseForm.RptParam() {
                            New ReportCeBaseForm.RptParam("V_DEALERFILE_PROCESSED_ID", DALBase.GuidToSQLString(DealerfileProcessedId)),
                            New ReportCeBaseForm.RptParam("V_LANGUAGE_ID", DALBase.GuidToSQLString(Thread.CurrentPrincipal.GetLanguageId())),
                            New ReportCeBaseForm.RptParam("LANG_CULTURE_VALUE", culturevalue),
                            New ReportCeBaseForm.RptParam("V_PARENT_FILE", State.isParentFile),
                            New ReportCeBaseForm.RptParam("V_FILE_NAME", State.parentFileName),
                            New ReportCeBaseForm.RptParam("V_INCLUDE_BYPASS", sIncludeByPassRecs)}
                    End If

                Case DealerFileProcessedData.InterfaceTypeCode.CERT
                    If isExportRptFormat Then
                        Select Case State.reportType
                            Case REJECT_REPORT
                                'RejectedCertificates-Exp_EN
                                repParams = New ReportCeBaseForm.RptParam() {
                                    New ReportCeBaseForm.RptParam("V_DEALERFILE_PROCESSED_ID", DALBase.GuidToSQLString(DealerfileProcessedId)),
                                    New ReportCeBaseForm.RptParam("V_DEALERTYPE", State.dealertype),
                                    New ReportCeBaseForm.RptParam("V_LANGUAGE_ID", DALBase.GuidToSQLString(Thread.CurrentPrincipal.GetLanguageId())),
                                    New ReportCeBaseForm.RptParam("LANG_CULTURE_VALUE", culturevalue),
                                    New ReportCeBaseForm.RptParam("V_PARENT_FILE", State.isParentFile),
                                    New ReportCeBaseForm.RptParam("V_FILE_NAME", State.parentFileName),
                                    New ReportCeBaseForm.RptParam("V_INCLUDE_BYPASS", sIncludeByPassRecs)}
                            Case ERROR_EXPORT
                                'RejectedCertificates_ErrorExp
                                Dim sEntireRecordOnly As String = "N"
                                If moEntireRecordCheck.Visible Then
                                    If moEntireRecordCheck.Checked Then
                                        sEntireRecordOnly = "Y"
                                    End If
                                End If
                                repParams = New ReportCeBaseForm.RptParam() {
                                    New ReportCeBaseForm.RptParam("V_DEALERFILE_PROCESSED_ID", DALBase.GuidToSQLString(DealerfileProcessedId)),
                                    New ReportCeBaseForm.RptParam("V_DEALERTYPE", State.dealertype),
                                    New ReportCeBaseForm.RptParam("V_LANGUAGE_ID", DALBase.GuidToSQLString(Thread.CurrentPrincipal.GetLanguageId())),
                                    New ReportCeBaseForm.RptParam("V_ENTIRE_RECORD_ONLY", sEntireRecordOnly),
                                    New ReportCeBaseForm.RptParam("V_PARENT_FILE", State.isParentFile),
                                    New ReportCeBaseForm.RptParam("V_FILE_NAME", State.parentFileName),
                                    New ReportCeBaseForm.RptParam("V_INCLUDE_BYPASS", sIncludeByPassRecs)}
                            Case PROCESSED_EXPORT
                                'ProcessedCertificates-Exp_EN
                                repParams = New ReportCeBaseForm.RptParam() {
                                    New ReportCeBaseForm.RptParam("V_DEALERFILE_PROCESSED_ID", DALBase.GuidToSQLString(DealerfileProcessedId)),
                                    New ReportCeBaseForm.RptParam("LANG_CULTURE_VALUE", culturevalue),
                                    New ReportCeBaseForm.RptParam("V_PARENT_FILE", State.isParentFile),
                                    New ReportCeBaseForm.RptParam("V_FILE_NAME", State.parentFileName)}
                        End Select
                    Else
                        'RejectedCertificates_EN
                        repParams = New ReportCeBaseForm.RptParam() {
                            New ReportCeBaseForm.RptParam("V_DEALERFILE_PROCESSED_ID", DALBase.GuidToSQLString(DealerfileProcessedId)),
                            New ReportCeBaseForm.RptParam("V_DEALERTYPE", State.dealertype),
                            New ReportCeBaseForm.RptParam("V_LANGUAGE_ID", DALBase.GuidToSQLString(Thread.CurrentPrincipal.GetLanguageId())),
                            New ReportCeBaseForm.RptParam("LANG_CULTURE_VALUE", culturevalue),
                            New ReportCeBaseForm.RptParam("V_PARENT_FILE", State.isParentFile),
                            New ReportCeBaseForm.RptParam("V_FILE_NAME", State.parentFileName),
                            New ReportCeBaseForm.RptParam("V_INCLUDE_BYPASS", sIncludeByPassRecs)}
                    End If
            End Select

            With params
                .msRptName = reportName
                .moRptFormat = reportFormat
                .moAction = ReportCeBaseForm.RptAction.SCHEDULE_VIEW
                .moRptParams = repParams

                If State.reportType.Equals(ERROR_EXPORT) Or State.reportType.Equals(PROCESSED_EXPORT) Then
                    .msCsvDelimiter = ReportCeBaseForm.CsvDelimiter.CSV_DELIMITER_DQUOTE
                    .msCsvSeparator = ReportCeBaseForm.CsvSeparator.CSV_SEPARATOR_COMMA
                End If

                If (State.iscultureImplemented) Then
                    .msRptWindowName = reportFileNameWindow
                Else
                    .msRptWindowName = TranslationBase.TranslateLabelOrMessage(reportFileNameWindow)
                End If
            End With

            Return params

        End Function

        Function GetReportName(rptFormat As ReportCeBaseForm.RptFormat) As String
            Dim isExportRptFormat As Boolean = (rptFormat = ReportCeBase.RptFormat.TEXT_TAB) OrElse (rptFormat = ReportCeBase.RptFormat.TEXT_CSV)
            Dim InterfaceTypeCode As DealerFileProcessedData.InterfaceTypeCode = State.moInterfaceTypeCode

            Select Case InterfaceTypeCode
                Case DealerFileProcessedData.InterfaceTypeCode.PAYM
                    If isExportRptFormat Then
                        If State.reportType.Equals(REJECT_REPORT) Then
                            State.iscultureImplemented = True
                            'RejectedPayments-Exp_EN
                            reportName = MasterPage.ReportCeInputControl.getReportName(RPT_REJECTED_PAYMENTS_FILENAME_EXP, True)
                            reportFileNameWindow = RPT_FILENAME_WINDOW_PAYMENTS
                        ElseIf State.reportType.Equals(ERROR_EXPORT) Then
                            'RejectedPayments_ErrorExp
                            reportName = RPT_REJECTED_PAYMENTS_ERROR_EXP
                            reportFileNameWindow = RPT_FILENAME_WINDOW_PAYMENTS_ERROR
                        ElseIf State.reportType.Equals(PROCESSED_EXPORT) Then
                            State.iscultureImplemented = True
                            'ProcessedPayments-Exp_EN
                            reportName = MasterPage.ReportCeInputControl.getReportName(RPT_PROCESSED_PAYMENTS_FILENAME_EXP, True)
                            reportFileNameWindow = RPT_FILENAME_WINDOW_PROCESSED_PAYMENTS
                        End If
                    Else
                        State.iscultureImplemented = True
                        'RejectedPayments_EN
                        reportName = MasterPage.ReportCeInputControl.getReportName(RPT_REJECTED_PAYMENTS_FILENAME, False)
                        reportFileNameWindow = RPT_FILENAME_WINDOW_PAYMENTS
                    End If

                Case DealerFileProcessedData.InterfaceTypeCode.CERT
                    reportFileNameWindow = RPT_FILENAME_WINDOW_CERTIFICATES
                    If isExportRptFormat Then
                        If State.reportType.Equals(REJECT_REPORT) Then
                            State.iscultureImplemented = True
                            'RejectedCertificates-Exp_EN
                            reportName = MasterPage.ReportCeInputControl.getReportName(RPT_REJECTED_CERTIFICATES_FILENAME_EXP, True)
                            MasterPage.ReportCeInputControl.SetModifiedFileName = State.SelectionCode
                        ElseIf State.reportType.Equals(ERROR_EXPORT) Then
                            'RejectedCertificates_ErrorExp
                            reportName = RPT_REJECTED_CERTIFICATES_ERROR_FILENAME
                            reportFileNameWindow = RPT_FILENAME_WINDOW_CERTIFICATES_ERROR
                        ElseIf State.reportType.Equals(PROCESSED_EXPORT) Then
                            State.iscultureImplemented = True
                            'ProcessedCertificates-Exp_EN
                            reportName = MasterPage.ReportCeInputControl.getReportName(RPT_PROCESSED_CERTIFICATES_FILENAME_EXP, True)
                            reportFileNameWindow = RPT_FILENAME_WINDOW_PROCESSED_CERTIFICATES
                        End If
                    Else
                        State.iscultureImplemented = True
                        'RejectedCertificates_EN
                        reportName = MasterPage.ReportCeInputControl.getReportName(RPT_REJECTED_CERTIFICATES_FILENAME, False)
                        MasterPage.ReportCeInputControl.SetModifiedFileName = State.SelectionCode
                    End If
            End Select
            Return reportName
        End Function

        Private Sub GenerateReport()

            Dim DealerfileProcessedId As Guid = State.DealerfileProcessedId

            'If QueryString Is Nothing
            '    ReportCeBase.EnableReportCe(Me, MasterPage.ReportCeInputControl)
            '    Dim params As ReportCeBaseForm.Params = GetReportParameters(DealerfileProcessedId)
            '    Session(ReportCeBaseForm.SESSION_PARAMETERS_KEY) = params
            'Else

            Dim InterfaceTypeCode As DealerFileProcessedData.InterfaceTypeCode = State.moInterfaceTypeCode

            Dim sIncludeByPassRecs As String = "Y"
            If moInclBypassedRecCheck.Visible Then
                If Not moInclBypassedRecCheck.Checked Then
                    sIncludeByPassRecs = "N"
                End If
            End If

            Dim reportParams As New System.Text.StringBuilder

            State.MyBO = New ReportRequests
            State.ForEdit = True

            'Me.PopulateBOProperty(Me.State.MyBO, "ReportProc", "R_TIM_CERTIFICATE_EXTRACT.Report")

            Select Case InterfaceTypeCode
                Case DealerFileProcessedData.InterfaceTypeCode.PAYM
                    reportParams.AppendFormat("V_DEALERFILE_PROCESSED_ID=> '{0}',", DALBase.GuidToSQLString(DealerfileProcessedId))
                    reportParams.AppendFormat("V_LANGUAGE_ID => '{0}',", DALBase.GuidToSQLString(Thread.CurrentPrincipal.GetLanguageId()))
                    reportParams.AppendFormat("V_PARENT_FILE => '{0}',", State.isParentFile)
                    reportParams.AppendFormat("V_FILE_NAME => '{0}',", State.parentFileName)
                    Select Case State.reportType
                        Case REJECT_REPORT
                            'RejectedPayments-Exp_EN
                            reportParams.AppendFormat("V_INCLUDE_BYPASS => '{0}',", sIncludeByPassRecs)
                            reportParams.AppendFormat("V_REPORT_TYPE => '{0}'", "PAYMENT_REJECT_EXPORT")
                            PopulateBOProperty(State.MyBO, "ReportType", "PAYMENT_REJECT_EXPORT")
                            PopulateBOProperty(State.MyBO, "ReportProc", "REJECTEDPAYMENTSRPT.Oracle_Export")
                        Case ERROR_EXPORT
                            'RejectedPayments_ErrorExp  
                            reportParams.AppendFormat("V_INCLUDE_BYPASS => '{0}',", sIncludeByPassRecs)
                            reportParams.AppendFormat("V_REPORT_TYPE => '{0}'", "PAYMENT_ERROR_EXPORT")
                            PopulateBOProperty(State.MyBO, "ReportType", "PAYMENT_ERROR_EXPORT")
                            PopulateBOProperty(State.MyBO, "ReportProc", "REJECTEDPAYMENTSRPT.Oracle_Export")
                        Case PROCESSED_EXPORT
                            'ProcessedPayments-Exp_EN
                            reportParams.AppendFormat("V_REPORT_TYPE => '{0}'", "PAYMENT_LOAD_EXPORT")
                            PopulateBOProperty(State.MyBO, "ReportType", "PAYMENT_LOAD_EXPORT")
                            PopulateBOProperty(State.MyBO, "ReportProc", "R_ProcessedPayments.Oracle_Export")
                    End Select


                Case DealerFileProcessedData.InterfaceTypeCode.CERT
                    Dim sEntireRecordOnly As String = "N"
                    reportParams.AppendFormat("V_DEALERFILE_PROCESSED_ID=> '{0}',", DALBase.GuidToSQLString(DealerfileProcessedId))
                    reportParams.AppendFormat("V_PARENT_FILE => '{0}',", State.isParentFile)
                    reportParams.AppendFormat("V_FILE_NAME => '{0}',", State.parentFileName)
                    reportParams.AppendFormat("V_DEALERTYPE => '{0}',", State.dealertype)

                    Select Case State.reportType
                        Case REJECT_REPORT
                            'RejectedCertificates-Exp_EN
                            reportParams.AppendFormat("V_LANGUAGE_ID => '{0}',", DALBase.GuidToSQLString(Thread.CurrentPrincipal.GetLanguageId()))
                            reportParams.AppendFormat("V_INCLUDE_BYPASS => '{0}',", sIncludeByPassRecs)
                            reportParams.AppendFormat("V_ENTIRE_RECORD_ONLY => '{0}',", sEntireRecordOnly)
                            reportParams.AppendFormat("V_REPORT_TYPE => '{0}'", "CERT_REJECT_REPORT")
                            PopulateBOProperty(State.MyBO, "ReportType", "CERT_REJECT_REPORT")
                            PopulateBOProperty(State.MyBO, "ReportProc", "REJECTEDCERTIFICATESRPT.Oracle_Export")
                        Case ERROR_EXPORT
                            'RejectedCertificates_ErrorExp
                            If moEntireRecordCheck.Visible Then
                                If moEntireRecordCheck.Checked Then
                                    sEntireRecordOnly = "Y"
                                End If
                            End If
                            reportParams.AppendFormat("V_LANGUAGE_ID => '{0}',", DALBase.GuidToSQLString(Thread.CurrentPrincipal.GetLanguageId()))
                            reportParams.AppendFormat("V_INCLUDE_BYPASS => '{0}',", sIncludeByPassRecs)
                            reportParams.AppendFormat("V_ENTIRE_RECORD_ONLY => '{0}',", sEntireRecordOnly)
                            reportParams.AppendFormat("V_REPORT_TYPE => '{0}'", "CERT_ERROR_EXPORT")
                            PopulateBOProperty(State.MyBO, "ReportType", "CERT_ERROR_EXPORT")
                            PopulateBOProperty(State.MyBO, "ReportProc", "REJECTEDCERTIFICATESRPT.Oracle_Export")
                        Case PROCESSED_EXPORT
                            'ProcessedCertificates-Exp_EN
                            reportParams.AppendFormat("V_REPORT_TYPE => '{0}'", "CERT_LOAD_EXPORT")
                            PopulateBOProperty(State.MyBO, "ReportType", "CERT_LOAD_EXPORT")
                            PopulateBOProperty(State.MyBO, "ReportProc", "PROCESSEDCERTIFICATESRPT.Oracle_Export")
                    End Select


            End Select

            PopulateBOProperty(State.MyBO, "ReportParameters", reportParams.ToString())
            PopulateBOProperty(State.MyBO, "UserEmailAddress", ElitaPlusIdentity.Current.EmailAddress)
            'End If

            ScheduleReport()

        End Sub

        Private Sub ScheduleReport()
            Try
                Dim scheduleDate As DateTime = TheReportExtractInputControl.GetSchedDate()
                If State.MyBO.IsDirty Then
                    State.MyBO.Save()

                    State.IsNew = False
                    State.HasDataChanged = True
                    State.MyBO.CreateJob(scheduleDate)

                    If String.IsNullOrEmpty(ElitaPlusIdentity.Current.EmailAddress) Then
                        DisplayMessage(Message.MSG_Email_not_configured, "", MSG_BTN_OK, MSG_TYPE_ALERT, , True)
                    Else
                        DisplayMessage(Message.MSG_REPORT_REQUEST_IS_GENERATED, "", MSG_BTN_OK, MSG_TYPE_ALERT, , True)
                    End If

                    'btnGenRpt.Enabled = False

                End If

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

#End Region

    End Class

End Namespace