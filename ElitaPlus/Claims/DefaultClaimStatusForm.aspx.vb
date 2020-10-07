Imports System.Threading
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms
Imports Assurant.Elita.CommonConfiguration.DataElements

Namespace Claims

    Partial Public Class DefaultClaimStatusForm
        Inherits ElitaPlusSearchPage

#Region "Bread Crum"
        Private Sub UpdateBreadCrum()

            MasterPage.UsePageTabTitleInBreadCrum = False
            MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage(PAGESUBMENU) & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage(PAGETITLE)
            MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage(PAGETITLE)
            MasterPage.MessageController.Clear()
            MasterPage.BreadCrum = TranslationBase.TranslateLabelOrMessage(PAGETAB) & ElitaBase.Sperator & MasterPage.PageTab
        End Sub
#End Region

#Region "Constants"
        Public Const URL As String = "DefaultClaimStatusForm.aspx"
        Public Const PAGETITLE As String = "DEFAULT_CLAIM_STATUS"
        Public Const PAGETAB As String = "TABLES"
        Public Const PAGESUBMENU As String = "EXTENDED_CLAIM_STATUS"

        Private Const MSG_CONFIRM_PROMPT As String = "MSG_CONFIRM_PROMPT"
        Private Const MSG_RECORD_DELETED_OK As String = "MSG_RECORD_DELETED_OK"
        Private Const MSG_RECORD_SAVED_OK As String = "MSG_RECORD_SAVED_OK"
        Private Const MSG_RECORD_NOT_SAVED As String = "MSG_RECORD_NOT_SAVED"

        Private Const GRID_COL_DEFAULT_CLAIM_STATUS_ID_IDX As Integer = 0
        Private Const GRID_COL_DEFAULT_TYPE_ID_IDX As Integer = 1
        Private Const GRID_COL_DEFAULT_TYPE_IDX As Integer = 2
        Private Const GRID_COL_CLAIM_STATUS_BY_GROUP_ID_IDX As Integer = 3
        Private Const GRID_COL_CLAIM_STATUS_BY_GROUP_IDX As Integer = 4
        Private Const GRID_COL_METHOD_OF_REPAIR_ID_IDX As Integer = 5
        Private Const GRID_COL_METHOD_OF_REPAIR_IDX As Integer = 6
        Private Const GRID_COL_EDIT_IDX As Integer = 7
        Private Const GRID_COL_DELETE_IDX As Integer = 8

        Private Const GRID_CTRL_NAME_LABLE_DEFAULT_CLAIM_STATUS_ID As String = "lblDefaultClaimStatusID"
        Private Const GRID_CTRL_NAME_LABLE_DEFAULT_TYPE_ID As String = "lblDefaultTypeId"
        Private Const GRID_CTRL_NAME_LABLE_DEFAULT_TYPE As String = "lblDefaultType"
        Private Const GRID_CTRL_NAME_LABEL_CLAIM_STATUS_BY_GROUP_ID As String = "lblClaimStatusByGroupId"
        Private Const GRID_CTRL_NAME_LABEL_CLAIM_STATUS_BY_GROUP As String = "lblClaimStatusByGroup"
        Private Const GRID_CTRL_NAME_LABLE_METHOD_OF_REPAIR_ID As String = "lblMethodOfRepairId"
        Private Const GRID_CTRL_NAME_LABLE_METHOD_OF_REPAIR As String = "lblMethodOfRepair"

        Private Const GRID_CTRL_NAME_EDIT_DEFAULT_TYPE As String = "cboDefaultType"
        Private Const GRID_CTRL_NAME_EDIT_DEFAULT_CLAIM_STATUS As String = "cboClaimStatusByGroup"
        Private Const GRID_CTRL_NAME_EDIT_METHOD_OF_REPAIR As String = "cboMethodOfRepair"

        Private Const EDIT_COMMAND As String = "SelectAction"
        Private Const DELETE_COMMAND As String = "DeleteRecord"
        Private Const SORT_COMMAND As String = "Sort"

        Private Const NO_ROW_SELECTED_INDEX As Integer = -1

        Public Enum SearchByType
            Dealer
            CompanyGroup
        End Enum
