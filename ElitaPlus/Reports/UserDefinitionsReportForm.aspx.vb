Imports System.Threading
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms

Namespace Reports

    Partial Class UserDefinitionsReportForm
        Inherits ElitaPlusPage


#Region "Parameters"

        Public Structure ReportParams
            Public companyGroupCode As String
            Public companyGroupDescription As String
            Public roleCode As String
            Public sortOrder As String
            Public external As String
            Public active As String
        End Structure

#End Region

#Region "Constants"

        Private Const RPT_FILENAME_WINDOW As String = "USER DEFINITIONS"
        Private Const RPT_FILENAME As String = "UserDefinitions"
        Private Const RPT_FILENAME_EXPORT As String = "UserDefinitions_Exp"

        Public Const CRYSTAL As String = "0"
        Public Const HTML As String = "2"
        Public Const CSV As String = "3"

        Public Const VIEW_REPORT As String = "1"
        Public Const PDF As String = "2"
        Public Const TAB As String = "3"
        Public Const EXCEL As String = "4"
        Public Const EXCEL_DATA_ONLY As String = "5"
        Public Const JAVA As String = "6"

        Public Const ALL As String = "*"
        Public Const NOTHING_SELECTED As Int16 = -1

        Private Const TOTALPARAMS As Integer = 5
        Private Const TOTALEXPPARAMS As Integer = 5
        Private Const PARAMS_PER_REPORT As Integer = 5

        Protected WithEvents isProgressVisible As System.Web.UI.HtmlControls.HtmlInputHidden
        Protected WithEvents isReportCeVisible As System.Web.UI.HtmlControls.HtmlInputHidden
        Protected WithEvents moReportCeStatus As System.Web.UI.HtmlControls.HtmlInputHidden
        Public Const PAGETITLE As String = "USER DEFINITIONS"
        Public Const PAGETAB As String = "REPORTS"
        Public Const DOCTITLE As String = "USER DEFINITIONS"

#End Region

#Region "variables"
        Private moReportFormat As ReportCeBaseForm.RptFormat
