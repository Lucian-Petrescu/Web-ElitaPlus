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
        Public Sub New(ByVal LastOp As DetailPageCommand, ByVal curEditingBo As TransallMapping, ByVal hasDataChanged As Boolean)
            Me.LastOperation = LastOp
            Me.EditingBo = curEditingBo
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

    Private Sub Page_PageCall(ByVal CallFromUrl As String, ByVal CallingPar As Object) Handles MyBase.PageCall
        Try
            If Not Me.CallingParameters Is Nothing Then
                'Get the id from the parent
                Me.State.MyBO = New TransallMapping(CType(Me.CallingParameters, Guid))
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try

    End Sub

#End Region

#Region "Page Events"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Me.ErrControllerMaster.Clear_Hide()

        Try
            If Not Me.IsPostBack Then

                Me.SetFormTitle(PAGETITLE)
                Me.SetFormTab(PAGETAB)
                Me.SortDirection = TransallMappingOut.TransallMappingOutDV.COL_ACCT_OUTPUT_MASK

                Me.MenuEnabled = False
                Me.AddConfirmation(Me.btnDelete_WRITE, Message.DELETE_RECORD_PROMPT)
                If Me.State.MyBO Is Nothing Then
                    Me.State.MyBO = New TransallMapping
                    Me.State.IsNew = True
                    ControlMgr.SetVisibleControl(Me, btnNewOutput_WRITE, False)
                Else
                    Me.State.IsNew = False
                End If

                Me.PopulateDropdowns()
                Me.PopulateDealer()
                Me.PopulateFormFromBOs()
                Me.TranslateGridHeader(Me.Grid)
                Me.TranslateGridControls(Me.Grid)
                Me.EnableDisableFields()

                Me.SetButtonsState()

            End If

            BindBoPropertiesToLabels()
            BindBoPropertiesToGridHeaders()
            CheckIfComingFromSaveConfirm()
            CheckIfComingFromDeleteConfirm()
            If Not Me.IsPostBack Then
                Me.AddLabelDecorations(Me.State.MyBO)
                Me.PopulateGrid()

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
        ControlMgr.SetEnableControl(Me, btnDelete_WRITE, True)
        ControlMgr.SetEnableControl(Me, btnNew_WRITE, True)
        ControlMgr.SetEnableControl(Me, btnCopy_WRITE, True)
        'Now disable depending on the object state
        If Me.State.MyBO.IsNew Then
            ControlMgr.SetEnableControl(Me, btnDelete_WRITE, False)
            ControlMgr.SetEnableControl(Me, btnNew_WRITE, False)
            ControlMgr.SetEnableControl(Me, btnCopy_WRITE, False)
        End If
        ControlMgr.SetEnableControl(Me, Me.Grid, True)
        'WRITE YOU OWN CODE HERE
    End Sub

    Protected Sub BindBoPropertiesToLabels()

        Me.BindBOPropertyToLabel(Me.State.MyBO, "DealerId", Me.ddlDealer.CaptionLabel)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "InboundFilename", Me.moInboundLABEL)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "LayoutCodeId", Me.moLayoutCodeLABEL)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "LogfileEmails", Me.moLogEmailsLABEL)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "NumFiles", Me.moNumFilesLABEL)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "OutputPath", Me.moOutputPathLABEL)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "TransallPackage", Me.moPackageNameLABEL)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "FtpSiteId", Me.moFTPSiteLABEL)
        Me.ClearGridHeadersAndLabelsErrSign()
    End Sub

    Protected Sub PopulateDropdowns()

        ' Me.BindListControlToDataView(Me.moLayoutCodeCBX, LookupListNew.DropdownLookupList(LookupListCache.LK_TRANSALL_LAYOUT_CODES, ElitaPlusIdentity.Current.ActiveUser.LanguageId)) 'TALAYOUT
        Me.moLayoutCodeCBX.Populate(CommonConfigManager.Current.ListManager.GetList("TALAYOUT", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
                           {
                          .AddBlankItem = True
                            })
        ' Me.BindListControlToDataView(Me.moFTPSiteCBX, FtpSite.getList(String.Empty, String.Empty), , "ftp_site_id")
        Dim sortfunc As Func(Of DataElements.ListItem, String) = Function(li As DataElements.ListItem)
                                                                     Return li.Code.ToUpper() + li.Translation.ToUpper() + li.ExtendedCode.ToUpper()
                                                                 End Function
        Me.moFTPSiteCBX.Populate(CommonConfigManager.Current.ListManager.GetList(ListCodes.FtpSite, Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
                           {
                          .AddBlankItem = True,
                          .SortFunc = sortfunc
                            })


    End Sub

    Protected Sub PopulateFormFromBOs()
        With Me.State.MyBO

            Me.PopulateControlFromBOProperty(Me.moInboundTEXT, .InboundFilename)
            Me.PopulateControlFromBOProperty(Me.moLogEmailsTEXT, .LogfileEmails)
            Me.PopulateControlFromBOProperty(Me.moNumFilesTEXT, .NumFiles)
            Me.PopulateControlFromBOProperty(Me.moOutputPathTEXT, .OutputPath)
            Me.PopulateControlFromBOProperty(Me.moPackageNameTEXT, .TransallPackage)
            Me.PopulateControlFromBOProperty(Me.moLayoutCodeCBX, .LayoutCodeId)
            Me.PopulateControlFromBOProperty(Me.moFTPSiteCBX, .FtpSiteId)

            ddlDealer.SelectedIndex = ElitaPlusSearchPage.SELECTED_GUID_COL
            ddlDealer.SelectedGuid = Me.State.MyBO.DealerId

            Me.PopulateGrid()

        End With

    End Sub

    Protected Sub PopulateBOsFormFrom()
        With Me.State.MyBO

            Me.PopulateBOProperty(Me.State.MyBO, "DealerId", ddlDealer.SelectedGuid)
            Me.PopulateBOProperty(Me.State.MyBO, "InboundFilename", Me.moInboundTEXT)
            Me.PopulateBOProperty(Me.State.MyBO, "LayoutCodeId", Me.moLayoutCodeCBX)
            Me.PopulateBOProperty(Me.State.MyBO, "LogfileEmails", Me.moLogEmailsTEXT)
            Me.PopulateBOProperty(Me.State.MyBO, "NumFiles", Me.moNumFilesTEXT)
            Me.PopulateBOProperty(Me.State.MyBO, "OutputPath", Me.moOutputPathTEXT)
            Me.PopulateBOProperty(Me.State.MyBO, "TransallPackage", Me.moPackageNameTEXT)
            Me.PopulateBOProperty(Me.State.MyBO, "FtpSiteId", Me.moFTPSiteCBX)

        End With
        If Me.ErrCollection.Count > 0 Then
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
            Me.State.dvOutput.Sort = Me.SortDirection

            Me.Grid.AutoGenerateColumns = False
            Me.SortAndBindGrid()

        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try

    End Sub
    Private Sub SortAndBindGrid()

        If (Me.State.dvOutput.Count = 0) Then

            Me.State.bnoRow = True
            CreateHeaderForEmptyGrid(Grid, Me.SortDirection)
        Else
            Me.State.bnoRow = False
            Me.Grid.Enabled = True
            Me.Grid.DataSource = Me.State.dvOutput
            HighLightSortColumn(Grid, Me.SortDirection)
            Me.Grid.DataBind()
        End If

        If Not Grid.BottomPagerRow.Visible Then Grid.BottomPagerRow.Visible = True

        ControlMgr.SetVisibleControl(Me, Grid, True)


        Session("recCount") = Me.State.dvOutput.Count

        ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me, Grid)
    End Sub

    Protected Sub CreateNew()

        Me.State.MyBO = New TransallMapping
        Me.State.IsNew = True
        Me.PopulateFormFromBOs()
        Me.EnableDisableFields()
        SetButtonsState()
    End Sub

    Protected Sub CreateNewWithCopy()

        ddlDealer.SelectedIndex = ElitaPlusSearchPage.SELECTED_GUID_COL
        Me.moInboundTEXT.Text = String.Empty
        Me.moPackageNameTEXT.Text = String.Empty

        Me.State.MyBO = New TransallMapping
        Me.PopulateBOsFormFrom()
        Me.EnableDisableFields()

    End Sub
    Protected Sub CheckIfComingFromDeleteConfirm()
        Dim confResponse As String = Me.HiddenDeletePromptResponse.Value
        If Not confResponse Is Nothing AndAlso confResponse = Me.MSG_VALUE_YES Then
            Dim tranMapOutBO As TransallMappingOut = New TransallMappingOut(Me.State.TransallMappingOutId)
            If Not tranMapOutBO Is Nothing Then
                tranMapOutBO.Delete()
                tranMapOutBO.Save()
            End If
            Me.State.TransallMappingOutId = Guid.Empty
            Me.PopulateGrid()
        End If

        'Clean after consuming the action
        Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
        Me.HiddenDeletePromptResponse.Value = ""
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
                    Me.ErrControllerMaster.AddErrorAndShow(Me.State.LastErrMsg)
            End Select
        End If
        'Clean after consuming the action
        Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
        Me.HiddenSaveChangesPromptResponse.Value = ""
    End Sub

    Private Sub GetDv()

        Me.State.dvOutput = TransallMappingOut.GetList(Me.State.MyBO.Id)

    End Sub

    Private Sub ReturnFromEditing()

        Grid.EditIndex = NO_ROW_SELECTED_INDEX

        If Me.Grid.PageCount = 0 Then
            'if returning to the "1st time in" screen
            ControlMgr.SetVisibleControl(Me, Grid, False)
        Else
            ControlMgr.SetVisibleControl(Me, Grid, True)
        End If

        Me.State.IsEditMode = False
        Me.PopulateGrid()
        Me.State.PageIndex = Grid.PageIndex
        SetButtonsState()

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
            Me.HandleErrors(ex, Me.ErrControllerMaster)
            Me.AddConfirmMsg(Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR, Me.HiddenSaveChangesPromptResponse)
            Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.BackOnErr
            Me.State.LastErrMsg = Me.ErrControllerMaster.Text
        End Try
    End Sub

    Private Sub btnSave_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave_WRITE.Click
        Try
            Me.PopulateBOsFormFrom()
            If Me.State.MyBO.IsDirty Then
                Me.State.MyBO.Save()
                Me.State.HasDataChanged = True
                Me.EnableDisableFields()
                If Me.State.IsNew = True Then
                    Me.State.IsNew = False
                End If
                Me.SetButtonsState()
                Me.AddInfoMsg(Message.SAVE_RECORD_CONFIRMATION)
            Else
                Me.AddInfoMsg(Message.MSG_RECORD_NOT_SAVED)
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub

    Private Sub btnUndo_Write_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUndo_Write.Click
        Try
            If Not Me.State.MyBO.IsNew Then
                'Reload from the DB
                Me.State.MyBO = New TransallMapping(Me.State.MyBO.Id)
            Else
                Me.State.MyBO = New TransallMapping
            End If
            Me.PopulateFormFromBOs()
            Me.EnableDisableFields()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub

    Private Sub btnDelete_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete_WRITE.Click

        Dim transMapOutIds As New ArrayList
        Dim i As Integer
        For i = 0 To Me.State.dvOutput.Count - 1
            'transMapOutIds.Add(Me.State.dvOutput(i)(0).ToString)
            transMapOutIds.Add(New Guid(CType(Me.State.dvOutput(i)(0), Byte())))
        Next
        Me.State.MyBO.DetachTransMapOut(transMapOutIds)

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
            If Me.State.MyBO.IsDirty Then
                Me.AddConfirmMsg(Message.SAVE_CHANGES_PROMPT, Me.HiddenSaveChangesPromptResponse)
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.New_
            Else
                Me.CreateNew()
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub

    Private Sub btnCopy_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCopy_WRITE.Click
        Try
            Me.PopulateBOsFormFrom()
            If Me.State.MyBO.IsDirty Then
                Me.AddConfirmMsg(Message.SAVE_CHANGES_PROMPT, Me.HiddenSaveChangesPromptResponse)
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.NewAndCopy
            Else
                Me.CreateNewWithCopy()
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub

    Private Sub btnNewOutput_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNewOutput_WRITE.Click
        Try
            Me.State.IsEditMode = True
            Me.State.IsGridVisible = True
            Me.State.AddingNewRow = True
            AddNew()
            SetButtonsState()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub

    Private Sub GridCancelButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridCancelButton.Click


        Try
            Me.Grid.SelectedIndex = Me.NO_ITEM_SELECTED_INDEX
            Me.State.Canceling = True
            If (Me.State.AddingNewRow) Then
                Me.State.AddingNewRow = False
                Me.State.dvOutput = Nothing
            End If
            ReturnFromEditing()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try

    End Sub

    Private Sub GridSaveButton_WRITE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridSaveButton_WRITE.Click

        Try
            AssignBOFromSelectedRecord()
            If (Me.State.TransallOutBO.IsDirty) Then
                Me.State.TransallOutBO.Save()
                '   Me.State.IsAfterSave = True
                Me.State.AddingNewRow = False
                Me.AddInfoMsg(Me.MSG_RECORD_SAVED_OK)
                Me.State.dvOutput = Nothing
                Me.ReturnFromEditing()
            Else
                Me.AddInfoMsg(Me.MSG_RECORD_NOT_SAVED)
                Me.ReturnFromEditing()
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try

    End Sub

    Private Sub SetButtonsState()

        Dim boolOn As Boolean = True

        If (Me.State.IsEditMode) Then
            ControlMgr.SetEnableControl(Me, btnBack, Not boolOn)
            ControlMgr.SetEnableControl(Me, btnCopy_WRITE, Not boolOn)
            ControlMgr.SetEnableControl(Me, btnDelete_WRITE, Not boolOn)
            ControlMgr.SetEnableControl(Me, btnNew_WRITE, Not boolOn)
            ControlMgr.SetEnableControl(Me, btnSave_WRITE, Not boolOn)
            ControlMgr.SetEnableControl(Me, btnUndo_Write, Not boolOn)
            ControlMgr.SetVisibleControl(Me, GridSaveButton_WRITE, boolOn)
            ControlMgr.SetVisibleControl(Me, GridCancelButton, boolOn)
            ControlMgr.SetVisibleControl(Me, btnNewOutput_WRITE, Not boolOn)
            Me.MenuEnabled = False
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
            Me.MenuEnabled = True
        End If

        If Me.State.IsNew = True Then
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
        Set(ByVal value As String)
            ViewState("SortDirection") = value
        End Set
    End Property

    Private Sub Grid_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Grid.PageIndexChanging

        Try
            If (Not (Me.State.IsEditMode)) Then
                Me.State.PageIndex = e.NewPageIndex
                Me.Grid.PageIndex = Me.State.PageIndex
                Me.PopulateGrid()
                Me.Grid.SelectedIndex = Me.NO_ITEM_SELECTED_INDEX
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try

    End Sub

    Protected Sub ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)

        Try
            Dim index As Integer

            If (e.CommandName = Me.EDIT_COMMAND) Then
                index = CInt(e.CommandArgument)
                Me.State.IsEditMode = True
                Me.State.TransallOutBO = New TransallMappingOut(New Guid(CType(Me.Grid.Rows(index).Cells(Me.TRANSALL_MAPPING_OUT_ID_COL).FindControl(Me.CTL_TRANSALLMAPPINGOUTID_LABEL), Label).Text))
                Me.State.TransallMappingOutId = Me.State.TransallOutBO.Id
                Me.State.editRowIndex = index
                Me.Grid.EditIndex = index

                Me.PopulateGrid()

                Me.State.PageIndex = Grid.PageIndex

                'Disable all Edit and Delete icon buttons on the Grid
                SetGridControls(Me.Grid, False)

                Me.AssignSelectedRecordFromBO()

                'Set focus on the Description TextBox for the EditItemIndex row
                Me.SetFocusOnEditableFieldInGrid(Me.Grid, Me.OUTPUT_MASK_COL, Me.CTL_OUTPUTMASK_TEXT, index)


                Me.SetButtonsState()

            ElseIf (e.CommandName = Me.DELETE_COMMAND) Then
                index = CInt(e.CommandArgument)

                'Clear the SelectedItemStyle to remove the highlight from the previously saved row
                Grid.SelectedIndex = Me.NO_ROW_SELECTED_INDEX

                Me.State.TransallMappingOutId = New Guid(CType(Me.Grid.Rows(index).Cells(Me.TRANSALL_MAPPING_OUT_ID_COL).FindControl(Me.CTL_TRANSALLMAPPINGOUTID_LABEL), Label).Text)
                Me.DisplayMessage(Message.DELETE_RECORD_PROMPT, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Me.HiddenDeletePromptResponse)
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Delete
            End If

        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try

    End Sub

    'The Binding Logic is here
    Private Sub Grid_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowDataBound
        Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
        Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)

        If Not dvRow Is Nothing And Not Me.State.bnoRow Then

            If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then

                CType(e.Row.Cells(Me.TRANSALL_MAPPING_OUT_ID_COL).FindControl(Me.CTL_TRANSALLMAPPINGOUTID_LABEL), Label).Text = GetGuidStringFromByteArray(CType(dvRow(TransallMappingOut.TransallMappingOutDV.COL_TRANSALL_MAPPING_OUT_ID), Byte()))
                e.Row.Cells(Me.TRANSALL_MAPPING_ID_COL).Text = GetGuidStringFromByteArray(CType(dvRow(TransallMappingOut.TransallMappingOutDV.COL_ACCT_TRANSALL_MAPPING_ID), Byte()))

                If (Me.State.IsEditMode = True _
                        AndAlso Me.State.TransallMappingOutId.ToString.Equals(GetGuidStringFromByteArray(CType(dvRow(TransallMappingOut.TransallMappingOutDV.COL_TRANSALL_MAPPING_OUT_ID), Byte())))) Then

                    CType(e.Row.Cells(Me.OUTPUT_MASK_COL).FindControl(Me.CTL_OUTPUTMASK_TEXT), TextBox).Text = dvRow(TransallMappingOut.TransallMappingOutDV.COL_ACCT_OUTPUT_MASK).ToString

                    'BindListControlToDataView(CType(e.Row.Cells(Me.LAYOUT_CODE_COL).FindControl(Me.CTL_LAYOUTCODE_DDL), DropDownList), LookupListNew.DropdownLookupList(LookupListCache.LK_TRANSALL_LAYOUT_CODES, ElitaPlusIdentity.Current.ActiveUser.LanguageId), , , False)
                    CType(e.Row.Cells(Me.LAYOUT_CODE_COL).FindControl(Me.CTL_LAYOUTCODE_DDL), DropDownList).Populate(CommonConfigManager.Current.ListManager.GetList("TALAYOUT", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions())
                    If Not dvRow(TransallMappingOut.TransallMappingOutDV.COL_LAYOUT_CODE_ID) Is DBNull.Value AndAlso Not GuidControl.ByteArrayToGuid(dvRow(TransallMappingOut.TransallMappingOutDV.COL_LAYOUT_CODE_ID)) = Guid.Empty Then
                        Me.SetSelectedItem(CType(e.Row.Cells(Me.LAYOUT_CODE_COL).FindControl(Me.CTL_LAYOUTCODE_DDL), DropDownList), GetGuidStringFromByteArray(CType(dvRow(TransallMappingOut.TransallMappingOutDV.COL_LAYOUT_CODE_ID), Byte())))
                    End If


                Else
                    CType(e.Row.Cells(Me.OUTPUT_MASK_COL).FindControl(Me.CTL_OUTPUTMASK_LABEL), Label).Text = dvRow(TransallMappingOut.TransallMappingOutDV.COL_ACCT_OUTPUT_MASK).ToString

                    If Not dvRow(TransallMappingOut.TransallMappingOutDV.COL_LAYOUT_CODE_ID) Is DBNull.Value Then
                        CType(e.Row.Cells(Me.LAYOUT_CODE_COL).FindControl(Me.CTL_LAYOUTCODE_LABEL), Label).Text = LookupListNew.GetDescriptionFromId(LookupListNew.DropdownLookupList(LookupListCache.LK_TRANSALL_LAYOUT_CODES, ElitaPlusIdentity.Current.ActiveUser.LanguageId), New Guid(CType(dvRow(TransallMappingOut.TransallMappingOutDV.COL_LAYOUT_CODE_ID), Byte())))
                    End If

                End If
            End If
        End If
    End Sub
    Protected Sub ItemBound(ByVal source As Object, ByVal e As GridViewRowEventArgs) Handles Grid.RowDataBound

        Try
            If Not Me.State.bnoRow Then
                BaseItemBound(source, e)
            End If

        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub

    Public Sub ItemCreated(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
        BaseItemCreated(sender, e)
    End Sub

    Protected Sub BindBoPropertiesToGridHeaders()
        Me.BindBOPropertyToGridHeader(Me.State.TransallOutBO, "LayoutCodeId", Me.Grid.Columns(Me.LAYOUT_CODE_COL))
        Me.BindBOPropertyToGridHeader(Me.State.TransallOutBO, "OutputMask", Me.Grid.Columns(Me.OUTPUT_MASK_COL))
        Me.ClearGridViewHeadersAndLabelsErrSign()
    End Sub

    Private Sub SetFocusOnEditableFieldInGrid(ByVal grid As GridView, ByVal cellPosition As Integer, ByVal controlName As String, ByVal itemIndex As Integer)
        'Set focus on the Description TextBox for the EditItemIndex row
        Dim ctl As TextBox = CType(grid.Rows(itemIndex).Cells(cellPosition).FindControl(controlName), TextBox)
        SetFocus(ctl)
    End Sub


    Private Sub Grid_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles Grid.Sorting
        Try
            Dim spaceIndex As Integer = Me.SortDirection.LastIndexOf(" ")


            If spaceIndex > 0 AndAlso Me.SortDirection.Substring(0, spaceIndex).Equals(e.SortExpression) Then
                If Me.SortDirection.EndsWith(" ASC") Then
                    Me.SortDirection = e.SortExpression + " DESC"
                Else
                    Me.SortDirection = e.SortExpression + " ASC"
                End If
            Else
                Me.SortDirection = e.SortExpression + " ASC"
            End If

            Me.State.PageIndex = 0
            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub

    Private Sub AssignSelectedRecordFromBO()

        Dim gridRowIdx As Integer = Me.Grid.EditIndex
        Try
            With Me.State.TransallOutBO
                If Not .OutputMask Is Nothing Then
                    CType(Me.Grid.Rows(gridRowIdx).Cells(Me.OUTPUT_MASK_COL).FindControl(Me.CTL_OUTPUTMASK_TEXT), TextBox).Text = .OutputMask
                End If

                Dim LayoutCodelst As DropDownList = CType(Grid.Rows(Grid.EditIndex).Cells(Me.LAYOUT_CODE_COL).Controls(1), DropDownList)

                'Me.BindListControlToDataView(LayoutCodelst, LookupListNew.DropdownLookupList(LookupListCache.LK_TRANSALL_LAYOUT_CODES, ElitaPlusIdentity.Current.ActiveUser.LanguageId))
                LayoutCodelst.Populate(CommonConfigManager.Current.ListManager.GetList("TALAYOUT", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
                           {
                          .AddBlankItem = True
                            })
                Me.SetSelectedItem(LayoutCodelst, .LayoutCodeId)

                CType(Me.Grid.Rows(gridRowIdx).Cells(Me.TRANSALL_MAPPING_OUT_ID_COL).FindControl(Me.CTL_TRANSALLMAPPINGOUTID_LABEL), Label).Text = .Id.ToString
                '    CType(Me.Grid.Rows(gridRowIdx).Cells(Me.TRANSALL_MAPPING_ID_COL).FindControl(Me.CTL_TRANSALLMAPPINGID_LABEL), Label).Text = .TransallMappingId.ToString
            End With
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try

    End Sub

    Private Sub AddNew()

        Dim dv As DataView

        Me.State.TransallOutBO = New TransallMappingOut
        Me.State.TransallMappingOutId = Me.State.TransallOutBO.Id
        Me.State.TransallOutBO.TransallMappingId = Me.State.MyBO.Id

        'Check if dvOutput is nothing.
        If Me.State.dvOutput Is Nothing Then GetDv()

        Me.State.dvOutput = Me.State.TransallOutBO.GetNewDataViewRow(Me.State.dvOutput, Me.State.TransallMappingOutId, Me.State.TransallOutBO)
        Grid.DataSource = Me.State.dvOutput

        Me.SetPageAndSelectedIndexFromGuid(Me.State.dvOutput, Me.State.TransallMappingOutId, Me.Grid, Me.State.PageIndex, Me.State.IsEditMode)

        Me.State.bnoRow = False
        Grid.DataBind()

        Me.State.PageIndex = Grid.PageIndex

        SetGridControls(Me.Grid, False)

        'Set focus on the BusinessUnit TextBox for the EditItemIndex row
        Me.SetFocusOnEditableFieldInGrid(Me.Grid, Me.OUTPUT_MASK_COL, Me.CTL_OUTPUTMASK_TEXT, Me.Grid.EditIndex)

        Me.AssignSelectedRecordFromBO()

        'Me.TranslateGridControls(Grid)
        Me.SetButtonsState()
        ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me, Grid)
    End Sub

    Private Sub AssignBOFromSelectedRecord()

        Try
            With Me.State.TransallOutBO

                .OutputMask = CType(Me.Grid.Rows(Grid.EditIndex).Cells(Me.OUTPUT_MASK_COL).FindControl(Me.CTL_OUTPUTMASK_TEXT), TextBox).Text
                .LayoutCodeId = Me.GetGuidFromString(CType(Grid.Rows(Grid.EditIndex).Cells(Me.LAYOUT_CODE_COL).Controls(1), DropDownList).SelectedValue())

            End With
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try

    End Sub
#End Region



End Class