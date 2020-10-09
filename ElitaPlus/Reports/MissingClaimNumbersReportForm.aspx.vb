Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Namespace Reports

    Partial Class MissingClaimNumbersReportForm
        Inherits ElitaPlusPage


#Region "Constants"

        Private Const RPT_FILENAME_WINDOW As String = "MissingClaimNumbers"
        Private Const RPT_FILENAME As String = "MissingClaimNumbersEnglishUSA"
        Private Const RPT_FILENAME_EXPORT As String = "MissingClaimNumbersEnglishUSA_Exp"

        Public Const ALL As String = "*"
        ' Public Const SP_DATE_FORMAT As String = "yyyyMMdd"
        Public Const YES As String = "Y"
        Protected WithEvents btnViewHidden As System.Web.UI.WebControls.Button
        Public Const NO As String = "N"

        Public Const MAX_LOWER_DATE_DIFF As Integer = -30
        Private Const ONE_ITEM As Integer = 1
        Private Const LABEL_SELECT_COMPANY As String = "SELECT_COMPANY"
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
        Public ReadOnly Property UserCompanyMultipleDrop() As MultipleColumnDDLabelControl
            Get
                If moUserCompanyMultipleDrop Is Nothing Then
                    moUserCompanyMultipleDrop = CType(FindControl("multipleDropControl"), MultipleColumnDDLabelControl)
                End If
                Return moUserCompanyMultipleDrop
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
        Protected WithEvents moUserCompanyMultipleDrop As MultipleColumnDDLabelControl
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

        Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            ErrorCtrl.Clear_Hide()
            Try
                If Not IsPostBack Then
                    PopulateCompaniesDropdown()
                    'TheReportCeInputControl.SetExportOnly()
                Else
                    ClearErrLabels()
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

#End Region

#End Region

#Region "Clear"

        Private Sub ClearErrLabels()
            ClearLabelErrSign(moBeginClaimLabel)
            ClearLabelErrSign(moEndClaimLabel)
        End Sub
#End Region

