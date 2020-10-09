Imports System.Threading
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms
Imports System.Globalization
Imports Assurant.Elita.CommonConfiguration.DataElements

Namespace Certificates

    Partial Public Class RewardSearchForm
        Inherits ElitaPlusSearchPage
#Region "Constants"


        Private Const DEFAULT_ITEM As Integer = 0
        Private Const LABEL_SELECT_COMPANY As String = "COMPANY"
        Public Const GRID_COL_REWARD_ID_IDX As Integer = 0
        Public Const GRID_COL_REWARD_STATUS_CODE_IDX As Integer = 1
        Public Const GRID_COL_REWARD_AMOUNT_IDX As Integer = 2
        Public Const GRID_COL_REWARD_TYPE_IDX As Integer = 3
        Public Const GRID_COL_REWARD_NUMBER_CTRL As String = "btnEditCase"
        Public Const SELECT_ACTION_COMMAND As String = "SelectAction"
        Public Const URL As String = "~/Certificates/RewardSearchForm.aspx"
#End Region
#Region "Variable"
        Private IsReturningFromChild As Boolean = False
#End Region
#Region "Page State"
        Class MyState


            Public CompanyId As Guid
            Public DealerId As Guid
            Public CertificateNumber As String
            Public RewardStatus As String
            Public selectedSortById As Guid = Guid.Empty
            Public selectedCertId As Guid = Guid.Empty

            Public SearchDV As Rewards.RewardSearchDV = Nothing
            Public searchClick As Boolean = False

            Public PageIndex As Integer = 0
            Public PageSize As Integer = 30
            Public IsGridVisible As Boolean = False
            Public SortExpression As String = String.Empty

            Sub New()
            End Sub
        End Class
#End Region
        Public Sub New()
            MyBase.New(New MyState)
        End Sub

#Region "Handlers-Init, page events"


        Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

            Try
                MasterPage.MessageController.Clear()
                Form.DefaultButton = btnSearch.UniqueID
                If Not IsPostBack Then

                    ' Populate the header and breadcrumb
                    MasterPage.UsePageTabTitleInBreadCrum = False
                    UpdateBreadCrum()
                    TranslateGridHeader(Grid)
                    PopulateControls()
                    'Set page size


                    If IsReturningFromChild Then
                        ' It is returning from detail
                        PopulateGrid()
                        GetStateProperties()
                    Else
                        cboPageSize.SelectedValue = State.PageSize.ToString()
                        ControlMgr.SetVisibleControl(Me, trPageSize, False)
                    End If

                    'SetFocus(Me.TextBoxCaseNumber)
                End If
                DisplayNewProgressBarOnClick(btnSearch, "LOADING_REWARDS")
                ShowMissingTranslations(MasterPage.MessageController)
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try


        End Sub

        Private Sub Page_PageReturn(ReturnFromUrl As String, ReturnPar As Object) Handles MyBase.PageReturn
            Try
                MenuEnabled = True
                IsReturningFromChild = True
                ' Dim retObj As RewardDetailsForm.ReturnType = CType(ReturnPar, CaseDetailsForm.ReturnType)
                'If Not retObj Is Nothing AndAlso retObj.BoChanged Then
                '    Me.State.searchDV = Nothing
                'End If
                'Select Case retObj.LastOperation
                '    Case ElitaPlusPage.DetailPageCommand.Back
                '        If Not retObj Is Nothing Then
                '            If Not retObj.EditingBo.IsNew Then
                '                Me.State.selectedCaseId = retObj.EditingBo.Id
                '            End If
                '            Me.State.IsGridVisible = True
                '        End If
                'End Select
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub
#End Region

