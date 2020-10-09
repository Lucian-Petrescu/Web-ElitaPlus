Imports System.Collections.Generic
Imports System.Threading
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.Web.Forms
Imports Assurant.ElitaPlus.DALObjects
Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Imports Assurant.ElitaPlus.Security
Imports Microsoft.VisualBasic

Namespace Tables
    Partial Class TemplateForm
        Inherits ElitaPlusSearchPage

#Region "Page State"

#Region "MyState"
        Class MyState
            Public TemplateGroupId As Guid = Guid.Empty

            Public TemplateId As Guid = Guid.Empty
            Public IsTemplateNew As Boolean = False
            Public IsNewWithCopy As Boolean = False
            Public IsUndo As Boolean = False
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
            'Me.State.TemplateId = CType(Me.CallingParameters, Guid)
            If State.TemplateId.Equals(Guid.Empty) Then
                State.IsTemplateNew = True
                BindBoPropertiesToLabels()
                AddLabelDecorations(TheTemplate)
                ClearAll()
                PopulateAll()
            Else
                State.IsTemplateNew = False
                BindBoPropertiesToLabels()
                AddLabelDecorations(TheTemplate)
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

        Public Const URL As String = "TemplateForm.aspx"

        Public Const TEMPLATECODE_PROPERTY As String = "TemplateCode"
        Public Const DESCRIPTION_PROPERTY As String = "Description"
        Public Const HASCUSTOMIZEDPARAMSXCD_PROPERTY As String = "HasCustomizedParamsXcd"
        Public Const ALLOWMANUALUSEXCD_PROPERTY As String = "AllowManualUseXcd"
        Public Const ALLOWMANUALRESENDXCD_PROPERTY As String = "AllowManualResendXcd"
        Public Const EFFECTIVEDATE_PROPERTY As String = "EffectiveDate"
        Public Const EXPIRATIONDATE_PROPERTY As String = "ExpirationDate"

        Private Const ID_CONTROL_NAME As String = "IdLabel"

        Private Const GRID_PARAM_COL_OC_TEMPLATE_PARAMS_ID As Integer = 0

        Private Const GRID_PARAM_COL_PARAM_NAME As Integer = 1
        Private Const GRID_PARAM_COL_PARAM_NAME_LABEL_CONTROL_NAME As String = "lblParamName"
        Private Const GRID_PARAM_COL_PARAM_NAME_TEXTBOX_CONTROL_NAME As String = "txtParamName"

        Private Const GRID_PARAM_COL_PARAM_VALUE_SOURCE_XCD As Integer = 2
        Private Const GRID_PARAM_COL_PARAM_VALUE_SOURCE_XCD_LABEL_CONTROL_NAME As String = "lblParamValueSource"
        Private Const GRID_PARAM_COL_PARAM_VALUE_SOURCE_XCD_DROPDOWNLIST_CONTROL_NAME As String = "cboParamValueSource"

        Private Const GRID_PARAM_COL_PARAM_DATA_TYPE_XCD As Integer = 3
        Private Const GRID_PARAM_COL_PARAM_DATA_TYPE_XCD_LABEL_CONTROL_NAME As String = "lblParamDataType"
        Private Const GRID_PARAM_COL_PARAM_DATA_TYPE_XCD_DROPDOWNLIST_CONTROL_NAME As String = "cboParamDataType"

        Private Const GRID_PARAM_COL_DATE_FORMAT_STRING As Integer = 4
        Private Const GRID_PARAM_COL_DATE_FORMAT_STRING_LABEL_CONTROL_NAME As String = "lblDateFormatString"
        Private Const GRID_PARAM_COL_DATE_FORMAT_STRING_TEXTBOX_CONTROL_NAME As String = "txtDateFormatString"

        Private Const GRID_PARAM_COL_ALLOW_EMPTY_VALUE_XCD As Integer = 5
        Private Const GRID_PARAM_COL_ALLOW_EMPTY_VALUE_XCD_LABEL_CONTROL_NAME As String = "lblAllowEmptyValue"
        Private Const GRID_PARAM_COL_ALLOW_EMPTY_VALUE_XCD_DROPDOWNLIST_CONTROL_NAME As String = "cboAllowEmptyValue"

        Private Const GRID_RECIPIENT_COL_OC_TEMPLATE_RECIPIENT_ID As Integer = 0

        Private Const GRID_RECIPIENT_COL_RECIPIENT_SOURCE_FIELD_XCD As Integer = 1
        Private Const GRID_RECIPIENT_COL_RECIPIENT_SOURCE_FIELD_XCD_LABEL_CONTROL_NAME As String = "lblRecipientSourceField"
        Private Const GRID_RECIPIENT_COL_RECIPIENT_SOURCE_FIELD_XCD_DROPDOWNLIST_CONTROL_NAME As String = "cboRecipientSourceField"

        Private Const GRID_RECIPIENT_COL_DESCRIPTION As Integer = 2
        Private Const GRID_RECIPIENT_COL_DESCRIPTION_LABEL_CONTROL_NAME As String = "lblDescription"
        Private Const GRID_RECIPIENT_COL_DESCRIPTION_TEXTBOX_CONTROL_NAME As String = "txtDescription"
#End Region

#Region "Page Call Type"
        Public Class CallType
            Public TemplateGroupId As Guid
            Public TemplateId As Guid
            Public Sub New(templateId As Guid, templateGroupId As Guid)
                Me.TemplateGroupId = templateGroupId
                Me.TemplateId = templateId
            End Sub
        End Class
#End Region

#Region "Page Return Type"
        Public Class ReturnType
            Public LastOperation As DetailPageCommand
            Public TemplateId As Guid
            Public HasDataChanged As Boolean
            Public Sub New(LastOp As DetailPageCommand, templateId As Guid, hasDataChanged As Boolean)
                LastOperation = LastOp
                Me.TemplateId = templateId
                Me.HasDataChanged = hasDataChanged
            End Sub
        End Class
#End Region

#Region "Properties"
        Private ReadOnly Property TheTemplate As OcTemplate
            Get
                If State.Template Is Nothing Then
                    If State.IsTemplateNew = True Then
                        ' For creating, inserting
                        State.Template = New OcTemplate
                        State.TemplateId = State.Template.Id
                    Else
                        ' For updating, deleting
                        State.Template = New OcTemplate(State.TemplateId)
                    End If
                End If

                Return State.Template
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
                    AddCalendar_New(btnEffectiveDate, txtEffectiveDate)
                    AddCalendar_New(btnExpirationDate, txtExpirationDate)

                    MasterPage.MessageController.Clear()
                    MasterPage.UsePageTabTitleInBreadCrum = False
                    MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("Tables")

                    TranslateGridHeader(ParametersGrid)
                    TranslateGridHeader(RecipientsGrid)
                    UpdateBreadCrum()

                    SetStateProperties()
                    AddControlMsg(btnDelete_WRITE, Message.DELETE_RECORD_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, True)

                    If State.IsTemplateNew = True Then
                        CreateNew()
                    End If
                Else
                    If State.ParametersGrid_RecordDelete Then
                        ParametersGrid_CheckIfComingFromDeleteConfirm()
                        State.ParametersGrid_RecordDelete = False
                    ElseIf State.RecipientsGrid_RecordDelete Then
                        RecipientsGrid_CheckIfComingFromDeleteConfirm()
                        State.RecipientsGrid_RecordDelete = False
                    End If
                End If

                BindBoPropertiesToLabels()
                ParametersGrid_BindBoPropertiesToHeaders()
                RecipientsGrid_BindBoPropertiesToHeaders()
                CheckIfComingFromConfirm()

                If Not IsPostBack Then
                    AddLabelDecorations(TheTemplate)
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
                Dim callObj As TemplateForm.CallType = CType(CallingPar, TemplateForm.CallType)

                If callObj Is Nothing Then
                    Throw New ArgumentNullException()
                End If

                If callObj.TemplateGroupId <> Guid.Empty Then
                    State.TemplateGroupId = callObj.TemplateGroupId
                Else
                    Throw New ArgumentException()
                End If

                If callObj.TemplateId <> Guid.Empty Then
                    'Get the id from the parent
                    State.TemplateId = callObj.TemplateId
                Else
                    State.IsTemplateNew = True
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
        Private Sub btnApply_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnApply_WRITE.Click
            ApplyChanges()
        End Sub

        Private Sub GoBack()
            Dim retType As New TemplateForm.ReturnType(ElitaPlusPage.DetailPageCommand.Back, State.TemplateId, State.boChanged)
            ReturnToCallingPage(retType)
        End Sub

        Private Sub btnBack_Click(sender As System.Object, e As System.EventArgs) Handles btnBack.Click
            Try
                If IsDirtyBO() = True Then
                    DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                    State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
                Else
                    GoBack()
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnUndo_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnUndo_WRITE.Click
            Try
                If Not State.IsTemplateNew Then
                    'Reload from the DB
                    State.Template = New OcTemplate(State.TemplateId)
                ElseIf State.ScreenSnapShotBO IsNot Nothing Then
                    'It was a new with copy
                    State.Template.Clone(State.ScreenSnapShotBO)
                Else
                    State.Template = New OcTemplate
                End If

                PopulateAll()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub CreateNew()
            State.ScreenSnapShotBO = Nothing
            State.TemplateId = Guid.Empty
            State.IsTemplateNew = True
            State.Template = New OcTemplate
            State.ParametersGrid_DV = Nothing
            State.RecipientsGrid_DV = Nothing
            ClearAll()
            SetButtonsState(True)
            DisabledTabsList.Clear()
            PopulateAll()

            If State.IsTemplateNew Then
                ControlMgr.SetEnableControl(Me, btnDelete_WRITE, False)
                ControlMgr.SetEnableControl(Me, btnNew_WRITE, False)
                ControlMgr.SetEnableControl(Me, btnCopy_WRITE, False)
            End If
        End Sub

        Private Sub btnNew_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnNew_WRITE.Click
            Try
                If IsDirtyBO() = True Then
                    DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                    State.ActionInProgress = ElitaPlusPage.DetailPageCommand.New_
                Else
                    CreateNew()
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub CreateNewCopy()
            PopulateBOsFromForm()

            Dim newObj As New OcTemplate
            newObj.Copy(TheTemplate)

            State.Template = newObj
            State.TemplateId = Guid.Empty
            State.IsTemplateNew = True

            With TheTemplate
                .TemplateCode = Nothing
                .Description = Nothing
                .HasCustomizedParamsXcd = Nothing
                .AllowManualUseXcd = Nothing
                .AllowManualResendXcd = Nothing
                .EffectiveDate = Nothing
                .ExpirationDate = Nothing
            End With

            State.ParametersGrid_DV = Nothing
            State.RecipientsGrid_DV = Nothing
            ParametersGrid_Populate()
            RecipientsGrid_Populate()

            SetButtonsState(True)
            DisabledTabsList.Clear()

            'create the backup copy
            State.ScreenSnapShotBO = New OcTemplate
            State.ScreenSnapShotBO.Copy(TheTemplate)
        End Sub

        Private Sub btnCopy_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnCopy_WRITE.Click
            Try
                If IsDirtyBO() = True Then
                    DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                    State.ActionInProgress = ElitaPlusPage.DetailPageCommand.NewAndCopy
                Else
                    CreateNewCopy()
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
                DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.NewAndCopy
            End Try
        End Sub

        Private Sub btnDelete_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnDelete_WRITE.Click
            Try
                If DeleteTemplate() = True Then
                    State.boChanged = True
                    Dim retType As New TemplateForm.ReturnType(ElitaPlusPage.DetailPageCommand.Delete, State.TemplateId, True)
                    ReturnToCallingPage(retType)
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub
#End Region

