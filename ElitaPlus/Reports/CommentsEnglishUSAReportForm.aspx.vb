Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports Assurant.Elita.Web.Forms
Imports System.Threading
Namespace Reports

    Partial Class CommentsEnglishUSAReportForm
        Inherits ElitaPlusPage

#Region "Constants"

        Private Const RPT_FILENAME_WINDOW As String = "Comments"
        Private Const RPT_FILENAME As String = "CommentsEnglishUSA"
        Private Const RPT_FILENAME_EXPORT As String = "CommentsEnglishUSA_Exp"

        Public Const ALL As String = "*"
        '   Public Const SP_DATE_FORMAT As String = "yyyyMMdd"
        Public Const YES As String = "Y"
        Protected WithEvents btnViewHidden As System.Web.UI.WebControls.Button
        Public Const NO As String = "N"

        Public Const BY_DEALER_CODE As String = "0"
        Public Const BY_CERTIFICATE_NUMBER As String = "1"
        Public Const BY_DATE_COMMENT_ADDED As String = "2"
        Public Const BY_COMMENT_TYPE As String = "3"

        Public Const SORT_BY_DEALER_CODE As String = "1"
        Public Const SORT_BY_CERTIFICATE_NUMBER As String = "2"
        Public Const SORT_BY_DATE_COMMENT_ADDED As String = "3"
        Public Const SORT_BY_COMMENT_TYPE As String = "4"
        Private Const LABEL_SELECT_DEALER As String = "OR A SINGLE DEALER"

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
        Public ReadOnly Property DealerMultipleDrop() As MultipleColumnDDLabelControl
            Get
                If moDealerMultipleDrop Is Nothing Then
                    moDealerMultipleDrop = CType(FindControl("multipleDropControl"), MultipleColumnDDLabelControl)
                End If
                Return moDealerMultipleDrop
            End Get
        End Property
#End Region

#Region "Handlers"

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub
        Protected WithEvents moDealerMultipleDrop As MultipleColumnDDLabelControl
        Protected WithEvents moReportCeInputControl As ReportCeInputControl
        Protected WithEvents ErrorCtrl As ErrorController
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
                    'Date Calendars
                    Me.AddCalendar(Me.BtnBeginDate, Me.moBeginDateText)
                    Me.AddCalendar(Me.BtnEndDate, Me.moEndDateText)
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

#Region "Clear"

        Private Sub ClearErrLabels()
            Me.ClearLabelErrSign(moCertNumberLabel)
            Me.ClearLabelErrSign(moBeginDateLabel)
            Me.ClearLabelErrSign(moEndDateLabel)
            Me.ClearLabelErrSign(moCommentTypeLabel)
            Me.ClearLabelErrSign(DealerMultipleDrop.CaptionLabel)
            If Me.rdealer.Checked Then DealerMultipleDrop.SelectedIndex = -1
        End Sub

#End Region

#Region "Populate"

        Sub PopulateDealerDropDown()
            '''Me.BindListControlToDataView(Me.cboDealerDec, LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies, False, "CODE"), , , True)
            '''Dim DealersLookupListSortedByCode As DataView = LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies, False, "CODE")
            '''DealersLookupListSortedByCode.Sort = "CODE"
            '''Me.BindListControlToDataView(Me.cboDealerCode, DealersLookupListSortedByCode, "CODE", , True)
            Dim dv As DataView = LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies, False, "CODE")
            DealerMultipleDrop.NothingSelected = True
            DealerMultipleDrop.SetControl(False, DealerMultipleDrop.MODES.NEW_MODE, True, dv, TranslationBase.TranslateLabelOrMessage(LABEL_SELECT_DEALER), True, True, " document.forms[0].rdealer.checked = false;", "", "", "", False, 6)

        End Sub

        Sub PopulateCommentTypeDropdown()
            Dim commentType As ListItem() = CommonConfigManager.Current.ListManager.GetList("COMMT", Thread.CurrentPrincipal.GetLanguageCode())
            commentType.OrderBy("Code", LinqExtentions.SortDirection.Ascending)
            cboCommentType.Populate(commentType, New PopulateOptions() With
                                              {
                                                .AddBlankItem = True
                                               })
        End Sub

        Private Sub InitializeForm()
            PopulateDealerDropDown()
            PopulateCommentTypeDropdown()
            Me.moBeginDateText.Text = GetDateFormattedString(Date.Now.AddDays(-1))
            Me.moEndDateText.Text = GetDateFormattedString(Date.Now)
            Me.rdealer.Checked = True
            Me.rcommentType.Checked = True
            Me.rcerts.Checked = True
            Me.RadiobuttonAllRecords.Checked = True
            Me.RadiobuttonAllComments.Checked = True
        End Sub

#End Region

