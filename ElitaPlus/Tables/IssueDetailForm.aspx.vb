Imports Assurant.Elita.CommonConfiguration
Imports Assurant.ElitaPlus.Security
Imports System.Threading
Imports Assurant.Elita.Web.Forms

Namespace Tables

    Partial Public Class IssueDetailForm
        Inherits ElitaPlusSearchPage

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub

        Private Sub Page_Init(sender As System.Object, e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

#Region "Constants"
        Public Const URL As String = "IssueDetailForm.aspx"
        Private Const GRID_COL_EDIT As Integer = 0
        Private Const GRID_COL_DELETE As Integer = 1
        Private Const GRID_COL_NOTE_ID_IDX As Integer = 2
        Private Const GRID_COL_NOTE_TYPE_IDX As Integer = 3
        Private Const GRID_COL_CODE_IDX As Integer = 4
        Private Const GRID_COL_NOTE_IDX As Integer = 5

        Private Const EDIT_COMMAND As String = "EditRecord"
        Private Const DELETE_COMMAND As String = "DeleteRecord"
        Private Const NO_ROW_SELECTED_INDEX As Integer = -1

        Private Const MSG_RECORD_DELETED_OK As String = "MSG_RECORD_DELETED_OK"
        Private Const MSG_RECORD_SAVED_OK As String = "MSG_RECORD_SAVED_OK"
        Private Const MSG_RECORD_NOT_SAVED As String = "MSG_RECORD_NOT_SAVED"

        Private Const COMBO_NOTE_TYPE As String = "cboNoteType"
        Private Const COMBO_ISSUE_TYPE As String = "cboIssueTypeText"
        Private Const TEXT_CODE As String = "moCode"
        Private Const TEXT_NOTE As String = "moText"
        Private Const DESCRIPTION As String = "Description"
        Private Const TABLE_ISSUE_COMMENT As String = "elp_issue_comment"
        Private Const ICTYP As String = "ICTYP"
        Private Const CODE As String = "Code"
#End Region

#Region "Page Return Type"
        Public Class ReturnType
            Public LastOperation As DetailPageCommand
            Public EditingBo As Issue
            Public HasDataChanged As Boolean
            Public Sub New(LastOp As DetailPageCommand, curEditingBo As Issue, hasDataChanged As Boolean)
                LastOperation = LastOp
                EditingBo = curEditingBo
                Me.HasDataChanged = hasDataChanged
            End Sub
        End Class
#End Region

#Region "Page State"
        Private IsReturningFromChild As Boolean = False

        Class MyState
            Public EquipmentListDetailId As Guid = Guid.Empty
            Public MyBO As Issue
            Public ScreenSnapShotBO As Issue
            Public MyNotesChildBO As IssueComment
            Public ScreenSnapShotNotesChildBO As IssueComment
            Public MyQuestionsChildBO As IssueQuestion
            Public ScreenSnapShotQuestionsChildBO As IssueQuestion
            Public MyRulesChildBO As RuleIssue
            Public ScreenSnapShotRulesChildBO As RuleIssue

            Public IsACopy As Boolean
            Public SelectedChildId As Guid = Guid.Empty

            Public Code As String = String.Empty
            Public Description As String = String.Empty
            Public IssueType As String = String.Empty
            Public EffectiveDate As DateType = Nothing
            Public ExpirationDate As DateType = Nothing
            Public ActiveOn As DateType

            Public IsNotesEditing As Boolean = False
            Public IsQuestionsEditing As Boolean = False
            Public IsRulesEditing As Boolean = False
            Public IsEditMode As Boolean = False
            Public IsGridVisible As Boolean
            Public IsChildCreated As Boolean

            Public AddingNewRow As Boolean
            Public SearchDV As DataView = Nothing
            Public NoteDV As DataView = Nothing
            Public IsAfterSave As Boolean

            Public SelectedNotesChildId As Guid = Guid.Empty
            Public SelectedQuestionsChildId As Guid = Guid.Empty
            Public SelectedRulesChildId As Guid = Guid.Empty
            Public PageIndex As Integer = 0

            Public ActionInProgress As DetailPageCommand = DetailPageCommand.Nothing_
            Public LastErrMsg As String
            Public HasDataChanged As Boolean

            Public SearchClick As Boolean = False
            Public SortExpression As String = IssueComment.IssueCommentGridDV.COL_NAME_CODE
            Public SelectedPageSize As Integer = DEFAULT_PAGE_SIZE

            Sub New()
            End Sub
        End Class

        Public Sub New()
            MyBase.New(New MyState)
        End Sub

        Protected Shadows ReadOnly Property State() As MyState
            Get
                Return CType(MyBase.State, MyState)
            End Get
        End Property

        Private Sub Page_PageCall(CallFromUrl As String, CallingPar As Object) Handles MyBase.PageCall
            Dim Issue As IssueSearchForm.MyState

            Try
                If CallingParameters IsNot Nothing Then
                    'Get the id from the parent
                    Issue = CType(CallingParameters, IssueSearchForm.MyState)
                    State.MyBO = New Issue(CType(Issue.SelectedIssueId, Guid))
                    If Not DateHelper.GetDateValue(Issue.ActiveOnDate) = Nothing Then
                        State.MyBO.ActiveOn = DateHelper.GetDateValue(Issue.ActiveOnDate)
                        State.ActiveOn = DateHelper.GetDateValue(Issue.ActiveOnDate)
                    Else
                        State.MyBO.ActiveOn = DateTime.Now
                        State.ActiveOn = DateTime.Now
                    End If
                    State.IsEditMode = True
                End If
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try
        End Sub

        Private Sub Page_PageReturn(ReturnFromUrl As String, ReturnPar As Object) Handles MyBase.PageReturn
            Try
                MenuEnabled = True
                IsReturningFromChild = True
                Dim retObj As EquipmentListDetailForm.ReturnType = CType(ReturnPar, EquipmentListDetailForm.ReturnType)
                State.HasDataChanged = retObj.HasDataChanged
                Select Case retObj.LastOperation
                    Case ElitaPlusPage.DetailPageCommand.Back
                        If retObj IsNot Nothing Then
                            If Not retObj.EditingBo.IsNew Then
                                State.EquipmentListDetailId = retObj.EditingBo.Id
                            End If
                        End If
                    Case ElitaPlusPage.DetailPageCommand.Delete
                        DisplayMessage(Message.DELETE_RECORD_CONFIRMATION, "", MSG_BTN_OK, MSG_TYPE_INFO)
                End Select
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try
        End Sub

        Private Sub SaveGuiState()
            State.Code = moCodeText.Text
            State.Description = moDescriptionText.Text
            If moEffectiveDateText.Text IsNot String.Empty Then
                State.EffectiveDate = DateHelper.GetDateValue(moEffectiveDateText.Text)
            End If
            If moExpirationDateText.Text IsNot String.Empty Then
                State.ExpirationDate = DateHelper.GetDateValue(moExpirationDateText.Text)
            End If
        End Sub

        Private Sub RestoreGuiState()
            moCodeText.Text = State.MyBO.Code
            moDescriptionText.Text = State.MyBO.Description
            moPreConditionsTextBox.Text = State.MyBO.PreConditions
            cboIssueTypeText.SelectedValue = State.MyBO.IssueTypeId.ToString
            If State.MyBO.Effective IsNot Nothing Then
                moEffectiveDateText.Text = ElitaPlusPage.GetLongDate12FormattedString(CDate(State.MyBO.Effective))
            Else
                moEffectiveDateText.Text = String.Empty
            End If
            If State.MyBO.Expiration IsNot Nothing Then
                moExpirationDateText.Text = ElitaPlusPage.GetLongDate12FormattedString(CDate(State.MyBO.Expiration))
            Else
                moExpirationDateText.Text = String.Empty
            End If
        End Sub
#End Region

#Region "Page_Events"
        Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            Try
                ErrorCtrl.Clear_Hide()
                ValidateDates()

                If Not IsPostBack Then

                    'If Assurant.Elita.Configuration.ElitaConfig.Current.General.IntegrateWorkQueueImagingServices = False Then
                    '    ControlMgr.SetEnableTabStrip(Me, tsQuestions.Items(3), False)
                    'End If
                    AddCalendarwithTime(imgEffectiveDate, moEffectiveDateText)
                    AddCalendarwithTime(imgExpirationDate, moExpirationDateText)

                    If State.MyBO Is Nothing Then
                        State.MyBO = New Issue
                        State.MyBO.ActiveOn = System.DateTime.Now()
                    Else
                        EnableHeaderControls(False)
                    End If

                    PopulateDropdown()

                    ErrorCtrl.Clear_Hide()
                    If Not IsPostBack Then
                        AddControlMsg(btnDelete_WRITE, Message.DELETE_RECORD_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, True)
                        If State.MyBO Is Nothing Then
                            State.MyBO = New Issue
                        End If
                        UC_QUEUE_AVASEL.BackColor = "#d5d6e4"
                        PopulateChildern()
                        PopulateFormFromBOs()
                        EnableDisableFields()
                    End If
                    ControlMgr.SetVisibleControl(Me, trPageSize, False)
                    RestoreGuiState()
                    PopulateWorkQueue()
                    If State.IsEditMode Then
                        EnableDisableNotesButtons(False)
                    End If
                Else
                    SaveGuiState()
                End If
                CheckIfComingFromSaveConfirm()
                BindBoPropertiesToLabels()
                AddLabelDecorations(State.MyBO)
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try
            ShowMissingTranslations(ErrorCtrl)
        End Sub
#End Region

#Region "Button Clicks"

        Private Sub btnBack_Click(sender As System.Object, e As System.EventArgs) Handles btnBack.Click
            Try

                PopulateBOsFormFrom()
                If State.MyBO.IsDirty Then
                    DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO_CANCEL, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                    State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
                Else
                    ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, State.MyBO, State.HasDataChanged))
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
                DisplayMessage(Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR, "", MSG_BTN_YES_NO_CANCEL, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.BackOnErr
                State.LastErrMsg = ErrorCtrl.Text
            End Try
        End Sub

        Private Sub btnSave_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnSave_WRITE.Click
            Try
                IsBackdated()
                PopulateBOsFormFrom()
                IsDateValidated()
                EditModeValidation()
                '#5 - Expiration date lies between Effective and Expiration of earlier list code and there is no future list code available
                If (IsListCodeOverlapped(State.Code, State.EffectiveDate, State.ExpirationDate, State.MyBO.Id)) Then
                    DisplayMessage(Message.MSG_EXPIRE_PREVIOUS_ISSUE, "", MSG_BTN_YES_NO_CANCEL, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                    State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Accept
                    Return
                End If

                '#6 - Expiration/Expiration date lies between any other Effective and Expiration range of earlier list code
                If (IsListCodeDurationOverlapped(State.Code, State.EffectiveDate, State.ExpirationDate, State.MyBO.Id)) Then
                    Throw New GUIException(Message.MSG_GUI_INVALID_EFFECTIVE_DATE_SMALLER_THAN_SYSDATE, Assurant.ElitaPlus.Common.ErrorCodes.EQUIPMENT_LIST_CODE_OVERLAPPED)
                End If

                If State.MyBO.IsDirty Then
                    State.MyBO.Save()
                    State.HasDataChanged = True
                    PopulateFormFromBOs()
                    EnableDisableFields()
                    EnableHeaderControls(False)
                    If Not State.IsChildCreated Then
                        EnableDisableUserControlTab(PanelQuestionsEditDetail, True)
                        EnableDisableUserControlTab(PanelRulesEditDetail, True)
                        EnableDisableUserControlTab(PanelNotesEditDetail, True)
                    End If
                    DisplayMessage(Message.SAVE_RECORD_CONFIRMATION, "", MSG_BTN_OK, MSG_TYPE_INFO)
                Else
                    DisplayMessage(Message.MSG_RECORD_NOT_SAVED, "", MSG_BTN_OK, MSG_TYPE_INFO)
                End If

            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try
        End Sub

        Private Sub btnUndo_Write_Click(sender As System.Object, e As System.EventArgs) Handles btnUndo_Write.Click
            Try
                If Not State.MyBO.IsNew Then
                    'Reload from the DB
                    State.MyBO = New Issue(State.MyBO.Id)
                ElseIf State.ScreenSnapShotBO IsNot Nothing Then
                    'It was a new with copy
                    State.MyBO.Clone(State.ScreenSnapShotBO)
                Else
                    State.MyBO = New Issue
                End If
                PopulateFormFromBOs()
                EnableDisableFields()
                EnableDisableUserControlTab(PanelQuestionsEditDetail, False)
                EnableDisableUserControlTab(PanelRulesEditDetail, False)
                EnableDisableUserControlTab(PanelNotesEditDetail, False)
                EnableUserControl(True)
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try
        End Sub

        Private Sub btnDelete_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnDelete_WRITE.Click
            Try
                If (State.MyBO.CheckIfIssueIsAssignedToQuestionNoteOrRule(State.MyBO.Id)) Then
                    Throw New GUIException(Message.MSG_GUI_LIST_CODE_ASSIGNED_TO_DEALER_NO_DELETE, Assurant.ElitaPlus.Common.ErrorCodes.MSG_GUI_ISSUE_IS_ASSIGNED_TO_QUESTION_OR_RULE_OR_NOTE)
                Else
                    PopulateChildern()
                    State.MyBO.Delete()
                    State.MyBO.Save()
                    State.HasDataChanged = True
                    ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Delete, State.MyBO, State.HasDataChanged))
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                'undo the delete
                State.MyBO.RejectChanges()
                HandleErrors(ex, ErrorCtrl)
            End Try
        End Sub

        Private Sub btnNew_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnNew_WRITE.Click
            Try
                PopulateBOsFormFrom()
                If State.MyBO.IsDirty Then
                    DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO_CANCEL, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                    State.ActionInProgress = ElitaPlusPage.DetailPageCommand.New_
                Else
                    CreateNew()
                    ClearGridHeadersAndLabelsErrSign()
                End If
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try
        End Sub

        Private Sub btnCopy_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnCopy_WRITE.Click
            Try
                If State.MyBO.IsDirty Then
                    DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO_CANCEL, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                    State.ActionInProgress = ElitaPlusPage.DetailPageCommand.NewAndCopy
                Else
                    CreateNewWithCopy()
                End If
                PopulateBOsFormFrom()
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try
        End Sub

        Private Sub btnNew_Comment_Click(sender As System.Object, e As System.EventArgs) Handles btnNew_Comment.Click

            Try
                State.IsEditMode = True
                State.IsGridVisible = True
                State.AddingNewRow = True
                AddNew()
                EnableDisableFields()
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try

        End Sub

        Private Sub btnCancel_Comment_Click(sender As Object, e As System.EventArgs) Handles btnCancel_Comment.Click

            Try
                SetGridControls(GVNotes, False)
                EndNotesChildEdit(ElitaPlusPage.DetailPageCommand.Cancel)
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try

        End Sub

        Private Sub btnSave_Comment_Click(sender As Object, e As System.EventArgs) Handles btnSave_Comment.Click
            Try
                PopulateCommentBOFromForm()
                EndNotesChildEdit(ElitaPlusPage.DetailPageCommand.OK)
                State.IsNotesEditing = False
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try
        End Sub

