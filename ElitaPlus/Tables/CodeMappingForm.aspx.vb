Public Partial Class CodeMappingForm
    Inherits ElitaPlusSearchPage

#Region "Constants"

    Public Const URL As String = "Tables/CodeMappingForm.aspx"
    Public Const PAGETITLE As String = "CODE_MAPPING"
    Public Const PAGETAB As String = "ADMIN"
    Private Const GRID_COL_LIST_ITEM_ID As Integer = 0
    Private Const GRID_COL_CODE_ALIAS As Integer = 1
    Private Const GRID_COL_DESCRIPTION_ALIAS As Integer = 2
    Private Const GRID_COL_NEW_DESCRIPTION_ALIAS As Integer = 3
    Private Const GRID_COL_CODE_MAPPING_ID As Integer = 4

    Private Const CODE_IN_GRID_CONTROL_NAME As String = "moCodeLabelGrid"
    Private Const DESCRIPTION_IN_GRID_CONTROL_NAME As String = "moDescriptionLabelGrid"
    Private Const NEW_DESCRIPTION_IN_GRID_CONTROL_NAME As String = "moNewDescriptionTextGrid"
    Private Const CODE_MAPPING_ID As String = "moCodeMappingIdLabel"

    Private Const MSG_RECORD_SAVED_OK As String = "MSG_RECORD_SAVED_OK"
    Private Const MSG_RECORD_NOT_SAVED As String = "MSG_RECORD_NOT_SAVED" '"The record has not been saved because the current record has not been changed"
    Private Const ONE_ITEM As Integer = 1
    Private Const NO_ROW_SELECTED_INDEX As Integer = -1



    'Protected WithEvents moErrorController As ErrorController

#End Region

#Region "Properties"

    Public ReadOnly Property UserCompanyMultipleDrop() As Common.MultipleColumnDDLabelControl
        Get
            If moUserCompanyMultipleDrop Is Nothing Then
                moUserCompanyMultipleDrop = CType(FindControl("moUserCompanyMultipleDrop"), Common.MultipleColumnDDLabelControl)
            End If
            Return moUserCompanyMultipleDrop
        End Get
    End Property

    Public ReadOnly Property ListItemMultipleDrop() As Common.MultipleColumnDDLabelControl
        Get
            If moMultipleColumnDDListItem Is Nothing Then
                moMultipleColumnDDListItem = CType(FindControl("moMultipleColumnDDListItem"), Common.MultipleColumnDDLabelControl)
            End If
            Return moMultipleColumnDDListItem
        End Get

    End Property

#End Region

#Region "PAGE STATE"

    Class MyState
        Public MyBO As CodeMapping
        Public searchDV As DataView = Nothing
        Public SortExpression As String = CodeMapping.ListItemSearchDV.GRID_COL_DESCRIPTION
        Public listId As Guid = Guid.Empty
        Public companyId As Guid = Guid.Empty
        Public PageIndex As Integer = 0
        Public PageSize As Integer = DEFAULT_PAGE_SIZE
        'Public selectedPageSize As Integer = DEFAULT_PAGE_SIZE
        Public IsGridVisible As Boolean
        Public deleteCreator As Boolean = False
        Public ActionInProgress As DetailPageCommand = DetailPageCommand.Nothing_
        'DEF-1444: Errors on Code Mapping Screen. 
        Public selectedGuid As Guid = Guid.Empty
        'End of DEF-1444
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

#Region "Handlers-Init"

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        'Put user code to initialize the page here
        ErrControllerMaster.Clear_Hide()
        
        Try
            If Not IsPostBack Then
                SetFormTitle(PAGETITLE)
                SetFormTab(PAGETAB)
                SetGridItemStyleColor(moListItemGrid)
                If State.IsGridVisible Then
                    'If Not (Me.State.selectedPageSize = DEFAULT_PAGE_SIZE) Then
                    cboPageSize.SelectedValue = CType(State.PageSize, String)
                    moListItemGrid.PageSize = State.PageSize
                    'End If
                End If
                PopulateDropdowns()
                EnableDisableButtons(IsDataGPageDirty)
            Else
                CheckIfComingFromSaveConfirm()
            End If

        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try

    End Sub

#End Region

