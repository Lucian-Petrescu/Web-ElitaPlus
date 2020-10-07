Public Partial Class AccountingReversalListForm
    Inherits ElitaPlusSearchPage

#Region "CONSTANTS"

    Public Const URL As String = "Interfaces/AccountingReversalListForm.aspx"
    Public Const PAGETITLE As String = "ACCOUNTING_REVERSAL"
    Public Const PAGETAB As String = "INTERFACES"

    Private Const NOTHING_SELECTED As String = "00000000-0000-0000-0000-000000000000"
    Private Const GRD_COL_ID_INX As Integer = 0


#End Region
#Region "Page State"
    Private IsReturningFromChild As Boolean = False

    ' This class keeps the current state for the search page.
    Class MyState
        Public MyBO As AcctEvent
        Public AccountingCompanyId As Guid = Guid.Empty
        Public EventName As String
        Public IsGridVisible As Boolean
        Private mnPageIndex As Integer = 0
        Private msPageSort As String
        Private mnPageSize As Integer = DEFAULT_PAGE_SIZE
        Public searchDV As AcctTransmission.AcctTransmissionSearchDV = Nothing
        Public selectedPageSize As Integer = DEFAULT_PAGE_SIZE
        Public SortExpression As String = AcctEvent.AcctEventSearchDV.COL_EVENT_NAME
        Public HasDataChanged As Boolean

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

#Region "PAGE INITIALIZATION"

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        'Put user code to initialize the page here
        ErrControllerMaster.Clear_Hide()

        Try
            If Not IsPostBack Then

                SetFormTitle(PAGETITLE)
                SetFormTab(PAGETAB)
                AddCalendar(btnStartDate, moTxtStartDate)
                AddCalendar(btnEndDate, moTxtEndDate)

                'Fill Companies
                Dim dv As DataView = LookupListNew.GetUserCompaniesLookupList()
                moUserCompanyMultipleDrop.NothingSelected = True
                moUserCompanyMultipleDrop.SetControl(True, moUserCompanyMultipleDrop.MODES.NEW_MODE, True, dv, moUserCompanyMultipleDrop.NO_CAPTION, True)
                If dv.Count.Equals(1) Then
                    moUserCompanyMultipleDrop.SelectedIndex = 1
                    moUserCompanyMultipleDrop.ChangeEnabledControlProperty(False)
                End If


            End If
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
        ShowMissingTranslations(ErrControllerMaster)
    End Sub


#End Region

#Region "CONTROLLING LOGIC"


    Private Sub btnClearSearch_Click(sender As Object, e As System.EventArgs) Handles btnClearSearch.Click

        moTxtStartDate.Text = String.Empty
        moTxtEndDate.Text = String.Empty
        moTxtBatchNumber.Text = String.Empty

    End Sub


    Private Sub btnSearch_Click(sender As Object, e As System.EventArgs) Handles btnSearch.Click

        'Check to be sure a company is selected

        If moUserCompanyMultipleDrop.SelectedGuid.ToString = NOTHING_SELECTED Then
            ErrControllerMaster.AddErrorAndShow(ElitaPlus.Common.ErrorCodes.GUI_COMPANY_IS_REQUIRED)
            Exit Sub
        End If

        'Check if any search criteria submitted.  If not, return an error
        If moTxtStartDate.Text = String.Empty AndAlso moTxtEndDate.Text = String.Empty AndAlso moTxtBatchNumber.Text = String.Empty Then
            ErrControllerMaster.AddErrorAndShow(ElitaPlus.Common.ErrorCodes.GUI_SEARCH_FIELD_NOT_SUPPLIED_ERR)
            Exit Sub
        End If

        Dim dtStart, dtEnd As Date
        If Date.TryParse(moTxtStartDate.Text, dtStart) AndAlso Date.TryParse(moTxtEndDate.Text, dtEnd) Then
            If dtStart > dtEnd Then
                ErrControllerMaster.AddErrorAndShow(ElitaPlus.Common.ErrorCodes.GUI_BEGIN_END_DATE_ERR)
                Exit Sub
            End If
        End If

        Populate()
        PopulateGrid()

        If State.searchDV.Count = 0 Then
            AddInfoMsg(Message.MSG_NO_RECORDS_FOUND)
        End If

    End Sub


    Private Sub btnBack_Click(sender As Object, e As System.EventArgs) Handles btnBack.Click
        ReturnToTabHomePage()
    End Sub

#End Region

#Region "GRID RELATED"

    Private Sub Populate()

        Try
            If moTxtBatchNumber.Text.Trim.Equals(String.Empty) Then
                State.searchDV = AcctTransmission.GetFilesForReversal(moUserCompanyMultipleDrop.SelectedGuid, If(moTxtStartDate.Text.Trim.Equals(String.Empty), Date.MinValue, DateHelper.GetDateValue(moTxtStartDate.Text.Trim)), If(moTxtEndDate.Text.Trim.Equals(String.Empty), Date.MinValue, DateHelper.GetDateValue(moTxtEndDate.Text.Trim)))
            Else
                State.searchDV = AcctTransmission.GetFilesForReversal(moUserCompanyMultipleDrop.SelectedGuid, moTxtBatchNumber.Text.Trim)
            End If

        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try

    End Sub

    Private Sub PopulateGrid()

        Try

            If State.searchDV Is Nothing Then Populate()

            If State.searchDV.Count > 0 Then
                moDataGrid.Visible = True
                moDataGrid.PageSize = State.selectedPageSize
                moDataGrid.DataSource = State.searchDV
                moDataGrid.DataBind()
                trPageSize.Attributes("style") = ""
            Else
                moDataGrid.Visible = False
                trPageSize.Attributes("style") = "display:none;"
            End If

            lblRecordCount.Text = State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)

        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try

    End Sub

    Private Sub moDataGrid_ItemCommand(source As Object, e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles moDataGrid.ItemCommand

        Try
            If e.CommandName = EDIT_COMMAND_NAME Then
                moDataGrid.SelectedIndex = e.Item.ItemIndex
                Navigator.callPage(Me, AccountingReversalForm.URL, State, New Guid(moDataGrid.SelectedItem.Cells(GRD_COL_ID_INX).Text))
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try

    End Sub

    Private Sub moDataGrid_ItemCreated(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles moDataGrid.ItemCreated
        BaseItemCreated(sender, e)
    End Sub

    Private Sub moDataGrid_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles moDataGrid.ItemDataBound
        If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then
            Dim drv As DataRowView = CType(e.Item.DataItem, DataRowView)
            e.Item.Cells(GRD_COL_ID_INX).Text = GuidControl.ByteArrayToGuid(CType(drv(AcctTransmission.AcctTransmissionSearchDV.COL_ACCT_TRANSMISSION_ID), Byte())).ToString
        End If
    End Sub

    Private Sub Grid_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles moDataGrid.PageIndexChanged
        Try
            moDataGrid.CurrentPageIndex = e.NewPageIndex
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub

    Protected Sub cboPageSize_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
        Try
            State.selectedPageSize = CType(cboPageSize.SelectedValue, Integer)
            moDataGrid.CurrentPageIndex = 0
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub


    Private Sub moDataGrid_SortCommand(source As Object, e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles moDataGrid.SortCommand

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

            moDataGrid.CurrentPageIndex = 0

            PopulateGrid()

        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub

#End Region


   
End Class