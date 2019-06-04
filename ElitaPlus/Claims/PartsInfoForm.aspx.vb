Option Strict On
Option Explicit On

Partial Class PartsInfoForm
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
            IsEditing = (Me.Grid.EditItemIndex > NO_ROW_SELECTED_INDEX)
        End Get
    End Property

#End Region

#Region "Page Parameters"
    Public Class Parameters
        Public ClaimBO As ClaimBase
        Public Sub New(ByVal claimBO As ClaimBase)
            Me.ClaimBO = claimBO
        End Sub
    End Class
#End Region

#Region "Page State"
    Class BaseState
        Public NavCtrl As INavigationController
    End Class

    Class MyState
        Public PageIndex As Integer = 0
        Public PartsInfoBO As PartsInfo
        Public PartsAdded As Boolean = False
        Public ClaimBO As ClaimBase
        Public CompanyId As Guid
        Public PartsInfoID As Guid
        Public RiskGroupID As Guid
        Public RiskGroup As String
        Public IsAfterSave As Boolean
        Public IsEditMode As Boolean
    End Class

    'Public Sub New()
    '    MyBase.New(New MyState)
    'End Sub

    'Protected Shadows ReadOnly Property State() As MyState
    '    Get
    '        Return CType(MyBase.State, MyState)
    '    End Get
    'End Property

    Public Sub New()
        MyBase.New(New BaseState)
    End Sub
    Protected Shadows ReadOnly Property State() As MyState
        Get
            'Return CType(MyBase.State, MyState)
            If Me.NavController.State Is Nothing Then
                Me.NavController.State = New MyState
            End If
            Return CType(Me.NavController.State, MyState)
        End Get
    End Property
#End Region

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents SearchDescriptionLabel As System.Web.UI.WebControls.Label

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
    Public Const URL As String = "PartsInfoForm.aspx"

    Private Const ID_COL As Integer = 2
    Private Const DESCRIPTION_COL As Integer = 3
    Private Const COST_COL As Integer = 4

    Private Const DESCRIPTION_CONTROL_NAME As String = "DescriptionDropDownList"
    Private Const COST_CONTROL_NAME As String = "CostTextBox"
    Private Const ID_CONTROL_NAME As String = "IdLabel"

    Private Const EDIT_COMMAND As String = "EditRecord"
    Private Const DELETE_COMMAND As String = "DeleteRecord"
    Private Const SORT_COMMAND As String = "Sort"
    Private Const YESNO As String = "YESNO"

    Private Const NO_ROW_SELECTED_INDEX As Integer = -1

#End Region

#Region "Button Click Handlers"

    Private Sub NewButton_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NewButton_WRITE.Click

        Try
            Dim desc As DropDownList = New DropDownList
            Me.BindListControlToDataView(desc, PartsInfo.getAvailList(Me.State.RiskGroupID, Me.State.ClaimBO.Id), LookupListNew.COL_DESCRIPTION_NAME, LookupListNew.COL_ID_NAME, False)
            If desc.Items.Count > 0 Then
                Me.State.IsEditMode = True
                AddNewPartsInfo()
            Else
                If Me.State.PartsAdded Then
                    Me.DisplayMessage(Message.MSG_NO_MORE_PARTSDESC_FOUND, "", ElitaPlusPage.MSG_BTN_OK, ElitaPlusPage.MSG_TYPE_ALERT)
                Else
                    Me.DisplayMessage(Message.MSG_NO_PARTSDESC_FOUND, "", ElitaPlusPage.MSG_BTN_OK, ElitaPlusPage.MSG_TYPE_ALERT)
                End If
                Me.ReturnFromEditing()
            End If

        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrController)
        End Try

    End Sub
    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Try
            'Me.ReturnToCallingPage()
            Me.NavController.Navigate(Me, "back")
        Catch exT As System.Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrController)
        End Try
    End Sub

    Private Sub SaveButton_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveButton_WRITE.Click

        Try
            PopulateBOFromForm()
            If (Me.State.PartsInfoBO.IsDirty) Then
                Me.State.PartsInfoBO.Save()
                Me.State.IsAfterSave = True
                Me.DisplayMessage(Message.RECORD_ADDED_OK, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
                Me.ReturnFromEditing()
            Else
                Me.DisplayMessage(Message.MSG_RECORD_NOT_SAVED, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
                Me.ReturnFromEditing()
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrController)
        End Try

    End Sub

    Private Sub CancelButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CancelButton.Click
        Try
            CancelEditing()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrController)
        End Try
    End Sub

    Private Sub CancelEditing()
        Try
            Me.Grid.SelectedIndex = Me.NO_ITEM_SELECTED_INDEX
            ReturnFromEditing()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrController)
        End Try
    End Sub


#End Region

