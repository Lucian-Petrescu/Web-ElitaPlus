Imports System.Threading
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms

Partial Class PayClaimListForm
    Inherits ElitaPlusSearchPage

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    'Protected WithEvents ErrorCtrl As ErrorController
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
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
    Public Const GRID_COL_EDIT_IDX As Integer = 0
    Public Const GRID_COL_CLAIM_NUMBER_IDX As Integer = 1
    Public Const GRID_COL_STATUS_IDX As Integer = 2
    Public Const GRID_COL_DEALER_IDX As Integer = 3
    Public Const GRID_COL_CUSTOMER_NAME_IDX As Integer = 4
    Public Const GRID_COL_SERVICE_CENTER_IDX As Integer = 5
    Public Const GRID_COL_AUTHORIZATION_NUMBER_IDX As Integer = 6
    Public Const GRID_COL_AUTHORIZED_AMOUNT_IDX As Integer = 7
    Public Const GRID_COL_CLAIM_ID_IDX As Integer = 8

    Public Const GRID_CTRL_NAME_CLAIM_ID As String = "claim_id"
    Public Const GRID_TOTAL_COLUMNS As Integer = 8
    Public Const MAX_LIMIT As Integer = 1000

    Public Const PAGETITLE As String = "PAY INVOICE"
    Public Const PAGETAB As String = "CLAIM"

#End Region

#Region "Page State"
    Private IsReturningFromChild As Boolean = False

    Class MyState
        Public PageIndex As Integer = 0
        Public SortExpression As String = Claim.ClaimSearchDV.COL_CLAIM_NUMBER
        Public selectedClaimId As Guid = Guid.Empty
        Public claimNumber As String
        Public customerName As String
        Public serviceCenterName As String = String.Empty
        '   Public selectedServiceCenterId As Guid = Guid.Empty
        Public selectedDealerId As Guid = Guid.Empty
        Public selectedSortById As Guid = Guid.Empty
        Public selectedPageSize As Integer = DEFAULT_PAGE_SIZE
        Public authorizationNumber As String
        Public authorizedAmount As String
        Public IsGridVisible As Boolean = False
        Public searchDV As Claim.ClaimSearchDV = Nothing
        Public SearchClicked As Boolean
        Public authorizedAmountCulture As String
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
    'Protected Shadows ReadOnly Property State() As MyState
    '    Get

    '        If ((Me.NavController Is Nothing) OrElse (Me.NavController.CurrentFlow.Name <> "PAY_CLAIM_INVOICE")) Then
    '            Me.StartNavControl()
    '        End If
    '        If Me.NavController.State Is Nothing Then
    '            Me.NavController.State = New MyState
    '        Else
    '            If Me.NavController.IsFlowEnded Then
    '                'restart flow
    '                Dim s As MyState = CType(Me.NavController.State, MyState)
    '                Me.StartNavControl()
    '                Me.NavController.State = s
    '            End If
    '        End If
    '        Return CType(Me.NavController.State, MyState)

    '    End Get
    'End Property
    Private Sub Page_PageReturn(ReturnFromUrl As String, ReturnPar As Object) Handles MyBase.PageReturn
        MenuEnabled = True
        IsReturningFromChild = True

        Try
            If CalledUrl = PayClaimForm.URL Then
                Dim retObj As PayClaimForm.ReturnType = CType(ReturnPar, PayClaimForm.ReturnType)
                Select Case retObj.LastOperation
                    Case ElitaPlusPage.DetailPageCommand.Back
                        If retObj IsNot Nothing Then
                            If Not retObj.EditingBo.IsNew Then
                                State.selectedClaimId = retObj.EditingBo.Invoiceable.Claim_Id
                            End If
                            State.IsGridVisible = True
                        End If
                    Case ElitaPlusPage.DetailPageCommand.Delete
                        AddInfoMsg(Message.DELETE_RECORD_CONFIRMATION)
                End Select

                If retObj IsNot Nothing AndAlso retObj.BoChanged Then
                    State.searchDV = Nothing
                End If
            ElseIf CalledUrl = ClaimForm.URL Then
                Dim retObj As ClaimForm.ReturnType = CType(ReturnedValues, ClaimForm.ReturnType)
                Select Case retObj.LastOperation
                    Case ElitaPlusPage.DetailPageCommand.Back
                        If retObj IsNot Nothing Then
                            If Not retObj.EditingBo.IsNew Then
                                State.selectedClaimId = retObj.EditingBo.Id
                            End If
                            State.IsGridVisible = True
                            State.searchDV = Nothing
                        End If
                    Case ElitaPlusPage.DetailPageCommand.Delete
                        AddInfoMsg(Message.DELETE_RECORD_CONFIRMATION)
                End Select
                If retObj IsNot Nothing AndAlso retObj.BoChanged Then
                    State.searchDV = Nothing
                End If
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

