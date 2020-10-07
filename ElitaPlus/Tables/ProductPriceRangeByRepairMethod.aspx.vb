Imports System.Globalization
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports System.Threading
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms
Partial Class ProductPriceRangeByRepairMethod
    Inherits ElitaPlusSearchPage

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

#Region "Constants"
    Private Const PRDREPAIRPRICE_FORM001 As String = "PRDREPAIRPRICE_FORM001" ' Maintain ProductCode repair price Fetch Exception
    'Public Const URL As String = "ProductCodeForm.aspx"
    Public Const URL As String = "ProductPriceRangeByRepairMethod.aspx"


    Public Const GRID_COL_EDIT As Integer = 0
    Public Const GRID_COL_PRICE_RANGE_FROM As Integer = 1
    Public Const GRID_COL_PRICE_RANGE_TO As Integer = 2
    Public Const GRID_COL_METHOD_OF_REPAIR As Integer = 3
    Public Const GRID_COL_PG_DETAIL_ID As Integer = 4

    Public Const AS_DIRTY_COLUMNS_COUNT As Integer = 2
#End Region


#Region "Page Return Type"
    Public Class ReturnType
        Public LastOperation As DetailPageCommand
        Public EditingId As Guid
        Public HasDataChanged As Boolean
        Public Sub New(LastOp As DetailPageCommand, curProductCodeId As Guid, hasDataChanged As Boolean)
            LastOperation = LastOp
            EditingId = curProductCodeId
            Me.HasDataChanged = hasDataChanged
        End Sub
    End Class

#End Region

#Region "Page State"
    Class MyState
        Public MyBO As ProdRepairPrice
        Public ScreenSnapShotBO As ProdRepairPrice
        Public moProdRepairPriceId As Guid = Guid.Empty
        Public IsProdRepairPriceNew As Boolean = False
        Public ActionInProgress As DetailPageCommand = DetailPageCommand.Nothing_
        Public LastErrMsg As String
        Public selectedChildId As Guid = Guid.Empty
        Public DetailPageIndex As Integer = 0

        Public ProductCodeId As Guid
        Public IsChildEditing As Boolean = False
        Public SortExpressionDetailGrid As String = ProdRepairPrice.ProdRepairPriceSearchDV.COL_NAME_PRICE_RANGE_FROM
    End Class

    Public Sub New()
        MyBase.New(New MyState)
    End Sub

    Protected Shadows ReadOnly Property State() As MyState
        Get
            Return CType(MyBase.State, MyState)
        End Get
    End Property

    Private Sub Page_PageCall(CallFromUrl As String, CallingPar As Object) Handles MyBase.PageCall
        Try
            If CallingParameters IsNot Nothing Then
                'Dim params As Parameters = CType(Me.CallingParameters, Parameters)
                'Me.State.ProductCodeId = params.oProductCodeId
                State.ProductCodeId = CType(CallingParameters, Guid)
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try

    End Sub


#End Region

#Region "Page Parameters"
    Public Class Parameters
        Public oProductCodeId As Guid
        Public Sub New(ProductCodeId As Guid)
            oProductCodeId = ProductCodeId
        End Sub
    End Class
#End Region

#Region "Page Events"
    Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Try
            ErrorCtrl.Clear_Hide()
            If Not IsPostBack Then
                AddControlMsg(btnDeleteChild_Write, Message.DELETE_RECORD_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, True)
                If State.MyBO Is Nothing Then
                    State.MyBO = New ProdRepairPrice
                End If
                PopulateDropdowns()
                PopulateFormFromBOs()
                EnableDisableFields()
                EnableDisableChildControls(False)
            End If
            BindBoPropertiesToLabels()
            CheckIfComingFromSaveConfirm()
            If Not IsPostBack Then
                AddLabelDecorations(State.MyBO)
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
        ShowMissingTranslations(ErrorCtrl)
    End Sub
#End Region

