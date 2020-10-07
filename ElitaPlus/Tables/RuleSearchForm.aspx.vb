Imports BusObjects = Assurant.ElitaPlus.BusinessObjectsNew
Imports Microsoft.VisualBasic
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports Assurant.Elita.Web.Forms
Imports System.Threading

Namespace Tables
    Partial Public Class RuleSearchForm
        Inherits ElitaPlusSearchPage


#Region "Constants"
        Public Const URL As String = "RuleSearchForm.aspx"
        Private Const GRID_COL_EDIT As Integer = 0
        Private Const GRID_COL_RULE_ID_IDX As Integer = 1
        Private Const GRID_COL_CODE_IDX As Integer = 2
        Private Const GRID_COL_DESCRIPTION_IDX As Integer = 3
        Private Const GRID_COL_RULE_TYPE_IDX As Integer = 4
        Private Const GRID_COL_RULE_CATEGORY_IDX As Integer = 5
        Private Const GRID_COL_EFFECTIVE_IDX As Integer = 6
        Private Const GRID_COL_EXPIRATION_IDX As Integer = 7
#End Region

#Region "Page Return Type"
        Public Class ReturnType
            Public LastOperation As DetailPageCommand
            Public EditingBo As BusObjects.Rule
            Public HasDataChanged As Boolean
            Public Sub New(LastOp As DetailPageCommand, curEditingBo As BusObjects.Rule, hasDataChanged As Boolean)
                LastOperation = LastOp
                EditingBo = curEditingBo
                Me.HasDataChanged = hasDataChanged
            End Sub
        End Class
#End Region

#Region "Page State"
        Private IsReturningFromChild As Boolean = False

        Class MyState

            Public SelectedQuestionId As Guid = Guid.Empty
            Public Code As String = String.Empty
            Public Description As String = String.Empty
            Public ActiveOnDate As DateType = Date.Today
            Public ruletypeId As Guid
            Public rulecategoryId As Guid
            Public SortExpression As String = BusObjects.Rule.RuleSearchDV.COL_NAME_CODE
            Public PageIndex As Integer
            Public SearchDV As BusObjects.Rule.RuleSearchDV
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

        Private Sub Page_PageReturn(ReturnFromUrl As String, ReturnPar As Object) Handles MyBase.PageReturn
            Try
                MenuEnabled = True
                IsReturningFromChild = True
                Dim retObj As RuleForm.ReturnType = CType(ReturnPar, RuleForm.ReturnType)
                State.HasDataChanged = retObj.HasDataChanged
                Select Case retObj.LastOperation
                    Case ElitaPlusPage.DetailPageCommand.Back
                        If retObj IsNot Nothing Then
                            If Not retObj.EditingBo.IsNew Then
                                State.SelectedQuestionId = retObj.EditingBo.Id
                            End If
                            State.IsGridVisible = True
                        End If
                    Case ElitaPlusPage.DetailPageCommand.Delete
                        DisplayMessage(Message.DELETE_RECORD_CONFIRMATION, "", MSG_BTN_OK, MSG_TYPE_INFO)
                    Case ElitaPlusPage.DetailPageCommand.Expire
                        DisplayMessage(Message.EXPIRE_RECORD_CONFIRMATION, "", MSG_BTN_OK, MSG_TYPE_INFO)
                End Select
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try
        End Sub

        Private Sub SaveGuiState()
            With State
                .Code = moCodeText.Text
                .Description = moDescriptionText.Text
            End With
            PopulateBOProperty(State, "ActiveOnDate", moActiveOnDateText)
            PopulateBOProperty(State, "ruletypeid", ddlRuleType)
            PopulateBOProperty(State, "rulecategoryid", ddlRuleCategory)
        End Sub

        Private Sub RestoreGuiState()
            With State
                moCodeText.Text = .Code
                moDescriptionText.Text = .Description
                PopulateControlFromBOProperty(moActiveOnDateText, .ActiveOnDate)
                PopulateControlFromBOProperty(ddlRuleType, .ruletypeid)
                PopulateControlFromBOProperty(ddlRuleCategory, .rulecategoryid)
            End With

        End Sub

#End Region

