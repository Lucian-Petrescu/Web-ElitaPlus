Imports System.Threading
Imports Assurant.ElitaPlus.DALObjects
Imports Assurant.ElitaPlus.ElitaPlusWebApp.Interfaces
Imports Assurant.ElitaPlus.Security

Namespace Reports
    Public Class ExportFileProcessedNewForm
        Inherits ElitaPlusPage

#Region "Page State"
        Class MyState
            Public ReportFileName As String
            Public LoadStatus As String
            Public FileProcessedId As Guid
            Public ServiceCenterId As Guid
            Public moFileTypeCode As FileProcessedData.FileTypeCode
            Public oReturnType As FileProcessedControllerNew.ReturnType
            Public reportType As Integer
            Public MyBO As ReportRequests
            Public ForEdit As Boolean = False
            Public IsNew As Boolean = False
            Public IsACopy As Boolean
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

#Region "Constants"

        Public Const REJECT_REPORT As Integer = 0
        Public Const PROCESSED_EXPORT As Integer = 1
        Public Const VENDINV_PAGETITLE As String = "VENDOR_INVENTORY_EXP"
        Public Const TAB_NAME_REJECT_REPORT As String = "REJECT_REPORT_EXP"
        Public Const TAB_NAME_PROCESSED_EXPORT As String = "PROCESSED_REPORT_EXP"
        Public Shared URL As String = ELPWebConstants.APPLICATION_PATH & "/Reports/ExportFileProcessedNewForm.aspx"

#End Region

#Region "Variables"
        Private mReportFileName As String
#End Region

#Region "Properties"

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

        Public Property ServiceCenterId As Guid
            Get
                Return State.ServiceCenterId
            End Get
            Set(value As Guid)
                State.ServiceCenterId = value
            End Set
        End Property

#End Region

#Region "Handlers"

#Region " Web Form Designer Generated Code "
        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        End Sub
        Protected WithEvents moReportCeInputControl As ReportExtractInputControl
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

        Private Sub SetStateProperties()
            State.FileProcessedId = CType(CType(CallingParameters, MyState).FileProcessedId, Guid)
            State.moFileTypeCode = CType(CType(CallingParameters, MyState).moFileTypeCode, FileProcessedData.FileTypeCode)
            State.LoadStatus = CType(CallingParameters, MyState).LoadStatus
            State.ReportFileName = CType(CallingParameters, MyState).ReportFileName
            State.reportType = CType(CallingParameters, MyState).reportType
            State.oReturnType = CType(CallingParameters, MyState).oReturnType
            State.ServiceCenterId = CType(CType(CallingParameters, MyState).ServiceCenterId, Guid)
        End Sub

        Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
            Try
                MasterPage.MessageController.Clear_Hide()
                If Not IsPostBack Then
                    InitializeForm()
                End If
                InstallDisplayNewReportProgressBar()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
            ShowMissingTranslations(MasterPage.MessageController)
        End Sub

        Private Sub UpdateBreadCrum(TabName As String, Title As String)
            MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage(TabName) & ":"
            MasterPage.BreadCrum = MasterPage.PageTab & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage(Title)
            MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage(Title)

        End Sub

#End Region

#Region "Handlers-Buttons"

        Private Sub btnGenRpt_Click(sender As System.Object, e As System.EventArgs) Handles btnGenRpt.Click
            Try
                GenerateReport()
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
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

#Region "Handlers-DroDown"
        Protected Sub OnFromDrop_Changed(sender As Object, e As System.EventArgs) _
                     Handles moReportCeInputControl.SelectedDestOptionChanged

            moReportCeInputControl.UpdateFileNameControlVisible(False)
            moReportCeInputControl.ModifyFileNameChecked = False

        End Sub
#End Region

#End Region

#Region "Populate"

        Private Sub InitializeForm()
            SetStateProperties()
            Dim FileTypeCode As FileProcessedData.FileTypeCode = State.moFileTypeCode
            lblEntireRecord.Visible = False
            moEntireRecordCheck.Visible = False
            lblIncludeBypassedRecords.Visible = False
            moInclBypassedRecCheck.Visible = False
            TheReportExtractInputControl.SetExportOnly()
            TheReportExtractInputControl.DestinationVisible = False

            If State.reportType.Equals(PROCESSED_EXPORT) Then
                UpdateBreadCrum(TAB_NAME_PROCESSED_EXPORT, VENDINV_PAGETITLE)
            Else
                UpdateBreadCrum(TAB_NAME_REJECT_REPORT, VENDINV_PAGETITLE)
            End If

        End Sub

#End Region

#Region "Export"
        Private Sub GenerateReport()

            Dim sEntireRecordOnly As String = "N"
            Dim FileTypeCode As FileProcessedData.FileTypeCode = State.moFileTypeCode
            Dim sIncludeByPassRecs As String = "Y"
            Dim reportParams As New System.Text.StringBuilder
            State.MyBO = New ReportRequests
            State.ForEdit = True

            If moInclBypassedRecCheck.Visible Then
                If Not moInclBypassedRecCheck.Checked Then
                    sIncludeByPassRecs = "N"
                End If
            End If

            Select Case FileTypeCode
                Case FileProcessedData.FileTypeCode.VendorInv
                    reportParams.AppendFormat("V_FILE_PROCESSED_ID=> '{0}',", DALBase.GuidToSQLString(FileProcessedId))
                    reportParams.AppendFormat("V_SERVICE_CENTER_ID=> '{0}',", DALBase.GuidToSQLString(ServiceCenterId))
                    reportParams.AppendFormat("V_LANGUAGE_ID => '{0}',", DALBase.GuidToSQLString(Thread.CurrentPrincipal.GetLanguageId()))
                    reportParams.AppendFormat("V_FILE_NAME => '{0}',", State.ReportFileName)
                    Select Case State.reportType
                        Case REJECT_REPORT
                            reportParams.AppendFormat("V_INCLUDE_BYPASS => '{0}',", sIncludeByPassRecs)
                            reportParams.AppendFormat("V_REPORT_TYPE => '{0}'", "VENDOR_INVENTORY_REJECT_EXPORT")
                            PopulateBOProperty(State.MyBO, "ReportType", "VENDOR_INVENTORY_REJECT_EXPORT")
                            PopulateBOProperty(State.MyBO, "ReportProc", "R_VENDOR_INVOICE_EXPORT.gen_reject_export")
                        Case PROCESSED_EXPORT
                            'ProcessedPayments-Exp_EN
                            reportParams.AppendFormat("V_REPORT_TYPE => '{0}'", "VENDOR_INVENTORY_PROCESS_EXPORT")
                            PopulateBOProperty(State.MyBO, "ReportType", "VENDOR_INVENTORY_PROCESS_EXPORT")
                            PopulateBOProperty(State.MyBO, "ReportProc", "R_VENDOR_INVOICE_EXPORT.gen_processed_export")
                    End Select
            End Select

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
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub
#End Region

    End Class
End Namespace
