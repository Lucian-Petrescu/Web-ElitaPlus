
Namespace Translation
    Partial Class MaintainDictionaryForm
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
#End Region

#Region "Page State"
        Class MyState
            Public PageIndex As Integer = 0
            Public MyBO As DictItemTranslation
            Public MyCompanyLang As Company
            Public MyBOGroup As DictItemTranslation
            Public DictItemTransId As Guid
            Public LangId As Guid
            Public DescriptionMask As String = ""
            Public OrderByTrans As Boolean = True
            Public IsAfterSave As Boolean
            Public IsEditMode As Boolean
            Public IsGridVisible As Boolean
            Public searchDV As DataView = Nothing
            Public SortExpression As String = DictItemTranslation.TranslationSearchDV.GRID_COL_ENGLISH
            Public Canceling As Boolean
            Public selectedPageSize As Integer = DEFAULT_PAGE_SIZE
            Public ActionInProgress As DetailPageCommand = DetailPageCommand.Nothing_
            Public LastErrMsg As String
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

#Region "Constants"

        Public Const PAGETITLE As String = "DICTIONARY"
        Public Const PAGETAB As String = "TABLES"
        Private Const GRID_COL_TRANSLATION_ID As Integer = 0
        Private Const GRID_COL_TRANSLATION_ALIAS As Integer = 1
        Private Const GRID_COL_ENGLIS_ALIAS As Integer = 2

        Private Const TRANSLATION_IN_GRID_CONTROL_NAME As String = "moTranslationTextGrid"
        Private Const ENLISH_IN_GRID_CONTROL_NAME As String = "moEnglishLabelGrid"

        Private Const PAGE_SIZE As Integer = 10
        Private Const EMPTY As String = ""

        Private Const EDIT_COMMAND As String = "EditRecord"
        Private Const MSG_RECORD_SAVED_OK As String = "MSG_RECORD_SAVED_OK"
        Private Const MSG_RECORD_NOT_SAVED As String = "MSG_RECORD_NOT_SAVED" '"The record has not been saved because the current record has not been changed"
        Private Const LABEL_SELECT_DEALERCODE As String = "DEALER"
        Private Const LABEL_COMPANY As String = "COMPANY"
        Private Const NO_ROW_SELECTED_INDEX As Integer = -1

#End Region
#Region "Handlers-Init"
        Private Sub Page_PageReturn(ReturnFromUrl As String, ReturnPar As Object) Handles MyBase.PageReturn
            'Enable the Menu Navigation Back after returning from the child
            Try
                MenuEnabled = True
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub


        Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
            Try
                ErrControllerMaster.Clear_Hide()

                If Not Page.IsPostBack Then
                    SetFormTitle(PAGETITLE)
                    SetFormTab(PAGETAB)
                    ControlMgr.SetVisibleControl(Me, trPageSize, False)
                    SetDefaultButton(txtSearchTrans, btnSearch)
                    SetGridItemStyleColor(moDictionaryGrid)
                    MenuEnabled = False
                    PopulateDropdowns()
                    State.PageIndex = 0
                Else
                    If State.searchDV IsNot Nothing Then
                        moDictionaryGrid.DataSource = State.searchDV
                    End If
                    CheckIfComingFromSaveConfirm()
                End If
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
            ShowMissingTranslations(ErrControllerMaster)
        End Sub
        Protected Sub PopulateDropdowns()


            Try
                ' moCompanyMultipleDrop.NothingSelected = True
                moCompanyMultipleDrop.SetControl(True, moCompanyMultipleDrop.MODES.NEW_MODE, True, LookupListNew.GetUserCompaniesLookupList(), TranslationBase.TranslateLabelOrMessage(LABEL_COMPANY), True)
            Catch ex As Exception
                ErrControllerMaster.AddErrorAndShow(Assurant.ElitaPlus.Common.ErrorCodes.DB_ERROR)
            End Try

            'Me.BindListControlToDataView(Me.cboCompayDropDown, LookupListNew.GetUserCompaniesLookupList(), , , False)
            'Me.BindListControlToDataView(Me.moCompanyMultipleDrop., LookupListNew.GetUserCompaniesLookupList())
        End Sub
#End Region
#Region "Handlers"
        '
