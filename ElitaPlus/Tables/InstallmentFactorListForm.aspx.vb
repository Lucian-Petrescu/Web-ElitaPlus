Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common

Partial Class InstallmentFactorListForm
    Inherits ElitaPlusSearchPage

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    'Protected WithEvents ErrorCtrl As ErrorController
    Protected WithEvents lblBlank As System.Web.UI.WebControls.Label
    Protected WithEvents trSortBy As System.Web.UI.HtmlControls.HtmlTableRow
    'Protected WithEvents multipleDropControl As MultipleColumnDDLabelControl

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
    Public Const GRID_COL_DEALER_IDX As Integer = 1
    Public Const GRID_COL_START_DATE_IDX As Integer = 2
    Public Const GRID_COL_END_DATE_IDX As Integer = 3
    Public Const GRID_COL_DEALER_ID_IDX As Integer = 4
    Public Const GRID_TOTAL_COLUMNS As Integer = 5

    Private Const LABEL_SELECT_DEALERCODE As String = "DEALER"
    Private Const BRANCH_LIST_FORM001 As String = "BRANCH_LIST_FORM001" ' Maintain Branch List Exception
    Private Const INSTALLMENT_FACTOR_FORM_URL As String = "InstallmentFactorForm.aspx"

    Public params As New ArrayList

#End Region

#Region "Page State"
    Private IsReturningFromChild As Boolean = False

    Class MyState
        Public PageIndex As Integer = 0
        Public SortExpression As String = InstallmentFactor.InstallmentFactorSearchDV.COL_DEALER_NAME
        Public selectedContractId As Guid = Guid.Empty
        Public DealerId As Guid
        Public Effective As Date
        Public Expiration As Date
        Public selectedSortById As Guid = Guid.Empty
        Public selectedPageSize As Integer = DEFAULT_PAGE_SIZE
        Public IsGridVisible As Boolean = False
        Public searchDV As InstallmentFactor.InstallmentFactorSearchDV = Nothing
        Public SearchClicked As Boolean

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

#Region "Properties"

    Public ReadOnly Property TheDealerControl() As MultipleColumnDDLabelControl
        Get
            If multipleDropControl Is Nothing Then
                multipleDropControl = CType(FindControl("multipleDropControl"), MultipleColumnDDLabelControl)
            End If
            Return multipleDropControl
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
                PopulateDealer()
                ControlMgr.SetVisibleControl(Me, trPageSize, False)
                If State.IsGridVisible Then
                    If Not (State.selectedPageSize = DEFAULT_PAGE_SIZE) Then
                        cboPageSize.SelectedValue = CType(State.selectedPageSize, String)
                        Grid.PageSize = State.selectedPageSize
                    End If
                    PopulateGrid()
                End If

                SetGridItemStyleColor(Grid)
            End If
            'Me.DisplayProgressBarOnClick(Me.btnSearch, "Loading_Claims")
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

    End Sub

#End Region

