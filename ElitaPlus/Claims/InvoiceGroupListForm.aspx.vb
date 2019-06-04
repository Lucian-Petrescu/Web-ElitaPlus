Imports System.Threading
Imports Assurant.Elita.CommonConfiguration
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

    Private Sub Page_PageReturn(ByVal ReturnFromUrl As String, ByVal ReturnPar As Object) Handles MyBase.PageReturn

        Try
            Me.MenuEnabled = True
            Me.IsReturningFromChild = True
            Dim retObj As InvoiceGroupDetailForm.ReturnType = CType(ReturnPar, InvoiceGroupDetailForm.ReturnType)
            Me.State.HasDataChanged = retObj.HasDataChanged
            If Not retObj Is Nothing AndAlso retObj.HasDataChanged Then
                Me.State.searchDV = Nothing

            End If
            Me.State.IsGridVisible = False
            Select Case retObj.LastOperation
                Case ElitaPlusPage.DetailPageCommand.Back
                    If Not retObj Is Nothing Then
                        If Not retObj.EditingBo.IsNew Then
                            Me.State.selectedInvoiceGroupId = retObj.EditingBo.Id
                            Me.State.IsGridVisible = True
                        End If

                    End If
                Case ElitaPlusPage.DetailPageCommand.Delete
                    Me.DisplayMessage(Message.DELETE_RECORD_CONFIRMATION, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
                    'Case ElitaPlusPage.DetailPageCommand.Expire
                    '    Me.DisplayMessage(Message.EXPIRE_RECORD_CONFIRMATION, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
                    Me.State.IsGridVisible = True
            End Select
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub
#End Region

#Region "Page Events"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Me.MasterPage.MessageController.Clear()
        Me.Form.DefaultButton = btnSearch.UniqueID
        Try

            ' Populate the header and bredcrumb
            Me.MasterPage.MessageController.Clear()
            Me.MasterPage.UsePageTabTitleInBreadCrum = False
            Me.MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage(CLAIMS)
            Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage(INVOICE_GROUP)
            Me.UpdateBreadCrum()


            If Not Me.IsPostBack Then

                PopulateDropdowns()
                PopulateSearchFieldsFromState()

                Me.AddCalendar_New(Me.ImageButtonDueDate, Me.txtDuedate)
                Me.AddCalendar_New(Me.ImageGroupnumfromDate, Me.txtgrpnumbfromdate)
                Me.AddCalendar_New(Me.ImageGroupnumtoDate, Me.txtgrpnumbertodate)
                If Me.State.IsGridVisible Then
                    If Not (Me.State.selectedPageSize = DEFAULT_NEW_UI_PAGE_SIZE) Then
                        cboPageSize.SelectedValue = CType(Me.State.selectedPageSize, String)
                        Grid.PageSize = Me.State.selectedPageSize
                    End If
                    If Not Me.State Is Nothing Then
                        Me.PopulateGrid()

                    End If
                End If
                Me.SetGridItemStyleColor(Me.Grid)

            End If
            Me.DisplayNewProgressBarOnClick(Me.btnSearch, LOADING_INVOICE_GROUPS)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
        Me.ShowMissingTranslations(Me.MasterPage.MessageController)

    End Sub


    Private Sub UpdateBreadCrum()

        If (Not Me.State Is Nothing) Then
            Me.MasterPage.BreadCrum = Me.MasterPage.PageTab & ElitaBase.Sperator &
                TranslationBase.TranslateLabelOrMessage(INVOICE_GROUP)
        End If

    End Sub
#End Region

#Region "Button Event Handlers"

    Private Sub btnClearSearch_Click(ByVal Sender As System.Object, ByVal e As System.EventArgs) Handles btnClearSearch.Click
        Try
            Me.ClearSearch()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try

    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            Dim begindate, enddate As Date
            If Date.TryParse(Me.txtgrpnumbfromdate.Text, begindate) AndAlso Date.TryParse(Me.txtgrpnumbertodate.Text, enddate) Then
                If begindate > enddate Then

                    Throw New GUIException(Message.MSG_INVALID_BEGIN_END_DATES_ERR, Assurant.ElitaPlus.Common.ErrorCodes.GUI_BEGIN_END_DATE_ERR)
                End If

            End If
            Me.State.PageIndex = 0
            Me.State.IsGridVisible = True
            Me.State.searchDV = Nothing
            Me.State.HasDataChanged = False
            Me.State.searchBtnClicked = True
            PopulateGrid()
            Me.State.searchBtnClicked = False
            If Not Me.State.searchDV Is Nothing Then
                Me.ValidSearchResultCountNew(Me.State.searchDV.Count, True)
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnnew_CLick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Try
            Me.callPage(InvoiceGroupDetailForm.URL)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

#End Region


#Region "Controlling Logic"

    Public Sub ClearSearch()
        Try
            Me.txtinvgroupnumber.Text = String.Empty
            Me.txtclaimnumber.Text = String.Empty
            Me.ddlcountry.SelectedIndex = Me.BLANK_ITEM_SELECTED
            Me.txtgrpnumbertodate.Text = String.Empty
            Me.txtgrpnumbfromdate.Text = String.Empty
            Me.txtMobilenumber.Text = String.Empty
            Me.txtDuedate.Text = String.Empty
            Me.txtservicecentername.Text = String.Empty
            Me.txtgrpnumbfromdate.Text = String.Empty
            Me.txtInvoiceNum.Text = String.Empty
            Me.ddlStatus.SelectedIndex = Me.BLANK_ITEM_SELECTED
            Me.txtbxmembershipnumb.Text = String.Empty
            Me.txtCertificatenumb.Text = String.Empty

        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub
    Public Sub PopulateSearchFieldsFromState()
        Me.SetSelectedItem(ddlcountry, Me.State.CountryId)
        Me.txtInvoiceNum.Text = Me.State.Invgroupnum
        Me.txtservicecentername.Text = Me.State.servicecentername
        Me.txtclaimnumber.Text = Me.State.claimnumber
        Me.txtMobilenumber.Text = Me.State.mobilenum
        Me.txtDuedate.Text = Me.State.duedate
        Me.txtgrpnumbfromdate.Text = Me.State.groupnumberfrom
        Me.txtgrpnumbertodate.Text = Me.State.groupnumberto
        Me.txtInvoiceNum.Text = Me.State.Invoicenum
        Me.SetSelectedItem(ddlStatus, Me.State.InvoicestatusId)
        Me.txtbxmembershipnumb.Text = Me.State.membershipnum
        Me.txtCertificatenumb.Text = Me.State.certnumber
    End Sub
    Public Sub PopulateStateFromSearchFields()
        Me.State.CountryId = Me.GetSelectedItem(ddlcountry)
        Me.State.Invgroupnum = Me.txtinvgroupnumber.Text.ToUpper.Trim
        Me.State.servicecentername = Me.txtservicecentername.Text.ToUpper
        Me.State.claimnumber = Me.txtclaimnumber.Text.ToUpper.Trim
        Me.State.mobilenum = Me.txtMobilenumber.Text.ToUpper.Trim
        Me.State.duedate = Me.txtDuedate.Text
        Me.State.groupnumberfrom = Me.txtgrpnumbfromdate.Text
        Me.State.groupnumberto = Me.txtgrpnumbertodate.Text
        Me.State.Invoicenum = Me.txtInvoiceNum.Text.ToUpper.Trim
        Me.State.InvoicestatusId = Me.GetSelectedItem(ddlStatus)
        Me.State.membershipnum = Me.txtbxmembershipnumb.Text.ToUpper.Trim
        Me.State.certnumber = Me.txtCertificatenumb.Text.ToUpper.Trim

    End Sub
    Public Sub PopulateGrid()
        Try
            PopulateStateFromSearchFields()
            If ((Me.State.searchDV Is Nothing) OrElse (Me.State.HasDataChanged)) Then

                Me.State.searchDV = InvoiceGroup.getList(Me.State.Invgroupnum, Me.State.claimnumber,
                                                         Me.State.CountryId, Me.State.groupnumberfrom,
                                                         Me.State.mobilenum, Me.State.duedate,
                                                         Me.State.servicecentername, Me.State.groupnumberto,
                                                         Me.State.Invoicenum, Me.State.InvoicestatusId,
                                                         Me.State.membershipnum, Me.State.certnumber, Me.State.searchBtnClicked)
            End If

            Me.Grid.PageSize = Me.State.selectedPageSize
            If Not (Me.State.searchDV Is Nothing) Then

                If Me.State.searchBtnClicked Then
                    Me.State.SortExpression = InvoiceGroup.InvoiceGroupSearchDV.COL_INVOICE_GROUP_NUMBER
                    Me.State.SortExpression &= " DESC"

                    Me.State.searchDV.Sort = Me.State.SortExpression

                Else
                    Me.State.searchDV.Sort = Me.State.SortExpression
                End If
                Me.Grid.Columns(Me.GRID_COL_Group_NUMBER_IDX).SortExpression = InvoiceGroup.InvoiceGroupSearchDV.COL_INVOICE_GROUP_NUMBER
                Me.Grid.Columns(Me.GRID_COL_SERVICE_CENTER_IDX).SortExpression = InvoiceGroup.InvoiceGroupSearchDV.COL_SERVICE_CENTER_NAME
                Me.Grid.Columns(Me.GRID_COL_GROUP_TIMESTAMP_IDX).SortExpression = InvoiceGroup.InvoiceGroupSearchDV.COL_INVOICE_GROUP_CREATED_DATE
                Me.Grid.Columns(Me.GRID_COL_USER_IDX).SortExpression = InvoiceGroup.InvoiceGroupSearchDV.COL_INVOICE_GROUP_USER

                SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.selectedInvoiceGroupId, Me.Grid, Me.State.PageIndex)
                Me.SortAndBindGrid()
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub SortAndBindGrid()
        Try
            Me.State.PageIndex = Me.Grid.CurrentPageIndex
            Me.Grid.DataSource = Me.State.searchDV
            HighLightSortColumn(Grid, Me.State.SortExpression, Me.IsNewUI)
            Me.Grid.DataBind()



            Session("recCount") = Me.State.searchDV.Count

            If Me.State.searchDV.Count > 0 Then

                ControlMgr.SetVisibleControl(Me, Grid, True)
                ControlMgr.SetVisibleControl(Me, SearchResults, True)

                If Me.Grid.Visible Then
                    Me.lblRecordCount.Text = Me.State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                End If
            Else

                ControlMgr.SetVisibleControl(Me, Grid, False)
                ControlMgr.SetVisibleControl(Me, SearchResults, False)
                Me.lblRecordCount.Text = Me.State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)

            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Public Sub PopulateDropdowns()
        Try
            'Dim langID As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
            'Me.BindListControlToDataView(Me.ddlcountry, LookupListNew.GetUserCountriesLookupList)
            'Me.BindListControlToDataView(Me.ddlStatus, LookupListNew.DropdownLookupList(LookupListNew.LK_INVOICE_STATUS, langID))
            Dim CountryList As DataElements.ListItem() =
                                CommonConfigManager.Current.ListManager.GetList(listCode:=ListCodes.Country)
            Dim UserCountries As DataElements.ListItem() = (From Country In CountryList
                                                            Where ElitaPlusIdentity.Current.ActiveUser.Countries.Contains(Country.ListItemId)
                                                            Select Country).ToArray()
            Me.ddlcountry.Populate(UserCountries.ToArray(),
                            New PopulateOptions() With
                            {
                                .AddBlankItem = True
                            })

            Dim StatusList As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="INV_STAT",
                                                                                                        languageCode:=Thread.CurrentPrincipal.GetLanguageCode())
            Me.ddlStatus.Populate(StatusList.ToArray(),
                                    New PopulateOptions() With
                                    {
                                        .AddBlankItem = True
                                    })
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub PopulateInvoiceStatus()
        Try
            'Dim langID As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
            'Me.BindListControlToDataView(Me.ddlStatus, LookupListNew.DropdownLookupList(LookupListNew.LK_INVOICE_STATUS, langID))
            Dim StatusList As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="INV_STAT",
                                                                                                        languageCode:=Thread.CurrentPrincipal.GetLanguageCode())
            Me.ddlStatus.Populate(StatusList.ToArray(),
                                    New PopulateOptions() With
                                    {
                                        .AddBlankItem = True
                                    })
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub
#End Region

