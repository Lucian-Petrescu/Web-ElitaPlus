Namespace Translation
    Partial Class AdminMaintainLabelTranslationForm
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
            Public MyBO As DictionaryItem
            Public oDictItemTrans As DictItemTranslation
            Public oLabelExtended As Label_Extended
            Public DictItemId As Guid
            'Public IsAfterSave As Boolean
            Public IsEditMode As Boolean
            Public isNew As Boolean = False
            Public searchDV As DataView = Nothing
            Public SortExpression As String = DictItemTranslation.TranslationSearchDV.GRID_COL_ENGLISH
            'Public Canceling As Boolean
            Public ActionInProgress As DetailPageCommand = DetailPageCommand.Nothing_
            'Public LastErrMsg As String
        End Class

        Public Sub New()
            MyBase.New(New MyState)
        End Sub

        Protected Shadows ReadOnly Property State() As MyState
            Get
                Return CType(MyBase.State, MyState)
            End Get
        End Property

        Private Sub Page_PageCall(ByVal CallFromUrl As String, ByVal CallingPar As Object) Handles MyBase.PageCall
            Try
                If Not Me.CallingParameters Is Nothing Then
                    'Get the id from the parent
                    Me.State.MyBO = New DictionaryItem(CType(Me.CallingParameters, Guid))
                Else
                    Me.State.isNew = True
                    Me.State.MyBO = New DictionaryItem
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try

        End Sub

#End Region

#Region "Constants"

        Public Const URL As String = "AdminMaintainLabelTranslationForm.aspx"
        Public Const PAGETITLE As String = "LABEL TRANSLATION"
        Public Const PAGETAB As String = "ADMIN"
        Private Const GRID_COL_DICT_ITEM_ID As Integer = 0
        Private Const GRID_COL_TRANSLATION_ALIAS As Integer = 1
        Private Const GRID_COL_LANGUAGE_ALIAS As Integer = 2

        Private Const TRANSLATION_IN_GRID_CONTROL_NAME As String = "moTranslationTextGrid"
        Private Const LANGUAGE_IN_GRID_CONTROL_NAME As String = "moLanguageLabelGrid"

        'Private Const PAGE_SIZE As Integer = 10
        Private Const EMPTY As String = ""

        Private Const EDIT_COMMAND As String = "EditRecord"
        Private Const MSG_RECORD_SAVED_OK As String = "MSG_RECORD_SAVED_OK"
        Private Const MSG_RECORD_NOT_SAVED As String = "MSG_RECORD_NOT_SAVED" '"The record has not been saved because the current record has not been changed"
        Private Const LABEL_SELECT_DEALERCODE As String = "DEALER"

        Private Const NO_ROW_SELECTED_INDEX As Integer = -1

#End Region
#Region "Handlers-Init"
        
        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            Try
                Me.ErrControllerMaster.Clear_Hide()
                If Not Page.IsPostBack Then
                    Me.SetFormTitle(PAGETITLE)
                    Me.SetFormTab(PAGETAB)
                    ControlMgr.SetVisibleControl(Me, trPageSize, False)
                    Me.SetGridItemStyleColor(Me.moDictionaryGrid)
                    'Me.MenuEnabled = False
                    If Me.State.isNew Then
                        ControlMgr.SetVisibleControl(Me, moDictionaryGrid, False)
                    End If
                    PopulateTranslationGrid()
                    Me.State.oLabelExtended = Me.State.MyBO.AssociatedLabel(Me.State.MyBO.Id, True, Me.State.isNew)
                    PopulateFormFromBO()
                    Me.State.PageIndex = 0
                Else
                    If Not Me.State.searchDV Is Nothing Then
                        moDictionaryGrid.DataSource = Me.State.searchDV
                    End If
                    If Me.State.oLabelExtended Is Nothing Then
                        Me.State.oLabelExtended = Me.State.MyBO.AssociatedLabel(Me.State.MyBO.Id, , True)
                    End If
                    BindBoPropertiesToGridHeaders()
                    CheckIfComingFromSaveConfirm()
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
            Me.ShowMissingTranslations(ErrControllerMaster)
        End Sub

