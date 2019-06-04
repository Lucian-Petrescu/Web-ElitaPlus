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

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Put user code to initialize the page here
        Me.ErrControllerMaster.Clear_Hide()

        Try
            If Not Me.IsPostBack Then

                Me.SetFormTitle(PAGETITLE)
                Me.SetFormTab(PAGETAB)
                Me.AddCalendar(Me.btnStartDate, Me.moTxtStartDate)
                Me.AddCalendar(Me.btnEndDate, Me.moTxtEndDate)

                'Fill Companies
                Dim dv As DataView = LookupListNew.GetUserCompaniesLookupList()
                Me.moUserCompanyMultipleDrop.NothingSelected = True
                Me.moUserCompanyMultipleDrop.SetControl(True, Me.moUserCompanyMultipleDrop.MODES.NEW_MODE, True, dv, Me.moUserCompanyMultipleDrop.NO_CAPTION, True)
                If dv.Count.Equals(1) Then
                    Me.moUserCompanyMultipleDrop.SelectedIndex = 1
                    Me.moUserCompanyMultipleDrop.ChangeEnabledControlProperty(False)
                End If


            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
        Me.ShowMissingTranslations(Me.ErrControllerMaster)
    End Sub


#End Region

#Region "CONTROLLING LOGIC"


    Private Sub btnClearSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClearSearch.Click

        Me.moTxtStartDate.Text = String.Empty
        Me.moTxtEndDate.Text = String.Empty
        Me.moTxtBatchNumber.Text = String.Empty

    End Sub


    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click

        'Check to be sure a company is selected

        If Me.moUserCompanyMultipleDrop.SelectedGuid.ToString = Me.NOTHING_SELECTED Then
            Me.ErrControllerMaster.AddErrorAndShow(ElitaPlus.Common.ErrorCodes.GUI_COMPANY_IS_REQUIRED)
            Exit Sub
        End If

        'Check if any search criteria submitted.  If not, return an error
        If Me.moTxtStartDate.Text = String.Empty AndAlso Me.moTxtEndDate.Text = String.Empty AndAlso Me.moTxtBatchNumber.Text = String.Empty Then
            Me.ErrControllerMaster.AddErrorAndShow(ElitaPlus.Common.ErrorCodes.GUI_SEARCH_FIELD_NOT_SUPPLIED_ERR)
            Exit Sub
        End If

        Dim dtStart, dtEnd As Date
        If Date.TryParse(Me.moTxtStartDate.Text, dtStart) AndAlso Date.TryParse(Me.moTxtEndDate.Text, dtEnd) Then
            If dtStart > dtEnd Then
                Me.ErrControllerMaster.AddErrorAndShow(ElitaPlus.Common.ErrorCodes.GUI_BEGIN_END_DATE_ERR)
                Exit Sub
            End If
        End If

        Populate()
        PopulateGrid()

        If Me.State.searchDV.Count = 0 Then
            Me.AddInfoMsg(Message.MSG_NO_RECORDS_FOUND)
        End If

    End Sub


    Private Sub btnBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Me.ReturnToTabHomePage()
    End Sub

#End Region

#Region "GRID RELATED"

    Private Sub Populate()

        Try
            If Me.moTxtBatchNumber.Text.Trim.Equals(String.Empty) Then
                Me.State.searchDV = AcctTransmission.GetFilesForReversal(Me.moUserCompanyMultipleDrop.SelectedGuid, If(Me.moTxtStartDate.Text.Trim.Equals(String.Empty), Date.MinValue, DateHelper.GetDateValue(Me.moTxtStartDate.Text.Trim)), If(Me.moTxtEndDate.Text.Trim.Equals(String.Empty), Date.MinValue, DateHelper.GetDateValue(Me.moTxtEndDate.Text.Trim)))
            Else
                Me.State.searchDV = AcctTransmission.GetFilesForReversal(Me.moUserCompanyMultipleDrop.SelectedGuid, Me.moTxtBatchNumber.Text.Trim)
            End If

        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try

    End Sub

    Private Sub PopulateGrid()

        Try

            If Me.State.searchDV Is Nothing Then Populate()

            If Me.State.searchDV.Count > 0 Then
                Me.moDataGrid.Visible = True
                Me.moDataGrid.PageSize = Me.State.selectedPageSize
                Me.moDataGrid.DataSource = Me.State.searchDV
                Me.moDataGrid.DataBind()
                Me.trPageSize.Attributes("style") = ""
            Else
                Me.moDataGrid.Visible = False
                Me.trPageSize.Attributes("style") = "display:none;"
            End If

            Me.lblRecordCount.Text = Me.State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)

        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try

    End Sub

    Private Sub moDataGrid_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles moDataGrid.ItemCommand

        Try
            If e.CommandName = Me.EDIT_COMMAND_NAME Then
                moDataGrid.SelectedIndex = e.Item.ItemIndex
                Navigator.callPage(Me, AccountingReversalForm.URL, Me.State, New Guid(moDataGrid.SelectedItem.Cells(GRD_COL_ID_INX).Text))
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try

    End Sub

    Private Sub moDataGrid_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles moDataGrid.ItemCreated
        BaseItemCreated(sender, e)
    End Sub

    Private Sub moDataGrid_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles moDataGrid.ItemDataBound
        If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then
            Dim drv As DataRowView = CType(e.Item.DataItem, DataRowView)
            e.Item.Cells(Me.GRD_COL_ID_INX).Text = GuidControl.ByteArrayToGuid(CType(drv(AcctTransmission.AcctTransmissionSearchDV.COL_ACCT_TRANSMISSION_ID), Byte())).ToString
        End If
    End Sub

    Private Sub Grid_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles moDataGrid.PageIndexChanged
        Try
            Me.moDataGrid.CurrentPageIndex = e.NewPageIndex
            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub

    Protected Sub cboPageSize_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
        Try
            Me.State.selectedPageSize = CType(cboPageSize.SelectedValue, Integer)
            Me.moDataGrid.CurrentPageIndex = 0
            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub


    Private Sub moDataGrid_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles moDataGrid.SortCommand

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

            Me.moDataGrid.CurrentPageIndex = 0

            Me.PopulateGrid()

        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub

#End Region


   
End Class