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

        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

#Region "Handlers-Init"

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            Me.ErrorCtrl.Clear_Hide()
            Try
                If Not Me.IsPostBack Then
                    InitializeForm()
                    'Date Calendar
                    Me.AddCalendar(Me.BtnAsOfDate, Me.moAsOfDateText)
                Else
                    ClearErrLabels()
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

#End Region

#End Region

#Region "Clear"

        Private Sub ClearErrLabels()
            Me.ClearLabelErrSign(moAsOfDateLabel)
            Me.ClearLabelErrSign(moDealerLabel)
            Me.ClearLabelErrSign(moCampaignNumberLabel)
        End Sub

#End Region

#Region "Populate"

        Sub PopulateDealerDropDown()
            Me.BindListControlToDataView(Me.cboDealer, LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies, False, "CODE"), , , True)
        End Sub

        Sub PopulateCampaignNumbersDropdown()
            Dim dv As DataView
            Dim i As Integer
            dv = LookupListNew.GetCampaignNumberLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies)
            Me.cboCampaignNumber.Items.Clear()
            Me.cboCampaignNumber.Items.Add(New ListItem("", ""))
            If Not dv Is Nothing Then
                For i = 0 To dv.Count - 1
                    Me.cboCampaignNumber.Items.Add(New ListItem(dv(i)("campaign_number").ToString, dv(i)("campaign_number").ToString))
                Next
            End If
        End Sub

        Private Sub InitializeForm()
            PopulateDealerDropDown()
            PopulateCampaignNumbersDropdown()
            Dim t As Date = Date.Now
            Me.moAsOfDateText.Text = GetDateFormattedString(t)
            Me.rdealer.Checked = True
            Me.RadiobuttonTotalsOnly.Checked = True
            Me.rbCampaignNumber.Checked = True
        End Sub

#End Region

#Region "Crystal Enterprise"

        Function SetParameters(ByVal userId As String, ByVal dealerCode As String, ByVal detailCode As String,
                                  ByVal asOfDate As String, ByVal MfgCoverage As String, ByVal campaign As String) As ReportCeBaseForm.Params

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
            Dim selectedDealerId As Guid = Me.GetSelectedItem(Me.cboDealer)
            Dim dv As DataView = LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies, False, "CODE")
            Dim dealerCode As String = LookupListNew.GetCodeFromId(dv, selectedDealerId)
            Dim detailCode As String
            Dim asOfDate As String
            Dim MfgCoverage As String
            Dim oinforceDate As DataView
            Dim inforceDate As String
            Dim sbMsg As New System.Text.StringBuilder
            Dim selectedCampaign As String = Me.GetSelectedDescription(Me.cboCampaignNumber)
            'Dates
            asOfDate = ReportCeBase.FormatDate(moAsOfDateLabel, Me.moAsOfDateText.Text)
            If Me.RadiobuttonTotalsOnly.Checked Then
                detailCode = NO
            Else
                If Me.RadiobuttonDetail.Checked Then
                    detailCode = YES
                Else
                    detailCode = YES
                End If
            End If

            If Me.rdealer.Checked Then
                dealerCode = ALL
            Else
                If selectedDealerId.Equals(Guid.Empty) Then
                    ElitaPlusPage.SetLabelError(moDealerLabel)
                    Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_DEALER_MUST_BE_SELECTED_ERR)
                End If
            End If

            If Me.rbCampaignNumber.Checked Then
                selectedCampaign = ALL
            Else
                If selectedCampaign.Equals(String.Empty) Then
                    ElitaPlusPage.SetLabelError(moCampaignNumberLabel)
                    Throw New GUIException(Message.MSG_INVALID_DEALER, Assurant.ElitaPlus.Common.ErrorCodes.GUI_CAMPAIGN_MUST_BE_SELECTED_ERR)
                End If
            End If

            If Me.chkINCLUDE_MFG_COVERAGE.Checked Then
                MfgCoverage = Me.INCLUDE_MFG_COVERAGE
            Else
                MfgCoverage = Me.EXCLUDE_MFG_COVERAGE
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
                    Me.HandleErrors(ex, Me.ErrorCtrl, False)
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
