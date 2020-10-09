Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports Assurant.Elita.Web.Forms
Imports System.Threading

Namespace Reports
    Partial Class ClaimDetailByDateClosedWEPPReportForm
        Inherits ElitaPlusPage

#Region "Constants"

        Private Const RPT_FILENAME_WINDOW As String = "CLAIM DETAIL BY DATE CLOSED"
        Private Const RPT_FILENAME As String = "ClaimDetailByDateClosedWEPP"
        Private Const RPT_FILENAME_EXPORT As String = "ClaimDetailByDateClosedWEPP-Exp"

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

        Private Sub Page_Init(sender As System.Object, e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

#Region "Handlers-Init"

        Private Sub InitializeForm()
            PopulateDropDowns()
            Dim t As Date = Date.Now.AddDays(-1)
            moBeginDateText.Text = GetDateFormattedString(t)
            moEndDateText.Text = GetDateFormattedString(Date.Now)
            rCountry.Checked = True
            rDealer.Checked = True
            rSvcCtr.Checked = True
            rRiskType.Checked = True
            rMethodofRepair.Checked = True
            rCoverageType.Checked = True
            rReasonClosed.Checked = True
        End Sub


        Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            ErrorCtrl.Clear_Hide()
            Try
                If Not IsPostBack Then
                    InitializeForm()
                    TheRptCeInputControl.populateReportLanguages(RPT_FILENAME)
                    'Date Calendars
                    AddCalendar(BtnBeginDate, moBeginDateText)
                    AddCalendar(BtnEndDate, moEndDateText)
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

#Region "Handlers-DropDowns"

        Private Sub cboCountry_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cboCountry.SelectedIndexChanged
            Try
                selectedCountryId = GetSelectedItem(cboCountry)
                If selectedCountryId.Equals(Guid.Empty) Then
                    Throw New GUIException(Message.MSG_INVALID_COUNTRY, Assurant.ElitaPlus.Common.ErrorCodes.GUI_COUNTRY_MUST_BE_SELECTED_ERR)
                Else
                    'Me.BindListControlToDataView(Me.cboSvcCtr, LookupListNew.GetServiceCenterLookupList(selectedCountryId), , , True)
                    Dim listcontext As ListContext = New ListContext()
                    listcontext.CountryId = selectedCountryId
                    cboSvcCtr.Populate(CommonConfigManager.Current.ListManager.GetList(listCode:="ServiceCenterListByCountry", languageCode:=Thread.CurrentPrincipal.GetLanguageCode(), context:=listcontext), New PopulateOptions() With
                        {
                            .AddBlankItem = True
                        })
                End If
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try
        End Sub

        Private Sub rCountry_CheckedChanged(sender As Object, e As System.EventArgs) Handles rCountry.CheckedChanged
            If rCountry.Checked = True Then
                cboCountry.SelectedIndex = None
                PopulateSvcCtrDropDown()
            End If
        End Sub
#End Region


#End Region

#Region "Clear"

        Private Sub ClearErrLabels()
            ClearLabelErrSign(moBeginDateLabel)
            ClearLabelErrSign(moEndDateLabel)
            ClearLabelErrSign(moDealerLabel)
            ClearLabelErrSign(SvcCtrLabel)
            ClearLabelErrSign(RiskTypeLabel)
            ClearLabelErrSign(lblMethodofRepair)
            ClearLabelErrSign(lblCoverageType)
            ClearLabelErrSign(lblReasonClosed)
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
            'Me.BindListControlToDataView(Me.cboCountry, LookupListNew.GetUserCountriesLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies), , , True)
            Dim countryList As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="Country")
            Dim filteredCountryList As DataElements.ListItem() = (From x In countryList
                                                                  Where ElitaPlusIdentity.Current.ActiveUser.Countries.Contains(x.ListItemId)
                                                                  Select x).ToArray()
            cboCountry.Populate(filteredCountryList.ToArray(), New PopulateOptions() With
                                                   {
                                                    .AddBlankItem = True
                                                   })
            If cboCountry.Items.Count < 3 Then
                HideHtmlElement("ddHideRow")
                ControlMgr.SetVisibleControl(Me, rCountry, False)
                ControlMgr.SetVisibleControl(Me, moCountryLabel, False)
                'ControlMgr.SetVisibleControl(Me, moCountryColonLabel_NO_TRANSLATE, False)
                ControlMgr.SetVisibleControl(Me, cboCountry, False)
            End If
        End Sub
        Private Sub PopulateDealerDropDown()
            'Me.BindListControlToDataView(Me.cboDealer, LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies, False, "CODE"), , , True)
            Dim oListContext As New Assurant.Elita.CommonConfiguration.ListContext
            Dim oDealerList As New Collections.Generic.List(Of Assurant.Elita.CommonConfiguration.DataElements.ListItem)
            For Each _company As Guid In ElitaPlusIdentity.Current.ActiveUser.Companies
                oListContext.CompanyId = _company
                Dim oDealerListForCompany As Assurant.Elita.CommonConfiguration.DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="DealerListByCompany", context:=oListContext)
                If oDealerListForCompany.Count > 0 Then
                    If oDealerList IsNot Nothing Then
                        oDealerList.AddRange(oDealerListForCompany)
                    Else
                        oDealerList = oDealerListForCompany.Clone()
                    End If
                End If
            Next
            cboDealer.Populate(oDealerList.ToArray(), New PopulateOptions() With
                {
                    .AddBlankItem = True
                })
        End Sub

        Private Sub PopulateSvcCtrDropDown()
            'Me.BindListControlToDataView(Me.cboSvcCtr, LookupListNew.GetServiceCenterLookupList(ElitaPlusIdentity.Current.ActiveUser.Countries), , , True)
            Dim oListContext As New Assurant.Elita.CommonConfiguration.ListContext
            Dim oServiceCenterList As New Collections.Generic.List(Of Assurant.Elita.CommonConfiguration.DataElements.ListItem)
            For Each _country As Guid In ElitaPlusIdentity.Current.ActiveUser.Countries
                oListContext.CountryId = _country
                Dim oServiceCenterListForCountry As Assurant.Elita.CommonConfiguration.DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="ServiceCenterListByCountry", context:=oListContext)
                If oServiceCenterListForCountry.Count > 0 Then
                    If oServiceCenterList IsNot Nothing Then
                        oServiceCenterList.AddRange(oServiceCenterListForCountry)
                    Else
                        oServiceCenterList = oServiceCenterListForCountry.Clone()
                    End If
                End If
            Next

            cboSvcCtr.Populate(oServiceCenterList.ToArray(), New PopulateOptions() With
                {
                    .AddBlankItem = True
                })
        End Sub

        Private Sub PopulateRiskTypeDropDown()
            'Me.BindListControlToDataView(Me.cboRiskType, LookupListNew.GetRiskTypeLookupList(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id), , , True)
            Dim oListContext As New Assurant.Elita.CommonConfiguration.ListContext
            oListContext.CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
            Dim oRiskTypeListForCountry As Assurant.Elita.CommonConfiguration.DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="RiskTypeByCompanyGroup", context:=oListContext)
            cboRiskType.Populate(oRiskTypeListForCountry.ToArray(), New PopulateOptions() With
                {
                    .AddBlankItem = True
                })
        End Sub

        Private Sub PopulateMethodOfRepairDropDown()
            'Me.BindListControlToDataView(Me.cboMethodofRepair, LookupListNew.GetMethodOfRepairLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), , , True)
            Dim oMethodOfRepair As Assurant.Elita.CommonConfiguration.DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="METHR", languageCode:=Thread.CurrentPrincipal.GetLanguageCode())
            cboMethodofRepair.Populate(oMethodOfRepair.ToArray(), New PopulateOptions() With
                {
                    .AddBlankItem = True
                })
        End Sub

        Private Sub PopulateCoverageTypeDropDown()
            'Me.BindListControlToDataView(Me.cboCoverageType, LookupListNew.GetCoverageTypeByCompanyGroupLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId, ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id), , , True)
            Dim oListContext As New Assurant.Elita.CommonConfiguration.ListContext
            oListContext.CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
            Dim oCoverageTypeList As Assurant.Elita.CommonConfiguration.DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="CoverageTypeByCompanyGroup", context:=oListContext)
            cboCoverageType.Populate(oCoverageTypeList.ToArray(), New PopulateOptions() With
                {
                    .AddBlankItem = True
                })
        End Sub
        Private Sub PopulateReasonsClosedDropDown()
            'Me.BindListControlToDataView(Me.cboReasonClosed, LookupListNew.GetReasonClosedLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), , , True)
            Dim oReasonClosed As Assurant.Elita.CommonConfiguration.DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="RESCL", languageCode:=Thread.CurrentPrincipal.GetLanguageCode())
            cboReasonClosed.Populate(oReasonClosed.ToArray(), New PopulateOptions() With
                {
                    .AddBlankItem = True
                })
        End Sub
