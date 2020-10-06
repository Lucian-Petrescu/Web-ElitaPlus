Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Imports Assurant.ElitaPlus.BusinessObjects.Common
Namespace Reports

    Partial Class TIMMCertificateExtractForm
        Inherits ElitaPlusPage


#Region "Parameters"

        Public Structure ReportParams
            Public companyCode As String
            Public beginDate As String
            Public endDate As String

        End Structure

#End Region

#Region "Constants"

        Public Const ALL As String = "*"
        Public Const NOTHING_SELECTED As Int16 = -1
        Public Const SP_DATE_FORMAT As String = "yyyyMMdd"
        Public Const YES As String = "Y"
        Public Const NO As String = "N"

        Public Const PAGETITLE As String = "TIMM_Certificate_Extract_Report"
        Public Const PAGETAB As String = "REPORTS"
        Public Const DOCTITLE As String = "TIMM_Certificate_Extract_Report"


#End Region

#Region "variables"
        'Dim moReportFormat As ReportCeBaseForm.RptFormat
        Private dtLatestAccountingCloseDate As Date
#End Region

#Region "Properties"


#End Region

#Region "Handlers-DropDown"



#End Region
#Region "Page State"

        Class MyState
            Public MyBO As ReportRequests
            Public IsNew As Boolean = False
            Public IsACopy As Boolean

            Public ActionInProgress As DetailPageCommand = DetailPageCommand.Nothing_
            Public LastErrMsg As String
            Public HasDataChanged As Boolean
            Public ForEdit As Boolean = False
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

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub
        Protected WithEvents Label1 As System.Web.UI.WebControls.Label

        Protected WithEvents isProgressVisible As System.Web.UI.HtmlControls.HtmlInputHidden
        Protected WithEvents isReportCeVisible As System.Web.UI.HtmlControls.HtmlInputHidden
        Protected WithEvents moReportCeStatus As System.Web.UI.HtmlControls.HtmlInputHidden
        Protected WithEvents moDaysActiveLabel As System.Web.UI.WebControls.Label

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
            MasterPage.MessageController.Clear_Hide()
            ClearLabelsErrSign()

            Try
                If Not IsPostBack Then

                    TheReportExtractInputControl.ViewVisible = False
                    TheReportExtractInputControl.PdfVisible = False
                    TheReportExtractInputControl.ExportDataVisible = False
                    TheReportExtractInputControl.DestinationVisible = False
                    MasterPage.UsePageTabTitleInBreadCrum = False
                    SetFormTab(PAGETAB)
                    UpdateBreadCrum()
                    AddCalendar(btnBeginDate, txtBeginDate)
                    AddCalendar(btnEndDate, txtEndDate)
                End If

                InstallDisplayNewReportProgressBar()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
            ShowMissingTranslations(MasterPage.MessageController)
        End Sub
        Public Sub ClearLabelsErrSign()
            Try
                ClearLabelErrSign(lblEndDate)
                ClearLabelErrSign(lblBeginDate)
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub UpdateBreadCrum()
            MasterPage.BreadCrum = MasterPage.PageTab & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage(PAGETITLE)
            MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage(PAGETITLE)
        End Sub

        Private Sub btnGenRpt_Click(sender As System.Object, e As System.EventArgs) Handles btnGenRpt.Click
            Try
                GenerateReport()
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub GenerateReport()
            Dim reportParams As New System.Text.StringBuilder
            Dim endDate As String
            Dim beginDate As String
            Dim SelectedExtractType As String
            Dim userId As String = GuidControl.GuidToHexString(ElitaPlusIdentity.Current.ActiveUser.Id)
            Dim companygrpId As String = GuidControl.GuidToHexString(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id)

            If txtBeginDate.Text.Equals(String.Empty) AndAlso txtEndDate.Text.Equals(String.Empty) Then
                ElitaPlusPage.SetLabelError(lblBeginDate)
                ElitaPlusPage.SetLabelError(lblEndDate)
                Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_INVALID_SELECTION)
            ElseIf ((Not txtBeginDate.Text.Equals(String.Empty) And txtEndDate.Text.Equals(String.Empty)) Or (txtBeginDate.Text.Equals(String.Empty) And Not txtEndDate.Text.Equals(String.Empty))) Then
                ElitaPlusPage.SetLabelError(lblBeginDate)
                ElitaPlusPage.SetLabelError(lblEndDate)
                Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_BEGIN_AND_END_DATES_MUST_BE_SELECTED_ERR)
            ElseIf Not txtBeginDate.Text.Equals(String.Empty) And Not txtEndDate.Text.Equals(String.Empty) Then
                ReportExtractBase.ValidateBeginEndDate(lblBeginDate, txtBeginDate.Text, lblEndDate, txtEndDate.Text)
            End If

            SelectedExtractType = moExtractTypeList.SelectedValue
            endDate = ReportExtractBase.FormatDate(lblEndDate, txtEndDate.Text)
            beginDate = ReportExtractBase.FormatDate(lblBeginDate, txtBeginDate.Text)

            reportParams.AppendFormat("pi_User_Id=> '{0}',", userId)
            reportParams.AppendFormat("pi_Company_Group_Id => '{0}',", companygrpId)
            reportParams.AppendFormat("pi_Extract_Type => '{0}',", SelectedExtractType)
            reportParams.AppendFormat("pi_Start_Date => '{0}',", beginDate)
            reportParams.AppendFormat("pi_End_Date => '{0}'", endDate)

            State.MyBO = New ReportRequests
            State.ForEdit = True
            PopulateBOProperty(State.MyBO, "ReportType", "TIMM Certificate Extract Report")
            PopulateBOProperty(State.MyBO, "ReportProc", "R_TIM_CERTIFICATE_EXTRACT.Report")
            PopulateBOProperty(State.MyBO, "ReportParameters", reportParams.ToString())
            PopulateBOProperty(State.MyBO, "UserEmailAddress", ElitaPlusIdentity.Current.EmailAddress)

            ScheduleReport()
        End Sub

        Private Sub ScheduleReport()
            Try
                Dim scheduleDate As DateTime = TheReportExtractInputControl.GetSchedDate()
                If State.MyBO.IsDirty Then
                    State.MyBO.Save()

                    State.IsNew = False
                    State.HasDataChanged = True
                    State.MyBO.CreateJob(scheduleDate)

                    If String.IsNullOrEmpty(ElitaPlusIdentity.Current.EmailAddress) Then
                        DisplayMessage(Message.MSG_Email_not_configured, "", MSG_BTN_OK, MSG_TYPE_ALERT, , True)
                    Else
                        DisplayMessage(Message.MSG_REPORT_REQUEST_IS_GENERATED, "", MSG_BTN_OK, MSG_TYPE_ALERT, , True)
                    End If

                    'btnGenRpt.Enabled = False

                End If

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub
    End Class
End Namespace


