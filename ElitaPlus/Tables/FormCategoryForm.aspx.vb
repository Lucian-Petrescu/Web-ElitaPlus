Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports Assurant.Elita.Web.Forms
Imports System.Threading
Partial Public Class FormCategoryForm
    Inherits ElitaPlusSearchPage

#Region "Constants"
    Public Const URL As String = "Tables/FormCategoryForm.aspx"
    Public Const PAGETITLE As String = "FORM_CATEGORY"
    Public Const PAGETAB As String = "ADMIN"

    Public Const ERR_MSG_FORM_CATEGORY_IN_USE As String = "FORM_CATEGORY_IN_USE"

    Private Const GRID_NO_SELECTEDITEM_INX As Integer = -1

    Private Const GRID_COL_EDIT_IDX As Integer = 0
    Private Const GRID_COL_DELETE_IDX As Integer = 1
    Private Const GRID_COL_FORM_CATEGORY_ID_IDX As Integer = 2
    Private Const GRID_COL_TAB_IDX As Integer = 3
    Private Const GRID_COL_CODE_IDX As Integer = 4
    Private Const GRID_COL_DESC_IDX As Integer = 5

    Private Const GRID_CTRL_NAME_FORM_CATEGORY_ID As String = "lblFormCategoryID"
    Private Const GRID_CTRL_NAME_TAB As String = "ddlTab"
    Private Const GRID_CTRL_NAME_TAB_Label As String = "lblTab"
    Private Const GRID_CTRL_NAME_CODE As String = "txtCode"
    Private Const GRID_CTRL_NAME_CODE_Label As String = "lblCode"
    Private Const GRID_CTRL_NAME_DESC As String = "txtDesc"
    Private Const GRID_CTRL_NAME_DESC_Label As String = "lblDesc"
    'Private _TabList As DataView
    Private IsReturnFromChild As Boolean = False
#End Region

#Region "Page State"
    ' This class keeps the current state for the search page.
    Class MyState
        Public MyBO As FormCategory
        Public FormCategoryID As Guid
        Public PageIndex As Integer = 0
        Public PageSize As Integer = DEFAULT_PAGE_SIZE
        Public HasDataChanged As Boolean
        Public IsGridAddNew As Boolean = False
        Public IsGridVisible As Boolean
        Public GridEditIndex As Integer = NO_ITEM_SELECTED_INDEX
        Public searchDV As FormCategory.FormCategorySearchDV = Nothing
        Public SortExpression As String = FormCategory.FormCategorySearchDV.COL_CODE
        Public editDescription As String = ""

        Public searchTab As Guid = Guid.Empty
        Public searchCode As String = ""
        Public searchDesc As String = ""

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
            Return Me.Grid.EditIndex > Me.NO_ITEM_SELECTED_INDEX
        End Get
    End Property

    'Public ReadOnly Property TABLIST() As DataView
    '    Get
    '        If _TabList Is Nothing Then
    '            _TabList = FormCategory.getTabList()
    '        End If
    '        Return _TabList
    '    End Get
    'End Property
#End Region

#Region "Page Events"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.ErrControllerMaster.Clear_Hide()
        Me.Form.DefaultButton = btnSearch.UniqueID
        Try
            If Not Me.IsPostBack Then
                Me.SetFormTitle(PAGETITLE)
                Me.SetFormTab(PAGETAB)
                populateSearchControls()
                TranslateGridHeader(Grid)
                If IsReturnFromChild Then
                    With State
                        txtCode.Text = .searchCode
                        txtDesc.Text = .searchDesc
                        SetSelectedItem(ddlTab, .searchTab)
                        .searchDV = Nothing
                        PopulateGrid()
                    End With
                End If
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
        Me.ShowMissingTranslations(Me.ErrControllerMaster)
    End Sub

    Private Sub FormCategoryForm_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete
        SetControlState()
        If ErrControllerMaster.Visible Then
            If Grid.Visible And Grid.Rows.Count < 10 Then
                Dim fillerHight As Integer = 200
                fillerHight = fillerHight - Grid.Rows.Count * 20
                Me.spanFiller.Text = "<tr><td colspan=""2"" style=""height:" & fillerHight & "px"">&nbsp;</td></tr>"
            End If
        Else
            Me.spanFiller.Text = ""
        End If
    End Sub

    Private Sub FormCategoryForm_PageReturn(ByVal ReturnFromUrl As String, ByVal ReturnPar As Object) Handles Me.PageReturn
        Try
            Me.MenuEnabled = True
            IsReturnFromChild = True
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub
#End Region

