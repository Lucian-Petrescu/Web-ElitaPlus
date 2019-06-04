Imports Assurant.ElitaPlus.ElitaPlusWebApp.Generic
Imports Assurant.ElitaPlus.ElitaPlusWebApp.Interfaces
Imports Assurant.ElitaPlus.DALObjects
Imports System.Threading
Imports Assurant.ElitaPlus.Security

Namespace Reports

    Partial Class PrintClaimLoadRejectForm
        Inherits ElitaPlusPage

#Region "Constants"
        Private Const RPT_FILENAME_WINDOW_NEW_CLAIMS As String = "Rejected New Claims"
        Private Const RPT_FILENAME_WINDOW_NEW_CLAIMS_HP As String = "Rejected Hp New Claims"
        Private Const RPT_FILENAME_WINDOW_CLOSE_CLAIMS As String = "Rejected Close Claims"
        Private Const RPT_FILENAME_WINDOW_CLOSE_CLAIMS_SUNCOM As String = "Rejected SunCom Close Claims"
        Private Const RPT_FILENAME_WINDOW_PROCESSED_CLOSE_CLAIMS_SUNCOM As String = "Processed Closed Claims"
        Private Const RPT_FILENAME_WINDOW_PROCESSED_EXPORT As String = "Processed Export"
        Private Const RPT_FILENAME_WINDOW_REJECT_REPORT As String = "Reject Report"

        Private Const RPT_FILENAME_WINDOW_CLAIM_SUSPENSE As String = "Claim Suspense"
        Private Const RPT_REJECTED_NEW_CLAIMS_FILENAME As String = "RejectedNewClaims"
        Private Const RPT_REJECTED_NEW_CLAIMS_HP As String = "RejectedHpNewClaims"
        Private Const RPT_REJECTED_CLOSE_CLAIMS_FILENAME As String = "RejectedCloseClaims"
        Private Const RPT_REJECTED_CLOSE_CLAIMS_SUNCOM As String = "RejectedSunComCloseClaims"
        Private Const RPT_PROCESSED_CLOSE_CLAIMS_SUNCOM As String = "ProcessedSuncomClosedClaims-Exp"
        Private Const RPT_REJECTED_NEW_CLAIMS_FILENAME_EXP As String = "RejectedNewClaims_exp"
        Private Const RPT_REJECTED_NEW_CLAIMS_HP_EXP As String = "RejectedHpNewClaims_exp"
        Private Const RPT_REJECTED_CLOSE_CLAIMS_FILENAME_EXP As String = "RejectedCloseClaims_exp"
        Private Const RPT_REJECTED_CLOSE_CLAIMS_SUNCOM_EXP As String = "RejectedSunComCloseClaims_exp"
        Private Const RPT_REJECTED_SUSPENSE_EXP As String = "ClaimSuspense_Exp"
        Private Const RPT_REJECTED_SUSPENSE As String = "ClaimSuspense"
        Private Const RPT_FILENAME_EXPORT As String = "RejectedNewClaims_Exp"

        Private Const RPT_FILENAME_WINDOW_CLAIM_FILE As String = "CLAIM_FILE"
        Private Const RPT_FILENAME_WINDOW_INVOICE_FILE As String = "INVOICE_FILE"
        Private Const RPT_FILENAME_REJECTED_CLAIMLOAD As String = "RejectedClaimLoad_EN"
        Private Const RPT_FILENAME_REJECTED_INVOICELOAD As String = "RejectedInvoiceLoad_EN"
        'DEF 2162 Changing name from RejectedClaimLoad_EN_exp to RejectedClaimLoad_Exp_EN
        Private Const RPT_FILENAME_REJECTED_CLAIMLOAD_EXP As String = "RejectedClaimLoad-Exp_EN"
        Private Const RPT_FILENAME_REJECTED_INVOICELOAD_EXP As String = "RejectedInvoiceLoad-Exp_EN"

        Private Const ALL As String = "*"
        Private Const PARSED_ALL As String = "%"
        Private Const SP_DATE_FORMAT As String = "yyyyMMdd"
        Private Const YES As String = "Y"
        Private Const NO As String = "N"
        Public Const MAX_ROWS As String = "500"
        Public Const MAX_ROW_OVERRIDE As String = "0"

        Public Shared URL As String = ELPWebConstants.APPLICATION_PATH & "/Reports/PrintClaimLoadRejectForm.aspx"

        Public Const REJECT_REPORT As Integer = 0
        Public Const ERROR_EXPORT As Integer = 1
        Public Const PROCESSED_EXPORT As Integer = 2


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
            Public ClaimfileProcessedId As Guid
            Public moInterfaceTypeCode As ClaimFileProcessedData.InterfaceTypeCode
            Public SearchCertificate As String
            Public SearchFilename As String
            Public reportType As Integer
            Public SearchAuthorization As String
            Public iscultureImplemented As Boolean
            Public MyBO As ReportRequests
            Public IsNew As Boolean = False
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
            Me.State.moInterfaceTypeCode = CType(CType(Me.CallingParameters, MyState).moInterfaceTypeCode, ClaimFileProcessedData.InterfaceTypeCode)
            Me.State.reportType = CType(Me.CallingParameters, MyState).reportType
            If Me.State.moInterfaceTypeCode = ClaimFileProcessedData.InterfaceTypeCode.CLAIM_SUSPENSE Then
                Me.State.SearchCertificate = CType(CType(Me.CallingParameters, MyState).SearchCertificate, String).Replace(ALL, PARSED_ALL)
                Me.State.SearchAuthorization = CType(CType(Me.CallingParameters, MyState).SearchAuthorization, String).Replace(ALL, PARSED_ALL)
                Me.State.SearchFilename = CType(CType(Me.CallingParameters, MyState).SearchFilename, String).Replace(ALL, PARSED_ALL)
            Else
                Me.State.ClaimfileProcessedId = CType(CType(Me.CallingParameters, MyState).ClaimfileProcessedId, Guid)
            End If
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

        Public ReadOnly Property TheRptCeInputControl() As ReportExtractInputControl
            Get
                If moReportCeInputControl Is Nothing Then
                    moReportCeInputControl = CType(FindControl("moReportCeInputControl"), ReportExtractInputControl)
                End If
                Return moReportCeInputControl
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
        Protected WithEvents ErrorCtrl As ErrorController
        Protected WithEvents moReportCeInputControl As ReportExtractInputControl

        'NOTE: The following placeholder declaration is required by the Web Form Designer.
        'Do not delete or move it.

        'NOTE: The following placeholder declaration is required by the Web Form Designer.
        'Do not delete or move it.
        Private designerPlaceholderDeclaration As System.Object

        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

