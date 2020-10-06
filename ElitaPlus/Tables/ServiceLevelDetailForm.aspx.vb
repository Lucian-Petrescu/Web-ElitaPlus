Imports System.Globalization
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports System.Threading
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms
Partial Class ServiceLevelDetailForm
    Inherits ElitaPlusSearchPage
#Region " Web Form Designer Generated Code "

    Protected WithEvents ErrorCtrl As ErrorController
    'Protected WithEvents Dealer As System.Web.UI.WebControls.Label

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
    Public Const GRID_COL_EDIT_IDX As Integer = 0
    Private Const GRID_COL_DELETE_IDX As Integer = 1
    Public Const GRID_COL_SERVICE_LEVEL_CODE_IDX As Integer = 2
    Public Const GRID_COL_SERVICE_LEVEL_DESCRIPTION_IDX As Integer = 3
    Public Const GRID_COL_RISK_TYPE_IDX As Integer = 4
    Public Const GRID_COL_COST_TYPE_IDX As Integer = 5
    Public Const GRID_COL_SERVICE_LEVEL_COST_IDX As Integer = 6
    Public Const GRID_COL_EFFECTIVE_DATE_IDX As Integer = 7
    Public Const GRID_COL_EXPIRATION_DATE_IDX As Integer = 8
    Public Const GRID_COL_SERVICE_LEVEL_DETAIL_ID_IDX As Integer = 9

    Public Const GRID_TOTAL_COLUMNS As Integer = 10
    Private Const NOTHING_SELECTED As String = "00000000-0000-0000-0000-000000000000"
    Public Const URL As String = "ServiceLevelDetailForm.aspx"
    Private Const ServiceLevelDetailForm As String = "ServiceLevelDetailForm.aspx"


    Private Const MSG_CONFIRM_PROMPT As String = "MSG_CONFIRM_PROMPT"
    Private Const MSG_RECORD_DELETED_OK As String = "MSG_RECORD_DELETED_OK"
    Private Const MSG_RECORD_SAVED_OK As String = "MSG_RECORD_SAVED_OK"
    Private Const MSG_RECORD_NOT_SAVED As String = "MSG_RECORD_NOT_SAVED"

#End Region

#Region "Page Return Type"
    Public Class ReturnType
        Public LastOperation As DetailPageCommand
        Public EditingBo As ServiceLevelGroup
        Public HasDataChanged As Boolean
        Public Sub New(LastOp As DetailPageCommand, curEditingBo As ServiceLevelGroup, hasDataChanged As Boolean)
            LastOperation = LastOp
            EditingBo = curEditingBo
            Me.HasDataChanged = hasDataChanged
        End Sub
    End Class
#End Region

#Region "Page State"
    Private IsReturningFromChild As Boolean = False

    ' This class keeps the current state for the search page.
    Class MyState
        Public PageIndex As Integer = 0
        Public DescriptionMask As String
        Public CodeMask As String
        Public DealerId As Guid = Guid.Empty
        Public ServiceLevelDetailId As Guid = Guid.Empty
        Public IsGridVisible As Boolean = True
        Public searchDV As ServiceLevelDetail.ServiceLevelDetailSearchDV = Nothing
        Public selectedPageSize As Integer = DEFAULT_PAGE_SIZE
        Public SortExpression As String = ServiceLevelDetail.ServiceLevelDetailSearchDV.COL_SERVICE_LEVEL_CODE
        Public HasDataChanged As Boolean
        Public bnoRow As Boolean = False
        Public IsEditMode As Boolean
        Public myBO As ServiceLevelGroup
        Public myChildBO As ServiceLevelDetail
        Public tempBO As ServiceLevelDetail
        Public IsGridAddNew As Boolean = False
        Public Canceling As Boolean
        Public AddingNewRow As Boolean
        Public IsAfterSave As Boolean
        Public ActionInProgress As DetailPageCommand = DetailPageCommand.Nothing_
        Public DateMask As String = String.Empty

        Public GridFormEditIndex As Integer = NO_ITEM_SELECTED_INDEX
        Public IsFormValueChanged As Boolean = False

        Sub New()

        End Sub

    End Class

    Public Sub New()
        MyBase.New(New MyState)
    End Sub

    Protected Shadows ReadOnly Property State() As MyState
        Get
            Return CType(MyBase.State, MyState)
        End Get
    End Property

    Private Sub Page_PageCall(CallFromUrl As String, CallingPar As Object) Handles MyBase.PageCall
        Try
            If CallingParameters IsNot Nothing Then
                'Get the id from the parent
                State.myBO = New ServiceLevelGroup(CType(CallingParameters, Guid))
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try

    End Sub

