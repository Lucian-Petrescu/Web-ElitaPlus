Imports System.Globalization
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports System.Threading
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms
Namespace Tables

    Partial Public Class EventTaskListForm
        Inherits ElitaPlusSearchPage

#Region "Constants"
        Public Const URL As String = "Tables/EventTaslListForm.aspx"
        Public Const PAGETITLE As String = "EVENT_TASK"
        Public Const PAGETAB As String = "ADMIN"
        Public Const SUMMARYTITLE As String = "SEARCH"

        Private Const GRID_COL_EVENT_TASK_ID_IDX As Integer = 0

        Private Const MSG_CONFIRM_PROMPT As String = "MSG_CONFIRM_PROMPT"
        Private Const MSG_RECORD_DELETED_OK As String = "MSG_RECORD_DELETED_OK"
        Private Const MSG_RECORD_SAVED_OK As String = "MSG_RECORD_SAVED_OK"
        Private Const MSG_RECORD_NOT_SAVED As String = "MSG_RECORD_NOT_SAVED"

        Private Const GRID_CTRL_NAME_LABLE_EVENT_TASK_ID As String = "lblEventTaskID"
        Private Const GRID_CTRL_NAME_LABLE_EVENT_ARGUMENT As String = "lblEventArgument"

        Private Const SELECT_COMMAND As String = "SelectAction"
        Private Const NO_ROW_SELECTED_INDEX As Integer = -1
#End Region

#Region "Page State"
        Private IsReturningFromChild As Boolean = False
        Private ChildMessage As String = String.Empty
        Class MyState
            Public EventTaskID As Guid
            Public PageIndex As Integer = 0
            Public PageSize As Integer = DEFAULT_NEW_UI_PAGE_SIZE
            Public searchDV As EventTask.EventTaskSearchDV = Nothing
            Public HasDataChanged As Boolean
            Public IsGridVisible As Boolean = False
            Public bnoRow As Boolean = False
            Public SortExpression As String = EventTask.EventTaskSearchDV.COL_EVENT_TYPE_DESC
            Public searchCompanyGrp As Guid = Guid.Empty
            Public searchCompany As Guid = Guid.Empty
            Public searchCountry As Guid = Guid.Empty
            Public searchDealerGrp As Guid = Guid.Empty
            Public searchDealer As Guid = Guid.Empty
            Public searchProdCode As String = ""
            Public searchEventType As Guid = Guid.Empty
            Public searchTask As Guid = Guid.Empty
            Public searchCoverageType As Guid = Guid.Empty
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

#Region "Page event"
        Private Sub UpdateBreadCrum()
            MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage(PAGETAB)
            MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage(SUMMARYTITLE)
            MasterPage.UsePageTabTitleInBreadCrum = False
            MasterPage.BreadCrum = TranslationBase.TranslateLabelOrMessage(PAGETAB) + ElitaBase.Sperator + TranslationBase.TranslateLabelOrMessage(PAGETITLE)
        End Sub

        Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
            Try
                MasterPage.MessageController.Clear()
                If Not IsPostBack Then
                    SortDirection = State.SortExpression

                    UpdateBreadCrum()

                    SetDefaultButton(txtSearchProdCode, btnSearch)
                    SetDefaultButton(ddlSearchCompanyGroup, btnSearch)
                    SetDefaultButton(ddlSearchCompany, btnSearch)
                    SetDefaultButton(ddlSearchCountry, btnSearch)
                    SetDefaultButton(ddlSearchDealer, btnSearch)
                    SetDefaultButton(ddlSearchEventType, btnSearch)
                    SetDefaultButton(ddlSearchTask, btnSearch)
                    SetDefaultButton(ddlSearchCoverageType, btnSearch)
                    ControlMgr.SetVisibleControl(Me, moSearchResults, False)
                    PopulateSearchFieldsFromState()
                    TranslateGridHeader(Grid)
                    cboPageSize.SelectedValue = CType(State.PageSize, String)
                    Grid.PageSize = State.PageSize
                    If State.IsGridVisible Then
                        PopulateGrid()
                    End If
                    SetGridItemStyleColor(Grid)

                    If IsReturningFromChild AndAlso ChildMessage.Trim <> String.Empty Then
                        MasterPage.MessageController.AddSuccess(ChildMessage)
                    End If
                End If

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
            ShowMissingTranslations(MasterPage.MessageController)
        End Sub

        Private Sub Page_PageReturn(ReturnFromUrl As String, ReturnPar As Object) Handles MyBase.PageReturn
            Try
                MenuEnabled = True
                IsReturningFromChild = True
                Dim retObj As EventTaskForm.ReturnType = CType(ReturnPar, EventTaskForm.ReturnType)
                State.HasDataChanged = retObj.HasDataChanged
                If retObj IsNot Nothing AndAlso retObj.HasDataChanged Then
                    State.searchDV = Nothing
                End If
                'Me.State.IsGridVisible = True
                ' Me.TranslateGridHeader(Grid)
                'Me.TranslateGridControls(Grid)
                Select Case retObj.LastOperation
                    Case ElitaPlusPage.DetailPageCommand.Back
                        If retObj IsNot Nothing Then
                            If Not retObj.EditingBo.IsNew Then
                                State.EventTaskID = retObj.EditingBo.Id
                            End If
                        End If
                    Case ElitaPlusPage.DetailPageCommand.Delete
                        ChildMessage = Message.DELETE_RECORD_CONFIRMATION
                End Select
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub
#End Region

