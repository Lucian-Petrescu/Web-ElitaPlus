Namespace Tables

    Partial Public Class RuleListSearchForm
        Inherits ElitaPlusSearchPage


#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub

        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

#Region "Constants"
        Private Const GRID_COL_EDIT As Integer = 0
        Private Const GRID_COL_RULE_LIST_ID_IDX As Integer = 1
        Private Const GRID_COL_CODE_IDX As Integer = 2
        Private Const GRID_COL_DESCRIPTION_IDX As Integer = 3
        Private Const GRID_COL_EFFECTIVE_IDX As Integer = 4
        Private Const GRID_COL_EXPIRATION_IDX As Integer = 5
#End Region

#Region "Page State"
        Private IsReturningFromChild As Boolean = False

        Class MyState

            Public SelectedRuleId As Guid = Guid.Empty
            Public Code As String = String.Empty
            Public Description As String = String.Empty
            Public ActiveOnDate As DateType = Date.Today
            Public SortExpression As String = RuleList.RuleListSearchDV.COL_NAME_CODE
            Public PageIndex As Integer
            Public SearchDV As RuleList.RuleListSearchDV
            Public HasDataChanged As Boolean
            Public IsGridVisible As Boolean
            Public SelectedPageSize As Integer
            Public searchClick As Boolean = False

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

        Private Sub Page_PageReturn(ByVal ReturnFromUrl As String, ByVal ReturnPar As Object) Handles MyBase.PageReturn
            Try
                Me.MenuEnabled = True
                Me.IsReturningFromChild = True
                Dim retObj As RuleListDetailForm.ReturnType = CType(ReturnPar, RuleListDetailForm.ReturnType)
                Me.State.HasDataChanged = retObj.HasDataChanged
                Select Case retObj.LastOperation
                    Case ElitaPlusPage.DetailPageCommand.Back
                        If Not retObj Is Nothing Then
                            If Not retObj.EditingBo.IsNew Then
                                Me.State.SelectedRuleId = retObj.EditingBo.Id
                            End If
                            Me.State.IsGridVisible = True
                        End If
                    Case ElitaPlusPage.DetailPageCommand.Expire
                        Me.DisplayMessage(Message.EXPIRE_RECORD_CONFIRMATION, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
                    Case ElitaPlusPage.DetailPageCommand.Delete
                        Me.DisplayMessage(Message.DELETE_RECORD_CONFIRMATION, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
                End Select
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try
        End Sub

        Private Sub SaveGuiState()
            With Me.State
                .Code = Me.moCodeText.Text
                .Description = Me.moDescriptionText.Text
                '.ActiveOnDate = Date.Parse(Me.moActiveOnDateText.Text)
                PopulateBOProperty(Me.State, "ActiveOnDate", Me.moActiveOnDateText)
            End With
        End Sub

        Private Sub RestoreGuiState()
            With Me.State
                Me.moCodeText.Text = .Code
                Me.moDescriptionText.Text = .Description
                PopulateControlFromBOProperty(Me.moActiveOnDateText, .ActiveOnDate)
            End With
        End Sub

#End Region

#Region "Page_Events"
        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            Try
                Me.ErrorCtrl.Clear_Hide()
                If Not Me.IsPostBack Then
                    'ValidateDates()
                    Me.AddCalendar(Me.ImageButtonActiveOnDate, Me.moActiveOnDateText)
                    Me.SetDefaultButton(Me.moCodeText, btnSearch)
                    Me.SetDefaultButton(Me.moDescriptionText, btnSearch)
                    ControlMgr.SetVisibleControl(Me, trPageSize, False)

                    Me.RestoreGuiState()

                    If Me.State.IsGridVisible Then
                        If Not (Me.State.SelectedPageSize = DEFAULT_PAGE_SIZE) Then
                            cboPageSize.SelectedValue = CType(Me.State.SelectedPageSize, String)
                            If (Me.State.SelectedPageSize = 0) Then
                                Me.State.SelectedPageSize = DEFAULT_PAGE_SIZE
                            End If
                            Grid.PageSize = Me.State.SelectedPageSize
                        End If
                        Me.PopulateGrid()
                    End If
                    Me.SetGridItemStyleColor(Me.Grid)
                Else
                    ' Me.SaveGuiState()
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try
            Me.ShowMissingTranslations(Me.ErrorCtrl)
        End Sub
#End Region

#Region "Controlling Logic"

        Public Sub ValidateDates()
                If Not moActiveOnDateText.Text Is String.Empty Then
                    If (DateHelper.IsDate(moActiveOnDateText.Text) = False) Then
                        ElitaPlusPage.SetLabelError(Me.moActiveOnDateLabel)
                        Throw New GUIException(Message.MSG_BEGIN_END_DATE, Message.MSG_INVALID_DATE)
                    End If
            End If
        
        End Sub

        Public Sub PopulateGrid()
            If ((Me.State.SearchDV Is Nothing) OrElse (Me.State.HasDataChanged)) Then
                Me.State.Code = moCodeText.Text
                Me.State.Description = moDescriptionText.Text
                Dim ActiveOn As DateTimeType
                If moActiveOnDateText.Text.Trim = String.Empty Then
                    ActiveOn = Nothing
                Else
                    ActiveOn = DateHelper.GetDateValue(Me.moActiveOnDateText.Text)
                End If
                Me.State.SearchDV = RuleList.GetList(Me.State.Code, Me.State.Description, ActiveOn)
            End If
            Me.State.SearchDV.Sort = Me.State.SortExpression

            Me.Grid.AutoGenerateColumns = False
            Me.Grid.Columns(Me.GRID_COL_CODE_IDX).SortExpression = RuleList.RuleListSearchDV.COL_NAME_CODE
            Me.Grid.Columns(Me.GRID_COL_DESCRIPTION_IDX).SortExpression = RuleList.RuleListSearchDV.COL_NAME_DESCRIPTION
            Me.Grid.Columns(Me.GRID_COL_EFFECTIVE_IDX).SortExpression = RuleList.RuleListSearchDV.COL_NAME_EFFECTIVE
            Me.Grid.Columns(Me.GRID_COL_EXPIRATION_IDX).SortExpression = RuleList.RuleListSearchDV.COL_NAME_EXPIRATION

            SetPageAndSelectedIndexFromGuid(Me.State.SearchDV, Me.State.SelectedRuleId, Me.Grid, Me.State.PageIndex)
            If Me.State.searchClick Then
                Me.ValidSearchResultCount(Me.State.SearchDV.Count, True)
                Me.State.searchClick = False
            End If
            Me.SortAndBindGrid()
        End Sub

        Private Sub SortAndBindGrid()
            Me.State.PageIndex = Me.Grid.CurrentPageIndex
            Me.Grid.DataSource = Me.State.SearchDV
            HighLightSortColumn(Grid, Me.State.SortExpression)
            Me.Grid.DataBind()

            ControlMgr.SetVisibleControl(Me, Grid, Me.State.IsGridVisible)
            ControlMgr.SetVisibleControl(Me, trPageSize, Me.Grid.Visible)

            Session("recCount") = Me.State.SearchDV.Count

            If Me.State.SearchDV.Count > 0 Then
                If Me.Grid.Visible Then
                    Me.lblRecordCount.Text = Me.State.SearchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                End If
            Else
                If Me.Grid.Visible Then
                    Me.lblRecordCount.Text = Me.State.SearchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                End If
            End If
        End Sub
#End Region

#Region " Datagrid Related "

        'The Binding Logic is here  
        Private Sub Grid_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles Grid.ItemDataBound
            Try
                Dim itemType As ListItemType = CType(e.Item.ItemType, ListItemType)
                Dim dvRow As DataRowView = CType(e.Item.DataItem, DataRowView)

                If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then
                    e.Item.Cells(Me.GRID_COL_RULE_LIST_ID_IDX).Text = GetGuidStringFromByteArray(CType(dvRow(RuleList.RuleListSearchDV.COL_NAME_RULE_LIST_ID), Byte()))
                    e.Item.Cells(Me.GRID_COL_CODE_IDX).Text = dvRow(RuleList.RuleListSearchDV.COL_NAME_CODE).ToString
                    e.Item.Cells(Me.GRID_COL_DESCRIPTION_IDX).Text = dvRow(RuleList.RuleListSearchDV.COL_NAME_DESCRIPTION).ToString
                    e.Item.Cells(Me.GRID_COL_EFFECTIVE_IDX).Text = dvRow(RuleList.RuleListSearchDV.COL_NAME_EFFECTIVE).ToString
                    e.Item.Cells(Me.GRID_COL_EXPIRATION_IDX).Text = dvRow(RuleList.RuleListSearchDV.COL_NAME_EXPIRATION).ToString
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try
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

        Public Sub ItemCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs)
            Try
                If e.CommandName = "SelectAction" Then
                    Me.State.SelectedRuleId = New Guid(e.Item.Cells(Me.GRID_COL_RULE_LIST_ID_IDX).Text)
                    Me.callPage(RuleListDetailForm.URL, Me.State.SelectedRuleId)
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try

        End Sub

        Public Sub ItemCreated(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
            BaseItemCreated(sender, e)
        End Sub

        Private Sub Grid_PageIndexChanged(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles Grid.PageIndexChanged
            Try
                Me.State.PageIndex = e.NewPageIndex
                Me.State.SelectedRuleId = Guid.Empty
                Me.PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try
        End Sub

#End Region

#Region "Button Clicks "

        Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
            Try
                Me.State.PageIndex = 0
                Me.State.SelectedRuleId = Guid.Empty
                Me.State.IsGridVisible = True
                Me.State.SearchDV = Nothing
                Me.State.HasDataChanged = False
                Me.State.searchClick = True
                Me.ValidateDates()
                Me.PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try
        End Sub

        Protected Sub btnAdd_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd_WRITE.Click
            Try
                Me.callPage(RuleListDetailForm.URL)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try
        End Sub

        Protected Sub btnClearSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClearSearch.Click
            Try
                Me.moCodeText.Text = String.Empty
                Me.moDescriptionText.Text = String.Empty
                Me.moActiveOnDateText.Text = String.Empty


                Me.State.Code = String.Empty
                Me.State.Description = String.Empty
                Me.State.ActiveOnDate = Nothing

            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try
        End Sub

#End Region

#Region "Error Handling"


#End Region


    End Class

End Namespace

