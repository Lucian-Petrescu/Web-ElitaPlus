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

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
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
        Public Sub New(ByVal LastOp As DetailPageCommand, ByVal curEditingBo As Notification, ByVal hasDataChanged As Boolean)
            Me.LastOperation = LastOp
            Me.EditingBo = curEditingBo
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

    Private Sub Page_PageCall(ByVal CallFromUrl As String, ByVal CallingPar As Object) Handles MyBase.PageCall
        Try
            If Not Me.CallingParameters Is Nothing Then
                'Get the id from the parent
                Me.State.MyBO = New Notification(CType(Me.CallingParameters, Guid))
            Else
                Me.State.IsNew = True
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try

    End Sub

#End Region

#Region "Page Events"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        If mbIsFirstPass = True Then
            mbIsFirstPass = False
        Else
            ' Do not load again the Page that was already loaded
            Return
        End If

        Me.MasterPage.MessageController.Clear_Hide()

        Try
            If Not Me.IsPostBack Then
                Me.MasterPage.MessageController.Clear()
                Me.MasterPage.UsePageTabTitleInBreadCrum = False
                Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("Tables")

                UpdateBreadCrum()

                Me.MenuEnabled = False
                Me.AddConfirmation(Me.btnDelete_WRITE, Message.DELETE_RECORD_PROMPT)
                Me.AddCalendarwithTime(Me.ImageButtonBeginDate, Me.txtBeginDate)
                Me.AddCalendarwithTime(Me.ImageButtonEndDate, Me.txtEndDate)

                Me.AddCalendarwithTime(Me.ImageButtonOutageBeginDate, Me.txtOutageBeginDate)
                Me.AddCalendarwithTime(Me.ImageButtonOutageEndDate, Me.txtOutageEndDate)

                If Me.State.MyBO Is Nothing Then
                    Me.State.MyBO = New Notification
                End If

                PopulateDropdowns()

                If Me.State.IsNew = True Then
                    CreateNew()
                End If

                Me.PopulateFormFromBOs()
                Me.EnableDisableFields()
            End If

            BindBoPropertiesToLabels()
            CheckIfComingFromSaveConfirm()

            If Not Me.IsPostBack Then
                Me.AddLabelDecorations(Me.State.MyBO)
            End If

            'Me.cboRegulatoryReporting.Attributes.Add("onchange", "ShowHideRegulatoryExtractDate()")

        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            ' Clean Popup Input
            Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
            Me.HiddenSaveChangesPromptResponse.Value = ""
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try

        Me.ShowMissingTranslations(Me.MasterPage.MessageController)

    End Sub

#End Region

