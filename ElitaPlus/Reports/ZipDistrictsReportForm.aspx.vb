Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Imports System.Globalization
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports System.Threading
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms
Namespace Reports

    Partial Class ZipDistrictsReportForm
        Inherits ElitaPlusPage

#Region "Constants"

        Private Const RPT_FILENAME_WINDOW As String = "ZIP_DISTRICTS"
        Private Const RPT_FILENAME As String = "ZipDistrictsReport"
        Private Const RPT_FILENAME_EXPORT As String = "ZipDistrictsReport_Exp"

        Private Const TOTAL_PARAMS As Integer = 7 ' 8 Elements
        Private Const TOTAL_EXP_PARAMS As Integer = 3 ' 4 Elements
        Private Const PARAMS_PER_REPORT As Integer = 4 ' 4 Elements
        Private Const ALL As String = "*"
        Private Const SP_DATE_FORMAT As String = "yyyyMMdd"
        Private Const YES As String = "Y"
        Private Const NO As String = "N"
        Private Const LABEL_SELECT_ZIP_DISTRICT As String = "OR_ONLY_ZIP_DISTRICT"
#End Region

#Region "Parameters"

        Public Structure ReportParams
            Public countryCode As String
            Public zipDistrict As String
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
        Private reportName As String = RPT_FILENAME
#End Region

#Region "Handlers"


#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub
        Protected WithEvents ErrorCtrl As ErrorController
        Protected WithEvents moReportCeInputControl As ReportCeInputControl
        Protected WithEvents moZipDistrictsMultipleDrop As MultipleColumnDDLabelControl
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

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            Me.ErrorCtrl.Clear_Hide()
            Try
                If Not Me.IsPostBack Then
                    InitializeForm()
                Else
                    ClearErrLabels()
                End If
                Me.InstallProgressBar()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try
            Me.ShowMissingTranslations(Me.ErrorCtrl)
        End Sub
        Private Sub ClearErrLabels()
            Me.ClearLabelErrSign(ZipDistrictsMultipleDrop.CaptionLabel)

        End Sub
#End Region

#Region "Handlers-Drop"

        Private Sub moCountryDrop_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles moCountryDrop.SelectedIndexChanged
            PopulateZipDistrictDrop()
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

#End Region

#Region "Populate"

        Private Sub PopulateCountry()
            'Me.BindListControlToDataView(moCountryDrop, LookupListNew.GetUserCountriesLookupList(), , , False)
            Dim listcontext As ListContext = New ListContext()
            Dim countryLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList(ListCodes.Country, Thread.CurrentPrincipal.GetLanguageCode(), listcontext)
            Dim list As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Countries

            Dim filteredList As ListItem() = (From x In countryLkl
                                              Where list.Contains(x.ListItemId)
                                              Select x).ToArray()
            moCountryDrop.Populate(filteredList, New PopulateOptions())
            moCountryDrop.SelectedIndex = 0
            Me.PopulateControlFromBOProperty(moCountryLabel_NO_TRANSLATE, Me.GetSelectedDescription(moCountryDrop))

            If moCountryDrop.Items.Count > 1 Then
                ' Multiple Countries
                ControlMgr.SetVisibleControl(Me, moCountryLabel_NO_TRANSLATE, False)
                Me.PopulateControlFromBOProperty(moLabelReq_NO_TRANSLATE, "*")
            Else
                ControlMgr.SetVisibleControl(Me, moCountryDrop, False)
                ControlMgr.SetVisibleControl(Me, moLabelReq_NO_TRANSLATE, False)
            End If
        End Sub

        Private Sub PopulateZipDistrictDrop()
            '''' Me.BindListControlToDataView(Me.cboZipDistrictsDec, LookupListNew.GetZipDistrictLookupList(ElitaPlusIdentity.Current.ActiveUser.CompanyId), , , True)
            '''Me.BindListControlToDataView(Me.cboZipDistrictsDec, LookupListNew.GetZipDistrictLookupList(Me.GetSelectedItem(moCountryDrop)), , , True)
            '''Dim ZipDistrictsLookupListSortedByCode As DataView = LookupListNew.GetZipDistrictLookupList(Me.GetSelectedItem(moCountryDrop))
            '''ZipDistrictsLookupListSortedByCode.Sort = "CODE"
            '''Me.BindListControlToDataView(Me.cboZipDistrictsCode, ZipDistrictsLookupListSortedByCode, "CODE", , True)
            '''' Me.BindListControlToDataView(Me.cboZipDistrictsCode, LookupListNew.GetZipDistrictLookupList(ElitaPlusIdentity.Current.ActiveUser.CompanyId), "CODE", , True)

            Dim dv As DataView = LookupListNew.GetZipDistrictLookupList(Me.GetSelectedItem(moCountryDrop))
            ZipDistrictsMultipleDrop.NothingSelected = True
            ZipDistrictsMultipleDrop.SetControl(False, ZipDistrictsMultipleDrop.MODES.NEW_MODE, True, dv, TranslationBase.TranslateLabelOrMessage(LABEL_SELECT_ZIP_DISTRICT), True, False, " document.forms[0].rZipDistricts.checked = false;")

        End Sub


        Private Sub InitializeForm()
            PopulateCountry()
            PopulateZipDistrictDrop()
            Me.rZipDistricts.Checked = True
        End Sub