#End Region

#Region "Page State"
        ' This class keeps the current state for the search page.
        Class MyState
            Public MyBO As DefaultClaimStatus
            Public DefaultClaimStatusID As Guid
            Public PageIndex As Integer = 0
            Public PageSize As Integer = DEFAULT_NEW_UI_PAGE_SIZE
            Public searchDV As DefaultClaimStatus.DefaultClaimStatusSearchDV = Nothing
            Public HasDataChanged As Boolean
            Public IsGridAddNew As Boolean = False
            Public IsEditMode As Boolean = False
            Public IsGridVisible As Boolean
            Public IsAfterSave As Boolean
            Public ActionInProgress As DetailPageCommand = DetailPageCommand.Nothing_
            Public SortExpression As String = DefaultClaimStatus.DefaultClaimStatusSearchDV.COL_DEFAULT_TYPE
            Public bnoRow As Boolean = False

            Public dealerId As Guid = Guid.Empty
            Public searchBy As Integer = 0
            Public isNew As String = "N"
        End Class

        Public Sub New()
            MyBase.New(New MyState)
        End Sub

        Protected Shadows ReadOnly Property State() As MyState
            Get
                Return CType(MyBase.State, MyState)
            End Get
        End Property

        Public ReadOnly Property IsGridInEditMode() As Boolean
            Get
                Return Grid.EditIndex > NO_ITEM_SELECTED_INDEX
            End Get
        End Property

        Public Property SortDirection() As String
            Get
                If ViewState("SortDirection") IsNot Nothing Then
                    Return ViewState("SortDirection").ToString
                Else
                    Return String.Empty
                End If

            End Get
            Set(value As String)
                ViewState("SortDirection") = value
            End Set
        End Property
#End Region

#Region "Page Return Type"
        Public Class ReturnType
            Public LastOperation As DetailPageCommand
            Public HasDataChanged As Boolean
            Public dealerId As Guid = Guid.Empty
            Public ObjectType As TargetType

            Public Sub New(LastOp As DetailPageCommand, dealerId As Guid, objectType As TargetType, hasDataChanged As Boolean)
                LastOperation = LastOp
                Me.HasDataChanged = hasDataChanged
                Me.dealerId = dealerId
                Me.ObjectType = objectType
            End Sub

            Public Enum TargetType
                Dealer
                CompanyGroup
            End Enum

        End Class
#End Region

#Region "Page Events"
        Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
            MasterPage.MessageController.Clear()

            Try
                If Not IsPostBack Then
                    UpdateBreadCrum()
                    ControlMgr.SetVisibleControl(Me, moSearchResults, False)
                    'SetControlState()
                    State.PageIndex = 0
                    TranslateGridHeader(Grid)
                    TranslateGridControls(Grid)
                    Search()
                Else
                    CheckIfComingFromDeleteConfirm()
                    BindBoPropertiesToGridHeaders()
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
            ShowMissingTranslations(MasterPage.MessageController)
        End Sub

        Private Sub Page_PageCall(CallFromUrl As String, CallingPar As Object) Handles MyBase.PageCall
            Try
                If CallingParameters IsNot Nothing Then
                    State.searchBy = CType(CType(CallingPar, ArrayList)(0), Integer)
                    State.dealerId = CType(CType(CallingPar, ArrayList)(1), Guid)
                    State.isNew = CType(CType(CallingPar, ArrayList)(2), String)
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub
#End Region

