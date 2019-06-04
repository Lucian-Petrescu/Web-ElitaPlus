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
                IsEditing = (Me.grdView.EditIndex > NO_ROW_SELECTED_INDEX)
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
            Public Sub New(ByVal LastOp As DetailPageCommand, ByVal curEditingBo As AcctCompany, ByVal hasDataChanged As Boolean)
                Me.LastOperation = LastOp
                Me.EditingBo = curEditingBo
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

        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
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

        Private Sub SearchButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SearchButton.Click
            Try
                Me.State.IsGridVisible = True
                Me.State.searchDV = Nothing
                PopulateGrid()
                'Me.State.PageIndex = Me.grdView.CurrentPageIndex
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try

        End Sub

        Private Sub ClearButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ClearButton.Click

            Try
                ClearSearchCriteria()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try

        End Sub

        Private Sub ClearSearchCriteria()

            Try
                SearchDescriptionTextBox.Text = String.Empty

                'Update Page State
                With Me.State
                    .DescriptionMask = SearchDescriptionTextBox.Text
                End With
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try

        End Sub

        Private Sub NewButton_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NewButton_WRITE.Click

            Try
                Me.State.IsEditMode = True
                Me.State.IsGridVisible = True
                Me.State.AddingNewRow = True
                AddNew()
                ' SetButtonsState()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try

        End Sub

#End Region


