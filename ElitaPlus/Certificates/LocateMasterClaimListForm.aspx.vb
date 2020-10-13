Imports System.Diagnostics
Imports System.Threading

Partial Class LocateMasterClaimListForm
    Inherits ElitaPlusSearchPage

#Region " Web Form Designer Generated Code "

    Protected WithEvents ErrorCtrl As ErrorController
    Protected WithEvents Panel1 As Panel
    Protected WithEvents trPageSize As HtmlTableRow
    Protected WithEvents searchPanel As Panel
    Protected WithEvents allPanel As Panel



    'This call is required by the Web Form Designer.
    <DebuggerStepThrough> Private Sub InitializeComponent()

    End Sub

    Private Sub Page_Init(sender As Object, e As EventArgs) Handles MyBase.Init
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
            Me.ClaimedEquipment = _claimed_equipment
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

    Protected Shadows ReadOnly Property State As MyState
        Get
            'Return CType(MyBase.State, MyState)
            If Me.NavController.State Is Nothing Then
                Me.NavController.State = New MyState
                InitializeFromFlowSession()
                Me.State.NewClaim = CType(Me.NavController.FlowSession(FlowSessionKeys.SESSION_CLAIM), Claim)
            End If
            Return CType(Me.NavController.State, MyState)
        End Get
    End Property

    Protected Sub InitializeFromFlowSession()
        Me.State.inputParameters = CType(Me.NavController.ParametersPassed, Parameters)
    End Sub

    Private Sub Page_PageReturn(ReturnFromUrl As String, ReturnPar As Object) Handles MyBase.PageReturn
        Try
            Me.IsReturningFromChild = True
            Dim retObj As LocateServiceCenterDetailForm.ReturnType = CType(ReturnPar, LocateServiceCenterDetailForm.ReturnType)
            Select Case retObj.LastOperation
                Case DetailPageCommand.Back
            End Select
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Page_PageCall(CallFromUrl As String, CallingPar As Object) Handles MyBase.PageCall
        Try
            If Me.CallingParameters IsNot Nothing Then
                'Get the id from the parent
                Dim pageParameters As Parameters = CType(Me.CallingParameters, Parameters)
                Me.State.inputParameters = pageParameters
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try

    End Sub

#End Region

#Region "Properties"


#End Region

#Region "Page_Events"

    Private Sub UpdateBreadCrum()
        Me.MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage(PAGETAB)
        Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage(PAGETITLE)
        Me.MasterPage.UsePageTabTitleInBreadCrum = False
        Me.MasterPage.BreadCrum = TranslationBase.TranslateLabelOrMessage(PAGETAB) + ElitaBase.Sperator + TranslationBase.TranslateLabelOrMessage(PAGETITLE)
    End Sub

    Private Sub Page_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Me.MasterPage.MessageController.Clear()
        Try
            If Not Me.IsPostBack Then
                UpdateBreadCrum()
                Trace(Me, "CertItemCoverageId=" & GuidControl.GuidToHexString(Me.State.inputParameters.CertItemCoverageId))
                EnableDisableFields()
                Me.PopulateGrid()
                Me.SetGridItemStyleColor(Me.Grid)
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
        Me.ShowMissingTranslations(Me.MasterPage.MessageController)
    End Sub
#End Region

