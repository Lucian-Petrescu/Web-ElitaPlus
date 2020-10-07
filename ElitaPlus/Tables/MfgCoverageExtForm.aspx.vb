Option Strict On
Option Explicit On
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports System.Threading
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms
Namespace Tables


    Partial Public Class MfgCoverageExtForm
        Inherits ElitaPlusSearchPage

        Public Shared URL As String = "MfgCoverageExtForm.aspx"

#Region "Constants"
        Private Const MSG_CONFIRM_PROMPT As String = "MSG_CONFIRM_PROMPT"
        Private Const MSG_RECORD_DELETED_OK As String = "MSG_RECORD_DELETED_OK"
        Private Const MSG_RECORD_SAVED_OK As String = "MSG_RECORD_SAVED_OK"
        Private Const MSG_RECORD_NOT_SAVED As String = "MSG_RECORD_NOT_SAVED"

        Private Const EDIT_COMMAND As String = "EditRecord"
        Private Const DELETE_COMMAND As String = "DeleteRecord"
        Private Const SORT_COMMAND As String = "Sort"

        Private Const NO_ROW_SELECTED_INDEX As Integer = -1

        Private Const MFG_COVERAGE_EXT_ID_COL As Integer = 2
        Private Const DEALER_COL As Integer = 3
        Private Const EXT_WARRANTY_COL As Integer = 4

        Private Const DEALER_DDL_CONTROL As String = "ddlstDealer"
        Private Const DEALER_LABEL_CONTROL As String = "lblDealer"
        Private Const EXT_WARRANTY_CONTROL As String = "txtExtWarranty"
        Private Const EXT_WARRANTY_LABEL_CONTROL As String = "lblExtWarranty"
        Private Const MFG_COVERAGE_EXT_ID_CONTROL As String = "lblMfgCoverageExtID"

        Public Const PAGETITLE As String = "EXT_WARRANTY"
        Public Const PAGETAB As String = "TABLES"
#End Region

        Private Class PageStatus

            Public Sub New()
                pageIndex = 0
                pageCount = 0
            End Sub

        End Class

#Region "Page Return Type"
        Public Class ReturnType
            Public mfgCoverageId As Guid
            Public RiskType As String
            Public Manufacturer As String
            Public Model As String

            Public Sub New(_mfgCoverageId As Guid, _riskType As String, _Manafacturer As String, _Model As String)
                mfgCoverageId = _mfgCoverageId
                RiskType = _riskType
                Manufacturer = _Manafacturer
                Model = _Model
            End Sub
        End Class
#End Region

#Region "Member Variables"

        Private Shared pageIndex As Integer
        Private Shared pageCount As Integer
#End Region

#Region "Properties"

        Public ReadOnly Property IsEditing() As Boolean
            Get
                IsEditing = (Grid.EditIndex > NO_ROW_SELECTED_INDEX)
            End Get
        End Property

        Private WriteOnly Property EnableControls() As Boolean
            Set(Value As Boolean)
                ControlMgr.SetEnableControl(Me, NewButton_WRITE, Value)
            End Set
        End Property

#End Region

#Region "Page State"
        Class MyState
            Public PageIndex As Integer = 0
            Public MyBO As MfgCoverageExt
            Public MfgCoverageExtId As Guid
            Public MfgCoverageId As Guid
            Public RiskType As String
            Public Manufacturer As String
            Public Model As String
            Public IsGridVisible As Boolean
            Public IsAfterSave As Boolean
            Public IsEditMode As Boolean
            Public AddingNewRow As Boolean
            Public Canceling As Boolean
            Public searchDV As DataView = Nothing
            Public YESNOdv As DataView = Nothing
            Public editRowIndex As Integer
            Public selectedPageSize As Integer = DEFAULT_PAGE_SIZE
            Public bnoRow As Boolean = False
            Public ActionInProgress As DetailPageCommand = DetailPageCommand.Nothing_
        End Class

        Public Sub New()
            MyBase.New(New MyState)
        End Sub

        Protected Shadows ReadOnly Property State() As MyState
            Get
                Return CType(MyBase.State, MyState)
            End Get
        End Property

#End Region

