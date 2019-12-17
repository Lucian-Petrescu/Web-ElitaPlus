Imports Microsoft.VisualBasic
Imports System.Globalization
Imports System.Threading
Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.Web.Forms
Partial Public Class ComunaCodeForm
    Inherits ElitaPlusSearchPage

#Region "Constants"
    Public Const PAGETITLE As String = "COMUNA"
    Public Const PAGETAB As String = "TABLES"

    Private Const GRID_NO_SELECTEDITEM_INX As Integer = -1

    Private Const GRID_COL_EDIT_IDX As Integer = 0
    Private Const GRID_COL_DELETE_IDX As Integer = 1
    Private Const GRID_COL_ID_IDX As Integer = 2
    Private Const GRID_COL_COMUNA_IDX As Integer = 3
    Private Const GRID_COL_POSTALCODE_IDX As Integer = 4
    Private Const GRID_COL_REGION_IDX As Integer = 5

    Private Const GRID_CTRL_NAME_COMUNA As String = "moComunaTextBox"
    Private Const GRID_CTRL_NAME_COMUNA_LABEL As String = "moComunaLabel"
    Private Const GRID_CTRL_NAME_POSTALCODE As String = "moPostalCodeTextBox"
    Private Const GRID_CTRL_NAME_POSTALCODE_LABEL As String = "moPostalCodeLabel"
    Private Const GRID_CTRL_NAME_COMUNA_CODE_ID As String = "IdLabel"
    Private Const GRID_CTRL_NAME_REGION As String = "moRegionDropdown"
    Private Const GRID_CTRL_NAME_REGION_LABEL As String = "moRegionLabel"
    'Private _RegionList As DataView
    Private _RegionList As DataElements.ListItem()
    Private IsReturnFromChild As Boolean = False
#End Region

