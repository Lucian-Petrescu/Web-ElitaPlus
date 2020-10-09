Imports System.Threading

Namespace Claims
    Partial Public Class PendingApprovalClaimListForm
        Inherits ElitaPlusSearchPage
        'Implements IStateController

#Region "Variables"

        Private mbIsFirstPass As Boolean = True

#End Region

#Region "Class NonTransient Members"
        Protected isReturningFromDetail As Boolean = False
#End Region

#Region "Constants"
        Public Const GRID_COL_EDIT_IDX As Integer = 0
        Public Const GRID_COL_CLAIM_NUMBER_IDX As Integer = 1
        Public Const GRID_COL_CUSTOMER_NAME_IDX As Integer = 2
        Public Const GRID_COL_SERVICE_CENTER_NAME_IDX As Integer = 3
        Public Const GRID_COL_STATUS_DATE_IDX As Integer = 4
        Public Const GRID_COL_CLAIM_ID_IDX As Integer = 5

        Public Const MAX_LIMIT As Integer = 1000

        Public Const PAGETITLE As String = "PENDING_APPROVAL_CLAIMS"
        Public Const PAGETAB As String = "Claims"

#End Region

#Region "Page State"
        Private IsReturningFromChild As Boolean = False

        Class MyState
            Public PageIndex As Integer = 0
            Public SortExpression As String = Claim.ClaimSearchDV.COL_CLAIM_NUMBER
            Public selectedClaimId As Guid = Guid.Empty
            Public claimNumber As String
            Public certificate As String
            Public serviceCenterName As String
            Public selectedSortById As Guid = Guid.Empty
            Public selectedPageSize As Int32 = DEFAULT_PAGE_SIZE
            Public IsGridVisible As Boolean = False
            Public searchDV As Claim.PendingApprovalClaimSearchDV = Nothing
            Public SearchClicked As Boolean
            Public bnoRow As Boolean = False

            Sub New()

            End Sub

        End Class

        Public Sub New()
            MyBase.New(New MyState)
        End Sub

        Protected Shadows ReadOnly Property State() As MyState
            Get

                Return CType(MyBase.State, MyState)

            End Get
        End Property

#End Region


#Region "Page_Events"


        Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
            Page.RegisterHiddenField("__EVENTTARGET", btnSearch.ClientID)
            ErrControllerMaster.Clear_Hide()
            Try
                If Not IsPostBack Then
                    SetFormTitle(PAGETITLE)
                    SetFormTab(PAGETAB)
                    SetDefaultButton(TextBoxSearchClaimNumber, btnSearch)
                    SetDefaultButton(TextBoxSearchCertificate, btnSearch)
                    SetDefaultButton(TextServiceCenterName, btnSearch)
                    ControlMgr.SetVisibleControl(Me, trPageSize, False)
                    'Me.State.selectedSortById = LookupListNew.GetIdFromCode(LookupListNew.LK_PENDING_APPROVAL_CLAIM_SEARCH_FIELDS, Codes.PENDING_CLAIM_SORT_COLUMN__CLAIM_NUMBER)
                    PopulateSearchFieldsFromState()
                    TranslateGridHeader(Grid)
                    TranslateGridControls(Grid)
                    If State.IsGridVisible Then
                        If Not (State.selectedPageSize = 10) Then
                            cboPageSize.SelectedValue = CType(State.selectedPageSize, String)
                            Grid.PageSize = State.selectedPageSize
                        End If
                        PopulateGrid()
                    End If
                    SetGridItemStyleColor(Grid)
                    SetFocus(TextBoxSearchClaimNumber)
                End If
                DisplayProgressBarOnClick(btnSearch, "Loading_Pending_Approval_Claims")
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
            ShowMissingTranslations(ErrControllerMaster)
        End Sub

        Private Sub Page_PageReturn(ReturnFromUrl As String, ReturnPar As Object) Handles MyBase.PageReturn
            Try
                MenuEnabled = True
                IsReturningFromChild = True
                Dim retObj As ClaimForm.ReturnType = CType(ReturnedValues, ClaimForm.ReturnType)
                If retObj IsNot Nothing AndAlso retObj.BoChanged Then
                    State.searchDV = Nothing
                End If
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try

        End Sub
