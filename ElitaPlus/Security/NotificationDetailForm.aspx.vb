'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BOEditingWebFormCodeBehind.cst (9/16/2004)  ********************
Imports Assurant.Common.Zip.aZip
Imports Assurant.ElitaPlus.DALObjects
Imports Assurant.Common.Ftp
Imports Assurant.ElitaPlus.Common.MiscUtil
Imports Assurant.ElitaPlus.ElitaPlusWebApp.DownLoad
Imports System.IO
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms
Imports System.Threading

Partial Class NotificationDetailForm
    Inherits ElitaPlusPage

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
    Public Const URL As String = "NotificationDetailForm.aspx"

#End Region

#Region "Variables"

    Private mbIsFirstPass As Boolean = True
    Private Shared ListItemValue As Integer

    Private Const COL_ID_NAME As String = "id"
    Private Const COL_CODE_NAME As String = "code"
    Private Const COL_DESCRIPTION_NAME As String = "description"

#End Region

#Region "Page Return Type"
    Public Class ReturnType
        Public LastOperation As DetailPageCommand
        Public EditingBo As Notification
        Public HasDataChanged As Boolean
        Public Sub New(LastOp As DetailPageCommand, curEditingBo As Notification, hasDataChanged As Boolean)
            LastOperation = LastOp
            EditingBo = curEditingBo
            Me.HasDataChanged = hasDataChanged
        End Sub
    End Class
#End Region

#Region "Page State"

    Class MyState
        Public MyBO As Notification
        Public ScreenSnapShotBO As Notification
        Public IsNew As Boolean = False
        Public ActionInProgress As DetailPageCommand = DetailPageCommand.Nothing_
        Public LastErrMsg As String
        Public HasDataChanged As Boolean
        Public PageCalledFrom As String

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
                State.MyBO = New Notification(CType(CallingParameters, Guid))
            Else
                State.IsNew = True
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try

    End Sub

#End Region

#Region "Page Events"
    Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        If mbIsFirstPass = True Then
            mbIsFirstPass = False
        Else
            ' Do not load again the Page that was already loaded
            Return
        End If

        MasterPage.MessageController.Clear_Hide()

        Try
            If Not IsPostBack Then
                MasterPage.MessageController.Clear()
                MasterPage.UsePageTabTitleInBreadCrum = False
                MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("Tables")

                UpdateBreadCrum()

                MenuEnabled = False
                AddConfirmation(btnDelete_WRITE, Message.DELETE_RECORD_PROMPT)
                AddCalendarwithTime(ImageButtonBeginDate, txtBeginDate)
                AddCalendarwithTime(ImageButtonEndDate, txtEndDate)

                AddCalendarwithTime(ImageButtonOutageBeginDate, txtOutageBeginDate)
                AddCalendarwithTime(ImageButtonOutageEndDate, txtOutageEndDate)

                If State.MyBO Is Nothing Then
                    State.MyBO = New Notification
                End If

                PopulateDropdowns()

                If State.IsNew = True Then
                    CreateNew()
                End If

                PopulateFormFromBOs()
                EnableDisableFields()
            End If

            BindBoPropertiesToLabels()
            CheckIfComingFromSaveConfirm()

            If Not IsPostBack Then
                AddLabelDecorations(State.MyBO)
            End If

            'Me.cboRegulatoryReporting.Attributes.Add("onchange", "ShowHideRegulatoryExtractDate()")

        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            ' Clean Popup Input
            State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
            HiddenSaveChangesPromptResponse.Value = ""
            HandleErrors(ex, MasterPage.MessageController)
        End Try

        ShowMissingTranslations(MasterPage.MessageController)

    End Sub

#End Region

