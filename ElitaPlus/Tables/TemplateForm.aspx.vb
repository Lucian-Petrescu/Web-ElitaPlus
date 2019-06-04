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
            If Me.State.TemplateId.Equals(Guid.Empty) Then
                Me.State.IsTemplateNew = True
                BindBoPropertiesToLabels()
                Me.AddLabelDecorations(TheTemplate)
                ClearAll()
                PopulateAll()
            Else
                Me.State.IsTemplateNew = False
                BindBoPropertiesToLabels()
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
            Public Sub New(ByVal templateId As Guid, ByVal templateGroupId As Guid)
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
                    Me.AddCalendar_New(Me.btnEffectiveDate, Me.txtEffectiveDate)
                    Me.AddCalendar_New(Me.btnExpirationDate, Me.txtExpirationDate)

                    Me.MasterPage.MessageController.Clear()
                    Me.MasterPage.UsePageTabTitleInBreadCrum = False
                    Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("Tables")

                    TranslateGridHeader(ParametersGrid)
                    TranslateGridHeader(RecipientsGrid)
                    UpdateBreadCrum()

                    Me.SetStateProperties()
                    Me.AddControlMsg(Me.btnDelete_WRITE, Message.DELETE_RECORD_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, True)

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

                BindBoPropertiesToLabels()
                ParametersGrid_BindBoPropertiesToHeaders()
                RecipientsGrid_BindBoPropertiesToHeaders()
                CheckIfComingFromConfirm()

                If Not Me.IsPostBack Then
                    Me.AddLabelDecorations(TheTemplate)
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
                Dim callObj As TemplateForm.CallType = CType(CallingPar, TemplateForm.CallType)

                If callObj Is Nothing Then
                    Throw New ArgumentNullException()
                End If

                If callObj.TemplateGroupId <> Guid.Empty Then
                    Me.State.TemplateGroupId = callObj.TemplateGroupId
                Else
                    Throw New ArgumentException()
                End If

                If callObj.TemplateId <> Guid.Empty Then
                    'Get the id from the parent
                    Me.State.TemplateId = callObj.TemplateId
                Else
                    Me.State.IsTemplateNew = True
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
            ApplyChanges()
        End Sub

        Private Sub GoBack()
            Dim retType As New TemplateForm.ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.TemplateId, Me.State.boChanged)
            Me.ReturnToCallingPage(retType)
        End Sub

        Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
            Try
                If IsDirtyBO() = True Then
                    Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                    Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
                Else
                    GoBack()
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnUndo_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUndo_WRITE.Click
            Try
                If Not Me.State.IsTemplateNew Then
                    'Reload from the DB
                    Me.State.Template = New OcTemplate(Me.State.TemplateId)
                ElseIf Not Me.State.ScreenSnapShotBO Is Nothing Then
                    'It was a new with copy
                    Me.State.Template.Clone(Me.State.ScreenSnapShotBO)
                Else
                    Me.State.Template = New OcTemplate
                End If

                PopulateAll()
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
            ClearAll()
            Me.SetButtonsState(True)
            Me.DisabledTabsList.Clear()
            Me.PopulateAll()

            If Me.State.IsTemplateNew Then
                ControlMgr.SetEnableControl(Me, btnDelete_WRITE, False)
                ControlMgr.SetEnableControl(Me, btnNew_WRITE, False)
                ControlMgr.SetEnableControl(Me, btnCopy_WRITE, False)
            End If
        End Sub

        Private Sub btnNew_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew_WRITE.Click
            Try
                If IsDirtyBO() = True Then
                    Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                    Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.New_
                Else
                    CreateNew()
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
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

            Me.SetButtonsState(True)
            Me.DisabledTabsList.Clear()

            'create the backup copy
            Me.State.ScreenSnapShotBO = New OcTemplate
            Me.State.ScreenSnapShotBO.Copy(TheTemplate)
        End Sub

        Private Sub btnCopy_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCopy_WRITE.Click
            Try
                If IsDirtyBO() = True Then
                    Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                    Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.NewAndCopy
                Else
                    CreateNewCopy()
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
                Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.NewAndCopy
            End Try
        End Sub

        Private Sub btnDelete_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete_WRITE.Click
            Try
                If DeleteTemplate() = True Then
                    Me.State.boChanged = True
                    Dim retType As New TemplateForm.ReturnType(ElitaPlusPage.DetailPageCommand.Delete, Me.State.TemplateId, True)
                    Me.ReturnToCallingPage(retType)
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
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
            If (Not Me.State Is Nothing) Then
                If (Not Me.State Is Nothing) Then
                    Me.MasterPage.BreadCrum = Me.MasterPage.PageTab & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage("TEMPLATE_DETAIL")
                    Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("TEMPLATE_DETAIL")
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
                Me.MasterPage.MessageController.AddError(TEMPLATE_FORM001)
                Me.MasterPage.MessageController.AddError(ex.Message, False)
                Me.MasterPage.MessageController.Show()
            End Try
        End Sub

        Private Sub PopulateTexts()
            Try
                Dim oLanguageId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId

                With TheTemplate
                    If Me.State.IsTemplateNew = True Then
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

                        Me.PopulateControlFromBOProperty(Me.txtTemplateCode, .TemplateCode)
                        Me.PopulateControlFromBOProperty(Me.txtTemplateDescription, .Description)
                        Me.PopulateControlFromBOProperty(Me.txtEffectiveDate, .EffectiveDate)
                        Me.PopulateControlFromBOProperty(Me.txtExpirationDate, .ExpirationDate)

                        SetSelectedItem(ddlTemplateType, .TemplateTypeXcd)
                        PopulateControlFromBOProperty(txtSMSAppKey, .SmsAppKey)
                        PopulateControlFromBOProperty(txtSMSShortCode, .SmsShortCode)
                        PopulateControlFromBOProperty(txtSMSTriggerId, .SmsTriggerId)
                    End If
                End With
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub PopulateAll()
            If Me.State.IsTemplateNew = True Then
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
            With Me.TheTemplate
                Me.PopulateBOProperty(TheTemplate, TEMPLATECODE_PROPERTY, Me.txtTemplateCode)
                Me.PopulateBOProperty(TheTemplate, DESCRIPTION_PROPERTY, Me.txtTemplateDescription)

                Me.PopulateBOProperty(TheTemplate, HASCUSTOMIZEDPARAMSXCD_PROPERTY, cboHasCustomizedParams.SelectedValue)
                Me.PopulateBOProperty(TheTemplate, ALLOWMANUALUSEXCD_PROPERTY, cboAllowManualUse.SelectedValue)
                Me.PopulateBOProperty(TheTemplate, ALLOWMANUALRESENDXCD_PROPERTY, cboAllowManualResend.SelectedValue)

                Me.PopulateBOProperty(TheTemplate, EFFECTIVEDATE_PROPERTY, Me.txtEffectiveDate)
                Me.PopulateBOProperty(TheTemplate, EXPIRATIONDATE_PROPERTY, Me.txtExpirationDate)

                PopulateBOProperty(TheTemplate, "TemplateTypeXcd", ddlTemplateType.SelectedValue)

                If(TheTemplate.TemplateTypeXcd <> "OC_TEMP_TYPE-SMS") Then
                    ClearTextsSMS 'clear the SMS configuration if template type is not SMS
                End If

                PopulateBOProperty(TheTemplate, "SmsAppKey", txtSMSAppKey)
                PopulateBOProperty(TheTemplate, "SmsShortCode", txtSMSShortCode)
                PopulateBOProperty(TheTemplate, "SmsTriggerId", txtSMSTriggerId)
                
            End With

            If Me.ErrCollection.Count > 0 Then
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
        Private Sub SetButtonsState(ByVal bIsNew As Boolean)
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
                Dim errors As List(Of ValidationError) = New List(Of ValidationError)

                If errors.Count > 0 Then
                    Throw New BOValidationException(errors.ToArray, GetType(OcTemplate).FullName)
                End If

                SetLabelColor(Me.lblTemplateCode)
                SetLabelColor(Me.lblTemplateDescription)
                SetLabelColor(Me.lblHasCustomizedParams)
                SetLabelColor(Me.lblAllowManualUse)
                SetLabelColor(Me.lblAllowManualResend)
                SetLabelColor(Me.lblEffectiveDate)
                SetLabelColor(Me.lblExpirationDate)

                SetLabelColor(lblTemplateCode)
                SetLabelColor(lblSMSAppKey)
                SetLabelColor(Me.lblSMSTriggerId)
                SetLabelColor(Me.lblSMSShortCode)

                If TheTemplate.IsDirty OrElse TheTemplate.IsChildrenDirty Then
                    If Me.State.IsTemplateNew = True Then
                        Me.TheTemplate.OcTemplateGroupId = Me.State.TemplateGroupId
                    End If

                    Me.TheTemplate.Save()

                    'Save reloads the object from the DB, therefore the children collection references
                    'are cleared from the underlying data structure (row). So we need to reload the children collections and
                    'to achieve this, simply clear the corresponding DataViews to force a reload upon populating the grids.
                    Me.State.ParametersGrid_DV = Nothing
                    Me.State.RecipientsGrid_DV = Nothing

                    Me.State.boChanged = True

                    If Me.State.IsTemplateNew = True Then
                        Me.State.IsTemplateNew = False
                        DisabledTabsList.Clear()
                    End If

                    PopulateAll()
                    Me.SetButtonsState(Me.State.IsTemplateNew)
                    Me.MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION)
                Else
                    Me.MasterPage.MessageController.AddInformation(Message.MSG_RECORD_NOT_SAVED)
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Function

        Private Function DeleteTemplate() As Boolean
            Dim bIsOk As Boolean = True

            Try
                With TheTemplate
                    PopulateBOsFromForm()

                    'Check if there are any elp_oc_message entities associated with this template.
                    If .GetAssociatedMessageCount(.Id) > 0 Then
                        Me.MasterPage.MessageController.AddError(TranslationBase.TranslateLabelOrMessage(Assurant.ElitaPlus.Common.ErrorCodes.ERR_DELETE_TEMPLATE_WITH_MESSAGES_PRESENT, ElitaPlusIdentity.Current.ActiveUser.LanguageId))
                        .cancelEdit()
                        bIsOk = False
                    Else
                        'When children collections are loaded correctly, delete with delete the children first, then the object.
                        .Delete()
                        .Save()
                    End If
                End With
            Catch ex As Exception
                Me.MasterPage.MessageController.AddError(TranslationBase.TranslateLabelOrMessage(TEMPLATE_FORM002, ElitaPlusIdentity.Current.ActiveUser.LanguageId))
                Me.MasterPage.MessageController.AddError(ex.Message, False)
                Me.MasterPage.MessageController.Show()
                bIsOk = False
            End Try
            Return bIsOk
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
                            Me.State.boChanged = True
                            GoBack()
                        End If
                    Case MSG_VALUE_NO
                        GoBack()
                End Select
            End If
        End Sub

        Protected Sub ComingFromNewCopy()
            Dim confResponse As String = Me.HiddenSaveChangesPromptResponse.Value

            If Not confResponse = String.Empty Then
                ' Return from the New Copy Button
                Select Case confResponse
                    Case MSG_VALUE_YES
                        ' Save and create a new Copy BO
                        If ApplyChanges() = True Then
                            Me.State.boChanged = True
                            CreateNewCopy()
                        End If
                    Case MSG_VALUE_NO
                        ' create a new BO
                        CreateNewCopy()
                End Select
            End If
        End Sub
        Protected Sub ComingFromNew()
            Dim confResponse As String = Me.HiddenSaveChangesPromptResponse.Value

            If Not confResponse = String.Empty Then
                ' Return from the New Copy Button
                Select Case confResponse
                    Case MSG_VALUE_YES
                        ' Save and create a new Copy BO
                        If ApplyChanges() = True Then
                            Me.State.boChanged = True
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
                Select Case Me.State.ActionInProgress
                    ' Period
                    Case ElitaPlusPage.DetailPageCommand.Back
                        ComingFromBack()
                    Case ElitaPlusPage.DetailPageCommand.New_
                        ComingFromNew()
                    Case ElitaPlusPage.DetailPageCommand.NewAndCopy
                        ComingFromNewCopy()
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
        Private Sub BindBoPropertiesToLabels()
            Me.BindBOPropertyToLabel(TheTemplate, TEMPLATECODE_PROPERTY, lblTemplateCode)
            Me.BindBOPropertyToLabel(TheTemplate, DESCRIPTION_PROPERTY, lblTemplateDescription)
            Me.BindBOPropertyToLabel(TheTemplate, HASCUSTOMIZEDPARAMSXCD_PROPERTY, lblHasCustomizedParams)
            Me.BindBOPropertyToLabel(TheTemplate, ALLOWMANUALUSEXCD_PROPERTY, lblAllowManualUse)
            Me.BindBOPropertyToLabel(TheTemplate, ALLOWMANUALRESENDXCD_PROPERTY, lblAllowManualResend)
            Me.BindBOPropertyToLabel(TheTemplate, EFFECTIVEDATE_PROPERTY, lblEffectiveDate)
            Me.BindBOPropertyToLabel(TheTemplate, EXPIRATIONDATE_PROPERTY, lblExpirationDate)

            Me.BindBOPropertyToLabel(TheTemplate, "TemplateCode", lblTemplateCode)
            Me.BindBOPropertyToLabel(TheTemplate, "SmsAppKey", lblSMSAppKey)
            Me.BindBOPropertyToLabel(TheTemplate, "SmsShortCode", lblSMSShortCode)
            Me.BindBOPropertyToLabel(TheTemplate, "SmsTriggerId", lblSMSTriggerId)
        End Sub

        Private Sub ClearLabelsErrSign()
            Me.ClearLabelErrSign(lblTemplateCode)
            Me.ClearLabelErrSign(lblTemplateDescription)
            Me.ClearLabelErrSign(lblHasCustomizedParams)
            Me.ClearLabelErrSign(lblAllowManualUse)
            Me.ClearLabelErrSign(lblAllowManualResend)
            Me.ClearLabelErrSign(lblEffectiveDate)
            Me.ClearLabelErrSign(lblExpirationDate)

            ClearLabelErrSign(lblTemplateCode)
            ClearLabelErrSign(lblSMSAppKey)
            ClearLabelErrSign(lblSMSShortCode)
            ClearLabelErrSign(lblSMSTriggerId)
        End Sub

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

                            Me.BindCodeToListControl(CType(e.Row.Cells(Me.GRID_PARAM_COL_PARAM_DATA_TYPE_XCD).FindControl(Me.GRID_PARAM_COL_PARAM_DATA_TYPE_XCD_DROPDOWNLIST_CONTROL_NAME), DropDownList),
                                                       LookupListNew.GetParamDataTypeLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), , , True)

                            Me.BindCodeToListControl(CType(e.Row.Cells(Me.GRID_PARAM_COL_ALLOW_EMPTY_VALUE_XCD).FindControl(Me.GRID_PARAM_COL_ALLOW_EMPTY_VALUE_XCD_DROPDOWNLIST_CONTROL_NAME), DropDownList),
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
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub ParametersGrid_Populate()
            Dim oDataView As DataView

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

                    Dim txtParamName As TextBox = CType(Me.ParametersGrid.Rows(gridRowIdx).Cells(GRID_PARAM_COL_PARAM_NAME).FindControl(GRID_PARAM_COL_PARAM_NAME_TEXTBOX_CONTROL_NAME), TextBox)
                    Me.PopulateControlFromBOProperty(txtParamName, .ParamName)

                    Dim txtDateFormatString As TextBox = CType(Me.ParametersGrid.Rows(gridRowIdx).Cells(GRID_PARAM_COL_DATE_FORMAT_STRING).FindControl(GRID_PARAM_COL_DATE_FORMAT_STRING_TEXTBOX_CONTROL_NAME), TextBox)
                    Me.PopulateControlFromBOProperty(txtDateFormatString, .DateFormatString)

                    CType(Me.ParametersGrid.Rows(gridRowIdx).Cells(GRID_PARAM_COL_OC_TEMPLATE_PARAMS_ID).FindControl(ID_CONTROL_NAME), Label).Text = .Id.ToString
                End With
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub ParametersGrid_SetButtonsState(ByVal bIsEdit As Boolean)
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
            Dim cboParamDataType As DropDownList = CType(Me.ParametersGrid.Rows(Me.ParametersGrid.EditIndex).Cells(GRID_PARAM_COL_PARAM_DATA_TYPE_XCD).FindControl(Me.GRID_PARAM_COL_PARAM_DATA_TYPE_XCD_DROPDOWNLIST_CONTROL_NAME), DropDownList)
            Dim txtDateFormatString As TextBox = CType(Me.ParametersGrid.Rows(Me.ParametersGrid.EditIndex).Cells(GRID_PARAM_COL_DATE_FORMAT_STRING).FindControl(Me.GRID_PARAM_COL_DATE_FORMAT_STRING_TEXTBOX_CONTROL_NAME), TextBox)
            Dim cboAllowEmptyValue As DropDownList = CType(Me.ParametersGrid.Rows(Me.ParametersGrid.EditIndex).Cells(GRID_PARAM_COL_ALLOW_EMPTY_VALUE_XCD).FindControl(Me.GRID_PARAM_COL_ALLOW_EMPTY_VALUE_XCD_DROPDOWNLIST_CONTROL_NAME), DropDownList)

            PopulateBOProperty(Me.State.ParametersGrid_TemplateParamsBO, "OcTemplateId", TheTemplate.Id)
            PopulateBOProperty(Me.State.ParametersGrid_TemplateParamsBO, "ParamName", txtParamName)
            PopulateBOProperty(Me.State.ParametersGrid_TemplateParamsBO, "ParamValueSourceXcd", cboParamValueSource, False, True)
            PopulateBOProperty(Me.State.ParametersGrid_TemplateParamsBO, "ParamValueSourceDescription", cboParamValueSource, False, False)
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

                    RecipientsGrid_SetFocusOnEditableField(Me.RecipientsGrid, GRID_RECIPIENT_COL_DESCRIPTION, GRID_RECIPIENT_COL_DESCRIPTION_TEXTBOX_CONTROL_NAME, index)
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
                            CType(e.Row.Cells(GRID_RECIPIENT_COL_DESCRIPTION).FindControl(GRID_RECIPIENT_COL_DESCRIPTION_TEXTBOX_CONTROL_NAME), TextBox).Text = dvRow(OcTemplateRecipient.TemplateRecipientsDV.COL_DESCRIPTION).ToString

                            Me.BindCodeToListControl(CType(e.Row.Cells(Me.GRID_RECIPIENT_COL_RECIPIENT_SOURCE_FIELD_XCD).FindControl(Me.GRID_RECIPIENT_COL_RECIPIENT_SOURCE_FIELD_XCD_DROPDOWNLIST_CONTROL_NAME), DropDownList),
                                                       LookupListNew.GetRecipientSourceFieldLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), , , True)
                        Else
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
            Dim oDataView As DataView

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
                            'Me.PopulateControlFromBOProperty(cboRecipientSourceField, .RecipientSourceFieldXcd)
                            SetSelectedItem(cboRecipientSourceField, .RecipientSourceFieldXcd)
                        End If
                    End If

                    Dim txtDescription As TextBox = CType(Me.RecipientsGrid.Rows(gridRowIdx).Cells(GRID_RECIPIENT_COL_DESCRIPTION).FindControl(GRID_RECIPIENT_COL_DESCRIPTION_TEXTBOX_CONTROL_NAME), TextBox)
                    Me.PopulateControlFromBOProperty(txtDescription, .Description)

                    CType(Me.RecipientsGrid.Rows(gridRowIdx).Cells(GRID_RECIPIENT_COL_OC_TEMPLATE_RECIPIENT_ID).FindControl(ID_CONTROL_NAME), Label).Text = .Id.ToString
                End With
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub RecipientsGrid_SetButtonsState(ByVal bIsEdit As Boolean)
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
            Dim txtDescription As TextBox = CType(Me.RecipientsGrid.Rows(Me.RecipientsGrid.EditIndex).Cells(GRID_RECIPIENT_COL_DESCRIPTION).FindControl(Me.GRID_RECIPIENT_COL_DESCRIPTION_TEXTBOX_CONTROL_NAME), TextBox)
            Dim cboRecipientSourceField As DropDownList = CType(Me.RecipientsGrid.Rows(Me.RecipientsGrid.EditIndex).Cells(GRID_RECIPIENT_COL_RECIPIENT_SOURCE_FIELD_XCD).FindControl(Me.GRID_RECIPIENT_COL_RECIPIENT_SOURCE_FIELD_XCD_DROPDOWNLIST_CONTROL_NAME), DropDownList)

            PopulateBOProperty(Me.State.RecipientsGrid_TemplateRecipientBO, "OcTemplateId", TheTemplate.Id)
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
