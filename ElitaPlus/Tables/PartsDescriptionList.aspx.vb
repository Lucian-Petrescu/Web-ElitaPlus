Option Strict On
Option Explicit On
Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports Assurant.Elita.Web.Forms
Imports System.Threading
Partial Class PartsDescriptionList
    Inherits ElitaPlusSearchPage

#Region "Constants"
    Public Const URL As String = "PartsDescriptionList.aspx"
#End Region

    Private Class PageStatus

        Public Sub New()
            pageIndex = 0
            pageCount = 0
        End Sub

    End Class

#Region "Page Return Type"
    Public Class ReturnType
        Public LastOperation As DetailPageCommand
        Public EditingRiskGroupID As Guid
        Public HasDataChanged As Boolean
        Public Sub New(LastOp As DetailPageCommand, curRGId As Guid, hasDataChanged As Boolean)
            LastOperation = LastOp
            EditingRiskGroupID = curRGId
            Me.HasDataChanged = hasDataChanged
        End Sub
    End Class
#End Region

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
        Public PartsDesc As PartsDescription
        Public CompanyGrpId As Guid
        Public PartsDescriptionID As Guid
        Public RiskGroupID As Guid
        Public IsAfterSave As Boolean
        Public IsEditMode As Boolean
        Public IsNewRiskGroup As Boolean
        Public HasDataChanged As Boolean
        Public bnoRow As Boolean = False
        Public SortExpression As String = DALObjects.PartsDescriptionDAL.COL_NAME_DESCRIPTION_ENGLISH
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
    Private Const DESCRIPTION_COL As Integer = 3
    Private Const DESCRIPTION_ENGLISH_COL As Integer = 4
    Private Const CODE_COL As Integer = 5

    Private Const DESCRIPTION_CONTROL_NAME As String = "DescriptionTextBox"
    Private Const DESCRIPTION_ENG_CONTROL_NAME As String = "DescEnglishTextbox"
    Private Const CODE_CONTROL_NAME As String = "CodeTextbox"
    Private Const CODE_LABEL_CONTROL_NAME As String = "CodeLabel"
    Private Const ID_CONTROL_NAME As String = "IdLabel"

    Private Const EDIT_COMMAND As String = "EditRecord"
    Private Const DELETE_COMMAND As String = "DeleteRecord"
    Private Const SORT_COMMAND As String = "Sort"

    Private Const NO_ROW_SELECTED_INDEX As Integer = -1
    Public Const PAGETITLE As String = "PART_DESCRIPTION"
    Public Const PAGETAB As String = "Tables"

#End Region

#Region "Button Click Handlers"

    Private Sub RiskGroupDropdown_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles RiskGroupDropdown.SelectedIndexChanged
        Try
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub

    Private Sub btnBack_Click(sender As System.Object, e As System.EventArgs) Handles btnBack.Click
        ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, State.RiskGroupID, State.HasDataChanged))
    End Sub

    Private Sub NewButton_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles NewButton_WRITE.Click

        Try
            State.IsEditMode = True
            AddNewPartsDescription()
            SetButtonsState()
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try

    End Sub

    Private Sub SaveButton_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles SaveButton_WRITE.Click

        Try
            PopulateBOFromForm()
            If (State.PartsDesc.IsDirty) Then
                State.PartsDesc.Save()
                State.HasDataChanged = True
                State.IsAfterSave = True
                'once saved, disable the dropdown list for risk group.
                ChangeEnabledProperty(RiskGroupDropdown, False)
                DisplayMessage(Message.RECORD_ADDED_OK, "", MSG_BTN_OK, MSG_TYPE_INFO)
                ReturnFromEditing()
            Else
                DisplayMessage(Message.MSG_RECORD_NOT_SAVED, "", MSG_BTN_OK, MSG_TYPE_INFO)
                ReturnFromEditing()
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try

    End Sub

    Private Sub CancelButton_Click(sender As System.Object, e As System.EventArgs) Handles CancelButton.Click

        Try
            Grid.SelectedIndex = NO_ITEM_SELECTED_INDEX
            ReturnFromEditing()
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try

    End Sub

#End Region

