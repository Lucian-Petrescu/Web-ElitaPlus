Option Strict On
Option Explicit On
Imports System.Collections.Generic
Imports System.Diagnostics
Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Imports Assurant.ElitaPlus.Security
Imports Assurant.ElitaPlus.DALObjects
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports Assurant.Elita.Web.Forms
Imports System.Threading
Imports System.Web.Script.Services
Imports System.Web.Services

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
            IsEditing = (Grid.EditIndex > NO_ROW_SELECTED_INDEX)
        End Get
    End Property


    Public ReadOnly Property IsGridInEditMode() As Boolean
        Get
            Return Grid.EditIndex > NO_ITEM_SELECTED_INDEX
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
        Public SortExpression As String = PartsInfo.PartsInfoDV.COL_NAME_PARTS_DESCRIPTION
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
            If NavController.State Is Nothing Then
                NavController.State = New MyState
            End If
            Return CType(NavController.State, MyState)
        End Get
    End Property

#End Region

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As Object

    Private Sub Page_Init(sender As Object, e As EventArgs) Handles MyBase.Init
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
        Public Sub New(ClaimBO As Claim, ClaimAuthDetailBO As ClaimAuthDetail, PartsInfoDV As DataView, hasDataChanged As Boolean, hasClaimStatusBOChanged As Boolean)
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

        Public Sub New(ClaimBO As Claim, ClaimAuthDetailBO As ClaimAuthDetail, PartsInfoDV As DataView)
            Me.ClaimBO = ClaimBO
            Me.ClaimAuthDetailBO = ClaimAuthDetailBO
            Me.PartsInfoDV = PartsInfoDV
        End Sub
    End Class
#End Region

