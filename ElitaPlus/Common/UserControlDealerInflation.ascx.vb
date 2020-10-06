 Imports System.ComponentModel
 Imports System.Web.UI.WebControls.Expressions
 Imports Assurant.ElitaPlus.DALObjects
Partial Class UserControlDealerInflation
    Inherits System.Web.UI.UserControl

    Public Class RequestDataEventArgs
        Inherits EventArgs

        Public Data As  DealerInflation.DealerInflationDV

    End Class

    Public Delegate Sub RequestData(sender As Object, ByRef e As RequestDataEventArgs)

    Public Event RequestDealerInflationData As RequestData
    Public Event PropertyChanged As PropertyChangedEventHandler

    #Region "Constants"
   
    Private Const GRID_COL_DEALER_INFLATION_ID As Integer = 0
    Private Const GRID_COL_DEALER_ID As Integer = 1
    Private Const GRID_COL_DEALER As Integer = 2
    Private Const GRID_COL_INFLATION_MONTH As Integer = 3
    Private Const GRID_COL_INFLATION_YEAR As Integer = 4
    Private Const GRID_COL_INFLATION_PCT As Integer = 5
  
    Private Const GRID_CTRL_NAME_LABEL_INFLATION_MONTH As String = "lblInflationMonth"
    Private Const GRID_CTRL_NAME_LABLE_INFLATION_YEAR As String = "lblInflationYear"
    Private Const GRID_CTRL_NAME_LABEL_INFLATION_PCT As String = "lblInflationPct"
    Private Const GRID_CTRL_NAME_LABLE_DEALER As String = "lblDealer"
    
    Private Const GRID_CTRL_NAME_EDIT_INFLATION_MONTH As String = "cboInflationMonth"
    Private Const GRID_CTRL_NAME_EDIT_INFLATION_YEAR As String = "cboInflationYear"
    Private Const GRID_CTRL_NAME_EDIT_INFLATION_PCT As String = "txtInflationPct"
   
    Private Const MSG_CONFIRM_PROMPT As String = "MSG_CONFIRM_PROMPT"
    Private Const MSG_RECORD_DELETED_OK As String = "MSG_RECORD_DELETED_OK"
    Private Const MSG_RECORD_SAVED_OK As String = "MSG_RECORD_SAVED_OK"
    Private Const MSG_RECORD_NOT_SAVED As String = "MSG_RECORD_NOT_SAVED"
   
    Private Const EDIT_COMMAND As String = "SelectAction"
    Private Const DELETE_COMMAND As String = "DeleteRecord"
    Private Const SORT_COMMAND As String = "Sort"

    Private Const NO_ROW_SELECTED_INDEX As Integer = -1
    Private Const COL_NAME_INFLATION_YEAR_ID AS string ="ID"
    Private Const COL_NAME_INFLATION_YEAR_DESC AS string ="DESCRIPTION"
    Private Const NO_INFLATION_YEAR_RANGE As Integer = 25

#End Region

    Public Property DealerId As Nullable(Of Guid)
        Get
            Return TheState.dealerId
        End Get
        Set(value As Nullable(Of Guid))
            TheState.dealerId = CType(value, Guid)
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("DealerId"))
        End Set
    End Property

    #Region "Page State"
  Class MyState
        Public MyBO As DealerInflation
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
                If ThePage.StateSession.Item(UniqueID) Is Nothing Then
                    ThePage.StateSession.Item(UniqueID) = New MyState
                End If
                Return CType(ThePage.StateSession.Item(UniqueID), MyState)

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
            Return DealerInflationGrid.EditIndex > ThePage.NO_ITEM_SELECTED_INDEX
        End Get
    End Property

    Public Property SortDirection() As String
        Get
            If ViewState("SortDirection") IsNot Nothing Then
                Return ViewState("SortDirection").ToString
            Else
                Return dealerinflation.COL_NAME_INFLATION_YEAR
            End If

        End Get
        Set(value As String)
            ViewState("SortDirection") = value
        End Set
    End Property
    Public Property Dealer As String
        Get
            Return TheState.dealer
        End Get
        Set(value As String)
            TheState.dealer = value
        End Set
    End Property
