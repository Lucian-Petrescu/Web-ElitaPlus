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
            Public selectedPageSize As Int32 = ElitaPlusSearchPage.DEFAULT_PAGE_SIZE
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


        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            Page.RegisterHiddenField("__EVENTTARGET", Me.btnSearch.ClientID)
            Me.ErrControllerMaster.Clear_Hide()
            Try
                If Not Me.IsPostBack Then
                    Me.SetFormTitle(PAGETITLE)
                    Me.SetFormTab(PAGETAB)
                    Me.SetDefaultButton(Me.TextBoxSearchClaimNumber, btnSearch)
                    Me.SetDefaultButton(Me.TextBoxSearchCertificate, btnSearch)
                    Me.SetDefaultButton(Me.TextServiceCenterName, btnSearch)
                    ControlMgr.SetVisibleControl(Me, trPageSize, False)
                    'Me.State.selectedSortById = LookupListNew.GetIdFromCode(LookupListNew.LK_PENDING_APPROVAL_CLAIM_SEARCH_FIELDS, Codes.PENDING_CLAIM_SORT_COLUMN__CLAIM_NUMBER)
                    PopulateSearchFieldsFromState()
                    Me.TranslateGridHeader(Grid)
                    Me.TranslateGridControls(Grid)
                    If Me.State.IsGridVisible Then
                        If Not (Me.State.selectedPageSize = 10) Then
                            cboPageSize.SelectedValue = CType(Me.State.selectedPageSize, String)
                            Me.Grid.PageSize = Me.State.selectedPageSize
                        End If
                        Me.PopulateGrid()
                    End If
                    Me.SetGridItemStyleColor(Me.Grid)
                    SetFocus(Me.TextBoxSearchClaimNumber)
                End If
                Me.DisplayProgressBarOnClick(Me.btnSearch, "Loading_Pending_Approval_Claims")
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
            Me.ShowMissingTranslations(Me.ErrControllerMaster)
        End Sub

        Private Sub Page_PageReturn(ByVal ReturnFromUrl As String, ByVal ReturnPar As Object) Handles MyBase.PageReturn
            Try
                Me.MenuEnabled = True
                Me.IsReturningFromChild = True
                Dim retObj As ClaimForm.ReturnType = CType(Me.ReturnedValues, ClaimForm.ReturnType)
                If Not retObj Is Nothing AndAlso retObj.BoChanged Then
                    Me.State.searchDV = Nothing
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try

        End Sub
#End Region

#Region "Controlling Logic"

        Public Sub PopulateGrid()

            Try
                PopulateStateFromSearchFields()

                If (Me.State.searchDV Is Nothing) Then
                    Me.State.searchDV = Claim.GetPendingApprovalClaimList(Me.State.claimNumber, _
                                                                      Me.State.certificate, _
                                                                      Me.State.serviceCenterName)
                    If (Me.State.SearchClicked) Then
                        Me.ValidSearchResultCount(Me.State.searchDV.Count, True)
                        Me.State.SearchClicked = False
                    End If

                    State.searchDV.Sort = Me.GetSortByColumn
                End If

                Me.Grid.AutoGenerateColumns = False
                SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.selectedClaimId, Me.Grid, Me.State.PageIndex)
                Me.State.PageIndex = Me.Grid.PageIndex

                ControlMgr.SetVisibleControl(Me, Grid, Me.State.IsGridVisible)

                ControlMgr.SetVisibleControl(Me, trPageSize, Me.Grid.Visible)

                Session("recCount") = Me.State.searchDV.Count

                If Me.State.searchDV.Count > 0 Then
                    If Me.Grid.Visible Then
                        Me.lblRecordCount.Text = Me.State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_CLAIMS_FOUND)
                    End If
                    Me.State.bnoRow = False
                    Me.Grid.DataSource = Me.State.searchDV
                    Me.Grid.AllowSorting = False
                    Me.Grid.DataBind()
                Else
                    If Me.Grid.Visible Then
                        Me.lblRecordCount.Text = Me.State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_CLAIMS_FOUND)
                    End If
                    Me.State.bnoRow = True
                    CreateHeaderForEmptyGrid(Grid, " ASC")
                End If

                If Not Grid.BottomPagerRow.Visible Then
                    Grid.BottomPagerRow.Visible = True
                End If


            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try

        End Sub

        Function GetSortByColumn() As String
            Dim sortbyCode As String
            Try
                If (Not (Me.State.selectedSortById.Equals(Guid.Empty))) Then
                    sortbyCode = LookupListNew.GetCodeFromId(LookupListNew.LK_PENDING_CLAIM_SEARCH_FIELDS, Me.State.selectedSortById)
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
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Function

        Public Sub PopulateSearchFieldsFromState()

            Try
                Me.TextBoxSearchCertificate.Text = Me.State.certificate
                Me.TextBoxSearchClaimNumber.Text = Me.State.claimNumber
                Me.TextServiceCenterName.Text = Me.State.serviceCenterName
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try

        End Sub

        Public Sub PopulateStateFromSearchFields()

            Try
                Me.State.claimNumber = Me.TextBoxSearchClaimNumber.Text
                Me.State.certificate = Me.TextBoxSearchCertificate.Text
                Me.State.serviceCenterName = Me.TextServiceCenterName.Text
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try

        End Sub

        Public Sub ClearSearch()
            Me.TextBoxSearchClaimNumber.Text = String.Empty
            Me.TextBoxSearchCertificate.Text = String.Empty
            Me.TextServiceCenterName.Text = String.Empty
        End Sub

