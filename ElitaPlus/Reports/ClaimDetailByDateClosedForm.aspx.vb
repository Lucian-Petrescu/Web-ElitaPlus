Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Imports Microsoft.VisualBasic
Imports System.Globalization
Imports System.Threading
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.Web.Forms

Namespace Reports
    Partial Class ClaimDetailByDateClosedForm
        Inherits ElitaPlusPage

#Region "Constants"

        Private Const RPT_FILENAME_WINDOW As String = "CLAIM DETAIL BY DATE CLOSED"
        Private Const RPT_FILENAME As String = "ClaimDetailByDateClosed"
        Private Const RPT_FILENAME_EXPORT As String = "ClaimDetailByDateClosed-Exp"

        'Req-818 Quick changes for this requirement to show seperate report for China and rest of the countries.
        Private Const RPT_FILENAME_CHINA As String = "ClaimDetailByDateClosedCN"
        Private Const RPT_FILENAME_CHINA_EXPORT As String = "ClaimDetailByDateClosedCN-Exp"

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
        '    Public Const SP_DATE_FORMAT As String = "yyyyMMdd"
        Public Const YES As String = "Y"
        Public Const NO As String = "N"
        Public Const None As Integer = 0

#End Region

#Region "variables"
        Private moReportFormat As ReportCeBaseForm.RptFormat
        ' Private reportName As String = RPT_FILENAME
        Dim selectedCountryId As Guid

        'Req-818 changes for two reports
        Dim oCountry As Country
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
#End Region

#Region "Handlers"

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub
        Protected WithEvents isProgressVisible As System.Web.UI.HtmlControls.HtmlInputHidden
        Protected WithEvents isReportCeVisible As System.Web.UI.HtmlControls.HtmlInputHidden
        Protected WithEvents moReportCeStatus As System.Web.UI.HtmlControls.HtmlInputHidden
        Protected WithEvents ErrorCtrl As ErrorController
        Protected WithEvents moReportCeInputControl As ReportCeInputControl

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
            PopulateDropDowns()
            Dim t As Date = Date.Now.AddDays(-1)
            Me.moBeginDateText.Text = GetDateFormattedString(t)
            Me.moEndDateText.Text = GetDateFormattedString(Date.Now)
            Me.rCountry.Checked = True
            Me.rDealer.Checked = True
            Me.rSvcCtr.Checked = True
            Me.rRiskType.Checked = True
            Me.rMethodofRepair.Checked = True
            Me.rCoverageType.Checked = True
            Me.rReasonClosed.Checked = True
        End Sub


        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            Me.ErrorCtrl.Clear_Hide()

            'Req-818 changes for two reports
            oCountry = ElitaPlusIdentity.Current.ActiveUser.Country(ElitaPlusIdentity.Current.ActiveUser.CompanyId)

            Try
                If Not Me.IsPostBack Then
                    InitializeForm()

                    'Req-818 changes for two reports
                    If oCountry.Code.ToUpper = "CN" And Not Request.QueryString("REPORT_SERVER").Equals("SSRS") Then
                        TheRptCeInputControl.populateReportLanguages(RPT_FILENAME_CHINA)
                    Else
                        TheRptCeInputControl.populateReportLanguages(RPT_FILENAME)
                    End If

                    'Date Calendars
                    Me.AddCalendar(Me.BtnBeginDate, Me.moBeginDateText)
                    Me.AddCalendar(Me.BtnEndDate, Me.moEndDateText)
                Else
                    ClearErrLabels()
                End If
                Me.InstallProgressBar()
                chkIncColumns.Visible = False
                lblIncColumns.Visible = False
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

