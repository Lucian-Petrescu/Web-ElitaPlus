Imports System.ComponentModel
Imports Assurant.ElitaPlus.DALObjects
Partial Class UserControlRiskTypeTolerance
    Inherits System.Web.UI.UserControl

     Public Class RequestDataEventArgs
        Inherits EventArgs

        Public Data As  RiskTypeTolerance.RiskTypeToleranceDV

    End Class

    Public Delegate Sub RequestData(ByVal sender As Object, ByRef e As RequestDataEventArgs)

    Public Event RequestRiskTypeToleranceData As RequestData
    Public Event PropertyChanged As PropertyChangedEventHandler

  #Region "Constants"

    Private Const GRID_COL_DLR_RK_TYP_TOLERANCE_ID As Integer = 0
    Private Const GRID_COL_DEALER_ID As Integer = 1
    Private Const GRID_COL_RISK_TYPE As Integer = 2
    Private Const GRID_COL_TOLERANCE_PCT As Integer = 3
  
    Private Const GRID_CTRL_NAME_LABEL_RISK_TYPE As String = "lblRiskType"
    Private Const GRID_CTRL_NAME_LABEL_TOLERANCE_PCT As String = "lblTolerancePct"
    
    Private Const GRID_CTRL_NAME_EDIT_RISK_TYPE As String = "cboRiskType"
    Private Const GRID_CTRL_NAME_EDIT_TOLERANCE_PCT As String = "txtTolerancePct"
   
    Private Const GRID_CTRL_NAME_DELETE_RISK_TYPE As String = "DeleteButton_WRITE"

    Private Const MSG_CONFIRM_PROMPT As String = "MSG_CONFIRM_PROMPT"
    Private Const MSG_RECORD_DELETED_OK As String = "MSG_RECORD_DELETED_OK"
    Private Const MSG_RECORD_SAVED_OK As String = "MSG_RECORD_SAVED_OK"
    Private Const MSG_RECORD_NOT_SAVED As String = "MSG_RECORD_NOT_SAVED"

    Private Const EDIT_COMMAND As String = "SelectAction"
    Private Const DELETE_COMMAND As String = "DeleteRecord"
    Private Const SORT_COMMAND As String = "Sort"

    Private Const NO_ROW_SELECTED_INDEX As Integer = -1
   
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
        Public MyBO As RiskTypeTolerance
        Public DefaultRiskTypeToleranceID As Guid
        Public PageIndex As Integer = 0
        Public PageSize As Integer = 10
        Public RiskTypeToleranceDV As RiskTypeTolerance.RiskTypeToleranceDV = Nothing
        Public HasDataChanged As Boolean
        Public IsGridAddNew As Boolean = False
        Public IsEditMode As Boolean = False
        Public IsGridVisible As Boolean
        Public IsAfterSave As Boolean
        Public ActionInProgress As Integer = ElitaPlusPage.DetailPageCommand.Nothing_
        Public SortExpression As String = RiskTypeTolerance.RiskTypeToleranceDV.COL_DEALER_ID
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
            Return Me.RiskTypeToleranceGrid.EditIndex > Me.ThePage.NO_ITEM_SELECTED_INDEX
        End Get
    End Property

    Public Property SortDirection() As String
        Get
            If Not ViewState("SortDirection") Is Nothing Then
                Return ViewState("SortDirection").ToString
            Else
                Return RiskTypeTolerance.COL_NAME_RISK_TYPE
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
                       Me.TheState.DefaultRiskTypeToleranceID = GuidControl.ByteArrayToGuid(RiskTypeToleranceGrid.DataKeys(Me.TheState.deleteRowIndex).Values(0))
                       Me.ThePage.MasterPage.MessageController.AddSuccess(Me.MSG_RECORD_DELETED_OK, True)
                       Me.TheState.PageIndex = RiskTypeToleranceGrid.PageIndex
                       Me.TheState.IsAfterSave = True
                       Me.TheState.RiskTypeToleranceDV = Nothing
                       PopulateGrid()
                       Me.TheState.PageIndex = RiskTypeToleranceGrid.PageIndex
                       Me.TheState.IsEditMode = False
                       SetControlState()
                       Me.TheState.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
                       Me.HiddenDIDeletePromptResponse.Value = ""
                   End If
               ElseIf Not confResponseDel Is Nothing AndAlso confResponseDel = Me.ThePage.MSG_VALUE_NO Then
                   Me.TheState.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
                   Me.HiddenDIDeletePromptResponse.Value = ""
               End If
           Else     
               SortDirection = RiskTypeTolerance.COL_NAME_RISK_TYPE
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

        RaiseEvent RequestRiskTypeToleranceData(Me, e)
        Me.TheState.RiskTypeToleranceDV = e.Data
        Me.PopulateGrid()

    End Sub

    Public Sub PopulateGrid()
        
        If (Not Page.IsPostBack) Then
            Me.ThePage.TranslateGridHeader(RiskTypeToleranceGrid)
        End If
       
        Dim blnNewSearch As Boolean = False
        cboDiPageSize.SelectedValue = CType(Me.TheState.PageSize, String)
        Dim objRiskTypeTolerance As New RiskTypeTolerance

        
       
        Try
            With TheState
                If (.RiskTypeToleranceDV Is Nothing) Then
                    objRiskTypeTolerance.DealerId = Me.TheState.dealerId
                    .RiskTypeToleranceDV = objRiskTypeTolerance.GetRiskTypeTolerance()
                    blnNewSearch = True
                End If
            End With

            If Not TheState.RiskTypeToleranceDV Is Nothing Then
                TheState.RiskTypeToleranceDV.Sort = SortDirection
            End If
            
            If (Me.TheState.IsAfterSave) Then
                Me.TheState.IsAfterSave = False
                Me.ThePage.SetPageAndSelectedIndexFromGuid(Me.TheState.RiskTypeToleranceDV, Me.TheState.DefaultRiskTypeToleranceID, Me.RiskTypeToleranceGrid, Me.TheState.PageIndex)
            ElseIf (Me.TheState.IsEditMode) Then
                Me.ThePage.SetPageAndSelectedIndexFromGuid(Me.TheState.RiskTypeToleranceDV, Me.TheState.DefaultRiskTypeToleranceID, Me.RiskTypeToleranceGrid, Me.TheState.PageIndex, Me.TheState.IsEditMode)
            Else
                If Not TheState.RiskTypeToleranceDV Is Nothing
                    Me.ThePage.SetPageAndSelectedIndexFromGuid(Me.TheState.RiskTypeToleranceDV, Guid.Empty, Me.RiskTypeToleranceGrid, Me.TheState.PageIndex)
                End if
            End If

            RiskTypeToleranceGrid.Columns(GRID_COL_RISK_TYPE).SortExpression = RiskTypeTolerance.COL_NAME_RISK_TYPE
            RiskTypeToleranceGrid.Columns(GRID_COL_TOLERANCE_PCT).SortExpression = RiskTypeTolerance.COL_NAME_TOLERANCE_PCT

            If Not TheState.RiskTypeToleranceDV is Nothing AndAlso Me.TheState.RiskTypeToleranceDV.Count = 0 Then
                For Each gvRow As GridViewRow In RiskTypeToleranceGrid.Rows
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
            Me.RiskTypeToleranceGrid.AutoGenerateColumns = False
            SortAndBindGrid(blnNewSearch)

        Catch ex As Exception
            Me.ThePage.HandleErrors(ex, Me.ThePage.MasterPage.MessageController)
        End Try

    End Sub

    Private Sub Grid_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles RiskTypeToleranceGrid.RowDataBound
        Try
            Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
            Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
            Dim strID As String

            If Not dvRow Is Nothing And Not Me.TheState.bnoRow Then
                strID = Me.ThePage.GetGuidStringFromByteArray(CType(dvRow(RiskTypeTolerance.RiskTypeToleranceDV.COL_DLR_RK_TYP_TOLERANCE_ID), Byte()))

                If (Me.TheState.IsEditMode = True AndAlso Me.TheState.DefaultRiskTypeToleranceID.ToString.Equals(strID)) Then


                    Dim moRiskTypeDropDown As DropDownList = CType(e.Row.Cells(Me.GRID_COL_RISK_TYPE).FindControl(Me.GRID_CTRL_NAME_EDIT_RISK_TYPE), DropDownList)
                    ElitaPlusPage.BindListControlToDataView(moRiskTypeDropDown, LookupListNew.GetRiskTypeLookupList(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id),"DESCRIPTION",,false)
                    If (Not String.IsNullOrWhiteSpace(dvRow(RiskTypeTolerance.RiskTypeToleranceDV.COL_RISK_TYPE).ToString())) Then
                        Me.ThePage.SetSelectedItemByText(moRiskTypeDropDown, RiskTypeToleranceGrid.DataKeys(e.Row.RowIndex).Values(GRID_COL_RISK_TYPE))
                    End If

                   If TheState.IsGridAddNew = True Then
                       ControlMgr.SetEnableControl(Me.ThePage, moRiskTypeDropDown, true)
                   Else 
                      ControlMgr.SetEnableControl(Me.ThePage, moRiskTypeDropDown, false)
                   End If

                    CType(e.Row.Cells(Me.GRID_COL_TOLERANCE_PCT).FindControl(Me.GRID_CTRL_NAME_EDIT_TOLERANCE_PCT), TextBox).Text = dvRow(RiskTypeTolerance.RiskTypeToleranceDV.COL_TOLERANCE_PCT).ToString
                    
                Else
                    
                    CType(e.Row.Cells(Me.GRID_COL_RISK_TYPE).FindControl(Me.GRID_CTRL_NAME_LABEL_RISK_TYPE), Label).Text = dvRow(RiskTypeTolerance.RiskTypeToleranceDV.COL_RISK_TYPE).ToString
                    CType(e.Row.Cells(Me.GRID_COL_TOLERANCE_PCT).FindControl(Me.GRID_CTRL_NAME_LABEL_TOLERANCE_PCT), Label).Text = dvRow(RiskTypeTolerance.RiskTypeToleranceDV.COL_TOLERANCE_PCT).ToString
                   
           
                End If
            End If

        Catch ex As Exception
            Me.ThePage.HandleErrors(ex, Me.ThePage.MasterPage.MessageController)
        End Try

    End Sub

    Public Sub RowCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles RiskTypeToleranceGrid.RowCommand

        Try
            Dim index As Integer
            If (e.CommandName = Me.EDIT_COMMAND) Then
                index = CInt(e.CommandArgument)
                Me.TheState.IsEditMode = True
                Me.TheState.DefaultRiskTypeToleranceID = GuidControl.ByteArrayToGuid(RiskTypeToleranceGrid.DataKeys(index).Values(0))
                Me.TheState.MyBO = New RiskTypeTolerance(Me.TheState.dealerId,Me.TheState.DefaultRiskTypeToleranceID)
                Me.Populate()
                Me.TheState.PageIndex = RiskTypeToleranceGrid.PageIndex
                Me.SetControlState()

                Try
                    Me.RiskTypeToleranceGrid.Rows(index).Focus()
                Catch ex As Exception
                    Me.RiskTypeToleranceGrid.Focus()
                End Try

            ElseIf (e.CommandName = Me.DELETE_COMMAND) Then
               
                Try
                    index = CInt(e.CommandArgument)
                    Me.TheState.DefaultRiskTypeToleranceID = GuidControl.ByteArrayToGuid(RiskTypeToleranceGrid.DataKeys(index).Values(0))
                    Me.TheState.MyBO = New RiskTypeTolerance(Me.TheState.dealerId, Me.TheState.DefaultRiskTypeToleranceID)
                    Me.TheState.MyBO.Delete()
                    Me.TheState.ActionInProgress = ElitaPlusPage.DetailPageCommand.Delete
                    Me.TheState.MyBO.Save()
                    Me.TheState.IsAfterSave = True
                    Populate()
                    Me.ThePage.MasterPage.MessageController.AddSuccess(Assurant.ElitaPlus.Common.ErrorCodes.MSG_RECORD_DELETED_OK, True)
                Catch ex As Exception
                    Me.TheState.MyBO.RejectChanges()
                    Throw ex
                End Try

            End If
        Catch ex As Exception
            Me.ThePage.HandleErrors(ex, Me.ThePage.MasterPage.MessageController)
        End Try

    End Sub

    Private Sub SortAndBindGrid(Optional ByVal blnShowErr As Boolean = True)
       
        Dim objRiskTypeTolerance As New RiskTypeTolerance
        Me.TheState.PageIndex = Me.RiskTypeToleranceGrid.PageIndex

        If (Not Me.TheState.RiskTypeToleranceDV is Nothing AndAlso  Me.TheState.RiskTypeToleranceDV.Count = 0) Then
            Dim dv As DataView = objRiskTypeTolerance.GetRiskTypeTolerance()
            Me.TheState.bnoRow = True
            if not dv is Nothing Then
                objRiskTypeTolerance.GetEmptyList(dv)
            End If
            Me.TheState.RiskTypeToleranceDV = Nothing
            Me.TheState.MyBO = New RiskTypeTolerance
            TheState.MyBO.AddNewRowToSearchDV(Me.TheState.RiskTypeToleranceDV, Me.TheState.MyBO)
            Me.RiskTypeToleranceGrid.DataSource = Me.TheState.RiskTypeToleranceDV
            Me.ThePage.HighLightSortColumn(RiskTypeToleranceGrid, Me.SortDirection, True)
            Me.RiskTypeToleranceGrid.DataBind()
            If Not RiskTypeToleranceGrid.BottomPagerRow.Visible Then RiskTypeToleranceGrid.BottomPagerRow.Visible = True
            Me.RiskTypeToleranceGrid.Rows(0).Visible = False
            Me.TheState.IsGridAddNew = True
            Me.TheState.IsGridVisible = False
            Me.lblRecordCount.Text = "0 " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
            If blnShowErr Then
                Me.ThePage.MasterPage.MessageController.AddInformation(ElitaPlus.ElitaPlusWebApp.Message.MSG_NO_RECORDS_FOUND, True)
            End If
        Else
            Me.TheState.bnoRow = False
            Me.RiskTypeToleranceGrid.Enabled = True
            Me.RiskTypeToleranceGrid.PageSize = Me.TheState.PageSize
            Me.RiskTypeToleranceGrid.DataSource = Me.TheState.RiskTypeToleranceDV
            Me.TheState.IsGridVisible = True
            Me.ThePage.HighLightSortColumn(RiskTypeToleranceGrid, Me.SortDirection, True)
            Me.RiskTypeToleranceGrid.DataBind()
            If Not RiskTypeToleranceGrid.BottomPagerRow.Visible Then RiskTypeToleranceGrid.BottomPagerRow.Visible = True
        End If

        ControlMgr.SetVisibleControl(Me.ThePage, RiskTypeToleranceGrid, Me.TheState.IsGridVisible)

        If Me.RiskTypeToleranceGrid.Visible AndAlso  Not Me.TheState.RiskTypeToleranceDV Is Nothing Then

            Session("recCount") = Me.TheState.RiskTypeToleranceDV.Count
            If Not RiskTypeToleranceGrid.BottomPagerRow.Visible Then RiskTypeToleranceGrid.BottomPagerRow.Visible = True
            If (Me.TheState.IsGridAddNew ) Then
                Me.lblRecordCount.Text = String.Format("{0} {1}", (Me.TheState.RiskTypeToleranceDV.Count - 1), TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND))
            Else
                Me.lblRecordCount.Text = String.Format("{0} {1}", Me.TheState.RiskTypeToleranceDV.Count, TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND))
            End If
        End If
        ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me.ThePage, RiskTypeToleranceGrid)

    End Sub

    Private Sub Grid_PageIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RiskTypeToleranceGrid.PageIndexChanged
        Try
            If (Not (Me.TheState.IsEditMode)) Then
                Me.TheState.PageIndex = RiskTypeToleranceGrid.PageIndex
                Me.TheState.DefaultRiskTypeToleranceID = Guid.Empty
                Me.PopulateGrid()
            End If
        Catch ex As Exception
            Me.ThePage.HandleErrors(ex, Me.ThePage.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Grid_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles RiskTypeToleranceGrid.PageIndexChanging
        Try
            RiskTypeToleranceGrid.PageIndex = e.NewPageIndex
            TheState.PageIndex = RiskTypeToleranceGrid.PageIndex
        Catch ex As Exception
            Me.ThePage.HandleErrors(ex, Me.ThePage.MasterPage.MessageController)
        End Try
    End Sub

    Public Sub RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles RiskTypeToleranceGrid.RowCreated
        Try
            ThePage.BaseItemCreated(sender, e)
        Catch ex As Exception
            Me.ThePage.HandleErrors(ex, Me.ThePage.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Grid_SortCommand(ByVal source As Object, ByVal e As GridViewSortEventArgs) Handles RiskTypeToleranceGrid.Sorting
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

            Me.TheState.PageIndex = 0
            PopulateGrid()
        Catch ex As Exception
            Me.ThePage.HandleErrors(ex, Me.ThePage.MasterPage.MessageController)
        End Try
    End Sub

    
#End Region

    #Region "Helper functions"

    Private Sub SetControlState()
        If (Me.TheState.IsEditMode) Then
            ControlMgr.SetVisibleControl(Me.ThePage, NewButton_WRITE, False)
            If (Me.cboDiPageSize.Enabled) Then
                ControlMgr.SetEnableControl(Me.ThePage, cboDiPageSize, False)
            End If

        Else
            ControlMgr.SetVisibleControl(Me.ThePage, NewButton_WRITE, True)
            If Not (Me.cboDiPageSize.Enabled) Then
                ControlMgr.SetEnableControl(Me.ThePage, Me.cboDiPageSize, True)
            End If
        End If
    End Sub

    Private Sub ReturnFromEditing()

        RiskTypeToleranceGrid.EditIndex = NO_ROW_SELECTED_INDEX

        If (Me.RiskTypeToleranceGrid.PageCount = 0) Then
            ControlMgr.SetVisibleControl(Me.ThePage, RiskTypeToleranceGrid, False)
        Else
            ControlMgr.SetVisibleControl(Me.ThePage, RiskTypeToleranceGrid, True)
        End If

        Me.TheState.IsEditMode = False
        Me.PopulateGrid()
        Me.TheState.PageIndex = RiskTypeToleranceGrid.PageIndex
        SetControlState()
    End Sub

    Private Sub RemoveNewRowFromSearchDV()
        Dim rowind As Integer = Me.ThePage.NO_ITEM_SELECTED_INDEX
        With TheState
            If Not .RiskTypeToleranceDV Is Nothing Then
                rowind = Me.ThePage.FindSelectedRowIndexFromGuid(.RiskTypeToleranceDV, .DefaultRiskTypeToleranceID)
            End If
        End With
        If rowind <> Me.ThePage.NO_ITEM_SELECTED_INDEX Then TheState.RiskTypeToleranceDV.Delete(rowind)
    End Sub

    Private Sub AddNew()
        If TheState.MyBO Is Nothing OrElse Me.TheState.MyBO.IsNew = False Then
            TheState.MyBO = New RiskTypeTolerance
            TheState.MyBO.AddNewRowToSearchDV(Me.TheState.RiskTypeToleranceDV, Me.TheState.MyBO)
        End If
        TheState.DefaultRiskTypeToleranceID = Me.TheState.MyBO.Id
        TheState.IsGridAddNew = True
        PopulateGrid()
        'Set focus on the Code TextBox for the EditItemIndex row
        ThePage.SetPageAndSelectedIndexFromGuid(Me.TheState.RiskTypeToleranceDV, Me.TheState.DefaultRiskTypeToleranceID, Me.RiskTypeToleranceGrid, _
                                                TheState.PageIndex, Me.TheState.IsEditMode)
        ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me.ThePage, RiskTypeToleranceGrid)
        ThePage.SetGridControls(Me.RiskTypeToleranceGrid, False)

        Try
            Me.RiskTypeToleranceGrid.Rows(Me.RiskTypeToleranceGrid.SelectedIndex).Focus()
        Catch ex As Exception
            Me.RiskTypeToleranceGrid.Focus()
        End Try

    End Sub
    Private Function PopulateBOFromForm() As Boolean
        Dim objDropDownList As DropDownList
        Dim RiskTypeTolerancePct As TextBox
       
        With Me.TheState.MyBO

            RiskTypeTolerancePct = CType(RiskTypeToleranceGrid.Rows(Me.RiskTypeToleranceGrid.EditIndex).Cells(GRID_COL_TOLERANCE_PCT).FindControl(GRID_CTRL_NAME_EDIT_TOLERANCE_PCT), TextBox)
           
            If (Me.TheState.IsEditMode = True AndAlso Me.TheState.IsGridAddNew = False) Then

                Me.ThePage.PopulateBOProperty(TheState.MyBO, "RiskTypeToleranceId", New Guid(CType(RiskTypeToleranceGrid.DataKeys(RiskTypeToleranceGrid.EditIndex).Values(GRID_COL_DLR_RK_TYP_TOLERANCE_ID), Byte())))
            Elseif  Me.TheState.IsGridAddNew = true
                Me.ThePage.PopulateBOProperty(TheState.MyBO, "RiskTypeToleranceId", Guid.NewGuid())
            End If
           
            Me.ThePage.PopulateBOProperty(TheState.MyBO, "DealerId", Me.TheState.dealerId)
           
            objDropDownList = CType(RiskTypeToleranceGrid.Rows((Me.RiskTypeToleranceGrid.EditIndex)).Cells(GRID_COL_RISK_TYPE).FindControl(GRID_CTRL_NAME_EDIT_RISK_TYPE), DropDownList)
            Me.ThePage.PopulateBOProperty(TheState.MyBO, "RiskType", objDropDownList,false)
            Me.ThePage.PopulateBOProperty(TheState.MyBO, "RiskTypeId", objDropDownList,true)

           If (RiskTypeTolerancePct.Text = String.Empty) Then
                RiskTypeTolerancePct.Text = 0
            End If
            Me.ThePage.PopulateBOProperty(TheState.MyBO, "TolerancePct", RiskTypeTolerancePct) 

            
        End With
        If Me.ThePage.ErrCollection.Count > 0 Then
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
            Me.SetControlState()

        Catch ex As Exception
            Me.ThePage.HandleErrors(ex, Me.ThePage.MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As EventArgs)

        Try
            PopulateBOFromForm()
           
            If Me.TheState.IsGridAddNew AndAlso TheState.MyBO.ValidateNewRiskTypeTolerance(Me.TheState.RiskTypeToleranceDV) Then
                Me.ThePage.MasterPage.MessageController.AddWarning(Assurant.ElitaPlus.Common.ErrorCodes.INVALID_RISK_TYPE_TOLERANCE, True)
                Return
            End If
            If  TheState.MyBO.DealerId = Guid.Empty  Then
                Me.ThePage.MasterPage.MessageController.AddWarning(Assurant.ElitaPlus.Common.ErrorCodes.DEALER_IS_REQUIRED, True)
                Return
            End If


            If (Me.TheState.MyBO.IsDirty) Then
                Try
                    Me.TheState.MyBO.Save()
                Catch ex As DataBaseUniqueKeyConstraintViolationException
                    Throw New GUIException("Unique constraint violation", Assurant.ElitaPlus.Common.ErrorCodes.DUPLICATE_KEY_CONSTRAINT_VIOLATED)
                End Try

                Me.TheState.IsAfterSave = True
                Me.TheState.IsGridAddNew = False
                Me.ThePage.MasterPage.MessageController.AddSuccess(Me.MSG_RECORD_SAVED_OK, True)
                Me.TheState.RiskTypeToleranceDV = Nothing
                Me.TheState.MyBO = Nothing
                Me.ReturnFromEditing()
            Else
                Me.ThePage.MasterPage.MessageController.AddWarning(Me.MSG_RECORD_NOT_SAVED, True)
                Me.ReturnFromEditing()
            End If
        Catch ex As Exception
            Me.ThePage.HandleErrors(ex, Me.ThePage.MasterPage.MessageController)
        End Try

    End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As EventArgs)
        Try
            With TheState
                If .IsGridAddNew Then
                    RemoveNewRowFromSearchDV()
                    .IsGridAddNew = False
                    RiskTypeToleranceGrid.PageIndex = .PageIndex
                End If
                .DefaultRiskTypeToleranceID = Guid.Empty
                Me.TheState.MyBO = Nothing
                .IsEditMode = False
            End With
            RiskTypeToleranceGrid.EditIndex = Me.ThePage.NO_ITEM_SELECTED_INDEX

            PopulateGrid()
            SetControlState()
            Me.RiskTypeToleranceGrid.Focus()
        Catch ex As Exception
            Me.ThePage.HandleErrors(ex, Me.ThePage.MasterPage.MessageController)
        End Try
    End Sub
#End Region


End Class