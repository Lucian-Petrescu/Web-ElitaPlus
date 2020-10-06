
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
        Private Sub Page_Init(sender As System.Object, e As System.EventArgs) Handles MyBase.Init
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

        Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
            MasterPage.MessageController.Clear_Hide()

            Try

                If Not IsPostBack Then
                    MasterPage.MessageController.Clear()
                    MasterPage.UsePageTabTitleInBreadCrum = False
                    MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("Tables")
                    TranslateGridHeader(Grid)
                    UpdateBreadCrum()

                    ControlMgr.SetVisibleControl(Me, trPageSize, False)
                    SortDirection = State.SortExpression
                    PopulateDealerDropdown()
                    PopulateServiceCenterDropDown()
             
                    If State.IsGridVisible Then
                        If Not (State.SelectedPageSize = DEFAULT_NEW_UI_PAGE_SIZE) Or Not (State.SelectedPageSize = Grid.PageSize) Then
                            Grid.PageSize = State.SelectedPageSize
                        End If
                        cboPageSize.SelectedValue = CType(State.SelectedPageSize, String)
                        If State.SearchDV IsNot Nothing AndAlso State.SearchDV.Count > 0 Then
                            PopulateGrid()
                        End If
                    End If
                    SetGridItemStyleColor(Grid)

                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
            ShowMissingTranslations(MasterPage.MessageController)
        End Sub

        Private Sub Page_PageReturn(ReturnFromUrl As String, ReturnPar As Object) Handles MyBase.PageReturn
            Try
                MenuEnabled = True
                IsReturningFromChild = True
                Dim retObj As BonusStructureDetailForm.ReturnType = CType(ReturnPar, BonusStructureDetailForm.ReturnType)
                State.HasDataChanged = retObj.HasDataChanged
                Select Case retObj.LastOperation
                    Case ElitaPlusPage.DetailPageCommand.Back
                        If retObj IsNot Nothing Then
                            State.BonusStructureId = retObj.moBonusStructureId
                            State.IsGridVisible = True
                        End If
                    Case ElitaPlusPage.DetailPageCommand.Delete

                        DisplayMessage(Message.DELETE_RECORD_CONFIRMATION, "", MSG_BTN_OK, MSG_TYPE_INFO)
                        State.BonusStructureId = Guid.Empty
                    Case Else
                        State.BonusStructureId = Guid.Empty
                End Select

                Grid.PageIndex = State.PageIndex
                cboPageSize.SelectedValue = CType(State.SelectedPageSize, String)
                Grid.PageSize = State.SelectedPageSize
                ControlMgr.SetVisibleControl(Me, trPageSize, Grid.Visible)
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

#End Region

