Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports Assurant.Elita.Web.Forms

Namespace Tables

    Partial Public Class ConfigQuestionSetListForm
        Inherits ElitaPlusSearchPage

#Region "Constants"
        Public Const URL As String = "Tables/ConfigQuestionSetListForm.aspx"
        Public Const PAGETITLE As String = "CONFIG_QUESTION_SET_LIST"
        Public Const PAGETAB As String = "TABLES"
        Public Const SUMMARYTITLE As String = "SEARCH"

        Private Const GridColConfigQuestionSetIdIdx As Integer = 0
        Private Const GridCtrlNameLableConfigQuestionSetId As String = "lblConfigQuestionSetID"
        Private Const SelectCommand As String = "SelectAction"
#End Region

#Region "Page State"
        Private _isReturningFromChild As Boolean = False
        Private ReadOnly _childMessage As String = String.Empty
        Class MyState
            Public QuestionSetConfigId As Guid
            Public PageIndex As Integer = 0
            Public PageSize As Integer = DEFAULT_NEW_UI_PAGE_SIZE
            Public SearchDv As ConfigQuestionSet.ConfigQuestionSetSearchDV = Nothing
            Public HasDataChanged As Boolean
            Public IsGridVisible As Boolean = False
            Public BnoRow As Boolean = False
            Public SortExpression As String = ConfigQuestionSet.ConfigQuestionSetSearchDV.COL_QUESTION_SET_CODE
            Public SearchCompanyGrp As Guid = Guid.Empty
            Public SearchCompany As Guid = Guid.Empty
            Public SearchDealerGrp As Guid = Guid.Empty
            Public SearchDealer As Guid = Guid.Empty
            Public SearchProductCode As String = String.Empty
            Public SearchRiskType As Guid = Guid.Empty
            Public SearchCoverageType As Guid = Guid.Empty
            Public SearchQuestionSetCode As String = String.Empty
            Public SearchPurposeCode As String = String.Empty

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
            MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage(PAGETAB)
            MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage(SUMMARYTITLE)
            MasterPage.UsePageTabTitleInBreadCrum = False
            MasterPage.BreadCrum = TranslationBase.TranslateLabelOrMessage(PAGETAB) + ElitaBase.Sperator + TranslationBase.TranslateLabelOrMessage(PAGETITLE)
        End Sub

        Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
            Try
                MasterPage.MessageController.Clear()
                If Not IsPostBack Then
                    SortDirection = State.SortExpression

                    UpdateBreadCrum()

                    SetDefaultButton(ddlSearchCompanyGroup, btnSearch)
                    SetDefaultButton(ddlSearchCompany, btnSearch)
                    SetDefaultButton(ddlSearchDealerGroup, btnSearch)
                    SetDefaultButton(ddlSearchDealer, btnSearch)
                    SetDefaultButton(txtSearchProductCode, btnSearch)
                    SetDefaultButton(ddlSearchRiskType, btnSearch)
                    SetDefaultButton(ddlSearchCoverageType, btnSearch)
                    SetDefaultButton(ddlSearchQuestionSetCode, btnSearch)
                    SetDefaultButton(ddlSearchPurposeCode, btnSearch)

                    ControlMgr.SetVisibleControl(Me, moSearchResults, False)
                    PopulateSearchFieldsFromState()
                    TranslateGridHeader(Grid)
                    cboPageSize.SelectedValue = CType(State.PageSize, String)
                    Grid.PageSize = State.PageSize

                    If State.IsGridVisible Then
                        PopulateGrid()
                    End If
                    SetGridItemStyleColor(Grid)

                    If _isReturningFromChild AndAlso _childMessage.Trim <> String.Empty Then
                        MasterPage.MessageController.AddSuccess(_childMessage)
                    End If
                End If

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
            ShowMissingTranslations(MasterPage.MessageController)
        End Sub

        Private Sub Page_PageReturn(returnFromUrl As String, returnPar As Object) Handles MyBase.PageReturn
            Try
                MenuEnabled = True
                _isReturningFromChild = True
                Dim retObj As ConfigQuestionSetForm.ReturnType = CType(returnPar, ConfigQuestionSetForm.ReturnType)
                State.HasDataChanged = retObj.HasDataChanged
                If Not retObj Is Nothing AndAlso retObj.HasDataChanged Then
                    State.SearchDv = Nothing
                End If

                Select Case retObj.LastOperation
                    Case DetailPageCommand.Back
                        If Not retObj Is Nothing Then
                            If Not retObj.EditingBo.IsNew Then
                                State.QuestionSetConfigId = retObj.EditingBo.Id
                            End If
                        End If
                End Select
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub
#End Region

