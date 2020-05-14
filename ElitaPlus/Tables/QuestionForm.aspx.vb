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

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
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
        'Public LastOperation As DetailPageCommand
        Public ReadOnly Property LastOperation As DetailPageCommand
        Public ReadOnly Property EditingBo As Question 
        Public ReadOnly Property HasDataChanged As Boolean
        Public Sub New(ByVal lastOp As DetailPageCommand, ByVal curEditingBo As Question, ByVal hasDataChanged As Boolean)
            Me.LastOperation = lastOp
            Me.EditingBo = curEditingBo
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

    Private Sub Page_PageCall(ByVal CallFromUrl As String, ByVal CallingPar As Object) Handles MyBase.PageCall
        Try
            If Not Me.CallingParameters Is Nothing Then
                'Get the id from the parent
                Me.State.MyBO = New Question(CType(Me.CallingParameters, Guid))
                Me.State.SoftQuestionGrp = Me.State.MyBO.GetSoftQuestionGroup()
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

#End Region

#Region "Page Events"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Try
            Me.ErrorCtrl.Clear_Hide()
            If Not Me.IsPostBack Then
                'Date Calendars
                '  Me.MenuEnabled = False
                Me.AddControlMsg(Me.btnDelete_WRITE, Message.DELETE_RECORD_PROMPT, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, True)
                If Me.State.MyBO Is Nothing Then
                    Me.State.MyBO = New Question
                    Me.State.SoftQuestionGrp = Me.State.MyBO.GetSoftQuestionGroup()
                    Me.State.MyBO.SoftQuestionGroupId = Me.State.SoftQuestionGrp.Id
                End If
                PopulateDropdowns()
                Me.TranslateGridHeader(Me.GVAnswers)
                Me.PopulateFormFromBOs()
                Me.EnableDisableFields()
            Else
                SelectedTabIndex = hdnSelectedTab.Value
                DisableTabIndex = hdnDisabledTab.Value
            End If
            BindBoPropertiesToLabels()
            BindBoPropertiesToGridHeader()
            CheckIfComingFromSaveConfirm()
            If Not Me.IsPostBack Then
                Me.AddLabelDecorations(Me.State.MyBO)
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
        Me.ShowMissingTranslations(Me.ErrorCtrl)
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
        If Me.State.MyBO.IsNew Then
            ControlMgr.SetEnableControl(Me, btnDelete_WRITE, False)
            ControlMgr.SetEnableControl(Me, btnNew_WRITE, False)
            ControlMgr.SetEnableControl(Me, btnCopy_WRITE, False)
        End If
        EnableDisableaNSWERControl(False)
        'WRITE YOU OWN CODE HERE
    End Sub

    Sub EnableDisableParentControls(ByVal enableToggle As Boolean)
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
            With Me.State.MyBO
                If Not .IsNew And Not .QuestionTypeId.Equals(Guid.Empty) Then
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

    Sub EnableDisableaNSWERControl(ByVal enableToggle As Boolean)
        ControlMgr.SetEnableControl(Me, btnCancelAnswer, enableToggle)
        ControlMgr.SetEnableControl(Me, btnSaveAnswer, enableToggle)
        ControlMgr.SetEnableControl(Me, btnNewAnswer, Not enableToggle)
    End Sub


    Protected Sub BindBoPropertiesToLabels()
        Me.BindBOPropertyToLabel(Me.State.MyBO, "Code", Me.lblCode)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "Description", Me.lblDescription)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "Effective", Me.lbleffectiveDate)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "Expiration", Me.lblExpirationDate)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "QuestionType", Me.lblQuestionType)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "AnswerType", Me.lblAnswerType)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "Attribute", Me.lblAttribute)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "CustomerMessage", Me.lblCustomerMessage)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "SearchTags", Me.lblSearchTags)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "ImpactsClaim", Me.lblImpactsClaim)
        Me.ClearGridViewHeadersAndLabelsErrSign()
    End Sub

    Private Sub BindBoPropertiesToGridHeader()
        Me.BindBOPropertyToGridHeader(Me.State.MyAnswerChildBO, "AnswerOrder", GVAnswers.Columns(GRID_COL_ANSWER_ORDER))
        Me.BindBOPropertyToGridHeader(Me.State.MyAnswerChildBO, "Code", GVAnswers.Columns(GRID_COL_ANSWER_CODE))
        Me.BindBOPropertyToGridHeader(Me.State.MyAnswerChildBO, "AnswerValue", GVAnswers.Columns(GRID_COL_ANSWER_VALUE)) 'DEF-2846
        Me.BindBOPropertyToGridHeader(Me.State.MyAnswerChildBO, "Description", GVAnswers.Columns(GRID_COL_DESCRIPTION))
        Me.BindBOPropertyToGridHeader(Me.State.MyAnswerChildBO, "SupportsClaimId", GVAnswers.Columns(GRID_COL_SUPPORTS_CLAIM))
        Me.BindBOPropertyToGridHeader(Me.State.MyAnswerChildBO, "Score", GVAnswers.Columns(GRID_COL_SCORE))
        Me.BindBOPropertyToGridHeader(Me.State.MyAnswerChildBO, "Effective", GVAnswers.Columns(GRID_COL_EFFECTIVE))
        Me.BindBOPropertyToGridHeader(Me.State.MyAnswerChildBO, "Expiration", GVAnswers.Columns(GRID_COL_EXPIRATION))
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
        Me.AddCalendarwithTime(btneffective, txtEffective, , txtEffective.Text)
        Me.AddCalendarwithTime(btnExpiration, txtExpirationDate, , txtExpirationDate.Text)
    End Sub

    Protected Sub PopulateFormFromBOs()
        With Me.State.MyBO
            Dim languageId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
            Me.SetGridItemStyleColor(GVAnswers)
            PopulateAnswerDetailGrid()
            Me.PopulateControlFromBOProperty(ddlanswerType, .AnswerTypeId)
            Me.PopulateControlFromBOProperty(ddlQuestionType, .QuestionTypeId)

            'Question type id -> don't allow to change for a saved/old record
            If Not .IsNew And Not .QuestionTypeId.Equals(Guid.Empty) Then
                ControlMgr.SetEnableControl(Me, ddlQuestionType, False)
            Else
                ControlMgr.SetEnableControl(Me, ddlQuestionType, True)
            End If

            If LookupListNew.GetCodeFromId(LookupListNew.GetQuestionTypeLookupList(languageId), .QuestionTypeId) = QTYP_ISSUE Then
                Me.PopulateControlFromBOProperty(ddlAttribute, .EntityAttributeId)
                Me.PopulateControlFromBOProperty(ddlImpactsClaim, .ImpactsClaimId)
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

            Me.PopulateControlFromBOProperty(txtDescription, .Description)
            Me.PopulateControlFromBOProperty(txtCode, .Code)
            Me.PopulateControlFromBOProperty(txtEffective, .Effective, "dd-MMM-yyyy HH:mm:ss tt")
            Me.PopulateControlFromBOProperty(txtExpirationDate, .Expiration, "dd-MMM-yyyy HH:mm:ss tt")
            Me.PopulateControlFromBOProperty(txtCustomerMessage, .CustomerMessage)
            Me.PopulateControlFromBOProperty(txtSearchTags, .SearchTags)

            'IF question is expired then don't allow the user to make any change to it
            If Me.State.MyBO.Expiration.Value < DateTime.Now Then
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
            Dim dv As Question.AnswerSelectionView = Me.State.MyBO.GetAnswerSelectionView()
            dv.Sort = Me.State.SortExpressionAnswerDetailGrid
            Session("recCount") = dv.Count

            Me.GVAnswers.Columns(Me.GRID_COL_ANSWER_CODE).SortExpression = Question.AnswerSelectionView.COL_NAME_CODE
            Me.GVAnswers.Columns(Me.GRID_COL_ANSWER_ORDER).SortExpression = Question.AnswerSelectionView.COL_NAME_ORDER
            Me.GVAnswers.Columns(Me.GRID_COL_DESCRIPTION).SortExpression = Question.AnswerSelectionView.COL_NAME_DESCRIPTION
            Me.GVAnswers.Columns(Me.GRID_COL_SUPPORTS_CLAIM).SortExpression = Question.AnswerSelectionView.COL_NAME_SUPPORTS_CLAIM
            Me.GVAnswers.Columns(Me.GRID_COL_ANSWER_VALUE).SortExpression = Question.AnswerSelectionView.COL_NAME_VALUE
            Me.GVAnswers.Columns(Me.GRID_COL_EFFECTIVE).SortExpression = Question.AnswerSelectionView.COL_NAME_EFFECTIVE
            Me.GVAnswers.Columns(Me.GRID_COL_EXPIRATION).SortExpression = Question.AnswerSelectionView.COL_NAME_EXPIRATION
            Me.SetGridItemStyleColor(Me.GVAnswers)

            If Me.State.IsAnswerEditing Then
                Me.SetPageAndSelectedIndexFromGuid(dv, Me.State.AnswerSelectedChildId, GVAnswers,
                                        GVAnswers.PageIndex, True)
            Else
                SetPageAndSelectedIndexFromGuid(dv, Me.State.AnswerSelectedChildId, Me.GVAnswers, Me.State.AnswerDetailPageIndex)
                Me.State.AnswerDetailPageIndex = Me.GVAnswers.PageIndex
            End If


            If dv.Count > 0 Then
                Me.GVAnswers.DataSource = dv
                Me.GVAnswers.AutoGenerateColumns = False
                Me.GVAnswers.DataBind()
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
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try





    End Sub

    Protected Sub PopulateBOsFormFrom()
        With Me.State.MyBO
            Me.PopulateBOProperty(Me.State.MyBO, "Code", Me.txtCode)
            Me.PopulateBOProperty(Me.State.MyBO, "Description", Me.txtDescription)
            Me.PopulateBOProperty(Me.State.MyBO, "Effective", Me.txtEffective)
            Me.PopulateBOProperty(Me.State.MyBO, "Expiration", Me.txtExpirationDate)
            Me.PopulateBOProperty(Me.State.MyBO, "QuestionTypeId", Me.ddlQuestionType)
            Me.PopulateBOProperty(Me.State.MyBO, "AnswerTypeId", Me.ddlanswerType)
            Me.PopulateBOProperty(Me.State.MyBO, "EntityAttributeId", Me.ddlAttribute)
            Me.PopulateBOProperty(Me.State.MyBO, "ImpactsClaimId", Me.ddlImpactsClaim)
            Me.PopulateBOProperty(Me.State.MyBO, "CustomerMessage", Me.txtCustomerMessage)
            Me.PopulateBOProperty(Me.State.MyBO, "SearchTags", Me.txtSearchTags)

            'set the description of soft question group same as question 
            Me.PopulateBOProperty(Me.State.SoftQuestionGrp, "Description", Me.txtDescription)

        End With
        If Me.ErrCollection.Count > 0 Then
            Throw New PopulateBOErrorException
        End If
    End Sub

    Protected Sub CreateNew()
        Me.State.ScreenSnapShotBO = Nothing 'Reset the backup copy
        Me.State.MyBO = New Question
        Me.State.SoftQuestionGrp = Me.State.MyBO.GetSoftQuestionGroup()
        Me.State.MyBO.SoftQuestionGroupId = Me.State.SoftQuestionGrp.Id
        Me.PopulateFormFromBOs()
        Me.EnableDisableFields()
    End Sub

    Protected Sub CreateNewWithCopy()
        Dim newObj As New Question
        newObj.Copy(Me.State.MyBO)
        Me.State.MyBO = newObj
        Me.State.SoftQuestionGrp = Me.State.MyBO.GetSoftQuestionGroup()
        Me.State.MyBO.SoftQuestionGroupId = Me.State.SoftQuestionGrp.Id
        EnableDisableParentControls(True)
        Me.PopulateFormFromBOs()
        Me.EnableDisableFields()
        'create the backup copy
        Me.State.ScreenSnapShotBO = New Question
        Me.State.ScreenSnapShotBO.Copy(Me.State.MyBO)
    End Sub

    Protected Sub CheckIfComingFromSaveConfirm()
        Dim confResponse As String = Me.HiddenSaveChangesPromptResponse.Value
        If Not confResponse Is Nothing AndAlso confResponse = Me.MSG_VALUE_YES Then
            If Me.State.ActionInProgress <> ElitaPlusPage.DetailPageCommand.BackOnErr AndAlso Me.State.ActionInProgress <> ElitaPlusPage.DetailPageCommand.Accept Then
                Me.State.MyBO.Save()
            End If
            Select Case Me.State.ActionInProgress
                Case ElitaPlusPage.DetailPageCommand.Back
                    Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.MyBO, Me.State.HasDataChanged))
                Case ElitaPlusPage.DetailPageCommand.Delete

                Case ElitaPlusPage.DetailPageCommand.New_
                    Me.DisplayMessage(Message.SAVE_RECORD_CONFIRMATION, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
                    Me.CreateNew()
                Case ElitaPlusPage.DetailPageCommand.NewAndCopy
                    Me.DisplayMessage(Message.SAVE_RECORD_CONFIRMATION, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
                    Me.CreateNewWithCopy()
                Case ElitaPlusPage.DetailPageCommand.BackOnErr
                    Me.ReturnToCallingPage(New ReturnType(Me.State.ActionInProgress, Me.State.MyBO, Me.State.HasDataChanged))
                Case ElitaPlusPage.DetailPageCommand.Accept
                    If Me.State.IsAnswerEditing Then
                        Me.PopulateAnswerChildBOFromDetail()
                        Me.State.MyAnswerChildBO.Save()
                        Me.State.MyAnswerChildBO.EndEdit()
                        Me.State.IsAnswerEditing = False
                    End If
                    If Me.State.OverlapExists Then
                        If Me.State.MyBO.IsDirty Then
                            If Me.State.MyBO.ExpireOverLappingQuestions() Then
                                Me.State.SoftQuestionGrp.Save()
                                Me.State.MyBO.Save()
                                Me.State.HasDataChanged = False
                                Me.PopulateFormFromBOs()
                                Me.EnableDisableFields()
                                Me.DisplayMessage(Message.SAVE_RECORD_CONFIRMATION, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
                            End If
                        Else
                            Me.DisplayMessage(Message.MSG_RECORD_NOT_SAVED, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
                        End If
                        Me.State.OverlapExists = False
                    Else
                        Me.DisplayMessage(Message.MSG_RECORD_NOT_SAVED, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
                    End If
                    Me.EnableDisableFields()
                    Me.PopulateAnswerDetailGrid()
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
                    If Me.State.IsAnswerEditing Then
                        Me.State.MyAnswerChildBO.cancelEdit()
                        If Me.State.MyAnswerChildBO.IsSaveNew Then
                            Me.State.MyAnswerChildBO.Delete()
                            Me.State.MyAnswerChildBO.Save()
                        End If
                        Me.State.IsAnswerEditing = False
                    End If
                    Me.EnableDisableFields()
                    Me.PopulateAnswerDetailGrid()
            End Select
        End If
        'Clean after consuming the action
        Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
        Me.HiddenSaveChangesPromptResponse.Value = ""
    End Sub

    Sub EndAnswerChildEdit(ByVal lastop As ElitaPlusPage.DetailPageCommand)
        Try
            With Me.State
                Select Case lastop
                    Case ElitaPlusPage.DetailPageCommand.OK
                        Me.PopulateAnswerChildBOFromDetail()
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
            Me.State.IsAnswerEditing = False
            Me.EnableDisableFields()
            Me.PopulateAnswerDetailGrid()
            EnableDisableaNSWERControl(False)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

    Sub BeginAnswerChildEdit()
        Me.State.IsAnswerEditing = True
        With Me.State
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
        Dim ddlSupportsClaim As DropDownList = CType(gRow.Cells(Me.GRID_COL_SUPPORTS_CLAIM).FindControl(Me.GRID_CONTROL_SUPPORTS_CLAIM), DropDownList)
        Dim txtCommon As TextBox

        With Me.State.MyAnswerChildBO
            If Not ddlSupportsClaim Is Nothing Then
                If Not .SupportsClaimId = Guid.Empty Then
                    Me.PopulateControlFromBOProperty(ddlSupportsClaim, .SupportsClaimId)
                End If
            End If

            Dim lblCommon As Label = CType(gRow.Cells(Me.GRID_COL_ANSWER_CODE).FindControl(Me.GRID_CONTROL_ANSWER_CODE), Label)
            If Not lblCommon Is Nothing Then
                Me.PopulateControlFromBOProperty(lblCommon, .Code)
            End If

            Dim imgbtn As ImageButton = CType(gRow.Cells(Me.GRID_COL_ANSWER_CODE).FindControl(Me.GRID_CONTROL_CLEAR_CODE), ImageButton)
            imgbtn.Attributes.Add("onclick", "javascript:return ClearCodeValue('" & GVAnswers.ClientID & "','" & GVAnswers.EditIndex & "')")


            txtCommon = CType(gRow.Cells(Me.GRID_COL_DESCRIPTION).FindControl(Me.GRID_CONTROL_ANSWER_DESCRIPTION), TextBox)
            If Not txtCommon Is Nothing Then
                Me.PopulateControlFromBOProperty(txtCommon, .Description)
            End If

            'user should allowed to edit effective date of future questions 
            ''any list having effective Date (not time) less than today will not be able to change effective date
            txtCommon = CType(gRow.Cells(Me.GRID_COL_EFFECTIVE).FindControl(Me.GRID_CONTROL_EFFECTIVE), TextBox)
            If Not txtCommon Is Nothing Then
                Me.PopulateControlFromBOProperty(txtCommon, .Effective)
            End If

            imgbtn = CType(gRow.Cells(Me.GRID_COL_ANSWER_CODE).FindControl(Me.GRID_CONTROL_BTN_EFFECTIVE), ImageButton)
            If Not .Effective.Value.Date < DateTime.Now.Date Then
                ControlMgr.SetEnableControl(Me, txtCommon, True)
                If Not imgbtn Is Nothing Then
                    ControlMgr.SetEnableControl(Me, imgbtn, True)
                End If
            Else
                ControlMgr.SetEnableControl(Me, txtCommon, False)
                If Not imgbtn Is Nothing Then
                    ControlMgr.SetEnableControl(Me, imgbtn, False)
                End If
            End If

            txtCommon = CType(gRow.Cells(Me.GRID_COL_EXPIRATION).FindControl(Me.GRID_CONTROL_EXPIRATION), TextBox)
            If Not txtCommon Is Nothing Then
                Me.PopulateControlFromBOProperty(txtCommon, .Expiration)
            End If

            txtCommon = CType(gRow.Cells(Me.GRID_COL_ANSWER_VALUE).FindControl(Me.GRID_CONTROL_ANSWER_VALUE), TextBox)
            If Not txtCommon Is Nothing Then
                Me.PopulateControlFromBOProperty(txtCommon, .AnswerValue)
            End If

            txtCommon = CType(gRow.Cells(Me.GRID_COL_ANSWER_ORDER).FindControl(Me.GRID_CONTROL_ANSWER_ORDER), TextBox)
            If Not txtCommon Is Nothing Then
                Me.PopulateControlFromBOProperty(txtCommon, .AnswerOrder)
            End If

            txtCommon = CType(gRow.Cells(Me.GRID_COL_SCORE).FindControl(Me.GRID_CONTROL_ANSWER_SCORE), TextBox)
            If Not txtCommon Is Nothing Then
                Me.PopulateControlFromBOProperty(txtCommon, .Score)
            End If

        End With
    End Sub


    Public Sub SetGridRowControls(ByVal gridrow As GridViewRow, ByVal enable As Boolean)
        Dim i As Integer
        Dim edt As ImageButton
        Dim del As ImageButton

        'grid.AllowSorting = enable  ' Enable/Disable the sorting
        '  Enable or Disable EDIT and DELETE buttons on the GridRow

        edt = CType(gridrow.Cells(EDIT_COL).FindControl(EDIT_CONTROL_NAME), ImageButton)
        If Not edt Is Nothing Then
            edt.Enabled = enable
            edt.Visible = enable
        End If

        del = CType(gridrow.Cells(DELETE_COL).FindControl(DELETE_CONTROL_NAME), ImageButton)
        If Not del Is Nothing Then
            del.Enabled = enable
            del.Visible = enable
        End If


    End Sub


    Sub PopulateAnswerChildBOFromDetail()
        'ensure that grid's edit index is set before this gets a call
        If GVAnswers.EditIndex = -1 Then Exit Sub
        Dim gRow As GridViewRow = GVAnswers.Rows(GVAnswers.EditIndex)

        Dim ddlSupportsClaim As DropDownList = CType(Me.GVAnswers.Rows(Me.GVAnswers.EditIndex).Cells(Me.GRID_COL_SUPPORTS_CLAIM).FindControl(Me.GRID_CONTROL_SUPPORTS_CLAIM), DropDownList)
        If Not ddlSupportsClaim Is Nothing Then
            Me.PopulateBOProperty(Me.State.MyAnswerChildBO, "SupportsClaimId", New Guid(ddlSupportsClaim.SelectedValue))
        End If

        With Me.State.MyAnswerChildBO
            Dim txtCommon As TextBox
            With Me.State.MyAnswerChildBO
                Dim lblCommon As Label = CType(gRow.Cells(Me.GRID_COL_ANSWER_CODE).FindControl(Me.GRID_CONTROL_ANSWER_CODE), Label)
                If Not lblCommon Is Nothing Then
                    Me.PopulateBOProperty(Me.State.MyAnswerChildBO, "Code", lblCommon.Text)
                End If

                txtCommon = CType(gRow.Cells(Me.GRID_COL_DESCRIPTION).FindControl(Me.GRID_CONTROL_ANSWER_DESCRIPTION), TextBox)
                If Not txtCommon Is Nothing Then
                    Me.PopulateBOProperty(Me.State.MyAnswerChildBO, "Description", txtCommon)
                End If

                txtCommon = CType(gRow.Cells(Me.GRID_COL_EFFECTIVE).FindControl(Me.GRID_CONTROL_EFFECTIVE), TextBox)
                If Not txtCommon Is Nothing Then
                    Me.PopulateBOProperty(Me.State.MyAnswerChildBO, "Effective", txtCommon)
                End If

                txtCommon = CType(gRow.Cells(Me.GRID_COL_EXPIRATION).FindControl(Me.GRID_CONTROL_EXPIRATION), TextBox)
                If Not txtCommon Is Nothing Then
                    Me.PopulateBOProperty(Me.State.MyAnswerChildBO, "Expiration", txtCommon)
                End If

                txtCommon = CType(gRow.Cells(Me.GRID_COL_ANSWER_VALUE).FindControl(Me.GRID_CONTROL_ANSWER_VALUE), TextBox)
                If Not txtCommon Is Nothing Then
                    Me.PopulateBOProperty(Me.State.MyAnswerChildBO, "AnswerValue", txtCommon)
                End If

                txtCommon = CType(gRow.Cells(Me.GRID_COL_ANSWER_ORDER).FindControl(Me.GRID_CONTROL_ANSWER_ORDER), TextBox)
                If Not txtCommon Is Nothing Then
                    Me.PopulateBOProperty(Me.State.MyAnswerChildBO, "AnswerOrder", txtCommon)
                End If

                txtCommon = CType(gRow.Cells(Me.GRID_COL_SCORE).FindControl(Me.GRID_CONTROL_ANSWER_SCORE), TextBox)
                If Not txtCommon Is Nothing Then
                    Me.PopulateBOProperty(Me.State.MyAnswerChildBO, "Score", txtCommon)
                End If

                'Hardcoding for now to remove dependency of running scripts everytime an answer is added.
                'Needs to be fixed by creating a UI for it..
                If (.AnswerValue.ToUpperInvariant.Contains("YES") Or .AnswerValue.ToUpperInvariant.Contains("SI")) Then
                    Me.State.MyAnswerChildBO.ListItemId = LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, Codes.YESNO_Y)
                End If

                If (.AnswerValue.ToUpperInvariant.Contains("NO")) Then
                    Me.State.MyAnswerChildBO.ListItemId = LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, Codes.YESNO_N)
                End If

            End With
        End With
        If Me.ErrCollection.Count > 0 Then
            Throw New PopulateBOErrorException
        End If
    End Sub
    Private Sub EnableTab(ByVal blnFlag As Boolean)
        If blnFlag = True Then 'enable
            SelectedTabIndex = "0"
            DisableTabIndex = String.Empty
        Else 'disable
            SelectedTabIndex = String.Empty
            DisableTabIndex = "0"
        End If
    End Sub
    Private Function CheckQuestionOverlap() As Boolean
        Return Me.State.MyBO.Accept(New OverlapValidationVisitor)
    End Function

    Private Function CheckExistingFutureRuleOverlap() As Boolean
        Return Me.State.MyBO.Accept(New FutureOverlapValidationVisitor)
    End Function


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
            Me.PopulateBOsFormFrom()
            Me.State.MyBO.Validate()
            If Me.CheckQuestionOverlap() Then
                If Me.CheckExistingFutureRuleOverlap() Then
                    Throw New GUIException(Message.MSG_GUI_OVERLAPPING_RULES, Assurant.ElitaPlus.Common.ErrorCodes.EQUIPMENT_LIST_CODE_OVERLAPPED)
                End If
                Me.DisplayMessage(Message.MSG_GUI_OVERLAPPING_RECORDS, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                Me.State.ActionInProgress = DetailPageCommand.Accept
                Me.State.OverlapExists = True
                Exit Sub
            End If

            If Me.State.MyBO.IsDirty Then
                Me.State.SoftQuestionGrp.Save()
                Me.State.MyBO.UpdateTranslation()
                Me.State.MyBO.Save()
                Me.State.HasDataChanged = False
                Me.PopulateFormFromBOs()
                Me.EnableDisableFields()
                Me.ClearGridViewHeadersAndLabelsErrSign()
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
                Me.State.MyBO = New Question(Me.State.MyBO.Id)
            ElseIf Not Me.State.ScreenSnapShotBO Is Nothing Then
                'It was a new with copy
                Me.State.MyBO.Clone(Me.State.ScreenSnapShotBO)
            Else
                Me.State.MyBO = New Question
            End If
            Me.PopulateFormFromBOs()
            Me.EnableDisableFields()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

    Private Sub btnDelete_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete_WRITE.Click
        Try
            If Me.State.MyBO.IsQuestionAssignedtoIssue Then
                Throw New GUIException(Message.MSG_GUI_QUESTION_ASSIGNED_TO_ISSUE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_LIST_CODE_ERR)
            Else
                If Not Me.State.MyBO.Effective.Value > DateTime.Now Then
                    'for future effective date delete the question which has not been assigned to any question list
                    Me.State.MyBO.Delete()
                    Me.State.MyBO.Save()
                    Me.State.HasDataChanged = True
                    Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Delete, Me.State.MyBO, Me.State.HasDataChanged))
                Else
                    'for current question - expire it by setting the expiry date
                    Me.State.MyBO.Accept(New ExpirationVisitor)
                    Me.State.MyBO.Save()
                    Me.State.HasDataChanged = True
                    Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Expire, Me.State.MyBO, Me.State.HasDataChanged))
                End If
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
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

    Private Sub btnCopy_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCopy_WRITE.Click
        Try
            Me.PopulateBOsFormFrom()
            If Me.State.MyBO.IsDirty Then
                Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", Me.MSG_BTN_YES_NO_CANCEL, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.NewAndCopy
            Else
                Me.CreateNewWithCopy()
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

