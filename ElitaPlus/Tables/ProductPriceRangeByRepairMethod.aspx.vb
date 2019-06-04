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

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
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
        Public Sub New(ByVal LastOp As DetailPageCommand, ByVal curProductCodeId As Guid, ByVal hasDataChanged As Boolean)
            Me.LastOperation = LastOp
            Me.EditingId = curProductCodeId
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

    Private Sub Page_PageCall(ByVal CallFromUrl As String, ByVal CallingPar As Object) Handles MyBase.PageCall
        Try
            If Not Me.CallingParameters Is Nothing Then
                'Dim params As Parameters = CType(Me.CallingParameters, Parameters)
                'Me.State.ProductCodeId = params.oProductCodeId
                Me.State.ProductCodeId = CType(Me.CallingParameters, Guid)
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try

    End Sub


#End Region

#Region "Page Parameters"
    Public Class Parameters
        Public oProductCodeId As Guid
        Public Sub New(ByVal ProductCodeId As Guid)
            Me.oProductCodeId = ProductCodeId
        End Sub
    End Class
#End Region

#Region "Page Events"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Try
            Me.ErrorCtrl.Clear_Hide()
            If Not Me.IsPostBack Then
                Me.AddControlMsg(Me.btnDeleteChild_Write, Message.DELETE_RECORD_PROMPT, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, True)
                If Me.State.MyBO Is Nothing Then
                    Me.State.MyBO = New ProdRepairPrice
                End If
                PopulateDropdowns()
                Me.PopulateFormFromBOs()
                Me.EnableDisableFields()
                EnableDisableChildControls(False)
            End If
            BindBoPropertiesToLabels()
            CheckIfComingFromSaveConfirm()
            If Not Me.IsPostBack Then
                Me.AddLabelDecorations(Me.State.MyBO)
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
        Me.ShowMissingTranslations(Me.ErrorCtrl)
    End Sub
#End Region

