Option Strict On
Option Explicit On
Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports System.Threading
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms

Namespace Tables

    Partial Class BillingPlanListForm
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
        Public Const GRID_COL_DEALER_GROUP_CODE_IDX As Integer = 2
        Public Const GRID_COL_DEALER_CODE_IDX As Integer = 3
        Public Const GRID_COL_BILLING_PLAN_CODE_IDX As Integer = 4
        Public Const GRID_COL_BILLING_PLAN_IDX As Integer = 5
        Public Const GRID_COL_BILLING_PLAN_ID_IDX As Integer = 6

        Public Const GRID_TOTAL_COLUMNS As Integer = 7
        Private Const NOTHING_SELECTED As String = "00000000-0000-0000-0000-000000000000"
        Private Const BILLINGPLANLISTFORM As String = "BillingPlanListForm.aspx"
        Private Const LABEL_SELECT_DEALERCODE As String = "DEALER"
        Private Const LABEL_SELECT_DEALERGROUPCODE As String = "DEALER_GROUP"

        Private Const MSG_CONFIRM_PROMPT As String = "MSG_CONFIRM_PROMPT"
        Private Const MSG_RECORD_DELETED_OK As String = "MSG_RECORD_DELETED_OK"
        Private Const MSG_RECORD_SAVED_OK As String = "MSG_RECORD_SAVED_OK"
        Private Const MSG_RECORD_NOT_SAVED As String = "MSG_RECORD_NOT_SAVED"

#End Region

#Region "Page State"
        Private IsReturningFromChild As Boolean = False

        ' This class keeps the current state for the search page.
        Class MyState
            Public PageIndex As Integer = 0
            Public DescriptionMask As String
            Public CodeMask As String
            Public DealerId As Guid = Guid.Empty
            Public BillingPlanId As Guid = Guid.Empty
            Public IsGridVisible As Boolean
            Public searchDV As BillingPlan.BillingPlanSearchDV = Nothing
            Public selectedPageSize As Integer = DEFAULT_PAGE_SIZE
            Public SortExpression As String = BillingPlan.BillingPlanSearchDV.COL_DEALER_GROUP_CODE
            Public HasDataChanged As Boolean
            Public bnoRow As Boolean = False
            Public IsEditMode As Boolean
            Public myBO As BillingPlan
            Public IsGridAddNew As Boolean = False
            Public Canceling As Boolean
            Public AddingNewRow As Boolean
            Public IsAfterSave As Boolean

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

#End Region
#Region "Properties"

        Public ReadOnly Property DealerMultipleDrop() As MultipleColumnDDLabelControl
            Get
                If moDealerMultipleDrop Is Nothing Then
                    moDealerMultipleDrop = CType(FindControl("multipleDropControl"), MultipleColumnDDLabelControl)
                End If
                Return moDealerMultipleDrop
            End Get
        End Property

        Public ReadOnly Property DealerGroupMultipleDrop() As MultipleColumnDDLabelControl
            Get
                If moDealeGrouprMultipleDrop Is Nothing Then
                    moDealeGrouprMultipleDrop = CType(FindControl("multipleDropControl"), MultipleColumnDDLabelControl)
                End If
                Return moDealeGrouprMultipleDrop
            End Get
        End Property

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
                    SortDirection = State.SortExpression
                    PopulateDropdown()
                    SetButtonsState()
                    If State.myBO Is Nothing Then
                        State.myBO = New BillingPlan
                    End If
                    If State.IsGridVisible Then
                        If Not (State.selectedPageSize = DEFAULT_PAGE_SIZE) Then
                            cboPageSize.SelectedValue = CType(State.selectedPageSize, String)
                            Grid.PageSize = State.selectedPageSize
                        End If
                        PopulateGrid()
                    End If
                    SetGridItemStyleColor(Grid)

                    TranslateGridHeader(Grid)
                    TranslateGridControls(Grid)
                    TranslateGridHeader(Grid1)
                End If
                BindBoPropertiesToGridHeaders()
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try
            ShowMissingTranslations(ErrorCtrl)
        End Sub

        'Private Sub Page_PageReturn(ByVal ReturnFromUrl As String, ByVal ReturnPar As Object) Handles MyBase.PageReturn
        '    Try
        '        Me.MenuEnabled = True
        '        Me.IsReturningFromChild = True
        '        Dim retObj As DealerForm.ReturnType = CType(ReturnPar, DealerForm.ReturnType)
        '        Me.State.HasDataChanged = retObj.HasDataChanged
        '        Select Case retObj.LastOperation
        '            Case ElitaPlusPage.DetailPageCommand.Back
        '                If Not retObj Is Nothing Then
        '                    Me.State.DealerId = retObj.EditingBo.Id

        '                    'Me.State.CompanyId = CType(Session(ELPWebConstants.OLDCOMPANYID), Guid)
        '                    'Me.UpdateUserCompany()
        '                    'Session(ELPWebConstants.OLDCOMPANYID) = Nothing
        '                    Me.State.IsGridVisible = True
        '                End If
        '            Case ElitaPlusPage.DetailPageCommand.Delete
        '                Me.AddInfoMsg(Message.DELETE_RECORD_CONFIRMATION)
        '        End Select
        '    Catch ex As Exception
        '        Me.HandleErrors(ex, Me.ErrorCtrl)
        '    End Try
        'End Sub

