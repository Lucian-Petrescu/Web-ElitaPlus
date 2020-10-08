
Imports System.Collections.Generic
Imports System.Diagnostics
Imports System.Threading
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports Assurant.Elita.Web.Forms
Imports Assurant.ElitaPlus.Security

Partial Class PendingClaimListForm
    Inherits ElitaPlusSearchPage
    Implements IStateController

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    ' Protected WithEvents ErrControllerMaster As ErrorController

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As Object

    Private Sub Page_Init(sender As Object, e As EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Variables"

    Private mbIsFirstPass As Boolean = True

#End Region

#Region "Class NonTransient Members"
    Protected isReturningFromDetail As Boolean = False
#End Region

#Region "Constants"
    Public Const GRID_COL_EDIT_IDX As Integer = 0
    Public Const GRID_COL_DEALER_IDX As Integer = 1
    Public Const GRID_COL_CERTIFICATE_IDX As Integer = 2
    Public Const GRID_COL_CLAIM_NUMBER_IDX As Integer = 3
    Public Const GRID_COL_DATE_ADDED_IDX As Integer = 4
    'Public Const GRID_COL_STATUS_IDX As Integer = 5
    Public Const GRID_COL_AUTHORIZED_AMOUNT_IDX As Integer = 5
    Public Const GRID_COL_PRODUCT_CODE_IDX As Integer = 6
    Public Const GRID_COL_CLAIM_ID_IDX As Integer = 7

    Public Const MAX_LIMIT As Integer = 1000

    Public Const PAGETITLE As String = "Pending"
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
        Public selectedDealerId As Guid = Guid.Empty
        Public selectedDealer As String = String.Empty
        'Public selectedSortById As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_PENDING_CLAIM_SEARCH_FIELDS, Codes.PENDING_CLAIM_SORT_COLUMN__CLAIM_NUMBER)
        Public selectedSortById As Guid = Guid.Empty
        Public selectedPageSize As Int32 = DEFAULT_PAGE_SIZE
        Public IsGridVisible As Boolean = False
        Public searchDV As Claim.PendingClaimSearchDV = Nothing
        Public SearchClicked As Boolean
        Public bnoRow As Boolean = False

        Sub New()

        End Sub

    End Class

    Public Sub New()
        MyBase.New(New MyState)
    End Sub

    'Protected Shadows ReadOnly Property State() As MyState
    '    Get
    '        Return CType(MyBase.State, MyState)
    '    End Get
    'End Property

    'Protected Shadows ReadOnly Property State() As MyState
    '    Get
    '        Return CType(Me.NavController.State, MyState)
    '    End Get
    'End Property

    Protected Shadows ReadOnly Property State() As MyState
        Get
            'Return CType(MyBase.State, MyState)
            If NavController.State Is Nothing Then
                NavController.State = New MyState
            Else
                If NavController.IsFlowEnded Then
                    'restart flow
                    Dim s As MyState = CType(NavController.State, MyState)
                    StartNavControl()
                    NavController.State = s
                End If
            End If
            Return CType(NavController.State, MyState)
        End Get
    End Property

#End Region


#Region "Page_Events"

    Private Sub Page_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here

        Page.RegisterHiddenField("__EVENTTARGET", btnSearch.ClientID)
        ErrControllerMaster.Clear_Hide()
        Try
            If Not IsPostBack Then
                'This Code must be the first thing to execute
                If Not IsReturningFromChild Then
                    StartNavControl()
                End If
                SetFormTitle(PAGETITLE)
                SetFormTab(PAGETAB)
                Trace(Me, "Cert = " & State.certificate & "@ Claim = " & State.claimNumber)
                SetDefaultButton(TextBoxSearchClaimNumber, btnSearch)
                SetDefaultButton(TextBoxSearchCertificate, btnSearch)
                ControlMgr.SetVisibleControl(Me, trPageSize, False)
                State.selectedSortById = LookupListNew.GetIdFromCode(LookupListNew.LK_PENDING_CLAIM_SEARCH_FIELDS, Codes.PENDING_CLAIM_SORT_COLUMN__CLAIM_NUMBER)
                PopulateDealerDropDown()
                PopulateSearchFieldsFromState()
                TranslateGridHeader(PendingGrid)
                TranslateGridControls(PendingGrid)
                If State.IsGridVisible Then
                    If Not (State.selectedPageSize = 10) Then
                        cboPageSize.SelectedValue = CType(State.selectedPageSize, String)
                        PendingGrid.PageSize = State.selectedPageSize
                    End If
                    PopulateGrid()
                End If
                SetGridItemStyleColor(PendingGrid)
                SetFocus(TextBoxSearchClaimNumber)
            End If
            DisplayProgressBarOnClick(btnSearch, "Loading_Pending_Claims")
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
        ShowMissingTranslations(ErrControllerMaster)
    End Sub

    'Private Sub SetDefaultButton(ByVal txt As TextBox, ByVal defaultButton As Button)
    '    txt.Attributes.Add("onkeydown", "fnTrapKD(" + defaultButton.ClientID + ",event)")
    'End Sub

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

    Sub PopulateDealerDropDown()
        Try

            Dim oDealerList As ListItem() = GetDealerListByCompanyForUser()
            cboSearchDealer.Populate(oDealerList, New PopulateOptions() With
                                                   {
                                                   .AddBlankItem = True
                                                   })

            SetSelectedItem(cboSearchDealer, State.selectedDealerId)
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub



    Private Function GetDealerListByCompanyForUser() As ListItem()
        Dim Index As Integer
        Dim oListContext As New ListContext

        Dim UserCompanies As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Companies

        Dim oDealerList As New List(Of ListItem)

        For Index = 0 To UserCompanies.Count - 1
            'UserCompanyList &= ",'" & GuidControl.GuidToHexString(UserCompanyies(Index))
            oListContext.CompanyId = UserCompanies(Index)
            Dim oDealerListForCompany As ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="DealerListByCompany", context:=oListContext)
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

    Public Sub PopulateGrid()

        Try
            PopulateStateFromSearchFields()

            If (State.searchDV Is Nothing) Then
                Dim sortBy As String = GetSortByColumn

                State.searchDV = Claim.GetPendingClaimList(State.claimNumber, _
                                                                  State.certificate, _
                                                                  State.selectedDealer)
                If (State.SearchClicked) Then
                    ValidSearchResultCount(State.searchDV.Count, True)
                    State.SearchClicked = False
                End If
            End If

            PendingGrid.AutoGenerateColumns = False
            SetPageAndSelectedIndexFromGuid(State.searchDV, State.selectedClaimId, PendingGrid, State.PageIndex)
            State.PageIndex = PendingGrid.PageIndex

            ControlMgr.SetVisibleControl(Me, PendingGrid, State.IsGridVisible)

            ControlMgr.SetVisibleControl(Me, trPageSize, PendingGrid.Visible)

            Session("recCount") = State.searchDV.Count

            If State.searchDV.Count > 0 Then
                If PendingGrid.Visible Then
                    lblRecordCount.Text = State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_CLAIMS_FOUND)
                End If
                State.bnoRow = False
                PendingGrid.DataSource = State.searchDV
                PendingGrid.AllowSorting = False
                PendingGrid.DataBind()
            Else
                If PendingGrid.Visible Then
                    lblRecordCount.Text = State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_CLAIMS_FOUND)
                End If
                State.bnoRow = True
                CreateHeaderForEmptyGrid(PendingGrid, " ASC")
            End If

            If Not PendingGrid.BottomPagerRow.Visible Then
                PendingGrid.BottomPagerRow.Visible = True
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
                Case Codes.PENDING_CLAIM_SORT_COLUMN__CERT_NUMBER
                    Return Claim.PendingClaimSearchDV.COL_NAME_CERTIFICATE_NUMBER
                Case Codes.PENDING_CLAIM_SORT_COLUMN__CLAIM_NUMBER
                    Return Claim.PendingClaimSearchDV.COL_NAME_CLAIM_NUMBER
                Case Codes.PENDING_CLAIM_SORT_COLUMN__DATE_ADDED
                    Return Claim.PendingClaimSearchDV.COL_NAME_DATE_ADDED
                Case Codes.PENDING_CLAIM_SORT_COLUMN__DEALER_CODE
                    Return Claim.PendingClaimSearchDV.COL_NAME_DEALER_CODE
                Case Else
                    Return Nothing
            End Select
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Function

    Public Sub PopulateSearchFieldsFromState()

        Try
            TextBoxSearchCertificate.Text = State.certificate
            TextBoxSearchClaimNumber.Text = State.claimNumber
            SetSelectedItem(cboSearchDealer, State.selectedDealerId)
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try

    End Sub

    Public Sub PopulateStateFromSearchFields()

        Try
            State.claimNumber = TextBoxSearchClaimNumber.Text
            State.certificate = TextBoxSearchCertificate.Text
            State.selectedDealerId = GetSelectedItem(cboSearchDealer)
            State.selectedDealer = LookupListNew.GetCodeFromId(LookupListNew.LK_DEALERS, State.selectedDealerId)

        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try

    End Sub

    Public Sub ClearSearch()
        TextBoxSearchClaimNumber.Text = String.Empty
        TextBoxSearchCertificate.Text = String.Empty
        cboSearchDealer.SelectedIndex = 0
    End Sub

