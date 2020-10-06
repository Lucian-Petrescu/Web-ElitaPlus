Partial Class LocateMasterClaimListForm
    Inherits ElitaPlusSearchPage

#Region " Web Form Designer Generated Code "

    Protected WithEvents ErrorCtrl As ErrorController
    Protected WithEvents Panel1 As System.Web.UI.WebControls.Panel
    Protected WithEvents trPageSize As System.Web.UI.HtmlControls.HtmlTableRow
    Protected WithEvents searchPanel As System.Web.UI.WebControls.Panel
    Protected WithEvents allPanel As System.Web.UI.WebControls.Panel



    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    Private Sub Page_Init(sender As System.Object, e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Constants"
    Public Const URL As String = "~\Certificates\LocateServiceCenterForm.aspx"
    Public Const PAGETITLE As String = "SELECT_MASTER_CLAIM_NUMBER"
    Public Const PAGETAB As String = "CERTIFICATE"

    Public Const GRID_COL_EDIT_IDX As Integer = 0
    Public Const GRID_COL_MSTR_CLAIM_IDX As Integer = 1
    Public Const GRID_COL_CLAIM_NUMBER_IDX As Integer = 2
    Public Const GRID_COL_DATE_OF_LOSS_IDX As Integer = 3
    Public Const GRID_COL_SERVICE_CNTER_IDX As Integer = 4
    Public Const GRID_COL_ID_IDX As Integer = 5

#End Region

#Region "Page Parameters"
    Public Class Parameters
        Public DealerId As Guid
        Public ZipLocator As String
        Public RiskTypeId As Guid
        Public ManufacturerId As Guid
        Public ShowAcceptButton As Boolean = True
        Public CovTypeCode As String
        Public CertItemCoverageId As Guid
        Public WhenAcceptGoToCreateClaim As Boolean
        Public ClaimedEquipment As ClaimEquipment
        Public masterClaimProcCode As String
        Public selDateofLoss As Date

        Public Sub New(dealerId As Guid, zipLocator As String,
                        riskTypeId As Guid, ManufacturerId As Guid,
                        covTypeCode As String,
                        certItemCoverageId As Guid,
                        Optional ByVal showAcceptButton As Boolean = True,
                        Optional ByVal whenAcceptGoToCreateClaim As Boolean = True,
                        Optional ByVal _claimed_equipment As ClaimEquipment = Nothing,
                        Optional ByVal masterClaimProcCode As String = Codes.MasterClmProc_NONE,
                        Optional ByVal selDateofLoss As Date = Nothing)
            Me.DealerId = dealerId
            Me.ZipLocator = zipLocator
            Me.RiskTypeId = riskTypeId
            Me.ManufacturerId = ManufacturerId
            Me.CovTypeCode = covTypeCode
            Me.ShowAcceptButton = showAcceptButton
            Me.WhenAcceptGoToCreateClaim = whenAcceptGoToCreateClaim
            Me.CertItemCoverageId = certItemCoverageId
            ClaimedEquipment = _claimed_equipment
            Me.masterClaimProcCode = masterClaimProcCode
            Me.selDateofLoss = selDateofLoss
        End Sub
    End Class
#End Region

#Region "Page Return Type"
    'Same as Detail Page Return Parameters 

#End Region

#Region "Page State"
    Private IsReturningFromChild As Boolean = False

    Class MyState
        Public PageIndex As Integer = 0
        Public NewClaim As Claim
        Public SelectedServiceCenterId As Guid = Guid.Empty
        Public inputParameters As Parameters
        Public allowdifferentcoverage As Boolean = False
    End Class

    Public Sub New()
        MyBase.New(New MyState)
    End Sub

    Protected Shadows ReadOnly Property State() As MyState
        Get
            'Return CType(MyBase.State, MyState)
            If NavController.State Is Nothing Then
                NavController.State = New MyState
                InitializeFromFlowSession()
                Me.State.NewClaim = CType(NavController.FlowSession(FlowSessionKeys.SESSION_CLAIM), Claim)
            End If
            Return CType(NavController.State, MyState)
        End Get
    End Property

    Protected Sub InitializeFromFlowSession()
        State.inputParameters = CType(NavController.ParametersPassed, Parameters)
    End Sub

    Private Sub Page_PageReturn(ReturnFromUrl As String, ReturnPar As Object) Handles MyBase.PageReturn
        Try
            IsReturningFromChild = True
            Dim retObj As LocateServiceCenterDetailForm.ReturnType = CType(ReturnPar, LocateServiceCenterDetailForm.ReturnType)
            Select Case retObj.LastOperation
                Case ElitaPlusPage.DetailPageCommand.Back
            End Select
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Page_PageCall(CallFromUrl As String, CallingPar As Object) Handles MyBase.PageCall
        Try
            If CallingParameters IsNot Nothing Then
                'Get the id from the parent
                Dim pageParameters As Parameters = CType(CallingParameters, Parameters)
                State.inputParameters = pageParameters
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try

    End Sub

#End Region

#Region "Properties"


#End Region

#Region "Page_Events"

    Private Sub UpdateBreadCrum()
        MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage(PAGETAB)
        MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage(PAGETITLE)
        MasterPage.UsePageTabTitleInBreadCrum = False
        MasterPage.BreadCrum = TranslationBase.TranslateLabelOrMessage(PAGETAB) + ElitaBase.Sperator + TranslationBase.TranslateLabelOrMessage(PAGETITLE)
    End Sub

    Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        MasterPage.MessageController.Clear()
        Try
            If Not IsPostBack Then
                UpdateBreadCrum()
                Trace(Me, "CertItemCoverageId=" & GuidControl.GuidToHexString(State.inputParameters.CertItemCoverageId))
                EnableDisableFields()
                PopulateGrid()
                SetGridItemStyleColor(Grid)
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
        ShowMissingTranslations(MasterPage.MessageController)
    End Sub
#End Region

#Region "Controlling Logic"

    Private Sub PopulateGrid()
        Try
            Dim oContract As New Contract
            Dim objCertItemCoverage As New CertItemCoverage(State.inputParameters.CertItemCoverageId)
            Dim objCert As New Certificate(objCertItemCoverage.CertId)
            oContract = Contract.GetContract(State.inputParameters.DealerId, objCert.WarrantySalesDate.Value)
            If oContract IsNot Nothing Then
                If LookupListNew.GetCodeFromId(LookupListNew.LK_YESNO, oContract.AllowDifferentCoverage) = Codes.YESNO_Y Then
                    'ControlMgr.SetVisibleControl(Me, pnlUpfrontComm, True)
                    State.allowdifferentcoverage = True
                End If
            End If
            Dim dv As Claim.MaterClaimDV = Claim.getList(State.inputParameters.CertItemCoverageId, State.allowdifferentcoverage, State.inputParameters.masterClaimProcCode, State.inputParameters.selDateofLoss)
            ControlMgr.SetVisibleControl(Me, Grid, True)

            If State.inputParameters.masterClaimProcCode = Codes.MasterClmProc_BYDOL Then
                If dv.Count > 0 Then
                    ControlMgr.SetVisibleControl(Me, btnNew_WRITE, False)
                Else
                    ControlMgr.SetVisibleControl(Me, btnNew_WRITE, True)
                End If
            End If

            'ValidSearchResultCount(dv.Count, True)

            Grid.AutoGenerateColumns = False

            'SetPageAndSelectedIndexFromGuid(dv, Me.State.SelectedServiceCenterId, Me.Grid, Me.State.PageIndex)
            State.PageIndex = Grid.CurrentPageIndex
            Grid.DataSource = dv
            Grid.DataBind()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub


    Sub EnableDisableFields()

        'ControlMgr.SetVisibleControl(Me, Grid, False)
        'ControlMgr.SetVisibleForControlFamily(Me, allPanel, showAllFields, True)

    End Sub
#End Region

#Region "Functions"

    Private Function LoadParameters() As LocateServiceCenterForm.Parameters

        Return New LocateServiceCenterForm.Parameters(State.inputParameters.DealerId,
                                                      State.inputParameters.ZipLocator,
                                                      State.inputParameters.RiskTypeId,
                                                      State.inputParameters.ManufacturerId,
                                                      State.inputParameters.CovTypeCode,
                                                      State.inputParameters.CertItemCoverageId,
                                                      Guid.Empty,
                                                      State.inputParameters.ShowAcceptButton,
                                                      , , , State.inputParameters.ClaimedEquipment)
    End Function

#End Region

#Region " Datagrid Related "

    'The Binding LOgic is here
    Private Sub ItemDataBound(sender As System.Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles Grid.ItemDataBound
        'Try
        '    Dim itemType As ListItemType = CType(e.Item.ItemType, ListItemType)
        '    Dim dvRow As DataRowView = CType(e.Item.DataItem, DataRowView)

        '    If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then
        '        If Not dvRow(Claim.MaterClaimDV.COL_NAME_MASTER_CLAIM_NUMBER) Is DBNull.Value Then
        '            e.Item.Cells(Me.GRID_COL_MSTR_CLAIM_IDX).Text = CType(dvRow(Claim.MaterClaimDV.COL_NAME_MASTER_CLAIM_NUMBER), String)
        '        End If
        '        If Not dvRow(Claim.MaterClaimDV.COL_NAME_CLAIM_NUMBER) Is DBNull.Value Then
        '            e.Item.Cells(Me.GRID_COL_CLAIM_NUMBER_IDX).Text = CType(dvRow(Claim.MaterClaimDV.COL_NAME_CLAIM_NUMBER), String)
        '        End If
        '        If Not dvRow(Claim.MaterClaimDV.COL_NAME_LOSS_DATE) Is DBNull.Value Then
        '            e.Item.Cells(Me.GRID_COL_DATE_OF_LOSS_IDX).Text = Me.GetDateFormattedString(CType(dvRow(Claim.MaterClaimDV.COL_NAME_LOSS_DATE), Date))
        '        End If
        '        If Not dvRow(Claim.MaterClaimDV.COL_SERVICE_CENTER) Is DBNull.Value Then
        '            e.Item.Cells(Me.GRID_COL_SERVICE_CNTER_IDX).Text = CType(dvRow(Claim.MaterClaimDV.COL_SERVICE_CENTER), String)
        '        End If

        '        'GRID_COL_SERVICE_CNTER_IDX
        '        e.Item.Cells(Me.GRID_COL_ID_IDX).Text = GetGuidStringFromByteArray(CType(dvRow(Claim.MaterClaimDV.COL_NAME_CLAIM_ID), Byte()))
        '    End If
        'Catch ex As Exception
        '    Me.HandleErrors(ex, Me.MasterPage.MessageController)
        'End Try
    End Sub

    Public Sub ItemCommand(source As System.Object, e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles Grid.ItemCommand
        Try
            If e.CommandName = "SelectAction" Then
                Dim itemType As ListItemType = CType(e.Item.ItemType, ListItemType)
                ' Dim dvRow As DataRowView = CType(e.Item.DataItem, DataRowView)

                If (itemType = ListItemType.Item OrElse itemType = ListItemType.AlternatingItem _
                    OrElse itemType = ListItemType.SelectedItem) Then
                    NavController.FlowSession(FlowSessionKeys.SESSION_MSTR_CLAIM_NUMB) = CType(Grid.Items(e.Item.ItemIndex).Cells(GRID_COL_MSTR_CLAIM_IDX).FindControl("lblMasterClaimNumber"), Label).Text
                    NavController.FlowSession(FlowSessionKeys.SESSION_DATE_OF_LOSS) = CType(Grid.Items(e.Item.ItemIndex).Cells(GRID_COL_DATE_OF_LOSS_IDX).FindControl("lblDateOfLoss"), Label).Text
                    NavController.Navigate(Me, "locate_service_center", LoadParameters)
                End If
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub

    Public Sub ItemCreated(sender As System.Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles Grid.ItemCreated
        BaseItemCreated(sender, e)
    End Sub

    Private Sub Grid_PageIndexChanged(source As System.Object, e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles Grid.PageIndexChanged
        Try
            State.PageIndex = e.NewPageIndex
            State.SelectedServiceCenterId = Guid.Empty
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
#End Region

#Region " Buttons Clicks "

    Private Sub btnBack_Click(sender As System.Object, e As System.EventArgs) Handles btnBack.Click
        Try
            NavController.Navigate(Me, "back")
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnNew_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnNew_WRITE.Click
        Try
            NavController.Navigate(Me, "locate_service_center", LoadParameters)
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

#End Region

#Region "Error Handling"
#End Region

End Class