#Region "Page State"
    ' This class keeps the current state for the search page.
    Class MyState
        Public MyBO As ComunaCode
        Public ComunaCodeID As Guid
        Public PageIndex As Integer = 0
        Public PageSize As Integer = DEFAULT_PAGE_SIZE
        Public HasDataChanged As Boolean
        Public IsGridAddNew As Boolean = False
        Public IsGridVisible As Boolean
        Public GridEditIndex As Integer = NO_ITEM_SELECTED_INDEX
        Public searchDV As ComunaCode.ComunaCodeSearchDV = Nothing
        Public SortExpression As String = ComunaCode.ComunaCodeSearchDV.COL_COMUNA
        Public editDescription As String = ""

        Public searchRegion As Guid = Guid.Empty
        Public searchComuna As String = ""
        Public searchPostalCode As String = ""
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

    Public ReadOnly Property IsGridInEditMode() As Boolean
        Get
            Return Me.Grid.EditIndex > Me.NO_ITEM_SELECTED_INDEX
        End Get
    End Property

    'Public ReadOnly Property REGIONLIST() As DataView
    '    Get
    '        If _RegionList Is Nothing Then
    '            _RegionList = LookupListNew.GetRegionLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies)
    '        End If
    '        Return _RegionList
    '    End Get
    'End Property

    Public ReadOnly Property REGIONLIST() As DataElements.ListItem()
        Get
            If _RegionList Is Nothing Then
                Dim countryList As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="Country")
                Dim filteredCountryList As DataElements.ListItem() = (From x In countryList
                                                                      Where ElitaPlusIdentity.Current.ActiveUser.Countries.Contains(x.ListItemId)
                                                                      Select x).ToArray()

                Dim oListContext As New Assurant.Elita.CommonConfiguration.ListContext
                For Each _country As DataElements.ListItem In filteredCountryList
                    oListContext.CountryId = _country.ListItemId
                    Dim oRegionListForCountry As Assurant.Elita.CommonConfiguration.DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:=ListCodes.RegionsByCountry, context:=oListContext)
                    If oRegionListForCountry.Count > 0 Then
                        If Not _RegionList Is Nothing Then
                            _RegionList.ToList().AddRange(oRegionListForCountry)
                        Else
                            _RegionList = oRegionListForCountry.Clone()
                        End If
                    End If
                Next
            End If

            Return _RegionList
        End Get
    End Property

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
                        moComunaTextBox.Text = .searchComuna
                        moPostalCodeTextbox.Text = .searchPostalCode
                        SetSelectedItem(moRegionDrop, .searchRegion)
                        .searchDV = Nothing
                        PopulateGrid()
                    End With
                End If
            Else
                CheckIfComingFromDeleteConfirm()
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
        Me.ShowMissingTranslations(Me.ErrControllerMaster)
    End Sub
    Protected Sub CheckIfComingFromDeleteConfirm()
        Dim confResponse As String = Me.HiddenDeletePromptResponse.Value
        If Not confResponse Is Nothing AndAlso confResponse = Me.MSG_VALUE_YES Then
            If Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Delete Then
                DoDelete()
            End If
            Select Case Me.State.ActionInProgress
                Case ElitaPlusPage.DetailPageCommand.Delete
            End Select
        ElseIf Not confResponse Is Nothing AndAlso confResponse = Me.MSG_VALUE_NO Then
            Select Case Me.State.ActionInProgress
                Case ElitaPlusPage.DetailPageCommand.Delete
            End Select
        End If
        'Clean after consuming the action
        Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
        Me.HiddenDeletePromptResponse.Value = ""
    End Sub
    Private Sub DoDelete()
        Me.State.MyBO = New ComunaCode(Me.State.ComunaCodeID)
        Try
            Me.State.MyBO.Delete()
            Me.State.MyBO.Save()
            'Me.AddInfoMsg(Me.MSG_RECORD_DELETED_OK)
        Catch ex As Exception
            Me.State.MyBO.RejectChanges()
            Throw ex
        End Try

        Me.State.PageIndex = Grid.PageIndex

        'Set the IsAfterSave flag to TRUE so that the Paging logic gets invoked
        Me.State.searchDV = Nothing
        PopulateGrid()
        Me.State.PageIndex = Grid.PageIndex
    End Sub
    Private Sub ComunaCodeForm_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete
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

    Private Sub ComunaCodeForm_PageReturn(ByVal ReturnFromUrl As String, ByVal ReturnPar As Object) Handles Me.PageReturn
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
            Me.State.ComunaCodeID = Guid.Empty
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
                lblCtrl = CType(Grid.Rows(RowInd).Cells(GRID_COL_ID_IDX).FindControl(GRID_CTRL_NAME_COMUNA_CODE_ID), Label)
                Me.State.ComunaCodeID = New Guid(lblCtrl.Text)
                Me.State.MyBO = New ComunaCode(State.ComunaCodeID)
                lblCtrl = Nothing
                lblCtrl = CType(Grid.Rows(RowInd).Cells(GRID_COL_COMUNA_IDX).FindControl(GRID_CTRL_NAME_COMUNA_LABEL), Label)
                If Not lblCtrl Is Nothing Then State.MyBO.Comuna = lblCtrl.Text
                lblCtrl = Nothing
                If e.CommandName = "SelectAction" Then
                    Grid.EditIndex = RowInd
                    PopulateGrid()
                    'Disable all Edit and Delete icon buttons on the Grid
                    SetGridControls(Me.Grid, False)
                ElseIf e.CommandName = "DeleteAction" Then
                    'Clear the SelectedItemStyle to remove the highlight from the previously saved row
                    Grid.SelectedIndex = Me.GRID_NO_SELECTEDITEM_INX

                    Me.DisplayMessage(Message.DELETE_RECORD_PROMPT, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Me.HiddenDeletePromptResponse)
                    Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Delete
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
                    If .RowIndex = Grid.EditIndex Then
                        ddl = CType(e.Row.FindControl(GRID_CTRL_NAME_REGION), DropDownList)
                        If Not ddl Is Nothing Then
                            Try
                                ddl.Populate(REGIONLIST.ToArray(), New PopulateOptions() With
                                    {
                                        .AddBlankItem = True
                                    })
                                'Me.BindListControlToDataView(ddl, REGIONLIST, "Description", "id", True)
                                Me.SetSelectedItem(ddl, Me.State.MyBO.RegionId)
                            Catch ex As Exception
                            End Try
                        End If
                        ddl = Nothing

                        txt = CType(e.Row.FindControl(GRID_CTRL_NAME_COMUNA), TextBox)
                        If Not txt Is Nothing Then
                            If State.IsGridAddNew Then
                                txt.Text = State.MyBO.Comuna
                            End If
                            txt = Nothing
                        End If

                        txt = CType(e.Row.FindControl(GRID_CTRL_NAME_POSTALCODE), TextBox)
                        If Not txt Is Nothing Then
                            If State.IsGridAddNew Then
                                txt.Text = State.MyBO.Postalcode
                            End If
                        End If
                        txt = Nothing
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
            Me.moRegionDrop.Populate(REGIONLIST.ToArray(), New PopulateOptions() With
                {
                    .AddBlankItem = True,
                    .SortFunc = AddressOf PopulateOptions.GetExtendedCode
                })

            'Dim dv As DataView = REGIONLIST
            'dv.Sort = "Description"
            'BindListControlToDataView(Me.moRegionDrop, dv, "Description", "Id", True)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub

    Public Sub PopulateGrid()
        Try
            With State
                If (.searchDV Is Nothing) Then
                    .searchDV = ComunaCode.LoadList(.searchComuna, .searchPostalCode, .searchRegion)
                End If
                If .IsGridAddNew Then
                    .PageIndex = Grid.PageIndex
                Else
                    .searchDV.Sort = .SortExpression
                End If
            End With

            Grid.PageSize = State.PageSize
            If State.searchDV.Count = 0 Then
                Dim dv As ComunaCode.ComunaCodeSearchDV = State.searchDV.AddNewRowToEmptyDV()
                SetPageAndSelectedIndexFromGuid(dv, Me.State.ComunaCodeID, Me.Grid, Me.State.PageIndex, (Me.IsGridInEditMode OrElse State.IsGridAddNew))
                Me.Grid.DataSource = dv
            Else
                SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.ComunaCodeID, Me.Grid, Me.State.PageIndex, (Me.IsGridInEditMode OrElse State.IsGridAddNew))
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
                Me.DisplayMessage(Message.MSG_NO_RECORDS_FOUND, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub

    Private Sub SetControlState()
        If (Me.IsGridInEditMode) Then
            ControlMgr.SetVisibleControl(Me, btnNew, False)
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
            Dim ddl As DropDownList = CType(Grid.Rows(ind).Cells(GRID_COL_REGION_IDX).FindControl(GRID_CTRL_NAME_REGION), DropDownList)
            Me.PopulateBOProperty(Me.State.MyBO, "RegionId", ddl)
            If .RegionId = Guid.Empty Then
                blnSuccess = False
                errMsg.Add(TranslationBase.TranslateLabelOrMessage("REGION") & ":" & TranslationBase.TranslateLabelOrMessage(ElitaPlus.Common.ErrorCodes.GUI_VALUE_MANDATORY_ERR))
            End If
            ddl = Nothing

            .Comuna = CType(Grid.Rows(ind).Cells(GRID_COL_COMUNA_IDX).FindControl(GRID_CTRL_NAME_COMUNA), TextBox).Text.Trim.ToUpper
            If .Comuna = String.Empty Then
                blnSuccess = False
                errMsg.Add(TranslationBase.TranslateLabelOrMessage("Comuna") & ":" & TranslationBase.TranslateLabelOrMessage(ElitaPlus.Common.ErrorCodes.GUI_VALUE_MANDATORY_ERR))
            End If

            .Postalcode = CType(Grid.Rows(ind).Cells(GRID_COL_POSTALCODE_IDX).FindControl(GRID_CTRL_NAME_POSTALCODE), TextBox).Text.Trim
            If .Postalcode = String.Empty Then
                blnSuccess = False
                errMsg.Add(TranslationBase.TranslateLabelOrMessage("Postal_Code") & ":" & TranslationBase.TranslateLabelOrMessage(ElitaPlus.Common.ErrorCodes.GUI_VALUE_MANDATORY_ERR))
            End If
        End With
        Return blnSuccess
    End Function

    Private Sub RemoveNewRowFromSearchDV()
        Dim rowind As Integer = NO_ITEM_SELECTED_INDEX
        With State
            If Not .searchDV Is Nothing Then
                rowind = FindSelectedRowIndexFromGuid(.searchDV, State.ComunaCodeID)
            End If
        End With
        If rowind <> NO_ITEM_SELECTED_INDEX Then State.searchDV.Delete(rowind)
    End Sub
#End Region

#Region "Button click handlers"
    Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnNew.Click
        Try
            State.IsGridVisible = True
            Me.State.MyBO = New ComunaCode
            Me.State.ComunaCodeID = Me.State.MyBO.Id
            If State.searchDV Is Nothing Then State.searchDV = ComunaCode.LoadList("", "", State.ComunaCodeID)
            State.MyBO.AddNewRowToSearchDV(Me.State.searchDV, Me.State.MyBO)
            State.IsGridAddNew = True
            PopulateGrid()
            'Disable all Edit and Delete icon buttons on the Grid
            SetGridControls(Me.Grid, False)
            'Set focus on the code TextBox for the EditItemIndex row
            Dim objCtrl As WebControl = CType(Grid.Rows(Grid.EditIndex).Cells(GRID_COL_COMUNA_IDX).FindControl(GRID_CTRL_NAME_COMUNA), WebControl)
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
                    If (.MyBO.IsDirty) Then
                        .MyBO.Save()
                        If .IsGridAddNew And .searchDV.Count = 1 Then
                            .searchRegion = .MyBO.RegionId
                            .searchComuna = .MyBO.Comuna
                            .searchPostalCode = .MyBO.Postalcode
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
                .ComunaCodeID = Guid.Empty
            End With
            Grid.EditIndex = NO_ITEM_SELECTED_INDEX
            PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub

    Protected Sub btnClearSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnClearSearch.Click
        Try
            Me.moComunaTextBox.Text = String.Empty
            Me.moPostalCodeTextbox.Text = String.Empty
            Me.moRegionDrop.SelectedIndex = Me.NO_ITEM_SELECTED_INDEX

            Grid.EditIndex = Me.NO_ITEM_SELECTED_INDEX
            With State
                .ComunaCodeID = Guid.Empty
            End With
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
        Try
            With State
                .PageIndex = 0
                .ComunaCodeID = Guid.Empty
                .IsGridVisible = True
                .searchDV = Nothing
                .HasDataChanged = False
                .searchComuna = Me.moComunaTextBox.Text.Trim
                .searchPostalCode = moPostalCodeTextbox.Text.Trim
                .searchRegion = New Guid(moRegionDrop.SelectedValue)
            End With
            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub
#End Region



End Class