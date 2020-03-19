Public Class QuestionSetConfigListForm
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

End Class

Imports System.Globalization
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports System.Threading
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms
Namespace Tables

    Partial Public Class QuestionSetConfigListForm
        Inherits ElitaPlusSearchPage

#Region "Constants"
        Public Const URL As String = "Tables/QuestionSetConfigListForm.aspx"
        Public Const PAGETITLE As String = "QUESTION_SET_CONFIG"
        Public Const PAGETAB As String = "ADMIN"
        Public Const SUMMARYTITLE As String = "SEARCH"

        Private Const GRID_COL_QUESTION_SET_CONFIG_ID_IDX As Integer = 0

        Private Const GRID_CTRL_NAME_LABLE_QUESTION_SET_CONFIG_ID As String = "lblQuestionSetConfigID"
        'Private Const GRID_CTRL_NAME_LABLE_EVENT_ARGUMENT As String = "lblEventArgument"

        Private Const SELECT_COMMAND As String = "SelectAction"
        Private Const NO_ROW_SELECTED_INDEX As Integer = -1
#End Region

#Region "Page State"
        Private IsReturningFromChild As Boolean = False
        Private ChildMessage As String = String.Empty
        Class MyState
            Public QuestionSetConfigID As Guid
            Public PageIndex As Integer = 0
            Public PageSize As Integer = DEFAULT_NEW_UI_PAGE_SIZE
            Public searchDV As QuestionSetConfig.QuestionSetConfigSearchDV = Nothing
            Public HasDataChanged As Boolean
            Public IsGridVisible As Boolean = False
            Public bnoRow As Boolean = False
            Public SortExpression As String = QuestionSetConfig.QuestionSetConfigSearchDV.QUESTION_SET_CODE_DESC
            Public searchCompanyGrp As Guid = Guid.Empty
            Public searchCompany As Guid = Guid.Empty
            Public searchDealerGrp As Guid = Guid.Empty
            Public searchDealer As Guid = Guid.Empty
            Public searchProdCode As String = ""
            Public searchRiskType As Guid = Guid.Empty
            Public searchCoverageType As Guid = Guid.Empty
            Public searchCoverageConsqDamage As Guid = Guid.Empty
            Public searchQuestionSetCode As String = ""
            Public searchPurposeCode As String = ""

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

#Region "Page event"
        Private Sub UpdateBreadCrum()
            Me.MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage(PAGETAB)
            Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage(SUMMARYTITLE)
            Me.MasterPage.UsePageTabTitleInBreadCrum = False
            Me.MasterPage.BreadCrum = TranslationBase.TranslateLabelOrMessage(PAGETAB) + ElitaBase.Sperator + TranslationBase.TranslateLabelOrMessage(PAGETITLE)
        End Sub

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            Try
                Me.MasterPage.MessageController.Clear()
                If Not Me.IsPostBack Then
                    Me.SortDirection = Me.State.SortExpression

                    UpdateBreadCrum()

                    Me.SetDefaultButton(Me.txtSearchProdCode, btnSearch)
                    Me.SetDefaultButton(Me.ddlSearchCompanyGroup, btnSearch)
                    Me.SetDefaultButton(Me.ddlSearchCompany, btnSearch)
                    Me.SetDefaultButton(Me.ddlSearchCountry, btnSearch)
                    Me.SetDefaultButton(Me.ddlSearchDealer, btnSearch)
                    Me.SetDefaultButton(Me.ddlSearchEventType, btnSearch)
                    Me.SetDefaultButton(Me.ddlSearchTask, btnSearch)
                    Me.SetDefaultButton(Me.ddlSearchCoverageType, btnSearch)
                    ControlMgr.SetVisibleControl(Me, moSearchResults, False)
                    PopulateSearchFieldsFromState()
                    Me.TranslateGridHeader(Grid)
                    cboPageSize.SelectedValue = CType(Me.State.PageSize, String)
                    Grid.PageSize = Me.State.PageSize
                    If Me.State.IsGridVisible Then
                        Me.PopulateGrid()
                    End If
                    Me.SetGridItemStyleColor(Me.Grid)

                    If IsReturningFromChild AndAlso ChildMessage.Trim <> String.Empty Then
                        Me.MasterPage.MessageController.AddSuccess(ChildMessage)
                    End If
                End If

            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
            Me.ShowMissingTranslations(Me.MasterPage.MessageController)
        End Sub

        Private Sub Page_PageReturn(ByVal ReturnFromUrl As String, ByVal ReturnPar As Object) Handles MyBase.PageReturn
            Try
                Me.MenuEnabled = True
                Me.IsReturningFromChild = True
                Dim retObj As EventTaskForm.ReturnType = CType(ReturnPar, EventTaskForm.ReturnType)
                Me.State.HasDataChanged = retObj.HasDataChanged
                If Not retObj Is Nothing AndAlso retObj.HasDataChanged Then
                    Me.State.searchDV = Nothing
                End If
                'Me.State.IsGridVisible = True
                ' Me.TranslateGridHeader(Grid)
                'Me.TranslateGridControls(Grid)
                Select Case retObj.LastOperation
                    Case ElitaPlusPage.DetailPageCommand.Back
                        If Not retObj Is Nothing Then
                            If Not retObj.EditingBo.IsNew Then
                                Me.State.EventTaskID = retObj.EditingBo.Id
                            End If
                        End If
                    Case ElitaPlusPage.DetailPageCommand.Delete
                        ChildMessage = Message.DELETE_RECORD_CONFIRMATION
                End Select
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub
#End Region

