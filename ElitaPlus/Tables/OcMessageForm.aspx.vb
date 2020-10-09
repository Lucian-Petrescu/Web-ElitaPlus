Imports System.Collections.Generic
Imports Assurant.ElitaPlus.DALObjects
Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Imports System.Text.RegularExpressions
Imports Microsoft.VisualBasic

Namespace Tables

    Public Class OcMessageForm
        Inherits ElitaPlusSearchPage

#Region "Page State"

#Region "MyState"
        Class MyState
            Public OcMessageId As Guid = Guid.Empty

            Public ActionInProgress As DetailPageCommand = DetailPageCommand.Nothing_
            Public LastOperation As DetailPageCommand = DetailPageCommand.Nothing_
            Public boChanged As Boolean = False
            Public LastErrMsg As String
            Public HasDataChanged As Boolean
            Public Message As OcMessage
            Public ScreenSnapShotBO As OcMessage
            Public SelectedRecipientAddress As String
            Public SelectedRecipientDescription As String

            'Message Parameters Grid
            Public MessageParametersGrid_PageIndex As Integer
            Public MessageParametersGrid_DV As OcMessageParams.MessageParamsDV = Nothing
            Public MessageParametersGrid_SortExpression As String = OcMessageParams.MessageParamsDV.COL_PARAM_NAME
            Public MessageParametersGrid_IsInEditMode As Boolean
            Public MessageParametersGrid_MessageParamsId As Guid = Guid.Empty
            Public MessageParametersGrid_MessageParamsBO As OcMessageParams
            Public MessageParametersGrid_IsAfterSave As Boolean

            'Message Attemps Grid
            Public MessageAttemptsGrid_PageIndex As Integer
            Public MessageAttemptsGrid_DV As OcMessageAttempts.MessageAttemptsDV = Nothing
            Public MessageAttemptsGrid_SortExpression As String = OcMessageAttempts.MessageAttemptsDV.COL_MESSAGE_ATTEMPTED_ON
            Public MessageAttemptsGrid_IsInEditMode As Boolean
            Public MessageAttemptsGrid_OcMessageAttemptId As Guid = Guid.Empty
            Public MessageAttemptsGrid_OcMessageAttemptBO As OcMessageAttempts
            Public MessageAttemptsGrid_RecordEdit As Boolean
            Public MessageAttemptsGrid_RecordDelete As Boolean
            Public MessageAttemptsGrid_RecordNew As Boolean
            Public MessageAttemptsGrid_IsAfterSave As Boolean

        End Class
#End Region

        Public Sub New()
            MyBase.New(New MyState)
        End Sub

        Protected Shadows ReadOnly Property State() As MyState
            Get
                Return CType(MyBase.State, MyState)
            End Get
        End Property

        Private Sub SetStateProperties()
            BindBoPropertiesToLabels()
            AddLabelDecorations(TheMessage)
            If State.OcMessageId.Equals(Guid.Empty) Then
                ClearAll()
            End If
            PopulateAll()
        End Sub
#End Region

#Region "Constants"

        Public Const URL As String = "~/tables/OcMessageForm.aspx"

        Private Const NO_ROW_SELECTED_INDEX As Integer = -1

#End Region

#Region "Page Call Type"
        Public Class CallType
            Public MessageId As Guid
            Public Sub New(messageId As Guid)
                Me.MessageId = messageId
            End Sub
        End Class
#End Region

#Region "Page Return Type"
        Public Class ReturnType
            Public LastOperation As DetailPageCommand
            Public MessageId As Guid
            Public HasDataChanged As Boolean
            Public Sub New(LastOp As DetailPageCommand, messageId As Guid, hasDataChanged As Boolean)
                LastOperation = LastOp
                Me.MessageId = messageId
                Me.HasDataChanged = hasDataChanged
            End Sub
        End Class
#End Region

#Region "Properties"
        Private ReadOnly Property TheMessage As OcMessage
            Get
                If State.Message Is Nothing Then
                    State.Message = New OcMessage(State.OcMessageId)
                End If

                Return State.Message
            End Get
        End Property
#End Region

#Region "Handlers"

