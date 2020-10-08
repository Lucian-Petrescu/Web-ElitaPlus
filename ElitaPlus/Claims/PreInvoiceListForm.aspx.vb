Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports Assurant.Elita.Web.Forms
Imports System.Threading
Partial Class PreInvoiceListForm
    Inherits ElitaPlusSearchPage

#Region "Constants"

    Public Const SELECT_ACTION_COMMAND As String = "SelectAction"
    Private Const LABEL_SELECT_COMPANY As String = "SELECT_COMPANY"
    Public Const GRID_COL_BATCH_NUMBER_IDX As Integer = 0
    Public Const GRID_COL_STATUS_IDX As Integer = 1
    Public Const GRID_COL_CREATED_DATE_IDX As Integer = 2
    Public Const GRID_COL_DISPLAY_DATE_IDX As Integer = 3
    Public Const GRID_COL_CLAIMS_COUNT_IDX As Integer = 4
    Public Const GRID_COL_TOTAL_AUTH_AMOUNT_IDX As Integer = 5
    Public Const GRID_COL_BONUS_AMOUNT_IDX As Integer = 6
    Public Const GRID_COL_DEDUCTIBLE_IDX As Integer = 7
    Public Const GRID_COL_TOTAL_AMOUNT_IDX As Integer = 8
    Public Const GRID_COL_PRE_INVOICE_ID_NUMBER_IDX As Integer = 9
    Public Const GRID_COL_EDIT_BATCH_NUMBER_CTRL As String = "btnEditBatchNumber"
#End Region

#Region "Page State"
    Private IsReturningFromChild As Boolean = False

    Class MyState

        Public MyBO As PreInvoice
        Public selectedPreInvoiceID As Guid = Guid.Empty
        Public searchDV As PreInvoice.PreinvoiceSearchDV = Nothing
        Public searchBtnClicked As Boolean = False
        Public selectedBatchNumber As String = String.Empty
        Public IsGridVisible As Boolean = False
        Public SortExpression As String = String.Empty
        Public PageIndex As Integer = 0
        Public PageSize As Integer = 30
        Public BatchNumber As String
        Public selectedStatusId As Guid = Guid.Empty
        Public CreatedDateFrom As String = String.Empty
        Public CreatedDateTo As String = String.Empty

        'parameters to the detail page
        Public status As String = String.Empty
        Public createDate As String = String.Empty
        Public displayDate As String = String.Empty
        Public claims As String = String.Empty
        Public totalBonusAmount As String = String.Empty
        Public Deductible As String = String.Empty
        Public totalAmount As String = String.Empty
        Public selectedCompanyId As Guid = Guid.Empty
        Public selectedCompanycode As String = String.Empty
        Public selectedCompanyDesc As String = String.Empty

        Public HasDataChanged As Boolean

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

#Region "Page event handlers"
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        MasterPage.MessageController.Clear()

        Form.DefaultButton = btnSearch.UniqueID
        Try
            If Not IsPostBack Then

                MasterPage.MessageController.Clear()

                ' Populate the header and bredcrumb
                MasterPage.UsePageTabTitleInBreadCrum = False
                MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("Claims")
                UpdateBreadCrum()


                TranslateGridHeader(Grid)
                'Me.TranslateGridControls(Grid)
                AddCalendar_New(btnCreatedDateFromCert, txtCreatedDateFrom)
                AddCalendar_New(btnCreatedDateToCert, txtCreatedDateTo)
                PopulateCompanyDropDown()
                populatePreInvoiceStatusDropdown()
                GetStateProperties()

                cboPageSize.SelectedValue = CType(State.PageSize, String)

                If Not IsReturningFromChild Then
                    ControlMgr.SetVisibleControl(Me, trPageSize, False)
                End If

                PopulateSearchFieldsFromState()

                If IsReturningFromChild Then
                    PopulateGrid()
                End If

                SetFocus(txtBatchNumber)
            End If
            DisplayNewProgressBarOnClick(btnSearch, "Loading Information")
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
        ShowMissingTranslations(MasterPage.MessageController)
    End Sub


    Private Sub UpdateBreadCrum()
        If (State IsNot Nothing) Then
            If (State IsNot Nothing) Then
                MasterPage.BreadCrum = MasterPage.PageTab & ElitaBase.Sperator &
                    TranslationBase.TranslateLabelOrMessage("PRE_INVOICE_SEARCH")
                MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("PRE_INVOICE_SEARCH")
            End If
        End If
    End Sub


    Private Sub Page_PageReturn(ReturnFromUrl As String, ReturnPar As Object) Handles MyBase.PageReturn
        Try
            MenuEnabled = True
            IsReturningFromChild = True
            State.searchDV = Nothing
            'Dim retObj As PreinvoiceDetailForm.ReturnType = CType(ReturnPar, PreinvoiceDetailForm.ReturnType)
            'If Not retObj Is Nothing AndAlso retObj.BoChanged Then
            '    Me.State.searchDV = Nothing
            'End If
            'Select Case retObj.LastOperation
            '    Case ElitaPlusPage.DetailPageCommand.Back
            '        If Not retObj Is Nothing Then
            '            If Not retObj.EditingBo.IsNew Then
            '                Me.State.selectedPreInvoiceID = retObj.EditingBo.Id
            '            End If
            '            Me.State.IsGridVisible = True
            '        End If
            'End Select
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

