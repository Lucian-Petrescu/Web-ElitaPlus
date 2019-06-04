Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Imports Microsoft.VisualBasic
Imports System.Globalization
Imports System.Threading
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.Web.Forms
Imports Assurant.Common.Ftp
Imports Assurant.ElitaPlus.Common.MiscUtil
Imports Assurant.ElitaPlus.ElitaPlusWebApp.DownLoad

Partial Public Class FormCategoryAssignForm
    Inherits ElitaPlusSearchPage

#Region "Constants"
    Public Const URL As String = "FormCategoryAssignForm.aspx"
    Public Const PAGETITLE As String = "ASSIGN_FORM_CATEGORY"
    Public Const PAGETAB As String = "ADMIN"

    Private Const GRID_NO_SELECTEDITEM_INX As Integer = -1

    Private Const GRID_COL_EDIT_IDX As Integer = 0
    Private Const GRID_COL_FORM_ID_IDX As Integer = 1
    Private Const GRID_COL_FORM_NAME_IDX As Integer = 2
    Private Const GRID_COL_TAB_IDX As Integer = 3
    Private Const GRID_COL_FORM_CATEGORY_ID_IDX As Integer = 4

    Private Const GRID_CTRL_NAME_FORM_ID As String = "lblFormID"
    Private Const GRID_CTRL_NAME_FORM_CATEGORY As String = "lblFormCategory"
    Private Const GRID_CTRL_NAME_FORM_CATEGORY_ID As String = "ddlFormCategory"

    Private _TabList As DataView
    Private _FormCategoryList As DataView
#End Region
#Region "Page properties"
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

    Public ReadOnly Property TabList() As DataView
        Get
            If _TabList Is Nothing Then
                _TabList = FormCategory.getTabList()
            End If
            Return _TabList
        End Get
    End Property

    Public ReadOnly Property FormCategoryList() As DataView
        Get
            If _FormCategoryList Is Nothing Then
                _FormCategoryList = FormCategory.getList(Nothing, "", "")
            End If
            Return _FormCategoryList
        End Get
    End Property
#End Region

#Region "Page State"
    ' This class keeps the current state for the search page.
    Class MyState
        Public FormID As Guid
        Public FormCategoryID As Guid

        Public IsGridVisible As Boolean
        Public IsValueChanged As Boolean
        Public PageIndex As Integer = 0
        Public PageSize As Integer = DEFAULT_PAGE_SIZE
        Public GridEditIndex As Integer = NO_ITEM_SELECTED_INDEX
        Public searchDV As FormCategory.FormSearchDV = Nothing
        Public SortExpression As String = FormCategory.FormSearchDV.COL_FORM_NAME

        Public searchTab As Guid = Guid.Empty
        Public searchCategory As Guid = Guid.Empty
        Public searchFormDesc As String = ""

    End Class

    Public Sub New()
        MyBase.New(New MyState)
    End Sub
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
            If e.CommandName = "SelectAction" Then
                Dim row As GridViewRow = CType(CType(e.CommandSource, Control).Parent.Parent, GridViewRow)
                Dim RowInd As Integer = row.RowIndex

                Dim lblCtrl As Label
                lblCtrl = CType(Grid.Rows(RowInd).Cells(GRID_COL_FORM_ID_IDX).FindControl(GRID_CTRL_NAME_FORM_ID), Label)
                State.FormID = New Guid(lblCtrl.Text)
                'State.FormCategoryID = 
                lblCtrl = Nothing

                Grid.EditIndex = RowInd
                PopulateGrid()
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
                        State.FormID = New Guid(CType(dvRow("form_id"), Byte()))

                        If Not dvRow("form_category_id") Is DBNull.Value Then
                            State.FormCategoryID = New Guid(CType(dvRow("form_category_id"), Byte()))
                        Else
                            State.FormCategoryID = Guid.Empty
                        End If

                        ddl = CType(e.Row.FindControl(GRID_CTRL_NAME_FORM_CATEGORY_ID), DropDownList)
                        If Not ddl Is Nothing Then
                            Try
                                Dim oFormCategory As Assurant.Elita.CommonConfiguration.DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="FormCategory")
                                Dim filteredFormCategory As Assurant.Elita.CommonConfiguration.DataElements.ListItem() = (From fc In oFormCategory
                                                                                                                          Where fc.ExtendedCode = dvRow("Tab_Code").ToString
                                                                                                                          Select fc)
                                ddl.Populate(filteredFormCategory.ToArray(), New PopulateOptions() With
                                    {
                                        .AddBlankItem = True
                                    })

                                'Dim dv As DataView = FormCategory.getFormCategoryList()
                                'dv.RowFilter = "Tab_Code='" & dvRow("Tab_Code").ToString & "'"
                                'dv.Sort = "Description"
                                'Me.BindListControlToDataView(ddl, dv, "Description", "Form_Category_id", True)

                                Me.SetSelectedItem(ddl, State.FormCategoryID)
                            Catch ex As Exception
                            End Try
                        End If
                        ddl = Nothing
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
            Dim oListContext As New ListContext
            oListContext.LanguageId = Thread.CurrentPrincipal.GetLanguageId()
            Dim oTabList As Assurant.Elita.CommonConfiguration.DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="GetTabList", context:=oListContext)
            Me.ddlTab.Populate(oTabList.ToArray(), New PopulateOptions() With
                                    {
                                        .AddBlankItem = True
                                    })

            'Dim dv As DataView = TabList
            'dv.Sort = "Tab_DESC"
            'BindListControlToDataView(Me.ddlTab, dv, "Tab_DESC", "Tab_id", True)

            Dim oFormCategory As Assurant.Elita.CommonConfiguration.DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="FormCategory", context:=oListContext)
            Me.ddlFormCategory.Populate(oFormCategory.ToArray(), New PopulateOptions() With
                                    {
                                        .AddBlankItem = True
                                    })

            'dv = Nothing
            'dv = FormCategoryList
            'dv.Sort = FormCategory.FormCategorySearchDV.COL_DESCRIPTION
            'BindListControlToDataView(Me.ddlFormCategory, dv, FormCategory.FormCategorySearchDV.COL_DESCRIPTION, FormCategory.FormCategorySearchDV.COL_FORM_CATEGORY_ID, True)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub

    Public Sub PopulateGrid()
        Try
            With State
                If (.searchDV Is Nothing) Then
                    .searchDV = FormCategory.getFormList(.searchTab, .searchCategory, .searchFormDesc)
                End If
                .searchDV.Sort = .SortExpression
            End With

            Grid.PageSize = State.PageSize
            If State.searchDV.Count = 0 Then
                Dim dv As FormCategory.FormSearchDV = State.searchDV.AddNewRowToEmptyDV()
                SetPageAndSelectedIndexFromGuid(dv, Me.State.FormID, Me.Grid, Me.State.PageIndex, Me.IsGridInEditMode)
                Me.Grid.DataSource = dv
            Else
                SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.FormID, Me.Grid, Me.State.PageIndex, Me.IsGridInEditMode)
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
            ControlMgr.SetVisibleControl(Me, btnCancel, True)
            ControlMgr.SetVisibleControl(Me, btnSave, True)
            ControlMgr.SetVisibleControl(Me, btnBack, False)
            ControlMgr.SetEnableControl(Me, btnSearch, False)
            ControlMgr.SetEnableControl(Me, btnClearSearch, False)
            Me.MenuEnabled = False
            If (Me.cboPageSize.Enabled) Then
                ControlMgr.SetEnableControl(Me, cboPageSize, False)
            End If
        Else
            ControlMgr.SetVisibleControl(Me, btnCancel, False)
            ControlMgr.SetVisibleControl(Me, btnSave, False)
            ControlMgr.SetVisibleControl(Me, btnBack, True)
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
        Dim blnSuccess As Boolean = True, guidTemp As Guid
        State.IsValueChanged = False
        Dim ind As Integer = Grid.EditIndex, strTemp As String
        With Me.State
            'Dim lblCtrl As Label = CType(Grid.Rows(ind).Cells(GRID_COL_FORM_ID_IDX).FindControl(GRID_CTRL_NAME_FORM_ID), Label)
            'guidTemp = New Guid(lblCtrl.Text)
            'If guidTemp <> .FormID Then
            '    .FormID = guidTemp
            '    State.IsValueChanged = True
            'End If

            Dim ddl As DropDownList = CType(Grid.Rows(ind).Cells(GRID_COL_FORM_CATEGORY_ID_IDX).FindControl(GRID_CTRL_NAME_FORM_CATEGORY_ID), DropDownList)
            guidTemp = GetSelectedItem(ddl)
            ddl = Nothing
            If guidTemp <> .FormCategoryID Then
                .FormCategoryID = guidTemp
                State.IsValueChanged = True
            End If
        End With
        Return blnSuccess
    End Function