#Region "Handlers-Init"

        Private Sub InitializeForm()
            SetStateProperties()
            Dim InterfaceTypeCode As ClaimFileProcessedData.InterfaceTypeCode = Me.State.moInterfaceTypeCode
            TheRptCeInputControl.SetExportOnly()
            TheRptCeInputControl.DestinationVisible = False
            If Me.State.reportType.Equals(PROCESSED_EXPORT) Then
                Select Case InterfaceTypeCode
                    Case ClaimFileProcessedData.InterfaceTypeCode.CLOSE_CLAIM_SUNCOM
                    Case ClaimFileProcessedData.InterfaceTypeCode.CLOSE_CLAIM
                        Me.moTitleCloseClaims.Visible = True
                        'TheRptCeInputControl.populateReportLanguages(RPT_PROCESSED_CLOSE_CLAIMS_SUNCOM)
                        'reportName = TheRptCeInputControl.getReportName(RPT_PROCESSED_CLOSE_CLAIMS_SUNCOM, True)
                        'Me.HeadTitle.InnerText = TheRptCeInputControl.getReportWindowTitle(RPT_FILENAME_WINDOW_PROCESSED_EXPORT)
                        LabelReports.Text = RPT_FILENAME_WINDOW_PROCESSED_EXPORT
                End Select
            Else
                Me.HeadTitle.InnerText = RPT_FILENAME_WINDOW_REJECT_REPORT
                Select Case InterfaceTypeCode
                    Case ClaimFileProcessedData.InterfaceTypeCode.NEW_CLAIM
                        Me.moTitleNewClaims.Visible = True
                        reportName = RPT_REJECTED_NEW_CLAIMS_FILENAME
                    Case ClaimFileProcessedData.InterfaceTypeCode.NEW_CLAIM_HP
                        Me.moTitleNewClaims.Visible = True
                        reportName = RPT_REJECTED_NEW_CLAIMS_HP
                    Case ClaimFileProcessedData.InterfaceTypeCode.CLOSE_CLAIM
                        Me.moTitleCloseClaims.Visible = True
                        reportName = RPT_REJECTED_CLOSE_CLAIMS_FILENAME
                    Case ClaimFileProcessedData.InterfaceTypeCode.CLOSE_CLAIM_SUNCOM
                        Me.moTitleCloseClaims.Visible = True
                        reportName = RPT_REJECTED_CLOSE_CLAIMS_SUNCOM
                    Case ClaimFileProcessedData.InterfaceTypeCode.CLAIM_SUSPENSE
                        Me.moTitleClaims.Visible = True
                        reportName = RPT_REJECTED_SUSPENSE
                    Case ClaimFileProcessedData.InterfaceTypeCode.CLAIM_LOAD_COMMON
                        Me.moTitleClaimFile.Visible = True
                        reportName = RPT_FILENAME_REJECTED_CLAIMLOAD
                    Case ClaimFileProcessedData.InterfaceTypeCode.INVOICE_LOAD_COMMON
                        Me.moTitleInvoiceFile.Visible = True
                        reportName = RPT_FILENAME_REJECTED_INVOICELOAD
                End Select
            End If
        End Sub

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            Try
                Me.ErrorCtrl.Clear_Hide()
                If Not IsPostBack Then
                    InitializeForm()
                End If
                Me.InstallProgressBar()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try
            Me.ShowMissingTranslations(Me.ErrorCtrl)
        End Sub