#Region "Handlers-DropDowns"

        Private Sub cboCountry_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCountry.SelectedIndexChanged
            Try
                selectedCountryId = Me.GetSelectedItem(Me.cboCountry)
                If selectedCountryId.Equals(Guid.Empty) Then
                    'ElitaPlusPage.SetLabelError(moCountryLabel)
                    Throw New GUIException(Message.MSG_INVALID_COUNTRY, Assurant.ElitaPlus.Common.ErrorCodes.GUI_COUNTRY_MUST_BE_SELECTED_ERR)
                Else
                    Dim oListContext As New Assurant.Elita.CommonConfiguration.ListContext
                    oListContext.CountryId = selectedCountryId
                    Dim oServiceCenterListForCountry As Assurant.Elita.CommonConfiguration.DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="ServiceCenterListByCountry", context:=oListContext)
                    Me.cboSvcCtr.Populate(oServiceCenterListForCountry, New PopulateOptions() With
                                                   {
                                                    .AddBlankItem = True
                                                   })
                    'Me.BindListControlToDataView(Me.cboSvcCtr, LookupListNew.GetServiceCenterLookupList(selectedCountryId), , , True)
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try
        End Sub

        Private Sub rCountry_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rCountry.CheckedChanged
            If rCountry.Checked = True Then
                cboCountry.SelectedIndex = None
                PopulateSvcCtrDropDown()
            End If
        End Sub
#End Region


#End Region

#Region "Clear"

        Private Sub ClearErrLabels()
            Me.ClearLabelErrSign(moBeginDateLabel)
            Me.ClearLabelErrSign(moEndDateLabel)
            Me.ClearLabelErrSign(moDealerLabel)
            Me.ClearLabelErrSign(SvcCtrLabel)
            Me.ClearLabelErrSign(RiskTypeLabel)
            Me.ClearLabelErrSign(lblMethodofRepair)
            Me.ClearLabelErrSign(lblCoverageType)
            Me.ClearLabelErrSign(lblReasonClosed)
        End Sub
#End Region

