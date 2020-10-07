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

    Private Sub Page_Init(sender As System.Object, e As System.EventArgs) Handles MyBase.Init
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
            If NavController IsNot Nothing Then
                If NavController.State Is Nothing Then
                    NavController.State = New MyState
                Else
                    If IsPendingClaimFlowStarted() Then
                        If NavController.IsFlowEnded Then
                            'restart flow
                            Dim s As MyState = CType(NavController.State, MyState)
                            StartNavControlPendingClaim()
                            NavController.State = s
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
                Return CType(NavController.State, MyState)
            Else
                Return CType(MyBase.State, MyState)
            End If
        End Get
    End Property

#End Region


#Region "Page_Events"

    Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Page.RegisterHiddenField("__EVENTTARGET", btnSearch.ClientID)
        ErrorCtrl.Clear_Hide()
        Try
            If Not IsPostBack Then
                'This Code must be the first thing to execute
                If Not IsReturningFromChild Then
                    StartNavControlPendingClaim()
                End If
                SetDefaultButton(TextBoxSearchClaimNumber, btnSearch)
                SetDefaultButton(TextBoxSearchAuthorizationNumber, btnSearch)
                SetDefaultButton(TextBoxSearchCustomerName, btnSearch)
                ControlMgr.SetVisibleControl(Me, trPageSize, False)
                SetButtonsState()
                PopulateSortByDropDown()
                PopulateClaimStatusDropDown()
                PopulateCommentTypeDropdown()
                PopulateSearchFieldsFromState()

                If State.IsGridVisible Then
                    If Not (State.selectedPageSize = DEFAULT_PAGE_SIZE) Then
                        cboPageSize.SelectedValue = CType(State.selectedPageSize, String)
                        Grid.PageSize = State.selectedPageSize
                    End If
                    PopulateGrid()
                End If
                SetGridItemStyleColor(Grid)
                SetFocus(TextBoxSearchClaimNumber)
            End If
            DisplayProgressBarOnClick(btnSearch, "Loading_Claims")
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
        ShowMissingTranslations(ErrorCtrl)
    End Sub


    Private Sub Page_PageReturn(ReturnFromUrl As String, ReturnPar As Object) Handles MyBase.PageReturn
        Try
            MenuEnabled = True
            IsReturningFromChild = True
            'Dim strurl As String = Me.CalledUrl
            'Dim retObj As ClaimForm.ReturnType = CType(Me.ReturnedValues, ClaimForm.ReturnType)
            'If Not retObj Is Nothing AndAlso retObj.BoChanged Then
            '    Me.State.searchDV = Nothing
            'End If
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

#End Region

