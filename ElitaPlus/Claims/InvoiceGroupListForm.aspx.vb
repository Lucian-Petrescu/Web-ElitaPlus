Imports System.Threading
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms
Public Class InvoiceGroupListForm
    Inherits ElitaPlusSearchPage

#Region "Constants"
    'Public Const GRID_COL_EDIT_IDX As Integer = 0
    Public Const GRID_COL_Group_NUMBER_IDX As Integer = 0
    Public Const GRID_COL_SERVICE_CENTER_IDX As Integer = 1
    Public Const GRID_COL_GROUP_TIMESTAMP_IDX As Integer = 2
    Public Const GRID_COL_USER_IDX As Integer = 3
    Public Const GRID_COL_INVOICE_GROUP_ID_IDX As Integer = 4

    Public Const GRID_TOTAL_COLUMNS As Integer = 5
    Public Const GRID_COL_Group_Number_CTRL As String = "btnEditCode"
    Public Const SELECT_ACTION_COMMAND As String = "SelectAction"
    Public Const CLAIMS As String = "Claims"
    Public Const INVOICE_GROUP As String = "INVOICE_GROUP"
    Public Const LOADING_INVOICE_GROUPS As String = "LOADING_INVOICE_GROUPS"


#End Region


#Region "Page State"
    Private IsReturningFromChild As Boolean = False
    Class MyState
        Public PageIndex As Integer = 0
        Public SortExpression As String = InvoiceGroup.InvoiceGroupSearchDV.COL_INVOICE_GROUP_NUMBER
        Public selectedInvoiceGroupId As Guid = Guid.Empty
        Public CountryId As Guid
        Public Invgroupnum As String
        Public claimnumber As String
        Public servicecentername As String
        Public groupnumberfrom As String
        Public groupnumberto As String
        Public mobilenum As String
        Public duedate As String
        Public Invoicenum As String
        Public InvoicestatusId As Guid
        Public Invoicestatus As String
        Public membershipnum As String
        Public grpreceveddate As String
        Public certnumber As String
        Public selectedPageSize As Integer = DEFAULT_NEW_UI_PAGE_SIZE
        Public IsGridVisible As Boolean
        Public searchDV As InvoiceGroup.InvoiceGroupSearchDV = Nothing
        Public HasDataChanged As Boolean
        Public searchBtnClicked As Boolean = False

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
            Dim retObj As InvoiceGroupDetailForm.ReturnType = CType(ReturnPar, InvoiceGroupDetailForm.ReturnType)
            State.HasDataChanged = retObj.HasDataChanged
            If retObj IsNot Nothing AndAlso retObj.HasDataChanged Then
                State.searchDV = Nothing

            End If
            State.IsGridVisible = False
            Select Case retObj.LastOperation
                Case DetailPageCommand.Back
                    If retObj IsNot Nothing Then
                        If Not retObj.EditingBo.IsNew Then
                            State.selectedInvoiceGroupId = retObj.EditingBo.Id
                            State.IsGridVisible = True
                        End If

                    End If
                Case DetailPageCommand.Delete
                    DisplayMessage(Message.DELETE_RECORD_CONFIRMATION, "", MSG_BTN_OK, MSG_TYPE_INFO)
                    'Case ElitaPlusPage.DetailPageCommand.Expire
                    '    Me.DisplayMessage(Message.EXPIRE_RECORD_CONFIRMATION, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
                    State.IsGridVisible = True
            End Select
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
#End Region