#End Region
#Region "Controlling Logic"

        Private Sub ReturnFromEditing(Optional ByVal isSaveWithErrors As Boolean = False)

            If Not isSaveWithErrors Then

                moDictionaryGrid.EditItemIndex = NO_ROW_SELECTED_INDEX

                If (moDictionaryGrid.PageCount = 0) Then
                    'if returning to the "1st time in" screen
                    ControlMgr.SetVisibleControl(Me, moDictionaryGrid, False)
                Else
                    ControlMgr.SetVisibleControl(Me, moDictionaryGrid, True)
                End If

                State.IsEditMode = False
                PopulateTranslationGrid()
                State.PageIndex = moDictionaryGrid.CurrentPageIndex
                'SetButtonsState()
            End If
        End Sub

        Private Sub SetFocusOnEditableFieldInGrid(grid As DataGrid, cellPosition As Integer, itemIndex As Integer, IsNewRowAndSingleCountry As Boolean)
            'Set focus on the Description TextBox for the EditItemIndex row
            Dim ctrlDealer As TextBox = CType(grid.Items(itemIndex).Cells(GRID_COL_TRANSLATION_ALIAS).FindControl(TRANSLATION_IN_GRID_CONTROL_NAME), TextBox)
            SetFocus(ctrlDealer)
            If cellPosition = GRID_COL_TRANSLATION_ALIAS Then
                SetFocus(ctrlDealer)
                ctrlDealer.Enabled = True
            Else
                Dim ctrlDescription As TextBox = CType(grid.Items(itemIndex).Cells(GRID_COL_ENGLIS_ALIAS).FindControl(ENLISH_IN_GRID_CONTROL_NAME), TextBox)
                SetFocus(ctrlDescription)
                ctrlDealer.Enabled = False
            End If
        End Sub

        Protected Sub BindBoPropertiesToGridHeaders(bo As DictItemTranslation)

            BindBOPropertyToGridHeader(bo, "Translation", moDictionaryGrid.Columns(GRID_COL_TRANSLATION_ALIAS))
            ClearGridHeadersAndLabelsErrSign()
        End Sub

        Private Sub PopulateBOFromForm(bo As DictItemTranslation)
            Try
                bo.Translation = CType(moDictionaryGrid.Items(moDictionaryGrid.SelectedIndex).Cells(GRID_COL_TRANSLATION_ALIAS).FindControl(TRANSLATION_IN_GRID_CONTROL_NAME), TextBox).Text
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try

        End Sub

        Private Sub SetButtonsState()

            If (State.IsEditMode) Then
                ControlMgr.SetVisibleControl(Me, moBtnSave_WRITE, True)
                ControlMgr.SetVisibleControl(Me, moBtnCancel, True)
                ControlMgr.SetEnableControl(Me, btnSearch, False)
                ControlMgr.SetEnableControl(Me, btnClear, False)
                MenuEnabled = False
                If (cboPageSize.Visible) Then
                    ControlMgr.SetEnableControl(Me, cboPageSize, False)
                End If
                SetGridControls(moDictionaryGrid, False)
            Else
                ControlMgr.SetVisibleControl(Me, moBtnSave_WRITE, False)
                ControlMgr.SetVisibleControl(Me, moBtnCancel, False)
                ControlMgr.SetEnableControl(Me, btnSearch, True)
                ControlMgr.SetEnableControl(Me, btnClear, True)
                MenuEnabled = True
                If (cboPageSize.Visible) Then
                    ControlMgr.SetEnableControl(Me, cboPageSize, True)
                End If
            End If

        End Sub

        Private Sub SetSortOption()

            Dim mIndex As Integer = rdbViewType.SelectedIndex

            If mIndex = 1 Then
                State.SortExpression = DictItemTranslation.TranslationSearchDV.GRID_COL_ENGLISH
                State.OrderByTrans = False
            Else
                State.SortExpression = DictItemTranslation.TranslationSearchDV.GRID_COL_TRANSLATION
                State.OrderByTrans = True
            End If
            PopulateTranslationGrid()
        End Sub

        Private Sub CreateBoFromGrid(index As Integer)
            Dim DictionaryId As Guid

            moDictionaryGrid.SelectedIndex = index
            DictionaryId = New Guid(moDictionaryGrid.Items(index).Cells(GRID_COL_TRANSLATION_ID).Text)
            If State.MyBO Is Nothing Then
                State.MyBO = New DictItemTranslation(DictionaryId)
                BindBoPropertiesToGridHeaders(State.MyBO)
                PopulateBOFromForm(State.MyBO)
            Else
                State.MyBOGroup = New DictItemTranslation(DictionaryId, State.MyBO.GetDataset)
                PopulateBOFromForm(State.MyBOGroup)
            End If

        End Sub

        Private Sub SavePage()
            Dim index As Integer = 0
            Dim DictionaryId As Guid
            Dim selectedDS As DataSet
            Dim totItems As Integer = moDictionaryGrid.Items.Count

            For index = 0 To totItems - 1
                CreateBoFromGrid(index)
            Next
        End Sub

        Function IsDataGPageDirty() As Boolean
            Dim Result As String = HiddenIsPageDirty.Value

            Return Result.Equals("YES")
        End Function

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
                        ReturnToTabHomePage()
                    Case ElitaPlusPage.DetailPageCommand.New_
                        'Me.AddInfoMsg(Message.SAVE_RECORD_CONFIRMATION)
                        'Me.State.isNew = True
                        'Me.PopulateGrid()
                End Select
            ElseIf confResponse IsNot Nothing AndAlso confResponse = MSG_VALUE_NO Then
                HiddenIsPageDirty.Value = "NO"
                Select Case State.ActionInProgress
                    Case ElitaPlusPage.DetailPageCommand.Back
                        ReturnToTabHomePage()
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

