Option Strict On
Option Explicit On
Imports System.Collections.Generic
Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Imports Assurant.ElitaPlus.Security
Imports Assurant.ElitaPlus.DALObjects
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports Assurant.Elita.Web.Forms
Imports System.Threading
Partial Class ClaimAuthDetailForm
    Inherits ElitaPlusSearchPage

    Protected WithEvents ErrController As ErrorController

    Private Class PageStatus

        Public Sub New()
            pageIndex = 0
            pageCount = 0
        End Sub

    End Class

#Region "Member Variables"

    Private Shared pageIndex As Integer
    Private Shared pageCount As Integer
    Private Shared PAY_CLAIM_FORM_URL As String = ELPWebConstants.APPLICATION_PATH & "/Claims/PayClaimForm.aspx"
#End Region

#Region "Properties"

    Public ReadOnly Property IsEditing() As Boolean
        Get
            IsEditing = (Me.Grid.EditIndex > NO_ROW_SELECTED_INDEX)
        End Get
    End Property


    Public ReadOnly Property IsGridInEditMode() As Boolean
        Get
            Return Me.Grid.EditIndex > Me.NO_ITEM_SELECTED_INDEX
        End Get
    End Property
#End Region

#Region "Page State"

    'DEF-17426
    Class BaseState
        Public NavCtrl As INavigationController
    End Class

    Class MyState
        Public PageIndex As Integer = 0
        Public PartsInfoBOs As ArrayList
        Public PartsInfoBOIDs As ArrayList
        Public PartsInfoDV As DataView
        Public OreginalPartsInfoTable As DataTable
        Public OreginalPartsInfoDataViewFilter As String = String.Empty
        'Public PartsInfoBO As PartsInfo
        Public ClaimAuthDetailBO As ClaimAuthDetail
        Public OriginalClaimAuthDetailBO As ClaimAuthDetail
        Public PartsAdded As Boolean = False
        Public ClaimBO As Claim
        Public CompanyId As Guid
        Public PartsInfoID As Guid
        Public PartsInfoIDs As ArrayList
        Public RiskGroupID As Guid
        Public RiskGroup As String
        Public IsAfterSave As Boolean
        Public IsEditMode_PI As Boolean
        Public IsEditMode_AD As Boolean
        Public IsEditable_AD As Boolean
        Public IsNewMode_PI As Boolean
        Public IsNewMode_AD As Boolean
        Public IsDone As Boolean
        Public IsViewOnly As Boolean
        Public LastOperation As String = String.Empty
        Public LastRowIndexInUse As Integer
        Public ActionInProgress As DetailPageCommand = DetailPageCommand.Nothing_
        Public HasDataChanged As Boolean
        Public HasclaimStatusBOChanged As Boolean
        Public HasclaimStatusBOSaved As Boolean
        Public LastErrMsg As String
        Public IsReplacementClaim As Boolean
        Public NoClaimAuthDetailBOFound As Boolean
        Public IsCalledByPayClaimForm As Boolean
        Public HasDataBeenSaved As Boolean
        Public ClaimStatusBO As ClaimStatus
        Public NewClaimStatusBO As ClaimStatus
        Public SortExpression As String = Assurant.ElitaPlus.BusinessObjectsNew.PartsInfo.PartsInfoDV.COL_NAME_PARTS_DESCRIPTION
        Public bnoRow As Boolean = False
        Public PartsDescriptionAvailable As Boolean = False
        Public ClaimTaxRatesData As ClaimInvoiceDAL.ClaimTaxRatesData
        Public TotalTaxAmount As Decimal
        Public ClaimMethodOfRepair As String

    End Class

    'DEF-17426
    'Public Sub New()
    '    MyBase.New(New MyState)
    'End Sub

    'Protected Shadows ReadOnly Property State() As MyState
    '    Get
    '        Return CType(MyBase.State, MyState)
    '    End Get
    'End Property

    Public Sub New()
        MyBase.New(New BaseState)
    End Sub

    Protected Shadows ReadOnly Property State() As MyState
        Get
            If Me.NavController.State Is Nothing Then
                Me.NavController.State = New MyState
            End If
            Return CType(Me.NavController.State, MyState)
        End Get
    End Property

#End Region

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

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
    Public Const URL As String = "ClaimAuthDetailForm.aspx"

    Private Const PART_DESCRIPTION_COL As Integer = 1
    Private Const ID_COL As Integer = 2
    Private Const DESCRIPTION_COL As Integer = 3
    Private Const IN_STOCK_ID_COL As Integer = 4
    Private Const COST_COL As Integer = 5
    Private Const VIEW_COST_COL As Integer = 3

    Private Const DESCRIPTION_CONTROL_NAME As String = "DescriptionDropDownList"
    Private Const IN_STOCK_ID_CONTROL_NAME As String = "InStockDropDownList"
    Private Const COST_CONTROL_NAME As String = "CostTextBox"
    Private Const ID_CONTROL_NAME As String = "IdLabel"
    Private Const EDITBUTTON_WRITE_CONTROL_NAME As String = "EditButton_WRITE"
    Private Const DELETEBUTTON_WRITE_CONTROL_NAME As String = "DeleteButton_WRITE"

    Private Const EDIT_COMMAND As String = "SelectAction"
    Private Const DELETE_COMMAND As String = "DeleteRecord"
    Private Const NEW_COMMAND As String = "NewRecord"
    Private Const SORT_COMMAND As String = "Sort"

    Private Const NO_ROW_SELECTED_INDEX As Integer = -1
    Private Const AUTH_DETAIL_TAB As Integer = 1
    Private Const PARTS_INFO_TAB As Integer = 0

    Private Const LABEL_OTHER_AMOUNT As String = "OTHER_AMOUNT"
    Private Const YESNO As String = "YESNO"

    Private Const DV_ID_COL As Integer = 0


#End Region

#Region "Tabs"
    Public Const Tab_PartsInfo As String = "0"
    Public Const Tab_AuthDetail As String = "1"

    Dim DisabledTabsList As New List(Of String)()
#End Region
#Region "Page Return Type"
    Public Class ReturnType
        Public ClaimAuthDetailBO As ClaimAuthDetail
        Public ClaimBO As Claim
        Public PartsInfoDV As DataView
        Public HasDataChanged As Boolean = False
        Public HasClaimStatusBOChanged As Boolean = False
        Public Sub New(ByVal ClaimBO As Claim, ByVal ClaimAuthDetailBO As ClaimAuthDetail, ByVal PartsInfoDV As DataView, ByVal hasDataChanged As Boolean, ByVal hasClaimStatusBOChanged As Boolean)
            Me.ClaimBO = ClaimBO
            Me.ClaimAuthDetailBO = ClaimAuthDetailBO
            Me.PartsInfoDV = PartsInfoDV
            Me.HasDataChanged = hasDataChanged
            Me.HasClaimStatusBOChanged = hasClaimStatusBOChanged
        End Sub
    End Class
#End Region

#Region "Page Parameters"
    Public Class Parameters
        Public ClaimAuthDetailBO As ClaimAuthDetail
        Public ClaimBO As Claim
        Public PartsInfoDV As DataView

        Public Sub New(ByVal ClaimBO As Claim, ByVal ClaimAuthDetailBO As ClaimAuthDetail, ByVal PartsInfoDV As DataView)
            Me.ClaimBO = ClaimBO
            Me.ClaimAuthDetailBO = ClaimAuthDetailBO
            Me.PartsInfoDV = PartsInfoDV
        End Sub
    End Class
#End Region

