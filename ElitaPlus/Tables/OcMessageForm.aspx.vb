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
            Me.AddLabelDecorations(TheMessage)
            If Me.State.OcMessageId.Equals(Guid.Empty) Then
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
            Public Sub New(ByVal messageId As Guid)
                Me.MessageId = messageId
            End Sub
        End Class
#End Region

#Region "Page Return Type"
        Public Class ReturnType
            Public LastOperation As DetailPageCommand
            Public MessageId As Guid
            Public HasDataChanged As Boolean
            Public Sub New(ByVal LastOp As DetailPageCommand, ByVal messageId As Guid, ByVal hasDataChanged As Boolean)
                Me.LastOperation = LastOp
                Me.MessageId = messageId
                Me.HasDataChanged = hasDataChanged
            End Sub
        End Class
#End Region

#Region "Properties"
        Private ReadOnly Property TheMessage As OcMessage
            Get
                If Me.State.Message Is Nothing Then
                    Me.State.Message = New OcMessage(Me.State.OcMessageId)
                End If

                Return Me.State.Message
            End Get
        End Property
#End Region

#Region "Handlers"

#Region "Handlers-Init"

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

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            Try
                Me.MasterPage.MessageController.Clear_Hide()

                If Not Page.IsPostBack Then
                    Me.MasterPage.MessageController.Clear()
                    Me.MasterPage.UsePageTabTitleInBreadCrum = False
                    Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("Tables")

                    TranslateGridHeader(MessageParametersGrid)
                    TranslateGridHeader(MessageAttemptsGrid)
                    UpdateBreadCrum()

                    Me.SetStateProperties()
                End If

                BindBoPropertiesToLabels()
                MessageParametersGrid_BindBoPropertiesToHeaders()
                MessageAttemptsGrid_BindBoPropertiesToHeaders()
                CheckIfComingFromConfirm()

                If Not Me.IsPostBack Then
                    Me.AddLabelDecorations(TheMessage)
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try

            If Me.State.LastOperation = DetailPageCommand.Redirect_ Then
                Me.MasterPage.MessageController.Clear_Hide()
                ClearLabelsErrSign()
                Me.State.LastOperation = DetailPageCommand.Nothing_
            Else
                Me.ShowMissingTranslations(Me.MasterPage.MessageController)
            End If
        End Sub

        Private Sub Page_PageCall(ByVal CallFromUrl As String, ByVal CallingPar As Object) Handles MyBase.PageCall
            Try
                Dim callObj As OcMessageForm.CallType = CType(CallingPar, OcMessageForm.CallType)

                If callObj Is Nothing Then
                    Throw New ArgumentNullException()
                End If

                If callObj.MessageId <> Guid.Empty Then
                    'Get the id from the parent
                    Me.State.OcMessageId = callObj.MessageId
                Else
                    Throw New ArgumentException()
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub Page_PreRender(sender As Object, e As EventArgs) Handles Me.PreRender
            hdnDisabledTab.Value = String.Join(",", DisabledTabsList)
        End Sub
#End Region