#End Region

#Region "Controlling Logic"

        Public Sub PopulateGrid()
            If ((State.searchDV Is Nothing) OrElse (State.HasDataChanged)) Then
                State.searchDV = BillingPlan.getList(DealerMultipleDrop.SelectedGuid, DealerGroupMultipleDrop.SelectedGuid, BillingPlanTextBox.Text.ToUpper) ', ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id)

            End If
            If Not (State.searchDV Is Nothing) Then

                State.searchDV.Sort = SortDirection

                Grid.AutoGenerateColumns = False

                If (State.IsAfterSave) Then
                    State.IsAfterSave = False
                    SetPageAndSelectedIndexFromGuid(State.searchDV, State.BillingPlanId, Grid, State.PageIndex)
                ElseIf (State.IsEditMode) Then
                    SetPageAndSelectedIndexFromGuid(State.searchDV, State.BillingPlanId, Grid, State.PageIndex, State.IsEditMode)
                Else
                    SetPageAndSelectedIndexFromGuid(State.searchDV, Guid.Empty, Grid, State.PageIndex, State.IsEditMode)
                End If

                SortAndBindGrid()
            End If
        End Sub

        Private Sub PopulateDropdown()

            DealerGroupMultipleDrop.Caption = TranslationBase.TranslateLabelOrMessage(LABEL_SELECT_DEALERGROUPCODE)
            DealerGroupMultipleDrop.NothingSelected = True

            DealerGroupMultipleDrop.BindData(LookupListNew.GetDealerGroupLookupList(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id))
            DealerGroupMultipleDrop.AutoPostBackDD = True

            DealerMultipleDrop.Caption = TranslationBase.TranslateLabelOrMessage(LABEL_SELECT_DEALERCODE)
            DealerMultipleDrop.NothingSelected = True

            DealerMultipleDrop.BindData(LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies))
            DealerMultipleDrop.AutoPostBackDD = True


        End Sub

        Private Sub OnFromDrop_Changed(fromMultipleDrop As Assurant.ElitaPlus.ElitaPlusWebApp.Common.MultipleColumnDDLabelControl) _
            Handles moDealerMultipleDrop.SelectedDropChanged
            Try
                State.DealerId = DealerMultipleDrop.SelectedGuid()
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

        Private Sub SortAndBindGrid()
            If (State.searchDV.Count = 0) Then

                State.bnoRow = True

                CreateHeaderForEmptyGrid(Grid1, SortDirection)
                'For Each gvRow As GridViewRow In Grid.Rows
                '    gvRow.Visible = False
                '    gvRow.Controls.Clear()
                'Next
                Grid.DataSource = State.searchDV
                Grid.DataBind()
                ControlMgr.SetVisibleControl(Me, Grid1, State.IsGridVisible)
                ControlMgr.SetVisibleControl(Me, Grid, False)
                ControlMgr.SetVisibleControl(Me, trPageSize, Grid.Visible)
                Grid1.PagerSettings.Visible = True
                If Not Grid1.BottomPagerRow.Visible Then Grid1.BottomPagerRow.Visible = True
            Else
                State.bnoRow = False
                Grid.Enabled = True
                Grid.DataSource = State.searchDV
                HighLightSortColumn(Grid, SortDirection)
                Grid.DataBind()
                ControlMgr.SetVisibleControl(Me, Grid, State.IsGridVisible)
                ControlMgr.SetVisibleControl(Me, trPageSize, Grid.Visible)
                ControlMgr.SetVisibleControl(Me, Grid1, False)
                If Not Grid.BottomPagerRow.Visible Then Grid.BottomPagerRow.Visible = True
            End If
            ' If Not Grid.BottomPagerRow.Visible Then Grid.BottomPagerRow.Visible = True

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


                If _
                    dvRow IsNot Nothing AndAlso Not State.bnoRow AndAlso
                    (itemType = ListItemType.Item OrElse itemType = ListItemType.AlternatingItem OrElse
                     itemType = ListItemType.SelectedItem) Then
                    CType(e.Row.Cells(GRID_COL_BILLING_PLAN_ID_IDX).FindControl("BillingPlanIdLabel"), Label).Text =
                        GetGuidStringFromByteArray(CType(dvRow(BillingPlan.BillingPlanSearchDV.COL_BILLING_PLAN_ID),
                                                         Byte()))
                    If _
                        (State.IsEditMode = True AndAlso
                         State.BillingPlanId.ToString.Equals(
                             GetGuidStringFromByteArray(CType(dvRow(BillingPlan.BillingPlanSearchDV.COL_BILLING_PLAN_ID),
                                                              Byte())))) Then

                        CType(e.Row.Cells(GRID_COL_BILLING_PLAN_CODE_IDX).FindControl("BillingPlanCodeTextBox"), TextBox) _
                            .Text = dvRow(BillingPlan.BillingPlanSearchDV.COL_BILLING_PLAN_CODE).ToString
                        CType(e.Row.Cells(GRID_COL_BILLING_PLAN_IDX).FindControl("BillingPlanTextBox"), TextBox).Text =
                            dvRow(BillingPlan.BillingPlanSearchDV.COL_BILLING_PLAN).ToString
                        Dim DealerGroupList As DropDownList =
                                CType(e.Row.Cells(GRID_COL_DEALER_GROUP_CODE_IDX).FindControl("DealerGroupDropdown"),
                                      DropDownList)
                        Dim DealerList As DropDownList =
                                CType(e.Row.Cells(GRID_COL_DEALER_CODE_IDX).FindControl("DealerDropdown"), DropDownList)
                        PopulateDropdown(DealerGroupList, DealerList)

                    Else
                        CType(e.Row.Cells(GRID_COL_DEALER_CODE_IDX).FindControl("DealerCodeLabel"), Label).Text =
                            dvRow(BillingPlan.BillingPlanSearchDV.COL_DEALER_CODE).ToString
                        CType(e.Row.Cells(GRID_COL_DEALER_GROUP_CODE_IDX).FindControl("DealerGroupCodeLabel"), Label).
                            Text = dvRow(BillingPlan.BillingPlanSearchDV.COL_DEALER_GROUP_CODE).ToString
                        CType(e.Row.Cells(GRID_COL_BILLING_PLAN_CODE_IDX).FindControl("BillingPlanCodeLabel"), Label).
                            Text = dvRow(BillingPlan.BillingPlanSearchDV.COL_BILLING_PLAN_CODE).ToString
                        CType(e.Row.Cells(GRID_COL_BILLING_PLAN_IDX).FindControl("BillingPlanLabel"), Label).Text =
                            dvRow(BillingPlan.BillingPlanSearchDV.COL_BILLING_PLAN).ToString

                    End If
                End If

            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try
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

                    State.BillingPlanId = New Guid(CType(Grid.Rows(index).Cells(GRID_COL_BILLING_PLAN_ID_IDX).FindControl("BillingPlanIdLabel"), Label).Text)
                    State.myBO = New BillingPlan(State.BillingPlanId)

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
                    lblCtrl = CType(Grid.Rows(RowInd).Cells(GRID_COL_BILLING_PLAN_ID_IDX).FindControl("BillingPlanIdLabel"), Label)
                    State.BillingPlanId = New Guid(lblCtrl.Text)
                    'lblCtrl = Nothing
                    'If e.CommandName = "SelectAction" Then
                    '    Grid.EditIndex = RowInd
                    '    PopulateForms()
                    '    'Disable all Edit and Delete icon buttons on the Grid
                    '    SetGridControls(Me.Grid, False)
                    'ElseIf e.CommandName = "DeleteAction" Then
                    Dim intErrCode As Integer, strErrMsg As String
                    BillingPlan.DeleteBillingPlan(State.BillingPlanId)
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

        Protected Sub BindBoPropertiesToGridHeaders()
            BindBOPropertyToGridHeader(State.myBO, "billingplancode", Grid.Columns(GRID_COL_BILLING_PLAN_CODE_IDX))
            BindBOPropertyToGridHeader(State.myBO, "BillingPlanDescription", Grid.Columns(GRID_COL_BILLING_PLAN_IDX))
            ClearGridViewHeadersAndLabelsErrSign()
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
                DealerMultipleDrop.SelectedIndex = BLANK_ITEM_SELECTED
                DealerGroupMultipleDrop.SelectedIndex = BLANK_ITEM_SELECTED
                BillingPlanTextBox.Text = String.Empty



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
                Dim errors() As ValidationError = {New ValidationError("Dealer or Dealer Group is required", GetType(BillingPlan), Nothing, "DealerID", Nothing)}
                PopulateBOFromForm()
                If ((State.myBO.DealerGroupId.ToString = Guid.Empty.ToString) AndAlso (State.myBO.DealerId.ToString = Guid.Empty.ToString)) Then
                    Throw New BOValidationException(errors, GetType(BillingPlan).FullName)
                End If
                If (State.myBO.IsDirty) Then
                    State.myBO.Save()
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