#Region "Private Methods"
    Private Sub Page_PageCall(ByVal CallFromUrl As String, ByVal CallingPar As Object) Handles MyBase.PageCall
        Try

            If Not Me.CallingParameters Is Nothing Then

                If CallFromUrl.Equals(Me.PAY_CLAIM_FORM_URL) Then
                    Me.State.ClaimBO = CType(Me.CallingParameters, Claim)
                Else
                    Dim objParam As Parameters
                    objParam = CType(Me.CallingParameters, Parameters)
                    Me.State.ClaimBO = objParam.ClaimBO
                    Me.State.ClaimAuthDetailBO = objParam.ClaimAuthDetailBO
                    Me.State.PartsInfoDV = objParam.PartsInfoDV

                End If

                If Me.State.ClaimBO.MethodOfRepairCode = Codes.METHOD_OF_REPAIR_REPLACEMENT Or Me.State.ClaimBO.MethodOfRepairCode = Codes.METHOD_OF_REPAIR__REPLACEMENT Then
                    Me.State.ClaimMethodOfRepair = "RPL"
                    Me.LabelTaxType.Text = TranslationBase.TranslateLabelOrMessage("REPLACEMENT")
                Else
                    Me.State.ClaimMethodOfRepair = "RPR"
                    Me.LabelTaxType.Text = TranslationBase.TranslateLabelOrMessage("REPAIR")
                End If

                Me.SetStateProperties()

                'If Me.State.ClaimBO.AuthDetailUsage = PayClaimForm.URL Then 'DEF-17426
                If Me.NavController.Context = "PAY_CLAIM_DETAIL-AUTH_DETAIL" Or Me.NavController.Context = "PAY_CLAIM_ADJUSTMENT-PAY_CLAIM_DETAIL_CLADJ-AUTH_DETAIL" Then
                    Me.State.IsCalledByPayClaimForm = True
                Else
                    Me.State.IsCalledByPayClaimForm = False
                End If

                If Me.State.ClaimBO.StatusCode = Codes.CLAIM_STATUS__CLOSED Then
                    Me.State.IsViewOnly = True
                Else
                    Me.State.IsViewOnly = False
                End If

            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrController)
        End Try

    End Sub

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'Put user code to initialize the page here
        ErrController.Clear_Hide()

        Try
            txtLabor.Attributes.Add("onchange", "doAmtCalc(this, 'txtLabor');")
            txtParts.Attributes.Add("onchange", "doAmtCalc(this, 'txtParts');")
            txtTripAmt.Attributes.Add("onchange", "doAmtCalc(this, 'txtTripAmt');")
            txtShippingAmt.Attributes.Add("onchange", "doAmtCalc(this, 'txtShippingAmt');")
            txtServiceCharge.Attributes.Add("onchange", "doAmtCalc(this, 'txtServiceCharge');")
            txtDiagnostics.Attributes.Add("onchange", "doAmtCalc(this, 'txtDiagnostics');")
            txtDisposition.Attributes.Add("onchange", "doAmtCalc(this, 'txtDisposition');")
            txtOtherAmt.Attributes.Add("onchange", "doAmtCalc(this, 'txtOtherAmt');")
            txtTotal.Attributes.Add("onchange", "doAmtCalc(this, 'txtTotal');")

            txtLabor.Attributes.Add("onfocus", "select( );")
            txtParts.Attributes.Add("onfocus", "select( );")
            txtTripAmt.Attributes.Add("onfocus", "select( );")
            txtShippingAmt.Attributes.Add("onfocus", "select( );")
            txtServiceCharge.Attributes.Add("onfocus", "select( );")
            txtDiagnostics.Attributes.Add("onfocus", "select( );")
            txtDisposition.Attributes.Add("onfocus", "select( );")
            txtOtherAmt.Attributes.Add("onfocus", "select( );")
            txtTotal.Attributes.Add("onfocus", "select( );")
            SetDecimalSeparatorSymbol()

            If Not Page.IsPostBack Then
                CheckifComingFromBackEndClaim() 'DEF-17426
                'Me.SortDirection = Me.State.SortExpression 'DEF-17426
                Me.MenuEnabled = False
                Me.TextboxClaimNumber.Text = Me.State.ClaimBO.ClaimNumber
                Me.TextboxCustomerName.Text = Me.State.ClaimBO.CustomerName
                Me.TextboxRiskGroup.Text = Me.State.RiskGroup
                Me.SetGridItemStyleColor(Grid)
                Me.ShowMissingTranslations(ErrController)
                Me.State.PageIndex = 0
                Me.State.ClaimStatusBO = ClaimStatus.GetLatestClaimStatus(Me.State.ClaimBO.Id)
                PopulateGrid()
                CheckIfPartsDescriptionAvailable()
                Me.TranslateGridHeader(Me.Grid)
                Me.TranslateGridControls(Me.Grid)
                PopulateClaimAuthDetailTab()
                SetButtonsState(False)
                If Me.State.ClaimBO.ClaimActivityCode = Codes.CLAIM_ACTIVITY__REPLACED Or
                    Me.State.ClaimBO.ClaimActivityCode = Codes.CLAIM_ACTIVITY__PENDING_REPLACEMENT Then
                    Me.State.IsReplacementClaim = True
                    DisabledTabsList.Add(Tab_PartsInfo)
                    hdnSelectedTab.Value = Tab_AuthDetail
                End If
            Else
                If Not Me.State Is Nothing AndAlso Me.State.ClaimStatusBO Is Nothing Then
                    Me.State.ClaimStatusBO = ClaimStatus.GetLatestClaimStatus(Me.State.ClaimBO.Id)
                End If
                GetDisabledTabs()
            End If

            'Me.SetStateProperties() 'DEF-17426
            If Not Me.State.PartsInfoBOs Is Nothing AndAlso Me.State.PartsInfoBOs.Count > 0 Then
                For i As Integer = 0 To Me.State.PartsInfoBOs.Count - 1
                    BindBoPropertiesToGridHeaders(CType(Me.State.PartsInfoBOs(i), PartsInfo))
                Next
            End If

            BindADBoPropertiesToLabels()

            'If Me.State.IsCalledByPayClaimForm Then 'DEF-17426
            If Me.NavController.Context = "PAY_CLAIM_DETAIL-AUTH_DETAIL" Or Me.NavController.Context = "PAY_CLAIM_ADJUSTMENT-PAY_CLAIM_DETAIL_CLADJ-AUTH_DETAIL" Then
                CheckIfComingFromSaveConfirm()
            End If

        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrController)
        End Try
    End Sub

    Private Sub CheckIfPartsDescriptionAvailable()
        Dim dv As DataView = PartsInfo.getAvailList(Me.State.RiskGroupID, Me.State.ClaimBO.Id)
        If Not dv.Count > 0 Then
            Me.State.PartsDescriptionAvailable = True
        End If

    End Sub

    Private Sub GetDisabledTabs()
        Dim DisabledTabs As Array = hdnDisabledTab.Value.Split(",".ToCharArray)
        For Each tab As String In DisabledTabs
            If tab IsNot String.Empty Then
                DisabledTabsList.Add(tab)
                hdnDisabledTab.Value = String.Empty
            End If
        Next
    End Sub


    Private Sub CheckifComingFromBackEndClaim()
        If Me.NavController Is Nothing Then
            Exit Sub
        End If
        If Me.NavController.CurrentNavState.Name = "AUTH_DETAIL" Then
            Dim objParam As Parameters
            objParam = CType(Me.NavController.ParametersPassed, Parameters)
            If Not objParam.ClaimBO Is Nothing Then
                Me.State.ClaimBO = objParam.ClaimBO
                SetStateProperties()
                If Me.State.ClaimBO.MethodOfRepairCode = Codes.METHOD_OF_REPAIR_REPLACEMENT Or Me.State.ClaimBO.MethodOfRepairCode = Codes.METHOD_OF_REPAIR__REPLACEMENT Then
                    Me.State.ClaimMethodOfRepair = "RPL"
                    Me.LabelTaxType.Text = TranslationBase.TranslateLabelOrMessage("REPLACEMENT")
                Else
                    Me.State.ClaimMethodOfRepair = "RPR"
                    Me.LabelTaxType.Text = TranslationBase.TranslateLabelOrMessage("REPAIR")
                End If
            End If
        End If
    End Sub

    Protected Sub BindADBoPropertiesToLabels()

        Me.BindBOPropertyToLabel(Me.State.ClaimAuthDetailBO, "LaborAmount", Me.LabelLabor)
        Me.BindBOPropertyToLabel(Me.State.ClaimAuthDetailBO, "PartAmount", Me.LabelParts)
        Me.BindBOPropertyToLabel(Me.State.ClaimAuthDetailBO, "ServiceCharge", Me.LabelSvcCharge)
        Me.BindBOPropertyToLabel(Me.State.ClaimAuthDetailBO, "TripAmount", Me.LabelTripAmount)
        Me.BindBOPropertyToLabel(Me.State.ClaimAuthDetailBO, "ShippingAmount", Me.LabelShippingAmount)
        Me.BindBOPropertyToLabel(Me.State.ClaimAuthDetailBO, "DiagnosticsAmount", Me.LabelDiagnostics)
        Me.BindBOPropertyToLabel(Me.State.ClaimAuthDetailBO, "DispositionAmount", Me.LabelDisposition)
        Dim LabelOtherDesc As New Label
        LabelOtherDesc.Text = TranslationBase.TranslateLabelOrMessage(LABEL_OTHER_AMOUNT)
        Me.BindBOPropertyToLabel(Me.State.ClaimAuthDetailBO, "OtherAmount", LabelOtherDesc)
        Me.ClearGridViewHeadersAndLabelsErrSign()
    End Sub

    Protected Sub DisableControl(ByVal ctl As System.Web.UI.Control, Optional ByVal invisible As Boolean = False, Optional ByVal isShippingField As Boolean = False)
        'If the service center of the claim is integrated with GVS and also the claim status must be one of the following, 
        ' then the parts info tab is editable:
        ' Waiting on Budget Approval - COD
        ' Exception: If Status is “Waiting Documentation”, Elita will allow the Claim Adjuster to edit only “Shipping” field.

        If IsIntegratedWithGVS() Then
            If Not isEditableForGVS(isShippingField) Then
                If invisible Then
                    ControlMgr.SetVisibleControl(Me, CType(ctl, System.Web.UI.WebControls.WebControl), False)
                Else
                    ControlMgr.SetEnableControl(Me, CType(ctl, System.Web.UI.WebControls.WebControl), False)
                End If
            End If
        End If
    End Sub

    Public Function isEditableForGVS(Optional ByVal isShippingField As Boolean = False) As Boolean
        'If the service center of the claim is integrated with GVS and also the claim status must be one of the following, 
        ' then the auth detail tab is editable:
        ' Waiting on Budget Approval - COD
        ' Exception: If Status is “Waiting Documentation”, Elita will allow the Claim Adjuster to edit only “Shipping” field.
        Dim retVal As Boolean = True

        If IsIntegratedWithGVS() Then
            If (Not Me.State.ClaimStatusBO Is Nothing AndAlso (Me.State.ClaimStatusBO.StatusCode = Codes.CLAIM_EXTENDED_STATUS__WAITING_ON_BUDGET_APPROVAL)) Then
                retVal = True
            ElseIf (Not Me.State.ClaimStatusBO Is Nothing AndAlso Me.State.ClaimStatusBO.StatusCode = Codes.CLAIM_EXTENDED_STATUS__WAITING_DOCUMENTATION _
                    AndAlso ElitaPlusPrincipal.Current.IsInRole(Codes.USER_ROLE__OFFICE_MANAGER)) Then
                If isShippingField Then
                    retVal = True
                Else
                    retVal = False
                End If
            Else
                retVal = False
            End If
        End If

        Return retVal
    End Function

    Private Sub PopulateGrid()

        Dim dv As DataView
        Try
            'Refresh the DataView and Call SetPageAndSelectedIndexFromGuid() to go to the Page 
            'where the most recently saved Record exists in the DataView
            dv = GetDV()
            If (Me.State.IsAfterSave) Then
                Me.State.IsAfterSave = False
                Me.SetPageAndSelectedIndexFromGuid(dv, Me.State.PartsInfoID, Me.Grid, Me.State.PageIndex)
            ElseIf (Me.State.IsEditMode_PI) Then
                Me.SetPageAndSelectedIndexFromGuid(dv, Me.State.PartsInfoID, Me.Grid, Me.State.PageIndex, Me.State.IsEditMode_PI)
            Else
                Me.SetPageAndSelectedIndexFromGuid(dv, Guid.Empty, Me.Grid, Me.State.PageIndex)
            End If

            Me.TranslateGridControls(Grid)

            Grid.DataSource = dv
            Me.Grid.DataBind()

            Me.State.PartsAdded = (dv.Count > 0)

            'Me.PopulateControlFromBOProperty(Me.TextTotalCost, PartsInfo.getTotalCost(Me.State.ClaimBO.Id).Value)
            Dim totalpartsCost As DecimalType = Me.GetTotalCost()
            Me.PopulateControlFromBOProperty(Me.TextTotalCost, totalpartsCost)
            If totalpartsCost.Value > 0 Then
                Me.PopulateControlFromBOProperty(Me.TextPartsTax1, ComputePartsTaxes(totalpartsCost.Value))
            Else
                Me.PopulateControlFromBOProperty(Me.TextPartsTax1, "0.00")
            End If


            'CreatePartsInfoArrayList(dv)

            If State.PartsInfoDV.Count = 0 Then
                Dim dt As DataTable = State.PartsInfoDV.Table.Clone()
                Dim dvEmpty As New PartsInfo.PartsInfoDV(dt)
                Dim objEmpty As PartsInfo = New PartsInfo
                PartsInfo.AddNewRowToPartsInfoSearchDV(dvEmpty, objEmpty)
                SetPageAndSelectedIndexFromGuid(dvEmpty, Me.State.PartsInfoID, Me.Grid, Me.State.PageIndex, Me.State.IsEditMode_PI)
                SortAndBindGrid(dvEmpty, True)
            Else
                SetPageAndSelectedIndexFromGuid(Me.State.PartsInfoDV, Me.State.PartsInfoID, Me.Grid, Me.State.PageIndex, Me.State.IsEditMode_PI)
                SortAndBindGrid(State.PartsInfoDV)
            End If

            If State.PartsInfoDV.Count = 0 Then
                For Each gvRow As GridViewRow In Grid.Rows
                    gvRow.Visible = False
                    gvRow.Controls.Clear()
                Next
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrController)
        End Try

    End Sub

    Private Sub SortAndBindGrid(ByVal dvBinding As DataView, Optional ByVal blnEmptyList As Boolean = False)
        Me.Grid.DataSource = dvBinding
        HighLightSortColumn(Grid, Me.State.SortExpression)
        Me.Grid.DataBind()
        If Not Grid.BottomPagerRow.Visible Then Grid.BottomPagerRow.Visible = True
        'ControlMgr.SetVisibleControl(Me, Grid, Me.State.IsGridVisible)
        'ControlMgr.SetVisibleControl(Me, trPageSize, Me.Grid.Visible)

        'If Me.State.PartsInfoDV.Count > 0 Then
        '    If Me.Grid.Visible Then
        '        Me.lblRecordCount.Text = Me.State.PartsInfoDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
        '    End If
        'Else
        '    If Me.Grid.Visible Then
        '        Me.lblRecordCount.Text = Me.State.PartsInfoDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
        '    End If
        'End If

        If blnEmptyList Then
            For Each gvRow As GridViewRow In Grid.Rows
                'gvRow.Visible = False
                gvRow.Controls.Clear()
            Next
        End If
    End Sub

    Private Function GetTotalCost() As DecimalType
        Dim index As Integer
        Dim totalCost As Decimal = 0
        For index = 0 To Me.State.PartsInfoDV.Count - 1
            totalCost = totalCost + CType(Me.State.PartsInfoDV.Item(index).Item(Me.VIEW_COST_COL), Decimal)
        Next
        Return New DecimalType(totalCost)
    End Function

    Private Function GetSubTotal() As Decimal
        Dim subTotal As Decimal = 0
        If Not Me.State.ClaimAuthDetailBO Is Nothing Then
            With Me.State.ClaimAuthDetailBO
                If Not .LaborAmount Is Nothing Then subTotal += .LaborAmount.Value
                If Not .PartAmount Is Nothing Then subTotal += .PartAmount.Value
                If Not .ServiceCharge Is Nothing Then subTotal += .ServiceCharge.Value
                If Not .TripAmount Is Nothing Then subTotal += .TripAmount.Value
                If Not .ShippingAmount Is Nothing Then subTotal += .ShippingAmount.Value
                If Not .OtherAmount Is Nothing Then subTotal += .OtherAmount.Value
                If Not .DiagnosticsAmount Is Nothing Then subTotal += .DiagnosticsAmount.Value
                If Not .DispositionAmount Is Nothing Then subTotal += .DispositionAmount.Value
            End With
        End If
        Return subTotal
    End Function

    Private Sub PopulateClaimAuthDetailTab()
        ' The record may/may not exist.
        Try
            If Me.State.ClaimAuthDetailBO Is Nothing Then
                'If Me.State.IsCalledByPayClaimForm Then 'DEF-17426
                If Me.NavController.Context = "PAY_CLAIM_DETAIL-AUTH_DETAIL" Or Me.NavController.Context = "PAY_CLAIM_ADJUSTMENT-PAY_CLAIM_DETAIL_CLADJ-AUTH_DETAIL" Then
                    Me.State.ClaimAuthDetailBO = New ClaimAuthDetail(Me.State.ClaimBO.Id, True)
                Else
                    Me.State.ClaimAuthDetailBO = Me.State.ClaimBO.AddClaimAuthDetail(Me.State.ClaimBO.Id, True, True) '= New ClaimAuthDetail(Me.State.ClaimBO.Id, True)
                End If
                Me.State.OriginalClaimAuthDetailBO = New ClaimAuthDetail
                Me.State.OriginalClaimAuthDetailBO.Clone(Me.State.ClaimAuthDetailBO)
            End If
            Me.State.IsEditMode_AD = True
            Me.State.IsNewMode_AD = False
            Me.PopulateADFormFromBO()
        Catch ex As DataNotFoundException
            LoadClaimTaxRates(True)
            Me.State.IsNewMode_AD = True
            Me.State.IsEditMode_AD = False
            'If Me.State.IsCalledByPayClaimForm Then 'DEF-17426
            If Me.NavController.Context = "PAY_CLAIM_DETAIL-AUTH_DETAIL" Or Me.NavController.Context = "PAY_CLAIM_ADJUSTMENT-PAY_CLAIM_DETAIL_CLADJ-AUTH_DETAIL" Then
                Me.State.ClaimAuthDetailBO = New ClaimAuthDetail()
            Else
                Me.State.ClaimAuthDetailBO = Me.State.ClaimBO.AddClaimAuthDetail(Guid.Empty) 'New ClaimAuthDetail()
            End If
            Me.State.OriginalClaimAuthDetailBO = New ClaimAuthDetail
            Me.State.OriginalClaimAuthDetailBO.Clone(Me.State.ClaimAuthDetailBO)
            Me.State.NoClaimAuthDetailBOFound = True
            Me.State.ClaimAuthDetailBO.PartAmount = GetTotalCost()
            If Me.State.ClaimAuthDetailBO.PartAmount.Value > 0 Then
                Me.State.HasDataChanged = False
                Me.State.HasclaimStatusBOChanged = False
                Me.PopulateADFormFromBO()
            Else
                Me.moAuthDetailTabPanel_WRITE.Visible = False
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrController)
        End Try

    End Sub

    Private Function GetDV() As DataView

        Dim dv As DataView

        dv = GetGridDataView()
        dv.Sort = Grid.DataMember()

        Return (dv)

    End Function

    Private Function GetGridDataView() As DataView
        If Me.State.PartsInfoDV Is Nothing Then
            Me.State.PartsInfoDV = PartsInfo.getSelectedList(Me.State.ClaimBO.Id)
        End If
        Me.State.OreginalPartsInfoTable = Me.State.PartsInfoDV.Table.Copy
        Me.State.OreginalPartsInfoDataViewFilter = Me.State.PartsInfoDV.RowFilter
        Return Me.State.PartsInfoDV
    End Function

    Private Sub SetStateProperties()
        Me.State.CompanyId = Me.State.ClaimBO.CompanyId
        Dim riskTypeBO As RiskType = New RiskType(Me.State.ClaimBO.RiskTypeId)
        Me.State.RiskGroupID = riskTypeBO.RiskGroupId
        Me.State.RiskGroup = LookupListNew.GetDescriptionFromId(LookupListNew.GetRiskGroupsLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), Me.State.RiskGroupID)
    End Sub

    Private Sub AddNewPartsInfo()

        Dim dv As DataView

        Try
            dv = GetGridDataView()

            Dim objPartsInfo As PartsInfo

            'If Me.State.IsCalledByPayClaimForm Then 'DEF-17426
            If Me.NavController.Context = "PAY_CLAIM_DETAIL-AUTH_DETAIL" Or Me.NavController.Context = "PAY_CLAIM_ADJUSTMENT-PAY_CLAIM_DETAIL_CLADJ-AUTH_DETAIL" Then
                objPartsInfo = Me.State.ClaimAuthDetailBO.AddPartsInfo(Guid.Empty)
            Else
                objPartsInfo = Me.State.ClaimBO.AddPartsInfo(Guid.Empty) 'Me.State.ClaimAuthDetailBO.AddPartsInfo(Guid.Empty)
            End If

            Me.State.PartsInfoID = objPartsInfo.Id
            objPartsInfo.ClaimId = Me.State.ClaimBO.Id

            'add the new BO to arraylist
            If Me.State.PartsInfoBOs Is Nothing Then
                Me.State.PartsInfoBOs = New ArrayList
                Me.State.PartsInfoBOIDs = New ArrayList
            End If
            Me.State.PartsInfoBOs.Add(objPartsInfo)
            Me.State.PartsInfoBOIDs.Add(objPartsInfo.Id)

            dv = objPartsInfo.GetNewDataViewRow(dv, Me.State.PartsInfoID, Me.State.ClaimBO.Id)

            Me.State.PartsInfoDV = dv
            Me.State.LastRowIndexInUse = dv.Count - 1

            Grid.DataSource = dv

            Me.SetPageAndSelectedIndexFromGuid(dv, Me.State.PartsInfoID, Me.Grid, Me.State.PageIndex, Me.State.IsEditMode_PI)

            Grid.DataBind()

            Me.State.PageIndex = Grid.PageIndex
            Me.State.LastOperation = Me.NEW_COMMAND

            SetGridControls(Me.Grid, False)

            'Set focus on the Description TextBox for the EditIndex row
            Me.SetFocusOnEditableFieldInGrid(Me.Grid, Me.DESCRIPTION_COL, Me.DESCRIPTION_CONTROL_NAME, Me.Grid.EditIndex, True, objPartsInfo)

            Me.PopulateFormFromBO(objPartsInfo)

            Me.TranslateGridControls(Grid)
            Me.SetButtonsState()

            'Update the dropdown row filter arraylist
            'If Me.State.DropDownRowFilterArrayList Is Nothing Then Me.State.DropDownRowFilterArrayList = New ArrayList
            'Me.State.DropDownRowFilterArrayList.Add(objPartsInfo.PartsDescriptionId)

        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrController)
        End Try
    End Sub

    Private Sub PopulateBOFromForm(ByVal objPartsInfo As PartsInfo, ByVal command As String)


        If Not command.Equals(Me.DELETE_COMMAND) Then
            With objPartsInfo
                .PartsDescriptionId = New Guid(CType(Me.Grid.Rows(Me.Grid.EditIndex).Cells(Me.DESCRIPTION_COL).FindControl(Me.DESCRIPTION_CONTROL_NAME), DropDownList).SelectedValue)
                .InStockID = New Guid(CType(Me.Grid.Rows(Me.Grid.EditIndex).Cells(Me.IN_STOCK_ID_COL).FindControl(Me.IN_STOCK_ID_CONTROL_NAME), DropDownList).SelectedValue)
                Try
                    .Cost = New DecimalType(CType(CType(Me.Grid.Rows(Me.Grid.EditIndex).Cells(Me.COST_COL).FindControl(Me.COST_CONTROL_NAME), TextBox).Text, Decimal))
                Catch
                    .Cost = Nothing
                End Try

                Me.State.PartsInfoDV.Table.Rows(Me.State.LastRowIndexInUse)(PartsInfo.PartsInfoDV.COL_NAME_PARTS_INFO_ID) = .Id.ToByteArray
                Me.State.PartsInfoDV.Table.Rows(Me.State.LastRowIndexInUse)(PartsInfo.PartsInfoDV.COL_NAME_PARTS_DESCRIPTION_ID) = .PartsDescriptionId.ToByteArray
                Me.State.PartsInfoDV.Table.Rows(Me.State.LastRowIndexInUse)(PartsInfo.PartsInfoDV.COL_NAME_PARTS_DESCRIPTION) = CType(Me.Grid.Rows(Me.Grid.EditIndex).Cells(Me.PART_DESCRIPTION_COL).FindControl(Me.DESCRIPTION_CONTROL_NAME), DropDownList).SelectedItem.Text
                Me.State.PartsInfoDV.Table.Rows(Me.State.LastRowIndexInUse)(PartsInfo.PartsInfoDV.COL_NAME_IN_STOCK_DESCRIPTION) = CType(Me.Grid.Rows(Me.Grid.EditIndex).Cells(Me.IN_STOCK_ID_COL).FindControl(Me.IN_STOCK_ID_CONTROL_NAME), DropDownList).SelectedItem.Text
                Me.State.PartsInfoDV.Table.Rows(Me.State.LastRowIndexInUse)(PartsInfo.PartsInfoDV.COL_NAME_CLAIM_ID) = .ClaimId.ToByteArray
                Dim cost As Decimal = 0
                If Not .Cost Is Nothing Then
                    cost = .Cost.Value
                End If
                Me.State.PartsInfoDV.Table.Rows(Me.State.LastRowIndexInUse)(PartsInfo.PartsInfoDV.COL_NAME_COST) = cost.ToString
            End With
            objPartsInfo.Validate()
            If Me.ErrCollection.Count > 0 Then
                Throw New PopulateBOErrorException
            End If
        End If


    End Sub

    Private Sub Populate_AD_BOFromForm(Optional ByVal validateBO As Boolean = True)

        'Me.ClearADZeroAmounts()

        If Not Me.State.ClaimAuthDetailBO Is Nothing AndAlso Not Me.State.ClaimAuthDetailBO.IsDeleted Then
            Me.PopulateBOProperty(Me.State.ClaimAuthDetailBO, "ClaimId", Me.State.ClaimBO.Id)
            Me.PopulateBOProperty(Me.State.ClaimAuthDetailBO, "LaborAmount", Me.txtLabor)
            Me.PopulateBOProperty(Me.State.ClaimAuthDetailBO, "PartAmount", Me.txtParts)
            Me.PopulateBOProperty(Me.State.ClaimAuthDetailBO, "ServiceCharge", Me.txtServiceCharge)
            Me.PopulateBOProperty(Me.State.ClaimAuthDetailBO, "TripAmount", Me.txtTripAmt)
            Me.PopulateBOProperty(Me.State.ClaimAuthDetailBO, "ShippingAmount", Me.txtShippingAmt)
            Me.PopulateBOProperty(Me.State.ClaimAuthDetailBO, "DispositionAmount", Me.txtDisposition)
            Me.PopulateBOProperty(Me.State.ClaimAuthDetailBO, "DiagnosticsAmount", Me.txtDiagnostics)
            Me.PopulateBOProperty(Me.State.ClaimAuthDetailBO, "OtherAmount", Me.txtOtherAmt)
            Me.PopulateBOProperty(Me.State.ClaimAuthDetailBO, "OtherExplanation", Me.txtOtherDesc)
            Me.PopulateBOProperty(Me.State.ClaimAuthDetailBO, "TotalTaxAmount", Me.txtTotalTaxAmount)

            If validateBO Then Me.State.ClaimAuthDetailBO.Validate()

            Dim Total As Decimal
            With Me.State.ClaimAuthDetailBO
                Dim subTotal As Decimal = 0
                If Not .LaborAmount Is Nothing Then subTotal += .LaborAmount.Value
                If Not .PartAmount Is Nothing Then subTotal += .PartAmount.Value
                If Not .ServiceCharge Is Nothing Then subTotal += .ServiceCharge.Value
                If Not .TripAmount Is Nothing Then subTotal += .TripAmount.Value
                If Not .ShippingAmount Is Nothing Then subTotal += .ShippingAmount.Value
                If Not .DiagnosticsAmount Is Nothing Then subTotal += .DiagnosticsAmount.Value
                If Not .DispositionAmount Is Nothing Then subTotal += .DispositionAmount.Value
                If Not .OtherAmount Is Nothing Then subTotal += .OtherAmount.Value
                Me.PopulateControlFromBOProperty(Me.txtSubTotal, subTotal)
                LoadClaimTaxRates(True)
                Total = subTotal + Me.State.TotalTaxAmount
                Me.PopulateControlFromBOProperty(Me.txtTotal, Total)
            End With

            If Total > 0 Then
                Me.State.IsEditMode_AD = True
                Me.State.IsNewMode_AD = False
            Else
                Me.State.IsEditMode_AD = False
                Me.State.IsNewMode_AD = True
            End If

        End If

        If Me.ErrCollection.Count > 0 Then
            Throw New PopulateBOErrorException
        End If


        If Not Me.State.ClaimAuthDetailBO Is Nothing AndAlso Not Me.State.ClaimAuthDetailBO.IsDeleted Then
            With Me.State.ClaimAuthDetailBO
                If Not .LaborAmount Is Nothing Then Me.PopulateControlFromBOProperty(Me.txtLabor, .LaborAmount.Value)
                If Not .PartAmount Is Nothing Then Me.PopulateControlFromBOProperty(Me.txtParts, .PartAmount.Value)
                If Not .ServiceCharge Is Nothing Then Me.PopulateControlFromBOProperty(Me.txtServiceCharge, .ServiceCharge.Value)
                If Not .TripAmount Is Nothing Then Me.PopulateControlFromBOProperty(Me.txtTripAmt, .TripAmount.Value)
                If Not .ShippingAmount Is Nothing Then Me.PopulateControlFromBOProperty(Me.txtShippingAmt, .ShippingAmount.Value)
                If Not .DiagnosticsAmount Is Nothing Then Me.PopulateControlFromBOProperty(Me.txtDiagnostics, .DiagnosticsAmount.Value)
                If Not .DispositionAmount Is Nothing Then Me.PopulateControlFromBOProperty(Me.txtDisposition, .DispositionAmount.Value)
                If Not .OtherAmount Is Nothing Then Me.PopulateControlFromBOProperty(Me.txtOtherAmt, .OtherAmount.Value)
                Me.PopulateControlFromBOProperty(Me.txtOtherDesc, .OtherExplanation)

                Dim subTotal As Decimal = 0
                If Not .LaborAmount Is Nothing Then subTotal += .LaborAmount.Value
                If Not .PartAmount Is Nothing Then subTotal += .PartAmount.Value
                If Not .ServiceCharge Is Nothing Then subTotal += .ServiceCharge.Value
                If Not .TripAmount Is Nothing Then subTotal += .TripAmount.Value
                If Not .ShippingAmount Is Nothing Then subTotal += .ShippingAmount.Value
                If Not .DiagnosticsAmount Is Nothing Then subTotal += .DiagnosticsAmount.Value
                If Not .DispositionAmount Is Nothing Then subTotal += .DispositionAmount.Value
                If Not .OtherAmount Is Nothing Then subTotal += .OtherAmount.Value
                Me.PopulateControlFromBOProperty(Me.txtSubTotal, subTotal)
                LoadClaimTaxRates(True)
                Dim Total As Decimal = subTotal + Me.State.TotalTaxAmount
                Me.PopulateControlFromBOProperty(Me.txtTotal, Total)

                If Total > 0 Then
                    Me.State.IsEditMode_AD = True
                    Me.State.IsNewMode_AD = False
                Else
                    Me.State.IsEditMode_AD = False
                    Me.State.IsNewMode_AD = True
                End If
            End With

        End If
    End Sub

    Private Sub PopulateApprove_DisApproveClaimStatus()
        'GVS Approve or disapprove 
        If IsIntegratedWithGVS() Then
            If (Not Me.State.ClaimStatusBO Is Nothing AndAlso (Me.State.ClaimStatusBO.StatusCode = Codes.CLAIM_EXTENDED_STATUS__WAITING_ON_BUDGET_APPROVAL)) Then
                Dim objNewClaimStatusId As Guid = Me.GetStatusId
                If Not objNewClaimStatusId.Equals(Guid.Empty) Then
                    'If Me.State.IsCalledByPayClaimForm Then 'DEF-17426
                    If Me.NavController.Context = "PAY_CLAIM_DETAIL-AUTH_DETAIL" Or Me.NavController.Context = "PAY_CLAIM_ADJUSTMENT-PAY_CLAIM_DETAIL_CLADJ-AUTH_DETAIL" Then
                        Me.State.NewClaimStatusBO = Me.State.ClaimAuthDetailBO.AddClaimStatus(Guid.Empty)
                    Else
                        Me.State.NewClaimStatusBO = Me.State.ClaimBO.AddExtendedClaimStatus(Guid.Empty)
                    End If
                    Me.State.NewClaimStatusBO.ClaimStatusByGroupId = objNewClaimStatusId
                    Me.State.NewClaimStatusBO.ClaimId = Me.State.ClaimBO.Id
                    Me.State.NewClaimStatusBO.StatusDate = Date.Now
                    Me.State.HasclaimStatusBOChanged = True
                End If
            End If
        End If
    End Sub

    Function GetStatusId() As Guid
        If Me.chkApproved.Checked Then
            Return ClaimStatusByGroup.GetClaimStatusByGroupID(Codes.CLAIM_EXTENDED_STATUS__BUDGET_APPROVED)
        ElseIf Me.chkDisapproved.Checked Then
            Return ClaimStatusByGroup.GetClaimStatusByGroupID(Codes.CLAIM_EXTENDED_STATUS__BUDGET_REJECTED)
        Else
            Return Guid.Empty
        End If

    End Function

    Private Sub PopulateFormFromBO(ByVal objPartsInfo As PartsInfo)

        Dim gridRowIdx As Integer = Me.Grid.EditIndex
        Try
            With objPartsInfo
                Dim ddlDescription As DropDownList = CType(Me.Grid.Rows(gridRowIdx).Cells(Me.DESCRIPTION_COL).FindControl(Me.DESCRIPTION_CONTROL_NAME), DropDownList)
                If Not .PartsDescriptionId.Equals(Guid.Empty) AndAlso Not ddlDescription Is Nothing Then
                    ddlDescription.SelectedValue = .PartsDescriptionId.ToString()
                End If

                Dim ddlInStock As DropDownList = CType(Me.Grid.Rows(gridRowIdx).Cells(Me.IN_STOCK_ID_COL).FindControl(Me.IN_STOCK_ID_CONTROL_NAME), DropDownList)
                If Not .InStockID.Equals(Guid.Empty) AndAlso Not ddlInStock Is Nothing Then
                    ddlInStock.SelectedValue = .InStockID.ToString()
                End If

                Dim txtCost As TextBox = CType(Me.Grid.Rows(gridRowIdx).Cells(Me.COST_COL).FindControl(Me.COST_CONTROL_NAME), TextBox)
                If Not .Cost Is Nothing AndAlso Not txtCost Is Nothing Then
                    Me.PopulateControlFromBOProperty(txtCost, .Cost.Value)
                End If

                Dim lblID As Label = CType(Me.Grid.Rows(gridRowIdx).Cells(Me.ID_COL).FindControl(Me.ID_CONTROL_NAME), Label)
                If Not lblID Is Nothing Then
                    lblID.Text = .Id.ToString
                End If

            End With

        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrController)
        End Try

    End Sub

    Private Sub PopulateADFormFromBO()

        Dim gridRowIdx As Integer = Me.Grid.EditIndex
        Try
            If Not Me.State.ClaimAuthDetailBO Is Nothing AndAlso Not Me.State.ClaimAuthDetailBO.IsDeleted Then
                With Me.State.ClaimAuthDetailBO
                    If Not .LaborAmount Is Nothing AndAlso .LaborAmount.Value > 0 Then
                        Me.PopulateControlFromBOProperty(Me.txtLabor, .LaborAmount.Value)
                    Else
                        txtLabor.Text = Nothing
                    End If

                    If Not .PartAmount Is Nothing AndAlso .PartAmount.Value > 0 Then
                        Me.PopulateControlFromBOProperty(Me.txtParts, .PartAmount.Value)
                    Else
                        txtParts.Text = Nothing
                    End If

                    If Not .ServiceCharge Is Nothing AndAlso .ServiceCharge.Value > 0 Then
                        Me.PopulateControlFromBOProperty(Me.txtServiceCharge, .ServiceCharge.Value)
                    Else
                        txtServiceCharge.Text = Nothing
                    End If

                    If Not .TripAmount Is Nothing AndAlso .TripAmount.Value > 0 Then
                        Me.PopulateControlFromBOProperty(Me.txtTripAmt, .TripAmount.Value)
                    Else
                        txtTripAmt.Text = Nothing
                    End If

                    If Not .ShippingAmount Is Nothing AndAlso .ShippingAmount.Value > 0 Then
                        Me.PopulateControlFromBOProperty(Me.txtShippingAmt, .ShippingAmount.Value)
                    Else
                        txtShippingAmt.Text = Nothing
                    End If

                    If Not .DiagnosticsAmount Is Nothing AndAlso .DiagnosticsAmount.Value > 0 Then
                        Me.PopulateControlFromBOProperty(Me.txtDiagnostics, .DiagnosticsAmount.Value)
                    Else
                        txtDiagnostics.Text = Nothing
                    End If

                    If Not .DispositionAmount Is Nothing AndAlso .DispositionAmount.Value > 0 Then
                        Me.PopulateControlFromBOProperty(Me.txtDisposition, .DispositionAmount.Value)
                    Else
                        txtDisposition.Text = Nothing
                    End If

                    If Not .OtherAmount Is Nothing AndAlso .OtherAmount.Value > 0 Then
                        Me.PopulateControlFromBOProperty(Me.txtOtherAmt, .OtherAmount.Value)
                    Else
                        txtOtherAmt.Text = Nothing
                    End If
                    Me.PopulateControlFromBOProperty(Me.txtOtherDesc, .OtherExplanation)


                    Dim subTotal As Decimal = Me.GetSubTotal
                    LoadClaimTaxRates(True)
                    Me.PopulateControlFromBOProperty(Me.txtSubTotal, subTotal)

                    Dim Total As Decimal = subTotal + Me.State.TotalTaxAmount
                    Me.PopulateControlFromBOProperty(Me.txtTotal, Total)

                    If Total > 0 Then
                        Me.State.IsEditMode_AD = True
                        Me.State.IsNewMode_AD = False
                        Me.moAuthDetailTabPanel_WRITE.Visible = True
                    Else
                        Me.State.IsEditMode_AD = False
                        Me.State.IsNewMode_AD = True
                        Me.moAuthDetailTabPanel_WRITE.Visible = False
                        Me.ClearADForm()
                    End If
                End With
            End If



        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrController)
        End Try

    End Sub


    Private Sub LoadClaimTaxRates(Optional blnComputeTax As Boolean = False)

        If Me.State.ClaimTaxRatesData Is Nothing Then
            Dim company As New Company(Me.State.CompanyId)
            Dim CountryId As Guid = company.BusinessCountryId
            Dim RegionId As Guid = Me.State.ClaimBO.ServiceCenterObject.Address.RegionId
            Dim dealer_id As Guid = Me.State.ClaimBO.Dealer.Id
            Me.State.ClaimTaxRatesData = ClaimInvoice.GetClaimTaxRatesData(CountryId, RegionId, dealer_id, System.DateTime.Now, Me.State.ClaimMethodOfRepair)
        End If

        With Me.State.ClaimTaxRatesData
            hdTaxRateClaimDiagnostics.Value = .taxRateClaimDiagnostics.ToString
            hdComputeMethodClaimDiagnostics.Value = .computeMethodCodeClaimDiagnostics

            hdTaxRateClaimOther.Value = .taxRateClaimOther.ToString
            hdComputeMethodClaimOther.Value = .computeMethodCodeClaimOther

            hdTaxRateClaimDisposition.Value = .taxRateClaimDisposition.ToString
            hdComputeMethodClaimDisposition.Value = .computeMethodCodeClaimDisposition

            hdTaxRateClaimLabor.Value = .taxRateClaimLabor.ToString
            hdComputeMethodClaimLabor.Value = .computeMethodCodeClaimLabor

            hdTaxRateClaimParts.Value = .taxRateClaimParts.ToString
            hdComputeMethodClaimParts.Value = .computeMethodCodeClaimParts

            hdTaxRateClaimShipping.Value = .taxRateClaimShipping.ToString
            hdComputeMethodClaimShipping.Value = .computeMethodCodeClaimShipping

            hdTaxRateClaimService.Value = .taxRateClaimService.ToString
            hdComputeMethodClaimService.Value = .computeMethodCodeClaimService

            hdTaxRateClaimTrip.Value = .taxRateClaimTrip.ToString
            hdComputeMethodClaimTrip.Value = .computeMethodCodeClaimTrip

            If blnComputeTax Then ComputeAutoTaxes()

        End With
    End Sub

    Private Sub ComputeAutoTaxes()
        Me.State.TotalTaxAmount = 0

        Try
            Me.txtPartsTax.Text = computeTaxAmtByComputeMethod(CType(Me.txtParts.Text, Decimal), CType(Me.hdTaxRateClaimParts.Value, Decimal), hdComputeMethodClaimParts.Value).ToString
        Catch ex As Exception
            Me.txtPartsTax.Text = ""
        End Try

        Try
            Me.txtLaborTax.Text = computeTaxAmtByComputeMethod(CType(Me.txtLabor.Text, Decimal), CType(Me.hdTaxRateClaimLabor.Value, Decimal), hdComputeMethodClaimLabor.Value).ToString
        Catch ex As Exception
            Me.txtLaborTax.Text = ""
        End Try

        Try
            Me.txtServiceChargeTax.Text = computeTaxAmtByComputeMethod(CType(Me.txtServiceCharge.Text, Decimal), CType(Me.hdTaxRateClaimService.Value, Decimal), hdComputeMethodClaimService.Value).ToString
        Catch ex As Exception
            Me.txtServiceChargeTax.Text = ""
        End Try

        Try
            Me.txtTripAmtTax.Text = computeTaxAmtByComputeMethod(CType(Me.txtTripAmt.Text, Decimal), CType(Me.hdTaxRateClaimTrip.Value, Decimal), hdComputeMethodClaimTrip.Value).ToString
        Catch ex As Exception
            Me.txtTripAmtTax.Text = ""
        End Try

        Try
            Me.txtShippingTax.Text = computeTaxAmtByComputeMethod(CType(Me.txtShippingAmt.Text, Decimal), CType(Me.hdTaxRateClaimShipping.Value, Decimal), hdComputeMethodClaimShipping.Value).ToString
        Catch ex As Exception
            Me.txtShippingTax.Text = ""
        End Try

        Try
            Me.txtDispositionTax.Text = computeTaxAmtByComputeMethod(CType(Me.txtDisposition.Text, Decimal), CType(Me.hdTaxRateClaimDisposition.Value, Decimal), hdComputeMethodClaimDisposition.Value).ToString
        Catch ex As Exception
            Me.txtDispositionTax.Text = ""
        End Try

        Try
            Me.txtDiagnosticsTax.Text = computeTaxAmtByComputeMethod(CType(Me.txtDiagnostics.Text, Decimal), CType(Me.hdTaxRateClaimDiagnostics.Value, Decimal), hdComputeMethodClaimDiagnostics.Value).ToString
        Catch ex As Exception
            Me.txtDiagnosticsTax.Text = ""
        End Try

        Try
            Me.txtOtherTax.Text = computeTaxAmtByComputeMethod(CType(Me.txtOtherAmt.Text, Decimal), CType(Me.hdTaxRateClaimOther.Value, Decimal), hdComputeMethodClaimOther.Value).ToString
        Catch ex As Exception
            Me.txtOtherTax.Text = ""
        End Try


        Me.txtTotalTaxAmount.Text = Math.Round(Me.State.TotalTaxAmount, 2).ToString

    End Sub

    Private Function ComputePartsTaxes(totalpartsCost As DecimalType) As DecimalType
        LoadClaimTaxRates(False)
        Try
            Return New DecimalType(computeTaxAmtByComputeMethod(totalpartsCost, CType(Me.hdTaxRateClaimParts.Value, Decimal), hdComputeMethodClaimParts.Value))

        Catch ex As Exception
            Return New DecimalType(0)
        End Try


    End Function

    Private Function computeTaxAmtByComputeMethod(amount As DecimalType, TaxRate As DecimalType, ComputeMethodCode As String) As Decimal
        Dim taxAmount As Decimal = 0

        If amount.Value > 0 Then
            If ComputeMethodCode.ToUpper = "N" Then
                taxAmount = Math.Round(((getDecimalValue(amount) * getDecimalValue(TaxRate)) / 100), 2)

            ElseIf ComputeMethodCode.ToUpper = "G" Then
                Dim gross As Decimal
                gross = CDec(Math.Round((getDecimalValue(amount) / (1.0 + getDecimalValue(TaxRate))), 2))
                taxAmount = Math.Round(((getDecimalValue(amount) / gross) - 1), 2)
            End If
        End If

        Me.State.TotalTaxAmount += taxAmount

        Return taxAmount

    End Function
    Private Function getDecimalValue(ByVal decimalObj As DecimalType, Optional ByVal decimalDigit As Integer = 2) As Decimal
        If decimalObj Is Nothing Then
            Return 0D
        Else
            Return Math.Round(decimalObj.Value, decimalDigit, MidpointRounding.AwayFromZero)
        End If
    End Function


    Private Sub ClearADForm()
        txtLabor.Text = Nothing
        txtParts.Text = Nothing
        txtServiceCharge.Text = Nothing
        txtTripAmt.Text = Nothing
        txtShippingAmt.Text = Nothing
        txtDiagnostics.Text = Nothing
        txtDisposition.Text = Nothing
        txtOtherAmt.Text = Nothing
        txtOtherDesc.Text = Nothing
        txtTotal.Text = Nothing
        txtSubTotal.Text = Nothing
        txtTotalTaxAmount.Text = Nothing
    End Sub

    Private Sub UpdatePartsAmountInClaimAuthDetailBO()
        If Not Me.State.ClaimAuthDetailBO Is Nothing AndAlso Not Me.State.ClaimAuthDetailBO.IsDeleted Then
            Dim partsTotal As Decimal
            For i As Integer = 0 To Me.State.PartsInfoDV.Count - 1
                partsTotal = partsTotal + CType(Me.State.PartsInfoDV.Item(i).Item(Me.VIEW_COST_COL), Decimal)
            Next
            If partsTotal > 0 Then
                Me.State.ClaimAuthDetailBO.PartAmount = partsTotal
            Else
                Me.State.ClaimAuthDetailBO.PartAmount = 0
            End If
        End If
    End Sub

    Private Sub ReturnFromEditing()

        Grid.EditIndex = NO_ROW_SELECTED_INDEX

        If Me.Grid.PageCount = 0 Then
            'if returning to the "1st time in" screen
            ControlMgr.SetVisibleControl(Me, Grid, False)
        Else
            ControlMgr.SetVisibleControl(Me, Grid, True)
        End If

        Me.State.IsEditMode_PI = False
        Me.State.IsNewMode_PI = False
        Me.State.IsDone = True
        Me.PopulateGrid()
        Me.State.PageIndex = Grid.PageIndex
        SetButtonsState()

    End Sub

    Private Sub ReturnFromEditingAD()

        Me.State.IsDone = True
        Me.PopulateGrid()
        SetButtonsState()
        Me.EnableDisableFields()
    End Sub

    Private Sub SetButtonsState(Optional ByVal blnSetGridControl As Boolean = True)
        If Not Me.State.IsViewOnly Then
            ControlMgr.SetEnableControl(Me, Me.btnNew_PI_WRITE, Not Me.State.IsEditMode_PI And Not Me.State.IsNewMode_PI And isEditableForGVS())
            ControlMgr.SetEnableControl(Me, Me.btnSave_PI_WRITE, (Me.State.IsEditMode_PI Or Me.State.IsNewMode_PI) And isEditableForGVS())
            ControlMgr.SetVisibleControl(Me, Me.btnSave_WRITE, (Me.NavController.Context = "PAY_CLAIM_DETAIL-AUTH_DETAIL" Or Me.NavController.Context = "PAY_CLAIM_ADJUSTMENT-PAY_CLAIM_DETAIL_CLADJ-AUTH_DETAIL"))
            ControlMgr.SetEnableControl(Me, Me.Cancel_PI_Button, (Me.State.IsEditMode_PI Or Me.State.IsNewMode_PI) And isEditableForGVS())
            'ControlMgr.SetVisibleControl(Me, Me.btnSave_WRITE, Me.State.IsCalledByPayClaimForm) 'DEF-17426
            ControlMgr.SetEnableControl(Me, Me.btnSave_WRITE, (Me.NavController.Context = "PAY_CLAIM_DETAIL-AUTH_DETAIL" Or Me.NavController.Context = "PAY_CLAIM_ADJUSTMENT-PAY_CLAIM_DETAIL_CLADJ-AUTH_DETAIL"))
            ControlMgr.SetEnableControl(Me, Me.btnBack, True)
            ControlMgr.SetEnableControl(Me, Me.btnSave_WRITE, Me.State.IsDone And Me.State.IsCalledByPayClaimForm)
            ControlMgr.SetEnableControl(Me, Me.btnUndo_Write, Me.State.IsDone)

            'Claim Detail controls
            ControlMgr.SetVisibleControl(Me, Me.BtnEdit_AD_WRITE, Me.State.IsEditMode_AD And isEditableForGVS(True))
            ControlMgr.SetVisibleControl(Me, Me.btnNew_AD_WRITE, Me.State.IsNewMode_AD And isEditableForGVS(True))

            ControlMgr.SetEnableControl(Me, Me.BtnEdit_AD_WRITE, Me.State.IsEditMode_AD And isEditableForGVS(True))
            ControlMgr.SetEnableControl(Me, Me.btnNew_AD_WRITE, Me.State.IsNewMode_AD And isEditableForGVS(True))
            ControlMgr.SetEnableControl(Me, Me.Cancel_AD_Button, False)
            ControlMgr.SetEnableControl(Me, Me.btnSave_AD_WRITE, False)
            'ControlMgr.SetVisibleControl(Me, Me.pnlApprove_Disapprove, Me.State.IsEditMode_AD And isEditableForGVS(False))
            ControlMgr.SetEnableControl(Me, Me.pnlApprove_Disapprove, False)
        Else
            ControlMgr.SetEnableControl(Me, Me.btnNew_PI_WRITE, Not Me.State.IsViewOnly)
            ControlMgr.SetEnableControl(Me, Me.btnSave_PI_WRITE, Not Me.State.IsViewOnly)
            ControlMgr.SetEnableControl(Me, Me.Cancel_PI_Button, Not Me.State.IsViewOnly)
            ControlMgr.SetVisibleControl(Me, Me.btnSave_WRITE, Not Me.State.IsViewOnly)

            ControlMgr.SetEnableControl(Me, Me.btnBack, True)
            ControlMgr.SetEnableControl(Me, Me.btnSave_WRITE, Not Me.State.IsViewOnly)
            ControlMgr.SetEnableControl(Me, Me.btnUndo_Write, Not Me.State.IsViewOnly)

            'Claim Detail controls
            ControlMgr.SetVisibleControl(Me, Me.BtnEdit_AD_WRITE, Not Me.State.IsViewOnly)
            ControlMgr.SetVisibleControl(Me, Me.btnNew_AD_WRITE, Not Me.State.IsViewOnly)

            ControlMgr.SetEnableControl(Me, Me.BtnEdit_AD_WRITE, Not Me.State.IsViewOnly)
            ControlMgr.SetEnableControl(Me, Me.btnNew_AD_WRITE, Not Me.State.IsViewOnly)
            ControlMgr.SetEnableControl(Me, Me.Cancel_AD_Button, False)
            ControlMgr.SetEnableControl(Me, Me.btnSave_AD_WRITE, False)
            'ControlMgr.SetVisibleControl(Me, Me.pnlApprove_Disapprove, Me.State.IsEditMode_AD And isEditableForGVS(False))
            If blnSetGridControl Then SetGridControls(Me.Grid, False)
        End If
    End Sub

    Private Sub EnableDisableFields()
        If Not Me.State.IsViewOnly Then
            Me.txtLabor.ReadOnly = Not ((Me.State.IsEditMode_AD Or Me.State.IsNewMode_AD) And Me.State.IsEditable_AD And isEditableForGVS())
            Me.txtServiceCharge.ReadOnly = Not ((Me.State.IsEditMode_AD Or Me.State.IsNewMode_AD) And Me.State.IsEditable_AD And isEditableForGVS())
            Me.txtTripAmt.ReadOnly = Not ((Me.State.IsEditMode_AD Or Me.State.IsNewMode_AD) And Me.State.IsEditable_AD And isEditableForGVS())
            Me.txtShippingAmt.ReadOnly = Not ((Me.State.IsEditMode_AD Or Me.State.IsNewMode_AD) And Me.State.IsEditable_AD And isEditableForGVS(True))
            Me.txtOtherAmt.ReadOnly = Not ((Me.State.IsEditMode_AD Or Me.State.IsNewMode_AD) And Me.State.IsEditable_AD And isEditableForGVS())
            Me.txtOtherDesc.ReadOnly = Not ((Me.State.IsEditMode_AD Or Me.State.IsNewMode_AD) And Me.State.IsEditable_AD And isEditableForGVS())
            Me.txtDiagnostics.ReadOnly = Not ((Me.State.IsEditMode_AD Or Me.State.IsNewMode_AD) And Me.State.IsEditable_AD And isEditableForGVS())
            Me.txtDisposition.ReadOnly = Not ((Me.State.IsEditMode_AD Or Me.State.IsNewMode_AD) And Me.State.IsEditable_AD And isEditableForGVS())
            Me.txtParts.ReadOnly = Not ((Me.State.IsEditMode_AD Or Me.State.IsNewMode_AD) And Me.State.IsEditable_AD And isEditableForGVS() And Me.State.PartsDescriptionAvailable)
        End If
    End Sub

    Private Sub ToggleMainButtons(ByVal enabled As Boolean)
        ControlMgr.SetEnableControl(Me, Me.btnBack, enabled)
        ControlMgr.SetEnableControl(Me, Me.btnSave_WRITE, enabled)
        ControlMgr.SetEnableControl(Me, Me.btnUndo_Write, enabled)
    End Sub

    Private Function GetDirtyStatus() As Boolean
        Dim IsBODirty As Boolean = False
        If Me.State.ClaimBO.MethodOfRepairCode <> Codes.METHOD_OF_REPAIR__REPLACEMENT Then
            If Me.State.ClaimAuthDetailBO.IsNew Then
                If Not Me.State.ClaimAuthDetailBO.PartAmount Is Nothing AndAlso Me.GetSubTotal > Me.State.ClaimAuthDetailBO.PartAmount.Value Then
                    IsBODirty = True
                    'ElseIf Me.State.ClaimAuthDetailBO.PartAmount Is Nothing AndAlso Me.GetSubTotal >= 0 Then
                ElseIf Me.State.ClaimAuthDetailBO.PartAmount Is Nothing AndAlso Me.GetSubTotal > 0 Then 'DEF: 29274 - REQ-289 Actions dropdown list for active claim for CLAR dealer. (Changed the above commented code to that on this line.)
                    IsBODirty = True
                End If
            Else
                IsBODirty = Me.State.ClaimAuthDetailBO.IsDirty
            End If
        ElseIf (Me.State.ClaimAuthDetailBO.IsNew AndAlso Me.GetSubTotal >= 0) Then
            IsBODirty = True
        Else
            IsBODirty = Me.State.ClaimAuthDetailBO.IsDirty
        End If

        Return IsBODirty
    End Function

    Private Function IsClaimAuthDetailBODirty() As Boolean

    End Function
