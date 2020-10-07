Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports Assurant.Elita.Web.Forms
Imports System.Threading
Namespace Tables

    Partial Public Class ClaimStageListForm
        Inherits ElitaPlusSearchPage

#Region "Constants"
        Public Const URL As String = "Tables/ClaimStageListForm.aspx"
        Public Const PAGETITLE As String = "CLAIM_STAGE"
        Public Const PAGETAB As String = "TABLES"
        Public Const SUMMARYTITLE As String = "SEARCH"

        Private Const GRID_COL_CLAIM_STAGE_ID_IDX As Integer = 0

        Private Const MSG_CONFIRM_PROMPT As String = "MSG_CONFIRM_PROMPT"
        Private Const MSG_RECORD_DELETED_OK As String = "MSG_RECORD_DELETED_OK"
        Private Const MSG_RECORD_SAVED_OK As String = "MSG_RECORD_SAVED_OK"
        Private Const MSG_RECORD_NOT_SAVED As String = "MSG_RECORD_NOT_SAVED"

        Private Const GRID_CTRL_NAME_LABLE_CLAIM_STAGE_ID As String = "lblClaimStageID"

        Private Const SELECT_COMMAND As String = "SelectAction"

        Private Const NO_ROW_SELECTED_INDEX As Integer = -1
#End Region