#Region "Private Methods"

    Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

        'Put user code to initialize the page here
        Try
            ErrControllerMaster.Clear_Hide()

            If Not Page.IsPostBack Then
                SetFormTitle(PAGETITLE)
                SetFormTab(PAGETAB)
                TranslateGridHeader(Grid)
                SortDirection = State.SortExpression
                SetGridItemStyleColor(Grid)
                'Me.BindListControlToDataView(Me.RiskGroupDropdown, LookupListNew.GetRiskGroupsLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), LookupListNew.COL_DESCRIPTION_NAME, LookupListNew.COL_ID_NAME, False)

                Dim riskGroupsTypeList As ListItem() = CommonConfigManager.Current.ListManager.GetList("RGRP", Thread.CurrentPrincipal.GetLanguageCode())
                RiskGroupDropdown.Populate(riskGroupsTypeList, New PopulateOptions() With
                                          {
                                            .AddBlankItem = False
                                           })

                If State.IsNewRiskGroup Then
                    'we need to delete the ones that are already there...
                    Dim dv As PartsDescription.PartsDescriptionDV = PartsDescription.getAssignedList()
                    If dv.Count > 0 Then
                        For Each row As DataRow In dv.Table.Rows
                            Dim rgItem As System.Web.UI.WebControls.ListItem = RiskGroupDropdown.Items.FindByValue(GetGuidStringFromByteArray(CType(row(PartsDescription.PartsDescriptionDV.COL_NAME_RISK_GROUP_ID), Byte())))
                            RiskGroupDropdown.Items.Remove(rgItem)
                        Next
                    End If
                End If

                If RiskGroupDropdown.Items.Count > 0 Then
                    If State.IsNewRiskGroup Then
                        RiskGroupDropdown.SelectedIndex = 0
                        State.RiskGroupID = New Guid(RiskGroupDropdown.SelectedValue)
                        ChangeEnabledProperty(RiskGroupDropdown, True)
                    Else
                        RiskGroupDropdown.SelectedValue = State.RiskGroupID.ToString
                        ChangeEnabledProperty(RiskGroupDropdown, False)
                    End If
                Else
                    DisplayMessageWithSubmit(Message.MSG_NO_RISKGROUP_FOUND, "", ElitaPlusPage.MSG_BTN_OK, ElitaPlusPage.MSG_TYPE_ALERT)
                    ControlMgr.SetVisibleControl(Me, NewButton_WRITE, False)
                End If
                State.PageIndex = 0
                SetButtonsState()
                PopulateGrid()
            End If
            SetStateProperties()
            BindBoPropertiesToGridHeaders()
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
        ShowMissingTranslations(ErrControllerMaster)
    End Sub

    Private Sub Page_PageCall(CallFromUrl As String, CallingPar As Object) Handles MyBase.PageCall
        Try
            If CallingPar IsNot Nothing Then
                State.RiskGroupID = CType(CallingPar, Guid)
                State.IsNewRiskGroup = False
            Else
                State.RiskGroupID = Guid.Empty
                State.IsNewRiskGroup = True
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try

    End Sub
    Private Sub PopulateGrid()

        Dim dv As DataView
        Try
            If RiskGroupDropdown.Items.Count = 0 Then Return
            'Refresh the DataView and Call SetPageAndSelectedIndexFromGuid() to go to the Page 
            'where the most recently saved Record exists in the DataView
            dv = GetDV()

            Grid.AutoGenerateColumns = False

            'Me.Grid.Columns(Me.DESCRIPTION_ENGLISH_COL).SortExpression = Me.State.SortExpression

            If (State.IsAfterSave) Then
                State.IsAfterSave = False
                SetPageAndSelectedIndexFromGuid(dv, State.PartsDescriptionID, Grid, State.PageIndex)
            ElseIf (State.IsEditMode) Then
                SetPageAndSelectedIndexFromGuid(dv, State.PartsDescriptionID, Grid, State.PageIndex, State.IsEditMode)
            Else
                SetPageAndSelectedIndexFromGuid(dv, Guid.Empty, Grid, State.PageIndex)
            End If

            TranslateGridControls(Grid)
            Grid.DataSource = dv
            Grid.DataBind()
            ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me, Grid)
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try

    End Sub

    Private Function GetDV() As DataView

        Dim dv As DataView

        dv = GetGridDataView()
        'dv.Sort = Grid.DataMember()
        dv.Sort = State.SortExpression

        Return (dv)

    End Function

    Private Function GetGridDataView() As DataView
        If RiskGroupDropdown.Items.Count > 0 Then
            With State
                .RiskGroupID = New Guid(RiskGroupDropdown.SelectedValue)
                Return (PartsDescription.getList(.RiskGroupID, Nothing))
            End With
        End If
    End Function

    Private Sub SetStateProperties()

        'Me.State.PartsDescriptionID = SearchDescriptionTextBox.Text
        If RiskGroupDropdown.Items.Count > 0 Then
            State.RiskGroupID = New Guid(RiskGroupDropdown.SelectedValue)
        Else
            State.RiskGroupID = Nothing
        End If
        State.CompanyGrpId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id

    End Sub

    Private Sub AddNewPartsDescription()

        Dim dv As DataView

        dv = GetGridDataView()

        State.PartsDesc = New PartsDescription
        State.PartsDescriptionID = State.PartsDesc.Id
        State.PartsDesc.RiskGroupId = State.RiskGroupID
        State.PartsDesc.CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id

        dv = State.PartsDesc.GetNewDataViewRow(dv, State.PartsDescriptionID, State.RiskGroupID, State.CompanyGrpId)

        Grid.DataSource = dv

        SetPageAndSelectedIndexFromGuid(dv, State.PartsDescriptionID, Grid, State.PageIndex, State.IsEditMode)

        Grid.DataBind()

        State.PageIndex = Grid.PageIndex

        SetGridControls(Grid, False)

        'Set focus on the Description TextBox for the EditItemIndex row
        SetFocusOnEditableFieldInGrid(Grid, DESCRIPTION_COL, DESCRIPTION_CONTROL_NAME, Grid.EditIndex)

        PopulateFormFromBO()

        TranslateGridControls(Grid)
        SetButtonsState()
        ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me, Grid)
    End Sub

    Private Sub PopulateBOFromForm()

        Try
            With State.PartsDesc
                .Description = CType(Grid.Rows(Grid.EditIndex).Cells(DESCRIPTION_COL).FindControl(DESCRIPTION_CONTROL_NAME), TextBox).Text
                .DescriptionEnglish = CType(Grid.Rows(Grid.EditIndex).Cells(DESCRIPTION_ENGLISH_COL).FindControl(DESCRIPTION_ENG_CONTROL_NAME), TextBox).Text
                .Code = CType(Grid.Rows(Grid.EditIndex).Cells(CODE_COL).FindControl(CODE_CONTROL_NAME), TextBox).Text
            End With
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try

    End Sub

    Private Sub PopulateFormFromBO()

        Dim gridRowIdx As Integer = Grid.EditIndex
        Try
            With State.PartsDesc
                If .Description IsNot Nothing Then
                    CType(Grid.Rows(gridRowIdx).Cells(DESCRIPTION_COL).FindControl(DESCRIPTION_CONTROL_NAME), TextBox).Text = .Description
                End If
                If .DescriptionEnglish IsNot Nothing Then
                    CType(Grid.Rows(gridRowIdx).Cells(DESCRIPTION_ENGLISH_COL).FindControl(DESCRIPTION_ENG_CONTROL_NAME), TextBox).Text = .DescriptionEnglish
                End If
                If .Code IsNot Nothing Then
                    CType(Grid.Rows(gridRowIdx).Cells(CODE_COL).FindControl(CODE_CONTROL_NAME), TextBox).Text = .Code
                End If

                CType(Grid.Rows(gridRowIdx).Cells(ID_COL).FindControl(ID_CONTROL_NAME), Label).Text = .Id.ToString
            End With
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try

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
            ControlMgr.SetVisibleControl(Me, btnBack, False)
            MenuEnabled = False
            ChangeEnabledProperty(RiskGroupDropdown, False)
        Else
            ControlMgr.SetVisibleControl(Me, SaveButton_WRITE, False)
            ControlMgr.SetVisibleControl(Me, CancelButton, False)
            ControlMgr.SetVisibleControl(Me, NewButton_WRITE, True)
            ControlMgr.SetVisibleControl(Me, btnBack, True)
            MenuEnabled = True
            ChangeEnabledProperty(RiskGroupDropdown, State.IsNewRiskGroup)
        End If
        If RiskGroupDropdown.Items.Count = 0 Then ControlMgr.SetVisibleControl(Me, NewButton_WRITE, False)
    End Sub