#Region "Controlling Logic"

    Protected Sub EnableDisableFields()
        'Enabled by Default
        'ControlMgr.SetEnableControl(Me, btnDelete_WRITE, False)
        ControlMgr.SetEnableControl(Me, btnNew_WRITE, True)
        ControlMgr.SetEnableControl(Me, btnCopy_WRITE, True)
        ControlMgr.SetEnableControl(Me, btnUndo_Write, False)
        'Now disable depending on the object state
        If State.MyBO.IsNew Then
            ControlMgr.SetEnableControl(Me, btnDelete_WRITE, False)
            ControlMgr.SetEnableControl(Me, btnNew_WRITE, True)
            ControlMgr.SetEnableControl(Me, btnCopy_WRITE, False)
            EnableDisableFieldsForNotActive(True)
        End If

        'WRITE YOU OWN CODE HERE
    End Sub

    Protected Sub EnableDisableFieldsForNotActive(blnActive As Boolean)


        'ControlMgr.SetEnableControl(Me, btnDelete_WRITE, blnActive)
        'ControlMgr.SetEnableControl(Me, btnNew_WRITE, blnActive)
        'ControlMgr.SetEnableControl(Me, btnCopy_WRITE, True)
        ControlMgr.SetEnableControl(Me, btnSave_WRITE, blnActive)

        ControlMgr.SetEnableControl(Me, ddlNotificationType, blnActive)
        ControlMgr.SetEnableControl(Me, ddlAudianceType, blnActive)
        ControlMgr.SetEnableControl(Me, txtBeginDate, blnActive)
        ControlMgr.SetEnableControl(Me, txtEndDate, blnActive)
        ControlMgr.SetEnableControl(Me, txtOutageBeginDate, blnActive)
        ControlMgr.SetEnableControl(Me, txtOutageEndDate, blnActive)
        ControlMgr.SetEnableControl(Me, txtNOTIFICATION_NAME, blnActive)
        ControlMgr.SetEnableControl(Me, txtNOTIFICATION_DETAILS, blnActive)
        ControlMgr.SetEnableControl(Me, txtContactInfo, blnActive)
        ControlMgr.SetEnableControl(Me, txtContactInfo, blnActive)

        ControlMgr.SetEnableControl(Me, ImageButtonBeginDate, blnActive)
        ControlMgr.SetEnableControl(Me, ImageButtonEndDate, blnActive)
        ControlMgr.SetEnableControl(Me, ImageButtonOutageBeginDate, blnActive)
        ControlMgr.SetEnableControl(Me, ImageButtonOutageEndDate, blnActive)

    End Sub

    Protected Sub BindBoPropertiesToLabels()

        BindBOPropertyToLabel(State.MyBO, "NotificationTypeId", lblNOTIFICATION_TYPE)
        BindBOPropertyToLabel(State.MyBO, "AudianceTypeId", lblAUDIANCE_TYPE)

        BindBOPropertyToLabel(State.MyBO, "NotificationBeginDate", lblBegin_date)
        BindBOPropertyToLabel(State.MyBO, "NotificationEndDate", lblEnd_date)

        BindBOPropertyToLabel(State.MyBO, "OutageEndDate", lblOutageEnd_date)
        BindBOPropertyToLabel(State.MyBO, "OutageBeginDate", lblOutageBegin_date)

        BindBOPropertyToLabel(State.MyBO, "NotificationName", lblNOTIFICATION_NAME)
        BindBOPropertyToLabel(State.MyBO, "NotificationDetails", lblNOTIFICATION_DETAILS)
        BindBOPropertyToLabel(State.MyBO, "SerialNo", lblSerialNo)
        BindBOPropertyToLabel(State.MyBO, "ContactInfo", lblContactInfo)

        ClearGridHeadersAndLabelsErrSign()
    End Sub

    Protected Sub PopulateDropdowns()
        Try
            Dim audianceList As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="AUDIANCE", languageCode:=Thread.CurrentPrincipal.GetLanguageCode())
            ddlAudianceType.Populate(audianceList, New PopulateOptions() With
                {
                    .AddBlankItem = True
                })

            'Me.BindListControlToDataView(Me.ddlAudianceType, LookupListNew.GetAudianceTypeLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId))

            Dim notificationList As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="NOTIFICATION", languageCode:=Thread.CurrentPrincipal.GetLanguageCode())
            ddlNotificationType.Populate(notificationList, New PopulateOptions() With
                {
                    .AddBlankItem = True
                })

            'Me.BindListControlToDataView(Me.ddlNotificationType, LookupListNew.GetSystemNotificationTypesLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId))

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub PopulateFormFromBOs()

        With State.MyBO

            SetSelectedItem(ddlNotificationType, .NotificationTypeId)
            SetSelectedItem(ddlAudianceType, .AudianceTypeId)

            PopulateControlFromBOProperty(txtBeginDate, .NotificationBeginDate)
            PopulateControlFromBOProperty(txtEndDate, .NotificationEndDate)

            PopulateControlFromBOProperty(txtOutageBeginDate, .OutageBeginDate)
            PopulateControlFromBOProperty(txtOutageEndDate, .OutageEndDate)

            PopulateControlFromBOProperty(txtNOTIFICATION_NAME, .NotificationName)
            PopulateControlFromBOProperty(txtNOTIFICATION_DETAILS, .NotificationDetails)
            PopulateControlFromBOProperty(txtSerialNo, .SerialNo)
            PopulateControlFromBOProperty(txtContactInfo, .ContactInfo)

            If .Enabled.ToUpper.Equals("Y") Then
                btnDelete_WRITE.Text = TranslationBase.TranslateLabelOrMessage("Disable")
                ControlMgr.SetEnableControl(Me, btnDelete_WRITE, True)
            Else
                ControlMgr.SetEnableControl(Me, btnDelete_WRITE, False)
                EnableDisableFieldsForNotActive(False)
            End If
        End With

    End Sub

    Protected Sub PopulateBOsFormFrom()
        With State.MyBO
            PopulateBOProperty(State.MyBO, "NotificationTypeId", ddlNotificationType)
            PopulateBOProperty(State.MyBO, "AudianceTypeId", ddlAudianceType)
            PopulateBOProperty(State.MyBO, "NotificationBeginDate", txtBeginDate)
            PopulateBOProperty(State.MyBO, "NotificationEndDate", txtEndDate)
            PopulateBOProperty(State.MyBO, "OutageBeginDate", txtOutageBeginDate)
            PopulateBOProperty(State.MyBO, "OutageEndDate", txtOutageEndDate)
            PopulateBOProperty(State.MyBO, "NotificationName", txtNOTIFICATION_NAME)
            PopulateBOProperty(State.MyBO, "NotificationDetails", txtNOTIFICATION_DETAILS)
            PopulateBOProperty(State.MyBO, "ContactInfo", txtContactInfo)

        End With
        If ErrCollection.Count > 0 Then
            Throw New PopulateBOErrorException
        End If
    End Sub

    Protected Sub CreateNew()
        State.ScreenSnapShotBO = Nothing 'Reset the backup copy
        State.MyBO = New Notification
        State.MyBO.Enabled = "Y"
        PopulateFormFromBOs()
        EnableDisableFields()
    End Sub

    Protected Sub CreateNewWithCopy()
        'Me.txtNOTIFICATION_NAME.Text = ""
        txtSerialNo.Text = ""
        State.MyBO = New Notification
        State.MyBO.Enabled = "Y"
        PopulateBOsFormFrom()
        EnableDisableFields()

        'create the backup copy
        State.ScreenSnapShotBO = New Notification
        State.ScreenSnapShotBO.Clone(State.MyBO)

    End Sub

    Protected Sub CheckIfComingFromSaveConfirm()
        Dim confResponse As String = HiddenSaveChangesPromptResponse.Value
        If confResponse IsNot Nothing AndAlso confResponse = MSG_VALUE_YES Then
            If State.ActionInProgress <> ElitaPlusPage.DetailPageCommand.BackOnErr Then
                State.MyBO.Save()
            End If
            Select Case State.ActionInProgress
                Case ElitaPlusPage.DetailPageCommand.Back
                    ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, State.MyBO, State.HasDataChanged))
                Case ElitaPlusPage.DetailPageCommand.New_
                    AddInfoMsg(Message.SAVE_RECORD_CONFIRMATION)
                    CreateNew()
                Case ElitaPlusPage.DetailPageCommand.NewAndCopy
                    AddInfoMsg(Message.SAVE_RECORD_CONFIRMATION)
                    CreateNewWithCopy()
                Case ElitaPlusPage.DetailPageCommand.BackOnErr
                    ReturnToCallingPage(New ReturnType(State.ActionInProgress, State.MyBO, State.HasDataChanged))
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
                    MasterPage.MessageController.AddErrorAndShow(State.LastErrMsg)
            End Select
        End If
        'Clean after consuming the action
        State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
        HiddenSaveChangesPromptResponse.Value = ""


    End Sub

    Public Sub ValidateDates()

        If txtBeginDate.Text.Trim() <> String.Empty Then
            GUIException.ValidateDate(lblBegin_date, txtBeginDate.Text)
        End If

        If txtEndDate.Text.Trim() <> String.Empty Then
            GUIException.ValidateDate(lblEnd_date, txtEndDate.Text)

            If txtBeginDate.Text.Trim() <> String.Empty AndAlso CDate(txtBeginDate.Text) > CDate(txtEndDate.Text) Then
                ElitaPlusPage.SetLabelError(lblBegin_date)
                ElitaPlusPage.SetLabelError(lblEnd_date)
                Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_BEGIN_END_DATE_ERR)
            End If
        End If

        lblBegin_date.ForeColor = lblAUDIANCE_TYPE.ForeColor
        lblEnd_date.ForeColor = lblAUDIANCE_TYPE.ForeColor



        If txtOutageBeginDate.Text.Trim() <> String.Empty Then
            GUIException.ValidateDate(lblOutageBegin_date, txtOutageBeginDate.Text)
        End If

        If txtOutageEndDate.Text.Trim() <> String.Empty Then
            GUIException.ValidateDate(lblOutageEnd_date, txtOutageEndDate.Text)

            If txtOutageBeginDate.Text.Trim() <> String.Empty AndAlso CDate(txtOutageBeginDate.Text) > CDate(txtOutageEndDate.Text) Then
                ElitaPlusPage.SetLabelError(lblOutageBegin_date)
                ElitaPlusPage.SetLabelError(lblOutageEnd_date)
                Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_BEGIN_END_DATE_ERR)
            End If
        End If

        lblOutageBegin_date.ForeColor = lblAUDIANCE_TYPE.ForeColor
        lblOutageEnd_date.ForeColor = lblAUDIANCE_TYPE.ForeColor


    End Sub


