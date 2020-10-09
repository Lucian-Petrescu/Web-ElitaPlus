Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Imports Microsoft.VisualBasic
Imports System.Threading
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms

Namespace Reports
    Partial Public Class SalesByPeriodAndBranchReportForm
        Inherits ElitaPlusPage

#Region "Constants"

        '   Private Const RPT_FILENAME As String = "Claims Following Service Warranty English (USA)_TEST7"
        Private Const RPT_FILENAME_WINDOW As String = "BRANCH_SALES"
        Private Const RPT_FILENAME As String = "BranchSales"
        Private Const RPT_FILENAME_EXPORT As String = "BranchSales-Exp"

        Public Const CRYSTAL As String = "0"
        'Public Const PDF As String = "1"
        Public Const HTML As String = "2"
        Public Const CSV As String = "3"
        'Public Const EXCEL As String = "4"

        Public Const VIEW_REPORT As String = "1"
        Public Const PDF As String = "2"
        Public Const TAB As String = "3"
        Public Const EXCEL As String = "4"


        Public Const EXCEL_DATA_ONLY As String = "5"
        Public Const JAVA As String = "6"
        Public Const ALL As String = "*"
        Public Const NOTHING_SELECTED As Int16 = -1
        '  Public Const SP_DATE_FORMAT As String = "yyyyMMdd"
        Public Const YES As String = "Y"
        Protected WithEvents Label2 As System.Web.UI.WebControls.Label
        Protected WithEvents rdReportFormat_NO_TRANSLATE As System.Web.UI.WebControls.RadioButtonList
        Public Const NO As String = "N"

        Private Const ONE_ITEM As Integer = 1
        Private Const LABEL_SELECT_DEALER As String = "OR A SINGLE DEALER"
        Private Const LABEL_SELECT_COMPANY As String = "SELECT_COMPANY"

        Public Const ZIP_CODE As String = "0"
        Public Const BRANCH_CODE As String = "1"




#End Region

#Region "variables"
        Dim moReportFormat As ReportCeBaseForm.RptFormat
        Public EndDate As String
        Public BeginDate As String
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
        Protected WithEvents isReportCeVisible As System.Web.UI.HtmlControls.HtmlInputHidden
        Protected WithEvents isProgressVisible As System.Web.UI.HtmlControls.HtmlInputHidden
        Protected WithEvents moReportCeStatus As System.Web.UI.HtmlControls.HtmlInputHidden
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
            PopulateCompaniesDropdown()
            PopulateDealerDropDown()
            PopulateYearDropDown()
            moWeekBeginDateText.Text = String.Empty
            moWeekEndDateText.Text = String.Empty
            txtBeginWeekNum.Text = String.Empty
            txtEndWeekNum.Text = String.Empty

            txtBeginQuatNum.Text = String.Empty
            cboBeginQuatYear.SelectedIndex = -1
            txtEndQuatNum.Text = String.Empty
            cboEndQuatYear.SelectedIndex = -1

            txtBeginMonthNum.Text = String.Empty
            cboBeginMonthyear.SelectedIndex = -1
            txtEndMonthNum.Text = String.Empty
            cboEndMonthyear.SelectedIndex = -1
            rdealer.Checked = True
            TheRptCeInputControl.populateReportLanguages(RPT_FILENAME)

            txtBeginWeekNum.Style.Add("display", "none")
            txtEndWeekNum.Style.Add("display", "none")
            lblBeginWeekNum.Style.Add("display", "none")
            lblEndWeekNum.Style.Add("display", "none")

        End Sub

        Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load, Me.Load
            'Put user code to initialize the page here
            ErrorCtrl.Clear_Hide()
            Try
                If Not IsPostBack Then
                    InitializeForm()
                    'Date Calendars
                    AddCalendar(BtnBeginDate, moWeekBeginDateText)
                    AddCalendar(BtnEndDate, moWeekEndDateText)
                    TheRptCeInputControl.ExcludeExport()
                Else
                    ClearErrLabels()
                End If
                InstallProgressBar()
                If moWeekEndDateText.Text.Trim <> String.Empty Then
                    txtEndWeekNum.Style.Add("display", "")
                    lblEndWeekNum.Style.Add("display", "")
                Else
                    txtEndWeekNum.Style.Add("display", "none")
                    lblEndWeekNum.Style.Add("display", "none")
                End If
                If moWeekBeginDateText.Text.Trim <> String.Empty Then
                    txtBeginWeekNum.Style.Add("display", "")
                    lblBeginWeekNum.Style.Add("display", "")
                Else
                    txtBeginWeekNum.Style.Add("display", "none")
                    lblBeginWeekNum.Style.Add("display", "none")
                End If
                ClearErrLabels()
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try
            ShowMissingTranslations(ErrorCtrl)
        End Sub