#End Region


#Region "Controlling Logic"

        Sub IsCodeDuplicate()

        End Sub

        Public Sub PopulateDropdown()
            If moAsOfDateText.Text = String.Empty Then
                moAsOfDateText.Text = System.DateTime.Now().ToString("dd-MMM-yyyy")
            End If

            'Me.BindListControlToDataView(Me.cboIssueTypeText, LookupListNew.GetIssueTypeLookupList(Authentication.LangId, True), , , True)

            Dim IssueType As DataElements.ListItem() =
                CommonConfigManager.Current.ListManager.GetList(listCode:=ListCodes.IssueTypeList,
                                                                languageCode:=Thread.CurrentPrincipal.GetLanguageCode())

            cboIssueTypeText.Populate(IssueType.ToArray(),
                                       New PopulateOptions() With
                                        {
                                            .AddBlankItem = True
                                        })

            State.NoteDV = LookupListNew.GetIssueCommentTypeLookupList(Authentication.LangId)

            Dim PopulateOption = New PopulateOptions() With
                                    {
                                        .AddBlankItem = True,
                                        .BlankItemValue = String.Empty,
                                        .TextFunc = AddressOf .GetDescription,
                                        .ValueFunc = AddressOf .GetExtendedCode,
                                        .SortFunc = AddressOf .GetDescription
                                    }

            'cboIssueProcessor.PopulateOld("ISSPRO", ListValueType.Description, ListValueType.ExtendedCode, PopulateBehavior.AddBlankListItem, String.Empty, ListValueType.Description)

            Dim IssueProcessor As DataElements.ListItem() =
                CommonConfigManager.Current.ListManager.GetList(listCode:="ISSPRO",
                                                                languageCode:=Thread.CurrentPrincipal.GetLanguageCode())

            cboIssueProcessor.Populate(IssueProcessor, PopulateOption)

            'cboClaimType.PopulateOld("SP_CLAIM_CODE", ListValueType.Description, ListValueType.ExtendedCode, PopulateBehavior.AddBlankListItem, String.Empty, ListValueType.Description)

            Dim ClaimType As DataElements.ListItem() =
                CommonConfigManager.Current.ListManager.GetList(listCode:="SP_CLAIM_CODE",
                                                                languageCode:=Thread.CurrentPrincipal.GetLanguageCode())

            cboClaimType.Populate(ClaimType, PopulateOption)

            'cboClaimDeniedRsn.PopulateOld("DNDREASON", ListValueType.Description, ListValueType.ExtendedCode, PopulateBehavior.AddBlankListItem, String.Empty, ListValueType.Description)


            'KDDI CHANGES
            'Dim listcontextForMgList As ListContext = New ListContext()
            'listcontextForMgList.CompanyGroupId = Guid.Empty
            'listcontextForMgList.DealerId = Guid.Empty
            'listcontextForMgList.CompanyId = Guid.Empty
            'listcontextForMgList.DealerGroupId = Guid.Empty
            'Dim ClaimDeniedRsn As DataElements.ListItem() =
            '    CommonConfigManager.Current.ListManager.GetList(listCode:="DNDREASON",
            '                                                    languageCode:=Thread.CurrentPrincipal.GetLanguageCode(), context:=listcontextForMgList)
            Dim ClaimDeniedRsn As DataElements.ListItem() =
                CommonConfigManager.Current.ListManager.GetList(listCode:="DNDREASON",
                                                                languageCode:=Thread.CurrentPrincipal.GetLanguageCode())

            cboClaimDeniedRsn.Populate(ClaimDeniedRsn, PopulateOption)
        End Sub

        Sub IsBackdated()

            '#1 - Restrict to save backdated list in edit mode
            If State.EffectiveDate IsNot Nothing AndAlso State.MyBO.IsNew = False Then
                If DateHelper.GetDateValue(State.MyBO.Effective.ToString) <> DateHelper.GetDateValue(moEffectiveDateText.Text.ToString) Then
                    If DateHelper.GetDateValue(moEffectiveDateText.Text.ToString) < EquipmentListDetail.GetCurrentDateTime() Then
                        Throw New GUIException(Message.MSG_GUI_INVALID_EFFECTIVE_DATE_SMALLER_THAN_SYSDATE, Assurant.ElitaPlus.Common.ErrorCodes.EQUIPMENT_INVALID_EFFECIVE_DATE)
                    End If
                End If
            End If

            '#2 - Restrict to save backdated list in edit mode
            If State.ExpirationDate IsNot Nothing AndAlso State.MyBO.IsNew = False Then
                If DateHelper.GetDateValue(State.MyBO.Expiration.ToString) <> DateHelper.GetDateValue(moExpirationDateText.Text.ToString) Then
                    If (State.MyBO.CheckIfIssueIsAssignedToQuestionNoteOrRule(State.MyBO.Id)) Then
                        If DateHelper.GetDateValue(State.ExpirationDate.ToString) < EquipmentListDetail.GetCurrentDateTime() Then
                            Throw New GUIException(Message.MSG_GUI_INVALID_EXPIRATION_DATE_SMALLER_THAN_SYSDATE, Assurant.ElitaPlus.Common.ErrorCodes.EQUIPMENT_INVALID_EXPIRATION_DATE_LESSTHAN_SYSDATE)
                        End If
                    End If
                End If
            End If

        End Sub

        Sub IsDateValidated()

            '#3 - Effective date should be greater than Expiration Date
            If State.EffectiveDate IsNot Nothing AndAlso State.ExpirationDate IsNot Nothing Then
                If DateHelper.GetDateValue(State.EffectiveDate.ToString) > DateHelper.GetDateValue(State.ExpirationDate.ToString) Then
                    Throw New GUIException(Message.MSG_GUI_INVALID_EFFECTIVE_HIGHER_EXPIRATION_DATE, Assurant.ElitaPlus.Common.ErrorCodes.EQUIPMENT_INVALID_EXPIRATION_DATE)
                End If
            End If

        End Sub

        Sub EditModeValidation()
            '#4 - For new records, check for no backdated List code and no duplicate List code - Effective Date Combination
            If Not State.IsEditMode Then
                If State.EffectiveDate IsNot Nothing AndAlso State.ExpirationDate IsNot Nothing Then
                    If DateHelper.GetDateValue(State.EffectiveDate.ToString) < EquipmentListDetail.GetCurrentDateTime().Today Then
                        Throw New GUIException(Message.MSG_GUI_INVALID_EFFECTIVE_DATE_SMALLER_THAN_SYSDATE, Assurant.ElitaPlus.Common.ErrorCodes.EQUIPMENT_INVALID_EFFECIVE_DATE)
                    End If
                End If

                If (IsListCodeDuplicate(State.Code, State.EffectiveDate.ToString, State.MyBO.Id)) Then
                    Throw New GUIException(Message.MSG_GUI_INVALID_LIST_CODE_WITH_SAME_EFFECIVE_DATE, Assurant.ElitaPlus.Common.ErrorCodes.EQUIPMENT_DUPLICATE_CODE_EFFECTIVE)
                End If
            End If
        End Sub

        Protected Function IsListCodeDuplicate(code As String, effective As String,
                                        id As Guid) As Boolean

            If (Issue.CheckDuplicateEquipmentListCode(code, DateHelper.GetDateValue(effective).ToString(ElitaPlusPage.DATE_TIME_FORMAT, System.Globalization.CultureInfo.CreateSpecificCulture("en-US")), id)) Then
                Return True
            End If
            Return False
        End Function

        Protected Function IsListCodeOverlapped(code As String, effective As DateType,
                                        expiration As DateType, id As Guid) As Boolean

            If (Issue.CheckListCodeForOverlap(code, effective, expiration, id)) Then
                Return True
            End If
            Return False
        End Function

        Protected Function IsListCodeDurationOverlapped(code As String, effective As DateType,
                                        expiration As DateType, listId As Guid) As Boolean

            If (Issue.CheckListCodeDurationOverlap(code, effective, expiration, listId)) Then
                Return True
            End If
            Return False
        End Function

        Protected Function ExpirePreviousList(code As String, effective As DateType,
                                        expiration As DateType, listId As Guid) As Boolean

            If (Issue.ExpirePreviousList(code, effective, expiration, listId)) Then
                Return True
            End If
            Return False
        End Function

        Protected Sub EnableUserControl(bVisible As Boolean)
            UserControlQuestionsAvailable.ShowCancelButton = True
            UserControlRulesAvailable.ShowCancelButton = True
            UserControlQuestionsAvailable.dvSelectedQuestions = Issue.GetSelectedQuestionsList(State.MyBO.Id)
            UserControlRulesAvailable.dvSelectedDealer = Issue.GetSelectedRulesList(State.MyBO.Id)
        End Sub

        Public Sub ValidateDates()
            Dim tempDate As DateTime = New DateTime

            If moEffectiveDateText.Text IsNot String.Empty Then
                If (DateHelper.IsDate(moEffectiveDateText.Text.ToString()) = False) Then
                    ElitaPlusPage.SetLabelError(moEffectiveDateLabel)
                    Throw New GUIException(Message.MSG_BEGIN_END_DATE, Message.MSG_INVALID_DATE)
                Else
                    tempDate = DateHelper.GetDateValue(moEffectiveDateText.Text.ToString())
                End If
            End If


            If moExpirationDateText.Text IsNot String.Empty Then
                If (DateHelper.IsDate(moExpirationDateText.Text.ToString()) = False) Then
                    ElitaPlusPage.SetLabelError(moExpirationDateLabel)
                    Throw New GUIException(Message.MSG_BEGIN_END_DATE, Message.MSG_INVALID_DATE)
                Else
                    tempDate = DateHelper.GetDateValue(moExpirationDateText.Text.ToString())
                End If
            End If
        
        End Sub

        Protected Sub BindBoPropertiesToLabels()
            BindBOPropertyToLabel(State.MyBO, "Code", moCodeLabel)
            BindBOPropertyToLabel(State.MyBO, "Description", moDescriptionLabel)
            BindBOPropertyToLabel(State.MyBO, "Effective", moEffectiveDateLabel)
            BindBOPropertyToLabel(State.MyBO, "Expiration", moExpirationDateLabel)
            BindBOPropertyToLabel(State.MyBO, "IssueTypeId", moIssueTypeLabel)
            BindBOPropertyToLabel(State.MyBO, "ActiveOn", moAsOfDateLabel)
            BindBOPropertyToLabel(State.MyBO, "PreConditions", moPreConditionsLabel)
            BindBOPropertyToLabel(State.MyBO, "IssueProcessor", moIssueProcessorLabel)
            BindBOPropertyToLabel(State.MyBO, "DeniedReason", moClaimDeniedRsnLabel)
            BindBOPropertyToLabel(State.MyBO, "SPClaimValue", moClaimValueLabel)
            BindBOPropertyToLabel(State.MyBO, "SPClaimType", moClaimTypeLabel)
        End Sub

        Protected Sub PopulateBOsFormFrom()
            With State.MyBO
                PopulateBOProperty(State.MyBO, "Code", moCodeText)
                PopulateBOProperty(State.MyBO, "Description", moDescriptionText)
                PopulateBOProperty(State.MyBO, "PreConditions", moPreConditionsTextBox)
                PopulateBOProperty(State.MyBO, "Effective", moEffectiveDateText)
                PopulateBOProperty(State.MyBO, "Expiration", moExpirationDateText)
                PopulateBOProperty(State.MyBO, "IssueTypeId", cboIssueTypeText)
                PopulateBOProperty(State.MyBO, "ActiveOn", moAsOfDateText)
                PopulateBOProperty(State.MyBO, "IssueProcessor", cboIssueProcessor, False, True)
                PopulateBOProperty(State.MyBO, "DeniedReason", cboClaimDeniedRsn, False, True)
                PopulateBOProperty(State.MyBO, "SPClaimValue", moClaimValueTextBox)
                PopulateBOProperty(State.MyBO, "SPClaimType", cboClaimType, False, True)
                If State.MyBO.IssueProcessor = String.Empty Then
                    State.MyBO.IssueProcessor = Nothing
                End If
                If State.MyBO.SPClaimType = String.Empty Then
                    State.MyBO.SPClaimType = Nothing
                End If
            End With
            If ErrCollection.Count > 0 Then
                Throw New PopulateBOErrorException
            End If
        End Sub

        Protected Sub PopulateChildern()
            Dim CommentChildren As IssueNotesChildrenList = State.MyBO.IssueNotesChildren
            Dim QuestionChildren As IssueQuestionsChildrenList = State.MyBO.IssueQuestionsChildren
            Dim RuleChildren As IssueRulesChildrenList = State.MyBO.IssueRulesChildren
        End Sub

        Protected Sub PopulateFormFromBOs()
            PopulateGrid()
            With State.MyBO
                PopulateControlFromBOProperty(moCodeText, .Code)
                PopulateControlFromBOProperty(moDescriptionText, .Description)
                PopulateControlFromBOProperty(moPreConditionsTextBox, .PreConditions)
                If State.MyBO.Effective IsNot Nothing Then
                    moEffectiveDateText.Text = ElitaPlusPage.GetLongDate12FormattedString(CDate(State.MyBO.Effective))
                Else
                    moEffectiveDateText.Text = String.Empty
                End If
                If State.MyBO.Expiration IsNot Nothing Then
                    moExpirationDateText.Text = ElitaPlusPage.GetLongDate12FormattedString(CDate(State.MyBO.Expiration))
                Else
                    moExpirationDateText.Text = String.Empty
                End If
                PopulateControlFromBOProperty(moAsOfDateText, .ActiveOn)
                SetSelectedItem(cboIssueTypeText, .IssueTypeId)
                BindSelectItem(.SPClaimType, cboClaimType)
                BindSelectItem(.IssueProcessor, cboIssueProcessor)
                BindSelectItem(.DeniedReason, cboClaimDeniedRsn)
                PopulateControlFromBOProperty(moClaimValueTextBox, .SPClaimValue)
            End With

        End Sub
        Private Sub cboissueprocessor_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboIssueProcessor.SelectedIndexChanged
            If (GetSelectedValue(cboIssueProcessor) = Codes.ISSUE_PROCESSOR__CUST OrElse GetSelectedValue(cboIssueProcessor) = String.Empty) Then
                ControlMgr.SetVisibleControl(Me, moClaimTypeLabel, False)
                ControlMgr.SetVisibleControl(Me, moClaimValueLabel, False)
                ControlMgr.SetVisibleControl(Me, moClaimValueTextBox, False)
                ControlMgr.SetVisibleControl(Me, cboClaimType, False)
                moClaimValueTextBox.Text = Nothing
                cboClaimType.ClearSelection()
            Else
                ControlMgr.SetVisibleControl(Me, moClaimTypeLabel, True)
                ControlMgr.SetVisibleControl(Me, moClaimValueLabel, True)
                ControlMgr.SetVisibleControl(Me, moClaimValueTextBox, True)
                ControlMgr.SetVisibleControl(Me, cboClaimType, True)
            End If
        End Sub

        Sub EnableHeaderControls(enableToggle As Boolean)
            ControlMgr.SetEnableControl(Me, moCodeText, enableToggle)
            If (State.MyBO.CheckIfIssueIsAssignedToQuestionNoteOrRule(State.MyBO.Id)) Then
                ControlMgr.SetEnableControl(Me, moDescriptionText, enableToggle)
                ControlMgr.SetEnableControl(Me, moEffectiveDateText, enableToggle)
            Else
                ControlMgr.SetEnableControl(Me, moDescriptionText, Not (enableToggle))
                ControlMgr.SetEnableControl(Me, moEffectiveDateText, Not (enableToggle))
            End If
        End Sub

        Sub EnableDisableParentControls(enableToggle As Boolean)
            ControlMgr.SetEnableControl(Me, btnCopy_WRITE, enableToggle)
            ControlMgr.SetEnableControl(Me, btnDelete_WRITE, enableToggle)
            ControlMgr.SetEnableControl(Me, btnSave_WRITE, enableToggle)
            ControlMgr.SetEnableControl(Me, btnUndo_Write, enableToggle)
            ControlMgr.SetEnableControl(Me, btnNew_WRITE, enableToggle)
            ControlMgr.SetEnableControl(Me, btnBack, enableToggle)
        End Sub

        Sub EnableDisableNotesButtons(enableToggle As Boolean)
            ControlMgr.SetEnableControl(Me, btnNew_Comment, Not enableToggle)
            ControlMgr.SetEnableControl(Me, btnSave_Comment, enableToggle)
            ControlMgr.SetEnableControl(Me, btnCancel_Comment, enableToggle)
        End Sub

        Sub EnableDisableUserControlTab(panel As WebControls.Panel, enableToggle As Boolean)
            ControlMgr.SetEnableControl(Me, panel, enableToggle)
        End Sub

        Protected Sub EnableDisableFields()
            If State.IsNotesEditing Then
                EnableHeaderControls(False)
                EnableDisableParentControls(False)
                EnableUserControl(True)
                EnableDisableNotesButtons(True)
            ElseIf State.IsQuestionsEditing Then
            ElseIf State.IsRulesEditing Then
            Else
                EnableDisableParentControls(True)
                EnableUserControl(False)
            End If

            'ControlMgr.SetEnableControl(Me, btnDelete_WRITE, True)
            'ControlMgr.SetEnableControl(Me, btnNew_WRITE, True)
            'ControlMgr.SetEnableControl(Me, btnCopy_WRITE, True)

            If State.MyBO.IsNew Then
                ControlMgr.SetEnableControl(Me, moCodeText, True)
                ControlMgr.SetEnableControl(Me, moDescriptionText, True)
                ControlMgr.SetEnableControl(Me, moEffectiveDateText, True)

                ControlMgr.SetEnableControl(Me, btnDelete_WRITE, False)
                ControlMgr.SetEnableControl(Me, btnNew_WRITE, False)
                ControlMgr.SetEnableControl(Me, btnCopy_WRITE, False)
                EnableDisableUserControlTab(PanelQuestionsEditDetail, False)
                EnableDisableUserControlTab(PanelRulesEditDetail, False)
                EnableDisableUserControlTab(PanelNotesEditDetail, False)

                EnableDisableNotesButtons(False)
            End If
            If Assurant.Elita.Configuration.ElitaConfig.Current.General.IntegrateWorkQueueImagingServices = False Then
                EnableDisableUserControlTab(plnQueue, False)
            End If

            If (GetSelectedValue(cboIssueProcessor) = Codes.ISSUE_PROCESSOR__CUST OrElse GetSelectedValue(cboIssueProcessor) = String.Empty) Then
                ControlMgr.SetVisibleControl(Me, moClaimTypeLabel, False)
                ControlMgr.SetVisibleControl(Me, moClaimValueLabel, False)
                ControlMgr.SetVisibleControl(Me, moClaimValueTextBox, False)
                ControlMgr.SetVisibleControl(Me, cboClaimType, False)
                moClaimValueTextBox.Text = Nothing
                cboClaimType.ClearSelection()
            Else
                ControlMgr.SetVisibleControl(Me, moClaimTypeLabel, True)
                ControlMgr.SetVisibleControl(Me, moClaimValueLabel, True)
                ControlMgr.SetVisibleControl(Me, moClaimValueTextBox, True)
                ControlMgr.SetVisibleControl(Me, cboClaimType, True)
            End If
        End Sub

        Protected Sub CreateNew()
            State.ScreenSnapShotBO = Nothing 'Reset the backup copy
            State.MyBO = New Issue
            PopulateFormFromBOs()
            EnableDisableFields()
            State.IsEditMode = False
        End Sub

        Protected Sub CreateNewWithCopy()
            State.IsACopy = True
            Dim newObj As New Issue
            newObj.Copy(State.MyBO)

            State.MyBO = newObj
            State.MyBO.Effective = EquipmentListDetail.GetCurrentDateTime()
            State.MyBO.Expiration = New DateTime(2499, 12, 31, 23, 59, 59)
            State.MyBO.Code = Nothing
            State.MyBO.Description = Nothing
            PopulateFormFromBOs()
            EnableDisableFields()
            State.ScreenSnapShotBO = New Issue
            State.ScreenSnapShotBO.Clone(State.MyBO)
            State.IsACopy = False
            State.IsEditMode = False
        End Sub

        Protected Sub CheckIfComingFromSaveConfirm()
            Dim confResponse As String = HiddenSaveChangesPromptResponse.Value
            If confResponse IsNot Nothing AndAlso confResponse = MSG_VALUE_YES Then
                If State.ActionInProgress <> ElitaPlusPage.DetailPageCommand.BackOnErr AndAlso State.ActionInProgress <> ElitaPlusPage.DetailPageCommand.Accept Then
                    BindBoPropertiesToLabels()
                    IsBackdated()
                    PopulateBOsFormFrom()
                    IsDateValidated()
                    EditModeValidation()
                    PopulateWorkQueue()
                    '#5 - Expiration date lies between Effective and Expiration of earlier list code and there is no future list code available
                    If (IsListCodeOverlapped(State.Code, State.EffectiveDate, State.ExpirationDate, State.MyBO.Id)) Then
                        DisplayMessage(Message.MSG_EXPIRE_PREVIOUS_ISSUE, "", MSG_BTN_YES_NO_CANCEL, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                        State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Accept
                        Return
                    End If

                    '#6 - Expiration/Expiration date lies between any other Effective and Expiration range of earlier list code
                    If (IsListCodeDurationOverlapped(State.Code, State.EffectiveDate, State.ExpirationDate, State.MyBO.Id)) Then
                        Throw New GUIException(Message.MSG_GUI_INVALID_EFFECTIVE_DATE_SMALLER_THAN_SYSDATE, Assurant.ElitaPlus.Common.ErrorCodes.EQUIPMENT_LIST_CODE_OVERLAPPED)
                    End If

                    State.MyBO.Save()
                End If
                Select Case State.ActionInProgress
                    Case ElitaPlusPage.DetailPageCommand.Back
                        ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, State.MyBO, State.HasDataChanged))
                    Case ElitaPlusPage.DetailPageCommand.New_
                        DisplayMessage(Message.SAVE_RECORD_CONFIRMATION, "", MSG_BTN_OK, MSG_TYPE_INFO)
                        CreateNew()
                    Case ElitaPlusPage.DetailPageCommand.NewAndCopy
                        DisplayMessage(Message.SAVE_RECORD_CONFIRMATION, "", MSG_BTN_OK, MSG_TYPE_INFO)
                        CreateNewWithCopy()
                    Case ElitaPlusPage.DetailPageCommand.BackOnErr
                        ReturnToCallingPage(New ReturnType(State.ActionInProgress, State.MyBO, State.HasDataChanged))
                    Case ElitaPlusPage.DetailPageCommand.Accept
                        If State.MyBO.IsDirty Then
                            State.MyBO.Save()
                            Issue.ExpirePreviousList(State.Code, State.EffectiveDate, State.ExpirationDate, State.MyBO.Id)
                            State.HasDataChanged = True
                            PopulateFormFromBOs()
                            EnableDisableFields()
                            EnableDisableUserControlTab(PanelQuestionsEditDetail, True)
                            EnableDisableUserControlTab(PanelRulesEditDetail, True)
                            EnableHeaderControls(False)
                            PopulateWorkQueue()
                            DisplayMessage(Message.SAVE_RECORD_CONFIRMATION, "", MSG_BTN_OK, MSG_TYPE_INFO)
                        Else
                            DisplayMessage(Message.MSG_RECORD_NOT_SAVED, "", MSG_BTN_OK, MSG_TYPE_INFO)
                        End If
                End Select
            ElseIf confResponse IsNot Nothing AndAlso confResponse = MSG_VALUE_NO Then
                Select Case State.ActionInProgress
                    Case ElitaPlusPage.DetailPageCommand.Back
                        ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, State.MyBO, State.HasDataChanged))
                    Case ElitaPlusPage.DetailPageCommand.New_
                        CreateNew()
                    Case ElitaPlusPage.DetailPageCommand.NewAndCopy
                        CreateNewWithCopy()
                    Case ElitaPlusPage.DetailPageCommand.BackOnErr
                        ErrorCtrl.AddErrorAndShow(State.LastErrMsg)
                    Case ElitaPlusPage.DetailPageCommand.Accept
                        EnableDisableFields()
                End Select
            End If
            State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
            HiddenSaveChangesPromptResponse.Value = ""
        End Sub

        Private Sub AddNew()
            Dim dv As DataView

            BeginNotesChildEdit(Guid.Empty, False)
            State.MyNotesChildBO.IssueId = State.MyBO.Id
            State.MyNotesChildBO.IssueCommentTypeId = Guid.Empty
            State.MyNotesChildBO.Code = String.Empty
            State.MyNotesChildBO.Text = String.Empty
            State.MyNotesChildBO.DisplayOnWeb = LookupListNew.GetIdFromCode(LookupListNew.LK_LANG_INDEPENDENT_YES_NO, Codes.YESNO_Y)
            State.SearchDV = State.MyBO.GetNotesSelectionView()

            GVNotes.DataSource = State.SearchDV
            GVNotes.AutoGenerateColumns = False

            SetPageAndSelectedIndexFromGuid(State.SearchDV, State.MyNotesChildBO.Id, GVNotes, State.PageIndex, State.IsEditMode)
            GVNotes.DataBind()
            State.PageIndex = GVNotes.CurrentPageIndex

            SetPageAndSelectedIndexFromGuid(State.SearchDV, State.MyNotesChildBO.Id, GVNotes, GVNotes.CurrentPageIndex, True)
            SetGridControls(GVNotes, False)
            EnableDisableFields()
            ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me, GVNotes)
        End Sub