#End Region
    
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
       Try
           If Page.IsPostBack Then
             
               Dim confResponseDel As String = HiddenDIDeletePromptResponse.Value
             
               If confResponseDel IsNot Nothing AndAlso confResponseDel = ThePage.MSG_VALUE_YES Then
                   If TheState.ActionInProgress = ElitaPlusPage.DetailPageCommand.Delete Then
                       TheState.DefaultDealerInflationID = GuidControl.ByteArrayToGuid(dealerinflationGrid.DataKeys(TheState.deleteRowIndex).Values(0))
                       ThePage.MasterPage.MessageController.AddSuccess(MSG_RECORD_DELETED_OK, True)
                       TheState.PageIndex = dealerinflationGrid.PageIndex
                       TheState.IsAfterSave = True
                       TheState.DealerInflationDV = Nothing
                       PopulateGrid()
                       TheState.PageIndex = dealerinflationGrid.PageIndex
                       TheState.IsEditMode = False
                       SetControlState()
                       TheState.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
                       HiddenDIDeletePromptResponse.Value = ""
                   End If
               ElseIf confResponseDel IsNot Nothing AndAlso confResponseDel = ThePage.MSG_VALUE_NO Then
                   TheState.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
                   HiddenDIDeletePromptResponse.Value = ""
               End If
           Else     
               SortDirection = DealerInflation.COL_NAME_INFLATION_YEAR
           End If
       Catch ex As Exception
           ThePage.HandleErrors(ex, ThePage.MasterPage.MessageController)
       End Try
        
    End Sub

    #Region "Grid related"

   
    Public Sub Populate()

        Dim e As New RequestDataEventArgs
        If (TheState.dealerId.Equals(Guid.Empty)) Then
            Throw New GUIException("You must select a dealer first", Assurant.ElitaPlus.Common.ErrorCodes.GUI_DEALER_MUST_BE_SELECTED_ERR)
        end if

        RaiseEvent RequestDealerInflationData(Me, e)
        TheState.DealerInflationDV = e.Data
        PopulateGrid()

    End Sub

    Public Sub PopulateGrid()
        
        If (Not Page.IsPostBack) Then
            ThePage.TranslateGridHeader(DealerInflationGrid)
        End If
       
        Dim blnNewSearch As Boolean = False
        cboDiPageSize.SelectedValue = CType(TheState.PageSize, String)
        Dim objDealerInflation As New DealerInflation

        
       
        Try
            With TheState
                If (.DealerInflationDV Is Nothing) Then
                    objDealerInflation.DealerId = TheState.dealerId
                    .DealerInflationDV = objDealerInflation.GetDealerInflation()
                    blnNewSearch = True
                End If
            End With

            If TheState.DealerInflationDV IsNot Nothing  AndAlso Not TheState.IsGridAddNew Then
                TheState.DealerInflationDV.Sort = SortDirection
            End If
            
            If (TheState.IsAfterSave) Then
                TheState.IsAfterSave = False
                ThePage.SetPageAndSelectedIndexFromGuid(TheState.DealerInflationDV, TheState.DefaultDealerInflationID, DealerInflationGrid, TheState.PageIndex)
            ElseIf (TheState.IsEditMode) Then
                ThePage.SetPageAndSelectedIndexFromGuid(TheState.DealerInflationDV, TheState.DefaultDealerInflationID, DealerInflationGrid, TheState.PageIndex, TheState.IsEditMode)
            Else
                If TheState.DealerInflationDV IsNot Nothing
                    ThePage.SetPageAndSelectedIndexFromGuid(TheState.DealerInflationDV, Guid.Empty, DealerInflationGrid, TheState.PageIndex)
                End if
            End If

            DealerInflationGrid.Columns(GRID_COL_INFLATION_YEAR).SortExpression = DealerInflation.COL_NAME_INFLATION_YEAR
            DealerInflationGrid.Columns(GRID_COL_INFLATION_MONTH).SortExpression = DealerInflation.COL_NAME_INFLATION_MONTH
            DealerInflationGrid.Columns(GRID_COL_INFLATION_PCT).SortExpression = DealerInflation.COL_NAME_INFLATION_PCT

            If TheState.DealerInflationDV IsNot Nothing AndAlso TheState.DealerInflationDV.Count = 0 Then
                For Each gvRow As GridViewRow In DealerInflationGrid.Rows
                    gvRow.Visible = False
                    gvRow.Controls.Clear()
                Next
                lblPageSize.Visible = False
                cboDiPageSize.Visible = False
                colonSepertor.Visible = False
            Else
                lblPageSize.Visible = True
                cboDiPageSize.Visible = True
                colonSepertor.Visible = True
            End If
            DealerInflationGrid.AutoGenerateColumns = False
            SortAndBindGrid(blnNewSearch)

        Catch ex As Exception
            ThePage.HandleErrors(ex, ThePage.MasterPage.MessageController)
        End Try

    End Sub

    Private Sub Grid_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles DealerInflationGrid.RowDataBound
        Try
            Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
            Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
            Dim strID As String

            If dvRow IsNot Nothing And Not TheState.bnoRow Then
                strID = ThePage.GetGuidStringFromByteArray(CType(dvRow(DealerInflation.DealerInflationDV.COL_dealer_inflation_id), Byte()))

                If (TheState.IsEditMode = True AndAlso TheState.DefaultDealerInflationID.ToString.Equals(strID)) Then

                    CType(e.Row.Cells(GRID_COL_DEALER).FindControl(GRID_CTRL_NAME_LABLE_DEALER), Label).Text =  TheState.dealer
                   
                    Dim moInflationMonthDropDown As DropDownList = CType(e.Row.Cells(GRID_COL_INFLATION_MONTH).FindControl(GRID_CTRL_NAME_EDIT_INFLATION_MONTH), DropDownList)
                    ElitaPlusPage.BindListControlToDataView(moInflationMonthDropDown, LookupListNew.GetMonthsLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId),"CODE",,false)
                    If (Not String.IsNullOrWhiteSpace(dvRow(DealerInflation.DealerInflationDV.COL_inflation_month).ToString())) Then
                        ThePage.SetSelectedItemByText(moInflationMonthDropDown, DealerInflationGrid.DataKeys(e.Row.RowIndex).Values(GRID_COL_INFLATION_MONTH))
                    End If

                    

                    Dim moInflationYearDropDown As DropDownList = CType(e.Row.Cells(GRID_COL_INFLATION_YEAR).FindControl(GRID_CTRL_NAME_EDIT_INFLATION_YEAR), DropDownList)
                    ElitaPlusPage.BindListControlToDataView(moInflationYearDropDown, GetInflationYears(),,,false)
                   If TheState.IsGridAddNew = True then  
                       ThePage.SetSelectedItemByText(moInflationYearDropDown, DateTime.Now.Year)
                   Elseif (Not String.IsNullOrWhiteSpace(dvRow(DealerInflation.DealerInflationDV.COL_inflation_month).ToString())) Then
                        ThePage.SetSelectedItemByText(moInflationYearDropDown, DealerInflationGrid.DataKeys(e.Row.RowIndex).Values(GRID_COL_INFLATION_YEAR))
                   End If

                   If TheState.IsGridAddNew = True Then
                       ControlMgr.SetEnableControl(ThePage, moInflationYearDropDown, true)
                       ControlMgr.SetEnableControl(ThePage, moInflationMonthDropDown, true)
                   Else 
                       ControlMgr.SetEnableControl(ThePage, moInflationYearDropDown, false)
                       ControlMgr.SetEnableControl(ThePage, moInflationMonthDropDown, false)
                   End If

                    CType(e.Row.Cells(GRID_COL_INFLATION_PCT).FindControl(GRID_CTRL_NAME_EDIT_INFLATION_PCT), TextBox).Text = dvRow(DealerInflation.DealerInflationDV.COL_inflation_pct).ToString
                    
                Else
                    CType(e.Row.Cells(GRID_COL_DEALER).FindControl(GRID_CTRL_NAME_LABLE_DEALER), Label).Text = dvRow(DealerInflation.DealerInflationDV.COL_DEALER).ToString
                    CType(e.Row.Cells(GRID_COL_INFLATION_MONTH).FindControl(GRID_CTRL_NAME_LABEL_INFLATION_MONTH), Label).Text = dvRow(DealerInflation.DealerInflationDV.COL_inflation_month).ToString
                    CType(e.Row.Cells(GRID_COL_INFLATION_YEAR).FindControl(GRID_CTRL_NAME_LABLE_INFLATION_YEAR), Label).Text = dvRow(DealerInflation.DealerInflationDV.COL_inflation_year).ToString
                    CType(e.Row.Cells(GRID_COL_INFLATION_PCT).FindControl(GRID_CTRL_NAME_LABEL_INFLATION_PCT), Label).Text = dvRow(DealerInflation.DealerInflationDV.COL_inflation_pct).ToString
           
                End If
            End If

        Catch ex As Exception
            ThePage.HandleErrors(ex, ThePage.MasterPage.MessageController)
        End Try

    End Sub

    Public Sub RowCommand(source As System.Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles DealerInflationGrid.RowCommand

        Try
            Dim index As Integer
            If (e.CommandName = EDIT_COMMAND) Then
                index = CInt(e.CommandArgument)
                TheState.IsEditMode = True
                TheState.DefaultDealerInflationID = GuidControl.ByteArrayToGuid(DealerInflationGrid.DataKeys(index).Values(0))
                TheState.MyBO = New DealerInflation(TheState.dealerId,TheState.DefaultDealerInflationID)
                Populate()
                TheState.PageIndex = DealerInflationGrid.PageIndex
                SetControlState()

                Try
                    DealerInflationGrid.Rows(index).Focus()
                Catch ex As Exception
                    DealerInflationGrid.Focus()
                End Try

            ElseIf (e.CommandName = DELETE_COMMAND) Then
               
                Try
                    index = CInt(e.CommandArgument)
                    TheState.DefaultDealerInflationID = GuidControl.ByteArrayToGuid(DealerInflationGrid.DataKeys(index).Values(0))
                    TheState.MyBO = New DealerInflation(TheState.dealerId, TheState.DefaultDealerInflationID)
                    TheState.MyBO.Delete()
                    TheState.ActionInProgress = ElitaPlusPage.DetailPageCommand.Delete
                    TheState.MyBO.Save()
                    TheState.IsAfterSave = True
                    Populate()
                    ThePage.MasterPage.MessageController.AddSuccess(Assurant.ElitaPlus.Common.ErrorCodes.MSG_RECORD_DELETED_OK, True)
                Catch ex As Exception
                    TheState.MyBO.RejectChanges()
                    Throw ex
                End Try

            End If
        Catch ex As Exception
            ThePage.HandleErrors(ex, ThePage.MasterPage.MessageController)
        End Try

    End Sub

    Private Sub SortAndBindGrid(Optional ByVal blnShowErr As Boolean = True)
       
        Dim objDealerInflation As New DealerInflation
        TheState.PageIndex = DealerInflationGrid.PageIndex

        If (TheState.DealerInflationDV IsNot Nothing AndAlso  TheState.DealerInflationDV.Count = 0) Then
            Dim dv As DataView = objDealerInflation.GetDealerInflation()
            TheState.bnoRow = True
            if dv IsNot Nothing Then
                objDealerInflation.GetEmptyList(dv)
            End If
            TheState.DealerInflationDV = Nothing
            TheState.MyBO = New DealerInflation
            TheState.MyBO.AddNewRowToSearchDV(TheState.DealerInflationDV, TheState.MyBO)
           DealerInflationGrid.DataSource = TheState.DealerInflationDV
            ThePage.HighLightSortColumn(DealerInflationGrid,   SortDirection, True)
            DealerInflationGrid.DataBind()
            If Not DealerInflationGrid.BottomPagerRow.Visible Then DealerInflationGrid.BottomPagerRow.Visible = True
            DealerInflationGrid.Rows(0).Visible = False
            TheState.IsGridAddNew = True
            TheState.IsGridVisible = False
            lblRecordCount.Text = "0 " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
            If blnShowErr Then
                ThePage.MasterPage.MessageController.AddInformation(ElitaPlus.ElitaPlusWebApp.Message.MSG_NO_RECORDS_FOUND, True)
            End If
        Else
            TheState.bnoRow = False
            DealerInflationGrid.Enabled = True
            DealerInflationGrid.PageSize = TheState.PageSize
            DealerInflationGrid.DataSource = TheState.DealerInflationDV
            TheState.IsGridVisible = True
            ThePage.HighLightSortColumn(DealerInflationGrid,   SortDirection , True)
            DealerInflationGrid.DataBind()
            If Not DealerInflationGrid.BottomPagerRow.Visible Then DealerInflationGrid.BottomPagerRow.Visible = True
        End If

        ControlMgr.SetVisibleControl(ThePage, DealerInflationGrid, TheState.IsGridVisible)

        If DealerInflationGrid.Visible AndAlso  TheState.DealerInflationDV IsNot Nothing Then

            Session("recCount") = TheState.DealerInflationDV.Count
            If Not DealerInflationGrid.BottomPagerRow.Visible Then DealerInflationGrid.BottomPagerRow.Visible = True
            If (TheState.IsGridAddNew ) Then
                lblRecordCount.Text = String.Format("{0} {1}", (TheState.DealerInflationDV.Count - 1), TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND))
            Else
                lblRecordCount.Text = String.Format("{0} {1}", TheState.DealerInflationDV.Count, TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND))
            End If
        End If
        ControlMgr.DisableEditDeleteGridIfNotEditAuth(ThePage, DealerInflationGrid)

    End Sub

    Private Sub Grid_PageIndexChanged(sender As Object, e As System.EventArgs) Handles DealerInflationGrid.PageIndexChanged
        Try
            If (Not (TheState.IsEditMode)) Then
                TheState.PageIndex = DealerInflationGrid.PageIndex
                TheState.DefaultDealerInflationID = Guid.Empty
                PopulateGrid()
            End If
        Catch ex As Exception
            ThePage.HandleErrors(ex, ThePage.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Grid_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles DealerInflationGrid.PageIndexChanging
        Try
            DealerInflationGrid.PageIndex = e.NewPageIndex
            TheState.PageIndex = DealerInflationGrid.PageIndex
        Catch ex As Exception
            ThePage.HandleErrors(ex, ThePage.MasterPage.MessageController)
        End Try
    End Sub

    Public Sub RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles DealerInflationGrid.RowCreated
        Try
            ThePage.BaseItemCreated(sender, e)
        Catch ex As Exception
            ThePage.HandleErrors(ex, ThePage.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Grid_SortCommand(source As Object, e As GridViewSortEventArgs) Handles DealerInflationGrid.Sorting
        Try
            Dim spaceIndex As Integer = SortDirection.LastIndexOf(" ", StringComparison.Ordinal)


            If spaceIndex > 0 AndAlso SortDirection.Substring(0, spaceIndex).Equals(e.SortExpression) Then
                If SortDirection.EndsWith(" ASC") Then
                    SortDirection = e.SortExpression + " DESC"
                Else
                    SortDirection = e.SortExpression + " ASC"
                End If
            Else
                SortDirection = e.SortExpression + " ASC"
            End If

            TheState.PageIndex = 0
            PopulateGrid()
        Catch ex As Exception
            ThePage.HandleErrors(ex, ThePage.MasterPage.MessageController)
        End Try
    End Sub

    
#End Region

    #Region "Helper functions"

    Private Sub SetControlState()
        If (TheState.IsEditMode) Then
            ControlMgr.SetVisibleControl(ThePage, NewButton_WRITE, False)
            If (cboDiPageSize.Enabled) Then
                ControlMgr.SetEnableControl(ThePage, cboDiPageSize, False)
            End If

        Else
            ControlMgr.SetVisibleControl(ThePage, NewButton_WRITE, True)
            If Not (cboDiPageSize.Enabled) Then
                ControlMgr.SetEnableControl(ThePage, cboDiPageSize, True)
            End If
        End If
    End Sub

    Private Sub ReturnFromEditing()

        DealerInflationGrid.EditIndex = NO_ROW_SELECTED_INDEX

        If (DealerInflationGrid.PageCount = 0) Then
            ControlMgr.SetVisibleControl(ThePage, DealerInflationGrid, False)
        Else
            ControlMgr.SetVisibleControl(ThePage, DealerInflationGrid, True)
        End If

        TheState.IsEditMode = False
        PopulateGrid()
        TheState.PageIndex = DealerInflationGrid.PageIndex
        SetControlState()
    End Sub

    Private Sub RemoveNewRowFromSearchDV()
        Dim rowind As Integer = ThePage.NO_ITEM_SELECTED_INDEX
        With TheState
            If .DealerInflationDV IsNot Nothing Then
                rowind = ThePage.FindSelectedRowIndexFromGuid(.DealerInflationDV, .DefaultDealerInflationID)
            End If
        End With
        If rowind <> ThePage.NO_ITEM_SELECTED_INDEX Then TheState.DealerInflationDV.Delete(rowind)
    End Sub

    Private Sub AddNew()
        If TheState.MyBO Is Nothing OrElse TheState.MyBO.IsNew = False Then
            TheState.MyBO = New Dealerinflation
            TheState.MyBO.AddNewRowToSearchDV(TheState.DealerInflationDV, TheState.MyBO)
        End If
        TheState.DefaultDealerInflationID = TheState.MyBO.Id
        TheState.IsGridAddNew = True
        PopulateGrid()
        'Set focus on the Code TextBox for the EditItemIndex row
        ThePage.SetPageAndSelectedIndexFromGuid(TheState.DealerInflationDV, TheState.DefaultDealerInflationID, DealerInflationGrid, _
                                                TheState.PageIndex, TheState.IsEditMode)
        ControlMgr.DisableEditDeleteGridIfNotEditAuth(ThePage, DealerInflationGrid)
        ThePage.SetGridControls(DealerInflationGrid, False)

        Try
            DealerInflationGrid.Rows(DealerInflationGrid.SelectedIndex).Focus()
        Catch ex As Exception
            DealerInflationGrid.Focus()
        End Try

    End Sub

    private Shared function GetInflationYears() As DataView
        Dim dt As DataTable =New DataTable
        
        dt.Columns.Add(COL_NAME_INFLATION_YEAR_ID, Guid.NewGuid.ToByteArray.GetType)
        dt.Columns.Add(COL_NAME_INFLATION_YEAR_DESC, GetType(String))

        Dim row As DataRow
        
        For i As Integer = - NO_INFLATION_YEAR_RANGE To NO_INFLATION_YEAR_RANGE
            row = dt.NewRow
            row(COL_NAME_INFLATION_YEAR_ID) = Guid.NewGuid.ToByteArray
            row(COL_NAME_INFLATION_YEAR_DESC) = DateTime.Now.Year +i
            dt.Rows.Add(row)
        Next
        Dim inflationYearDataview As DataView = new DataView(dt)
        return inflationYearDataview
    End function

    Private Function PopulateBOFromForm() As Boolean
        Dim objDropDownList As DropDownList
        Dim dealerInflationPct As TextBox
       
        With TheState.MyBO

            dealerInflationPct = CType(DealerInflationGrid.Rows(DealerInflationGrid.EditIndex).Cells(GRID_COL_INFLATION_PCT).FindControl(GRID_CTRL_NAME_EDIT_INFLATION_PCT), TextBox)
           
            If (TheState.IsEditMode = True AndAlso TheState.IsGridAddNew = False) Then

                ThePage.PopulateBOProperty(TheState.MyBO, "DealerInflationId", New Guid(CType(DealerInflationGrid.DataKeys(DealerInflationGrid.EditIndex).Values(GRID_COL_DEALER_INFLATION_ID), Byte())))
            Elseif  TheState.IsGridAddNew = true
                ThePage.PopulateBOProperty(TheState.MyBO, "DealerInflationId", Guid.NewGuid())
            End If
           
            ThePage.PopulateBOProperty(TheState.MyBO, "DealerId", TheState.dealerId)
           
            objDropDownList = CType(DealerInflationGrid.Rows((DealerInflationGrid.EditIndex)).Cells(GRID_COL_INFLATION_MONTH).FindControl(GRID_CTRL_NAME_EDIT_INFLATION_MONTH), DropDownList)
            ThePage.PopulateBOProperty(TheState.MyBO, "InflationMonth", objDropDownList,false)

            objDropDownList = CType(DealerInflationGrid.Rows((DealerInflationGrid.EditIndex)).Cells(GRID_COL_INFLATION_YEAR).FindControl(GRID_CTRL_NAME_EDIT_INFLATION_YEAR), DropDownList)
            ThePage.PopulateBOProperty(TheState.MyBO, "InflationYear", objDropDownList,False)
            
            ThePage.PopulateBOProperty(TheState.MyBO, "InflationPct", dealerInflationPct)
            
        End With
        If ThePage.ErrCollection.Count > 0 Then
            Throw New PopulateBOErrorException
        End If
    End Function
#End Region

#Region "Control Event Handlers"
    Protected Sub NewButton_WRITE_Click(sender As Object, e As EventArgs) Handles NewButton_WRITE.Click
        Try
            TheState.IsEditMode = True
            TheState.IsGridVisible = True
            TheState.IsGridAddNew = True
            AddNew()
            SetControlState()

        Catch ex As Exception
            ThePage.HandleErrors(ex, ThePage.MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs)

        Try
            PopulateBOFromForm()
           
            If TheState.IsGridAddNew AndAlso TheState.MyBO.ValidateNewDealerInflation(TheState.DealerInflationDV) Then
                ThePage.MasterPage.MessageController.AddWarning(Assurant.ElitaPlus.Common.ErrorCodes.INVALID_DEALER_INFLATION, True)
                Return
            End If
            If  TheState.MyBO.DealerId = Guid.Empty  Then
                ThePage.MasterPage.MessageController.AddWarning(Assurant.ElitaPlus.Common.ErrorCodes.DEALER_IS_REQUIRED, True)
                Return
            End If


            If (TheState.MyBO.IsDirty) Then
                Try
                    TheState.MyBO.Save()
                Catch ex As DataBaseUniqueKeyConstraintViolationException
                    Throw New GUIException("Unique constraint violation", Assurant.ElitaPlus.Common.ErrorCodes.DUPLICATE_KEY_CONSTRAINT_VIOLATED)
                End Try

                TheState.IsAfterSave = True
                TheState.IsGridAddNew = False
                ThePage.MasterPage.MessageController.AddSuccess(MSG_RECORD_SAVED_OK, True)
                TheState.DealerInflationDV = Nothing
                TheState.MyBO = Nothing
                ReturnFromEditing()
            Else
                ThePage.MasterPage.MessageController.AddWarning(MSG_RECORD_NOT_SAVED, True)
                ReturnFromEditing()
            End If
        Catch ex As Exception
            ThePage.HandleErrors(ex, ThePage.MasterPage.MessageController)
        End Try

    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs)
        Try
            With TheState
                If .IsGridAddNew Then
                    RemoveNewRowFromSearchDV()
                    .IsGridAddNew = False
                    DealerInflationGrid.PageIndex = .PageIndex
                End If
                .DefaultDealerInflationID = Guid.Empty
                TheState.MyBO = Nothing
                .IsEditMode = False
            End With
            DealerInflationGrid.EditIndex = ThePage.NO_ITEM_SELECTED_INDEX

            PopulateGrid()
            SetControlState()
            DealerInflationGrid.Focus()
        Catch ex As Exception
            ThePage.HandleErrors(ex, ThePage.MasterPage.MessageController)
        End Try
    End Sub
#End Region
End Class