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
        Public Sub New(ByVal ClaimId As Guid, ByVal claimNumber As String, ByVal customerName As String, ByVal serviceCenter As String)
            Me.serviceCenter = serviceCenter
            Me.customerName = customerName
            Me.claimNumber = claimNumber
            Me.ClaimId = ClaimId
        End Sub
    End Class
#End Region

#Region "Page_Events"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Page.RegisterHiddenField("__EVENTTARGET", Me.btnSearch.ClientID)
        Me.ErrorCtrl.Clear_Hide()
        Try
            If Not Me.IsPostBack Then
                Me.InitializePage()
            End If
            Me.DisplayProgressBarOnClick(Me.btnSearch, "Loading_Claims")
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
        Me.ShowMissingTranslations(Me.ErrorCtrl)
    End Sub

    Private Sub InitializePage()
        Try
            Me.SetDefaultButton(Me.TextBoxSearchClaimNumber, btnSearch)
            Me.SetDefaultButton(Me.TextBoxSearchAuthorizedAmount, btnSearch)
            Me.SetDefaultButton(Me.TextBoxSearchAuthorizationNumber, btnSearch)
            Me.SetDefaultButton(Me.TextBoxSearchCustomerName, btnSearch)
            Me.SetDefaultButton(moServiceCenterText, btnSearch)
            ControlMgr.SetVisibleControl(Me, trPageSize, False)
            '   PopulateServiceCenterDropDown()
            PopulateSortByDropDown()
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
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

    Private Sub Page_PageReturn(ByVal ReturnFromUrl As String, ByVal ReturnPar As Object) Handles MyBase.PageReturn
        Try
            Me.MenuEnabled = True
            Me.IsReturningFromChild = True
            Dim retObj As ClaimPaymentAdjustmentsForm.ReturnType = CType(Me.ReturnedValues, ClaimPaymentAdjustmentsForm.ReturnType)
            If Not retObj Is Nothing AndAlso retObj.BoChanged Then
                Me.State.searchDV = Nothing
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
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

            If (Me.State.selectedSortById.Equals(Guid.Empty)) Then
                Me.SetSelectedItem(Me.cboSortBy, defaultSelectedCodeId)
                Me.State.selectedSortById = defaultSelectedCodeId
            Else
                Me.SetSelectedItem(Me.cboSortBy, Me.State.selectedSortById)
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

    Public Sub PopulateGrid()

        Try
            If Not PopulateStateFromSearchFields() Then Exit Sub

            If (Me.State.searchDV Is Nothing) Then
                Dim sortBy As String
                sortBy = LookupListNew.GetCodeFromId(LookupListNew.LK_CLAIM_SEARCH_FIELDS, Me.State.selectedSortById)

                Me.State.searchDV = Claim.getNonReworkClaimsList(Me.State.claimNumber,
                                                                      Me.State.customerName,
                                                                      Me.State.selectedServiceCenter,
                                                                      Me.State.authorizationNumber,
                                                                      Me.State.authorizedAmount,
                                                                      sortBy)
                If (Me.State.SearchClicked) Then
                    Me.ValidSearchResultCount(Me.State.searchDV.Count, True, Message.MSG_NO_CLAIM_INVOICE_RECORDS_FOUND)
                    Me.State.SearchClicked = False
                End If
            End If

            Me.Grid.AutoGenerateColumns = False

            SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.selectedClaimId, Me.Grid, Me.State.PageIndex)
            Me.State.PageIndex = Me.Grid.CurrentPageIndex
            Me.Grid.DataSource = Me.State.searchDV
            Me.Grid.AllowSorting = False
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
            Me.TextBoxSearchClaimNumber.Text = Me.State.claimNumber
            Me.TextBoxSearchCustomerName.Text = Me.State.customerName
            '   Me.SetSelectedItem(Me.cboSearchServiceCenter, Me.State.selectedServiceCenterId)
            moServiceCenterText.Text = Me.State.selectedServiceCenter
            Me.TextBoxSearchAuthorizationNumber.Text = Me.State.authorizationNumber
            'Me.TextBoxSearchAuthorizedAmount.Text = Me.State.authorizedAmount

            If Me.State.authorizedAmount Is Nothing Then
                Me.TextBoxSearchAuthorizedAmount.Text = Me.State.authorizedAmount
            Else
                Me.TextBoxSearchAuthorizedAmount.Text = Me.State.authorizedAmountCulture 'Me.State.authorizedAmount
            End If

            Me.SetSelectedItem(Me.cboSortBy, Me.State.selectedSortById)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

    Public Function PopulateStateFromSearchFields() As Boolean
        Dim dblAmount As Double
        Try
            Me.State.claimNumber = Me.TextBoxSearchClaimNumber.Text.ToUpper
            Me.State.customerName = Me.TextBoxSearchCustomerName.Text.ToUpper
            ' Me.State.selectedServiceCenterId = Me.GetSelectedItem(Me.cboSearchServiceCenter)
            Me.State.selectedServiceCenter = moServiceCenterText.Text.ToUpper
            Me.State.authorizationNumber = Me.TextBoxSearchAuthorizationNumber.Text.ToUpper
            'Me.State.authorizedAmount = Me.TextBoxSearchAuthorizedAmount.Text.ToUpper
            Me.State.selectedSortById = Me.GetSelectedItem(Me.cboSortBy)

            If Not Me.TextBoxSearchAuthorizedAmount.Text.Trim = String.Empty Then
                If Not Double.TryParse(Me.TextBoxSearchAuthorizedAmount.Text, dblAmount) Then
                    Me.ErrorCtrl.AddErrorAndShow(Assurant.ElitaPlus.Common.ErrorCodes.INVALID_AUTHORIZED_AMOUNT_ERR)
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
        Me.TextBoxSearchClaimNumber.Text = String.Empty
        Me.TextBoxSearchCustomerName.Text = String.Empty
        '  Me.cboSearchServiceCenter.SelectedIndex = 0
        moServiceCenterText.Text = String.Empty
        Me.TextBoxSearchAuthorizationNumber.Text = String.Empty
        Me.TextBoxSearchAuthorizedAmount.Text = String.Empty
    End Sub

