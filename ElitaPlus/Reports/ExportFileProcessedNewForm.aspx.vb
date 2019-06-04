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

        Public Property ServiceCenterId As Guid
            Get
                Return Me.State.ServiceCenterId
            End Get
            Set(ByVal value As Guid)
                Me.State.ServiceCenterId = value
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

        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

#Region "Handlers-Init"

        Private Sub SetStateProperties()
            Me.State.FileProcessedId = CType(CType(Me.CallingParameters, MyState).FileProcessedId, Guid)
            Me.State.moFileTypeCode = CType(CType(Me.CallingParameters, MyState).moFileTypeCode, FileProcessedData.FileTypeCode)
            Me.State.LoadStatus = CType(Me.CallingParameters, MyState).LoadStatus
            Me.State.ReportFileName = CType(Me.CallingParameters, MyState).ReportFileName
            Me.State.reportType = CType(Me.CallingParameters, MyState).reportType
            Me.State.oReturnType = CType(Me.CallingParameters, MyState).oReturnType
            Me.State.ServiceCenterId = CType(CType(Me.CallingParameters, MyState).ServiceCenterId, Guid)
        End Sub

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            Try
                Me.MasterPage.MessageController.Clear_Hide()
                If Not IsPostBack Then
                    InitializeForm()
                End If
                Me.InstallDisplayNewReportProgressBar()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
            ShowMissingTranslations(Me.MasterPage.MessageController)
        End Sub

        Private Sub UpdateBreadCrum(TabName As String, Title As String)
            Me.MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage(TabName) & ":"
            Me.MasterPage.BreadCrum = Me.MasterPage.PageTab & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage(Title)
            Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage(Title)

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

#Region "Handlers-DroDown"
        Protected Sub OnFromDrop_Changed(ByVal sender As Object, ByVal e As System.EventArgs) _
                     Handles moReportCeInputControl.SelectedDestOptionChanged

            moReportCeInputControl.UpdateFileNameControlVisible(False)
            moReportCeInputControl.ModifyFileNameChecked = False

        End Sub
#End Region

#End Region

#Region "Populate"

        Private Sub InitializeForm()
            SetStateProperties()
            Dim FileTypeCode As FileProcessedData.FileTypeCode = Me.State.moFileTypeCode
            Me.lblEntireRecord.Visible = False
            Me.moEntireRecordCheck.Visible = False
            Me.lblIncludeBypassedRecords.Visible = False
            Me.moInclBypassedRecCheck.Visible = False
            TheReportExtractInputControl.SetExportOnly()
            TheReportExtractInputControl.DestinationVisible = False

            If Me.State.reportType.Equals(PROCESSED_EXPORT) Then
                UpdateBreadCrum(TAB_NAME_PROCESSED_EXPORT, VENDINV_PAGETITLE)
            Else
                UpdateBreadCrum(TAB_NAME_REJECT_REPORT, VENDINV_PAGETITLE)
            End If

        End Sub

#End Region

#Region "Export"
        Private Sub GenerateReport()

            Dim sEntireRecordOnly As String = "N"
            Dim FileTypeCode As FileProcessedData.FileTypeCode = Me.State.moFileTypeCode
            Dim sIncludeByPassRecs As String = "Y"
            Dim reportParams As New System.Text.StringBuilder
            Me.State.MyBO = New ReportRequests
            Me.State.ForEdit = True

            If Me.moInclBypassedRecCheck.Visible Then
                If Not Me.moInclBypassedRecCheck.Checked Then
                    sIncludeByPassRecs = "N"
                End If
            End If

            Select Case FileTypeCode
                Case FileProcessedData.FileTypeCode.VendorInv
                    reportParams.AppendFormat("V_FILE_PROCESSED_ID=> '{0}',", DALBase.GuidToSQLString(FileProcessedId))
                    reportParams.AppendFormat("V_SERVICE_CENTER_ID=> '{0}',", DALBase.GuidToSQLString(ServiceCenterId))
                    reportParams.AppendFormat("V_LANGUAGE_ID => '{0}',", DALBase.GuidToSQLString(Thread.CurrentPrincipal.GetLanguageId()))
                    reportParams.AppendFormat("V_FILE_NAME => '{0}',", Me.State.ReportFileName)
                    Select Case Me.State.reportType
                        Case REJECT_REPORT
                            reportParams.AppendFormat("V_INCLUDE_BYPASS => '{0}',", sIncludeByPassRecs)
                            reportParams.AppendFormat("V_REPORT_TYPE => '{0}'", "VENDOR_INVENTORY_REJECT_EXPORT")
                            Me.PopulateBOProperty(Me.State.MyBO, "ReportType", "VENDOR_INVENTORY_REJECT_EXPORT")
                            Me.PopulateBOProperty(Me.State.MyBO, "ReportProc", "R_VENDOR_INVOICE_EXPORT.gen_reject_export")
                        Case PROCESSED_EXPORT
                            'ProcessedPayments-Exp_EN
                            reportParams.AppendFormat("V_REPORT_TYPE => '{0}'", "VENDOR_INVENTORY_PROCESS_EXPORT")
                            Me.PopulateBOProperty(Me.State.MyBO, "ReportType", "VENDOR_INVENTORY_PROCESS_EXPORT")
                            Me.PopulateBOProperty(Me.State.MyBO, "ReportProc", "R_VENDOR_INVOICE_EXPORT.gen_processed_export")
                    End Select
            End Select

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
                        Me.DisplayMessage(Message.MSG_Email_not_configured, "", Me.MSG_BTN_OK, Me.MSG_TYPE_ALERT, , True)
                    Else
                        Me.DisplayMessage(Message.MSG_REPORT_REQUEST_IS_GENERATED, "", Me.MSG_BTN_OK, Me.MSG_TYPE_ALERT, , True)
                    End If
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub
#End Region

    End Class
End Namespace
