Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Namespace Reports
    Partial Public Class MissingAcctingDataReportForm
        Inherits ElitaPlusPage

#Region "Constants"

        Private Const RPT_FILENAME_WINDOW As String = "MISSING_ACCTING_DATA"
        Private Const RPT_FILENAME As String = "MissingAccountingData"
        Public Const ALL As String = "*"
        Public Const YES As String = "Y"
        Protected WithEvents btnViewHidden As System.Web.UI.WebControls.Button
        Public Const NO As String = "N"
        Private Const LABEL_SELECT_COMPANY As String = "SELECT_COMPANY"


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
        Public ReadOnly Property CompanyMultipleDrop() As MultipleColumnDDLabelControl
            Get
                If moCompanyMultipleDrop Is Nothing Then
                    moCompanyMultipleDrop = CType(FindControl("multipleDropControl"), MultipleColumnDDLabelControl)
                End If
                Return moCompanyMultipleDrop
            End Get
        End Property
#End Region

#Region "variables"
        Private moReportFormat As ReportCeBaseForm.RptFormat
#End Region

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub

        Protected WithEvents moCompanyMultipleDrop As MultipleColumnDDLabelControl
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
                    TheRptCeInputControl.ExcludeExport()
                Else
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

#Region "Clear"

        Private Sub ClearErrLabels()
            Me.ClearLabelErrSign(CompanyMultipleDrop.CaptionLabel)
        End Sub

#End Region

#Region "Populate"

        Sub PopulateCompanyDropDown()

            Dim dv As DataView = LookupListNew.GetUserCompaniesLookupList()
            If dv.Count <= 1 Then
                'CompanyMultipleDrop.NothingSelected = False
                CompanyMultipleDrop.SetControl(False, CompanyMultipleDrop.MODES.NEW_MODE, False, dv, "* " & TranslationBase.TranslateLabelOrMessage(LABEL_SELECT_COMPANY), True)
            Else
                'CompanyMultipleDrop.NothingSelected = False
                CompanyMultipleDrop.SetControl(True, CompanyMultipleDrop.MODES.NEW_MODE, True, dv, "* " & TranslationBase.TranslateLabelOrMessage(LABEL_SELECT_COMPANY), True)
            End If
        End Sub

        Private Sub InitializeForm()
            TheRptCeInputControl.populateReportLanguages(RPT_FILENAME)
            PopulateCompanyDropDown()
        End Sub

#End Region

#Region "Crystal Enterprise"

        Function SetParameters(ByVal CompanyId As String, ByVal CompanyDesc As String) As ReportCeBaseForm.Params


            Dim culturevalue As String = TheRptCeInputControl.getCultureValue(False)
            Dim reportName As String = TheRptCeInputControl.getReportName(RPT_FILENAME, False)

            Dim params As New ReportCeBaseForm.Params
            Dim moReportFormat As ReportCeBaseForm.RptFormat
            moReportFormat = ReportCeBase.GetReportFormat(Me)

            Dim repParams() As ReportCeBaseForm.RptParam = New ReportCeBaseForm.RptParam() _
                                    {
                                     New ReportCeBaseForm.RptParam("V_COMPANY_ID", CompanyId),
                                     New ReportCeBaseForm.RptParam("V_COMPANY_DESC", CompanyDesc),
                                     New ReportCeBaseForm.RptParam("LANG_CULTURE_VALUE", culturevalue)}

            Me.rptWindowTitle.InnerText = TheRptCeInputControl.getReportWindowTitle(TranslationBase.TranslateLabelOrMessage(RPT_FILENAME_WINDOW))

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
            Dim CompanyId As Guid = CompanyMultipleDrop.SelectedGuid
            Dim CompanyDesc As String = CompanyMultipleDrop.SelectedDesc 'LookupListNew.GetCodeFromId(dv, selectedDealerId)
            Dim endDate As String
            Dim beginDate As String
            Dim oCountryId As Guid = ElitaPlusIdentity.Current.ActiveUser.Country(ElitaPlusIdentity.Current.ActiveUser.FirstCompanyID).Id
            Dim oCountry As New Country(oCountryId)
            Dim params As ReportCeBaseForm.Params

            'Currency
            Dim Currencyid As Guid = oCountry.PrimaryCurrencyId
            Dim strCurrency As String = LookupListNew.GetDescriptionFromId("CURRENCIES", Currencyid)

            If CompanyId.Equals(Guid.Empty) Then
                ElitaPlusPage.SetLabelError(CompanyMultipleDrop.CaptionLabel)
                Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_COMPANY_IS_REQUIRED)
            End If

            ReportCeBase.EnableReportCe(Me, TheRptCeInputControl)

            moReportFormat = ReportCeBase.GetReportFormat(Me)

            params = SetParameters(GuidControl.GuidToHexString(CompanyId), CompanyDesc)

            Session(ReportCeBaseForm.SESSION_PARAMETERS_KEY) = params

        End Sub
#End Region


    End Class
End Namespace