#Region "Clear"
        Private Sub ClearTexts()
            txtTemplateCode.Text = Nothing
            txtTemplateDescription.Text = Nothing
            txtEffectiveDate.Text = Nothing
            txtExpirationDate.Text = Nothing           
        End Sub

        Private Sub ClearTextsSMS()
            txtSMSAppKey.Text = Nothing
            txtSMSShortCode.Text = Nothing
            txtSMSTriggerId.Text = Nothing
        End Sub

        Private Sub ClearAll()
            ClearTexts()
            ClearTextsSMS()
            ClearList(cboHasCustomizedParams)
            ClearList(cboAllowManualUse)
            ClearList(cboAllowManualResend)
            ClearList(ddlTemplateType)
        End Sub
#End Region

#Region "Populate"
        Private Sub UpdateBreadCrum()
            If (State IsNot Nothing) Then
                If (State IsNot Nothing) Then
                    MasterPage.BreadCrum = MasterPage.PageTab & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage("TEMPLATE_DETAIL")
                    MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("TEMPLATE_DETAIL")
                End If
            End If
        End Sub

        Private Sub PopulateDropDowns()
            'Dim oLanguageId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
            Try
                Dim oYesNoList As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="YESNO", languageCode:=Thread.CurrentPrincipal.GetLanguageCode())
                Dim populateOptions As PopulateOptions = New PopulateOptions() With
                    {
                        .AddBlankItem = True,
                        .ValueFunc = AddressOf .GetExtendedCode
                    }
                cboHasCustomizedParams.Populate(oYesNoList, populateOptions)
                cboAllowManualUse.Populate(oYesNoList, populateOptions)
                cboAllowManualResend.Populate(oYesNoList, populateOptions)

                'Me.BindCodeToListControl(cboHasCustomizedParams, LookupListNew.GetYesNoXcdList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), , , True)
                'Me.BindCodeToListControl(cboAllowManualUse, LookupListNew.GetYesNoXcdList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), , , True)
                'Me.BindCodeToListControl(cboAllowManualResend, LookupListNew.GetYesNoXcdList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), , , True)

                ddlTemplateType.Populate(CommonConfigManager.Current.ListManager.GetList(listCode:="OC_TEMP_TYPE", languageCode:=Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
                    {
                        .AddBlankItem = False,
                        .ValueFunc = AddressOf .GetExtendedCode
                    })

                ddlTemplateType.SelectedValue = "OC_TEMP_TYPE-EMAIL"
                
            Catch ex As Exception
                MasterPage.MessageController.AddError(TEMPLATE_FORM001)
                MasterPage.MessageController.AddError(ex.Message, False)
                MasterPage.MessageController.Show()
            End Try
        End Sub

        Private Sub PopulateTexts()
            Try
                Dim oLanguageId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId

                With TheTemplate
                    If State.IsTemplateNew = True Then
                        BindSelectItem(Nothing, cboHasCustomizedParams)
                        BindSelectItem(Nothing, cboAllowManualUse)
                        BindSelectItem(Nothing, cboAllowManualResend)
                        txtTemplateCode.Text = Nothing
                        txtTemplateDescription.Text = Nothing
                        txtEffectiveDate.Text = Nothing
                        txtExpirationDate.Text = Nothing
                        ClearTextsSMS
                    Else
                        If Not String.IsNullOrEmpty(.HasCustomizedParamsXcd) Then
                            SetSelectedItem(cboHasCustomizedParams, .HasCustomizedParamsXcd)
                        End If

                        If Not String.IsNullOrEmpty(.AllowManualUseXcd) Then
                            SetSelectedItem(cboAllowManualUse, .AllowManualUseXcd)
                        End If

                        If Not String.IsNullOrEmpty(.AllowManualUseXcd) Then
                            SetSelectedItem(cboAllowManualResend, .AllowManualResendXcd)
                        End If

                        PopulateControlFromBOProperty(txtTemplateCode, .TemplateCode)
                        PopulateControlFromBOProperty(txtTemplateDescription, .Description)
                        PopulateControlFromBOProperty(txtEffectiveDate, .EffectiveDate)
                        PopulateControlFromBOProperty(txtExpirationDate, .ExpirationDate)

                        SetSelectedItem(ddlTemplateType, .TemplateTypeXcd)
                        PopulateControlFromBOProperty(txtSMSAppKey, .SmsAppKey)
                        PopulateControlFromBOProperty(txtSMSShortCode, .SmsShortCode)
                        PopulateControlFromBOProperty(txtSMSTriggerId, .SmsTriggerId)
                    End If
                End With
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub PopulateAll()
            If State.IsTemplateNew = True Then
                PopulateDropDowns()
                ParametersGrid_Populate()
                RecipientsGrid_Populate()
            Else
                ClearAll()
                PopulateDropDowns()
                PopulateTexts()
                PopulateDateFields()
                ParametersGrid_Populate()
                RecipientsGrid_Populate()
            End If
        End Sub

        Protected Sub PopulateBOsFromForm()
            With TheTemplate
                PopulateBOProperty(TheTemplate, TEMPLATECODE_PROPERTY, txtTemplateCode)
                PopulateBOProperty(TheTemplate, DESCRIPTION_PROPERTY, txtTemplateDescription)

                PopulateBOProperty(TheTemplate, HASCUSTOMIZEDPARAMSXCD_PROPERTY, cboHasCustomizedParams.SelectedValue)
                PopulateBOProperty(TheTemplate, ALLOWMANUALUSEXCD_PROPERTY, cboAllowManualUse.SelectedValue)
                PopulateBOProperty(TheTemplate, ALLOWMANUALRESENDXCD_PROPERTY, cboAllowManualResend.SelectedValue)

                PopulateBOProperty(TheTemplate, EFFECTIVEDATE_PROPERTY, txtEffectiveDate)
                PopulateBOProperty(TheTemplate, EXPIRATIONDATE_PROPERTY, txtExpirationDate)

                PopulateBOProperty(TheTemplate, "TemplateTypeXcd", ddlTemplateType.SelectedValue)

                If(TheTemplate.TemplateTypeXcd <> "OC_TEMP_TYPE-SMS") Then
                    ClearTextsSMS 'clear the SMS configuration if template type is not SMS
                End If

                PopulateBOProperty(TheTemplate, "SmsAppKey", txtSMSAppKey)
                PopulateBOProperty(TheTemplate, "SmsShortCode", txtSMSShortCode)
                PopulateBOProperty(TheTemplate, "SmsTriggerId", txtSMSTriggerId)
                
            End With

            If ErrCollection.Count > 0 Then
                Throw New PopulateBOErrorException
            End If
        End Sub

        Public Sub PopulateDateFields()
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

#Region "Gui-Validation"
        Private Sub SetButtonsState(bIsNew As Boolean)
            ControlMgr.SetEnableControl(Me, btnNew_WRITE, Not bIsNew)
            ControlMgr.SetEnableControl(Me, btnCopy_WRITE, Not bIsNew)
            ControlMgr.SetEnableControl(Me, btnDelete_WRITE, Not bIsNew)
        End Sub
#End Region

#Region "Business Part"
        Private Function IsDirtyBO() As Boolean
            Dim bIsDirty As Boolean = True

            Try
                With TheTemplate
                    PopulateBOsFromForm()
                    bIsDirty = .IsDirty OrElse TheTemplate.IsChildrenDirty
                End With
            Catch ex As Exception
                MasterPage.MessageController.AddError(TEMPLATE_FORM001)
                MasterPage.MessageController.AddError(ex.Message, False)
                MasterPage.MessageController.Show()
            End Try
            Return bIsDirty
        End Function

        Private Function ApplyChanges() As Boolean
            Try
                PopulateBOsFromForm()
                Dim errors As List(Of ValidationError) = New List(Of ValidationError)

                If errors.Count > 0 Then
                    Throw New BOValidationException(errors.ToArray, GetType(OcTemplate).FullName)
                End If

                SetLabelColor(lblTemplateCode)
                SetLabelColor(lblTemplateDescription)
                SetLabelColor(lblHasCustomizedParams)
                SetLabelColor(lblAllowManualUse)
                SetLabelColor(lblAllowManualResend)
                SetLabelColor(lblEffectiveDate)
                SetLabelColor(lblExpirationDate)

                SetLabelColor(lblTemplateCode)
                SetLabelColor(lblSMSAppKey)
                SetLabelColor(lblSMSTriggerId)
                SetLabelColor(lblSMSShortCode)

                If TheTemplate.IsDirty OrElse TheTemplate.IsChildrenDirty Then
                    If State.IsTemplateNew = True Then
                        TheTemplate.OcTemplateGroupId = State.TemplateGroupId
                    End If

                    TheTemplate.Save()

                    'Save reloads the object from the DB, therefore the children collection references
                    'are cleared from the underlying data structure (row). So we need to reload the children collections and
                    'to achieve this, simply clear the corresponding DataViews to force a reload upon populating the grids.
                    State.ParametersGrid_DV = Nothing
                    State.RecipientsGrid_DV = Nothing

                    State.boChanged = True

                    If State.IsTemplateNew = True Then
                        State.IsTemplateNew = False
                        DisabledTabsList.Clear()
                    End If

                    PopulateAll()
                    SetButtonsState(State.IsTemplateNew)
                    MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION)
                Else
                    MasterPage.MessageController.AddInformation(Message.MSG_RECORD_NOT_SAVED)
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Function

        Private Function DeleteTemplate() As Boolean
            Dim bIsOk As Boolean = True

            Try
                With TheTemplate
                    PopulateBOsFromForm()

                    'Check if there are any elp_oc_message entities associated with this template.
                    If .GetAssociatedMessageCount(.Id) > 0 Then
                        MasterPage.MessageController.AddError(TranslationBase.TranslateLabelOrMessage(Assurant.ElitaPlus.Common.ErrorCodes.ERR_DELETE_TEMPLATE_WITH_MESSAGES_PRESENT, ElitaPlusIdentity.Current.ActiveUser.LanguageId))
                        .cancelEdit()
                        bIsOk = False
                    Else
                        'When children collections are loaded correctly, delete with delete the children first, then the object.
                        .Delete()
                        .Save()
                    End If
                End With
            Catch ex As Exception
                MasterPage.MessageController.AddError(TranslationBase.TranslateLabelOrMessage(TEMPLATE_FORM002, ElitaPlusIdentity.Current.ActiveUser.LanguageId))
                MasterPage.MessageController.AddError(ex.Message, False)
                MasterPage.MessageController.Show()
                bIsOk = False
            End Try
            Return bIsOk
        End Function