#Region "Page State"
        Private IsReturningFromChild As Boolean = False
        Private ChildMessage As String = String.Empty
        Class MyState
            Public ClaimStageID As Guid
            Public PageIndex As Integer = 0
            Public PageSize As Integer = DEFAULT_NEW_UI_PAGE_SIZE
            Public searchDV As ClaimStage.ClaimStageSearchDV = Nothing
            Public HasDataChanged As Boolean
            Public IsGridVisible As Boolean = False
            Public bnoRow As Boolean = False
            Public SortExpression As String = ClaimStage.ClaimStageSearchDV.COL_STAGE_NAME_DESC
            Public searchStageName As Guid = Guid.Empty
            Public searchCompanyGrp As Guid = Guid.Empty
            Public searchCompany As Guid = Guid.Empty
            Public searchDealer As Guid = Guid.Empty
            Public searchCoverageType As Guid = Guid.Empty
            Public searchActiveOn As DateType
            Public searchEventType As Guid = Guid.Empty
            Public searchSequence As String
            Public searchScreen As Guid = Guid.Empty
            Public searchPortal As Guid = Guid.Empty
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
                    AddCalendarwithTime_New(imgSearchActiveOn, txtSearchActiveOn)

                    UpdateBreadCrum()

                    SetDefaultButton(ddlSearchStageName, btnSearch)
                    SetDefaultButton(ddlSearchCompanyGroup, btnSearch)
                    SetDefaultButton(ddlSearchCompany, btnSearch)
                    SetDefaultButton(ddlSearchDealer, btnSearch)
                    SetDefaultButton(ddlSearchCoverageType, btnSearch)
                    SetDefaultButton(txtSearchSequence, btnSearch)
                    SetDefaultButton(imgSearchActiveOn, btnSearch)
                    SetDefaultButton(ddlSearchScreen, btnSearch)
                    SetDefaultButton(ddlSearchPortal, btnSearch)

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
                Dim retObj As ClaimStageForm.ReturnType = CType(ReturnPar, ClaimStageForm.ReturnType)
                State.HasDataChanged = retObj.HasDataChanged

                If retObj IsNot Nothing AndAlso retObj.HasDataChanged Then
                    State.searchDV = Nothing
                End If

                Select Case retObj.LastOperation
                    Case ElitaPlusPage.DetailPageCommand.Back
                        If retObj IsNot Nothing Then
                            If Not retObj.EditingBo.IsNew Then
                                State.ClaimStageID = retObj.EditingBo.Id
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
            callPage(ClaimStageForm.URL)
        End Sub

        Private Sub btnClearSearch_Click(sender As Object, e As System.EventArgs) Handles btnClearSearch.Click
            Try
                ddlSearchStageName.SelectedIndex = -1
                ddlSearchCompanyGroup.SelectedIndex = -1
                ddlSearchCompany.SelectedIndex = -1
                ddlSearchDealer.SelectedIndex = -1
                ddlSearchCoverageType.SelectedIndex = -1
                txtSearchSequence.Text = String.Empty
                txtSearchActiveOn.Text = String.Empty
                ddlSearchScreen.SelectedIndex = -1
                ddlSearchPortal.SelectedIndex = -1

                Grid.EditIndex = NO_ITEM_SELECTED_INDEX

                With State
                    .ClaimStageID = Guid.Empty
                    .searchStageName = Guid.Empty
                    .searchCompanyGrp = Guid.Empty
                    .searchCompany = Guid.Empty
                    .searchDealer = Guid.Empty
                    .searchCoverageType = Guid.Empty
                    .searchSequence = String.Empty
                    .searchActiveOn = Nothing
                    .searchPortal = Guid.Empty
                    .searchScreen = Guid.Empty
                End With
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnSearch_Click(sender As Object, e As System.EventArgs) Handles btnSearch.Click
            Try
                With State
                    .PageIndex = 0
                    .ClaimStageID = Guid.Empty
                    .IsGridVisible = True
                    .searchDV = Nothing
                    .HasDataChanged = False
                    'get search control value
                    .searchStageName = GetSelectedItem(ddlSearchStageName)
                    .searchCompanyGrp = GetSelectedItem(ddlSearchCompanyGroup)
                    .searchCompany = GetSelectedItem(ddlSearchCompany)
                    .searchDealer = GetSelectedItem(ddlSearchDealer)
                    .searchCoverageType = GetSelectedItem(ddlSearchCoverageType)
                    .searchScreen = GetSelectedItem(ddlSearchScreen)
                    .searchPortal = GetSelectedItem(ddlSearchPortal)
                    .searchSequence = txtSearchSequence.Text.Trim
                    If Not String.IsNullOrEmpty(txtSearchActiveOn.Text.Trim) Then
                        .searchActiveOn = DateHelper.GetDateValue(txtSearchActiveOn.Text.Trim)
                    End If
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
                'Dim dv As DataView = LookupListNew.DropdownLookupList("CLM_STAGE_NAME", ElitaPlusIdentity.Current.ActiveUser.LanguageId)
                'Me.BindListControlToDataView(Me.ddlSearchStageName, dv, , , True)
                Dim clmStage As ListItem() = CommonConfigManager.Current.ListManager.GetList("CLM_STAGE_NAME", Thread.CurrentPrincipal.GetLanguageCode())
                ddlSearchStageName.Populate(clmStage, New PopulateOptions() With
                                                      {
                                                        .AddBlankItem = True
                                                       })
                Dim dv As DataView = LookupListNew.GetUserCompanyGroupList()
                ddlSearchCompanyGroup.Items.Add(New System.Web.UI.WebControls.ListItem("", Guid.Empty.ToString))
                If dv.Count > 0 Then
                    ddlSearchCompanyGroup.Items.Add(New System.Web.UI.WebControls.ListItem(dv(0)("DESCRIPTION").ToString, New Guid(CType(dv(0)("ID"), Byte())).ToString))
                End If

                ' Me.BindListControlToDataView(Me.ddlSearchCompany, LookupListNew.GetUserCompaniesLookupList(), , , True)
                Dim compLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("Company", Thread.CurrentPrincipal.GetLanguageCode())
                Dim list As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Companies

                Dim filteredList As ListItem() = (From x In compLkl
                                                  Where list.Contains(x.ListItemId)
                                                  Select x).ToArray()

                ddlSearchCompany.Populate(filteredList, New PopulateOptions() With
                  {
                   .AddBlankItem = True
                  })
                ' Me.BindListControlToDataView(Me.ddlSearchDealer, LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies, False, "Code"), , , True)
                Dim oDealerList = GetDealerListByCompanyForUser()
                ddlSearchDealer.Populate(oDealerList, New PopulateOptions() With
                                           {
                                            .AddBlankItem = True
                                           })
                'dv = LookupListNew.DropdownLookupList("CTYP", ElitaPlusIdentity.Current.ActiveUser.LanguageId)
                'Me.BindListControlToDataView(Me.ddlSearchCoverageType, dv, , , True)
                Dim coverageTypes As ListItem() = CommonConfigManager.Current.ListManager.GetList("CTYP", Thread.CurrentPrincipal.GetLanguageCode())
                ddlSearchCoverageType.Populate(coverageTypes, New PopulateOptions() With
                                                      {
                                                        .AddBlankItem = True
                                                       })

                'dv = LookupListNew.GetYesNoLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId)
                'Me.BindListControlToDataView(Me.ddlSearchScreen, dv, , , True)
                'Me.BindListControlToDataView(Me.ddlSearchPortal, dv, , , True)
                Dim yesNoLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("YESNO", Thread.CurrentPrincipal.GetLanguageCode())
                ddlSearchScreen.Populate(yesNoLkl, New PopulateOptions() With
                                                      {
                                                        .AddBlankItem = True
                                                       })
                ddlSearchPortal.Populate(yesNoLkl, New PopulateOptions() With
                                                      {
                                                        .AddBlankItem = True
                                                       })

                If State.searchStageName <> Guid.Empty Then
                    SetSelectedItem(ddlSearchStageName, State.searchStageName)
                End If
                If State.searchCompanyGrp <> Guid.Empty Then
                    SetSelectedItem(ddlSearchCompanyGroup, State.searchCompanyGrp)
                End If
                If State.searchCompany <> Guid.Empty Then
                    SetSelectedItem(ddlSearchCompany, State.searchCompany)
                End If
                If State.searchDealer <> Guid.Empty Then
                    SetSelectedItem(ddlSearchDealer, State.searchDealer)
                End If
                If State.searchCoverageType <> Guid.Empty Then
                    SetSelectedItem(ddlSearchCoverageType, State.searchCoverageType)
                End If
                If State.searchScreen <> Guid.Empty Then
                    SetSelectedItem(ddlSearchScreen, State.searchScreen)
                End If
                If State.searchPortal <> Guid.Empty Then
                    SetSelectedItem(ddlSearchPortal, State.searchPortal)
                End If
                If State.searchSequence IsNot Nothing Then
                    txtSearchSequence.Text = State.searchSequence
                End If
                If State.searchActiveOn IsNot Nothing Then
                    txtSearchActiveOn.Text = State.searchActiveOn
                End If

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

        Public Sub PopulateGrid()
            Dim blnNewSearch As Boolean = False
            With State
                If ((.searchDV Is Nothing) OrElse (.HasDataChanged)) Then
                    .searchDV = ClaimStage.getList(.searchStageName, .searchCompanyGrp, .searchCompany, .searchDealer, .searchCoverageType,
                                                  .searchActiveOn, .searchSequence, .searchScreen, .searchPortal)
                    blnNewSearch = True
                End If
                If Not (.searchDV Is Nothing) Then
                    .searchDV.Sort = SortDirection

                    Grid.AutoGenerateColumns = False

                    SetPageAndSelectedIndexFromGuid(.searchDV, .ClaimStageID, Grid, State.PageIndex)
                    SortAndBindGrid(blnNewSearch)
                End If
            End With

        End Sub

        Private Sub SortAndBindGrid(Optional ByVal blnShowErr As Boolean = True)
            State.PageIndex = Grid.PageIndex

            If (State.searchDV.Count = 0) Then
                State.bnoRow = True
                State.searchDV = Nothing
                Dim objNew As ClaimStage = New ClaimStage
                ClaimStage.AddNewRowToSearchDV(State.searchDV, objNew)
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
                State.ClaimStageID = Guid.Empty
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
                    State.ClaimStageID = New Guid(CType(Grid.Rows(index).Cells(GRID_COL_CLAIM_STAGE_ID_IDX).FindControl(GRID_CTRL_NAME_LABLE_CLAIM_STAGE_ID), Label).Text)
                    callPage(ClaimStageForm.URL, State.ClaimStageID)
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub
#End Region

    End Class
End Namespace