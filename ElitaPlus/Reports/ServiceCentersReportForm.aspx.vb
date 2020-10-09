Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Imports System.Globalization
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports System.Threading
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms

Namespace Reports

    Partial Class ServiceCentersReportForm
        Inherits ElitaPlusPage

#Region "Constants"

        Private Const RPT_FILENAME_WINDOW As String = "Service Centers Report"
        Private Const RPT_FILENAME As String = "ServiceCentersReport"
        Private Const RPT_FILENAME_EXPORT As String = "ServiceCentersReport_Exp"

        Private Const TOTAL_PARAMS As Integer = 7 ' 8 Elements
        Private Const TOTAL_EXP_PARAMS As Integer = 3 ' 4 Elements
        Private Const PARAMS_PER_REPORT As Integer = 4 ' 4 Elements
        Private Const ALL As String = "*"
        Private Const SP_DATE_FORMAT As String = "yyyyMMdd"
        Private Const YES As String = "Y"
        Private Const NO As String = "N"
        Private Const None As Integer = 0
        Private Const DEFAULTSELECT As Integer = 1
        Private Const LABEL_SELECT_SERVICE_CENTER As String = "OR_ONLY_SERVICE_CENTER"
#End Region

#Region "Parameters"

        Public Structure ReportParams
            Public companyCode As String
            Public countryCode As String
            Public serviceCenter As String
        End Structure

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
        Public ReadOnly Property ServiceCenterMultipleDrop() As MultipleColumnDDLabelControl
            Get
                If moServiceCenterMultipleDrop Is Nothing Then
                    moServiceCenterMultipleDrop = CType(FindControl("multipleDropControl"), MultipleColumnDDLabelControl)
                End If
                Return moServiceCenterMultipleDrop
            End Get
        End Property
#End Region

#Region "variables"
        Private reportFormat As ReportCeBaseForm.RptFormat
        Protected WithEvents ServiceCenterLabel As System.Web.UI.WebControls.Label
        Private reportName As String = RPT_FILENAME
        Private selectedCountryId As Guid

#End Region

#Region "Handlers"


#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub
        Protected WithEvents ErrorCtrl As ErrorController
        Protected WithEvents moReportCeInputControl As ReportCeInputControl
        Protected WithEvents moServiceCenterMultipleDrop As MultipleColumnDDLabelControl
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
            Try
                If Not IsPostBack Then
                    InitializeForm()
                Else
                    ClearErrLabels()
                End If
                InstallProgressBar()
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try
            ShowMissingTranslations(ErrorCtrl)
        End Sub
        Private Sub ClearErrLabels()
            ClearLabelErrSign(ServiceCenterMultipleDrop.CaptionLabel)
            ClearLabelErrSign(moCountryLabel)
            If moCountryLabel.Visible = False Then
                ddRowHide.Visible = False
            End If

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
                    'ElitaPlusPage.SetLabelError(moCountryLabel)
                    Throw New GUIException(Message.MSG_INVALID_COUNTRY, Assurant.ElitaPlus.Common.ErrorCodes.GUI_COUNTRY_MUST_BE_SELECTED_ERR)
                Else
                    PopulateServiceCenterDropDown()
                End If
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try
        End Sub

#End Region

#End Region

