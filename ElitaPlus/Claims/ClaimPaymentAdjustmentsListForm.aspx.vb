Imports System.Threading
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms
Imports Assurant.Elita.CommonConfiguration.DataElements
Partial Class ClaimPaymentAdjustmentsListForm
    Inherits ElitaPlusSearchPage

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents ErrorCtrl As ErrorController
    Protected WithEvents Label7 As System.Web.UI.WebControls.Label
    Protected WithEvents Label1a As System.Web.UI.WebControls.Label
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
    Public Const GRID_COL_CUSTOMER_NAME_IDX As Integer = 3
    Public Const GRID_COL_SERVICE_CENTER_IDX As Integer = 4
    Public Const GRID_COL_AUTHORIZATION_NUMBER_IDX As Integer = 5
    Public Const GRID_COL_AUTHORIZED_AMOUNT_IDX As Integer = 6
    Public Const GRID_COL_CLAIM_ID_IDX As Integer = 7

    Public Const GRID_TOTAL_COLUMNS As Integer = 8
#End Region

#Region "Page State"
    Private IsReturningFromChild As Boolean = False

    Class MyState
        Public PageIndex As Integer = 0
        Public SortExpression As String = Claim.ClaimSearchDV.COL_CLAIM_NUMBER
        Public selectedClaimId As Guid = Guid.Empty
        Public selectedClaimNumber As String = String.Empty
        Public claimNumber As String
        Public customerName As String = String.Empty
        '   Public selectedServiceCenterId As Guid = Guid.Empty
        Public selectedServiceCenter As String = String.Empty
        Public selectedSortById As Guid = Guid.Empty
        Public selectedPageSize As Integer = DEFAULT_PAGE_SIZE
        Public authorizationNumber As String
        Public authorizedAmount As String
        Public IsGridVisible As Boolean = False
        Public searchDV As Claim.ClaimSearchDV = Nothing
        Public SearchClicked As Boolean
        Public ForwardedParameters As ForwardedParameters
        Public authorizedAmountCulture As String

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

#End Region

#Region "Page Parameters"
    Public Class ForwardedParameters
        Public serviceCenter As String = String.Empty
        Public customerName As String = String.Empty
        Public claimNumber As String = String.Empty
        Public ClaimId As Guid = Guid.Empty
        Public Sub New(ClaimId As Guid, claimNumber As String, customerName As String, serviceCenter As String)
            Me.serviceCenter = serviceCenter
            Me.customerName = customerName
            Me.claimNumber = claimNumber
            Me.ClaimId = ClaimId
        End Sub
    End Class
#End Region

#Region "Page_Events"

    Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Page.RegisterHiddenField("__EVENTTARGET", btnSearch.ClientID)
        ErrorCtrl.Clear_Hide()
        Try
            If Not IsPostBack Then
                InitializePage()
            End If
            DisplayProgressBarOnClick(btnSearch, "Loading_Claims")
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
        ShowMissingTranslations(ErrorCtrl)
    End Sub

    Private Sub InitializePage()
        Try
            SetDefaultButton(TextBoxSearchClaimNumber, btnSearch)
            SetDefaultButton(TextBoxSearchAuthorizedAmount, btnSearch)
            SetDefaultButton(TextBoxSearchAuthorizationNumber, btnSearch)
            SetDefaultButton(TextBoxSearchCustomerName, btnSearch)
            SetDefaultButton(moServiceCenterText, btnSearch)
            ControlMgr.SetVisibleControl(Me, trPageSize, False)
            '   PopulateServiceCenterDropDown()
            PopulateSortByDropDown()
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
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

    Private Sub Page_PageReturn(ReturnFromUrl As String, ReturnPar As Object) Handles MyBase.PageReturn
        Try
            MenuEnabled = True
            IsReturningFromChild = True
            Dim retObj As ClaimPaymentAdjustmentsForm.ReturnType = CType(ReturnedValues, ClaimPaymentAdjustmentsForm.ReturnType)
            If retObj IsNot Nothing AndAlso retObj.BoChanged Then
                State.searchDV = Nothing
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

#End Region

