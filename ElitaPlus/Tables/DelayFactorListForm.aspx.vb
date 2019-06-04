Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common

Partial Class DelayFactorListForm
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
    Private Const DELAY_FACTOR_FORM_URL As String = "DelayFactor.aspx"

    Public params As New ArrayList

#End Region

#Region "Page State"
    Private IsReturningFromChild As Boolean = False

    Class MyState
        Public PageIndex As Integer = 0
        Public SortExpression As String = DelayFactor.DelayFactorSearchDV.COL_DEALER_NAME
        Public selectedContractId As Guid = Guid.Empty
        Public DealerId As Guid
        Public Effective As Date
        Public Expiration As Date
        Public selectedSortById As Guid = Guid.Empty
        Public selectedPageSize As Integer = DEFAULT_PAGE_SIZE
        Public IsGridVisible As Boolean = False
        Public searchDV As DelayFactor.DelayFactorSearchDV = Nothing
        Public SearchClicked As Boolean
        Public bnoRow As Boolean = False

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
                Me.SortDirection = Me.State.SortExpression
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
                Me.State.searchDV = DelayFactor.GetDelayFactorByDealer(Me.State.DealerId, ElitaPlusIdentity.Current.ActiveUser.Companies)

                If (Me.State.SearchClicked) Then
                    Me.ValidSearchResultCount(Me.State.searchDV.Count, True)
                    Me.State.SearchClicked = False
                End If
            End If

            Me.State.searchDV.Sort = Me.SortDirection
            Me.Grid.AutoGenerateColumns = False

            SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.selectedContractId, Me.Grid, Me.State.PageIndex)
            SortAndBindGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub
    Private Sub SortAndBindGrid()
        Me.State.PageIndex = Me.Grid.PageIndex
        If (Me.State.searchDV.Count = 0) Then

            Me.State.bnoRow = True
            CreateHeaderForEmptyGrid(Grid, Me.SortDirection)
        Else
            Me.State.bnoRow = False
            Me.Grid.Enabled = True
            Me.Grid.DataSource = Me.State.searchDV
            HighLightSortColumn(Grid, Me.SortDirection)
            Me.Grid.DataBind()
        End If

        If Not Grid.BottomPagerRow.Visible Then Grid.BottomPagerRow.Visible = True

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

    End Sub

#End Region


#Region " GridView Related "
    Public Property SortDirection() As String
        Get
            Return ViewState("SortDirection").ToString
        End Get
        Set(ByVal value As String)
            ViewState("SortDirection") = value
        End Set
    End Property

    'The Binding LOgic is here
    Private Sub Grid_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowDataBound
        Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
        Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)


        Try
            If Not dvRow Is Nothing And Not Me.State.bnoRow Then

                If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then
                    e.Row.Cells(Me.GRID_COL_DEALER_IDX).Text = dvRow(DelayFactor.DelayFactorSearchDV.COL_DEALER_NAME).ToString
                    e.Row.Cells(Me.GRID_COL_START_DATE_IDX).Text = dvRow(DelayFactor.DelayFactorSearchDV.COL_EFFECTIVE_DATE).ToString
                    e.Row.Cells(Me.GRID_COL_END_DATE_IDX).Text = dvRow(DelayFactor.DelayFactorSearchDV.COL_EXPIRATION_DATE).ToString
                    e.Row.Cells(Me.GRID_COL_DEALER_ID_IDX).Text = GetGuidStringFromByteArray(CType(dvRow(DelayFactor.DelayFactorSearchDV.COL_DEALER_ID), Byte()))
                End If
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

    Private Sub Grid_PageSizeChanged(ByVal source As Object, ByVal e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
        Try
            Grid.PageIndex = NewCurrentPageIndex(Grid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
            Me.State.selectedPageSize = CType(cboPageSize.SelectedValue, Integer)
            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

    Public Sub RowCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)
        Try
            If e.CommandName = "SelectAction" Then
                Dim index As Integer = CInt(e.CommandArgument)

                Me.State.DealerId = New Guid((Me.Grid.Rows(index).Cells(Me.GRID_COL_DEALER_ID_IDX).Text))
                Me.State.Effective = CType((Me.Grid.Rows(index).Cells(Me.GRID_COL_START_DATE_IDX).Text), Date)
                Me.State.Expiration = CType((Me.Grid.Rows(index).Cells(Me.GRID_COL_END_DATE_IDX).Text), Date)
                params.Add(Me.State.DealerId)
                params.Add(Me.State.Effective.ToString)
                params.Add(Me.State.Expiration.ToString)
                Me.callPage(DelayFactorForm.URL, params)
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

    Public Sub RowCreated(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
        BaseItemCreated(sender, e)
    End Sub

    Private Sub Grid_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Grid.PageIndexChanging
        Try
            Me.State.PageIndex = e.NewPageIndex
            Me.State.selectedContractId = Guid.Empty
            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

    Private Sub Grid_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles Grid.Sorting
        Try
            Dim spaceIndex As Integer = Me.SortDirection.LastIndexOf(" ")

            If spaceIndex > 0 AndAlso Me.SortDirection.Substring(0, spaceIndex).Equals(e.SortExpression) Then
                If Me.SortDirection.EndsWith(" ASC") Then
                    Me.SortDirection = e.SortExpression + " DESC"
                Else
                    Me.SortDirection = e.SortExpression + " ASC"
                End If
            Else
                Me.SortDirection = e.SortExpression + " ASC"
            End If
            Me.State.SortExpression = Me.SortDirection
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
            Me.callPage(DelayFactorForm.URL, params)
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



