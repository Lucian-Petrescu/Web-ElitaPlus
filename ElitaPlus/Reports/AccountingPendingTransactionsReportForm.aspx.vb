﻿Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Imports System.Threading
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms

Namespace Reports
    Public Class AccountingPendingTransactionsReportForm
        Inherits ElitaPlusPage
#Region "Constants"

        Private Const RPT_FILENAME_WINDOW As String = "ACCOUNTING_PENDING_TRANSACTIONS"
        Private Const RPT_FILENAME As String = "AccountingPendingTransactions"
        Private Const RPT_FILENAME_EXPORT As String = "AccountingPendingTransactions-Exp"
        Private Const TOTAL_PARAMS As Integer = 11 ' 12 Elements
        Private Const TOTAL_EXP_PARAMS As Integer = 5 ' 4 Elements
        Private Const PARAMS_PER_REPORT As Integer = 5 ' 4 Elements
        Private Const ALL As String = "*"
        Private Const SP_DATE_FORMAT As String = "yyyyMMdd"
        Private Const YES As String = "D"
        Private Const NO As String = "S"
        Private Const ONE_ITEM As Integer = 1
        Private Const LABEL_SELECT_COMPANY As String = "SELECT_COMPANY"

        Public Const PAGETITLE As String = "ACCOUNTING_PENDING_TRANSACTIONS"
        Public Const PAGETAB As String = "REPORTS"
#End Region

#Region "Properties"

        Public ReadOnly Property UserCompanyMultipleDrop() As MultipleColumnDDLabelControl
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
        Private reportName As String = RPT_FILENAME
        'Dim moReportFormat As ReportCeBaseForm.RptFormat
#End Region

#Region "Handlers"

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub

        Protected WithEvents rdReportFormat_NO_TRANSLATE As System.Web.UI.WebControls.RadioButtonList
        Protected WithEvents isReportCeVisible As System.Web.UI.HtmlControls.HtmlInputHidden
        Protected WithEvents isProgressVisible As System.Web.UI.HtmlControls.HtmlInputHidden
        Protected WithEvents moReportCeStatus As System.Web.UI.HtmlControls.HtmlInputHidden
        Protected WithEvents moExportDropDownList As System.Web.UI.WebControls.DropDownList
        Protected WithEvents rdReportType As System.Web.UI.WebControls.RadioButtonList
        Protected WithEvents moDealerLabel As System.Web.UI.WebControls.Label
        Protected WithEvents Radiobutton1 As System.Web.UI.WebControls.RadioButton
        Protected WithEvents btnViewHidden As System.Web.UI.WebControls.Button
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
            Me.ErrControllerMaster.Clear_Hide()
            Me.ClearLabelsErrSign()
            Try
                If Not Me.IsPostBack Then
                    Me.SetFormTitle(PAGETITLE)
                    Me.SetFormTab(PAGETAB)
                    InitializeForm()

                    Me.cboEventType.Attributes.Add("onchange", String.Format("ToggleSingleDropDownSelection('{0}','{1}',true);", cboEventType.ClientID, Me.rEventType.ClientID))
                    Me.rEventType.Attributes.Add("onclick", String.Format("ToggleSingleDropDownSelection('{0}','{1}',false);", cboEventType.ClientID, Me.rEventType.ClientID))

                End If
                Me.InstallProgressBar()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
            Me.ShowMissingTranslations(Me.ErrControllerMaster)

        End Sub

        Public Sub ClearLabelsErrSign()
            Try
                Me.ClearLabelErrSign(UserCompanyMultipleDrop.CaptionLabel)
                Me.ClearLabelErrSign(EventTypeLabel)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub
#End Region


#Region "Handlers-Buttons"

        Private Sub btnGenRpt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenRpt.Click
            Try
                GenerateReport()
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

#End Region

#Region "Handlers-DropDown"

        Private Sub OnFromDrop_Changed(ByVal fromMultipleDrop As Assurant.ElitaPlus.ElitaPlusWebApp.Common.MultipleColumnDDLabelControl) _
             Handles multipleDropControl.SelectedDropChanged
            Try
                PopulateEventsDropDown()
            Catch ex As Exception
                HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

#End Region

#End Region