#Region "Handlers-Init"

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

        Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            Try
                MasterPage.MessageController.Clear_Hide()

                If Not Page.IsPostBack Then
                    MasterPage.MessageController.Clear()
                    MasterPage.UsePageTabTitleInBreadCrum = False
                    MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("Tables")

                    TranslateGridHeader(MessageParametersGrid)
                    TranslateGridHeader(MessageAttemptsGrid)
                    UpdateBreadCrum()

                    SetStateProperties()
                End If

                BindBoPropertiesToLabels()
                MessageParametersGrid_BindBoPropertiesToHeaders()
                MessageAttemptsGrid_BindBoPropertiesToHeaders()
                CheckIfComingFromConfirm()

                If Not IsPostBack Then
                    AddLabelDecorations(TheMessage)
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

            If Me.State.LastOperation = DetailPageCommand.Redirect_ Then
                MasterPage.MessageController.Clear_Hide()
                ClearLabelsErrSign()
                State.LastOperation = DetailPageCommand.Nothing_
            Else
                ShowMissingTranslations(MasterPage.MessageController)
            End If
        End Sub

        Private Sub Page_PageCall(CallFromUrl As String, CallingPar As Object) Handles MyBase.PageCall
            Try
                Dim callObj As OcMessageForm.CallType = CType(CallingPar, OcMessageForm.CallType)

                If callObj Is Nothing Then
                    Throw New ArgumentNullException()
                End If

                If callObj.MessageId <> Guid.Empty Then
                    'Get the id from the parent
                    State.OcMessageId = callObj.MessageId
                Else
                    Throw New ArgumentException()
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub Page_PreRender(sender As Object, e As EventArgs) Handles Me.PreRender
            hdnDisabledTab.Value = String.Join(",", DisabledTabsList)
        End Sub
#End Region

