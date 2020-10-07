Namespace Reports

    Partial Class InForceCertificatesEnglishUSAReportForm
        Inherits ElitaPlusPage

#Region "Constants"

        Private Const RPT_FILENAME_WINDOW As String = "IN-FORCE CERTIFICATES"
        Private Const RPT_FILENAME As String = "InForceCertificatesEnglishUSA"
        Private Const RPT_FILENAME_EXPORT As String = "InForceCertificatesEnglishUSA_Exp"
        Private Const SUBRPT_FILENAME As String = "InForceCertificatesEnglishUSAsr.rpt"

        Public Const ALL As String = "*"
        '  Public Const SP_DATE_FORMAT As String = "yyyyMMdd"
        Public Const YES As String = "Y"
        Protected WithEvents btnViewHidden As System.Web.UI.WebControls.Button
        Public Const NO As String = "N"
        Public Const EXCLUDE_MFG_COVERAGE As String = "M"
        Public Const INCLUDE_MFG_COVERAGE As String = "%"


#End Region

#Region "Properties"
        Public ReadOnly Property MyReportCeInputControl() As ReportCeInputControl
            Get
                If moReportCeInputControl Is Nothing Then
                    moReportCeInputControl = CType(FindControl("moReportCeInputControl"), ReportCeInputControl)
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

        Protected WithEvents moReportCeInputControl As ReportCeInputControl
        Protected WithEvents ErrorCtrl As ErrorController

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

        Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            ErrorCtrl.Clear_Hide()
            Try
                If Not IsPostBack Then
                    InitializeForm()
                    'Date Calendar
                    AddCalendar(BtnAsOfDate, moAsOfDateText)
                Else
                    ClearErrLabels()
                End If
                InstallProgressBar()
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try
            ShowMissingTranslations(ErrorCtrl)
        End Sub
#End Region

#Region "Handlers-Buttons"

        Private Sub btnGenRpt_Click(sender As System.Object, e As System.EventArgs) Handles btnGenRpt.Click
            Try
                GenerateReport()
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try
        End Sub

#End Region

#End Region

#Region "Clear"

        Private Sub ClearErrLabels()
            ClearLabelErrSign(moAsOfDateLabel)
            ClearLabelErrSign(moDealerLabel)
            ClearLabelErrSign(moCampaignNumberLabel)
        End Sub

#End Region

#Region "Populate"

        Sub PopulateDealerDropDown()
            BindListControlToDataView(cboDealer, LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies, False, "CODE"), , , True)
        End Sub

        Sub PopulateCampaignNumbersDropdown()
            Dim dv As DataView
            Dim i As Integer
            dv = LookupListNew.GetCampaignNumberLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies)
            cboCampaignNumber.Items.Clear()
            cboCampaignNumber.Items.Add(New ListItem("", ""))
            If dv IsNot Nothing Then
                For i = 0 To dv.Count - 1
                    cboCampaignNumber.Items.Add(New ListItem(dv(i)("campaign_number").ToString, dv(i)("campaign_number").ToString))
                Next
            End If
        End Sub

        Private Sub InitializeForm()
            PopulateDealerDropDown()
            PopulateCampaignNumbersDropdown()
            Dim t As Date = Date.Now
            moAsOfDateText.Text = GetDateFormattedString(t)
            rdealer.Checked = True
            RadiobuttonTotalsOnly.Checked = True
            rbCampaignNumber.Checked = True
        End Sub

#End Region

