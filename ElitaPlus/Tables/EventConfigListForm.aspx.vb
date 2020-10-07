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
                Return Grid.EditIndex > NO_ITEM_SELECTED_INDEX
            End Get
        End Property

        Public Property SortDirection() As String
            Get
                If ViewState("SortDirection") IsNot Nothing Then
                    Return ViewState("SortDirection").ToString
                Else
                    Return String.Empty
                End If

            End Get
            Set(value As String)
                ViewState("SortDirection") = value
            End Set
        End Property
#End Region

#Region "Bread Crum"
        Private Sub UpdateBreadCrum()
            MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage(PAGETAB)
            MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage(SUMMARYTITLE)
            MasterPage.UsePageTabTitleInBreadCrum = False
            MasterPage.BreadCrum = TranslationBase.TranslateLabelOrMessage(PAGETAB) + ElitaBase.Sperator + TranslationBase.TranslateLabelOrMessage(PAGETITLE)
        End Sub
#End Region

#Region "Page Events"
        Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
            MasterPage.MessageController.Clear()
            Form.DefaultButton = btnSearch.UniqueID
            Try
                If Not IsPostBack Then
                    UpdateBreadCrum()
                    'Me.SortDirection = Role.RoleSearchDV.COL_NAME_CODE
                    ControlMgr.SetVisibleControl(Me, moSearchResults, False)
                    'SetControlState()
                    State.PageIndex = 0
                    TranslateGridHeader(Grid)
                    TranslateGridControls(Grid)
                    PopulateSearchControls()
                Else
                    CheckIfComingFromDeleteConfirm()
                    BindBoPropertiesToGridHeaders()
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
            ShowMissingTranslations(MasterPage.MessageController)
        End Sub
#End Region

