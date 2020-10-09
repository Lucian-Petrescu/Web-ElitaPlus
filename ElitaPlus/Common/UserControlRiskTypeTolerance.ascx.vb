Imports System.ComponentModel
Imports Assurant.ElitaPlus.DALObjects
Partial Class UserControlRiskTypeTolerance
    Inherits System.Web.UI.UserControl

     Public Class RequestDataEventArgs
        Inherits EventArgs

        Public Data As  RiskTypeTolerance.RiskTypeToleranceDV

    End Class

    Public Delegate Sub RequestData(sender As Object, ByRef e As RequestDataEventArgs)

    Public Event RequestRiskTypeToleranceData As RequestData
    Public Event PropertyChanged As PropertyChangedEventHandler

  #Region "Constants"

    Private Const GRID_COL_DLR_RK_TYP_TOLERANCE_ID As Integer = 0
    Private Const GRID_COL_DEALER_ID As Integer = 1
     Private Const GRID_COL_DEALER As Integer = 2
    Private Const GRID_COL_RISK_TYPE As Integer = 3
    Private Const GRID_COL_TOLERANCE_PCT As Integer = 4
  
    Private Const GRID_CTRL_NAME_LABEL_RISK_TYPE As String = "lblRiskType"
    Private Const GRID_CTRL_NAME_LABEL_TOLERANCE_PCT As String = "lblTolerancePct"
     Private Const GRID_CTRL_NAME_LABLE_DEALER As String = "lblDealer"
    
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
            Return TheState.dealerId
        End Get
        Set(value As Nullable(Of Guid))
            TheState.dealerId = CType(value, Guid)
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("DealerId"))
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
            Return RiskTypeToleranceGrid.EditIndex > ThePage.NO_ITEM_SELECTED_INDEX
        End Get
    End Property

    Public Property SortDirection() As String
        Get
            If ViewState("SortDirection") IsNot Nothing Then
                Return ViewState("SortDirection").ToString
            Else
                Return RiskTypeTolerance.COL_NAME_RISK_TYPE
            End If

        End Get
        Set(value As String)
            ViewState("SortDirection") = value
        End Set
    End Property

