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

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
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
    Private Sub Page_PageReturn(ByVal ReturnFromUrl As String, ByVal ReturnPar As Object) Handles MyBase.PageReturn
        Me.MenuEnabled = True
        Me.IsReturningFromChild = True

        Try
            If Me.CalledUrl = PayClaimForm.URL Then
                Dim retObj As PayClaimForm.ReturnType = CType(ReturnPar, PayClaimForm.ReturnType)
                Select Case retObj.LastOperation
                    Case ElitaPlusPage.DetailPageCommand.Back
                        If Not retObj Is Nothing Then
                            If Not retObj.EditingBo.IsNew Then
                                Me.State.selectedClaimId = retObj.EditingBo.Invoiceable.Claim_Id
                            End If
                            Me.State.IsGridVisible = True
                        End If
                    Case ElitaPlusPage.DetailPageCommand.Delete
                        Me.AddInfoMsg(Message.DELETE_RECORD_CONFIRMATION)
                End Select

                If Not retObj Is Nothing AndAlso retObj.BoChanged Then
                    Me.State.searchDV = Nothing
                End If
            ElseIf Me.CalledUrl = ClaimForm.URL Then
                Dim retObj As ClaimForm.ReturnType = CType(Me.ReturnedValues, ClaimForm.ReturnType)
                Select Case retObj.LastOperation
                    Case ElitaPlusPage.DetailPageCommand.Back
                        If Not retObj Is Nothing Then
                            If Not retObj.EditingBo.IsNew Then
                                Me.State.selectedClaimId = retObj.EditingBo.Id
                            End If
                            Me.State.IsGridVisible = True
                            Me.State.searchDV = Nothing
                        End If
                    Case ElitaPlusPage.DetailPageCommand.Delete
                        Me.AddInfoMsg(Message.DELETE_RECORD_CONFIRMATION)
                End Select
                If Not retObj Is Nothing AndAlso retObj.BoChanged Then
                    Me.State.searchDV = Nothing
                End If
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

#End Region

#Region "Page_Events"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Page.RegisterHiddenField("__EVENTTARGET", Me.btnSearch.ClientID)
        Me.MasterPage.MessageController.Clear_Hide()
        Try
            If Not Me.IsPostBack Then
                Me.SetFormTitle(PAGETITLE)
                Me.SetFormTab(PAGETAB)
                Me.TranslateGridHeader(Me.moGrid)
                Me.TranslateGridControls(Me.moGrid)
                Me.SortDirection = Claim.ClaimSearchDV.COL_NAME_CLAIM_NUMBER
                Me.SetDefaultButton(Me.TextBoxSearchAuthorizationNumber, Me.btnSearch)
                Me.SetDefaultButton(Me.TextBoxSearchAuthorizedAmount, Me.btnSearch)
                Me.SetDefaultButton(Me.TextBoxSearchClaimNumber, Me.btnSearch)
                Me.SetDefaultButton(Me.TextBoxSearchCustomerName, Me.btnSearch)
                Me.SetDefaultButton(moServiceCenterText, Me.btnSearch)
                ControlMgr.SetVisibleControl(Me, trPageSize, False)
                ' PopulateServiceCenterDropDown()
                PopulateDealerDropDown()
                PopulateSortByDropDown()
                PopulateSearchFieldsFromState()
                If Me.State.IsGridVisible Then
                    If Not (Me.State.selectedPageSize = DEFAULT_PAGE_SIZE) Then
                        cboPageSize.SelectedValue = CType(Me.State.selectedPageSize, String)
                        moGrid.PageSize = Me.State.selectedPageSize
                    End If
                    Me.PopulateGrid()
                End If
                Me.SetGridItemStyleColor(Me.moGrid)
                SetFocus(Me.TextBoxSearchClaimNumber)

            End If
            If Not Me.HiddenSaveChangesPromptResponse.Value Is Nothing AndAlso Me.HiddenSaveChangesPromptResponse.Value = Me.MSG_VALUE_YES Then
                Me.callPage(ClaimForm.URL, Me.State.selectedClaimId)
            End If
            Me.HiddenSaveChangesPromptResponse.Value = ""
            Me.DisplayNewProgressBarOnClick(Me.btnSearch, "Loading_Claims")
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
        Me.ShowMissingTranslations(Me.MasterPage.MessageController)
    End Sub
