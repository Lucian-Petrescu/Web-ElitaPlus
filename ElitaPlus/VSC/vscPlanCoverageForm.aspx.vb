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

        Public Sub New(ByVal RateVersionId As Guid, ByVal VSCPlan As String, ByVal Dealer As String, ByVal DealerGroup As String)
            Me.VSC_RATE_VERSION_ID = RateVersionId
            Me.VSC_Plan = VSCPlan
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
        Public Sub New(ByVal LastOp As DetailPageCommand, ByVal curEditingBo As VSCRateVersion, ByVal hasDataChanged As Boolean)
            Me.LastOperation = LastOp
            Me.EditingBo = curEditingBo
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
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.ErrControllerMaster.Clear_Hide()
        Try
            If Not Me.IsPostBack Then

                Me.SetFormTitle(PAGETITLE)
                Me.SetFormTab(PAGETAB)

                populatePage()

            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
        Me.ShowMissingTranslations(Me.ErrControllerMaster)
    End Sub

    Private Sub Page_PageCall(ByVal CallFromUrl As String, ByVal CallingPar As Object) Handles MyBase.PageCall
        Dim objParam As Parameters
        Try
            If Not Me.CallingParameters Is Nothing Then
                objParam = CType(Me.CallingParameters, Parameters)
                Me.State.MyBO = New VSCRateVersion(objParam.VSC_RATE_VERSION_ID)
                Me.State.VSC_Plan = objParam.VSC_Plan
                Me.State.Dealer = objParam.Dealer
                Me.State.DealerGroup = objParam.DealerGroup
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub
#End Region

#Region "Helper functions"
    Private Sub populatePage()
        Me.txtDealer.Text = State.Dealer
        Me.txtDealerGroup.Text = State.DealerGroup
        Me.txtPlan.Text = State.VSC_Plan
        Me.txtVersion.Text = State.MyBO.VersionNumber.ToString
        Me.txtEffectiveDate.Text = State.MyBO.EffectiveDate.Value.ToString("dd-MMM-yyyy")
        Me.txtExpirationDate.Text = State.MyBO.ExpirationDate.Value.ToString("dd-MMM-yyyy")

        populateGrid()
    End Sub

    Private Sub populateGrid()
        If Me.State.searchDV Is Nothing Then GetCoverageList()

        Me.Grid.AutoGenerateColumns = False
        SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.VSCCoverageID, Me.Grid, Me.State.PageIndex)

        Me.State.PageIndex = Me.Grid.CurrentPageIndex
        Me.Grid.DataSource = Me.State.searchDV
        Me.Grid.DataBind()
        ControlMgr.SetVisibleControl(Me, Grid, Me.State.IsGridVisible)
        ControlMgr.SetVisibleControl(Me, trPageSize, Me.Grid.Visible)

        If Me.Grid.Visible Then
            Me.lblRecordCount.Text = Me.State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
        End If
    End Sub

    Private Sub GetCoverageList()
        Dim objCR As VSCCoverage = New VSCCoverage
        Me.State.searchDV = objCR.GetCoverageList(Authentication.CurrentUser.LanguageId, State.MyBO.VscPlanId)
    End Sub
#End Region

#Region "Grid related"
    Public Sub ItemCreated(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
        'Dim elemType As ListItemType = e.Item.ItemType
        '' make sure it is the pager bar
        'If elemType = ListItemType.Pager Then
        '    Dim pager As TableCell = DirectCast(e.Item.Controls(0), TableCell)
        '    pager.Attributes.Add("style", "text-align:center;")
        'End If
        BaseItemCreated(sender, e)
    End Sub

    Public Sub ItemCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs)
        Dim index As Integer
        Dim lblControl As Label, dr As DataRow
        Try
            'Find the datarow associated with the selected item
            If e.CommandName = "SelectAction" Then
                index = e.Item.ItemIndex
                lblControl = CType(Me.Grid.Items(e.Item.ItemIndex).Cells(Me.GRID_COL_COVERAGE_ID_IDX).FindControl(Me.GRID_CONTROL_NAME_COVERAGE_ID), Label)
                dr = State.searchDV.Table.Select("ROWNUM=" & lblControl.Text)(0)
                State.VSCCoverageID = New Guid(CType(dr(Me.COL_COVERAGE_ID), Byte()))
                Me.callPage(vscPlanCoveragesRateForm.URL, New vscPlanCoveragesRateForm.Parameters(State.MyBO.Id, State.MyBO.VscPlanId, State.VSC_Plan, State.Dealer, State.DealerGroup, _
                    dr(Me.COL_COVERAGE_TYPE).ToString, CType(dr(Me.COL_ALLOC_NEW), Decimal), CType(dr(Me.COL_ALLOC_USED), Decimal), dr(Me.COL_ADD_TO_PLAN).ToString, dr(Me.COL_CLAIM_ALLOWED).ToString, dr(Me.COL_DEALER_DISCOUNT).ToString))
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub

    Private Sub Grid_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles Grid.PageIndexChanged
        Try
            Me.State.PageIndex = e.NewPageIndex
            Me.State.VSCCoverageID = Guid.Empty
            Me.populateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub

    Protected Sub cboPageSize_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
        Try
            State.PageSize = CType(cboPageSize.SelectedValue, Integer)
            Me.State.PageIndex = NewCurrentPageIndex(Grid, State.searchDV.Count, State.PageSize)
            Me.Grid.CurrentPageIndex = Me.State.PageIndex
            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub

#End Region

#Region "Button click"
    Protected Sub btnBACK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBACK.Click
        Try
            Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.MyBO, Me.State.HasDataChanged))
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub
#End Region
End Class