#End Region

#Region "Handlers-Buttons"

        Private Sub btnGenRpt_Click(sender As System.Object, e As System.EventArgs) Handles btnGenRpt.Click
            Try
                ClearErrLabels()
                GenerateReport()
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try
        End Sub

#End Region

#End Region

#Region "Handlers-DropDown"

        Private Sub OnFromDrop_Changed(fromMultipleDrop As Assurant.ElitaPlus.ElitaPlusWebApp.Common.MultipleColumnDDLabelControl) _
            Handles moUserCompanyMultipleDrop.SelectedDropChanged
            Try
                PopulateDealerDropDown()
                If moWeekEndDateText.Text.Trim <> String.Empty Then
                    txtEndWeekNum.Style.Add("display", "")
                    lblEndWeekNum.Style.Add("display", "")
                Else
                    txtEndWeekNum.Style.Add("display", "none")
                    lblEndWeekNum.Style.Add("display", "none")
                End If
                If moWeekBeginDateText.Text.Trim <> String.Empty Then
                    txtBeginWeekNum.Style.Add("display", "")
                    lblBeginWeekNum.Style.Add("display", "")
                Else
                    txtBeginWeekNum.Style.Add("display", "none")
                    lblBeginWeekNum.Style.Add("display", "none")
                End If
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try
        End Sub

#End Region

#Region "Clear"

        Private Sub ClearErrLabels()
            ClearLabelErrSign(moWBeginDateLabel)
            ClearLabelErrSign(moWEndDateLabel)
            ClearLabelErrSign(moQBeginDateLabel)
            ClearLabelErrSign(moQEndDateLabel)
            ClearLabelErrSign(moMBeginDateLabel)
            ClearLabelErrSign(moMEndDateLabel)
            ClearLabelErrSign(lblWeeklySelect)
            ClearLabelErrSign(lblQuaterlySelect)
            ClearLabelErrSign(lblMonthlySelect)
            ClearLabelErrSign(UserCompanyMultipleDrop.CaptionLabel)
        End Sub

#End Region

