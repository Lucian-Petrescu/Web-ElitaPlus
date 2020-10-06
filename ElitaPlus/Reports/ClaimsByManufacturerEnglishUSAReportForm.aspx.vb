Imports System.Text.RegularExpressions
Imports System.Security.Cryptography.X509Certificates
Imports Assurant.ElitaPlus.DALObjects
Imports System.Globalization
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports System.Threading
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms
Namespace Reports
    Partial Class ClaimsByManufacturerEnglishUSAReportForm
        Inherits ElitaPlusPage


#Region "Constants"

        '   Private Const RPT_FILENAME As String = "Claims Following Service Warranty English (USA)_TEST7"
        Private Const RPT_FILENAME_WINDOW As String = "CLAIMS BY MANUFACTURER"
        Private Const RPT_FILENAME As String = "ClaimsByManufacturer_EN"
        Private Const RPT_FILENAME_EXPORT As String = "ClaimsByManufacturer-Exp_EN"

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


#End Region

#Region "variables"
        Dim moReportFormat As ReportCeBaseForm.RptFormat
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
        Protected WithEvents isReportCeVisible As System.Web.UI.HtmlControls.HtmlInputHidden
        Protected WithEvents isProgressVisible As System.Web.UI.HtmlControls.HtmlInputHidden
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
            PopulateManufacturerDropDown()
            Dim t As Date = Date.Now.AddDays(-1)
            moBeginDateText.Text = GetDateFormattedString(t)
            moEndDateText.Text = GetDateFormattedString(Date.Now)
            rmanufacturer.Checked = True
        End Sub

        Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
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

#End Region

#Region "Clear"

        Private Sub ClearErrLabels()
            ClearLabelErrSign(moBeginDateLabel)
            ClearLabelErrSign(moEndDateLabel)
            ClearLabelErrSign(moManufacturerLabel)
        End Sub

#End Region

#Region "Populate"

        Sub PopulateManufacturerDropDown()
            'Dim oCompanyId As Guid = ElitaPlusIdentity.Current.ActiveUser.FirstCompanyID
            'Me.BindListControlToDataView(Me.cboManufacturer, LookupListNew.GetManufacturerLookupList(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id))
            Dim compGroupId As Guid = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
            Dim listcontext As ListContext = New ListContext()
            listcontext.CompanyGroupId = compGroupId
            Dim manufacturerLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("ManufacturerByCompanyGroup", Thread.CurrentPrincipal.GetLanguageCode(), listcontext)
            cboManufacturer.Populate(manufacturerLkl, New PopulateOptions() With
            {
              .AddBlankItem = True
            })
        End Sub

#End Region

#Region "Crystal Enterprise"

        Function SetParameters(userId As String, manufacturerIdParam As String, beginDate As String,
                                      endDate As String, selectedManufacturerName As String) As ReportCeBaseForm.Params

            Dim params As New ReportCeBaseForm.Params
            Dim reportName As String = RPT_FILENAME

            Dim exportData As String = NO

            moReportFormat = ReportCeBase.GetReportFormat(Me)
            If (moReportFormat = ReportCeBase.RptFormat.TEXT_TAB) OrElse (moReportFormat = ReportCeBase.RptFormat.TEXT_CSV) Then
                reportName = RPT_FILENAME_EXPORT
            End If

            Dim repParams() As ReportCeBaseForm.RptParam = New ReportCeBaseForm.RptParam() _
                    {
                     New ReportCeBaseForm.RptParam("V_USER_ID", userId),
                     New ReportCeBaseForm.RptParam("V_MANUFACTURER_ID", manufacturerIdParam),
                     New ReportCeBaseForm.RptParam("V_BEGIN_DATE", beginDate),
                     New ReportCeBaseForm.RptParam("V_END_DATE", endDate),
                     New ReportCeBaseForm.RptParam("P_MANUFACTURER_NAME", selectedManufacturerName),
                     New ReportCeBaseForm.RptParam("V_FREE_ZONE_FLAG", "ALL")}

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
            Dim selectedManufacturerId As Guid = GetSelectedItem(cboManufacturer)
            Dim dv As DataView = LookupListNew.GetManufacturerLookupList(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id)
            Dim selectedManufacturerName As String = LookupListNew.GetDescriptionFromId(dv, selectedManufacturerId)
            Dim manufacturerIdParam As String
            Dim endDate As String
            Dim beginDate As String

            'Dates
            ReportCeBase.ValidateBeginEndDate(moBeginDateLabel, moBeginDateText.Text, moEndDateLabel, moEndDateText.Text)
            endDate = ReportCeBase.FormatDate(moEndDateLabel, moEndDateText.Text)
            beginDate = ReportCeBase.FormatDate(moBeginDateLabel, moBeginDateText.Text)
            If rmanufacturer.Checked Then
                manufacturerIdParam = ALL
            Else
                If selectedManufacturerId.Equals(Guid.Empty) Then
                    ElitaPlusPage.SetLabelError(moManufacturerLabel)
                    Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_MANUFACTURER_MUST_BE_SELECTED_ERR)
                Else
                    manufacturerIdParam = GuidControl.GuidToHexString(selectedManufacturerId)
                End If
            End If

            ReportCeBase.EnableReportCe(Me, TheRptCeInputControl)

            'Dim params As ReportCeBaseForm.Params = SetParameters(compCode, manufacturerIdParam, beginDate, endDate, selectedManufacturerName)
            Dim params As ReportCeBaseForm.Params = SetParameters(GuidControl.GuidToHexString(userId), manufacturerIdParam, beginDate, endDate, selectedManufacturerName)
            Session(ReportCeBaseForm.SESSION_PARAMETERS_KEY) = params
        End Sub

#End Region

    End Class
End Namespace
