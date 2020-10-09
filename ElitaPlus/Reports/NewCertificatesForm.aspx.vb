Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Imports Assurant.ElitaPlus.DALObjects
Imports System.Threading
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms
Namespace Reports

    Partial Class NewCertificatesForm
        Inherits ElitaPlusPage

#Region "Constants"
        Protected WithEvents UserControlAvailableSelectedDealers As Assurant.ElitaPlus.ElitaPlusWebApp.Generic.UserControlAvailableSelected
        Private Const RPT_FILENAME_WINDOW As String = "NEW CERTIFICATES"
        Private Const RPT_FILENAME As String = "NewCertificates"
        Public Const PAGETITLEWITHWIRELESS As String = "NEW_CERTIFICATES_WITH_WIRELESS"
        ' Private Const RPT_FILENAME As String = "ExpiredCertificatesEnglishUSAdgfasf"
        Private Const RPT_FILENAME_EXPORT As String = "NewCertificates-Exp"

        Public Const ALL As String = "*"
        '  Public Const SP_DATE_FORMAT As String = "yyyyMMdd"
        Private Const DATE_ADDED As String = "A"
        Private Const DATE_SOLD As String = "S"

        Public Const YES As String = "Y"
        Protected WithEvents btnViewHidden As System.Web.UI.WebControls.Button
        Public Const NO As String = "N"

        Private Const LABEL_SELECT_DEALER As String = "OR A SINGLE DEALER"

        Private Const TOTALPARAMS As Integer = 23 '19 '17  ' 23
        Private Const TOTALEXPPARAMS As Integer = 12 '10  '9 '8  ' 7
        Private Const PARAMS_PER_REPORT As Integer = 12 '10 '9 '8 '8
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
            Public campaignNumber As String
            Public dateAddedSold As String
            Public dealersList As String
            Public dealerGroup As Guid
        End Structure
#End Region

#Region "variables"
        Private moReportFormat As ReportCeBaseForm.RptFormat
        Private queryStringCaller As String = String.Empty
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
#Region "Page State"

        Class MyState
            Public MyBO As ReportRequests
            Public IsNew As Boolean = False
            'Public IsACopy As Boolean
            'Public CompanyGroupIdId As Guid
            'Public ActionInProgress As DetailPageCommand = DetailPageCommand.Nothing_
            'Public LastErrMsg As String
            Public HasDataChanged As Boolean
            Public ForEdit As Boolean = False
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

        Private Sub Page_Init(sender As System.Object, e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

#Region "Handlers-Init"

        Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load ', Me.Load
            'Put user code to initialize the page here
            ErrorCtrl.Clear_Hide()
            Try
                If Not IsPostBack Then
                    InitializeForm()

                    'Date Calendars
                    AddCalendar(BtnBeginDate, moBeginDateText)
                    AddCalendar(BtnEndDate, moEndDateText)
                Else
                    ClearErrLabels()
                End If
                CheckQuerystringForWirelessReports()
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
                If (queryStringCaller.ToString().ToUpper() = "NCW") Then
                    GenerateReportWireless()
                Else
                    GenerateReport()
                End If

            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try
        End Sub

#End Region

#End Region

#Region "Clear"

        Private Sub ClearErrLabels()
            ClearLabelErrSign(moBeginDateLabel)
            ClearLabelErrSign(moEndDateLabel)
            ClearLabelErrSign(DealerMultipleDrop.CaptionLabel)
            If rdealer.Checked Then DealerMultipleDrop.SelectedIndex = -1
            ClearLabelErrSign(moCampaignNumberLabel)
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
            DealerMultipleDrop.SetControl(False, DealerMultipleDrop.MODES.NEW_MODE, True, dv, TranslationBase.TranslateLabelOrMessage(LABEL_SELECT_DEALER), True, True, " document.forms[0].rdealer.checked = false;document.forms[0].moDealerGroupList.selectedIndex=0;RemoveAllSelectedDealersForReports('UserControlAvailableSelectedDealers');")

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

        Sub PopulateDealerGroupDropDown()
            'Dim _dal As New AcctSettingDAL
            'Dim ds As DataSet

            'ds = _dal.LoadDealerGroups("", "", ElitaPlusIdentity.Current.ActiveUser.Company.CompanyGroupId)
            'Me.BindListControlToDataView(Me.moDealerGroupList, ds.Tables(0).DefaultView, , "Dealer_Group_ID", True)

            Dim DealerGroups As DataElements.ListItem() =
                CommonConfigManager.Current.ListManager.GetList(listCode:="DealerGroupByCompanyGroup",
                                                                context:=New ListContext() With
                                                                {
                                                                  .CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
                                                                })

            moDealerGroupList.Populate(DealerGroups.ToArray(),
                                        New PopulateOptions() With
                                        {
                                         .AddBlankItem = True
                                        })

        End Sub

        Sub populateDealersList()
            Dim dv As DataView = LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies, False, "CODE")

            UserControlAvailableSelectedDealers.SetAvailableData(dv, LookupListNew.COL_CODE_NAME, LookupListNew.COL_ID_NAME)
            ControlMgr.SetVisibleControl(Me, UserControlAvailableSelectedDealers, True)

        End Sub

        Private Sub InitializeForm()
            TheRptCeInputControl.populateReportLanguages(RPT_FILENAME)
            PopulateDealerDropDown()
            PopulateDealerGroupDropDown()
            populateDealersList()
            PopulateCampaignNumbersDropdown()
            Dim t As Date = Date.Now.AddMonths(-1).AddDays(1)
            moBeginDateText.Text = GetDateFormattedString(t)
            moEndDateText.Text = GetDateFormattedString(Date.Now)
            rdealer.Checked = True
            RadiobuttonTotalsOnly.Checked = True
        End Sub