#End Region

#Region "DataViewRelated "
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
            'Me.State.SortExpression = Me.SortDirection
            Me.State.PageIndex = 0
            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrController)
        End Try
    End Sub

    Public Property SortDirection() As String
        Get
            Return ViewState("SortDirection").ToString
        End Get
        Set(ByVal value As String)
            ViewState("SortDirection") = value
        End Set
    End Property

    Private Sub Grid_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Grid.PageIndexChanging

        Try
            If (Not (Me.State.IsEditMode_PI)) Then
                Me.State.PageIndex = e.NewPageIndex
                'Me.Grid.PageIndex = Me.State.PageIndex
                Me.PopulateGrid()
                Me.Grid.SelectedIndex = Me.NO_ITEM_SELECTED_INDEX
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrController)
        End Try

        Try
            Me.State.PageIndex = e.NewPageIndex

            Me.PopulateGrid()
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)

        Try
            Dim index As Integer
            Me.State.IsDone = False


            If (e.CommandName = Me.EDIT_COMMAND) Then
                'Do the Edit here

                Dim rowIdx As GridViewRow = CType(CType(e.CommandSource, Control).Parent.Parent, GridViewRow)
                index = rowIdx.RowIndex
                Me.State.LastRowIndexInUse = index

                Me.State.LastOperation = Me.EDIT_COMMAND

                'Set the IsEditMode flag to TRUE
                Me.State.IsEditMode_PI = True

                'Save the Id in the Session
                Me.State.PartsInfoID = New Guid(Me.Grid.Rows(index).Cells(Me.ID_COL).Text)

                'intailize the array list
                If Me.State.PartsInfoBOs Is Nothing Then
                    Me.State.PartsInfoBOs = New ArrayList
                    Me.State.PartsInfoBOIDs = New ArrayList
                End If

                Dim objPartsInfo As PartsInfo

                'check if this item has been added to the arraylist
                Dim objPI_ID As Guid = Guid.Empty
                Dim AddToArray As Boolean = True
                For i As Integer = 0 To Me.State.PartsInfoBOs.Count - 1
                    objPI_ID = CType(Me.State.PartsInfoBOIDs(i), Guid)
                    Dim objPI As PartsInfo = CType(Me.State.PartsInfoBOs(i), PartsInfo)
                    If Me.State.PartsInfoID.Equals(objPI_ID) Then
                        AddToArray = False
                        objPartsInfo = objPI
                        Exit For
                    End If
                Next

                If AddToArray Then
                    'If Me.State.IsCalledByPayClaimForm Then 'DEF-17426
                    If Me.NavController.Context = "PAY_CLAIM_DETAIL-AUTH_DETAIL" Or Me.NavController.Context = "PAY_CLAIM_ADJUSTMENT-PAY_CLAIM_DETAIL_CLADJ-AUTH_DETAIL" Then
                        objPartsInfo = Me.State.ClaimAuthDetailBO.AddPartsInfo(Me.State.PartsInfoID)
                    Else
                        objPartsInfo = Me.State.ClaimBO.AddPartsInfo(Me.State.PartsInfoID) 'Me.State.ClaimAuthDetailBO.AddPartsInfo(Me.State.PartsInfoID)
                    End If
                    Me.State.PartsInfoBOIDs.Add(objPI_ID)
                    Me.State.PartsInfoBOs.Add(objPartsInfo)
                End If

                Me.PopulateGrid()

                Me.State.PageIndex = Grid.PageIndex

                'Disable all Edit and Delete icon buttons on the Grid
                SetGridControls(Me.Grid, False)

                'Set focus on the Description dropdown for the EditIndex row
                Me.SetFocusOnEditableFieldInGrid(Me.Grid, Me.DESCRIPTION_COL, Me.DESCRIPTION_CONTROL_NAME, index, False, objPartsInfo)

                Me.PopulateFormFromBO(objPartsInfo)

                Me.SetButtonsState(False)
                DisabledTabsList.Add(Tab_AuthDetail)
                Me.ToggleMainButtons(False)
            ElseIf (e.CommandName = Me.DELETE_COMMAND) Then
                'Do the delete here

                Dim rowIdx As GridViewRow = CType(CType(e.CommandSource, Control).Parent.Parent, GridViewRow)
                index = rowIdx.RowIndex
                Me.State.LastRowIndexInUse = index

                Me.State.LastOperation = Me.DELETE_COMMAND

                'Clear the SelectedItemStyle to remove the highlight from the previously saved row
                Grid.SelectedIndex = Me.NO_ROW_SELECTED_INDEX

                'Save the Id in the Session
                Me.State.PartsInfoID = New Guid(Me.Grid.Rows(index).Cells(Me.ID_COL).Text)
                '   Dim objPartsInfo As PartsInfo = Me.State.ClaimAuthDetailBO.AddPartsInfo(Me.State.PartsInfoID)
                If Me.State.PartsInfoBOs Is Nothing Then
                    Me.State.PartsInfoBOs = New ArrayList
                    Me.State.PartsInfoBOIDs = New ArrayList
                End If
                Dim objPartsInfo As PartsInfo

                Try
                    Dim AddToArray As Boolean = True
                    Dim objPI_ID As Guid = Guid.Empty
                    For i As Integer = 0 To Me.State.PartsInfoBOs.Count - 1
                        objPI_ID = CType(Me.State.PartsInfoBOIDs(i), Guid)
                        Dim objPI As PartsInfo = CType(Me.State.PartsInfoBOs(i), PartsInfo)
                        If Me.State.PartsInfoID.Equals(objPI_ID) Then
                            Me.State.PartsInfoBOs.RemoveAt(i)
                            Me.State.PartsInfoBOIDs.RemoveAt(i)
                            AddToArray = False
                            Exit For
                        End If
                    Next


                    'check if this bo id was in the original dv
                    Dim row As DataRow
                    For Each row In Me.State.OreginalPartsInfoTable.Rows
                        Dim partInfoID As Guid = New Guid(CType(row(DV_ID_COL), Byte()))
                        If partInfoID.Equals(Me.State.PartsInfoID) Then
                            'if it is, then add to the family and mark it delete
                            'If Me.State.IsCalledByPayClaimForm Then 'DEF-17426
                            If Me.NavController.Context = "PAY_CLAIM_DETAIL-AUTH_DETAIL" Or Me.NavController.Context = "PAY_CLAIM_ADJUSTMENT-PAY_CLAIM_DETAIL_CLADJ-AUTH_DETAIL" Then
                                objPartsInfo = Me.State.ClaimAuthDetailBO.AddPartsInfo(Me.State.PartsInfoID)
                            Else
                                objPartsInfo = Me.State.ClaimBO.AddPartsInfo(Me.State.PartsInfoID) 'Me.State.ClaimAuthDetailBO.AddPartsInfo(Me.State.PartsInfoID)
                            End If
                            objPartsInfo.Delete()
                            Exit For
                        End If
                    Next
                    If AddToArray And Not objPartsInfo Is Nothing Then
                        Me.State.PartsInfoBOIDs.Add(objPI_ID)
                        Me.State.PartsInfoBOs.Add(objPartsInfo)
                    End If


                    'Call the Save() method in the Region Business Object here
                    'Me.State.PartsInfoBO.Save()

                Catch ex As Exception
                    objPartsInfo.RejectChanges()
                    Throw ex
                End Try

                Me.State.PageIndex = Grid.PageIndex

                'Set the IsAfterSave flag to TRUE so that the Paging logic gets invoked
                Me.State.IsAfterSave = True

                'drop the row
                Me.State.PartsInfoDV.Table.Rows.RemoveAt((Grid.PageIndex * 10) + index)

                PopulateGrid()
                Me.State.PageIndex = Grid.PageIndex

                'update the part amount in the claim auth detail bo
                Me.UpdatePartsAmountInClaimAuthDetailBO()
                Me.PopulateADFormFromBO()
                Me.ToggleMainButtons(True)
            ElseIf ((e.CommandName = Me.SORT_COMMAND) AndAlso Not (Me.IsEditing)) Then

                Dim rowIdx As GridViewRow = CType(CType(e.CommandSource, Control).Parent.Parent, GridViewRow)
                index = rowIdx.RowIndex
                Me.State.LastRowIndexInUse = index

                Grid.DataMember = e.CommandArgument.ToString
                PopulateGrid()
            End If
            Me.State.HasDataChanged = True
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrController)
        End Try

    End Sub

    Protected Sub ItemBound(ByVal source As Object, ByVal e As GridViewRowEventArgs) Handles Grid.RowDataBound
        Try
            BaseItemBound(source, e)

            Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
            Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
            If Not dvRow Is Nothing And Not Me.State.bnoRow Then
                If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then
                    Me.PopulateControlFromBOProperty(e.Row.Cells(Me.ID_COL), dvRow(PartsInfo.PartsInfoDV.COL_NAME_PARTS_INFO_ID))
                    If itemType = ListItemType.EditItem Or Me.State.IsNewMode_PI = True Or Me.State.IsEditMode_PI = True Then
                        If (Not e.Row.Cells(Me.IN_STOCK_ID_COL).FindControl(Me.IN_STOCK_ID_CONTROL_NAME) Is Nothing) Then
                            Dim YESNOdv As DataView = LookupListNew.DropdownLookupList(YESNO, ElitaPlusIdentity.Current.ActiveUser.LanguageId, True)
                            Dim dropdownInStock As DropDownList = CType(e.Row.Cells(Me.IN_STOCK_ID_COL).FindControl(Me.IN_STOCK_ID_CONTROL_NAME), DropDownList)

                            Dim YESNOList As ListItem() = CommonConfigManager.Current.ListManager.GetList("YESNO", Thread.CurrentPrincipal.GetLanguageCode())
                            dropdownInStock.Populate(YESNOList, New PopulateOptions() With
                                              {
                                                .AddBlankItem = True,
                                                .SortFunc = AddressOf PopulateOptions.GetCode
                                               })

                            Dim drv As DataRowView = CType(e.Row.DataItem, DataRowView)
                            If Not drv(DALObjects.PartsInfoDAL.COL_NAME_IN_STOCK_ID) Is DBNull.Value Then
                                Me.SetSelectedItem(dropdownInStock, New Guid(CType(dvRow(PartsInfo.PartsInfoDV.COL_NAME_IN_STOCK_ID), Byte())))
                            End If

                        End If
                    Else
                        Me.PopulateControlFromBOProperty(e.Row.Cells(Me.DESCRIPTION_COL), dvRow(PartsInfo.PartsInfoDV.COL_NAME_PARTS_DESCRIPTION))
                        Me.PopulateControlFromBOProperty(e.Row.Cells(Me.COST_COL), dvRow(PartsInfo.PartsInfoDV.COL_NAME_COST))
                        If (Not e.Row.Cells(Me.IN_STOCK_ID_COL).FindControl(Me.IN_STOCK_ID_CONTROL_NAME) Is Nothing) Then
                            DisableControl(e.Row.Cells(Me.IN_STOCK_ID_COL).FindControl(Me.IN_STOCK_ID_CONTROL_NAME))
                        End If

                        If (Not e.Row.Cells(Me.PART_DESCRIPTION_COL).FindControl(Me.DESCRIPTION_CONTROL_NAME) Is Nothing) Then
                            DisableControl(e.Row.Cells(Me.PART_DESCRIPTION_COL).FindControl(Me.DESCRIPTION_CONTROL_NAME), True)
                        End If

                        If (Not e.Row.Cells(Me.ID_COL).FindControl(Me.DELETEBUTTON_WRITE_CONTROL_NAME) Is Nothing) Then
                            DisableControl(e.Row.Cells(Me.ID_COL).FindControl(Me.DELETEBUTTON_WRITE_CONTROL_NAME), True)
                        End If
                    End If


                End If

                If itemType = ListItemType.EditItem Then

                End If
            End If

        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrController)
        End Try
    End Sub

    Protected Sub ItemCreated(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
        Try
            BaseItemCreated(sender, e)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrController)
        End Try
    End Sub

    Protected Sub BindBoPropertiesToGridHeaders(ByVal objPartsInfoBO As PartsInfo)
        Me.BindBOPropertyToGridHeader(objPartsInfoBO, "DescriptionId", Me.Grid.Columns(Me.DESCRIPTION_COL))
        Me.BindBOPropertyToGridHeader(objPartsInfoBO, "COST", Me.Grid.Columns(Me.COST_COL))
        'Me.BindBOPropertyToGridHeader(Me.State.Region, "RiskTypeEnglish", Me.Grid.Columns(Me.CODE_COL))
        Me.ClearGridViewHeadersAndLabelsErrSign()
    End Sub

    Private Sub SetFocusOnEditableFieldInGrid(ByVal grid As GridView, ByVal cellPosition As Integer, ByVal controlName As String, ByVal itemIndex As Integer, ByVal newRow As Boolean, ByVal objPartsInfo As PartsInfo)
        'Set focus on the Description TextBox for the EditIndex row
        Dim desc As DropDownList = CType(Me.Grid.Rows(itemIndex).Cells(cellPosition).FindControl(controlName), DropDownList)
        If Not desc Is Nothing Then
            If newRow Then
                Dim dv As DataView = PartsInfo.getAvailList(Me.State.RiskGroupID, Me.State.ClaimBO.Id)
                'update the rowfilter of the dropdown dv
                If Not Me.State.PartsInfoBOs Is Nothing Then
                    For j As Integer = 0 To Me.State.PartsInfoBOs.Count - 1
                        Dim row As DataRow
                        For Each row In dv.Table.Rows
                            Dim partDescID As Guid = New Guid(CType(row(DV_ID_COL), Byte()))
                            Try
                                If partDescID.Equals(CType(Me.State.PartsInfoBOs(j), PartsInfo).PartsDescriptionId) Then
                                    dv.Table.Rows.Remove(row)
                                    Exit For
                                End If
                            Catch ex As BOInvalidOperationException
                                'do noting
                                j += 1
                            End Try

                            If j = Me.State.PartsInfoBOs.Count - 1 Then Exit For
                        Next
                        If j = Me.State.PartsInfoBOs.Count - 1 Then Exit For
                    Next
                End If
                Me.BindListControlToDataView(desc, dv, LookupListNew.COL_DESCRIPTION_NAME, LookupListNew.COL_ID_NAME, False)
            Else
                Dim oListContext As New Assurant.Elita.CommonConfiguration.ListContext
                oListContext.CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
                oListContext.ClaimId = Me.State.ClaimBO.Id
                oListContext.RiskGroupId = Me.State.RiskGroupID
                oListContext.PartsDescriptionId = objPartsInfo.PartsDescriptionId
                Dim partsInfoList As Assurant.Elita.CommonConfiguration.DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList("CurrentPartInfoByClaim", Thread.CurrentPrincipal.GetLanguageCode(), oListContext)
                desc.Populate(partsInfoList, New PopulateOptions() With
                                               {
                                               .AddBlankItem = True
                                               })
            End If

            SetFocus(desc)
        End If
    End Sub

