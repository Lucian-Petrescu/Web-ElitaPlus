Imports Assurant.ElitaPlus.DALObjects
Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports Assurant.Elita.Web.Forms
Imports System.Threading

Namespace Tables

    Partial Class CommissionPlanSearchForm
        Inherits ElitaPlusSearchPage
        ' Inherits System.Web.UI.Page

#Region "Constants"

        Public Const GRIDCOMMPLAN_COL_COMM_PLAN_ID_IDX As Integer = 1
        Public Const GRIDCOMMPLAN_COL_COMPANY_CODE_IDX As Integer = 2
        Public Const GRIDCOMMPLAN_COL_DEALER_IDX As Integer = 3
        Public Const GRIDCOMMPLAN_COL_CODE_IDX As Integer = 4
        Public Const GRIDCOMMPLAN_COL_DESCRIPTION_IDX As Integer = 5
        Public Const GRIDCOMMPLAN_COL_EFFECTIVE_IDX As Integer = 6
        Public Const GRIDCOMMPLAN_COL_EXPIRATION_IDX As Integer = 7
        Public Const GRIDCOMMPLAN_TOTAL_COLUMNS As Integer = 8

        Private Const GRID_CTRL_NAME_COMM_PERIOD_ID As String = "moCommissionPeriodId_NO_TRANSLATE"

        Private Const LABEL_DEALER As String = "DEALER"
        Public Const PAGETITLE As String = "COMMISSION_PLAN"
        Public Const PAGETAB As String = "TABLES"
#End Region

#Region "Page State"

#Region "MyState"
        Class MyState
            Public moCommissionPlanId As Guid = Guid.Empty
            Public moDealerId As Guid = Guid.Empty
            Public mnPageIndex As Integer
            Public searchDV As DataView = Nothing
            Public PageSize As Integer = DEFAULT_PAGE_SIZE
            Public HasDataChanged As Boolean
            Public IsGridVisible As Boolean = False
            Public searchBtnClicked As Boolean = False
            Public SortExpression As String = CommissionPeriodDAL.COL_NAME_DEALER_NAME
            Public moCommPlanDistId As Guid = Guid.Empty
            Public moCommPlanId As Guid = Guid.Empty
            ' these variables are used to store the sorting columns information.
            Public SortColumns(GRIDCOMMPLAN_TOTAL_COLUMNS - 1) As String
            Public IsSortDesc(GRIDCOMMPLAN_TOTAL_COLUMNS - 1) As Boolean
            Public bnoRow As Boolean = False

            Sub New()

            End Sub

            ' this will be called before the populate list to get the correct sort order
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
#End Region

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

                State.HasDataChanged = retObj.BoChanged
                If retObj IsNot Nothing AndAlso retObj.BoChanged Then
                    State.searchDV = Nothing
                End If
                If retObj IsNot Nothing Then

                    Select Case retObj.LastOperation
                        Case ElitaPlusPage.DetailPageCommand.Back
                            State.moCommissionPlanId = retObj.moCommissionPlanId
                        Case Else
                            State.moCommissionPlanId = Guid.Empty
                    End Select

                    PopulateDealerDropDown()

                    If State.IsGridVisible Then
                        GridCommPlan.PageIndex = State.mnPageIndex
                        GridCommPlan.PageSize = State.PageSize
                        cboPageSize.SelectedValue = CType(State.PageSize, String)
                        GridCommPlan.PageSize = State.PageSize
                        ControlMgr.SetVisibleControl(Me, trPageSize, GridCommPlan.Visible)
                    End If

                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Public Class ReturnType
            Public LastOperation As ElitaPlusPage.DetailPageCommand
            Public moCommissionPlanId As Guid
            Public BoChanged As Boolean = False

            Public Sub New(LastOp As ElitaPlusPage.DetailPageCommand, oCommissionPlanId As Guid, Optional ByVal boChanged As Boolean = False)
                LastOperation = LastOp
                moCommissionPlanId = oCommissionPlanId
                Me.BoChanged = boChanged
            End Sub

        End Class
#End Region

        Public Sub New()
            MyBase.New(New MyState)
        End Sub

        Protected Shadows ReadOnly Property State() As MyState
            Get
                Return CType(MyBase.State, MyState)
            End Get
        End Property

#End Region

#Region "Variables"

#End Region

#Region "Properties"

#End Region

#Region "Handlers"


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

