Imports System.Globalization
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports System.Threading
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms
Partial Public Class SuspendedReasonsForm
    Inherits ElitaPlusSearchPage

#Region "Constants"
    Public Const URL As String = "Tables/SuspendedReasonsForm.aspx"
    Public Const PAGETITLE As String = "SUSPENDED_REASONS"
    Public Const PAGETAB As String = "ADMIN"
    Public Const SUMMARYTITLE As String = "SEARCH_RESULTS_FOR_SUSPENDED_REASONS"

    Private Const GRID_COL_ID_IDX As Integer = 0
    Private Const GRID_COL_DEALER_IDX As Integer = 1
    Private Const GRID_COL_CODE_IDX As Integer = 2
    Private Const GRID_COL_DESCRIPTION_IDX As Integer = 3
    Private Const GRID_COL_CLAIM_ALLOWDE_IDX As Integer = 4
    Private Const GRID_COL_EDIT_IDX As Integer = 5
    'Private Const GRID_COL_DELETE_IDX As Integer = 5

    Private Const MSG_CONFIRM_PROMPT As String = "MSG_CONFIRM_PROMPT"
    Private Const MSG_RECORD_DELETED_OK As String = "MSG_RECORD_DELETED_OK"
    Private Const MSG_RECORD_SAVED_OK As String = "MSG_RECORD_SAVED_OK"
    Private Const MSG_RECORD_NOT_SAVED As String = "MSG_RECORD_NOT_SAVED"

    Private Const GRID_CTRL_NAME_ID As String = "moIDLabel"
    Private Const GRID_CTRL_NAME_DEALER_LABLE As String = "moDealerLabel"
    Private Const GRID_CTRL_NAME_CODE_LABLE As String = "moCodeLabel"
    Private Const GRID_CTRL_NAME_DESCRIPTION_LABEL As String = "moDescriptionLabel"
    Private Const GRID_CTRL_NAME_CLAIM_ALLOWED_LABLE As String = "moClaimAllowedLabel"

    Private Const GRID_CTRL_NAME_CODE_TXT As String = "moCodeText"
    Private Const GRID_CTRL_NAME_DESCRIPTION_TXT As String = "moDescriptionText"

    Private Const GRID_CTRL_NAME_CLAIM_ALLOWED_DDL As String = "moClaimAllowedDDL"
    Private Const GRID_CTRL_NAME_DEALER_DDL As String = "moDealerDDL"

    Private Const EDIT_COMMAND As String = "SelectAction"
    Private Const DELETE_COMMAND As String = "DeleteRecord"
    Private Const SORT_COMMAND As String = "Sort"

    Private Const NO_ROW_SELECTED_INDEX As Integer = -1
#End Region

#Region "Page State"
    ' This class keeps the current state for the search page.
    Class MyState
        Public MyBO As SuspendedReasons
        '  Public Roleid As Guid
        Public SuspendedReasonsID As Guid
        Public PageIndex As Integer = 0
        Public PageSize As Integer = DEFAULT_NEW_UI_PAGE_SIZE
        Public searchDV As SuspendedReasons.SearchDV = Nothing
        Public HasDataChanged As Boolean
        Public IsGridAddNew As Boolean = False
        Public IsEditMode As Boolean = False
        Public IsGridVisible As Boolean
        'Public IsAfterSave As Boolean
        Public SortExpression As String = SuspendedReasons.COL_NAME_DESCRIPTION
        Public SearchValues As SuspendedReasons.SearchDV.Values

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
#End Region

#Region "Bread Crum"
    Private Sub UpdateBreadCrum()
        With Me.MasterPage
            .PageTab = TranslationBase.TranslateLabelOrMessage(PAGETAB)
            .PageTitle = TranslationBase.TranslateLabelOrMessage(PAGETITLE)
            .UsePageTabTitleInBreadCrum = False
            .BreadCrum = TranslationBase.TranslateLabelOrMessage(PAGETAB) + ElitaBase.Sperator + TranslationBase.TranslateLabelOrMessage(PAGETITLE)
        End With
    End Sub
