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

    Private Sub Page_Init(sender As System.Object, e As System.EventArgs) Handles MyBase.Init
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

    Private Sub Page_PageReturn(ReturnFromUrl As String, ReturnPar As Object) Handles MyBase.PageReturn
        Try
            MenuEnabled = True
            IsReturningFromChild = True
            Dim retObj As ServiceLevelDetailForm.ReturnType = CType(ReturnPar, ServiceLevelDetailForm.ReturnType)
            State.HasDataChanged = retObj.HasDataChanged
            Select Case retObj.LastOperation
                Case ElitaPlusPage.DetailPageCommand.Back
                    If retObj IsNot Nothing Then
                        If Not retObj.EditingBo.IsNew Then
                            State.SelectedSLGId = retObj.EditingBo.Id
                        End If
                        State.IsGridVisible = True
                    End If
                Case ElitaPlusPage.DetailPageCommand.Delete
                    DisplayMessage(Message.DELETE_RECORD_CONFIRMATION, "", MSG_BTN_OK, MSG_TYPE_INFO)
            End Select
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

    Private Sub SaveGuiState()
        State.SearchCountryId = GetSelectedItem(moCountryDrop)
        State.SearchDescription = TextBoxSearchDescription.Text
        State.SearchCode = TextBoxSearchCode.Text
    End Sub

    Private Sub RestoreGuiState()
        SetSelectedItem(moCountryDrop, State.SearchCountryId)
        TextBoxSearchDescription.Text = State.SearchDescription
        TextBoxSearchCode.Text = State.SearchCode
    End Sub

#End Region

#Region "Page_Events"
    Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Try
            ErrorCtrl.Clear_Hide()

            If Not IsPostBack Then
                SetDefaultButton(TextBoxSearchCode, btnSearch)
                SetDefaultButton(TextBoxSearchDescription, btnSearch)
                ControlMgr.SetVisibleControl(Me, trPageSize, False)
                AddCalendar(btnDateFrom, txtDateFrom)
                AddCalendar(btnDateTo, txtDateTo)
                SetButtonsState()
                PopulateCountry()
                RestoreGuiState()
                If State.IsGridVisible Then
                    If Not (State.selectedPageSize = DEFAULT_PAGE_SIZE) Then
                        cboPageSize.SelectedValue = CType(State.selectedPageSize, String)
                        Grid.PageSize = State.selectedPageSize
                    End If
                    PopulateGrid()
                End If
                SetGridItemStyleColor(Grid)
            Else
                SaveGuiState()
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
        ShowMissingTranslations(ErrorCtrl)
    End Sub

#End Region

