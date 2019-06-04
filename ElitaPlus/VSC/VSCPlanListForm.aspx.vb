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

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
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
            Set(ByVal Value As Integer)
                mnPageSize = Value
            End Set
        End Property

        Public Property PageSort() As String
            Get
                Return msPageSort
            End Get
            Set(ByVal Value As String)
                msPageSort = Value
            End Set
        End Property

        Public Property SearchDataView() As VSCPlan.VSCPlanSearchDV
            Get
                Return searchDV
            End Get
            Set(ByVal Value As VSCPlan.VSCPlanSearchDV)
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


    Private Sub Page_PageReturn(ByVal ReturnFromUrl As String, ByVal ReturnPar As Object) Handles MyBase.PageReturn
        Try
            Me.MenuEnabled = True
            Me.IsReturningFromChild = True
            Dim retObj As VSCPlanForm.ReturnType = CType(ReturnPar, VSCPlanForm.ReturnType)

            Me.State.HasDataChanged = retObj.HasDataChanged
            Select Case retObj.LastOperation
                Case ElitaPlusPage.DetailPageCommand.Back
                    If Not retObj Is Nothing Then
                        Me.State.PlanId = retObj.EditingBo.Id
                        Me.State.IsGridVisible = True
                    End If
                Case ElitaPlusPage.DetailPageCommand.Delete
                    Me.AddInfoMsg(Message.DELETE_RECORD_CONFIRMATION)
            End Select
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

#End Region
#End Region

#Region "Page_Events"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Me.ErrorCtrl.Clear_Hide()
        Try
            If Not Me.IsPostBack Then
                If Me.IsReturningFromChild = True Then
                    GetSession()
                End If
                Me.PopulateFormFromBOs()
                ControlMgr.SetVisibleControl(Me, trPageSize, False)
                If Me.State.IsGridVisible Then
                    If Not (Me.State.selectedPageSize = DEFAULT_PAGE_SIZE) Then
                        cboPageSize.SelectedValue = CType(Me.State.selectedPageSize, String)
                        Grid.PageSize = Me.State.selectedPageSize
                    End If
                    Me.PopulateGrid()
                End If
                Me.SetGridItemStyleColor(Me.Grid)
            Else
                ClearErrLabels()
            End If

            If Me.IsReturningFromChild = True Then
                Me.IsReturningFromChild = False
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
        Me.ShowMissingTranslations(Me.ErrorCtrl)
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
            Me.TheVSCPlanControl.SelectedGuid = Me.State.SelectedDropdownPlanId
        Catch ex As Exception
            ErrorCtrl.AddError(VSCPLAN_LIST_FORM001)
            ErrorCtrl.AddError(ex.Message, False)
            ErrorCtrl.Show()
        End Try
    End Sub
#End Region

#Region "Clear"

    Private Sub ClearErrLabels()
        Me.ClearLabelErrSign(TheVSCPlanControl.CaptionLabel)
    End Sub

#End Region

#Region "Controlling Logic"

    Public Sub PopulateGrid()

        Dim oVSCPlan As VSCPlan
        Dim oCompanyGroupIds As New ArrayList
        oCompanyGroupIds.Add(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id)

        If TheVSCPlanControl.SelectedDesc = "" Then
            Me.State.searchDV = VSCPlan.getList(oCompanyGroupIds)
        Else
            Me.State.searchDV = VSCPlan.getPlan(oCompanyGroupIds, TheVSCPlanControl.SelectedGuid)
        End If

        Me.State.searchDV.Sort = Me.State.SortExpression
        Me.Grid.AutoGenerateColumns = False
        Me.Grid.Columns(Me.GRID_COL_PLAN_CODE_IDX).SortExpression = VSCPlan.VSCPlanSearchDV.COL_CODE
        Me.Grid.Columns(Me.GRID_COL_PLAN_DESC_IDX).SortExpression = VSCPlan.VSCPlanSearchDV.COL_DESCRIPTION

        SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.PlanId, Me.Grid, Me.State.PageIndex)
        Me.SortAndBindGrid()

    End Sub
    Private Sub SortAndBindGrid()
        Me.State.PageIndex = Me.Grid.CurrentPageIndex
        Me.Grid.DataSource = Me.State.searchDV
        HighLightSortColumn(Grid, Me.State.SortExpression)
        Me.Grid.DataBind()

        ControlMgr.SetVisibleControl(Me, Grid, Me.State.IsGridVisible)

        ControlMgr.SetVisibleControl(Me, trPageSize, Me.Grid.Visible)

        Session("recCount") = Me.State.searchDV.Count

        If Me.State.searchDV.Count > 0 Then

            If Me.Grid.Visible Then
                Me.lblRecordCount.Text = Me.State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
            End If
        Else
            If Me.Grid.Visible Then
                Me.lblRecordCount.Text = Me.State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
            End If
        End If
    End Sub

