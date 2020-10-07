Imports System.Threading
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms
Imports Assurant.Elita.CommonConfiguration.DataElements

Partial Class ClaimListForm
    Inherits ElitaPlusSearchPage
    Implements IStateController


#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
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
    Public Const GRID_COL_CLAIM_NUMBER_CTRL As String = "btnEditClaim"
    Public Const GRID_COL_CLAIM_NUMBER_IDX As Integer = 0
    Public Const GRID_COL_STATUS_IDX As Integer = 1
    Public Const GRID_COL_CUSTOMER_NAME_IDX As Integer = 2
    Public Const GRID_COL_SERVICE_CENTER_IDX As Integer = 3
    Public Const GRID_COL_DEALER_IDX As Integer = 4
    Public Const GRID_COL_CERTIFICATE_IDX As Integer = 5
    Public Const GRID_COL_DATE_ADDED_IDX As Integer = 6
    Public Const GRID_COL_PRODUCT_CODE_IDX As Integer = 7
    'Public Const GRID_COL_AUTHORIZATION_NUMBER_IDX As Integer = 8
    Public Const GRID_COL_TRACKING_NUMBER_IDX As Integer = 8
    Public Const GRID_COL_AUTHORIZED_AMOUNT_IDX As Integer = 9
    Public Const GRID_COL_AUTHORIZATION_NUMBER_IDX As Integer = 10
    Public Const GRID_COL_AUTHORIZATION_STATUS_IDX As Integer = 11
    Public Const GRID_COL_CLAIM_ID_IDX As Integer = 12
    Public Const GRID_COL_CLAIM_AUTH_TYPE_ID_IDX As Integer = 13
    Public Const GRID_COL_CLAIM_STATUS_CODE_IDX As Integer = 14



    Public Const GRID_TOTAL_COLUMNS As Integer = 14

    Public Const SESSION_KEY_BACKUP_STATE As String = "SESSION_KEY_BACKUP_STATE_CLAIM_LIST_FORM"
#End Region

#Region "Page State"
    Private IsReturningFromChild As Boolean = False

    Class MyState
        Public PageIndex As Integer = 0
        Public SortExpression As String = String.Empty
        Public selectedClaimId As Guid = Guid.Empty
        Public claimNumber As String
        Public certificate As String
        Public selectedDealerId As Guid = Guid.Empty
        Public selectedDealer As String = String.Empty
        Public selectedClaimStatusId As Guid = Guid.Empty
        Public hasPendingAuthId As Guid = Guid.Empty

        Public customerName As String
        Public trackingNumber As String
        '  Public selectedServiceCenterId As Guid = Guid.Empty
        '  Public selectedServiceCenterIds As ArrayList
        Public serviceCenterName As String = String.Empty
        Public selectedSortById As Guid = Guid.Empty
        Public PageSize As Integer = DEFAULT_NEW_UI_PAGE_SIZE
        Public serviceRefNumber As String
        Public authorizedAmount As String
        Public authorizationNumber As String
        Public selectedClaimAuthStatusId As Guid = Guid.Empty

        Public IsGridVisible As Boolean = False
        Public searchDV As Claim.ClaimSearchDV = Nothing
        Public SearchClicked As Boolean
        Public authorizedAmountCulture As String
        Public dealerBO As Dealer


        'Property selectedServiceCenterId() As Guid
        '    Get
        '        Return CType(selectedServiceCenterIds(0), Guid)        '    End Get
        '    Set(ByVal Value As Guid)
        '        selectedServiceCenterIds.Clear()
        '        selectedServiceCenterIds.Add(Value)
        '    End Set
        'End Property

        Sub New()
            'selectedServiceCenterIds = New ArrayList
            'selectedServiceCenterIds.Add(Guid.Empty)
        End Sub

    End Class

    Public Sub New()
        MyBase.New(New MyState)
    End Sub

    Protected Shadows ReadOnly Property State() As MyState
        Get
            Dim retState As MyState
            'Return CType(MyBase.State, MyState)
            If NavController Is Nothing Then
                'Restart flow
                StartNavControl()
                NavController.State = CType(Session(SESSION_KEY_BACKUP_STATE), MyState)
            ElseIf NavController.State Is Nothing Then
                NavController.State = New MyState
            ElseIf ([GetType].BaseType.FullName <>
                    NavController.State.GetType.ReflectedType.FullName) Then
                'Restart flow
                StartNavControl()
                NavController.State = CType(Session(SESSION_KEY_BACKUP_STATE), MyState)
            Else
                If NavController.IsFlowEnded Then
                    'restart flow
                    Dim s As MyState = CType(NavController.State, MyState)
                    StartNavControl()
                    NavController.State = s
                End If
            End If
            If NavController.State Is Nothing Then
                NavController.State = New MyState
            End If
            retState = CType(NavController.State, MyState)
            Session(SESSION_KEY_BACKUP_STATE) = retState
            Return retState
        End Get
    End Property
