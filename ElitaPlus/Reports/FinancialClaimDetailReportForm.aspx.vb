Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Imports System.Text.RegularExpressions
Imports System.Security.Cryptography.X509Certificates
Imports Assurant.ElitaPlus.DALObjects
Imports System.Globalization
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports System.Threading
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms
Namespace Reports
    Partial Public Class FinancialClaimDetailReportForm
        Inherits ElitaPlusPage

#Region "Constants"

        Private Const RPT_FILENAME_WINDOW As String = "FINANCIAL_CLAIM_DETAIL"
        Private Const RPT_FILENAME As String = "FinancialClaimDetail"
        Public Const ALL As String = "*"
        Public Const YES As String = "Y"
        Protected WithEvents btnViewHidden As System.Web.UI.WebControls.Button
        Public Const NO As String = "N"
        Private Const LABEL_SELECT_COMPANY As String = "SELECT_COMPANY"
        Private Const RPT_FILENAME_EXPORT As String = "FinancialClaimDetail-Exp"

        Private Const LABEL_SELECT_DEALER As String = "OR A SINGLE DEALER"

        Private Const TOTALPARAMS As Integer = 25  ' 23
        Private Const TOTALEXPPARAMS As Integer = 13  ' 7
        Private Const PARAMS_PER_REPORT As Integer = 13 '8
        Private Const ONE_ITEM As Integer = 1
        'Private Const LABEL_OR_A_SINGLE_DEALER As String = "OR A SINGLE DEALER"
        Private Const FromLoss As String = "LOSSTABLE"
        Private Const FromFinancialClaimReport As String = "FINANCIALREPORT"


#End Region

#Region "parameters"
        Public Structure ReportParams
            Public companyCode As String
            Public companyDesc As String
            Public dealerCode As String
            Public dealerDesc As String
            Public beginDate As String
            Public endDate As String
            Public monthyear As String
            Public QueryOption As String
            Public coverageType As String
            Public isDetail As String
            Public isSummarybyCov As String
            Public culturevalue As String
            Public langCode As String
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
                    moDealerMultipleDrop = CType(FindControl("moDealerMultipleDrop"), MultipleColumnDDLabelControl)
                End If
                Return moDealerMultipleDrop
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

#Region "Handlers"


#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub
        Protected WithEvents moReportCeInputControl As ReportCeInputControl
        Protected WithEvents ErrorCtrl As ErrorController
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
                    EnableOrDisableControls()
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

        Private Sub OnFromDrop_Changed(ByVal fromMultipleDrop As Assurant.ElitaPlus.ElitaPlusWebApp.Common.MultipleColumnDDLabelControl) _
            Handles moUserCompanyMultipleDrop.SelectedDropChanged
            Try
                PopulateDealerDropDown()
            Catch ex As Exception
                HandleErrors(ex, Me.ErrorCtrl)
            End Try
        End Sub

#End Region

#End Region

#Region "Clear"

        Private Sub ClearErrLabels()
            Me.ClearLabelErrSign(moBeginDateLabel)
            Me.ClearLabelErrSign(moEndDateLabel)
            Me.ClearLabelErrSign(moMonthLabel)
            Me.ClearLabelErrSign(moYearLabel)
            Me.ClearLabelErrSign(DealerMultipleDrop.CaptionLabel)
            Me.ClearLabelErrSign(UserCompanyMultipleDrop.CaptionLabel)
            If Me.rdealer.Checked Then DealerMultipleDrop.SelectedIndex = -1
            If Me.rCoverageType.Checked Then cboCoverageType.SelectedIndex = -1
        End Sub
        Private Sub EnableOrDisableControls()
            If Me.rSelectDates.Checked = True Then
                ControlMgr.SetEnableControl(Me, moBeginDateText, True)
                ControlMgr.SetEnableControl(Me, moEndDateText, True)
                ControlMgr.SetEnableControl(Me, MonthDropDownList, False)
                ControlMgr.SetEnableControl(Me, YearDropDownList, False)
            Else
                ControlMgr.SetEnableControl(Me, moBeginDateText, False)
                ControlMgr.SetEnableControl(Me, moEndDateText, False)
                ControlMgr.SetEnableControl(Me, MonthDropDownList, True)
                ControlMgr.SetEnableControl(Me, YearDropDownList, True)

            End If
        End Sub

#End Region

