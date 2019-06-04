Imports System.Collections.Generic
Imports Assurant.ElitaPlus.DALObjects
Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Imports Microsoft.VisualBasic

Namespace Tables
    Partial Class AdhocOcMessageForm
        Inherits ElitaPlusSearchPage

#Region "Page State"

#Region "MyState"
        Class MyState
            Public TemplateGroupId As Guid = Guid.Empty

            Public TemplateId As Guid = Guid.Empty
            Public SearchBy As String = String.Empty
            Public ConditionId As Guid = Guid.Empty
            Public IsTemplateNew As Boolean = False
            Public ActionInProgress As DetailPageCommand = DetailPageCommand.Nothing_
            Public LastOperation As DetailPageCommand = DetailPageCommand.Nothing_
            Public boChanged As Boolean = False
            Public LastErrMsg As String
            Public HasDataChanged As Boolean
            Public Template As OcTemplate
            Public ScreenSnapShotBO As OcTemplate

            'Parameters Grid
            Public ParametersGrid_PageIndex As Integer
            Public ParametersGrid_DV As OcTemplateParams.TemplateParamsDV = Nothing
            Public ParametersGrid_SortExpression As String = OcTemplateParams.TemplateParamsDV.COL_PARAM_NAME
            Public ParametersGrid_IsInEditMode As Boolean
            Public ParametersGrid_TemplateParamsId As Guid = Guid.Empty
            Public ParametersGrid_TemplateParamsBO As OcTemplateParams
            Public ParametersGrid_RecordEdit As Boolean
            Public ParametersGrid_RecordDelete As Boolean
            Public ParametersGrid_RecordNew As Boolean
            Public ParametersGrid_IsAfterSave As Boolean

            'Recipients Grid
            Public RecipientsGrid_PageIndex As Integer
            Public RecipientsGrid_DV As OcTemplateRecipient.TemplateRecipientsDV = Nothing
            Public RecipientsGrid_SortExpression As String = OcTemplateRecipient.TemplateRecipientsDV.COL_DESCRIPTION
            Public RecipientsGrid_IsInEditMode As Boolean
            Public RecipientsGrid_TemplateRecipientId As Guid = Guid.Empty
            Public RecipientsGrid_TemplateRecipientBO As OcTemplateRecipient
            Public RecipientsGrid_RecordEdit As Boolean
            Public RecipientsGrid_RecordDelete As Boolean
            Public RecipientsGrid_RecordNew As Boolean
            Public RecipientsGrid_IsAfterSave As Boolean
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
            If Me.State.TemplateId.Equals(Guid.Empty) Then
                Me.State.IsTemplateNew = True
                Me.AddLabelDecorations(TheTemplate)
                PopulateAll()
            Else
                Me.State.IsTemplateNew = False
                Me.AddLabelDecorations(TheTemplate)
                PopulateAll()
            End If
        End Sub
#End Region

#Region "Constants"
        Private Const TEMPLATE_FORM001 As String = "TEMPLATE_FORM001" 'Template List Exception
        Private Const TEMPLATE_FORM002 As String = "TEMPLATE_FORM002" 'Template Code Field Exception
        Private Const TEMPLATE_FORM003 As String = "TEMPLATE_FORM003" 'Template Update Exception
        Private Const TEMPLATE_FORM004 As String = "TEMPLATE_FORM004" 'Template Delete Exception

        Private Const CONFIRM_MSG As String = "MGS_CONFIRM_PROMPT" '"Are you sure you want to delete the selected records?"
        Private Const MSG_RECORD_SAVED_OK As String = "MSG_RECORD_SAVED_OK" '"The record has been successfully saved"
        Private Const MSG_UNIQUE_VIOLATION As String = "MSG_DUPLICATE_KEY_CONSTRAINT_VIOLATED" '"Unique value is in use"
        Private Const UNIQUE_VIOLATION As String = "unique constraint"
        Private Const MSG_RECORD_NOT_SAVED As String = "MSG_RECORD_NOT_SAVED"

        Private Const NO_ROW_SELECTED_INDEX As Integer = -1

        Private Const EDIT_COMMAND As String = "EditRecord"
        Private Const CANCEL_COMMAND As String = "CancelRecord"
        Private Const SAVE_COMMAND As String = "SaveRecord"
        Private Const DELETE_COMMAND As String = "DeleteRecord"
        Private Const SORT_COMMAND As String = "Sort"

        Private Const FIRST_POS As Integer = 0
        Private Const COL_NAME As String = "ID"
        Private Const LABEL_DEALER As String = "DEALER_NAME"
        Private Const YES As String = "Y"
        Private Const NO As String = "N"

        Public Const URL As String = "AdhocOcMessageForm.aspx"

        Private Const ID_CONTROL_NAME As String = "IdLabel"

        Private Const GRID_PARAM_COL_OC_TEMPLATE_PARAMS_ID As Integer = 0

        Private Const GRID_PARAM_COL_PARAM_NAME As Integer = 1
        Private Const GRID_PARAM_COL_PARAM_NAME_LABEL_CONTROL_NAME As String = "lblParamName"
        Private Const GRID_PARAM_COL_PARAM_NAME_TEXTBOX_CONTROL_NAME As String = "txtParamName"

        Private Const GRID_PARAM_COL_PARAM_VALUE_SOURCE_XCD As Integer = 2
        Private Const GRID_PARAM_COL_PARAM_VALUE_SOURCE_XCD_LABEL_CONTROL_NAME As String = "lblParamValueSource"
        Private Const GRID_PARAM_COL_PARAM_VALUE_SOURCE_XCD_DROPDOWNLIST_CONTROL_NAME As String = "cboParamValueSource"

        Private Const GRID_PARAM_COL_PARAM_VALUE As Integer = 3
        Private Const GRID_PARAM_COL_PARAM_VALUE_LABEL_CONTROL_NAME As String = "lblParamValue"
        Private Const GRID_PARAM_COL_PARAM_VALUE_TEXTBOX_CONTROL_NAME As String = "txtParamValue"

        Private Const GRID_PARAM_COL_PARAM_DATA_TYPE_XCD As Integer = 4
        Private Const GRID_PARAM_COL_PARAM_DATA_TYPE_XCD_LABEL_CONTROL_NAME As String = "lblParamDataType"
        Private Const GRID_PARAM_COL_PARAM_DATA_TYPE_XCD_DROPDOWNLIST_CONTROL_NAME As String = "cboParamDataType"

        Private Const GRID_PARAM_COL_DATE_FORMAT_STRING As Integer = 5
        Private Const GRID_PARAM_COL_DATE_FORMAT_STRING_LABEL_CONTROL_NAME As String = "lblDateFormatString"
        Private Const GRID_PARAM_COL_DATE_FORMAT_STRING_TEXTBOX_CONTROL_NAME As String = "txtDateFormatString"

        Private Const GRID_PARAM_COL_ALLOW_EMPTY_VALUE_XCD As Integer = 6
        Private Const GRID_PARAM_COL_ALLOW_EMPTY_VALUE_XCD_LABEL_CONTROL_NAME As String = "lblAllowEmptyValue"
        Private Const GRID_PARAM_COL_ALLOW_EMPTY_VALUE_XCD_DROPDOWNLIST_CONTROL_NAME As String = "cboAllowEmptyValue"

        Private Const GRID_RECIPIENT_COL_OC_TEMPLATE_RECIPIENT_ID As Integer = 0

        Private Const GRID_RECIPIENT_COL_RECIPIENT_SOURCE_FIELD_XCD As Integer = 1
        Private Const GRID_RECIPIENT_COL_RECIPIENT_SOURCE_FIELD_XCD_LABEL_CONTROL_NAME As String = "lblRecipientSourceField"
        Private Const GRID_RECIPIENT_COL_RECIPIENT_SOURCE_FIELD_XCD_DROPDOWNLIST_CONTROL_NAME As String = "cboRecipientSourceField"

        Private Const GRID_RECIPIENT_COL_RECIPIENT_ADDRESS As Integer = 2
        Private Const GRID_RECIPIENT_COL_RECIPIENT_ADDRESS_LABEL_CONTROL_NAME As String = "lblRecipientAddress"
        Private Const GRID_RECIPIENT_COL_RECIPIENT_ADDRESS_TEXTBOX_CONTROL_NAME As String = "txtRecipientAddress"

        Private Const GRID_RECIPIENT_COL_DESCRIPTION As Integer = 3
        Private Const GRID_RECIPIENT_COL_DESCRIPTION_LABEL_CONTROL_NAME As String = "lblDescription"
        Private Const GRID_RECIPIENT_COL_DESCRIPTION_TEXTBOX_CONTROL_NAME As String = "txtDescription"
#End Region

#Region "Page Call Type"
        Public Class CallType
            Public SearchBy As String
            Public ConditionId As Guid
            Public Sub New(ByVal _searchby As String, ByVal _conditionid As Guid)
                Me.SearchBy = _searchby
                Me.ConditionId = _conditionid
            End Sub
        End Class
#End Region

#Region "Page Return Type"
        Public Class ReturnType
            Public LastOperation As DetailPageCommand
            Public TemplateId As Guid
            Public HasDataChanged As Boolean
            Public Sub New(ByVal LastOp As DetailPageCommand, ByVal templateId As Guid, ByVal hasDataChanged As Boolean)
                Me.LastOperation = LastOp
                Me.TemplateId = templateId
                Me.HasDataChanged = hasDataChanged
            End Sub
        End Class
