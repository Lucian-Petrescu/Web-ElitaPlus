Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Imports Assurant.ElitaPlus.DALObjects
Imports System.Threading
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms
Imports System.Collections.Generic

Namespace Tables
    Partial Public Class EventConfigListForm
        Inherits ElitaPlusSearchPage

#Region "Constants"
        Public Const URL As String = "Tables/EventConfigListForm.aspx"
        Public Const PAGETITLE As String = "EVENT_CONFIG"
        Public Const PAGETAB As String = "ADMIN"
        Public Const SUMMARYTITLE As String = "SEARCH"

        Private Const GRID_COL_EVENT_CONFIG_ID_IDX As Integer = 0
        Private Const GRID_COL_COMPANY_GROUP_IDX As Integer = 1
        Private Const GRID_COL_COMPANY_IDX As Integer = 2
        Private Const GRID_COL_COUNTRY_IDX As Integer = 3
        Private Const GRID_COL_DEALER_GROUP_IDX As Integer = 4
        Private Const GRID_COL_DEALER_IDX As Integer = 5
        Private Const GRID_COL_PRODUCT_CODE_IDX As Integer = 6
        Private Const GRID_COL_COVERAGE_TYPE_IDX As Integer = 7
        Private Const GRID_COL_EVENT_TYPE_IDX As Integer = 8
        Private Const GRID_COL_EVENT_ARGUMENT_IDX As Integer = 9
        Private Const GRID_COL_EDIT_IDX As Integer = 10
        Private Const GRID_COL_DELETE_IDX As Integer = 11

        Private Const MSG_CONFIRM_PROMPT As String = "MSG_CONFIRM_PROMPT"
        Private Const MSG_RECORD_DELETED_OK As String = "MSG_RECORD_DELETED_OK"
        Private Const MSG_RECORD_SAVED_OK As String = "MSG_RECORD_SAVED_OK"
        Private Const MSG_RECORD_NOT_SAVED As String = "MSG_RECORD_NOT_SAVED"

        Private Const GRID_CTRL_NAME_LABLE_EVENT_CONFIG_ID As String = "lblEventConfigID"
        Private Const GRID_CTRL_NAME_LABLE_COMPANY_GROUP As String = "lblCompanyGroup"
        Private Const GRID_CTRL_NAME_LABEL_COMPANY As String = "lblCompany"
        Private Const GRID_CTRL_NAME_LABLE_COUNTRY As String = "lblCountry"
        Private Const GRID_CTRL_NAME_LABLE_DEALER_GROUP As String = "lblDealerGroup"
        Private Const GRID_CTRL_NAME_LABLE_DEALER As String = "lblDealer"
        Private Const GRID_CTRL_NAME_LABLE_PRODUCT_CODE As String = "lblProdCode"
        Private Const GRID_CTRL_NAME_LABLE_COVERAGE_TYPE As String = "lblCoverageType"
        Private Const GRID_CTRL_NAME_LABLE_EVENT_TYPE As String = "lblEventType"
        Private Const GRID_CTRL_NAME_LABLE_EVENT_ARGUMENT As String = "lblEventArgument"

        Private Const GRID_CTRL_NAME_EDIT_COMPANY_GROUP As String = "ddlCompanyGroup"
        Private Const GRID_CTRL_NAME_EDIT_COMPANY As String = "ddlCompany"
        Private Const GRID_CTRL_NAME_EDIT_COUNTRY As String = "ddlCountry"
        Private Const GRID_CTRL_NAME_EDIT_DEALER_GROUP As String = "ddlDealerGroup"
        Private Const GRID_CTRL_NAME_EDIT_DEALER As String = "ddlDealer"
        Private Const GRID_CTRL_NAME_EDIT_PRODUCT_CODE As String = "txtProdCode"
        Private Const GRID_CTRL_NAME_EDIT_COVERAGE_TYPE As String = "ddlCoverageType"
        Private Const GRID_CTRL_NAME_EDIT_EVENT_TYPE As String = "ddlEventType"
        Private Const GRID_CTRL_NAME_EDIT_EVENT_ARGUMENT As String = "ddlEventArgument"

        Private Const EDIT_COMMAND As String = "SelectAction"
        Private Const DELETE_COMMAND As String = "DeleteRecord"
        Private Const SORT_COMMAND As String = "Sort"

        Private Const NO_ROW_SELECTED_INDEX As Integer = -1
#End Region

#Region "Page State"
        ' This class keeps the current state for the search page.
        Class MyState
            Public MyBO As EventConfig
            Public EventConfigID As Guid
            Public PageIndex As Integer = 0
            Public PageSize As Integer = DEFAULT_NEW_UI_PAGE_SIZE
            Public searchDV As EventConfig.EventConfigSearchDV = Nothing
            Public HasDataChanged As Boolean
            Public IsGridAddNew As Boolean = False
            Public IsEditMode As Boolean = False
            Public IsGridVisible As Boolean
            Public IsAfterSave As Boolean
            Public SortExpression As String = EventConfig.EventConfigSearchDV.COL_EVENT_TYPE_DESC
            Public searchCompanyGrp As Guid = Guid.Empty
            Public searchCompany As Guid = Guid.Empty
            Public searchCountry As Guid = Guid.Empty
            Public searchDealerGrp As Guid = Guid.Empty
            Public searchDealer As Guid = Guid.Empty
            Public searchCoverageType As Guid = Guid.Empty
            Public searchProdCode As String = ""
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

        Public Property SortDirection() As String
            Get
                If Not ViewState("SortDirection") Is Nothing Then
                    Return ViewState("SortDirection").ToString
                Else
                    Return String.Empty
                End If

            End Get
            Set(ByVal value As String)
                ViewState("SortDirection") = value
            End Set
        End Property
#End Region