#End Region

#Region "Page_Events"
    Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Page.RegisterHiddenField("__EVENTTARGET", btnSearch.ClientID)
        MasterPage.MessageController.Clear_Hide()
        Try
            If Not IsPostBack Then
                SetFormTitle(PAGETITLE)
                SetFormTab(PAGETAB)
                TranslateGridHeader(moGrid)
                TranslateGridControls(moGrid)
                SortDirection = Claim.ClaimSearchDV.COL_NAME_CLAIM_NUMBER
                SetDefaultButton(TextBoxSearchAuthorizationNumber, btnSearch)
                SetDefaultButton(TextBoxSearchAuthorizedAmount, btnSearch)
                SetDefaultButton(TextBoxSearchClaimNumber, btnSearch)
                SetDefaultButton(TextBoxSearchCustomerName, btnSearch)
                SetDefaultButton(moServiceCenterText, btnSearch)
                ControlMgr.SetVisibleControl(Me, trPageSize, False)
                ' PopulateServiceCenterDropDown()
                PopulateDealerDropDown()
                PopulateSortByDropDown()
                PopulateSearchFieldsFromState()
                If State.IsGridVisible Then
                    If Not (State.selectedPageSize = DEFAULT_PAGE_SIZE) Then
                        cboPageSize.SelectedValue = CType(State.selectedPageSize, String)
                        moGrid.PageSize = State.selectedPageSize
                    End If
                    PopulateGrid()
                End If
                SetGridItemStyleColor(moGrid)
                SetFocus(TextBoxSearchClaimNumber)

            End If
            If HiddenSaveChangesPromptResponse.Value IsNot Nothing AndAlso HiddenSaveChangesPromptResponse.Value = MSG_VALUE_YES Then
                callPage(ClaimForm.URL, State.selectedClaimId)
            End If
            HiddenSaveChangesPromptResponse.Value = ""
            DisplayNewProgressBarOnClick(btnSearch, "Loading_Claims")
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
        ShowMissingTranslations(MasterPage.MessageController)
    End Sub
#End Region