#Region "Private Methods"


        Private Sub Page_PageReturn(ByVal ReturnFromUrl As String, ByVal ReturnPar As Object) Handles MyBase.PageReturn
            Try
                Me.MenuEnabled = True
                Me.IsReturningFromChild = True
                Dim retObj As AccountingCompanyForm.ReturnType = CType(ReturnPar, AccountingCompanyForm.ReturnType)

                Me.State.searchDV = Nothing

                Select Case retObj.LastOperation
                    Case ElitaPlusPage.DetailPageCommand.Back
                        If Not retObj Is Nothing AndAlso Not retObj.EditingBo.IsNew Then
                            Me.State.Id = retObj.EditingBo.Id
                        End If
                        Me.State.IsGridVisible = True

                    Case ElitaPlusPage.DetailPageCommand.Delete
                        Me.AddInfoMsg(Message.DELETE_RECORD_CONFIRMATION)
                End Select
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

            'Put user code to initialize the page here
            Try
                Me.ErrControllerMaster.Clear_Hide()

                Me.SetFormTitle(PAGETITLE)
                Me.SetFormTab(PAGETAB)

                Me.SetStateProperties()
                If Not Page.IsPostBack Then
                    Me.SortDirection = AcctCompany.AcctCompanyDV.COL_DESCRIPTION
                    Me.SetDefaultButton(Me.SearchDescriptionTextBox, Me.SearchButton)
                    ControlMgr.SetVisibleControl(Me, trPageSize, False)
                    Me.SetGridItemStyleColor(grdView)
                    Me.State.PageIndex = 0
                    If Me.State.MyBO Is Nothing Then
                        Me.State.MyBO = New AcctCompany
                    End If
                    '   SetButtonsState()
                    Me.TranslateGridHeader(Me.grdView)
                    Me.TranslateGridControls(Me.grdView)
                    PopulateAll()

                    If Me.IsReturningFromChild = True Then
                        PopulateGrid()
                    End If
                    'Else
                    '    CheckIfComingFromDeleteConfirm()
                End If
                BindBoPropertiesToGridHeaders()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
            Me.ShowMissingTranslations(ErrControllerMaster)
        End Sub


        Private Sub PopulateAll()
            Dim YESNOdv As DataView = LookupListNew.DropdownLookupList(YESNO, ElitaPlusIdentity.Current.ActiveUser.LanguageId, True)
            Me.State.YESNOdv = YESNOdv

        End Sub

        Private Sub PopulateGrid()

            Try

                If (Me.State.searchDV Is Nothing) Then
                    Me.State.searchDV = GetDV()
                End If
                Me.State.searchDV.Sort = Me.SortDirection

                If Me.IsReturningFromChild Then
                    SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.Id, Me.grdView, Me.State.PageIndex)
                Else
                    Me.SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Guid.Empty, Me.grdView, Me.State.PageIndex)
                End If

                Me.grdView.AutoGenerateColumns = False
                Me.grdView.Columns(Me.DESCRIPTION_COL).SortExpression = AcctCompany.AcctCompanyDV.COL_DESCRIPTION
                Me.grdView.Columns(Me.CODE_COL).SortExpression = AcctCompany.AcctCompanyDV.COL_CODE
                Me.grdView.Columns(Me.USE_ACCOUNTING_COL).SortExpression = AcctCompany.AcctCompanyDV.COL_USE_ACCOUNTING
                Me.grdView.Columns(Me.ACCT_SYSTEM_ID_COL).SortExpression = AcctCompany.AcctCompanyDV.COL_ACCT_SYSTEM_ID


                Me.SortAndBindGrid()

            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
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
            Me.State.MyBO = New AcctCompany(Me.State.Id)

            Try
                Me.State.MyBO.Delete()
                Me.State.MyBO.Save()
            Catch ex As Exception
                Me.State.MyBO.RejectChanges()
                Throw ex
            End Try

            Me.State.PageIndex = grdView.PageIndex

            'Set the IsAfterSave flag to TRUE so that the Paging logic gets invoked
            Me.State.IsAfterSave = True
            Me.State.searchDV = Nothing
            PopulateGrid()
            Me.State.PageIndex = grdView.PageIndex
        End Sub

        Private Function GetDV() As DataView

            Dim dv As DataView

            Me.State.searchDV = GetGridDataView()
            Me.State.searchDV.Sort = Me.grdView.DataMember()

            Return (Me.State.searchDV)

        End Function

        Private Function GetGridDataView() As DataView

            With State
                Return (AcctCompany.LoadList(.DescriptionMask))
            End With

        End Function

        Private Sub SetStateProperties()

            Me.State.DescriptionMask = SearchDescriptionTextBox.Text

        End Sub

        Private Sub AddNew()

            Me.State.MyBO = New AcctCompany
            Me.State.Id = Me.State.MyBO.Id

            Try
                Me.callPage(AccountingCompanyDetailForm.URL, New AccountingCompanyDetailForm.ReturnType(Guid.Empty, Me.State.MyBO))
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.BackOnErr
            End Try
        End Sub

        Private Sub SortAndBindGrid()

            Me.State.PageIndex = Me.grdView.PageIndex
            If (Me.State.searchDV.Count = 0) Then

                Me.State.bnoRow = True
                CreateHeaderForEmptyGrid(grdView, Me.SortDirection)
            Else
                Me.State.bnoRow = False
                Me.grdView.Enabled = True
                Me.grdView.DataSource = Me.State.searchDV
                HighLightSortColumn(grdView, Me.SortDirection)
                Me.grdView.DataBind()
            End If

            If Not grdView.BottomPagerRow.Visible Then grdView.BottomPagerRow.Visible = True

            ControlMgr.SetVisibleControl(Me, grdView, Me.State.IsGridVisible)

            ControlMgr.SetVisibleControl(Me, trPageSize, Me.grdView.Visible)

            Session("recCount") = Me.State.searchDV.Count

            If Me.grdView.Visible Then
                If (Me.State.AddingNewRow) Then
                    Me.lblRecordCount.Text = (Me.State.searchDV.Count - 1) & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                Else
                    Me.lblRecordCount.Text = Me.State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
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

        Private Sub Grid_PageSizeChanged(ByVal source As Object, ByVal e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
            Try
                grdView.PageIndex = NewCurrentPageIndex(grdView, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
                Me.State.selectedPageSize = CType(cboPageSize.SelectedValue, Integer)
                Me.PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
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

        Private Sub Grid_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdView.PageIndexChanging

            Try
                Me.State.PageIndex = e.NewPageIndex
                Me.grdView.PageIndex = Me.State.PageIndex
                Me.grdView.SelectedIndex = Me.NO_ITEM_SELECTED_INDEX
                Me.State.Id = Guid.Empty
                Me.PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try

        End Sub

        Protected Sub ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)

            Try
                Dim index As Integer

                If (e.CommandName = Me.EDIT_COMMAND) Then
                    index = CInt(e.CommandArgument)
                    Me.State.Id = New Guid(CType(Me.grdView.Rows(index).Cells(Me.ID_COL).FindControl(Me.ID_CONTROL_NAME), Label).Text)
                    Me.State.MyBO = New AcctCompany(Me.State.Id)

                    Me.callPage(AccountingCompanyDetailForm.URL, New AccountingCompanyDetailForm.ReturnType(Me.State.MyBO.Id, Me.State.MyBO))


                ElseIf (e.CommandName = Me.DELETE_COMMAND) Then
                    index = CInt(e.CommandArgument)

                    'Clear the SelectedItemStyle to remove the highlight from the previously saved row
                    grdView.SelectedIndex = Me.NO_ROW_SELECTED_INDEX

                    Me.State.Id = New Guid(CType(Me.grdView.Rows(index).Cells(Me.ID_COL).FindControl(Me.ID_CONTROL_NAME), Label).Text)
                    Me.DisplayMessage(Message.DELETE_RECORD_PROMPT, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Me.HiddenDeletePromptResponse)
                    Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Delete
                End If

            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try

        End Sub

        'The Binding Logic is here
        Private Sub ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdView.RowDataBound
            Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
            Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)

            If Not dvRow Is Nothing And Not Me.State.bnoRow Then

                If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then

                    CType(e.Row.Cells(Me.ID_COL).FindControl(Me.ID_CONTROL_NAME), Label).Text = GetGuidStringFromByteArray(CType(dvRow(AcctCompany.AcctCompanyDV.COL_ACCT_COMPANY_ID), Byte()))

                    Dim dRow() As DataRow

                    CType(e.Row.Cells(Me.DESCRIPTION_COL).FindControl(Me.DESCRIPTION_LABEL_CONTROL_NAME), Label).Text = dvRow(AcctCompany.AcctCompanyDV.COL_DESCRIPTION).ToString
                    CType(e.Row.Cells(Me.CODE_COL).FindControl(Me.CODE_LABEL_CONTROL_NAME), Label).Text = dvRow(AcctCompany.AcctCompanyDV.COL_CODE).ToString

                    Dim useAccountingLabel As String = dvRow(AcctCompany.AcctCompanyDV.COL_USE_ACCOUNTING).ToString
                    dRow = Me.State.YESNOdv.Table.Select("code='" & useAccountingLabel & "' and language_id='" & GuidControl.GuidToHexString(ElitaPlusIdentity.Current.ActiveUser.LanguageId) & "'")
                    If (Not dRow Is Nothing AndAlso dRow.Length > 0) Then
                        CType(e.Row.Cells(Me.USE_ACCOUNTING_COL).FindControl(Me.USE_ACCOUNTING_LABEL_CONTROL_NAME), Label).Text = CType(dRow(0).Item("Description"), String)
                    End If

                    If (Not e.Row.Cells(Me.ACCT_SYSTEM_ID_COL).FindControl(Me.ACCT_SYSTEM_LIST_LABEL_CONTROL_NAME) Is Nothing) AndAlso (Not dvRow Is Nothing) Then
                        CType(e.Row.Cells(Me.ACCT_SYSTEM_ID_COL).FindControl(Me.ACCT_SYSTEM_LIST_LABEL_CONTROL_NAME), Label).Text = LookupListNew.GetDescriptionFromId(LookupListNew.DropdownLookupList(LookupListNew.LK_ACCT_SYSTEM, ElitaPlusIdentity.Current.ActiveUser.LanguageId, True), GuidControl.ByteArrayToGuid(dvRow("Acct_system_id")))
                    End If

                    If (Not e.Row.Cells(Me.PROCESS_METHOD_COL).FindControl(Me.PROCESS_METHOD_LABEL_CONTROL_NAME) Is Nothing) AndAlso (Not dvRow Is Nothing) Then
                        CType(e.Row.Cells(Me.PROCESS_METHOD_COL).FindControl(Me.PROCESS_METHOD_LABEL_CONTROL_NAME), Label).Text = LookupListNew.GetDescriptionFromId(LookupListNew.DropdownLookupList(LookupListNew.LK_ACCOUNTING_PROCESS_METHOD, ElitaPlusIdentity.Current.ActiveUser.LanguageId, True), GuidControl.ByteArrayToGuid(dvRow("process_method_id")))
                    End If

                End If
            End If
        End Sub

        Protected Sub ItemBound(ByVal source As Object, ByVal e As GridViewRowEventArgs) Handles grdView.RowDataBound

            Try
                If Not Me.State.bnoRow Then
                    BaseItemBound(source, e)
                End If

            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

        Protected Sub ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
            BaseItemCreated(sender, e)
        End Sub

        Private Sub Grid_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdView.Sorting
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
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

        Protected Sub BindBoPropertiesToGridHeaders()
            Me.BindBOPropertyToGridHeader(Me.State.MyBO, "Description", Me.grdView.Columns(Me.DESCRIPTION_COL))
            Me.BindBOPropertyToGridHeader(Me.State.MyBO, "Code", Me.grdView.Columns(Me.CODE_COL))
            Me.BindBOPropertyToGridHeader(Me.State.MyBO, "UseAccounting", Me.grdView.Columns(Me.USE_ACCOUNTING_COL))
            Me.BindBOPropertyToGridHeader(Me.State.MyBO, "AcctSystemId", Me.grdView.Columns(Me.ACCT_SYSTEM_ID_COL))
            Me.BindBOPropertyToGridHeader(Me.State.MyBO, "ProcessMethodId", Me.grdView.Columns(Me.PROCESS_METHOD_COL))

            Me.ClearGridViewHeadersAndLabelsErrSign()
        End Sub

        Private Sub SetFocusOnEditableFieldInGrid(ByVal grid As GridView, ByVal cellPosition As Integer, ByVal controlName As String, ByVal itemIndex As Integer)
            'Set focus on the Description TextBox for the EditItemIndex row
            Dim ctl As TextBox = CType(grid.Rows(itemIndex).Cells(cellPosition).FindControl(controlName), TextBox)
            SetFocus(ctl)
        End Sub

#End Region



    End Class

End Namespace
