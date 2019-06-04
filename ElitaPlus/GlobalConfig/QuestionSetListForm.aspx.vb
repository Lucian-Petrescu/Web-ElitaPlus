Option Infer On
Option Explicit On

Imports System.Collections.Generic
Imports System.Threading
Imports Assurant.ElitaPlus.ElitaPlusWebApp.QuestionService

Namespace GlobalConfig

    Partial Class QuestionSetListForm
        Inherits ElitaPlusSearchPage

        ''' <summary>
        ''' Gets New Instance of Question Service Client with Crdentials Configured from Web Passwords
        ''' </summary>
        ''' <returns>Instance of <see cref="QuestionServiceClient"/></returns>
        Private Shared Function GetClient() As QuestionServiceClient
            Dim oWebPasswd = New WebPasswd(Guid.Empty, LookupListNew.GetIdFromCode(Codes.SERVICE_TYPE, Codes.SERVICE_TYPE__CLAIMS_QUESTION_SERVICE), False)
            Dim client = New QuestionServiceClient("CustomBinding_IQuestionService", oWebPasswd.Url)
            client.ClientCredentials.UserName.UserName = oWebPasswd.UserId
            client.ClientCredentials.UserName.Password = oWebPasswd.Password
            Return client
        End Function

#Region "Constants"

        Private Const SelectActionCommand As String = "SelectRecord"
#End Region

#Region "Page State"

        ' This class keeps the current state for the search page.
        Class MyState
            Public PageIndex As Integer = 0
            Public CodeMask As String
            Public DescriptionMask As String
            Public SearchData As IEnumerable(Of QuestionSetInfo)
            Public SelectedPageSize As Integer = DEFAULT_NEW_UI_PAGE_SIZE
            Public QuestionSetCode As String = String.Empty
            Public SortColumn As String = "Code"
            Public SortDirection As WebControls.SortDirection = WebControls.SortDirection.Ascending
            Public ReadOnly Property SortExpression As String
                Get
                    Return String.Format("{0} {1}", SortColumn, If(SortDirection = WebControls.SortDirection.Ascending, "ASC", "DESC"))
                End Get
            End Property
        End Class

        Protected Sub New()
            MyBase.New(New MyState)
        End Sub

        Private Shadows ReadOnly Property State() As MyState
            Get
                Return CType(MyBase.State, MyState)
            End Get
        End Property

#End Region

#Region "Page_Events"

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load

            MasterPage.MessageController.Clear_Hide()
            SetStateProperties()

            Try
                If Not IsPostBack Then
                    MasterPage.MessageController.Clear()
                    UpdateBreadCrum()
                    TranslateGridHeader(QuestionSetGridView)
                    SetGridItemStyleColor(QuestionSetGridView)
                    ControlMgr.SetVisibleControl(Me, PageSizeRow, False)
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
            ShowMissingTranslations(MasterPage.MessageController)
        End Sub

        Private Sub UpdateBreadCrum()
            Dim pageTitle = TranslationBase.TranslateLabelOrMessage("QUESTION_SET")
            MasterPage.BreadCrum = TranslationBase.TranslateLabelOrMessage("ADMIN") & ElitaBase.Sperator & pageTitle
            MasterPage.PageTitle = pageTitle
            MasterPage.UsePageTabTitleInBreadCrum = False
        End Sub

        Private Sub Page_PageReturn(ByVal returnFromUrl As String, ByVal returnParameter As Object) Handles MyBase.PageReturn
            MenuEnabled = True
            Dim returnObject As PageReturnType(Of Object) = CType(returnParameter, PageReturnType(Of Object))
            If (returnObject.HasDataChanged) Then State.SearchData = Nothing
        End Sub

#End Region

