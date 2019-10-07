Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Imports System.Threading
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms

Namespace Reports
    Public Class ActiveClaimsReInsuranceEnglishUSAReportForm
        Inherits ElitaPlusPage


#Region "Parameters"

        Public Structure ReportParams
            Public userId As String
            Public countryCode As String
            Public companyCode As String
            Public dealerCode As String
            Public svcCenterCode As String
            Public numberActiveDays As String
            Public includeAllClaims As String
            Public sortOrder As String
            Public langCultureValue As String
            Public langcode As String
            Public svcCenterName As String
            Public createdby As String
        End Structure

#End Region

#Region "Constants"

        Private Const RPT_FILENAME_WINDOW As String = "ACTIVE CLAIMS REINSURANCE"

        Private Const RPT_FILENAME As String = "ActiveClaimsReinsurance"
        Private Const RPT_FILENAME_EXPORT As String = "ActiveClaimsReinsurance-Exp"

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

        Public Const BY_NUMBER_ACTIVE_DAYS As String = "0"
        Public Const BY_CLAIM_NUMBER As String = "1"
        Public Const BY_SERVICE_CENTER_NAME As String = "2"
        Public Const BY_DEALER As String = "3"

        Public Const SORT_BY_NUMBER_ACTIVE_DAYS As String = "A"
        Public Const SORT_BY_CLAIM_NUMBER As String = "C"
        Public Const SORT_BY_SERVICE_CENTER_NAME As String = "S"
        Public Const SORT_BY_DEALER As String = "D"

        Public Const ALL As String = "*"
        Public Const NOTHING_SELECTED As Int16 = -1
        Public Const SP_DATE_FORMAT As String = "yyyyMMdd"
        Public Const YES As String = "Y"
        Public Const NO As String = "N"
        Public Const ReportName_LangSep As String = "_"
        Public Const numeric_cultureSep As String = "¦"

        Private Const TOTALPARAMS As Integer = 11  ' 23
        Private Const TOTALEXPPARAMS As Integer = 11  ' 7
        Private Const PARAMS_PER_REPORT As Integer = 11 '8

        Protected WithEvents isProgressVisible As System.Web.UI.HtmlControls.HtmlInputHidden
        Protected WithEvents isReportCeVisible As System.Web.UI.HtmlControls.HtmlInputHidden
        Protected WithEvents moReportCeStatus As System.Web.UI.HtmlControls.HtmlInputHidden
        Public Const DEFAULT_NUMBER_ACTIVE_DAYS As String = "3"
        Public Const None As Integer = 0

        Private Const LABEL_SELECT_DEALER As String = "OR A SINGLE DEALER"

#End Region

#Region "variables"
        Private moReportFormat As ReportCeBaseForm.RptFormat
        Dim selectedCountryId As Guid
        Dim rptLangSelected As String
        Dim reportName As String
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

#End Region

#Region "Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub
        Protected WithEvents RadiobuttonEXCEL As System.Web.UI.WebControls.RadioButton
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

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load, Me.Load
            'Put user code to initialize the page here
            Me.ErrorCtrl.Clear_Hide()
            Try
                If Not Me.IsPostBack Then
                    InitializeForm()
                    moReportCeInputControl.populateReportLanguages(RPT_FILENAME)
                    TheRptCeInputControl.SetExportOnly()
                End If
                '    Me.DisplayProgressBarOnClick(Me.btnGenRpt, "Loading_Claims")
                EnableOrDisableControls()
                Me.InstallProgressBar()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try
            Me.ShowMissingTranslations(Me.ErrorCtrl)
        End Sub

        Private Sub InitializeForm()
            PopulateCountryDropDown()
            PopulateDealerDropDown()
            PopulateServiceCenterDropDown()
            Me.rCountry.Checked = True
            Me.rsvccenter.Checked = True
            Me.rdealer.Checked = True
            Me.rAllUsers.Checked = True
            Me.RadiobuttonAllClaims.Checked = True
            Me.rdReportSortOrder.Items(0).Selected = True
            Me.PopulateControlFromBOProperty(Me.txtActiveDays, Me.DEFAULT_NUMBER_ACTIVE_DAYS)
            'Dim re As RadioButton = CType(Me.moReportCeInputControl.FindControl("RadiobuttonEXCEL"), RadioButton)
            'me.rbEXCEL.
        End Sub