#Region "Helper functions"
        Protected Sub CheckIfComingFromDeleteConfirm()
            Dim confResponse As String = HiddenDeletePromptResponse.Value
            If confResponse IsNot Nothing AndAlso confResponse = MSG_VALUE_YES Then
                If Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Delete Then
                    DoDelete()
                End If
                Select Case State.ActionInProgress
                    Case ElitaPlusPage.DetailPageCommand.Delete
                End Select
            ElseIf confResponse IsNot Nothing AndAlso confResponse = MSG_VALUE_NO Then
                Select Case State.ActionInProgress
                    Case ElitaPlusPage.DetailPageCommand.Delete
                End Select
            End If
            'Clean after consuming the action
            State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
            HiddenDeletePromptResponse.Value = ""
        End Sub

        Private Sub DoDelete()
            'Do the delete here
            State.ActionInProgress = DetailPageCommand.Nothing_

            Dim obj As EventConfig = New EventConfig(State.EventConfigID)
            obj.DeleteAndSave()

            MasterPage.MessageController.AddSuccess(MSG_RECORD_DELETED_OK, True)
            State.PageIndex = Grid.PageIndex

            'Set the IsAfterSave flag to TRUE so that the Paging logic gets invoked
            State.IsAfterSave = True
            State.searchDV = Nothing
            PopulateGrid()
            State.PageIndex = Grid.PageIndex
            State.IsEditMode = False
            SetControlState()
        End Sub

        Private Sub ReturnFromEditing()

            Grid.EditIndex = NO_ROW_SELECTED_INDEX

            If (Grid.PageCount = 0) Then
                'if returning to the "1st time in" screen
                ControlMgr.SetVisibleControl(Me, Grid, False)
            Else
                ControlMgr.SetVisibleControl(Me, Grid, True)
            End If

            State.IsEditMode = False
            PopulateGrid()
            State.PageIndex = Grid.PageIndex
            SetControlState()

        End Sub

        Private Function GetEventArgumentBasedOnEventType(EventTypeCode As String) As DataElements.ListItem()
            Dim ocListContext As New ListContext
            If (EventTypeCode = "ISSUE_OPENED" _
                    OrElse EventTypeCode = "ISSUE_RESOLVED" _
                    OrElse EventTypeCode = "ISSUE_REJECTED" _
                    OrElse EventTypeCode = "ISSUE_CLOSED" _
                    OrElse EventTypeCode = "ISSUE_PENDING" _
                    OrElse EventTypeCode = "ISSUE_WAIVED" _
                    OrElse EventTypeCode = "ISSUE_REOPENED") Then

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
                        If oCancellationList IsNot Nothing Then
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
            BindBOPropertyToGridHeader(State.MyBO, "CompanyGroupId", Grid.Columns(GRID_COL_COMPANY_GROUP_IDX))
            BindBOPropertyToGridHeader(State.MyBO, "CompanyId", Grid.Columns(GRID_COL_COMPANY_IDX))
            BindBOPropertyToGridHeader(State.MyBO, "CountryId", Grid.Columns(GRID_COL_COUNTRY_IDX))
            BindBOPropertyToGridHeader(State.MyBO, "DealerGroupId", Grid.Columns(GRID_COL_DEALER_GROUP_IDX))
            BindBOPropertyToGridHeader(State.MyBO, "DealerId", Grid.Columns(GRID_COL_DEALER_IDX))
            BindBOPropertyToGridHeader(State.MyBO, "ProductCode", Grid.Columns(GRID_COL_PRODUCT_CODE_IDX))
            BindBOPropertyToGridHeader(State.MyBO, "CoverageTypeId", Grid.Columns(GRID_COL_COVERAGE_TYPE_IDX))
            BindBOPropertyToGridHeader(State.MyBO, "EventTypeId", Grid.Columns(GRID_COL_EVENT_TYPE_IDX))
            BindBOPropertyToGridHeader(State.MyBO, "EventArgumentId", Grid.Columns(GRID_COL_EVENT_ARGUMENT_IDX))
            ClearGridViewHeadersAndLabelsErrSign()
        End Sub

        Protected Sub PopulateSearchControls()
            Try
                Dim companyGroupList As New Collections.Generic.List(Of DataElements.ListItem)
                Dim ocListContext As New ListContext

                For Each company_id As Guid In ElitaPlusIdentity.Current.ActiveUser.Companies
                    ocListContext.CompanyId = company_id
                    Dim companyGroupListForCompany As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="CompanyGroupByCompany", context:=ocListContext)
                    If companyGroupListForCompany.Count > 0 Then
                        If companyGroupList IsNot Nothing Then
                            companyGroupList.AddRange(companyGroupListForCompany)
                        Else
                            companyGroupList = companyGroupListForCompany.Clone()
                        End If
                    End If
                Next

                ddlSearchCompanyGroup.Populate(companyGroupList.ToArray(), New PopulateOptions() With
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

                ddlSearchCompany.Populate(filteredCompanyList, New PopulateOptions() With
                {
                    .AddBlankItem = True
                })
                'Me.BindListControlToDataView(Me.ddlSearchCompany, LookupListNew.GetUserCompaniesLookupList(), , , True)

                ddlSearchCountry.Populate(filteredCountryList, New PopulateOptions() With
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

                ddlSearchDealerGroup.Populate(dealerGroupList.ToArray(), New PopulateOptions() With
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
                        If dealerist IsNot Nothing Then
                            dealerist.AddRange(dealerListForCompany)
                        Else
                            dealerist = dealerListForCompany.Clone()
                        End If
                    End If
                Next

                ddlSearchDealer.Populate(dealerist.ToArray(), New PopulateOptions() With
                {
                    .AddBlankItem = True
                })
                'Me.BindListControlToDataView(Me.ddlSearchDealer, LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies, False, "Code"), , , True)

                ddlSearchCoverageType.Populate(CommonConfigManager.Current.ListManager.GetList("CTYP", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
                {
                    .AddBlankItem = True,
                    .SortFunc = AddressOf PopulateOptions.GetDescription
                })
                'Me.BindListControlToDataView(Me.ddlSearchCoverageType, LookupListNew.GetCoverageTypeLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), , , True)

                If State.searchCompanyGrp <> Guid.Empty Then
                    SetSelectedItem(ddlSearchCompanyGroup, State.searchCompanyGrp)
                End If

                If State.searchCompany <> Guid.Empty Then
                    SetSelectedItem(ddlSearchCompany, State.searchCompany)
                End If

                If State.searchCountry <> Guid.Empty Then
                    SetSelectedItem(ddlSearchCountry, State.searchCountry)
                End If

                If State.searchDealerGrp <> Guid.Empty Then
                    SetSelectedItem(ddlSearchDealerGroup, State.searchDealerGrp)
                End If

                If State.searchDealer <> Guid.Empty Then
                    SetSelectedItem(ddlSearchDealer, State.searchDealer)
                End If

                If State.searchCoverageType <> Guid.Empty Then
                    SetSelectedItem(ddlSearchCoverageType, State.searchCoverageType)
                End If

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub

        Private Sub SetControlState()
            If (State.IsEditMode) Then
                ControlMgr.SetVisibleControl(Me, btnNew, False)
                ControlMgr.SetEnableControl(Me, btnSearch, False)
                ControlMgr.SetEnableControl(Me, btnClearSearch, False)
                MenuEnabled = False
                If (cboPageSize.Enabled) Then
                    ControlMgr.SetEnableControl(Me, cboPageSize, False)
                End If
            Else
                ControlMgr.SetVisibleControl(Me, btnNew, True)
                ControlMgr.SetEnableControl(Me, btnSearch, True)
                ControlMgr.SetEnableControl(Me, btnClearSearch, True)
                MenuEnabled = True
                If Not (cboPageSize.Enabled) Then
                    ControlMgr.SetEnableControl(Me, cboPageSize, True)
                End If
            End If
        End Sub

        Private Sub RemoveNewRowFromSearchDV()
            Dim rowind As Integer = NO_ITEM_SELECTED_INDEX
            With State
                If .searchDV IsNot Nothing Then
                    rowind = FindSelectedRowIndexFromGuid(.searchDV, .EventConfigID)
                End If
            End With
            If rowind <> NO_ITEM_SELECTED_INDEX Then State.searchDV.Delete(rowind)
        End Sub

        Private Function PopulateBOFromForm() As Boolean

            With State.MyBO
                .ProductCode = CType(Grid.Rows((Grid.EditIndex)).Cells(GRID_COL_PRODUCT_CODE_IDX).FindControl(GRID_CTRL_NAME_EDIT_PRODUCT_CODE), TextBox).Text.Trim.ToUpper

                Dim objDDL As DropDownList
                objDDL = CType(Grid.Rows((Grid.EditIndex)).Cells(GRID_COL_COMPANY_GROUP_IDX).FindControl(GRID_CTRL_NAME_EDIT_COMPANY_GROUP), DropDownList)
                PopulateBOProperty(State.MyBO, "CompanyGroupId", objDDL)

                objDDL = CType(Grid.Rows((Grid.EditIndex)).Cells(GRID_COL_COMPANY_IDX).FindControl(GRID_CTRL_NAME_EDIT_COMPANY), DropDownList)
                PopulateBOProperty(State.MyBO, "CompanyId", objDDL)

                objDDL = CType(Grid.Rows((Grid.EditIndex)).Cells(GRID_COL_COUNTRY_IDX).FindControl(GRID_CTRL_NAME_EDIT_COUNTRY), DropDownList)
                PopulateBOProperty(State.MyBO, "CountryId", objDDL)

                objDDL = CType(Grid.Rows((Grid.EditIndex)).Cells(GRID_COL_DEALER_GROUP_IDX).FindControl(GRID_CTRL_NAME_EDIT_DEALER_GROUP), DropDownList)
                PopulateBOProperty(State.MyBO, "DealerGroupId", objDDL)

                objDDL = CType(Grid.Rows((Grid.EditIndex)).Cells(GRID_COL_DEALER_IDX).FindControl(GRID_CTRL_NAME_EDIT_DEALER), DropDownList)
                PopulateBOProperty(State.MyBO, "DealerId", objDDL)

                objDDL = CType(Grid.Rows((Grid.EditIndex)).Cells(GRID_COL_EVENT_TYPE_IDX).FindControl(GRID_CTRL_NAME_EDIT_EVENT_TYPE), DropDownList)
                PopulateBOProperty(State.MyBO, "EventTypeId", objDDL)


                objDDL = CType(Grid.Rows((Grid.EditIndex)).Cells(GRID_COL_EVENT_ARGUMENT_IDX).FindControl(GRID_CTRL_NAME_EDIT_EVENT_ARGUMENT), DropDownList)
                PopulateBOProperty(State.MyBO, "EventArgumentId", objDDL)

                objDDL = CType(Grid.Rows((Grid.EditIndex)).Cells(GRID_COL_COVERAGE_TYPE_IDX).FindControl(GRID_CTRL_NAME_EDIT_COVERAGE_TYPE), DropDownList)
                PopulateBOProperty(State.MyBO, "CoverageTypeId", objDDL)
            End With

            If ErrCollection.Count > 0 Then
                Throw New PopulateBOErrorException
            End If
        End Function

#End Region

#Region "Grid related"
        Private Sub Grid_PageIndexChanged(sender As Object, e As System.EventArgs) Handles Grid.PageIndexChanged
            Try
                If (Not (State.IsEditMode)) Then
                    State.PageIndex = Grid.PageIndex
                    State.EventConfigID = Guid.Empty
                    PopulateGrid()
                End If
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

        Private Sub Grid_Sorting(sender As Object, e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles Grid.Sorting
            Try
                Dim spaceIndex As Integer = SortDirection.LastIndexOf(" ")

                If spaceIndex > 0 AndAlso SortDirection.Substring(0, spaceIndex).Equals(e.SortExpression) Then
                    If SortDirection.EndsWith(" ASC") Then
                        SortDirection = e.SortExpression + " DESC"
                    Else
                        SortDirection = e.SortExpression + " ASC"
                    End If
                Else
                    SortDirection = e.SortExpression + " ASC"
                End If

                State.PageIndex = 0
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub cboPageSize_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
            Try
                State.PageSize = CType(cboPageSize.SelectedValue, Integer)
                State.PageIndex = NewCurrentPageIndex(Grid, State.searchDV.Count, State.PageSize)
                Grid.PageIndex = State.PageIndex
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub Grid_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowDataBound
            Try
                Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
                Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
                Dim strECID As String
                Dim dvYesNo As DataView = LookupListNew.DropdownLookupList("YESNO", ElitaPlusIdentity.Current.ActiveUser.LanguageId, True)

                If dvRow IsNot Nothing Then
                    strECID = GetGuidStringFromByteArray(CType(dvRow(EventConfig.EventConfigSearchDV.COL_EVENT_CONFIG_ID), Byte()))
                    If itemType = ListItemType.Item OrElse itemType = ListItemType.AlternatingItem OrElse itemType = ListItemType.SelectedItem Then
                        CType(e.Row.Cells(GRID_COL_EVENT_CONFIG_ID_IDX).FindControl(GRID_CTRL_NAME_LABLE_EVENT_CONFIG_ID), Label).Text = strECID

                        If (State.IsEditMode = True AndAlso State.EventConfigID.ToString.Equals(strECID)) Then
                            CType(e.Row.Cells(GRID_COL_PRODUCT_CODE_IDX).FindControl(GRID_CTRL_NAME_EDIT_PRODUCT_CODE), TextBox).Text = dvRow(EventConfig.EventConfigSearchDV.COL_PRODUCT_CODE).ToString

                            Dim objDDL As DropDownList
                            'Dim dv As DataView
                            Dim guidVal As Guid

                            'populate company group dropdown
                            objDDL = CType(e.Row.Cells(GRID_COL_COMPANY_GROUP_IDX).FindControl(GRID_CTRL_NAME_EDIT_COMPANY_GROUP), DropDownList)

                            Dim companyGroupList As New Collections.Generic.List(Of DataElements.ListItem)
                            Dim cListContext As New ListContext

                            For Each company_id As Guid In ElitaPlusIdentity.Current.ActiveUser.Companies
                                cListContext.CompanyId = company_id
                                Dim companyGroupListForUser As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="CompanyGroupByCompany", context:=cListContext)
                                If companyGroupListForUser.Count > 0 Then
                                    If companyGroupList IsNot Nothing Then
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

                            If dvRow(EventConfig.EventConfigSearchDV.COL_COMPANY_GROUP_ID) IsNot DBNull.Value Then
                                guidVal = New Guid(CType(dvRow(EventConfig.EventConfigSearchDV.COL_COMPANY_GROUP_ID), Byte()))
                                SetSelectedItem(objDDL, guidVal)
                            End If

                            'populate company dropdown
                            objDDL = CType(e.Row.Cells(GRID_COL_COMPANY_IDX).FindControl(GRID_CTRL_NAME_EDIT_COMPANY), DropDownList)

                            Dim comListContext As New ListContext
                            comListContext.UserId = ElitaPlusIdentity.Current.ActiveUser.Id
                            Dim companyListForUser As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="UserCompanies", context:=comListContext)

                            objDDL.Populate(companyListForUser, New PopulateOptions() With
                                {
                                    .AddBlankItem = True
                                })

                            'dv = LookupListNew.GetUserCompaniesLookupList()
                            'Me.BindListControlToDataView(objDDL, dv, , , True)

                            If dvRow(EventConfig.EventConfigSearchDV.COL_COMPANY_ID) IsNot DBNull.Value Then
                                guidVal = New Guid(CType(dvRow(EventConfig.EventConfigSearchDV.COL_COMPANY_ID), Byte()))
                                SetSelectedItem(objDDL, guidVal)
                            End If

                            objDDL = CType(e.Row.Cells(GRID_COL_COUNTRY_IDX).FindControl(GRID_CTRL_NAME_EDIT_COUNTRY), DropDownList)

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

                            If dvRow(EventConfig.EventConfigSearchDV.COL_COUNTRY_ID) IsNot DBNull.Value Then
                                guidVal = New Guid(CType(dvRow(EventConfig.EventConfigSearchDV.COL_COUNTRY_ID), Byte()))
                                SetSelectedItem(objDDL, guidVal)
                            End If

                            'populate dealer group dropdown
                            objDDL = CType(e.Row.Cells(GRID_COL_DEALER_GROUP_IDX).FindControl(GRID_CTRL_NAME_EDIT_DEALER_GROUP), DropDownList)

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
                            objDDL = CType(e.Row.Cells(GRID_COL_DEALER_IDX).FindControl(GRID_CTRL_NAME_EDIT_DEALER), DropDownList)

                            Dim dealerist As New Collections.Generic.List(Of DataElements.ListItem)
                            Dim oListContext As New ListContext

                            For Each company_id As Guid In ElitaPlusIdentity.Current.ActiveUser.Companies
                                oListContext.CompanyId = company_id
                                Dim dealerListForCompany As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="DealerListByCompany", context:=oListContext)
                                If dealerListForCompany.Count > 0 Then
                                    If dealerist IsNot Nothing Then
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

                            If dvRow(EventConfig.EventConfigSearchDV.COL_DEALER_ID) IsNot DBNull.Value Then
                                guidVal = New Guid(CType(dvRow(EventConfig.EventConfigSearchDV.COL_DEALER_ID), Byte()))
                                SetSelectedItem(objDDL, guidVal)
                            End If

                            'populate event type dropdown
                            objDDL = CType(e.Row.Cells(GRID_COL_EVENT_TYPE_IDX).FindControl(GRID_CTRL_NAME_EDIT_EVENT_TYPE), DropDownList)

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
                            objDDL = CType(e.Row.Cells(GRID_COL_EVENT_ARGUMENT_IDX).FindControl(GRID_CTRL_NAME_EDIT_EVENT_ARGUMENT), DropDownList)

                            Dim SelectedEventTypeDDL As DropDownList = CType(objDDL, DropDownList)

                            Dim issueList As New Collections.Generic.List(Of DataElements.ListItem)
                            If EventTypeCode IsNot Nothing Then
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
                            objDDL = CType(e.Row.Cells(GRID_COL_COVERAGE_TYPE_IDX).FindControl(GRID_CTRL_NAME_EDIT_COVERAGE_TYPE), DropDownList)

                            objDDL.Populate(CommonConfigManager.Current.ListManager.GetList("CTYP", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
                                {
                                    .AddBlankItem = True,
                                    .SortFunc = AddressOf .GetDescription
                                })

                            'dv = LookupListNew.GetCoverageTypeLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId)
                            'Me.BindListControlToDataView(objDDL, dv, , , True)

                            If dvRow(EventConfig.EventConfigSearchDV.COL_COVERAGE_TYPE_ID) IsNot Nothing AndAlso Not String.IsNullOrEmpty(dvRow(EventConfig.EventConfigSearchDV.COL_COVERAGE_TYPE_ID).ToString()) Then

                                ' dv = LookupListNew.DropdownLookupList("CTYP", ElitaPlusIdentity.Current.ActiveUser.LanguageId)
                                guidVal = New Guid(CType(dvRow(EventConfig.EventConfigSearchDV.COL_COVERAGE_TYPE_ID), Byte()))
                                If Not guidVal = Nothing AndAlso Not guidVal = Guid.Empty Then
                                    SetSelectedItem(objDDL, guidVal)
                                End If

                            End If

                        Else
                            CType(e.Row.Cells(GRID_COL_COMPANY_GROUP_IDX).FindControl(GRID_CTRL_NAME_LABLE_COMPANY_GROUP), Label).Text = dvRow(EventConfig.EventConfigSearchDV.COL_COMPANY_GROUP_DESC).ToString
                            CType(e.Row.Cells(GRID_COL_COMPANY_IDX).FindControl(GRID_CTRL_NAME_LABEL_COMPANY), Label).Text = dvRow(EventConfig.EventConfigSearchDV.COL_COMPANY_DESC).ToString
                            CType(e.Row.Cells(GRID_COL_COUNTRY_IDX).FindControl(GRID_CTRL_NAME_LABLE_COUNTRY), Label).Text = dvRow(EventConfig.EventConfigSearchDV.COL_COUNTRY_DESC).ToString
                            CType(e.Row.Cells(GRID_COL_DEALER_GROUP_IDX).FindControl(GRID_CTRL_NAME_LABLE_DEALER_GROUP), Label).Text = dvRow(EventConfig.EventConfigSearchDV.COL_DEALER_GROUP_DESC).ToString
                            CType(e.Row.Cells(GRID_COL_DEALER_IDX).FindControl(GRID_CTRL_NAME_LABLE_DEALER), Label).Text = dvRow(EventConfig.EventConfigSearchDV.COL_DEALER_DESC).ToString
                            CType(e.Row.Cells(GRID_COL_PRODUCT_CODE_IDX).FindControl(GRID_CTRL_NAME_LABLE_PRODUCT_CODE), Label).Text = dvRow(EventConfig.EventConfigSearchDV.COL_PRODUCT_CODE).ToString
                            CType(e.Row.Cells(GRID_COL_EVENT_TYPE_IDX).FindControl(GRID_CTRL_NAME_LABLE_EVENT_TYPE), Label).Text = dvRow(EventConfig.EventConfigSearchDV.COL_EVENT_TYPE_DESC).ToString
                            CType(e.Row.Cells(GRID_COL_EVENT_ARGUMENT_IDX).FindControl(GRID_CTRL_NAME_LABLE_EVENT_ARGUMENT), Label).Text = dvRow(EventConfig.EventConfigSearchDV.COL_EVENT_ARGUMENT_DESC).ToString
                            CType(e.Row.Cells(GRID_COL_COVERAGE_TYPE_IDX).FindControl(GRID_CTRL_NAME_LABLE_COVERAGE_TYPE), Label).Text = dvRow(EventConfig.EventConfigSearchDV.COL_COVERAGE_TYPE_DESC).ToString
                        End If
                    End If
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub

        Public Sub RowCommand(source As System.Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs)

            Try
                Dim index As Integer
                If (e.CommandName = EDIT_COMMAND) Then
                    index = CInt(e.CommandArgument)
                    'Do the Edit here

                    'Set the IsEditMode flag to TRUE
                    State.IsEditMode = True

                    State.EventConfigID = New Guid(CType(Grid.Rows(index).Cells(GRID_COL_EVENT_CONFIG_ID_IDX).FindControl(GRID_CTRL_NAME_LABLE_EVENT_CONFIG_ID), Label).Text)
                    State.MyBO = New EventConfig(State.EventConfigID)

                    PopulateGrid()

                    State.PageIndex = Grid.PageIndex

                    'Set focus on the Code TextBox for the EditItemIndex row
                    'Me.SetFocusOnEditableFieldInGrid(Me.Grid, Me.GRID_COL_ROLE_CODE_IDX, Me.GRID_CTRL_NAME_ROLE_CODE_TXT, index)

                    SetControlState()

                ElseIf (e.CommandName = DELETE_COMMAND) Then
                    index = CInt(e.CommandArgument)
                    State.EventConfigID = New Guid(CType(Grid.Rows(index).Cells(GRID_COL_EVENT_CONFIG_ID_IDX).FindControl(GRID_CTRL_NAME_LABLE_EVENT_CONFIG_ID), Label).Text)
                    DisplayMessage(Message.DELETE_RECORD_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenDeletePromptResponse)
                    State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Delete

                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub

        Public Sub RowCreated(sender As System.Object, e As System.Web.UI.WebControls.GridViewRowEventArgs)
            Try
                BaseItemCreated(sender, e)
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
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

                State.searchDV.Sort = SortDirection
                If (State.IsAfterSave) Then
                    State.IsAfterSave = False
                    SetPageAndSelectedIndexFromGuid(State.searchDV, State.EventConfigID, Grid, State.PageIndex)
                ElseIf (State.IsEditMode) Then
                    SetPageAndSelectedIndexFromGuid(State.searchDV, State.EventConfigID, Grid, State.PageIndex, State.IsEditMode)
                Else
                    SetPageAndSelectedIndexFromGuid(State.searchDV, Guid.Empty, Grid, State.PageIndex)
                End If

                Grid.AutoGenerateColumns = False
                SortAndBindGrid(blnNewSearch)

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub

        Private Sub SortAndBindGrid(Optional ByVal blnShowErr As Boolean = True)

            TranslateGridControls(Grid)

            If (State.searchDV.Count = 0) Then
                State.searchDV = Nothing
                State.MyBO = New EventConfig
                State.MyBO.AddNewRowToSearchDV(State.searchDV, State.MyBO)
                Grid.DataSource = State.searchDV
                Grid.DataBind()
                Grid.Rows(0).Visible = False
                State.IsGridAddNew = True
                State.IsGridVisible = False
                If blnShowErr Then
                    MasterPage.MessageController.AddInformation(ElitaPlus.ElitaPlusWebApp.Message.MSG_NO_RECORDS_FOUND, True)
                End If
            Else
                Grid.Enabled = True
                Grid.PageSize = State.PageSize
                Grid.DataSource = State.searchDV
                State.IsGridVisible = True
                HighLightSortColumn(Grid, SortDirection)
                Grid.DataBind()
            End If

            ControlMgr.SetVisibleControl(Me, Grid, State.IsGridVisible)
            ControlMgr.SetVisibleControl(Me, moSearchResults, State.IsGridVisible)

            Session("recCount") = State.searchDV.Count

            If Grid.Visible Then
                If (State.IsGridAddNew) Then
                    lblRecordCount.Text = (State.searchDV.Count - 1) & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                Else
                    lblRecordCount.Text = State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                End If
            End If
            ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me, Grid)

        End Sub
#End Region

#Region "Control Handler"
        Protected Sub btnClearSearch_Click(sender As Object, e As EventArgs) Handles btnClearSearch.Click
            Try
                ddlSearchCompanyGroup.SelectedIndex = -1
                ddlSearchCompany.SelectedIndex = -1
                ddlSearchCountry.SelectedIndex = -1
                ddlSearchDealer.SelectedIndex = -1
                ddlSearchDealerGroup.SelectedIndex = -1
                ddlSearchCoverageType.SelectedIndex = -1
                txtSearchProdCode.Text = String.Empty

                Grid.EditIndex = NO_ITEM_SELECTED_INDEX

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
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
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
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click

            Try
                State.IsEditMode = True
                State.IsGridVisible = True
                State.IsGridAddNew = True
                AddNew()
                SetControlState()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub

        Private Sub AddNew()
            If State.MyBO Is Nothing OrElse State.MyBO.IsNew = False Then
                State.MyBO = New EventConfig
                State.MyBO.AddNewRowToSearchDV(State.searchDV, State.MyBO)
            End If
            State.EventConfigID = State.MyBO.Id
            State.IsGridAddNew = True
            PopulateGrid()
            'Set focus on the Code TextBox for the EditItemIndex row
            SetPageAndSelectedIndexFromGuid(State.searchDV, State.EventConfigID, Grid,
                                               State.PageIndex, State.IsEditMode)
            ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me, Grid)
            SetGridControls(Grid, False)
        End Sub

        Protected Sub btnSave_Click(sender As Object, e As EventArgs)

            Try
                PopulateBOFromForm()
                If (State.MyBO.IsDirty) Then
                    State.MyBO.Save()
                    State.IsAfterSave = True
                    State.IsGridAddNew = False
                    MasterPage.MessageController.AddSuccess(MSG_RECORD_SAVED_OK, True)
                    State.searchDV = Nothing
                    State.MyBO = Nothing
                    ReturnFromEditing()
                Else
                    MasterPage.MessageController.AddWarning(MSG_RECORD_NOT_SAVED, True)
                    ReturnFromEditing()
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub

        Protected Sub btnCancel_Click(sender As Object, e As EventArgs)
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
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub ddlEventType_SelectedIndexChanged(sender As Object, e As EventArgs)

            Dim EventArgumentDDL As DropDownList
            Dim SelectedEventTypeDDL As DropDownList = CType(sender, DropDownList)

            Dim EventTypeCode As String = LookupListNew.GetCodeFromId(Codes.EVNT_TYP, New Guid(SelectedEventTypeDDL.SelectedValue))

            Dim issueList As New Collections.Generic.List(Of DataElements.ListItem)
            Dim EventArgs As DataElements.ListItem()
            EventArgs = GetEventArgumentBasedOnEventType(EventTypeCode)
            If EventArgs IsNot Nothing Then
                issueList = EventArgs.ToList()
            End If
            'Dim dv As DataView = GetEventArgumentBasedOnEventType(EventTypeCode)

            EventArgumentDDL = CType(Grid.SelectedRow.FindControl(GRID_CTRL_NAME_EDIT_EVENT_ARGUMENT), DropDownList)

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