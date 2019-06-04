Partial Class AccountingSettingListForm
    Inherits ElitaPlus.ElitaPlusWebApp.ElitaPlusSearchPage

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents moDealerDrop As System.Web.UI.WebControls.DropDownList
    Protected WithEvents moServiceCenterDrop As System.Web.UI.WebControls.DropDownList
    Protected WithEvents moErrorController As ErrorController
    Protected WithEvents HiddenSaveChangesPromptResponse As System.Web.UI.HtmlControls.HtmlInputHidden
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
    Public Enum SearchType
        DealerGroup
        Dealer
        ServiceCenter
        Branch
        CommissionEntity
        Other
    End Enum

    
    'Public Const DEALER_GRID_COL_STATUS_IDX As Integer = 3
    Public Shared URL As String = "AccountingSettingPopup.aspx"
    Public Shared URL1 As String = "AccountingSettingListForm.aspx"

    Public Const GRID_COL_ACCT_SETTING_ID_IDX As Integer = 0
    Public Const GRID_CTRL_ACCT_SETTING_ID As String = "lblAcctSettingID"
    Public Const GRID_COL_DEALER_NAME_IDX As Integer = 1
    Public Const GRID_COL_DEALER_GROUP_NAME_IDX As Integer = 1
    Public Const GRID_COL_SERVICE_CENTER_NAME_IDX As Integer = 1
    Public Const GRID_COL_BRANCH_NAME_IDX As Integer = 2
    Public Const GRID_COL_COMMISSION_ENTITY_NAME_IDX As Integer = 1

    Private Const NOTHING_SELECTED As String = "00000000-0000-0000-0000-000000000000"
    Private Const ACCOUNTINGSETTINGLISTFORM As String = "AccountingSettingListForm.aspx"

    Private Const YES_STRING As String = "Y"
    Private Const NO_STRING As String = "N"
    Public Const TYPE_DEALER As String = "Dealer"
    Public Const TYPE_SERVICE_CENTER As String = "ServiceCenter"

#End Region

#Region "Page State"
    Private IsReturningFromChild As Boolean = False

    Class MyState
        Public sDealerGroupName As String
        Public sDealerGroupCode As String
        Public sDealerName As String
        Public sDealerCode As String
        Public sSCName As String
        Public sSCCode As String
        Public sBranchName As String
        Public sBranchCode As String
        Public sCommissionEntity As String

        Public selectedPageSize As Integer = DEFAULT_PAGE_SIZE
        Public IsGridVisible As Boolean
        Public HasDataChanged As Boolean
        Public AcctSettingsId As Guid = Guid.Empty

        Public searchDV As DataView = Nothing
        Public SortBy As String = AcctSetting.DealerGroupAcctSettingsDV.DEALER_GROUP_NAME
        Public PageIndex As Integer = 0
        Public TransType As SearchType = SearchType.DealerGroup

        Public Property PageDealerSize() As Integer
            Get
                Return selectedPageSize
            End Get
            Set(ByVal Value As Integer)
                selectedPageSize = Value
            End Set
        End Property

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
#End Region

#Region "PAGE EVENTS"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'Put user code to initialize the page here
        Me.moErrorController.Clear_Hide()

        Try
            If Not Page.IsPostBack Then
                SetDefaultButtons()
                ControlMgr.SetVisibleControl(Me, trPageSize, False)
                If IsReturningFromChild Then
                    PopulateFormFromState()
                Else
                    txtDealerGroupName.Focus()
                End If
                SetNoPostBackAttributes()
            End If
            SetClientAttributes()
            If Me.IsReturningFromChild = True Then
                Me.IsReturningFromChild = False
            End If
            'Write the scripts to toggle the search Field from dealer to svc center
            WritePageScripts()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.moErrorController)
        End Try
        Me.ShowMissingTranslations(Me.moErrorController)

    End Sub

    Private Sub Page_PageReturn(ByVal ReturnFromUrl As String, ByVal ReturnPar As Object) Handles MyBase.PageReturn
        Try
            Me.MenuEnabled = True
            Me.IsReturningFromChild = True
            Dim retObj As AccountingSettingForm.ReturnType = CType(ReturnPar, AccountingSettingForm.ReturnType)

            Me.State.HasDataChanged = retObj.HasDataChanged
            Select Case retObj.LastOperation
                Case ElitaPlusPage.DetailPageCommand.Back
                    If Not retObj Is Nothing Then
                        If Not retObj.EditingBo Is Nothing Then
                            Me.State.AcctSettingsId = retObj.EditingBo.Id
                        End If
                        Me.State.IsGridVisible = True
                    End If
                Case ElitaPlusPage.DetailPageCommand.Delete
                    Me.AddInfoMsg(Message.DELETE_RECORD_CONFIRMATION)
            End Select
        Catch ex As Exception
            Me.HandleErrors(ex, Me.moErrorController)
        End Try
    End Sub
