Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Imports System.Globalization
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports System.Threading
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms

Namespace Reports

    Partial Class ServiceGroupReportForm
        Inherits ElitaPlusPage

#Region "Constants"

        Private Const RPT_FILENAME_WINDOW As String = "Service Group Report"
        Private Const RPT_FILENAME As String = "ServiceGroupsReport"
        Private Const RPT_FILENAME_EXPORT As String = "ServiceGroupsReport_Exp"

        Private Const TOTAL_PARAMS As Integer = 7 ' 8 Elements
        Private Const TOTAL_EXP_PARAMS As Integer = 3 ' 4 Elements
        Private Const PARAMS_PER_REPORT As Integer = 4 ' 4 Elements
        Private Const ALL As String = "*"
        Private Const SP_DATE_FORMAT As String = "yyyyMMdd"
        Private Const YES As String = "Y"
        Private Const NO As String = "N"
        Private Const LABEL_SELECT_SERVICE_GROUP As String = "Or Only Service Group"
#End Region

#Region "Parameters"

        Public Structure ReportParams
            Public companyCode As String
            Public serviceGroup As String
            Public mfg As String
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
        Public ReadOnly Property ServiceGroupMultipleDrop() As MultipleColumnDDLabelControl
            Get
                If moServiceGroupMultipleDrop Is Nothing Then
                    moServiceGroupMultipleDrop = CType(FindControl("multipleDropControl"), MultipleColumnDDLabelControl)
                End If
                Return moServiceGroupMultipleDrop
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
        Protected WithEvents moServiceGroupMultipleDrop As MultipleColumnDDLabelControl
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
            ClearLabelErrSign(ServiceGroupMultipleDrop.CaptionLabel)

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

        Private Sub PopulateServiceGroupDropDown()
            '''Me.BindListControlToDataView(Me.cboServiceGroupsDec, LookupListNew.GetServiceGroupLookupList(ElitaPlusIdentity.Current.ActiveUser.Country(ElitaPlusIdentity.Current.ActiveUser.FirstCompanyID).Id), , , True)
            '''Dim ServiceGroupLookupListSortedByCode As DataView = LookupListNew.GetServiceGroupLookupList(ElitaPlusIdentity.Current.ActiveUser.Country(ElitaPlusIdentity.Current.ActiveUser.FirstCompanyID).Id)
            '''ServiceGroupLookupListSortedByCode.Sort = "CODE"
            '''Me.BindListControlToDataView(Me.cboServiceGroupsCode, ServiceGroupLookupListSortedByCode, "CODE", , True)
            ''''Me.BindListControlToDataView(Me.cboServiceGroupsCode, LookupListNew.GetServiceGroupLookupList(ElitaPlusIdentity.Current.ActiveUser.CompanyId), "CODE", , True)
            Dim dv As DataView = LookupListNew.GetServiceGroupLookupList(ElitaPlusIdentity.Current.ActiveUser.Country(ElitaPlusIdentity.Current.ActiveUser.FirstCompanyID).Id)
            ServiceGroupMultipleDrop.NothingSelected = True
            ServiceGroupMultipleDrop.SetControl(False, ServiceGroupMultipleDrop.MODES.NEW_MODE, True, dv, TranslationBase.TranslateLabelOrMessage(LABEL_SELECT_SERVICE_GROUP), True, False, " document.forms[0].rServiceGroups.checked = false;")

        End Sub

        Private Sub PopulateMfgDropDown()
            '   Me.BindListControlToDataView(moMfgDrop, LookupListNew.GetManufacturerLookupList(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id))
            ' Dim compGroupId As Guid = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
            Dim listcontext As ListContext = New ListContext()
            listcontext.CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
            Dim mfgLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("ManufacturerByCompanyGroup", Thread.CurrentPrincipal.GetLanguageCode(), listcontext)
            moMfgDrop.Populate(mfgLkl, New PopulateOptions() With
             {
               .AddBlankItem = True
                })
        End Sub

        Private Sub InitializeForm()
            PopulateServiceGroupDropDown()
            PopulateMfgDropDown()
            rServiceGroups.Checked = True
        End Sub

#End Region

#Region "Crystal Enterprise"

        Function SetParameters(companyCode As String, serviceGroup As String, mfg As String) As ReportCeBaseForm.Params

            Dim reportFormat As ReportCeBaseForm.RptFormat
            Dim params As New ReportCeBaseForm.Params
            '''Dim repParams(TOTAL_PARAMS) As ReportCeBaseForm.RptParam
            Dim rptParams As ReportParams

            With rptParams
                .companyCode = companyCode
                .serviceGroup = serviceGroup
                .mfg = mfg
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
                                                 New ReportCeBaseForm.RptParam("V_SERVICE_GROUP_CODE", serviceGroup),
                                                 New ReportCeBaseForm.RptParam("V_MFG", mfg)}

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
            Dim selectedServiceGroupId As Guid
            'Dim dvServiceGroup As DataView = LookupListNew.GetServiceGroupLookupList(ElitaPlusIdentity.Current.ActiveUser.Country(ElitaPlusIdentity.Current.ActiveUser.FirstCompanyID).Id)
            Dim serviceGroup As String

            Dim mfg As String
            mfg = moMfgDrop.SelectedItem.Text

            If rServiceGroups.Checked Then
                serviceGroup = ALL
            Else
                selectedServiceGroupId = ServiceGroupMultipleDrop.SelectedGuid 'Me.GetSelectedItem(Me.cboServiceGroupsCode)
                serviceGroup = ServiceGroupMultipleDrop.SelectedCode 'LookupListNew.GetCodeFromId(dvServiceGroup, selectedServiceGroupId)
                If selectedServiceGroupId.Equals(Guid.Empty) Then
                    ElitaPlusPage.SetLabelError(ServiceGroupMultipleDrop.CaptionLabel)
                    Throw New GUIException(Message.MSG_INVALID_SERVICE_GROUP, Assurant.ElitaPlus.Common.ErrorCodes.GUI_SERVICE_GROUP_MUST_BE_SELECTED_ERR)
                End If
            End If

            ReportCeBase.EnableReportCe(Me, TheRptCeInputControl)

            reportFormat = ReportCeBase.GetReportFormat(Me)

            Dim params As ReportCeBaseForm.Params = SetParameters(compCode, serviceGroup, mfg)
            Session(ReportCeBaseForm.SESSION_PARAMETERS_KEY) = params

        End Sub

#End Region

    End Class

End Namespace
