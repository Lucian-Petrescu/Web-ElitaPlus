Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports Assurant.Elita.Web.Forms
Imports System.Threading
Partial Class ClaimListByCommentType
    Inherits ElitaPlusSearchPage
    Implements IStateController

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents ErrorCtrl As ErrorController
    Protected WithEvents lblBlank As System.Web.UI.WebControls.Label
    Protected WithEvents trSortBy As System.Web.UI.HtmlControls.HtmlTableRow

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region


#Region "Constants"
    Public Const URL As String = "~\Claims\claimlistbycommenttype.aspx"
    Public Const GRID_COL_EDIT_IDX As Integer = 0
    Public Const GRID_COL_CLAIM_NUMBER_IDX As Integer = 1
    Public Const GRID_COL_AUTH_NUMBER_IDX As Integer = 2
    Public Const GRID_COL_STATUS_IDX As Integer = 3
    Public Const GRID_COL_CUSTOMER_NAME_IDX As Integer = 4
    Public Const GRID_COL_COMMENT_TYPE_IDX As Integer = 5
    Public Const GRID_COL_COMMENT_DETAILS_IDX As Integer = 6
    Public Const GRID_COL_CLAIM_ID_IDX As Integer = 7

    Public Const GRID_TOTAL_COLUMNS As Integer = 8

    Public Const CLAIM_STATUS_DESCRIPTION_ACTIVE As String = "Active"
    Public Const CLAIM_STATUS_DESCRIPTION_PENDING As String = "Pending"
    Public Const CLAIM_STATUS_DESCRIPTION_DENIED As String = "Denied"
    Public Const CLAIM_STATUS_DESCRIPTION_CLOSED As String = "Closed"

    Private Const MSG_RECORD_SAVED_OK As String = "MSG_RECORD_SAVED_OK"
    Private Const MSG_RECORD_NOT_SAVED As String = "MSG_RECORD_NOT_SAVED"

#End Region

#Region "Page State"
    Private IsReturningFromChild As Boolean = False

    Class MyState
        Public PageIndex As Integer = 0
        Public SortExpression As String = Claim.ClaimSearchDV.COL_CLAIM_NUMBER
        Public selectedClaimId As Guid = Guid.Empty
        Public claimNumber As String
        Public claimStatus As String = String.Empty
        Public customerName As String = String.Empty
        Public commentTypeId As Guid = Guid.Empty
        Public editedCommentType As String
        Public bnoRow As Boolean = False
        Public selectedSortById As Guid = Guid.Empty
        Public selectedPageSize As Integer = DEFAULT_PAGE_SIZE
        Public authorizationNumber As String
        Public IsGridVisible As Boolean = False
        Public IsAfterSave As Boolean
        Public Canceling As Boolean
        Public IsEditMode As Boolean
        Public AddingNewRow As Boolean
        Public searchDV As Claim.ClaimSearchDV = Nothing
        Public SearchClicked As Boolean

        Sub New()

        End Sub

    End Class

    Public Sub New()
        MyBase.New(New MyState)
    End Sub

    Protected Shadows ReadOnly Property State() As MyState
        Get
            'Return CType(MyBase.State, MyState)
            If Not Me.NavController Is Nothing Then
                If Me.NavController.State Is Nothing Then
                    Me.NavController.State = New MyState
                Else
                    If IsPendingClaimFlowStarted() Then
                        If Me.NavController.IsFlowEnded Then
                            'restart flow
                            Dim s As MyState = CType(Me.NavController.State, MyState)
                            Me.StartNavControlPendingClaim()
                            Me.NavController.State = s
                        End If
                        'ElseIf IsClaimDetailFlowStarted() Then
                        '    If Me.NavController.IsFlowEnded Then
                        '        'restart flow
                        '        Dim s As MyState = CType(Me.NavController.State, MyState)
                        '        Me.StartNavControlClaimDetail()
                        '        Me.NavController.State = s
                        '    End If
                    Else
                        Return CType(MyBase.State, MyState)
                    End If
                End If
                Return CType(Me.NavController.State, MyState)
            Else
                Return CType(MyBase.State, MyState)
            End If
        End Get
    End Property