#Region "Populate"

        Private Sub PopulateCompaniesDropdown()
            Dim dv As DataView = LookupListNew.GetUserCompaniesLookupList()

            UserCompanyMultipleDrop.NothingSelected = True
            UserCompanyMultipleDrop.SetControl(True, UserCompanyMultipleDrop.MODES.NEW_MODE, True, dv, TranslationBase.TranslateLabelOrMessage(LABEL_SELECT_COMPANY), True, False)
            If dv.Count.Equals(ONE_ITEM) Then
                HideHtmlElement("ddSeparator")
                UserCompanyMultipleDrop.SelectedIndex = Me.ONE_ITEM
                UserCompanyMultipleDrop.Visible = False

            End If
        End Sub
        Private Sub PopulateDealerDropDown()
            Dim dv As DataView = LookupListNew.GetDealerLookupList(New Guid(UserCompanyMultipleDrop.SelectedGuid.ToString))
            DealerMultipleDrop.NothingSelected = True
            DealerMultipleDrop.SetControl(False,
                                              DealerMultipleDrop.MODES.NEW_MODE,
                                              True,
                                              dv,
                                              TranslationBase.TranslateLabelOrMessage(LABEL_SELECT_DEALER),
                                              True,
                                              False,
                                              " document.forms[0].rdealer.checked = false;",
                                              "moDealerMultipleDrop_moMultipleColumnDrop",
                                              "moDealerMultipleDrop_moMultipleColumnDropDesc", "moDealerMultipleDrop_lb_DropDown")

        End Sub

        Private Sub PopulateCoverageTypeDropDown()
            '  Me.BindListControlToDataView(Me.cboCoverageType, LookupListNew.GetCoverageTypeByCompanyGroupLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId, ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id), , , True)
            Dim compGroupId As Guid = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
            Dim listcontext As ListContext = New ListContext()
            listcontext.CompanyGroupId = compGroupId
            Dim covLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList(ListCodes.CoverageTypeByCompanyGroup, Thread.CurrentPrincipal.GetLanguageCode(), listcontext)
            Me.cboCoverageType.Populate(covLkl, New PopulateOptions() With
            {
              .AddBlankItem = True
            })
        End Sub
        Private Sub PopulateMonthsAndYearsDropdown()
            '   Dim dvM As DataView = LookupListNew.GetMonthsLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId)
            '  dvM.Sort = "CODE"
            '  Me.BindListControlToDataView(Me.MonthDropDownList, dvM, , , True)
            Dim monthLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("MONTH", Thread.CurrentPrincipal.GetLanguageCode())
            Me.MonthDropDownList.Populate(monthLkl, New PopulateOptions() With
           {
              .AddBlankItem = True
           })

            '  Dim dvY As DataView = AccountingCloseInfo.GetClosingYearsByUser(ElitaPlusIdentity.Current.ActiveUser.Id)
            ' Me.BindListTextToDataView(Me.YearDropDownList, dvY, , , True)
            Dim listcontext As ListContext = New ListContext()
            listcontext.UserId = ElitaPlusIdentity.Current.ActiveUser.Id
            Dim YearListLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("GetClosingYearsByUser", Thread.CurrentPrincipal.GetLanguageCode(), listcontext)
            Me.YearDropDownList.Populate(YearListLkl, New PopulateOptions() With
             {
            .AddBlankItem = True,
            .ValueFunc = AddressOf PopulateOptions.GetCode
                  })
        End Sub

        Private Sub InitializeForm()

            PopulateCompaniesDropdown()
            PopulateDealerDropDown()
            PopulateCoverageTypeDropDown()
            PopulateMonthsAndYearsDropdown()
            Dim t As Date = Date.Now.AddMonths(-1).AddDays(1)
            Me.moBeginDateText.Text = GetDateFormattedString(t)
            Me.moEndDateText.Text = GetDateFormattedString(Date.Now)
            Me.rdealer.Checked = True
            Me.rCoverageType.Checked = True
            Me.RadiobuttonTotalsOnly.Checked = True
            Me.rSelectDates.Checked = True
            ControlMgr.SetEnableControl(Me, MonthDropDownList, False)
            ControlMgr.SetEnableControl(Me, YearDropDownList, False)
            TheRptCeInputControl.populateReportLanguages(RPT_FILENAME)
        End Sub

#End Region