#Region "Controlling Logic"

    Protected Sub EnableDisableFields()
        If State.IsChildEditing Then
            ControlMgr.SetVisibleControl(Me, PanelAllEditDetail, True)
            EnableDisableParentControls(False)
        Else
            ControlMgr.SetVisibleControl(Me, PanelAllEditDetail, False)
            EnableDisableParentControls(True)
        End If

    End Sub

    Sub EnableDisableParentControls(enableToggle As Boolean)
        ControlMgr.SetEnableControl(Me, btnBack, enableToggle)
        ControlMgr.SetVisibleControl(Me, DataGridDetail, enableToggle)
        ControlMgr.SetVisibleControl(Me, btnAddNewChildFromGrid_WRITE, enableToggle)
    End Sub

    Sub EnableDisableChildControls(IsNew As Boolean)
        'Enable by default
        ControlMgr.SetEnableControl(Me, btnDeleteChild_Write, True)
        ControlMgr.SetEnableControl(Me, btnAddChildWithCopy_Write, True)
        ControlMgr.SetEnableControl(Me, btnAddNewChild_Write, True)

        If IsNew = True Then
            ControlMgr.SetEnableControl(Me, btnDeleteChild_Write, False)
            ControlMgr.SetEnableControl(Me, btnAddChildWithCopy_Write, False)
            ControlMgr.SetEnableControl(Me, btnAddNewChild_Write, False)
        End If

    End Sub

    Protected Sub BindBoPropertiesToLabels()
        With State
            BindBOPropertyToLabel(.MyBO, "MethodOfRepairId", LabelMethodOfRepair)
            BindBOPropertyToLabel(.MyBO, "PriceRangeFrom", LabelPriceBandRangeFrom)
            BindBOPropertyToLabel(.MyBO, "PriceRangeTo", LabelPriceBandRangeTo)
        End With
        ClearGridHeadersAndLabelsErrSign()
    End Sub

    Protected Sub PopulateDropdowns()
        Dim oLanguageId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
        Try
            ' BindListControlToDataView(DropdownlistMethodOfRepair, LookupListNew.GetMethodOfRepairForRepairsLookupList(oLanguageId), , , True) 'METHR
            Dim methodOfRepairLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("METHR", Thread.CurrentPrincipal.GetLanguageCode())
            Dim filteredRecordList As ListItem() = (From x In methodOfRepairLkl
                                                    Where x.Code = "C" OrElse x.Code = "H" OrElse x.Code = "S" OrElse x.Code = "P" OrElse x.Code = "R"
                                                    Select x).ToArray()
            DropdownlistMethodOfRepair.Populate(filteredRecordList, New PopulateOptions() With
                    {
                  .AddBlankItem = True
                    })
            'BindListControlToDataView(moRiskGroupDrop, LookupListNew.GetRiskGroupsLookupList(oLanguageId), , , True) 'RISK_GROUPS
            moRiskGroupDrop.Populate(CommonConfigManager.Current.ListManager.GetList("RGRP", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
                    {
                  .AddBlankItem = True
                    })
        Catch ex As Exception
            ErrorCtrl.AddError(PRDREPAIRPRICE_FORM001)
            ErrorCtrl.AddError(ex.Message, False)
            ErrorCtrl.Show()
        End Try
    End Sub

    Protected Sub PopulateFormFromBOs()
        PopulateDetailGrid()
        Dim oProdCode As New ProductCode(State.ProductCodeId)
        PopulateControlFromBOProperty(moProductCodeText, oProdCode.ProductCode)
        PopulateControlFromBOProperty(moDescriptionText, oProdCode.Description)
        SetSelectedItem(moRiskGroupDrop, oProdCode.RiskGroupId)
    End Sub

    Sub PopulateDetailGrid()
        Dim dv As ProdRepairPrice.ProdRepairPriceSearchDV = ProdRepairPrice.getList(State.ProductCodeId, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
        dv.Sort = State.SortExpressionDetailGrid

        DataGridDetail.Columns(GRID_COL_METHOD_OF_REPAIR).SortExpression = ProdRepairPrice.ProdRepairPriceSearchDV.COL_NAME_METHOD_OF_REPAIR_DESC
        DataGridDetail.Columns(GRID_COL_PRICE_RANGE_FROM).SortExpression = ProdRepairPrice.ProdRepairPriceSearchDV.COL_NAME_PRICE_RANGE_FROM
        DataGridDetail.Columns(GRID_COL_PRICE_RANGE_TO).SortExpression = ProdRepairPrice.ProdRepairPriceSearchDV.COL_NAME_PRICE_RANGE_TO
        SetGridItemStyleColor(DataGridDetail)

        SetPageAndSelectedIndexFromGuid(dv, State.selectedChildId, DataGridDetail, State.DetailPageIndex)
        State.DetailPageIndex = DataGridDetail.CurrentPageIndex

        DataGridDetail.DataSource = dv
        DataGridDetail.AutoGenerateColumns = False
        DataGridDetail.DataBind()
    End Sub

    Protected Sub PopulateBOsFormFrom()
        With State.MyBO
            PopulateBOProperty(State.MyBO, "ProductCodeId", State.ProductCodeId)
            PopulateBOProperty(State.MyBO, "MethodOfRepairId", DropdownlistMethodOfRepair)
            PopulateBOProperty(State.MyBO, "PriceRangeFrom", TextboxPriceBandRangeFrom)
            PopulateBOProperty(State.MyBO, "PriceRangeTo", TextboxPriceBandRangeTo)
        End With
        If ErrCollection.Count > 0 Then
            Throw New PopulateBOErrorException
        End If
    End Sub
    Protected Sub PopulateChildBODetails()
        With State.MyBO
            SetSelectedItem(DropdownlistMethodOfRepair, .MethodOfRepairId)
            PopulateControlFromBOProperty(TextboxPriceBandRangeFrom, .PriceRangeFrom)
            PopulateControlFromBOProperty(TextboxPriceBandRangeTo, .PriceRangeTo)
        End With
    End Sub

    Protected Sub CreateNew()
        State.ScreenSnapShotBO = Nothing 'Reset the backup copy
        State.IsChildEditing = True
        State.MyBO = New ProdRepairPrice
        PopulateDropdowns()
        PopulateFormFromBOs()
        EnableDisableFields()
        EnableDisableChildControls(True)
        PopulateChildBODetails()
    End Sub

    Protected Sub CreateNewWithCopy()
        State.selectedChildId = Guid.Empty
        BeginChildEdit()
        EnableDisableChildControls(True)
    End Sub

    Protected Sub CheckIfComingFromSaveConfirm()
        Dim confResponse As String = HiddenSaveChangesPromptResponse.Value
        If confResponse IsNot Nothing AndAlso confResponse = MSG_VALUE_YES Then
            '  If Me.State.ActionInProgress <> ElitaPlusPage.DetailPageCommand.BackOnErr AndAlso Me.State.ActionInProgress <> ElitaPlusPage.DetailPageCommand.Accept Then
            'Me.State.MyBO.Save()
            'End If
            Select Case State.ActionInProgress
                Case ElitaPlusPage.DetailPageCommand.Back
                    EndChildEdit(ElitaPlusPage.DetailPageCommand.Back)
                Case ElitaPlusPage.DetailPageCommand.New_
                    DisplayMessage(Message.SAVE_RECORD_CONFIRMATION, "", MSG_BTN_OK, MSG_TYPE_INFO)
                    CreateNew()
                Case ElitaPlusPage.DetailPageCommand.NewAndCopy
                    DisplayMessage(Message.SAVE_RECORD_CONFIRMATION, "", MSG_BTN_OK, MSG_TYPE_INFO)
                    CreateNewWithCopy()
                Case ElitaPlusPage.DetailPageCommand.BackOnErr
                    EndChildEdit(ElitaPlusPage.DetailPageCommand.Back)
                Case ElitaPlusPage.DetailPageCommand.Accept
                    EndChildEdit(ElitaPlusPage.DetailPageCommand.OK)
            End Select
        ElseIf confResponse IsNot Nothing AndAlso confResponse = MSG_VALUE_NO Then
            Select Case State.ActionInProgress
                Case ElitaPlusPage.DetailPageCommand.Back
                    EndChildEdit(ElitaPlusPage.DetailPageCommand.Back)
                Case ElitaPlusPage.DetailPageCommand.New_
                    CreateNew()
                Case ElitaPlusPage.DetailPageCommand.NewAndCopy
                    CreateNewWithCopy()
                Case ElitaPlusPage.DetailPageCommand.BackOnErr
                    ErrorCtrl.AddErrorAndShow(State.LastErrMsg)
                Case ElitaPlusPage.DetailPageCommand.Accept
                    EndChildEdit(ElitaPlusPage.DetailPageCommand.Back)
            End Select
        End If
        'Clean after consuming the action
        State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
        HiddenSaveChangesPromptResponse.Value = ""
    End Sub

    Sub BeginChildEdit()
        State.IsChildEditing = True
        EnableDisableFields()
        EnableDisableChildControls(False)
        With State
            If Not .selectedChildId.Equals(Guid.Empty) Then
                .MyBO = New ProdRepairPrice(CType(.selectedChildId, Guid))
            Else
                .MyBO = New ProdRepairPrice
            End If
        End With
        PopulateChildBODetails()
    End Sub

    Sub EndChildEdit(lastop As ElitaPlusPage.DetailPageCommand)
        Try
            With State
                Select Case lastop
                    Case ElitaPlusPage.DetailPageCommand.OK
                        PopulateBOsFormFrom()
                        .MyBO.Save()
                        AddInfoMsg(Message.SAVE_RECORD_CONFIRMATION)
                    Case ElitaPlusPage.DetailPageCommand.Cancel
                        .MyBO.cancelEdit()
                    Case ElitaPlusPage.DetailPageCommand.Back
                        .MyBO.cancelEdit()
                    Case ElitaPlusPage.DetailPageCommand.Delete
                        .MyBO.Delete()
                        .MyBO.Save()
                        AddInfoMsg(Message.DELETE_RECORD_CONFIRMATION)
                End Select
            End With
            State.IsChildEditing = False
            EnableDisableFields()
            EnableDisableChildControls(False)
            PopulateDetailGrid()
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

#End Region

#Region "Handle-Drop"

#End Region


#Region "Detail Grid Events"



    Public Sub DataGridDetail_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGridDetail.ItemDataBound
        Try
            Dim itemType As ListItemType = CType(e.Item.ItemType, ListItemType)
            Dim dvRow As DataRowView = CType(e.Item.DataItem, DataRowView)

            If itemType = ListItemType.Item OrElse itemType = ListItemType.AlternatingItem OrElse itemType = ListItemType.SelectedItem Then
                e.Item.Cells(GRID_COL_PG_DETAIL_ID).Text = New Guid(CType(dvRow(ProdRepairPrice.ProdRepairPriceSearchDV.COL_NAME_PROD_REPAIR_PRICE_ID), Byte())).ToString
                e.Item.Cells(GRID_COL_METHOD_OF_REPAIR).Text = dvRow(ProdRepairPrice.ProdRepairPriceSearchDV.COL_NAME_METHOD_OF_REPAIR_DESC).ToString
                e.Item.Cells(GRID_COL_PRICE_RANGE_FROM).Text = GetAmountFormattedDoubleString(dvRow(ProdRepairPrice.ProdRepairPriceSearchDV.COL_NAME_PRICE_RANGE_FROM).ToString)
                e.Item.Cells(GRID_COL_PRICE_RANGE_TO).Text = GetAmountFormattedDoubleString(dvRow(ProdRepairPrice.ProdRepairPriceSearchDV.COL_NAME_PRICE_RANGE_TO).ToString)
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub


    Public Sub ItemCreated(sender As System.Object, e As System.Web.UI.WebControls.DataGridItemEventArgs)
        Try
            BaseItemCreated(sender, e)
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub


    Private Sub DataGridDetail_SortCommand(source As Object, e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles DataGridDetail.SortCommand
        Try
            If State.SortExpressionDetailGrid.StartsWith(e.SortExpression) Then
                If State.SortExpressionDetailGrid.StartsWith(e.SortExpression & " DESC") Then
                    State.SortExpressionDetailGrid = e.SortExpression
                Else
                    State.SortExpressionDetailGrid = e.SortExpression & " DESC"
                End If
            Else
                State.SortExpressionDetailGrid = e.SortExpression
            End If
            If State.SortExpressionDetailGrid.StartsWith(ProdRepairPrice.ProdRepairPriceSearchDV.COL_NAME_METHOD_OF_REPAIR_DESC) Then
                State.SortExpressionDetailGrid &= ", " & ProdRepairPrice.ProdRepairPriceSearchDV.COL_NAME_METHOD_OF_REPAIR_DESC
            End If
            If State.SortExpressionDetailGrid.StartsWith(ProdRepairPrice.ProdRepairPriceSearchDV.COL_NAME_PRICE_RANGE_TO) Then
                State.SortExpressionDetailGrid &= ", " & ProdRepairPrice.ProdRepairPriceSearchDV.COL_NAME_PRICE_RANGE_TO
            End If
            PopulateDetailGrid()
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

    Private Sub DataGridDetail_ItemCommand(source As Object, e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGridDetail.ItemCommand
        Try
            Try
                If e.CommandName = "ViewRecord" Then
                    State.IsChildEditing = True
                    State.selectedChildId = New Guid(e.Item.Cells(GRID_COL_PG_DETAIL_ID).Text)
                    BeginChildEdit()
                    EnableDisableFields()
                    EnableDisableChildControls(False)
                End If
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

    Private Sub DataGridDetail_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGridDetail.PageIndexChanged
        Try
            State.DetailPageIndex = e.NewPageIndex
            State.selectedChildId = Guid.Empty
            PopulateDetailGrid()
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub
#End Region

#Region "Button Clicks"

    Private Sub btnBack_Click(sender As System.Object, e As System.EventArgs) Handles btnBack.Click
        'Me.callPage(ProductPriceRangeByRepairMethod.URL, Me.State.ProductCodeId)
        ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, State.ProductCodeId, False))
    End Sub


#Region "Detail Clicks"

    Private Sub btnAddNewChildFromGrid_WRITE_Click(sender As Object, e As System.EventArgs) Handles btnAddNewChildFromGrid_WRITE.Click
        CreateNew()
    End Sub

    Private Sub btnAddNewChild_Click(sender As Object, e As System.EventArgs) Handles btnAddNewChild_Write.Click
        PopulateBOsFormFrom()
        If (State.MyBO.IsDirty) Then
            DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
            State.ActionInProgress = ElitaPlusPage.DetailPageCommand.New_
        Else
            CreateNew()
        End If
    End Sub

    Private Sub btnAddChildWithCopy_Click(sender As Object, e As System.EventArgs) Handles btnAddChildWithCopy_Write.Click
        Try
            PopulateBOsFormFrom()

            If State.MyBO.IsDirty Then
                AddConfirmMsg(Message.SAVE_CHANGES_PROMPT, HiddenSaveChangesPromptResponse)
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.NewAndCopy
            Else
                CreateNewWithCopy()
            End If

        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

    Private Sub btnBackChild_Click(sender As System.Object, e As System.EventArgs) Handles btnBackChild.Click
        Try
            PopulateBOsFormFrom()
            Dim iDirtyCols As Integer
            iDirtyCols = State.MyBO.DirtyColumns.Count
            If State.MyBO.IsDirty Then
                If iDirtyCols > AS_DIRTY_COLUMNS_COUNT Then
                    DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO_CANCEL, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                    State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Accept
                Else
                    EndChildEdit(ElitaPlusPage.DetailPageCommand.Back)
                End If
            Else
                EndChildEdit(ElitaPlusPage.DetailPageCommand.Back)
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
            DisplayMessage(Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR, "", MSG_BTN_YES_NO_CANCEL, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
            State.ActionInProgress = ElitaPlusPage.DetailPageCommand.BackOnErr
            State.LastErrMsg = ErrorCtrl.Text
        End Try
    End Sub

    Private Sub btnCancelChild_Click(sender As Object, e As System.EventArgs) Handles btnCancelChild.Click
        Try
            If Not State.MyBO.IsNew Then
                'Reload from the DB
                State.MyBO = New ProdRepairPrice(State.MyBO.Id)
            ElseIf State.ScreenSnapShotBO IsNot Nothing Then
                'It was a new with copy
                State.MyBO.Clone(State.ScreenSnapShotBO)
            Else
                CreateNew()
            End If
            PopulateChildBODetails()
            EnableDisableFields()
            EnableDisableChildControls(False)
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

    Private Sub btnOkChild_Click(sender As Object, e As System.EventArgs) Handles btnOkChild_Write.Click
        Try
            EndChildEdit(ElitaPlusPage.DetailPageCommand.OK)
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

    Private Sub btnDeleteChild_Write_Click(sender As Object, e As System.EventArgs) Handles btnDeleteChild_Write.Click
        Try
            EndChildEdit(ElitaPlusPage.DetailPageCommand.Delete)
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

#End Region

#End Region

End Class


