Imports System.Collections.Generic
Imports Microsoft.VisualBasic
Partial Class PreinvoiceDetailForm
    Inherits ElitaPlusSearchPage

#Region " Web Form Designer Generated Code "


    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()

    End Sub

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
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


        Public Sub New(ByVal CompanyCode As String, ByVal CompanyDesc As String, ByVal PreInvoiceID As Guid, ByVal batchNumber As String, ByVal status As String, ByVal createdDate As String, ByVal DisplayDate As String, ByVal claims As String, totalBonusAmount As String, totalAmount As String, deductible As String)
            Me.PreInvoiceID = PreInvoiceID
            Me.BatchNumber = batchNumber
            Me.Status = status
            Me.CreatedDate = createdDate
            Me.DisplayDate = DisplayDate
            Me.ClaimsCount = claims
            Me.TotalBonusAmount = totalBonusAmount
            Me.TotalAmount = totalAmount
            Me.Deductible = deductible
            Me.selectedCompanyCode = CompanyCode
            Me.selectedCompanyDesc = CompanyDesc

        End Sub

    End Class
#End Region

#Region "Page Return Type"
    Public Class ReturnType
        Public LastOperation As DetailPageCommand
        Public EditingBo As PreInvoiceDetails
        Public BoChanged As Boolean
        Public Sub New(ByVal LastOp As DetailPageCommand, ByVal curEditingBo As PreInvoiceDetails, Optional ByVal boChanged As Boolean = False)
            Me.LastOperation = LastOp
            Me.EditingBo = curEditingBo
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

    Private Sub PreinvoiceDetailForm_PageReturn(ByVal ReturnFromUrl As String, ByVal ReturnPar As Object) Handles Me.PageReturn
        Try
            'if coming from Claim Details form reload claim
            If Me.CalledUrl = ClaimForm.URL Then
                Me.State.PreInvDetailSearchDv = Nothing
                PopulateGrid()
            End If

        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Page_PageCall(ByVal CallFromUrl As String, ByVal CallingParameters As Object) Handles MyBase.PageCall
        Try
            If Not Me.CallingParameters Is Nothing Then
                Me.State.pageParameters = CType(Me.CallingParameters, Parameters)
                Me.State.PreInvoiceId = Me.State.pageParameters.PreInvoiceID
                Me.State.preInvBO = New PreInvoice(Me.State.PreInvoiceId)
                Me.State.CompanyCode = Me.State.pageParameters.selectedCompanyCode
                Me.State.CompanyDesc = Me.State.pageParameters.selectedCompanyDesc
                'btnReject.Attributes.Add("disabled", "disabled")
            End If

        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

#End Region

#Region "Page Events"
    Private Sub UpdateBreadCrum()
        If (Not Me.State Is Nothing) Then
            If (Not Me.State Is Nothing) Then
                Me.MasterPage.BreadCrum = Me.MasterPage.PageTab & ElitaBase.Sperator & _
                    TranslationBase.TranslateLabelOrMessage("PRE_INVOICE_DETAIL")
                Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("PRE_INVOICE_DETAIL")
            End If
        End If
    End Sub


    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Try

            Me.MasterPage.MessageController.Clear_Hide()
            'Me.ResolveShippingFeeVisibility()

            If Not Me.IsPostBack Then
                Me.UpdateBreadCrum()
                cboPageSize.SelectedValue = CType(Me.State.PageSize, String)
                Me.AddControlMsg(Me.btnApprove, Message.MSG_APPROVE_PRE_INVOICE, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, True)
                'Me.AddControlMsg(Me.btnReject, Message.MSG_REMOVE_CLAIM_FROM_INVOICING_CYCLE, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, True)
                LoadTopLevelData()
                populateBatchNumberData()
                TranslateGridHeader(Grid)
                If Not Me.State.PreInvoiceId.Equals(Guid.Empty) Then
                    PopulateGrid()
                End If
            End If
            EnableDisablePageControls()

        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
        Me.ShowMissingTranslations(Me.MasterPage.MessageController)
    End Sub