#End Region
#Region "Properties"





    Public ReadOnly Property IsGridFormInEditMode() As Boolean
        Get
            Return Grid.EditIndex > NO_ITEM_SELECTED_INDEX
        End Get
    End Property

#End Region
#Region "Page_Events"

    Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        ErrorCtrl.Clear_Hide()


        Try
            If Not IsPostBack Then

                ControlMgr.SetVisibleControl(Me, trPageSize, False)
                AddCalendar(btnDate, txtDate)
                SortDirection = State.SortExpression
                PopulateHeaderTextbox()
                txtDate.Text = Date.Now.ToString("dd-MMM-yy", LocalizationMgr.CurrentFormatProvider)
                State.DateMask = DateHelper.GetFormattedDate(txtDate.Text, "dd-MMM-yy").ToString("yyyyMMdd")
                SetButtonsState()
                If State.myBO Is Nothing Then
                    State.myBO = New ServiceLevelGroup
                End If
                'If Not Me.State.IsGridVisible Then
                If Not (State.selectedPageSize = DEFAULT_PAGE_SIZE) Then
                    cboPageSize.SelectedValue = CType(State.selectedPageSize, String)
                    Grid.PageSize = State.selectedPageSize
                End If
                TranslateGridHeader(Grid)
                TranslateGridControls(Grid)
                PopulateGrid()
                'End If
                SetGridItemStyleColor(Grid)

            End If
            'Me.BindBoPropertiesToGridHeaders()
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
        ShowMissingTranslations(ErrorCtrl)
    End Sub



#End Region

#Region "Controlling Logic"

    Public Sub PopulateGrid()
        If ((State.searchDV Is Nothing) OrElse (State.HasDataChanged)) Then
            State.searchDV = ServiceLevelDetail.getList(State.myBO.Id, TextboxServiceLevelCode.Text.ToUpper, TextboxServiceLevelDesc.Text.ToUpper, State.DateMask)

        End If
        If Not (State.searchDV Is Nothing) Then

            If SortDirection.ToUpper.Contains("SERVICE_LEVEL_CODE") Then
                SortDirection = SortDirection.ToUpper.Replace("SERVICE_LEVEL_CODE", "CODE")
            ElseIf SortDirection.ToUpper.Contains("SERVICE_LEVEL_DESCRIPTION") Then
                SortDirection = SortDirection.ToUpper.Replace("SERVICE_LEVEL_DESCRIPTION", "DESCRIPTION")
            End If

            State.searchDV.Sort = SortDirection

            Grid.AutoGenerateColumns = False

            If (State.IsAfterSave) Then
                State.IsAfterSave = False
                SetPageAndSelectedIndexFromGuid(State.searchDV, State.ServiceLevelDetailId, Grid, State.PageIndex)
            ElseIf (State.IsEditMode) Then
                SetPageAndSelectedIndexFromGuid(State.searchDV, State.ServiceLevelDetailId, Grid, State.PageIndex, State.IsEditMode)
            Else
                SetPageAndSelectedIndexFromGuid(State.searchDV, Guid.Empty, Grid, State.PageIndex, State.IsEditMode)
            End If

            SortAndBindGrid()

            'Save the current record to a BO if editing detail record with effective date in the past
            If Not State.IsGridAddNew AndAlso State.IsEditMode Then
                PopulateBOFromForm()
                If (State.myChildBO.EffectiveDate IsNot Nothing) AndAlso (CDate(State.myChildBO.EffectiveDate) < Date.Now) Then
                    State.tempBO = New ServiceLevelDetail
                    State.tempBO.CopyFrom(State.myChildBO)
                    State.tempBO.ExpirationDate = CType(Date.Now.AddDays(-1), DateType)
                End If
            End If
        End If
    End Sub





    Private Sub SortAndBindGrid()



        If (State.searchDV.Count = 0) Then
            State.bnoRow = True
            CreateHeaderForEmptyGrid(Grid, SortDirection)
            ControlMgr.SetVisibleControl(Me, Grid, State.IsGridVisible)
            ControlMgr.SetVisibleControl(Me, trPageSize, Grid.Visible)
            Grid.PagerSettings.Visible = True
            If Not Grid.BottomPagerRow.Visible Then Grid.BottomPagerRow.Visible = True
        Else
            State.bnoRow = False
            Grid.Enabled = True
            Grid.DataSource = State.searchDV
            HighLightSortColumn(Grid, SortDirection)
            Grid.DataBind()
            ControlMgr.SetVisibleControl(Me, Grid, State.IsGridVisible)
            ControlMgr.SetVisibleControl(Me, trPageSize, Grid.Visible)
            If Not Grid.BottomPagerRow.Visible Then Grid.BottomPagerRow.Visible = True
        End If

        If Grid.BottomPagerRow IsNot Nothing AndAlso Not Grid.BottomPagerRow.Visible Then Grid.BottomPagerRow.Visible = True


        If State.searchDV.Count > 0 Then
            If Grid.Visible Then
                If (State.AddingNewRow) Then
                    lblRecordCount.Text = (State.searchDV.Count - 1) & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                Else
                    lblRecordCount.Text = State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                End If
            Else
                If Grid.Visible Then
                    lblRecordCount.Text = State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                End If
            End If
        Else
            If Grid.Visible Then
                lblRecordCount.Text = State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
            End If
        End If

        ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me, Grid)
    End Sub

