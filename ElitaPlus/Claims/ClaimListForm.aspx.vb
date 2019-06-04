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

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
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
            If Me.NavController Is Nothing Then
                'Restart flow
                Me.StartNavControl()
                Me.NavController.State = CType(Session(Me.SESSION_KEY_BACKUP_STATE), MyState)
            ElseIf Me.NavController.State Is Nothing Then
                Me.NavController.State = New MyState
            ElseIf (Me.GetType.BaseType.FullName <>
                    Me.NavController.State.GetType.ReflectedType.FullName) Then
                'Restart flow
                Me.StartNavControl()
                Me.NavController.State = CType(Session(Me.SESSION_KEY_BACKUP_STATE), MyState)
            Else
                If Me.NavController.IsFlowEnded Then
                    'restart flow
                    Dim s As MyState = CType(Me.NavController.State, MyState)
                    Me.StartNavControl()
                    Me.NavController.State = s
                End If
            End If
            If Me.NavController.State Is Nothing Then
                Me.NavController.State = New MyState
            End If
            retState = CType(Me.NavController.State, MyState)
            Session(Me.SESSION_KEY_BACKUP_STATE) = retState
            Return retState
        End Get
    End Property
#End Region


#Region "Page_Events"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Page.RegisterHiddenField("__EVENTTARGET", Me.btnSearch.ClientID)
        Dim langId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
        Me.MasterPage.MessageController.Clear_Hide()
        Try
            If Not Me.IsPostBack Then
                'Trace(Me, "Cert = " & Me.State.certificate & "@ Claim = " & Me.State.claimNumber)
                Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("Claims")
                UpdateBreadCrum()

                Me.SetDefaultButton(Me.TextBoxSearchClaimNumber, btnSearch)
                Me.SetDefaultButton(Me.TextBoxSearchAuthorizedAmount, btnSearch)
                Me.SetDefaultButton(Me.TextBoxSearchSvcRefNumber, btnSearch)
                Me.SetDefaultButton(Me.TextBoxSearchCustomerName, btnSearch)

                Me.SetDefaultButton(Me.TextBoxSearchCertificate, btnSearch)
                Me.SetDefaultButton(Me.TextBoxSearchAuthorizationNumber, btnSearch)

                Me.SetDefaultButton(moServiceCenterText, btnSearch)
                TranslateGridHeader(Me.Grid)
                ControlMgr.SetVisibleControl(Me, trPageSize, False)
                If ElitaPlusIdentity.Current.ActiveUser.IsDealer Then
                    Me.State.selectedDealerId = ElitaPlusIdentity.Current.ActiveUser.ScDealerId
                    ControlMgr.SetEnableControl(Me, cboSearchDealer, False)
                End If
                PopulateSortByDropDown()
                PopulateHaspendingAuthDropDown()
                PopulateDealerDropDown()
                PopulateClaimStatusDropDown()
                PopulateClaimAuthorizationStatusDropDown()
                cboPageSize.SelectedValue = CType(Me.State.PageSize, String)
                Grid.PageSize = Me.State.PageSize
                If Me.IsReturningFromChild Then
                    ' It is returning from detail
                    PopulateSearchFieldsFromState()
                    Me.PopulateGrid()
                Else
                    Me.ClearSearch()
                    Me.ClearState()
                End If
                Me.SetGridItemStyleColor(Me.Grid)
                SetFocus(Me.TextBoxSearchClaimNumber)
            End If
            Me.DisplayNewProgressBarOnClick(Me.btnSearch, "Loading_Claims")
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
        Me.ShowMissingTranslations(Me.MasterPage.MessageController)
    End Sub

    Private Sub Page_PageReturn(ByVal ReturnFromUrl As String, ByVal ReturnPar As Object) Handles MyBase.PageReturn
        Try
            Me.MenuEnabled = True
            Me.IsReturningFromChild = True
            Me.State.searchDV = Nothing

            Dim retObj As ClaimForm.ReturnType = CType(Me.ReturnedValues, ClaimForm.ReturnType)
            If Not retObj Is Nothing Then
                Select Case retObj.LastOperation
                    Case ElitaPlusPage.DetailPageCommand.Back
                        If Not retObj Is Nothing Then
                            If Not retObj.EditingBo.IsNew Then
                                Me.State.selectedClaimId = retObj.EditingBo.Id
                            End If
                            Me.State.IsGridVisible = True
                        End If
                End Select
            End If

        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
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
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Sub PopulateDealerDropDown()
        Try
            Dim dealerList As ListItem() = GetDealerListByCompanyForUser()
            cboSearchDealer.Populate(dealerList, New PopulateOptions() With
                                                 {
                                                   .AddBlankItem = True,
                                                   .SortFunc = AddressOf .GetDescription
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

    Sub PopulateClaimStatusDropDown()
        Try
            Dim stateListLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("CLSTAT", Thread.CurrentPrincipal.GetLanguageCode())
            cboSearchClaimStatus.Populate(stateListLkl, New PopulateOptions() With
            {
              .AddBlankItem = True
            })

            Me.SetSelectedItem(Me.cboSearchClaimStatus, Me.State.selectedClaimStatusId)
        Catch ex As Exception
            Me.HandleErrors(ex, MasterPage.MessageController)
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
            Me.HandleErrors(ex, MasterPage.MessageController)
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

    Sub PopulateClaimAuthorizationStatusDropDown()
        Try

            Dim authorizationStatusList As ListItem() = CommonConfigManager.Current.ListManager.GetList("CLM_AUTH_STAT", Thread.CurrentPrincipal.GetLanguageCode())
            cboAuthorizationStatus.Populate(authorizationStatusList, New PopulateOptions() With
            {
              .AddBlankItem = True
            })
            Me.SetSelectedItem(Me.cboAuthorizationStatus, Me.State.selectedClaimAuthStatusId)
        Catch ex As Exception
            Me.HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub


    Public Sub PutInvisibleSvcColumns(ByVal oGrid As GridView)
        Try
            If ElitaPlusIdentity.Current.ActiveUser.IsServiceCenter Then
                oGrid.Columns(GRID_COL_SERVICE_CENTER_IDX).Visible = False
                oGrid.Columns(GRID_COL_AUTHORIZATION_NUMBER_IDX).Visible = False
                oGrid.Columns(GRID_COL_AUTHORIZED_AMOUNT_IDX).Visible = False
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Public Sub PopulateGrid()

        Try
            Dim sortBy As String

            If (Me.State.searchDV Is Nothing) Then
                If (Not (Me.State.selectedSortById.Equals(Guid.Empty))) Then
                    sortBy = LookupListNew.GetCodeFromId(LookupListNew.LK_CLAIM_SEARCH_FIELDS, Me.State.selectedSortById)
                End If

                Dim ClaimStatus As String = String.Empty
                Dim ClaimAuthStatusId As Guid = Nothing

                ClaimStatus = LookupListNew.GetCodeFromId(LookupListNew.LK_CLAIM_STATUS, Me.State.selectedClaimStatusId)
                If (Me.State.selectedClaimAuthStatusId.Equals(Guid.Empty)) Then
                    ClaimAuthStatusId = Nothing
                Else
                    ClaimAuthStatusId = Me.State.selectedClaimAuthStatusId
                End If

                Me.State.searchDV = Claim.getListFromArray(Me.State.claimNumber,
                                                                      Me.State.customerName,
                                                                      Me.State.serviceCenterName,
                                                                      Me.State.serviceRefNumber,
                                                                      Me.State.authorizedAmount,
                                                                      Me.State.hasPendingAuthId,
                                                                      Nothing,
                                                                      Me.State.certificate,
                                                                      Me.State.selectedDealerId,
                                                                      ClaimStatus,
                                                                      sortBy,
                                                                      Me.State.trackingNumber,
                                                                      Me.State.authorizationNumber,
                                                                      ClaimAuthStatusId)
                If (Me.State.SearchClicked) Then
                    Me.ValidSearchResultCountNew(Me.State.searchDV.Count, True)
                    Me.State.SearchClicked = False
                End If
            End If

            Grid.PageSize = State.PageSize

            SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.selectedClaimId, Me.Grid, Me.State.PageIndex)
            Me.Grid.DataSource = Me.State.searchDV
            Me.State.PageIndex = Me.Grid.PageIndex
            PutInvisibleSvcColumns(Me.Grid)
            If (Not Me.State.SortExpression.Equals(String.Empty)) Then
                Me.State.searchDV.Sort = Me.State.SortExpression
            Else
                Me.State.SortExpression = sortBy
            End If

            HighLightSortColumn(Me.Grid, Me.State.SortExpression, Me.IsNewUI)

            If (State.searchDV.Count > 0 AndAlso Not State.searchDV(0)("dealer_id").Equals(Guid.Empty)) Then
                Me.State.dealerBO = New Dealer(GuidControl.ByteArrayToGuid(Me.State.searchDV(0)("dealer_id")))
            End If


            Me.Grid.DataBind()

            ControlMgr.SetVisibleControl(Me, Grid, Me.State.IsGridVisible)

            ControlMgr.SetVisibleControl(Me, trPageSize, Me.Grid.Visible)

            Session("recCount") = Me.State.searchDV.Count

            'hide and show authorization number and authorization status if dealer is Multi Auth enabled
            If Not Me.State.dealerBO Is Nothing AndAlso Me.State.dealerBO.UseClaimAuthorizationId.Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_LANG_INDEPENDENT_YES_NO, "N")) Then
                Grid.Columns(10).Visible = False
                Grid.Columns(11).Visible = False
            End If

            If Me.Grid.Visible Then
                Me.lblRecordCount.Text = Me.State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_CLAIMS_FOUND)
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Public Sub PopulateSearchFieldsFromState()
        Try

            Me.TextBoxSearchCustomerName.Text = Me.State.customerName
            Me.TextBoxSearchClaimNumber.Text = Me.State.claimNumber
            Me.TextBoxSearchCertificate.Text = Me.State.certificate
            moServiceCenterText.Text = Me.State.serviceCenterName

            Me.TextBoxSearchSvcRefNumber.Text = Me.State.serviceRefNumber

            If Me.State.authorizedAmount Is Nothing Then
                Me.TextBoxSearchAuthorizedAmount.Text = Me.State.authorizedAmount
            Else
                Me.TextBoxSearchAuthorizedAmount.Text = Me.State.authorizedAmountCulture 'Me.State.authorizedAmount
            End If
            Me.TextBoxSearchTrackingNumber.Text = Me.State.trackingNumber
            Me.TextBoxSearchAuthorizationNumber.Text = Me.State.authorizedAmount

            Me.SetSelectedItem(Me.cboSearchDealer, Me.State.selectedDealerId)
            Me.SetSelectedItem(Me.cboSearchClaimStatus, Me.State.selectedClaimStatusId)
            Me.SetSelectedItem(Me.cboSortBy, Me.State.selectedSortById)
            Me.SetSelectedItem(Me.cboSearchHasPendingAuth, Me.State.hasPendingAuthId)
            Me.SetSelectedItem(Me.cboAuthorizationStatus, Me.State.selectedClaimAuthStatusId)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Public Sub ClearState()
        Dim dblAmount As Double

        Try
            Me.State.claimNumber = String.Empty
            Me.State.certificate = String.Empty
            Me.State.customerName = String.Empty

            Me.State.selectedDealerId = Guid.Empty
            Me.State.selectedDealer = String.Empty

            Me.State.selectedClaimStatusId = Guid.Empty
            Me.State.serviceCenterName = String.Empty
            Me.State.serviceRefNumber = String.Empty
            Me.State.selectedSortById = Guid.Empty
            Me.State.trackingNumber = String.Empty

            Me.State.authorizedAmount = String.Empty
            Me.State.authorizedAmountCulture = String.Empty

            Me.State.authorizationNumber = String.Empty
            Me.State.selectedClaimAuthStatusId = Guid.Empty


        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Public Function PopulateStateFromSearchFields() As Boolean
        Dim dblAmount As Double

        Try
            Me.State.claimNumber = Me.TextBoxSearchClaimNumber.Text
            Me.State.certificate = Me.TextBoxSearchCertificate.Text
            Me.State.customerName = Me.TextBoxSearchCustomerName.Text

            Me.State.selectedDealerId = Me.GetSelectedItem(Me.cboSearchDealer)
            Me.State.selectedDealer = LookupListNew.GetCodeFromId(LookupListNew.LK_DEALERS, Me.State.selectedDealerId)

            Me.State.selectedClaimStatusId = Me.GetSelectedItem(Me.cboSearchClaimStatus)
            Me.State.hasPendingAuthId = Me.GetSelectedItem(Me.cboSearchHasPendingAuth)
            Me.State.serviceCenterName = moServiceCenterText.Text
            Me.State.serviceRefNumber = Me.TextBoxSearchSvcRefNumber.Text
            Me.State.selectedSortById = Me.GetSelectedItem(Me.cboSortBy)
            Me.State.trackingNumber = Me.TextBoxSearchTrackingNumber.Text
            Me.State.authorizationNumber = Me.TextBoxSearchAuthorizationNumber.Text
            Me.State.selectedClaimAuthStatusId = Me.GetSelectedItem(Me.cboAuthorizationStatus)

            If Not Me.TextBoxSearchAuthorizedAmount.Text.Trim = String.Empty Then
                If Not Double.TryParse(Me.TextBoxSearchAuthorizedAmount.Text, dblAmount) Then
                    Me.MasterPage.MessageController.AddErrorAndShow(Assurant.ElitaPlus.Common.ErrorCodes.INVALID_AUTHORIZED_AMOUNT_ERR)
                    Return False
                Else
                    Me.State.authorizedAmountCulture = Me.TextBoxSearchAuthorizedAmount.Text
                    Me.State.authorizedAmount = dblAmount.ToString(System.Threading.Thread.CurrentThread.CurrentCulture.InvariantCulture)
                End If
            Else
                Me.State.authorizedAmount = Me.TextBoxSearchAuthorizedAmount.Text
                Me.State.authorizedAmountCulture = Me.TextBoxSearchAuthorizedAmount.Text
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

    Public Sub ClearSearch()
        Try
            Me.TextBoxSearchClaimNumber.Text = String.Empty
            Me.TextBoxSearchCustomerName.Text = String.Empty
            '  Me.cboSearchServiceCenter.SelectedIndex = 0
            moServiceCenterText.Text = String.Empty
            Me.TextBoxSearchSvcRefNumber.Text = String.Empty
            Me.TextBoxSearchAuthorizedAmount.Text = String.Empty
            Me.TextBoxSearchCertificate.Text = String.Empty
            Me.TextBoxSearchTrackingNumber.Text = String.Empty
            Me.TextBoxSearchAuthorizationNumber.Text = String.Empty

            If Not ElitaPlusIdentity.Current.ActiveUser.IsDealer Then
                Me.cboSearchDealer.SelectedIndex = 0
            End If
            Me.cboSearchClaimStatus.SelectedIndex = 0
            Me.cboSearchHasPendingAuth.SelectedIndex = 0
            Me.cboAuthorizationStatus.SelectedIndex = 0
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