#Region "Event Handlers"


    Private Sub moListItemDropDown_SelectedIndexChanged(moUserCompanyMultipleDrop As Common.MultipleColumnDDLabelControl) Handles moMultipleColumnDDListItem.SelectedDropChanged


        Try
            State.searchDV = Nothing
            State.IsGridVisible = True
            State.companyId = UserCompanyMultipleDrop.SelectedGuid

            ValidatedDropdownSelections()

            State.listId = moMultipleColumnDDListItem.SelectedGuid
            State.PageIndex = moListItemGrid.CurrentPageIndex
            PopulateListItemGrid()
            EnableDisableButtons(True)

        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub


    Private Sub moBtnCancel_Click(sender As Object, e As System.EventArgs) Handles moBtnCancel.Click
        PopulateListItemGrid()
    End Sub

    Private Sub btnBack_Click(sender As Object, e As System.EventArgs) Handles btnBack.Click
        Try
            If IsDataGPageDirty() Then
                SavePage()
                If State.MyBO.IsFamilyDirty() Then
                    DisplayMessage(Message.MSG_PAGE_SAVE_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSavePagePromptResponse)
                    State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
                Else
                    ReturnToCallingPage()
                End If
            End If

        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
            DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSavePagePromptResponse)
            State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
        End Try

        ReturnToTabHomePage()
    End Sub


    Private Sub moBtnSave_WRITE_Click(sender As Object, e As System.EventArgs) Handles moBtnSave_WRITE.Click
        Try
            If IsDataGPageDirty() Then
                SavePage()
                If (State.MyBO.IsFamilyDirty) Then
                    State.MyBO.Save()
                    ShowInfoMsgBox(MSG_RECORD_SAVED_OK)
                    State.searchDV = Nothing
                    PopulateListItemGrid()
                    ControlMgr.SetVisibleControl(Me, moListItemGrid, True)
                Else
                    ShowInfoMsgBox(MSG_RECORD_NOT_SAVED)
                    PopulateListItemGrid()
                    ReturnFromEditing()
                End If
            Else
                ShowInfoMsgBox(MSG_RECORD_NOT_SAVED)
                PopulateListItemGrid()
                ReturnFromEditing()
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
            ReturnFromEditing(True)
            State.searchDV = Nothing
            PopulateListItemGrid()

        End Try
    End Sub
#End Region

#Region "POPULATE"

    Private Sub PopulateDropdowns()


        Try

            'Fill Companies
            Dim CompanyDV As DataView = LookupListNew.GetUserCompaniesLookupList()

            UserCompanyMultipleDrop.NothingSelected = True
            UserCompanyMultipleDrop.SetControl(True, UserCompanyMultipleDrop.MODES.NEW_MODE, True, CompanyDV, UserCompanyMultipleDrop.NO_CAPTION, True)
            If CompanyDV.Count.Equals(ONE_ITEM) Then
                UserCompanyMultipleDrop.SelectedIndex = ONE_ITEM
                State.companyId = UserCompanyMultipleDrop.SelectedGuid
                UserCompanyMultipleDrop.ChangeEnabledControlProperty(False)
            End If

            ListItemMultipleDrop.NothingSelected = True
            ListItemMultipleDrop.SetControl(True, ListItemMultipleDrop.MODES.NEW_MODE, True, LookupListNew.GetListId(ElitaPlusIdentity.Current.ActiveUser.LanguageId, True), ListItemMultipleDrop.NO_CAPTION, True)


        Catch ex As Exception
            ErrControllerMaster.AddErrorAndShow(Assurant.ElitaPlus.Common.ErrorCodes.DB_ERROR)
        End Try


    End Sub

    

#End Region

