
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
        Private Sub Page_PageReturn(ByVal ReturnFromUrl As String, ByVal ReturnPar As Object) Handles MyBase.PageReturn
            'Enable the Menu Navigation Back after returning from the child
            Try
                Me.MenuEnabled = True
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub


        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            Try
                Me.ErrControllerMaster.Clear_Hide()

                If Not Page.IsPostBack Then
                    Me.SetFormTitle(PAGETITLE)
                    Me.SetFormTab(PAGETAB)
                    ControlMgr.SetVisibleControl(Me, trPageSize, False)
                    Me.SetDefaultButton(Me.txtSearchTrans, Me.btnSearch)
                    Me.SetGridItemStyleColor(Me.moDictionaryGrid)
                    Me.MenuEnabled = False
                    PopulateDropdowns()
                    Me.State.PageIndex = 0
                Else
                    If Not Me.State.searchDV Is Nothing Then
                        moDictionaryGrid.DataSource = Me.State.searchDV
                    End If
                    CheckIfComingFromSaveConfirm()
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
            Me.ShowMissingTranslations(ErrControllerMaster)
        End Sub
        Protected Sub PopulateDropdowns()


            Try
                ' moCompanyMultipleDrop.NothingSelected = True
                moCompanyMultipleDrop.SetControl(True, moCompanyMultipleDrop.MODES.NEW_MODE, True, LookupListNew.GetUserCompaniesLookupList(), TranslationBase.TranslateLabelOrMessage(LABEL_COMPANY), True)
            Catch ex As Exception
                Me.ErrControllerMaster.AddErrorAndShow(Assurant.ElitaPlus.Common.ErrorCodes.DB_ERROR)
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

                If (Me.moDictionaryGrid.PageCount = 0) Then
                    'if returning to the "1st time in" screen
                    ControlMgr.SetVisibleControl(Me, moDictionaryGrid, False)
                Else
                    ControlMgr.SetVisibleControl(Me, moDictionaryGrid, True)
                End If

                Me.State.IsEditMode = False
                Me.PopulateTranslationGrid()
                Me.State.PageIndex = moDictionaryGrid.CurrentPageIndex
                'SetButtonsState()
            End If
        End Sub

        Private Sub SetFocusOnEditableFieldInGrid(ByVal grid As DataGrid, ByVal cellPosition As Integer, ByVal itemIndex As Integer, ByVal IsNewRowAndSingleCountry As Boolean)
            'Set focus on the Description TextBox for the EditItemIndex row
            Dim ctrlDealer As TextBox = CType(grid.Items(itemIndex).Cells(Me.GRID_COL_TRANSLATION_ALIAS).FindControl(Me.TRANSLATION_IN_GRID_CONTROL_NAME), TextBox)
            SetFocus(ctrlDealer)
            If cellPosition = Me.GRID_COL_TRANSLATION_ALIAS Then
                SetFocus(ctrlDealer)
                ctrlDealer.Enabled = True
            Else
                Dim ctrlDescription As TextBox = CType(grid.Items(itemIndex).Cells(Me.GRID_COL_ENGLIS_ALIAS).FindControl(ENLISH_IN_GRID_CONTROL_NAME), TextBox)
                SetFocus(ctrlDescription)
                ctrlDealer.Enabled = False
            End If
        End Sub

        Protected Sub BindBoPropertiesToGridHeaders(ByVal bo As DictItemTranslation)

            Me.BindBOPropertyToGridHeader(bo, "Translation", Me.moDictionaryGrid.Columns(Me.GRID_COL_TRANSLATION_ALIAS))
            Me.ClearGridHeadersAndLabelsErrSign()
        End Sub

        Private Sub PopulateBOFromForm(ByVal bo As DictItemTranslation)
            Try
                bo.Translation = CType(Me.moDictionaryGrid.Items(Me.moDictionaryGrid.SelectedIndex).Cells(Me.GRID_COL_TRANSLATION_ALIAS).FindControl(Me.TRANSLATION_IN_GRID_CONTROL_NAME), TextBox).Text
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try

        End Sub

        Private Sub SetButtonsState()

            If (Me.State.IsEditMode) Then
                ControlMgr.SetVisibleControl(Me, Me.moBtnSave_WRITE, True)
                ControlMgr.SetVisibleControl(Me, moBtnCancel, True)
                ControlMgr.SetEnableControl(Me, btnSearch, False)
                ControlMgr.SetEnableControl(Me, btnClear, False)
                Me.MenuEnabled = False
                If (Me.cboPageSize.Visible) Then
                    ControlMgr.SetEnableControl(Me, cboPageSize, False)
                End If
                Me.SetGridControls(Me.moDictionaryGrid, False)
            Else
                ControlMgr.SetVisibleControl(Me, moBtnSave_WRITE, False)
                ControlMgr.SetVisibleControl(Me, moBtnCancel, False)
                ControlMgr.SetEnableControl(Me, btnSearch, True)
                ControlMgr.SetEnableControl(Me, btnClear, True)
                Me.MenuEnabled = True
                If (Me.cboPageSize.Visible) Then
                    ControlMgr.SetEnableControl(Me, cboPageSize, True)
                End If
            End If

        End Sub

        Private Sub SetSortOption()

            Dim mIndex As Integer = rdbViewType.SelectedIndex

            If mIndex = 1 Then
                Me.State.SortExpression = DictItemTranslation.TranslationSearchDV.GRID_COL_ENGLISH
                Me.State.OrderByTrans = False
            Else
                Me.State.SortExpression = DictItemTranslation.TranslationSearchDV.GRID_COL_TRANSLATION
                Me.State.OrderByTrans = True
            End If
            Me.PopulateTranslationGrid()
        End Sub

        Private Sub CreateBoFromGrid(ByVal index As Integer)
            Dim DictionaryId As Guid

            moDictionaryGrid.SelectedIndex = index
            DictionaryId = New Guid(moDictionaryGrid.Items(index).Cells(Me.GRID_COL_TRANSLATION_ID).Text)
            If Me.State.MyBO Is Nothing Then
                Me.State.MyBO = New DictItemTranslation(DictionaryId)
                BindBoPropertiesToGridHeaders(Me.State.MyBO)
                PopulateBOFromForm(Me.State.MyBO)
            Else
                Me.State.MyBOGroup = New DictItemTranslation(DictionaryId, Me.State.MyBO.GetDataset)
                PopulateBOFromForm(Me.State.MyBOGroup)
            End If

        End Sub

        Private Sub SavePage()
            Dim index As Integer = 0
            Dim DictionaryId As Guid
            Dim selectedDS As DataSet
            Dim totItems As Integer = Me.moDictionaryGrid.Items.Count

            For index = 0 To totItems - 1
                CreateBoFromGrid(index)
            Next
        End Sub

        Function IsDataGPageDirty() As Boolean
            Dim Result As String = Me.HiddenIsPageDirty.Value

            Return Result.Equals("YES")
        End Function

        Protected Sub CheckIfComingFromSaveConfirm()

            Dim confResponse As String = Me.HiddenSavePagePromptResponse.Value

            If Not confResponse Is Nothing AndAlso confResponse = Me.MSG_VALUE_YES Then
                If Me.State.ActionInProgress <> ElitaPlusPage.DetailPageCommand.BackOnErr Then
                    Me.SavePage()
                    If (Me.State.MyBO.IsFamilyDirty) Then
                        Me.State.MyBO.Save()
                    End If
                End If
                Select Case Me.State.ActionInProgress
                    Case ElitaPlusPage.DetailPageCommand.Back
                        Me.ReturnToTabHomePage()
                    Case ElitaPlusPage.DetailPageCommand.New_
                        'Me.AddInfoMsg(Message.SAVE_RECORD_CONFIRMATION)
                        'Me.State.isNew = True
                        'Me.PopulateGrid()
                End Select
            ElseIf Not confResponse Is Nothing AndAlso confResponse = Me.MSG_VALUE_NO Then
                Me.HiddenIsPageDirty.Value = "NO"
                Select Case Me.State.ActionInProgress
                    Case ElitaPlusPage.DetailPageCommand.Back
                        Me.ReturnToTabHomePage()
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