#End Region

#Region "Controlling Logic"

        Public Sub PopulateGrid()

            Try
                PopulateStateFromSearchFields()

                If (State.searchDV Is Nothing) Then
                    State.searchDV = Claim.GetPendingApprovalClaimList(State.claimNumber, _
                                                                      State.certificate, _
                                                                      State.serviceCenterName)
                    If (State.SearchClicked) Then
                        ValidSearchResultCount(State.searchDV.Count, True)
                        State.SearchClicked = False
                    End If

                    State.searchDV.Sort = GetSortByColumn
                End If

                Grid.AutoGenerateColumns = False
                SetPageAndSelectedIndexFromGuid(State.searchDV, State.selectedClaimId, Grid, State.PageIndex)
                State.PageIndex = Grid.PageIndex

                ControlMgr.SetVisibleControl(Me, Grid, State.IsGridVisible)

                ControlMgr.SetVisibleControl(Me, trPageSize, Grid.Visible)

                Session("recCount") = State.searchDV.Count

                If State.searchDV.Count > 0 Then
                    If Grid.Visible Then
                        lblRecordCount.Text = State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_CLAIMS_FOUND)
                    End If
                    State.bnoRow = False
                    Grid.DataSource = State.searchDV
                    Grid.AllowSorting = False
                    Grid.DataBind()
                Else
                    If Grid.Visible Then
                        lblRecordCount.Text = State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_CLAIMS_FOUND)
                    End If
                    State.bnoRow = True
                    CreateHeaderForEmptyGrid(Grid, " ASC")
                End If

                If Not Grid.BottomPagerRow.Visible Then
                    Grid.BottomPagerRow.Visible = True
                End If


            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try

        End Sub

        Function GetSortByColumn() As String
            Dim sortbyCode As String
            Try
                If (Not (State.selectedSortById.Equals(Guid.Empty))) Then
                    sortbyCode = LookupListNew.GetCodeFromId(LookupListNew.LK_PENDING_CLAIM_SEARCH_FIELDS, State.selectedSortById)
                End If
                Select Case sortbyCode
                    Case Codes.PENDING_CLAIM_SORT_COLUMN__CLAIM_NUMBER
                        Return Claim.PendingApprovalClaimSearchDV.COL_NAME_CLAIM_NUMBER
                    Case Codes.PENDING_CLAIM_SORT_COLUMN__CUSTOMER_NAME
                        Return Claim.PendingApprovalClaimSearchDV.COL_NAME_CUSTOMER_NAME
                    Case Codes.PENDING_CLAIM_SORT_COLUMN__SERVICE_CENTER_NAME
                        Return Claim.PendingApprovalClaimSearchDV.COL_NAME_SERVICE_CENTER_NAME
                    Case Codes.PENDING_CLAIM_SORT_COLUMN__STATUS_DATE
                        Return Claim.PendingApprovalClaimSearchDV.COL_NAME_STATUS_DATE
                    Case Else
                        'Default order by status date descending
                        Return Claim.PendingApprovalClaimSearchDV.COL_NAME_STATUS_DATE ' & " ASC"
                End Select
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Function

        Public Sub PopulateSearchFieldsFromState()

            Try
                TextBoxSearchCertificate.Text = State.certificate
                TextBoxSearchClaimNumber.Text = State.claimNumber
                TextServiceCenterName.Text = State.serviceCenterName
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try

        End Sub

        Public Sub PopulateStateFromSearchFields()

            Try
                State.claimNumber = TextBoxSearchClaimNumber.Text
                State.certificate = TextBoxSearchCertificate.Text
                State.serviceCenterName = TextServiceCenterName.Text
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try

        End Sub

        Public Sub ClearSearch()
            TextBoxSearchClaimNumber.Text = String.Empty
            TextBoxSearchCertificate.Text = String.Empty
            TextServiceCenterName.Text = String.Empty
        End Sub

#End Region