#End Region

#Region "Button Click Handlers"

    Private Sub btnNew_PI_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew_PI_WRITE.Click

        Try
            Me.State.IsDone = False
            Me.State.IsEditable_AD = False
            Dim desc As DropDownList = New DropDownList

            Dim oListContext As New Assurant.Elita.CommonConfiguration.ListContext
            oListContext.CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
            oListContext.ClaimId = Me.State.ClaimBO.Id
            oListContext.RiskGroupId = Me.State.RiskGroupID
            Dim partsInfoList As ListItem() = CommonConfigManager.Current.ListManager.GetList("PartInfoByClaim", Thread.CurrentPrincipal.GetLanguageCode(), oListContext)

            desc.Populate(partsInfoList, New PopulateOptions() With
                                           {
                                           .AddBlankItem = True
                                           })


            If Me.State.PartsInfoDV.Count < desc.Items.Count Then
                Me.State.IsEditMode_PI = True
                Me.State.IsNewMode_PI = True
                AddNewPartsInfo()
                Me.ToggleMainButtons(False)
                DisabledTabsList.Add(Tab_AuthDetail)
            Else
                If Me.State.PartsAdded Then
                    Me.DisplayMessage(Message.MSG_NO_MORE_PARTSDESC_FOUND, "", ElitaPlusPage.MSG_BTN_OK, ElitaPlusPage.MSG_TYPE_ALERT)
                Else
                    Me.DisplayMessage(Message.MSG_NO_PARTSDESC_FOUND, "", ElitaPlusPage.MSG_BTN_OK, ElitaPlusPage.MSG_TYPE_ALERT)
                End If
                CancelEditing()
                Me.ToggleMainButtons(True)
            End If


        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrController)
        End Try

    End Sub

    Private Sub btnSave_PI_WRITE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave_PI_WRITE.Click
        Try

            Me.State.HasDataChanged = True
            Me.State.IsDone = True
            Me.State.IsEditable_AD = False
            Dim objPartsInfo As PartsInfo = CType(Me.State.PartsInfoBOs.Item(Me.State.PartsInfoBOs.Count - 1), PartsInfo)
            PopulateBOFromForm(objPartsInfo, Me.State.LastOperation)

            Me.Populate_AD_BOFromForm(False)
            UpdatePartsAmountInClaimAuthDetailBO()
            Me.PopulateADFormFromBO()
            Me.ReturnFromEditing()
            Me.ToggleMainButtons(True)
            Me.State.HasDataBeenSaved = False
            Me.State.HasclaimStatusBOSaved = False

        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrController)
        End Try
    End Sub

    Private Sub BtnEdit_AD_WRITE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnEdit_AD_WRITE.Click
        Try
            Me.State.IsDone = True
            Me.State.IsEditMode_AD = True
            Me.State.IsNewMode_AD = False
            Me.State.IsEditable_AD = True
            Me.EnableDisableFields()
            Me.SetButtonsState()
            ControlMgr.SetEnableControl(Me, Me.Cancel_AD_Button, True)
            ControlMgr.SetEnableControl(Me, Me.BtnEdit_AD_WRITE, False)
            ControlMgr.SetEnableControl(Me, Me.btnSave_AD_WRITE, True)
            If IsIntegratedWithGVS() Then
                If (Not Me.State.ClaimStatusBO Is Nothing AndAlso (Me.State.ClaimStatusBO.StatusCode = Codes.CLAIM_EXTENDED_STATUS__WAITING_ON_BUDGET_APPROVAL)) Then
                    ControlMgr.SetVisibleControl(Me, Me.pnlApprove_Disapprove, True)
                    ControlMgr.SetEnableControl(Me, Me.pnlApprove_Disapprove, True)
                End If
            End If
            SetFocusOnFirstEditableControl()
            PopulateGrid()
            Me.ToggleMainButtons(False)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrController)
        End Try
    End Sub

    Private Sub btnNew_AD_WRITE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew_AD_WRITE.Click
        Try
            Me.moAuthDetailTabPanel_WRITE.Visible = True
            Me.State.IsDone = True
            Me.State.IsEditMode_AD = False
            Me.State.IsNewMode_AD = True
            Me.State.IsEditable_AD = True
            Me.EnableDisableFields()
            Me.SetButtonsState()
            ControlMgr.SetEnableControl(Me, Me.Cancel_AD_Button, True)
            ControlMgr.SetEnableControl(Me, Me.btnNew_AD_WRITE, False)
            ControlMgr.SetEnableControl(Me, Me.btnSave_AD_WRITE, True)
            If IsIntegratedWithGVS() Then
                If (Not Me.State.ClaimStatusBO Is Nothing AndAlso (Me.State.ClaimStatusBO.StatusCode = Codes.CLAIM_EXTENDED_STATUS__WAITING_ON_BUDGET_APPROVAL)) Then
                    ControlMgr.SetVisibleControl(Me, Me.pnlApprove_Disapprove, True)
                    ControlMgr.SetEnableControl(Me, Me.pnlApprove_Disapprove, True)
                End If
            End If
            SetFocusOnFirstEditableControl()
            PopulateGrid()
            Me.ToggleMainButtons(False)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrController)
        End Try
    End Sub

    Private Sub Cancel_AD_Button_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Cancel_AD_Button.Click
        Try
            Me.State.IsEditable_AD = False
            PopulateClaimAuthDetailTab()
            Me.EnableDisableFields()
            Me.SetButtonsState()
            PopulateGrid()
            If Me.State.IsReplacementClaim Then
                DisabledTabsList.Add(Tab_PartsInfo)
            End If
            Me.ToggleMainButtons(True)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrController)
        End Try
    End Sub

    Private Sub btnSave_AD_WRITE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave_AD_WRITE.Click
        Try
            Me.State.HasDataChanged = True
            Me.State.IsDone = True
            Me.State.IsEditable_AD = False
            Me.Populate_AD_BOFromForm(False)

            Me.ReturnFromEditingAD()
            Me.State.HasDataBeenSaved = False
            Me.State.HasclaimStatusBOSaved = False
            If Me.State.IsReplacementClaim Then
                DisabledTabsList.Add(Tab_PartsInfo)
            End If
            Me.ToggleMainButtons(True)

        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrController)
        End Try
    End Sub

    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click

        Try

            UpdatePartsAmountInClaimAuthDetailBO()
            Populate_AD_BOFromForm(False)
            PopulateApprove_DisApproveClaimStatus()

            Dim IsBODirty As Boolean = False
            Dim IsNewClaimStatusBODirty As Boolean = False

            If Not Me.State.PartsInfoBOs Is Nothing Then
                Dim objPartsInfo As PartsInfo
                For i As Integer = 0 To Me.State.PartsInfoBOs.Count - 1
                    objPartsInfo = CType(Me.State.PartsInfoBOs.Item(i), PartsInfo)
                    If objPartsInfo.IsDeleted Then
                        IsBODirty = True
                        Exit For
                    ElseIf objPartsInfo.IsDirty Then
                        IsBODirty = True
                        Exit For
                    End If
                Next
            End If

            If Not IsBODirty AndAlso Not Me.State.ClaimAuthDetailBO Is Nothing Then
                If Me.State.ClaimAuthDetailBO.IsDeleted Then
                    IsBODirty = True
                Else
                    IsBODirty = Me.GetDirtyStatus 'Me.State.ClaimAuthDetailBO.IsDirty And Me.GetSubTotal > 0
                    hdnSelectedTab.Value = Tab_AuthDetail
                End If
            End If

            If Not Me.State.NewClaimStatusBO Is Nothing Then
                IsNewClaimStatusBODirty = Me.State.NewClaimStatusBO.IsDirty
            End If

            If (Not Me.NavController Is Nothing) Then
                If Me.NavController.Context = "PAY_CLAIM_DETAIL-AUTH_DETAIL" Or Me.NavController.Context = "PAY_CLAIM_ADJUSTMENT-PAY_CLAIM_DETAIL_CLADJ-AUTH_DETAIL" Then
                    If IsBODirty AndAlso Not Me.State.HasDataBeenSaved Then
                        PopulateGrid()
                        Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", Me.MSG_BTN_YES_NO_CANCEL, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                        Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
                    Else
                        'Me.ReturnToCallingPage(New ReturnType(Me.State.ClaimBO, Me.State.ClaimAuthDetailBO, Me.State.PartsInfoDV, IsBODirty Or Me.State.HasDataBeenSaved, IsNewClaimStatusBODirty))
                        Me.NavController.Navigate(Me, "back", BuildPayClaimFormParameters(IsBODirty))
                    End If
                Else
                    'Me.ReturnToCallingPage(New ReturnType(Me.State.ClaimBO, Me.State.ClaimAuthDetailBO, Me.State.PartsInfoDV, IsBODirty, IsNewClaimStatusBODirty))
                    Me.NavController.Navigate(Me, "back", BuildClaimFormParameters(IsBODirty Or Me.State.HasDataBeenSaved, IsNewClaimStatusBODirty))
                End If
            End If

        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrController)
            Me.DisplayMessage(Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
            Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.BackOnErr
            Me.State.LastErrMsg = Me.ErrController.Text
        End Try
    End Sub

    Function BuildClaimFormParameters(ByVal IsBODirty As Boolean, ByVal IsClaimStatusBODirty As Boolean) As ClaimForm.Parameters
        Return New ClaimForm.Parameters(Me.State.ClaimAuthDetailBO.ClaimId, New ReturnType(Me.State.ClaimBO, Me.State.ClaimAuthDetailBO, Me.State.PartsInfoDV, IsBODirty, IsClaimStatusBODirty))
    End Function

    Function BuildPayClaimFormParameters(ByVal IsBODirty As Boolean) As PayClaimForm.Parameters
        Return New PayClaimForm.Parameters(Me.State.ClaimAuthDetailBO.ClaimId, IsBODirty, False)
    End Function

    Protected Sub btnSave_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave_WRITE.Click
        Try
            Me.DoSave()
            Me.PopulateBOProperty(Me.State.ClaimBO, "AuthorizedAmount", txtTotal)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrController)
        End Try
    End Sub

    Private Sub BtnUndo_Write_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUndo_Write.Click
        Try
            UndoChanges()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrController)
        End Try
    End Sub

    Private Sub CancelEditing()
        Try
            Me.Grid.SelectedIndex = Me.NO_ITEM_SELECTED_INDEX
            If Me.State.IsNewMode_PI Then
                'drop the row and the bo from the arraylist
                Me.State.PartsInfoDV.Table.Rows.RemoveAt(Me.State.LastRowIndexInUse)
                Me.State.PartsInfoBOs.RemoveAt(Me.State.PartsInfoBOs.Count - 1)
            End If
            ReturnFromEditing()

        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrController)
        End Try
    End Sub

    Private Sub UndoChanges()
        Try
            Me.State.IsEditable_AD = False
            Me.Grid.SelectedIndex = Me.NO_ITEM_SELECTED_INDEX

            'refresh the claim BO
            Me.State.ClaimBO = ClaimFacade.Instance.GetClaim(Of Claim)(Me.State.ClaimBO.Id)

            'refresh the ClaimAuthDetailBO
            Me.State.ClaimAuthDetailBO = Nothing
            Me.PopulateClaimAuthDetailTab()
            Me.PopulateADFormFromBO()

            'refresh the parts info grid and arraylist
            Me.State.PartsInfoDV.Table = Me.State.OreginalPartsInfoTable.Copy
            Me.State.PartsInfoBOs = Nothing
            Me.State.HasDataChanged = False
            Me.State.HasclaimStatusBOChanged = False
            ReturnFromEditing()

            If Me.State.IsReplacementClaim Then
                DisabledTabsList.Add(Tab_PartsInfo)
            End If

            Me.EnableDisableFields()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrController)
        End Try
    End Sub

    Public Function IsIntegratedWithGVS() As Boolean
        Dim retVal As Boolean = False

        'If Me.State.ClaimBO.ServiceCenterObject.IntegratedWithGVS AndAlso Not Me.State.ClaimBO.ServiceCenterObject.IntegratedAsOf Is Nothing AndAlso Me.State.ClaimBO.CreatedDateTime.Value >= Me.State.ClaimBO.ServiceCenterObject.IntegratedAsOf.Value Then
        If Not Me.State.ClaimBO Is Nothing AndAlso Not Me.State.ClaimBO.ServiceCenterObject Is Nothing Then
            If Me.State.ClaimBO.ServiceCenterObject.IntegratedWithGVS AndAlso Not Me.State.ClaimBO.ServiceCenterObject.IntegratedAsOf Is Nothing AndAlso Me.State.ClaimBO.CreatedDateTime.Value >= Me.State.ClaimBO.ServiceCenterObject.IntegratedAsOf.Value Then
                retVal = True
            Else
                retVal = False
            End If
        Else
            retVal = False
        End If


        Return retVal
    End Function