#Region "Handlers-Buttons"

        Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
            Try
                If Me.State.MessageAttemptsGrid_IsInEditMode Then
                    Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                    Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
                Else
                    GoBack()
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub btnResend_WRITE_Click(sender As Object, e As EventArgs) Handles btnResend_WRITE.Click
            Try
                If MessageAttemptsGrid.SelectedIndex = -1 Then
                    Me.moMessageController.Clear()
                    Me.moMessageController.AddError(Assurant.ElitaPlus.Common.ErrorCodes.INVALID_NO_RECORD_SELECTED)
                    Exit Sub
                End If

                If SendMessage() Then
                    ' Refresh Screen
                    MessageAttemptsGrid.SelectedIndex = -1
                    Me.State.MessageAttemptsGrid_DV = Nothing
                    Me.State.MessageParametersGrid_DV = Nothing
                    MessageParametersGrid_Populate()
                    MessageAttemptsGrid_Populate()
                End If

            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub CheckIfComingFromConfirm()
            Try
                Select Case Me.State.ActionInProgress
                    Case ElitaPlusPage.DetailPageCommand.Back
                        ComingFromBack()
                End Select

                'Clean after consuming the action
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
                Me.HiddenSaveChangesPromptResponse.Value = String.Empty
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub ComingFromBack()
            Dim confResponse As String = Me.HiddenSaveChangesPromptResponse.Value

            If Not confResponse = String.Empty Then
                ' Return from the Back Button
                Select Case confResponse
                    Case MSG_VALUE_YES
                        ' Save and go back to Search Page
                        If SendMessage() = True Then
                            Me.State.boChanged = True
                            GoBack()
                        End If
                    Case MSG_VALUE_NO
                        GoBack()
                End Select
            End If
        End Sub

        Private Sub GoBack()
            Dim retType As New OcMessageForm.ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.OcMessageId, True)
            Me.ReturnToCallingPage(retType)
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

                emailTextBox = CType(Me.MessageAttemptsGrid.SelectedRow.Cells(2).FindControl("txtRecipientAddress"), TextBox)
                descTextBox = CType(Me.MessageAttemptsGrid.SelectedRow.Cells(3).FindControl("txtRecipientDescription"), TextBox)

                emailLabel = CType(Me.MessageAttemptsGrid.SelectedRow.Cells(2).FindControl("lblRecipientAddress"), Label)
                descLabel = CType(Me.MessageAttemptsGrid.SelectedRow.Cells(3).FindControl("lblRecipientDescription"), Label)

                If Not emailTextBox Is Nothing AndAlso Not descTextBox Is Nothing Then
                    Me.State.SelectedRecipientAddress = emailTextBox.Text
                    Me.State.SelectedRecipientDescription = descTextBox.Text

                    ' Required Email Address
                    If String.IsNullOrEmpty(Me.State.SelectedRecipientAddress) Then
                        Me.moMessageController.Clear()
                        Me.moMessageController.AddError(Assurant.ElitaPlus.Common.ErrorCodes.GUI_EMAIL_IS_REQUIRED_ERR)
                        emailTextBox.Focus()
                        Return False
                    End If

                    ' Validate Email Address
                    Dim emailExpression As New Regex("^[_a-z0-9-]+(.[a-z0-9-]+)@[a-z0-9-]+(.[a-z0-9-]+)*(.[a-z]{2,4})$")
                    If Not emailExpression.IsMatch(Me.State.SelectedRecipientAddress) Then
                        Me.moMessageController.Clear()
                        Me.moMessageController.AddError(Assurant.ElitaPlus.Common.ErrorCodes.GUI_EMAIL_IS_INVALID_ERR)
                        emailTextBox.Focus()
                        Return False
                    End If

                    ' Required Recipient Description
                    If Me.State.MessageAttemptsGrid_IsInEditMode AndAlso String.IsNullOrEmpty(Me.State.SelectedRecipientDescription) Then
                        Me.moMessageController.Clear()
                        Me.moMessageController.AddError(Assurant.ElitaPlus.Common.ErrorCodes.GUI_DESCRIPTION_IS_REQUIED_ERR)
                        descTextBox.Focus()
                        Return False
                    End If
                ElseIf Not emailLabel Is Nothing AndAlso Not descLabel Is Nothing Then
                    Me.State.SelectedRecipientAddress = emailLabel.Text
                    Me.State.SelectedRecipientDescription = descLabel.Text
                End If

                msgAttempt.SaveNewMsgAttempt(Me.State.OcMessageId, Me.State.SelectedRecipientAddress, Me.State.SelectedRecipientDescription, ElitaPlusIdentity.Current.ActiveUser.NetworkId, rtnCode, rtnMessage)

                If rtnCode <> 0 Then
                    Me.moMessageController.Clear()
                    Me.moMessageController.AddError(rtnMessage)
                    Return False
                Else
                    Me.moMessageController.Clear()
                    Me.moMessageController.AddSuccess("MESSAGE_RESEND_SUCCESS", True)
                    Return True
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
                Return False
            End Try

        End Function

        Protected Sub btnResendDiffEmail_WRITE_Click(sender As Object, e As EventArgs) Handles btnResendDiffEmail_WRITE.Click
            Try
                If Not TheMessage.Id.Equals(Guid.Empty) Then
                    If Not Me.State.MessageAttemptsGrid_IsInEditMode Then
                        Me.State.MessageAttemptsGrid_IsInEditMode = True
                        Me.State.MessageAttemptsGrid_DV = Nothing
                        MessageAttemptsGrid_AddNew()
                        'Disable Resend button on the Form
                        Me.btnResend_WRITE.Enabled = False
                        'Change btnResendDiffEmail button's Text
                        Me.btnResendDiffEmail_WRITE.Enabled = True
                        Me.btnResendDiffEmail_WRITE.Text = TranslationBase.TranslateLabelOrMessage("SEND", ElitaPlusIdentity.Current.ActiveUser.LanguageId)
                    Else
                        If SendMessage() Then
                            ' Refresh Screen
                            'Enable Resend button on the Form
                            Me.btnResend_WRITE.Enabled = True
                            'Change btnResendDiffEmail button's Text
                            Me.btnResendDiffEmail_WRITE.Enabled = True
                            Me.btnResendDiffEmail_WRITE.Text = TranslationBase.TranslateLabelOrMessage("MESSAGE_RESEND_DIFF_EMAIL", ElitaPlusIdentity.Current.ActiveUser.LanguageId)
                            Me.State.MessageAttemptsGrid_IsInEditMode = False
                            MessageAttemptsGrid.SelectedIndex = -1
                            Me.State.MessageAttemptsGrid_DV = Nothing
                            Me.State.MessageParametersGrid_DV = Nothing
                            MessageParametersGrid_Populate()
                            MessageAttemptsGrid_Populate()
                        End If
                    End If
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub MessageAttemptsGrid_AddNew()
            Me.State.MessageAttemptsGrid_OcMessageAttemptBO = TheMessage.GetNewMessageAttemptChild()
            Me.State.MessageAttemptsGrid_DV = MessageAttemptsGrid_GetDV()
            Me.State.MessageAttemptsGrid_OcMessageAttemptId = Me.State.MessageAttemptsGrid_OcMessageAttemptBO.Id
            Me.MessageAttemptsGrid.DataSource = Me.State.MessageAttemptsGrid_DV
            Me.SetPageAndSelectedIndexFromGuid(Me.State.MessageAttemptsGrid_DV, Me.State.MessageAttemptsGrid_OcMessageAttemptId, Me.MessageAttemptsGrid, Me.State.MessageAttemptsGrid_PageIndex, Me.State.MessageAttemptsGrid_IsInEditMode)
            Me.MessageAttemptsGrid.AutoGenerateColumns = False
            MessageAttemptsGrid_SortAndBind(Me.State.MessageAttemptsGrid_DV)
            SetGridControls(Me.MessageAttemptsGrid, False)
            Me.State.MessageAttemptsGrid_RecordNew = True
            MessageAttemptsGrid_PopulateFormFromBO()
        End Sub

        Private Sub MessageAttemptsGrid_PopulateFormFromBO(Optional ByVal gridRowIdx As Integer? = Nothing)
            If IsNothing(gridRowIdx) Then gridRowIdx = Me.MessageAttemptsGrid.EditIndex

            Try
                With Me.State.MessageAttemptsGrid_OcMessageAttemptBO
                    Dim txtRecipientAddress As TextBox = CType(Me.MessageAttemptsGrid.Rows(gridRowIdx).Cells(2).FindControl("txtRecipientAddress"), TextBox)
                    Me.PopulateControlFromBOProperty(txtRecipientAddress, .RecipientAddress)

                    Dim txtRecipientDescription As TextBox = CType(Me.MessageAttemptsGrid.Rows(gridRowIdx).Cells(3).FindControl("txtRecipientDescription"), TextBox)
                    Me.PopulateControlFromBOProperty(txtRecipientDescription, .RecipientDescription)

                    CType(Me.MessageAttemptsGrid.Rows(gridRowIdx).Cells(0).FindControl("IdLabel"), Label).Text = .Id.ToString
                End With
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
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
            If (Not Me.State Is Nothing) Then
                If (Not Me.State Is Nothing) Then
                    Me.MasterPage.BreadCrum = Me.MasterPage.PageTab & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage("MESSAGE_DETAIL")
                    Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("MESSAGE_DETAIL")
                End If
            End If
        End Sub

        Private Sub PopulateTexts()
            Try
                With TheMessage
                    Me.PopulateControlFromBOProperty(Me.txtTemplateCode, .TemplateCode)
                    Me.PopulateControlFromBOProperty(Me.txtTemplateDescription, .TemplateDescription)
                    Me.PopulateControlFromBOProperty(Me.txtSenderReason, .SenderReason)
                End With
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
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
            Me.BindBOPropertyToLabel(TheMessage, "TemplateCode", lblTemplateCode)
            Me.BindBOPropertyToLabel(TheMessage, "TemplateDescription", lblTemplateDescription)
            Me.BindBOPropertyToLabel(TheMessage, "SenderReason", lblSenderReason)
        End Sub

        Private Sub ClearLabelsErrSign()
            Me.ClearLabelErrSign(lblTemplateCode)
            Me.ClearLabelErrSign(lblTemplateDescription)
            Me.ClearLabelErrSign(lblSenderReason)
        End Sub

        Public Shared Sub SetLabelColor(ByVal lbl As Label)
            lbl.ForeColor = Color.Black
        End Sub
