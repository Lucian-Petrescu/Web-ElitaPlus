Partial Class MasterClaimListForm
    Inherits ElitaPlusSearchPage

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
    Public Const GRID_COL_EDIT_IDX As Integer = 0
    Public Const GRID_COL_MASTER_CLAIM_NUMBER_IDX As Integer = 1
    Public Const GRID_COL_CLAIM_NUMBER_IDX As Integer = 2
    Public Const GRID_COL_CUSTOMER_NAME_IDX As Integer = 3
    Public Const GRID_COL_TOTAL_PAID_IDX As Integer = 4
    Public Const GRID_COL_AUTHORIZED_AMOUNT_IDX As Integer = 5
    Public Const GRID_COL_CLAIM_ID_IDX As Integer = 6
    Public Const GRID_COL_CERT_ID_IDX As Integer = 7

    Public Const GRID_TOTAL_COLUMNS As Integer = 8

    Public params As New ArrayList

#End Region

#Region "Page State"
    Private IsReturningFromChild As Boolean = False

    Class MyState
        Public PageIndex As Integer = 0
        Public SortExpression As String = Claim.ClaimSearchDV.COL_NAME_MASTER_CLAIM_NUMBER
        Public selectedClaimId As Guid = Guid.Empty
        Public selectedCertId As Guid = Guid.Empty
        Public selectedMasterClaimNumber As String = ""
        Public claimNumber As String
        Public customerName As String
        Public masterClaimNumber As String
        Public selectedSortById As Guid = Guid.Empty
        Public selectedPageSize As Integer = DEFAULT_PAGE_SIZE
        Public authorizationNumber As String
        Public authorizedAmount As String
        Public IsGridVisible As Boolean = False
        Public searchDV As Claim.ClaimSearchDV = Nothing
        Public SearchClicked As Boolean
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


