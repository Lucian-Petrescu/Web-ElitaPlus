Public Class BrowseAPSOracleLogsListForm
    Inherits ElitaPlusSearchPage

#Region "Constants"
    Public Const SHOW_EXTENDED_CONTENT_COMMAND As String = "ShowExtendedContent"
    Private Const ADMIN As String = "Admin"
    Private Const BROWSE_APS_PUBLISHING_LOGS As String = "BROWSE_APS_PUBLISHING_LOGS" 'Page Title
    Private Const BROWSE_ORACLE_ERROR_LOGS As String = "BROWSE_ORACLE_ERROR_LOGS" 'Page Title

    Private Const APS_TABLE_NAME As String = "APS_PUBLISHING_LOG"
    Private Const ORACLE_TABLE_NAME As String = "ELP_ORACLE_ERROR_LOG"

    Private Const APS_TYPE_NAME As String = "ERROR_UNEXPECTED"
    Private Const ORACLE_TYPE_NAME As String = "ORACLE"
#End Region

#Region "Page State"
    Private IsReturningFromChild As Boolean = False

    Class MyState

        Public MachineName As String
        Public UserName As String
        Public Header As String
        Public Type As String
        Public TypeName As String
        Public Code As String
        Public ApplicationName As Guid
        Public Generationdate As SearchCriteriaStructType(Of Date) = Nothing
        Public APSOracleLogssearchDV As ApsPublishingLog.APSOracleLogsSearchDV

        Public SortExpression As String = "Generation_Date_Time"

        Public HasDataChanged As Boolean
        Public IsGridVisible As Boolean
        Public SelectedPageSize As Integer
        Public SearchClick As Boolean = False

        Public PageIndex As Integer = 0
        Public PageSize As Integer = 30

        Public Caller As String

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

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        MasterPage.MessageController.Clear()
        Form.DefaultButton = btnSearch.UniqueID
        MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage(ADMIN)

        Try
            If Not IsPostBack Then
                ControlMgr.SetVisibleControl(Me, trPageSize, False)
                'RestoreGuiState()
                SetQuerystringValues()

                ' Populate the header and bredcrumb
                MasterPage.UsePageTabTitleInBreadCrum = False
                UpdateBreadCrum()
                TranslateGridHeader(Grid)

                If State.IsGridVisible Then
                    PopulateGrid()
                End If
                cboPageSize.SelectedValue = State.PageSize.ToString()

                Dim ddlSearchType As DropDownList = CType(moGenerationDate.FindControl("moSearchType"), DropDownList)
                ddlSearchType.Enabled = False

            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
        ShowMissingTranslations(MasterPage.MessageController)

    End Sub

    Private Sub SetQuerystringValues()
        Try
            If Request.QueryString("CALLER") IsNot Nothing Then
                State.Caller = Request.QueryString("CALLER")
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub Page_PageReturn(ReturnFromUrl As String, ReturnPar As Object) Handles MyBase.PageReturn
        Try
            'Me.MenuEnabled = True
            'Me.IsReturningFromChild = True
            'Dim retObj As PriceListDetailForm.ReturnType = CType(ReturnPar, PriceListDetailForm.ReturnType)
            'Me.State.HasDataChanged = retObj.HasDataChanged
            'Select Case retObj.LastOperation
            '    Case ElitaPlusPage.DetailPageCommand.Back
            '        If Not retObj Is Nothing Then
            '            If Not retObj.EditingBo.IsNew Then
            '                Me.State.SelectedPriceListId = retObj.EditingBo.Id
            '            End If
            '            Me.State.IsGridVisible = True
            '        End If
            '    Case ElitaPlusPage.DetailPageCommand.Delete
            '        Me.DisplayMessage(Message.DELETE_RECORD_CONFIRMATION, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
            '    Case ElitaPlusPage.DetailPageCommand.Expire
            '        Me.DisplayMessage(Message.EXPIRE_RECORD_CONFIRMATION, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
            'End Select
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub UpdateBreadCrum()
        MasterPage.UsePageTabTitleInBreadCrum = False
        MasterPage.DisplayRequiredFieldNote = False

        Dim strTranslation As String = String.Empty
        If State.Caller = "APS" Then
            strTranslation = TranslationBase.TranslateLabelOrMessage(BROWSE_APS_PUBLISHING_LOGS)
            MasterPage.BreadCrum = MasterPage.PageTab & ElitaBase.Sperator & strTranslation
            MasterPage.PageTitle = strTranslation
        Else
            strTranslation = TranslationBase.TranslateLabelOrMessage(BROWSE_ORACLE_ERROR_LOGS)
            MasterPage.BreadCrum = MasterPage.PageTab & ElitaBase.Sperator & strTranslation
            MasterPage.PageTitle = strTranslation
        End If

        MasterPage.MessageController.Clear()

    End Sub

    'Private Sub SaveGuiState()
    '    With Me.State
    '        .Header = Me.txtHeader.Text
    '        .Type = Me.ddlType.SelectedValue.ToString()
    '        .TypeName = Me.ddlType.SelectedItem.Text.ToString()
    '        .Code = Me.txtCode.Text
    '        .MachineName = Me.txtMachineName.Text
    '        .UserName = Me.txtUserName.Text
    '        .GenerationDate = DirectCast(moGenerationDate.Value, SearchCriteriaStructType(Of Date))
    '    End With
    'End Sub

    'Private Sub RestoreGuiState()
    '    With Me.State
    '        PopulateControlFromBOProperty(Me.txtHeader, .Header)
    '        'PopulateControlFromBOProperty(Me.ddlType, .Type)
    '        If Not String.IsNullOrEmpty(.Type) Then
    '            MyBase.SetSelectedItem(Me.ddlType, .Type)
    '        End If
    '        PopulateControlFromBOProperty(Me.txtCode, .Code)
    '        PopulateControlFromBOProperty(Me.txtMachineName, .MachineName)
    '        PopulateControlFromBOProperty(Me.txtUserName, .UserName)

    '    End With
    'End Sub

    Private Sub btnSearch_Click(sender As Object, e As System.EventArgs) Handles btnSearch.Click
        Try
            'Add validations to make from and to date mandatory and also check for the 10 days
            Dim fromValue As String = moGenerationDate.FromValue
            Dim toValue As String = moGenerationDate.ToValue

            If moGenerationDate.IsEmpty Then
                MasterPage.MessageController.AddErrorAndShow(ElitaPlus.Common.ErrorCodes.MSG_SELECT_FROM_AND_TO_DATE, True)
                Exit Sub
            End If
            If (Not moGenerationDate.IsEmpty) And Not moGenerationDate.Validate() Then
                Exit Sub
            End If

            Dim ts As TimeSpan = (DateTime.Parse(toValue) - DateTime.Parse(fromValue))
            If ts.Days > 10 Then
                Dim Errs() As ValidationError = {New ValidationError("GENERATION_DATE_NOT_BEYOND_10_DAYS", GetType(ApsPublishingLog), Nothing, "Search", Nothing)}
                Throw New BOValidationException(Errs, GetType(ApsPublishingLog).FullName)
            End If

            State.PageIndex = 0
            State.IsGridVisible = True
            State.SearchClick = True
            State.APSOracleLogssearchDV = Nothing

            PopulateGrid()

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