#End Region


#Region "Page_Events"

    Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Page.RegisterHiddenField("__EVENTTARGET", btnSearch.ClientID)
        Dim langId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
        MasterPage.MessageController.Clear_Hide()
        Try
            If Not IsPostBack Then
                'Trace(Me, "Cert = " & Me.State.certificate & "@ Claim = " & Me.State.claimNumber)
                MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("Claims")
                UpdateBreadCrum()

                SetDefaultButton(TextBoxSearchClaimNumber, btnSearch)
                SetDefaultButton(TextBoxSearchAuthorizedAmount, btnSearch)
                SetDefaultButton(TextBoxSearchSvcRefNumber, btnSearch)
                SetDefaultButton(TextBoxSearchCustomerName, btnSearch)

                SetDefaultButton(TextBoxSearchCertificate, btnSearch)
                SetDefaultButton(TextBoxSearchAuthorizationNumber, btnSearch)

                SetDefaultButton(moServiceCenterText, btnSearch)
                TranslateGridHeader(Grid)
                ControlMgr.SetVisibleControl(Me, trPageSize, False)
                If ElitaPlusIdentity.Current.ActiveUser.IsDealer Then
                    State.selectedDealerId = ElitaPlusIdentity.Current.ActiveUser.ScDealerId
                    ControlMgr.SetEnableControl(Me, cboSearchDealer, False)
                End If
                PopulateSortByDropDown()
                PopulateHaspendingAuthDropDown()
                PopulateDealerDropDown()
                PopulateClaimStatusDropDown()
                PopulateClaimAuthorizationStatusDropDown()
                cboPageSize.SelectedValue = CType(State.PageSize, String)
                Grid.PageSize = State.PageSize
                If IsReturningFromChild Then
                    ' It is returning from detail
                    PopulateSearchFieldsFromState()
                    PopulateGrid()
                Else
                    ClearSearch()
                    ClearState()
                End If
                SetGridItemStyleColor(Grid)
                SetFocus(TextBoxSearchClaimNumber)
            End If
            DisplayNewProgressBarOnClick(btnSearch, "Loading_Claims")
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
        ShowMissingTranslations(MasterPage.MessageController)
    End Sub

    Private Sub Page_PageReturn(ReturnFromUrl As String, ReturnPar As Object) Handles MyBase.PageReturn
        Try
            MenuEnabled = True
            IsReturningFromChild = True
            State.searchDV = Nothing

            Dim retObj As ClaimForm.ReturnType = CType(ReturnedValues, ClaimForm.ReturnType)
            If retObj IsNot Nothing Then
                Select Case retObj.LastOperation
                    Case ElitaPlusPage.DetailPageCommand.Back
                        If retObj IsNot Nothing Then
                            If Not retObj.EditingBo.IsNew Then
                                State.selectedClaimId = retObj.EditingBo.Id
                            End If
                            State.IsGridVisible = True
                        End If
                End Select
            End If

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
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
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Sub PopulateDealerDropDown()
        Try
            Dim dealerList As ListItem()
            If Authentication.CurrentUser.IsDealerGroup Then
                dealerList = CaseBase.GetDealerListByCompanyForExternalUser()
            Else
                dealerList = GetDealerListByCompanyForUser()
            End If
            cboSearchDealer.Populate(dealerList, New PopulateOptions() With
                                                 {
                                                   .AddBlankItem = True,
                                                   .SortFunc = AddressOf .GetDescription
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

    Sub PopulateClaimStatusDropDown()
        Try
            Dim stateListLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("CLSTAT", Thread.CurrentPrincipal.GetLanguageCode())
            cboSearchClaimStatus.Populate(stateListLkl, New PopulateOptions() With
            {
              .AddBlankItem = True
            })

            SetSelectedItem(cboSearchClaimStatus, State.selectedClaimStatusId)
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Sub PopulateHaspendingAuthDropDown()
        Try
            Dim haspendingAuthListLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("YESNO", Thread.CurrentPrincipal.GetLanguageCode())
            cboSearchHasPendingAuth.Populate(haspendingAuthListLkl, New PopulateOptions() With
            {
              .AddBlankItem = True
            })
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Sub PopulateSortByDropDown()
        Try
            Dim oSortListLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("CSEDR", Thread.CurrentPrincipal.GetLanguageCode())
            cboSortBy.Populate(oSortListLkl, New PopulateOptions() With
            {
              .SortFunc = AddressOf .GetDescription
            })

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

    Sub PopulateClaimAuthorizationStatusDropDown()
        Try

            Dim authorizationStatusList As ListItem() = CommonConfigManager.Current.ListManager.GetList("CLM_AUTH_STAT", Thread.CurrentPrincipal.GetLanguageCode())
            cboAuthorizationStatus.Populate(authorizationStatusList, New PopulateOptions() With
            {
              .AddBlankItem = True
            })
            SetSelectedItem(cboAuthorizationStatus, State.selectedClaimAuthStatusId)
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub


    Public Sub PutInvisibleSvcColumns(oGrid As GridView)
        Try
            If ElitaPlusIdentity.Current.ActiveUser.IsServiceCenter Then
                oGrid.Columns(GRID_COL_SERVICE_CENTER_IDX).Visible = False
                oGrid.Columns(GRID_COL_AUTHORIZATION_NUMBER_IDX).Visible = False
                oGrid.Columns(GRID_COL_AUTHORIZED_AMOUNT_IDX).Visible = False
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Public Sub PopulateGrid()

        Try
            Dim sortBy As String

            If (State.searchDV Is Nothing) Then
                If (Not (State.selectedSortById.Equals(Guid.Empty))) Then
                    sortBy = LookupListNew.GetCodeFromId(LookupListNew.LK_CLAIM_SEARCH_FIELDS, State.selectedSortById)
                End If

                Dim ClaimStatus As String = String.Empty
                Dim ClaimAuthStatusId As Guid = Nothing

                ClaimStatus = LookupListNew.GetCodeFromId(LookupListNew.LK_CLAIM_STATUS, State.selectedClaimStatusId)
                If (State.selectedClaimAuthStatusId.Equals(Guid.Empty)) Then
                    ClaimAuthStatusId = Nothing
                Else
                    ClaimAuthStatusId = State.selectedClaimAuthStatusId
                End If

                State.searchDV = Claim.getListFromArray(State.claimNumber,
                                                                      State.customerName,
                                                                      State.serviceCenterName,
                                                                      State.serviceRefNumber,
                                                                      State.authorizedAmount,
                                                                      State.hasPendingAuthId,
                                                                      Nothing,
                                                                      State.certificate,
                                                                      State.selectedDealerId,
                                                                      ClaimStatus,
                                                                      sortBy,
                                                                      State.trackingNumber,
                                                                      State.authorizationNumber,
                                                                      ClaimAuthStatusId)
                If (State.SearchClicked) Then
                    ValidSearchResultCountNew(State.searchDV.Count, True)
                    State.SearchClicked = False
                End If
            End If

            Grid.PageSize = State.PageSize

            SetPageAndSelectedIndexFromGuid(State.searchDV, State.selectedClaimId, Grid, State.PageIndex)
            Grid.DataSource = State.searchDV
            State.PageIndex = Grid.PageIndex
            PutInvisibleSvcColumns(Grid)
            If (Not State.SortExpression.Equals(String.Empty)) Then
                State.searchDV.Sort = State.SortExpression
            Else
                State.SortExpression = sortBy
            End If

            HighLightSortColumn(Grid, State.SortExpression, IsNewUI)

            If (State.searchDV.Count > 0 AndAlso Not State.searchDV(0)("dealer_id").Equals(Guid.Empty)) Then
                State.dealerBO = New Dealer(GuidControl.ByteArrayToGuid(State.searchDV(0)("dealer_id")))
            End If


            Grid.DataBind()

            ControlMgr.SetVisibleControl(Me, Grid, State.IsGridVisible)

            ControlMgr.SetVisibleControl(Me, trPageSize, Grid.Visible)

            Session("recCount") = State.searchDV.Count

            'hide and show authorization number and authorization status if dealer is Multi Auth enabled
            If State.dealerBO IsNot Nothing AndAlso State.dealerBO.UseClaimAuthorizationId.Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_LANG_INDEPENDENT_YES_NO, "N")) Then
                Grid.Columns(10).Visible = False
                Grid.Columns(11).Visible = False
            End If

            If Grid.Visible Then
                lblRecordCount.Text = State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_CLAIMS_FOUND)
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Public Sub PopulateSearchFieldsFromState()
        Try

            TextBoxSearchCustomerName.Text = State.customerName
            TextBoxSearchClaimNumber.Text = State.claimNumber
            TextBoxSearchCertificate.Text = State.certificate
            moServiceCenterText.Text = State.serviceCenterName

            TextBoxSearchSvcRefNumber.Text = State.serviceRefNumber

            If State.authorizedAmount Is Nothing Then
                TextBoxSearchAuthorizedAmount.Text = State.authorizedAmount
            Else
                TextBoxSearchAuthorizedAmount.Text = State.authorizedAmountCulture 'Me.State.authorizedAmount
            End If
            TextBoxSearchTrackingNumber.Text = State.trackingNumber
            TextBoxSearchAuthorizationNumber.Text = State.authorizedAmount

            SetSelectedItem(cboSearchDealer, State.selectedDealerId)
            SetSelectedItem(cboSearchClaimStatus, State.selectedClaimStatusId)
            SetSelectedItem(cboSortBy, State.selectedSortById)
            SetSelectedItem(cboSearchHasPendingAuth, State.hasPendingAuthId)
            SetSelectedItem(cboAuthorizationStatus, State.selectedClaimAuthStatusId)
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Public Sub ClearState()
        Dim dblAmount As Double

        Try
            State.claimNumber = String.Empty
            State.certificate = String.Empty
            State.customerName = String.Empty

            State.selectedDealerId = Guid.Empty
            State.selectedDealer = String.Empty

            State.selectedClaimStatusId = Guid.Empty
            State.serviceCenterName = String.Empty
            State.serviceRefNumber = String.Empty
            State.selectedSortById = Guid.Empty
            State.trackingNumber = String.Empty

            State.authorizedAmount = String.Empty
            State.authorizedAmountCulture = String.Empty

            State.authorizationNumber = String.Empty
            State.selectedClaimAuthStatusId = Guid.Empty


        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Public Function PopulateStateFromSearchFields() As Boolean
        Dim dblAmount As Double

        Try
            State.claimNumber = TextBoxSearchClaimNumber.Text
            State.certificate = TextBoxSearchCertificate.Text
            State.customerName = TextBoxSearchCustomerName.Text

            State.selectedDealerId = GetSelectedItem(cboSearchDealer)
            State.selectedDealer = LookupListNew.GetCodeFromId(LookupListNew.LK_DEALERS, State.selectedDealerId)

            State.selectedClaimStatusId = GetSelectedItem(cboSearchClaimStatus)
            State.hasPendingAuthId = GetSelectedItem(cboSearchHasPendingAuth)
            State.serviceCenterName = moServiceCenterText.Text
            State.serviceRefNumber = TextBoxSearchSvcRefNumber.Text
            State.selectedSortById = GetSelectedItem(cboSortBy)
            State.trackingNumber = TextBoxSearchTrackingNumber.Text
            State.authorizationNumber = TextBoxSearchAuthorizationNumber.Text
            State.selectedClaimAuthStatusId = GetSelectedItem(cboAuthorizationStatus)

            If Not TextBoxSearchAuthorizedAmount.Text.Trim = String.Empty Then
                If Not Double.TryParse(TextBoxSearchAuthorizedAmount.Text, dblAmount) Then
                    MasterPage.MessageController.AddErrorAndShow(Assurant.ElitaPlus.Common.ErrorCodes.INVALID_AUTHORIZED_AMOUNT_ERR)
                    Return False
                Else
                    State.authorizedAmountCulture = TextBoxSearchAuthorizedAmount.Text
                    State.authorizedAmount = dblAmount.ToString(System.Threading.Thread.CurrentThread.CurrentCulture.InvariantCulture)
                End If
            Else
                State.authorizedAmount = TextBoxSearchAuthorizedAmount.Text
                State.authorizedAmountCulture = TextBoxSearchAuthorizedAmount.Text
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

    Public Sub ClearSearch()
        Try
            TextBoxSearchClaimNumber.Text = String.Empty
            TextBoxSearchCustomerName.Text = String.Empty
            '  Me.cboSearchServiceCenter.SelectedIndex = 0
            moServiceCenterText.Text = String.Empty
            TextBoxSearchSvcRefNumber.Text = String.Empty
            TextBoxSearchAuthorizedAmount.Text = String.Empty
            TextBoxSearchCertificate.Text = String.Empty
            TextBoxSearchTrackingNumber.Text = String.Empty
            TextBoxSearchAuthorizationNumber.Text = String.Empty

            If Not ElitaPlusIdentity.Current.ActiveUser.IsDealer Then
                cboSearchDealer.SelectedIndex = 0
            End If
            cboSearchClaimStatus.SelectedIndex = 0
            cboSearchHasPendingAuth.SelectedIndex = 0
            cboAuthorizationStatus.SelectedIndex = 0
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

