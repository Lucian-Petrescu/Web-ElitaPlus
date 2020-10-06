Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Partial Class VSCPlanListForm
    Inherits ElitaPlusSearchPage

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents ErrorCtrl As ErrorController
    Protected WithEvents multipleDropControl As MultipleColumnDDLabelControl

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(sender As System.Object, e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Properties"

    Public ReadOnly Property TheVSCPlanControl() As MultipleColumnDDLabelControl
        Get
            If multipleDropControl Is Nothing Then
                multipleDropControl = CType(FindControl("multipleDropControl"), MultipleColumnDDLabelControl)
            End If
            Return multipleDropControl
        End Get
    End Property

#End Region

#Region "Constants"
    Public Const GRID_COL_EDIT_IDX As Integer = 0
    Public Const GRID_COL_PLAN_CODE_IDX As Integer = 1
    Public Const GRID_COL_PLAN_DESC_IDX As Integer = 2
    Public Const GRID_COL_PLAN_IDX As Integer = 3

    Public Const GRID_TOTAL_COLUMNS As Integer = 4
    Private Const VSCPLAN_LIST_FORM001 As String = "VSCPLAN_LIST_FORM001" ' Maintain VSC PLAN list Exception
    Private Const VSCPLANLISTFORM As String = "VSCPlanListForm.aspx"
    Private Const LABEL_SELECT_VSCPLANCODE As String = "Plan"
#End Region

#Region "Page State"
    Private IsReturningFromChild As Boolean = False

    ' This class keeps the current state for the search page.
    Class MyState
        Public MyBO As VSCPlan
        Public PageIndex As Integer = 0
        Public DescriptionMask As String
        Public CodeMask As String
        Public PlanId As Guid
        Public SelectedDropdownPlanId As Guid
        Public IsGridVisible As Boolean
        Private mnPageIndex As Integer = 0
        Private msPageSort As String
        Private mnPageSize As Integer = DEFAULT_PAGE_SIZE
        Public searchDV As VSCPlan.VSCPlanSearchDV = Nothing
        Public selectedPageSize As Integer = DEFAULT_PAGE_SIZE
        Public SortExpression As String = VSCPlan.VSCPlanSearchDV.COL_DESCRIPTION
        Public HasDataChanged As Boolean


        Public Property PageSize() As Integer
            Get
                Return mnPageSize
            End Get
            Set(Value As Integer)
                mnPageSize = Value
            End Set
        End Property

        Public Property PageSort() As String
            Get
                Return msPageSort
            End Get
            Set(Value As String)
                msPageSort = Value
            End Set
        End Property

        Public Property SearchDataView() As VSCPlan.VSCPlanSearchDV
            Get
                Return searchDV
            End Get
            Set(Value As VSCPlan.VSCPlanSearchDV)
                searchDV = Value
            End Set
        End Property

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
            Dim retObj As VSCPlanForm.ReturnType = CType(ReturnPar, VSCPlanForm.ReturnType)

            State.HasDataChanged = retObj.HasDataChanged
            Select Case retObj.LastOperation
                Case ElitaPlusPage.DetailPageCommand.Back
                    If retObj IsNot Nothing Then
                        State.PlanId = retObj.EditingBo.Id
                        State.IsGridVisible = True
                    End If
                Case ElitaPlusPage.DetailPageCommand.Delete
                    AddInfoMsg(Message.DELETE_RECORD_CONFIRMATION)
            End Select
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

#End Region
#End Region

#Region "Page_Events"
    Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        ErrorCtrl.Clear_Hide()
        Try
            If Not IsPostBack Then
                If IsReturningFromChild = True Then
                    GetSession()
                End If
                PopulateFormFromBOs()
                ControlMgr.SetVisibleControl(Me, trPageSize, False)
                If State.IsGridVisible Then
                    If Not (State.selectedPageSize = DEFAULT_PAGE_SIZE) Then
                        cboPageSize.SelectedValue = CType(State.selectedPageSize, String)
                        Grid.PageSize = State.selectedPageSize
                    End If
                    PopulateGrid()
                End If
                SetGridItemStyleColor(Grid)
            Else
                ClearErrLabels()
            End If

            If IsReturningFromChild = True Then
                IsReturningFromChild = False
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
        ShowMissingTranslations(ErrorCtrl)
    End Sub
#End Region

#Region "Populate"
    Protected Sub PopulateFormFromBOs()
        PopulateVSCPlans()
    End Sub

    Private Sub PopulateVSCPlans()
        Try
            Dim oVSCPlan As VSCPlan
            Dim oCompanyGroupIds As New ArrayList
            oCompanyGroupIds.Add(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id)

            Dim oVSCPlanview As DataView = oVSCPlan.getList(oCompanyGroupIds)

            TheVSCPlanControl.SetControl(False, _
                                        TheVSCPlanControl.MODES.NEW_MODE, _
                                        True, _
                                        oVSCPlanview, _
                                        "" + TranslationBase.TranslateLabelOrMessage(LABEL_SELECT_VSCPLANCODE), _
                                        True, True, _
                                        , _
                                        "multipleDropControl_moMultipleColumnDrop", _
                                        "multipleDropControl_moMultipleColumnDropDesc", _
                                        "multipleDropControl_lb_DropDown", _
                                        False, _
                                        0)
            TheVSCPlanControl.SelectedGuid = State.SelectedDropdownPlanId
        Catch ex As Exception
            ErrorCtrl.AddError(VSCPLAN_LIST_FORM001)
            ErrorCtrl.AddError(ex.Message, False)
            ErrorCtrl.Show()
        End Try
    End Sub
#End Region

#Region "Clear"

    Private Sub ClearErrLabels()
        ClearLabelErrSign(TheVSCPlanControl.CaptionLabel)
    End Sub

#End Region

#Region "Controlling Logic"

    Public Sub PopulateGrid()

        Dim oVSCPlan As VSCPlan
        Dim oCompanyGroupIds As New ArrayList
        oCompanyGroupIds.Add(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id)

        If TheVSCPlanControl.SelectedDesc = "" Then
            State.searchDV = VSCPlan.getList(oCompanyGroupIds)
        Else
            State.searchDV = VSCPlan.getPlan(oCompanyGroupIds, TheVSCPlanControl.SelectedGuid)
        End If

        State.searchDV.Sort = State.SortExpression
        Grid.AutoGenerateColumns = False
        Grid.Columns(GRID_COL_PLAN_CODE_IDX).SortExpression = VSCPlan.VSCPlanSearchDV.COL_CODE
        Grid.Columns(GRID_COL_PLAN_DESC_IDX).SortExpression = VSCPlan.VSCPlanSearchDV.COL_DESCRIPTION

        SetPageAndSelectedIndexFromGuid(State.searchDV, State.PlanId, Grid, State.PageIndex)
        SortAndBindGrid()

    End Sub
    Private Sub SortAndBindGrid()
        State.PageIndex = Grid.CurrentPageIndex
        Grid.DataSource = State.searchDV
        HighLightSortColumn(Grid, State.SortExpression)
        Grid.DataBind()

        ControlMgr.SetVisibleControl(Me, Grid, State.IsGridVisible)

        ControlMgr.SetVisibleControl(Me, trPageSize, Grid.Visible)

        Session("recCount") = State.searchDV.Count

        If State.searchDV.Count > 0 Then

            If Grid.Visible Then
                lblRecordCount.Text = State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
            End If
        Else
            If Grid.Visible Then
                lblRecordCount.Text = State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
            End If
        End If
    End Sub

#End Region

#Region "Datagrid Related "

    'The Binding Logic is here
    Private Sub Grid_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles Grid.ItemDataBound
        Dim itemType As ListItemType = CType(e.Item.ItemType, ListItemType)
        Dim dvRow As DataRowView = CType(e.Item.DataItem, DataRowView)
        If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then
            e.Item.Cells(GRID_COL_PLAN_CODE_IDX).Text = dvRow(VSCPlan.VSCPlanSearchDV.COL_CODE).ToString
            e.Item.Cells(GRID_COL_PLAN_DESC_IDX).Text = dvRow(VSCPlan.VSCPlanSearchDV.COL_DESCRIPTION).ToString
            e.Item.Cells(GRID_COL_PLAN_IDX).Text = GetGuidStringFromByteArray(CType(dvRow(VSCPlan.VSCPlanSearchDV.COL_VSCPlan_ID), Byte()))
        End If
    End Sub

    Public Sub ItemCommand(source As System.Object, e As System.Web.UI.WebControls.DataGridCommandEventArgs)
        Try
            If e.CommandName = "SelectAction" Then
                State.PlanId = New Guid(e.Item.Cells(GRID_COL_PLAN_IDX).Text)
                SetSession()
                callPage(VSCPlanForm.URL, State.PlanId)
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try

    End Sub

    Public Sub ItemCreated(sender As System.Object, e As System.Web.UI.WebControls.DataGridItemEventArgs)
        BaseItemCreated(sender, e)
    End Sub

    Private Sub Grid_PageSizeChanged(source As Object, e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
        Try
            Grid.CurrentPageIndex = NewCurrentPageIndex(Grid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

    Private Sub Grid_SortCommand(source As System.Object, e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles Grid.SortCommand
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
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

    Private Sub Grid_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles Grid.PageIndexChanged
        Try
            State.PageIndex = e.NewPageIndex
            State.PlanId = Guid.Empty
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub
#End Region

#Region "Button Clicks "
    Private Sub btnAdd_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnAdd_WRITE.Click
        SetSession()
        callPage(VSCPlanForm.URL)
    End Sub

    Private Sub btnClearSearch_Click(sender As System.Object, e As System.EventArgs) Handles btnClearSearch.Click
        TheVSCPlanControl.SelectedIndex = 0
    End Sub

    Private Sub btnSearch_Click(sender As System.Object, e As System.EventArgs) Handles btnSearch.Click
        Try
            State.PageIndex = 0
            State.PlanId = Guid.Empty
            State.IsGridVisible = True
            State.searchDV = Nothing
            State.HasDataChanged = False
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub
#End Region

#Region "State-Management"

    Private Sub SetSession()
        With State
            .PlanId = State.PlanId
            .PageIndex = Grid.CurrentPageIndex
            .PageSize = Grid.PageSize
            .PageSort = State.SortExpression
            .SearchDataView = State.searchDV
            .SelectedDropdownPlanId = TheVSCPlanControl.SelectedGuid
        End With
    End Sub

    Private Sub GetSession()
        With State
            Grid.PageSize = .PageSize
            cboPageSize.SelectedValue = CType(.PageSize, String)
        End With
    End Sub
#End Region
End Class
