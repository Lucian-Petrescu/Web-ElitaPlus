Partial Class RedoListForm
    Inherits ElitaPlusSearchPage

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents ErrorCtrl As ErrorController

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
    Public Const GRID_COL_EDIT_IDX As Integer = 0
    Public Const GRID_COL_SELECT_IDX As Integer = 1
    Public Const GRID_COL_CLAIM_IDX As Integer = 2
    Public Const GRID_COL_CLAIM_NUMBERX As Integer = 3
    Public Const GRID_COL_SERVICE_CENTER_CODEX As Integer = 4
    Public Const GRID_COL_PICK_UP_DATEX As Integer = 5
    Public Const GRID_COL_MASTER_CALIM_NUMBERX As Integer = 6


    Public Const GRID_TOTAL_COLUMNS As Integer = 7
    Private Const NOTHING_SELECTED As String = "00000000-0000-0000-0000-000000000000"
    'Private Const BRANCH_LIST_FORM001 As String = "BRANCH_LIST_FORM001" ' Maintain Branch List Exception
    'Private Const BRANCHLISTFORM As String = "BranchListForm.aspx"
    'Private Const LABEL_SELECT_DEALERCODE As String = "DEALER"
#End Region

#Region "Properties"


#End Region

#Region "Page State"
    Private IsReturningFromChild As Boolean = False

    ' This class keeps the current state for the search page.
    Class MyState
        Public MyBO As Claim
        Public PageIndex As Integer = 0
        Public DescriptionMask As String
        Public inputParameters As Parameters
        Public CodeMask As String
        Public RedoClaimID As Guid
        Public CertCovItemId As Guid = Guid.Empty
        Public ClaimId As Guid = Guid.Empty
        Public IsGridVisible As Boolean = True
        Private mnPageIndex As Integer = 0
        Private msPageSort As String
        Private mnPageSize As Integer = DEFAULT_PAGE_SIZE
        Public searchDV As Claim.ClaimRedoDV = Nothing
        Public selectedPageSize As Integer = DEFAULT_PAGE_SIZE
        Public SortExpression As String = Claim.ClaimRedoDV.COL_CLAIM_NUMBER
        Public HasDataChanged As Boolean
        Public bnoRow As Boolean = False
        Public selectedClaimNumber As String
        Public claimStat As ClaimStatus
        Public selectedMasterClaim As String



        Public Property PageSize() As Integer
            Get
                Return mnPageSize
            End Get
            Set(ByVal Value As Integer)
                mnPageSize = Value
            End Set
        End Property

        Public Property PageSort() As String
            Get
                Return msPageSort
            End Get
            Set(ByVal Value As String)
                msPageSort = Value
            End Set
        End Property

        Public Property SearchDataView() As Claim.ClaimRedoDV
            Get
                Return searchDV
            End Get
            Set(ByVal Value As Claim.ClaimRedoDV)
                searchDV = Value
            End Set
        End Property

    End Class

    Public Sub New()
        MyBase.New(New MyState)
    End Sub

    Protected Shadows ReadOnly Property State() As MyState
        Get
            'Return CType(MyBase.State, MyState)
            If Me.NavController.State Is Nothing Then
                Me.NavController.State = New MyState
                InitializeFromFlowSession()
            End If
            Return CType(Me.NavController.State, MyState)
        End Get
    End Property
    Protected Sub InitializeFromFlowSession()

        Me.State.MyBO = CType(Me.NavController.ParametersPassed, Claim)
    End Sub
    Public Class Parameters
        Public ClaimBO As Claim

        Public Sub New(ByVal claimBO As Claim)
            Me.ClaimBO = claimBO

        End Sub
    End Class
#Region "Page Return"


    Private Sub Page_PageReturn(ByVal ReturnFromUrl As String, ByVal ReturnPar As Object) Handles MyBase.PageReturn
        Try
            Me.MenuEnabled = True
            Me.IsReturningFromChild = True
            'Dim retObj As BranchForm.ReturnType = CType(ReturnPar, BranchForm.ReturnType)

            'Me.State.HasDataChanged = retObj.HasDataChanged
            'Select Case retObj.LastOperation
            '    Case ElitaPlusPage.DetailPageCommand.Back
            '        If Not retObj Is Nothing Then
            '            Me.State.BranchId = retObj.EditingBo.Id
            '            Me.State.IsGridVisible = True
            '        End If
            '    Case ElitaPlusPage.DetailPageCommand.Delete
            '        Me.AddInfoMsg(Message.DELETE_RECORD_CONFIRMATION)
            'End Select
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