#End Region
#Region "Handlers"

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
                If Me.State.oLabelExtended.UiProgCode Is Nothing Then
                    Dim obj As Label_Extended = New Label_Extended(Me.State.oLabelExtended.Id)
                    Me.State.oLabelExtended.UiProgCode = obj.UiProgCode
                End If
                PopulateFormFromBO()
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
                Dim ctrlDescription As TextBox = CType(grid.Items(itemIndex).Cells(Me.GRID_COL_LANGUAGE_ALIAS).FindControl(LANGUAGE_IN_GRID_CONTROL_NAME), TextBox)
                SetFocus(ctrlDescription)
                ctrlDealer.Enabled = False
            End If
        End Sub

        Protected Sub BindBoPropertiesToGridHeaders()

            Me.BindBOPropertyToGridHeader(Me.State.oDictItemTrans, "Translation", Me.moDictionaryGrid.Columns(Me.GRID_COL_TRANSLATION_ALIAS))
            Me.BindBOPropertyToLabel(Me.State.oLabelExtended, "UiProgCode", Me.lblUiProgCode)
            Me.ClearGridHeadersAndLabelsErrSign()
        End Sub

        Private Sub PopulateBOFromForm(ByVal bo As DictItemTranslation)
            Try
                Dim str As String = CType(Me.moDictionaryGrid.Items(Me.moDictionaryGrid.SelectedIndex).Cells(Me.GRID_COL_TRANSLATION_ALIAS).FindControl(Me.TRANSLATION_IN_GRID_CONTROL_NAME), TextBox).Text

                If Me.State.isNew Then
                    str = Me.txtUiProgCode.Text
                End If

                str = str.Replace("_", " ")

                Me.PopulateBOProperty(bo, "Translation", str)
                If Me.ErrCollection.Count > 0 Then
                    Throw New PopulateBOErrorException
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try

        End Sub

        Private Sub PopulateFormFromBO()
            Try
                Me.PopulateControlFromBOProperty(Me.txtUiProgCode, Me.State.oLabelExtended.UiProgCode)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try

        End Sub

        Private Sub SetButtonsState()

            If (Me.State.IsEditMode) Then
                ControlMgr.SetVisibleControl(Me, Me.moBtnSave_WRITE, True)
                ControlMgr.SetVisibleControl(Me, moBtnCancel, True)
                'Me.MenuEnabled = False
                If (Me.cboPageSize.Visible) Then
                    ControlMgr.SetEnableControl(Me, cboPageSize, False)
                End If
                Me.SetGridControls(Me.moDictionaryGrid, False)
            Else
                ControlMgr.SetVisibleControl(Me, moBtnSave_WRITE, False)
                ControlMgr.SetVisibleControl(Me, moBtnCancel, False)
                'Me.MenuEnabled = True
                If (Me.cboPageSize.Visible) Then
                    ControlMgr.SetEnableControl(Me, cboPageSize, True)
                End If
            End If

        End Sub

        Private Sub CreateBoFromGrid(ByVal index As Integer)
            Dim DictionaryId As Guid
            Dim oLanguage As String

            moDictionaryGrid.SelectedIndex = index
            DictionaryId = New Guid(moDictionaryGrid.Items(index).Cells(Me.GRID_COL_DICT_ITEM_ID).Text)
            'oLanguage = moDictionaryGrid.Items(index).Cells(Me.GRID_COL_LANGUAGE_ALIAS).ToString
            oLanguage = CType(moDictionaryGrid.Items(index).Cells(Me.GRID_COL_LANGUAGE_ALIAS).FindControl(LANGUAGE_IN_GRID_CONTROL_NAME), Label).Text

            For i As Integer = 0 To Me.State.searchDV.Table.Rows.Count - 1
                If Not Me.State.isNew Then
                    Dim oDictItemId As Guid = New Guid(CType(Me.State.searchDV.Table.Rows(i).Item(0), Byte()))
                    If DictionaryId.Equals(oDictItemId) Then
                        If Not Me.State.searchDV.Table.Rows(i).Item(1).Equals(CType(Me.moDictionaryGrid.Items(Me.moDictionaryGrid.SelectedIndex).Cells(Me.GRID_COL_TRANSLATION_ALIAS).FindControl(Me.TRANSLATION_IN_GRID_CONTROL_NAME), TextBox).Text) Then
                            Me.State.oDictItemTrans = Me.State.MyBO.AssociatedTranslation(DictionaryId)
                            PopulateBOFromForm(Me.State.oDictItemTrans)
                            Me.State.oDictItemTrans.Save()
                        End If
                    End If
                Else
                    Dim Language As String = Me.State.searchDV.Table.Rows(i).Item(2).ToString
                    If Language.ToUpper = oLanguage.ToUpper Then
                        Me.State.oDictItemTrans = Me.State.MyBO.AssociatedTranslation(Guid.Empty, Me.State.isNew)
                        Me.State.oDictItemTrans.LanguageId = New Guid(CType(Me.State.searchDV.Table.Rows(i).Item(3), Byte()))
                        Me.State.oDictItemTrans.DictItemId = Me.State.MyBO.Id
                        PopulateBOFromForm(Me.State.oDictItemTrans)
                        Me.State.oDictItemTrans.Save()
                    End If
                End If
            Next

        End Sub

        Private Sub SavePage()
            Dim index As Integer = 0
            Dim totItems As Integer = Me.moDictionaryGrid.Items.Count

            If Me.State.isNew Then
                Me.State.oLabelExtended.DictItemId = Me.State.MyBO.Id
                Me.PopulateBOProperty(Me.State.oLabelExtended, "InUse", "Y")
            End If

            Me.PopulateBOProperty(Me.State.oLabelExtended, "UiProgCode", Me.txtUiProgCode.Text.ToUpper)
            Me.State.oLabelExtended.Save()

            For index = 0 To totItems - 1
                CreateBoFromGrid(index)
            Next

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
                        Me.ReturnToCallingPage(Me.State.MyBO.Id)
                    Case ElitaPlusPage.DetailPageCommand.New_
                        'Me.AddInfoMsg(Message.SAVE_RECORD_CONFIRMATION)
                End Select
            ElseIf Not confResponse Is Nothing AndAlso confResponse = Me.MSG_VALUE_NO Then
                Select Case Me.State.ActionInProgress
                    Case ElitaPlusPage.DetailPageCommand.Back
                        Me.ReturnToCallingPage(Me.State.MyBO.Id)
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

        Private Sub moBtnSave_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles moBtnSave_WRITE.Click
            Try
                SavePage()
                If (Me.State.MyBO.IsFamilyDirty) Then
                    Me.State.MyBO.Save()
                    Me.State.isNew = False
                    ShowInfoMsgBox(Me.MSG_RECORD_SAVED_OK)
                    Me.State.searchDV = Nothing
                    PopulateTranslationGrid()
                    PopulateFormFromBO()
                    ControlMgr.SetVisibleControl(Me, moDictionaryGrid, True)
                Else
                    ShowInfoMsgBox(Me.MSG_RECORD_NOT_SAVED)
                    Me.ReturnFromEditing()
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
                Me.ReturnFromEditing(True)
                If Me.State.isNew Then
                    Me.State.MyBO = Nothing
                    Me.State.MyBO = New DictionaryItem
                    Me.State.oLabelExtended = Nothing
                    Me.State.searchDV = Nothing
                    Me.State.searchDV = Me.State.MyBO.GetLanguageList()
                    PopulateTranslationGrid()
                End If
            End Try

        End Sub

        Private Sub moBtnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles moBtnCancel.Click
            Try
                Me.moDictionaryGrid.SelectedIndex = Me.NO_ITEM_SELECTED_INDEX
                'Me.State.Canceling = True
                ReturnFromEditing()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub


        Private Sub btnBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.Click
            Try
                Me.SavePage()
                If Me.State.MyBO.IsFamilyDirty() Then
                    DisplayMessage(Message.MSG_PAGE_SAVE_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, Me.HiddenSavePagePromptResponse)
                    Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
                Else
                    Me.ReturnToCallingPage(State.MyBO.Id)
                End If

            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                Me.HandleErrors(ex, ErrControllerMaster)
                'Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR
                Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Me.HiddenSavePagePromptResponse)
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
                'Me.State.LastErrMsg = Me.ErrControllerMaster.Text
            End Try
        End Sub


        Private Sub btnNew_WRITE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew_WRITE.Click
            Me.State.searchDV = Nothing
            Me.State.searchDV = Me.State.MyBO.GetLanguageList()

            PopulateTranslationGrid()
        End Sub

