Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Imports System.Globalization
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports System.Threading
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms
Namespace Reports
    Partial Class UnearnedPremiumReservesForm
        Inherits ElitaPlusPage

#Region "Constants"

        Private Const RPT_FILENAME_WINDOW As String = "UNEARNED PREMIUMS RESERVES"
        Private Const RPT_FILENAME As String = "UnearnedPremiumsReserves"
        Private Const RPT_FILENAME_EXPORT As String = "UnearnedPremiumsReserves-Exp"
        Private Const TOTAL_PARAMS As Integer = 14 ' 12 Elements
        Private Const TOTAL_EXP_PARAMS As Integer = 4 ' 4 Elements
        Private Const PARAMS_PER_REPORT As Integer = 5 ' 4 Elements
        Private Const ALL As String = "*"
        Private Const SP_DATE_FORMAT As String = "yyyyMMdd"
        Private Const YES As String = "D"
        Private Const NO As String = "S"
        Private Const ONE_ITEM As Integer = 1
        Private Const LABEL_SELECT_COMPANY As String = "SELECT_COMPANY"
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

        Public ReadOnly Property UserCompanyMultipleDrop() As MultipleColumnDDLabelControl
            Get
                If moUserCompanyMultipleDrop Is Nothing Then
                    moUserCompanyMultipleDrop = CType(FindControl("moUserCompanyMultipleDrop"), MultipleColumnDDLabelControl)
                End If
                Return moUserCompanyMultipleDrop
            End Get
        End Property

#End Region

#Region "variables"
        Private reportFormat As ReportCeBaseForm.RptFormat
        Protected WithEvents Label3 As System.Web.UI.WebControls.Label
        Private reportName As String = RPT_FILENAME
#End Region


#Region "Parameters"

        Public Structure ReportParams
            Public companyCode As String
            Public selectedYearMonth As String
            Public dealerCode As String
            Public detailCode As String
            Public culturecode As String
        End Structure

#End Region

#Region "Handlers"

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub

        Protected WithEvents Label2 As System.Web.UI.WebControls.Label
        Protected WithEvents rdReportFormat_NO_TRANSLATE As System.Web.UI.WebControls.RadioButtonList
        Protected WithEvents isReportCeVisible As System.Web.UI.HtmlControls.HtmlInputHidden
        Protected WithEvents isProgressVisible As System.Web.UI.HtmlControls.HtmlInputHidden
        Protected WithEvents moReportCeStatus As System.Web.UI.HtmlControls.HtmlInputHidden
        Protected WithEvents Label1 As System.Web.UI.WebControls.Label
        Protected WithEvents moExportDropDownList As System.Web.UI.WebControls.DropDownList
        Protected WithEvents rdReportType As System.Web.UI.WebControls.RadioButtonList
        Protected WithEvents ImageButtonBeginDate As System.Web.UI.WebControls.ImageButton
        Protected WithEvents ImageButtonEndDate As System.Web.UI.WebControls.ImageButton
        Protected WithEvents ErrorCtrl As ErrorController
        Protected WithEvents moEndDateLabel As System.Web.UI.WebControls.Label
        Protected WithEvents moEndDateText As System.Web.UI.WebControls.TextBox
        Protected WithEvents BtnEndDate As System.Web.UI.WebControls.ImageButton
        Protected WithEvents moBeginDateLabel As System.Web.UI.WebControls.Label
        Protected WithEvents moBeginDateText As System.Web.UI.WebControls.TextBox
        Protected WithEvents BtnBeginDate As System.Web.UI.WebControls.ImageButton
        Protected WithEvents moDealerLabel As System.Web.UI.WebControls.Label
        Protected WithEvents Radiobutton1 As System.Web.UI.WebControls.RadioButton
        Protected WithEvents moReportCeInputControl As ReportCeInputControl
        Protected WithEvents moUserCompanyMultipleDrop As MultipleColumnDDLabelControl
        Protected WithEvents btnViewHidden As System.Web.UI.WebControls.Button
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
            Me.ClearLabelsErrSign()
            Try
                If Not Me.IsPostBack Then
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