#End Region


#Region " Datagrid Related "

    Private Sub Grid_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles Grid.Sorting
        Try
            If Me.State.SortExpression.StartsWith(e.SortExpression) Then
                If Me.State.SortExpression.EndsWith(" DESC") Then
                    Me.State.SortExpression = e.SortExpression
                Else
                    Me.State.SortExpression &= " DESC"
                End If
            Else
                Me.State.SortExpression = e.SortExpression
            End If
            Me.State.PageIndex = 0
            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try

    End Sub

    Private Sub Grid_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Grid.PageIndexChanging
        Try
            Grid.PageIndex = e.NewPageIndex
            State.PageIndex = Grid.PageIndex
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    'The Binding LOgic is here
    Private Sub Grid_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowDataBound
        'Dim itemType As ListItemType = CType(e.Row.ItemType, ListItemType)
        Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
        Dim btnEditClaimItem As LinkButton

        Try
            If e.Row.RowType = DataControlRowType.DataRow OrElse (e.Row.RowType = DataControlRowType.Separator) Then
                If (Not e.Row.Cells(Me.GRID_COL_CLAIM_NUMBER_IDX).FindControl(GRID_COL_CLAIM_NUMBER_CTRL) Is Nothing) Then
                    btnEditClaimItem = CType(e.Row.Cells(Me.GRID_COL_CLAIM_NUMBER_IDX).FindControl(GRID_COL_CLAIM_NUMBER_CTRL), LinkButton)
                    btnEditClaimItem.CommandArgument = e.Row.RowIndex.ToString
                    btnEditClaimItem.CommandName = SELECT_COMMAND_NAME
                    btnEditClaimItem.Text = dvRow(Claim.ClaimSearchDV.COL_CLAIM_NUMBER).ToString
                End If

                Me.PopulateControlFromBOProperty(e.Row.Cells(Me.GRID_COL_STATUS_IDX), dvRow(Claim.ClaimSearchDV.COL_STATUS_CODE))
                e.Row.Cells(Me.GRID_COL_STATUS_IDX).Text = LookupListNew.GetDescriptionFromCode("CLSTAT", dvRow(Claim.ClaimSearchDV.COL_STATUS_CODE).ToString, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
                If (dvRow(Claim.ClaimSearchDV.COL_STATUS_CODE).ToString = Codes.CLAIM_STATUS__ACTIVE) Then
                    e.Row.Cells(Me.GRID_COL_STATUS_IDX).CssClass = "StatActive"
                Else
                    e.Row.Cells(Me.GRID_COL_STATUS_IDX).CssClass = "StatInactive"
                End If
                Me.PopulateControlFromBOProperty(e.Row.Cells(Me.GRID_COL_CUSTOMER_NAME_IDX), dvRow(Claim.ClaimSearchDV.COL_CUSTOMER_NAME))
                Me.PopulateControlFromBOProperty(e.Row.Cells(Me.GRID_COL_SERVICE_CENTER_IDX), dvRow(Claim.ClaimSearchDV.COL_SERVICE_CENTER_NAME))
                Me.PopulateControlFromBOProperty(e.Row.Cells(Me.GRID_COL_DEALER_IDX), dvRow(Claim.ClaimSearchDV.COL_DEALER_CODE))
                Me.PopulateControlFromBOProperty(e.Row.Cells(Me.GRID_COL_CERTIFICATE_IDX), dvRow(Claim.ClaimSearchDV.COL_CERTIFICATE_NUMBER))
                Me.PopulateControlFromBOProperty(e.Row.Cells(Me.GRID_COL_DATE_ADDED_IDX), GetLongDateFormattedString(CType(dvRow(Claim.ClaimSearchDV.COL_DATE_ADDED), Date)))
                Me.PopulateControlFromBOProperty(e.Row.Cells(Me.GRID_COL_PRODUCT_CODE_IDX), dvRow(Claim.ClaimSearchDV.COL_PRODUCT_CODE))
                'Me.PopulateControlFromBOProperty(e.Row.Cells(Me.GRID_COL_AUTHORIZATION_NUMBER_IDX), dvRow(Claim.ClaimSearchDV.COL_AUTHORIZATION_NUMBER))
                Me.PopulateControlFromBOProperty(e.Row.Cells(Me.GRID_COL_TRACKING_NUMBER_IDX), dvRow(Claim.ClaimSearchDV.COL_TRACKING_NUMBER))
                Me.PopulateControlFromBOProperty(e.Row.Cells(Me.GRID_COL_AUTHORIZED_AMOUNT_IDX), dvRow(Claim.ClaimSearchDV.COL_AUTHORIZED_AMOUNT))
                Me.PopulateControlFromBOProperty(e.Row.Cells(Me.GRID_COL_CLAIM_ID_IDX), dvRow(Claim.ClaimSearchDV.COL_CLAIM_ID))
                Me.PopulateControlFromBOProperty(e.Row.Cells(Me.GRID_COL_CLAIM_AUTH_TYPE_ID_IDX), dvRow(Claim.ClaimSearchDV.COL_CLAIM_AUTH_TYPE_ID))
                Me.PopulateControlFromBOProperty(e.Row.Cells(Me.GRID_COL_CLAIM_STATUS_CODE_IDX), dvRow(Claim.ClaimSearchDV.COL_STATUS_CODE))
                Me.PopulateControlFromBOProperty(e.Row.Cells(Me.GRID_COL_AUTHORIZATION_NUMBER_IDX), dvRow(Claim.ClaimSearchDV.COL_AUTHORIZATION_NUMBER))
                Me.PopulateControlFromBOProperty(e.Row.Cells(Me.GRID_COL_AUTHORIZATION_STATUS_IDX), dvRow(Claim.ClaimSearchDV.COL_AUTHORIZATION_STATUS))

            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub cboPageSize_SelectedIndexChanged(ByVal source As Object, ByVal e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
        Try
            State.PageSize = CType(cboPageSize.SelectedValue, Integer)
            Me.State.PageIndex = NewCurrentPageIndex(Grid, State.searchDV.Count, State.PageSize)
            Grid.PageIndex = NewCurrentPageIndex(Grid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Public Sub Grid_RowCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles Grid.RowCommand
        Dim rowIndex As Integer = 0
        Dim claimid As String = String.Empty
        Try
            If (Not e.CommandArgument.ToString().Equals(String.Empty)) And (e.CommandName = SELECT_COMMAND_NAME) Then
                rowIndex = CInt(e.CommandArgument)

                If Me.State Is Nothing Then
                    Me.Trace(Me, "Restoring State")
                    Me.RestoreState(New MyState)
                End If

                claimid = Grid.Rows(rowIndex).Cells(Me.GRID_COL_CLAIM_ID_IDX).Text
                Me.State.selectedClaimId = New Guid(claimid)

                If Grid.Rows(rowIndex).Cells(Me.GRID_COL_CLAIM_STATUS_CODE_IDX).Text = "P" Then
                    Dim claim_auth_type_code As String
                    Dim ClaimAutthTypeid As Guid = New Guid(Grid.Rows(rowIndex).Cells(Me.GRID_COL_CLAIM_AUTH_TYPE_ID_IDX).Text) ' New Guid(BusinessObjectBase.FindRow(Me.State.selectedClaimId, Claim.ClaimSearchDV.COL_CLAIM_ID, Me.State.searchDV.Table)(Claim.ClaimSearchDV.COL_NAME_CLAIM_AUTH_TYPE_ID).ToString())
                    claim_auth_type_code = LookupListNew.GetCodeFromId(LookupListNew.LK_CLAIM_AUTHORIZATION_TYPE, ClaimAutthTypeid)
                    Dim selectedClaimId As Guid = Me.State.selectedClaimId
                    If (claim_auth_type_code = "M") Then
                        Me.NavController = Nothing
                        Me.callPage(ClaimWizardForm.URL, New ClaimWizardForm.Parameters(ClaimWizardForm.ClaimWizardSteps.Step3, Nothing, selectedClaimId, Nothing))
                    Else
                        Dim claimBo As Claim = ClaimFacade.Instance.GetClaim(Of Claim)(Me.State.selectedClaimId)
                        Me.NavController.FlowSession(FlowSessionKeys.SESSION_CLAIM) = claimBo
                        Me.NavController.Navigate(Me, FlowEvents.EVENT_CLAIM_SELECTED)

                    End If
                Else
                    Me.NavController = Nothing
                    Me.callPage(ClaimForm.URL, Me.State.selectedClaimId)
                End If


            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try

    End Sub

    Public Sub RowCreated(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowCreated
        Try
            BaseItemCreated(sender, e)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Grid_PageIndexChanged(ByVal source As Object, ByVal e As System.EventArgs) Handles Grid.PageIndexChanged
        Try
            Me.State.PageIndex = Grid.PageIndex
            Me.State.selectedClaimId = Guid.Empty
            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub
#End Region

#Region " Button Clicks "

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            PopulateStateFromSearchFields()
            Me.State.SearchClicked = True
            Me.State.PageIndex = 0
            Me.State.selectedClaimId = Guid.Empty
            Me.State.IsGridVisible = True
            Me.State.searchDV = Nothing
            Me.State.selectedSortById = New Guid(Me.cboSortBy.SelectedValue)
            Me.State.SortExpression = String.Empty
            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnClearSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearSearch.Click
        Try
            Me.ClearSearch()
            Me.ClearState()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

#End Region
#Region "Navigation Handling"
    Public Sub Process(ByVal callingPage As Page, ByVal navCtrl As INavigationController) Implements IStateController.Process
        Try
            If Not Me.IsPostBack AndAlso navCtrl.CurrentFlow.Name = Me.FLOW_NAME AndAlso
                        Not navCtrl.PrevNavState Is Nothing Then
                Me.IsReturningFromChild = True
                If navCtrl.IsFlowEnded Then
                    Me.State.searchDV = Nothing 'This will force a reload
                    'restart the flow
                    Dim savedState As MyState = CType(navCtrl.State, MyState)
                    Me.StartNavControl()
                    Me.NavController.State = savedState
                End If
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub
#End Region

#Region "State Controller"
    Public Const FLOW_NAME As String = "AUTHORIZE_PENDING_CLAIM"
    Sub StartNavControl()
        Dim nav As New ElitaPlusNavigation
        Me.NavController = New NavControllerBase(nav.Flow(FLOW_NAME))
        Me.NavController.State = New MyState
    End Sub

    Function IsFlowStarted() As Boolean
        Return Not Me.NavController Is Nothing AndAlso Me.NavController.CurrentFlow.Name = Me.FLOW_NAME
    End Function
#End Region

    Private Sub UpdateBreadCrum()
        If (Not Me.State Is Nothing) Then
            If (Not Me.State Is Nothing) Then
                Me.MasterPage.BreadCrum = Me.MasterPage.PageTab & ElitaBase.Sperator &
                    TranslationBase.TranslateLabelOrMessage("CLAIM SEARCH")
                Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("CLAIM SEARCH")
            End If
        End If
    End Sub

End Class



