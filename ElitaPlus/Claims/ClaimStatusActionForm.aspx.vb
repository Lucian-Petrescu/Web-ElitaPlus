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
            IsEditing = (Me.Grid.EditIndex > NO_ROW_SELECTED_INDEX)
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

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
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

#Region "Private Methods"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'Put user code to initialize the page here
        Try
            ErrController.Clear_Hide()
            If Not Page.IsPostBack Then
                Me.SortDirection = Me.State.SortExpression

                ControlMgr.SetVisibleControl(Me, trPageSize, False)
                Me.TranslateGridHeader(Me.Grid)
                Me.TranslateGridControls(Me.Grid)
                Me.SetGridItemStyleColor(Grid)
                Me.State.PageIndex = 0
                SetButtonsState()
                Me.Search()
            End If
            BindBoPropertiesToGridHeaders()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrController)
        End Try
        Me.ShowMissingTranslations(ErrController)
    End Sub

    Private Sub Page_PageCall(ByVal CallFromUrl As String, ByVal CallingPar As Object) Handles MyBase.PageCall
        Try
            If Not Me.CallingParameters Is Nothing Then
                Me.State.searchBy = CType(CType(CallingPar, ArrayList)(0), Integer)
                Me.State.dealerId = CType(CType(CallingPar, ArrayList)(1), Guid)
                Me.State.isNew = CType(CType(CallingPar, ArrayList)(2), String)
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrController)
        End Try

    End Sub

    Private Sub PopulateGrid()

        Dim dv As DataView
        Try
            If (Me.State.searchDV Is Nothing) Then
                Me.State.searchDV = GetDV()
            End If

            Me.State.searchDV.Sort = Me.State.SortExpression
            If (Me.State.IsAfterSave) Then
                Me.State.IsAfterSave = False
                Me.SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.ClaimStatusActionId, Me.Grid, Me.State.PageIndex)
            ElseIf (Me.State.IsEditMode) Then
                Me.SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.ClaimStatusActionId, Me.Grid, Me.State.PageIndex, Me.State.IsEditMode)
            Else
                Me.SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Guid.Empty, Me.Grid, Me.State.PageIndex)
            End If

            Me.Grid.AutoGenerateColumns = False
            Me.Grid.Columns(Me.ACTION_COL).SortExpression = ClaimStatusAction.COL_NAME_ACTION

            Me.SortAndBindGrid()


        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrController)
        End Try

    End Sub

    Private Function GetDV() As DataView

        Dim dv As DataView

        Me.State.searchDV = Assurant.ElitaPlus.BusinessObjectsNew.ClaimStatusAction.LoadList()
        Me.State.searchDV.Sort = Grid.DataMember()

        Return (Me.State.searchDV)

    End Function



    Private Sub AddNewClaimStatusAction()

        Me.State.searchDV = Assurant.ElitaPlus.BusinessObjectsNew.ClaimStatusAction.LoadList()

        Me.State.ClaimStatusAction = New Assurant.ElitaPlus.BusinessObjectsNew.ClaimStatusAction
        Me.State.ClaimStatusActionId = Me.State.ClaimStatusAction.Id

        Me.State.searchDV = Me.State.ClaimStatusAction.GetNewDataViewRow(Me.State.searchDV, Me.State.ClaimStatusActionId)

        Grid.DataSource = Me.State.searchDV
        SetGridControls(Me.Grid, False)
        Me.SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.ClaimStatusActionId, Me.Grid, Me.State.PageIndex, Me.State.IsEditMode)

        Me.Grid.AutoGenerateColumns = False
        Me.Grid.Columns(Me.ACTION_COL).SortExpression = ClaimStatusAction.COL_NAME_ACTION
        Me.SortAndBindGrid()

    End Sub

    Private Sub SortAndBindGrid()
        Dim dv As New DataView
        Me.State.PageIndex = Me.Grid.PageIndex

        If (Me.State.searchDV.Count = 0) Then

            dv = Assurant.ElitaPlus.BusinessObjectsNew.ClaimStatusAction.LoadList()

            Me.State.bnoRow = True
            dv = ClaimStatusAction.getEmptyList(dv)
            Me.Grid.DataSource = dv
            Me.Grid.DataBind()
            Me.Grid.Rows(0).Visible = False


        Else
            Me.State.bnoRow = False
            Me.Grid.Enabled = True
            Me.Grid.DataSource = Me.State.searchDV
            HighLightSortColumn(Grid, Me.SortDirection)
            Me.Grid.DataBind()
        End If

        If Not Grid.BottomPagerRow.Visible Then Grid.BottomPagerRow.Visible = True

        ControlMgr.SetVisibleControl(Me, Grid, Me.State.IsGridVisible)

        ControlMgr.SetVisibleControl(Me, trPageSize, Me.Grid.Visible)

        Session("recCount") = Me.State.searchDV.Count

        If Me.Grid.Visible Then
            If (Me.State.AddingNewRow) Then
                Me.lblRecordCount.Text = (Me.State.searchDV.Count - 1) & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
            Else
                Me.lblRecordCount.Text = Me.State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
            End If
        End If
        ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me, Grid)
    End Sub

    Private Sub PopulateBOFromForm()

        Try
            With Me.State.ClaimStatusAction


                .ActionId = Me.GetSelectedItem(CType(Me.Grid.Rows(Me.Grid.EditIndex).Cells(Me.ACTION_COL).FindControl(Me.ACTION_CONTROL_NAME), DropDownList))
                .CurrentStatusId = Me.GetSelectedItem(CType(Me.Grid.Rows(Me.Grid.EditIndex).Cells(Me.CURRENT_STATUS_COL).FindControl(Me.CURRENT_STATUS_CONTROL_NAME), DropDownList))
                .NextStatusId = Me.GetSelectedItem(CType(Me.Grid.Rows(Me.Grid.EditIndex).Cells(Me.NEXT_STATUS_COL).FindControl(Me.NEXT_STATUS_CONTROL_NAME), DropDownList))
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

        If Me.Grid.PageCount = 0 Then
            'if returning to the "1st time in" screen
            ControlMgr.SetVisibleControl(Me, Grid, False)
        Else
            ControlMgr.SetVisibleControl(Me, Grid, True)
        End If

        Me.State.IsEditMode = False
        Me.PopulateGrid()
        Me.State.PageIndex = Grid.PageIndex
        SetButtonsState()

    End Sub

    Private Sub SetButtonsState()

        If (Me.State.IsEditMode) Then
            ControlMgr.SetVisibleControl(Me, SaveButton_WRITE, True)
            ControlMgr.SetVisibleControl(Me, CancelButton, True)
            ControlMgr.SetVisibleControl(Me, NewButton_WRITE, False)
            Me.MenuEnabled = False
            If (Me.cboPageSize.Visible) Then
                ControlMgr.SetEnableControl(Me, cboPageSize, False)
            End If
        Else
            ControlMgr.SetVisibleControl(Me, SaveButton_WRITE, False)
            ControlMgr.SetVisibleControl(Me, CancelButton, False)
            ControlMgr.SetVisibleControl(Me, NewButton_WRITE, True)
            Me.MenuEnabled = False
            If (Me.cboPageSize.Visible) Then
                ControlMgr.SetEnableControl(Me, cboPageSize, True)
            End If
        End If

    End Sub

    Private Sub Grid_PageSizeChanged(ByVal source As Object, ByVal e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
        Try
            Grid.PageIndex = NewCurrentPageIndex(Grid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
            Me.State.selectedPageSize = CType(cboPageSize.SelectedValue, Integer)
            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrController)
        End Try
    End Sub


#End Region

#Region "Button Click Handlers"

    Private Sub Search()
        Try
            Me.State.IsGridVisible = True
            Me.State.searchDV = Nothing
            PopulateGrid()
            'Me.State.PageIndex = Grid.CurrentPageIndex
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrController)
        End Try

    End Sub

    Private Sub NewButton_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NewButton_WRITE.Click

        Try
            Me.State.IsEditMode = True
            Me.State.IsGridVisible = True
            Me.State.AddingNewRow = True
            AddNewClaimStatusAction()
            Me.SetGridControls(Me.Grid, False)
            SetButtonsState()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrController)
        End Try

    End Sub

    Private Sub SaveButton_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveButton_WRITE.Click

        Try
            PopulateBOFromForm()
            If (Me.State.ClaimStatusAction.IsDirty) Then
                Me.State.ClaimStatusAction.Save()
                Me.State.IsAfterSave = True
                Me.State.AddingNewRow = False
                Me.AddInfoMsg(Me.MSG_RECORD_SAVED_OK)
                Me.State.searchDV = Nothing
                Me.ReturnFromEditing()
            Else
                Me.AddInfoMsg(Me.MSG_RECORD_NOT_SAVED)
                Me.ReturnFromEditing()
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrController)
        End Try

    End Sub

    Private Sub CancelButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CancelButton.Click

        Try
            Me.Grid.SelectedIndex = Me.NO_ITEM_SELECTED_INDEX
            Me.State.Canceling = True
            If (Me.State.AddingNewRow) Then
                Me.State.AddingNewRow = False
                Me.State.searchDV = Nothing
            End If
            ReturnFromEditing()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrController)
        End Try

    End Sub

