 Imports System.ComponentModel
 Imports Assurant.ElitaPlus.BusinessObjects.Common
 Imports Assurant.ElitaPlus.DALObjects
Partial Class UserControlDealerInflation
    Inherits System.Web.UI.UserControl

    Public Class RequestDataEventArgs
        Inherits EventArgs

        Public Data As  DealerInflation.DealerInflationDV

    End Class

    Public Delegate Sub RequestData(ByVal sender As Object, ByRef e As RequestDataEventArgs)

    Public Event RequestClaimCloseRulesData As RequestData
    Public Event PropertyChanged As PropertyChangedEventHandler

    Private _companyId As Guid
    Private _dealerId As Nullable(Of Guid)

    #Region "Constants"
    Private moState As MyState

    Private Const GRID_COL_DEALER_INFLATION_ID As Integer = 0
    Private Const GRID_COL_DEALER_ID As Integer = 1
    Private Const GRID_COL_INFLATION_MONTH As Integer = 2
    Private Const GRID_COL_INFLATION_YEAR As Integer = 3
    Private Const GRID_COL_INFLATION_PCT As Integer = 4
  

    Private Const GRID_CTRL_NAME_LABEL__DEALER As String = "lblDealer"
    Private Const GRID_CTRL_NAME_LABEL_INFLATION_MONTH As String = "lblInflationMonth"
    Private Const GRID_CTRL_NAME_LABLE_INFLATION_YEAR As String = "lblInflationYear"
    Private Const GRID_CTRL_NAME_LABEL_INFLATION_PCT As String = "lblInflationPct"
    
    Private Const GRID_CTRL_NAME_EDIT_INFLATION_MONTH As String = "cboInflationMonth"
    Private Const GRID_CTRL_NAME_EDIT_INFLATION_YEAR As String = "cboInflationYear"
    Private Const GRID_CTRL_NAME_EDIT_INFLATION_PCT As String = "txtInflationPct"
   
    Private Const GRID_CTRL_NAME_DELETE_CLAIM_RULE As String = "DeleteButton_WRITE"

    Private Const MSG_CONFIRM_PROMPT As String = "MSG_CONFIRM_PROMPT"
    Private Const MSG_RECORD_DELETED_OK As String = "MSG_RECORD_DELETED_OK"
    Private Const MSG_RECORD_SAVED_OK As String = "MSG_RECORD_SAVED_OK"
    Private Const MSG_RECORD_NOT_SAVED As String = "MSG_RECORD_NOT_SAVED"

    Private Const EDIT_COMMAND As String = "SelectAction"
    Private Const DELETE_COMMAND As String = "DeleteRecord"
    Private Const SORT_COMMAND As String = "Sort"

    Private Const NO_ROW_SELECTED_INDEX As Integer = -1
    Private Const CONST_ACTIVE_FLAG_YES As String = "Y"

    Public Const SESSION_LOCALSTATE_KEY As String = "DEALERINFLATION_SESSION_LOCALSTATE_KEY"
    

