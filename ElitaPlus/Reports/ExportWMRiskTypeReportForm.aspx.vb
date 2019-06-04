Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common

Namespace Reports

    Partial Public Class ExportWMRiskTypeReportForm
        Inherits ElitaPlusPage
#Region "Constants"
        Private Const RPT_FILENAME_WINDOW As String = "WM_RISK_TYPE"
        Private Const RPT_FILENAME As String = "WMRiskType"
        Private Const RPT_FILENAME_EXPORT As String = "WMRiskType-Exp"

        Private Const LABEL_SELECT_DEALER As String = "DEALER"
        Private Const ONE_ITEM As Integer = 1
        Private Const LABEL_SELECT_COMPANY As String = "COMPANY"
#End Region

#Region "Properties"

        Public ReadOnly Property TheRptCeInputControl() As ReportExtractInputControl
            Get
                If moReportExtractInputControl Is Nothing Then
                    moReportExtractInputControl = CType(FindControl("moReportCeInputControl"), ReportExtractInputControl)
                End If
                Return moReportExtractInputControl
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
        Public ReadOnly Property UserCompanyMultipleDrop() As MultipleColumnDDLabelControl
            Get
                If moUserCompanyMultipleDrop Is Nothing Then
                    moUserCompanyMultipleDrop = CType(FindControl("moUserCompanyMultipleDrop"), MultipleColumnDDLabelControl)
                End If
                Return moUserCompanyMultipleDrop
            End Get
        End Property
#End Region


#Region "Page State"

        Class MyState
            Public MyBO As ReportRequests
            Public IsNew As Boolean = False
            Public ForEdit As Boolean = False
            Public HasDataChanged As Boolean
        End Class

        Public Sub New()
            MyBase.New(New MyState)
        End Sub

        Protected Shadows ReadOnly Property State() As MyState
            Get
                Return CType(MyBase.State, MyState)
            End Get
        End Property

#End Region

#Region "Handlers"

#Region " Web Form Designer Generated Code "
        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        End Sub
        Protected WithEvents moDealerMultipleDrop As MultipleColumnDDLabelControl
        'Protected WithEvents moReportCeInputControl As ReportCeInputControl
        'Protected WithEvents ErrorCtrl As ErrorController
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
            Me.ErrorCtrl.Clear_Hide()
            Try
                If Not Me.IsPostBack Then
                    InitializeForm()
                    'TheRptCeInputControl.populateReportLanguages(RPT_FILENAME_EXPORT)
                    'Date Calendars                    
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

#Region "Handlers-DropDown"

        Private Sub OnFromDrop_Changed(ByVal fromMultipleDrop As Assurant.ElitaPlus.ElitaPlusWebApp.Common.MultipleColumnDDLabelControl) _
            Handles moUserCompanyMultipleDrop.SelectedDropChanged
            Try
                PopulateDealerDropDown()
            Catch ex As Exception
                HandleErrors(ex, Me.ErrorCtrl)
            End Try
        End Sub

#End Region

#Region "Clear"

        Private Sub ClearErrLabels()
            Me.ClearLabelErrSign(DealerMultipleDrop.CaptionLabel)
            Me.ClearLabelErrSign(UserCompanyMultipleDrop.CaptionLabel)
            'If Me.rdealer.Checked Then DealerMultipleDrop.SelectedIndex = -1
        End Sub

#End Region

#Region "Populate"

        Private Sub PopulateCompaniesDropdown()
            Dim dv As DataView = LookupListNew.GetUserCompaniesLookupList()

            UserCompanyMultipleDrop.NothingSelected = True
            UserCompanyMultipleDrop.SetControl(True, UserCompanyMultipleDrop.MODES.NEW_MODE, True, dv, "* " + TranslationBase.TranslateLabelOrMessage(LABEL_SELECT_COMPANY), True)
            If dv.Count.Equals(ONE_ITEM) Then
                HideHtmlElement("ddSeparator")
                UserCompanyMultipleDrop.SelectedIndex = Me.ONE_ITEM
                UserCompanyMultipleDrop.Visible = False

            End If
        End Sub
        Sub PopulateDealerDropDown()
            Dim dv As DataView = LookupListNew.GetDealerLookupList(New Guid(UserCompanyMultipleDrop.SelectedGuid.ToString))
            DealerMultipleDrop.NothingSelected = True
            DealerMultipleDrop.SetControl(False, DealerMultipleDrop.MODES.NEW_MODE, True, dv, "* " + TranslationBase.TranslateLabelOrMessage(LABEL_SELECT_DEALER), True, True)
        End Sub

        Private Sub InitializeForm()
            PopulateCompaniesDropdown()
            PopulateDealerDropDown()
            'Dim t As Date = Date.Now.AddMonths(-1).AddDays(1)            
            Me.TheRptCeInputControl.SetExportOnly()
        End Sub

#End Region

