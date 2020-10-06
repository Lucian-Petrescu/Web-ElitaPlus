Public Partial Class TransAllMappingListForm
    Inherits ElitaPlusSearchPage

#Region "CONSTANTS"
    Public Const URL As String = "~/Tables/TransAllMappingListForm.aspx"
    Public Const PAGETITLE As String = "TransAll_Mapping"
    Public Const PAGETAB As String = "ADMIN"

    Private Const GRD_IDX_ID_COL As Integer = 0
    Private Const GRD_IDX_BUTTON_COL As Integer = 1
    Private Const GRD_IDX_DEALER_COL As Integer = 2
    Private Const GRD_IDX_PACKAGE_COL As Integer = 3

    Private Const NOTHING_SELECTED_GUID As String = "00000000-0000-0000-0000-000000000000"
    Private Const COL_NAME_DEALER_NAME As String = "dealer_name"
#End Region


#Region "PAGE STATE"
    Private IsReturningFromChild As Boolean = False

    Class MyState
        Public PageIndex As Integer = 0
        Public TransAllMappingGUID As Guid = Guid.Empty
        Public searchDV As DataView
        Public SortDirection As String
        Public SelectedDealerId As Guid = Guid.Empty
        Public IsGridVisible As Boolean = False
        Public selectedPageSize As Integer = DEFAULT_PAGE_SIZE
        Public SortExpression As String = COL_NAME_DEALER_NAME
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


#Region "Page Return"


    Private Sub Page_PageReturn(ReturnFromUrl As String, ReturnPar As Object) Handles MyBase.PageReturn
        Try
            MenuEnabled = True
            IsReturningFromChild = True
            Dim retObj As TransAllMappingForm.ReturnType = CType(ReturnPar, TransAllMappingForm.ReturnType)

            State.HasDataChanged = retObj.HasDataChanged
            Select Case retObj.LastOperation
                Case ElitaPlusPage.DetailPageCommand.Back
                    If retObj IsNot Nothing Then
                        State.TransAllMappingGUID = retObj.EditingBo.Id
                        State.IsGridVisible = True
                    End If
                Case ElitaPlusPage.DetailPageCommand.Delete
                    AddInfoMsg(Message.DELETE_RECORD_CONFIRMATION)
            End Select
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub

#End Region
#End Region

#Region "Page Events"

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        'Put user code to initialize the page here
        ErrControllerMaster.Clear_Hide()

        Try

            DisplayProgressBarOnClick(btnSearch, ElitaPlusWebApp.Message.MSG_PERFORMING_REQUEST)

            If Not IsPostBack Then
                SetFormTitle(PAGETITLE)
                SetFormTab(PAGETAB)
                PopulateDealer()
                TranslateGridHeader(grdResults)

                If State.IsGridVisible Then
                    If Not (State.selectedPageSize = DEFAULT_PAGE_SIZE) Then
                        cboPageSize.SelectedValue = CType(State.selectedPageSize, String)
                        grdResults.PageSize = State.selectedPageSize
                    End If
                    PopulateGrid()
                End If
            End If
            If IsReturningFromChild = True Then
                IsReturningFromChild = False
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try

    End Sub


    Private Sub TransAllMappingListForm_PageReturn(ReturnFromUrl As String, ReturnPar As Object) Handles Me.PageReturn

    End Sub


#End Region
   