#Region "Grid Handler"

    Private Sub Grid_PageIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Grid.PageIndexChanged
        Try
            Me.State.PageIndex = Grid.PageIndex
            Me.State.FormCategoryID = Guid.Empty
            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub

    Private Sub Grid_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Grid.PageIndexChanging
        Try
            Grid.PageIndex = e.NewPageIndex
            State.PageIndex = Grid.PageIndex
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub

    Private Sub Grid_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles Grid.RowCommand
        Try
            'ignore other commands
            If e.CommandName = "SelectAction" OrElse e.CommandName = "DeleteAction" Then
                Dim lblCtrl As Label
                Dim row As GridViewRow = CType(CType(e.CommandSource, Control).Parent.Parent, GridViewRow)
                Dim RowInd As Integer = row.RowIndex
                lblCtrl = CType(Grid.Rows(RowInd).Cells(GRID_COL_FORM_CATEGORY_ID_IDX).FindControl(GRID_CTRL_NAME_FORM_CATEGORY_ID), Label)
                State.FormCategoryID = New Guid(lblCtrl.Text)
                State.MyBO = New FormCategory(State.FormCategoryID)
                lblCtrl = Nothing
                lblCtrl = CType(Grid.Rows(RowInd).Cells(GRID_COL_DESC_IDX).FindControl(GRID_CTRL_NAME_DESC_Label), Label)
                If Not lblCtrl Is Nothing Then State.MyBO.Description = lblCtrl.Text
                lblCtrl = Nothing
                If e.CommandName = "SelectAction" Then
                    Grid.EditIndex = RowInd
                    PopulateGrid()
                    'Disable all Edit and Delete icon buttons on the Grid
                    SetGridControls(Me.Grid, False)
                ElseIf e.CommandName = "DeleteAction" Then
                    Try
                        Dim strTemp As String, intFormCnt As Integer
                        strTemp = State.searchDV.Item(RowInd)(FormCategory.FormCategorySearchDV.COL_FORM_COUNT).ToString()
                        If Not Integer.TryParse(strTemp, intFormCnt) Then intFormCnt = 0
                        If intFormCnt = 0 Then
                            Dim guidDictItemID As Guid = State.MyBO.DictItemId
                            Me.State.MyBO.Delete()
                            Me.State.MyBO.SaveDelete(guidDictItemID)
                            State.searchDV.Delete(RowInd)
                            PopulateGrid()
                        Else
                            Dim ErrMsg As New Collections.Generic.List(Of String)
                            ErrMsg.Add(TranslationBase.TranslateLabelOrMessage(ERR_MSG_FORM_CATEGORY_IN_USE))
                            Me.ErrControllerMaster.AddErrorAndShow(ErrMsg.ToArray, False)
                        End If
                    Catch ex As Exception
                        Me.State.MyBO.RejectChanges()
                        Throw ex
                    End Try
                End If
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub

    Private Sub Grid_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowCreated
        Try
            BaseItemCreated(sender, e)
        Catch ex As Exception
            HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub

    Private Sub Grid_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowDataBound
        Try
            Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
            Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
            Dim lblTemp As Label, ddl As DropDownList, txt As TextBox

            If (itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem) AndAlso e.Row.RowIndex <> -1 Then
                With e.Row
                    Dim intFormCnt As Integer = 0
                    Integer.TryParse(dvRow(FormCategory.FormCategorySearchDV.COL_FORM_COUNT).ToString, intFormCnt)

                    If .RowIndex = Grid.EditIndex Then
                        ddl = CType(e.Row.FindControl(GRID_CTRL_NAME_TAB), DropDownList)
                        If Not ddl Is Nothing Then
                            Try
                                'Me.BindListControlToDataView(ddl, TABLIST, "Tab_DESC", "Tab_id", True)

                                Dim oListContext As New Assurant.Elita.CommonConfiguration.ListContext
                                oListContext.LanguageId = Thread.CurrentPrincipal.GetLanguageId()

                                Dim TabList As DataElements.ListItem() =
                                    CommonConfigManager.Current.ListManager.GetList(listCode:="GetTabList",
                                                                languageCode:=Thread.CurrentPrincipal.GetLanguageCode(),
                                                                context:=oListContext)

                                ddl.Populate(TabList.ToArray(),
                                                New PopulateOptions() With
                                                {
                                                    .AddBlankItem = True
                                                })


                                Me.SetSelectedItem(ddl, Me.State.MyBO.TabId)
                            Catch ex As Exception
                            End Try
                            If (Not State.IsGridAddNew) AndAlso (intFormCnt > 0) Then ddl.Enabled = False
                        End If
                        ddl = Nothing

                        txt = CType(e.Row.FindControl(GRID_CTRL_NAME_CODE), TextBox)
                        If Not txt Is Nothing Then
                            If State.IsGridAddNew Then
                                txt.Text = State.MyBO.Code
                            Else
                                If intFormCnt > 0 Then txt.Enabled = False
                            End If
                            txt = Nothing
                        End If

                        txt = CType(e.Row.FindControl(GRID_CTRL_NAME_DESC), TextBox)
                        If Not txt Is Nothing Then
                            If State.IsGridAddNew Then
                                txt.Text = State.MyBO.Description
                            End If
                        End If
                        txt = Nothing
                    Else
                        If intFormCnt > 0 Then
                            Dim objBtn As ImageButton = CType(.Cells(DELETE_COL).FindControl(DELETE_CONTROL_NAME), ImageButton)
                            If Not objBtn Is Nothing Then
                                objBtn.Enabled = False
                                objBtn.Visible = False
                            End If
                        End If
                    End If
                End With
            End If
            BaseItemBound(sender, e)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub

    Private Sub Grid_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles Grid.Sorting
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
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub

    Protected Sub cboPageSize_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
        Try
            State.PageSize = CType(cboPageSize.SelectedValue, Integer)
            Me.State.PageIndex = NewCurrentPageIndex(Grid, State.searchDV.Count, State.PageSize)
            Me.Grid.PageIndex = Me.State.PageIndex
            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub
#End Region

#Region "Helper functions"
    Private Sub populateSearchControls()
        Try
            'Dim dv As DataView = TABLIST
            'dv.Sort = "Tab_DESC"
            'BindListControlToDataView(Me.ddlTab, dv, "Tab_DESC", "Tab_id", True)

            Dim oListContext As New Assurant.Elita.CommonConfiguration.ListContext
            oListContext.LanguageId = Thread.CurrentPrincipal.GetLanguageId()

            Dim TabList As DataElements.ListItem() =
                                   CommonConfigManager.Current.ListManager.GetList(listCode:="GetTabList",
                                                               languageCode:=Thread.CurrentPrincipal.GetLanguageCode(),
                                                               context:=oListContext)

            ddlTab.Populate(TabList.ToArray(),
                            New PopulateOptions() With
                            {
                                .AddBlankItem = True
                            })

        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub

    Public Sub PopulateGrid()
        Try
            With State
                If (.searchDV Is Nothing) Then
                    .searchDV = FormCategory.getList(.searchTab, .searchCode, .searchDesc)
                End If
                If .IsGridAddNew Then
                    .PageIndex = Grid.PageIndex
                Else
                    .searchDV.Sort = .SortExpression
                End If
            End With

            Grid.PageSize = State.PageSize
            If State.searchDV.Count = 0 Then
                Dim dv As FormCategory.FormCategorySearchDV = State.searchDV.AddNewRowToEmptyDV()
                SetPageAndSelectedIndexFromGuid(dv, Me.State.FormCategoryID, Me.Grid, Me.State.PageIndex, (Me.IsGridInEditMode OrElse State.IsGridAddNew))
                Me.Grid.DataSource = dv
            Else
                SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.FormCategoryID, Me.Grid, Me.State.PageIndex, (Me.IsGridInEditMode OrElse State.IsGridAddNew))
                Me.Grid.DataSource = Me.State.searchDV
            End If

            Me.State.PageIndex = Me.Grid.PageIndex
            Me.Grid.DataBind()

            HighLightGridViewSortColumn(Grid, State.SortExpression)
            ControlMgr.SetVisibleControl(Me, Grid, True)
            ControlMgr.SetVisibleControl(Me, trPageSize, Me.Grid.Visible)

            If Me.Grid.Visible Then
                Me.lblRecordCount.Text = Me.State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
            End If
            If Not Grid.BottomPagerRow.Visible Then Grid.BottomPagerRow.Visible = True

            If State.searchDV.Count = 0 Then
                For Each gvRow As GridViewRow In Grid.Rows
                    gvRow.Visible = False
                    gvRow.Controls.Clear()
                Next
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub

    Private Sub SetControlState()
        If (Me.IsGridInEditMode) Then
            ControlMgr.SetVisibleControl(Me, btnNew, False)
            ControlMgr.SetVisibleControl(Me, btnAssignForm, False)
            ControlMgr.SetVisibleControl(Me, btnCancel, True)
            ControlMgr.SetVisibleControl(Me, btnSave, True)
            ControlMgr.SetEnableControl(Me, btnSearch, False)
            ControlMgr.SetEnableControl(Me, btnClearSearch, False)
            Me.MenuEnabled = False
            If (Me.cboPageSize.Enabled) Then
                ControlMgr.SetEnableControl(Me, cboPageSize, False)
            End If
        Else
            ControlMgr.SetVisibleControl(Me, btnNew, True)
            ControlMgr.SetVisibleControl(Me, btnAssignForm, True)
            ControlMgr.SetVisibleControl(Me, btnCancel, False)
            ControlMgr.SetVisibleControl(Me, btnSave, False)
            ControlMgr.SetEnableControl(Me, btnSearch, True)
            ControlMgr.SetEnableControl(Me, btnClearSearch, True)
            Me.MenuEnabled = True
            If Not (Me.cboPageSize.Enabled) Then
                ControlMgr.SetEnableControl(Me, Me.cboPageSize, True)
            End If
        End If
        cboPageSize.Visible = Grid.Visible
        lblRecordCount.Visible = Grid.Visible
    End Sub

    Private Function PopulateBOFromForm(ByRef errMsg As Collections.Generic.List(Of String)) As Boolean
        Dim blnSuccess As Boolean = True
        Dim ind As Integer = Grid.EditIndex
        With Me.State.MyBO
            Dim ddl As DropDownList = CType(Grid.Rows(ind).Cells(GRID_COL_TAB_IDX).FindControl(GRID_CTRL_NAME_TAB), DropDownList)
            Me.PopulateBOProperty(Me.State.MyBO, "TabId", ddl)
            If .TabId = Guid.Empty Then
                blnSuccess = False
                errMsg.Add(TranslationBase.TranslateLabelOrMessage("TAB") & ":" & TranslationBase.TranslateLabelOrMessage(ElitaPlus.Common.ErrorCodes.GUI_VALUE_MANDATORY_ERR))
            End If
            ddl = Nothing

            .Code = CType(Grid.Rows(ind).Cells(GRID_COL_CODE_IDX).FindControl(GRID_CTRL_NAME_CODE), TextBox).Text.Trim.ToUpper
            If .Code = String.Empty Then
                blnSuccess = False
                errMsg.Add(TranslationBase.TranslateLabelOrMessage("CODE") & ":" & TranslationBase.TranslateLabelOrMessage(ElitaPlus.Common.ErrorCodes.GUI_VALUE_MANDATORY_ERR))
            End If

            State.editDescription = CType(Grid.Rows(ind).Cells(GRID_COL_DESC_IDX).FindControl(GRID_CTRL_NAME_DESC), TextBox).Text.Trim
            .Description = State.editDescription
            If State.editDescription = String.Empty Then
                blnSuccess = False
                errMsg.Add(TranslationBase.TranslateLabelOrMessage("DESCRIPTION") & ":" & TranslationBase.TranslateLabelOrMessage(ElitaPlus.Common.ErrorCodes.GUI_VALUE_MANDATORY_ERR))
            End If
        End With
        Return blnSuccess
    End Function

    Private Function GetRowIndexFromSearchDVByID(ByVal MSGCodeID As Guid) As Integer
        Dim rowind As Integer = NO_ITEM_SELECTED_INDEX
        With State
            If Not .searchDV Is Nothing Then
                rowind = FindSelectedRowIndexFromGuid(.searchDV, MSGCodeID)
            End If
        End With
        Return rowind
    End Function
    Private Sub RemoveNewRowFromSearchDV()
        Dim rowind As Integer = NO_ITEM_SELECTED_INDEX
        With State
            If Not .searchDV Is Nothing Then
                rowind = FindSelectedRowIndexFromGuid(.searchDV, State.FormCategoryID)
            End If
        End With
        If rowind <> NO_ITEM_SELECTED_INDEX Then State.searchDV.Delete(rowind)
    End Sub