#End Region

#Region " GridView Related "
    Public Property SortDirection() As String
        Get
            Return ViewState("SortDirection").ToString
        End Get
        Set(value As String)
            ViewState("SortDirection") = value
        End Set
    End Property

    'The Binding Logic is here
    Private Sub Grid_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowDataBound
        Try
            Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
            Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
            Dim txt As TextBox




            If dvRow IsNot Nothing And Not State.bnoRow Then
                If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then
                    CType(e.Row.Cells(GRID_COL_SERVICE_LEVEL_DETAIL_ID_IDX).FindControl("ServiceLevelDetailIdLabel"), Label).Text = GetGuidStringFromByteArray(CType(dvRow(ServiceLevelDetail.ServiceLevelDetailSearchDV.COL_SERVICE_LEVEL_DETAIL_ID), Byte()))

                    If (State.IsEditMode = True AndAlso State.ServiceLevelDetailId.ToString.Equals(GetGuidStringFromByteArray(CType(dvRow(ServiceLevelDetail.ServiceLevelDetailSearchDV.COL_SERVICE_LEVEL_DETAIL_ID), Byte())))) Then


                        CType(e.Row.Cells(GRID_COL_SERVICE_LEVEL_CODE_IDX).FindControl("ServceLevelCodeTextBox"), TextBox).Text = dvRow(ServiceLevelDetail.ServiceLevelDetailSearchDV.COL_SERVICE_LEVEL_CODE).ToString
                        CType(e.Row.Cells(GRID_COL_SERVICE_LEVEL_DESCRIPTION_IDX).FindControl("ServceLevelDescTextBox"), TextBox).Text = dvRow(ServiceLevelDetail.ServiceLevelDetailSearchDV.COL_SERVICE_LEVEL_DESCRIPTION).ToString
                        Dim RiskTypeList As DropDownList = CType(e.Row.Cells(GRID_COL_RISK_TYPE_IDX).FindControl("RiskTypeDropdown"), DropDownList)
                        Dim CostTypeList As DropDownList = CType(e.Row.Cells(GRID_COL_COST_TYPE_IDX).FindControl("CostTypeDropdown"), DropDownList)
                        PopulateDropdown(RiskTypeList, CostTypeList)
                        If dvRow(ServiceLevelDetail.ServiceLevelDetailSearchDV.COL_RISK_TYPE_ID) IsNot System.DBNull.Value Then
                            SetSelectedItem(RiskTypeList, New Guid(CType(dvRow(ServiceLevelDetail.ServiceLevelDetailSearchDV.COL_RISK_TYPE_ID), Byte())))
                        Else
                            SetSelectedItem(RiskTypeList, Guid.Empty)
                        End If
                        If dvRow(ServiceLevelDetail.ServiceLevelDetailSearchDV.COL_COST_TYPE_ID) IsNot System.DBNull.Value Then
                            SetSelectedItem(CostTypeList, New Guid(CType(dvRow(ServiceLevelDetail.ServiceLevelDetailSearchDV.COL_COST_TYPE_ID), Byte())))
                        Else
                            SetSelectedItem(CostTypeList, Guid.Empty)
                        End If

                        If dvRow(ServiceLevelDetail.ServiceLevelDetailSearchDV.COL_SERVICE_LEVEL_COST) IsNot System.DBNull.Value Then
                            CType(e.Row.Cells(GRID_COL_SERVICE_LEVEL_COST_IDX).FindControl("ServiceLevelCostTextBox"), TextBox).Text = GetAmountFormattedDoubleString(dvRow(ServiceLevelDetail.ServiceLevelDetailSearchDV.COL_SERVICE_LEVEL_COST).ToString)
                        Else
                            CType(e.Row.Cells(GRID_COL_SERVICE_LEVEL_COST_IDX).FindControl("ServiceLevelCostTextBox"), TextBox).Text = "0.00"
                        End If
                        If dvRow(ServiceLevelDetail.ServiceLevelDetailSearchDV.COL_EFFECTIVE_DATE) IsNot System.DBNull.Value Then
                            CType(e.Row.Cells(GRID_COL_EFFECTIVE_DATE_IDX).FindControl("EffectiveDateTextBox"), TextBox).Text = GetDateFormattedString(CType(dvRow(ServiceLevelDetail.ServiceLevelDetailSearchDV.COL_EFFECTIVE_DATE), Date))
                        Else
                            CType(e.Row.Cells(GRID_COL_EFFECTIVE_DATE_IDX).FindControl("EffectiveDateTextBox"), TextBox).Text = String.Empty
                        End If
                        If dvRow(ServiceLevelDetail.ServiceLevelDetailSearchDV.COL_EXPIRATION_DATE) IsNot System.DBNull.Value Then
                            CType(e.Row.Cells(GRID_COL_EXPIRATION_DATE_IDX).FindControl("ExpirationDateTextBox"), TextBox).Text = GetDateFormattedString(CType(dvRow(ServiceLevelDetail.ServiceLevelDetailSearchDV.COL_EXPIRATION_DATE), Date))
                        Else
                            CType(e.Row.Cells(GRID_COL_EXPIRATION_DATE_IDX).FindControl("ExpirationDateTextBox"), TextBox).Text = String.Empty
                        End If

                        Dim btnEffectiveDate As ImageButton = CType(e.Row.Cells(GRID_COL_EFFECTIVE_DATE_IDX).FindControl("moEffectiveDateImageGrid"), ImageButton)
                        Dim txtEffectiveDate As TextBox = CType(e.Row.Cells(GRID_COL_EFFECTIVE_DATE_IDX).FindControl("EffectiveDateTextBox"), TextBox)
                        AddCalendar(btnEffectiveDate, txtEffectiveDate)

                        Dim btnExpirationDate As ImageButton = CType(e.Row.Cells(GRID_COL_EXPIRATION_DATE_IDX).FindControl("moExpirationDateImageGrid"), ImageButton)
                        Dim txtExpirationDate As TextBox = CType(e.Row.Cells(GRID_COL_EXPIRATION_DATE_IDX).FindControl("ExpirationDateTextBox"), TextBox)
                        AddCalendar(btnExpirationDate, txtExpirationDate)



                    Else
                        CType(e.Row.Cells(GRID_COL_SERVICE_LEVEL_CODE_IDX).FindControl("ServceLevelCodeLabel"), Label).Text = dvRow(ServiceLevelDetail.ServiceLevelDetailSearchDV.COL_SERVICE_LEVEL_CODE).ToString
                        CType(e.Row.Cells(GRID_COL_SERVICE_LEVEL_DESCRIPTION_IDX).FindControl("ServceLevelDescLabel"), Label).Text = dvRow(ServiceLevelDetail.ServiceLevelDetailSearchDV.COL_SERVICE_LEVEL_DESCRIPTION).ToString
                        CType(e.Row.Cells(GRID_COL_RISK_TYPE_IDX).FindControl("RiskTypeLabel"), Label).Text = dvRow(ServiceLevelDetail.ServiceLevelDetailSearchDV.COL_RISK_TYPE).ToString 'LookupListNew.GetDescriptionFromId("RISK_TYPES", New Guid(GetGuidStringFromByteArray(CType(dvRow(ServiceLevelDetail.ServiceLevelDetailSearchDV.COL_RISK_TYPE_ID), Byte()))))
                        CType(e.Row.Cells(GRID_COL_COST_TYPE_IDX).FindControl("CostTypeLabel"), Label).Text = dvRow(ServiceLevelDetail.ServiceLevelDetailSearchDV.COL_COST_TYPE).ToString 'LookupListNew.GetDescriptionFromId("COST_TYPE", New Guid(GetGuidStringFromByteArray(CType(dvRow(ServiceLevelDetail.ServiceLevelDetailSearchDV.COL_COST_TYPE_ID), Byte()))))
                        CType(e.Row.Cells(GRID_COL_SERVICE_LEVEL_COST_IDX).FindControl("ServiceLevelCostLabel"), Label).Text = GetAmountFormattedDoubleString(dvRow(ServiceLevelDetail.ServiceLevelDetailSearchDV.COL_SERVICE_LEVEL_COST).ToString)
                        If dvRow(ServiceLevelDetail.ServiceLevelDetailSearchDV.COL_EFFECTIVE_DATE) IsNot System.DBNull.Value Then
                            CType(e.Row.Cells(GRID_COL_EFFECTIVE_DATE_IDX).FindControl("EffectiveDateLabel"), Label).Text = GetDateFormattedString(CType(dvRow(ServiceLevelDetail.ServiceLevelDetailSearchDV.COL_EFFECTIVE_DATE), Date))
                            If CType(dvRow(ServiceLevelDetail.ServiceLevelDetailSearchDV.COL_EFFECTIVE_DATE), Date) < Date.Now Then
                                CType(e.Row.Cells(GRID_COL_DELETE_IDX).FindControl("DeleteButton_WRITE"), ImageButton).Visible = False
                            End If
                        End If
                        If dvRow(ServiceLevelDetail.ServiceLevelDetailSearchDV.COL_EXPIRATION_DATE) IsNot System.DBNull.Value Then
                            CType(e.Row.Cells(GRID_COL_EXPIRATION_DATE_IDX).FindControl("ExpirationDateLabel"), Label).Text = GetDateFormattedString(CType(dvRow(ServiceLevelDetail.ServiceLevelDetailSearchDV.COL_EXPIRATION_DATE), Date))
                        End If

                    End If
                End If
            End If

        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

    Private Sub Grid_PreRender(sender As Object, e As System.EventArgs) Handles Grid.PreRender
        If (Grid.EditIndex <> -1) Then

            Dim editRow As GridViewRow = Grid.Rows(Grid.EditIndex)
            'get the textbox  
            Dim effDateTxtBox As TextBox = CType(editRow.FindControl("EffectiveDateTextBox"), TextBox)
            If (effDateTxtBox.Text IsNot String.Empty) AndAlso (CType(effDateTxtBox.Text, Date) > Date.Now) Then
                Dim expDateTxtBox As TextBox = CType(editRow.FindControl("ExpirationDateTextBox"), TextBox)
                'get the cell where the textbox is located  
                Dim cellEffDate As TableCell = CType(effDateTxtBox.Parent, TableCell)
                cellEffDate.Text = effDateTxtBox.Text

                Dim cellExpDate As TableCell = CType(expDateTxtBox.Parent, TableCell)
                cellExpDate.Text = expDateTxtBox.Text
            End If
        End If
    End Sub

    Private Sub Grid_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles Grid.RowCommand
        Try
            'ignore other commands
            Dim index As Integer
            If (e.CommandName = "EditRecord") Then

                index = CInt(e.CommandArgument)

                'Do the Edit here

                'Set the IsEditMode flag to TRUE
                State.IsEditMode = True

                State.ServiceLevelDetailId = New Guid(CType(Grid.Rows(index).Cells(GRID_COL_SERVICE_LEVEL_DETAIL_ID_IDX).FindControl("ServiceLevelDetailIdLabel"), Label).Text)
                State.myChildBO = New ServiceLevelDetail(State.ServiceLevelDetailId)

                PopulateGrid()

                State.PageIndex = Grid.PageIndex

                'Disable all Edit and Delete icon buttons on the Grid
                SetGridControls(Grid, False)

                'Set focus on the Description TextBox for the EditItemIndex row
                'Me.SetFocusOnEditableFieldInGrid(Me.Grid, Me.DESCRIPTION_COL_IDX, Me.DESCRIPTION_CONTROL_NAME, index)

                PopulateFormFromBO()

                SetButtonsState()
            ElseIf (e.CommandName = "DeleteAction") Then
                Dim lblCtrl As Label
                Dim row As GridViewRow = CType(CType(e.CommandSource, Control).Parent.Parent, GridViewRow)
                Dim RowInd As Integer = row.RowIndex
                lblCtrl = CType(Grid.Rows(RowInd).Cells(GRID_COL_SERVICE_LEVEL_DETAIL_ID_IDX).FindControl("ServiceLevelDetailIdLabel"), Label)
                State.ServiceLevelDetailId = New Guid(lblCtrl.Text)
                'lblCtrl = Nothing
                'If e.CommandName = "SelectAction" Then
                '    Grid.EditIndex = RowInd
                '    PopulateForms()
                '    'Disable all Edit and Delete icon buttons on the Grid
                '    SetGridControls(Me.Grid, False)
                'ElseIf e.CommandName = "DeleteAction" Then
                Dim intErrCode As Integer, strErrMsg As String
                ServiceLevelDetail.DeleteServiceLevelDetail(State.ServiceLevelDetailId)
                State.searchDV = Nothing
                PopulateGrid()

                ' End If
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub

    Public Sub RowCreated(sender As System.Object, e As System.Web.UI.WebControls.GridViewRowEventArgs)
        BaseItemCreated(sender, e)
    End Sub

    Private Sub Grid_PageSizeChanged(source As Object, e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
        Try
            Grid.PageIndex = NewCurrentPageIndex(Grid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
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
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub


    Private Sub Grid_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Grid.PageIndexChanging
        Try
            State.PageIndex = e.NewPageIndex
            State.DealerId = Guid.Empty
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub


#End Region

#Region " Button Clicks "


    Private Sub moBtnSearch_Click(sender As Object, e As System.EventArgs) Handles moBtnSearch.Click
        Try
            State.PageIndex = 0
            State.DealerId = Guid.Empty
            State.IsGridVisible = True
            State.searchDV = Nothing
            State.HasDataChanged = False

            If txtDate.Text <> "" Then
                State.DateMask = DateHelper.GetDateValue(txtDate.Text).ToString("yyyyMMdd")
            End If

            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub


    Private Sub moBtnClearSearch_Click(sender As Object, e As System.EventArgs) Handles moBtnClearSearch.Click
        ClearSearchCriteria()
    End Sub
    Private Sub ClearSearchCriteria()

        Try

            TextboxServiceLevelCode.Text = String.Empty
            TextboxServiceLevelDesc.Text = String.Empty
            txtDate.Text = String.Empty
            State.DateMask = String.Empty



        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try

    End Sub


    Private Sub btnAdd_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnAdd_WRITE.Click

        Try
            State.IsEditMode = True
            State.IsGridVisible = True
            State.IsGridAddNew = True
            State.HasDataChanged = True
            State.AddingNewRow = True
            AddNew()
            SetButtonsState()
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try

    End Sub

    Private Sub SaveButton_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles SaveButton_WRITE.Click
        Try

            PopulateBOFromForm()

            If State.IsGridAddNew Then
                With State.myChildBO
                    If (.EffectiveDate IsNot Nothing) AndAlso Not (ServiceLevelDetail.IsServiceLevelDetailValid(.ServiceLevelGroupId, .Code, .RiskTypeId, .CostTypeId, CDate(.EffectiveDate))) Then
                        DisplayMessage(Message.MSG_SVC_LVL_DTL_UNIQUE, "", MSG_BTN_OK, MSG_TYPE_ALERT, , True)
                        Exit Sub
                    End If
                End With
            End If

            If (State.myChildBO.IsDirty) Then
                'Insert new record when saving service level detail with effective date in the past
                If Not State.IsGridAddNew AndAlso (State.tempBO IsNot Nothing) Then
                    State.myChildBO.EffectiveDate = CType(Date.Now, DateType)
                    State.myChildBO.Save()
                    State.tempBO.GetNewDataViewRow(State.searchDV, State.tempBO)
                    State.tempBO.Save()
                Else
                    State.myChildBO.Save()
                End If

                State.IsGridAddNew = False
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
            HandleErrors(ex, ErrorCtrl)
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
            HandleErrors(ex, ErrorCtrl)
        End Try

    End Sub

    Private Sub btnBack_Click(sender As System.Object, e As System.EventArgs) Handles btnBack.Click
        Try
            If Grid.EditIndex <> -1 Then
                PopulateBOFromForm()
            End If
            If State.myBO.IsDirty Then
                'Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", Me.MSG_BTN_YES_NO_CANCEL, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
            Else
                ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, State.myBO, State.HasDataChanged))
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
            'Me.DisplayMessage(Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR, "", Me.MSG_BTN_YES_NO_CANCEL, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
            State.ActionInProgress = ElitaPlusPage.DetailPageCommand.BackOnErr
            'Me.State.LastErrMsg = Me.ErrorCtrl.Text
        End Try
    End Sub

