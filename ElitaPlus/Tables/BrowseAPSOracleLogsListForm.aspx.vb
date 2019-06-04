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

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Me.MasterPage.MessageController.Clear()
        Me.Form.DefaultButton = btnSearch.UniqueID
        Me.MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage(ADMIN)

        Try
            If Not Me.IsPostBack Then
                ControlMgr.SetVisibleControl(Me, trPageSize, False)
                'RestoreGuiState()
                SetQuerystringValues()

                ' Populate the header and bredcrumb
                Me.MasterPage.UsePageTabTitleInBreadCrum = False
                UpdateBreadCrum()
                TranslateGridHeader(Grid)

                If Me.State.IsGridVisible Then
                    Me.PopulateGrid()
                End If
                cboPageSize.SelectedValue = Me.State.PageSize.ToString()

                Dim ddlSearchType As DropDownList = CType(moGenerationDate.FindControl("moSearchType"), DropDownList)
                ddlSearchType.Enabled = False

            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
        Me.ShowMissingTranslations(Me.MasterPage.MessageController)

    End Sub

    Private Sub SetQuerystringValues()
        Try
            If Not Request.QueryString("CALLER") Is Nothing Then
                Me.State.Caller = Request.QueryString("CALLER")
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub Page_PageReturn(ByVal ReturnFromUrl As String, ByVal ReturnPar As Object) Handles MyBase.PageReturn
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
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub UpdateBreadCrum()
        Me.MasterPage.UsePageTabTitleInBreadCrum = False
        Me.MasterPage.DisplayRequiredFieldNote = False

        Dim strTranslation As String = String.Empty
        If Me.State.Caller = "APS" Then
            strTranslation = TranslationBase.TranslateLabelOrMessage(BROWSE_APS_PUBLISHING_LOGS)
            Me.MasterPage.BreadCrum = Me.MasterPage.PageTab & ElitaBase.Sperator & strTranslation
            Me.MasterPage.PageTitle = strTranslation
        Else
            strTranslation = TranslationBase.TranslateLabelOrMessage(BROWSE_ORACLE_ERROR_LOGS)
            Me.MasterPage.BreadCrum = Me.MasterPage.PageTab & ElitaBase.Sperator & strTranslation
            Me.MasterPage.PageTitle = strTranslation
        End If

        Me.MasterPage.MessageController.Clear()

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

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            'Add validations to make from and to date mandatory and also check for the 10 days
            Dim fromValue As String = moGenerationDate.FromValue
            Dim toValue As String = moGenerationDate.ToValue

            If moGenerationDate.IsEmpty Then
                Me.MasterPage.MessageController.AddErrorAndShow(ElitaPlus.Common.ErrorCodes.MSG_SELECT_FROM_AND_TO_DATE, True)
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

            Me.State.PageIndex = 0
            Me.State.IsGridVisible = True
            Me.State.SearchClick = True
            Me.State.APSOracleLogssearchDV = Nothing

            Me.PopulateGrid()

        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

#Region "Clear Button Related"
    Private Sub btnClearSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClearSearch.Click
        Try
            ' Clear all search options typed or selected by the user
            Me.ClearAllSearchOptions()

            ' Update the Bo state properties with the new value
            Me.ClearStateValues()

            ' Me.SetStateProperties()

        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub ClearStateValues()
        Try
            'clear State
            Me.State.Header = String.Empty
            Me.State.Type = String.Empty
            Me.State.TypeName = String.Empty
            Me.State.Code = String.Empty
            Me.State.MachineName = String.Empty
            Me.State.UserName = String.Empty

        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub ClearAllSearchOptions()
        Me.txtHeader.Text = String.Empty
        Me.txtCode.Text = String.Empty
        Me.txtMachineName.Text = String.Empty
        Me.txtUserName.Text = String.Empty
        Me.moGenerationDate.Clear()

    End Sub
#End Region

