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
            IsEditing = (Grid.EditItemIndex > NO_ROW_SELECTED_INDEX)
        End Get
    End Property

#End Region

#Region "Page Parameters"
    Public Class Parameters
        Public ClaimBO As ClaimBase
        Public Sub New(claimBO As ClaimBase)
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
            If NavController.State Is Nothing Then
                NavController.State = New MyState
            End If
            Return CType(NavController.State, MyState)
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

    Private Sub Page_Init(sender As System.Object, e As System.EventArgs) Handles MyBase.Init
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

    Private Sub NewButton_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles NewButton_WRITE.Click

        Try
            Dim desc As DropDownList = New DropDownList
            BindListControlToDataView(desc, PartsInfo.getAvailList(State.RiskGroupID, State.ClaimBO.Id), LookupListNew.COL_DESCRIPTION_NAME, LookupListNew.COL_ID_NAME, False)
            If desc.Items.Count > 0 Then
                State.IsEditMode = True
                AddNewPartsInfo()
            Else
                If State.PartsAdded Then
                    DisplayMessage(Message.MSG_NO_MORE_PARTSDESC_FOUND, "", ElitaPlusPage.MSG_BTN_OK, ElitaPlusPage.MSG_TYPE_ALERT)
                Else
                    DisplayMessage(Message.MSG_NO_PARTSDESC_FOUND, "", ElitaPlusPage.MSG_BTN_OK, ElitaPlusPage.MSG_TYPE_ALERT)
                End If
                ReturnFromEditing()
            End If

        Catch ex As Exception
            HandleErrors(ex, ErrController)
        End Try

    End Sub
    Private Sub btnBack_Click(sender As System.Object, e As System.EventArgs) Handles btnBack.Click
        Try
            'Me.ReturnToCallingPage()
            NavController.Navigate(Me, "back")
        Catch exT As System.Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, ErrController)
        End Try
    End Sub

    Private Sub SaveButton_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles SaveButton_WRITE.Click

        Try
            PopulateBOFromForm()
            If (State.PartsInfoBO.IsDirty) Then
                State.PartsInfoBO.Save()
                State.IsAfterSave = True
                DisplayMessage(Message.RECORD_ADDED_OK, "", MSG_BTN_OK, MSG_TYPE_INFO)
                ReturnFromEditing()
            Else
                DisplayMessage(Message.MSG_RECORD_NOT_SAVED, "", MSG_BTN_OK, MSG_TYPE_INFO)
                ReturnFromEditing()
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrController)
        End Try

    End Sub

    Private Sub CancelButton_Click(sender As System.Object, e As System.EventArgs) Handles CancelButton.Click
        Try
            CancelEditing()
        Catch ex As Exception
            HandleErrors(ex, ErrController)
        End Try
    End Sub

    Private Sub CancelEditing()
        Try
            Grid.SelectedIndex = NO_ITEM_SELECTED_INDEX
            ReturnFromEditing()
        Catch ex As Exception
            HandleErrors(ex, ErrController)
        End Try
    End Sub


#End Region