#End Region

#Region "Crystal Enterprise"

        Function SetParameters(userId As String, dealerCode As String, beginDate As String,
                                  endDate As String, isSummary As String, sCurrency As String, totalsByCov As String, campaign As String, dateAddedSold As String,
                                  dealerGroup As Guid, dealersList As String) As ReportCeBaseForm.Params

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
                .campaignNumber = campaign
                .dateAddedSold = dateAddedSold
                .dealerGroup = dealerGroup
                .dealersList = dealersList
            End With
            SetReportParams(rptParams, repParams, String.Empty, PARAMS_PER_REPORT * 0)     ' Main Report
            rptParams.isSummary = "Y"

            rptWindowTitle.InnerText = TheRptCeInputControl.getReportWindowTitle(TranslationBase.TranslateLabelOrMessage(RPT_FILENAME_WINDOW))

            With Params
                .msRptName = reportName
                .msRptWindowName = TranslationBase.TranslateLabelOrMessage(RPT_FILENAME_WINDOW)
                .moRptFormat = moReportFormat
                .moAction = ReportCeBaseForm.RptAction.SCHEDULE_VIEW
                .moRptParams = repParams
            End With

            Return Params
        End Function

        Function SetExpParameters(userId As String, dealerCode As String, beginDate As String,
                                  endDate As String, isSummary As String, sCurrency As String, totalsByCov As String, campaign As String, dateAddedSold As String,
                                  dealerGroup As Guid, dealersList As String) As ReportCeBaseForm.Params

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
                .isSummary = isSummary
                .sCurrency = sCurrency
                .totalsByCov = totalsByCov
                .culturevalue = culturevalue
                .campaignNumber = campaign
                .dateAddedSold = dateAddedSold
                .dealerGroup = dealerGroup
                .dealersList = dealersList
            End With
            SetReportParams(rptParams, repParams, String.Empty, PARAMS_PER_REPORT * 0)     ' Main Report

            rptWindowTitle.InnerText = TheRptCeInputControl.getReportWindowTitle(TranslationBase.TranslateLabelOrMessage(RPT_FILENAME_WINDOW))

            With Params
                .msRptName = reportName
                .msRptWindowName = TranslationBase.TranslateLabelOrMessage(RPT_FILENAME_WINDOW)
                .moRptFormat = moReportFormat
                .moAction = ReportCeBaseForm.RptAction.SCHEDULE_VIEW
                .moRptParams = repParams
            End With

            Return Params
        End Function

        Sub SetReportParams(rptParams As ReportParams, repParams() As ReportCeBaseForm.RptParam,
                          reportName As String, startIndex As Integer)
            With rptParams
                repParams(startIndex) = New ReportCeBaseForm.RptParam("V_USER_ID", .userId, reportName)
                repParams(startIndex + 1) = New ReportCeBaseForm.RptParam("V_DEALER_CODE", .dealerCode, reportName)
                repParams(startIndex + 2) = New ReportCeBaseForm.RptParam("V_BEGIN_DATE", .beginDate, reportName)
                repParams(startIndex + 3) = New ReportCeBaseForm.RptParam("V_END_DATE", .endDate, reportName)
                repParams(startIndex + 4) = New ReportCeBaseForm.RptParam("P_CURRENCY", .sCurrency, reportName)
                repParams(startIndex + 5) = New ReportCeBaseForm.RptParam("V_IS_SUMMARY", .isSummary, reportName)
                repParams(startIndex + 6) = New ReportCeBaseForm.RptParam("IS_TOTALSBYCOV", .totalsByCov, reportName)
                repParams(startIndex + 7) = New ReportCeBaseForm.RptParam("LANG_CULTURE_VALUE", .culturevalue, reportName)
                repParams(startIndex + 8) = New ReportCeBaseForm.RptParam("V_CAMPAIGN_NUMBER", .campaignNumber, reportName)
                repParams(startIndex + 9) = New ReportCeBaseForm.RptParam("V_ADDED_SOLD", .dateAddedSold, reportName)
                repParams(startIndex + 10) = New ReportCeBaseForm.RptParam("V_DEALER_GROUP", GuidControl.GuidToHexString(.dealerGroup), reportName)
                repParams(startIndex + 11) = New ReportCeBaseForm.RptParam("V_DEALER_LIST", .dealersList, reportName)
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
            Dim selectedCampaign As String = GetSelectedDescription(cboCampaignNumber)
            Dim dateAddedSold As String
            Dim dealerGroup As Guid = Guid.Empty
            Dim dealersList As String

            'Currency
            Dim Currencyid As Guid = oCountry.PrimaryCurrencyId
            Dim strCurrency As String = LookupListNew.GetDescriptionFromId("CURRENCIES", Currencyid)

            'Dates
            ReportCeBase.ValidateBeginEndDate(moBeginDateLabel, moBeginDateText.Text, moEndDateLabel, moEndDateText.Text)
            endDate = ReportCeBase.FormatDate(moEndDateLabel, moEndDateText.Text)
            beginDate = ReportCeBase.FormatDate(moBeginDateLabel, moBeginDateText.Text)

            If RadiobuttonTotalsOnly.Checked Then
                isSummary = YES
            Else
                isSummary = NO
            End If

            If rdealer.Checked Then
                dealerCode = ALL
            End If
            'Else
            '    If selectedDealerId.Equals(Guid.Empty) Then
            '        ElitaPlusPage.SetLabelError(DealerMultipleDrop.CaptionLabel)
            '        Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_DEALER_MUST_BE_SELECTED_ERR)
            '    End If
            'End If

            'Dealer Group and individual Dealers list
            dealerGroup = New Guid(moDealerGroupList.SelectedValue)
            Dim arrDealerList As ArrayList = UserControlAvailableSelectedDealers.SelectedListCodes()
            Dim tempList As String
            For Each s As String In arrDealerList
                tempList = tempList & s & ";"
            Next
            dealersList = tempList

            If (dealersList Is Nothing AndAlso dealerGroup.Equals(Guid.Empty) AndAlso dealerCode = String.Empty AndAlso Not rdealer.Checked) Then
                'ElitaPlusPage.SetLabelError(DealerMultipleDrop.CaptionLabel)
                Throw New GUIException(Message.MSG_INVALID_DEALER, Assurant.ElitaPlus.Common.ErrorCodes.GUI_DEALER_MUST_BE_SELECTED_ERR)
            End If

            If rbCampaignNumber.Checked Then
                selectedCampaign = ALL
            Else
                If selectedCampaign.Equals(String.Empty) Then
                    ElitaPlusPage.SetLabelError(moCampaignNumberLabel)
                    Throw New GUIException(Message.MSG_INVALID_DEALER, Assurant.ElitaPlus.Common.ErrorCodes.GUI_CAMPAIGN_MUST_BE_SELECTED_ERR)
                End If
            End If

            If RadiobuttonSold.Checked Then
                dateAddedSold = DATE_SOLD
            Else
                dateAddedSold = DATE_ADDED
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

                params = SetExpParameters(GuidControl.GuidToHexString(userId), dealerCode, beginDate, endDate, isSummary, strCurrency, totalsByCov, selectedCampaign, dateAddedSold, dealerGroup, dealersList)
            Else
                'View Report
                params = SetParameters(GuidControl.GuidToHexString(userId), dealerCode, beginDate, endDate, isSummary, strCurrency, totalsByCov, selectedCampaign, dateAddedSold, dealerGroup, dealersList)
            End If
            Session(ReportCeBaseForm.SESSION_PARAMETERS_KEY) = params

        End Sub