#Region "Crystal Enterprise"

        Function SetParameters(ByVal selectedDealerId As String, ByVal langCode As String) As ReportCeBaseForm.Params

            Dim params As New ReportCeBaseForm.Params
            Dim moReportFormat As ReportCeBaseForm.RptFormat
            Dim reportName As String = TheRptCeInputControl.getReportName(RPT_FILENAME_EXPORT, True)
            Dim culturevalue As String = TheRptCeInputControl.getCultureValue(True, GuidControl.GuidToHexString((UserCompanyMultipleDrop.SelectedGuid)))
            Me.rptWindowTitle.InnerText = TheRptCeInputControl.getReportWindowTitle(TranslationBase.TranslateLabelOrMessage(RPT_FILENAME_WINDOW))

            moReportFormat = ReportCeBase.GetReportFormat(Me)

            Dim repParams() As ReportCeBaseForm.RptParam = New ReportCeBaseForm.RptParam() _
                                    {New ReportCeBaseForm.RptParam("V_DEALER_ID", selectedDealerId),
                                     New ReportCeBaseForm.RptParam("V_LANG_CODE", langCode),
                                     New ReportCeBaseForm.RptParam("LANG_CULTURE_VALUE", culturevalue)}

            With params
                .msRptName = reportName 'RPT_FILENAME_EXPORT
                .msRptWindowName = TranslationBase.TranslateLabelOrMessage(RPT_FILENAME_WINDOW)
                .moRptFormat = moReportFormat
                .moAction = ReportCeBaseForm.RptAction.SCHEDULE_VIEW
                .moRptParams = repParams
            End With
            Return params
        End Function

        Private Sub GenerateReport()
            Dim selectedDealerId As Guid = DealerMultipleDrop.SelectedGuid
            Dim dealerCode As String = DealerMultipleDrop.SelectedCode
            Dim isTotalsOnly As String
            Dim endDate As String
            Dim beginDate As String
            Dim CompanyCode As String = UserCompanyMultipleDrop.SelectedCode
            Dim CompanyDesc As String = UserCompanyMultipleDrop.SelectedDesc
            Dim companyid As String = GuidControl.GuidToHexString((UserCompanyMultipleDrop.SelectedGuid))
            Dim langId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
            Dim langCode As String = LookupListNew.GetCodeFromId(LookupListNew.LK_LANGUAGES, langId)
            Dim reportParams As New System.Text.StringBuilder

            'Validating the Company selection
            If UserCompanyMultipleDrop.SelectedGuid.Equals(Guid.Empty) Then
                ElitaPlusPage.SetLabelError(UserCompanyMultipleDrop.CaptionLabel)
                Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_COMPANY_IS_REQUIRED)
            End If

            If selectedDealerId.Equals(Guid.Empty) Then
                ElitaPlusPage.SetLabelError(DealerMultipleDrop.CaptionLabel)
                Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_DEALER_MUST_BE_SELECTED_ERR)
            End If

            reportParams.AppendFormat("pi_dealer_id => '{0}'", GuidControl.GuidToHexString(selectedDealerId))


            Me.State.MyBO = New ReportRequests
            Me.State.ForEdit = True
            Me.PopulateBOProperty(Me.State.MyBO, "ReportType", "WM_RISKTYPE")
            Me.PopulateBOProperty(Me.State.MyBO, "ReportProc", "R_WMRiskType.Oralce_Export")
            Me.PopulateBOProperty(Me.State.MyBO, "ReportParameters", ReportParams.ToString())
            Me.PopulateBOProperty(Me.State.MyBO, "UserEmailAddress", ElitaPlusIdentity.Current.EmailAddress)
            'ReportCeBase.EnableReportCe(Me, TheRptCeInputControl)

            'Dim params As ReportCeBaseForm.Params = SetParameters(GuidControl.GuidToHexString(selectedDealerId), langCode)
            'Session(ReportCeBaseForm.SESSION_PARAMETERS_KEY) = params
            ScheduleReport()
        End Sub


        Private Sub ScheduleReport()
            Try
                Dim scheduleDate As DateTime = TheRptCeInputControl.GetSchedDate()
                If Me.State.MyBO.IsDirty Then
                    Me.State.MyBO.Save()

                    Me.State.IsNew = False
                    Me.State.HasDataChanged = True
                    Me.State.MyBO.CreateJob(scheduleDate)

                    If String.IsNullOrEmpty(ElitaPlusIdentity.Current.EmailAddress) Then
                        Me.DisplayMessage(Message.MSG_Email_not_configured, "", Me.MSG_BTN_OK, Me.MSG_TYPE_ALERT, , True)
                    Else
                        Me.DisplayMessage(Message.MSG_REPORT_REQUEST_IS_GENERATED, "", Me.MSG_BTN_OK, Me.MSG_TYPE_ALERT, , True)
                    End If

                    btnGenRpt.Enabled = False

                Else
                End If

            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try
        End Sub

#End Region

    End Class
End Namespace