#Region "Button Clicks "
        Private Sub btnNew_Click(sender As Object, e As System.EventArgs) Handles btnNew.Click
            callPage(EventTaskForm.URL)
        End Sub

        Private Sub btnClearSearch_Click(sender As Object, e As System.EventArgs) Handles btnClearSearch.Click
            Try
                ddlSearchCompanyGroup.SelectedIndex = -1
                ddlSearchCompany.SelectedIndex = -1
                ddlSearchCountry.SelectedIndex = -1
                ddlSearchDealer.SelectedIndex = -1
                ddlSearchDealerGroup.SelectedIndex = -1
                ddlSearchEventType.SelectedIndex = -1
                ddlSearchTask.SelectedIndex = -1
                ddlSearchCoverageType.SelectedIndex = -1
                txtSearchProdCode.Text = String.Empty

                Grid.EditIndex = NO_ITEM_SELECTED_INDEX

                With State
                    .EventTaskID = Guid.Empty
                    .searchCompanyGrp = Guid.Empty
                    .searchCompany = Guid.Empty
                    .searchCountry = Guid.Empty
                    .searchDealer = Guid.Empty
                    .searchProdCode = String.Empty
                    .searchTask = Guid.Empty
                    .searchEventType = Guid.Empty
                    .searchCoverageType = Guid.Empty
                End With
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnSearch_Click(sender As Object, e As System.EventArgs) Handles btnSearch.Click
            Try
                With State
                    .PageIndex = 0
                    .EventTaskID = Guid.Empty
                    .IsGridVisible = True
                    .searchDV = Nothing
                    .HasDataChanged = False
                    'get search control value
                    .searchCompanyGrp = GetSelectedItem(ddlSearchCompanyGroup)
                    .searchCompany = GetSelectedItem(ddlSearchCompany)
                    .searchCountry = GetSelectedItem(ddlSearchCountry)
                    .searchDealerGrp = GetSelectedItem(ddlSearchDealerGroup)
                    .searchDealer = GetSelectedItem(ddlSearchDealer)
                    .searchProdCode = txtSearchProdCode.Text.Trim.Trim
                    .searchEventType = GetSelectedItem(ddlSearchEventType)
                    .searchTask = GetSelectedItem(ddlSearchTask)
                    .searchCoverageType = GetSelectedItem(ddlSearchCoverageType)
                End With
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub
#End Region