#Region "Page Events"
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        MasterPage.MessageController.Clear()
        Form.DefaultButton = btnSearch.UniqueID
        Try

            ' Populate the header and bredcrumb
            MasterPage.MessageController.Clear()
            MasterPage.UsePageTabTitleInBreadCrum = False
            MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage(CLAIMS)
            MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage(INVOICE_GROUP)
            UpdateBreadCrum()


            If Not IsPostBack Then

                PopulateDropdowns()
                PopulateSearchFieldsFromState()

                AddCalendar_New(ImageButtonDueDate, txtDuedate)
                AddCalendar_New(ImageGroupnumfromDate, txtgrpnumbfromdate)
                AddCalendar_New(ImageGroupnumtoDate, txtgrpnumbertodate)
                If State.IsGridVisible Then
                    If Not (State.selectedPageSize = DEFAULT_NEW_UI_PAGE_SIZE) Then
                        cboPageSize.SelectedValue = CType(State.selectedPageSize, String)
                        Grid.PageSize = State.selectedPageSize
                    End If
                    If State IsNot Nothing Then
                        PopulateGrid()

                    End If
                End If
                SetGridItemStyleColor(Grid)

            End If
            DisplayNewProgressBarOnClick(btnSearch, LOADING_INVOICE_GROUPS)
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
        ShowMissingTranslations(MasterPage.MessageController)

    End Sub


    Private Sub UpdateBreadCrum()

        If (State IsNot Nothing) Then
            MasterPage.BreadCrum = MasterPage.PageTab & ElitaBase.Sperator &
                TranslationBase.TranslateLabelOrMessage(INVOICE_GROUP)
        End If

    End Sub
#End Region

#Region "Button Event Handlers"

    Private Sub btnClearSearch_Click(Sender As Object, e As EventArgs) Handles btnClearSearch.Click
        Try
            ClearSearch()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try

    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Try
            Dim begindate, enddate As Date
            If Date.TryParse(txtgrpnumbfromdate.Text, begindate) AndAlso Date.TryParse(txtgrpnumbertodate.Text, enddate) Then
                If begindate > enddate Then

                    Throw New GUIException(Message.MSG_INVALID_BEGIN_END_DATES_ERR, Assurant.ElitaPlus.Common.ErrorCodes.GUI_BEGIN_END_DATE_ERR)
                End If

            End If
            State.PageIndex = 0
            State.IsGridVisible = True
            State.searchDV = Nothing
            State.HasDataChanged = False
            State.searchBtnClicked = True
            PopulateGrid()
            State.searchBtnClicked = False
            If State.searchDV IsNot Nothing Then
                ValidSearchResultCountNew(State.searchDV.Count, True)
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnnew_CLick(sender As Object, e As EventArgs) Handles btnNew.Click
        Try
            callPage(InvoiceGroupDetailForm.URL)
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

#End Region