#End Region

#Region "Private Methods"


#End Region
#Region "Button Management"
        Private Sub BtnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
            Try
                Me.State.IsGridVisible = True
                Me.State.searchDV = Nothing
                PopulateTranslationGrid()
                Me.State.PageIndex = moDictionaryGrid.CurrentPageIndex
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

        Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
            Try
                Me.txtSearchTrans.Text = String.Empty

                'Update Page State
                With Me.State
                    .DescriptionMask = Nothing
                End With
                Me.PopulateTranslationGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub


        Private Sub moBtnSave_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles moBtnSave_WRITE.Click
            Try
                SavePage()
                If (Me.State.MyBO.IsFamilyDirty) Then
                    Me.State.MyBO.Save()
                    Me.State.IsAfterSave = True
                    ShowInfoMsgBox(Me.MSG_RECORD_SAVED_OK)
                    Me.State.searchDV = Nothing
                    Me.State.MyBO = Nothing
                    Me.HiddenIsPageDirty.Value = EMPTY
                    Me.ReturnFromEditing()
                Else
                    ShowInfoMsgBox(Me.MSG_RECORD_NOT_SAVED)
                    Me.ReturnFromEditing()
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
                Me.ReturnFromEditing(True)
            End Try

        End Sub

        Private Sub moBtnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles moBtnCancel.Click
            Try
                Me.moDictionaryGrid.SelectedIndex = Me.NO_ITEM_SELECTED_INDEX
                Me.State.Canceling = True
                Me.HiddenIsPageDirty.Value = EMPTY
                ReturnFromEditing()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

        Public Sub Index_Changed(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbViewType.SelectedIndexChanged
            Try
                SetSortOption()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

        Private Sub btnBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.Click
            Try
                If Me.IsDataGPageDirty() Then
                    Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, _
                                                Me.HiddenSavePagePromptResponse)
                    Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
                Else
                    Me.ReturnToTabHomePage()
                End If

            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                Me.HandleErrors(ex, ErrControllerMaster)
                'Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR
                Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Me.HiddenSavePagePromptResponse)
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
                Me.State.LastErrMsg = Me.ErrControllerMaster.Text
            End Try
        End Sub