#End Region


#Region " Datagrid Related "

        'The Binding LOgic is here
        Private Sub Grid_ItemBound(ByVal source As Object, ByVal e As GridViewRowEventArgs) Handles Grid.RowDataBound
            Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
            Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)

            Try
                If Not dvRow Is Nothing And Not Me.State.bnoRow Then
                    If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then
                        Me.PopulateControlFromBOProperty(e.Row.Cells(Me.GRID_COL_CLAIM_ID_IDX), dvRow(Claim.PendingApprovalClaimSearchDV.COL_NAME_CLAIM_ID))
                        Me.PopulateControlFromBOProperty(e.Row.Cells(Me.GRID_COL_CLAIM_NUMBER_IDX), dvRow(Claim.PendingApprovalClaimSearchDV.COL_NAME_CLAIM_NUMBER))
                        Me.PopulateControlFromBOProperty(e.Row.Cells(Me.GRID_COL_CUSTOMER_NAME_IDX), dvRow(Claim.PendingApprovalClaimSearchDV.COL_NAME_CUSTOMER_NAME))
                        Me.PopulateControlFromBOProperty(e.Row.Cells(Me.GRID_COL_SERVICE_CENTER_NAME_IDX), dvRow(Claim.PendingApprovalClaimSearchDV.COL_NAME_SERVICE_CENTER_NAME))
                        Me.PopulateControlFromBOProperty(e.Row.Cells(Me.GRID_COL_STATUS_DATE_IDX), _
                                                         If(dvRow(Claim.PendingApprovalClaimSearchDV.COL_NAME_STATUS_DATE) Is DBNull.Value, _
                                                            Nothing, _
                                                            GetLongDateFormattedString(CType(dvRow(Claim.PendingApprovalClaimSearchDV.COL_NAME_STATUS_DATE), Date))))

                    End If
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

        Private Sub Grid_PageSizeChanged(ByVal source As Object, ByVal e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
            Try
                Grid.PageIndex = NewCurrentPageIndex(Grid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
                Me.State.selectedPageSize = CType(cboPageSize.SelectedValue, Int32)
                Me.PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub


        Public Sub RowCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)

            Try
                Dim index As Integer = Nothing
                If (Not e.CommandArgument Is Nothing) AndAlso (CType(e.CommandArgument, String)).Length > 0 Then
                    index = Integer.Parse(CType(e.CommandArgument, String))
                End If
                If e.CommandName = "Select" Then
                    Dim row As GridViewRow = Me.Grid.Rows(index)
                    Me.State.selectedClaimId = New Guid(row.Cells(Me.GRID_COL_CLAIM_ID_IDX).Text)
                    'Dim claimBo As Claim = New Claim(Me.State.selectedClaimId)
                    'Me.NavController.FlowSession(FlowSessionKeys.SESSION_CLAIM) = claimBo
                    'Me.NavController.Navigate(Me, FlowEvents.EVENT_CLAIM_SELECTED)
                    If Me.State Is Nothing Then
                        Me.Trace(Me, "Restoring State")
                        Me.RestoreState(New MyState)
                    End If
                    'Me.State.selectedClaimId = New Guid(e.Item.Cells(Me.GRID_COL_CLAIM_ID_IDX).Text)
                    Me.callPage(ClaimForm.URL, Me.State.selectedClaimId)

                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try

        End Sub

        Public Sub ItemCreated(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
            BaseItemCreated(sender, e)
        End Sub

        Private Sub Grid_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Grid.PageIndexChanging
            Try
                Me.State.PageIndex = e.NewPageIndex
                Me.State.selectedClaimId = Guid.Empty
                Me.PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub
#End Region

#Region " Button Clicks "

        Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
            Try
                Me.State.SearchClicked = True
                Me.State.PageIndex = 0
                Me.State.selectedClaimId = Guid.Empty
                Me.State.IsGridVisible = True
                Me.State.searchDV = Nothing
                Me.PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

        Private Sub btnClearSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearSearch.Click
            Try
                Me.ClearSearch()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

#End Region

    End Class
End Namespace