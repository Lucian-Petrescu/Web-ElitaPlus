Partial Class ServiceNetworkForm
    Inherits ElitaPlusSearchPage



    Protected WithEvents ErrorCtrl As ErrorController
    Protected WithEvents LabelRiskType As System.Web.UI.WebControls.Label

    Protected WithEvents Label7 As System.Web.UI.WebControls.Label
    Protected WithEvents moCountryColonLabel_NO_TRANSLATE As System.Web.UI.WebControls.Label
    Protected WithEvents SearchCodeLabel As System.Web.UI.WebControls.Label
    Protected WithEvents TextBoxSearchCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents LabelSearchDescription As System.Web.UI.WebControls.Label
    Protected WithEvents TextBoxSearchDescription As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnClearSearch As System.Web.UI.WebControls.Button
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents lblPageSize As System.Web.UI.WebControls.Label
    Protected WithEvents cboPageSize As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblRecordCount As System.Web.UI.WebControls.Label
    Protected WithEvents Grid As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnAdd_WRITE As System.Web.UI.WebControls.Button
    Protected WithEvents trPageSize As System.Web.UI.HtmlControls.HtmlTableRow


    Protected WithEvents UserControlAvailableSelectedServiceCenters As Assurant.ElitaPlus.ElitaPlusWebApp.Generic.UserControlAvailableSelected

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
    Public Const URL As String = "ServiceNetworkForm.aspx"
#End Region

#Region "Page Return Type"
    Public Class ReturnType
        Public LastOperation As DetailPageCommand
        Public EditingBo As ServiceNetwork
        Public HasDataChanged As Boolean
        Public Sub New(LastOp As DetailPageCommand, curEditingBo As ServiceNetwork, hasDataChanged As Boolean)
            LastOperation = LastOp
            EditingBo = curEditingBo
            Me.HasDataChanged = hasDataChanged
        End Sub
    End Class
#End Region

#Region "Page State"


    Class MyState
        Public MyBO As ServiceNetwork
        Public ScreenSnapShotBO As ServiceNetwork
        Public SelectedSCId As Guid = Guid.Empty
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
                State.MyBO = New ServiceNetwork(CType(CallingParameters, Guid))
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try

    End Sub

#End Region

#Region "Page Events"
    Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Try
            ErrorCtrl.Clear_Hide()
            If Not IsPostBack Then
                'Date Calendars
                MenuEnabled = False
                'Me.AddConfirmation(Me.btnDelete_WRITE, Message.DELETE_RECORD_PROMPT)
                AddControlMsg(btnDelete_WRITE, Message.DELETE_RECORD_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, True)
                If State.MyBO Is Nothing Then
                    State.MyBO = New ServiceNetwork
                    State.IsNew = True
                End If
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
            HandleErrors(ex, ErrorCtrl)
        End Try
        ShowMissingTranslations(ErrorCtrl)
    End Sub
#End Region

#Region "Controlling Logic"

    Protected Sub EnableDisableFields()
        'Enabled by Default
        ControlMgr.SetEnableControl(Me, btnDelete_WRITE, True)
        ControlMgr.SetEnableControl(Me, btnNew_WRITE, True)
        ControlMgr.SetEnableControl(Me, btnCopy_WRITE, True)
        'Now disable depebding on the object state
        If State.MyBO.IsNew Then
            ControlMgr.SetEnableControl(Me, btnDelete_WRITE, False)
            ControlMgr.SetEnableControl(Me, btnNew_WRITE, False)
            ControlMgr.SetEnableControl(Me, btnCopy_WRITE, False)
        End If
        ControlMgr.SetEnableControl(Me, TextboxShortDesc, True)
        If Not State.MyBO.IsNew Then
            ControlMgr.SetEnableControl(Me, TextboxShortDesc, False)
        End If
    End Sub

    Protected Sub BindBoPropertiesToLabels()
        BindBOPropertyToLabel(State.MyBO, "ShortDesc", LabelShortDesc)
        BindBOPropertyToLabel(State.MyBO, "Description", LabelDescription)
        ClearGridHeadersAndLabelsErrSign()
    End Sub

#Region "Detail Grid"

    Sub PopulateUserControlAvailableSelectedServiceCenters()
        UserControlAvailableSelectedServiceCenters.BackColor = "#d5d6e4"
        ControlMgr.SetVisibleControl(Me, UserControlAvailableSelectedServiceCenters, False)
        Dim availableDv As DataView = State.MyBO.GetAvailableServiceCenters()
        Dim selectedDv As DataView = State.MyBO.GetSelectedServiceCenters()
        UserControlAvailableSelectedServiceCenters.SetAvailableData(availableDv, LookupListNew.COL_DESCRIPTION_NAME, LookupListNew.COL_ID_NAME)
        UserControlAvailableSelectedServiceCenters.SetSelectedData(selectedDv, LookupListNew.COL_DESCRIPTION_NAME, LookupListNew.COL_ID_NAME)
        ControlMgr.SetVisibleControl(Me, UserControlAvailableSelectedServiceCenters, True)
    End Sub

    Sub PopulateDetailControls()

        PopulateUserControlAvailableSelectedServiceCenters()
    End Sub
