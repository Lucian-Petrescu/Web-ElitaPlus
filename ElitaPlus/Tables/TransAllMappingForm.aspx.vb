Imports System.Globalization
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports System.Threading
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms
Partial Public Class TransAllMappingForm
    Inherits ElitaPlusSearchPage


#Region "CONSTANTS"
    Public Const URL As String = "~/Tables/TransAllMappingForm.aspx"
    Public Const PAGETITLE As String = "TransAll_Mapping"
    Public Const PAGETAB As String = "ADMIN"

    Private Const MSG_CONFIRM_PROMPT As String = "MSG_CONFIRM_PROMPT"
    Private Const MSG_RECORD_DELETED_OK As String = "MSG_RECORD_DELETED_OK"
    Private Const MSG_RECORD_SAVED_OK As String = "MSG_RECORD_SAVED_OK"
    Private Const MSG_RECORD_NOT_SAVED As String = "MSG_RECORD_NOT_SAVED"

    Private Const EDIT_COMMAND As String = "EditRecord"
    Private Const DELETE_COMMAND As String = "DeleteRecord"
    Private Const SORT_COMMAND As String = "Sort"

    Private Const NO_ROW_SELECTED_INDEX As Integer = -1

    Private Const TRANSALL_MAPPING_OUT_ID_COL As Integer = 2
    Private Const TRANSALL_MAPPING_ID_COL As Integer = 3
    Private Const OUTPUT_MASK_COL As Integer = 4
    Private Const LAYOUT_CODE_COL As Integer = 5

    'Controls
    Private Const CTL_TRANSALLMAPPINGOUTID_LABEL As String = "lblTransallMappingOutId"
    Private Const CTL_TRANSALLMAPPINGID_LABEL As String = "lblTransallMappingId"
    Private Const CTL_OUTPUTMASK_LABEL As String = "lblColOutputMask"
    Private Const CTL_OUTPUTMASK_TEXT As String = "txtColOutputMask"
    Private Const CTL_LAYOUTCODE_LABEL As String = "lblColLayoutCode"
    Private Const CTL_LAYOUTCODE_DDL As String = "ddlColLayoutCode"

#End Region

#Region "Page Return Type"
    Public Class ReturnType
        Public LastOperation As DetailPageCommand
        Public EditingBo As TransallMapping
        Public HasDataChanged As Boolean
        Public Sub New(LastOp As DetailPageCommand, curEditingBo As TransallMapping, hasDataChanged As Boolean)
            LastOperation = LastOp
            EditingBo = curEditingBo
            Me.HasDataChanged = hasDataChanged
        End Sub
    End Class
#End Region

#Region "Page State"


    Class MyState
        Public MyBO As TransallMapping
        Public TransallOutBO As TransallMappingOut
        Public dvOutput As DataView = Nothing
        Public ActionInProgress As DetailPageCommand = DetailPageCommand.Nothing_
        Public LastErrMsg As String
        Public HasDataChanged As Boolean
        Public bnoRow As Boolean = False
        Public PageIndex As Integer = 0
        Public IsGridVisible As Boolean = True
        Public IsEditMode As Boolean
        Public IsNew As Boolean
        Public AddingNewRow As Boolean
        Public Canceling As Boolean
        Public editRowIndex As Integer
        Public selectedPageSize As Integer = DEFAULT_PAGE_SIZE
        Public TransallMappingOutId As Guid = Guid.Empty

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
                State.MyBO = New TransallMapping(CType(CallingParameters, Guid))
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try

    End Sub

#End Region