#End Region

#Region "BUTTON CLICKS"

    Private Sub moBtnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles moBtnSearch.Click
        'Determine if they are searching for dealers, service centers, branches, or commission entity
        ClearSearchStrings()
        If Me.rdoDealer.Checked Then
            'Dealers
            With State
                .sDealerName = txtDealerName.Text.Trim
                .sDealerCode = txtDealerCode.Text.Trim
                If .sDealerName.Length = 0 And .sDealerCode.Length = 0 Then
                    Me.moErrorController.AddErrorAndShow(ElitaPlus.Common.ErrorCodes.GUI_SEARCH_FIELD_NOT_SUPPLIED_ERR)
                    Exit Sub
                End If

                If .sDealerName.Length > 0 And .sDealerCode.Length > 0 Then
                    Me.moErrorController.AddErrorAndShow(ElitaPlus.Common.ErrorCodes.GUI_ENTER_ONLY_DEALER_NAME_OR_CODE_ERR)
                    Exit Sub
                End If
                .TransType = SearchType.Dealer
            End With
        ElseIf rdoDealerGroup.Checked Then
            'dealer group
            With State
                .sDealerGroupName = txtDealerGroupName.Text.Trim
                .sDealerGroupCode = txtDealerGroupCode.Text.Trim
                If .sDealerGroupName.Length = 0 And .sDealerGroupCode.Length = 0 Then
                    Me.moErrorController.AddErrorAndShow(ElitaPlus.Common.ErrorCodes.GUI_SEARCH_FIELD_NOT_SUPPLIED_ERR)
                    Exit Sub
                End If

                If .sDealerGroupName.Length > 0 And .sDealerGroupCode.Length > 0 Then
                    Me.moErrorController.AddErrorAndShow(ElitaPlus.Common.ErrorCodes.GUI_ENTER_ONLY_DEALER_NAME_OR_CODE_ERR)
                    Exit Sub
                End If
                .TransType = SearchType.DealerGroup
            End With
        ElseIf Me.rdoServiceCenter.Checked Then
            ' Service Center
            With State
                .sSCCode = txtSCCode.Text.Trim
                .sSCName = txtSCName.Text.Trim
                If .sSCCode.Length = 0 And .sSCName.Length = 0 Then
                    Me.moErrorController.AddErrorAndShow(ElitaPlus.Common.ErrorCodes.GUI_SEARCH_FIELD_NOT_SUPPLIED_ERR)
                    Exit Sub
                End If
                If .sSCCode.Length > 0 And .sSCName.Length > 0 Then
                    Me.moErrorController.AddErrorAndShow(ElitaPlus.Common.ErrorCodes.GUI_ENTER_ONLY_SERVICE_CENTER_NAME_OR_CODE_ERR)
                    Exit Sub
                End If
                .TransType = SearchType.ServiceCenter
            End With
        ElseIf Me.rdoBranch.Checked Then
            'Branches
            With State
                .sBranchCode = txtBranchCode.Text.Trim
                .sBranchName = txtBranchName.Text.Trim
                If .sBranchName.Length = 0 And .sBranchCode.Length = 0 Then
                    Me.moErrorController.AddErrorAndShow(ElitaPlus.Common.ErrorCodes.GUI_SEARCH_FIELD_NOT_SUPPLIED_ERR)
                    Exit Sub
                End If

                If .sBranchName.Length > 0 And .sBranchCode.Length > 0 Then
                    Me.moErrorController.AddErrorAndShow(ElitaPlus.Common.ErrorCodes.GUI_ENTER_ONLY_BRANCH_NAME_OR_CODE_ERR)
                    Exit Sub
                End If
                .TransType = SearchType.Branch
            End With
        Else 'commission entity
            State.sCommissionEntity = txtCommEntity.Text.Trim
            If Me.txtCommEntity.Text.Trim.Length = 0 Then
                Me.moErrorController.AddErrorAndShow(ElitaPlus.Common.ErrorCodes.GUI_SEARCH_FIELD_NOT_SUPPLIED_ERR)
                Exit Sub
            End If
            State.TransType = SearchType.CommissionEntity
        End If
        State.AcctSettingsId = Guid.Empty
        State.PageIndex = 0
        State.searchDV = Nothing
        PopulateGrid(True)
    End Sub

    Private Sub moBtnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles moBtnClear.Click
        ClearSearchCriteria()
        ClearSearchStrings()
    End Sub

    Private Sub btnAdd_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd_WRITE.Click
        'SetSession()
        ClearSearchStrings()

        If Me.rdoDealer.Checked Then
            ' Dealer
            State.sDealerName = txtDealerName.Text.Trim
            State.sDealerCode = txtDealerCode.Text.Trim
            Me.callPage(AccountingSettingForm.URL, New AccountingSettingForm.ReturnType(AccountingSettingForm.ReturnType.TargetType.Dealer, False))
        ElseIf rdoDealerGroup.Checked Then
            'dealer group
            State.sDealerGroupName = txtDealerGroupName.Text.Trim
            State.sDealerGroupCode = txtDealerGroupCode.Text.Trim
            Me.callPage(AccountingSettingForm.URL, New AccountingSettingForm.ReturnType(AccountingSettingForm.ReturnType.TargetType.DealerGroup, False))
        ElseIf Me.rdoServiceCenter.Checked Then
            State.sSCCode = txtSCCode.Text.Trim
            State.sSCName = txtSCName.Text.Trim
            ' Service Center
            Me.callPage(AccountingSettingForm.URL, New AccountingSettingForm.ReturnType(AccountingSettingForm.ReturnType.TargetType.ServiceCenter, False))
        ElseIf rdoBranch.Checked Then
            'Branch
            State.sBranchCode = txtBranchCode.Text.Trim
            State.sBranchName = txtBranchName.Text.Trim
            Me.callPage(AccountingSettingForm.URL, New AccountingSettingForm.ReturnType(AccountingSettingForm.ReturnType.TargetType.Branch, False))
        ElseIf rdoCommEntity.Checked Then
            'Commission Entity
            State.sCommissionEntity = txtCommEntity.Text.Trim
            Me.callPage(AccountingSettingForm.URL, New AccountingSettingForm.ReturnType(AccountingSettingForm.ReturnType.TargetType.CommissionEntity, False))
        End If
    End Sub