#Region "Private Methods"
    Private Sub Page_PageCall(ByVal CallFromUrl As String, ByVal CallingPar As Object) Handles MyBase.PageCall
        Try
            If Not Me.CallingParameters Is Nothing Then
                Me.State.ClaimBO = CType(Me.CallingParameters, Claim)
                Me.SetStateProperties()
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrController)
        End Try

    End Sub

    Private Sub CheckifComingFromBackEndClaim()
        If Me.NavController Is Nothing Then
            Exit Sub
        End If
        If Me.NavController.CurrentNavState.Name = "PARTS_INFO" Then
            Dim params As Parameters = CType(Me.NavController.ParametersPassed, Parameters)
            If Not params.ClaimBO Is Nothing Then
                Me.State.ClaimBO = params.ClaimBO
                SetStateProperties()
            End If
        End If
    End Sub

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'Put user code to initialize the page here
        ErrController.Clear_Hide()
        Try
            If Not Page.IsPostBack Then
                CheckifComingFromBackEndClaim()
                Me.MenuEnabled = False
                Me.TextboxClaimNumber.Text = Me.State.ClaimBO.ClaimNumber
                Me.TextboxCustomerName.Text = Me.State.ClaimBO.CustomerName
                Me.TextboxRiskGroup.Text = Me.State.RiskGroup
                Me.SetGridItemStyleColor(Grid)
                Me.ShowMissingTranslations(ErrController)
                Me.State.PageIndex = 0
                SetButtonsState()
                PopulateGrid()
            End If
            Me.SetStateProperties()
            BindBoPropertiesToGridHeaders()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrController)
        End Try
    End Sub

    Private Sub PopulateGrid()

        Dim dv As DataView
        Try
            'Refresh the DataView and Call SetPageAndSelectedIndexFromGuid() to go to the Page 
            'where the most recently saved Record exists in the DataView
            dv = GetDV()
            If (Me.State.IsAfterSave) Then
                Me.State.IsAfterSave = False
                Me.SetPageAndSelectedIndexFromGuid(dv, Me.State.PartsInfoID, Me.Grid, Me.State.PageIndex)
            ElseIf (Me.State.IsEditMode) Then
                Me.SetPageAndSelectedIndexFromGuid(dv, Me.State.PartsInfoID, Me.Grid, Me.State.PageIndex, Me.State.IsEditMode)
            Else
                Me.SetPageAndSelectedIndexFromGuid(dv, Guid.Empty, Me.Grid, Me.State.PageIndex)
            End If

            Me.TranslateGridControls(Grid)
            Grid.DataSource = dv
            Me.Grid.DataBind()

            Me.State.PartsAdded = (dv.Count > 0)

            Me.PopulateControlFromBOProperty(Me.TextTotalCost, PartsInfo.getTotalCost(Me.State.ClaimBO.Id).Value)


        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrController)
        End Try

    End Sub

    Private Function GetDV() As DataView

        Dim dv As DataView

        dv = GetGridDataView()
        dv.Sort = Grid.DataMember()

        Return (dv)

    End Function

    Private Function GetGridDataView() As DataView
        With State
            Return (PartsInfo.getSelectedList(Me.State.ClaimBO.Id))
        End With
    End Function

    Private Sub SetStateProperties()
        Me.State.CompanyId = Me.State.ClaimBO.CompanyId
        Dim riskTypeBO As RiskType = New RiskType(Me.State.ClaimBO.RiskTypeId)
        Me.State.RiskGroupID = riskTypeBO.RiskGroupId
        Me.State.RiskGroup = LookupListNew.GetDescriptionFromId(LookupListNew.GetRiskGroupsLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), Me.State.RiskGroupID)
    End Sub

    Private Sub AddNewPartsInfo()

        Dim dv As DataView

        Try
            dv = GetGridDataView()

            Me.State.PartsInfoBO = New PartsInfo
            Me.State.PartsInfoID = Me.State.PartsInfoBO.Id
            Me.State.PartsInfoBO.ClaimId = Me.State.ClaimBO.Id

            dv = Me.State.PartsInfoBO.GetNewDataViewRow(dv, Me.State.PartsInfoID, Me.State.ClaimBO.Id)

            Grid.DataSource = dv

            Me.SetPageAndSelectedIndexFromGuid(dv, Me.State.PartsInfoID, Me.Grid, Me.State.PageIndex, Me.State.IsEditMode)

            Grid.DataBind()

            Me.State.PageIndex = Grid.CurrentPageIndex

            SetGridControls(Me.Grid, False)

            'Set focus on the Description TextBox for the EditItemIndex row
            Me.SetFocusOnEditableFieldInGrid(Me.Grid, Me.DESCRIPTION_COL, Me.DESCRIPTION_CONTROL_NAME, Me.Grid.EditItemIndex, True)

            Me.PopulateFormFromBO()

            Me.TranslateGridControls(Grid)
            Me.SetButtonsState()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrController)
        End Try
    End Sub

    Private Sub PopulateBOFromForm()

        Try
            With Me.State.PartsInfoBO
                .PartsDescriptionId = New Guid(CType(Me.Grid.Items(Me.Grid.EditItemIndex).Cells(Me.DESCRIPTION_COL).FindControl(Me.DESCRIPTION_CONTROL_NAME), DropDownList).SelectedValue)
                Try
                    .Cost = New DecimalType(CType(CType(Me.Grid.Items(Me.Grid.EditItemIndex).Cells(Me.COST_COL).FindControl(Me.COST_CONTROL_NAME), TextBox).Text, Decimal))
                Catch
                    .Cost = Nothing
                End Try
                If Me.State.PartsInfoBO.InStockID.Equals(Guid.Empty) Then
                    Dim YESNOdv As DataView = LookupListNew.DropdownLookupList(YESNO, ElitaPlusIdentity.Current.ActiveUser.LanguageId, True)
                    YESNOdv.RowFilter = DALObjects.LookupListDALNew.COL_NAME_CODE & "='Y'"
                    If Not YESNOdv Is Nothing AndAlso YESNOdv.Count > 0 Then
                        Me.State.PartsInfoBO.InStockID = New Guid(CType(YESNOdv(0)(DALObjects.LookupListDALNew.COL_NAME_ID), Byte()))
                    End If
                End If
            End With
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrController)
        End Try

    End Sub

    Private Sub PopulateFormFromBO()

        Dim gridRowIdx As Integer = Me.Grid.EditItemIndex
        Try
            With Me.State.PartsInfoBO
                If Not .PartsDescriptionId.Equals(Guid.Empty) Then
                    CType(Me.Grid.Items(gridRowIdx).Cells(Me.DESCRIPTION_COL).FindControl(Me.DESCRIPTION_CONTROL_NAME), DropDownList).SelectedValue = .PartsDescriptionId.ToString()
                End If
                If Not .Cost Is Nothing Then
                    Me.PopulateControlFromBOProperty(CType(Me.Grid.Items(gridRowIdx).Cells(Me.COST_COL).FindControl(Me.COST_CONTROL_NAME), TextBox), .Cost.Value)
                End If
                CType(Me.Grid.Items(gridRowIdx).Cells(Me.ID_COL).FindControl(Me.ID_CONTROL_NAME), Label).Text = .Id.ToString
            End With
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrController)
        End Try

    End Sub

    Private Sub ReturnFromEditing()

        Grid.EditItemIndex = NO_ROW_SELECTED_INDEX

        If Me.Grid.PageCount = 0 Then
            'if returning to the "1st time in" screen
            ControlMgr.SetVisibleControl(Me, Grid, False)
        Else
            ControlMgr.SetVisibleControl(Me, Grid, True)
        End If

        Me.State.IsEditMode = False
        Me.PopulateGrid()
        Me.State.PageIndex = Grid.CurrentPageIndex
        SetButtonsState()

    End Sub

    Private Sub SetButtonsState()

        If (Me.State.IsEditMode) Then
            ControlMgr.SetVisibleControl(Me, SaveButton_WRITE, True)
            ControlMgr.SetVisibleControl(Me, CancelButton, True)
            ControlMgr.SetVisibleControl(Me, NewButton_WRITE, False)
            ControlMgr.SetVisibleControl(Me, btnBack, False)
            'Me.MenuEnabled = False
        Else
            ControlMgr.SetVisibleControl(Me, SaveButton_WRITE, False)
            ControlMgr.SetVisibleControl(Me, CancelButton, False)
            ControlMgr.SetVisibleControl(Me, NewButton_WRITE, True)
            ControlMgr.SetVisibleControl(Me, btnBack, True)
            'Me.MenuEnabled = True
        End If

    End Sub