#End Region

#Region "Controlling Logic"

    Sub PopulateSortByDropDown()
        Try

            cboSortBy.Populate(CommonConfigManager.Current.ListManager.GetList("CSEDR", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions())

            Dim lst1 As ListItem = Me.cboSortBy.Items.FindByText(LookupListNew.GetDescriptionFromCode(LookupListNew.LK_CLAIM_SEARCH_FIELDS, Codes.SORT_BY_CERT_NUMBER, ElitaPlusIdentity.Current.ActiveUser.LanguageId))
            If Not lst1 Is Nothing Then
                Me.cboSortBy.Items.Remove(lst1)
            End If

            Dim defaultSelectedCodeId As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_CLAIM_SEARCH_FIELDS, Codes.DEFAULT_SORT_FOR_CLAIMS)

            If (Me.State.selectedSortById.Equals(Guid.Empty)) Then
                Me.SetSelectedItem(Me.cboSortBy, defaultSelectedCodeId)
                Me.State.selectedSortById = defaultSelectedCodeId
            Else
                Me.SetSelectedItem(Me.cboSortBy, Me.State.selectedSortById)
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub
    Sub PopulateDealerDropDown()
        Try
            Dim oDealerList As Assurant.Elita.CommonConfiguration.DataElements.ListItem() = GetDealerListByCompanyForUser()
            cboSearchDealer.Populate(oDealerList, New PopulateOptions() With
                                                   {
                                                   .AddBlankItem = True
                                                   })

            If Me.State.selectedDealerId <> Guid.Empty Then
                Me.SetSelectedItem(Me.cboSearchDealer, Me.State.selectedDealerId)
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, MasterPage.MessageController)
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

            If (Me.State.searchDV Is Nothing) Then
                Dim claimList As Claim.ClaimSearchDV
                Dim sortBy As String
                If (Not (Me.State.selectedSortById.Equals(Guid.Empty))) Then
                    sortBy = LookupListNew.GetCodeFromId(LookupListNew.LK_CLAIM_SEARCH_FIELDS, Me.State.selectedSortById)

                    claimList = Claim.getActiveClaimsList(Me.State.claimNumber,
                                                      Me.State.customerName,
                                                      Me.State.serviceCenterName,
                                                      Me.State.authorizationNumber,
                                                      Me.State.authorizedAmount,
                                                      Me.State.selectedDealerId,
                                                      sortBy)
                Else
                    claimList = Claim.getActiveClaimsList(Me.State.claimNumber,
                                                      Me.State.customerName,
                                                      Me.State.serviceCenterName,
                                                      Me.State.authorizationNumber,
                                                      Me.State.authorizedAmount,
                                                      Me.State.selectedDealerId)
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


                Me.State.searchDV = claimList

                If (Me.State.SearchClicked) Then
                    Me.ValidSearchResultCount(Me.State.searchDV.Count, True)
                    Me.State.SearchClicked = False
                End If
            End If

            Me.moGrid.AutoGenerateColumns = False

            SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.selectedClaimId, Me.moGrid, Me.State.PageIndex)
            Me.State.PageIndex = moGrid.PageIndex
            'Me.moGrid.DataSource = Me.State.searchDV
            'Me.moGrid.AllowSorting = False
            '  Me.moGrid.DataBind()

            ControlMgr.SetVisibleControl(Me, moGrid, Me.State.IsGridVisible)

            ControlMgr.SetVisibleControl(Me, trPageSize, Me.moGrid.Visible)

            Session("recCount") = Me.State.searchDV.Count

            If Me.State.searchDV.Count > 0 Then

                If Me.moGrid.Visible Then
                    Me.lblRecordCount.Text = Me.State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_CLAIMS_FOUND)
                    Me.State.bnoRow = False
                End If
                Me.moGrid.DataSource = Me.State.searchDV
                Me.moGrid.AllowSorting = False
                Me.moGrid.DataBind()
            Else
                If Me.moGrid.Visible Then
                    Me.State.bnoRow = True
                    CreateHeaderForEmptyGrid(moGrid, Me.SortDirection)
                    Me.lblRecordCount.Text = Me.State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_CLAIMS_FOUND)
                End If
            End If

            If Not moGrid.BottomPagerRow.Visible Then moGrid.BottomPagerRow.Visible = True

        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Public Sub PopulateSearchFieldsFromState()
        Dim tempBO As Claim = ClaimFacade.Instance.CreateClaim(Of Claim)()

        Try
            Me.TextBoxSearchCustomerName.Text = Me.State.customerName
            Me.TextBoxSearchClaimNumber.Text = Me.State.claimNumber
            '   Me.SetSelectedItem(Me.cboSearchServiceCenter, Me.State.selectedServiceCenterId)
            moServiceCenterText.Text = Me.State.serviceCenterName
            Me.TextBoxSearchAuthorizationNumber.Text = Me.State.authorizationNumber
            'Me.TextBoxSearchAuthorizedAmount.Text = Me.State.authorizedAmount
            Me.TextBoxSearchAuthorizationNumber.Text = Me.State.authorizationNumber

            If Me.State.authorizedAmount Is Nothing Then
                Me.TextBoxSearchAuthorizedAmount.Text = Me.State.authorizedAmount
            Else
                Me.TextBoxSearchAuthorizedAmount.Text = Me.State.authorizedAmountCulture 'Me.State.authorizedAmount
            End If
            Me.SetSelectedItem(Me.cboSearchDealer, Me.State.selectedDealerId)
            Me.SetSelectedItem(Me.cboSortBy, Me.State.selectedSortById)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try

    End Sub

    Public Function PopulateStateFromSearchFields() As Boolean
        Dim dblAmount As Double
        Try
            Me.State.claimNumber = Me.TextBoxSearchClaimNumber.Text
            Me.State.customerName = Me.TextBoxSearchCustomerName.Text
            '  Me.State.selectedServiceCenterId = Me.GetSelectedItem(Me.cboSearchServiceCenter)
            Me.State.serviceCenterName = moServiceCenterText.Text
            Me.State.authorizationNumber = Me.TextBoxSearchAuthorizationNumber.Text
            'Me.State.authorizedAmount = Me.TextBoxSearchAuthorizedAmount.Text
            Me.State.selectedDealerId = Me.GetSelectedItem(Me.cboSearchDealer)
            Me.State.selectedSortById = Me.GetSelectedItem(Me.cboSortBy)

            If Not Me.TextBoxSearchAuthorizedAmount.Text.Trim = String.Empty Then
                If Not Double.TryParse(Me.TextBoxSearchAuthorizedAmount.Text, dblAmount) Then
                    Me.ErrControllerMaster.AddErrorAndShow(Assurant.ElitaPlus.Common.ErrorCodes.INVALID_AUTHORIZED_AMOUNT_ERR)
                    Return False
                Else
                    Me.State.authorizedAmountCulture = Me.TextBoxSearchAuthorizedAmount.Text
                    Me.State.authorizedAmount = dblAmount.ToString(System.Threading.Thread.CurrentThread.CurrentCulture.InvariantCulture)
                End If
            Else
                Me.State.authorizedAmount = Me.TextBoxSearchAuthorizedAmount.Text
            End If

            Return True
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try

    End Function

    'This method will change the Page Index and the Selected Index
    Public Function FindDVSelectedRowIndex(ByVal dv As Claim.ClaimSearchDV) As Integer
        Try
            If Me.State.selectedClaimId.Equals(Guid.Empty) Then
                Return -1
            Else
                'Jump to the Right Page
                Dim i As Integer
                For i = 0 To dv.Count - 1
                    If New Guid(CType(dv(i)(Claim.ClaimSearchDV.COL_CLAIM_ID), Byte())).Equals(Me.State.selectedClaimId) Then
                        Return i
                    End If
                Next
            End If
            Return -1
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Function


