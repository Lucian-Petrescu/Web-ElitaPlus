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

            Me.MasterPage.UsePageTabTitleInBreadCrum = False
            Me.MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage(PAGESUBMENU) & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage(PAGETITLE)
            Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage(PAGETITLE)
            Me.MasterPage.MessageController.Clear()
            Me.MasterPage.BreadCrum = TranslationBase.TranslateLabelOrMessage(PAGETAB) & ElitaBase.Sperator & Me.MasterPage.PageTab
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
                Return Me.Grid.EditIndex > Me.NO_ITEM_SELECTED_INDEX
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

#Region "Page Return Type"
        Public Class ReturnType
            Public LastOperation As DetailPageCommand
            Public HasDataChanged As Boolean
            Public dealerId As Guid = Guid.Empty
            Public ObjectType As TargetType

            Public Sub New(ByVal LastOp As DetailPageCommand, ByVal dealerId As Guid, ByVal objectType As TargetType, ByVal hasDataChanged As Boolean)
                Me.LastOperation = LastOp
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
        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            Me.MasterPage.MessageController.Clear()

            Try
                If Not Me.IsPostBack Then
                    UpdateBreadCrum()
                    ControlMgr.SetVisibleControl(Me, moSearchResults, False)
                    'SetControlState()
                    Me.State.PageIndex = 0
                    Me.TranslateGridHeader(Grid)
                    Me.TranslateGridControls(Grid)
                    Search()
                Else
                    CheckIfComingFromDeleteConfirm()
                    BindBoPropertiesToGridHeaders()
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
            Me.ShowMissingTranslations(Me.MasterPage.MessageController)
        End Sub

        Private Sub Page_PageCall(ByVal CallFromUrl As String, ByVal CallingPar As Object) Handles MyBase.PageCall
            Try
                If Not Me.CallingParameters Is Nothing Then
                    Me.State.searchBy = CType(CType(CallingPar, ArrayList)(0), Integer)
                    Me.State.dealerId = CType(CType(CallingPar, ArrayList)(1), Guid)
                    Me.State.isNew = CType(CType(CallingPar, ArrayList)(2), String)
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try

        End Sub
#End Region