#Region "Button Click Handlers"

        Private Sub NewButton_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles NewButton_WRITE.Click

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

        Private Sub SaveButton_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles SaveButton_WRITE.Click

            Try
                AssignBOFromSelectedRecord()
                If (State.MyBO.IsDirty) Then
                    State.MyBO.Save()
                    State.IsAfterSave = True
                    State.AddingNewRow = False
                    AddInfoMsg(MSG_RECORD_SAVED_OK)
                    State.searchDV = Nothing
                    ReturnFromEditing()
                Else
                    AddInfoMsg(MSG_RECORD_NOT_SAVED)
                    ReturnFromEditing()
                End If
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try

        End Sub

        Private Sub CancelButton_Click(sender As System.Object, e As System.EventArgs) Handles CancelButton.Click

            Try
                Grid.SelectedIndex = NO_ITEM_SELECTED_INDEX
                State.Canceling = True
                If (State.AddingNewRow) Then
                    State.AddingNewRow = False
                    State.searchDV = Nothing
                End If
                ReturnFromEditing()
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try

        End Sub

        Private Sub BackButton_WRITE_Click(sender As Object, e As System.EventArgs) Handles BackButton_WRITE.Click

            Try
                ReturnToCallingPage(New ReturnType(State.MfgCoverageId, State.RiskType, State.Manufacturer, State.Model))
            Catch ex As Threading.ThreadAbortException
            End Try

        End Sub

#End Region