#Region "Page Events"
    Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        ErrControllerMaster.Clear_Hide()

        Try
            If Not IsPostBack Then

                SetFormTitle(PAGETITLE)
                SetFormTab(PAGETAB)
                SortDirection = TransallMappingOut.TransallMappingOutDV.COL_ACCT_OUTPUT_MASK

                MenuEnabled = False
                AddConfirmation(btnDelete_WRITE, Message.DELETE_RECORD_PROMPT)
                If State.MyBO Is Nothing Then
                    State.MyBO = New TransallMapping
                    State.IsNew = True
                    ControlMgr.SetVisibleControl(Me, btnNewOutput_WRITE, False)
                Else
                    State.IsNew = False
                End If

                PopulateDropdowns()
                PopulateDealer()
                PopulateFormFromBOs()
                TranslateGridHeader(Grid)
                TranslateGridControls(Grid)
                EnableDisableFields()

                SetButtonsState()

            End If

            BindBoPropertiesToLabels()
            BindBoPropertiesToGridHeaders()
            CheckIfComingFromSaveConfirm()
            CheckIfComingFromDeleteConfirm()
            If Not IsPostBack Then
                AddLabelDecorations(State.MyBO)
                PopulateGrid()

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
        ControlMgr.SetEnableControl(Me, btnDelete_WRITE, True)
        ControlMgr.SetEnableControl(Me, btnNew_WRITE, True)
        ControlMgr.SetEnableControl(Me, btnCopy_WRITE, True)
        'Now disable depending on the object state
        If State.MyBO.IsNew Then
            ControlMgr.SetEnableControl(Me, btnDelete_WRITE, False)
            ControlMgr.SetEnableControl(Me, btnNew_WRITE, False)
            ControlMgr.SetEnableControl(Me, btnCopy_WRITE, False)
        End If
        ControlMgr.SetEnableControl(Me, Grid, True)
        'WRITE YOU OWN CODE HERE
    End Sub

    Protected Sub BindBoPropertiesToLabels()

        BindBOPropertyToLabel(State.MyBO, "DealerId", ddlDealer.CaptionLabel)
        BindBOPropertyToLabel(State.MyBO, "InboundFilename", moInboundLABEL)
        BindBOPropertyToLabel(State.MyBO, "LayoutCodeId", moLayoutCodeLABEL)
        BindBOPropertyToLabel(State.MyBO, "LogfileEmails", moLogEmailsLABEL)
        BindBOPropertyToLabel(State.MyBO, "NumFiles", moNumFilesLABEL)
        BindBOPropertyToLabel(State.MyBO, "OutputPath", moOutputPathLABEL)
        BindBOPropertyToLabel(State.MyBO, "TransallPackage", moPackageNameLABEL)
        BindBOPropertyToLabel(State.MyBO, "FtpSiteId", moFTPSiteLABEL)
        ClearGridHeadersAndLabelsErrSign()
    End Sub

    Protected Sub PopulateDropdowns()

        ' Me.BindListControlToDataView(Me.moLayoutCodeCBX, LookupListNew.DropdownLookupList(LookupListCache.LK_TRANSALL_LAYOUT_CODES, ElitaPlusIdentity.Current.ActiveUser.LanguageId)) 'TALAYOUT
        moLayoutCodeCBX.Populate(CommonConfigManager.Current.ListManager.GetList("TALAYOUT", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
                           {
                          .AddBlankItem = True
                            })
        ' Me.BindListControlToDataView(Me.moFTPSiteCBX, FtpSite.getList(String.Empty, String.Empty), , "ftp_site_id")
        Dim sortfunc As Func(Of DataElements.ListItem, String) = Function(li As DataElements.ListItem)
                                                                     Return li.Code.ToUpper() + li.Translation.ToUpper() + li.ExtendedCode.ToUpper()
                                                                 End Function
        moFTPSiteCBX.Populate(CommonConfigManager.Current.ListManager.GetList(ListCodes.FtpSite, Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
                           {
                          .AddBlankItem = True,
                          .SortFunc = sortfunc
                            })


    End Sub

    Protected Sub PopulateFormFromBOs()
        With State.MyBO

            PopulateControlFromBOProperty(moInboundTEXT, .InboundFilename)
            PopulateControlFromBOProperty(moLogEmailsTEXT, .LogfileEmails)
            PopulateControlFromBOProperty(moNumFilesTEXT, .NumFiles)
            PopulateControlFromBOProperty(moOutputPathTEXT, .OutputPath)
            PopulateControlFromBOProperty(moPackageNameTEXT, .TransallPackage)
            PopulateControlFromBOProperty(moLayoutCodeCBX, .LayoutCodeId)
            PopulateControlFromBOProperty(moFTPSiteCBX, .FtpSiteId)

            ddlDealer.SelectedIndex = ElitaPlusSearchPage.SELECTED_GUID_COL
            ddlDealer.SelectedGuid = State.MyBO.DealerId

            PopulateGrid()

        End With

    End Sub

    Protected Sub PopulateBOsFormFrom()
        With State.MyBO

            PopulateBOProperty(State.MyBO, "DealerId", ddlDealer.SelectedGuid)
            PopulateBOProperty(State.MyBO, "InboundFilename", moInboundTEXT)
            PopulateBOProperty(State.MyBO, "LayoutCodeId", moLayoutCodeCBX)
            PopulateBOProperty(State.MyBO, "LogfileEmails", moLogEmailsTEXT)
            PopulateBOProperty(State.MyBO, "NumFiles", moNumFilesTEXT)
            PopulateBOProperty(State.MyBO, "OutputPath", moOutputPathTEXT)
            PopulateBOProperty(State.MyBO, "TransallPackage", moPackageNameTEXT)
            PopulateBOProperty(State.MyBO, "FtpSiteId", moFTPSiteCBX)

        End With
        If ErrCollection.Count > 0 Then
            Throw New PopulateBOErrorException
        End If
    End Sub

    Private Sub PopulateDealer()
        Try
            Dim oDealerview As DataView = LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies)
            ddlDealer.SetControl(False,
                                        ddlDealer.MODES.NEW_MODE,
                                        True,
                                        oDealerview,
                                        "* " + TranslationBase.TranslateLabelOrMessage("DEALER"),
                                        True, True,
                                        ,
                                        "multipleDropControl_moMultipleColumnDrop",
                                        "multipleDropControl_moMultipleColumnDropDesc",
                                        "multipleDropControl_lb_DropDown",
                                        False,
                                        0)
            ddlDealer.SelectedIndex = ElitaPlusSearchPage.SELECTED_GUID_COL
        Catch ex As Exception
            ErrControllerMaster.AddError(ex.Message, False)
            ErrControllerMaster.Show()
        End Try
    End Sub

    Private Sub PopulateGrid()
        Dim dv As DataView

        Try
            ' If (Me.State.dvOutput Is Nothing) Then
            GetDv()
            ' End If
            State.dvOutput.Sort = SortDirection

            Grid.AutoGenerateColumns = False
            SortAndBindGrid()

        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try

    End Sub
    Private Sub SortAndBindGrid()

        If (State.dvOutput.Count = 0) Then

            State.bnoRow = True
            CreateHeaderForEmptyGrid(Grid, SortDirection)
        Else
            State.bnoRow = False
            Grid.Enabled = True
            Grid.DataSource = State.dvOutput
            HighLightSortColumn(Grid, SortDirection)
            Grid.DataBind()
        End If

        If Not Grid.BottomPagerRow.Visible Then Grid.BottomPagerRow.Visible = True

        ControlMgr.SetVisibleControl(Me, Grid, True)


        Session("recCount") = State.dvOutput.Count

        ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me, Grid)
    End Sub

    Protected Sub CreateNew()

        State.MyBO = New TransallMapping
        State.IsNew = True
        PopulateFormFromBOs()
        EnableDisableFields()
        SetButtonsState()
    End Sub

    Protected Sub CreateNewWithCopy()

        ddlDealer.SelectedIndex = ElitaPlusSearchPage.SELECTED_GUID_COL
        moInboundTEXT.Text = String.Empty
        moPackageNameTEXT.Text = String.Empty

        State.MyBO = New TransallMapping
        PopulateBOsFormFrom()
        EnableDisableFields()

    End Sub
    Protected Sub CheckIfComingFromDeleteConfirm()
        Dim confResponse As String = HiddenDeletePromptResponse.Value
        If confResponse IsNot Nothing AndAlso confResponse = MSG_VALUE_YES Then
            Dim tranMapOutBO As TransallMappingOut = New TransallMappingOut(State.TransallMappingOutId)
            If tranMapOutBO IsNot Nothing Then
                tranMapOutBO.Delete()
                tranMapOutBO.Save()
            End If
            State.TransallMappingOutId = Guid.Empty
            PopulateGrid()
        End If

        'Clean after consuming the action
        State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
        HiddenDeletePromptResponse.Value = ""
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
                    ErrControllerMaster.AddErrorAndShow(State.LastErrMsg)
            End Select
        End If
        'Clean after consuming the action
        State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
        HiddenSaveChangesPromptResponse.Value = ""
    End Sub

    Private Sub GetDv()

        State.dvOutput = TransallMappingOut.GetList(State.MyBO.Id)

    End Sub

    Private Sub ReturnFromEditing()

        Grid.EditIndex = NO_ROW_SELECTED_INDEX

        If Grid.PageCount = 0 Then
            'if returning to the "1st time in" screen
            ControlMgr.SetVisibleControl(Me, Grid, False)
        Else
            ControlMgr.SetVisibleControl(Me, Grid, True)
        End If

        State.IsEditMode = False
        PopulateGrid()
        State.PageIndex = Grid.PageIndex
        SetButtonsState()

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
            HandleErrors(ex, ErrControllerMaster)
            AddConfirmMsg(Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR, HiddenSaveChangesPromptResponse)
            State.ActionInProgress = ElitaPlusPage.DetailPageCommand.BackOnErr
            State.LastErrMsg = ErrControllerMaster.Text
        End Try
    End Sub

    Private Sub btnSave_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnSave_WRITE.Click
        Try
            PopulateBOsFormFrom()
            If State.MyBO.IsDirty Then
                State.MyBO.Save()
                State.HasDataChanged = True
                EnableDisableFields()
                If State.IsNew = True Then
                    State.IsNew = False
                End If
                SetButtonsState()
                AddInfoMsg(Message.SAVE_RECORD_CONFIRMATION)
            Else
                AddInfoMsg(Message.MSG_RECORD_NOT_SAVED)
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub

    Private Sub btnUndo_Write_Click(sender As System.Object, e As System.EventArgs) Handles btnUndo_Write.Click
        Try
            If Not State.MyBO.IsNew Then
                'Reload from the DB
                State.MyBO = New TransallMapping(State.MyBO.Id)
            Else
                State.MyBO = New TransallMapping
            End If
            PopulateFormFromBOs()
            EnableDisableFields()
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub

    Private Sub btnDelete_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnDelete_WRITE.Click

        Dim transMapOutIds As New ArrayList
        Dim i As Integer
        For i = 0 To State.dvOutput.Count - 1
            'transMapOutIds.Add(Me.State.dvOutput(i)(0).ToString)
            transMapOutIds.Add(New Guid(CType(State.dvOutput(i)(0), Byte())))
        Next
        State.MyBO.DetachTransMapOut(transMapOutIds)

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
            If State.MyBO.IsDirty Then
                AddConfirmMsg(Message.SAVE_CHANGES_PROMPT, HiddenSaveChangesPromptResponse)
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.New_
            Else
                CreateNew()
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub

    Private Sub btnCopy_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnCopy_WRITE.Click
        Try
            PopulateBOsFormFrom()
            If State.MyBO.IsDirty Then
                AddConfirmMsg(Message.SAVE_CHANGES_PROMPT, HiddenSaveChangesPromptResponse)
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.NewAndCopy
            Else
                CreateNewWithCopy()
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub

    Private Sub btnNewOutput_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnNewOutput_WRITE.Click
        Try
            State.IsEditMode = True
            State.IsGridVisible = True
            State.AddingNewRow = True
            AddNew()
            SetButtonsState()
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub

    Private Sub GridCancelButton_Click(sender As Object, e As System.EventArgs) Handles GridCancelButton.Click


        Try
            Grid.SelectedIndex = NO_ITEM_SELECTED_INDEX
            State.Canceling = True
            If (State.AddingNewRow) Then
                State.AddingNewRow = False
                State.dvOutput = Nothing
            End If
            ReturnFromEditing()
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try

    End Sub

    Private Sub GridSaveButton_WRITE_Click(sender As Object, e As System.EventArgs) Handles GridSaveButton_WRITE.Click

        Try
            AssignBOFromSelectedRecord()
            If (State.TransallOutBO.IsDirty) Then
                State.TransallOutBO.Save()
                '   Me.State.IsAfterSave = True
                State.AddingNewRow = False
                AddInfoMsg(MSG_RECORD_SAVED_OK)
                State.dvOutput = Nothing
                ReturnFromEditing()
            Else
                AddInfoMsg(MSG_RECORD_NOT_SAVED)
                ReturnFromEditing()
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try

    End Sub

    Private Sub SetButtonsState()

        Dim boolOn As Boolean = True

        If (State.IsEditMode) Then
            ControlMgr.SetEnableControl(Me, btnBack, Not boolOn)
            ControlMgr.SetEnableControl(Me, btnCopy_WRITE, Not boolOn)
            ControlMgr.SetEnableControl(Me, btnDelete_WRITE, Not boolOn)
            ControlMgr.SetEnableControl(Me, btnNew_WRITE, Not boolOn)
            ControlMgr.SetEnableControl(Me, btnSave_WRITE, Not boolOn)
            ControlMgr.SetEnableControl(Me, btnUndo_Write, Not boolOn)
            ControlMgr.SetVisibleControl(Me, GridSaveButton_WRITE, boolOn)
            ControlMgr.SetVisibleControl(Me, GridCancelButton, boolOn)
            ControlMgr.SetVisibleControl(Me, btnNewOutput_WRITE, Not boolOn)
            MenuEnabled = False
        Else
            ControlMgr.SetEnableControl(Me, btnBack, boolOn)
            ControlMgr.SetEnableControl(Me, btnCopy_WRITE, boolOn)
            ControlMgr.SetEnableControl(Me, btnDelete_WRITE, boolOn)
            ControlMgr.SetEnableControl(Me, btnNew_WRITE, boolOn)
            ControlMgr.SetEnableControl(Me, btnSave_WRITE, boolOn)
            ControlMgr.SetEnableControl(Me, btnUndo_Write, boolOn)
            ControlMgr.SetVisibleControl(Me, GridSaveButton_WRITE, Not boolOn)
            ControlMgr.SetVisibleControl(Me, GridCancelButton, Not boolOn)
            ControlMgr.SetVisibleControl(Me, btnNewOutput_WRITE, boolOn)
            MenuEnabled = True
        End If

        If State.IsNew = True Then
            ControlMgr.SetVisibleControl(Me, btnNewOutput_WRITE, False)
            ControlMgr.SetEnableControl(Me, btnCopy_WRITE, False)
            ControlMgr.SetEnableControl(Me, btnDelete_WRITE, False)
            ControlMgr.SetEnableControl(Me, btnNew_WRITE, False)
        Else
            ControlMgr.SetVisibleControl(Me, btnNewOutput_WRITE, True)
        End If
    End Sub


