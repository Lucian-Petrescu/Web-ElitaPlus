Imports System.Threading
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms

Namespace Reports
    Partial Class RolePermissionsReportForm
        Inherits ElitaPlusPage
#Region "Constants"
        Private Const RPT_FILENAME_WINDOW As String = "ROLE PERMISSIONS"
        Private Const RPT_FILENAME As String = "RolePermissionsEnglishUSA"
        Private Const RPT_FILENAME_EXPORT As String = "RolePermissionsEnglishUSA_Exp"

        Public Const ALL As String = "*"
        Public Const YES As String = "Y"
        Public Const NO As String = "N"

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
        Protected WithEvents btnViewHidden As System.Web.UI.WebControls.Button
        Protected WithEvents moReportCeInputControl As ReportCeInputControl
        Protected WithEvents ErrorCtrl As ErrorController

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

#Region "Clear"

        Private Sub ClearErrLabels()
            Me.ClearLabelErrSign(moRoleLabel)
            Me.ClearLabelErrSign(moTabLabel)
        End Sub

#End Region

#Region "Populate"

        Sub PopulateDropDowns()
            Dim roleLkl As Assurant.Elita.CommonConfiguration.DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList("GetRoleList")
            Me.cboRole.Populate(roleLkl, New PopulateOptions() With
                {
                    .AddBlankItem = True,
                    .BlankItemValue = "0",
                    .ValueFunc = AddressOf .GetCode,
                    .SortFunc = AddressOf .GetCode
                })

            'Me.BindListTextToDataView(Me.cboRole, LookupListNew.GetRolesLookupList(), , "CODE", True)
            Dim oListContext As New Assurant.Elita.CommonConfiguration.ListContext
            oListContext.LanguageId = Thread.CurrentPrincipal.GetLanguageId()
            Me.cboTab.Populate(CommonConfigManager.Current.ListManager.GetList(listCode:="GetTabList", context:=oListContext, languageCode:=Thread.CurrentPrincipal.GetLanguageCode()).ToArray(), New PopulateOptions() With
                                                   {
                                                    .AddBlankItem = True,
                                                    .SortFunc = AddressOf .GetDescription
                                                   })
            'Me.BindListControlToDataView(Me.cboTab, LookupListNew.GetTabsLookupList(), , , True)
        End Sub

        Private Sub InitializeForm()
            PopulateDropDowns()
            Me.rrole.Checked = True
            Me.rtabs.Checked = True
        End Sub

#End Region

#Region "Crystal Enterprise"

        Function SetParameters(ByVal RoleId As String, ByVal TabId As String) As ReportCeBaseForm.Params

            Dim params As New ReportCeBaseForm.Params
            Dim reportName As String = RPT_FILENAME
            Dim moReportFormat As ReportCeBaseForm.RptFormat
            Dim langCode As String = LookupListNew.GetCodeFromId("LANGUAGES", Authentication.LangId)
            Dim exportData As String = NO

            moReportFormat = ReportCeBase.GetReportFormat(Me)
            If (moReportFormat = ReportCeBase.RptFormat.TEXT_TAB) OrElse (moReportFormat = ReportCeBase.RptFormat.TEXT_CSV) Then
                exportData = YES
                reportName = RPT_FILENAME_EXPORT
            End If

            Dim repParams() As ReportCeBaseForm.RptParam = New ReportCeBaseForm.RptParam() _
                    {
                     New ReportCeBaseForm.RptParam("V_LANG_ID", langCode),
                     New ReportCeBaseForm.RptParam("V_ROLESIDS", RoleId),
                     New ReportCeBaseForm.RptParam("V_TABSIDS", TabId)}

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
            Dim selectedRoleId As String = Me.GetSelectedValue(Me.cboRole)
            Dim selectedRoleDesc As String = Me.GetSelectedDescription(Me.cboRole)
            Dim selectedTabId As String = Me.GetSelectedDescription(Me.cboTab)
            Dim RoleId As String, TabId As String

            If Me.rrole.Checked Then
                RoleId = ALL
            Else
                RoleId = selectedRoleId
                If selectedRoleDesc.Equals(String.Empty) Then
                    ElitaPlusPage.SetLabelError(moRoleLabel)
                    Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_ROLE_MUST_BE_SELECTED_ERR)
                End If
            End If

            If Me.rtabs.Checked Then
                TabId = ALL
            Else
                TabId = selectedTabId
                If selectedTabId.Equals(String.Empty) Then
                    ElitaPlusPage.SetLabelError(moTabLabel)
                    Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_TAB_MUST_BE_SELECTED_ERR)
                End If
            End If

            ReportCeBase.EnableReportCe(Me, TheRptCeInputControl)

            Dim params As ReportCeBaseForm.Params = SetParameters(RoleId, TabId)
            Session(ReportCeBaseForm.SESSION_PARAMETERS_KEY) = params
        End Sub


#End Region


    End Class
End Namespace