#End Region

#Region "Handlers-Buttons"

        Private Sub btnGenRpt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenRpt.Click
            Try
                GenerateReport()
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try
        End Sub

        Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
            If Me.State.moInterfaceTypeCode = ClaimFileProcessedData.InterfaceTypeCode.CLAIM_SUSPENSE Then
                Dim retType As New ClaimSuspenseReconForm.ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.SearchAuthorization, Me.State.SearchFilename, Me.State.SearchCertificate)
                Me.ReturnToCallingPage(retType)
            ElseIf State.moInterfaceTypeCode = ClaimFileProcessedData.InterfaceTypeCode.CLAIM_LOAD_COMMON Or State.moInterfaceTypeCode = ClaimFileProcessedData.InterfaceTypeCode.INVOICE_LOAD_COMMON Then
                Dim retType As New ClaimLoadForm.ReturnType(ElitaPlusPage.DetailPageCommand.Back, State.ClaimfileProcessedId)
                Me.ReturnToCallingPage(retType)
            Else
                Dim retType As New ClaimFileProcessedController.ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.ClaimfileProcessedId)
                Me.ReturnToCallingPage(retType)
            End If

        End Sub

#Region "Handlers-DroDown"
        Protected Sub OnFromDrop_Changed(ByVal sender As Object, ByVal e As System.EventArgs) _
                     Handles moReportCeInputControl.SelectedDestOptionChanged

            moReportCeInputControl.UpdateFileNameControlVisible(False)
            moReportCeInputControl.ModifyFileNameChecked = False
        End Sub
#End Region

#End Region

#End Region

