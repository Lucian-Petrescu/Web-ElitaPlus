Namespace Reports
    Partial Public Class AMLRegulatoryUIFForm
        Inherits ElitaPlusPage

#Region "Constants"

        Public Const PAGETITLE As String = "AML_REGULATORY_UIF"
        Public Const PAGETAB As String = "REPORTS"

#End Region

#Region "User Companies"
        Private Function GetUserCompanies() As String
            Dim oSelectedlist As String = ","
            For Each row As DataRowView In ElitaPlusIdentity.Current.ActiveUser.GetUserCompanies(ElitaPlusIdentity.Current.ActiveUser.Id)
                oSelectedlist = oSelectedlist + row("code").ToString() + ","
            Next
            Return oSelectedlist
        End Function

#End Region

#Region "Page State"
        Class MyState
            Public MyBO As ReportRequests
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

#Region "Web Form Designer Generated Code"

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub

        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
            InitializeComponent()
        End Sub

#End Region

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            Me.MasterPage.MessageController.Clear_Hide()
            Me.ClearLabelsErrSign()

            Try
                If Not Me.IsPostBack Then
                    ' Step - Hide standard Crystal Report Display Options
                    TheReportCeInputControl.ExportDataVisible = True
                    TheReportCeInputControl.SetExportOnly()
                    TheReportCeInputControl.DestinationVisible = False

                    ' Step - Configure Header, Breadcrum, etc.
                    Me.MasterPage.UsePageTabTitleInBreadCrum = False
                    Me.SetFormTab(PAGETAB)

                    UpdateBreadCrum()

                    ' Step - Attach Javascripts to Calandar and Populate Company Dropdown
                    Me.AddCalendar_New(Me.BtnBeginDate, Me.BeginDateText)
                    Me.AddCalendar_New(Me.BtnEndDate, Me.EndDateText)
                End If

                Me.InstallDisplayNewReportProgressBar()

            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try

            Me.ShowMissingTranslations(Me.MasterPage.MessageController)
        End Sub

        Public Sub ClearLabelsErrSign()
            Try
                Me.ClearLabelErrSign(lblReportBy)
                Me.ClearLabelErrSign(lblReportType)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub UpdateBreadCrum()
            Me.MasterPage.BreadCrum = Me.MasterPage.PageTab & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage(PAGETITLE)
            Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage(PAGETITLE)
        End Sub

        Protected Sub btnGenRpt_Click(sender As Object, e As EventArgs) Handles btnGenRpt.Click
            Try
                GenerateReport()
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub GenerateReport()

            Me.ClearLabelsErrSign()

            Dim reportParams As New System.Text.StringBuilder
            Dim oBeginDateTime As DateTime = Nothing
            Dim oEndDateTime As DateTime = Nothing
            Dim runReport As Boolean = True

            ' Begin Date Missing
            If String.IsNullOrEmpty(Me.BeginDateText.Text.Trim) Then
                runReport = False
                Throw New GUIException(Message.MSG_INVALID_BEGIN_END_DATES_ERR, Assurant.ElitaPlus.Common.ErrorCodes.GUI_BEGIN_DATE_REQUIRED_ERR)
            End If

            ' End Date Missing
            If String.IsNullOrEmpty(Me.EndDateText.Text.Trim) Then
                runReport = False
                Throw New GUIException(Message.MSG_INVALID_BEGIN_END_DATES_ERR, Assurant.ElitaPlus.Common.ErrorCodes.GUI_END_DATE_REQUIRED_ERR)
            End If

            ' Both Dates are present
            oBeginDateTime = DateHelper.GetDateValue(Me.BeginDateText.Text)
            oEndDateTime = DateHelper.GetDateValue(Me.EndDateText.Text)

            ' End Date is Less Than Begin Date
            If oEndDateTime < oBeginDateTime Then
                runReport = False
                ElitaPlusPage.SetLabelError(lblReportBy)
                Throw New GUIException(Message.MSG_INVALID_DATE_RANGE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_INVALID_DATE_RANGE)
            End If

            If String.IsNullOrEmpty(ElitaPlusIdentity.Current.EmailAddress) Then
                runReport = False
                Me.DisplayMessage(Message.MSG_Email_not_configured, "", ElitaPlusPage.MSG_BTN_OK, ElitaPlusPage.MSG_TYPE_ALERT, , True)
                btnGenRpt.Enabled = False
            End If

            If runReport Then
                reportParams.AppendFormat("pi_claim_created_from => '{0}',", oBeginDateTime.ToString("yyyyMMdd"))
                reportParams.AppendFormat("pi_claim_created_to => '{0}',", oEndDateTime.ToString("yyyyMMdd"))
                reportParams.AppendFormat("pi_report_type =>'{0}',", Me.rblReportType.SelectedValue)
                reportParams.AppendFormat("pi_companies => '{0}'", GetUserCompanies())

                Me.State.MyBO = New ReportRequests
                Me.PopulateBOProperty(Me.State.MyBO, "ReportType", TranslationBase.TranslateLabelOrMessage(PAGETITLE))
                Me.PopulateBOProperty(Me.State.MyBO, "ReportProc", "R_AML_REGULATORY_UIF.REPORT")
                Me.PopulateBOProperty(Me.State.MyBO, "ReportParameters", reportParams.ToString())
                Me.PopulateBOProperty(Me.State.MyBO, "UserEmailAddress", ElitaPlusIdentity.Current.EmailAddress)

                ScheduleReport()
            End If
        End Sub

        Private Sub ScheduleReport()
            Try
                Dim scheduleDate As DateTime = TheReportCeInputControl.GetSchedDate()

                If Me.State.MyBO.IsDirty Then
                    Me.State.MyBO.CreateReportRequest(scheduleDate)
                    Me.DisplayMessage(Message.MSG_REPORT_REQUEST_IS_GENERATED, "", ElitaPlusPage.MSG_BTN_OK, ElitaPlusPage.MSG_TYPE_ALERT, , True)
                    btnGenRpt.Enabled = False
                End If

            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

    End Class
End Namespace