#Region "Controlling Logic"


    Private Sub SortSvc(oSortDv As DataView)
        Try
            If ElitaPlusIdentity.Current.ActiveUser.IsServiceCenter Then
                oSortDv.RowFilter = "(LANGUAGE_ID =  '" &
                  GuidControl.GuidToHexString(ElitaPlusIdentity.Current.ActiveUser.LanguageId) &
                  "') AND ((CODE = 'CLNUM') OR (CODE = 'CUSTN'))"
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
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

            If (State.selectedSortById.Equals(Guid.Empty)) Then
                SetSelectedItem(cboSortBy, defaultSelectedCodeId)
                State.selectedSortById = defaultSelectedCodeId
            Else
                SetSelectedItem(cboSortBy, State.selectedSortById)
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub


    Sub PopulateClaimStatusDropDown()
        Try
            cboClaimStatus.Items.Add(New System.Web.UI.WebControls.ListItem(""))
            cboClaimStatus.Items.Add(New System.Web.UI.WebControls.ListItem(CLAIM_STATUS_DESCRIPTION_ACTIVE))
            cboClaimStatus.Items.Add(New System.Web.UI.WebControls.ListItem(CLAIM_STATUS_DESCRIPTION_DENIED))
            cboClaimStatus.Items.Add(New System.Web.UI.WebControls.ListItem(CLAIM_STATUS_DESCRIPTION_PENDING))
            cboClaimStatus.Items.Add(New System.Web.UI.WebControls.ListItem(CLAIM_STATUS_DESCRIPTION_CLOSED))
            If State.claimStatus IsNot String.Empty Then
                Dim setClaimStatusText As String
                If State.claimStatus = Codes.CLAIM_STATUS__ACTIVE Then
                    setClaimStatusText = CLAIM_STATUS_DESCRIPTION_ACTIVE
                ElseIf State.claimStatus = Codes.CLAIM_STATUS__PENDING Then
                    setClaimStatusText = CLAIM_STATUS_DESCRIPTION_PENDING
                ElseIf State.claimStatus = Codes.CLAIM_STATUS__DENIED Then
                    setClaimStatusText = CLAIM_STATUS_DESCRIPTION_DENIED
                ElseIf State.claimStatus = Codes.CLAIM_STATUS__CLOSED Then
                    setClaimStatusText = CLAIM_STATUS_DESCRIPTION_CLOSED
                End If
                SetSelectedItemByText(cboClaimStatus, setClaimStatusText)
            End If

        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
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
                If Not State.commentTypeId = Guid.Empty Then
                    SetSelectedItem(cboCommentType, State.commentTypeId)
                End If
            Else
                'Me.BindListControlToDataView(commentTypeList, commentTypeLk) 'BindListTextToDataView
                commentTypeList.Populate(commTypeList, New PopulateOptions() With
                                                {
                                                  .AddBlankItem = True
                                                 })
            End If

        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub


    Public Sub PopulateGrid()

        Try
            If Not PopulateStateFromSearchFields() Then Exit Sub

            If (State.searchDV Is Nothing) Then
                Dim sortBy As String
                sortBy = LookupListNew.GetCodeFromId(LookupListNew.LK_CLAIM_SEARCH_BY_COMMENT_TYPE_FIELDS, State.selectedSortById)

                State.searchDV = Claim.GetClaimsByCommentType(State.claimNumber,
                                                                 State.customerName,
                                                                 State.authorizationNumber,
                                                                 State.commentTypeId,
                                                                 State.claimStatus,
                                                                 ElitaPlusIdentity.Current.ActiveUser.LanguageId,
                                                                 sortBy)
                If (State.SearchClicked) Then
                    ValidSearchResultCount(State.searchDV.Count, True)
                    State.SearchClicked = False
                End If
            End If

            Grid.AutoGenerateColumns = False

            If (State.IsAfterSave) Then
                State.IsAfterSave = False
                SetPageAndSelectedIndexFromGuid(State.searchDV, State.selectedClaimId, Grid, State.PageIndex)
            ElseIf (State.IsEditMode) Then
                SetPageAndSelectedIndexFromGuid(State.searchDV, State.selectedClaimId, Grid, State.PageIndex, State.IsEditMode)
            Else
                SetPageAndSelectedIndexFromGuid(State.searchDV, Guid.Empty, Grid, State.PageIndex, State.IsEditMode)
            End If

            SortAndBindGrid()

        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub


    Private Sub PopulateCommentBOFromForm()

        Dim oclaim As Claim = ClaimFacade.Instance.GetClaim(Of Claim)(State.selectedClaimId)
        Dim commentBO As Comment = oclaim.AddNewComment()

        PopulateBOProperty(commentBO, "CallerName", ElitaPlusIdentity.Current.ActiveUser.UserName)

        PopulateBOProperty(commentBO, "Comments", CType(Grid.Items(Grid.EditItemIndex).Cells(GRID_COL_COMMENT_DETAILS_IDX).FindControl("moCommentDetail"), TextBox).Text)

        commentBO.CommentTypeId = LookupListNew.GetIdFromCode(LookupListNew.LK_COMMENT_TYPES, Codes.COMMENT_TYPE__CUSTOMER_CALL)

    End Sub


    Private Sub SortAndBindGrid()

        If (State.searchDV.Count = 0) Then
            State.bnoRow = True
            'CreateHeaderForEmptyGrid(Grid, SortDirection)
            Grid.Enabled = True
            Grid.DataSource = State.searchDV
            Grid.DataBind()
            ControlMgr.SetVisibleControl(Me, Grid, State.IsGridVisible)
            ControlMgr.SetVisibleControl(Me, trPageSize, Grid.Visible)
            'Grid.PagerSettings.Visible = True
            'If Not Grid.BottomPagerRow.Visible Then Grid.BottomPagerRow.Visible = True
        Else
            State.bnoRow = False
            Grid.Enabled = True
            Grid.DataSource = State.searchDV
            'HighLightSortColumn(Grid, Me.SortDirection)
            Grid.DataBind()
            ControlMgr.SetVisibleControl(Me, Grid, State.IsGridVisible)
            ControlMgr.SetVisibleControl(Me, trPageSize, Grid.Visible)
            'If Not Grid.BottomPagerRow.Visible Then Grid.BottomPagerRow.Visible = True
        End If

        'If Not Grid.BottomPagerRow Is Nothing AndAlso Not Grid.BottomPagerRow.Visible Then Grid.BottomPagerRow.Visible = True

        If State.searchDV.Count > 0 Then
            If Grid.Visible Then
                If (State.AddingNewRow) Then
                    lblRecordCount.Text = (State.searchDV.Count - 1) & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_CLAIMS_FOUND)
                Else
                    lblRecordCount.Text = State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_CLAIMS_FOUND)
                End If
            Else
                If Grid.Visible Then
                    lblRecordCount.Text = State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_CLAIMS_FOUND)
                End If
            End If
        Else
            If Grid.Visible Then
                lblRecordCount.Text = State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_CLAIMS_FOUND)
            End If
        End If

        ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me, Grid)

    End Sub

    Public Sub PopulateSearchFieldsFromState()
        Try

            TextBoxSearchCustomerName.Text = State.customerName
            TextBoxSearchClaimNumber.Text = State.claimNumber

            TextBoxSearchAuthorizationNumber.Text = State.authorizationNumber

            If State.claimStatus IsNot Nothing AndAlso Not State.claimStatus <> "" Then
                If State.claimStatus = Codes.CLAIM_STATUS__ACTIVE Then
                    cboClaimStatus.SelectedItem.Text = CLAIM_STATUS_DESCRIPTION_ACTIVE
                ElseIf State.claimStatus = Codes.CLAIM_STATUS__PENDING Then
                    cboClaimStatus.SelectedItem.Text = CLAIM_STATUS_DESCRIPTION_PENDING
                ElseIf State.claimStatus = Codes.CLAIM_STATUS__DENIED Then
                    cboClaimStatus.SelectedItem.Text = CLAIM_STATUS_DESCRIPTION_DENIED
                ElseIf State.claimStatus = Codes.CLAIM_STATUS__CLOSED Then
                    cboClaimStatus.SelectedItem.Text = CLAIM_STATUS_DESCRIPTION_CLOSED
                End If
            End If

            SetSelectedItem(cboCommentType, State.commentTypeId)

            SetSelectedItem(cboSortBy, State.selectedSortById)

        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

    Public Function PopulateStateFromSearchFields() As Boolean

        Try
            State.claimNumber = TextBoxSearchClaimNumber.Text
            State.customerName = TextBoxSearchCustomerName.Text

            State.authorizationNumber = TextBoxSearchAuthorizationNumber.Text
            State.commentTypeId = GetSelectedItem(cboCommentType)
            State.selectedSortById = GetSelectedItem(cboSortBy)

            If cboClaimStatus.SelectedItem.Text = "" Then
                State.claimStatus = String.Empty
            ElseIf cboClaimStatus.SelectedItem.Text = CLAIM_STATUS_DESCRIPTION_ACTIVE Then
                State.claimStatus = Codes.CLAIM_STATUS__ACTIVE
            ElseIf cboClaimStatus.SelectedItem.Text = CLAIM_STATUS_DESCRIPTION_PENDING Then
                State.claimStatus = Codes.CLAIM_STATUS__PENDING
            ElseIf cboClaimStatus.SelectedItem.Text = CLAIM_STATUS_DESCRIPTION_DENIED Then
                State.claimStatus = Codes.CLAIM_STATUS__DENIED
            ElseIf cboClaimStatus.SelectedItem.Text = CLAIM_STATUS_DESCRIPTION_CLOSED Then
                State.claimStatus = Codes.CLAIM_STATUS__CLOSED
            End If

            Return True

        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try

    End Function

    Public Sub ClearSearch()
        Try
            Dim defaultSelectedCodeId As Guid =
                LookupListNew.GetIdFromCode(LookupListNew.LK_CLAIM_SEARCH_BY_COMMENT_TYPE_FIELDS, Codes.DEFAULT_SORT_FOR_CLAIMS)
            TextBoxSearchClaimNumber.Text = String.Empty
            TextBoxSearchCustomerName.Text = String.Empty
            TextBoxSearchAuthorizationNumber.Text = String.Empty
            SetSelectedItem(cboSortBy, defaultSelectedCodeId)
            cboClaimStatus.SelectedIndex = NO_ITEM_SELECTED_INDEX
            cboCommentType.SelectedIndex = NO_ITEM_SELECTED_INDEX
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