#Region "Populate"

        Private Sub PopulateDropDowns()
            PopulateCountryDropDown()
            PopulateDealerDropDown()
            PopulateSvcCtrDropDown()
            PopulateRiskTypeDropDown()
            'PopulateClaimTypeDropDown()
            PopulateMethodOfRepairDropDown()
            PopulateCoverageTypeDropDown()
            PopulateReasonsClosedDropDown()
        End Sub
        Private Sub PopulateCountryDropDown()
            Dim countryList As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="Country")
            Dim filteredCountryList As DataElements.ListItem() = (From x In countryList
                                                                  Where ElitaPlusIdentity.Current.ActiveUser.Countries.Contains(x.ListItemId)
                                                                  Select x).ToArray()
            Me.cboCountry.Populate(filteredCountryList.ToArray(), New PopulateOptions() With
                                                   {
                                                    .AddBlankItem = True
                                                   })
            'Me.BindListControlToDataView(Me.cboCountry, LookupListNew.GetUserCountriesLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies), , , True)
            If cboCountry.Items.Count < 3 Then
                HideHtmlElement("ddHideRow")
                ControlMgr.SetVisibleControl(Me, rCountry, False)
                ControlMgr.SetVisibleControl(Me, moCountryLabel, False)
                'ControlMgr.SetVisibleControl(Me, moCountryColonLabel_NO_TRANSLATE, False)
                ControlMgr.SetVisibleControl(Me, cboCountry, False)
            End If
        End Sub
        Private Sub PopulateDealerDropDown()
            Dim oListContext As New Assurant.Elita.CommonConfiguration.ListContext
            Dim oDealerList As New Collections.Generic.List(Of Assurant.Elita.CommonConfiguration.DataElements.ListItem)
            For Each _company As Guid In ElitaPlusIdentity.Current.ActiveUser.Companies
                oListContext.CompanyId = _company
                Dim oDealerListForCompany As Assurant.Elita.CommonConfiguration.DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="DealerListByCompany", context:=oListContext)
                If oDealerListForCompany.Count > 0 Then
                    If Not oDealerList Is Nothing Then
                        oDealerList.AddRange(oDealerListForCompany)
                    Else
                        oDealerList = oDealerListForCompany.Clone()
                    End If
                End If
            Next
            Me.cboDealer.Populate(oDealerList.ToArray(), New PopulateOptions() With
                {
                    .AddBlankItem = True
                })

            'Me.BindListControlToDataView(Me.cboDealer, LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies, False, "CODE"), , , True)
        End Sub

        Private Sub PopulateSvcCtrDropDown()
            Dim oListContext As New Assurant.Elita.CommonConfiguration.ListContext
            Dim oServiceCenterList As New Collections.Generic.List(Of Assurant.Elita.CommonConfiguration.DataElements.ListItem)
            For Each _country As Guid In ElitaPlusIdentity.Current.ActiveUser.Countries
                oListContext.CountryId = _country
                Dim oServiceCenterListForCountry As Assurant.Elita.CommonConfiguration.DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="ServiceCenterListByCountry", context:=oListContext)
                If oServiceCenterListForCountry.Count > 0 Then
                    If Not oServiceCenterList Is Nothing Then
                        oServiceCenterList.AddRange(oServiceCenterListForCountry)
                    Else
                        oServiceCenterList = oServiceCenterListForCountry.Clone()
                    End If
                End If
            Next

            Me.cboSvcCtr.Populate(oServiceCenterList.ToArray(), New PopulateOptions() With
                {
                    .AddBlankItem = True
                })
            'Me.BindListControlToDataView(Me.cboSvcCtr, LookupListNew.GetServiceCenterLookupList(ElitaPlusIdentity.Current.ActiveUser.Countries), , , True)
        End Sub

        Private Sub PopulateRiskTypeDropDown()
            Dim oListContext As New Assurant.Elita.CommonConfiguration.ListContext
            oListContext.CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
            Dim oRiskTypeListForCountry As Assurant.Elita.CommonConfiguration.DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="RiskTypeByCompanyGroup", context:=oListContext)
            Me.cboRiskType.Populate(oRiskTypeListForCountry.ToArray(), New PopulateOptions() With
                {
                    .AddBlankItem = True
                })
            'Me.BindListControlToDataView(Me.cboRiskType, LookupListNew.GetRiskTypeLookupList(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id), , , True)
        End Sub

        Private Sub PopulateMethodOfRepairDropDown()
            Dim oMethodOfRepair As Assurant.Elita.CommonConfiguration.DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="METHR", languageCode:=Thread.CurrentPrincipal.GetLanguageCode())
            Me.cboMethodofRepair.Populate(oMethodOfRepair.ToArray(), New PopulateOptions() With
                {
                    .AddBlankItem = True
                })
            'Me.BindListControlToDataView(Me.cboMethodofRepair, LookupListNew.GetMethodOfRepairLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), , , True)
        End Sub

        Private Sub PopulateCoverageTypeDropDown()
            Dim oListContext As New Assurant.Elita.CommonConfiguration.ListContext
            oListContext.CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
            Dim oCoverageTypeList As Assurant.Elita.CommonConfiguration.DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="CoverageTypeByCompanyGroup", languageCode:=Thread.CurrentPrincipal.GetLanguageCode(), context:=oListContext)
            Me.cboCoverageType.Populate(oCoverageTypeList.ToArray(), New PopulateOptions() With
                {
                    .AddBlankItem = True
                })
            'Me.BindListControlToDataView(Me.cboCoverageType, LookupListNew.GetCoverageTypeByCompanyGroupLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId, ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id), , , True)
        End Sub
        Private Sub PopulateReasonsClosedDropDown()
            'RESCL
            Dim oReasonClosed As Assurant.Elita.CommonConfiguration.DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="RESCL", languageCode:=Thread.CurrentPrincipal.GetLanguageCode())
            Me.cboReasonClosed.Populate(oReasonClosed.ToArray(), New PopulateOptions() With
                {
                    .AddBlankItem = True
                })
            'Me.BindListControlToDataView(Me.cboReasonClosed, LookupListNew.GetReasonClosedLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), , , True)
        End Sub
#End Region

