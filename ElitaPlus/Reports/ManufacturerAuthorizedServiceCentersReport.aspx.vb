Imports Assurant.ElitaPlus.DALObjects
Imports Assurant.Common.Ftp
Imports Assurant.ElitaPlus.Common.MiscUtil
Imports Assurant.ElitaPlus.ElitaPlusWebApp.Reports
Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Imports System.Threading
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms

Namespace Reports

    Partial Class ManufacturerAuthorizedServiceCentersReport
        Inherits ElitaPlusPage

#Region "Constants"

        Private Const RPT_FILENAME_WINDOW As String = "Manufacturer Authorized Service Centers Report"
        Private Const RPT_FILENAME As String = "ManufacturerAuthorizedServiceCentersReport"
        Private Const RPT_FILENAME_EXPORT As String = "ManufacturerAuthorizedServiceCentersReport_Exp"

        Private Const TOTAL_PARAMS As Integer = 7 ' 8 Elements
        Private Const TOTAL_EXP_PARAMS As Integer = 3 ' 4 Elements
        Private Const PARAMS_PER_REPORT As Integer = 4 ' 4 Elements
        Private Const ALL As String = "*"
        Private Const SP_DATE_FORMAT As String = "yyyyMMdd"
        Private Const YES As String = "Y"
        Private Const NO As String = "N"

#End Region

#Region "Parameters"

        Public Structure ReportParams
            Public companyCode As String
            Public manufacturerCode As String
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

#Region "Populate"

        Private Sub PopulateManufacturerDropDown()
            Dim oListContext As New ListContext
            oListContext.CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
            Dim manufacturerList As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="ManufacturerByCompanyGroup", context:=oListContext, languageCode:=Thread.CurrentPrincipal.GetLanguageCode())

            cboManufacturer.Populate(manufacturerList, New PopulateOptions() With
                {
                    .AddBlankItem = True
                })

            'Me.BindListControlToDataView(Me.cboManufacturer, LookupListNew.GetManufacturerLookupList(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id), , , True)
        End Sub

        Private Sub InitializeForm()
            PopulateManufacturerDropDown()
            rManufacturers.Checked = True
        End Sub

#End Region

#Region "Crystal Enterprise"

        Function SetParameters(companyCode As String, manufacturerCode As String) As ReportCeBaseForm.Params

            Dim reportFormat As ReportCeBaseForm.RptFormat
            Dim params As New ReportCeBaseForm.Params
            '''Dim repParams(TOTAL_PARAMS) As ReportCeBaseForm.RptParam
            Dim rptParams As ReportParams

            With rptParams
                .companyCode = companyCode
                .manufacturerCode = manufacturerCode
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
                                                 New ReportCeBaseForm.RptParam("V_MANUFACTURER_CODE", manufacturerCode)}




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
            Dim selectedManufacturerId As Guid
            Dim dvManufacturers As DataView = LookupListNew.GetManufacturerLookupList(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id)
            Dim manufacturerCode As String
            Dim detailCode As String



            If rManufacturers.Checked Then
                manufacturerCode = ALL
            Else
                selectedManufacturerId = GetSelectedItem(cboManufacturer)
                manufacturerCode = LookupListNew.GetCodeFromId(dvManufacturers, selectedManufacturerId)
                If selectedManufacturerId.Equals(Guid.Empty) Then
                    Throw New GUIException(Message.MSG_INVALID_MANUFACTURER, Assurant.ElitaPlus.Common.ErrorCodes.GUI_MANUFACTURER_MUST_BE_SELECTED_ERR)
                End If
            End If

            ReportCeBase.EnableReportCe(Me, TheRptCeInputControl)

            reportFormat = ReportCeBase.GetReportFormat(Me)

            Dim params As ReportCeBaseForm.Params = SetParameters(compCode, manufacturerCode)
            Session(ReportCeBaseForm.SESSION_PARAMETERS_KEY) = params

        End Sub

        '''Sub SetReportParams(ByVal rptParams As ReportParams, ByVal repParams() As ReportCeBaseForm.RptParam, _
        '''                    ByVal rptName As String, ByVal startIndex As Integer)

        '''    With rptParams
        '''        repParams(startIndex) = New ReportCeBaseForm.RptParam("V_COMPANY", .companyCode, rptName)
        '''        repParams(startIndex + 1) = New ReportCeBaseForm.RptParam("V_DEALER_CODE", .manufacturerCode, rptName)
        '''    End With

        '''End Sub

#End Region

    End Class

End Namespace