#End Region


#Region " Datagrid Related "

    'The Binding LOgic is here
    Private Sub Grid_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles Grid.ItemDataBound
        Dim itemType As ListItemType = CType(e.Item.ItemType, ListItemType)
        Dim dvRow As DataRowView = CType(e.Item.DataItem, DataRowView)
        Dim lbClaimNumber As LinkButton = CType(e.Item.FindControl("btnClaimNum"), LinkButton)
        Dim commentTypeList As DropDownList = CType(e.Item.FindControl("moCommentType"), DropDownList)
        Dim commentDetail As TextBox = CType(e.Item.FindControl("moCommentDetail"), TextBox)

        Try
            If dvRow IsNot Nothing AndAlso Not State.bnoRow Then
                If itemType = ListItemType.Item OrElse itemType = ListItemType.AlternatingItem OrElse itemType = ListItemType.SelectedItem OrElse itemType = ListItemType.EditItem Then
                    If (State.IsEditMode = True AndAlso
                        State.selectedClaimId.ToString.Equals(GetGuidStringFromByteArray(CType(dvRow(Claim.ClaimSearchDV.COL_CLAIM_ID), Byte())))) Then

                        PopulateControlFromBOProperty(e.Item.Cells(GRID_COL_CLAIM_ID_IDX), dvRow(Claim.ClaimSearchDV.COL_CLAIM_ID))
                        lbClaimNumber.Text = CType(dvRow(Claim.ClaimSearchDV.COL_CLAIM_NUMBER), String)
                        PopulateControlFromBOProperty(e.Item.Cells(GRID_COL_AUTH_NUMBER_IDX), dvRow(Claim.ClaimSearchDV.COL_AUTHORIZATION_NUMBER))
                        PopulateControlFromBOProperty(e.Item.Cells(GRID_COL_STATUS_IDX), dvRow(Claim.ClaimSearchDV.COL_CLAIM_STATUS))
                        PopulateControlFromBOProperty(e.Item.Cells(GRID_COL_CUSTOMER_NAME_IDX), dvRow(Claim.ClaimSearchDV.COL_CUSTOMER_NAME))
                        PopulateCommentTypeDropdown(commentTypeList)
                        'This should populate the Comment Type in the Edit Row of Grid and not in the Search Parameter 
                        SetSelectedItemByText(commentTypeList, State.editedCommentType)
                        commentDetail.Text = "" 'CType(dvRow(Claim.ClaimSearchDV.COL_COMMENTS), String)
                    Else
                        PopulateControlFromBOProperty(e.Item.Cells(GRID_COL_CLAIM_ID_IDX), dvRow(Claim.ClaimSearchDV.COL_CLAIM_ID))
                        lbClaimNumber.Text = CType(dvRow(Claim.ClaimSearchDV.COL_CLAIM_NUMBER), String)
                        PopulateControlFromBOProperty(e.Item.Cells(GRID_COL_AUTH_NUMBER_IDX), dvRow(Claim.ClaimSearchDV.COL_AUTHORIZATION_NUMBER))
                        PopulateControlFromBOProperty(e.Item.Cells(GRID_COL_STATUS_IDX), dvRow(Claim.ClaimSearchDV.COL_CLAIM_STATUS))
                        PopulateControlFromBOProperty(e.Item.Cells(GRID_COL_CUSTOMER_NAME_IDX), dvRow(Claim.ClaimSearchDV.COL_CUSTOMER_NAME))
                        PopulateControlFromBOProperty(e.Item.Cells(GRID_COL_COMMENT_TYPE_IDX), dvRow(Claim.ClaimSearchDV.COL_COMMENT_TYPE))
                        PopulateControlFromBOProperty(e.Item.Cells(GRID_COL_COMMENT_DETAILS_IDX), dvRow(Claim.ClaimSearchDV.COL_COMMENTS))
                    End If
                End If
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

    Private Sub Grid_PageSizeChanged(source As Object, e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
        Try
            Grid.CurrentPageIndex = NewCurrentPageIndex(Grid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
            State.selectedPageSize = CType(cboPageSize.SelectedValue, Integer)
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

    Public Sub ItemCommand(source As System.Object, e As System.Web.UI.WebControls.DataGridCommandEventArgs)
        Dim nIndex As Integer
        Dim oCommentTypeDrop As DropDownList

        Try
            If e.CommandName = "EditRecord" Then
                If State Is Nothing Then
                    Trace(Me, "Restoring State")
                    RestoreState(New MyState)
                End If

                'Set the IsEditMode flag to TRUE
                State.IsEditMode = True

                State.selectedClaimId = New Guid(e.Item.Cells(GRID_COL_CLAIM_ID_IDX).Text)

                State.editedCommentType = e.Item.Cells(GRID_COL_COMMENT_TYPE_IDX).Text

                'Populate the Main Grid and then at ItemDataBound, Populate the dropdown within the selected Row
                PopulateGrid()

                State.PageIndex = Grid.CurrentPageIndex

                SetButtonsState()

            ElseIf e.CommandName = "Select" Then
                State.selectedClaimId = New Guid(e.Item.Cells(GRID_COL_CLAIM_ID_IDX).Text)
                Dim claimBo As ClaimBase = ClaimFacade.Instance.GetClaim(Of ClaimBase)(State.selectedClaimId)
                If claimBo.StatusCode = Codes.CLAIM_STATUS__PENDING Then
                    If (claimBo.ClaimAuthorizationType = ClaimAuthorizationType.Single) Then
                        StartNavControlPendingClaim()
                        NavController.FlowSession(FlowSessionKeys.SESSION_CLAIM) = claimBo
                        NavController.Navigate(Me, FlowEvents.EVENT_PENDING_CLAIM_SELECTED)
                    Else
                        NavController = Nothing
                        callPage(ClaimWizardForm.URL, New ClaimWizardForm.Parameters(ClaimWizardForm.ClaimWizardSteps.Step3, Nothing, Nothing, claimBo))
                    End If
                Else
                    callPage(ClaimForm.URL, State.selectedClaimId)
                    'Me.StartNavControlClaimDetail()
                    'Dim oParam As ClaimForm.Parameters = New ClaimForm.Parameters(Me.State.selectedClaimId)
                    'Me.NavController.Navigate(Me, FlowEvents.EVENT_CLAIM_SELECTED, oParam)
                End If
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try

    End Sub

    Public Sub ItemCreated(sender As System.Object, e As System.Web.UI.WebControls.DataGridItemEventArgs)
        BaseItemCreated(sender, e)
    End Sub

    Private Sub Grid_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles Grid.PageIndexChanged
        Try
            State.PageIndex = e.NewPageIndex
            State.selectedClaimId = Guid.Empty
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub
#End Region

#Region " Button Clicks "

    Private Sub btnSearch_Click(sender As System.Object, e As System.EventArgs) Handles btnSearch.Click
        Try
            State.SearchClicked = True
            State.PageIndex = 0
            State.selectedClaimId = Guid.Empty
            State.IsGridVisible = True
            State.searchDV = Nothing
            State.selectedSortById = New Guid(cboSortBy.SelectedValue)
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

    Private Sub btnClearSearch_Click(sender As System.Object, e As System.EventArgs) Handles btnClearSearch.Click
        Try
            ClearSearch()
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

    Private Sub SaveButton_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles SaveButton_WRITE.Click
        Try

            Dim oclaim As Claim = ClaimFacade.Instance.GetClaim(Of Claim)(State.selectedClaimId)

            'use below if comment has to be updated using a ClaimBO 
            Dim commentBO As Comment = oclaim.AddNewComment()

            commentBO.Comments = CType(Grid.Items(Grid.EditItemIndex).Cells(GRID_COL_COMMENT_DETAILS_IDX).FindControl("moCommentDetail"), TextBox).Text
            commentBO.CommentTypeId = GetSelectedItem(CType(Grid.Items(Grid.EditItemIndex).Cells(GRID_COL_COMMENT_DETAILS_IDX).FindControl("moCommentType"), DropDownList))

            If (commentBO.IsDirty) Then

                oclaim.Save()
                'Me.State.IsGridAddNew = False
                State.IsAfterSave = True
                State.AddingNewRow = False
                AddInfoMsg(MSG_RECORD_SAVED_OK)
                State.searchDV = Nothing
                ReturnFromEditing()
            Else
                AddInfoMsg(MSG_RECORD_NOT_SAVED)
                ReturnFromEditing()
            End If

        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try

    End Sub

#End Region


#Region "Helper Functions"

    Private Sub ReturnFromEditing()

        Grid.EditItemIndex = NO_ITEM_SELECTED_INDEX

        If Grid.PageCount = 0 Then
            'if returning to the "1st time in" screen
            ControlMgr.SetVisibleControl(Me, Grid, False)
        Else
            ControlMgr.SetVisibleControl(Me, Grid, True)
        End If

        SetGridControls(Grid, True)
        State.IsEditMode = False
        PopulateGrid()
        State.PageIndex = Grid.CurrentPageIndex
        SetButtonsState()

    End Sub

    Private Sub SetButtonsState()

        If (State.IsEditMode) Then
            ControlMgr.SetVisibleControl(Me, SaveButton_WRITE, True)
            ControlMgr.SetVisibleControl(Me, UndoButton, True)
            'ControlMgr.SetVisibleControl(Me, btnAdd_WRITE, False)
            ControlMgr.SetEnableControl(Me, btnSearch, False)
            ControlMgr.SetEnableControl(Me, btnClearSearch, False)
            MenuEnabled = False
            If (cboPageSize.Visible) Then
                ControlMgr.SetEnableControl(Me, cboPageSize, False)
            End If
        Else
            ControlMgr.SetVisibleControl(Me, SaveButton_WRITE, False)
            ControlMgr.SetVisibleControl(Me, UndoButton, False)
            'ControlMgr.SetVisibleControl(Me, btnAdd_WRITE, True)
            ControlMgr.SetEnableControl(Me, btnSearch, True)
            ControlMgr.SetEnableControl(Me, btnClearSearch, True)
            MenuEnabled = True
            If (cboPageSize.Visible) Then
                ControlMgr.SetEnableControl(Me, cboPageSize, True)
            End If
        End If

    End Sub

#End Region

    Private Sub UndoButton_Click(sender As Object, e As System.EventArgs) Handles UndoButton.Click
        Try
            Grid.SelectedIndex = NO_ITEM_SELECTED_INDEX
            State.Canceling = True
            If (State.AddingNewRow) Then
                State.AddingNewRow = False
                State.searchDV = Nothing
            End If
            ReturnFromEditing()
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

#Region "Navigation Handling"
    Public Sub Process(callingPage As Page, navCtrl As INavigationController) Implements IStateController.Process
        Try
            If Not IsPostBack AndAlso navCtrl.CurrentFlow.Name = FLOW_NAME_PENDING_CLAIM AndAlso
navCtrl.PrevNavState IsNot Nothing Then
                IsReturningFromChild = True
                If navCtrl.IsFlowEnded Then
                    State.searchDV = Nothing 'This will force a reload
                    'restart the flow
                    Dim savedState As MyState = CType(navCtrl.State, MyState)
                    StartNavControlPendingClaim()
                    NavController.State = savedState
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
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub
#End Region

#Region "State Controller"
    Public Const FLOW_NAME_PENDING_CLAIM As String = "AUTHORIZE_PENDING_CLAIM_FROM_SEARCH_BY_COMMENT_TYPE"
    'Public Const FLOW_NAME_CLAIM_DETAIL As String = "CLAIM_DETAIL_FROM_SEARCH_BY_COMMENT_TYPE"
    Sub StartNavControlPendingClaim()
        Dim nav As New ElitaPlusNavigation
        NavController = New NavControllerBase(nav.Flow(FLOW_NAME_PENDING_CLAIM))
        If Navigator.PageState IsNot Nothing Then
            NavController.State = Navigator.PageState
        Else
            NavController.State = New MyState
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
        Return NavController IsNot Nothing AndAlso NavController.CurrentFlow.Name = FLOW_NAME_PENDING_CLAIM
    End Function
    'Function IsClaimDetailFlowStarted() As Boolean
    '    Return Not Me.NavController Is Nothing AndAlso Me.NavController.CurrentFlow.Name = Me.FLOW_NAME_CLAIM_DETAIL
    'End Function
#End Region

End Class

