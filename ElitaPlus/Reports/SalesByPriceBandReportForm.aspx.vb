Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Imports System.Globalization
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports System.Threading
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms

Namespace Reports
    Partial Public Class SalesByPriceBandReportForm
        Inherits ElitaPlusPage
#Region "Constants"

        Private Const RPT_FILENAME_WINDOW As String = "SALES_BY_PRICE_BAND"
        Private Const RPT_FILENAME As String = "SalesByPriceBand"
        Public Const ALL As String = "*"
        Public Const YES As String = "Y"
        Protected WithEvents btnViewHidden As System.Web.UI.WebControls.Button
        Public Const NO As String = "N"
        Private Const LABEL_SELECT_COMPANY As String = "SELECT_COMPANY"
        Private Const RPT_FILENAME_EXPORT As String = "SalesByPriceBand-Exp"

        Private Const LABEL_SELECT_DEALER As String = "OR A SINGLE DEALER"

        Private Const TOTALPARAMS As Integer = 12  ' 23
        Private Const TOTALEXPPARAMS As Integer = 12  ' 7
        Private Const PARAMS_PER_REPORT As Integer = 12 '8
        Private Const ONE_ITEM As Integer = 1
        Private Const WARRANTY_SALES_DATE As String = "W"
        Private Const ADDED_DATE As String = "A"

#End Region

#Region "parameters"
        Public Structure ReportParams
            Public companyCode As String
            Public companyDesc As String
            Public dealerCode As String
            Public dealerDesc As String
            Public dealergrpCode As String
            Public dealerGrpDesc As String
            Public includedealer As String
            Public beginDate As String
            Public endDate As String
            Public selecttype As String
            Public selectdatetype As String
            Public langCode As String
            Public culturevalue As String
        End Structure
#End Region

#Region "variables"
        Private moReportFormat As ReportCeBaseForm.RptFormat
        Dim odealergrp As DealerGroup
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
                If moUserDealerMultipleDrop Is Nothing Then
                    moUserDealerMultipleDrop = CType(FindControl("moDealerMultipleDrop"), MultipleColumnDDLabelControl)
                End If
                Return moUserDealerMultipleDrop
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
                    EnableOrDisableControls()
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

#Region "Handlers-DropDown"

        Private Sub OnFromDrop_Changed(ByVal fromMultipleDrop As Assurant.ElitaPlus.ElitaPlusWebApp.Common.MultipleColumnDDLabelControl) _
            Handles moUserCompanyMultipleDrop.SelectedDropChanged
            Try
                PopulateDealerDropDown()
            Catch ex As Exception
                HandleErrors(ex, Me.ErrorCtrl)
            End Try
        End Sub

        Private Sub EnableOrDisableControls()
            If Me.rGroup.Checked = True Then
                ControlMgr.SetEnableControl(Me, rIncludeDealer, True)
                ControlMgr.SetEnableControl(Me, rExcludeDealer, True)
            Else
                ControlMgr.SetEnableControl(Me, rIncludeDealer, False)
                ControlMgr.SetEnableControl(Me, rExcludeDealer, False)
            End If
            If rGroup.Checked = True Or cboDealerGroup.SelectedIndex > 0 Then
                DealerMultipleDrop.SelectedIndex = -1
            End If
        End Sub

#End Region

#End Region

#Region "Clear"

        Private Sub ClearErrLabels()
            Me.ClearLabelErrSign(moBeginDateLabel)
            Me.ClearLabelErrSign(moEndDateLabel)
            Me.ClearLabelErrSign(DealerMultipleDrop.CaptionLabel)
            Me.ClearLabelErrSign(UserCompanyMultipleDrop.CaptionLabel)
            If Me.rdealer.Checked Then DealerMultipleDrop.SelectedIndex = -1
            If Me.rGroup.Checked Then cboDealerGroup.SelectedIndex = -1
            Me.ClearLabelErrSign(lbldisplay)
        End Sub

#End Region