#End Region
    
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
       Try
           If Page.IsPostBack Then
               Dim confResponse As String = HiddenDIDeletePromptResponse.Value
               Dim confResponseDel As String = HiddenDIDeletePromptResponse.Value
             
               If confResponseDel IsNot Nothing AndAlso confResponseDel = ThePage.MSG_VALUE_YES Then
                   If TheState.ActionInProgress = ElitaPlusPage.DetailPageCommand.Delete Then
                       TheState.DefaultRiskTypeToleranceID = GuidControl.ByteArrayToGuid(RiskTypeToleranceGrid.DataKeys(TheState.deleteRowIndex).Values(0))
                       ThePage.MasterPage.MessageController.AddSuccess(MSG_RECORD_DELETED_OK, True)
                       TheState.PageIndex = RiskTypeToleranceGrid.PageIndex
                       TheState.IsAfterSave = True
                       TheState.RiskTypeToleranceDV = Nothing
                       PopulateGrid()
                       TheState.PageIndex = RiskTypeToleranceGrid.PageIndex
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
               SortDirection = RiskTypeTolerance.COL_NAME_RISK_TYPE
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

        RaiseEvent RequestRiskTypeToleranceData(Me, e)
        TheState.RiskTypeToleranceDV = e.Data
        PopulateGrid()

    End Sub

    Public Sub PopulateGrid()
        
        If (Not Page.IsPostBack) Then
            ThePage.TranslateGridHeader(RiskTypeToleranceGrid)
        End If
       
        Dim blnNewSearch As Boolean = False
        cboDiPageSize.SelectedValue = CType(TheState.PageSize, String)
        Dim objRiskTypeTolerance As New RiskTypeTolerance

        
       
        Try
            With TheState
                If (.RiskTypeToleranceDV Is Nothing) Then
                    objRiskTypeTolerance.DealerId = TheState.dealerId
                    .RiskTypeToleranceDV = objRiskTypeTolerance.GetRiskTypeTolerance()
                    blnNewSearch = True
                End If
            End With

            If TheState.RiskTypeToleranceDV IsNot Nothing AndAlso  Not TheState.IsGridAddNew = True Then
                TheState.RiskTypeToleranceDV.Sort = SortDirection
            End If
            
            If (TheState.IsAfterSave) Then
                TheState.IsAfterSave = False
                ThePage.SetPageAndSelectedIndexFromGuid(TheState.RiskTypeToleranceDV, TheState.DefaultRiskTypeToleranceID, RiskTypeToleranceGrid, TheState.PageIndex)
            ElseIf (TheState.IsEditMode) Then
                ThePage.SetPageAndSelectedIndexFromGuid(TheState.RiskTypeToleranceDV, TheState.DefaultRiskTypeToleranceID, RiskTypeToleranceGrid, TheState.PageIndex, TheState.IsEditMode)
            Else
                If TheState.RiskTypeToleranceDV IsNot Nothing
                    ThePage.SetPageAndSelectedIndexFromGuid(TheState.RiskTypeToleranceDV, Guid.Empty, RiskTypeToleranceGrid, TheState.PageIndex)
                End if
            End If

            RiskTypeToleranceGrid.Columns(GRID_COL_RISK_TYPE).SortExpression = RiskTypeTolerance.COL_NAME_RISK_TYPE
            RiskTypeToleranceGrid.Columns(GRID_COL_TOLERANCE_PCT).SortExpression = RiskTypeTolerance.COL_NAME_TOLERANCE_PCT

            If TheState.RiskTypeToleranceDV IsNot Nothing AndAlso TheState.RiskTypeToleranceDV.Count = 0 Then
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
            RiskTypeToleranceGrid.AutoGenerateColumns = False
            SortAndBindGrid(blnNewSearch)

        Catch ex As Exception
            ThePage.HandleErrors(ex, ThePage.MasterPage.MessageController)
        End Try

    End Sub

    Private Sub Grid_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles RiskTypeToleranceGrid.RowDataBound
        Try
            Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
            Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
            Dim strID As String

            If dvRow IsNot Nothing AndAlso Not TheState.bnoRow Then
                strID = ThePage.GetGuidStringFromByteArray(CType(dvRow(RiskTypeTolerance.RiskTypeToleranceDV.COL_DLR_RK_TYP_TOLERANCE_ID), Byte()))

                If (TheState.IsEditMode = True AndAlso TheState.DefaultRiskTypeToleranceID.ToString.Equals(strID)) Then

                    CType(e.Row.Cells(GRID_COL_DEALER).FindControl(GRID_CTRL_NAME_LABLE_DEALER), Label).Text =  TheState.dealer

                    Dim moRiskTypeDropDown As DropDownList = CType(e.Row.Cells(GRID_COL_RISK_TYPE).FindControl(GRID_CTRL_NAME_EDIT_RISK_TYPE), DropDownList)
                    ElitaPlusPage.BindListControlToDataView(moRiskTypeDropDown, LookupListNew.GetRiskTypeLookupList(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id),"DESCRIPTION",,false)
                    If (Not String.IsNullOrWhiteSpace(dvRow(RiskTypeTolerance.RiskTypeToleranceDV.COL_RISK_TYPE).ToString())) Then
                        ThePage.SetSelectedItemByText(moRiskTypeDropDown, RiskTypeToleranceGrid.DataKeys(e.Row.RowIndex).Values(GRID_COL_RISK_TYPE))
                    End If

                   If TheState.IsGridAddNew = True Then
                       ControlMgr.SetEnableControl(ThePage, moRiskTypeDropDown, true)
                   Else 
                      ControlMgr.SetEnableControl(ThePage, moRiskTypeDropDown, false)
                   End If

                    CType(e.Row.Cells(GRID_COL_TOLERANCE_PCT).FindControl(GRID_CTRL_NAME_EDIT_TOLERANCE_PCT), TextBox).Text = dvRow(RiskTypeTolerance.RiskTypeToleranceDV.COL_TOLERANCE_PCT).ToString
                    
                Else
                    CType(e.Row.Cells(GRID_COL_DEALER).FindControl(GRID_CTRL_NAME_LABLE_DEALER), Label).Text = dvRow(RiskTypeTolerance.RiskTypeToleranceDV.COL_DEALER).ToString
                    CType(e.Row.Cells(GRID_COL_RISK_TYPE).FindControl(GRID_CTRL_NAME_LABEL_RISK_TYPE), Label).Text = dvRow(RiskTypeTolerance.RiskTypeToleranceDV.COL_RISK_TYPE).ToString
                    CType(e.Row.Cells(GRID_COL_TOLERANCE_PCT).FindControl(GRID_CTRL_NAME_LABEL_TOLERANCE_PCT), Label).Text = dvRow(RiskTypeTolerance.RiskTypeToleranceDV.COL_TOLERANCE_PCT).ToString
                   
           
                End If
            End If

        Catch ex As Exception
            ThePage.HandleErrors(ex, ThePage.MasterPage.MessageController)
        End Try

    End Sub

    Public Sub RowCommand(source As System.Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles RiskTypeToleranceGrid.RowCommand

        Try
            Dim index As Integer
            If (e.CommandName = EDIT_COMMAND) Then
                index = CInt(e.CommandArgument)
                TheState.IsEditMode = True
                TheState.DefaultRiskTypeToleranceID = GuidControl.ByteArrayToGuid(RiskTypeToleranceGrid.DataKeys(index).Values(0))
                TheState.MyBO = New RiskTypeTolerance(TheState.dealerId,TheState.DefaultRiskTypeToleranceID)
                Populate()
                TheState.PageIndex = RiskTypeToleranceGrid.PageIndex
                SetControlState()

                Try
                    RiskTypeToleranceGrid.Rows(index).Focus()
                Catch ex As Exception
                    RiskTypeToleranceGrid.Focus()
                End Try

            ElseIf (e.CommandName = DELETE_COMMAND) Then
               
                Try
                    index = CInt(e.CommandArgument)
                    TheState.DefaultRiskTypeToleranceID = GuidControl.ByteArrayToGuid(RiskTypeToleranceGrid.DataKeys(index).Values(0))
                    TheState.MyBO = New RiskTypeTolerance(TheState.dealerId, TheState.DefaultRiskTypeToleranceID)
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
       
        Dim objRiskTypeTolerance As New RiskTypeTolerance
        TheState.PageIndex = RiskTypeToleranceGrid.PageIndex

        If (TheState.RiskTypeToleranceDV IsNot Nothing AndAlso  TheState.RiskTypeToleranceDV.Count = 0) Then
            Dim dv As DataView = objRiskTypeTolerance.GetRiskTypeTolerance()
            TheState.bnoRow = True
            if dv IsNot Nothing Then
                objRiskTypeTolerance.GetEmptyList(dv)
            End If
            TheState.RiskTypeToleranceDV = Nothing
            TheState.MyBO = New RiskTypeTolerance
            TheState.MyBO.AddNewRowToSearchDV(TheState.RiskTypeToleranceDV, TheState.MyBO)
            RiskTypeToleranceGrid.DataSource = TheState.RiskTypeToleranceDV
            ThePage.HighLightSortColumn(RiskTypeToleranceGrid, SortDirection, True)
            RiskTypeToleranceGrid.DataBind()
            If Not RiskTypeToleranceGrid.BottomPagerRow.Visible Then RiskTypeToleranceGrid.BottomPagerRow.Visible = True
            RiskTypeToleranceGrid.Rows(0).Visible = False
            TheState.IsGridAddNew = True
            TheState.IsGridVisible = False
            lblRecordCount.Text = "0 " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
            If blnShowErr Then
                ThePage.MasterPage.MessageController.AddInformation(ElitaPlus.ElitaPlusWebApp.Message.MSG_NO_RECORDS_FOUND, True)
            End If
        Else
            TheState.bnoRow = False
            RiskTypeToleranceGrid.Enabled = True
            RiskTypeToleranceGrid.PageSize = TheState.PageSize
            RiskTypeToleranceGrid.DataSource = TheState.RiskTypeToleranceDV
            TheState.IsGridVisible = True
            ThePage.HighLightSortColumn(RiskTypeToleranceGrid, SortDirection, True)
            RiskTypeToleranceGrid.DataBind()
            If Not RiskTypeToleranceGrid.BottomPagerRow.Visible Then RiskTypeToleranceGrid.BottomPagerRow.Visible = True
        End If

        ControlMgr.SetVisibleControl(ThePage, RiskTypeToleranceGrid, TheState.IsGridVisible)

        If RiskTypeToleranceGrid.Visible AndAlso  TheState.RiskTypeToleranceDV IsNot Nothing Then

            Session("recCount") = TheState.RiskTypeToleranceDV.Count
            If Not RiskTypeToleranceGrid.BottomPagerRow.Visible Then RiskTypeToleranceGrid.BottomPagerRow.Visible = True
            If (TheState.IsGridAddNew ) Then
                lblRecordCount.Text = String.Format("{0} {1}", (TheState.RiskTypeToleranceDV.Count - 1), TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND))
            Else
                lblRecordCount.Text = String.Format("{0} {1}", TheState.RiskTypeToleranceDV.Count, TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND))
            End If
        End If
        ControlMgr.DisableEditDeleteGridIfNotEditAuth(ThePage, RiskTypeToleranceGrid)

    End Sub

    Private Sub Grid_PageIndexChanged(sender As Object, e As System.EventArgs) Handles RiskTypeToleranceGrid.PageIndexChanged
        Try
            If (Not (TheState.IsEditMode)) Then
                TheState.PageIndex = RiskTypeToleranceGrid.PageIndex
                TheState.DefaultRiskTypeToleranceID = Guid.Empty
                PopulateGrid()
            End If
        Catch ex As Exception
            ThePage.HandleErrors(ex, ThePage.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Grid_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles RiskTypeToleranceGrid.PageIndexChanging
        Try
            RiskTypeToleranceGrid.PageIndex = e.NewPageIndex
            TheState.PageIndex = RiskTypeToleranceGrid.PageIndex
        Catch ex As Exception
            ThePage.HandleErrors(ex, ThePage.MasterPage.MessageController)
        End Try
    End Sub

    Public Sub RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles RiskTypeToleranceGrid.RowCreated
        Try
            ThePage.BaseItemCreated(sender, e)
        Catch ex As Exception
            ThePage.HandleErrors(ex, ThePage.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Grid_SortCommand(source As Object, e As GridViewSortEventArgs) Handles RiskTypeToleranceGrid.Sorting
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

        RiskTypeToleranceGrid.EditIndex = NO_ROW_SELECTED_INDEX

        If (RiskTypeToleranceGrid.PageCount = 0) Then
            ControlMgr.SetVisibleControl(ThePage, RiskTypeToleranceGrid, False)
        Else
            ControlMgr.SetVisibleControl(ThePage, RiskTypeToleranceGrid, True)
        End If

        TheState.IsEditMode = False
        PopulateGrid()
        TheState.PageIndex = RiskTypeToleranceGrid.PageIndex
        SetControlState()
    End Sub

    Private Sub RemoveNewRowFromSearchDV()
        Dim rowind As Integer = ThePage.NO_ITEM_SELECTED_INDEX
        With TheState
            If .RiskTypeToleranceDV IsNot Nothing Then
                rowind = ThePage.FindSelectedRowIndexFromGuid(.RiskTypeToleranceDV, .DefaultRiskTypeToleranceID)
            End If
        End With
        If rowind <> ThePage.NO_ITEM_SELECTED_INDEX Then TheState.RiskTypeToleranceDV.Delete(rowind)
    End Sub

    Private Sub AddNew()
        If TheState.MyBO Is Nothing OrElse TheState.MyBO.IsNew = False Then
            TheState.MyBO = New RiskTypeTolerance
            TheState.MyBO.AddNewRowToSearchDV(TheState.RiskTypeToleranceDV, TheState.MyBO)
        End If
        TheState.DefaultRiskTypeToleranceID = TheState.MyBO.Id
        TheState.IsGridAddNew = True
        PopulateGrid()
        'Set focus on the Code TextBox for the EditItemIndex row
        ThePage.SetPageAndSelectedIndexFromGuid(TheState.RiskTypeToleranceDV, TheState.DefaultRiskTypeToleranceID, RiskTypeToleranceGrid, _
                                                TheState.PageIndex, TheState.IsEditMode)
        ControlMgr.DisableEditDeleteGridIfNotEditAuth(ThePage, RiskTypeToleranceGrid)
        ThePage.SetGridControls(RiskTypeToleranceGrid, False)

        Try
            RiskTypeToleranceGrid.Rows(RiskTypeToleranceGrid.SelectedIndex).Focus()
        Catch ex As Exception
            RiskTypeToleranceGrid.Focus()
        End Try

    End Sub
    Private Function PopulateBOFromForm() As Boolean
        Dim objDropDownList As DropDownList
        Dim RiskTypeTolerancePct As TextBox
       
        With TheState.MyBO

            RiskTypeTolerancePct = CType(RiskTypeToleranceGrid.Rows(RiskTypeToleranceGrid.EditIndex).Cells(GRID_COL_TOLERANCE_PCT).FindControl(GRID_CTRL_NAME_EDIT_TOLERANCE_PCT), TextBox)
           
            If (TheState.IsEditMode = True AndAlso TheState.IsGridAddNew = False) Then

                ThePage.PopulateBOProperty(TheState.MyBO, "RiskTypeToleranceId", New Guid(CType(RiskTypeToleranceGrid.DataKeys(RiskTypeToleranceGrid.EditIndex).Values(GRID_COL_DLR_RK_TYP_TOLERANCE_ID), Byte())))
            Elseif  TheState.IsGridAddNew = true
                ThePage.PopulateBOProperty(TheState.MyBO, "RiskTypeToleranceId", Guid.NewGuid())
            End If
           
            ThePage.PopulateBOProperty(TheState.MyBO, "DealerId", TheState.dealerId)
           
            objDropDownList = CType(RiskTypeToleranceGrid.Rows((RiskTypeToleranceGrid.EditIndex)).Cells(GRID_COL_RISK_TYPE).FindControl(GRID_CTRL_NAME_EDIT_RISK_TYPE), DropDownList)
            ThePage.PopulateBOProperty(TheState.MyBO, "RiskType", objDropDownList,false)
            ThePage.PopulateBOProperty(TheState.MyBO, "RiskTypeId", objDropDownList,true)

            ThePage.PopulateBOProperty(TheState.MyBO, "TolerancePct", RiskTypeTolerancePct) 

            
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
           
            If TheState.IsGridAddNew AndAlso TheState.MyBO.ValidateNewRiskTypeTolerance(TheState.RiskTypeToleranceDV) Then
                ThePage.MasterPage.MessageController.AddWarning(Assurant.ElitaPlus.Common.ErrorCodes.INVALID_RISK_TYPE_TOLERANCE, True)
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
                TheState.RiskTypeToleranceDV = Nothing
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
                    RiskTypeToleranceGrid.PageIndex = .PageIndex
                End If
                .DefaultRiskTypeToleranceID = Guid.Empty
                TheState.MyBO = Nothing
                .IsEditMode = False
            End With
            RiskTypeToleranceGrid.EditIndex = ThePage.NO_ITEM_SELECTED_INDEX

            PopulateGrid()
            SetControlState()
            RiskTypeToleranceGrid.Focus()
        Catch ex As Exception
            ThePage.HandleErrors(ex, ThePage.MasterPage.MessageController)
        End Try
    End Sub
#End Region


End Class