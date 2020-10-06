Imports System.Text.RegularExpressions
Imports System.Security.Cryptography.X509Certificates
Imports Assurant.ElitaPlus.DALObjects
Imports System.Globalization
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports System.Threading
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms
Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Namespace Reports
    Partial Class IBNRExportReportForm
        Inherits ElitaPlusPage

#Region "Constants"

        Private Const RPT_FILENAME_WINDOW As String = "IBNR"
        Private Const RPT_FILENAME As String = ""
        Private Const RPT_FILENAME_EXPORT As String = "IBNR-Exp"
        Private Const TOTAL_PARAMS As Integer = 11 ' 12 Elements
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
        Public ReadOnly Property TheRptCeInputControl() As ReportExtractInputControl
            Get
                If moReportExtractInputControl Is Nothing Then
                    moReportExtractInputControl = CType(FindControl("moReportExtractInputControl"), ReportExtractInputControl)
                End If
                Return moReportExtractInputControl
            End Get
        End Property

        Public ReadOnly Property UserCompanyMultipleDrop() As MultipleColumnDDLabelControl
            Get
                If multipleDropControl Is Nothing Then
                    multipleDropControl = CType(FindControl("multipleDropControl"), MultipleColumnDDLabelControl)
                End If
                Return multipleDropControl
            End Get
        End Property

#End Region

#Region "Page State"

        Class MyState
            Public MyBO As ReportRequests
            Public IsNew As Boolean = False
            Public ForEdit As Boolean = False
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

#End Region

#Region "variables"
        Private reportFormat As ReportCeBaseForm.RptFormat
        Private reportName As String = RPT_FILENAME
#End Region


#Region "Parameters"

        Public Structure ReportParams
            Public companyCode As String
            Public selectedYearMonth As String
            Public dealerCode As String
            Public culturevalue As String
            Public langCode As String
        End Structure

#End Region

#Region "Handlers"

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub

        Protected WithEvents rdReportFormat_NO_TRANSLATE As System.Web.UI.WebControls.RadioButtonList
        Protected WithEvents isReportCeVisible As System.Web.UI.HtmlControls.HtmlInputHidden
        Protected WithEvents isProgressVisible As System.Web.UI.HtmlControls.HtmlInputHidden
        Protected WithEvents moReportCeStatus As System.Web.UI.HtmlControls.HtmlInputHidden
        Protected WithEvents moExportDropDownList As System.Web.UI.WebControls.DropDownList
        Protected WithEvents rdReportType As System.Web.UI.WebControls.RadioButtonList
        Protected WithEvents ErrorCtrl As ErrorController
        Protected WithEvents moDealerLabel As System.Web.UI.WebControls.Label
        Protected WithEvents Radiobutton1 As System.Web.UI.WebControls.RadioButton
        Protected WithEvents moReportCeInputControl As ReportCeInputControl
        Protected WithEvents btnViewHidden As System.Web.UI.WebControls.Button
        Protected WithEvents multipleDropControl As MultipleColumnDDLabelControl
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
            ClearLabelsErrSign()
            Try
                If Not IsPostBack Then
                    InitializeForm()
                End If
                InstallProgressBar()
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try
            ShowMissingTranslations(ErrorCtrl)

        End Sub

        Public Sub ClearLabelsErrSign()
            Try
                ClearLabelErrSign(MonthYearLabel)
                ClearLabelErrSign(UserCompanyMultipleDrop.CaptionLabel)
                ClearLabelErrSign(DealerLabel)
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try
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

#Region "Handlers-DropDown"

        Private Sub OnFromDrop_Changed(fromMultipleDrop As Assurant.ElitaPlus.ElitaPlusWebApp.Common.MultipleColumnDDLabelControl) _
             Handles multipleDropControl.SelectedDropChanged
            Try
                PopulateDealerDropDown()
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try
        End Sub

#End Region

#End Region