#Region "Helper functions"
        Protected Sub CheckIfComingFromDeleteConfirm()
            Dim confResponse As String = Me.HiddenDeletePromptResponse.Value
            If Not confResponse Is Nothing AndAlso confResponse = Me.MSG_VALUE_YES Then
                If Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Delete Then
                    DoDelete()
                End If
                Select Case Me.State.ActionInProgress
                    Case ElitaPlusPage.DetailPageCommand.Delete
                End Select
            ElseIf Not confResponse Is Nothing AndAlso confResponse = Me.MSG_VALUE_NO Then
                Select Case Me.State.ActionInProgress
                    Case ElitaPlusPage.DetailPageCommand.Delete
                End Select
            End If
            'Clean after consuming the action
            Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
            Me.HiddenDeletePromptResponse.Value = ""
        End Sub

        Private Sub DoDelete()
            'Do the delete here
            Me.State.ActionInProgress = DetailPageCommand.Nothing_
            'Save the RiskTypeId in the Session

            Dim obj As DefaultClaimStatus = New DefaultClaimStatus(Me.State.DefaultClaimStatusID)

            obj.Delete()

            'Call the Save() method in the Role Business Object here

            obj.Save()

            Me.MasterPage.MessageController.AddSuccess(Me.MSG_RECORD_DELETED_OK, True)

            Me.State.PageIndex = Grid.PageIndex

            'Set the IsAfterSave flag to TRUE so that the Paging logic gets invoked
            Me.State.IsAfterSave = True
            Me.State.searchDV = Nothing
            PopulateGrid()
            Me.State.PageIndex = Grid.PageIndex
            Me.State.IsEditMode = False
            SetControlState()
        End Sub

        Private Sub SetControlState()
            If (Me.State.IsEditMode) Then
                ControlMgr.SetVisibleControl(Me, NewButton_WRITE, False)
                If (Me.cboPageSize.Enabled) Then
                    ControlMgr.SetEnableControl(Me, cboPageSize, False)
                End If
            Else
                ControlMgr.SetVisibleControl(Me, NewButton_WRITE, True)
                If Not (Me.cboPageSize.Enabled) Then
                    ControlMgr.SetEnableControl(Me, Me.cboPageSize, True)
                End If
            End If
        End Sub

        Protected Sub BindBoPropertiesToGridHeaders()
            Me.BindBOPropertyToGridHeader(Me.State.MyBO, "DefaultTypeId", Me.Grid.Columns(Me.GRID_COL_DEFAULT_TYPE_IDX))
            Me.BindBOPropertyToGridHeader(Me.State.MyBO, "ClaimStatusByGroupId", Me.Grid.Columns(Me.GRID_COL_CLAIM_STATUS_BY_GROUP_IDX))
            Me.BindBOPropertyToGridHeader(Me.State.MyBO, "MethodOfRepairId", Me.Grid.Columns(Me.GRID_COL_METHOD_OF_REPAIR_IDX))
            Me.ClearGridViewHeadersAndLabelsErrSign()
        End Sub

        Private Sub ReturnFromEditing()

            Grid.EditIndex = NO_ROW_SELECTED_INDEX

            If (Me.Grid.PageCount = 0) Then
                'if returning to the "1st time in" screen
                ControlMgr.SetVisibleControl(Me, Grid, False)
            Else
                ControlMgr.SetVisibleControl(Me, Grid, True)
            End If

            Me.State.IsEditMode = False
            Me.PopulateGrid()
            Me.State.PageIndex = Grid.PageIndex
            SetControlState()
        End Sub

        Private Sub RemoveNewRowFromSearchDV()
            Dim rowind As Integer = NO_ITEM_SELECTED_INDEX
            With State
                If Not .searchDV Is Nothing Then
                    rowind = FindSelectedRowIndexFromGuid(.searchDV, .DefaultClaimStatusID)
                End If
            End With
            If rowind <> NO_ITEM_SELECTED_INDEX Then State.searchDV.Delete(rowind)
        End Sub

        Private Function PopulateBOFromForm() As Boolean
            Dim objDropDownList As DropDownList
            With Me.State.MyBO
                objDropDownList = CType(Grid.Rows((Me.Grid.EditIndex)).Cells(GRID_COL_DEFAULT_TYPE_IDX).FindControl(GRID_CTRL_NAME_EDIT_DEFAULT_TYPE), DropDownList)
                PopulateBOProperty(State.MyBO, "DefaultTypeId", objDropDownList)

                objDropDownList = CType(Grid.Rows((Me.Grid.EditIndex)).Cells(GRID_COL_CLAIM_STATUS_BY_GROUP_IDX).FindControl(GRID_CTRL_NAME_EDIT_DEFAULT_CLAIM_STATUS), DropDownList)
                PopulateBOProperty(State.MyBO, "ClaimStatusByGroupId", objDropDownList)

                objDropDownList = CType(Grid.Rows((Me.Grid.EditIndex)).Cells(GRID_COL_METHOD_OF_REPAIR_IDX).FindControl(GRID_CTRL_NAME_EDIT_METHOD_OF_REPAIR), DropDownList)
                PopulateBOProperty(State.MyBO, "MethodOfRepairId", objDropDownList)
            End With
            If Me.ErrCollection.Count > 0 Then
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

                Me.State.searchDV.Sort = Me.SortDirection
                If (Me.State.IsAfterSave) Then
                    Me.State.IsAfterSave = False
                    Me.SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.DefaultClaimStatusID, Me.Grid, Me.State.PageIndex)
                ElseIf (Me.State.IsEditMode) Then
                    Me.SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.DefaultClaimStatusID, Me.Grid, Me.State.PageIndex, Me.State.IsEditMode)
                Else
                    Me.SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Guid.Empty, Me.Grid, Me.State.PageIndex)
                End If

                Me.Grid.AutoGenerateColumns = False
                Me.Grid.Columns(Me.GRID_COL_DEFAULT_TYPE_IDX).SortExpression = DefaultClaimStatus.DefaultClaimStatusSearchDV.COL_DEFAULT_TYPE
                Me.Grid.Columns(Me.GRID_COL_CLAIM_STATUS_BY_GROUP_IDX).SortExpression = DefaultClaimStatus.DefaultClaimStatusSearchDV.COL_CLAIM_STATUS_BY_GROUP
                SortAndBindGrid(blnNewSearch)

            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try

        End Sub

        Private Sub SortAndBindGrid(Optional ByVal blnShowErr As Boolean = True)

            Me.TranslateGridControls(Grid)
            Dim dv As New DataView
            Me.State.PageIndex = Me.Grid.PageIndex

            If (Me.State.searchDV.Count = 0) Then

                dv = DefaultClaimStatus.getList(Guid.Empty)

                Me.State.bnoRow = True
                dv = DefaultClaimStatus.getEmptyList(dv)
                Me.State.searchDV = Nothing
                Me.State.MyBO = New DefaultClaimStatus
                State.MyBO.AddNewRowToSearchDV(Me.State.searchDV, Me.State.MyBO)
                Me.Grid.DataSource = dv
                Me.Grid.DataBind()
                Me.Grid.Rows(0).Visible = False
                Me.State.IsGridAddNew = True
                Me.State.IsGridVisible = False
                If blnShowErr Then
                    Me.MasterPage.MessageController.AddInformation(ElitaPlus.ElitaPlusWebApp.Message.MSG_NO_RECORDS_FOUND, True)
                End If
            Else
                Me.State.bnoRow = False
                Me.Grid.Enabled = True
                Me.Grid.PageSize = Me.State.PageSize
                Me.Grid.DataSource = Me.State.searchDV
                Me.State.IsGridVisible = True
                HighLightSortColumn(Grid, Me.SortDirection)
                Me.Grid.DataBind()
            End If


            ControlMgr.SetVisibleControl(Me, Grid, Me.State.IsGridVisible)
            ControlMgr.SetVisibleControl(Me, moSearchResults, Me.State.IsGridVisible)

            Session("recCount") = Me.State.searchDV.Count

            If Me.Grid.Visible Then
                If (Me.State.IsGridAddNew) Then
                    Me.lblRecordCount.Text = (Me.State.searchDV.Count - 1) & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                Else
                    Me.lblRecordCount.Text = Me.State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                End If
            End If
            ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me, Grid)

        End Sub

        Private Sub Grid_PageIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Grid.PageIndexChanged
            Try
                If (Not (Me.State.IsEditMode)) Then
                    Me.State.PageIndex = Grid.PageIndex
                    Me.State.DefaultClaimStatusID = Guid.Empty
                    Me.PopulateGrid()
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub Grid_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Grid.PageIndexChanging
            Try
                Grid.PageIndex = e.NewPageIndex
                State.PageIndex = Grid.PageIndex
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub Grid_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles Grid.Sorting
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

                Me.State.PageIndex = 0
                Me.PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub cboPageSize_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
            Try
                State.PageSize = CType(cboPageSize.SelectedValue, Integer)
                Me.State.PageIndex = NewCurrentPageIndex(Grid, State.searchDV.Count, State.PageSize)
                Me.Grid.PageIndex = Me.State.PageIndex
                Me.PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub Grid_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowDataBound
            Try
                Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
                Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
                Dim strID As String

                If Not dvRow Is Nothing And Not Me.State.bnoRow Then
                    strID = GetGuidStringFromByteArray(CType(dvRow(DefaultClaimStatus.DefaultClaimStatusSearchDV.COL_DEFAULT_CLAIM_STATUS_ID), Byte()))
                    If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then
                        CType(e.Row.Cells(Me.GRID_COL_DEFAULT_CLAIM_STATUS_ID_IDX).FindControl(Me.GRID_CTRL_NAME_LABLE_DEFAULT_CLAIM_STATUS_ID), Label).Text = strID

                        If (Me.State.IsEditMode = True AndAlso Me.State.DefaultClaimStatusID.ToString.Equals(strID)) Then
                            Dim moDefaultTypeDropDown As DropDownList = CType(e.Row.Cells(Me.GRID_COL_DEFAULT_TYPE_IDX).FindControl(Me.GRID_CTRL_NAME_EDIT_DEFAULT_TYPE), DropDownList)
                           ' ElitaPlusPage.BindListControlToDataView(moDefaultTypeDropDown, LookupListNew.GetExtendedClaimStatusDefaultTypes(ElitaPlusIdentity.Current.ActiveUser.LanguageId))
                            moDefaultTypeDropDown.Populate(CommonConfigManager.Current.ListManager.GetList("ECSDT",Thread.CurrentPrincipal.GetLanguageCode()),New PopulateOptions() With
                            {
                                    .AddBlankItem = True
                             })

                            If (dvRow(DefaultClaimStatus.DefaultClaimStatusSearchDV.COL_DEFAULT_TYPE).ToString() <> String.Empty) Then
                                Me.SetSelectedItemByText(moDefaultTypeDropDown, dvRow(DefaultClaimStatus.DefaultClaimStatusSearchDV.COL_DEFAULT_TYPE).ToString())
                            End If

                            Dim moClaimStatusByGroupDropDown As DropDownList = CType(e.Row.Cells(Me.GRID_COL_CLAIM_STATUS_BY_GROUP_IDX).FindControl(Me.GRID_CTRL_NAME_EDIT_DEFAULT_CLAIM_STATUS), DropDownList)
                            'ElitaPlusPage.BindListControlToDataView(moClaimStatusByGroupDropDown, LookupListNew.GetExtendedStatusByGroupLookupList(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id, ElitaPlusIdentity.Current.ActiveUser.LanguageId))                            
                             Dim listcontext As ListContext = New ListContext()
                             listcontext.CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id

                             Dim oExtendedStatus As ListItem() = CommonConfigManager.Current.ListManager.GetList("ExtendedStatusByCompanyGroup", Thread.CurrentPrincipal.GetLanguageCode(), listcontext)
                             moClaimStatusByGroupDropDown.Populate(oExtendedStatus, New PopulateOptions() With
                                                  {
                                                    .AddBlankItem = True
                                                   })
                            If (dvRow(DefaultClaimStatus.DefaultClaimStatusSearchDV.COL_CLAIM_STATUS_BY_GROUP).ToString() <> String.Empty) Then
                                Me.SetSelectedItemByText(moClaimStatusByGroupDropDown, dvRow(DefaultClaimStatus.DefaultClaimStatusSearchDV.COL_CLAIM_STATUS_BY_GROUP).ToString())
                            End If

                            Dim moMethodOfRepairDropDown As DropDownList = CType(e.Row.Cells(Me.GRID_COL_METHOD_OF_REPAIR_IDX).FindControl(Me.GRID_CTRL_NAME_EDIT_METHOD_OF_REPAIR), DropDownList)
                           ' ElitaPlusPage.BindListControlToDataView(moMethodOfRepairDropDown, LookupListNew.GetMethodOfRepairLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId))
                               moMethodOfRepairDropDown.Populate(CommonConfigManager.Current.ListManager.GetList("METHR",Thread.CurrentPrincipal.GetLanguageCode()),New PopulateOptions() With
                                            {
                                                .AddBlankItem = True
                                            })
                            If (dvRow(DefaultClaimStatus.DefaultClaimStatusSearchDV.COL_METHOD_OF_REPAIR).ToString() <> String.Empty) Then
                                Me.SetSelectedItemByText(moMethodOfRepairDropDown, dvRow(DefaultClaimStatus.DefaultClaimStatusSearchDV.COL_METHOD_OF_REPAIR).ToString())
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
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try

        End Sub

        Public Sub RowCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)

            Try
                Dim index As Integer
                If (e.CommandName = Me.EDIT_COMMAND) Then
                    index = CInt(e.CommandArgument)
                    'Do the Edit here

                    'Set the IsEditMode flag to TRUE
                    Me.State.IsEditMode = True

                    Me.State.DefaultClaimStatusID = New Guid(CType(Me.Grid.Rows(index).Cells(Me.GRID_COL_DEFAULT_CLAIM_STATUS_ID_IDX).FindControl(Me.GRID_CTRL_NAME_LABLE_DEFAULT_CLAIM_STATUS_ID), Label).Text)
                    Me.State.MyBO = New DefaultClaimStatus(Me.State.DefaultClaimStatusID)

                    Me.PopulateGrid()

                    Me.State.PageIndex = Grid.PageIndex

                    Me.SetControlState()

                ElseIf (e.CommandName = Me.DELETE_COMMAND) Then
                    index = CInt(e.CommandArgument)
                    Me.State.DefaultClaimStatusID = New Guid(CType(Me.Grid.Rows(index).Cells(Me.GRID_COL_DEFAULT_CLAIM_STATUS_ID_IDX).FindControl(Me.GRID_CTRL_NAME_LABLE_DEFAULT_CLAIM_STATUS_ID), Label).Text)
                    Me.DisplayMessage(Message.DELETE_RECORD_PROMPT, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Me.HiddenDeletePromptResponse)
                    Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Delete

                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try

        End Sub

        Public Sub RowCreated(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
            Try
                BaseItemCreated(sender, e)
            Catch ex As Exception
                HandleErrors(ex, Me.MasterPage.MessageController)
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
                Me.PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub NewButton_WRITE_Click(ByVal sender As Object, ByVal e As EventArgs) Handles NewButton_WRITE.Click

            Try
                Me.State.IsEditMode = True
                Me.State.IsGridVisible = True
                Me.State.IsGridAddNew = True
                AddNew()
                Me.SetControlState()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try

        End Sub

        Private Sub AddNew()
            If Me.State.MyBO Is Nothing OrElse Me.State.MyBO.IsNew = False Then
                Me.State.MyBO = New DefaultClaimStatus
                State.MyBO.AddNewRowToSearchDV(Me.State.searchDV, Me.State.MyBO)
            End If
            Me.State.DefaultClaimStatusID = Me.State.MyBO.Id
            State.IsGridAddNew = True
            PopulateGrid()
            'Set focus on the Code TextBox for the EditItemIndex row
            Me.SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.DefaultClaimStatusID, Me.Grid, _
                                               Me.State.PageIndex, Me.State.IsEditMode)
            ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me, Grid)
            SetGridControls(Me.Grid, False)
        End Sub

        Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As EventArgs)

            Try
                PopulateBOFromForm()
                If (Me.State.MyBO.IsDirty) Then
                    Me.State.MyBO.Save()
                    Me.State.IsAfterSave = True
                    Me.State.IsGridAddNew = False
                    Me.MasterPage.MessageController.AddSuccess(Me.MSG_RECORD_SAVED_OK, True)
                    Me.State.searchDV = Nothing
                    Me.State.MyBO = Nothing
                    Me.ReturnFromEditing()
                Else
                    Me.MasterPage.MessageController.AddWarning(Me.MSG_RECORD_NOT_SAVED, True)
                    Me.ReturnFromEditing()
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try

        End Sub

        Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As EventArgs)
            Try
                With State
                    If .IsGridAddNew Then
                        RemoveNewRowFromSearchDV()
                        .IsGridAddNew = False
                        Grid.PageIndex = .PageIndex
                    End If
                    .DefaultClaimStatusID = Guid.Empty
                    Me.State.MyBO = Nothing
                    .IsEditMode = False
                End With
                Grid.EditIndex = NO_ITEM_SELECTED_INDEX
                'Me.State.searchDV = Nothing
                PopulateGrid()
                SetControlState()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub
        Private Sub btnBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.Click
            Try
                Me.Back(ElitaPlusPage.DetailPageCommand.Back)
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub Back(ByVal cmd As ElitaPlusPage.DetailPageCommand)
            If Me.State.searchBy = SearchByType.CompanyGroup Then
                Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Nothing, ReturnType.TargetType.CompanyGroup, False))
            Else
                Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.dealerId, ReturnType.TargetType.Dealer, False))
            End If
        End Sub
#End Region
    End Class

End Namespace