
Option Strict On
Option Explicit On

Imports System.Globalization
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports System.Threading
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms
Imports Microsoft.VisualBasic
Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common

Namespace Tables
    Public Class BonusStructureForm
        Inherits ElitaPlusSearchPage

#Region "Handlers-Init"

        Protected WithEvents moErrorController As ErrorController
        'Protected WithEvents multipleDropControl As MultipleColumnDDLabelControl

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()>
        Private Sub InitializeComponent()

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
#End Region

#Region "Constants"
        Public Const URL As String = "BonusStructureForm.aspx"

        'Bonus Settings Detail Columns
        Public Const GRID_COL_EDIT_IDX As Integer = 0
        Public Const GRID_COL_DEALERID_IDX As Integer = 2
        Public Const GRID_COL_SERVICE_CENTERID_IDX As Integer = 1
        Public Const GRID_COL_PRODUCT_CODE_IDX As Integer = 3

        Public Const GRID_COL_COMPUTE_BONUS_METHODID_IDX As Integer = 4
        Public Const GRID_COL_MAXIMUM_TURNAROUND_TIME_IDX As Integer = 5
        Public Const GRID_COL_PERCENTAGE_OR_AMOUNT_IDX As Integer = 6
        Public Const GRID_COL_PRIORITY_IDX As Integer = 7
        Public Const GRID_COL_SC_REPLACEMENT_PERCENTAGE_IDX As Integer = 8
        Public Const GRID_COL_BONUS_AMOUNT_PERIOD_IDX As Integer = 9
        Public Const GRID_COL_EFFECTIVE_DATEID_IDX As Integer = 10
        Public Const GRID_COL_EXPIRATION_DATEID_IDX As Integer = 11
        Public Const GRID_COL_BONUS_STRUCTURE_IDX As Integer = 12

        Public Const GRID_TOTAL_COLUMNS As Integer = 13
        Private Const NOTHING_SELECTED As String = "00000000-0000-0000-0000-000000000000"

        Private Const LABEL_DEALER As String = "DEALER"
        Private Const LABEL_SERVICE_CENTER As String = "SERVICE CENTER"
        Private Const LABEL_PRODUCT_CODE As String = "PRODUCT_CODE"

        Public Const BTN_CONTROL_EDIT_DETAIL_LIST As String = "EditButton_WRITE"

        'controls used in the form


        Private Const BONUSSTRUCTUREFORM As String = "BonusStructureForm.aspx"



#End Region


#Region "Page State"
        Private IsReturningFromChild As Boolean = False

        ' This class keeps the current state for the search page.
        Class MyState
            Public PageIndex As Integer = 0
            Public BonusStructureId As Guid = Guid.Empty
            Public IsGridVisible As Boolean
            Public SearchDV As ClaimBonusSettings.BonusSettingsDV = Nothing
            Public SelectedPageSize As Integer = DEFAULT_NEW_UI_PAGE_SIZE
            Public SortExpression As String = ClaimBonusSettings.BonusSettingsDV.COL_NAME_DEALER_NAME
            Public HasDataChanged As Boolean
            Public bNoRow As Boolean = False
            Public ProductCodeId As Guid = Guid.Empty
            Public DealerId As Guid = Guid.Empty
            Public ServiceCenterId As Guid = Guid.Empty

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

