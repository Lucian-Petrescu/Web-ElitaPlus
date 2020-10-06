Imports System.Globalization
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports System.Threading
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms

Namespace Tables

    Partial Public Class ConfigQuestionSetListForm
        Inherits ElitaPlusSearchPage

#Region "Constants"
        Public Const URL As String = "Tables/ConfigQuestionSetListForm.aspx"
        Public Const PAGETITLE As String = "CONFIG_QUESTION_SET_LIST"
        Public Const PAGETAB As String = "TABLES"
        Public Const SUMMARYTITLE As String = "SEARCH"

        Private Const GRID_COL_CONFIG_QUESTION_SET_ID_IDX As Integer = 0

        Private Const GRID_CTRL_NAME_LABLE_CONFIG_QUESTION_SET_ID As String = "lblConfigQuestionSetID"

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
            Public searchDV As ConfigQuestionSet.ConfigQuestionSetSearchDV = Nothing
            Public HasDataChanged As Boolean
            Public IsGridVisible As Boolean = False
            Public bnoRow As Boolean = False
            Public SortExpression As String = ConfigQuestionSet.ConfigQuestionSetSearchDV.COL_QUESTION_SET_CODE
            Public searchCompanyGrp As Guid = Guid.Empty
            Public searchCompany As Guid = Guid.Empty
            Public searchDealerGrp As Guid = Guid.Empty
            Public searchDealer As Guid = Guid.Empty
            Public searchProductCode As String = String.Empty
            Public searchRiskType As Guid = Guid.Empty
            Public searchCoverageType As Guid = Guid.Empty
            Public searchQuestionSetCode As String = String.Empty
            Public searchPurposeCode As String = String.Empty

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
                Dim retObj As ConfigQuestionSetForm.ReturnType = CType(ReturnPar, ConfigQuestionSetForm.ReturnType)
                State.HasDataChanged = retObj.HasDataChanged
                If retObj IsNot Nothing AndAlso retObj.HasDataChanged Then
                    State.searchDV = Nothing
                End If

                Select Case retObj.LastOperation
                    Case ElitaPlusPage.DetailPageCommand.Back
                        If retObj IsNot Nothing Then
                            If Not retObj.EditingBo.IsNew Then
                                State.QuestionSetConfigID = retObj.EditingBo.Id
                            End If
                        End If
                End Select
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub
#End Region

