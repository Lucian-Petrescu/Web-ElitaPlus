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
                    Me.AddCalendarwithTime_New(Me.imgSearchActiveOn, txtSearchActiveOn)

                    UpdateBreadCrum()

                    Me.SetDefaultButton(Me.ddlSearchStageName, btnSearch)
                    Me.SetDefaultButton(Me.ddlSearchCompanyGroup, btnSearch)
                    Me.SetDefaultButton(Me.ddlSearchCompany, btnSearch)
                    Me.SetDefaultButton(Me.ddlSearchDealer, btnSearch)
                    Me.SetDefaultButton(Me.ddlSearchCoverageType, btnSearch)
                    Me.SetDefaultButton(Me.txtSearchSequence, btnSearch)
                    Me.SetDefaultButton(Me.imgSearchActiveOn, btnSearch)
                    Me.SetDefaultButton(Me.ddlSearchScreen, btnSearch)
                    Me.SetDefaultButton(Me.ddlSearchPortal, btnSearch)

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
                Dim retObj As ClaimStageForm.ReturnType = CType(ReturnPar, ClaimStageForm.ReturnType)
                Me.State.HasDataChanged = retObj.HasDataChanged

                If Not retObj Is Nothing AndAlso retObj.HasDataChanged Then
                    Me.State.searchDV = Nothing
                End If

                Select Case retObj.LastOperation
                    Case ElitaPlusPage.DetailPageCommand.Back
                        If Not retObj Is Nothing Then
                            If Not retObj.EditingBo.IsNew Then
                                Me.State.ClaimStageID = retObj.EditingBo.Id
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
            callPage(ClaimStageForm.URL)
        End Sub

        Private Sub btnClearSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClearSearch.Click
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

                Grid.EditIndex = Me.NO_ITEM_SELECTED_INDEX

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
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
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
                Me.PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub
#End Region

#Region "Helper Functions"
        Protected Sub PopulateSearchFieldsFromState()
            Try
                'Dim dv As DataView = LookupListNew.DropdownLookupList("CLM_STAGE_NAME", ElitaPlusIdentity.Current.ActiveUser.LanguageId)
                'Me.BindListControlToDataView(Me.ddlSearchStageName, dv, , , True)
                Dim clmStage As ListItem() = CommonConfigManager.Current.ListManager.GetList("CLM_STAGE_NAME", Thread.CurrentPrincipal.GetLanguageCode())
                Me.ddlSearchStageName.Populate(clmStage, New PopulateOptions() With
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

                Me.ddlSearchCompany.Populate(filteredList, New PopulateOptions() With
                  {
                   .AddBlankItem = True
                  })
                ' Me.BindListControlToDataView(Me.ddlSearchDealer, LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies, False, "Code"), , , True)
                Dim oDealerList = GetDealerListByCompanyForUser()
                Me.ddlSearchDealer.Populate(oDealerList, New PopulateOptions() With
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
                Me.ddlSearchScreen.Populate(yesNoLkl, New PopulateOptions() With
                                                      {
                                                        .AddBlankItem = True
                                                       })
                Me.ddlSearchPortal.Populate(yesNoLkl, New PopulateOptions() With
                                                      {
                                                        .AddBlankItem = True
                                                       })

                If Me.State.searchStageName <> Guid.Empty Then
                    Me.SetSelectedItem(ddlSearchStageName, Me.State.searchStageName)
                End If
                If Me.State.searchCompanyGrp <> Guid.Empty Then
                    Me.SetSelectedItem(ddlSearchCompanyGroup, Me.State.searchCompanyGrp)
                End If
                If Me.State.searchCompany <> Guid.Empty Then
                    Me.SetSelectedItem(ddlSearchCompany, Me.State.searchCompany)
                End If
                If Me.State.searchDealer <> Guid.Empty Then
                    Me.SetSelectedItem(ddlSearchDealer, Me.State.searchDealer)
                End If
                If Me.State.searchCoverageType <> Guid.Empty Then
                    Me.SetSelectedItem(ddlSearchCoverageType, Me.State.searchCoverageType)
                End If
                If Me.State.searchScreen <> Guid.Empty Then
                    Me.SetSelectedItem(ddlSearchScreen, Me.State.searchScreen)
                End If
                If Me.State.searchPortal <> Guid.Empty Then
                    Me.SetSelectedItem(ddlSearchPortal, Me.State.searchPortal)
                End If
                If Not Me.State.searchSequence Is Nothing Then
                    Me.txtSearchSequence.Text = Me.State.searchSequence
                End If
                If Not Me.State.searchActiveOn Is Nothing Then
                    Me.txtSearchActiveOn.Text = Me.State.searchActiveOn
                End If

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

        Public Sub PopulateGrid()
            Dim blnNewSearch As Boolean = False
            With State
                If ((.searchDV Is Nothing) OrElse (.HasDataChanged)) Then
                    .searchDV = ClaimStage.getList(.searchStageName, .searchCompanyGrp, .searchCompany, .searchDealer, .searchCoverageType,
                                                  .searchActiveOn, .searchSequence, .searchScreen, .searchPortal)
                    blnNewSearch = True
                End If
                If Not (.searchDV Is Nothing) Then
                    .searchDV.Sort = Me.SortDirection

                    Me.Grid.AutoGenerateColumns = False

                    SetPageAndSelectedIndexFromGuid(.searchDV, .ClaimStageID, Me.Grid, Me.State.PageIndex)
                    Me.SortAndBindGrid(blnNewSearch)
                End If
            End With

        End Sub

        Private Sub SortAndBindGrid(Optional ByVal blnShowErr As Boolean = True)
            Me.State.PageIndex = Me.Grid.PageIndex

            If (Me.State.searchDV.Count = 0) Then
                Me.State.bnoRow = True
                Me.State.searchDV = Nothing
                Dim objNew As ClaimStage = New ClaimStage
                ClaimStage.AddNewRowToSearchDV(Me.State.searchDV, objNew)
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
                Me.State.ClaimStageID = Guid.Empty
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
                    Me.State.ClaimStageID = New Guid(CType(Me.Grid.Rows(index).Cells(Me.GRID_COL_CLAIM_STAGE_ID_IDX).FindControl(Me.GRID_CTRL_NAME_LABLE_CLAIM_STAGE_ID), Label).Text)
                    Me.callPage(ClaimStageForm.URL, Me.State.ClaimStageID)
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try

        End Sub
#End Region

    End Class
End Namespace