#End Region

#Region "Properties"
        Private ReadOnly Property TheTemplate As OcTemplate
            Get
                If Me.State.Template Is Nothing Then
                    If Me.State.IsTemplateNew = True Then
                        ' For creating, inserting
                        Me.State.Template = New OcTemplate
                        Me.State.TemplateId = Me.State.Template.Id
                    Else
                        ' For updating, deleting
                        Me.State.Template = New OcTemplate(Me.State.TemplateId)
                    End If
                End If

                Return Me.State.Template
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
                    DealerMultipleDrop.Caption = TranslationBase.TranslateLabelOrMessage("DEALER")
                    TemplateMultipleDrop.Caption = TranslationBase.TranslateLabelOrMessage("TEMPLATE")

                    PopulateDealer()

                    Me.btnNewParameter_WRITE.Enabled = False
                    TranslateGridHeader(ParametersGrid)
                    Me.btnNewRecipient_WRITE.Enabled = False
                    TranslateGridHeader(RecipientsGrid)

                    UpdateBreadCrum()

                    If Me.State.IsTemplateNew = True Then
                        CreateNew()
                    End If
                Else
                    If Me.State.ParametersGrid_RecordDelete Then
                        ParametersGrid_CheckIfComingFromDeleteConfirm()
                        Me.State.ParametersGrid_RecordDelete = False
                    ElseIf Me.State.RecipientsGrid_RecordDelete Then
                        RecipientsGrid_CheckIfComingFromDeleteConfirm()
                        Me.State.RecipientsGrid_RecordDelete = False
                    End If
                End If

                ParametersGrid_BindBoPropertiesToHeaders()
                RecipientsGrid_BindBoPropertiesToHeaders()
                CheckIfComingFromConfirm()

            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try

            If Me.State.LastOperation = DetailPageCommand.Redirect_ Then
                Me.MasterPage.MessageController.Clear_Hide()
                Me.State.LastOperation = DetailPageCommand.Nothing_
            Else
                Me.ShowMissingTranslations(Me.MasterPage.MessageController)
            End If
        End Sub

        Private Sub PopulateDealer()
            Try
                DealerMultipleDrop.NothingSelected = True
                DealerMultipleDrop.AutoPostBackDD = True
                DealerMultipleDrop.NothingSelected = True
                DealerMultipleDrop.BindData(LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies))
            Catch ex As Exception
                Me.MasterPage.MessageController.AddError(ex.Message, False)
                Me.MasterPage.MessageController.Show()
            End Try
        End Sub

        Protected Sub DealerMultipleDrop_SelectedDropChanged() Handles DealerMultipleDrop.SelectedDropChanged
            Try
                TemplateMultipleDrop.NothingSelected = True
                TemplateMultipleDrop.AutoPostBackDD = True
                TemplateMultipleDrop.NothingSelected = True
                Dim templateListdv As DataView = LookupListNew.GetTemplateLookupList(DealerMultipleDrop.SelectedCode)
                templateListdv.RowFilter = templateListdv.RowFilter + " AND " + CStr("effective_date <= #" + Today.Date.ToString("MM/dd/yyyy") + "# AND expiration_date >= #" + Today.Date.ToString("MM/dd/yyyy") + "# AND allow_manual_use_xcd = 'YESNO-Y'")
                TemplateMultipleDrop.BindData(templateListdv)
            Catch ex As Exception
                Me.MasterPage.MessageController.AddError(ex.Message, False)
                Me.MasterPage.MessageController.Show()
            End Try
        End Sub

        Protected Sub TemplateMultipleDrop_SelectedDropChanged() Handles TemplateMultipleDrop.SelectedDropChanged
            Try
                Me.State.TemplateId = TemplateMultipleDrop.SelectedGuid
                Me.AddLabelDecorations(TheTemplate)
                Me.SetStateProperties()
                Me.AddLabelDecorations(TheTemplate)
                If Me.State.TemplateId <> Guid.Empty Then
                    If Me.State.Template.HasCustomizedParamsXcd = "YESNO-Y" Then
                        ControlMgr.SetEnableControl(Me, btnNewParameter_WRITE, True)
                    End If
                    ControlMgr.SetEnableControl(Me, btnNewRecipient_WRITE, True)
                End If
            Catch ex As Exception
                Me.MasterPage.MessageController.AddError(ex.Message, False)
                Me.MasterPage.MessageController.Show()
            End Try
        End Sub

        Private Sub Page_PageCall(ByVal CallFromUrl As String, ByVal CallingPar As Object) Handles MyBase.PageCall
            Try
                Dim callObj As AdhocOcMessageForm.CallType = CType(CallingPar, AdhocOcMessageForm.CallType)

                If callObj Is Nothing Then
                    Throw New ArgumentNullException()
                End If

                If Not String.IsNullOrEmpty(callObj.SearchBy) Then
                    Me.State.SearchBy = callObj.SearchBy
                Else
                    Throw New ArgumentException()
                End If

                If callObj.ConditionId <> Guid.Empty Then
                    Me.State.ConditionId = callObj.ConditionId
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
        Private Sub btnApply_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnApply_WRITE.Click
            If Not Me.State.TemplateId = Guid.Empty Then
                ApplyChanges()
            End If
        End Sub

        Private Sub GoBack()
            Me.ReturnToCallingPage()
        End Sub

        Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
            Try
                If Me.State.boChanged = True Then
                    GoBack()
                End If

                If IsDirtyBO() = True Then
                    Me.DisplayMessage(Message.MSG_EMAIL_NOT_SENT_SEND_PROMPT, "SEND_ADHOC_MESSAGE", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                    Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
                Else
                    GoBack()
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub CreateNew()
            Me.State.ScreenSnapShotBO = Nothing
            Me.State.TemplateId = Guid.Empty
            Me.State.IsTemplateNew = True
            Me.State.Template = New OcTemplate
            Me.State.ParametersGrid_DV = Nothing
            Me.State.RecipientsGrid_DV = Nothing
            Me.DisabledTabsList.Clear()
            Me.PopulateAll()
        End Sub

        Private Sub CreateNewCopy()
            Me.PopulateBOsFromForm()

            Dim newObj As New OcTemplate
            newObj.Copy(TheTemplate)

            Me.State.Template = newObj
            Me.State.TemplateId = Guid.Empty
            Me.State.IsTemplateNew = True

            With TheTemplate
                .TemplateCode = Nothing
                .Description = Nothing
                .HasCustomizedParamsXcd = Nothing
                .AllowManualUseXcd = Nothing
                .AllowManualResendXcd = Nothing
                .EffectiveDate = Nothing
                .ExpirationDate = Nothing
            End With

            Me.State.ParametersGrid_DV = Nothing
            Me.State.RecipientsGrid_DV = Nothing
            Me.ParametersGrid_Populate()
            Me.RecipientsGrid_Populate()

            Me.DisabledTabsList.Clear()

            'create the backup copy
            Me.State.ScreenSnapShotBO = New OcTemplate
            Me.State.ScreenSnapShotBO.Copy(TheTemplate)
        End Sub

#End Region

#Region "Populate"
        Private Sub UpdateBreadCrum()
            If (Not Me.State Is Nothing) Then
                If (Not Me.State Is Nothing) Then
                    Me.MasterPage.BreadCrum = Me.MasterPage.PageTab & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage("ADHOC_MESSAGE_DETAIL")
                    Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("ADHOC_MESSAGE_DETAIL")
                End If
            End If
        End Sub

        Private Sub PopulateAll()
            If Me.State.IsTemplateNew = True Then
                ParametersGrid_Populate()
                RecipientsGrid_Populate()
            Else
                ParametersGrid_Populate()
                RecipientsGrid_Populate()
            End If
        End Sub

        Protected Sub PopulateBOsFromForm()
            If Me.ErrCollection.Count > 0 Then
                Throw New PopulateBOErrorException
            End If
        End Sub

#End Region

#Region "Tabs"
        Public Const Tab_Parameters As String = "0"
        Public Const Tab_Recipients As String = "1"

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

#Region "Business Part"
        Private Function IsDirtyBO() As Boolean
            Dim bIsDirty As Boolean = True

            If Me.State.TemplateId = Guid.Empty Then
                Return False
            End If

            Try
                With TheTemplate
                    PopulateBOsFromForm()
                    bIsDirty = .IsDirty Or TheTemplate.IsChildrenDirty
                End With
            Catch ex As Exception
                Me.MasterPage.MessageController.AddError(TEMPLATE_FORM001)
                Me.MasterPage.MessageController.AddError(ex.Message, False)
                Me.MasterPage.MessageController.Show()
            End Try
            Return bIsDirty
        End Function

        Private Function ApplyChanges() As Boolean
            Try
                Me.PopulateBOsFromForm()
                TheTemplate.Validate()

                Dim errors As List(Of ValidationError) = New List(Of ValidationError)
                If errors.Count > 0 Then
                    Throw New BOValidationException(errors.ToArray, GetType(OcTemplate).FullName)
                End If

                Dim FieldSeparator As String = "<<>>"
                Dim ValueSeparator As String = "||"

                'pi_std_recipient & 'pi_cst_recipient
                Dim pi_cst_recipient As String = String.Empty
                Dim pi_std_recipient As String = String.Empty
                For Each recipient As OcTemplateRecipient In TheTemplate.RecipientsList
                    If String.IsNullOrEmpty(recipient.RecipientSourceFieldXcd) Then 'Custom Recipient
                        pi_cst_recipient = pi_cst_recipient + FieldSeparator + recipient.RecipientAddress.Trim() + ValueSeparator + recipient.Description.Trim()
                    Else ' Standard Recipient
                        pi_std_recipient = pi_std_recipient + FieldSeparator + recipient.RecipientSourceFieldXcd
                    End If
                Next

                If Not String.IsNullOrEmpty(pi_cst_recipient) AndAlso pi_cst_recipient.StartsWith(FieldSeparator) Then
                    pi_cst_recipient = pi_cst_recipient.Substring(Len(FieldSeparator))
                End If

                If Not String.IsNullOrEmpty(pi_std_recipient) AndAlso pi_std_recipient.StartsWith(FieldSeparator) Then
                    pi_std_recipient = pi_std_recipient.Substring(Len(FieldSeparator))
                End If

                'pi_std_parameter & 'pi_cst_parameter
                Dim pi_cst_parameter As String = String.Empty
                Dim pi_std_parameter As String = String.Empty

                For Each parameter As OcTemplateParams In TheTemplate.ParametersList
                    If String.IsNullOrEmpty(parameter.ParamValueSourceXcd) Then 'Custom Parameter
                        pi_cst_parameter = pi_cst_parameter + FieldSeparator +
                                           If(Not String.IsNullOrEmpty(parameter.ParamName), parameter.ParamName.Trim(), String.Empty) + ValueSeparator +
                                           parameter.DateFormatString + ValueSeparator +
                                           If(Not String.IsNullOrEmpty(parameter.ParamValue), parameter.ParamValue.Trim(), String.Empty) + ValueSeparator +
                                           parameter.AllowEmptyValueXcd
                    Else ' Standard Parameter
                        pi_std_parameter = pi_std_parameter + FieldSeparator +
                                           If(Not String.IsNullOrEmpty(parameter.ParamName), parameter.ParamName.Trim(), String.Empty) + ValueSeparator +
                                           parameter.DateFormatString + ValueSeparator +
                                           If(Not String.IsNullOrEmpty(parameter.ParamValueSourceXcd), parameter.ParamValueSourceXcd.Trim(), String.Empty) + ValueSeparator +
                                           parameter.AllowEmptyValueXcd
                    End If
                Next

                pi_cst_parameter = pi_cst_parameter.Replace(ValueSeparator + ValueSeparator, ValueSeparator + "-" + ValueSeparator)
                If Not String.IsNullOrEmpty(pi_cst_parameter) AndAlso pi_cst_parameter.StartsWith(FieldSeparator) Then
                    pi_cst_parameter = pi_cst_parameter.Substring(Len(FieldSeparator))
                End If

                pi_std_parameter = pi_std_parameter.Replace(ValueSeparator + ValueSeparator, ValueSeparator + "-" + ValueSeparator)
                If Not String.IsNullOrEmpty(pi_std_parameter) AndAlso pi_std_parameter.StartsWith(FieldSeparator) Then
                    pi_std_parameter = pi_std_parameter.Substring(Len(FieldSeparator))
                End If

                Dim OcMessage_id As Guid = Guid.Empty
                Dim Err_Code As Integer = 0
                Dim Err_Message As String = String.Empty

                Dim msg As OcMessage = New OcMessage()
                msg.SendAdhocMessage(Me.DealerMultipleDrop.SelectedGuid, Me.State.SearchBy, Me.State.ConditionId, TheTemplate.TemplateCode,
                                     pi_std_recipient, pi_cst_recipient, pi_std_parameter, pi_cst_parameter, ElitaPlusIdentity.Current.ActiveUser.NetworkId,
                                     OcMessage_id, Err_Code, Err_Message)

                If Err_Code = 0 Then
                    Me.MasterPage.MessageController.AddSuccess(Message.MSG_EMAIL_SENT)
                    btnApply_WRITE.Enabled = False
                    Me.State.boChanged = True
                Else
                    Me.MasterPage.MessageController.AddInformation(Message.MSG_EMAIL_NOT_SENT + "<BR/>" + Err_Message)
                End If

            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Function

#End Region

#Region "State-Management"
        Protected Sub ComingFromBack()
            Dim confResponse As String = Me.HiddenSaveChangesPromptResponse.Value

            If Not confResponse = String.Empty Then
                ' Return from the Back Button
                Select Case confResponse
                    Case MSG_VALUE_YES
                        ' Save and go back to Search Page
                        If ApplyChanges() = True Then
                            GoBack()
                        End If
                    Case MSG_VALUE_NO
                        GoBack()
                End Select
            End If
        End Sub

        Protected Sub CheckIfComingFromConfirm()
            Try
                Select Case Me.State.ActionInProgress
                    ' Period
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
#End Region

#Region "Handlers-Labels"
        Public Shared Sub SetLabelColor(ByVal lbl As Label)
            lbl.ForeColor = Color.Black
        End Sub
#End Region

#Region "Datagrid Related"
#Region "Parameters Grid"
        Public Sub ParametersGrid_RowCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)
            Dim index As Integer

            Try
                If e.CommandName = EDIT_COMMAND_NAME Then
                    index = CInt(e.CommandArgument)
                    ParametersGrid.EditIndex = index
                    ParametersGrid.SelectedIndex = index

                    Me.State.ParametersGrid_IsInEditMode = True
                    Me.State.ParametersGrid_TemplateParamsId = New Guid(CType(Me.ParametersGrid.Rows(index).Cells(GRID_PARAM_COL_OC_TEMPLATE_PARAMS_ID).FindControl(ID_CONTROL_NAME), Label).Text)

                    Me.State.ParametersGrid_TemplateParamsBO = TheTemplate.GetParameterChild(Me.State.ParametersGrid_TemplateParamsId)

                    ParametersGrid_Populate()

                    'Disable all Edit and Delete icon buttons on the Grid
                    SetGridControls(Me.ParametersGrid, False)
                    Me.State.ParametersGrid_PageIndex = ParametersGrid.PageIndex

                    Me.State.ParametersGrid_RecordEdit = True

                    ParametersGrid_PopulateFormFromBO(index)

                    ParametersGrid_SetFocusOnEditableField(Me.ParametersGrid, GRID_PARAM_COL_PARAM_NAME, GRID_PARAM_COL_PARAM_NAME_TEXTBOX_CONTROL_NAME, index)
                    ParametersGrid_SetButtonsState(False)
                ElseIf (e.CommandName = DELETE_COMMAND) Then
                    index = CInt(e.CommandArgument)

                    Me.ParametersGrid_Populate()
                    Me.State.ParametersGrid_PageIndex = ParametersGrid.PageIndex

                    'Clear the SelectedItemStyle to remove the highlight from the previously saved row
                    ParametersGrid.SelectedIndex = NO_ROW_SELECTED_INDEX
                    'Save the Id in the Session
                    Me.State.ParametersGrid_TemplateParamsId = New Guid(CType(Me.ParametersGrid.Rows(index).Cells(GRID_PARAM_COL_OC_TEMPLATE_PARAMS_ID).FindControl(ID_CONTROL_NAME), Label).Text)
                    Me.DisplayMessage(Message.DELETE_RECORD_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, Me.HiddenDeletePromptResponse)
                    Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Delete
                    Me.State.ParametersGrid_RecordDelete = True
                ElseIf (e.CommandName = SAVE_COMMAND) Then
                    ParametersGrid_Save()
                ElseIf (e.CommandName = CANCEL_COMMAND) Then
                    ParametersGrid_Cancel()
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Public Sub ParametersGrid_RowCreated(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
            BaseItemCreated(sender, e)
        End Sub

        Private Sub ParametersGrid__PageIndexChanging(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles ParametersGrid.PageIndexChanging
            Try
                If (Not (Me.State.ParametersGrid_IsInEditMode)) Then
                    Me.State.ParametersGrid_PageIndex = e.NewPageIndex
                    Me.ParametersGrid.PageIndex = Me.State.ParametersGrid_PageIndex
                    Me.ParametersGrid_Populate()
                    Me.ParametersGrid.SelectedIndex = NO_ITEM_SELECTED_INDEX
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub ParametersGrid_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles ParametersGrid.RowDataBound
            Try
                Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
                Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)

                If Not dvRow Is Nothing And Me.State.ParametersGrid_DV.Count > 0 Then
                    If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Or itemType = ListItemType.EditItem Then
                        CType(e.Row.Cells(GRID_PARAM_COL_OC_TEMPLATE_PARAMS_ID).FindControl(Me.ID_CONTROL_NAME), Label).Text = GetGuidStringFromByteArray(CType(dvRow(OcTemplateParams.TemplateParamsDV.COL_OC_TEMPLATE_PARAMS_ID), Byte()))

                        If (Me.State.ParametersGrid_IsInEditMode = True AndAlso Me.State.ParametersGrid_TemplateParamsId.ToString.Equals(GetGuidStringFromByteArray(CType(dvRow(OcTemplateParams.TemplateParamsDV.COL_OC_TEMPLATE_PARAMS_ID), Byte())))) Then
                            CType(e.Row.Cells(GRID_PARAM_COL_PARAM_NAME).FindControl(GRID_PARAM_COL_PARAM_NAME_TEXTBOX_CONTROL_NAME), TextBox).Text = dvRow(OcTemplateParams.TemplateParamsDV.COL_PARAM_NAME).ToString
                            CType(e.Row.Cells(GRID_PARAM_COL_DATE_FORMAT_STRING).FindControl(GRID_PARAM_COL_DATE_FORMAT_STRING_TEXTBOX_CONTROL_NAME), TextBox).Text = dvRow(OcTemplateParams.TemplateParamsDV.COL_DATE_FORMAT_STRING).ToString

                            Me.BindCodeToListControl(CType(e.Row.Cells(Me.GRID_PARAM_COL_PARAM_VALUE_SOURCE_XCD).FindControl(Me.GRID_PARAM_COL_PARAM_VALUE_SOURCE_XCD_DROPDOWNLIST_CONTROL_NAME), DropDownList),
                                                       LookupListNew.GetParamValueSourceLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), , , True)

                            CType(e.Row.Cells(GRID_PARAM_COL_PARAM_VALUE).FindControl(GRID_PARAM_COL_PARAM_VALUE_TEXTBOX_CONTROL_NAME), TextBox).Text = dvRow(OcTemplateParams.TemplateParamsDV.COL_PARAM_VALUE).ToString

                            Me.BindCodeToListControl(CType(e.Row.Cells(Me.GRID_PARAM_COL_PARAM_DATA_TYPE_XCD).FindControl(Me.GRID_PARAM_COL_PARAM_DATA_TYPE_XCD_DROPDOWNLIST_CONTROL_NAME), DropDownList),
                                                       LookupListNew.GetParamDataTypeLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), , , True)

                            Me.BindCodeToListControl(CType(e.Row.Cells(Me.GRID_PARAM_COL_ALLOW_EMPTY_VALUE_XCD).FindControl(Me.GRID_PARAM_COL_ALLOW_EMPTY_VALUE_XCD_DROPDOWNLIST_CONTROL_NAME), DropDownList),
                                                       LookupListNew.GetYesNoXcdList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), , , True)
                        Else
                            CType(e.Row.Cells(GRID_PARAM_COL_PARAM_NAME).FindControl(GRID_PARAM_COL_PARAM_NAME_LABEL_CONTROL_NAME), Label).Text = dvRow(OcTemplateParams.TemplateParamsDV.COL_PARAM_NAME).ToString
                            CType(e.Row.Cells(GRID_PARAM_COL_DATE_FORMAT_STRING).FindControl(GRID_PARAM_COL_DATE_FORMAT_STRING_LABEL_CONTROL_NAME), Label).Text = dvRow(OcTemplateParams.TemplateParamsDV.COL_DATE_FORMAT_STRING).ToString
                            CType(e.Row.Cells(GRID_PARAM_COL_PARAM_VALUE_SOURCE_XCD).FindControl(GRID_PARAM_COL_PARAM_VALUE_SOURCE_XCD_LABEL_CONTROL_NAME), Label).Text = dvRow(OcTemplateParams.TemplateParamsDV.COL_PARAM_VALUE_SOURCE_DESCRIPTION).ToString
                            CType(e.Row.Cells(GRID_PARAM_COL_PARAM_VALUE).FindControl(GRID_PARAM_COL_PARAM_VALUE_LABEL_CONTROL_NAME), Label).Text = dvRow(OcTemplateParams.TemplateParamsDV.COL_PARAM_VALUE).ToString
                            CType(e.Row.Cells(GRID_PARAM_COL_PARAM_DATA_TYPE_XCD).FindControl(GRID_PARAM_COL_PARAM_DATA_TYPE_XCD_LABEL_CONTROL_NAME), Label).Text = dvRow(OcTemplateParams.TemplateParamsDV.COL_PARAM_DATA_TYPE_DESCRIPTION).ToString
                            CType(e.Row.Cells(GRID_PARAM_COL_ALLOW_EMPTY_VALUE_XCD).FindControl(GRID_PARAM_COL_ALLOW_EMPTY_VALUE_XCD_LABEL_CONTROL_NAME), Label).Text = dvRow(OcTemplateParams.TemplateParamsDV.COL_ALLOW_EMPTY_VALUE_DESCRIPTION).ToString
                        End If
                    End If
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub ParametersGrid_Populate()
            Try
                With TheTemplate
                    If Not .Id.Equals(Guid.Empty) Then
                        If Me.State.ParametersGrid_DV Is Nothing Then
                            Me.State.ParametersGrid_DV = ParametersGrid_GetDV()
                        End If
                    End If
                End With

                If Not Me.State.ParametersGrid_DV Is Nothing Then
                    Dim dv As OcTemplateParams.TemplateParamsDV

                    If Me.State.ParametersGrid_DV.Count = 0 Then
                        dv = Me.State.ParametersGrid_DV.AddNewRowToEmptyDV
                        SetPageAndSelectedIndexFromGuid(dv, Me.State.ParametersGrid_TemplateParamsId, Me.ParametersGrid, Me.State.ParametersGrid_PageIndex)
                        Me.ParametersGrid.DataSource = dv
                    Else
                        SetPageAndSelectedIndexFromGuid(Me.State.ParametersGrid_DV, Me.State.ParametersGrid_TemplateParamsId, Me.ParametersGrid, Me.State.ParametersGrid_PageIndex)
                        Me.ParametersGrid.DataSource = Me.State.ParametersGrid_DV
                    End If

                    If (Me.State.ParametersGrid_IsAfterSave) Then
                        Me.State.ParametersGrid_IsAfterSave = False
                        Me.SetPageAndSelectedIndexFromGuid(Me.State.ParametersGrid_DV, Me.State.ParametersGrid_TemplateParamsId, Me.ParametersGrid, Me.ParametersGrid.PageIndex)
                    ElseIf (Me.State.ParametersGrid_IsInEditMode) Then
                        Me.SetPageAndSelectedIndexFromGuid(Me.State.ParametersGrid_DV, Me.State.ParametersGrid_TemplateParamsId, Me.ParametersGrid, Me.ParametersGrid.PageIndex, Me.State.ParametersGrid_IsInEditMode)
                    Else
                        'In a Delete scenario...
                        Me.SetPageAndSelectedIndexFromGuid(Me.State.ParametersGrid_DV, Guid.Empty, Me.ParametersGrid, Me.ParametersGrid.PageIndex, Me.State.ParametersGrid_IsInEditMode)
                    End If

                    Me.ParametersGrid.AutoGenerateColumns = False

                    If Me.State.ParametersGrid_DV.Count = 0 Then
                        ParametersGrid_SortAndBind(dv)
                    Else
                        ParametersGrid_SortAndBind(Me.State.ParametersGrid_DV)
                    End If

                    If Me.State.ParametersGrid_DV.Count = 0 Then
                        For Each gvRow As GridViewRow In Me.ParametersGrid.Rows
                            gvRow.Visible = False
                            gvRow.Controls.Clear()
                        Next
                    End If
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub ParametersGrid_BindBoPropertiesToHeaders()
            If Not Me.State.ParametersGrid_TemplateParamsBO Is Nothing Then
                Me.BindBOPropertyToGridHeader(Me.State.ParametersGrid_TemplateParamsBO, "ParamName", Me.ParametersGrid.Columns(Me.GRID_PARAM_COL_PARAM_NAME))
                Me.BindBOPropertyToGridHeader(Me.State.ParametersGrid_TemplateParamsBO, "ParamValueSourceXcd", Me.ParametersGrid.Columns(Me.GRID_PARAM_COL_PARAM_VALUE_SOURCE_XCD))
                Me.BindBOPropertyToGridHeader(Me.State.ParametersGrid_TemplateParamsBO, "ParamValue", Me.ParametersGrid.Columns(Me.GRID_PARAM_COL_PARAM_VALUE))
                Me.BindBOPropertyToGridHeader(Me.State.ParametersGrid_TemplateParamsBO, "ParamDataTypeXcd", Me.ParametersGrid.Columns(Me.GRID_PARAM_COL_PARAM_DATA_TYPE_XCD))
                Me.BindBOPropertyToGridHeader(Me.State.ParametersGrid_TemplateParamsBO, "DateFormatString", Me.ParametersGrid.Columns(Me.GRID_PARAM_COL_DATE_FORMAT_STRING))
                Me.BindBOPropertyToGridHeader(Me.State.ParametersGrid_TemplateParamsBO, "AllowEmptyValueXcd", Me.ParametersGrid.Columns(Me.GRID_PARAM_COL_ALLOW_EMPTY_VALUE_XCD))
            End If
            Me.ClearGridViewHeadersAndLabelsErrSign()
        End Sub

        Private Sub ParametersGrid_SetFocusOnEditableField(ByVal grid As GridView, ByVal cellPosition As Integer, ByVal controlName As String, ByVal itemIndex As Integer)
            Dim control As TextBox = CType(grid.Rows(itemIndex).Cells(cellPosition).FindControl(controlName), TextBox)
            SetFocus(control)
        End Sub

        Private Sub ParametersGrid_PopulateFormFromBO(Optional ByVal gridRowIdx As Integer? = Nothing)
            If IsNothing(gridRowIdx) Then gridRowIdx = Me.ParametersGrid.EditIndex

            Dim cboParamValueSource As DropDownList = CType(Me.ParametersGrid.Rows(gridRowIdx).Cells(GRID_PARAM_COL_PARAM_VALUE_SOURCE_XCD).FindControl(GRID_PARAM_COL_PARAM_VALUE_SOURCE_XCD_DROPDOWNLIST_CONTROL_NAME), DropDownList)
            Me.BindCodeToListControl(cboParamValueSource, LookupListNew.GetParamValueSourceLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), , , True)

            Dim cboParamDataType As DropDownList = CType(Me.ParametersGrid.Rows(gridRowIdx).Cells(GRID_PARAM_COL_PARAM_DATA_TYPE_XCD).FindControl(GRID_PARAM_COL_PARAM_DATA_TYPE_XCD_DROPDOWNLIST_CONTROL_NAME), DropDownList)
            Me.BindCodeToListControl(cboParamDataType, LookupListNew.GetParamDataTypeLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), , , True)

            Dim cboAllowEmptyValue As DropDownList = CType(Me.ParametersGrid.Rows(gridRowIdx).Cells(GRID_PARAM_COL_ALLOW_EMPTY_VALUE_XCD).FindControl(GRID_PARAM_COL_ALLOW_EMPTY_VALUE_XCD_DROPDOWNLIST_CONTROL_NAME), DropDownList)
            Me.BindCodeToListControl(cboAllowEmptyValue, LookupListNew.GetYesNoXcdList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), , , True)

            Try
                With Me.State.ParametersGrid_TemplateParamsBO
                    If (Not .Id.Equals(Guid.Empty)) AndAlso Not .IsNew Then
                        If Not String.IsNullOrEmpty(.ParamValueSourceXcd) Then
                            SetSelectedItem(cboParamValueSource, .ParamValueSourceXcd)
                        End If

                        If Not String.IsNullOrEmpty(.ParamDataTypeXcd) Then
                            SetSelectedItem(cboParamDataType, .ParamDataTypeXcd)
                        End If

                        If Not String.IsNullOrEmpty(.AllowEmptyValueXcd) Then
                            SetSelectedItem(cboAllowEmptyValue, .AllowEmptyValueXcd)
                        End If
                    End If

                    Dim txtParamName As TextBox = CType(Me.ParametersGrid.Rows(gridRowIdx).Cells(GRID_PARAM_COL_PARAM_NAME).FindControl(GRID_PARAM_COL_PARAM_NAME_TEXTBOX_CONTROL_NAME), TextBox)
                    Me.PopulateControlFromBOProperty(txtParamName, .ParamName)

                    Dim txtDateFormatString As TextBox = CType(Me.ParametersGrid.Rows(gridRowIdx).Cells(GRID_PARAM_COL_DATE_FORMAT_STRING).FindControl(GRID_PARAM_COL_DATE_FORMAT_STRING_TEXTBOX_CONTROL_NAME), TextBox)
                    Me.PopulateControlFromBOProperty(txtDateFormatString, .DateFormatString)

                    Dim txtParamValue As TextBox = CType(Me.ParametersGrid.Rows(gridRowIdx).Cells(GRID_PARAM_COL_PARAM_VALUE).FindControl(GRID_PARAM_COL_PARAM_VALUE_TEXTBOX_CONTROL_NAME), TextBox)
                    Me.PopulateControlFromBOProperty(txtParamValue, .ParamValue)

                    CType(Me.ParametersGrid.Rows(gridRowIdx).Cells(GRID_PARAM_COL_OC_TEMPLATE_PARAMS_ID).FindControl(ID_CONTROL_NAME), Label).Text = .Id.ToString
                End With
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub ParametersGrid_SetButtonsState(ByVal bIsEdit As Boolean)
            If bIsEdit Then
                If Me.State.TemplateId <> Guid.Empty Then
                    If Me.State.Template.HasCustomizedParamsXcd = "YESNO-Y" Then
                        ControlMgr.SetEnableControl(Me, btnNewParameter_WRITE, bIsEdit)
                    End If
                    ControlMgr.SetEnableControl(Me, btnNewRecipient_WRITE, bIsEdit)
                End If
            Else
                ControlMgr.SetEnableControl(Me, btnNewParameter_WRITE, bIsEdit)
                ControlMgr.SetEnableControl(Me, btnNewRecipient_WRITE, bIsEdit)
            End If

            ControlMgr.SetEnableControl(Me, btnApply_WRITE, bIsEdit)
            ControlMgr.SetEnableControl(Me, btnBack, bIsEdit)
        End Sub

        Private Function ParametersGrid_GetDV() As OcTemplateParams.TemplateParamsDV
            Dim dv As OcTemplateParams.TemplateParamsDV
            dv = ParametersGrid_GetDataView()
            dv.Sort = Me.State.ParametersGrid_SortExpression
            Me.ParametersGrid.DataSource = dv
            Return (dv)
        End Function

        Private Function ParametersGrid_GetDataView() As OcTemplateParams.TemplateParamsDV
            Dim dt As DataTable = TheTemplate.ParametersList.Table
            Return New OcTemplateParams.TemplateParamsDV(dt)
        End Function

        Private Sub ParametersGrid_SortAndBind(ByVal dvBinding As DataView, Optional ByVal blnEmptyList As Boolean = False)
            Me.ParametersGrid.DataSource = dvBinding
            HighLightSortColumn(Me.ParametersGrid, Me.State.ParametersGrid_SortExpression)
            Me.ParametersGrid.DataBind()

            If Not Me.ParametersGrid.BottomPagerRow.Visible Then Me.ParametersGrid.BottomPagerRow.Visible = True

            If blnEmptyList Then
                For Each gvRow As GridViewRow In Me.ParametersGrid.Rows
                    gvRow.Controls.Clear()
                Next
            End If

            Session("recCount") = Me.State.ParametersGrid_DV.Count

            ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me, Me.ParametersGrid)
        End Sub

        Private Sub ParametersGrid_AddNew()
            Me.State.ParametersGrid_TemplateParamsBO = TheTemplate.GetNewParameterChild
            Me.State.ParametersGrid_DV = ParametersGrid_GetDV()
            Me.State.ParametersGrid_TemplateParamsId = Me.State.ParametersGrid_TemplateParamsBO.Id
            Me.ParametersGrid.DataSource = Me.State.ParametersGrid_DV
            Me.SetPageAndSelectedIndexFromGuid(Me.State.ParametersGrid_DV, Me.State.ParametersGrid_TemplateParamsId, Me.ParametersGrid, Me.State.ParametersGrid_PageIndex, Me.State.ParametersGrid_IsInEditMode)
            Me.ParametersGrid.AutoGenerateColumns = False
            ParametersGrid_SortAndBind(Me.State.ParametersGrid_DV)
            SetGridControls(Me.ParametersGrid, False)
            Me.State.ParametersGrid_RecordNew = True
            ParametersGrid_PopulateFormFromBO()
        End Sub

        Private Sub ParametersGrid_ReturnFromEditing()
            Me.ParametersGrid.EditIndex = NO_ROW_SELECTED_INDEX

            If Me.ParametersGrid.PageCount = 0 Then
                'if returning to the "1st time in" screen
                ControlMgr.SetVisibleControl(Me, Me.ParametersGrid, False)
            Else
                ControlMgr.SetVisibleControl(Me, Me.ParametersGrid, True)
            End If

            SetGridControls(Me.ParametersGrid, True)
            Me.State.ParametersGrid_IsInEditMode = False
            Me.State.ParametersGrid_TemplateParamsId = Guid.Empty
            Me.State.ParametersGrid_TemplateParamsBO = Nothing
            Me.ParametersGrid_Populate()
            Me.State.ParametersGrid_PageIndex = Me.ParametersGrid.PageIndex
            ParametersGrid_SetButtonsState(True)
        End Sub

        Private Sub ParametersGrid_PopulateBOFromForm()
            Dim txtParamName As TextBox = CType(Me.ParametersGrid.Rows(Me.ParametersGrid.EditIndex).Cells(GRID_PARAM_COL_PARAM_NAME).FindControl(Me.GRID_PARAM_COL_PARAM_NAME_TEXTBOX_CONTROL_NAME), TextBox)
            Dim cboParamValueSource As DropDownList = CType(Me.ParametersGrid.Rows(Me.ParametersGrid.EditIndex).Cells(GRID_PARAM_COL_PARAM_VALUE_SOURCE_XCD).FindControl(Me.GRID_PARAM_COL_PARAM_VALUE_SOURCE_XCD_DROPDOWNLIST_CONTROL_NAME), DropDownList)
            Dim txtParamValue As TextBox = CType(Me.ParametersGrid.Rows(Me.ParametersGrid.EditIndex).Cells(GRID_PARAM_COL_PARAM_VALUE).FindControl(Me.GRID_PARAM_COL_PARAM_VALUE_TEXTBOX_CONTROL_NAME), TextBox)
            Dim cboParamDataType As DropDownList = CType(Me.ParametersGrid.Rows(Me.ParametersGrid.EditIndex).Cells(GRID_PARAM_COL_PARAM_DATA_TYPE_XCD).FindControl(Me.GRID_PARAM_COL_PARAM_DATA_TYPE_XCD_DROPDOWNLIST_CONTROL_NAME), DropDownList)
            Dim txtDateFormatString As TextBox = CType(Me.ParametersGrid.Rows(Me.ParametersGrid.EditIndex).Cells(GRID_PARAM_COL_DATE_FORMAT_STRING).FindControl(Me.GRID_PARAM_COL_DATE_FORMAT_STRING_TEXTBOX_CONTROL_NAME), TextBox)
            Dim cboAllowEmptyValue As DropDownList = CType(Me.ParametersGrid.Rows(Me.ParametersGrid.EditIndex).Cells(GRID_PARAM_COL_ALLOW_EMPTY_VALUE_XCD).FindControl(Me.GRID_PARAM_COL_ALLOW_EMPTY_VALUE_XCD_DROPDOWNLIST_CONTROL_NAME), DropDownList)

            PopulateBOProperty(Me.State.ParametersGrid_TemplateParamsBO, "OcTemplateId", TheTemplate.Id)
            PopulateBOProperty(Me.State.ParametersGrid_TemplateParamsBO, "ParamName", txtParamName)
            PopulateBOProperty(Me.State.ParametersGrid_TemplateParamsBO, "ParamValueSourceXcd", cboParamValueSource, False, True)
            PopulateBOProperty(Me.State.ParametersGrid_TemplateParamsBO, "ParamValueSourceDescription", cboParamValueSource, False, False)
            PopulateBOProperty(Me.State.ParametersGrid_TemplateParamsBO, "ParamValue", txtParamValue)
            PopulateBOProperty(Me.State.ParametersGrid_TemplateParamsBO, "ParamDataTypeXcd", cboParamDataType, False, True)
            PopulateBOProperty(Me.State.ParametersGrid_TemplateParamsBO, "ParamDataTypeDescription", cboParamDataType, False, False)
            PopulateBOProperty(Me.State.ParametersGrid_TemplateParamsBO, "DateFormatString", txtDateFormatString)
            PopulateBOProperty(Me.State.ParametersGrid_TemplateParamsBO, "AllowEmptyValueXcd", cboAllowEmptyValue, False, True)
            PopulateBOProperty(Me.State.ParametersGrid_TemplateParamsBO, "AllowEmptyValueDescription", cboAllowEmptyValue, False, False)

            If Me.ErrCollection.Count > 0 Then
                Throw New PopulateBOErrorException
            End If
        End Sub

        Private Sub ParametersGrid_btnNew_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNewParameter_WRITE.Click
            Try
                If Not TheTemplate.Id.Equals(Guid.Empty) Then
                    Me.State.ParametersGrid_IsInEditMode = True
                    Me.State.ParametersGrid_DV = Nothing
                    ParametersGrid_AddNew()
                    ParametersGrid_SetButtonsState(False)
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub ParametersGrid_Cancel()
            Try
                SetGridControls(Me.ParametersGrid, True)

                If Me.State.ParametersGrid_RecordNew Then
                    TheTemplate.RemoveParametersChild(Me.State.ParametersGrid_TemplateParamsId)
                    Me.State.ParametersGrid_DV = Nothing
                    Me.State.ParametersGrid_RecordNew = False
                Else
                    Me.State.ParametersGrid_RecordEdit = False
                End If

                ParametersGrid_ReturnFromEditing()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub ParametersGrid_Save()
            Try
                ParametersGrid_BindBoPropertiesToHeaders()
                ParametersGrid_PopulateBOFromForm()

                If (Me.State.ParametersGrid_TemplateParamsBO.IsDirty) Then
                    Try
                        Me.State.ParametersGrid_TemplateParamsBO.Save()
                    Catch ex As Exception
                        If Not Me.State.ParametersGrid_RecordNew Then
                            Me.State.ParametersGrid_TemplateParamsBO.RejectChanges()
                        End If

                        Throw
                    End Try

                    Me.State.ParametersGrid_IsAfterSave = True
                    Me.State.ParametersGrid_TemplateParamsBO.EndEdit()
                    Me.State.ParametersGrid_DV = Nothing
                    Me.State.ParametersGrid_RecordNew = False
                    Me.State.ParametersGrid_RecordEdit = False
                    Me.ParametersGrid_ReturnFromEditing()
                Else
                    Me.AddInfoMsg(Me.MSG_RECORD_NOT_SAVED)
                    Me.ParametersGrid_ReturnFromEditing()
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub ParametersGrid_Delete()
            Dim templateParamsBO = TheTemplate.GetParameterChild(Me.State.ParametersGrid_TemplateParamsId)

            Try
                templateParamsBO.Delete()
                templateParamsBO.Save()
                templateParamsBO.EndEdit()
                templateParamsBO = Nothing
                Me.State.ParametersGrid_DV = Nothing
            Catch ex As Exception
                TheTemplate.RejectChanges()
                Throw ex
            End Try

            ParametersGrid_ReturnFromEditing()
        End Sub

        Protected Sub ParametersGrid_CheckIfComingFromDeleteConfirm()
            Dim confResponse As String = Me.HiddenDeletePromptResponse.Value
            If Not confResponse Is Nothing AndAlso confResponse = MSG_VALUE_YES Then
                If Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Delete Then
                    ParametersGrid_Delete()
                    'Clean after consuming the action
                    Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
                    Me.HiddenDeletePromptResponse.Value = ""
                End If
            ElseIf Not confResponse Is Nothing AndAlso confResponse = MSG_VALUE_NO Then
                ParametersGrid_ReturnFromEditing()
                'Clean after consuming the action
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
                Me.HiddenDeletePromptResponse.Value = ""
            End If
        End Sub