#End Region
#Region " Datagrid Related "
    Public Property SortDirection() As String
        Get
            Return ViewState("SortDirection").ToString
        End Get
        Set(ByVal value As String)
            ViewState("SortDirection") = value
        End Set
    End Property

    Private Sub Grid_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Grid.PageIndexChanging

        Try
            If (Not (Me.State.IsEditMode)) Then
                Me.State.PageIndex = e.NewPageIndex
                Me.Grid.PageIndex = Me.State.PageIndex
                Me.PopulateGrid()
                Me.Grid.SelectedIndex = Me.NO_ITEM_SELECTED_INDEX
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrController)
        End Try

    End Sub

    Protected Sub ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)

        Try
            Dim index As Integer

            If (e.CommandName = Me.EDIT_COMMAND) Then
                'Do the Edit here
                index = CInt(e.CommandArgument)
                'Set the IsEditMode flag to TRUE
                Me.State.IsEditMode = True

                Me.State.ClaimStatusActionId = New Guid(CType(Me.Grid.Rows(index).Cells(Me.ID_COL).FindControl(Me.ID_CONTROL_NAME), Label).Text)

                Me.State.ClaimStatusAction = New Assurant.ElitaPlus.BusinessObjectsNew.ClaimStatusAction(Me.State.ClaimStatusActionId)

                Me.PopulateGrid()

                Me.State.PageIndex = Grid.PageIndex

                'Disable all Edit and Delete icon buttons on the Grid
                SetGridControls(Me.Grid, False)

                'Set focus on the Description TextBox for the EditItemIndex row
                Me.SetFocusOnEditableFieldInGrid(Me.Grid, Me.ACTION_COL, Me.ACTION_CONTROL_NAME, index)

                ' Me.PopulateFormFromBO()

                Me.SetButtonsState()

            ElseIf (e.CommandName = Me.DELETE_COMMAND) Then
                'Do the delete here
                index = CInt(e.CommandArgument)
                'Clear the SelectedItemStyle to remove the highlight from the previously saved row
                Grid.SelectedIndex = Me.NO_ROW_SELECTED_INDEX

                'Save the Id in the Session

                Me.State.ClaimStatusActionId = New Guid(CType(Me.Grid.Rows(index).Cells(Me.ID_COL).FindControl(Me.ID_CONTROL_NAME), Label).Text)

                Me.State.ClaimStatusAction = New Assurant.ElitaPlus.BusinessObjectsNew.ClaimStatusAction(Me.State.ClaimStatusActionId)
                Try
                    Me.State.ClaimStatusAction.Delete()
                    'Call the Save() method in the ClaimStatusAction Business Object here
                    Me.State.ClaimStatusAction.Save()
                Catch ex As Exception
                    Me.State.ClaimStatusAction.RejectChanges()
                    Throw ex
                End Try

                Me.State.PageIndex = Grid.PageIndex

                'Set the IsAfterSave flag to TRUE so that the Paging logic gets invoked
                Me.State.IsAfterSave = True
                Me.State.searchDV = Nothing
                PopulateGrid()
                Me.State.PageIndex = Grid.PageIndex

            ElseIf ((e.CommandName = Me.SORT_COMMAND) AndAlso Not (Me.IsEditing)) Then


            End If

        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrController)
        End Try

    End Sub

    Protected Sub ItemBound(ByVal source As Object, ByVal e As GridViewRowEventArgs) Handles Grid.RowDataBound

        Try
            BaseItemBound(source, e)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrController)
        End Try
    End Sub
    'The Binding Logic is here
    Private Sub Grid_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowDataBound
        Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
        Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)

        If Not dvRow Is Nothing And Not Me.State.bnoRow Then

            If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then
                CType(e.Row.Cells(Me.ID_COL).FindControl(Me.ID_CONTROL_NAME), Label).Text = GetGuidStringFromByteArray(CType(dvRow("claim_status_action_id"), Byte()))


                If (Me.State.IsEditMode = True _
                        AndAlso Me.State.ClaimStatusActionId.ToString.Equals(GetGuidStringFromByteArray(CType(dvRow("claim_status_action_id"), Byte())))) Then

                    'Me.BindListControlToDataView(CType(e.Row.Cells(Me.ACTION_COL).FindControl(Me.ACTION_CONTROL_NAME), DropDownList), LookupListNew.GetClaimStatsActionLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId))

                    Dim actionDropDown As New DropDownList
                    actionDropDown = CType(e.Row.Cells(Me.ACTION_COL).FindControl(Me.ACTION_CONTROL_NAME), DropDownList)

                    Dim actionList As ListItem() = CommonConfigManager.Current.ListManager.GetList("ACTION", Thread.CurrentPrincipal.GetLanguageCode())
                    actionDropDown.Populate(actionList, New PopulateOptions() With
                                          {
                                            .AddBlankItem = True
                                           })

                    'Me.SetSelectedItem(CType(e.Row.Cells(Me.ACTION_COL).FindControl(Me.ACTION_CONTROL_NAME), DropDownList), Me.State.ClaimStatusAction.ActionId)
                    Me.SetSelectedItem(actionDropDown, Me.State.ClaimStatusAction.ActionId)
                    'Me.BindListControlToDataView(CType(e.Row.Cells(Me.CURRENT_STATUS_COL).FindControl(Me.CURRENT_STATUS_CONTROL_NAME), DropDownList), LookupListNew.GetExtendedStatusByGroupLookupList(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id, ElitaPlusIdentity.Current.ActiveUser.LanguageId))


                    Dim listcontext As ListContext = New ListContext()
                    listcontext.CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id

                    Dim currentStatusDropDown As New DropDownList
                    currentStatusDropDown = CType(e.Row.Cells(Me.CURRENT_STATUS_COL).FindControl(Me.CURRENT_STATUS_CONTROL_NAME), DropDownList)

                    Dim currentStatusList As ListItem() = CommonConfigManager.Current.ListManager.GetList("ExtendedStatusByCompanyGroup", Thread.CurrentPrincipal.GetLanguageCode(), listcontext)
                    currentStatusDropDown.Populate(currentStatusList, New PopulateOptions() With
                                                  {
                                                    .AddBlankItem = True
                                                   })

                    'Me.SetSelectedItem(CType(e.Row.Cells(Me.CURRENT_STATUS_COL).FindControl(Me.CURRENT_STATUS_CONTROL_NAME), DropDownList), Me.State.ClaimStatusAction.CurrentStatusId)
                    Me.SetSelectedItem(currentStatusDropDown, Me.State.ClaimStatusAction.CurrentStatusId)

                    'Me.BindListControlToDataView(CType(e.Row.Cells(Me.NEXT_STATUS_COL).FindControl(Me.NEXT_STATUS_CONTROL_NAME), DropDownList), LookupListNew.GetExtendedStatusByGroupLookupList(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id, ElitaPlusIdentity.Current.ActiveUser.LanguageId))
                    Dim nextStatusDropDown As New DropDownList
                    nextStatusDropDown = CType(e.Row.Cells(Me.CURRENT_STATUS_COL).FindControl(Me.NEXT_STATUS_CONTROL_NAME), DropDownList)

                    Dim nextStatusList As ListItem() = CommonConfigManager.Current.ListManager.GetList("ExtendedStatusByCompanyGroup", Thread.CurrentPrincipal.GetLanguageCode(), listcontext)
                    nextStatusDropDown.Populate(nextStatusList, New PopulateOptions() With
                                                  {
                                                    .AddBlankItem = True
                                                   })
                    'Me.SetSelectedItem(CType(e.Row.Cells(Me.NEXT_STATUS_COL).FindControl(Me.NEXT_STATUS_CONTROL_NAME), DropDownList), Me.State.ClaimStatusAction.NextStatusId)
                    Me.SetSelectedItem(nextStatusDropDown, Me.State.ClaimStatusAction.NextStatusId)
                Else
                    CType(e.Row.Cells(Me.ACTION_COL).FindControl("ACTIONLabel"), Label).Text = dvRow("action").ToString
                    CType(e.Row.Cells(Me.CURRENT_STATUS_COL).FindControl("CurrentStatusLabel"), Label).Text = dvRow("Current_Status").ToString
                    CType(e.Row.Cells(Me.NEXT_STATUS_COL).FindControl("NextStatusLabel"), Label).Text = dvRow("Next_Status").ToString
                End If

            End If
        End If
    End Sub

    Protected Sub ItemCreated(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
        BaseItemCreated(sender, e)


    End Sub

    Private Sub Grid_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles Grid.Sorting
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
            Me.State.SortExpression = Me.SortDirection
            Me.State.PageIndex = 0
            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrController)
        End Try
    End Sub

    Protected Sub BindBoPropertiesToGridHeaders()
        Me.BindBOPropertyToGridHeader(Me.State.ClaimStatusAction, "ActionId", Me.Grid.Columns(Me.ACTION_COL))
        Me.BindBOPropertyToGridHeader(Me.State.ClaimStatusAction, "CurrentStatusId", Me.Grid.Columns(Me.CURRENT_STATUS_COL))
        Me.BindBOPropertyToGridHeader(Me.State.ClaimStatusAction, "NextStatusId", Me.Grid.Columns(Me.NEXT_STATUS_COL))

        Me.ClearGridViewHeadersAndLabelsErrSign()
    End Sub

    Private Sub SetFocusOnEditableFieldInGrid(ByVal grid As GridView, ByVal cellPosition As Integer, ByVal controlName As String, ByVal itemIndex As Integer)
        'Set focus on the Description TextBox for the EditItemIndex row
        Dim desc As DropDownList = CType(grid.Rows(itemIndex).Cells(cellPosition).FindControl(controlName), DropDownList)
        SetFocus(desc)
    End Sub

#End Region

    Private Sub btnBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Try
            Me.Back(ElitaPlusPage.DetailPageCommand.Back)
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrController)
        End Try
    End Sub

    Protected Sub Back(ByVal cmd As ElitaPlusPage.DetailPageCommand)
        If Me.State.searchBy = SearchByType.CompanyGroup Then
            Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Nothing, ReturnType.TargetType.CompanyGroup, False))
        Else
            Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.dealerId, ReturnType.TargetType.Dealer, False))
        End If
    End Sub
End Class