#Region "Controlling Logic"

    Private Sub PopulateGrid()
        Try
            Dim oContract As New Contract
            Dim objCertItemCoverage As New CertItemCoverage(Me.State.inputParameters.CertItemCoverageId)
            Dim objCert As New Certificate(objCertItemCoverage.CertId)
            oContract = Contract.GetContract(Me.State.inputParameters.DealerId, objCert.WarrantySalesDate.Value)
            If oContract IsNot Nothing Then
                If LookupListNew.GetCodeFromId(LookupListNew.LK_YESNO, oContract.AllowDifferentCoverage) = Codes.YESNO_Y Then
                    'ControlMgr.SetVisibleControl(Me, pnlUpfrontComm, True)
                    Me.State.allowdifferentcoverage = True
                End If
            End If
            Dim dv As Claim.MaterClaimDV = Claim.getList(Me.State.inputParameters.CertItemCoverageId, Me.State.allowdifferentcoverage, Me.State.inputParameters.masterClaimProcCode, Me.State.inputParameters.selDateofLoss)
            ControlMgr.SetVisibleControl(Me, Grid, True)

            If Me.State.inputParameters.masterClaimProcCode = Codes.MasterClmProc_BYDOL Then
                If dv.Count > 0 Then
                    ControlMgr.SetVisibleControl(Me, btnNew_WRITE, False)
                Else
                    ControlMgr.SetVisibleControl(Me, btnNew_WRITE, True)
                End If
            End If

            'ValidSearchResultCount(dv.Count, True)

            Me.Grid.AutoGenerateColumns = False

            'SetPageAndSelectedIndexFromGuid(dv, Me.State.SelectedServiceCenterId, Me.Grid, Me.State.PageIndex)
            Me.State.PageIndex = Me.Grid.CurrentPageIndex
            Me.Grid.DataSource = dv
            Me.Grid.DataBind()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub


    Sub EnableDisableFields()

        'ControlMgr.SetVisibleControl(Me, Grid, False)
        'ControlMgr.SetVisibleForControlFamily(Me, allPanel, showAllFields, True)

    End Sub
#End Region

#Region "Functions"

    Private Function LoadParameters() As LocateServiceCenterForm.Parameters

        Return New LocateServiceCenterForm.Parameters(Me.State.inputParameters.DealerId,
                                                      Me.State.inputParameters.ZipLocator,
                                                      Me.State.inputParameters.RiskTypeId,
                                                      Me.State.inputParameters.ManufacturerId,
                                                      Me.State.inputParameters.CovTypeCode,
                                                      Me.State.inputParameters.CertItemCoverageId,
                                                      Guid.Empty,
                                                      Me.State.inputParameters.ShowAcceptButton,
                                                      , , , Me.State.inputParameters.ClaimedEquipment)
    End Function

#End Region

#Region " Datagrid Related "

    'The Binding LOgic is here
    Private Sub ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles Grid.ItemDataBound
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

    Public Sub ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles Grid.ItemCommand
        Try
            If e.CommandName = "SelectAction" Then
                Dim itemType As ListItemType = CType(e.Item.ItemType, ListItemType)
                ' Dim dvRow As DataRowView = CType(e.Item.DataItem, DataRowView)

                If (itemType = ListItemType.Item OrElse itemType = ListItemType.AlternatingItem _
                    OrElse itemType = ListItemType.SelectedItem) Then
                    Me.NavController.FlowSession(FlowSessionKeys.SESSION_MSTR_CLAIM_NUMB) = CType(Me.Grid.Items(e.Item.ItemIndex).Cells(GRID_COL_MSTR_CLAIM_IDX).FindControl("lblMasterClaimNumber"), Label).Text
                    Me.NavController.FlowSession(FlowSessionKeys.SESSION_DATE_OF_LOSS) = CType(Me.Grid.Items(e.Item.ItemIndex).Cells(GRID_COL_DATE_OF_LOSS_IDX).FindControl("lblDateOfLoss"), Label).Text
                    Me.NavController.Navigate(Me, "locate_service_center", Me.LoadParameters)
                End If
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub

    Public Sub ItemCreated(sender As Object, e As DataGridItemEventArgs) Handles Grid.ItemCreated
        BaseItemCreated(sender, e)
    End Sub

    Private Sub Grid_PageIndexChanged(source As Object, e As DataGridPageChangedEventArgs) Handles Grid.PageIndexChanged
        Try
            Me.State.PageIndex = e.NewPageIndex
            Me.State.SelectedServiceCenterId = Guid.Empty
            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub
#End Region

#Region " Buttons Clicks "

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Try
            Me.NavController.Navigate(Me, "back")
        Catch ex As ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnNew_WRITE_Click(sender As Object, e As EventArgs) Handles btnNew_WRITE.Click
        Try
            Me.NavController.Navigate(Me, "locate_service_center", Me.LoadParameters)
        Catch ex As ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

#End Region

#Region "Error Handling"
#End Region

End Class

