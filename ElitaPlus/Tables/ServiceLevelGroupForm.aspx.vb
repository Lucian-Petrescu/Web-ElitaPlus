Imports System.Collections.Generic
Imports System.Web.Services
Imports System.Threading
Imports Assurant.Elita.Web.Forms
Imports System.Web.Script.Services
Imports Assurant.ElitaPlus.Security
Imports System.Web.Script.Serialization
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Partial Class ServiceLevelGroupForm
    Inherits ElitaPlusSearchPage

    Protected WithEvents ErrorCtrl As ErrorController


#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Constants"
    Public Const GRID_COL_EDIT As Integer = 0
    Public Const GRID_COL_COUNTRY As Integer = 1
    Public Const GRID_COL_CODE As Integer = 2
    Public Const GRID_COL_DESCRIPTION As Integer = 3
    Public Const GRID_COL_SLG_ID As Integer = 4


    Private Const MSG_CONFIRM_PROMPT As String = "MSG_CONFIRM_PROMPT"
    Private Const MSG_RECORD_DELETED_OK As String = "MSG_RECORD_DELETED_OK"
    Private Const MSG_RECORD_SAVED_OK As String = "MSG_RECORD_SAVED_OK"
    Private Const MSG_RECORD_NOT_SAVED As String = "MSG_RECORD_NOT_SAVED"
#End Region

