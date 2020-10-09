Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports Assurant.Elita.Web.Forms
Imports System.Threading
Namespace Tables

    Partial Class OcMessageSearchForm
        Inherits ElitaPlusSearchPage

#Region " Web Form Designer Generated Code "
        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        End Sub

        'NOTE: The following placeholder declaration is required by the Web Form Designer.
        'Do not delete or move it.
        Private designerPlaceholderDeclaration As System.Object

        Private Sub Page_Init(sender As System.Object, e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

#Region "Constants"
        Private Const MESSAGE_LIST_FORM001 As String = "MESSAGE_LIST_FORM001" 'Maintain Message List Exception
        Private Const LABEL_DEALER As String = "DEALER"
        Public Const URL As String = "~/tables/OcMessageSearchForm.aspx"

        Public Const GRID_TOTAL_COLUMNS As Integer = 14
        Public Const GRID_COL_SELECT_IDX As Integer = 0
        Public Const GRID_COL_EDIT_IDX As Integer = 1

        Public Const GRID_COL_SELECT_RECORD As Integer = 0
        Public Const GRID_COL_TEMPLATE_DESCRIPTION As Integer = 1
        Public Const GRID_COL_SENDER_REASON As Integer = 2
        Public Const GRID_COL_RECIPIENT_ADDRESS As Integer = 3
        Public Const GRID_COL_LAST_ATTEMPTED_ON As Integer = 4
        Public Const GRID_COL_LAST_ATTEMPTED_STATUS As Integer = 5

        Public Const GRID_COL_CERT_NUMBER As Integer = 6
        Public Const GRID_COL_CLAIM_NUMBER As Integer = 7
        Public Const GRID_COL_CASE_NUMBER As Integer = 8

        Public Const GRID_COL_OC_MESSAGE_ID As Integer = 9
        Public Const GRID_COL_OC_TEMPLATE_ID As Integer = 10
        Public Const GRID_COL_CERTIFICATE_ID As Integer = 11
        Public Const GRID_COL_CLAIM_ID As Integer = 12
        Public Const GRID_COL_CASE_ID As Integer = 13

#End Region

#Region "Page Call Type"
        Public Class CallType
            Public search_by As String
            Public search_value As String
            Public search_id As Guid
            Public dealer_id As Guid

            Public Sub New(searchby As String, searchvalue As String, searchid As Guid, dealerid As Guid)
                search_by = searchby
                search_value = searchvalue
                search_id = searchid
                dealer_id = dealerid
            End Sub

            Public Sub New(searchby As String, searchvalue As String, dealerid As Guid)
                search_by = searchby
                search_value = searchvalue
                dealer_id = dealerid
            End Sub

        End Class
#End Region

#Region "Page State"
        ' This class keeps the current state for the search page.
        Class MyState
            Public IsGridVisible As Boolean = False
            Public SearchDV As OcMessage.MessageSearchDV = Nothing
            Public SortExpression As String = OcMessage.MessageSearchDV.COL_CREATED_DATE
            Public PageIndex As Integer = 0
            Public PageSort As String
            Public PageSize As Integer = 15

            Public MessageId As Guid = Guid.Empty

            Public FromDetailPage As Boolean = False
            Public DealerId As Guid = Guid.Empty
            Public SearchBy As String
            Public ConditionMask As String
            Public ConditionId As Guid = Guid.Empty

            Public bNoRow As Boolean = False

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

#Region "Page Return"
        Private IsReturningFromChild As Boolean = False

        Private Sub HandleReturnFromMessageForm(ReturnPar As Object)
            Dim retObj As OcMessageForm.ReturnType = CType(ReturnPar, OcMessageForm.ReturnType)

            If retObj IsNot Nothing AndAlso retObj.HasDataChanged Then
                State.SearchDV = Nothing
            End If

            If retObj IsNot Nothing Then
                Select Case retObj.LastOperation
                    Case ElitaPlusPage.DetailPageCommand.Back
                        If Not retObj.MessageId = Guid.Empty Then
                            State.MessageId = retObj.MessageId
                            State.IsGridVisible = True
                        End If
                    Case Else
                        State.MessageId = Guid.Empty
                End Select

                Grid.PageIndex = State.PageIndex
                cboPageSize.SelectedValue = CType(State.PageSize, String)
                Grid.PageSize = State.PageSize
                ControlMgr.SetVisibleControl(Me, trPageSize, Grid.Visible)
            End If
        End Sub

        Private Sub Page_PageReturn(ReturnFromUrl As String, ReturnPar As Object) Handles Me.PageReturn
            Try
                MenuEnabled = True
                IsReturningFromChild = True

                If TypeOf ReturnPar Is OcMessageForm.ReturnType Then
                    HandleReturnFromMessageForm(ReturnPar)
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Public Class ReturnType
            Public LastOperation As ElitaPlusPage.DetailPageCommand
            Public MessageId As Guid
            Public HasDataChanged As Boolean = False

            Public Sub New(LastOp As ElitaPlusPage.DetailPageCommand, MessageId As Guid, Optional ByVal HasDataChanged As Boolean = False)
                LastOperation = LastOp
                Me.MessageId = MessageId
                Me.HasDataChanged = HasDataChanged
            End Sub
        End Class
#End Region

#Region "Page_Events"

        Private Sub Page_PageCall(CallFromUrl As String, CallingPar As Object) Handles MyBase.PageCall
            Try
                Dim callObj As OcMessageSearchForm.CallType = CType(CallingPar, OcMessageSearchForm.CallType)

                If callObj IsNot Nothing Then
                    State.FromDetailPage = True
                    If Not String.IsNullOrEmpty(callObj.search_by) Then
                        State.SearchBy = callObj.search_by
                        State.ConditionMask = callObj.search_value
                        State.ConditionId = callObj.search_id
                        State.DealerId = callObj.dealer_id
                    End If
                End If

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
            Try
                MasterPage.MessageController.Clear_Hide()

                If Not Page.IsPostBack Then
                    MasterPage.MessageController.Clear()
                    MasterPage.UsePageTabTitleInBreadCrum = False
                    MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("Tables")

                    TranslateGridHeader(Grid)
                    UpdateBreadCrum()

                    ControlMgr.SetVisibleControl(Me, trPageSize, False)
                    SortDirection = State.SortExpression
                    PopulateDropdown()

                    If State.DealerId <> Guid.Empty Then
                        DealerMultipleDrop.SelectedGuid = State.DealerId
                        State.IsGridVisible = True
                    End If

                    If State.FromDetailPage Then
                        If Not String.IsNullOrEmpty(State.SearchBy) Then
                            Select Case State.SearchBy
                                Case "cert_number"
                                    OcMessageSearchByDropDown.SelectedValue = "CERTIFICATE"
                                Case "claim_number"
                                    OcMessageSearchByDropDown.SelectedValue = "CLAIM"
                                Case "case_number"
                                    OcMessageSearchByDropDown.SelectedValue = "CASE"
                            End Select

                            txtSearchValue.Text = State.ConditionMask

                            State.IsGridVisible = True
                            btnSendAdhocMsg.Enabled = True

                            DealerMultipleDrop.CodeDropDown.Enabled = False
                            DealerMultipleDrop.CodeTextBox.Enabled = False
                            DealerMultipleDrop.DescDropDown.Enabled = False
                            DealerMultipleDrop.DescriptionTextBox.Enabled = False

                            OcMessageSearchByDropDown.Enabled = False
                            txtSearchValue.Enabled = False

                            moBtnClear.Enabled = False
                            moBtnSearch.Enabled = False
                            btnBack.Enabled = True
                        End If
                    Else
                        If Not String.IsNullOrEmpty(State.SearchBy) Then
                            Select Case State.SearchBy
                                Case "cert_number"
                                    OcMessageSearchByDropDown.SelectedValue = "CERTIFICATE"
                                Case "claim_number"
                                    OcMessageSearchByDropDown.SelectedValue = "CLAIM"
                                Case "case_number"
                                    OcMessageSearchByDropDown.SelectedValue = "CASE"
                            End Select
                            txtSearchValue.Text = State.ConditionMask
                        End If
                        btnSendAdhocMsg.Enabled = False
                        btnBack.Enabled = False
                    End If

                    If State.IsGridVisible Then
                        moBtnSearch_Click(sender, e)
                    End If

                    SetGridItemStyleColor(Grid)
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

            ShowMissingTranslations(MasterPage.MessageController)
        End Sub
#End Region

#Region "Controlling Logic"
        Private Sub PopulateDropdown()
            PopulateMessageSearchByDropdown()
            PopulateDealer()
        End Sub

        Private Sub PopulateDealer()
            Try
                DealerMultipleDrop.Caption = TranslationBase.TranslateLabelOrMessage(LABEL_DEALER)
                DealerMultipleDrop.AutoPostBackDD = False
                DealerMultipleDrop.NothingSelected = True
                DealerMultipleDrop.SelectedGuid = State.DealerId
                DealerMultipleDrop.BindData(LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies))
            Catch ex As Exception
                MasterPage.MessageController.AddError(MESSAGE_LIST_FORM001)
                MasterPage.MessageController.AddError(ex.Message, False)
                MasterPage.MessageController.Show()
            End Try
        End Sub

        Private Sub BindDataGrid(oDataView As DataView)
            Grid.DataSource = oDataView
            Grid.DataBind()
        End Sub

        Private Sub PopulateGrid(Optional ByVal oAction As String = POPULATE_ACTION_NONE)
            Try
                If (State.SearchDV Is Nothing) Then
                    State.SearchDV = OcMessage.GetMessageSearchDV(DealerMultipleDrop.SelectedGuid, State.SearchBy, State.ConditionMask, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
                End If

                If (State.SearchDV.Count = 0) Then
                    State.bNoRow = True
                    CreateHeaderForEmptyGrid(Grid, SortDirection)
                Else
                    State.bNoRow = False
                    Grid.Enabled = True
                End If

                State.SearchDV.Sort = State.SortExpression
                Grid.AutoGenerateColumns = False

                Grid.Columns(GRID_COL_TEMPLATE_DESCRIPTION).SortExpression = OcMessage.MessageSearchDV.COL_TEMPLATE_DESCRIPTION
                Grid.Columns(GRID_COL_SENDER_REASON).SortExpression = OcMessage.MessageSearchDV.COL_SENDER_REASON
                Grid.Columns(GRID_COL_RECIPIENT_ADDRESS).SortExpression = OcMessage.MessageSearchDV.COL_RECIPIENT_ADDRESS
                Grid.Columns(GRID_COL_LAST_ATTEMPTED_ON).SortExpression = OcMessage.MessageSearchDV.COL_LAST_ATTEMPTED_ON
                Grid.Columns(GRID_COL_LAST_ATTEMPTED_STATUS).SortExpression = OcMessage.MessageSearchDV.COL_LAST_ATTEMPTED_STATUS
                Grid.Columns(GRID_COL_CERT_NUMBER).SortExpression = OcMessage.MessageSearchDV.COL_CERT_NUMBER
                Grid.Columns(GRID_COL_CLAIM_NUMBER).SortExpression = OcMessage.MessageSearchDV.COL_CLAIM_NUMBER
                Grid.Columns(GRID_COL_CASE_NUMBER).SortExpression = OcMessage.MessageSearchDV.COL_CASE_NUMBER

                HighLightSortColumn(Grid, State.SortExpression)

                SetPageAndSelectedIndexFromGuid(State.SearchDV, State.MessageId, Grid, State.PageIndex)

                Grid.DataSource = State.SearchDV
                HighLightSortColumn(Grid, SortDirection)
                Grid.DataBind()

                ControlMgr.SetVisibleControl(Me, trPageSize, Grid.Visible)

                Session("recCount") = State.SearchDV.Count

                If Not Grid.BottomPagerRow.Visible Then Grid.BottomPagerRow.Visible = True

                lblRecordCount.Text = State.SearchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub ClearSearch()
            DealerMultipleDrop.SelectedIndex = 0
            OcMessageSearchByDropDown.SelectedIndex = 0
            txtSearchValue.Text = Nothing

            State.MessageId = Guid.Empty
            State.SearchBy = Nothing
            State.ConditionMask = Nothing
        End Sub

        Private Sub UpdateBreadCrum()
            If (State IsNot Nothing) Then
                If (State IsNot Nothing) Then
                    MasterPage.BreadCrum = MasterPage.PageTab & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage("MESSAGE_SEARCH")
                    MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("MESSAGE_SEARCH")
                End If
            End If
        End Sub

        Private Sub PopulateMessageSearchByDropdown()
            Try
                Dim messageSearchList As ListItem() = CommonConfigManager.Current.ListManager.GetList("MSGSRCHBY", Thread.CurrentPrincipal.GetLanguageCode())
                OcMessageSearchByDropDown.Populate(messageSearchList, New PopulateOptions() With
                                                  {
                                                    .AddBlankItem = False,
                                                    .ValueFunc = AddressOf PopulateOptions.GetCode,
                                                    .SortFunc = AddressOf PopulateOptions.GetCode
                                                   })
                OcMessageSearchByDropDown.SelectedIndex = 0
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Function GetMessageSearchByCondition(MessageSearchBySelectedValue As String) As String
            Select Case MessageSearchBySelectedValue
                Case "CERTIFICATE"
                    Return "cert_number"
                Case "CLAIM"
                    Return "claim_number"
                Case "CASE"
                    Return "case_number"
                Case Else
                    Return String.Empty
            End Select
        End Function

#End Region

#Region "Datagrid Related"
        Public Property SortDirection() As String
            Get
                Return ViewState("SortDirection").ToString
            End Get
            Set(value As String)
                ViewState("SortDirection") = value
            End Set
        End Property

        Private Sub Grid_PageSizeChanged(source As Object, e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
            Try
                Grid.PageIndex = NewCurrentPageIndex(Grid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
                State.PageIndex = Grid.PageSize
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub Grid_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Grid.PageIndexChanging
            Try
                State.PageIndex = e.NewPageIndex
                State.DealerId = Guid.Empty
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Public Sub RowCreated(sender As System.Object, e As System.Web.UI.WebControls.GridViewRowEventArgs)
            BaseItemCreated(sender, e)
        End Sub

        Private Sub Grid_SortCommand(source As Object, e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles Grid.Sorting
            Try
                Dim spaceIndex As Integer = SortDirection.LastIndexOf(" ")

                If spaceIndex > 0 AndAlso SortDirection.Substring(0, spaceIndex).Equals(e.SortExpression) Then
                    If SortDirection.EndsWith(" ASC") Then
                        SortDirection = e.SortExpression + " DESC"
                    Else
                        SortDirection = e.SortExpression + " ASC"
                    End If
                Else
                    SortDirection = e.SortExpression + " ASC"
                End If
                State.SortExpression = SortDirection
                State.PageIndex = 0
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub Grid_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowDataBound
            Try
                Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
                Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
                Dim btnEditItem As LinkButton
                Dim btnSelect As ImageButton

                If dvRow IsNot Nothing AndAlso Not State.bNoRow Then
                    If itemType = ListItemType.Item OrElse itemType = ListItemType.AlternatingItem OrElse itemType = ListItemType.SelectedItem Then
                        btnSelect = CType(e.Row.Cells(GRID_COL_SELECT_IDX).FindControl("btnSelect"), ImageButton)
                        btnSelect.Visible = True

                        btnEditItem = CType(e.Row.Cells(GRID_COL_EDIT_IDX).FindControl("SelectAction"), LinkButton)
                        btnEditItem.Text = dvRow(OcMessage.MessageSearchDV.COL_TEMPLATE_DESCRIPTION).ToString

                        e.Row.Cells(GRID_COL_SENDER_REASON).Text = dvRow(OcMessage.MessageSearchDV.COL_SENDER_REASON).ToString
                        e.Row.Cells(GRID_COL_RECIPIENT_ADDRESS).Text = dvRow(OcMessage.MessageSearchDV.COL_RECIPIENT_ADDRESS).ToString
                        If (dvRow(OcMessage.MessageSearchDV.COL_LAST_ATTEMPTED_ON) IsNot DBNull.Value) Then
                            e.Row.Cells(GRID_COL_LAST_ATTEMPTED_ON).Text = GetLongDate12FormattedStringNullable(CType(dvRow(OcMessage.MessageSearchDV.COL_LAST_ATTEMPTED_ON), Date))
                        End If
                        e.Row.Cells(GRID_COL_LAST_ATTEMPTED_STATUS).Text = dvRow(OcMessage.MessageSearchDV.COL_LAST_ATTEMPTED_STATUS).ToString

                        e.Row.Cells(GRID_COL_CERT_NUMBER).Text = dvRow(OcMessage.MessageSearchDV.COL_CERT_NUMBER).ToString
                        e.Row.Cells(GRID_COL_CLAIM_NUMBER).Text = dvRow(OcMessage.MessageSearchDV.COL_CLAIM_NUMBER).ToString
                        e.Row.Cells(GRID_COL_CASE_NUMBER).Text = dvRow(OcMessage.MessageSearchDV.COL_CASE_NUMBER).ToString

                        e.Row.Cells(GRID_COL_OC_MESSAGE_ID).Text = GetGuidStringFromByteArray(CType(dvRow(OcMessage.MessageSearchDV.COL_MESSAGE_ID), Byte()))
                        e.Row.Cells(GRID_COL_OC_TEMPLATE_ID).Text = GetGuidStringFromByteArray(CType(dvRow(OcMessage.MessageSearchDV.COL_TEMPLATE_ID), Byte()))

                        If Not dvRow.Row.IsNull(OcMessage.MessageSearchDV.COL_CERTIFICATE_ID) Then
                            e.Row.Cells(GRID_COL_CERTIFICATE_ID).Text = GetGuidStringFromByteArray(CType(dvRow(OcMessage.MessageSearchDV.COL_CERTIFICATE_ID), Byte()))
                        End If
                        If Not dvRow.Row.IsNull(OcMessage.MessageSearchDV.COL_CLAIM_ID) Then
                            e.Row.Cells(GRID_COL_CLAIM_ID).Text = GetGuidStringFromByteArray(CType(dvRow(OcMessage.MessageSearchDV.COL_CLAIM_ID), Byte()))
                        End If
                        If Not dvRow.Row.IsNull(OcMessage.MessageSearchDV.COL_CASE_ID) Then
                            e.Row.Cells(GRID_COL_CASE_ID).Text = GetGuidStringFromByteArray(CType(dvRow(OcMessage.MessageSearchDV.COL_CASE_ID), Byte()))
                        End If
                    End If
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Public Sub RowCommand(source As System.Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs)
            Try
                If e.CommandName = "SelectAction" Then
                    Dim index As Integer = CInt(e.CommandArgument)
                    Dim messageId = New Guid(Grid.Rows(index).Cells(GRID_COL_OC_MESSAGE_ID).Text)
                    State.MessageId = messageId
                    callPage(OcMessageForm.URL, New OcMessageForm.CallType(State.MessageId))
                ElseIf e.CommandName = "SelectRecord" Then
                    Dim index As Integer = CInt(e.CommandArgument)
                    Grid.SelectedIndex = index
                    Select Case OcMessageSearchByDropDown.SelectedValue
                        Case "CERTIFICATE"
                            State.ConditionId = New Guid(Grid.Rows(index).Cells(GRID_COL_CERTIFICATE_ID).Text)
                        Case "CLAIM"
                            State.ConditionId = New Guid(Grid.Rows(index).Cells(GRID_COL_CLAIM_ID).Text)
                        Case "CASE"
                            State.ConditionId = New Guid(Grid.Rows(index).Cells(GRID_COL_CASE_ID).Text)
                    End Select
                    btnSendAdhocMsg.Enabled = True
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub
#End Region

#Region "Button Clicks"
        Private Sub moBtnSearch_Click(sender As System.Object, e As System.EventArgs) Handles moBtnSearch.Click
            If DealerMultipleDrop.SelectedIndex = -1 Then
                moMessageController.Clear()
                moMessageController.AddError(Assurant.ElitaPlus.Common.ErrorCodes.GUI_DEALER_MUST_BE_SELECTED_ERR)
                Exit Sub
            End If

            Try
                If Not State.IsGridVisible Then
                    cboPageSize.SelectedValue = CType(State.PageSize, String)
                    If Not (Grid.PageSize = DEFAULT_NEW_UI_PAGE_SIZE) Then
                        cboPageSize.SelectedValue = CType(State.PageSize, String)
                        Grid.PageSize = State.PageSize
                    End If
                    State.IsGridVisible = True
                End If
                Grid.PageIndex = NO_PAGE_INDEX
                Grid.DataMember = Nothing
                State.SearchDV = Nothing
                SetSession()
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub moBtnClear_Click(sender As System.Object, e As System.EventArgs) Handles moBtnClear.Click
            Try
                ClearSearch()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

#End Region

#Region "State-Management"
        Private Sub SetSession()
            With State
                .SearchBy = GetMessageSearchByCondition(OcMessageSearchByDropDown.SelectedValue)
                .ConditionMask = txtSearchValue.Text.ToUpper
                .DealerId = DealerMultipleDrop.SelectedGuid
                .PageIndex = Grid.PageIndex
                .PageSize = Grid.PageSize
                .PageSort = State.SortExpression
            End With
        End Sub

        Protected Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
            Try
                ReturnToCallingPage()
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

        Protected Sub btnSendAdhocMsg_Click(sender As Object, e As EventArgs) Handles btnSendAdhocMsg.Click
            Try
                callPage(AdhocOcMessageForm.URL, New Tables.AdhocOcMessageForm.CallType(OcMessageSearchByDropDown.SelectedValue, State.ConditionId))
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

#End Region

    End Class

End Namespace