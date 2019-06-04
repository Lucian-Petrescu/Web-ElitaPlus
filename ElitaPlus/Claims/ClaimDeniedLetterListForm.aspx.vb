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


            Public Sub New(ByVal oClaimbo As ClaimBase, ByVal oClaimDenielLetters As claims.LetterSearchDV, ByVal oClaimID As Guid, ByVal oClaimNumber As String, ByVal oCertificate As String, ByVal oCertItemCoverage As Guid)
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
                If Me.NavController.State Is Nothing Then
                    Me.NavController.State = New MyState
                Else
                    If Me.NavController.IsFlowEnded Then
                        'restart flow
                        Dim s As MyState = CType(Me.NavController.State, MyState)
                        'Me.StartNavControl()
                        Me.NavController.State = s
                    End If
                End If
                Return CType(Me.NavController.State, MyState)
            End Get
        End Property

        
        Private Sub SetStateProperties()
            Try
                Me.State.moParams = CType(Me.NavController.ParametersPassed, Parameters)
                If (Me.State.moParams Is Nothing) OrElse (Me.State.moParams.moCliamID.Equals(Guid.Empty)) Then
                    Throw New DataNotFoundException
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub
#End Region


#Region "Page_Events"


        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            'Page.RegisterHiddenField("__EVENTTARGET", Me.btnSearch.ClientID)
            Me.ErrControllerMaster.Clear_Hide()



            Me.SetStateProperties()

            Try
                If Not Me.IsPostBack Then
                    Me.State.SearchClicked = True
                    Me.State.PageIndex = 0
                    Me.State.selectedClaimId = Guid.Empty
                    Me.State.IsGridVisible = True
                    Me.SetFormTitle(PAGETITLE)
                    Me.SetFormTab(PAGETAB)


                    Me.TranslateGridHeader(Grid)
                    Me.TranslateGridControls(Grid)
                    Me.PopulateGrid()

                    Me.SetGridItemStyleColor(Me.Grid)
                End If
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
                    ' Me.State.LetterSearchDV = Nothing
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try

        End Sub
#End Region

#Region "Controlling Logic"

        Public Sub PopulateGrid()

            Try

                Me.Grid.AutoGenerateColumns = False
                Me.State.PageIndex = Me.Grid.PageIndex

                ControlMgr.SetVisibleControl(Me, Grid, Me.State.IsGridVisible)

                ControlMgr.SetVisibleControl(Me, trPageSize, Me.Grid.Visible)

                Me.State.IsGridVisible = True
                Me.State.bnoRow = False
                Me.Grid.DataSource = Me.State.moParams.moClaimDenielLetters
                Me.Grid.AllowSorting = False
                Me.Grid.DataBind()

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
                        Return Nothing
                End Select
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Function


        
        

#End Region


#Region " Datagrid Related "

        'The Binding LOgic is here
        Private Sub Grid_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowDataBound
            Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
            Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)

            Try
                If Not dvRow Is Nothing And Not Me.State.bnoRow Then
                    If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then
                        Me.PopulateControlFromBOProperty(e.Row.Cells(Me.GRID_COL_DATE_IDX), dvRow(2))
                        Me.PopulateControlFromBOProperty(e.Row.Cells(Me.GRID_COL_DENIAL_RESON_CODE_IDX), dvRow(1))
                        Me.PopulateControlFromBOProperty(e.Row.Cells(Me.GRID_COL_DENIAL_RESON_IDX), dvRow(3))
                        Me.PopulateControlFromBOProperty(e.Row.Cells(Me.GRID_COL_EDIT_IDX), dvRow(4))
                        
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


        Public Sub ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)

            Try
                Dim index As Integer = Nothing
                If (Not e.CommandArgument Is Nothing) AndAlso (CType(e.CommandArgument, String)).Length > 0 Then
                    index = Integer.Parse(CType(e.CommandArgument, String))
                End If
                If e.CommandName = "Select" Then
                    Dim row As GridViewRow = Me.Grid.Rows(index)
                    Me.State.selectedClaimId = New Guid(row.Cells(Me.GRID_COL_EDIT_IDX).Text)
                    Me.NavController.Navigate(Me, "denied_claims_next", New Claims.DeniedClaimsForm.Parameters(Me.State.moParams.moClaimbo, Me.State.selectedClaimId, Me.State.moParams.moClaimNumber, Me.State.moParams.moCertificate, Me.State.moParams.moCertItemCoverage, False, Me.State.moParams.moClaimDenielLetters))
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

        

#End Region

#Region "Error Handling"


#End Region


#Region "Navigation Handling"
        
#End Region

#Region "State Controller"
        
#End Region

        Private Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew_WRITE.Click

            Me.NavController.Navigate(Me, "new", New Claims.DeniedClaimsForm.Parameters(Me.State.moParams.moClaimbo, Me.State.moParams.moCliamID, Me.State.moParams.moClaimNumber, Me.State.moParams.moCertificate, Me.State.moParams.moCertItemCoverage, True, Me.State.moParams.moClaimDenielLetters))
        End Sub

        Private Sub btnBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack_WRITE.Click


            Me.NavController.Navigate(Me, "back", New Claims.ClaimDeniedInformationForm.Parameters(Me.State.moParams.moClaimbo, Me.State.moParams.moClaimbo.Id, Me.State.moParams.moCertItemCoverage))


        End Sub

    End Class
End Namespace