#End Region

#Region "Helper Functions"

    Private Sub AddNew()
        State.searchDV = GetGridDataView()

        State.myChildBO = New ServiceLevelDetail
        State.ServiceLevelDetailId = State.myChildBO.Id
        State.myChildBO.ServiceLevelGroupId = State.myBO.Id

        State.searchDV = State.myChildBO.GetNewDataViewRow(State.searchDV, State.myChildBO)

        Grid.DataSource = State.searchDV

        SetPageAndSelectedIndexFromGuid(State.searchDV, State.ServiceLevelDetailId, Grid, State.PageIndex, State.IsEditMode)

        Grid.AutoGenerateColumns = False


        SortAndBindGrid()
        SetGridControls(Grid, False)


        PopulateFormFromBO()
    End Sub



    Private Sub SetButtonsState()

        If (State.IsEditMode) Then
            ControlMgr.SetVisibleControl(Me, SaveButton_WRITE, True)
            ControlMgr.SetVisibleControl(Me, CancelButton, True)
            ControlMgr.SetVisibleControl(Me, btnAdd_WRITE, False)
            ControlMgr.SetEnableControl(Me, moBtnSearch, False)
            ControlMgr.SetEnableControl(Me, moBtnClearSearch, False)
            MenuEnabled = False
            If (cboPageSize.Visible) Then
                ControlMgr.SetEnableControl(Me, cboPageSize, False)
            End If
        Else
            ControlMgr.SetVisibleControl(Me, SaveButton_WRITE, False)
            ControlMgr.SetVisibleControl(Me, CancelButton, False)
            ControlMgr.SetVisibleControl(Me, btnAdd_WRITE, True)
            ControlMgr.SetEnableControl(Me, moBtnSearch, True)
            ControlMgr.SetEnableControl(Me, moBtnClearSearch, True)
            MenuEnabled = True
            If (cboPageSize.Visible) Then
                ControlMgr.SetEnableControl(Me, cboPageSize, True)
            End If
        End If

    End Sub


    Private Function GetGridDataView() As ServiceLevelDetail.ServiceLevelDetailSearchDV

        With State
            Return (ServiceLevelDetail.getList(State.myBO.Id, TextboxServiceLevelCode.Text.ToUpper, TextboxServiceLevelDesc.Text.ToUpper, State.DateMask))
        End With

    End Function


    Private Sub ReturnFromEditing()

        Grid.EditIndex = NO_ITEM_SELECTED_INDEX

        If Grid.PageCount = 0 Then
            'if returning to the "1st time in" screen
            ControlMgr.SetVisibleControl(Me, Grid, False)
        Else
            ControlMgr.SetVisibleControl(Me, Grid, True)
        End If

        SetGridControls(Grid, True)
        State.IsEditMode = False
        PopulateGrid()
        State.PageIndex = Grid.PageIndex
        SetButtonsState()

    End Sub

    Private Sub PopulateDropdown(RiskTypeList As DropDownList, CostTypeList As DropDownList)
        Try
            '  Me.BindListControlToDataView(RiskTypeList, LookupListNew.GetRiskTypeLookupList(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id))
            Dim listcontext As ListContext = New ListContext()
            listcontext.CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
            Dim riskTypeLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("RiskTypeByCompanyGroup", Thread.CurrentPrincipal.GetLanguageCode(), listcontext)
            RiskTypeList.Populate(riskTypeLkl, New PopulateOptions() With
             {
               .AddBlankItem = True
             })
            'Me.BindListControlToDataView(CostTypeList, LookupListNew.GetCostTypeList(Authentication.LangId, True))
            CostTypeList.Populate(CommonConfigManager.Current.ListManager.GetList("CT", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
            {
              .AddBlankItem = True
            })
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try

    End Sub


    Private Sub PopulateBOFromForm()

        Dim editRow As GridViewRow = Grid.Rows(Grid.EditIndex)
        Dim effDateTxtBox As TextBox = CType(editRow.FindControl("EffectiveDateTextBox"), TextBox)
        Dim expDateTxtBox As TextBox = CType(editRow.FindControl("ExpirationDateTextBox"), TextBox)


        Try
            With State.myChildBO

                .Code = CType(Grid.Rows(Grid.EditIndex).Cells(GRID_COL_SERVICE_LEVEL_CODE_IDX).FindControl("ServceLevelCodeTextBox"), TextBox).Text
                .Description = CType(Grid.Rows(Grid.EditIndex).Cells(GRID_COL_SERVICE_LEVEL_DESCRIPTION_IDX).FindControl("ServceLevelDescTextBox"), TextBox).Text
                .RiskTypeId = New Guid(CType(Grid.Rows(Grid.EditIndex).Cells(GRID_COL_RISK_TYPE_IDX).FindControl("RiskTypeDropdown"), DropDownList).SelectedValue)
                .CostTypeId = New Guid(CType(Grid.Rows(Grid.EditIndex).Cells(GRID_COL_COST_TYPE_IDX).FindControl("CostTypeDropdown"), DropDownList).SelectedValue)
                PopulateBOProperty(State.myChildBO, "ServiceLevelCost", CType(Grid.Rows(Grid.EditIndex).Cells(GRID_COL_SERVICE_LEVEL_COST_IDX).FindControl("ServiceLevelCostTextBox"), TextBox))
                If (effDateTxtBox IsNot Nothing) AndAlso (effDateTxtBox.Text IsNot String.Empty) Then
                    PopulateBOProperty(State.myChildBO, "EffectiveDate", effDateTxtBox)
                Else
                    PopulateBOProperty(State.myChildBO, "EffectiveDate", Grid.Rows(Grid.EditIndex).Cells(GRID_COL_EFFECTIVE_DATE_IDX).Text)
                End If
                If (expDateTxtBox IsNot Nothing) AndAlso (expDateTxtBox.Text IsNot String.Empty) Then
                    PopulateBOProperty(State.myChildBO, "ExpirationDate", expDateTxtBox)
                Else
                    PopulateBOProperty(State.myChildBO, "ExpirationDate", Grid.Rows(Grid.EditIndex).Cells(GRID_COL_EXPIRATION_DATE_IDX).Text)
                End If

            End With
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try

    End Sub

    Private Sub PopulateFormFromBO()

        Dim gridRowIdx As Integer = Grid.EditIndex
        Try
            With State.myChildBO


                If (.Code IsNot Nothing) Then
                    CType(Grid.Rows(gridRowIdx).Cells(GRID_COL_SERVICE_LEVEL_CODE_IDX).FindControl("ServceLevelCodeTextBox"), TextBox).Text = .Code
                End If
                If (.Description IsNot Nothing) Then
                    CType(Grid.Rows(gridRowIdx).Cells(GRID_COL_SERVICE_LEVEL_DESCRIPTION_IDX).FindControl("ServceLevelDescTextBox"), TextBox).Text = .Description
                End If

                Dim RiskTypeList As DropDownList = CType(Grid.Rows(gridRowIdx).Cells(GRID_COL_RISK_TYPE_IDX).FindControl("RiskTypeDropdown"), DropDownList)
                Dim CostTypeList As DropDownList = CType(Grid.Rows(gridRowIdx).Cells(GRID_COL_COST_TYPE_IDX).FindControl("CostTypeDropdown"), DropDownList)
                PopulateDropdown(RiskTypeList, CostTypeList)

                SetSelectedItem(RiskTypeList, .RiskTypeId)
                SetSelectedItem(CostTypeList, .CostTypeId)

                If (.ServiceLevelCost IsNot Nothing) Then
                    CType(Grid.Rows(gridRowIdx).Cells(GRID_COL_SERVICE_LEVEL_COST_IDX).FindControl("ServiceLevelCostTextBox"), TextBox).Text = .ServiceLevelCost.ToString
                End If

                If (.EffectiveDate IsNot Nothing) Then
                    CType(Grid.Rows(gridRowIdx).Cells(GRID_COL_EFFECTIVE_DATE_IDX).FindControl("EffectiveDateTextBox"), TextBox).Text = GetDateFormattedString(CType(.EffectiveDate, Date))
                End If

                If (.ExpirationDate IsNot Nothing) Then
                    CType(Grid.Rows(gridRowIdx).Cells(GRID_COL_EXPIRATION_DATE_IDX).FindControl("ExpirationDateTextBox"), TextBox).Text = GetDateFormattedString(CType(.ExpirationDate, Date))
                End If


            End With
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try

    End Sub

    Private Sub SetFocusOnEditableFieldInGrid(grid As GridView, cellPosition As Integer, controlName As String, itemIndex As Integer)
        'Set focus on the Description TextBox for the EditItemIndex row
        Dim desc As TextBox = CType(grid.Rows(itemIndex).Cells(cellPosition).FindControl(controlName), TextBox)
        SetFocus(desc)
    End Sub

    Private Sub PopulateHeaderTextbox()
        Dim oCountry As Country

        oCountry = New Country(State.myBO.CountryId)
        PopulateControlFromBOProperty(moCountryText_NO_TRANSLATE, oCountry.Description)
        PopulateControlFromBOProperty(TextboxShortDesc_WRITE, State.myBO.Code)
        PopulateControlFromBOProperty(TextboxDescription_WRITE, State.myBO.Description)


    End Sub


#End Region

End Class


