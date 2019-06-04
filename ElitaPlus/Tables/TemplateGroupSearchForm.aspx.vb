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

        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
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

        Private Sub HandleReturnFromTemplateGroupForm(ByVal ReturnPar As Object)
            Dim retObj As TemplateGroupForm.ReturnType = CType(ReturnPar, TemplateGroupForm.ReturnType)

            If Not retObj Is Nothing AndAlso retObj.HasDataChanged Then
                Me.State.SearchDV = Nothing
            End If

            If Not retObj Is Nothing Then
                Select Case retObj.LastOperation
                    Case ElitaPlusPage.DetailPageCommand.Back
                        If Not retObj.TemplateGroupId = Guid.Empty Then
                            Me.State.TemplateGroupId = retObj.TemplateGroupId
                            Me.State.IsGridVisible = True
                        End If
                    Case ElitaPlusPage.DetailPageCommand.Delete
                        Me.AddInfoMsg(Message.DELETE_RECORD_CONFIRMATION)
                        Me.State.TemplateGroupId = Guid.Empty
                    Case Else
                        Me.State.TemplateGroupId = Guid.Empty
                End Select

                Grid.PageIndex = Me.State.PageIndex
                cboPageSize.SelectedValue = CType(Me.State.PageSize, String)
                Grid.PageSize = Me.State.PageSize
                ControlMgr.SetVisibleControl(Me, trPageSize, Grid.Visible)
            End If
        End Sub

        Private Sub HandleReturnFromTemplateForm(ByVal ReturnPar As Object)
            Dim retObj As TemplateForm.ReturnType = CType(ReturnPar, TemplateForm.ReturnType)

            If Not retObj Is Nothing AndAlso retObj.HasDataChanged Then
                Me.State.SearchDV = Nothing
            End If

            If Not retObj Is Nothing Then
                Select Case retObj.LastOperation
                    Case ElitaPlusPage.DetailPageCommand.Back
                        If Not retObj.TemplateId = Guid.Empty Then
                            Me.State.TemplateId = retObj.TemplateId
                            Me.State.IsGridVisible = True
                        End If
                    Case ElitaPlusPage.DetailPageCommand.Delete
                        Me.AddInfoMsg(Message.DELETE_RECORD_CONFIRMATION)
                        Me.State.TemplateId = Guid.Empty
                    Case Else
                        Me.State.TemplateId = Guid.Empty
                End Select

                Grid.PageIndex = Me.State.PageIndex
                cboPageSize.SelectedValue = CType(Me.State.PageSize, String)
                Grid.PageSize = Me.State.PageSize
                ControlMgr.SetVisibleControl(Me, trPageSize, Grid.Visible)
            End If
        End Sub

        Private Sub Page_PageReturn(ByVal ReturnFromUrl As String, ByVal ReturnPar As Object) Handles Me.PageReturn
            Try
                Me.MenuEnabled = True
                Me.IsReturningFromChild = True

                If TypeOf ReturnPar Is TemplateGroupForm.ReturnType Then
                    HandleReturnFromTemplateGroupForm(ReturnPar)
                ElseIf TypeOf ReturnPar Is TemplateForm.ReturnType Then
                    HandleReturnFromTemplateForm(ReturnPar)
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Public Class ReturnType
            Public LastOperation As ElitaPlusPage.DetailPageCommand
            Public TemplateGroupCodeId As Guid
            Public TemplateCodeId As Guid
            Public HasDataChanged As Boolean = False

            Public Sub New(ByVal LastOp As ElitaPlusPage.DetailPageCommand, ByVal TemplateGroupCodeId As Guid, ByVal TemplateCodeId As Guid, Optional ByVal HasDataChanged As Boolean = False)
                Me.LastOperation = LastOp
                Me.TemplateGroupCodeId = TemplateGroupCodeId
                Me.TemplateCodeId = TemplateCodeId
                Me.HasDataChanged = HasDataChanged
            End Sub
        End Class
#End Region