#Region "Controlling Logic"

    Sub PopulateSortByDropDown()
        Try

            cboSortBy.Populate(CommonConfigManager.Current.ListManager.GetList("CSEDR", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions())

            Dim lst1 As ListItem = cboSortBy.Items.FindByText(LookupListNew.GetDescriptionFromCode(LookupListNew.LK_CLAIM_SEARCH_FIELDS, Codes.SORT_BY_CERT_NUMBER, ElitaPlusIdentity.Current.ActiveUser.LanguageId))
            If lst1 IsNot Nothing Then
                cboSortBy.Items.Remove(lst1)
            End If

            Dim defaultSelectedCodeId As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_CLAIM_SEARCH_FIELDS, Codes.DEFAULT_SORT_FOR_CLAIMS)

            If (State.selectedSortById.Equals(Guid.Empty)) Then
                SetSelectedItem(cboSortBy, defaultSelectedCodeId)
                State.selectedSortById = defaultSelectedCodeId
            Else
                SetSelectedItem(cboSortBy, State.selectedSortById)
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
    Sub PopulateDealerDropDown()
        Try
            Dim oDealerList As Assurant.Elita.CommonConfiguration.DataElements.ListItem() = GetDealerListByCompanyForUser()
            cboSearchDealer.Populate(oDealerList, New PopulateOptions() With
                                                   {
                                                   .AddBlankItem = True
                                                   })

            If State.selectedDealerId <> Guid.Empty Then
                SetSelectedItem(cboSearchDealer, State.selectedDealerId)
            End If
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

    'Sub PopulateServiceCenterDropDown()
    '    Me.BindListControlToDataView(Me.cboSearchServiceCenter, LookupListNew.GetServiceCenterLookupList(ElitaPlusIdentity.Current.ActiveUser.CompanyId), , , True)
    '    Me.SetSelectedItem(Me.cboSearchServiceCenter, Me.State.selectedServiceCenterId)
    'End Sub

    Public Sub PopulateGrid()

        Try
            If Not PopulateStateFromSearchFields() Then Exit Sub

            'DEF-34232, instead of blocking the all claims selected dealer, change the query to return
            'only the single auth claim claim_auth_type_id = 'S'
            'If Me.State.selectedDealerId <> Guid.Empty Then
            '    Dim YesId As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, Codes.YESNO_Y)
            '    'Dim oDealer As Dealer = New Dealer(Me.State.selectedDealerId)
            '    'If oDealer.UseClaimAuthorizationId.Equals(YesId) Then
            '    '    Me.ValidSearchResultCount(0)
            '    '    Me.State.SearchClicked = False
            '    '    Exit Sub
            '    'End If
            'End If

            If (State.searchDV Is Nothing) Then
                Dim claimList As Claim.ClaimSearchDV
                Dim sortBy As String
                If (Not (State.selectedSortById.Equals(Guid.Empty))) Then
                    sortBy = LookupListNew.GetCodeFromId(LookupListNew.LK_CLAIM_SEARCH_FIELDS, State.selectedSortById)

                    claimList = Claim.getActiveClaimsList(State.claimNumber,
                                                      State.customerName,
                                                      State.serviceCenterName,
                                                      State.authorizationNumber,
                                                      State.authorizedAmount,
                                                      State.selectedDealerId,
                                                      sortBy)
                Else
                    claimList = Claim.getActiveClaimsList(State.claimNumber,
                                                      State.customerName,
                                                      State.serviceCenterName,
                                                      State.authorizationNumber,
                                                      State.authorizedAmount,
                                                      State.selectedDealerId)
                End If

                'DEF-34232, the query changed to return only single auth claim, no need to clear data
                'For Each row As DataRow In claimList.Table.Rows
                '    Dim dealerId As Guid = New Guid(CType(row(Claim.ClaimSearchDV.COL_DEALER_ID), Byte()))
                '    Dim oDealer As Dealer = New Dealer(dealerId)
                '    Dim YesId As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, Codes.YESNO_Y)

                '    If oDealer.UseClaimAuthorizationId.Equals(YesId) Then
                '        row.Delete()
                '    End If
                'Next


                State.searchDV = claimList

                If (State.SearchClicked) Then
                    ValidSearchResultCount(State.searchDV.Count, True)
                    State.SearchClicked = False
                End If
            End If

            moGrid.AutoGenerateColumns = False

            SetPageAndSelectedIndexFromGuid(State.searchDV, State.selectedClaimId, moGrid, State.PageIndex)
            State.PageIndex = moGrid.PageIndex
            'Me.moGrid.DataSource = Me.State.searchDV
            'Me.moGrid.AllowSorting = False
            '  Me.moGrid.DataBind()

            ControlMgr.SetVisibleControl(Me, moGrid, State.IsGridVisible)

            ControlMgr.SetVisibleControl(Me, trPageSize, moGrid.Visible)

            Session("recCount") = State.searchDV.Count

            If State.searchDV.Count > 0 Then

                If moGrid.Visible Then
                    lblRecordCount.Text = State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_CLAIMS_FOUND)
                    State.bnoRow = False
                End If
                moGrid.DataSource = State.searchDV
                moGrid.AllowSorting = False
                moGrid.DataBind()
            Else
                If moGrid.Visible Then
                    State.bnoRow = True
                    CreateHeaderForEmptyGrid(moGrid, SortDirection)
                    lblRecordCount.Text = State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_CLAIMS_FOUND)
                End If
            End If

            If Not moGrid.BottomPagerRow.Visible Then moGrid.BottomPagerRow.Visible = True

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Public Sub PopulateSearchFieldsFromState()
        Dim tempBO As Claim = ClaimFacade.Instance.CreateClaim(Of Claim)()

        Try
            TextBoxSearchCustomerName.Text = State.customerName
            TextBoxSearchClaimNumber.Text = State.claimNumber
            '   Me.SetSelectedItem(Me.cboSearchServiceCenter, Me.State.selectedServiceCenterId)
            moServiceCenterText.Text = State.serviceCenterName
            TextBoxSearchAuthorizationNumber.Text = State.authorizationNumber
            'Me.TextBoxSearchAuthorizedAmount.Text = Me.State.authorizedAmount
            TextBoxSearchAuthorizationNumber.Text = State.authorizationNumber

            If State.authorizedAmount Is Nothing Then
                TextBoxSearchAuthorizedAmount.Text = State.authorizedAmount
            Else
                TextBoxSearchAuthorizedAmount.Text = State.authorizedAmountCulture 'Me.State.authorizedAmount
            End If
            SetSelectedItem(cboSearchDealer, State.selectedDealerId)
            SetSelectedItem(cboSortBy, State.selectedSortById)
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try

    End Sub

    Public Function PopulateStateFromSearchFields() As Boolean
        Dim dblAmount As Double
        Try
            State.claimNumber = TextBoxSearchClaimNumber.Text
            State.customerName = TextBoxSearchCustomerName.Text
            '  Me.State.selectedServiceCenterId = Me.GetSelectedItem(Me.cboSearchServiceCenter)
            State.serviceCenterName = moServiceCenterText.Text
            State.authorizationNumber = TextBoxSearchAuthorizationNumber.Text
            'Me.State.authorizedAmount = Me.TextBoxSearchAuthorizedAmount.Text
            State.selectedDealerId = GetSelectedItem(cboSearchDealer)
            State.selectedSortById = GetSelectedItem(cboSortBy)

            If Not TextBoxSearchAuthorizedAmount.Text.Trim = String.Empty Then
                If Not Double.TryParse(TextBoxSearchAuthorizedAmount.Text, dblAmount) Then
                    ErrControllerMaster.AddErrorAndShow(Assurant.ElitaPlus.Common.ErrorCodes.INVALID_AUTHORIZED_AMOUNT_ERR)
                    Return False
                Else
                    State.authorizedAmountCulture = TextBoxSearchAuthorizedAmount.Text
                    State.authorizedAmount = dblAmount.ToString(System.Threading.Thread.CurrentThread.CurrentCulture.InvariantCulture)
                End If
            Else
                State.authorizedAmount = TextBoxSearchAuthorizedAmount.Text
            End If

            Return True
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try

    End Function

    'This method will change the Page Index and the Selected Index
    Public Function FindDVSelectedRowIndex(dv As Claim.ClaimSearchDV) As Integer
        Try
            If State.selectedClaimId.Equals(Guid.Empty) Then
                Return -1
            Else
                'Jump to the Right Page
                Dim i As Integer
                For i = 0 To dv.Count - 1
                    If New Guid(CType(dv(i)(Claim.ClaimSearchDV.COL_CLAIM_ID), Byte())).Equals(State.selectedClaimId) Then
                        Return i
                    End If
                Next
            End If
            Return -1
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Function