#Region "Page_Events"

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            Me.MasterPage.MessageController.Clear_Hide()

            Try

                If Not Me.IsPostBack Then
                    Me.MasterPage.MessageController.Clear()
                    Me.MasterPage.UsePageTabTitleInBreadCrum = False
                    Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("Tables")
                    Me.TranslateGridHeader(Grid)
                    UpdateBreadCrum()

                    ControlMgr.SetVisibleControl(Me, trPageSize, False)
                    Me.SortDirection = Me.State.SortExpression
                    PopulateDealerDropdown()
                    PopulateServiceCenterDropDown()
             
                    If Me.State.IsGridVisible Then
                        If Not (Me.State.SelectedPageSize = DEFAULT_NEW_UI_PAGE_SIZE) Or Not (State.SelectedPageSize = Grid.PageSize) Then
                            Grid.PageSize = Me.State.SelectedPageSize
                        End If
                        cboPageSize.SelectedValue = CType(Me.State.SelectedPageSize, String)
                        If Not Me.State.SearchDV Is Nothing AndAlso Me.State.SearchDV.Count > 0 Then
                            Me.PopulateGrid()
                        End If
                    End If
                    Me.SetGridItemStyleColor(Me.Grid)

                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
            Me.ShowMissingTranslations(Me.MasterPage.MessageController)
        End Sub

        Private Sub Page_PageReturn(ByVal ReturnFromUrl As String, ByVal ReturnPar As Object) Handles MyBase.PageReturn
            Try
                Me.MenuEnabled = True
                Me.IsReturningFromChild = True
                Dim retObj As BonusStructureDetailForm.ReturnType = CType(ReturnPar, BonusStructureDetailForm.ReturnType)
                Me.State.HasDataChanged = retObj.HasDataChanged
                Select Case retObj.LastOperation
                    Case ElitaPlusPage.DetailPageCommand.Back
                        If Not retObj Is Nothing Then
                            Me.State.BonusStructureId = retObj.moBonusStructureId
                            Me.State.IsGridVisible = True
                        End If
                    Case ElitaPlusPage.DetailPageCommand.Delete

                        Me.DisplayMessage(Message.DELETE_RECORD_CONFIRMATION, "", MSG_BTN_OK, MSG_TYPE_INFO)
                        Me.State.BonusStructureId = Guid.Empty
                    Case Else
                        Me.State.BonusStructureId = Guid.Empty
                End Select

                Grid.PageIndex = Me.State.PageIndex
                cboPageSize.SelectedValue = CType(Me.State.SelectedPageSize, String)
                Grid.PageSize = Me.State.SelectedPageSize
                ControlMgr.SetVisibleControl(Me, trPageSize, Grid.Visible)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

#End Region