#Region "Private Methods"
    Private Sub Page_PageCall(CallFromUrl As String, CallingPar As Object) Handles MyBase.PageCall
        Try

            If CallingParameters IsNot Nothing Then

                If CallFromUrl.Equals(PAY_CLAIM_FORM_URL) Then
                    State.ClaimBO = CType(CallingParameters, Claim)
                Else
                    Dim objParam As Parameters
                    objParam = CType(CallingParameters, Parameters)
                    State.ClaimBO = objParam.ClaimBO
                    State.ClaimAuthDetailBO = objParam.ClaimAuthDetailBO
                    State.PartsInfoDV = objParam.PartsInfoDV

                End If

                If State.ClaimBO.MethodOfRepairCode = Codes.METHOD_OF_REPAIR_REPLACEMENT OrElse State.ClaimBO.MethodOfRepairCode = Codes.METHOD_OF_REPAIR__REPLACEMENT Then
                    State.ClaimMethodOfRepair = "RPL"
                    LabelTaxType.Text = TranslationBase.TranslateLabelOrMessage("REPLACEMENT")
                Else
                    State.ClaimMethodOfRepair = "RPR"
                    LabelTaxType.Text = TranslationBase.TranslateLabelOrMessage("REPAIR")
                End If

                SetStateProperties()

                'If Me.State.ClaimBO.AuthDetailUsage = PayClaimForm.URL Then 'DEF-17426
                If NavController.Context = "PAY_CLAIM_DETAIL-AUTH_DETAIL" OrElse NavController.Context = "PAY_CLAIM_ADJUSTMENT-PAY_CLAIM_DETAIL_CLADJ-AUTH_DETAIL" Then
                    State.IsCalledByPayClaimForm = True
                Else
                    State.IsCalledByPayClaimForm = False
                End If

                If State.ClaimBO.StatusCode = Codes.CLAIM_STATUS__CLOSED Then
                    State.IsViewOnly = True
                Else
                    State.IsViewOnly = False
                End If

            End If
        Catch ex As Exception
            HandleErrors(ex, ErrController)
        End Try

    End Sub

    Private Sub Page_Load(sender As Object, e As EventArgs) Handles MyBase.Load

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
                MenuEnabled = False
                TextboxClaimNumber.Text = State.ClaimBO.ClaimNumber
                TextboxCustomerName.Text = State.ClaimBO.CustomerName
                TextboxRiskGroup.Text = State.RiskGroup
                SetGridItemStyleColor(Grid)
                ShowMissingTranslations(ErrController)
                State.PageIndex = 0
                State.ClaimStatusBO = ClaimStatus.GetLatestClaimStatus(State.ClaimBO.Id)
                PopulateGrid()
                CheckIfPartsDescriptionAvailable()
                TranslateGridHeader(Grid)
                TranslateGridControls(Grid)
                PopulateClaimAuthDetailTab()
                SetButtonsState(False)
                If State.ClaimBO.ClaimActivityCode = Codes.CLAIM_ACTIVITY__REPLACED OrElse State.ClaimBO.ClaimActivityCode = Codes.CLAIM_ACTIVITY__PENDING_REPLACEMENT Then
                    State.IsReplacementClaim = True
                    DisabledTabsList.Add(Tab_PartsInfo)
                    hdnSelectedTab.Value = Tab_AuthDetail
                End If
            Else
                If State IsNot Nothing AndAlso State.ClaimStatusBO Is Nothing Then
                    State.ClaimStatusBO = ClaimStatus.GetLatestClaimStatus(State.ClaimBO.Id)
                End If
                GetDisabledTabs()
            End If

            'Me.SetStateProperties() 'DEF-17426
            If State.PartsInfoBOs IsNot Nothing AndAlso State.PartsInfoBOs.Count > 0 Then
                For i As Integer = 0 To State.PartsInfoBOs.Count - 1
                    BindBoPropertiesToGridHeaders(CType(State.PartsInfoBOs(i), PartsInfo))
                Next
            End If

            BindADBoPropertiesToLabels()

            'If Me.State.IsCalledByPayClaimForm Then 'DEF-17426
            If NavController.Context = "PAY_CLAIM_DETAIL-AUTH_DETAIL" OrElse NavController.Context = "PAY_CLAIM_ADJUSTMENT-PAY_CLAIM_DETAIL_CLADJ-AUTH_DETAIL" Then
                CheckIfComingFromSaveConfirm()
            End If

        Catch ex As Exception
            HandleErrors(ex, ErrController)
        End Try
    End Sub

    Private Sub CheckIfPartsDescriptionAvailable()
        Dim dv As DataView = PartsInfo.getAvailList(State.RiskGroupID, State.ClaimBO.Id)
        If Not dv.Count > 0 Then
            State.PartsDescriptionAvailable = True
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
        If NavController Is Nothing Then
            Exit Sub
        End If
        If NavController.CurrentNavState.Name = "AUTH_DETAIL" Then
            Dim objParam As Parameters
            objParam = CType(NavController.ParametersPassed, Parameters)
            If objParam.ClaimBO IsNot Nothing Then
                State.ClaimBO = objParam.ClaimBO
                SetStateProperties()
                If State.ClaimBO.MethodOfRepairCode = Codes.METHOD_OF_REPAIR_REPLACEMENT OrElse State.ClaimBO.MethodOfRepairCode = Codes.METHOD_OF_REPAIR__REPLACEMENT Then
                    State.ClaimMethodOfRepair = "RPL"
                    LabelTaxType.Text = TranslationBase.TranslateLabelOrMessage("REPLACEMENT")
                Else
                    State.ClaimMethodOfRepair = "RPR"
                    LabelTaxType.Text = TranslationBase.TranslateLabelOrMessage("REPAIR")
                End If
            End If
        End If
    End Sub

    Protected Sub BindADBoPropertiesToLabels()

        BindBOPropertyToLabel(State.ClaimAuthDetailBO, "LaborAmount", LabelLabor)
        BindBOPropertyToLabel(State.ClaimAuthDetailBO, "PartAmount", LabelParts)
        BindBOPropertyToLabel(State.ClaimAuthDetailBO, "ServiceCharge", LabelSvcCharge)
        BindBOPropertyToLabel(State.ClaimAuthDetailBO, "TripAmount", LabelTripAmount)
        BindBOPropertyToLabel(State.ClaimAuthDetailBO, "ShippingAmount", LabelShippingAmount)
        BindBOPropertyToLabel(State.ClaimAuthDetailBO, "DiagnosticsAmount", LabelDiagnostics)
        BindBOPropertyToLabel(State.ClaimAuthDetailBO, "DispositionAmount", LabelDisposition)
        Dim LabelOtherDesc As New Label
        LabelOtherDesc.Text = TranslationBase.TranslateLabelOrMessage(LABEL_OTHER_AMOUNT)
        BindBOPropertyToLabel(State.ClaimAuthDetailBO, "OtherAmount", LabelOtherDesc)
        ClearGridViewHeadersAndLabelsErrSign()
    End Sub

    Protected Sub DisableControl(ctl As Control, Optional ByVal invisible As Boolean = False, Optional ByVal isShippingField As Boolean = False)
        'If the service center of the claim is integrated with GVS and also the claim status must be one of the following, 
        ' then the parts info tab is editable:
        ' Waiting on Budget Approval - COD
        ' Exception: If Status is “Waiting Documentation”, Elita will allow the Claim Adjuster to edit only “Shipping” field.

        If IsIntegratedWithGVS() Then
            If Not isEditableForGVS(isShippingField) Then
                If invisible Then
                    ControlMgr.SetVisibleControl(Me, CType(ctl, WebControl), False)
                Else
                    ControlMgr.SetEnableControl(Me, CType(ctl, WebControl), False)
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
            If (State.ClaimStatusBO IsNot Nothing AndAlso (State.ClaimStatusBO.StatusCode = Codes.CLAIM_EXTENDED_STATUS__WAITING_ON_BUDGET_APPROVAL)) Then
                retVal = True
            ElseIf (State.ClaimStatusBO IsNot Nothing AndAlso State.ClaimStatusBO.StatusCode = Codes.CLAIM_EXTENDED_STATUS__WAITING_DOCUMENTATION _
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
            If (State.IsAfterSave) Then
                State.IsAfterSave = False
                SetPageAndSelectedIndexFromGuid(dv, State.PartsInfoID, Grid, State.PageIndex)
            ElseIf (State.IsEditMode_PI) Then
                SetPageAndSelectedIndexFromGuid(dv, State.PartsInfoID, Grid, State.PageIndex, State.IsEditMode_PI)
            Else
                SetPageAndSelectedIndexFromGuid(dv, Guid.Empty, Grid, State.PageIndex)
            End If

            TranslateGridControls(Grid)

            Grid.DataSource = dv
            Grid.DataBind()

            State.PartsAdded = (dv.Count > 0)

            'Me.PopulateControlFromBOProperty(Me.TextTotalCost, PartsInfo.getTotalCost(Me.State.ClaimBO.Id).Value)
            Dim totalpartsCost As DecimalType = GetTotalCost()
            PopulateControlFromBOProperty(TextTotalCost, totalpartsCost)
            If totalpartsCost.Value > 0 Then
                PopulateControlFromBOProperty(TextPartsTax1, ComputePartsTaxes(totalpartsCost.Value))
            Else
                PopulateControlFromBOProperty(TextPartsTax1, "0.00")
            End If


            'CreatePartsInfoArrayList(dv)

            If State.PartsInfoDV.Count = 0 Then
                Dim dt As DataTable = State.PartsInfoDV.Table.Clone()
                Dim dvEmpty As New PartsInfo.PartsInfoDV(dt)
                Dim objEmpty As PartsInfo = New PartsInfo
                PartsInfo.AddNewRowToPartsInfoSearchDV(dvEmpty, objEmpty)
                SetPageAndSelectedIndexFromGuid(dvEmpty, State.PartsInfoID, Grid, State.PageIndex, State.IsEditMode_PI)
                SortAndBindGrid(dvEmpty, True)
            Else
                SetPageAndSelectedIndexFromGuid(State.PartsInfoDV, State.PartsInfoID, Grid, State.PageIndex, State.IsEditMode_PI)
                SortAndBindGrid(State.PartsInfoDV)
            End If

            If State.PartsInfoDV.Count = 0 Then
                For Each gvRow As GridViewRow In Grid.Rows
                    gvRow.Visible = False
                    gvRow.Controls.Clear()
                Next
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrController)
        End Try

    End Sub

    Private Sub SortAndBindGrid(dvBinding As DataView, Optional ByVal blnEmptyList As Boolean = False)
        Grid.DataSource = dvBinding
        HighLightSortColumn(Grid, State.SortExpression)
        Grid.DataBind()
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
        For index = 0 To State.PartsInfoDV.Count - 1
            totalCost = totalCost + CType(State.PartsInfoDV.Item(index).Item(VIEW_COST_COL), Decimal)
        Next
        Return New DecimalType(totalCost)
    End Function

    Private Function GetSubTotal() As Decimal
        Dim subTotal As Decimal = 0
        If State.ClaimAuthDetailBO IsNot Nothing Then
            With State.ClaimAuthDetailBO
                If .LaborAmount IsNot Nothing Then subTotal += .LaborAmount.Value
                If .PartAmount IsNot Nothing Then subTotal += .PartAmount.Value
                If .ServiceCharge IsNot Nothing Then subTotal += .ServiceCharge.Value
                If .TripAmount IsNot Nothing Then subTotal += .TripAmount.Value
                If .ShippingAmount IsNot Nothing Then subTotal += .ShippingAmount.Value
                If .OtherAmount IsNot Nothing Then subTotal += .OtherAmount.Value
                If .DiagnosticsAmount IsNot Nothing Then subTotal += .DiagnosticsAmount.Value
                If .DispositionAmount IsNot Nothing Then subTotal += .DispositionAmount.Value
            End With
        End If
        Return subTotal
    End Function

    Private Sub PopulateClaimAuthDetailTab()
        ' The record may/may not exist.
        Try
            If State.ClaimAuthDetailBO Is Nothing Then
                'If Me.State.IsCalledByPayClaimForm Then 'DEF-17426
                If NavController.Context = "PAY_CLAIM_DETAIL-AUTH_DETAIL" OrElse NavController.Context = "PAY_CLAIM_ADJUSTMENT-PAY_CLAIM_DETAIL_CLADJ-AUTH_DETAIL" Then
                    State.ClaimAuthDetailBO = New ClaimAuthDetail(State.ClaimBO.Id, True)
                Else
                    State.ClaimAuthDetailBO = State.ClaimBO.AddClaimAuthDetail(State.ClaimBO.Id, True, True) '= New ClaimAuthDetail(Me.State.ClaimBO.Id, True)
                End If
                State.OriginalClaimAuthDetailBO = New ClaimAuthDetail
                State.OriginalClaimAuthDetailBO.Clone(State.ClaimAuthDetailBO)
            End If
            State.IsEditMode_AD = True
            State.IsNewMode_AD = False
            PopulateADFormFromBO()
        Catch ex As DataNotFoundException
            LoadClaimTaxRates(True)
            State.IsNewMode_AD = True
            State.IsEditMode_AD = False
            'If Me.State.IsCalledByPayClaimForm Then 'DEF-17426
            If NavController.Context = "PAY_CLAIM_DETAIL-AUTH_DETAIL" OrElse NavController.Context = "PAY_CLAIM_ADJUSTMENT-PAY_CLAIM_DETAIL_CLADJ-AUTH_DETAIL" Then
                State.ClaimAuthDetailBO = New ClaimAuthDetail()
            Else
                State.ClaimAuthDetailBO = State.ClaimBO.AddClaimAuthDetail(Guid.Empty) 'New ClaimAuthDetail()
            End If
            State.OriginalClaimAuthDetailBO = New ClaimAuthDetail
            State.OriginalClaimAuthDetailBO.Clone(State.ClaimAuthDetailBO)
            State.NoClaimAuthDetailBOFound = True
            State.ClaimAuthDetailBO.PartAmount = GetTotalCost()
            If State.ClaimAuthDetailBO.PartAmount.Value > 0 Then
                State.HasDataChanged = False
                State.HasclaimStatusBOChanged = False
                PopulateADFormFromBO()
            Else
                moAuthDetailTabPanel_WRITE.Visible = False
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrController)
        End Try

    End Sub

    Private Function GetDV() As DataView

        Dim dv As DataView

        dv = GetGridDataView()
        dv.Sort = Grid.DataMember()

        Return (dv)

    End Function

    Private Function GetGridDataView() As DataView
        If State.PartsInfoDV Is Nothing Then
            State.PartsInfoDV = PartsInfo.getSelectedList(State.ClaimBO.Id)
        End If
        State.OreginalPartsInfoTable = State.PartsInfoDV.Table.Copy
        State.OreginalPartsInfoDataViewFilter = State.PartsInfoDV.RowFilter
        Return State.PartsInfoDV
    End Function

    Private Sub SetStateProperties()
        State.CompanyId = State.ClaimBO.CompanyId
        Dim riskTypeBO As RiskType = New RiskType(State.ClaimBO.RiskTypeId)
        State.RiskGroupID = riskTypeBO.RiskGroupId
        State.RiskGroup = LookupListNew.GetDescriptionFromId(LookupListNew.GetRiskGroupsLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), State.RiskGroupID)
    End Sub

    Private Sub AddNewPartsInfo()

        Dim dv As DataView

        Try
            dv = GetGridDataView()

            Dim objPartsInfo As PartsInfo

            'If Me.State.IsCalledByPayClaimForm Then 'DEF-17426
            If NavController.Context = "PAY_CLAIM_DETAIL-AUTH_DETAIL" OrElse NavController.Context = "PAY_CLAIM_ADJUSTMENT-PAY_CLAIM_DETAIL_CLADJ-AUTH_DETAIL" Then
                objPartsInfo = State.ClaimAuthDetailBO.AddPartsInfo(Guid.Empty)
            Else
                objPartsInfo = State.ClaimBO.AddPartsInfo(Guid.Empty) 'Me.State.ClaimAuthDetailBO.AddPartsInfo(Guid.Empty)
            End If

            State.PartsInfoID = objPartsInfo.Id
            objPartsInfo.ClaimId = State.ClaimBO.Id

            'add the new BO to arraylist
            If State.PartsInfoBOs Is Nothing Then
                State.PartsInfoBOs = New ArrayList
                State.PartsInfoBOIDs = New ArrayList
            End If
            State.PartsInfoBOs.Add(objPartsInfo)
            State.PartsInfoBOIDs.Add(objPartsInfo.Id)

            dv = objPartsInfo.GetNewDataViewRow(dv, State.PartsInfoID, State.ClaimBO.Id)

            State.PartsInfoDV = dv
            State.LastRowIndexInUse = dv.Count - 1

            Grid.DataSource = dv

            SetPageAndSelectedIndexFromGuid(dv, State.PartsInfoID, Grid, State.PageIndex, State.IsEditMode_PI)

            Grid.DataBind()

            State.PageIndex = Grid.PageIndex
            State.LastOperation = NEW_COMMAND

            SetGridControls(Grid, False)

            'Set focus on the Description TextBox for the EditIndex row
            SetFocusOnEditableFieldInGrid(Grid, DESCRIPTION_COL, DESCRIPTION_CONTROL_NAME, Grid.EditIndex, True, objPartsInfo)

            PopulateFormFromBO(objPartsInfo)

            TranslateGridControls(Grid)
            SetButtonsState()

            'Update the dropdown row filter arraylist
            'If Me.State.DropDownRowFilterArrayList Is Nothing Then Me.State.DropDownRowFilterArrayList = New ArrayList
            'Me.State.DropDownRowFilterArrayList.Add(objPartsInfo.PartsDescriptionId)

        Catch ex As Exception
            HandleErrors(ex, ErrController)
        End Try
    End Sub

    Private Sub PopulateBOFromForm(objPartsInfo As PartsInfo, command As String)


        If Not command.Equals(DELETE_COMMAND) Then
            With objPartsInfo
                .PartsDescriptionId = New Guid(CType(Grid.Rows(Grid.EditIndex).Cells(DESCRIPTION_COL).FindControl(DESCRIPTION_CONTROL_NAME), DropDownList).SelectedValue)
                .InStockID = New Guid(CType(Grid.Rows(Grid.EditIndex).Cells(IN_STOCK_ID_COL).FindControl(IN_STOCK_ID_CONTROL_NAME), DropDownList).SelectedValue)
                Try
                    .Cost = New DecimalType(CType(CType(Grid.Rows(Grid.EditIndex).Cells(COST_COL).FindControl(COST_CONTROL_NAME), TextBox).Text, Decimal))
                Catch
                    .Cost = Nothing
                End Try

                State.PartsInfoDV.Table.Rows(State.LastRowIndexInUse)(PartsInfo.PartsInfoDV.COL_NAME_PARTS_INFO_ID) = .Id.ToByteArray
                State.PartsInfoDV.Table.Rows(State.LastRowIndexInUse)(PartsInfo.PartsInfoDV.COL_NAME_PARTS_DESCRIPTION_ID) = .PartsDescriptionId.ToByteArray
                State.PartsInfoDV.Table.Rows(State.LastRowIndexInUse)(PartsInfo.PartsInfoDV.COL_NAME_PARTS_DESCRIPTION) = CType(Grid.Rows(Grid.EditIndex).Cells(PART_DESCRIPTION_COL).FindControl(DESCRIPTION_CONTROL_NAME), DropDownList).SelectedItem.Text
                State.PartsInfoDV.Table.Rows(State.LastRowIndexInUse)(PartsInfo.PartsInfoDV.COL_NAME_IN_STOCK_DESCRIPTION) = CType(Grid.Rows(Grid.EditIndex).Cells(IN_STOCK_ID_COL).FindControl(IN_STOCK_ID_CONTROL_NAME), DropDownList).SelectedItem.Text
                State.PartsInfoDV.Table.Rows(State.LastRowIndexInUse)(PartsInfo.PartsInfoDV.COL_NAME_CLAIM_ID) = .ClaimId.ToByteArray
                Dim cost As Decimal = 0
                If .Cost IsNot Nothing Then
                    cost = .Cost.Value
                End If
                State.PartsInfoDV.Table.Rows(State.LastRowIndexInUse)(PartsInfo.PartsInfoDV.COL_NAME_COST) = cost.ToString
            End With
            objPartsInfo.Validate()
            If ErrCollection.Count > 0 Then
                Throw New PopulateBOErrorException
            End If
        End If


    End Sub

    Private Sub Populate_AD_BOFromForm(Optional ByVal validateBO As Boolean = True)

        'Me.ClearADZeroAmounts()

        If State.ClaimAuthDetailBO IsNot Nothing AndAlso Not State.ClaimAuthDetailBO.IsDeleted Then
            PopulateBOProperty(State.ClaimAuthDetailBO, "ClaimId", State.ClaimBO.Id)
            PopulateBOProperty(State.ClaimAuthDetailBO, "LaborAmount", txtLabor)
            PopulateBOProperty(State.ClaimAuthDetailBO, "PartAmount", txtParts)
            PopulateBOProperty(State.ClaimAuthDetailBO, "ServiceCharge", txtServiceCharge)
            PopulateBOProperty(State.ClaimAuthDetailBO, "TripAmount", txtTripAmt)
            PopulateBOProperty(State.ClaimAuthDetailBO, "ShippingAmount", txtShippingAmt)
            PopulateBOProperty(State.ClaimAuthDetailBO, "DispositionAmount", txtDisposition)
            PopulateBOProperty(State.ClaimAuthDetailBO, "DiagnosticsAmount", txtDiagnostics)
            PopulateBOProperty(State.ClaimAuthDetailBO, "OtherAmount", txtOtherAmt)
            PopulateBOProperty(State.ClaimAuthDetailBO, "OtherExplanation", txtOtherDesc)
            PopulateBOProperty(State.ClaimAuthDetailBO, "TotalTaxAmount", txtTotalTaxAmount)

            If validateBO Then State.ClaimAuthDetailBO.Validate()

            Dim Total As Decimal
            With State.ClaimAuthDetailBO
                Dim subTotal As Decimal = 0
                If .LaborAmount IsNot Nothing Then subTotal += .LaborAmount.Value
                If .PartAmount IsNot Nothing Then subTotal += .PartAmount.Value
                If .ServiceCharge IsNot Nothing Then subTotal += .ServiceCharge.Value
                If .TripAmount IsNot Nothing Then subTotal += .TripAmount.Value
                If .ShippingAmount IsNot Nothing Then subTotal += .ShippingAmount.Value
                If .DiagnosticsAmount IsNot Nothing Then subTotal += .DiagnosticsAmount.Value
                If .DispositionAmount IsNot Nothing Then subTotal += .DispositionAmount.Value
                If .OtherAmount IsNot Nothing Then subTotal += .OtherAmount.Value
                PopulateControlFromBOProperty(txtSubTotal, subTotal)
                LoadClaimTaxRates(True)
                Total = subTotal + State.TotalTaxAmount
                PopulateControlFromBOProperty(txtTotal, Total)
            End With

            If Total > 0 Then
                State.IsEditMode_AD = True
                State.IsNewMode_AD = False
            Else
                State.IsEditMode_AD = False
                State.IsNewMode_AD = True
            End If

        End If

        If ErrCollection.Count > 0 Then
            Throw New PopulateBOErrorException
        End If


        If State.ClaimAuthDetailBO IsNot Nothing AndAlso Not State.ClaimAuthDetailBO.IsDeleted Then
            With State.ClaimAuthDetailBO
                If .LaborAmount IsNot Nothing Then PopulateControlFromBOProperty(txtLabor, .LaborAmount.Value)
                If .PartAmount IsNot Nothing Then PopulateControlFromBOProperty(txtParts, .PartAmount.Value)
                If .ServiceCharge IsNot Nothing Then PopulateControlFromBOProperty(txtServiceCharge, .ServiceCharge.Value)
                If .TripAmount IsNot Nothing Then PopulateControlFromBOProperty(txtTripAmt, .TripAmount.Value)
                If .ShippingAmount IsNot Nothing Then PopulateControlFromBOProperty(txtShippingAmt, .ShippingAmount.Value)
                If .DiagnosticsAmount IsNot Nothing Then PopulateControlFromBOProperty(txtDiagnostics, .DiagnosticsAmount.Value)
                If .DispositionAmount IsNot Nothing Then PopulateControlFromBOProperty(txtDisposition, .DispositionAmount.Value)
                If .OtherAmount IsNot Nothing Then PopulateControlFromBOProperty(txtOtherAmt, .OtherAmount.Value)
                PopulateControlFromBOProperty(txtOtherDesc, .OtherExplanation)

                Dim subTotal As Decimal = 0
                If .LaborAmount IsNot Nothing Then subTotal += .LaborAmount.Value
                If .PartAmount IsNot Nothing Then subTotal += .PartAmount.Value
                If .ServiceCharge IsNot Nothing Then subTotal += .ServiceCharge.Value
                If .TripAmount IsNot Nothing Then subTotal += .TripAmount.Value
                If .ShippingAmount IsNot Nothing Then subTotal += .ShippingAmount.Value
                If .DiagnosticsAmount IsNot Nothing Then subTotal += .DiagnosticsAmount.Value
                If .DispositionAmount IsNot Nothing Then subTotal += .DispositionAmount.Value
                If .OtherAmount IsNot Nothing Then subTotal += .OtherAmount.Value
                PopulateControlFromBOProperty(txtSubTotal, subTotal)
                LoadClaimTaxRates(True)
                Dim Total As Decimal = subTotal + State.TotalTaxAmount
                PopulateControlFromBOProperty(txtTotal, Total)

                If Total > 0 Then
                    State.IsEditMode_AD = True
                    State.IsNewMode_AD = False
                Else
                    State.IsEditMode_AD = False
                    State.IsNewMode_AD = True
                End If
            End With

        End If
    End Sub

    Private Sub PopulateApprove_DisApproveClaimStatus()
        'GVS Approve or disapprove 
        If IsIntegratedWithGVS() Then
            If (State.ClaimStatusBO IsNot Nothing AndAlso (State.ClaimStatusBO.StatusCode = Codes.CLAIM_EXTENDED_STATUS__WAITING_ON_BUDGET_APPROVAL)) Then
                Dim objNewClaimStatusId As Guid = GetStatusId
                If Not objNewClaimStatusId.Equals(Guid.Empty) Then
                    'If Me.State.IsCalledByPayClaimForm Then 'DEF-17426
                    If NavController.Context = "PAY_CLAIM_DETAIL-AUTH_DETAIL" OrElse NavController.Context = "PAY_CLAIM_ADJUSTMENT-PAY_CLAIM_DETAIL_CLADJ-AUTH_DETAIL" Then
                        State.NewClaimStatusBO = State.ClaimAuthDetailBO.AddClaimStatus(Guid.Empty)
                    Else
                        State.NewClaimStatusBO = State.ClaimBO.AddExtendedClaimStatus(Guid.Empty)
                    End If
                    State.NewClaimStatusBO.ClaimStatusByGroupId = objNewClaimStatusId
                    State.NewClaimStatusBO.ClaimId = State.ClaimBO.Id
                    State.NewClaimStatusBO.StatusDate = Date.Now
                    State.HasclaimStatusBOChanged = True
                End If
            End If
        End If
    End Sub

    Function GetStatusId() As Guid
        If chkApproved.Checked Then
            Return ClaimStatusByGroup.GetClaimStatusByGroupID(Codes.CLAIM_EXTENDED_STATUS__BUDGET_APPROVED)
        ElseIf chkDisapproved.Checked Then
            Return ClaimStatusByGroup.GetClaimStatusByGroupID(Codes.CLAIM_EXTENDED_STATUS__BUDGET_REJECTED)
        Else
            Return Guid.Empty
        End If

    End Function

    Private Sub PopulateFormFromBO(objPartsInfo As PartsInfo)

        Dim gridRowIdx As Integer = Grid.EditIndex
        Try
            With objPartsInfo
                Dim ddlDescription As DropDownList = CType(Grid.Rows(gridRowIdx).Cells(DESCRIPTION_COL).FindControl(DESCRIPTION_CONTROL_NAME), DropDownList)
                If Not .PartsDescriptionId.Equals(Guid.Empty) AndAlso ddlDescription IsNot Nothing Then
                    ddlDescription.SelectedValue = .PartsDescriptionId.ToString()
                End If

                Dim ddlInStock As DropDownList = CType(Grid.Rows(gridRowIdx).Cells(IN_STOCK_ID_COL).FindControl(IN_STOCK_ID_CONTROL_NAME), DropDownList)
                If Not .InStockID.Equals(Guid.Empty) AndAlso ddlInStock IsNot Nothing Then
                    ddlInStock.SelectedValue = .InStockID.ToString()
                End If

                Dim txtCost As TextBox = CType(Grid.Rows(gridRowIdx).Cells(COST_COL).FindControl(COST_CONTROL_NAME), TextBox)
                If .Cost IsNot Nothing AndAlso txtCost IsNot Nothing Then
                    PopulateControlFromBOProperty(txtCost, .Cost.Value)
                End If

                Dim lblID As Label = CType(Grid.Rows(gridRowIdx).Cells(ID_COL).FindControl(ID_CONTROL_NAME), Label)
                If lblID IsNot Nothing Then
                    lblID.Text = .Id.ToString
                End If

            End With

        Catch ex As Exception
            HandleErrors(ex, ErrController)
        End Try

    End Sub

    Private Sub PopulateADFormFromBO()

        Dim gridRowIdx As Integer = Grid.EditIndex
        Try
            If State.ClaimAuthDetailBO IsNot Nothing AndAlso Not State.ClaimAuthDetailBO.IsDeleted Then
                With State.ClaimAuthDetailBO
                    If .LaborAmount IsNot Nothing AndAlso .LaborAmount.Value > 0 Then
                        PopulateControlFromBOProperty(txtLabor, .LaborAmount.Value)
                    Else
                        txtLabor.Text = Nothing
                    End If

                    If .PartAmount IsNot Nothing AndAlso .PartAmount.Value > 0 Then
                        PopulateControlFromBOProperty(txtParts, .PartAmount.Value)
                    Else
                        txtParts.Text = Nothing
                    End If

                    If .ServiceCharge IsNot Nothing AndAlso .ServiceCharge.Value > 0 Then
                        PopulateControlFromBOProperty(txtServiceCharge, .ServiceCharge.Value)
                    Else
                        txtServiceCharge.Text = Nothing
                    End If

                    If .TripAmount IsNot Nothing AndAlso .TripAmount.Value > 0 Then
                        PopulateControlFromBOProperty(txtTripAmt, .TripAmount.Value)
                    Else
                        txtTripAmt.Text = Nothing
                    End If

                    If .ShippingAmount IsNot Nothing AndAlso .ShippingAmount.Value > 0 Then
                        PopulateControlFromBOProperty(txtShippingAmt, .ShippingAmount.Value)
                    Else
                        txtShippingAmt.Text = Nothing
                    End If

                    If .DiagnosticsAmount IsNot Nothing AndAlso .DiagnosticsAmount.Value > 0 Then
                        PopulateControlFromBOProperty(txtDiagnostics, .DiagnosticsAmount.Value)
                    Else
                        txtDiagnostics.Text = Nothing
                    End If

                    If .DispositionAmount IsNot Nothing AndAlso .DispositionAmount.Value > 0 Then
                        PopulateControlFromBOProperty(txtDisposition, .DispositionAmount.Value)
                    Else
                        txtDisposition.Text = Nothing
                    End If

                    If .OtherAmount IsNot Nothing AndAlso .OtherAmount.Value > 0 Then
                        PopulateControlFromBOProperty(txtOtherAmt, .OtherAmount.Value)
                    Else
                        txtOtherAmt.Text = Nothing
                    End If
                    PopulateControlFromBOProperty(txtOtherDesc, .OtherExplanation)


                    Dim subTotal As Decimal = GetSubTotal
                    LoadClaimTaxRates(True)
                    PopulateControlFromBOProperty(txtSubTotal, subTotal)

                    Dim Total As Decimal = subTotal + State.TotalTaxAmount
                    PopulateControlFromBOProperty(txtTotal, Total)

                    If Total > 0 Then
                        State.IsEditMode_AD = True
                        State.IsNewMode_AD = False
                        moAuthDetailTabPanel_WRITE.Visible = True
                    Else
                        State.IsEditMode_AD = False
                        State.IsNewMode_AD = True
                        moAuthDetailTabPanel_WRITE.Visible = False
                        ClearADForm()
                    End If
                End With
            End If



        Catch ex As Exception
            HandleErrors(ex, ErrController)
        End Try

    End Sub


    Private Sub LoadClaimTaxRates(Optional blnComputeTax As Boolean = False)

        If State.ClaimTaxRatesData Is Nothing Then
            Dim company As New Company(State.CompanyId)
            Dim CountryId As Guid = company.BusinessCountryId
            Dim RegionId As Guid = State.ClaimBO.ServiceCenterObject.Address.RegionId
            Dim dealer_id As Guid = State.ClaimBO.Dealer.Id
            State.ClaimTaxRatesData = ClaimInvoice.GetClaimTaxRatesData(CountryId, RegionId, dealer_id, DateTime.Now, State.ClaimMethodOfRepair)
        End If

        With State.ClaimTaxRatesData
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
        State.TotalTaxAmount = 0

        Try
            txtPartsTax.Text = computeTaxAmtByComputeMethod(CType(txtParts.Text, Decimal), CType(hdTaxRateClaimParts.Value, Decimal), hdComputeMethodClaimParts.Value).ToString
        Catch ex As Exception
            txtPartsTax.Text = ""
        End Try

        Try
            txtLaborTax.Text = computeTaxAmtByComputeMethod(CType(txtLabor.Text, Decimal), CType(hdTaxRateClaimLabor.Value, Decimal), hdComputeMethodClaimLabor.Value).ToString
        Catch ex As Exception
            txtLaborTax.Text = ""
        End Try

        Try
            txtServiceChargeTax.Text = computeTaxAmtByComputeMethod(CType(txtServiceCharge.Text, Decimal), CType(hdTaxRateClaimService.Value, Decimal), hdComputeMethodClaimService.Value).ToString
        Catch ex As Exception
            txtServiceChargeTax.Text = ""
        End Try

        Try
            txtTripAmtTax.Text = computeTaxAmtByComputeMethod(CType(txtTripAmt.Text, Decimal), CType(hdTaxRateClaimTrip.Value, Decimal), hdComputeMethodClaimTrip.Value).ToString
        Catch ex As Exception
            txtTripAmtTax.Text = ""
        End Try

        Try
            txtShippingTax.Text = computeTaxAmtByComputeMethod(CType(txtShippingAmt.Text, Decimal), CType(hdTaxRateClaimShipping.Value, Decimal), hdComputeMethodClaimShipping.Value).ToString
        Catch ex As Exception
            txtShippingTax.Text = ""
        End Try

        Try
            txtDispositionTax.Text = computeTaxAmtByComputeMethod(CType(txtDisposition.Text, Decimal), CType(hdTaxRateClaimDisposition.Value, Decimal), hdComputeMethodClaimDisposition.Value).ToString
        Catch ex As Exception
            txtDispositionTax.Text = ""
        End Try

        Try
            txtDiagnosticsTax.Text = computeTaxAmtByComputeMethod(CType(txtDiagnostics.Text, Decimal), CType(hdTaxRateClaimDiagnostics.Value, Decimal), hdComputeMethodClaimDiagnostics.Value).ToString
        Catch ex As Exception
            txtDiagnosticsTax.Text = ""
        End Try

        Try
            txtOtherTax.Text = computeTaxAmtByComputeMethod(CType(txtOtherAmt.Text, Decimal), CType(hdTaxRateClaimOther.Value, Decimal), hdComputeMethodClaimOther.Value).ToString
        Catch ex As Exception
            txtOtherTax.Text = ""
        End Try


        txtTotalTaxAmount.Text = Math.Round(State.TotalTaxAmount, 2).ToString

    End Sub

    Private Function ComputePartsTaxes(totalpartsCost As DecimalType) As DecimalType
        LoadClaimTaxRates(False)
        Try
            Return New DecimalType(computeTaxAmtByComputeMethod(totalpartsCost, CType(hdTaxRateClaimParts.Value, Decimal), hdComputeMethodClaimParts.Value))

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

        State.TotalTaxAmount += taxAmount

        Return taxAmount

    End Function
    Private Function getDecimalValue(decimalObj As DecimalType, Optional ByVal decimalDigit As Integer = 2) As Decimal
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
        If State.ClaimAuthDetailBO IsNot Nothing AndAlso Not State.ClaimAuthDetailBO.IsDeleted Then
            Dim partsTotal As Decimal
            For i As Integer = 0 To State.PartsInfoDV.Count - 1
                partsTotal = partsTotal + CType(State.PartsInfoDV.Item(i).Item(VIEW_COST_COL), Decimal)
            Next
            If partsTotal > 0 Then
                State.ClaimAuthDetailBO.PartAmount = partsTotal
            Else
                Me.State.ClaimAuthDetailBO.PartAmount = 0
            End If
        End If
    End Sub

    Private Sub ReturnFromEditing()

        Grid.EditIndex = NO_ROW_SELECTED_INDEX

        If Grid.PageCount = 0 Then
            'if returning to the "1st time in" screen
            ControlMgr.SetVisibleControl(Me, Grid, False)
        Else
            ControlMgr.SetVisibleControl(Me, Grid, True)
        End If

        State.IsEditMode_PI = False
        State.IsNewMode_PI = False
        State.IsDone = True
        PopulateGrid()
        State.PageIndex = Grid.PageIndex
        SetButtonsState()

    End Sub

    Private Sub ReturnFromEditingAD()

        State.IsDone = True
        PopulateGrid()
        SetButtonsState()
        EnableDisableFields()
    End Sub

    Private Sub SetButtonsState(Optional ByVal blnSetGridControl As Boolean = True)
        If Not State.IsViewOnly Then
            ControlMgr.SetEnableControl(Me, btnNew_PI_WRITE, Not State.IsEditMode_PI AndAlso Not State.IsNewMode_PI AndAlso isEditableForGVS())
            ControlMgr.SetEnableControl(Me, btnSave_PI_WRITE, (State.IsEditMode_PI OrElse State.IsNewMode_PI) AndAlso isEditableForGVS())
            ControlMgr.SetVisibleControl(Me, btnSave_WRITE, (NavController.Context = "PAY_CLAIM_DETAIL-AUTH_DETAIL" OrElse NavController.Context = "PAY_CLAIM_ADJUSTMENT-PAY_CLAIM_DETAIL_CLADJ-AUTH_DETAIL"))
            ControlMgr.SetEnableControl(Me, Cancel_PI_Button, (State.IsEditMode_PI OrElse State.IsNewMode_PI) AndAlso isEditableForGVS())
            'ControlMgr.SetVisibleControl(Me, Me.btnSave_WRITE, Me.State.IsCalledByPayClaimForm) 'DEF-17426
            ControlMgr.SetEnableControl(Me, btnSave_WRITE, (NavController.Context = "PAY_CLAIM_DETAIL-AUTH_DETAIL" OrElse NavController.Context = "PAY_CLAIM_ADJUSTMENT-PAY_CLAIM_DETAIL_CLADJ-AUTH_DETAIL"))
            ControlMgr.SetEnableControl(Me, btnBack, True)
            ControlMgr.SetEnableControl(Me, btnSave_WRITE, State.IsDone AndAlso State.IsCalledByPayClaimForm)
            ControlMgr.SetEnableControl(Me, btnUndo_Write, State.IsDone)

            'Claim Detail controls
            ControlMgr.SetVisibleControl(Me, BtnEdit_AD_WRITE, State.IsEditMode_AD AndAlso isEditableForGVS(True))
            ControlMgr.SetVisibleControl(Me, btnNew_AD_WRITE, State.IsNewMode_AD AndAlso isEditableForGVS(True))

            ControlMgr.SetEnableControl(Me, BtnEdit_AD_WRITE, State.IsEditMode_AD AndAlso isEditableForGVS(True))
            ControlMgr.SetEnableControl(Me, btnNew_AD_WRITE, State.IsNewMode_AD AndAlso isEditableForGVS(True))
            ControlMgr.SetEnableControl(Me, Cancel_AD_Button, False)
            ControlMgr.SetEnableControl(Me, btnSave_AD_WRITE, False)
            'ControlMgr.SetVisibleControl(Me, Me.pnlApprove_Disapprove, Me.State.IsEditMode_AD And isEditableForGVS(False))
            ControlMgr.SetEnableControl(Me, pnlApprove_Disapprove, False)
        Else
            ControlMgr.SetEnableControl(Me, btnNew_PI_WRITE, Not State.IsViewOnly)
            ControlMgr.SetEnableControl(Me, btnSave_PI_WRITE, Not State.IsViewOnly)
            ControlMgr.SetEnableControl(Me, Cancel_PI_Button, Not State.IsViewOnly)
            ControlMgr.SetVisibleControl(Me, btnSave_WRITE, Not State.IsViewOnly)

            ControlMgr.SetEnableControl(Me, btnBack, True)
            ControlMgr.SetEnableControl(Me, btnSave_WRITE, Not State.IsViewOnly)
            ControlMgr.SetEnableControl(Me, btnUndo_Write, Not State.IsViewOnly)

            'Claim Detail controls
            ControlMgr.SetVisibleControl(Me, BtnEdit_AD_WRITE, Not State.IsViewOnly)
            ControlMgr.SetVisibleControl(Me, btnNew_AD_WRITE, Not State.IsViewOnly)

            ControlMgr.SetEnableControl(Me, BtnEdit_AD_WRITE, Not State.IsViewOnly)
            ControlMgr.SetEnableControl(Me, btnNew_AD_WRITE, Not State.IsViewOnly)
            ControlMgr.SetEnableControl(Me, Cancel_AD_Button, False)
            ControlMgr.SetEnableControl(Me, btnSave_AD_WRITE, False)
            'ControlMgr.SetVisibleControl(Me, Me.pnlApprove_Disapprove, Me.State.IsEditMode_AD And isEditableForGVS(False))
            If blnSetGridControl Then SetGridControls(Grid, False)
        End If
    End Sub

    Private Sub EnableDisableFields()
        If Not State.IsViewOnly Then
            txtLabor.ReadOnly = Not ((State.IsEditMode_AD OrElse State.IsNewMode_AD) AndAlso State.IsEditable_AD AndAlso isEditableForGVS())
            txtServiceCharge.ReadOnly = Not ((State.IsEditMode_AD OrElse State.IsNewMode_AD) AndAlso State.IsEditable_AD AndAlso isEditableForGVS())
            txtTripAmt.ReadOnly = Not ((State.IsEditMode_AD OrElse State.IsNewMode_AD) AndAlso State.IsEditable_AD AndAlso isEditableForGVS())
            txtShippingAmt.ReadOnly = Not ((State.IsEditMode_AD OrElse State.IsNewMode_AD) AndAlso State.IsEditable_AD AndAlso isEditableForGVS(True))
            txtOtherAmt.ReadOnly = Not ((State.IsEditMode_AD OrElse State.IsNewMode_AD) AndAlso State.IsEditable_AD AndAlso isEditableForGVS())
            txtOtherDesc.ReadOnly = Not ((State.IsEditMode_AD OrElse State.IsNewMode_AD) AndAlso State.IsEditable_AD AndAlso isEditableForGVS())
            txtDiagnostics.ReadOnly = Not ((State.IsEditMode_AD OrElse State.IsNewMode_AD) AndAlso State.IsEditable_AD AndAlso isEditableForGVS())
            txtDisposition.ReadOnly = Not ((State.IsEditMode_AD OrElse State.IsNewMode_AD) AndAlso State.IsEditable_AD AndAlso isEditableForGVS())
            txtParts.ReadOnly = Not ((State.IsEditMode_AD OrElse State.IsNewMode_AD) AndAlso State.IsEditable_AD AndAlso isEditableForGVS() AndAlso State.PartsDescriptionAvailable)
        End If
    End Sub

    Private Sub ToggleMainButtons(enabled As Boolean)
        ControlMgr.SetEnableControl(Me, btnBack, enabled)
        ControlMgr.SetEnableControl(Me, btnSave_WRITE, enabled)
        ControlMgr.SetEnableControl(Me, btnUndo_Write, enabled)
    End Sub

    Private Function GetDirtyStatus() As Boolean
        Dim IsBODirty As Boolean = False
        If State.ClaimBO.MethodOfRepairCode <> Codes.METHOD_OF_REPAIR__REPLACEMENT Then
            If State.ClaimAuthDetailBO.IsNew Then
                If State.ClaimAuthDetailBO.PartAmount IsNot Nothing AndAlso GetSubTotal > State.ClaimAuthDetailBO.PartAmount.Value Then
                    IsBODirty = True
                    'ElseIf Me.State.ClaimAuthDetailBO.PartAmount Is Nothing AndAlso Me.GetSubTotal >= 0 Then
                ElseIf State.ClaimAuthDetailBO.PartAmount Is Nothing AndAlso GetSubTotal > 0 Then 'DEF: 29274 - REQ-289 Actions dropdown list for active claim for CLAR dealer. (Changed the above commented code to that on this line.)
                    IsBODirty = True
                End If
            Else
                IsBODirty = State.ClaimAuthDetailBO.IsDirty
            End If
        ElseIf (State.ClaimAuthDetailBO.IsNew AndAlso GetSubTotal >= 0) Then
            IsBODirty = True
        Else
            IsBODirty = State.ClaimAuthDetailBO.IsDirty
        End If

        Return IsBODirty
    End Function

    Private Function IsClaimAuthDetailBODirty() As Boolean

    End Function