#Region "Crystal Enterprise"

        Function SetParameters(ByVal userId As String, ByVal countryCode As String, ByVal langCode As String, ByVal dealerCode As String, ByVal svcCtrCode As String, ByVal riskTypeDescription As String, ByVal RepairTypeDesc As String, ByVal CoverageTypeDesc As String,
                                 ByVal ReasonClosedDesc As String, ByVal beginDate As String, ByVal endDate As String, ByVal svcCtrName As String, ByVal freeZone As String, ByVal sortBy As String,
                                 ByVal AddColumns As String) As ReportCeBaseForm.Params

            Dim reportFormat As ReportCeBaseForm.RptFormat
            Dim culturecode As String = TheRptCeInputControl.getCultureValue(False)
            'Req-818 changes for two reports
            Dim reportName As String
            If oCountry.Code.ToUpper = "CN" And Not Request.QueryString("REPORT_SERVER").Equals("SSRS") Then
                reportName = TheRptCeInputControl.getReportName(RPT_FILENAME_CHINA, False)
            Else
                reportName = TheRptCeInputControl.getReportName(RPT_FILENAME, False)
            End If

            Dim params As New ReportCeBaseForm.Params

            reportFormat = ReportCeBase.GetReportFormat(Me)
            If (reportFormat = ReportCeBase.RptFormat.TEXT_TAB OrElse
                reportFormat = ReportCeBase.RptFormat.TEXT_CSV) Then
                'Req-818 changes for two reports
                If oCountry.Code.ToUpper = "CN" And Not Request.QueryString("REPORT_SERVER").Equals("SSRS") Then
                    reportName = TheRptCeInputControl.getReportName(RPT_FILENAME_CHINA_EXPORT, True)
                Else
                    reportName = TheRptCeInputControl.getReportName(RPT_FILENAME_EXPORT, True)
                End If

                culturecode = TheRptCeInputControl.getCultureValue(True)
            End If

            Me.rptWindowTitle.InnerText = TheRptCeInputControl.getReportWindowTitle(TranslationBase.TranslateLabelOrMessage(RPT_FILENAME_WINDOW))



            Dim repParams() As ReportCeBaseForm.RptParam = New ReportCeBaseForm.RptParam() _
                    {
                     New ReportCeBaseForm.RptParam("V_USER_KEY", userId),
                     New ReportCeBaseForm.RptParam("V_BEGIN_DATE", beginDate),
                     New ReportCeBaseForm.RptParam("V_END_DATE", endDate),
                     New ReportCeBaseForm.RptParam("V_COUNTRY", countryCode),
                     New ReportCeBaseForm.RptParam("V_DEALER", dealerCode),
                     New ReportCeBaseForm.RptParam("V_SERVICE_CENTER_CODE", svcCtrCode),
                     New ReportCeBaseForm.RptParam("V_RISK_TYPE_NAME", riskTypeDescription),
                     New ReportCeBaseForm.RptParam("V_REPAIR_TYPE", RepairTypeDesc),
                     New ReportCeBaseForm.RptParam("V_COVERAGE_TYPE", CoverageTypeDesc),
                     New ReportCeBaseForm.RptParam("V_REASON_CLOSED", ReasonClosedDesc),
                     New ReportCeBaseForm.RptParam("V_LANGUAGE", langCode),
                     New ReportCeBaseForm.RptParam("V_FREE_ZONE_FLAG", freeZone),
                     New ReportCeBaseForm.RptParam("V_SORT_BY", sortBy),
                     New ReportCeBaseForm.RptParam("SERVICE_CENTER_NAME", svcCtrName),
                     New ReportCeBaseForm.RptParam("V_ADD_COLUMNS", AddColumns),
                     New ReportCeBaseForm.RptParam("V_LANG_CULTURE_CODE", culturecode)}

            With params
                .msRptName = reportName
                .msRptWindowName = TranslationBase.TranslateLabelOrMessage(RPT_FILENAME_WINDOW)
                .moRptFormat = reportFormat
                .moAction = ReportCeBaseForm.RptAction.SCHEDULE_VIEW
                .moRptParams = repParams
            End With
            Return params
        End Function

        Private Sub GenerateReport()
            Dim userId As String = GuidControl.GuidToHexString(ElitaPlusIdentity.Current.ActiveUser.Id)
            Dim langId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
            Dim langCode As String = LookupListNew.GetCodeFromId("LANGUAGES", langId)
            Dim selectedDealerId As Guid = Me.GetSelectedItem(Me.cboDealer)
            Dim dvDealer As DataView = LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies, False, "CODE")
            Dim dealerCode As String = LookupListNew.GetCodeFromId(dvDealer, selectedDealerId)
            Dim selectedSvcCtrId As Guid = Me.GetSelectedItem(Me.cboSvcCtr)
            Dim dvSvcCtr As DataView = LookupListNew.GetServiceCenterLookupList(ElitaPlusIdentity.Current.ActiveUser.Country(ElitaPlusIdentity.Current.ActiveUser.FirstCompanyID).Id)
            Dim svcCtrCode As String = LookupListNew.GetCodeFromId(dvSvcCtr, selectedSvcCtrId)
            Dim svcCtrName As String = LookupListNew.GetDescriptionFromId(dvSvcCtr, selectedSvcCtrId)
            Dim selectedRiskTypeId As Guid = Me.GetSelectedItem(Me.cboRiskType)
            Dim dvRiskType As DataView = LookupListNew.GetRiskTypeLookupList(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id)
            Dim riskTypeDescription As String = LookupListNew.GetDescriptionFromId(dvRiskType, selectedRiskTypeId)
            'Dim selectedClaimTypeId As Guid = Me.GetSelectedItem(Me.cboClaimType)
            'Dim dvClaimType As DataView = LookupListNew.GetClaimTypeLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId)
            'Dim claimTypeCode As String = LookupListNew.GetCodeFromId(dvClaimType, selectedClaimTypeId)
            Dim selectedRepairTypeId As Guid = Me.GetSelectedItem(Me.cboMethodofRepair)
            Dim dvRepairType As DataView = LookupListNew.GetMethodOfRepairLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId)
            Dim RepairTypeDesc As String = LookupListNew.GetDescriptionFromId(dvRepairType, selectedRepairTypeId)
            Dim sortBy As String = Me.rdReportSortOrder.SelectedValue
            selectedCountryId = Me.GetSelectedItem(Me.cboCountry)
            Dim dvCountry As DataView = LookupListNew.GetUserCountriesLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies)
            Dim countryCode As String = LookupListNew.GetDescriptionFromId(dvCountry, selectedCountryId)
            Dim selectionType As Integer
            Dim endDate As String
            Dim beginDate As String
            Dim selectedCoverageTypeId As Guid = Me.GetSelectedItem(Me.cboCoverageType)
            Dim dvCoverageType As DataView = LookupListNew.GetCoverageTypeByCompanyGroupLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId, ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id)
            Dim CoverageTypeDesc As String = LookupListNew.GetDescriptionFromId(dvCoverageType, selectedCoverageTypeId)
            Dim AddColumns As String

            Dim selectedReasonClosedId As Guid = Me.GetSelectedItem(Me.cboReasonClosed)
            Dim dvReasonClosed As DataView = LookupListNew.GetReasonClosedLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId)
            Dim ReasonClosedDesc As String = LookupListNew.GetDescriptionFromId(dvReasonClosed, selectedReasonClosedId)

            'Dates
            ReportCeBase.ValidateBeginEndDate(moBeginDateLabel, moBeginDateText.Text, moEndDateLabel, moEndDateText.Text)
            endDate = ReportCeBase.FormatDate(moEndDateLabel, moEndDateText.Text)
            beginDate = ReportCeBase.FormatDate(moBeginDateLabel, moBeginDateText.Text)

            'DEF-1827
            'Include Columns
            '           If chkIncColumns.Checked Then
            'Req-818 changes for two reports
            If oCountry.Code.ToUpper = "CN" Then
                AddColumns = YES
            Else
                AddColumns = NO
            End If

            'Else
            'AddColumns = NO
            'End If

            If Me.rCountry.Checked Then
                countryCode = ALL
            Else
                If selectedCountryId.Equals(Guid.Empty) Then
                    ElitaPlusPage.SetLabelError(moCountryLabel)
                    Throw New GUIException(Message.MSG_INVALID_COUNTRY, Assurant.ElitaPlus.Common.ErrorCodes.GUI_COUNTRY_MUST_BE_SELECTED_ERR)
                End If
            End If

            If Me.rDealer.Checked Then
                dealerCode = ALL
            Else
                If selectedDealerId.Equals(Guid.Empty) Then
                    ElitaPlusPage.SetLabelError(moDealerLabel)
                    Throw New GUIException(Message.MSG_INVALID_DEALER, Assurant.ElitaPlus.Common.ErrorCodes.GUI_DEALER_MUST_BE_SELECTED_ERR)
                End If
            End If

            If Me.rSvcCtr.Checked Then
                svcCtrCode = ALL
            Else
                If selectedSvcCtrId.Equals(Guid.Empty) Then
                    ElitaPlusPage.SetLabelError(SvcCtrLabel)
                    Throw New GUIException(Message.MSG_INVALID_SERVICE_CENTER, Assurant.ElitaPlus.Common.ErrorCodes.GUI_SERVICE_CENTER_MUST_BE_SELECTED_ERR)
                ElseIf cboCountry.Visible = True And selectedCountryId.Equals(Guid.Empty) Then
                    'ElitaPlusPage.SetLabelError(moCountryLabel)
                    Throw New GUIException(Message.MSG_INVALID_COUNTRY, Assurant.ElitaPlus.Common.ErrorCodes.GUI_COUNTRY_MUST_BE_SELECTED_ERR)
                End If
            End If

            If Me.rRiskType.Checked Then
                riskTypeDescription = ALL
            Else
                If selectedRiskTypeId.Equals(Guid.Empty) Then
                    ElitaPlusPage.SetLabelError(RiskTypeLabel)
                    Throw New GUIException(Message.MSG_INVALID_RISK_TYPE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_RISK_TYPE_MUST_BE_SELECTED_ERR)
                End If
            End If

            If Me.rMethodofRepair.Checked Then
                RepairTypeDesc = ALL
            Else
                If selectedRepairTypeId.Equals(Guid.Empty) Then
                    ElitaPlusPage.SetLabelError(lblMethodofRepair)
                    Throw New GUIException(Message.MSG_INVALID_CLAIM_TYPE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_METHOD_OF_REPAIR_MUST_BE_SELECTED_ERR)
                End If
            End If


            If Me.rCoverageType.Checked Then
                CoverageTypeDesc = ALL
            Else
                If selectedCoverageTypeId.Equals(Guid.Empty) Then
                    ElitaPlusPage.SetLabelError(lblCoverageType)
                    Throw New GUIException(Message.MSG_INVALID_CLAIM_TYPE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_COVERAGE_TYPE_MUST_BE_SELECTED_ERR)
                End If
            End If


            If Me.rReasonClosed.Checked Then
                ReasonClosedDesc = ALL
            Else
                If selectedReasonClosedId.Equals(Guid.Empty) Then
                    ElitaPlusPage.SetLabelError(lblReasonClosed)
                    Throw New GUIException(Message.MSG_INVALID_CLAIM_TYPE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_REASON_CLOSED_MUST_BE_SELECTED_ERR)
                End If
            End If
            ReportCeBase.EnableReportCe(Me, TheRptCeInputControl)

            Dim params As ReportCeBaseForm.Params = SetParameters(userId, countryCode, langCode, dealerCode, svcCtrCode, riskTypeDescription, RepairTypeDesc, CoverageTypeDesc, ReasonClosedDesc, beginDate, endDate, svcCtrName, "ALL", sortBy, AddColumns)
            Session(ReportCeBaseForm.SESSION_PARAMETERS_KEY) = params
        End Sub
#End Region

    End Class
End Namespace