Option Strict On
Option Explicit On

Namespace Tables
    Partial Class AccountingCompanyForm
        Inherits ElitaPlusSearchPage

        Private Class PageStatus

            Public Sub New()
                pageIndex = 0
                pageCount = 0
            End Sub

        End Class
#Region "Member Variables"

        Private Shared pageIndex As Integer
        Private Shared pageCount As Integer
        Private IsReturningFromChild As Boolean = True

#End Region

#Region "Properties"

        Public ReadOnly Property IsEditing() As Boolean
            Get
                IsEditing = (grdView.EditIndex > NO_ROW_SELECTED_INDEX)
            End Get
        End Property

#End Region

#Region "Page State"
        Class MyState
            Public PageIndex As Integer = 0
            'Public MyBO As AcctCompany = New AcctCompany
            Public MyBO As AcctCompany
            Public DescriptionMask As String
            Public UseAccountingMask As String
            Public CompanyGroupId As Guid
            Public Id As Guid
            Public IsGridVisible As Boolean
            Public IsAfterSave As Boolean
            Public IsEditMode As Boolean
            Public AddingNewRow As Boolean
            Public Canceling As Boolean
            Public searchDV As DataView = Nothing
            Public SortExpression As String = AcctCompany.AcctCompanyDV.COL_DESCRIPTION
            Public selectedPageSize As Integer = DEFAULT_PAGE_SIZE
            Public bnoRow As Boolean = False
            Public YESNOdv As DataView
            Public ActionInProgress As DetailPageCommand = DetailPageCommand.Nothing_
        End Class

        Public Sub New()
            MyBase.New(New MyState)
        End Sub

        Protected Shadows ReadOnly Property State() As MyState
            Get
                Return CType(MyBase.State, MyState)
            End Get
        End Property

#Region "Page Return Type"
        Public Class ReturnType
            Public LastOperation As DetailPageCommand
            Public EditingBo As AcctCompany
            Public HasDataChanged As Boolean
            Public Sub New(LastOp As DetailPageCommand, curEditingBo As AcctCompany, hasDataChanged As Boolean)
                LastOperation = LastOp
                EditingBo = curEditingBo
                Me.HasDataChanged = hasDataChanged
            End Sub
        End Class
#End Region

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

        Private Const ID_COL As Integer = 1
        Private Const DESCRIPTION_COL As Integer = 2
        Private Const CODE_COL As Integer = 3
        Private Const USE_ACCOUNTING_COL As Integer = 4
        Private Const ACCT_SYSTEM_ID_COL As Integer = 5
        Private Const PROCESS_METHOD_COL As Integer = 6

        Private Const MSG_CONFIRM_PROMPT As String = "MSG_CONFIRM_PROMPT"
        Private Const MSG_RECORD_DELETED_OK As String = "MSG_RECORD_DELETED_OK"
        Private Const MSG_RECORD_SAVED_OK As String = "MSG_RECORD_SAVED_OK"
        Private Const MSG_RECORD_NOT_SAVED As String = "MSG_RECORD_NOT_SAVED"

        Private Const ID_CONTROL_NAME As String = "IdLabel"
        Private Const ACCT_SYSTEM_LIST_LABEL_CONTROL_NAME As String = "AccountingSystemLabel"
        Private Const RPT_COMMISSION_LABEL_CONTROL_NAME As String = "RptCommissionLabel"
        Private Const USE_ACCOUNTING_LABEL_CONTROL_NAME As String = "UseAccountingLabel"
        Private Const USE_ELITA_BANK_INFO_LABEL_CONTROL_NAME As String = "UseElitaBankInfoLabel"
        Private Const DESCRIPTION_LABEL_CONTROL_NAME As String = "DescriptionLabel"
        Private Const CODE_LABEL_CONTROL_NAME As String = "CodeLabel"
        Private Const FTP_DIRECTORY_LABEL_CONTROL_NAME As String = "ftpDirectoryLabel"

        Private Const BALANCE_DIRECTORY_LABEL_CONTROL_NAME As String = "balanceDirectoryLabel"
        Private Const NOTIFY_EMAIL_LABEL_CONTROL_NAME As String = "notifyEmailLabel"

        Private Const PROCESS_METHOD_LABEL_CONTROL_NAME As String = "ProcessMethodLabel"
        Private Const COV_ENTITY_BY_REGION_LABEL_CONTROL_NAME As String = "CoverageEntityByRegionLabel"


        Private Const EDIT_COMMAND As String = "EditRecord"
        Private Const DELETE_COMMAND As String = "DeleteRecord"
        Private Const SORT_COMMAND As String = "Sort"

        Private Const YESNO As String = "YESNO"

        Private Const NO_ROW_SELECTED_INDEX As Integer = -1

        Public Const PAGETITLE As String = "ACCOUNTING_COMPANY"
        Public Const PAGETAB As String = "Admin"

