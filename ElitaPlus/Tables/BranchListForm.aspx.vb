Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Partial Class BranchListForm
    Inherits ElitaPlusSearchPage

#Region "Page State"

    ' This class keeps the current state for the search page.
    Class MyState
        Public MyBO As Branch
        Public PageIndex As Integer = 0
        Public DescriptionMask As String
        Public CodeMask As String
        Public DealerId As Guid
        Public BranchId As Guid = Guid.Empty
        Public IsGridVisible As Boolean
        Private mnPageIndex As Integer = 0
        Private msPageSort As String
        Private mnPageSize As Integer = DEFAULT_PAGE_SIZE
        Public searchDV As Branch.BranchSearchDV = Nothing
        Public SortExpression As String = Branch.COL_BRANCH_NAME
        Public selectedPageSize As Integer = DEFAULT_PAGE_SIZE
        Public searchBtnClicked As Boolean = False
        'Public SortExpression As String = Branch.BranchSearchDV.COL_BRANCH_CODE
        Public HasDataChanged As Boolean
        Public bnoRow As Boolean = False


#Region "State-Properties"

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

        Public Property SearchDataView() As Branch.BranchSearchDV
            Get
                Return searchDV
            End Get
            Set(Value As Branch.BranchSearchDV)
                searchDV = Value
            End Set
        End Property
#End Region

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

    Private IsReturningFromChild As Boolean = False

    Public Sub Page_PageReturn(ReturnFromUrl As String, ReturnPar As Object) Handles MyBase.PageReturn
        Try
            MenuEnabled = True
            IsReturningFromChild = True
            Dim retObj As BranchForm.ReturnType = CType(ReturnPar, BranchForm.ReturnType)
            State.HasDataChanged = retObj.HasDataChanged
            Select Case retObj.LastOperation
                Case ElitaPlusPage.DetailPageCommand.Back
                    If retObj IsNot Nothing Then
                        State.BranchId = retObj.moBranchId
                        State.IsGridVisible = True
                    End If
                Case ElitaPlusPage.DetailPageCommand.Delete
                    AddInfoMsg(Message.DELETE_RECORD_CONFIRMATION)
                    State.BranchId = Guid.Empty
                Case Else
                    State.BranchId = Guid.Empty
            End Select
            moDataGrid.CurrentPageIndex = State.PageIndex
            cboPageSize.SelectedValue = CType(State.PageSize, String)
            moDataGrid.PageSize = State.PageSize
            ControlMgr.SetVisibleControl(Me, trPageSize, moDataGrid.Visible)
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Public Class ReturnType
        Public LastOperation As ElitaPlusPage.DetailPageCommand
        Public moBranchId As Guid
        Public BoChanged As Boolean = False

        Public Sub New(LastOp As ElitaPlusPage.DetailPageCommand, oBranchId As Guid, Optional ByVal boChanged As Boolean = False)
            LastOperation = LastOp
            moBranchId = oBranchId
            Me.BoChanged = boChanged
        End Sub

    End Class

#End Region

#End Region

#Region "Constants"
    Public Const GRID_COL_EDIT_IDX As Integer = 0
    Public Const GRID_COL_BRANCH_CODE_IDX As Integer = 1
    Public Const GRID_COL_BRANCH_NAME_IDX As Integer = 2
    Public Const GRID_COL_BRANCH_IDX As Integer = 3

    Public Const GRID_TOTAL_COLUMNS As Integer = 4
    Private Const NOTHING_SELECTED As String = "00000000-0000-0000-0000-000000000000"
    Private Const BRANCH_LIST_FORM001 As String = "BRANCH_LIST_FORM001" ' Maintain Branch List Exception
    Private Const BRANCHLISTFORM As String = "BranchListForm.aspx"
    Private Const LABEL_SELECT_DEALERCODE As String = "DEALER"
#End Region


