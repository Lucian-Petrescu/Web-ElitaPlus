Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Imports System.Globalization
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports System.Threading
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms
Imports Microsoft.VisualBasic

Namespace Reports

    Partial Class CoveredZipDistrictsReportForm
        Inherits ElitaPlusPage

#Region "Constants"

        Private Const RPT_FILENAME_WINDOW As String = "COVERED_ZIP_DISTRICTS"
        Private Const RPT_FILENAME As String = "CoveredZipDistrictsReport"
        Private Const RPT_FILENAME_EXPORT As String = "CoveredZipDistrictsReport_Exp"

        Private Const TOTAL_PARAMS As Integer = 7 ' 8 Elements
        Private Const TOTAL_EXP_PARAMS As Integer = 3 ' 4 Elements
        Private Const PARAMS_PER_REPORT As Integer = 4 ' 4 Elements
        Private Const ALL As String = "*"
        Private Const SP_DATE_FORMAT As String = "yyyyMMdd"
        Private Const YES As String = "Y"
        Private Const NO As String = "N"
        Private Const LABEL_OR_ONLY_ZIP_DISTRICT As String = "OR_ONLY_ZIP_DISTRICT"
#End Region

#Region "Parameters"

        Public Structure ReportParams
            Public countryCode As String
            Public zipDistrict As String
        End Structure

#End Region

#Region "Properties"
        Public ReadOnly Property MyReportCeInputControl() As ReportCeInputControl
            Get
                If moReportCeInputControl Is Nothing Then
                    moReportCeInputControl = CType(FindControl("moReportCeInputControl"), ReportCeInputControl)
                End If
                Return moReportCeInputControl
            End Get
        End Property
        Public ReadOnly Property ZipDistrictsMultipleDrop() As MultipleColumnDDLabelControl
            Get
                If moZipDistrictsMultipleDrop Is Nothing Then
                    moZipDistrictsMultipleDrop = CType(FindControl("multipleDropControl"), MultipleColumnDDLabelControl)
                End If
                Return moZipDistrictsMultipleDrop
            End Get
        End Property
#End Region

#Region "variables"
        Private reportFormat As ReportCeBaseForm.RptFormat
        Protected WithEvents lblZIPDISTRICTS As System.Web.UI.WebControls.Label
        Protected WithEvents moZipDistrictsMultipleDrop As MultipleColumnDDLabelControl
        Private reportName As String = RPT_FILENAME
#End Region

#Region "Handlers"


#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub
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

#End Region

#Region "Handlers-Drop"

        Private Sub moCountryDrop_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles moCountryDrop.SelectedIndexChanged
            PopulateZipDistrictDrop()
        End Sub

#End Region

#Region "Clear"

        Private Sub ClearErrLabels()
            ClearLabelErrSign(ZipDistrictsMultipleDrop.CaptionLabel)
            If rZipDistricts.Checked Then ZipDistrictsMultipleDrop.SelectedIndex = -1
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

#End Region

#Region "Populate"

        Private Sub PopulateCountry()
            '  Me.BindListControlToDataView(moCountryDrop, LookupListNew.GetUserCountriesLookupList(), , , False)
            Dim listcontext As ListContext = New ListContext()
            Dim countryLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList(ListCodes.Country, Thread.CurrentPrincipal.GetLanguageCode(), listcontext)
            Dim list As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Countries

            Dim filteredList As ListItem() = (From x In countryLkl
                                              Where list.Contains(x.ListItemId)
                                              Select x).ToArray()
            moCountryDrop.Populate(filteredList, New PopulateOptions())
            moCountryDrop.SelectedIndex = 0
            PopulateControlFromBOProperty(moCountryLabel_NO_TRANSLATE, GetSelectedDescription(moCountryDrop))

            If moCountryDrop.Items.Count > 1 Then
                ' Multiple Countries
                ControlMgr.SetVisibleControl(Me, moCountryLabel_NO_TRANSLATE, False)
            Else
                ControlMgr.SetVisibleControl(Me, moCountryDrop, False)
            End If
        End Sub

        Private Sub PopulateZipDistrictDrop()
            ''''     Me.BindListControlToDataView(Me.cboZipDistrictsDec, LookupListNew.GetZipDistrictLookupList(ElitaPlusIdentity.Current.ActiveUser.CompanyId), , , True)
            '''Me.BindListControlToDataView(Me.cboZipDistrictsDec, LookupListNew.GetZipDistrictLookupList(Me.GetSelectedItem(moCountryDrop)), , , True)
            '''Dim ZipDistrictsLookupListSortedByCode As DataView = LookupListNew.GetZipDistrictLookupList(Me.GetSelectedItem(moCountryDrop))
            '''ZipDistrictsLookupListSortedByCode.Sort = "CODE"
            '''Me.BindListControlToDataView(Me.cboZipDistrictsCode, ZipDistrictsLookupListSortedByCode, "CODE", , True)
            ''''Me.BindListControlToDataView(Me.cboZipDistrictsCode, LookupListNew.GetZipDistrictLookupList(ElitaPlusIdentity.Current.ActiveUser.CompanyId), "CODE", , True)

            Dim dv As DataView = LookupListNew.GetZipDistrictLookupList(GetSelectedItem(moCountryDrop))
            ZipDistrictsMultipleDrop.NothingSelected = True
            ZipDistrictsMultipleDrop.SetControl(False, ZipDistrictsMultipleDrop.MODES.NEW_MODE, True, dv, TranslationBase.TranslateLabelOrMessage(LABEL_OR_ONLY_ZIP_DISTRICT), True, True, " document.forms[0].rZipDistricts.checked = false;", "", "", "", False, 3)

        End Sub




        Private Sub InitializeForm()
            PopulateCountry()
            PopulateZipDistrictDrop()
            rZipDistricts.Checked = True
        End Sub