#Region "Handlers-Buttons"

        Private Sub btnBack_Click(sender As System.Object, e As System.EventArgs) Handles btnBack.Click
            Try
                If State.MessageAttemptsGrid_IsInEditMode Then
                    DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                    State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
                Else
                    GoBack()
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub btnResend_WRITE_Click(sender As Object, e As EventArgs) Handles btnResend_WRITE.Click
            Try
                If MessageAttemptsGrid.SelectedIndex = -1 Then
                    moMessageController.Clear()
                    moMessageController.AddError(Assurant.ElitaPlus.Common.ErrorCodes.INVALID_NO_RECORD_SELECTED)
                    Exit Sub
                End If

                If SendMessage() Then
                    ' Refresh Screen
                    MessageAttemptsGrid.SelectedIndex = -1
                    State.MessageAttemptsGrid_DV = Nothing
                    State.MessageParametersGrid_DV = Nothing
                    MessageParametersGrid_Populate()
                    MessageAttemptsGrid_Populate()
                End If

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub CheckIfComingFromConfirm()
            Try
                Select Case State.ActionInProgress
                    Case ElitaPlusPage.DetailPageCommand.Back
                        ComingFromBack()
                End Select

                'Clean after consuming the action
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
                HiddenSaveChangesPromptResponse.Value = String.Empty
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub ComingFromBack()
            Dim confResponse As String = HiddenSaveChangesPromptResponse.Value

            If Not confResponse = String.Empty Then
                ' Return from the Back Button
                Select Case confResponse
                    Case MSG_VALUE_YES
                        ' Save and go back to Search Page
                        If SendMessage() = True Then
                            State.boChanged = True
                            GoBack()
                        End If
                    Case MSG_VALUE_NO
                        GoBack()
                End Select
            End If
        End Sub

        Private Sub GoBack()
            Dim retType As New OcMessageForm.ReturnType(ElitaPlusPage.DetailPageCommand.Back, State.OcMessageId, True)
            ReturnToCallingPage(retType)
        End Sub

        Private Function SendMessage() As Boolean
            Try
                Dim msgAttempt As OcMessageAttempts = New OcMessageAttempts()
                Dim rtnCode As Integer = 0
                Dim rtnMessage As String = String.Empty

                Dim emailTextBox As TextBox
                Dim emailLabel As Label
                Dim descTextBox As TextBox
                Dim descLabel As Label

                emailTextBox = CType(MessageAttemptsGrid.SelectedRow.Cells(2).FindControl("txtRecipientAddress"), TextBox)
                descTextBox = CType(MessageAttemptsGrid.SelectedRow.Cells(3).FindControl("txtRecipientDescription"), TextBox)

                emailLabel = CType(MessageAttemptsGrid.SelectedRow.Cells(2).FindControl("lblRecipientAddress"), Label)
                descLabel = CType(MessageAttemptsGrid.SelectedRow.Cells(3).FindControl("lblRecipientDescription"), Label)

                If emailTextBox IsNot Nothing AndAlso descTextBox IsNot Nothing Then
                    State.SelectedRecipientAddress = emailTextBox.Text
                    State.SelectedRecipientDescription = descTextBox.Text

                    ' Required Email Address
                    If String.IsNullOrEmpty(State.SelectedRecipientAddress) Then
                        moMessageController.Clear()
                        moMessageController.AddError(Assurant.ElitaPlus.Common.ErrorCodes.GUI_EMAIL_IS_REQUIRED_ERR)
                        emailTextBox.Focus()
                        Return False
                    End If

                    ' Validate Email Address
                    'Dim emailExpression As New Regex("^[_a-z0-9-]+(.[a-z0-9-]+)@[a-z0-9-]+(.[a-z0-9-]+)*(.[a-z]{2,4})$")
                    Dim emailExpression As New Regex("^[_a-z0-9-]+(.[a-z0-9-]+)+(.[a-z0-9-]+)@[a-z0-9-]+(.[a-z0-9-]+)*(.[a-z]{2,4})$")

                    If Not emailExpression.IsMatch(State.SelectedRecipientAddress) Then
                        moMessageController.Clear()
                        moMessageController.AddError(Assurant.ElitaPlus.Common.ErrorCodes.GUI_EMAIL_IS_INVALID_ERR)
                        emailTextBox.Focus()
                        Return False
                    End If

                    ' Required Recipient Description
                    If State.MessageAttemptsGrid_IsInEditMode AndAlso String.IsNullOrEmpty(State.SelectedRecipientDescription) Then
                        moMessageController.Clear()
                        moMessageController.AddError(Assurant.ElitaPlus.Common.ErrorCodes.GUI_DESCRIPTION_IS_REQUIED_ERR)
                        descTextBox.Focus()
                        Return False
                    End If
                ElseIf emailLabel IsNot Nothing AndAlso descLabel IsNot Nothing Then
                    State.SelectedRecipientAddress = emailLabel.Text
                    State.SelectedRecipientDescription = descLabel.Text
                End If

                msgAttempt.SaveNewMsgAttempt(State.OcMessageId, State.SelectedRecipientAddress, State.SelectedRecipientDescription, ElitaPlusIdentity.Current.ActiveUser.NetworkId, rtnCode, rtnMessage)

                If rtnCode <> 0 Then
                    moMessageController.Clear()
                    moMessageController.AddError(rtnMessage)
                    Return False
                Else
                    moMessageController.Clear()
                    moMessageController.AddSuccess("MESSAGE_RESEND_SUCCESS", True)
                    Return True
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
                Return False
            End Try

        End Function

        Protected Sub btnResendDiffEmail_WRITE_Click(sender As Object, e As EventArgs) Handles btnResendDiffEmail_WRITE.Click
            Try
                If Not TheMessage.Id.Equals(Guid.Empty) Then
                    If Not State.MessageAttemptsGrid_IsInEditMode Then
                        State.MessageAttemptsGrid_IsInEditMode = True
                        State.MessageAttemptsGrid_DV = Nothing
                        MessageAttemptsGrid_AddNew()
                        'Disable Resend button on the Form
                        btnResend_WRITE.Enabled = False
                        'Change btnResendDiffEmail button's Text
                        btnResendDiffEmail_WRITE.Enabled = True
                        btnResendDiffEmail_WRITE.Text = TranslationBase.TranslateLabelOrMessage("SEND", ElitaPlusIdentity.Current.ActiveUser.LanguageId)
                    Else
                        If SendMessage() Then
                            ' Refresh Screen
                            'Enable Resend button on the Form
                            btnResend_WRITE.Enabled = True
                            'Change btnResendDiffEmail button's Text
                            btnResendDiffEmail_WRITE.Enabled = True
                            btnResendDiffEmail_WRITE.Text = TranslationBase.TranslateLabelOrMessage("MESSAGE_RESEND_DIFF_EMAIL", ElitaPlusIdentity.Current.ActiveUser.LanguageId)
                            State.MessageAttemptsGrid_IsInEditMode = False
                            MessageAttemptsGrid.SelectedIndex = -1
                            State.MessageAttemptsGrid_DV = Nothing
                            State.MessageParametersGrid_DV = Nothing
                            MessageParametersGrid_Populate()
                            MessageAttemptsGrid_Populate(True)
                        End If
                    End If
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub MessageAttemptsGrid_AddNew()
            State.MessageAttemptsGrid_OcMessageAttemptBO = TheMessage.GetNewMessageAttemptChild()
            State.MessageAttemptsGrid_DV = MessageAttemptsGrid_GetDV()
            State.MessageAttemptsGrid_OcMessageAttemptId = State.MessageAttemptsGrid_OcMessageAttemptBO.Id
            MessageAttemptsGrid.DataSource = State.MessageAttemptsGrid_DV
            SetPageAndSelectedIndexFromGuid(State.MessageAttemptsGrid_DV, State.MessageAttemptsGrid_OcMessageAttemptId, MessageAttemptsGrid, State.MessageAttemptsGrid_PageIndex, State.MessageAttemptsGrid_IsInEditMode)
            MessageAttemptsGrid.AutoGenerateColumns = False
            MessageAttemptsGrid_SortAndBind(State.MessageAttemptsGrid_DV)
            SetGridControls(MessageAttemptsGrid, False)
            State.MessageAttemptsGrid_RecordNew = True
            MessageAttemptsGrid_PopulateFormFromBO()
        End Sub

        Private Sub MessageAttemptsGrid_PopulateFormFromBO(Optional ByVal gridRowIdx As Integer? = Nothing)
            If IsNothing(gridRowIdx) Then gridRowIdx = MessageAttemptsGrid.EditIndex

            Try
                With State.MessageAttemptsGrid_OcMessageAttemptBO
                    Dim txtRecipientAddress As TextBox = CType(MessageAttemptsGrid.Rows(gridRowIdx).Cells(2).FindControl("txtRecipientAddress"), TextBox)
                    PopulateControlFromBOProperty(txtRecipientAddress, .RecipientAddress)

                    Dim txtRecipientDescription As TextBox = CType(MessageAttemptsGrid.Rows(gridRowIdx).Cells(3).FindControl("txtRecipientDescription"), TextBox)
                    PopulateControlFromBOProperty(txtRecipientDescription, .RecipientDescription)

                    CType(MessageAttemptsGrid.Rows(gridRowIdx).Cells(0).FindControl("IdLabel"), Label).Text = .Id.ToString
                End With
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

