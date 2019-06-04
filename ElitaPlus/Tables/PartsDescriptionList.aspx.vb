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
        Public Sub New(ByVal LastOp As DetailPageCommand, ByVal curRGId As Guid, ByVal hasDataChanged As Boolean)
            Me.LastOperation = LastOp
            Me.EditingRiskGroupID = curRGId
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
            IsEditing = (Me.Grid.EditIndex > NO_ROW_SELECTED_INDEX)
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

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
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

    Private Sub RiskGroupDropdown_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RiskGroupDropdown.SelectedIndexChanged
        Try
            PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub

    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.RiskGroupID, Me.State.HasDataChanged))
    End Sub

    Private Sub NewButton_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NewButton_WRITE.Click

        Try
            Me.State.IsEditMode = True
            AddNewPartsDescription()
            SetButtonsState()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try

    End Sub

    Private Sub SaveButton_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveButton_WRITE.Click

        Try
            PopulateBOFromForm()
            If (Me.State.PartsDesc.IsDirty) Then
                Me.State.PartsDesc.Save()
                Me.State.HasDataChanged = True
                Me.State.IsAfterSave = True
                'once saved, disable the dropdown list for risk group.
                Me.ChangeEnabledProperty(RiskGroupDropdown, False)
                Me.DisplayMessage(Message.RECORD_ADDED_OK, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
                Me.ReturnFromEditing()
            Else
                Me.DisplayMessage(Message.MSG_RECORD_NOT_SAVED, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
                Me.ReturnFromEditing()
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try

    End Sub

    Private Sub CancelButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CancelButton.Click

        Try
            Me.Grid.SelectedIndex = Me.NO_ITEM_SELECTED_INDEX
            ReturnFromEditing()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try

    End Sub

#End Region

#Region "Private Methods"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'Put user code to initialize the page here
        Try
            Me.ErrControllerMaster.Clear_Hide()

            If Not Page.IsPostBack Then
                Me.SetFormTitle(PAGETITLE)
                Me.SetFormTab(PAGETAB)
                TranslateGridHeader(Grid)
                Me.SortDirection = Me.State.SortExpression
                Me.SetGridItemStyleColor(Grid)
                'Me.BindListControlToDataView(Me.RiskGroupDropdown, LookupListNew.GetRiskGroupsLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), LookupListNew.COL_DESCRIPTION_NAME, LookupListNew.COL_ID_NAME, False)

                Dim riskGroupsTypeList As ListItem() = CommonConfigManager.Current.ListManager.GetList("RGRP", Thread.CurrentPrincipal.GetLanguageCode())
                RiskGroupDropdown.Populate(riskGroupsTypeList, New PopulateOptions() With
                                          {
                                            .AddBlankItem = False
                                           })

                If Me.State.IsNewRiskGroup Then
                    'we need to delete the ones that are already there...
                    Dim dv As PartsDescription.PartsDescriptionDV = PartsDescription.getAssignedList()
                    If dv.Count > 0 Then
                        For Each row As DataRow In dv.Table.Rows
                            Dim rgItem As System.Web.UI.WebControls.ListItem = RiskGroupDropdown.Items.FindByValue(GetGuidStringFromByteArray(CType(row(PartsDescription.PartsDescriptionDV.COL_NAME_RISK_GROUP_ID), Byte())))
                            RiskGroupDropdown.Items.Remove(rgItem)
                        Next
                    End If
                End If

                If Me.RiskGroupDropdown.Items.Count > 0 Then
                    If Me.State.IsNewRiskGroup Then
                        RiskGroupDropdown.SelectedIndex = 0
                        Me.State.RiskGroupID = New Guid(RiskGroupDropdown.SelectedValue)
                        Me.ChangeEnabledProperty(RiskGroupDropdown, True)
                    Else
                        RiskGroupDropdown.SelectedValue = Me.State.RiskGroupID.ToString
                        Me.ChangeEnabledProperty(RiskGroupDropdown, False)
                    End If
                Else
                    Me.DisplayMessageWithSubmit(Message.MSG_NO_RISKGROUP_FOUND, "", ElitaPlusPage.MSG_BTN_OK, ElitaPlusPage.MSG_TYPE_ALERT)
                    ControlMgr.SetVisibleControl(Me, Me.NewButton_WRITE, False)
                End If
                Me.State.PageIndex = 0
                SetButtonsState()
                PopulateGrid()
            End If
            Me.SetStateProperties()
            BindBoPropertiesToGridHeaders()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
        Me.ShowMissingTranslations(ErrControllerMaster)
    End Sub

    Private Sub Page_PageCall(ByVal CallFromUrl As String, ByVal CallingPar As Object) Handles MyBase.PageCall
        Try
            If Not CallingPar Is Nothing Then
                Me.State.RiskGroupID = CType(CallingPar, Guid)
                Me.State.IsNewRiskGroup = False
            Else
                Me.State.RiskGroupID = Guid.Empty
                Me.State.IsNewRiskGroup = True
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try

    End Sub
    Private Sub PopulateGrid()

        Dim dv As DataView
        Try
            If Me.RiskGroupDropdown.Items.Count = 0 Then Return
            'Refresh the DataView and Call SetPageAndSelectedIndexFromGuid() to go to the Page 
            'where the most recently saved Record exists in the DataView
            dv = GetDV()

            Me.Grid.AutoGenerateColumns = False

            'Me.Grid.Columns(Me.DESCRIPTION_ENGLISH_COL).SortExpression = Me.State.SortExpression

            If (Me.State.IsAfterSave) Then
                Me.State.IsAfterSave = False
                Me.SetPageAndSelectedIndexFromGuid(dv, Me.State.PartsDescriptionID, Me.Grid, Me.State.PageIndex)
            ElseIf (Me.State.IsEditMode) Then
                Me.SetPageAndSelectedIndexFromGuid(dv, Me.State.PartsDescriptionID, Me.Grid, Me.State.PageIndex, Me.State.IsEditMode)
            Else
                Me.SetPageAndSelectedIndexFromGuid(dv, Guid.Empty, Me.Grid, Me.State.PageIndex)
            End If

            Me.TranslateGridControls(Grid)
            Grid.DataSource = dv
            Me.Grid.DataBind()
            ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me, Grid)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try

    End Sub

    Private Function GetDV() As DataView

        Dim dv As DataView

        dv = GetGridDataView()
        'dv.Sort = Grid.DataMember()
        dv.Sort = Me.State.SortExpression

        Return (dv)

    End Function

    Private Function GetGridDataView() As DataView
        If Me.RiskGroupDropdown.Items.Count > 0 Then
            With State
                .RiskGroupID = New Guid(RiskGroupDropdown.SelectedValue)
                Return (PartsDescription.getList(.RiskGroupID, Nothing))
            End With
        End If
    End Function

    Private Sub SetStateProperties()

        'Me.State.PartsDescriptionID = SearchDescriptionTextBox.Text
        If Me.RiskGroupDropdown.Items.Count > 0 Then
            Me.State.RiskGroupID = New Guid(RiskGroupDropdown.SelectedValue)
        Else
            Me.State.RiskGroupID = Nothing
        End If
        Me.State.CompanyGrpId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id

    End Sub

    Private Sub AddNewPartsDescription()

        Dim dv As DataView

        dv = GetGridDataView()

        Me.State.PartsDesc = New PartsDescription
        Me.State.PartsDescriptionID = Me.State.PartsDesc.Id
        Me.State.PartsDesc.RiskGroupId = Me.State.RiskGroupID
        Me.State.PartsDesc.CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id

        dv = Me.State.PartsDesc.GetNewDataViewRow(dv, Me.State.PartsDescriptionID, Me.State.RiskGroupID, Me.State.CompanyGrpId)

        Grid.DataSource = dv

        Me.SetPageAndSelectedIndexFromGuid(dv, Me.State.PartsDescriptionID, Me.Grid, Me.State.PageIndex, Me.State.IsEditMode)

        Grid.DataBind()

        Me.State.PageIndex = Grid.PageIndex

        SetGridControls(Me.Grid, False)

        'Set focus on the Description TextBox for the EditItemIndex row
        Me.SetFocusOnEditableFieldInGrid(Me.Grid, Me.DESCRIPTION_COL, Me.DESCRIPTION_CONTROL_NAME, Me.Grid.EditIndex)

        Me.PopulateFormFromBO()

        Me.TranslateGridControls(Grid)
        Me.SetButtonsState()
        ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me, Grid)
    End Sub

    Private Sub PopulateBOFromForm()

        Try
            With Me.State.PartsDesc
                .Description = CType(Me.Grid.Rows(Me.Grid.EditIndex).Cells(Me.DESCRIPTION_COL).FindControl(Me.DESCRIPTION_CONTROL_NAME), TextBox).Text
                .DescriptionEnglish = CType(Me.Grid.Rows(Me.Grid.EditIndex).Cells(Me.DESCRIPTION_ENGLISH_COL).FindControl(Me.DESCRIPTION_ENG_CONTROL_NAME), TextBox).Text
                .Code = CType(Me.Grid.Rows(Me.Grid.EditIndex).Cells(Me.CODE_COL).FindControl(Me.CODE_CONTROL_NAME), TextBox).Text
            End With
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try

    End Sub

    Private Sub PopulateFormFromBO()

        Dim gridRowIdx As Integer = Me.Grid.EditIndex
        Try
            With Me.State.PartsDesc
                If Not .Description Is Nothing Then
                    CType(Me.Grid.Rows(gridRowIdx).Cells(Me.DESCRIPTION_COL).FindControl(Me.DESCRIPTION_CONTROL_NAME), TextBox).Text = .Description
                End If
                If Not .DescriptionEnglish Is Nothing Then
                    CType(Me.Grid.Rows(gridRowIdx).Cells(Me.DESCRIPTION_ENGLISH_COL).FindControl(Me.DESCRIPTION_ENG_CONTROL_NAME), TextBox).Text = .DescriptionEnglish
                End If
                If Not .Code Is Nothing Then
                    CType(Me.Grid.Rows(gridRowIdx).Cells(Me.CODE_COL).FindControl(Me.CODE_CONTROL_NAME), TextBox).Text = .Code
                End If

                CType(Me.Grid.Rows(gridRowIdx).Cells(Me.ID_COL).FindControl(Me.ID_CONTROL_NAME), Label).Text = .Id.ToString
            End With
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try

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
            ControlMgr.SetVisibleControl(Me, btnBack, False)
            Me.MenuEnabled = False
            Me.ChangeEnabledProperty(RiskGroupDropdown, False)
        Else
            ControlMgr.SetVisibleControl(Me, SaveButton_WRITE, False)
            ControlMgr.SetVisibleControl(Me, CancelButton, False)
            ControlMgr.SetVisibleControl(Me, NewButton_WRITE, True)
            ControlMgr.SetVisibleControl(Me, btnBack, True)
            Me.MenuEnabled = True
            Me.ChangeEnabledProperty(RiskGroupDropdown, Me.State.IsNewRiskGroup)
        End If
        If Me.RiskGroupDropdown.Items.Count = 0 Then ControlMgr.SetVisibleControl(Me, NewButton_WRITE, False)
    End Sub

