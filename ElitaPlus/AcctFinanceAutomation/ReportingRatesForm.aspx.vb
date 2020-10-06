Public Class ReportingRatesForm
    Inherits ElitaPlusSearchPage


#Region "Constants"

    Public Const URL As String = "ReportingRatesForm.aspx"
    Public Const PAGETITLE As String = "REPORTING_RATE"
    Public Const PAGETAB As String = "FINANCE_AUTOMATION"
    Public Const SUMMARYTITLE As String = "REPORTING_RATE"

    Private Const MSG_CONFIRM_PROMPT As String = "MSG_CONFIRM_PROMPT"
    Private Const MSG_RECORD_DELETED_OK As String = "MSG_RECORD_DELETED_OK"
    Private Const MSG_RECORD_SAVED_OK As String = "MSG_RECORD_SAVED_OK"
    Private Const MSG_RECORD_NOT_SAVED As String = "MSG_RECORD_NOT_SAVED"

    Private Const GRID_COL_RPT_RATE_ID_IDX As Integer = 0
    Private Const GRID_COL_RISK_FEE_IDX As Integer = 1
    Private Const GRID_COL_SPM_COE_IDX As Integer = 2
    Private Const GRID_COL_FULLFILLMENT_NOTIFICATION_IDX As Integer = 3
    Private Const GRID_COL_PREMIUM_AMT_IDX As Integer = 4
    Private Const GRID_COL_MARKETING_EXPENSES_IDX As Integer = 5
    Private Const GRID_COL_PREMIUM_TAXES_IDX As Integer = 6
    Private Const GRID_COL_LOSS_RESERVE_COST_IDX As Integer = 7
    Private Const GRID_COL_OVERHEAD_IDX As Integer = 8
    Private Const GRID_COL_GENERAL_EXPENSES_IDX As Integer = 9
    Private Const GRID_COL_ASSESSMENTS_IDX As Integer = 10
    Private Const GRID_COL_LAE_IDX As Integer = 11

    Private Const GRID_CTRL_NAME_LABEL_RPT_RATE_ID As String = "lblReportingRateID"
    Private Const GRID_CTRL_NAME_LABEL_RISK_FEE As String = "lblRiskFee"
    Private Const GRID_CTRL_NAME_LABEL_SPM_COE As String = "lblSPMCOE"
    Private Const GRID_CTRL_NAME_LABEL_FULLFILLMENT_NOTIFICATION As String = "lblFullfillmentNotification"
    Private Const GRID_CTRL_NAME_LABEL_MARKETING_EXPENSES As String = "lblMarketingExpenses"
    Private Const GRID_CTRL_NAME_LABEL_PREMIUM_TAXES As String = "lblPremiumTaxes"
    Private Const GRID_CTRL_NAME_LABEL_LOSS_RESERVE_COST As String = "lblLossReserveCost"
    Private Const GRID_CTRL_NAME_LABEL_OVERHEAD As String = "lblOverhead"
    Private Const GRID_CTRL_NAME_LABEL_GENERAL_EXPENSES As String = "lblGeneralExpenses"
    Private Const GRID_CTRL_NAME_LABEL_ASSESSMENTS As String = "lblAssessments"
    Private Const GRID_CTRL_NAME_LABEL_LAE As String = "lblLAE"

    Private Const GRID_CTRL_NAME_EDIT_RISK_FEE As String = "txtEditRiskFee"
    Private Const GRID_CTRL_NAME_EDIT_SPM_COE As String = "txtSPMCOE"
    Private Const GRID_CTRL_NAME_EDIT_FULLFILLMENT_NOTIFICATION As String = "txtFullfillmentNotification"
    Private Const GRID_CTRL_NAME_EDIT_MARKETING_EXPENSES As String = "txtMarketingExpenses"
    Private Const GRID_CTRL_NAME_EDIT_PREMIUM_TAXES As String = "txtPremiumTaxes"
    Private Const GRID_CTRL_NAME_EDIT_LOSS_RESERVE_COST As String = "txtLossReserveCost"
    Private Const GRID_CTRL_NAME_EDIT_OVERHEAD As String = "txtOverhead"
    Private Const GRID_CTRL_NAME_EDIT_GENERAL_EXPENSES As String = "txtGeneralExpenses"
    Private Const GRID_CTRL_NAME_EDIT_ASSESSMENTS As String = "txtAssessments"
    Private Const GRID_CTRL_NAME_EDIT_LAE As String = "txtLAE"

    Private Const EDIT_COMMAND As String = "SelectRecord"
    Private Const DELETE_COMMAND As String = "DeleteRecord"
    Private Const SORT_COMMAND As String = "Sort"

    Private Const NO_ROW_SELECTED_INDEX As Integer = -1