#Region "Page_Events"
        Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            Try
                ErrorCtrl.Clear_Hide()
                If Not IsPostBack Then
                    'ValidateDates()
                    AddCalendar(ImageButtonActiveOnDate, moActiveOnDateText)
                    SetDefaultButton(moCodeText, btnSearch)
                    SetDefaultButton(moDescriptionText, btnSearch)
                    ControlMgr.SetVisibleControl(Me, trPageSize, False)
                    PopulateDropDowns()
                    RestoreGuiState()

                    If State.IsGridVisible Then
                        If Not (State.SelectedPageSize = DEFAULT_PAGE_SIZE) Then
                            cboPageSize.SelectedValue = CType(State.SelectedPageSize, String)
                            If (State.SelectedPageSize = 0) Then
                                State.SelectedPageSize = DEFAULT_PAGE_SIZE
                            End If
                            Grid.PageSize = State.SelectedPageSize
                        End If
                        PopulateGrid()
                    End If
                    SetGridItemStyleColor(Grid)
                Else
                    ' Me.SaveGuiState()
                End If
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try
            ShowMissingTranslations(ErrorCtrl)
        End Sub
#End Region

#Region "Controlling Logic"

        Public Sub ValidateDates()
                If moActiveOnDateText.Text.Trim IsNot String.Empty Then
                If (DateHelper.IsDate(moActiveOnDateText.Text.ToString()) = False) Then
                    ElitaPlusPage.SetLabelError(moActiveOnDateLabel)
                    Throw New GUIException(Message.MSG_BEGIN_END_DATE, Message.MSG_INVALID_DATE)
                End If
                End If
        End Sub

        Public Sub PopulateGrid()
            Dim langid As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
            If ((State.SearchDV Is Nothing) OrElse (State.HasDataChanged)) Then
                State.Code = moCodeText.Text
                State.Description = moDescriptionText.Text
                Dim ActiveOn As DateTimeType
                If moActiveOnDateText.Text.Trim = String.Empty Then
                    ActiveOn = Nothing
                Else
                    ActiveOn = DateHelper.GetDateValue(moActiveOnDateText.Text)
                End If
                State.SearchDV = BusObjects.Rule.GetList(State.Code, State.Description, _
                             ActiveOn, New Guid(ddlRuleType.SelectedItem.Value), _
                             New Guid(ddlRuleCategory.SelectedItem.Value), langid)
            End If

            State.SearchDV.Sort = State.SortExpression

            Grid.AutoGenerateColumns = False
            Grid.Columns(GRID_COL_CODE_IDX).SortExpression = BusObjects.Rule.RuleSearchDV.COL_NAME_CODE
            Grid.Columns(GRID_COL_DESCRIPTION_IDX).SortExpression = BusObjects.Rule.RuleSearchDV.COL_NAME_DESCRIPTION
            Grid.Columns(GRID_COL_RULE_TYPE_IDX).SortExpression = BusObjects.Rule.RuleSearchDV.COL_NAME_RULE_TYPE
            Grid.Columns(GRID_COL_RULE_CATEGORY_IDX).SortExpression = BusObjects.Rule.RuleSearchDV.COL_NAME_RULE_CATEGORY
            Grid.Columns(GRID_COL_EFFECTIVE_IDX).SortExpression = BusObjects.Rule.RuleSearchDV.COL_NAME_EFFECTIVE
            Grid.Columns(GRID_COL_EXPIRATION_IDX).SortExpression = BusObjects.Rule.RuleSearchDV.COL_NAME_EXPIRATION

            SetPageAndSelectedIndexFromGuid(State.SearchDV, State.SelectedQuestionId, Grid, State.PageIndex)
            If State.searchClick Then
                ValidSearchResultCount(State.SearchDV.Count, True)
                State.searchClick = False
            End If
            SortAndBindGrid()
        End Sub

        Private Sub SortAndBindGrid()
            State.PageIndex = Grid.CurrentPageIndex
            Grid.DataSource = State.SearchDV
            HighLightSortColumn(Grid, State.SortExpression)
            Grid.DataBind()

            ControlMgr.SetVisibleControl(Me, Grid, State.IsGridVisible)
            ControlMgr.SetVisibleControl(Me, trPageSize, Grid.Visible)

            Session("recCount") = State.SearchDV.Count

            If State.SearchDV.Count > 0 Then
                If Grid.Visible Then
                    lblRecordCount.Text = State.SearchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                End If
            Else
                If Grid.Visible Then
                    lblRecordCount.Text = State.SearchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                End If
            End If
        End Sub

        Private Sub PopulateDropDowns()
            'populate the rule tyep and rule category
            Dim languageId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
            'Me.BindListControlToDataView(ddlRuleType, LookupListNew.GetRuleTypeLookupList(languageId))
            Dim ruleTypeLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("RTYPE", Thread.CurrentPrincipal.GetLanguageCode())
            ddlRuleType.Populate(ruleTypeLkl, New PopulateOptions() With
                                                              {
                                                                .AddBlankItem = True
                                                               })
            'Me.BindListControlToDataView(ddlRuleCategory, LookupListNew.GetRuleCategoryLookupList(languageId))
            Dim ruleCategoryLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("RCAT", Thread.CurrentPrincipal.GetLanguageCode())
            ddlRuleCategory.Populate(ruleCategoryLkl, New PopulateOptions() With
                                                              {
                                                                .AddBlankItem = True
                                                               })
        End Sub