#End Region

    Protected Sub PopulateFormFromBOs()
        With State.MyBO

            PopulateControlFromBOProperty(TextboxShortDesc, .ShortDesc)
            PopulateControlFromBOProperty(TextboxDescription, .Description)

        End With

        PopulateDetailControls()

    End Sub

    Protected Sub PopulateBOsFormFrom()
        With State.MyBO
            PopulateBOProperty(State.MyBO, "ShortDesc", TextboxShortDesc)
            PopulateBOProperty(State.MyBO, "Description", TextboxDescription)
            .CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
        End With
        If ErrCollection.Count > 0 Then
            Throw New PopulateBOErrorException
        End If
    End Sub


    Protected Sub CreateNew()
        State.ScreenSnapShotBO = Nothing 'Reset the backup copy

        State.MyBO = New ServiceNetwork
        State.IsNew = True
        PopulateFormFromBOs()
        EnableDisableFields()
    End Sub

    Protected Sub CreateNewWithCopy()
        Dim newObj As New ServiceNetwork
        newObj.Copy(State.MyBO)

        State.MyBO = newObj
        State.MyBO.ShortDesc = Nothing
        State.MyBO.Description = Nothing
        PopulateFormFromBOs()
        EnableDisableFields()

        'create the backup copy
        State.ScreenSnapShotBO = New ServiceNetwork
        State.ScreenSnapShotBO.Copy(State.MyBO)
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
                    DisplayMessage(Message.SAVE_RECORD_CONFIRMATION, "", MSG_BTN_OK, MSG_TYPE_INFO)
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
                    ErrorCtrl.AddErrorAndShow(State.LastErrMsg)
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
            If State.MyBO.IsDirty Then
                DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO_CANCEL, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                'Me.AddConfirmMsg(Message.SAVE_CHANGES_PROMPT, Me.HiddenSaveChangesPromptResponse)
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
            Else
                ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, State.MyBO, State.HasDataChanged))
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
            DisplayMessage(Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
            'Me.AddConfirmMsg(Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR, Me.HiddenSaveChangesPromptResponse)
            State.ActionInProgress = ElitaPlusPage.DetailPageCommand.BackOnErr
            State.LastErrMsg = ErrorCtrl.Text
        End Try
    End Sub

    Private Sub btnSave_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnSave_WRITE.Click
        Try
            PopulateBOsFormFrom()
            If State.MyBO.IsDirty Then
                State.MyBO.Save()
                State.IsNew = False
                State.HasDataChanged = True
                PopulateFormFromBOs()
                EnableDisableFields()
                'Me.AddInfoMsg(Message.SAVE_RECORD_CONFIRMATION)
                DisplayMessage(Message.SAVE_RECORD_CONFIRMATION, "", MSG_BTN_OK, MSG_TYPE_INFO)
            Else
                DisplayMessage(Message.MSG_RECORD_NOT_SAVED, "", MSG_BTN_OK, MSG_TYPE_INFO)
                'Me.AddInfoMsg(Message.MSG_RECORD_NOT_SAVED)
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

    Private Sub btnUndo_Write_Click(sender As System.Object, e As System.EventArgs) Handles btnUndo_Write.Click
        Try
            If Not State.MyBO.IsNew Then
                'Reload from the DB
                State.MyBO = New ServiceNetwork(State.MyBO.Id)
            ElseIf State.ScreenSnapShotBO IsNot Nothing Then
                'It was a new with copy
                State.MyBO.Clone(State.ScreenSnapShotBO)
            Else
                State.MyBO = New ServiceNetwork
            End If
            PopulateFormFromBOs()
            EnableDisableFields()
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
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
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

    Private Sub btnNew_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnNew_WRITE.Click
        Try
            PopulateBOsFormFrom()
            If State.MyBO.IsDirty Then
                DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO_CANCEL, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                'Me.AddConfirmMsg(Message.SAVE_CHANGES_PROMPT, Me.HiddenSaveChangesPromptResponse)
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.New_
            Else
                CreateNew()
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub



    Private Sub btnCopy_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnCopy_WRITE.Click
        Try
            PopulateBOsFormFrom()
            If State.MyBO.IsDirty Then
                DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO_CANCEL, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                'Me.AddConfirmMsg(Message.SAVE_CHANGES_PROMPT, Me.HiddenSaveChangesPromptResponse)
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.NewAndCopy
            Else
                CreateNewWithCopy()
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub


#End Region

#Region "Page Control Events"

#End Region

#Region "Handle-Drop"


#End Region

#Region "Attach - Detach Event Handlers"

    Private Sub UserControlAvailableSelectedServiceCenters_Attach(aSrc As Generic.UserControlAvailableSelected, attachedList As System.Collections.ArrayList) Handles UserControlAvailableSelectedServiceCenters.Attach
        Try
            If attachedList.Count > 0 Then
                State.MyBO.AttachServiceCenters(attachedList)
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

    Private Sub UserControlAvailableSelectedServiceCenters_Detach(aSrc As Generic.UserControlAvailableSelected, detachedList As System.Collections.ArrayList) Handles UserControlAvailableSelectedServiceCenters.Detach
        Try
            If detachedList.Count > 0 Then
                State.MyBO.DetachServiceCenters(detachedList)
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub
#End Region

    Private Sub mpHoriz_SelectedIndexChange(sender As System.Object, e As System.EventArgs)

    End Sub
End Class
