Imports System.Reflection
Imports System.Threading
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms
Imports Assurant.Elita.CommonConfiguration.DataElements


Namespace Certificates
    Partial Public Class CaseSearchForm
        Inherits ElitaPlusSearchPage
#Region "Constants"


        Private Const DefaultItem As Integer = 0
        Public Const GridColCaseIdIdx As Integer = 0
        Public Const GridColCaseOpenDateIdx As Integer = 1
        Public Const GridColCaseStatusCodeIdx As Integer = 2
        Public Const GridColCaseCloseDateIdx As Integer = 3
        Public Const GridColCaseNumberCtrl As String = "btnEditCase"
        Public Const SelectActionCommand As String = "SelectAction"
        Public Const Url As String = "~/Certificates/CaseSearchForm.aspx"
        Private Const OneSpace As String = " "
#End Region
#Region "Variable"
        Private _isReturningFromChild As Boolean = False
#End Region
#Region "Page State"
        Class MyState


            Public CompanyId As Guid
            Public CaseNumber As String
            Public CaseStatus As String
            Public CallerFirstName As String
            Public CallerLastName As String
            Public CaseOpenDateFrom As String
            Public CaseOpenDateTo As String
            Public CasePurpose As String
            Public CertificateNumber As String
            Public CaseClosedReason As String
            Public SelectedSortById As Guid = Guid.Empty
            Public SelectedCaseId As Guid = Guid.Empty

            Public SearchDv As CaseBase.CaseSearchDv = Nothing
            Public SearchClick As Boolean = False

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


        Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

            Try
                MasterPage.MessageController.Clear()
                Form.DefaultButton = btnSearch.UniqueID
                If Not IsPostBack Then

                    ' Populate the header and breadcrumb
                    MasterPage.UsePageTabTitleInBreadCrum = False
                    UpdateBreadCrum()
                    TranslateGridHeader(Grid)
                    PopulateControls()
                    AddCalendar_New(btnCaseOpenDateFrom, TextBoxCaseOpenDateFrom)
                    AddCalendar_New(btnCaseOpenToDateTo, TextBoxCaseOpenDateTo)
                    'Set page size


                    If _isReturningFromChild Then
                        ' It is returning from detail
                        PopulateGrid()
                        GetStateProperties()
                    Else
                        cboPageSize.SelectedValue = State.PageSize.ToString()
                        ControlMgr.SetVisibleControl(Me, trPageSize, False)
                    End If

                    SetFocus(TextBoxCaseNumber)
                End If
                DisplayNewProgressBarOnClick(btnSearch, "LOADING_CASES")
                ShowMissingTranslations(MasterPage.MessageController)
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try


        End Sub

        Private Sub Page_PageReturn(returnFromUrl As String, returnPar As Object) Handles MyBase.PageReturn
            Try
                MenuEnabled = True
                _isReturningFromChild = True
                Dim retObj As CaseDetailsForm.ReturnType = CType(ReturnPar, CaseDetailsForm.ReturnType)
                If retObj IsNot Nothing AndAlso retObj.BoChanged Then
                    State.searchDV = Nothing
                End If
                Select Case retObj.LastOperation
                    Case DetailPageCommand.Back
                        If retObj IsNot Nothing Then
                            If Not retObj.EditingBo.IsNew Then
                                State.selectedCaseId = retObj.EditingBo.Id
                            End If
                            State.IsGridVisible = True
                        End If
                End Select
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub
#End Region

