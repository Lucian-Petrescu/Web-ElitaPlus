Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common

Namespace Reports

    Partial Class NewCertificatesWEPPReportForm
        Inherits ElitaPlusPage

#Region "Constants"

        Private Const RPT_FILENAME_WINDOW As String = "NEW CERTIFICATES"
        Private Const RPT_FILENAME As String = "NewCertificatesWEPP"
        ' Private Const RPT_FILENAME As String = "ExpiredCertificatesEnglishUSAdgfasf"
        Private Const RPT_FILENAME_EXPORT As String = "NewCertificatesWEPP-Exp"

        Public Const ALL As String = "*"
        '  Public Const SP_DATE_FORMAT As String = "yyyyMMdd"
        Public Const YES As String = "Y"
        Protected WithEvents btnViewHidden As System.Web.UI.WebControls.Button
        Public Const NO As String = "N"

        Private Const LABEL_SELECT_DEALER As String = "OR A SINGLE DEALER"

        Private Const TOTALPARAMS As Integer = 15  ' 23
        Private Const TOTALEXPPARAMS As Integer = 8  ' 7
        Private Const PARAMS_PER_REPORT As Integer = 8 '8
#End Region

#Region "parameters"
        Public Structure ReportParams
            Public userId As String
            Public dealerCode As String
            Public beginDate As String
            Public endDate As String
            Public sCurrency As String
            Public isSummary As String
            Public totalsByCov As String
            Public culturevalue As String
        End Structure
#End Region

#Region "variables"
        Private moReportFormat As ReportCeBaseForm.RptFormat
#End Region

#Region "Properties"

        Public ReadOnly Property TheRptCeInputControl() As ReportCeInputControl
            Get
                If moReportCeInputControl Is Nothing Then
                    moReportCeInputControl = CType(FindControl("moReportCeInputControl"), ReportCeInputControl)
                End If
                Return moReportCeInputControl
            End Get
        End Property
        Public ReadOnly Property DealerMultipleDrop() As MultipleColumnDDLabelControl
            Get
                If moDealerMultipleDrop Is Nothing Then
                    moDealerMultipleDrop = CType(FindControl("multipleDropControl"), MultipleColumnDDLabelControl)
                End If
                Return moDealerMultipleDrop
            End Get
        End Property
#End Region

#Region "Handlers"


#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub
        Protected WithEvents moDealerMultipleDrop As MultipleColumnDDLabelControl
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

                    'Date Calendars
                    Me.AddCalendar(Me.BtnBeginDate, Me.moBeginDateText)
                    Me.AddCalendar(Me.BtnEndDate, Me.moEndDateText)
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
            Me.ClearLabelErrSign(moBeginDateLabel)
            Me.ClearLabelErrSign(moEndDateLabel)
            Me.ClearLabelErrSign(DealerMultipleDrop.CaptionLabel)
            If Me.rdealer.Checked Then DealerMultipleDrop.SelectedIndex = -1
        End Sub

#End Region

#Region "Populate"

        Sub PopulateDealerDropDown()
            '''Me.BindListControlToDataView(Me.cboDealerDec, LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies, False, "CODE"), , , True)
            '''Dim DealersLookupListSortedByCode As DataView = LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies, False, "CODE")
            '''DealersLookupListSortedByCode.Sort = "CODE"
            '''Me.BindListControlToDataView(Me.cboDealerCode, DealersLookupListSortedByCode, "CODE", , True)
            Dim dv As DataView = LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies, False, "CODE")
            DealerMultipleDrop.NothingSelected = True
            DealerMultipleDrop.SetControl(False, DealerMultipleDrop.MODES.NEW_MODE, True, dv, TranslationBase.TranslateLabelOrMessage(LABEL_SELECT_DEALER), True, True, " document.forms[0].rdealer.checked = false;")

        End Sub

        Private Sub InitializeForm()
            TheRptCeInputControl.populateReportLanguages(RPT_FILENAME)
            PopulateDealerDropDown()
            Dim t As Date = Date.Now.AddMonths(-1).AddDays(1)
            Me.moBeginDateText.Text = GetDateFormattedString(t)
            Me.moEndDateText.Text = GetDateFormattedString(Date.Now)
            Me.rdealer.Checked = True
            Me.RadiobuttonTotalsOnly.Checked = True
        End Sub

#End Region