#Region "Controlling Logic"

    Public Sub ClearSearch()
        Try
            txtinvgroupnumber.Text = String.Empty
            txtclaimnumber.Text = String.Empty
            ddlcountry.SelectedIndex = BLANK_ITEM_SELECTED
            txtgrpnumbertodate.Text = String.Empty
            txtgrpnumbfromdate.Text = String.Empty
            txtMobilenumber.Text = String.Empty
            txtDuedate.Text = String.Empty
            txtservicecentername.Text = String.Empty
            txtgrpnumbfromdate.Text = String.Empty
            txtInvoiceNum.Text = String.Empty
            ddlStatus.SelectedIndex = BLANK_ITEM_SELECTED
            txtbxmembershipnumb.Text = String.Empty
            txtCertificatenumb.Text = String.Empty

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
    Public Sub PopulateSearchFieldsFromState()
        SetSelectedItem(ddlcountry, State.CountryId)
        txtInvoiceNum.Text = State.Invgroupnum
        txtservicecentername.Text = State.servicecentername
        txtclaimnumber.Text = State.claimnumber
        txtMobilenumber.Text = State.mobilenum
        txtDuedate.Text = State.duedate
        txtgrpnumbfromdate.Text = State.groupnumberfrom
        txtgrpnumbertodate.Text = State.groupnumberto
        txtInvoiceNum.Text = State.Invoicenum
        SetSelectedItem(ddlStatus, State.InvoicestatusId)
        txtbxmembershipnumb.Text = State.membershipnum
        txtCertificatenumb.Text = State.certnumber
    End Sub
    Public Sub PopulateStateFromSearchFields()
        State.CountryId = GetSelectedItem(ddlcountry)
        State.Invgroupnum = txtinvgroupnumber.Text.ToUpper.Trim
        State.servicecentername = txtservicecentername.Text.ToUpper
        State.claimnumber = txtclaimnumber.Text.ToUpper.Trim
        State.mobilenum = txtMobilenumber.Text.ToUpper.Trim
        State.duedate = txtDuedate.Text
        State.groupnumberfrom = txtgrpnumbfromdate.Text
        State.groupnumberto = txtgrpnumbertodate.Text
        State.Invoicenum = txtInvoiceNum.Text.ToUpper.Trim
        State.InvoicestatusId = GetSelectedItem(ddlStatus)
        State.membershipnum = txtbxmembershipnumb.Text.ToUpper.Trim
        State.certnumber = txtCertificatenumb.Text.ToUpper.Trim

    End Sub
    Public Sub PopulateGrid()
        Try
            PopulateStateFromSearchFields()
            If ((State.searchDV Is Nothing) OrElse (State.HasDataChanged)) Then

                State.searchDV = InvoiceGroup.getList(State.Invgroupnum, State.claimnumber,
                                                         State.CountryId, State.groupnumberfrom,
                                                         State.mobilenum, State.duedate,
                                                         State.servicecentername, State.groupnumberto,
                                                         State.Invoicenum, State.InvoicestatusId,
                                                         State.membershipnum, State.certnumber, State.searchBtnClicked)
            End If

            Grid.PageSize = State.selectedPageSize
            If Not (State.searchDV Is Nothing) Then

                If State.searchBtnClicked Then
                    State.SortExpression = InvoiceGroup.InvoiceGroupSearchDV.COL_INVOICE_GROUP_NUMBER
                    State.SortExpression &= " DESC"

                    State.searchDV.Sort = State.SortExpression

                Else
                    State.searchDV.Sort = State.SortExpression
                End If
                Grid.Columns(GRID_COL_Group_NUMBER_IDX).SortExpression = InvoiceGroup.InvoiceGroupSearchDV.COL_INVOICE_GROUP_NUMBER
                Grid.Columns(GRID_COL_SERVICE_CENTER_IDX).SortExpression = InvoiceGroup.InvoiceGroupSearchDV.COL_SERVICE_CENTER_NAME
                Grid.Columns(GRID_COL_GROUP_TIMESTAMP_IDX).SortExpression = InvoiceGroup.InvoiceGroupSearchDV.COL_INVOICE_GROUP_CREATED_DATE
                Grid.Columns(GRID_COL_USER_IDX).SortExpression = InvoiceGroup.InvoiceGroupSearchDV.COL_INVOICE_GROUP_USER

                SetPageAndSelectedIndexFromGuid(State.searchDV, State.selectedInvoiceGroupId, Grid, State.PageIndex)
                SortAndBindGrid()
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub SortAndBindGrid()
        Try
            State.PageIndex = Grid.CurrentPageIndex
            Grid.DataSource = State.searchDV
            HighLightSortColumn(Grid, State.SortExpression, IsNewUI)
            Grid.DataBind()



            Session("recCount") = State.searchDV.Count

            If State.searchDV.Count > 0 Then

                ControlMgr.SetVisibleControl(Me, Grid, True)
                ControlMgr.SetVisibleControl(Me, SearchResults, True)

                If Grid.Visible Then
                    lblRecordCount.Text = State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                End If
            Else

                ControlMgr.SetVisibleControl(Me, Grid, False)
                ControlMgr.SetVisibleControl(Me, SearchResults, False)
                lblRecordCount.Text = State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)

            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Public Sub PopulateDropdowns()
        Try
            'Dim langID As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
            'Me.BindListControlToDataView(Me.ddlcountry, LookupListNew.GetUserCountriesLookupList)
            'Me.BindListControlToDataView(Me.ddlStatus, LookupListNew.DropdownLookupList(LookupListNew.LK_INVOICE_STATUS, langID))
            Dim CountryList As ListItem() =
                                CommonConfigManager.Current.ListManager.GetList(listCode:=ListCodes.Country)
            Dim UserCountries As ListItem() = (From Country In CountryList
                                                            Where ElitaPlusIdentity.Current.ActiveUser.Countries.Contains(Country.ListItemId)
                                                            Select Country).ToArray()
            ddlcountry.Populate(UserCountries.ToArray(),
                            New PopulateOptions() With
                            {
                                .AddBlankItem = True
                            })

            Dim StatusList As ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="INV_STAT",
                                                                                                        languageCode:=Thread.CurrentPrincipal.GetLanguageCode())
            ddlStatus.Populate(StatusList.ToArray(),
                                    New PopulateOptions() With
                                    {
                                        .AddBlankItem = True
                                    })
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub PopulateInvoiceStatus()
        Try
            'Dim langID As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
            'Me.BindListControlToDataView(Me.ddlStatus, LookupListNew.DropdownLookupList(LookupListNew.LK_INVOICE_STATUS, langID))
            Dim StatusList As ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="INV_STAT",
                                                                                                        languageCode:=Thread.CurrentPrincipal.GetLanguageCode())
            ddlStatus.Populate(StatusList.ToArray(),
                                    New PopulateOptions() With
                                    {
                                        .AddBlankItem = True
                                    })
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
#End Region