#Region "Handlers-DropDown"
        Private Sub cboPageSize_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboPageSize.SelectedIndexChanged
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
                State.selectedCaseId = Guid.Empty
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
            PopulateSortByDropDown(cboSortBy)

            Dim caseStatus As ListItem() = CommonConfigManager.Current.ListManager.GetList("CASESTAT", Thread.CurrentPrincipal.GetLanguageCode())
            caseStatus.OrderBy("Translation", LinqExtentions.SortDirection.Descending)

            ddlCaseStatus.Items.Clear()
            Dim itm As New WebControls.ListItem
            itm.Text = String.Empty
            itm.Value = String.Empty
            ddlCaseStatus.Items.Add(itm)
            For Each li As ListItem In caseStatus
                itm = New WebControls.ListItem
                itm.Text = li.Translation
                itm.Value = li.ExtendedCode
                ddlCaseStatus.Items.Add(itm)
            Next

            'ddlCaseStatus.Populate(caseStatus, New PopulateOptions() With
            '                                  {
            '                                    .AddBlankItem = True,
            '                                    .BlankItemValue = String.Empty,
            '                                    .ValueFunc = AddressOf PopulateOptions.GetExtendedCode,
            '                                    .SortFunc = AddressOf .GetDescription
            '                                   })

            Dim casePurpose As ListItem() = CommonConfigManager.Current.ListManager.GetList("CASEPUR", Thread.CurrentPrincipal.GetLanguageCode())
            casePurpose.OrderBy("Translation", LinqExtentions.SortDirection.Ascending)
            ddlCasePurpose.Populate(casePurpose, New PopulateOptions() With
                                              {
                                                .AddBlankItem = True,
                                                .BlankItemValue = String.Empty,
                                                .ValueFunc = AddressOf PopulateOptions.GetExtendedCode
                                               })

            'ddlCaseClosedReason.PopulateOld("CASECLSRSN", ListValueType.Description, ListValueType.ExtendedCode, PopulateBehavior.AddBlankListItem, String.Empty, ListValueType.Description)
            Dim caseClosedReason As ListItem() = CommonConfigManager.Current.ListManager.GetList("CASECLSRSN", Thread.CurrentPrincipal.GetLanguageCode())
            caseClosedReason.OrderBy("ListItemId", LinqExtentions.SortDirection.Ascending)
            ddlCaseClosedReason.Populate(caseClosedReason, New PopulateOptions() With
                                              {
                                                .AddBlankItem = True,
                                                .BlankItemValue = String.Empty,
                                                .TextFunc = AddressOf PopulateOptions.GetDescription,
                                                .ValueFunc = AddressOf PopulateOptions.GetExtendedCode
                                               })

            TextBoxCaseOpenDateFrom.Text = GetDateFormattedString(Date.Today.AddDays(-7))
            TextBoxCaseOpenDateTo.Text = GetDateFormattedString(Date.Today)
        End Sub
        Private Sub PopulateCompaniesDropdown()
            Try
                Dim companyList As ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="Company")

                Dim filteredCompanyList As ListItem() = (From x In companyList
                                                                      Where Authentication.CurrentUser.Companies.Contains(x.ListItemId)
                                                                      Select x).ToArray()

                Dim companyTextFunc As Func(Of ListItem, String) = Function(li As ListItem)
                                                                                    Return li.Translation + " " + "(" + li.Code + ")"
                                                                                End Function

                ddlCompanyName.Populate(filteredCompanyList, New PopulateOptions() With
                                                   {
                                                    .AddBlankItem = False,
                                                    .TextFunc = companyTextFunc
                                                   })

                If ddlCompanyName.Items.Count > DefaultItem Then
                    ddlCompanyName.SelectedIndex = DefaultItem
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub
        Private Sub PopulateSortByDropDown(sortByDropDownList As DropDownList)
            Try

                Dim caseSearchField As ListItem() =
                        CommonConfigManager.Current.ListManager.GetList("CASESORTDRP",
                                                                        Thread.CurrentPrincipal.GetLanguageCode())
                sortByDropDownList.Populate(caseSearchField, New PopulateOptions())

                'Set the default Sort by
                Dim selectedListItem As ListItem = (From item In caseSearchField Where item.Code = "CASE_NUMBER" Select item).FirstOrDefault
                'Dim defaultSelectedCodeId As Guid = LookupListNew.GetIdFromCode("CASESORTDRP", "CASE_NUMBER")

                If (State.selectedSortById.Equals(Guid.Empty)) Then
                    SetSelectedItem(sortByDropDownList, selectedListItem.ListItemId)
                    State.selectedSortById = selectedListItem.ListItemId
                Else
                    SetSelectedItem(sortByDropDownList, State.selectedSortById)
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
                        sortBy = LookupListNew.GetCodeFromId(LookupListNew.LK_CASE_SEARCH_FIELDS, State.selectedSortById)
                    End If
                    State.searchDV = CaseBase.GetCaseList(State.CompanyId,
                                                             State.CaseNumber,
                                                             State.CaseStatus,
                                                             State.CallerFirstName,
                                                             State.CallerLastName,
                                                             State.CaseOpenDateFrom,
                                                             State.CaseOpenDateTo,
                                                             State.CasePurpose,
                                                             State.CertificateNumber,
                                                             State.CaseClosedReason,
                                                             Authentication.LangId,
                                                             Authentication.CurrentUser.NetworkId)


                    If State.searchClick Then
                        ValidSearchResultCountNew(State.searchDV.Count, True)
                        State.searchClick = False
                    End If
                End If

                Grid.PageSize = State.PageSize
                SetPageAndSelectedIndexFromGuid(State.searchDV, State.selectedCaseId, Grid, State.PageIndex)
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
                    lblRecordCount.Text = State.searchDV.Count & OneSpace & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
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
                Dim getExceptionType As String = ex.GetBaseException.GetType().Name
                If ((Not GetExceptionType.Equals(String.Empty)) And GetExceptionType.Equals("BOValidationException")) Then
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
                    TranslationBase.TranslateLabelOrMessage("CASE_SEARCH")
                MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("CASE_SEARCH")
            End If
        End Sub
        Protected Sub ClearAllSearchOptions()
            Try
                ddlCompanyName.SelectedIndex = DefaultItem
                ddlCaseStatus.SelectedIndex = DefaultItem
                ddlCasePurpose.SelectedIndex = DefaultItem
                ddlCaseClosedReason.SelectedIndex = DefaultItem

                TextBoxCaseNumber.Text = String.Empty
                TextBoxCaseOpenDateFrom.Text = String.Empty 'GetDateFormattedString(Date.Today.AddDays(-7))
                TextBoxCaseOpenDateTo.Text = String.Empty 'GetDateFormattedString(Date.Today)
                TextBoxCallerFirstName.Text = String.Empty
                TextBoxCallerLastName.Text = String.Empty
                TextBoxCertificateNumber.Text = String.Empty

                PopulateSortByDropDown(cboSortBy)
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub
        Protected Sub ClearStateValues()
            Try
                'clear State
                State.CompanyId = Nothing
                State.CaseNumber = String.Empty
                State.CaseStatus = String.Empty
                State.CaseOpenDateFrom = String.Empty
                State.CaseOpenDateTo = String.Empty
                State.CasePurpose = String.Empty
                State.CallerFirstName = String.Empty
                State.CallerLastName = String.Empty
                State.CertificateNumber = String.Empty
                State.CaseClosedReason = String.Empty

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub

        Private Sub GetStateProperties()
            Try
                If State.CompanyId <> Guid.Empty And ddlCompanyName.Items.Count > 0 Then SetSelectedItem(ddlCompanyName, State.CompanyId)

                TextBoxCaseNumber.Text = State.CaseNumber
                If State.CaseStatus <> String.Empty And ddlCaseStatus.Items.Count > 0 Then SetSelectedItem(ddlCaseStatus, State.CaseStatus)
                TextBoxCaseOpenDateFrom.Text = State.CaseOpenDateFrom
                TextBoxCaseOpenDateTo.Text = State.CaseOpenDateTo
                If State.CasePurpose <> String.Empty And ddlCasePurpose.Items.Count > 0 Then SetSelectedItem(ddlCasePurpose, State.CasePurpose)
                TextBoxCallerFirstName.Text = State.CallerFirstName
                TextBoxCallerLastName.Text = State.CallerLastName
                TextBoxCertificateNumber.Text = State.CertificateNumber
                If State.CaseClosedReason <> String.Empty And ddlCaseClosedReason.Items.Count > 0 Then SetSelectedItem(ddlCaseClosedReason, State.CaseClosedReason)
                If State.selectedSortById <> Guid.Empty And cboSortBy.Items.Count > 0 Then SetSelectedItem(cboSortBy, State.selectedSortById)

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
                State.CaseNumber = TextBoxCaseNumber.Text.ToUpper.Trim
                State.CaseStatus = GetSelectedValue(ddlCaseStatus)
                State.CaseOpenDateFrom = TextBoxCaseOpenDateFrom.Text.Trim
                State.CaseOpenDateTo = TextBoxCaseOpenDateTo.Text.Trim
                State.CasePurpose = GetSelectedValue(ddlCasePurpose)
                State.CallerFirstName = TextBoxCallerFirstName.Text.ToUpper.Trim
                State.CallerLastName = TextBoxCallerLastName.Text.ToUpper.Trim
                State.CertificateNumber = TextBoxCertificateNumber.Text.ToUpper.Trim
                State.CaseClosedReason = GetSelectedValue(ddlCaseClosedReason)
                State.selectedSortById = GetSelectedItem(cboSortBy)

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub
#End Region
#Region "Grid Action"
        Private Sub Grid_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles Grid.RowDataBound
            Try
                Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
                Dim btnEditItem As LinkButton
                If (e.Row.RowType = DataControlRowType.DataRow) _
                OrElse (e.Row.RowType = DataControlRowType.Separator) Then
                    If (e.Row.Cells(GridColCaseIdIdx).FindControl(GridColCaseNumberCtrl) IsNot Nothing) Then
                        btnEditItem = CType(e.Row.Cells(GridColCaseIdIdx).FindControl(GridColCaseNumberCtrl), LinkButton)
                        btnEditItem.CommandArgument = GetGuidStringFromByteArray(CType(dvRow(CaseBase.CaseSearchDv.ColCaseId), Byte()))
                        btnEditItem.CommandName = SelectActionCommand
                        btnEditItem.Text = dvRow(CaseBase.CaseSearchDv.ColCaseNumber).ToString
                    End If

                    If (dvRow(CaseBase.CaseSearchDv.ColCaseStatusCode).ToString = Codes.CASE_STATUS__OPEN) Then
                        e.Row.Cells(GridColCaseStatusCodeIdx).CssClass = "StatActive"
                    Else
                        e.Row.Cells(GridColCaseStatusCodeIdx).CssClass = "StatInactive"
                    End If
                    Dim strOpenDate As String = Convert.ToString(e.Row.Cells(GridColCaseOpenDateIdx).Text)
                    strOpenDate = strOpenDate.Replace("&nbsp;", "")
                    If String.IsNullOrWhiteSpace(strOpenDate) = False Then
                        Dim tempOpenDate = Convert.ToDateTime(e.Row.Cells(GridColCaseOpenDateIdx).Text.Trim())
                        Dim formattedOpenDate = GetDateFormattedStringNullable(tempOpenDate)
                        e.Row.Cells(GridColCaseOpenDateIdx).Text = Convert.ToString(formattedOpenDate)
                    End If
                    Dim strCloseDate As String = Convert.ToString(e.Row.Cells(GridColCaseCloseDateIdx).Text)
                    strCloseDate = strCloseDate.Replace("&nbsp;", "")
                    If String.IsNullOrEmpty(strCloseDate) = False Then
                        Dim tempCloseDate = Convert.ToDateTime(e.Row.Cells(GridColCaseCloseDateIdx).Text.Trim())
                        Dim formattedCloseDate = GetDateFormattedStringNullable(tempCloseDate)
                        e.Row.Cells(GridColCaseCloseDateIdx).Text = Convert.ToString(formattedCloseDate)
                    End If


                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub
        Private Sub Grid_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles Grid.RowCommand
            Try
                If e.CommandName = SelectActionCommand Then
                    If Not e.CommandArgument.ToString().Equals(String.Empty) Then
                        State.selectedCaseId = New Guid(e.CommandArgument.ToString())
                        callPage(CaseDetailsForm.URL, State.selectedCaseId)
                    End If
                End If
            Catch ex As ThreadAbortException
            Catch ex As Exception
                If (TypeOf ex Is TargetInvocationException) AndAlso
               (TypeOf ex.InnerException Is ThreadAbortException) Then Return
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub
        Private Sub Grid_PageIndexChanged(sender As Object, e As EventArgs) Handles Grid.PageIndexChanged
            Try
                State.PageIndex = Grid.PageIndex
                State.selectedCaseId = Guid.Empty
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
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub
        Private Sub Grid_SortCommand(source As Object, e As GridViewSortEventArgs) Handles Grid.Sorting
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
        Private Sub Grid_RowCreated(sender As Object, e As GridViewRowEventArgs) Handles Grid.RowCreated
            Try
                BaseItemCreated(sender, e)
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub
#End Region
    End Class
End Namespace