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


        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

            Try
                Me.MasterPage.MessageController.Clear()
                Me.Form.DefaultButton = btnSearch.UniqueID
                If Not Me.IsPostBack Then

                    ' Populate the header and breadcrumb
                    Me.MasterPage.UsePageTabTitleInBreadCrum = False
                    UpdateBreadCrum()
                    TranslateGridHeader(Grid)
                    PopulateControls()
                    'Set page size


                    If Me.IsReturningFromChild Then
                        ' It is returning from detail
                        Me.PopulateGrid()
                        Me.GetStateProperties()
                    Else
                        cboPageSize.SelectedValue = Me.State.PageSize.ToString()
                        ControlMgr.SetVisibleControl(Me, trPageSize, False)
                    End If

                    'SetFocus(Me.TextBoxCaseNumber)
                End If
                Me.DisplayNewProgressBarOnClick(Me.btnSearch, "LOADING_REWARDS")
                Me.ShowMissingTranslations(Me.MasterPage.MessageController)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try


        End Sub

        Private Sub Page_PageReturn(ByVal ReturnFromUrl As String, ByVal ReturnPar As Object) Handles MyBase.PageReturn
            Try
                Me.MenuEnabled = True
                Me.IsReturningFromChild = True
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
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub
#End Region

#Region "Handlers-DropDown"
        Private Sub cboPageSize_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
            Try
                State.PageSize = CType(cboPageSize.SelectedValue, Integer)
                Me.State.PageIndex = NewCurrentPageIndex(Grid, State.searchDV.Count, State.PageSize)
                Me.Grid.PageIndex = Me.State.PageIndex
                Me.PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub
#End Region
#Region "Handlers-Buttons"


        Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
            Try

                Me.SetStateProperties()
                Me.State.PageIndex = 0
                Me.State.selectedCertId = Guid.Empty
                Me.State.IsGridVisible = True
                Me.State.searchClick = True
                Me.State.searchDV = Nothing
                PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub
        Protected Sub btnClearSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnClearSearch.Click
            Try

                ' Clear all search options typed or selected by the user
                Me.ClearAllSearchOptions()

                ' Update the Bo state properties with the new value
                Me.SetStateProperties()

            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub
#End Region
#Region "Populate"
        Private Sub PopulateControls()
            PopulateCompaniesDropdown()
            PopulateDealersDropdown()
            'Me.ddlRewardStatus.PopulateOld("REWARD_STATUS", ListValueType.Description, ListValueType.ExtendedCode, PopulateBehavior.AddBlankListItem, String.Empty, ListValueType.Description)
            Me.ddlRewardStatus.Populate(CommonConfigManager.Current.ListManager.GetList("REWARD_STATUS", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
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
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
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

        Private Sub PopulateSortByDropDown(ByVal sortByDropDownList As DropDownList)
            Try
                sortByDropDownList.Populate(CommonConfigManager.Current.ListManager.GetList("REWARDSORTDRP", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
                                                    {
                                                        .AddBlankItem = True
                                                    })

                'Set the default Sort by
                Dim defaultSelectedCodeId As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_REWARD_SEARCH_FIELDS, "CERT_NUMBER")

                If (Me.State.selectedSortById.Equals(Guid.Empty)) Then
                    SetSelectedItem(sortByDropDownList, defaultSelectedCodeId)
                    Me.State.selectedSortById = defaultSelectedCodeId
                Else
                    Me.SetSelectedItem(sortByDropDownList, Me.State.selectedSortById)
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub
        Private Sub PopulateGrid()
            Try

                Dim sortBy As String = String.Empty
                If (Me.State.searchDV Is Nothing) Then
                    If (Not (Me.State.selectedSortById.Equals(Guid.Empty))) Then
                        sortBy = LookupListNew.GetCodeFromId(LookupListNew.LK_REWARD_SEARCH_FIELDS, Me.State.selectedSortById)
                    End If
                    Me.State.searchDV = Rewards.getRewardList(Me.State.CompanyId,
                                                             Me.State.DealerId,
                                                             Me.State.CertificateNumber,
                                                             Me.State.RewardStatus)


                    If Me.State.searchClick Then
                        Me.ValidSearchResultCountNew(Me.State.searchDV.Count, True)
                        Me.State.searchClick = False
                    End If
                End If

                Grid.PageSize = State.PageSize
                SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.selectedCertId, Me.Grid, Me.State.PageIndex)
                Me.Grid.DataSource = Me.State.searchDV
                Me.State.PageIndex = Me.Grid.PageIndex
                If (Not Me.State.SortExpression.Equals(String.Empty)) Then
                    Me.State.searchDV.Sort = Me.State.SortExpression
                Else
                    Me.State.SortExpression = sortBy
                    Me.State.searchDV.Sort = Me.State.SortExpression
                End If

                HighLightSortColumn(Me.Grid, Me.State.SortExpression, Me.IsNewUI)
                Me.Grid.DataBind()

                ControlMgr.SetVisibleControl(Me, Grid, True)
                ControlMgr.SetVisibleControl(Me, trPageSize, Me.Grid.Visible)
                Session("recCount") = Me.State.searchDV.Count

                If Me.Grid.Visible Then
                    Me.lblRecordCount.Text = Me.State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
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
                If ((Not GetExceptionType.Equals(String.Empty)) And GetExceptionType.Equals("BOValidationException")) Then
                    ControlMgr.SetVisibleControl(Me, Grid, False)
                    lblPageSize.Visible = False
                    lblRecordCount.Visible = False
                    cboPageSize.Visible = False
                    colonSepertor.Visible = False
                End If
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

#End Region
#Region "Other"
        Private Sub UpdateBreadCrum()
            If (Not Me.State Is Nothing) Then
                Me.MasterPage.BreadCrum = Me.MasterPage.PageTab & ElitaBase.Sperator &
                    TranslationBase.TranslateLabelOrMessage("REWARD_SEARCH")
                Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("REWARD_SEARCH")
            End If
        End Sub
        Protected Sub ClearAllSearchOptions()
            Try
                ddlCompanyName.SelectedIndex = DEFAULT_ITEM
                ddlDealerName.SelectedIndex = DEFAULT_ITEM
                ddlRewardStatus.SelectedIndex = DEFAULT_ITEM
                Me.TextBoxCertificateNumber.Text = String.Empty

            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub
        Protected Sub ClearStateValues()
            Try
                'clear State
                Me.State.CompanyId = Nothing
                Me.State.DealerId = Nothing
                Me.State.RewardStatus = String.Empty
                Me.State.CertificateNumber = String.Empty

            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try

        End Sub

        Private Sub GetStateProperties()
            Try
                If Me.State.CompanyId <> Guid.Empty And ddlCompanyName.Items.Count > 0 Then Me.SetSelectedItem(ddlCompanyName, State.CompanyId)
                If Me.State.DealerId <> Guid.Empty And ddlDealerName.Items.Count > 0 Then Me.SetSelectedItem(ddlDealerName, State.DealerId)


                If Me.State.RewardStatus <> String.Empty And ddlRewardStatus.Items.Count > 0 Then Me.SetSelectedItem(ddlRewardStatus, State.RewardStatus)
                Me.TextBoxCertificateNumber.Text = Me.State.CertificateNumber

            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub
        Private Sub SetStateProperties()
            Try
                If Me.State Is Nothing Then
                    Trace(Me, "Restoring State")
                    Me.RestoreState(New MyState)
                End If

                Me.ClearStateValues()

                Me.State.CompanyId = GetSelectedItem(Me.ddlCompanyName)
                Me.State.DealerId = GetSelectedItem(Me.ddlDealerName)
                Me.State.RewardStatus = GetSelectedValue(Me.ddlRewardStatus)
                Me.State.CertificateNumber = Me.TextBoxCertificateNumber.Text.ToUpper.Trim

            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub
#End Region
#Region "Grid Action"
        Private Sub Grid_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowDataBound
            Try
                Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
                Dim btnEditItem As LinkButton
                If (e.Row.RowType = DataControlRowType.DataRow) _
                OrElse (e.Row.RowType = DataControlRowType.Separator) Then
                    If (Not e.Row.Cells(GRID_COL_REWARD_ID_IDX).FindControl(GRID_COL_REWARD_NUMBER_CTRL) Is Nothing) Then
                        btnEditItem = CType(e.Row.Cells(GRID_COL_REWARD_ID_IDX).FindControl(GRID_COL_REWARD_NUMBER_CTRL), LinkButton)
                        btnEditItem.CommandArgument = GetGuidStringFromByteArray(CType(dvRow(Rewards.RewardSearchDV.COL_REWARD_ID), Byte()))
                        btnEditItem.CommandName = SELECT_ACTION_COMMAND
                        btnEditItem.Text = dvRow(Rewards.RewardSearchDV.COL_CERT_NUMBER).ToString
                    End If
                    e.Row.Cells(GRID_COL_REWARD_STATUS_CODE_IDX).Text = LookupListNew.GetDescriptionFromCode("REWARD_STATUS", dvRow(Rewards.RewardSearchDV.COL_REWARD_STATUS_CODE).ToString, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
                    e.Row.Cells(GRID_COL_REWARD_TYPE_IDX).Text = LookupListNew.GetDescriptionFromCode("REWARD_TYPE", dvRow(Rewards.RewardSearchDV.COL_REWARD_TYPE).ToString, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub
        Private Sub Grid_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles Grid.RowCommand
            Try
                If e.CommandName = SELECT_ACTION_COMMAND Then
                    If Not e.CommandArgument.ToString().Equals(String.Empty) Then
                        Me.State.selectedCertId = New Guid(e.CommandArgument.ToString())
                        Me.callPage(RewardDetailsForm.URL, Me.State.selectedCertId)
                    End If
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                If (TypeOf ex Is System.Reflection.TargetInvocationException) AndAlso
               (TypeOf ex.InnerException Is Threading.ThreadAbortException) Then Return
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub
        Private Sub Grid_PageIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Grid.PageIndexChanged
            Try
                Me.State.PageIndex = Grid.PageIndex
                Me.State.selectedCertId = Guid.Empty
                PopulateGrid()
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
        Private Sub Grid_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles Grid.Sorting
            Try
                If Me.State.SortExpression.StartsWith(e.SortExpression) Then
                    If Me.State.SortExpression.EndsWith(" DESC") Then
                        Me.State.SortExpression = e.SortExpression
                    Else
                        Me.State.SortExpression &= " DESC"
                    End If
                Else
                    Me.State.SortExpression = e.SortExpression
                End If
                Me.State.PageIndex = 0
                Me.PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try

        End Sub
        Private Sub Grid_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowCreated
            Try
                BaseItemCreated(sender, e)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub
#End Region
    End Class
End Namespace