#End Region

#Region " Datagrid Related "

        'The Binding Logic is here  
        Private Sub Grid_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles Grid.ItemDataBound
            Try
                Dim itemType As ListItemType = CType(e.Item.ItemType, ListItemType)
                Dim dvRow As DataRowView = CType(e.Item.DataItem, DataRowView)

                If itemType = ListItemType.Item OrElse itemType = ListItemType.AlternatingItem OrElse itemType = ListItemType.SelectedItem Then
                    e.Item.Cells(GRID_COL_RULE_ID_IDX).Text = GetGuidStringFromByteArray(CType(dvRow(BusObjects.Rule.RuleSearchDV.COL_NAME_RULE_ID), Byte()))
                    e.Item.Cells(GRID_COL_CODE_IDX).Text = dvRow(BusObjects.Rule.RuleSearchDV.COL_NAME_CODE).ToString
                    e.Item.Cells(GRID_COL_DESCRIPTION_IDX).Text = dvRow(BusObjects.Rule.RuleSearchDV.COL_NAME_DESCRIPTION).ToString
                    e.Item.Cells(GRID_COL_RULE_TYPE_IDX).Text = dvRow(BusObjects.Rule.RuleSearchDV.COL_NAME_RULE_TYPE).ToString
                    e.Item.Cells(GRID_COL_RULE_CATEGORY_IDX).Text = dvRow(BusObjects.Rule.RuleSearchDV.COL_NAME_RULE_CATEGORY).ToString
                    e.Item.Cells(GRID_COL_EFFECTIVE_IDX).Text = dvRow(BusObjects.Rule.RuleSearchDV.COL_NAME_EFFECTIVE).ToString
                    e.Item.Cells(GRID_COL_EXPIRATION_IDX).Text = dvRow(BusObjects.Rule.RuleSearchDV.COL_NAME_EXPIRATION).ToString
                End If
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try
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

        Public Sub ItemCommand(source As System.Object, e As System.Web.UI.WebControls.DataGridCommandEventArgs)
            Try
                If e.CommandName = "SelectAction" Then
                    State.SelectedQuestionId = New Guid(e.Item.Cells(GRID_COL_RULE_ID_IDX).Text)
                    callPage(RuleForm.URL, State.SelectedQuestionId)
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try

        End Sub

        Public Sub ItemCreated(sender As System.Object, e As System.Web.UI.WebControls.DataGridItemEventArgs)
            BaseItemCreated(sender, e)
        End Sub

        Private Sub Grid_PageIndexChanged(source As System.Object, e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles Grid.PageIndexChanged
            Try
                State.PageIndex = e.NewPageIndex
                State.SelectedQuestionId = Guid.Empty
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try
        End Sub

#End Region

#Region "Button Clicks "

        Protected Sub btnSearch_Click(sender As Object, e As System.EventArgs) Handles btnSearch.Click
            Try
                State.PageIndex = 0
                State.SelectedQuestionId = Guid.Empty
                State.IsGridVisible = True
                State.SearchDV = Nothing
                State.HasDataChanged = False
                State.searchClick = True
                ValidateDates()
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try
        End Sub

        Protected Sub btnAdd_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnAdd_WRITE.Click
            Try
                callPage(RuleForm.URL)
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try
        End Sub

        Protected Sub btnClearSearch_Click(sender As Object, e As System.EventArgs) Handles btnClearSearch.Click
            Try
                State.ActiveOnDate = Nothing
                State.Code = String.Empty
                State.Description = String.Empty
                State.ruletypeId = Guid.Empty
                State.rulecategoryId = Guid.Empty
                RestoreGuiState()
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try
        End Sub

#End Region

#Region "Error Handling"


#End Region

    End Class

End Namespace