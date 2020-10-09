Imports System.Threading
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms
Partial Public Class vscPlanCoveragesRateForm
    Inherits ElitaPlusSearchPage
#Region "Constants"

    Public Const URL As String = "vscPlanCoveragesRateForm.aspx"
    Public Const PAGETITLE As String = "VSC_COVERAGE_RATE"
    Public Const PAGETAB As String = "TABLES"
#End Region

#Region "Page Parameters"
    Public Class Parameters
        Public VSC_RATE_VERSION_ID As Guid
        Public VSC_PLAN_ID As Guid
        Public VSC_Plan As String
        Public Dealer As String
        Public DealerGroup As String
        Public CoverageType As String
        Public AllocPctNew As Decimal
        Public AllocPctUsed As Decimal
        Public AddToPlan As String
        Public ClaimAllowed As String
        Public DealerDiscount As String

        Public Sub New(RateVersionId As Guid, VSCPlanID As Guid, VSCPlan As String,
                        Dealer As String, DealerGroup As String, CoverageType As String,
                        allocPctNew As Decimal, allocPctUsed As Decimal,
                        AddToPlan As String, ClaimAllowed As String, DealerDiscount As String)
            VSC_RATE_VERSION_ID = RateVersionId
            VSC_PLAN_ID = VSCPlanID
            VSC_Plan = VSCPlan
            Me.Dealer = Dealer
            Me.DealerGroup = DealerGroup
            Me.CoverageType = CoverageType
            Me.AllocPctNew = allocPctNew
            Me.AllocPctUsed = allocPctUsed
            Me.AddToPlan = AddToPlan
            Me.ClaimAllowed = ClaimAllowed
            Me.DealerDiscount = DealerDiscount
        End Sub
    End Class
#End Region

#Region "Page Return Type"
    Public Class ReturnType
        Public LastOperation As DetailPageCommand
        Public Sub New(LastOp As DetailPageCommand)
            LastOperation = LastOp
        End Sub
    End Class
#End Region

#Region "Page State"
    Class MyState
        Public ActionInProgress As DetailPageCommand = DetailPageCommand.Nothing_
        Public HasDataChanged As Boolean = False

        Public RateVersionID As Guid
        Public VSCPlanID As Guid
        Public VSCPlan As String
        Public Dealer As String
        Public DealerGroup As String
        Public CoverageType As String
        Public AllocPctNew As Decimal
        Public AllocPctUsed As Decimal
        Public AddToPlan As String
        Public ClaimAllowed As String
        Public DealerDiscount As String


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

#Region "Properties "
    Private _blnAllocForNew As Boolean = True

    Public ReadOnly Property AllocationPercent() As Decimal
        Get
            If _blnAllocForNew Then
                Return State.AllocPctNew
            Else
                Return State.AllocPctUsed
            End If
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
        'Me.ShowMissingTranslations(Me.ErrControllerMaster)
    End Sub

    Private Sub Page_PageCall(CallFromUrl As String, CallingPar As Object) Handles MyBase.PageCall
        Dim objParam As Parameters
        Try
            If CallingParameters IsNot Nothing Then
                objParam = CType(CallingParameters, Parameters)
                State.RateVersionID = objParam.VSC_RATE_VERSION_ID
                State.VSCPlanID = objParam.VSC_PLAN_ID
                State.VSCPlan = objParam.VSC_Plan
                State.Dealer = objParam.Dealer
                State.DealerGroup = objParam.DealerGroup
                State.CoverageType = objParam.CoverageType
                State.ClaimAllowed = objParam.ClaimAllowed
                State.AddToPlan = objParam.AddToPlan
                State.DealerDiscount = objParam.DealerDiscount
                State.AllocPctNew = objParam.AllocPctNew
                State.AllocPctUsed = objParam.AllocPctUsed
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub
#End Region

