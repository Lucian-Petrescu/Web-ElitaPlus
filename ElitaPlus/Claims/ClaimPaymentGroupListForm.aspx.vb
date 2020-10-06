Imports System.Web.Services
Imports System.Web.Script.Services
Imports System.Collections.Generic
Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports Assurant.Elita.Web.Forms
Imports System.Threading
<ScriptService()> _
Partial Public Class ClaimPaymentGroupListForm
    Inherits ElitaPlusSearchPage

#Region "Constants"
    Public Const SELECT_ACTION_COMMAND As String = "SelectAction"
    Public Const GRID_COL_PMNT_GRP_NUMBER_CTRL As String = "btnEditPaymentGroup"
    Public Const GRID_COL_PMNT_GRP_STATUS_CTRL As String = "lblPymntGroupStatus"

    Public Const GRID_COL_NAME_PAYMENT_GROUP_ID As Integer = 0
    Public Const GRID_COL_NAME_PAYMENT_GROUP_NUMBER As Integer = 1
    Public Const GRID_COL_NAME_PAYMENT_GROUP_DATE As Integer = 2
    Public Const GRID_COL_NAME_PAYMENT_GROUP_STATUS As Integer = 3
    Public Const GRID_COL_NAME_PAYMENT_GROUP_TOTAL As Integer = 4

#End Region


#Region "Page State"
    Private IsReturningFromChild As Boolean = False

    Class MyState
        Public PageIndex As Integer = 0
        Public PageSize As Integer = DEFAULT_NEW_UI_PAGE_SIZE
        Public searchDV As ClaimPaymentGroup.PaymentGroupSearchDV
        Public SearchClicked As Boolean = False
        Public IsGridVisible As Boolean = False
        Public CountryIdInSearch As Guid = Guid.Empty
        Public ServiceCenterIdInSearch As Guid = Guid.Empty
        Public selectedPaymentGroupId As Guid = Guid.Empty
        Public PymntGrpNumberInSearch As String = String.Empty
        Public PymntGrpStatusInSearch As Guid = Guid.Empty
        Public InvoiceNumberInSearch As String = String.Empty
        Public InvoiceGrpNumberInSearch As String = String.Empty
        Public PymntGrpTotalInSearch As DecimalType = Nothing
        'Public PymntGrpFromDateInSearch As DateType = Nothing
        'Public PymntGrpToDateInSearch As DateType = Nothing
        Public PaymentGroupDateRange As Global.Assurant.ElitaPlus.ElitaPlusWebApp.FieldSearchCriteriaControl

        Public SortExpression As String = ClaimPaymentGroup.PaymentGroupSearchDV.COL_NAME_PAYMENT_GROUP_DATE & " DESC"
        'Public SortExpression As String = String.Empty
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

#End Region

#Region "Page_Events"

    Private Sub Page_PageReturn(ReturnFromUrl As String, ReturnPar As Object) Handles MyBase.PageReturn
        Try
            'Me.MenuEnabled = True
            IsReturningFromChild = True
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub UpdateBreadCrum()
        If (State IsNot Nothing) Then
            If (State IsNot Nothing) Then
                MasterPage.BreadCrum = MasterPage.PageTab & ElitaBase.Sperator & _
                    TranslationBase.TranslateLabelOrMessage("PAYMENT_GROUP_SEARCH")
                MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("PAYMENT_GROUP_SEARCH")
            End If
        End If
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        MasterPage.MessageController.Clear()
        Form.DefaultButton = btnSearch.UniqueID
        Try
            If Not IsPostBack Then
                
                MasterPage.MessageController.Clear()
                MasterPage.UsePageTabTitleInBreadCrum = False
                MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage("CLAIM")
                UpdateBreadCrum()
                TranslateGridHeader(Grid)
                divDataContainer.Visible = False
                cboPageSize.SelectedValue = CType(State.PageSize, String)
                PopulateDropdowns()

                If IsReturningFromChild Then
                    ' It is returning from detail
                    PopulateSearchFieldsFromState()
                    If Not isSearchClear() Then
                        State.searchDV = Nothing
                        PopulateGrid()
                    End If
                Else
                    ClearSearch()
                End If
            End If
            DisplayNewProgressBarOnClick(btnSearch, "LOADING_PAYMENT_GROUPS")
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
        'Me.ShowMissingTranslations(Me.MasterPage.MessageController)
    End Sub

