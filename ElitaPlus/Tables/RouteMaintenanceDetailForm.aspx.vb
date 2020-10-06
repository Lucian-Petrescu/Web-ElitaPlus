Partial Public Class RouteMaintenanceDetailForm
    Inherits ElitaPlusPage

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    'Protected WithEvents UserControlAvailableSelectedServiceCenters As Assurant.ElitaPlus.ElitaPlusWebApp.Generic.UserControlAvailableSelected

    Private Sub Page_Init(sender As System.Object, e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Constants"
    Public Const URL As String = "RouteMaintenanceDetailForm.aspx"
    Public Const PAGETITLE As String = "ROUTE_MAINTENANCE"
    Public Const PAGETAB As String = "TABLES"
    Public Const AVAILABLE_SERVICE_CENTERS As String = "AVAILABLE_SERVICE_CENTERS"
    Public Const SELECTED_SERVICE_CENTERS As String = "SELECTED_SERVICE_CENTERS"
    Private Const ADDL_DAC_NONE As String = "NONE"
    Private Const LABEL_SERVICE_NETWORK As String = "SERVICE_NETWORK"
#End Region

#Region "Page Return Type"
    Public Class ReturnType
        Public LastOperation As DetailPageCommand
        Public EditingBo As Route
        Public HasDataChanged As Boolean
        Public Sub New(LastOp As DetailPageCommand, curEditingBo As Route, hasDataChanged As Boolean)
            LastOperation = LastOp
            EditingBo = curEditingBo
            Me.HasDataChanged = hasDataChanged
        End Sub
    End Class
#End Region

#Region "Page State"


    Class MyState
        Public MyBO As Route
        Public ScreenSnapShotBO As Route
        Public SelectedSCId As Guid = Guid.Empty
        Public ServiceNetworkId As Guid = Guid.Empty
        Public SortExpressionDetailGrid As String = ServiceGroup.RiskTypeManufacturerDataView.RISK_TYPE_COL_NAME
        Public ActionInProgress As DetailPageCommand = DetailPageCommand.Nothing_
        Public LastErrMsg As String
        Public HasDataChanged As Boolean
        Public IsNew As Boolean = False
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
                State.MyBO = New Route(CType(CallingParameters, Guid))
                State.ServiceNetworkId = State.MyBO.ServiceNetworkId
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try

    End Sub

#End Region

#Region "Page Events"
    Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Try
            ErrControllerMaster.Clear_Hide()
            If Not IsPostBack Then
                SetFormTitle(PAGETITLE)
                SetFormTab(PAGETAB)
                MenuEnabled = False
                AddControlMsg(btnDelete_WRITE, Message.DELETE_RECORD_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, True)
                If State.MyBO Is Nothing Then
                    State.MyBO = New Route
                    State.IsNew = True
                End If
                PopulateDropdowns()
                PopulateFormFromBOs()
                EnableDisableFields()
            End If
            BindBoPropertiesToLabels()
            CheckIfComingFromSaveConfirm()
            If Not IsPostBack Then
                AddLabelDecorations(State.MyBO)
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
        ShowMissingTranslations(ErrControllerMaster)
    End Sub
#End Region

#Region "Controlling Logic"

    Protected Sub EnableDisableFields()
        'Enabled by Default
        ControlMgr.SetEnableControl(Me, btnDelete_WRITE, False)
        ControlMgr.SetEnableControl(Me, btnNew_WRITE, False)
        ControlMgr.SetEnableControl(Me, LabelShortDesc, True)
        ControlMgr.SetEnableControl(Me, TextboxShortDesc, True)
        ControlMgr.SetEnableControl(Me, LabelDescription, True)
        ControlMgr.SetEnableControl(Me, TextboxDescription, True)
        moServiceNetworkMultipleDrop.ChangeEnabledControlProperty(True)

        If UserControlAvailableSelectedServiceCenters.SelectedList.Count > 0 Then
            ControlMgr.SetEnableControl(Me, btnDelete_WRITE, False)
        ElseIf Not State.IsNew Then
            ControlMgr.SetEnableControl(Me, btnDelete_WRITE, True)
        End If

        If Not State.MyBO.IsNew Then
            ControlMgr.SetEnableControl(Me, btnNew_WRITE, True)
            ControlMgr.SetEnableControl(Me, LabelShortDesc, False)
            ControlMgr.SetEnableControl(Me, TextboxShortDesc, False)
            ControlMgr.SetEnableControl(Me, LabelDescription, False)
            ControlMgr.SetEnableControl(Me, TextboxDescription, False)
            If UserControlAvailableSelectedServiceCenters.SelectedList.Count > 0 Then
                moServiceNetworkMultipleDrop.ChangeEnabledControlProperty(False)
            End If
        End If
    End Sub

    Protected Sub BindBoPropertiesToLabels()
        BindBOPropertyToLabel(State.MyBO, "Code", LabelShortDesc)
        BindBOPropertyToLabel(State.MyBO, "Description", LabelDescription)
        ClearGridHeadersAndLabelsErrSign()
    End Sub

    Protected Sub PopulateDropdowns()

        Try
            moServiceNetworkMultipleDrop.NothingSelected = True
            moServiceNetworkMultipleDrop.SetControl(True, moServiceNetworkMultipleDrop.MODES.NEW_MODE, True, LookupListNew.GetServiceNetworkLookupList(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id), TranslationBase.TranslateLabelOrMessage(LABEL_SERVICE_NETWORK), False)
        Catch ex As Exception
            ErrControllerMaster.AddErrorAndShow(Assurant.ElitaPlus.Common.ErrorCodes.DB_ERROR)
        End Try

    End Sub

#Region "Detail Grid"

    Sub PopulateUserControlAvailableSelectedServiceCenters()
        UserControlAvailableSelectedServiceCenters.BackColor = "#d5d6e4"
        ControlMgr.SetVisibleControl(Me, UserControlAvailableSelectedServiceCenters, False)
        If Not State.MyBO.Id.Equals(Guid.Empty) Then
            Dim availableDv As DataView
            Dim selectedDv As DataView

            Dim availDS As DataSet = State.MyBO.GetAvailableSCs(State.ServiceNetworkId)
            If availDS.Tables.Count > 0 Then
                availableDv = New DataView(availDS.Tables(0))
            End If

            Dim selectedDS As DataSet = State.MyBO.GetSelectedSCs(State.MyBO.Id)

            If selectedDS.Tables.Count > 0 Then
                selectedDv = New DataView(selectedDS.Tables(0))
            End If

            UserControlAvailableSelectedServiceCenters.SetAvailableData(availableDv, LookupListNew.COL_DESCRIPTION_NAME, LookupListNew.COL_ID_NAME)
            UserControlAvailableSelectedServiceCenters.SetSelectedData(selectedDv, LookupListNew.COL_DESCRIPTION_NAME, LookupListNew.COL_ID_NAME)
            ControlMgr.SetVisibleControl(Me, UserControlAvailableSelectedServiceCenters, True)
            UserControlAvailableSelectedServiceCenters.AvailableDesc = TranslationBase.TranslateLabelOrMessage(AVAILABLE_SERVICE_CENTERS)
            UserControlAvailableSelectedServiceCenters.SelectedDesc = TranslationBase.TranslateLabelOrMessage(SELECTED_SERVICE_CENTERS)
        End If
    End Sub

    Sub PopulateDetailControls()
        'ControlMgr.SetVisibleControl(Me, moServiceNetworkMultipleDrop, False)
        'Me.moServiceNetworkMultipleDrop.Visible = False
        'If Me.State.IsNew Then
        'ControlMgr.SetVisibleControl(Me, moServiceNetworkMultipleDrop, True)
        'End If

    End Sub



    Private Sub moServiceNetworkMultipleDrop_SelectedIndexChanged(moUserCompanyMultipleDrop As Common.MultipleColumnDDLabelControl) Handles moServiceNetworkMultipleDrop.SelectedDropChanged
        Try
            State.ServiceNetworkId = moServiceNetworkMultipleDrop.SelectedGuid
            PopulateUserControlAvailableSelectedServiceCenters()
            EnableDisableFields()
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub

#End Region

    Protected Sub PopulateFormFromBOs()
        With State.MyBO

            PopulateControlFromBOProperty(TextboxShortDesc, .Code)
            PopulateControlFromBOProperty(TextboxDescription, .Description)
            moServiceNetworkMultipleDrop.SelectedIndex = BLANK_ITEM_SELECTED
            If Not State.IsNew Then
                moServiceNetworkMultipleDrop.SelectedGuid = .ServiceNetworkId
            End If

            State.ServiceNetworkId = .ServiceNetworkId
            PopulateUserControlAvailableSelectedServiceCenters()

        End With

        PopulateDetailControls()

    End Sub

    Protected Sub PopulateBOsFormFrom()
        With State.MyBO
            PopulateBOProperty(State.MyBO, "Code", TextboxShortDesc)
            PopulateBOProperty(State.MyBO, "Description", TextboxDescription)
            '.CountryId = ElitaPlusIdentity.Current.ActiveUser.Country(ElitaPlusIdentity.Current.ActiveUser.FirstCompanyID).Id
            .ServiceNetworkId = State.ServiceNetworkId
        End With
        If ErrCollection.Count > 0 Then
            Throw New PopulateBOErrorException
        End If
    End Sub


    Protected Sub CreateNew()
        State.ScreenSnapShotBO = Nothing 'Reset the backup copy

        State.MyBO = New Route
        State.IsNew = True
        PopulateFormFromBOs()
        EnableDisableFields()
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
                    DisplayMessage(Message.SAVE_RECORD_CONFIRMATION, "", MSG_BTN_OK, MSG_TYPE_INFO)
                    CreateNew()
                Case ElitaPlusPage.DetailPageCommand.NewAndCopy
                    'Me.DisplayMessage(Message.SAVE_RECORD_CONFIRMATION, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
                    'Me.CreateNewWithCopy()
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
                    'Me.CreateNewWithCopy()
                Case ElitaPlusPage.DetailPageCommand.BackOnErr
                    ErrControllerMaster.AddErrorAndShow(State.LastErrMsg)
            End Select
        End If
        'Clean after consuming the action
        State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
        HiddenSaveChangesPromptResponse.Value = ""
    End Sub


#End Region


#Region "Button Clicks"

    Private Sub btnBack_Click(sender As System.Object, e As System.EventArgs) Handles btnBack.Click
        Try
            PopulateBOsFormFrom()
            If State.MyBO.IsFamilyDirty Then
                DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO_CANCEL, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
            Else
                ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, State.MyBO, State.HasDataChanged))
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
            DisplayMessage(Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
            State.ActionInProgress = ElitaPlusPage.DetailPageCommand.BackOnErr
            State.LastErrMsg = ErrControllerMaster.Text
        End Try
    End Sub

    Private Sub moBtnSave_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles moBtnSave_WRITE.Click
        Try
            PopulateBOsFormFrom()
            If State.MyBO.IsFamilyDirty Then
                State.MyBO.Save()
                State.IsNew = False
                State.HasDataChanged = True
                PopulateFormFromBOs()
                EnableDisableFields()
                DisplayMessage(Message.SAVE_RECORD_CONFIRMATION, "", MSG_BTN_OK, MSG_TYPE_INFO)
            Else
                DisplayMessage(Message.MSG_RECORD_NOT_SAVED, "", MSG_BTN_OK, MSG_TYPE_INFO)
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub

    Private Sub moBtnCancelClick(sender As System.Object, e As System.EventArgs) Handles moBtnCancel.Click
        Try
            If Not State.MyBO.IsNew Then
                'Reload from the DB
                State.MyBO = New Route(State.MyBO.Id)
            ElseIf State.ScreenSnapShotBO IsNot Nothing Then
                'It was a new with copy
                State.MyBO.Clone(State.ScreenSnapShotBO)
            Else
                State.MyBO = New Route
            End If
            PopulateFormFromBOs()
            EnableDisableFields()
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub



    Private Sub btnDelete_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnDelete_WRITE.Click
        Try
            State.MyBO.Delete()
            State.MyBO.Save()
            State.HasDataChanged = True
            ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Delete, State.MyBO, State.HasDataChanged))
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            'undo the delete
            State.MyBO.RejectChanges()
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub

    Private Sub btnNew_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnNew_WRITE.Click
        Try
            PopulateBOsFormFrom()
            If State.MyBO.IsFamilyDirty Then
                DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO_CANCEL, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                'Me.AddConfirmMsg(Message.SAVE_CHANGES_PROMPT, Me.HiddenSaveChangesPromptResponse)
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.New_
            Else
                CreateNew()
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub

#End Region

#Region "Attach - Detach Event Handlers"

    Private Sub UserControlAvailableSelectedServiceCenters_Attach(aSrc As Generic.UserControlAvailableSelected, attachedList As System.Collections.ArrayList) Handles UserControlAvailableSelectedServiceCenters.Attach
        Try
            If attachedList.Count > 0 Then
                State.MyBO.AttachServiceCenters(attachedList)
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub

    Private Sub UserControlAvailableSelectedServiceCenters_Detach(aSrc As Generic.UserControlAvailableSelected, detachedList As System.Collections.ArrayList) Handles UserControlAvailableSelectedServiceCenters.Detach
        Try
            If detachedList.Count > 0 Then
                State.MyBO.DetachServiceCenters(detachedList)
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub
#End Region

    Private Sub mpHoriz_SelectedIndexChange(sender As System.Object, e As System.EventArgs)

    End Sub
End Class