#End Region

#Region "GridView Related"
    Private Sub Grid_PageIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Grid.PageIndexChanged
        Try
            Me.State.PageIndex = Grid.PageIndex
            Me.State.AcctSettingsId = Guid.Empty
            PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub

    Private Sub Grid_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Grid.PageIndexChanging
        Try
            Grid.PageIndex = e.NewPageIndex
            State.PageIndex = Grid.PageIndex
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub
    Private Sub Grid_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles Grid.RowCommand
        Try
            If e.CommandName = "Select" Then
                Dim lblCtrl As Label
                Dim row As GridViewRow = CType(CType(e.CommandSource, Control).Parent.Parent, GridViewRow)
                Dim RowInd As Integer = row.RowIndex
                lblCtrl = CType(Grid.Rows(RowInd).Cells(GRID_COL_ACCT_SETTING_ID_IDX).FindControl(Me.GRID_CTRL_ACCT_SETTING_ID), Label)
                Me.State.AcctSettingsId = New Guid(lblCtrl.Text)
                'Todo: call detail pages
                Dim TargetType As AccountingSettingForm.ReturnType.TargetType, strTitle As String, strOtherParam As String
                Select Case State.TransType
                    Case SearchType.DealerGroup
                        TargetType = AccountingSettingForm.ReturnType.TargetType.DealerGroup
                        strTitle = Grid.Rows(RowInd).Cells(GRID_COL_DEALER_GROUP_NAME_IDX).Text
                    Case SearchType.Dealer
                        TargetType = AccountingSettingForm.ReturnType.TargetType.Dealer
                        strTitle = Grid.Rows(RowInd).Cells(GRID_COL_DEALER_NAME_IDX).Text
                    Case SearchType.ServiceCenter
                        TargetType = AccountingSettingForm.ReturnType.TargetType.ServiceCenter
                        strTitle = Grid.Rows(RowInd).Cells(GRID_COL_SERVICE_CENTER_NAME_IDX).Text
                    Case SearchType.Branch
                        TargetType = AccountingSettingForm.ReturnType.TargetType.Branch
                        strTitle = Grid.Rows(RowInd).Cells(GRID_COL_DEALER_NAME_IDX).Text
                        strOtherParam = Grid.Rows(RowInd).Cells(GRID_COL_BRANCH_NAME_IDX).Text
                    Case SearchType.CommissionEntity
                        TargetType = AccountingSettingForm.ReturnType.TargetType.CommissionEntity
                        strTitle = Grid.Rows(RowInd).Cells(GRID_COL_COMMISSION_ENTITY_NAME_IDX).Text
                    Case Else
                        TargetType = AccountingSettingForm.ReturnType.TargetType.Other
                End Select
                callPage(AccountingSettingForm.URL, New AccountingSettingForm.ReturnType(State.AcctSettingsId, TargetType, Guid.Empty, strTitle, False, strOtherParam))
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub

    Private Sub Grid_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowCreated
        Try
            BaseItemCreated(sender, e)
        Catch ex As Exception
            HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub

    Private Sub Grid_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowDataBound
        Try
            BaseItemBound(sender, e)
        Catch ex As Exception
            HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub

    Private Sub Grid_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles Grid.Sorting
        Try
            Dim strSort As String = e.SortExpression
            With State
                If .SortBy.StartsWith(e.SortExpression) Then
                    If Not .SortBy.EndsWith(" DESC") Then
                        strSort = strSort & " DESC"
                    End If
                End If
                .SortBy = strSort
            End With
            PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub

    Private Sub Grid_PageSizeChanged(ByVal source As Object, ByVal e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
        Try
            State.selectedPageSize = CType(cboPageSize.SelectedValue, Integer)
            Me.State.PageIndex = NewCurrentPageIndex(Grid, State.searchDV.Count, State.selectedPageSize)
            Me.Grid.PageIndex = Me.State.PageIndex
            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub
#End Region

#Region "Client Attributes"

    Private Sub SetDefaultButtons()
        Me.SetDefaultButton(Me.txtDealerGroupName, moBtnSearch)
        Me.SetDefaultButton(Me.txtDealerGroupCode, moBtnSearch)
        Me.SetDefaultButton(Me.txtDealerName, moBtnSearch)
        Me.SetDefaultButton(Me.txtDealerCode, moBtnSearch)
        Me.SetDefaultButton(Me.txtSCName, moBtnSearch)
        Me.SetDefaultButton(Me.txtSCCode, moBtnSearch)
        Me.SetDefaultButton(Me.txtBranchName, moBtnSearch)
        Me.SetDefaultButton(Me.txtBranchCode, moBtnSearch)
        Me.SetDefaultButton(Me.txtCommEntity, moBtnSearch)
    End Sub

    Private Sub SetNoPostBackAttributes()
        rdoDealer.Attributes("onclick") = "toggleSelection('d');"
        rdoServiceCenter.Attributes("onclick") = "toggleSelection('s');"
        rdoBranch.Attributes("onclick") = "toggleSelection('b');"
        rdoDealerGroup.Attributes("onclick") = "toggleSelection('g');"
        rdoCommEntity.Attributes("onclick") = "toggleSelection('c');"
    End Sub

    Private Sub SetClientAttributes()
        ControlMgr.SetEnableControl(Me, txtDealerGroupName, rdoDealerGroup.Checked)
        ControlMgr.SetEnableControl(Me, txtDealerGroupCode, rdoDealerGroup.Checked)
        ControlMgr.SetEnableControl(Me, txtDealerName, rdoDealer.Checked)
        ControlMgr.SetEnableControl(Me, txtDealerCode, rdoDealer.Checked)
        ControlMgr.SetEnableControl(Me, txtSCName, rdoServiceCenter.Checked)
        ControlMgr.SetEnableControl(Me, txtSCCode, rdoServiceCenter.Checked)
        ControlMgr.SetEnableControl(Me, txtBranchName, rdoBranch.Checked)
        ControlMgr.SetEnableControl(Me, txtBranchCode, rdoBranch.Checked)
        ControlMgr.SetEnableControl(Me, txtCommEntity, rdoCommEntity.Checked)
    End Sub

    Private Sub WritePageScripts()

        Dim str As New System.Text.StringBuilder

        str.Append("<script language=javascript>")
        str.Append("function toggleSelection(v){")
        str.Append("if (v == 'd'){")
        'Dealer
        str.Append("document.getElementById('" + txtDealerName.ClientID + "').disabled = false;")
        str.Append("document.getElementById('" + txtDealerCode.ClientID + "').disabled = false;")
        str.Append("document.getElementById('" + txtDealerName.ClientID + "').focus();")
        str.Append("document.getElementById('" + txtDealerGroupName.ClientID + "').disabled = true;")
        str.Append("document.getElementById('" + txtDealerGroupCode.ClientID + "').disabled = true;")
        str.Append("document.getElementById('" + txtDealerGroupName.ClientID + "').value = '';")
        str.Append("document.getElementById('" + txtDealerGroupCode.ClientID + "').value = '';")
        str.Append("document.getElementById('" + txtSCName.ClientID + "').disabled = true;")
        str.Append("document.getElementById('" + txtSCCode.ClientID + "').disabled = true;")
        str.Append("document.getElementById('" + txtSCName.ClientID + "').value = '';")
        str.Append("document.getElementById('" + txtSCCode.ClientID + "').value = '';")
        str.Append("document.getElementById('" + txtBranchName.ClientID + "').disabled = true;")
        str.Append("document.getElementById('" + txtBranchCode.ClientID + "').disabled = true;")
        str.Append("document.getElementById('" + txtBranchName.ClientID + "').value = '';")
        str.Append("document.getElementById('" + txtBranchCode.ClientID + "').value = '';")
        str.Append("document.getElementById('" + txtCommEntity.ClientID + "').disabled = true;")
        str.Append("document.getElementById('" + txtCommEntity.ClientID + "').value = '';")
        str.Append("} else if (v == 'g') {")        'dealer group
        str.Append("document.getElementById('" + txtDealerGroupName.ClientID + "').disabled = false;")
        str.Append("document.getElementById('" + txtDealerGroupName.ClientID + "').focus();")
        str.Append("document.getElementById('" + txtDealerGroupCode.ClientID + "').disabled = false;")
        str.Append("document.getElementById('" + txtDealerName.ClientID + "').disabled = true;")
        str.Append("document.getElementById('" + txtDealerCode.ClientID + "').disabled = true;")
        str.Append("document.getElementById('" + txtDealerName.ClientID + "').value = '';")
        str.Append("document.getElementById('" + txtDealerCode.ClientID + "').value = '';")
        str.Append("document.getElementById('" + txtSCName.ClientID + "').disabled = true;")
        str.Append("document.getElementById('" + txtSCCode.ClientID + "').disabled = true;")
        str.Append("document.getElementById('" + txtSCName.ClientID + "').value = '';")
        str.Append("document.getElementById('" + txtSCCode.ClientID + "').value = '';")
        str.Append("document.getElementById('" + txtBranchName.ClientID + "').disabled = true;")
        str.Append("document.getElementById('" + txtBranchCode.ClientID + "').disabled = true;")
        str.Append("document.getElementById('" + txtBranchName.ClientID + "').value = '';")
        str.Append("document.getElementById('" + txtBranchCode.ClientID + "').value = '';")
        str.Append("document.getElementById('" + txtCommEntity.ClientID + "').disabled = true;")
        str.Append("document.getElementById('" + txtCommEntity.ClientID + "').value = '';")
        str.Append("} else if (v == 's') {")        'Service Center
        str.Append("document.getElementById('" + txtDealerGroupName.ClientID + "').disabled = true;")
        str.Append("document.getElementById('" + txtDealerGroupCode.ClientID + "').disabled = true;")
        str.Append("document.getElementById('" + txtDealerGroupName.ClientID + "').value = '';")
        str.Append("document.getElementById('" + txtDealerGroupCode.ClientID + "').value = '';")
        str.Append("document.getElementById('" + txtDealerName.ClientID + "').disabled = true;")
        str.Append("document.getElementById('" + txtDealerCode.ClientID + "').disabled = true;")
        str.Append("document.getElementById('" + txtDealerName.ClientID + "').value = '';")
        str.Append("document.getElementById('" + txtDealerCode.ClientID + "').value = '';")
        str.Append("document.getElementById('" + txtSCName.ClientID + "').disabled = false;")
        str.Append("document.getElementById('" + txtSCName.ClientID + "').focus();")
        str.Append("document.getElementById('" + txtSCCode.ClientID + "').disabled = false;")
        str.Append("document.getElementById('" + txtBranchName.ClientID + "').disabled = true;")
        str.Append("document.getElementById('" + txtBranchCode.ClientID + "').disabled = true;")
        str.Append("document.getElementById('" + txtBranchName.ClientID + "').value = '';")
        str.Append("document.getElementById('" + txtBranchCode.ClientID + "').value = '';")
        str.Append("document.getElementById('" + txtCommEntity.ClientID + "').disabled = true;")
        str.Append("document.getElementById('" + txtCommEntity.ClientID + "').value = '';")
        str.Append("} else if (v == 'b') {")        'Branch
        str.Append("document.getElementById('" + txtDealerGroupName.ClientID + "').disabled = true;")
        str.Append("document.getElementById('" + txtDealerGroupCode.ClientID + "').disabled = true;")
        str.Append("document.getElementById('" + txtDealerGroupName.ClientID + "').value = '';")
        str.Append("document.getElementById('" + txtDealerGroupCode.ClientID + "').value = '';")
        str.Append("document.getElementById('" + txtDealerName.ClientID + "').disabled = true;")
        str.Append("document.getElementById('" + txtDealerCode.ClientID + "').disabled = true;")
        str.Append("document.getElementById('" + txtDealerName.ClientID + "').value = '';")
        str.Append("document.getElementById('" + txtDealerCode.ClientID + "').value = '';")
        str.Append("document.getElementById('" + txtSCName.ClientID + "').disabled = true;")
        str.Append("document.getElementById('" + txtSCCode.ClientID + "').disabled = true;")
        str.Append("document.getElementById('" + txtSCName.ClientID + "').value = '';")
        str.Append("document.getElementById('" + txtSCCode.ClientID + "').value = '';")
        str.Append("document.getElementById('" + txtBranchName.ClientID + "').disabled = false;")
        str.Append("document.getElementById('" + txtBranchCode.ClientID + "').disabled = false;")
        str.Append("document.getElementById('" + txtBranchName.ClientID + "').focus();")
        str.Append("document.getElementById('" + txtCommEntity.ClientID + "').disabled = true;")
        str.Append("document.getElementById('" + txtCommEntity.ClientID + "').value = '';")
        str.Append("} else if (v == 'c') {") 'Commission Entity
        str.Append("document.getElementById('" + txtDealerGroupName.ClientID + "').disabled = true;")
        str.Append("document.getElementById('" + txtDealerGroupCode.ClientID + "').disabled = true;")
        str.Append("document.getElementById('" + txtDealerGroupName.ClientID + "').value = '';")
        str.Append("document.getElementById('" + txtDealerGroupCode.ClientID + "').value = '';")
        str.Append("document.getElementById('" + txtDealerName.ClientID + "').disabled = true;")
        str.Append("document.getElementById('" + txtDealerCode.ClientID + "').disabled = true;")
        str.Append("document.getElementById('" + txtDealerName.ClientID + "').value = '';")
        str.Append("document.getElementById('" + txtDealerCode.ClientID + "').value = '';")
        str.Append("document.getElementById('" + txtSCName.ClientID + "').disabled = true;")
        str.Append("document.getElementById('" + txtSCCode.ClientID + "').disabled = true;")
        str.Append("document.getElementById('" + txtSCName.ClientID + "').value = '';")
        str.Append("document.getElementById('" + txtSCCode.ClientID + "').value = '';")
        str.Append("document.getElementById('" + txtBranchName.ClientID + "').disabled = true;")
        str.Append("document.getElementById('" + txtBranchCode.ClientID + "').disabled = true;")
        str.Append("document.getElementById('" + txtBranchName.ClientID + "').value = '';")
        str.Append("document.getElementById('" + txtBranchCode.ClientID + "').value = '';")
        str.Append("document.getElementById('" + txtCommEntity.ClientID + "').disabled = false;")
        str.Append("document.getElementById('" + txtCommEntity.ClientID + "').focus();")
        str.Append("}}")
        str.Append("</script>")

        Me.RegisterClientScriptBlock("TOGGLESEARCH", str.ToString)

    End Sub

#End Region

#Region "CONTROLLING LOGIC"

    Protected Sub CheckIfComingFromAcctSettTypeConfirm()
        Dim confResponse As String = Me.HiddenASTypePromptResponse.Value
        If Not confResponse Is Nothing AndAlso confResponse = TYPE_DEALER Then
            Me.callPage(AccountingSettingForm.URL, New AccountingSettingForm.ReturnType(AccountingSettingForm.ReturnType.TargetType.Dealer, False))
        ElseIf Not confResponse Is Nothing AndAlso confResponse = TYPE_SERVICE_CENTER Then
            Me.callPage(AccountingSettingForm.URL, New AccountingSettingForm.ReturnType(AccountingSettingForm.ReturnType.TargetType.ServiceCenter, False))
        End If
        Me.HiddenASTypePromptResponse.Value = ""
    End Sub

    Private Sub ClearSearchStrings()
        With State
            .sDealerGroupName = ""
            .sDealerGroupCode = ""
            .sDealerName = ""
            .sDealerCode = ""
            .sSCName = ""
            .sSCCode = ""
            .sBranchName = ""
            .sBranchCode = ""
            .sCommissionEntity = ""
        End With
    End Sub

    Private Sub AddColumnsToGrid(ByVal st As SearchType)
        Select Case st
            Case SearchType.Dealer
                With CType(Grid.Columns(1), BoundField)
                    .DataField = AcctSetting.DealerAcctSettingsDV.DEALER_NAME
                    .HeaderText = TranslationBase.TranslateLabelOrMessage("DEALER_NAME")
                    .SortExpression = AcctSetting.DealerAcctSettingsDV.DEALER_NAME
                    .HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                    .ItemStyle.CssClass = "LeftCol"
                End With

                With CType(Grid.Columns(2), BoundField)
                    .DataField = AcctSetting.DealerAcctSettingsDV.DEALER
                    .HeaderText = TranslationBase.TranslateLabelOrMessage("DEALER_CODE")
                    .SortExpression = AcctSetting.DealerAcctSettingsDV.DEALER
                    .HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                End With

                With CType(Grid.Columns(3), BoundField)
                    .DataField = ""
                    .Visible = False
                End With
            Case SearchType.DealerGroup
                With CType(Grid.Columns(1), BoundField)
                    .DataField = AcctSetting.DealerGroupAcctSettingsDV.DEALER_GROUP_NAME
                    .HeaderText = TranslationBase.TranslateLabelOrMessage("DEALER_GROUP_NAME")
                    .SortExpression = AcctSetting.DealerGroupAcctSettingsDV.DEALER_GROUP_NAME
                    .HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                    .ItemStyle.CssClass = "LeftCol"
                End With

                With CType(Grid.Columns(2), BoundField)
                    .DataField = AcctSetting.DealerGroupAcctSettingsDV.DEALER_GROUP_CODE
                    .HeaderText = TranslationBase.TranslateLabelOrMessage("DEALER_GROUP_CODE")
                    .SortExpression = AcctSetting.DealerGroupAcctSettingsDV.DEALER_GROUP_CODE
                    .HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                End With

                With CType(Grid.Columns(3), BoundField)
                    .DataField = ""
                    .Visible = False
                End With
            Case SearchType.ServiceCenter
                With CType(Grid.Columns(1), BoundField)
                    .DataField = AcctSetting.ServiceCenterAcctSettingsDV.DESCRIPTION
                    .HeaderText = TranslationBase.TranslateLabelOrMessage("SERVICE_CENTER_NAME")
                    .SortExpression = AcctSetting.ServiceCenterAcctSettingsDV.DESCRIPTION
                    .HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                    .ItemStyle.CssClass = "LeftCol"
                End With

                With CType(Grid.Columns(2), BoundField)
                    .DataField = AcctSetting.ServiceCenterAcctSettingsDV.CODE
                    .HeaderText = TranslationBase.TranslateLabelOrMessage("SERVICE_CENTER_CODE")
                    .SortExpression = AcctSetting.ServiceCenterAcctSettingsDV.CODE
                    .HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                End With

                With CType(Grid.Columns(3), BoundField)
                    .DataField = ""
                    .Visible = False
                End With
            Case SearchType.Branch
                With CType(Grid.Columns(1), BoundField)
                    .DataField = AcctSetting.BranchAcctSettingsDV.DEALER_NAME
                    .HeaderText = TranslationBase.TranslateLabelOrMessage("DEALER_NAME")
                    .SortExpression = AcctSetting.BranchAcctSettingsDV.DEALER_NAME
                    .HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                    .ItemStyle.CssClass = "LeftCol"
                End With

                With CType(Grid.Columns(2), BoundField)
                    .DataField = AcctSetting.BranchAcctSettingsDV.BRANCH_NAME
                    .HeaderText = TranslationBase.TranslateLabelOrMessage("BRANCH_NAME")
                    .SortExpression = AcctSetting.BranchAcctSettingsDV.BRANCH_NAME
                    .HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                    .ItemStyle.CssClass = "LeftCol"
                End With

                With CType(Grid.Columns(3), BoundField)
                    .DataField = AcctSetting.BranchAcctSettingsDV.BRANCH_CODE
                    .HeaderText = TranslationBase.TranslateLabelOrMessage("BRANCH_CODE")
                    .SortExpression = AcctSetting.BranchAcctSettingsDV.BRANCH_CODE
                    .HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                    .Visible = True
                End With
            Case SearchType.CommissionEntity
                With CType(Grid.Columns(1), BoundField)
                    .DataField = AcctSetting.CommissionEntityAcctSettingsDV.ENTITY_NAME
                    .HeaderText = TranslationBase.TranslateLabelOrMessage("COMMISSION_ENTITY")
                    .SortExpression = AcctSetting.CommissionEntityAcctSettingsDV.ENTITY_NAME
                    .HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                    .ItemStyle.CssClass = "LeftCol"
                End With

                With CType(Grid.Columns(2), BoundField)
                    .DataField = ""
                    .Visible = False
                End With

                With CType(Grid.Columns(3), BoundField)
                    .DataField = ""
                    .Visible = False
                End With
        End Select
    End Sub

    Sub GetSearchDataView(ByVal st As SearchType)
        Dim _d As AcctSetting
        Select Case st
            Case SearchType.Dealer
                _d = New AcctSetting
                State.searchDV = _d.GetDealers(txtDealerName.Text.Trim, txtDealerCode.Text.Trim)
                State.SortBy = AcctSetting.DealerAcctSettingsDV.DEALER_NAME
            Case SearchType.DealerGroup
                State.searchDV = AcctSetting.GetDealerGroups(txtDealerGroupName.Text.Trim, txtDealerGroupCode.Text.Trim)
                State.SortBy = AcctSetting.DealerGroupAcctSettingsDV.DEALER_GROUP_NAME
            Case SearchType.ServiceCenter
                _d = New AcctSetting
                State.searchDV = _d.GetServiceCenters(Me.txtSCName.Text.Trim, Me.txtSCCode.Text.Trim)
                State.SortBy = AcctSetting.ServiceCenterAcctSettingsDV.DESCRIPTION
            Case SearchType.Branch
                _d = New AcctSetting
                State.searchDV = _d.GetBranches(txtBranchName.Text.Trim, txtBranchCode.Text.Trim)
                State.SortBy = AcctSetting.BranchAcctSettingsDV.DEALER_NAME
            Case SearchType.CommissionEntity
                State.searchDV = AcctSetting.GetCommissionEntities(txtCommEntity.Text.Trim)
                State.SortBy = AcctSetting.CommissionEntityAcctSettingsDV.ENTITY_NAME
        End Select
        State.searchDV.Sort = State.SortBy
    End Sub

    Private Function AddNewRowToEmptyDV(ByVal st As SearchType) As DataView
        Select Case st
            Case SearchType.Dealer
                Return CType(State.searchDV, AcctSetting.DealerAcctSettingsDV).AddNewRowToEmptyDV
            Case SearchType.DealerGroup
                Return CType(State.searchDV, AcctSetting.DealerGroupAcctSettingsDV).AddNewRowToEmptyDV
            Case SearchType.ServiceCenter
                Return CType(State.searchDV, AcctSetting.ServiceCenterAcctSettingsDV).AddNewRowToEmptyDV
            Case SearchType.Branch
                Return CType(State.searchDV, AcctSetting.BranchAcctSettingsDV).AddNewRowToEmptyDV
            Case SearchType.CommissionEntity
                Return CType(State.searchDV, AcctSetting.CommissionEntityAcctSettingsDV).AddNewRowToEmptyDV
        End Select
    End Function

    Private Sub PopulateGrid(Optional ByVal blnChangeCols As Boolean = False)
        Try
            If State.searchDV Is Nothing Then
                GetSearchDataView(State.TransType)
            End If
            If blnChangeCols Then AddColumnsToGrid(State.TransType)


            If State.searchDV.Count = 0 Then
                Dim dv As DataView = AddNewRowToEmptyDV(State.TransType)
                SetPageAndSelectedIndexFromGuid(dv, Me.State.AcctSettingsId, Me.Grid, Me.State.PageIndex)
                Me.Grid.DataSource = dv
            Else
                SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.AcctSettingsId, Me.Grid, Me.State.PageIndex)
                Me.Grid.DataSource = Me.State.searchDV
            End If

            Me.State.PageIndex = Me.Grid.PageIndex
            Me.Grid.DataBind()


            HighLightGridViewSortColumn(Grid, State.SortBy)
            ControlMgr.SetVisibleControl(Me, Grid, True)
            ControlMgr.SetVisibleControl(Me, trPageSize, Me.Grid.Visible)

            If Me.Grid.Visible Then
                Me.lblRecordCount.Text = Me.State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
            End If
            If Not Grid.BottomPagerRow.Visible Then Grid.BottomPagerRow.Visible = True

            If State.searchDV.Count = 0 Then
                For Each gvRow As GridViewRow In Grid.Rows
                    gvRow.Visible = False
                    gvRow.Controls.Clear()
                Next
            End If
        Catch ex As Exception
            moErrorController.AddErrorAndShow(ElitaPlus.Common.ErrorCodes.UNEXPECTED_ERROR)
        End Try
    End Sub

    Private Sub PopulateFormFromState()
        With Me.State
            Me.txtDealerGroupCode.Text = .sDealerGroupCode
            Me.txtDealerGroupName.Text = .sDealerGroupName
            Me.txtDealerCode.Text = .sDealerCode
            Me.txtDealerName.Text = .sDealerName
            Me.txtSCCode.Text = .sSCCode
            Me.txtSCName.Text = .sSCName
            Me.txtBranchCode.Text = .sBranchCode
            Me.txtBranchName.Text = .sBranchName
            If Not (.selectedPageSize = DEFAULT_PAGE_SIZE) Then
                cboPageSize.SelectedValue = CType(.selectedPageSize, String)
                Grid.PageSize = .selectedPageSize
            End If

            Select Case .TransType
                Case SearchType.DealerGroup
                    rdoDealerGroup.Checked = True
                Case SearchType.Dealer
                    rdoDealer.Checked = True
                Case SearchType.ServiceCenter
                    rdoServiceCenter.Checked = True
                Case SearchType.Branch
                    rdoBranch.Checked = True
                Case SearchType.CommissionEntity
                    rdoCommEntity.Checked = True
            End Select
        End With
        If State.HasDataChanged Then State.searchDV = Nothing
        PopulateGrid(True)
    End Sub

    Private Sub ClearSearchCriteria()
        Try
            txtDealerGroupName.Text = String.Empty
            txtDealerGroupCode.Text = String.Empty
            txtDealerName.Text = String.Empty
            txtDealerCode.Text = String.Empty
            txtSCName.Text = String.Empty
            txtSCCode.Text = String.Empty
            txtBranchName.Text = String.Empty
            txtBranchCode.Text = String.Empty
        Catch ex As Exception
            Me.HandleErrors(ex, Me.moErrorController)
        End Try
    End Sub

#End Region


End Class