#End Region

    Protected Sub PopulateDropdowns()
        'Dim oUser As New User(ElitaPlusIdentity.Current.ActiveUser.NetworkId)
        'Dim userCountriesDv As DataView = oUser.GetUserCountries(ElitaPlusIdentity.Current.ActiveUser.Id)
        'Me.BindListControlToDataView(Me.ddlCountryDropDown, userCountriesDv, , "COUNTRY_ID")
        'Me.BindListControlToDataView(PmntGrpStatusDropDown, LookupListNew.GetPaymentStatusLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId))

        Dim countryList As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="Country")

        Dim filteredCountryList As DataElements.ListItem() = (From x In countryList
                                                              Where ElitaPlusIdentity.Current.ActiveUser.Countries.Contains(x.ListItemId)
                                                              Select x).ToArray()

        ddlCountryDropDown.Populate(filteredCountryList, New PopulateOptions() With
                                                   {
                                                    .AddBlankItem = True,
                                                    .SortFunc = AddressOf .GetDescription
                                                   })

        Dim paymentStatusList As ListItem() = CommonConfigManager.Current.ListManager.GetList("PMTGRPSTAT", Thread.CurrentPrincipal.GetLanguageCode())
        PmntGrpStatusDropDown.Populate(paymentStatusList, New PopulateOptions() With
                                  {
                                    .AddBlankItem = True,
                                    .SortFunc = AddressOf .GetDescription
                                   })
    End Sub

    Public Sub SetSvcCentersForCountry(sender As Object, e As System.EventArgs) Handles ddlCountryDropDown.SelectedIndexChanged
        Dim oUser As New User(ElitaPlusIdentity.Current.ActiveUser.NetworkId)
        Dim oSvcCenter As New ServiceCenter
        Dim userCountriesDv As DataView = oUser.GetUserCountries(ElitaPlusIdentity.Current.ActiveUser.Id)
        'Dim contrySvcCenters As DataView
        Dim selectedCountryItem As Guid = Guid.Empty
        'Reset the Service Centre dropdown value to Nothing Selected
        ddlSvcCenterDropDown.SelectedIndex = NO_ITEM_SELECTED_INDEX

        'Get the selected Country
        selectedCountryItem = GetSelectedItem(ddlCountryDropDown)
        'Pull the list of service center as per the selected country and bind that to the Service Center dropdown.
        'contrySvcCenters = oSvcCenter.GetServiceCenterForCountry(selectedCountryItem)
        'Me.BindListControlToDataView(Me.ddlSvcCenterDropDown, contrySvcCenters, , "SERVICE_CENTER_ID")

        Dim lstcontext As ListContext = New ListContext()
        lstcontext.CountryId = selectedCountryItem
        Dim svcCenterList As ListItem() = CommonConfigManager.Current.ListManager.GetList("ServiceCenterListByCountry", Thread.CurrentPrincipal.GetLanguageCode(), lstcontext)

        ddlSvcCenterDropDown.Populate(svcCenterList, New PopulateOptions() With
                                                 {
                                                   .AddBlankItem = False
                                                  })

        ' Clear the Search parameters at the change of country
        ClearSearch()
    End Sub



    Private Sub ddlSvcCenterDropDown_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlSvcCenterDropDown.SelectedIndexChanged
        'Get the selected Service Center
        State.ServiceCenterIdInSearch = GetSelectedItem(ddlSvcCenterDropDown)

    End Sub

    Public Function PopulateStateFromSearchFields() As Boolean
        Try
            State.CountryIdInSearch = GetSelectedItem(ddlCountryDropDown)
            State.ServiceCenterIdInSearch = GetSelectedItem(ddlSvcCenterDropDown)
            State.PymntGrpStatusInSearch = GetSelectedItem(PmntGrpStatusDropDown)
            State.PymntGrpNumberInSearch = moPaymentGroupNumber.Text
            State.InvoiceGrpNumberInSearch = moInvoiceGroupNumber.Text
            State.InvoiceNumberInSearch = moInvoiceNumber.Text

            State.PaymentGroupDateRange = moPaymentGroupDateRange

            Return True

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Function

    Public Sub PopulateSearchFieldsFromState()
        'Dim contrySvcCenters As DataView
        Try
            moPaymentGroupNumber.Text = State.PymntGrpNumberInSearch
            moInvoiceGroupNumber.Text = State.InvoiceGrpNumberInSearch
            moInvoiceNumber.Text = State.InvoiceNumberInSearch

            If Not State.CountryIdInSearch = Guid.Empty Then
                'Set the Country based on the selected Country Id
                SetSelectedItem(ddlCountryDropDown, State.CountryIdInSearch)
                'Populate the Service Center based on the selected Country/Country in State
                'Dim oSvcCenter As New ServiceCenter
                'contrySvcCenters = oSvcCenter.GetServiceCenterForCountry(Me.State.CountryIdInSearch)
                'Me.BindListControlToDataView(Me.ddlSvcCenterDropDown, contrySvcCenters, , "SERVICE_CENTER_ID")

                Dim lstcontext As ListContext = New ListContext()
                lstcontext.CountryId = State.CountryIdInSearch
                Dim svcCenterList As ListItem() = CommonConfigManager.Current.ListManager.GetList("ServiceCenterListByCountry", Thread.CurrentPrincipal.GetLanguageCode(), lstcontext)

                ddlSvcCenterDropDown.Populate(svcCenterList, New PopulateOptions() With
                                                 {
                                                   .AddBlankItem = True,
                                                   .SortFunc = AddressOf .GetDescription
                                                  })

                'Set the Service Center based on the selected Service Center
                If Not State.ServiceCenterIdInSearch = Guid.Empty Then
                    SetSelectedItem(ddlSvcCenterDropDown, State.ServiceCenterIdInSearch)
                End If
            End If

            If Not State.PymntGrpStatusInSearch = Guid.Empty Then
                SetSelectedItem(PmntGrpStatusDropDown, State.PymntGrpStatusInSearch)
            End If

            moPaymentGroupDateRange = State.PaymentGroupDateRange

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub PopulateGrid()
        Try

            If Not PopulateStateFromSearchFields() Then Exit Sub

            If (State.searchDV Is Nothing) Then
                State.searchDV = ClaimPaymentGroup.GetPaymentGroup(State.ServiceCenterIdInSearch, State.PymntGrpNumberInSearch, _
                                                                      State.PymntGrpStatusInSearch, _
                                                                      DirectCast(State.PaymentGroupDateRange.Value, SearchCriteriaStructType(Of Date)), _
                                                                      State.InvoiceNumberInSearch, _
                                                                      State.InvoiceGrpNumberInSearch)
                If State.SearchClicked Then
                    ValidSearchResultCountNew(State.searchDV.Count, True)
                    State.SearchClicked = False
                End If
            End If

            Grid.PageSize = State.PageSize
            If State.searchDV.Count = 0 Then
            Else
                Grid.DataSource = State.searchDV
            End If
            State.PageIndex = Grid.PageIndex
            divDataContainer.Visible = True

            Grid.Columns(GRID_COL_NAME_PAYMENT_GROUP_ID).Visible = False
            Grid.Columns(GRID_COL_NAME_PAYMENT_GROUP_NUMBER).Visible = True
            Grid.Columns(GRID_COL_NAME_PAYMENT_GROUP_DATE).SortExpression = ClaimPaymentGroup.PaymentGroupSearchDV.COL_NAME_PAYMENT_GROUP_DATE
            Grid.Columns(GRID_COL_NAME_PAYMENT_GROUP_STATUS).Visible = True
            Grid.Columns(GRID_COL_NAME_PAYMENT_GROUP_TOTAL).SortExpression = ClaimPaymentGroup.PaymentGroupSearchDV.COL_NAME_PAYMENT_GROUP_TOTAL

            If (Not State.SortExpression.Equals(String.Empty)) Then
                State.searchDV.Sort = State.SortExpression 'Me.SortDirection
            End If

            HighLightSortColumn(Grid, State.SortExpression, True) 'Me.SortDirection
            Grid.DataBind()

            ControlMgr.SetVisibleControl(Me, Grid, True)
            Session("recCount") = State.searchDV.Count

            If Grid.Visible Then
                lblSearchResults.Visible = True
                lblSearchResults.Text = State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
            End If

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

