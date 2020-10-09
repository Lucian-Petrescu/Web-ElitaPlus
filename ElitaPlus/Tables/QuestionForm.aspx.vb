Imports System.Globalization
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports System.Threading
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms
Partial Class Questionform
    Inherits ElitaPlusSearchPage

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()

    End Sub

    Private Sub Page_Init(sender As System.Object, e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Constants"
    Public Const URL As String = "QuestionForm.aspx"

    Public Const GRID_COL_EDIT As Integer = 0
    Public Const GRID_COL_DELETE As Integer = 1

    'Related Equipment
    Public Const GRID_COL_ANSWER_ID As Integer = 2
    Public Const GRID_COL_ANSWER_ORDER As Integer = 3
    Public Const GRID_COL_ANSWER_CODE As Integer = 4
    Public Const GRID_COL_ANSWER_VALUE As Integer = 5
    Public Const GRID_COL_DESCRIPTION As Integer = 6
    Public Const GRID_COL_SUPPORTS_CLAIM As Integer = 7
    Public Const GRID_COL_SCORE As Integer = 8
    Public Const GRID_COL_EFFECTIVE As Integer = 9
    Public Const GRID_COL_EXPIRATION As Integer = 10



    'control in Related Equipment Grid
    Public Const GRID_CONTROL_SUPPORTS_CLAIM As String = "ddlSupports_Claim"
    Public Const GRID_CONTROL_ANSWER_CODE As String = "lblAnswerCode"
    Public Const GRID_CONTROL_ANSWER_ORDER As String = "txtAnswerOrder"
    Public Const GRID_CONTROL_ANSWER_VALUE As String = "txtAnswerValue"
    Public Const GRID_CONTROL_ANSWER_SCORE As String = "txtScore"
    Public Const GRID_CONTROL_ANSWER_DESCRIPTION As String = "txtDescription"
    Public Const GRID_CONTROL_EFFECTIVE As String = "txtEffective"
    Public Const GRID_CONTROL_EXPIRATION As String = "txtExpiration"
    Public Const GRID_CONTROL_BTN_EFFECTIVE As String = "btnEffectiveDate"
    Public Const GRID_CONTROL_BTN_EXPIRATION As String = "btnExpirationDate"
    Public Const GRID_CONTROL_CLEAR_CODE As String = "btnClearCode"
    'Issue Constants
    Public Const QTYP_ISSUE As String = "ISSUE"
    Public Const ANSWER_VALUE As String = "ANSWER_VALUE"
    Public Const SCORE As String = "SCORE"
    Public Const SUPPORTS_CLAIM_ID As String = "SUPPORTS_CLAIM_ID"
    Public Const ELP_ANSWER As String = "ELP_ANSWER"

    Private Const TAB_TOTAL_COUNT As Integer = 1
    Private Const TAB_ANSWER As Integer = 0
#End Region
#Region "Variables"
    Private SelectedTabIndex As String = "0"
    Private DisableTabIndex As String = ""
#End Region
#Region "Page Return Type"
    Public Class ReturnType
        Public LastOperation As DetailPageCommand
        Public EditingBo As Question
        Public HasDataChanged As Boolean
        Public Sub New(LastOp As DetailPageCommand, curEditingBo As Question, hasDataChanged As Boolean)
            LastOperation = LastOp
            EditingBo = curEditingBo
            Me.HasDataChanged = hasDataChanged
        End Sub
    End Class
#End Region


#Region "Page State"

    Class MyState
        Public MyBO As Question
        Public MyAnswerChildBO As Answer
        Public MyAnswerChildBOOld As Answer
        Public ScreenSnapShotBO As Question
        Public screenSnapAnswerChildBO As Answer
        Public SoftQuestionGrp As SoftQuestionGroup
        Public IsAnswerEditing As Boolean = False
        Public OverlapExists As Boolean = False
        Public SortExpressionAnswerDetailGrid As String = Question.AnswerSelectionView.COL_NAME_ORDER & "," & Question.AnswerSelectionView.COL_NAME_EFFECTIVE
        Public AnswerDetailPageIndex As Integer = 0
        Public AnswerSelectedChildId As Guid = Guid.Empty
        Public ActionInProgress As DetailPageCommand = DetailPageCommand.Nothing_
        Public LastErrMsg As String
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

    Private Sub Page_PageCall(CallFromUrl As String, CallingPar As Object) Handles MyBase.PageCall
        Try
            If CallingParameters IsNot Nothing Then
                'Get the id from the parent
                State.MyBO = New Question(CType(CallingParameters, Guid))
                State.SoftQuestionGrp = State.MyBO.GetSoftQuestionGroup()
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

#End Region

#Region "Page Events"

    Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Try
            ErrorCtrl.Clear_Hide()
            If Not IsPostBack Then
                'Date Calendars
                '  Me.MenuEnabled = False
                AddControlMsg(btnDelete_WRITE, Message.DELETE_RECORD_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, True)
                If State.MyBO Is Nothing Then
                    State.MyBO = New Question
                    State.SoftQuestionGrp = State.MyBO.GetSoftQuestionGroup()
                    State.MyBO.SoftQuestionGroupId = State.SoftQuestionGrp.Id
                End If
                PopulateDropdowns()
                TranslateGridHeader(GVAnswers)
                PopulateFormFromBOs()
                EnableDisableFields()
            Else
                SelectedTabIndex = hdnSelectedTab.Value
                DisableTabIndex = hdnDisabledTab.Value
            End If
            BindBoPropertiesToLabels()
            BindBoPropertiesToGridHeader()
            CheckIfComingFromSaveConfirm()
            If Not IsPostBack Then
                AddLabelDecorations(State.MyBO)
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
        ShowMissingTranslations(ErrorCtrl)
    End Sub
    Private Sub Questionform_PreRender(sender As Object, e As EventArgs) Handles Me.PreRender
        hdnSelectedTab.Value = SelectedTabIndex
        hdnDisabledTab.Value = DisableTabIndex
    End Sub
#End Region

#Region "Controlling Logic"

    Protected Sub EnableDisableFields()

        'Enabled by Default
        ControlMgr.SetEnableControl(Me, btnDelete_WRITE, True)
        ControlMgr.SetEnableControl(Me, btnNew_WRITE, True)
        ControlMgr.SetEnableControl(Me, btnCopy_WRITE, True)
        'Now disable depebding on the object state
        If State.MyBO.IsNew Then
            ControlMgr.SetEnableControl(Me, btnDelete_WRITE, False)
            ControlMgr.SetEnableControl(Me, btnNew_WRITE, False)
            ControlMgr.SetEnableControl(Me, btnCopy_WRITE, False)
        End If
        EnableDisableaNSWERControl(False)
        'WRITE YOU OWN CODE HERE
    End Sub

    Sub EnableDisableParentControls(enableToggle As Boolean)
        ControlMgr.SetEnableControl(Me, btnBack, enableToggle)
        ControlMgr.SetEnableControl(Me, btnCopy_WRITE, enableToggle)
        ControlMgr.SetEnableControl(Me, btnDelete_WRITE, enableToggle)
        ControlMgr.SetEnableControl(Me, btnSave_WRITE, enableToggle)
        ControlMgr.SetEnableControl(Me, btnUndo_Write, enableToggle)
        ControlMgr.SetEnableControl(Me, btnDelete_WRITE, enableToggle)

        'text box and dropdown lists
        ControlMgr.SetEnableControl(Me, txtCode, enableToggle)
        ControlMgr.SetEnableControl(Me, txtDescription, enableToggle)
        ControlMgr.SetEnableControl(Me, ddlAttribute, enableToggle)
        ControlMgr.SetEnableControl(Me, ddlanswerType, enableToggle)

        ControlMgr.SetEnableControl(Me, txtCustomerMessage, enableToggle)
        ControlMgr.SetEnableControl(Me, txtSearchTags, enableToggle)
        ControlMgr.SetEnableControl(Me, btneffective, enableToggle)
        ControlMgr.SetEnableControl(Me, btnExpiration, enableToggle)
        ControlMgr.SetEnableControl(Me, ddlImpactsClaim, enableToggle)
        ControlMgr.SetEnableControl(Me, txtExpirationDate, enableToggle)
        ControlMgr.SetEnableControl(Me, ddlQuestionType, enableToggle)
        ControlMgr.SetEnableControl(Me, txtEffective, enableToggle)

        If enableToggle Then        'if you are trying to enable we'll enable only those that need to
            With State.MyBO
                If Not .IsNew AndAlso Not .QuestionTypeId.Equals(Guid.Empty) Then
                    ControlMgr.SetEnableControl(Me, ddlQuestionType, False)
                Else
                    ControlMgr.SetEnableControl(Me, ddlQuestionType, True)
                End If

                If .IsNew AndAlso Not .Effective.Value.Date < DateTime.Now.Date Then
                    ControlMgr.SetEnableControl(Me, txtEffective, True)
                    ControlMgr.SetEnableControl(Me, btneffective, True)
                    ControlMgr.SetEnableControl(Me, txtCode, True)
                Else
                    ControlMgr.SetEnableControl(Me, txtEffective, False)
                    ControlMgr.SetEnableControl(Me, btneffective, False)
                    ControlMgr.SetEnableControl(Me, txtCode, False)
                End If
            End With
        End If
    End Sub

    Sub EnableDisableaNSWERControl(enableToggle As Boolean)
        ControlMgr.SetEnableControl(Me, btnCancelAnswer, enableToggle)
        ControlMgr.SetEnableControl(Me, btnSaveAnswer, enableToggle)
        ControlMgr.SetEnableControl(Me, btnNewAnswer, Not enableToggle)
    End Sub


    Protected Sub BindBoPropertiesToLabels()
        BindBOPropertyToLabel(State.MyBO, "Code", lblCode)
        BindBOPropertyToLabel(State.MyBO, "Description", lblDescription)
        BindBOPropertyToLabel(State.MyBO, "Effective", lbleffectiveDate)
        BindBOPropertyToLabel(State.MyBO, "Expiration", lblExpirationDate)
        BindBOPropertyToLabel(State.MyBO, "QuestionType", lblQuestionType)
        BindBOPropertyToLabel(State.MyBO, "AnswerType", lblAnswerType)
        BindBOPropertyToLabel(State.MyBO, "Attribute", lblAttribute)
        BindBOPropertyToLabel(State.MyBO, "CustomerMessage", lblCustomerMessage)
        BindBOPropertyToLabel(State.MyBO, "SearchTags", lblSearchTags)
        BindBOPropertyToLabel(State.MyBO, "ImpactsClaim", lblImpactsClaim)
        ClearGridViewHeadersAndLabelsErrSign()
    End Sub

    Private Sub BindBoPropertiesToGridHeader()
        BindBOPropertyToGridHeader(State.MyAnswerChildBO, "AnswerOrder", GVAnswers.Columns(GRID_COL_ANSWER_ORDER))
        BindBOPropertyToGridHeader(State.MyAnswerChildBO, "Code", GVAnswers.Columns(GRID_COL_ANSWER_CODE))
        BindBOPropertyToGridHeader(State.MyAnswerChildBO, "AnswerValue", GVAnswers.Columns(GRID_COL_ANSWER_VALUE)) 'DEF-2846
        BindBOPropertyToGridHeader(State.MyAnswerChildBO, "Description", GVAnswers.Columns(GRID_COL_DESCRIPTION))
        BindBOPropertyToGridHeader(State.MyAnswerChildBO, "SupportsClaimId", GVAnswers.Columns(GRID_COL_SUPPORTS_CLAIM))
        BindBOPropertyToGridHeader(State.MyAnswerChildBO, "Score", GVAnswers.Columns(GRID_COL_SCORE))
        BindBOPropertyToGridHeader(State.MyAnswerChildBO, "Effective", GVAnswers.Columns(GRID_COL_EFFECTIVE))
        BindBOPropertyToGridHeader(State.MyAnswerChildBO, "Expiration", GVAnswers.Columns(GRID_COL_EXPIRATION))
    End Sub

    Protected Sub PopulateDropdowns()
        Dim languageId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
        Dim companyGroupId As Guid = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id

        ' Me.BindListControlToDataView(ddlImpactsClaim, LookupListNew.GetYesNoLookupList(languageId))
        ddlImpactsClaim.Populate(CommonConfigManager.Current.ListManager.GetList("YESNO", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
                                           {
                                            .AddBlankItem = True
                                           })

        ' Me.BindListControlToDataView(ddlanswerType, LookupListNew.GetAnswerTypeLookupList(languageId)) 'GetAnswerType

        ddlanswerType.Populate(CommonConfigManager.Current.ListManager.GetList(ListCodes.GetAnswerType, Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
                                           {
                                            .AddBlankItem = True
                                           })

        ' Me.BindListControlToDataView(ddlQuestionType, LookupListNew.GetQuestionTypeLookupList(languageId))
        Dim queLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("QTYP", Thread.CurrentPrincipal.GetLanguageCode())
        Dim FilteredRecord As ListItem() = (From x In queLkl
                                            Where Not x.Code = "DIAG"
                                            Select x).ToArray()
        ddlQuestionType.Populate(FilteredRecord, New PopulateOptions() With
                                           {
                                            .AddBlankItem = True
                                           })

        ' Me.BindListControlToDataView(ddlAttribute, LookupListNew.GetEntityAttributeList(DateTime.Now)) 'DLL
        ddlAttribute.Populate(CommonConfigManager.Current.ListManager.GetList(ListCodes.GetEntityAttributes, Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
                                           {
                                            .AddBlankItem = True
                                           })
        AddCalendarwithTime(btneffective, txtEffective, , txtEffective.Text)
        AddCalendarwithTime(btnExpiration, txtExpirationDate, , txtExpirationDate.Text)
    End Sub

    Protected Sub PopulateFormFromBOs()
        With State.MyBO
            Dim languageId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
            SetGridItemStyleColor(GVAnswers)
            PopulateAnswerDetailGrid()
            PopulateControlFromBOProperty(ddlanswerType, .AnswerTypeId)
            PopulateControlFromBOProperty(ddlQuestionType, .QuestionTypeId)

            'Question type id -> don't allow to change for a saved/old record
            If Not .IsNew AndAlso Not .QuestionTypeId.Equals(Guid.Empty) Then
                ControlMgr.SetEnableControl(Me, ddlQuestionType, False)
            Else
                ControlMgr.SetEnableControl(Me, ddlQuestionType, True)
            End If

            If LookupListNew.GetCodeFromId(LookupListNew.GetQuestionTypeLookupList(languageId), .QuestionTypeId) = QTYP_ISSUE Then
                PopulateControlFromBOProperty(ddlAttribute, .EntityAttributeId)
                PopulateControlFromBOProperty(ddlImpactsClaim, .ImpactsClaimId)
            Else
                ControlMgr.SetEnableControl(Me, ddlImpactsClaim, False)
                ControlMgr.SetEnableControl(Me, ddlAttribute, False)
            End If
            'user should allowed to edit effective date of future questions 
            ''any list having effective Date (not time) less than today will not be able to change effective date
            If .IsNew AndAlso Not .Effective.Value.Date < DateTime.Now.Date Then
                ControlMgr.SetEnableControl(Me, txtEffective, True)
                ControlMgr.SetEnableControl(Me, btneffective, True)
                ControlMgr.SetEnableControl(Me, txtCode, True)
            Else
                ControlMgr.SetEnableControl(Me, txtEffective, False)
                ControlMgr.SetEnableControl(Me, btneffective, False)
                ControlMgr.SetEnableControl(Me, txtCode, False)
            End If

            PopulateControlFromBOProperty(txtDescription, .Description)
            PopulateControlFromBOProperty(txtCode, .Code)
            PopulateControlFromBOProperty(txtEffective, .Effective, "dd-MMM-yyyy HH:mm:ss tt")
            PopulateControlFromBOProperty(txtExpirationDate, .Expiration, "dd-MMM-yyyy HH:mm:ss tt")
            PopulateControlFromBOProperty(txtCustomerMessage, .CustomerMessage)
            PopulateControlFromBOProperty(txtSearchTags, .SearchTags)

            'IF question is expired then don't allow the user to make any change to it
            If State.MyBO.Expiration.Value < DateTime.Now Then
                EnableDisableParentControls(False)
                EnableTab(False)

                ControlMgr.SetEnableControl(Me, btnNewAnswer, False)
                ControlMgr.SetEnableControl(Me, btnSaveAnswer, False)
                ControlMgr.SetEnableControl(Me, btnCancelAnswer, False)
                ControlMgr.SetEnableControl(Me, btnDelete_WRITE, False)
                ControlMgr.SetEnableControl(Me, btnCopy_WRITE, True)
                ControlMgr.SetEnableControl(Me, btnBack, True)
            End If


        End With
    End Sub

    Sub PopulateAnswerDetailGrid()
        Try
            Dim dv As Question.AnswerSelectionView = State.MyBO.GetAnswerSelectionView()
            dv.Sort = State.SortExpressionAnswerDetailGrid
            Session("recCount") = dv.Count

            GVAnswers.Columns(GRID_COL_ANSWER_CODE).SortExpression = Question.AnswerSelectionView.COL_NAME_CODE
            GVAnswers.Columns(GRID_COL_ANSWER_ORDER).SortExpression = Question.AnswerSelectionView.COL_NAME_ORDER
            GVAnswers.Columns(GRID_COL_DESCRIPTION).SortExpression = Question.AnswerSelectionView.COL_NAME_DESCRIPTION
            GVAnswers.Columns(GRID_COL_SUPPORTS_CLAIM).SortExpression = Question.AnswerSelectionView.COL_NAME_SUPPORTS_CLAIM
            GVAnswers.Columns(GRID_COL_ANSWER_VALUE).SortExpression = Question.AnswerSelectionView.COL_NAME_VALUE
            GVAnswers.Columns(GRID_COL_EFFECTIVE).SortExpression = Question.AnswerSelectionView.COL_NAME_EFFECTIVE
            GVAnswers.Columns(GRID_COL_EXPIRATION).SortExpression = Question.AnswerSelectionView.COL_NAME_EXPIRATION
            SetGridItemStyleColor(GVAnswers)

            If State.IsAnswerEditing Then
                SetPageAndSelectedIndexFromGuid(dv, State.AnswerSelectedChildId, GVAnswers,
                                        GVAnswers.PageIndex, True)
            Else
                SetPageAndSelectedIndexFromGuid(dv, State.AnswerSelectedChildId, GVAnswers, State.AnswerDetailPageIndex)
                State.AnswerDetailPageIndex = GVAnswers.PageIndex
            End If


            If dv.Count > 0 Then
                GVAnswers.DataSource = dv
                GVAnswers.AutoGenerateColumns = False
                GVAnswers.DataBind()
                GVAnswers.PageIndex = NewCurrentPageIndex(GVAnswers, CType(Session("recCount"), Int32), CType(ddlPageSize.SelectedValue, Int32))
            Else
                dv.AddNew()
                dv(0)(0) = Guid.Empty.ToByteArray
                GVAnswers.DataSource = dv
                GVAnswers.DataBind()
                GVAnswers.Rows(0).Visible = False
                GVAnswers.Rows(0).Controls.Clear()
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try





    End Sub

    Protected Sub PopulateBOsFormFrom()
        With State.MyBO
            PopulateBOProperty(State.MyBO, "Code", txtCode)
            PopulateBOProperty(State.MyBO, "Description", txtDescription)
            PopulateBOProperty(State.MyBO, "Effective", txtEffective)
            PopulateBOProperty(State.MyBO, "Expiration", txtExpirationDate)
            PopulateBOProperty(State.MyBO, "QuestionTypeId", ddlQuestionType)
            PopulateBOProperty(State.MyBO, "AnswerTypeId", ddlanswerType)
            PopulateBOProperty(State.MyBO, "EntityAttributeId", ddlAttribute)
            PopulateBOProperty(State.MyBO, "ImpactsClaimId", ddlImpactsClaim)
            PopulateBOProperty(State.MyBO, "CustomerMessage", txtCustomerMessage)
            PopulateBOProperty(State.MyBO, "SearchTags", txtSearchTags)

            'set the description of soft question group same as question 
            PopulateBOProperty(State.SoftQuestionGrp, "Description", txtDescription)

        End With
        If ErrCollection.Count > 0 Then
            Throw New PopulateBOErrorException
        End If
    End Sub

    Protected Sub CreateNew()
        State.ScreenSnapShotBO = Nothing 'Reset the backup copy
        State.MyBO = New Question
        State.SoftQuestionGrp = State.MyBO.GetSoftQuestionGroup()
        State.MyBO.SoftQuestionGroupId = State.SoftQuestionGrp.Id
        PopulateFormFromBOs()
        EnableDisableFields()
    End Sub

    Protected Sub CreateNewWithCopy()
        Dim newObj As New Question
        newObj.Copy(State.MyBO)
        State.MyBO = newObj
        State.SoftQuestionGrp = State.MyBO.GetSoftQuestionGroup()
        State.MyBO.SoftQuestionGroupId = State.SoftQuestionGrp.Id
        EnableDisableParentControls(True)
        PopulateFormFromBOs()
        EnableDisableFields()
        'create the backup copy
        State.ScreenSnapShotBO = New Question
        State.ScreenSnapShotBO.Copy(State.MyBO)
    End Sub

    Protected Sub CheckIfComingFromSaveConfirm()
        Dim confResponse As String = HiddenSaveChangesPromptResponse.Value
        If confResponse IsNot Nothing AndAlso confResponse = MSG_VALUE_YES Then
            If State.ActionInProgress <> ElitaPlusPage.DetailPageCommand.BackOnErr AndAlso State.ActionInProgress <> ElitaPlusPage.DetailPageCommand.Accept Then
                State.MyBO.Save()
            End If
            Select Case State.ActionInProgress
                Case ElitaPlusPage.DetailPageCommand.Back
                    ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, State.MyBO, State.HasDataChanged))
                Case ElitaPlusPage.DetailPageCommand.Delete

                Case ElitaPlusPage.DetailPageCommand.New_
                    DisplayMessage(Message.SAVE_RECORD_CONFIRMATION, "", MSG_BTN_OK, MSG_TYPE_INFO)
                    CreateNew()
                Case ElitaPlusPage.DetailPageCommand.NewAndCopy
                    DisplayMessage(Message.SAVE_RECORD_CONFIRMATION, "", MSG_BTN_OK, MSG_TYPE_INFO)
                    CreateNewWithCopy()
                Case ElitaPlusPage.DetailPageCommand.BackOnErr
                    ReturnToCallingPage(New ReturnType(State.ActionInProgress, State.MyBO, State.HasDataChanged))
                Case ElitaPlusPage.DetailPageCommand.Accept
                    If State.IsAnswerEditing Then
                        PopulateAnswerChildBOFromDetail()
                        State.MyAnswerChildBO.Save()
                        State.MyAnswerChildBO.EndEdit()
                        State.IsAnswerEditing = False
                    End If
                    If State.OverlapExists Then
                        If State.MyBO.IsDirty Then
                            If State.MyBO.ExpireOverLappingQuestions() Then
                                State.SoftQuestionGrp.Save()
                                State.MyBO.Save()
                                State.HasDataChanged = False
                                PopulateFormFromBOs()
                                EnableDisableFields()
                                DisplayMessage(Message.SAVE_RECORD_CONFIRMATION, "", MSG_BTN_OK, MSG_TYPE_INFO)
                            End If
                        Else
                            DisplayMessage(Message.MSG_RECORD_NOT_SAVED, "", MSG_BTN_OK, MSG_TYPE_INFO)
                        End If
                        State.OverlapExists = False
                    Else
                        DisplayMessage(Message.MSG_RECORD_NOT_SAVED, "", MSG_BTN_OK, MSG_TYPE_INFO)
                    End If
                    EnableDisableFields()
                    PopulateAnswerDetailGrid()
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
                    If State.IsAnswerEditing Then
                        State.MyAnswerChildBO.cancelEdit()
                        If State.MyAnswerChildBO.IsSaveNew Then
                            State.MyAnswerChildBO.Delete()
                            State.MyAnswerChildBO.Save()
                        End If
                        State.IsAnswerEditing = False
                    End If
                    EnableDisableFields()
                    PopulateAnswerDetailGrid()
            End Select
        End If
        'Clean after consuming the action
        State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
        HiddenSaveChangesPromptResponse.Value = ""
    End Sub

    Sub EndAnswerChildEdit(lastop As ElitaPlusPage.DetailPageCommand)
        Try
            With State
                Select Case lastop
                    Case ElitaPlusPage.DetailPageCommand.OK
                        PopulateAnswerChildBOFromDetail()
                        .MyAnswerChildBO.Save()
                        .MyAnswerChildBO.EndEdit()
                    Case ElitaPlusPage.DetailPageCommand.Cancel
                        .MyAnswerChildBO.cancelEdit()
                        If .MyAnswerChildBO.IsSaveNew Then
                            .MyAnswerChildBO.Delete()
                            .MyAnswerChildBO.Save()
                        End If
                    Case ElitaPlusPage.DetailPageCommand.Delete
                        .MyAnswerChildBO.Accept(New ExpirationVisitor)
                        .MyAnswerChildBO.Save()
                        .MyAnswerChildBO.EndEdit()
                        .AnswerSelectedChildId = Guid.Empty
                End Select
            End With
            State.IsAnswerEditing = False
            EnableDisableFields()
            PopulateAnswerDetailGrid()
            EnableDisableaNSWERControl(False)
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

    Sub BeginAnswerChildEdit()
        State.IsAnswerEditing = True
        With State
            If Not .AnswerSelectedChildId.Equals(Guid.Empty) Then
                .MyAnswerChildBO = .MyBO.GetAnswerChild(.AnswerSelectedChildId)
            Else
                .MyAnswerChildBO = .MyBO.GetNewAnswerChild
            End If
            .MyAnswerChildBO.BeginEdit()
        End With
        EnableDisableaNSWERControl(True)
    End Sub

    Sub PopulateDetailFromAnswerChildBO()
        'ensure that grid's edit index is set before this gets a call
        If GVAnswers.EditIndex = -1 Then Exit Sub
        Dim gRow As GridViewRow = GVAnswers.Rows(GVAnswers.EditIndex)
        Dim ddlSupportsClaim As DropDownList = CType(gRow.Cells(GRID_COL_SUPPORTS_CLAIM).FindControl(GRID_CONTROL_SUPPORTS_CLAIM), DropDownList)
        Dim txtCommon As TextBox

        With State.MyAnswerChildBO
            If ddlSupportsClaim IsNot Nothing Then
                If Not .SupportsClaimId = Guid.Empty Then
                    PopulateControlFromBOProperty(ddlSupportsClaim, .SupportsClaimId)
                End If
            End If

            Dim lblCommon As Label = CType(gRow.Cells(GRID_COL_ANSWER_CODE).FindControl(GRID_CONTROL_ANSWER_CODE), Label)
            If lblCommon IsNot Nothing Then
                PopulateControlFromBOProperty(lblCommon, .Code)
            End If

            Dim imgbtn As ImageButton = CType(gRow.Cells(GRID_COL_ANSWER_CODE).FindControl(GRID_CONTROL_CLEAR_CODE), ImageButton)
            imgbtn.Attributes.Add("onclick", "javascript:return ClearCodeValue('" & GVAnswers.ClientID & "','" & GVAnswers.EditIndex & "')")


            txtCommon = CType(gRow.Cells(GRID_COL_DESCRIPTION).FindControl(GRID_CONTROL_ANSWER_DESCRIPTION), TextBox)
            If txtCommon IsNot Nothing Then
                PopulateControlFromBOProperty(txtCommon, .Description)
            End If

            'user should allowed to edit effective date of future questions 
            ''any list having effective Date (not time) less than today will not be able to change effective date
            txtCommon = CType(gRow.Cells(GRID_COL_EFFECTIVE).FindControl(GRID_CONTROL_EFFECTIVE), TextBox)
            If txtCommon IsNot Nothing Then
                PopulateControlFromBOProperty(txtCommon, .Effective)
            End If

            imgbtn = CType(gRow.Cells(GRID_COL_ANSWER_CODE).FindControl(GRID_CONTROL_BTN_EFFECTIVE), ImageButton)
            If Not .Effective.Value.Date < DateTime.Now.Date Then
                ControlMgr.SetEnableControl(Me, txtCommon, True)
                If imgbtn IsNot Nothing Then
                    ControlMgr.SetEnableControl(Me, imgbtn, True)
                End If
            Else
                ControlMgr.SetEnableControl(Me, txtCommon, False)
                If imgbtn IsNot Nothing Then
                    ControlMgr.SetEnableControl(Me, imgbtn, False)
                End If
            End If

            txtCommon = CType(gRow.Cells(GRID_COL_EXPIRATION).FindControl(GRID_CONTROL_EXPIRATION), TextBox)
            If txtCommon IsNot Nothing Then
                PopulateControlFromBOProperty(txtCommon, .Expiration)
            End If

            txtCommon = CType(gRow.Cells(GRID_COL_ANSWER_VALUE).FindControl(GRID_CONTROL_ANSWER_VALUE), TextBox)
            If txtCommon IsNot Nothing Then
                PopulateControlFromBOProperty(txtCommon, .AnswerValue)
            End If

            txtCommon = CType(gRow.Cells(GRID_COL_ANSWER_ORDER).FindControl(GRID_CONTROL_ANSWER_ORDER), TextBox)
            If txtCommon IsNot Nothing Then
                PopulateControlFromBOProperty(txtCommon, .AnswerOrder)
            End If

            txtCommon = CType(gRow.Cells(GRID_COL_SCORE).FindControl(GRID_CONTROL_ANSWER_SCORE), TextBox)
            If txtCommon IsNot Nothing Then
                PopulateControlFromBOProperty(txtCommon, .Score)
            End If

        End With
    End Sub


    Public Sub SetGridRowControls(gridrow As GridViewRow, enable As Boolean)
        Dim i As Integer
        Dim edt As ImageButton
        Dim del As ImageButton

        'grid.AllowSorting = enable  ' Enable/Disable the sorting
        '  Enable or Disable EDIT and DELETE buttons on the GridRow

        edt = CType(gridrow.Cells(EDIT_COL).FindControl(EDIT_CONTROL_NAME), ImageButton)
        If edt IsNot Nothing Then
            edt.Enabled = enable
            edt.Visible = enable
        End If

        del = CType(gridrow.Cells(DELETE_COL).FindControl(DELETE_CONTROL_NAME), ImageButton)
        If del IsNot Nothing Then
            del.Enabled = enable
            del.Visible = enable
        End If


    End Sub


    Sub PopulateAnswerChildBOFromDetail()
        'ensure that grid's edit index is set before this gets a call
        If GVAnswers.EditIndex = -1 Then Exit Sub
        Dim gRow As GridViewRow = GVAnswers.Rows(GVAnswers.EditIndex)

        Dim ddlSupportsClaim As DropDownList = CType(GVAnswers.Rows(GVAnswers.EditIndex).Cells(GRID_COL_SUPPORTS_CLAIM).FindControl(GRID_CONTROL_SUPPORTS_CLAIM), DropDownList)
        If ddlSupportsClaim IsNot Nothing Then
            PopulateBOProperty(State.MyAnswerChildBO, "SupportsClaimId", New Guid(ddlSupportsClaim.SelectedValue))
        End If

        With State.MyAnswerChildBO
            Dim txtCommon As TextBox
            With State.MyAnswerChildBO
                Dim lblCommon As Label = CType(gRow.Cells(GRID_COL_ANSWER_CODE).FindControl(GRID_CONTROL_ANSWER_CODE), Label)
                If lblCommon IsNot Nothing Then
                    PopulateBOProperty(State.MyAnswerChildBO, "Code", lblCommon.Text)
                End If

                txtCommon = CType(gRow.Cells(GRID_COL_DESCRIPTION).FindControl(GRID_CONTROL_ANSWER_DESCRIPTION), TextBox)
                If txtCommon IsNot Nothing Then
                    PopulateBOProperty(State.MyAnswerChildBO, "Description", txtCommon)
                End If

                txtCommon = CType(gRow.Cells(GRID_COL_EFFECTIVE).FindControl(GRID_CONTROL_EFFECTIVE), TextBox)
                If txtCommon IsNot Nothing Then
                    PopulateBOProperty(State.MyAnswerChildBO, "Effective", txtCommon)
                End If

                txtCommon = CType(gRow.Cells(GRID_COL_EXPIRATION).FindControl(GRID_CONTROL_EXPIRATION), TextBox)
                If txtCommon IsNot Nothing Then
                    PopulateBOProperty(State.MyAnswerChildBO, "Expiration", txtCommon)
                End If

                txtCommon = CType(gRow.Cells(GRID_COL_ANSWER_VALUE).FindControl(GRID_CONTROL_ANSWER_VALUE), TextBox)
                If txtCommon IsNot Nothing Then
                    PopulateBOProperty(State.MyAnswerChildBO, "AnswerValue", txtCommon)
                End If

                txtCommon = CType(gRow.Cells(GRID_COL_ANSWER_ORDER).FindControl(GRID_CONTROL_ANSWER_ORDER), TextBox)
                If txtCommon IsNot Nothing Then
                    PopulateBOProperty(State.MyAnswerChildBO, "AnswerOrder", txtCommon)
                End If

                txtCommon = CType(gRow.Cells(GRID_COL_SCORE).FindControl(GRID_CONTROL_ANSWER_SCORE), TextBox)
                If txtCommon IsNot Nothing Then
                    PopulateBOProperty(State.MyAnswerChildBO, "Score", txtCommon)
                End If

                'Hardcoding for now to remove dependency of running scripts everytime an answer is added.
                'Needs to be fixed by creating a UI for it..
                If (.AnswerValue.ToUpperInvariant.Contains("YES") OrElse .AnswerValue.ToUpperInvariant.Contains("SI")) Then
                    State.MyAnswerChildBO.ListItemId = LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, Codes.YESNO_Y)
                End If

                If (.AnswerValue.ToUpperInvariant.Contains("NO")) Then
                    State.MyAnswerChildBO.ListItemId = LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, Codes.YESNO_N)
                End If

            End With
        End With
        If ErrCollection.Count > 0 Then
            Throw New PopulateBOErrorException
        End If
    End Sub
    Private Sub EnableTab(blnFlag As Boolean)
        If blnFlag = True Then 'enable
            SelectedTabIndex = "0"
            DisableTabIndex = String.Empty
        Else 'disable
            SelectedTabIndex = String.Empty
            DisableTabIndex = "0"
        End If
    End Sub
    Private Function CheckQuestionOverlap() As Boolean
        Return State.MyBO.Accept(New OverlapValidationVisitor)
    End Function

    Private Function CheckExistingFutureRuleOverlap() As Boolean
        Return State.MyBO.Accept(New FutureOverlapValidationVisitor)
    End Function


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
            PopulateBOsFormFrom()
            State.MyBO.Validate()
            If CheckQuestionOverlap() Then
                If CheckExistingFutureRuleOverlap() Then
                    Throw New GUIException(Message.MSG_GUI_OVERLAPPING_RULES, Assurant.ElitaPlus.Common.ErrorCodes.EQUIPMENT_LIST_CODE_OVERLAPPED)
                End If
                DisplayMessage(Message.MSG_GUI_OVERLAPPING_RECORDS, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                State.ActionInProgress = DetailPageCommand.Accept
                State.OverlapExists = True
                Exit Sub
            End If

            If State.MyBO.IsDirty Then
                State.SoftQuestionGrp.Save()
                State.MyBO.UpdateTranslation()
                State.MyBO.Save()
                State.HasDataChanged = False
                PopulateFormFromBOs()
                EnableDisableFields()
                ClearGridViewHeadersAndLabelsErrSign()
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
                State.MyBO = New Question(State.MyBO.Id)
            ElseIf State.ScreenSnapShotBO IsNot Nothing Then
                'It was a new with copy
                State.MyBO.Clone(State.ScreenSnapShotBO)
            Else
                State.MyBO = New Question
            End If
            PopulateFormFromBOs()
            EnableDisableFields()
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

    Private Sub btnDelete_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnDelete_WRITE.Click
        Try
            If State.MyBO.IsQuestionAssignedtoIssue Then
                Throw New GUIException(Message.MSG_GUI_QUESTION_ASSIGNED_TO_ISSUE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_LIST_CODE_ERR)
            Else
                If Not State.MyBO.Effective.Value > DateTime.Now Then
                    'for future effective date delete the question which has not been assigned to any question list
                    State.MyBO.Delete()
                    State.MyBO.Save()
                    State.HasDataChanged = True
                    ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Delete, State.MyBO, State.HasDataChanged))
                Else
                    'for current question - expire it by setting the expiry date
                    State.MyBO.Accept(New ExpirationVisitor)
                    State.MyBO.Save()
                    State.HasDataChanged = True
                    ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Expire, State.MyBO, State.HasDataChanged))
                End If
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
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

    Private Sub btnCopy_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnCopy_WRITE.Click
        Try
            PopulateBOsFormFrom()
            If State.MyBO.IsDirty Then
                DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO_CANCEL, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.NewAndCopy
            Else
                CreateNewWithCopy()
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

