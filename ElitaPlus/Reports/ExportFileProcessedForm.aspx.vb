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
                Return Me.State.ReportWindowName
            End Get
            Set(ByVal value As String)
                Me.State.ReportWindowName = value
                TitleTextLabel.Text = Me.ReportWindowName
            End Set
        End Property

        Public Property ReportFileName As String
            Get
                Return Me.State.ReportFileName
            End Get
            Set(ByVal value As String)
                Me.State.ReportFileName = value
            End Set
        End Property

        Public Property LoadStatus As String
            Get
                Return Me.State.LoadStatus
            End Get
            Set(ByVal value As String)
                Me.State.LoadStatus = value
            End Set
        End Property

        Public Property FileProcessedId As Guid
            Get
                Return Me.State.FileProcessedId
            End Get
            Set(ByVal value As Guid)
                Me.State.FileProcessedId = value
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

        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

#Region "Handlers-Init"

        Private Sub ExportFileProcessedForm_PageCall(ByVal CallFromUrl As String, ByVal CallingPar As Object) Handles Me.PageCall
            Dim callingParam As MyState
            callingParam = CType(CallingPar, MyState)
            Me.State.FileProcessedId = callingParam.FileProcessedId
            Me.State.LoadStatus = callingParam.LoadStatus
            Me.State.ReportFileName = callingParam.ReportFileName
            Me.ReportWindowName = callingParam.ReportWindowName
            Me.State.oReturnType = callingParam.oReturnType
        End Sub

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            Me.ErrorCtrl.Clear_Hide()
            Try
                If Not Me.IsPostBack Then
                    InitializeForm()
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

        Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
            Try
                Me.State.oReturnType.LastOperation = ElitaPlusPage.DetailPageCommand.Back
                Me.ReturnToCallingPage(Me.State.oReturnType)
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try
        End Sub

#End Region

#End Region

#Region "Populate"

        Private Sub InitializeForm()
            Me.TheRptCeInputControl.SetExportOnly()
        End Sub

#End Region

#Region "Crystal Enterprise"

        Function SetParameters(ByVal fileProcessedId As Guid, ByVal loadStatus As String) As ReportCeBaseForm.Params

            Dim params As New ReportCeBaseForm.Params
            Dim moReportFormat As ReportCeBaseForm.RptFormat
            'Dim reportName As String = TheReportCeInputControl.getReportName(Me.ReportFileName, True)
            'Me.rptWindowTitle.InnerText = TheReportCeInputControl.getReportWindowTitle(RPT_FILENAME_WINDOW)

            moReportFormat = ReportCeBase.GetReportFormat(Me)

            Dim repParams() As ReportCeBaseForm.RptParam = New ReportCeBaseForm.RptParam() _
                                    {New ReportCeBaseForm.RptParam("P_FILE_PROCESSED_ID", GuidControl.GuidToHexString(fileProcessedId)),
                                     New ReportCeBaseForm.RptParam("P_LOAD_STATUS", loadStatus)}

            With params
                .msRptName = Me.ReportFileName 'RPT_FILENAME_EXPORT
                .msRptWindowName = TranslationBase.TranslateLabelOrMessage(Me.ReportWindowName)
                .moRptFormat = moReportFormat
                .moAction = ReportCeBaseForm.RptAction.SCHEDULE_VIEW
                .moRptParams = repParams
            End With
            Return params
        End Function

        Private Sub GenerateReport()
            ReportCeBase.EnableReportCe(Me, TheRptCeInputControl)

            Dim params As ReportCeBaseForm.Params = SetParameters(Me.State.FileProcessedId, Me.State.LoadStatus)
            Session(ReportCeBaseForm.SESSION_PARAMETERS_KEY) = params

        End Sub

#End Region

    End Class
End Namespace