#Region "Private Methods"

        Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

            'Put user code to initialize the page here
            Try
                ErrControllerMaster.Clear_Hide()

                SetFormTitle(PAGETITLE)
                SetFormTab(PAGETAB)

                If Not Page.IsPostBack Then
                    SortDirection = MfgCoverageExt.COL_DEALER_NAME
                    SetGridItemStyleColor(Grid)
                    State.PageIndex = 0
                    If State.MyBO Is Nothing Then
                        State.MyBO = New MfgCoverageExt
                    End If
                    SetButtonsState()
                    TranslateGridHeader(Grid)
                    TranslateGridControls(Grid)
                    PopulateGrid()
                Else
                    CheckIfComingFromDeleteConfirm()
                End If
                BindBoPropertiesToGridHeaders()
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
            ShowMissingTranslations(ErrControllerMaster)
        End Sub

        Private Sub PopulateGrid()
            Dim dv As DataView

            Try
                If (State.searchDV Is Nothing) Then
                    State.searchDV = GetDV()
                End If
                State.searchDV.Sort = SortDirection
                State.IsGridVisible = True

                If (State.IsAfterSave) Then
                    State.IsAfterSave = False
                    SetPageAndSelectedIndexFromGuid(State.searchDV, State.MfgCoverageExtId, Grid, State.PageIndex)
                ElseIf (State.IsEditMode) Then
                    SetPageAndSelectedIndexFromGuid(State.searchDV, State.MfgCoverageExtId, Grid, State.PageIndex, State.IsEditMode)
                Else
                    SetPageAndSelectedIndexFromGuid(State.searchDV, Guid.Empty, Grid, State.PageIndex)
                End If

                Grid.AutoGenerateColumns = False
                SortAndBindGrid()

                If State.searchDV.Count = 0 Then
                    trPageSize.Visible = False
                Else
                    trPageSize.Visible = True
                End If

            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try

        End Sub

        Protected Sub CheckIfComingFromDeleteConfirm()
            Dim confResponse As String = HiddenDeletePromptResponse.Value
            If confResponse IsNot Nothing AndAlso confResponse = MSG_VALUE_YES Then
                If Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Delete Then
                    DoDelete()
                End If
                Select Case State.ActionInProgress
                    Case ElitaPlusPage.DetailPageCommand.Delete
                End Select
            ElseIf confResponse IsNot Nothing AndAlso confResponse = MSG_VALUE_NO Then
                Select Case State.ActionInProgress
                    Case ElitaPlusPage.DetailPageCommand.Delete
                End Select
            End If
            'Clean after consuming the action
            State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
            HiddenDeletePromptResponse.Value = ""
        End Sub

        Private Sub DoDelete()
            State.MyBO = New MfgCoverageExt(State.MfgCoverageExtId)

            Try
                State.MyBO.Delete()
                State.MyBO.Save()
            Catch ex As Exception
                State.MyBO.RejectChanges()
                Throw ex
            End Try

            State.PageIndex = Grid.PageIndex

            'Set the IsAfterSave flag to TRUE so that the Paging logic gets invoked
            State.IsAfterSave = True
            State.searchDV = Nothing
            PopulateGrid()
            State.PageIndex = Grid.PageIndex
        End Sub

        Private Function GetDV() As DataView

            Dim dv As DataView

            State.searchDV = GetGridDataView()
            State.searchDV.Sort = Grid.DataMember()

            Return (State.searchDV)

        End Function

        Private Function GetGridDataView() As DataView

            With State
                Return (MfgCoverageExt.LoadList(State.MfgCoverageId))
            End With

        End Function

        Private Sub PopulateDealerDropdown(listCtl As System.Web.UI.WebControls.DropDownList)

            Try
                'Dim dealerDv As DataView = MfgCoverageExt.GetAvailableDealers(Me.State.MfgCoverageId, Me.State.MyBO.DealerId) 'ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
                '  BindListControlToDataView(listCtl, dealerDv, "DEALER_NAME", "DEALER_ID", True) 'DealerByCompanyGroupMfgCoverage
                Dim listcontext As ListContext = New ListContext()
                listcontext.CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
                listcontext.DealerId = State.MyBO.DealerId
                listcontext.ManufacturerId = State.MfgCoverageId
                Dim compLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList(ListCodes.DealerByCompanyGroupMfgCoverage, Thread.CurrentPrincipal.GetLanguageCode(), listcontext)
                listCtl.Populate(compLkl, New PopulateOptions() With
                     {
                    .AddBlankItem = True
                     })
            Catch ex As Exception
                ErrControllerMaster.AddErrorAndShow(ex.Message)
                EnableControls = False
                Exit Sub
            End Try


        End Sub

        Private Sub AddNew()

            If State.searchDV Is Nothing Then
                State.searchDV = GetGridDataView()
            End If


            State.MyBO = New MfgCoverageExt
            State.MfgCoverageExtId = State.MyBO.Id

            State.searchDV = State.MyBO.GetNewDataViewRow(State.searchDV, State.MfgCoverageExtId, State.MyBO)
            Grid.DataSource = State.searchDV

            SetPageAndSelectedIndexFromGuid(State.searchDV, State.MfgCoverageExtId, Grid, State.PageIndex, State.IsEditMode)

            State.bnoRow = False
            Grid.DataBind()

            State.PageIndex = Grid.PageIndex

            SetGridControls(Grid, False)

            'Set focus on the Dealer Dropdown for the EditItemIndex row
            SetFocusOnEditableFieldInGrid(Grid, DEALER_COL, DEALER_DDL_CONTROL, Grid.EditIndex)

            AssignSelectedRecordFromBO()

            'Me.TranslateGridControls(Grid)
            SetButtonsState()
            ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me, Grid)
        End Sub

        Private Sub SortAndBindGrid()
            State.PageIndex = Grid.PageIndex

            If (State.searchDV.Count = 0) Then

                State.bnoRow = True
                CreateHeaderForEmptyGrid(Grid, SortDirection)
            Else
                State.bnoRow = False
                Grid.Enabled = True
                Grid.DataSource = State.searchDV
                HighLightSortColumn(Grid, SortDirection)
                Grid.DataBind()
            End If

            If Not Grid.BottomPagerRow.Visible Then Grid.BottomPagerRow.Visible = True

            ControlMgr.SetVisibleControl(Me, Grid, State.IsGridVisible)

            Session("recCount") = State.searchDV.Count

            If Grid.Visible Then
                If (State.AddingNewRow) Then
                    lblRecordCount.Text = (State.searchDV.Count - 1) & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                Else
                    lblRecordCount.Text = State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                End If
            End If
            ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me, Grid)
        End Sub

        Private Sub AssignBOFromSelectedRecord()

            Dim extWarr As Long

            Try
                With State.MyBO
                    .MfgCoverageId = State.MfgCoverageId
                    .DealerId = GetGuidFromString(CType(Grid.Rows(Grid.EditIndex).Cells(DEALER_COL).FindControl(DEALER_DDL_CONTROL), DropDownList).SelectedValue())
                    If Long.TryParse(CType(Grid.Rows(Grid.EditIndex).Cells(EXT_WARRANTY_COL).FindControl(EXT_WARRANTY_CONTROL), TextBox).Text, extWarr) Then
                        .ExtWarranty = extWarr
                    Else
                        .ExtWarranty = 0
                    End If
                End With
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try

        End Sub

        Private Sub AssignSelectedRecordFromBO()

            Dim gridRowIdx As Integer = Grid.EditIndex
            Try
                With State.MyBO
                    If .ExtWarranty IsNot Nothing Then
                        CType(Grid.Rows(gridRowIdx).Cells(EXT_WARRANTY_COL).FindControl(EXT_WARRANTY_CONTROL), TextBox).Text = .ExtWarranty.ToString
                    End If

                    Dim dealerLst As DropDownList = CType(Grid.Rows(Grid.EditIndex).Cells(DEALER_COL).FindControl(DEALER_DDL_CONTROL), DropDownList)

                    PopulateDealerDropdown(dealerLst)
                    SetSelectedItem(dealerLst, .DealerId)

                    CType(Grid.Rows(gridRowIdx).Cells(MFG_COVERAGE_EXT_ID_COL).FindControl(MFG_COVERAGE_EXT_ID_CONTROL), Label).Text = .Id.ToString
                End With
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try

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

        Private Sub SetButtonsState()
            If (State.IsEditMode) Then
                ControlMgr.SetVisibleControl(Me, SaveButton_WRITE, True)
                ControlMgr.SetVisibleControl(Me, CancelButton, True)
                ControlMgr.SetVisibleControl(Me, NewButton_WRITE, False)
                ControlMgr.SetVisibleControl(Me, BackButton_WRITE, False)
                MenuEnabled = False
                If (cboPageSize.Visible) Then
                    ControlMgr.SetEnableControl(Me, cboPageSize, False)
                End If
            Else
                ControlMgr.SetVisibleControl(Me, SaveButton_WRITE, False)
                ControlMgr.SetVisibleControl(Me, CancelButton, False)
                ControlMgr.SetVisibleControl(Me, NewButton_WRITE, True)
                ControlMgr.SetVisibleControl(Me, BackButton_WRITE, True)
                MenuEnabled = True
                If (cboPageSize.Visible) Then
                    ControlMgr.SetEnableControl(Me, cboPageSize, True)
                End If
            End If
        End Sub

        Private Sub Grid_PageSizeChanged(source As Object, e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
            Try
                Grid.PageIndex = NewCurrentPageIndex(Grid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
                State.selectedPageSize = CType(cboPageSize.SelectedValue, Integer)
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

        Private Sub Page_PageCall(CallFromUrl As String, CallingPar As Object) Handles MyBase.PageCall
            Try
                If CallingParameters IsNot Nothing Then

                    Dim _ret As ReturnType = CType(CallingParameters, ReturnType)

                    State.MfgCoverageId = _ret.mfgCoverageId
                    State.Model = _ret.Model
                    State.RiskType = _ret.RiskType
                    State.Manufacturer = _ret.Manufacturer

                End If
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
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
                    State.MfgCoverageExtId = New Guid(CType(Grid.Rows(index).Cells(MFG_COVERAGE_EXT_ID_COL).FindControl(MFG_COVERAGE_EXT_ID_CONTROL), Label).Text)
                    State.MyBO = New MfgCoverageExt(State.MfgCoverageExtId)

                    PopulateGrid()

                    State.PageIndex = Grid.PageIndex

                    'Disable all Edit and Delete icon buttons on the Grid
                    SetGridControls(Grid, False)

                    'Set focus on the Dealer dropdown for the EditItemIndex row
                    SetFocusOnEditableFieldInGrid(Grid, DEALER_COL, DEALER_DDL_CONTROL, index)

                    AssignSelectedRecordFromBO()

                    SetButtonsState()

                ElseIf (e.CommandName = DELETE_COMMAND) Then
                    index = CInt(e.CommandArgument)

                    'Clear the SelectedItemStyle to remove the highlight from the previously saved row
                    Grid.SelectedIndex = NO_ROW_SELECTED_INDEX

                    State.MfgCoverageExtId = New Guid(CType(Grid.Rows(index).Cells(MFG_COVERAGE_EXT_ID_COL).FindControl(MFG_COVERAGE_EXT_ID_CONTROL), Label).Text)
                    DisplayMessage(Message.DELETE_RECORD_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenDeletePromptResponse)
                    State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Delete
                End If

            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try

        End Sub

        Private Sub Grid_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowDataBound
            Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
            Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)

            If dvRow IsNot Nothing AndAlso Not State.bnoRow Then

                If itemType = ListItemType.Item OrElse itemType = ListItemType.AlternatingItem OrElse itemType = ListItemType.SelectedItem Then
                    CType(e.Row.Cells(MFG_COVERAGE_EXT_ID_COL).FindControl(MFG_COVERAGE_EXT_ID_CONTROL), Label).Text = GetGuidStringFromByteArray(CType(dvRow(MfgCoverageExt.COL_MFG_COVERAGE_EXT_ID), Byte()))

                    If (State.IsEditMode = True _
                            AndAlso State.MfgCoverageExtId.ToString.Equals(GetGuidStringFromByteArray(CType(dvRow(MfgCoverageExt.COL_MFG_COVERAGE_EXT_ID), Byte())))) Then

                        CType(e.Row.Cells(EXT_WARRANTY_COL).FindControl(EXT_WARRANTY_CONTROL), TextBox).Text = dvRow(MfgCoverageExt.COL_EXT_WARRANTY).ToString

                        Dim dlrId() As Byte
                        If dvRow(MfgCoverageExt.COL_DEALER_ID) Is DBNull.Value Then
                            dlrId = Nothing
                        Else
                            dlrId = CType(dvRow(MfgCoverageExt.COL_DEALER_ID), Byte())
                        End If
                        Dim dvDealer As DataView = MfgCoverageExt.GetAvailableDealers(State.MfgCoverageId, CType(If(dlrId Is Nothing, Guid.Empty, GuidControl.ByteArrayToGuid(dlrId)), Guid))
                        ' BindListControlToDataView(CType(e.Row.Cells(Me.DEALER_COL).FindControl(Me.DEALER_DDL_CONTROL), DropDownList), dvDealer, MfgCoverageExt.COL_DEALER_NAME, MfgCoverageExt.COL_DEALER_ID, True)
                        Dim listcontext As ListContext = New ListContext()
                        listcontext.CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
                        listcontext.DealerId = CType(If(dlrId Is Nothing, Guid.Empty, GuidControl.ByteArrayToGuid(dlrId)), Guid)
                        listcontext.ManufacturerId = State.MfgCoverageId
                        Dim compLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList(ListCodes.DealerByCompanyGroupMfgCoverage, Thread.CurrentPrincipal.GetLanguageCode(), listcontext)
                        CType(e.Row.Cells(DEALER_COL).FindControl(DEALER_DDL_CONTROL), DropDownList).Populate(compLkl, New PopulateOptions() With
                     {
                    .AddBlankItem = True
                     })
                        If dvRow(MfgCoverageExt.COL_DEALER_ID) IsNot DBNull.Value Then
                            SetSelectedItem(CType(e.Row.Cells(DEALER_COL).FindControl(DEALER_DDL_CONTROL), DropDownList), GuidControl.ByteArrayToGuid(dlrId).ToString)
                        End If

                    Else

                        CType(e.Row.Cells(DEALER_COL).FindControl(DEALER_LABEL_CONTROL), Label).Text = dvRow(MfgCoverageExt.COL_DEALER_NAME).ToString
                        CType(e.Row.Cells(EXT_WARRANTY_COL).FindControl(EXT_WARRANTY_LABEL_CONTROL), Label).Text = dvRow(MfgCoverageExt.COL_EXT_WARRANTY).ToString

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
            BindBOPropertyToGridHeader(State.MyBO, "ExtWarranty", Grid.Columns(EXT_WARRANTY_COL))
            BindBOPropertyToGridHeader(State.MyBO, "DealerId", Grid.Columns(DEALER_COL))
            ClearGridViewHeadersAndLabelsErrSign()
        End Sub

        Private Sub SetFocusOnEditableFieldInGrid(grid As GridView, cellPosition As Integer, controlName As String, itemIndex As Integer)
            'Set focus on the Description TextBox for the EditItemIndex row
            Dim ctl As DropDownList = CType(grid.Rows(itemIndex).Cells(cellPosition).FindControl(controlName), DropDownList)
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
#End Region

    End Class

End Namespace