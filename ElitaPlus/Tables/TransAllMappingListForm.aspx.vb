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


    Private Sub Page_PageReturn(ByVal ReturnFromUrl As String, ByVal ReturnPar As Object) Handles MyBase.PageReturn
        Try
            Me.MenuEnabled = True
            Me.IsReturningFromChild = True
            Dim retObj As TransAllMappingForm.ReturnType = CType(ReturnPar, TransAllMappingForm.ReturnType)

            Me.State.HasDataChanged = retObj.HasDataChanged
            Select Case retObj.LastOperation
                Case ElitaPlusPage.DetailPageCommand.Back
                    If Not retObj Is Nothing Then
                        Me.State.TransAllMappingGUID = retObj.EditingBo.Id
                        Me.State.IsGridVisible = True
                    End If
                Case ElitaPlusPage.DetailPageCommand.Delete
                    Me.AddInfoMsg(Message.DELETE_RECORD_CONFIRMATION)
            End Select
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub

#End Region
#End Region

#Region "Page Events"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'Put user code to initialize the page here
        Me.ErrControllerMaster.Clear_Hide()

        Try

            Me.DisplayProgressBarOnClick(Me.btnSearch, ElitaPlusWebApp.Message.MSG_PERFORMING_REQUEST)

            If Not Me.IsPostBack Then
                Me.SetFormTitle(PAGETITLE)
                Me.SetFormTab(PAGETAB)
                PopulateDealer()
                Me.TranslateGridHeader(Me.grdResults)

                If Me.State.IsGridVisible Then
                    If Not (Me.State.selectedPageSize = DEFAULT_PAGE_SIZE) Then
                        cboPageSize.SelectedValue = CType(Me.State.selectedPageSize, String)
                        grdResults.PageSize = Me.State.selectedPageSize
                    End If
                    Me.PopulateGrid()
                End If
            End If
            If Me.IsReturningFromChild = True Then
                Me.IsReturningFromChild = False
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try

    End Sub


    Private Sub TransAllMappingListForm_PageReturn(ByVal ReturnFromUrl As String, ByVal ReturnPar As Object) Handles Me.PageReturn

    End Sub


#End Region
   

#Region "POPULATE"

    Private Sub PopulateGrid()

        Try

            Me.State.SelectedDealerId = ddlDealer.SelectedGuid
            If ((Me.State.searchDV Is Nothing) OrElse (Me.State.HasDataChanged)) Then
                Me.State.searchDV = TransallMapping.GetList(ddlDealer.SelectedGuid, ElitaPlusIdentity.Current.ActiveUser.Companies)
            End If

            grdResults.AutoGenerateColumns = False
            SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.TransAllMappingGUID, Me.grdResults, Me.State.PageIndex)
            Me.SortAndBindGrid()

        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try

    End Sub

    Private Sub SortAndBindGrid()
        Me.State.PageIndex = Me.grdResults.PageIndex
        grdResults.DataSource = Me.State.searchDV
        grdResults.DataBind()

        ControlMgr.SetVisibleControl(Me, grdResults, Me.State.IsGridVisible)

        ControlMgr.SetVisibleControl(Me, trPageSize, Me.grdResults.Visible)

        Session("recCount") = Me.State.searchDV.Count

        If Me.State.searchDV.Count > 0 Then

            If Me.grdResults.Visible Then
                Me.lblRecordCount.Text = Me.State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
            End If
        Else
            If Me.grdResults.Visible Then
                Me.lblRecordCount.Text = Me.State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
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
            ddlDealer.SelectedGuid = Me.State.SelectedDealerId
        Catch ex As Exception
            ErrControllerMaster.AddError(ex.Message, False)
            ErrControllerMaster.Show()
        End Try
    End Sub

#End Region

#Region "EVENTS"


    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click

        Try
            Me.State.PageIndex = 0
            Me.State.TransAllMappingGUID = Guid.Empty
            Me.State.IsGridVisible = True
            Me.State.searchDV = Nothing
            Me.State.HasDataChanged = False
            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try

    End Sub

    Private Sub btnClearSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClearSearch.Click

        Me.ddlDealer.SelectedIndex = -1
        Me.State.PageIndex = 0
        Me.State.TransAllMappingGUID = Guid.Empty
        Me.State.searchDV = Nothing
        Me.lblRecordCount.Text = "0"
        Me.grdResults.DataSource = Nothing
        Me.grdResults.DataBind()

    End Sub

    Private Sub btnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Try
            Me.callPage(TransAllMappingForm.URL)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub

#End Region

#Region "GRIDVIEW RELATED"

    Public Property SortDirection() As String
        Get
            Return Me.State.SortDirection
        End Get
        Set(ByVal value As String)
            Me.State.SortDirection = value
        End Set
    End Property

    'The Binding Logic is here
    Private Sub Grid_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdResults.RowDataBound
        Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
        Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
        If Not dvRow Is Nothing Then
            If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then
                e.Row.Cells(Me.GRD_IDX_ID_COL).Text = GetGuidStringFromByteArray(CType(dvRow(DALObjects.TransAllMappingDAL.COL_NAME_TRANSALL_MAPPING_ID), Byte()))
                e.Row.Cells(Me.GRD_IDX_DEALER_COL).Text = dvRow(DALObjects.TransAllMappingDAL.COL_NAME_DEALER_NAME).ToString
                e.Row.Cells(Me.GRD_IDX_PACKAGE_COL).Text = dvRow(DALObjects.TransAllMappingDAL.COL_NAME_TRANSALL_PACKAGE).ToString
            End If
        End If
    End Sub

    Public Sub ItemCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdResults.RowCommand
        Try
            If e.CommandName = "SelectAction" Then
                Dim index As Integer = CInt(e.CommandArgument)
                Me.State.TransAllMappingGUID = New Guid(Me.grdResults.Rows(index).Cells(Me.GRD_IDX_ID_COL).Text)
                Me.callPage(TransAllMappingForm.URL, Me.State.TransAllMappingGUID)
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try

    End Sub

    Public Sub ItemCreated(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
        BaseItemCreated(sender, e)
    End Sub

    Private Sub Grid_PageSizeChanged(ByVal source As Object, ByVal e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
        Try
            grdResults.PageIndex = NewCurrentPageIndex(grdResults, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub
    Private Sub Grid_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdResults.Sorting
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
            Me.State.SortDirection = Me.SortDirection
            Me.State.PageIndex = 0
            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub


    Private Sub Grid_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdResults.PageIndexChanging
        Try
            Me.State.PageIndex = e.NewPageIndex
            Me.State.TransAllMappingGUID = Guid.Empty
            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub

#End Region



  
   

End Class