#End Region

#Region "Datagrid Related "

    Private Sub Grid_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles Grid.PageIndexChanged

        Try
            If (Not (Me.State.IsEditMode)) Then
                Me.State.PageIndex = e.NewPageIndex
                Me.Grid.CurrentPageIndex = Me.State.PageIndex
                Me.PopulateGrid()
                Me.Grid.SelectedIndex = Me.NO_ITEM_SELECTED_INDEX
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrController)
        End Try

    End Sub

    Protected Sub ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs)

        Try
            Dim index As Integer = e.Item.ItemIndex

            If (e.CommandName = Me.EDIT_COMMAND) Then
                'Do the Edit here

                'Set the IsEditMode flag to TRUE
                Me.State.IsEditMode = True

                Me.State.PartsInfoID = New Guid(Me.Grid.Items(e.Item.ItemIndex).Cells(Me.ID_COL).Text)

                Me.State.PartsInfoBO = New PartsInfo(Me.State.PartsInfoID)

                Me.PopulateGrid()
                'Me.

                Me.State.PageIndex = Grid.CurrentPageIndex

                'Disable all Edit and Delete icon buttons on the Grid
                SetGridControls(Me.Grid, False)

                'Set focus on the Description dropdown for the EditItemIndex row
                Me.SetFocusOnEditableFieldInGrid(Me.Grid, Me.DESCRIPTION_COL, Me.DESCRIPTION_CONTROL_NAME, index, False)

                Me.PopulateFormFromBO()

                Me.SetButtonsState()

            ElseIf (e.CommandName = Me.DELETE_COMMAND) Then
                'Do the delete here

                'Clear the SelectedItemStyle to remove the highlight from the previously saved row
                Grid.SelectedIndex = Me.NO_ROW_SELECTED_INDEX

                'Save the Id in the Session

                Me.State.PartsInfoID = New Guid(Me.Grid.Items(e.Item.ItemIndex).Cells(Me.ID_COL).Text)

                Me.State.PartsInfoBO = New PartsInfo(Me.State.PartsInfoID)

                Try
                    Me.State.PartsInfoBO.Delete()
                    'Call the Save() method in the Region Business Object here
                    Me.State.PartsInfoBO.Save()
                Catch ex As Exception
                    Me.State.PartsInfoBO.RejectChanges()
                    Throw ex
                End Try

                Me.State.PageIndex = Grid.CurrentPageIndex

                'Set the IsAfterSave flag to TRUE so that the Paging logic gets invoked
                Me.State.IsAfterSave = True

                PopulateGrid()
                Me.State.PageIndex = Grid.CurrentPageIndex

            ElseIf ((e.CommandName = Me.SORT_COMMAND) AndAlso Not (Me.IsEditing)) Then

                Grid.DataMember = e.CommandArgument.ToString
                PopulateGrid()
            End If

        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrController)
        End Try

    End Sub

    Protected Sub ItemBound(ByVal source As Object, ByVal e As DataGridItemEventArgs) Handles Grid.ItemDataBound
        Try
            BaseItemBound(source, e)

            Dim itemType As ListItemType = CType(e.Item.ItemType, ListItemType)
            Dim dvRow As DataRowView = CType(e.Item.DataItem, DataRowView)

            If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then

                Me.PopulateControlFromBOProperty(e.Item.Cells(Me.ID_COL), dvRow(PartsInfo.PartsInfoDV.COL_NAME_PARTS_INFO_ID))
                Me.PopulateControlFromBOProperty(e.Item.Cells(Me.DESCRIPTION_COL), dvRow(PartsInfo.PartsInfoDV.COL_NAME_PARTS_DESCRIPTION))
                Me.PopulateControlFromBOProperty(e.Item.Cells(Me.COST_COL), dvRow(PartsInfo.PartsInfoDV.COL_NAME_COST))

            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrController)
        End Try
    End Sub

    Protected Sub ItemCreated(ByVal sender As Object, ByVal e As DataGridItemEventArgs)
        Try
            BaseItemCreated(sender, e)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrController)
        End Try
    End Sub

    Protected Sub BindBoPropertiesToGridHeaders()
        Me.BindBOPropertyToGridHeader(Me.State.PartsInfoBO, "DescriptionId", Me.Grid.Columns(Me.DESCRIPTION_COL))
        Me.BindBOPropertyToGridHeader(Me.State.PartsInfoBO, "COST", Me.Grid.Columns(Me.COST_COL))
        'Me.BindBOPropertyToGridHeader(Me.State.Region, "RiskTypeEnglish", Me.Grid.Columns(Me.CODE_COL))
        Me.ClearGridHeadersAndLabelsErrSign()
    End Sub

    Private Sub SetFocusOnEditableFieldInGrid(ByVal grid As DataGrid, ByVal cellPosition As Integer, ByVal controlName As String, ByVal itemIndex As Integer, ByVal newRow As Boolean)
        'Set focus on the Description TextBox for the EditItemIndex row
        Dim desc As DropDownList = CType(grid.Items(itemIndex).Cells(cellPosition).FindControl(controlName), DropDownList)
        If newRow Then
            Me.BindListControlToDataView(desc, _
                PartsInfo.getAvailList(Me.State.RiskGroupID, Me.State.ClaimBO.Id), LookupListNew.COL_DESCRIPTION_NAME, LookupListNew.COL_ID_NAME, False)
        Else
            Me.BindListControlToDataView(desc, PartsInfo.getAvailListWithCurrentPart(Me.State.RiskGroupID, Me.State.ClaimBO.Id, Me.State.PartsInfoBO.PartsDescriptionId), LookupListNew.COL_DESCRIPTION_NAME, LookupListNew.COL_ID_NAME, False)
        End If
        SetFocus(desc)
    End Sub

#End Region


End Class