#End Region


#Region " Datagrid Related "

    'The Binding LOgic is here
    Private Sub Grid_ItemBound(source As Object, e As GridViewRowEventArgs) Handles PendingGrid.RowDataBound
        Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
        Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)

        Try
            If dvRow IsNot Nothing AndAlso Not State.bnoRow Then
                If itemType = ListItemType.Item OrElse itemType = ListItemType.AlternatingItem OrElse itemType = ListItemType.SelectedItem Then
                    PopulateControlFromBOProperty(e.Row.Cells(GRID_COL_CLAIM_ID_IDX), dvRow(Claim.PendingClaimSearchDV.COL_NAME_CLAIM_ID))
                    PopulateControlFromBOProperty(e.Row.Cells(GRID_COL_CLAIM_NUMBER_IDX), dvRow(Claim.PendingClaimSearchDV.COL_NAME_CLAIM_NUMBER))
                    PopulateControlFromBOProperty(e.Row.Cells(GRID_COL_CERTIFICATE_IDX), dvRow(Claim.PendingClaimSearchDV.COL_NAME_CERTIFICATE_NUMBER))
                    PopulateControlFromBOProperty(e.Row.Cells(GRID_COL_DATE_ADDED_IDX), GetLongDateFormattedString(CType(dvRow(Claim.PendingClaimSearchDV.COL_NAME_DATE_ADDED), Date)))
                    'Me.PopulateControlFromBOProperty(e.Item.Cells(Me.GRID_COL_STATUS_IDX), dvRow(Claim.PendingClaimSearchDV.COL_NAME_STATUS_CODE))
                    PopulateControlFromBOProperty(e.Row.Cells(GRID_COL_DEALER_IDX), dvRow(Claim.PendingClaimSearchDV.COL_NAME_DEALER_CODE))
                    PopulateControlFromBOProperty(e.Row.Cells(GRID_COL_AUTHORIZED_AMOUNT_IDX), dvRow(Claim.PendingClaimSearchDV.COL_NAME_AUTHORIZED_AMOUNT))
                    PopulateControlFromBOProperty(e.Row.Cells(GRID_COL_PRODUCT_CODE_IDX), dvRow(Claim.PendingClaimSearchDV.COL_NAME_PRODUCT_CODE))
                End If
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub

    Private Sub Grid_PageSizeChanged(source As Object, e As EventArgs) Handles cboPageSize.SelectedIndexChanged
        Try
            PendingGrid.PageIndex = NewCurrentPageIndex(PendingGrid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
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
                Dim row As GridViewRow = PendingGrid.Rows(index)
                State.selectedClaimId = New Guid(row.Cells(GRID_COL_CLAIM_ID_IDX).Text)
                Dim claimBo As Claim = ClaimFacade.Instance.GetClaim(Of Claim)(State.selectedClaimId)
                NavController.FlowSession(FlowSessionKeys.SESSION_CLAIM) = claimBo
                NavController.Navigate(Me, FlowEvents.EVENT_CLAIM_SELECTED)
            End If
        Catch ex As ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try

    End Sub

    Public Sub ItemCreated(sender As Object, e As GridViewRowEventArgs)
        BaseItemCreated(sender, e)
    End Sub

    Private Sub Grid_PageIndexChanged(source As Object, e As GridViewPageEventArgs) Handles PendingGrid.PageIndexChanging
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
            'Me.PopulateSearchFieldsFromState()
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

    'Private Sub btnAdd_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Me.callPage(ClaimForm.URL)
    'End Sub

    Private Sub btnClearSearch_Click(sender As Object, e As EventArgs) Handles btnClearSearch.Click
        Try
            ClearSearch()
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub

#End Region

#Region "Error Handling"


#End Region


#Region "Navigation Handling"
    Public Sub Process(callingPage As Page, navCtrl As INavigationController) Implements IStateController.Process
        Try
            If Not IsPostBack AndAlso navCtrl.CurrentFlow.Name = FLOW_NAME AndAlso _
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
    Public Const FLOW_NAME As String = "AUTHORIZE_PENDING_CLAIM_FROM_PENDING_SEARCH"
    Sub StartNavControl()
        Dim nav As New ElitaPlusNavigation
        NavController = New NavControllerBase(nav.Flow(FLOW_NAME))
        NavController.State = New MyState
    End Sub

    Function IsFlowStarted() As Boolean
        Return NavController IsNot Nothing AndAlso NavController.CurrentFlow.Name = FLOW_NAME
    End Function
#End Region

End Class