#Region "Controlling Logic"

    Protected Sub EnableDisableFields()
        If Me.State.IsChildEditing Then
            ControlMgr.SetVisibleControl(Me, PanelAllEditDetail, True)
            EnableDisableParentControls(False)
        Else
            ControlMgr.SetVisibleControl(Me, PanelAllEditDetail, False)
            EnableDisableParentControls(True)
        End If

    End Sub

    Sub EnableDisableParentControls(ByVal enableToggle As Boolean)
        ControlMgr.SetEnableControl(Me, btnBack, enableToggle)
        ControlMgr.SetVisibleControl(Me, DataGridDetail, enableToggle)
        ControlMgr.SetVisibleControl(Me, btnAddNewChildFromGrid_WRITE, enableToggle)
    End Sub

    Sub EnableDisableChildControls(ByVal IsNew As Boolean)
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
        With Me.State
            Me.BindBOPropertyToLabel(.MyBO, "MethodOfRepairId", Me.LabelMethodOfRepair)
            Me.BindBOPropertyToLabel(.MyBO, "PriceRangeFrom", Me.LabelPriceBandRangeFrom)
            Me.BindBOPropertyToLabel(.MyBO, "PriceRangeTo", Me.LabelPriceBandRangeTo)
        End With
        Me.ClearGridHeadersAndLabelsErrSign()
    End Sub

    Protected Sub PopulateDropdowns()
        Dim oLanguageId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
        Try
            ' BindListControlToDataView(DropdownlistMethodOfRepair, LookupListNew.GetMethodOfRepairForRepairsLookupList(oLanguageId), , , True) 'METHR
            Dim methodOfRepairLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("METHR", Thread.CurrentPrincipal.GetLanguageCode())
            Dim filteredRecordList As ListItem() = (From x In methodOfRepairLkl
                                                    Where x.Code = "C" Or x.Code = "H" Or x.Code = "S" Or x.Code = "P" Or x.Code = "R"
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
        Dim oProdCode As New ProductCode(Me.State.ProductCodeId)
        Me.PopulateControlFromBOProperty(Me.moProductCodeText, oProdCode.ProductCode)
        Me.PopulateControlFromBOProperty(Me.moDescriptionText, oProdCode.Description)
        Me.SetSelectedItem(Me.moRiskGroupDrop, oProdCode.RiskGroupId)
    End Sub

    Sub PopulateDetailGrid()
        Dim dv As ProdRepairPrice.ProdRepairPriceSearchDV = ProdRepairPrice.getList(Me.State.ProductCodeId, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
        dv.Sort = Me.State.SortExpressionDetailGrid

        Me.DataGridDetail.Columns(Me.GRID_COL_METHOD_OF_REPAIR).SortExpression = ProdRepairPrice.ProdRepairPriceSearchDV.COL_NAME_METHOD_OF_REPAIR_DESC
        Me.DataGridDetail.Columns(Me.GRID_COL_PRICE_RANGE_FROM).SortExpression = ProdRepairPrice.ProdRepairPriceSearchDV.COL_NAME_PRICE_RANGE_FROM
        Me.DataGridDetail.Columns(Me.GRID_COL_PRICE_RANGE_TO).SortExpression = ProdRepairPrice.ProdRepairPriceSearchDV.COL_NAME_PRICE_RANGE_TO
        Me.SetGridItemStyleColor(Me.DataGridDetail)

        SetPageAndSelectedIndexFromGuid(dv, Me.State.selectedChildId, Me.DataGridDetail, Me.State.DetailPageIndex)
        Me.State.DetailPageIndex = Me.DataGridDetail.CurrentPageIndex

        Me.DataGridDetail.DataSource = dv
        Me.DataGridDetail.AutoGenerateColumns = False
        Me.DataGridDetail.DataBind()
    End Sub

    Protected Sub PopulateBOsFormFrom()
        With Me.State.MyBO
            Me.PopulateBOProperty(Me.State.MyBO, "ProductCodeId", Me.State.ProductCodeId)
            Me.PopulateBOProperty(Me.State.MyBO, "MethodOfRepairId", Me.DropdownlistMethodOfRepair)
            Me.PopulateBOProperty(Me.State.MyBO, "PriceRangeFrom", Me.TextboxPriceBandRangeFrom)
            Me.PopulateBOProperty(Me.State.MyBO, "PriceRangeTo", Me.TextboxPriceBandRangeTo)
        End With
        If Me.ErrCollection.Count > 0 Then
            Throw New PopulateBOErrorException
        End If
    End Sub
    Protected Sub PopulateChildBODetails()
        With Me.State.MyBO
            Me.SetSelectedItem(Me.DropdownlistMethodOfRepair, .MethodOfRepairId)
            Me.PopulateControlFromBOProperty(Me.TextboxPriceBandRangeFrom, .PriceRangeFrom)
            Me.PopulateControlFromBOProperty(Me.TextboxPriceBandRangeTo, .PriceRangeTo)
        End With
    End Sub

    Protected Sub CreateNew()
        Me.State.ScreenSnapShotBO = Nothing 'Reset the backup copy
        Me.State.IsChildEditing = True
        Me.State.MyBO = New ProdRepairPrice
        PopulateDropdowns()
        Me.PopulateFormFromBOs()
        Me.EnableDisableFields()
        EnableDisableChildControls(True)
        PopulateChildBODetails()
    End Sub

    Protected Sub CreateNewWithCopy()
        Me.State.selectedChildId = Guid.Empty
        Me.BeginChildEdit()
        EnableDisableChildControls(True)
    End Sub

    Protected Sub CheckIfComingFromSaveConfirm()
        Dim confResponse As String = Me.HiddenSaveChangesPromptResponse.Value
        If Not confResponse Is Nothing AndAlso confResponse = Me.MSG_VALUE_YES Then
            '  If Me.State.ActionInProgress <> ElitaPlusPage.DetailPageCommand.BackOnErr AndAlso Me.State.ActionInProgress <> ElitaPlusPage.DetailPageCommand.Accept Then
            'Me.State.MyBO.Save()
            'End If
            Select Case Me.State.ActionInProgress
                Case ElitaPlusPage.DetailPageCommand.Back
                    Me.EndChildEdit(ElitaPlusPage.DetailPageCommand.Back)
                Case ElitaPlusPage.DetailPageCommand.New_
                    Me.DisplayMessage(Message.SAVE_RECORD_CONFIRMATION, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
                    Me.CreateNew()
                Case ElitaPlusPage.DetailPageCommand.NewAndCopy
                    Me.DisplayMessage(Message.SAVE_RECORD_CONFIRMATION, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
                    Me.CreateNewWithCopy()
                Case ElitaPlusPage.DetailPageCommand.BackOnErr
                    Me.EndChildEdit(ElitaPlusPage.DetailPageCommand.Back)
                Case ElitaPlusPage.DetailPageCommand.Accept
                    Me.EndChildEdit(ElitaPlusPage.DetailPageCommand.OK)
            End Select
        ElseIf Not confResponse Is Nothing AndAlso confResponse = Me.MSG_VALUE_NO Then
            Select Case Me.State.ActionInProgress
                Case ElitaPlusPage.DetailPageCommand.Back
                    Me.EndChildEdit(ElitaPlusPage.DetailPageCommand.Back)
                Case ElitaPlusPage.DetailPageCommand.New_
                    Me.CreateNew()
                Case ElitaPlusPage.DetailPageCommand.NewAndCopy
                    Me.CreateNewWithCopy()
                Case ElitaPlusPage.DetailPageCommand.BackOnErr
                    Me.ErrorCtrl.AddErrorAndShow(Me.State.LastErrMsg)
                Case ElitaPlusPage.DetailPageCommand.Accept
                    Me.EndChildEdit(ElitaPlusPage.DetailPageCommand.Back)
            End Select
        End If
        'Clean after consuming the action
        Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
        Me.HiddenSaveChangesPromptResponse.Value = ""
    End Sub

    Sub BeginChildEdit()
        Me.State.IsChildEditing = True
        Me.EnableDisableFields()
        EnableDisableChildControls(False)
        With Me.State
            If Not .selectedChildId.Equals(Guid.Empty) Then
                .MyBO = New ProdRepairPrice(CType(.selectedChildId, Guid))
            Else
                .MyBO = New ProdRepairPrice
            End If
        End With
        PopulateChildBODetails()
    End Sub

    Sub EndChildEdit(ByVal lastop As ElitaPlusPage.DetailPageCommand)
        Try
            With Me.State
                Select Case lastop
                    Case ElitaPlusPage.DetailPageCommand.OK
                        Me.PopulateBOsFormFrom()
                        .MyBO.Save()
                        Me.AddInfoMsg(Message.SAVE_RECORD_CONFIRMATION)
                    Case ElitaPlusPage.DetailPageCommand.Cancel
                        .MyBO.cancelEdit()
                    Case ElitaPlusPage.DetailPageCommand.Back
                        .MyBO.cancelEdit()
                    Case ElitaPlusPage.DetailPageCommand.Delete
                        .MyBO.Delete()
                        .MyBO.Save()
                        Me.AddInfoMsg(Message.DELETE_RECORD_CONFIRMATION)
                End Select
            End With
            Me.State.IsChildEditing = False
            Me.EnableDisableFields()
            EnableDisableChildControls(False)
            Me.PopulateDetailGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

#End Region

#Region "Handle-Drop"

#End Region


#Region "Detail Grid Events"



    Public Sub DataGridDetail_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGridDetail.ItemDataBound
        Try
            Dim itemType As ListItemType = CType(e.Item.ItemType, ListItemType)
            Dim dvRow As DataRowView = CType(e.Item.DataItem, DataRowView)

            If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then
                e.Item.Cells(Me.GRID_COL_PG_DETAIL_ID).Text = New Guid(CType(dvRow(ProdRepairPrice.ProdRepairPriceSearchDV.COL_NAME_PROD_REPAIR_PRICE_ID), Byte())).ToString
                e.Item.Cells(Me.GRID_COL_METHOD_OF_REPAIR).Text = dvRow(ProdRepairPrice.ProdRepairPriceSearchDV.COL_NAME_METHOD_OF_REPAIR_DESC).ToString
                e.Item.Cells(Me.GRID_COL_PRICE_RANGE_FROM).Text = GetAmountFormattedDoubleString(dvRow(ProdRepairPrice.ProdRepairPriceSearchDV.COL_NAME_PRICE_RANGE_FROM).ToString)
                e.Item.Cells(Me.GRID_COL_PRICE_RANGE_TO).Text = GetAmountFormattedDoubleString(dvRow(ProdRepairPrice.ProdRepairPriceSearchDV.COL_NAME_PRICE_RANGE_TO).ToString)
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub


    Public Sub ItemCreated(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
        Try
            BaseItemCreated(sender, e)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub


    Private Sub DataGridDetail_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles DataGridDetail.SortCommand
        Try
            If Me.State.SortExpressionDetailGrid.StartsWith(e.SortExpression) Then
                If Me.State.SortExpressionDetailGrid.StartsWith(e.SortExpression & " DESC") Then
                    Me.State.SortExpressionDetailGrid = e.SortExpression
                Else
                    Me.State.SortExpressionDetailGrid = e.SortExpression & " DESC"
                End If
            Else
                Me.State.SortExpressionDetailGrid = e.SortExpression
            End If
            If Me.State.SortExpressionDetailGrid.StartsWith(ProdRepairPrice.ProdRepairPriceSearchDV.COL_NAME_METHOD_OF_REPAIR_DESC) Then
                Me.State.SortExpressionDetailGrid &= ", " & ProdRepairPrice.ProdRepairPriceSearchDV.COL_NAME_METHOD_OF_REPAIR_DESC
            End If
            If Me.State.SortExpressionDetailGrid.StartsWith(ProdRepairPrice.ProdRepairPriceSearchDV.COL_NAME_PRICE_RANGE_TO) Then
                Me.State.SortExpressionDetailGrid &= ", " & ProdRepairPrice.ProdRepairPriceSearchDV.COL_NAME_PRICE_RANGE_TO
            End If
            Me.PopulateDetailGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

    Private Sub DataGridDetail_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGridDetail.ItemCommand
        Try
            Try
                If e.CommandName = "ViewRecord" Then
                    Me.State.IsChildEditing = True
                    Me.State.selectedChildId = New Guid(e.Item.Cells(Me.GRID_COL_PG_DETAIL_ID).Text)
                    Me.BeginChildEdit()
                    Me.EnableDisableFields()
                    EnableDisableChildControls(False)
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

    Private Sub DataGridDetail_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGridDetail.PageIndexChanged
        Try
            Me.State.DetailPageIndex = e.NewPageIndex
            Me.State.selectedChildId = Guid.Empty
            Me.PopulateDetailGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub
#End Region

#Region "Button Clicks"

    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        'Me.callPage(ProductPriceRangeByRepairMethod.URL, Me.State.ProductCodeId)
        Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.ProductCodeId, False))
    End Sub


#Region "Detail Clicks"

    Private Sub btnAddNewChildFromGrid_WRITE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddNewChildFromGrid_WRITE.Click
        Me.CreateNew()
    End Sub

    Private Sub btnAddNewChild_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddNewChild_Write.Click
        Me.PopulateBOsFormFrom()
        If (Me.State.MyBO.IsDirty) Then
            Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
            Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.New_
        Else
            Me.CreateNew()
        End If
    End Sub

    Private Sub btnAddChildWithCopy_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddChildWithCopy_Write.Click
        Try
            Me.PopulateBOsFormFrom()

            If Me.State.MyBO.IsDirty Then
                Me.AddConfirmMsg(Message.SAVE_CHANGES_PROMPT, Me.HiddenSaveChangesPromptResponse)
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.NewAndCopy
            Else
                Me.CreateNewWithCopy()
            End If

        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

    Private Sub btnBackChild_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBackChild.Click
        Try
            Me.PopulateBOsFormFrom()
            Dim iDirtyCols As Integer
            iDirtyCols = Me.State.MyBO.DirtyColumns.Count
            If Me.State.MyBO.IsDirty Then
                If iDirtyCols > AS_DIRTY_COLUMNS_COUNT Then
                    Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", Me.MSG_BTN_YES_NO_CANCEL, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                    Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Accept
                Else
                    Me.EndChildEdit(ElitaPlusPage.DetailPageCommand.Back)
                End If
            Else
                Me.EndChildEdit(ElitaPlusPage.DetailPageCommand.Back)
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
            Me.DisplayMessage(Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR, "", Me.MSG_BTN_YES_NO_CANCEL, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
            Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.BackOnErr
            Me.State.LastErrMsg = Me.ErrorCtrl.Text
        End Try
    End Sub

    Private Sub btnCancelChild_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelChild.Click
        Try
            If Not Me.State.MyBO.IsNew Then
                'Reload from the DB
                Me.State.MyBO = New ProdRepairPrice(Me.State.MyBO.Id)
            ElseIf Not Me.State.ScreenSnapShotBO Is Nothing Then
                'It was a new with copy
                Me.State.MyBO.Clone(Me.State.ScreenSnapShotBO)
            Else
                CreateNew()
            End If
            Me.PopulateChildBODetails()
            Me.EnableDisableFields()
            EnableDisableChildControls(False)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

    Private Sub btnOkChild_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnOkChild_Write.Click
        Try
            Me.EndChildEdit(ElitaPlusPage.DetailPageCommand.OK)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

    Private Sub btnDeleteChild_Write_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDeleteChild_Write.Click
        Try
            Me.EndChildEdit(ElitaPlusPage.DetailPageCommand.Delete)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

#End Region

#End Region

End Class