#Region "Helper functions"
        Protected Sub CheckIfComingFromDeleteConfirm()
            Dim confResponse As String = HiddenDeletePromptResponse.Value
            If confResponse IsNot Nothing AndAlso confResponse = MSG_VALUE_YES Then
                If Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Delete Then
                    DoDelete()
                End If
                Select Case State.ActionInProgress
                    Case ElitaPlusPage.DetailPageCommand.Delete
                End Select
            ElseIf confResponse IsNot Nothing AndAlso confResponse = MSG_VALUE_NO Then
                Select Case State.ActionInProgress
                    Case ElitaPlusPage.DetailPageCommand.Delete
                End Select
            End If
            'Clean after consuming the action
            State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
            HiddenDeletePromptResponse.Value = ""
        End Sub

        Private Sub DoDelete()
            'Do the delete here
            State.ActionInProgress = DetailPageCommand.Nothing_
            'Save the RiskTypeId in the Session

            Dim obj As DefaultClaimStatus = New DefaultClaimStatus(State.DefaultClaimStatusID)

            obj.Delete()

            'Call the Save() method in the Role Business Object here

            obj.Save()

            MasterPage.MessageController.AddSuccess(MSG_RECORD_DELETED_OK, True)

            State.PageIndex = Grid.PageIndex

            'Set the IsAfterSave flag to TRUE so that the Paging logic gets invoked
            State.IsAfterSave = True
            State.searchDV = Nothing
            PopulateGrid()
            State.PageIndex = Grid.PageIndex
            State.IsEditMode = False
            SetControlState()
        End Sub

        Private Sub SetControlState()
            If (State.IsEditMode) Then
                ControlMgr.SetVisibleControl(Me, NewButton_WRITE, False)
                If (cboPageSize.Enabled) Then
                    ControlMgr.SetEnableControl(Me, cboPageSize, False)
                End If
            Else
                ControlMgr.SetVisibleControl(Me, NewButton_WRITE, True)
                If Not (cboPageSize.Enabled) Then
                    ControlMgr.SetEnableControl(Me, cboPageSize, True)
                End If
            End If
        End Sub

        Protected Sub BindBoPropertiesToGridHeaders()
            BindBOPropertyToGridHeader(State.MyBO, "DefaultTypeId", Grid.Columns(GRID_COL_DEFAULT_TYPE_IDX))
            BindBOPropertyToGridHeader(State.MyBO, "ClaimStatusByGroupId", Grid.Columns(GRID_COL_CLAIM_STATUS_BY_GROUP_IDX))
            BindBOPropertyToGridHeader(State.MyBO, "MethodOfRepairId", Grid.Columns(GRID_COL_METHOD_OF_REPAIR_IDX))
            ClearGridViewHeadersAndLabelsErrSign()
        End Sub

        Private Sub ReturnFromEditing()

            Grid.EditIndex = NO_ROW_SELECTED_INDEX

            If (Grid.PageCount = 0) Then
                'if returning to the "1st time in" screen
                ControlMgr.SetVisibleControl(Me, Grid, False)
            Else
                ControlMgr.SetVisibleControl(Me, Grid, True)
            End If

            State.IsEditMode = False
            PopulateGrid()
            State.PageIndex = Grid.PageIndex
            SetControlState()
        End Sub

        Private Sub RemoveNewRowFromSearchDV()
            Dim rowind As Integer = NO_ITEM_SELECTED_INDEX
            With State
                If .searchDV IsNot Nothing Then
                    rowind = FindSelectedRowIndexFromGuid(.searchDV, .DefaultClaimStatusID)
                End If
            End With
            If rowind <> NO_ITEM_SELECTED_INDEX Then State.searchDV.Delete(rowind)
        End Sub

        Private Function PopulateBOFromForm() As Boolean
            Dim objDropDownList As DropDownList
            With State.MyBO
                objDropDownList = CType(Grid.Rows((Grid.EditIndex)).Cells(GRID_COL_DEFAULT_TYPE_IDX).FindControl(GRID_CTRL_NAME_EDIT_DEFAULT_TYPE), DropDownList)
                PopulateBOProperty(State.MyBO, "DefaultTypeId", objDropDownList)

                objDropDownList = CType(Grid.Rows((Grid.EditIndex)).Cells(GRID_COL_CLAIM_STATUS_BY_GROUP_IDX).FindControl(GRID_CTRL_NAME_EDIT_DEFAULT_CLAIM_STATUS), DropDownList)
                PopulateBOProperty(State.MyBO, "ClaimStatusByGroupId", objDropDownList)

                objDropDownList = CType(Grid.Rows((Grid.EditIndex)).Cells(GRID_COL_METHOD_OF_REPAIR_IDX).FindControl(GRID_CTRL_NAME_EDIT_METHOD_OF_REPAIR), DropDownList)
                PopulateBOProperty(State.MyBO, "MethodOfRepairId", objDropDownList)
            End With
            If ErrCollection.Count > 0 Then
                Throw New PopulateBOErrorException
            End If
        End Function