#Region "Controlling Logic"

    Private Sub PopulateCountry()
        ' Me.BindListControlToDataView(moCountryDrop, LookupListNew.GetUserCountriesLookupList())
        Dim countryList As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="Country")

        Dim filteredCountryList As DataElements.ListItem() = (From x In countryList
                                                              Where ElitaPlusIdentity.Current.ActiveUser.Countries.Contains(x.ListItemId)
                                                              Select x).ToArray()

        moCountryDrop.Populate(filteredCountryList, New PopulateOptions() With
                                                   {
                                                    .AddBlankItem = True
                                                   })

        If moCountryDrop.Items.Count < 3 Then
            ControlMgr.SetVisibleControl(Me, moCountryLabel, False)
            ControlMgr.SetVisibleControl(Me, moCountryDrop, False)
        End If
    End Sub

    Public Sub PopulateGrid()

        If ((State.searchDV Is Nothing) OrElse (State.HasDataChanged)) Then
            State.searchDV = ServiceLevelGroup.getList(TextBoxSearchCode.Text,
            TextBoxSearchDescription.Text, State.SearchCountryId, State.FromDateMask, State.ToDateMask)

        End If

        State.searchDV.Sort = State.SortExpression

        Grid.AutoGenerateColumns = False
        Grid.Columns(GRID_COL_COUNTRY).SortExpression = ServiceLevelGroup.ServiceLevelGroupSearchDV.COL_COUNTRY_DESC
        Grid.Columns(GRID_COL_CODE).SortExpression = ServiceLevelGroup.ServiceLevelGroupSearchDV.COL_NAME_CODE
        Grid.Columns(GRID_COL_DESCRIPTION).SortExpression = ServiceLevelGroup.ServiceLevelGroupSearchDV.COL_NAME_DESCRIPTION

        SetPageAndSelectedIndexFromGuid(State.searchDV, State.SelectedSLGId, Grid, State.PageIndex)
        SortAndBindGrid()

    End Sub

    Private Sub SortAndBindGrid()
        'Me.State.PageIndex = Me.Grid.PageIndex
        'Me.Grid.DataSource = Me.State.searchDV
        'HighLightSortColumn(Grid, Me.State.SortExpression)
        'Me.Grid.DataBind()

        'ControlMgr.SetVisibleControl(Me, Grid, Me.State.IsGridVisible)

        'ControlMgr.SetVisibleControl(Me, trPageSize, Me.Grid.Visible)

        If (State.searchDV.Count = 0) Then
            State.bNoRow = True
            CreateHeaderForEmptyGrid(Grid, State.SortExpression)
            ControlMgr.SetVisibleControl(Me, Grid, State.IsGridVisible)
            ControlMgr.SetVisibleControl(Me, trPageSize, Grid.Visible)
            Grid.PagerSettings.Visible = True
            If Not Grid.BottomPagerRow.Visible Then Grid.BottomPagerRow.Visible = True
        Else
            State.bNoRow = False
            Grid.Enabled = True
            Grid.DataSource = State.searchDV
            HighLightSortColumn(Grid, State.SortExpression)
            Grid.DataBind()
            ControlMgr.SetVisibleControl(Me, Grid, State.IsGridVisible)
            ControlMgr.SetVisibleControl(Me, trPageSize, Grid.Visible)
            If Not Grid.BottomPagerRow.Visible Then Grid.BottomPagerRow.Visible = True
        End If

        Session("recCount") = State.searchDV.Count

        If State.searchDV.Count > 0 Then

            If Grid.Visible Then
                lblRecordCount.Text = State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
            End If
        Else
            If Grid.Visible Then
                lblRecordCount.Text = State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
            End If
        End If
    End Sub


#End Region