#End Region
#End Region

#Region "Page_Events"

    'Private Sub Page_PageCall(ByVal CallFromUrl As String, ByVal CallingPar As Object) Handles MyBase.PageCall
    '    Try
    '        Me.State.inputParameters = CType(CallingPar, Parameters)
    '        If Not Me.State.inputParameters.ClaimBO Is Nothing Then
    '            'Get the id from the parent
    '            Me.State.MyBO = Me.State.inputParameters.ClaimBO
    '        End If
    '    Catch ex As Exception
    '        Me.HandleErrors(ex, Me.ErrorCtrl)
    '    End Try

    'End Sub

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Me.ErrorCtrl.Clear_Hide()

        Try
            If Not Me.IsPostBack Then
                Me.AddControlMsg(Me.btnSave_WRITE, Message.MSG_PROMPT_ARE_YOU_SURE, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, True)
                Me.State.claimStat = New ClaimStatus()
                Me.SortDirection = Claim.ClaimRedoDV.COL_CLAIM_NUMBER
                Me.PopulateFormFromBOs()
                ControlMgr.SetVisibleControl(Me, trPageSize, False)
                If Me.State.IsGridVisible Then
                    If Not (Me.State.selectedPageSize = DEFAULT_PAGE_SIZE) Then
                        cboPageSize.SelectedValue = CType(Me.State.selectedPageSize, String)
                        Grid.PageSize = Me.State.selectedPageSize
                    End If
                    Me.PopulateGrid()
                End If
                Me.SetGridItemStyleColor(Me.Grid)
            Else
                ClearErrLabels()
            End If
            'Me.TranslateGridHeader(Me.Grid)
            'Me.TranslateGridControls(Me.Grid)
            'If Me.IsReturningFromChild = True Then
            '    Me.IsReturningFromChild = False
            'End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
        Me.ShowMissingTranslations(Me.ErrorCtrl)

    End Sub

    Protected Sub PopulateFormFromBOs()
        PopulateRedoClaimAttributes()
    End Sub

    Private Sub PopulateRedoClaimAttributes()
        Try

            With Me.State.MyBO
                Me.PopulateControlFromBOProperty(Me.txtCertificateNumber, .CertificateNumber)
                Me.PopulateControlFromBOProperty(Me.txtCustomerName, .CustomerName)
                Me.PopulateControlFromBOProperty(Me.txtServiceCenter, .ServiceCenter)
                Me.PopulateControlFromBOProperty(Me.txtRedoClaimNumber, .ClaimNumber)
            End With

            Dim ar As New ArrayList
            ar.Add(Me.State.MyBO.CompanyId)
            Me.State.ClaimId = Claim.GetClaimID(ar, Me.State.MyBO.ClaimNumber)
            Me.State.claimStat = ClaimStatus.GetLatestClaimStatus(Me.State.ClaimId)

            If Not Me.State.claimStat Is Nothing Then
                Me.PopulateControlFromBOProperty(Me.txtExtendedStatus, Me.State.claimStat.StatusDescription)
            End If

            Me.ChangeEnabledProperty(txtCertificateNumber, False)
            Me.ChangeEnabledProperty(txtCustomerName, False)
            Me.ChangeEnabledProperty(txtServiceCenter, False)
            Me.ChangeEnabledProperty(txtRedoClaimNumber, False)
            Me.ChangeEnabledProperty(txtExtendedStatus, False)
        Catch ex As Exception

        End Try
    End Sub
#End Region

