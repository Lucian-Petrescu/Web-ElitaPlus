Option Strict On
Option Explicit On
Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports Assurant.Elita.Web.Forms
Imports System.Threading


Partial Class ClaimStatusActionForm
    Inherits ElitaPlusSearchPage

    Protected WithEvents ErrController As ErrorController

    Private Class PageStatus

        Public Sub New()
            pageIndex = 0
            pageCount = 0
        End Sub

    End Class
#Region "Member Variables"

    Private Shared pageIndex As Integer
    Private Shared pageCount As Integer

#End Region

#Region "Properties"

    Public ReadOnly Property IsEditing() As Boolean
        Get
            IsEditing = (Grid.EditIndex > NO_ROW_SELECTED_INDEX)
        End Get
    End Property

#End Region

#Region "Page State"
    Class MyState
        Public PageIndex As Integer = 0
        Public ClaimStatusAction As BusinessObjectsNew.ClaimStatusAction

        Public ClaimStatusActionId As Guid
        Public IsGridVisible As Boolean
        Public IsAfterSave As Boolean
        Public IsEditMode As Boolean
        Public AddingNewRow As Boolean
        Public Canceling As Boolean
        Public searchDV As DataView = Nothing
        Public SortExpression As String = ClaimStatusAction.COL_NAME_ACTION
        Public selectedPageSize As Integer = DEFAULT_PAGE_SIZE
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

#End Region


#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

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

#Region "Constants"

    Private Const ID_COL As Integer = 2
    Private Const ACTION_COL As Integer = 3
    Private Const CURRENT_STATUS_COL As Integer = 4
    Private Const NEXT_STATUS_COL As Integer = 5

    Private Const MSG_CONFIRM_PROMPT As String = "MSG_CONFIRM_PROMPT"
    Private Const MSG_RECORD_DELETED_OK As String = "MSG_RECORD_DELETED_OK"
    Private Const MSG_RECORD_SAVED_OK As String = "MSG_RECORD_SAVED_OK"
    Private Const MSG_RECORD_NOT_SAVED As String = "MSG_RECORD_NOT_SAVED"

    Private Const ACTION_CONTROL_NAME As String = "cboAction"
    Private Const CURRENT_STATUS_CONTROL_NAME As String = "cboCurrentStatus"
    Private Const NEXT_STATUS_CONTROL_NAME As String = "cboNextStatus"

    Private Const ID_CONTROL_NAME As String = "IdLabel"

    Private Const EDIT_COMMAND As String = "EditRecord"
    Private Const DELETE_COMMAND As String = "DeleteRecord"
    Private Const SORT_COMMAND As String = "Sort"

    'Private Const EDIT As String = "Edit"
    'Private Const DELETE As String = "Delete"

    Private Const NO_ROW_SELECTED_INDEX As Integer = -1
    Public Enum SearchByType
        Dealer
        CompanyGroup
    End Enum
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