#End Region

    Private Sub Cancel_PI_Button_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Cancel_PI_Button.Click
        Try
            CancelEditing()
            Me.ToggleMainButtons(True)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrController)
        End Try
    End Sub

#Region "Controlling Logic"

    <System.Web.Services.WebMethod()>
    <Script.Services.ScriptMethod()>
    Public Shared Function SetClientCultureFormat(ByVal txtBoxId As String, ByVal value As String,
        ByVal laborVal As String, ByVal partsVal As String, ByVal svcChargeVal As String,
        ByVal tripAmtVal As String, ByVal otherAmtVal As String, ByVal shippingAmtVal As String,
        ByVal diagnosticsAmtVal As String, ByVal dispositionAmtVal As String, ByVal totalTaxAmt As String) As String

        Dim subTotal As Double
        Dim totalAmt As Double
        Dim total_tax As Double = CType(totalTaxAmt, Double)
        Dim retStr As String

        subTotal = CType(laborVal, Double) + CType(partsVal, Double) + CType(svcChargeVal, Double) + CType(tripAmtVal, Double) + CType(otherAmtVal, Double) + CType(shippingAmtVal, Double) + CType(diagnosticsAmtVal, Double) + CType(dispositionAmtVal, Double)
        totalAmt = subTotal + total_tax

        retStr = txtBoxId & "|" & MiscUtil.GetAmountFormattedDoubleString(value) & "|" & MiscUtil.GetAmountFormattedDoubleString(CType(subTotal, String)) & "|" & MiscUtil.GetAmountFormattedDoubleString(CType(totalAmt, String)) & "|" & MiscUtil.GetAmountFormattedDoubleString(CType(total_tax, String))

        Return retStr

    End Function

    Protected Sub CheckIfComingFromSaveConfirm()
        Dim confResponse As String = Me.HiddenSaveChangesPromptResponse.Value
        If Not confResponse Is Nothing AndAlso confResponse = Me.MSG_VALUE_YES Then
            If Me.State.ActionInProgress <> ElitaPlusPage.DetailPageCommand.BackOnErr Then
                Me.DoSave(True)
            End If

            Select Case Me.State.ActionInProgress
                Case ElitaPlusPage.DetailPageCommand.Back
                    'DEF-17426
                    'Me.ReturnToCallingPage(New ReturnType(Me.State.ClaimBO, Me.State.ClaimAuthDetailBO, Me.State.PartsInfoDV, Me.State.HasDataChanged Or Me.State.HasDataBeenSaved, Me.State.HasclaimStatusBOChanged Or Me.State.HasclaimStatusBOSaved))
                    If Me.NavController.Context = "PAY_CLAIM_DETAIL-AUTH_DETAIL" Or Me.NavController.Context = "PAY_CLAIM_ADJUSTMENT-PAY_CLAIM_DETAIL_CLADJ-AUTH_DETAIL" Then
                        Me.NavController.Navigate(Me, "back", BuildPayClaimFormParameters(Me.State.HasDataChanged Or Me.State.HasDataBeenSaved))
                    Else
                        Me.NavController.Navigate(Me, "back", BuildClaimFormParameters(Me.State.HasDataChanged Or Me.State.HasDataBeenSaved, Me.State.NewClaimStatusBO.IsDirty))
                    End If
                Case ElitaPlusPage.DetailPageCommand.BackOnErr
                    'DEF-17426
                    'Me.ReturnToCallingPage(New ReturnType(Me.State.ClaimBO, Me.State.ClaimAuthDetailBO, Me.State.PartsInfoDV, Me.State.HasDataChanged Or Me.State.HasDataBeenSaved, Me.State.HasclaimStatusBOChanged Or Me.State.HasclaimStatusBOSaved))
                    If Me.NavController.Context = "PAY_CLAIM_DETAIL-AUTH_DETAIL" Or Me.NavController.Context = "PAY_CLAIM_ADJUSTMENT-PAY_CLAIM_DETAIL_CLADJ-AUTH_DETAIL" Then
                        Me.NavController.Navigate(Me, "back", BuildPayClaimFormParameters(Me.State.HasDataChanged Or Me.State.HasDataBeenSaved))
                    Else
                        Me.NavController.Navigate(Me, "back", BuildClaimFormParameters(Me.State.HasDataChanged Or Me.State.HasDataBeenSaved, Me.State.NewClaimStatusBO.IsDirty))
                    End If
            End Select
        ElseIf Not confResponse Is Nothing AndAlso confResponse = Me.MSG_VALUE_NO Then
            Select Case Me.State.ActionInProgress
                Case ElitaPlusPage.DetailPageCommand.Back
                    'DEF-17426
                    'Me.ReturnToCallingPage(New ReturnType(Me.State.ClaimBO, Me.State.ClaimAuthDetailBO, Me.State.PartsInfoDV, Me.State.HasDataChanged Or Me.State.HasDataBeenSaved, Me.State.HasclaimStatusBOChanged Or Me.State.HasclaimStatusBOSaved))
                    If Me.NavController.Context = "PAY_CLAIM_DETAIL-AUTH_DETAIL" Or Me.NavController.Context = "PAY_CLAIM_ADJUSTMENT-PAY_CLAIM_DETAIL_CLADJ-AUTH_DETAIL" Then
                        Me.NavController.Navigate(Me, "back", BuildPayClaimFormParameters(Me.State.HasDataChanged Or Me.State.HasDataBeenSaved))
                    Else
                        Me.NavController.Navigate(Me, "back", BuildClaimFormParameters(Me.State.HasDataChanged Or Me.State.HasDataBeenSaved, Me.State.NewClaimStatusBO.IsDirty))
                    End If
                Case ElitaPlusPage.DetailPageCommand.BackOnErr
                    'DEF-17426
                    'Me.ReturnToCallingPage(New ReturnType(Me.State.ClaimBO, Me.State.ClaimAuthDetailBO, Me.State.PartsInfoDV, Me.State.HasDataChanged Or Me.State.HasDataBeenSaved, Me.State.HasclaimStatusBOChanged Or Me.State.HasclaimStatusBOSaved))
                    If Me.NavController.Context = "PAY_CLAIM_DETAIL-AUTH_DETAIL" Or Me.NavController.Context = "PAY_CLAIM_ADJUSTMENT-PAY_CLAIM_DETAIL_CLADJ-AUTH_DETAIL" Then
                        Me.NavController.Navigate(Me, "back", BuildPayClaimFormParameters(Me.State.HasDataChanged Or Me.State.HasDataBeenSaved))
                    Else
                        Me.NavController.Navigate(Me, "back", BuildClaimFormParameters(Me.State.HasDataChanged Or Me.State.HasDataBeenSaved, Me.State.NewClaimStatusBO.IsDirty))
                    End If
            End Select
        End If
        'Clean after consuming the action
        Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
        Me.HiddenSaveChangesPromptResponse.Value = ""

    End Sub

    Private Sub DoSave(Optional ByVal blnComingFromBack As Boolean = False)
        Try
            Me.State.IsEditable_AD = False
            If Not Me.State.PartsInfoBOs Is Nothing AndAlso Me.State.PartsInfoBOs.Count > 0 AndAlso Not Me.State.IsDone Then
                Dim objPartsInfo As PartsInfo = CType(Me.State.PartsInfoBOs.Item(Me.State.PartsInfoBOs.Count - 1), PartsInfo)
                PopulateBOFromForm(objPartsInfo, Me.State.LastOperation)
            End If


            UpdatePartsAmountInClaimAuthDetailBO()
            Populate_AD_BOFromForm(False)

            PopulateApprove_DisApproveClaimStatus()

            Dim IsBODirty As Boolean = False
            If Not Me.State.PartsInfoBOs Is Nothing Then
                For i As Integer = 0 To Me.State.PartsInfoBOs.Count - 1
                    Dim objPartsInfo As PartsInfo = CType(Me.State.PartsInfoBOs.Item(i), PartsInfo)
                    If (objPartsInfo.IsDirty) Then
                        IsBODirty = True
                        If Not objPartsInfo.IsDeleted Then objPartsInfo.Validate()
                    End If
                Next
            End If

            If Not IsBODirty Then
                IsBODirty = Me.State.ClaimAuthDetailBO.IsDirty
            End If

            If Not IsBODirty AndAlso Not Me.State.NewClaimStatusBO Is Nothing Then
                IsBODirty = Me.State.NewClaimStatusBO.IsDirty
            End If

            If (IsBODirty) Then
                If GetSubTotal() <= 0 Then
                    Me.ResetClaimAuthDetailBOToOriginal()
                    Me.State.ClaimAuthDetailBO.TempClaimId = Me.State.ClaimAuthDetailBO.ClaimId
                    'Me.State.ClaimAuthDetailBO.BeginEdit()
                    Me.State.ClaimAuthDetailBO.Delete()

                End If

                Me.State.ClaimAuthDetailBO.Save()
                Me.State.IsAfterSave = True
                Me.State.HasDataChanged = True
                Me.State.HasDataBeenSaved = True
                Me.State.HasclaimStatusBOSaved = True
                Me.DisplayMessage(Message.RECORD_ADDED_OK, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)

            Else
                Me.DisplayMessage(Message.MSG_RECORD_NOT_SAVED, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
            End If

            Me.State.IsDone = False

            If Not blnComingFromBack Then Me.ReturnFromEditing()

        Catch ex As Exception
            'Me.State.ClaimAuthDetailBO.cancelEdit()
            Throw ex
        End Try
    End Sub

    Private Sub ResetClaimAuthDetailBOToOriginal()
        With Me.State.ClaimAuthDetailBO
            If Not .LaborAmount Is Nothing Then .LaborAmount = Me.State.OriginalClaimAuthDetailBO.LaborAmount.Value
            If Not .PartAmount Is Nothing Then .PartAmount = Me.State.OriginalClaimAuthDetailBO.PartAmount.Value
            If Not .ServiceCharge Is Nothing Then .ServiceCharge = Me.State.OriginalClaimAuthDetailBO.ServiceCharge.Value
            If Not .TripAmount Is Nothing Then .TripAmount = Me.State.OriginalClaimAuthDetailBO.TripAmount.Value
            If Not .ShippingAmount Is Nothing Then .ShippingAmount = Me.State.OriginalClaimAuthDetailBO.ShippingAmount.Value
            If Not .OtherAmount Is Nothing Then .OtherAmount = Me.State.OriginalClaimAuthDetailBO.OtherAmount.Value
            If Not .OtherExplanation Is Nothing Then .OtherExplanation = Me.State.OriginalClaimAuthDetailBO.OtherExplanation
        End With

    End Sub


#End Region


End Class
