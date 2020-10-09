Namespace Translation
    Partial Public Class AdminLabelTransListForm
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
        Private IsReturningFromChild As Boolean = False

        ' This class keeps the current state for the search page.
        Class MyState
            Public PageIndex As Integer = 0
            Public MyBO As Label_Extended
            Public DictItemId As Guid
            Public LangId As Guid
            Public DescriptionMask As String = ""
            Public OrderByTrans As Boolean = True
            Public IsAfterSave As Boolean
            Public IsEditMode As Boolean
            Public IsGridVisible As Boolean
            Public searchDV As DataView = Nothing
            Public SortExpression As String = Label_Extended.LabelSearchDV.GRID_COL_UI_PROG_CODE
            Public Canceling As Boolean
            Public selectedPageSize As Integer = DEFAULT_PAGE_SIZE
            Public ActionInProgress As DetailPageCommand = DetailPageCommand.Nothing_
            Public LastErrMsg As String
            Public HasDataChanged As Boolean

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

        Public Const PAGETITLE As String = "LABEL TRANSLATION"
        Public Const PAGETAB As String = "ADMIN"
        Public Const GRID_COL_EDIT_IDX As Integer = 0
        Private Const GRID_COL_DICT_ITEM_ID As Integer = 1
        Private Const GRID_COL_UI_PROG_CODE_ALIAS As Integer = 2
        Private Const GRID_COL_ENGLIS_ALIAS As Integer = 3

        Private Const UI_PROG_CODE_IN_GRID_CONTROL_NAME As String = "moUIProgcodeLabelGrid"
        Private Const ENLISH_IN_GRID_CONTROL_NAME As String = "moEnglishLabelGrid"

        Private Const PAGE_SIZE As Integer = 10
        Private Const EMPTY As String = ""

#End Region
#Region "Handlers-Init"
        Private Sub Page_PageReturn(ReturnFromUrl As String, ReturnPar As Object) Handles MyBase.PageReturn
            MenuEnabled = True
            IsReturningFromChild = True
            If ReturnPar IsNot Nothing Then
                State.DictItemId = CType(ReturnPar, Guid)
                State.searchDV = Nothing
                'PopulateTranslationGrid()
                'Else
                '    State.HasDataChanged = False
            End If
        End Sub

        Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
            Try
                ErrControllerMaster.Clear_Hide()
                If Not IsPostBack Then
                    SetFormTitle(PAGETITLE)
                    SetFormTab(PAGETAB)

                    If Not IsReturningFromChild Then
                        ControlMgr.SetVisibleControl(Me, trPageSize, False)
                        SetDefaultButton(txtSearchTrans, btnSearch)
                        SetGridItemStyleColor(moDictionaryGrid)
                        State.PageIndex = 0
                    End If

                    GetStateProperties()

                    If State.IsGridVisible Then
                        PopulateTranslationGrid()
                    End If

                    'Set page size
                    cboPageSize.SelectedValue = State.selectedPageSize.ToString()

                Else
                    If State.searchDV IsNot Nothing Then
                        moDictionaryGrid.DataSource = State.searchDV
                    End If
                    'CheckIfComingFromSaveConfirm()
                End If
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
            ShowMissingTranslations(ErrControllerMaster)
        End Sub

#End Region
#Region "Handlers"

#End Region
#Region "Controlling Logic"

        Private Sub SetFocusOnEditableFieldInGrid(grid As DataGrid, cellPosition As Integer, itemIndex As Integer, IsNewRowAndSingleCountry As Boolean)
            'Set focus on the Description TextBox for the EditItemIndex row
            Dim ctrlDealer As TextBox = CType(grid.Items(itemIndex).Cells(GRID_COL_UI_PROG_CODE_ALIAS).FindControl(UI_PROG_CODE_IN_GRID_CONTROL_NAME), TextBox)
            SetFocus(ctrlDealer)
            If cellPosition = GRID_COL_UI_PROG_CODE_ALIAS Then
                SetFocus(ctrlDealer)
                ctrlDealer.Enabled = True
            Else
                Dim ctrlDescription As TextBox = CType(grid.Items(itemIndex).Cells(GRID_COL_ENGLIS_ALIAS).FindControl(ENLISH_IN_GRID_CONTROL_NAME), TextBox)
                SetFocus(ctrlDescription)
                ctrlDealer.Enabled = False
            End If
        End Sub

        Private Sub SetSortOption()

            Dim mIndex As Integer = rdbViewType.SelectedIndex

            If mIndex = 1 Then
                State.SortExpression = Label_Extended.LabelSearchDV.GRID_COL_ENGLISH
                State.OrderByTrans = False
            Else
                State.SortExpression = Label_Extended.LabelSearchDV.GRID_COL_UI_PROG_CODE
                State.OrderByTrans = True
            End If
            PopulateTranslationGrid()
        End Sub