#Region "Clear Button Related"
    Private Sub btnClearSearch_Click(sender As Object, e As System.EventArgs) Handles btnClearSearch.Click
        Try
            ' Clear all search options typed or selected by the user
            ClearAllSearchOptions()

            ' Update the Bo state properties with the new value
            ClearStateValues()

            ' Me.SetStateProperties()

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub ClearStateValues()
        Try
            'clear State
            State.Header = String.Empty
            State.Type = String.Empty
            State.TypeName = String.Empty
            State.Code = String.Empty
            State.MachineName = String.Empty
            State.UserName = String.Empty

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub ClearAllSearchOptions()
        txtHeader.Text = String.Empty
        txtCode.Text = String.Empty
        txtMachineName.Text = String.Empty
        txtUserName.Text = String.Empty
        moGenerationDate.Clear()

    End Sub
#End Region

#Region " GridView Related "
    Public Sub PopulateGrid()
        Try
            If (State.APSOracleLogssearchDV Is Nothing) Then
                If State.Caller = "APS" Then   'Unexpected_Error Logs
                    State.APSOracleLogssearchDV = ApsPublishingLog.GetAPSPublishingLogsList(State.Header, State.Code,
                                                                                                   State.MachineName, State.UserName,
                                                                                                   APS_TYPE_NAME,
                                                                                                   APS_TABLE_NAME,
                                                                                                   DirectCast(moGenerationDate.Value, SearchCriteriaStructType(Of Date)))
                Else
                    State.APSOracleLogssearchDV = ApsPublishingLog.GetAPSPublishingLogsList(State.Header, State.Code,
                                                                                                  State.MachineName, State.UserName,
                                                                                                  ORACLE_TYPE_NAME,
                                                                                                  ORACLE_TABLE_NAME,
                                                                                                  DirectCast(moGenerationDate.Value, SearchCriteriaStructType(Of Date)))
                End If
            End If

            If State.SearchClick Then
                ValidSearchResultCountNew(State.APSOracleLogssearchDV.Count, True)
                State.SearchClick = False
            End If
            State.APSOracleLogssearchDV.Sort = State.SortExpression
            SortAndBindGrid()

            'Else 'Oracle Eror Logs
            '    If (Me.State.OracleErrorLogssearchDV Is Nothing) Then
            '        Me.State.OracleErrorLogssearchDV = OracleErrorLog.GetOracleErrorLogsList(Me.State.Header, Me.State.Code, Me.State.MachineName, Me.State.UserName, Me.ddlType.SelectedItem.Text.ToString(), Me.State.GenerationDateTime)
            '    End If
            '    If Me.State.SearchClick Then
            '        Me.ValidSearchResultCountNew(Me.State.OracleErrorLogssearchDV.Count, True)
            '        Me.State.SearchClick = False
            '    End If
            '    Me.State.OracleErrorLogssearchDV.Sort = Me.State.SortExpression
            '    Me.SortAndBindGrid(Me.State.OracleErrorLogssearchDV)
            'End If

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub SortAndBindGrid()

        Grid.AutoGenerateColumns = False
        Grid.PageSize = State.PageSize

        State.PageIndex = Grid.PageIndex
        Grid.DataSource = State.APSOracleLogssearchDV
        HighLightSortColumn(Grid, State.SortExpression)
        Grid.DataBind()

        ControlMgr.SetVisibleControl(Me, Grid, State.IsGridVisible)
        ControlMgr.SetVisibleControl(Me, trPageSize, Grid.Visible)

        Session("recCount") = State.APSOracleLogssearchDV.Count
        lblRecordCount.Text = State.APSOracleLogssearchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
    End Sub

    Private Sub Grid_SortCommand(source As Object, e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles Grid.Sorting
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

    Private Sub cboPageSize_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
        Try
            State.PageSize = CType(cboPageSize.SelectedValue, Integer)
            State.SelectedPageSize = State.PageSize
            'Me.State.PageIndex = NewCurrentPageIndex(Grid, State.SearchDV.Count, State.PageSize)
            Grid.PageIndex = State.PageIndex
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Grid_PageIndexChanged(sender As Object, e As System.EventArgs) Handles Grid.PageIndexChanged
        Try
            State.PageIndex = Grid.PageIndex

            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Grid_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Grid.PageIndexChanging
        Try
            Grid.PageIndex = e.NewPageIndex
            State.PageIndex = Grid.PageIndex
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Grid_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowCreated
        Try
            BaseItemCreated(sender, e)
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Grid_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles Grid.RowCommand
        Try
            If e.CommandName = SHOW_EXTENDED_CONTENT_COMMAND Then
                Dim row As GridViewRow = CType(CType(e.CommandSource, Control).Parent.Parent, GridViewRow)
                Dim RowInd As Integer = row.RowIndex
                txtExtendedContent.Text = "Extended Content: " & HttpUtility.HtmlEncode(Grid.DataKeys(RowInd).Values(0).ToString()) & Microsoft.VisualBasic.vbCrLf & Microsoft.VisualBasic.vbCrLf &
                 " Extended Content2: " & HttpUtility.HtmlEncode(Grid.DataKeys(RowInd).Values(1).ToString())

                mdlPopup.Show()
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

#End Region

    ''' <summary>
    ''' Modal pupup cancel button
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' 
    Private Sub btnNewItemCancel_Click(sender As Object, e As System.EventArgs) Handles btnExtendedContentPopupCancel.Click
        Try
            mdlPopup.Hide()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

End Class