#Region "Populate"

        Private Sub PopulateCompaniesDropdown()
            Dim dv As DataView = LookupListNew.GetUserCompaniesLookupList()

            UserCompanyMultipleDrop.NothingSelected = True
            UserCompanyMultipleDrop.SetControl(True, UserCompanyMultipleDrop.MODES.NEW_MODE, True, dv, "* " + TranslationBase.TranslateLabelOrMessage(LABEL_SELECT_COMPANY), True, False)
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
                                              " document.forms[0].rdealer.checked = false;  document.forms[0].rGroup.checked = false; document.forms[0].cboDealerGroup.selectedIndex = -1; fncEnable1(2);",
                                              "moUserDealerMultipleDrop_moMultipleColumnDrop",
                                              "moUserDealerMultipleDrop_moMultipleColumnDropDesc", "moUserDealerMultipleDrop_lb_DropDown")
        End Sub

        Private Sub PopulateDealerGroup()
            'Me.BindListControlToDataView(cboDealerGroup, LookupListNew.GetDealerGroupLookupList(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id))
            Dim listcontext As ListContext = New ListContext()
            listcontext.CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id

            Dim dealerGroupLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("DealerGroupByCompanyGroup", Thread.CurrentPrincipal.GetLanguageCode(), listcontext)
            cboDealerGroup.Populate(dealerGroupLkl, New PopulateOptions() With
             {
            .AddBlankItem = True
             })
        End Sub

        Private Sub InitializeForm()

            PopulateCompaniesDropdown()
            PopulateDealerDropDown()
            PopulateDealerGroup()
            Dim t As Date = Date.Now.AddMonths(-1).AddDays(1)
            Me.moBeginDateText.Text = GetDateFormattedString(t)
            Me.moEndDateText.Text = GetDateFormattedString(Date.Now)
            Me.rdealer.Checked = True
            rSalesDate.Checked = True
            ControlMgr.SetEnableControl(Me, rIncludeDealer, False)
            ControlMgr.SetEnableControl(Me, rExcludeDealer, False)
            TheRptCeInputControl.ExcludeExport()
            TheRptCeInputControl.populateReportLanguages(RPT_FILENAME)
        End Sub

#End Region

