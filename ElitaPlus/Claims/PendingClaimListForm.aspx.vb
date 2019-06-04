
Imports System.Threading
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.Web.Forms
Imports Assurant.ElitaPlus.Security

Partial Class PendingClaimListForm
    Inherits ElitaPlusSearchPage
    Implements IStateController

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    ' Protected WithEvents ErrControllerMaster As ErrorController

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
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
        Public selectedPageSize As Int32 = ElitaPlusSearchPage.DEFAULT_PAGE_SIZE
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
            If Me.NavController.State Is Nothing Then
                Me.NavController.State = New MyState
            Else
                If Me.NavController.IsFlowEnded Then
                    'restart flow
                    Dim s As MyState = CType(Me.NavController.State, MyState)
                    Me.StartNavControl()
                    Me.NavController.State = s
                End If
            End If
            Return CType(Me.NavController.State, MyState)
        End Get
    End Property

#End Region


#Region "Page_Events"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here

        Page.RegisterHiddenField("__EVENTTARGET", Me.btnSearch.ClientID)
        Me.ErrControllerMaster.Clear_Hide()
        Try
            If Not Me.IsPostBack Then
                'This Code must be the first thing to execute
                If Not Me.IsReturningFromChild Then
                    Me.StartNavControl()
                End If
                Me.SetFormTitle(PAGETITLE)
                Me.SetFormTab(PAGETAB)
                Trace(Me, "Cert = " & Me.State.certificate & "@ Claim = " & Me.State.claimNumber)
                Me.SetDefaultButton(Me.TextBoxSearchClaimNumber, btnSearch)
                Me.SetDefaultButton(Me.TextBoxSearchCertificate, btnSearch)
                ControlMgr.SetVisibleControl(Me, trPageSize, False)
                Me.State.selectedSortById = LookupListNew.GetIdFromCode(LookupListNew.LK_PENDING_CLAIM_SEARCH_FIELDS, Codes.PENDING_CLAIM_SORT_COLUMN__CLAIM_NUMBER)
                Me.PopulateDealerDropDown()
                PopulateSearchFieldsFromState()
                Me.TranslateGridHeader(PendingGrid)
                Me.TranslateGridControls(PendingGrid)
                If Me.State.IsGridVisible Then
                    If Not (Me.State.selectedPageSize = 10) Then
                        cboPageSize.SelectedValue = CType(Me.State.selectedPageSize, String)
                        Me.PendingGrid.PageSize = Me.State.selectedPageSize
                    End If
                    Me.PopulateGrid()
                End If
                Me.SetGridItemStyleColor(Me.PendingGrid)
                SetFocus(Me.TextBoxSearchClaimNumber)
            End If
            Me.DisplayProgressBarOnClick(Me.btnSearch, "Loading_Pending_Claims")
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
        Me.ShowMissingTranslations(Me.ErrControllerMaster)
    End Sub

    'Private Sub SetDefaultButton(ByVal txt As TextBox, ByVal defaultButton As Button)
    '    txt.Attributes.Add("onkeydown", "fnTrapKD(" + defaultButton.ClientID + ",event)")
    'End Sub

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

    Sub PopulateDealerDropDown()
        Try

            Dim oDealerList As Assurant.Elita.CommonConfiguration.DataElements.ListItem() = GetDealerListByCompanyForUser()
            cboSearchDealer.Populate(oDealerList, New PopulateOptions() With
                                                   {
                                                   .AddBlankItem = True
                                                   })

            Me.SetSelectedItem(Me.cboSearchDealer, Me.State.selectedDealerId)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
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

    Public Sub PopulateGrid()

        Try
            PopulateStateFromSearchFields()

            If (Me.State.searchDV Is Nothing) Then
                Dim sortBy As String = Me.GetSortByColumn

                Me.State.searchDV = Claim.GetPendingClaimList(Me.State.claimNumber, _
                                                                  Me.State.certificate, _
                                                                  Me.State.selectedDealer)
                If (Me.State.SearchClicked) Then
                    Me.ValidSearchResultCount(Me.State.searchDV.Count, True)
                    Me.State.SearchClicked = False
                End If
            End If

            Me.PendingGrid.AutoGenerateColumns = False
            SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.selectedClaimId, Me.PendingGrid, Me.State.PageIndex)
            Me.State.PageIndex = Me.PendingGrid.PageIndex

            ControlMgr.SetVisibleControl(Me, PendingGrid, Me.State.IsGridVisible)

            ControlMgr.SetVisibleControl(Me, trPageSize, Me.PendingGrid.Visible)

            Session("recCount") = Me.State.searchDV.Count

            If Me.State.searchDV.Count > 0 Then
                If Me.PendingGrid.Visible Then
                    Me.lblRecordCount.Text = Me.State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_CLAIMS_FOUND)
                End If
                Me.State.bnoRow = False
                Me.PendingGrid.DataSource = Me.State.searchDV
                Me.PendingGrid.AllowSorting = False
                Me.PendingGrid.DataBind()
            Else
                If Me.PendingGrid.Visible Then
                    Me.lblRecordCount.Text = Me.State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_CLAIMS_FOUND)
                End If
                Me.State.bnoRow = True
                CreateHeaderForEmptyGrid(PendingGrid, " ASC")
            End If

            If Not PendingGrid.BottomPagerRow.Visible Then
                PendingGrid.BottomPagerRow.Visible = True
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
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Function

    Public Sub PopulateSearchFieldsFromState()

        Try
            Me.TextBoxSearchCertificate.Text = Me.State.certificate
            Me.TextBoxSearchClaimNumber.Text = Me.State.claimNumber
            Me.SetSelectedItem(Me.cboSearchDealer, Me.State.selectedDealerId)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try

    End Sub

    Public Sub PopulateStateFromSearchFields()

        Try
            Me.State.claimNumber = Me.TextBoxSearchClaimNumber.Text
            Me.State.certificate = Me.TextBoxSearchCertificate.Text
            Me.State.selectedDealerId = Me.GetSelectedItem(Me.cboSearchDealer)
            Me.State.selectedDealer = LookupListNew.GetCodeFromId(LookupListNew.LK_DEALERS, Me.State.selectedDealerId)

        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try

    End Sub

    Public Sub ClearSearch()
        Me.TextBoxSearchClaimNumber.Text = String.Empty
        Me.TextBoxSearchCertificate.Text = String.Empty
        Me.cboSearchDealer.SelectedIndex = 0
    End Sub