#Region "Controlling Logic"
        Private Sub UpdateBreadCrum()
            If (Not Me.State Is Nothing) Then
                If (Not Me.State Is Nothing) Then
                    Me.MasterPage.BreadCrum = Me.MasterPage.PageTab & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage("BONUS_STRUCTURE")
                    Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("BONUS_STRUCTURE")
                End If
            End If
        End Sub

        Private Sub ClearSearchCriteria()
            Try
                Me.moDealerMultipleDropControl.SelectedIndex = BLANK_ITEM_SELECTED
                Me.moServiceCenterMultipleDropControl.SelectedIndex = BLANK_ITEM_SELECTED
                Me.moProductCode.SelectedIndex = BLANK_ITEM_SELECTED

            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub PopulateDealerDropdown()
            moDealerMultipleDropControl.AutoPostBackDD = True
            moDealerMultipleDropControl.Caption = TranslationBase.TranslateLabelOrMessage(LABEL_DEALER) & "&nbsp;&nbsp;&nbsp;"
            moDealerMultipleDropControl.NothingSelected = True
            moDealerMultipleDropControl.BindData(LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies))
        End Sub
        Private Sub PopulateServiceCenterDropDown()

            moServiceCenterMultipleDropControl.AutoPostBackDD = False
            moServiceCenterMultipleDropControl.Caption = TranslationBase.TranslateLabelOrMessage(LABEL_SERVICE_CENTER)
            moServiceCenterMultipleDropControl.NothingSelected = True
            moServiceCenterMultipleDropControl.BindData(LookupListNew.GetServiceCenterLookupList(ElitaPlusIdentity.Current.ActiveUser.Countries))
        End Sub
        Private Sub PopulateProductCode()
            'Dim oDealerId As Guid = moDealerMultipleDropControl.SelectedGuid
            'Me.BindListControlToDataView(moProductCode, LookupListNew.GetProductCodeLookupList(oDealerId), "CODE", , True) 'ProductCodeList
            Dim listcontext As ListContext = New ListContext()
            listcontext.DealerId = moDealerMultipleDropControl.SelectedGuid
            Dim ProdCodeLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="ProductCodeByDealer", languageCode:=Thread.CurrentPrincipal.GetLanguageCode(), context:=listcontext)
            moProductCode.Populate(ProdCodeLkl, New PopulateOptions() With
                {
                    .AddBlankItem = True,
                    .TextFunc = AddressOf .GetCode,
                    .SortFunc = AddressOf .GetCode
                })

            BindSelectItem(Me.State.ProductCodeId.ToString, moProductCode)

            ControlMgr.SetEnableControl(Me, moProductCode, True)
        End Sub


        Public Sub PopulateGrid()
            If ((Me.State.SearchDV Is Nothing) OrElse (Me.State.HasDataChanged)) Then



                Dim productid As Guid = If(String.IsNullOrEmpty(Me.moProductCode.SelectedValue), Guid.Empty, New Guid(Me.moProductCode.SelectedValue))


                Me.State.SearchDV = ClaimBonusSettings.GetList(Me.moDealerMultipleDropControl.SelectedGuid, Me.moServiceCenterMultipleDropControl.SelectedGuid, productid)
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

            Grid.Columns(GRID_COL_SERVICE_CENTERID_IDX).SortExpression = ClaimBonusSettings.BonusSettingsDV.COL_NAME_SERVICE_CENTER
            Grid.Columns(GRID_COL_DEALERID_IDX).SortExpression = ClaimBonusSettings.BonusSettingsDV.COL_NAME_DEALER_NAME
            Grid.Columns(GRID_COL_PRODUCT_CODE_IDX).SortExpression = ClaimBonusSettings.BonusSettingsDV.COL_NAME_PRODUCT_CODE
            Grid.Columns(GRID_COL_COMPUTE_BONUS_METHODID_IDX).SortExpression = ClaimBonusSettings.BonusSettingsDV.COL_NAME_BONUS_COMPUTE_METHOD
            Grid.Columns(GRID_COL_MAXIMUM_TURNAROUND_TIME_IDX).SortExpression = ClaimBonusSettings.BonusSettingsDV.COL_NAME_SC_AVG_TAT
            Grid.Columns(GRID_COL_PERCENTAGE_OR_AMOUNT_IDX).SortExpression = ClaimBonusSettings.BonusSettingsDV.COL_NAME_PERCENTAGE_OR_AMOUNT
            Grid.Columns(GRID_COL_PRIORITY_IDX).SortExpression = ClaimBonusSettings.BonusSettingsDV.COL_NAME_PRIORITY
            Grid.Columns(GRID_COL_SC_REPLACEMENT_PERCENTAGE_IDX).SortExpression = ClaimBonusSettings.BonusSettingsDV.COL_NAME_SC_REPLACEMENT_PCT
            Grid.Columns(GRID_COL_BONUS_AMOUNT_PERIOD_IDX).SortExpression = ClaimBonusSettings.BonusSettingsDV.COL_NAME_BONUS_AMOUNT_PERIOD
            Grid.Columns(GRID_COL_EFFECTIVE_DATEID_IDX).SortExpression = ClaimBonusSettings.BonusSettingsDV.COL_NAME_EFFECTIVE_DATE
            Grid.Columns(GRID_COL_EXPIRATION_DATEID_IDX).SortExpression = ClaimBonusSettings.BonusSettingsDV.COL_NAME_EXPIRATION_DATE
            HighLightSortColumn(Grid, Me.State.SortExpression)

            SetPageAndSelectedIndexFromGuid(Me.State.SearchDV, Me.State.BonusStructureId, Me.Grid, Me.State.PageIndex)

            Me.Grid.DataSource = Me.State.SearchDV
            HighLightSortColumn(Grid, Me.SortDirection)
            Me.Grid.DataBind()

            ControlMgr.SetVisibleControl(Me, trPageSize, Grid.Visible)

            Session("recCount") = Me.State.SearchDV.Count

            If Not Grid.BottomPagerRow.Visible Then Grid.BottomPagerRow.Visible = True

            Me.lblRecordCount.Text = Me.State.SearchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
        End Sub

        Private Sub SortAndBindGrid()
            If (Me.State.SearchDV.Count = 0) Then
                Me.State.bNoRow = True
                CreateHeaderForEmptyGrid(Grid, Me.SortDirection)
                ControlMgr.SetVisibleControl(Me, Grid, Me.State.IsGridVisible)
                ControlMgr.SetVisibleControl(Me, trPageSize, Me.Grid.Visible)
                Grid.PagerSettings.Visible = True
                If Not Grid.BottomPagerRow.Visible Then Grid.BottomPagerRow.Visible = True
            Else
                Me.State.bNoRow = False
                Me.Grid.Enabled = True
                Me.Grid.DataSource = Me.State.SearchDV
                HighLightSortColumn(Grid, Me.SortDirection)
                Me.Grid.DataBind()
                ControlMgr.SetVisibleControl(Me, Grid, Me.State.IsGridVisible)
                ControlMgr.SetVisibleControl(Me, trPageSize, Me.Grid.Visible)
                If Not Grid.BottomPagerRow.Visible Then Grid.BottomPagerRow.Visible = True
            End If

            If Me.State.SearchDV.Count > 0 Then
                Me.lblRecordCount.Text = Me.State.SearchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
            Else
                Me.lblRecordCount.Text = TranslationBase.TranslateLabelOrMessage(Message.MSG_NO_RECORDS_FOUND)
            End If
            ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me, Grid)
        End Sub