#End Region

    Public Property DealerId As Nullable(Of Guid)
        Get
            Return Me.TheState.dealerId
        End Get
        Set(ByVal value As Nullable(Of Guid))
            Me.TheState.dealerId = CType(value, Guid)
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("DealerId"))
        End Set
    End Property

    #Region "Page State"
  Class MyState
        Public MyBO As DealerInflation
        Public dtDealerInflation As DataTable
        Public DefaultDealerInflationID As Guid
        Public PageIndex As Integer = 0
        Public PageSize As Integer = 10
        Public DealerInflationDV As DealerInflation.DealerInflationDV = Nothing
        Public HasDataChanged As Boolean
        Public IsGridAddNew As Boolean = False
        Public IsEditMode As Boolean = False
        Public IsGridVisible As Boolean
        Public IsAfterSave As Boolean
        Public ActionInProgress As Integer = ElitaPlusPage.DetailPageCommand.Nothing_
        Public SortExpression As String = DealerInflation.DealerInflationDV.COL_DEALER_ID
        Public bnoRow As Boolean = False
        Public companyId As Guid = Guid.Empty
        Public companyCode As String = String.Empty
        Public dealerId As Guid = Guid.Empty
        Public dealer As String = String.Empty
        Public deleteRowIndex As Integer = 0
        Public isNew As String = "N"

    End Class

    Protected ReadOnly Property TheState() As MyState
        Get
            Try
                If Me.ThePage.StateSession.Item(Me.UniqueID) Is Nothing Then
                    Me.ThePage.StateSession.Item(Me.UniqueID) = New MyState
                End If
                Return CType(Me.ThePage.StateSession.Item(Me.UniqueID), MyState)

            Catch ex As Exception
                Return Nothing
            End Try
        End Get
    End Property

    Private Shadows ReadOnly Property ThePage() As ElitaPlusSearchPage
        Get
            Return CType(MyBase.Page, ElitaPlusSearchPage)
        End Get
    End Property

    Public ReadOnly Property IsGridInEditMode() As Boolean
        Get
            Return Me.DealerInflationGrid.EditIndex > Me.ThePage.NO_ITEM_SELECTED_INDEX
        End Get
    End Property

    Public Property SortDirection() As String
        Get
            If Not ViewState("SortDirection") Is Nothing Then
                Return ViewState("SortDirection").ToString
            Else
                Return String.Empty
            End If

        End Get
        Set(ByVal value As String)
            ViewState("SortDirection") = value
        End Set
    End Property

