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

        Public Sub New(ByVal RateVersionId As Guid, ByVal VSCPlanID As Guid, ByVal VSCPlan As String,
                        ByVal Dealer As String, ByVal DealerGroup As String, ByVal CoverageType As String,
                        ByVal allocPctNew As Decimal, ByVal allocPctUsed As Decimal,
                        ByVal AddToPlan As String, ByVal ClaimAllowed As String, ByVal DealerDiscount As String)
            Me.VSC_RATE_VERSION_ID = RateVersionId
            Me.VSC_PLAN_ID = VSCPlanID
            Me.VSC_Plan = VSCPlan
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
        Public Sub New(ByVal LastOp As DetailPageCommand)
            Me.LastOperation = LastOp
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
        'Me.ShowMissingTranslations(Me.ErrControllerMaster)
    End Sub

    Private Sub Page_PageCall(ByVal CallFromUrl As String, ByVal CallingPar As Object) Handles MyBase.PageCall
        Dim objParam As Parameters
        Try
            If Not Me.CallingParameters Is Nothing Then
                objParam = CType(Me.CallingParameters, Parameters)
                Me.State.RateVersionID = objParam.VSC_RATE_VERSION_ID
                Me.State.VSCPlanID = objParam.VSC_PLAN_ID
                Me.State.VSCPlan = objParam.VSC_Plan
                Me.State.Dealer = objParam.Dealer
                Me.State.DealerGroup = objParam.DealerGroup
                Me.State.CoverageType = objParam.CoverageType
                Me.State.ClaimAllowed = objParam.ClaimAllowed
                Me.State.AddToPlan = objParam.AddToPlan
                Me.State.DealerDiscount = objParam.DealerDiscount
                Me.State.AllocPctNew = objParam.AllocPctNew
                Me.State.AllocPctUsed = objParam.AllocPctUsed
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub
#End Region