#End Region

#Region "Crystal Enterprise"

        Function SetParameters(userId As String, countryCode As String, langCode As String, dealerCode As String, svcCtrCode As String, riskTypeDescription As String, RepairTypeDesc As String, CoverageTypeDesc As String,
                                 ReasonClosedDesc As String, beginDate As String, endDate As String, svcCtrName As String, sortBy As String) As ReportCeBaseForm.Params

            Dim reportFormat As ReportCeBaseForm.RptFormat
            Dim culturecode As String = TheRptCeInputControl.getCultureValue(False)
            Dim reportName As String = TheRptCeInputControl.getReportName(RPT_FILENAME, False)
            Dim params As New ReportCeBaseForm.Params

            reportFormat = ReportCeBase.GetReportFormat(Me)
            If (reportFormat = ReportCeBase.RptFormat.TEXT_TAB OrElse
                reportFormat = ReportCeBase.RptFormat.TEXT_CSV) Then
                reportName = TheRptCeInputControl.getReportName(RPT_FILENAME_EXPORT, True)
                culturecode = TheRptCeInputControl.getCultureValue(True)
            End If

            rptWindowTitle.InnerText = TheRptCeInputControl.getReportWindowTitle(TranslationBase.TranslateLabelOrMessage(RPT_FILENAME_WINDOW))



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
                     New ReportCeBaseForm.RptParam("V_SORT_BY", sortBy),
                     New ReportCeBaseForm.RptParam("SERVICE_CENTER_NAME", svcCtrName),
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
            Dim selectedDealerId As Guid = GetSelectedItem(cboDealer)
            Dim dvDealer As DataView = LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies, False, "CODE")
            Dim dealerCode As String = LookupListNew.GetCodeFromId(dvDealer, selectedDealerId)
            Dim selectedSvcCtrId As Guid = GetSelectedItem(cboSvcCtr)
            Dim dvSvcCtr As DataView = LookupListNew.GetServiceCenterLookupList(ElitaPlusIdentity.Current.ActiveUser.Country(ElitaPlusIdentity.Current.ActiveUser.FirstCompanyID).Id)
            Dim svcCtrCode As String = LookupListNew.GetCodeFromId(dvSvcCtr, selectedSvcCtrId)
            Dim svcCtrName As String = LookupListNew.GetDescriptionFromId(dvSvcCtr, selectedSvcCtrId)
            Dim selectedRiskTypeId As Guid = GetSelectedItem(cboRiskType)
            Dim dvRiskType As DataView = LookupListNew.GetRiskTypeLookupList(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id)
            Dim riskTypeDescription As String = LookupListNew.GetDescriptionFromId(dvRiskType, selectedRiskTypeId)
            'Dim selectedClaimTypeId As Guid = Me.GetSelectedItem(Me.cboClaimType)
            'Dim dvClaimType As DataView = LookupListNew.GetClaimTypeLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId)
            'Dim claimTypeCode As String = LookupListNew.GetCodeFromId(dvClaimType, selectedClaimTypeId)
            Dim selectedRepairTypeId As Guid = GetSelectedItem(cboMethodofRepair)
            Dim dvRepairType As DataView = LookupListNew.GetMethodOfRepairLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId)
            Dim RepairTypeDesc As String = LookupListNew.GetDescriptionFromId(dvRepairType, selectedRepairTypeId)
            Dim sortBy As String = rdReportSortOrder.SelectedValue
            selectedCountryId = GetSelectedItem(cboCountry)
            Dim dvCountry As DataView = LookupListNew.GetUserCountriesLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies)
            Dim countryCode As String = LookupListNew.GetDescriptionFromId(dvCountry, selectedCountryId)
            Dim selectionType As Integer
            Dim endDate As String
            Dim beginDate As String
            Dim selectedCoverageTypeId As Guid = GetSelectedItem(cboCoverageType)
            Dim dvCoverageType As DataView = LookupListNew.GetCoverageTypeByCompanyGroupLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId, ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id)
            Dim CoverageTypeDesc As String = LookupListNew.GetDescriptionFromId(dvCoverageType, selectedCoverageTypeId)

            Dim selectedReasonClosedId As Guid = GetSelectedItem(cboReasonClosed)
            Dim dvReasonClosed As DataView = LookupListNew.GetReasonClosedLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId)
            Dim ReasonClosedDesc As String = LookupListNew.GetDescriptionFromId(dvReasonClosed, selectedReasonClosedId)

            'Dates
            ReportCeBase.ValidateBeginEndDate(moBeginDateLabel, moBeginDateText.Text, moEndDateLabel, moEndDateText.Text)
            endDate = ReportCeBase.FormatDate(moEndDateLabel, moEndDateText.Text)
            beginDate = ReportCeBase.FormatDate(moBeginDateLabel, moBeginDateText.Text)


            If rCountry.Checked Then
                countryCode = ALL
            Else
                If selectedCountryId.Equals(Guid.Empty) Then
                    ElitaPlusPage.SetLabelError(moCountryLabel)
                    Throw New GUIException(Message.MSG_INVALID_COUNTRY, Assurant.ElitaPlus.Common.ErrorCodes.GUI_COUNTRY_MUST_BE_SELECTED_ERR)
                End If
            End If

            If rDealer.Checked Then
                dealerCode = ALL
            Else
                If selectedDealerId.Equals(Guid.Empty) Then
                    ElitaPlusPage.SetLabelError(moDealerLabel)
                    Throw New GUIException(Message.MSG_INVALID_DEALER, Assurant.ElitaPlus.Common.ErrorCodes.GUI_DEALER_MUST_BE_SELECTED_ERR)
                End If
            End If

            If rSvcCtr.Checked Then
                svcCtrCode = ALL
            Else
                If selectedSvcCtrId.Equals(Guid.Empty) Then
                    ElitaPlusPage.SetLabelError(SvcCtrLabel)
                    Throw New GUIException(Message.MSG_INVALID_SERVICE_CENTER, Assurant.ElitaPlus.Common.ErrorCodes.GUI_SERVICE_CENTER_MUST_BE_SELECTED_ERR)
                ElseIf cboCountry.Visible = True AndAlso selectedCountryId.Equals(Guid.Empty) Then
                    'ElitaPlusPage.SetLabelError(moCountryLabel)
                    Throw New GUIException(Message.MSG_INVALID_COUNTRY, Assurant.ElitaPlus.Common.ErrorCodes.GUI_COUNTRY_MUST_BE_SELECTED_ERR)
                End If
            End If

            If rRiskType.Checked Then
                riskTypeDescription = ALL
            Else
                If selectedRiskTypeId.Equals(Guid.Empty) Then
                    ElitaPlusPage.SetLabelError(RiskTypeLabel)
                    Throw New GUIException(Message.MSG_INVALID_RISK_TYPE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_RISK_TYPE_MUST_BE_SELECTED_ERR)
                End If
            End If

            If rMethodofRepair.Checked Then
                RepairTypeDesc = ALL
            Else
                If selectedRepairTypeId.Equals(Guid.Empty) Then
                    ElitaPlusPage.SetLabelError(lblMethodofRepair)
                    Throw New GUIException(Message.MSG_INVALID_CLAIM_TYPE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_METHOD_OF_REPAIR_MUST_BE_SELECTED_ERR)
                End If
            End If


            If rCoverageType.Checked Then
                CoverageTypeDesc = ALL
            Else
                If selectedCoverageTypeId.Equals(Guid.Empty) Then
                    ElitaPlusPage.SetLabelError(lblCoverageType)
                    Throw New GUIException(Message.MSG_INVALID_CLAIM_TYPE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_COVERAGE_TYPE_MUST_BE_SELECTED_ERR)
                End If
            End If


            If rReasonClosed.Checked Then
                ReasonClosedDesc = ALL
            Else
                If selectedReasonClosedId.Equals(Guid.Empty) Then
                    ElitaPlusPage.SetLabelError(lblReasonClosed)
                    Throw New GUIException(Message.MSG_INVALID_CLAIM_TYPE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_REASON_CLOSED_MUST_BE_SELECTED_ERR)
                End If
            End If
            ReportCeBase.EnableReportCe(Me, TheRptCeInputControl)

            Dim params As ReportCeBaseForm.Params = SetParameters(userId, countryCode, langCode, dealerCode, svcCtrCode, riskTypeDescription, RepairTypeDesc, CoverageTypeDesc, ReasonClosedDesc, beginDate, endDate, svcCtrName, sortBy)
            Session(ReportCeBaseForm.SESSION_PARAMETERS_KEY) = params
        End Sub
#End Region

    End Class

End Namespace