#Region "Crystal Enterprise"

        Function SetParameters(ByVal userId As String, ByVal dealerCode As String, ByVal beginDate As String,
                                  ByVal endDate As String, ByVal isSummary As String, ByVal sCurrency As String, ByVal totalsByCov As String) As ReportCeBaseForm.Params

            'Dim reportName As String = RPT_FILENAME
            Dim Params As New ReportCeBaseForm.Params
            Dim repParams(TOTALPARAMS) As ReportCeBaseForm.RptParam
            Dim rptParams As ReportParams
            moReportFormat = ReportCeBase.GetReportFormat(Me)
            Dim culturevalue As String = TheRptCeInputControl.getCultureValue(False)
            Dim reportName As String = TheRptCeInputControl.getReportName(RPT_FILENAME, False)

            With rptParams
                .userId = userId
                .dealerCode = dealerCode
                .beginDate = beginDate
                .endDate = endDate
                .isSummary = isSummary
                .sCurrency = sCurrency
                .totalsByCov = totalsByCov
                .culturevalue = culturevalue
            End With
            SetReportParams(rptParams, repParams, String.Empty, PARAMS_PER_REPORT * 0)     ' Main Report
            rptParams.isSummary = "Y"

            Me.rptWindowTitle.InnerText = TheRptCeInputControl.getReportWindowTitle(TranslationBase.TranslateLabelOrMessage(RPT_FILENAME_WINDOW))

            With Params
                .msRptName = reportName
                .msRptWindowName = TranslationBase.TranslateLabelOrMessage(RPT_FILENAME_WINDOW)
                .moRptFormat = moReportFormat
                .moAction = ReportCeBaseForm.RptAction.SCHEDULE_VIEW
                .moRptParams = repParams
            End With

            Return Params

            'Dim params As New ReportCeBaseForm.Params
            'Dim reportName As String = RPT_FILENAME
            'Dim moReportFormat As ReportCeBaseForm.RptFormat

            'Dim exportData As String = NO

            'moReportFormat = ReportCeBase.GetReportFormat(Me)

            'If (moReportFormat = ReportCeBase.RptFormat.TEXT_TAB) OrElse (moReportFormat = ReportCeBase.RptFormat.TEXT_CSV) Then
            '    isSummary = NO
            '    exportData = YES
            '    reportName = RPT_FILENAME_EXPORT
            'End If

            'Dim repParams() As ReportCeBaseForm.RptParam = New ReportCeBaseForm.RptParam() _
            '                        { _
            '                         New ReportCeBaseForm.RptParam("V_USER_ID", userId), _
            '                         New ReportCeBaseForm.RptParam("V_DEALER_CODE", dealerCode), _
            '                         New ReportCeBaseForm.RptParam("V_BEGIN_DATE", beginDate), _
            '                         New ReportCeBaseForm.RptParam("V_END_DATE", endDate), _
            '                         New ReportCeBaseForm.RptParam("P_CURRENCY", sCurrency), _
            '                         New ReportCeBaseForm.RptParam("V_IS_SUMMARY", isSummary)}

            'With params
            '    .msRptName = reportName
            '    .msRptWindowName = TranslationBase.TranslateLabelOrMessage(RPT_FILENAME_WINDOW)
            '    .moRptFormat = moReportFormat
            '    .moAction = ReportCeBaseForm.RptAction.SCHEDULE_VIEW
            '    .moRptParams = repParams
            'End With
            'Return params
        End Function
        Function SetExpParameters(ByVal userId As String, ByVal dealerCode As String, ByVal beginDate As String,
                                  ByVal endDate As String, ByVal isSummary As String, ByVal sCurrency As String, ByVal totalsByCov As String) As ReportCeBaseForm.Params

            'Dim reportName As String = RPT_FILENAME_EXPORT
            Dim Params As New ReportCeBaseForm.Params
            Dim repParams(TOTALEXPPARAMS) As ReportCeBaseForm.RptParam
            Dim rptParams As ReportParams
            moReportFormat = ReportCeBase.GetReportFormat(Me)
            Dim culturevalue As String = TheRptCeInputControl.getCultureValue(True)
            Dim reportName As String = TheRptCeInputControl.getReportName(RPT_FILENAME_EXPORT, True)

            With rptParams
                .userId = userId
                .dealerCode = dealerCode
                .beginDate = beginDate
                .endDate = endDate
                .isSummary = "N"
                .sCurrency = sCurrency
                .totalsByCov = totalsByCov
                .culturevalue = culturevalue
            End With
            SetReportParams(rptParams, repParams, String.Empty, PARAMS_PER_REPORT * 0)     ' Main Report

            Me.rptWindowTitle.InnerText = TheRptCeInputControl.getReportWindowTitle(TranslationBase.TranslateLabelOrMessage(RPT_FILENAME_WINDOW))

            With Params
                .msRptName = reportName
                .msRptWindowName = TranslationBase.TranslateLabelOrMessage(RPT_FILENAME_WINDOW)
                .moRptFormat = moReportFormat
                .moAction = ReportCeBaseForm.RptAction.SCHEDULE_VIEW
                .moRptParams = repParams
            End With

            Return Params
        End Function

        Sub SetReportParams(ByVal rptParams As ReportParams, ByVal repParams() As ReportCeBaseForm.RptParam,
                          ByVal reportName As String, ByVal startIndex As Integer)
            With rptParams
                repParams(startIndex) = New ReportCeBaseForm.RptParam("V_USER_ID", .userId, reportName)
                repParams(startIndex + 1) = New ReportCeBaseForm.RptParam("V_DEALER_CODE", .dealerCode, reportName)
                repParams(startIndex + 2) = New ReportCeBaseForm.RptParam("V_BEGIN_DATE", .beginDate, reportName)
                repParams(startIndex + 3) = New ReportCeBaseForm.RptParam("V_END_DATE", .endDate, reportName)
                repParams(startIndex + 4) = New ReportCeBaseForm.RptParam("P_CURRENCY", .sCurrency, reportName)
                repParams(startIndex + 5) = New ReportCeBaseForm.RptParam("V_IS_SUMMARY", .isSummary, reportName)
                repParams(startIndex + 6) = New ReportCeBaseForm.RptParam("IS_TOTALSBYCOV", .totalsByCov, reportName)
                repParams(startIndex + 7) = New ReportCeBaseForm.RptParam("LANG_CULTURE_VALUE", .culturevalue, reportName)
            End With
        End Sub
        Private Sub GenerateReport()
            Dim userId As Guid = ElitaPlusIdentity.Current.ActiveUser.Id
            Dim selectedDealerId As Guid = DealerMultipleDrop.SelectedGuid 'Me.GetSelectedItem(Me.cboDealerDec)
            'Dim dv As DataView = LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies, False, "CODE")
            Dim dealerCode As String = DealerMultipleDrop.SelectedCode 'LookupListNew.GetCodeFromId(dv, selectedDealerId)
            Dim isSummary As String
            Dim endDate As String
            Dim beginDate As String
            Dim oCountryId As Guid = ElitaPlusIdentity.Current.ActiveUser.Country(ElitaPlusIdentity.Current.ActiveUser.FirstCompanyID).Id
            Dim oCountry As New Country(oCountryId)
            Dim params As ReportCeBaseForm.Params
            Dim totalsByCov As String

            'Currency
            Dim Currencyid As Guid = oCountry.PrimaryCurrencyId
            Dim strCurrency As String = LookupListNew.GetDescriptionFromId("CURRENCIES", Currencyid)

            'Dates
            ReportCeBase.ValidateBeginEndDate(moBeginDateLabel, moBeginDateText.Text, moEndDateLabel, moEndDateText.Text)
            endDate = ReportCeBase.FormatDate(moEndDateLabel, moEndDateText.Text)
            beginDate = ReportCeBase.FormatDate(moBeginDateLabel, moBeginDateText.Text)

            If Me.RadiobuttonTotalsOnly.Checked Then
                isSummary = YES
            Else
                isSummary = NO
            End If

            If Me.rdealer.Checked Then
                dealerCode = ALL
            Else
                If selectedDealerId.Equals(Guid.Empty) Then
                    ElitaPlusPage.SetLabelError(DealerMultipleDrop.CaptionLabel)
                    Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_DEALER_MUST_BE_SELECTED_ERR)
                End If
            End If

            If chkTotalsPageByCov.Checked = True Then
                totalsByCov = YES
            Else
                totalsByCov = NO
            End If

            ReportCeBase.EnableReportCe(Me, TheRptCeInputControl)


            moReportFormat = ReportCeBase.GetReportFormat(Me)
            If (moReportFormat = ReportCeBase.RptFormat.TEXT_TAB) OrElse (moReportFormat = ReportCeBase.RptFormat.TEXT_CSV) Then
                'Export Report

                params = SetExpParameters(GuidControl.GuidToHexString(userId), dealerCode, beginDate, endDate, isSummary, strCurrency, totalsByCov)
            Else
                'View Report
                params = SetParameters(GuidControl.GuidToHexString(userId), dealerCode, beginDate, endDate, isSummary, strCurrency, totalsByCov)
            End If
            Session(ReportCeBaseForm.SESSION_PARAMETERS_KEY) = params

        End Sub

#End Region

    End Class


End Namespace