#Region "Handlers-Init"

        Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            MasterPage.MessageController.Clear_Hide()
            Try
                If Not IsPostBack Then
                    ' Set Master Page Header
                    MasterPage.MessageController.Clear()
                    MasterPage.UsePageTabTitleInBreadCrum = False
                    MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("Tables")

                    ' Update Bread Crum
                    UpdateBreadCrum()

                    SortDirection = CommissionPeriod.CommissionPeriodSearchDV.COL_DEALER_NAME
                    ControlMgr.SetVisibleControl(Me, trPageSize, False)
                    SetFormTitle(PAGETITLE)
                    SetFormTab(PAGETAB)
                    TranslateGridHeader(GridCommPlan)
                    TranslateGridControls(GridCommPlan)
                    If Not IsReturningFromChild Then
                        ' It is The First Time
                        ' It is not Returning from Detail
                        ControlMgr.SetVisibleControl(Me, trPageSize, False)
                        PopulateDealerDropDown()
                    Else
                        ' It is returning from detail
                        ControlMgr.SetVisibleControl(Me, GridCommPlan, State.IsGridVisible)
                        ControlMgr.SetVisibleControl(Me, trPageSize, State.IsGridVisible)
                        If State.IsGridVisible Then
                            PopulateGrid()
                        End If
                    End If
                    SetGridItemStyleColor(GridCommPlan)
                    SetGridItemStyleColor(GridCommPlan)
                End If
                If IsReturningFromChild = True Then
                    IsReturningFromChild = False
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
            ShowMissingTranslations(MasterPage.MessageController)
        End Sub

#End Region

#Region "Handlers-Buttons"

        Private Sub BtnClear_Click(sender As System.Object, e As System.EventArgs) Handles btnClear.Click
            Try
                ClearAll()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub BtnSearch_Click(sender As System.Object, e As System.EventArgs) Handles btnSearch.Click
            Try
                GridCommPlan.PageIndex = NO_PAGE_INDEX
                State.searchDV = Nothing
                State.searchBtnClicked = True
                State.moDealerId = moDealerMultipleDrop.SelectedGuid ' Me.GetSelectedItem(moDealerDrop)
                State.HasDataChanged = False
                State.IsGridVisible = True
                PopulateGrid()
                State.searchBtnClicked = False
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnNew_Click(sender As Object, e As System.EventArgs) Handles btnNew.Click
            Try
                State.moDealerId = moDealerMultipleDrop.SelectedGuid 'Me.GetSelectedItem(moDealerDrop)
                State.moCommPlanId = Guid.Empty
                State.mnPageIndex = GridCommPlan.PageIndex
                callPage(CommissionPlanForm.URL, State.moCommPlanId)
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

        'The Binding Logic is here

        Protected Sub GridCommPlan_RowCreated(sender As Object, e As GridViewRowEventArgs) Handles GridCommPlan.RowCreated
            Try
                BaseItemCreated(sender, e)
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub GridCommPlan_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridCommPlan.RowCommand
            Try
                If e.CommandName = SELECT_COMMAND_NAME Then
                    Dim index As Integer = CInt(e.CommandArgument)
                    State.moCommPlanId = New Guid(GridCommPlan.Rows(index).Cells(GRIDCOMMPLAN_COL_COMM_PLAN_ID_IDX).Text)
                    State.mnPageIndex = GridCommPlan.PageIndex
                    State.moDealerId = moDealerMultipleDrop.SelectedGuid 'Me.GetSelectedItem(moDealerDrop)
                    callPage(CommissionPlanForm.URL, State.moCommPlanId)
                End If
            Catch ex As Threading.ThreadAbortException
                '  System.Threading.Thread.Sleep(500)
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub GridCommPlan_ItemDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridCommPlan.RowDataBound
            Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
            Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
            If _
                dvRow IsNot Nothing AndAlso Not State.bnoRow AndAlso
                (itemType = ListItemType.Item OrElse itemType = ListItemType.AlternatingItem OrElse
                 itemType = ListItemType.SelectedItem) Then
                e.Row.Cells(GRIDCOMMPLAN_COL_COMM_PLAN_ID_IDX).Text =
                    GetGuidStringFromByteArray(CType(dvRow(CommPlan.CommPlanSearchDV.COL_COMMISSION_PERIOD_ID), Byte()))
                e.Row.Cells(GRIDCOMMPLAN_COL_COMPANY_CODE_IDX).Text =
                    dvRow(CommPlan.CommPlanSearchDV.COL_COMPANY_CODE).ToString
                e.Row.Cells(GRIDCOMMPLAN_COL_DEALER_IDX).Text =
                    dvRow(CommPlan.CommPlanSearchDV.COL_DEALER_NAME).ToString
                e.Row.Cells(GRIDCOMMPLAN_COL_CODE_IDX).Text = dvRow(CommPlan.CommPlanSearchDV.COL_CODE).ToString
                e.Row.Cells(GRIDCOMMPLAN_COL_DESCRIPTION_IDX).Text =
                    dvRow(CommPlan.CommPlanSearchDV.COL_DESCRIPTION).ToString
                e.Row.Cells(GRIDCOMMPLAN_COL_EFFECTIVE_IDX).Text =
                    GetDateFormattedString(CType(dvRow(CommPlan.CommPlanSearchDV.COL_EFFECTIVE_DATE), Date))
                e.Row.Cells(GRIDCOMMPLAN_COL_EXPIRATION_IDX).Text =
                    GetDateFormattedString(CType(dvRow(CommPlan.CommPlanSearchDV.COL_EXPIRATION_DATE), Date))
            End If
        End Sub

        Protected Sub GridCommPlan_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles GridCommPlan.PageIndexChanging
            Try
                State.mnPageIndex = e.NewPageIndex
                GridCommPlan.PageIndex = e.NewPageIndex
                State.moCommPlanId = Guid.Empty
                PopulateCommPlanGrid()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub GridCommPlan_SortCommand(sender As Object, e As GridViewSortEventArgs) Handles GridCommPlan.Sorting
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

                State.mnPageIndex = 0
                PopulateCommPlanGrid()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub
#End Region

#End Region

#Region "Clear"

        Private Sub ClearAll()
            moDealerMultipleDrop.SelectedIndex = BLANK_ITEM_SELECTED
        End Sub

#End Region

#Region "Populate"
        Private Sub UpdateBreadCrum()
            If (State IsNot Nothing) Then
                MasterPage.BreadCrum = MasterPage.PageTab & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage(PAGETITLE)
                MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage(PAGETITLE)
            End If
        End Sub

        Sub PopulateDealerDropDown()
            Try
                Dim oDataView As DataView
                oDataView = LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies)
                moDealerMultipleDrop.Caption = TranslationBase.TranslateLabelOrMessage(LABEL_DEALER)
                moDealerMultipleDrop.NothingSelected = True

                moDealerMultipleDrop.BindData(oDataView)
                moDealerMultipleDrop.AutoPostBackDD = True
                moDealerMultipleDrop.SelectedGuid = State.moDealerId

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub PopulateGrid()
            Try
                State.moDealerId = moDealerMultipleDrop.SelectedGuid
                PopulateCommPlanGrid()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub GridCommPlan_PageSizeChanged(source As Object, e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
            Try
                GridCommPlan.PageIndex = NewCurrentPageIndex(GridCommPlan, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
                State.PageSize = GridCommPlan.PageSize
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub


        Private Sub OnFromDrop_Changed(fromMultipleDrop As Assurant.ElitaPlus.ElitaPlusWebApp.Common.MultipleColumnDDLabelControl_New) _
           Handles moDealerMultipleDrop.SelectedDropChanged
            Try
                State.moDealerId = moDealerMultipleDrop.SelectedGuid
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

#End Region

#Region "Commission Plan Related"
        Private Sub PopulateCommPlanGrid()
            Try
                State.moDealerId = moDealerMultipleDrop.SelectedGuid

                State.searchDV = CommPlan.getList(GetSearchParmCommPlan)

                State.searchDV.Sort = SortDirection

                GridCommPlan.AutoGenerateColumns = False
                SetPageAndSelectedIndexFromGuid(State.searchDV, State.moCommPlanId, GridCommPlan, State.mnPageIndex)

                SortAndBindCommPlanGrid()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub SortAndBindCommPlanGrid()
            State.mnPageIndex = GridCommPlan.PageIndex
            If (State.searchDV.Count = 0) Then
                State.bnoRow = True
                CreateHeaderForEmptyGrid(GridCommPlan, SortDirection)
            Else
                State.bnoRow = False
                GridCommPlan.Enabled = True
                GridCommPlan.DataSource = State.searchDV
                HighLightSortColumn(GridCommPlan, SortDirection)
                GridCommPlan.DataBind()
            End If

            If Not GridCommPlan.BottomPagerRow.Visible Then GridCommPlan.BottomPagerRow.Visible = True

            ControlMgr.SetVisibleControl(Me, GridCommPlan, True)
            ControlMgr.SetVisibleControl(Me, trPageSize, True)

            Session("recCount") = State.searchDV.Count

            If State.searchDV.Count > 0 Then
                If GridCommPlan.Visible Then
                    lblRecordCount.Text = State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                End If
            End If
        End Sub

        Private Function GetSearchParmCommPlan() As CommPlanData
            Dim oCommPlanData As CommPlanData = New CommPlanData

            With oCommPlanData
                .companyIds = ElitaPlusIdentity.Current.ActiveUser.Companies
                .dealerId = State.moDealerId
            End With

            Return oCommPlanData

        End Function


#End Region
    End Class


End Namespace