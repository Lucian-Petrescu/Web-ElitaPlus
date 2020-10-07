Namespace Claims
    Partial Public Class ClaimDeniedLetterListForm
        Inherits ElitaPlusSearchPage
        'Implements IStateController

#Region "Variables"

        Private mbIsFirstPass As Boolean = True
        Public Const URL As String = "ClaimDeniedLetterListForm.aspx"
#End Region

#Region "Class NonTransient Members"
        Protected isReturningFromDetail As Boolean = False
#End Region
#Region "Parameters"

        Public Class Parameters
            Public moClaimbo As ClaimBase
            Public moClaimDenielLetters As Claims.LetterSearchDV
            Public moCliamID As Guid
            Public moClaimNumber As String
            Public moCertificate As String
            Public moCertItemCoverage As Guid


            Public Sub New(oClaimbo As ClaimBase, oClaimDenielLetters As claims.LetterSearchDV, oClaimID As Guid, oClaimNumber As String, oCertificate As String, oCertItemCoverage As Guid)
                ' If oClaimDenielLetters.Count < 0 Then
                moClaimbo = oClaimbo
                moClaimDenielLetters = oClaimDenielLetters
                moCliamID = oClaimID
                moClaimNumber = oClaimNumber
                moCertificate = oCertificate
                moCertItemCoverage = oCertItemCoverage
                ' End If
            End Sub

        End Class

#End Region
#Region "Constants"
        Public Const GRID_COL_EDIT_IDX As Integer = 4
        Public Const GRID_COL_DATE_IDX As Integer = 2
        'Public Const GRID_COL_CUSTOMER_NAME_IDX As Integer = 2
        'Public Const GRID_COL_SERVICE_CENTER_NAME_IDX As Integer = 3
        Public Const GRID_COL_DENIAL_RESON_IDX As Integer = 3
        Public Const GRID_COL_DENIAL_RESON_CODE_IDX As Integer = 1

        Public Const MAX_LIMIT As Integer = 1000

        Public Const PAGETITLE As String = "DENIAL_LETTER_LIST"
        Public Const PAGETAB As String = "Claims"

#End Region

#Region "Page State"
        Private IsReturningFromChild As Boolean = False

        Class MyState
            Public PageIndex As Integer = 0
            Public SortExpression As String = Claim.ClaimSearchDV.COL_CLAIM_NUMBER
            Public selectedClaimId As Guid = Guid.Empty
            Public createdDate As String
            Public denialReason As String
            Public moParams As Parameters
            Public denialReasonCode As String
            Public selectedSortById As Guid = Guid.Empty
            Public selectedPageSize As Int32 = ElitaPlusSearchPage.DEFAULT_PAGE_SIZE
            Public IsGridVisible As Boolean = False
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
                'Return CType(MyBase.State, MyState)
                If NavController.State Is Nothing Then
                    NavController.State = New MyState
                Else
                    If NavController.IsFlowEnded Then
                        'restart flow
                        Dim s As MyState = CType(NavController.State, MyState)
                        'Me.StartNavControl()
                        NavController.State = s
                    End If
                End If
                Return CType(NavController.State, MyState)
            End Get
        End Property

        
        Private Sub SetStateProperties()
            Try
                State.moParams = CType(NavController.ParametersPassed, Parameters)
                If (State.moParams Is Nothing) OrElse (State.moParams.moCliamID.Equals(Guid.Empty)) Then
                    Throw New DataNotFoundException
                End If
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub
#End Region


#Region "Page_Events"


        Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
            'Page.RegisterHiddenField("__EVENTTARGET", Me.btnSearch.ClientID)
            ErrControllerMaster.Clear_Hide()



            SetStateProperties()

            Try
                If Not IsPostBack Then
                    State.SearchClicked = True
                    State.PageIndex = 0
                    State.selectedClaimId = Guid.Empty
                    State.IsGridVisible = True
                    SetFormTitle(PAGETITLE)
                    SetFormTab(PAGETAB)


                    TranslateGridHeader(Grid)
                    TranslateGridControls(Grid)
                    PopulateGrid()

                    SetGridItemStyleColor(Grid)
                End If
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
                    ' Me.State.LetterSearchDV = Nothing
                End If
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try

        End Sub