#Region "Button Clicks "
        Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
            callPage(ConfigQuestionSetForm.URL)
        End Sub

        Private Sub btnClearSearch_Click(sender As Object, e As EventArgs) Handles btnClearSearch.Click
            Try
                ddlSearchCompanyGroup.SelectedIndex = -1
                ddlSearchCompany.SelectedIndex = -1
                ddlSearchDealerGroup.SelectedIndex = -1
                ddlSearchDealer.SelectedIndex = -1
                ddlSearchRiskType.SelectedIndex = -1
                ddlSearchCoverageType.SelectedIndex = -1
                ddlSearchPurposeCode.SelectedIndex = -1
                txtSearchProductCode.Text = String.Empty
                ddlSearchQuestionSetCode.SelectedIndex = -1

                Grid.EditIndex = NO_ITEM_SELECTED_INDEX

                With State
                    .QuestionSetConfigId = Guid.Empty
                    .SearchCompanyGrp = Guid.Empty
                    .SearchCompany = Guid.Empty
                    .SearchDealerGrp = Guid.Empty
                    .SearchDealer = Guid.Empty
                    .SearchProductCode = String.Empty
                    .SearchCoverageType = Guid.Empty
                    .SearchQuestionSetCode = String.Empty
                    .SearchPurposeCode = String.Empty
                End With
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
            Try
                With State
                    .PageIndex = 0
                    .QuestionSetConfigId = Guid.Empty
                    .IsGridVisible = True
                    .SearchDv = Nothing
                    .HasDataChanged = False
                    'get search control value
                    .SearchCompanyGrp = GetSelectedItem(ddlSearchCompanyGroup)
                    .SearchCompany = GetSelectedItem(ddlSearchCompany)
                    .SearchDealerGrp = GetSelectedItem(ddlSearchDealerGroup)
                    .SearchDealer = GetSelectedItem(ddlSearchDealer)
                    .SearchProductCode = txtSearchProductCode.Text
                    .SearchCoverageType = GetSelectedItem(ddlSearchCoverageType)
                    .SearchQuestionSetCode = GetSelectedValue(ddlSearchQuestionSetCode)
                    .SearchPurposeCode = GetSelectedValue(ddlSearchPurposeCode)
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
                Dim textFun As Func(Of ListItem, String) = Function(li As ListItem)
                                                                            Return li.Code + " - " + li.Translation
                                                                        End Function

                'CompanyGroup
                Dim dv As DataView = LookupListNew.GetUserCompanyGroupList()
                ddlSearchCompanyGroup.Items.Add(New WebControls.ListItem("", Guid.Empty.ToString))

                If dv.Count > 0 Then
                    ddlSearchCompanyGroup.Items.Add(New WebControls.ListItem(dv(0)("DESCRIPTION").ToString, New Guid(CType(dv(0)("ID"), Byte())).ToString))
                End If

                'Company
                Dim oCompanyList = GetCompanyListByCompanyGroup(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id)
                ddlSearchCompany.Populate(oCompanyList, New PopulateOptions() With
                {
                    .AddBlankItem = True
                })

                'DealerGroup
                Dim oDealerGroupList = GetDealerGroupListByCompanyForUser()
                ddlSearchDealerGroup.Populate(oDealerGroupList, New PopulateOptions() With
                {
                    .AddBlankItem = True,
                    .SortFunc = AddressOf PopulateOptions.GetCode
                })

                'Dealer
                Dim oDealerList = GetDealerListByCompanyForUser()
                ddlSearchDealer.Populate(oDealerList, New PopulateOptions() With
                {
                    .AddBlankItem = True
                })

                'Coverage Type
                ddlSearchCoverageType.Populate(CommonConfigManager.Current.ListManager.GetList("CTYP", Authentication.CurrentUser.LanguageCode), New PopulateOptions() With
                {
                    .AddBlankItem = True
                })

                'Risk Type
                Dim listContext As ListContext = New ListContext()
                Dim compGroupId As Guid = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
                listContext.CompanyGroupId = compGroupId
                Dim riskLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("RiskTypeByCompanyGroup", Authentication.CurrentUser.LanguageCode, listContext)
                ddlSearchRiskType.Populate(riskLkl, New PopulateOptions() With
                {
                    .AddBlankItem = True
                })

                'Purpose
                ddlSearchPurposeCode.Populate(CommonConfigManager.Current.ListManager.GetList("CASEPUR", Authentication.CurrentUser.LanguageCode), New PopulateOptions() With
                {
                    .BlankItemValue = String.Empty,
                    .AddBlankItem = True,
                    .TextFunc = textFun,
                    .ValueFunc = AddressOf PopulateOptions.GetExtendedCode
                })

                'Question Set Code
                ddlSearchQuestionSetCode.Populate(CommonConfigManager.Current.ListManager.GetList("DcmQuestionSet", Authentication.CurrentUser.LanguageCode), New PopulateOptions() With
                {
                    .BlankItemValue = String.Empty,
                    .AddBlankItem = True,
                    .ValueFunc = AddressOf PopulateOptions.GetCode,
                    .TextFunc = textFun
                })

                If State.SearchCompanyGrp <> Guid.Empty Then
                    SetSelectedItem(ddlSearchCompanyGroup, State.SearchCompanyGrp)
                End If

                If State.SearchCompany <> Guid.Empty Then
                    SetSelectedItem(ddlSearchCompany, State.SearchCompany)
                End If

                If State.SearchDealerGrp <> Guid.Empty Then
                    SetSelectedItem(ddlSearchDealerGroup, State.SearchDealerGrp)
                End If

                If State.SearchDealer <> Guid.Empty Then
                    SetSelectedItem(ddlSearchDealer, State.SearchDealer)
                End If

                If State.SearchProductCode <> String.Empty Then
                    txtSearchProductCode.Text = State.SearchProductCode
                End If

                If State.SearchCoverageType <> Guid.Empty Then
                    SetSelectedItem(ddlSearchCoverageType, State.SearchCoverageType)
                End If

                If State.SearchRiskType <> Guid.Empty Then
                    SetSelectedItem(ddlSearchRiskType, State.SearchRiskType)
                End If

                If State.SearchPurposeCode <> String.Empty Then
                    SetSelectedItem(ddlSearchPurposeCode, State.SearchPurposeCode)
                End If

                If State.SearchQuestionSetCode <> String.Empty Then
                    SetSelectedItem(ddlSearchQuestionSetCode, State.SearchQuestionSetCode)
                End If

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub

        Private Function GetCompanyListByCompanyGroup(companyGroupId As Guid) As ListItem()
            Dim listContext As ListContext = New ListContext()

            listContext.CompanyGroupId = companyGroupId
            Dim companyListForCompanyGroup As ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="GetCompanyByCompanyGroup", context:=listContext)

            Return companyListForCompanyGroup.ToArray()
        End Function

        Private Function GetDealerListByCompanyForUser() As ListItem()
            Dim index As Integer
            Dim oListContext As New ListContext
            Dim userCompanies As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Companies
            Dim oDealerList As New Collections.Generic.List(Of ListItem)

            For index = 0 To userCompanies.Count - 1
                oListContext.CompanyId = userCompanies(index)
                Dim oDealerListForCompany As ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="DealerListByCompany", context:=oListContext)
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
            Dim index As Integer
            Dim oListContext As New ListContext
            Dim userCompanies As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Companies
            Dim oDealerGroupList As New Collections.Generic.List(Of ListItem)

            For index = 0 To userCompanies.Count - 1
                oListContext.CompanyId = userCompanies(index)
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

        Private Function GetDealerGroupListByCompany() As ListItem()
            Dim oListContext As New ListContext
            oListContext.CompanyId = Guid.Parse(ddlSearchCompany.SelectedValue)
            Dim oDealerGroupListForCompany As ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="DealerGroupByCompany", context:=oListContext)
            Return oDealerGroupListForCompany.ToArray()
        End Function

        Private Function GetDealerListByCompany() As ListItem()
            Dim oListContext As New ListContext
            oListContext.CompanyId = Guid.Parse(ddlSearchCompany.SelectedValue)
            Dim oDealerListForCompany As ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="DealerListByCompany", context:=oListContext)
            Return oDealerListForCompany.ToArray()
        End Function

        Public Sub PopulateGrid()
            Dim blnNewSearch As Boolean = False
            With State
                If ((.SearchDv Is Nothing) OrElse (.HasDataChanged)) Then
                    .SearchDv = ConfigQuestionSet.getList(CompGrpID:= .SearchCompanyGrp, CompanyID:= .SearchCompany, DealerGrpID:= .SearchDealerGrp, DealerID:= .SearchDealer,
                                                  ProductCode:= .SearchProductCode, CoverageTypeID:= .SearchCoverageType,
                                                  RiskTypeID:= .SearchRiskType, strPurposeXCD:= .SearchPurposeCode, strQuestionSetCode:= .SearchQuestionSetCode)
                    blnNewSearch = True
                End If
                If Not (.SearchDv Is Nothing) Then
                    .SearchDv.Sort = SortDirection
                    Grid.AutoGenerateColumns = False
                    SetPageAndSelectedIndexFromGuid(.SearchDv, .QuestionSetConfigId, Grid, State.PageIndex)
                    SortAndBindGrid(blnNewSearch)
                End If
            End With
        End Sub

        Private Sub SortAndBindGrid(Optional ByVal blnShowErr As Boolean = True)
            State.PageIndex = Grid.PageIndex

            If (State.SearchDv.Count = 0) Then
                State.BnoRow = True
                State.SearchDv = Nothing
                Dim objNew As ConfigQuestionSet = New ConfigQuestionSet
                ConfigQuestionSet.AddNewRowToSearchDV(State.SearchDv, objNew)
                Grid.DataSource = State.SearchDv
                Grid.DataBind()
                Grid.Rows(0).Visible = False
                State.IsGridVisible = False
                If blnShowErr Then
                    MasterPage.MessageController.AddInformation(Message.MSG_NO_RECORDS_FOUND, True)
                End If
            Else
                State.IsGridVisible = True
                State.BnoRow = False
                Grid.Enabled = True
                Grid.DataSource = State.SearchDv
                HighLightSortColumn(Grid, SortDirection, True)
                Grid.DataBind()
            End If

            If Not Grid.BottomPagerRow.Visible Then Grid.BottomPagerRow.Visible = True

            ControlMgr.SetVisibleControl(Me, moSearchResults, State.IsGridVisible)

            Session("recCount") = State.SearchDv.Count

            If State.IsGridVisible Then
                lblRecordCount.Text = State.SearchDv.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
            End If

        End Sub