#Region "Page_Events"
        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            Try
                Me.MasterPage.MessageController.Clear_Hide()

                If Not Page.IsPostBack Then
                    Me.MasterPage.MessageController.Clear()
                    Me.MasterPage.UsePageTabTitleInBreadCrum = False
                    Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("Tables")

                    TranslateGridHeader(Grid)
                    UpdateBreadCrum()

                    ControlMgr.SetVisibleControl(Me, trPageSize, False)
                    Me.SortDirection = Me.State.SortExpression
                    PopulateDropdown()

                    If Me.State.IsGridVisible Then
                        If Not (Me.State.PageSize = DEFAULT_NEW_UI_PAGE_SIZE) Or Not (State.PageSize = Grid.PageSize) Then
                            Grid.PageSize = Me.State.PageSize
                        End If

                        cboPageSize.SelectedValue = CType(Me.State.PageSize, String)
                        Me.PopulateGrid()
                    End If

                    Me.SetGridItemStyleColor(Me.Grid)
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try

            Me.ShowMissingTranslations(Me.MasterPage.MessageController)
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
                DealerMultipleDrop.SelectedGuid = Me.State.DealerId
                DealerMultipleDrop.BindData(LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies))
            Catch ex As Exception
                Me.MasterPage.MessageController.AddError(TEMPLATEGROUP_LIST_FORM001)
                Me.MasterPage.MessageController.AddError(ex.Message, False)
                Me.MasterPage.MessageController.Show()
            End Try
        End Sub

        Private Sub PopulateTemplateGroupCode()
            txtTemplateGroupCode.Text = Me.State.TemplateGroupCodeMask
        End Sub

        Private Sub PopulateTemplateCode()
            txtTemplateCode.Text = Me.State.TemplateCodeMask
        End Sub

        Private Sub BindDataGrid(ByVal oDataView As DataView)
            Grid.DataSource = oDataView
            Grid.DataBind()
        End Sub

        Private Sub PopulateGrid(Optional ByVal oAction As String = POPULATE_ACTION_NONE)
            Dim oDataView As DataView

            Try
                If (Me.State.SearchDV Is Nothing) Then
                    Me.State.SearchDV = OcTemplate.GetTemplateSearchDV(ElitaPlusIdentity.Current.ActiveUser.Companies, DealerMultipleDrop.SelectedGuid, Me.State.TemplateGroupCodeMask, Me.State.TemplateCodeMask)
                End If

                If (Me.State.SearchDV.Count = 0) Then
                    Me.State.bNoRow = True
                    CreateHeaderForEmptyGrid(Grid, Me.SortDirection)
                Else
                    Me.State.bNoRow = False
                    Me.Grid.Enabled = True
                End If

                Me.State.SearchDV.Sort = Me.State.SortExpression
                Grid.AutoGenerateColumns = False

                Grid.Columns(Me.GRID_COL_TEMPLATE_GROUP_CODE).SortExpression = OcTemplate.TemplateSearchDV.COL_TEMPLATE_GROUP_CODE
                Grid.Columns(Me.GRID_COL_TEMPLATE_GROUP_DESCRIPTION).SortExpression = OcTemplate.TemplateSearchDV.COL_TEMPLATE_GROUP_DESCRIPTION
                Grid.Columns(Me.GRID_COL_TEMPLATE_CODE).SortExpression = OcTemplate.TemplateSearchDV.COL_TEMPLATE_CODE
                Grid.Columns(Me.GRID_COL_TEMPLATE_DESCRIPTION).SortExpression = OcTemplate.TemplateSearchDV.COL_TEMPLATE_DESCRIPTION

                HighLightSortColumn(Grid, Me.State.SortExpression)

                SetPageAndSelectedIndexFromGuid(Me.State.SearchDV, Me.State.DealerId, Me.Grid, Me.State.PageIndex)

                Me.Grid.DataSource = Me.State.SearchDV
                HighLightSortColumn(Grid, Me.SortDirection)
                Me.Grid.DataBind()

                ControlMgr.SetVisibleControl(Me, trPageSize, Grid.Visible)

                Session("recCount") = Me.State.SearchDV.Count

                If Not Grid.BottomPagerRow.Visible Then Grid.BottomPagerRow.Visible = True

                Me.lblRecordCount.Text = Me.State.SearchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)

            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub ClearSearch()
            DealerMultipleDrop.SelectedIndex = 0
            txtTemplateGroupCode.Text = Nothing
            txtTemplateCode.Text = Nothing
            Me.State.TemplateGroupId = Guid.Empty
            Me.State.TemplateId = Guid.Empty
        End Sub

        Private Sub UpdateBreadCrum()
            If (Not Me.State Is Nothing) Then
                If (Not Me.State Is Nothing) Then
                    Me.MasterPage.BreadCrum = Me.MasterPage.PageTab & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage("TEMPLATE_SEARCH")
                    Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("TEMPLATE_SEARCH")
                End If
            End If
        End Sub
#End Region