#Region "Page State"
    Private IsReturningFromChild As Boolean = False

    Class MyState
        Public PageIndex As Integer = 0
        Public SortExpression As String = ServiceLevelGroup.ServiceLevelGroupSearchDV.COL_NAME_CODE
        Public SelectedSLGId As Guid = Guid.Empty
        Public IsGridVisible As Boolean
        Public SearchCode As String
        Public SearchDescription As String
        Public SearchCountryId As Guid
        Public selectedPageSize As Integer = DEFAULT_PAGE_SIZE
        Public searchDV As ServiceLevelGroup.ServiceLevelGroupSearchDV = Nothing
        Public HasDataChanged As Boolean
        Public IsEditMode As Boolean
        Public myBO As ServiceLevelGroup
        Public IsGridAddNew As Boolean = False
        Public Canceling As Boolean
        Public AddingNewRow As Boolean
        Public IsAfterSave As Boolean
        Public bNoRow As Boolean = False
        Public FromDateMask As String = String.Empty
        Public ToDateMask As String = String.Empty
        Sub New()
        End Sub
    End Class

    Public Sub New()
        MyBase.New(New MyState)
    End Sub

    Protected Shadows ReadOnly Property State() As MyState
        Get
            Return CType(MyBase.State, MyState)
        End Get
    End Property

    Private Sub Page_PageReturn(ByVal ReturnFromUrl As String, ByVal ReturnPar As Object) Handles MyBase.PageReturn
        Try
            Me.MenuEnabled = True
            Me.IsReturningFromChild = True
            Dim retObj As ServiceLevelDetailForm.ReturnType = CType(ReturnPar, ServiceLevelDetailForm.ReturnType)
            Me.State.HasDataChanged = retObj.HasDataChanged
            Select Case retObj.LastOperation
                Case ElitaPlusPage.DetailPageCommand.Back
                    If Not retObj Is Nothing Then
                        If Not retObj.EditingBo.IsNew Then
                            Me.State.SelectedSLGId = retObj.EditingBo.Id
                        End If
                        Me.State.IsGridVisible = True
                    End If
                Case ElitaPlusPage.DetailPageCommand.Delete
                    Me.DisplayMessage(Message.DELETE_RECORD_CONFIRMATION, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
            End Select
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

    Private Sub SaveGuiState()
        Me.State.SearchCountryId = Me.GetSelectedItem(moCountryDrop)
        Me.State.SearchDescription = Me.TextBoxSearchDescription.Text
        Me.State.SearchCode = Me.TextBoxSearchCode.Text
    End Sub

    Private Sub RestoreGuiState()
        Me.SetSelectedItem(moCountryDrop, Me.State.SearchCountryId)
        Me.TextBoxSearchDescription.Text = Me.State.SearchDescription
        Me.TextBoxSearchCode.Text = Me.State.SearchCode
    End Sub

#End Region

#Region "Page_Events"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Try
            Me.ErrorCtrl.Clear_Hide()

            If Not Me.IsPostBack Then
                Me.SetDefaultButton(Me.TextBoxSearchCode, btnSearch)
                Me.SetDefaultButton(Me.TextBoxSearchDescription, btnSearch)
                ControlMgr.SetVisibleControl(Me, trPageSize, False)
                Me.AddCalendar(Me.btnDateFrom, Me.txtDateFrom)
                Me.AddCalendar(Me.btnDateTo, Me.txtDateTo)
                SetButtonsState()
                PopulateCountry()
                Me.RestoreGuiState()
                If Me.State.IsGridVisible Then
                    If Not (Me.State.selectedPageSize = DEFAULT_PAGE_SIZE) Then
                        cboPageSize.SelectedValue = CType(Me.State.selectedPageSize, String)
                        Grid.PageSize = Me.State.selectedPageSize
                    End If
                    Me.PopulateGrid()
                End If
                Me.SetGridItemStyleColor(Me.Grid)
            Else
                Me.SaveGuiState()
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
        Me.ShowMissingTranslations(Me.ErrorCtrl)
    End Sub

#End Region

#Region "Controlling Logic"

    Private Sub PopulateCountry()
        ' Me.BindListControlToDataView(moCountryDrop, LookupListNew.GetUserCountriesLookupList())
        Dim countryList As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="Country")

        Dim filteredCountryList As DataElements.ListItem() = (From x In countryList
                                                              Where ElitaPlusIdentity.Current.ActiveUser.Countries.Contains(x.ListItemId)
                                                              Select x).ToArray()

        Me.moCountryDrop.Populate(filteredCountryList, New PopulateOptions() With
                                                   {
                                                    .AddBlankItem = True
                                                   })

        If moCountryDrop.Items.Count < 3 Then
            ControlMgr.SetVisibleControl(Me, moCountryLabel, False)
            ControlMgr.SetVisibleControl(Me, moCountryDrop, False)
        End If
    End Sub

    Public Sub PopulateGrid()

        If ((Me.State.searchDV Is Nothing) OrElse (Me.State.HasDataChanged)) Then
            Me.State.searchDV = ServiceLevelGroup.getList(Me.TextBoxSearchCode.Text,
            Me.TextBoxSearchDescription.Text, Me.State.SearchCountryId, Me.State.FromDateMask, Me.State.ToDateMask)

        End If

        Me.State.searchDV.Sort = Me.State.SortExpression

        Me.Grid.AutoGenerateColumns = False
        Me.Grid.Columns(Me.GRID_COL_COUNTRY).SortExpression = ServiceLevelGroup.ServiceLevelGroupSearchDV.COL_COUNTRY_DESC
        Me.Grid.Columns(Me.GRID_COL_CODE).SortExpression = ServiceLevelGroup.ServiceLevelGroupSearchDV.COL_NAME_CODE
        Me.Grid.Columns(Me.GRID_COL_DESCRIPTION).SortExpression = ServiceLevelGroup.ServiceLevelGroupSearchDV.COL_NAME_DESCRIPTION

        SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.SelectedSLGId, Me.Grid, Me.State.PageIndex)
        Me.SortAndBindGrid()

    End Sub

    Private Sub SortAndBindGrid()
        'Me.State.PageIndex = Me.Grid.PageIndex
        'Me.Grid.DataSource = Me.State.searchDV
        'HighLightSortColumn(Grid, Me.State.SortExpression)
        'Me.Grid.DataBind()

        'ControlMgr.SetVisibleControl(Me, Grid, Me.State.IsGridVisible)

        'ControlMgr.SetVisibleControl(Me, trPageSize, Me.Grid.Visible)

        If (Me.State.searchDV.Count = 0) Then
            Me.State.bNoRow = True
            CreateHeaderForEmptyGrid(Grid, Me.State.SortExpression)
            ControlMgr.SetVisibleControl(Me, Grid, Me.State.IsGridVisible)
            ControlMgr.SetVisibleControl(Me, trPageSize, Me.Grid.Visible)
            Grid.PagerSettings.Visible = True
            If Not Grid.BottomPagerRow.Visible Then Grid.BottomPagerRow.Visible = True
        Else
            Me.State.bNoRow = False
            Me.Grid.Enabled = True
            Me.Grid.DataSource = Me.State.searchDV
            HighLightSortColumn(Grid, Me.State.SortExpression)
            Me.Grid.DataBind()
            ControlMgr.SetVisibleControl(Me, Grid, Me.State.IsGridVisible)
            ControlMgr.SetVisibleControl(Me, trPageSize, Me.Grid.Visible)
            If Not Grid.BottomPagerRow.Visible Then Grid.BottomPagerRow.Visible = True
        End If

        Session("recCount") = Me.State.searchDV.Count

        If Me.State.searchDV.Count > 0 Then

            If Me.Grid.Visible Then
                Me.lblRecordCount.Text = Me.State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
            End If
        Else
            If Me.Grid.Visible Then
                Me.lblRecordCount.Text = Me.State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
            End If
        End If
    End Sub