#End Region

#Region "Child_Control"



        Sub BeginNotesChildEdit(IssueCommentId As Guid, expireNow As Boolean)
            State.IsNotesEditing = True
            State.SelectedNotesChildId = IssueCommentId
            With State
                If Not .SelectedNotesChildId = Guid.Empty Then
                    .MyNotesChildBO = .MyBO.GetNotesChild(.SelectedNotesChildId)
                Else
                    .MyNotesChildBO = .MyBO.GetNewNotesChild
                End If
                .MyNotesChildBO.BeginEdit()
            End With
        End Sub

        Sub BeginQuestionsChildEdit(SoftQuestionId As Guid, expireNow As Boolean, DisplayOrder As Integer)
            State.IsQuestionsEditing = True
            State.SelectedQuestionsChildId = Guid.Empty
            State.SelectedQuestionsChildId = New Guid(State.MyQuestionsChildBO.IsChild(State.MyBO.Id, SoftQuestionId))
            With State
                If Not .SelectedQuestionsChildId = Guid.Empty Then
                    .MyQuestionsChildBO = .MyBO.GetQuestionsChild(.SelectedQuestionsChildId)
                Else
                    .MyQuestionsChildBO = .MyBO.GetNewQuestionsChild
                End If
                .MyQuestionsChildBO.BeginEdit()
                .MyQuestionsChildBO.DisplayOrder = DisplayOrder

                If expireNow Then
                    SetQuestionsExpiration(SoftQuestionId)
                Else
                    PopulateQuestionChildBOFrom(SoftQuestionId)
                End If
            End With
        End Sub

        Sub BeginRulesChildEdit(RuleIssueId As Guid, expireNow As Boolean)
            State.IsRulesEditing = True
            State.SelectedRulesChildId = Guid.Empty
            State.SelectedRulesChildId = New Guid(State.MyRulesChildBO.IsChild(State.MyBO.Id, RuleIssueId))
            With State
                If Not .SelectedRulesChildId = Guid.Empty Then
                    .MyRulesChildBO = .MyBO.GetRulesChild(.SelectedRulesChildId)
                Else
                    .MyRulesChildBO = .MyBO.GetNewRulesChild
                End If
                .MyRulesChildBO.BeginEdit()
                'Def-26369:Added condition to set rules expiration.
                If expireNow Then
                    SetRulesExpiration(RuleIssueId)
                Else
                    PopulateRuleChildBOFrom(RuleIssueId)
                End If
            End With
        End Sub

        ''' <summary>
        ''' Update Child Equipment BO with exact Effective and Expiraiton
        ''' #1 : Update Equipment Id
        ''' #2 : Set earliest available expiration date 
        ''' #3 : Set Effective date if Equipment added was earlier existing equipment
        ''' </summary>
        ''' <param name="equipmentId">Equipment GUID</param>
        ''' <remarks></remarks>

        Sub PopulateQuestionChildBOFrom(SoftQuestionId As Guid)
            Dim NewQuestionExpiration As DateTime
            Dim SelectedQuestionExpiration As DateTime
            Dim QuestionOldExpiraitonDate As DateTime

            With State.MyQuestionsChildBO
                SelectedQuestionExpiration = Issue.GetQuestionExpiration(State.MyBO.Id, SoftQuestionId)
                QuestionOldExpiraitonDate = CDate("#" & .Expiration.ToString & "#")

                If State.MyBO.Expiration IsNot Nothing Then
                    NewQuestionExpiration = CDate("#" & State.MyBO.Expiration.ToString & "#")
                End If
                ''#1 
                .IssueId = State.MyBO.Id
                .SoftQuestionId = SoftQuestionId
                ''#2
                If Not SelectedQuestionExpiration = Nothing AndAlso SelectedQuestionExpiration < NewQuestionExpiration Then
                    .Expiration = SelectedQuestionExpiration
                Else
                    .Expiration = NewQuestionExpiration
                End If
                ''#3
                If QuestionOldExpiraitonDate < EquipmentListDetail.GetCurrentDateTime() Then
                    .Effective = EquipmentListDetail.GetCurrentDateTime()
                End If
            End With
            If ErrCollection.Count > 0 Then
                Throw New PopulateBOErrorException
            End If
        End Sub

        Sub PopulateRuleChildBOFrom(RuleId As Guid)
            Dim NewRuleExpiration As DateTime
            Dim SelectedRuleExpiration As DateTime
            Dim RuleOldExpiraitonDate As DateTime

            With State.MyRulesChildBO
                SelectedRuleExpiration = Issue.GetRuleExpiration(State.MyBO.Id, RuleId)
                RuleOldExpiraitonDate = CDate("#" & .Expiration.ToString & "#")

                If State.MyBO.Expiration IsNot Nothing Then
                    NewRuleExpiration = CDate("#" & State.MyBO.Expiration.ToString & "#")
                End If
                ''#1 
                .IssueId = State.MyBO.Id
                .RuleId = RuleId
                ''#2
                If Not SelectedRuleExpiration = Nothing AndAlso SelectedRuleExpiration < NewRuleExpiration Then
                    .Expiration = SelectedRuleExpiration
                Else
                    .Expiration = NewRuleExpiration
                End If
                ''#3
                If RuleOldExpiraitonDate < EquipmentListDetail.GetCurrentDateTime() Then
                    .Effective = EquipmentListDetail.GetCurrentDateTime()
                End If
            End With
            If ErrCollection.Count > 0 Then
                Throw New PopulateBOErrorException
            End If
        End Sub

        Sub SetQuestionsExpiration(IssueQuestionId As Guid)
            With State.MyQuestionsChildBO
                State.MyQuestionsChildBO.Expiration = EquipmentListDetail.GetCurrentDateTime().AddSeconds(-1)
            End With
            If ErrCollection.Count > 0 Then
                Throw New PopulateBOErrorException
            End If
        End Sub

        Sub SetRulesExpiration(RuleIssueId As Guid)
            With State.MyRulesChildBO
                State.MyRulesChildBO.Expiration = EquipmentListDetail.GetCurrentDateTime().AddSeconds(-1)
            End With
            If ErrCollection.Count > 0 Then
                Throw New PopulateBOErrorException
            End If
        End Sub

        Sub EndNotesChildEdit(lastop As ElitaPlusPage.DetailPageCommand)

            With State
                Select Case lastop
                    Case ElitaPlusPage.DetailPageCommand.OK
                        .MyNotesChildBO.Save()
                        .MyNotesChildBO.EndEdit()
                    Case ElitaPlusPage.DetailPageCommand.Cancel
                        .MyNotesChildBO.cancelEdit()
                        If .MyNotesChildBO.IsSaveNew Then
                            .MyNotesChildBO.Delete()
                            .MyNotesChildBO.Save()
                        End If
                    Case ElitaPlusPage.DetailPageCommand.Back
                        .MyNotesChildBO.cancelEdit()
                        If .MyNotesChildBO.IsSaveNew Then
                            .MyNotesChildBO.Delete()
                            .MyNotesChildBO.Save()
                        End If
                    Case ElitaPlusPage.DetailPageCommand.Delete
                        .MyNotesChildBO.Delete()
                        .MyNotesChildBO.Save()
                        .MyNotesChildBO.EndEdit()
                        .SelectedNotesChildId = Guid.Empty
                End Select
            End With
            State.IsNotesEditing = False
            EnableDisableFields()
            EnableDisableNotesButtons(False)
            PopulateGrid()

        End Sub

        Sub EndQuestionsChildEdit(lastop As ElitaPlusPage.DetailPageCommand)
            Try
                With State
                    Select Case lastop
                        Case ElitaPlusPage.DetailPageCommand.OK
                            .MyQuestionsChildBO.Save()
                            .MyQuestionsChildBO.EndEdit()
                        Case ElitaPlusPage.DetailPageCommand.Cancel
                            .MyQuestionsChildBO.cancelEdit()
                            If .MyQuestionsChildBO.IsSaveNew Then
                                .MyQuestionsChildBO.Delete()
                                .MyQuestionsChildBO.Save()
                            End If
                        Case ElitaPlusPage.DetailPageCommand.Back
                            .MyQuestionsChildBO.cancelEdit()
                            If .MyQuestionsChildBO.IsSaveNew Then
                                .MyQuestionsChildBO.Delete()
                                .MyQuestionsChildBO.Save()
                            End If
                        Case ElitaPlusPage.DetailPageCommand.Delete
                            .MyQuestionsChildBO.Delete()
                            .MyQuestionsChildBO.Save()
                            .MyQuestionsChildBO.EndEdit()
                            .SelectedQuestionsChildId = Guid.Empty
                    End Select
                End With
                State.IsQuestionsEditing = False
                EnableDisableFields()
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try
        End Sub

        Sub EndRulesChildEdit(lastop As ElitaPlusPage.DetailPageCommand)
            Try
                With State
                    Select Case lastop
                        Case ElitaPlusPage.DetailPageCommand.OK
                            .MyRulesChildBO.Save()
                            .MyRulesChildBO.EndEdit()
                        Case ElitaPlusPage.DetailPageCommand.Cancel
                            .MyRulesChildBO.cancelEdit()
                            If .MyRulesChildBO.IsSaveNew Then
                                .MyRulesChildBO.Delete()
                                .MyRulesChildBO.Save()
                            End If
                        Case ElitaPlusPage.DetailPageCommand.Back
                            .MyRulesChildBO.cancelEdit()
                            If .MyRulesChildBO.IsSaveNew Then
                                .MyRulesChildBO.Delete()
                                .MyRulesChildBO.Save()
                            End If
                        Case ElitaPlusPage.DetailPageCommand.Delete
                            .MyRulesChildBO.Delete()
                            .MyRulesChildBO.Save()
                            .MyRulesChildBO.EndEdit()
                            .SelectedRulesChildId = Guid.Empty
                    End Select
                End With
                State.IsRulesEditing = False
                EnableDisableFields()
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try
        End Sub
        'Def-26680:Added try catch block to display message when work queue details not found.
        Sub PopulateWorkQueue()
            Try
                'populate available work queue
                Dim AvailableWQ As Issue.WorkQyueueSelectionView
                Dim SelectedWQ As Issue.WorkQyueueSelectionView

                AvailableWQ = Issue.GetAvailableWorkQueue()
                UC_QUEUE_AVASEL.SetAvailableData(AvailableWQ, "DESCRIPTION", "WORKQUEUE_ID")

                'populate selected work queue
                SelectedWQ = State.MyBO.GetWorkQyueueSelectionView()
                UC_QUEUE_AVASEL.SetSelectedData(SelectedWQ, "DESCRIPTION", "WORKQUEUE_ID")

                UC_QUEUE_AVASEL.RemoveSelectedFromAvailable()
            Catch ex As Exception
                Throw New GUIException(Message.MSG_WORK_QUEUE_NOT_FOUND,
               Message.MSG_WORK_QUEUE_NOT_FOUND)
            End Try
        End Sub