#End Region

#Region "State-Management"
        Protected Sub ComingFromBack()
            Dim confResponse As String = HiddenSaveChangesPromptResponse.Value

            If Not confResponse = String.Empty Then
                ' Return from the Back Button
                Select Case confResponse
                    Case MSG_VALUE_YES
                        ' Save and go back to Search Page
                        If ApplyChanges() = True Then
                            State.boChanged = True
                            GoBack()
                        End If
                    Case MSG_VALUE_NO
                        GoBack()
                End Select
            End If
        End Sub

        Protected Sub ComingFromNewCopy()
            Dim confResponse As String = HiddenSaveChangesPromptResponse.Value

            If Not confResponse = String.Empty Then
                ' Return from the New Copy Button
                Select Case confResponse
                    Case MSG_VALUE_YES
                        ' Save and create a new Copy BO
                        If ApplyChanges() = True Then
                            State.boChanged = True
                            CreateNewCopy()
                        End If
                    Case MSG_VALUE_NO
                        ' create a new BO
                        CreateNewCopy()
                End Select
            End If
        End Sub
        Protected Sub ComingFromNew()
            Dim confResponse As String = HiddenSaveChangesPromptResponse.Value

            If Not confResponse = String.Empty Then
                ' Return from the New Copy Button
                Select Case confResponse
                    Case MSG_VALUE_YES
                        ' Save and create a new Copy BO
                        If ApplyChanges() = True Then
                            State.boChanged = True
                            CreateNew()
                        End If
                    Case MSG_VALUE_NO
                        ' create a new BO
                        CreateNew()
                End Select
            End If
        End Sub

        Protected Sub CheckIfComingFromConfirm()
            Try
                Select Case State.ActionInProgress
                    ' Period
                    Case ElitaPlusPage.DetailPageCommand.Back
                        ComingFromBack()
                    Case ElitaPlusPage.DetailPageCommand.New_
                        ComingFromNew()
                    Case ElitaPlusPage.DetailPageCommand.NewAndCopy
                        ComingFromNewCopy()
                End Select

                'Clean after consuming the action
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
                HiddenSaveChangesPromptResponse.Value = String.Empty
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub
#End Region

#Region "Handlers-Labels"
        Private Sub BindBoPropertiesToLabels()
            BindBOPropertyToLabel(TheTemplate, TEMPLATECODE_PROPERTY, lblTemplateCode)
            BindBOPropertyToLabel(TheTemplate, DESCRIPTION_PROPERTY, lblTemplateDescription)
            BindBOPropertyToLabel(TheTemplate, HASCUSTOMIZEDPARAMSXCD_PROPERTY, lblHasCustomizedParams)
            BindBOPropertyToLabel(TheTemplate, ALLOWMANUALUSEXCD_PROPERTY, lblAllowManualUse)
            BindBOPropertyToLabel(TheTemplate, ALLOWMANUALRESENDXCD_PROPERTY, lblAllowManualResend)
            BindBOPropertyToLabel(TheTemplate, EFFECTIVEDATE_PROPERTY, lblEffectiveDate)
            BindBOPropertyToLabel(TheTemplate, EXPIRATIONDATE_PROPERTY, lblExpirationDate)

            BindBOPropertyToLabel(TheTemplate, "TemplateCode", lblTemplateCode)
            BindBOPropertyToLabel(TheTemplate, "SmsAppKey", lblSMSAppKey)
            BindBOPropertyToLabel(TheTemplate, "SmsShortCode", lblSMSShortCode)
            BindBOPropertyToLabel(TheTemplate, "SmsTriggerId", lblSMSTriggerId)
        End Sub

        Private Sub ClearLabelsErrSign()
            ClearLabelErrSign(lblTemplateCode)
            ClearLabelErrSign(lblTemplateDescription)
            ClearLabelErrSign(lblHasCustomizedParams)
            ClearLabelErrSign(lblAllowManualUse)
            ClearLabelErrSign(lblAllowManualResend)
            ClearLabelErrSign(lblEffectiveDate)
            ClearLabelErrSign(lblExpirationDate)

            ClearLabelErrSign(lblTemplateCode)
            ClearLabelErrSign(lblSMSAppKey)
            ClearLabelErrSign(lblSMSShortCode)
            ClearLabelErrSign(lblSMSTriggerId)
        End Sub

        Public Shared Sub SetLabelColor(lbl As Label)
            lbl.ForeColor = Color.Black
        End Sub
#End Region

