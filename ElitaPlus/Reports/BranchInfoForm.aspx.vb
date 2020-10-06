Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Namespace Reports
    Partial Public Class BranchInfoForm
        Inherits ElitaPlusPage

#Region "Constants"

        Private Const RPT_FILENAME_WINDOW As String = "Branch_Info"
        Private Const RPT_FILENAME As String = "BranchInfo"
        Private Const RPT_FILENAME_EXPORT As String = "BranchInfo-Exp"

        Private Const TOTAL_PARAMS As Integer = 5 ' 8 Elements
        Private Const TOTAL_EXP_PARAMS As Integer = 5 ' 4 Elements
        Private Const PARAMS_PER_REPORT As Integer = 5 ' 4 Elements
        Private Const ALL As String = "*"
        Private Const SP_DATE_FORMAT As String = "yyyyMMdd"
        Private Const YES As String = "Y"
        Private Const NO As String = "N"
        Private Const LABEL_SELECT_DEALER As String = "OR A SINGLE DEALER"
        Public Const BY_ZIP_CODE As String = "1"
        Public Const BY_BRANCH_CODE As String = "2"
#End Region

#Region "Parameters"

        Public Structure ReportParams
            Public userid As String
            Public dealercode As String
            Public dealerdesc As String
            Public sortorder As String
            Public culturecode As String
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
        Public ReadOnly Property DealerMultipleDrop() As MultipleColumnDDLabelControl
            Get
                If multipleDropControl Is Nothing Then
                    multipleDropControl = CType(FindControl("multipleDropControl"), MultipleColumnDDLabelControl)
                End If
                Return multipleDropControl
            End Get
        End Property
#End Region

#Region "variables"
        Private reportFormat As ReportCeBaseForm.RptFormat
        Protected WithEvents ErrorCtrl As ErrorController
        Protected WithEvents moReportCeInputControl As ReportCeInputControl
        Dim moReportFormat As ReportCeBaseForm.RptFormat
#End Region

#Region "Handlers"


#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub

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
            ClearLabelsErrSign()
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
        Public Sub ClearLabelsErrSign()
            Try
                ClearLabelErrSign(DealerMultipleDrop.CaptionLabel)
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try
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
            ClearLabelErrSign(DealerMultipleDrop.CaptionLabel)
            If rdealer.Checked Then DealerMultipleDrop.SelectedIndex = -1
        End Sub

#End Region

#Region "Populate"

        Private Sub PopulateDealerDropDown()
            Dim dv As DataView = LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies, False, "CODE")
            DealerMultipleDrop.NothingSelected = True
            DealerMultipleDrop.SetControl(False, DealerMultipleDrop.MODES.NEW_MODE, True, dv, TranslationBase.TranslateLabelOrMessage(LABEL_SELECT_DEALER), True, True, " document.forms[0].rdealer.checked = false;", "", "", "", False, 5)

        End Sub



        Private Sub InitializeForm()
            PopulateDealerDropDown()
            rdealer.Checked = True
            rdReportSortOrder.Items(0).Selected = True
            TheRptCeInputControl.populateReportLanguages(RPT_FILENAME)

        End Sub

#End Region

#Region "Crystal Enterprise"

        Function SetParameters(userId As String, dealerCode As String, dealerdesc As String,
                                sortorder As String) As ReportCeBaseForm.Params

            ' Dim reportFormat As ReportCeBaseForm.RptFormat
            Dim params As New ReportCeBaseForm.Params
            Dim culturecode As String = TheRptCeInputControl.getCultureValue(False)
            Dim reportName As String = TheRptCeInputControl.getReportName(RPT_FILENAME, False)

            moReportFormat = ReportCeBase.GetReportFormat(Me)
            If (moReportFormat = ReportCeBase.RptFormat.TEXT_TAB) OrElse (moReportFormat = ReportCeBase.RptFormat.TEXT_CSV) Then
                reportName = TheRptCeInputControl.getReportName(RPT_FILENAME_EXPORT, True)
                culturecode = TheRptCeInputControl.getCultureValue(True)
            End If

            rptWindowTitle.InnerText = TheRptCeInputControl.getReportWindowTitle(TranslationBase.TranslateLabelOrMessage(RPT_FILENAME_WINDOW))


            Dim repParams() As ReportCeBaseForm.RptParam = New ReportCeBaseForm.RptParam() _
                                                {
                                                 New ReportCeBaseForm.RptParam("V_USER_KEY", userId),
                                                 New ReportCeBaseForm.RptParam("V_DEALER_CODE", dealerCode),
                                                 New ReportCeBaseForm.RptParam("V_DEALER_DESC", dealerdesc),
                                                 New ReportCeBaseForm.RptParam("V_SORT_ORDER", sortorder),
                                                 New ReportCeBaseForm.RptParam("LANG_CULTURE_VALUE", culturecode)}


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
            Dim selectedDealerId As Guid = DealerMultipleDrop.SelectedGuid
            Dim dealerCode As String = DealerMultipleDrop.SelectedCode
            Dim dealerdesc As String = DealerMultipleDrop.SelectedDesc
            Dim sortorder As String

            If rdealer.Checked Then
                dealerCode = ALL
            Else
                If selectedDealerId.Equals(Guid.Empty) Then
                    ElitaPlusPage.SetLabelError(DealerMultipleDrop.CaptionLabel)
                    Throw New GUIException(Message.MSG_INVALID_DEALER, Assurant.ElitaPlus.Common.ErrorCodes.GUI_DEALER_MUST_BE_SELECTED_ERR)
                End If
            End If

            Select Case rdReportSortOrder.SelectedValue()
                Case BY_ZIP_CODE
                    sortorder = "1"
                Case BY_BRANCH_CODE
                    sortorder = "2"
            End Select

            ReportCeBase.EnableReportCe(Me, TheRptCeInputControl)

            reportFormat = ReportCeBase.GetReportFormat(Me)

            Dim params As ReportCeBaseForm.Params = SetParameters(GuidControl.GuidToHexString(userId), dealerCode, dealerdesc, sortorder)
            Session(ReportCeBaseForm.SESSION_PARAMETERS_KEY) = params

        End Sub

#End Region


    End Class
End Namespace