#Region "Button Clicks "
        Private Sub btnNew_Click(sender As Object, e As System.EventArgs) Handles btnNew.Click
            callPage(ConfigQuestionSetForm.URL)
        End Sub

        Private Sub btnClearSearch_Click(sender As Object, e As System.EventArgs) Handles btnClearSearch.Click
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
                    .QuestionSetConfigID = Guid.Empty
                    .searchCompanyGrp = Guid.Empty
                    .searchCompany = Guid.Empty
                    .searchDealerGrp = Guid.Empty
                    .searchDealer = Guid.Empty
                    .searchProductCode = String.Empty
                    .searchCoverageType = Guid.Empty
                    .searchQuestionSetCode = String.Empty
                    .searchPurposeCode = String.Empty
                End With
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnSearch_Click(sender As Object, e As System.EventArgs) Handles btnSearch.Click
            Try
                With State
                    .PageIndex = 0
                    .QuestionSetConfigID = Guid.Empty
                    .IsGridVisible = True
                    .searchDV = Nothing
                    .HasDataChanged = False
                    'get search control value
                    .searchCompanyGrp = GetSelectedItem(ddlSearchCompanyGroup)
                    .searchCompany = GetSelectedItem(ddlSearchCompany)
                    .searchDealerGrp = GetSelectedItem(ddlSearchDealerGroup)
                    .searchDealer = GetSelectedItem(ddlSearchDealer)
                    .searchProductCode = txtSearchProductCode.Text
                    .searchCoverageType = GetSelectedItem(ddlSearchCoverageType)
                    .searchQuestionSetCode = GetSelectedValue(ddlSearchQuestionSetCode)
                    .searchPurposeCode = GetSelectedValue(ddlSearchPurposeCode)
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
                Dim textFun As Func(Of DataElements.ListItem, String) = Function(li As DataElements.ListItem)
                                                                            Return li.Code + " - " + li.Translation
                                                                        End Function

                'CompanyGroup
                Dim dv As DataView = LookupListNew.GetUserCompanyGroupList()
                ddlSearchCompanyGroup.Items.Add(New System.Web.UI.WebControls.ListItem("", Guid.Empty.ToString))

                If dv.Count > 0 Then
                    ddlSearchCompanyGroup.Items.Add(New System.Web.UI.WebControls.ListItem(dv(0)("DESCRIPTION").ToString, New Guid(CType(dv(0)("ID"), Byte())).ToString))
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
                    .SortFunc = AddressOf .GetCode
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
                Dim listcontext As ListContext = New ListContext()
                Dim compGroupId As Guid = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
                listcontext.CompanyGroupId = compGroupId
                Dim riskLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("RiskTypeByCompanyGroup", Authentication.CurrentUser.LanguageCode, listcontext)
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
                    .ValueFunc = AddressOf .GetExtendedCode
                })

                'Question Set Code
                ddlSearchQuestionSetCode.Populate(CommonConfigManager.Current.ListManager.GetList("DcmQuestionSet", Authentication.CurrentUser.LanguageCode), New PopulateOptions() With
                {
                    .BlankItemValue = String.Empty,
                    .AddBlankItem = True,
                    .ValueFunc = AddressOf .GetCode,
                    .TextFunc = textFun
                })

                If State.searchCompanyGrp <> Guid.Empty Then
                    SetSelectedItem(ddlSearchCompanyGroup, State.searchCompanyGrp)
                End If

                If State.searchCompany <> Guid.Empty Then
                    SetSelectedItem(ddlSearchCompany, State.searchCompany)
                End If

                If State.searchDealerGrp <> Guid.Empty Then
                    SetSelectedItem(ddlSearchDealerGroup, State.searchDealerGrp)
                End If

                If State.searchDealer <> Guid.Empty Then
                    SetSelectedItem(ddlSearchDealer, State.searchDealer)
                End If

                If State.searchProductCode <> String.Empty Then
                    txtSearchProductCode.Text = State.searchProductCode
                End If

                If State.searchCoverageType <> Guid.Empty Then
                    SetSelectedItem(ddlSearchCoverageType, State.searchCoverageType)
                End If

                If State.searchRiskType <> Guid.Empty Then
                    SetSelectedItem(ddlSearchRiskType, State.searchRiskType)
                End If

                If State.searchPurposeCode <> String.Empty Then
                    SetSelectedItem(ddlSearchPurposeCode, State.searchPurposeCode)
                End If

                If State.searchQuestionSetCode <> String.Empty Then
                    SetSelectedItem(ddlSearchQuestionSetCode, State.searchQuestionSetCode)
                End If

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub

        Private Function GetCompanyListByCompanyGroup(companyGroupId As Guid) As ListItem()
            Dim listcontext As ListContext = New ListContext()

            listcontext.CompanyGroupId = companyGroupId
            Dim companyListForCompanyGroup As ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="GetCompanyByCompanyGroup", context:=listcontext)

            Return companyListForCompanyGroup.ToArray()
        End Function

        Private Function GetDealerListByCompanyForUser() As ListItem()
            Dim Index As Integer
            Dim oListContext As New ListContext

            Dim UserCompanies As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Companies

            Dim oDealerList As New Collections.Generic.List(Of ListItem)

            For Index = 0 To UserCompanies.Count - 1
                oListContext.CompanyId = UserCompanies(Index)
                Dim oDealerListForCompany As ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="DealerListByCompany", context:=oListContext)
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

        Private Function GetProductListByCompany() As ListItem()
            Dim oListContext As New ListContext
            oListContext.CompanyId = Guid.Parse(ddlSearchCompany.SelectedValue)
            Dim oProductListForCompany As ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="ProductCodeByCompany", context:=oListContext)
            Return oProductListForCompany.ToArray()
        End Function

        Public Sub PopulateGrid()
            Dim blnNewSearch As Boolean = False
            With State
                If ((.searchDV Is Nothing) OrElse (.HasDataChanged)) Then
                    .searchDV = ConfigQuestionSet.getList(CompGrpID:= .searchCompanyGrp, CompanyID:= .searchCompany, DealerGrpID:= .searchDealerGrp, DealerID:= .searchDealer,
                                                  ProductCode:= .searchProductCode, CoverageTypeID:= .searchCoverageType,
                                                  RiskTypeID:= .searchRiskType, strPurposeXCD:= .searchPurposeCode, strQuestionSetCode:= .searchQuestionSetCode)
                    blnNewSearch = True
                End If
                If Not (.searchDV Is Nothing) Then
                    .searchDV.Sort = SortDirection
                    Grid.AutoGenerateColumns = False
                    SetPageAndSelectedIndexFromGuid(.searchDV, .QuestionSetConfigID, Grid, State.PageIndex)
                    SortAndBindGrid(blnNewSearch)
                End If
            End With
        End Sub

        Private Sub SortAndBindGrid(Optional ByVal blnShowErr As Boolean = True)
            State.PageIndex = Grid.PageIndex

            If (State.searchDV.Count = 0) Then
                State.bnoRow = True
                State.searchDV = Nothing
                Dim objNew As ConfigQuestionSet = New ConfigQuestionSet
                ConfigQuestionSet.AddNewRowToSearchDV(State.searchDV, objNew)
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
                State.QuestionSetConfigID = Guid.Empty
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

                    State.QuestionSetConfigID = New Guid(CType(Grid.Rows(index).Cells(GRID_COL_CONFIG_QUESTION_SET_ID_IDX).FindControl(GRID_CTRL_NAME_LABLE_CONFIG_QUESTION_SET_ID), Label).Text)
                    callPage(ConfigQuestionSetForm.URL, State.QuestionSetConfigID)
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub ddlSearchCompany_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSearchCompany.SelectedIndexChanged

            Dim textFun As Func(Of DataElements.ListItem, String) = Function(li As DataElements.ListItem)
                                                                        Return li.Code + " - " + li.Translation
                                                                    End Function

            If ddlSearchCompany.SelectedIndex > NO_ITEM_SELECTED_INDEX Then
                If ddlSearchCompany.SelectedIndex = BLANK_ITEM_SELECTED Then
                    'DealerGroup
                    Dim oDealerGroupList = GetDealerGroupListByCompanyForUser()
                    ddlSearchDealerGroup.Populate(oDealerGroupList, New PopulateOptions() With
                    {
                        .AddBlankItem = True,
                        .SortFunc = AddressOf .GetCode
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
                        .SortFunc = AddressOf .GetCode
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