#Region "Controlling Logic"

    Protected Sub EnableDisableFields()
        'Enabled by Default
        'ControlMgr.SetEnableControl(Me, btnDelete_WRITE, False)
        ControlMgr.SetEnableControl(Me, btnNew_WRITE, True)
        ControlMgr.SetEnableControl(Me, btnCopy_WRITE, True)
        ControlMgr.SetEnableControl(Me, Me.btnUndo_Write, False)
        'Now disable depending on the object state
        If Me.State.MyBO.IsNew Then
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

        ControlMgr.SetEnableControl(Me, Me.ddlNotificationType, blnActive)
        ControlMgr.SetEnableControl(Me, Me.ddlAudianceType, blnActive)
        ControlMgr.SetEnableControl(Me, Me.txtBeginDate, blnActive)
        ControlMgr.SetEnableControl(Me, Me.txtEndDate, blnActive)
        ControlMgr.SetEnableControl(Me, Me.txtOutageBeginDate, blnActive)
        ControlMgr.SetEnableControl(Me, Me.txtOutageEndDate, blnActive)
        ControlMgr.SetEnableControl(Me, Me.txtNOTIFICATION_NAME, blnActive)
        ControlMgr.SetEnableControl(Me, Me.txtNOTIFICATION_DETAILS, blnActive)
        ControlMgr.SetEnableControl(Me, Me.txtContactInfo, blnActive)
        ControlMgr.SetEnableControl(Me, Me.txtContactInfo, blnActive)

        ControlMgr.SetEnableControl(Me, Me.ImageButtonBeginDate, blnActive)
        ControlMgr.SetEnableControl(Me, Me.ImageButtonEndDate, blnActive)
        ControlMgr.SetEnableControl(Me, Me.ImageButtonOutageBeginDate, blnActive)
        ControlMgr.SetEnableControl(Me, Me.ImageButtonOutageEndDate, blnActive)

    End Sub

    Protected Sub BindBoPropertiesToLabels()

        Me.BindBOPropertyToLabel(Me.State.MyBO, "NotificationTypeId", Me.lblNOTIFICATION_TYPE)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "AudianceTypeId", Me.lblAUDIANCE_TYPE)

        Me.BindBOPropertyToLabel(Me.State.MyBO, "NotificationBeginDate", Me.lblBegin_date)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "NotificationEndDate", Me.lblEnd_date)

        Me.BindBOPropertyToLabel(Me.State.MyBO, "OutageEndDate", Me.lblOutageEnd_date)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "OutageBeginDate", Me.lblOutageBegin_date)

        Me.BindBOPropertyToLabel(Me.State.MyBO, "NotificationName", Me.lblNOTIFICATION_NAME)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "NotificationDetails", Me.lblNOTIFICATION_DETAILS)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "SerialNo", Me.lblSerialNo)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "ContactInfo", Me.lblContactInfo)

        Me.ClearGridHeadersAndLabelsErrSign()
    End Sub

    Protected Sub PopulateDropdowns()
        Try
            Dim audianceList As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="AUDIANCE", languageCode:=Thread.CurrentPrincipal.GetLanguageCode())
            Me.ddlAudianceType.Populate(audianceList, New PopulateOptions() With
                {
                    .AddBlankItem = True
                })

            'Me.BindListControlToDataView(Me.ddlAudianceType, LookupListNew.GetAudianceTypeLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId))

            Dim notificationList As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="NOTIFICATION", languageCode:=Thread.CurrentPrincipal.GetLanguageCode())
            Me.ddlNotificationType.Populate(notificationList, New PopulateOptions() With
                {
                    .AddBlankItem = True
                })

            'Me.BindListControlToDataView(Me.ddlNotificationType, LookupListNew.GetSystemNotificationTypesLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId))

        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub PopulateFormFromBOs()

        With Me.State.MyBO

            Me.SetSelectedItem(Me.ddlNotificationType, .NotificationTypeId)
            Me.SetSelectedItem(Me.ddlAudianceType, .AudianceTypeId)

            Me.PopulateControlFromBOProperty(Me.txtBeginDate, .NotificationBeginDate)
            Me.PopulateControlFromBOProperty(Me.txtEndDate, .NotificationEndDate)

            Me.PopulateControlFromBOProperty(Me.txtOutageBeginDate, .OutageBeginDate)
            Me.PopulateControlFromBOProperty(Me.txtOutageEndDate, .OutageEndDate)

            Me.PopulateControlFromBOProperty(Me.txtNOTIFICATION_NAME, .NotificationName)
            Me.PopulateControlFromBOProperty(Me.txtNOTIFICATION_DETAILS, .NotificationDetails)
            Me.PopulateControlFromBOProperty(Me.txtSerialNo, .SerialNo)
            Me.PopulateControlFromBOProperty(Me.txtContactInfo, .ContactInfo)

            If .Enabled.ToUpper.Equals("Y") Then
                Me.btnDelete_WRITE.Text = TranslationBase.TranslateLabelOrMessage("Disable")
                ControlMgr.SetEnableControl(Me, btnDelete_WRITE, True)
            Else
                ControlMgr.SetEnableControl(Me, btnDelete_WRITE, False)
                EnableDisableFieldsForNotActive(False)
            End If
        End With

    End Sub

    Protected Sub PopulateBOsFormFrom()
        With Me.State.MyBO
            Me.PopulateBOProperty(Me.State.MyBO, "NotificationTypeId", Me.ddlNotificationType)
            Me.PopulateBOProperty(Me.State.MyBO, "AudianceTypeId", Me.ddlAudianceType)
            Me.PopulateBOProperty(Me.State.MyBO, "NotificationBeginDate", Me.txtBeginDate)
            Me.PopulateBOProperty(Me.State.MyBO, "NotificationEndDate", Me.txtEndDate)
            Me.PopulateBOProperty(Me.State.MyBO, "OutageBeginDate", Me.txtOutageBeginDate)
            Me.PopulateBOProperty(Me.State.MyBO, "OutageEndDate", Me.txtOutageEndDate)
            Me.PopulateBOProperty(Me.State.MyBO, "NotificationName", Me.txtNOTIFICATION_NAME)
            Me.PopulateBOProperty(Me.State.MyBO, "NotificationDetails", Me.txtNOTIFICATION_DETAILS)
            Me.PopulateBOProperty(Me.State.MyBO, "ContactInfo", Me.txtContactInfo)

        End With
        If Me.ErrCollection.Count > 0 Then
            Throw New PopulateBOErrorException
        End If
    End Sub

    Protected Sub CreateNew()
        Me.State.ScreenSnapShotBO = Nothing 'Reset the backup copy
        Me.State.MyBO = New Notification
        Me.State.MyBO.Enabled = "Y"
        Me.PopulateFormFromBOs()
        Me.EnableDisableFields()
    End Sub

    Protected Sub CreateNewWithCopy()
        'Me.txtNOTIFICATION_NAME.Text = ""
        Me.txtSerialNo.Text = ""
        Me.State.MyBO = New Notification
        Me.State.MyBO.Enabled = "Y"
        Me.PopulateBOsFormFrom()
        Me.EnableDisableFields()

        'create the backup copy
        Me.State.ScreenSnapShotBO = New Notification
        Me.State.ScreenSnapShotBO.Clone(Me.State.MyBO)

    End Sub

    Protected Sub CheckIfComingFromSaveConfirm()
        Dim confResponse As String = Me.HiddenSaveChangesPromptResponse.Value
        If Not confResponse Is Nothing AndAlso confResponse = Me.MSG_VALUE_YES Then
            If Me.State.ActionInProgress <> ElitaPlusPage.DetailPageCommand.BackOnErr Then
                Me.State.MyBO.Save()
            End If
            Select Case Me.State.ActionInProgress
                Case ElitaPlusPage.DetailPageCommand.Back
                    Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.MyBO, Me.State.HasDataChanged))
                Case ElitaPlusPage.DetailPageCommand.New_
                    Me.AddInfoMsg(Message.SAVE_RECORD_CONFIRMATION)
                    Me.CreateNew()
                Case ElitaPlusPage.DetailPageCommand.NewAndCopy
                    Me.AddInfoMsg(Message.SAVE_RECORD_CONFIRMATION)
                    Me.CreateNewWithCopy()
                Case ElitaPlusPage.DetailPageCommand.BackOnErr
                    Me.ReturnToCallingPage(New ReturnType(Me.State.ActionInProgress, Me.State.MyBO, Me.State.HasDataChanged))
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
                    Me.MasterPage.MessageController.AddErrorAndShow(Me.State.LastErrMsg)
            End Select
        End If
        'Clean after consuming the action
        Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
        Me.HiddenSaveChangesPromptResponse.Value = ""


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

        Me.lblBegin_date.ForeColor = Me.lblAUDIANCE_TYPE.ForeColor
        Me.lblEnd_date.ForeColor = Me.lblAUDIANCE_TYPE.ForeColor



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

        Me.lblOutageBegin_date.ForeColor = Me.lblAUDIANCE_TYPE.ForeColor
        Me.lblOutageEnd_date.ForeColor = Me.lblAUDIANCE_TYPE.ForeColor


    End Sub