#Region "Datagrid Related"
#Region "Parameters Grid"
        Public Sub ParametersGrid_RowCommand(source As System.Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs)
            Dim index As Integer

            Try
                If e.CommandName = EDIT_COMMAND_NAME Then
                    index = CInt(e.CommandArgument)
                    ParametersGrid.EditIndex = index
                    ParametersGrid.SelectedIndex = index

                    State.ParametersGrid_IsInEditMode = True
                    State.ParametersGrid_TemplateParamsId = New Guid(CType(ParametersGrid.Rows(index).Cells(GRID_PARAM_COL_OC_TEMPLATE_PARAMS_ID).FindControl(ID_CONTROL_NAME), Label).Text)

                    State.ParametersGrid_TemplateParamsBO = TheTemplate.GetParameterChild(State.ParametersGrid_TemplateParamsId)

                    ParametersGrid_Populate()

                    'Disable all Edit and Delete icon buttons on the Grid
                    SetGridControls(ParametersGrid, False)
                    State.ParametersGrid_PageIndex = ParametersGrid.PageIndex

                    State.ParametersGrid_RecordEdit = True

                    ParametersGrid_PopulateFormFromBO(index)

                    ParametersGrid_SetFocusOnEditableField(ParametersGrid, GRID_PARAM_COL_PARAM_NAME, GRID_PARAM_COL_PARAM_NAME_TEXTBOX_CONTROL_NAME, index)
                    ParametersGrid_SetButtonsState(False)
                ElseIf (e.CommandName = DELETE_COMMAND) Then
                    index = CInt(e.CommandArgument)

                    ParametersGrid_Populate()
                    State.ParametersGrid_PageIndex = ParametersGrid.PageIndex

                    'Clear the SelectedItemStyle to remove the highlight from the previously saved row
                    ParametersGrid.SelectedIndex = NO_ROW_SELECTED_INDEX
                    'Save the Id in the Session
                    State.ParametersGrid_TemplateParamsId = New Guid(CType(ParametersGrid.Rows(index).Cells(GRID_PARAM_COL_OC_TEMPLATE_PARAMS_ID).FindControl(ID_CONTROL_NAME), Label).Text)
                    DisplayMessage(Message.DELETE_RECORD_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenDeletePromptResponse)
                    State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Delete
                    State.ParametersGrid_RecordDelete = True
                ElseIf (e.CommandName = SAVE_COMMAND) Then
                    ParametersGrid_Save()
                ElseIf (e.CommandName = CANCEL_COMMAND) Then
                    ParametersGrid_Cancel()
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Public Sub ParametersGrid_RowCreated(sender As System.Object, e As System.Web.UI.WebControls.GridViewRowEventArgs)
            BaseItemCreated(sender, e)
        End Sub

        Private Sub ParametersGrid__PageIndexChanging(source As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles ParametersGrid.PageIndexChanging
            Try
                If (Not (State.ParametersGrid_IsInEditMode)) Then
                    State.ParametersGrid_PageIndex = e.NewPageIndex
                    ParametersGrid.PageIndex = State.ParametersGrid_PageIndex
                    ParametersGrid_Populate()
                    ParametersGrid.SelectedIndex = NO_ITEM_SELECTED_INDEX
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub ParametersGrid_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles ParametersGrid.RowDataBound
            Try
                Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
                Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)

                If dvRow IsNot Nothing AndAlso State.ParametersGrid_DV.Count > 0 Then
                    If itemType = ListItemType.Item OrElse itemType = ListItemType.AlternatingItem OrElse itemType = ListItemType.SelectedItem OrElse itemType = ListItemType.EditItem Then
                        CType(e.Row.Cells(GRID_PARAM_COL_OC_TEMPLATE_PARAMS_ID).FindControl(ID_CONTROL_NAME), Label).Text = GetGuidStringFromByteArray(CType(dvRow(OcTemplateParams.TemplateParamsDV.COL_OC_TEMPLATE_PARAMS_ID), Byte()))

                        If (State.ParametersGrid_IsInEditMode = True AndAlso State.ParametersGrid_TemplateParamsId.ToString.Equals(GetGuidStringFromByteArray(CType(dvRow(OcTemplateParams.TemplateParamsDV.COL_OC_TEMPLATE_PARAMS_ID), Byte())))) Then
                            CType(e.Row.Cells(GRID_PARAM_COL_PARAM_NAME).FindControl(GRID_PARAM_COL_PARAM_NAME_TEXTBOX_CONTROL_NAME), TextBox).Text = dvRow(OcTemplateParams.TemplateParamsDV.COL_PARAM_NAME).ToString
                            CType(e.Row.Cells(GRID_PARAM_COL_DATE_FORMAT_STRING).FindControl(GRID_PARAM_COL_DATE_FORMAT_STRING_TEXTBOX_CONTROL_NAME), TextBox).Text = dvRow(OcTemplateParams.TemplateParamsDV.COL_DATE_FORMAT_STRING).ToString

                            BindCodeToListControl(CType(e.Row.Cells(GRID_PARAM_COL_PARAM_VALUE_SOURCE_XCD).FindControl(GRID_PARAM_COL_PARAM_VALUE_SOURCE_XCD_DROPDOWNLIST_CONTROL_NAME), DropDownList),
                                                       LookupListNew.GetParamValueSourceLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), , , True)

                            BindCodeToListControl(CType(e.Row.Cells(GRID_PARAM_COL_PARAM_DATA_TYPE_XCD).FindControl(GRID_PARAM_COL_PARAM_DATA_TYPE_XCD_DROPDOWNLIST_CONTROL_NAME), DropDownList),
                                                       LookupListNew.GetParamDataTypeLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), , , True)

                            BindCodeToListControl(CType(e.Row.Cells(GRID_PARAM_COL_ALLOW_EMPTY_VALUE_XCD).FindControl(GRID_PARAM_COL_ALLOW_EMPTY_VALUE_XCD_DROPDOWNLIST_CONTROL_NAME), DropDownList),
                                                       LookupListNew.GetYesNoXcdList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), , , True)
                        Else
                            CType(e.Row.Cells(GRID_PARAM_COL_PARAM_NAME).FindControl(GRID_PARAM_COL_PARAM_NAME_LABEL_CONTROL_NAME), Label).Text = dvRow(OcTemplateParams.TemplateParamsDV.COL_PARAM_NAME).ToString
                            CType(e.Row.Cells(GRID_PARAM_COL_DATE_FORMAT_STRING).FindControl(GRID_PARAM_COL_DATE_FORMAT_STRING_LABEL_CONTROL_NAME), Label).Text = dvRow(OcTemplateParams.TemplateParamsDV.COL_DATE_FORMAT_STRING).ToString
                            CType(e.Row.Cells(GRID_PARAM_COL_PARAM_VALUE_SOURCE_XCD).FindControl(GRID_PARAM_COL_PARAM_VALUE_SOURCE_XCD_LABEL_CONTROL_NAME), Label).Text = dvRow(OcTemplateParams.TemplateParamsDV.COL_PARAM_VALUE_SOURCE_DESCRIPTION).ToString
                            CType(e.Row.Cells(GRID_PARAM_COL_PARAM_DATA_TYPE_XCD).FindControl(GRID_PARAM_COL_PARAM_DATA_TYPE_XCD_LABEL_CONTROL_NAME), Label).Text = dvRow(OcTemplateParams.TemplateParamsDV.COL_PARAM_DATA_TYPE_DESCRIPTION).ToString
                            CType(e.Row.Cells(GRID_PARAM_COL_ALLOW_EMPTY_VALUE_XCD).FindControl(GRID_PARAM_COL_ALLOW_EMPTY_VALUE_XCD_LABEL_CONTROL_NAME), Label).Text = dvRow(OcTemplateParams.TemplateParamsDV.COL_ALLOW_EMPTY_VALUE_DESCRIPTION).ToString
                        End If
                    End If
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub ParametersGrid_Populate()
            Dim oDataView As DataView

            Try
                With TheTemplate
                    If Not .Id.Equals(Guid.Empty) Then
                        If State.ParametersGrid_DV Is Nothing Then
                            State.ParametersGrid_DV = ParametersGrid_GetDV()
                        End If
                    End If
                End With

                If State.ParametersGrid_DV IsNot Nothing Then
                    Dim dv As OcTemplateParams.TemplateParamsDV

                    If State.ParametersGrid_DV.Count = 0 Then
                        dv = State.ParametersGrid_DV.AddNewRowToEmptyDV
                        SetPageAndSelectedIndexFromGuid(dv, State.ParametersGrid_TemplateParamsId, ParametersGrid, State.ParametersGrid_PageIndex)
                        ParametersGrid.DataSource = dv
                    Else
                        SetPageAndSelectedIndexFromGuid(State.ParametersGrid_DV, State.ParametersGrid_TemplateParamsId, ParametersGrid, State.ParametersGrid_PageIndex)
                        ParametersGrid.DataSource = State.ParametersGrid_DV
                    End If

                    If (State.ParametersGrid_IsAfterSave) Then
                        State.ParametersGrid_IsAfterSave = False
                        SetPageAndSelectedIndexFromGuid(State.ParametersGrid_DV, State.ParametersGrid_TemplateParamsId, ParametersGrid, ParametersGrid.PageIndex)
                    ElseIf (State.ParametersGrid_IsInEditMode) Then
                        SetPageAndSelectedIndexFromGuid(State.ParametersGrid_DV, State.ParametersGrid_TemplateParamsId, ParametersGrid, ParametersGrid.PageIndex, State.ParametersGrid_IsInEditMode)
                    Else
                        'In a Delete scenario...
                        SetPageAndSelectedIndexFromGuid(State.ParametersGrid_DV, Guid.Empty, ParametersGrid, ParametersGrid.PageIndex, State.ParametersGrid_IsInEditMode)
                    End If

                    ParametersGrid.AutoGenerateColumns = False

                    If State.ParametersGrid_DV.Count = 0 Then
                        ParametersGrid_SortAndBind(dv)
                    Else
                        ParametersGrid_SortAndBind(State.ParametersGrid_DV)
                    End If

                    If State.ParametersGrid_DV.Count = 0 Then
                        For Each gvRow As GridViewRow In ParametersGrid.Rows
                            gvRow.Visible = False
                            gvRow.Controls.Clear()
                        Next
                    End If
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub ParametersGrid_BindBoPropertiesToHeaders()
            If State.ParametersGrid_TemplateParamsBO IsNot Nothing Then
                BindBOPropertyToGridHeader(State.ParametersGrid_TemplateParamsBO, "ParamName", ParametersGrid.Columns(GRID_PARAM_COL_PARAM_NAME))
                BindBOPropertyToGridHeader(State.ParametersGrid_TemplateParamsBO, "ParamValueSourceXcd", ParametersGrid.Columns(GRID_PARAM_COL_PARAM_VALUE_SOURCE_XCD))
                BindBOPropertyToGridHeader(State.ParametersGrid_TemplateParamsBO, "ParamDataTypeXcd", ParametersGrid.Columns(GRID_PARAM_COL_PARAM_DATA_TYPE_XCD))
                BindBOPropertyToGridHeader(State.ParametersGrid_TemplateParamsBO, "DateFormatString", ParametersGrid.Columns(GRID_PARAM_COL_DATE_FORMAT_STRING))
                BindBOPropertyToGridHeader(State.ParametersGrid_TemplateParamsBO, "AllowEmptyValueXcd", ParametersGrid.Columns(GRID_PARAM_COL_ALLOW_EMPTY_VALUE_XCD))
            End If
            ClearGridViewHeadersAndLabelsErrSign()
        End Sub

        Private Sub ParametersGrid_SetFocusOnEditableField(grid As GridView, cellPosition As Integer, controlName As String, itemIndex As Integer)
            Dim control As TextBox = CType(grid.Rows(itemIndex).Cells(cellPosition).FindControl(controlName), TextBox)
            SetFocus(control)
        End Sub

        Private Sub ParametersGrid_PopulateFormFromBO(Optional ByVal gridRowIdx As Integer? = Nothing)
            If IsNothing(gridRowIdx) Then gridRowIdx = ParametersGrid.EditIndex

            Dim cboParamValueSource As DropDownList = CType(ParametersGrid.Rows(gridRowIdx).Cells(GRID_PARAM_COL_PARAM_VALUE_SOURCE_XCD).FindControl(GRID_PARAM_COL_PARAM_VALUE_SOURCE_XCD_DROPDOWNLIST_CONTROL_NAME), DropDownList)
            BindCodeToListControl(cboParamValueSource, LookupListNew.GetParamValueSourceLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), , , True)

            Dim cboParamDataType As DropDownList = CType(ParametersGrid.Rows(gridRowIdx).Cells(GRID_PARAM_COL_PARAM_DATA_TYPE_XCD).FindControl(GRID_PARAM_COL_PARAM_DATA_TYPE_XCD_DROPDOWNLIST_CONTROL_NAME), DropDownList)
            BindCodeToListControl(cboParamDataType, LookupListNew.GetParamDataTypeLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), , , True)

            Dim cboAllowEmptyValue As DropDownList = CType(ParametersGrid.Rows(gridRowIdx).Cells(GRID_PARAM_COL_ALLOW_EMPTY_VALUE_XCD).FindControl(GRID_PARAM_COL_ALLOW_EMPTY_VALUE_XCD_DROPDOWNLIST_CONTROL_NAME), DropDownList)
            BindCodeToListControl(cboAllowEmptyValue, LookupListNew.GetYesNoXcdList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), , , True)

            Try
                With State.ParametersGrid_TemplateParamsBO
                    If (Not .Id.Equals(Guid.Empty)) AndAlso Not .IsNew Then
                        If Not String.IsNullOrEmpty(.ParamValueSourceXcd) Then
                            'Me.PopulateControlFromBOProperty(cboParamValueSource, .ParamValueSourceXcd)
                            SetSelectedItem(cboParamValueSource, .ParamValueSourceXcd)
                        End If

                        If Not String.IsNullOrEmpty(.ParamDataTypeXcd) Then
                            SetSelectedItem(cboParamDataType, .ParamDataTypeXcd)
                        End If

                        If Not String.IsNullOrEmpty(.AllowEmptyValueXcd) Then
                            SetSelectedItem(cboAllowEmptyValue, .AllowEmptyValueXcd)
                        End If
                    End If

                    Dim txtParamName As TextBox = CType(ParametersGrid.Rows(gridRowIdx).Cells(GRID_PARAM_COL_PARAM_NAME).FindControl(GRID_PARAM_COL_PARAM_NAME_TEXTBOX_CONTROL_NAME), TextBox)
                    PopulateControlFromBOProperty(txtParamName, .ParamName)

                    Dim txtDateFormatString As TextBox = CType(ParametersGrid.Rows(gridRowIdx).Cells(GRID_PARAM_COL_DATE_FORMAT_STRING).FindControl(GRID_PARAM_COL_DATE_FORMAT_STRING_TEXTBOX_CONTROL_NAME), TextBox)
                    PopulateControlFromBOProperty(txtDateFormatString, .DateFormatString)

                    CType(ParametersGrid.Rows(gridRowIdx).Cells(GRID_PARAM_COL_OC_TEMPLATE_PARAMS_ID).FindControl(ID_CONTROL_NAME), Label).Text = .Id.ToString
                End With
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub ParametersGrid_SetButtonsState(bIsEdit As Boolean)
            ControlMgr.SetEnableControl(Me, btnNewParameter_WRITE, bIsEdit)
            ControlMgr.SetEnableControl(Me, btnNewRecipient_WRITE, bIsEdit)
            ControlMgr.SetEnableControl(Me, btnNew_WRITE, bIsEdit)
            ControlMgr.SetEnableControl(Me, btnCopy_WRITE, bIsEdit)
            ControlMgr.SetEnableControl(Me, btnDelete_WRITE, bIsEdit)
            ControlMgr.SetEnableControl(Me, btnApply_WRITE, bIsEdit)
            ControlMgr.SetEnableControl(Me, btnBack, bIsEdit)
            ControlMgr.SetEnableControl(Me, btnUndo_WRITE, bIsEdit)
        End Sub

        Private Function ParametersGrid_GetDV() As OcTemplateParams.TemplateParamsDV
            Dim dv As OcTemplateParams.TemplateParamsDV
            dv = ParametersGrid_GetDataView()
            'dv.Sort = Me.ParametersGrid.DataMember()
            dv.Sort = State.ParametersGrid_SortExpression
            ParametersGrid.DataSource = dv
            Return (dv)
        End Function

        Private Function ParametersGrid_GetDataView() As OcTemplateParams.TemplateParamsDV
            Dim dt As DataTable = TheTemplate.ParametersList.Table
            Return New OcTemplateParams.TemplateParamsDV(dt)
        End Function

        Private Sub ParametersGrid_SortAndBind(dvBinding As DataView, Optional ByVal blnEmptyList As Boolean = False)
            ParametersGrid.DataSource = dvBinding
            HighLightSortColumn(ParametersGrid, State.ParametersGrid_SortExpression)
            ParametersGrid.DataBind()

            If Not ParametersGrid.BottomPagerRow.Visible Then ParametersGrid.BottomPagerRow.Visible = True

            If blnEmptyList Then
                For Each gvRow As GridViewRow In ParametersGrid.Rows
                    gvRow.Controls.Clear()
                Next
            End If

            Session("recCount") = State.ParametersGrid_DV.Count

            ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me, ParametersGrid)
        End Sub

        Private Sub ParametersGrid_AddNew()
            State.ParametersGrid_TemplateParamsBO = TheTemplate.GetNewParameterChild
            State.ParametersGrid_DV = ParametersGrid_GetDV()
            State.ParametersGrid_TemplateParamsId = State.ParametersGrid_TemplateParamsBO.Id
            ParametersGrid.DataSource = State.ParametersGrid_DV
            SetPageAndSelectedIndexFromGuid(State.ParametersGrid_DV, State.ParametersGrid_TemplateParamsId, ParametersGrid, State.ParametersGrid_PageIndex, State.ParametersGrid_IsInEditMode)
            ParametersGrid.AutoGenerateColumns = False
            ParametersGrid_SortAndBind(State.ParametersGrid_DV)
            SetGridControls(ParametersGrid, False)
            State.ParametersGrid_RecordNew = True
            ParametersGrid_PopulateFormFromBO()
        End Sub

        Private Sub ParametersGrid_ReturnFromEditing()
            ParametersGrid.EditIndex = NO_ROW_SELECTED_INDEX

            If ParametersGrid.PageCount = 0 Then
                'if returning to the "1st time in" screen
                ControlMgr.SetVisibleControl(Me, ParametersGrid, False)
            Else
                ControlMgr.SetVisibleControl(Me, ParametersGrid, True)
            End If

            SetGridControls(ParametersGrid, True)
            State.ParametersGrid_IsInEditMode = False
            State.ParametersGrid_TemplateParamsId = Guid.Empty
            State.ParametersGrid_TemplateParamsBO = Nothing
            ParametersGrid_Populate()
            State.ParametersGrid_PageIndex = ParametersGrid.PageIndex
            ParametersGrid_SetButtonsState(True)
        End Sub

        Private Sub ParametersGrid_PopulateBOFromForm()
            Dim txtParamName As TextBox = CType(ParametersGrid.Rows(ParametersGrid.EditIndex).Cells(GRID_PARAM_COL_PARAM_NAME).FindControl(GRID_PARAM_COL_PARAM_NAME_TEXTBOX_CONTROL_NAME), TextBox)
            Dim cboParamValueSource As DropDownList = CType(ParametersGrid.Rows(ParametersGrid.EditIndex).Cells(GRID_PARAM_COL_PARAM_VALUE_SOURCE_XCD).FindControl(GRID_PARAM_COL_PARAM_VALUE_SOURCE_XCD_DROPDOWNLIST_CONTROL_NAME), DropDownList)
            Dim cboParamDataType As DropDownList = CType(ParametersGrid.Rows(ParametersGrid.EditIndex).Cells(GRID_PARAM_COL_PARAM_DATA_TYPE_XCD).FindControl(GRID_PARAM_COL_PARAM_DATA_TYPE_XCD_DROPDOWNLIST_CONTROL_NAME), DropDownList)
            Dim txtDateFormatString As TextBox = CType(ParametersGrid.Rows(ParametersGrid.EditIndex).Cells(GRID_PARAM_COL_DATE_FORMAT_STRING).FindControl(GRID_PARAM_COL_DATE_FORMAT_STRING_TEXTBOX_CONTROL_NAME), TextBox)
            Dim cboAllowEmptyValue As DropDownList = CType(ParametersGrid.Rows(ParametersGrid.EditIndex).Cells(GRID_PARAM_COL_ALLOW_EMPTY_VALUE_XCD).FindControl(GRID_PARAM_COL_ALLOW_EMPTY_VALUE_XCD_DROPDOWNLIST_CONTROL_NAME), DropDownList)

            PopulateBOProperty(State.ParametersGrid_TemplateParamsBO, "OcTemplateId", TheTemplate.Id)
            PopulateBOProperty(State.ParametersGrid_TemplateParamsBO, "ParamName", txtParamName)
            PopulateBOProperty(State.ParametersGrid_TemplateParamsBO, "ParamValueSourceXcd", cboParamValueSource, False, True)
            PopulateBOProperty(State.ParametersGrid_TemplateParamsBO, "ParamValueSourceDescription", cboParamValueSource, False, False)
            PopulateBOProperty(State.ParametersGrid_TemplateParamsBO, "ParamDataTypeXcd", cboParamDataType, False, True)
            PopulateBOProperty(State.ParametersGrid_TemplateParamsBO, "ParamDataTypeDescription", cboParamDataType, False, False)
            PopulateBOProperty(State.ParametersGrid_TemplateParamsBO, "DateFormatString", txtDateFormatString)
            PopulateBOProperty(State.ParametersGrid_TemplateParamsBO, "AllowEmptyValueXcd", cboAllowEmptyValue, False, True)
            PopulateBOProperty(State.ParametersGrid_TemplateParamsBO, "AllowEmptyValueDescription", cboAllowEmptyValue, False, False)

            If ErrCollection.Count > 0 Then
                Throw New PopulateBOErrorException
            End If
        End Sub

        Private Sub ParametersGrid_btnNew_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnNewParameter_WRITE.Click
            Try
                If Not TheTemplate.Id.Equals(Guid.Empty) Then
                    State.ParametersGrid_IsInEditMode = True
                    State.ParametersGrid_DV = Nothing
                    ParametersGrid_AddNew()
                    ParametersGrid_SetButtonsState(False)
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub ParametersGrid_Cancel()
            Try
                SetGridControls(ParametersGrid, True)

                If State.ParametersGrid_RecordNew Then
                    TheTemplate.RemoveParametersChild(State.ParametersGrid_TemplateParamsId)
                    State.ParametersGrid_DV = Nothing
                    State.ParametersGrid_RecordNew = False
                Else
                    State.ParametersGrid_RecordEdit = False
                End If

                ParametersGrid_ReturnFromEditing()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub ParametersGrid_Save()
            Try
                ParametersGrid_BindBoPropertiesToHeaders()
                ParametersGrid_PopulateBOFromForm()

                If (State.ParametersGrid_TemplateParamsBO.IsDirty) Then
                    Try
                        State.ParametersGrid_TemplateParamsBO.Save()
                    Catch ex As Exception
                        If Not State.ParametersGrid_RecordNew Then
                            State.ParametersGrid_TemplateParamsBO.RejectChanges()
                        End If

                        Throw
                    End Try

                    State.ParametersGrid_IsAfterSave = True
                    State.ParametersGrid_TemplateParamsBO.EndEdit()
                    State.ParametersGrid_DV = Nothing
                    State.ParametersGrid_RecordNew = False
                    State.ParametersGrid_RecordEdit = False
                    ParametersGrid_ReturnFromEditing()
                Else
                    AddInfoMsg(MSG_RECORD_NOT_SAVED)
                    ParametersGrid_ReturnFromEditing()
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub ParametersGrid_Delete()
            Dim templateParamsBO = TheTemplate.GetParameterChild(State.ParametersGrid_TemplateParamsId)

            Try
                templateParamsBO.Delete()
                templateParamsBO.Save()
                templateParamsBO.EndEdit()

                templateParamsBO = Nothing
                State.ParametersGrid_DV = Nothing
            Catch ex As Exception
                TheTemplate.RejectChanges()
                Throw ex
            End Try

            ParametersGrid_ReturnFromEditing()
        End Sub

        Protected Sub ParametersGrid_CheckIfComingFromDeleteConfirm()
            Dim confResponse As String = HiddenDeletePromptResponse.Value
            If confResponse IsNot Nothing AndAlso confResponse = MSG_VALUE_YES Then
                If Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Delete Then
                    ParametersGrid_Delete()
                    'Clean after consuming the action
                    State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
                    HiddenDeletePromptResponse.Value = ""
                End If
            ElseIf confResponse IsNot Nothing AndAlso confResponse = MSG_VALUE_NO Then
                ParametersGrid_ReturnFromEditing()
                'Clean after consuming the action
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
                HiddenDeletePromptResponse.Value = ""
            End If
        End Sub
