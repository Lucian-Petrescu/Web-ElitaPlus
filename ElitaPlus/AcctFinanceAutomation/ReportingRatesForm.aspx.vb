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
            If Not ViewState("SortDirection") Is Nothing Then
                Return ViewState("SortDirection").ToString
            Else
                Return String.Empty
            End If

        End Get
        Set(ByVal value As String)
            ViewState("SortDirection") = value
        End Set
    End Property

#End Region


#Region "Page Parameters"

    Private Sub Page_PageCall(ByVal CallFromUrl As String, ByVal CallingPar As Object) Handles MyBase.PageCall
        Try
            If Not Me.CallingParameters Is Nothing Then
                Me.State.selectedInvoiceRateId = CType(Me.CallingParameters, Guid)

                If Not Me.State.selectedInvoiceRateId.Equals(Guid.Empty) Then
                    Me.State.MyInvoiceRateBO = New AfaInvoiceRate(Me.State.selectedInvoiceRateId)
                    Me.State.MyAFProductBO = New AfAProduct(Me.State.MyInvoiceRateBO.AfaProductId)
                End If
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub
#End Region

#Region "Bread Crum"
    Private Sub UpdateBreadCrum()
        Me.MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage(PAGETAB)
        Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage(SUMMARYTITLE)
        Me.MasterPage.UsePageTabTitleInBreadCrum = False
        Me.MasterPage.BreadCrum = TranslationBase.TranslateLabelOrMessage(PAGETAB) + ElitaBase.Sperator + TranslationBase.TranslateLabelOrMessage(PAGETITLE)
    End Sub
#End Region

#Region "Page Events"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.MasterPage.MessageController.Clear()
        Try
            If Not Me.IsPostBack Then
                UpdateBreadCrum()

                ControlMgr.SetVisibleControl(Me, moSearchResults, False)

                Me.State.PageIndex = 0
                Me.TranslateGridHeader(Grid)
                Me.TranslateGridControls(Grid)
                PopulateHeader()
                Me.State.searchDV = Nothing
                If Not Me.State.selectedInvoiceRateId.Equals(Guid.Empty) Then
                    PopulateGrid()
                End If
            Else
                CheckIfComingFromDeleteConfirm()
                BindBoPropertiesToGridHeaders()
            End If

        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
        'Me.ShowMissingTranslations(Me.MasterPage.MessageController)
    End Sub

#End Region


