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

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
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
                For i = 0 To Me.SortColumns.Length - 1
                    If Not Me.SortColumns(i) Is Nothing Then
                        sortExp &= Me.SortColumns(i)
                        If Me.IsSortDesc(i) Then sortExp &= " DESC"
                        sortExp &= ","
                    End If
                Next
                Return sortExp.Substring(0, sortExp.Length - 1) 'to remove the last comma
            End Get
        End Property

        Public Sub ToggleSort1(ByVal gridColIndex As Integer)
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

    Private Sub Page_PageReturn(ByVal ReturnFromUrl As String, ByVal ReturnPar As Object) Handles MyBase.PageReturn
        Try
            Me.MenuEnabled = True
            Me.IsReturningFromChild = True
            If Me.State.searchDV Is Nothing Then
                Me.State.IsGridVisible = False
            Else
                Me.State.IsGridVisible = True
            End If
            Dim retObj As PremiumAdjustmentSettingsForm.ReturnType = CType(ReturnPar, PremiumAdjustmentSettingsForm.ReturnType)
            If Not retObj Is Nothing AndAlso retObj.BoChanged Then
                Me.State.searchDV = Nothing
            End If
            Select Case retObj.LastOperation
                Case ElitaPlusPage.DetailPageCommand.Back
                    If Not retObj Is Nothing Then
                        If Not retObj.EditingBo.IsNew Then
                            Me.State.PremiumAdjustmentSettingId = retObj.EditingBo.Id
                        End If
                        'Me.State.IsGridVisible = True
                    End If
                Case ElitaPlusPage.DetailPageCommand.Delete
                    Me.AddInfoMsg(Message.DELETE_RECORD_CONFIRMATION)
            End Select
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try

    End Sub

#End Region

#Region "Page_Events"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Me.ErrorCtrl.Clear_Hide()
        Me.SetStateProperties()
        Try
            If Not Me.IsPostBack Then
                Me.SortDirection = PremiumAdjustmentSettings.PremiumAdjustnmentSettingSearchDV.COL_DEALER_CODE
                ControlMgr.SetVisibleControl(Me, trPageSize, False)
                PopulateDropdown()
                Me.SetGridItemStyleColor(Grid)
                Me.TranslateGridHeader(Me.Grid)
                Me.TranslateGridControls(Me.Grid)
                If Me.State.IsGridVisible Then
                    If Not (Me.State.PageSize = DEFAULT_PAGE_SIZE) Then
                        cboPageSize.SelectedValue = CType(Me.State.PageSize, String)
                        Grid.PageSize = Me.State.PageSize
                    End If
                    Me.PopulateGrid()
                End If

            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
        Me.ShowMissingTranslations(Me.ErrorCtrl)
    End Sub
#End Region

#Region "Controlling Logic"

    Public Sub PopulateGrid()
        If (Me.State.searchDV Is Nothing) Then
            Me.State.searchDV = PremiumAdjustmentSettings.getList(Me.State.DealerId)
        End If
        Me.State.searchDV.Sort = Me.SortDirection
        Me.Grid.AutoGenerateColumns = False
        SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.PremiumAdjustmentSettingId, Me.Grid, Me.State.PageIndex)
        SortAndBindGrid()

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



        ControlMgr.SetVisibleControl(Me, Grid, Me.State.IsGridVisible)

        ControlMgr.SetVisibleControl(Me, trPageSize, Me.Grid.Visible)

        Session("recCount") = Me.State.searchDV.Count

        If Me.Grid.Visible Then
            Me.lblRecordCount.Text = Me.State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
        End If

    End Sub

    Private Sub PopulateDropdown()



        DealerMultipleDrop.Caption = TranslationBase.TranslateLabelOrMessage(LABEL_SELECT_DEALERCODE)
        DealerMultipleDrop.NothingSelected = True

        DealerMultipleDrop.BindData(LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies))
        DealerMultipleDrop.AutoPostBackDD = True


    End Sub

    Private Sub moDealerMultipleDrop_SelectedDropChanged(ByVal aSrc As Common.MultipleColumnDDLabelControl) Handles moDealerMultipleDrop.SelectedDropChanged
        Try
            Me.State.DealerId = Me.DealerMultipleDrop.SelectedGuid()
        Catch ex As Exception
            HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub


#End Region


#Region " Datagrid Related "

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
                e.Row.Cells(Me.GRID_COL_DEALER_CODE_IDX).Text = dvRow(PremiumAdjustmentSettings.PremiumAdjustnmentSettingSearchDV.COL_DEALER_CODE).ToString
                e.Row.Cells(Me.GRID_COL_ADJUSTMENT_BY_IDX).Text = LookupListNew.GetDescriptionFromId("FIN_ADJ_BY", New Guid(GetGuidStringFromByteArray(CType(dvRow(PremiumAdjustmentSettings.PremiumAdjustnmentSettingSearchDV.COL_ADJUSTMENT_BY), Byte()))))
                e.Row.Cells(Me.GRID_COL_EFFECTIVE_DATE_IDX).Text = dvRow(PremiumAdjustmentSettings.PremiumAdjustnmentSettingSearchDV.COL_EFFECTIVE_DATE).ToString
                e.Row.Cells(Me.GRID_COL_PREMIUM_ADJUSTMENT_SETTING_ID_IDX).Text = GetGuidStringFromByteArray(CType(dvRow(PremiumAdjustmentSettings.PremiumAdjustnmentSettingSearchDV.COL_PREMIUM_ADJUSTMENT_SETTING_ID), Byte()))
            End If
        End If
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

    Public Sub ItemCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)
        Try
            If e.CommandName = "SelectAction" Then
                Dim index As Integer = CInt(e.CommandArgument)
                Me.State.PremiumAdjustmentSettingId = New Guid(Me.Grid.Rows(index).Cells(Me.GRID_COL_PREMIUM_ADJUSTMENT_SETTING_ID_IDX).Text)
                Me.callPage(PremiumAdjustmentSettingsForm.URL, Me.State.PremiumAdjustmentSettingId)
            ElseIf e.CommandName = "Sort" Then
                Grid.DataMember = e.CommandArgument.ToString
                Me.PopulateGrid()
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try

    End Sub
    Public Sub ItemCreated(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
        BaseItemCreated(sender, e)
    End Sub



    Private Sub Grid_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Grid.PageIndexChanging
        Try
            Me.State.PageIndex = e.NewPageIndex
            Me.State.PremiumAdjustmentSettingId = Guid.Empty
            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

    Private Sub Grid_PageSizeChanged(ByVal source As Object, ByVal e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
        Try
            Grid.PageIndex = NewCurrentPageIndex(Grid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
            Me.State.PageSize = Grid.PageSize
            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub
#End Region

#Region " Buttons Clicks "
    Private Sub SetStateProperties()

        Me.State.DealerId = Me.DealerMultipleDrop.SelectedGuid()

    End Sub

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            Me.State.PageIndex = 0
            Me.State.PremiumAdjustmentSettingId = Guid.Empty
            Me.State.IsGridVisible = True
            Me.State.searchDV = Nothing
            Me.State.searchBtnClicked = True
            Me.PopulateGrid()
            Me.State.searchBtnClicked = False
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

    Private Sub btnAdd_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd_WRITE.Click
        Try
            Me.callPage(PremiumAdjustmentSettingsForm.URL)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

    Private Sub btnClearSearch_Click1(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClearSearch.Click
        Try
            ClearSearchCriteria()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub
    Private Sub ClearSearchCriteria()

        Try
            Me.DealerMultipleDrop.SelectedIndex = Me.BLANK_ITEM_SELECTED
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try

    End Sub

#End Region

#Region "Error Handling"


#End Region


End Class