#Region "Button Clicks "
        Private Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
            callPage(EventTaskForm.URL)
        End Sub

        Private Sub btnClearSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClearSearch.Click
            Try
                ddlSearchCompanyGroup.SelectedIndex = -1
                ddlSearchCompany.SelectedIndex = -1
                ddlSearchCountry.SelectedIndex = -1
                ddlSearchDealer.SelectedIndex = -1
                ddlSearchDealerGroup.SelectedIndex = -1
                ddlSearchEventType.SelectedIndex = -1
                ddlSearchTask.SelectedIndex = -1
                ddlSearchCoverageType.SelectedIndex = -1
                Me.txtSearchProdCode.Text = String.Empty

                Grid.EditIndex = Me.NO_ITEM_SELECTED_INDEX

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
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
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
                Me.PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
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

                Me.ddlSearchCompany.Populate(filteredList, New PopulateOptions() With
                  {
                   .AddBlankItem = True
                  })
                ' Me.BindListControlToDataView(Me.ddlSearchCountry, LookupListNew.GetUserCountriesLookupList(), , , True)
                Dim countryLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList(ListCodes.Country, Thread.CurrentPrincipal.GetLanguageCode())
                Dim cList As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Countries

                Dim filteredcountryList As ListItem() = (From x In countryLkl
                                                         Where cList.Contains(x.ListItemId)
                                                         Select x).ToArray()

                Me.ddlSearchCountry.Populate(filteredcountryList, New PopulateOptions() With
                  {
                   .AddBlankItem = True
                  })

                Dim oDealerGroupList = GetDealerGroupListByCompanyForUser()
                Me.ddlSearchDealerGroup.Populate(oDealerGroupList, New PopulateOptions() With
                                                     {
                                                     .AddBlankItem = True,
                                                     .SortFunc = AddressOf .GetCode
                                                     })

                ' Me.BindListControlToDataView(Me.ddlSearchDealer, LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies, False, "Code"), , , True)
                Dim oDealerList = GetDealerListByCompanyForUser()
                Me.ddlSearchDealer.Populate(oDealerList, New PopulateOptions() With
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
                If Me.State.searchEventType <> Guid.Empty Then
                    Me.SetSelectedItem(ddlSearchEventType, Me.State.searchEventType)
                End If
                If Me.State.searchTask <> Guid.Empty Then
                    Me.SetSelectedItem(ddlSearchTask, Me.State.searchTask)
                End If
                If Me.State.searchCoverageType <> Guid.Empty Then
                    Me.SetSelectedItem(ddlSearchCoverageType, Me.State.searchCoverageType)
                End If
                txtSearchProdCode.Text = Me.State.searchProdCode
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
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
                    If Not oDealerList Is Nothing Then
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
                    .searchDV.Sort = Me.SortDirection

                    Me.Grid.AutoGenerateColumns = False

                    SetPageAndSelectedIndexFromGuid(.searchDV, .EventTaskID, Me.Grid, Me.State.PageIndex)
                    Me.SortAndBindGrid(blnNewSearch)
                End If
            End With

        End Sub

        Private Sub SortAndBindGrid(Optional ByVal blnShowErr As Boolean = True)
            Me.State.PageIndex = Me.Grid.PageIndex

            If (Me.State.searchDV.Count = 0) Then
                Me.State.bnoRow = True
                Me.State.searchDV = Nothing
                Dim objNew As EventTask = New EventTask
                EventTask.AddNewRowToSearchDV(Me.State.searchDV, objNew)
                Me.Grid.DataSource = Me.State.searchDV
                Me.Grid.DataBind()
                Me.Grid.Rows(0).Visible = False
                Me.State.IsGridVisible = False
                If blnShowErr Then
                    Me.MasterPage.MessageController.AddInformation(ElitaPlus.ElitaPlusWebApp.Message.MSG_NO_RECORDS_FOUND, True)
                End If
            Else
                State.IsGridVisible = True
                Me.State.bnoRow = False
                Me.Grid.Enabled = True
                Me.Grid.DataSource = Me.State.searchDV
                HighLightSortColumn(Grid, Me.SortDirection, True)
                Me.Grid.DataBind()
            End If

            If Not Grid.BottomPagerRow.Visible Then Grid.BottomPagerRow.Visible = True

            ControlMgr.SetVisibleControl(Me, moSearchResults, Me.State.IsGridVisible)

            Session("recCount") = Me.State.searchDV.Count

            If Me.State.IsGridVisible Then
                Me.lblRecordCount.Text = Me.State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
            End If

        End Sub
#End Region

#Region "Grid related"
        Private Sub Grid_PageIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Grid.PageIndexChanged
            Try
                Me.State.PageIndex = Grid.PageIndex
                Me.State.EventTaskID = Guid.Empty
                Me.PopulateGrid()
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

        Public Sub RowCreated(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
            Try
                BaseItemCreated(sender, e)
            Catch ex As Exception
                HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Public Sub RowCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)

            Try
                Dim index As Integer
                If (e.CommandName = Me.SELECT_COMMAND) Then
                    index = CInt(e.CommandArgument)

                    Me.State.EventTaskID = New Guid(CType(Me.Grid.Rows(index).Cells(Me.GRID_COL_EVENT_TASK_ID_IDX).FindControl(Me.GRID_CTRL_NAME_LABLE_EVENT_TASK_ID), Label).Text)
                    'SetSession()
                    Me.callPage(EventTaskForm.URL, Me.State.EventTaskID)
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try

        End Sub
#End Region


    End Class
End Namespace