#End Region

#Region "Dropdowns Populate"
    Private Sub PopulateCompanyDropDown()
        Dim dv As DataView = LookupListNew.GetUserCompaniesLookupList()

        moMultipleColumnDrop.NothingSelected = True
        moMultipleColumnDrop.SetControl(True, MultipleColumnDDLabelControl_New.MODES.NEW_MODE, True, dv, "* " + TranslationBase.TranslateLabelOrMessage(LABEL_SELECT_COMPANY), True, False)
        If dv.Count.Equals(1) Then
            moMultipleColumnDrop.SelectedIndex = 1
            moMultipleColumnDrop.Visible = False
            trSeparator.Visible = False
        End If

    End Sub

    Private Sub populatePreInvoiceStatusDropdown()
        Try

            Dim preInvoiceStatusList As ListItem() = CommonConfigManager.Current.ListManager.GetList("PREINVSTAT", Thread.CurrentPrincipal.GetLanguageCode())
            preInvoiceStatusList.OrderBy("Code", LinqExtentions.SortDirection.Ascending)
            ddlStatus.Populate(preInvoiceStatusList, New PopulateOptions() With
                                              {
                                                .AddBlankItem = True
                                               })
        Catch ex As Exception

        End Try
    End Sub
#End Region

#Region "Button event handlers"
    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Try
            State.searchBtnClicked = True
            State.PageIndex = 0
            State.selectedBatchNumber = String.Empty
            State.IsGridVisible = True
            State.searchDV = Nothing
            PopulateGrid()
            State.searchBtnClicked = False
            If State.searchDV IsNot Nothing Then
                ValidSearchResultCountNew(State.searchDV.Count, True)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub PopulateGrid()
        Try
            'Validating the Company selection
            If moMultipleColumnDrop.SelectedGuid.Equals(Guid.Empty) Then
                SetLabelError(moMultipleColumnDrop.CaptionLabel)
                Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_COMPANY_IS_REQUIRED)
            End If

            PopulateStateFromSearchFields()
            If ((State.searchDV Is Nothing) OrElse (State.HasDataChanged)) Then
                State.searchDV = PreInvoice.LoadPreInvoiceProcess(State.selectedCompanyId, State.selectedStatusId, State.BatchNumber, State.CreatedDateFrom, State.CreatedDateTo)
            End If

            Grid.PageSize = State.PageSize
            If Not (State.searchDV Is Nothing) Then
                If State.searchBtnClicked Then
                    State.SortExpression = PreInvoice.PreinvoiceSearchDV.COL_BATCH_NUMBER
                    State.SortExpression &= " DESC"
                    State.searchDV.Sort = State.SortExpression
                Else
                    State.searchDV.Sort = State.SortExpression
                End If

                SetPageAndSelectedIndexFromGuid(State.searchDV, State.selectedPreInvoiceID, Grid, State.PageIndex)
                Grid.DataSource = State.searchDV
                State.PageIndex = Grid.PageIndex

                HighLightSortColumn(Grid, State.SortExpression, IsNewUI)
                Grid.DataBind()

                ControlMgr.SetVisibleControl(Me, Grid, True)
                ControlMgr.SetVisibleControl(Me, trPageSize, Grid.Visible)
                Session("recCount") = State.searchDV.Count

                If Grid.Visible Then
                    lblRecordCount.Text = State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                End If

                If State.searchDV.Count = 0 Then
                    For Each gvRow As GridViewRow In Grid.Rows
                        gvRow.Visible = False
                        gvRow.Controls.Clear()
                    Next
                    lblPageSize.Visible = False
                    cboPageSize.Visible = False
                    colonSepertor.Visible = False
                Else
                    lblPageSize.Visible = True
                    cboPageSize.Visible = True
                    colonSepertor.Visible = True
                End If

            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnNewInvCycle_Click(sender As Object, e As EventArgs) Handles btnNewInvCycle.Click
        Try
            Dim oCompanyCode As String = moMultipleColumnDrop.SelectedCode
            'Validating the Company selection
            If moMultipleColumnDrop.SelectedGuid.Equals(Guid.Empty) Then
                SetLabelError(moMultipleColumnDrop.CaptionLabel)
                Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_COMPANY_IS_REQUIRED)
            End If

            Dim errMsg As String = PreInvoice.GeneratePreInvoice(oCompanyCode.Trim)
            btnSearch_Click(sender, e)

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