#Region "Properties"
    Public ReadOnly Property TheDealerControl() As MultipleColumnDDLabelControl_New
        Get
            If multipleDropControl Is Nothing Then
                multipleDropControl = CType(FindControl("multipleDropControl"), MultipleColumnDDLabelControl_New)
            End If
            Return multipleDropControl
        End Get
    End Property
#End Region

#Region "Handlers"

#Region "Handlers-Init"

    Protected WithEvents moErrorController As ErrorController
    Protected WithEvents multipleDropControl As MultipleColumnDDLabelControl_New

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    Private Sub Page_Init(sender As System.Object, e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        'Put User code to initialize the page here
        Try
            MasterPage.MessageController.Clear_Hide()

            If Not Page.IsPostBack Then

                MasterPage.MessageController.Clear()
                MasterPage.UsePageTabTitleInBreadCrum = False
                MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("Tables")
                UpdateBreadCrum()
                ControlMgr.SetVisibleControl(Me, trPageSize, False)
                SetGridItemStyleColor(moDataGrid)
                PopulateDealer()
                If Not IsReturningFromChild Then
                    ' It is The First Time
                    ' It is not Returning from Detail
                    ControlMgr.SetVisibleControl(Me, trPageSize, False)
                Else
                    ' It is returning from detail
                    ControlMgr.SetVisibleControl(Me, moDataGrid, State.IsGridVisible)
                    ControlMgr.SetVisibleControl(Me, trPageSize, State.IsGridVisible)
                    If State.IsGridVisible Then
                        PopulateGrid(POPULATE_ACTION_SAVE)
                    End If
                End If
            Else
                ClearErrLabels()
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
        ShowMissingTranslations(MasterPage.MessageController)
    End Sub

    Private Sub UpdateBreadCrum()
        If (State IsNot Nothing) Then
            If (State IsNot Nothing) Then
                MasterPage.BreadCrum = MasterPage.PageTab & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage("BRANCH_INFO")
                MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("BRANCH_INFO")
            End If
        End If
    End Sub

#End Region

#Region "Handlers-Buttons"

    Private Sub moBtnSearch_Click(sender As System.Object, e As System.EventArgs) Handles btnSearch.Click
        Try
            If TheDealerControl.SelectedGuid.Equals(Guid.Empty) Then
                ElitaPlusPage.SetLabelError(TheDealerControl.CaptionLabel)
                Throw New GUIException(Message.MSG_INVALID_DEALER, Assurant.ElitaPlus.Common.ErrorCodes.GUI_DEALER_MUST_BE_SELECTED_ERR)
            End If
            If TheDealerControl.SelectedIndex > NO_ITEM_SELECTED_INDEX Then
                moDataGrid.CurrentPageIndex = NO_PAGE_INDEX
                State.SortExpression = Branch.COL_BRANCH_NAME
                moDataGrid.DataMember = Nothing
                State.searchDV = Nothing
                State.searchBtnClicked = True
                PopulateGrid()
                State.searchBtnClicked = False
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub moBtnClear_Click(sender As System.Object, e As System.EventArgs) Handles btnClearSearch.Click
        Try
            ClearSearch()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub BtnNew_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles BtnNew_WRITE.Click
        Try
            SetSession()
            callPage(BranchForm.URL)
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

#End Region

#Region "Handlers-Grid"

    Public Property SortDirection() As String
        Get
            Return ViewState("SortDirection").ToString
        End Get
        Set(value As String)
            ViewState("SortDirection") = value
        End Set
    End Property

    Private Sub moDataGrid_PageIndexChanged(source As System.Object, e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles moDataGrid.PageIndexChanged
        Try
            moDataGrid.CurrentPageIndex = e.NewPageIndex
            PopulateGrid(POPULATE_ACTION_NO_EDIT)
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Public Sub ItemCreated(sender As Object, e As DataGridItemEventArgs)
        Try
            BaseItemCreated(sender, e)
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Sub ItemCommand(source As System.Object, e As System.Web.UI.WebControls.DataGridCommandEventArgs)
        Dim sBranchId As String
        Try
            If e.CommandSource.GetType.Equals(GetType(ImageButton)) Then
                'this only runs when they click the pencil button for editing.
                sBranchId = CType(e.Item.FindControl("branch_id"), Label).Text
                State.BranchId = GetGuidFromString(sBranchId)
                SetSession()
                callPage(BranchForm.URL, State.BranchId)
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
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
            moDataGrid.SelectedIndex = -1
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

#End Region

#End Region


#Region "Populate"

    Private Sub PopulateDealer()
        Try
            Dim oDealerview As DataView = LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies)
            Dim oDataView As DataView = LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies)
            TheDealerControl.Caption = TranslationBase.TranslateLabelOrMessage(LABEL_SELECT_DEALERCODE)
            TheDealerControl.NothingSelected = True
            TheDealerControl.BindData(oDataView)
            TheDealerControl.SelectedGuid = State.DealerId
            TheDealerControl.AutoPostBackDD = True
        Catch ex As Exception
            moErrorController.AddError(BRANCH_LIST_FORM001)
            moErrorController.AddError(ex.Message, False)
            moErrorController.Show()
        End Try
    End Sub

    Private Sub BindDataGrid(oDataView As DataView)
        moDataGrid.DataSource = oDataView
        moDataGrid.DataBind()
    End Sub

    Private Sub PopulateGrid(Optional ByVal oAction As String = POPULATE_ACTION_NONE)

        Try
            State.DealerId = TheDealerControl.SelectedGuid

            If ((State.searchDV Is Nothing) OrElse (State.HasDataChanged)) Then
                State.searchDV = Branch.getList(SearchDescriptionTextBox.Text, SearchCodeTextBox.Text, State.DealerId)
            End If
            State.searchDV.Sort = State.SortExpression
            moDataGrid.AutoGenerateColumns = False
            HighLightSortColumn(moDataGrid, State.SortExpression)
            BasePopulateGrid(moDataGrid, State.searchDV, State.BranchId, oAction)

            ControlMgr.SetVisibleControl(Me, trPageSize, moDataGrid.Visible)

            Session("recCount") = State.searchDV.Count

            If moDataGrid.Visible Then
                lblRecordCount.Text = State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
            End If

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Grid_PageSizeChanged(source As Object, e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
        Try
            moDataGrid.CurrentPageIndex = NewCurrentPageIndex(moDataGrid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, moErrorController)
        End Try
    End Sub


#End Region

#Region "Clear"

    Private Sub ClearSearch()
        Try
            SearchDescriptionTextBox.Text = String.Empty
            SearchCodeTextBox.Text = String.Empty
            TheDealerControl.SelectedIndex = 0
            'moDealerDrop.SelectedIndex = 0
            'Update Page State
            With State
                .DescriptionMask = SearchDescriptionTextBox.Text
                .CodeMask = SearchCodeTextBox.Text
            End With
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try

    End Sub

    Private Sub ClearErrLabels()
        ClearLabelErrSign(TheDealerControl.CaptionLabel)
    End Sub

#End Region

#Region "State-Management"

    Private Sub SetSession()
        With State
            .CodeMask = SearchCodeTextBox.Text
            .DescriptionMask = SearchDescriptionTextBox.Text
            .DealerId = State.DealerId 'Me.GetSelectedItem(moDealerDrop)
            .PageIndex = moDataGrid.CurrentPageIndex
            .PageSize = moDataGrid.PageSize
            .PageSort = State.SortExpression
            .SearchDataView = State.searchDV
        End With
    End Sub

    Private Sub GetSession()
        'Dim oDataView As DataView
        With State
            SearchCodeTextBox.Text = .CodeMask
            SearchDescriptionTextBox.Text = .DescriptionMask
            moDataGrid.PageSize = .PageSize
            cboPageSize.SelectedValue = CType(.PageSize, String)
            'oDataView.Sort = .PageSort
        End With
    End Sub

#End Region

End Class