#Region "Helper Functions"
        Protected Sub PopulateSearchFieldsFromState()
            Try
                Dim dv As DataView = LookupListNew.GetUserCompanyGroupList()
                ddlSearchCompanyGroup.Items.Add(New System.Web.UI.WebControls.ListItem("", Guid.Empty.ToString))
                If dv.Count > 0 Then
                    ddlSearchCompanyGroup.Items.Add(New System.Web.UI.WebControls.ListItem(dv(0)("DESCRIPTION").ToString, New Guid(CType(dv(0)("ID"), Byte())).ToString))
                End If
                'Me.BindListControlToDataView(Me.ddlSearchCompany, LookupListNew.GetUserCompaniesLookupList(), , , True)
                Dim compLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("Company", Thread.CurrentPrincipal.GetLanguageCode())
                Dim list As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Companies

                Dim filteredList As ListItem() = (From x In compLkl
                                                  Where list.Contains(x.ListItemId)
                                                  Select x).ToArray()

                ddlSearchCompany.Populate(filteredList, New PopulateOptions() With
                  {
                   .AddBlankItem = True
                  })
                ' Me.BindListControlToDataView(Me.ddlSearchCountry, LookupListNew.GetUserCountriesLookupList(), , , True)
                Dim countryLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList(ListCodes.Country, Thread.CurrentPrincipal.GetLanguageCode())
                Dim cList As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Countries

                Dim filteredcountryList As ListItem() = (From x In countryLkl
                                                         Where cList.Contains(x.ListItemId)
                                                         Select x).ToArray()

                ddlSearchCountry.Populate(filteredcountryList, New PopulateOptions() With
                  {
                   .AddBlankItem = True
                  })

                Dim oDealerGroupList = GetDealerGroupListByCompanyForUser()
                ddlSearchDealerGroup.Populate(oDealerGroupList, New PopulateOptions() With
                                                     {
                                                     .AddBlankItem = True,
                                                     .SortFunc = AddressOf .GetCode
                                                     })

                ' Me.BindListControlToDataView(Me.ddlSearchDealer, LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies, False, "Code"), , , True)
                Dim oDealerList = GetDealerListByCompanyForUser()
                ddlSearchDealer.Populate(oDealerList, New PopulateOptions() With
                                           {
                                            .AddBlankItem = True
                                           })
                '   dv = Task.getList(String.Empty, String.Empty)
                'BindListControlToDataView(ddlSearchTask, dv, Task.TaskSearchDV.COL_DESCRIPTION, Task.TaskSearchDV.COL_TASK_ID, True)
                Dim taskLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("GetTaskList", Thread.CurrentPrincipal.GetLanguageCode())
                'Dim filterlist As ListItem() = (From x In taskLkl
                '                                Where x.Code = String.Empty And x.Translation = String.Empty
                '                                Select x).ToArray()
                ddlSearchTask.Populate(taskLkl, New PopulateOptions() With
                                         {
                                          .AddBlankItem = True
                                         })
                'dv = LookupListNew.DropdownLookupList("EVNT_TYP", ElitaPlusIdentity.Current.ActiveUser.LanguageId)
                ' Me.BindListControlToDataView(ddlSearchEventType, dv, , , True)
                ddlSearchEventType.Populate(CommonConfigManager.Current.ListManager.GetList("EVNT_TYP", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
                                          {
                                           .AddBlankItem = True
                                          })
                ' dv = LookupListNew.DropdownLookupList("CTYP", ElitaPlusIdentity.Current.ActiveUser.LanguageId)
                '  Me.BindListControlToDataView(ddlSearchCoverageType, dv, , , True)
                ddlSearchCoverageType.Populate(CommonConfigManager.Current.ListManager.GetList("CTYP", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
                                          {
                                           .AddBlankItem = True
                                          })
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
                If State.searchEventType <> Guid.Empty Then
                    SetSelectedItem(ddlSearchEventType, State.searchEventType)
                End If
                If State.searchTask <> Guid.Empty Then
                    SetSelectedItem(ddlSearchTask, State.searchTask)
                End If
                If State.searchCoverageType <> Guid.Empty Then
                    SetSelectedItem(ddlSearchCoverageType, State.searchCoverageType)
                End If
                txtSearchProdCode.Text = State.searchProdCode
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

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
                    If oDealerList IsNot Nothing Then
                        oDealerList.AddRange(oDealerListForCompany)
                    Else
                        oDealerList = oDealerListForCompany.Clone()
                    End If

                End If
            Next

            Return oDealerList.ToArray()

        End Function

        Private Function GetDealerGroupListByCompanyForUser() As ListItem()
            Dim Index As Integer
            Dim oListContext As New ListContext

            Dim UserCompanies As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Companies

            Dim oDealerGroupList As New Collections.Generic.List(Of ListItem)

            For Index = 0 To UserCompanies.Count - 1
                'UserCompanyList &= ",'" & GuidControl.GuidToHexString(UserCompanyies(Index))
                oListContext.CompanyId = UserCompanies(Index)
                Dim oDealerGroupListForCompany As ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="DealerGroupByCompany", context:=oListContext)
                If oDealerGroupListForCompany.Count > 0 Then
                    If oDealerGroupList IsNot Nothing Then
                        oDealerGroupList.AddRange(oDealerGroupListForCompany)
                    Else
                        oDealerGroupList = oDealerGroupListForCompany.Clone()
                    End If

                End If
            Next

            Return oDealerGroupList.ToArray()

        End Function

        Public Sub PopulateGrid()
            Dim blnNewSearch As Boolean = False
            With State
                If ((.searchDV Is Nothing) OrElse (.HasDataChanged)) Then
                    .searchDV = EventTask.getList(.searchCompanyGrp, .searchCompany, .searchCountry, .searchDealerGrp, .searchDealer,
                                                  .searchProdCode, .searchEventType, .searchTask, .searchCoverageType)
                    blnNewSearch = True
                End If
                If Not (.searchDV Is Nothing) Then
                    .searchDV.Sort = SortDirection

                    Grid.AutoGenerateColumns = False

                    SetPageAndSelectedIndexFromGuid(.searchDV, .EventTaskID, Grid, State.PageIndex)
                    SortAndBindGrid(blnNewSearch)
                End If
            End With

        End Sub

        Private Sub SortAndBindGrid(Optional ByVal blnShowErr As Boolean = True)
            State.PageIndex = Grid.PageIndex

            If (State.searchDV.Count = 0) Then
                State.bnoRow = True
                State.searchDV = Nothing
                Dim objNew As EventTask = New EventTask
                EventTask.AddNewRowToSearchDV(State.searchDV, objNew)
                Grid.DataSource = State.searchDV
                Grid.DataBind()
                Grid.Rows(0).Visible = False
                State.IsGridVisible = False
                If blnShowErr Then
                    MasterPage.MessageController.AddInformation(ElitaPlus.ElitaPlusWebApp.Message.MSG_NO_RECORDS_FOUND, True)
                End If
            Else
                State.IsGridVisible = True
                State.bnoRow = False
                Grid.Enabled = True
                Grid.DataSource = State.searchDV
                HighLightSortColumn(Grid, SortDirection, True)
                Grid.DataBind()
            End If

            If Not Grid.BottomPagerRow.Visible Then Grid.BottomPagerRow.Visible = True

            ControlMgr.SetVisibleControl(Me, moSearchResults, State.IsGridVisible)

            Session("recCount") = State.searchDV.Count

            If State.IsGridVisible Then
                lblRecordCount.Text = State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
            End If

        End Sub
#End Region

#Region "Grid related"
        Private Sub Grid_PageIndexChanged(sender As Object, e As System.EventArgs) Handles Grid.PageIndexChanged
            Try
                State.PageIndex = Grid.PageIndex
                State.EventTaskID = Guid.Empty
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

        Public Sub RowCreated(sender As System.Object, e As System.Web.UI.WebControls.GridViewRowEventArgs)
            Try
                BaseItemCreated(sender, e)
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Public Sub RowCommand(source As System.Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs)

            Try
                Dim index As Integer
                If (e.CommandName = SELECT_COMMAND) Then
                    index = CInt(e.CommandArgument)

                    State.EventTaskID = New Guid(CType(Grid.Rows(index).Cells(GRID_COL_EVENT_TASK_ID_IDX).FindControl(GRID_CTRL_NAME_LABLE_EVENT_TASK_ID), Label).Text)
                    'SetSession()
                    callPage(EventTaskForm.URL, State.EventTaskID)
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub
#End Region


    End Class
End Namespace