#End Region

#Region "Populate"

        Sub PopulateCountryDropDown()
            'Me.BindListControlToDataView(cboCountry, LookupListNew.GetUserCountriesLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies))

            Dim CountryList As DataElements.ListItem() =
                            CommonConfigManager.Current.ListManager.GetList(listCode:=ListCodes.Country)

            Dim UserCountries As DataElements.ListItem() = (From Country In CountryList
                                                            Where ElitaPlusIdentity.Current.ActiveUser.Countries.Contains(Country.ListItemId)
                                                            Select Country).ToArray()

            Me.cboCountry.Populate(UserCountries.ToArray(),
                                    New PopulateOptions() With
                                    {
                                        .AddBlankItem = True
                                    })

            If cboCountry.Items.Count < 3 Then
                'HideHtmlElement("ddSeparator")
                HideHtmlElement("ddHideRow")
                ControlMgr.SetVisibleControl(Me, rCountry, False)
                ControlMgr.SetVisibleControl(Me, moCountryLabel, False)
                ' ControlMgr.SetVisibleControl(Me, moCountryColonLabel_NO_TRANSLATE, False)
                ControlMgr.SetVisibleControl(Me, cboCountry, False)
            End If
        End Sub

        Sub PopulateDealerDropDown()
            Dim dv As DataView = LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies)
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

        Sub PopulateServiceCenterDropDown()
            'Me.BindListControlToDataView(Me.cboSvcCenter, LookupListNew.GetServiceCenterLookupList(ElitaPlusIdentity.Current.ActiveUser.Countries), , , True)

            Dim ServiceCenterList As New Collections.Generic.List(Of DataElements.ListItem)

            For Each Country_id As Guid In ElitaPlusIdentity.Current.ActiveUser.Countries
                Dim ServiceCenters As DataElements.ListItem() =
                    CommonConfigManager.Current.ListManager.GetList(listCode:="ServiceCenterListByCountry",
                                                                    context:=New ListContext() With
                                                                    {
                                                                      .CountryId = Country_id
                                                                    })

                If ServiceCenters.Count > 0 Then
                    If Not ServiceCenterList Is Nothing Then
                        ServiceCenterList.AddRange(ServiceCenters)
                    Else
                        ServiceCenterList = ServiceCenters.Clone()
                    End If
                End If
            Next

            Me.cboSvcCenter.Populate(ServiceCenterList.ToArray(),
                                    New PopulateOptions() With
                                    {
                                        .AddBlankItem = True
                                    })
        End Sub

#End Region