#End Region


#Region " Datagrid Related "

    Private Sub Grid_SortCommand(source As Object, e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles Grid.Sorting
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

    Private Sub Grid_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Grid.PageIndexChanging
        Try
            Grid.PageIndex = e.NewPageIndex
            State.PageIndex = Grid.PageIndex
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    'The Binding LOgic is here
    Private Sub Grid_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowDataBound
        'Dim itemType As ListItemType = CType(e.Row.ItemType, ListItemType)
        Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
        Dim btnEditClaimItem As LinkButton

        Try
            If e.Row.RowType = DataControlRowType.DataRow OrElse (e.Row.RowType = DataControlRowType.Separator) Then
                If (e.Row.Cells(GRID_COL_CLAIM_NUMBER_IDX).FindControl(GRID_COL_CLAIM_NUMBER_CTRL) IsNot Nothing) Then
                    btnEditClaimItem = CType(e.Row.Cells(GRID_COL_CLAIM_NUMBER_IDX).FindControl(GRID_COL_CLAIM_NUMBER_CTRL), LinkButton)
                    btnEditClaimItem.CommandArgument = e.Row.RowIndex.ToString
                    btnEditClaimItem.CommandName = SELECT_COMMAND_NAME
                    btnEditClaimItem.Text = dvRow(Claim.ClaimSearchDV.COL_CLAIM_NUMBER).ToString
                End If

                PopulateControlFromBOProperty(e.Row.Cells(GRID_COL_STATUS_IDX), dvRow(Claim.ClaimSearchDV.COL_STATUS_CODE))
                e.Row.Cells(GRID_COL_STATUS_IDX).Text = LookupListNew.GetDescriptionFromCode("CLSTAT", dvRow(Claim.ClaimSearchDV.COL_STATUS_CODE).ToString, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
                If (dvRow(Claim.ClaimSearchDV.COL_STATUS_CODE).ToString = Codes.CLAIM_STATUS__ACTIVE) Then
                    e.Row.Cells(GRID_COL_STATUS_IDX).CssClass = "StatActive"
                Else
                    e.Row.Cells(GRID_COL_STATUS_IDX).CssClass = "StatInactive"
                End If
                PopulateControlFromBOProperty(e.Row.Cells(GRID_COL_CUSTOMER_NAME_IDX), dvRow(Claim.ClaimSearchDV.COL_CUSTOMER_NAME))
                PopulateControlFromBOProperty(e.Row.Cells(GRID_COL_SERVICE_CENTER_IDX), dvRow(Claim.ClaimSearchDV.COL_SERVICE_CENTER_NAME))
                PopulateControlFromBOProperty(e.Row.Cells(GRID_COL_DEALER_IDX), dvRow(Claim.ClaimSearchDV.COL_DEALER_CODE))
                PopulateControlFromBOProperty(e.Row.Cells(GRID_COL_CERTIFICATE_IDX), dvRow(Claim.ClaimSearchDV.COL_CERTIFICATE_NUMBER))
                PopulateControlFromBOProperty(e.Row.Cells(GRID_COL_DATE_ADDED_IDX), GetLongDateFormattedString(CType(dvRow(Claim.ClaimSearchDV.COL_DATE_ADDED), Date)))
                PopulateControlFromBOProperty(e.Row.Cells(GRID_COL_PRODUCT_CODE_IDX), dvRow(Claim.ClaimSearchDV.COL_PRODUCT_CODE))
                'Me.PopulateControlFromBOProperty(e.Row.Cells(Me.GRID_COL_AUTHORIZATION_NUMBER_IDX), dvRow(Claim.ClaimSearchDV.COL_AUTHORIZATION_NUMBER))
                PopulateControlFromBOProperty(e.Row.Cells(GRID_COL_TRACKING_NUMBER_IDX), dvRow(Claim.ClaimSearchDV.COL_TRACKING_NUMBER))
                PopulateControlFromBOProperty(e.Row.Cells(GRID_COL_AUTHORIZED_AMOUNT_IDX), dvRow(Claim.ClaimSearchDV.COL_AUTHORIZED_AMOUNT))
                PopulateControlFromBOProperty(e.Row.Cells(GRID_COL_CLAIM_ID_IDX), dvRow(Claim.ClaimSearchDV.COL_CLAIM_ID))
                PopulateControlFromBOProperty(e.Row.Cells(GRID_COL_CLAIM_AUTH_TYPE_ID_IDX), dvRow(Claim.ClaimSearchDV.COL_CLAIM_AUTH_TYPE_ID))
                PopulateControlFromBOProperty(e.Row.Cells(GRID_COL_CLAIM_STATUS_CODE_IDX), dvRow(Claim.ClaimSearchDV.COL_STATUS_CODE))
                PopulateControlFromBOProperty(e.Row.Cells(GRID_COL_AUTHORIZATION_NUMBER_IDX), dvRow(Claim.ClaimSearchDV.COL_AUTHORIZATION_NUMBER))
                PopulateControlFromBOProperty(e.Row.Cells(GRID_COL_AUTHORIZATION_STATUS_IDX), dvRow(Claim.ClaimSearchDV.COL_AUTHORIZATION_STATUS))

            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub cboPageSize_SelectedIndexChanged(source As Object, e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
        Try
            State.PageSize = CType(cboPageSize.SelectedValue, Integer)
            State.PageIndex = NewCurrentPageIndex(Grid, State.searchDV.Count, State.PageSize)
            Grid.PageIndex = NewCurrentPageIndex(Grid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Public Sub Grid_RowCommand(source As System.Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles Grid.RowCommand
        Dim rowIndex As Integer = 0
        Dim claimid As String = String.Empty
        Try
            If (Not e.CommandArgument.ToString().Equals(String.Empty)) AndAlso (e.CommandName = SELECT_COMMAND_NAME) Then
                rowIndex = CInt(e.CommandArgument)

                If State Is Nothing Then
                    Trace(Me, "Restoring State")
                    RestoreState(New MyState)
                End If

                claimid = Grid.Rows(rowIndex).Cells(GRID_COL_CLAIM_ID_IDX).Text
                State.selectedClaimId = New Guid(claimid)

                If Grid.Rows(rowIndex).Cells(GRID_COL_CLAIM_STATUS_CODE_IDX).Text = "P" Then
                    Dim claim_auth_type_code As String
                    Dim ClaimAutthTypeid As Guid = New Guid(Grid.Rows(rowIndex).Cells(GRID_COL_CLAIM_AUTH_TYPE_ID_IDX).Text) ' New Guid(BusinessObjectBase.FindRow(Me.State.selectedClaimId, Claim.ClaimSearchDV.COL_CLAIM_ID, Me.State.searchDV.Table)(Claim.ClaimSearchDV.COL_NAME_CLAIM_AUTH_TYPE_ID).ToString())
                    claim_auth_type_code = LookupListNew.GetCodeFromId(LookupListNew.LK_CLAIM_AUTHORIZATION_TYPE, ClaimAutthTypeid)
                    Dim selectedClaimId As Guid = State.selectedClaimId
                    If (claim_auth_type_code = "M") Then
                        NavController = Nothing
                        callPage(ClaimWizardForm.URL, New ClaimWizardForm.Parameters(ClaimWizardForm.ClaimWizardSteps.Step3, Nothing, selectedClaimId, Nothing))
                    Else
                        Dim claimBo As Claim = ClaimFacade.Instance.GetClaim(Of Claim)(State.selectedClaimId)
                        NavController.FlowSession(FlowSessionKeys.SESSION_CLAIM) = claimBo
                        NavController.Navigate(Me, FlowEvents.EVENT_CLAIM_SELECTED)

                    End If
                Else
                    NavController = Nothing
                    callPage(ClaimForm.URL, State.selectedClaimId)
                End If


            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try

    End Sub

    Public Sub RowCreated(sender As System.Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowCreated
        Try
            BaseItemCreated(sender, e)
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Grid_PageIndexChanged(source As Object, e As System.EventArgs) Handles Grid.PageIndexChanged
        Try
            State.PageIndex = Grid.PageIndex
            State.selectedClaimId = Guid.Empty
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
#End Region

#Region " Button Clicks "

    Private Sub btnSearch_Click(sender As System.Object, e As System.EventArgs) Handles btnSearch.Click
        Try
            PopulateStateFromSearchFields()
            State.SearchClicked = True
            State.PageIndex = 0
            State.selectedClaimId = Guid.Empty
            State.IsGridVisible = True
            State.searchDV = Nothing
            State.selectedSortById = New Guid(cboSortBy.SelectedValue)
            State.SortExpression = String.Empty
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnClearSearch_Click(sender As System.Object, e As System.EventArgs) Handles btnClearSearch.Click
        Try
            ClearSearch()
            ClearState()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

#End Region
#Region "Navigation Handling"
    Public Sub Process(callingPage As Page, navCtrl As INavigationController) Implements IStateController.Process
        Try
            If Not IsPostBack AndAlso navCtrl.CurrentFlow.Name = FLOW_NAME AndAlso
navCtrl.PrevNavState IsNot Nothing Then
                IsReturningFromChild = True
                If navCtrl.IsFlowEnded Then
                    State.searchDV = Nothing 'This will force a reload
                    'restart the flow
                    Dim savedState As MyState = CType(navCtrl.State, MyState)
                    StartNavControl()
                    NavController.State = savedState
                End If
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub
#End Region

#Region "State Controller"
    Public Const FLOW_NAME As String = "AUTHORIZE_PENDING_CLAIM"
    Sub StartNavControl()
        Dim nav As New ElitaPlusNavigation
        NavController = New NavControllerBase(nav.Flow(FLOW_NAME))
        NavController.State = New MyState
    End Sub

    Function IsFlowStarted() As Boolean
        Return NavController IsNot Nothing AndAlso NavController.CurrentFlow.Name = FLOW_NAME
    End Function
#End Region

    Private Sub UpdateBreadCrum()
        If (State IsNot Nothing) Then
            If (State IsNot Nothing) Then
                MasterPage.BreadCrum = MasterPage.PageTab & ElitaBase.Sperator &
                    TranslationBase.TranslateLabelOrMessage("CLAIM SEARCH")
                MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("CLAIM SEARCH")
            End If
        End If
    End Sub

End Class