#Region "Controlling Logic"

    Private Sub PopulateDealer()
        Try
            Dim oDealerview As DataView = LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies)
            TheDealerControl.SetControl(False, _
                                        TheDealerControl.MODES.NEW_MODE, _
                                        True, _
                                        oDealerview, _
                                        TranslationBase.TranslateLabelOrMessage(LABEL_SELECT_DEALERCODE), _
                                        True, True, _
                                        , _
                                        "multipleDropControl_moMultipleColumnDrop", _
                                        "multipleDropControl_moMultipleColumnDropDesc", _
                                        "multipleDropControl_lb_DropDown", _
                                        False, _
                                        0)
            TheDealerControl.SelectedGuid = State.DealerId
            ' Me.BindListControlToDataView(moDealerDrop, LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies), , , True)
            'BindSelectItem(Me.State.DealerId.ToString, moDealerDrop)
        Catch ex As Exception
            ErrorCtrl.AddError(BRANCH_LIST_FORM001)
            ErrorCtrl.AddError(ex.Message, False)
            ErrorCtrl.Show()
        End Try
    End Sub

    Private Sub SortSvc(oSortDv As DataView)
        Try
            If ElitaPlusIdentity.Current.ActiveUser.IsServiceCenter Then
                oSortDv.RowFilter = "(LANGUAGE_ID =  '" & _
                  GuidControl.GuidToHexString(ElitaPlusIdentity.Current.ActiveUser.LanguageId) & _
                  "') AND ((CODE = 'CLNUM') OR (CODE = 'CUSTN'))"
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

    Public Sub PopulateGrid()

        Try
            State.DealerId = TheDealerControl.SelectedGuid

            If (State.searchDV Is Nothing) Then
                State.searchDV = InstallmentFactor.GetInstallmentFactorByDealer(State.DealerId, ElitaPlusIdentity.Current.ActiveUser.Companies)

                If (State.SearchClicked) Then
                    ValidSearchResultCount(State.searchDV.Count, True)
                    State.SearchClicked = False
                End If
            End If

            Grid.AutoGenerateColumns = False

            SetPageAndSelectedIndexFromGuid(State.searchDV, State.selectedContractId, Grid, State.PageIndex)
            State.PageIndex = Grid.CurrentPageIndex
            Grid.DataSource = State.searchDV
            Grid.AllowSorting = True

            State.searchDV.Sort = State.SortExpression
            Grid.Columns(GRID_COL_DEALER_IDX).SortExpression = InstallmentFactor.InstallmentFactorSearchDV.COL_DEALER_NAME
            Grid.Columns(GRID_COL_START_DATE_IDX).SortExpression = InstallmentFactor.InstallmentFactorSearchDV.COL_EFFECTIVE_DATE
            Grid.Columns(GRID_COL_END_DATE_IDX).SortExpression = InstallmentFactor.InstallmentFactorSearchDV.COL_EXPIRATION_DATE

            SetPageAndSelectedIndexFromGuid(State.searchDV, State.selectedContractId, Grid, State.PageIndex)
            HighLightSortColumn(Grid, State.SortExpression)
            Grid.DataBind()

            ControlMgr.SetVisibleControl(Me, Grid, State.IsGridVisible)

            ControlMgr.SetVisibleControl(Me, trPageSize, Grid.Visible)

            Session("recCount") = State.searchDV.Count

            If State.searchDV.Count > 0 Then

                If Grid.Visible Then
                    lblRecordCount.Text = State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                End If
            Else
                If Grid.Visible Then
                    lblRecordCount.Text = State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                End If
            End If
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

                PopulateControlFromBOProperty(e.Item.Cells(GRID_COL_DEALER_IDX), dvRow(InstallmentFactor.InstallmentFactorSearchDV.COL_DEALER_NAME))
                PopulateControlFromBOProperty(e.Item.Cells(GRID_COL_START_DATE_IDX), dvRow(InstallmentFactor.InstallmentFactorSearchDV.COL_EFFECTIVE_DATE))
                PopulateControlFromBOProperty(e.Item.Cells(GRID_COL_END_DATE_IDX), dvRow(InstallmentFactor.InstallmentFactorSearchDV.COL_EXPIRATION_DATE))
                PopulateControlFromBOProperty(e.Item.Cells(GRID_COL_DEALER_ID_IDX), dvRow(InstallmentFactor.InstallmentFactorSearchDV.COL_DEALER_ID))

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
                State.DealerId = New Guid((e.Item.Cells(GRID_COL_DEALER_ID_IDX).Text))
                State.Effective = CType((e.Item.Cells(GRID_COL_START_DATE_IDX).Text), Date)
                State.Expiration = CType((e.Item.Cells(GRID_COL_END_DATE_IDX).Text), Date)
                params.Add(State.DealerId)
                params.Add(State.Effective.ToString)
                params.Add(State.Expiration.ToString)
                callPage(InstallmentFactorForm.URL, params)
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
            State.selectedContractId = Guid.Empty
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
            State.selectedContractId = Guid.Empty
            State.IsGridVisible = True
            State.searchDV = Nothing
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

    Private Sub BtnNew_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles BtnNew_WRITE.Click
        Try
            params.Add(Guid.Empty)  ' Dealer_ID
            params.Add("")          ' Effective
            params.Add("")          ' Expiration
            callPage(InstallmentFactorForm.URL, params)
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

    Private Sub btnClearSearch_Click(sender As System.Object, e As System.EventArgs) Handles btnClearSearch.Click
        Try
            Try
                TheDealerControl.SelectedIndex = 0
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

#End Region

End Class