#Region " Datagrid Related "

    Private Sub PopulateListItemGrid()
        Dim dv As DataView
        Try
            If (State.searchDV Is Nothing) Then
                'Me.SetStateProperties()
                State.searchDV = GetListItemsGridDataView()
            End If

            State.searchDV.Sort = State.SortExpression

            moListItemGrid.AutoGenerateColumns = False
            moListItemGrid.Columns(GRID_COL_CODE_ALIAS).SortExpression = State.MyBO.ListItemSearchDV.GRID_COL_CODE
            moListItemGrid.Columns(GRID_COL_DESCRIPTION_ALIAS).SortExpression = State.MyBO.ListItemSearchDV.GRID_COL_DESCRIPTION
            moListItemGrid.Columns(GRID_COL_NEW_DESCRIPTION_ALIAS).SortExpression = State.MyBO.ListItemSearchDV.GRID_COL_NEW_DESCRIPTION
            'DEF-1444: Errors on Code Mapping Screen. 
            SetPageAndSelectedIndexFromGuid(State.searchDV, State.selectedGuid, moListItemGrid, State.PageIndex)
            'End of DEF-1444
            ' SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.DictItemId, Me.moListItemGrid, Me.State.PageIndex)
            SortAndBindGrid()

        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub

    Private Sub SortAndBindGrid()
        State.PageIndex = moListItemGrid.CurrentPageIndex
        TranslateGridControls(moListItemGrid)
        moListItemGrid.DataSource = State.searchDV
        HighLightSortColumn(moListItemGrid, State.SortExpression)
        moListItemGrid.DataBind()

        ControlMgr.SetVisibleControl(Me, moListItemGrid, State.IsGridVisible)
        ControlMgr.SetVisibleControl(Me, trPageSize, moListItemGrid.Visible)
        lblRecordCount.Text = State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)

        Session("recCount") = State.searchDV.Count

        ' ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me, moListItemGrid)
    End Sub

    Private Function GetListItemsGridDataView() As DataView
        Dim bo As CodeMapping = New CodeMapping

        State.searchDV = bo.AdminLoadListItems(ElitaPlusIdentity.Current.ActiveUser.LanguageId, State.listId, State.companyId)

        State.searchDV.Sort = moListItemGrid.DataMember()
        moListItemGrid.DataSource = State.searchDV

        Return (State.searchDV)
    End Function

    Private Sub moDictionaryGrid_PageIndexChanged(source As System.Object, e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles moListItemGrid.PageIndexChanged
        Try
            State.PageIndex = e.NewPageIndex
            moListItemGrid.CurrentPageIndex = State.PageIndex
            PopulateListItemGrid()
            moListItemGrid.SelectedIndex = NO_ITEM_SELECTED_INDEX
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub


    Private Sub Grid_PageSizeChanged(source As Object, e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
        Try
            State.PageSize = CType(cboPageSize.SelectedValue, Integer)
            moListItemGrid.CurrentPageIndex = NewCurrentPageIndex(moListItemGrid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
            PopulateListItemGrid()
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub

    Private Sub Grid_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles moListItemGrid.ItemDataBound
        Dim itemType As ListItemType = CType(e.Item.ItemType, ListItemType)
        Dim dvRow As DataRowView = CType(e.Item.DataItem, DataRowView)
        Dim oTextBox As TextBox

        If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then
            With e.Item
                PopulateControlFromBOProperty(.Cells(GRID_COL_LIST_ITEM_ID), dvRow(CodeMapping.ListItemSearchDV.GRID_COL_LIST_ITEM_ID))
                'Me.PopulateControlFromBOProperty(.Cells(Me.GRID_COL_CODE_MAPPING_ID), dvRow(CodeMapping.ListItemSearchDV.GRID_COL_CODE_MAPPING_ID))


                oTextBox = CType(e.Item.Cells(GRID_COL_NEW_DESCRIPTION_ALIAS).FindControl(NEW_DESCRIPTION_IN_GRID_CONTROL_NAME), TextBox)
                oTextBox.Attributes.Add("onchange", "setDirty()")
                'If e.Item.ItemIndex() = 0 Then
                '    SetFocus(oTextBox)
                'End If
                PopulateControlFromBOProperty(oTextBox, dvRow(CodeMapping.ListItemSearchDV.GRID_COL_NEW_DESCRIPTION))
                PopulateControlFromBOProperty(.Cells(GRID_COL_CODE_ALIAS), dvRow(CodeMapping.ListItemSearchDV.GRID_COL_CODE))
                PopulateControlFromBOProperty(.Cells(GRID_COL_DESCRIPTION_ALIAS), dvRow(CodeMapping.ListItemSearchDV.GRID_COL_DESCRIPTION))
                PopulateControlFromBOProperty(.Cells(GRID_COL_CODE_MAPPING_ID), dvRow(CodeMapping.ListItemSearchDV.GRID_COL_CODE_MAPPING_ID))

            End With
        End If
        BaseItemBound(sender, e)

    End Sub


    Private Sub Grid_SortCommand(source As Object, e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles moListItemGrid.SortCommand
        Try
            If State.SortExpression.StartsWith(e.SortExpression) Then
                If State.SortExpression.EndsWith(" DESC") Then
                    State.SortExpression = e.SortExpression
                Else
                    State.SortExpression &= " DESC"
                End If
            Else
                State.SortExpression = e.SortExpression
            End If
            'To handle the requirement of always going to the FIRST page on the Grid whenever the user switches the sorting criterion
            'Set the Me.State.selectedClaimId = Guid.Empty and set Me.State.PageIndex = 0
            'Me.State.listId = Guid.Empty
            'Me.State.PageIndex = 0

            PopulateListItemGrid()
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try

    End Sub

    Public Sub ItemCommand(source As Object, e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles moListItemGrid.ItemCommand
       
    End Sub

    Public Sub ItemCreated(sender As Object, e As DataGridItemEventArgs)
        BaseItemCreated(sender, e)
    End Sub


#End Region


#Region "Controlling Logic"

    Private Sub ReturnFromEditing(Optional ByVal isSaveWithErrors As Boolean = False)

        If Not isSaveWithErrors Then
            If (moListItemGrid.PageCount = 0) Then
                ControlMgr.SetVisibleControl(Me, moListItemGrid, False)
            Else
                ControlMgr.SetVisibleControl(Me, moListItemGrid, True)
            End If

            State.PageIndex = moListItemGrid.CurrentPageIndex
        End If
    End Sub

    Private Sub SavePage()

        Dim CodeMappId As Guid
        Dim otherBOs As CodeMapping
        Dim newCode As String
        'DEF-1444. Setting one flag, isFirstCodeMap to resolve the errors on code mapping screen
        Dim isFirstCodeMap As Boolean
        isFirstCodeMap = False
        'DEF-1444. End
        For i As Integer = 0 To moListItemGrid.Items.Count - 1
            moListItemGrid.SelectedIndex = i
            BindBOPropertyToGridHeader(State.MyBO, "NewDescription", moListItemGrid.Columns(GRID_COL_NEW_DESCRIPTION_ALIAS))
            ClearGridHeadersAndLabelsErrSign()
            If moListItemGrid.Items(i).Cells(GRID_COL_CODE_MAPPING_ID).Text = "" Then
                CodeMappId = Guid.Empty
            Else
                CodeMappId = New Guid(moListItemGrid.Items(i).Cells(GRID_COL_CODE_MAPPING_ID).Text)
            End If
            newCode = CType(moListItemGrid.Items(moListItemGrid.SelectedIndex).Cells(GRID_COL_NEW_DESCRIPTION_ALIAS).FindControl(NEW_DESCRIPTION_IN_GRID_CONTROL_NAME), TextBox).Text()
            'DEF-1444. Start
            If State.searchDV.Item(i)(GRID_COL_NEW_DESCRIPTION_ALIAS).ToString() <> newCode Then
                State.selectedGuid = New Guid(moListItemGrid.Items(i).Cells(GRID_COL_LIST_ITEM_ID).Text)
            End If
            'DEF-1444. End
            If Not newCode = "" Then
                If CodeMappId.Equals(Guid.Empty) Then
                    'DEF-1444. Start
                    If i = 0 Or State.MyBO Is Nothing Or Not isFirstCodeMap Then
                        State.MyBO = New CodeMapping
                        PopulateBOProperty(State.MyBO, "CompanyId", State.companyId)
                        PopulateBOProperty(State.MyBO, "ListItemId", New Guid(moListItemGrid.Items(i).Cells(GRID_COL_LIST_ITEM_ID).Text))
                        PopulateBOProperty(State.MyBO, "NewDescription", CType(moListItemGrid.Items(moListItemGrid.SelectedIndex).Cells(GRID_COL_NEW_DESCRIPTION_ALIAS).FindControl(NEW_DESCRIPTION_IN_GRID_CONTROL_NAME), TextBox).Text)
                        isFirstCodeMap = True
                        'DEF-1444. End
                    Else
                        otherBOs = New CodeMapping(State.MyBO.MyDataset)
                        PopulateBOProperty(otherBOs, "CompanyId", State.companyId)
                        PopulateBOProperty(otherBOs, "ListItemId", New Guid(moListItemGrid.Items(i).Cells(GRID_COL_LIST_ITEM_ID).Text))
                        PopulateBOProperty(otherBOs, "NewDescription", CType(moListItemGrid.Items(moListItemGrid.SelectedIndex).Cells(GRID_COL_NEW_DESCRIPTION_ALIAS).FindControl(NEW_DESCRIPTION_IN_GRID_CONTROL_NAME), TextBox).Text)
                        otherBOs.Save()
                    End If
                Else
                    'DEF-1444. Start
                    If i = 0 Or State.MyBO Is Nothing Or Not isFirstCodeMap Then
                        State.MyBO = New CodeMapping(CodeMappId)
                        PopulateBOProperty(State.MyBO, "NewDescription", CType(moListItemGrid.Items(moListItemGrid.SelectedIndex).Cells(GRID_COL_NEW_DESCRIPTION_ALIAS).FindControl(NEW_DESCRIPTION_IN_GRID_CONTROL_NAME), TextBox).Text)
                        isFirstCodeMap = True
                        'DEF-1444. End
                    Else
                        otherBOs = New CodeMapping(CodeMappId, State.MyBO.MyDataset)
                        PopulateBOProperty(otherBOs, "NewDescription", CType(moListItemGrid.Items(moListItemGrid.SelectedIndex).Cells(GRID_COL_NEW_DESCRIPTION_ALIAS).FindControl(NEW_DESCRIPTION_IN_GRID_CONTROL_NAME), TextBox).Text)
                        otherBOs.Save()
                    End If
                End If
            ElseIf newCode = "" Then
                If Not CodeMappId.Equals(Guid.Empty) Then
                    'DEF-1444. Start
                    If i = 0 Or State.MyBO Is Nothing Or Not isFirstCodeMap Then
                        State.MyBO = New CodeMapping(CodeMappId)
                        State.MyBO.Delete()
                        isFirstCodeMap = True
                        'DEF-1444. End
                    Else
                        otherBOs = New CodeMapping(CodeMappId, State.MyBO.MyDataset)
                        otherBOs.Delete()
                        otherBOs.Save()
                    End If
                End If
            End If
        Next

    End Sub

    Private Sub CreateBoFromGrid(index As Integer)
        
    End Sub

    Private Sub ValidatedDropdownSelections()

        Dim result As Boolean = True

        If UserCompanyMultipleDrop.SelectedIndex = 0 Then
            Throw New GUIException(Message.MSG_COMPANY_IS_REQUIRED, Assurant.ElitaPlus.Common.ErrorCodes.GUI_COMPANY_REQUIRED)
        End If

        If moMultipleColumnDDListItem.SelectedIndex = 0 Then
            Throw New GUIException(Message.MSG_LIST_ITEM_REQUIRED, Assurant.ElitaPlus.Common.ErrorCodes.GUI_LIST_ITEM_TYPE_IS_REQUIRED)
        End If

    End Sub


    Protected Sub CheckIfComingFromSaveConfirm()

        Dim confResponse As String = HiddenSavePagePromptResponse.Value

        If confResponse IsNot Nothing AndAlso confResponse = MSG_VALUE_YES Then
            If State.ActionInProgress <> ElitaPlusPage.DetailPageCommand.BackOnErr Then
                SavePage()
                If (State.MyBO.IsFamilyDirty) Then
                    State.MyBO.Save()
                End If
            End If
            Select Case State.ActionInProgress
                Case ElitaPlusPage.DetailPageCommand.Back
                    ReturnToCallingPage()
                Case ElitaPlusPage.DetailPageCommand.New_
                    'Me.AddInfoMsg(Message.SAVE_RECORD_CONFIRMATION)
            End Select
        ElseIf confResponse IsNot Nothing AndAlso confResponse = MSG_VALUE_NO Then
            Select Case State.ActionInProgress
                Case ElitaPlusPage.DetailPageCommand.Back
                    ReturnToCallingPage()
                Case ElitaPlusPage.DetailPageCommand.New_
                    'Me.State.isNew = True
                    'Me.PopulateGrid()
                Case ElitaPlusPage.DetailPageCommand.Accept
                    'Me.State.isNew = False
                    'Me.State.year = Me.GetSelectedDescription(Me.moYearDropDownList)
                    'PopulateYearsDropdown()
                    'setYearSelection(Me.State.year)
                    'PopulateGrid()
            End Select
        End If

        If Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Undo Then
            'Me.State.year = Me.State.prevSelectedYear
            'PopulateYearsDropdown()
            'setYearSelection(Me.State.year)
            Return
        End If

        'Clean after consuming the action
        State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
        HiddenSavePagePromptResponse.Value = ""

    End Sub


    Function IsDataGPageDirty() As Boolean
        Dim Result As String = HiddenIsPageDirty.Value

        Return Result.Equals("YES")
    End Function

    Private Sub EnableDisableButtons(isAnable As Boolean)

        ControlMgr.SetEnableControl(Me, moBtnCancel, isAnable)
        ControlMgr.SetEnableControl(Me, moBtnSave_WRITE, isAnable)


    End Sub

#End Region
    Private Sub ShowInfoMsgBox(strMsg As String, Optional ByVal Translate As Boolean = True)
        Dim translatedMsg As String = strMsg
        If Translate Then translatedMsg = TranslationBase.TranslateLabelOrMessage(strMsg)
        Dim sJavaScript As String
        sJavaScript = "<SCRIPT>" & Environment.NewLine
        sJavaScript &= "setTimeout(""showMessage('" & translatedMsg & "', '" & "AlertWindow" & "', '" & MSG_BTN_OK & "', '" & MSG_TYPE_INFO & "', '" & "null" & "')"", 0);" & Environment.NewLine
        sJavaScript &= "</SCRIPT>" & Environment.NewLine
        RegisterStartupScript("ShowConfirmation", sJavaScript)
    End Sub

End Class