#End Region

#Region "Button click handlers"
    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSave.Click
        Try
            Dim ErrMsg As New Collections.Generic.List(Of String)
            If PopulateBOFromForm(ErrMsg) Then
                With State
                    If (.IsValueChanged) Then
                        FormCategory.AssignFormCategory(State.FormID, State.FormCategoryID)
                        Me.AddInfoMsg(Message.SAVE_RECORD_CONFIRMATION)
                        State.FormID = Guid.Empty
                        State.FormCategoryID = Guid.Empty
                        State.searchDV = Nothing
                    Else
                        Me.AddInfoMsg(Message.MSG_RECORD_NOT_SAVED)
                    End If
                    Grid.EditIndex = Me.NO_ITEM_SELECTED_INDEX
                    'Disable all Edit and Delete icon buttons on the Grid
                    SetGridControls(Me.Grid, True)
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
                .FormCategoryID = Guid.Empty
                .FormID = Guid.Empty
            End With
            Grid.EditIndex = NO_ITEM_SELECTED_INDEX
            PopulateGrid()
            'enable all Edit and Delete icon buttons on the Grid
            If State.searchDV.Count > 0 Then SetGridControls(Me.Grid, True)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub

    Protected Sub btnClearSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnClearSearch.Click
        Try
            Me.txtFormDesc.Text = String.Empty
            Me.ddlFormCategory.SelectedIndex = NO_ITEM_SELECTED_INDEX
            Me.ddlTab.SelectedIndex = NO_ITEM_SELECTED_INDEX

            Grid.EditIndex = Me.NO_ITEM_SELECTED_INDEX
            With State
                .FormID = Guid.Empty
            End With
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
        Try
            With State
                .PageIndex = 0
                .FormID = Guid.Empty
                .IsGridVisible = True
                .searchDV = Nothing
                .searchCategory = New Guid(ddlFormCategory.SelectedValue)
                .searchFormDesc = txtFormDesc.Text.Trim.Trim
                .searchTab = New Guid(ddlTab.SelectedValue)
            End With
            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub

    Protected Sub btnBack_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnBack.Click
        Try
            ReturnToCallingPage()
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub
#End Region

End Class