#End Region

        Private Sub CheckQuerystringForWirelessReports()
            'ShowHideControls(False)
            'Me.SetFormTitle(PAGETITLE)

            If (Request.QueryString("CALLER") IsNot Nothing) Then
                If (Request.QueryString("CALLER") = "NCW") Then
                    queryStringCaller = Request.QueryString("CALLER")
                    SetFormTitle(PAGETITLEWITHWIRELESS)
                    ShowHideControls(True)
                End If
            End If
        End Sub
        Private Sub GenerateReportWireless()

            Dim reportParams As New System.Text.StringBuilder

            Dim userId As Guid = ElitaPlusIdentity.Current.ActiveUser.Id
            Dim selectedDealerId As Guid = DealerMultipleDrop.SelectedGuid 'Me.GetSelectedItem(Me.cboDealerDec)
            Dim dealerCode As String = DealerMultipleDrop.SelectedCode 'LookupListNew.GetCodeFromId(dv, selectedDealerId)
            Dim endDate As String
            Dim beginDate As String
            Dim oCountryId As Guid = ElitaPlusIdentity.Current.ActiveUser.Country(ElitaPlusIdentity.Current.ActiveUser.FirstCompanyID).Id
            Dim oCountry As New Country(oCountryId)
            Dim totalsByCov As String
            Dim selectedCampaign As String = GetSelectedDescription(cboCampaignNumber)
            Dim dateAddedSold As String
            Dim dealerGroup As Guid = Guid.Empty
            Dim dealersList As String

            'Currency
            Dim Currencyid As Guid = oCountry.PrimaryCurrencyId
            Dim strCurrency As String = LookupListNew.GetDescriptionFromId("CURRENCIES", Currencyid)

            'Dates
            ReportCeBase.ValidateBeginEndDate(moBeginDateLabel, moBeginDateText.Text, moEndDateLabel, moEndDateText.Text)
            endDate = ReportCeBase.FormatDate(moEndDateLabel, moEndDateText.Text)
            beginDate = ReportCeBase.FormatDate(moBeginDateLabel, moBeginDateText.Text)




            If rdealer.Checked Then
                dealerCode = ALL
            End If

            'Dealer Group and individual Dealers list
            dealerGroup = New Guid(moDealerGroupList.SelectedValue)

            Dim arrDealerList As ArrayList = UserControlAvailableSelectedDealers.SelectedListCodes()
            Dim tempList As String
            For Each s As String In arrDealerList
                tempList = tempList & s & ";"
            Next
            dealersList = tempList


            If (dealersList Is Nothing AndAlso dealerGroup.Equals(Guid.Empty) AndAlso dealerCode = String.Empty AndAlso Not rdealer.Checked) Then
                'ElitaPlusPage.SetLabelError(DealerMultipleDrop.CaptionLabel)
                Throw New GUIException(Message.MSG_INVALID_DEALER, Assurant.ElitaPlus.Common.ErrorCodes.GUI_DEALER_MUST_BE_SELECTED_ERR)
            End If

            If rbCampaignNumber.Checked Then
                selectedCampaign = ALL
            Else
                If selectedCampaign.Equals(String.Empty) Then
                    ElitaPlusPage.SetLabelError(moCampaignNumberLabel)
                    Throw New GUIException(Message.MSG_INVALID_DEALER, Assurant.ElitaPlus.Common.ErrorCodes.GUI_CAMPAIGN_MUST_BE_SELECTED_ERR)
                End If
            End If

            If RadiobuttonSold.Checked Then
                dateAddedSold = DATE_SOLD
            Else
                dateAddedSold = DATE_ADDED
            End If

            If chkTotalsPageByCov.Checked = True Then
                totalsByCov = YES
            Else
                totalsByCov = NO
            End If

            reportParams.AppendFormat("pi_user_id => '{0}',", GuidControl.GuidToHexString(userId))
            reportParams.AppendFormat("pi_dealer_code => '{0}',", dealerCode)
            reportParams.AppendFormat("pi_begin_date => '{0}',", beginDate)
            reportParams.AppendFormat("pi_end_date => '{0}',", endDate)
            reportParams.AppendFormat("pi_is_summary => '{0}',", "N")
            reportParams.AppendFormat("pi_currency => '{0}',", strCurrency)
            reportParams.AppendFormat("pi_is_totalsbycov => '{0}',", totalsByCov)
            reportParams.AppendFormat("pi_campaign_number => '{0}',", selectedCampaign)
            reportParams.AppendFormat("pi_added_sold => '{0}',", dateAddedSold)
            reportParams.AppendFormat("pi_dealer_group => '{0}',", GuidControl.GuidToHexString(dealerGroup))
            reportParams.AppendFormat("pi_dealer_List => '{0}'", dealersList)

            State.MyBO = New ReportRequests
            State.ForEdit = True
            PopulateBOProperty(State.MyBO, "ReportType", "NEW_CERTIFICATES_WIRELESS_REPORT")
            PopulateBOProperty(State.MyBO, "ReportProc", "r_newcertificateswireless.Report")
            PopulateBOProperty(State.MyBO, "ReportParameters", reportParams.ToString())
            PopulateBOProperty(State.MyBO, "UserEmailAddress", ElitaPlusIdentity.Current.EmailAddress)

            ScheduleReport()

        End Sub

#Region "visibility control logic"
        Private Sub ShowHideControls(show As Boolean)
            If (show) Then
                TheRptCeInputControl.SetExportOnly()
                TheRptCeInputControl.ViewVisible = False
                TheRptCeInputControl.PdfVisible = False
                TheRptCeInputControl.ExportDataVisible = True
                TheRptCeInputControl.DestinationVisible = False
                btnGenRpt.Text = TranslationBase.TranslateLabelOrMessage("GENERATE_REPORT_REQUEST") '"Generate Report Request"
                btnGenRpt.Width = 200
                Label7.Text = TranslationBase.TranslateLabelOrMessage("NEW_CERTIFICATES_WIRELESS")

            End If
        End Sub
#End Region

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
    End Class

End Namespace