#Region "Page_Events"

    Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here

        Page.RegisterHiddenField("__EVENTTARGET", btnSearch.ClientID)
        ErrorCtrl.Clear_Hide()
        Try
            If Not IsPostBack Then
                SetDefaultButton(TextBoxSearchClaimNumber, btnSearch)
                SetDefaultButton(TextBoxSearchAuthorizationNumber, btnSearch)
                SetDefaultButton(TextBoxSearchCustomerName, btnSearch)
                SetDefaultButton(TextBoxSearchMasterClaimNumber, btnSearch)

                ControlMgr.SetVisibleControl(Me, trPageSize, False)
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
            State.searchDV = Nothing
            'Dim retObj As ClaimForm.ReturnType = CType(Me.ReturnedValues, ClaimForm.ReturnType)
            'If Not retObj Is Nothing AndAlso retObj.BoChanged Then
            '    Me.State.searchDV = Nothing
            'End If
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

    Private Sub Page_PageCall(CallFromUrl As String, CallingPar As Object) Handles MyBase.PageCall
        Try
            If CallingParameters IsNot Nothing AndAlso CallFromUrl = "/ElitaPlus/Claims/ClaimForm.aspx" Then
                'Me.StartNavControl()
                State.masterClaimNumber = CallingParameters.ToString
            End If
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

    Public Sub PopulateGrid()

        Try
            If Not PopulateStateFromSearchFields() Then Exit Sub

            If (State.searchDV Is Nothing) Then

                State.searchDV = Claim.getMasterClaimListFromArray(State.claimNumber,
                                                                      State.customerName,
                                                                      State.masterClaimNumber,
                                                                      State.authorizationNumber)


                If (State.SearchClicked) Then
                    ValidSearchResultCount(State.searchDV.Count, True)
                    State.SearchClicked = False
                End If
            End If

            Grid.AutoGenerateColumns = False

            SetPageAndSelectedIndexFromGuid(State.searchDV, State.selectedClaimId, Grid, State.PageIndex)
            State.PageIndex = Grid.CurrentPageIndex
            Grid.DataSource = State.searchDV
            Grid.AllowSorting = True

            State.searchDV.Sort = State.SortExpression
            Grid.Columns(GRID_COL_MASTER_CLAIM_NUMBER_IDX).SortExpression = Claim.ClaimSearchDV.COL_NAME_MASTER_CLAIM_NUMBER
            Grid.Columns(GRID_COL_CLAIM_NUMBER_IDX).SortExpression = Claim.ClaimSearchDV.COL_NAME_CLAIM_NUMBER
            Grid.Columns(GRID_COL_CUSTOMER_NAME_IDX).SortExpression = Claim.ClaimSearchDV.COL_NAME_CUSTOMER_NAME
            Grid.Columns(GRID_COL_TOTAL_PAID_IDX).SortExpression = Claim.ClaimSearchDV.COL_NAME_TOTAL_PAID
            Grid.Columns(GRID_COL_AUTHORIZED_AMOUNT_IDX).SortExpression = Claim.ClaimSearchDV.COL_NAME_AUTHORIZED_AMOUNT

            SetPageAndSelectedIndexFromGuid(State.searchDV, State.selectedClaimId, Grid, State.PageIndex)
            HighLightSortColumn(Grid, State.SortExpression)
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
            TextBoxSearchMasterClaimNumber.Text = State.masterClaimNumber
            TextBoxSearchCustomerName.Text = State.customerName
            TextBoxSearchClaimNumber.Text = State.claimNumber
            TextBoxSearchAuthorizationNumber.Text = State.authorizationNumber

        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

    Public Function PopulateStateFromSearchFields() As Boolean
        Dim dblAmount As Double

        Try
            State.masterClaimNumber = TextBoxSearchMasterClaimNumber.Text
            State.claimNumber = TextBoxSearchClaimNumber.Text
            State.customerName = TextBoxSearchCustomerName.Text
            State.authorizationNumber = TextBoxSearchAuthorizationNumber.Text

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
        Try
            TextBoxSearchMasterClaimNumber.Text = String.Empty
            TextBoxSearchClaimNumber.Text = String.Empty
            TextBoxSearchCustomerName.Text = String.Empty
            TextBoxSearchAuthorizationNumber.Text = String.Empty
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

        Try
            If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then

                PopulateControlFromBOProperty(e.Item.Cells(GRID_COL_MASTER_CLAIM_NUMBER_IDX), dvRow(Claim.ClaimSearchDV.COL_NAME_MASTER_CLAIM_NUMBER))
                PopulateControlFromBOProperty(e.Item.Cells(GRID_COL_CLAIM_NUMBER_IDX), dvRow(Claim.ClaimSearchDV.COL_NAME_CLAIM_NUMBER))
                PopulateControlFromBOProperty(e.Item.Cells(GRID_COL_CUSTOMER_NAME_IDX), dvRow(Claim.ClaimSearchDV.COL_NAME_CUSTOMER_NAME))
                PopulateControlFromBOProperty(e.Item.Cells(GRID_COL_TOTAL_PAID_IDX), dvRow(Claim.ClaimSearchDV.COL_NAME_TOTAL_PAID))
                PopulateControlFromBOProperty(e.Item.Cells(GRID_COL_AUTHORIZED_AMOUNT_IDX), dvRow(Claim.ClaimSearchDV.COL_NAME_AUTHORIZED_AMOUNT))
                PopulateControlFromBOProperty(e.Item.Cells(GRID_COL_CLAIM_ID_IDX), dvRow(Claim.ClaimSearchDV.COL_CLAIM_ID))
                PopulateControlFromBOProperty(e.Item.Cells(GRID_COL_CERT_ID_IDX), dvRow(Claim.ClaimSearchDV.COL_NAME_CERT_ID))

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
        Try
            If e.CommandName = "SelectAction" Then
                State.selectedMasterClaimNumber = CType(e.Item.Cells(GRID_COL_MASTER_CLAIM_NUMBER_IDX).Text, String)
                State.selectedClaimId = New Guid((e.Item.Cells(GRID_COL_CLAIM_ID_IDX).Text))
                State.selectedCertId = New Guid((e.Item.Cells(GRID_COL_CERT_ID_IDX).Text))
                params.Add(State.selectedMasterClaimNumber)
                params.Add(State.selectedClaimId)
                params.Add(State.selectedCertId)
                callPage(ClaimForm.MASTER_CLAIM_DETAIL_URL, params)
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

    Private Sub Grid_SortCommand(source As Object, e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles Grid.SortCommand

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
            HandleErrors(ex, ErrorCtrl)
        End Try

    End Sub

#End Region

#Region " Button Clicks "

    Private Sub btnSearch_Click(sender As System.Object, e As System.EventArgs) Handles btnSearch.Click
        Try
            'Me.PopulateSearchFieldsFromState()
            State.SearchClicked = True
            State.PageIndex = 0
            State.selectedClaimId = Guid.Empty
            State.IsGridVisible = True
            State.searchDV = Nothing
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



