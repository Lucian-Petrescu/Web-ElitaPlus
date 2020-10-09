Option Strict On
Option Explicit On
Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common

Public Class PremiumAdjustmentSettingsListForm
    Inherits ElitaPlusSearchPage

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(sender As System.Object, e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Constants"
    Public Const GRID_COL_EDIT_IDX As Integer = 0
    Public Const GRID_COL_DEALER_CODE_IDX As Integer = 1
    Public Const GRID_COL_ADJUSTMENT_BY_IDX As Integer = 2
    Public Const GRID_COL_EFFECTIVE_DATE_IDX As Integer = 3
    Public Const GRID_COL_PREMIUM_ADJUSTMENT_SETTING_ID_IDX As Integer = 4

    Public Const GRID_TOTAL_COLUMNS As Integer = 5

    Private Const LABEL_SELECT_DEALERCODE As String = "DEALER"

#End Region

#Region "Properties"

    Public ReadOnly Property DealerMultipleDrop() As MultipleColumnDDLabelControl
        Get
            If moDealerMultipleDrop Is Nothing Then
                moDealerMultipleDrop = CType(FindControl("multipleDropControl"), MultipleColumnDDLabelControl)
            End If
            Return moDealerMultipleDrop
        End Get
    End Property


#End Region

#Region "Page State"
    Private IsReturningFromChild As Boolean = False

    ' This class keeps the current state for the search page.
    Class MyState
        Public PageIndex As Integer = 0
        Public PageSize As Integer = DEFAULT_PAGE_SIZE
        Public DealerId As Guid
        Public PremiumAdjustmentSettingId As Guid = Guid.Empty
        Public IsGridVisible As Boolean = False
        Public searchBtnClicked As Boolean = False
        Public searchDV As PremiumAdjustmentSettings.PremiumAdjustnmentSettingSearchDV = Nothing
        Public bnoRow As Boolean = False
        'Public SortExpression As String = PremiumAdjustmentSettings.PremiumAdjustnmentSettingSearchDV.COL_DESCRIPTION

        ' these variables are used to store the sorting columns information.
        Public SortColumns(GRID_TOTAL_COLUMNS - 1) As String
        Public IsSortDesc(GRID_TOTAL_COLUMNS - 1) As Boolean
        Sub New()
            SortColumns(GRID_COL_DEALER_CODE_IDX) = PremiumAdjustmentSettings.PremiumAdjustnmentSettingSearchDV.COL_DEALER_CODE
            IsSortDesc(GRID_COL_DEALER_CODE_IDX) = False
        End Sub

        ' this will be called before the populate list to get the correct sort order
        Public ReadOnly Property CurrentSortExpresion1() As String
            Get
                Dim s As String
                Dim i As Integer
                Dim sortExp As String = ""
                For i = 0 To SortColumns.Length - 1
                    If SortColumns(i) IsNot Nothing Then
                        sortExp &= SortColumns(i)
                        If IsSortDesc(i) Then sortExp &= " DESC"
                        sortExp &= ","
                    End If
                Next
                Return sortExp.Substring(0, sortExp.Length - 1) 'to remove the last comma
            End Get
        End Property

        Public Sub ToggleSort1(gridColIndex As Integer)
            IsSortDesc(gridColIndex) = Not IsSortDesc(gridColIndex)
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

    Private Sub Page_PageReturn(ReturnFromUrl As String, ReturnPar As Object) Handles MyBase.PageReturn
        Try
            MenuEnabled = True
            IsReturningFromChild = True
            If State.searchDV Is Nothing Then
                State.IsGridVisible = False
            Else
                State.IsGridVisible = True
            End If
            Dim retObj As PremiumAdjustmentSettingsForm.ReturnType = CType(ReturnPar, PremiumAdjustmentSettingsForm.ReturnType)
            If retObj IsNot Nothing AndAlso retObj.BoChanged Then
                State.searchDV = Nothing
            End If
            Select Case retObj.LastOperation
                Case ElitaPlusPage.DetailPageCommand.Back
                    If retObj IsNot Nothing Then
                        If Not retObj.EditingBo.IsNew Then
                            State.PremiumAdjustmentSettingId = retObj.EditingBo.Id
                        End If
                        'Me.State.IsGridVisible = True
                    End If
                Case ElitaPlusPage.DetailPageCommand.Delete
                    AddInfoMsg(Message.DELETE_RECORD_CONFIRMATION)
            End Select
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try

    End Sub

#End Region

#Region "Page_Events"
    Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        ErrorCtrl.Clear_Hide()
        SetStateProperties()
        Try
            If Not IsPostBack Then
                SortDirection = PremiumAdjustmentSettings.PremiumAdjustnmentSettingSearchDV.COL_DEALER_CODE
                ControlMgr.SetVisibleControl(Me, trPageSize, False)
                PopulateDropdown()
                SetGridItemStyleColor(Grid)
                TranslateGridHeader(Grid)
                TranslateGridControls(Grid)
                If State.IsGridVisible Then
                    If Not (State.PageSize = DEFAULT_PAGE_SIZE) Then
                        cboPageSize.SelectedValue = CType(State.PageSize, String)
                        Grid.PageSize = State.PageSize
                    End If
                    PopulateGrid()
                End If

            End If
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
        ShowMissingTranslations(ErrorCtrl)
    End Sub
#End Region

#Region "Controlling Logic"

    Public Sub PopulateGrid()
        If (State.searchDV Is Nothing) Then
            State.searchDV = PremiumAdjustmentSettings.getList(State.DealerId)
        End If
        State.searchDV.Sort = SortDirection
        Grid.AutoGenerateColumns = False
        SetPageAndSelectedIndexFromGuid(State.searchDV, State.PremiumAdjustmentSettingId, Grid, State.PageIndex)
        SortAndBindGrid()

    End Sub

    Private Sub SortAndBindGrid()
        State.PageIndex = Grid.PageIndex
        If (State.searchDV.Count = 0) Then

            State.bnoRow = True
            CreateHeaderForEmptyGrid(Grid, SortDirection)
        Else
            State.bnoRow = False
            Grid.Enabled = True
            Grid.DataSource = State.searchDV
            HighLightSortColumn(Grid, SortDirection)
            Grid.DataBind()
        End If
        If Not Grid.BottomPagerRow.Visible Then Grid.BottomPagerRow.Visible = True



        ControlMgr.SetVisibleControl(Me, Grid, State.IsGridVisible)

        ControlMgr.SetVisibleControl(Me, trPageSize, Grid.Visible)

        Session("recCount") = State.searchDV.Count

        If Grid.Visible Then
            lblRecordCount.Text = State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
        End If

    End Sub

    Private Sub PopulateDropdown()



        DealerMultipleDrop.Caption = TranslationBase.TranslateLabelOrMessage(LABEL_SELECT_DEALERCODE)
        DealerMultipleDrop.NothingSelected = True

        DealerMultipleDrop.BindData(LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies))
        DealerMultipleDrop.AutoPostBackDD = True


    End Sub

    Private Sub moDealerMultipleDrop_SelectedDropChanged(aSrc As Common.MultipleColumnDDLabelControl) Handles moDealerMultipleDrop.SelectedDropChanged
        Try
            State.DealerId = DealerMultipleDrop.SelectedGuid()
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub


#End Region


#Region " Datagrid Related "

    Public Property SortDirection() As String
        Get
            Return ViewState("SortDirection").ToString
        End Get
        Set(value As String)
            ViewState("SortDirection") = value
        End Set
    End Property
    'The Binding Logic is here
    Private Sub Grid_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowDataBound
        Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
        Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
        If dvRow IsNot Nothing AndAlso Not State.bnoRow Then
            If itemType = ListItemType.Item OrElse itemType = ListItemType.AlternatingItem OrElse itemType = ListItemType.SelectedItem Then
                e.Row.Cells(GRID_COL_DEALER_CODE_IDX).Text = dvRow(PremiumAdjustmentSettings.PremiumAdjustnmentSettingSearchDV.COL_DEALER_CODE).ToString
                e.Row.Cells(GRID_COL_ADJUSTMENT_BY_IDX).Text = LookupListNew.GetDescriptionFromId("FIN_ADJ_BY", New Guid(GetGuidStringFromByteArray(CType(dvRow(PremiumAdjustmentSettings.PremiumAdjustnmentSettingSearchDV.COL_ADJUSTMENT_BY), Byte()))))
                e.Row.Cells(GRID_COL_EFFECTIVE_DATE_IDX).Text = dvRow(PremiumAdjustmentSettings.PremiumAdjustnmentSettingSearchDV.COL_EFFECTIVE_DATE).ToString
                e.Row.Cells(GRID_COL_PREMIUM_ADJUSTMENT_SETTING_ID_IDX).Text = GetGuidStringFromByteArray(CType(dvRow(PremiumAdjustmentSettings.PremiumAdjustnmentSettingSearchDV.COL_PREMIUM_ADJUSTMENT_SETTING_ID), Byte()))
            End If
        End If
    End Sub

    Private Sub Grid_SortCommand(source As Object, e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles Grid.Sorting
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
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

    Public Sub ItemCommand(source As System.Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs)
        Try
            If e.CommandName = "SelectAction" Then
                Dim index As Integer = CInt(e.CommandArgument)
                State.PremiumAdjustmentSettingId = New Guid(Grid.Rows(index).Cells(GRID_COL_PREMIUM_ADJUSTMENT_SETTING_ID_IDX).Text)
                callPage(PremiumAdjustmentSettingsForm.URL, State.PremiumAdjustmentSettingId)
            ElseIf e.CommandName = "Sort" Then
                Grid.DataMember = e.CommandArgument.ToString
                PopulateGrid()
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try

    End Sub
    Public Sub ItemCreated(sender As System.Object, e As System.Web.UI.WebControls.GridViewRowEventArgs)
        BaseItemCreated(sender, e)
    End Sub



    Private Sub Grid_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Grid.PageIndexChanging
        Try
            State.PageIndex = e.NewPageIndex
            State.PremiumAdjustmentSettingId = Guid.Empty
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

    Private Sub Grid_PageSizeChanged(source As Object, e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
        Try
            Grid.PageIndex = NewCurrentPageIndex(Grid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
            State.PageSize = Grid.PageSize
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub
#End Region

#Region " Buttons Clicks "
    Private Sub SetStateProperties()

        State.DealerId = DealerMultipleDrop.SelectedGuid()

    End Sub

    Private Sub btnSearch_Click(sender As Object, e As System.EventArgs) Handles btnSearch.Click
        Try
            State.PageIndex = 0
            State.PremiumAdjustmentSettingId = Guid.Empty
            State.IsGridVisible = True
            State.searchDV = Nothing
            State.searchBtnClicked = True
            PopulateGrid()
            State.searchBtnClicked = False
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

    Private Sub btnAdd_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnAdd_WRITE.Click
        Try
            callPage(PremiumAdjustmentSettingsForm.URL)
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

    Private Sub btnClearSearch_Click1(sender As Object, e As System.EventArgs) Handles btnClearSearch.Click
        Try
            ClearSearchCriteria()
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub
    Private Sub ClearSearchCriteria()

        Try
            DealerMultipleDrop.SelectedIndex = BLANK_ITEM_SELECTED
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try

    End Sub

#End Region

#Region "Error Handling"


#End Region


End Class