#End Region

#Region "Datagrid Related "

    'The Binding Logic is here
    Private Sub Grid_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles Grid.ItemDataBound
        Dim itemType As ListItemType = CType(e.Item.ItemType, ListItemType)
        Dim dvRow As DataRowView = CType(e.Item.DataItem, DataRowView)
        If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then
            e.Item.Cells(Me.GRID_COL_PLAN_CODE_IDX).Text = dvRow(VSCPlan.VSCPlanSearchDV.COL_CODE).ToString
            e.Item.Cells(Me.GRID_COL_PLAN_DESC_IDX).Text = dvRow(VSCPlan.VSCPlanSearchDV.COL_DESCRIPTION).ToString
            e.Item.Cells(Me.GRID_COL_PLAN_IDX).Text = GetGuidStringFromByteArray(CType(dvRow(VSCPlan.VSCPlanSearchDV.COL_VSCPlan_ID), Byte()))
        End If
    End Sub

    Public Sub ItemCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs)
        Try
            If e.CommandName = "SelectAction" Then
                Me.State.PlanId = New Guid(e.Item.Cells(Me.GRID_COL_PLAN_IDX).Text)
                SetSession()
                Me.callPage(VSCPlanForm.URL, Me.State.PlanId)
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try

    End Sub

    Public Sub ItemCreated(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
        BaseItemCreated(sender, e)
    End Sub

    Private Sub Grid_PageSizeChanged(ByVal source As Object, ByVal e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
        Try
            Grid.CurrentPageIndex = NewCurrentPageIndex(Grid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

    Private Sub Grid_SortCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles Grid.SortCommand
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
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

    Private Sub Grid_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles Grid.PageIndexChanged
        Try
            Me.State.PageIndex = e.NewPageIndex
            Me.State.PlanId = Guid.Empty
            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub
#End Region

#Region "Button Clicks "
    Private Sub btnAdd_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd_WRITE.Click
        SetSession()
        Me.callPage(VSCPlanForm.URL)
    End Sub

    Private Sub btnClearSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearSearch.Click
        TheVSCPlanControl.SelectedIndex = 0
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            Me.State.PageIndex = 0
            Me.State.PlanId = Guid.Empty
            Me.State.IsGridVisible = True
            Me.State.searchDV = Nothing
            Me.State.HasDataChanged = False
            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub
#End Region

#Region "State-Management"

    Private Sub SetSession()
        With Me.State
            .PlanId = Me.State.PlanId
            .PageIndex = Grid.CurrentPageIndex
            .PageSize = Grid.PageSize
            .PageSort = Me.State.SortExpression
            .SearchDataView = Me.State.searchDV
            .SelectedDropdownPlanId = TheVSCPlanControl.SelectedGuid
        End With
    End Sub

    Private Sub GetSession()
        With Me.State
            Me.Grid.PageSize = .PageSize
            cboPageSize.SelectedValue = CType(.PageSize, String)
        End With
    End Sub
#End Region
End Class