#Region "Handlers-Buttons"

        Private Sub btnGenRpt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenRpt.Click
            Try
                Dim userId As Guid = ElitaPlusIdentity.Current.ActiveUser.Id
                Dim langId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
                Dim langCode As String = LookupListNew.GetCodeFromId(LookupListNew.LK_LANGUAGES, langId)
                Dim langCultureValue As String = LookupListNew.GetCodeFromId(LookupListNew.LK_LANGUAGE_CULTURES, langId)
                Dim selectedDealerId As Guid = DealerMultipleDrop.SelectedGuid
                Dim dealerDV As DataView = LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies, False, "CODE")
                Dim dealerCode As String = LookupListNew.GetCodeFromId(dealerDV, selectedDealerId)
                Dim selectedServiceCenterId As Guid = Me.GetSelectedItem(Me.cboSvcCenter)
                Dim svcCenterDV As DataView = LookupListNew.GetServiceCenterLookupList(ElitaPlusIdentity.Current.ActiveUser.Country(ElitaPlusIdentity.Current.ActiveUser.FirstCompanyID).Id)
                Dim svcCenterCode As String = LookupListNew.GetCodeFromId(svcCenterDV, selectedServiceCenterId)
                Dim svcCenterName As String = LookupListNew.GetDescriptionFromId(svcCenterDV, selectedServiceCenterId)
                Dim numberActiveDays As Integer
                selectedCountryId = Me.GetSelectedItem(Me.cboCountry)
                Dim dvCountry As DataView = LookupListNew.GetUserCountriesLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies)
                Dim countryCode As String = LookupListNew.GetDescriptionFromId(dvCountry, selectedCountryId)
                Dim sortOrder As String
                Dim includeAllClaims As String = NO
                Dim params As ReportCeBaseForm.Params
                Dim createdby As String

                If rCountry.Visible = True Then
                    If Me.rCountry.Checked Then
                        countryCode = ALL
                    Else
                        If selectedCountryId.Equals(Guid.Empty) Then
                            'ElitaPlusPage.SetLabelError(moCountryLabel)
                            Throw New GUIException(Message.MSG_INVALID_COUNTRY, Assurant.ElitaPlus.Common.ErrorCodes.GUI_COUNTRY_MUST_BE_SELECTED_ERR)
                        End If
                    End If
                Else
                    countryCode = ALL
                End If

                If Me.rdealer.Checked Then
                    dealerCode = ALL
                Else
                    If selectedDealerId.Equals(Guid.Empty) Then
                        Throw New GUIException(Message.MSG_INVALID_DEALER, Assurant.ElitaPlus.Common.ErrorCodes.GUI_DEALER_MUST_BE_SELECTED_ERR)
                    End If
                End If

                If Me.rsvccenter.Checked Then
                    svcCenterCode = ALL
                Else
                    If selectedServiceCenterId.Equals(Guid.Empty) Then
                        Throw New GUIException(Message.MSG_INVALID_SERVICE_CENTER, Assurant.ElitaPlus.Common.ErrorCodes.GUI_SERVICE_CENTER_MUST_BE_SELECTED_ERR)
                    ElseIf cboCountry.Visible = True And selectedCountryId.Equals(Guid.Empty) Then
                        'ElitaPlusPage.SetLabelError(moCountryLabel)
                        Throw New GUIException(Message.MSG_INVALID_COUNTRY, Assurant.ElitaPlus.Common.ErrorCodes.GUI_COUNTRY_MUST_BE_SELECTED_ERR)
                    End If
                End If

                If txtActiveDays.Text.Trim.ToString = String.Empty Then
                    Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_INVALID_NUMBER_OF_ACTIVE_DAYS_ERR)
                Else
                    numberActiveDays = CType(Me.txtActiveDays.Text, Integer)
                    If ((numberActiveDays < 0) OrElse (numberActiveDays > 999)) Then
                        Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_INVALID_NUMBER_OF_ACTIVE_DAYS_ERR)
                    End If
                End If

                If Me.RadiobuttonExcludeRepairedClaims.Checked Then
                    includeAllClaims = YES
                End If


                If Me.rAllUsers.Checked Then
                    createdby = ALL
                Else
                    createdby = txtUserId.Text.Trim.ToString()
                    If createdby = String.Empty Then
                        'ElitaPlusPage.SetLabelError(lblUserId)
                        Throw New GUIException(Message.MSG_INVALID_USER_ID, Assurant.ElitaPlus.Common.ErrorCodes.GUI_USER_MUST_BE_ENTERED_ERR)
                    End If
                End If

                ReportCeBase.EnableReportCe(Me, TheRptCeInputControl)

                Select Case Me.rdReportSortOrder.SelectedValue()
                    Case BY_NUMBER_ACTIVE_DAYS
                        sortOrder = SORT_BY_NUMBER_ACTIVE_DAYS
                    Case BY_CLAIM_NUMBER
                        sortOrder = SORT_BY_CLAIM_NUMBER
                    Case BY_SERVICE_CENTER_NAME
                        sortOrder = SORT_BY_SERVICE_CENTER_NAME
                    Case BY_DEALER
                        sortOrder = SORT_BY_DEALER
                End Select
                moReportFormat = ReportCeBase.GetReportFormat(Me)
                If (moReportFormat = ReportCeBase.RptFormat.TEXT_TAB) OrElse (moReportFormat = ReportCeBase.RptFormat.TEXT_CSV) Then
                    'Export Report                    
                    langCultureValue = moReportCeInputControl.getCultureValue(True)
                    params = SetExpParameters(GuidControl.GuidToHexString(userId), countryCode, dealerCode, svcCenterCode, svcCenterName,
                                 numberActiveDays, includeAllClaims, sortOrder, langCode, langCultureValue, createdby)
                Else
                    'View Report
                    langCode = moReportCeInputControl.LanguageCodeSelected
                    langCultureValue = moReportCeInputControl.getCultureValue(False)
                    params = SetParameters(GuidControl.GuidToHexString(userId), countryCode, dealerCode, svcCenterCode, svcCenterName,
                                  numberActiveDays, includeAllClaims, sortOrder, langCode, langCultureValue, createdby)
                End If
                Session(ReportCeBaseForm.SESSION_PARAMETERS_KEY) = params
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try
        End Sub