#End Region

#Region "Datagrid Related "
    Public Property SortDirection() As String
        Get
            Return ViewState("SortDirection").ToString
        End Get
        Set(value As String)
            ViewState("SortDirection") = value
        End Set
    End Property

    'The Binding LOgic is here
    Private Sub moGrid_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles moGrid.RowDataBound
        Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
        Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)

        Try
            If dvRow IsNot Nothing AndAlso Not State.bnoRow Then
                If itemType = ListItemType.Item OrElse itemType = ListItemType.AlternatingItem OrElse itemType = ListItemType.SelectedItem Then
                    'e.Row.Cells(Me.GRID_COL_CLAIM_ID_IDX).Text = dvRow(Claim.ClaimSearchDV.COL_CLAIM_ID).ToString
                    'e.Row.Cells(Me.GRID_COL_CLAIM_ID_IDX).Text = GetGuidStringFromByteArray(CType(dvRow(Claim.ClaimSearchDV.COL_NAME_CLAIM_ID), Byte()))

                    'e.Row.Cells(Me.GRID_COL_CLAIM_NUMBER_IDX).Text = dvRow(Claim.ClaimSearchDV.COL_CLAIM_NUMBER).ToString
                    'e.Row.Cells(Me.GRID_COL_STATUS_IDX).Text = dvRow(Claim.ClaimSearchDV.COL_STATUS_CODE).ToString
                    'e.Row.Cells(Me.GRID_COL_SERVICE_CENTER_IDX).Text = dvRow(Claim.ClaimSearchDV.COL_SERVICE_CENTER_NAME).ToString
                    'e.Row.Cells(Me.GRID_COL_CUSTOMER_NAME_IDX).Text = dvRow(Claim.ClaimSearchDV.COL_CUSTOMER_NAME).ToString
                    'e.Row.Cells(Me.GRID_COL_AUTHORIZATION_NUMBER_IDX).Text = dvRow(Claim.ClaimSearchDV.COL_AUTHORIZATION_NUMBER).ToString
                    'e.Row.Cells(Me.GRID_COL_AUTHORIZED_AMOUNT_IDX).Text = dvRow(Claim.ClaimSearchDV.COL_AUTHORIZED_AMOUNT).ToString

                    PopulateControlFromBOProperty(e.Row.Cells(GRID_COL_CLAIM_ID_IDX), dvRow(Claim.ClaimSearchDV.COL_CLAIM_ID))
                    PopulateControlFromBOProperty(e.Row.Cells(GRID_COL_CLAIM_NUMBER_IDX), dvRow(Claim.ClaimSearchDV.COL_CLAIM_NUMBER))
                    PopulateControlFromBOProperty(e.Row.Cells(GRID_COL_STATUS_IDX), dvRow(Claim.ClaimSearchDV.COL_STATUS_CODE))
                    PopulateControlFromBOProperty(e.Row.Cells(GRID_COL_DEALER_IDX), dvRow(Claim.ClaimSearchDV.COL_DEALER_NAME))
                    PopulateControlFromBOProperty(e.Row.Cells(GRID_COL_SERVICE_CENTER_IDX), dvRow(Claim.ClaimSearchDV.COL_SERVICE_CENTER_NAME))
                    PopulateControlFromBOProperty(e.Row.Cells(GRID_COL_CUSTOMER_NAME_IDX), dvRow(Claim.ClaimSearchDV.COL_CUSTOMER_NAME))
                    PopulateControlFromBOProperty(e.Row.Cells(GRID_COL_AUTHORIZATION_NUMBER_IDX), dvRow(Claim.ClaimSearchDV.COL_AUTHORIZATION_NUMBER))
                    PopulateControlFromBOProperty(e.Row.Cells(GRID_COL_AUTHORIZED_AMOUNT_IDX), dvRow(Claim.ClaimSearchDV.COL_AUTHORIZED_AMOUNT))
                End If
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub moGrid_PageSizeChanged(source As Object, e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
        Try
            moGrid.PageIndex = NewCurrentPageIndex(moGrid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
            State.selectedPageSize = CType(cboPageSize.SelectedValue, Integer)
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub


    Public Sub RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles moGrid.RowCommand
        Try
            Dim index As Integer
            If e.CommandName = "SelectAction" Then
                index = CInt(e.CommandArgument)
                'Me.State.selectedClaimId = New Guid(CType(Me.moGrid.Rows(index).Cells(Me.GRID_COL_CLAIM_ID_IDX).FindControl(Me.GRID_CTRL_NAME_CLAIM_ID), Label).Text)
                State.selectedClaimId = New Guid(moGrid.Rows(index).Cells(GRID_COL_CLAIM_ID_IDX).Text)

                Dim claimBO As Claim = ClaimFacade.Instance.GetClaim(Of Claim)(State.selectedClaimId)

                'Get the latest Claim Status for this Claim and Check if it is 'Pending Review for Payment'
                Dim maxClaimStatus As ClaimStatus = ClaimStatus.GetLatestClaimStatus(State.selectedClaimId)
                If maxClaimStatus IsNot Nothing AndAlso maxClaimStatus.StatusCode = Codes.CLAIM_EXTENDED_STATUS__PENDING_REVIEW_FOR_PAYMENT Then
                    DisplayMessage(Message.MSG_PROMPT_FOR_CLAIM_PENDING_REVIEW, "", ElitaPlusPage.MSG_BTN_YES_NO, ElitaPlusPage.MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                Else
                    If claimBO.AssurantPays.Value > 0 Then
                        callPage(PayClaimForm.URL, New PayClaimForm.Parameters(State.selectedClaimId, False))
                    Else
                        If claimBO.MethodOfRepairCode = Codes.METHOD_OF_REPAIR__RECOVERY Then
                            callPage(PayClaimForm.URL, New PayClaimForm.Parameters(State.selectedClaimId, False))
                        Else
                            DisplayMessage(Message.MSG_PROMPT_FOR_PAY_CLAIM_WITH_ZERO_AMOUNT, "", ElitaPlusPage.MSG_BTN_YES_NO, ElitaPlusPage.MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                            'Me.callPage(ClaimForm.URL, claimBO.Id)
                        End If
                    End If
                    'Me.NavController.Navigate(Me, FlowEvents.EVENT_CLAIM_SELECTED, Me.State.selectedClaimId)
                End If
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try

    End Sub

    Public Sub RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles moGrid.RowCreated
        BaseItemCreated(sender, e)
    End Sub

    Private Sub moGrid_PageIndexChanged(sender As Object, e As System.EventArgs) Handles moGrid.PageIndexChanged
        Try

            State.PageIndex = moGrid.PageIndex
            PopulateGrid()

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    'Private Sub moGrid_PageIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles moGrid.PageIndexChanged
    Private Sub moGrid_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles moGrid.PageIndexChanging
        Try
            moGrid.PageIndex = e.NewPageIndex
            State.PageIndex = moGrid.PageIndex
            'Fix for defect-1542 start: to enable paging after back button click
            State.selectedClaimId = Guid.Empty
            'Fix for defect-1542 End
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
#End Region

#Region "Button Clicks"

    Private Sub btnSearch_Click(sender As System.Object, e As System.EventArgs) Handles btnSearch.Click
        Try
            'Me.PopulateSearchFieldsFromState()
            State.SearchClicked = True
            State.PageIndex = 0
            State.selectedClaimId = Guid.Empty
            State.searchDV = Nothing
            State.IsGridVisible = True
            State.selectedSortById = New Guid(cboSortBy.SelectedValue)
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    'Private Sub btnAdd_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Me.callPage(ClaimForm.URL)
    'End Sub

    Private Sub btnClearSearch_Click(sender As System.Object, e As System.EventArgs) Handles btnClearSearch.Click
        Try
            ClearSearch()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub ClearSearch()
        Try
            TextBoxSearchClaimNumber.Text = String.Empty
            TextBoxSearchCustomerName.Text = String.Empty
            '   Me.cboSearchServiceCenter.SelectedIndex = 0
            moServiceCenterText.Text = String.Empty
            cboSearchDealer.SelectedIndex = 0
            TextBoxSearchAuthorizationNumber.Text = String.Empty
            TextBoxSearchAuthorizedAmount.Text = String.Empty
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

#End Region


End Class



