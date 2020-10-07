Imports System.Threading
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Imports Assurant.ElitaPlus.Security

Namespace Tables

    Partial Class TemplateGroupSearchForm
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
        Private Const TEMPLATEGROUP_LIST_FORM001 As String = "TEMPLATEGROUP_LIST_FORM001" 'Maintain Template Group List Exception
        Private Const LABEL_DEALER As String = "DEALER"

        Public Const GRID_TOTAL_COLUMNS As Integer = 6

        Public Const GRID_COL_EDIT_IDX As Integer = 0
        Public Const GRID_COL_EDIT2_IDX As Integer = 2

        Public Const GRID_COL_TEMPLATE_GROUP_CODE As Integer = 0
        Public Const GRID_COL_TEMPLATE_GROUP_DESCRIPTION As Integer = 1
        Public Const GRID_COL_TEMPLATE_CODE As Integer = 2
        Public Const GRID_COL_TEMPLATE_DESCRIPTION As Integer = 3

        Public Const GRID_COL_TEMPLATE_GROUP_ID As Integer = 4
        Public Const GRID_COL_TEMPLATE_ID As Integer = 5
        Public Const GRID_COL_TEMPLATE_TYPE As Integer = 6
#End Region

#Region "Page State"
        ' This class keeps the current state for the search page.
        Class MyState
            Public IsGridVisible As Boolean = False
            Public SearchDV As OcTemplate.TemplateSearchDV = Nothing
            Public SortExpression As String = OcTemplate.TemplateSearchDV.COL_TEMPLATE_GROUP_CODE
            Public PageIndex As Integer = 0
            Public PageSort As String
            Public PageSize As Integer = 15

            Public TemplateGroupId As Guid = Guid.Empty
            Public TemplateId As Guid = Guid.Empty

            Public DealerId As Guid = Guid.Empty
            Public TemplateGroupCodeMask As String
            Public TemplateCodeMask As String

            Public bNoRow As Boolean = False

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
#End Region

#Region "Page Return"
        Private IsReturningFromChild As Boolean = False

        Private Sub HandleReturnFromTemplateGroupForm(ReturnPar As Object)
            Dim retObj As TemplateGroupForm.ReturnType = CType(ReturnPar, TemplateGroupForm.ReturnType)

            If retObj IsNot Nothing AndAlso retObj.HasDataChanged Then
                State.SearchDV = Nothing
            End If

            If retObj IsNot Nothing Then
                Select Case retObj.LastOperation
                    Case ElitaPlusPage.DetailPageCommand.Back
                        If Not retObj.TemplateGroupId = Guid.Empty Then
                            State.TemplateGroupId = retObj.TemplateGroupId
                            State.IsGridVisible = True
                        End If
                    Case ElitaPlusPage.DetailPageCommand.Delete
                        AddInfoMsg(Message.DELETE_RECORD_CONFIRMATION)
                        State.TemplateGroupId = Guid.Empty
                    Case Else
                        State.TemplateGroupId = Guid.Empty
                End Select

                Grid.PageIndex = State.PageIndex
                cboPageSize.SelectedValue = CType(State.PageSize, String)
                Grid.PageSize = State.PageSize
                ControlMgr.SetVisibleControl(Me, trPageSize, Grid.Visible)
            End If
        End Sub

        Private Sub HandleReturnFromTemplateForm(ReturnPar As Object)
            Dim retObj As TemplateForm.ReturnType = CType(ReturnPar, TemplateForm.ReturnType)

            If retObj IsNot Nothing AndAlso retObj.HasDataChanged Then
                State.SearchDV = Nothing
            End If

            If retObj IsNot Nothing Then
                Select Case retObj.LastOperation
                    Case ElitaPlusPage.DetailPageCommand.Back
                        If Not retObj.TemplateId = Guid.Empty Then
                            State.TemplateId = retObj.TemplateId
                            State.IsGridVisible = True
                        End If
                    Case ElitaPlusPage.DetailPageCommand.Delete
                        AddInfoMsg(Message.DELETE_RECORD_CONFIRMATION)
                        State.TemplateId = Guid.Empty
                    Case Else
                        State.TemplateId = Guid.Empty
                End Select

                Grid.PageIndex = State.PageIndex
                cboPageSize.SelectedValue = CType(State.PageSize, String)
                Grid.PageSize = State.PageSize
                ControlMgr.SetVisibleControl(Me, trPageSize, Grid.Visible)
            End If
        End Sub

        Private Sub Page_PageReturn(ReturnFromUrl As String, ReturnPar As Object) Handles Me.PageReturn
            Try
                MenuEnabled = True
                IsReturningFromChild = True

                If TypeOf ReturnPar Is TemplateGroupForm.ReturnType Then
                    HandleReturnFromTemplateGroupForm(ReturnPar)
                ElseIf TypeOf ReturnPar Is TemplateForm.ReturnType Then
                    HandleReturnFromTemplateForm(ReturnPar)
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Public Class ReturnType
            Public LastOperation As ElitaPlusPage.DetailPageCommand
            Public TemplateGroupCodeId As Guid
            Public TemplateCodeId As Guid
            Public HasDataChanged As Boolean = False

            Public Sub New(LastOp As ElitaPlusPage.DetailPageCommand, TemplateGroupCodeId As Guid, TemplateCodeId As Guid, Optional ByVal HasDataChanged As Boolean = False)
                LastOperation = LastOp
                Me.TemplateGroupCodeId = TemplateGroupCodeId
                Me.TemplateCodeId = TemplateCodeId
                Me.HasDataChanged = HasDataChanged
            End Sub
        End Class