#End Region

#Region "Crystal Enterprise"

        Function SetParameters(countryCode As String, zipDistrict As String) As ReportCeBaseForm.Params

            Dim reportFormat As ReportCeBaseForm.RptFormat
            Dim params As New ReportCeBaseForm.Params
            '''Dim repParams(TOTAL_PARAMS) As ReportCeBaseForm.RptParam
            Dim rptParams As ReportParams

            With rptParams
                .countryCode = countryCode
                .zipDistrict = zipDistrict
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
                                                 New ReportCeBaseForm.RptParam("V_COUNTRY", countryCode),
                                                 New ReportCeBaseForm.RptParam("V_ZIP_DISTRICT_CODE", zipDistrict)}




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

            'Dim companyId As Guid = ElitaPlusIdentity.Current.ActiveUser.FirstCompanyID
            'Dim compCode As String = LookupListNew.GetCodeFromId("COMPANIES", companyId)
            Dim oCountryId As Guid = GetSelectedItem(moCountryDrop)
            Dim countryCode As String = LookupListNew.GetCodeFromId(LookupListNew.LK_COUNTRIES, oCountryId)
            Dim selectedZipDistrictId As Guid
            ' Dim dvZipDistricts As DataView = LookupListNew.GetZipDistrictLookupList(oCountryId)
            Dim zipDistrict As String
            Dim detailCode As String



            If rZipDistricts.Checked Then
                zipDistrict = ALL
            Else
                selectedZipDistrictId = ZipDistrictsMultipleDrop.SelectedGuid 'Me.GetSelectedItem(Me.cboZipDistrictsCode)
                zipDistrict = ZipDistrictsMultipleDrop.SelectedCode 'LookupListNew.GetCodeFromId(dvZipDistricts, selectedZipDistrictId)
                If selectedZipDistrictId.Equals(Guid.Empty) Then
                    ElitaPlusPage.SetLabelError(ZipDistrictsMultipleDrop.CaptionLabel)
                    Throw New GUIException(Message.MSG_INVALID_ZIP_DISTRICT, Assurant.ElitaPlus.Common.ErrorCodes.GUI_ZIP_DISTRICT_MUST_BE_SELECTED_ERR)
                End If
            End If

            ReportCeBase.EnableReportCe(Me, MyReportCeInputControl)

            reportFormat = ReportCeBase.GetReportFormat(Me)

            Dim params As ReportCeBaseForm.Params = SetParameters(countryCode, zipDistrict)
            Session(ReportCeBaseForm.SESSION_PARAMETERS_KEY) = params

        End Sub

        '''Sub SetReportParams(ByVal rptParams As ReportParams, ByVal repParams() As ReportCeBaseForm.RptParam, _
        '''                    ByVal rptName As String, ByVal startIndex As Integer)

        '''    With rptParams
        '''        repParams(startIndex) = New ReportCeBaseForm.RptParam("V_COMPANY", .companyCode, rptName)
        '''        repParams(startIndex + 1) = New ReportCeBaseForm.RptParam("V_DEALER_CODE", .priceGroup, rptName)
        '''    End With

        '''End Sub

#End Region

      
    End Class

End Namespace