#End Region

#Region " Datagrid Related "

        Private Sub PopulateTranslationGrid()
            Dim dv As DataView
            Try
                If (Me.State.searchDV Is Nothing) Then
                    Me.SetStateProperties()
                    Me.State.searchDV = GetTranslationGridDataView()
                End If

                If (Me.State.IsAfterSave) Then
                    Me.State.IsAfterSave = False
                    Me.SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.DictItemTransId, Me.moDictionaryGrid, Me.State.PageIndex)
                ElseIf (Me.State.IsEditMode) Then
                    Me.SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.DictItemTransId, Me.moDictionaryGrid, Me.State.PageIndex, Me.State.IsEditMode)
                End If

                Me.State.searchDV.Sort = Me.State.SortExpression

                Me.moDictionaryGrid.AutoGenerateColumns = False


                If Not moCompanyMultipleDrop.SelectedCode = "" Then

                    Me.moDictionaryGrid.Columns(1).HeaderText = LookupListNew.GetDescriptionFromId(LookupListNew.LK_LANGUAGES, Me.State.MyCompanyLang.LanguageId)
                Else
                    Me.moDictionaryGrid.Columns(1).HeaderText = "Translation"
                End If



                Me.moDictionaryGrid.Columns(Me.GRID_COL_TRANSLATION_ALIAS).SortExpression = Me.State.MyBO.TranslationSearchDV.GRID_COL_TRANSLATION
                Me.moDictionaryGrid.Columns(Me.GRID_COL_ENGLIS_ALIAS).SortExpression = Me.State.MyBO.TranslationSearchDV.GRID_COL_ENGLISH

                SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.DictItemTransId, Me.moDictionaryGrid, Me.State.PageIndex)
                SortAndBindGrid()

            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

        Private Sub SortAndBindGrid()
            Me.State.PageIndex = Me.moDictionaryGrid.CurrentPageIndex
            Me.TranslateGridControls(moDictionaryGrid)
            moDictionaryGrid.DataSource = Me.State.searchDV
            HighLightSortColumn(moDictionaryGrid, Me.State.SortExpression)
            Me.moDictionaryGrid.DataBind()

            ControlMgr.SetVisibleControl(Me, moDictionaryGrid, Me.State.IsGridVisible)
            ControlMgr.SetVisibleControl(Me, trPageSize, Me.moDictionaryGrid.Visible)

            Session("recCount") = Me.State.searchDV.Count

            ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me, moDictionaryGrid)

            If Me.State.searchDV.Count > 0 Then

                If Me.moDictionaryGrid.Visible Then
                    Me.lblRecordCount.Text = Me.State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                End If
            Else
                If Me.moDictionaryGrid.Visible Then
                    Me.lblRecordCount.Text = Me.State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                End If
            End If

        End Sub

        Private Function GetTranslationGridDataView() As DataView
            Dim moCompanyLangId As Guid

            If Not moCompanyMultipleDrop.SelectedCode = "" Then
                Me.State.MyCompanyLang = New Company(moCompanyMultipleDrop.SelectedGuid)
                moCompanyLangId = Me.State.MyCompanyLang.LanguageId
            End If

            With State
                Me.State.searchDV = Me.State.MyBO.GetTranslationList(moCompanyLangId, Me.State.DescriptionMask.ToUpper, _
                                                                             Me.State.OrderByTrans)
            End With

            Me.State.searchDV.Sort = moDictionaryGrid.DataMember()
            moDictionaryGrid.DataSource = Me.State.searchDV

            Return (Me.State.searchDV)
        End Function

        Private Sub SetStateProperties()

            Me.State.LangId = ElitaPlusIdentity.Current.ActiveUser.Company.LanguageId
            Me.State.DescriptionMask = txtSearchTrans.Text

        End Sub


        Private Sub moDictionaryGrid_PageIndexChanged(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles moDictionaryGrid.PageIndexChanged
            Try
                If (Not (Me.State.IsEditMode)) Then
                    Me.State.PageIndex = e.NewPageIndex
                    Me.moDictionaryGrid.CurrentPageIndex = Me.State.PageIndex
                    Me.PopulateTranslationGrid()
                    Me.moDictionaryGrid.SelectedIndex = Me.NO_ITEM_SELECTED_INDEX
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

        Private Sub Grid_PageSizeChanged(ByVal source As Object, ByVal e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
            Try
                moDictionaryGrid.CurrentPageIndex = NewCurrentPageIndex(moDictionaryGrid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
                Me.PopulateTranslationGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

        Private Sub Grid_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles moDictionaryGrid.ItemDataBound
            Dim itemType As ListItemType = CType(e.Item.ItemType, ListItemType)
            Dim dvRow As DataRowView = CType(e.Item.DataItem, DataRowView)
            Dim str As String
            Dim oTextBox As TextBox
            Dim oLabel As Label

            If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then
                With e.Item
                    Me.PopulateControlFromBOProperty(.Cells(Me.GRID_COL_TRANSLATION_ID), dvRow(DictItemTranslation.TranslationSearchDV.GRID_COL_ID))
                    oTextBox = CType(e.Item.Cells(Me.GRID_COL_TRANSLATION_ALIAS).FindControl(TRANSLATION_IN_GRID_CONTROL_NAME), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    If e.Item.ItemIndex() = 0 Then
                        SetFocus(oTextBox)
                    End If
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(DictItemTranslation.TranslationSearchDV.GRID_COL_TRANSLATION))

                    oLabel = CType(e.Item.Cells(Me.GRID_COL_ENGLIS_ALIAS).FindControl(ENLISH_IN_GRID_CONTROL_NAME), Label)

                    'Replace Leading spaces with HTML Escapes
                    str = dvRow(DictItemTranslation.TranslationSearchDV.GRID_COL_ENGLISH).ToString.ToUpper
                    For i As Integer = 0 To str.Length - 1
                        If str.Chars(i) = " " Then
                            str = If(i > 0, str.Substring(0, i - 1), "").ToString + "&nbsp;" + If(i < str.Length - 1, str.Substring(i + 1), "").ToString
                        Else
                            Exit For
                        End If
                    Next
                    Me.PopulateControlFromBOProperty(oLabel, str)
                End With
            End If
            BaseItemBound(sender, e)

        End Sub

        Private Sub Grid_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles moDictionaryGrid.SortCommand
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
                Me.State.DictItemTransId = Guid.Empty
                Me.State.PageIndex = 0

                Me.PopulateTranslationGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try

        End Sub

        'The pencil or the trash icon was clicked
        Sub ItemCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs)

        End Sub

        Public Sub ItemCreated(ByVal sender As Object, ByVal e As DataGridItemEventArgs)
            BaseItemCreated(sender, e)
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

End Namespace

