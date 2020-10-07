Namespace Tables

    Partial Class vscModelSearchForm
        Inherits ElitaPlusSearchPage

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub
        'Protected WithEvents ErrorCtrl As ErrorController
        Protected WithEvents MakeModelCtrl As MakeModel

        'NOTE: The following placeholder declaration is required by the Web Form Designer.
        'Do not delete or move it.
        Private designerPlaceholderDeclaration As System.Object

        Private Sub Page_Init(sender As System.Object, e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

#Region " Constants"

        Private Const GRID_COL_MAKE As Integer = 0 '1
        Private Const GRID_COL_MODEL_YEAR As Integer = 1 '2
        Private Const GRID_COL_MODEL As Integer = 2 '3
        Private Const GRID_COL_DESCRIPTION As Integer = 3 '4
        Private Const GRID_COL_NEW_CLASS_CODE As Integer = 4 '5
        Private Const GRID_COL_USED_CLASS_CODE As Integer = 5 '6
        Private Const GRID_COL_ACTIVE_NEW As Integer = 6 '7
        Private Const GRID_COL_ACTIVE_USED As Integer = 7 '8
        Private Const GRID_COL_ENGINE_MONTHS_KM_MI As Integer = 8 '9
        Public Const GRID_COL_MODEL_IDX As Integer = 9 '10

        Private Const YES As String = "Y"
        Private Const NO As String = "N"

#End Region

#Region "Page Return"

        Private Sub Page_PageReturn(ReturnFromUrl As String, ReturnPar As Object) Handles MyBase.PageReturn
            Try
                MenuEnabled = True
                IsReturningFromChild = True
                Dim retObj As vscModelForm.ReturnType = CType(ReturnPar, vscModelForm.ReturnType)
                State.HasDataChanged = retObj.HasDataChanged
                Select Case retObj.LastOperation
                    Case ElitaPlusPage.DetailPageCommand.Back
                        If retObj IsNot Nothing Then
                            If Not retObj.EditingBo.IsNew Then
                                State.SelectedEXDNId = retObj.EditingBo.Id
                            End If
                            State.IsGridVisible = True
                        End If
                End Select
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub
        Public Class ReturnType
            Public LastOperation As ElitaPlusPage.DetailPageCommand
            Public moVSCModelId As Guid
            Public BoChanged As Boolean = False

            Public Sub New(LastOp As ElitaPlusPage.DetailPageCommand, oVSCModelId As Guid, Optional ByVal boChanged As Boolean = False)
                LastOperation = LastOp
                moVSCModelId = oVSCModelId
                Me.BoChanged = boChanged
            End Sub
        End Class
#End Region

#Region " Page Events"

        Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

            'ErrorCtrl.Clear_Hide()
            MasterPage.MessageController.Clear()
            Try
                If Not IsPostBack Then
                    ' Set Master Page Header
                    MasterPage.MessageController.Clear()
                    MasterPage.UsePageTabTitleInBreadCrum = False
                    MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("Tables")
                    UpdateBreadCrum()

                    ControlMgr.SetVisibleControl(Me, trPageSize, False)
                    RestoreGuiState()
                    If State.IsGridVisible Then
                        If Not (State.PageSize = DEFAULT_NEW_UI_PAGE_SIZE) OrElse Not (State.PageSize = Grid.PageSize) Then
                            cboPageSize.SelectedValue = CType(State.PageSize, String)
                            Grid.PageSize = State.PageSize
                        End If
                        PopulateGrid()
                    End If
                    SetGridItemStyleColor(Grid)
                Else
                    SaveGuiState()
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
            ShowMissingTranslations(MasterPage.MessageController)

        End Sub

        Private Sub UpdateBreadCrum()
            If (State IsNot Nothing) Then
                If (State IsNot Nothing) Then
                    MasterPage.BreadCrum = MasterPage.PageTab & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage("VSC_MODEL")
                    MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("VSC_MODEL")
                End If
            End If
        End Sub

        Private Sub moBtnClear_Click(sender As Object, e As System.EventArgs) Handles moBtnClear.Click

            MakeModelCtrl.Reset()

        End Sub

        Private Sub moBtnSearch_Click(sender As Object, e As System.EventArgs) Handles moBtnSearch.Click

            ''''If (Not MakeModelCtrl.Make Is Nothing) AndAlso MakeModelCtrl.Make.Trim.Length > 0 Then
            '''Grid.CurrentPageIndex = NO_PAGE_INDEX
            '''Me.State.IsGridVisible = True
            '''Grid.DataMember = Nothing
            '''State.searchDV = Nothing
            '''PopulateGrid()
            ''''End If

            Try
                State.PageIndex = 0
                State.SelectedEXDNId = Guid.Empty
                If Not State.IsGridVisible Then
                    If Not (Grid.PageSize = DEFAULT_NEW_UI_PAGE_SIZE) Then
                        cboPageSize.SelectedValue = CType(State.PageSize, String)
                        Grid.PageSize = State.PageSize
                    End If
                    State.IsGridVisible = True
                End If
                State.searchDV = Nothing
                State.HasDataChanged = False
                PopulateGrid()

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

#End Region

#Region " Page State"

        Private IsReturningFromChild As Boolean = False

        Class MyState
            Private mnPageIndex As Integer = 0
            Public SortExpression As String = VSCModel.COL_MAKE
            Public SelectedEXDNId As Guid = Guid.Empty
            Public IsGridVisible As Boolean
            '  Private moVSCModelData As VSCModel
            Private mnPageSize As Integer = DEFAULT_NEW_UI_PAGE_SIZE
            Private msPageSort As String
            Public searchDV As VSCModel.VSCModelSearchDV = Nothing
            Public HasDataChanged As Boolean
            Private moSearchDataView As DataView
            Public moManufacturerId As Guid = Guid.Empty
            Public moModel As String = String.Empty
            Public moModelYear As Integer = 0
            Public moDescription As String = String.Empty

            Sub New()
                '  moVSCModelData = New VSCModel
            End Sub
#Region "State-Properties"

            'Public Property MakeId() As String
            '    Get
            '        Return moVSCModelData.ManufacturerId.ToString
            '    End Get
            '    Set(ByVal Value As String)
            '        moVSCModelData.ManufacturerId = New Guid(Value)

            '    End Set
            'End Property

            Public Property MakeId() As String
                Get
                    Return moManufacturerId.ToString
                End Get
                Set(Value As String)
                    moManufacturerId = New Guid(Value)

                End Set
            End Property

            'Public Property Model() As String
            '    Get
            '        Return moVSCModelData.Model
            '    End Get
            '    Set(ByVal Value As String)
            '        moVSCModelData.Model = Value
            '    End Set
            'End Property

            Public Property Model() As String
                Get
                    Return moModel
                End Get
                Set(Value As String)
                    moModel = Value
                End Set
            End Property

            'Public Property Year() As String
            '    Get
            '        Return moVSCModelData.ModelYear.ToString
            '    End Get
            '    Set(ByVal Value As String)
            '        moVSCModelData.ModelYear = Convert.ToInt16(Value)
            '    End Set
            'End Property

            Public Property Year() As String
                Get
                    Return moModelYear.ToString
                End Get
                Set(Value As String)
                    moModelYear = Convert.ToInt16(Value)
                End Set
            End Property

            'Public Property EngineVersion() As String
            '    Get
            '        Return moVSCModelData.Description
            '    End Get
            '    Set(ByVal Value As String)
            '        moVSCModelData.Description = Value
            '    End Set
            'End Property

            Public Property EngineVersion() As String
                Get
                    Return moDescription
                End Get
                Set(Value As String)
                    moDescription = Value
                End Set
            End Property

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
        End Class

        Public Sub New()
            MyBase.New(New MyState)
        End Sub

        Protected Shadows ReadOnly Property State() As MyState
            Get
                Return CType(MyBase.State, MyState)
            End Get
        End Property



        Private Sub SaveGuiState()
            'Me.State.MakeId = MakeModelCtrl.Make
            'Me.State.Model = MakeModelCtrl.Model
            'Me.State.EngineVersion = MakeModelCtrl.EngineVersion
            'Me.State.Year = MakeModelCtrl.Year

            'user control
            MakeModelCtrl.State.makeState = MakeModelCtrl.Make
            MakeModelCtrl.State.ModelState = MakeModelCtrl.Model
            MakeModelCtrl.State.EngineVersionState = MakeModelCtrl.EngineVersion
            MakeModelCtrl.State.YearState = MakeModelCtrl.Year
            MakeModelCtrl.State.coverageSupportState = VSCModel.VAL_BOTH
            If rbShowNew.Checked Then MakeModelCtrl.State.coverageSupportState = VSCModel.VAL_NEW
            If rbShowUsed.Checked Then MakeModelCtrl.State.coverageSupportState = VSCModel.VAL_USED

        End Sub

        Private Sub RestoreGuiState()
            rbShowBoth.Checked = True
            rbShowNew.Checked = False
            rbShowUsed.Checked = False
            If MakeModelCtrl.State.coverageSupportState = VSCModel.VAL_NEW Then
                rbShowNew.Checked = True
                rbShowBoth.Checked = False
                rbShowUsed.Checked = False
            End If

            If MakeModelCtrl.State.coverageSupportState = VSCModel.VAL_USED Then
                rbShowUsed.Checked = True
                rbShowBoth.Checked = False
                rbShowNew.Checked = False
            End If

            'MakeModelCtrl.RebuildDropDowns()
            'Me.SetSelectedItem(moCountryDrop, Me.State.SearchCountryId)
            'Me.TextBoxSearchDescription.Text = Me.State.SearchDescription
            'Me.TextBoxSearchCode.Text = Me.State.SearchCode
        End Sub

#End Region

#Region " Button Clicks "
        Private Sub BtnNew_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles BtnNew_WRITE.Click
            Try
                callPage(vscModelForm.URL)
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub
#End Region

#Region " Private Methods"
        

        Private Sub BindGrid(dw As DataView)

            Grid.DataSource = dw
            Grid.DataBind()

        End Sub

        Private Sub PopulateGrid()

            If ((State.searchDV Is Nothing) OrElse (State.HasDataChanged)) Then

                Dim make As String = String.Empty
                If isEmpty(MakeModelCtrl.Make) Then
                    make = MakeModelCtrl.State.makeState
                Else
                    make = MakeModelCtrl.Make
                End If

                Dim year As String = String.Empty
                If isEmpty(MakeModelCtrl.Year) Then
                    year = MakeModelCtrl.State.YearState
                Else
                    year = MakeModelCtrl.Year
                End If

                Dim model As String = String.Empty
                If isEmpty(MakeModelCtrl.Model) Then
                    model = MakeModelCtrl.State.ModelState
                Else
                    model = MakeModelCtrl.Model
                End If

                Dim trim As String = String.Empty
                If isEmpty(MakeModelCtrl.EngineVersion) Then
                    trim = MakeModelCtrl.State.EngineVersionState
                Else
                    trim = MakeModelCtrl.EngineVersion
                End If


                Dim coverageSupport As String = String.Empty
                If isEmpty(MakeModelCtrl.State.coverageSupportState) Then
                    If rbShowNew.Checked Then coverageSupport = VSCModel.VAL_NEW
                    If rbShowUsed.Checked Then coverageSupport = VSCModel.VAL_USED
                Else
                    coverageSupport = MakeModelCtrl.State.coverageSupportState
                End If

                State.searchDV = VSCModel.getList(make, model, trim, year, coverageSupport)
                ControlMgr.SetVisibleControl(Me, Grid, True)

            End If

            State.searchDV.Sort = State.SortExpression
            Grid.AutoGenerateColumns = False
            Grid.Columns(GRID_COL_MAKE).SortExpression = VSCModel.COL_MAKE
            Grid.Columns(GRID_COL_MODEL_YEAR).SortExpression = VSCModel.COL_YEAR
            Grid.Columns(GRID_COL_MODEL).SortExpression = VSCModel.COL_MODEL
            'Grid.Columns(GRID_COL_TRIM).SortExpression = VSCModel.COL_ENGINE_VERSION


            SetPageAndSelectedIndexFromGuid(State.searchDV, State.SelectedEXDNId, Grid, State.PageIndex)
            SortAndBindGrid()






            'BindGrid(State.searchDV)

            'ControlMgr.SetVisibleControl(Me, trPageSize, Grid.Visible)

            'Session("recCount") = Me.State.searchDV.Count

            'If Grid.Visible Then
            '    Me.lblRecordCount.Text = Me.State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
            'End If






        End Sub

        Private Function isEmpty(val As String) As Boolean

            Return (val Is Nothing) OrElse val.Trim.Length = 0

        End Function

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

#Region " Datagrid Related "


        Private Sub Grid_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles Grid.PageIndexChanged

            Try
                State.PageIndex = e.NewPageIndex
                State.SelectedEXDNId = Guid.Empty
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub Grid_ItemCreated(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles Grid.ItemCreated
            Try
                BaseItemCreated(sender, e)
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub Grid_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles Grid.ItemDataBound
            Dim itemType As ListItemType = CType(e.Item.ItemType, ListItemType)
            Dim dvRow As DataRowView = CType(e.Item.DataItem, DataRowView)

            If itemType = ListItemType.Item OrElse itemType = ListItemType.AlternatingItem OrElse itemType = ListItemType.SelectedItem Then
                Dim active_New As String = e.Item.Cells(GRID_COL_ACTIVE_NEW).Text
                Dim active_Used As String = e.Item.Cells(GRID_COL_ACTIVE_USED).Text
                e.Item.Cells(GRID_COL_MODEL_IDX).Text = GetGuidStringFromByteArray(CType(dvRow(VSCModel.VSCModelSearchDV.COL_NAME_MODEL_ID), Byte()))
                If active_New = NO Then
                    e.Item.Cells(GRID_COL_NEW_CLASS_CODE).Enabled = False
                End If


                If active_Used = NO Then
                    e.Item.Cells(GRID_COL_USED_CLASS_CODE).Enabled = False
                End If

            End If

        End Sub


        Private Sub Grid_ItemCommand(source As Object, e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles Grid.ItemCommand
            Try
                If e.CommandName = "SelectAction" Then
                    State.SelectedEXDNId = New Guid(e.Item.Cells(GRID_COL_MODEL_IDX).Text)
                    callPage(vscModelForm.URL, State.SelectedEXDNId)
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub Grid_SortCommand(source As Object, e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles Grid.SortCommand
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
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub cboPageSize_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
            Try
                Grid.CurrentPageIndex = NewCurrentPageIndex(Grid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
                State.PageSize = Grid.PageSize
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub
#End Region

#Region "State-Management"

        Private Sub SetSession()
            With State
                .MakeId = MakeModelCtrl.Make
                .Model = MakeModelCtrl.Model
                .EngineVersion = MakeModelCtrl.EngineVersion
                .Year = MakeModelCtrl.Year
                .PageIndex = Grid.CurrentPageIndex
                .PageSize = Grid.PageSize
                .PageSort = State.SortExpression
                .SearchDataView = State.searchDV
            End With
        End Sub

#End Region

    End Class
End Namespace