#End Region

#Region "Page Events"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Me.MasterPage.MessageController.Clear()
        Me.Form.DefaultButton = btnSearch.UniqueID

        Try
            If Not Me.IsPostBack Then
                UpdateBreadCrum()

                '** Load LOV List
                ' Me.BindListControlToDataView(Me.SearchDealerDD,
                'LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies, True))
                Dim oDealerList = GetDealerListByCompanyForUser()
                Dim dealerTextFunc As Func(Of DataElements.ListItem, String) = Function(li As DataElements.ListItem)
                                                                                   Return li.ExtendedCode + " - " + li.Translation + " " + "(" + li.Code + ")"
                                                                               End Function
                Me.SearchDealerDD.Populate(oDealerList, New PopulateOptions() With
                                           {
                                            .TextFunc = dealerTextFunc,
                                            .AddBlankItem = True
                                           })
                'Me.BindListTextToDataView(Me.SearchClaimAllowDD, LookupListNew.DropdownLookupList("YESNO", ElitaPlusIdentity.Current.ActiveUser.LanguageId, True), , "Code")
                Me.SearchClaimAllowDD.Populate(CommonConfigManager.Current.ListManager.GetList("YESNO", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
               {
                .AddBlankItem = True,
                .ValueFunc = AddressOf .GetCode
               })
                Me.SortDirection = SuspendedReasons.COL_NAME_CODE

                ControlMgr.SetVisibleControl(Me, moSearchResults, False)

                EnableControlState(True)

                Me.State.PageIndex = 0
                Me.TranslateGridHeader(Grid)
                Me.TranslateGridControls(Grid)
            Else
                '** Connect the Properties from the BO to the Columns on the Grid for the Error MSG.
                Me.BindBOPropertyToGridHeader(Me.State.MyBO, "DealerID", Me.Grid.Columns(Me.GRID_COL_DEALER_IDX))
                Me.BindBOPropertyToGridHeader(Me.State.MyBO, "Code", Me.Grid.Columns(Me.GRID_COL_CODE_IDX))
                Me.BindBOPropertyToGridHeader(Me.State.MyBO, "Description", Me.Grid.Columns(Me.GRID_COL_DESCRIPTION_IDX))
                Me.BindBOPropertyToGridHeader(Me.State.MyBO, "Claim_Allowed", Me.Grid.Columns(Me.GRID_COL_CLAIM_ALLOWDE_IDX))

                Me.ClearGridViewHeadersAndLabelsErrSign()
            End If

            Me.DisplayNewProgressBarOnClick(Me.btnSearch, "Loading")
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try

        Me.ShowMissingTranslations(Me.MasterPage.MessageController)
    End Sub
    Private Function GetDealerListByCompanyForUser() As Assurant.Elita.CommonConfiguration.DataElements.ListItem()
        Dim Index As Integer
        Dim oListContext As New Assurant.Elita.CommonConfiguration.ListContext

        Dim UserCompanies As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Companies

        Dim oDealerList As New Collections.Generic.List(Of Assurant.Elita.CommonConfiguration.DataElements.ListItem)

        For Index = 0 To UserCompanies.Count - 1
            'UserCompanyList &= ",'" & GuidControl.GuidToHexString(UserCompanyies(Index))
            oListContext.CompanyId = UserCompanies(Index)
            Dim oDealerListForCompany As Assurant.Elita.CommonConfiguration.DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="DealerListByCompany", context:=oListContext)
            If oDealerListForCompany.Count > 0 Then
                If Not oDealerList Is Nothing Then
                    oDealerList.AddRange(oDealerListForCompany)
                Else
                    oDealerList = oDealerListForCompany.Clone()
                End If

            End If
        Next

        Return oDealerList.ToArray()

    End Function
#End Region

