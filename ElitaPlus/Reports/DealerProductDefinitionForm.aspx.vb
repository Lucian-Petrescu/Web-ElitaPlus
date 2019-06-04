Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common

Namespace Reports

    Partial Class DealerProductDefinitionForm
        Inherits BaseReportPage

#Region "Constants"

        Private Const RPT_FILENAME_WINDOW As String = "DEALER PRODUCT DEFINITION"
        Private Const RPT_FILENAME As String = "DealerProductDefinitionReport"
        Private Const RPT_FILENAME_EXPORT As String = "DealerProductDefinitionReport_Exp"

        Private Const REPORT_TITLE_UI_PROG_CODE As String = "Dealer_Product_Definition_Report_For"
        Private Const REPORT_PAGE_UI_PROG_CODE As String = "Page"
        Private Const REPORT_OF_UI_PROG_CODE As String = "Of"

        Private Const TOTAL_PARAMS As Integer = 7 ' 8 Elements
        Private Const TOTAL_EXP_PARAMS As Integer = 3 ' 4 Elements
        Private Const PARAMS_PER_REPORT As Integer = 4 ' 4 Elements
        Private Const ALL As String = "*"
        Private Const SP_DATE_FORMAT As String = "yyyyMMdd"
        Private Const YES As String = "Y"
        Private Const NO As String = "N"
        Private Const LABEL_SELECT_DEALER As String = "SELECT_DEALER"
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
                    moDealerMultipleDrop = CType(FindControl("multipleDropControl"), MultipleColumnDDLabelControl)
                End If
                Return moDealerMultipleDrop
            End Get
        End Property
#End Region

#Region "variables"
        Private reportFormat As ReportCeBaseForm.RptFormat
        Protected WithEvents ErrorCtrl As ErrorController
        Protected WithEvents lblType As System.Web.UI.WebControls.Label
        Protected WithEvents moReportCeInputControl As ReportCeInputControl
        Protected WithEvents cboDealerCode As System.Web.UI.WebControls.DropDownList
        Protected WithEvents Label2 As System.Web.UI.WebControls.Label
        Protected WithEvents cboDealerDec As System.Web.UI.WebControls.DropDownList
        Protected WithEvents lblCode As System.Web.UI.WebControls.Label
        Protected WithEvents lblDescription As System.Web.UI.WebControls.Label
        Protected WithEvents moDealerMultipleDrop As MultipleColumnDDLabelControl
        Private reportName As String = RPT_FILENAME
#End Region


#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub


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
                    Me.ClearLabelErrSign(DealerMultipleDrop.CaptionLabel)
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

#Region "Populate"

        Private Sub InitializeForm()
            Dim dv As DataView = LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies, True)
            DealerMultipleDrop.NothingSelected = True
            DealerMultipleDrop.SetControl(False, DealerMultipleDrop.MODES.NEW_MODE, True, dv, "* " & TranslationBase.TranslateLabelOrMessage(LABEL_SELECT_DEALER), True)
        End Sub

#End Region

#Region "Crystal Enterprise"

        Function SetParameters(ByVal dealerId As String, ByVal languageCode As String) As ReportCeBaseForm.Params

            Dim reportFormat As ReportCeBaseForm.RptFormat
            Dim params As New ReportCeBaseForm.Params
            '''Dim repParams(TOTAL_PARAMS) As ReportCeBaseForm.RptParam

            reportFormat = ReportCeBase.GetReportFormat(Me)

            Dim exportData As String = NO

            reportName = Me.RPT_FILENAME
            If (reportFormat = ReportCeBase.RptFormat.TEXT_TAB) OrElse (reportFormat = ReportCeBase.RptFormat.TEXT_CSV) Then
                exportData = YES
                reportName = RPT_FILENAME_EXPORT
            End If

            Dim repParams() As ReportCeBaseForm.RptParam = New ReportCeBaseForm.RptParam() _
                                                {
                                                New ReportCeBaseForm.RptParam("V_DEALER_ID", dealerId),
                                                New ReportCeBaseForm.RptParam("V_LANGUAGE_ID", languageCode)}

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

            Dim languageID As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
            Dim languageCode As String = LookupListNew.GetCodeFromId("LANGUAGES", languageID)

            Dim selectedDealerId As Guid = DealerMultipleDrop.SelectedGuid 'Me.GetSelectedItem(Me.cboDealerDec)

            If selectedDealerId.Equals(Guid.Empty) Then
                ElitaPlusPage.SetLabelError(DealerMultipleDrop.CaptionLabel)
                Throw New GUIException(Message.MSG_INVALID_DEALER, Assurant.ElitaPlus.Common.ErrorCodes.GUI_DEALER_MUST_BE_SELECTED_ERR)
            End If

            ReportCeBase.EnableReportCe(Me, TheRptCeInputControl)

            reportFormat = ReportCeBase.GetReportFormat(Me)

            Dim params As ReportCeBaseForm.Params = SetParameters(GuidControl.GuidToHexString(selectedDealerId), languageCode)
            Session(ReportCeBaseForm.SESSION_PARAMETERS_KEY) = params

        End Sub

#End Region


    End Class

End Namespace