#Region "Controlling Logic"
        Private Sub UpdateBreadCrum()
            If (State IsNot Nothing) Then
                If (State IsNot Nothing) Then
                    MasterPage.BreadCrum = MasterPage.PageTab & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage("BONUS_STRUCTURE")
                    MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("BONUS_STRUCTURE")
                End If
            End If
        End Sub

        Private Sub ClearSearchCriteria()
            Try
                moDealerMultipleDropControl.SelectedIndex = BLANK_ITEM_SELECTED
                moServiceCenterMultipleDropControl.SelectedIndex = BLANK_ITEM_SELECTED
                moProductCode.SelectedIndex = BLANK_ITEM_SELECTED

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
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

            BindSelectItem(State.ProductCodeId.ToString, moProductCode)

            ControlMgr.SetEnableControl(Me, moProductCode, True)
        End Sub


        Public Sub PopulateGrid()
            If ((State.SearchDV Is Nothing) OrElse (State.HasDataChanged)) Then



                Dim productid As Guid = If(String.IsNullOrEmpty(moProductCode.SelectedValue), Guid.Empty, New Guid(moProductCode.SelectedValue))


                State.SearchDV = ClaimBonusSettings.GetList(moDealerMultipleDropControl.SelectedGuid, moServiceCenterMultipleDropControl.SelectedGuid, productid)
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
            HighLightSortColumn(Grid, State.SortExpression)

            SetPageAndSelectedIndexFromGuid(State.SearchDV, State.BonusStructureId, Grid, State.PageIndex)

            Grid.DataSource = State.SearchDV
            HighLightSortColumn(Grid, SortDirection)
            Grid.DataBind()

            ControlMgr.SetVisibleControl(Me, trPageSize, Grid.Visible)

            Session("recCount") = State.SearchDV.Count

            If Not Grid.BottomPagerRow.Visible Then Grid.BottomPagerRow.Visible = True

            lblRecordCount.Text = State.SearchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
        End Sub

        Private Sub SortAndBindGrid()
            If (State.SearchDV.Count = 0) Then
                State.bNoRow = True
                CreateHeaderForEmptyGrid(Grid, SortDirection)
                ControlMgr.SetVisibleControl(Me, Grid, State.IsGridVisible)
                ControlMgr.SetVisibleControl(Me, trPageSize, Grid.Visible)
                Grid.PagerSettings.Visible = True
                If Not Grid.BottomPagerRow.Visible Then Grid.BottomPagerRow.Visible = True
            Else
                State.bNoRow = False
                Grid.Enabled = True
                Grid.DataSource = State.SearchDV
                HighLightSortColumn(Grid, SortDirection)
                Grid.DataBind()
                ControlMgr.SetVisibleControl(Me, Grid, State.IsGridVisible)
                ControlMgr.SetVisibleControl(Me, trPageSize, Grid.Visible)
                If Not Grid.BottomPagerRow.Visible Then Grid.BottomPagerRow.Visible = True
            End If

            If State.SearchDV.Count > 0 Then
                lblRecordCount.Text = State.SearchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
            Else
                lblRecordCount.Text = TranslationBase.TranslateLabelOrMessage(Message.MSG_NO_RECORDS_FOUND)
            End If
            ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me, Grid)
        End Sub
#End Region