#Region "Controlling Logic"

    Public Sub PopulateGrid()

        'Dim oDealerview As DataView = LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies)
        'Me.State.DealerId = TheDealerControl.SelectedGuid
        If ((Me.State.searchDV Is Nothing) OrElse (Me.State.HasDataChanged)) Then
            Me.State.searchDV = Claim.getRedoList(Me.State.MyBO.CertItemCoverageId, Me.State.MyBO.CreatedDate, Me.State.ClaimId)

        End If
        Me.State.searchDV.Sort = Me.SortDirection
        Me.Grid.AutoGenerateColumns = False

        SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.CertCovItemId, Me.Grid, Me.State.PageIndex)
        Me.SortAndBindGrid()

    End Sub
    Private Sub SortAndBindGrid()
        Me.State.PageIndex = Me.Grid.PageIndex
        If (Me.State.searchDV.Count = 0) Then

            Me.State.bnoRow = True
            CreateHeaderForEmptyGrid(Grid, Me.SortDirection)
        Else
            Me.State.bnoRow = False
            Me.Grid.Enabled = True
            Me.Grid.DataSource = Me.State.searchDV
            HighLightSortColumn(Grid, Me.SortDirection)
            Me.Grid.DataBind()
        End If
        If Not Grid.BottomPagerRow.Visible Then Grid.BottomPagerRow.Visible = True
        'Me.Grid.DataSource = Me.State.searchDV
        'HighLightSortColumn(Grid, Me.SortDirection)
        'Me.Grid.DataBind()

        ControlMgr.SetVisibleControl(Me, Grid, Me.State.IsGridVisible)

        ControlMgr.SetVisibleControl(Me, trPageSize, Me.Grid.Visible)

        Session("recCount") = Me.State.searchDV.Count

        If Me.State.searchDV.Count > 0 Then

            If Me.Grid.Visible Then
                Me.lblRecordCount.Text = Me.State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
            End If
        Else
            If Me.Grid.Visible Then
                Me.lblRecordCount.Text = Me.State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
            End If
        End If
    End Sub

#End Region

