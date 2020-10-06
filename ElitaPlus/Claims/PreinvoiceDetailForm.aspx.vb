Imports System.Collections.Generic
Imports Microsoft.VisualBasic
Partial Class PreinvoiceDetailForm
    Inherits ElitaPlusSearchPage

#Region " Web Form Designer Generated Code "


    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()

    End Sub

    Private Sub Page_Init(sender As System.Object, e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Constants"
    Public Const SELECT_ACTION_COMMAND As String = "SelectAction"
    Public Const URL As String = "~/Claims/PreinvoiceDetailForm.aspx"
    Public ds As New DataSet
    Public params As New ArrayList
    Private Const MSG_RECORD_SAVED_OK As String = "MSG_RECORD_SAVED_OK"
    Private Const CLAIMS As String = "CLAIMS"
    Public Const GRID_COL_CLAIM_ID_IDX As Integer = 0
    Public Const GRID_COL_CHK_BOX_IDX As Integer = 1
    Public Const GRID_COL_CLAIM_NUMBER_IDX As Integer = 2
    Public Const GRID_COL_MASTER_CENTER_IDX As Integer = 3
    Public Const GRID_COL_SERVICE_CENTER_IDX As Integer = 4
    Public Const GRID_COL_CLAIMS_TYPE_IDX As Integer = 5
    Public Const GRID_COL_AUTH_AMOUNT_IDX As Integer = 6
    Public Const GRID_COL_BONUS_AMOUNT_IDX As Integer = 7
    'Public Const GRID_COL_DEDUCTIBLE_IDX As Integer = 8
    Public Const GRID_COL_TOTAL_AMOUNT_IDX As Integer = 8

    Public Const GRID_COL_EDIT_CLAIM_NUMBER_CTRL As String = "btnEditClaimNumber"
    Private Const GRID_CTRL_NAME_CHECKBOX As String = "btnSelected"
    Private Const GRID_CTRL_NAME_HEADER_CHECKBOX As String = "HeaderLevelCheckBox"

    Protected checkValueArray() As String
#End Region

#Region "Navigation Controller"
    Public Const SESSION_KEY_BACKUP_STATE As String = "SESSION_KEY_BACKUP_STATE"

#End Region

#Region "Page Parameters"
    Public Class Parameters
        Public PreInvoiceID As Guid
        Public PayClaimID As Guid
        Public ViewOnly As Boolean = False
        Public isForClaimPayAdjust As Boolean
        Public hasDataChanged As Boolean
        Public BatchNumber As String
        Public Status As String
        Public CreatedDate As String
        Public DisplayDate As String
        Public ClaimsCount As String
        Public TotalBonusAmount As String
        Public TotalAmount As String
        Public Deductible As String
        Public selectedCompanyCode As String
        Public selectedCompanyDesc As String


        Public Sub New(CompanyCode As String, CompanyDesc As String, PreInvoiceID As Guid, batchNumber As String, status As String, createdDate As String, DisplayDate As String, claims As String, totalBonusAmount As String, totalAmount As String, deductible As String)
            Me.PreInvoiceID = PreInvoiceID
            Me.BatchNumber = batchNumber
            Me.Status = status
            Me.CreatedDate = createdDate
            Me.DisplayDate = DisplayDate
            ClaimsCount = claims
            Me.TotalBonusAmount = totalBonusAmount
            Me.TotalAmount = totalAmount
            Me.Deductible = deductible
            selectedCompanyCode = CompanyCode
            selectedCompanyDesc = CompanyDesc

        End Sub

    End Class
#End Region

#Region "Page Return Type"
    Public Class ReturnType
        Public LastOperation As DetailPageCommand
        Public EditingBo As PreInvoiceDetails
        Public BoChanged As Boolean
        Public Sub New(LastOp As DetailPageCommand, curEditingBo As PreInvoiceDetails, Optional ByVal boChanged As Boolean = False)
            LastOperation = LastOp
            EditingBo = curEditingBo
            Me.BoChanged = boChanged
        End Sub
    End Class
#End Region

#Region "Page State"
    Class BaseState
        Public NavCtrl As INavigationController
    End Class
    Class MyState
        'Invoicedetail variables
        Public pageParameters As Parameters
        Public MyBO As PreInvoiceDetails
        Public preInvBO As PreInvoice
        Public PreInvoiceId As Guid

        Public PreInvDetailId As Guid = Guid.Empty
        Public selectedClaimID As Guid = Guid.Empty
        Public selectedClaimNumber As String = String.Empty

        Public PreInvDetailSearchDv As PreInvoiceDetails.PreInvoiceDetailSearchDV = Nothing
        Public IsNew As Boolean = False
        Public IsEditMode As Boolean = False
        Public ActionInProgress As DetailPageCommand = DetailPageCommand.Nothing_
        Public LastErrMsg As String
        Public HasDataChanged As Boolean = False
        Public SortExpression As String = String.Empty
        Public PageIndex As Integer = 0
        Public PageSize As Integer = 30
        Public modalpgsize As Integer = 5
        Public SelectedGridValueToEdit As Guid
        Public IsGridVisible As Boolean = False
        Public SelectedPageSize As Integer
        Public groupcount As Integer = 0
        Public CompanyDV As DataView = Nothing
        Public ISinvoicedelete As Boolean = False
        Public searchBtnClicked As Boolean = False

        Public BatchNumber As String = String.Empty
        Public Status As String = String.Empty
        Public CreatedDate As String = String.Empty
        Public DisplayDate As String = String.Empty
        Public ClaimsCount As String = String.Empty
        Public TotalAmount As String = String.Empty
        Public Deductible As String = String.Empty
        Public TotalBonusAmount As String = String.Empty
        Public CompanyCode As String = String.Empty
        Public CompanyDesc As String = String.Empty

        Public serviceCenterName As String = String.Empty
        Public MasterCenterName As String = String.Empty
        Public serviceCenterId As Guid = Guid.Empty
        Public masterCenterId As Guid = Guid.Empty

        Public cmdProcessRecord As String = String.Empty

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
    Private ReadOnly Property IsNewInvoiceGroup() As Boolean
        Get
            ' Return Me.State.InvgrpBO.IsNew
        End Get

    End Property

    Private Sub PreinvoiceDetailForm_PageReturn(ReturnFromUrl As String, ReturnPar As Object) Handles Me.PageReturn
        Try
            'if coming from Claim Details form reload claim
            If CalledUrl = ClaimForm.URL Then
                State.PreInvDetailSearchDv = Nothing
                PopulateGrid()
            End If

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Page_PageCall(CallFromUrl As String, CallingParameters As Object) Handles MyBase.PageCall
        Try
            If Me.CallingParameters IsNot Nothing Then
                State.pageParameters = CType(Me.CallingParameters, Parameters)
                State.PreInvoiceId = State.pageParameters.PreInvoiceID
                State.preInvBO = New PreInvoice(State.PreInvoiceId)
                State.CompanyCode = State.pageParameters.selectedCompanyCode
                State.CompanyDesc = State.pageParameters.selectedCompanyDesc
                'btnReject.Attributes.Add("disabled", "disabled")
            End If

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

#End Region

#Region "Page Events"
    Private Sub UpdateBreadCrum()
        If (State IsNot Nothing) Then
            If (State IsNot Nothing) Then
                MasterPage.BreadCrum = MasterPage.PageTab & ElitaBase.Sperator & _
                    TranslationBase.TranslateLabelOrMessage("PRE_INVOICE_DETAIL")
                MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("PRE_INVOICE_DETAIL")
            End If
        End If
    End Sub


    Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Try

            MasterPage.MessageController.Clear_Hide()
            'Me.ResolveShippingFeeVisibility()

            If Not IsPostBack Then
                UpdateBreadCrum()
                cboPageSize.SelectedValue = CType(State.PageSize, String)
                AddControlMsg(btnApprove, Message.MSG_APPROVE_PRE_INVOICE, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, True)
                'Me.AddControlMsg(Me.btnReject, Message.MSG_REMOVE_CLAIM_FROM_INVOICING_CYCLE, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, True)
                LoadTopLevelData()
                populateBatchNumberData()
                TranslateGridHeader(Grid)
                If Not State.PreInvoiceId.Equals(Guid.Empty) Then
                    PopulateGrid()
                End If
            End If
            EnableDisablePageControls()

        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
        ShowMissingTranslations(MasterPage.MessageController)
    End Sub
#End Region

    Private Sub LoadTopLevelData()
        State.BatchNumber = State.preInvBO.BatchNumber 'Me.State.pageParameters.BatchNumber
        State.Status = LookupListNew.GetDescriptionFromId(LookupListNew.LK_PRE_INVOICE_STATUS, State.preInvBO.PreInvoiceStatusId) 'Me.State.pageParameters.Status
        State.CreatedDate = GetDateFormattedStringNullable(State.preInvBO.CreatedDate.Value) 'Me.State.pageParameters.CreatedDate
        State.DisplayDate = GetDateFormattedStringNullable(State.preInvBO.ScDisplayDate.Value) 'Me.State.pageParameters.DisplayDate
        State.ClaimsCount = State.preInvBO.TotalClaims.ToString() 'Me.State.pageParameters.ClaimsCount
        If (State.preInvBO.TotalAmount IsNot Nothing) Then
            State.TotalAmount = State.preInvBO.TotalAmount.ToString() 'Me.State.pageParameters.TotalAmount
        Else
            State.TotalAmount = State.preInvBO.TotalAmount
        End If
        If (State.preInvBO.TotalBonusAmount IsNot Nothing) Then
            State.TotalBonusAmount = State.preInvBO.TotalBonusAmount.ToString()
        Else
            State.TotalBonusAmount = State.preInvBO.TotalBonusAmount
        End If

        State.Deductible = State.pageParameters.Deductible

    End Sub

    Private Sub populateBatchNumberData()
        txtBatchNumber.Text = State.BatchNumber
        txtStatus.Text = State.Status
        txtCreatedDate.Text = State.CreatedDate
        txtDisplayDate.Text = State.DisplayDate
        txtClaimsCount.Text = State.ClaimsCount
        txtTotalAmount.Text = State.TotalAmount
        txtDeductible.Text = State.Deductible
        txtTotalBonusAmount.Text = State.TotalBonusAmount
        If (State.CompanyCode = String.Empty) Then
            trCompany.Visible = False
        Else
            txtCompanyCode.Text = State.CompanyCode
            txtCompanyDesc.Text = State.CompanyDesc
            trCompany.Visible = True
        End If
    End Sub

    Private Sub EnableDisablePageControls()
        If (State.Status <> LookupListNew.GetDescrionFromListCode("PREINVSTAT", "P")) Then
            ControlMgr.SetEnableControl(Me, btnReject, False)
            ControlMgr.SetEnableControl(Me, btnApprove, False)
        Else
            If IsDataGPageDirty() Then
                'Me.btnReject.Visible = True
                ControlMgr.SetEnableControl(Me, btnReject, True)
            Else
                ControlMgr.SetEnableControl(Me, btnReject, False)
            End If

            ControlMgr.SetEnableControl(Me, btnApprove, True)
        End If

    End Sub

    Private Sub PopulateGrid()
        Try

            If (txtMasterCtrName.Text <> String.Empty AndAlso inpMasterCenterId.Value <> String.Empty) Then
                State.masterCenterId = New Guid(inpMasterCenterId.Value)
            Else
                State.masterCenterId = Guid.Empty
            End If
            If (txtServiceCtrName.Text <> String.Empty AndAlso inpServiceCenterId.Value <> String.Empty) Then
                State.serviceCenterId = New Guid(inpServiceCenterId.Value)
            Else
                State.serviceCenterId = Guid.Empty
            End If
            ' PopulateStateFromSearchFields()
            If ((State.PreInvDetailSearchDv Is Nothing) OrElse (State.HasDataChanged)) Then
                State.PreInvDetailSearchDv = PreInvoiceDetails.LoadPreInvoiceClaims(State.PreInvoiceId, State.serviceCenterId, State.masterCenterId, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
            End If

            Grid.PageSize = State.PageSize
            If Not (State.PreInvDetailSearchDv Is Nothing) Then
                If State.searchBtnClicked Then
                    State.SortExpression = PreInvoiceDetails.PreInvoiceDetailSearchDV.COL_CLAIM_NUMBER
                    'Me.State.SortExpression &= " DESC"
                    State.PreInvDetailSearchDv.Sort = State.SortExpression
                Else
                    If (State.SortExpression = String.Empty) Then
                        State.SortExpression = PreInvoiceDetails.PreInvoiceDetailSearchDV.COL_CLAIM_NUMBER
                    End If
                    State.PreInvDetailSearchDv.Sort = State.SortExpression
                End If


                SetPageAndSelectedIndexFromGuid(State.PreInvDetailSearchDv, State.PreInvoiceId, Grid, State.PageIndex)
                Grid.DataSource = State.PreInvDetailSearchDv
                State.PageIndex = Grid.PageIndex
                'compare pre-invoice total with the acutal total amount for all the claims in the pre-invoice
                'Def-25238:Check the reocrd count of pre invoice details before computing Sum(authorization_amount).
                If (State.PreInvDetailSearchDv.Table.Rows.Count > 0) Then
                    Dim totalAuthAmount1 As Decimal = CDec(State.PreInvDetailSearchDv.Table.Compute("Sum(authorization_amount)", ""))
                    If (totalAuthAmount1 <> Decimal.Subtract(CDec(txtTotalAmount.Text), CDec(txtTotalBonusAmount.Text))) Then
                        PreInvoiceDetails.UpdatePreInvoiceTotal(State.PreInvoiceId, totalAuthAmount1)
                        txtTotalBonusAmount.Text = CDec(State.PreInvDetailSearchDv.Table.Compute("Sum(bonus_amount)", "")).ToString
                        txtTotalAmount.Text = CDec(State.PreInvDetailSearchDv.Table.Compute("Sum(total_amount)", "")).ToString
                        txtDeductible.Text = CDec(State.PreInvDetailSearchDv.Table.Compute("Sum(deductible)", "")).ToString
                    End If
                End If

                'Def-25238:End
                HighLightSortColumn(Grid, State.SortExpression, IsNewUI)
                Grid.DataBind()

                ControlMgr.SetVisibleControl(Me, Grid, True)
                ControlMgr.SetVisibleControl(Me, trPageSize, Grid.Visible)
                Session("recCount") = State.PreInvDetailSearchDv.Count

                If Grid.Visible Then
                    lblRecordCount.Text = State.PreInvDetailSearchDv.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                End If

                If (State.Status <> LookupListNew.GetDescrionFromListCode("PREINVSTAT", "P")) Then
                    Dim headerChkBox As CheckBox = New CheckBox
                    headerChkBox = CType(Grid.HeaderRow.FindControl(GRID_CTRL_NAME_HEADER_CHECKBOX), CheckBox)
                    If (headerChkBox IsNot Nothing) Then
                        headerChkBox.Enabled = False
                    End If
                End If
                If State.PreInvDetailSearchDv.Count = 0 Then
                    For Each gvRow As GridViewRow In Grid.Rows
                        gvRow.Visible = False
                        gvRow.Controls.Clear()
                    Next
                    lblPageSize.Visible = False
                    cboPageSize.Visible = False
                    colonSepertor.Visible = False
                    btnApprove.Visible = False
                    btnReject.Visible = False
                Else
                    BuildCheckBoxIDsArray()
                    lblPageSize.Visible = True
                    cboPageSize.Visible = True
                    colonSepertor.Visible = True
                    btnApprove.Visible = True
                    btnReject.Visible = True
                    'ControlMgr.SetEnableControl(Me, btnReject, False)
                End If

                If IsDataGPageDirty() Then
                    'Me.btnReject.Visible = True
                    ControlMgr.SetEnableControl(Me, btnReject, True)
                End If

            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

#Region "Grid Events"
    Public Property SortDirection() As String
        Get
            Return ViewState("SortDirection").ToString
        End Get
        Set(value As String)
            ViewState("SortDirection") = value
        End Set
    End Property

    Public Sub ItemCreated(sender As System.Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowCreated
        BaseItemCreated(sender, e)
    End Sub

    Function IsDataGPageDirty() As Boolean
        Dim Result As String = HiddenIsPageDirty.Value
        Return Result.Equals("YES")
    End Function

    Private Sub Grid_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Grid.PageIndexChanging
        Try
            State.PageIndex = e.NewPageIndex
            If IsDataGPageDirty() Then
                DisplayMessage(Message.MSG_PAGE_ALERT_PROMPT, "", MSG_BTN_OK, MSG_TYPE_ALERT)
                btnReject.Enabled = True
            Else
                Grid.PageIndex = e.NewPageIndex
                PopulateGrid()
            End If

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Grid_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles Grid.RowCommand
        Try
            Dim index As Integer
            If IsDataGPageDirty() Then
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.GridPageSize
                btnReject.Enabled = True
                DisplayMessage(Message.MSG_PAGE_ALERT_PROMPT, "", MSG_BTN_OK, MSG_TYPE_ALERT)
            ElseIf e.CommandName = SELECT_ACTION_COMMAND Then
                index = CInt(e.CommandArgument)

                State.selectedClaimID = New Guid(CType(Grid.DataKeys(index).Values(0), Byte())) 'New Guid(Me.Grid.Rows(index).Cells(Me.GRID_COL_CLAIM_ID_IDX).Text)
                callPage(ClaimForm.URL, State.selectedClaimID)
            End If

        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Grid_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowDataBound
        Try
            Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
            Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
            Dim btnEditItem As LinkButton
            If (e.Row.RowType = DataControlRowType.DataRow) _
               OrElse (e.Row.RowType = DataControlRowType.Separator) Then
                'Checkbox logic
                PopulateControlFromBOProperty(e.Row.Cells(GRID_COL_CLAIM_ID_IDX), dvRow(PreInvoiceDetails.PreInvoiceDetailSearchDV.COL_CLAIM_ID))
                Dim transIdStr As String = String.Empty
                Dim checkBox As CheckBox = New CheckBox
                checkBox = CType(e.Row.FindControl(GRID_CTRL_NAME_CHECKBOX), CheckBox)
                checkBox.Attributes.Add("onclick", "CheckboxAction('" & transIdStr & "','" & checkBox.ClientID & "','" & btnApprove.ClientID & "', '" & btnReject.ClientID & "','" & checkRecords.ClientID & "') ; ChangeHeaderAsNeeded();")

                If (State.Status <> LookupListNew.GetDescrionFromListCode("PREINVSTAT", "P")) Then
                    checkBox.Enabled = False
                End If
                If (e.Row.FindControl(GRID_COL_EDIT_CLAIM_NUMBER_CTRL) IsNot Nothing) Then

                    btnEditItem = CType(e.Row.Cells(GRID_COL_CLAIM_NUMBER_IDX).FindControl(GRID_COL_EDIT_CLAIM_NUMBER_CTRL), LinkButton)
                    'btnEditItem.CommandArgument = GetGuidStringFromByteArray(CType(dvRow(PreInvoice.PreinvoiceSearchDV.COL_PRE_INVOICE_ID), Byte()))
                    btnEditItem.CommandName = SELECT_ACTION_COMMAND
                    btnEditItem.Text = dvRow(PreInvoiceDetails.PreInvoiceDetailSearchDV.COL_CLAIM_NUMBER).ToString
                    State.selectedClaimNumber = btnEditItem.Text.Trim()
                End If

            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Grid_Sorting(sender As Object, e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles Grid.Sorting
        Try
            If State.SortExpression.StartsWith(e.SortExpression) Then
                If State.SortExpression.EndsWith(" DESC") Then
                    State.SortExpression = e.SortExpression
                Else
                    State.SortExpression &= " DESC"
                End If
            Else
                State.SortExpression = e.SortExpression
            End If
            State.PageIndex = 0
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Grid_PageSizeChanged(source As Object, e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
        Try
            If IsDataGPageDirty() Then
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.GridPageSize
                btnReject.Enabled = True
                DisplayMessage(Message.MSG_PAGE_ALERT_PROMPT, "", MSG_BTN_OK, MSG_TYPE_ALERT)
            Else
                Grid.PageIndex = NewCurrentPageIndex(Grid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
                State.PageSize = CType(cboPageSize.SelectedValue, Integer)
                PopulateGrid()
            End If

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
#End Region

#Region "Ajax State"

    Private Shared ReadOnly Property AjaxState() As MyState
        Get
            Return CType(NavPage.ClientNavigator.PageState, MyState)
        End Get

    End Property

#End Region

#Region "Ajax Autocomplete"

    <System.Web.Services.WebMethod()>
    <Script.Services.ScriptMethod()>
    Public Shared Function PopulateMasterCenterDrop(prefixText As String, count As Integer) As String()

        Dim countryId As Guid = New Guid(CType(LookupListNew.GetCountryLookupList(AjaxState.preInvBO.CompanyId)(0)(0), Byte()))

        Dim dv As DataView = LookupListNew.GetServiceCenterLookupList(countryId)

        Return AjaxController.BindAutoComplete(prefixText, dv)
    End Function


#End Region

    Private Sub BuildCheckBoxIDsArray()
        'Each time the data is bound to the grid we need to build up the CheckBoxIDs array

        'Get the header CheckBox
        Dim cbHeader As CheckBox = CType(Grid.HeaderRow.FindControl("HeaderLevelCheckBox"), CheckBox)

        'Run the ChangeCheckBoxState client-side function whenever the
        'header checkbox is checked/unchecked
        cbHeader.Attributes("onclick") = "ChangeAllCheckBoxStates(this.checked, '" & btnApprove.ClientID & "', '" & btnReject.ClientID & "');"

        'Add the CheckBox's ID to the client-side CheckBoxIDs array
        Dim ArrayValues As New List(Of String)
        ArrayValues.Add(String.Concat("'", cbHeader.ClientID, "'"))

        For Each gvr As GridViewRow In Grid.Rows
            'Get a programmatic reference to the CheckBox control
            Dim cb As CheckBox = CType(gvr.FindControl("btnSelected"), CheckBox)

            'If the checkbox is unchecked, ensure that the Header CheckBox is unchecked
            'cb.Attributes("onclick") = "ChangeHeaderAsNeeded();"

            'Add the CheckBox's ID to the client-side CheckBoxIDs array
            If cb IsNot Nothing Then ArrayValues.Add(String.Concat("'", cb.ClientID, "'"))
        Next

        'Output the array to the Literal control (CheckBoxIDsArray)
        CheckBoxIDsArray.Text = "<script type=""text/javascript"">" & vbCrLf &
                                "<!--" & vbCrLf &
                                String.Concat("var CheckBoxIDs =  new Array(", String.Join(",", ArrayValues.ToArray()), ");") & vbCrLf &
                                "// -->" & vbCrLf &
                                "</script>"
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As System.EventArgs) Handles btnSearch.Click
        Try
            State.searchBtnClicked = True
            State.PageIndex = 0
            State.IsGridVisible = True
            State.PreInvDetailSearchDv = Nothing
            State.searchBtnClicked = True
            PopulateGrid()
            State.searchBtnClicked = False
            If State.PreInvDetailSearchDv IsNot Nothing Then
                ValidSearchResultCountNew(State.PreInvDetailSearchDv.Count, True)
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnBack_Click(sender As System.Object, e As System.EventArgs) Handles btnBack.Click
        Try
            'Me.PopulateBOsFromForm()
            'If Me.IsDataGPageDirty() Then
            'Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", Me.MSG_BTN_YES_NO_CANCEL, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
            Dim myBo As PreInvoiceDetails = State.MyBO
            Dim retObj As ReturnType = New ReturnType(ElitaPlusPage.DetailPageCommand.Back, myBo)
            NavController = Nothing
            ReturnToCallingPage(retObj)
            'End If

        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
            DisplayMessage(Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR, "", MSG_BTN_YES_NO_CANCEL, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
            State.ActionInProgress = ElitaPlusPage.DetailPageCommand.BackOnErr
            State.LastErrMsg = MasterPage.MessageController.Text
        End Try
    End Sub

    Private Sub ApproveButton_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnApprove.Click
        Try

            State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Accept
            State.cmdProcessRecord = DALObjects.ClaimDAL.CMD_APPROVE
            ProcessRecords()
            HiddenIsPageDirty.Value = "NO"
            'reload the top level data
            State.preInvBO = New PreInvoice(State.PreInvoiceId)
            LoadTopLevelData()
            populateBatchNumberData()
            PopulateGrid()
            EnableDisablePageControls()

        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub

    'Private Sub btnReject_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReject.Click
    '    Try
    '        Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Accept
    '        Me.State.cmdProcessRecord = DALObjects.ClaimDAL.CMD_REJECT
    '        Me.ProcessCommand()
    '        PopulateGrid()
    '        HiddenIsPageDirty.Value = "NO"
    '        'reload the top level data
    '        Me.State.preInvBO = New PreInvoice(Me.State.PreInvoiceId)
    '        LoadTopLevelData()
    '        populateBatchNumberData()

    '        ControlMgr.SetEnableControl(Me, btnReject, False)
    '    Catch ex As Threading.ThreadAbortException
    '    Catch ex As Exception
    '        Me.HandleErrors(ex, Me.ErrControllerMaster)
    '    End Try
    'End Sub

    Private Sub ProcessCommand()
        'Process transaction
        Dim checkValues As String = String.Empty
        Dim i As Integer
        checkValueArray = checkRecords.Value.Split(":"c)

        For i = 0 To checkValueArray.Length - 1
            If (checkValueArray(i) IsNot Nothing And checkValueArray(i) <> "") Then
                checkValues = checkValueArray(i).ToString & ":" & checkValues
            End If
        Next
        checkRecords.Value = GetCheckedItemsValues()
        ProcessRecords()
        checkRecords.Value = ""
        State.PreInvDetailSearchDv = Nothing

    End Sub

    Private Function GetCheckedItemsValues() As String
        Dim checkedValues As String = String.Empty
        For Each gvrow As GridViewRow In Grid.Rows
            Dim CheckBox1 As CheckBox = DirectCast(gvrow.FindControl(GRID_CTRL_NAME_CHECKBOX), CheckBox)

            If CheckBox1.Checked Then
                'Dim claimId As Guid = New Guid(gvrow.Cells(Me.GRID_COL_CLAIM_ID_IDX).Text)
                Dim claimId As Guid = New Guid(CType(Grid.DataKeys(gvrow.RowIndex).Values(0), Byte()))

                checkedValues += GuidControl.GuidToHexString(claimId) & ":"
            End If

        Next
        Return checkedValues
    End Function

    Protected Function ProcessRecords() As Boolean
        Try
            Dim outputParameters() As DALObjects.DBHelper.DBHelperParameter
            If (State.cmdProcessRecord = DALObjects.ClaimDAL.CMD_APPROVE) Then
                outputParameters = PreInvoiceDetails.ApprovePreInvoiceClaims(State.preInvBO.CompanyId, State.PreInvoiceId)
            Else
                outputParameters = PreInvoiceDetails.RejectPreInvoiceClaims(State.preInvBO.CompanyId, checkRecords.Value, State.PreInvoiceId, txtRejectComments.Text)
            End If
            If CType(outputParameters(0).Value, Integer) = 0 Then
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.OK
                HiddenSaveChangesPromptResponse.Value = MSG_BTN_OK
                MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION)
                MasterPage.MessageController.Show()
            ElseIf CType(outputParameters(0).Value, Integer) = 100 Then
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.OK
                HiddenSaveChangesPromptResponse.Value = MSG_BTN_OK
                MasterPage.MessageController.AddError(TranslationBase.TranslateLabelOrMessage(CType(outputParameters(1).Value, String)), True)
                MasterPage.MessageController.Show()
            Else
                'Throw New GUIException(Assurant.ElitaPlus.Common.ErrorCodes.DB_ERROR, Assurant.ElitaPlus.Common.ErrorCodes.DB_ERROR)
                ErrControllerMaster.AddErrorAndShow(Assurant.ElitaPlus.Common.ErrorCodes.DB_ERROR)
            End If

            Return True
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
            Return False
        End Try
    End Function

    Private Sub btnClearSearch_Click(sender As Object, e As System.EventArgs) Handles btnClearSearch.Click
        State.SortExpression = PreInvoiceDetails.PreInvoiceDetailSearchDV.COL_CLAIM_NUMBER
        txtServiceCtrName.Text = String.Empty
        txtMasterCtrName.Text = String.Empty
        inpMasterCenterId.Value = String.Empty
        inpServiceCenterId.Value = String.Empty
        State.PreInvDetailSearchDv = Nothing

        PopulateGrid()
    End Sub

    Private Sub btnOk_Click(sender As Object, e As System.EventArgs) Handles btnOk.Click
        Try
            State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Accept
            State.cmdProcessRecord = DALObjects.ClaimDAL.CMD_REJECT
            ProcessCommand()
            PopulateGrid()
            HiddenIsPageDirty.Value = "NO"
            'reload the top level data
            State.preInvBO = New PreInvoice(State.PreInvoiceId)
            LoadTopLevelData()
            populateBatchNumberData()
            txtRejectComments.Text = String.Empty
            ControlMgr.SetEnableControl(Me, btnReject, False)
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As System.EventArgs) Handles btnCancel.Click
        txtRejectComments.Text = String.Empty
    End Sub
End Class