#Region "Helper functions"

    Protected Sub CheckIfComingFromDeleteConfirm()
        Dim confResponse As String = Me.HiddenDeletePromptResponse.Value
        If Not confResponse Is Nothing AndAlso confResponse = Me.MSG_VALUE_YES Then
            If Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Delete Then
                DoDelete()
            End If
            Select Case Me.State.ActionInProgress
                Case ElitaPlusPage.DetailPageCommand.Delete
            End Select
        ElseIf Not confResponse Is Nothing AndAlso confResponse = Me.MSG_VALUE_NO Then
            Select Case Me.State.ActionInProgress
                Case ElitaPlusPage.DetailPageCommand.Delete
            End Select
        End If
        'Clean after consuming the action
        Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
        Me.HiddenDeletePromptResponse.Value = ""
    End Sub

    Private Sub DoDelete()
        'Do the delete here
        Me.State.ActionInProgress = DetailPageCommand.Nothing_

        Dim obj As AfaReportingRates = New AfaReportingRates(Me.State.ReportingRateID)

        obj.Delete()

        'Call the Save() method in the ReportingRates Business Object here
        obj.Save()

        Me.MasterPage.MessageController.AddSuccess(Me.MSG_RECORD_DELETED_OK, True)

        Me.State.PageIndex = Grid.PageIndex

        'Set the IsAfterSave flag to TRUE so that the Paging logic gets invoked
        Me.State.IsAfterSave = True
        Me.State.searchDV = Nothing
        PopulateGrid()
        Me.State.PageIndex = Grid.PageIndex
        Me.State.IsEditMode = False
        SetControlState()
    End Sub

    Private Sub SetControlState()
        If (Me.State.IsEditMode) Then
            ControlMgr.SetVisibleControl(Me, btnNew, False)
            Me.MenuEnabled = False
            If (Me.cboPageSize.Enabled) Then
                ControlMgr.SetEnableControl(Me, cboPageSize, False)
            End If
        Else
            ControlMgr.SetVisibleControl(Me, btnNew, True)
            Me.MenuEnabled = True
            If Not (Me.cboPageSize.Enabled) Then
                ControlMgr.SetEnableControl(Me, Me.cboPageSize, True)
            End If
        End If
    End Sub

    Protected Sub BindBoPropertiesToGridHeaders()
        Me.BindBOPropertyToGridHeader(Me.State.MyReportingRateBO, "RiskFee", Me.Grid.Columns(Me.GRID_COL_RISK_FEE_IDX))
        Me.BindBOPropertyToGridHeader(Me.State.MyReportingRateBO, "SpmCoe", Me.Grid.Columns(Me.GRID_COL_SPM_COE_IDX))
        Me.BindBOPropertyToGridHeader(Me.State.MyReportingRateBO, "FullfillmentNotification", Me.Grid.Columns(Me.GRID_COL_FULLFILLMENT_NOTIFICATION_IDX))
        Me.BindBOPropertyToGridHeader(Me.State.MyReportingRateBO, "MarketingExpenses", Me.Grid.Columns(Me.GRID_COL_MARKETING_EXPENSES_IDX))
        Me.BindBOPropertyToGridHeader(Me.State.MyReportingRateBO, "PremiumTaxes", Me.Grid.Columns(Me.GRID_COL_PREMIUM_TAXES_IDX))
        Me.BindBOPropertyToGridHeader(Me.State.MyReportingRateBO, "LossReserveCost", Me.Grid.Columns(Me.GRID_COL_LOSS_RESERVE_COST_IDX))
        Me.BindBOPropertyToGridHeader(Me.State.MyReportingRateBO, "Overhead", Me.Grid.Columns(Me.GRID_COL_OVERHEAD_IDX))
        Me.BindBOPropertyToGridHeader(Me.State.MyReportingRateBO, "GeneralExpenses", Me.Grid.Columns(Me.GRID_COL_GENERAL_EXPENSES_IDX))
        Me.BindBOPropertyToGridHeader(Me.State.MyReportingRateBO, "Assessments", Me.Grid.Columns(Me.GRID_COL_ASSESSMENTS_IDX))
        Me.BindBOPropertyToGridHeader(Me.State.MyReportingRateBO, "Lae", Me.Grid.Columns(Me.GRID_COL_LAE_IDX))
        Me.ClearGridViewHeadersAndLabelsErrSign()
    End Sub

    Private Sub ReturnFromEditing()

        Grid.EditIndex = NO_ROW_SELECTED_INDEX

        If (Me.Grid.PageCount = 0) Then
            'if returning to the "1st time in" screen
            ControlMgr.SetVisibleControl(Me, Grid, False)
        Else
            ControlMgr.SetVisibleControl(Me, Grid, True)
        End If

        Me.State.IsEditMode = False
        Me.PopulateGrid()
        Me.State.PageIndex = Grid.PageIndex
        SetControlState()
    End Sub

    Private Sub RemoveNewRowFromSearchDV()
        Dim rowind As Integer = NO_ITEM_SELECTED_INDEX
        With State
            If Not .searchDV Is Nothing Then
                rowind = FindSelectedRowIndexFromGuid(.searchDV, .ReportingRateID)
            End If
        End With
        If rowind <> NO_ITEM_SELECTED_INDEX Then State.searchDV.Delete(rowind)
    End Sub

    Private Function PopulateBOFromForm() As Boolean
        Dim objtxt As TextBox

        With Me.State.MyReportingRateBO
            objtxt = CType(Grid.Rows((Me.Grid.EditIndex)).Cells(GRID_COL_RISK_FEE_IDX).FindControl(GRID_CTRL_NAME_EDIT_RISK_FEE), TextBox)
            PopulateBOProperty(State.MyReportingRateBO, "RiskFee", objtxt)
            objtxt = CType(Grid.Rows((Me.Grid.EditIndex)).Cells(GRID_COL_SPM_COE_IDX).FindControl(GRID_CTRL_NAME_EDIT_SPM_COE), TextBox)
            PopulateBOProperty(State.MyReportingRateBO, "SpmCoe", objtxt)
            objtxt = CType(Grid.Rows((Me.Grid.EditIndex)).Cells(GRID_COL_FULLFILLMENT_NOTIFICATION_IDX).FindControl(GRID_CTRL_NAME_EDIT_FULLFILLMENT_NOTIFICATION), TextBox)
            PopulateBOProperty(State.MyReportingRateBO, "FullfillmentNotification", objtxt)
            objtxt = CType(Grid.Rows((Me.Grid.EditIndex)).Cells(GRID_COL_MARKETING_EXPENSES_IDX).FindControl(GRID_CTRL_NAME_EDIT_MARKETING_EXPENSES), TextBox)
            PopulateBOProperty(State.MyReportingRateBO, "MarketingExpenses", objtxt)
            objtxt = CType(Grid.Rows((Me.Grid.EditIndex)).Cells(GRID_COL_PREMIUM_TAXES_IDX).FindControl(GRID_CTRL_NAME_EDIT_PREMIUM_TAXES), TextBox)
            PopulateBOProperty(State.MyReportingRateBO, "PremiumTaxes", objtxt)
            objtxt = CType(Grid.Rows((Me.Grid.EditIndex)).Cells(GRID_COL_LOSS_RESERVE_COST_IDX).FindControl(GRID_CTRL_NAME_EDIT_LOSS_RESERVE_COST), TextBox)
            PopulateBOProperty(State.MyReportingRateBO, "LossReserveCost", objtxt)
            objtxt = CType(Grid.Rows((Me.Grid.EditIndex)).Cells(GRID_COL_OVERHEAD_IDX).FindControl(GRID_CTRL_NAME_EDIT_OVERHEAD), TextBox)
            PopulateBOProperty(State.MyReportingRateBO, "Overhead", objtxt)
            objtxt = CType(Grid.Rows((Me.Grid.EditIndex)).Cells(GRID_COL_GENERAL_EXPENSES_IDX).FindControl(GRID_CTRL_NAME_EDIT_GENERAL_EXPENSES), TextBox)
            PopulateBOProperty(State.MyReportingRateBO, "GeneralExpenses", objtxt)
            objtxt = CType(Grid.Rows((Me.Grid.EditIndex)).Cells(GRID_COL_ASSESSMENTS_IDX).FindControl(GRID_CTRL_NAME_EDIT_ASSESSMENTS), TextBox)
            PopulateBOProperty(State.MyReportingRateBO, "Assessments", objtxt)
            objtxt = CType(Grid.Rows((Me.Grid.EditIndex)).Cells(GRID_COL_LAE_IDX).FindControl(GRID_CTRL_NAME_EDIT_LAE), TextBox)
            PopulateBOProperty(State.MyReportingRateBO, "Lae", objtxt)

        End With

        If Me.ErrCollection.Count > 0 Then
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

            Me.State.searchDV.Sort = Me.SortDirection
            If (Me.State.IsAfterSave) Then
                Me.State.IsAfterSave = False
                Me.SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.ReportingRateID, Me.Grid, Me.State.PageIndex)
            ElseIf (Me.State.IsEditMode) Then
                Me.SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.ReportingRateID, Me.Grid, Me.State.PageIndex, Me.State.IsEditMode)
            Else
                Me.SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Guid.Empty, Me.Grid, Me.State.PageIndex)
            End If

            Me.Grid.AutoGenerateColumns = False
            SortAndBindGrid(blnNewSearch)

        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub SortAndBindGrid(Optional ByVal blnShowErr As Boolean = True)

        Me.TranslateGridControls(Grid)

        If (Me.State.searchDV.Count = 0) Then
            Me.State.searchDV = Nothing
            Me.State.MyReportingRateBO = New AfaReportingRates
            Me.State.MyReportingRateBO.AddNewRowToSearchDV(Me.State.searchDV, Me.State.MyReportingRateBO)
            Me.Grid.DataSource = Me.State.searchDV
            Me.Grid.DataBind()
            Me.Grid.Rows(0).Visible = False
            Me.State.IsGridAddNew = True
            Me.State.IsGridVisible = False
            If blnShowErr Then
                Me.MasterPage.MessageController.AddInformation(ElitaPlus.ElitaPlusWebApp.Message.MSG_NO_RECORDS_FOUND, True)
            End If
        Else
            Me.Grid.Enabled = True
            Me.Grid.PageSize = Me.State.PageSize
            Me.Grid.DataSource = Me.State.searchDV
            Me.State.IsGridVisible = True
            HighLightSortColumn(Grid, Me.SortDirection)
            Me.Grid.DataBind()
        End If

        ControlMgr.SetVisibleControl(Me, Grid, Me.State.IsGridVisible)
        ControlMgr.SetVisibleControl(Me, moSearchResults, Me.State.IsGridVisible)

        Session("recCount") = Me.State.searchDV.Count

        If Me.Grid.Visible Then
            If (Me.State.IsGridAddNew) Then
                Me.lblRecordCount.Text = (Me.State.searchDV.Count - 1) & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
            Else
                Me.lblRecordCount.Text = Me.State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
            End If
        End If
        ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me, Grid)

    End Sub

    Private Sub Grid_PageIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Grid.PageIndexChanged
        Try
            If (Not (Me.State.IsEditMode)) Then
                Me.State.PageIndex = Grid.PageIndex
                Me.State.ReportingRateID = Guid.Empty
                Me.PopulateGrid()
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Grid_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Grid.PageIndexChanging
        Try
            Grid.PageIndex = e.NewPageIndex
            State.PageIndex = Grid.PageIndex
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Grid_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles Grid.Sorting
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
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub cboPageSize_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
        Try
            State.PageSize = CType(cboPageSize.SelectedValue, Integer)
            Me.State.PageIndex = NewCurrentPageIndex(Grid, State.searchDV.Count, State.PageSize)
            Me.Grid.PageIndex = Me.State.PageIndex
            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Grid_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowDataBound
        Try
            Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
            Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
            Dim strID As String
            Dim moLabel As Label
            Dim moTextBox As TextBox
            Dim moImageButton As ImageButton
            Dim btnEditItem As LinkButton

            If Not dvRow Is Nothing Then
                strID = GetGuidStringFromByteArray(CType(dvRow(AfaReportingRates.RptRateSearchDV.COL_AFA_REPORTING_RATE_ID), Byte()))
                If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then
                    CType(e.Row.Cells(Me.GRID_COL_RPT_RATE_ID_IDX).FindControl(Me.GRID_CTRL_NAME_LABEL_RPT_RATE_ID), Label).Text = strID

                    If (Me.State.IsEditMode = True AndAlso Me.State.ReportingRateID.ToString.Equals(strID)) Then

                        CType(e.Row.Cells(Me.GRID_COL_RISK_FEE_IDX).FindControl(Me.GRID_CTRL_NAME_EDIT_RISK_FEE), TextBox).Text = dvRow(AfaReportingRates.RptRateSearchDV.COL_RISK_FEE).ToString
                        CType(e.Row.Cells(Me.GRID_COL_SPM_COE_IDX).FindControl(Me.GRID_CTRL_NAME_EDIT_SPM_COE), TextBox).Text = dvRow(AfaReportingRates.RptRateSearchDV.COL_SPM_COE).ToString
                        CType(e.Row.Cells(Me.GRID_COL_FULLFILLMENT_NOTIFICATION_IDX).FindControl(Me.GRID_CTRL_NAME_EDIT_FULLFILLMENT_NOTIFICATION), TextBox).Text = dvRow(AfaReportingRates.RptRateSearchDV.COL_FULLFILLMENT_NOTIFICATION).ToString
                        CType(e.Row.Cells(Me.GRID_COL_MARKETING_EXPENSES_IDX).FindControl(Me.GRID_CTRL_NAME_EDIT_MARKETING_EXPENSES), TextBox).Text = dvRow(AfaReportingRates.RptRateSearchDV.COL_MARKETING_EXPENSES).ToString
                        CType(e.Row.Cells(Me.GRID_COL_PREMIUM_TAXES_IDX).FindControl(Me.GRID_CTRL_NAME_EDIT_PREMIUM_TAXES), TextBox).Text = dvRow(AfaReportingRates.RptRateSearchDV.COL_PREMIUM_TAXES).ToString
                        CType(e.Row.Cells(Me.GRID_COL_LOSS_RESERVE_COST_IDX).FindControl(Me.GRID_CTRL_NAME_EDIT_LOSS_RESERVE_COST), TextBox).Text = dvRow(AfaReportingRates.RptRateSearchDV.COL_LOSS_RESERVE_COST).ToString
                        CType(e.Row.Cells(Me.GRID_COL_OVERHEAD_IDX).FindControl(Me.GRID_CTRL_NAME_EDIT_OVERHEAD), TextBox).Text = dvRow(AfaReportingRates.RptRateSearchDV.COL_OVERHEAD).ToString
                        CType(e.Row.Cells(Me.GRID_COL_GENERAL_EXPENSES_IDX).FindControl(Me.GRID_CTRL_NAME_EDIT_GENERAL_EXPENSES), TextBox).Text = dvRow(AfaReportingRates.RptRateSearchDV.COL_GENERAL_EXPENSES).ToString
                        CType(e.Row.Cells(Me.GRID_COL_ASSESSMENTS_IDX).FindControl(Me.GRID_CTRL_NAME_EDIT_ASSESSMENTS), TextBox).Text = dvRow(AfaReportingRates.RptRateSearchDV.COL_ASSESSMENTS).ToString
                        CType(e.Row.Cells(Me.GRID_COL_LAE_IDX).FindControl(Me.GRID_CTRL_NAME_EDIT_LAE), TextBox).Text = dvRow(AfaReportingRates.RptRateSearchDV.COL_LAE).ToString
                    Else

                        CType(e.Row.Cells(Me.GRID_COL_RISK_FEE_IDX).FindControl(Me.GRID_CTRL_NAME_LABEL_RISK_FEE), Label).Text = dvRow(AfaReportingRates.RptRateSearchDV.COL_RISK_FEE).ToString

                        CType(e.Row.Cells(Me.GRID_COL_SPM_COE_IDX).FindControl(Me.GRID_CTRL_NAME_LABEL_SPM_COE), Label).Text = dvRow(AfaReportingRates.RptRateSearchDV.COL_SPM_COE).ToString

                        CType(e.Row.Cells(Me.GRID_COL_FULLFILLMENT_NOTIFICATION_IDX).FindControl(Me.GRID_CTRL_NAME_LABEL_FULLFILLMENT_NOTIFICATION), Label).Text = dvRow(AfaReportingRates.RptRateSearchDV.COL_FULLFILLMENT_NOTIFICATION).ToString
                        CType(e.Row.Cells(Me.GRID_COL_MARKETING_EXPENSES_IDX).FindControl(Me.GRID_CTRL_NAME_LABEL_MARKETING_EXPENSES), Label).Text = dvRow(AfaReportingRates.RptRateSearchDV.COL_MARKETING_EXPENSES).ToString
                        CType(e.Row.Cells(Me.GRID_COL_PREMIUM_TAXES_IDX).FindControl(Me.GRID_CTRL_NAME_LABEL_PREMIUM_TAXES), Label).Text = dvRow(AfaReportingRates.RptRateSearchDV.COL_PREMIUM_TAXES).ToString
                        CType(e.Row.Cells(Me.GRID_COL_LOSS_RESERVE_COST_IDX).FindControl(Me.GRID_CTRL_NAME_LABEL_LOSS_RESERVE_COST), Label).Text = dvRow(AfaReportingRates.RptRateSearchDV.COL_LOSS_RESERVE_COST).ToString
                        CType(e.Row.Cells(Me.GRID_COL_OVERHEAD_IDX).FindControl(Me.GRID_CTRL_NAME_LABEL_OVERHEAD), Label).Text = dvRow(AfaReportingRates.RptRateSearchDV.COL_OVERHEAD).ToString
                        CType(e.Row.Cells(Me.GRID_COL_GENERAL_EXPENSES_IDX).FindControl(Me.GRID_CTRL_NAME_LABEL_GENERAL_EXPENSES), Label).Text = dvRow(AfaReportingRates.RptRateSearchDV.COL_GENERAL_EXPENSES).ToString
                        CType(e.Row.Cells(Me.GRID_COL_ASSESSMENTS_IDX).FindControl(Me.GRID_CTRL_NAME_LABEL_ASSESSMENTS), Label).Text = dvRow(AfaReportingRates.RptRateSearchDV.COL_ASSESSMENTS).ToString
                        CType(e.Row.Cells(Me.GRID_COL_LAE_IDX).FindControl(Me.GRID_CTRL_NAME_LABEL_LAE), Label).Text = dvRow(AfaReportingRates.RptRateSearchDV.COL_LAE).ToString

                    End If
                End If
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try

    End Sub


    Public Sub RowCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)

        Try
            Dim index As Integer
            If (e.CommandName = Me.EDIT_COMMAND) Then
                index = CInt(e.CommandArgument)
                'Do the Edit here

                'Set the IsEditMode flag to TRUE
                Me.State.IsEditMode = True

                Me.State.ReportingRateID = New Guid(CType(Me.Grid.Rows(index).Cells(Me.GRID_COL_RPT_RATE_ID_IDX).FindControl(Me.GRID_CTRL_NAME_LABEL_RPT_RATE_ID), Label).Text)
                Me.State.MyReportingRateBO = New AfaReportingRates(Me.State.ReportingRateID)

                Me.PopulateGrid()

                Me.State.PageIndex = Grid.PageIndex

                Me.SetControlState()

            ElseIf (e.CommandName = Me.DELETE_COMMAND) Then
                index = CInt(e.CommandArgument)
                Me.State.ReportingRateID = New Guid(CType(Me.Grid.Rows(index).Cells(Me.GRID_COL_RPT_RATE_ID_IDX).FindControl(Me.GRID_CTRL_NAME_LABEL_RPT_RATE_ID), Label).Text)
                Me.DisplayMessage(Message.DELETE_RECORD_PROMPT, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Me.HiddenDeletePromptResponse)
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Delete

            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try

    End Sub

    Public Sub RowCreated(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
        Try
            BaseItemCreated(sender, e)
        Catch ex As Exception
            HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

#End Region


#Region "Control Handler"

    Private Sub PopulateHeader()
        Try
            If Not Me.State.selectedInvoiceRateId.Equals(Guid.Empty) Then
                txtProductCode.Text = Me.State.MyAFProductBO.Code
                txtInsCode.Text = Me.State.MyInvoiceRateBO.InsuranceCode
                txtHandsetTier.Text = Me.State.MyInvoiceRateBO.Tier
                txtLossType.Text = LookupListNew.GetDescrionFromListCode("CTYP", Me.State.MyInvoiceRateBO.LossType)
            End If

        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnNew.Click

        Try
            Me.State.IsEditMode = True
            Me.State.IsGridVisible = True
            Me.State.IsGridAddNew = True
            AddNew()
            Me.SetControlState()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try

    End Sub

    Private Sub AddNew()

        Me.State.MyReportingRateBO = New AfaReportingRates
        Me.State.MyReportingRateBO.AfaInvoiceRateId = Me.State.MyInvoiceRateBO.Id
        State.MyReportingRateBO.AddNewRowToSearchDV(Me.State.searchDV, Me.State.MyReportingRateBO)

        Me.State.ReportingRateID = Me.State.MyReportingRateBO.Id
        State.IsGridAddNew = True
        PopulateGrid()
        'Set focus on the Code TextBox for the EditItemIndex row
        Me.SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.ReportingRateID, Me.Grid, _
                                           Me.State.PageIndex, Me.State.IsEditMode)
        ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me, Grid)
        SetGridControls(Me.Grid, False)
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As EventArgs)

        Try
            PopulateBOFromForm()
            If (Me.State.MyReportingRateBO.IsDirty) Then
                Me.State.MyReportingRateBO.Save()
                Me.State.IsAfterSave = True
                Me.State.IsGridAddNew = False
                Me.MasterPage.MessageController.AddSuccess(Me.MSG_RECORD_SAVED_OK, True)
                Me.State.searchDV = Nothing
                Me.State.MyReportingRateBO = Nothing
                Me.ReturnFromEditing()
            Else
                Me.MasterPage.MessageController.AddWarning(Me.MSG_RECORD_NOT_SAVED, True)
                Me.ReturnFromEditing()
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try

    End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As EventArgs)
        Try
            With State
                If .IsGridAddNew Then
                    RemoveNewRowFromSearchDV()
                    .IsGridAddNew = False
                    Grid.PageIndex = .PageIndex
                End If
                .ReportingRateID = Guid.Empty
                Me.State.MyReportingRateBO = Nothing
                .IsEditMode = False
            End With
            Grid.EditIndex = NO_ITEM_SELECTED_INDEX
            'Me.State.searchDV = Nothing
            PopulateGrid()
            SetControlState()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub
#End Region


#Region "Button Click Handlers"

    Private Sub btnBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Try
            Me.ReturnToCallingPage()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub
#End Region
End Class