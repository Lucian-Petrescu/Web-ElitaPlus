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

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'Put user code to initialize the page here
        Me.ErrControllerMaster.Clear_Hide()
        
        Try
            If Not Me.IsPostBack Then
                Me.SetFormTitle(PAGETITLE)
                Me.SetFormTab(PAGETAB)
                Me.SetGridItemStyleColor(Me.moListItemGrid)
                If Me.State.IsGridVisible Then
                    'If Not (Me.State.selectedPageSize = DEFAULT_PAGE_SIZE) Then
                    cboPageSize.SelectedValue = CType(Me.State.PageSize, String)
                    moListItemGrid.PageSize = Me.State.PageSize
                    'End If
                End If
                PopulateDropdowns()
                EnableDisableButtons(IsDataGPageDirty)
            Else
                CheckIfComingFromSaveConfirm()
            End If

        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try

    End Sub

#End Region

#Region "Event Handlers"


    Private Sub moListItemDropDown_SelectedIndexChanged(ByVal moUserCompanyMultipleDrop As Common.MultipleColumnDDLabelControl) Handles moMultipleColumnDDListItem.SelectedDropChanged


        Try
            Me.State.searchDV = Nothing
            Me.State.IsGridVisible = True
            Me.State.companyId = Me.UserCompanyMultipleDrop.SelectedGuid

            ValidatedDropdownSelections()

            Me.State.listId = Me.moMultipleColumnDDListItem.SelectedGuid
            Me.State.PageIndex = moListItemGrid.CurrentPageIndex
            PopulateListItemGrid()
            EnableDisableButtons(True)

        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub


    Private Sub moBtnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles moBtnCancel.Click
        Me.PopulateListItemGrid()
    End Sub

    Private Sub btnBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Try
            If IsDataGPageDirty() Then
                Me.SavePage()
                If Me.State.MyBO.IsFamilyDirty() Then
                    DisplayMessage(Message.MSG_PAGE_SAVE_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, Me.HiddenSavePagePromptResponse)
                    Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
                Else
                    Me.ReturnToCallingPage()
                End If
            End If

        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, ErrControllerMaster)
            Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Me.HiddenSavePagePromptResponse)
            Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
        End Try

        Me.ReturnToTabHomePage()
    End Sub


    Private Sub moBtnSave_WRITE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles moBtnSave_WRITE.Click
        Try
            If IsDataGPageDirty() Then
                SavePage()
                If (Me.State.MyBO.IsFamilyDirty) Then
                    Me.State.MyBO.Save()
                    ShowInfoMsgBox(Me.MSG_RECORD_SAVED_OK)
                    Me.State.searchDV = Nothing
                    PopulateListItemGrid()
                    ControlMgr.SetVisibleControl(Me, moListItemGrid, True)
                Else
                    ShowInfoMsgBox(Me.MSG_RECORD_NOT_SAVED)
                    PopulateListItemGrid()
                    Me.ReturnFromEditing()
                End If
            Else
                ShowInfoMsgBox(Me.MSG_RECORD_NOT_SAVED)
                PopulateListItemGrid()
                Me.ReturnFromEditing()
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
            Me.ReturnFromEditing(True)
            Me.State.searchDV = Nothing
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
                UserCompanyMultipleDrop.SelectedIndex = Me.ONE_ITEM
                Me.State.companyId = Me.UserCompanyMultipleDrop.SelectedGuid
                UserCompanyMultipleDrop.ChangeEnabledControlProperty(False)
            End If

            ListItemMultipleDrop.NothingSelected = True
            ListItemMultipleDrop.SetControl(True, ListItemMultipleDrop.MODES.NEW_MODE, True, LookupListNew.GetListId(ElitaPlusIdentity.Current.ActiveUser.LanguageId, True), ListItemMultipleDrop.NO_CAPTION, True)


        Catch ex As Exception
            Me.ErrControllerMaster.AddErrorAndShow(Assurant.ElitaPlus.Common.ErrorCodes.DB_ERROR)
        End Try


    End Sub

    

#End Region