#End Region

#Region "Clear"
        Private Sub ClearAll()
            txtTemplateCode.Text = Nothing
            txtTemplateDescription.Text = Nothing
            txtSenderReason.Text = Nothing
        End Sub
#End Region

#Region "Populate"
        Private Sub UpdateBreadCrum()
            If (State IsNot Nothing) Then
                If (State IsNot Nothing) Then
                    MasterPage.BreadCrum = MasterPage.PageTab & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage("MESSAGE_DETAIL")
                    MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("MESSAGE_DETAIL")
                End If
            End If
        End Sub

        Private Sub PopulateTexts()
            Try
                With TheMessage
                    PopulateControlFromBOProperty(txtTemplateCode, .TemplateCode)
                    PopulateControlFromBOProperty(txtTemplateDescription, .TemplateDescription)
                    PopulateControlFromBOProperty(txtSenderReason, .SenderReason)
                End With
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub PopulateAll()
            ClearAll()
            PopulateTexts()
            MessageParametersGrid_Populate()
            MessageAttemptsGrid_Populate()
        End Sub

#End Region

#Region "Tabs"
        Public Const Tab_MessageParameters As String = "0"
        Public Const Tab_MessageAttempts As String = "1"

        Dim _DisabledTabsList As List(Of String)

        Private ReadOnly Property DisabledTabsList As List(Of String)
            Get
                If _DisabledTabsList Is Nothing Then
                    _DisabledTabsList = New List(Of String)
                    Dim DisabledTabs As Array = hdnDisabledTab.Value.Split(",")

                    If DisabledTabs.Length > 0 AndAlso DisabledTabs(0) IsNot String.Empty Then
                        _DisabledTabsList.AddRange(DisabledTabs)
                    End If
                End If

                Return _DisabledTabsList
            End Get
        End Property
#End Region

#Region "Handlers-Labels"
        Private Sub BindBoPropertiesToLabels()
            BindBOPropertyToLabel(TheMessage, "TemplateCode", lblTemplateCode)
            BindBOPropertyToLabel(TheMessage, "TemplateDescription", lblTemplateDescription)
            BindBOPropertyToLabel(TheMessage, "SenderReason", lblSenderReason)
        End Sub

        Private Sub ClearLabelsErrSign()
            ClearLabelErrSign(lblTemplateCode)
            ClearLabelErrSign(lblTemplateDescription)
            ClearLabelErrSign(lblSenderReason)
        End Sub

        Public Shared Sub SetLabelColor(lbl As Label)
            lbl.ForeColor = Color.Black
        End Sub
