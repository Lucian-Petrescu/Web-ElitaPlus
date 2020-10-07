Imports Assurant.ElitaPlus.ElitaPlusWebApp.Interfaces
Namespace Reports
    Public Class ExportFileProcessedForm
        Inherits ElitaPlusPage

#Region "Page State"
        Class MyState
            Public ReportFileName As String
            Public LoadStatus As String
            Public FileProcessedId As Guid
            Public ReportWindowName As String
            Public oReturnType As FileProcessedController.ReturnType
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

#Region "Constants"
        Private Const RPT_FILENAME_WINDOW As String = "COVERAGE DEDUCTIBLE DEFINITIONS"

        Private Const ONE_ITEM As Integer = 1
#End Region

#Region "Variables"
        Private mReportFileName As String
#End Region

#Region "Properties"

        Public Property ReportWindowName As String
            Get
                Return State.ReportWindowName
            End Get
            Set(value As String)
                State.ReportWindowName = value
                TitleTextLabel.Text = ReportWindowName
            End Set
        End Property

        Public Property ReportFileName As String
            Get
                Return State.ReportFileName
            End Get
            Set(value As String)
                State.ReportFileName = value
            End Set
        End Property

        Public Property LoadStatus As String
            Get
                Return State.LoadStatus
            End Get
            Set(value As String)
                State.LoadStatus = value
            End Set
        End Property

        Public Property FileProcessedId As Guid
            Get
                Return State.FileProcessedId
            End Get
            Set(value As Guid)
                State.FileProcessedId = value
            End Set
        End Property

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
        Protected WithEvents moReportCeInputControl As ReportCeInputControl
        Protected WithEvents ErrorCtrl As ErrorController
        Protected WithEvents btnViewHidden As System.Web.UI.WebControls.Button

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

        Private Sub ExportFileProcessedForm_PageCall(CallFromUrl As String, CallingPar As Object) Handles Me.PageCall
            Dim callingParam As MyState
            callingParam = CType(CallingPar, MyState)
            State.FileProcessedId = callingParam.FileProcessedId
            State.LoadStatus = callingParam.LoadStatus
            State.ReportFileName = callingParam.ReportFileName
            ReportWindowName = callingParam.ReportWindowName
            State.oReturnType = callingParam.oReturnType
        End Sub

        Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            ErrorCtrl.Clear_Hide()
            Try
                If Not IsPostBack Then
                    InitializeForm()
                End If
                InstallProgressBar()
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try
            ShowMissingTranslations(ErrorCtrl)
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

        Private Sub btnBack_Click(sender As System.Object, e As System.EventArgs) Handles btnBack.Click
            Try
                State.oReturnType.LastOperation = ElitaPlusPage.DetailPageCommand.Back
                ReturnToCallingPage(State.oReturnType)
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try
        End Sub

#End Region

#End Region

#Region "Populate"

        Private Sub InitializeForm()
            TheRptCeInputControl.SetExportOnly()
        End Sub

#End Region

#Region "Crystal Enterprise"

        Function SetParameters(fileProcessedId As Guid, loadStatus As String) As ReportCeBaseForm.Params

            Dim params As New ReportCeBaseForm.Params
            Dim moReportFormat As ReportCeBaseForm.RptFormat
            'Dim reportName As String = TheReportCeInputControl.getReportName(Me.ReportFileName, True)
            'Me.rptWindowTitle.InnerText = TheReportCeInputControl.getReportWindowTitle(RPT_FILENAME_WINDOW)

            moReportFormat = ReportCeBase.GetReportFormat(Me)

            Dim repParams() As ReportCeBaseForm.RptParam = New ReportCeBaseForm.RptParam() _
                                    {New ReportCeBaseForm.RptParam("P_FILE_PROCESSED_ID", GuidControl.GuidToHexString(fileProcessedId)),
                                     New ReportCeBaseForm.RptParam("P_LOAD_STATUS", loadStatus)}

            With params
                .msRptName = ReportFileName 'RPT_FILENAME_EXPORT
                .msRptWindowName = TranslationBase.TranslateLabelOrMessage(ReportWindowName)
                .moRptFormat = moReportFormat
                .moAction = ReportCeBaseForm.RptAction.SCHEDULE_VIEW
                .moRptParams = repParams
            End With
            Return params
        End Function

        Private Sub GenerateReport()
            ReportCeBase.EnableReportCe(Me, TheRptCeInputControl)

            Dim params As ReportCeBaseForm.Params = SetParameters(State.FileProcessedId, State.LoadStatus)
            Session(ReportCeBaseForm.SESSION_PARAMETERS_KEY) = params

        End Sub

#End Region

    End Class
End Namespace