#End Region

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

        Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            ErrControllerMaster.Clear_Hide()
            Title = TranslationBase.TranslateLabelOrMessage(RPT_FILENAME_WINDOW)
            Try
                If Not IsPostBack Then
                    SetFormTitle(PAGETITLE)
                    SetFormTab(PAGETAB)
                    'JavascriptCalls()
                    InitializeForm()
                End If
                InstallProgressBar()
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
            ShowMissingTranslations(ErrControllerMaster)
        End Sub

        Sub PopulateRoleDropDown()
            Dim roleLkl As Assurant.Elita.CommonConfiguration.DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList("GetRoleList")
            cboRoles.Populate(roleLkl, New PopulateOptions() With
                {
                    .AddBlankItem = True
                })
            'Me.BindListControlToDataView(Me.cboRoles, LookupListNew.GetRolesLookupList, , , True)
        End Sub

        Private Sub InitializeForm()

            PopulateRoleDropDown()
            rrole.Checked = True
            rIntern.Checked = True
            rActive.Checked = True
            rdReportSortOrder.Items(0).Selected = True

        End Sub

        Private Sub btnGenRpt_Click(sender As System.Object, e As System.EventArgs) Handles btnGenRpt.Click
            Try

                Dim sortOrder As String
                Dim roleCode As String
                Dim companyGroupCode As String = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Code
                Dim companyGroupName As String = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Description
                Dim params As ReportCeBaseForm.Params
                Dim external As String
                Dim active As String

                moReportFormat = ReportCeBase.GetReportFormat(Me)
                If (moReportFormat <> ReportCeBase.RptFormat.PDF) AndAlso (moReportFormat <> ReportCeBase.RptFormat.JAVA) AndAlso (rAll.Checked) Then
                    Throw New GUIException(Message.MSG_GUI_INVALID_SELECTION, Assurant.ElitaPlus.Common.ErrorCodes.GUI_INVALID_SELECTION)
                End If

                If rrole.Checked Then
                    roleCode = ALL
                Else
                    If GetSelectedItem(cboRoles).Equals(Guid.Empty) Then
                        Throw New GUIException(Message.MSG_GUI_INVALID_SELECTION, Assurant.ElitaPlus.Common.ErrorCodes.GUI_INVALID_SELECTION)
                    End If
                    Dim dvRoles As DataView = LookupListNew.GetRolesLookupList
                    roleCode = LookupListNew.GetCodeFromId(dvRoles, GetSelectedItem(cboRoles))
                End If

                If rIntern.Checked = True Then
                    external = "N"
                ElseIf rExtern.Checked = True Then
                    external = "Y"
                Else
                    external = "*"
                End If

                If rInActive.Checked = True Then
                    active = "N"
                ElseIf rActive.Checked = True Then
                    active = "Y"
                Else
                    active = "*"
                End If


                ReportCeBase.EnableReportCe(Me, TheReportCeInputControl)

                sortOrder = rdReportSortOrder.SelectedValue()

                moReportFormat = ReportCeBase.GetReportFormat(Me)
                If (moReportFormat = ReportCeBase.RptFormat.TEXT_TAB) OrElse (moReportFormat = ReportCeBase.RptFormat.TEXT_CSV) Then
                    'Export Report
                    params = SetExpParameters(roleCode, companyGroupCode, companyGroupName, sortOrder, external, active)
                Else
                    'View Report
                    params = SetParameters(roleCode, companyGroupCode, companyGroupName, sortOrder, external, active)
                End If
                Session(ReportCeBaseForm.SESSION_PARAMETERS_KEY) = params

            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

        Sub SetReportParams(oReportParams As ReportParams, repParams() As ReportCeBaseForm.RptParam,
                            reportName As String, startIndex As Integer)
            With oReportParams
                repParams(startIndex) = New ReportCeBaseForm.RptParam("V_COMPANY_GROUP", .companyGroupCode, reportName)
                repParams(startIndex + 1) = New ReportCeBaseForm.RptParam("V_ROLE", .roleCode, reportName)
                repParams(startIndex + 2) = New ReportCeBaseForm.RptParam("V_SORT", .sortOrder, reportName)
                repParams(startIndex + 3) = New ReportCeBaseForm.RptParam("V_COMPANY_GROUP_NAME", .companyGroupDescription, reportName)
                repParams(startIndex + 4) = New ReportCeBaseForm.RptParam("V_EXTERNAL", .external, reportName)
                repParams(startIndex + 5) = New ReportCeBaseForm.RptParam("V_ACTIVE", .active, reportName)
            End With

        End Sub

        Function SetParameters(roleCode As String,
                               companyGroupCode As String,
                               companyGroupDescription As String,
                               sortOrder As String,
                               external As String,
                               active As String) As ReportCeBaseForm.Params

            Dim reportFormat As ReportCeBaseForm.RptFormat
            Dim reportName As String = RPT_FILENAME
            Dim params As New ReportCeBaseForm.Params
            Dim repParams(TOTALPARAMS) As ReportCeBaseForm.RptParam
            Dim oReportParams As ReportParams

            With oReportParams
                .companyGroupCode = companyGroupCode
                .companyGroupDescription = companyGroupDescription
                .roleCode = roleCode
                .sortOrder = sortOrder
                .external = external
                .active = active
            End With
            SetReportParams(oReportParams, repParams, String.Empty, PARAMS_PER_REPORT * 0)     ' Main Report

            With params
                .msRptName = reportName
                .msRptWindowName = TranslationBase.TranslateLabelOrMessage(RPT_FILENAME_WINDOW)
                .moRptFormat = moReportFormat
                .moAction = ReportCeBaseForm.RptAction.SCHEDULE_VIEW
                .moRptParams = repParams
            End With
            Return params
        End Function

        Function SetExpParameters(roleCode As String,
                               companyGroupCode As String,
                               companyGroupDescription As String,
                               sortOrder As String,
                                external As String,
                                active As String) As ReportCeBaseForm.Params

            Dim reportFormat As ReportCeBaseForm.RptFormat
            Dim reportName As String = RPT_FILENAME_EXPORT
            Dim params As New ReportCeBaseForm.Params
            Dim repParams(TOTALEXPPARAMS) As ReportCeBaseForm.RptParam
            Dim oReportParams As ReportParams

            With oReportParams
                .companyGroupCode = companyGroupCode
                .companyGroupDescription = companyGroupDescription
                .roleCode = roleCode
                .sortOrder = sortOrder
                .external = external
                .active = active
            End With
            SetReportParams(oReportParams, repParams, String.Empty, PARAMS_PER_REPORT * 0)     ' Main Report

            With params
                .msRptName = reportName
                .msRptWindowName = TranslationBase.TranslateLabelOrMessage(RPT_FILENAME_WINDOW)
                .moRptFormat = moReportFormat
                .moAction = ReportCeBaseForm.RptAction.SCHEDULE_VIEW
                .moRptParams = repParams
            End With

            Return params
        End Function

    End Class

End Namespace