#Region "Populate"

        Private Sub PopulateCountryDropDown()
            ' Me.BindListControlToDataView(Me.cboCountry, LookupListNew.GetUserCountriesLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies), , , True)
            Dim listcontext As ListContext = New ListContext()
            Dim countryLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList(ListCodes.Country, Thread.CurrentPrincipal.GetLanguageCode(), listcontext)
            Dim list As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Countries

            Dim filteredList As ListItem() = (From x In countryLkl
                                              Where list.Contains(x.ListItemId)
                                              Select x).ToArray()
            cboCountry.Populate(filteredList, New PopulateOptions() With
             {
               .AddBlankItem = True
             })
            If cboCountry.Items.Count < 3 Then
                HideHtmlElement("ddRowHide")
                ControlMgr.SetVisibleControl(Me, moCountryLabel, False)
                'ControlMgr.SetVisibleControl(Me, moCountryColonLabel_NO_TRANSLATE, False)
                ControlMgr.SetVisibleControl(Me, cboCountry, False)
                cboCountry.Items(DEFAULTSELECT).Selected = True
                ' selectedCountryId = Me.GetSelectedItem(Me.cboCountry)
            End If
        End Sub


        Private Sub PopulateServiceCenterDropDown()
            '''Dim ServiceCentersLookupListSortedByCode As DataView
            '''If cboCountry.Visible = True And cboCountry.SelectedIndex > Me.BLANK_ITEM_SELECTED Then
            '''    Me.BindListControlToDataView(Me.cboServiceCentersDec, LookupListNew.GetServiceCenterLookupList(selectedCountryId), , , True)
            '''    ServiceCentersLookupListSortedByCode = LookupListNew.GetServiceCenterLookupList(selectedCountryId)
            '''    ServiceCentersLookupListSortedByCode.Sort = "CODE"
            '''    Me.BindListControlToDataView(Me.cboServiceCentersCode, ServiceCentersLookupListSortedByCode, "CODE", , True)
            '''Else
            '''    Me.BindListControlToDataView(Me.cboServiceCentersDec, LookupListNew.GetServiceCenterLookupList(ElitaPlusIdentity.Current.ActiveUser.Country(ElitaPlusIdentity.Current.ActiveUser.FirstCompanyID).Id), , , True)
            '''    ServiceCentersLookupListSortedByCode = LookupListNew.GetServiceCenterLookupList(ElitaPlusIdentity.Current.ActiveUser.Country(ElitaPlusIdentity.Current.ActiveUser.FirstCompanyID).Id)
            '''    ServiceCentersLookupListSortedByCode.Sort = "CODE"
            '''    Me.BindListControlToDataView(Me.cboServiceCentersCode, ServiceCentersLookupListSortedByCode, "CODE", , True)
            '''End If

            Dim dv As DataView
            If cboCountry.Visible = True AndAlso cboCountry.SelectedIndex > BLANK_ITEM_SELECTED Then
                dv = LookupListNew.GetServiceCenterLookupList(selectedCountryId)
            Else
                dv = LookupListNew.GetServiceCenterLookupList(ElitaPlusIdentity.Current.ActiveUser.Country(ElitaPlusIdentity.Current.ActiveUser.FirstCompanyID).Id)
            End If

            ServiceCenterMultipleDrop.NothingSelected = True
            ServiceCenterMultipleDrop.SetControl(False, ServiceCenterMultipleDrop.MODES.NEW_MODE, True, dv, TranslationBase.TranslateLabelOrMessage(LABEL_SELECT_SERVICE_CENTER), True, False, " document.forms[0].rServiceCenters.checked = false;")

        End Sub

        Private Sub InitializeForm()
            PopulateCountryDropDown()
            PopulateServiceCenterDropDown()
            'Me.rCountry.Checked = True
            rServiceCenters.Checked = True
        End Sub

#End Region