#End Region

#Region "User Control Event Handler"

#Region "Questions"

        Protected Sub ExecuteSearchFilter(sender As Object, args As SearchAvailableQuestionsEventArgs) Handles UserControlQuestionsAvailable.ExecuteSearchFilter
            Dim issues As New Issue
            Try
                If Not State.ActiveOn = Nothing Then
                    args.dvAvailableQuestions = issues.ExecuteQuestionsListFilter(args.Issue, args.QuestionList, args.SearchTags, State.MyBO.ActiveOn.ToString)
                End If
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try
        End Sub

        Protected Sub SaveClicked(sender As Object, args As SearchAvailableQuestionsEventArgs) Handles UserControlQuestionsAvailable.EventSaveQuestionsListDetail
            Dim oQuestionList As ArrayList
            Dim oDisplayOrder As Integer = 1
            Dim dictQuestions As Hashtable

            Try
                oQuestionList = New ArrayList()
                PopulateBOsFormFrom()
                dictQuestions = New Hashtable()
                For Each argQuestion As String In args.listSelectedQuestions
                    dictQuestions.Add(argQuestion, oDisplayOrder)
                    oDisplayOrder += 1
                Next

                oQuestionList = IssueQuestionList.GetQuestionList(State.MyBO.Id)
                For Each argQuestion As String In args.listSelectedQuestions
                    For Each questionRaw As Byte() In oQuestionList
                        If New Guid(questionRaw).ToString = argQuestion Then
                            oQuestionList.Remove(questionRaw)
                            Exit For
                        End If
                    Next
                    BeginQuestionsChildEdit(New Guid(argQuestion), False, CInt(dictQuestions.Item(argQuestion)))
                    EndQuestionsChildEdit(ElitaPlusPage.DetailPageCommand.OK)
                Next
                For Each Question As Byte() In oQuestionList
                    BeginQuestionsChildEdit(New Guid(Question), True, CInt(dictQuestions.Item(Question)))
                    EndQuestionsChildEdit(ElitaPlusPage.DetailPageCommand.OK)
                Next
                State.HasDataChanged = True
                State.IsQuestionsEditing = False
                EnableDisableFields()
                EnableDisableParentControls(True)
                EnableDisableUserControlTab(PanelQuestionsEditDetail, False)
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try
        End Sub

        Protected Sub CancelButtonClicked(sender As Object, args As SearchAvailableQuestionsEventArgs) Handles UserControlQuestionsAvailable.EventCancelButtonClicked
            Try
                UserControlQuestionsAvailable.dvSelectedQuestions = Issue.GetSelectedQuestionsList(State.MyBO.Id)
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try
        End Sub