#End Region

#Region "Controlling Logic"

        Public Sub PopulateGrid()

            Try

                Grid.AutoGenerateColumns = False
                State.PageIndex = Grid.PageIndex

                ControlMgr.SetVisibleControl(Me, Grid, State.IsGridVisible)

                ControlMgr.SetVisibleControl(Me, trPageSize, Grid.Visible)

                State.IsGridVisible = True
                State.bnoRow = False
                Grid.DataSource = State.moParams.moClaimDenielLetters
                Grid.AllowSorting = False
                Grid.DataBind()

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
                        Return Nothing
                End Select
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Function


        
        

#End Region


#Region " Datagrid Related "

        'The Binding LOgic is here
        Private Sub Grid_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowDataBound
            Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
            Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)

            Try
                If dvRow IsNot Nothing And Not State.bnoRow Then
                    If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then
                        PopulateControlFromBOProperty(e.Row.Cells(GRID_COL_DATE_IDX), dvRow(2))
                        PopulateControlFromBOProperty(e.Row.Cells(GRID_COL_DENIAL_RESON_CODE_IDX), dvRow(1))
                        PopulateControlFromBOProperty(e.Row.Cells(GRID_COL_DENIAL_RESON_IDX), dvRow(3))
                        PopulateControlFromBOProperty(e.Row.Cells(GRID_COL_EDIT_IDX), dvRow(4))
                        
                    End If
                End If
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

        Private Sub Grid_PageSizeChanged(source As Object, e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
            Try
                Grid.PageIndex = NewCurrentPageIndex(Grid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
                State.selectedPageSize = CType(cboPageSize.SelectedValue, Int32)
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub


        Public Sub ItemCommand(source As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs)

            Try
                Dim index As Integer = Nothing
                If (e.CommandArgument IsNot Nothing) AndAlso (CType(e.CommandArgument, String)).Length > 0 Then
                    index = Integer.Parse(CType(e.CommandArgument, String))
                End If
                If e.CommandName = "Select" Then
                    Dim row As GridViewRow = Grid.Rows(index)
                    State.selectedClaimId = New Guid(row.Cells(GRID_COL_EDIT_IDX).Text)
                    NavController.Navigate(Me, "denied_claims_next", New Claims.DeniedClaimsForm.Parameters(State.moParams.moClaimbo, State.selectedClaimId, State.moParams.moClaimNumber, State.moParams.moCertificate, State.moParams.moCertItemCoverage, False, State.moParams.moClaimDenielLetters))
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try

        End Sub

        Public Sub ItemCreated(sender As System.Object, e As System.Web.UI.WebControls.GridViewRowEventArgs)
            BaseItemCreated(sender, e)
        End Sub

        Private Sub Grid_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Grid.PageIndexChanging
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

        

#End Region

#Region "Error Handling"


#End Region


#Region "Navigation Handling"
        
#End Region

#Region "State Controller"
        
#End Region

        Private Sub btnNew_Click(sender As Object, e As System.EventArgs) Handles btnNew_WRITE.Click

            NavController.Navigate(Me, "new", New Claims.DeniedClaimsForm.Parameters(State.moParams.moClaimbo, State.moParams.moCliamID, State.moParams.moClaimNumber, State.moParams.moCertificate, State.moParams.moCertItemCoverage, True, State.moParams.moClaimDenielLetters))
        End Sub

        Private Sub btnBack_Click(sender As Object, e As System.EventArgs) Handles btnBack_WRITE.Click


            NavController.Navigate(Me, "back", New Claims.ClaimDeniedInformationForm.Parameters(State.moParams.moClaimbo, State.moParams.moClaimbo.Id, State.moParams.moCertItemCoverage))


        End Sub

    End Class
End Namespace