#End Region

#Region "Handlers-DropDown"

        Private Sub OnFromDrop_Changed(ByVal fromMultipleDrop As MultipleColumnDDLabelControl) _
                        Handles moUserCompanyMultipleDrop.SelectedDropChanged
            Try
                PopulateDealerDropDown()
            Catch ex As Exception
                HandleErrors(ex, Me.ErrorCtrl)
            End Try
        End Sub

#End Region

#End Region

#Region "Populate"

        Private Sub PopulateDealerDropDown()
            ' Me.BindListControlToDataView(Me.cboDealer, LookupListNew.GetDealerLookupList(UserCompanyMultipleDrop.SelectedGuid), , , True) 'DealerListByCompany
            Dim listcontext As ListContext = New ListContext()
            listcontext.CompanyId = UserCompanyMultipleDrop.SelectedGuid
            Dim dealerLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList(ListCodes.DealerListByCompany, Thread.CurrentPrincipal.GetLanguageCode(), listcontext)
            Me.cboDealer.Populate(dealerLkl, New PopulateOptions() With
             {
               .AddBlankItem = True
             })
        End Sub

        Private Sub PopulateYearsDropdown()
            'Dim dv As DataView = AccountingCloseInfo.GetClosingYears(ElitaPlusIdentity.Current.ActiveUser.CompanyId)
            'Me.BindListTextToDataView(Me.YearDropDownList, dv, , )
            Dim listcontext As ListContext = New ListContext()
            listcontext.CompanyId = ElitaPlusIdentity.Current.ActiveUser.CompanyId

            Dim YearListLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("ClosingYearsByCompany", Thread.CurrentPrincipal.GetLanguageCode(), listcontext)
            Me.YearDropDownList.Populate(YearListLkl, New PopulateOptions() With
             {
            .AddBlankItem = True,
            .ValueFunc = AddressOf PopulateOptions.GetCode,
            .BlankItemValue = "0"
                  })

        End Sub

        Private Sub PopulateMonthsDropdown()
            'Dim dv As DataView = LookupListNew.GetMonthsLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId)
            'dv.Sort = "CODE"
            'Me.BindListControlToDataView(Me.MonthDropDownList, dv, , )
            Dim monthLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("MONTH", Thread.CurrentPrincipal.GetLanguageCode())
            Me.MonthDropDownList.Populate(monthLkl, New PopulateOptions() With
           {
              .AddBlankItem = True
           })

        End Sub

        Private Sub PopulateCompaniesDropdown()
            Dim dv As DataView = LookupListNew.GetUserCompaniesLookupList()

            '''Me.UserCompanyMultipleDrop.NothingSelected = False
            '''Me.UserCompanyMultipleDrop.Caption = Me.TranslateLabelOrMessage("SELECT_COMPANY")
            '''UserCompanyMultipleDrop.BindData(dv)
            '''If UserCompanyMultipleDrop.Count.Equals(ONE_ITEM) Then
            '''    HideHtmlElement("ddSeparator")
            '''    UserCompanyMultipleDrop.Visible = False
            '''End If

            UserCompanyMultipleDrop.NothingSelected = True
            UserCompanyMultipleDrop.SetControl(True, UserCompanyMultipleDrop.MODES.NEW_MODE, True, dv, "* " & TranslationBase.TranslateLabelOrMessage(LABEL_SELECT_COMPANY), True)
            If dv.Count.Equals(ONE_ITEM) Then
                HideHtmlElement("ddSeparator")
                UserCompanyMultipleDrop.SelectedIndex = Me.ONE_ITEM
                UserCompanyMultipleDrop.Visible = False
            End If

        End Sub

        Private Sub InitializeForm()
            PopulateYearsDropdown()
            PopulateMonthsDropdown()
            PopulateCompaniesDropdown()
            PopulateDealerDropDown()
            Me.rdealer.Checked = True
            Me.RadiobuttonTotalsOnly.Checked = True
            TheRptCeInputControl.populateReportLanguages(RPT_FILENAME)
        End Sub

        Public Sub ClearLabelsErrSign()
            Try
                Me.ClearLabelErrSign(MonthYearLabel)
                Me.ClearLabelErrSign(UserCompanyMultipleDrop.CaptionLabel)
                Me.ClearLabelErrSign(DealerLabel)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try
        End Sub
#End Region

#Region "Crystal Enterprise"

        Private Sub GenerateReport()
            'Dim companyId As Guid = Me.GetApplicationUser.CompanyID
            'Dim compCode As String = LookupListNew.GetCodeFromId("COMPANIES", companyId)
            Dim compCode As String = UserCompanyMultipleDrop.SelectedCode
            Dim selectedYear As String = Me.GetSelectedDescription(Me.YearDropDownList)
            Dim selectedMonthID As Guid = Me.GetSelectedItem(Me.MonthDropDownList)
            Dim selectedMonth As String = LookupListNew.GetCodeFromId(LookupListNew.LK_MONTHS, selectedMonthID)
            Dim selectedYearMonth As String = selectedYear & selectedMonth
            Dim selectedDealerId As Guid = Me.GetSelectedItem(Me.cboDealer)
            Dim dvDealer As DataView = LookupListNew.GetDealerLookupList(UserCompanyMultipleDrop.SelectedGuid)
            Dim dealerCode As String = LookupListNew.GetCodeFromId(dvDealer, selectedDealerId)
            Dim detailCode As String
            Dim params As ReportCeBaseForm.Params

            If Me.RadiobuttonTotalsOnly.Checked Then
                detailCode = NO
            Else
                detailCode = YES
            End If

            'Validating the month and year
            If selectedMonthID.Equals(Guid.Empty) OrElse selectedYear.Equals(String.Empty) Then
                ElitaPlusPage.SetLabelError(MonthYearLabel)
                Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_YEARMONTH_MUST_BE_SELECTED_ERR)
            End If

            'Validating the Company selection
            If UserCompanyMultipleDrop.SelectedGuid.Equals(Guid.Empty) Then
                ElitaPlusPage.SetLabelError(UserCompanyMultipleDrop.CaptionLabel)
                Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_COMPANY_IS_REQUIRED)
            End If

            If Me.rdealer.Checked Then
                dealerCode = ALL
            Else
                If selectedDealerId.Equals(Guid.Empty) Then
                    ElitaPlusPage.SetLabelError(DealerLabel)
                    Throw New GUIException(Message.MSG_INVALID_DEALER, Assurant.ElitaPlus.Common.ErrorCodes.GUI_DEALER_MUST_BE_SELECTED_ERR)
                End If
            End If

            ReportCeBase.EnableReportCe(Me, TheRptCeInputControl)

            reportFormat = ReportCeBase.GetReportFormat(Me)

            If (reportFormat = ReportCeBase.RptFormat.TEXT_TAB) OrElse (reportFormat = ReportCeBase.RptFormat.TEXT_CSV) Then
                'Export Report
                Dim exportData As String = NO
                detailCode = YES
                exportData = YES
                reportName = RPT_FILENAME_EXPORT
                params = SetExpParameters(compCode, selectedYearMonth, dealerCode, detailCode)
            Else
                'View Report
                params = SetParameters(compCode, selectedYearMonth, dealerCode, detailCode)
            End If

            Session(ReportCeBaseForm.SESSION_PARAMETERS_KEY) = params
        End Sub

        Function SetParameters(ByVal companyCode As String, ByVal selectedYearMonth As String, ByVal dealerCode As String,
                               ByVal detailCode As String) As ReportCeBaseForm.Params

            Dim reportFormat As ReportCeBaseForm.RptFormat
            'Dim reportName As String = RPT_FILENAME
            Dim params As New ReportCeBaseForm.Params
            Dim repParams(TOTAL_PARAMS) As ReportCeBaseForm.RptParam
            Dim rptParams As ReportParams
            Dim culturecode As String = TheRptCeInputControl.getCultureValue(False)
            Dim reportName As String = TheRptCeInputControl.getReportName(RPT_FILENAME, False)

            reportFormat = ReportCeBase.GetReportFormat(Me)

            With rptParams
                .companyCode = companyCode
                .selectedYearMonth = selectedYearMonth
                .dealerCode = dealerCode
                .detailCode = detailCode
                .culturecode = culturecode
            End With

            SetReportParams(rptParams, repParams, String.Empty, PARAMS_PER_REPORT * 0)  ' Main Report
            ' SetReportParams(rptParams, repParams, "Summary1", PARAMS_PER_REPORT * 1)    ' Summary1 (Sub Report)
            ' SetReportParams(rptParams, repParams, "Summary2", PARAMS_PER_REPORT * 2)    ' Summary2 (Sub Report)

            Me.rptWindowTitle.InnerText = TheRptCeInputControl.getReportWindowTitle(TranslationBase.TranslateLabelOrMessage(RPT_FILENAME_WINDOW))
            With params
                .msRptName = reportName
                .msRptWindowName = TranslationBase.TranslateLabelOrMessage(RPT_FILENAME_WINDOW)
                .moRptFormat = reportFormat
                .moAction = ReportCeBaseForm.RptAction.SCHEDULE_VIEW
                .moRptParams = repParams
            End With
            Return params
        End Function

        Function SetExpParameters(ByVal companyCode As String, ByVal selectedYearMonth As String, ByVal dealerCode As String,
                                ByVal detailCode As String) As ReportCeBaseForm.Params

            'Dim reportName As String = RPT_FILENAME_EXPORT
            Dim params As New ReportCeBaseForm.Params
            Dim repParams(TOTAL_EXP_PARAMS) As ReportCeBaseForm.RptParam
            Dim rptParams As ReportParams
            Dim culturecode As String = TheRptCeInputControl.getCultureValue(True)
            Dim reportName As String = TheRptCeInputControl.getReportName(RPT_FILENAME_EXPORT, True)

            With rptParams
                .companyCode = companyCode
                .selectedYearMonth = selectedYearMonth
                .dealerCode = dealerCode
                .detailCode = detailCode
                .culturecode = culturecode
            End With

            SetReportParams(rptParams, repParams, String.Empty, PARAMS_PER_REPORT * 0)  ' Main Report

            Me.rptWindowTitle.InnerText = TheRptCeInputControl.getReportWindowTitle(TranslationBase.TranslateLabelOrMessage(RPT_FILENAME_WINDOW))

            With params
                .msRptName = reportName
                .msRptWindowName = TranslationBase.TranslateLabelOrMessage(RPT_FILENAME_WINDOW)
                .moRptFormat = reportFormat
                .moAction = ReportCeBaseForm.RptAction.SCHEDULE_VIEW
                .moRptParams = repParams
            End With

            Return params
        End Function

        Sub SetReportParams(ByVal rptParams As ReportParams, ByVal repParams() As ReportCeBaseForm.RptParam, _
                            ByVal rptName As String, ByVal startIndex As Integer)

            With rptParams
                repParams(startIndex) = New ReportCeBaseForm.RptParam("V_COMPANY_KEY", .companyCode, rptName)
                repParams(startIndex + 1) = New ReportCeBaseForm.RptParam("V_YEARMONTH", .selectedYearMonth, rptName)
                repParams(startIndex + 2) = New ReportCeBaseForm.RptParam("V_DEALER_CODE", .dealerCode, rptName)
                repParams(startIndex + 3) = New ReportCeBaseForm.RptParam("V_DETAIL_CODE", .detailCode, rptName)
                repParams(startIndex + 4) = New ReportCeBaseForm.RptParam("LANG_CULTURE_VALUE", .culturecode, rptName)
            End With

        End Sub

#End Region


    End Class
End Namespace