#End Region

#Region "Rules"

        Protected Sub ExecuteDealerSearchFilter(sender As Object, args As SearchAvailableDealerEventArgs) Handles UserControlRulesAvailable.ExecuteDealerSearchFilter
            Dim issues As New Issue
            Try
                args.dvAvailableDealer = issues.ExecuteRulesListFilter(State.MyBO.Id)
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try
        End Sub

        Protected Sub SaveClicked(sender As Object, args As SearchAvailableDealerEventArgs) Handles UserControlRulesAvailable.EventSaveDealerListDetail
            Dim oRuleList As ArrayList

            Try
                oRuleList = New ArrayList()
                PopulateBOsFormFrom()

                oRuleList = RuleIssue.GetRulesInList(State.MyBO.Id)
                For Each argQuestion As String In args.listSelectedDealer
                    For Each questionRaw As Byte() In oRuleList
                        If New Guid(questionRaw).ToString = argQuestion Then
                            oRuleList.Remove(questionRaw)
                            Exit For
                        End If
                    Next
                    BeginRulesChildEdit(New Guid(argQuestion), False)
                    EndRulesChildEdit(ElitaPlusPage.DetailPageCommand.OK)
                Next
                For Each Question As Byte() In oRuleList
                    BeginRulesChildEdit(New Guid(Question), True)
                    EndRulesChildEdit(ElitaPlusPage.DetailPageCommand.OK)
                Next
                State.HasDataChanged = True
                State.IsRulesEditing = False
                EnableDisableFields()
                EnableDisableParentControls(True)
                EnableDisableUserControlTab(PanelRulesEditDetail, False)
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try
        End Sub

        Protected Sub CancelButtonClicked(sender As Object, args As SearchAvailableDealerEventArgs) Handles UserControlRulesAvailable.EventCancelButtonClicked
            Try
                Dim Issue As New Issue
                UserControlRulesAvailable.dvSelectedDealer = Issue.GetSelectedRulesList(State.MyBO.Id)
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try
        End Sub