#Region "Populate"

        Private Sub PopulateEventsDropDown()
            'If Not UserCompanyMultipleDrop.SelectedGuid.Equals(Guid.Empty) Then
            '    Dim oCompany As New Company(UserCompanyMultipleDrop.SelectedGuid)
            '    Me.BindListControlToDataView(Me.cboEventType, LookupListNew.getAccountingEvents(oCompany.AcctCompanyId, ElitaPlusIdentity.Current.ActiveUser.LanguageId), , , True)
            'Else
            '    Me.BindListControlToDataView(Me.cboEventType, LookupListNew.getAccountingEvents(UserCompanyMultipleDrop.SelectedGuid, ElitaPlusIdentity.Current.ActiveUser.LanguageId), , , True)
            'End If

            Dim AccountingEvents As DataElements.ListItem() =
                CommonConfigManager.Current.ListManager.GetList(listCode:="AccountingEventByAccountingCompany",
                                                                languageCode:=Thread.CurrentPrincipal.GetLanguageCode(),
                                                                context:=New ListContext() With
                                                                {
                                                                  .AccountingCompanyId =
                                                                        If(Not UserCompanyMultipleDrop.SelectedGuid.Equals(Guid.Empty),
                                                                             New Company(UserCompanyMultipleDrop.SelectedGuid).AcctCompanyId,
                                                                             UserCompanyMultipleDrop.SelectedGuid)
                                                                })

            Me.cboEventType.Populate(AccountingEvents.ToArray(),
                                     New PopulateOptions() With
                                     {
                                      .AddBlankItem = True
                                     })
        End Sub

        Private Sub PopulateCompaniesDropdown()
            Dim dv As DataView = LookupListNew.GetUserCompaniesLookupList()
            UserCompanyMultipleDrop.NothingSelected = True
            UserCompanyMultipleDrop.SetControl(True, UserCompanyMultipleDrop.MODES.NEW_MODE, True, dv, "* " & TranslationBase.TranslateLabelOrMessage(LABEL_SELECT_COMPANY), True)
            If dv.Count.Equals(ONE_ITEM) Then
                HideHtmlElement("ddSeparator1")
                UserCompanyMultipleDrop.SelectedIndex = Me.ONE_ITEM
                UserCompanyMultipleDrop.Visible = False
            End If
        End Sub

        Private Sub InitializeForm()
            PopulateCompaniesDropdown()
            PopulateEventsDropDown()
            Me.rEventType.Checked = True
            TheReportCeInputControl.populateReportLanguages(RPT_FILENAME)
        End Sub

#End Region

#Region "Crystal Enterprise"

        Private Sub GenerateReport()
            Dim compDesc As String = UserCompanyMultipleDrop.SelectedDesc
            Dim compId As Guid = UserCompanyMultipleDrop.SelectedGuid
            Dim selectedEventType As String = Me.cboEventType.SelectedItem.ToString

            Dim langCode As String = LookupListNew.GetCodeFromId("LANGUAGES", ElitaPlusIdentity.Current.ActiveUser.LanguageId)

            ' Dim EventType As String
            Dim params As ReportCeBaseForm.Params

            'Validating the Company selection
            If UserCompanyMultipleDrop.SelectedGuid.Equals(Guid.Empty) Then
                ElitaPlusPage.SetLabelError(UserCompanyMultipleDrop.CaptionLabel)
                Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_COMPANY_IS_REQUIRED)
            End If

            If Me.rEventType.Checked Then
                selectedEventType = ALL
            Else
                If selectedEventType.Equals(String.Empty) Then
                    ElitaPlusPage.SetLabelError(Me.EventTypeLabel)
                    Throw New GUIException(Message.MSG_INVALID_DEALER, Assurant.ElitaPlus.Common.ErrorCodes.GUI_EVENT_TYPE_MUST_BE_SELECTED_ERR)
                End If
            End If

            ReportCeBase.EnableReportCe(Me, TheReportCeInputControl)
            reportFormat = ReportCeBase.GetReportFormat(Me)
            params = SetParameters(GuidControl.GuidToHexString(compId), compDesc, selectedEventType, langCode)
            Session(ReportCeBaseForm.SESSION_PARAMETERS_KEY) = params
        End Sub

        Function SetParameters(ByVal companyId As String, ByVal companyDesc As String, ByVal EventType As String, ByVal langCode As String) As ReportCeBaseForm.Params


            Dim params As New ReportCeBaseForm.Params
            Dim culturecode As String = TheReportCeInputControl.getCultureValue(False)
            Dim reportName As String = TheReportCeInputControl.getReportName(RPT_FILENAME, False)

            Dim exportData As String = NO

            reportFormat = ReportCeBase.GetReportFormat(Me)
            If (reportFormat = ReportCeBase.RptFormat.TEXT_TAB) OrElse (reportFormat = ReportCeBase.RptFormat.TEXT_CSV) Then
                reportName = TheReportCeInputControl.getReportName(RPT_FILENAME_EXPORT, True)
                culturecode = TheReportCeInputControl.getCultureValue(True, companyId)
            End If

            Dim repParams() As ReportCeBaseForm.RptParam = New ReportCeBaseForm.RptParam() _
                    { _
                     New ReportCeBaseForm.RptParam("V_COMPANY_ID", companyId, ), _
                     New ReportCeBaseForm.RptParam("V_COMPANY_DESC", companyDesc), _
                     New ReportCeBaseForm.RptParam("V_EVENT_TYPE", EventType), _
                     New ReportCeBaseForm.RptParam("V_LANG_CODE", langCode), _
                     New ReportCeBaseForm.RptParam("LANG_CULTURE_VALUE", culturecode)}

            With params
                .msRptName = reportName
                .msRptWindowName = TranslationBase.TranslateLabelOrMessage(RPT_FILENAME_WINDOW)
                .moRptFormat = reportFormat
                .moAction = ReportCeBaseForm.RptAction.SCHEDULE_VIEW
                .moRptParams = repParams
            End With
            Return params
        End Function

#End Region

    End Class

End Namespace