#End Region

#Region "Crystal Enterprise"

        Sub SetReportParams(ByVal oReportParams As ReportParams, ByVal repParams() As ReportCeBaseForm.RptParam,
                            ByVal reportName As String, ByVal startIndex As Integer)
            With oReportParams
                repParams(startIndex) = New ReportCeBaseForm.RptParam("V_USER_ID", .userId, reportName)
                repParams(startIndex + 1) = New ReportCeBaseForm.RptParam("V_DEALER_CODE", .dealerCode, reportName)
                repParams(startIndex + 2) = New ReportCeBaseForm.RptParam("V_SVCCENTER_CODE", .svcCenterCode, reportName)
                repParams(startIndex + 3) = New ReportCeBaseForm.RptParam("V_NUMBER_ACTIVE_DAYS", .numberActiveDays, reportName)
                repParams(startIndex + 4) = New ReportCeBaseForm.RptParam("V_REPAIR_CLAIM_FLAG", .includeAllClaims, reportName)
                repParams(startIndex + 5) = New ReportCeBaseForm.RptParam("V_FREE_ZONE_FLAG", "ALL", reportName)
                repParams(startIndex + 6) = New ReportCeBaseForm.RptParam("V_SORT_ORDER", .sortOrder, reportName)
                repParams(startIndex + 7) = New ReportCeBaseForm.RptParam("V_LANGUAGE_CODE", .langcode, reportName)
                repParams(startIndex + 8) = New ReportCeBaseForm.RptParam("P_SERVICE_CENTER_NAME", .svcCenterName, reportName)
                repParams(startIndex + 9) = New ReportCeBaseForm.RptParam("V_COUNTRY_CODE", .countryCode, reportName)
                repParams(startIndex + 10) = New ReportCeBaseForm.RptParam("V_CREATED_BY", .createdby, reportName)
                repParams(startIndex + 11) = New ReportCeBaseForm.RptParam("V_LANG_CULTURE_CODE", .langCultureValue, reportName)
            End With
        End Sub

        Function SetParameters(ByVal userId As String, ByVal countryCode As String, ByVal dealerCode As String, ByVal svcCenterCode As String,
                                 ByVal svcCenterName As String,
                                 ByVal numberActiveDays As Integer, ByVal includeAllClaims As String, ByVal sortOrder As String,
                                 ByVal langCode As String, ByVal langCultureValue As String, ByVal createdby As String) As ReportCeBaseForm.Params

            Dim reportFormat As ReportCeBaseForm.RptFormat
            reportName = moReportCeInputControl.getReportName(RPT_FILENAME, False)
            Dim params As New ReportCeBaseForm.Params
            Dim repParams(TOTALPARAMS) As ReportCeBaseForm.RptParam
            Dim oReportParams As ReportParams
            Me.rptWindowTitle.InnerText = TheRptCeInputControl.getReportWindowTitle(TranslationBase.TranslateLabelOrMessage(RPT_FILENAME_WINDOW))


            With oReportParams
                .userId = userId
                .countryCode = countryCode
                .dealerCode = dealerCode
                .svcCenterCode = svcCenterCode
                .numberActiveDays = numberActiveDays.ToString
                .includeAllClaims = includeAllClaims
                .sortOrder = sortOrder
                .langcode = langCode
                .svcCenterName = svcCenterName
                .createdby = createdby
                .langCultureValue = langCultureValue
            End With
            SetReportParams(oReportParams, repParams, String.Empty, PARAMS_PER_REPORT * 0)     ' Main Report
            'SetReportParams(oReportParams, repParams, "Days", PARAMS_PER_REPORT * 1)           ' Days SubReport
            'SetReportParams(oReportParams, repParams, "Days - 01", PARAMS_PER_REPORT * 2)      ' Days - 01 SubReport

            With params
                .msRptName = reportName
                .msRptWindowName = TranslationBase.TranslateLabelOrMessage(RPT_FILENAME_WINDOW)
                .moRptFormat = moReportFormat
                .moAction = ReportCeBaseForm.RptAction.SCHEDULE_VIEW
                .moRptParams = repParams
            End With


            Return params
        End Function

        Function SetExpParameters(ByVal userId As String, ByVal countryCode As String, ByVal dealerCode As String, ByVal svcCenterCode As String,
                                 ByVal svcCenterName As String,
                                 ByVal numberActiveDays As Integer, ByVal includeAllClaims As String, ByVal sortOrder As String,
                                 ByVal langCode As String, ByVal langCultureValue As String, ByVal createdby As String) As ReportCeBaseForm.Params

            Dim reportFormat As ReportCeBaseForm.RptFormat
            ' rptLangSelected = moReportCeInputControl.LanguageCodeSelected
            reportName = moReportCeInputControl.getReportName(RPT_FILENAME_EXPORT, True)
            Dim params As New ReportCeBaseForm.Params
            Dim repParams(TOTALEXPPARAMS) As ReportCeBaseForm.RptParam
            Me.rptWindowTitle.InnerText = TheRptCeInputControl.getReportWindowTitle(TranslationBase.TranslateLabelOrMessage(RPT_FILENAME_WINDOW))
            Dim oReportParams As ReportParams

            With oReportParams
                .userId = userId
                .countryCode = countryCode
                .dealerCode = dealerCode
                .svcCenterCode = svcCenterCode
                .numberActiveDays = numberActiveDays.ToString
                .includeAllClaims = includeAllClaims
                .sortOrder = sortOrder
                .langcode = langCode
                .svcCenterName = svcCenterName
                .createdby = createdby
                .langCultureValue = langCultureValue
            End With
            SetReportParams(oReportParams, repParams, String.Empty, PARAMS_PER_REPORT * 0)     ' Main Report

            With params
                .msRptName = reportName
                .msRptWindowName = TranslationBase.TranslateLabelOrMessage(RPT_FILENAME_WINDOW)
                .moRptFormat = moReportFormat
                .moAction = ReportCeBaseForm.RptAction.SCHEDULE_VIEW
                .moRptParams = repParams
            End With


            Return params
        End Function

#End Region

#Region "Handlers-Dropdowns"

        Private Sub cboCountry_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCountry.SelectedIndexChanged

            Try
                selectedCountryId = Me.GetSelectedItem(Me.cboCountry)
                If selectedCountryId.Equals(Guid.Empty) Then
                    'ElitaPlusPage.SetLabelError(moCountryLabel)
                    Throw New GUIException(Message.MSG_INVALID_COUNTRY, Assurant.ElitaPlus.Common.ErrorCodes.GUI_COUNTRY_MUST_BE_SELECTED_ERR)
                Else
                    Me.BindListControlToDataView(Me.cboSvcCenter, LookupListNew.GetServiceCenterLookupList(selectedCountryId), , , True)
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try

        End Sub

        Private Sub rCountry_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rCountry.CheckedChanged
            If rCountry.Checked = True Then
                cboCountry.SelectedIndex = None
                PopulateServiceCenterDropDown()
            End If
        End Sub

        Private Sub EnableOrDisableControls()

            If Me.rdealer.Checked = True Then
                DealerMultipleDrop.SelectedIndex = -1
            End If

        End Sub

#End Region

    End Class
End Namespace