#Region "Populate"

        Private Sub PopulateCompaniesDropdown()
            Dim dv As DataView = LookupListNew.GetUserCompaniesLookupList()

            '''Me.UserCompanyMultipleDrop.NothingSelected = False
            '''Me.UserCompanyMultipleDrop.Caption = Me.TranslateLabelOrMessage("SELECT_COMPANY")
            '''UserCompanyMultipleDrop.BindData(dv)
            '''If UserCompanyMultipleDrop.Count.Equals(ONE_ITEM) Then
            '''    '   HideHtmlElement("ddSeparator")
            '''    UserCompanyMultipleDrop.Visible = False
            '''End If

            UserCompanyMultipleDrop.NothingSelected = True
            UserCompanyMultipleDrop.SetControl(False, UserCompanyMultipleDrop.MODES.NEW_MODE, True, dv, TranslationBase.TranslateLabelOrMessage(LABEL_SELECT_COMPANY), True)
            If dv.Count.Equals(ONE_ITEM) Then
                UserCompanyMultipleDrop.SelectedIndex = ONE_ITEM
                UserCompanyMultipleDrop.Visible = False
            End If
        End Sub
#End Region

#Region "Crystal Enterprise"

        Function SetParameters(companyCode As String, strStartClaim As String,
                                  strEndClaim As String) As ReportCeBaseForm.Params

            Dim params As New ReportCeBaseForm.Params
            Dim reportName As String = RPT_FILENAME
            Dim moReportFormat As ReportCeBaseForm.RptFormat

            Dim exportData As String = NO

            moReportFormat = ReportCeBase.GetReportFormat(Me)
            If (moReportFormat = ReportCeBase.RptFormat.TEXT_TAB) OrElse (moReportFormat = ReportCeBase.RptFormat.TEXT_CSV) Then
                reportName = RPT_FILENAME_EXPORT
            End If

            Dim repParams() As ReportCeBaseForm.RptParam = New ReportCeBaseForm.RptParam() _
                    {
                     New ReportCeBaseForm.RptParam("V_COMPANY_KEY", companyCode),
                     New ReportCeBaseForm.RptParam("V_START_CLAIM", strStartClaim),
                     New ReportCeBaseForm.RptParam("V_END_CLAIM", strEndClaim)}

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
            'Dim oCompanyId As Guid = Me.GetApplicationUser.CompanyID
            'Dim compCode As String = LookupListNew.GetCodeFromId("COMPANIES", oCompanyId)
            Dim compCode As String = UserCompanyMultipleDrop.SelectedCode
            Dim intStartClaim As Integer = -1
            Dim intEndClaim As Integer = -1

            'Start and End claim numbers must be 8 digits long.
            Try
                If moStartClaimText.Text.Length < 8 OrElse moEndClaimText.Text.Length < 8 Then
                    Throw New Exception
                End If
            Catch ex As Exception
                If moStartClaimText.Text.Length < 8 Then ElitaPlusPage.SetLabelError(moBeginClaimLabel)
                If moEndClaimText.Text.Length < 8 Then ElitaPlusPage.SetLabelError(moEndClaimLabel)
                Throw New GUIException(Message.MSG_INVALID_NUMBER, Assurant.ElitaPlus.Common.ErrorCodes.GUI_CLAIM_NUMBER_MUST_BE_8_CHAR)
            End Try

            'End claim number and start claim number must a valid integer.
            Try
                intStartClaim = Integer.Parse(moStartClaimText.Text)
                intEndClaim = Integer.Parse(moEndClaimText.Text)
            Catch ex As Exception
                If intStartClaim = -1 Then
                    ElitaPlusPage.SetLabelError(moBeginClaimLabel)
                    Try
                        intEndClaim = Integer.Parse(moEndClaimText.Text)
                    Catch ex1 As Exception
                        ElitaPlusPage.SetLabelError(moEndClaimLabel)
                    End Try
                Else
                    ElitaPlusPage.SetLabelError(moEndClaimLabel)
                End If
                Throw New GUIException(Message.MSG_INVALID_NUMBER, Assurant.ElitaPlus.Common.ErrorCodes.GUI_INVALID_NUMBER)
            End Try

            'End claim number must be higher than start claim number.
            If intEndClaim - intStartClaim <= 0 Then
                ElitaPlusPage.SetLabelError(moEndClaimLabel)
                ElitaPlusPage.SetLabelError(moBeginClaimLabel)
                Throw New GUIException(Message.MSG_INVALID_NUMBER, Assurant.ElitaPlus.Common.ErrorCodes.GUI_INVALID_RANGE)
            End If

            'Start and End claim numbers must be within the same year, first 2 digits must be the same.
            If String.Compare(moStartClaimText.Text.Substring(0, 2), moEndClaimText.Text.Substring(0, 2)) <> 0 Then
                ElitaPlusPage.SetLabelError(moEndClaimLabel)
                ElitaPlusPage.SetLabelError(moBeginClaimLabel)
                Throw New GUIException(Message.MSG_INVALID_NUMBER, Assurant.ElitaPlus.Common.ErrorCodes.GUI_RANGE_MUST_BE_IN_SAME_YEAR)
            End If

            'Validating the Company selection
            If UserCompanyMultipleDrop.SelectedGuid.Equals(Guid.Empty) Then
                ElitaPlusPage.SetLabelError(UserCompanyMultipleDrop.CaptionLabel)
                Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_COMPANY_IS_REQUIRED)
            End If

            'Convert into 8 char string
            Dim strStartClaim As String = intStartClaim.ToString
            Dim strEndClaim As String = intEndClaim.ToString
            If strStartClaim.Length < 8 Then
                strStartClaim = "00000000".Substring(0, 8 - strStartClaim.Length) & strStartClaim
            End If
            If strEndClaim.Length < 8 Then
                strEndClaim = "00000000".Substring(0, 8 - strEndClaim.Length) & strEndClaim
            End If

            ReportCeBase.EnableReportCe(Me, TheRptCeInputControl)

            Dim params As ReportCeBaseForm.Params = SetParameters(compCode, strStartClaim, strEndClaim)
            Session(ReportCeBaseForm.SESSION_PARAMETERS_KEY) = params

        End Sub

#End Region


    End Class
End Namespace
