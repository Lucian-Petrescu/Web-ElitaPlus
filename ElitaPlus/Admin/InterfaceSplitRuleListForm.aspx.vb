Imports System.Collections.Generic
Imports Assurant.ElitaPlus.ElitaPlusWebApp.Tables
Imports System.Threading
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms

Public Class InterfaceSplitRuleListForm
    Inherits ElitaPlusSearchPage

#Region "Constants"
    Public Const PAGETAB As String = "Admin"
    Public Const PAGETITLE As String = "INTERFACE_SPLIT_RULES"
    Public Const URL As String = "~/Admin/InterfaceSplitRuleListForm.aspx"
#End Region

#Region "Return Type"
    Public Class InterfaceSplitRuleReturnType
        Public Source As String
        Public SourceCode As String
        Public InterfaceSplitRuleId As Guid
    End Class
#End Region

#Region "Page State"
    Private IsReturningFromChild As Boolean = False
    Private ReturnType As InterfaceSplitRuleReturnType = Nothing

    Class MyState
        ' Selected Item Information
        Public SearchDV As InterfaceSplitRule.InterfaceSplitRuleSearchDV = Nothing
        Public PageIndex As Integer ' Stores Current Page Index
        Public PageSize As Integer = DEFAULT_NEW_UI_PAGE_SIZE ' Stores Current Page Size
        Public SortExpression As String = "Source" ' Stores Sort Column Name
        Public SortDirection As String = "ASC" ' Stores Sort Direction

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

#Region "Page Event Handlers"
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If (Not IsReturningFromChild) Then MasterPage.MessageController.Clear()
        Form.DefaultButton = btnSearch.UniqueID
        Try
            If (Not IsPostBack) Then
                ' Populate Source Drop Downs
                'Me.BindListControlToDataView(Me.moSource, LookupListNew.DropdownLookupList(LookupListCache.LK_SOURCE, ElitaPlusIdentity.Current.ActiveUser.LanguageId))

                Dim SourceList As DataElements.ListItem() =
                        CommonConfigManager.Current.ListManager.GetList(listCode:="SOURCE",
                                                                        languageCode:=Thread.CurrentPrincipal.GetLanguageCode())

                moSource.Populate(SourceList.ToArray(),
                                        New PopulateOptions() With
                                        {
                                            .AddBlankItem = True
                                        })

                ' Translate Grid Headers
                TranslateGridHeader(InterfaceSplitRuleGrid)

                ' Populate Search Criteria if Returning from Page and Information is Provided
                If (IsReturningFromChild AndAlso (Not (ReturnType Is Nothing))) Then
                    moSource.Text = ReturnType.Source
                    PopulateControlFromBOProperty(moSourceCode, ReturnType.SourceCode)
                    PopulateGrid()
                End If
            End If

            UpdateBreadCrum()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
        ShowMissingTranslations(MasterPage.MessageController)
    End Sub

    Private Sub InterfaceSplitRuleListForm_PageReturn(ReturnFromUrl As String, ReturnParameter As Object) Handles Me.PageReturn, Me.PageCall
        MasterPage.MessageController.Clear()
        Try
            MenuEnabled = True
            IsReturningFromChild = True
            If (ReturnParameter Is Nothing) Then
                Exit Sub
            End If

            Dim returnObj As PageReturnType(Of InterfaceSplitRuleReturnType) = CType(ReturnParameter, PageReturnType(Of InterfaceSplitRuleReturnType))
            If returnObj.HasDataChanged Then
                State.SearchDV = Nothing
            End If
            ReturnType = returnObj.EditingBo
            Select Case returnObj.LastOperation
                Case ElitaPlusPage.DetailPageCommand.Back
                    'If Not returnObj.EditingBo.IsNew Then
                    '    Me.State.SelectedWorkQueueId = returnObj.EditingBo.Id
                    'End If
                Case ElitaPlusPage.DetailPageCommand.Delete
                    MasterPage.MessageController.AddSuccess(Message.DELETE_RECORD_CONFIRMATION)
            End Select
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub UpdateBreadCrum()
        MasterPage.BreadCrum = TranslationBase.TranslateLabelOrMessage(PAGETAB) & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage(PAGETITLE)
        MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage(PAGETITLE)
        MasterPage.UsePageTabTitleInBreadCrum = False
    End Sub
#End Region