#Region "Crystal Enterprise"

        Function SetParameters(ByVal companycode As String, ByVal companydesc As String, ByVal dealerCode As String, ByVal dealerDesc As String,
                               ByVal dealergrpCode As String, ByVal dealergrpdesc As String, ByVal includedealer As String, ByVal beginDate As String,
                                  ByVal endDate As String, ByVal selecttype As String, ByVal selectdatetype As String, ByVal langCode As String) As ReportCeBaseForm.Params

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
                .dealergrpCode = dealergrpCode
                .dealerGrpDesc = dealergrpdesc
                .includedealer = includedealer
                .beginDate = beginDate
                .endDate = endDate
                .selecttype = selecttype
                .selectdatetype = selectdatetype
                .culturevalue = culturevalue

            End With
            SetReportParams(rptParams, repParams, String.Empty, PARAMS_PER_REPORT * 0)  ' Main Report

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
                repParams(startIndex) = New ReportCeBaseForm.RptParam("V_COMPANY_CODE", .companyCode, reportName)
                repParams(startIndex + 1) = New ReportCeBaseForm.RptParam("V_COMPANY_DESC", .companyDesc, reportName)
                repParams(startIndex + 2) = New ReportCeBaseForm.RptParam("V_DEALER_CODE", .dealerCode, reportName)
                repParams(startIndex + 3) = New ReportCeBaseForm.RptParam("V_DEALER_DESC", .dealerDesc, reportName)
                repParams(startIndex + 4) = New ReportCeBaseForm.RptParam("V_DEALER_GRP_CODE", .dealergrpCode, reportName)
                repParams(startIndex + 5) = New ReportCeBaseForm.RptParam("V_DEALER_GRP_DESC", .dealerGrpDesc, reportName)
                repParams(startIndex + 6) = New ReportCeBaseForm.RptParam("V_INCLUDE_DEALER", .includedealer, reportName)
                repParams(startIndex + 7) = New ReportCeBaseForm.RptParam("V_BEGIN_DATE", .beginDate, reportName)
                repParams(startIndex + 8) = New ReportCeBaseForm.RptParam("V_END_DATE", .endDate, reportName)
                repParams(startIndex + 9) = New ReportCeBaseForm.RptParam("V_SELECT_TYPE", .selecttype, reportName)
                repParams(startIndex + 10) = New ReportCeBaseForm.RptParam("V_SELECT_DATE_TYPE", .selectdatetype, reportName)
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

            Dim params As ReportCeBaseForm.Params
            Dim isSummarybyCov As String

            Dim dealerGrpCode As String
            Dim dealerGrpDesc As String
            Dim dealerGroupId As Guid = Me.GetSelectedItem(Me.cboDealerGroup)
            Dim selecttype As String
            Dim selectdatetype As String
            Dim includedealer As String

            'Dates
            If Not moBeginDateText.Text.Trim.ToString = String.Empty AndAlso Not moEndDateText.Text.Trim.ToString = String.Empty Then
                ReportCeBase.ValidateBeginEndDate(moBeginDateLabel, moBeginDateText.Text, moEndDateLabel, moEndDateText.Text)
                endDate = ReportCeBase.FormatDate(moEndDateLabel, moEndDateText.Text)
                beginDate = ReportCeBase.FormatDate(moBeginDateLabel, moBeginDateText.Text)
            Else
                ElitaPlusPage.SetLabelError(Me.moBeginDateLabel)
                ElitaPlusPage.SetLabelError(Me.moEndDateLabel)
                Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_YEARMONTH_MUST_BE_SELECTED_ERR)
            End If

            'Date Type
            If Me.rSalesDate.Checked Then
                selectdatetype = WARRANTY_SALES_DATE
            Else
                selectdatetype = ADDED_DATE
            End If

            'Validating the Company selection
            If UserCompanyMultipleDrop.SelectedGuid.Equals(Guid.Empty) Then
                ElitaPlusPage.SetLabelError(UserCompanyMultipleDrop.CaptionLabel)
                Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_COMPANY_IS_REQUIRED)
            End If

            'Validating the Dealer selection
            If Me.rdealer.Checked Then
                dealerCode = ALL
            ElseIf Me.rGroup.Checked Then
                dealerGrpCode = ALL
            ElseIf Not selectedDealerId.Equals(Guid.Empty) Then
                dealerCode = DealerMultipleDrop.SelectedCode
                dealerDesc = DealerMultipleDrop.SelectedDesc
            ElseIf Not dealerGroupId.Equals(Guid.Empty) Then
                odealergrp = New DealerGroup(dealerGroupId)
                dealerGrpCode = odealergrp.Code
                dealerGrpDesc = odealergrp.Description
            ElseIf selectedDealerId.Equals(Guid.Empty) And dealerGroupId.Equals(Guid.Empty) Then
                ElitaPlusPage.SetLabelError(DealerMultipleDrop.CaptionLabel)
                Throw New GUIException(Message.MSG_INVALID_DEALER, Assurant.ElitaPlus.Common.ErrorCodes.GUI_DEALER_MUST_BE_SELECTED_ERR)
            End If

            'Include Dealer
            If Me.rIncludeDealer.Checked = True Then
                includedealer = YES
            Else
                includedealer = NO
            End If

            'display items selected
            Dim i As Integer
            For i = 0 To chkdisplay.Items.Count - 1
                If chkdisplay.Items(i).Selected Then
                    selecttype += chkdisplay.Items(i).Value + TheRptCeInputControl.numeric_cultureSep
                End If
            Next

            If selecttype Is Nothing Then
                ElitaPlusPage.SetLabelError(lbldisplay)
                Throw New GUIException(Message.MSG_INVALID_DEALER, Assurant.ElitaPlus.Common.ErrorCodes.AT_LEAST_ONE_FIELD_REQUIRED)
            Else
                selecttype = selecttype.Substring(0, selecttype.Length - 1)
            End If

            ReportCeBase.EnableReportCe(Me, TheRptCeInputControl)
            params = SetParameters(CompanyCode, CompanyDesc, dealerCode, dealerDesc, dealerGrpCode, dealerGrpDesc, includedealer, beginDate, endDate, selecttype, selectdatetype, langCode)

            Session(ReportCeBaseForm.SESSION_PARAMETERS_KEY) = params

        End Sub

#End Region
    End Class
End Namespace