#Region "Populate"

        Private Sub PopulateCompaniesDropdown()
            Dim dv As DataView = LookupListNew.GetUserCompaniesLookupList()

            UserCompanyMultipleDrop.NothingSelected = True
            UserCompanyMultipleDrop.SetControl(True, UserCompanyMultipleDrop.MODES.NEW_MODE, True, dv, "*" + TranslationBase.TranslateLabelOrMessage(LABEL_SELECT_COMPANY), True)
            If dv.Count.Equals(ONE_ITEM) Then
                HideHtmlElement("ddSeparator")
                UserCompanyMultipleDrop.SelectedIndex = ONE_ITEM
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

        Private Sub PopulateYearDropDown()
            'Dim dv As DataView = AccountingCloseInfo.GetClosingYearsByUser(ElitaPlusIdentity.Current.ActiveUser.Id)
            'Me.BindListTextToDataView(Me.cboBeginQuatYear, dv, , , True)
            'Me.BindListTextToDataView(Me.cboEndQuatYear, dv, , , True)
            'Me.BindListTextToDataView(Me.cboBeginMonthyear, dv, , , True)
            'Me.BindListTextToDataView(Me.cboEndMonthyear, dv, , , True)

            Dim YearList As New Collections.Generic.List(Of Assurant.Elita.CommonConfiguration.DataElements.ListItem)
            Dim DistinctYearList As New Collections.Generic.List(Of Assurant.Elita.CommonConfiguration.DataElements.ListItem)
            Dim oListContext As New Assurant.Elita.CommonConfiguration.ListContext

            For Each _company As Guid In ElitaPlusIdentity.Current.ActiveUser.Companies
                oListContext.CompanyId = _company
                Dim YearListForCompany As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="ClosingYearsByCompany", context:=oListContext, languageCode:=ElitaPlusIdentity.Current.ActiveUser.LanguageCode)
                If YearListForCompany.Count > 0 Then
                    If YearList IsNot Nothing Then
                        YearList.AddRange(YearListForCompany)
                    Else
                        YearList = YearListForCompany.Clone()
                    End If
                End If
            Next

            DistinctYearList = YearList.Distinct().ToList()

            cboBeginQuatYear.Populate(DistinctYearList.ToArray(),
                                         New PopulateOptions() With
                                         {
                                           .AddBlankItem = True,
                                           .BlankItemValue = "0",
                                           .ValueFunc = AddressOf .GetCode
                                         })

            cboEndQuatYear.Populate(DistinctYearList.ToArray(),
                                         New PopulateOptions() With
                                         {
                                           .AddBlankItem = True,
                                           .BlankItemValue = "0",
                                           .ValueFunc = AddressOf .GetCode
                                         })

            cboBeginMonthyear.Populate(DistinctYearList.ToArray(),
                                         New PopulateOptions() With
                                         {
                                           .AddBlankItem = True,
                                           .BlankItemValue = "0"
                                         })

            cboEndMonthyear.Populate(DistinctYearList.ToArray(),
                                         New PopulateOptions() With
                                         {
                                           .AddBlankItem = True,
                                           .BlankItemValue = "0",
                                           .ValueFunc = AddressOf .GetCode
                                         })

        End Sub

#End Region