#Region "Crystal Enterprise"

        Function SetParameters(companyCode As String, countryCode As String, serviceCenter As String, languageCode As String) As ReportCeBaseForm.Params

            Dim reportFormat As ReportCeBaseForm.RptFormat
            Dim params As New ReportCeBaseForm.Params
            '''Dim repParams(TOTAL_PARAMS) As ReportCeBaseForm.RptParam
            Dim rptParams As ReportParams

            With rptParams
                .companyCode = companyCode
                .countryCode = countryCode
                .serviceCenter = serviceCenter
            End With

            reportFormat = ReportCeBase.GetReportFormat(Me)

            Dim exportData As String = NO

            reportName = RPT_FILENAME
            If (reportFormat = ReportCeBase.RptFormat.TEXT_TAB) OrElse (reportFormat = ReportCeBase.RptFormat.TEXT_CSV) Then
                exportData = YES
                reportName = RPT_FILENAME_EXPORT
            End If

            Dim repParams() As ReportCeBaseForm.RptParam = New ReportCeBaseForm.RptParam() _
                                                {
                                                 New ReportCeBaseForm.RptParam("V_COMPANY", companyCode),
                                                 New ReportCeBaseForm.RptParam("V_COUNTRY", countryCode),
                                                 New ReportCeBaseForm.RptParam("V_SERVICE_CENTER_CODE", serviceCenter),
                                                 New ReportCeBaseForm.RptParam("V_LANGUAGE_CODE", languageCode)}




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

            Dim companyId As Guid = ElitaPlusIdentity.Current.ActiveUser.FirstCompanyID
            Dim compCode As String = LookupListNew.GetCodeFromId("COMPANIES", companyId)
            Dim selectedServiceCenterId As Guid
            'Dim dvServiceCenter As DataView = LookupListNew.GetServiceCenterLookupList(ElitaPlusIdentity.Current.ActiveUser.Country(ElitaPlusIdentity.Current.ActiveUser.FirstCompanyID).Id)
            Dim serviceCenter As String
            Dim languageID As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
            Dim languageCode As String = LookupListNew.GetCodeFromId("LANGUAGES", languageID)
            Dim dvCountry As DataView = LookupListNew.GetUserCountriesLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies)
            Dim countryCode As String

            If cboCountry.Visible = False Then
                selectedCountryId = GetSelectedItem(cboCountry)
                countryCode = LookupListNew.GetCodeFromId(dvCountry, selectedCountryId)
            Else
                'countryCode = ALL
                selectedCountryId = GetSelectedItem(cboCountry)
                countryCode = LookupListNew.GetCodeFromId(dvCountry, selectedCountryId)
                If selectedCountryId.Equals(Guid.Empty) Then
                    ElitaPlusPage.SetLabelError(moCountryLabel)
                    Throw New GUIException(Message.MSG_INVALID_COUNTRY, Assurant.ElitaPlus.Common.ErrorCodes.GUI_COUNTRY_MUST_BE_SELECTED_ERR)
                End If
            End If

            If rServiceCenters.Checked Then
                serviceCenter = ALL
            Else
                selectedServiceCenterId = ServiceCenterMultipleDrop.SelectedGuid 'Me.GetSelectedItem(Me.cboServiceCentersCode)
                serviceCenter = ServiceCenterMultipleDrop.SelectedCode 'LookupListNew.GetCodeFromId(dvServiceCenter, selectedServiceCenterId)
                If selectedServiceCenterId.Equals(Guid.Empty) Then
                    ElitaPlusPage.SetLabelError(ServiceCenterMultipleDrop.CaptionLabel)
                    Throw New GUIException(Message.MSG_INVALID_SERVICE_CENTER, Assurant.ElitaPlus.Common.ErrorCodes.GUI_SERVICE_CENTER_MUST_BE_SELECTED_ERR)
                    'ElseIf cboCountry.Visible = True And selectedCountryId.Equals(Guid.Empty) Then
                    '    Throw New GUIException(Message.MSG_INVALID_COUNTRY, Assurant.ElitaPlus.Common.ErrorCodes.GUI_COUNTRY_MUST_BE_SELECTED_ERR)
                End If
            End If

            ReportCeBase.EnableReportCe(Me, TheRptCeInputControl)

            reportFormat = ReportCeBase.GetReportFormat(Me)

            Dim params As ReportCeBaseForm.Params = SetParameters(compCode, countryCode, serviceCenter, languageCode)
            Session(ReportCeBaseForm.SESSION_PARAMETERS_KEY) = params

        End Sub

#End Region

    End Class

End Namespace
