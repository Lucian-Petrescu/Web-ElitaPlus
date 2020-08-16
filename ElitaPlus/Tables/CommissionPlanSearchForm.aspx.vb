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
                    For i = 0 To Me.SortColumns.Length - 1
                        If Not Me.SortColumns(i) Is Nothing Then
                            sortExp &= Me.SortColumns(i)
                            If Me.IsSortDesc(i) Then sortExp &= " DESC"
                            sortExp &= ","
                        End If
                    Next
                    Return sortExp.Substring(0, sortExp.Length - 1) 'to remove the last comma
                End Get
            End Property

            Public Sub ToggleSort1(ByVal gridColIndex As Integer)
                IsSortDesc(gridColIndex) = Not IsSortDesc(gridColIndex)
            End Sub
        End Class
#End Region

#Region "Page Return"
        Private IsReturningFromChild As Boolean = False

        Public Sub Page_PageReturn(ByVal ReturnFromUrl As String, ByVal ReturnPar As Object) Handles MyBase.PageReturn
            Try
                Me.IsReturningFromChild = True
                If Me.State.searchDV Is Nothing Then
                    Me.State.IsGridVisible = False
                Else
                    Me.State.IsGridVisible = True
                End If
                Dim retObj As ReturnType = CType(ReturnPar, ReturnType)

                Me.State.HasDataChanged = retObj.BoChanged
                If Not retObj Is Nothing AndAlso retObj.BoChanged Then
                    Me.State.searchDV = Nothing
                End If
                If Not retObj Is Nothing Then

                    Select Case retObj.LastOperation
                        Case ElitaPlusPage.DetailPageCommand.Back
                            Me.State.moCommissionPlanId = retObj.moCommissionPlanId
                        Case Else
                            Me.State.moCommissionPlanId = Guid.Empty
                    End Select

                    Me.PopulateDealerDropDown()

                    If Me.State.IsGridVisible Then
                        GridCommPlan.PageIndex = Me.State.mnPageIndex
                        GridCommPlan.PageSize = Me.State.PageSize
                        cboPageSize.SelectedValue = CType(Me.State.PageSize, String)
                        GridCommPlan.PageSize = Me.State.PageSize
                        ControlMgr.SetVisibleControl(Me, trPageSize, GridCommPlan.Visible)
                    End If

                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Public Class ReturnType
            Public LastOperation As ElitaPlusPage.DetailPageCommand
            Public moCommissionPlanId As Guid
            Public BoChanged As Boolean = False

            Public Sub New(ByVal LastOp As ElitaPlusPage.DetailPageCommand, ByVal oCommissionPlanId As Guid, Optional ByVal boChanged As Boolean = False)
                Me.LastOperation = LastOp
                Me.moCommissionPlanId = oCommissionPlanId
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

        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

#Region "Handlers-Init"

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            Me.MasterPage.MessageController.Clear_Hide()
            Try
                If Not Me.IsPostBack Then
                    ' Set Master Page Header
                    Me.MasterPage.MessageController.Clear()
                    Me.MasterPage.UsePageTabTitleInBreadCrum = False
                    Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("Tables")

                    ' Update Bread Crum
                    UpdateBreadCrum()

                    Me.SortDirection = CommissionPeriod.CommissionPeriodSearchDV.COL_DEALER_NAME
                    ControlMgr.SetVisibleControl(Me, trPageSize, False)
                    Me.SetFormTitle(PAGETITLE)
                    Me.SetFormTab(PAGETAB)
                    Me.TranslateGridHeader(GridCommPlan)
                    Me.TranslateGridControls(GridCommPlan)
                    If Not Me.IsReturningFromChild Then
                        ' It is The First Time
                        ' It is not Returning from Detail
                        ControlMgr.SetVisibleControl(Me, Me.trPageSize, False)
                        PopulateDealerDropDown()
                    Else
                        ' It is returning from detail
                        ControlMgr.SetVisibleControl(Me, GridCommPlan, Me.State.IsGridVisible)
                        ControlMgr.SetVisibleControl(Me, trPageSize, Me.State.IsGridVisible)
                        If Me.State.IsGridVisible Then
                            Me.PopulateGrid()
                        End If
                    End If
                    Me.SetGridItemStyleColor(Me.GridCommPlan)
                    Me.SetGridItemStyleColor(Me.GridCommPlan)
                End If
                If Me.IsReturningFromChild = True Then
                    Me.IsReturningFromChild = False
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
            Me.ShowMissingTranslations(Me.MasterPage.MessageController)
        End Sub