#End Region

#Region " Datagrid Related "
    Public Property SortDirection() As String
        Get
            Return ViewState("SortDirection").ToString
        End Get
        Set(value As String)
            ViewState("SortDirection") = value
        End Set
    End Property

    Private Sub Grid_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Grid.PageIndexChanging

        Try
            If (Not (State.IsEditMode)) Then
                State.PageIndex = e.NewPageIndex
                Grid.PageIndex = State.PageIndex
                PopulateGrid()
                Grid.SelectedIndex = NO_ITEM_SELECTED_INDEX
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try

    End Sub

    Protected Sub ItemCommand(source As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs)

        Try
            Dim index As Integer

            If (e.CommandName = EDIT_COMMAND) Then
                index = CInt(e.CommandArgument)
                State.IsEditMode = True
                State.TransallOutBO = New TransallMappingOut(New Guid(CType(Grid.Rows(index).Cells(TRANSALL_MAPPING_OUT_ID_COL).FindControl(CTL_TRANSALLMAPPINGOUTID_LABEL), Label).Text))
                State.TransallMappingOutId = State.TransallOutBO.Id
                State.editRowIndex = index
                Grid.EditIndex = index

                PopulateGrid()

                State.PageIndex = Grid.PageIndex

                'Disable all Edit and Delete icon buttons on the Grid
                SetGridControls(Grid, False)

                AssignSelectedRecordFromBO()

                'Set focus on the Description TextBox for the EditItemIndex row
                SetFocusOnEditableFieldInGrid(Grid, OUTPUT_MASK_COL, CTL_OUTPUTMASK_TEXT, index)


                SetButtonsState()

            ElseIf (e.CommandName = DELETE_COMMAND) Then
                index = CInt(e.CommandArgument)

                'Clear the SelectedItemStyle to remove the highlight from the previously saved row
                Grid.SelectedIndex = NO_ROW_SELECTED_INDEX

                State.TransallMappingOutId = New Guid(CType(Grid.Rows(index).Cells(TRANSALL_MAPPING_OUT_ID_COL).FindControl(CTL_TRANSALLMAPPINGOUTID_LABEL), Label).Text)
                DisplayMessage(Message.DELETE_RECORD_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenDeletePromptResponse)
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Delete
            End If

        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try

    End Sub

    'The Binding Logic is here
    Private Sub Grid_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowDataBound
        Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
        Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)

        If dvRow IsNot Nothing AndAlso Not State.bnoRow Then

            If itemType = ListItemType.Item OrElse itemType = ListItemType.AlternatingItem OrElse itemType = ListItemType.SelectedItem Then

                CType(e.Row.Cells(TRANSALL_MAPPING_OUT_ID_COL).FindControl(CTL_TRANSALLMAPPINGOUTID_LABEL), Label).Text = GetGuidStringFromByteArray(CType(dvRow(TransallMappingOut.TransallMappingOutDV.COL_TRANSALL_MAPPING_OUT_ID), Byte()))
                e.Row.Cells(TRANSALL_MAPPING_ID_COL).Text = GetGuidStringFromByteArray(CType(dvRow(TransallMappingOut.TransallMappingOutDV.COL_ACCT_TRANSALL_MAPPING_ID), Byte()))

                If (State.IsEditMode = True _
                        AndAlso State.TransallMappingOutId.ToString.Equals(GetGuidStringFromByteArray(CType(dvRow(TransallMappingOut.TransallMappingOutDV.COL_TRANSALL_MAPPING_OUT_ID), Byte())))) Then

                    CType(e.Row.Cells(OUTPUT_MASK_COL).FindControl(CTL_OUTPUTMASK_TEXT), TextBox).Text = dvRow(TransallMappingOut.TransallMappingOutDV.COL_ACCT_OUTPUT_MASK).ToString

                    'BindListControlToDataView(CType(e.Row.Cells(Me.LAYOUT_CODE_COL).FindControl(Me.CTL_LAYOUTCODE_DDL), DropDownList), LookupListNew.DropdownLookupList(LookupListCache.LK_TRANSALL_LAYOUT_CODES, ElitaPlusIdentity.Current.ActiveUser.LanguageId), , , False)
                    CType(e.Row.Cells(LAYOUT_CODE_COL).FindControl(CTL_LAYOUTCODE_DDL), DropDownList).Populate(CommonConfigManager.Current.ListManager.GetList("TALAYOUT", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions())
                    If dvRow(TransallMappingOut.TransallMappingOutDV.COL_LAYOUT_CODE_ID) IsNot DBNull.Value AndAlso Not GuidControl.ByteArrayToGuid(dvRow(TransallMappingOut.TransallMappingOutDV.COL_LAYOUT_CODE_ID)) = Guid.Empty Then
                        SetSelectedItem(CType(e.Row.Cells(LAYOUT_CODE_COL).FindControl(CTL_LAYOUTCODE_DDL), DropDownList), GetGuidStringFromByteArray(CType(dvRow(TransallMappingOut.TransallMappingOutDV.COL_LAYOUT_CODE_ID), Byte())))
                    End If


                Else
                    CType(e.Row.Cells(OUTPUT_MASK_COL).FindControl(CTL_OUTPUTMASK_LABEL), Label).Text = dvRow(TransallMappingOut.TransallMappingOutDV.COL_ACCT_OUTPUT_MASK).ToString

                    If dvRow(TransallMappingOut.TransallMappingOutDV.COL_LAYOUT_CODE_ID) IsNot DBNull.Value Then
                        CType(e.Row.Cells(LAYOUT_CODE_COL).FindControl(CTL_LAYOUTCODE_LABEL), Label).Text = LookupListNew.GetDescriptionFromId(LookupListNew.DropdownLookupList(LookupListCache.LK_TRANSALL_LAYOUT_CODES, ElitaPlusIdentity.Current.ActiveUser.LanguageId), New Guid(CType(dvRow(TransallMappingOut.TransallMappingOutDV.COL_LAYOUT_CODE_ID), Byte())))
                    End If

                End If
            End If
        End If
    End Sub
    Protected Sub ItemBound(source As Object, e As GridViewRowEventArgs) Handles Grid.RowDataBound

        Try
            If Not State.bnoRow Then
                BaseItemBound(source, e)
            End If

        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub

    Public Sub ItemCreated(sender As System.Object, e As System.Web.UI.WebControls.GridViewRowEventArgs)
        BaseItemCreated(sender, e)
    End Sub

    Protected Sub BindBoPropertiesToGridHeaders()
        BindBOPropertyToGridHeader(State.TransallOutBO, "LayoutCodeId", Grid.Columns(LAYOUT_CODE_COL))
        BindBOPropertyToGridHeader(State.TransallOutBO, "OutputMask", Grid.Columns(OUTPUT_MASK_COL))
        ClearGridViewHeadersAndLabelsErrSign()
    End Sub

    Private Sub SetFocusOnEditableFieldInGrid(grid As GridView, cellPosition As Integer, controlName As String, itemIndex As Integer)
        'Set focus on the Description TextBox for the EditItemIndex row
        Dim ctl As TextBox = CType(grid.Rows(itemIndex).Cells(cellPosition).FindControl(controlName), TextBox)
        SetFocus(ctl)
    End Sub


    Private Sub Grid_SortCommand(source As Object, e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles Grid.Sorting
        Try
            Dim spaceIndex As Integer = SortDirection.LastIndexOf(" ")


            If spaceIndex > 0 AndAlso SortDirection.Substring(0, spaceIndex).Equals(e.SortExpression) Then
                If SortDirection.EndsWith(" ASC") Then
                    SortDirection = e.SortExpression + " DESC"
                Else
                    SortDirection = e.SortExpression + " ASC"
                End If
            Else
                SortDirection = e.SortExpression + " ASC"
            End If

            State.PageIndex = 0
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub

    Private Sub AssignSelectedRecordFromBO()

        Dim gridRowIdx As Integer = Grid.EditIndex
        Try
            With State.TransallOutBO
                If .OutputMask IsNot Nothing Then
                    CType(Grid.Rows(gridRowIdx).Cells(OUTPUT_MASK_COL).FindControl(CTL_OUTPUTMASK_TEXT), TextBox).Text = .OutputMask
                End If

                Dim LayoutCodelst As DropDownList = CType(Grid.Rows(Grid.EditIndex).Cells(LAYOUT_CODE_COL).Controls(1), DropDownList)

                'Me.BindListControlToDataView(LayoutCodelst, LookupListNew.DropdownLookupList(LookupListCache.LK_TRANSALL_LAYOUT_CODES, ElitaPlusIdentity.Current.ActiveUser.LanguageId))
                LayoutCodelst.Populate(CommonConfigManager.Current.ListManager.GetList("TALAYOUT", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
                           {
                          .AddBlankItem = True
                            })
                SetSelectedItem(LayoutCodelst, .LayoutCodeId)

                CType(Grid.Rows(gridRowIdx).Cells(TRANSALL_MAPPING_OUT_ID_COL).FindControl(CTL_TRANSALLMAPPINGOUTID_LABEL), Label).Text = .Id.ToString
                '    CType(Me.Grid.Rows(gridRowIdx).Cells(Me.TRANSALL_MAPPING_ID_COL).FindControl(Me.CTL_TRANSALLMAPPINGID_LABEL), Label).Text = .TransallMappingId.ToString
            End With
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try

    End Sub

    Private Sub AddNew()

        Dim dv As DataView

        State.TransallOutBO = New TransallMappingOut
        State.TransallMappingOutId = State.TransallOutBO.Id
        State.TransallOutBO.TransallMappingId = State.MyBO.Id

        'Check if dvOutput is nothing.
        If State.dvOutput Is Nothing Then GetDv()

        State.dvOutput = State.TransallOutBO.GetNewDataViewRow(State.dvOutput, State.TransallMappingOutId, State.TransallOutBO)
        Grid.DataSource = State.dvOutput

        SetPageAndSelectedIndexFromGuid(State.dvOutput, State.TransallMappingOutId, Grid, State.PageIndex, State.IsEditMode)

        State.bnoRow = False
        Grid.DataBind()

        State.PageIndex = Grid.PageIndex

        SetGridControls(Grid, False)

        'Set focus on the BusinessUnit TextBox for the EditItemIndex row
        SetFocusOnEditableFieldInGrid(Grid, OUTPUT_MASK_COL, CTL_OUTPUTMASK_TEXT, Grid.EditIndex)

        AssignSelectedRecordFromBO()

        'Me.TranslateGridControls(Grid)
        SetButtonsState()
        ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me, Grid)
    End Sub

    Private Sub AssignBOFromSelectedRecord()

        Try
            With State.TransallOutBO

                .OutputMask = CType(Grid.Rows(Grid.EditIndex).Cells(OUTPUT_MASK_COL).FindControl(CTL_OUTPUTMASK_TEXT), TextBox).Text
                .LayoutCodeId = GetGuidFromString(CType(Grid.Rows(Grid.EditIndex).Cells(LAYOUT_CODE_COL).Controls(1), DropDownList).SelectedValue())

            End With
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try

    End Sub
#End Region



End Class