#End Region

#Region "Page State"
    Private IsReturningFromChild As Boolean = False

    Class MyState
        Public PageIndex As Integer = 0
        Public PageSize As Integer = DEFAULT_NEW_UI_PAGE_SIZE
        Public searchDV As AfaReportingRates.RptRateSearchDV = Nothing
        Public HasDataChanged As Boolean
        Public IsGridAddNew As Boolean = False
        Public IsEditMode As Boolean = False
        Public IsGridVisible As Boolean
        Public IsAfterSave As Boolean
        Public ActionInProgress As DetailPageCommand = DetailPageCommand.Nothing_

        Public selectedInvoiceRateId As Guid = Guid.Empty
        Public MyAFProductBO As AfAProduct
        Public MyInvoiceRateBO As AfaInvoiceRate
        Public ReportingRateID As Guid
        Public MyReportingRateBO As AfaReportingRates

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

    'Public ReadOnly Property IsGridInEditMode() As Boolean
    '    Get
    '        Return Me.Grid.EditIndex > Me.NO_ITEM_SELECTED_INDEX
    '    End Get
    'End Property

    Public Property SortDirection() As String
        Get
            If ViewState("SortDirection") IsNot Nothing Then
                Return ViewState("SortDirection").ToString
            Else
                Return String.Empty
            End If

        End Get
        Set(value As String)
            ViewState("SortDirection") = value
        End Set
    End Property

#End Region


#Region "Page Parameters"

    Private Sub Page_PageCall(CallFromUrl As String, CallingPar As Object) Handles MyBase.PageCall
        Try
            If CallingParameters IsNot Nothing Then
                State.selectedInvoiceRateId = CType(CallingParameters, Guid)

                If Not State.selectedInvoiceRateId.Equals(Guid.Empty) Then
                    State.MyInvoiceRateBO = New AfaInvoiceRate(State.selectedInvoiceRateId)
                    State.MyAFProductBO = New AfAProduct(State.MyInvoiceRateBO.AfaProductId)
                End If
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
#End Region

#Region "Bread Crum"
    Private Sub UpdateBreadCrum()
        MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage(PAGETAB)
        MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage(SUMMARYTITLE)
        MasterPage.UsePageTabTitleInBreadCrum = False
        MasterPage.BreadCrum = TranslationBase.TranslateLabelOrMessage(PAGETAB) + ElitaBase.Sperator + TranslationBase.TranslateLabelOrMessage(PAGETITLE)
    End Sub
#End Region

#Region "Page Events"

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        MasterPage.MessageController.Clear()
        Try
            If Not IsPostBack Then
                UpdateBreadCrum()

                ControlMgr.SetVisibleControl(Me, moSearchResults, False)

                State.PageIndex = 0
                TranslateGridHeader(Grid)
                TranslateGridControls(Grid)
                PopulateHeader()
                State.searchDV = Nothing
                If Not State.selectedInvoiceRateId.Equals(Guid.Empty) Then
                    PopulateGrid()
                End If
            Else
                CheckIfComingFromDeleteConfirm()
                BindBoPropertiesToGridHeaders()
            End If

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
        'Me.ShowMissingTranslations(Me.MasterPage.MessageController)
    End Sub

#End Region


