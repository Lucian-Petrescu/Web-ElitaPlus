Namespace Tables

    Partial Class EarningPatternListForm
        Inherits ElitaPlusSearchPage

#Region "Page State"

        Class MyState

            Public IsGridVisible As Boolean = False
            Public searchDV As DataView = Nothing
            Public searchBtnClicked As Boolean = False
            Public SortExpression As String = EarningPattern.COL_CODE
            Public boChangedStr As String = "FALSE"
            Public moEarningPatternData As EarningPattern
            Public mnPageIndex As Integer = 0
            Public msPageSort As String
            Public mnPageSize As Integer = DEFAULT_PAGE_SIZE
            Private moSearchDataView As DataView
            Public moEarningPatternId As Guid = Guid.Empty
            Public SortColumns(GRID_TOTAL_COLUMNS - 1) As String
            Public IsSortDesc(GRID_TOTAL_COLUMNS - 1) As Boolean
            Public EarningPatternId As Guid = Guid.Empty
            Public EarningPatternName As String = String.Empty
            Public EarningPatternCode As String = String.Empty
            Public Effective As DateType
            Public Expiration As DateType
            Public bnoRow As Boolean = False

#Region "State-Properties"

            'Public Property EarningPatternId() As Guid
            '    Get
            '        Return moEarningPatternData.Id
            '    End Get
            '    Set(ByVal Value As Guid)
            '        'moEarningPatternData.Id = Value
            '    End Set
            'End Property

            'Public ReadOnly Property EarningPatternName() As String
            '    Get
            '        Return moEarningPatternData.Description
            '    End Get
            'End Property

            'Public ReadOnly Property EarningPatternCode() As String
            '    Get
            '        Return moEarningPatternData.Code
            '    End Get
            'End Property

            'Public Property Effective() As DateType
            '    Get
            '        Return moEarningPatternData.Effective
            '    End Get
            '    Set(ByVal Value As DateType)
            '        moEarningPatternData.Effective = Value
            '    End Set
            'End Property

            'Public Property Expiration() As DateType
            '    Get
            '        Return moEarningPatternData.Expiration
            '    End Get
            '    Set(ByVal Value As DateType)
            '        moEarningPatternData.Expiration = Value
            '    End Set
            'End Property

            Public Property PageIndex() As Integer
                Get
                    Return mnPageIndex
                End Get
                Set(Value As Integer)
                    mnPageIndex = Value
                End Set
            End Property

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

            Public Property SearchDataView() As DataView
                Get
                    Return moSearchDataView
                End Get
                Set(Value As DataView)
                    moSearchDataView = Value
                End Set
            End Property
#End Region

            Sub New()
                SortColumns(GRID_COL_EARNING_PATTERN_IDX) = EarningPattern.COL_EARNING_PATTERN_ID
                SortColumns(GRID_COL_DESCRIPTION) = EarningPattern.COL_DESCRIPTION
                SortColumns(GRID_COL_CODE) = EarningPattern.COL_CODE
                SortColumns(GRID_COL_EFFECTIVE) = EarningPattern.COL_EFFECTIVE
                SortColumns(GRID_COL_EXPIRATION) = EarningPattern.COL_EXPIRATION

                IsSortDesc(GRID_COL_DESCRIPTION) = False
                IsSortDesc(GRID_COL_CODE) = False
                IsSortDesc(GRID_COL_EFFECTIVE) = False
                IsSortDesc(GRID_COL_EXPIRATION) = False
                '  moEarningPatternData = New EarningPattern
            End Sub

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

#Region "Page Return"

        Private IsReturningFromChild As Boolean = False

        Public Sub Page_PageReturn(ReturnFromUrl As String, ReturnPar As Object) Handles MyBase.PageReturn
            Try
                IsReturningFromChild = True
                If State.searchDV Is Nothing Then
                    State.IsGridVisible = False
                Else
                    State.IsGridVisible = True
                End If
                Dim retObj As ReturnType = CType(ReturnPar, ReturnType)
                If retObj IsNot Nothing AndAlso retObj.BoChanged Then
                    State.searchDV = Nothing
                End If
                If retObj IsNot Nothing Then

                    Select Case retObj.LastOperation
                        Case ElitaPlusPage.DetailPageCommand.Back
                            State.moEarningPatternId = retObj.moEarningPatternId
                        Case Else
                            State.moEarningPatternId = Guid.Empty
                    End Select
                    If State.IsGridVisible Then
                        moDataGrid.PageIndex = State.PageIndex
                        moDataGrid.PageSize = State.PageSize
                        cboPageSize.SelectedValue = CType(State.PageSize, String)
                        moDataGrid.PageSize = State.PageSize
                        ControlMgr.SetVisibleControl(Me, trPageSize, moDataGrid.Visible)
                    End If
                End If
            Catch ex As Exception
                HandleErrors(ex, moErrorController)
            End Try
        End Sub

        Public Class ReturnType
            Public LastOperation As ElitaPlusPage.DetailPageCommand
            Public moEarningPatternId As Guid
            Public BoChanged As Boolean = False

            Public Sub New(LastOp As ElitaPlusPage.DetailPageCommand, oEarningPatternId As Guid, Optional ByVal boChanged As Boolean = False)
                LastOperation = LastOp
                moEarningPatternId = oEarningPatternId
                Me.BoChanged = boChanged
            End Sub


        End Class

