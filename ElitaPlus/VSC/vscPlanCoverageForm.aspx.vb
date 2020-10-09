Public Partial Class vscPlanCoverageForm
    Inherits ElitaPlusSearchPage

#Region "Constants"

    Public Const URL As String = "vscPlanCoverageForm.aspx"
    Public Const PAGETITLE As String = "VSC_PLAN_COVERAGE"
    Public Const PAGETAB As String = "TABLES"

    Private Const COL_COVERAGE_ID As String = "VSC_COVERAGE_id"
    Private Const COL_ALLOC_USED As String = "ALLOCATION_PERCENT_USED"
    Private Const COL_ALLOC_NEW As String = "ALLOCATION_PERCENT_NEW"
    Private Const COL_COVERAGE_TYPE As String = "Coverage_Type"
    Private Const COL_DEALER_DISCOUNT As String = "IS_DEALER_DISCOUNT"
    Private Const COL_ADD_TO_PLAN As String = "ADD_TO_PLAN"
    Private Const COL_CLAIM_ALLOWED As String = "IS_CLAIM_ALLOWED"

    Private Const GRID_COL_COVERAGE_ID_IDX As Integer = 1
    Private Const GRID_CONTROL_NAME_COVERAGE_ID As String = "VSCCoverageId"

#End Region

#Region "Page Parameters"
    Public Class Parameters
        Public VSC_RATE_VERSION_ID As Guid
        Public VSC_Plan As String
        Public Dealer As String
        Public DealerGroup As String

        Public Sub New(RateVersionId As Guid, VSCPlan As String, Dealer As String, DealerGroup As String)
            VSC_RATE_VERSION_ID = RateVersionId
            VSC_Plan = VSCPlan
            Me.Dealer = Dealer
            Me.DealerGroup = DealerGroup
        End Sub
    End Class
#End Region

#Region "Page Return Type"
    Public Class ReturnType
        Public LastOperation As DetailPageCommand
        Public EditingBo As VSCRateVersion
        Public HasDataChanged As Boolean
        Public Sub New(LastOp As DetailPageCommand, curEditingBo As VSCRateVersion, hasDataChanged As Boolean)
            LastOperation = LastOp
            EditingBo = curEditingBo
            Me.HasDataChanged = hasDataChanged
        End Sub
    End Class
#End Region

#Region "Page State"
    Class MyState
        Public MyBO As VSCRateVersion
        Public VSCCoverageID As Guid
        Public ActionInProgress As DetailPageCommand = DetailPageCommand.Nothing_
        Public LastErrMsg As String
        Public HasDataChanged As Boolean = False

        Public VSC_Plan As String
        Public Dealer As String
        Public DealerGroup As String

        Public PageIndex As Integer = 0
        Public IsGridVisible As Boolean = True
        Public PageSize As Integer = DEFAULT_PAGE_SIZE
        Public searchDV As DataView = Nothing

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

#Region "Page events"
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        ErrControllerMaster.Clear_Hide()
        Try
            If Not IsPostBack Then

                SetFormTitle(PAGETITLE)
                SetFormTab(PAGETAB)

                populatePage()

            End If
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
        ShowMissingTranslations(ErrControllerMaster)
    End Sub

    Private Sub Page_PageCall(CallFromUrl As String, CallingPar As Object) Handles MyBase.PageCall
        Dim objParam As Parameters
        Try
            If CallingParameters IsNot Nothing Then
                objParam = CType(CallingParameters, Parameters)
                State.MyBO = New VSCRateVersion(objParam.VSC_RATE_VERSION_ID)
                State.VSC_Plan = objParam.VSC_Plan
                State.Dealer = objParam.Dealer
                State.DealerGroup = objParam.DealerGroup
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub
#End Region

