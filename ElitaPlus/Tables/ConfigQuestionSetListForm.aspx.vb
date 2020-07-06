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
            Public searchProductCode As Guid = Guid.Empty
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

                    SetDefaultButton(Me.ddlSearchCompanyGroup, btnSearch)
                    SetDefaultButton(Me.ddlSearchCompany, btnSearch)
                    SetDefaultButton(Me.ddlSearchDealerGroup, btnSearch)
                    SetDefaultButton(Me.ddlSearchDealer, btnSearch)
                    SetDefaultButton(Me.ddlSearchProductCode, btnSearch)
                    SetDefaultButton(Me.ddlSearchRiskType, btnSearch)
                    SetDefaultButton(Me.ddlSearchCoverageType, btnSearch)
                    SetDefaultButton(Me.ddlSearchQuestionSetCode, btnSearch)
                    SetDefaultButton(Me.ddlSearchPurposeCode, btnSearch)

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
                Dim retObj As ConfigQuestionSetForm.ReturnType = CType(ReturnPar, ConfigQuestionSetForm.ReturnType)
                Me.State.HasDataChanged = retObj.HasDataChanged
                If Not retObj Is Nothing AndAlso retObj.HasDataChanged Then
                    Me.State.searchDV = Nothing
                End If

                Select Case retObj.LastOperation
                    Case ElitaPlusPage.DetailPageCommand.Back
                        If Not retObj Is Nothing Then
                            If Not retObj.EditingBo.IsNew Then
                                Me.State.QuestionSetConfigID = retObj.EditingBo.Id
                            End If
                        End If
                End Select
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub
#End Region

