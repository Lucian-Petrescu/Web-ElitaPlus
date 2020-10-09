Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Imports System.Threading
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms
Namespace Reports

    Partial Class ExportIBNREnglishUSAReportForm_Old
        Inherits ElitaPlusPage

#Region "Parameters"

        Public Structure ReportParams
            Public companyCode As String
            Public beginDate As String
            Public endDate As String

        End Structure

#End Region

#Region "Constants"

        '   Private Const RPT_FILENAME As String = "Claims Following Service Warranty English (USA)_TEST7"
        Private ReadOnly RPT_FILENAME_WINDOW As String = "IBNR_PAID_" & Date.Today.ToString(SP_DATE_FORMAT)
        Private Const RPT_FILENAME As String = "ExportIBNRPaidEnglishUSA_Exp"
        Private Const RPT_FILENAME_EXPORT As String = "ExportIBNRPaidEnglishUSA_Exp"

        Private ReadOnly RPT_PENDING_FILENAME_WINDOW As String = "IBNR_PENDING_" & Date.Today.ToString(SP_DATE_FORMAT)
        Private Const RPT_PENDING_FILENAME As String = "ExportIBNRPendingEnglishUSA_Exp"
        Private Const RPT_PENDING_FILENAME_EXPORT As String = "ExportIBNRPendingEnglishUSA_Exp"

        Private Const ONE_ITEM As Integer = 1

        Public Const ALL As String = "*"
        Public Const NOTHING_SELECTED As Int16 = -1
        Public Const SP_DATE_FORMAT As String = "MMddyy"
        Public Const YES As String = "Y"
        Public Const NO As String = "N"

        Public Const EXPORT_IBNR_PAID As String = "0"
        Public Const EXPORT_IBNR_PENDING As String = "1"

        Protected WithEvents isProgressVisible As System.Web.UI.HtmlControls.HtmlInputHidden
        Protected WithEvents isReportCeVisible As System.Web.UI.HtmlControls.HtmlInputHidden
        Protected WithEvents moReportCeStatus As System.Web.UI.HtmlControls.HtmlInputHidden
        Protected WithEvents moDaysActiveLabel As System.Web.UI.WebControls.Label
        Protected WithEvents moDealerLabel As System.Web.UI.WebControls.Label

        Private Const LABEL_SELECT_COMPANY As String = "SELECT_COMPANY"
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
        Public ReadOnly Property UserCompanyMultipleDrop() As MultipleColumnDDLabelControl
            Get
                If moUserCompanyMultipleDrop Is Nothing Then
                    moUserCompanyMultipleDrop = CType(FindControl("multipleDropControl"), MultipleColumnDDLabelControl)
                End If
                Return moUserCompanyMultipleDrop
            End Get
        End Property
#End Region