#Region "Helper functions"

    Protected Sub CheckIfComingFromDeleteConfirm()
        Dim confResponse As String = HiddenDeletePromptResponse.Value
        If confResponse IsNot Nothing AndAlso confResponse = MSG_VALUE_YES Then
            If Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Delete Then
                DoDelete()
            End If
            Select Case State.ActionInProgress
                Case ElitaPlusPage.DetailPageCommand.Delete
            End Select
        ElseIf confResponse IsNot Nothing AndAlso confResponse = MSG_VALUE_NO Then
            Select Case State.ActionInProgress
                Case ElitaPlusPage.DetailPageCommand.Delete
            End Select
        End If
        'Clean after consuming the action
        State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
        HiddenDeletePromptResponse.Value = ""
    End Sub

    Private Sub DoDelete()
        'Do the delete here
        State.ActionInProgress = DetailPageCommand.Nothing_

        Dim obj As AfaReportingRates = New AfaReportingRates(State.ReportingRateID)

        obj.Delete()

        'Call the Save() method in the ReportingRates Business Object here
        obj.Save()

        MasterPage.MessageController.AddSuccess(MSG_RECORD_DELETED_OK, True)

        State.PageIndex = Grid.PageIndex

        'Set the IsAfterSave flag to TRUE so that the Paging logic gets invoked
        State.IsAfterSave = True
        State.searchDV = Nothing
        PopulateGrid()
        State.PageIndex = Grid.PageIndex
        State.IsEditMode = False
        SetControlState()
    End Sub

    Private Sub SetControlState()
        If (State.IsEditMode) Then
            ControlMgr.SetVisibleControl(Me, btnNew, False)
            MenuEnabled = False
            If (cboPageSize.Enabled) Then
                ControlMgr.SetEnableControl(Me, cboPageSize, False)
            End If
        Else
            ControlMgr.SetVisibleControl(Me, btnNew, True)
            MenuEnabled = True
            If Not (cboPageSize.Enabled) Then
                ControlMgr.SetEnableControl(Me, cboPageSize, True)
            End If
        End If
    End Sub

    Protected Sub BindBoPropertiesToGridHeaders()
        BindBOPropertyToGridHeader(State.MyReportingRateBO, "RiskFee", Grid.Columns(GRID_COL_RISK_FEE_IDX))
        BindBOPropertyToGridHeader(State.MyReportingRateBO, "SpmCoe", Grid.Columns(GRID_COL_SPM_COE_IDX))
        BindBOPropertyToGridHeader(State.MyReportingRateBO, "FullfillmentNotification", Grid.Columns(GRID_COL_FULLFILLMENT_NOTIFICATION_IDX))
        BindBOPropertyToGridHeader(State.MyReportingRateBO, "MarketingExpenses", Grid.Columns(GRID_COL_MARKETING_EXPENSES_IDX))
        BindBOPropertyToGridHeader(State.MyReportingRateBO, "PremiumTaxes", Grid.Columns(GRID_COL_PREMIUM_TAXES_IDX))
        BindBOPropertyToGridHeader(State.MyReportingRateBO, "LossReserveCost", Grid.Columns(GRID_COL_LOSS_RESERVE_COST_IDX))
        BindBOPropertyToGridHeader(State.MyReportingRateBO, "Overhead", Grid.Columns(GRID_COL_OVERHEAD_IDX))
        BindBOPropertyToGridHeader(State.MyReportingRateBO, "GeneralExpenses", Grid.Columns(GRID_COL_GENERAL_EXPENSES_IDX))
        BindBOPropertyToGridHeader(State.MyReportingRateBO, "Assessments", Grid.Columns(GRID_COL_ASSESSMENTS_IDX))
        BindBOPropertyToGridHeader(State.MyReportingRateBO, "Lae", Grid.Columns(GRID_COL_LAE_IDX))
        ClearGridViewHeadersAndLabelsErrSign()
    End Sub

    Private Sub ReturnFromEditing()

        Grid.EditIndex = NO_ROW_SELECTED_INDEX

        If (Grid.PageCount = 0) Then
            'if returning to the "1st time in" screen
            ControlMgr.SetVisibleControl(Me, Grid, False)
        Else
            ControlMgr.SetVisibleControl(Me, Grid, True)
        End If

        State.IsEditMode = False
        PopulateGrid()
        State.PageIndex = Grid.PageIndex
        SetControlState()
    End Sub

    Private Sub RemoveNewRowFromSearchDV()
        Dim rowind As Integer = NO_ITEM_SELECTED_INDEX
        With State
            If .searchDV IsNot Nothing Then
                rowind = FindSelectedRowIndexFromGuid(.searchDV, .ReportingRateID)
            End If
        End With
        If rowind <> NO_ITEM_SELECTED_INDEX Then State.searchDV.Delete(rowind)
    End Sub

    Private Function PopulateBOFromForm() As Boolean
        Dim objtxt As TextBox

        With State.MyReportingRateBO
            objtxt = CType(Grid.Rows((Grid.EditIndex)).Cells(GRID_COL_RISK_FEE_IDX).FindControl(GRID_CTRL_NAME_EDIT_RISK_FEE), TextBox)
            PopulateBOProperty(State.MyReportingRateBO, "RiskFee", objtxt)
            objtxt = CType(Grid.Rows((Grid.EditIndex)).Cells(GRID_COL_SPM_COE_IDX).FindControl(GRID_CTRL_NAME_EDIT_SPM_COE), TextBox)
            PopulateBOProperty(State.MyReportingRateBO, "SpmCoe", objtxt)
            objtxt = CType(Grid.Rows((Grid.EditIndex)).Cells(GRID_COL_FULLFILLMENT_NOTIFICATION_IDX).FindControl(GRID_CTRL_NAME_EDIT_FULLFILLMENT_NOTIFICATION), TextBox)
            PopulateBOProperty(State.MyReportingRateBO, "FullfillmentNotification", objtxt)
            objtxt = CType(Grid.Rows((Grid.EditIndex)).Cells(GRID_COL_MARKETING_EXPENSES_IDX).FindControl(GRID_CTRL_NAME_EDIT_MARKETING_EXPENSES), TextBox)
            PopulateBOProperty(State.MyReportingRateBO, "MarketingExpenses", objtxt)
            objtxt = CType(Grid.Rows((Grid.EditIndex)).Cells(GRID_COL_PREMIUM_TAXES_IDX).FindControl(GRID_CTRL_NAME_EDIT_PREMIUM_TAXES), TextBox)
            PopulateBOProperty(State.MyReportingRateBO, "PremiumTaxes", objtxt)
            objtxt = CType(Grid.Rows((Grid.EditIndex)).Cells(GRID_COL_LOSS_RESERVE_COST_IDX).FindControl(GRID_CTRL_NAME_EDIT_LOSS_RESERVE_COST), TextBox)
            PopulateBOProperty(State.MyReportingRateBO, "LossReserveCost", objtxt)
            objtxt = CType(Grid.Rows((Grid.EditIndex)).Cells(GRID_COL_OVERHEAD_IDX).FindControl(GRID_CTRL_NAME_EDIT_OVERHEAD), TextBox)
            PopulateBOProperty(State.MyReportingRateBO, "Overhead", objtxt)
            objtxt = CType(Grid.Rows((Grid.EditIndex)).Cells(GRID_COL_GENERAL_EXPENSES_IDX).FindControl(GRID_CTRL_NAME_EDIT_GENERAL_EXPENSES), TextBox)
            PopulateBOProperty(State.MyReportingRateBO, "GeneralExpenses", objtxt)
            objtxt = CType(Grid.Rows((Grid.EditIndex)).Cells(GRID_COL_ASSESSMENTS_IDX).FindControl(GRID_CTRL_NAME_EDIT_ASSESSMENTS), TextBox)
            PopulateBOProperty(State.MyReportingRateBO, "Assessments", objtxt)
            objtxt = CType(Grid.Rows((Grid.EditIndex)).Cells(GRID_COL_LAE_IDX).FindControl(GRID_CTRL_NAME_EDIT_LAE), TextBox)
            PopulateBOProperty(State.MyReportingRateBO, "Lae", objtxt)

        End With

        If ErrCollection.Count > 0 Then
            Throw New PopulateBOErrorException
        End If
    End Function