#Region "Crystal Enterprise"

        Function SetParameters(companyId As String, companydesc As String,
                                    dealercode As String, dealerdesc As String, beginDate As String,
                                      endDate As String, formatype As String, sortOrder As String) As ReportCeBaseForm.Params

            Dim params As New ReportCeBaseForm.Params
            Dim culturecode As String = TheRptCeInputControl.getCultureValue(False)
            Dim reportName As String = TheRptCeInputControl.getReportName(RPT_FILENAME, False)

            rptWindowTitle.InnerText = TheRptCeInputControl.getReportWindowTitle(TranslationBase.TranslateLabelOrMessage(RPT_FILENAME_WINDOW))

            Dim repParams() As ReportCeBaseForm.RptParam = New ReportCeBaseForm.RptParam() _
                    {
                     New ReportCeBaseForm.RptParam("V_COMPANY_ID", companyId),
                     New ReportCeBaseForm.RptParam("V_COMPANYDESC", companydesc),
                     New ReportCeBaseForm.RptParam("V_DEALER", dealercode),
                     New ReportCeBaseForm.RptParam("V_DEALERDESC", dealerdesc),
                     New ReportCeBaseForm.RptParam("V_BEGIN_DATE", beginDate),
                     New ReportCeBaseForm.RptParam("V_END_DATE", endDate),
                     New ReportCeBaseForm.RptParam("V_TYPE", formatype),
                     New ReportCeBaseForm.RptParam("V_SORT_ORDER", sortOrder),
                     New ReportCeBaseForm.RptParam("LANG_CULTURE_VALUE", culturecode)}

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
            Dim userId As Guid = ElitaPlusIdentity.Current.ActiveUser.Id
            Dim selectedDealerId As Guid = DealerMultipleDrop.SelectedGuid 'Me.GetSelectedItem(Me.cboDealerDec)            
            Dim dealerCode As String = DealerMultipleDrop.SelectedCode 'LookupListNew.GetCodeFromId(dv, selectedDealerId)
            Dim dealerDesc As String = DealerMultipleDrop.SelectedDesc
            Dim CompanyCode As String = UserCompanyMultipleDrop.SelectedCode
            Dim CompanyDesc As String = UserCompanyMultipleDrop.SelectedDesc
            Dim companyid As String = GuidControl.GuidToHexString((UserCompanyMultipleDrop.SelectedGuid))
            Dim EndNumber As String
            Dim BeginNumber As String
            Dim EndYear As String
            Dim BeginYear As String
            Dim ForamatType As String
            Dim sortOrder As String


            Dim params As ReportCeBaseForm.Params

            '  Try
            'Validating the Company selection
            If UserCompanyMultipleDrop.SelectedGuid.Equals(Guid.Empty) Then
                ElitaPlusPage.SetLabelError(UserCompanyMultipleDrop.CaptionLabel)
                Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_COMPANY_IS_REQUIRED)
            End If

            If moWeekEndDateText.Text <> String.Empty AndAlso moWeekBeginDateText.Text <> String.Empty Then

                'Dates
                ReportCeBase.ValidateBeginEndDate(moWBeginDateLabel, moWeekBeginDateText.Text, moWEndDateLabel, moWeekEndDateText.Text)
                EndDate = ReportCeBase.FormatDate(moWEndDateLabel, GetWeekDates(DateHelper.GetDateValue(moWeekEndDateText.Text), "end"))
                BeginDate = ReportCeBase.FormatDate(moWBeginDateLabel, GetWeekDates(DateHelper.GetDateValue(moWeekBeginDateText.Text), "start"))
                ForamatType = "Week"

                'Number (Quarter)
            ElseIf txtEndQuatNum.Text <> String.Empty AndAlso txtBeginQuatNum.Text <> String.Empty _
                      AndAlso cboEndQuatYear.SelectedValue.ToString <> String.Empty _
                  AndAlso cboBeginQuatYear.SelectedValue.ToString <> String.Empty Then

                If IsNumeric(txtEndQuatNum.Text) AndAlso IsNumeric(txtEndQuatNum.Text) Then

                    If CInt(txtEndQuatNum.Text.Trim.ToString) > 0 AndAlso CInt(txtEndQuatNum.Text.Trim.ToString) <= 4 _
                        AndAlso CInt(txtBeginQuatNum.Text.Trim.ToString) > 0 AndAlso CInt(txtEndQuatNum.Text.Trim.ToString) <= 4 Then

                        If cboBeginQuatYear.SelectedItem.ToString <> String.Empty AndAlso cboEndQuatYear.SelectedItem.ToString <> String.Empty Then

                            EndNumber = txtEndQuatNum.Text
                            BeginNumber = txtBeginQuatNum.Text
                            EndYear = cboEndQuatYear.SelectedValue
                            BeginYear = cboBeginQuatYear.SelectedValue
                            ForamatType = "Quarter"

                            If CInt(BeginNumber) = 1 Then
                                BeginDate = "01/01/" + BeginYear
                            ElseIf CInt(BeginNumber) = 2 Then
                                BeginDate = "04/01/" + BeginYear
                            ElseIf CInt(BeginNumber) = 3 Then
                                BeginDate = "07/01/" + BeginYear
                            Else : BeginDate = "10/01/" + BeginYear
                            End If

                            If CInt(EndNumber) = 1 Then
                                EndDate = "03/31/" + EndYear
                            ElseIf CInt(EndNumber) = 2 Then
                                EndDate = "06/30/" + EndYear
                            ElseIf CInt(EndNumber) = 3 Then
                                EndDate = "09/30/" + EndYear
                            Else : EndDate = "12/31/" + EndYear
                            End If

                            ReportCeBase.ValidateBeginEndDate(moQBeginDateLabel, BeginDate, moQEndDateLabel, EndDate)
                            EndDate = ReportCeBase.FormatDate(moQEndDateLabel, EndDate)
                            BeginDate = ReportCeBase.FormatDate(moQBeginDateLabel, BeginDate)

                        Else
                            ElitaPlusPage.SetLabelError(lblQuaterlySelect)
                            Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_QUARTER_YEAR_SELECTION_ERROR)
                        End If

                    Else
                        ElitaPlusPage.SetLabelError(lblQuaterlySelect)
                        Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_QUARTER_SELECTION_ERROR)
                    End If
                Else
                    ElitaPlusPage.SetLabelError(lblQuaterlySelect)
                    Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_INVALID_NUMBER)
                End If

            ElseIf txtEndMonthNum.Text <> String.Empty AndAlso txtBeginMonthNum.Text <> String.Empty _
                AndAlso cboEndMonthyear.SelectedValue.ToString <> String.Empty _
                AndAlso cboBeginMonthyear.SelectedValue.ToString <> String.Empty Then

                If IsNumeric(txtEndMonthNum.Text) AndAlso IsNumeric(txtBeginMonthNum.Text) Then

                    'Number (Month)
                    If CInt(txtEndMonthNum.Text.Trim.ToString) > 0 AndAlso CInt(txtEndMonthNum.Text.Trim.ToString) <= 12 _
                    AndAlso CInt(txtBeginMonthNum.Text.Trim.ToString) > 0 AndAlso CInt(txtBeginMonthNum.Text.Trim.ToString) <= 12 Then

                        If cboBeginMonthyear.SelectedItem.ToString <> String.Empty AndAlso cboEndMonthyear.SelectedItem.ToString <> String.Empty Then

                            EndNumber = txtEndMonthNum.Text
                            BeginNumber = txtBeginMonthNum.Text
                            EndYear = cboEndMonthyear.SelectedValue
                            BeginYear = cboBeginMonthyear.SelectedValue
                            ForamatType = "Month"

                            ' BeginNumber = CStr(IIf(BeginNumber.StartsWith("0"), BeginNumber.Substring(1, 1).ToString(), BeginNumber))
                            'EndNumber = CStr(IIf(EndNumber.StartsWith("0"), EndNumber.Substring(1, 1).ToString(), EndNumber))

                            BeginDate = BeginNumber + "/" + "01" + "/" + BeginYear
                            EndDate = CStr(GetLastDayInMonth(DateHelper.GetDateValue(EndNumber + "/" + "01" + "/" + EndYear)))

                            ReportCeBase.ValidateBeginEndDate(moMBeginDateLabel, BeginDate, moMEndDateLabel, EndDate)
                            EndDate = ReportCeBase.FormatDate(moMEndDateLabel, EndDate)
                            BeginDate = ReportCeBase.FormatDate(moMBeginDateLabel, BeginDate)
                        Else
                            ElitaPlusPage.SetLabelError(lblMonthlySelect)
                            Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_MONTH_YEAR_SELECTION_ERROR)
                        End If
                    Else
                        ElitaPlusPage.SetLabelError(lblMonthlySelect)
                        Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_MONTH_SELECTION_ERROR)
                    End If

                Else
                    ElitaPlusPage.SetLabelError(lblMonthlySelect)
                    Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_INVALID_NUMBER)
                End If

            Else
                ElitaPlusPage.SetLabelError(lblWeeklySelect)
                Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_SELECTION_TYPE_REQUIRED)
            End If

            'Dealer
            If rdealer.Checked Then
                dealerCode = ALL
            Else
                If selectedDealerId.Equals(Guid.Empty) Then
                    ElitaPlusPage.SetLabelError(DealerMultipleDrop.CaptionLabel)
                    Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_DEALER_MUST_BE_SELECTED_ERR)
                End If
            End If

            Select Case rdReportSortOrder.SelectedValue()
                Case ZIP_CODE
                    sortOrder = "POSTAL_CODE"
                Case BRANCH_CODE
                    sortOrder = "BRANCH_CODE"
            End Select


            ReportCeBase.EnableReportCe(Me, TheRptCeInputControl)
            moReportFormat = ReportCeBase.GetReportFormat(Me)
            params = SetParameters(companyid, CompanyDesc, dealerCode, dealerDesc, BeginDate, EndDate, ForamatType, sortOrder)
            Session(ReportCeBaseForm.SESSION_PARAMETERS_KEY) = params
        End Sub

        Protected Sub moWeekBeginDateText_TextChanged(sender As Object, e As EventArgs) Handles moWeekBeginDateText.TextChanged
            Try
                txtBeginWeekNum.Text = String.Empty
                If moWeekBeginDateText.Text.Trim <> String.Empty Then
                    GUIException.ValidateDate(moWBeginDateLabel, moWeekBeginDateText.Text, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_DATE_ERR)
                    txtBeginWeekNum.Text = CStr(GetWeekNumber(DateHelper.GetDateValue(moWeekBeginDateText.Text)))
                    ControlMgr.SetVisibleControl(Me, txtBeginWeekNum, True)
                    ControlMgr.SetVisibleControl(Me, lblBeginWeekNum, True)
                    txtBeginWeekNum.Style.Add("display", "")
                    lblBeginWeekNum.Style.Add("display", "")
                    If moWeekEndDateText.Text.Trim <> String.Empty Then
                        txtEndWeekNum.Style.Add("display", "")
                        lblEndWeekNum.Style.Add("display", "")
                    Else
                        txtEndWeekNum.Style.Add("display", "none")
                        lblEndWeekNum.Style.Add("display", "none")
                    End If
                Else
                    txtBeginWeekNum.Text = String.Empty
                End If

            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try
        End Sub

        Protected Sub moWeekEndDateText_TextChanged(sender As Object, e As EventArgs) Handles moWeekEndDateText.TextChanged
            Try
                txtEndWeekNum.Text = String.Empty
                If moWeekEndDateText.Text.Trim <> String.Empty Then
                    GUIException.ValidateDate(moWEndDateLabel, moWeekEndDateText.Text, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_DATE_ERR)
                    txtEndWeekNum.Text = CStr(GetWeekNumber(DateHelper.GetDateValue(moWeekEndDateText.Text)))
                    ControlMgr.SetVisibleControl(Me, txtEndWeekNum, True)
                    ControlMgr.SetVisibleControl(Me, lblEndWeekNum, True)
                    txtEndWeekNum.Style.Add("display", "")
                    lblEndWeekNum.Style.Add("display", "")
                    If moWeekBeginDateText.Text.Trim <> String.Empty Then
                        txtBeginWeekNum.Style.Add("display", "")
                        lblBeginWeekNum.Style.Add("display", "")
                    Else
                        txtBeginWeekNum.Style.Add("display", "none")
                        lblBeginWeekNum.Style.Add("display", "none")
                    End If
                Else
                    txtEndWeekNum.Text = String.Empty
                End If
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try
        End Sub

        'Public Function GetWeekNumber(ByVal dtPassed As Date) As Integer

        '    Dim weekNum As Integer
        '    Dim ciCurr As CultureInfo
        '    ciCurr = CultureInfo.CurrentCulture
        '    'weekNum = ciCurr.Calendar.GetWeekOfYear(dtPassed, CalendarWeekRule.FirstFullWeek, DayOfWeek.Monday)
        '    ' weekNum = DatePart(DateInterval.WeekOfYear, dtPassed, FirstDayOfWeek.Monday, FirstWeekOfYear.FirstFullWeek)
        '    weekNum = GetWeekNumber(dtPassed)
        '    Return weekNum

        'End Function
        Public Function GetWeekDates(dtCurrrent As Date, dttype As String) As String
            Dim dtStartOfWeek As DateTime = dtCurrrent
            Dim strday As DayOfWeek
            Dim days As Integer
            Dim BeginDate As DateTime
            Dim EndDate As DateTime
            If dttype = "start" Then
                strday = dtStartOfWeek.DayOfWeek
                If strday <> 0 Then
                    days = strday - dtStartOfWeek.DayOfWeek.Monday
                    BeginDate = dtStartOfWeek.Date.AddDays(-days)
                Else
                    BeginDate = dtStartOfWeek.AddDays(-6)
                End If

                Return BeginDate.ToString
            Else
                strday = dtStartOfWeek.DayOfWeek
                If strday <> 0 Then
                    days = strday - dtStartOfWeek.DayOfWeek.Monday
                    BeginDate = dtStartOfWeek.Date.AddDays(-days)
                    EndDate = BeginDate.AddDays(6)
                Else
                    EndDate = dtStartOfWeek
                End If
                Return EndDate.ToString
            End If

        End Function
        Private Function GetLastDayInMonth(dDate As Date) As Date
            dDate = DateAdd(DateInterval.Month, 1, dDate)
            dDate = Convert.ToDateTime(Month(dDate).ToString() & "/" & "1/" & Year(dDate).ToString())
            dDate = DateAdd(DateInterval.Day, -1, dDate)
            Return dDate
        End Function
        Public Function GetWeekNumber(inDate As DateTime) As Integer
            Const JAN As Integer = 1
            Const DEC As Integer = 12
            Const LASTDAYOFDEC As Integer = 31
            Const FIRSTDAYOFJAN As Integer = 1
            Const THURSDAY As Integer = 4
            Dim ThursdayFlag As Boolean = False

            ' Get the day number since the beginning of the year
            Dim DayOfYear As Integer = inDate.DayOfYear

            ' Get the numeric weekday of the first day of the
            ' year (using sunday as FirstDay)
            Dim StartWeekDayOfYear As Integer = CInt(New DateTime(inDate.Year, JAN, FIRSTDAYOFJAN).DayOfWeek)
            Dim EndWeekDayOfYear As Integer = CInt(New DateTime(inDate.Year, DEC, LASTDAYOFDEC).DayOfWeek)

            ' Compensate for the fact that we are using monday
            ' as the first day of the week
            If StartWeekDayOfYear = 0 Then
                StartWeekDayOfYear = 7
            End If
            If EndWeekDayOfYear = 0 Then
                EndWeekDayOfYear = 7
            End If

            ' Calculate the number of days in the first and last week
            Dim DaysInFirstWeek As Integer = 8 - StartWeekDayOfYear
            Dim DaysInLastWeek As Integer = 8 - EndWeekDayOfYear

            ' If the year either starts or ends on a thursday it will have a 53rd week
            If StartWeekDayOfYear = THURSDAY OrElse EndWeekDayOfYear = THURSDAY Then
                ThursdayFlag = True
            End If

            ' We begin by calculating the number of FULL weeks between the start of the year and
            ' our date. The number is rounded up, so the smallest possible value is 0.
            Dim FullWeeks As Integer = _
                CType(Math.Ceiling((DayOfYear - DaysInFirstWeek) / 7), Integer)

            Dim WeekNumber As Integer = FullWeeks

            ' If the first week of the year has at least four days, then the actual week number for our date
            ' can be incremented by one.
            If DaysInFirstWeek >= THURSDAY Then
                WeekNumber = WeekNumber + 1
            End If

            ' If week number is larger than week 52 (and the year doesn't either start or end on a thursday)
            ' then the correct week number is 1.
            If WeekNumber > 52 AndAlso Not ThursdayFlag Then
                WeekNumber = 1
            End If

            'If week number is still 0, it means that we are trying to evaluate the week number for a
            'week that belongs in the previous year (since that week has 3 days or less in our date's year).
            'We therefore make a recursive call using the last day of the previous year.
            If WeekNumber = 0 Then
                WeekNumber = GetWeekNumber(New DateTime(inDate.Year - 1, DEC, LASTDAYOFDEC))
            End If
            Return WeekNumber
        End Function

#End Region

    End Class

End Namespace