#Region "Handlers-DropDown"
        Private Sub cboPageSize_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
            Try
                State.PageSize = CType(cboPageSize.SelectedValue, Integer)
                State.PageIndex = NewCurrentPageIndex(Grid, State.searchDV.Count, State.PageSize)
                Grid.PageIndex = State.PageIndex
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub
#End Region
#Region "Handlers-Buttons"


        Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
            Try

                SetStateProperties()
                State.PageIndex = 0
                State.selectedCertId = Guid.Empty
                State.IsGridVisible = True
                State.searchClick = True
                State.searchDV = Nothing
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub
        Protected Sub btnClearSearch_Click(sender As Object, e As EventArgs) Handles btnClearSearch.Click
            Try

                ' Clear all search options typed or selected by the user
                ClearAllSearchOptions()

                ' Update the Bo state properties with the new value
                SetStateProperties()

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub
#End Region
#Region "Populate"
        Private Sub PopulateControls()
            PopulateCompaniesDropdown()
            PopulateDealersDropdown()
            'Me.ddlRewardStatus.PopulateOld("REWARD_STATUS", ListValueType.Description, ListValueType.ExtendedCode, PopulateBehavior.AddBlankListItem, String.Empty, ListValueType.Description)
            ddlRewardStatus.Populate(CommonConfigManager.Current.ListManager.GetList("REWARD_STATUS", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
               {
                 .ValueFunc = AddressOf .GetExtendedCode,
                 .AddBlankItem = True,
                 .BlankItemValue = String.Empty
               })
        End Sub
        Private Sub PopulateCompaniesDropdown()
            Try
                Dim compLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList(ListCodes.Company, Thread.CurrentPrincipal.GetLanguageCode())
                Dim FilteredRecord As ListItem() = (From x In compLkl
                                                    Where (ElitaPlusIdentity.Current.ActiveUser.Companies).Contains(x.ListItemId)
                                                    Select x).ToArray()
                Dim companyTextFunc As Func(Of DataElements.ListItem, String) = Function(li As DataElements.ListItem)
                                                                                    Return li.Translation + " " + "(" + li.Code + ")"
                                                                                End Function
                ddlCompanyName.Populate(FilteredRecord, New PopulateOptions() With
                                                  {
                                                    .AddBlankItem = False,
                                                    .TextFunc = companyTextFunc
                                                   })

                If ddlCompanyName.Items.Count > DEFAULT_ITEM Then
                    ddlCompanyName.SelectedIndex = DEFAULT_ITEM
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub PopulateDealersDropdown()
            Try
                Dim oDealerList = GetDealerListByCompanyForUser()
                Dim dealerTextFunc As Func(Of DataElements.ListItem, String) = Function(li As DataElements.ListItem)
                                                                                   Return li.Translation + " " + "(" + li.Code + ")"
                                                                               End Function
                ddlDealerName.Populate(oDealerList, New PopulateOptions() With
                                                   {
                                                    .AddBlankItem = True,
                                                    .TextFunc = dealerTextFunc
                                                   })

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

        Private Sub PopulateSortByDropDown(sortByDropDownList As DropDownList)
            Try
                sortByDropDownList.Populate(CommonConfigManager.Current.ListManager.GetList("REWARDSORTDRP", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
                                                    {
                                                        .AddBlankItem = True
                                                    })

                'Set the default Sort by
                Dim defaultSelectedCodeId As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_REWARD_SEARCH_FIELDS, "CERT_NUMBER")

                If (State.selectedSortById.Equals(Guid.Empty)) Then
                    SetSelectedItem(sortByDropDownList, defaultSelectedCodeId)
                    State.selectedSortById = defaultSelectedCodeId
                Else
                    Me.SetSelectedItem(sortByDropDownList, State.selectedSortById)
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub
        Private Sub PopulateGrid()
            Try

                Dim sortBy As String = String.Empty
                If (State.searchDV Is Nothing) Then
                    If (Not (State.selectedSortById.Equals(Guid.Empty))) Then
                        sortBy = LookupListNew.GetCodeFromId(LookupListNew.LK_REWARD_SEARCH_FIELDS, State.selectedSortById)
                    End If
                    State.searchDV = Rewards.getRewardList(State.CompanyId,
                                                             State.DealerId,
                                                             State.CertificateNumber,
                                                             State.RewardStatus)


                    If State.searchClick Then
                        ValidSearchResultCountNew(State.searchDV.Count, True)
                        State.searchClick = False
                    End If
                End If

                Grid.PageSize = State.PageSize
                SetPageAndSelectedIndexFromGuid(State.searchDV, State.selectedCertId, Grid, State.PageIndex)
                Grid.DataSource = State.searchDV
                State.PageIndex = Grid.PageIndex
                If (Not State.SortExpression.Equals(String.Empty)) Then
                    State.searchDV.Sort = State.SortExpression
                Else
                    State.SortExpression = sortBy
                    State.searchDV.Sort = State.SortExpression
                End If

                HighLightSortColumn(Grid, State.SortExpression, IsNewUI)
                Grid.DataBind()

                ControlMgr.SetVisibleControl(Me, Grid, True)
                ControlMgr.SetVisibleControl(Me, trPageSize, Grid.Visible)
                Session("recCount") = State.searchDV.Count

                If Grid.Visible Then
                    lblRecordCount.Text = State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                End If

                If State.searchDV.Count = 0 Then
                    For Each gvRow As GridViewRow In Grid.Rows
                        gvRow.Visible = False
                        gvRow.Controls.Clear()
                    Next
                    lblPageSize.Visible = False
                    cboPageSize.Visible = False
                    colonSepertor.Visible = False
                Else
                    lblPageSize.Visible = True
                    cboPageSize.Visible = True
                    colonSepertor.Visible = True
                End If

            Catch ex As Exception
                Dim GetExceptionType As String = ex.GetBaseException.GetType().Name
                If ((Not GetExceptionType.Equals(String.Empty)) AndAlso GetExceptionType.Equals("BOValidationException")) Then
                    ControlMgr.SetVisibleControl(Me, Grid, False)
                    lblPageSize.Visible = False
                    lblRecordCount.Visible = False
                    cboPageSize.Visible = False
                    colonSepertor.Visible = False
                End If
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

#End Region
#Region "Other"
        Private Sub UpdateBreadCrum()
            If (State IsNot Nothing) Then
                MasterPage.BreadCrum = MasterPage.PageTab & ElitaBase.Sperator &
                    TranslationBase.TranslateLabelOrMessage("REWARD_SEARCH")
                MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("REWARD_SEARCH")
            End If
        End Sub
        Protected Sub ClearAllSearchOptions()
            Try
                ddlCompanyName.SelectedIndex = DEFAULT_ITEM
                ddlDealerName.SelectedIndex = DEFAULT_ITEM
                ddlRewardStatus.SelectedIndex = DEFAULT_ITEM
                TextBoxCertificateNumber.Text = String.Empty

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub
        Protected Sub ClearStateValues()
            Try
                'clear State
                State.CompanyId = Nothing
                State.DealerId = Nothing
                State.RewardStatus = String.Empty
                State.CertificateNumber = String.Empty

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub

        Private Sub GetStateProperties()
            Try
                If State.CompanyId <> Guid.Empty AndAlso ddlCompanyName.Items.Count > 0 Then Me.SetSelectedItem(ddlCompanyName, State.CompanyId)
                If State.DealerId <> Guid.Empty AndAlso ddlDealerName.Items.Count > 0 Then Me.SetSelectedItem(ddlDealerName, State.DealerId)


                If State.RewardStatus <> String.Empty And ddlRewardStatus.Items.Count > 0 Then Me.SetSelectedItem(ddlRewardStatus, State.RewardStatus)
                TextBoxCertificateNumber.Text = State.CertificateNumber

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub
        Private Sub SetStateProperties()
            Try
                If State Is Nothing Then
                    Trace(Me, "Restoring State")
                    RestoreState(New MyState)
                End If

                ClearStateValues()

                State.CompanyId = GetSelectedItem(ddlCompanyName)
                State.DealerId = GetSelectedItem(ddlDealerName)
                State.RewardStatus = GetSelectedValue(ddlRewardStatus)
                State.CertificateNumber = TextBoxCertificateNumber.Text.ToUpper.Trim

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub
#End Region
#Region "Grid Action"
        Private Sub Grid_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowDataBound
            Try
                Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
                Dim btnEditItem As LinkButton
                If (e.Row.RowType = DataControlRowType.DataRow) _
                OrElse (e.Row.RowType = DataControlRowType.Separator) Then
                    If (e.Row.Cells(GRID_COL_REWARD_ID_IDX).FindControl(GRID_COL_REWARD_NUMBER_CTRL) IsNot Nothing) Then
                        btnEditItem = CType(e.Row.Cells(GRID_COL_REWARD_ID_IDX).FindControl(GRID_COL_REWARD_NUMBER_CTRL), LinkButton)
                        btnEditItem.CommandArgument = GetGuidStringFromByteArray(CType(dvRow(Rewards.RewardSearchDV.COL_REWARD_ID), Byte()))
                        btnEditItem.CommandName = SELECT_ACTION_COMMAND
                        btnEditItem.Text = dvRow(Rewards.RewardSearchDV.COL_CERT_NUMBER).ToString
                    End If
                    e.Row.Cells(GRID_COL_REWARD_STATUS_CODE_IDX).Text = LookupListNew.GetDescriptionFromCode("REWARD_STATUS", dvRow(Rewards.RewardSearchDV.COL_REWARD_STATUS_CODE).ToString, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
                    e.Row.Cells(GRID_COL_REWARD_TYPE_IDX).Text = LookupListNew.GetDescriptionFromCode("REWARD_TYPE", dvRow(Rewards.RewardSearchDV.COL_REWARD_TYPE).ToString, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub
        Private Sub Grid_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles Grid.RowCommand
            Try
                If e.CommandName = SELECT_ACTION_COMMAND Then
                    If Not e.CommandArgument.ToString().Equals(String.Empty) Then
                        State.selectedCertId = New Guid(e.CommandArgument.ToString())
                        callPage(RewardDetailsForm.URL, State.selectedCertId)
                    End If
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                If (TypeOf ex Is System.Reflection.TargetInvocationException) AndAlso
               (TypeOf ex.InnerException Is Threading.ThreadAbortException) Then Return
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub
        Private Sub Grid_PageIndexChanged(sender As Object, e As System.EventArgs) Handles Grid.PageIndexChanged
            Try
                State.PageIndex = Grid.PageIndex
                State.selectedCertId = Guid.Empty
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
        Private Sub Grid_SortCommand(source As Object, e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles Grid.Sorting
            Try
                If State.SortExpression.StartsWith(e.SortExpression) Then
                    If State.SortExpression.EndsWith(" DESC") Then
                        State.SortExpression = e.SortExpression
                    Else
                        State.SortExpression &= " DESC"
                    End If
                Else
                    State.SortExpression = e.SortExpression
                End If
                State.PageIndex = 0
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub
        Private Sub Grid_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowCreated
            Try
                BaseItemCreated(sender, e)
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub
#End Region
    End Class
End Namespace