#End Region

#Region "GridView Related"
        Public Property SortDirection() As String
            Get
                Return ViewState("SortDirection").ToString
            End Get
            Set(ByVal value As String)
                ViewState("SortDirection") = value
            End Set
        End Property

        Private Sub Grid_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowDataBound
            Try
                Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
                Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
                Dim btnEditItem As ImageButton



                If Not dvRow Is Nothing And Not Me.State.bNoRow Then
                    If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then
                        Me.PopulateControlFromBOProperty(e.Row.Cells(GRID_COL_BONUS_STRUCTURE_IDX), dvRow(ClaimBonusSettings.BonusSettingsDV.COL_NAME_CLAIM_BONUS_SETTINGS_ID))
                        Me.PopulateControlFromBOProperty(e.Row.Cells(GRID_COL_DEALERID_IDX), dvRow(ClaimBonusSettings.BonusSettingsDV.COL_NAME_DEALER_NAME))
                        Me.PopulateControlFromBOProperty(e.Row.Cells(GRID_COL_SERVICE_CENTERID_IDX), dvRow(ClaimBonusSettings.BonusSettingsDV.COL_NAME_SERVICE_CENTER))
                        Me.PopulateControlFromBOProperty(e.Row.Cells(GRID_COL_PRODUCT_CODE_IDX), dvRow(ClaimBonusSettings.BonusSettingsDV.COL_NAME_PRODUCT_CODE))
                        Me.PopulateControlFromBOProperty(e.Row.Cells(GRID_COL_COMPUTE_BONUS_METHODID_IDX), dvRow(ClaimBonusSettings.BonusSettingsDV.COL_NAME_BONUS_COMPUTE_METHOD))
                        Me.PopulateControlFromBOProperty(e.Row.Cells(GRID_COL_MAXIMUM_TURNAROUND_TIME_IDX), dvRow(ClaimBonusSettings.BonusSettingsDV.COL_NAME_SC_AVG_TAT))
                        Me.PopulateControlFromBOProperty(e.Row.Cells(GRID_COL_PERCENTAGE_OR_AMOUNT_IDX), dvRow(ClaimBonusSettings.BonusSettingsDV.COL_NAME_PERCENTAGE_OR_AMOUNT))
                        Me.PopulateControlFromBOProperty(e.Row.Cells(GRID_COL_PRIORITY_IDX), dvRow(ClaimBonusSettings.BonusSettingsDV.COL_NAME_PRIORITY))
                        Me.PopulateControlFromBOProperty(e.Row.Cells(GRID_COL_SC_REPLACEMENT_PERCENTAGE_IDX), dvRow(ClaimBonusSettings.BonusSettingsDV.COL_NAME_SC_REPLACEMENT_PCT))
                        Me.PopulateControlFromBOProperty(e.Row.Cells(GRID_COL_BONUS_AMOUNT_PERIOD_IDX), dvRow(ClaimBonusSettings.BonusSettingsDV.COL_NAME_BONUS_AMOUNT_PERIOD))
                        Me.PopulateControlFromBOProperty(e.Row.Cells(GRID_COL_EFFECTIVE_DATEID_IDX), dvRow(ClaimBonusSettings.BonusSettingsDV.COL_NAME_EFFECTIVE_DATE))
                        Me.PopulateControlFromBOProperty(e.Row.Cells(GRID_COL_EXPIRATION_DATEID_IDX), dvRow(ClaimBonusSettings.BonusSettingsDV.COL_NAME_EXPIRATION_DATE))

                    End If
                End If
                If (Not e.Row.Cells(GRID_COL_EDIT_IDX).FindControl(BTN_CONTROL_EDIT_DETAIL_LIST) Is Nothing) Then
                    'Edit Button argument changed to id
                    btnEditItem = CType(e.Row.Cells(GRID_COL_EDIT_IDX).FindControl(BTN_CONTROL_EDIT_DETAIL_LIST), ImageButton)
                    If Not (e.Row.Cells(GRID_COL_EXPIRATION_DATEID_IDX).Text.ToString().Equals(String.Empty)) Then
                        If (DateHelper.GetDateValue(e.Row.Cells(GRID_COL_EXPIRATION_DATEID_IDX).Text.ToString()) < DateTime.Now) Then
                            'e.Row.Cells(Me.GRID_COL_EDITID_IDX).Visible = False
                            btnEditItem.Visible = False
                        End If
                    End If
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Public Sub RowCreated(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
            BaseItemCreated(sender, e)
        End Sub

        Private Sub cboPageSize_SelectedIndexChanged(ByVal source As Object, ByVal e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
            Try
                Grid.PageIndex = NewCurrentPageIndex(Grid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
                Me.PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub Grid_Sorting(ByVal source As Object, ByVal e As GridViewSortEventArgs) Handles Grid.Sorting
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

        Private Sub Grid_PageIndexChanging(ByVal source As Object, ByVal e As GridViewPageEventArgs) Handles Grid.PageIndexChanging
            Try
                Me.State.PageIndex = e.NewPageIndex
                Me.PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Public Sub RowCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)
            Try
                If e.CommandName = "SelectAction" Then
                    Dim index As Integer = CInt(e.CommandArgument)
                    Me.State.BonusStructureId = New Guid(Me.Grid.Rows(index).Cells(GRID_COL_BONUS_STRUCTURE_IDX).Text)
                    Me.callPage(BonusStructureDetailForm.URL, Me.State.BonusStructureId)
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try

        End Sub


#End Region

#Region "Button Clicks"
        Private Sub moBtnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
            Try
                Me.State.PageIndex = 0
                If Not State.IsGridVisible Then
                    cboPageSize.SelectedValue = CType(State.SelectedPageSize, String)
                    If Not (Grid.PageSize = DEFAULT_NEW_UI_PAGE_SIZE) Then
                        cboPageSize.SelectedValue = CType(State.SelectedPageSize, String)
                        Grid.PageSize = State.SelectedPageSize
                    End If
                    Me.State.IsGridVisible = True
                End If
                Me.State.SearchDV = Nothing
                Me.State.HasDataChanged = False
                Me.PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub moBtnClearSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClearSearch.Click
            ClearSearchCriteria()
        End Sub

        Private Sub btnNew_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew_WRITE.Click
            Try
                Me.State.BonusStructureId = Guid.Empty
                Me.callPage(BonusStructureDetailForm.URL, Me.State.BonusStructureId)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

#End Region
        Private Sub OnFromDrop_Changed(ByVal fromMultipleDrop As Assurant.ElitaPlus.ElitaPlusWebApp.Common.MultipleColumnDDLabelControl) _
         Handles moDealerMultipleDropControl.SelectedDropChanged
            Try
                Me.State.DealerId = moDealerMultipleDropControl.SelectedGuid
                If moDealerMultipleDropControl.SelectedIndex > 0 Then
                    PopulateProductCode()
                End If
            Catch ex As Exception
                HandleErrors(ex, Me.moErrorController)
            End Try
        End Sub

    End Class
End Namespace