#Region "Helper functions"

    Private Sub populatePage()
        Dim objRV As VSCRateVersion = New VSCRateVersion(State.RateVersionID)
        Me.txtDealer.Text = State.Dealer
        Me.txtDealerGroup.Text = State.DealerGroup
        Me.txtPlan.Text = State.VSCPlan
        Me.txtVersion.Text = objRV.VersionNumber.ToString
        Me.txtEffectiveDate.Text = objRV.EffectiveDate.Value.ToString("dd-MMM-yyyy")
        Me.txtExpirationDate.Text = objRV.ExpirationDate.Value.ToString("dd-MMM-yyyy")

        Me.txtCoverageType.Text = State.CoverageType
        Me.txtAllocPctNew.Text = State.AllocPctNew.ToString("#,##0.00")
        Me.txtAllocPctUsed.Text = State.AllocPctUsed.ToString("#,##0.00")
        Me.txtDealerDiscount.Text = State.DealerDiscount
        Me.txtAddToPlan.Text = State.AddToPlan
        Me.txtClaimAllowed.Text = State.ClaimAllowed

        'Me.BindListControlToDataView(Me.ddlEngineWarranty, LookupListNew.GetVSCCoverageLimitLookupList(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id))

        Dim CoverageLimits As DataElements.ListItem() =
                CommonConfigManager.Current.ListManager.GetList(listCode:=ListCodes.CoverageLimitByCompanyGroup,
                                                                context:=New ListContext() With
                                                                {
                                                                  .CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
                                                                })

        Me.ddlEngineWarranty.Populate(CoverageLimits.ToArray(),
                                        New PopulateOptions() With
                                        {
                                            .AddBlankItem = True
                                        })

    End Sub

    Private Sub PopulateGrid()

        If Me.State.searchDV Is Nothing Then SearchVSCRateVersion()

        Me.Grid.AutoGenerateColumns = False

        SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Guid.Empty, Me.Grid, Me.State.PageIndex)

        Me.State.PageIndex = Me.Grid.CurrentPageIndex
        Me.Grid.DataSource = Me.State.searchDV
        Me.Grid.DataBind()
        ControlMgr.SetVisibleControl(Me, Grid, Me.State.IsGridVisible)
        ControlMgr.SetVisibleControl(Me, trPageSize, Me.State.IsGridVisible)

        If Me.Grid.Visible Then
            Me.lblRecordCount.Text = Me.State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
        End If

    End Sub

    Private Sub SearchVSCRateVersion()
        Dim intTermMon As Integer = -1, dDeductible As Decimal = -1,
        dVehiclevalue As Decimal = -1
        Dim intOdometer As Integer = -1, gEngineWarranty As Guid
        Dim strTemp As String
        Dim errMsg() As String = New String() {}, hasErr As Boolean = False

        If Me.rdoAllocNew.Checked Then
            Me._blnAllocForNew = True
        Else
            Me._blnAllocForNew = False
        End If

        strTemp = Me.txtTermMon.Text.Trim()
        If strTemp <> "" Then
            If Not Integer.TryParse(strTemp, intTermMon) Then
                hasErr = True
                Array.Resize(errMsg, errMsg.Length + 1)
                errMsg(errMsg.Length - 1) = "INVALID_TERM_MONTHS"
                intTermMon = -1
            End If
        End If

        strTemp = Me.txtDeductible.Text.Trim()
        If strTemp <> "" Then
            If Not Decimal.TryParse(strTemp, dDeductible) Then
                hasErr = True
                Array.Resize(errMsg, errMsg.Length + 1)
                errMsg(errMsg.Length - 1) = "INVALID_DEDUCTIBLE"
                dDeductible = -1
            End If
        End If

        strTemp = Me.txtOdometer.Text.Trim()
        If strTemp <> "" Then
            If Not Integer.TryParse(strTemp, intOdometer) Then
                hasErr = True
                Array.Resize(errMsg, errMsg.Length + 1)
                errMsg(errMsg.Length - 1) = "INVALID_ODOMETER"
                intOdometer = -1
            End If
        End If

        strTemp = Me.txtVehiclevalue.Text.Trim()
        If strTemp <> "" Then
            If Not Decimal.TryParse(strTemp, dVehiclevalue) Then
                hasErr = True
                Array.Resize(errMsg, errMsg.Length + 1)
                errMsg(errMsg.Length - 1) = "INVALID_VEHICLE_VALUE"
                dVehiclevalue = -1
            End If
        End If

        gEngineWarranty = Me.GetSelectedItem(Me.ddlEngineWarranty)

        If hasErr Then
            Me.ErrControllerMaster.AddErrorAndShow(errMsg)
            Throw New GUIException("", "")
        Else
            State.searchDV = VscCoverageRate.GetCoverageRateList(State.RateVersionID, State.VSCPlanID, gEngineWarranty, Me.txtClassCode.Text.Trim, intTermMon,
                                dDeductible, intOdometer, dVehiclevalue)
        End If


    End Sub

#End Region

#Region "Button event handler"
    Protected Sub btnBACK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBACK.Click
        Try
            Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back))
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            Me.State.PageIndex = 0
            Me.State.IsGridVisible = True
            Me.State.searchDV = Nothing
            Me.State.HasDataChanged = False
            Me.PopulateGrid()
        Catch ex As Exception
            Me.State.IsGridVisible = False
            ControlMgr.SetVisibleControl(Me, Grid, Me.State.IsGridVisible)
            ControlMgr.SetVisibleControl(Me, trPageSize, Me.State.IsGridVisible)
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub

    Protected Sub btnClearSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearSearch.Click
        Me.ddlEngineWarranty.SelectedIndex = 0
        Me.txtClassCode.Text = ""
        Me.txtDeductible.Text = ""
        Me.txtOdometer.Text = ""
        Me.txtTermMon.Text = ""
        Me.txtVehiclevalue.Text = ""
        Me.rdoAllocNew.Checked = True
        Me.rdoAllocUsed.Checked = False
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

    Private Sub Grid_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles Grid.PageIndexChanged
        Try
            Me.State.PageIndex = e.NewPageIndex
            Me.State.RateVersionID = Guid.Empty
            Me.PopulateGrid()
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


End Class