#Region "Crystal Enterprise"

        Function SetParameters(userId As String, dealerCode As String, detailCode As String,
                                  asOfDate As String, MfgCoverage As String, campaign As String) As ReportCeBaseForm.Params

            Dim params As New ReportCeBaseForm.Params
            Dim reportName As String = RPT_FILENAME
            Dim moReportFormat As ReportCeBaseForm.RptFormat

            Dim exportData As String = NO
            Dim repParams() As ReportCeBaseForm.RptParam

            moReportFormat = ReportCeBase.GetReportFormat(Me)
            If (moReportFormat = ReportCeBase.RptFormat.TEXT_TAB) OrElse (moReportFormat = ReportCeBase.RptFormat.TEXT_CSV) Then
                detailCode = YES
                exportData = YES
                reportName = RPT_FILENAME_EXPORT
            End If

            repParams = New ReportCeBaseForm.RptParam() _
                        {
                         New ReportCeBaseForm.RptParam("V_USER_KEY", userId),
                         New ReportCeBaseForm.RptParam("V_DEALER", dealerCode),
                         New ReportCeBaseForm.RptParam("V_DETAIL_CODE", detailCode),
                         New ReportCeBaseForm.RptParam("V_AS_OF_DATE", asOfDate),
                         New ReportCeBaseForm.RptParam("V_EXCLUDE_MFG_COVERAGE", MfgCoverage),
                         New ReportCeBaseForm.RptParam("V_CAMPAIGN_NUMBER", campaign)
                        }

            With params
                .msRptName = reportName
                .msRptWindowName = TranslationBase.TranslateLabelOrMessage(RPT_FILENAME_WINDOW)
                .moRptFormat = moReportFormat
                .moAction = ReportCeBaseForm.RptAction.SCHEDULE_VIEW
                .moRptParams = repParams
            End With
            Return params
        End Function

        Private Sub GenerateReport()

            Dim userId As String = GuidControl.GuidToHexString(ElitaPlusIdentity.Current.ActiveUser.Id)
            Dim selectedDealerId As Guid = GetSelectedItem(cboDealer)
            Dim dv As DataView = LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies, False, "CODE")
            Dim dealerCode As String = LookupListNew.GetCodeFromId(dv, selectedDealerId)
            Dim detailCode As String
            Dim asOfDate As String
            Dim MfgCoverage As String
            Dim oinforceDate As DataView
            Dim inforceDate As String
            Dim sbMsg As New System.Text.StringBuilder
            Dim selectedCampaign As String = GetSelectedDescription(cboCampaignNumber)
            'Dates
            asOfDate = ReportCeBase.FormatDate(moAsOfDateLabel, moAsOfDateText.Text)
            If RadiobuttonTotalsOnly.Checked Then
                detailCode = NO
            Else
                If RadiobuttonDetail.Checked Then
                    detailCode = YES
                Else
                    detailCode = YES
                End If
            End If

            If rdealer.Checked Then
                dealerCode = ALL
            Else
                If selectedDealerId.Equals(Guid.Empty) Then
                    ElitaPlusPage.SetLabelError(moDealerLabel)
                    Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_DEALER_MUST_BE_SELECTED_ERR)
                End If
            End If

            If rbCampaignNumber.Checked Then
                selectedCampaign = ALL
            Else
                If selectedCampaign.Equals(String.Empty) Then
                    ElitaPlusPage.SetLabelError(moCampaignNumberLabel)
                    Throw New GUIException(Message.MSG_INVALID_DEALER, Assurant.ElitaPlus.Common.ErrorCodes.GUI_CAMPAIGN_MUST_BE_SELECTED_ERR)
                End If
            End If

            If chkINCLUDE_MFG_COVERAGE.Checked Then
                MfgCoverage = INCLUDE_MFG_COVERAGE
            Else
                MfgCoverage = EXCLUDE_MFG_COVERAGE
            End If

            Dim bocert As New Certificate
            oinforceDate = bocert.GetInforceCertsLastestDate()

            If oinforceDate.Count > 0 Then
                Try
                    inforceDate = CType(oinforceDate.Item(0).Row(0).ToString, Date).ToString("yyyyMMdd")
                    If asOfDate > inforceDate Then
                        sbMsg.Append(TranslationBase.TranslateLabelOrMessage(Assurant.ElitaPlus.Common.ErrorCodes.ERR_INFORCECERTS_LATEST_DATE))
                        sbMsg.Append(" " + CType(oinforceDate.Item(0).Row(0).ToString, Date).ToString("dd-MMM-yyyy"))
                        Throw New GUIException(Message.MSG_BEGIN_END_DATE, sbMsg.ToString)
                    End If
                Catch ex As Exception
                    HandleErrors(ex, ErrorCtrl, False)
                    Exit Sub
                End Try
            Else
                Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.ERR_NO_INFORCECERTS_DATE_AVIALABLE)
            End If

            ReportCeBase.EnableReportCe(Me, MyReportCeInputControl)

            Dim params As ReportCeBaseForm.Params = SetParameters(userId, dealerCode, detailCode, asOfDate, MfgCoverage, selectedCampaign)
            Session(ReportCeBaseForm.SESSION_PARAMETERS_KEY) = params

        End Sub

#End Region


    End Class
End Namespace