#End Region

#Region "Datagrid Related "
    Public Property SortDirection() As String
        Get
            Return ViewState("SortDirection").ToString
        End Get
        Set(ByVal value As String)
            ViewState("SortDirection") = value
        End Set
    End Property

    'The Binding LOgic is here
    Private Sub moGrid_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles moGrid.RowDataBound
        Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
        Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)

        Try
            If Not dvRow Is Nothing And Not Me.State.bnoRow Then
                If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then
                    'e.Row.Cells(Me.GRID_COL_CLAIM_ID_IDX).Text = dvRow(Claim.ClaimSearchDV.COL_CLAIM_ID).ToString
                    'e.Row.Cells(Me.GRID_COL_CLAIM_ID_IDX).Text = GetGuidStringFromByteArray(CType(dvRow(Claim.ClaimSearchDV.COL_NAME_CLAIM_ID), Byte()))

                    'e.Row.Cells(Me.GRID_COL_CLAIM_NUMBER_IDX).Text = dvRow(Claim.ClaimSearchDV.COL_CLAIM_NUMBER).ToString
                    'e.Row.Cells(Me.GRID_COL_STATUS_IDX).Text = dvRow(Claim.ClaimSearchDV.COL_STATUS_CODE).ToString
                    'e.Row.Cells(Me.GRID_COL_SERVICE_CENTER_IDX).Text = dvRow(Claim.ClaimSearchDV.COL_SERVICE_CENTER_NAME).ToString
                    'e.Row.Cells(Me.GRID_COL_CUSTOMER_NAME_IDX).Text = dvRow(Claim.ClaimSearchDV.COL_CUSTOMER_NAME).ToString
                    'e.Row.Cells(Me.GRID_COL_AUTHORIZATION_NUMBER_IDX).Text = dvRow(Claim.ClaimSearchDV.COL_AUTHORIZATION_NUMBER).ToString
                    'e.Row.Cells(Me.GRID_COL_AUTHORIZED_AMOUNT_IDX).Text = dvRow(Claim.ClaimSearchDV.COL_AUTHORIZED_AMOUNT).ToString

                    Me.PopulateControlFromBOProperty(e.Row.Cells(Me.GRID_COL_CLAIM_ID_IDX), dvRow(Claim.ClaimSearchDV.COL_CLAIM_ID))
                    Me.PopulateControlFromBOProperty(e.Row.Cells(Me.GRID_COL_CLAIM_NUMBER_IDX), dvRow(Claim.ClaimSearchDV.COL_CLAIM_NUMBER))
                    Me.PopulateControlFromBOProperty(e.Row.Cells(Me.GRID_COL_STATUS_IDX), dvRow(Claim.ClaimSearchDV.COL_STATUS_CODE))
                    Me.PopulateControlFromBOProperty(e.Row.Cells(Me.GRID_COL_DEALER_IDX), dvRow(Claim.ClaimSearchDV.COL_DEALER_NAME))
                    Me.PopulateControlFromBOProperty(e.Row.Cells(Me.GRID_COL_SERVICE_CENTER_IDX), dvRow(Claim.ClaimSearchDV.COL_SERVICE_CENTER_NAME))
                    Me.PopulateControlFromBOProperty(e.Row.Cells(Me.GRID_COL_CUSTOMER_NAME_IDX), dvRow(Claim.ClaimSearchDV.COL_CUSTOMER_NAME))
                    Me.PopulateControlFromBOProperty(e.Row.Cells(Me.GRID_COL_AUTHORIZATION_NUMBER_IDX), dvRow(Claim.ClaimSearchDV.COL_AUTHORIZATION_NUMBER))
                    Me.PopulateControlFromBOProperty(e.Row.Cells(Me.GRID_COL_AUTHORIZED_AMOUNT_IDX), dvRow(Claim.ClaimSearchDV.COL_AUTHORIZED_AMOUNT))
                End If
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub moGrid_PageSizeChanged(ByVal source As Object, ByVal e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
        Try
            moGrid.PageIndex = NewCurrentPageIndex(moGrid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
            Me.State.selectedPageSize = CType(cboPageSize.SelectedValue, Integer)
            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub


    Public Sub RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles moGrid.RowCommand
        Try
            Dim index As Integer
            If e.CommandName = "SelectAction" Then
                index = CInt(e.CommandArgument)
                'Me.State.selectedClaimId = New Guid(CType(Me.moGrid.Rows(index).Cells(Me.GRID_COL_CLAIM_ID_IDX).FindControl(Me.GRID_CTRL_NAME_CLAIM_ID), Label).Text)
                Me.State.selectedClaimId = New Guid(Me.moGrid.Rows(index).Cells(Me.GRID_COL_CLAIM_ID_IDX).Text)

                Dim claimBO As Claim = ClaimFacade.Instance.GetClaim(Of Claim)(Me.State.selectedClaimId)

                'Get the latest Claim Status for this Claim and Check if it is 'Pending Review for Payment'
                Dim maxClaimStatus As ClaimStatus = ClaimStatus.GetLatestClaimStatus(Me.State.selectedClaimId)
                If Not maxClaimStatus Is Nothing AndAlso maxClaimStatus.StatusCode = Codes.CLAIM_EXTENDED_STATUS__PENDING_REVIEW_FOR_PAYMENT Then
                    Me.DisplayMessage(Message.MSG_PROMPT_FOR_CLAIM_PENDING_REVIEW, "", ElitaPlusPage.MSG_BTN_YES_NO, ElitaPlusPage.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                Else
                    If claimBO.AssurantPays.Value > 0 Then
                        Me.callPage(PayClaimForm.URL, New PayClaimForm.Parameters(Me.State.selectedClaimId, False))
                    Else
                        If claimBO.MethodOfRepairCode = Codes.METHOD_OF_REPAIR__RECOVERY Then
                            Me.callPage(PayClaimForm.URL, New PayClaimForm.Parameters(Me.State.selectedClaimId, False))
                        Else
                            Me.DisplayMessage(Message.MSG_PROMPT_FOR_PAY_CLAIM_WITH_ZERO_AMOUNT, "", ElitaPlusPage.MSG_BTN_YES_NO, ElitaPlusPage.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                            'Me.callPage(ClaimForm.URL, claimBO.Id)
                        End If
                    End If
                    'Me.NavController.Navigate(Me, FlowEvents.EVENT_CLAIM_SELECTED, Me.State.selectedClaimId)
                End If
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try

    End Sub

    Public Sub RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles moGrid.RowCreated
        BaseItemCreated(sender, e)
    End Sub

    Private Sub moGrid_PageIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles moGrid.PageIndexChanged
        Try

            Me.State.PageIndex = moGrid.PageIndex
            Me.PopulateGrid()

        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    'Private Sub moGrid_PageIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles moGrid.PageIndexChanged
    Private Sub moGrid_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles moGrid.PageIndexChanging
        Try
            moGrid.PageIndex = e.NewPageIndex
            State.PageIndex = moGrid.PageIndex
            'Fix for defect-1542 start: to enable paging after back button click
            Me.State.selectedClaimId = Guid.Empty
            'Fix for defect-1542 End
            PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub
#End Region

#Region "Button Clicks"

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            'Me.PopulateSearchFieldsFromState()
            Me.State.SearchClicked = True
            Me.State.PageIndex = 0
            Me.State.selectedClaimId = Guid.Empty
            Me.State.searchDV = Nothing
            Me.State.IsGridVisible = True
            Me.State.selectedSortById = New Guid(Me.cboSortBy.SelectedValue)
            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    'Private Sub btnAdd_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Me.callPage(ClaimForm.URL)
    'End Sub

    Private Sub btnClearSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearSearch.Click
        Try
            ClearSearch()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub ClearSearch()
        Try
            Me.TextBoxSearchClaimNumber.Text = String.Empty
            Me.TextBoxSearchCustomerName.Text = String.Empty
            '   Me.cboSearchServiceCenter.SelectedIndex = 0
            moServiceCenterText.Text = String.Empty
            Me.cboSearchDealer.SelectedIndex = 0
            Me.TextBoxSearchAuthorizationNumber.Text = String.Empty
            Me.TextBoxSearchAuthorizedAmount.Text = String.Empty
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

#End Region


End Class