#End Region

#Region "Datagrid Related"
#Region "Parameters Grid"
        Public Sub ParametersGrid_RowCreated(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
            BaseItemCreated(sender, e)
        End Sub

        Private Sub ParametersGrid__PageIndexChanging(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles MessageParametersGrid.PageIndexChanging
            Try
                If (Not (Me.State.MessageParametersGrid_IsInEditMode)) Then
                    Me.State.MessageParametersGrid_PageIndex = e.NewPageIndex
                    Me.MessageParametersGrid.PageIndex = Me.State.MessageParametersGrid_PageIndex
                    Me.MessageParametersGrid_Populate()
                    Me.MessageParametersGrid.SelectedIndex = NO_ITEM_SELECTED_INDEX
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub MessageParametersGrid_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles MessageParametersGrid.RowDataBound
            Try
                Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
                Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)

                If Not dvRow Is Nothing And Me.State.MessageParametersGrid_DV.Count > 0 Then
                    If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then
                        CType(e.Row.Cells(0).FindControl("lblParamName"), Label).Text = dvRow(OcMessageParams.MessageParamsDV.COL_PARAM_NAME).ToString
                        CType(e.Row.Cells(1).FindControl("lblParamValue"), Label).Text = dvRow(OcMessageParams.MessageParamsDV.COL_PARAM_VALUE).ToString
                    End If
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub MessageParametersGrid_Populate()
            Try
                With TheMessage
                    If Not .Id.Equals(Guid.Empty) Then
                        If Me.State.MessageParametersGrid_DV Is Nothing Then
                            Me.State.MessageParametersGrid_DV = MessageParametersGrid_GetDV()
                        End If
                    End If
                End With

                If Not Me.State.MessageParametersGrid_DV Is Nothing Then
                    Dim dv As OcMessageParams.MessageParamsDV

                    If Me.State.MessageParametersGrid_DV.Count = 0 Then
                        dv = Me.State.MessageParametersGrid_DV.AddNewRowToEmptyDV
                        SetPageAndSelectedIndexFromGuid(dv, Me.State.MessageParametersGrid_MessageParamsId, Me.MessageParametersGrid, Me.State.MessageParametersGrid_PageIndex)
                        Me.MessageParametersGrid.DataSource = dv
                    Else
                        SetPageAndSelectedIndexFromGuid(Me.State.MessageParametersGrid_DV, Me.State.MessageParametersGrid_MessageParamsId, Me.MessageParametersGrid, Me.State.MessageParametersGrid_PageIndex)
                        Me.MessageParametersGrid.DataSource = Me.State.MessageParametersGrid_DV
                    End If

                    Me.State.MessageParametersGrid_DV.Sort = Me.State.MessageParametersGrid_SortExpression
                    Me.MessageParametersGrid.AutoGenerateColumns = False

                    If Me.State.MessageParametersGrid_DV.Count = 0 Then
                        MessageParametersGrid_SortAndBind(dv)
                    Else
                        MessageParametersGrid_SortAndBind(Me.State.MessageParametersGrid_DV)
                    End If

                    If Me.State.MessageParametersGrid_DV.Count = 0 Then
                        For Each gvRow As GridViewRow In Me.MessageParametersGrid.Rows
                            gvRow.Visible = False
                            gvRow.Controls.Clear()
                        Next
                    End If
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub MessageParametersGrid_BindBoPropertiesToHeaders()
            If Not Me.State.MessageParametersGrid_MessageParamsBO Is Nothing Then
                Me.BindBOPropertyToGridHeader(Me.State.MessageParametersGrid_MessageParamsBO, "ParamName", Me.MessageParametersGrid.Columns(0))
                Me.BindBOPropertyToGridHeader(Me.State.MessageParametersGrid_MessageParamsBO, "ParamValue", Me.MessageParametersGrid.Columns(1))
            End If
            Me.ClearGridViewHeadersAndLabelsErrSign()
        End Sub

        Protected Sub MessageAttemptsGrid_BindBoPropertiesToHeaders()
            If Not Me.State.MessageAttemptsGrid_OcMessageAttemptBO Is Nothing Then
                Me.BindBOPropertyToGridHeader(Me.State.MessageAttemptsGrid_OcMessageAttemptBO, "RecipientAddress", Me.MessageAttemptsGrid.Columns(0))
                Me.BindBOPropertyToGridHeader(Me.State.MessageAttemptsGrid_OcMessageAttemptBO, "RecipientDescription", Me.MessageAttemptsGrid.Columns(1))
                Me.BindBOPropertyToGridHeader(Me.State.MessageAttemptsGrid_OcMessageAttemptBO, "MessageAttemptedOn", Me.MessageAttemptsGrid.Columns(2))
                Me.BindBOPropertyToGridHeader(Me.State.MessageAttemptsGrid_OcMessageAttemptBO, "MessageAttemptedBy", Me.MessageAttemptsGrid.Columns(3))
                Me.BindBOPropertyToGridHeader(Me.State.MessageAttemptsGrid_OcMessageAttemptBO, "ProcessStatusDescription", Me.MessageAttemptsGrid.Columns(4))
                Me.BindBOPropertyToGridHeader(Me.State.MessageAttemptsGrid_OcMessageAttemptBO, "MessageError", Me.MessageAttemptsGrid.Columns(5))
            End If
            Me.ClearGridViewHeadersAndLabelsErrSign()
        End Sub

        Private Function MessageParametersGrid_GetDV() As OcMessageParams.MessageParamsDV
            Dim dv As OcMessageParams.MessageParamsDV
            dv = MessageParametersGrid_GetDataView()
            dv.Sort = Me.MessageParametersGrid.DataMember()
            Me.MessageParametersGrid.DataSource = dv
            Return (dv)
        End Function

        Private Function MessageParametersGrid_GetDataView() As OcMessageParams.MessageParamsDV
            Dim dt As DataTable = TheMessage.MessageParametersList.Table
            Return New OcMessageParams.MessageParamsDV(dt)
        End Function

        Private Sub MessageParametersGrid_SortAndBind(ByVal dvBinding As DataView, Optional ByVal blnEmptyList As Boolean = False)
            Me.MessageParametersGrid.DataSource = dvBinding
            HighLightSortColumn(Me.MessageParametersGrid, Me.State.MessageParametersGrid_SortExpression)
            Me.MessageParametersGrid.DataBind()

            If blnEmptyList Then
                For Each gvRow As GridViewRow In Me.MessageParametersGrid.Rows
                    gvRow.Controls.Clear()
                Next
            End If

            Session("recCount") = Me.State.MessageParametersGrid_DV.Count

            ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me, Me.MessageParametersGrid)
        End Sub