#Region "Private Methods"

    Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

        'Put user code to initialize the page here
        Try
            ErrController.Clear_Hide()
            If Not Page.IsPostBack Then
                SortDirection = State.SortExpression

                ControlMgr.SetVisibleControl(Me, trPageSize, False)
                TranslateGridHeader(Grid)
                TranslateGridControls(Grid)
                SetGridItemStyleColor(Grid)
                State.PageIndex = 0
                SetButtonsState()
                Search()
            End If
            BindBoPropertiesToGridHeaders()
        Catch ex As Exception
            HandleErrors(ex, ErrController)
        End Try
        ShowMissingTranslations(ErrController)
    End Sub

    Private Sub Page_PageCall(CallFromUrl As String, CallingPar As Object) Handles MyBase.PageCall
        Try
            If CallingParameters IsNot Nothing Then
                State.searchBy = CType(CType(CallingPar, ArrayList)(0), Integer)
                State.dealerId = CType(CType(CallingPar, ArrayList)(1), Guid)
                State.isNew = CType(CType(CallingPar, ArrayList)(2), String)
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrController)
        End Try

    End Sub

    Private Sub PopulateGrid()

        Dim dv As DataView
        Try
            If (State.searchDV Is Nothing) Then
                State.searchDV = GetDV()
            End If

            State.searchDV.Sort = State.SortExpression
            If (State.IsAfterSave) Then
                State.IsAfterSave = False
                SetPageAndSelectedIndexFromGuid(State.searchDV, State.ClaimStatusActionId, Grid, State.PageIndex)
            ElseIf (State.IsEditMode) Then
                SetPageAndSelectedIndexFromGuid(State.searchDV, State.ClaimStatusActionId, Grid, State.PageIndex, State.IsEditMode)
            Else
                SetPageAndSelectedIndexFromGuid(State.searchDV, Guid.Empty, Grid, State.PageIndex)
            End If

            Grid.AutoGenerateColumns = False
            Grid.Columns(ACTION_COL).SortExpression = ClaimStatusAction.COL_NAME_ACTION

            SortAndBindGrid()


        Catch ex As Exception
            HandleErrors(ex, ErrController)
        End Try

    End Sub

    Private Function GetDV() As DataView

        Dim dv As DataView

        State.searchDV = Assurant.ElitaPlus.BusinessObjectsNew.ClaimStatusAction.LoadList()
        State.searchDV.Sort = Grid.DataMember()

        Return (State.searchDV)

    End Function



    Private Sub AddNewClaimStatusAction()

        State.searchDV = Assurant.ElitaPlus.BusinessObjectsNew.ClaimStatusAction.LoadList()

        State.ClaimStatusAction = New Assurant.ElitaPlus.BusinessObjectsNew.ClaimStatusAction
        State.ClaimStatusActionId = State.ClaimStatusAction.Id

        State.searchDV = State.ClaimStatusAction.GetNewDataViewRow(State.searchDV, State.ClaimStatusActionId)

        Grid.DataSource = State.searchDV
        SetGridControls(Grid, False)
        SetPageAndSelectedIndexFromGuid(State.searchDV, State.ClaimStatusActionId, Grid, State.PageIndex, State.IsEditMode)

        Grid.AutoGenerateColumns = False
        Grid.Columns(ACTION_COL).SortExpression = ClaimStatusAction.COL_NAME_ACTION
        SortAndBindGrid()

    End Sub

    Private Sub SortAndBindGrid()
        Dim dv As New DataView
        State.PageIndex = Grid.PageIndex

        If (State.searchDV.Count = 0) Then

            dv = Assurant.ElitaPlus.BusinessObjectsNew.ClaimStatusAction.LoadList()

            State.bnoRow = True
            dv = ClaimStatusAction.getEmptyList(dv)
            Grid.DataSource = dv
            Grid.DataBind()
            Grid.Rows(0).Visible = False


        Else
            State.bnoRow = False
            Grid.Enabled = True
            Grid.DataSource = State.searchDV
            HighLightSortColumn(Grid, SortDirection)
            Grid.DataBind()
        End If

        If Not Grid.BottomPagerRow.Visible Then Grid.BottomPagerRow.Visible = True

        ControlMgr.SetVisibleControl(Me, Grid, State.IsGridVisible)

        ControlMgr.SetVisibleControl(Me, trPageSize, Grid.Visible)

        Session("recCount") = State.searchDV.Count

        If Grid.Visible Then
            If (State.AddingNewRow) Then
                lblRecordCount.Text = (State.searchDV.Count - 1) & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
            Else
                lblRecordCount.Text = State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
            End If
        End If
        ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me, Grid)
    End Sub

    Private Sub PopulateBOFromForm()

        Try
            With State.ClaimStatusAction


                .ActionId = GetSelectedItem(CType(Grid.Rows(Grid.EditIndex).Cells(ACTION_COL).FindControl(ACTION_CONTROL_NAME), DropDownList))
                .CurrentStatusId = GetSelectedItem(CType(Grid.Rows(Grid.EditIndex).Cells(CURRENT_STATUS_COL).FindControl(CURRENT_STATUS_CONTROL_NAME), DropDownList))
                .NextStatusId = GetSelectedItem(CType(Grid.Rows(Grid.EditIndex).Cells(NEXT_STATUS_COL).FindControl(NEXT_STATUS_CONTROL_NAME), DropDownList))
                .CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
                If .CurrentStatusId.Equals(.NextStatusId) Then
                    Throw New GUIException(Message.MSG_INVALID_MIN_MAX_VALUE, Assurant.ElitaPlus.Common.ErrorCodes.NEXT_STATUS_MUST_BE_DIFF_THAN_CURRENT_STATUS)
                End If
            End With
        Catch ex As Exception
            Throw ex 'Me.HandleErrors(ex, Me.ErrController)
        End Try

    End Sub

    Private Sub PopulateFormFromBO()

        'Dim gridRowIdx As Integer = Me.Grid.EditIndex
        'Try
        '    With Me.State.ClaimStatusAction
        '        If Not .Description Is Nothing Then
        '            CType(Me.Grid.Rows(gridRowIdx).Cells(Me.DESCRIPTION_COL).FindControl(Me.DESCRIPTION_CONTROL_NAME), TextBox).Text = .Description
        '        End If
        '        'If Not .Code Is Nothing Then
        '        '    CType(Me.Grid.Items(gridRowIdx).Cells(Me.ACTION_COL).FindControl(Me.ACTION_CONTROL_NAME), TextBox).Text = .Code
        '        'End If
        '        CType(Me.Grid.Rows(gridRowIdx).Cells(Me.ID_COL).FindControl(Me.ID_CONTROL_NAME), Label).Text = .Id.ToString
        '    End With
        'Catch ex As Exception
        '    Me.HandleErrors(ex, Me.ErrController)
        'End Try

    End Sub

    Private Sub ReturnFromEditing()

        Grid.EditIndex = NO_ROW_SELECTED_INDEX

        If Grid.PageCount = 0 Then
            'if returning to the "1st time in" screen
            ControlMgr.SetVisibleControl(Me, Grid, False)
        Else
            ControlMgr.SetVisibleControl(Me, Grid, True)
        End If

        State.IsEditMode = False
        PopulateGrid()
        State.PageIndex = Grid.PageIndex
        SetButtonsState()

    End Sub

    Private Sub SetButtonsState()

        If (State.IsEditMode) Then
            ControlMgr.SetVisibleControl(Me, SaveButton_WRITE, True)
            ControlMgr.SetVisibleControl(Me, CancelButton, True)
            ControlMgr.SetVisibleControl(Me, NewButton_WRITE, False)
            MenuEnabled = False
            If (cboPageSize.Visible) Then
                ControlMgr.SetEnableControl(Me, cboPageSize, False)
            End If
        Else
            ControlMgr.SetVisibleControl(Me, SaveButton_WRITE, False)
            ControlMgr.SetVisibleControl(Me, CancelButton, False)
            ControlMgr.SetVisibleControl(Me, NewButton_WRITE, True)
            MenuEnabled = False
            If (cboPageSize.Visible) Then
                ControlMgr.SetEnableControl(Me, cboPageSize, True)
            End If
        End If

    End Sub

    Private Sub Grid_PageSizeChanged(source As Object, e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
        Try
            Grid.PageIndex = NewCurrentPageIndex(Grid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
            State.selectedPageSize = CType(cboPageSize.SelectedValue, Integer)
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, ErrController)
        End Try
    End Sub


#End Region

#Region "Button Click Handlers"

    Private Sub Search()
        Try
            State.IsGridVisible = True
            State.searchDV = Nothing
            PopulateGrid()
            'Me.State.PageIndex = Grid.CurrentPageIndex
        Catch ex As Exception
            HandleErrors(ex, ErrController)
        End Try

    End Sub

    Private Sub NewButton_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles NewButton_WRITE.Click

        Try
            State.IsEditMode = True
            State.IsGridVisible = True
            State.AddingNewRow = True
            AddNewClaimStatusAction()
            SetGridControls(Grid, False)
            SetButtonsState()
        Catch ex As Exception
            HandleErrors(ex, ErrController)
        End Try

    End Sub

    Private Sub SaveButton_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles SaveButton_WRITE.Click

        Try
            PopulateBOFromForm()
            If (State.ClaimStatusAction.IsDirty) Then
                State.ClaimStatusAction.Save()
                State.IsAfterSave = True
                State.AddingNewRow = False
                AddInfoMsg(MSG_RECORD_SAVED_OK)
                State.searchDV = Nothing
                ReturnFromEditing()
            Else
                AddInfoMsg(MSG_RECORD_NOT_SAVED)
                ReturnFromEditing()
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrController)
        End Try

    End Sub

    Private Sub CancelButton_Click(sender As System.Object, e As System.EventArgs) Handles CancelButton.Click

        Try
            Grid.SelectedIndex = NO_ITEM_SELECTED_INDEX
            State.Canceling = True
            If (State.AddingNewRow) Then
                State.AddingNewRow = False
                State.searchDV = Nothing
            End If
            ReturnFromEditing()
        Catch ex As Exception
            HandleErrors(ex, ErrController)
        End Try

    End Sub