#Region "DataGrid Related"
    Private Sub Grid_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles Grid.ItemDataBound
        Try
            Dim itemType As ListItemType = CType(e.Item.ItemType, ListItemType)
            Dim dvRow As DataRowView = CType(e.Item.DataItem, DataRowView)
            Dim btnEditButtonCode As LinkButton

            If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then

                If (Not e.Item.Cells(Me.GRID_COL_Group_NUMBER_IDX).FindControl(Me.GRID_COL_Group_Number_CTRL) Is Nothing) Then
                    btnEditButtonCode = CType(e.Item.Cells(Me.GRID_COL_Group_NUMBER_IDX).FindControl(Me.GRID_COL_Group_Number_CTRL), LinkButton)
                    btnEditButtonCode.Text = dvRow(InvoiceGroup.InvoiceGroupSearchDV.COL_INVOICE_GROUP_NUMBER).ToString
                    btnEditButtonCode.CommandArgument = GetGuidStringFromByteArray(CType(dvRow(InvoiceGroup.InvoiceGroupSearchDV.COL_INVOICE_GROUP_ID), Byte()))
                    btnEditButtonCode.CommandName = SELECT_ACTION_COMMAND
                End If

                e.Item.Cells(Me.GRID_COL_INVOICE_GROUP_ID_IDX).Text = GetGuidStringFromByteArray(CType(dvRow(InvoiceGroup.InvoiceGroupSearchDV.COL_INVOICE_GROUP_ID), Byte()))
                e.Item.Cells(Me.GRID_COL_SERVICE_CENTER_IDX).Text = dvRow(InvoiceGroup.InvoiceGroupSearchDV.COL_SERVICE_CENTER_NAME).ToString
                e.Item.Cells(Me.GRID_COL_GROUP_TIMESTAMP_IDX).Text = dvRow(InvoiceGroup.InvoiceGroupSearchDV.COL_INVOICE_GROUP_CREATED_DATE).ToString
                e.Item.Cells(Me.GRID_COL_USER_IDX).Text = dvRow(InvoiceGroup.InvoiceGroupSearchDV.COL_INVOICE_GROUP_USER).ToString

            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Public Sub ItemCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles Grid.ItemCommand
        Try
            Dim recievedate As Date
            If e.CommandName = "SelectAction" Then


                Me.State.selectedInvoiceGroupId = New Guid(e.Item.Cells(Me.GRID_COL_INVOICE_GROUP_ID_IDX).Text)

                Me.callPage(InvoiceGroupDetailForm.URL, Me.State.selectedInvoiceGroupId)
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try

    End Sub

    Public Sub ItemCreated(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles Grid.ItemCreated
        BaseItemCreated(sender, e)
    End Sub

    Private Sub Grid_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles Grid.PageIndexChanged
        Try
            Me.State.PageIndex = e.NewPageIndex
            Me.State.selectedInvoiceGroupId = Guid.Empty
            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub


    Private Sub Grid_PageSizeChanged(ByVal source As Object, ByVal e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
        Try
            Grid.CurrentPageIndex = NewCurrentPageIndex(Grid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
            Me.State.selectedPageSize = CType(cboPageSize.SelectedValue, Integer)
            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Grid_SortCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles Grid.SortCommand
        Try

            If Me.State.SortExpression.StartsWith(e.SortExpression) Then
                If Me.State.SortExpression.EndsWith("DESC") Then
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
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub
#End Region

End Class