#End Region

#Region " Datagrid Related "

        Private Sub PopulateTranslationGrid()
            Dim dv As DataView
            Try
                If (Me.State.searchDV Is Nothing) Then
                    'Me.SetStateProperties()
                    Me.State.searchDV = GetTranslationGridDataView()
                End If

                Me.State.searchDV.Sort = Me.State.SortExpression

                Me.moDictionaryGrid.AutoGenerateColumns = False
                Me.moDictionaryGrid.Columns(Me.GRID_COL_TRANSLATION_ALIAS).SortExpression = Me.State.oDictItemTrans.TranslationSearchDV.GRID_COL_TRANSLATION
                Me.moDictionaryGrid.Columns(Me.GRID_COL_LANGUAGE_ALIAS).SortExpression = Me.State.oDictItemTrans.TranslationSearchDV.GRID_COL_ENGLISH.ToUpper

                SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.DictItemId, Me.moDictionaryGrid, Me.State.PageIndex)
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

            'ControlMgr.SetVisibleControl(Me, moDictionaryGrid, Me.State.IsGridVisible)
            ControlMgr.SetVisibleControl(Me, trPageSize, Me.moDictionaryGrid.Visible)

            Session("recCount") = Me.State.searchDV.Count

            ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me, moDictionaryGrid)
        End Sub

        Private Function GetTranslationGridDataView() As DataView
            With State
                If Not Me.State.isNew Then
                    Me.State.searchDV = Me.State.oDictItemTrans.GetTranslationsList(Me.State.MyBO.Id)
                Else
                    Me.State.searchDV = Me.State.MyBO.GetLanguageList()
                End If
            End With

            Me.State.searchDV.Sort = moDictionaryGrid.DataMember()
            moDictionaryGrid.PageSize = Me.State.searchDV.Count
            moDictionaryGrid.DataSource = Me.State.searchDV

            Return (Me.State.searchDV)
        End Function

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
                    Me.PopulateControlFromBOProperty(.Cells(Me.GRID_COL_DICT_ITEM_ID), dvRow(DictItemTranslation.TranslationSearchDV.GRID_COL_ID))
                    oTextBox = CType(e.Item.Cells(Me.GRID_COL_TRANSLATION_ALIAS).FindControl(TRANSLATION_IN_GRID_CONTROL_NAME), TextBox)
                    If e.Item.ItemIndex() = 0 Then
                        SetFocus(oTextBox)
                    End If
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(DictItemTranslation.TranslationSearchDV.GRID_COL_TRANSLATION))
                    Me.PopulateControlFromBOProperty(e.Item.Cells(Me.GRID_COL_LANGUAGE_ALIAS).FindControl(LANGUAGE_IN_GRID_CONTROL_NAME), dvRow(DictItemTranslation.TranslationSearchDV.GRID_COL_ENGLISH).ToString.ToUpper)
                    oLabel = CType(e.Item.Cells(Me.GRID_COL_LANGUAGE_ALIAS).FindControl(LANGUAGE_IN_GRID_CONTROL_NAME), Label)

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
                Me.State.DictItemId = Guid.Empty
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