#End Region

#Region "MessageAttempts Grid"

        Public Sub MessageAttemptsGrid_RowCreated(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
            BaseItemCreated(sender, e)
        End Sub

        Private Sub MessageAttemptsGrid__PageIndexChanging(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles MessageAttemptsGrid.PageIndexChanging
            Try
                If (Not (Me.State.MessageAttemptsGrid_IsInEditMode)) Then
                    Me.State.MessageAttemptsGrid_PageIndex = e.NewPageIndex
                    Me.MessageAttemptsGrid.PageIndex = Me.State.MessageAttemptsGrid_PageIndex
                    Me.MessageAttemptsGrid_Populate()
                    Me.MessageAttemptsGrid.SelectedIndex = NO_ITEM_SELECTED_INDEX
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub MessageAttemptsGrid_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles MessageAttemptsGrid.RowCommand
            Dim index As Integer = 0

            If e.CommandName = "SelectRecord" Then
                If Me.State.MessageAttemptsGrid_IsInEditMode Then
                    Exit Sub
                End If
                index = CInt(e.CommandArgument)
                If index <> -1 Then
                    If index <> MessageAttemptsGrid.SelectedIndex Then
                        MessageAttemptsGrid.SelectedIndex = index
                        Me.State.SelectedRecipientAddress = CType(Me.MessageAttemptsGrid.Rows(index).Cells(0).FindControl("lblRecipientAddress"), Label).Text
                        Me.State.SelectedRecipientDescription = CType(Me.MessageAttemptsGrid.Rows(index).Cells(0).FindControl("lblRecipientDescription"), Label).Text
                        Me.btnResend_WRITE.Enabled = True
                        Me.btnResendDiffEmail_WRITE.Enabled = False
                    Else
                        MessageAttemptsGrid.SelectedIndex = -1
                        Me.State.SelectedRecipientAddress = String.Empty
                        Me.State.SelectedRecipientDescription = String.Empty
                        Me.btnResend_WRITE.Enabled = False
                        Me.btnResendDiffEmail_WRITE.Enabled = True
                    End If
                Else
                    MessageAttemptsGrid.SelectedIndex = -1
                    Me.State.SelectedRecipientAddress = String.Empty
                    Me.State.SelectedRecipientDescription = String.Empty
                    Me.btnResend_WRITE.Enabled = False
                    Me.btnResendDiffEmail_WRITE.Enabled = True
                End If
            ElseIf e.CommandName = "EditRecord" Then
                index = CInt(e.CommandArgument)
                MessageAttemptsGrid.EditIndex = index
                MessageAttemptsGrid.SelectedIndex = index

                Me.State.MessageAttemptsGrid_IsInEditMode = True
                Me.State.MessageAttemptsGrid_OcMessageAttemptId = New Guid(CType(Me.MessageAttemptsGrid.Rows(index).Cells(0).FindControl("IdLabel"), Label).Text)
                Me.State.MessageAttemptsGrid_OcMessageAttemptBO = TheMessage.GetMessageAttemptChild(Me.State.MessageAttemptsGrid_OcMessageAttemptId)

                MessageAttemptsGrid_Populate()
                Me.State.MessageAttemptsGrid_PageIndex = MessageAttemptsGrid.PageIndex
                Me.State.MessageAttemptsGrid_RecordEdit = True
                MessageAttemptsGrid_PopulateFormFromBO(index)
                MessageAttemptsGrid_SetFocusOnEditableField(Me.MessageAttemptsGrid, 2, "txtRecipientAddress", index)
            End If
        End Sub

        Private Sub MessageAttemptsGrid_SetFocusOnEditableField(ByVal grid As GridView, ByVal cellPosition As Integer, ByVal controlName As String, ByVal itemIndex As Integer)
            Dim control As TextBox = CType(grid.Rows(itemIndex).Cells(cellPosition).FindControl(controlName), TextBox)
            SetFocus(control)
        End Sub

        Private Sub MessageAttemptsGrid_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles MessageAttemptsGrid.RowDataBound
            Try
                Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
                Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)

                If Not dvRow Is Nothing And Me.State.MessageAttemptsGrid_DV.Count > 0 Then
                    If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Or itemType = ListItemType.EditItem Then
                        If (Me.State.MessageAttemptsGrid_IsInEditMode = True AndAlso Me.State.MessageAttemptsGrid_OcMessageAttemptId.ToString.Equals(GetGuidStringFromByteArray(CType(dvRow(OcMessageAttempts.MessageAttemptsDV.COL_OC_MESSAGE_ATTEMPS_ID), Byte())))) Then
                            CType(e.Row.Cells(1).FindControl("btnSelect"), ImageButton).Visible = False
                            CType(e.Row.Cells(2).FindControl("txtRecipientAddress"), TextBox).Text = dvRow(OcMessageAttempts.MessageAttemptsDV.COL_RECIPIENT_ADDRESS).ToString
                            CType(e.Row.Cells(3).FindControl("txtRecipientDescription"), TextBox).Text = dvRow(OcMessageAttempts.MessageAttemptsDV.COL_RECIPIENT_DESCRIPTION).ToString
                            CType(e.Row.Cells(6).FindControl("lblStatus"), Label).Text = "In-Progress"
                        Else
                            If dvRow(OcMessageAttempts.MessageAttemptsDV.COL_PROCESS_STATUS_XCD).ToString.ToUpper() = "TASK_STATUS-C" Or
                               dvRow(OcMessageAttempts.MessageAttemptsDV.COL_PROCESS_STATUS_XCD).ToString.ToUpper() = "TASK_STATUS-F" Then
                                CType(e.Row.Cells(1).FindControl("btnSelect"), ImageButton).Visible = True
                            Else
                                CType(e.Row.Cells(1).FindControl("btnSelect"), ImageButton).Visible = False
                            End If
                            CType(e.Row.Cells(2).FindControl("lblRecipientAddress"), Label).Text = dvRow(OcMessageAttempts.MessageAttemptsDV.COL_RECIPIENT_ADDRESS).ToString
                            CType(e.Row.Cells(3).FindControl("lblRecipientDescription"), Label).Text = dvRow(OcMessageAttempts.MessageAttemptsDV.COL_RECIPIENT_DESCRIPTION).ToString
                            CType(e.Row.Cells(4).FindControl("lblAttemptedOn"), Label).Text = dvRow(OcMessageAttempts.MessageAttemptsDV.COL_MESSAGE_ATTEMPTED_ON).ToString
                            CType(e.Row.Cells(5).FindControl("lblAttemptedBy"), Label).Text = dvRow(OcMessageAttempts.MessageAttemptsDV.COL_MESSAGE_ATTEMPTED_BY).ToString
                            CType(e.Row.Cells(6).FindControl("lblStatus"), Label).Text = dvRow(OcMessageAttempts.MessageAttemptsDV.COL_PROCESS_STATUS_DESCRIPTION).ToString
                            CType(e.Row.Cells(7).FindControl("lblErrorMessage"), Label).Text = dvRow(OcMessageAttempts.MessageAttemptsDV.COL_MESSAGE_ERROR).ToString
                        End If
                    End If
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub MessageAttemptsGrid_Populate()
            Try
                With TheMessage
                    If Not .Id.Equals(Guid.Empty) Then
                        If Me.State.MessageAttemptsGrid_DV Is Nothing Then
                            Me.State.MessageAttemptsGrid_DV = MessageAttemptsGrid_GetDV()
                        End If
                    End If
                End With

                If Not Me.State.MessageAttemptsGrid_DV Is Nothing Then
                    Dim dv As OcMessageAttempts.MessageAttemptsDV

                    If Me.State.MessageAttemptsGrid_DV.Count = 0 Then
                        dv = Me.State.MessageAttemptsGrid_DV.AddNewRowToEmptyDV
                        SetPageAndSelectedIndexFromGuid(dv, Me.State.MessageAttemptsGrid_OcMessageAttemptId, Me.MessageAttemptsGrid, Me.State.MessageAttemptsGrid_PageIndex)
                        Me.MessageAttemptsGrid.DataSource = dv
                    Else
                        SetPageAndSelectedIndexFromGuid(Me.State.MessageAttemptsGrid_DV, Me.State.MessageAttemptsGrid_OcMessageAttemptId, Me.MessageAttemptsGrid, Me.State.MessageAttemptsGrid_PageIndex)
                        Me.MessageAttemptsGrid.DataSource = Me.State.MessageAttemptsGrid_DV
                    End If

                    Me.State.MessageAttemptsGrid_DV.Sort = Me.State.MessageAttemptsGrid_SortExpression
                    Me.MessageAttemptsGrid.AutoGenerateColumns = False

                    If Me.State.MessageAttemptsGrid_DV.Count = 0 Then
                        MessageAttemptsGrid_SortAndBind(dv)
                    Else
                        MessageAttemptsGrid_SortAndBind(Me.State.MessageAttemptsGrid_DV)
                    End If

                    If Me.State.MessageAttemptsGrid_DV.Count = 0 Then
                        For Each gvRow As GridViewRow In Me.MessageAttemptsGrid.Rows
                            gvRow.Visible = False
                            gvRow.Controls.Clear()
                        Next
                    End If
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Function MessageAttemptsGrid_GetDV() As OcMessageAttempts.MessageAttemptsDV
            Dim dv As OcMessageAttempts.MessageAttemptsDV
            dv = MessageAttemptsGrid_GetDataView()
            dv.Sort = Me.MessageAttemptsGrid.DataMember()
            Me.MessageAttemptsGrid.DataSource = dv
            Return (dv)
        End Function

        Private Function MessageAttemptsGrid_GetDataView() As OcMessageAttempts.MessageAttemptsDV
            Dim dt As DataTable = TheMessage.MessageAttemptsList.Table
            Return New OcMessageAttempts.MessageAttemptsDV(dt)
        End Function

        Private Sub MessageAttemptsGrid_SortAndBind(ByVal dvBinding As DataView, Optional ByVal blnEmptyList As Boolean = False)
            Me.MessageAttemptsGrid.DataSource = dvBinding
            HighLightSortColumn(Me.MessageAttemptsGrid, Me.State.MessageAttemptsGrid_SortExpression)
            Me.MessageAttemptsGrid.DataBind()

            If blnEmptyList Then
                For Each gvRow As GridViewRow In Me.MessageAttemptsGrid.Rows
                    gvRow.Controls.Clear()
                Next
            End If

            Session("recCount") = Me.State.MessageAttemptsGrid_DV.Count

            ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me, Me.MessageAttemptsGrid)
        End Sub

#End Region
#End Region
#End Region

    End Class

End Namespace