#End Region


#Region " Datagrid Related "

    'The Binding LOgic is here
    Private Sub Grid_ItemBound(ByVal source As Object, ByVal e As GridViewRowEventArgs) Handles PendingGrid.RowDataBound
        Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
        Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)

        Try
            If Not dvRow Is Nothing And Not Me.State.bnoRow Then
                If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then
                    Me.PopulateControlFromBOProperty(e.Row.Cells(Me.GRID_COL_CLAIM_ID_IDX), dvRow(Claim.PendingClaimSearchDV.COL_NAME_CLAIM_ID))
                    Me.PopulateControlFromBOProperty(e.Row.Cells(Me.GRID_COL_CLAIM_NUMBER_IDX), dvRow(Claim.PendingClaimSearchDV.COL_NAME_CLAIM_NUMBER))
                    Me.PopulateControlFromBOProperty(e.Row.Cells(Me.GRID_COL_CERTIFICATE_IDX), dvRow(Claim.PendingClaimSearchDV.COL_NAME_CERTIFICATE_NUMBER))
                    Me.PopulateControlFromBOProperty(e.Row.Cells(Me.GRID_COL_DATE_ADDED_IDX), GetLongDateFormattedString(CType(dvRow(Claim.PendingClaimSearchDV.COL_NAME_DATE_ADDED), Date)))
                    'Me.PopulateControlFromBOProperty(e.Item.Cells(Me.GRID_COL_STATUS_IDX), dvRow(Claim.PendingClaimSearchDV.COL_NAME_STATUS_CODE))
                    Me.PopulateControlFromBOProperty(e.Row.Cells(Me.GRID_COL_DEALER_IDX), dvRow(Claim.PendingClaimSearchDV.COL_NAME_DEALER_CODE))
                    Me.PopulateControlFromBOProperty(e.Row.Cells(Me.GRID_COL_AUTHORIZED_AMOUNT_IDX), dvRow(Claim.PendingClaimSearchDV.COL_NAME_AUTHORIZED_AMOUNT))
                    Me.PopulateControlFromBOProperty(e.Row.Cells(Me.GRID_COL_PRODUCT_CODE_IDX), dvRow(Claim.PendingClaimSearchDV.COL_NAME_PRODUCT_CODE))
                End If
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub

    Private Sub Grid_PageSizeChanged(ByVal source As Object, ByVal e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
        Try
            PendingGrid.PageIndex = NewCurrentPageIndex(PendingGrid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
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
                Dim row As GridViewRow = Me.PendingGrid.Rows(index)
                Me.State.selectedClaimId = New Guid(row.Cells(Me.GRID_COL_CLAIM_ID_IDX).Text)
                Dim claimBo As Claim = ClaimFacade.Instance.GetClaim(Of Claim)(Me.State.selectedClaimId)
                Me.NavController.FlowSession(FlowSessionKeys.SESSION_CLAIM) = claimBo
                Me.NavController.Navigate(Me, FlowEvents.EVENT_CLAIM_SELECTED)
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try

    End Sub

    Public Sub ItemCreated(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
        BaseItemCreated(sender, e)
    End Sub

    Private Sub Grid_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles PendingGrid.PageIndexChanging
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
            'Me.PopulateSearchFieldsFromState()
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

    'Private Sub btnAdd_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Me.callPage(ClaimForm.URL)
    'End Sub

    Private Sub btnClearSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearSearch.Click
        Try
            Me.ClearSearch()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub

#End Region

#Region "Error Handling"


#End Region


#Region "Navigation Handling"
    Public Sub Process(ByVal callingPage As Page, ByVal navCtrl As INavigationController) Implements IStateController.Process
        Try
            If Not Me.IsPostBack AndAlso navCtrl.CurrentFlow.Name = Me.FLOW_NAME AndAlso _
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
    Public Const FLOW_NAME As String = "AUTHORIZE_PENDING_CLAIM_FROM_PENDING_SEARCH"
    Sub StartNavControl()
        Dim nav As New ElitaPlusNavigation
        Me.NavController = New NavControllerBase(nav.Flow(FLOW_NAME))
        Me.NavController.State = New MyState
    End Sub

    Function IsFlowStarted() As Boolean
        Return Not Me.NavController Is Nothing AndAlso Me.NavController.CurrentFlow.Name = Me.FLOW_NAME
    End Function
#End Region

End Class