#Region " GridView Related "
    Public Sub PopulateGrid()
        Try
            If (Me.State.APSOracleLogssearchDV Is Nothing) Then
                If Me.State.Caller = "APS" Then   'Unexpected_Error Logs
                    Me.State.APSOracleLogssearchDV = ApsPublishingLog.GetAPSPublishingLogsList(Me.State.Header, Me.State.Code,
                                                                                                   Me.State.MachineName, Me.State.UserName,
                                                                                                   APS_TYPE_NAME,
                                                                                                   APS_TABLE_NAME,
                                                                                                   DirectCast(moGenerationDate.Value, SearchCriteriaStructType(Of Date)))
                Else
                    Me.State.APSOracleLogssearchDV = ApsPublishingLog.GetAPSPublishingLogsList(Me.State.Header, Me.State.Code,
                                                                                                  Me.State.MachineName, Me.State.UserName,
                                                                                                  ORACLE_TYPE_NAME,
                                                                                                  ORACLE_TABLE_NAME,
                                                                                                  DirectCast(moGenerationDate.Value, SearchCriteriaStructType(Of Date)))
                End If
            End If

            If Me.State.SearchClick Then
                Me.ValidSearchResultCountNew(Me.State.APSOracleLogssearchDV.Count, True)
                Me.State.SearchClick = False
            End If
            Me.State.APSOracleLogssearchDV.Sort = Me.State.SortExpression
            Me.SortAndBindGrid()

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
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub SortAndBindGrid()

        Me.Grid.AutoGenerateColumns = False
        Me.Grid.PageSize = Me.State.PageSize

        Me.State.PageIndex = Me.Grid.PageIndex
        Me.Grid.DataSource = Me.State.APSOracleLogssearchDV
        HighLightSortColumn(Grid, Me.State.SortExpression)
        Me.Grid.DataBind()

        ControlMgr.SetVisibleControl(Me, Grid, Me.State.IsGridVisible)
        ControlMgr.SetVisibleControl(Me, trPageSize, Me.Grid.Visible)

        Session("recCount") = Me.State.APSOracleLogssearchDV.Count
        Me.lblRecordCount.Text = Me.State.APSOracleLogssearchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
    End Sub

    Private Sub Grid_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles Grid.Sorting
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
            Me.State.PageIndex = 0
            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try

    End Sub

    Private Sub cboPageSize_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
        Try
            Me.State.PageSize = CType(cboPageSize.SelectedValue, Integer)
            Me.State.SelectedPageSize = Me.State.PageSize
            'Me.State.PageIndex = NewCurrentPageIndex(Grid, State.SearchDV.Count, State.PageSize)
            Me.Grid.PageIndex = Me.State.PageIndex
            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Grid_PageIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Grid.PageIndexChanged
        Try
            Me.State.PageIndex = Grid.PageIndex

            PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Grid_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Grid.PageIndexChanging
        Try
            Grid.PageIndex = e.NewPageIndex
            State.PageIndex = Grid.PageIndex
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Grid_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowCreated
        Try
            BaseItemCreated(sender, e)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Grid_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles Grid.RowCommand
        Try
            If e.CommandName = SHOW_EXTENDED_CONTENT_COMMAND Then
                Dim row As GridViewRow = CType(CType(e.CommandSource, Control).Parent.Parent, GridViewRow)
                Dim RowInd As Integer = row.RowIndex
                txtExtendedContent.Text = "Extended Content: " & HttpUtility.HtmlEncode(Grid.DataKeys(RowInd).Values(0).ToString()) & Microsoft.VisualBasic.vbCrLf & Microsoft.VisualBasic.vbCrLf &
                 " Extended Content2: " & HttpUtility.HtmlEncode(Grid.DataKeys(RowInd).Values(1).ToString())

                mdlPopup.Show()
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
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
    Private Sub btnNewItemCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExtendedContentPopupCancel.Click
        Try
            mdlPopup.Hide()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

End Class