#Region "Bread Crum"
        Private Sub UpdateBreadCrum()
            Me.MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage(PAGETAB)
            Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage(SUMMARYTITLE)
            Me.MasterPage.UsePageTabTitleInBreadCrum = False
            Me.MasterPage.BreadCrum = TranslationBase.TranslateLabelOrMessage(PAGETAB) + ElitaBase.Sperator + TranslationBase.TranslateLabelOrMessage(PAGETITLE)
        End Sub
#End Region

#Region "Page Events"
        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            Me.MasterPage.MessageController.Clear()
            Me.Form.DefaultButton = btnSearch.UniqueID
            Try
                If Not Me.IsPostBack Then
                    UpdateBreadCrum()
                    'Me.SortDirection = Role.RoleSearchDV.COL_NAME_CODE
                    ControlMgr.SetVisibleControl(Me, moSearchResults, False)
                    'SetControlState()
                    Me.State.PageIndex = 0
                    Me.TranslateGridHeader(Grid)
                    Me.TranslateGridControls(Grid)
                    PopulateSearchControls()
                Else
                    CheckIfComingFromDeleteConfirm()
                    BindBoPropertiesToGridHeaders()
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
            Me.ShowMissingTranslations(Me.MasterPage.MessageController)
        End Sub
#End Region