#End Region


#Region "Page_Events"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Page.RegisterHiddenField("__EVENTTARGET", Me.btnSearch.ClientID)
        Me.ErrorCtrl.Clear_Hide()
        Try
            If Not Me.IsPostBack Then
                'This Code must be the first thing to execute
                If Not Me.IsReturningFromChild Then
                    Me.StartNavControlPendingClaim()
                End If
                Me.SetDefaultButton(Me.TextBoxSearchClaimNumber, btnSearch)
                Me.SetDefaultButton(Me.TextBoxSearchAuthorizationNumber, btnSearch)
                Me.SetDefaultButton(Me.TextBoxSearchCustomerName, btnSearch)
                ControlMgr.SetVisibleControl(Me, trPageSize, False)
                SetButtonsState()
                PopulateSortByDropDown()
                PopulateClaimStatusDropDown()
                PopulateCommentTypeDropdown()
                PopulateSearchFieldsFromState()

                If Me.State.IsGridVisible Then
                    If Not (Me.State.selectedPageSize = DEFAULT_PAGE_SIZE) Then
                        cboPageSize.SelectedValue = CType(Me.State.selectedPageSize, String)
                        Grid.PageSize = Me.State.selectedPageSize
                    End If
                    Me.PopulateGrid()
                End If
                Me.SetGridItemStyleColor(Me.Grid)
                SetFocus(Me.TextBoxSearchClaimNumber)
            End If
            Me.DisplayProgressBarOnClick(Me.btnSearch, "Loading_Claims")
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
        Me.ShowMissingTranslations(Me.ErrorCtrl)
    End Sub


    Private Sub Page_PageReturn(ByVal ReturnFromUrl As String, ByVal ReturnPar As Object) Handles MyBase.PageReturn
        Try
            Me.MenuEnabled = True
            Me.IsReturningFromChild = True
            'Dim strurl As String = Me.CalledUrl
            'Dim retObj As ClaimForm.ReturnType = CType(Me.ReturnedValues, ClaimForm.ReturnType)
            'If Not retObj Is Nothing AndAlso retObj.BoChanged Then
            '    Me.State.searchDV = Nothing
            'End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

#End Region