#End Region

#Region "Recipients Grid"
        Public Sub RecipientsGrid_RowCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)
            Dim index As Integer

            Try
                If e.CommandName = EDIT_COMMAND_NAME Then
                    index = CInt(e.CommandArgument)
                    RecipientsGrid.EditIndex = index
                    RecipientsGrid.SelectedIndex = index

                    Me.State.RecipientsGrid_IsInEditMode = True
                    Me.State.RecipientsGrid_TemplateRecipientId = New Guid(CType(Me.RecipientsGrid.Rows(index).Cells(GRID_RECIPIENT_COL_OC_TEMPLATE_RECIPIENT_ID).FindControl(ID_CONTROL_NAME), Label).Text)

                    Me.State.RecipientsGrid_TemplateRecipientBO = TheTemplate.GetRecipientChild(Me.State.RecipientsGrid_TemplateRecipientId)

                    RecipientsGrid_Populate()

                    'Disable all Edit and Delete icon buttons on the Grid
                    SetGridControls(Me.RecipientsGrid, False)
                    Me.State.RecipientsGrid_PageIndex = RecipientsGrid.PageIndex

                    Me.State.RecipientsGrid_RecordEdit = True

                    RecipientsGrid_PopulateFormFromBO(index)

                    RecipientsGrid_SetFocusOnEditableField(Me.RecipientsGrid, GRID_RECIPIENT_COL_RECIPIENT_ADDRESS, GRID_RECIPIENT_COL_RECIPIENT_ADDRESS_TEXTBOX_CONTROL_NAME, index)
                    RecipientsGrid_SetButtonsState(False)
                ElseIf (e.CommandName = DELETE_COMMAND) Then
                    index = CInt(e.CommandArgument)

                    Me.RecipientsGrid_Populate()
                    Me.State.RecipientsGrid_PageIndex = RecipientsGrid.PageIndex

                    'Clear the SelectedItemStyle to remove the highlight from the previously saved row
                    RecipientsGrid.SelectedIndex = NO_ROW_SELECTED_INDEX
                    'Save the Id in the Session
                    Me.State.RecipientsGrid_TemplateRecipientId = New Guid(CType(Me.RecipientsGrid.Rows(index).Cells(GRID_RECIPIENT_COL_OC_TEMPLATE_RECIPIENT_ID).FindControl(ID_CONTROL_NAME), Label).Text)
                    Me.DisplayMessage(Message.DELETE_RECORD_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, Me.HiddenDeletePromptResponse)
                    Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Delete
                    Me.State.RecipientsGrid_RecordDelete = True
                ElseIf (e.CommandName = SAVE_COMMAND) Then
                    RecipientsGrid_Save()
                ElseIf (e.CommandName = CANCEL_COMMAND) Then
                    RecipientsGrid_Cancel()
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Public Sub RecipientsGrid_RowCreated(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
            BaseItemCreated(sender, e)
        End Sub

        Private Sub RecipientsGrid__PageIndexChanging(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles RecipientsGrid.PageIndexChanging
            Try
                If (Not (Me.State.RecipientsGrid_IsInEditMode)) Then
                    Me.State.RecipientsGrid_PageIndex = e.NewPageIndex
                    Me.RecipientsGrid.PageIndex = Me.State.RecipientsGrid_PageIndex
                    Me.RecipientsGrid_Populate()
                    Me.RecipientsGrid.SelectedIndex = NO_ITEM_SELECTED_INDEX
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub RecipientsGrid_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles RecipientsGrid.RowDataBound
            Try
                Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
                Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)

                If Not dvRow Is Nothing And Me.State.RecipientsGrid_DV.Count > 0 Then
                    If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Or itemType = ListItemType.EditItem Then
                        CType(e.Row.Cells(GRID_RECIPIENT_COL_OC_TEMPLATE_RECIPIENT_ID).FindControl(Me.ID_CONTROL_NAME), Label).Text = GetGuidStringFromByteArray(CType(dvRow(OcTemplateRecipient.TemplateRecipientsDV.COL_OC_TEMPLATE_RECIPIENT_ID), Byte()))

                        If (Me.State.RecipientsGrid_IsInEditMode = True AndAlso Me.State.RecipientsGrid_TemplateRecipientId.ToString.Equals(GetGuidStringFromByteArray(CType(dvRow(OcTemplateRecipient.TemplateRecipientsDV.COL_OC_TEMPLATE_RECIPIENT_ID), Byte())))) Then
                            CType(e.Row.Cells(GRID_RECIPIENT_COL_RECIPIENT_ADDRESS).FindControl(GRID_RECIPIENT_COL_RECIPIENT_ADDRESS_TEXTBOX_CONTROL_NAME), TextBox).Text = dvRow(OcTemplateRecipient.TemplateRecipientsDV.COL_RECIPIENT_ADDRESS).ToString
                            CType(e.Row.Cells(GRID_RECIPIENT_COL_DESCRIPTION).FindControl(GRID_RECIPIENT_COL_DESCRIPTION_TEXTBOX_CONTROL_NAME), TextBox).Text = dvRow(OcTemplateRecipient.TemplateRecipientsDV.COL_DESCRIPTION).ToString

                            Me.BindCodeToListControl(CType(e.Row.Cells(Me.GRID_RECIPIENT_COL_RECIPIENT_SOURCE_FIELD_XCD).FindControl(Me.GRID_RECIPIENT_COL_RECIPIENT_SOURCE_FIELD_XCD_DROPDOWNLIST_CONTROL_NAME), DropDownList),
                                                       LookupListNew.GetRecipientSourceFieldLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), , , True)
                        Else
                            CType(e.Row.Cells(GRID_RECIPIENT_COL_RECIPIENT_ADDRESS).FindControl(GRID_RECIPIENT_COL_RECIPIENT_ADDRESS_LABEL_CONTROL_NAME), Label).Text = dvRow(OcTemplateRecipient.TemplateRecipientsDV.COL_RECIPIENT_ADDRESS).ToString
                            CType(e.Row.Cells(GRID_RECIPIENT_COL_DESCRIPTION).FindControl(GRID_RECIPIENT_COL_DESCRIPTION_LABEL_CONTROL_NAME), Label).Text = dvRow(OcTemplateRecipient.TemplateRecipientsDV.COL_DESCRIPTION).ToString
                            CType(e.Row.Cells(GRID_RECIPIENT_COL_RECIPIENT_SOURCE_FIELD_XCD).FindControl(GRID_RECIPIENT_COL_RECIPIENT_SOURCE_FIELD_XCD_LABEL_CONTROL_NAME), Label).Text = dvRow(OcTemplateRecipient.TemplateRecipientsDV.COL_RECIPIENT_SOURCE_FIELD_DESCRIPTION).ToString
                        End If
                    End If
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub RecipientsGrid_Populate()
            Try
                With TheTemplate
                    If Not .Id.Equals(Guid.Empty) Then
                        If Me.State.RecipientsGrid_DV Is Nothing Then
                            Me.State.RecipientsGrid_DV = RecipientsGrid_GetDV()
                        End If
                    End If
                End With

                If Not Me.State.RecipientsGrid_DV Is Nothing Then
                    Dim dv As OcTemplateRecipient.TemplateRecipientsDV

                    If Me.State.RecipientsGrid_DV.Count = 0 Then
                        dv = Me.State.RecipientsGrid_DV.AddNewRowToEmptyDV
                        SetPageAndSelectedIndexFromGuid(dv, Me.State.RecipientsGrid_TemplateRecipientId, Me.RecipientsGrid, Me.State.RecipientsGrid_PageIndex)
                        Me.RecipientsGrid.DataSource = dv
                    Else
                        SetPageAndSelectedIndexFromGuid(Me.State.RecipientsGrid_DV, Me.State.RecipientsGrid_TemplateRecipientId, Me.RecipientsGrid, Me.State.RecipientsGrid_PageIndex)
                        Me.RecipientsGrid.DataSource = Me.State.RecipientsGrid_DV
                    End If

                    Me.State.RecipientsGrid_DV.Sort = Me.State.RecipientsGrid_SortExpression

                    If (Me.State.RecipientsGrid_IsAfterSave) Then
                        Me.State.RecipientsGrid_IsAfterSave = False
                        Me.SetPageAndSelectedIndexFromGuid(Me.State.RecipientsGrid_DV, Me.State.RecipientsGrid_TemplateRecipientId, Me.RecipientsGrid, Me.RecipientsGrid.PageIndex)
                    ElseIf (Me.State.RecipientsGrid_IsInEditMode) Then
                        Me.SetPageAndSelectedIndexFromGuid(Me.State.RecipientsGrid_DV, Me.State.RecipientsGrid_TemplateRecipientId, Me.RecipientsGrid, Me.RecipientsGrid.PageIndex, Me.State.RecipientsGrid_IsInEditMode)
                    Else
                        'In a Delete scenario...
                        Me.SetPageAndSelectedIndexFromGuid(Me.State.RecipientsGrid_DV, Guid.Empty, Me.RecipientsGrid, Me.RecipientsGrid.PageIndex, Me.State.RecipientsGrid_IsInEditMode)
                    End If

                    Me.RecipientsGrid.AutoGenerateColumns = False

                    If Me.State.RecipientsGrid_DV.Count = 0 Then
                        RecipientsGrid_SortAndBind(dv)
                    Else
                        RecipientsGrid_SortAndBind(Me.State.RecipientsGrid_DV)
                    End If

                    If Me.State.RecipientsGrid_DV.Count = 0 Then
                        For Each gvRow As GridViewRow In Me.RecipientsGrid.Rows
                            gvRow.Visible = False
                            gvRow.Controls.Clear()
                        Next
                    End If
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub RecipientsGrid_BindBoPropertiesToHeaders()
            If Not Me.State.RecipientsGrid_TemplateRecipientBO Is Nothing Then
                Me.BindBOPropertyToGridHeader(Me.State.RecipientsGrid_TemplateRecipientBO, "RecipientAddress", Me.RecipientsGrid.Columns(Me.GRID_RECIPIENT_COL_RECIPIENT_ADDRESS))
                Me.BindBOPropertyToGridHeader(Me.State.RecipientsGrid_TemplateRecipientBO, "Description", Me.RecipientsGrid.Columns(Me.GRID_RECIPIENT_COL_DESCRIPTION))
                Me.BindBOPropertyToGridHeader(Me.State.RecipientsGrid_TemplateRecipientBO, "RecipientSourceFieldXcd", Me.RecipientsGrid.Columns(Me.GRID_RECIPIENT_COL_RECIPIENT_SOURCE_FIELD_XCD))
            End If
            Me.ClearGridViewHeadersAndLabelsErrSign()
        End Sub

        Private Sub RecipientsGrid_SetFocusOnEditableField(ByVal grid As GridView, ByVal cellPosition As Integer, ByVal controlName As String, ByVal itemIndex As Integer)
            Dim control As TextBox = CType(grid.Rows(itemIndex).Cells(cellPosition).FindControl(controlName), TextBox)
            SetFocus(control)
        End Sub

        Private Sub RecipientsGrid_PopulateFormFromBO(Optional ByVal gridRowIdx As Integer? = Nothing)
            If IsNothing(gridRowIdx) Then gridRowIdx = Me.RecipientsGrid.EditIndex

            Dim cboRecipientSourceField As DropDownList = CType(Me.RecipientsGrid.Rows(gridRowIdx).Cells(GRID_RECIPIENT_COL_RECIPIENT_SOURCE_FIELD_XCD).FindControl(GRID_RECIPIENT_COL_RECIPIENT_SOURCE_FIELD_XCD_DROPDOWNLIST_CONTROL_NAME), DropDownList)
            Me.BindCodeToListControl(cboRecipientSourceField, LookupListNew.GetRecipientSourceFieldLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), , , True)

            Try
                With Me.State.RecipientsGrid_TemplateRecipientBO
                    If (Not .Id.Equals(Guid.Empty)) AndAlso Not .IsNew Then
                        If Not String.IsNullOrEmpty(.RecipientSourceFieldXcd) Then
                            SetSelectedItem(cboRecipientSourceField, .RecipientSourceFieldXcd)
                        End If
                    End If

                    Dim txtRecipientAddress As TextBox = CType(Me.RecipientsGrid.Rows(gridRowIdx).Cells(GRID_RECIPIENT_COL_RECIPIENT_ADDRESS).FindControl(GRID_RECIPIENT_COL_RECIPIENT_ADDRESS_TEXTBOX_CONTROL_NAME), TextBox)
                    Me.PopulateControlFromBOProperty(txtRecipientAddress, .RecipientAddress)

                    Dim txtDescription As TextBox = CType(Me.RecipientsGrid.Rows(gridRowIdx).Cells(GRID_RECIPIENT_COL_DESCRIPTION).FindControl(GRID_RECIPIENT_COL_DESCRIPTION_TEXTBOX_CONTROL_NAME), TextBox)
                    Me.PopulateControlFromBOProperty(txtDescription, .Description)

                    CType(Me.RecipientsGrid.Rows(gridRowIdx).Cells(GRID_RECIPIENT_COL_OC_TEMPLATE_RECIPIENT_ID).FindControl(ID_CONTROL_NAME), Label).Text = .Id.ToString
                End With
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub RecipientsGrid_SetButtonsState(ByVal bIsEdit As Boolean)
            If bIsEdit Then
                If Me.State.TemplateId <> Guid.Empty Then
                    If Me.State.Template.HasCustomizedParamsXcd = "YESNO-Y" Then
                        ControlMgr.SetEnableControl(Me, btnNewParameter_WRITE, bIsEdit)
                    End If
                    ControlMgr.SetEnableControl(Me, btnNewRecipient_WRITE, bIsEdit)
                End If
                Else
                ControlMgr.SetEnableControl(Me, btnNewParameter_WRITE, bIsEdit)
                ControlMgr.SetEnableControl(Me, btnNewRecipient_WRITE, bIsEdit)
            End If

            ControlMgr.SetEnableControl(Me, btnApply_WRITE, bIsEdit)
            ControlMgr.SetEnableControl(Me, btnBack, bIsEdit)
        End Sub

        Private Function RecipientsGrid_GetDV() As OcTemplateRecipient.TemplateRecipientsDV
            Dim dv As OcTemplateRecipient.TemplateRecipientsDV
            dv = RecipientsGrid_GetDataView()
            dv.Sort = Me.State.RecipientsGrid_SortExpression
            Me.RecipientsGrid.DataSource = dv
            Return (dv)
        End Function

        Private Function RecipientsGrid_GetDataView() As OcTemplateRecipient.TemplateRecipientsDV
            Dim dt As DataTable = TheTemplate.RecipientsList.Table
            Return New OcTemplateRecipient.TemplateRecipientsDV(dt)
        End Function

        Private Sub RecipientsGrid_SortAndBind(ByVal dvBinding As DataView, Optional ByVal blnEmptyList As Boolean = False)
            Me.RecipientsGrid.DataSource = dvBinding
            HighLightSortColumn(Me.RecipientsGrid, Me.State.RecipientsGrid_SortExpression)
            Me.RecipientsGrid.DataBind()

            If Not Me.RecipientsGrid.BottomPagerRow.Visible Then Me.RecipientsGrid.BottomPagerRow.Visible = True

            If blnEmptyList Then
                For Each gvRow As GridViewRow In Me.RecipientsGrid.Rows
                    gvRow.Controls.Clear()
                Next
            End If

            Session("recCount") = Me.State.RecipientsGrid_DV.Count

            ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me, Me.RecipientsGrid)
        End Sub

        Private Sub RecipientsGrid_AddNew()
            Me.State.RecipientsGrid_TemplateRecipientBO = TheTemplate.GetNewRecipientChild
            Me.State.RecipientsGrid_DV = RecipientsGrid_GetDV()
            Me.State.RecipientsGrid_TemplateRecipientId = Me.State.RecipientsGrid_TemplateRecipientBO.Id
            Me.RecipientsGrid.DataSource = Me.State.RecipientsGrid_DV
            Me.SetPageAndSelectedIndexFromGuid(Me.State.RecipientsGrid_DV, Me.State.RecipientsGrid_TemplateRecipientId, Me.RecipientsGrid, Me.State.RecipientsGrid_PageIndex, Me.State.RecipientsGrid_IsInEditMode)
            Me.RecipientsGrid.AutoGenerateColumns = False
            RecipientsGrid_SortAndBind(Me.State.RecipientsGrid_DV)
            SetGridControls(Me.RecipientsGrid, False)
            Me.State.RecipientsGrid_RecordNew = True
            RecipientsGrid_PopulateFormFromBO()
        End Sub

        Private Sub RecipientsGrid_ReturnFromEditing()
            Me.RecipientsGrid.EditIndex = NO_ROW_SELECTED_INDEX

            If Me.RecipientsGrid.PageCount = 0 Then
                'if returning to the "1st time in" screen
                ControlMgr.SetVisibleControl(Me, Me.RecipientsGrid, False)
            Else
                ControlMgr.SetVisibleControl(Me, Me.RecipientsGrid, True)
            End If

            SetGridControls(Me.RecipientsGrid, True)
            Me.State.RecipientsGrid_IsInEditMode = False
            Me.State.RecipientsGrid_TemplateRecipientId = Guid.Empty
            Me.State.RecipientsGrid_TemplateRecipientBO = Nothing
            Me.RecipientsGrid_Populate()
            Me.State.RecipientsGrid_PageIndex = Me.RecipientsGrid.PageIndex
            RecipientsGrid_SetButtonsState(True)
        End Sub

        Private Sub RecipientsGrid_PopulateBOFromForm()
            Dim txtRecipientAddress As TextBox = CType(Me.RecipientsGrid.Rows(Me.RecipientsGrid.EditIndex).Cells(GRID_RECIPIENT_COL_RECIPIENT_ADDRESS).FindControl(Me.GRID_RECIPIENT_COL_RECIPIENT_ADDRESS_TEXTBOX_CONTROL_NAME), TextBox)
            Dim txtDescription As TextBox = CType(Me.RecipientsGrid.Rows(Me.RecipientsGrid.EditIndex).Cells(GRID_RECIPIENT_COL_DESCRIPTION).FindControl(Me.GRID_RECIPIENT_COL_DESCRIPTION_TEXTBOX_CONTROL_NAME), TextBox)
            Dim cboRecipientSourceField As DropDownList = CType(Me.RecipientsGrid.Rows(Me.RecipientsGrid.EditIndex).Cells(GRID_RECIPIENT_COL_RECIPIENT_SOURCE_FIELD_XCD).FindControl(Me.GRID_RECIPIENT_COL_RECIPIENT_SOURCE_FIELD_XCD_DROPDOWNLIST_CONTROL_NAME), DropDownList)

            PopulateBOProperty(Me.State.RecipientsGrid_TemplateRecipientBO, "OcTemplateId", TheTemplate.Id)
            PopulateBOProperty(Me.State.RecipientsGrid_TemplateRecipientBO, "RecipientAddress", txtRecipientAddress)
            PopulateBOProperty(Me.State.RecipientsGrid_TemplateRecipientBO, "Description", txtDescription)
            PopulateBOProperty(Me.State.RecipientsGrid_TemplateRecipientBO, "RecipientSourceFieldXcd", cboRecipientSourceField, False, True)
            PopulateBOProperty(Me.State.RecipientsGrid_TemplateRecipientBO, "RecipientSourceFieldDescription", cboRecipientSourceField, False, False)

            If Me.ErrCollection.Count > 0 Then
                Throw New PopulateBOErrorException
            End If
        End Sub

        Private Sub RecipientsGrid_btnNew_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNewRecipient_WRITE.Click
            Try
                If Not TheTemplate.Id.Equals(Guid.Empty) Then
                    Me.State.RecipientsGrid_IsInEditMode = True
                    Me.State.RecipientsGrid_DV = Nothing
                    RecipientsGrid_AddNew()
                    RecipientsGrid_SetButtonsState(False)
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub RecipientsGrid_Cancel()
            Try
                SetGridControls(Me.RecipientsGrid, True)

                If Me.State.RecipientsGrid_RecordNew Then
                    TheTemplate.RemoveRecipientsChild(Me.State.RecipientsGrid_TemplateRecipientId)
                    Me.State.RecipientsGrid_DV = Nothing
                    Me.State.RecipientsGrid_RecordNew = False
                Else
                    Me.State.RecipientsGrid_RecordEdit = False
                End If

                RecipientsGrid_ReturnFromEditing()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub RecipientsGrid_Save()
            Try
                RecipientsGrid_BindBoPropertiesToHeaders()
                RecipientsGrid_PopulateBOFromForm()

                If (Me.State.RecipientsGrid_TemplateRecipientBO.IsDirty) Then
                    Try
                        Me.State.RecipientsGrid_TemplateRecipientBO.Save()
                    Catch ex As Exception
                        If Not Me.State.RecipientsGrid_RecordNew Then
                            Me.State.RecipientsGrid_TemplateRecipientBO.RejectChanges()
                        End If

                        Throw
                    End Try

                    Me.State.RecipientsGrid_IsAfterSave = True
                    Me.State.RecipientsGrid_TemplateRecipientBO.EndEdit()
                    Me.State.RecipientsGrid_DV = Nothing
                    Me.State.RecipientsGrid_RecordNew = False
                    Me.State.RecipientsGrid_RecordEdit = False
                    Me.RecipientsGrid_ReturnFromEditing()
                Else
                    Me.AddInfoMsg(Me.MSG_RECORD_NOT_SAVED)
                    Me.RecipientsGrid_ReturnFromEditing()
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub RecipientsGrid_Delete()
            Dim templateRecipientBO = TheTemplate.GetRecipientChild(Me.State.RecipientsGrid_TemplateRecipientId)

            Try
                templateRecipientBO.Delete()
                templateRecipientBO.Save()
                templateRecipientBO.EndEdit()
                templateRecipientBO = Nothing
                Me.State.RecipientsGrid_DV = Nothing
            Catch ex As Exception
                TheTemplate.RejectChanges()
                Throw ex
            End Try

            RecipientsGrid_ReturnFromEditing()
        End Sub

        Protected Sub RecipientsGrid_CheckIfComingFromDeleteConfirm()
            Dim confResponse As String = Me.HiddenDeletePromptResponse.Value
            If Not confResponse Is Nothing AndAlso confResponse = MSG_VALUE_YES Then
                If Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Delete Then
                    RecipientsGrid_Delete()
                    'Clean after consuming the action
                    Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
                    Me.HiddenDeletePromptResponse.Value = ""
                End If
            ElseIf Not confResponse Is Nothing AndAlso confResponse = MSG_VALUE_NO Then
                RecipientsGrid_ReturnFromEditing()
                'Clean after consuming the action
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
                Me.HiddenDeletePromptResponse.Value = ""
            End If
        End Sub
#End Region
#End Region
#End Region
    End Class
End Namespace