#Region "Crystal Enterprise"

        Function SetParameters(ByVal userId As String, ByVal dealerCode As String, ByVal certNumber As String,
                               ByVal commentType As String, ByVal excludeClosedClaims As String, ByVal claimsCommentsOnly As String, ByVal beginDate As String,
                                  ByVal endDate As String, ByVal sortBy As String, ByVal langCode As String) As ReportCeBaseForm.Params

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
                     New ReportCeBaseForm.RptParam("V_USER_ID", userId),
                     New ReportCeBaseForm.RptParam("V_DEALER_CODE", dealerCode),
                     New ReportCeBaseForm.RptParam("V_CERT_NUMBER", certNumber),
                     New ReportCeBaseForm.RptParam("V_COMMENT_TYPE", commentType),
                     New ReportCeBaseForm.RptParam("V_CLOSED_CLAIMS", excludeClosedClaims),
                     New ReportCeBaseForm.RptParam("V_ONLY_CLAIMS", claimsCommentsOnly),
                     New ReportCeBaseForm.RptParam("V_FROM_DATE", beginDate),
                     New ReportCeBaseForm.RptParam("V_TO_DATE", endDate),
                     New ReportCeBaseForm.RptParam("V_SORT_BY", sortBy),
                     New ReportCeBaseForm.RptParam("V_LANGUAGE_CODE", langCode)}


            'If (moReportFormat = ReportCeBase.RptFormat.TEXT_TAB) Then
            '    reportName = RPT_FILENAME_EXPORT
            'End If

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
            Dim userId As Guid = ElitaPlusIdentity.Current.ActiveUser.Id
            Dim langId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
            Dim langCode As String = LookupListNew.GetCodeFromId("LANGUAGES", langId)
            Dim selectedDealerId As Guid = DealerMultipleDrop.SelectedGuid 'Me.GetSelectedItem(Me.cboDealerDec)
            'Dim dv As DataView = LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies, False, "CODE")
            Dim dealerCode As String = DealerMultipleDrop.SelectedCode 'LookupListNew.GetCodeFromId(dv, dealerID)
            Dim commentTypeLk As DataView
            Dim commentTypeId As Guid = Me.GetSelectedItem(Me.cboCommentType)
            commentTypeLk = LookupListNew.GetCommentTypeLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId)

            Dim commentCode As String = LookupListNew.GetCodeFromId(commentTypeLk, commentTypeId)
            Dim commentDesc As String = LookupListNew.GetDescriptionFromCode(LookupListNew.LK_COMMENT_TYPES, commentCode)
            Dim endDate As String
            Dim beginDate As String
            Dim certNumber As String = CType(Me.moCertNumberTextbox.Text, String)
            Dim excludeClosedClaims As String
            Dim claimsCommentsOnly As String
            Dim sortBy As String

            'Dates
            ReportCeBase.ValidateBeginEndDate(moBeginDateLabel, moBeginDateText.Text, moEndDateLabel, moEndDateText.Text)
            endDate = ReportCeBase.FormatDate(moEndDateLabel, moEndDateText.Text)
            beginDate = ReportCeBase.FormatDate(moBeginDateLabel, moBeginDateText.Text)

            'Certifcates moCertNumberTextbox
            If Me.rcerts.Checked Then
                certNumber = ALL
            Else
                If moCertNumberTextbox.Text.Equals(String.Empty) Then
                    ElitaPlusPage.SetLabelError(moCertNumberLabel)
                    Throw New GUIException(Message.MSG_INVALID_DEALER, Assurant.ElitaPlus.Common.ErrorCodes.GUI_CERTIFICATE_NUMBER_IS_REQUIRED_ERRR)
                End If
            End If

            'dealer
            If Me.rdealer.Checked Then
                dealerCode = ALL
            Else
                If selectedDealerId.Equals(Guid.Empty) Then
                    ElitaPlusPage.SetLabelError(DealerMultipleDrop.CaptionLabel)
                    Throw New GUIException(Message.MSG_INVALID_DEALER, Assurant.ElitaPlus.Common.ErrorCodes.GUI_DEALER_MUST_BE_SELECTED_ERR)
                End If
            End If

            'CommentType
            If Me.rcommentType.Checked Then
                commentCode = ALL
            Else
                If commentTypeId.Equals(Guid.Empty) Then
                    ElitaPlusPage.SetLabelError(moCommentTypeLabel)
                    Throw New GUIException(Message.MSG_INVALID_DEALER, Assurant.ElitaPlus.Common.ErrorCodes.GUI_COMMENT_TYPE_MUST_BE_SELECTED_ERR)
                End If
            End If


            If Me.RadiobuttonAllRecords.Checked Then
                excludeClosedClaims = NO
            Else
                excludeClosedClaims = YES
            End If

            If Me.RadiobuttonAllComments.Checked Then
                claimsCommentsOnly = NO
            Else
                claimsCommentsOnly = YES
            End If

            Select Case Me.rdReportSortOrder.SelectedValue()
                Case BY_DEALER_CODE
                    sortBy = SORT_BY_DEALER_CODE
                Case BY_CERTIFICATE_NUMBER
                    sortBy = SORT_BY_CERTIFICATE_NUMBER
                Case BY_DATE_COMMENT_ADDED
                    sortBy = SORT_BY_DATE_COMMENT_ADDED
                Case BY_COMMENT_TYPE
                    sortBy = SORT_BY_COMMENT_TYPE
            End Select

            ReportCeBase.EnableReportCe(Me, TheRptCeInputControl)

            Dim params As ReportCeBaseForm.Params = SetParameters(GuidControl.GuidToHexString(userId), dealerCode, certNumber, _
                                commentCode, excludeClosedClaims, claimsCommentsOnly, beginDate, _
                                   endDate, sortBy, langCode)
            Session(ReportCeBaseForm.SESSION_PARAMETERS_KEY) = params

        End Sub

#End Region



    End Class
End Namespace