#End Region

#Region "Crystal Enterprise"

        Function SetParameters(ByVal countryCode As String, ByVal zipDistrict As String) As ReportCeBaseForm.Params

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

            reportName = Me.RPT_FILENAME
            If (reportFormat = ReportCeBase.RptFormat.TEXT_TAB) OrElse (reportFormat = ReportCeBase.RptFormat.TEXT_CSV) Then
                exportData = YES
                reportName = RPT_FILENAME_EXPORT
            End If

            Dim repParams() As ReportCeBaseForm.RptParam = New ReportCeBaseForm.RptParam() _
                                                {
                                                 New ReportCeBaseForm.RptParam("V_COUNTRY", countryCode),
                                                 New ReportCeBaseForm.RptParam("V_DISTRICT_CODE", zipDistrict)}




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
            Dim oCountryId As Guid = Me.GetSelectedItem(moCountryDrop)
            Dim countryCode As String = LookupListNew.GetCodeFromId(LookupListNew.LK_COUNTRIES, oCountryId)
            Dim selectedZipDistrictId As Guid
            'Dim dvZipDistricts As DataView = LookupListNew.GetZipDistrictLookupList(oCountryId)
            Dim zipDistrict As String
            Dim detailCode As String



            If Me.rZipDistricts.Checked Then
                zipDistrict = ALL
            Else
                selectedZipDistrictId = ZipDistrictsMultipleDrop.SelectedGuid 'Me.GetSelectedItem(Me.cboZipDistrictsCode)
                zipDistrict = ZipDistrictsMultipleDrop.SelectedCode 'LookupListNew.GetCodeFromId(dvZipDistricts, selectedZipDistrictId)
                If selectedZipDistrictId.Equals(Guid.Empty) Then
                    ElitaPlusPage.SetLabelError(ZipDistrictsMultipleDrop.CaptionLabel)
                    Throw New GUIException(Message.MSG_INVALID_ZIP_DISTRICT, Assurant.ElitaPlus.Common.ErrorCodes.GUI_ZIP_DISTRICT_MUST_BE_SELECTED_ERR)
                End If
            End If

            ReportCeBase.EnableReportCe(Me, TheRptCeInputControl)

            reportFormat = ReportCeBase.GetReportFormat(Me)

            Dim params As ReportCeBaseForm.Params = SetParameters(countryCode, zipDistrict)
            Session(ReportCeBaseForm.SESSION_PARAMETERS_KEY) = params

        End Sub


#End Region


    End Class

End Namespace