#Region " Button Clicks "

    Private Function isSearchClear() As Boolean

        If (moInvoiceGroupNumber.Text.Trim().Equals(String.Empty) AndAlso moInvoiceNumber.Text.Trim().Equals(String.Empty) AndAlso _
               moPaymentGroupNumber.Text.Trim().Equals(String.Empty) AndAlso moPaymentGroupDateRange.IsEmpty AndAlso _
               PmntGrpStatusDropDown.SelectedIndex = 0 AndAlso GetSelectedItem(ddlSvcCenterDropDown).Equals(Guid.Empty)) Then
            Return True
        Else
            Return False
        End If

    End Function


    Private Sub btnSearch_Click(sender As System.Object, e As System.EventArgs) Handles btnSearch.Click
        Try

            If isSearchClear() AndAlso Not GetSelectedItem(ddlCountryDropDown).Equals(Guid.Empty) Then
                MasterPage.MessageController.AddErrorAndShow(ElitaPlus.Common.ErrorCodes.GUI_SERVICE_CENTER_MUST_BE_SELECTED_ERR, True)
                Exit Sub
            End If

            If isSearchClear() Then
                MasterPage.MessageController.AddErrorAndShow(ElitaPlus.Common.ErrorCodes.GUI_SEARCH_FIELD_NOT_SUPPLIED_ERR, True)
                Exit Sub
            End If

            State.SearchClicked = True
            State.PageIndex = 0
            State.selectedPaymentGroupId = Guid.Empty
            'Me.SetStateProperties()
            State.IsGridVisible = True
            State.searchDV = Nothing
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnClearSearch_Click(sender As System.Object, e As System.EventArgs) Handles btnClearSearch.Click
        Try
            ClearSearch()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Public Sub ClearSearch()
        Try
            'Do Not clear the country and Service Center
            moInvoiceGroupNumber.Text = String.Empty
            moInvoiceNumber.Text = String.Empty
            moPaymentGroupNumber.Text = String.Empty
            moPaymentGroupDateRange.Clear()
            PmntGrpStatusDropDown.SelectedIndex = NO_ITEM_SELECTED_INDEX
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub BtnNew_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles BtnNew_WRITE.Click
        Try
            If Not PopulateStateFromSearchFields() Then Exit Sub
            State.selectedPaymentGroupId = Guid.Empty
            callPage(ClaimPaymentGroupForm.URL, State.selectedPaymentGroupId)
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