#End Region
    
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
       Try
           If Page.IsPostBack Then
               Dim confResponse As String = Me.HiddenDIDeletePromptResponse.Value
               Dim confResponseDel As String = Me.HiddenDIDeletePromptResponse.Value
             
               If Not confResponseDel Is Nothing AndAlso confResponseDel = Me.ThePage.MSG_VALUE_YES Then
                   If Me.TheState.ActionInProgress = ElitaPlusPage.DetailPageCommand.Delete Then
                       Me.TheState.DefaultDealerInflationID = GuidControl.ByteArrayToGuid(dealerinflationGrid.DataKeys(Me.TheState.deleteRowIndex).Values(0))
                       Me.ThePage.MasterPage.MessageController.AddSuccess(Me.MSG_RECORD_DELETED_OK, True)
                       Me.TheState.PageIndex = dealerinflationGrid.PageIndex
                       Me.TheState.IsAfterSave = True
                       Me.TheState.DealerInflationDV = Nothing
                       PopulateGrid()
                       Me.TheState.PageIndex = dealerinflationGrid.PageIndex
                       Me.TheState.IsEditMode = False
                       SetControlState()
                       'Clean after consuming the action
                       Me.TheState.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
                       Me.HiddenDIDeletePromptResponse.Value = ""
                   End If
               ElseIf Not confResponseDel Is Nothing AndAlso confResponseDel = Me.ThePage.MSG_VALUE_NO Then
                   'Clean after consuming the action
                   Me.TheState.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
                   Me.HiddenDIDeletePromptResponse.Value = ""
               End If
           End If
       Catch ex As Exception
           Me.ThePage.HandleErrors(ex, Me.ThePage.MasterPage.MessageController)
       End Try
        
    End Sub

    #Region "Grid related"

    Public Sub Populate()

        Dim e As New RequestDataEventArgs
        If (Me.TheState.dealerId.Equals(Guid.Empty)) Then
            Throw New GUIException("You must select a dealer first", Assurant.ElitaPlus.Common.ErrorCodes.GUI_DEALER_MUST_BE_SELECTED_ERR)
        end if

        RaiseEvent RequestClaimCloseRulesData(Me, e)
        Me.TheState.DealerInflationDV = e.Data
        Me.PopulateGrid()

    End Sub

    Public Sub PopulateGrid()
        If (Not Page.IsPostBack) Then
            Me.ThePage.TranslateGridHeader(DealerInflationGrid)
        End If
        Dim blnNewSearch As Boolean = False
        cboPageSize.SelectedValue = CType(Me.TheState.PageSize, String)
        Dim objDealerInflation As New DealerInflation
       
        Try
            With TheState
                If (.DealerInflationDV Is Nothing) Then
                    objDealerInflation.CompanyId = Me.TheState.companyId
                    objDealerInflation.DealerId = Me.TheState.dealerId
                    .DealerInflationDV = objDealerInflation.GetDealerInflation()
                    blnNewSearch = True
                End If
            End With
            Me.TheState.DealerInflationDV.Sort = Me.SortDirection

            If (Me.TheState.IsAfterSave) Then
                Me.TheState.IsAfterSave = False
                Me.ThePage.SetPageAndSelectedIndexFromGuid(Me.TheState.DealerInflationDV, Me.TheState.DefaultDealerInflationID, Me.DealerInflationGrid, Me.TheState.PageIndex)
            ElseIf (Me.TheState.IsEditMode) Then
                Me.ThePage.SetPageAndSelectedIndexFromGuid(Me.TheState.DealerInflationDV, Me.TheState.DefaultDealerInflationID, Me.DealerInflationGrid, Me.TheState.PageIndex, Me.TheState.IsEditMode)
            Else
                Me.ThePage.SetPageAndSelectedIndexFromGuid(Me.TheState.DealerInflationDV, Guid.Empty, Me.DealerInflationGrid, Me.TheState.PageIndex)
            End If

            If Me.TheState.DealerInflationDV.Count = 0 Then
                For Each gvRow As GridViewRow In DealerInflationGrid.Rows
                    gvRow.Visible = False
                    gvRow.Controls.Clear()
                Next
                lblPageSize.Visible = False
                cboPageSize.Visible = False
                colonSepertor.Visible = False
            Else
                lblPageSize.Visible = True
                cboPageSize.Visible = True
                colonSepertor.Visible = True
            End If
            Me.DealerInflationGrid.AutoGenerateColumns = False
            SortAndBindGrid(blnNewSearch)

        Catch ex As Exception
            Me.ThePage.HandleErrors(ex, Me.ThePage.MasterPage.MessageController)
        End Try

    End Sub

     Private Sub Grid_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles DealerInflationGrid.RowDataBound
        Try
            Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
            Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
            Dim strID As String

            If Not dvRow Is Nothing And Not Me.TheState.bnoRow Then
                strID = Me.ThePage.GetGuidStringFromByteArray(CType(dvRow(DealerInflation.DealerInflationDV.COL_dealer_inflation_id), Byte()))

                If (Me.TheState.IsEditMode = True AndAlso Me.TheState.DefaultDealerInflationID.ToString.Equals(strID)) Then


                    Dim moInflationMonthDropDown As DropDownList = CType(e.Row.Cells(Me.GRID_COL_INFLATION_MONTH).FindControl(Me.GRID_CTRL_NAME_EDIT_INFLATION_MONTH), DropDownList)
                    moInflationMonthDropDown.Enabled = False
                    ElitaPlusPage.BindListControlToDataView(moInflationMonthDropDown, LookupListNew.GetMonthsLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId))
                    If (Not String.IsNullOrWhiteSpace(dvRow(DealerInflation.DealerInflationDV.COL_inflation_month).ToString())) Then
                        Me.ThePage.SetSelectedItem(moInflationMonthDropDown, GuidControl.ByteArrayToGuid(DealerInflationGrid.DataKeys(e.Row.RowIndex).Values(2)))
                    End If

                    Dim moInflationYearDropDown As DropDownList = CType(e.Row.Cells(Me.GRID_COL_INFLATION_YEAR).FindControl(Me.GRID_CTRL_NAME_EDIT_INFLATION_YEAR), DropDownList)
                    moInflationYearDropDown.Enabled = False
                    ElitaPlusPage.BindListControlToDataView(moInflationYearDropDown, LookupListNew.GetMonthsLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId))
                    If (Not String.IsNullOrWhiteSpace(dvRow(DealerInflation.DealerInflationDV.COL_inflation_month).ToString())) Then
                        Me.ThePage.SetSelectedItem(moInflationYearDropDown, GuidControl.ByteArrayToGuid(DealerInflationGrid.DataKeys(e.Row.RowIndex).Values(3)))
                    End If

                   'time period text box
                    CType(e.Row.Cells(Me.GRID_COL_INFLATION_PCT).FindControl(Me.GRID_CTRL_NAME_EDIT_INFLATION_PCT), TextBox).Text = dvRow(DealerInflation.DealerInflationDV.COL_inflation_pct).ToString
                    
                Else
                    
                    CType(e.Row.Cells(Me.GRID_COL_INFLATION_MONTH).FindControl(Me.GRID_CTRL_NAME_LABEL_INFLATION_MONTH), Label).Text = dvRow(DealerInflation.DealerInflationDV.COL_inflation_month).ToString
                    CType(e.Row.Cells(Me.GRID_COL_INFLATION_YEAR).FindControl(Me.GRID_CTRL_NAME_LABLE_INFLATION_YEAR), Label).Text = dvRow(DealerInflation.DealerInflationDV.COL_inflation_year).ToString
                    CType(e.Row.Cells(Me.GRID_COL_INFLATION_PCT).FindControl(Me.GRID_CTRL_NAME_LABEL_INFLATION_PCT), Label).Text = dvRow(DealerInflation.DealerInflationDV.COL_inflation_pct).ToString
           
                End If
            End If

        Catch ex As Exception
            Me.ThePage.HandleErrors(ex, Me.ThePage.MasterPage.MessageController)
        End Try

    End Sub

    Public Sub RowCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles DealerInflationGrid.RowCommand

        Try
            Dim index As Integer
            If (e.CommandName = Me.EDIT_COMMAND) Then
                index = CInt(e.CommandArgument)
                Me.TheState.IsEditMode = True
                Me.TheState.DefaultDealerInflationID = GuidControl.ByteArrayToGuid(DealerInflationGrid.DataKeys(index).Values(0))
                Me.TheState.MyBO = New DealerInflation(Me.TheState.DefaultDealerInflationID)
                Me.Populate()
                Me.TheState.PageIndex = DealerInflationGrid.PageIndex
                Me.SetControlState()

                Try
                    Me.DealerInflationGrid.Rows(index).Focus()
                Catch ex As Exception
                    Me.DealerInflationGrid.Focus()
                End Try

            ElseIf (e.CommandName = Me.DELETE_COMMAND) Then
                index = CInt(e.CommandArgument)
                Me.TheState.deleteRowIndex = index

                Me.ThePage.DisplayMessage(Message.DELETE_RECORD_PROMPT, "", Me.ThePage.MSG_BTN_YES_NO, Me.ThePage.MSG_TYPE_CONFIRM, Me.HiddenDIDeletePromptResponse)
                Me.TheState.ActionInProgress = ElitaPlusPage.DetailPageCommand.Delete
                Me.DealerInflationGrid.Focus()
            End If
        Catch ex As Exception
            Me.ThePage.HandleErrors(ex, Me.ThePage.MasterPage.MessageController)
        End Try

    End Sub

    Private Sub SortAndBindGrid(Optional ByVal blnShowErr As Boolean = True)

        Dim dv As New DataView
        Dim objDealerInflation As New DealerInflation
        Me.TheState.PageIndex = Me.DealerInflationGrid.PageIndex

        If (Me.TheState.DealerInflationDV.Count = 0) Then
            dv = objDealerInflation.GetDealerInflation()

            Me.TheState.bnoRow = True
            dv = objDealerInflation.GetEmptyList(dv)
            Me.TheState.DealerInflationDV = Nothing
            Me.TheState.MyBO = New DealerInflation
            TheState.MyBO.AddNewRowToSearchDV(Me.TheState.DealerInflationDV, Me.TheState.MyBO)
            Me.DealerInflationGrid.DataSource = Me.TheState.DealerInflationDV
            Me.ThePage.HighLightSortColumn(DealerInflationGrid, Me.SortDirection, True)
            Me.DealerInflationGrid.DataBind()
            If Not DealerInflationGrid.BottomPagerRow.Visible Then DealerInflationGrid.BottomPagerRow.Visible = True
            Me.DealerInflationGrid.Rows(0).Visible = False
            Me.TheState.IsGridAddNew = True
            Me.TheState.IsGridVisible = False
            Me.lblRecordCount.Text = "0 " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
            If blnShowErr Then
                Me.ThePage.MasterPage.MessageController.AddInformation(ElitaPlus.ElitaPlusWebApp.Message.MSG_NO_RECORDS_FOUND, True)
            End If
        Else
            Me.TheState.bnoRow = False
            Me.DealerInflationGrid.Enabled = True
            Me.DealerInflationGrid.PageSize = Me.TheState.PageSize
            Me.DealerInflationGrid.DataSource = Me.TheState.DealerInflationDV
            Me.TheState.IsGridVisible = True
            Me.ThePage.HighLightSortColumn(DealerInflationGrid, Me.SortDirection, True)
            Me.DealerInflationGrid.DataBind()
            If Not DealerInflationGrid.BottomPagerRow.Visible Then DealerInflationGrid.BottomPagerRow.Visible = True
        End If

        ControlMgr.SetVisibleControl(Me.ThePage, DealerInflationGrid, Me.TheState.IsGridVisible)

        Session("recCount") = Me.TheState.DealerInflationDV.Count

        If Me.DealerInflationGrid.Visible Then
            If (Me.TheState.IsGridAddNew) Then
                Me.lblRecordCount.Text = String.Format("{0} {1}", (Me.TheState.DealerInflationDV.Count - 1), TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND))
            Else
                Me.lblRecordCount.Text = String.Format("{0} {1}", Me.TheState.DealerInflationDV.Count, TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND))
            End If
        End If
        ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me.ThePage, DealerInflationGrid)

    End Sub

    Private Sub Grid_PageIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DealerInflationGrid.PageIndexChanged
        Try
            If (Not (Me.TheState.IsEditMode)) Then
                Me.TheState.PageIndex = DealerInflationGrid.PageIndex
                Me.TheState.DefaultDealerInflationID = Guid.Empty
                Me.PopulateGrid()
            End If
        Catch ex As Exception
            Me.ThePage.HandleErrors(ex, Me.ThePage.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Grid_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles DealerInflationGrid.PageIndexChanging
        Try
            DealerInflationGrid.PageIndex = e.NewPageIndex
            TheState.PageIndex = DealerInflationGrid.PageIndex
        Catch ex As Exception
            Me.ThePage.HandleErrors(ex, Me.ThePage.MasterPage.MessageController)
        End Try
    End Sub

    Public Sub RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles DealerInflationGrid.RowCreated
        Try
            ThePage.BaseItemCreated(sender, e)
        Catch ex As Exception
            Me.ThePage.HandleErrors(ex, Me.ThePage.MasterPage.MessageController)
        End Try
    End Sub

#End Region

    #Region "Helper functions"

    Private Sub SetControlState()
        If (Me.TheState.IsEditMode) Then
            ControlMgr.SetVisibleControl(Me.ThePage, NewButton_WRITE, False)
            If (Me.cboPageSize.Enabled) Then
                ControlMgr.SetEnableControl(Me.ThePage, cboPageSize, False)
            End If
        Else
            ControlMgr.SetVisibleControl(Me.ThePage, NewButton_WRITE, True)
            If Not (Me.cboPageSize.Enabled) Then
                ControlMgr.SetEnableControl(Me.ThePage, Me.cboPageSize, True)
            End If
        End If
    End Sub

    Private Sub ReturnFromEditing()

        DealerInflationGrid.EditIndex = NO_ROW_SELECTED_INDEX

        If (Me.DealerInflationGrid.PageCount = 0) Then
            ControlMgr.SetVisibleControl(Me.ThePage, DealerInflationGrid, False)
        Else
            ControlMgr.SetVisibleControl(Me.ThePage, DealerInflationGrid, True)
        End If

        Me.TheState.IsEditMode = False
        Me.PopulateGrid()
        Me.TheState.PageIndex = DealerInflationGrid.PageIndex
        SetControlState()
    End Sub

    Private Sub RemoveNewRowFromSearchDV()
        Dim rowind As Integer = Me.ThePage.NO_ITEM_SELECTED_INDEX
        With TheState
            If Not .DealerInflationDV Is Nothing Then
                rowind = Me.ThePage.FindSelectedRowIndexFromGuid(.DealerInflationDV, .DefaultDealerInflationID)
            End If
        End With
        If rowind <> Me.ThePage.NO_ITEM_SELECTED_INDEX Then TheState.DealerInflationDV.Delete(rowind)
    End Sub

#End Region
End Class