#End Region

#Region "Datagrid Related "

    Public Property SortDirection() As String
        Get
            Return ViewState("SortDirection").ToString
        End Get
        Set(value As String)
            ViewState("SortDirection") = value
        End Set
    End Property

    Private Sub Grid_SortCommand(source As Object, e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles Grid.Sorting
        Try
            Dim spaceIndex As Integer = SortDirection.LastIndexOf(" ")

            If spaceIndex > 0 AndAlso SortDirection.Substring(0, spaceIndex).Equals(e.SortExpression) Then
                If SortDirection.EndsWith(" ASC") Then
                    SortDirection = e.SortExpression.Replace(" DESC", "").Replace(" ASC", "") + " DESC"
                Else
                    SortDirection = e.SortExpression.Replace(" DESC", "").Replace(" ASC", "") + " ASC"
                End If
            Else
                SortDirection = e.SortExpression.Replace(" DESC", "").Replace(" ASC", "") + " ASC"
            End If
            State.SortExpression = SortDirection
            State.PageIndex = 0
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub

    Private Sub Grid_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Grid.PageIndexChanging

        Try
            If (Not (State.IsEditMode)) Then
                State.PageIndex = e.NewPageIndex
                Grid.PageIndex = State.PageIndex
                PopulateGrid()
                Grid.SelectedIndex = NO_ITEM_SELECTED_INDEX
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try

    End Sub

    Protected Sub ItemCommand(source As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs)

        Try
            If (e.CommandName = EDIT_COMMAND) Then
                Dim index As Integer = CInt(e.CommandArgument)

                'Do the Edit here

                'Set the IsEditMode flag to TRUE
                State.IsEditMode = True

                State.PartsDescriptionID = New Guid(CType(Grid.Rows(index).Cells(ID_COL).FindControl(ID_CONTROL_NAME), Label).Text)

                State.PartsDesc = New PartsDescription(State.PartsDescriptionID)

                PopulateGrid()

                State.PageIndex = Grid.PageIndex
                'Disable all Edit and Delete icon buttons on the Grid
                SetGridControls(Grid, False)

                'Set focus on the Description TextBox for the EditItemIndex row
                SetFocusOnEditableFieldInGrid(Grid, DESCRIPTION_COL, DESCRIPTION_CONTROL_NAME, index)

                PopulateFormFromBO()

                SetButtonsState()

            ElseIf (e.CommandName = DELETE_COMMAND) Then
                Dim index As Integer = CInt(e.CommandArgument)
                'Do the delete here

                'Clear the SelectedItemStyle to remove the highlight from the previously saved row
                Grid.SelectedIndex = NO_ROW_SELECTED_INDEX

                'Save the Id in the Session

                State.PartsDescriptionID = New Guid(CType(Grid.Rows(index).Cells(ID_COL).FindControl(ID_CONTROL_NAME), Label).Text)

                State.PartsDesc = New PartsDescription(State.PartsDescriptionID)

                Try
                    State.PartsDesc.Delete()
                    'Call the Save() method in the Region Business Object here
                    State.PartsDesc.Save()
                Catch ex As Exception
                    State.PartsDesc.RejectChanges()
                    Throw ex
                End Try

                State.PageIndex = Grid.PageIndex

                'Set the IsAfterSave flag to TRUE so that the Paging logic gets invoked
                State.IsAfterSave = True
                State.HasDataChanged = True
                PopulateGrid()
                State.PageIndex = Grid.PageIndex

            ElseIf ((e.CommandName = SORT_COMMAND) AndAlso Not (IsEditing)) Then

                Grid.DataMember = e.CommandArgument.ToString
                PopulateGrid()
            End If

        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try

    End Sub

    Protected Sub ItemBound(source As Object, e As GridViewRowEventArgs) Handles Grid.RowDataBound
        BaseItemBound(source, e)

        Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
        Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)

        If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then

            If e.Row.Cells.Count >= 5 AndAlso e.Row.Cells(CODE_COL).FindControl(CODE_CONTROL_NAME) IsNot Nothing Then
                If State.IsEditMode AndAlso State.PartsDesc.DescriptionEnglish Is Nothing Then
                    ControlMgr.SetEnableControl(Me, CType(e.Row.Cells(CODE_COL).FindControl(CODE_CONTROL_NAME), System.Web.UI.WebControls.WebControl), True)
                Else
                    ControlMgr.SetEnableControl(Me, CType(e.Row.Cells(CODE_COL).FindControl(CODE_CONTROL_NAME), System.Web.UI.WebControls.WebControl), False)
                End If
            End If
        End If

    End Sub

    Protected Sub ItemCreated(sender As Object, e As GridViewRowEventArgs)
        BaseItemCreated(sender, e)
    End Sub

    Protected Sub BindBoPropertiesToGridHeaders()
        BindBOPropertyToGridHeader(State.PartsDesc, "Description", Grid.Columns(DESCRIPTION_COL))
        BindBOPropertyToGridHeader(State.PartsDesc, "DescriptionEnglish", Grid.Columns(DESCRIPTION_ENGLISH_COL))
        BindBOPropertyToGridHeader(State.PartsDesc, "Code", Grid.Columns(CODE_COL))
        'Me.BindBOPropertyToGridHeader(Me.State.Region, "RiskTypeEnglish", Me.Grid.Columns(Me.CODE_COL))
        ClearGridViewHeadersAndLabelsErrSign()
    End Sub

    Private Sub SetFocusOnEditableFieldInGrid(grid As GridView, cellPosition As Integer, controlName As String, itemIndex As Integer)
        'Set focus on the Description TextBox for the EditItemIndex row
        Dim desc As TextBox = CType(grid.Rows(itemIndex).Cells(cellPosition).FindControl(controlName), TextBox)
        SetFocus(desc)
    End Sub

#End Region


End Class