#End Region

#Region "Grid related"

    Public Sub RowCreated(sender As System.Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowCreated
        Try
            BaseItemCreated(sender, e)
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Grid_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowDataBound
        Dim pmntGrpStatusId As Guid
        Dim PymntGroupStatusLabel As Label = CType(e.Row.FindControl(GRID_COL_PMNT_GRP_STATUS_CTRL), Label)
        Try
            Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
            Dim btnEditItem As LinkButton
            If (e.Row.RowType = DataControlRowType.DataRow) OrElse (e.Row.RowType = DataControlRowType.Separator) Then
                If (e.Row.Cells(GRID_COL_NAME_PAYMENT_GROUP_ID).FindControl(GRID_COL_PMNT_GRP_NUMBER_CTRL) IsNot Nothing) Then
                    btnEditItem = CType(e.Row.Cells(GRID_COL_NAME_PAYMENT_GROUP_ID).FindControl(GRID_COL_PMNT_GRP_NUMBER_CTRL), LinkButton)
                    btnEditItem.CommandArgument = GetGuidStringFromByteArray(CType(dvRow(ClaimPaymentGroup.PaymentGroupSearchDV.COL_NAME_PAYMENT_GROUP_ID), Byte()))
                    btnEditItem.CommandName = SELECT_ACTION_COMMAND
                    btnEditItem.Text = dvRow(ClaimPaymentGroup.PaymentGroupSearchDV.COL_NAME_PAYMENT_GROUP_NUMBER).ToString
                End If
                pmntGrpStatusId = New Guid(CType(dvRow(ClaimPaymentGroup.PaymentGroupSearchDV.COL_NAME_PAYMENT_GROUP_STATUS), Byte()))
                PopulateControlFromBOProperty(PymntGroupStatusLabel, LookupListNew.GetDescriptionFromId(LookupListCache.LK_PAYMENT_GRP_STAT, pmntGrpStatusId))
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Grid_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles Grid.RowCommand
        Try
            If e.CommandName = SELECT_ACTION_COMMAND Then
                If Not e.CommandArgument.ToString().Equals(String.Empty) Then
                    State.selectedPaymentGroupId = New Guid(e.CommandArgument.ToString())
                    callPage(ClaimPaymentGroupForm.URL, State.selectedPaymentGroupId)
                End If
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub cboPageSize_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
        Try
            State.PageSize = CType(cboPageSize.SelectedValue, Integer)
            State.PageIndex = NewCurrentPageIndex(Grid, State.searchDV.Count, State.PageSize)
            Grid.PageIndex = State.PageIndex
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Grid_PageIndexChanged(sender As Object, e As System.EventArgs) Handles Grid.PageIndexChanged
        Try
            State.PageIndex = Grid.PageIndex
            State.selectedPaymentGroupId = Guid.Empty
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Grid_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Grid.PageIndexChanging
        Try
            Grid.PageIndex = e.NewPageIndex
            State.PageIndex = Grid.PageIndex
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Grid_SortCommand(source As Object, e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles Grid.Sorting
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
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

#End Region

End Class