#End Region

#Region "Recipients Grid"
        Public Sub RecipientsGrid_RowCommand(source As System.Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs)
            Dim index As Integer

            Try
                If e.CommandName = EDIT_COMMAND_NAME Then
                    index = CInt(e.CommandArgument)
                    RecipientsGrid.EditIndex = index
                    RecipientsGrid.SelectedIndex = index

                    State.RecipientsGrid_IsInEditMode = True
                    State.RecipientsGrid_TemplateRecipientId = New Guid(CType(RecipientsGrid.Rows(index).Cells(GRID_RECIPIENT_COL_OC_TEMPLATE_RECIPIENT_ID).FindControl(ID_CONTROL_NAME), Label).Text)

                    State.RecipientsGrid_TemplateRecipientBO = TheTemplate.GetRecipientChild(State.RecipientsGrid_TemplateRecipientId)

                    RecipientsGrid_Populate()

                    'Disable all Edit and Delete icon buttons on the Grid
                    SetGridControls(RecipientsGrid, False)
                    State.RecipientsGrid_PageIndex = RecipientsGrid.PageIndex

                    State.RecipientsGrid_RecordEdit = True

                    RecipientsGrid_PopulateFormFromBO(index)

                    RecipientsGrid_SetFocusOnEditableField(RecipientsGrid, GRID_RECIPIENT_COL_DESCRIPTION, GRID_RECIPIENT_COL_DESCRIPTION_TEXTBOX_CONTROL_NAME, index)
                    RecipientsGrid_SetButtonsState(False)
                ElseIf (e.CommandName = DELETE_COMMAND) Then
                    index = CInt(e.CommandArgument)

                    RecipientsGrid_Populate()
                    State.RecipientsGrid_PageIndex = RecipientsGrid.PageIndex

                    'Clear the SelectedItemStyle to remove the highlight from the previously saved row
                    RecipientsGrid.SelectedIndex = NO_ROW_SELECTED_INDEX
                    'Save the Id in the Session
                    State.RecipientsGrid_TemplateRecipientId = New Guid(CType(RecipientsGrid.Rows(index).Cells(GRID_RECIPIENT_COL_OC_TEMPLATE_RECIPIENT_ID).FindControl(ID_CONTROL_NAME), Label).Text)
                    DisplayMessage(Message.DELETE_RECORD_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenDeletePromptResponse)
                    State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Delete
                    State.RecipientsGrid_RecordDelete = True
                ElseIf (e.CommandName = SAVE_COMMAND) Then
                    RecipientsGrid_Save()
                ElseIf (e.CommandName = CANCEL_COMMAND) Then
                    RecipientsGrid_Cancel()
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Public Sub RecipientsGrid_RowCreated(sender As System.Object, e As System.Web.UI.WebControls.GridViewRowEventArgs)
            BaseItemCreated(sender, e)
        End Sub

        Private Sub RecipientsGrid__PageIndexChanging(source As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles RecipientsGrid.PageIndexChanging
            Try
                If (Not (State.RecipientsGrid_IsInEditMode)) Then
                    State.RecipientsGrid_PageIndex = e.NewPageIndex
                    RecipientsGrid.PageIndex = State.RecipientsGrid_PageIndex
                    RecipientsGrid_Populate()
                    RecipientsGrid.SelectedIndex = NO_ITEM_SELECTED_INDEX
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub RecipientsGrid_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles RecipientsGrid.RowDataBound
            Try
                Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
                Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)

                If dvRow IsNot Nothing AndAlso State.RecipientsGrid_DV.Count > 0 Then
                    If itemType = ListItemType.Item OrElse itemType = ListItemType.AlternatingItem OrElse itemType = ListItemType.SelectedItem OrElse itemType = ListItemType.EditItem Then
                        CType(e.Row.Cells(GRID_RECIPIENT_COL_OC_TEMPLATE_RECIPIENT_ID).FindControl(ID_CONTROL_NAME), Label).Text = GetGuidStringFromByteArray(CType(dvRow(OcTemplateRecipient.TemplateRecipientsDV.COL_OC_TEMPLATE_RECIPIENT_ID), Byte()))

                        If (State.RecipientsGrid_IsInEditMode = True AndAlso State.RecipientsGrid_TemplateRecipientId.ToString.Equals(GetGuidStringFromByteArray(CType(dvRow(OcTemplateRecipient.TemplateRecipientsDV.COL_OC_TEMPLATE_RECIPIENT_ID), Byte())))) Then
                            CType(e.Row.Cells(GRID_RECIPIENT_COL_DESCRIPTION).FindControl(GRID_RECIPIENT_COL_DESCRIPTION_TEXTBOX_CONTROL_NAME), TextBox).Text = dvRow(OcTemplateRecipient.TemplateRecipientsDV.COL_DESCRIPTION).ToString

                            BindCodeToListControl(CType(e.Row.Cells(GRID_RECIPIENT_COL_RECIPIENT_SOURCE_FIELD_XCD).FindControl(GRID_RECIPIENT_COL_RECIPIENT_SOURCE_FIELD_XCD_DROPDOWNLIST_CONTROL_NAME), DropDownList),
                                                       LookupListNew.GetRecipientSourceFieldLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), , , True)
                        Else
                            CType(e.Row.Cells(GRID_RECIPIENT_COL_DESCRIPTION).FindControl(GRID_RECIPIENT_COL_DESCRIPTION_LABEL_CONTROL_NAME), Label).Text = dvRow(OcTemplateRecipient.TemplateRecipientsDV.COL_DESCRIPTION).ToString
                            CType(e.Row.Cells(GRID_RECIPIENT_COL_RECIPIENT_SOURCE_FIELD_XCD).FindControl(GRID_RECIPIENT_COL_RECIPIENT_SOURCE_FIELD_XCD_LABEL_CONTROL_NAME), Label).Text = dvRow(OcTemplateRecipient.TemplateRecipientsDV.COL_RECIPIENT_SOURCE_FIELD_DESCRIPTION).ToString
                        End If
                    End If
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub RecipientsGrid_Populate()
            Dim oDataView As DataView

            Try
                With TheTemplate
                    If Not .Id.Equals(Guid.Empty) Then
                        If State.RecipientsGrid_DV Is Nothing Then
                            State.RecipientsGrid_DV = RecipientsGrid_GetDV()
                        End If
                    End If
                End With

                If State.RecipientsGrid_DV IsNot Nothing Then
                    Dim dv As OcTemplateRecipient.TemplateRecipientsDV

                    If State.RecipientsGrid_DV.Count = 0 Then
                        dv = State.RecipientsGrid_DV.AddNewRowToEmptyDV
                        SetPageAndSelectedIndexFromGuid(dv, State.RecipientsGrid_TemplateRecipientId, RecipientsGrid, State.RecipientsGrid_PageIndex)
                        RecipientsGrid.DataSource = dv
                    Else
                        SetPageAndSelectedIndexFromGuid(State.RecipientsGrid_DV, State.RecipientsGrid_TemplateRecipientId, RecipientsGrid, State.RecipientsGrid_PageIndex)
                        RecipientsGrid.DataSource = State.RecipientsGrid_DV
                    End If

                    State.RecipientsGrid_DV.Sort = State.RecipientsGrid_SortExpression

                    If (State.RecipientsGrid_IsAfterSave) Then
                        State.RecipientsGrid_IsAfterSave = False
                        SetPageAndSelectedIndexFromGuid(State.RecipientsGrid_DV, State.RecipientsGrid_TemplateRecipientId, RecipientsGrid, RecipientsGrid.PageIndex)
                    ElseIf (State.RecipientsGrid_IsInEditMode) Then
                        SetPageAndSelectedIndexFromGuid(State.RecipientsGrid_DV, State.RecipientsGrid_TemplateRecipientId, RecipientsGrid, RecipientsGrid.PageIndex, State.RecipientsGrid_IsInEditMode)
                    Else
                        'In a Delete scenario...
                        SetPageAndSelectedIndexFromGuid(State.RecipientsGrid_DV, Guid.Empty, RecipientsGrid, RecipientsGrid.PageIndex, State.RecipientsGrid_IsInEditMode)
                    End If

                    RecipientsGrid.AutoGenerateColumns = False

                    If State.RecipientsGrid_DV.Count = 0 Then
                        RecipientsGrid_SortAndBind(dv)
                    Else
                        RecipientsGrid_SortAndBind(State.RecipientsGrid_DV)
                    End If

                    If State.RecipientsGrid_DV.Count = 0 Then
                        For Each gvRow As GridViewRow In RecipientsGrid.Rows
                            gvRow.Visible = False
                            gvRow.Controls.Clear()
                        Next
                    End If
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub RecipientsGrid_BindBoPropertiesToHeaders()
            If State.RecipientsGrid_TemplateRecipientBO IsNot Nothing Then
                BindBOPropertyToGridHeader(State.RecipientsGrid_TemplateRecipientBO, "Description", RecipientsGrid.Columns(GRID_RECIPIENT_COL_DESCRIPTION))
                BindBOPropertyToGridHeader(State.RecipientsGrid_TemplateRecipientBO, "RecipientSourceFieldXcd", RecipientsGrid.Columns(GRID_RECIPIENT_COL_RECIPIENT_SOURCE_FIELD_XCD))
            End If
            ClearGridViewHeadersAndLabelsErrSign()
        End Sub

        Private Sub RecipientsGrid_SetFocusOnEditableField(grid As GridView, cellPosition As Integer, controlName As String, itemIndex As Integer)
            Dim control As TextBox = CType(grid.Rows(itemIndex).Cells(cellPosition).FindControl(controlName), TextBox)
            SetFocus(control)
        End Sub

        Private Sub RecipientsGrid_PopulateFormFromBO(Optional ByVal gridRowIdx As Integer? = Nothing)
            If IsNothing(gridRowIdx) Then gridRowIdx = RecipientsGrid.EditIndex

            Dim cboRecipientSourceField As DropDownList = CType(RecipientsGrid.Rows(gridRowIdx).Cells(GRID_RECIPIENT_COL_RECIPIENT_SOURCE_FIELD_XCD).FindControl(GRID_RECIPIENT_COL_RECIPIENT_SOURCE_FIELD_XCD_DROPDOWNLIST_CONTROL_NAME), DropDownList)
            BindCodeToListControl(cboRecipientSourceField, LookupListNew.GetRecipientSourceFieldLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), , , True)

            Try
                With State.RecipientsGrid_TemplateRecipientBO
                    If (Not .Id.Equals(Guid.Empty)) AndAlso Not .IsNew Then
                        If Not String.IsNullOrEmpty(.RecipientSourceFieldXcd) Then
                            'Me.PopulateControlFromBOProperty(cboRecipientSourceField, .RecipientSourceFieldXcd)
                            SetSelectedItem(cboRecipientSourceField, .RecipientSourceFieldXcd)
                        End If
                    End If

                    Dim txtDescription As TextBox = CType(RecipientsGrid.Rows(gridRowIdx).Cells(GRID_RECIPIENT_COL_DESCRIPTION).FindControl(GRID_RECIPIENT_COL_DESCRIPTION_TEXTBOX_CONTROL_NAME), TextBox)
                    PopulateControlFromBOProperty(txtDescription, .Description)

                    CType(RecipientsGrid.Rows(gridRowIdx).Cells(GRID_RECIPIENT_COL_OC_TEMPLATE_RECIPIENT_ID).FindControl(ID_CONTROL_NAME), Label).Text = .Id.ToString
                End With
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub RecipientsGrid_SetButtonsState(bIsEdit As Boolean)
            ControlMgr.SetEnableControl(Me, btnNewParameter_WRITE, bIsEdit)
            ControlMgr.SetEnableControl(Me, btnNewRecipient_WRITE, bIsEdit)
            ControlMgr.SetEnableControl(Me, btnNew_WRITE, bIsEdit)
            ControlMgr.SetEnableControl(Me, btnCopy_WRITE, bIsEdit)
            ControlMgr.SetEnableControl(Me, btnDelete_WRITE, bIsEdit)
            ControlMgr.SetEnableControl(Me, btnApply_WRITE, bIsEdit)
            ControlMgr.SetEnableControl(Me, btnBack, bIsEdit)
            ControlMgr.SetEnableControl(Me, btnUndo_WRITE, bIsEdit)
        End Sub

        Private Function RecipientsGrid_GetDV() As OcTemplateRecipient.TemplateRecipientsDV
            Dim dv As OcTemplateRecipient.TemplateRecipientsDV
            dv = RecipientsGrid_GetDataView()
            'dv.Sort = Me.RecipientsGrid.DataMember()
            dv.Sort = State.RecipientsGrid_SortExpression
            RecipientsGrid.DataSource = dv
            Return (dv)
        End Function

        Private Function RecipientsGrid_GetDataView() As OcTemplateRecipient.TemplateRecipientsDV
            Dim dt As DataTable = TheTemplate.RecipientsList.Table
            Return New OcTemplateRecipient.TemplateRecipientsDV(dt)
        End Function

        Private Sub RecipientsGrid_SortAndBind(dvBinding As DataView, Optional ByVal blnEmptyList As Boolean = False)
            RecipientsGrid.DataSource = dvBinding
            HighLightSortColumn(RecipientsGrid, State.RecipientsGrid_SortExpression)
            RecipientsGrid.DataBind()

            If Not RecipientsGrid.BottomPagerRow.Visible Then RecipientsGrid.BottomPagerRow.Visible = True

            If blnEmptyList Then
                For Each gvRow As GridViewRow In RecipientsGrid.Rows
                    gvRow.Controls.Clear()
                Next
            End If

            Session("recCount") = State.RecipientsGrid_DV.Count

            ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me, RecipientsGrid)
        End Sub

        Private Sub RecipientsGrid_AddNew()
            State.RecipientsGrid_TemplateRecipientBO = TheTemplate.GetNewRecipientChild
            State.RecipientsGrid_DV = RecipientsGrid_GetDV()
            State.RecipientsGrid_TemplateRecipientId = State.RecipientsGrid_TemplateRecipientBO.Id
            RecipientsGrid.DataSource = State.RecipientsGrid_DV
            SetPageAndSelectedIndexFromGuid(State.RecipientsGrid_DV, State.RecipientsGrid_TemplateRecipientId, RecipientsGrid, State.RecipientsGrid_PageIndex, State.RecipientsGrid_IsInEditMode)
            RecipientsGrid.AutoGenerateColumns = False
            RecipientsGrid_SortAndBind(State.RecipientsGrid_DV)
            SetGridControls(RecipientsGrid, False)
            State.RecipientsGrid_RecordNew = True
            RecipientsGrid_PopulateFormFromBO()
        End Sub

        Private Sub RecipientsGrid_ReturnFromEditing()
            RecipientsGrid.EditIndex = NO_ROW_SELECTED_INDEX

            If RecipientsGrid.PageCount = 0 Then
                'if returning to the "1st time in" screen
                ControlMgr.SetVisibleControl(Me, RecipientsGrid, False)
            Else
                ControlMgr.SetVisibleControl(Me, RecipientsGrid, True)
            End If

            SetGridControls(RecipientsGrid, True)
            State.RecipientsGrid_IsInEditMode = False
            State.RecipientsGrid_TemplateRecipientId = Guid.Empty
            State.RecipientsGrid_TemplateRecipientBO = Nothing
            RecipientsGrid_Populate()
            State.RecipientsGrid_PageIndex = RecipientsGrid.PageIndex
            RecipientsGrid_SetButtonsState(True)
        End Sub

        Private Sub RecipientsGrid_PopulateBOFromForm()
            Dim txtDescription As TextBox = CType(RecipientsGrid.Rows(RecipientsGrid.EditIndex).Cells(GRID_RECIPIENT_COL_DESCRIPTION).FindControl(GRID_RECIPIENT_COL_DESCRIPTION_TEXTBOX_CONTROL_NAME), TextBox)
            Dim cboRecipientSourceField As DropDownList = CType(RecipientsGrid.Rows(RecipientsGrid.EditIndex).Cells(GRID_RECIPIENT_COL_RECIPIENT_SOURCE_FIELD_XCD).FindControl(GRID_RECIPIENT_COL_RECIPIENT_SOURCE_FIELD_XCD_DROPDOWNLIST_CONTROL_NAME), DropDownList)

            PopulateBOProperty(State.RecipientsGrid_TemplateRecipientBO, "OcTemplateId", TheTemplate.Id)
            PopulateBOProperty(State.RecipientsGrid_TemplateRecipientBO, "Description", txtDescription)
            PopulateBOProperty(State.RecipientsGrid_TemplateRecipientBO, "RecipientSourceFieldXcd", cboRecipientSourceField, False, True)
            PopulateBOProperty(State.RecipientsGrid_TemplateRecipientBO, "RecipientSourceFieldDescription", cboRecipientSourceField, False, False)

            If ErrCollection.Count > 0 Then
                Throw New PopulateBOErrorException
            End If
        End Sub

        Private Sub RecipientsGrid_btnNew_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnNewRecipient_WRITE.Click
            Try
                If Not TheTemplate.Id.Equals(Guid.Empty) Then
                    State.RecipientsGrid_IsInEditMode = True
                    State.RecipientsGrid_DV = Nothing
                    RecipientsGrid_AddNew()
                    RecipientsGrid_SetButtonsState(False)
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub RecipientsGrid_Cancel()
            Try
                SetGridControls(RecipientsGrid, True)

                If State.RecipientsGrid_RecordNew Then
                    TheTemplate.RemoveRecipientsChild(State.RecipientsGrid_TemplateRecipientId)
                    State.RecipientsGrid_DV = Nothing
                    State.RecipientsGrid_RecordNew = False
                Else
                    State.RecipientsGrid_RecordEdit = False
                End If

                RecipientsGrid_ReturnFromEditing()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub RecipientsGrid_Save()
            Try
                RecipientsGrid_BindBoPropertiesToHeaders()
                RecipientsGrid_PopulateBOFromForm()

                If (State.RecipientsGrid_TemplateRecipientBO.IsDirty) Then
                    Try
                        State.RecipientsGrid_TemplateRecipientBO.Save()
                    Catch ex As Exception
                        If Not State.RecipientsGrid_RecordNew Then
                            State.RecipientsGrid_TemplateRecipientBO.RejectChanges()
                        End If

                        Throw
                    End Try

                    State.RecipientsGrid_IsAfterSave = True
                    State.RecipientsGrid_TemplateRecipientBO.EndEdit()
                    State.RecipientsGrid_DV = Nothing
                    State.RecipientsGrid_RecordNew = False
                    State.RecipientsGrid_RecordEdit = False
                    RecipientsGrid_ReturnFromEditing()
                Else
                    AddInfoMsg(MSG_RECORD_NOT_SAVED)
                    RecipientsGrid_ReturnFromEditing()
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub RecipientsGrid_Delete()
            Dim templateRecipientBO = TheTemplate.GetRecipientChild(State.RecipientsGrid_TemplateRecipientId)

            Try
                templateRecipientBO.Delete()
                templateRecipientBO.Save()
                templateRecipientBO.EndEdit()

                templateRecipientBO = Nothing
                State.RecipientsGrid_DV = Nothing
            Catch ex As Exception
                TheTemplate.RejectChanges()
                Throw ex
            End Try

            RecipientsGrid_ReturnFromEditing()
        End Sub

        Protected Sub RecipientsGrid_CheckIfComingFromDeleteConfirm()
            Dim confResponse As String = HiddenDeletePromptResponse.Value
            If confResponse IsNot Nothing AndAlso confResponse = MSG_VALUE_YES Then
                If Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Delete Then
                    RecipientsGrid_Delete()
                    'Clean after consuming the action
                    State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
                    HiddenDeletePromptResponse.Value = ""
                End If
            ElseIf confResponse IsNot Nothing AndAlso confResponse = MSG_VALUE_NO Then
                RecipientsGrid_ReturnFromEditing()
                'Clean after consuming the action
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
                HiddenDeletePromptResponse.Value = ""
            End If
        End Sub
#End Region
#End Region
#End Region
    End Class
End Namespace
