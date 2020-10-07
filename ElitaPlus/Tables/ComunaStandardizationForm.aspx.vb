Imports System.Security.Cryptography.X509Certificates
Imports Assurant.ElitaPlus.DALObjects
Imports System.Globalization
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports System.Threading
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms
Partial Public Class ComunaStandardizationForm
    Inherits ElitaPlusSearchPage

#Region "Constants"
    Public Const URL As String = "~/Tables/ComunaStandardizationForm.aspx"

    Public Const PAGETITLE As String = "COMUNA_STANDARDIZATION"
    Public Const PAGETAB As String = "TABLES"

    Private Const GRID_NO_SELECTEDITEM_INX As Integer = -1

    Private Const GRID_COL_EDIT_IDX As Integer = 0
    Private Const GRID_COL_DELETE_IDX As Integer = 1
    Private Const GRID_COL_ID_IDX As Integer = 2
    Private Const GRID_COL_COMUNA_ALIAS_IDX As Integer = 3
    Private Const GRID_COL_COMUNA_IDX As Integer = 4

    Private Const GRID_CTRL_NAME_COMUNA As String = "moComunaDropdown"
    Private Const GRID_CTRL_NAME_COMUNA_LABEL As String = "moComunaLabel"
    Private Const GRID_CTRL_NAME_COMUNA_ALIAS As String = "moComunaAliasTextBox"
    Private Const GRID_CTRL_NAME_COMUNA_ALIAS_LABEL As String = "moComunaAliasLabel"
    Private Const GRID_CTRL_NAME_COMUNA_ALIAS_ID As String = "IdLabel"
    Private _ComunaList As DataView
    Private IsReturnFromChild As Boolean = False
#End Region

#Region "Page Return Type"
    Public Class ReturnType
        Public LastOperation As DetailPageCommand
        Public EditingBo As String
        Public HasDataChanged As Boolean
        Public Sub New(LastOp As DetailPageCommand, curEditingBo As String, hasDataChanged As Boolean)
            LastOperation = LastOp
            EditingBo = curEditingBo
            Me.HasDataChanged = hasDataChanged
        End Sub
    End Class
#End Region

#Region "Page State"
    ' This class keeps the current state for the search page.
    Class MyState
        Public MyBO As ComunaStandardization
        Public ComunaAliasID As Guid
        Public PageIndex As Integer = 0
        Public PageSize As Integer = DEFAULT_PAGE_SIZE
        Public HasDataChanged As Boolean
        Public IsGridAddNew As Boolean = False
        Public IsGridVisible As Boolean
        Public GridEditIndex As Integer = NO_ITEM_SELECTED_INDEX
        Public searchDV As ComunaStandardization.ComunaStdSearchDV = Nothing
        Public SortExpression As String = ComunaStandardization.ComunaStdSearchDV.COL_COMUNA
        Public editDescription As String = ""

        Public searchComuna As String = ""
        Public searchComunaAlias As String = ""
        Public ActionInProgress As DetailPageCommand = DetailPageCommand.Nothing_
        Public IsCalledFromOtherForm As Boolean = False
        Public transactionLogHeaderId As String
    End Class

    Public Sub New()
        MyBase.New(New MyState)
    End Sub

    Protected Shadows ReadOnly Property State() As MyState
        Get
            Return CType(MyBase.State, MyState)
        End Get
    End Property

    Public ReadOnly Property IsGridInEditMode() As Boolean
        Get
            Return Grid.EditIndex > NO_ITEM_SELECTED_INDEX
        End Get
    End Property

    Public ReadOnly Property COMUNALIST() As DataView
        Get
            If _ComunaList Is Nothing Then
                _ComunaList = ComunaStandardization.GetComunaList()
            End If
            Return _ComunaList
        End Get
    End Property

    Private Sub Page_PageCall(CallFromUrl As String, CallingPar As Object) Handles MyBase.PageCall
        Try
            If CallFromUrl.ToUpper.Contains("TRANSEXCEPTIONDETAIL") Then
                btnBack.Visible = True
                State.IsCalledFromOtherForm = True
                MenuEnabled = False
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try

    End Sub