#Region "Crystal Enterprise"

        Function SetParameters(ByVal companycode As String, ByVal companydesc As String, ByVal dealerCode As String, ByVal dealerDesc As String, ByVal beginDate As String,
                                  ByVal endDate As String, ByVal monthyear As String, ByVal CoverageTypeDesc As String, ByVal isDetail As String, ByVal isSummarybyCov As String, ByVal rptQueryOption As String, ByVal langCode As String) As ReportCeBaseForm.Params

            Dim Params As New ReportCeBaseForm.Params
            Dim repParams(TOTALPARAMS) As ReportCeBaseForm.RptParam
            Dim rptParams As ReportParams
            moReportFormat = ReportCeBase.GetReportFormat(Me)
            Dim culturevalue As String = TheRptCeInputControl.getCultureValue(False, GuidControl.GuidToHexString(UserCompanyMultipleDrop.SelectedGuid))
            Dim reportName As String = TheRptCeInputControl.getReportName(RPT_FILENAME, False)

            With rptParams
                .companyCode = companycode
                .companyDesc = companydesc
                .dealerCode = dealerCode
                .dealerDesc = dealerDesc
                .beginDate = beginDate
                .endDate = endDate
                .monthyear = monthyear
                .coverageType = CoverageTypeDesc
                .isDetail = isDetail
                .isSummarybyCov = isSummarybyCov
                .QueryOption = rptQueryOption
                .langCode = langCode
                .culturevalue = culturevalue

            End With
            SetReportParams(rptParams, repParams, String.Empty, PARAMS_PER_REPORT * 0)     ' Main Report
            rptParams.isDetail = NO

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
        Function SetExpParameters(ByVal companycode As String, ByVal companydesc As String, ByVal dealerCode As String, ByVal dealerDesc As String, ByVal beginDate As String,
                                  ByVal endDate As String, ByVal monthyear As String, ByVal CoverageTypeDesc As String, ByVal isDetail As String, ByVal isSummarybyCov As String, ByVal rptQueryOption As String, ByVal langCode As String) As ReportCeBaseForm.Params

            'Dim reportName As String = RPT_FILENAME_EXPORT
            Dim Params As New ReportCeBaseForm.Params
            Dim repParams(TOTALEXPPARAMS) As ReportCeBaseForm.RptParam
            Dim rptParams As ReportParams
            moReportFormat = ReportCeBase.GetReportFormat(Me)
            Dim culturevalue As String = TheRptCeInputControl.getCultureValue(True, GuidControl.GuidToHexString(UserCompanyMultipleDrop.SelectedGuid))
            Dim reportName As String = TheRptCeInputControl.getReportName(RPT_FILENAME_EXPORT, True)

            With rptParams
                .companyCode = companycode
                .companyDesc = companydesc
                .dealerCode = dealerCode
                .dealerDesc = dealerDesc
                .beginDate = beginDate
                .endDate = endDate
                .monthyear = monthyear
                .coverageType = CoverageTypeDesc
                .isDetail = isDetail
                .isSummarybyCov = isSummarybyCov
                .QueryOption = rptQueryOption
                .langCode = langCode
                .culturevalue = culturevalue
            End With
            rptParams.isDetail = YES
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
                repParams(startIndex) = New ReportCeBaseForm.RptParam("V_COMPANY", .companyCode, reportName)
                repParams(startIndex + 1) = New ReportCeBaseForm.RptParam("V_COMPANYDESC", .companyDesc, reportName)
                repParams(startIndex + 2) = New ReportCeBaseForm.RptParam("V_DEALER", .dealerCode, reportName)
                repParams(startIndex + 3) = New ReportCeBaseForm.RptParam("V_DEALER_DESC", .dealerDesc, reportName)
                repParams(startIndex + 4) = New ReportCeBaseForm.RptParam("V_BEGIN_DATE", .beginDate, reportName)
                repParams(startIndex + 5) = New ReportCeBaseForm.RptParam("V_END_DATE", .endDate, reportName)
                repParams(startIndex + 6) = New ReportCeBaseForm.RptParam("V_MONTH_YEAR", .monthyear, reportName)
                repParams(startIndex + 7) = New ReportCeBaseForm.RptParam("V_COVTYPE", .coverageType, reportName)
                repParams(startIndex + 8) = New ReportCeBaseForm.RptParam("V_DETAIL", .isDetail, reportName)
                repParams(startIndex + 9) = New ReportCeBaseForm.RptParam("V_SUMMARYBYCOV", .isSummarybyCov, reportName)
                repParams(startIndex + 10) = New ReportCeBaseForm.RptParam("V_QUERYOPTION", .QueryOption, reportName)
                repParams(startIndex + 11) = New ReportCeBaseForm.RptParam("V_LANG_CODE", .langCode, reportName)
                repParams(startIndex + 12) = New ReportCeBaseForm.RptParam("LANG_CULTURE_VALUE", .culturevalue, reportName)
            End With
            '  End If
        End Sub
        Private Sub GenerateReport()
            Dim userId As Guid = ElitaPlusIdentity.Current.ActiveUser.Id
            Dim selectedDealerId As Guid = DealerMultipleDrop.SelectedGuid

            Dim langCode As String = LookupListNew.GetCodeFromId("LANGUAGES", ElitaPlusIdentity.Current.ActiveUser.LanguageId)

            Dim dealerCode As String = DealerMultipleDrop.SelectedCode
            Dim dealerDesc As String = DealerMultipleDrop.SelectedDesc
            Dim isDetail As String
            Dim endDate As String
            Dim beginDate As String

            Dim CompanyCode As String = UserCompanyMultipleDrop.SelectedCode
            Dim CompanyDesc As String = UserCompanyMultipleDrop.SelectedDesc
            Dim CoverageTypeDesc As String = cboCoverageType.SelectedItem.ToString
            Dim params As ReportCeBaseForm.Params
            Dim isSummarybyCov As String

            Dim selectedYear As String = Me.GetSelectedDescription(Me.YearDropDownList)
            Dim selectedMonthID As Guid = Me.GetSelectedItem(Me.MonthDropDownList)
            Dim selectedMonth As String = LookupListNew.GetCodeFromId(LookupListNew.LK_MONTHS, selectedMonthID)
            Dim monthyear As String

            Dim rptQueryOption As String

            If Me.rSelectDates.Checked = True Then
                If Not moBeginDateText.Text.Trim.ToString = String.Empty AndAlso Not moEndDateText.Text.Trim.ToString = String.Empty Then
                    ReportCeBase.ValidateBeginEndDate(moBeginDateLabel, moBeginDateText.Text, moEndDateLabel, moEndDateText.Text)
                    endDate = ReportCeBase.FormatDate(moEndDateLabel, moEndDateText.Text)
                    beginDate = ReportCeBase.FormatDate(moBeginDateLabel, moBeginDateText.Text)
                    rptQueryOption = FromFinancialClaimReport
                Else
                    ElitaPlusPage.SetLabelError(Me.moBeginDateLabel)
                    ElitaPlusPage.SetLabelError(Me.moEndDateLabel)
                    Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_YEARMONTH_MUST_BE_SELECTED_ERR)
                End If
            Else
                If Me.rMonthYear.Checked = True Then
                    If Not selectedMonthID.Equals(Guid.Empty) AndAlso Not selectedYear.Equals(String.Empty) Then
                        monthyear = selectedYear & selectedMonth
                        rptQueryOption = FromLoss
                    Else
                        ElitaPlusPage.SetLabelError(Me.moMonthLabel)
                        ElitaPlusPage.SetLabelError(Me.moYearLabel)
                        Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_YEARMONTH_MUST_BE_SELECTED_ERR)
                    End If
                End If
            End If


            'Validating the Company selection
            If UserCompanyMultipleDrop.SelectedGuid.Equals(Guid.Empty) Then
                ElitaPlusPage.SetLabelError(UserCompanyMultipleDrop.CaptionLabel)
                Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_COMPANY_IS_REQUIRED)
            End If

            'Dealer
            If Me.rdealer.Checked Then
                dealerCode = ALL
            Else
                If selectedDealerId.Equals(Guid.Empty) Then
                    ElitaPlusPage.SetLabelError(DealerMultipleDrop.CaptionLabel)
                    Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_DEALER_MUST_BE_SELECTED_ERR)
                End If
            End If

            'Coverage Type
            If Me.rCoverageType.Checked Then
                CoverageTypeDesc = ALL
            Else
                If CoverageTypeDesc.Equals(String.Empty) Then
                    ElitaPlusPage.SetLabelError(lblCoverageType)
                    Throw New GUIException(Message.MSG_INVALID_CLAIM_TYPE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_COVERAGE_TYPE_MUST_BE_SELECTED_ERR)
                End If
            End If

            If Me.RadiobuttonTotalsOnly.Checked Then
                isDetail = NO
            Else
                isDetail = YES
            End If
            If Me.chkTotalsPageByCov.Checked = True Then
                isSummarybyCov = YES
            Else
                isSummarybyCov = NO
            End If

            ReportCeBase.EnableReportCe(Me, TheRptCeInputControl)

            moReportFormat = ReportCeBase.GetReportFormat(Me)
            If (moReportFormat = ReportCeBase.RptFormat.TEXT_TAB) OrElse (moReportFormat = ReportCeBase.RptFormat.TEXT_CSV) Then
                'Export Report

                params = SetExpParameters(CompanyCode, CompanyDesc, dealerCode, dealerDesc, beginDate, endDate, monthyear, CoverageTypeDesc, isDetail, isSummarybyCov, rptQueryOption, langCode)
            Else
                'View Report
                params = SetParameters(CompanyCode, CompanyDesc, dealerCode, dealerDesc, beginDate, endDate, monthyear, CoverageTypeDesc, isDetail, isSummarybyCov, rptQueryOption, langCode)
            End If
            Session(ReportCeBaseForm.SESSION_PARAMETERS_KEY) = params

        End Sub

#End Region
    End Class
End Namespace