#End Region

#Region "Detail Grid Events"

    Public Sub ItemCreated(ByVal sender As System.Object, ByVal e As DataGridItemEventArgs)
        Try
            BaseItemCreated(sender, e)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

#Region "Answers Grid"
    Protected Sub GVAnswers_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GVAnswers.PageIndexChanging
        Try
            Me.State.AnswerDetailPageIndex = e.NewPageIndex
            Me.State.AnswerSelectedChildId = Guid.Empty
            Me.PopulateAnswerDetailGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

    Protected Sub GVAnswers_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)
        Try
            Dim nIndex As Integer
            If e.CommandName = ElitaPlusSearchPage.EDIT_COMMAND_NAME Then
                nIndex = CInt(e.CommandArgument)
                Me.State.AnswerSelectedChildId = New Guid(CType(Me.GVAnswers.Rows(nIndex).Cells(Me.GRID_COL_ANSWER_ID).Controls(0), Label).Text)
                Me.State.IsAnswerEditing = True
                Me.BeginAnswerChildEdit()
                PopulateAnswerDetailGrid()
                Me.FillDropdownList()
                Me.PopulateDetailFromAnswerChildBO()
                ElitaPlusSearchPage.SetGridControls(GVAnswers, False)
                Me.EnableDisableFields()
                EnableDisableaNSWERControl(True)
            ElseIf e.CommandName = ElitaPlusSearchPage.DELETE_COMMAND_NAME Then
                nIndex = CInt(e.CommandArgument)
                Me.State.AnswerSelectedChildId = New Guid(CType(Me.GVAnswers.Rows(nIndex).Cells(Me.GRID_COL_ANSWER_ID).Controls(0), Label).Text)
                Me.State.IsAnswerEditing = True
                BeginAnswerChildEdit()
                EndAnswerChildEdit(ElitaPlusPage.DetailPageCommand.Delete)
                PopulateAnswerDetailGrid()
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

    Protected Sub GVAnswers_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
        Try
            MyBase.BaseItemCreated(sender, e)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

    Private Sub GVAnswers_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GVAnswers.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            If Not Me.State.IsAnswerEditing Then
                If CType(e.Row.Cells(Me.GRID_COL_EXPIRATION).Controls(0), Label).Text <> "" Then
                    Dim expDate As DateTime = DateHelper.GetDateValue(CType(e.Row.Cells(Me.GRID_COL_EXPIRATION).Controls(0), Label).Text)
                    If expDate < DateTime.Now Then
                        SetGridRowControls(e.Row, False)
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub GVAnswers_Sorting(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles GVAnswers.Sorting
        Try
            If Me.State.SortExpressionAnswerDetailGrid.StartsWith(e.SortExpression) Then
                If Me.State.SortExpressionAnswerDetailGrid.StartsWith(e.SortExpression & " DESC") Then
                    Me.State.SortExpressionAnswerDetailGrid = e.SortExpression
                Else
                    Me.State.SortExpressionAnswerDetailGrid = e.SortExpression & " DESC"
                End If
            Else
                Me.State.SortExpressionAnswerDetailGrid = e.SortExpression
            End If
            Me.PopulateAnswerDetailGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

    Private Sub FillDropdownList()

        'fill the drop downs
        If GVAnswers.EditIndex = -1 Then Exit Sub
        Dim gRow As GridViewRow = GVAnswers.Rows(GVAnswers.EditIndex)
        Dim moIsCoveredDrop As DropDownList = DirectCast(gRow.Cells(Me.GRID_COL_SUPPORTS_CLAIM).FindControl(Me.GRID_CONTROL_SUPPORTS_CLAIM), DropDownList)
        Dim languageId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId

        With Me.State.MyAnswerChildBO
            If Not moIsCoveredDrop Is Nothing Then
                ' Me.BindListControlToDataView(moIsCoveredDrop, LookupListNew.GetYesNoLookupList(languageId), , , False)
                moIsCoveredDrop.Populate(CommonConfigManager.Current.ListManager.GetList("YESNO", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions())
            End If
        End With

        Dim btn As ImageButton = DirectCast(gRow.Cells(Me.GRID_COL_EFFECTIVE).FindControl(Me.GRID_CONTROL_BTN_EFFECTIVE), ImageButton)
        Dim btn1 As ImageButton = DirectCast(gRow.Cells(Me.GRID_COL_EXPIRATION).FindControl(Me.GRID_CONTROL_BTN_EXPIRATION), ImageButton)

        Dim txtcommon As TextBox
        txtcommon = CType(gRow.Cells(Me.GRID_COL_EFFECTIVE).FindControl(Me.GRID_CONTROL_EFFECTIVE), TextBox)
        If Not txtcommon Is Nothing Then
            Me.AddCalendarwithTime(btn, txtcommon, , txtcommon.Text)
        End If

        txtcommon = CType(gRow.Cells(Me.GRID_COL_EXPIRATION).FindControl(Me.GRID_CONTROL_EXPIRATION), TextBox)
        If Not txtcommon Is Nothing Then
            Me.AddCalendarwithTime(btn1, txtcommon, , txtcommon.Text)
        End If
    End Sub

    Protected Sub Answer_Value_TextChanged(ByVal obj As Object, ByVal arg As EventArgs)
        Try
            Dim gRow As GridViewRow = GVAnswers.Rows(GVAnswers.EditIndex)
            Dim lblcode As Label = DirectCast(gRow.Cells(Me.GRID_COL_SUPPORTS_CLAIM).FindControl(Me.GRID_CONTROL_ANSWER_CODE), Label)
            If Not lblcode Is Nothing Then
                Dim str As String = Answer.GetAnswerCodebyValue(CType(obj, TextBox).Text)
                If Not str Is Nothing Then
                    lblcode.Text = str
                Else
                    lblcode.Text = ""
                End If
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

    Private Sub Grid_PageSizeChanged(ByVal source As Object, ByVal e As System.EventArgs) Handles ddlPageSize.SelectedIndexChanged
        Try

            Me.PopulateAnswerDetailGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

#End Region

#End Region

#Region "Detail Clicks"

#Region "Answers Grid  buttons"
    Private Sub btnAnswerCancelChild_Write_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelAnswer.Click
        Try
            ElitaPlusSearchPage.SetGridControls(GVAnswers, True)
            Me.EndAnswerChildEdit(ElitaPlusPage.DetailPageCommand.Cancel)
            EnableDisableParentControls(True)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

    Private Sub btnAnswerOkChild_Write_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSaveAnswer.Click
        Try
            ''DEF-2285
            Me.ValidateAnswer()
            Me.EndAnswerChildEdit(ElitaPlusPage.DetailPageCommand.OK)
            EnableDisableParentControls(True)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

    ''DEF-2285
    Private Sub ValidateAnswer()
        Dim ds As DataSet = New DataSet()
        Dim newScore As Decimal
        Dim oldScore As Decimal

        Try
            '#1 get the database values to compare with new changed values
            Me.State.MyAnswerChildBO.Load(ds, Me.State.MyAnswerChildBO.Id)
            Dim gRow As GridViewRow = GVAnswers.Rows(GVAnswers.EditIndex)

            '#2 If changed then expire previous Answer
            If ds.Tables(ELP_ANSWER).Rows.Count > 0 Then
                If Not CType(gRow.Cells(Me.GRID_COL_ANSWER_VALUE).FindControl(Me.GRID_CONTROL_ANSWER_VALUE), TextBox).Text = Nothing And
                     Not CType(gRow.Cells(Me.GRID_COL_ANSWER_VALUE).FindControl(Me.GRID_CONTROL_ANSWER_VALUE), TextBox).Text.Equals(ds.Tables(ELP_ANSWER).Rows(0)(ANSWER_VALUE)) Then
                    ExpireOldCreateNewAnswer()
                ElseIf Not New Guid(DirectCast(gRow.Cells(Me.GRID_COL_SUPPORTS_CLAIM).FindControl(Me.GRID_CONTROL_SUPPORTS_CLAIM), DropDownList).Text) = New Guid(CType(ds.Tables(ELP_ANSWER).Rows(0)(SUPPORTS_CLAIM_ID), Byte())) Then
                    ExpireOldCreateNewAnswer()
                Else
                    Decimal.TryParse(CType(gRow.Cells(Me.GRID_COL_SCORE).FindControl(Me.GRID_CONTROL_ANSWER_SCORE), TextBox).Text, newScore)
                    Decimal.TryParse(ds.Tables(ELP_ANSWER).Rows(0)(SCORE).ToString, oldScore)
                    If Not newScore = oldScore Then
                        ExpireOldCreateNewAnswer()
                    End If
                End If
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

    ''DEF-2285
    Private Sub ExpireOldCreateNewAnswer()
        Try
            Dim gRow As GridViewRow = GVAnswers.Rows(GVAnswers.EditIndex)
            '#3 Create new answer and copy old answer to new answer
            Me.State.MyAnswerChildBOOld = Me.State.MyBO.GetNewAnswerChild
            Me.State.MyAnswerChildBOOld.BeginEdit()
            Me.State.MyAnswerChildBOOld.Clone(Me.State.MyAnswerChildBO)
            Me.State.MyAnswerChildBOOld.Expiration = DateTime.Now
            Me.State.MyAnswerChildBOOld.EndEdit()
            CType(gRow.Cells(Me.GRID_COL_EFFECTIVE).FindControl(Me.GRID_CONTROL_EFFECTIVE), TextBox).Text = DateTime.Now.ToString
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub


    Private Sub btnSaveAnswerChild_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNewAnswer.Click
        Try
            Me.State.AnswerSelectedChildId = Guid.Empty
            Me.BeginAnswerChildEdit()
            Me.State.AnswerSelectedChildId = Me.State.MyAnswerChildBO.Id
            Me.PopulateAnswerDetailGrid()
            Me.FillDropdownList()
            PopulateDetailFromAnswerChildBO()
            EnableDisableParentControls(False)
            ElitaPlusSearchPage.SetGridControls(GVAnswers, False)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

    Protected Sub btnClearCode_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            If Me.State.IsAnswerEditing Then
                Me.State.MyAnswerChildBO.Code = ""
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

#End Region

#End Region

#Region "Handle-Drop downs"
    Private Sub ddlQuestionType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlQuestionType.SelectedIndexChanged
        Try
            Dim languageId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
            Me.PopulateBOProperty(Me.State.MyBO, "QuestionTypeId", Me.ddlQuestionType)
            With Me.State.MyBO
                If LookupListNew.GetCodeFromId(LookupListNew.GetQuestionTypeLookupList(languageId), .QuestionTypeId) = QTYP_ISSUE Then
                    ControlMgr.SetEnableControl(Me, ddlAttribute, True)
                    ControlMgr.SetEnableControl(Me, ddlImpactsClaim, True)

                Else
                    ControlMgr.SetEnableControl(Me, ddlAttribute, False)
                    ControlMgr.SetEnableControl(Me, ddlImpactsClaim, False)
                    Me.State.MyBO.ImpactsClaimId = Nothing
                    PopulateControlFromBOProperty(ddlImpactsClaim, Me.State.MyBO.ImpactsClaimId)
                End If
            End With
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub


#End Region

End Class