#Region "Button Event Handlers"
    Protected Sub cboPageSize_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboPageSize.SelectedIndexChanged
        Try
            State.PageSize = CType(cboPageSize.SelectedValue, Integer)
            PopulateGrid(True)
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        Try
            moSourceCode.Text = String.Empty
            moSource.SelectedIndex = BLANK_ITEM_SELECTED
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Try
            If (moSourceCode.Text = "" AndAlso _
                moSource.SelectedIndex = BLANK_ITEM_SELECTED) Then
                MasterPage.MessageController.AddErrorAndShow(ElitaPlus.Common.ErrorCodes.GUI_SEARCH_FIELD_NOT_SUPPLIED_ERR, True)
                Exit Sub
            End If
            'Reset the Caching on Search Results
            State.SearchDV = Nothing
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnAdd_WRITE_Click(sender As Object, e As System.EventArgs) Handles btnAdd_WRITE.Click
        Try
            Dim returnType As InterfaceSplitRuleReturnType = New InterfaceSplitRuleReturnType()
            Dim activeOn As Date
            With returnType
                .Source = GetSelectedValue(moSource)
                .SourceCode = moSourceCode.Text
            End With

            callPage(InterfaceSplitRuleForm.URL, returnType)
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub PopulateGrid(Optional ByVal updatePageIndex As Boolean = False)
        Dim recCount As Integer
        Try

            InterfaceSplitRuleGrid.PageSize = State.PageSize

            Dim result As InterfaceSplitRule.InterfaceSplitRuleSearchDV
            If State.SearchDV Is Nothing Then

                result = InterfaceSplitRule.GetList(LookupListNew.GetCodeFromId(LookupListNew.DropdownLookupList(LookupListNew.LK_SOURCE, ElitaPlusIdentity.Current.ActiveUser.LanguageId), GetSelectedItem(moSource)), moSourceCode.Text)
                State.SearchDV = result
            Else
                result = State.SearchDV
            End If

            recCount = result.Count

            If (updatePageIndex) Then
                State.PageIndex = NewCurrentPageIndex(InterfaceSplitRuleGrid, recCount, State.PageSize)
                InterfaceSplitRuleGrid.PageIndex = State.PageIndex
            End If

            State.SearchDV.Sort = State.SortExpression
            HighLightSortColumn(InterfaceSplitRuleGrid, State.SortExpression + " " + State.SortDirection, True)
            InterfaceSplitRuleGrid.DataSource = result
            InterfaceSplitRuleGrid.DataBind()


            If (InterfaceSplitRuleGrid.Rows.Count = 0) Then
                MasterPage.MessageController.AddInformation(Message.MSG_NO_RECORDS_FOUND, True)
                ControlMgr.SetVisibleControl(Me, moSearchResults, False)
            Else
                ControlMgr.SetVisibleControl(Me, moSearchResults, True)
                lblRecordCount.Text = recCount & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

#End Region

#Region "Grid Events"

    Private Sub Grid_Sorting(source As Object, e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles InterfaceSplitRuleGrid.Sorting
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
            State.PageIndex = 0
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try

    End Sub

    Private Sub Grid_PageIndexChanged(sender As Object, e As System.EventArgs) Handles InterfaceSplitRuleGrid.PageIndexChanged
        Try
            State.PageIndex = InterfaceSplitRuleGrid.PageIndex
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Grid_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles InterfaceSplitRuleGrid.PageIndexChanging
        Try
            InterfaceSplitRuleGrid.PageIndex = e.NewPageIndex
            State.PageIndex = InterfaceSplitRuleGrid.PageIndex
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub RowCreated(sender As Object, e As GridViewRowEventArgs) Handles InterfaceSplitRuleGrid.RowCreated
        Try
            BaseItemCreated(sender, e)
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try

    End Sub

    Private Sub Grid_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles InterfaceSplitRuleGrid.RowCommand
        Try
            Dim returnType As InterfaceSplitRuleReturnType = New InterfaceSplitRuleReturnType()
            Dim activeOn As Date
            With returnType
                .Source = GetSelectedValue(moSource)
                .SourceCode = moSourceCode.Text
            End With

            Select Case e.CommandName.ToString().ToUpper()
                Case "SELECTACTION"
                    returnType.InterfaceSplitRuleId = New Guid(e.CommandArgument.ToString())
                    callPage(InterfaceSplitRuleForm.URL, returnType)
            End Select
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

#End Region
End Class