#Region "Helper functions"
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
            'Do the delete here
            Me.State.ActionInProgress = DetailPageCommand.Nothing_

            Dim obj As EventConfig = New EventConfig(Me.State.EventConfigID)
            obj.DeleteAndSave()

            Me.MasterPage.MessageController.AddSuccess(Me.MSG_RECORD_DELETED_OK, True)
            Me.State.PageIndex = Grid.PageIndex

            'Set the IsAfterSave flag to TRUE so that the Paging logic gets invoked
            Me.State.IsAfterSave = True
            Me.State.searchDV = Nothing
            PopulateGrid()
            Me.State.PageIndex = Grid.PageIndex
            Me.State.IsEditMode = False
            SetControlState()
        End Sub

        Private Sub ReturnFromEditing()

            Grid.EditIndex = NO_ROW_SELECTED_INDEX

            If (Me.Grid.PageCount = 0) Then
                'if returning to the "1st time in" screen
                ControlMgr.SetVisibleControl(Me, Grid, False)
            Else
                ControlMgr.SetVisibleControl(Me, Grid, True)
            End If

            Me.State.IsEditMode = False
            Me.PopulateGrid()
            Me.State.PageIndex = Grid.PageIndex
            SetControlState()

        End Sub

        Private Function GetEventArgumentBasedOnEventType(ByVal EventTypeCode As String) As DataElements.ListItem()
            Dim ocListContext As New ListContext
            If (EventTypeCode = "ISSUE_OPENED" _
                    Or EventTypeCode = "ISSUE_RESOLVED" _
                    Or EventTypeCode = "ISSUE_REJECTED" _
                    Or EventTypeCode = "ISSUE_CLOSED" _
                    Or EventTypeCode = "ISSUE_PENDING" _
                    Or EventTypeCode = "ISSUE_WAIVED") Then

                Return CommonConfigManager.Current.ListManager.GetList(listCode:="GetIssue", languageCode:=Thread.CurrentPrincipal.GetLanguageCode())
            ElseIf (EventTypeCode = "CLM_EXT_STATUS") Then
                ocListContext.CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
                Return CommonConfigManager.Current.ListManager.GetList(listCode:="ExtendedStatusByCompanyGroup", languageCode:=Thread.CurrentPrincipal.GetLanguageCode(), context:=ocListContext)
            ElseIf (EventTypeCode = "CRT_CANCEL") Then
                Dim oCancellationList As New List(Of DataElements.ListItem)
                For Each _company As Guid In ElitaPlusIdentity.Current.ActiveUser.Companies
                    ocListContext.CompanyId = _company

                    Dim oCancellationListForCompany As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(ListCodes.CancellationReasonsByCompany, context:=ocListContext)
                    If oCancellationListForCompany.Count > 0 Then
                        If Not oCancellationList Is Nothing Then
                            oCancellationList.AddRange(oCancellationListForCompany)
                        Else
                            oCancellationList = oCancellationListForCompany.Clone()
                        End If
                    End If
                Next
                Return oCancellationList.ToArray
            ElseIf (EventTypeCode = "CLAIM_DENIED") Then
                ocListContext = New ListContext
                ocListContext.CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
                Dim oDeniedReasonList = CommonConfigManager.Current.ListManager.GetList("DNDREASON", languageCode:=Thread.CurrentPrincipal.GetLanguageCode(), context:=ocListContext)
                Return oDeniedReasonList
            Else
                Return Nothing
            End If

        End Function

        Protected Sub BindBoPropertiesToGridHeaders()
            Me.BindBOPropertyToGridHeader(Me.State.MyBO, "CompanyGroupId", Me.Grid.Columns(Me.GRID_COL_COMPANY_GROUP_IDX))
            Me.BindBOPropertyToGridHeader(Me.State.MyBO, "CompanyId", Me.Grid.Columns(Me.GRID_COL_COMPANY_IDX))
            Me.BindBOPropertyToGridHeader(Me.State.MyBO, "CountryId", Me.Grid.Columns(Me.GRID_COL_COUNTRY_IDX))
            Me.BindBOPropertyToGridHeader(Me.State.MyBO, "DealerGroupId", Me.Grid.Columns(Me.GRID_COL_DEALER_GROUP_IDX))
            Me.BindBOPropertyToGridHeader(Me.State.MyBO, "DealerId", Me.Grid.Columns(Me.GRID_COL_DEALER_IDX))
            Me.BindBOPropertyToGridHeader(Me.State.MyBO, "ProductCode", Me.Grid.Columns(Me.GRID_COL_PRODUCT_CODE_IDX))
            Me.BindBOPropertyToGridHeader(Me.State.MyBO, "CoverageTypeId", Me.Grid.Columns(Me.GRID_COL_COVERAGE_TYPE_IDX))
            Me.BindBOPropertyToGridHeader(Me.State.MyBO, "EventTypeId", Me.Grid.Columns(Me.GRID_COL_EVENT_TYPE_IDX))
            Me.BindBOPropertyToGridHeader(Me.State.MyBO, "EventArgumentId", Me.Grid.Columns(Me.GRID_COL_EVENT_ARGUMENT_IDX))
            Me.ClearGridViewHeadersAndLabelsErrSign()
        End Sub

        Protected Sub PopulateSearchControls()
            Try
                Dim companyGroupList As New Collections.Generic.List(Of DataElements.ListItem)
                Dim ocListContext As New ListContext

                For Each company_id As Guid In ElitaPlusIdentity.Current.ActiveUser.Companies
                    ocListContext.CompanyId = company_id
                    Dim companyGroupListForCompany As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="CompanyGroupByCompany", context:=ocListContext)
                    If companyGroupListForCompany.Count > 0 Then
                        If Not companyGroupList Is Nothing Then
                            companyGroupList.AddRange(companyGroupListForCompany)
                        Else
                            companyGroupList = companyGroupListForCompany.Clone()
                        End If
                    End If
                Next

                Me.ddlSearchCompanyGroup.Populate(companyGroupList.ToArray(), New PopulateOptions() With
                {
                    .AddBlankItem = True,
                    .SortFunc = AddressOf .GetCode
                })

                'Dim dv As DataView = LookupListNew.GetUserCompanyGroupList()
                'ddlSearchCompanyGroup.Items.Add(New ListItem("", Guid.Empty.ToString))
                'If dv.Count > 0 Then
                'ddlSearchCompanyGroup.Items.Add(New ListItem(dv(0)("DESCRIPTION").ToString, New Guid(CType(dv(0)("ID"), Byte())).ToString))
                'End If

                Dim companyList As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="Company")
                Dim filteredCompanyList As DataElements.ListItem() = (From x In companyList
                                                                      Where ElitaPlusIdentity.Current.ActiveUser.Companies.Contains(x.ListItemId)
                                                                      Select x).ToArray()

                Dim countryList As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="Country")
                Dim filteredCountryList As DataElements.ListItem() = (From x In countryList
                                                                      Where ElitaPlusIdentity.Current.ActiveUser.Countries.Contains(x.ListItemId)
                                                                      Select x).ToArray()

                Me.ddlSearchCompany.Populate(filteredCompanyList, New PopulateOptions() With
                {
                    .AddBlankItem = True
                })
                'Me.BindListControlToDataView(Me.ddlSearchCompany, LookupListNew.GetUserCompaniesLookupList(), , , True)

                Me.ddlSearchCountry.Populate(filteredCountryList, New PopulateOptions() With
                {
                    .AddBlankItem = True
                })
                'Me.BindListControlToDataView(Me.ddlSearchCountry, LookupListNew.GetUserCountriesLookupList(), , , True)

                Dim dealerGroupList As New Collections.Generic.List(Of DataElements.ListItem)
                Dim odListContext As New ListContext

                For Each company_id As Guid In ElitaPlusIdentity.Current.ActiveUser.Companies
                    odListContext.CompanyId = company_id
                    Dim dealerGroupListForCompany As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="DealerGroupByCompany", context:=odListContext)
                    If dealerGroupListForCompany.Count > 0 Then
                        If dealerGroupList IsNot Nothing Then
                            dealerGroupList.AddRange(dealerGroupListForCompany)
                        Else
                            dealerGroupList = dealerGroupListForCompany.Clone()
                        End If
                    End If
                Next

                Me.ddlSearchDealerGroup.Populate(dealerGroupList.ToArray(), New PopulateOptions() With
                                                     {
                                                     .AddBlankItem = True,
                                                     .SortFunc = AddressOf .GetCode
                                                     })

                Dim dealerist As New Collections.Generic.List(Of DataElements.ListItem)
                Dim oListContext As New ListContext

                For Each company_id As Guid In ElitaPlusIdentity.Current.ActiveUser.Companies
                    oListContext.CompanyId = company_id
                    Dim dealerListForCompany As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="DealerListByCompany", context:=oListContext)
                    If dealerListForCompany.Count > 0 Then
                        If Not dealerist Is Nothing Then
                            dealerist.AddRange(dealerListForCompany)
                        Else
                            dealerist = dealerListForCompany.Clone()
                        End If
                    End If
                Next

                Me.ddlSearchDealer.Populate(dealerist.ToArray(), New PopulateOptions() With
                {
                    .AddBlankItem = True
                })
                'Me.BindListControlToDataView(Me.ddlSearchDealer, LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies, False, "Code"), , , True)

                Me.ddlSearchCoverageType.Populate(CommonConfigManager.Current.ListManager.GetList("CTYP", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
                {
                    .AddBlankItem = True,
                    .SortFunc = AddressOf PopulateOptions.GetDescription
                })
                'Me.BindListControlToDataView(Me.ddlSearchCoverageType, LookupListNew.GetCoverageTypeLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), , , True)

                If Me.State.searchCompanyGrp <> Guid.Empty Then
                    Me.SetSelectedItem(ddlSearchCompanyGroup, Me.State.searchCompanyGrp)
                End If

                If Me.State.searchCompany <> Guid.Empty Then
                    Me.SetSelectedItem(ddlSearchCompany, Me.State.searchCompany)
                End If

                If Me.State.searchCountry <> Guid.Empty Then
                    Me.SetSelectedItem(ddlSearchCountry, Me.State.searchCountry)
                End If

                If Me.State.searchDealerGrp <> Guid.Empty Then
                    Me.SetSelectedItem(ddlSearchDealerGroup, Me.State.searchDealerGrp)
                End If

                If Me.State.searchDealer <> Guid.Empty Then
                    Me.SetSelectedItem(ddlSearchDealer, Me.State.searchDealer)
                End If

                If Me.State.searchCoverageType <> Guid.Empty Then
                    Me.SetSelectedItem(ddlSearchCoverageType, Me.State.searchCoverageType)
                End If

            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try

        End Sub

        Private Sub SetControlState()
            If (Me.State.IsEditMode) Then
                ControlMgr.SetVisibleControl(Me, btnNew, False)
                ControlMgr.SetEnableControl(Me, btnSearch, False)
                ControlMgr.SetEnableControl(Me, btnClearSearch, False)
                Me.MenuEnabled = False
                If (Me.cboPageSize.Enabled) Then
                    ControlMgr.SetEnableControl(Me, cboPageSize, False)
                End If
            Else
                ControlMgr.SetVisibleControl(Me, btnNew, True)
                ControlMgr.SetEnableControl(Me, btnSearch, True)
                ControlMgr.SetEnableControl(Me, btnClearSearch, True)
                Me.MenuEnabled = True
                If Not (Me.cboPageSize.Enabled) Then
                    ControlMgr.SetEnableControl(Me, Me.cboPageSize, True)
                End If
            End If
        End Sub

        Private Sub RemoveNewRowFromSearchDV()
            Dim rowind As Integer = NO_ITEM_SELECTED_INDEX
            With State
                If Not .searchDV Is Nothing Then
                    rowind = FindSelectedRowIndexFromGuid(.searchDV, .EventConfigID)
                End If
            End With
            If rowind <> NO_ITEM_SELECTED_INDEX Then State.searchDV.Delete(rowind)
        End Sub

        Private Function PopulateBOFromForm() As Boolean

            With Me.State.MyBO
                .ProductCode = CType(Grid.Rows((Me.Grid.EditIndex)).Cells(GRID_COL_PRODUCT_CODE_IDX).FindControl(GRID_CTRL_NAME_EDIT_PRODUCT_CODE), TextBox).Text.Trim.ToUpper

                Dim objDDL As DropDownList
                objDDL = CType(Grid.Rows((Me.Grid.EditIndex)).Cells(GRID_COL_COMPANY_GROUP_IDX).FindControl(GRID_CTRL_NAME_EDIT_COMPANY_GROUP), DropDownList)
                PopulateBOProperty(Me.State.MyBO, "CompanyGroupId", objDDL)

                objDDL = CType(Grid.Rows((Me.Grid.EditIndex)).Cells(GRID_COL_COMPANY_IDX).FindControl(GRID_CTRL_NAME_EDIT_COMPANY), DropDownList)
                PopulateBOProperty(Me.State.MyBO, "CompanyId", objDDL)

                objDDL = CType(Grid.Rows((Me.Grid.EditIndex)).Cells(GRID_COL_COUNTRY_IDX).FindControl(GRID_CTRL_NAME_EDIT_COUNTRY), DropDownList)
                PopulateBOProperty(Me.State.MyBO, "CountryId", objDDL)

                objDDL = CType(Grid.Rows((Me.Grid.EditIndex)).Cells(GRID_COL_DEALER_GROUP_IDX).FindControl(GRID_CTRL_NAME_EDIT_DEALER_GROUP), DropDownList)
                PopulateBOProperty(Me.State.MyBO, "DealerGroupId", objDDL)

                objDDL = CType(Grid.Rows((Me.Grid.EditIndex)).Cells(GRID_COL_DEALER_IDX).FindControl(GRID_CTRL_NAME_EDIT_DEALER), DropDownList)
                PopulateBOProperty(Me.State.MyBO, "DealerId", objDDL)

                objDDL = CType(Grid.Rows((Me.Grid.EditIndex)).Cells(GRID_COL_EVENT_TYPE_IDX).FindControl(GRID_CTRL_NAME_EDIT_EVENT_TYPE), DropDownList)
                PopulateBOProperty(Me.State.MyBO, "EventTypeId", objDDL)


                objDDL = CType(Grid.Rows((Me.Grid.EditIndex)).Cells(GRID_COL_EVENT_ARGUMENT_IDX).FindControl(GRID_CTRL_NAME_EDIT_EVENT_ARGUMENT), DropDownList)
                PopulateBOProperty(Me.State.MyBO, "EventArgumentId", objDDL)

                objDDL = CType(Grid.Rows((Me.Grid.EditIndex)).Cells(GRID_COL_COVERAGE_TYPE_IDX).FindControl(GRID_CTRL_NAME_EDIT_COVERAGE_TYPE), DropDownList)
                PopulateBOProperty(Me.State.MyBO, "CoverageTypeId", objDDL)
            End With

            If Me.ErrCollection.Count > 0 Then
                Throw New PopulateBOErrorException
            End If
        End Function

#End Region

#Region "Grid related"
        Private Sub Grid_PageIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Grid.PageIndexChanged
            Try
                If (Not (Me.State.IsEditMode)) Then
                    Me.State.PageIndex = Grid.PageIndex
                    Me.State.EventConfigID = Guid.Empty
                    Me.PopulateGrid()
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
                Me.PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub Grid_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowDataBound
            Try
                Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
                Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
                Dim strECID As String
                Dim dvYesNo As DataView = LookupListNew.DropdownLookupList("YESNO", ElitaPlusIdentity.Current.ActiveUser.LanguageId, True)

                If Not dvRow Is Nothing Then
                    strECID = GetGuidStringFromByteArray(CType(dvRow(EventConfig.EventConfigSearchDV.COL_EVENT_CONFIG_ID), Byte()))
                    If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then
                        CType(e.Row.Cells(Me.GRID_COL_EVENT_CONFIG_ID_IDX).FindControl(Me.GRID_CTRL_NAME_LABLE_EVENT_CONFIG_ID), Label).Text = strECID

                        If (Me.State.IsEditMode = True AndAlso Me.State.EventConfigID.ToString.Equals(strECID)) Then
                            CType(e.Row.Cells(Me.GRID_COL_PRODUCT_CODE_IDX).FindControl(Me.GRID_CTRL_NAME_EDIT_PRODUCT_CODE), TextBox).Text = dvRow(EventConfig.EventConfigSearchDV.COL_PRODUCT_CODE).ToString

                            Dim objDDL As DropDownList
                            'Dim dv As DataView
                            Dim guidVal As Guid

                            'populate company group dropdown
                            objDDL = CType(e.Row.Cells(Me.GRID_COL_COMPANY_GROUP_IDX).FindControl(Me.GRID_CTRL_NAME_EDIT_COMPANY_GROUP), DropDownList)

                            Dim companyGroupList As New Collections.Generic.List(Of DataElements.ListItem)
                            Dim cListContext As New ListContext

                            For Each company_id As Guid In ElitaPlusIdentity.Current.ActiveUser.Companies
                                cListContext.CompanyId = company_id
                                Dim companyGroupListForUser As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="CompanyGroupByCompany", context:=cListContext)
                                If companyGroupListForUser.Count > 0 Then
                                    If Not companyGroupList Is Nothing Then
                                        companyGroupList.AddRange(companyGroupListForUser)
                                    Else
                                        companyGroupList = companyGroupListForUser.Clone()
                                    End If
                                End If
                            Next

                            objDDL.Populate(companyGroupList.ToArray(), New PopulateOptions() With
                                {
                                    .AddBlankItem = True
                                })

                            'dv = LookupListNew.GetUserCompanyGroupList()
                            'objDDL.Items.Add(New ListItem("", Guid.Empty.ToString))
                            'If dv.Count > 0 Then
                            '    objDDL.Items.Add(New ListItem(dv(0)("DESCRIPTION").ToString, New Guid(CType(dv(0)("ID"), Byte())).ToString))
                            'End If

                            If Not dvRow(EventConfig.EventConfigSearchDV.COL_COMPANY_GROUP_ID) Is DBNull.Value Then
                                guidVal = New Guid(CType(dvRow(EventConfig.EventConfigSearchDV.COL_COMPANY_GROUP_ID), Byte()))
                                SetSelectedItem(objDDL, guidVal)
                            End If

                            'populate company dropdown
                            objDDL = CType(e.Row.Cells(Me.GRID_COL_COMPANY_IDX).FindControl(Me.GRID_CTRL_NAME_EDIT_COMPANY), DropDownList)

                            Dim comListContext As New ListContext
                            comListContext.UserId = ElitaPlusIdentity.Current.ActiveUser.Id
                            Dim companyListForUser As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="UserCompanies", context:=comListContext)

                            objDDL.Populate(companyListForUser, New PopulateOptions() With
                                {
                                    .AddBlankItem = True
                                })

                            'dv = LookupListNew.GetUserCompaniesLookupList()
                            'Me.BindListControlToDataView(objDDL, dv, , , True)

                            If Not dvRow(EventConfig.EventConfigSearchDV.COL_COMPANY_ID) Is DBNull.Value Then
                                guidVal = New Guid(CType(dvRow(EventConfig.EventConfigSearchDV.COL_COMPANY_ID), Byte()))
                                SetSelectedItem(objDDL, guidVal)
                            End If

                            objDDL = CType(e.Row.Cells(Me.GRID_COL_COUNTRY_IDX).FindControl(Me.GRID_CTRL_NAME_EDIT_COUNTRY), DropDownList)

                            'populate country dropdown
                            Dim countryList As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="Country")
                            Dim filteredCountryList As DataElements.ListItem() = (From x In countryList
                                                                                  Where ElitaPlusIdentity.Current.ActiveUser.Countries.Contains(x.ListItemId)
                                                                                  Select x).ToArray()
                            objDDL.Populate(filteredCountryList, New PopulateOptions() With
                                {
                                    .AddBlankItem = True
                                })

                            'dv = LookupListNew.GetUserCountriesLookupList()
                            'Me.BindListControlToDataView(objDDL, dv, , , True)

                            If Not dvRow(EventConfig.EventConfigSearchDV.COL_COUNTRY_ID) Is DBNull.Value Then
                                guidVal = New Guid(CType(dvRow(EventConfig.EventConfigSearchDV.COL_COUNTRY_ID), Byte()))
                                SetSelectedItem(objDDL, guidVal)
                            End If

                            'populate dealer group dropdown
                            objDDL = CType(e.Row.Cells(Me.GRID_COL_DEALER_GROUP_IDX).FindControl(Me.GRID_CTRL_NAME_EDIT_DEALER_GROUP), DropDownList)

                            Dim dealerGroupList As New Collections.Generic.List(Of DataElements.ListItem)
                            Dim odListContext As New ListContext

                            For Each company_id As Guid In ElitaPlusIdentity.Current.ActiveUser.Companies
                                odListContext.CompanyId = company_id
                                Dim dealerGroupListForCompany As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="DealerGroupByCompany", context:=odListContext)
                                If dealerGroupListForCompany.Count > 0 Then
                                    If dealerGroupList IsNot Nothing Then
                                        dealerGroupList.AddRange(dealerGroupListForCompany)
                                    Else
                                        dealerGroupList = dealerGroupListForCompany.Clone()
                                    End If
                                End If
                            Next

                            objDDL.Populate(dealerGroupList.ToArray(), New PopulateOptions() With
                                                     {
                                                     .AddBlankItem = True
                                                     })

                            If dvRow(EventConfig.EventConfigSearchDV.COL_DEALER_GROUP_ID) IsNot DBNull.Value Then
                                guidVal = New Guid(CType(dvRow(EventConfig.EventConfigSearchDV.COL_DEALER_GROUP_ID), Byte()))
                                SetSelectedItem(objDDL, guidVal)
                            End If



                            'populate dealer dropdown
                            objDDL = CType(e.Row.Cells(Me.GRID_COL_DEALER_IDX).FindControl(Me.GRID_CTRL_NAME_EDIT_DEALER), DropDownList)

                            Dim dealerist As New Collections.Generic.List(Of DataElements.ListItem)
                            Dim oListContext As New ListContext

                            For Each company_id As Guid In ElitaPlusIdentity.Current.ActiveUser.Companies
                                oListContext.CompanyId = company_id
                                Dim dealerListForCompany As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="DealerListByCompany", context:=oListContext)
                                If dealerListForCompany.Count > 0 Then
                                    If Not dealerist Is Nothing Then
                                        dealerist.AddRange(dealerListForCompany)
                                    Else
                                        dealerist = dealerListForCompany.Clone()
                                    End If
                                End If
                            Next

                            objDDL.Populate(dealerist.ToArray(), New PopulateOptions() With
                                {
                                    .AddBlankItem = True
                                })

                            'dv = LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies, False, "Code")
                            'Me.BindListControlToDataView(objDDL, dv, , , True)

                            If Not dvRow(EventConfig.EventConfigSearchDV.COL_DEALER_ID) Is DBNull.Value Then
                                guidVal = New Guid(CType(dvRow(EventConfig.EventConfigSearchDV.COL_DEALER_ID), Byte()))
                                SetSelectedItem(objDDL, guidVal)
                            End If

                            'populate event type dropdown
                            objDDL = CType(e.Row.Cells(Me.GRID_COL_EVENT_TYPE_IDX).FindControl(Me.GRID_CTRL_NAME_EDIT_EVENT_TYPE), DropDownList)

                            objDDL.Populate(CommonConfigManager.Current.ListManager.GetList("EVNT_TYP", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
                                {
                                    .AddBlankItem = True
                                })

                            'dv = LookupListNew.DropdownLookupList("EVNT_TYP", ElitaPlusIdentity.Current.ActiveUser.LanguageId)
                            'Me.BindListControlToDataView(objDDL, dv, , , True)

                            guidVal = New Guid(CType(dvRow(EventConfig.EventConfigSearchDV.COL_EVENT_TYPE_ID), Byte()))
                            SetSelectedItem(objDDL, guidVal)
                            Dim EventTypeCode As String = LookupListNew.GetCodeFromId(Codes.EVNT_TYP, guidVal)

                            'populate event argument type dropdown
                            objDDL = CType(e.Row.Cells(Me.GRID_COL_EVENT_ARGUMENT_IDX).FindControl(Me.GRID_CTRL_NAME_EDIT_EVENT_ARGUMENT), DropDownList)

                            Dim SelectedEventTypeDDL As DropDownList = CType(objDDL, DropDownList)

                            Dim issueList As New Collections.Generic.List(Of DataElements.ListItem)
                            If Not EventTypeCode Is Nothing Then
                                issueList = GetEventArgumentBasedOnEventType(EventTypeCode)?.ToList()
                            End If
                            'dv = GetEventArgumentBasedOnEventType(EventTypeCode)

                            If (issueList Is Nothing) Then
                                objDDL.Items.Clear()
                            Else
                                objDDL.Populate(issueList.ToArray(), New PopulateOptions() With
                                {
                                    .AddBlankItem = True
                                })
                                'Me.BindListControlToDataView(objDDL, dv, , , True)

                                guidVal = New Guid(CType(dvRow(EventConfig.EventConfigSearchDV.COL_EVENT_ARGUMENT_ID), Byte()))
                                SetSelectedItem(objDDL, guidVal)
                            End If

                            'populate coverage type dropdown
                            objDDL = CType(e.Row.Cells(Me.GRID_COL_COVERAGE_TYPE_IDX).FindControl(Me.GRID_CTRL_NAME_EDIT_COVERAGE_TYPE), DropDownList)

                            objDDL.Populate(CommonConfigManager.Current.ListManager.GetList("CTYP", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
                                {
                                    .AddBlankItem = True,
                                    .SortFunc = AddressOf .GetDescription
                                })

                            'dv = LookupListNew.GetCoverageTypeLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId)
                            'Me.BindListControlToDataView(objDDL, dv, , , True)

                            If Not dvRow(EventConfig.EventConfigSearchDV.COL_COVERAGE_TYPE_ID) Is Nothing And Not String.IsNullOrEmpty(dvRow(EventConfig.EventConfigSearchDV.COL_COVERAGE_TYPE_ID).ToString()) Then

                                ' dv = LookupListNew.DropdownLookupList("CTYP", ElitaPlusIdentity.Current.ActiveUser.LanguageId)
                                guidVal = New Guid(CType(dvRow(EventConfig.EventConfigSearchDV.COL_COVERAGE_TYPE_ID), Byte()))
                                If Not guidVal = Nothing And Not guidVal = Guid.Empty Then
                                    SetSelectedItem(objDDL, guidVal)
                                End If

                            End If

                        Else
                            CType(e.Row.Cells(Me.GRID_COL_COMPANY_GROUP_IDX).FindControl(Me.GRID_CTRL_NAME_LABLE_COMPANY_GROUP), Label).Text = dvRow(EventConfig.EventConfigSearchDV.COL_COMPANY_GROUP_DESC).ToString
                            CType(e.Row.Cells(Me.GRID_COL_COMPANY_IDX).FindControl(Me.GRID_CTRL_NAME_LABEL_COMPANY), Label).Text = dvRow(EventConfig.EventConfigSearchDV.COL_COMPANY_DESC).ToString
                            CType(e.Row.Cells(Me.GRID_COL_COUNTRY_IDX).FindControl(Me.GRID_CTRL_NAME_LABLE_COUNTRY), Label).Text = dvRow(EventConfig.EventConfigSearchDV.COL_COUNTRY_DESC).ToString
                            CType(e.Row.Cells(Me.GRID_COL_DEALER_GROUP_IDX).FindControl(Me.GRID_CTRL_NAME_LABLE_DEALER_GROUP), Label).Text = dvRow(EventConfig.EventConfigSearchDV.COL_DEALER_GROUP_DESC).ToString
                            CType(e.Row.Cells(Me.GRID_COL_DEALER_IDX).FindControl(Me.GRID_CTRL_NAME_LABLE_DEALER), Label).Text = dvRow(EventConfig.EventConfigSearchDV.COL_DEALER_DESC).ToString
                            CType(e.Row.Cells(Me.GRID_COL_PRODUCT_CODE_IDX).FindControl(Me.GRID_CTRL_NAME_LABLE_PRODUCT_CODE), Label).Text = dvRow(EventConfig.EventConfigSearchDV.COL_PRODUCT_CODE).ToString
                            CType(e.Row.Cells(Me.GRID_COL_EVENT_TYPE_IDX).FindControl(Me.GRID_CTRL_NAME_LABLE_EVENT_TYPE), Label).Text = dvRow(EventConfig.EventConfigSearchDV.COL_EVENT_TYPE_DESC).ToString
                            CType(e.Row.Cells(Me.GRID_COL_EVENT_ARGUMENT_IDX).FindControl(Me.GRID_CTRL_NAME_LABLE_EVENT_ARGUMENT), Label).Text = dvRow(EventConfig.EventConfigSearchDV.COL_EVENT_ARGUMENT_DESC).ToString
                            CType(e.Row.Cells(Me.GRID_COL_COVERAGE_TYPE_IDX).FindControl(Me.GRID_CTRL_NAME_LABLE_COVERAGE_TYPE), Label).Text = dvRow(EventConfig.EventConfigSearchDV.COL_COVERAGE_TYPE_DESC).ToString
                        End If
                    End If
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try

        End Sub

        Public Sub RowCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)

            Try
                Dim index As Integer
                If (e.CommandName = Me.EDIT_COMMAND) Then
                    index = CInt(e.CommandArgument)
                    'Do the Edit here

                    'Set the IsEditMode flag to TRUE
                    Me.State.IsEditMode = True

                    Me.State.EventConfigID = New Guid(CType(Me.Grid.Rows(index).Cells(Me.GRID_COL_EVENT_CONFIG_ID_IDX).FindControl(Me.GRID_CTRL_NAME_LABLE_EVENT_CONFIG_ID), Label).Text)
                    Me.State.MyBO = New EventConfig(Me.State.EventConfigID)

                    Me.PopulateGrid()

                    Me.State.PageIndex = Grid.PageIndex

                    'Set focus on the Code TextBox for the EditItemIndex row
                    'Me.SetFocusOnEditableFieldInGrid(Me.Grid, Me.GRID_COL_ROLE_CODE_IDX, Me.GRID_CTRL_NAME_ROLE_CODE_TXT, index)

                    Me.SetControlState()

                ElseIf (e.CommandName = Me.DELETE_COMMAND) Then
                    index = CInt(e.CommandArgument)
                    Me.State.EventConfigID = New Guid(CType(Me.Grid.Rows(index).Cells(Me.GRID_COL_EVENT_CONFIG_ID_IDX).FindControl(Me.GRID_CTRL_NAME_LABLE_EVENT_CONFIG_ID), Label).Text)
                    Me.DisplayMessage(Message.DELETE_RECORD_PROMPT, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Me.HiddenDeletePromptResponse)
                    Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Delete

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

        Public Sub PopulateGrid()
            Dim blnNewSearch As Boolean = False
            'Dim dv As DataView
            Try
                'Refresh the DataView and Call SetPageAndSelectedIndexFromGuid() to go to the Page 
                'where the most recently saved Record exists in the DataView
                With State
                    If (.searchDV Is Nothing) Then
                        .searchDV = EventConfig.getList(.searchCompanyGrp, .searchCompany, .searchCountry, .searchDealerGrp, .searchDealer, .searchProdCode, .searchCoverageType)
                        blnNewSearch = True
                    End If
                End With

                Me.State.searchDV.Sort = Me.SortDirection
                If (Me.State.IsAfterSave) Then
                    Me.State.IsAfterSave = False
                    Me.SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.EventConfigID, Me.Grid, Me.State.PageIndex)
                ElseIf (Me.State.IsEditMode) Then
                    Me.SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.EventConfigID, Me.Grid, Me.State.PageIndex, Me.State.IsEditMode)
                Else
                    Me.SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Guid.Empty, Me.Grid, Me.State.PageIndex)
                End If

                Me.Grid.AutoGenerateColumns = False
                SortAndBindGrid(blnNewSearch)

            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try

        End Sub

        Private Sub SortAndBindGrid(Optional ByVal blnShowErr As Boolean = True)

            Me.TranslateGridControls(Grid)

            If (Me.State.searchDV.Count = 0) Then
                Me.State.searchDV = Nothing
                Me.State.MyBO = New EventConfig
                State.MyBO.AddNewRowToSearchDV(Me.State.searchDV, Me.State.MyBO)
                Me.Grid.DataSource = Me.State.searchDV
                Me.Grid.DataBind()
                Me.Grid.Rows(0).Visible = False
                Me.State.IsGridAddNew = True
                Me.State.IsGridVisible = False
                If blnShowErr Then
                    Me.MasterPage.MessageController.AddInformation(ElitaPlus.ElitaPlusWebApp.Message.MSG_NO_RECORDS_FOUND, True)
                End If
            Else
                Me.Grid.Enabled = True
                Me.Grid.PageSize = Me.State.PageSize
                Me.Grid.DataSource = Me.State.searchDV
                Me.State.IsGridVisible = True
                HighLightSortColumn(Grid, Me.SortDirection)
                Me.Grid.DataBind()
            End If

            ControlMgr.SetVisibleControl(Me, Grid, Me.State.IsGridVisible)
            ControlMgr.SetVisibleControl(Me, moSearchResults, Me.State.IsGridVisible)

            Session("recCount") = Me.State.searchDV.Count

            If Me.Grid.Visible Then
                If (Me.State.IsGridAddNew) Then
                    Me.lblRecordCount.Text = (Me.State.searchDV.Count - 1) & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                Else
                    Me.lblRecordCount.Text = Me.State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                End If
            End If
            ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me, Grid)

        End Sub