#Region " Datagrid Related "

        'The Binding LOgic is here
        Private Sub Grid_ItemBound(source As Object, e As GridViewRowEventArgs) Handles Grid.RowDataBound
            Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
            Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)

            Try
                If dvRow IsNot Nothing AndAlso Not State.bnoRow Then
                    If itemType = ListItemType.Item OrElse itemType = ListItemType.AlternatingItem OrElse itemType = ListItemType.SelectedItem Then
                        PopulateControlFromBOProperty(e.Row.Cells(GRID_COL_CLAIM_ID_IDX), dvRow(Claim.PendingApprovalClaimSearchDV.COL_NAME_CLAIM_ID))
                        PopulateControlFromBOProperty(e.Row.Cells(GRID_COL_CLAIM_NUMBER_IDX), dvRow(Claim.PendingApprovalClaimSearchDV.COL_NAME_CLAIM_NUMBER))
                        PopulateControlFromBOProperty(e.Row.Cells(GRID_COL_CUSTOMER_NAME_IDX), dvRow(Claim.PendingApprovalClaimSearchDV.COL_NAME_CUSTOMER_NAME))
                        PopulateControlFromBOProperty(e.Row.Cells(GRID_COL_SERVICE_CENTER_NAME_IDX), dvRow(Claim.PendingApprovalClaimSearchDV.COL_NAME_SERVICE_CENTER_NAME))
                        PopulateControlFromBOProperty(e.Row.Cells(GRID_COL_STATUS_DATE_IDX), _
                                                         If(dvRow(Claim.PendingApprovalClaimSearchDV.COL_NAME_STATUS_DATE) Is DBNull.Value, _
                                                            Nothing, _
                                                            GetLongDateFormattedString(CType(dvRow(Claim.PendingApprovalClaimSearchDV.COL_NAME_STATUS_DATE), Date))))

                    End If
                End If
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

        Private Sub Grid_PageSizeChanged(source As Object, e As EventArgs) Handles cboPageSize.SelectedIndexChanged
            Try
                Grid.PageIndex = NewCurrentPageIndex(Grid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
                State.selectedPageSize = CType(cboPageSize.SelectedValue, Int32)
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub


        Public Sub RowCommand(source As Object, e As GridViewCommandEventArgs)

            Try
                Dim index As Integer = Nothing
                If (e.CommandArgument IsNot Nothing) AndAlso (CType(e.CommandArgument, String)).Length > 0 Then
                    index = Integer.Parse(CType(e.CommandArgument, String))
                End If
                If e.CommandName = "Select" Then
                    Dim row As GridViewRow = Grid.Rows(index)
                    State.selectedClaimId = New Guid(row.Cells(GRID_COL_CLAIM_ID_IDX).Text)
                    'Dim claimBo As Claim = New Claim(Me.State.selectedClaimId)
                    'Me.NavController.FlowSession(FlowSessionKeys.SESSION_CLAIM) = claimBo
                    'Me.NavController.Navigate(Me, FlowEvents.EVENT_CLAIM_SELECTED)
                    If State Is Nothing Then
                        Trace(Me, "Restoring State")
                        RestoreState(New MyState)
                    End If
                    'Me.State.selectedClaimId = New Guid(e.Item.Cells(Me.GRID_COL_CLAIM_ID_IDX).Text)
                    callPage(ClaimForm.URL, State.selectedClaimId)

                End If
            Catch ex As ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try

        End Sub

        Public Sub ItemCreated(sender As Object, e As GridViewRowEventArgs)
            BaseItemCreated(sender, e)
        End Sub

        Private Sub Grid_PageIndexChanged(source As Object, e As GridViewPageEventArgs) Handles Grid.PageIndexChanging
            Try
                State.PageIndex = e.NewPageIndex
                State.selectedClaimId = Guid.Empty
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub
#End Region

#Region " Button Clicks "

        Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
            Try
                State.SearchClicked = True
                State.PageIndex = 0
                State.selectedClaimId = Guid.Empty
                State.IsGridVisible = True
                State.searchDV = Nothing
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

        Private Sub btnClearSearch_Click(sender As Object, e As EventArgs) Handles btnClearSearch.Click
            Try
                ClearSearch()
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

#End Region

    End Class
End Namespace