#End Region

#Region "Grid related"

        Public Sub PopulateGrid()
            Dim blnNewSearch As Boolean = False
            Dim dv As DataView
            Try
                'Refresh the DataView and Call SetPageAndSelectedIndexFromGuid() to go to the Page 
                'where the most recently saved Record exists in the DataView
                With State
                    If (.searchDV Is Nothing) Then
                        .searchDV = DefaultClaimStatus.getList(Guid.Empty)
                        blnNewSearch = True
                    End If
                End With

                State.searchDV.Sort = SortDirection
                If (State.IsAfterSave) Then
                    State.IsAfterSave = False
                    SetPageAndSelectedIndexFromGuid(State.searchDV, State.DefaultClaimStatusID, Grid, State.PageIndex)
                ElseIf (State.IsEditMode) Then
                    SetPageAndSelectedIndexFromGuid(State.searchDV, State.DefaultClaimStatusID, Grid, State.PageIndex, State.IsEditMode)
                Else
                    SetPageAndSelectedIndexFromGuid(State.searchDV, Guid.Empty, Grid, State.PageIndex)
                End If

                Grid.AutoGenerateColumns = False
                Grid.Columns(GRID_COL_DEFAULT_TYPE_IDX).SortExpression = DefaultClaimStatus.DefaultClaimStatusSearchDV.COL_DEFAULT_TYPE
                Grid.Columns(GRID_COL_CLAIM_STATUS_BY_GROUP_IDX).SortExpression = DefaultClaimStatus.DefaultClaimStatusSearchDV.COL_CLAIM_STATUS_BY_GROUP
                SortAndBindGrid(blnNewSearch)

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub

        Private Sub SortAndBindGrid(Optional ByVal blnShowErr As Boolean = True)

            TranslateGridControls(Grid)
            Dim dv As New DataView
            State.PageIndex = Grid.PageIndex

            If (State.searchDV.Count = 0) Then

                dv = DefaultClaimStatus.getList(Guid.Empty)

                State.bnoRow = True
                dv = DefaultClaimStatus.getEmptyList(dv)
                State.searchDV = Nothing
                State.MyBO = New DefaultClaimStatus
                State.MyBO.AddNewRowToSearchDV(State.searchDV, State.MyBO)
                Grid.DataSource = dv
                Grid.DataBind()
                Grid.Rows(0).Visible = False
                State.IsGridAddNew = True
                State.IsGridVisible = False
                If blnShowErr Then
                    MasterPage.MessageController.AddInformation(ElitaPlus.ElitaPlusWebApp.Message.MSG_NO_RECORDS_FOUND, True)
                End If
            Else
                State.bnoRow = False
                Grid.Enabled = True
                Grid.PageSize = State.PageSize
                Grid.DataSource = State.searchDV
                State.IsGridVisible = True
                HighLightSortColumn(Grid, SortDirection)
                Grid.DataBind()
            End If


            ControlMgr.SetVisibleControl(Me, Grid, State.IsGridVisible)
            ControlMgr.SetVisibleControl(Me, moSearchResults, State.IsGridVisible)

            Session("recCount") = State.searchDV.Count

            If Grid.Visible Then
                If (State.IsGridAddNew) Then
                    lblRecordCount.Text = (State.searchDV.Count - 1) & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                Else
                    lblRecordCount.Text = State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                End If
            End If
            ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me, Grid)

        End Sub

        Private Sub Grid_PageIndexChanged(sender As Object, e As System.EventArgs) Handles Grid.PageIndexChanged
            Try
                If (Not (State.IsEditMode)) Then
                    State.PageIndex = Grid.PageIndex
                    State.DefaultClaimStatusID = Guid.Empty
                    PopulateGrid()
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub Grid_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Grid.PageIndexChanging
            Try
                Grid.PageIndex = e.NewPageIndex
                State.PageIndex = Grid.PageIndex
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub Grid_Sorting(sender As Object, e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles Grid.Sorting
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

                State.PageIndex = 0
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub cboPageSize_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
            Try
                State.PageSize = CType(cboPageSize.SelectedValue, Integer)
                State.PageIndex = NewCurrentPageIndex(Grid, State.searchDV.Count, State.PageSize)
                Grid.PageIndex = State.PageIndex
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub Grid_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowDataBound
            Try
                Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
                Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
                Dim strID As String

                If dvRow IsNot Nothing AndAlso Not State.bnoRow Then
                    strID = GetGuidStringFromByteArray(CType(dvRow(DefaultClaimStatus.DefaultClaimStatusSearchDV.COL_DEFAULT_CLAIM_STATUS_ID), Byte()))
                    If itemType = ListItemType.Item OrElse itemType = ListItemType.AlternatingItem OrElse itemType = ListItemType.SelectedItem Then
                        CType(e.Row.Cells(GRID_COL_DEFAULT_CLAIM_STATUS_ID_IDX).FindControl(GRID_CTRL_NAME_LABLE_DEFAULT_CLAIM_STATUS_ID), Label).Text = strID

                        If (State.IsEditMode = True AndAlso State.DefaultClaimStatusID.ToString.Equals(strID)) Then
                            Dim moDefaultTypeDropDown As DropDownList = CType(e.Row.Cells(GRID_COL_DEFAULT_TYPE_IDX).FindControl(GRID_CTRL_NAME_EDIT_DEFAULT_TYPE), DropDownList)
                           ' ElitaPlusPage.BindListControlToDataView(moDefaultTypeDropDown, LookupListNew.GetExtendedClaimStatusDefaultTypes(ElitaPlusIdentity.Current.ActiveUser.LanguageId))
                            moDefaultTypeDropDown.Populate(CommonConfigManager.Current.ListManager.GetList("ECSDT",Thread.CurrentPrincipal.GetLanguageCode()),New PopulateOptions() With
                            {
                                    .AddBlankItem = True
                             })

                            If (dvRow(DefaultClaimStatus.DefaultClaimStatusSearchDV.COL_DEFAULT_TYPE).ToString() <> String.Empty) Then
                                SetSelectedItemByText(moDefaultTypeDropDown, dvRow(DefaultClaimStatus.DefaultClaimStatusSearchDV.COL_DEFAULT_TYPE).ToString())
                            End If

                            Dim moClaimStatusByGroupDropDown As DropDownList = CType(e.Row.Cells(GRID_COL_CLAIM_STATUS_BY_GROUP_IDX).FindControl(GRID_CTRL_NAME_EDIT_DEFAULT_CLAIM_STATUS), DropDownList)
                            'ElitaPlusPage.BindListControlToDataView(moClaimStatusByGroupDropDown, LookupListNew.GetExtendedStatusByGroupLookupList(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id, ElitaPlusIdentity.Current.ActiveUser.LanguageId))                            
                             Dim listcontext As ListContext = New ListContext()
                             listcontext.CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id

                             Dim oExtendedStatus As ListItem() = CommonConfigManager.Current.ListManager.GetList("ExtendedStatusByCompanyGroup", Thread.CurrentPrincipal.GetLanguageCode(), listcontext)
                             moClaimStatusByGroupDropDown.Populate(oExtendedStatus, New PopulateOptions() With
                                                  {
                                                    .AddBlankItem = True
                                                   })
                            If (dvRow(DefaultClaimStatus.DefaultClaimStatusSearchDV.COL_CLAIM_STATUS_BY_GROUP).ToString() <> String.Empty) Then
                                SetSelectedItemByText(moClaimStatusByGroupDropDown, dvRow(DefaultClaimStatus.DefaultClaimStatusSearchDV.COL_CLAIM_STATUS_BY_GROUP).ToString())
                            End If

                            Dim moMethodOfRepairDropDown As DropDownList = CType(e.Row.Cells(GRID_COL_METHOD_OF_REPAIR_IDX).FindControl(GRID_CTRL_NAME_EDIT_METHOD_OF_REPAIR), DropDownList)
                           ' ElitaPlusPage.BindListControlToDataView(moMethodOfRepairDropDown, LookupListNew.GetMethodOfRepairLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId))
                               moMethodOfRepairDropDown.Populate(CommonConfigManager.Current.ListManager.GetList("METHR",Thread.CurrentPrincipal.GetLanguageCode()),New PopulateOptions() With
                                            {
                                                .AddBlankItem = True
                                            })
                            If (dvRow(DefaultClaimStatus.DefaultClaimStatusSearchDV.COL_METHOD_OF_REPAIR).ToString() <> String.Empty) Then
                                SetSelectedItemByText(moMethodOfRepairDropDown, dvRow(DefaultClaimStatus.DefaultClaimStatusSearchDV.COL_METHOD_OF_REPAIR).ToString())
                            End If

                            'CType(e.Row.Cells(Me.GRID_COL_DEFAULT_TYPE_IDX).FindControl(Me.GRID_CTRL_NAME_EDIT_DEFAULT_TYPE), DropDownList).Text = dvRow(DefaultClaimStatus.DefaultClaimStatusSearchDV.COL_DEFAULT_TYPE).ToString
                            'If (Me.State.IsGridAddNew) Then
                            ' CType(e.Row.Cells(Me.GRID_COL_DEFAULT_TYPE_IDX).FindControl(Me.GRID_CTRL_NAME_EDIT_DEFAULT_TYPE), TextBox).ReadOnly = False
                            'Else
                            'CType(e.Row.Cells(Me.GRID_COL_DEFAULT_TYPE_IDX).FindControl(Me.GRID_CTRL_NAME_EDIT_DEFAULT_TYPE), TextBox).ReadOnly = True
                            'End If
                            'CType(e.Row.Cells(Me.GRID_COL_CLAIM_STATUS_BY_GROUP_IDX).FindControl(Me.GRID_CTRL_NAME_EDIT_DEFAULT_CLAIM_STATUS), TextBox).Text = dvRow(DefaultClaimStatus.DefaultClaimStatusSearchDV.COL_CLAIM_STATUS_BY_GROUP).ToString
                            'CType(e.Row.Cells(Me.GRID_COL_METHOD_OF_REPAIR_IDX).FindControl(Me.GRID_CTRL_NAME_EDIT_METHOD_OF_REPAIR), TextBox).Text = dvRow(DefaultClaimStatus.DefaultClaimStatusSearchDV.COL_METHOD_OF_REPAIR).ToString
                        Else
                            'CType(e.Row.Cells(Me.GRID_COL_DEFAULT_TYPE_IDX).FindControl(Me.GRID_CTRL_NAME_LABLE_DEFAULT_TYPE), Label).Text = dvRow(DefaultClaimStatus.DefaultClaimStatusSearchDV.COL_DEFAULT_TYPE).ToString
                            'CType(e.Row.Cells(Me.GRID_COL_CLAIM_STATUS_BY_GROUP_IDX).FindControl(Me.GRID_CTRL_NAME_LABEL_CLAIM_STATUS_BY_GROUP), Label).Text = dvRow(DefaultClaimStatus.DefaultClaimStatusSearchDV.COL_CLAIM_STATUS_BY_GROUP).ToString
                            'CType(e.Row.Cells(Me.GRID_COL_METHOD_OF_REPAIR_IDX).FindControl(Me.GRID_CTRL_NAME_LABLE_METHOD_OF_REPAIR), Label).Text = dvRow(DefaultClaimStatus.DefaultClaimStatusSearchDV.COL_METHOD_OF_REPAIR).ToString
                        End If
                    End If
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub

        Public Sub RowCommand(source As System.Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs)

            Try
                Dim index As Integer
                If (e.CommandName = EDIT_COMMAND) Then
                    index = CInt(e.CommandArgument)
                    'Do the Edit here

                    'Set the IsEditMode flag to TRUE
                    State.IsEditMode = True

                    State.DefaultClaimStatusID = New Guid(CType(Grid.Rows(index).Cells(GRID_COL_DEFAULT_CLAIM_STATUS_ID_IDX).FindControl(GRID_CTRL_NAME_LABLE_DEFAULT_CLAIM_STATUS_ID), Label).Text)
                    State.MyBO = New DefaultClaimStatus(State.DefaultClaimStatusID)

                    PopulateGrid()

                    State.PageIndex = Grid.PageIndex

                    SetControlState()

                ElseIf (e.CommandName = DELETE_COMMAND) Then
                    index = CInt(e.CommandArgument)
                    State.DefaultClaimStatusID = New Guid(CType(Grid.Rows(index).Cells(GRID_COL_DEFAULT_CLAIM_STATUS_ID_IDX).FindControl(GRID_CTRL_NAME_LABLE_DEFAULT_CLAIM_STATUS_ID), Label).Text)
                    DisplayMessage(Message.DELETE_RECORD_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenDeletePromptResponse)
                    State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Delete

                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub

        Public Sub RowCreated(sender As System.Object, e As System.Web.UI.WebControls.GridViewRowEventArgs)
            Try
                BaseItemCreated(sender, e)
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

#End Region

#Region "Control Handler"

        Private Sub Search()
            Try
                With State
                    .PageIndex = 0
                    .DefaultClaimStatusID = Guid.Empty
                    .MyBO = Nothing
                    .IsGridVisible = True
                    .searchDV = Nothing
                    .HasDataChanged = False
                    .IsGridAddNew = False
                End With
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub NewButton_WRITE_Click(sender As Object, e As EventArgs) Handles NewButton_WRITE.Click

            Try
                State.IsEditMode = True
                State.IsGridVisible = True
                State.IsGridAddNew = True
                AddNew()
                SetControlState()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub

        Private Sub AddNew()
            If State.MyBO Is Nothing OrElse State.MyBO.IsNew = False Then
                State.MyBO = New DefaultClaimStatus
                State.MyBO.AddNewRowToSearchDV(State.searchDV, State.MyBO)
            End If
            State.DefaultClaimStatusID = State.MyBO.Id
            State.IsGridAddNew = True
            PopulateGrid()
            'Set focus on the Code TextBox for the EditItemIndex row
            SetPageAndSelectedIndexFromGuid(State.searchDV, State.DefaultClaimStatusID, Grid, _
                                               State.PageIndex, State.IsEditMode)
            ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me, Grid)
            SetGridControls(Grid, False)
        End Sub

        Protected Sub btnSave_Click(sender As Object, e As EventArgs)

            Try
                PopulateBOFromForm()
                If (State.MyBO.IsDirty) Then
                    State.MyBO.Save()
                    State.IsAfterSave = True
                    State.IsGridAddNew = False
                    MasterPage.MessageController.AddSuccess(MSG_RECORD_SAVED_OK, True)
                    State.searchDV = Nothing
                    State.MyBO = Nothing
                    ReturnFromEditing()
                Else
                    MasterPage.MessageController.AddWarning(MSG_RECORD_NOT_SAVED, True)
                    ReturnFromEditing()
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub

        Protected Sub btnCancel_Click(sender As Object, e As EventArgs)
            Try
                With State
                    If .IsGridAddNew Then
                        RemoveNewRowFromSearchDV()
                        .IsGridAddNew = False
                        Grid.PageIndex = .PageIndex
                    End If
                    .DefaultClaimStatusID = Guid.Empty
                    State.MyBO = Nothing
                    .IsEditMode = False
                End With
                Grid.EditIndex = NO_ITEM_SELECTED_INDEX
                'Me.State.searchDV = Nothing
                PopulateGrid()
                SetControlState()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub
        Private Sub btnBack_Click(sender As Object, e As System.EventArgs) Handles btnBack.Click
            Try
                Back(ElitaPlusPage.DetailPageCommand.Back)
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub Back(cmd As ElitaPlusPage.DetailPageCommand)
            If State.searchBy = SearchByType.CompanyGroup Then
                ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Nothing, ReturnType.TargetType.CompanyGroup, False))
            Else
                ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, State.dealerId, ReturnType.TargetType.Dealer, False))
            End If
        End Sub
#End Region
    End Class

End Namespace