#End Region

#Region "Page Events"
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        ErrControllerMaster.Clear_Hide()
        Form.DefaultButton = btnSearch.UniqueID
        Try
            If Not IsPostBack Then
                SetFormTitle(PAGETITLE)
                SetFormTab(PAGETAB)
                TranslateGridHeader(Grid)
                If IsReturnFromChild Then
                    With State
                        moComunaAliasTextBox.Text = .searchComunaAlias
                        moComunaTextBox.Text = .searchComuna
                        .searchDV = Nothing
                        PopulateGrid()
                    End With
                End If
                If Not State.IsCalledFromOtherForm Then
                    MenuEnabled = True
                Else
                    MenuEnabled = False
                End If
            Else
                CheckIfComingFromDeleteConfirm()
            End If

        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
        ShowMissingTranslations(ErrControllerMaster)
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
        State.MyBO = New ComunaStandardization(State.ComunaAliasID)
        Try
            State.MyBO.Delete()
            State.MyBO.Save()
            'Me.AddInfoMsg(Me.MSG_RECORD_DELETED_OK)
        Catch ex As Exception
            State.MyBO.RejectChanges()
            Throw ex
        End Try

        State.PageIndex = Grid.PageIndex

        'Set the IsAfterSave flag to TRUE so that the Paging logic gets invoked
        State.searchDV = Nothing
        PopulateGrid()
        State.PageIndex = Grid.PageIndex
    End Sub
    Private Sub ComunaCodeForm_LoadComplete(sender As Object, e As System.EventArgs) Handles Me.LoadComplete
        SetControlState()
        If ErrControllerMaster.Visible Then
            If Grid.Visible AndAlso Grid.Rows.Count < 10 Then
                Dim fillerHight As Integer = 200
                fillerHight = fillerHight - Grid.Rows.Count * 20
                spanFiller.Text = "<tr><td colspan=""2"" style=""height:" & fillerHight & "px"">&nbsp;</td></tr>"
            End If
        Else
            spanFiller.Text = ""
        End If
    End Sub

    Private Sub ComunaCodeForm_PageReturn(ReturnFromUrl As String, ReturnPar As Object) Handles Me.PageReturn
        Try
            MenuEnabled = True
            IsReturnFromChild = True
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub
#End Region