#End Region

#Region "Helper functions"
    Private Sub PopulateStateFromSearchFields()
        Try
            If State Is Nothing Then
                Trace(Me, "Restoring State")
                RestoreState(New MyState)
            End If

            ClearStateValues()
            State.selectedCompanyId = moMultipleColumnDrop.SelectedGuid
            State.BatchNumber = txtBatchNumber.Text.ToUpper.Trim
            State.selectedStatusId = GetSelectedItem(ddlStatus)
            State.CreatedDateFrom = txtCreatedDateFrom.Text.Trim
            State.CreatedDateTo = txtCreatedDateTo.Text.Trim

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Public Sub PopulateSearchFieldsFromState()

        Try
            txtBatchNumber.Text = State.BatchNumber
            txtCreatedDateFrom.Text = State.CreatedDateFrom
            txtCreatedDateTo.Text = State.CreatedDateTo
            SetSelectedItem(ddlStatus, State.selectedStatusId)

            If (State.selectedCompanyId <> Guid.Empty) Then
                moMultipleColumnDrop.SelectedGuid = State.selectedCompanyId
            End If

        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try

    End Sub

    Protected Sub ClearStateValues()
        Try
            'clear State
            State.BatchNumber = String.Empty
            State.selectedStatusId = Guid.Empty
            State.CreatedDateFrom = String.Empty
            State.CreatedDateTo = String.Empty

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try

    End Sub

    Private Sub GetStateProperties()
        Try
            txtBatchNumber.Text = State.BatchNumber
            SetSelectedItem(ddlStatus, State.selectedStatusId)
            txtCreatedDateFrom.Text = State.CreatedDateFrom
            txtCreatedDateTo.Text = State.CreatedDateTo

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

#End Region