#Region "DataGrid Related"
    Private Sub Grid_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles Grid.ItemDataBound
        Try
            Dim itemType As ListItemType = CType(e.Item.ItemType, ListItemType)
            Dim dvRow As DataRowView = CType(e.Item.DataItem, DataRowView)
            Dim btnEditButtonCode As LinkButton

            If itemType = ListItemType.Item OrElse itemType = ListItemType.AlternatingItem OrElse itemType = ListItemType.SelectedItem Then

                If (e.Item.Cells(GRID_COL_Group_NUMBER_IDX).FindControl(GRID_COL_Group_Number_CTRL) IsNot Nothing) Then
                    btnEditButtonCode = CType(e.Item.Cells(GRID_COL_Group_NUMBER_IDX).FindControl(GRID_COL_Group_Number_CTRL), LinkButton)
                    btnEditButtonCode.Text = dvRow(InvoiceGroup.InvoiceGroupSearchDV.COL_INVOICE_GROUP_NUMBER).ToString
                    btnEditButtonCode.CommandArgument = GetGuidStringFromByteArray(CType(dvRow(InvoiceGroup.InvoiceGroupSearchDV.COL_INVOICE_GROUP_ID), Byte()))
                    btnEditButtonCode.CommandName = SELECT_ACTION_COMMAND
                End If

                e.Item.Cells(GRID_COL_INVOICE_GROUP_ID_IDX).Text = GetGuidStringFromByteArray(CType(dvRow(InvoiceGroup.InvoiceGroupSearchDV.COL_INVOICE_GROUP_ID), Byte()))
                e.Item.Cells(GRID_COL_SERVICE_CENTER_IDX).Text = dvRow(InvoiceGroup.InvoiceGroupSearchDV.COL_SERVICE_CENTER_NAME).ToString
                e.Item.Cells(GRID_COL_GROUP_TIMESTAMP_IDX).Text = dvRow(InvoiceGroup.InvoiceGroupSearchDV.COL_INVOICE_GROUP_CREATED_DATE).ToString
                e.Item.Cells(GRID_COL_USER_IDX).Text = dvRow(InvoiceGroup.InvoiceGroupSearchDV.COL_INVOICE_GROUP_USER).ToString

            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Public Sub ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles Grid.ItemCommand
        Try
            Dim recievedate As Date
            If e.CommandName = "SelectAction" Then


                State.selectedInvoiceGroupId = New Guid(e.Item.Cells(GRID_COL_INVOICE_GROUP_ID_IDX).Text)

                callPage(InvoiceGroupDetailForm.URL, State.selectedInvoiceGroupId)
            End If
        Catch ex As ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try

    End Sub

    Public Sub ItemCreated(sender As Object, e As DataGridItemEventArgs) Handles Grid.ItemCreated
        BaseItemCreated(sender, e)
    End Sub

    Private Sub Grid_PageIndexChanged(source As Object, e As DataGridPageChangedEventArgs) Handles Grid.PageIndexChanged
        Try
            State.PageIndex = e.NewPageIndex
            State.selectedInvoiceGroupId = Guid.Empty
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub


    Private Sub Grid_PageSizeChanged(source As Object, e As EventArgs) Handles cboPageSize.SelectedIndexChanged
        Try
            Grid.CurrentPageIndex = NewCurrentPageIndex(Grid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
            State.selectedPageSize = CType(cboPageSize.SelectedValue, Integer)
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Grid_SortCommand(source As Object, e As DataGridSortCommandEventArgs) Handles Grid.SortCommand
        Try

            If State.SortExpression.StartsWith(e.SortExpression) Then
                If State.SortExpression.EndsWith("DESC") Then
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
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
#End Region

End Class
