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

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
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

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here

        Page.RegisterHiddenField("__EVENTTARGET", Me.btnSearch.ClientID)
        Me.ErrorCtrl.Clear_Hide()
        Try
            If Not Me.IsPostBack Then
                Me.SetDefaultButton(Me.TextBoxSearchClaimNumber, btnSearch)
                Me.SetDefaultButton(Me.TextBoxSearchAuthorizationNumber, btnSearch)
                Me.SetDefaultButton(Me.TextBoxSearchCustomerName, btnSearch)
                Me.SetDefaultButton(Me.TextBoxSearchMasterClaimNumber, btnSearch)

                ControlMgr.SetVisibleControl(Me, trPageSize, False)
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
            Me.State.searchDV = Nothing
            'Dim retObj As ClaimForm.ReturnType = CType(Me.ReturnedValues, ClaimForm.ReturnType)
            'If Not retObj Is Nothing AndAlso retObj.BoChanged Then
            '    Me.State.searchDV = Nothing
            'End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

    Private Sub Page_PageCall(ByVal CallFromUrl As String, ByVal CallingPar As Object) Handles MyBase.PageCall
        Try
            If Not Me.CallingParameters Is Nothing AndAlso CallFromUrl = "/ElitaPlus/Claims/ClaimForm.aspx" Then
                'Me.StartNavControl()
                Me.State.masterClaimNumber = Me.CallingParameters.ToString
            End If
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

    Public Sub PopulateGrid()

        Try
            If Not PopulateStateFromSearchFields() Then Exit Sub

            If (Me.State.searchDV Is Nothing) Then

                Me.State.searchDV = Claim.getMasterClaimListFromArray(Me.State.claimNumber,
                                                                      Me.State.customerName,
                                                                      Me.State.masterClaimNumber,
                                                                      Me.State.authorizationNumber)


                If (Me.State.SearchClicked) Then
                    Me.ValidSearchResultCount(Me.State.searchDV.Count, True)
                    Me.State.SearchClicked = False
                End If
            End If

            Me.Grid.AutoGenerateColumns = False

            SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.selectedClaimId, Me.Grid, Me.State.PageIndex)
            Me.State.PageIndex = Me.Grid.CurrentPageIndex
            Me.Grid.DataSource = Me.State.searchDV
            Me.Grid.AllowSorting = True

            Me.State.searchDV.Sort = Me.State.SortExpression
            Me.Grid.Columns(Me.GRID_COL_MASTER_CLAIM_NUMBER_IDX).SortExpression = Claim.ClaimSearchDV.COL_NAME_MASTER_CLAIM_NUMBER
            Me.Grid.Columns(Me.GRID_COL_CLAIM_NUMBER_IDX).SortExpression = Claim.ClaimSearchDV.COL_NAME_CLAIM_NUMBER
            Me.Grid.Columns(Me.GRID_COL_CUSTOMER_NAME_IDX).SortExpression = Claim.ClaimSearchDV.COL_NAME_CUSTOMER_NAME
            Me.Grid.Columns(Me.GRID_COL_TOTAL_PAID_IDX).SortExpression = Claim.ClaimSearchDV.COL_NAME_TOTAL_PAID
            Me.Grid.Columns(Me.GRID_COL_AUTHORIZED_AMOUNT_IDX).SortExpression = Claim.ClaimSearchDV.COL_NAME_AUTHORIZED_AMOUNT

            SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.selectedClaimId, Me.Grid, Me.State.PageIndex)
            HighLightSortColumn(Grid, Me.State.SortExpression)
            Me.Grid.DataBind()

            ControlMgr.SetVisibleControl(Me, Grid, Me.State.IsGridVisible)

            ControlMgr.SetVisibleControl(Me, trPageSize, Me.Grid.Visible)

            Session("recCount") = Me.State.searchDV.Count

            If Me.State.searchDV.Count > 0 Then

                If Me.Grid.Visible Then
                    Me.lblRecordCount.Text = Me.State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_CLAIMS_FOUND)
                End If
            Else
                If Me.Grid.Visible Then
                    Me.lblRecordCount.Text = Me.State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_CLAIMS_FOUND)
                End If
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

    Public Sub PopulateSearchFieldsFromState()
        Try
            Me.TextBoxSearchMasterClaimNumber.Text = Me.State.masterClaimNumber
            Me.TextBoxSearchCustomerName.Text = Me.State.customerName
            Me.TextBoxSearchClaimNumber.Text = Me.State.claimNumber
            Me.TextBoxSearchAuthorizationNumber.Text = Me.State.authorizationNumber

        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

    Public Function PopulateStateFromSearchFields() As Boolean
        Dim dblAmount As Double

        Try
            Me.State.masterClaimNumber = Me.TextBoxSearchMasterClaimNumber.Text
            Me.State.claimNumber = Me.TextBoxSearchClaimNumber.Text
            Me.State.customerName = Me.TextBoxSearchCustomerName.Text
            Me.State.authorizationNumber = Me.TextBoxSearchAuthorizationNumber.Text

            Return True

        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
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
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Function

    Public Sub ClearSearch()
        Try
            Me.TextBoxSearchMasterClaimNumber.Text = String.Empty
            Me.TextBoxSearchClaimNumber.Text = String.Empty
            Me.TextBoxSearchCustomerName.Text = String.Empty
            Me.TextBoxSearchAuthorizationNumber.Text = String.Empty
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

        Try
            If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then

                Me.PopulateControlFromBOProperty(e.Item.Cells(Me.GRID_COL_MASTER_CLAIM_NUMBER_IDX), dvRow(Claim.ClaimSearchDV.COL_NAME_MASTER_CLAIM_NUMBER))
                Me.PopulateControlFromBOProperty(e.Item.Cells(Me.GRID_COL_CLAIM_NUMBER_IDX), dvRow(Claim.ClaimSearchDV.COL_NAME_CLAIM_NUMBER))
                Me.PopulateControlFromBOProperty(e.Item.Cells(Me.GRID_COL_CUSTOMER_NAME_IDX), dvRow(Claim.ClaimSearchDV.COL_NAME_CUSTOMER_NAME))
                Me.PopulateControlFromBOProperty(e.Item.Cells(Me.GRID_COL_TOTAL_PAID_IDX), dvRow(Claim.ClaimSearchDV.COL_NAME_TOTAL_PAID))
                Me.PopulateControlFromBOProperty(e.Item.Cells(Me.GRID_COL_AUTHORIZED_AMOUNT_IDX), dvRow(Claim.ClaimSearchDV.COL_NAME_AUTHORIZED_AMOUNT))
                Me.PopulateControlFromBOProperty(e.Item.Cells(Me.GRID_COL_CLAIM_ID_IDX), dvRow(Claim.ClaimSearchDV.COL_CLAIM_ID))
                Me.PopulateControlFromBOProperty(e.Item.Cells(Me.GRID_COL_CERT_ID_IDX), dvRow(Claim.ClaimSearchDV.COL_NAME_CERT_ID))

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
        Try
            If e.CommandName = "SelectAction" Then
                Me.State.selectedMasterClaimNumber = CType(e.Item.Cells(Me.GRID_COL_MASTER_CLAIM_NUMBER_IDX).Text, String)
                Me.State.selectedClaimId = New Guid((e.Item.Cells(Me.GRID_COL_CLAIM_ID_IDX).Text))
                Me.State.selectedCertId = New Guid((e.Item.Cells(Me.GRID_COL_CERT_ID_IDX).Text))
                params.Add(Me.State.selectedMasterClaimNumber)
                params.Add(Me.State.selectedClaimId)
                params.Add(Me.State.selectedCertId)
                Me.callPage(ClaimForm.MASTER_CLAIM_DETAIL_URL, params)
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

    Private Sub Grid_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles Grid.SortCommand

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
            Me.HandleErrors(ex, Me.ErrorCtrl)
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

#End Region

End Class