#Region "Button click Handler"
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
        Try
            With Me.State
                .PageIndex = 0
                .SuspendedReasonsID = Guid.Empty
                .searchDV = Nothing
                .HasDataChanged = False
                .IsGridVisible = True
                .IsGridAddNew = False
            End With

            SaveStateFromControls()

            PopulateGrid()

        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub btnClearSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnClearSearch.Click
        Try
            With Me
                .SearchDealerDD.SelectedIndex = 0
                .SearchCodeTxt.Text = String.Empty
                .SearchDescriptionTxt.Text = String.Empty
                .SearchClaimAllowDD.SelectedIndex = 0

                .SaveStateFromControls()

                Grid.EditIndex = .NO_ITEM_SELECTED_INDEX

                .State.IsGridAddNew = False
                .State.SuspendedReasonsID = Guid.Empty
            End With
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnNew.Click

        Try
            With Me.State
                .IsGridAddNew = True
                .IsEditMode = True
                .IsGridVisible = True

                '*** Add New Code ****
                .MyBO = New SuspendedReasons
                .SuspendedReasonsID = .MyBO.Suspended_Reasons_Id
                .MyBO.AddNewRowToSearchDV(.searchDV, .MyBO)

                PopulateGrid(True, True)

                SetGridControls(Me.Grid, False)

                EnableControlState(False)
            End With
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try

    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim tempDropDown As DropDownList

        Try

            With Me.State.MyBO
                ' ** Edit Not Allow for this fields only New.
                If Me.State.IsGridAddNew Then
                    tempDropDown = CType(Grid.Rows((Me.Grid.EditIndex)).Cells(1).FindControl("moDealerDDL"), DropDownList)
                    .DealerId = GetSelectedItem(tempDropDown)
                    .Dealer_Name = GetSelectedDescription(tempDropDown)

                    .Code = CType(Grid.Rows(Me.Grid.EditIndex).Cells(2).FindControl("moCodeText"), TextBox).Text.Trim.ToUpper
                End If

                .Description = CType(Grid.Rows(Me.Grid.EditIndex).Cells(3).FindControl("moDescriptionText"), TextBox).Text.Trim

                tempDropDown = CType(Grid.Rows((Me.Grid.EditIndex)).Cells(4).FindControl("moClaimAllowedDDL"), DropDownList)

                .Claim_Allowed = GetSelectedValue(tempDropDown)
                .Claim_Allowed_Str = GetSelectedDescription(tempDropDown)
            End With


            If (Me.State.MyBO.IsDirty) Then
                Me.State.MyBO.Save(ElitaPlusIdentity.Current.ActiveUser.LanguageId, Me.State.IsGridAddNew)

                Me.State.IsGridAddNew = False

                Me.MasterPage.MessageController.AddSuccess(Me.MSG_RECORD_SAVED_OK, True)

                For Each drv As DataRowView In CType(Me.State.searchDV, DataView)
                    Dim cguid As Guid = New Guid(CType(drv.Item(SuspendedReasons.COL_NAME_ID), Byte()))

                    If cguid = Me.State.MyBO.Suspended_Reasons_Id Then
                        drv.Item(SuspendedReasons.COL_NAME_DEALER_ID) = Me.State.MyBO.DealerId.ToByteArray
                        drv.Item(SuspendedReasons.COL_NAME_DEALER_NAME) = Me.State.MyBO.Dealer_Name
                        drv.Item(SuspendedReasons.COL_NAME_CODE) = Me.State.MyBO.Code
                        drv.Item(SuspendedReasons.COL_NAME_DESCRIPTION) = Me.State.MyBO.Description
                        drv.Item(SuspendedReasons.COL_NAME_CLAIM_ALLOWED) = Me.State.MyBO.Claim_Allowed
                        drv.Item(SuspendedReasons.COL_NAME_CLAIM_ALLOWED_STR) = Me.State.MyBO.Claim_Allowed_Str
                        drv.Row.AcceptChanges()
                        Exit For
                    End If
                Next
            Else
                Me.MasterPage.MessageController.AddWarning(Me.MSG_RECORD_NOT_SAVED, True)
            End If

            Grid.EditIndex = NO_ROW_SELECTED_INDEX

            ControlMgr.SetVisibleControl(Me, Grid, (Me.Grid.PageCount <> 0))

            Me.State.IsEditMode = False
            Me.State.SuspendedReasonsID = State.MyBO.Suspended_Reasons_Id

            PopulateGrid(True, False)

            Me.State.PageIndex = Grid.PageIndex

            EnableControlState(True)

        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try

    End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As EventArgs)
        Try
            If State.IsGridAddNew Then
                '** This Removes New Row From SearchDV **
                If Not State.searchDV Is Nothing Then
                    Dim rowind As Integer = FindSelectedRowIndexFromGuid(State.searchDV, State.SuspendedReasonsID)

                    If rowind <> NO_ITEM_SELECTED_INDEX Then State.searchDV.Delete(rowind)
                End If

                Grid.PageIndex = State.PageIndex
            End If

            State.SuspendedReasonsID = Guid.Empty

            State.IsEditMode = False
            State.IsGridAddNew = False
            Grid.EditIndex = NO_ITEM_SELECTED_INDEX

            PopulateGrid(False)

            EnableControlState(True)

        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub
#End Region

#Region "Grid Handler"
    Public Sub RowCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)

        Try
            Dim index As Integer
            If (e.CommandName = Me.EDIT_COMMAND) Then
                index = CInt(e.CommandArgument)

                Me.State.IsEditMode = True

                ' *** Get Row ID ****
                Dim IdLabel As Label = CType(Me.Grid.Rows(index).Cells(Me.GRID_COL_ID_IDX).FindControl(Me.GRID_CTRL_NAME_ID), Label)
                Me.State.SuspendedReasonsID = New Guid(IdLabel.Text)

                '' *** Load Row to be edited into MyBO ****
                Me.State.MyBO = New SuspendedReasons(Me.State.SuspendedReasonsID, Me.State.searchDV.Table.DataSet)

                Me.PopulateGrid(True, True)

                EnableControlState(False)
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try

    End Sub

    Public Sub RowCreated(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
        Try
            BaseItemCreated(sender, e)
        Catch ex As Exception
            HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Grid_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowDataBound
        Try
            Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
            Dim strClaimAllowed As String
            Dim strActive As String
            Dim guiClaimAllowedId As Guid
            Dim guiActiveId As Guid


            'If List Item Types (2,3,4)
            If itemType = ListItemType.Item OrElse itemType = ListItemType.AlternatingItem OrElse itemType = ListItemType.SelectedItem Then

                If State.IsEditMode = True AndAlso Not (e.Row.DataItem Is Nothing) Then
                    Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
                    Dim gui_ID As Guid = New Guid(CType(dvRow(SuspendedReasons.COL_NAME_ID), Byte()))

                    If State.SuspendedReasonsID.Equals(gui_ID) Then
                        'Dim dvYesNo As DataView = LookupListNew.DropdownLookupList("YESNO", ElitaPlusIdentity.Current.ActiveUser.LanguageId, True)

                        '*** Dealer ***
                        Dim DealerDDL As DropDownList = CType(e.Row.Cells(GRID_COL_DEALER_IDX).FindControl(GRID_CTRL_NAME_DEALER_DDL), DropDownList)
                        Dim DealerLabel2 As Label = CType(e.Row.Cells(GRID_COL_CODE_IDX).FindControl(GRID_CTRL_NAME_DEALER_LABLE & "2"), Label)
                        If State.IsGridAddNew Then
                            DealerDDL.Visible = True
                            DealerLabel2.Visible = False
                            ' BindListControlToDataView(DealerDDL, LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies, False)) 'simple
                            Dim oDealerList = GetDealerListByCompanyForUser()
                            DealerDDL.Populate(oDealerList, New PopulateOptions() With
                                           {
                                            .AddBlankItem = True
                                           })
                            DealerDDL.SelectedIndex = SearchDealerDD.SelectedIndex
                        Else
                            DealerDDL.Visible = False
                            DealerLabel2.Visible = True
                            DealerLabel2.Text = dvRow.Item(SuspendedReasons.COL_NAME_DEALER_NAME).ToString  'SuspendedReasons.SearchDV.COL_NAME_DEALER_NAME) 'State.MyBO.Dealer_Name
                        End If

                        '*** Code ***
                        Dim CodeTextBox As TextBox = CType(e.Row.Cells(GRID_COL_CODE_IDX).FindControl(GRID_CTRL_NAME_CODE_TXT), TextBox)
                        Dim CodeLabel2 As Label = CType(e.Row.Cells(GRID_COL_CODE_IDX).FindControl(GRID_CTRL_NAME_CODE_LABLE & "2"), Label)
                        If State.IsGridAddNew Then
                            CodeLabel2.Visible = False
                            CodeTextBox.Visible = True
                            CodeTextBox.Text = State.MyBO.Code
                        Else
                            CodeLabel2.Visible = True
                            CodeTextBox.Visible = False
                            CodeLabel2.Text = dvRow.Item(SuspendedReasons.COL_NAME_CODE).ToString()
                        End If

                        '*** Description ***
                        Dim DescriptionTextBox As TextBox = CType(e.Row.Cells(GRID_COL_DESCRIPTION_IDX).FindControl(GRID_CTRL_NAME_DESCRIPTION_TXT), TextBox)
                        If Not State.IsGridAddNew Then
                            DescriptionTextBox.Text = dvRow.Item(SuspendedReasons.COL_NAME_DESCRIPTION).ToString()
                            DescriptionTextBox.Focus()
                        End If

                        '*** Claim Allow ***
                        Dim ClaimAllowedDDL As DropDownList = CType(e.Row.Cells(GRID_COL_CLAIM_ALLOWDE_IDX).FindControl(GRID_CTRL_NAME_CLAIM_ALLOWED_DDL), DropDownList)
                        '  BindListTextToDataView(ClaimAllowedDDL, dvYesNo, , "Code")
                        ClaimAllowedDDL.Populate(CommonConfigManager.Current.ListManager.GetList("YESNO", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
                      {
                      .AddBlankItem = True,
                      .ValueFunc = AddressOf .GetCode
                       })
                        If State.IsGridAddNew Then
                            ClaimAllowedDDL.SelectedIndex = SearchClaimAllowDD.SelectedIndex
                        Else
                            SetSelectedItem(ClaimAllowedDDL, dvRow.Item(SuspendedReasons.COL_NAME_CLAIM_ALLOWED).ToString())
                        End If
                    End If
                End If
            End If

        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try

    End Sub

    Private Sub Grid_PageIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Grid.PageIndexChanged
        Try
            If (Not (State.IsEditMode)) Then
                State.PageIndex = Grid.PageIndex
                State.SuspendedReasonsID = Guid.Empty

                PopulateGrid()
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Grid_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Grid.PageIndexChanging
        Try
            Grid.PageIndex = e.NewPageIndex
            State.PageIndex = Grid.PageIndex
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub
    Private Sub Grid_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles Grid.Sorting
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
            Me.State.SuspendedReasonsID = Guid.Empty
            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub cboPageSize_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
        Try
            State.PageSize = CType(cboPageSize.SelectedValue, Integer)
            Me.State.PageIndex = NewCurrentPageIndex(Grid, State.searchDV.Count, State.PageSize)
            Me.Grid.PageIndex = Me.State.PageIndex
            Me.State.SuspendedReasonsID = Guid.Empty
            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub SetFocusOnEditableFieldInGrid(ByVal grid As GridView, ByVal cellPosition As Integer, ByVal controlName As String, ByVal itemIndex As Integer)
        'Set focus on the Description TextBox for the EditItemIndex row
        Dim desc As TextBox = CType(grid.Rows(itemIndex).Cells(cellPosition).FindControl(controlName), TextBox)
        SetFocus(desc)
    End Sub

#End Region

#Region "Control"

    Private Sub SaveStateFromControls()

        With Me.State.SearchValues
            .Code = SearchCodeTxt.Text.Trim
            .Description = SearchDescriptionTxt.Text.Trim.Trim
            .Dealer = GetSelectedItem(SearchDealerDD)
            .Claim_Allowed = GetSelectedValue(SearchClaimAllowDD)
            .CompanyGroupID = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
            .CompanyID = Nothing
            .LanguageID = ElitaPlusIdentity.Current.ActiveUser.LanguageId
        End With

    End Sub
#End Region

#Region "Helper functions"

    Public Property SortDirection() As String
        Get
            Return ViewState("SortDirection").ToString
        End Get
        Set(ByVal value As String)
            ViewState("SortDirection") = value
        End Set
    End Property

    Public Sub PopulateGrid(Optional ByVal DisplayNoDataFoundMsg As Boolean = True _
                          , Optional ByVal SetGridEditMode As Boolean = False)


        Dim rowCount As Integer = 0

        Dim SetToRowId As System.Guid = Me.State.SuspendedReasonsID

        Try
            If (State.searchDV Is Nothing) Then
                State.searchDV = SuspendedReasons.LoadSearchData(State.SearchValues)
                Grid.DataSource = State.searchDV
            End If

            rowCount = State.searchDV.Count
            State.searchDV.Sort = Me.SortDirection

            If (rowCount = 0) Then
                Me.State.searchDV = Nothing
                Me.Grid.DataSource = Nothing
                Me.Grid.DataBind()

                Me.State.IsGridVisible = False

                If DisplayNoDataFoundMsg Then
                    Me.MasterPage.MessageController.AddInformation(ElitaPlus.ElitaPlusWebApp.Message.MSG_NO_RECORDS_FOUND, True)
                End If
            Else
                Me.TranslateGridControls(Grid)

                Me.SetPageAndSelectedIndexFromGuid(State.searchDV, SetToRowId, Me.Grid, State.PageIndex, SetGridEditMode)

                Me.State.IsGridVisible = True

                With Me.Grid
                    .Enabled = True
                    .PageSize = Me.State.PageSize
                    .DataSource = Me.State.searchDV

                    .AutoGenerateColumns = False
                    .Columns(Me.GRID_COL_DEALER_IDX).SortExpression = SuspendedReasons.COL_NAME_DEALER_NAME
                    .Columns(Me.GRID_COL_CODE_IDX).SortExpression = SuspendedReasons.COL_NAME_CODE
                    .Columns(Me.GRID_COL_DESCRIPTION_IDX).SortExpression = SuspendedReasons.COL_NAME_DESCRIPTION

                    HighLightSortColumn(Grid, Me.SortDirection)

                    .DataBind()
                End With

                If (Me.State.IsGridAddNew) Then
                    rowCount = rowCount - 1
                End If

                Me.lblRecordCount.Text = rowCount & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)

                ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me, Grid)

                If rowCount >= 100 Then
                    Me.MasterPage.MessageController.AddInformation(Message.MSG_MAX_LIMIT_EXCEEDED_REFINE_SEARCH_CRITERIA, True)
                End If
            End If

            ControlMgr.SetVisibleControl(Me, Grid, Me.State.IsGridVisible)
            ControlMgr.SetVisibleControl(Me, moSearchResults, Me.State.IsGridVisible)

            Session("recCount") = rowCount

        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try

    End Sub

    Private Sub EnableControlState(ByVal EnableControls As Boolean)

        If EnableControls Then
            ControlMgr.SetVisibleControl(Me, btnNew, True)
            ControlMgr.SetEnableControl(Me, btnSearch, True)
            ControlMgr.SetEnableControl(Me, btnClearSearch, True)

            Me.MenuEnabled = True

            If Not (Me.cboPageSize.Enabled) Then
                ControlMgr.SetEnableControl(Me, Me.cboPageSize, True)
            End If
        Else
            ControlMgr.SetVisibleControl(Me, btnNew, False)
            ControlMgr.SetEnableControl(Me, btnSearch, False)
            ControlMgr.SetEnableControl(Me, btnClearSearch, False)

            Me.MenuEnabled = False

            If (Me.cboPageSize.Enabled) Then
                ControlMgr.SetEnableControl(Me, cboPageSize, False)
            End If

        End If

    End Sub
#End Region

End Class
