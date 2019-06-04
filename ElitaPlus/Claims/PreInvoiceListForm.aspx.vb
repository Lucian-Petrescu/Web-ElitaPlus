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
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Me.MasterPage.MessageController.Clear()

        Me.Form.DefaultButton = btnSearch.UniqueID
        Try
            If Not Me.IsPostBack Then

                Me.MasterPage.MessageController.Clear()

                ' Populate the header and bredcrumb
                Me.MasterPage.UsePageTabTitleInBreadCrum = False
                Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("Claims")
                UpdateBreadCrum()


                Me.TranslateGridHeader(Grid)
                'Me.TranslateGridControls(Grid)
                Me.AddCalendar_New(Me.btnCreatedDateFromCert, Me.txtCreatedDateFrom)
                Me.AddCalendar_New(Me.btnCreatedDateToCert, Me.txtCreatedDateTo)
                PopulateCompanyDropDown()
                populatePreInvoiceStatusDropdown()
                GetStateProperties()

                cboPageSize.SelectedValue = CType(Me.State.PageSize, String)

                If Not Me.IsReturningFromChild Then
                    ControlMgr.SetVisibleControl(Me, trPageSize, False)
                End If

                PopulateSearchFieldsFromState()

                If Me.IsReturningFromChild Then
                    Me.PopulateGrid()
                End If

                SetFocus(Me.txtBatchNumber)
            End If
            Me.DisplayNewProgressBarOnClick(Me.btnSearch, "Loading Information")
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
        Me.ShowMissingTranslations(Me.MasterPage.MessageController)
    End Sub


    Private Sub UpdateBreadCrum()
        If (Not Me.State Is Nothing) Then
            If (Not Me.State Is Nothing) Then
                Me.MasterPage.BreadCrum = Me.MasterPage.PageTab & ElitaBase.Sperator &
                    TranslationBase.TranslateLabelOrMessage("PRE_INVOICE_SEARCH")
                Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("PRE_INVOICE_SEARCH")
            End If
        End If
    End Sub


    Private Sub Page_PageReturn(ByVal ReturnFromUrl As String, ByVal ReturnPar As Object) Handles MyBase.PageReturn
        Try
            Me.MenuEnabled = True
            Me.IsReturningFromChild = True
            Me.State.searchDV = Nothing
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
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

#End Region