#Region "Grid Events"
    Public Property SortDirection() As String
        Get
            Return ViewState("SortDirection").ToString
        End Get
        Set(value As String)
            ViewState("SortDirection") = value
        End Set
    End Property

    'Private Sub SortAndBindGrid()
    '    Me.State.PageIndex = Me.Grid.PageIndex
    '    If (Me.State.searchDV.Count = 0) Then

    '        'Me.State.bnoRow = True
    '        CreateHeaderForEmptyGrid(Grid, Me.SortDirection)
    '    Else
    '        'Me.State.bnoRow = False
    '        Me.Grid.Enabled = True
    '        Me.Grid.DataSource = Me.State.searchDV
    '        'HighLightSortColumn(Grid, Me.state.searchDV.)
    '        Me.Grid.DataBind()
    '    End If
    '    If Not Grid.BottomPagerRow.Visible Then Grid.BottomPagerRow.Visible = True
    '    'Me.Grid.DataSource = Me.State.searchDV
    '    'HighLightSortColumn(Grid, Me.SortDirection)
    '    'Me.Grid.DataBind()

    '    ControlMgr.SetVisibleControl(Me, Grid, Me.State.IsGridVisible)

    '    ControlMgr.SetVisibleControl(Me, trPageSize, Me.Grid.Visible)

    '    Session("recCount") = Me.State.searchDV.Count

    '    If Me.State.searchDV.Count > 0 Then

    '        If Me.Grid.Visible Then
    '            Me.lblRecordCount.Text = Me.State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
    '        End If
    '    Else
    '        If Me.Grid.Visible Then
    '            Me.lblRecordCount.Text = Me.State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
    '        End If
    '    End If
    'End Sub

    Public Sub ItemCreated(sender As Object, e As GridViewRowEventArgs) Handles Grid.RowCreated
        BaseItemCreated(sender, e)
    End Sub

    Private Sub Grid_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles Grid.PageIndexChanging
        Try
            State.PageIndex = e.NewPageIndex

            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Grid_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles Grid.RowCommand
        Try
            Dim index As Integer
            If e.CommandName = SELECT_ACTION_COMMAND Then
                index = CInt(e.CommandArgument)

                State.selectedPreInvoiceID = New Guid(Grid.Rows(index).Cells(GRID_COL_PRE_INVOICE_ID_NUMBER_IDX).Text)
                State.selectedBatchNumber = Grid.DataKeys(index).Values(0).ToString()
                State.status = Grid.Rows(index).Cells(GRID_COL_STATUS_IDX).Text
                State.createDate = Grid.Rows(index).Cells(GRID_COL_CREATED_DATE_IDX).Text
                State.displayDate = Grid.Rows(index).Cells(GRID_COL_DISPLAY_DATE_IDX).Text
                State.claims = Grid.Rows(index).Cells(GRID_COL_CLAIMS_COUNT_IDX).Text
                State.totalBonusAmount = Grid.Rows(index).Cells(GRID_COL_BONUS_AMOUNT_IDX).Text
                State.Deductible = Grid.Rows(index).Cells(GRID_COL_DEDUCTIBLE_IDX).Text
                State.totalAmount = Grid.Rows(index).Cells(GRID_COL_TOTAL_AMOUNT_IDX).Text
                If (moMultipleColumnDrop.CodeDropDown.Items.Count > 2) Then ' one item for empty row and one for actual company code
                    State.selectedCompanycode = moMultipleColumnDrop.SelectedCode
                    State.selectedCompanyDesc = moMultipleColumnDrop.SelectedDesc
                End If
                callPage(PreinvoiceDetailForm.URL, New PreinvoiceDetailForm.Parameters(State.selectedCompanycode, State.selectedCompanyDesc, State.selectedPreInvoiceID, State.selectedBatchNumber, State.status, State.createDate, State.displayDate, State.claims, State.totalBonusAmount, State.totalAmount, State.Deductible))
            End If

        Catch ex As ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Grid_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles Grid.RowDataBound
        Try
            Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
            Dim btnEditItem As LinkButton
            If (e.Row.RowType = DataControlRowType.DataRow) _
                OrElse (e.Row.RowType = DataControlRowType.Separator) Then
                If (e.Row.Cells(GRID_COL_PRE_INVOICE_ID_NUMBER_IDX).FindControl(GRID_COL_EDIT_BATCH_NUMBER_CTRL) IsNot Nothing) Then
                    PopulateControlFromBOProperty(e.Row.Cells(GRID_COL_PRE_INVOICE_ID_NUMBER_IDX), dvRow(PreInvoice.PreinvoiceSearchDV.COL_PRE_INVOICE_ID))

                    btnEditItem = CType(e.Row.Cells(GRID_COL_BATCH_NUMBER_IDX).FindControl(GRID_COL_EDIT_BATCH_NUMBER_CTRL), LinkButton)
                    'btnEditItem.CommandArgument = GetGuidStringFromByteArray(CType(dvRow(PreInvoice.PreinvoiceSearchDV.COL_PRE_INVOICE_ID), Byte()))
                    btnEditItem.CommandName = SELECT_ACTION_COMMAND
                    btnEditItem.Text = dvRow(PreInvoice.PreinvoiceSearchDV.COL_BATCH_NUMBER).ToString
                    State.selectedBatchNumber = btnEditItem.Text.Trim()
                End If

            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Grid_Sorting(sender As Object, e As GridViewSortEventArgs) Handles Grid.Sorting
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

    Private Sub Grid_PageSizeChanged(source As Object, e As EventArgs) Handles cboPageSize.SelectedIndexChanged
        Try
            Grid.PageIndex = NewCurrentPageIndex(Grid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
            State.PageSize = CType(cboPageSize.SelectedValue, Integer)
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
#End Region

End Class