#Region "Grid Handler"

    Private Sub Grid_PageIndexChanged(sender As Object, e As System.EventArgs) Handles Grid.PageIndexChanged
        Try
            State.PageIndex = Grid.PageIndex
            State.ComunaAliasID = Guid.Empty
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub

    Private Sub Grid_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Grid.PageIndexChanging
        Try
            Grid.PageIndex = e.NewPageIndex
            State.PageIndex = Grid.PageIndex
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub

    Private Sub Grid_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles Grid.RowCommand
        Try
            'ignore other commands
            If e.CommandName = "SelectAction" OrElse e.CommandName = "DeleteAction" Then
                Dim lblCtrl As Label
                Dim row As GridViewRow = CType(CType(e.CommandSource, Control).Parent.Parent, GridViewRow)
                Dim RowInd As Integer = row.RowIndex
                lblCtrl = CType(Grid.Rows(RowInd).Cells(GRID_COL_ID_IDX).FindControl(GRID_CTRL_NAME_COMUNA_ALIAS_ID), Label)
                State.ComunaAliasID = New Guid(lblCtrl.Text)
                State.MyBO = New ComunaStandardization(State.ComunaAliasID)
                lblCtrl = Nothing
                lblCtrl = CType(Grid.Rows(RowInd).Cells(GRID_COL_COMUNA_ALIAS_IDX).FindControl(GRID_CTRL_NAME_COMUNA_ALIAS), Label)
                If lblCtrl IsNot Nothing Then State.MyBO.ComunaAlias = lblCtrl.Text
                lblCtrl = Nothing
                If e.CommandName = "SelectAction" Then
                    Grid.EditIndex = RowInd
                    PopulateGrid()
                    'Disable all Edit and Delete icon buttons on the Grid
                    SetGridControls(Grid, False)
                ElseIf e.CommandName = "DeleteAction" Then
                    'Clear the SelectedItemStyle to remove the highlight from the previously saved row
                    Grid.SelectedIndex = GRID_NO_SELECTEDITEM_INX

                    DisplayMessage(Message.DELETE_RECORD_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenDeletePromptResponse)
                    State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Delete
                End If
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub

    Private Sub Grid_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowCreated
        Try
            BaseItemCreated(sender, e)
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub

    Private Sub Grid_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowDataBound
        Try
            Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
            Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
            Dim lblTemp As Label, ddl As DropDownList, txt As TextBox

            If (itemType = ListItemType.Item OrElse itemType = ListItemType.AlternatingItem OrElse itemType = ListItemType.SelectedItem) AndAlso e.Row.RowIndex <> -1 Then
                With e.Row
                    If .RowIndex = Grid.EditIndex Then
                        ddl = CType(e.Row.FindControl(GRID_CTRL_NAME_COMUNA), DropDownList)
                        If ddl IsNot Nothing Then
                            Try
                                'Me.BindListControlToDataView(ddl, COMUNALIST, "Description", "id", True) 'dll

                                ddl.Populate(GetComunaList.ToArray(), New PopulateOptions() With
                             {
                                  .AddBlankItem = True
                             })

                                SetSelectedItem(ddl, State.MyBO.ComunaCodeId)
                            Catch ex As Exception
                            End Try
                        End If
                        ddl = Nothing

                        txt = CType(e.Row.FindControl(GRID_CTRL_NAME_COMUNA_ALIAS), TextBox)
                        If txt IsNot Nothing Then
                            If State.IsGridAddNew Then
                                txt.Text = State.MyBO.ComunaAlias
                            End If
                            txt = Nothing
                        End If
                    End If
                End With
            End If
            BaseItemBound(sender, e)
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub
    Private Function GetComunaList() As Assurant.Elita.CommonConfiguration.DataElements.ListItem()
        Dim Index As Integer
        Dim oListContext As New Assurant.Elita.CommonConfiguration.ListContext

        Dim usercountries As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Countries

        Dim oComuna As New Collections.Generic.List(Of Assurant.Elita.CommonConfiguration.DataElements.ListItem)

        For Index = 0 To usercountries.Count - 1
            oListContext.CountryId = usercountries(Index)
            Dim oComunaByCountry As Assurant.Elita.CommonConfiguration.DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(ListCodes.ComunaCodeByCountry, context:=oListContext)
            If oComunaByCountry.Count > 0 Then
                If oComuna IsNot Nothing Then
                    oComuna.AddRange(oComunaByCountry)
                Else
                    oComuna = oComunaByCountry.Clone()
                End If

            End If
        Next

        Return oComuna.ToArray()

    End Function
    Private Sub Grid_Sorting(sender As Object, e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles Grid.Sorting
        Try
            Dim strSort As String = e.SortExpression
            With State
                If .SortExpression.StartsWith(e.SortExpression) Then
                    If Not .SortExpression.EndsWith(" DESC") Then
                        strSort = strSort & " DESC"
                    End If
                End If
                .SortExpression = strSort
            End With
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub

    Protected Sub cboPageSize_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
        Try
            State.PageSize = CType(cboPageSize.SelectedValue, Integer)
            State.PageIndex = NewCurrentPageIndex(Grid, State.searchDV.Count, State.PageSize)
            Grid.PageIndex = State.PageIndex
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub
#End Region

#Region "Helper functions"

    Public Sub PopulateGrid()
        Try
            With State
                If (.searchDV Is Nothing) Then
                    .searchDV = ComunaStandardization.LoadList(.searchComunaAlias, .searchComuna)
                End If
                If .IsGridAddNew Then
                    .PageIndex = Grid.PageIndex
                Else
                    .searchDV.Sort = .SortExpression
                End If
            End With

            Grid.PageSize = State.PageSize
            If State.searchDV.Count = 0 Then
                Dim dv As ComunaStandardization.ComunaStdSearchDV = State.searchDV.AddNewRowToEmptyDV()
                SetPageAndSelectedIndexFromGuid(dv, State.ComunaAliasID, Grid, State.PageIndex, (IsGridInEditMode OrElse State.IsGridAddNew))
                Grid.DataSource = dv
            Else
                SetPageAndSelectedIndexFromGuid(State.searchDV, State.ComunaAliasID, Grid, State.PageIndex, (IsGridInEditMode OrElse State.IsGridAddNew))
                Grid.DataSource = State.searchDV
            End If

            State.PageIndex = Grid.PageIndex
            Grid.DataBind()

            HighLightGridViewSortColumn(Grid, State.SortExpression)
            ControlMgr.SetVisibleControl(Me, Grid, True)
            ControlMgr.SetVisibleControl(Me, trPageSize, Grid.Visible)

            If Grid.Visible Then
                lblRecordCount.Text = State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
            End If
            If Not Grid.BottomPagerRow.Visible Then Grid.BottomPagerRow.Visible = True

            If State.searchDV.Count = 0 Then
                For Each gvRow As GridViewRow In Grid.Rows
                    gvRow.Visible = False
                    gvRow.Controls.Clear()
                Next
                DisplayMessage(Message.MSG_NO_RECORDS_FOUND, "", MSG_BTN_OK, MSG_TYPE_INFO)
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub

    Private Sub SetControlState()
        If (IsGridInEditMode) Then
            ControlMgr.SetVisibleControl(Me, btnNew, False)
            ControlMgr.SetVisibleControl(Me, btnCancel, True)
            ControlMgr.SetVisibleControl(Me, btnSave, True)
            ControlMgr.SetEnableControl(Me, btnSearch, False)
            ControlMgr.SetEnableControl(Me, btnClearSearch, False)
            MenuEnabled = False
            If (cboPageSize.Enabled) Then
                ControlMgr.SetEnableControl(Me, cboPageSize, False)
            End If
        Else
            ControlMgr.SetVisibleControl(Me, btnNew, True)
            ControlMgr.SetVisibleControl(Me, btnCancel, False)
            ControlMgr.SetVisibleControl(Me, btnSave, False)
            ControlMgr.SetEnableControl(Me, btnSearch, True)
            ControlMgr.SetEnableControl(Me, btnClearSearch, True)
            If Not State.IsCalledFromOtherForm Then MenuEnabled = True
            If Not (cboPageSize.Enabled) Then
                ControlMgr.SetEnableControl(Me, cboPageSize, True)
            End If
        End If
        cboPageSize.Visible = Grid.Visible
        lblRecordCount.Visible = Grid.Visible
    End Sub

    Private Function PopulateBOFromForm(ByRef errMsg As Collections.Generic.List(Of String)) As Boolean
        Dim blnSuccess As Boolean = True
        Dim ind As Integer = Grid.EditIndex
        With State.MyBO
            Dim ddl As DropDownList = CType(Grid.Rows(ind).Cells(GRID_COL_COMUNA_IDX).FindControl(GRID_CTRL_NAME_COMUNA), DropDownList)
            PopulateBOProperty(State.MyBO, "ComunaCodeId", ddl)
            If .ComunaCodeId = Guid.Empty Then
                blnSuccess = False
                errMsg.Add(TranslationBase.TranslateLabelOrMessage("Comuna") & ":" & TranslationBase.TranslateLabelOrMessage(ElitaPlus.Common.ErrorCodes.GUI_VALUE_MANDATORY_ERR))
            End If
            ddl = Nothing

            .ComunaAlias = CType(Grid.Rows(ind).Cells(GRID_COL_COMUNA_ALIAS_IDX).FindControl(GRID_CTRL_NAME_COMUNA_ALIAS), TextBox).Text.Trim.ToUpper
            If .ComunaAlias = String.Empty Then
                blnSuccess = False
                errMsg.Add(TranslationBase.TranslateLabelOrMessage("Comuna_Alias") & ":" & TranslationBase.TranslateLabelOrMessage(ElitaPlus.Common.ErrorCodes.GUI_VALUE_MANDATORY_ERR))
            End If
        End With
        Return blnSuccess
    End Function

    Private Sub RemoveNewRowFromSearchDV()
        Dim rowind As Integer = NO_ITEM_SELECTED_INDEX
        With State
            If .searchDV IsNot Nothing Then
                rowind = FindSelectedRowIndexFromGuid(.searchDV, State.ComunaAliasID)
            End If
        End With
        If rowind <> NO_ITEM_SELECTED_INDEX Then State.searchDV.Delete(rowind)
    End Sub
#End Region

#Region "Button click handlers"
    Protected Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        Try
            State.IsGridVisible = True
            State.MyBO = New ComunaStandardization
            State.ComunaAliasID = State.MyBO.Id
            If State.searchDV Is Nothing Then State.searchDV = ComunaStandardization.LoadList("", "")
            State.MyBO.AddNewRowToSearchDV(State.searchDV, State.MyBO)
            State.IsGridAddNew = True
            PopulateGrid()
            'Disable all Edit and Delete icon buttons on the Grid
            SetGridControls(Grid, False)
            'Set focus on the code TextBox for the EditItemIndex row
            Dim objCtrl As WebControl = CType(Grid.Rows(Grid.EditIndex).Cells(GRID_COL_COMUNA_ALIAS_IDX).FindControl(GRID_CTRL_NAME_COMUNA_ALIAS), WebControl)
            If objCtrl IsNot Nothing Then SetFocus(objCtrl)
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            Dim ErrMsg As New Collections.Generic.List(Of String)
            If PopulateBOFromForm(ErrMsg) Then
                With State
                    If (.MyBO.IsDirty) Then
                        .MyBO.Save()
                        If .IsGridAddNew AndAlso .searchDV.Count = 1 Then
                            Dim oCom As New ComunaCode(.MyBO.ComunaCodeId)
                            .searchComunaAlias = .MyBO.ComunaAlias
                            .searchComuna = oCom.Comuna
                        End If
                        AddInfoMsg(Message.SAVE_RECORD_CONFIRMATION)
                        State.searchDV = Nothing
                    Else
                        AddInfoMsg(Message.MSG_RECORD_NOT_SAVED)
                    End If
                    Grid.EditIndex = NO_ITEM_SELECTED_INDEX
                    .IsGridAddNew = False
                End With
            Else
                ErrControllerMaster.AddErrorAndShow(ErrMsg.ToArray, False)
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
        PopulateGrid()
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Try
            With State
                If .IsGridAddNew Then
                    RemoveNewRowFromSearchDV()
                    .editDescription = ""
                    .IsGridAddNew = False
                    Grid.PageIndex = .PageIndex
                End If
                .ComunaAliasID = Guid.Empty
            End With
            Grid.EditIndex = NO_ITEM_SELECTED_INDEX
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub

    Protected Sub btnClearSearch_Click(sender As Object, e As EventArgs) Handles btnClearSearch.Click
        Try
            moComunaAliasTextBox.Text = String.Empty
            moComunaTextBox.Text = String.Empty

            Grid.EditIndex = NO_ITEM_SELECTED_INDEX
            With State
                .ComunaAliasID = Guid.Empty
            End With
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Try
            With State
                .PageIndex = 0
                .ComunaAliasID = Guid.Empty
                .IsGridVisible = True
                .searchDV = Nothing
                .HasDataChanged = False
                .searchComunaAlias = moComunaAliasTextBox.Text.Trim
                .searchComuna = moComunaTextBox.Text.Trim
            End With
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub

    Private Sub btnBack_Click(sender As System.Object, e As System.EventArgs) Handles btnBack.Click
        Try
            ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, State.transactionLogHeaderId, False))
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub

#End Region



End Class