#End Region


#Region " Datagrid Related "

    'The Binding Logic is here  
    Private Sub Grid_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowDataBound
        Try
            Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
            Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)

            If Not dvRow Is Nothing And Not Me.State.bNoRow Then
                If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then
                    CType(e.Row.Cells(Me.GRID_COL_SLG_ID).FindControl("SLGIdLabel"), Label).Text = GetGuidStringFromByteArray(CType(dvRow(ServiceLevelGroup.ServiceLevelGroupSearchDV.COL_NAME_SERVICE_LEVEL_GROUP_ID), Byte()))

                    If (Me.State.IsEditMode = True AndAlso Me.State.SelectedSLGId.ToString.Equals(GetGuidStringFromByteArray(CType(dvRow(ServiceLevelGroup.ServiceLevelGroupSearchDV.COL_NAME_SERVICE_LEVEL_GROUP_ID), Byte())))) Then

                        CType(e.Row.Cells(Me.GRID_COL_CODE).FindControl("SLGCodeTextBox"), TextBox).Text = dvRow(ServiceLevelGroup.ServiceLevelGroupSearchDV.COL_NAME_CODE).ToString
                        CType(e.Row.Cells(Me.GRID_COL_DESCRIPTION).FindControl("SLGDescTextBox"), TextBox).Text = dvRow(ServiceLevelGroup.ServiceLevelGroupSearchDV.COL_NAME_DESCRIPTION).ToString
                        Dim CountryList As DropDownList = CType(e.Row.Cells(Me.GRID_COL_COUNTRY).FindControl("CountryDropdown"), DropDownList)
                        PopulateDropdown(CountryList)

                    Else
                        CType(e.Row.Cells(Me.GRID_COL_COUNTRY).FindControl("CountryLabel"), Label).Text = dvRow(ServiceLevelGroup.ServiceLevelGroupSearchDV.COL_COUNTRY_DESC).ToString
                        CType(e.Row.Cells(Me.GRID_COL_CODE).FindControl("CodeLabel"), Label).Text = dvRow(ServiceLevelGroup.ServiceLevelGroupSearchDV.COL_NAME_CODE).ToString
                        CType(e.Row.Cells(Me.GRID_COL_DESCRIPTION).FindControl("DescriptionLabel"), Label).Text = dvRow(ServiceLevelGroup.ServiceLevelGroupSearchDV.COL_NAME_DESCRIPTION).ToString
                    End If
                End If
            End If


        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

    Private Sub Grid_PageSizeChanged(ByVal source As Object, ByVal e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
        Try
            Grid.PageIndex = NewCurrentPageIndex(Grid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
            Me.State.selectedPageSize = CType(cboPageSize.SelectedValue, Integer)
            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

    Private Sub Grid_SortCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles Grid.Sorting
        Try
            If Me.State.SortExpression.StartsWith(e.SortExpression) Then
                If Me.State.SortExpression.EndsWith(" DESC") Then
                    Me.State.SortExpression = e.SortExpression
                Else
                    Me.State.SortExpression &= " DESC"
                End If
            Else
                Me.State.SortExpression = e.SortExpression
            End If
            Me.State.PageIndex = 0
            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

    Public Sub Grid_RowCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles Grid.RowCommand
        Try
            Dim index As Integer = -1
            If e.CommandName = "SelectAction" Then
                index = CInt(e.CommandArgument)
                Me.State.SelectedSLGId = New Guid(CType(Grid.Rows(index).Cells(GRID_COL_SLG_ID).FindControl("SLGIdLabel"), Label).Text)
                Me.callPage(ServiceLevelDetailForm.URL, Me.State.SelectedSLGId)

            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try

    End Sub

    Public Sub ItemCreated(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
        BaseItemCreated(sender, e)
    End Sub

    Private Sub Grid_PageIndexChanging(ByVal source As Object, ByVal e As GridViewPageEventArgs) Handles Grid.PageIndexChanging
        Try
            Me.State.PageIndex = e.NewPageIndex
            Me.State.SelectedSLGId = Guid.Empty
            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub


#End Region

#Region " Button Clicks "

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            Me.State.PageIndex = 0
            Me.State.SelectedSLGId = Guid.Empty
            Me.State.IsGridVisible = True
            Me.State.searchDV = Nothing
            Me.State.HasDataChanged = False
            If txtDateFrom.Text <> "" Then
                Me.State.FromDateMask = DateHelper.GetDateValue(Me.txtDateFrom.Text).ToString("yyyyMMdd")
            End If
            If txtDateTo.Text <> "" Then
                Me.State.ToDateMask = DateHelper.GetDateValue(Me.txtDateTo.Text).ToString("yyyyMMdd")
            End If
            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub



    Private Sub btnAdd_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd_WRITE.Click

        Try
            Me.State.IsEditMode = True
            Me.State.IsGridVisible = True
            Me.State.IsGridAddNew = True
            Me.State.HasDataChanged = True
            Me.State.AddingNewRow = True
            AddNew()
            SetButtonsState()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try

    End Sub

    Private Sub btnClearSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearSearch.Click
        Try
            Me.TextBoxSearchCode.Text = ""
            Me.TextBoxSearchDescription.Text = ""
            moCountryDrop.SelectedIndex = Me.BLANK_ITEM_SELECTED
            txtDateFrom.Text = String.Empty
            State.FromDateMask = String.Empty
            txtDateTo.Text = String.Empty
            State.ToDateMask = String.Empty
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

    Private Sub SaveButton_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveButton_WRITE.Click
        Try
            Dim errors() As ValidationError = {New ValidationError("Country is required", GetType(ServiceLevelGroup), Nothing, "country_id", Nothing)}
            PopulateBOFromForm()
            'If ((Me.State.myBO.DealerGroupId.ToString = Guid.Empty.ToString) And (Me.State.myBO.DealerId.ToString = Guid.Empty.ToString)) Then
            '    Throw New BOValidationException(errors, GetType(BillingPlan).FullName)
            'End If
            If (Me.State.myBO.IsDirty) Then
                Me.State.myBO.Save()
                Me.State.IsAfterSave = True
                Me.State.AddingNewRow = False
                Me.AddInfoMsg(Me.MSG_RECORD_SAVED_OK)
                Me.State.searchDV = Nothing
                Me.ReturnFromEditing()
            Else
                Me.AddInfoMsg(Me.MSG_RECORD_NOT_SAVED)
                Me.ReturnFromEditing()
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try

    End Sub

    Private Sub CancelButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CancelButton.Click

        Try
            Me.Grid.SelectedIndex = Me.NO_ITEM_SELECTED_INDEX
            Me.State.Canceling = True
            If (Me.State.AddingNewRow) Then
                Me.State.AddingNewRow = False
                Me.State.searchDV = Nothing
            End If
            ReturnFromEditing()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try

    End Sub

#End Region

#Region "Helper Functions"
    Private Sub AddNew()
        Me.State.searchDV = GetGridDataView()

        Me.State.myBO = New ServiceLevelGroup
        Me.State.SelectedSLGId = Me.State.myBO.Id

        Me.State.searchDV = Me.State.myBO.GetNewDataViewRow(Me.State.searchDV, Me.State.myBO)

        Grid.DataSource = Me.State.searchDV

        Me.SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.SelectedSLGId, Me.Grid, Me.State.PageIndex, Me.State.IsEditMode)

        Me.Grid.AutoGenerateColumns = False

        SortAndBindGrid()
        SetGridControls(Me.Grid, False)


        PopulateFormFromBO()
    End Sub

    Private Function GetGridDataView() As ServiceLevelGroup.ServiceLevelGroupSearchDV

        With State
            Return (ServiceLevelGroup.getList(Me.TextBoxSearchCode.Text,
            Me.TextBoxSearchDescription.Text, Me.State.SearchCountryId, Me.State.FromDateMask, Me.State.ToDateMask))
        End With

    End Function

    Private Sub SetButtonsState()

        If (Me.State.IsEditMode) Then
            ControlMgr.SetVisibleControl(Me, SaveButton_WRITE, True)
            ControlMgr.SetVisibleControl(Me, CancelButton, True)
            ControlMgr.SetVisibleControl(Me, btnAdd_WRITE, False)
            ControlMgr.SetEnableControl(Me, btnSearch, False)
            ControlMgr.SetEnableControl(Me, btnClearSearch, False)
            Me.MenuEnabled = False
            If (Me.cboPageSize.Visible) Then
                ControlMgr.SetEnableControl(Me, cboPageSize, False)
            End If
        Else
            ControlMgr.SetVisibleControl(Me, SaveButton_WRITE, False)
            ControlMgr.SetVisibleControl(Me, CancelButton, False)
            ControlMgr.SetVisibleControl(Me, btnAdd_WRITE, True)
            ControlMgr.SetEnableControl(Me, btnSearch, True)
            ControlMgr.SetEnableControl(Me, btnClearSearch, True)
            Me.MenuEnabled = True
            If (Me.cboPageSize.Visible) Then
                ControlMgr.SetEnableControl(Me, Me.cboPageSize, True)
            End If
        End If

    End Sub


    Private Sub ReturnFromEditing()

        Grid.EditIndex = NO_ITEM_SELECTED_INDEX

        If Me.Grid.PageCount = 0 Then
            'if returning to the "1st time in" screen
            ControlMgr.SetVisibleControl(Me, Grid, False)
        Else
            ControlMgr.SetVisibleControl(Me, Grid, True)
        End If

        SetGridControls(Grid, True)
        Me.State.IsEditMode = False
        Me.PopulateGrid()
        Me.State.PageIndex = Grid.PageIndex
        SetButtonsState()

    End Sub
    Private Sub PopulateDropdown(ByVal CountryList As DropDownList)
        Try
            'Me.BindListControlToDataView(CountryList, LookupListNew.GetUserCountriesLookupList())
            Dim allCountries As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="Country")

            Dim filteredCountryList As DataElements.ListItem() = (From x In allCountries
                                                                  Where ElitaPlusIdentity.Current.ActiveUser.Countries.Contains(x.ListItemId)
                                                                  Select x).ToArray()

            CountryList.Populate(filteredCountryList, New PopulateOptions() With
                                                   {
                                                    .AddBlankItem = True
                                                   })
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try

    End Sub

    Private Sub PopulateFormFromBO()

        Dim gridRowIdx As Integer = Me.Grid.EditIndex
        Try
            With Me.State.myBO


                If (Not .Code Is Nothing) Then
                    CType(Me.Grid.Rows(gridRowIdx).Cells(Me.GRID_COL_CODE).FindControl("SLGCodeTextBox"), TextBox).Text = .Code
                End If
                If (Not .Description Is Nothing) Then
                    CType(Me.Grid.Rows(gridRowIdx).Cells(Me.GRID_COL_DESCRIPTION).FindControl("SLGDescTextBox"), TextBox).Text = .Description
                End If

                Dim CountryList As DropDownList = CType(Me.Grid.Rows(gridRowIdx).Cells(Me.GRID_COL_COUNTRY).FindControl("CountryDropdown"), DropDownList)

                PopulateDropdown(CountryList)

                Me.SetSelectedItem(CountryList, .CountryId)


            End With
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try

    End Sub



    Private Sub PopulateBOFromForm()

        Try
            With Me.State.myBO
                .CountryId = New Guid(CType(Me.Grid.Rows(Me.Grid.EditIndex).Cells(Me.GRID_COL_COUNTRY).FindControl("CountryDropdown"), DropDownList).SelectedValue)
                .Code = CType(Me.Grid.Rows(Me.Grid.EditIndex).Cells(Me.GRID_COL_CODE).FindControl("SLGCodeTextBox"), TextBox).Text
                .Description = CType(Me.Grid.Rows(Me.Grid.EditIndex).Cells(Me.GRID_COL_DESCRIPTION).FindControl("SLGDescTextBox"), TextBox).Text

            End With
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try

    End Sub


#End Region








End Class