#Region "Dropdowns Populate"
    Private Sub PopulateCompanyDropDown()
        Dim dv As DataView = LookupListNew.GetUserCompaniesLookupList()

        moMultipleColumnDrop.NothingSelected = True
        moMultipleColumnDrop.SetControl(True, moMultipleColumnDrop.MODES.NEW_MODE, True, dv, "* " + TranslationBase.TranslateLabelOrMessage(LABEL_SELECT_COMPANY), True, False)
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
    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            Me.State.searchBtnClicked = True
            Me.State.PageIndex = 0
            Me.State.selectedBatchNumber = String.Empty
            Me.State.IsGridVisible = True
            Me.State.searchDV = Nothing
            PopulateGrid()
            Me.State.searchBtnClicked = False
            If Not Me.State.searchDV Is Nothing Then
                Me.ValidSearchResultCountNew(Me.State.searchDV.Count, True)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub PopulateGrid()
        Try
            'Validating the Company selection
            If moMultipleColumnDrop.SelectedGuid.Equals(Guid.Empty) Then
                ElitaPlusPage.SetLabelError(moMultipleColumnDrop.CaptionLabel)
                Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_COMPANY_IS_REQUIRED)
            End If

            PopulateStateFromSearchFields()
            If ((Me.State.searchDV Is Nothing) OrElse (Me.State.HasDataChanged)) Then
                Me.State.searchDV = PreInvoice.LoadPreInvoiceProcess(Me.State.selectedCompanyId, Me.State.selectedStatusId, Me.State.BatchNumber, Me.State.CreatedDateFrom, Me.State.CreatedDateTo)
            End If

            Me.Grid.PageSize = Me.State.PageSize
            If Not (Me.State.searchDV Is Nothing) Then
                If Me.State.searchBtnClicked Then
                    Me.State.SortExpression = PreInvoice.PreinvoiceSearchDV.COL_BATCH_NUMBER
                    Me.State.SortExpression &= " DESC"
                    Me.State.searchDV.Sort = Me.State.SortExpression
                Else
                    Me.State.searchDV.Sort = Me.State.SortExpression
                End If

                SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.selectedPreInvoiceID, Me.Grid, Me.State.PageIndex)
                Me.Grid.DataSource = Me.State.searchDV
                Me.State.PageIndex = Me.Grid.PageIndex

                HighLightSortColumn(Me.Grid, Me.State.SortExpression, Me.IsNewUI)
                Me.Grid.DataBind()

                ControlMgr.SetVisibleControl(Me, Grid, True)
                ControlMgr.SetVisibleControl(Me, trPageSize, Me.Grid.Visible)
                Session("recCount") = Me.State.searchDV.Count

                If Me.Grid.Visible Then
                    Me.lblRecordCount.Text = Me.State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
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
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnNewInvCycle_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNewInvCycle.Click
        Try
            Dim oCompanyCode As String = moMultipleColumnDrop.SelectedCode
            'Validating the Company selection
            If moMultipleColumnDrop.SelectedGuid.Equals(Guid.Empty) Then
                ElitaPlusPage.SetLabelError(moMultipleColumnDrop.CaptionLabel)
                Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_COMPANY_IS_REQUIRED)
            End If

            Dim errMsg As String = PreInvoice.GeneratePreInvoice(oCompanyCode.Trim)
            btnSearch_Click(sender, e)

        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

#End Region

#Region "Helper functions"
    Private Sub PopulateStateFromSearchFields()
        Try
            If Me.State Is Nothing Then
                Me.Trace(Me, "Restoring State")
                Me.RestoreState(New MyState)
            End If

            Me.ClearStateValues()
            Me.State.selectedCompanyId = moMultipleColumnDrop.SelectedGuid
            Me.State.BatchNumber = Me.txtBatchNumber.Text.ToUpper.Trim
            Me.State.selectedStatusId = Me.GetSelectedItem(ddlStatus)
            Me.State.CreatedDateFrom = Me.txtCreatedDateFrom.Text.Trim
            Me.State.CreatedDateTo = Me.txtCreatedDateTo.Text.Trim

        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Public Sub PopulateSearchFieldsFromState()

        Try
            Me.txtBatchNumber.Text = Me.State.BatchNumber
            Me.txtCreatedDateFrom.Text = Me.State.CreatedDateFrom
            Me.txtCreatedDateTo.Text = Me.State.CreatedDateTo
            Me.SetSelectedItem(Me.ddlStatus, Me.State.selectedStatusId)

            If (Me.State.selectedCompanyId <> Guid.Empty) Then
                Me.moMultipleColumnDrop.SelectedGuid = Me.State.selectedCompanyId
            End If

        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try

    End Sub

    Protected Sub ClearStateValues()
        Try
            'clear State
            Me.State.BatchNumber = String.Empty
            Me.State.selectedStatusId = Guid.Empty
            Me.State.CreatedDateFrom = String.Empty
            Me.State.CreatedDateTo = String.Empty

        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try

    End Sub

    Private Sub GetStateProperties()
        Try
            Me.txtBatchNumber.Text = Me.State.BatchNumber
            Me.SetSelectedItem(ddlStatus, Me.State.selectedStatusId)
            Me.txtCreatedDateFrom.Text = Me.State.CreatedDateFrom
            Me.txtCreatedDateTo.Text = Me.State.CreatedDateTo

        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

#End Region