#End Region

#Region "Grid related"
        Private Sub Grid_PageIndexChanged(sender As Object, e As EventArgs) Handles Grid.PageIndexChanged
            Try
                State.PageIndex = Grid.PageIndex
                State.QuestionSetConfigId = Guid.Empty
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub Grid_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles Grid.PageIndexChanging
            Try
                Grid.PageIndex = e.NewPageIndex
                State.PageIndex = Grid.PageIndex
            Catch ex As Exception
                HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub Grid_Sorting(sender As Object, e As GridViewSortEventArgs) Handles Grid.Sorting
            Try
                Dim spaceIndex As Integer = SortDirection.LastIndexOf(" ", StringComparison.Ordinal)

                If spaceIndex > 0 AndAlso Me.SortDirection.Substring(0, spaceIndex).Equals(e.SortExpression) Then
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

        Protected Sub cboPageSize_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboPageSize.SelectedIndexChanged
            Try
                State.PageSize = CType(cboPageSize.SelectedValue, Integer)
                State.PageIndex = NewCurrentPageIndex(Grid, State.SearchDv.Count, State.PageSize)
                Grid.PageIndex = State.PageIndex
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Public Sub RowCreated(sender As Object, e As GridViewRowEventArgs)
            Try
                BaseItemCreated(sender, e)
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Public Sub RowCommand(source As Object, e As GridViewCommandEventArgs)
            Try
                Dim index As Integer
                If (e.CommandName = SelectCommand) Then
                    index = CInt(e.CommandArgument)

                    State.QuestionSetConfigId = New Guid(CType(Grid.Rows(index).Cells(GridColConfigQuestionSetIdIdx).FindControl(GridCtrlNameLableConfigQuestionSetId), Label).Text)
                    callPage(ConfigQuestionSetForm.URL, State.QuestionSetConfigId)
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub ddlSearchCompany_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSearchCompany.SelectedIndexChanged

            If ddlSearchCompany.SelectedIndex > NO_ITEM_SELECTED_INDEX Then
                If ddlSearchCompany.SelectedIndex = BLANK_ITEM_SELECTED Then
                    'DealerGroup
                    Dim oDealerGroupList = GetDealerGroupListByCompanyForUser()
                    ddlSearchDealerGroup.Populate(oDealerGroupList, New PopulateOptions() With
                    {
                        .AddBlankItem = True,
                        .SortFunc = AddressOf PopulateOptions.GetCode
                    })

                    'Dealer
                    Dim oDealerList = GetDealerListByCompanyForUser()
                    ddlSearchDealer.Populate(oDealerList, New PopulateOptions() With
                    {
                        .AddBlankItem = True
                    })
                Else
                    'DealerGroup
                    Dim oDealerGroupList = GetDealerGroupListByCompany()
                    ddlSearchDealerGroup.Populate(oDealerGroupList, New PopulateOptions() With
                    {
                        .AddBlankItem = True,
                        .SortFunc = AddressOf PopulateOptions.GetCode
                    })

                    'Dealer
                    Dim oDealerList = GetDealerListByCompany()
                    ddlSearchDealer.Populate(oDealerList, New PopulateOptions() With
                    {
                        .AddBlankItem = True
                    })
                End If
            End If
        End Sub

#End Region

    End Class
End Namespace