#Region "Controlling Logic"


    Private Sub SortSvc(ByVal oSortDv As DataView)
        Try
            If ElitaPlusIdentity.Current.ActiveUser.IsServiceCenter Then
                oSortDv.RowFilter = "(LANGUAGE_ID =  '" &
                  GuidControl.GuidToHexString(ElitaPlusIdentity.Current.ActiveUser.LanguageId) &
                  "') AND ((CODE = 'CLNUM') OR (CODE = 'CUSTN'))"
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

    Sub PopulateSortByDropDown()
        Dim oSortDv As DataView
        Try
            'oSortDv = LookupListNew.GetClaimSearchByCommentTypeFieldsLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId)
            'SortSvc(oSortDv)
            'Me.BindListControlToDataView(Me.cboSortBy, oSortDv, , , False)

            Dim commentSortList As ListItem() = CommonConfigManager.Current.ListManager.GetList("CMTYPSORT", Thread.CurrentPrincipal.GetLanguageCode())
            cboSortBy.Populate(commentSortList, New PopulateOptions() With
                                                 {
                                                   .AddBlankItem = False
                                                  })

            Dim defaultSelectedCodeId As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_CLAIM_SEARCH_BY_COMMENT_TYPE_FIELDS, Codes.DEFAULT_SORT_FOR_CLAIMS)

            If (Me.State.selectedSortById.Equals(Guid.Empty)) Then
                Me.SetSelectedItem(Me.cboSortBy, defaultSelectedCodeId)
                Me.State.selectedSortById = defaultSelectedCodeId
            Else
                Me.SetSelectedItem(Me.cboSortBy, Me.State.selectedSortById)
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub


    Sub PopulateClaimStatusDropDown()
        Try
            cboClaimStatus.Items.Add(New System.Web.UI.WebControls.ListItem(""))
            cboClaimStatus.Items.Add(New System.Web.UI.WebControls.ListItem(CLAIM_STATUS_DESCRIPTION_ACTIVE))
            cboClaimStatus.Items.Add(New System.Web.UI.WebControls.ListItem(CLAIM_STATUS_DESCRIPTION_DENIED))
            cboClaimStatus.Items.Add(New System.Web.UI.WebControls.ListItem(CLAIM_STATUS_DESCRIPTION_PENDING))
            cboClaimStatus.Items.Add(New System.Web.UI.WebControls.ListItem(CLAIM_STATUS_DESCRIPTION_CLOSED))
            If Not Me.State.claimStatus Is String.Empty Then
                Dim setClaimStatusText As String
                If Me.State.claimStatus = Codes.CLAIM_STATUS__ACTIVE Then
                    setClaimStatusText = CLAIM_STATUS_DESCRIPTION_ACTIVE
                ElseIf Me.State.claimStatus = Codes.CLAIM_STATUS__PENDING Then
                    setClaimStatusText = CLAIM_STATUS_DESCRIPTION_PENDING
                ElseIf Me.State.claimStatus = Codes.CLAIM_STATUS__DENIED Then
                    setClaimStatusText = CLAIM_STATUS_DESCRIPTION_DENIED
                ElseIf Me.State.claimStatus = Codes.CLAIM_STATUS__CLOSED Then
                    setClaimStatusText = CLAIM_STATUS_DESCRIPTION_CLOSED
                End If
                Me.SetSelectedItemByText(Me.cboClaimStatus, setClaimStatusText)
            End If

        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

    Public Sub PopulateCommentTypeDropdown(Optional ByVal commentTypeList As DropDownList = Nothing)

        Try
            ' commentTypeLk = LookupListNew.GetCommentTypeLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId)
            Dim commTypeList As ListItem() = CommonConfigManager.Current.ListManager.GetList("COMMT", Thread.CurrentPrincipal.GetLanguageCode())

            If commentTypeList Is Nothing Then
                'Me.BindListControlToDataView(Me.cboCommentType, commentTypeLk) '.BindListControlToDataView
                cboCommentType.Populate(commTypeList, New PopulateOptions() With
                                                 {
                                                   .AddBlankItem = True
                                                  })
                If Not Me.State.commentTypeId = Guid.Empty Then
                    Me.SetSelectedItem(Me.cboCommentType, Me.State.commentTypeId)
                End If
            Else
                'Me.BindListControlToDataView(commentTypeList, commentTypeLk) 'BindListTextToDataView
                commentTypeList.Populate(commTypeList, New PopulateOptions() With
                                                {
                                                  .AddBlankItem = True
                                                 })
            End If

        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub


    Public Sub PopulateGrid()

        Try
            If Not PopulateStateFromSearchFields() Then Exit Sub

            If (Me.State.searchDV Is Nothing) Then
                Dim sortBy As String
                sortBy = LookupListNew.GetCodeFromId(LookupListNew.LK_CLAIM_SEARCH_BY_COMMENT_TYPE_FIELDS, Me.State.selectedSortById)

                Me.State.searchDV = Claim.GetClaimsByCommentType(Me.State.claimNumber,
                                                                 Me.State.customerName,
                                                                 Me.State.authorizationNumber,
                                                                 Me.State.commentTypeId,
                                                                 Me.State.claimStatus,
                                                                 ElitaPlusIdentity.Current.ActiveUser.LanguageId,
                                                                 sortBy)
                If (Me.State.SearchClicked) Then
                    Me.ValidSearchResultCount(Me.State.searchDV.Count, True)
                    Me.State.SearchClicked = False
                End If
            End If

            Me.Grid.AutoGenerateColumns = False

            If (Me.State.IsAfterSave) Then
                Me.State.IsAfterSave = False
                SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.selectedClaimId, Me.Grid, Me.State.PageIndex)
            ElseIf (Me.State.IsEditMode) Then
                SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.selectedClaimId, Me.Grid, Me.State.PageIndex, Me.State.IsEditMode)
            Else
                SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Guid.Empty, Me.Grid, Me.State.PageIndex, Me.State.IsEditMode)
            End If

            Me.SortAndBindGrid()

        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub


    Private Sub PopulateCommentBOFromForm()

        Dim oclaim As Claim = ClaimFacade.Instance.GetClaim(Of Claim)(Me.State.selectedClaimId)
        Dim commentBO As Comment = oclaim.AddNewComment()

        Me.PopulateBOProperty(commentBO, "CallerName", ElitaPlusIdentity.Current.ActiveUser.UserName)

        Me.PopulateBOProperty(commentBO, "Comments", CType(Me.Grid.Items(Me.Grid.EditItemIndex).Cells(Me.GRID_COL_COMMENT_DETAILS_IDX).FindControl("moCommentDetail"), TextBox).Text)

        commentBO.CommentTypeId = LookupListNew.GetIdFromCode(LookupListNew.LK_COMMENT_TYPES, Codes.COMMENT_TYPE__CUSTOMER_CALL)

    End Sub


    Private Sub SortAndBindGrid()

        If (Me.State.searchDV.Count = 0) Then
            Me.State.bnoRow = True
            'CreateHeaderForEmptyGrid(Grid, SortDirection)
            Me.Grid.Enabled = True
            Me.Grid.DataSource = Me.State.searchDV
            Me.Grid.DataBind()
            ControlMgr.SetVisibleControl(Me, Grid, Me.State.IsGridVisible)
            ControlMgr.SetVisibleControl(Me, trPageSize, Me.Grid.Visible)
            'Grid.PagerSettings.Visible = True
            'If Not Grid.BottomPagerRow.Visible Then Grid.BottomPagerRow.Visible = True
        Else
            Me.State.bnoRow = False
            Me.Grid.Enabled = True
            Me.Grid.DataSource = Me.State.searchDV
            'HighLightSortColumn(Grid, Me.SortDirection)
            Me.Grid.DataBind()
            ControlMgr.SetVisibleControl(Me, Grid, Me.State.IsGridVisible)
            ControlMgr.SetVisibleControl(Me, trPageSize, Me.Grid.Visible)
            'If Not Grid.BottomPagerRow.Visible Then Grid.BottomPagerRow.Visible = True
        End If

        'If Not Grid.BottomPagerRow Is Nothing AndAlso Not Grid.BottomPagerRow.Visible Then Grid.BottomPagerRow.Visible = True

        If Me.State.searchDV.Count > 0 Then
            If Me.Grid.Visible Then
                If (Me.State.AddingNewRow) Then
                    Me.lblRecordCount.Text = (Me.State.searchDV.Count - 1) & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_CLAIMS_FOUND)
                Else
                    Me.lblRecordCount.Text = Me.State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_CLAIMS_FOUND)
                End If
            Else
                If Me.Grid.Visible Then
                    Me.lblRecordCount.Text = Me.State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_CLAIMS_FOUND)
                End If
            End If
        Else
            If Me.Grid.Visible Then
                Me.lblRecordCount.Text = Me.State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_CLAIMS_FOUND)
            End If
        End If

        ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me, Grid)

    End Sub

    Public Sub PopulateSearchFieldsFromState()
        Try

            Me.TextBoxSearchCustomerName.Text = Me.State.customerName
            Me.TextBoxSearchClaimNumber.Text = Me.State.claimNumber

            Me.TextBoxSearchAuthorizationNumber.Text = Me.State.authorizationNumber

            If Not Me.State.claimStatus Is Nothing AndAlso Not Me.State.claimStatus <> "" Then
                If Me.State.claimStatus = Codes.CLAIM_STATUS__ACTIVE Then
                    cboClaimStatus.SelectedItem.Text = CLAIM_STATUS_DESCRIPTION_ACTIVE
                ElseIf Me.State.claimStatus = Codes.CLAIM_STATUS__PENDING Then
                    cboClaimStatus.SelectedItem.Text = CLAIM_STATUS_DESCRIPTION_PENDING
                ElseIf Me.State.claimStatus = Codes.CLAIM_STATUS__DENIED Then
                    cboClaimStatus.SelectedItem.Text = CLAIM_STATUS_DESCRIPTION_DENIED
                ElseIf Me.State.claimStatus = Codes.CLAIM_STATUS__CLOSED Then
                    cboClaimStatus.SelectedItem.Text = CLAIM_STATUS_DESCRIPTION_CLOSED
                End If
            End If

            Me.SetSelectedItem(Me.cboCommentType, Me.State.commentTypeId)

            Me.SetSelectedItem(Me.cboSortBy, Me.State.selectedSortById)

        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

    Public Function PopulateStateFromSearchFields() As Boolean

        Try
            Me.State.claimNumber = Me.TextBoxSearchClaimNumber.Text
            Me.State.customerName = Me.TextBoxSearchCustomerName.Text

            Me.State.authorizationNumber = Me.TextBoxSearchAuthorizationNumber.Text
            Me.State.commentTypeId = Me.GetSelectedItem(Me.cboCommentType)
            Me.State.selectedSortById = Me.GetSelectedItem(Me.cboSortBy)

            If cboClaimStatus.SelectedItem.Text = "" Then
                Me.State.claimStatus = String.Empty
            ElseIf cboClaimStatus.SelectedItem.Text = CLAIM_STATUS_DESCRIPTION_ACTIVE Then
                Me.State.claimStatus = Codes.CLAIM_STATUS__ACTIVE
            ElseIf cboClaimStatus.SelectedItem.Text = CLAIM_STATUS_DESCRIPTION_PENDING Then
                Me.State.claimStatus = Codes.CLAIM_STATUS__PENDING
            ElseIf cboClaimStatus.SelectedItem.Text = CLAIM_STATUS_DESCRIPTION_DENIED Then
                Me.State.claimStatus = Codes.CLAIM_STATUS__DENIED
            ElseIf cboClaimStatus.SelectedItem.Text = CLAIM_STATUS_DESCRIPTION_CLOSED Then
                Me.State.claimStatus = Codes.CLAIM_STATUS__CLOSED
            End If

            Return True

        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try

    End Function

    Public Sub ClearSearch()
        Try
            Dim defaultSelectedCodeId As Guid =
                LookupListNew.GetIdFromCode(LookupListNew.LK_CLAIM_SEARCH_BY_COMMENT_TYPE_FIELDS, Codes.DEFAULT_SORT_FOR_CLAIMS)
            Me.TextBoxSearchClaimNumber.Text = String.Empty
            Me.TextBoxSearchCustomerName.Text = String.Empty
            Me.TextBoxSearchAuthorizationNumber.Text = String.Empty
            Me.SetSelectedItem(Me.cboSortBy, defaultSelectedCodeId)
            Me.cboClaimStatus.SelectedIndex = NO_ITEM_SELECTED_INDEX
            Me.cboCommentType.SelectedIndex = NO_ITEM_SELECTED_INDEX
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