#End Region

#Region "Private Methods"


#End Region
#Region "Button Management"
        Private Sub BtnSearch_Click(sender As System.Object, e As System.EventArgs) Handles btnSearch.Click
            Try
                State.IsGridVisible = True
                State.searchDV = Nothing
                PopulateTranslationGrid()
                State.PageIndex = moDictionaryGrid.CurrentPageIndex
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

        Private Sub btnClear_Click(sender As System.Object, e As System.EventArgs) Handles btnClear.Click
            Try
                txtSearchTrans.Text = String.Empty

                'Update Page State
                With State
                    .DescriptionMask = Nothing
                End With
                PopulateTranslationGrid()
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub


        Private Sub moBtnSave_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles moBtnSave_WRITE.Click
            Try
                SavePage()
                If (State.MyBO.IsFamilyDirty) Then
                    State.MyBO.Save()
                    State.IsAfterSave = True
                    ShowInfoMsgBox(MSG_RECORD_SAVED_OK)
                    State.searchDV = Nothing
                    State.MyBO = Nothing
                    HiddenIsPageDirty.Value = EMPTY
                    ReturnFromEditing()
                Else
                    ShowInfoMsgBox(MSG_RECORD_NOT_SAVED)
                    ReturnFromEditing()
                End If
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
                ReturnFromEditing(True)
            End Try

        End Sub

        Private Sub moBtnCancel_Click(sender As System.Object, e As System.EventArgs) Handles moBtnCancel.Click
            Try
                moDictionaryGrid.SelectedIndex = NO_ITEM_SELECTED_INDEX
                State.Canceling = True
                HiddenIsPageDirty.Value = EMPTY
                ReturnFromEditing()
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

        Public Sub Index_Changed(sender As System.Object, e As System.EventArgs) Handles rdbViewType.SelectedIndexChanged
            Try
                SetSortOption()
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

        Private Sub btnBack_Click(sender As Object, e As System.EventArgs) Handles btnBack.Click
            Try
                If IsDataGPageDirty() Then
                    DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, _
                                                HiddenSavePagePromptResponse)
                    State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
                Else
                    ReturnToTabHomePage()
                End If

            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
                'Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR
                DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSavePagePromptResponse)
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
                State.LastErrMsg = ErrControllerMaster.Text
            End Try
        End Sub

#End Region