#End Region

#Region "Grid related"

    Public Sub PopulateGrid()
        Dim blnNewSearch As Boolean = False
        Dim dv As DataView
        Try
            'Refresh the DataView and Call SetPageAndSelectedIndexFromGuid() to go to the Page 
            'where the most recently saved Record exists in the DataView
            With State
                If (.searchDV Is Nothing) Then
                    .searchDV = AfaReportingRates.getList(.selectedInvoiceRateId)
                    blnNewSearch = True
                End If
            End With

            State.searchDV.Sort = SortDirection
            If (State.IsAfterSave) Then
                State.IsAfterSave = False
                SetPageAndSelectedIndexFromGuid(State.searchDV, State.ReportingRateID, Grid, State.PageIndex)
            ElseIf (State.IsEditMode) Then
                SetPageAndSelectedIndexFromGuid(State.searchDV, State.ReportingRateID, Grid, State.PageIndex, State.IsEditMode)
            Else
                SetPageAndSelectedIndexFromGuid(State.searchDV, Guid.Empty, Grid, State.PageIndex)
            End If

            Grid.AutoGenerateColumns = False
            SortAndBindGrid(blnNewSearch)

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub SortAndBindGrid(Optional ByVal blnShowErr As Boolean = True)

        TranslateGridControls(Grid)

        If (State.searchDV.Count = 0) Then
            State.searchDV = Nothing
            State.MyReportingRateBO = New AfaReportingRates
            State.MyReportingRateBO.AddNewRowToSearchDV(State.searchDV, State.MyReportingRateBO)
            Grid.DataSource = State.searchDV
            Grid.DataBind()
            Grid.Rows(0).Visible = False
            State.IsGridAddNew = True
            State.IsGridVisible = False
            If blnShowErr Then
                MasterPage.MessageController.AddInformation(ElitaPlus.ElitaPlusWebApp.Message.MSG_NO_RECORDS_FOUND, True)
            End If
        Else
            Grid.Enabled = True
            Grid.PageSize = State.PageSize
            Grid.DataSource = State.searchDV
            State.IsGridVisible = True
            HighLightSortColumn(Grid, SortDirection)
            Grid.DataBind()
        End If

        ControlMgr.SetVisibleControl(Me, Grid, State.IsGridVisible)
        ControlMgr.SetVisibleControl(Me, moSearchResults, State.IsGridVisible)

        Session("recCount") = State.searchDV.Count

        If Grid.Visible Then
            If (State.IsGridAddNew) Then
                lblRecordCount.Text = (State.searchDV.Count - 1) & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
            Else
                lblRecordCount.Text = State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
            End If
        End If
        ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me, Grid)

    End Sub

    Private Sub Grid_PageIndexChanged(sender As Object, e As System.EventArgs) Handles Grid.PageIndexChanged
        Try
            If (Not (State.IsEditMode)) Then
                State.PageIndex = Grid.PageIndex
                State.ReportingRateID = Guid.Empty
                PopulateGrid()
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Grid_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Grid.PageIndexChanging
        Try
            Grid.PageIndex = e.NewPageIndex
            State.PageIndex = Grid.PageIndex
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Grid_Sorting(sender As Object, e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles Grid.Sorting
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

            State.PageIndex = 0
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub cboPageSize_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
        Try
            State.PageSize = CType(cboPageSize.SelectedValue, Integer)
            State.PageIndex = NewCurrentPageIndex(Grid, State.searchDV.Count, State.PageSize)
            Grid.PageIndex = State.PageIndex
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Grid_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowDataBound
        Try
            Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
            Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
            Dim strID As String
            Dim moLabel As Label
            Dim moTextBox As TextBox
            Dim moImageButton As ImageButton
            Dim btnEditItem As LinkButton

            If dvRow IsNot Nothing Then
                strID = GetGuidStringFromByteArray(CType(dvRow(AfaReportingRates.RptRateSearchDV.COL_AFA_REPORTING_RATE_ID), Byte()))
                If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then
                    CType(e.Row.Cells(GRID_COL_RPT_RATE_ID_IDX).FindControl(GRID_CTRL_NAME_LABEL_RPT_RATE_ID), Label).Text = strID

                    If (State.IsEditMode = True AndAlso State.ReportingRateID.ToString.Equals(strID)) Then

                        CType(e.Row.Cells(GRID_COL_RISK_FEE_IDX).FindControl(GRID_CTRL_NAME_EDIT_RISK_FEE), TextBox).Text = dvRow(AfaReportingRates.RptRateSearchDV.COL_RISK_FEE).ToString
                        CType(e.Row.Cells(GRID_COL_SPM_COE_IDX).FindControl(GRID_CTRL_NAME_EDIT_SPM_COE), TextBox).Text = dvRow(AfaReportingRates.RptRateSearchDV.COL_SPM_COE).ToString
                        CType(e.Row.Cells(GRID_COL_FULLFILLMENT_NOTIFICATION_IDX).FindControl(GRID_CTRL_NAME_EDIT_FULLFILLMENT_NOTIFICATION), TextBox).Text = dvRow(AfaReportingRates.RptRateSearchDV.COL_FULLFILLMENT_NOTIFICATION).ToString
                        CType(e.Row.Cells(GRID_COL_MARKETING_EXPENSES_IDX).FindControl(GRID_CTRL_NAME_EDIT_MARKETING_EXPENSES), TextBox).Text = dvRow(AfaReportingRates.RptRateSearchDV.COL_MARKETING_EXPENSES).ToString
                        CType(e.Row.Cells(GRID_COL_PREMIUM_TAXES_IDX).FindControl(GRID_CTRL_NAME_EDIT_PREMIUM_TAXES), TextBox).Text = dvRow(AfaReportingRates.RptRateSearchDV.COL_PREMIUM_TAXES).ToString
                        CType(e.Row.Cells(GRID_COL_LOSS_RESERVE_COST_IDX).FindControl(GRID_CTRL_NAME_EDIT_LOSS_RESERVE_COST), TextBox).Text = dvRow(AfaReportingRates.RptRateSearchDV.COL_LOSS_RESERVE_COST).ToString
                        CType(e.Row.Cells(GRID_COL_OVERHEAD_IDX).FindControl(GRID_CTRL_NAME_EDIT_OVERHEAD), TextBox).Text = dvRow(AfaReportingRates.RptRateSearchDV.COL_OVERHEAD).ToString
                        CType(e.Row.Cells(GRID_COL_GENERAL_EXPENSES_IDX).FindControl(GRID_CTRL_NAME_EDIT_GENERAL_EXPENSES), TextBox).Text = dvRow(AfaReportingRates.RptRateSearchDV.COL_GENERAL_EXPENSES).ToString
                        CType(e.Row.Cells(GRID_COL_ASSESSMENTS_IDX).FindControl(GRID_CTRL_NAME_EDIT_ASSESSMENTS), TextBox).Text = dvRow(AfaReportingRates.RptRateSearchDV.COL_ASSESSMENTS).ToString
                        CType(e.Row.Cells(GRID_COL_LAE_IDX).FindControl(GRID_CTRL_NAME_EDIT_LAE), TextBox).Text = dvRow(AfaReportingRates.RptRateSearchDV.COL_LAE).ToString
                    Else

                        CType(e.Row.Cells(GRID_COL_RISK_FEE_IDX).FindControl(GRID_CTRL_NAME_LABEL_RISK_FEE), Label).Text = dvRow(AfaReportingRates.RptRateSearchDV.COL_RISK_FEE).ToString

                        CType(e.Row.Cells(GRID_COL_SPM_COE_IDX).FindControl(GRID_CTRL_NAME_LABEL_SPM_COE), Label).Text = dvRow(AfaReportingRates.RptRateSearchDV.COL_SPM_COE).ToString

                        CType(e.Row.Cells(GRID_COL_FULLFILLMENT_NOTIFICATION_IDX).FindControl(GRID_CTRL_NAME_LABEL_FULLFILLMENT_NOTIFICATION), Label).Text = dvRow(AfaReportingRates.RptRateSearchDV.COL_FULLFILLMENT_NOTIFICATION).ToString
                        CType(e.Row.Cells(GRID_COL_MARKETING_EXPENSES_IDX).FindControl(GRID_CTRL_NAME_LABEL_MARKETING_EXPENSES), Label).Text = dvRow(AfaReportingRates.RptRateSearchDV.COL_MARKETING_EXPENSES).ToString
                        CType(e.Row.Cells(GRID_COL_PREMIUM_TAXES_IDX).FindControl(GRID_CTRL_NAME_LABEL_PREMIUM_TAXES), Label).Text = dvRow(AfaReportingRates.RptRateSearchDV.COL_PREMIUM_TAXES).ToString
                        CType(e.Row.Cells(GRID_COL_LOSS_RESERVE_COST_IDX).FindControl(GRID_CTRL_NAME_LABEL_LOSS_RESERVE_COST), Label).Text = dvRow(AfaReportingRates.RptRateSearchDV.COL_LOSS_RESERVE_COST).ToString
                        CType(e.Row.Cells(GRID_COL_OVERHEAD_IDX).FindControl(GRID_CTRL_NAME_LABEL_OVERHEAD), Label).Text = dvRow(AfaReportingRates.RptRateSearchDV.COL_OVERHEAD).ToString
                        CType(e.Row.Cells(GRID_COL_GENERAL_EXPENSES_IDX).FindControl(GRID_CTRL_NAME_LABEL_GENERAL_EXPENSES), Label).Text = dvRow(AfaReportingRates.RptRateSearchDV.COL_GENERAL_EXPENSES).ToString
                        CType(e.Row.Cells(GRID_COL_ASSESSMENTS_IDX).FindControl(GRID_CTRL_NAME_LABEL_ASSESSMENTS), Label).Text = dvRow(AfaReportingRates.RptRateSearchDV.COL_ASSESSMENTS).ToString
                        CType(e.Row.Cells(GRID_COL_LAE_IDX).FindControl(GRID_CTRL_NAME_LABEL_LAE), Label).Text = dvRow(AfaReportingRates.RptRateSearchDV.COL_LAE).ToString

                    End If
                End If
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try

    End Sub


    Public Sub RowCommand(source As System.Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs)

        Try
            Dim index As Integer
            If (e.CommandName = EDIT_COMMAND) Then
                index = CInt(e.CommandArgument)
                'Do the Edit here

                'Set the IsEditMode flag to TRUE
                State.IsEditMode = True

                State.ReportingRateID = New Guid(CType(Grid.Rows(index).Cells(GRID_COL_RPT_RATE_ID_IDX).FindControl(GRID_CTRL_NAME_LABEL_RPT_RATE_ID), Label).Text)
                State.MyReportingRateBO = New AfaReportingRates(State.ReportingRateID)

                PopulateGrid()

                State.PageIndex = Grid.PageIndex

                SetControlState()

            ElseIf (e.CommandName = DELETE_COMMAND) Then
                index = CInt(e.CommandArgument)
                State.ReportingRateID = New Guid(CType(Grid.Rows(index).Cells(GRID_COL_RPT_RATE_ID_IDX).FindControl(GRID_CTRL_NAME_LABEL_RPT_RATE_ID), Label).Text)
                DisplayMessage(Message.DELETE_RECORD_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenDeletePromptResponse)
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Delete

            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try

    End Sub

    Public Sub RowCreated(sender As System.Object, e As System.Web.UI.WebControls.GridViewRowEventArgs)
        Try
            BaseItemCreated(sender, e)
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

#End Region


#Region "Control Handler"

    Private Sub PopulateHeader()
        Try
            If Not State.selectedInvoiceRateId.Equals(Guid.Empty) Then
                txtProductCode.Text = State.MyAFProductBO.Code
                txtInsCode.Text = State.MyInvoiceRateBO.InsuranceCode
                txtHandsetTier.Text = State.MyInvoiceRateBO.Tier
                txtLossType.Text = LookupListNew.GetDescrionFromListCode("CTYP", State.MyInvoiceRateBO.LossType)
            End If

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click

        Try
            State.IsEditMode = True
            State.IsGridVisible = True
            State.IsGridAddNew = True
            AddNew()
            SetControlState()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try

    End Sub

    Private Sub AddNew()

        State.MyReportingRateBO = New AfaReportingRates
        State.MyReportingRateBO.AfaInvoiceRateId = State.MyInvoiceRateBO.Id
        State.MyReportingRateBO.AddNewRowToSearchDV(State.searchDV, State.MyReportingRateBO)

        State.ReportingRateID = State.MyReportingRateBO.Id
        State.IsGridAddNew = True
        PopulateGrid()
        'Set focus on the Code TextBox for the EditItemIndex row
        SetPageAndSelectedIndexFromGuid(State.searchDV, State.ReportingRateID, Grid, _
                                           State.PageIndex, State.IsEditMode)
        ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me, Grid)
        SetGridControls(Grid, False)
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs)

        Try
            PopulateBOFromForm()
            If (State.MyReportingRateBO.IsDirty) Then
                State.MyReportingRateBO.Save()
                State.IsAfterSave = True
                State.IsGridAddNew = False
                MasterPage.MessageController.AddSuccess(MSG_RECORD_SAVED_OK, True)
                State.searchDV = Nothing
                State.MyReportingRateBO = Nothing
                ReturnFromEditing()
            Else
                MasterPage.MessageController.AddWarning(MSG_RECORD_NOT_SAVED, True)
                ReturnFromEditing()
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try

    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs)
        Try
            With State
                If .IsGridAddNew Then
                    RemoveNewRowFromSearchDV()
                    .IsGridAddNew = False
                    Grid.PageIndex = .PageIndex
                End If
                .ReportingRateID = Guid.Empty
                State.MyReportingRateBO = Nothing
                .IsEditMode = False
            End With
            Grid.EditIndex = NO_ITEM_SELECTED_INDEX
            'Me.State.searchDV = Nothing
            PopulateGrid()
            SetControlState()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
#End Region


#Region "Button Click Handlers"

    Private Sub btnBack_Click(sender As Object, e As System.EventArgs) Handles btnBack.Click
        Try
            ReturnToCallingPage()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
#End Region
End Class