#Region "Helper functions"

    Private Sub populatePage()
        Dim objRV As VSCRateVersion = New VSCRateVersion(State.RateVersionID)
        txtDealer.Text = State.Dealer
        txtDealerGroup.Text = State.DealerGroup
        txtPlan.Text = State.VSCPlan
        txtVersion.Text = objRV.VersionNumber.ToString
        txtEffectiveDate.Text = objRV.EffectiveDate.Value.ToString("dd-MMM-yyyy")
        txtExpirationDate.Text = objRV.ExpirationDate.Value.ToString("dd-MMM-yyyy")

        txtCoverageType.Text = State.CoverageType
        txtAllocPctNew.Text = State.AllocPctNew.ToString("#,##0.00")
        txtAllocPctUsed.Text = State.AllocPctUsed.ToString("#,##0.00")
        txtDealerDiscount.Text = State.DealerDiscount
        txtAddToPlan.Text = State.AddToPlan
        txtClaimAllowed.Text = State.ClaimAllowed

        'Me.BindListControlToDataView(Me.ddlEngineWarranty, LookupListNew.GetVSCCoverageLimitLookupList(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id))

        Dim CoverageLimits As DataElements.ListItem() =
                CommonConfigManager.Current.ListManager.GetList(listCode:=ListCodes.CoverageLimitByCompanyGroup,
                                                                context:=New ListContext() With
                                                                {
                                                                  .CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
                                                                })

        ddlEngineWarranty.Populate(CoverageLimits.ToArray(),
                                        New PopulateOptions() With
                                        {
                                            .AddBlankItem = True
                                        })

    End Sub

    Private Sub PopulateGrid()

        If State.searchDV Is Nothing Then SearchVSCRateVersion()

        Grid.AutoGenerateColumns = False

        SetPageAndSelectedIndexFromGuid(State.searchDV, Guid.Empty, Grid, State.PageIndex)

        State.PageIndex = Grid.CurrentPageIndex
        Grid.DataSource = State.searchDV
        Grid.DataBind()
        ControlMgr.SetVisibleControl(Me, Grid, State.IsGridVisible)
        ControlMgr.SetVisibleControl(Me, trPageSize, State.IsGridVisible)

        If Grid.Visible Then
            lblRecordCount.Text = State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
        End If

    End Sub

    Private Sub SearchVSCRateVersion()
        Dim intTermMon As Integer = -1, dDeductible As Decimal = -1,
        dVehiclevalue As Decimal = -1
        Dim intOdometer As Integer = -1, gEngineWarranty As Guid
        Dim strTemp As String
        Dim errMsg() As String = New String() {}, hasErr As Boolean = False

        If rdoAllocNew.Checked Then
            _blnAllocForNew = True
        Else
            _blnAllocForNew = False
        End If

        strTemp = txtTermMon.Text.Trim()
        If strTemp <> "" Then
            If Not Integer.TryParse(strTemp, intTermMon) Then
                hasErr = True
                Array.Resize(errMsg, errMsg.Length + 1)
                errMsg(errMsg.Length - 1) = "INVALID_TERM_MONTHS"
                intTermMon = -1
            End If
        End If

        strTemp = txtDeductible.Text.Trim()
        If strTemp <> "" Then
            If Not Decimal.TryParse(strTemp, dDeductible) Then
                hasErr = True
                Array.Resize(errMsg, errMsg.Length + 1)
                errMsg(errMsg.Length - 1) = "INVALID_DEDUCTIBLE"
                dDeductible = -1
            End If
        End If

        strTemp = txtOdometer.Text.Trim()
        If strTemp <> "" Then
            If Not Integer.TryParse(strTemp, intOdometer) Then
                hasErr = True
                Array.Resize(errMsg, errMsg.Length + 1)
                errMsg(errMsg.Length - 1) = "INVALID_ODOMETER"
                intOdometer = -1
            End If
        End If

        strTemp = txtVehiclevalue.Text.Trim()
        If strTemp <> "" Then
            If Not Decimal.TryParse(strTemp, dVehiclevalue) Then
                hasErr = True
                Array.Resize(errMsg, errMsg.Length + 1)
                errMsg(errMsg.Length - 1) = "INVALID_VEHICLE_VALUE"
                dVehiclevalue = -1
            End If
        End If

        gEngineWarranty = GetSelectedItem(ddlEngineWarranty)

        If hasErr Then
            ErrControllerMaster.AddErrorAndShow(errMsg)
            Throw New GUIException("", "")
        Else
            State.searchDV = VscCoverageRate.GetCoverageRateList(State.RateVersionID, State.VSCPlanID, gEngineWarranty, txtClassCode.Text.Trim, intTermMon,
                                dDeductible, intOdometer, dVehiclevalue)
        End If


    End Sub

#End Region

#Region "Button event handler"
    Protected Sub btnBACK_Click(sender As System.Object, e As System.EventArgs) Handles btnBACK.Click
        Try
            ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back))
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub

    Protected Sub btnSearch_Click(sender As System.Object, e As System.EventArgs) Handles btnSearch.Click
        Try
            State.PageIndex = 0
            State.IsGridVisible = True
            State.searchDV = Nothing
            State.HasDataChanged = False
            PopulateGrid()
        Catch ex As Exception
            State.IsGridVisible = False
            ControlMgr.SetVisibleControl(Me, Grid, State.IsGridVisible)
            ControlMgr.SetVisibleControl(Me, trPageSize, State.IsGridVisible)
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub

    Protected Sub btnClearSearch_Click(sender As System.Object, e As System.EventArgs) Handles btnClearSearch.Click
        ddlEngineWarranty.SelectedIndex = 0
        txtClassCode.Text = ""
        txtDeductible.Text = ""
        txtOdometer.Text = ""
        txtTermMon.Text = ""
        txtVehiclevalue.Text = ""
        rdoAllocNew.Checked = True
        rdoAllocUsed.Checked = False
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

    Private Sub Grid_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles Grid.PageIndexChanged
        Try
            State.PageIndex = e.NewPageIndex
            State.RateVersionID = Guid.Empty
            PopulateGrid()
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


End Class