#Region "Populate"

        Private Sub PopulateDealerDropDown()
            ' Me.BindListControlToDataView(Me.cboDealer, LookupListNew.GetDealerLookupList(UserCompanyMultipleDrop.SelectedGuid), , , True)
            Dim oDealerList = GetDealerListByCompanyForUser()
            cboDealer.Populate(oDealerList, New PopulateOptions() With
                                               {
                                                .AddBlankItem = True
                                                })

        End Sub

        Private Sub PopulateYearsDropdown()
            ' Dim dv As DataView = AccountingCloseInfo.GetClosingYears(ElitaPlusIdentity.Current.ActiveUser.CompanyId)
            ' Me.BindListTextToDataView(Me.YearDropDownList, dv, , )
            Dim listcontext As ListContext = New ListContext()
            listcontext.CompanyId = ElitaPlusIdentity.Current.ActiveUser.CompanyId
            Dim YearListLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("ClosingYearsByCompany", Thread.CurrentPrincipal.GetLanguageCode(), listcontext)
            YearDropDownList.Populate(YearListLkl, New PopulateOptions() With
                                         {
                                         .AddBlankItem = True,
                                         .ValueFunc = AddressOf PopulateOptions.GetCode,
                                         .BlankItemValue = "0"
                                         })
        End Sub

        Private Sub PopulateMonthsDropdown()
            'Dim dv As DataView = LookupListNew.GetMonthsLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId)
            'dv.Sort = "CODE"
            ' Me.BindListControlToDataView(Me.MonthDropDownList, dv, , )
            Dim monthLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("MONTH", Thread.CurrentPrincipal.GetLanguageCode())
            MonthDropDownList.Populate(monthLkl, New PopulateOptions() With
           {
              .AddBlankItem = True
           })
        End Sub
        Private Function GetDealerListByCompanyForUser() As Assurant.Elita.CommonConfiguration.DataElements.ListItem()
            Dim Index As Integer
            Dim oListContext As New Assurant.Elita.CommonConfiguration.ListContext

            Dim UserCompanies As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Companies

            Dim oDealerList As New Collections.Generic.List(Of Assurant.Elita.CommonConfiguration.DataElements.ListItem)

            For Index = 0 To UserCompanies.Count - 1
                'UserCompanyList &= ",'" & GuidControl.GuidToHexString(UserCompanyies(Index))
                oListContext.CompanyId = UserCompanies(Index)
                Dim oDealerListForCompany As Assurant.Elita.CommonConfiguration.DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="DealerListByCompany", context:=oListContext)
                If oDealerListForCompany.Count > 0 Then
                    If oDealerList IsNot Nothing Then
                        oDealerList.AddRange(oDealerListForCompany)
                    Else
                        oDealerList = oDealerListForCompany.Clone()
                    End If

                End If
            Next

            Return oDealerList.ToArray()

        End Function

        Private Sub PopulateCompaniesDropdown()
            Dim dv As DataView = LookupListNew.GetUserCompaniesLookupList()
            UserCompanyMultipleDrop.NothingSelected = True
            UserCompanyMultipleDrop.SetControl(True, UserCompanyMultipleDrop.MODES.NEW_MODE, True, dv, "* " & TranslationBase.TranslateLabelOrMessage(LABEL_SELECT_COMPANY), True)
            If dv.Count.Equals(ONE_ITEM) Then
                HideHtmlElement("ddSeparator")
                UserCompanyMultipleDrop.SelectedIndex = ONE_ITEM
                UserCompanyMultipleDrop.Visible = False
            End If
        End Sub

        Private Sub InitializeForm()
            PopulateYearsDropdown()
            PopulateMonthsDropdown()
            PopulateCompaniesDropdown()
            PopulateDealerDropDown()
            TheRptCeInputControl.SetExportOnly()
            rdealer.Checked = True
            ' TheRptCeInputControl.populateReportLanguages(RPT_FILENAME_EXPORT)
        End Sub

#End Region