#End Region

#Region "Helper Functions"
        'Private Sub PopulateForms()
        '    Try
        '        Dim isDVEmpty As Boolean = False
        '        Dim dv As DataView
        '        'If State.searchDV Is Nothing Then State.dvForms = Forms
        '        dv = State.searchDV

        '        'If dv.Count = 0 Then
        '        '    isDVEmpty = True
        '        '    AddNewRowToDV(dv, "New_Application_Form_Id")
        '        'End If
        '        Grid.DataSource = dv

        '        If (IsGridFormInEditMode OrElse State.IsGridAddNew) Then
        '            State.GridFormEditIndex = FindSelectedRowIndexFromGuid(dv, State.BillingPlanId)
        '            Grid.EditIndex = State.GridFormEditIndex
        '        End If
        '        Grid.DataBind()

        '        If isDVEmpty Then
        '            For Each gvRow As GridViewRow In Grid.Rows
        '                gvRow.Visible = False
        '                gvRow.Controls.Clear()
        '            Next
        '        End If
        '    Catch oEx As Exception
        '        'ELPWebConstants.ShowTranslatedMessageAsPopup(APPOBJECTFORM003, ElitaPlusIdentity.Current.ActiveUser.LanguageId, Me.Page, oEx)
        '    End Try

        'End Sub


        'Public Shared Function GetGuidStringFromByteArrayNullable(ByVal value As Byte()) As String
        '    If value Is Nothing Then
        '        Return Guid.Empty.ToString
        '    Else
        '        Return GetGuidStringFromByteArray(value)
        '    End If
        'End Function


        Private Sub AddNew()
            State.searchDV = GetGridDataView()

            State.myBO = New BillingPlan
            State.BillingPlanId = State.myBO.Id

            State.searchDV = State.myBO.GetNewDataViewRow(State.searchDV, State.myBO)

            Grid.DataSource = State.searchDV

            SetPageAndSelectedIndexFromGuid(State.searchDV, State.BillingPlanId, Grid, State.PageIndex, State.IsEditMode)

            Grid.AutoGenerateColumns = False
            ' Me.Grid.Columns(Me.DESCRIPTION_COL_IDX).SortExpression = DealerGroup.DealerGroupSearchDV.COL_DESCRIPTION
            'Me.Grid.Columns(Me.CODE_COL_IDX).SortExpression = DealerGroup.DealerGroupSearchDV.COL_CODE

            SortAndBindGrid()
            SetGridControls(Grid, False)

            'Set focus on the Description TextBox for the EditItemIndex row
            ' Me.SetFocusOnEditableFieldInGrid(Me.Grid, Me.DESCRIPTION_COL_IDX, Me.DESCRIPTION_CONTROL_NAME, Me.Grid.EditIndex)
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


        Private Function GetGridDataView() As BillingPlan.BillingPlanSearchDV

            With State
                Return (BillingPlan.getList(DealerMultipleDrop.SelectedGuid, DealerGroupMultipleDrop.SelectedGuid, BillingPlanTextBox.Text.ToUpper))
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

        Private Sub PopulateDropdown(DealerGroupList As DropDownList, DealerList As DropDownList)
            Try
                'Me.BindListControlToDataView(DealerGroupList, DealerGroup.LoadList("", ""), "code", "dealer_group_id", , True) 'dealergroup by compoanygroup
                Dim listcontext As ListContext = New ListContext()
                listcontext.CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
                listcontext.LanguageId = ElitaPlusIdentity.Current.ActiveUser.LanguageId
                Dim prodLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList(ListCodes.DealerGroupByCompanyGroup, Thread.CurrentPrincipal.GetLanguageCode(), listcontext)
                DealerGroupList.Populate(prodLkl, New PopulateOptions() With
               {
               .AddBlankItem = True
               })
                'Me.BindListControlToDataView(DealerList, CType(Dealer.getList(Guid.Empty, Guid.Empty, ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id), DataView), "dealer", "dealer_id", , True)
                Dim oDealerList As Assurant.Elita.CommonConfiguration.DataElements.ListItem() = GetDealerListByCompanyForUser()

                DealerList.Populate(oDealerList, New PopulateOptions() With
                                                   {
                                                    .AddBlankItem = True,
                                                    .TextFunc = AddressOf .GetCode,
                                                    .SortFunc = AddressOf .GetCode
                                                   })
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try

        End Sub
        Private Function GetDealerListByCompanyForUser() As Assurant.Elita.CommonConfiguration.DataElements.ListItem()
            Dim Index As Integer
            Dim oListContext As New Assurant.Elita.CommonConfiguration.ListContext

            Dim UserCompanies As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Companies

            Dim oDealerList As New Collections.Generic.List(Of Assurant.Elita.CommonConfiguration.DataElements.ListItem)

            For Each company_id As Guid In ElitaPlusIdentity.Current.ActiveUser.Companies
                'UserCompanyList &= ",'" & GuidControl.GuidToHexString(UserCompanyies(Index))
                oListContext.CompanyId = company_id
                Dim oDealerListForCompany As Assurant.Elita.CommonConfiguration.DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="DealerListByCompany", context:=oListContext)
                If oDealerListForCompany.Count > 0 Then
                    If oDealerList IsNot Nothing Then
                        oDealerList.AddRange(oDealerListForCompany)
                    Else
                        Dim itm As New DataElements.ListItem
                        For Each li As DataElements.ListItem In oDealerListForCompany
                            itm = New DataElements.ListItem
                            itm.Code = li.Code
                            itm.Description = li.Description
                            itm.ExtendedCode = li.ExtendedCode
                            itm.ListItemId = li.ListItemId
                            itm.Translation = li.Translation
                            oDealerList.Add(itm)
                        Next
                    End If
                End If
            Next

            Return oDealerList.ToArray()

        End Function



        Private Sub PopulateBOFromForm()

            Try
                With State.myBO
                    .DealerGroupId = New Guid(CType(Grid.Rows(Grid.EditIndex).Cells(GRID_COL_DEALER_GROUP_CODE_IDX).FindControl("DealerGroupDropdown"), DropDownList).SelectedValue)
                    .DealerId = New Guid(CType(Grid.Rows(Grid.EditIndex).Cells(GRID_COL_DEALER_CODE_IDX).FindControl("DealerDropdown"), DropDownList).SelectedValue)
                    .BillingPlanCode = CType(Grid.Rows(Grid.EditIndex).Cells(GRID_COL_BILLING_PLAN_CODE_IDX).FindControl("BillingPlanCodeTextBox"), TextBox).Text
                    .BillingPlanDescription = CType(Grid.Rows(Grid.EditIndex).Cells(GRID_COL_BILLING_PLAN_IDX).FindControl("BillingPlanTextBox"), TextBox).Text

                End With
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try

        End Sub

        Private Sub PopulateFormFromBO()

            Dim gridRowIdx As Integer = Grid.EditIndex
            Try
                With State.myBO


                    If (.BillingPlanCode IsNot Nothing) Then
                        CType(Grid.Rows(gridRowIdx).Cells(GRID_COL_BILLING_PLAN_CODE_IDX).FindControl("BillingPlanCodeTextBox"), TextBox).Text = .BillingPlanCode
                    End If
                    If (.BillingPlanDescription IsNot Nothing) Then
                        CType(Grid.Rows(gridRowIdx).Cells(GRID_COL_BILLING_PLAN_IDX).FindControl("BillingPlanTextBox"), TextBox).Text = .BillingPlanDescription
                    End If

                    Dim DealerGroupList As DropDownList = CType(Grid.Rows(gridRowIdx).Cells(GRID_COL_DEALER_GROUP_CODE_IDX).FindControl("DealerGroupDropdown"), DropDownList)
                    Dim DealerList As DropDownList = CType(Grid.Rows(gridRowIdx).Cells(GRID_COL_DEALER_CODE_IDX).FindControl("DealerDropdown"), DropDownList)
                    PopulateDropdown(DealerGroupList, DealerList)

                    SetSelectedItem(DealerGroupList, .DealerGroupId)
                    SetSelectedItem(DealerList, .DealerId)


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

        Private Sub Grid1_Sorting(sender As Object, e As GridViewSortEventArgs) Handles Grid1.Sorting
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

#End Region

    End Class

End Namespace