#Region " Datagrid Related "

        Private Sub PopulateTranslationGrid()
            Dim dv As DataView
            Try
                If (State.searchDV Is Nothing) Then
                    SetStateProperties()
                    State.searchDV = GetTranslationGridDataView()
                End If

                If (State.IsAfterSave) Then
                    State.IsAfterSave = False
                    SetPageAndSelectedIndexFromGuid(State.searchDV, State.DictItemTransId, moDictionaryGrid, State.PageIndex)
                ElseIf (State.IsEditMode) Then
                    SetPageAndSelectedIndexFromGuid(State.searchDV, State.DictItemTransId, moDictionaryGrid, State.PageIndex, State.IsEditMode)
                End If

                State.searchDV.Sort = State.SortExpression

                moDictionaryGrid.AutoGenerateColumns = False


                If Not moCompanyMultipleDrop.SelectedCode = "" Then

                    moDictionaryGrid.Columns(1).HeaderText = LookupListNew.GetDescriptionFromId(LookupListNew.LK_LANGUAGES, State.MyCompanyLang.LanguageId)
                Else
                    moDictionaryGrid.Columns(1).HeaderText = "Translation"
                End If



                moDictionaryGrid.Columns(GRID_COL_TRANSLATION_ALIAS).SortExpression = State.MyBO.TranslationSearchDV.GRID_COL_TRANSLATION
                moDictionaryGrid.Columns(GRID_COL_ENGLIS_ALIAS).SortExpression = State.MyBO.TranslationSearchDV.GRID_COL_ENGLISH

                SetPageAndSelectedIndexFromGuid(State.searchDV, State.DictItemTransId, moDictionaryGrid, State.PageIndex)
                SortAndBindGrid()

            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

        Private Sub SortAndBindGrid()
            State.PageIndex = moDictionaryGrid.CurrentPageIndex
            TranslateGridControls(moDictionaryGrid)
            moDictionaryGrid.DataSource = State.searchDV
            HighLightSortColumn(moDictionaryGrid, State.SortExpression)
            moDictionaryGrid.DataBind()

            ControlMgr.SetVisibleControl(Me, moDictionaryGrid, State.IsGridVisible)
            ControlMgr.SetVisibleControl(Me, trPageSize, moDictionaryGrid.Visible)

            Session("recCount") = State.searchDV.Count

            ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me, moDictionaryGrid)

            If State.searchDV.Count > 0 Then

                If moDictionaryGrid.Visible Then
                    lblRecordCount.Text = State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                End If
            Else
                If moDictionaryGrid.Visible Then
                    lblRecordCount.Text = State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                End If
            End If

        End Sub

        Private Function GetTranslationGridDataView() As DataView
            Dim moCompanyLangId As Guid

            If Not moCompanyMultipleDrop.SelectedCode = "" Then
                State.MyCompanyLang = New Company(moCompanyMultipleDrop.SelectedGuid)
                moCompanyLangId = State.MyCompanyLang.LanguageId
            End If

            With State
                State.searchDV = State.MyBO.GetTranslationList(moCompanyLangId, State.DescriptionMask.ToUpper, _
                                                                             State.OrderByTrans)
            End With

            State.searchDV.Sort = moDictionaryGrid.DataMember()
            moDictionaryGrid.DataSource = State.searchDV

            Return (State.searchDV)
        End Function

        Private Sub SetStateProperties()

            State.LangId = ElitaPlusIdentity.Current.ActiveUser.Company.LanguageId
            State.DescriptionMask = txtSearchTrans.Text

        End Sub


        Private Sub moDictionaryGrid_PageIndexChanged(source As System.Object, e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles moDictionaryGrid.PageIndexChanged
            Try
                If (Not (State.IsEditMode)) Then
                    State.PageIndex = e.NewPageIndex
                    moDictionaryGrid.CurrentPageIndex = State.PageIndex
                    PopulateTranslationGrid()
                    moDictionaryGrid.SelectedIndex = NO_ITEM_SELECTED_INDEX
                End If
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

        Private Sub Grid_PageSizeChanged(source As Object, e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
            Try
                moDictionaryGrid.CurrentPageIndex = NewCurrentPageIndex(moDictionaryGrid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
                PopulateTranslationGrid()
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

        Private Sub Grid_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles moDictionaryGrid.ItemDataBound
            Dim itemType As ListItemType = CType(e.Item.ItemType, ListItemType)
            Dim dvRow As DataRowView = CType(e.Item.DataItem, DataRowView)
            Dim str As String
            Dim oTextBox As TextBox
            Dim oLabel As Label

            If itemType = ListItemType.Item OrElse itemType = ListItemType.AlternatingItem OrElse itemType = ListItemType.SelectedItem Then
                With e.Item
                    PopulateControlFromBOProperty(.Cells(GRID_COL_TRANSLATION_ID), dvRow(DictItemTranslation.TranslationSearchDV.GRID_COL_ID))
                    oTextBox = CType(e.Item.Cells(GRID_COL_TRANSLATION_ALIAS).FindControl(TRANSLATION_IN_GRID_CONTROL_NAME), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    If e.Item.ItemIndex() = 0 Then
                        SetFocus(oTextBox)
                    End If
                    PopulateControlFromBOProperty(oTextBox, dvRow(DictItemTranslation.TranslationSearchDV.GRID_COL_TRANSLATION))

                    oLabel = CType(e.Item.Cells(GRID_COL_ENGLIS_ALIAS).FindControl(ENLISH_IN_GRID_CONTROL_NAME), Label)

                    'Replace Leading spaces with HTML Escapes
                    str = dvRow(DictItemTranslation.TranslationSearchDV.GRID_COL_ENGLISH).ToString.ToUpper
                    For i As Integer = 0 To str.Length - 1
                        If str.Chars(i) = " " Then
                            str = If(i > 0, str.Substring(0, i - 1), "").ToString + "&nbsp;" + If(i < str.Length - 1, str.Substring(i + 1), "").ToString
                        Else
                            Exit For
                        End If
                    Next
                    PopulateControlFromBOProperty(oLabel, str)
                End With
            End If
            BaseItemBound(sender, e)

        End Sub

        Private Sub Grid_SortCommand(source As Object, e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles moDictionaryGrid.SortCommand
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
                State.DictItemTransId = Guid.Empty
                State.PageIndex = 0

                PopulateTranslationGrid()
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try

        End Sub

        'The pencil or the trash icon was clicked
        Sub ItemCommand(source As System.Object, e As System.Web.UI.WebControls.DataGridCommandEventArgs)

        End Sub

        Public Sub ItemCreated(sender As Object, e As DataGridItemEventArgs)
            BaseItemCreated(sender, e)
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

End Namespace