#End Region

#Region "DataViewRelated "
    Private Sub Grid_SortCommand(source As Object, e As GridViewSortEventArgs) Handles Grid.Sorting
        Try
            Dim spaceIndex As Integer = SortDirection.LastIndexOf(" ")

            If spaceIndex > 0 AndAlso SortDirection.Substring(0, spaceIndex).Equals(e.SortExpression) Then
                If SortDirection.EndsWith(" ASC") Then
                    SortDirection = e.SortExpression + " DESC"
                Else
                    SortDirection = e.SortExpression + " ASC"
                End If
            Else
                SortDirection = e.SortExpression + " ASC"
            End If
            'Me.State.SortExpression = Me.SortDirection
            State.PageIndex = 0
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, ErrController)
        End Try
    End Sub

    Public Property SortDirection() As String
        Get
            Return ViewState("SortDirection").ToString
        End Get
        Set(value As String)
            ViewState("SortDirection") = value
        End Set
    End Property

    Private Sub Grid_PageIndexChanged(source As Object, e As GridViewPageEventArgs) Handles Grid.PageIndexChanging

        Try
            If (Not (State.IsEditMode_PI)) Then
                State.PageIndex = e.NewPageIndex
                'Me.Grid.PageIndex = Me.State.PageIndex
                PopulateGrid()
                Grid.SelectedIndex = NO_ITEM_SELECTED_INDEX
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrController)
        End Try

        Try
            State.PageIndex = e.NewPageIndex

            PopulateGrid()
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub ItemCommand(source As Object, e As GridViewCommandEventArgs)

        Try
            Dim index As Integer
            State.IsDone = False


            If (e.CommandName = EDIT_COMMAND) Then
                'Do the Edit here

                Dim rowIdx As GridViewRow = CType(CType(e.CommandSource, Control).Parent.Parent, GridViewRow)
                index = rowIdx.RowIndex
                State.LastRowIndexInUse = index

                State.LastOperation = EDIT_COMMAND

                'Set the IsEditMode flag to TRUE
                State.IsEditMode_PI = True

                'Save the Id in the Session
                State.PartsInfoID = New Guid(Grid.Rows(index).Cells(ID_COL).Text)

                'intailize the array list
                If State.PartsInfoBOs Is Nothing Then
                    State.PartsInfoBOs = New ArrayList
                    State.PartsInfoBOIDs = New ArrayList
                End If

                Dim objPartsInfo As PartsInfo

                'check if this item has been added to the arraylist
                Dim objPI_ID As Guid = Guid.Empty
                Dim AddToArray As Boolean = True
                For i As Integer = 0 To State.PartsInfoBOs.Count - 1
                    objPI_ID = CType(State.PartsInfoBOIDs(i), Guid)
                    Dim objPI As PartsInfo = CType(State.PartsInfoBOs(i), PartsInfo)
                    If State.PartsInfoID.Equals(objPI_ID) Then
                        AddToArray = False
                        objPartsInfo = objPI
                        Exit For
                    End If
                Next

                If AddToArray Then
                    'If Me.State.IsCalledByPayClaimForm Then 'DEF-17426
                    If NavController.Context = "PAY_CLAIM_DETAIL-AUTH_DETAIL" OrElse NavController.Context = "PAY_CLAIM_ADJUSTMENT-PAY_CLAIM_DETAIL_CLADJ-AUTH_DETAIL" Then
                        objPartsInfo = State.ClaimAuthDetailBO.AddPartsInfo(State.PartsInfoID)
                    Else
                        objPartsInfo = State.ClaimBO.AddPartsInfo(State.PartsInfoID) 'Me.State.ClaimAuthDetailBO.AddPartsInfo(Me.State.PartsInfoID)
                    End If
                    State.PartsInfoBOIDs.Add(objPI_ID)
                    State.PartsInfoBOs.Add(objPartsInfo)
                End If

                PopulateGrid()

                State.PageIndex = Grid.PageIndex

                'Disable all Edit and Delete icon buttons on the Grid
                SetGridControls(Grid, False)

                'Set focus on the Description dropdown for the EditIndex row
                SetFocusOnEditableFieldInGrid(Grid, DESCRIPTION_COL, DESCRIPTION_CONTROL_NAME, index, False, objPartsInfo)

                PopulateFormFromBO(objPartsInfo)

                SetButtonsState(False)
                DisabledTabsList.Add(Tab_AuthDetail)
                ToggleMainButtons(False)
            ElseIf (e.CommandName = DELETE_COMMAND) Then
                'Do the delete here

                Dim rowIdx As GridViewRow = CType(CType(e.CommandSource, Control).Parent.Parent, GridViewRow)
                index = rowIdx.RowIndex
                State.LastRowIndexInUse = index

                State.LastOperation = DELETE_COMMAND

                'Clear the SelectedItemStyle to remove the highlight from the previously saved row
                Grid.SelectedIndex = NO_ROW_SELECTED_INDEX

                'Save the Id in the Session
                State.PartsInfoID = New Guid(Grid.Rows(index).Cells(ID_COL).Text)
                '   Dim objPartsInfo As PartsInfo = Me.State.ClaimAuthDetailBO.AddPartsInfo(Me.State.PartsInfoID)
                If State.PartsInfoBOs Is Nothing Then
                    State.PartsInfoBOs = New ArrayList
                    State.PartsInfoBOIDs = New ArrayList
                End If
                Dim objPartsInfo As PartsInfo

                Try
                    Dim AddToArray As Boolean = True
                    Dim objPI_ID As Guid = Guid.Empty
                    For i As Integer = 0 To State.PartsInfoBOs.Count - 1
                        objPI_ID = CType(State.PartsInfoBOIDs(i), Guid)
                        Dim objPI As PartsInfo = CType(State.PartsInfoBOs(i), PartsInfo)
                        If State.PartsInfoID.Equals(objPI_ID) Then
                            State.PartsInfoBOs.RemoveAt(i)
                            State.PartsInfoBOIDs.RemoveAt(i)
                            AddToArray = False
                            Exit For
                        End If
                    Next


                    'check if this bo id was in the original dv
                    Dim row As DataRow
                    For Each row In State.OreginalPartsInfoTable.Rows
                        Dim partInfoID As Guid = New Guid(CType(row(DV_ID_COL), Byte()))
                        If partInfoID.Equals(State.PartsInfoID) Then
                            'if it is, then add to the family and mark it delete
                            'If Me.State.IsCalledByPayClaimForm Then 'DEF-17426
                            If NavController.Context = "PAY_CLAIM_DETAIL-AUTH_DETAIL" OrElse NavController.Context = "PAY_CLAIM_ADJUSTMENT-PAY_CLAIM_DETAIL_CLADJ-AUTH_DETAIL" Then
                                objPartsInfo = State.ClaimAuthDetailBO.AddPartsInfo(State.PartsInfoID)
                            Else
                                objPartsInfo = State.ClaimBO.AddPartsInfo(State.PartsInfoID) 'Me.State.ClaimAuthDetailBO.AddPartsInfo(Me.State.PartsInfoID)
                            End If
                            objPartsInfo.Delete()
                            Exit For
                        End If
                    Next
                    If AddToArray AndAlso objPartsInfo IsNot Nothing Then
                        State.PartsInfoBOIDs.Add(objPI_ID)
                        State.PartsInfoBOs.Add(objPartsInfo)
                    End If


                    'Call the Save() method in the Region Business Object here
                    'Me.State.PartsInfoBO.Save()

                Catch ex As Exception
                    objPartsInfo.RejectChanges()
                    Throw ex
                End Try

                State.PageIndex = Grid.PageIndex

                'Set the IsAfterSave flag to TRUE so that the Paging logic gets invoked
                State.IsAfterSave = True

                'drop the row
                State.PartsInfoDV.Table.Rows.RemoveAt((Grid.PageIndex * 10) + index)

                PopulateGrid()
                State.PageIndex = Grid.PageIndex

                'update the part amount in the claim auth detail bo
                UpdatePartsAmountInClaimAuthDetailBO()
                PopulateADFormFromBO()
                ToggleMainButtons(True)
            ElseIf ((e.CommandName = SORT_COMMAND) AndAlso Not (IsEditing)) Then

                Dim rowIdx As GridViewRow = CType(CType(e.CommandSource, Control).Parent.Parent, GridViewRow)
                index = rowIdx.RowIndex
                State.LastRowIndexInUse = index

                Grid.DataMember = e.CommandArgument.ToString
                PopulateGrid()
            End If
            State.HasDataChanged = True
        Catch ex As Exception
            HandleErrors(ex, ErrController)
        End Try

    End Sub

    Protected Sub ItemBound(source As Object, e As GridViewRowEventArgs) Handles Grid.RowDataBound
        Try
            BaseItemBound(source, e)

            Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
            Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
            If dvRow IsNot Nothing AndAlso Not State.bnoRow Then
                If itemType = ListItemType.Item OrElse itemType = ListItemType.AlternatingItem OrElse itemType = ListItemType.SelectedItem Then
                    PopulateControlFromBOProperty(e.Row.Cells(ID_COL), dvRow(PartsInfo.PartsInfoDV.COL_NAME_PARTS_INFO_ID))
                    If itemType = ListItemType.EditItem OrElse State.IsNewMode_PI = True OrElse State.IsEditMode_PI = True Then
                        If (e.Row.Cells(IN_STOCK_ID_COL).FindControl(IN_STOCK_ID_CONTROL_NAME) IsNot Nothing) Then
                            Dim YESNOdv As DataView = LookupListNew.DropdownLookupList(YESNO, ElitaPlusIdentity.Current.ActiveUser.LanguageId, True)
                            Dim dropdownInStock As DropDownList = CType(e.Row.Cells(IN_STOCK_ID_COL).FindControl(IN_STOCK_ID_CONTROL_NAME), DropDownList)

                            Dim YESNOList As ListItem() = CommonConfigManager.Current.ListManager.GetList("YESNO", Thread.CurrentPrincipal.GetLanguageCode())
                            dropdownInStock.Populate(YESNOList, New PopulateOptions() With
                                              {
                                                .AddBlankItem = True,
                                                .SortFunc = AddressOf PopulateOptions.GetCode
                                               })

                            Dim drv As DataRowView = CType(e.Row.DataItem, DataRowView)
                            If drv(PartsInfoDAL.COL_NAME_IN_STOCK_ID) IsNot DBNull.Value Then
                                SetSelectedItem(dropdownInStock, New Guid(CType(dvRow(PartsInfo.PartsInfoDV.COL_NAME_IN_STOCK_ID), Byte())))
                            End If

                        End If
                    Else
                        PopulateControlFromBOProperty(e.Row.Cells(DESCRIPTION_COL), dvRow(PartsInfo.PartsInfoDV.COL_NAME_PARTS_DESCRIPTION))
                        PopulateControlFromBOProperty(e.Row.Cells(COST_COL), dvRow(PartsInfo.PartsInfoDV.COL_NAME_COST))
                        If (e.Row.Cells(IN_STOCK_ID_COL).FindControl(IN_STOCK_ID_CONTROL_NAME) IsNot Nothing) Then
                            DisableControl(e.Row.Cells(IN_STOCK_ID_COL).FindControl(IN_STOCK_ID_CONTROL_NAME))
                        End If

                        If (e.Row.Cells(PART_DESCRIPTION_COL).FindControl(DESCRIPTION_CONTROL_NAME) IsNot Nothing) Then
                            DisableControl(e.Row.Cells(PART_DESCRIPTION_COL).FindControl(DESCRIPTION_CONTROL_NAME), True)
                        End If

                        If (e.Row.Cells(ID_COL).FindControl(DELETEBUTTON_WRITE_CONTROL_NAME) IsNot Nothing) Then
                            DisableControl(e.Row.Cells(ID_COL).FindControl(DELETEBUTTON_WRITE_CONTROL_NAME), True)
                        End If
                    End If


                End If

                If itemType = ListItemType.EditItem Then

                End If
            End If

        Catch ex As Exception
            HandleErrors(ex, ErrController)
        End Try
    End Sub

    Protected Sub ItemCreated(sender As Object, e As GridViewRowEventArgs)
        Try
            BaseItemCreated(sender, e)
        Catch ex As Exception
            HandleErrors(ex, ErrController)
        End Try
    End Sub

    Protected Sub BindBoPropertiesToGridHeaders(objPartsInfoBO As PartsInfo)
        BindBOPropertyToGridHeader(objPartsInfoBO, "DescriptionId", Grid.Columns(DESCRIPTION_COL))
        BindBOPropertyToGridHeader(objPartsInfoBO, "COST", Grid.Columns(COST_COL))
        'Me.BindBOPropertyToGridHeader(Me.State.Region, "RiskTypeEnglish", Me.Grid.Columns(Me.CODE_COL))
        ClearGridViewHeadersAndLabelsErrSign()
    End Sub

    Private Sub SetFocusOnEditableFieldInGrid(grid As GridView, cellPosition As Integer, controlName As String, itemIndex As Integer, newRow As Boolean, objPartsInfo As PartsInfo)
        'Set focus on the Description TextBox for the EditIndex row
        Dim desc As DropDownList = CType(Me.Grid.Rows(itemIndex).Cells(cellPosition).FindControl(controlName), DropDownList)
        If desc IsNot Nothing Then
            If newRow Then
                Dim dv As DataView = PartsInfo.getAvailList(State.RiskGroupID, State.ClaimBO.Id)
                'update the rowfilter of the dropdown dv
                If State.PartsInfoBOs IsNot Nothing Then
                    For j As Integer = 0 To State.PartsInfoBOs.Count - 1
                        Dim row As DataRow
                        For Each row In dv.Table.Rows
                            Dim partDescID As Guid = New Guid(CType(row(DV_ID_COL), Byte()))
                            Try
                                If partDescID.Equals(CType(State.PartsInfoBOs(j), PartsInfo).PartsDescriptionId) Then
                                    dv.Table.Rows.Remove(row)
                                    Exit For
                                End If
                            Catch ex As BOInvalidOperationException
                                'do noting
                                j += 1
                            End Try

                            If j = State.PartsInfoBOs.Count - 1 Then Exit For
                        Next
                        If j = State.PartsInfoBOs.Count - 1 Then Exit For
                    Next
                End If
                BindListControlToDataView(desc, dv, LookupListNew.COL_DESCRIPTION_NAME, LookupListNew.COL_ID_NAME, False)
            Else
                Dim oListContext As New ListContext
                oListContext.CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
                oListContext.ClaimId = State.ClaimBO.Id
                oListContext.RiskGroupId = State.RiskGroupID
                oListContext.PartsDescriptionId = objPartsInfo.PartsDescriptionId
                Dim partsInfoList As ListItem() = CommonConfigManager.Current.ListManager.GetList("CurrentPartInfoByClaim", Thread.CurrentPrincipal.GetLanguageCode(), oListContext)
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

    Private Sub btnNew_PI_WRITE_Click(sender As Object, e As EventArgs) Handles btnNew_PI_WRITE.Click

        Try
            State.IsDone = False
            State.IsEditable_AD = False
            Dim desc As DropDownList = New DropDownList

            Dim oListContext As New ListContext
            oListContext.CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
            oListContext.ClaimId = State.ClaimBO.Id
            oListContext.RiskGroupId = State.RiskGroupID
            Dim partsInfoList As ListItem() = CommonConfigManager.Current.ListManager.GetList("PartInfoByClaim", Thread.CurrentPrincipal.GetLanguageCode(), oListContext)

            desc.Populate(partsInfoList, New PopulateOptions() With
                                           {
                                           .AddBlankItem = True
                                           })


            If State.PartsInfoDV.Count < desc.Items.Count Then
                State.IsEditMode_PI = True
                State.IsNewMode_PI = True
                AddNewPartsInfo()
                ToggleMainButtons(False)
                DisabledTabsList.Add(Tab_AuthDetail)
            Else
                If State.PartsAdded Then
                    DisplayMessage(Message.MSG_NO_MORE_PARTSDESC_FOUND, "", MSG_BTN_OK, MSG_TYPE_ALERT)
                Else
                    DisplayMessage(Message.MSG_NO_PARTSDESC_FOUND, "", MSG_BTN_OK, MSG_TYPE_ALERT)
                End If
                CancelEditing()
                ToggleMainButtons(True)
            End If


        Catch ex As Exception
            HandleErrors(ex, ErrController)
        End Try

    End Sub

    Private Sub btnSave_PI_WRITE_Click(sender As Object, e As EventArgs) Handles btnSave_PI_WRITE.Click
        Try

            State.HasDataChanged = True
            State.IsDone = True
            State.IsEditable_AD = False
            Dim objPartsInfo As PartsInfo = CType(State.PartsInfoBOs.Item(State.PartsInfoBOs.Count - 1), PartsInfo)
            PopulateBOFromForm(objPartsInfo, State.LastOperation)

            Populate_AD_BOFromForm(False)
            UpdatePartsAmountInClaimAuthDetailBO()
            PopulateADFormFromBO()
            ReturnFromEditing()
            ToggleMainButtons(True)
            State.HasDataBeenSaved = False
            State.HasclaimStatusBOSaved = False

        Catch ex As Exception
            HandleErrors(ex, ErrController)
        End Try
    End Sub

    Private Sub BtnEdit_AD_WRITE_Click(sender As Object, e As EventArgs) Handles BtnEdit_AD_WRITE.Click
        Try
            State.IsDone = True
            State.IsEditMode_AD = True
            State.IsNewMode_AD = False
            State.IsEditable_AD = True
            EnableDisableFields()
            SetButtonsState()
            ControlMgr.SetEnableControl(Me, Cancel_AD_Button, True)
            ControlMgr.SetEnableControl(Me, BtnEdit_AD_WRITE, False)
            ControlMgr.SetEnableControl(Me, btnSave_AD_WRITE, True)
            If IsIntegratedWithGVS() Then
                If (State.ClaimStatusBO IsNot Nothing AndAlso (State.ClaimStatusBO.StatusCode = Codes.CLAIM_EXTENDED_STATUS__WAITING_ON_BUDGET_APPROVAL)) Then
                    ControlMgr.SetVisibleControl(Me, pnlApprove_Disapprove, True)
                    ControlMgr.SetEnableControl(Me, pnlApprove_Disapprove, True)
                End If
            End If
            SetFocusOnFirstEditableControl()
            PopulateGrid()
            ToggleMainButtons(False)
        Catch ex As Exception
            HandleErrors(ex, ErrController)
        End Try
    End Sub

    Private Sub btnNew_AD_WRITE_Click(sender As Object, e As EventArgs) Handles btnNew_AD_WRITE.Click
        Try
            moAuthDetailTabPanel_WRITE.Visible = True
            State.IsDone = True
            State.IsEditMode_AD = False
            State.IsNewMode_AD = True
            State.IsEditable_AD = True
            EnableDisableFields()
            SetButtonsState()
            ControlMgr.SetEnableControl(Me, Cancel_AD_Button, True)
            ControlMgr.SetEnableControl(Me, btnNew_AD_WRITE, False)
            ControlMgr.SetEnableControl(Me, btnSave_AD_WRITE, True)
            If IsIntegratedWithGVS() Then
                If (State.ClaimStatusBO IsNot Nothing AndAlso (State.ClaimStatusBO.StatusCode = Codes.CLAIM_EXTENDED_STATUS__WAITING_ON_BUDGET_APPROVAL)) Then
                    ControlMgr.SetVisibleControl(Me, pnlApprove_Disapprove, True)
                    ControlMgr.SetEnableControl(Me, pnlApprove_Disapprove, True)
                End If
            End If
            SetFocusOnFirstEditableControl()
            PopulateGrid()
            ToggleMainButtons(False)
        Catch ex As Exception
            HandleErrors(ex, ErrController)
        End Try
    End Sub

    Private Sub Cancel_AD_Button_Click(sender As Object, e As EventArgs) Handles Cancel_AD_Button.Click
        Try
            State.IsEditable_AD = False
            PopulateClaimAuthDetailTab()
            EnableDisableFields()
            SetButtonsState()
            PopulateGrid()
            If State.IsReplacementClaim Then
                DisabledTabsList.Add(Tab_PartsInfo)
            End If
            ToggleMainButtons(True)
        Catch ex As Exception
            HandleErrors(ex, ErrController)
        End Try
    End Sub

    Private Sub btnSave_AD_WRITE_Click(sender As Object, e As EventArgs) Handles btnSave_AD_WRITE.Click
        Try
            State.HasDataChanged = True
            State.IsDone = True
            State.IsEditable_AD = False
            Populate_AD_BOFromForm(False)

            ReturnFromEditingAD()
            State.HasDataBeenSaved = False
            State.HasclaimStatusBOSaved = False
            If State.IsReplacementClaim Then
                DisabledTabsList.Add(Tab_PartsInfo)
            End If
            ToggleMainButtons(True)

        Catch ex As Exception
            HandleErrors(ex, ErrController)
        End Try
    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click

        Try

            UpdatePartsAmountInClaimAuthDetailBO()
            Populate_AD_BOFromForm(False)
            PopulateApprove_DisApproveClaimStatus()

            Dim IsBODirty As Boolean = False
            Dim IsNewClaimStatusBODirty As Boolean = False

            If State.PartsInfoBOs IsNot Nothing Then
                Dim objPartsInfo As PartsInfo
                For i As Integer = 0 To State.PartsInfoBOs.Count - 1
                    objPartsInfo = CType(State.PartsInfoBOs.Item(i), PartsInfo)
                    If objPartsInfo.IsDeleted Then
                        IsBODirty = True
                        Exit For
                    ElseIf objPartsInfo.IsDirty Then
                        IsBODirty = True
                        Exit For
                    End If
                Next
            End If

            If Not IsBODirty AndAlso State.ClaimAuthDetailBO IsNot Nothing Then
                If State.ClaimAuthDetailBO.IsDeleted Then
                    IsBODirty = True
                Else
                    IsBODirty = GetDirtyStatus 'Me.State.ClaimAuthDetailBO.IsDirty And Me.GetSubTotal > 0
                    hdnSelectedTab.Value = Tab_AuthDetail
                End If
            End If

            If State.NewClaimStatusBO IsNot Nothing Then
                IsNewClaimStatusBODirty = State.NewClaimStatusBO.IsDirty
            End If

            If (NavController IsNot Nothing) Then
                If NavController.Context = "PAY_CLAIM_DETAIL-AUTH_DETAIL" OrElse NavController.Context = "PAY_CLAIM_ADJUSTMENT-PAY_CLAIM_DETAIL_CLADJ-AUTH_DETAIL" Then
                    If IsBODirty AndAlso Not State.HasDataBeenSaved Then
                        PopulateGrid()
                        DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO_CANCEL, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                        State.ActionInProgress = DetailPageCommand.Back
                    Else
                        'Me.ReturnToCallingPage(New ReturnType(Me.State.ClaimBO, Me.State.ClaimAuthDetailBO, Me.State.PartsInfoDV, IsBODirty Or Me.State.HasDataBeenSaved, IsNewClaimStatusBODirty))
                        NavController.Navigate(Me, "back", BuildPayClaimFormParameters(IsBODirty))
                    End If
                Else
                    'Me.ReturnToCallingPage(New ReturnType(Me.State.ClaimBO, Me.State.ClaimAuthDetailBO, Me.State.PartsInfoDV, IsBODirty, IsNewClaimStatusBODirty))
                    NavController.Navigate(Me, "back", BuildClaimFormParameters(IsBODirty OrElse State.HasDataBeenSaved, IsNewClaimStatusBODirty))
                End If
            End If

        Catch ex As ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, ErrController)
            DisplayMessage(Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
            State.ActionInProgress = DetailPageCommand.BackOnErr
            State.LastErrMsg = ErrController.Text
        End Try
    End Sub

    Function BuildClaimFormParameters(IsBODirty As Boolean, IsClaimStatusBODirty As Boolean) As ClaimForm.Parameters
        Return New ClaimForm.Parameters(State.ClaimAuthDetailBO.ClaimId, New ReturnType(State.ClaimBO, State.ClaimAuthDetailBO, State.PartsInfoDV, IsBODirty, IsClaimStatusBODirty))
    End Function

    Function BuildPayClaimFormParameters(IsBODirty As Boolean) As PayClaimForm.Parameters
        Return New PayClaimForm.Parameters(State.ClaimAuthDetailBO.ClaimId, IsBODirty, False)
    End Function

    Protected Sub btnSave_WRITE_Click(sender As Object, e As EventArgs) Handles btnSave_WRITE.Click
        Try
            DoSave()
            PopulateBOProperty(State.ClaimBO, "AuthorizedAmount", txtTotal)
        Catch ex As Exception
            HandleErrors(ex, ErrController)
        End Try
    End Sub

    Private Sub BtnUndo_Write_Click(sender As Object, e As EventArgs) Handles btnUndo_Write.Click
        Try
            UndoChanges()
        Catch ex As Exception
            HandleErrors(ex, ErrController)
        End Try
    End Sub

    Private Sub CancelEditing()
        Try
            Grid.SelectedIndex = NO_ITEM_SELECTED_INDEX
            If State.IsNewMode_PI Then
                'drop the row and the bo from the arraylist
                State.PartsInfoDV.Table.Rows.RemoveAt(State.LastRowIndexInUse)
                State.PartsInfoBOs.RemoveAt(State.PartsInfoBOs.Count - 1)
            End If
            ReturnFromEditing()

        Catch ex As Exception
            HandleErrors(ex, ErrController)
        End Try
    End Sub

    Private Sub UndoChanges()
        Try
            State.IsEditable_AD = False
            Grid.SelectedIndex = NO_ITEM_SELECTED_INDEX

            'refresh the claim BO
            State.ClaimBO = ClaimFacade.Instance.GetClaim(Of Claim)(State.ClaimBO.Id)

            'refresh the ClaimAuthDetailBO
            State.ClaimAuthDetailBO = Nothing
            PopulateClaimAuthDetailTab()
            PopulateADFormFromBO()

            'refresh the parts info grid and arraylist
            State.PartsInfoDV.Table = State.OreginalPartsInfoTable.Copy
            State.PartsInfoBOs = Nothing
            State.HasDataChanged = False
            State.HasclaimStatusBOChanged = False
            ReturnFromEditing()

            If State.IsReplacementClaim Then
                DisabledTabsList.Add(Tab_PartsInfo)
            End If

            EnableDisableFields()
        Catch ex As Exception
            HandleErrors(ex, ErrController)
        End Try
    End Sub

    Public Function IsIntegratedWithGVS() As Boolean
        Dim retVal As Boolean = False

        'If Me.State.ClaimBO.ServiceCenterObject.IntegratedWithGVS AndAlso Not Me.State.ClaimBO.ServiceCenterObject.IntegratedAsOf Is Nothing AndAlso Me.State.ClaimBO.CreatedDateTime.Value >= Me.State.ClaimBO.ServiceCenterObject.IntegratedAsOf.Value Then
        If State.ClaimBO IsNot Nothing AndAlso State.ClaimBO.ServiceCenterObject IsNot Nothing Then
            If State.ClaimBO.ServiceCenterObject.IntegratedWithGVS AndAlso State.ClaimBO.ServiceCenterObject.IntegratedAsOf IsNot Nothing AndAlso State.ClaimBO.CreatedDateTime.Value >= State.ClaimBO.ServiceCenterObject.IntegratedAsOf.Value Then
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

    Private Sub Cancel_PI_Button_Click(sender As Object, e As EventArgs) Handles Cancel_PI_Button.Click
        Try
            CancelEditing()
            ToggleMainButtons(True)
        Catch ex As Exception
            HandleErrors(ex, ErrController)
        End Try
    End Sub

#Region "Controlling Logic"

    <WebMethod()>
    <ScriptMethod()>
    Public Shared Function SetClientCultureFormat(txtBoxId As String, value As String,
        laborVal As String, partsVal As String, svcChargeVal As String,
        tripAmtVal As String, otherAmtVal As String, shippingAmtVal As String,
        diagnosticsAmtVal As String, dispositionAmtVal As String, totalTaxAmt As String) As String

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
        Dim confResponse As String = HiddenSaveChangesPromptResponse.Value
        If confResponse IsNot Nothing AndAlso confResponse = MSG_VALUE_YES Then
            If State.ActionInProgress <> DetailPageCommand.BackOnErr Then
                DoSave(True)
            End If

            Select Case State.ActionInProgress
                Case DetailPageCommand.Back
                    'DEF-17426
                    'Me.ReturnToCallingPage(New ReturnType(Me.State.ClaimBO, Me.State.ClaimAuthDetailBO, Me.State.PartsInfoDV, Me.State.HasDataChanged Or Me.State.HasDataBeenSaved, Me.State.HasclaimStatusBOChanged Or Me.State.HasclaimStatusBOSaved))
                    If NavController.Context = "PAY_CLAIM_DETAIL-AUTH_DETAIL" OrElse NavController.Context = "PAY_CLAIM_ADJUSTMENT-PAY_CLAIM_DETAIL_CLADJ-AUTH_DETAIL" Then
                        NavController.Navigate(Me, "back", BuildPayClaimFormParameters(State.HasDataChanged OrElse State.HasDataBeenSaved))
                    Else
                        NavController.Navigate(Me, "back", BuildClaimFormParameters(State.HasDataChanged OrElse State.HasDataBeenSaved, State.NewClaimStatusBO.IsDirty))
                    End If
                Case DetailPageCommand.BackOnErr
                    'DEF-17426
                    'Me.ReturnToCallingPage(New ReturnType(Me.State.ClaimBO, Me.State.ClaimAuthDetailBO, Me.State.PartsInfoDV, Me.State.HasDataChanged Or Me.State.HasDataBeenSaved, Me.State.HasclaimStatusBOChanged Or Me.State.HasclaimStatusBOSaved))
                    If NavController.Context = "PAY_CLAIM_DETAIL-AUTH_DETAIL" OrElse NavController.Context = "PAY_CLAIM_ADJUSTMENT-PAY_CLAIM_DETAIL_CLADJ-AUTH_DETAIL" Then
                        NavController.Navigate(Me, "back", BuildPayClaimFormParameters(State.HasDataChanged OrElse State.HasDataBeenSaved))
                    Else
                        NavController.Navigate(Me, "back", BuildClaimFormParameters(State.HasDataChanged OrElse State.HasDataBeenSaved, State.NewClaimStatusBO.IsDirty))
                    End If
            End Select
        ElseIf confResponse IsNot Nothing AndAlso confResponse = MSG_VALUE_NO Then
            Select Case State.ActionInProgress
                Case DetailPageCommand.Back
                    'DEF-17426
                    'Me.ReturnToCallingPage(New ReturnType(Me.State.ClaimBO, Me.State.ClaimAuthDetailBO, Me.State.PartsInfoDV, Me.State.HasDataChanged Or Me.State.HasDataBeenSaved, Me.State.HasclaimStatusBOChanged Or Me.State.HasclaimStatusBOSaved))
                    If NavController.Context = "PAY_CLAIM_DETAIL-AUTH_DETAIL" OrElse NavController.Context = "PAY_CLAIM_ADJUSTMENT-PAY_CLAIM_DETAIL_CLADJ-AUTH_DETAIL" Then
                        NavController.Navigate(Me, "back", BuildPayClaimFormParameters(State.HasDataChanged OrElse State.HasDataBeenSaved))
                    Else
                        NavController.Navigate(Me, "back", BuildClaimFormParameters(State.HasDataChanged OrElse State.HasDataBeenSaved, State.NewClaimStatusBO.IsDirty))
                    End If
                Case DetailPageCommand.BackOnErr
                    'DEF-17426
                    'Me.ReturnToCallingPage(New ReturnType(Me.State.ClaimBO, Me.State.ClaimAuthDetailBO, Me.State.PartsInfoDV, Me.State.HasDataChanged Or Me.State.HasDataBeenSaved, Me.State.HasclaimStatusBOChanged Or Me.State.HasclaimStatusBOSaved))
                    If NavController.Context = "PAY_CLAIM_DETAIL-AUTH_DETAIL" OrElse NavController.Context = "PAY_CLAIM_ADJUSTMENT-PAY_CLAIM_DETAIL_CLADJ-AUTH_DETAIL" Then
                        NavController.Navigate(Me, "back", BuildPayClaimFormParameters(State.HasDataChanged OrElse State.HasDataBeenSaved))
                    Else
                        NavController.Navigate(Me, "back", BuildClaimFormParameters(State.HasDataChanged OrElse State.HasDataBeenSaved, State.NewClaimStatusBO.IsDirty))
                    End If
            End Select
        End If
        'Clean after consuming the action
        State.ActionInProgress = DetailPageCommand.Nothing_
        HiddenSaveChangesPromptResponse.Value = ""

    End Sub

    Private Sub DoSave(Optional ByVal blnComingFromBack As Boolean = False)
        Try
            State.IsEditable_AD = False
            If State.PartsInfoBOs IsNot Nothing AndAlso State.PartsInfoBOs.Count > 0 AndAlso Not State.IsDone Then
                Dim objPartsInfo As PartsInfo = CType(State.PartsInfoBOs.Item(State.PartsInfoBOs.Count - 1), PartsInfo)
                PopulateBOFromForm(objPartsInfo, State.LastOperation)
            End If


            UpdatePartsAmountInClaimAuthDetailBO()
            Populate_AD_BOFromForm(False)

            PopulateApprove_DisApproveClaimStatus()

            Dim IsBODirty As Boolean = False
            If State.PartsInfoBOs IsNot Nothing Then
                For i As Integer = 0 To State.PartsInfoBOs.Count - 1
                    Dim objPartsInfo As PartsInfo = CType(State.PartsInfoBOs.Item(i), PartsInfo)
                    If (objPartsInfo.IsDirty) Then
                        IsBODirty = True
                        If Not objPartsInfo.IsDeleted Then objPartsInfo.Validate()
                    End If
                Next
            End If

            If Not IsBODirty Then
                IsBODirty = State.ClaimAuthDetailBO.IsDirty
            End If

            If Not IsBODirty AndAlso State.NewClaimStatusBO IsNot Nothing Then
                IsBODirty = State.NewClaimStatusBO.IsDirty
            End If

            If (IsBODirty) Then
                If GetSubTotal() <= 0 Then
                    ResetClaimAuthDetailBOToOriginal()
                    State.ClaimAuthDetailBO.TempClaimId = State.ClaimAuthDetailBO.ClaimId
                    'Me.State.ClaimAuthDetailBO.BeginEdit()
                    State.ClaimAuthDetailBO.Delete()

                End If

                State.ClaimAuthDetailBO.Save()
                State.IsAfterSave = True
                State.HasDataChanged = True
                State.HasDataBeenSaved = True
                State.HasclaimStatusBOSaved = True
                DisplayMessage(Message.RECORD_ADDED_OK, "", MSG_BTN_OK, MSG_TYPE_INFO)

            Else
                DisplayMessage(Message.MSG_RECORD_NOT_SAVED, "", MSG_BTN_OK, MSG_TYPE_INFO)
            End If

            State.IsDone = False

            If Not blnComingFromBack Then ReturnFromEditing()

        Catch ex As Exception
            'Me.State.ClaimAuthDetailBO.cancelEdit()
            Throw ex
        End Try
    End Sub

    Private Sub ResetClaimAuthDetailBOToOriginal()
        With State.ClaimAuthDetailBO
            If .LaborAmount IsNot Nothing Then .LaborAmount = State.OriginalClaimAuthDetailBO.LaborAmount.Value
            If .PartAmount IsNot Nothing Then .PartAmount = State.OriginalClaimAuthDetailBO.PartAmount.Value
            If .ServiceCharge IsNot Nothing Then .ServiceCharge = State.OriginalClaimAuthDetailBO.ServiceCharge.Value
            If .TripAmount IsNot Nothing Then .TripAmount = State.OriginalClaimAuthDetailBO.TripAmount.Value
            If .ShippingAmount IsNot Nothing Then .ShippingAmount = State.OriginalClaimAuthDetailBO.ShippingAmount.Value
            If .OtherAmount IsNot Nothing Then .OtherAmount = State.OriginalClaimAuthDetailBO.OtherAmount.Value
            If .OtherExplanation IsNot Nothing Then .OtherExplanation = State.OriginalClaimAuthDetailBO.OtherExplanation
        End With

    End Sub


#End Region


End Class