#Region "Datagrid Related "

    Public Property SortDirection() As String
        Get
            Return ViewState("SortDirection").ToString
        End Get
        Set(ByVal value As String)
            ViewState("SortDirection") = value
        End Set
    End Property

    'The Binding Logic is here
    Private Sub Grid_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowDataBound
        Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
        Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
        If Not dvRow Is Nothing And Not Me.State.bnoRow Then
            If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then
                e.Row.Cells(Me.GRID_COL_CLAIM_IDX).Text = GetGuidStringFromByteArray(CType(dvRow(Claim.ClaimRedoDV.COL_CLAIM_ID), Byte()))
                e.Row.Cells(Me.GRID_COL_CLAIM_NUMBERX).Text = dvRow(Claim.ClaimRedoDV.COL_CLAIM_NUMBER).ToString
                e.Row.Cells(Me.GRID_COL_SERVICE_CENTER_CODEX).Text = dvRow(Claim.ClaimRedoDV.COL_SERVICE_CENTER_CODE).ToString
                e.Row.Cells(Me.GRID_COL_PICK_UP_DATEX).Text = dvRow(Claim.ClaimRedoDV.COL_PICK_UP_DATE).ToString
                e.Row.Cells(Me.GRID_COL_MASTER_CALIM_NUMBERX).Text = dvRow(Claim.ClaimRedoDV.COL_MASTER_CLAIM_NUMBER).ToString
            End If
        End If
    End Sub

    Public Sub ItemCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)
        Try
            If e.CommandName = "SelectRecord" Then
                Dim index As Integer = CInt(e.CommandArgument)
                Grid.SelectedIndex = index
                Me.State.selectedClaimNumber = Me.Grid.Rows(index).Cells(Me.GRID_COL_CLAIM_NUMBERX).Text
                Me.State.selectedMasterClaim = Me.Grid.Rows(index).Cells(Me.GRID_COL_MASTER_CALIM_NUMBERX).Text
            End If

        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try

    End Sub

    Public Sub ItemCreated(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
        BaseItemCreated(sender, e)
    End Sub

    Private Sub Grid_PageSizeChanged(ByVal source As Object, ByVal e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
        Try
            Grid.PageIndex = NewCurrentPageIndex(Grid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

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

            Me.State.PageIndex = 0
            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

    Private Sub Grid_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Grid.PageIndexChanging
        Try
            Me.State.PageIndex = e.NewPageIndex
            Me.State.ClaimId = Guid.Empty
            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub
#End Region

#Region "Button Clicks "
    Private Sub btnBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Try
            Me.NavController.Navigate(Me, "back")
            'Me.ReturnToCallingPage()
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub
    Private Sub SetStateProperties()
        'Me.State.DescriptionMask = SearchDescriptionTextBox.Text
        'Me.State.CodeMask = SearchCodeTextBox.Text
    End Sub




#End Region

#Region "Clear"

    Private Sub ClearErrLabels()
        'Me.ClearLabelErrSign(TheDealerControl.CaptionLabel)
    End Sub

#End Region

#Region "State-Management"

    Private Sub SetSession()
        With Me.State
            '.CodeMask = Me.SearchCodeTextBox.Text
            '.DescriptionMask = Me.SearchDescriptionTextBox.Text
            '.DealerId = Me.State.DealerId 'Me.GetSelectedItem(moDealerDrop)
            .PageIndex = Grid.PageIndex
            .PageSize = Grid.PageSize
            .PageSort = Me.SortDirection
            '.SearchDataView = Me.State.searchDV
        End With
    End Sub

    Private Sub GetSession()
        'Dim oDataView As DataView
        With Me.State
            'Me.SearchCodeTextBox.Text = .CodeMask
            'Me.SearchDescriptionTextBox.Text = .DescriptionMask
            Me.Grid.PageSize = .PageSize
            cboPageSize.SelectedValue = CType(.PageSize, String)

        End With
    End Sub
#End Region


    Private Sub btnSave_WRITE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave_WRITE.Click
        If (Grid.SelectedIndex > -1) Then
            'Me.AddConfirmMsg(Message.SAVE_CHANGES_PROMPT, Me.HiddenSaveChangesPromptResponse)

            ' new claim is created with the details from the redo-claim
            Dim newClaim As Claim = Me.State.MyBO.CreateNewClaim()
            newClaim.CopyFrom(Me.State.MyBO)

            newClaim.AuthorizedAmount = 0
            newClaim.ClaimNumber = Me.State.selectedClaimNumber & "S"
            newClaim.ClaimActivityId = LookupListNew.GetIdFromCode(LookupListNew.LK_CLAIM_ACTIVITIES, Codes.CLAIM_ACTIVITY__REWORK)
            newClaim.Deductible = 0
            newClaim.ProblemDescription = "Redo claim changed to Service Warranty on " & Date.Now.Date()
            newClaim.MasterClaimNumber = Me.State.selectedMasterClaim
            newClaim.Save()

            ' correct the existing latest claim status to contain the info.

            If Not Me.State.claimStat Is Nothing Then
                Me.State.claimStat.Comments = Me.State.MyBO.ClaimNumber & " Redo claim changed to Service Warranty"
                Me.State.claimStat.Save()
            End If

            ' chnage each of the extended claim status to point to the new claim id.
            Dim cstatView As ClaimStatus.ClaimStatusSearchDV

            For Each row As DataRowView In Me.State.claimStat.GetClaimStatusList(Me.State.ClaimId)
                Dim cs As New ClaimStatus(GuidControl.ByteArrayToGuid(row(cstatView.COL_CLAIM_STATUS_ID)))
                cs.ClaimId = newClaim.Id
                cs.Save()
            Next

            'Update the pick list detail to have the new claim id.

            Dim plView As PickupListDetail.PickListDetailDV

            For Each row As DataRowView In PickupListDetail.getPickListByClaimId(Me.State.ClaimId)
                Dim pick As New PickupListDetail(GuidControl.ByteArrayToGuid(row(plView.COL_DETAIL_ID)))
                pick.ClaimId = newClaim.Id
                pick.Save()
            Next

            ' Close the actual re-do claim.

            Me.State.MyBO.StatusCode = Codes.CLAIM_STATUS__CLOSED
            Me.State.MyBO.ReasonClosedId = LookupListNew.GetIdFromCode(LookupListNew.LK_REASONS_CLOSED, Codes.REASON_CLOSED__TO_BE_REPLACED)
            Me.State.MyBO.Save()

            Me.PopulateFormFromBOs()
        End If


    End Sub
End Class