#End Region

#Region "Detail Grid Events"

    Public Sub ItemCreated(sender As System.Object, e As DataGridItemEventArgs)
        Try
            BaseItemCreated(sender, e)
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

#Region "Answers Grid"
    Protected Sub GVAnswers_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GVAnswers.PageIndexChanging
        Try
            State.AnswerDetailPageIndex = e.NewPageIndex
            State.AnswerSelectedChildId = Guid.Empty
            PopulateAnswerDetailGrid()
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

    Protected Sub GVAnswers_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs)
        Try
            Dim nIndex As Integer
            If e.CommandName = ElitaPlusSearchPage.EDIT_COMMAND_NAME Then
                nIndex = CInt(e.CommandArgument)
                State.AnswerSelectedChildId = New Guid(CType(GVAnswers.Rows(nIndex).Cells(GRID_COL_ANSWER_ID).Controls(0), Label).Text)
                State.IsAnswerEditing = True
                BeginAnswerChildEdit()
                PopulateAnswerDetailGrid()
                FillDropdownList()
                PopulateDetailFromAnswerChildBO()
                ElitaPlusSearchPage.SetGridControls(GVAnswers, False)
                EnableDisableFields()
                EnableDisableaNSWERControl(True)
            ElseIf e.CommandName = ElitaPlusSearchPage.DELETE_COMMAND_NAME Then
                nIndex = CInt(e.CommandArgument)
                State.AnswerSelectedChildId = New Guid(CType(GVAnswers.Rows(nIndex).Cells(GRID_COL_ANSWER_ID).Controls(0), Label).Text)
                State.IsAnswerEditing = True
                BeginAnswerChildEdit()
                EndAnswerChildEdit(ElitaPlusPage.DetailPageCommand.Delete)
                PopulateAnswerDetailGrid()
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

    Protected Sub GVAnswers_ItemCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs)
        Try
            MyBase.BaseItemCreated(sender, e)
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

    Private Sub GVAnswers_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GVAnswers.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            If Not State.IsAnswerEditing Then
                If CType(e.Row.Cells(GRID_COL_EXPIRATION).Controls(0), Label).Text <> "" Then
                    Dim expDate As DateTime = DateHelper.GetDateValue(CType(e.Row.Cells(GRID_COL_EXPIRATION).Controls(0), Label).Text)
                    If expDate < DateTime.Now Then
                        SetGridRowControls(e.Row, False)
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub GVAnswers_Sorting(source As Object, e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles GVAnswers.Sorting
        Try
            If State.SortExpressionAnswerDetailGrid.StartsWith(e.SortExpression) Then
                If State.SortExpressionAnswerDetailGrid.StartsWith(e.SortExpression & " DESC") Then
                    State.SortExpressionAnswerDetailGrid = e.SortExpression
                Else
                    State.SortExpressionAnswerDetailGrid = e.SortExpression & " DESC"
                End If
            Else
                State.SortExpressionAnswerDetailGrid = e.SortExpression
            End If
            PopulateAnswerDetailGrid()
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

    Private Sub FillDropdownList()

        'fill the drop downs
        If GVAnswers.EditIndex = -1 Then Exit Sub
        Dim gRow As GridViewRow = GVAnswers.Rows(GVAnswers.EditIndex)
        Dim moIsCoveredDrop As DropDownList = DirectCast(gRow.Cells(GRID_COL_SUPPORTS_CLAIM).FindControl(GRID_CONTROL_SUPPORTS_CLAIM), DropDownList)
        Dim languageId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId

        With State.MyAnswerChildBO
            If moIsCoveredDrop IsNot Nothing Then
                ' Me.BindListControlToDataView(moIsCoveredDrop, LookupListNew.GetYesNoLookupList(languageId), , , False)
                moIsCoveredDrop.Populate(CommonConfigManager.Current.ListManager.GetList("YESNO", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions())
            End If
        End With

        Dim btn As ImageButton = DirectCast(gRow.Cells(GRID_COL_EFFECTIVE).FindControl(GRID_CONTROL_BTN_EFFECTIVE), ImageButton)
        Dim btn1 As ImageButton = DirectCast(gRow.Cells(GRID_COL_EXPIRATION).FindControl(GRID_CONTROL_BTN_EXPIRATION), ImageButton)

        Dim txtcommon As TextBox
        txtcommon = CType(gRow.Cells(GRID_COL_EFFECTIVE).FindControl(GRID_CONTROL_EFFECTIVE), TextBox)
        If txtcommon IsNot Nothing Then
            AddCalendarwithTime(btn, txtcommon, , txtcommon.Text)
        End If

        txtcommon = CType(gRow.Cells(GRID_COL_EXPIRATION).FindControl(GRID_CONTROL_EXPIRATION), TextBox)
        If txtcommon IsNot Nothing Then
            AddCalendarwithTime(btn1, txtcommon, , txtcommon.Text)
        End If
    End Sub

    Protected Sub Answer_Value_TextChanged(obj As Object, arg As EventArgs)
        Try
            Dim gRow As GridViewRow = GVAnswers.Rows(GVAnswers.EditIndex)
            Dim lblcode As Label = DirectCast(gRow.Cells(GRID_COL_SUPPORTS_CLAIM).FindControl(GRID_CONTROL_ANSWER_CODE), Label)
            If lblcode IsNot Nothing Then
                Dim str As String = Answer.GetAnswerCodebyValue(CType(obj, TextBox).Text)
                If str IsNot Nothing Then
                    lblcode.Text = str
                Else
                    lblcode.Text = ""
                End If
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

    Private Sub Grid_PageSizeChanged(source As Object, e As System.EventArgs) Handles ddlPageSize.SelectedIndexChanged
        Try

            PopulateAnswerDetailGrid()
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

#End Region

#End Region

#Region "Detail Clicks"

#Region "Answers Grid  buttons"
    Private Sub btnAnswerCancelChild_Write_Click(sender As Object, e As System.EventArgs) Handles btnCancelAnswer.Click
        Try
            ElitaPlusSearchPage.SetGridControls(GVAnswers, True)
            EndAnswerChildEdit(ElitaPlusPage.DetailPageCommand.Cancel)
            EnableDisableParentControls(True)
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

    Private Sub btnAnswerOkChild_Write_Click(sender As Object, e As System.EventArgs) Handles btnSaveAnswer.Click
        Try
            ''DEF-2285
            ValidateAnswer()
            EndAnswerChildEdit(ElitaPlusPage.DetailPageCommand.OK)
            EnableDisableParentControls(True)
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

    ''DEF-2285
    Private Sub ValidateAnswer()
        Dim ds As DataSet = New DataSet()
        Dim newScore As Decimal
        Dim oldScore As Decimal

        Try
            '#1 get the database values to compare with new changed values
            State.MyAnswerChildBO.Load(ds, State.MyAnswerChildBO.Id)
            Dim gRow As GridViewRow = GVAnswers.Rows(GVAnswers.EditIndex)

            '#2 If changed then expire previous Answer
            If ds.Tables(ELP_ANSWER).Rows.Count > 0 Then
                If Not CType(gRow.Cells(GRID_COL_ANSWER_VALUE).FindControl(GRID_CONTROL_ANSWER_VALUE), TextBox).Text = Nothing AndAlso Not CType(gRow.Cells(GRID_COL_ANSWER_VALUE).FindControl(GRID_CONTROL_ANSWER_VALUE), TextBox).Text.Equals(ds.Tables(ELP_ANSWER).Rows(0)(ANSWER_VALUE)) Then
                    ExpireOldCreateNewAnswer()
                ElseIf Not New Guid(DirectCast(gRow.Cells(GRID_COL_SUPPORTS_CLAIM).FindControl(GRID_CONTROL_SUPPORTS_CLAIM), DropDownList).Text) = New Guid(CType(ds.Tables(ELP_ANSWER).Rows(0)(SUPPORTS_CLAIM_ID), Byte())) Then
                    ExpireOldCreateNewAnswer()
                Else
                    Decimal.TryParse(CType(gRow.Cells(GRID_COL_SCORE).FindControl(GRID_CONTROL_ANSWER_SCORE), TextBox).Text, newScore)
                    Decimal.TryParse(ds.Tables(ELP_ANSWER).Rows(0)(SCORE).ToString, oldScore)
                    If Not newScore = oldScore Then
                        ExpireOldCreateNewAnswer()
                    End If
                End If
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

    ''DEF-2285
    Private Sub ExpireOldCreateNewAnswer()
        Try
            Dim gRow As GridViewRow = GVAnswers.Rows(GVAnswers.EditIndex)
            '#3 Create new answer and copy old answer to new answer
            State.MyAnswerChildBOOld = State.MyBO.GetNewAnswerChild
            State.MyAnswerChildBOOld.BeginEdit()
            State.MyAnswerChildBOOld.Clone(State.MyAnswerChildBO)
            State.MyAnswerChildBOOld.Expiration = DateTime.Now
            State.MyAnswerChildBOOld.EndEdit()
            CType(gRow.Cells(GRID_COL_EFFECTIVE).FindControl(GRID_CONTROL_EFFECTIVE), TextBox).Text = DateTime.Now.ToString
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub


    Private Sub btnSaveAnswerChild_Click(sender As Object, e As System.EventArgs) Handles btnNewAnswer.Click
        Try
            State.AnswerSelectedChildId = Guid.Empty
            BeginAnswerChildEdit()
            State.AnswerSelectedChildId = State.MyAnswerChildBO.Id
            PopulateAnswerDetailGrid()
            FillDropdownList()
            PopulateDetailFromAnswerChildBO()
            EnableDisableParentControls(False)
            ElitaPlusSearchPage.SetGridControls(GVAnswers, False)
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

    Protected Sub btnClearCode_Click(sender As Object, e As System.EventArgs)
        Try
            If State.IsAnswerEditing Then
                State.MyAnswerChildBO.Code = ""
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

#End Region

#End Region

#Region "Handle-Drop downs"
    Private Sub ddlQuestionType_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlQuestionType.SelectedIndexChanged
        Try
            Dim languageId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
            PopulateBOProperty(State.MyBO, "QuestionTypeId", ddlQuestionType)
            With State.MyBO
                If LookupListNew.GetCodeFromId(LookupListNew.GetQuestionTypeLookupList(languageId), .QuestionTypeId) = QTYP_ISSUE Then
                    ControlMgr.SetEnableControl(Me, ddlAttribute, True)
                    ControlMgr.SetEnableControl(Me, ddlImpactsClaim, True)

                Else
                    ControlMgr.SetEnableControl(Me, ddlAttribute, False)
                    ControlMgr.SetEnableControl(Me, ddlImpactsClaim, False)
                    State.MyBO.ImpactsClaimId = Nothing
                    PopulateControlFromBOProperty(ddlImpactsClaim, State.MyBO.ImpactsClaimId)
                End If
            End With
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub


#End Region

End Class