#Region "Helper functions"
    Private Sub populatePage()
        txtDealer.Text = State.Dealer
        txtDealerGroup.Text = State.DealerGroup
        txtPlan.Text = State.VSC_Plan
        txtVersion.Text = State.MyBO.VersionNumber.ToString
        txtEffectiveDate.Text = State.MyBO.EffectiveDate.Value.ToString("dd-MMM-yyyy")
        txtExpirationDate.Text = State.MyBO.ExpirationDate.Value.ToString("dd-MMM-yyyy")

        populateGrid()
    End Sub

    Private Sub populateGrid()
        If State.searchDV Is Nothing Then GetCoverageList()

        Grid.AutoGenerateColumns = False
        SetPageAndSelectedIndexFromGuid(State.searchDV, State.VSCCoverageID, Grid, State.PageIndex)

        State.PageIndex = Grid.CurrentPageIndex
        Grid.DataSource = State.searchDV
        Grid.DataBind()
        ControlMgr.SetVisibleControl(Me, Grid, State.IsGridVisible)
        ControlMgr.SetVisibleControl(Me, trPageSize, Grid.Visible)

        If Grid.Visible Then
            lblRecordCount.Text = State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
        End If
    End Sub

    Private Sub GetCoverageList()
        Dim objCR As VSCCoverage = New VSCCoverage
        State.searchDV = objCR.GetCoverageList(Authentication.CurrentUser.LanguageId, State.MyBO.VscPlanId)
    End Sub
#End Region

#Region "Grid related"
    Public Sub ItemCreated(sender As System.Object, e As System.Web.UI.WebControls.DataGridItemEventArgs)
        'Dim elemType As ListItemType = e.Item.ItemType
        '' make sure it is the pager bar
        'If elemType = ListItemType.Pager Then
        '    Dim pager As TableCell = DirectCast(e.Item.Controls(0), TableCell)
        '    pager.Attributes.Add("style", "text-align:center;")
        'End If
        BaseItemCreated(sender, e)
    End Sub

    Public Sub ItemCommand(source As System.Object, e As System.Web.UI.WebControls.DataGridCommandEventArgs)
        Dim index As Integer
        Dim lblControl As Label, dr As DataRow
        Try
            'Find the datarow associated with the selected item
            If e.CommandName = "SelectAction" Then
                index = e.Item.ItemIndex
                lblControl = CType(Grid.Items(e.Item.ItemIndex).Cells(GRID_COL_COVERAGE_ID_IDX).FindControl(GRID_CONTROL_NAME_COVERAGE_ID), Label)
                dr = State.searchDV.Table.Select("ROWNUM=" & lblControl.Text)(0)
                State.VSCCoverageID = New Guid(CType(dr(COL_COVERAGE_ID), Byte()))
                callPage(vscPlanCoveragesRateForm.URL, New vscPlanCoveragesRateForm.Parameters(State.MyBO.Id, State.MyBO.VscPlanId, State.VSC_Plan, State.Dealer, State.DealerGroup, _
                    dr(COL_COVERAGE_TYPE).ToString, CType(dr(COL_ALLOC_NEW), Decimal), CType(dr(COL_ALLOC_USED), Decimal), dr(COL_ADD_TO_PLAN).ToString, dr(COL_CLAIM_ALLOWED).ToString, dr(COL_DEALER_DISCOUNT).ToString))
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub

    Private Sub Grid_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles Grid.PageIndexChanged
        Try
            State.PageIndex = e.NewPageIndex
            State.VSCCoverageID = Guid.Empty
            populateGrid()
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub

    Protected Sub cboPageSize_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
        Try
            State.PageSize = CType(cboPageSize.SelectedValue, Integer)
            State.PageIndex = NewCurrentPageIndex(Grid, State.searchDV.Count, State.PageSize)
            Grid.CurrentPageIndex = State.PageIndex
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub

#End Region

#Region "Button click"
    Protected Sub btnBACK_Click(sender As System.Object, e As System.EventArgs) Handles btnBACK.Click
        Try
            ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, State.MyBO, State.HasDataChanged))
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub
#End Region
End Class