#Region "Crystal Enterprise"

        Private Sub SetReportName(ByVal rptFormat As ReportCeBaseForm.RptFormat)
            Dim isExportRptFormat As Boolean = (rptFormat = ReportCeBase.RptFormat.TEXT_TAB) OrElse (rptFormat = ReportCeBase.RptFormat.TEXT_CSV)
            Dim InterfaceTypeCode As ClaimFileProcessedData.InterfaceTypeCode = Me.State.moInterfaceTypeCode
            If Me.State.reportType.Equals(PROCESSED_EXPORT) Then
                Select Case InterfaceTypeCode
                    Case ClaimFileProcessedData.InterfaceTypeCode.CLOSE_CLAIM_SUNCOM
                    Case ClaimFileProcessedData.InterfaceTypeCode.CLOSE_CLAIM
                        reportName = TheRptCeInputControl.getReportName(RPT_PROCESSED_CLOSE_CLAIMS_SUNCOM, True)
                End Select
                Me.State.iscultureImplemented = True
                reportFileNameWindow = RPT_FILENAME_WINDOW_PROCESSED_CLOSE_CLAIMS_SUNCOM
            Else
                Select Case InterfaceTypeCode
                    Case ClaimFileProcessedData.InterfaceTypeCode.NEW_CLAIM
                        If isExportRptFormat Then
                            reportName = RPT_REJECTED_NEW_CLAIMS_FILENAME_EXP
                        Else
                            reportName = RPT_REJECTED_NEW_CLAIMS_FILENAME
                        End If
                        reportFileNameWindow = RPT_FILENAME_WINDOW_NEW_CLAIMS
                    Case ClaimFileProcessedData.InterfaceTypeCode.NEW_CLAIM_HP
                        If isExportRptFormat Then
                            reportName = RPT_REJECTED_NEW_CLAIMS_HP_EXP
                        Else
                            reportName = RPT_REJECTED_NEW_CLAIMS_HP
                        End If
                        reportFileNameWindow = RPT_FILENAME_WINDOW_NEW_CLAIMS_HP
                    Case ClaimFileProcessedData.InterfaceTypeCode.CLOSE_CLAIM
                        If isExportRptFormat Then
                            reportName = RPT_REJECTED_CLOSE_CLAIMS_FILENAME_EXP
                        Else
                            reportName = RPT_REJECTED_CLOSE_CLAIMS_FILENAME
                        End If
                        reportFileNameWindow = RPT_FILENAME_WINDOW_CLOSE_CLAIMS
                    Case ClaimFileProcessedData.InterfaceTypeCode.CLOSE_CLAIM_SUNCOM
                        If isExportRptFormat Then
                            reportName = RPT_REJECTED_CLOSE_CLAIMS_SUNCOM_EXP
                        Else
                            reportName = RPT_REJECTED_CLOSE_CLAIMS_SUNCOM
                        End If
                        reportFileNameWindow = RPT_FILENAME_WINDOW_CLOSE_CLAIMS_SUNCOM
                    Case ClaimFileProcessedData.InterfaceTypeCode.CLAIM_SUSPENSE
                        If isExportRptFormat Then
                            reportName = RPT_REJECTED_SUSPENSE_EXP
                        Else
                            reportName = RPT_REJECTED_SUSPENSE
                        End If
                        reportFileNameWindow = RPT_FILENAME_WINDOW_CLAIM_SUSPENSE
                    Case ClaimFileProcessedData.InterfaceTypeCode.CLAIM_LOAD_COMMON
                        If isExportRptFormat Then
                            reportName = RPT_FILENAME_REJECTED_CLAIMLOAD_EXP
                        Else
                            reportName = RPT_FILENAME_REJECTED_CLAIMLOAD
                        End If
                        reportFileNameWindow = RPT_FILENAME_WINDOW_CLAIM_FILE
                    Case ClaimFileProcessedData.InterfaceTypeCode.INVOICE_LOAD_COMMON
                        If isExportRptFormat Then
                            reportName = RPT_FILENAME_REJECTED_INVOICELOAD_EXP
                        Else
                            reportName = RPT_FILENAME_REJECTED_INVOICELOAD
                        End If
                        reportFileNameWindow = RPT_FILENAME_WINDOW_INVOICE_FILE
                End Select
            End If
        End Sub

        Function setparameters(ByVal claimfileprocessedid As Guid) As ReportCeBaseForm.Params

            Dim reportformat As ReportCeBaseForm.RptFormat
            Dim params As New ReportCeBaseForm.Params
            Dim repparams() As ReportCeBaseForm.RptParam

            reportformat = ReportCeBase.GetReportFormat(Me)
            SetReportName(reportformat)
            If (Me.State.iscultureImplemented) Then

                If reportformat = ReportCeBase.RptFormat.TEXT_CSV Or reportformat = ReportCeBase.RptFormat.TEXT_TAB Then
                    Me.culturevalue = TheRptCeInputControl.getCultureValue(True)
                Else
                    Me.culturevalue = TheRptCeInputControl.getCultureValue(False)
                End If

                repparams = New ReportCeBaseForm.RptParam() _
                                               {
                                                New ReportCeBaseForm.RptParam("v_claimfile_processed_id", DALBase.GuidToSQLString(claimfileprocessedid)),
                                                New ReportCeBaseForm.RptParam("lang_culture_value", culturevalue)}
                With params
                    .msRptName = reportName
                    .msRptWindowName = TranslationBase.TranslateLabelOrMessage(reportFileNameWindow)
                    .moRptFormat = reportformat
                    .msCsvDelimiter = ReportCeBaseForm.CsvDelimiter.CSV_DELIMITER_DQUOTE
                    .msCsvSeparator = ReportCeBaseForm.CsvSeparator.CSV_SEPARATOR_COMMA
                    .moAction = ReportCeBaseForm.RptAction.SCHEDULE_VIEW
                    .moRptParams = repparams
                End With

            Else
                If State.moInterfaceTypeCode = ClaimFileProcessedData.InterfaceTypeCode.CLAIM_LOAD_COMMON Or State.moInterfaceTypeCode = ClaimFileProcessedData.InterfaceTypeCode.INVOICE_LOAD_COMMON Then
                    repparams = New ReportCeBaseForm.RptParam() {New ReportCeBaseForm.RptParam("v_claimload_file_processed_id", DALBase.GuidToSQLString(claimfileprocessedid))}
                Else
                    repparams = New ReportCeBaseForm.RptParam() {New ReportCeBaseForm.RptParam("v_claimfile_processed_id", DALBase.GuidToSQLString(claimfileprocessedid))}
                End If

                With params
                    .msRptName = reportName
                    .msRptWindowName = TranslationBase.TranslateLabelOrMessage(reportFileNameWindow)
                    .moRptFormat = reportformat
                    .moAction = ReportCeBaseForm.RptAction.SCHEDULE_VIEW
                    .moRptParams = repparams
                End With
            End If
            Return params
        End Function

        'Function setparameters() As ReportCeBaseForm.Params

        '    Dim reportformat As ReportCeBaseForm.RptFormat
        '    Dim params As New ReportCeBaseForm.Params
        '    Dim repparams() As ReportCeBaseForm.RptParam
        '    Dim _maxrows As String

        '    reportformat = ReportCeBase.GetReportFormat(Me)
        '    SetReportName(reportformat)

        '    'alr - 12/30/2008 - ticket #1,620,062 - override maxrows if a filename is entered.
        '    If Me.State.moInterfaceTypeCode = ClaimFileProcessedData.InterfaceTypeCode.CLAIM_SUSPENSE AndAlso Me.State.SearchFilename IsNot Nothing AndAlso Me.State.SearchFilename.Trim.Length > 0 Then
        '        _maxrows = MAX_ROW_OVERRIDE
        '    Else
        '        _maxrows = MAX_ROWS
        '    End If

        '    repparams = New ReportCeBaseForm.RptParam() {New ReportCeBaseForm.RptParam("v_user_id", DALBase.GuidToSQLString(ElitaPlusIdentity.Current.ActiveUser.Id)),
        '                                                     New ReportCeBaseForm.RptParam("v_certificate_number", Me.State.SearchCertificate),
        '                                                     New ReportCeBaseForm.RptParam("v_authorization_number", Me.State.SearchAuthorization),
        '                                                     New ReportCeBaseForm.RptParam("v_filename", Me.State.SearchFilename),
        '                                                     New ReportCeBaseForm.RptParam("v_max_rows", _maxrows)}

        '    With params
        '        .msRptName = reportName
        '        .msRptWindowName = TranslationBase.TranslateLabelOrMessage(reportFileNameWindow)
        '        .moRptFormat = reportformat
        '        .moAction = ReportCeBaseForm.RptAction.SCHEDULE_VIEW
        '        .moRptParams = repparams
        '    End With

        '    Return params
        'End Function

        Private Sub GenerateReport()

            Dim reportParams As New System.Text.StringBuilder
            Me.State.MyBO = New ReportRequests
            Dim _maxrows As String

            Dim InterfaceTypeCode As ClaimFileProcessedData.InterfaceTypeCode = Me.State.moInterfaceTypeCode

            Select Case InterfaceTypeCode
                Case ClaimFileProcessedData.InterfaceTypeCode.CLOSE_CLAIM
                    reportParams.AppendFormat("v_claimfile_processed_id=> '{0}',", DALBase.GuidToSQLString(Me.State.ClaimfileProcessedId))
                    If Me.State.reportType.Equals(REJECT_REPORT) Then
                        'Rejectedcloseclaims-Exp_EN
                        reportParams.AppendFormat("V_REPORT_TYPE => '{0}'", "CLOSED_CLAIM_REJECT_EXPORT")
                        Me.PopulateBOProperty(Me.State.MyBO, "ReportType", "CLOSED_CLAIM_REJECT_EXPORT")
                        Me.PopulateBOProperty(Me.State.MyBO, "ReportProc", "REJECTEDCLOSECLAIMSRPT.Oracle_Export")

                    End If

                Case ClaimFileProcessedData.InterfaceTypeCode.CLOSE_CLAIM_SUNCOM
                    reportParams.AppendFormat("v_claimfile_processed_id=> '{0}',", DALBase.GuidToSQLString(Me.State.ClaimfileProcessedId))
                    Select Case Me.State.reportType
                        Case REJECT_REPORT
                            'RejectedSuncomcloseclaims-Exp_EN
                            reportParams.AppendFormat("V_REPORT_TYPE => '{0}'", "CLOSE_CALIM_SUNCOM_REJECT_EXPORT")
                            Me.PopulateBOProperty(Me.State.MyBO, "ReportType", "CLOSE_CALIM_SUNCOM_REJECT_EXPORT")
                            Me.PopulateBOProperty(Me.State.MyBO, "ReportProc", "R_REJSUNCOMCLOSECLAIMS.Oracle_Export")

                        Case PROCESSED_EXPORT
                            'ProcessedSuncomcloseclaims-processed-Exp    
                            reportParams.AppendFormat("V_REPORT_TYPE => '{0}'", "CLOSE_CALIM_SUNCOM_PROCESSED_EXPORT")
                            Me.PopulateBOProperty(Me.State.MyBO, "ReportType", "CLOSE_CALIM_SUNCOM_PROCESSED_EXPORT")
                            Me.PopulateBOProperty(Me.State.MyBO, "ReportProc", "R_PROCESSEDCLOSEDCLAIMS.Oracle_Export")
                    End Select

                Case ClaimFileProcessedData.InterfaceTypeCode.NEW_CLAIM
                    reportParams.AppendFormat("v_claimfile_processed_id=> '{0}',", DALBase.GuidToSQLString(Me.State.ClaimfileProcessedId))
                    If Me.State.reportType.Equals(REJECT_REPORT) Then
                        'RejectedNewclaims-Exp 
                        reportParams.AppendFormat("V_REPORT_TYPE => '{0}'", "NEW_CLAIM_REJECT_EXPORT")
                        Me.PopulateBOProperty(Me.State.MyBO, "ReportType", "NEW_CLAIM_REJECT_EXPORT")
                        Me.PopulateBOProperty(Me.State.MyBO, "ReportProc", "REJECTEDNEWCLAIMSRPT.Oracle_Export")
                    End If

                Case ClaimFileProcessedData.InterfaceTypeCode.CLAIM_SUSPENSE
                    If Me.State.reportType.Equals(REJECT_REPORT) Then
                        'Claimsuspence-Exp
                        If Me.State.SearchFilename IsNot Nothing AndAlso Me.State.SearchFilename.Trim.Length > 0 Then
                            _maxrows = MAX_ROW_OVERRIDE
                        Else
                            _maxrows = MAX_ROWS
                        End If
                        reportParams.AppendFormat("V_USER_ID => '{0}',", DALBase.GuidToSQLString(ElitaPlusIdentity.Current.ActiveUser.Id))
                        reportParams.AppendFormat("V_CERTIFICATE_NUMBER => '{0}',", Me.State.SearchCertificate)
                        reportParams.AppendFormat("V_AUTHORIZATION_NUMBER => '{0}',", Me.State.SearchAuthorization)
                        reportParams.AppendFormat("V_FILENAME => '{0}',", Me.State.SearchFilename)
                        reportParams.AppendFormat("V_MAX_ROWS => '{0}',", _maxrows)
                        reportParams.AppendFormat("V_REPORT_TYPE => '{0}'", "CLAIM_SUSPENSE_REJECT_EXPORT")
                        Me.PopulateBOProperty(Me.State.MyBO, "ReportType", "CLAIM_SUSPENSE_REJECT_EXPORT")
                        Me.PopulateBOProperty(Me.State.MyBO, "ReportProc", "ELP_CLAIM_SUSPENSE.Oracle_Export")
                    End If

                Case ClaimFileProcessedData.InterfaceTypeCode.CLAIM_LOAD_COMMON
                    reportParams.AppendFormat("v_claimload_file_processed_id=> '{0}',", DALBase.GuidToSQLString(Me.State.ClaimfileProcessedId))
                    If Me.State.reportType.Equals(REJECT_REPORT) Then

                        reportParams.AppendFormat("V_REPORT_TYPE => '{0}'", "CLAIM_LOAD_COMMON_REJECT_EXPORT")
                        Me.PopulateBOProperty(Me.State.MyBO, "ReportType", "CLAIM_LOAD_COMMON_REJECT_EXPORT")
                        Me.PopulateBOProperty(Me.State.MyBO, "ReportProc", "REJECTEDCLAIMLOADRPT.Oracle_Export")
                    End If

                Case ClaimFileProcessedData.InterfaceTypeCode.INVOICE_LOAD_COMMON
                    reportParams.AppendFormat("v_claimload_file_processed_id=> '{0}',", DALBase.GuidToSQLString(Me.State.ClaimfileProcessedId))
                    If Me.State.reportType.Equals(REJECT_REPORT) Then

                        reportParams.AppendFormat("V_REPORT_TYPE => '{0}'", "INVOICE_LOAD_COMMON_REJECT_EXPORT")
                        Me.PopulateBOProperty(Me.State.MyBO, "ReportType", "INVOICE_LOAD_COMMON_REJECT_EXPORT")
                        Me.PopulateBOProperty(Me.State.MyBO, "ReportProc", "R_REJECTEDINVOICE.Oracle_Export")
                    End If
            End Select

            Me.PopulateBOProperty(Me.State.MyBO, "ReportParameters", reportParams.ToString())
            Me.PopulateBOProperty(Me.State.MyBO, "UserEmailAddress", ElitaPlusIdentity.Current.EmailAddress)

            ScheduleReport()

        End Sub

        Private Sub ScheduleReport()
            Try
                Dim scheduleDate As DateTime = TheRptCeInputControl.GetSchedDate()
                If Me.State.MyBO.IsDirty Then
                    Me.State.MyBO.Save()
                    Me.State.IsNew = False
                    Me.State.HasDataChanged = True
                    Me.State.MyBO.CreateJob(scheduleDate)
                    If String.IsNullOrEmpty(ElitaPlusIdentity.Current.EmailAddress) Then
                        Me.DisplayMessage(Message.MSG_Email_not_configured, "", Me.MSG_BTN_OK, Me.MSG_TYPE_ALERT, , True)
                    Else
                        Me.DisplayMessage(Message.MSG_REPORT_REQUEST_IS_GENERATED, "", Me.MSG_BTN_OK, Me.MSG_TYPE_ALERT, , True)
                    End If
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

#End Region

    End Class

End Namespace