#Region " Datagrid Related "

    Private Sub PopulateListItemGrid()
        Dim dv As DataView
        Try
            If (Me.State.searchDV Is Nothing) Then
                'Me.SetStateProperties()
                Me.State.searchDV = GetListItemsGridDataView()
            End If

            Me.State.searchDV.Sort = Me.State.SortExpression

            Me.moListItemGrid.AutoGenerateColumns = False
            Me.moListItemGrid.Columns(Me.GRID_COL_CODE_ALIAS).SortExpression = Me.State.MyBO.ListItemSearchDV.GRID_COL_CODE
            Me.moListItemGrid.Columns(Me.GRID_COL_DESCRIPTION_ALIAS).SortExpression = Me.State.MyBO.ListItemSearchDV.GRID_COL_DESCRIPTION
            Me.moListItemGrid.Columns(Me.GRID_COL_NEW_DESCRIPTION_ALIAS).SortExpression = Me.State.MyBO.ListItemSearchDV.GRID_COL_NEW_DESCRIPTION
            'DEF-1444: Errors on Code Mapping Screen. 
            SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.selectedGuid, Me.moListItemGrid, Me.State.PageIndex)
            'End of DEF-1444
            ' SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.DictItemId, Me.moListItemGrid, Me.State.PageIndex)
            SortAndBindGrid()

        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub

    Private Sub SortAndBindGrid()
        Me.State.PageIndex = Me.moListItemGrid.CurrentPageIndex
        Me.TranslateGridControls(moListItemGrid)
        moListItemGrid.DataSource = Me.State.searchDV
        HighLightSortColumn(moListItemGrid, Me.State.SortExpression)
        Me.moListItemGrid.DataBind()

        ControlMgr.SetVisibleControl(Me, moListItemGrid, Me.State.IsGridVisible)
        ControlMgr.SetVisibleControl(Me, trPageSize, Me.moListItemGrid.Visible)
        Me.lblRecordCount.Text = Me.State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)

        Session("recCount") = Me.State.searchDV.Count

        ' ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me, moListItemGrid)
    End Sub

    Private Function GetListItemsGridDataView() As DataView
        Dim bo As CodeMapping = New CodeMapping

        Me.State.searchDV = bo.AdminLoadListItems(ElitaPlusIdentity.Current.ActiveUser.LanguageId, Me.State.listId, Me.State.companyId)

        Me.State.searchDV.Sort = moListItemGrid.DataMember()
        moListItemGrid.DataSource = Me.State.searchDV

        Return (Me.State.searchDV)
    End Function

    Private Sub moDictionaryGrid_PageIndexChanged(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles moListItemGrid.PageIndexChanged
        Try
            Me.State.PageIndex = e.NewPageIndex
            Me.moListItemGrid.CurrentPageIndex = Me.State.PageIndex
            Me.PopulateListItemGrid()
            Me.moListItemGrid.SelectedIndex = Me.NO_ITEM_SELECTED_INDEX
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub


    Private Sub Grid_PageSizeChanged(ByVal source As Object, ByVal e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
        Try
            Me.State.PageSize = CType(cboPageSize.SelectedValue, Integer)
            moListItemGrid.CurrentPageIndex = NewCurrentPageIndex(moListItemGrid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
            Me.PopulateListItemGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub

    Private Sub Grid_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles moListItemGrid.ItemDataBound
        Dim itemType As ListItemType = CType(e.Item.ItemType, ListItemType)
        Dim dvRow As DataRowView = CType(e.Item.DataItem, DataRowView)
        Dim oTextBox As TextBox

        If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then
            With e.Item
                Me.PopulateControlFromBOProperty(.Cells(Me.GRID_COL_LIST_ITEM_ID), dvRow(CodeMapping.ListItemSearchDV.GRID_COL_LIST_ITEM_ID))
                'Me.PopulateControlFromBOProperty(.Cells(Me.GRID_COL_CODE_MAPPING_ID), dvRow(CodeMapping.ListItemSearchDV.GRID_COL_CODE_MAPPING_ID))


                oTextBox = CType(e.Item.Cells(Me.GRID_COL_NEW_DESCRIPTION_ALIAS).FindControl(NEW_DESCRIPTION_IN_GRID_CONTROL_NAME), TextBox)
                oTextBox.Attributes.Add("onchange", "setDirty()")
                'If e.Item.ItemIndex() = 0 Then
                '    SetFocus(oTextBox)
                'End If
                Me.PopulateControlFromBOProperty(oTextBox, dvRow(CodeMapping.ListItemSearchDV.GRID_COL_NEW_DESCRIPTION))
                Me.PopulateControlFromBOProperty(.Cells(Me.GRID_COL_CODE_ALIAS), dvRow(CodeMapping.ListItemSearchDV.GRID_COL_CODE))
                Me.PopulateControlFromBOProperty(.Cells(Me.GRID_COL_DESCRIPTION_ALIAS), dvRow(CodeMapping.ListItemSearchDV.GRID_COL_DESCRIPTION))
                Me.PopulateControlFromBOProperty(.Cells(Me.GRID_COL_CODE_MAPPING_ID), dvRow(CodeMapping.ListItemSearchDV.GRID_COL_CODE_MAPPING_ID))

            End With
        End If
        BaseItemBound(sender, e)

    End Sub


    Private Sub Grid_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles moListItemGrid.SortCommand
        Try
            If Me.State.SortExpression.StartsWith(e.SortExpression) Then
                If Me.State.SortExpression.EndsWith(" DESC") Then
                    Me.State.SortExpression = e.SortExpression
                Else
                    Me.State.SortExpression &= " DESC"
                End If
            Else
                Me.State.SortExpression = e.SortExpression
            End If
            'To handle the requirement of always going to the FIRST page on the Grid whenever the user switches the sorting criterion
            'Set the Me.State.selectedClaimId = Guid.Empty and set Me.State.PageIndex = 0
            'Me.State.listId = Guid.Empty
            'Me.State.PageIndex = 0

            Me.PopulateListItemGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try

    End Sub

    Public Sub ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles moListItemGrid.ItemCommand
       
    End Sub

    Public Sub ItemCreated(ByVal sender As Object, ByVal e As DataGridItemEventArgs)
        BaseItemCreated(sender, e)
    End Sub


#End Region


#Region "Controlling Logic"

    Private Sub ReturnFromEditing(Optional ByVal isSaveWithErrors As Boolean = False)

        If Not isSaveWithErrors Then
            If (Me.moListItemGrid.PageCount = 0) Then
                ControlMgr.SetVisibleControl(Me, moListItemGrid, False)
            Else
                ControlMgr.SetVisibleControl(Me, moListItemGrid, True)
            End If

            Me.State.PageIndex = moListItemGrid.CurrentPageIndex
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
        For i As Integer = 0 To Me.moListItemGrid.Items.Count - 1
            moListItemGrid.SelectedIndex = i
            Me.BindBOPropertyToGridHeader(Me.State.MyBO, "NewDescription", Me.moListItemGrid.Columns(Me.GRID_COL_NEW_DESCRIPTION_ALIAS))
            Me.ClearGridHeadersAndLabelsErrSign()
            If moListItemGrid.Items(i).Cells(Me.GRID_COL_CODE_MAPPING_ID).Text = "" Then
                CodeMappId = Guid.Empty
            Else
                CodeMappId = New Guid(moListItemGrid.Items(i).Cells(Me.GRID_COL_CODE_MAPPING_ID).Text)
            End If
            newCode = CType(Me.moListItemGrid.Items(Me.moListItemGrid.SelectedIndex).Cells(Me.GRID_COL_NEW_DESCRIPTION_ALIAS).FindControl(Me.NEW_DESCRIPTION_IN_GRID_CONTROL_NAME), TextBox).Text()
            'DEF-1444. Start
            If Me.State.searchDV.Item(i)(Me.GRID_COL_NEW_DESCRIPTION_ALIAS).ToString() <> newCode Then
                Me.State.selectedGuid = New Guid(moListItemGrid.Items(i).Cells(Me.GRID_COL_LIST_ITEM_ID).Text)
            End If
            'DEF-1444. End
            If Not newCode = "" Then
                If CodeMappId.Equals(Guid.Empty) Then
                    'DEF-1444. Start
                    If i = 0 Or Me.State.MyBO Is Nothing Or Not isFirstCodeMap Then
                        Me.State.MyBO = New CodeMapping
                        Me.PopulateBOProperty(Me.State.MyBO, "CompanyId", Me.State.companyId)
                        Me.PopulateBOProperty(Me.State.MyBO, "ListItemId", New Guid(moListItemGrid.Items(i).Cells(Me.GRID_COL_LIST_ITEM_ID).Text))
                        Me.PopulateBOProperty(Me.State.MyBO, "NewDescription", CType(Me.moListItemGrid.Items(Me.moListItemGrid.SelectedIndex).Cells(Me.GRID_COL_NEW_DESCRIPTION_ALIAS).FindControl(Me.NEW_DESCRIPTION_IN_GRID_CONTROL_NAME), TextBox).Text)
                        isFirstCodeMap = True
                        'DEF-1444. End
                    Else
                        otherBOs = New CodeMapping(Me.State.MyBO.MyDataset)
                        Me.PopulateBOProperty(otherBOs, "CompanyId", Me.State.companyId)
                        Me.PopulateBOProperty(otherBOs, "ListItemId", New Guid(moListItemGrid.Items(i).Cells(Me.GRID_COL_LIST_ITEM_ID).Text))
                        Me.PopulateBOProperty(otherBOs, "NewDescription", CType(Me.moListItemGrid.Items(Me.moListItemGrid.SelectedIndex).Cells(Me.GRID_COL_NEW_DESCRIPTION_ALIAS).FindControl(Me.NEW_DESCRIPTION_IN_GRID_CONTROL_NAME), TextBox).Text)
                        otherBOs.Save()
                    End If
                Else
                    'DEF-1444. Start
                    If i = 0 Or Me.State.MyBO Is Nothing Or Not isFirstCodeMap Then
                        Me.State.MyBO = New CodeMapping(CodeMappId)
                        Me.PopulateBOProperty(Me.State.MyBO, "NewDescription", CType(Me.moListItemGrid.Items(Me.moListItemGrid.SelectedIndex).Cells(Me.GRID_COL_NEW_DESCRIPTION_ALIAS).FindControl(Me.NEW_DESCRIPTION_IN_GRID_CONTROL_NAME), TextBox).Text)
                        isFirstCodeMap = True
                        'DEF-1444. End
                    Else
                        otherBOs = New CodeMapping(CodeMappId, Me.State.MyBO.MyDataset)
                        Me.PopulateBOProperty(otherBOs, "NewDescription", CType(Me.moListItemGrid.Items(Me.moListItemGrid.SelectedIndex).Cells(Me.GRID_COL_NEW_DESCRIPTION_ALIAS).FindControl(Me.NEW_DESCRIPTION_IN_GRID_CONTROL_NAME), TextBox).Text)
                        otherBOs.Save()
                    End If
                End If
            ElseIf newCode = "" Then
                If Not CodeMappId.Equals(Guid.Empty) Then
                    'DEF-1444. Start
                    If i = 0 Or Me.State.MyBO Is Nothing Or Not isFirstCodeMap Then
                        Me.State.MyBO = New CodeMapping(CodeMappId)
                        Me.State.MyBO.Delete()
                        isFirstCodeMap = True
                        'DEF-1444. End
                    Else
                        otherBOs = New CodeMapping(CodeMappId, Me.State.MyBO.MyDataset)
                        otherBOs.Delete()
                        otherBOs.Save()
                    End If
                End If
            End If
        Next

    End Sub

    Private Sub CreateBoFromGrid(ByVal index As Integer)
        
    End Sub

    Private Sub ValidatedDropdownSelections()

        Dim result As Boolean = True

        If Me.UserCompanyMultipleDrop.SelectedIndex = 0 Then
            Throw New GUIException(Message.MSG_COMPANY_IS_REQUIRED, Assurant.ElitaPlus.Common.ErrorCodes.GUI_COMPANY_REQUIRED)
        End If

        If Me.moMultipleColumnDDListItem.SelectedIndex = 0 Then
            Throw New GUIException(Message.MSG_LIST_ITEM_REQUIRED, Assurant.ElitaPlus.Common.ErrorCodes.GUI_LIST_ITEM_TYPE_IS_REQUIRED)
        End If

    End Sub


    Protected Sub CheckIfComingFromSaveConfirm()

        Dim confResponse As String = Me.HiddenSavePagePromptResponse.Value

        If Not confResponse Is Nothing AndAlso confResponse = Me.MSG_VALUE_YES Then
            If Me.State.ActionInProgress <> ElitaPlusPage.DetailPageCommand.BackOnErr Then
                SavePage()
                If (Me.State.MyBO.IsFamilyDirty) Then
                    Me.State.MyBO.Save()
                End If
            End If
            Select Case Me.State.ActionInProgress
                Case ElitaPlusPage.DetailPageCommand.Back
                    Me.ReturnToCallingPage()
                Case ElitaPlusPage.DetailPageCommand.New_
                    'Me.AddInfoMsg(Message.SAVE_RECORD_CONFIRMATION)
            End Select
        ElseIf Not confResponse Is Nothing AndAlso confResponse = Me.MSG_VALUE_NO Then
            Select Case Me.State.ActionInProgress
                Case ElitaPlusPage.DetailPageCommand.Back
                    Me.ReturnToCallingPage()
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
        Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
        Me.HiddenSavePagePromptResponse.Value = ""

    End Sub


    Function IsDataGPageDirty() As Boolean
        Dim Result As String = Me.HiddenIsPageDirty.Value

        Return Result.Equals("YES")
    End Function

    Private Sub EnableDisableButtons(ByVal isAnable As Boolean)

        ControlMgr.SetEnableControl(Me, moBtnCancel, isAnable)
        ControlMgr.SetEnableControl(Me, moBtnSave_WRITE, isAnable)


    End Sub

#End Region
    Private Sub ShowInfoMsgBox(ByVal strMsg As String, Optional ByVal Translate As Boolean = True)
        Dim translatedMsg As String = strMsg
        If Translate Then translatedMsg = TranslationBase.TranslateLabelOrMessage(strMsg)
        Dim sJavaScript As String
        sJavaScript = "<SCRIPT>" & Environment.NewLine
        sJavaScript &= "setTimeout(""showMessage('" & translatedMsg & "', '" & "AlertWindow" & "', '" & Me.MSG_BTN_OK & "', '" & Me.MSG_TYPE_INFO & "', '" & "null" & "')"", 0);" & Environment.NewLine
        sJavaScript &= "</SCRIPT>" & Environment.NewLine
        Me.RegisterStartupScript("ShowConfirmation", sJavaScript)
    End Sub

End Class