#End Region
#Region " Datagrid Related "
    Public Property SortDirection() As String
        Get
            Return ViewState("SortDirection").ToString
        End Get
        Set(value As String)
            ViewState("SortDirection") = value
        End Set
    End Property

    Private Sub Grid_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Grid.PageIndexChanging

        Try
            If (Not (State.IsEditMode)) Then
                State.PageIndex = e.NewPageIndex
                Grid.PageIndex = State.PageIndex
                PopulateGrid()
                Grid.SelectedIndex = NO_ITEM_SELECTED_INDEX
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrController)
        End Try

    End Sub

    Protected Sub ItemCommand(source As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs)

        Try
            Dim index As Integer

            If (e.CommandName = EDIT_COMMAND) Then
                'Do the Edit here
                index = CInt(e.CommandArgument)
                'Set the IsEditMode flag to TRUE
                State.IsEditMode = True

                State.ClaimStatusActionId = New Guid(CType(Grid.Rows(index).Cells(ID_COL).FindControl(ID_CONTROL_NAME), Label).Text)

                State.ClaimStatusAction = New Assurant.ElitaPlus.BusinessObjectsNew.ClaimStatusAction(State.ClaimStatusActionId)

                PopulateGrid()

                State.PageIndex = Grid.PageIndex

                'Disable all Edit and Delete icon buttons on the Grid
                SetGridControls(Grid, False)

                'Set focus on the Description TextBox for the EditItemIndex row
                SetFocusOnEditableFieldInGrid(Grid, ACTION_COL, ACTION_CONTROL_NAME, index)

                ' Me.PopulateFormFromBO()

                SetButtonsState()

            ElseIf (e.CommandName = DELETE_COMMAND) Then
                'Do the delete here
                index = CInt(e.CommandArgument)
                'Clear the SelectedItemStyle to remove the highlight from the previously saved row
                Grid.SelectedIndex = NO_ROW_SELECTED_INDEX

                'Save the Id in the Session

                State.ClaimStatusActionId = New Guid(CType(Grid.Rows(index).Cells(ID_COL).FindControl(ID_CONTROL_NAME), Label).Text)

                State.ClaimStatusAction = New Assurant.ElitaPlus.BusinessObjectsNew.ClaimStatusAction(State.ClaimStatusActionId)
                Try
                    State.ClaimStatusAction.Delete()
                    'Call the Save() method in the ClaimStatusAction Business Object here
                    State.ClaimStatusAction.Save()
                Catch ex As Exception
                    State.ClaimStatusAction.RejectChanges()
                    Throw ex
                End Try

                State.PageIndex = Grid.PageIndex

                'Set the IsAfterSave flag to TRUE so that the Paging logic gets invoked
                State.IsAfterSave = True
                State.searchDV = Nothing
                PopulateGrid()
                State.PageIndex = Grid.PageIndex

            ElseIf ((e.CommandName = SORT_COMMAND) AndAlso Not (IsEditing)) Then


            End If

        Catch ex As Exception
            HandleErrors(ex, ErrController)
        End Try

    End Sub

    Protected Sub ItemBound(source As Object, e As GridViewRowEventArgs) Handles Grid.RowDataBound

        Try
            BaseItemBound(source, e)
        Catch ex As Exception
            HandleErrors(ex, ErrController)
        End Try
    End Sub
    'The Binding Logic is here
    Private Sub Grid_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowDataBound
        Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
        Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)

        If dvRow IsNot Nothing And Not State.bnoRow Then

            If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then
                CType(e.Row.Cells(ID_COL).FindControl(ID_CONTROL_NAME), Label).Text = GetGuidStringFromByteArray(CType(dvRow("claim_status_action_id"), Byte()))


                If (State.IsEditMode = True _
                        AndAlso State.ClaimStatusActionId.ToString.Equals(GetGuidStringFromByteArray(CType(dvRow("claim_status_action_id"), Byte())))) Then

                    'Me.BindListControlToDataView(CType(e.Row.Cells(Me.ACTION_COL).FindControl(Me.ACTION_CONTROL_NAME), DropDownList), LookupListNew.GetClaimStatsActionLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId))

                    Dim actionDropDown As New DropDownList
                    actionDropDown = CType(e.Row.Cells(ACTION_COL).FindControl(ACTION_CONTROL_NAME), DropDownList)

                    Dim actionList As ListItem() = CommonConfigManager.Current.ListManager.GetList("ACTION", Thread.CurrentPrincipal.GetLanguageCode())
                    actionDropDown.Populate(actionList, New PopulateOptions() With
                                          {
                                            .AddBlankItem = True
                                           })

                    'Me.SetSelectedItem(CType(e.Row.Cells(Me.ACTION_COL).FindControl(Me.ACTION_CONTROL_NAME), DropDownList), Me.State.ClaimStatusAction.ActionId)
                    SetSelectedItem(actionDropDown, State.ClaimStatusAction.ActionId)
                    'Me.BindListControlToDataView(CType(e.Row.Cells(Me.CURRENT_STATUS_COL).FindControl(Me.CURRENT_STATUS_CONTROL_NAME), DropDownList), LookupListNew.GetExtendedStatusByGroupLookupList(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id, ElitaPlusIdentity.Current.ActiveUser.LanguageId))


                    Dim listcontext As ListContext = New ListContext()
                    listcontext.CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id

                    Dim currentStatusDropDown As New DropDownList
                    currentStatusDropDown = CType(e.Row.Cells(CURRENT_STATUS_COL).FindControl(CURRENT_STATUS_CONTROL_NAME), DropDownList)

                    Dim currentStatusList As ListItem() = CommonConfigManager.Current.ListManager.GetList("ExtendedStatusByCompanyGroup", Thread.CurrentPrincipal.GetLanguageCode(), listcontext)
                    currentStatusDropDown.Populate(currentStatusList, New PopulateOptions() With
                                                  {
                                                    .AddBlankItem = True
                                                   })

                    'Me.SetSelectedItem(CType(e.Row.Cells(Me.CURRENT_STATUS_COL).FindControl(Me.CURRENT_STATUS_CONTROL_NAME), DropDownList), Me.State.ClaimStatusAction.CurrentStatusId)
                    SetSelectedItem(currentStatusDropDown, State.ClaimStatusAction.CurrentStatusId)

                    'Me.BindListControlToDataView(CType(e.Row.Cells(Me.NEXT_STATUS_COL).FindControl(Me.NEXT_STATUS_CONTROL_NAME), DropDownList), LookupListNew.GetExtendedStatusByGroupLookupList(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id, ElitaPlusIdentity.Current.ActiveUser.LanguageId))
                    Dim nextStatusDropDown As New DropDownList
                    nextStatusDropDown = CType(e.Row.Cells(CURRENT_STATUS_COL).FindControl(NEXT_STATUS_CONTROL_NAME), DropDownList)

                    Dim nextStatusList As ListItem() = CommonConfigManager.Current.ListManager.GetList("ExtendedStatusByCompanyGroup", Thread.CurrentPrincipal.GetLanguageCode(), listcontext)
                    nextStatusDropDown.Populate(nextStatusList, New PopulateOptions() With
                                                  {
                                                    .AddBlankItem = True
                                                   })
                    'Me.SetSelectedItem(CType(e.Row.Cells(Me.NEXT_STATUS_COL).FindControl(Me.NEXT_STATUS_CONTROL_NAME), DropDownList), Me.State.ClaimStatusAction.NextStatusId)
                    SetSelectedItem(nextStatusDropDown, State.ClaimStatusAction.NextStatusId)
                Else
                    CType(e.Row.Cells(ACTION_COL).FindControl("ACTIONLabel"), Label).Text = dvRow("action").ToString
                    CType(e.Row.Cells(CURRENT_STATUS_COL).FindControl("CurrentStatusLabel"), Label).Text = dvRow("Current_Status").ToString
                    CType(e.Row.Cells(NEXT_STATUS_COL).FindControl("NextStatusLabel"), Label).Text = dvRow("Next_Status").ToString
                End If

            End If
        End If
    End Sub

    Protected Sub ItemCreated(sender As Object, e As GridViewRowEventArgs)
        BaseItemCreated(sender, e)


    End Sub

    Private Sub Grid_SortCommand(source As Object, e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles Grid.Sorting
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
            HandleErrors(ex, ErrController)
        End Try
    End Sub

    Protected Sub BindBoPropertiesToGridHeaders()
        BindBOPropertyToGridHeader(State.ClaimStatusAction, "ActionId", Grid.Columns(ACTION_COL))
        BindBOPropertyToGridHeader(State.ClaimStatusAction, "CurrentStatusId", Grid.Columns(CURRENT_STATUS_COL))
        BindBOPropertyToGridHeader(State.ClaimStatusAction, "NextStatusId", Grid.Columns(NEXT_STATUS_COL))

        ClearGridViewHeadersAndLabelsErrSign()
    End Sub

    Private Sub SetFocusOnEditableFieldInGrid(grid As GridView, cellPosition As Integer, controlName As String, itemIndex As Integer)
        'Set focus on the Description TextBox for the EditItemIndex row
        Dim desc As DropDownList = CType(grid.Rows(itemIndex).Cells(cellPosition).FindControl(controlName), DropDownList)
        SetFocus(desc)
    End Sub

#End Region

    Private Sub btnBack_Click(sender As Object, e As System.EventArgs) Handles btnBack.Click
        Try
            Back(ElitaPlusPage.DetailPageCommand.Back)
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, ErrController)
        End Try
    End Sub

    Protected Sub Back(cmd As ElitaPlusPage.DetailPageCommand)
        If State.searchBy = SearchByType.CompanyGroup Then
            ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Nothing, ReturnType.TargetType.CompanyGroup, False))
        Else
            ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, State.dealerId, ReturnType.TargetType.Dealer, False))
        End If
    End Sub
End Class