#Region "Grid Events"
    Public Property SortDirection() As String
        Get
            Return ViewState("SortDirection").ToString
        End Get
        Set(ByVal value As String)
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

    Public Sub ItemCreated(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowCreated
        BaseItemCreated(sender, e)
    End Sub

    Private Sub Grid_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Grid.PageIndexChanging
        Try
            Me.State.PageIndex = e.NewPageIndex

            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Grid_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles Grid.RowCommand
        Try
            Dim index As Integer
            If e.CommandName = SELECT_ACTION_COMMAND Then
                index = CInt(e.CommandArgument)

                Me.State.selectedPreInvoiceID = New Guid(Me.Grid.Rows(index).Cells(Me.GRID_COL_PRE_INVOICE_ID_NUMBER_IDX).Text)
                Me.State.selectedBatchNumber = Grid.DataKeys(index).Values(0).ToString()
                Me.State.status = Me.Grid.Rows(index).Cells(Me.GRID_COL_STATUS_IDX).Text
                Me.State.createDate = Me.Grid.Rows(index).Cells(Me.GRID_COL_CREATED_DATE_IDX).Text
                Me.State.displayDate = Me.Grid.Rows(index).Cells(Me.GRID_COL_DISPLAY_DATE_IDX).Text
                Me.State.claims = Me.Grid.Rows(index).Cells(Me.GRID_COL_CLAIMS_COUNT_IDX).Text
                Me.State.totalBonusAmount = Me.Grid.Rows(index).Cells(GRID_COL_BONUS_AMOUNT_IDX).Text
                Me.State.Deductible = Me.Grid.Rows(index).Cells(GRID_COL_DEDUCTIBLE_IDX).Text
                Me.State.totalAmount = Me.Grid.Rows(index).Cells(GRID_COL_TOTAL_AMOUNT_IDX).Text
                If (moMultipleColumnDrop.CodeDropDown.Items.Count > 2) Then ' one item for empty row and one for actual company code
                    Me.State.selectedCompanycode = moMultipleColumnDrop.SelectedCode
                    Me.State.selectedCompanyDesc = moMultipleColumnDrop.SelectedDesc
                End If
                Me.callPage(PreinvoiceDetailForm.URL, New PreinvoiceDetailForm.Parameters(Me.State.selectedCompanycode, Me.State.selectedCompanyDesc, Me.State.selectedPreInvoiceID, Me.State.selectedBatchNumber, Me.State.status, Me.State.createDate, Me.State.displayDate, Me.State.claims, Me.State.totalBonusAmount, Me.State.totalAmount, Me.State.Deductible))
            End If

        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Grid_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowDataBound
        Try
            Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
            Dim btnEditItem As LinkButton
            If (e.Row.RowType = DataControlRowType.DataRow) _
                OrElse (e.Row.RowType = DataControlRowType.Separator) Then
                If (Not e.Row.Cells(Me.GRID_COL_PRE_INVOICE_ID_NUMBER_IDX).FindControl(GRID_COL_EDIT_BATCH_NUMBER_CTRL) Is Nothing) Then
                    Me.PopulateControlFromBOProperty(e.Row.Cells(Me.GRID_COL_PRE_INVOICE_ID_NUMBER_IDX), dvRow(PreInvoice.PreinvoiceSearchDV.COL_PRE_INVOICE_ID))

                    btnEditItem = CType(e.Row.Cells(Me.GRID_COL_BATCH_NUMBER_IDX).FindControl(GRID_COL_EDIT_BATCH_NUMBER_CTRL), LinkButton)
                    'btnEditItem.CommandArgument = GetGuidStringFromByteArray(CType(dvRow(PreInvoice.PreinvoiceSearchDV.COL_PRE_INVOICE_ID), Byte()))
                    btnEditItem.CommandName = SELECT_ACTION_COMMAND
                    btnEditItem.Text = dvRow(PreInvoice.PreinvoiceSearchDV.COL_BATCH_NUMBER).ToString
                    Me.State.selectedBatchNumber = btnEditItem.Text.Trim()
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
            Grid.PageIndex = NewCurrentPageIndex(Grid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
            Me.State.PageSize = CType(cboPageSize.SelectedValue, Integer)
            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub
#End Region

End Class