#End Region

#Region "Button Click Handlers"

        Private Sub SearchButton_Click(sender As System.Object, e As System.EventArgs) Handles SearchButton.Click
            Try
                State.IsGridVisible = True
                State.searchDV = Nothing
                PopulateGrid()
                'Me.State.PageIndex = Me.grdView.CurrentPageIndex
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try

        End Sub

        Private Sub ClearButton_Click(sender As System.Object, e As System.EventArgs) Handles ClearButton.Click

            Try
                ClearSearchCriteria()
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try

        End Sub

        Private Sub ClearSearchCriteria()

            Try
                SearchDescriptionTextBox.Text = String.Empty

                'Update Page State
                With State
                    .DescriptionMask = SearchDescriptionTextBox.Text
                End With
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try

        End Sub

        Private Sub NewButton_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles NewButton_WRITE.Click

            Try
                State.IsEditMode = True
                State.IsGridVisible = True
                State.AddingNewRow = True
                AddNew()
                ' SetButtonsState()
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try

        End Sub

#End Region


#Region "Private Methods"


        Private Sub Page_PageReturn(ReturnFromUrl As String, ReturnPar As Object) Handles MyBase.PageReturn
            Try
                MenuEnabled = True
                IsReturningFromChild = True
                Dim retObj As AccountingCompanyForm.ReturnType = CType(ReturnPar, AccountingCompanyForm.ReturnType)

                State.searchDV = Nothing

                Select Case retObj.LastOperation
                    Case ElitaPlusPage.DetailPageCommand.Back
                        If retObj IsNot Nothing AndAlso Not retObj.EditingBo.IsNew Then
                            State.Id = retObj.EditingBo.Id
                        End If
                        State.IsGridVisible = True

                    Case ElitaPlusPage.DetailPageCommand.Delete
                        AddInfoMsg(Message.DELETE_RECORD_CONFIRMATION)
                End Select
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

        Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

            'Put user code to initialize the page here
            Try
                ErrControllerMaster.Clear_Hide()

                SetFormTitle(PAGETITLE)
                SetFormTab(PAGETAB)

                SetStateProperties()
                If Not Page.IsPostBack Then
                    SortDirection = AcctCompany.AcctCompanyDV.COL_DESCRIPTION
                    SetDefaultButton(SearchDescriptionTextBox, SearchButton)
                    ControlMgr.SetVisibleControl(Me, trPageSize, False)
                    SetGridItemStyleColor(grdView)
                    State.PageIndex = 0
                    If State.MyBO Is Nothing Then
                        State.MyBO = New AcctCompany
                    End If
                    '   SetButtonsState()
                    TranslateGridHeader(grdView)
                    TranslateGridControls(grdView)
                    PopulateAll()

                    If IsReturningFromChild = True Then
                        PopulateGrid()
                    End If
                    'Else
                    '    CheckIfComingFromDeleteConfirm()
                End If
                BindBoPropertiesToGridHeaders()
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
            ShowMissingTranslations(ErrControllerMaster)
        End Sub


        Private Sub PopulateAll()
            Dim YESNOdv As DataView = LookupListNew.DropdownLookupList(YESNO, ElitaPlusIdentity.Current.ActiveUser.LanguageId, True)
            State.YESNOdv = YESNOdv

        End Sub

        Private Sub PopulateGrid()

            Try

                If (State.searchDV Is Nothing) Then
                    State.searchDV = GetDV()
                End If
                State.searchDV.Sort = SortDirection

                If IsReturningFromChild Then
                    SetPageAndSelectedIndexFromGuid(State.searchDV, State.Id, grdView, State.PageIndex)
                Else
                    SetPageAndSelectedIndexFromGuid(State.searchDV, Guid.Empty, grdView, State.PageIndex)
                End If

                grdView.AutoGenerateColumns = False
                grdView.Columns(DESCRIPTION_COL).SortExpression = AcctCompany.AcctCompanyDV.COL_DESCRIPTION
                grdView.Columns(CODE_COL).SortExpression = AcctCompany.AcctCompanyDV.COL_CODE
                grdView.Columns(USE_ACCOUNTING_COL).SortExpression = AcctCompany.AcctCompanyDV.COL_USE_ACCOUNTING
                grdView.Columns(ACCT_SYSTEM_ID_COL).SortExpression = AcctCompany.AcctCompanyDV.COL_ACCT_SYSTEM_ID


                SortAndBindGrid()

            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try

        End Sub

        'Protected Sub CheckIfComingFromDeleteConfirm()
        '    Dim confResponse As String = Me.HiddenDeletePromptResponse.Value
        '    If Not confResponse Is Nothing AndAlso confResponse = Me.MSG_VALUE_YES Then
        '        If Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Delete Then
        '            DoDelete()
        '        End If
        '        Select Case Me.State.ActionInProgress
        '            Case ElitaPlusPage.DetailPageCommand.Delete
        '        End Select
        '    ElseIf Not confResponse Is Nothing AndAlso confResponse = Me.MSG_VALUE_NO Then
        '        Select Case Me.State.ActionInProgress
        '            Case ElitaPlusPage.DetailPageCommand.Delete
        '        End Select
        '    End If
        '    'Clean after consuming the action
        '    Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
        '    Me.HiddenDeletePromptResponse.Value = ""
        'End Sub

        Private Sub DoDelete()
            State.MyBO = New AcctCompany(State.Id)

            Try
                State.MyBO.Delete()
                State.MyBO.Save()
            Catch ex As Exception
                State.MyBO.RejectChanges()
                Throw ex
            End Try

            State.PageIndex = grdView.PageIndex

            'Set the IsAfterSave flag to TRUE so that the Paging logic gets invoked
            State.IsAfterSave = True
            State.searchDV = Nothing
            PopulateGrid()
            State.PageIndex = grdView.PageIndex
        End Sub

        Private Function GetDV() As DataView

            Dim dv As DataView

            State.searchDV = GetGridDataView()
            State.searchDV.Sort = grdView.DataMember()

            Return (State.searchDV)

        End Function

        Private Function GetGridDataView() As DataView

            With State
                Return (AcctCompany.LoadList(.DescriptionMask))
            End With

        End Function

        Private Sub SetStateProperties()

            State.DescriptionMask = SearchDescriptionTextBox.Text

        End Sub

        Private Sub AddNew()

            State.MyBO = New AcctCompany
            State.Id = State.MyBO.Id

            Try
                callPage(AccountingCompanyDetailForm.URL, New AccountingCompanyDetailForm.ReturnType(Guid.Empty, State.MyBO))
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.BackOnErr
            End Try
        End Sub

        Private Sub SortAndBindGrid()

            State.PageIndex = grdView.PageIndex
            If (State.searchDV.Count = 0) Then

                State.bnoRow = True
                CreateHeaderForEmptyGrid(grdView, SortDirection)
            Else
                State.bnoRow = False
                grdView.Enabled = True
                grdView.DataSource = State.searchDV
                HighLightSortColumn(grdView, SortDirection)
                grdView.DataBind()
            End If

            If Not grdView.BottomPagerRow.Visible Then grdView.BottomPagerRow.Visible = True

            ControlMgr.SetVisibleControl(Me, grdView, State.IsGridVisible)

            ControlMgr.SetVisibleControl(Me, trPageSize, grdView.Visible)

            Session("recCount") = State.searchDV.Count

            If grdView.Visible Then
                If (State.AddingNewRow) Then
                    lblRecordCount.Text = (State.searchDV.Count - 1) & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                Else
                    lblRecordCount.Text = State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                End If
            End If
            ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me, grdView)
        End Sub

        'Private Sub SetButtonsState()

        '    If (Me.State.IsEditMode) Then
        '        ControlMgr.SetVisibleControl(Me, NewButton_WRITE, False)
        '        ControlMgr.SetEnableControl(Me, SearchButton, False)
        '        ControlMgr.SetEnableControl(Me, ClearButton, False)
        '        Me.MenuEnabled = False
        '        If (Me.cboPageSize.Visible) Then
        '            ControlMgr.SetEnableControl(Me, cboPageSize, False)
        '        End If
        '        'Linkbutton_panel.Enabled = False
        '    Else
        '        ControlMgr.SetVisibleControl(Me, NewButton_WRITE, True)
        '        ControlMgr.SetEnableControl(Me, SearchButton, True)
        '        ControlMgr.SetEnableControl(Me, ClearButton, True)
        '        Me.MenuEnabled = True
        '        If (Me.cboPageSize.Visible) Then
        '            ControlMgr.SetEnableControl(Me, cboPageSize, True)
        '        End If
        '        'Linkbutton_panel.Enabled = True
        '    End If

        'End Sub

        Private Sub Grid_PageSizeChanged(source As Object, e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
            Try
                grdView.PageIndex = NewCurrentPageIndex(grdView, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
                State.selectedPageSize = CType(cboPageSize.SelectedValue, Integer)
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
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

        Private Sub Grid_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdView.PageIndexChanging

            Try
                State.PageIndex = e.NewPageIndex
                grdView.PageIndex = State.PageIndex
                grdView.SelectedIndex = NO_ITEM_SELECTED_INDEX
                State.Id = Guid.Empty
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try

        End Sub

        Protected Sub ItemCommand(source As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs)

            Try
                Dim index As Integer

                If (e.CommandName = EDIT_COMMAND) Then
                    index = CInt(e.CommandArgument)
                    State.Id = New Guid(CType(grdView.Rows(index).Cells(ID_COL).FindControl(ID_CONTROL_NAME), Label).Text)
                    State.MyBO = New AcctCompany(State.Id)

                    callPage(AccountingCompanyDetailForm.URL, New AccountingCompanyDetailForm.ReturnType(State.MyBO.Id, State.MyBO))


                ElseIf (e.CommandName = DELETE_COMMAND) Then
                    index = CInt(e.CommandArgument)

                    'Clear the SelectedItemStyle to remove the highlight from the previously saved row
                    grdView.SelectedIndex = NO_ROW_SELECTED_INDEX

                    State.Id = New Guid(CType(grdView.Rows(index).Cells(ID_COL).FindControl(ID_CONTROL_NAME), Label).Text)
                    DisplayMessage(Message.DELETE_RECORD_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenDeletePromptResponse)
                    State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Delete
                End If

            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try

        End Sub

        'The Binding Logic is here
        Private Sub ItemDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdView.RowDataBound
            Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
            Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)

            If dvRow IsNot Nothing And Not State.bnoRow Then

                If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then

                    CType(e.Row.Cells(ID_COL).FindControl(ID_CONTROL_NAME), Label).Text = GetGuidStringFromByteArray(CType(dvRow(AcctCompany.AcctCompanyDV.COL_ACCT_COMPANY_ID), Byte()))

                    Dim dRow() As DataRow

                    CType(e.Row.Cells(DESCRIPTION_COL).FindControl(DESCRIPTION_LABEL_CONTROL_NAME), Label).Text = dvRow(AcctCompany.AcctCompanyDV.COL_DESCRIPTION).ToString
                    CType(e.Row.Cells(CODE_COL).FindControl(CODE_LABEL_CONTROL_NAME), Label).Text = dvRow(AcctCompany.AcctCompanyDV.COL_CODE).ToString

                    Dim useAccountingLabel As String = dvRow(AcctCompany.AcctCompanyDV.COL_USE_ACCOUNTING).ToString
                    dRow = State.YESNOdv.Table.Select("code='" & useAccountingLabel & "' and language_id='" & GuidControl.GuidToHexString(ElitaPlusIdentity.Current.ActiveUser.LanguageId) & "'")
                    If (dRow IsNot Nothing AndAlso dRow.Length > 0) Then
                        CType(e.Row.Cells(USE_ACCOUNTING_COL).FindControl(USE_ACCOUNTING_LABEL_CONTROL_NAME), Label).Text = CType(dRow(0).Item("Description"), String)
                    End If

                    If (e.Row.Cells(ACCT_SYSTEM_ID_COL).FindControl(ACCT_SYSTEM_LIST_LABEL_CONTROL_NAME) IsNot Nothing) AndAlso (dvRow IsNot Nothing) Then
                        CType(e.Row.Cells(ACCT_SYSTEM_ID_COL).FindControl(ACCT_SYSTEM_LIST_LABEL_CONTROL_NAME), Label).Text = LookupListNew.GetDescriptionFromId(LookupListNew.DropdownLookupList(LookupListNew.LK_ACCT_SYSTEM, ElitaPlusIdentity.Current.ActiveUser.LanguageId, True), GuidControl.ByteArrayToGuid(dvRow("Acct_system_id")))
                    End If

                    If (e.Row.Cells(PROCESS_METHOD_COL).FindControl(PROCESS_METHOD_LABEL_CONTROL_NAME) IsNot Nothing) AndAlso (dvRow IsNot Nothing) Then
                        CType(e.Row.Cells(PROCESS_METHOD_COL).FindControl(PROCESS_METHOD_LABEL_CONTROL_NAME), Label).Text = LookupListNew.GetDescriptionFromId(LookupListNew.DropdownLookupList(LookupListNew.LK_ACCOUNTING_PROCESS_METHOD, ElitaPlusIdentity.Current.ActiveUser.LanguageId, True), GuidControl.ByteArrayToGuid(dvRow("process_method_id")))
                    End If

                End If
            End If
        End Sub

        Protected Sub ItemBound(source As Object, e As GridViewRowEventArgs) Handles grdView.RowDataBound

            Try
                If Not State.bnoRow Then
                    BaseItemBound(source, e)
                End If

            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

        Protected Sub ItemCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs)
            BaseItemCreated(sender, e)
        End Sub

        Private Sub Grid_SortCommand(source As Object, e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdView.Sorting
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
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

        Protected Sub BindBoPropertiesToGridHeaders()
            BindBOPropertyToGridHeader(State.MyBO, "Description", grdView.Columns(DESCRIPTION_COL))
            BindBOPropertyToGridHeader(State.MyBO, "Code", grdView.Columns(CODE_COL))
            BindBOPropertyToGridHeader(State.MyBO, "UseAccounting", grdView.Columns(USE_ACCOUNTING_COL))
            BindBOPropertyToGridHeader(State.MyBO, "AcctSystemId", grdView.Columns(ACCT_SYSTEM_ID_COL))
            BindBOPropertyToGridHeader(State.MyBO, "ProcessMethodId", grdView.Columns(PROCESS_METHOD_COL))

            ClearGridViewHeadersAndLabelsErrSign()
        End Sub

        Private Sub SetFocusOnEditableFieldInGrid(grid As GridView, cellPosition As Integer, controlName As String, itemIndex As Integer)
            'Set focus on the Description TextBox for the EditItemIndex row
            Dim ctl As TextBox = CType(grid.Rows(itemIndex).Cells(cellPosition).FindControl(controlName), TextBox)
            SetFocus(ctl)
        End Sub

#End Region



    End Class

End Namespace