#End Region


#Region " Datagrid Related "

    'The Binding LOgic is here
    Private Sub Grid_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles Grid.ItemDataBound
        Dim itemType As ListItemType = CType(e.Item.ItemType, ListItemType)
        Dim dvRow As DataRowView = CType(e.Item.DataItem, DataRowView)
        Dim lbClaimNumber As LinkButton = CType(e.Item.FindControl("btnClaimNum"), LinkButton)
        Dim commentTypeList As DropDownList = CType(e.Item.FindControl("moCommentType"), DropDownList)
        Dim commentDetail As TextBox = CType(e.Item.FindControl("moCommentDetail"), TextBox)

        Try
            If Not dvRow Is Nothing And Not Me.State.bnoRow Then
                If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Or itemType = ListItemType.EditItem Then
                    If (Me.State.IsEditMode = True AndAlso
                        Me.State.selectedClaimId.ToString.Equals(GetGuidStringFromByteArray(CType(dvRow(Claim.ClaimSearchDV.COL_CLAIM_ID), Byte())))) Then

                        Me.PopulateControlFromBOProperty(e.Item.Cells(Me.GRID_COL_CLAIM_ID_IDX), dvRow(Claim.ClaimSearchDV.COL_CLAIM_ID))
                        lbClaimNumber.Text = CType(dvRow(Claim.ClaimSearchDV.COL_CLAIM_NUMBER), String)
                        Me.PopulateControlFromBOProperty(e.Item.Cells(Me.GRID_COL_AUTH_NUMBER_IDX), dvRow(Claim.ClaimSearchDV.COL_AUTHORIZATION_NUMBER))
                        Me.PopulateControlFromBOProperty(e.Item.Cells(Me.GRID_COL_STATUS_IDX), dvRow(Claim.ClaimSearchDV.COL_CLAIM_STATUS))
                        Me.PopulateControlFromBOProperty(e.Item.Cells(Me.GRID_COL_CUSTOMER_NAME_IDX), dvRow(Claim.ClaimSearchDV.COL_CUSTOMER_NAME))
                        PopulateCommentTypeDropdown(commentTypeList)
                        'This should populate the Comment Type in the Edit Row of Grid and not in the Search Parameter 
                        Me.SetSelectedItemByText(commentTypeList, Me.State.editedCommentType)
                        commentDetail.Text = "" 'CType(dvRow(Claim.ClaimSearchDV.COL_COMMENTS), String)
                    Else
                        Me.PopulateControlFromBOProperty(e.Item.Cells(Me.GRID_COL_CLAIM_ID_IDX), dvRow(Claim.ClaimSearchDV.COL_CLAIM_ID))
                        lbClaimNumber.Text = CType(dvRow(Claim.ClaimSearchDV.COL_CLAIM_NUMBER), String)
                        Me.PopulateControlFromBOProperty(e.Item.Cells(Me.GRID_COL_AUTH_NUMBER_IDX), dvRow(Claim.ClaimSearchDV.COL_AUTHORIZATION_NUMBER))
                        Me.PopulateControlFromBOProperty(e.Item.Cells(Me.GRID_COL_STATUS_IDX), dvRow(Claim.ClaimSearchDV.COL_CLAIM_STATUS))
                        Me.PopulateControlFromBOProperty(e.Item.Cells(Me.GRID_COL_CUSTOMER_NAME_IDX), dvRow(Claim.ClaimSearchDV.COL_CUSTOMER_NAME))
                        Me.PopulateControlFromBOProperty(e.Item.Cells(Me.GRID_COL_COMMENT_TYPE_IDX), dvRow(Claim.ClaimSearchDV.COL_COMMENT_TYPE))
                        Me.PopulateControlFromBOProperty(e.Item.Cells(Me.GRID_COL_COMMENT_DETAILS_IDX), dvRow(Claim.ClaimSearchDV.COL_COMMENTS))
                    End If
                End If
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

    Private Sub Grid_PageSizeChanged(ByVal source As Object, ByVal e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
        Try
            Grid.CurrentPageIndex = NewCurrentPageIndex(Grid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
            Me.State.selectedPageSize = CType(cboPageSize.SelectedValue, Integer)
            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

    Public Sub ItemCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs)
        Dim nIndex As Integer
        Dim oCommentTypeDrop As DropDownList

        Try
            If e.CommandName = "EditRecord" Then
                If Me.State Is Nothing Then
                    Me.Trace(Me, "Restoring State")
                    Me.RestoreState(New MyState)
                End If

                'Set the IsEditMode flag to TRUE
                Me.State.IsEditMode = True

                Me.State.selectedClaimId = New Guid(e.Item.Cells(Me.GRID_COL_CLAIM_ID_IDX).Text)

                Me.State.editedCommentType = e.Item.Cells(Me.GRID_COL_COMMENT_TYPE_IDX).Text

                'Populate the Main Grid and then at ItemDataBound, Populate the dropdown within the selected Row
                Me.PopulateGrid()

                Me.State.PageIndex = Grid.CurrentPageIndex

                Me.SetButtonsState()

            ElseIf e.CommandName = "Select" Then
                Me.State.selectedClaimId = New Guid(e.Item.Cells(Me.GRID_COL_CLAIM_ID_IDX).Text)
                Dim claimBo As ClaimBase = ClaimFacade.Instance.GetClaim(Of ClaimBase)(Me.State.selectedClaimId)
                If claimBo.StatusCode = Codes.CLAIM_STATUS__PENDING Then
                    If (claimBo.ClaimAuthorizationType = ClaimAuthorizationType.Single) Then
                        Me.StartNavControlPendingClaim()
                        Me.NavController.FlowSession(FlowSessionKeys.SESSION_CLAIM) = claimBo
                        Me.NavController.Navigate(Me, FlowEvents.EVENT_PENDING_CLAIM_SELECTED)
                    Else
                        Me.NavController = Nothing
                        Me.callPage(ClaimWizardForm.URL, New ClaimWizardForm.Parameters(ClaimWizardForm.ClaimWizardSteps.Step3, Nothing, Nothing, claimBo))
                    End If
                Else
                    Me.callPage(ClaimForm.URL, Me.State.selectedClaimId)
                    'Me.StartNavControlClaimDetail()
                    'Dim oParam As ClaimForm.Parameters = New ClaimForm.Parameters(Me.State.selectedClaimId)
                    'Me.NavController.Navigate(Me, FlowEvents.EVENT_CLAIM_SELECTED, oParam)
                End If
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try

    End Sub

    Public Sub ItemCreated(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
        BaseItemCreated(sender, e)
    End Sub

    Private Sub Grid_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles Grid.PageIndexChanged
        Try
            Me.State.PageIndex = e.NewPageIndex
            Me.State.selectedClaimId = Guid.Empty
            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
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
            Me.State.selectedSortById = New Guid(Me.cboSortBy.SelectedValue)
            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

    Private Sub btnClearSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearSearch.Click
        Try
            Me.ClearSearch()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

    Private Sub SaveButton_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveButton_WRITE.Click
        Try

            Dim oclaim As Claim = ClaimFacade.Instance.GetClaim(Of Claim)(Me.State.selectedClaimId)

            'use below if comment has to be updated using a ClaimBO 
            Dim commentBO As Comment = oclaim.AddNewComment()

            commentBO.Comments = CType(Me.Grid.Items(Me.Grid.EditItemIndex).Cells(Me.GRID_COL_COMMENT_DETAILS_IDX).FindControl("moCommentDetail"), TextBox).Text
            commentBO.CommentTypeId = Me.GetSelectedItem(CType(Me.Grid.Items(Me.Grid.EditItemIndex).Cells(Me.GRID_COL_COMMENT_DETAILS_IDX).FindControl("moCommentType"), DropDownList))

            If (commentBO.IsDirty) Then

                oclaim.Save()
                'Me.State.IsGridAddNew = False
                Me.State.IsAfterSave = True
                Me.State.AddingNewRow = False
                Me.AddInfoMsg(Me.MSG_RECORD_SAVED_OK)
                Me.State.searchDV = Nothing
                Me.ReturnFromEditing()
            Else
                Me.AddInfoMsg(Me.MSG_RECORD_NOT_SAVED)
                Me.ReturnFromEditing()
            End If

        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try

    End Sub

#End Region


#Region "Helper Functions"

    Private Sub ReturnFromEditing()

        Grid.EditItemIndex = NO_ITEM_SELECTED_INDEX

        If Me.Grid.PageCount = 0 Then
            'if returning to the "1st time in" screen
            ControlMgr.SetVisibleControl(Me, Grid, False)
        Else
            ControlMgr.SetVisibleControl(Me, Grid, True)
        End If

        SetGridControls(Grid, True)
        Me.State.IsEditMode = False
        Me.PopulateGrid()
        Me.State.PageIndex = Grid.CurrentPageIndex
        SetButtonsState()

    End Sub

    Private Sub SetButtonsState()

        If (Me.State.IsEditMode) Then
            ControlMgr.SetVisibleControl(Me, SaveButton_WRITE, True)
            ControlMgr.SetVisibleControl(Me, UndoButton, True)
            'ControlMgr.SetVisibleControl(Me, btnAdd_WRITE, False)
            ControlMgr.SetEnableControl(Me, btnSearch, False)
            ControlMgr.SetEnableControl(Me, btnClearSearch, False)
            Me.MenuEnabled = False
            If (Me.cboPageSize.Visible) Then
                ControlMgr.SetEnableControl(Me, cboPageSize, False)
            End If
        Else
            ControlMgr.SetVisibleControl(Me, SaveButton_WRITE, False)
            ControlMgr.SetVisibleControl(Me, UndoButton, False)
            'ControlMgr.SetVisibleControl(Me, btnAdd_WRITE, True)
            ControlMgr.SetEnableControl(Me, btnSearch, True)
            ControlMgr.SetEnableControl(Me, btnClearSearch, True)
            Me.MenuEnabled = True
            If (Me.cboPageSize.Visible) Then
                ControlMgr.SetEnableControl(Me, Me.cboPageSize, True)
            End If
        End If

    End Sub

#End Region

    Private Sub UndoButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles UndoButton.Click
        Try
            Me.Grid.SelectedIndex = Me.NO_ITEM_SELECTED_INDEX
            Me.State.Canceling = True
            If (Me.State.AddingNewRow) Then
                Me.State.AddingNewRow = False
                Me.State.searchDV = Nothing
            End If
            ReturnFromEditing()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

#Region "Navigation Handling"
    Public Sub Process(ByVal callingPage As Page, ByVal navCtrl As INavigationController) Implements IStateController.Process
        Try
            If Not Me.IsPostBack AndAlso navCtrl.CurrentFlow.Name = Me.FLOW_NAME_PENDING_CLAIM AndAlso
                        Not navCtrl.PrevNavState Is Nothing Then
                Me.IsReturningFromChild = True
                If navCtrl.IsFlowEnded Then
                    Me.State.searchDV = Nothing 'This will force a reload
                    'restart the flow
                    Dim savedState As MyState = CType(navCtrl.State, MyState)
                    Me.StartNavControlPendingClaim()
                    Me.NavController.State = savedState
                End If
                'ElseIf Not Me.IsPostBack AndAlso navCtrl.CurrentFlow.Name = Me.FLOW_NAME_CLAIM_DETAIL AndAlso _
                '            Not navCtrl.PrevNavState Is Nothing Then
                '    Me.IsReturningFromChild = True
                '    If navCtrl.IsFlowEnded Then
                '        Me.State.searchDV = Nothing 'This will force a reload
                '        'restart the flow
                '        Dim savedState As MyState = CType(navCtrl.State, MyState)
                '        Me.StartNavControlClaimDetail()
                '        Me.NavController.State = savedState
                '    End If
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub
#End Region

#Region "State Controller"
    Public Const FLOW_NAME_PENDING_CLAIM As String = "AUTHORIZE_PENDING_CLAIM_FROM_SEARCH_BY_COMMENT_TYPE"
    'Public Const FLOW_NAME_CLAIM_DETAIL As String = "CLAIM_DETAIL_FROM_SEARCH_BY_COMMENT_TYPE"
    Sub StartNavControlPendingClaim()
        Dim nav As New ElitaPlusNavigation
        Me.NavController = New NavControllerBase(nav.Flow(FLOW_NAME_PENDING_CLAIM))
        If Not Me.Navigator.PageState Is Nothing Then
            Me.NavController.State = Me.Navigator.PageState
        Else
            Me.NavController.State = New MyState
        End If
    End Sub
    'Sub StartNavControlClaimDetail()
    '    Dim nav As New ElitaPlusNavigation
    '    Me.NavController = New NavControllerBase(nav.Flow(FLOW_NAME_CLAIM_DETAIL))
    '    If Not Me.Navigator.PageState Is Nothing Then
    '        Me.NavController.State = Me.Navigator.PageState
    '    Else
    '        Me.NavController.State = New MyState
    '    End If
    'End Sub
    Function IsPendingClaimFlowStarted() As Boolean
        Return Not Me.NavController Is Nothing AndAlso Me.NavController.CurrentFlow.Name = Me.FLOW_NAME_PENDING_CLAIM
    End Function
    'Function IsClaimDetailFlowStarted() As Boolean
    '    Return Not Me.NavController Is Nothing AndAlso Me.NavController.CurrentFlow.Name = Me.FLOW_NAME_CLAIM_DETAIL
    'End Function
#End Region

End Class