#End Region

#Region "Private Methods"


#End Region
#Region "Button Management"
        Private Sub BtnSearch_Click(sender As System.Object, e As System.EventArgs) Handles btnSearch.Click
            Try
                State.IsGridVisible = True
                State.searchDV = Nothing
                State.DictItemId = Guid.Empty
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


        Public Sub Index_Changed(sender As System.Object, e As System.EventArgs) Handles rdbViewType.SelectedIndexChanged
            Try
                SetSortOption()
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

#End Region

#Region " Datagrid Related "

        Private Sub PopulateTranslationGrid()
            Dim dv As DataView
            Try
                If (State.searchDV Is Nothing)OrElse (State.HasDataChanged) Then
                    SetStateProperties()
                    State.searchDV = GetTranslationGridDataView()
                End If

                State.searchDV.Sort = State.SortExpression

                moDictionaryGrid.AutoGenerateColumns = False
                moDictionaryGrid.Columns(GRID_COL_UI_PROG_CODE_ALIAS).SortExpression = State.MyBO.LabelSearchDV.GRID_COL_UI_PROG_CODE
                moDictionaryGrid.Columns(GRID_COL_ENGLIS_ALIAS).SortExpression = State.MyBO.LabelSearchDV.GRID_COL_ENGLISH

                If IsReturningFromChild Then
                    moDictionaryGrid.PageSize = State.selectedPageSize
                End If

                SetPageAndSelectedIndexFromGuid(State.searchDV, State.DictItemId, moDictionaryGrid, State.PageIndex)

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
        End Sub

        Private Function GetTranslationGridDataView() As DataView
            With State
                State.searchDV = State.MyBO.LoadList(State.DescriptionMask.ToUpper, _
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

        Private Sub GetStateProperties()
            Try
                txtSearchTrans.Text = State.DescriptionMask
                cboPageSize.SelectedValue = State.selectedPageSize.ToString()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub moDictionaryGrid_ItemCommand(source As Object, e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles moDictionaryGrid.ItemCommand
            Try
                If e.CommandName = "SelectAction" Then
                    State.DictItemId = New Guid(e.Item.Cells(GRID_COL_DICT_ITEM_ID).Text)
                    'UpdateUserCompany()
                    callPage(AdminMaintainLabelTranslationForm.URL, State.DictItemId)
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try

        End Sub


        Private Sub moDictionaryGrid_PageIndexChanged(source As System.Object, e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles moDictionaryGrid.PageIndexChanged
            Try
                If (Not (State.IsEditMode)) Then
                    State.PageIndex = e.NewPageIndex
                    State.DictItemId = Guid.Empty
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
                State.selectedPageSize = CType(cboPageSize.SelectedValue, Integer)
                State.PageIndex = NewCurrentPageIndex(moDictionaryGrid, State.searchDV.Count, State.selectedPageSize)
                moDictionaryGrid.CurrentPageIndex = State.PageIndex
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
                    PopulateControlFromBOProperty(.Cells(GRID_COL_DICT_ITEM_ID), dvRow(Label_Extended.LabelSearchDV.DICT_ITEM_ID))

                    oLabel = CType(e.Item.Cells(GRID_COL_UI_PROG_CODE_ALIAS).FindControl(UI_PROG_CODE_IN_GRID_CONTROL_NAME), Label)

                    'Replace Leading spaces with HTML Escapes

                    str = dvRow(Label_Extended.LabelSearchDV.GRID_COL_UI_PROG_CODE).ToString
                    For i As Integer = 0 To str.Length - 1
                        If str.Chars(i) = " " Then
                            str = If(i > 0, str.Substring(0, i - 1), "").ToString + "&nbsp;" + If(i < str.Length - 1, str.Substring(i + 1), "").ToString
                        Else
                            Exit For
                        End If
                    Next
                    PopulateControlFromBOProperty(oLabel, str)

                    oLabel = CType(e.Item.Cells(GRID_COL_ENGLIS_ALIAS).FindControl(ENLISH_IN_GRID_CONTROL_NAME), Label)
                    str = dvRow(Label_Extended.LabelSearchDV.GRID_COL_ENGLISH).ToString
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

        Private Sub moDictionaryGrid_SortCommand(source As Object, e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles moDictionaryGrid.SortCommand
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
                State.DictItemId = Guid.Empty
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

        Private Sub btnAdd_WRITE_Click(sender As Object, e As System.EventArgs) Handles btnAdd_WRITE.Click
            callPage(AdminMaintainLabelTranslationForm.URL)
        End Sub
    End Class
End Namespace