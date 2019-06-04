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

    Private Sub Page_PageReturn(ByVal ReturnFromUrl As String, ByVal ReturnPar As Object) Handles MyBase.PageReturn
        Try
            'Me.MenuEnabled = True
            Me.IsReturningFromChild = True
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub UpdateBreadCrum()
        If (Not Me.State Is Nothing) Then
            If (Not Me.State Is Nothing) Then
                Me.MasterPage.BreadCrum = Me.MasterPage.PageTab & ElitaBase.Sperator & _
                    TranslationBase.TranslateLabelOrMessage("PAYMENT_GROUP_SEARCH")
                Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("PAYMENT_GROUP_SEARCH")
            End If
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.MasterPage.MessageController.Clear()
        Me.Form.DefaultButton = btnSearch.UniqueID
        Try
            If Not Me.IsPostBack Then
                
                Me.MasterPage.MessageController.Clear()
                Me.MasterPage.UsePageTabTitleInBreadCrum = False
                Me.MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage("CLAIM")
                UpdateBreadCrum()
                TranslateGridHeader(Grid)
                divDataContainer.Visible = False
                cboPageSize.SelectedValue = CType(Me.State.PageSize, String)
                PopulateDropdowns()

                If Me.IsReturningFromChild Then
                    ' It is returning from detail
                    PopulateSearchFieldsFromState()
                    If Not isSearchClear() Then
                        Me.State.searchDV = Nothing
                        Me.PopulateGrid()
                    End If
                Else
                    Me.ClearSearch()
                End If
            End If
            Me.DisplayNewProgressBarOnClick(Me.btnSearch, "LOADING_PAYMENT_GROUPS")
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
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

    Public Sub SetSvcCentersForCountry(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCountryDropDown.SelectedIndexChanged
        Dim oUser As New User(ElitaPlusIdentity.Current.ActiveUser.NetworkId)
        Dim oSvcCenter As New ServiceCenter
        Dim userCountriesDv As DataView = oUser.GetUserCountries(ElitaPlusIdentity.Current.ActiveUser.Id)
        'Dim contrySvcCenters As DataView
        Dim selectedCountryItem As Guid = Guid.Empty
        'Reset the Service Centre dropdown value to Nothing Selected
        ddlSvcCenterDropDown.SelectedIndex = NO_ITEM_SELECTED_INDEX

        'Get the selected Country
        selectedCountryItem = Me.GetSelectedItem(ddlCountryDropDown)
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
        Me.ClearSearch()
    End Sub



    Private Sub ddlSvcCenterDropDown_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlSvcCenterDropDown.SelectedIndexChanged
        'Get the selected Service Center
        Me.State.ServiceCenterIdInSearch = Me.GetSelectedItem(ddlSvcCenterDropDown)

    End Sub

    Public Function PopulateStateFromSearchFields() As Boolean
        Try
            Me.State.CountryIdInSearch = Me.GetSelectedItem(ddlCountryDropDown)
            Me.State.ServiceCenterIdInSearch = Me.GetSelectedItem(ddlSvcCenterDropDown)
            Me.State.PymntGrpStatusInSearch = Me.GetSelectedItem(PmntGrpStatusDropDown)
            Me.State.PymntGrpNumberInSearch = Me.moPaymentGroupNumber.Text
            Me.State.InvoiceGrpNumberInSearch = Me.moInvoiceGroupNumber.Text
            Me.State.InvoiceNumberInSearch = Me.moInvoiceNumber.Text

            Me.State.PaymentGroupDateRange = Me.moPaymentGroupDateRange

            Return True

        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Function

    Public Sub PopulateSearchFieldsFromState()
        'Dim contrySvcCenters As DataView
        Try
            Me.moPaymentGroupNumber.Text = Me.State.PymntGrpNumberInSearch
            Me.moInvoiceGroupNumber.Text = Me.State.InvoiceGrpNumberInSearch
            Me.moInvoiceNumber.Text = Me.State.InvoiceNumberInSearch

            If Not Me.State.CountryIdInSearch = Guid.Empty Then
                'Set the Country based on the selected Country Id
                Me.SetSelectedItem(Me.ddlCountryDropDown, Me.State.CountryIdInSearch)
                'Populate the Service Center based on the selected Country/Country in State
                'Dim oSvcCenter As New ServiceCenter
                'contrySvcCenters = oSvcCenter.GetServiceCenterForCountry(Me.State.CountryIdInSearch)
                'Me.BindListControlToDataView(Me.ddlSvcCenterDropDown, contrySvcCenters, , "SERVICE_CENTER_ID")

                Dim lstcontext As ListContext = New ListContext()
                lstcontext.CountryId = Me.State.CountryIdInSearch
                Dim svcCenterList As ListItem() = CommonConfigManager.Current.ListManager.GetList("ServiceCenterListByCountry", Thread.CurrentPrincipal.GetLanguageCode(), lstcontext)

                ddlSvcCenterDropDown.Populate(svcCenterList, New PopulateOptions() With
                                                 {
                                                   .AddBlankItem = True,
                                                   .SortFunc = AddressOf .GetDescription
                                                  })

                'Set the Service Center based on the selected Service Center
                If Not Me.State.ServiceCenterIdInSearch = Guid.Empty Then
                    Me.SetSelectedItem(Me.ddlSvcCenterDropDown, Me.State.ServiceCenterIdInSearch)
                End If
            End If

            If Not Me.State.PymntGrpStatusInSearch = Guid.Empty Then
                Me.SetSelectedItem(Me.PmntGrpStatusDropDown, Me.State.PymntGrpStatusInSearch)
            End If

            Me.moPaymentGroupDateRange = Me.State.PaymentGroupDateRange

        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub PopulateGrid()
        Try

            If Not PopulateStateFromSearchFields() Then Exit Sub

            If (Me.State.searchDV Is Nothing) Then
                Me.State.searchDV = ClaimPaymentGroup.GetPaymentGroup(Me.State.ServiceCenterIdInSearch, Me.State.PymntGrpNumberInSearch, _
                                                                      Me.State.PymntGrpStatusInSearch, _
                                                                      DirectCast(Me.State.PaymentGroupDateRange.Value, SearchCriteriaStructType(Of Date)), _
                                                                      Me.State.InvoiceNumberInSearch, _
                                                                      Me.State.InvoiceGrpNumberInSearch)
                If Me.State.SearchClicked Then
                    Me.ValidSearchResultCountNew(Me.State.searchDV.Count, True)
                    Me.State.SearchClicked = False
                End If
            End If

            Grid.PageSize = State.PageSize
            If State.searchDV.Count = 0 Then
            Else
                Me.Grid.DataSource = Me.State.searchDV
            End If
            Me.State.PageIndex = Me.Grid.PageIndex
            divDataContainer.Visible = True

            Grid.Columns(GRID_COL_NAME_PAYMENT_GROUP_ID).Visible = False
            Grid.Columns(GRID_COL_NAME_PAYMENT_GROUP_NUMBER).Visible = True
            Grid.Columns(GRID_COL_NAME_PAYMENT_GROUP_DATE).SortExpression = ClaimPaymentGroup.PaymentGroupSearchDV.COL_NAME_PAYMENT_GROUP_DATE
            Grid.Columns(GRID_COL_NAME_PAYMENT_GROUP_STATUS).Visible = True
            Grid.Columns(GRID_COL_NAME_PAYMENT_GROUP_TOTAL).SortExpression = ClaimPaymentGroup.PaymentGroupSearchDV.COL_NAME_PAYMENT_GROUP_TOTAL

            If (Not Me.State.SortExpression.Equals(String.Empty)) Then
                Me.State.searchDV.Sort = Me.State.SortExpression 'Me.SortDirection
            End If

            HighLightSortColumn(Grid, Me.State.SortExpression, True) 'Me.SortDirection
            Me.Grid.DataBind()

            ControlMgr.SetVisibleControl(Me, Grid, True)
            Session("recCount") = Me.State.searchDV.Count

            If Me.Grid.Visible Then
                Me.lblSearchResults.Visible = True
                Me.lblSearchResults.Text = Me.State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
            End If

        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

#Region " Button Clicks "

    Private Function isSearchClear() As Boolean

        If (moInvoiceGroupNumber.Text.Trim().Equals(String.Empty) AndAlso moInvoiceNumber.Text.Trim().Equals(String.Empty) AndAlso _
               moPaymentGroupNumber.Text.Trim().Equals(String.Empty) AndAlso moPaymentGroupDateRange.IsEmpty AndAlso _
               PmntGrpStatusDropDown.SelectedIndex = 0 AndAlso Me.GetSelectedItem(ddlSvcCenterDropDown).Equals(Guid.Empty)) Then
            Return True
        Else
            Return False
        End If

    End Function


    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try

            If isSearchClear() AndAlso Not Me.GetSelectedItem(ddlCountryDropDown).Equals(Guid.Empty) Then
                Me.MasterPage.MessageController.AddErrorAndShow(ElitaPlus.Common.ErrorCodes.GUI_SERVICE_CENTER_MUST_BE_SELECTED_ERR, True)
                Exit Sub
            End If

            If isSearchClear() Then
                Me.MasterPage.MessageController.AddErrorAndShow(ElitaPlus.Common.ErrorCodes.GUI_SEARCH_FIELD_NOT_SUPPLIED_ERR, True)
                Exit Sub
            End If

            Me.State.SearchClicked = True
            Me.State.PageIndex = 0
            Me.State.selectedPaymentGroupId = Guid.Empty
            'Me.SetStateProperties()
            Me.State.IsGridVisible = True
            Me.State.searchDV = Nothing
            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnClearSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearSearch.Click
        Try
            Me.ClearSearch()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Public Sub ClearSearch()
        Try
            'Do Not clear the country and Service Center
            Me.moInvoiceGroupNumber.Text = String.Empty
            Me.moInvoiceNumber.Text = String.Empty
            Me.moPaymentGroupNumber.Text = String.Empty
            Me.moPaymentGroupDateRange.Clear()
            Me.PmntGrpStatusDropDown.SelectedIndex = NO_ITEM_SELECTED_INDEX
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub BtnNew_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnNew_WRITE.Click
        Try
            If Not PopulateStateFromSearchFields() Then Exit Sub
            Me.State.selectedPaymentGroupId = Guid.Empty
            Me.callPage(ClaimPaymentGroupForm.URL, Me.State.selectedPaymentGroupId)
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

#End Region

#Region "Grid related"

    Public Sub RowCreated(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowCreated
        Try
            BaseItemCreated(sender, e)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Grid_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowDataBound
        Dim pmntGrpStatusId As Guid
        Dim PymntGroupStatusLabel As Label = CType(e.Row.FindControl(GRID_COL_PMNT_GRP_STATUS_CTRL), Label)
        Try
            Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
            Dim btnEditItem As LinkButton
            If (e.Row.RowType = DataControlRowType.DataRow) OrElse (e.Row.RowType = DataControlRowType.Separator) Then
                If (Not e.Row.Cells(Me.GRID_COL_NAME_PAYMENT_GROUP_ID).FindControl(GRID_COL_PMNT_GRP_NUMBER_CTRL) Is Nothing) Then
                    btnEditItem = CType(e.Row.Cells(Me.GRID_COL_NAME_PAYMENT_GROUP_ID).FindControl(GRID_COL_PMNT_GRP_NUMBER_CTRL), LinkButton)
                    btnEditItem.CommandArgument = GetGuidStringFromByteArray(CType(dvRow(ClaimPaymentGroup.PaymentGroupSearchDV.COL_NAME_PAYMENT_GROUP_ID), Byte()))
                    btnEditItem.CommandName = SELECT_ACTION_COMMAND
                    btnEditItem.Text = dvRow(ClaimPaymentGroup.PaymentGroupSearchDV.COL_NAME_PAYMENT_GROUP_NUMBER).ToString
                End If
                pmntGrpStatusId = New Guid(CType(dvRow(ClaimPaymentGroup.PaymentGroupSearchDV.COL_NAME_PAYMENT_GROUP_STATUS), Byte()))
                Me.PopulateControlFromBOProperty(PymntGroupStatusLabel, LookupListNew.GetDescriptionFromId(LookupListCache.LK_PAYMENT_GRP_STAT, pmntGrpStatusId))
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Grid_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles Grid.RowCommand
        Try
            If e.CommandName = SELECT_ACTION_COMMAND Then
                If Not e.CommandArgument.ToString().Equals(String.Empty) Then
                    Me.State.selectedPaymentGroupId = New Guid(e.CommandArgument.ToString())
                    Me.callPage(ClaimPaymentGroupForm.URL, Me.State.selectedPaymentGroupId)
                End If
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub cboPageSize_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
        Try
            State.PageSize = CType(cboPageSize.SelectedValue, Integer)
            Me.State.PageIndex = NewCurrentPageIndex(Grid, State.searchDV.Count, State.PageSize)
            Me.Grid.PageIndex = Me.State.PageIndex
            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Grid_PageIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Grid.PageIndexChanged
        Try
            Me.State.PageIndex = Grid.PageIndex
            Me.State.selectedPaymentGroupId = Guid.Empty
            PopulateGrid()
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

    Private Sub Grid_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles Grid.Sorting
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
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

#End Region

End Class