#Region "Controlling Logic"

        Private Sub PopulateGrid()
            If (State.SearchData Is Nothing) Then
                State.SearchData = GetClient().SearchQuestionSet(State.CodeMask, State.DescriptionMask)
            End If

            QuestionSetGridView.AutoGenerateColumns = False
            SortAndBindGrid()
        End Sub

        Private Sub SortAndBindGrid()

            If (State.SearchData Is Nothing OrElse State.SearchData.Count = 0) Then
                ControlMgr.SetVisibleControl(Me, QuestionSetGridView, False)
                RecordCountLabel.Text = TranslationBase.TranslateLabelOrMessage(Message.MSG_NO_RECORDS_FOUND)
            Else
                ControlMgr.SetVisibleControl(Me, QuestionSetGridView, True)
                Dim keySelector = If(State.SortColumn = "Code", Function(qs) qs.Code, Function(qs) qs.Description)
                Dim dataSource = If(State.SortDirection = WebControls.SortDirection.Ascending, State.SearchData.OrderBy(Of String)(keySelector), State.SearchData.OrderByDescending(Of String)(keySelector))
                QuestionSetGridView.PageIndex = State.PageIndex
                QuestionSetGridView.PageSize = State.SelectedPageSize
                QuestionSetGridView.DataSource = dataSource.ToList()
                HighLightSortColumn(QuestionSetGridView, State.SortExpression, True)
                QuestionSetGridView.DataBind()
                RecordCountLabel.Text = String.Format("{0} {1}", State.SearchData.Count.ToString(), TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND))
            End If

            ControlMgr.SetVisibleControl(Me, PageSizeRow, True)
        End Sub

#End Region

#Region " Datagrid Related "
        Protected Sub QuestionSetGridView_RowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs)
            Try
                If e.CommandName = SelectActionCommand Then
                    If Not e.CommandArgument.ToString().Equals(String.Empty) Then
                        State.QuestionSetCode = e.CommandArgument.ToString()
                        callPage(QuestionSetForm.Url, State.QuestionSetCode)
                        Return
                    End If
                End If
            Catch ex As ThreadAbortException
                ' Do nothing
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub QuestionSetGridView_RowCreated(ByVal sender As System.Object, ByVal e As GridViewRowEventArgs) Handles QuestionSetGridView.RowCreated
            BaseItemCreated(sender, e)
        End Sub

        Private Sub PageSizeCombo_SelectedIndexChanged(ByVal source As Object, ByVal e As EventArgs) Handles PageSizeCombo.SelectedIndexChanged
            Try
                State.SelectedPageSize = CType(PageSizeCombo.SelectedValue, Integer)
                State.PageIndex = NewCurrentPageIndex(QuestionSetGridView, State.SearchData.Count(), State.SelectedPageSize)
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub Grid_SortCommand(ByVal source As Object, ByVal e As GridViewSortEventArgs) Handles QuestionSetGridView.Sorting
            Try
                If (State.SortColumn = e.SortExpression) Then
                    State.SortDirection = If(State.SortDirection = WebControls.SortDirection.Ascending, WebControls.SortDirection.Descending, WebControls.SortDirection.Ascending)
                Else
                    State.SortDirection = WebControls.SortDirection.Ascending
                End If
                State.SortColumn = e.SortExpression
                State.PageIndex = 0
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub Grid_PageIndexChanged(ByVal source As Object, ByVal e As GridViewPageEventArgs) Handles QuestionSetGridView.PageIndexChanging

            Try
                State.PageIndex = e.NewPageIndex
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

#End Region

        ''' <summary>
        ''' Resets Search Results related State Properties like Page Index, Sort, Search Data, Selected Record etc.
        ''' </summary>
        Private Sub ResetSearchResults()
            State.PageIndex = 0
            State.QuestionSetCode = String.Empty
            State.SearchData = Nothing
        End Sub

        ''' <summary>
        ''' Clears Search Criteria
        ''' </summary>
        Private Sub ResetSearchCriteria()
            DescriptionTextBox.Text = String.Empty
            CodeTextBox.Text = String.Empty
        End Sub

#Region "Button Clicks"
        Private Sub ClearSearchButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ClearSearchButton.Click
            ResetSearchCriteria()
        End Sub

        Private Sub SearchButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles SearchButton.Click
            Try
                ResetSearchResults()
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub SetStateProperties()
            State.DescriptionMask = DescriptionTextBox.Text.Trim()
            State.CodeMask = CodeTextBox.Text.Trim()
        End Sub



        Private Sub AddButton_WRITE_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles AddButton_WRITE.Click
            Try
                callPage(QuestionSetForm.Url)
            Catch ex As ThreadAbortException
                ' Do nothing
            End Try

        End Sub


#End Region

    End Class
End Namespace