#End Region

#Region "Button Clicks"

    Private Sub btnBack_Click(sender As System.Object, e As System.EventArgs) Handles btnBack.Click
        Try
            PopulateBOsFormFrom()
            If State.MyBO.IsDirty Then
                DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
            Else
                ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, State.MyBO, State.HasDataChanged))
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
            DisplayMessage(Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
            'Me.AddConfirmMsg(Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR, Me.HiddenSaveChangesPromptResponse)
            State.ActionInProgress = ElitaPlusPage.DetailPageCommand.BackOnErr
            State.LastErrMsg = MasterPage.MessageController.Text
        End Try
    End Sub

    Private Sub btnSave_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnSave_WRITE.Click
        Try
            ValidateDates()
            PopulateBOsFormFrom()
            If State.MyBO.IsDirty Then
                State.MyBO.Save()
                PopulateControlFromBOProperty(txtSerialNo, State.MyBO.SerialNo)
                State.HasDataChanged = True
                EnableDisableFields()
                If State.MyBO.Enabled.Equals("N") Then
                    EnableDisableFieldsForNotActive(False)
                Else
                    EnableDisableFieldsForNotActive(True)
                End If

                MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION)
            Else
                MasterPage.MessageController.AddInformation(Message.MSG_RECORD_NOT_SAVED)
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnUndo_Write_Click(sender As System.Object, e As System.EventArgs) Handles btnUndo_Write.Click
        Try
            If Not State.MyBO.IsNew Then
                'Reload from the DB
                State.MyBO = New Notification(State.MyBO.Id)
            ElseIf State.ScreenSnapShotBO IsNot Nothing Then
                'It was a new with copy
                State.MyBO.Clone(State.ScreenSnapShotBO)
            Else
                State.MyBO = New Notification
            End If
            PopulateFormFromBOs()
            EnableDisableFields()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnDelete_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnDelete_WRITE.Click
        Try

            State.MyBO.Enabled = "N"
            State.MyBO.OutageEndDate = Date.Now
            State.MyBO.Save()
            State.HasDataChanged = True
            ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Delete, State.MyBO, State.HasDataChanged))
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            'undo the delete
            State.MyBO.RejectChanges()
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnNew_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnNew_WRITE.Click
        Try
            PopulateBOsFormFrom()
            If State.MyBO.IsDirty Then
                DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.New_
            Else
                CreateNew()
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnCopy_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnCopy_WRITE.Click
        Try
            PopulateBOsFormFrom()
            If State.MyBO.IsDirty Then
                DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.NewAndCopy
            Else
                CreateNewWithCopy()
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub


#End Region

#Region "Page Control Events"

#End Region

#Region "Error Handling"

#End Region

#Region "Ajax related"
    Private Shared ReadOnly Property AjaxState() As MyState
        Get
            Return CType(NavPage.ClientNavigator.PageState, MyState)
        End Get
    End Property

    <System.Web.Services.WebMethod()> _
    <Script.Services.ScriptMethod()> _
    Public Shared Function PopulateServiceCenterDrop(prefixText As String, count As Integer) As String()
        Dim dv As DataView = LookupListNew.GetServiceCenterLookupList(AjaxState.MyBO.Id)
        Return AjaxController.BindAutoComplete(prefixText, dv)
    End Function
#End Region

    Private Sub UpdateBreadCrum()
        If (State IsNot Nothing) Then
            MasterPage.BreadCrum = MasterPage.PageTab & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage("SYSTEM_NOTIFICATION_DETAIL")
            MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("SYSTEM_NOTIFICATION_DETAIL")
        End If
    End Sub

End Class


