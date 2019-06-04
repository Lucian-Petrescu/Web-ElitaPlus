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
        Private Sub Page_PageReturn(ByVal ReturnFromUrl As String, ByVal ReturnPar As Object) Handles MyBase.PageReturn
            Me.MenuEnabled = True
            Me.IsReturningFromChild = True
            If Not ReturnPar Is Nothing Then
                State.DictItemId = CType(ReturnPar, Guid)
                Me.State.searchDV = Nothing
                'PopulateTranslationGrid()
                'Else
                '    State.HasDataChanged = False
            End If
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
                    Me.State.PageIndex = 0
                    If Me.State.IsGridVisible Then
                        PopulateTranslationGrid()
                    End If

                Else
                    If Not Me.State.searchDV Is Nothing Then
                        moDictionaryGrid.DataSource = Me.State.searchDV
                    End If
                    'CheckIfComingFromSaveConfirm()
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

        Private Sub SetFocusOnEditableFieldInGrid(ByVal grid As DataGrid, ByVal cellPosition As Integer, ByVal itemIndex As Integer, ByVal IsNewRowAndSingleCountry As Boolean)
            'Set focus on the Description TextBox for the EditItemIndex row
            Dim ctrlDealer As TextBox = CType(grid.Items(itemIndex).Cells(Me.GRID_COL_UI_PROG_CODE_ALIAS).FindControl(Me.UI_PROG_CODE_IN_GRID_CONTROL_NAME), TextBox)
            SetFocus(ctrlDealer)
            If cellPosition = Me.GRID_COL_UI_PROG_CODE_ALIAS Then
                SetFocus(ctrlDealer)
                ctrlDealer.Enabled = True
            Else
                Dim ctrlDescription As TextBox = CType(grid.Items(itemIndex).Cells(Me.GRID_COL_ENGLIS_ALIAS).FindControl(ENLISH_IN_GRID_CONTROL_NAME), TextBox)
                SetFocus(ctrlDescription)
                ctrlDealer.Enabled = False
            End If
        End Sub

        Private Sub SetSortOption()

            Dim mIndex As Integer = rdbViewType.SelectedIndex

            If mIndex = 1 Then
                Me.State.SortExpression = Label_Extended.LabelSearchDV.GRID_COL_ENGLISH
                Me.State.OrderByTrans = False
            Else
                Me.State.SortExpression = Label_Extended.LabelSearchDV.GRID_COL_UI_PROG_CODE
                Me.State.OrderByTrans = True
            End If
            Me.PopulateTranslationGrid()
        End Sub

#End Region

#Region "Private Methods"


#End Region
#Region "Button Management"
        Private Sub BtnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
            Try
                Me.State.IsGridVisible = True
                Me.State.searchDV = Nothing
                Me.State.DictItemId = Guid.Empty
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


        Public Sub Index_Changed(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbViewType.SelectedIndexChanged
            Try
                SetSortOption()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

#End Region

#Region " Datagrid Related "

        Private Sub PopulateTranslationGrid()
            Dim dv As DataView
            Try
                If (Me.State.searchDV Is Nothing)OrElse (Me.State.HasDataChanged) Then
                    Me.SetStateProperties()
                    Me.State.searchDV = GetTranslationGridDataView()
                End If

                Me.State.searchDV.Sort = Me.State.SortExpression

                Me.moDictionaryGrid.AutoGenerateColumns = False
                Me.moDictionaryGrid.Columns(Me.GRID_COL_UI_PROG_CODE_ALIAS).SortExpression = Me.State.MyBO.LabelSearchDV.GRID_COL_UI_PROG_CODE
                Me.moDictionaryGrid.Columns(Me.GRID_COL_ENGLIS_ALIAS).SortExpression = Me.State.MyBO.LabelSearchDV.GRID_COL_ENGLISH

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

            ControlMgr.SetVisibleControl(Me, moDictionaryGrid, Me.State.IsGridVisible)
            ControlMgr.SetVisibleControl(Me, trPageSize, Me.moDictionaryGrid.Visible)

            Session("recCount") = Me.State.searchDV.Count

            ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me, moDictionaryGrid)
        End Sub

        Private Function GetTranslationGridDataView() As DataView
            With State
                Me.State.searchDV = Me.State.MyBO.LoadList(Me.State.DescriptionMask.ToUpper, _
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

        Private Sub moDictionaryGrid_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles moDictionaryGrid.ItemCommand
            Try
                If e.CommandName = "SelectAction" Then
                    Me.State.DictItemId = New Guid(e.Item.Cells(Me.GRID_COL_DICT_ITEM_ID).Text)
                    'UpdateUserCompany()
                    Me.callPage(AdminMaintainLabelTranslationForm.URL, Me.State.DictItemId)
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try

        End Sub


        Private Sub moDictionaryGrid_PageIndexChanged(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles moDictionaryGrid.PageIndexChanged
            Try
                If (Not (Me.State.IsEditMode)) Then
                    Me.State.PageIndex = e.NewPageIndex
                    Me.State.DictItemId = Guid.Empty
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
                    Me.PopulateControlFromBOProperty(.Cells(Me.GRID_COL_DICT_ITEM_ID), dvRow(Label_Extended.LabelSearchDV.DICT_ITEM_ID))

                    oLabel = CType(e.Item.Cells(Me.GRID_COL_UI_PROG_CODE_ALIAS).FindControl(UI_PROG_CODE_IN_GRID_CONTROL_NAME), Label)

                    'Replace Leading spaces with HTML Escapes

                    str = dvRow(Label_Extended.LabelSearchDV.GRID_COL_UI_PROG_CODE).ToString
                    For i As Integer = 0 To str.Length - 1
                        If str.Chars(i) = " " Then
                            str = If(i > 0, str.Substring(0, i - 1), "").ToString + "&nbsp;" + If(i < str.Length - 1, str.Substring(i + 1), "").ToString
                        Else
                            Exit For
                        End If
                    Next
                    Me.PopulateControlFromBOProperty(oLabel, str)

                    oLabel = CType(e.Item.Cells(Me.GRID_COL_ENGLIS_ALIAS).FindControl(ENLISH_IN_GRID_CONTROL_NAME), Label)
                    str = dvRow(Label_Extended.LabelSearchDV.GRID_COL_ENGLISH).ToString
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

        Private Sub moDictionaryGrid_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles moDictionaryGrid.SortCommand
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

        Private Sub btnAdd_WRITE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd_WRITE.Click
            Me.callPage(AdminMaintainLabelTranslationForm.URL)
        End Sub
    End Class
End Namespace