#End Region

#Region "Datagrid Related "

    Public Property SortDirection() As String
        Get
            Return ViewState("SortDirection").ToString
        End Get
        Set(ByVal value As String)
            ViewState("SortDirection") = value
        End Set
    End Property

    Private Sub Grid_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles Grid.Sorting
        Try
            Dim spaceIndex As Integer = Me.SortDirection.LastIndexOf(" ")

            If spaceIndex > 0 AndAlso Me.SortDirection.Substring(0, spaceIndex).Equals(e.SortExpression) Then
                If Me.SortDirection.EndsWith(" ASC") Then
                    Me.SortDirection = e.SortExpression.Replace(" DESC", "").Replace(" ASC", "") + " DESC"
                Else
                    Me.SortDirection = e.SortExpression.Replace(" DESC", "").Replace(" ASC", "") + " ASC"
                End If
            Else
                Me.SortDirection = e.SortExpression.Replace(" DESC", "").Replace(" ASC", "") + " ASC"
            End If
            Me.State.SortExpression = Me.SortDirection
            Me.State.PageIndex = 0
            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub

    Private Sub Grid_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Grid.PageIndexChanging

        Try
            If (Not (Me.State.IsEditMode)) Then
                Me.State.PageIndex = e.NewPageIndex
                Me.Grid.PageIndex = Me.State.PageIndex
                Me.PopulateGrid()
                Me.Grid.SelectedIndex = Me.NO_ITEM_SELECTED_INDEX
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try

    End Sub

    Protected Sub ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)

        Try
            If (e.CommandName = Me.EDIT_COMMAND) Then
                Dim index As Integer = CInt(e.CommandArgument)

                'Do the Edit here

                'Set the IsEditMode flag to TRUE
                Me.State.IsEditMode = True

                Me.State.PartsDescriptionID = New Guid(CType(Me.Grid.Rows(index).Cells(Me.ID_COL).FindControl(Me.ID_CONTROL_NAME), Label).Text)

                Me.State.PartsDesc = New PartsDescription(Me.State.PartsDescriptionID)

                Me.PopulateGrid()

                Me.State.PageIndex = Grid.PageIndex
                'Disable all Edit and Delete icon buttons on the Grid
                SetGridControls(Me.Grid, False)

                'Set focus on the Description TextBox for the EditItemIndex row
                Me.SetFocusOnEditableFieldInGrid(Me.Grid, Me.DESCRIPTION_COL, Me.DESCRIPTION_CONTROL_NAME, index)

                Me.PopulateFormFromBO()

                Me.SetButtonsState()

            ElseIf (e.CommandName = Me.DELETE_COMMAND) Then
                Dim index As Integer = CInt(e.CommandArgument)
                'Do the delete here

                'Clear the SelectedItemStyle to remove the highlight from the previously saved row
                Grid.SelectedIndex = Me.NO_ROW_SELECTED_INDEX

                'Save the Id in the Session

                Me.State.PartsDescriptionID = New Guid(CType(Me.Grid.Rows(index).Cells(Me.ID_COL).FindControl(Me.ID_CONTROL_NAME), Label).Text)

                Me.State.PartsDesc = New PartsDescription(Me.State.PartsDescriptionID)

                Try
                    Me.State.PartsDesc.Delete()
                    'Call the Save() method in the Region Business Object here
                    Me.State.PartsDesc.Save()
                Catch ex As Exception
                    Me.State.PartsDesc.RejectChanges()
                    Throw ex
                End Try

                Me.State.PageIndex = Grid.PageIndex

                'Set the IsAfterSave flag to TRUE so that the Paging logic gets invoked
                Me.State.IsAfterSave = True
                Me.State.HasDataChanged = True
                PopulateGrid()
                Me.State.PageIndex = Grid.PageIndex

            ElseIf ((e.CommandName = Me.SORT_COMMAND) AndAlso Not (Me.IsEditing)) Then

                Grid.DataMember = e.CommandArgument.ToString
                PopulateGrid()
            End If

        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try

    End Sub

    Protected Sub ItemBound(ByVal source As Object, ByVal e As GridViewRowEventArgs) Handles Grid.RowDataBound
        BaseItemBound(source, e)

        Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
        Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)

        If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then

            If e.Row.Cells.Count >= 5 AndAlso Not e.Row.Cells(Me.CODE_COL).FindControl(Me.CODE_CONTROL_NAME) Is Nothing Then
                If Me.State.IsEditMode AndAlso Me.State.PartsDesc.DescriptionEnglish Is Nothing Then
                    ControlMgr.SetEnableControl(Me, CType(e.Row.Cells(Me.CODE_COL).FindControl(Me.CODE_CONTROL_NAME), System.Web.UI.WebControls.WebControl), True)
                Else
                    ControlMgr.SetEnableControl(Me, CType(e.Row.Cells(Me.CODE_COL).FindControl(Me.CODE_CONTROL_NAME), System.Web.UI.WebControls.WebControl), False)
                End If
            End If
        End If

    End Sub

    Protected Sub ItemCreated(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
        BaseItemCreated(sender, e)
    End Sub

    Protected Sub BindBoPropertiesToGridHeaders()
        Me.BindBOPropertyToGridHeader(Me.State.PartsDesc, "Description", Me.Grid.Columns(Me.DESCRIPTION_COL))
        Me.BindBOPropertyToGridHeader(Me.State.PartsDesc, "DescriptionEnglish", Me.Grid.Columns(Me.DESCRIPTION_ENGLISH_COL))
        Me.BindBOPropertyToGridHeader(Me.State.PartsDesc, "Code", Me.Grid.Columns(Me.CODE_COL))
        'Me.BindBOPropertyToGridHeader(Me.State.Region, "RiskTypeEnglish", Me.Grid.Columns(Me.CODE_COL))
        Me.ClearGridViewHeadersAndLabelsErrSign()
    End Sub

    Private Sub SetFocusOnEditableFieldInGrid(ByVal grid As GridView, ByVal cellPosition As Integer, ByVal controlName As String, ByVal itemIndex As Integer)
        'Set focus on the Description TextBox for the EditItemIndex row
        Dim desc As TextBox = CType(grid.Rows(itemIndex).Cells(cellPosition).FindControl(controlName), TextBox)
        SetFocus(desc)
    End Sub

#End Region


End Class