#Region "Private Methods"
    Private Sub Page_PageCall(CallFromUrl As String, CallingPar As Object) Handles MyBase.PageCall
        Try
            If CallingParameters IsNot Nothing Then
                State.ClaimBO = CType(CallingParameters, Claim)
                SetStateProperties()
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrController)
        End Try

    End Sub

    Private Sub CheckifComingFromBackEndClaim()
        If NavController Is Nothing Then
            Exit Sub
        End If
        If NavController.CurrentNavState.Name = "PARTS_INFO" Then
            Dim params As Parameters = CType(NavController.ParametersPassed, Parameters)
            If params.ClaimBO IsNot Nothing Then
                State.ClaimBO = params.ClaimBO
                SetStateProperties()
            End If
        End If
    End Sub

    Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

        'Put user code to initialize the page here
        ErrController.Clear_Hide()
        Try
            If Not Page.IsPostBack Then
                CheckifComingFromBackEndClaim()
                MenuEnabled = False
                TextboxClaimNumber.Text = State.ClaimBO.ClaimNumber
                TextboxCustomerName.Text = State.ClaimBO.CustomerName
                TextboxRiskGroup.Text = State.RiskGroup
                SetGridItemStyleColor(Grid)
                ShowMissingTranslations(ErrController)
                State.PageIndex = 0
                SetButtonsState()
                PopulateGrid()
            End If
            SetStateProperties()
            BindBoPropertiesToGridHeaders()
        Catch ex As Exception
            HandleErrors(ex, ErrController)
        End Try
    End Sub

    Private Sub PopulateGrid()

        Dim dv As DataView
        Try
            'Refresh the DataView and Call SetPageAndSelectedIndexFromGuid() to go to the Page 
            'where the most recently saved Record exists in the DataView
            dv = GetDV()
            If (State.IsAfterSave) Then
                State.IsAfterSave = False
                SetPageAndSelectedIndexFromGuid(dv, State.PartsInfoID, Grid, State.PageIndex)
            ElseIf (State.IsEditMode) Then
                SetPageAndSelectedIndexFromGuid(dv, State.PartsInfoID, Grid, State.PageIndex, State.IsEditMode)
            Else
                SetPageAndSelectedIndexFromGuid(dv, Guid.Empty, Grid, State.PageIndex)
            End If

            TranslateGridControls(Grid)
            Grid.DataSource = dv
            Grid.DataBind()

            State.PartsAdded = (dv.Count > 0)

            PopulateControlFromBOProperty(TextTotalCost, PartsInfo.getTotalCost(State.ClaimBO.Id).Value)


        Catch ex As Exception
            HandleErrors(ex, ErrController)
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
            Return (PartsInfo.getSelectedList(State.ClaimBO.Id))
        End With
    End Function

    Private Sub SetStateProperties()
        State.CompanyId = State.ClaimBO.CompanyId
        Dim riskTypeBO As RiskType = New RiskType(State.ClaimBO.RiskTypeId)
        State.RiskGroupID = riskTypeBO.RiskGroupId
        State.RiskGroup = LookupListNew.GetDescriptionFromId(LookupListNew.GetRiskGroupsLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), State.RiskGroupID)
    End Sub

    Private Sub AddNewPartsInfo()

        Dim dv As DataView

        Try
            dv = GetGridDataView()

            State.PartsInfoBO = New PartsInfo
            State.PartsInfoID = State.PartsInfoBO.Id
            State.PartsInfoBO.ClaimId = State.ClaimBO.Id

            dv = State.PartsInfoBO.GetNewDataViewRow(dv, State.PartsInfoID, State.ClaimBO.Id)

            Grid.DataSource = dv

            SetPageAndSelectedIndexFromGuid(dv, State.PartsInfoID, Grid, State.PageIndex, State.IsEditMode)

            Grid.DataBind()

            State.PageIndex = Grid.CurrentPageIndex

            SetGridControls(Grid, False)

            'Set focus on the Description TextBox for the EditItemIndex row
            SetFocusOnEditableFieldInGrid(Grid, DESCRIPTION_COL, DESCRIPTION_CONTROL_NAME, Grid.EditItemIndex, True)

            PopulateFormFromBO()

            TranslateGridControls(Grid)
            SetButtonsState()
        Catch ex As Exception
            HandleErrors(ex, ErrController)
        End Try
    End Sub

    Private Sub PopulateBOFromForm()

        Try
            With State.PartsInfoBO
                .PartsDescriptionId = New Guid(CType(Grid.Items(Grid.EditItemIndex).Cells(DESCRIPTION_COL).FindControl(DESCRIPTION_CONTROL_NAME), DropDownList).SelectedValue)
                Try
                    .Cost = New DecimalType(CType(CType(Grid.Items(Grid.EditItemIndex).Cells(COST_COL).FindControl(COST_CONTROL_NAME), TextBox).Text, Decimal))
                Catch
                    .Cost = Nothing
                End Try
                If State.PartsInfoBO.InStockID.Equals(Guid.Empty) Then
                    Dim YESNOdv As DataView = LookupListNew.DropdownLookupList(YESNO, ElitaPlusIdentity.Current.ActiveUser.LanguageId, True)
                    YESNOdv.RowFilter = DALObjects.LookupListDALNew.COL_NAME_CODE & "='Y'"
                    If YESNOdv IsNot Nothing AndAlso YESNOdv.Count > 0 Then
                        State.PartsInfoBO.InStockID = New Guid(CType(YESNOdv(0)(DALObjects.LookupListDALNew.COL_NAME_ID), Byte()))
                    End If
                End If
            End With
        Catch ex As Exception
            HandleErrors(ex, ErrController)
        End Try

    End Sub

    Private Sub PopulateFormFromBO()

        Dim gridRowIdx As Integer = Grid.EditItemIndex
        Try
            With State.PartsInfoBO
                If Not .PartsDescriptionId.Equals(Guid.Empty) Then
                    CType(Grid.Items(gridRowIdx).Cells(DESCRIPTION_COL).FindControl(DESCRIPTION_CONTROL_NAME), DropDownList).SelectedValue = .PartsDescriptionId.ToString()
                End If
                If .Cost IsNot Nothing Then
                    PopulateControlFromBOProperty(CType(Grid.Items(gridRowIdx).Cells(COST_COL).FindControl(COST_CONTROL_NAME), TextBox), .Cost.Value)
                End If
                CType(Grid.Items(gridRowIdx).Cells(ID_COL).FindControl(ID_CONTROL_NAME), Label).Text = .Id.ToString
            End With
        Catch ex As Exception
            HandleErrors(ex, ErrController)
        End Try

    End Sub

    Private Sub ReturnFromEditing()

        Grid.EditItemIndex = NO_ROW_SELECTED_INDEX

        If Grid.PageCount = 0 Then
            'if returning to the "1st time in" screen
            ControlMgr.SetVisibleControl(Me, Grid, False)
        Else
            ControlMgr.SetVisibleControl(Me, Grid, True)
        End If

        State.IsEditMode = False
        PopulateGrid()
        State.PageIndex = Grid.CurrentPageIndex
        SetButtonsState()

    End Sub

    Private Sub SetButtonsState()

        If (State.IsEditMode) Then
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

    Private Sub Grid_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles Grid.PageIndexChanged

        Try
            If (Not (State.IsEditMode)) Then
                State.PageIndex = e.NewPageIndex
                Grid.CurrentPageIndex = State.PageIndex
                PopulateGrid()
                Grid.SelectedIndex = NO_ITEM_SELECTED_INDEX
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrController)
        End Try

    End Sub

    Protected Sub ItemCommand(source As Object, e As System.Web.UI.WebControls.DataGridCommandEventArgs)

        Try
            Dim index As Integer = e.Item.ItemIndex

            If (e.CommandName = EDIT_COMMAND) Then
                'Do the Edit here

                'Set the IsEditMode flag to TRUE
                State.IsEditMode = True

                State.PartsInfoID = New Guid(Grid.Items(e.Item.ItemIndex).Cells(ID_COL).Text)

                State.PartsInfoBO = New PartsInfo(State.PartsInfoID)

                PopulateGrid()
                'Me.

                State.PageIndex = Grid.CurrentPageIndex

                'Disable all Edit and Delete icon buttons on the Grid
                SetGridControls(Grid, False)

                'Set focus on the Description dropdown for the EditItemIndex row
                SetFocusOnEditableFieldInGrid(Grid, DESCRIPTION_COL, DESCRIPTION_CONTROL_NAME, index, False)

                PopulateFormFromBO()

                SetButtonsState()

            ElseIf (e.CommandName = DELETE_COMMAND) Then
                'Do the delete here

                'Clear the SelectedItemStyle to remove the highlight from the previously saved row
                Grid.SelectedIndex = NO_ROW_SELECTED_INDEX

                'Save the Id in the Session

                State.PartsInfoID = New Guid(Grid.Items(e.Item.ItemIndex).Cells(ID_COL).Text)

                State.PartsInfoBO = New PartsInfo(State.PartsInfoID)

                Try
                    State.PartsInfoBO.Delete()
                    'Call the Save() method in the Region Business Object here
                    State.PartsInfoBO.Save()
                Catch ex As Exception
                    State.PartsInfoBO.RejectChanges()
                    Throw ex
                End Try

                State.PageIndex = Grid.CurrentPageIndex

                'Set the IsAfterSave flag to TRUE so that the Paging logic gets invoked
                State.IsAfterSave = True

                PopulateGrid()
                State.PageIndex = Grid.CurrentPageIndex

            ElseIf ((e.CommandName = SORT_COMMAND) AndAlso Not (IsEditing)) Then

                Grid.DataMember = e.CommandArgument.ToString
                PopulateGrid()
            End If

        Catch ex As Exception
            HandleErrors(ex, ErrController)
        End Try

    End Sub

    Protected Sub ItemBound(source As Object, e As DataGridItemEventArgs) Handles Grid.ItemDataBound
        Try
            BaseItemBound(source, e)

            Dim itemType As ListItemType = CType(e.Item.ItemType, ListItemType)
            Dim dvRow As DataRowView = CType(e.Item.DataItem, DataRowView)

            If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then

                PopulateControlFromBOProperty(e.Item.Cells(ID_COL), dvRow(PartsInfo.PartsInfoDV.COL_NAME_PARTS_INFO_ID))
                PopulateControlFromBOProperty(e.Item.Cells(DESCRIPTION_COL), dvRow(PartsInfo.PartsInfoDV.COL_NAME_PARTS_DESCRIPTION))
                PopulateControlFromBOProperty(e.Item.Cells(COST_COL), dvRow(PartsInfo.PartsInfoDV.COL_NAME_COST))

            End If
        Catch ex As Exception
            HandleErrors(ex, ErrController)
        End Try
    End Sub

    Protected Sub ItemCreated(sender As Object, e As DataGridItemEventArgs)
        Try
            BaseItemCreated(sender, e)
        Catch ex As Exception
            HandleErrors(ex, ErrController)
        End Try
    End Sub

    Protected Sub BindBoPropertiesToGridHeaders()
        BindBOPropertyToGridHeader(State.PartsInfoBO, "DescriptionId", Grid.Columns(DESCRIPTION_COL))
        BindBOPropertyToGridHeader(State.PartsInfoBO, "COST", Grid.Columns(COST_COL))
        'Me.BindBOPropertyToGridHeader(Me.State.Region, "RiskTypeEnglish", Me.Grid.Columns(Me.CODE_COL))
        ClearGridHeadersAndLabelsErrSign()
    End Sub

    Private Sub SetFocusOnEditableFieldInGrid(grid As DataGrid, cellPosition As Integer, controlName As String, itemIndex As Integer, newRow As Boolean)
        'Set focus on the Description TextBox for the EditItemIndex row
        Dim desc As DropDownList = CType(grid.Items(itemIndex).Cells(cellPosition).FindControl(controlName), DropDownList)
        If newRow Then
            BindListControlToDataView(desc, _
                PartsInfo.getAvailList(State.RiskGroupID, State.ClaimBO.Id), LookupListNew.COL_DESCRIPTION_NAME, LookupListNew.COL_ID_NAME, False)
        Else
            BindListControlToDataView(desc, PartsInfo.getAvailListWithCurrentPart(State.RiskGroupID, State.ClaimBO.Id, State.PartsInfoBO.PartsDescriptionId), LookupListNew.COL_DESCRIPTION_NAME, LookupListNew.COL_ID_NAME, False)
        End If
        SetFocus(desc)
    End Sub

#End Region


End Class