#Region " Datagrid Related "

    'The Binding Logic is here  
    Private Sub Grid_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowDataBound
        Try
            Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
            Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)

            If dvRow IsNot Nothing AndAlso Not State.bNoRow Then
                If itemType = ListItemType.Item OrElse itemType = ListItemType.AlternatingItem OrElse itemType = ListItemType.SelectedItem Then
                    CType(e.Row.Cells(GRID_COL_SLG_ID).FindControl("SLGIdLabel"), Label).Text = GetGuidStringFromByteArray(CType(dvRow(ServiceLevelGroup.ServiceLevelGroupSearchDV.COL_NAME_SERVICE_LEVEL_GROUP_ID), Byte()))

                    If (State.IsEditMode = True AndAlso State.SelectedSLGId.ToString.Equals(GetGuidStringFromByteArray(CType(dvRow(ServiceLevelGroup.ServiceLevelGroupSearchDV.COL_NAME_SERVICE_LEVEL_GROUP_ID), Byte())))) Then

                        CType(e.Row.Cells(GRID_COL_CODE).FindControl("SLGCodeTextBox"), TextBox).Text = dvRow(ServiceLevelGroup.ServiceLevelGroupSearchDV.COL_NAME_CODE).ToString
                        CType(e.Row.Cells(GRID_COL_DESCRIPTION).FindControl("SLGDescTextBox"), TextBox).Text = dvRow(ServiceLevelGroup.ServiceLevelGroupSearchDV.COL_NAME_DESCRIPTION).ToString
                        Dim CountryList As DropDownList = CType(e.Row.Cells(GRID_COL_COUNTRY).FindControl("CountryDropdown"), DropDownList)
                        PopulateDropdown(CountryList)

                    Else
                        CType(e.Row.Cells(GRID_COL_COUNTRY).FindControl("CountryLabel"), Label).Text = dvRow(ServiceLevelGroup.ServiceLevelGroupSearchDV.COL_COUNTRY_DESC).ToString
                        CType(e.Row.Cells(GRID_COL_CODE).FindControl("CodeLabel"), Label).Text = dvRow(ServiceLevelGroup.ServiceLevelGroupSearchDV.COL_NAME_CODE).ToString
                        CType(e.Row.Cells(GRID_COL_DESCRIPTION).FindControl("DescriptionLabel"), Label).Text = dvRow(ServiceLevelGroup.ServiceLevelGroupSearchDV.COL_NAME_DESCRIPTION).ToString
                    End If
                End If
            End If


        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

    Private Sub Grid_PageSizeChanged(source As Object, e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
        Try
            Grid.PageIndex = NewCurrentPageIndex(Grid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
            State.selectedPageSize = CType(cboPageSize.SelectedValue, Integer)
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

    Private Sub Grid_SortCommand(source As System.Object, e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles Grid.Sorting
        Try
            If State.SortExpression.StartsWith(e.SortExpression) Then
                If State.SortExpression.EndsWith(" DESC") Then
                    State.SortExpression = e.SortExpression
                Else
                    State.SortExpression &= " DESC"
                End If
            Else
                State.SortExpression = e.SortExpression
            End If
            State.PageIndex = 0
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

    Public Sub Grid_RowCommand(source As System.Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles Grid.RowCommand
        Try
            Dim index As Integer = -1
            If e.CommandName = "SelectAction" Then
                index = CInt(e.CommandArgument)
                State.SelectedSLGId = New Guid(CType(Grid.Rows(index).Cells(GRID_COL_SLG_ID).FindControl("SLGIdLabel"), Label).Text)
                callPage(ServiceLevelDetailForm.URL, State.SelectedSLGId)

            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try

    End Sub

    Public Sub ItemCreated(sender As System.Object, e As System.Web.UI.WebControls.GridViewRowEventArgs)
        BaseItemCreated(sender, e)
    End Sub

    Private Sub Grid_PageIndexChanging(source As Object, e As GridViewPageEventArgs) Handles Grid.PageIndexChanging
        Try
            State.PageIndex = e.NewPageIndex
            State.SelectedSLGId = Guid.Empty
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub


#End Region

#Region " Button Clicks "

    Private Sub btnSearch_Click(sender As Object, e As System.EventArgs) Handles btnSearch.Click
        Try
            State.PageIndex = 0
            State.SelectedSLGId = Guid.Empty
            State.IsGridVisible = True
            State.searchDV = Nothing
            State.HasDataChanged = False
            If txtDateFrom.Text <> "" Then
                State.FromDateMask = DateHelper.GetDateValue(txtDateFrom.Text).ToString("yyyyMMdd")
            End If
            If txtDateTo.Text <> "" Then
                State.ToDateMask = DateHelper.GetDateValue(txtDateTo.Text).ToString("yyyyMMdd")
            End If
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub



    Private Sub btnAdd_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnAdd_WRITE.Click

        Try
            State.IsEditMode = True
            State.IsGridVisible = True
            State.IsGridAddNew = True
            State.HasDataChanged = True
            State.AddingNewRow = True
            AddNew()
            SetButtonsState()
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try

    End Sub

    Private Sub btnClearSearch_Click(sender As System.Object, e As System.EventArgs) Handles btnClearSearch.Click
        Try
            TextBoxSearchCode.Text = ""
            TextBoxSearchDescription.Text = ""
            moCountryDrop.SelectedIndex = BLANK_ITEM_SELECTED
            txtDateFrom.Text = String.Empty
            State.FromDateMask = String.Empty
            txtDateTo.Text = String.Empty
            State.ToDateMask = String.Empty
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

    Private Sub SaveButton_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles SaveButton_WRITE.Click
        Try
            Dim errors() As ValidationError = {New ValidationError("Country is required", GetType(ServiceLevelGroup), Nothing, "country_id", Nothing)}
            PopulateBOFromForm()
            'If ((Me.State.myBO.DealerGroupId.ToString = Guid.Empty.ToString) And (Me.State.myBO.DealerId.ToString = Guid.Empty.ToString)) Then
            '    Throw New BOValidationException(errors, GetType(BillingPlan).FullName)
            'End If
            If (State.myBO.IsDirty) Then
                State.myBO.Save()
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
            HandleErrors(ex, ErrorCtrl)
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
            HandleErrors(ex, ErrorCtrl)
        End Try

    End Sub

#End Region

#Region "Helper Functions"
    Private Sub AddNew()
        State.searchDV = GetGridDataView()

        State.myBO = New ServiceLevelGroup
        State.SelectedSLGId = State.myBO.Id

        State.searchDV = State.myBO.GetNewDataViewRow(State.searchDV, State.myBO)

        Grid.DataSource = State.searchDV

        SetPageAndSelectedIndexFromGuid(State.searchDV, State.SelectedSLGId, Grid, State.PageIndex, State.IsEditMode)

        Grid.AutoGenerateColumns = False

        SortAndBindGrid()
        SetGridControls(Grid, False)


        PopulateFormFromBO()
    End Sub

    Private Function GetGridDataView() As ServiceLevelGroup.ServiceLevelGroupSearchDV

        With State
            Return (ServiceLevelGroup.getList(TextBoxSearchCode.Text,
            TextBoxSearchDescription.Text, State.SearchCountryId, State.FromDateMask, State.ToDateMask))
        End With

    End Function

    Private Sub SetButtonsState()

        If (State.IsEditMode) Then
            ControlMgr.SetVisibleControl(Me, SaveButton_WRITE, True)
            ControlMgr.SetVisibleControl(Me, CancelButton, True)
            ControlMgr.SetVisibleControl(Me, btnAdd_WRITE, False)
            ControlMgr.SetEnableControl(Me, btnSearch, False)
            ControlMgr.SetEnableControl(Me, btnClearSearch, False)
            MenuEnabled = False
            If (cboPageSize.Visible) Then
                ControlMgr.SetEnableControl(Me, cboPageSize, False)
            End If
        Else
            ControlMgr.SetVisibleControl(Me, SaveButton_WRITE, False)
            ControlMgr.SetVisibleControl(Me, CancelButton, False)
            ControlMgr.SetVisibleControl(Me, btnAdd_WRITE, True)
            ControlMgr.SetEnableControl(Me, btnSearch, True)
            ControlMgr.SetEnableControl(Me, btnClearSearch, True)
            MenuEnabled = True
            If (cboPageSize.Visible) Then
                ControlMgr.SetEnableControl(Me, cboPageSize, True)
            End If
        End If

    End Sub


    Private Sub ReturnFromEditing()

        Grid.EditIndex = NO_ITEM_SELECTED_INDEX

        If Grid.PageCount = 0 Then
            'if returning to the "1st time in" screen
            ControlMgr.SetVisibleControl(Me, Grid, False)
        Else
            ControlMgr.SetVisibleControl(Me, Grid, True)
        End If

        SetGridControls(Grid, True)
        State.IsEditMode = False
        PopulateGrid()
        State.PageIndex = Grid.PageIndex
        SetButtonsState()

    End Sub
    Private Sub PopulateDropdown(CountryList As DropDownList)
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
            HandleErrors(ex, ErrorCtrl)
        End Try

    End Sub

    Private Sub PopulateFormFromBO()

        Dim gridRowIdx As Integer = Grid.EditIndex
        Try
            With State.myBO


                If (.Code IsNot Nothing) Then
                    CType(Grid.Rows(gridRowIdx).Cells(GRID_COL_CODE).FindControl("SLGCodeTextBox"), TextBox).Text = .Code
                End If
                If (.Description IsNot Nothing) Then
                    CType(Grid.Rows(gridRowIdx).Cells(GRID_COL_DESCRIPTION).FindControl("SLGDescTextBox"), TextBox).Text = .Description
                End If

                Dim CountryList As DropDownList = CType(Grid.Rows(gridRowIdx).Cells(GRID_COL_COUNTRY).FindControl("CountryDropdown"), DropDownList)

                PopulateDropdown(CountryList)

                SetSelectedItem(CountryList, .CountryId)


            End With
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try

    End Sub



    Private Sub PopulateBOFromForm()

        Try
            With State.myBO
                .CountryId = New Guid(CType(Grid.Rows(Grid.EditIndex).Cells(GRID_COL_COUNTRY).FindControl("CountryDropdown"), DropDownList).SelectedValue)
                .Code = CType(Grid.Rows(Grid.EditIndex).Cells(GRID_COL_CODE).FindControl("SLGCodeTextBox"), TextBox).Text
                .Description = CType(Grid.Rows(Grid.EditIndex).Cells(GRID_COL_DESCRIPTION).FindControl("SLGDescTextBox"), TextBox).Text

            End With
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try

    End Sub


#End Region








End Class
