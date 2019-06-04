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

        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
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
            Public Sub New(ByVal LastOp As DetailPageCommand, ByVal curEditingBo As Issue, ByVal hasDataChanged As Boolean)
                Me.LastOperation = LastOp
                Me.EditingBo = curEditingBo
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

        Private Sub Page_PageCall(ByVal CallFromUrl As String, ByVal CallingPar As Object) Handles MyBase.PageCall
            Dim Issue As IssueSearchForm.MyState

            Try
                If Not Me.CallingParameters Is Nothing Then
                    'Get the id from the parent
                    Issue = CType(Me.CallingParameters, IssueSearchForm.MyState)
                    Me.State.MyBO = New Issue(CType(Issue.SelectedIssueId, Guid))
                    If Not DateHelper.GetDateValue(Issue.ActiveOnDate) = Nothing Then
                        Me.State.MyBO.ActiveOn = DateHelper.GetDateValue(Issue.ActiveOnDate)
                        Me.State.ActiveOn = DateHelper.GetDateValue(Issue.ActiveOnDate)
                    Else
                        Me.State.MyBO.ActiveOn = DateTime.Now
                        Me.State.ActiveOn = DateTime.Now
                    End If
                    Me.State.IsEditMode = True
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try
        End Sub

        Private Sub Page_PageReturn(ByVal ReturnFromUrl As String, ByVal ReturnPar As Object) Handles MyBase.PageReturn
            Try
                Me.MenuEnabled = True
                Me.IsReturningFromChild = True
                Dim retObj As EquipmentListDetailForm.ReturnType = CType(ReturnPar, EquipmentListDetailForm.ReturnType)
                Me.State.HasDataChanged = retObj.HasDataChanged
                Select Case retObj.LastOperation
                    Case ElitaPlusPage.DetailPageCommand.Back
                        If Not retObj Is Nothing Then
                            If Not retObj.EditingBo.IsNew Then
                                Me.State.EquipmentListDetailId = retObj.EditingBo.Id
                            End If
                        End If
                    Case ElitaPlusPage.DetailPageCommand.Delete
                        Me.DisplayMessage(Message.DELETE_RECORD_CONFIRMATION, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
                End Select
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try
        End Sub

        Private Sub SaveGuiState()
            Me.State.Code = Me.moCodeText.Text
            Me.State.Description = Me.moDescriptionText.Text
            If Not Me.moEffectiveDateText.Text Is String.Empty Then
                Me.State.EffectiveDate = DateHelper.GetDateValue(Me.moEffectiveDateText.Text)
            End If
            If Not Me.moExpirationDateText.Text Is String.Empty Then
                Me.State.ExpirationDate = DateHelper.GetDateValue(Me.moExpirationDateText.Text)
            End If
        End Sub

        Private Sub RestoreGuiState()
            Me.moCodeText.Text = Me.State.MyBO.Code
            Me.moDescriptionText.Text = Me.State.MyBO.Description
            Me.moPreConditionsTextBox.Text = Me.State.MyBO.PreConditions
            Me.cboIssueTypeText.SelectedValue = Me.State.MyBO.IssueTypeId.ToString
            If Not Me.State.MyBO.Effective Is Nothing Then
                Me.moEffectiveDateText.Text = ElitaPlusPage.GetLongDate12FormattedString(CDate(Me.State.MyBO.Effective))
            Else
                Me.moEffectiveDateText.Text = String.Empty
            End If
            If Not Me.State.MyBO.Expiration Is Nothing Then
                Me.moExpirationDateText.Text = ElitaPlusPage.GetLongDate12FormattedString(CDate(Me.State.MyBO.Expiration))
            Else
                Me.moExpirationDateText.Text = String.Empty
            End If
        End Sub
#End Region

#Region "Page_Events"
        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            Try
                Me.ErrorCtrl.Clear_Hide()
                Me.ValidateDates()

                If Not Me.IsPostBack Then

                    'If Assurant.Elita.Configuration.ElitaConfig.Current.General.IntegrateWorkQueueImagingServices = False Then
                    '    ControlMgr.SetEnableTabStrip(Me, tsQuestions.Items(3), False)
                    'End If
                    Me.AddCalendarwithTime(Me.imgEffectiveDate, Me.moEffectiveDateText)
                    Me.AddCalendarwithTime(Me.imgExpirationDate, Me.moExpirationDateText)

                    If Me.State.MyBO Is Nothing Then
                        Me.State.MyBO = New Issue
                        Me.State.MyBO.ActiveOn = System.DateTime.Now()
                    Else
                        Me.EnableHeaderControls(False)
                    End If

                    Me.PopulateDropdown()

                    Me.ErrorCtrl.Clear_Hide()
                    If Not Me.IsPostBack Then
                        Me.AddControlMsg(Me.btnDelete_WRITE, Message.DELETE_RECORD_PROMPT, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, True)
                        If Me.State.MyBO Is Nothing Then
                            Me.State.MyBO = New Issue
                        End If
                        UC_QUEUE_AVASEL.BackColor = "#d5d6e4"
                        Me.PopulateChildern()
                        Me.PopulateFormFromBOs()
                        Me.EnableDisableFields()
                    End If
                    ControlMgr.SetVisibleControl(Me, trPageSize, False)
                    Me.RestoreGuiState()
                    PopulateWorkQueue()
                    If Me.State.IsEditMode Then
                        EnableDisableNotesButtons(False)
                    End If
                Else
                    Me.SaveGuiState()
                End If
                Me.CheckIfComingFromSaveConfirm()
                Me.BindBoPropertiesToLabels()
                Me.AddLabelDecorations(Me.State.MyBO)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try
            Me.ShowMissingTranslations(Me.ErrorCtrl)
        End Sub
#End Region

#Region "Button Clicks"

        Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
            Try

                Me.PopulateBOsFormFrom()
                If Me.State.MyBO.IsDirty Then
                    Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", Me.MSG_BTN_YES_NO_CANCEL, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                    Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
                Else
                    Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.MyBO, Me.State.HasDataChanged))
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
                Me.DisplayMessage(Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR, "", Me.MSG_BTN_YES_NO_CANCEL, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.BackOnErr
                Me.State.LastErrMsg = Me.ErrorCtrl.Text
            End Try
        End Sub

        Private Sub btnSave_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave_WRITE.Click
            Try
                Me.IsBackdated()
                Me.PopulateBOsFormFrom()
                Me.IsDateValidated()
                Me.EditModeValidation()
                '#5 - Expiration date lies between Effective and Expiration of earlier list code and there is no future list code available
                If (IsListCodeOverlapped(Me.State.Code, Me.State.EffectiveDate, Me.State.ExpirationDate, Me.State.MyBO.Id)) Then
                    Me.DisplayMessage(Message.MSG_EXPIRE_PREVIOUS_ISSUE, "", Me.MSG_BTN_YES_NO_CANCEL, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                    Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Accept
                    Return
                End If

                '#6 - Expiration/Expiration date lies between any other Effective and Expiration range of earlier list code
                If (IsListCodeDurationOverlapped(Me.State.Code, Me.State.EffectiveDate, Me.State.ExpirationDate, Me.State.MyBO.Id)) Then
                    Throw New GUIException(Message.MSG_GUI_INVALID_EFFECTIVE_DATE_SMALLER_THAN_SYSDATE, Assurant.ElitaPlus.Common.ErrorCodes.EQUIPMENT_LIST_CODE_OVERLAPPED)
                End If

                If Me.State.MyBO.IsDirty Then
                    Me.State.MyBO.Save()
                    Me.State.HasDataChanged = True
                    Me.PopulateFormFromBOs()
                    Me.EnableDisableFields()
                    Me.EnableHeaderControls(False)
                    If Not Me.State.IsChildCreated Then
                        EnableDisableUserControlTab(PanelQuestionsEditDetail, True)
                        EnableDisableUserControlTab(PanelRulesEditDetail, True)
                        EnableDisableUserControlTab(PanelNotesEditDetail, True)
                    End If
                    Me.DisplayMessage(Message.SAVE_RECORD_CONFIRMATION, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
                Else
                    Me.DisplayMessage(Message.MSG_RECORD_NOT_SAVED, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
                End If

            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try
        End Sub

        Private Sub btnUndo_Write_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUndo_Write.Click
            Try
                If Not Me.State.MyBO.IsNew Then
                    'Reload from the DB
                    Me.State.MyBO = New Issue(Me.State.MyBO.Id)
                ElseIf Not Me.State.ScreenSnapShotBO Is Nothing Then
                    'It was a new with copy
                    Me.State.MyBO.Clone(Me.State.ScreenSnapShotBO)
                Else
                    Me.State.MyBO = New Issue
                End If
                Me.PopulateFormFromBOs()
                Me.EnableDisableFields()
                Me.EnableDisableUserControlTab(PanelQuestionsEditDetail, False)
                Me.EnableDisableUserControlTab(PanelRulesEditDetail, False)
                EnableDisableUserControlTab(PanelNotesEditDetail, False)
                Me.EnableUserControl(True)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try
        End Sub

        Private Sub btnDelete_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete_WRITE.Click
            Try
                If (Me.State.MyBO.CheckIfIssueIsAssignedToQuestionNoteOrRule(Me.State.MyBO.Id)) Then
                    Throw New GUIException(Message.MSG_GUI_LIST_CODE_ASSIGNED_TO_DEALER_NO_DELETE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_LIST_CODE_ERR)
                Else
                    Me.PopulateChildern()
                    Me.State.MyBO.Delete()
                    Me.State.MyBO.Save()
                    Me.State.HasDataChanged = True
                    Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Delete, Me.State.MyBO, Me.State.HasDataChanged))
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                'undo the delete
                Me.State.MyBO.RejectChanges()
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try
        End Sub

        Private Sub btnNew_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew_WRITE.Click
            Try
                Me.PopulateBOsFormFrom()
                If Me.State.MyBO.IsDirty Then
                    Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", Me.MSG_BTN_YES_NO_CANCEL, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                    Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.New_
                Else
                    Me.CreateNew()
                    Me.ClearGridHeadersAndLabelsErrSign()
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try
        End Sub

        Private Sub btnCopy_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCopy_WRITE.Click
            Try
                If Me.State.MyBO.IsDirty Then
                    Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", Me.MSG_BTN_YES_NO_CANCEL, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                    Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.NewAndCopy
                Else
                    Me.CreateNewWithCopy()
                End If
                Me.PopulateBOsFormFrom()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try
        End Sub

        Private Sub btnNew_Comment_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew_Comment.Click

            Try
                Me.State.IsEditMode = True
                Me.State.IsGridVisible = True
                Me.State.AddingNewRow = True
                AddNew()
                EnableDisableFields()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try

        End Sub

        Private Sub btnCancel_Comment_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel_Comment.Click

            Try
                Me.SetGridControls(GVNotes, False)
                Me.EndNotesChildEdit(ElitaPlusPage.DetailPageCommand.Cancel)
                PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try

        End Sub

        Private Sub btnSave_Comment_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave_Comment.Click
            Try
                PopulateCommentBOFromForm()
                Me.EndNotesChildEdit(ElitaPlusPage.DetailPageCommand.OK)
                Me.State.IsNotesEditing = False
                PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
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

            Me.State.NoteDV = LookupListNew.GetIssueCommentTypeLookupList(Authentication.LangId)

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
            If Not Me.State.EffectiveDate Is Nothing And Me.State.MyBO.IsNew = False Then
                If DateHelper.GetDateValue(Me.State.MyBO.Effective.ToString) <> DateHelper.GetDateValue(moEffectiveDateText.Text.ToString) Then
                    If DateHelper.GetDateValue(moEffectiveDateText.Text.ToString) < EquipmentListDetail.GetCurrentDateTime() Then
                        Throw New GUIException(Message.MSG_GUI_INVALID_EFFECTIVE_DATE_SMALLER_THAN_SYSDATE, Assurant.ElitaPlus.Common.ErrorCodes.EQUIPMENT_INVALID_EFFECIVE_DATE)
                    End If
                End If
            End If

            '#2 - Restrict to save backdated list in edit mode
            If Not Me.State.ExpirationDate Is Nothing And Me.State.MyBO.IsNew = False Then
                If DateHelper.GetDateValue(Me.State.MyBO.Expiration.ToString) <> DateHelper.GetDateValue(moExpirationDateText.Text.ToString) Then
                    If (Me.State.MyBO.CheckIfIssueIsAssignedToQuestionNoteOrRule(Me.State.MyBO.Id)) Then
                        If DateHelper.GetDateValue(Me.State.ExpirationDate.ToString) < EquipmentListDetail.GetCurrentDateTime() Then
                            Throw New GUIException(Message.MSG_GUI_INVALID_EXPIRATION_DATE_SMALLER_THAN_SYSDATE, Assurant.ElitaPlus.Common.ErrorCodes.EQUIPMENT_INVALID_EXPIRATION_DATE_LESSTHAN_SYSDATE)
                        End If
                    End If
                End If
            End If

        End Sub

        Sub IsDateValidated()

            '#3 - Effective date should be greater than Expiration Date
            If Not Me.State.EffectiveDate Is Nothing And Not Me.State.ExpirationDate Is Nothing Then
                If DateHelper.GetDateValue(Me.State.EffectiveDate.ToString) > DateHelper.GetDateValue(Me.State.ExpirationDate.ToString) Then
                    Throw New GUIException(Message.MSG_GUI_INVALID_EFFECTIVE_HIGHER_EXPIRATION_DATE, Assurant.ElitaPlus.Common.ErrorCodes.EQUIPMENT_INVALID_EXPIRATION_DATE)
                End If
            End If

        End Sub

        Sub EditModeValidation()
            '#4 - For new records, check for no backdated List code and no duplicate List code - Effective Date Combination
            If Not Me.State.IsEditMode Then
                If Not Me.State.EffectiveDate Is Nothing And Not Me.State.ExpirationDate Is Nothing Then
                    If DateHelper.GetDateValue(Me.State.EffectiveDate.ToString) < EquipmentListDetail.GetCurrentDateTime().Today Then
                        Throw New GUIException(Message.MSG_GUI_INVALID_EFFECTIVE_DATE_SMALLER_THAN_SYSDATE, Assurant.ElitaPlus.Common.ErrorCodes.EQUIPMENT_INVALID_EFFECIVE_DATE)
                    End If
                End If

                If (IsListCodeDuplicate(Me.State.Code, Me.State.EffectiveDate.ToString, Me.State.MyBO.Id)) Then
                    Throw New GUIException(Message.MSG_GUI_INVALID_LIST_CODE_WITH_SAME_EFFECIVE_DATE, Assurant.ElitaPlus.Common.ErrorCodes.EQUIPMENT_DUPLICATE_CODE_EFFECTIVE)
                End If
            End If
        End Sub

        Protected Function IsListCodeDuplicate(ByVal code As String, ByVal effective As String,
                                        ByVal id As Guid) As Boolean

            If (Issue.CheckDuplicateEquipmentListCode(code, DateHelper.GetDateValue(effective).ToString(ElitaPlusPage.DATE_TIME_FORMAT, System.Globalization.CultureInfo.CreateSpecificCulture("en-US")), id)) Then
                Return True
            End If
            Return False
        End Function

        Protected Function IsListCodeOverlapped(ByVal code As String, ByVal effective As DateType,
                                        ByVal expiration As DateType, ByVal id As Guid) As Boolean

            If (Issue.CheckListCodeForOverlap(code, effective, expiration, id)) Then
                Return True
            End If
            Return False
        End Function

        Protected Function IsListCodeDurationOverlapped(ByVal code As String, ByVal effective As DateType,
                                        ByVal expiration As DateType, ByVal listId As Guid) As Boolean

            If (Issue.CheckListCodeDurationOverlap(code, effective, expiration, listId)) Then
                Return True
            End If
            Return False
        End Function

        Protected Function ExpirePreviousList(ByVal code As String, ByVal effective As DateType,
                                        ByVal expiration As DateType, ByVal listId As Guid) As Boolean

            If (Issue.ExpirePreviousList(code, effective, expiration, listId)) Then
                Return True
            End If
            Return False
        End Function

        Protected Sub EnableUserControl(ByVal bVisible As Boolean)
            UserControlQuestionsAvailable.ShowCancelButton = True
            UserControlRulesAvailable.ShowCancelButton = True
            UserControlQuestionsAvailable.dvSelectedQuestions = Issue.GetSelectedQuestionsList(Me.State.MyBO.Id)
            UserControlRulesAvailable.dvSelectedDealer = Issue.GetSelectedRulesList(Me.State.MyBO.Id)
        End Sub

        Public Sub ValidateDates()
            Dim tempDate As DateTime = New DateTime

            If Not moEffectiveDateText.Text Is String.Empty Then
                If (DateHelper.IsDate(moEffectiveDateText.Text.ToString()) = False) Then
                    ElitaPlusPage.SetLabelError(Me.moEffectiveDateLabel)
                    Throw New GUIException(Message.MSG_BEGIN_END_DATE, Message.MSG_INVALID_DATE)
                Else
                    tempDate = DateHelper.GetDateValue(moEffectiveDateText.Text.ToString())
                End If
            End If


            If Not moExpirationDateText.Text Is String.Empty Then
                If (DateHelper.IsDate(moExpirationDateText.Text.ToString()) = False) Then
                    ElitaPlusPage.SetLabelError(Me.moExpirationDateLabel)
                    Throw New GUIException(Message.MSG_BEGIN_END_DATE, Message.MSG_INVALID_DATE)
                Else
                    tempDate = DateHelper.GetDateValue(moExpirationDateText.Text.ToString())
                End If
            End If
        
        End Sub

        Protected Sub BindBoPropertiesToLabels()
            Me.BindBOPropertyToLabel(Me.State.MyBO, "Code", Me.moCodeLabel)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "Description", Me.moDescriptionLabel)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "Effective", Me.moEffectiveDateLabel)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "Expiration", Me.moExpirationDateLabel)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "IssueTypeId", Me.moIssueTypeLabel)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "ActiveOn", Me.moAsOfDateLabel)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "PreConditions", Me.moPreConditionsLabel)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "IssueProcessor", Me.moIssueProcessorLabel)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "DeniedReason", Me.moClaimDeniedRsnLabel)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "SPClaimValue", Me.moClaimValueLabel)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "SPClaimType", Me.moClaimTypeLabel)
        End Sub

        Protected Sub PopulateBOsFormFrom()
            With Me.State.MyBO
                Me.PopulateBOProperty(Me.State.MyBO, "Code", Me.moCodeText)
                Me.PopulateBOProperty(Me.State.MyBO, "Description", Me.moDescriptionText)
                Me.PopulateBOProperty(Me.State.MyBO, "PreConditions", Me.moPreConditionsTextBox)
                Me.PopulateBOProperty(Me.State.MyBO, "Effective", Me.moEffectiveDateText)
                Me.PopulateBOProperty(Me.State.MyBO, "Expiration", Me.moExpirationDateText)
                Me.PopulateBOProperty(Me.State.MyBO, "IssueTypeId", Me.cboIssueTypeText)
                Me.PopulateBOProperty(Me.State.MyBO, "ActiveOn", Me.moAsOfDateText)
                Me.PopulateBOProperty(Me.State.MyBO, "IssueProcessor", Me.cboIssueProcessor, False, True)
                Me.PopulateBOProperty(Me.State.MyBO, "DeniedReason", Me.cboClaimDeniedRsn, False, True)
                Me.PopulateBOProperty(Me.State.MyBO, "SPClaimValue", Me.moClaimValueTextBox)
                Me.PopulateBOProperty(Me.State.MyBO, "SPClaimType", Me.cboClaimType, False, True)
                If Me.State.MyBO.IssueProcessor = String.Empty Then
                    Me.State.MyBO.IssueProcessor = Nothing
                End If
                If Me.State.MyBO.SPClaimType = String.Empty Then
                    Me.State.MyBO.SPClaimType = Nothing
                End If
            End With
            If Me.ErrCollection.Count > 0 Then
                Throw New PopulateBOErrorException
            End If
        End Sub

        Protected Sub PopulateChildern()
            Dim CommentChildren As IssueNotesChildrenList = Me.State.MyBO.IssueNotesChildren
            Dim QuestionChildren As IssueQuestionsChildrenList = Me.State.MyBO.IssueQuestionsChildren
            Dim RuleChildren As IssueRulesChildrenList = Me.State.MyBO.IssueRulesChildren
        End Sub

        Protected Sub PopulateFormFromBOs()
            Me.PopulateGrid()
            With Me.State.MyBO
                Me.PopulateControlFromBOProperty(Me.moCodeText, .Code)
                Me.PopulateControlFromBOProperty(Me.moDescriptionText, .Description)
                Me.PopulateControlFromBOProperty(Me.moPreConditionsTextBox, .PreConditions)
                If Not Me.State.MyBO.Effective Is Nothing Then
                    Me.moEffectiveDateText.Text = ElitaPlusPage.GetLongDate12FormattedString(CDate(Me.State.MyBO.Effective))
                Else
                    Me.moEffectiveDateText.Text = String.Empty
                End If
                If Not Me.State.MyBO.Expiration Is Nothing Then
                    Me.moExpirationDateText.Text = ElitaPlusPage.GetLongDate12FormattedString(CDate(Me.State.MyBO.Expiration))
                Else
                    Me.moExpirationDateText.Text = String.Empty
                End If
                Me.PopulateControlFromBOProperty(Me.moAsOfDateText, .ActiveOn)
                Me.SetSelectedItem(Me.cboIssueTypeText, .IssueTypeId)
                BindSelectItem(.SPClaimType, Me.cboClaimType)
                BindSelectItem(.IssueProcessor, Me.cboIssueProcessor)
                BindSelectItem(.DeniedReason, Me.cboClaimDeniedRsn)
                Me.PopulateControlFromBOProperty(Me.moClaimValueTextBox, .SPClaimValue)
            End With

        End Sub
        Private Sub cboissueprocessor_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboIssueProcessor.SelectedIndexChanged
            If (GetSelectedValue(Me.cboIssueProcessor) = Codes.ISSUE_PROCESSOR__CUST Or GetSelectedValue(Me.cboIssueProcessor) = String.Empty) Then
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

        Sub EnableHeaderControls(ByVal enableToggle As Boolean)
            ControlMgr.SetEnableControl(Me, moCodeText, enableToggle)
            If (Me.State.MyBO.CheckIfIssueIsAssignedToQuestionNoteOrRule(Me.State.MyBO.Id)) Then
                ControlMgr.SetEnableControl(Me, moDescriptionText, enableToggle)
                ControlMgr.SetEnableControl(Me, moEffectiveDateText, enableToggle)
            Else
                ControlMgr.SetEnableControl(Me, moDescriptionText, Not (enableToggle))
                ControlMgr.SetEnableControl(Me, moEffectiveDateText, Not (enableToggle))
            End If
        End Sub

        Sub EnableDisableParentControls(ByVal enableToggle As Boolean)
            ControlMgr.SetEnableControl(Me, btnCopy_WRITE, enableToggle)
            ControlMgr.SetEnableControl(Me, btnDelete_WRITE, enableToggle)
            ControlMgr.SetEnableControl(Me, btnSave_WRITE, enableToggle)
            ControlMgr.SetEnableControl(Me, btnUndo_Write, enableToggle)
            ControlMgr.SetEnableControl(Me, btnNew_WRITE, enableToggle)
            ControlMgr.SetEnableControl(Me, btnBack, enableToggle)
        End Sub

        Sub EnableDisableNotesButtons(ByVal enableToggle As Boolean)
            ControlMgr.SetEnableControl(Me, btnNew_Comment, Not enableToggle)
            ControlMgr.SetEnableControl(Me, btnSave_Comment, enableToggle)
            ControlMgr.SetEnableControl(Me, btnCancel_Comment, enableToggle)
        End Sub

        Sub EnableDisableUserControlTab(ByVal panel As WebControls.Panel, ByVal enableToggle As Boolean)
            ControlMgr.SetEnableControl(Me, panel, enableToggle)
        End Sub

        Protected Sub EnableDisableFields()
            If Me.State.IsNotesEditing Then
                Me.EnableHeaderControls(False)
                Me.EnableDisableParentControls(False)
                Me.EnableUserControl(True)
                Me.EnableDisableNotesButtons(True)
            ElseIf Me.State.IsQuestionsEditing Then
            ElseIf Me.State.IsRulesEditing Then
            Else
                Me.EnableDisableParentControls(True)
                Me.EnableUserControl(False)
            End If

            'ControlMgr.SetEnableControl(Me, btnDelete_WRITE, True)
            'ControlMgr.SetEnableControl(Me, btnNew_WRITE, True)
            'ControlMgr.SetEnableControl(Me, btnCopy_WRITE, True)

            If Me.State.MyBO.IsNew Then
                ControlMgr.SetEnableControl(Me, moCodeText, True)
                ControlMgr.SetEnableControl(Me, moDescriptionText, True)
                ControlMgr.SetEnableControl(Me, moEffectiveDateText, True)

                ControlMgr.SetEnableControl(Me, btnDelete_WRITE, False)
                ControlMgr.SetEnableControl(Me, btnNew_WRITE, False)
                ControlMgr.SetEnableControl(Me, btnCopy_WRITE, False)
                EnableDisableUserControlTab(PanelQuestionsEditDetail, False)
                EnableDisableUserControlTab(PanelRulesEditDetail, False)
                EnableDisableUserControlTab(PanelNotesEditDetail, False)

                Me.EnableDisableNotesButtons(False)
            End If
            If Assurant.Elita.Configuration.ElitaConfig.Current.General.IntegrateWorkQueueImagingServices = False Then
                EnableDisableUserControlTab(plnQueue, False)
            End If

            If (GetSelectedValue(Me.cboIssueProcessor) = Codes.ISSUE_PROCESSOR__CUST Or GetSelectedValue(Me.cboIssueProcessor) = String.Empty) Then
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
            Me.State.ScreenSnapShotBO = Nothing 'Reset the backup copy
            Me.State.MyBO = New Issue
            Me.PopulateFormFromBOs()
            Me.EnableDisableFields()
            Me.State.IsEditMode = False
        End Sub

        Protected Sub CreateNewWithCopy()
            Me.State.IsACopy = True
            Dim newObj As New Issue
            newObj.Copy(Me.State.MyBO)

            Me.State.MyBO = newObj
            Me.State.MyBO.Effective = EquipmentListDetail.GetCurrentDateTime()
            Me.State.MyBO.Expiration = New DateTime(2499, 12, 31, 23, 59, 59)
            Me.State.MyBO.Code = Nothing
            Me.State.MyBO.Description = Nothing
            Me.PopulateFormFromBOs()
            Me.EnableDisableFields()
            Me.State.ScreenSnapShotBO = New Issue
            Me.State.ScreenSnapShotBO.Clone(Me.State.MyBO)
            Me.State.IsACopy = False
            Me.State.IsEditMode = False
        End Sub

        Protected Sub CheckIfComingFromSaveConfirm()
            Dim confResponse As String = Me.HiddenSaveChangesPromptResponse.Value
            If Not confResponse Is Nothing AndAlso confResponse = Me.MSG_VALUE_YES Then
                If Me.State.ActionInProgress <> ElitaPlusPage.DetailPageCommand.BackOnErr AndAlso Me.State.ActionInProgress <> ElitaPlusPage.DetailPageCommand.Accept Then
                    Me.BindBoPropertiesToLabels()
                    Me.IsBackdated()
                    Me.PopulateBOsFormFrom()
                    Me.IsDateValidated()
                    Me.EditModeValidation()
                    Me.PopulateWorkQueue()
                    '#5 - Expiration date lies between Effective and Expiration of earlier list code and there is no future list code available
                    If (IsListCodeOverlapped(Me.State.Code, Me.State.EffectiveDate, Me.State.ExpirationDate, Me.State.MyBO.Id)) Then
                        Me.DisplayMessage(Message.MSG_EXPIRE_PREVIOUS_ISSUE, "", Me.MSG_BTN_YES_NO_CANCEL, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                        Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Accept
                        Return
                    End If

                    '#6 - Expiration/Expiration date lies between any other Effective and Expiration range of earlier list code
                    If (IsListCodeDurationOverlapped(Me.State.Code, Me.State.EffectiveDate, Me.State.ExpirationDate, Me.State.MyBO.Id)) Then
                        Throw New GUIException(Message.MSG_GUI_INVALID_EFFECTIVE_DATE_SMALLER_THAN_SYSDATE, Assurant.ElitaPlus.Common.ErrorCodes.EQUIPMENT_LIST_CODE_OVERLAPPED)
                    End If

                    Me.State.MyBO.Save()
                End If
                Select Case Me.State.ActionInProgress
                    Case ElitaPlusPage.DetailPageCommand.Back
                        Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.MyBO, Me.State.HasDataChanged))
                    Case ElitaPlusPage.DetailPageCommand.New_
                        Me.DisplayMessage(Message.SAVE_RECORD_CONFIRMATION, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
                        Me.CreateNew()
                    Case ElitaPlusPage.DetailPageCommand.NewAndCopy
                        Me.DisplayMessage(Message.SAVE_RECORD_CONFIRMATION, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
                        Me.CreateNewWithCopy()
                    Case ElitaPlusPage.DetailPageCommand.BackOnErr
                        Me.ReturnToCallingPage(New ReturnType(Me.State.ActionInProgress, Me.State.MyBO, Me.State.HasDataChanged))
                    Case ElitaPlusPage.DetailPageCommand.Accept
                        If Me.State.MyBO.IsDirty Then
                            Me.State.MyBO.Save()
                            Issue.ExpirePreviousList(Me.State.Code, Me.State.EffectiveDate, Me.State.ExpirationDate, Me.State.MyBO.Id)
                            Me.State.HasDataChanged = True
                            Me.PopulateFormFromBOs()
                            Me.EnableDisableFields()
                            Me.EnableDisableUserControlTab(PanelQuestionsEditDetail, True)
                            Me.EnableDisableUserControlTab(PanelRulesEditDetail, True)
                            Me.EnableHeaderControls(False)
                            Me.PopulateWorkQueue()
                            Me.DisplayMessage(Message.SAVE_RECORD_CONFIRMATION, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
                        Else
                            Me.DisplayMessage(Message.MSG_RECORD_NOT_SAVED, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
                        End If
                End Select
            ElseIf Not confResponse Is Nothing AndAlso confResponse = Me.MSG_VALUE_NO Then
                Select Case Me.State.ActionInProgress
                    Case ElitaPlusPage.DetailPageCommand.Back
                        Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.MyBO, Me.State.HasDataChanged))
                    Case ElitaPlusPage.DetailPageCommand.New_
                        Me.CreateNew()
                    Case ElitaPlusPage.DetailPageCommand.NewAndCopy
                        Me.CreateNewWithCopy()
                    Case ElitaPlusPage.DetailPageCommand.BackOnErr
                        Me.ErrorCtrl.AddErrorAndShow(Me.State.LastErrMsg)
                    Case ElitaPlusPage.DetailPageCommand.Accept
                        Me.EnableDisableFields()
                End Select
            End If
            Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
            Me.HiddenSaveChangesPromptResponse.Value = ""
        End Sub

        Private Sub AddNew()
            Dim dv As DataView

            Me.BeginNotesChildEdit(Guid.Empty, False)
            Me.State.MyNotesChildBO.IssueId = Me.State.MyBO.Id
            Me.State.MyNotesChildBO.IssueCommentTypeId = Guid.Empty
            Me.State.MyNotesChildBO.Code = String.Empty
            Me.State.MyNotesChildBO.Text = String.Empty
            Me.State.MyNotesChildBO.DisplayOnWeb = LookupListNew.GetIdFromCode(LookupListNew.LK_LANG_INDEPENDENT_YES_NO, Codes.YESNO_Y)
            Me.State.SearchDV = Me.State.MyBO.GetNotesSelectionView()

            GVNotes.DataSource = Me.State.SearchDV
            Me.GVNotes.AutoGenerateColumns = False

            Me.SetPageAndSelectedIndexFromGuid(Me.State.SearchDV, Me.State.MyNotesChildBO.Id, Me.GVNotes, Me.State.PageIndex, Me.State.IsEditMode)
            GVNotes.DataBind()
            Me.State.PageIndex = GVNotes.CurrentPageIndex

            Me.SetPageAndSelectedIndexFromGuid(Me.State.SearchDV, Me.State.MyNotesChildBO.Id, GVNotes, GVNotes.CurrentPageIndex, True)
            SetGridControls(Me.GVNotes, False)
            Me.EnableDisableFields()
            ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me, GVNotes)
        End Sub

#End Region

#Region "Child_Control"



        Sub BeginNotesChildEdit(ByVal IssueCommentId As Guid, ByVal expireNow As Boolean)
            Me.State.IsNotesEditing = True
            Me.State.SelectedNotesChildId = IssueCommentId
            With Me.State
                If Not .SelectedNotesChildId = Guid.Empty Then
                    .MyNotesChildBO = .MyBO.GetNotesChild(.SelectedNotesChildId)
                Else
                    .MyNotesChildBO = .MyBO.GetNewNotesChild
                End If
                .MyNotesChildBO.BeginEdit()
            End With
        End Sub

        Sub BeginQuestionsChildEdit(ByVal SoftQuestionId As Guid, ByVal expireNow As Boolean, ByVal DisplayOrder As Integer)
            Me.State.IsQuestionsEditing = True
            Me.State.SelectedQuestionsChildId = Guid.Empty
            Me.State.SelectedQuestionsChildId = New Guid(Me.State.MyQuestionsChildBO.IsChild(Me.State.MyBO.Id, SoftQuestionId))
            With Me.State
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

        Sub BeginRulesChildEdit(ByVal RuleIssueId As Guid, ByVal expireNow As Boolean)
            Me.State.IsRulesEditing = True
            Me.State.SelectedRulesChildId = Guid.Empty
            Me.State.SelectedRulesChildId = New Guid(Me.State.MyRulesChildBO.IsChild(Me.State.MyBO.Id, RuleIssueId))
            With Me.State
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

        Sub PopulateQuestionChildBOFrom(ByVal SoftQuestionId As Guid)
            Dim NewQuestionExpiration As DateTime
            Dim SelectedQuestionExpiration As DateTime
            Dim QuestionOldExpiraitonDate As DateTime

            With Me.State.MyQuestionsChildBO
                SelectedQuestionExpiration = Issue.GetQuestionExpiration(Me.State.MyBO.Id, SoftQuestionId)
                QuestionOldExpiraitonDate = CDate("#" & .Expiration.ToString & "#")

                If Not Me.State.MyBO.Expiration Is Nothing Then
                    NewQuestionExpiration = CDate("#" & Me.State.MyBO.Expiration.ToString & "#")
                End If
                ''#1 
                .IssueId = Me.State.MyBO.Id
                .SoftQuestionId = SoftQuestionId
                ''#2
                If Not SelectedQuestionExpiration = Nothing And SelectedQuestionExpiration < NewQuestionExpiration Then
                    .Expiration = SelectedQuestionExpiration
                Else
                    .Expiration = NewQuestionExpiration
                End If
                ''#3
                If QuestionOldExpiraitonDate < EquipmentListDetail.GetCurrentDateTime() Then
                    .Effective = EquipmentListDetail.GetCurrentDateTime()
                End If
            End With
            If Me.ErrCollection.Count > 0 Then
                Throw New PopulateBOErrorException
            End If
        End Sub

        Sub PopulateRuleChildBOFrom(ByVal RuleId As Guid)
            Dim NewRuleExpiration As DateTime
            Dim SelectedRuleExpiration As DateTime
            Dim RuleOldExpiraitonDate As DateTime

            With Me.State.MyRulesChildBO
                SelectedRuleExpiration = Issue.GetRuleExpiration(Me.State.MyBO.Id, RuleId)
                RuleOldExpiraitonDate = CDate("#" & .Expiration.ToString & "#")

                If Not Me.State.MyBO.Expiration Is Nothing Then
                    NewRuleExpiration = CDate("#" & Me.State.MyBO.Expiration.ToString & "#")
                End If
                ''#1 
                .IssueId = Me.State.MyBO.Id
                .RuleId = RuleId
                ''#2
                If Not SelectedRuleExpiration = Nothing And SelectedRuleExpiration < NewRuleExpiration Then
                    .Expiration = SelectedRuleExpiration
                Else
                    .Expiration = NewRuleExpiration
                End If
                ''#3
                If RuleOldExpiraitonDate < EquipmentListDetail.GetCurrentDateTime() Then
                    .Effective = EquipmentListDetail.GetCurrentDateTime()
                End If
            End With
            If Me.ErrCollection.Count > 0 Then
                Throw New PopulateBOErrorException
            End If
        End Sub

        Sub SetQuestionsExpiration(ByVal IssueQuestionId As Guid)
            With Me.State.MyQuestionsChildBO
                Me.State.MyQuestionsChildBO.Expiration = EquipmentListDetail.GetCurrentDateTime().AddSeconds(-1)
            End With
            If Me.ErrCollection.Count > 0 Then
                Throw New PopulateBOErrorException
            End If
        End Sub

        Sub SetRulesExpiration(ByVal RuleIssueId As Guid)
            With Me.State.MyRulesChildBO
                Me.State.MyRulesChildBO.Expiration = EquipmentListDetail.GetCurrentDateTime().AddSeconds(-1)
            End With
            If Me.ErrCollection.Count > 0 Then
                Throw New PopulateBOErrorException
            End If
        End Sub

        Sub EndNotesChildEdit(ByVal lastop As ElitaPlusPage.DetailPageCommand)

            With Me.State
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
            Me.State.IsNotesEditing = False
            Me.EnableDisableFields()
            EnableDisableNotesButtons(False)
            PopulateGrid()

        End Sub

        Sub EndQuestionsChildEdit(ByVal lastop As ElitaPlusPage.DetailPageCommand)
            Try
                With Me.State
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
                Me.State.IsQuestionsEditing = False
                Me.EnableDisableFields()
                PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try
        End Sub

        Sub EndRulesChildEdit(ByVal lastop As ElitaPlusPage.DetailPageCommand)
            Try
                With Me.State
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
                Me.State.IsRulesEditing = False
                Me.EnableDisableFields()
                PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
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
                SelectedWQ = Me.State.MyBO.GetWorkQyueueSelectionView()
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

        Protected Sub ExecuteSearchFilter(ByVal sender As Object, ByVal args As SearchAvailableQuestionsEventArgs) Handles UserControlQuestionsAvailable.ExecuteSearchFilter
            Dim issues As New Issue
            Try
                If Not Me.State.ActiveOn = Nothing Then
                    args.dvAvailableQuestions = issues.ExecuteQuestionsListFilter(args.Issue, args.QuestionList, args.SearchTags, Me.State.MyBO.ActiveOn.ToString)
                End If
                PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try
        End Sub

        Protected Sub SaveClicked(ByVal sender As Object, ByVal args As SearchAvailableQuestionsEventArgs) Handles UserControlQuestionsAvailable.EventSaveQuestionsListDetail
            Dim oQuestionList As ArrayList
            Dim oDisplayOrder As Integer = 1
            Dim dictQuestions As Hashtable

            Try
                oQuestionList = New ArrayList()
                Me.PopulateBOsFormFrom()
                dictQuestions = New Hashtable()
                For Each argQuestion As String In args.listSelectedQuestions
                    dictQuestions.Add(argQuestion, oDisplayOrder)
                    oDisplayOrder += 1
                Next

                oQuestionList = IssueQuestionList.GetQuestionList(Me.State.MyBO.Id)
                For Each argQuestion As String In args.listSelectedQuestions
                    For Each questionRaw As Byte() In oQuestionList
                        If New Guid(questionRaw).ToString = argQuestion Then
                            oQuestionList.Remove(questionRaw)
                            Exit For
                        End If
                    Next
                    Me.BeginQuestionsChildEdit(New Guid(argQuestion), False, CInt(dictQuestions.Item(argQuestion)))
                    Me.EndQuestionsChildEdit(ElitaPlusPage.DetailPageCommand.OK)
                Next
                For Each Question As Byte() In oQuestionList
                    Me.BeginQuestionsChildEdit(New Guid(Question), True, CInt(dictQuestions.Item(Question)))
                    Me.EndQuestionsChildEdit(ElitaPlusPage.DetailPageCommand.OK)
                Next
                Me.State.HasDataChanged = True
                Me.State.IsQuestionsEditing = False
                Me.EnableDisableFields()
                Me.EnableDisableParentControls(True)
                Me.EnableDisableUserControlTab(PanelQuestionsEditDetail, False)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try
        End Sub

        Protected Sub CancelButtonClicked(ByVal sender As Object, ByVal args As SearchAvailableQuestionsEventArgs) Handles UserControlQuestionsAvailable.EventCancelButtonClicked
            Try
                UserControlQuestionsAvailable.dvSelectedQuestions = Issue.GetSelectedQuestionsList(Me.State.MyBO.Id)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try
        End Sub

#End Region

#Region "Rules"

        Protected Sub ExecuteDealerSearchFilter(ByVal sender As Object, ByVal args As SearchAvailableDealerEventArgs) Handles UserControlRulesAvailable.ExecuteDealerSearchFilter
            Dim issues As New Issue
            Try
                args.dvAvailableDealer = issues.ExecuteRulesListFilter(Me.State.MyBO.Id)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try
        End Sub

        Protected Sub SaveClicked(ByVal sender As Object, ByVal args As SearchAvailableDealerEventArgs) Handles UserControlRulesAvailable.EventSaveDealerListDetail
            Dim oRuleList As ArrayList

            Try
                oRuleList = New ArrayList()
                Me.PopulateBOsFormFrom()

                oRuleList = RuleIssue.GetRulesInList(Me.State.MyBO.Id)
                For Each argQuestion As String In args.listSelectedDealer
                    For Each questionRaw As Byte() In oRuleList
                        If New Guid(questionRaw).ToString = argQuestion Then
                            oRuleList.Remove(questionRaw)
                            Exit For
                        End If
                    Next
                    Me.BeginRulesChildEdit(New Guid(argQuestion), False)
                    Me.EndRulesChildEdit(ElitaPlusPage.DetailPageCommand.OK)
                Next
                For Each Question As Byte() In oRuleList
                    Me.BeginRulesChildEdit(New Guid(Question), True)
                    Me.EndRulesChildEdit(ElitaPlusPage.DetailPageCommand.OK)
                Next
                Me.State.HasDataChanged = True
                Me.State.IsRulesEditing = False
                Me.EnableDisableFields()
                Me.EnableDisableParentControls(True)
                Me.EnableDisableUserControlTab(PanelRulesEditDetail, False)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try
        End Sub

        Protected Sub CancelButtonClicked(ByVal sender As Object, ByVal args As SearchAvailableDealerEventArgs) Handles UserControlRulesAvailable.EventCancelButtonClicked
            Try
                Dim Issue As New Issue
                UserControlRulesAvailable.dvSelectedDealer = Issue.GetSelectedRulesList(Me.State.MyBO.Id)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try
        End Sub

#End Region

#End Region

#Region "Detail GVNotes Events"

#Region "Notes"

        Private Sub PopulateCommentBOFromForm()
            Try
                With Me.State.MyNotesChildBO
                    .IssueCommentTypeId = GetSelectedItem(CType(Me.GVNotes.Items(Me.GVNotes.EditItemIndex).Cells(GRID_COL_NOTE_TYPE_IDX).FindControl(COMBO_NOTE_TYPE), DropDownList))
                    .Code = CType(Me.GVNotes.Items(Me.GVNotes.EditItemIndex).Cells(GRID_COL_CODE_IDX).FindControl(TEXT_CODE), TextBox).Text
                    .Text = CType(GVNotes.Items(GVNotes.EditItemIndex).Cells(GRID_COL_NOTE_IDX).FindControl(TEXT_NOTE), TextBox).Text
                    .DisplayOnWeb = LookupListNew.GetIdFromCode(LookupListNew.LK_LANG_INDEPENDENT_YES_NO, Codes.YESNO_Y)
                End With
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

        Private Sub ReturnFromEditing()
            GVNotes.EditItemIndex = NO_ROW_SELECTED_INDEX
            SetGridControls(GVNotes, True)
            Me.State.IsEditMode = False
            Me.PopulateGrid()
            Me.State.PageIndex = GVNotes.CurrentPageIndex
            EnableDisableFields()
        End Sub

        Private Sub SortAndBindGrid()
            Me.State.PageIndex = Me.GVNotes.CurrentPageIndex
            Me.GVNotes.DataSource = Me.State.SearchDV
            HighLightSortColumn(GVNotes, Me.State.SortExpression)
            Me.GVNotes.DataBind()
            Session("recCount") = Me.State.SearchDV.Count
            If Me.GVNotes.Visible Then
                If (Me.State.AddingNewRow) Then
                    Me.lblRecordCount.Text = (Me.State.SearchDV.Count - 1) & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                Else
                    Me.lblRecordCount.Text = Me.State.SearchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                End If
            End If
            ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me, GVNotes)
        End Sub

        Private Sub PopulateGrid()
            Dim oIssueComment As IssueComment = New IssueComment()
            Try
                Me.State.Code = moCodeText.Text
                Me.State.Description = moDescriptionText.Text
                Me.State.SearchDV = Me.State.MyBO.GetNotesSelectionView()

                Me.State.SearchDV.Sort = Me.State.SortExpression
                Me.SetGridItemStyleColor(Me.GVNotes)

                If (Me.State.IsAfterSave) Then
                    Me.State.IsAfterSave = False
                    Me.SetPageAndSelectedIndexFromGuid(Me.State.SearchDV, Me.State.SelectedNotesChildId, Me.GVNotes, Me.State.PageIndex)
                ElseIf (Me.State.IsNotesEditing) Then
                    Me.SetPageAndSelectedIndexFromGuid(Me.State.SearchDV, Me.State.SelectedNotesChildId, Me.GVNotes, Me.State.PageIndex, Me.State.IsEditMode)
                Else
                    Me.SetPageAndSelectedIndexFromGuid(Me.State.SearchDV, Me.State.SelectedNotesChildId, Me.GVNotes, Me.State.PageIndex)
                End If
                Me.State.PageIndex = Me.GVNotes.CurrentPageIndex

                If Me.State.SearchDV.Count > 0 Then
                    Me.GVNotes.AutoGenerateColumns = False
                    Me.ValidSearchResultCount(Me.State.SearchDV.Count, True)
                    Me.SortAndBindGrid()
                Else
                    If Me.State.MyNotesChildBO Is Nothing Then
                        Me.State.MyNotesChildBO = New IssueComment()
                    End If
                    Me.State.SearchDV = Me.State.MyNotesChildBO.GetNewDataViewRow(Me.State.SearchDV, Me.State.MyBO.Id, Me.State.MyNotesChildBO)
                    Me.SortAndBindGrid()
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

        Private Function GetGridDataView() As DataView
            With State
                Return IssueComment.GetList(Me.State.MyBO.Id)
            End With

        End Function

        Protected Sub Notes_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
            Try
                MyBase.BaseItemCreated(sender, e)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try
        End Sub

        'The Binding Logic is here
        Private Sub GridItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles GVNotes.ItemDataBound
            Dim itemType As ListItemType = e.Item.ItemType
            Dim dvRow As DataRowView = CType(e.Item.DataItem, DataRowView)

            If (itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem) Then
                Dim dRow As DataRow
                e.Item.Cells(GRID_COL_NOTE_ID_IDX).Text = GetGuidStringFromByteArray(CType(dvRow(IssueComment.IssueCommentGridDV.COL_NAME_ISSUE_COMMENT_ID), Byte()))
                e.Item.Cells(GRID_COL_CODE_IDX).Text = dvRow(IssueComment.IssueCommentGridDV.COL_NAME_CODE).ToString
                e.Item.Cells(GRID_COL_NOTE_IDX).Text = dvRow(IssueComment.IssueCommentGridDV.COL_NAME_TEXT).ToString
                dRow = FilterDatasetRowById(CType(dvRow(IssueComment.IssueCommentGridDV.COL_NAME_ISSUE_COMMENT_TYPE_ID), Byte()), GuidControl.GuidToHexString(ElitaPlusIdentity.Current.ActiveUser.LanguageId))
                If (Not dRow Is Nothing) Then
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

                If Not Me.State.MyNotesChildBO.IssueCommentTypeId.Equals(Guid.Empty) Then
                    SetSelectedItem(CType(e.Item.Cells(GRID_COL_NOTE_TYPE_IDX).FindControl(COMBO_NOTE_TYPE), DropDownList), Me.State.MyNotesChildBO.IssueCommentTypeId)
                End If
                CType(e.Item.Cells(GRID_COL_CODE_IDX).FindControl(TEXT_CODE), TextBox).Text = Me.State.MyNotesChildBO.Code
                CType(e.Item.Cells(GRID_COL_NOTE_IDX).FindControl(TEXT_NOTE), TextBox).Text = Me.State.MyNotesChildBO.Text
            End If

        End Sub

        Public Function FilterDatasetRowById(ByVal id As Byte(), ByVal lang As String) As System.Data.DataRow
            For Each row As DataRow In Me.State.NoteDV.Table.Rows
                If CType(row("ID"), Byte()).SequenceEqual(id) Then
                    Return row
                End If
            Next
        End Function

        Protected Sub ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs)
            Try
                If (e.CommandName = EDIT_COMMAND) Then
                    Me.State.IsNotesEditing = True
                    Try
                        Me.State.SelectedNotesChildId = GetGuidFromString(Me.GVNotes.Items(e.Item.ItemIndex).Cells(GRID_COL_NOTE_ID_IDX).Text)
                        Me.BeginNotesChildEdit(Me.State.SelectedNotesChildId, False)
                        Me.State.PageIndex = GVNotes.CurrentPageIndex
                        Me.PopulateGrid()
                        Me.SetGridControls(Me.GVNotes, False)
                        Me.SetFocusOnEditableFieldInGrid(Me.GVNotes, GRID_COL_CODE_IDX, TEXT_CODE, e.Item.ItemIndex)
                        Me.EnableDisableFields()
                    Catch ex As Exception
                        Me.State.MyBO.RejectChanges()
                        Throw ex
                    End Try
                ElseIf (e.CommandName = DELETE_COMMAND) Then
                    GVNotes.SelectedIndex = NO_ROW_SELECTED_INDEX
                    Try
                        Me.State.SelectedNotesChildId = GetGuidFromString(Me.GVNotes.Items(e.Item.ItemIndex).Cells(GRID_COL_NOTE_ID_IDX).Text)
                        Me.State.MyNotesChildBO = New IssueComment(Me.State.SelectedNotesChildId, Me.State.MyBO.MyDataset)
                        Me.BeginNotesChildEdit(Me.State.SelectedNotesChildId, False)
                        Me.EndNotesChildEdit(ElitaPlusPage.DetailPageCommand.Delete)
                        Me.State.PageIndex = GVNotes.CurrentPageIndex
                        Me.PopulateGrid()
                    Catch ex As Exception
                        Me.State.MyBO.RejectChanges()
                        Throw ex
                    End Try
                    Me.State.PageIndex = GVNotes.CurrentPageIndex
                End If
            Catch ex As Exception

                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

        'Protected Sub ItemBound(ByVal source As Object, ByVal e As DataGridItemEventArgs) Handles GVNotes.ItemDataBound
        '    BaseItemBound(source, e)
        'End Sub

        Protected Sub ItemCreated(ByVal sender As Object, ByVal e As DataGridItemEventArgs)
            BaseItemCreated(sender, e)
        End Sub

        Private Sub GridPageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles GVNotes.PageIndexChanged
            Try
                Me.State.PageIndex = e.NewPageIndex
                Me.GVNotes.CurrentPageIndex = Me.State.PageIndex
                Me.PopulateGrid()
                Me.GVNotes.SelectedIndex = NO_ITEM_SELECTED_INDEX
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

        Private Sub GridPageSizeChanged(ByVal source As Object, ByVal e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
            Try
                GVNotes.CurrentPageIndex = NewCurrentPageIndex(GVNotes, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
                Me.State.SelectedPageSize = CType(cboPageSize.SelectedValue, Integer)
                Me.PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

        Private Sub GridSortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles GVNotes.SortCommand
            Try
                If Me.State.SortExpression.StartsWith(e.SortExpression) Then
                    If Me.State.SortExpression.EndsWith(" DESC") Then
                        Me.State.SortExpression = e.SortExpression
                    Else
                        Me.State.SortExpression &= " DESC"
                    End If
                Else
                    Me.State.SortExpression = e.SortExpression
                End If
                Me.State.PageIndex = 0
                Me.PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

        Protected Sub BindBoPropertiesToGridHeaders()
            Me.BindBOPropertyToGridHeader(Me.State.MyNotesChildBO, "IssueCommentTypeId", Me.GVNotes.Columns(GRID_COL_NOTE_TYPE_IDX))
            Me.BindBOPropertyToGridHeader(Me.State.MyNotesChildBO, "CODE", Me.GVNotes.Columns(GRID_COL_CODE_IDX))
            Me.BindBOPropertyToGridHeader(Me.State.MyNotesChildBO, "TEXT", Me.GVNotes.Columns(GRID_COL_NOTE_IDX))
            Me.ClearGridHeadersAndLabelsErrSign()
        End Sub

        Private Sub SetFocusOnEditableFieldInGrid(ByVal thisGrid As DataGrid, ByVal cellPosition As Integer, ByVal controlName As String, ByVal itemIndex As Integer)
            Dim desc As TextBox = CType(thisGrid.Items(itemIndex).Cells(cellPosition).FindControl(controlName), TextBox)
            SetFocus(desc)
        End Sub

#End Region

#End Region

        Private Sub btnCancel_WQ_Click(sender As Object, e As System.EventArgs) Handles btnCancel_WQ.Click
            Try
                PopulateWorkQueue()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try
        End Sub

        Private Sub btnSave_WQ_Click(sender As Object, e As System.EventArgs) Handles btnSave_WQ.Click
            Try
                Me.State.MyBO.SaveWorkQueueIssue(UC_QUEUE_AVASEL.SelectedList)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try
        End Sub
    End Class

End Namespace