#End Region

#Region "Control Handler"
        Protected Sub btnClearSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnClearSearch.Click
            Try
                ddlSearchCompanyGroup.SelectedIndex = -1
                ddlSearchCompany.SelectedIndex = -1
                ddlSearchCountry.SelectedIndex = -1
                ddlSearchDealer.SelectedIndex = -1
                ddlSearchDealerGroup.SelectedIndex = -1
                ddlSearchCoverageType.SelectedIndex = -1
                Me.txtSearchProdCode.Text = String.Empty

                Grid.EditIndex = Me.NO_ITEM_SELECTED_INDEX

                With State
                    .IsGridAddNew = False
                    .EventConfigID = Guid.Empty
                    .MyBO = Nothing
                    .searchCompanyGrp = Guid.Empty
                    .searchCompany = Guid.Empty
                    .searchCountry = Guid.Empty
                    .searchDealerGrp = Guid.Empty
                    .searchDealer = Guid.Empty
                    .searchCoverageType = Guid.Empty
                    .searchProdCode = String.Empty
                End With
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
            Try
                With State
                    .PageIndex = 0
                    .EventConfigID = Guid.Empty
                    .MyBO = Nothing
                    .IsGridVisible = True
                    .searchDV = Nothing
                    .HasDataChanged = False
                    .IsGridAddNew = False
                    'get search control value
                    .searchCompanyGrp = GetSelectedItem(ddlSearchCompanyGroup)
                    .searchCompany = GetSelectedItem(ddlSearchCompany)
                    .searchCountry = GetSelectedItem(ddlSearchCountry)
                    .searchDealerGrp = GetSelectedItem(ddlSearchDealerGroup)
                    .searchDealer = GetSelectedItem(ddlSearchDealer)
                    .searchCoverageType = GetSelectedItem(ddlSearchCoverageType)
                    .searchProdCode = txtSearchProdCode.Text.Trim.Trim
                End With
                Me.PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnNew.Click

            Try
                Me.State.IsEditMode = True
                Me.State.IsGridVisible = True
                Me.State.IsGridAddNew = True
                AddNew()
                Me.SetControlState()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try

        End Sub

        Private Sub AddNew()
            If Me.State.MyBO Is Nothing OrElse Me.State.MyBO.IsNew = False Then
                Me.State.MyBO = New EventConfig
                State.MyBO.AddNewRowToSearchDV(Me.State.searchDV, Me.State.MyBO)
            End If
            Me.State.EventConfigID = Me.State.MyBO.Id
            State.IsGridAddNew = True
            PopulateGrid()
            'Set focus on the Code TextBox for the EditItemIndex row
            Me.SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.EventConfigID, Me.Grid,
                                               Me.State.PageIndex, Me.State.IsEditMode)
            ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me, Grid)
            SetGridControls(Me.Grid, False)
        End Sub

        Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As EventArgs)

            Try
                PopulateBOFromForm()
                If (Me.State.MyBO.IsDirty) Then
                    Me.State.MyBO.Save()
                    Me.State.IsAfterSave = True
                    Me.State.IsGridAddNew = False
                    Me.MasterPage.MessageController.AddSuccess(Me.MSG_RECORD_SAVED_OK, True)
                    Me.State.searchDV = Nothing
                    Me.State.MyBO = Nothing
                    Me.ReturnFromEditing()
                Else
                    Me.MasterPage.MessageController.AddWarning(Me.MSG_RECORD_NOT_SAVED, True)
                    Me.ReturnFromEditing()
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try

        End Sub

        Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As EventArgs)
            Try
                With State
                    If .IsGridAddNew Then
                        RemoveNewRowFromSearchDV()
                        .IsGridAddNew = False
                        Grid.PageIndex = .PageIndex
                    End If
                    .EventConfigID = Guid.Empty
                    .MyBO = Nothing
                    .IsEditMode = False
                End With
                Grid.EditIndex = NO_ITEM_SELECTED_INDEX
                'Me.State.searchDV = Nothing
                PopulateGrid()
                SetControlState()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub ddlEventType_SelectedIndexChanged(sender As Object, e As EventArgs)

            Dim EventArgumentDDL As DropDownList
            Dim SelectedEventTypeDDL As DropDownList = CType(sender, DropDownList)

            Dim EventTypeCode As String = LookupListNew.GetCodeFromId(Codes.EVNT_TYP, New Guid(SelectedEventTypeDDL.SelectedValue))

            Dim issueList As New Collections.Generic.List(Of DataElements.ListItem)
            Dim EventArgs As DataElements.ListItem()
            EventArgs = GetEventArgumentBasedOnEventType(EventTypeCode)
            If Not EventArgs Is Nothing Then
                issueList = EventArgs.ToList()
            End If
            'Dim dv As DataView = GetEventArgumentBasedOnEventType(EventTypeCode)

            EventArgumentDDL = CType(Me.Grid.SelectedRow.FindControl(Me.GRID_CTRL_NAME_EDIT_EVENT_ARGUMENT), DropDownList)

            If (issueList Is Nothing) Then
                EventArgumentDDL.Items.Clear()
            Else
                EventArgumentDDL.Populate(issueList.ToArray(), New PopulateOptions() With
                    {
                        .AddBlankItem = True
                    })
            End If

            'If (dv Is Nothing) Then
            '    EventArgumentDDL.Items.Clear()
            'Else
            '    Me.BindListControlToDataView(EventArgumentDDL, dv, , , True)
            'End If

            SelectedEventTypeDDL.Focus()

        End Sub

#End Region

    End Class
End Namespace