#End Region

#Region "Handlers-Buttons"

        Private Sub BtnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
            Try
                ClearAll()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub BtnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
            Try
                GridCommPlan.PageIndex = Me.NO_PAGE_INDEX
                Me.State.searchDV = Nothing
                Me.State.searchBtnClicked = True
                Me.State.moDealerId = moDealerMultipleDrop.SelectedGuid ' Me.GetSelectedItem(moDealerDrop)
                Me.State.HasDataChanged = False
                Me.State.IsGridVisible = True
                PopulateGrid()
                Me.State.searchBtnClicked = False
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
            Try
                Me.State.moDealerId = moDealerMultipleDrop.SelectedGuid 'Me.GetSelectedItem(moDealerDrop)
                Me.State.moCommPlanId = Guid.Empty
                Me.State.mnPageIndex = GridCommPlan.PageIndex
                Me.callPage(CommissionPlanForm.URL, Me.State.moCommPlanId)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

#End Region

#Region "Handlers-Grid"

        Public Property SortDirection() As String
            Get
                Return ViewState("SortDirection").ToString
            End Get
            Set(ByVal value As String)
                ViewState("SortDirection") = value
            End Set
        End Property

        'The Binding Logic is here

        Protected Sub GridCommPlan_RowCreated(sender As Object, e As GridViewRowEventArgs) Handles GridCommPlan.RowCreated
            Try
                BaseItemCreated(sender, e)
            Catch ex As Exception
                HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub GridCommPlan_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridCommPlan.RowCommand
            Try
                If e.CommandName = Me.SELECT_COMMAND_NAME Then
                    Dim index As Integer = CInt(e.CommandArgument)
                    Me.State.moCommPlanId = New Guid(Me.GridCommPlan.Rows(index).Cells(Me.GRIDCOMMPLAN_COL_COMM_PLAN_ID_IDX).Text)
                    Me.State.mnPageIndex = GridCommPlan.PageIndex
                    Me.State.moDealerId = moDealerMultipleDrop.SelectedGuid 'Me.GetSelectedItem(moDealerDrop)
                    Me.callPage(CommissionPlanForm.URL, Me.State.moCommPlanId)
                End If
            Catch ex As Threading.ThreadAbortException
                '  System.Threading.Thread.Sleep(500)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub GridCommPlan_ItemDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridCommPlan.RowDataBound
            Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
            Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
            If Not dvRow Is Nothing And Not Me.State.bnoRow Then
                If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then
                    e.Row.Cells(Me.GRIDCOMMPLAN_COL_COMM_PLAN_ID_IDX).Text = GetGuidStringFromByteArray(CType(dvRow(CommPlan.CommPlanSearchDV.COL_COMMISSION_PERIOD_ID), Byte()))
                    e.Row.Cells(Me.GRIDCOMMPLAN_COL_COMPANY_CODE_IDX).Text = dvRow(CommPlan.CommPlanSearchDV.COL_COMPANY_CODE).ToString
                    e.Row.Cells(Me.GRIDCOMMPLAN_COL_DEALER_IDX).Text = dvRow(CommPlan.CommPlanSearchDV.COL_DEALER_NAME).ToString
                    e.Row.Cells(Me.GRIDCOMMPLAN_COL_CODE_IDX).Text = dvRow(CommPlan.CommPlanSearchDV.COL_CODE).ToString
                    e.Row.Cells(Me.GRIDCOMMPLAN_COL_DESCRIPTION_IDX).Text = dvRow(CommPlan.CommPlanSearchDV.COL_DESCRIPTION).ToString
                    e.Row.Cells(Me.GRIDCOMMPLAN_COL_EFFECTIVE_IDX).Text = Me.GetDateFormattedString(CType(dvRow(CommPlan.CommPlanSearchDV.COL_EFFECTIVE_DATE), Date))
                    e.Row.Cells(Me.GRIDCOMMPLAN_COL_EXPIRATION_IDX).Text = Me.GetDateFormattedString(CType(dvRow(CommPlan.CommPlanSearchDV.COL_EXPIRATION_DATE), Date))
                End If
            End If
        End Sub

        Protected Sub GridCommPlan_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles GridCommPlan.PageIndexChanging
            Try
                Me.State.mnPageIndex = e.NewPageIndex
                GridCommPlan.PageIndex = e.NewPageIndex
                Me.State.moCommPlanId = Guid.Empty
                PopulateCommPlanGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub GridCommPlan_SortCommand(sender As Object, e As GridViewSortEventArgs) Handles GridCommPlan.Sorting
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

                Me.State.mnPageIndex = 0
                Me.PopulateCommPlanGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub
#End Region

#End Region

#Region "Clear"

        Private Sub ClearAll()
            moDealerMultipleDrop.SelectedIndex = Me.BLANK_ITEM_SELECTED
        End Sub

#End Region

#Region "Populate"
        Private Sub UpdateBreadCrum()
            If (Not Me.State Is Nothing) Then
                Me.MasterPage.BreadCrum = Me.MasterPage.PageTab & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage(PAGETITLE)
                Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage(PAGETITLE)
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
                moDealerMultipleDrop.SelectedGuid = Me.State.moDealerId

            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub PopulateGrid()
            Try
                Me.State.moDealerId = moDealerMultipleDrop.SelectedGuid
                PopulateCommPlanGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub GridCommPlan_PageSizeChanged(ByVal source As Object, ByVal e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
            Try
                Me.GridCommPlan.PageIndex = NewCurrentPageIndex(GridCommPlan, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
                Me.State.PageSize = GridCommPlan.PageSize
                Me.PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub


        Private Sub OnFromDrop_Changed(ByVal fromMultipleDrop As Assurant.ElitaPlus.ElitaPlusWebApp.Common.MultipleColumnDDLabelControl_New) _
           Handles moDealerMultipleDrop.SelectedDropChanged
            Try
                Me.State.moDealerId = moDealerMultipleDrop.SelectedGuid
            Catch ex As Exception
                HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

#End Region

#Region "Commission Plan Related"
        Private Sub PopulateCommPlanGrid()
            Try
                Me.State.moDealerId = moDealerMultipleDrop.SelectedGuid

                Me.State.searchDV = CommPlan.getList(GetSearchParmCommPlan)

                Me.State.searchDV.Sort = Me.SortDirection

                Me.GridCommPlan.AutoGenerateColumns = False
                SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.moCommPlanId, Me.GridCommPlan, Me.State.mnPageIndex)

                Me.SortAndBindCommPlanGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub SortAndBindCommPlanGrid()
            Me.State.mnPageIndex = Me.GridCommPlan.PageIndex
            If (Me.State.searchDV.Count = 0) Then
                Me.State.bnoRow = True
                CreateHeaderForEmptyGrid(GridCommPlan, Me.SortDirection)
            Else
                Me.State.bnoRow = False
                Me.GridCommPlan.Enabled = True
                Me.GridCommPlan.DataSource = Me.State.searchDV
                HighLightSortColumn(GridCommPlan, Me.SortDirection)
                Me.GridCommPlan.DataBind()
            End If

            If Not GridCommPlan.BottomPagerRow.Visible Then GridCommPlan.BottomPagerRow.Visible = True

            ControlMgr.SetVisibleControl(Me, GridCommPlan, True)
            ControlMgr.SetVisibleControl(Me, trPageSize, True)

            Session("recCount") = Me.State.searchDV.Count

            If Me.State.searchDV.Count > 0 Then
                If Me.GridCommPlan.Visible Then
                    Me.lblRecordCount.Text = Me.State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                End If
            End If
        End Sub

        Private Function GetSearchParmCommPlan() As CommPlanData
            Dim oCommPlanData As CommPlanData = New CommPlanData

            With oCommPlanData
                .companyIds = ElitaPlusIdentity.Current.ActiveUser.Companies
                .dealerId = Me.State.moDealerId
            End With

            Return oCommPlanData

        End Function


#End Region
    End Class


End Namespace