#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub


        Protected WithEvents ErrorCtrl As ErrorController
        Protected WithEvents moReportCeInputControl As ReportCeInputControl
        Protected WithEvents moUserCompanyMultipleDrop As MultipleColumnDDLabelControl

        'NOTE: The following placeholder declaration is required by the Web Form Designer.
        'Do not delete or move it.
        Private designerPlaceholderDeclaration As System.Object

        Private Sub Page_Init(sender As System.Object, e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

        Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            ErrorCtrl.Clear_Hide()
            Try
                If Not IsPostBack Then
                    InitializeForm()
                    TheRptCeInputControl.SetExportOnly()
                End If
                'Me.DisplayProgressBarOnClick(Me.btnGenRpt, "LOADING_REPORT")
                InstallProgressBar()
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try
            ShowMissingTranslations(ErrorCtrl)
        End Sub

        Private Sub PopulateAccountingDates()
            'Dim dv As DataView = AccountingCloseInfo.GetAllAccountingCloseDates(ElitaPlusIdentity.Current.ActiveUser.CompanyId)
            'Dim dv As DataView = IbnrLossPaid.GetIBNRLossPaidAccountingDate(ElitaPlusIdentity.Current.ActiveUser.CompanyId)
            'Me.BindListTextToDataView(Me.moAccountingDateDropDownList, dv, , , False)

            Dim AccountingCloseDates As DataElements.ListItem() =
                CommonConfigManager.Current.ListManager.GetList(listCode:="AccountingCloseDateByCompany",
                                                                context:=New ListContext() With
                                                                {
                                                                  .CompanyId = ElitaPlusIdentity.Current.ActiveUser.CompanyId
                                                                })

            moAccountingDateDropDownList.Populate(AccountingCloseDates.ToArray(), New PopulateOptions())

        End Sub

        Private Sub PopulateCompaniesDropdown()
            Dim dv As DataView = LookupListNew.GetUserCompaniesLookupList()
            UserCompanyMultipleDrop.NothingSelected = True
            UserCompanyMultipleDrop.SetControl(False, UserCompanyMultipleDrop.MODES.NEW_MODE, True, dv, TranslationBase.TranslateLabelOrMessage(LABEL_SELECT_COMPANY), True)
            If dv.Count.Equals(ONE_ITEM) Then
                HideHtmlElement("ddSeparator")
                UserCompanyMultipleDrop.SelectedIndex = ONE_ITEM
                UserCompanyMultipleDrop.Visible = False
            End If
        End Sub

        Private Sub InitializeForm()
            PopulateAccountingDates()
            PopulateCompaniesDropdown()
        End Sub

        Private Sub btnGenRpt_Click(sender As System.Object, e As System.EventArgs) Handles btnGenRpt.Click
            Try

                If moAccountingDateDropDownList.SelectedIndex = -1 Then
                    ElitaPlusPage.SetLabelError(moLabel)
                    Throw New GUIException(Message.MSG_GUI_ACCOUNTING_DATE_NOT_SELECTED, Assurant.ElitaPlus.Common.ErrorCodes.GUI_ACCOUNTING_DATE_NOT_SELECTED)
                End If

                Dim compCode As String = UserCompanyMultipleDrop.SelectedCode
                Dim selectedAcctDateDesc As String = GetSelectedDescription(moAccountingDateDropDownList)
                Dim dv As DataView = AccountingCloseInfo.GetAllAccountingCloseDates(ElitaPlusIdentity.Current.ActiveUser.CompanyId)
                'Dim dv As DataView = IbnrLossPaid.GetIBNRLossPaidAccountingDate(ElitaPlusIdentity.Current.ActiveUser.CompanyId)
                Dim selctedAcctDateCode As String = LookupListNew.GetCodeFromDescription(dv, selectedAcctDateDesc)

                ReportCeBase.EnableReportCe(Me, TheRptCeInputControl)

                Dim params As ReportCeBaseForm.Params = SetParameters(compCode, selctedAcctDateCode)
                Session(ReportCeBaseForm.SESSION_PARAMETERS_KEY) = params

            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try
        End Sub

        Function SetParameters(companyCode As String, selctedAcctDateCode As String) As ReportCeBaseForm.Params

            Dim params As New ReportCeBaseForm.Params
            Dim reportName As String
            Dim rptFilenameWindow As String

            moReportFormat = ReportCeBase.GetReportFormat(Me)
            If (moReportFormat = ReportCeBase.RptFormat.TEXT_TAB) OrElse (moReportFormat = ReportCeBase.RptFormat.TEXT_CSV) Then
                Select Case rdExportSelectiion.SelectedValue()
                    Case EXPORT_IBNR_PAID
                        reportName = RPT_FILENAME
                        rptFilenameWindow = RPT_FILENAME_WINDOW
                    Case EXPORT_IBNR_PENDING
                        reportName = RPT_PENDING_FILENAME
                        rptFilenameWindow = RPT_PENDING_FILENAME_WINDOW
                End Select
            End If

            Dim repParams() As ReportCeBaseForm.RptParam = New ReportCeBaseForm.RptParam() _
                    { _
                     New ReportCeBaseForm.RptParam("V_COMPANY", companyCode), _
                     New ReportCeBaseForm.RptParam("V_CLOSING_DATE", selctedAcctDateCode)}

            With params
                .msRptName = reportName
                .msRptWindowName = rptFilenameWindow
                .moRptFormat = moReportFormat
                .moAction = ReportCeBaseForm.RptAction.SCHEDULE_VIEW
                .moRptParams = repParams
            End With
            Return params
        End Function

    End Class
End Namespace