#End Region

#End Region

#Region "Constants"
        Public Const GRID_TOTAL_COLUMNS As Integer = 6
        Public Const GRID_COL_EDIT_IDX As Integer = 0
        Public Const GRID_COL_EARNING_PATTERN_IDX As Integer = 1
        Public Const GRID_COL_CODE As Integer = 2
        Public Const GRID_COL_DESCRIPTION As Integer = 3
        Public Const GRID_COL_EFFECTIVE As Integer = 4
        Public Const GRID_COL_EXPIRATION As Integer = 5
#End Region

#Region "Handlers"

#Region "Handlers-Init"

        Protected WithEvents moErrorController As ErrorController

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
            'Put user code to initialize the page here
            Try
                moErrorController.Clear_Hide()
                If Not Page.IsPostBack Then
                    SortDirection = State.SortExpression
                    ControlMgr.SetVisibleControl(Me, trPageSize, False)
                    SetGridItemStyleColor(moDataGrid)
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
                End If

            Catch ex As Exception
                HandleErrors(ex, moErrorController)
            End Try
            ShowMissingTranslations(moErrorController)
        End Sub

#End Region

#Region "Handlers-Buttons"

        Private Sub moBtnSearch_Click(sender As System.Object, e As System.EventArgs) Handles moBtnSearch.Click
            Try
                moDataGrid.PageIndex = NO_PAGE_INDEX
                moDataGrid.DataMember = Nothing
                State.searchDV = Nothing
                State.searchBtnClicked = True
                PopulateGrid()
                State.searchBtnClicked = False
            Catch ex As Exception
                HandleErrors(ex, moErrorController)
            End Try
        End Sub

        Private Sub moBtnClear_Click(sender As System.Object, e As System.EventArgs) Handles moBtnClear.Click
            Try
                ClearSearch()
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, moErrorController)
            End Try
        End Sub

        Private Sub BtnNew_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles BtnNew_WRITE.Click
            Try
                State.moEarningPatternId = Guid.Empty
                SetSession()
                callPage(EarningPatternForm.URL, State.moEarningPatternId)
            Catch ex As Exception
                HandleErrors(ex, moErrorController)
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
        'The Binding LOgic is here
        Private Sub moDataGrid_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles moDataGrid.RowDataBound
            Try

                Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
                Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
                If dvRow IsNot Nothing AndAlso Not State.bnoRow Then
                    If itemType = ListItemType.Item OrElse itemType = ListItemType.AlternatingItem OrElse itemType = ListItemType.SelectedItem Then
                        e.Row.Cells(GRID_COL_EARNING_PATTERN_IDX).Text = GetGuidStringFromByteArray(CType(dvRow(Assurant.ElitaPlus.BusinessObjectsNew.EarningPattern.COL_EARNING_PATTERN_ID), Byte()))
                        e.Row.Cells(GRID_COL_DESCRIPTION).Text = dvRow(Assurant.ElitaPlus.BusinessObjectsNew.EarningPattern.COL_DESCRIPTION).ToString
                        e.Row.Cells(GRID_COL_CODE).Text = dvRow(Assurant.ElitaPlus.BusinessObjectsNew.EarningPattern.COL_CODE).ToString
                        e.Row.Cells(GRID_COL_EFFECTIVE).Text = dvRow(Assurant.ElitaPlus.BusinessObjectsNew.EarningPattern.COL_EFFECTIVE).ToString
                        e.Row.Cells(GRID_COL_EXPIRATION).Text = dvRow(Assurant.ElitaPlus.BusinessObjectsNew.EarningPattern.COL_EXPIRATION).ToString
                    End If
                End If
            Catch ex As Exception
                HandleErrors(ex, moErrorController)
            End Try
        End Sub
        Private Sub moDataGrid_PageIndexChanged(source As System.Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles moDataGrid.PageIndexChanging
            Try
                moDataGrid.PageIndex = e.NewPageIndex
                PopulateGrid(POPULATE_ACTION_NO_EDIT)
            Catch ex As Exception
                HandleErrors(ex, moErrorController)
            End Try
        End Sub

        Public Sub ItemCreated(sender As Object, e As GridViewRowEventArgs)
            Try
                BaseItemCreated(sender, e)
            Catch ex As Exception
                HandleErrors(ex, moErrorController)
            End Try
        End Sub

        Sub ItemCommand(source As System.Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs)

            Try
                If e.CommandSource.GetType.Equals(GetType(ImageButton)) Then
                    Dim index As Integer = CInt(e.CommandArgument)
                    State.moEarningPatternId = New Guid(moDataGrid.Rows(index).Cells(GRID_COL_EARNING_PATTERN_IDX).Text)
                    SetSession()
                    callPage(EarningPatternForm.URL, State.moEarningPatternId)
                End If
            Catch ex As Exception
                HandleErrors(ex, moErrorController)
            End Try
        End Sub

        Private Sub moDataGrid_SortCommand(source As Object, e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles moDataGrid.Sorting
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
                State.SortExpression = SortDirection
                State.PageIndex = 0
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, moErrorController)
            End Try
        End Sub
#End Region

#End Region

#Region "Populate"

        Public Function CheckGuidValue(gd As Byte()) As Byte()

            Dim guid As System.Guid = System.Guid.NewGuid

            If gd Is Nothing Then
                Return guid.ToByteArray()
            End If
            Return gd
        End Function

        Private Sub BindDataGrid(oDataView As DataView)
            'moDataGrid.DataSource = oDataView
            'moDataGrid.DataBind()
            State.PageIndex = moDataGrid.PageIndex

            If (State.searchDV.Count = 0) Then

                State.bnoRow = True
                CreateHeaderForEmptyGrid(moDataGrid, SortDirection)
            Else
                State.bnoRow = False
                moDataGrid.Enabled = True
                moDataGrid.DataSource = oDataView
                HighLightSortColumn(moDataGrid, SortDirection)
                moDataGrid.DataBind()
            End If
            If Not moDataGrid.BottomPagerRow.Visible Then moDataGrid.BottomPagerRow.Visible = True
        End Sub

        Private Sub PopulateGrid(Optional ByVal oAction As String = POPULATE_ACTION_NONE)
            Dim oDataView As DataView

            Try
                If (State.searchDV Is Nothing) Then
                    State.searchDV = GetDataView()
                    ControlMgr.SetVisibleControl(Me, moDataGrid, True)
                End If

                State.searchDV.Sort = State.SortExpression
                moDataGrid.AutoGenerateColumns = False
                HighLightSortColumn(moDataGrid, State.SortExpression)
                BasePopulateGrid(moDataGrid, State.searchDV, State.moEarningPatternId, oAction)
                ControlMgr.SetVisibleControl(Me, trPageSize, moDataGrid.Visible)
                Session("recCount") = State.searchDV.Count

                If moDataGrid.Visible Then
                    lblRecordCount.Text = State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                End If
                BindDataGrid(State.searchDV)
            Catch ex As Exception
                HandleErrors(ex, moErrorController)
            End Try
        End Sub

        Private Sub Grid_PageSizeChanged(source As System.Object, e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
            Try
                moDataGrid.PageIndex = NewCurrentPageIndex(moDataGrid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, moErrorController)
            End Try
        End Sub

#End Region

#Region "Clear"

        Private Sub ClearSearch()
            'moDataGrid.CurrentPageIndex = 0
            SearchDescriptionTextBox.Text = Nothing
            SearchCodeTextBox.Text = Nothing
            ' moDataGrid.DataSource = Nothing
            'moDataGrid.DataBind()
            ' ControlMgr.SetVisibleControl(Me, trPageSize, False)
            ' Me.State.moEarningPatternId = Guid.Empty
        End Sub

#End Region

#Region "State-Management"

        Private Sub SetSession()
            With State
                .PageIndex = moDataGrid.PageIndex
                .PageSort = State.SortExpression
                .PageSize = moDataGrid.PageSize
                .SearchDataView = State.searchDV
            End With
        End Sub

#End Region

#Region "Business Part"

        Private Function GetDataView() As DataView
            Dim oEarningPattern As EarningPattern = New EarningPattern
            Dim oCompanyGroupId As Guid = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
            Dim oDescription As String = SearchDescriptionTextBox.Text
            Dim oCode As String = SearchCodeTextBox.Text

            Return oEarningPattern.GetList(oDescription, oCode, oCompanyGroupId)

        End Function

#End Region

    End Class

End Namespace