#Region "Datagrid Related"
        Public Property SortDirection() As String
            Get
                Return ViewState("SortDirection").ToString
            End Get
            Set(ByVal value As String)
                ViewState("SortDirection") = value
            End Set
        End Property

        Private Sub Grid_PageSizeChanged(ByVal source As Object, ByVal e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
            Try
                Grid.PageIndex = NewCurrentPageIndex(Grid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
                Me.State.PageIndex = Grid.PageSize
                Me.PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub Grid_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Grid.PageIndexChanging
            Try
                Me.State.PageIndex = e.NewPageIndex
                Me.State.DealerId = Guid.Empty
                Me.PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Public Sub RowCreated(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
            BaseItemCreated(sender, e)
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
                Me.State.SortExpression = Me.SortDirection
                Me.State.PageIndex = 0
                Me.PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private _listitem() As Assurant.Elita.CommonConfiguration.DataElements.ListItem

        Private Function GetTemplateTypeByCode(ByVal strTemplateTypeXcd As String) As String
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
        Private Sub Grid_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowDataBound
            Try
                Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
                Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
                Dim btnEditItem As LinkButton
                Dim btnEditItem2 As LinkButton

                If Not dvRow Is Nothing And Not Me.State.bNoRow Then
                    If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then
                        btnEditItem = CType(e.Row.Cells(Me.GRID_COL_EDIT_IDX).FindControl("SelectAction"), LinkButton)
                        btnEditItem.Text = dvRow(OcTemplate.TemplateSearchDV.COL_TEMPLATE_GROUP_CODE).ToString

                        e.Row.Cells(Me.GRID_COL_TEMPLATE_GROUP_DESCRIPTION).Text = dvRow(OcTemplate.TemplateSearchDV.COL_TEMPLATE_GROUP_DESCRIPTION).ToString
                        e.Row.Cells(Me.GRID_COL_TEMPLATE_GROUP_ID).Text = GetGuidStringFromByteArray(CType(dvRow(OcTemplate.TemplateSearchDV.COL_TEMPLATE_GROUP_ID), Byte()))

                        e.Row.Cells(GRID_COL_TEMPLATE_TYPE).Text = GetTemplateTypeByCode(dvRow(OcTemplate.TemplateSearchDV.COL_TEMPLATE_TYPE_XCD).ToString)

                        If Not Convert.IsDBNull(dvRow(OcTemplate.TemplateSearchDV.COL_TEMPLATE_ID)) Then
                            btnEditItem2 = CType(e.Row.Cells(Me.GRID_COL_EDIT2_IDX).FindControl("SelectAction2"), LinkButton)
                            btnEditItem2.Text = dvRow(OcTemplate.TemplateSearchDV.COL_TEMPLATE_CODE).ToString
                            e.Row.Cells(Me.GRID_COL_TEMPLATE_DESCRIPTION).Text = dvRow(OcTemplate.TemplateSearchDV.COL_TEMPLATE_DESCRIPTION).ToString
                            e.Row.Cells(Me.GRID_COL_TEMPLATE_ID).Text = GetGuidStringFromByteArray(CType(dvRow(OcTemplate.TemplateSearchDV.COL_TEMPLATE_ID), Byte()))
                        Else
                            e.Row.Cells(Me.GRID_COL_EDIT2_IDX).Text = String.Empty
                            e.Row.Cells(Me.GRID_COL_TEMPLATE_ID).Text = String.Empty
                        End If

                    End If
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Public Sub RowCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)
            Try
                Dim index As Integer = CInt(e.CommandArgument)
                Dim templateGroupId = New Guid(Me.Grid.Rows(index).Cells(Me.GRID_COL_TEMPLATE_GROUP_ID).Text)

                If e.CommandName = "SelectAction" Then
                    Me.State.TemplateGroupId = templateGroupId
                    Me.callPage(TemplateGroupForm.URL, Me.State.TemplateGroupId)
                ElseIf e.CommandName = "SelectAction2" Then
                    Me.State.TemplateId = New Guid(Me.Grid.Rows(index).Cells(Me.GRID_COL_TEMPLATE_ID).Text)
                    Me.callPage(TemplateForm.URL, New TemplateForm.CallType(Me.State.TemplateId, templateGroupId))
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try

        End Sub
#End Region

#Region "Button Clicks"
        Private Sub moBtnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles moBtnSearch.Click
            Try
                If Not State.IsGridVisible Then
                    cboPageSize.SelectedValue = CType(State.PageSize, String)
                    If Not (Grid.PageSize = DEFAULT_NEW_UI_PAGE_SIZE) Then
                        cboPageSize.SelectedValue = CType(State.PageSize, String)
                        Grid.PageSize = State.PageSize
                    End If
                    Me.State.IsGridVisible = True
                End If
                Grid.PageIndex = Me.NO_PAGE_INDEX
                Grid.DataMember = Nothing
                Me.State.SearchDV = Nothing
                SetSession()
                Me.PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub moBtnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles moBtnClear.Click
            Try
                ClearSearch()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnNew_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew_WRITE.Click
            Try
                Me.State.TemplateGroupId = Guid.Empty
                SetSession()
                Me.callPage(TemplateGroupForm.URL, Me.State.TemplateGroupId)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub
#End Region

#Region "State-Management"
        Private Sub SetSession()
            With Me.State
                .TemplateGroupCodeMask = txtTemplateGroupCode.Text.ToUpper
                .TemplateCodeMask = txtTemplateCode.Text.ToUpper
                .DealerId = DealerMultipleDrop.SelectedGuid
                .PageIndex = Grid.PageIndex
                .PageSize = Grid.PageSize
                .PageSort = Me.State.SortExpression
            End With
        End Sub
#End Region
    End Class
End Namespace