#Region "GridView Related"
        Public Property SortDirection() As String
            Get
                Return ViewState("SortDirection").ToString
            End Get
            Set(value As String)
                ViewState("SortDirection") = value
            End Set
        End Property

        Private Sub Grid_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowDataBound
            Try
                Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
                Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
                Dim btnEditItem As ImageButton



                If dvRow IsNot Nothing And Not State.bNoRow Then
                    If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then
                        PopulateControlFromBOProperty(e.Row.Cells(GRID_COL_BONUS_STRUCTURE_IDX), dvRow(ClaimBonusSettings.BonusSettingsDV.COL_NAME_CLAIM_BONUS_SETTINGS_ID))
                        PopulateControlFromBOProperty(e.Row.Cells(GRID_COL_DEALERID_IDX), dvRow(ClaimBonusSettings.BonusSettingsDV.COL_NAME_DEALER_NAME))
                        PopulateControlFromBOProperty(e.Row.Cells(GRID_COL_SERVICE_CENTERID_IDX), dvRow(ClaimBonusSettings.BonusSettingsDV.COL_NAME_SERVICE_CENTER))
                        PopulateControlFromBOProperty(e.Row.Cells(GRID_COL_PRODUCT_CODE_IDX), dvRow(ClaimBonusSettings.BonusSettingsDV.COL_NAME_PRODUCT_CODE))
                        PopulateControlFromBOProperty(e.Row.Cells(GRID_COL_COMPUTE_BONUS_METHODID_IDX), dvRow(ClaimBonusSettings.BonusSettingsDV.COL_NAME_BONUS_COMPUTE_METHOD))
                        PopulateControlFromBOProperty(e.Row.Cells(GRID_COL_MAXIMUM_TURNAROUND_TIME_IDX), dvRow(ClaimBonusSettings.BonusSettingsDV.COL_NAME_SC_AVG_TAT))
                        PopulateControlFromBOProperty(e.Row.Cells(GRID_COL_PERCENTAGE_OR_AMOUNT_IDX), dvRow(ClaimBonusSettings.BonusSettingsDV.COL_NAME_PERCENTAGE_OR_AMOUNT))
                        PopulateControlFromBOProperty(e.Row.Cells(GRID_COL_PRIORITY_IDX), dvRow(ClaimBonusSettings.BonusSettingsDV.COL_NAME_PRIORITY))
                        PopulateControlFromBOProperty(e.Row.Cells(GRID_COL_SC_REPLACEMENT_PERCENTAGE_IDX), dvRow(ClaimBonusSettings.BonusSettingsDV.COL_NAME_SC_REPLACEMENT_PCT))
                        PopulateControlFromBOProperty(e.Row.Cells(GRID_COL_BONUS_AMOUNT_PERIOD_IDX), dvRow(ClaimBonusSettings.BonusSettingsDV.COL_NAME_BONUS_AMOUNT_PERIOD))
                        PopulateControlFromBOProperty(e.Row.Cells(GRID_COL_EFFECTIVE_DATEID_IDX), dvRow(ClaimBonusSettings.BonusSettingsDV.COL_NAME_EFFECTIVE_DATE))
                        PopulateControlFromBOProperty(e.Row.Cells(GRID_COL_EXPIRATION_DATEID_IDX), dvRow(ClaimBonusSettings.BonusSettingsDV.COL_NAME_EXPIRATION_DATE))

                    End If
                End If
                If (e.Row.Cells(GRID_COL_EDIT_IDX).FindControl(BTN_CONTROL_EDIT_DETAIL_LIST) IsNot Nothing) Then
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
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Public Sub RowCreated(sender As System.Object, e As System.Web.UI.WebControls.GridViewRowEventArgs)
            BaseItemCreated(sender, e)
        End Sub

        Private Sub cboPageSize_SelectedIndexChanged(source As Object, e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
            Try
                Grid.PageIndex = NewCurrentPageIndex(Grid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub Grid_Sorting(source As Object, e As GridViewSortEventArgs) Handles Grid.Sorting
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

        Private Sub Grid_PageIndexChanging(source As Object, e As GridViewPageEventArgs) Handles Grid.PageIndexChanging
            Try
                State.PageIndex = e.NewPageIndex
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Public Sub RowCommand(source As System.Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs)
            Try
                If e.CommandName = "SelectAction" Then
                    Dim index As Integer = CInt(e.CommandArgument)
                    State.BonusStructureId = New Guid(Grid.Rows(index).Cells(GRID_COL_BONUS_STRUCTURE_IDX).Text)
                    callPage(BonusStructureDetailForm.URL, State.BonusStructureId)
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub


#End Region

#Region "Button Clicks"
        Private Sub moBtnSearch_Click(sender As Object, e As System.EventArgs) Handles btnSearch.Click
            Try
                State.PageIndex = 0
                If Not State.IsGridVisible Then
                    cboPageSize.SelectedValue = CType(State.SelectedPageSize, String)
                    If Not (Grid.PageSize = DEFAULT_NEW_UI_PAGE_SIZE) Then
                        cboPageSize.SelectedValue = CType(State.SelectedPageSize, String)
                        Grid.PageSize = State.SelectedPageSize
                    End If
                    State.IsGridVisible = True
                End If
                State.SearchDV = Nothing
                State.HasDataChanged = False
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub moBtnClearSearch_Click(sender As Object, e As System.EventArgs) Handles btnClearSearch.Click
            ClearSearchCriteria()
        End Sub

        Private Sub btnNew_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnNew_WRITE.Click
            Try
                State.BonusStructureId = Guid.Empty
                callPage(BonusStructureDetailForm.URL, State.BonusStructureId)
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

#End Region
        Private Sub OnFromDrop_Changed(fromMultipleDrop As Assurant.ElitaPlus.ElitaPlusWebApp.Common.MultipleColumnDDLabelControl) _
         Handles moDealerMultipleDropControl.SelectedDropChanged
            Try
                State.DealerId = moDealerMultipleDropControl.SelectedGuid
                If moDealerMultipleDropControl.SelectedIndex > 0 Then
                    PopulateProductCode()
                End If
            Catch ex As Exception
                HandleErrors(ex, moErrorController)
            End Try
        End Sub

    End Class
End Namespace