#End Region

#Region "Datagrid Related"
#Region "Parameters Grid"
        Public Sub ParametersGrid_RowCreated(sender As System.Object, e As System.Web.UI.WebControls.GridViewRowEventArgs)
            BaseItemCreated(sender, e)
        End Sub

        Private Sub ParametersGrid__PageIndexChanging(source As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles MessageParametersGrid.PageIndexChanging
            Try
                If (Not (State.MessageParametersGrid_IsInEditMode)) Then
                    State.MessageParametersGrid_PageIndex = e.NewPageIndex
                    MessageParametersGrid.PageIndex = State.MessageParametersGrid_PageIndex
                    MessageParametersGrid_Populate()
                    MessageParametersGrid.SelectedIndex = NO_ITEM_SELECTED_INDEX
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub MessageParametersGrid_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles MessageParametersGrid.RowDataBound
            Try
                Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
                Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)

                If dvRow IsNot Nothing AndAlso State.MessageParametersGrid_DV.Count > 0 Then
                    If itemType = ListItemType.Item OrElse itemType = ListItemType.AlternatingItem OrElse itemType = ListItemType.SelectedItem Then
                        CType(e.Row.Cells(0).FindControl("lblParamName"), Label).Text = dvRow(OcMessageParams.MessageParamsDV.COL_PARAM_NAME).ToString
                        CType(e.Row.Cells(1).FindControl("lblParamValue"), Label).Text = dvRow(OcMessageParams.MessageParamsDV.COL_PARAM_VALUE).ToString
                    End If
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub MessageParametersGrid_Populate()
            Try
                With TheMessage
                    If Not .Id.Equals(Guid.Empty) Then
                        If State.MessageParametersGrid_DV Is Nothing Then
                            State.MessageParametersGrid_DV = MessageParametersGrid_GetDV()
                        End If
                    End If
                End With

                If State.MessageParametersGrid_DV IsNot Nothing Then
                    Dim dv As OcMessageParams.MessageParamsDV

                    If State.MessageParametersGrid_DV.Count = 0 Then
                        dv = State.MessageParametersGrid_DV.AddNewRowToEmptyDV
                        SetPageAndSelectedIndexFromGuid(dv, State.MessageParametersGrid_MessageParamsId, MessageParametersGrid, State.MessageParametersGrid_PageIndex)
                        MessageParametersGrid.DataSource = dv
                    Else
                        SetPageAndSelectedIndexFromGuid(State.MessageParametersGrid_DV, State.MessageParametersGrid_MessageParamsId, MessageParametersGrid, State.MessageParametersGrid_PageIndex)
                        MessageParametersGrid.DataSource = State.MessageParametersGrid_DV
                    End If

                    State.MessageParametersGrid_DV.Sort = State.MessageParametersGrid_SortExpression
                    MessageParametersGrid.AutoGenerateColumns = False

                    If State.MessageParametersGrid_DV.Count = 0 Then
                        MessageParametersGrid_SortAndBind(dv)
                    Else
                        MessageParametersGrid_SortAndBind(State.MessageParametersGrid_DV)
                    End If

                    If State.MessageParametersGrid_DV.Count = 0 Then
                        For Each gvRow As GridViewRow In MessageParametersGrid.Rows
                            gvRow.Visible = False
                            gvRow.Controls.Clear()
                        Next
                    End If
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub MessageParametersGrid_BindBoPropertiesToHeaders()
            If State.MessageParametersGrid_MessageParamsBO IsNot Nothing Then
                BindBOPropertyToGridHeader(State.MessageParametersGrid_MessageParamsBO, "ParamName", MessageParametersGrid.Columns(0))
                BindBOPropertyToGridHeader(State.MessageParametersGrid_MessageParamsBO, "ParamValue", MessageParametersGrid.Columns(1))
            End If
            ClearGridViewHeadersAndLabelsErrSign()
        End Sub

        Protected Sub MessageAttemptsGrid_BindBoPropertiesToHeaders()
            If State.MessageAttemptsGrid_OcMessageAttemptBO IsNot Nothing Then
                BindBOPropertyToGridHeader(State.MessageAttemptsGrid_OcMessageAttemptBO, "RecipientAddress", MessageAttemptsGrid.Columns(0))
                BindBOPropertyToGridHeader(State.MessageAttemptsGrid_OcMessageAttemptBO, "RecipientDescription", MessageAttemptsGrid.Columns(1))
                BindBOPropertyToGridHeader(State.MessageAttemptsGrid_OcMessageAttemptBO, "MessageAttemptedOn", MessageAttemptsGrid.Columns(2))
                BindBOPropertyToGridHeader(State.MessageAttemptsGrid_OcMessageAttemptBO, "MessageAttemptedBy", MessageAttemptsGrid.Columns(3))
                BindBOPropertyToGridHeader(State.MessageAttemptsGrid_OcMessageAttemptBO, "ProcessStatusDescription", MessageAttemptsGrid.Columns(4))
                BindBOPropertyToGridHeader(State.MessageAttemptsGrid_OcMessageAttemptBO, "MessageError", MessageAttemptsGrid.Columns(5))
            End If
            ClearGridViewHeadersAndLabelsErrSign()
        End Sub

        Private Function MessageParametersGrid_GetDV() As OcMessageParams.MessageParamsDV
            Dim dv As OcMessageParams.MessageParamsDV
            dv = MessageParametersGrid_GetDataView()
            dv.Sort = MessageParametersGrid.DataMember()
            MessageParametersGrid.DataSource = dv
            Return (dv)
        End Function

        Private Function MessageParametersGrid_GetDataView() As OcMessageParams.MessageParamsDV
            Dim dt As DataTable = TheMessage.MessageParametersList.Table
            Return New OcMessageParams.MessageParamsDV(dt)
        End Function

        Private Sub MessageParametersGrid_SortAndBind(dvBinding As DataView, Optional ByVal blnEmptyList As Boolean = False)
            MessageParametersGrid.DataSource = dvBinding
            HighLightSortColumn(MessageParametersGrid, State.MessageParametersGrid_SortExpression)
            MessageParametersGrid.DataBind()

            If blnEmptyList Then
                For Each gvRow As GridViewRow In MessageParametersGrid.Rows
                    gvRow.Controls.Clear()
                Next
            End If

            Session("recCount") = State.MessageParametersGrid_DV.Count

            ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me, MessageParametersGrid)
        End Sub