#Region "POPULATE"

    Private Sub PopulateGrid()

        Try

            State.SelectedDealerId = ddlDealer.SelectedGuid
            If ((State.searchDV Is Nothing) OrElse (State.HasDataChanged)) Then
                State.searchDV = TransallMapping.GetList(ddlDealer.SelectedGuid, ElitaPlusIdentity.Current.ActiveUser.Companies)
            End If

            grdResults.AutoGenerateColumns = False
            SetPageAndSelectedIndexFromGuid(State.searchDV, State.TransAllMappingGUID, grdResults, State.PageIndex)
            SortAndBindGrid()

        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try

    End Sub

    Private Sub SortAndBindGrid()
        State.PageIndex = grdResults.PageIndex
        grdResults.DataSource = State.searchDV
        grdResults.DataBind()

        ControlMgr.SetVisibleControl(Me, grdResults, State.IsGridVisible)

        ControlMgr.SetVisibleControl(Me, trPageSize, grdResults.Visible)

        Session("recCount") = State.searchDV.Count

        If State.searchDV.Count > 0 Then

            If grdResults.Visible Then
                lblRecordCount.Text = State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
            End If
        Else
            If grdResults.Visible Then
                lblRecordCount.Text = State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
            End If
        End If
    End Sub
    Private Sub PopulateDealer()
        Try
            Dim oDealerview As DataView = LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies)
            ddlDealer.SetControl(False, _
                                        ddlDealer.MODES.NEW_MODE, _
                                        True, _
                                        oDealerview, _
                                        "* " + TranslationBase.TranslateLabelOrMessage("DEALER"), _
                                        True, True, _
                                        , _
                                        "multipleDropControl_moMultipleColumnDrop", _
                                        "multipleDropControl_moMultipleColumnDropDesc", _
                                        "multipleDropControl_lb_DropDown", _
                                        False, _
                                        0)
            ddlDealer.SelectedIndex = ElitaPlusSearchPage.SELECTED_GUID_COL
            ddlDealer.SelectedGuid = State.SelectedDealerId
        Catch ex As Exception
            ErrControllerMaster.AddError(ex.Message, False)
            ErrControllerMaster.Show()
        End Try
    End Sub

#End Region

#Region "EVENTS"


    Private Sub btnSearch_Click(sender As Object, e As System.EventArgs) Handles btnSearch.Click

        Try
            State.PageIndex = 0
            State.TransAllMappingGUID = Guid.Empty
            State.IsGridVisible = True
            State.searchDV = Nothing
            State.HasDataChanged = False
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try

    End Sub

    Private Sub btnClearSearch_Click(sender As Object, e As System.EventArgs) Handles btnClearSearch.Click

        ddlDealer.SelectedIndex = -1
        State.PageIndex = 0
        State.TransAllMappingGUID = Guid.Empty
        State.searchDV = Nothing
        lblRecordCount.Text = "0"
        grdResults.DataSource = Nothing
        grdResults.DataBind()

    End Sub

    Private Sub btnAdd_Click(sender As Object, e As System.EventArgs) Handles btnAdd.Click
        Try
            callPage(TransAllMappingForm.URL)
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub

#End Region

#Region "GRIDVIEW RELATED"

    Public Property SortDirection() As String
        Get
            Return State.SortDirection
        End Get
        Set(value As String)
            State.SortDirection = value
        End Set
    End Property

    'The Binding Logic is here
    Private Sub Grid_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdResults.RowDataBound
        Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
        Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
        If dvRow IsNot Nothing Then
            If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then
                e.Row.Cells(GRD_IDX_ID_COL).Text = GetGuidStringFromByteArray(CType(dvRow(DALObjects.TransAllMappingDAL.COL_NAME_TRANSALL_MAPPING_ID), Byte()))
                e.Row.Cells(GRD_IDX_DEALER_COL).Text = dvRow(DALObjects.TransAllMappingDAL.COL_NAME_DEALER_NAME).ToString
                e.Row.Cells(GRD_IDX_PACKAGE_COL).Text = dvRow(DALObjects.TransAllMappingDAL.COL_NAME_TRANSALL_PACKAGE).ToString
            End If
        End If
    End Sub

    Public Sub ItemCommand(source As System.Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdResults.RowCommand
        Try
            If e.CommandName = "SelectAction" Then
                Dim index As Integer = CInt(e.CommandArgument)
                State.TransAllMappingGUID = New Guid(grdResults.Rows(index).Cells(GRD_IDX_ID_COL).Text)
                callPage(TransAllMappingForm.URL, State.TransAllMappingGUID)
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try

    End Sub

    Public Sub ItemCreated(sender As System.Object, e As System.Web.UI.WebControls.GridViewRowEventArgs)
        BaseItemCreated(sender, e)
    End Sub

    Private Sub Grid_PageSizeChanged(source As Object, e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
        Try
            grdResults.PageIndex = NewCurrentPageIndex(grdResults, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub
    Private Sub Grid_SortCommand(source As Object, e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdResults.Sorting
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
            State.SortDirection = SortDirection
            State.PageIndex = 0
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub


    Private Sub Grid_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdResults.PageIndexChanging
        Try
            State.PageIndex = e.NewPageIndex
            State.TransAllMappingGUID = Guid.Empty
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub

#End Region



  
   

End Class