#End Region

#End Region

#Region "Detail GVNotes Events"

#Region "Notes"

        Private Sub PopulateCommentBOFromForm()
            Try
                With State.MyNotesChildBO
                    .IssueCommentTypeId = GetSelectedItem(CType(GVNotes.Items(GVNotes.EditItemIndex).Cells(GRID_COL_NOTE_TYPE_IDX).FindControl(COMBO_NOTE_TYPE), DropDownList))
                    .Code = CType(GVNotes.Items(GVNotes.EditItemIndex).Cells(GRID_COL_CODE_IDX).FindControl(TEXT_CODE), TextBox).Text
                    .Text = CType(GVNotes.Items(GVNotes.EditItemIndex).Cells(GRID_COL_NOTE_IDX).FindControl(TEXT_NOTE), TextBox).Text
                    .DisplayOnWeb = LookupListNew.GetIdFromCode(LookupListNew.LK_LANG_INDEPENDENT_YES_NO, Codes.YESNO_Y)
                End With
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

        Private Sub ReturnFromEditing()
            GVNotes.EditItemIndex = NO_ROW_SELECTED_INDEX
            SetGridControls(GVNotes, True)
            State.IsEditMode = False
            PopulateGrid()
            State.PageIndex = GVNotes.CurrentPageIndex
            EnableDisableFields()
        End Sub

        Private Sub SortAndBindGrid()
            State.PageIndex = GVNotes.CurrentPageIndex
            GVNotes.DataSource = State.SearchDV
            HighLightSortColumn(GVNotes, State.SortExpression)
            GVNotes.DataBind()
            Session("recCount") = State.SearchDV.Count
            If GVNotes.Visible Then
                If (State.AddingNewRow) Then
                    lblRecordCount.Text = (State.SearchDV.Count - 1) & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                Else
                    lblRecordCount.Text = State.SearchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                End If
            End If
            ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me, GVNotes)
        End Sub

        Private Sub PopulateGrid()
            Dim oIssueComment As IssueComment = New IssueComment()
            Try
                State.Code = moCodeText.Text
                State.Description = moDescriptionText.Text
                State.SearchDV = State.MyBO.GetNotesSelectionView()

                State.SearchDV.Sort = State.SortExpression
                SetGridItemStyleColor(GVNotes)

                If (State.IsAfterSave) Then
                    State.IsAfterSave = False
                    SetPageAndSelectedIndexFromGuid(State.SearchDV, State.SelectedNotesChildId, GVNotes, State.PageIndex)
                ElseIf (State.IsNotesEditing) Then
                    SetPageAndSelectedIndexFromGuid(State.SearchDV, State.SelectedNotesChildId, GVNotes, State.PageIndex, State.IsEditMode)
                Else
                    SetPageAndSelectedIndexFromGuid(State.SearchDV, State.SelectedNotesChildId, GVNotes, State.PageIndex)
                End If
                State.PageIndex = GVNotes.CurrentPageIndex

                If State.SearchDV.Count > 0 Then
                    GVNotes.AutoGenerateColumns = False
                    ValidSearchResultCount(State.SearchDV.Count, True)
                    SortAndBindGrid()
                Else
                    If State.MyNotesChildBO Is Nothing Then
                        State.MyNotesChildBO = New IssueComment()
                    End If
                    State.SearchDV = State.MyNotesChildBO.GetNewDataViewRow(State.SearchDV, State.MyBO.Id, State.MyNotesChildBO)
                    SortAndBindGrid()
                End If
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

        Private Function GetGridDataView() As DataView
            With State
                Return IssueComment.GetList(State.MyBO.Id)
            End With

        End Function

        Protected Sub Notes_ItemCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs)
            Try
                MyBase.BaseItemCreated(sender, e)
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try
        End Sub

        'The Binding Logic is here
        Private Sub GridItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles GVNotes.ItemDataBound
            Dim itemType As ListItemType = e.Item.ItemType
            Dim dvRow As DataRowView = CType(e.Item.DataItem, DataRowView)

            If (itemType = ListItemType.Item OrElse itemType = ListItemType.AlternatingItem OrElse itemType = ListItemType.SelectedItem) Then
                Dim dRow As DataRow
                e.Item.Cells(GRID_COL_NOTE_ID_IDX).Text = GetGuidStringFromByteArray(CType(dvRow(IssueComment.IssueCommentGridDV.COL_NAME_ISSUE_COMMENT_ID), Byte()))
                e.Item.Cells(GRID_COL_CODE_IDX).Text = dvRow(IssueComment.IssueCommentGridDV.COL_NAME_CODE).ToString
                e.Item.Cells(GRID_COL_NOTE_IDX).Text = dvRow(IssueComment.IssueCommentGridDV.COL_NAME_TEXT).ToString
                dRow = FilterDatasetRowById(CType(dvRow(IssueComment.IssueCommentGridDV.COL_NAME_ISSUE_COMMENT_TYPE_ID), Byte()), GuidControl.GuidToHexString(ElitaPlusIdentity.Current.ActiveUser.LanguageId))
                If (dRow IsNot Nothing) Then
                    e.Item.Cells(GRID_COL_NOTE_TYPE_IDX).Text = CType(dRow.Item(DESCRIPTION), String).ToString
                End If
            ElseIf (itemType = ListItemType.EditItem) Then
                e.Item.Cells(GRID_COL_NOTE_ID_IDX).Text = GetGuidStringFromByteArray(CType(dvRow(IssueComment.IssueCommentGridDV.COL_NAME_ISSUE_COMMENT_ID), Byte()))

                Dim IssueCommentType As DataElements.ListItem() =
                CommonConfigManager.Current.ListManager.GetList(listCode:="ICTYP",
                                                                languageCode:=Thread.CurrentPrincipal.GetLanguageCode())
                CType(e.Item.Cells(GRID_COL_NOTE_TYPE_IDX).FindControl(COMBO_NOTE_TYPE), DropDownList).Populate(IssueCommentType,
                                       New PopulateOptions() With
                                        {
                                            .AddBlankItem = True
                                        })

                'BindListControlToDataView(CType(e.Item.Cells(GRID_COL_NOTE_TYPE_IDX).FindControl(COMBO_NOTE_TYPE), DropDownList), LookupListNew.GetIssueCommentTypeLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), , , True)

                If Not State.MyNotesChildBO.IssueCommentTypeId.Equals(Guid.Empty) Then
                    SetSelectedItem(CType(e.Item.Cells(GRID_COL_NOTE_TYPE_IDX).FindControl(COMBO_NOTE_TYPE), DropDownList), State.MyNotesChildBO.IssueCommentTypeId)
                End If
                CType(e.Item.Cells(GRID_COL_CODE_IDX).FindControl(TEXT_CODE), TextBox).Text = State.MyNotesChildBO.Code
                CType(e.Item.Cells(GRID_COL_NOTE_IDX).FindControl(TEXT_NOTE), TextBox).Text = State.MyNotesChildBO.Text
            End If

        End Sub

        Public Function FilterDatasetRowById(id As Byte(), lang As String) As System.Data.DataRow
            For Each row As DataRow In State.NoteDV.Table.Rows
                If CType(row("ID"), Byte()).SequenceEqual(id) Then
                    Return row
                End If
            Next
        End Function

        Protected Sub ItemCommand(source As Object, e As System.Web.UI.WebControls.DataGridCommandEventArgs)
            Try
                If (e.CommandName = EDIT_COMMAND) Then
                    State.IsNotesEditing = True
                    Try
                        State.SelectedNotesChildId = GetGuidFromString(GVNotes.Items(e.Item.ItemIndex).Cells(GRID_COL_NOTE_ID_IDX).Text)
                        BeginNotesChildEdit(State.SelectedNotesChildId, False)
                        State.PageIndex = GVNotes.CurrentPageIndex
                        PopulateGrid()
                        SetGridControls(GVNotes, False)
                        SetFocusOnEditableFieldInGrid(GVNotes, GRID_COL_CODE_IDX, TEXT_CODE, e.Item.ItemIndex)
                        EnableDisableFields()
                    Catch ex As Exception
                        State.MyBO.RejectChanges()
                        Throw ex
                    End Try
                ElseIf (e.CommandName = DELETE_COMMAND) Then
                    GVNotes.SelectedIndex = NO_ROW_SELECTED_INDEX
                    Try
                        State.SelectedNotesChildId = GetGuidFromString(GVNotes.Items(e.Item.ItemIndex).Cells(GRID_COL_NOTE_ID_IDX).Text)
                        State.MyNotesChildBO = New IssueComment(State.SelectedNotesChildId, State.MyBO.MyDataset)
                        BeginNotesChildEdit(State.SelectedNotesChildId, False)
                        EndNotesChildEdit(ElitaPlusPage.DetailPageCommand.Delete)
                        State.PageIndex = GVNotes.CurrentPageIndex
                        PopulateGrid()
                    Catch ex As Exception
                        State.MyBO.RejectChanges()
                        Throw ex
                    End Try
                    State.PageIndex = GVNotes.CurrentPageIndex
                End If
            Catch ex As Exception

                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

        'Protected Sub ItemBound(ByVal source As Object, ByVal e As DataGridItemEventArgs) Handles GVNotes.ItemDataBound
        '    BaseItemBound(source, e)
        'End Sub

        Protected Sub ItemCreated(sender As Object, e As DataGridItemEventArgs)
            BaseItemCreated(sender, e)
        End Sub

        Private Sub GridPageIndexChanged(source As Object, e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles GVNotes.PageIndexChanged
            Try
                State.PageIndex = e.NewPageIndex
                GVNotes.CurrentPageIndex = State.PageIndex
                PopulateGrid()
                GVNotes.SelectedIndex = NO_ITEM_SELECTED_INDEX
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

        Private Sub GridPageSizeChanged(source As Object, e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
            Try
                GVNotes.CurrentPageIndex = NewCurrentPageIndex(GVNotes, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
                State.SelectedPageSize = CType(cboPageSize.SelectedValue, Integer)
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

        Private Sub GridSortCommand(source As Object, e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles GVNotes.SortCommand
            Try
                If State.SortExpression.StartsWith(e.SortExpression) Then
                    If State.SortExpression.EndsWith(" DESC") Then
                        State.SortExpression = e.SortExpression
                    Else
                        State.SortExpression &= " DESC"
                    End If
                Else
                    State.SortExpression = e.SortExpression
                End If
                State.PageIndex = 0
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

        Protected Sub BindBoPropertiesToGridHeaders()
            BindBOPropertyToGridHeader(State.MyNotesChildBO, "IssueCommentTypeId", GVNotes.Columns(GRID_COL_NOTE_TYPE_IDX))
            BindBOPropertyToGridHeader(State.MyNotesChildBO, "CODE", GVNotes.Columns(GRID_COL_CODE_IDX))
            BindBOPropertyToGridHeader(State.MyNotesChildBO, "TEXT", GVNotes.Columns(GRID_COL_NOTE_IDX))
            ClearGridHeadersAndLabelsErrSign()
        End Sub

        Private Sub SetFocusOnEditableFieldInGrid(thisGrid As DataGrid, cellPosition As Integer, controlName As String, itemIndex As Integer)
            Dim desc As TextBox = CType(thisGrid.Items(itemIndex).Cells(cellPosition).FindControl(controlName), TextBox)
            SetFocus(desc)
        End Sub

#End Region

#End Region

        Private Sub btnCancel_WQ_Click(sender As Object, e As System.EventArgs) Handles btnCancel_WQ.Click
            Try
                PopulateWorkQueue()
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try
        End Sub

        Private Sub btnSave_WQ_Click(sender As Object, e As System.EventArgs) Handles btnSave_WQ.Click
            Try
                State.MyBO.SaveWorkQueueIssue(UC_QUEUE_AVASEL.SelectedList)
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try
        End Sub
    End Class

End Namespace