#Region "Crystal Enterprise"

        Private Sub GenerateReport()
            Dim compCode As String = UserCompanyMultipleDrop.SelectedCode
            Dim selectedYear As String = GetSelectedDescription(YearDropDownList)
            Dim selectedMonthID As Guid = GetSelectedItem(MonthDropDownList)
            Dim selectedMonth As String = LookupListNew.GetCodeFromId(LookupListNew.LK_MONTHS, selectedMonthID)
            Dim selectedYearMonth As String = selectedYear & selectedMonth
            Dim selectedDealerId As Guid = GetSelectedItem(cboDealer)
            Dim dvDealer As DataView = LookupListNew.GetDealerLookupList(UserCompanyMultipleDrop.SelectedGuid)
            Dim dealerCode As String = LookupListNew.GetCodeFromId(dvDealer, selectedDealerId)
            Dim params As ReportCeBaseForm.Params
            Dim langId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
            Dim langCode As String = LookupListNew.GetCodeFromId("LANGUAGES", langId)
            Dim reportParams As New System.Text.StringBuilder


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

            If rdealer.Checked Then
                dealerCode = ALL
            Else
                If selectedDealerId.Equals(Guid.Empty) Then
                    ElitaPlusPage.SetLabelError(DealerLabel)
                    Throw New GUIException(Message.MSG_INVALID_DEALER, Assurant.ElitaPlus.Common.ErrorCodes.GUI_DEALER_MUST_BE_SELECTED_ERR)
                End If
            End If

            reportParams.AppendFormat("pi_comp_code => '{0}',", compCode)
            reportParams.AppendFormat("pi_dealer_code => '{0}',", dealerCode)
            reportParams.AppendFormat("pi_year_month => '{0}',", selectedYearMonth)
            reportParams.AppendFormat("pi_lang_code => '{0}'", langCode)


            State.MyBO = New ReportRequests
            State.ForEdit = True
            PopulateBOProperty(State.MyBO, "ReportType", "IBNR_EXPORT")
            PopulateBOProperty(State.MyBO, "ReportProc", "R_IBNRExport.Oralce_Export")
            PopulateBOProperty(State.MyBO, "ReportParameters", ReportParams.ToString())
            PopulateBOProperty(State.MyBO, "UserEmailAddress", ElitaPlusIdentity.Current.EmailAddress)
            'ReportCeBase.EnableReportCe(Me, TheRptCeInputControl)

            'reportFormat = ReportCeBase.GetReportFormat(Me)

            'If (reportFormat = ReportCeBase.RptFormat.TEXT_TAB) OrElse (reportFormat = ReportCeBase.RptFormat.TEXT_CSV) Then
            '    'Export Report
            '    Dim exportData As String = NO
            '    exportData = YES
            '    reportName = RPT_FILENAME_EXPORT
            '    params = SetExpParameters(compCode, selectedYearMonth, dealerCode, langCode)
            '    '  Else
            '    'View Report
            '    '  params = SetParameters(compCode, selectedYearMonth, dealerCode)
            'End If

            'Session(ReportCeBaseForm.SESSION_PARAMETERS_KEY) = params
            ScheduleReport()
        End Sub


        Private Sub ScheduleReport()
            Try
                Dim scheduleDate As DateTime = TheRptCeInputControl.GetSchedDate()
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

                    btnGenRpt.Enabled = False

                Else
                End If

            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try
        End Sub

        'Function SetParameters(ByVal companyCode As String, ByVal selectedYearMonth As String, ByVal dealerCode As String) As ReportCeBaseForm.Params

        '    Dim reportFormat As ReportCeBaseForm.RptFormat
        '    'Dim reportName As String = RPT_FILENAME
        '    Dim params As New ReportCeBaseForm.Params
        '    Dim repParams(TOTAL_PARAMS) As ReportCeBaseForm.RptParam
        '    Dim rptParams As ReportParams

        '    reportFormat = ReportCeBase.GetReportFormat(Me)

        '    With rptParams
        '        .companyCode = companyCode
        '        .selectedYearMonth = selectedYearMonth
        '        .dealerCode = dealerCode                
        '    End With

        '    SetReportParams(rptParams, repParams, String.Empty, PARAMS_PER_REPORT * 0)  ' Main Report
        '    ' SetReportParams(rptParams, repParams, "Summary1", PARAMS_PER_REPORT * 1)    ' Summary1 (Sub Report)
        '    ' SetReportParams(rptParams, repParams, "Summary2", PARAMS_PER_REPORT * 2)    ' Summary2 (Sub Report)
        '    With params
        '        .msRptName = reportName
        '        .msRptWindowName = TranslationBase.TranslateLabelOrMessage(RPT_FILENAME_WINDOW)
        '        .moRptFormat = reportFormat
        '        .moAction = ReportCeBaseForm.RptAction.SCHEDULE_VIEW
        '        .moRptParams = repParams
        '    End With
        '    Return params
        'End Function

        Function SetExpParameters(companyCode As String, selectedYearMonth As String, dealerCode As String, langCode As String) As ReportCeBaseForm.Params

            'Dim reportName As String = RPT_FILENAME_EXPORT
            Dim params As New ReportCeBaseForm.Params
            Dim repParams(TOTAL_EXP_PARAMS) As ReportCeBaseForm.RptParam
            Dim rptParams As ReportParams
            Dim reportName As String = TheRptCeInputControl.getReportName(RPT_FILENAME_EXPORT, True)
            Dim culturevalue As String = TheRptCeInputControl.getCultureValue(True, GuidControl.GuidToHexString((UserCompanyMultipleDrop.SelectedGuid)))
            rptWindowTitle.InnerText = TheRptCeInputControl.getReportWindowTitle(TranslationBase.TranslateLabelOrMessage(RPT_FILENAME_WINDOW))


            With rptParams
                .companyCode = companyCode
                .selectedYearMonth = selectedYearMonth
                .dealerCode = dealerCode
                .culturevalue = culturevalue
                .langCode = langCode
            End With

            SetReportParams(rptParams, repParams, String.Empty, PARAMS_PER_REPORT * 0)  ' Main Report

            With params
                .msRptName = reportName
                .msRptWindowName = TranslationBase.TranslateLabelOrMessage(RPT_FILENAME_WINDOW)
                .moRptFormat = reportFormat
                .moAction = ReportCeBaseForm.RptAction.SCHEDULE_VIEW
                .moRptParams = repParams
            End With

            Return params
        End Function

        Sub SetReportParams(rptParams As ReportParams, repParams() As ReportCeBaseForm.RptParam, _
                            rptName As String, startIndex As Integer)

            With rptParams
                repParams(startIndex) = New ReportCeBaseForm.RptParam("V_COMPANY", .companyCode, rptName)
                repParams(startIndex + 1) = New ReportCeBaseForm.RptParam("V_DEALER_CODE", .dealerCode, rptName)
                repParams(startIndex + 2) = New ReportCeBaseForm.RptParam("V_YEARMONTH", .selectedYearMonth, rptName)
                repParams(startIndex + 3) = New ReportCeBaseForm.RptParam("V_LANG_CULTURE_CODE", .culturevalue, rptName)
                repParams(startIndex + 4) = New ReportCeBaseForm.RptParam("V_LANG_CODE", .langCode, rptName)
            End With

        End Sub

#End Region


    End Class
End Namespace