#End Region

#Region " Datagrid Related "

    'The Binding LOgic is here
    Private Sub Grid_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles Grid.ItemDataBound
        Dim itemType As ListItemType = CType(e.Item.ItemType, ListItemType)
        Dim dvRow As DataRowView = CType(e.Item.DataItem, DataRowView)

        Try
            If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then

                Me.PopulateControlFromBOProperty(e.Item.Cells(Me.GRID_COL_CLAIM_ID_IDX), dvRow(Claim.ClaimSearchDV.COL_CLAIM_ID))
                Me.PopulateControlFromBOProperty(e.Item.Cells(Me.GRID_COL_CLAIM_NUMBER_IDX), dvRow(Claim.ClaimSearchDV.COL_CLAIM_NUMBER))
                Me.PopulateControlFromBOProperty(e.Item.Cells(Me.GRID_COL_STATUS_IDX), dvRow(Claim.ClaimSearchDV.COL_STATUS_CODE))
                Me.PopulateControlFromBOProperty(e.Item.Cells(Me.GRID_COL_SERVICE_CENTER_IDX), dvRow(Claim.ClaimSearchDV.COL_SERVICE_CENTER_NAME))
                Me.PopulateControlFromBOProperty(e.Item.Cells(Me.GRID_COL_CUSTOMER_NAME_IDX), dvRow(Claim.ClaimSearchDV.COL_CUSTOMER_NAME))
                Me.PopulateControlFromBOProperty(e.Item.Cells(Me.GRID_COL_AUTHORIZATION_NUMBER_IDX), dvRow(Claim.ClaimSearchDV.COL_AUTHORIZATION_NUMBER))
                Me.PopulateControlFromBOProperty(e.Item.Cells(Me.GRID_COL_AUTHORIZED_AMOUNT_IDX), dvRow(Claim.ClaimSearchDV.COL_AUTHORIZED_AMOUNT))

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

    Public Sub ItemCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs)
        Try
            If e.CommandName = "SelectAction" Then
                Me.State.selectedClaimId = New Guid(e.Item.Cells(Me.GRID_COL_CLAIM_ID_IDX).Text)
                Dim oSelectedClaimNumber As String = e.Item.Cells(Me.GRID_COL_CLAIM_NUMBER_IDX).Text
                Dim oCustomerName As String = e.Item.Cells(Me.GRID_COL_CUSTOMER_NAME_IDX).Text
                Dim oSelectedServiceCenter As String = e.Item.Cells(Me.GRID_COL_SERVICE_CENTER_IDX).Text
                Me.State.ForwardedParameters = New ForwardedParameters(Me.State.selectedClaimId, oSelectedClaimNumber, oCustomerName, oSelectedServiceCenter)
                Me.callPage(ClaimPaymentAdjustmentsForm.URL, Me.State.ForwardedParameters)
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
#End Region

#Region " Button Clicks "

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            Me.State.SearchClicked = True
            Me.State.PageIndex = 0
            Me.State.selectedClaimId = Guid.Empty
            Me.State.IsGridVisible = True
            Me.State.searchDV = Nothing
            Me.State.selectedSortById = New Guid(Me.cboSortBy.SelectedValue)
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



