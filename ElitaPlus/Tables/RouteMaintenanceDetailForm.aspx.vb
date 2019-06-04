Partial Public Class RouteMaintenanceDetailForm
    Inherits ElitaPlusPage

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    'Protected WithEvents UserControlAvailableSelectedServiceCenters As Assurant.ElitaPlus.ElitaPlusWebApp.Generic.UserControlAvailableSelected

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
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
        Public Sub New(ByVal LastOp As DetailPageCommand, ByVal curEditingBo As Route, ByVal hasDataChanged As Boolean)
            Me.LastOperation = LastOp
            Me.EditingBo = curEditingBo
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

    Private Sub Page_PageCall(ByVal CallFromUrl As String, ByVal CallingPar As Object) Handles MyBase.PageCall
        Try
            If Not Me.CallingParameters Is Nothing Then
                'Get the id from the parent
                Me.State.MyBO = New Route(CType(Me.CallingParameters, Guid))
                Me.State.ServiceNetworkId = Me.State.MyBO.ServiceNetworkId
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try

    End Sub

#End Region

#Region "Page Events"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Try
            Me.ErrControllerMaster.Clear_Hide()
            If Not Me.IsPostBack Then
                Me.SetFormTitle(PAGETITLE)
                Me.SetFormTab(PAGETAB)
                Me.MenuEnabled = False
                Me.AddControlMsg(Me.btnDelete_WRITE, Message.DELETE_RECORD_PROMPT, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, True)
                If Me.State.MyBO Is Nothing Then
                    Me.State.MyBO = New Route
                    Me.State.IsNew = True
                End If
                Me.PopulateDropdowns()
                Me.PopulateFormFromBOs()
                Me.EnableDisableFields()
            End If
            BindBoPropertiesToLabels()
            CheckIfComingFromSaveConfirm()
            If Not Me.IsPostBack Then
                Me.AddLabelDecorations(Me.State.MyBO)
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
        Me.ShowMissingTranslations(Me.ErrControllerMaster)
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
        ElseIf Not Me.State.IsNew Then
            ControlMgr.SetEnableControl(Me, btnDelete_WRITE, True)
        End If

        If Not Me.State.MyBO.IsNew Then
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
        Me.BindBOPropertyToLabel(Me.State.MyBO, "Code", Me.LabelShortDesc)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "Description", Me.LabelDescription)
        Me.ClearGridHeadersAndLabelsErrSign()
    End Sub

    Protected Sub PopulateDropdowns()

        Try
            moServiceNetworkMultipleDrop.NothingSelected = True
            moServiceNetworkMultipleDrop.SetControl(True, moServiceNetworkMultipleDrop.MODES.NEW_MODE, True, LookupListNew.GetServiceNetworkLookupList(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id), TranslationBase.TranslateLabelOrMessage(LABEL_SERVICE_NETWORK), False)
        Catch ex As Exception
            Me.ErrControllerMaster.AddErrorAndShow(Assurant.ElitaPlus.Common.ErrorCodes.DB_ERROR)
        End Try

    End Sub

#Region "Detail Grid"

    Sub PopulateUserControlAvailableSelectedServiceCenters()
        Me.UserControlAvailableSelectedServiceCenters.BackColor = "#d5d6e4"
        ControlMgr.SetVisibleControl(Me, UserControlAvailableSelectedServiceCenters, False)
        If Not Me.State.MyBO.Id.Equals(Guid.Empty) Then
            Dim availableDv As DataView
            Dim selectedDv As DataView

            Dim availDS As DataSet = Me.State.MyBO.GetAvailableSCs(Me.State.ServiceNetworkId)
            If availDS.Tables.Count > 0 Then
                availableDv = New DataView(availDS.Tables(0))
            End If

            Dim selectedDS As DataSet = Me.State.MyBO.GetSelectedSCs(Me.State.MyBO.Id)

            If selectedDS.Tables.Count > 0 Then
                selectedDv = New DataView(selectedDS.Tables(0))
            End If

            Me.UserControlAvailableSelectedServiceCenters.SetAvailableData(availableDv, LookupListNew.COL_DESCRIPTION_NAME, LookupListNew.COL_ID_NAME)
            Me.UserControlAvailableSelectedServiceCenters.SetSelectedData(selectedDv, LookupListNew.COL_DESCRIPTION_NAME, LookupListNew.COL_ID_NAME)
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



    Private Sub moServiceNetworkMultipleDrop_SelectedIndexChanged(ByVal moUserCompanyMultipleDrop As Common.MultipleColumnDDLabelControl) Handles moServiceNetworkMultipleDrop.SelectedDropChanged
        Try
            Me.State.ServiceNetworkId = Me.moServiceNetworkMultipleDrop.SelectedGuid
            PopulateUserControlAvailableSelectedServiceCenters()
            EnableDisableFields()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub

#End Region

    Protected Sub PopulateFormFromBOs()
        With Me.State.MyBO

            Me.PopulateControlFromBOProperty(Me.TextboxShortDesc, .Code)
            Me.PopulateControlFromBOProperty(Me.TextboxDescription, .Description)
            Me.moServiceNetworkMultipleDrop.SelectedIndex = Me.BLANK_ITEM_SELECTED
            If Not Me.State.IsNew Then
                Me.moServiceNetworkMultipleDrop.SelectedGuid = .ServiceNetworkId
            End If

            Me.State.ServiceNetworkId = .ServiceNetworkId
            PopulateUserControlAvailableSelectedServiceCenters()

        End With

        PopulateDetailControls()

    End Sub

    Protected Sub PopulateBOsFormFrom()
        With Me.State.MyBO
            Me.PopulateBOProperty(Me.State.MyBO, "Code", Me.TextboxShortDesc)
            Me.PopulateBOProperty(Me.State.MyBO, "Description", Me.TextboxDescription)
            '.CountryId = ElitaPlusIdentity.Current.ActiveUser.Country(ElitaPlusIdentity.Current.ActiveUser.FirstCompanyID).Id
            .ServiceNetworkId = Me.State.ServiceNetworkId
        End With
        If Me.ErrCollection.Count > 0 Then
            Throw New PopulateBOErrorException
        End If
    End Sub


    Protected Sub CreateNew()
        Me.State.ScreenSnapShotBO = Nothing 'Reset the backup copy

        Me.State.MyBO = New Route
        Me.State.IsNew = True
        Me.PopulateFormFromBOs()
        Me.EnableDisableFields()
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
                    Me.DisplayMessage(Message.SAVE_RECORD_CONFIRMATION, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
                    Me.CreateNew()
                Case ElitaPlusPage.DetailPageCommand.NewAndCopy
                    'Me.DisplayMessage(Message.SAVE_RECORD_CONFIRMATION, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
                    'Me.CreateNewWithCopy()
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
                    'Me.CreateNewWithCopy()
                Case ElitaPlusPage.DetailPageCommand.BackOnErr
                    Me.ErrControllerMaster.AddErrorAndShow(Me.State.LastErrMsg)
            End Select
        End If
        'Clean after consuming the action
        Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
        Me.HiddenSaveChangesPromptResponse.Value = ""
    End Sub


#End Region


#Region "Button Clicks"

    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Try
            Me.PopulateBOsFormFrom()
            If Me.State.MyBO.IsFamilyDirty Then
                Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", Me.MSG_BTN_YES_NO_CANCEL, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
            Else
                Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.MyBO, Me.State.HasDataChanged))
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
            Me.DisplayMessage(Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
            Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.BackOnErr
            Me.State.LastErrMsg = Me.ErrControllerMaster.Text
        End Try
    End Sub

    Private Sub moBtnSave_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles moBtnSave_WRITE.Click
        Try
            Me.PopulateBOsFormFrom()
            If Me.State.MyBO.IsFamilyDirty Then
                Me.State.MyBO.Save()
                Me.State.IsNew = False
                Me.State.HasDataChanged = True
                Me.PopulateFormFromBOs()
                Me.EnableDisableFields()
                Me.DisplayMessage(Message.SAVE_RECORD_CONFIRMATION, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
            Else
                Me.DisplayMessage(Message.MSG_RECORD_NOT_SAVED, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub

    Private Sub moBtnCancelClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles moBtnCancel.Click
        Try
            If Not Me.State.MyBO.IsNew Then
                'Reload from the DB
                Me.State.MyBO = New Route(Me.State.MyBO.Id)
            ElseIf Not Me.State.ScreenSnapShotBO Is Nothing Then
                'It was a new with copy
                Me.State.MyBO.Clone(Me.State.ScreenSnapShotBO)
            Else
                Me.State.MyBO = New Route
            End If
            Me.PopulateFormFromBOs()
            Me.EnableDisableFields()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub



    Private Sub btnDelete_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete_WRITE.Click
        Try
            Me.State.MyBO.Delete()
            Me.State.MyBO.Save()
            Me.State.HasDataChanged = True
            Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Delete, Me.State.MyBO, Me.State.HasDataChanged))
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            'undo the delete
            Me.State.MyBO.RejectChanges()
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub

    Private Sub btnNew_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew_WRITE.Click
        Try
            Me.PopulateBOsFormFrom()
            If Me.State.MyBO.IsFamilyDirty Then
                Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", Me.MSG_BTN_YES_NO_CANCEL, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                'Me.AddConfirmMsg(Message.SAVE_CHANGES_PROMPT, Me.HiddenSaveChangesPromptResponse)
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.New_
            Else
                Me.CreateNew()
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub

#End Region

#Region "Attach - Detach Event Handlers"

    Private Sub UserControlAvailableSelectedServiceCenters_Attach(ByVal aSrc As Generic.UserControlAvailableSelected, ByVal attachedList As System.Collections.ArrayList) Handles UserControlAvailableSelectedServiceCenters.Attach
        Try
            If attachedList.Count > 0 Then
                Me.State.MyBO.AttachServiceCenters(attachedList)
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub

    Private Sub UserControlAvailableSelectedServiceCenters_Detach(ByVal aSrc As Generic.UserControlAvailableSelected, ByVal detachedList As System.Collections.ArrayList) Handles UserControlAvailableSelectedServiceCenters.Detach
        Try
            If detachedList.Count > 0 Then
                Me.State.MyBO.DetachServiceCenters(detachedList)
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub
#End Region

    Private Sub mpHoriz_SelectedIndexChange(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub
End Class