#End Region

#Region "Page_Events"
        Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
            Try
                MasterPage.MessageController.Clear_Hide()

                If Not Page.IsPostBack Then
                    MasterPage.MessageController.Clear()
                    MasterPage.UsePageTabTitleInBreadCrum = False
                    MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("Tables")

                    TranslateGridHeader(Grid)
                    UpdateBreadCrum()

                    ControlMgr.SetVisibleControl(Me, trPageSize, False)
                    SortDirection = State.SortExpression
                    PopulateDropdown()

                    If State.IsGridVisible Then
                        If Not (State.PageSize = DEFAULT_NEW_UI_PAGE_SIZE) OrElse Not (State.PageSize = Grid.PageSize) Then
                            Grid.PageSize = State.PageSize
                        End If

                        cboPageSize.SelectedValue = CType(State.PageSize, String)
                        PopulateGrid()
                    End If

                    SetGridItemStyleColor(Grid)
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

            ShowMissingTranslations(MasterPage.MessageController)
        End Sub
#End Region

#Region "Controlling Logic"
        Private Sub PopulateDropdown()
            PopulateDealer()
            PopulateTemplateGroupCode()
            PopulateTemplateCode()
        End Sub

        Private Sub PopulateDealer()
            Try
                DealerMultipleDrop.Caption = TranslationBase.TranslateLabelOrMessage(LABEL_DEALER)
                DealerMultipleDrop.NothingSelected = True
                DealerMultipleDrop.AutoPostBackDD = False
                DealerMultipleDrop.NothingSelected = True
                DealerMultipleDrop.SelectedGuid = State.DealerId
                DealerMultipleDrop.BindData(LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies))
            Catch ex As Exception
                MasterPage.MessageController.AddError(TEMPLATEGROUP_LIST_FORM001)
                MasterPage.MessageController.AddError(ex.Message, False)
                MasterPage.MessageController.Show()
            End Try
        End Sub

        Private Sub PopulateTemplateGroupCode()
            txtTemplateGroupCode.Text = State.TemplateGroupCodeMask
        End Sub

        Private Sub PopulateTemplateCode()
            txtTemplateCode.Text = State.TemplateCodeMask
        End Sub

        Private Sub BindDataGrid(oDataView As DataView)
            Grid.DataSource = oDataView
            Grid.DataBind()
        End Sub

        Private Sub PopulateGrid(Optional ByVal oAction As String = POPULATE_ACTION_NONE)
            Dim oDataView As DataView

            Try
                If (State.SearchDV Is Nothing) Then
                    State.SearchDV = OcTemplate.GetTemplateSearchDV(ElitaPlusIdentity.Current.ActiveUser.Companies, DealerMultipleDrop.SelectedGuid, State.TemplateGroupCodeMask, State.TemplateCodeMask)
                End If

                If (State.SearchDV.Count = 0) Then
                    State.bNoRow = True
                    CreateHeaderForEmptyGrid(Grid, SortDirection)
                Else
                    State.bNoRow = False
                    Grid.Enabled = True
                End If

                State.SearchDV.Sort = State.SortExpression
                Grid.AutoGenerateColumns = False

                Grid.Columns(GRID_COL_TEMPLATE_GROUP_CODE).SortExpression = OcTemplate.TemplateSearchDV.COL_TEMPLATE_GROUP_CODE
                Grid.Columns(GRID_COL_TEMPLATE_GROUP_DESCRIPTION).SortExpression = OcTemplate.TemplateSearchDV.COL_TEMPLATE_GROUP_DESCRIPTION
                Grid.Columns(GRID_COL_TEMPLATE_CODE).SortExpression = OcTemplate.TemplateSearchDV.COL_TEMPLATE_CODE
                Grid.Columns(GRID_COL_TEMPLATE_DESCRIPTION).SortExpression = OcTemplate.TemplateSearchDV.COL_TEMPLATE_DESCRIPTION

                HighLightSortColumn(Grid, State.SortExpression)

                SetPageAndSelectedIndexFromGuid(State.SearchDV, State.DealerId, Grid, State.PageIndex)

                Grid.DataSource = State.SearchDV
                HighLightSortColumn(Grid, SortDirection)
                Grid.DataBind()

                ControlMgr.SetVisibleControl(Me, trPageSize, Grid.Visible)

                Session("recCount") = State.SearchDV.Count

                If Not Grid.BottomPagerRow.Visible Then Grid.BottomPagerRow.Visible = True

                lblRecordCount.Text = State.SearchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub ClearSearch()
            DealerMultipleDrop.SelectedIndex = 0
            txtTemplateGroupCode.Text = Nothing
            txtTemplateCode.Text = Nothing
            State.TemplateGroupId = Guid.Empty
            State.TemplateId = Guid.Empty
        End Sub

        Private Sub UpdateBreadCrum()
            If (State IsNot Nothing) Then
                If (State IsNot Nothing) Then
                    MasterPage.BreadCrum = MasterPage.PageTab & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage("TEMPLATE_SEARCH")
                    MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("TEMPLATE_SEARCH")
                End If
            End If
        End Sub