#Region "Button Clicks "
        Private Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
            callPage(ConfigQuestionSetForm.URL)
        End Sub

        Private Sub btnClearSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClearSearch.Click
            Try
                Me.ddlSearchCompanyGroup.SelectedIndex = -1
                Me.ddlSearchCompany.SelectedIndex = -1
                Me.ddlSearchDealerGroup.SelectedIndex = -1
                Me.ddlSearchDealer.SelectedIndex = -1
                Me.ddlSearchRiskType.SelectedIndex = -1
                Me.ddlSearchCoverageType.SelectedIndex = -1
                Me.ddlSearchPurposeCode.SelectedIndex = -1
                Me.ddlSearchProductCode.SelectedIndex = -1
                Me.ddlSearchQuestionSetCode.SelectedIndex = -1

                Grid.EditIndex = NO_ITEM_SELECTED_INDEX

                With State
                    .QuestionSetConfigID = Guid.Empty
                    .searchCompanyGrp = Guid.Empty
                    .searchCompany = Guid.Empty
                    .searchDealerGrp = Guid.Empty
                    .searchDealer = Guid.Empty
                    .searchProductCode = Guid.Empty
                    .searchCoverageType = Guid.Empty
                    .searchQuestionSetCode = String.Empty
                    .searchPurposeCode = String.Empty
                End With
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
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
                    .searchProductCode = GetSelectedItem(ddlSearchProductCode)
                    .searchCoverageType = GetSelectedItem(ddlSearchCoverageType)
                    .searchQuestionSetCode = GetSelectedValue(ddlSearchQuestionSetCode)
                    .searchPurposeCode = GetSelectedValue(ddlSearchPurposeCode)
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
                Dim compLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("Company", Thread.CurrentPrincipal.GetLanguageCode())
                Dim list As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Companies

                Dim filteredList As ListItem() = (From x In compLkl
                                                  Where list.Contains(x.ListItemId)
                                                  Select x).ToArray()

                Me.ddlSearchCompany.Populate(filteredList, New PopulateOptions() With
                {
                    .AddBlankItem = True
                })

                'DealerGroup
                Dim oDealerGroupList = GetDealerGroupListByCompanyForUser()
                Me.ddlSearchDealerGroup.Populate(oDealerGroupList, New PopulateOptions() With
                {
                    .AddBlankItem = True,
                    .SortFunc = AddressOf .GetCode
                })

                'Dealer
                Dim oDealerList = GetDealerListByCompanyForUser()
                Me.ddlSearchDealer.Populate(oDealerList, New PopulateOptions() With
                {
                    .AddBlankItem = True
                })

                'ProductCode
                Dim oProductCodeList = GetProductCodeListByCompanyForUser()
                Me.ddlSearchProductCode.Populate(oProductCodeList, New PopulateOptions() With
                {
                    .AddBlankItem = True,
                    .TextFunc = textFun
                })

                'Coverage Type
                ddlSearchCoverageType.Populate(CommonConfigManager.Current.ListManager.GetList("CTYP", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
                {
                    .AddBlankItem = True
                })

                'Risk Type
                Dim listcontext As ListContext = New ListContext()
                Dim compGroupId As Guid = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
                listcontext.CompanyGroupId = compGroupId
                Dim riskLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("RiskTypeByCompanyGroup", Thread.CurrentPrincipal.GetLanguageCode(), listcontext)
                ddlSearchRiskType.Populate(riskLkl, New PopulateOptions() With
                {
                    .AddBlankItem = True
                })

                'Purpose
                ddlSearchPurposeCode.Populate(CommonConfigManager.Current.ListManager.GetList("CASEPUR", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
                {
                    .BlankItemValue = String.Empty,
                    .AddBlankItem = True,
                    .TextFunc = textFun,
                    .ValueFunc = AddressOf .GetExtendedCode
                })

                'Question Set Code
                Me.ddlSearchQuestionSetCode.Populate(CommonConfigManager.Current.ListManager.GetList("DcmQuestionSet", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
                {
                    .BlankItemValue = String.Empty,
                    .AddBlankItem = True,
                    .ValueFunc = AddressOf .GetCode,
                    .TextFunc = textFun
                })

                If Me.State.searchCompanyGrp <> Guid.Empty Then
                    SetSelectedItem(ddlSearchCompanyGroup, Me.State.searchCompanyGrp)
                End If

                If Me.State.searchCompany <> Guid.Empty Then
                    SetSelectedItem(ddlSearchCompany, Me.State.searchCompany)
                End If

                If Me.State.searchDealerGrp <> Guid.Empty Then
                    SetSelectedItem(ddlSearchDealerGroup, Me.State.searchDealerGrp)
                End If

                If Me.State.searchDealer <> Guid.Empty Then
                    SetSelectedItem(ddlSearchDealer, Me.State.searchDealer)
                End If

                If Me.State.searchProductCode <> Guid.Empty Then
                    SetSelectedItem(ddlSearchProductCode, Me.State.searchProductCode)
                End If

                If Me.State.searchCoverageType <> Guid.Empty Then
                    SetSelectedItem(ddlSearchCoverageType, Me.State.searchCoverageType)
                End If

                If Me.State.searchRiskType <> Guid.Empty Then
                    SetSelectedItem(ddlSearchRiskType, Me.State.searchRiskType)
                End If

                If Me.State.searchPurposeCode <> String.Empty Then
                    SetSelectedItem(ddlSearchPurposeCode, Me.State.searchPurposeCode)
                End If

                If Me.State.searchQuestionSetCode <> String.Empty Then
                    SetSelectedItem(ddlSearchQuestionSetCode, Me.State.searchQuestionSetCode)
                End If

            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try

        End Sub

        Private Function GetDealerListByCompanyForUser() As Assurant.Elita.CommonConfiguration.DataElements.ListItem()
            Dim Index As Integer
            Dim oListContext As New ListContext

            Dim UserCompanies As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Companies

            Dim oDealerList As New Collections.Generic.List(Of ListItem)

            For Index = 0 To UserCompanies.Count - 1
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

        Private Function GetProductListByDealer() As ListItem()
            Dim oListContext As New ListContext
            oListContext.DealerId = Guid.Parse(ddlSearchDealer.SelectedValue)
            Dim oProductListForCompany As ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="ProductCodeByDealer", context:=oListContext)
            Return oProductListForCompany.ToArray()
        End Function

        Private Function GetProductCodeListByCompanyForUser() As ListItem()
            Dim Index As Integer
            Dim oListContext As New ListContext

            Dim UserCompanies As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Companies

            Dim oProductCodeList As New Collections.Generic.List(Of ListItem)

            For Index = 0 To UserCompanies.Count - 1
                oListContext.CompanyId = UserCompanies(Index)
                Dim oProductCodeListForCompany As ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="ProductCodeByCompany", context:=oListContext)
                If oProductCodeListForCompany.Count > 0 Then
                    If oProductCodeList IsNot Nothing Then
                        oProductCodeList.AddRange(oProductCodeListForCompany)
                    Else
                        oProductCodeList = oProductCodeListForCompany.Clone()
                    End If
                End If
            Next

            Return oProductCodeList.ToArray()

        End Function

        Public Sub PopulateGrid()
            Dim blnNewSearch As Boolean = False
            With State
                If ((.searchDV Is Nothing) OrElse (.HasDataChanged)) Then
                    .searchDV = ConfigQuestionSet.getList(CompGrpID:= .searchCompanyGrp, CompanyID:= .searchCompany, DealerGrpID:= .searchDealerGrp, DealerID:= .searchDealer,
                                                  ProductCodeID:= .searchProductCode, CoverageTypeID:= .searchCoverageType,
                                                  RiskTypeID:= .searchRiskType, strPurposeXCD:= .searchPurposeCode, strQuestionSetCode:= .searchQuestionSetCode)
                    blnNewSearch = True
                End If
                If Not (.searchDV Is Nothing) Then
                    .searchDV.Sort = Me.SortDirection
                    Me.Grid.AutoGenerateColumns = False
                    SetPageAndSelectedIndexFromGuid(.searchDV, .QuestionSetConfigID, Me.Grid, Me.State.PageIndex)
                    Me.SortAndBindGrid(blnNewSearch)
                End If
            End With
        End Sub

        Private Sub SortAndBindGrid(Optional ByVal blnShowErr As Boolean = True)
            Me.State.PageIndex = Me.Grid.PageIndex

            If (Me.State.searchDV.Count = 0) Then
                Me.State.bnoRow = True
                Me.State.searchDV = Nothing
                Dim objNew As ConfigQuestionSet = New ConfigQuestionSet
                ConfigQuestionSet.AddNewRowToSearchDV(Me.State.searchDV, objNew)
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
                Me.State.QuestionSetConfigID = Guid.Empty
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
                If (e.CommandName = SELECT_COMMAND) Then
                    index = CInt(e.CommandArgument)

                    Me.State.QuestionSetConfigID = New Guid(CType(Me.Grid.Rows(index).Cells(GRID_COL_CONFIG_QUESTION_SET_ID_IDX).FindControl(GRID_CTRL_NAME_LABLE_CONFIG_QUESTION_SET_ID), Label).Text)
                    Me.callPage(ConfigQuestionSetForm.URL, Me.State.QuestionSetConfigID)
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub ddlSearchCompany_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSearchCompany.SelectedIndexChanged

            Dim textFun As Func(Of DataElements.ListItem, String) = Function(li As DataElements.ListItem)
                                                                        Return li.Code + " - " + li.Translation
                                                                    End Function

            If ddlSearchCompany.SelectedIndex > NO_ITEM_SELECTED_INDEX Then

                If ddlSearchCompany.SelectedIndex = BLANK_ITEM_SELECTED Then
                    Exit Sub
                End If

                'DealerGroup
                Dim oDealerGroupList = GetDealerGroupListByCompany()
                Me.ddlSearchDealerGroup.Populate(oDealerGroupList, New PopulateOptions() With
                {
                    .AddBlankItem = True,
                    .SortFunc = AddressOf .GetCode
                })

                'Dealer
                Dim oDealerList = GetDealerListByCompany()
                Me.ddlSearchDealer.Populate(oDealerList, New PopulateOptions() With
                {
                    .AddBlankItem = True
                })

                'ProductCode
                Dim oProductCodeList = GetProductListByCompany()
                Me.ddlSearchProductCode.Populate(oProductCodeList, New PopulateOptions() With
                {
                    .AddBlankItem = True,
                    .TextFunc = textFun
                })

            End If
        End Sub

        Private Sub ddlSearchDealer_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSearchDealer.SelectedIndexChanged
            Dim textFun As Func(Of DataElements.ListItem, String) = Function(li As DataElements.ListItem)
                                                                        Return li.Code + " - " + li.Translation
                                                                    End Function

            If ddlSearchDealer.SelectedIndex > NO_ITEM_SELECTED_INDEX Then

                If ddlSearchCompany.SelectedIndex = BLANK_ITEM_SELECTED Then
                    Exit Sub
                End If

                'ProductCode
                Dim oProductCodeList = GetProductListByDealer()
                Me.ddlSearchProductCode.Populate(oProductCodeList, New PopulateOptions() With
                {
                    .AddBlankItem = True,
                    .TextFunc = textFun
                })

            End If
        End Sub

#End Region

    End Class
End Namespace