#End Region

#Region "Button Clicks"

    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Try
            Me.PopulateBOsFormFrom()
            If Me.State.MyBO.IsDirty Then
                Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
            Else
                Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.MyBO, Me.State.HasDataChanged))
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
            Me.DisplayMessage(Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
            'Me.AddConfirmMsg(Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR, Me.HiddenSaveChangesPromptResponse)
            Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.BackOnErr
            Me.State.LastErrMsg = Me.MasterPage.MessageController.Text
        End Try
    End Sub

    Private Sub btnSave_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave_WRITE.Click
        Try
            ValidateDates()
            Me.PopulateBOsFormFrom()
            If Me.State.MyBO.IsDirty Then
                Me.State.MyBO.Save()
                Me.PopulateControlFromBOProperty(Me.txtSerialNo, Me.State.MyBO.SerialNo)
                Me.State.HasDataChanged = True
                Me.EnableDisableFields()
                If Me.State.MyBO.Enabled.Equals("N") Then
                    EnableDisableFieldsForNotActive(False)
                Else
                    EnableDisableFieldsForNotActive(True)
                End If

                Me.MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION)
            Else
                Me.MasterPage.MessageController.AddInformation(Message.MSG_RECORD_NOT_SAVED)
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnUndo_Write_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUndo_Write.Click
        Try
            If Not Me.State.MyBO.IsNew Then
                'Reload from the DB
                Me.State.MyBO = New Notification(Me.State.MyBO.Id)
            ElseIf Not Me.State.ScreenSnapShotBO Is Nothing Then
                'It was a new with copy
                Me.State.MyBO.Clone(Me.State.ScreenSnapShotBO)
            Else
                Me.State.MyBO = New Notification
            End If
            Me.PopulateFormFromBOs()
            Me.EnableDisableFields()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnDelete_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete_WRITE.Click
        Try

            Me.State.MyBO.Enabled = "N"
            Me.State.MyBO.OutageEndDate = Date.Now
            Me.State.MyBO.Save()
            Me.State.HasDataChanged = True
            Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Delete, Me.State.MyBO, Me.State.HasDataChanged))
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            'undo the delete
            Me.State.MyBO.RejectChanges()
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnNew_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew_WRITE.Click
        Try
            Me.PopulateBOsFormFrom()
            If Me.State.MyBO.IsDirty Then
                Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.New_
            Else
                Me.CreateNew()
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnCopy_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCopy_WRITE.Click
        Try
            Me.PopulateBOsFormFrom()
            If Me.State.MyBO.IsDirty Then
                Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.NewAndCopy
            Else
                Me.CreateNewWithCopy()
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
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
    Public Shared Function PopulateServiceCenterDrop(ByVal prefixText As String, ByVal count As Integer) As String()
        Dim dv As DataView = LookupListNew.GetServiceCenterLookupList(AjaxState.MyBO.Id)
        Return AjaxController.BindAutoComplete(prefixText, dv)
    End Function
#End Region

    Private Sub UpdateBreadCrum()
        If (Not Me.State Is Nothing) Then
            Me.MasterPage.BreadCrum = Me.MasterPage.PageTab & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage("SYSTEM_NOTIFICATION_DETAIL")
            Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("SYSTEM_NOTIFICATION_DETAIL")
        End If
    End Sub

End Class