#End Region

#Region "Datagrid Related"
        Public Property SortDirection() As String
            Get
                Return ViewState("SortDirection").ToString
            End Get
            Set(value As String)
                ViewState("SortDirection") = value
            End Set
        End Property

        Private Sub Grid_PageSizeChanged(source As Object, e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
            Try
                Grid.PageIndex = NewCurrentPageIndex(Grid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
                State.PageIndex = Grid.PageSize
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub Grid_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Grid.PageIndexChanging
            Try
                State.PageIndex = e.NewPageIndex
                State.DealerId = Guid.Empty
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Public Sub RowCreated(sender As System.Object, e As System.Web.UI.WebControls.GridViewRowEventArgs)
            BaseItemCreated(sender, e)
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
                State.SortExpression = SortDirection
                State.PageIndex = 0
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private _listitem() As Assurant.Elita.CommonConfiguration.DataElements.ListItem

        Private Function GetTemplateTypeByCode(strTemplateTypeXcd As String) As String
            If _listitem Is Nothing Then
                _listitem = CommonConfigManager.Current.ListManager.GetList(listCode:="OC_TEMP_TYPE", languageCode:=Thread.CurrentPrincipal.GetLanguageCode())
            End If

            Dim strResult As String = String.Empty

            For i As Integer = 0 To _listitem.Count - 1
                If _listitem(i).ExtendedCode = strTemplateTypeXcd Then
                    strResult = _listitem(i).Translation
                    Exit For
                End If
            Next
            Return strResult
        End Function
        Private Sub Grid_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowDataBound
            Try
                Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
                Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
                Dim btnEditItem As LinkButton
                Dim btnEditItem2 As LinkButton

                If dvRow IsNot Nothing AndAlso Not State.bNoRow Then
                    If itemType = ListItemType.Item OrElse itemType = ListItemType.AlternatingItem OrElse itemType = ListItemType.SelectedItem Then
                        btnEditItem = CType(e.Row.Cells(GRID_COL_EDIT_IDX).FindControl("SelectAction"), LinkButton)
                        btnEditItem.Text = dvRow(OcTemplate.TemplateSearchDV.COL_TEMPLATE_GROUP_CODE).ToString

                        e.Row.Cells(GRID_COL_TEMPLATE_GROUP_DESCRIPTION).Text = dvRow(OcTemplate.TemplateSearchDV.COL_TEMPLATE_GROUP_DESCRIPTION).ToString
                        e.Row.Cells(GRID_COL_TEMPLATE_GROUP_ID).Text = GetGuidStringFromByteArray(CType(dvRow(OcTemplate.TemplateSearchDV.COL_TEMPLATE_GROUP_ID), Byte()))

                        e.Row.Cells(GRID_COL_TEMPLATE_TYPE).Text = GetTemplateTypeByCode(dvRow(OcTemplate.TemplateSearchDV.COL_TEMPLATE_TYPE_XCD).ToString)

                        If Not Convert.IsDBNull(dvRow(OcTemplate.TemplateSearchDV.COL_TEMPLATE_ID)) Then
                            btnEditItem2 = CType(e.Row.Cells(GRID_COL_EDIT2_IDX).FindControl("SelectAction2"), LinkButton)
                            btnEditItem2.Text = dvRow(OcTemplate.TemplateSearchDV.COL_TEMPLATE_CODE).ToString
                            e.Row.Cells(GRID_COL_TEMPLATE_DESCRIPTION).Text = dvRow(OcTemplate.TemplateSearchDV.COL_TEMPLATE_DESCRIPTION).ToString
                            e.Row.Cells(GRID_COL_TEMPLATE_ID).Text = GetGuidStringFromByteArray(CType(dvRow(OcTemplate.TemplateSearchDV.COL_TEMPLATE_ID), Byte()))
                        Else
                            e.Row.Cells(GRID_COL_EDIT2_IDX).Text = String.Empty
                            e.Row.Cells(GRID_COL_TEMPLATE_ID).Text = String.Empty
                        End If

                    End If
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Public Sub RowCommand(source As System.Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs)
            Try
                Dim index As Integer = CInt(e.CommandArgument)
                Dim templateGroupId = New Guid(Grid.Rows(index).Cells(GRID_COL_TEMPLATE_GROUP_ID).Text)

                If e.CommandName = "SelectAction" Then
                    State.TemplateGroupId = templateGroupId
                    callPage(TemplateGroupForm.URL, State.TemplateGroupId)
                ElseIf e.CommandName = "SelectAction2" Then
                    State.TemplateId = New Guid(Grid.Rows(index).Cells(GRID_COL_TEMPLATE_ID).Text)
                    callPage(TemplateForm.URL, New TemplateForm.CallType(State.TemplateId, templateGroupId))
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub
#End Region

#Region "Button Clicks"
        Private Sub moBtnSearch_Click(sender As System.Object, e As System.EventArgs) Handles moBtnSearch.Click
            Try
                If Not State.IsGridVisible Then
                    cboPageSize.SelectedValue = CType(State.PageSize, String)
                    If Not (Grid.PageSize = DEFAULT_NEW_UI_PAGE_SIZE) Then
                        cboPageSize.SelectedValue = CType(State.PageSize, String)
                        Grid.PageSize = State.PageSize
                    End If
                    State.IsGridVisible = True
                End If
                Grid.PageIndex = NO_PAGE_INDEX
                Grid.DataMember = Nothing
                State.SearchDV = Nothing
                SetSession()
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub moBtnClear_Click(sender As System.Object, e As System.EventArgs) Handles moBtnClear.Click
            Try
                ClearSearch()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnNew_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnNew_WRITE.Click
            Try
                State.TemplateGroupId = Guid.Empty
                SetSession()
                callPage(TemplateGroupForm.URL, State.TemplateGroupId)
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub
#End Region

#Region "State-Management"
        Private Sub SetSession()
            With State
                .TemplateGroupCodeMask = txtTemplateGroupCode.Text.ToUpper
                .TemplateCodeMask = txtTemplateCode.Text.ToUpper
                .DealerId = DealerMultipleDrop.SelectedGuid
                .PageIndex = Grid.PageIndex
                .PageSize = Grid.PageSize
                .PageSort = State.SortExpression
            End With
        End Sub
#End Region
    End Class
End Namespace
