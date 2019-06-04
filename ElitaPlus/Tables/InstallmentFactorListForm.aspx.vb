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

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
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

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here

        Page.RegisterHiddenField("__EVENTTARGET", Me.btnSearch.ClientID)
        Me.ErrorCtrl.Clear_Hide()
        Try
            If Not Me.IsPostBack Then
                PopulateDealer()
                ControlMgr.SetVisibleControl(Me, trPageSize, False)
                If Me.State.IsGridVisible Then
                    If Not (Me.State.selectedPageSize = DEFAULT_PAGE_SIZE) Then
                        cboPageSize.SelectedValue = CType(Me.State.selectedPageSize, String)
                        Grid.PageSize = Me.State.selectedPageSize
                    End If
                    Me.PopulateGrid()
                End If

                Me.SetGridItemStyleColor(Me.Grid)
            End If
            'Me.DisplayProgressBarOnClick(Me.btnSearch, "Loading_Claims")
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
            Me.TheDealerControl.SelectedGuid = Me.State.DealerId
            ' Me.BindListControlToDataView(moDealerDrop, LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies), , , True)
            'BindSelectItem(Me.State.DealerId.ToString, moDealerDrop)
        Catch ex As Exception
            ErrorCtrl.AddError(BRANCH_LIST_FORM001)
            ErrorCtrl.AddError(ex.Message, False)
            ErrorCtrl.Show()
        End Try
    End Sub

    Private Sub SortSvc(ByVal oSortDv As DataView)
        Try
            If ElitaPlusIdentity.Current.ActiveUser.IsServiceCenter Then
                oSortDv.RowFilter = "(LANGUAGE_ID =  '" & _
                  GuidControl.GuidToHexString(ElitaPlusIdentity.Current.ActiveUser.LanguageId) & _
                  "') AND ((CODE = 'CLNUM') OR (CODE = 'CUSTN'))"
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

    Public Sub PopulateGrid()

        Try
            Me.State.DealerId = TheDealerControl.SelectedGuid

            If (Me.State.searchDV Is Nothing) Then
                Me.State.searchDV = InstallmentFactor.GetInstallmentFactorByDealer(Me.State.DealerId, ElitaPlusIdentity.Current.ActiveUser.Companies)

                If (Me.State.SearchClicked) Then
                    Me.ValidSearchResultCount(Me.State.searchDV.Count, True)
                    Me.State.SearchClicked = False
                End If
            End If

            Me.Grid.AutoGenerateColumns = False

            SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.selectedContractId, Me.Grid, Me.State.PageIndex)
            Me.State.PageIndex = Me.Grid.CurrentPageIndex
            Me.Grid.DataSource = Me.State.searchDV
            Me.Grid.AllowSorting = True

            Me.State.searchDV.Sort = Me.State.SortExpression
            Me.Grid.Columns(Me.GRID_COL_DEALER_IDX).SortExpression = InstallmentFactor.InstallmentFactorSearchDV.COL_DEALER_NAME
            Me.Grid.Columns(Me.GRID_COL_START_DATE_IDX).SortExpression = InstallmentFactor.InstallmentFactorSearchDV.COL_EFFECTIVE_DATE
            Me.Grid.Columns(Me.GRID_COL_END_DATE_IDX).SortExpression = InstallmentFactor.InstallmentFactorSearchDV.COL_EXPIRATION_DATE

            SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.selectedContractId, Me.Grid, Me.State.PageIndex)
            HighLightSortColumn(Grid, Me.State.SortExpression)
            Me.Grid.DataBind()

            ControlMgr.SetVisibleControl(Me, Grid, Me.State.IsGridVisible)

            ControlMgr.SetVisibleControl(Me, trPageSize, Me.Grid.Visible)

            Session("recCount") = Me.State.searchDV.Count

            If Me.State.searchDV.Count > 0 Then

                If Me.Grid.Visible Then
                    Me.lblRecordCount.Text = Me.State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                End If
            Else
                If Me.Grid.Visible Then
                    Me.lblRecordCount.Text = Me.State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                End If
            End If
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

                Me.PopulateControlFromBOProperty(e.Item.Cells(Me.GRID_COL_DEALER_IDX), dvRow(InstallmentFactor.InstallmentFactorSearchDV.COL_DEALER_NAME))
                Me.PopulateControlFromBOProperty(e.Item.Cells(Me.GRID_COL_START_DATE_IDX), dvRow(InstallmentFactor.InstallmentFactorSearchDV.COL_EFFECTIVE_DATE))
                Me.PopulateControlFromBOProperty(e.Item.Cells(Me.GRID_COL_END_DATE_IDX), dvRow(InstallmentFactor.InstallmentFactorSearchDV.COL_EXPIRATION_DATE))
                Me.PopulateControlFromBOProperty(e.Item.Cells(Me.GRID_COL_DEALER_ID_IDX), dvRow(InstallmentFactor.InstallmentFactorSearchDV.COL_DEALER_ID))

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
                Me.State.DealerId = New Guid((e.Item.Cells(Me.GRID_COL_DEALER_ID_IDX).Text))
                Me.State.Effective = CType((e.Item.Cells(Me.GRID_COL_START_DATE_IDX).Text), Date)
                Me.State.Expiration = CType((e.Item.Cells(Me.GRID_COL_END_DATE_IDX).Text), Date)
                params.Add(Me.State.DealerId)
                params.Add(Me.State.Effective.ToString)
                params.Add(Me.State.Expiration.ToString)
                Me.callPage(InstallmentFactorForm.URL, params)
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
            Me.State.selectedContractId = Guid.Empty
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
            Me.State.selectedContractId = Guid.Empty
            Me.State.IsGridVisible = True
            Me.State.searchDV = Nothing
            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

    Private Sub BtnNew_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnNew_WRITE.Click
        Try
            params.Add(Guid.Empty)  ' Dealer_ID
            params.Add("")          ' Effective
            params.Add("")          ' Expiration
            Me.callPage(InstallmentFactorForm.URL, params)
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

    Private Sub btnClearSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearSearch.Click
        Try
            Try
                TheDealerControl.SelectedIndex = 0
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

#End Region

End Class



