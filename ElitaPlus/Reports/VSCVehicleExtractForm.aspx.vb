'Imports Assurant.ElitaPlus.BusinessObjects.Common
'Imports Assurant.ElitaPlus.DALObjects
'Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Namespace Reports
    Partial Class VSCVehicleExtractForm
        Inherits ElitaPlusPage

#Region "Constants"

        Public Const PAGETITLE As String = "VSC_Make_And_Model_Extract"
        Public Const ALL As String = "*"


#Region "Handlers"

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub

        Protected WithEvents moReportCeInputControl As ReportCeInputControl

        'NOTE: The following placeholder declaration is required by the Web Form Designer.
        'Do not delete or move it.
        Private designerPlaceholderDeclaration As System.Object

        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

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

#Region "Handlers-Init"

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            Me.MasterPage.MessageController.Clear_Hide()
            '    Me.ClearLabelsErrSign()
            Try
                If Not Me.IsPostBack Then

                    TheReportExtractInputControl.ViewVisible = False
                    TheReportExtractInputControl.PdfVisible = False
                    TheReportExtractInputControl.ExportDataVisible = False
                    TheReportExtractInputControl.DestinationVisible = False
                    Me.MasterPage.UsePageTabTitleInBreadCrum = False
                    UpdateBreadCrum()
                    '      InitializeForm()

                End If
                Me.InstallDisplayNewReportProgressBar()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
            Me.ShowMissingTranslations(Me.MasterPage.MessageController)
        End Sub

#End Region

#Region "Handlers-Buttons"


        Private Sub btnGenRpt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenRpt.Click
            Try
                GenerateReport()
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub


#End Region

#End Region



        Private Sub UpdateBreadCrum()
            Me.MasterPage.BreadCrum = Me.MasterPage.PageTab & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage(PAGETITLE)
            Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage(PAGETITLE)
        End Sub

#End Region

        Private Sub GenerateReport()

            Dim reportParams As New System.Text.StringBuilder
            'Dim code As String = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Code
            reportParams.AppendFormat("v_company_group => '{0}'", ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Code)


            Me.State.MyBO = New ReportRequests
            '    Me.State.ForEdit = True

            Me.PopulateBOProperty(Me.State.MyBO, "ReportType", "VSC Make And Model Extract")
            'Me.PopulateBOProperty(Me.State.MyBO, "ReportProc", "Elp_Exp_Perf_Trak")
            Me.PopulateBOProperty(Me.State.MyBO, "ReportProc", "r_VSCMakeAndModelExtract.report")
            Me.PopulateBOProperty(Me.State.MyBO, "ReportParameters", reportParams.ToString())
            Me.PopulateBOProperty(Me.State.MyBO, "UserEmailAddress", ElitaPlusIdentity.Current.EmailAddress)

            ScheduleReport()

        End Sub

        Private Sub ScheduleReport()
            Try
                Dim scheduleDate As DateTime = TheReportExtractInputControl.GetSchedDate()
                If Me.State.MyBO.IsDirty Then
                    Me.State.MyBO.Save()

                    Me.State.IsNew = False
                    Me.State.HasDataChanged = True
                    Me.State.MyBO.CreateJob(scheduleDate)
                    If String.IsNullOrEmpty(ElitaPlusIdentity.Current.EmailAddress) Then
                        Me.DisplayMessage(Message.MSG_Email_not_configured, "", MSG_BTN_OK, MSG_TYPE_ALERT, , True)
                    Else
                        Me.DisplayMessage(Message.MSG_REPORT_REQUEST_IS_GENERATED, "", MSG_BTN_OK, MSG_TYPE_ALERT, , True)
                    End If
                    btnGenRpt.Enabled = False
                End If

            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub
    End Class
End Namespace