#End Region

#Region "Button click handlers"
    Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnNew.Click
        Try
            State.IsGridVisible = True
            Me.State.MyBO = New FormCategory
            Me.State.FormCategoryID = Me.State.MyBO.Id
            If State.searchDV Is Nothing Then State.searchDV = FormCategory.getList(State.FormCategoryID, "", "")
            State.MyBO.AddNewRowToSearchDV(Me.State.searchDV, Me.State.MyBO)
            State.IsGridAddNew = True
            PopulateGrid()
            'Disable all Edit and Delete icon buttons on the Grid
            SetGridControls(Me.Grid, False)
            'Set focus on the code TextBox for the EditItemIndex row
            Dim objCtrl As WebControl = CType(Grid.Rows(Grid.EditIndex).Cells(GRID_COL_CODE_IDX).FindControl(GRID_CTRL_NAME_CODE), WebControl)
            If Not objCtrl Is Nothing Then SetFocus(objCtrl)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSave.Click
        Try
            Dim ErrMsg As New Collections.Generic.List(Of String)
            If PopulateBOFromForm(ErrMsg) Then
                With State
                    If (.MyBO.IsDirty OrElse .MyBO.IsDescriptionChanged) Then
                        If .MyBO.IsNew AndAlso .MyBO.DictItemId = Guid.Empty Then
                            .MyBO.DictItemId = New Guid
                        End If
                        .MyBO.Save()
                        .editDescription = ""
                        If .IsGridAddNew And .searchDV.Count = 1 Then
                            .searchTab = .MyBO.TabId
                            .searchCode = .MyBO.Code
                            .searchDesc = .MyBO.Description
                        End If
                        Me.AddInfoMsg(Message.SAVE_RECORD_CONFIRMATION)
                        State.searchDV = Nothing
                    Else
                        Me.AddInfoMsg(Message.MSG_RECORD_NOT_SAVED)
                    End If
                    Grid.EditIndex = Me.NO_ITEM_SELECTED_INDEX
                    .IsGridAddNew = False
                End With
            Else
                Me.ErrControllerMaster.AddErrorAndShow(ErrMsg.ToArray, False)
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
        Me.PopulateGrid()
    End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancel.Click
        Try
            With State
                If .IsGridAddNew Then
                    RemoveNewRowFromSearchDV()
                    .editDescription = ""
                    .IsGridAddNew = False
                    Grid.PageIndex = .PageIndex
                End If
                .FormCategoryID = Guid.Empty
            End With
            Grid.EditIndex = NO_ITEM_SELECTED_INDEX
            PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub

    Protected Sub btnAssignForm_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAssignForm.Click
        Try
            With State
                .searchCode = Me.txtCode.Text.Trim
                .searchDesc = txtDesc.Text.Trim.Trim
                .searchTab = New Guid(ddlTab.SelectedValue)
            End With
            Me.callPage(FormCategoryAssignForm.URL)
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub

    Protected Sub btnClearSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnClearSearch.Click
        Try
            Me.txtCode.Text = String.Empty
            Me.txtDesc.Text = String.Empty
            Me.ddlTab.SelectedIndex = Me.NO_ITEM_SELECTED_INDEX

            Grid.EditIndex = Me.NO_ITEM_SELECTED_INDEX
            With State
                .FormCategoryID = Guid.Empty
            End With
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
        Try
            With State
                .PageIndex = 0
                .FormCategoryID = Guid.Empty
                .IsGridVisible = True
                .searchDV = Nothing
                .HasDataChanged = False
                .searchCode = Me.txtCode.Text.Trim
                .searchDesc = txtDesc.Text.Trim.Trim
                .searchTab = New Guid(ddlTab.SelectedValue)
            End With
            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub
#End Region



End Class