#End Region

    Private Sub LoadTopLevelData()
        Me.State.BatchNumber = Me.State.preInvBO.BatchNumber 'Me.State.pageParameters.BatchNumber
        Me.State.Status = LookupListNew.GetDescriptionFromId(LookupListNew.LK_PRE_INVOICE_STATUS, Me.State.preInvBO.PreInvoiceStatusId) 'Me.State.pageParameters.Status
        Me.State.CreatedDate = Me.State.preInvBO.CreatedDate.Value.ToString("dd-MMM-yyyy") 'Me.State.pageParameters.CreatedDate
        Me.State.DisplayDate = Me.State.preInvBO.ScDisplayDate.Value.ToString("dd-MMM-yyyy") 'Me.State.pageParameters.DisplayDate
        Me.State.ClaimsCount = Me.State.preInvBO.TotalClaims.ToString() 'Me.State.pageParameters.ClaimsCount
        If (Not Me.State.preInvBO.TotalAmount Is Nothing) Then
            Me.State.TotalAmount = Me.State.preInvBO.TotalAmount.ToString() 'Me.State.pageParameters.TotalAmount
        Else
            Me.State.TotalAmount = Me.State.preInvBO.TotalAmount
        End If
        If (Not Me.State.preInvBO.TotalBonusAmount Is Nothing) Then
            Me.State.TotalBonusAmount = Me.State.preInvBO.TotalBonusAmount.ToString()
        Else
            Me.State.TotalBonusAmount = Me.State.preInvBO.TotalBonusAmount
        End If

        Me.State.Deductible = Me.State.pageParameters.Deductible

    End Sub

    Private Sub populateBatchNumberData()
        txtBatchNumber.Text = Me.State.BatchNumber
        txtStatus.Text = Me.State.Status
        txtCreatedDate.Text = Me.State.CreatedDate
        txtDisplayDate.Text = Me.State.DisplayDate
        txtClaimsCount.Text = Me.State.ClaimsCount
        txtTotalAmount.Text = Me.State.TotalAmount
        txtDeductible.Text = Me.State.Deductible
        txtTotalBonusAmount.Text = Me.State.TotalBonusAmount
        If (Me.State.CompanyCode = String.Empty) Then
            trCompany.Visible = False
        Else
            txtCompanyCode.Text = Me.State.CompanyCode
            txtCompanyDesc.Text = Me.State.CompanyDesc
            trCompany.Visible = True
        End If
    End Sub

    Private Sub EnableDisablePageControls()
        If (Me.State.Status <> LookupListNew.GetDescrionFromListCode("PREINVSTAT", "P")) Then
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

            If (Me.txtMasterCtrName.Text <> String.Empty AndAlso Me.inpMasterCenterId.Value <> String.Empty) Then
                Me.State.masterCenterId = New Guid(Me.inpMasterCenterId.Value)
            Else
                Me.State.masterCenterId = Guid.Empty
            End If
            If (Me.txtServiceCtrName.Text <> String.Empty AndAlso Me.inpServiceCenterId.Value <> String.Empty) Then
                Me.State.serviceCenterId = New Guid(Me.inpServiceCenterId.Value)
            Else
                Me.State.serviceCenterId = Guid.Empty
            End If
            ' PopulateStateFromSearchFields()
            If ((Me.State.PreInvDetailSearchDv Is Nothing) OrElse (Me.State.HasDataChanged)) Then
                Me.State.PreInvDetailSearchDv = PreInvoiceDetails.LoadPreInvoiceClaims(Me.State.PreInvoiceId, Me.State.serviceCenterId, Me.State.masterCenterId, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
            End If

            Me.Grid.PageSize = Me.State.PageSize
            If Not (Me.State.PreInvDetailSearchDv Is Nothing) Then
                If Me.State.searchBtnClicked Then
                    Me.State.SortExpression = PreInvoiceDetails.PreInvoiceDetailSearchDV.COL_CLAIM_NUMBER
                    'Me.State.SortExpression &= " DESC"
                    Me.State.PreInvDetailSearchDv.Sort = Me.State.SortExpression
                Else
                    If (Me.State.SortExpression = String.Empty) Then
                        Me.State.SortExpression = PreInvoiceDetails.PreInvoiceDetailSearchDV.COL_CLAIM_NUMBER
                    End If
                    Me.State.PreInvDetailSearchDv.Sort = Me.State.SortExpression
                End If


                SetPageAndSelectedIndexFromGuid(Me.State.PreInvDetailSearchDv, Me.State.PreInvoiceId, Me.Grid, Me.State.PageIndex)
                Me.Grid.DataSource = Me.State.PreInvDetailSearchDv
                Me.State.PageIndex = Me.Grid.PageIndex
                'compare pre-invoice total with the acutal total amount for all the claims in the pre-invoice
                'Def-25238:Check the reocrd count of pre invoice details before computing Sum(authorization_amount).
                If (Me.State.PreInvDetailSearchDv.Table.Rows.Count > 0) Then
                    Dim totalAuthAmount1 As Decimal = CDec(Me.State.PreInvDetailSearchDv.Table.Compute("Sum(authorization_amount)", ""))
                    If (totalAuthAmount1 <> Decimal.Subtract(CDec(txtTotalAmount.Text), CDec(txtTotalBonusAmount.Text))) Then
                        PreInvoiceDetails.UpdatePreInvoiceTotal(Me.State.PreInvoiceId, totalAuthAmount1)
                        txtTotalBonusAmount.Text = CDec(Me.State.PreInvDetailSearchDv.Table.Compute("Sum(bonus_amount)", "")).ToString
                        txtTotalAmount.Text = CDec(Me.State.PreInvDetailSearchDv.Table.Compute("Sum(total_amount)", "")).ToString
                        txtDeductible.Text = CDec(Me.State.PreInvDetailSearchDv.Table.Compute("Sum(deductible)", "")).ToString
                    End If
                End If

                'Def-25238:End
                HighLightSortColumn(Me.Grid, Me.State.SortExpression, Me.IsNewUI)
                Me.Grid.DataBind()

                ControlMgr.SetVisibleControl(Me, Grid, True)
                ControlMgr.SetVisibleControl(Me, trPageSize, Me.Grid.Visible)
                Session("recCount") = Me.State.PreInvDetailSearchDv.Count

                If Me.Grid.Visible Then
                    Me.lblRecordCount.Text = Me.State.PreInvDetailSearchDv.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                End If

                If (Me.State.Status <> LookupListNew.GetDescrionFromListCode("PREINVSTAT", "P")) Then
                    Dim headerChkBox As CheckBox = New CheckBox
                    headerChkBox = CType(Grid.HeaderRow.FindControl(Me.GRID_CTRL_NAME_HEADER_CHECKBOX), CheckBox)
                    If (Not headerChkBox Is Nothing) Then
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
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

#Region "Grid Events"
    Public Property SortDirection() As String
        Get
            Return ViewState("SortDirection").ToString
        End Get
        Set(ByVal value As String)
            ViewState("SortDirection") = value
        End Set
    End Property

    Public Sub ItemCreated(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowCreated
        BaseItemCreated(sender, e)
    End Sub

    Function IsDataGPageDirty() As Boolean
        Dim Result As String = Me.HiddenIsPageDirty.Value
        Return Result.Equals("YES")
    End Function

    Private Sub Grid_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Grid.PageIndexChanging
        Try
            Me.State.PageIndex = e.NewPageIndex
            If IsDataGPageDirty() Then
                DisplayMessage(Message.MSG_PAGE_ALERT_PROMPT, "", MSG_BTN_OK, MSG_TYPE_ALERT)
                Me.btnReject.Enabled = True
            Else
                Me.Grid.PageIndex = e.NewPageIndex
                PopulateGrid()
            End If

        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Grid_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles Grid.RowCommand
        Try
            Dim index As Integer
            If IsDataGPageDirty() Then
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.GridPageSize
                Me.btnReject.Enabled = True
                DisplayMessage(Message.MSG_PAGE_ALERT_PROMPT, "", MSG_BTN_OK, MSG_TYPE_ALERT)
            ElseIf e.CommandName = SELECT_ACTION_COMMAND Then
                index = CInt(e.CommandArgument)

                Me.State.selectedClaimID = New Guid(CType(Grid.DataKeys(index).Values(0), Byte())) 'New Guid(Me.Grid.Rows(index).Cells(Me.GRID_COL_CLAIM_ID_IDX).Text)
                Me.callPage(ClaimForm.URL, Me.State.selectedClaimID)
            End If

        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Grid_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowDataBound
        Try
            Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
            Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
            Dim btnEditItem As LinkButton
            If (e.Row.RowType = DataControlRowType.DataRow) _
               OrElse (e.Row.RowType = DataControlRowType.Separator) Then
                'Checkbox logic
                Me.PopulateControlFromBOProperty(e.Row.Cells(Me.GRID_COL_CLAIM_ID_IDX), dvRow(PreInvoiceDetails.PreInvoiceDetailSearchDV.COL_CLAIM_ID))
                Dim transIdStr As String = String.Empty
                Dim checkBox As CheckBox = New CheckBox
                checkBox = CType(e.Row.FindControl(Me.GRID_CTRL_NAME_CHECKBOX), CheckBox)
                checkBox.Attributes.Add("onclick", "CheckboxAction('" & transIdStr & "','" & checkBox.ClientID & "','" & Me.btnApprove.ClientID & "', '" & Me.btnReject.ClientID & "','" & checkRecords.ClientID & "') ; ChangeHeaderAsNeeded();")

                If (Me.State.Status <> LookupListNew.GetDescrionFromListCode("PREINVSTAT", "P")) Then
                    checkBox.Enabled = False
                End If
                If (Not e.Row.FindControl(GRID_COL_EDIT_CLAIM_NUMBER_CTRL) Is Nothing) Then

                    btnEditItem = CType(e.Row.Cells(Me.GRID_COL_CLAIM_NUMBER_IDX).FindControl(GRID_COL_EDIT_CLAIM_NUMBER_CTRL), LinkButton)
                    'btnEditItem.CommandArgument = GetGuidStringFromByteArray(CType(dvRow(PreInvoice.PreinvoiceSearchDV.COL_PRE_INVOICE_ID), Byte()))
                    btnEditItem.CommandName = SELECT_ACTION_COMMAND
                    btnEditItem.Text = dvRow(PreInvoiceDetails.PreInvoiceDetailSearchDV.COL_CLAIM_NUMBER).ToString
                    Me.State.selectedClaimNumber = btnEditItem.Text.Trim()
                End If

            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Grid_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles Grid.Sorting
        Try
            If Me.State.SortExpression.StartsWith(e.SortExpression) Then
                If Me.State.SortExpression.EndsWith(" DESC") Then
                    Me.State.SortExpression = e.SortExpression
                Else
                    Me.State.SortExpression &= " DESC"
                End If
            Else
                Me.State.SortExpression = e.SortExpression
            End If
            Me.State.PageIndex = 0
            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Grid_PageSizeChanged(ByVal source As Object, ByVal e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
        Try
            If IsDataGPageDirty() Then
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.GridPageSize
                Me.btnReject.Enabled = True
                DisplayMessage(Message.MSG_PAGE_ALERT_PROMPT, "", MSG_BTN_OK, MSG_TYPE_ALERT)
            Else
                Grid.PageIndex = NewCurrentPageIndex(Grid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
                Me.State.PageSize = CType(cboPageSize.SelectedValue, Integer)
                Me.PopulateGrid()
            End If

        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
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
    Public Shared Function PopulateMasterCenterDrop(ByVal prefixText As String, ByVal count As Integer) As String()

        Dim countryId As Guid = New Guid(CType(LookupListNew.GetCountryLookupList(AjaxState.preInvBO.CompanyId)(0)(0), Byte()))

        Dim dv As DataView = LookupListNew.GetServiceCenterLookupList(countryId)

        Return AjaxController.BindAutoComplete(prefixText, dv)
    End Function


#End Region

    Private Sub BuildCheckBoxIDsArray()
        'Each time the data is bound to the grid we need to build up the CheckBoxIDs array

        'Get the header CheckBox
        Dim cbHeader As CheckBox = CType(Me.Grid.HeaderRow.FindControl("HeaderLevelCheckBox"), CheckBox)

        'Run the ChangeCheckBoxState client-side function whenever the
        'header checkbox is checked/unchecked
        cbHeader.Attributes("onclick") = "ChangeAllCheckBoxStates(this.checked, '" & Me.btnApprove.ClientID & "', '" & Me.btnReject.ClientID & "');"

        'Add the CheckBox's ID to the client-side CheckBoxIDs array
        Dim ArrayValues As New List(Of String)
        ArrayValues.Add(String.Concat("'", cbHeader.ClientID, "'"))

        For Each gvr As GridViewRow In Me.Grid.Rows
            'Get a programmatic reference to the CheckBox control
            Dim cb As CheckBox = CType(gvr.FindControl("btnSelected"), CheckBox)

            'If the checkbox is unchecked, ensure that the Header CheckBox is unchecked
            'cb.Attributes("onclick") = "ChangeHeaderAsNeeded();"

            'Add the CheckBox's ID to the client-side CheckBoxIDs array
            If Not cb Is Nothing Then ArrayValues.Add(String.Concat("'", cb.ClientID, "'"))
        Next

        'Output the array to the Literal control (CheckBoxIDsArray)
        CheckBoxIDsArray.Text = "<script type=""text/javascript"">" & vbCrLf &
                                "<!--" & vbCrLf &
                                String.Concat("var CheckBoxIDs =  new Array(", String.Join(",", ArrayValues.ToArray()), ");") & vbCrLf &
                                "// -->" & vbCrLf &
                                "</script>"
    End Sub

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            Me.State.searchBtnClicked = True
            Me.State.PageIndex = 0
            Me.State.IsGridVisible = True
            Me.State.PreInvDetailSearchDv = Nothing
            Me.State.searchBtnClicked = True
            PopulateGrid()
            Me.State.searchBtnClicked = False
            If Not Me.State.PreInvDetailSearchDv Is Nothing Then
                Me.ValidSearchResultCountNew(Me.State.PreInvDetailSearchDv.Count, True)
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Try
            'Me.PopulateBOsFromForm()
            'If Me.IsDataGPageDirty() Then
            'Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", Me.MSG_BTN_YES_NO_CANCEL, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
            Dim myBo As PreInvoiceDetails = Me.State.MyBO
            Dim retObj As ReturnType = New ReturnType(ElitaPlusPage.DetailPageCommand.Back, myBo)
            Me.NavController = Nothing
            Me.ReturnToCallingPage(retObj)
            'End If

        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
            Me.DisplayMessage(Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR, "", Me.MSG_BTN_YES_NO_CANCEL, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
            Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.BackOnErr
            Me.State.LastErrMsg = Me.MasterPage.MessageController.Text
        End Try
    End Sub

    Private Sub ApproveButton_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnApprove.Click
        Try

            Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Accept
            Me.State.cmdProcessRecord = DALObjects.ClaimDAL.CMD_APPROVE
            Me.ProcessRecords()
            HiddenIsPageDirty.Value = "NO"
            'reload the top level data
            Me.State.preInvBO = New PreInvoice(Me.State.PreInvoiceId)
            LoadTopLevelData()
            populateBatchNumberData()
            PopulateGrid()
            EnableDisablePageControls()

        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
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
            If (Not checkValueArray(i) Is Nothing And checkValueArray(i) <> "") Then
                checkValues = checkValueArray(i).ToString & ":" & checkValues
            End If
        Next
        checkRecords.Value = GetCheckedItemsValues()
        ProcessRecords()
        checkRecords.Value = ""
        Me.State.PreInvDetailSearchDv = Nothing

    End Sub

    Private Function GetCheckedItemsValues() As String
        Dim checkedValues As String = String.Empty
        For Each gvrow As GridViewRow In Me.Grid.Rows
            Dim CheckBox1 As CheckBox = DirectCast(gvrow.FindControl(Me.GRID_CTRL_NAME_CHECKBOX), CheckBox)

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
            If (Me.State.cmdProcessRecord = DALObjects.ClaimDAL.CMD_APPROVE) Then
                outputParameters = PreInvoiceDetails.ApprovePreInvoiceClaims(Me.State.preInvBO.CompanyId, Me.State.PreInvoiceId)
            Else
                outputParameters = PreInvoiceDetails.RejectPreInvoiceClaims(Me.State.preInvBO.CompanyId, checkRecords.Value, Me.State.PreInvoiceId, Me.txtRejectComments.Text)
            End If
            If CType(outputParameters(0).Value, Integer) = 0 Then
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.OK
                Me.HiddenSaveChangesPromptResponse.Value = Me.MSG_BTN_OK
                MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION)
                MasterPage.MessageController.Show()
            ElseIf CType(outputParameters(0).Value, Integer) = 100 Then
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.OK
                Me.HiddenSaveChangesPromptResponse.Value = Me.MSG_BTN_OK
                MasterPage.MessageController.AddError(TranslationBase.TranslateLabelOrMessage(CType(outputParameters(1).Value, String)), True)
                MasterPage.MessageController.Show()
            Else
                'Throw New GUIException(Assurant.ElitaPlus.Common.ErrorCodes.DB_ERROR, Assurant.ElitaPlus.Common.ErrorCodes.DB_ERROR)
                Me.ErrControllerMaster.AddErrorAndShow(Assurant.ElitaPlus.Common.ErrorCodes.DB_ERROR)
            End If

            Return True
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
            Return False
        End Try
    End Function

    Private Sub btnClearSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClearSearch.Click
        Me.State.SortExpression = PreInvoiceDetails.PreInvoiceDetailSearchDV.COL_CLAIM_NUMBER
        txtServiceCtrName.Text = String.Empty
        txtMasterCtrName.Text = String.Empty
        Me.inpMasterCenterId.Value = String.Empty
        Me.inpServiceCenterId.Value = String.Empty
        Me.State.PreInvDetailSearchDv = Nothing

        PopulateGrid()
    End Sub

    Private Sub btnOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnOk.Click
        Try
            Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Accept
            Me.State.cmdProcessRecord = DALObjects.ClaimDAL.CMD_REJECT
            Me.ProcessCommand()
            PopulateGrid()
            HiddenIsPageDirty.Value = "NO"
            'reload the top level data
            Me.State.preInvBO = New PreInvoice(Me.State.PreInvoiceId)
            LoadTopLevelData()
            populateBatchNumberData()
            Me.txtRejectComments.Text = String.Empty
            ControlMgr.SetEnableControl(Me, btnReject, False)
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub

    Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.txtRejectComments.Text = String.Empty
    End Sub
End Class