#Region "Controlling Logic"

    'Sub PopulateServiceCenterDropDown()
    '    Me.BindListControlToDataView(Me.cboSearchServiceCenter, LookupListNew.GetServiceCenterLookupList(ElitaPlusIdentity.Current.ActiveUser.CompanyId), , , True)
    '    Me.SetSelectedItem(Me.cboSearchServiceCenter, Me.State.selectedServiceCenterId)
    'End Sub

    Sub PopulateSortByDropDown()
        Try
            Dim sortBy As ListItem() = CommonConfigManager.Current.ListManager.GetList("CSEDR", Thread.CurrentPrincipal.GetLanguageCode(), Nothing)
            cboSortBy.Populate(sortBy, New PopulateOptions() With
                                                  {
                                                    .AddBlankItem = False
                                                   })

            Dim defaultSelectedCodeId As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_CLAIM_SEARCH_FIELDS, Codes.DEFAULT_SORT_FOR_CLAIMS)

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

    Public Sub PopulateGrid()

        Try
            If Not PopulateStateFromSearchFields() Then Exit Sub

            If (State.searchDV Is Nothing) Then
                Dim sortBy As String
                sortBy = LookupListNew.GetCodeFromId(LookupListNew.LK_CLAIM_SEARCH_FIELDS, State.selectedSortById)

                State.searchDV = Claim.getNonReworkClaimsList(State.claimNumber,
                                                                      State.customerName,
                                                                      State.selectedServiceCenter,
                                                                      State.authorizationNumber,
                                                                      State.authorizedAmount,
                                                                      sortBy)
                If (State.SearchClicked) Then
                    ValidSearchResultCount(State.searchDV.Count, True, Message.MSG_NO_CLAIM_INVOICE_RECORDS_FOUND)
                    State.SearchClicked = False
                End If
            End If

            Grid.AutoGenerateColumns = False

            SetPageAndSelectedIndexFromGuid(State.searchDV, State.selectedClaimId, Grid, State.PageIndex)
            State.PageIndex = Grid.CurrentPageIndex
            Grid.DataSource = State.searchDV
            Grid.AllowSorting = False
            Grid.DataBind()

            ControlMgr.SetVisibleControl(Me, Grid, State.IsGridVisible)

            ControlMgr.SetVisibleControl(Me, trPageSize, Grid.Visible)

            Session("recCount") = State.searchDV.Count

            If State.searchDV.Count > 0 Then

                If Grid.Visible Then
                    lblRecordCount.Text = State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_CLAIMS_FOUND)
                End If
            Else
                If Grid.Visible Then
                    lblRecordCount.Text = State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_CLAIMS_FOUND)
                End If
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try

    End Sub

    Public Sub PopulateSearchFieldsFromState()

        Try
            TextBoxSearchClaimNumber.Text = State.claimNumber
            TextBoxSearchCustomerName.Text = State.customerName
            '   Me.SetSelectedItem(Me.cboSearchServiceCenter, Me.State.selectedServiceCenterId)
            moServiceCenterText.Text = State.selectedServiceCenter
            TextBoxSearchAuthorizationNumber.Text = State.authorizationNumber
            'Me.TextBoxSearchAuthorizedAmount.Text = Me.State.authorizedAmount

            If State.authorizedAmount Is Nothing Then
                TextBoxSearchAuthorizedAmount.Text = State.authorizedAmount
            Else
                TextBoxSearchAuthorizedAmount.Text = State.authorizedAmountCulture 'Me.State.authorizedAmount
            End If

            SetSelectedItem(cboSortBy, State.selectedSortById)
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

    Public Function PopulateStateFromSearchFields() As Boolean
        Dim dblAmount As Double
        Try
            State.claimNumber = TextBoxSearchClaimNumber.Text.ToUpper
            State.customerName = TextBoxSearchCustomerName.Text.ToUpper
            ' Me.State.selectedServiceCenterId = Me.GetSelectedItem(Me.cboSearchServiceCenter)
            State.selectedServiceCenter = moServiceCenterText.Text.ToUpper
            State.authorizationNumber = TextBoxSearchAuthorizationNumber.Text.ToUpper
            'Me.State.authorizedAmount = Me.TextBoxSearchAuthorizedAmount.Text.ToUpper
            State.selectedSortById = GetSelectedItem(cboSortBy)

            If Not TextBoxSearchAuthorizedAmount.Text.Trim = String.Empty Then
                If Not Double.TryParse(TextBoxSearchAuthorizedAmount.Text, dblAmount) Then
                    ErrorCtrl.AddErrorAndShow(Assurant.ElitaPlus.Common.ErrorCodes.INVALID_AUTHORIZED_AMOUNT_ERR)
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
            HandleErrors(ex, ErrorCtrl)
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
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Function

    Public Sub ClearSearch()
        TextBoxSearchClaimNumber.Text = String.Empty
        TextBoxSearchCustomerName.Text = String.Empty
        '  Me.cboSearchServiceCenter.SelectedIndex = 0
        moServiceCenterText.Text = String.Empty
        TextBoxSearchAuthorizationNumber.Text = String.Empty
        TextBoxSearchAuthorizedAmount.Text = String.Empty
    End Sub