#End Region

#Region "MessageAttempts Grid"

        Public Sub MessageAttemptsGrid_RowCreated(sender As System.Object, e As System.Web.UI.WebControls.GridViewRowEventArgs)
            BaseItemCreated(sender, e)
        End Sub

        Private Sub MessageAttemptsGrid__PageIndexChanging(source As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles MessageAttemptsGrid.PageIndexChanging
            Try
                If (Not (State.MessageAttemptsGrid_IsInEditMode)) Then
                    State.MessageAttemptsGrid_PageIndex = e.NewPageIndex
                    MessageAttemptsGrid.PageIndex = State.MessageAttemptsGrid_PageIndex
                    MessageAttemptsGrid_Populate()
                    MessageAttemptsGrid.SelectedIndex = NO_ITEM_SELECTED_INDEX
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub MessageAttemptsGrid_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles MessageAttemptsGrid.RowCommand
            Dim index As Integer = 0

            If e.CommandName = "SelectRecord" Then
                If State.MessageAttemptsGrid_IsInEditMode Then
                    Exit Sub
                End If
                index = CInt(e.CommandArgument)
                If index <> -1 Then
                    If index <> MessageAttemptsGrid.SelectedIndex Then
                        MessageAttemptsGrid.SelectedIndex = index
                        State.SelectedRecipientAddress = CType(MessageAttemptsGrid.Rows(index).Cells(0).FindControl("lblRecipientAddress"), Label).Text
                        State.SelectedRecipientDescription = CType(MessageAttemptsGrid.Rows(index).Cells(0).FindControl("lblRecipientDescription"), Label).Text
                        btnResend_WRITE.Enabled = True
                        btnResendDiffEmail_WRITE.Enabled = False
                    Else
                        MessageAttemptsGrid.SelectedIndex = -1
                        State.SelectedRecipientAddress = String.Empty
                        State.SelectedRecipientDescription = String.Empty
                        btnResend_WRITE.Enabled = False
                        btnResendDiffEmail_WRITE.Enabled = True
                    End If
                Else
                    MessageAttemptsGrid.SelectedIndex = -1
                    State.SelectedRecipientAddress = String.Empty
                    State.SelectedRecipientDescription = String.Empty
                    btnResend_WRITE.Enabled = False
                    btnResendDiffEmail_WRITE.Enabled = True
                End If
            ElseIf e.CommandName = "EditRecord" Then
                index = CInt(e.CommandArgument)
                MessageAttemptsGrid.EditIndex = index
                MessageAttemptsGrid.SelectedIndex = index

                State.MessageAttemptsGrid_IsInEditMode = True
                State.MessageAttemptsGrid_OcMessageAttemptId = New Guid(CType(MessageAttemptsGrid.Rows(index).Cells(0).FindControl("IdLabel"), Label).Text)
                State.MessageAttemptsGrid_OcMessageAttemptBO = TheMessage.GetMessageAttemptChild(State.MessageAttemptsGrid_OcMessageAttemptId)

                MessageAttemptsGrid_Populate()
                State.MessageAttemptsGrid_PageIndex = MessageAttemptsGrid.PageIndex
                State.MessageAttemptsGrid_RecordEdit = True
                MessageAttemptsGrid_PopulateFormFromBO(index)
                MessageAttemptsGrid_SetFocusOnEditableField(MessageAttemptsGrid, 2, "txtRecipientAddress", index)
            End If
        End Sub

        Private Sub MessageAttemptsGrid_SetFocusOnEditableField(grid As GridView, cellPosition As Integer, controlName As String, itemIndex As Integer)
            Dim control As TextBox = CType(grid.Rows(itemIndex).Cells(cellPosition).FindControl(controlName), TextBox)
            SetFocus(control)
        End Sub

        Private Sub MessageAttemptsGrid_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles MessageAttemptsGrid.RowDataBound
            Try
                Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
                Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)

                If dvRow IsNot Nothing AndAlso State.MessageAttemptsGrid_DV.Count > 0 Then

                    If itemType = ListItemType.Item OrElse itemType = ListItemType.AlternatingItem OrElse itemType = ListItemType.SelectedItem OrElse itemType = ListItemType.EditItem Then
                        If (State.MessageAttemptsGrid_IsInEditMode = True AndAlso State.MessageAttemptsGrid_OcMessageAttemptId.ToString.Equals(GetGuidStringFromByteArray(CType(dvRow(OcMessageAttempts.MessageAttemptsDV.COL_OC_MESSAGE_ATTEMPS_ID), Byte())))) Then
                            CType(e.Row.Cells(1).FindControl("btnSelect"), ImageButton).Visible = False
                            CType(e.Row.Cells(2).FindControl("txtRecipientAddress"), TextBox).Text = dvRow(OcMessageAttempts.MessageAttemptsDV.COL_RECIPIENT_ADDRESS).ToString
                            CType(e.Row.Cells(3).FindControl("txtRecipientDescription"), TextBox).Text = dvRow(OcMessageAttempts.MessageAttemptsDV.COL_RECIPIENT_DESCRIPTION).ToString
                            CType(e.Row.Cells(6).FindControl("lblStatus"), Label).Text = "In-Progress"
                        Else
                            If Not String.IsNullOrEmpty(dvRow(OcMessageAttempts.MessageAttemptsDV.COL_RECIPIENT_ADDRESS).ToString) Then
                                If dvRow(OcMessageAttempts.MessageAttemptsDV.COL_PROCESS_STATUS_XCD).ToString.ToUpper() = "TASK_STATUS-C" OrElse dvRow(OcMessageAttempts.MessageAttemptsDV.COL_PROCESS_STATUS_XCD).ToString.ToUpper() = "TASK_STATUS-F" Then
                                    CType(e.Row.Cells(1).FindControl("btnSelect"), ImageButton).Visible = True
                                Else
                                    CType(e.Row.Cells(1).FindControl("btnSelect"), ImageButton).Visible = False
                                End If
                                CType(e.Row.Cells(2).FindControl("lblRecipientAddress"), Label).Text = dvRow(OcMessageAttempts.MessageAttemptsDV.COL_RECIPIENT_ADDRESS).ToString
                                CType(e.Row.Cells(3).FindControl("lblRecipientDescription"), Label).Text = dvRow(OcMessageAttempts.MessageAttemptsDV.COL_RECIPIENT_DESCRIPTION).ToString
                                If Not String.IsNullOrEmpty(dvRow(OcMessageAttempts.MessageAttemptsDV.COL_MESSAGE_ATTEMPTED_ON).ToString) Then
                                    CType(e.Row.Cells(4).FindControl("lblAttemptedOn"), Label).Text = GetLongDate12FormattedStringNullable(CType(dvRow(OcMessageAttempts.MessageAttemptsDV.COL_MESSAGE_ATTEMPTED_ON), Date))
                                End If
                                CType(e.Row.Cells(5).FindControl("lblAttemptedBy"), Label).Text = dvRow(OcMessageAttempts.MessageAttemptsDV.COL_MESSAGE_ATTEMPTED_BY).ToString
                                CType(e.Row.Cells(6).FindControl("lblStatus"), Label).Text = dvRow(OcMessageAttempts.MessageAttemptsDV.COL_PROCESS_STATUS_DESCRIPTION).ToString
                                CType(e.Row.Cells(7).FindControl("lblErrorMessage"), Label).Text = dvRow(OcMessageAttempts.MessageAttemptsDV.COL_MESSAGE_ERROR).ToString
                            End If
                        End If
                    End If
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub MessageAttemptsGrid_Populate(Optional ByVal clean As Boolean = False)
            Try
                With TheMessage
                    If Not .Id.Equals(Guid.Empty) Then
                        If State.MessageAttemptsGrid_DV Is Nothing Then
                            State.MessageAttemptsGrid_DV = MessageAttemptsGrid_GetDV(clean)
                        End If
                    End If
                End With

                If State.MessageAttemptsGrid_DV IsNot Nothing Then
                    Dim dv As OcMessageAttempts.MessageAttemptsDV

                    If State.MessageAttemptsGrid_DV.Count = 0 Then
                        dv = State.MessageAttemptsGrid_DV.AddNewRowToEmptyDV
                        SetPageAndSelectedIndexFromGuid(dv, State.MessageAttemptsGrid_OcMessageAttemptId, MessageAttemptsGrid, State.MessageAttemptsGrid_PageIndex)
                        MessageAttemptsGrid.DataSource = dv
                    Else
                        SetPageAndSelectedIndexFromGuid(State.MessageAttemptsGrid_DV, State.MessageAttemptsGrid_OcMessageAttemptId, MessageAttemptsGrid, State.MessageAttemptsGrid_PageIndex)
                        MessageAttemptsGrid.DataSource = State.MessageAttemptsGrid_DV
                    End If

                    State.MessageAttemptsGrid_DV.Sort = State.MessageAttemptsGrid_SortExpression
                    MessageAttemptsGrid.AutoGenerateColumns = False

                    If State.MessageAttemptsGrid_DV.Count = 0 Then
                        MessageAttemptsGrid_SortAndBind(dv)
                    Else
                        MessageAttemptsGrid_SortAndBind(State.MessageAttemptsGrid_DV)
                    End If

                    If State.MessageAttemptsGrid_DV.Count = 0 Then
                        For Each gvRow As GridViewRow In MessageAttemptsGrid.Rows
                            gvRow.Visible = False
                            gvRow.Controls.Clear()
                        Next
                    End If
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Function MessageAttemptsGrid_GetDV(Optional ByVal clean As Boolean = False) As OcMessageAttempts.MessageAttemptsDV
            Dim dv As OcMessageAttempts.MessageAttemptsDV
            dv = MessageAttemptsGrid_GetDataView(clean)
            dv.Sort = MessageAttemptsGrid.DataMember()
            MessageAttemptsGrid.DataSource = dv
            Return (dv)
        End Function

        Private Function MessageAttemptsGrid_GetDataView(Optional ByVal clean As Boolean = False) As OcMessageAttempts.MessageAttemptsDV
            Dim dt As DataTable = TheMessage.MessageAttemptsList(clean).Table
            Return New OcMessageAttempts.MessageAttemptsDV(dt)
        End Function

        Private Sub MessageAttemptsGrid_SortAndBind(dvBinding As DataView, Optional ByVal blnEmptyList As Boolean = False)
            MessageAttemptsGrid.DataSource = dvBinding
            HighLightSortColumn(MessageAttemptsGrid, State.MessageAttemptsGrid_SortExpression)
            MessageAttemptsGrid.DataBind()

            If blnEmptyList Then
                For Each gvRow As GridViewRow In MessageAttemptsGrid.Rows
                    gvRow.Controls.Clear()
                Next
            End If

            Session("recCount") = State.MessageAttemptsGrid_DV.Count

            ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me, MessageAttemptsGrid)
        End Sub

#End Region
#End Region
#End Region

    End Class

End Namespace