#End Region

#Region " Datagrid Related "

    'The Binding LOgic is here
    Private Sub Grid_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles Grid.ItemDataBound
        Dim itemType As ListItemType = CType(e.Item.ItemType, ListItemType)
        Dim dvRow As DataRowView = CType(e.Item.DataItem, DataRowView)

        Try
            If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then

                PopulateControlFromBOProperty(e.Item.Cells(GRID_COL_CLAIM_ID_IDX), dvRow(Claim.ClaimSearchDV.COL_CLAIM_ID))
                PopulateControlFromBOProperty(e.Item.Cells(GRID_COL_CLAIM_NUMBER_IDX), dvRow(Claim.ClaimSearchDV.COL_CLAIM_NUMBER))
                PopulateControlFromBOProperty(e.Item.Cells(GRID_COL_STATUS_IDX), dvRow(Claim.ClaimSearchDV.COL_STATUS_CODE))
                PopulateControlFromBOProperty(e.Item.Cells(GRID_COL_SERVICE_CENTER_IDX), dvRow(Claim.ClaimSearchDV.COL_SERVICE_CENTER_NAME))
                PopulateControlFromBOProperty(e.Item.Cells(GRID_COL_CUSTOMER_NAME_IDX), dvRow(Claim.ClaimSearchDV.COL_CUSTOMER_NAME))
                PopulateControlFromBOProperty(e.Item.Cells(GRID_COL_AUTHORIZATION_NUMBER_IDX), dvRow(Claim.ClaimSearchDV.COL_AUTHORIZATION_NUMBER))
                PopulateControlFromBOProperty(e.Item.Cells(GRID_COL_AUTHORIZED_AMOUNT_IDX), dvRow(Claim.ClaimSearchDV.COL_AUTHORIZED_AMOUNT))

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

    'Public Sub ItemCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs)
    '    Try
    '        If e.CommandName = "SelectAction" Then
    '            Me.State.selectedClaimId = New Guid(e.Item.Cells(Me.GRID_COL_CLAIM_ID_IDX).Text)
    '            selectedClaimNumber = e.Item.Cells(Me.GRID_COL_CLAIM_NUMBER_IDX).Text
    '            Me.State.customerName = e.Item.Cells(Me.GRID_COL_CUSTOMER_NAME_IDX).Text
    '            Me.State.selectedServiceCenter = e.Item.Cells(Me.GRID_COL_SERVICE_CENTER_IDX).Text
    '            Me.State.ForwardedParameters = New ForwardedParameters(Me.State.selectedClaimId, Me.State.selectedClaimNumber, Me.State.customerName, Me.State.selectedServiceCenter)
    '            Me.callPage(ClaimPaymentAdjustmentsForm.URL, Me.State.ForwardedParameters)
    '        End If
    '    Catch ex As Threading.ThreadAbortException
    '    Catch ex As Exception
    '        Me.HandleErrors(ex, Me.ErrorCtrl)
    '    End Try

    'End Sub

    Public Sub ItemCommand(source As System.Object, e As System.Web.UI.WebControls.DataGridCommandEventArgs)
        Try
            If e.CommandName = "SelectAction" Then
                State.selectedClaimId = New Guid(e.Item.Cells(GRID_COL_CLAIM_ID_IDX).Text)
                Dim oSelectedClaimNumber As String = e.Item.Cells(GRID_COL_CLAIM_NUMBER_IDX).Text
                Dim oCustomerName As String = e.Item.Cells(GRID_COL_CUSTOMER_NAME_IDX).Text
                Dim oSelectedServiceCenter As String = e.Item.Cells(GRID_COL_SERVICE_CENTER_IDX).Text
                State.ForwardedParameters = New ForwardedParameters(State.selectedClaimId, oSelectedClaimNumber, oCustomerName, oSelectedServiceCenter)
                callPage(ClaimPaymentAdjustmentsForm.URL, State.ForwardedParameters)
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

#End Region

End Class



