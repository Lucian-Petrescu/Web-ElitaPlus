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

        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
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

            Public Sub New(ByVal searchby As String, ByVal searchvalue As String, ByVal searchid As Guid, ByVal dealerid As Guid)
                Me.search_by = searchby
                Me.search_value = searchvalue
                Me.search_id = searchid
                Me.dealer_id = dealerid
            End Sub

            Public Sub New(ByVal searchby As String, ByVal searchvalue As String, ByVal dealerid As Guid)
                Me.search_by = searchby
                Me.search_value = searchvalue
                Me.dealer_id = dealerid
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

        Private Sub HandleReturnFromMessageForm(ByVal ReturnPar As Object)
            Dim retObj As OcMessageForm.ReturnType = CType(ReturnPar, OcMessageForm.ReturnType)

            If Not retObj Is Nothing AndAlso retObj.HasDataChanged Then
                Me.State.SearchDV = Nothing
            End If

            If Not retObj Is Nothing Then
                Select Case retObj.LastOperation
                    Case ElitaPlusPage.DetailPageCommand.Back
                        If Not retObj.MessageId = Guid.Empty Then
                            Me.State.MessageId = retObj.MessageId
                            Me.State.IsGridVisible = True
                        End If
                    Case Else
                        Me.State.MessageId = Guid.Empty
                End Select

                Grid.PageIndex = Me.State.PageIndex
                cboPageSize.SelectedValue = CType(Me.State.PageSize, String)
                Grid.PageSize = Me.State.PageSize
                ControlMgr.SetVisibleControl(Me, trPageSize, Grid.Visible)
            End If
        End Sub

        Private Sub Page_PageReturn(ByVal ReturnFromUrl As String, ByVal ReturnPar As Object) Handles Me.PageReturn
            Try
                Me.MenuEnabled = True
                Me.IsReturningFromChild = True

                If TypeOf ReturnPar Is OcMessageForm.ReturnType Then
                    HandleReturnFromMessageForm(ReturnPar)
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Public Class ReturnType
            Public LastOperation As ElitaPlusPage.DetailPageCommand
            Public MessageId As Guid
            Public HasDataChanged As Boolean = False

            Public Sub New(ByVal LastOp As ElitaPlusPage.DetailPageCommand, ByVal MessageId As Guid, Optional ByVal HasDataChanged As Boolean = False)
                Me.LastOperation = LastOp
                Me.MessageId = MessageId
                Me.HasDataChanged = HasDataChanged
            End Sub
        End Class
#End Region

#Region "Page_Events"

        Private Sub Page_PageCall(ByVal CallFromUrl As String, ByVal CallingPar As Object) Handles MyBase.PageCall
            Try
                Dim callObj As OcMessageSearchForm.CallType = CType(CallingPar, OcMessageSearchForm.CallType)

                If Not callObj Is Nothing Then
                    Me.State.FromDetailPage = True
                    If Not String.IsNullOrEmpty(callObj.search_by) Then
                        Me.State.SearchBy = callObj.search_by
                        Me.State.ConditionMask = callObj.search_value
                        Me.State.ConditionId = callObj.search_id
                        Me.State.DealerId = callObj.dealer_id
                    End If
                End If

            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            Try
                Me.MasterPage.MessageController.Clear_Hide()

                If Not Page.IsPostBack Then
                    Me.MasterPage.MessageController.Clear()
                    Me.MasterPage.UsePageTabTitleInBreadCrum = False
                    Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("Tables")

                    TranslateGridHeader(Grid)
                    UpdateBreadCrum()

                    ControlMgr.SetVisibleControl(Me, trPageSize, False)
                    Me.SortDirection = Me.State.SortExpression
                    PopulateDropdown()

                    If Me.State.DealerId <> Guid.Empty Then
                        Me.DealerMultipleDrop.SelectedGuid = Me.State.DealerId
                        Me.State.IsGridVisible = True
                    End If

                    If Me.State.FromDetailPage Then
                        If Not String.IsNullOrEmpty(Me.State.SearchBy) Then
                            Select Case Me.State.SearchBy
                                Case "cert_number"
                                    Me.OcMessageSearchByDropDown.SelectedValue = "CERTIFICATE"
                                Case "claim_number"
                                    Me.OcMessageSearchByDropDown.SelectedValue = "CLAIM"
                                Case "case_number"
                                    Me.OcMessageSearchByDropDown.SelectedValue = "CASE"
                            End Select

                            Me.txtSearchValue.Text = Me.State.ConditionMask

                            Me.State.IsGridVisible = True
                            Me.btnSendAdhocMsg.Enabled = True

                            Me.DealerMultipleDrop.CodeDropDown.Enabled = False
                            Me.DealerMultipleDrop.CodeTextBox.Enabled = False
                            Me.DealerMultipleDrop.DescDropDown.Enabled = False
                            Me.DealerMultipleDrop.DescriptionTextBox.Enabled = False

                            Me.OcMessageSearchByDropDown.Enabled = False
                            Me.txtSearchValue.Enabled = False

                            Me.moBtnClear.Enabled = False
                            Me.moBtnSearch.Enabled = False
                            Me.btnBack.Enabled = True
                        End If
                    Else
                        If Not String.IsNullOrEmpty(Me.State.SearchBy) Then
                            Select Case Me.State.SearchBy
                                Case "cert_number"
                                    Me.OcMessageSearchByDropDown.SelectedValue = "CERTIFICATE"
                                Case "claim_number"
                                    Me.OcMessageSearchByDropDown.SelectedValue = "CLAIM"
                                Case "case_number"
                                    Me.OcMessageSearchByDropDown.SelectedValue = "CASE"
                            End Select
                            Me.txtSearchValue.Text = Me.State.ConditionMask
                        End If
                        Me.btnSendAdhocMsg.Enabled = False
                        Me.btnBack.Enabled = False
                    End If

                    If Me.State.IsGridVisible Then
                        moBtnSearch_Click(sender, e)
                    End If

                    Me.SetGridItemStyleColor(Me.Grid)
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try

            Me.ShowMissingTranslations(Me.MasterPage.MessageController)
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
                DealerMultipleDrop.SelectedGuid = Me.State.DealerId
                DealerMultipleDrop.BindData(LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies))
            Catch ex As Exception
                Me.MasterPage.MessageController.AddError(MESSAGE_LIST_FORM001)
                Me.MasterPage.MessageController.AddError(ex.Message, False)
                Me.MasterPage.MessageController.Show()
            End Try
        End Sub

        Private Sub BindDataGrid(ByVal oDataView As DataView)
            Grid.DataSource = oDataView
            Grid.DataBind()
        End Sub

        Private Sub PopulateGrid(Optional ByVal oAction As String = POPULATE_ACTION_NONE)
            Try
                If (Me.State.SearchDV Is Nothing) Then
                    Me.State.SearchDV = OcMessage.GetMessageSearchDV(DealerMultipleDrop.SelectedGuid, Me.State.SearchBy, Me.State.ConditionMask, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
                End If

                If (Me.State.SearchDV.Count = 0) Then
                    Me.State.bNoRow = True
                    CreateHeaderForEmptyGrid(Grid, Me.SortDirection)
                Else
                    Me.State.bNoRow = False
                    Me.Grid.Enabled = True
                End If

                Me.State.SearchDV.Sort = Me.State.SortExpression
                Grid.AutoGenerateColumns = False

                Grid.Columns(Me.GRID_COL_TEMPLATE_DESCRIPTION).SortExpression = OcMessage.MessageSearchDV.COL_TEMPLATE_DESCRIPTION
                Grid.Columns(Me.GRID_COL_SENDER_REASON).SortExpression = OcMessage.MessageSearchDV.COL_SENDER_REASON
                Grid.Columns(Me.GRID_COL_RECIPIENT_ADDRESS).SortExpression = OcMessage.MessageSearchDV.COL_RECIPIENT_ADDRESS
                Grid.Columns(Me.GRID_COL_LAST_ATTEMPTED_ON).SortExpression = OcMessage.MessageSearchDV.COL_LAST_ATTEMPTED_ON
                Grid.Columns(Me.GRID_COL_LAST_ATTEMPTED_STATUS).SortExpression = OcMessage.MessageSearchDV.COL_LAST_ATTEMPTED_STATUS
                Grid.Columns(Me.GRID_COL_CERT_NUMBER).SortExpression = OcMessage.MessageSearchDV.COL_CERT_NUMBER
                Grid.Columns(Me.GRID_COL_CLAIM_NUMBER).SortExpression = OcMessage.MessageSearchDV.COL_CLAIM_NUMBER
                Grid.Columns(Me.GRID_COL_CASE_NUMBER).SortExpression = OcMessage.MessageSearchDV.COL_CASE_NUMBER

                HighLightSortColumn(Grid, Me.State.SortExpression)

                SetPageAndSelectedIndexFromGuid(Me.State.SearchDV, Me.State.MessageId, Me.Grid, Me.State.PageIndex)

                Me.Grid.DataSource = Me.State.SearchDV
                HighLightSortColumn(Grid, Me.SortDirection)
                Me.Grid.DataBind()

                ControlMgr.SetVisibleControl(Me, trPageSize, Grid.Visible)

                Session("recCount") = Me.State.SearchDV.Count

                If Not Grid.BottomPagerRow.Visible Then Grid.BottomPagerRow.Visible = True

                Me.lblRecordCount.Text = Me.State.SearchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)

            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub ClearSearch()
            DealerMultipleDrop.SelectedIndex = 0
            OcMessageSearchByDropDown.SelectedIndex = 0
            txtSearchValue.Text = Nothing

            Me.State.MessageId = Guid.Empty
            Me.State.SearchBy = Nothing
            Me.State.ConditionMask = Nothing
        End Sub

        Private Sub UpdateBreadCrum()
            If (Not Me.State Is Nothing) Then
                If (Not Me.State Is Nothing) Then
                    Me.MasterPage.BreadCrum = Me.MasterPage.PageTab & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage("MESSAGE_SEARCH")
                    Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("MESSAGE_SEARCH")
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
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Function GetMessageSearchByCondition(ByVal MessageSearchBySelectedValue As String) As String
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
            Set(ByVal value As String)
                ViewState("SortDirection") = value
            End Set
        End Property

        Private Sub Grid_PageSizeChanged(ByVal source As Object, ByVal e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
            Try
                Grid.PageIndex = NewCurrentPageIndex(Grid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
                Me.State.PageIndex = Grid.PageSize
                Me.PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub Grid_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Grid.PageIndexChanging
            Try
                Me.State.PageIndex = e.NewPageIndex
                Me.State.DealerId = Guid.Empty
                Me.PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Public Sub RowCreated(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
            BaseItemCreated(sender, e)
        End Sub

        Private Sub Grid_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles Grid.Sorting
            Try
                Dim spaceIndex As Integer = Me.SortDirection.LastIndexOf(" ")

                If spaceIndex > 0 AndAlso Me.SortDirection.Substring(0, spaceIndex).Equals(e.SortExpression) Then
                    If Me.SortDirection.EndsWith(" ASC") Then
                        Me.SortDirection = e.SortExpression + " DESC"
                    Else
                        Me.SortDirection = e.SortExpression + " ASC"
                    End If
                Else
                    Me.SortDirection = e.SortExpression + " ASC"
                End If
                Me.State.SortExpression = Me.SortDirection
                Me.State.PageIndex = 0
                Me.PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub Grid_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowDataBound
            Try
                Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
                Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
                Dim btnEditItem As LinkButton
                Dim btnSelect As ImageButton

                If Not dvRow Is Nothing And Not Me.State.bNoRow Then
                    If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then
                        btnSelect = CType(e.Row.Cells(Me.GRID_COL_SELECT_IDX).FindControl("btnSelect"), ImageButton)
                        btnSelect.Visible = True

                        btnEditItem = CType(e.Row.Cells(Me.GRID_COL_EDIT_IDX).FindControl("SelectAction"), LinkButton)
                        btnEditItem.Text = dvRow(OcMessage.MessageSearchDV.COL_TEMPLATE_DESCRIPTION).ToString

                        e.Row.Cells(Me.GRID_COL_SENDER_REASON).Text = dvRow(OcMessage.MessageSearchDV.COL_SENDER_REASON).ToString
                        e.Row.Cells(Me.GRID_COL_RECIPIENT_ADDRESS).Text = dvRow(OcMessage.MessageSearchDV.COL_RECIPIENT_ADDRESS).ToString
                        e.Row.Cells(Me.GRID_COL_LAST_ATTEMPTED_ON).Text = dvRow(OcMessage.MessageSearchDV.COL_LAST_ATTEMPTED_ON).ToString
                        e.Row.Cells(Me.GRID_COL_LAST_ATTEMPTED_STATUS).Text = dvRow(OcMessage.MessageSearchDV.COL_LAST_ATTEMPTED_STATUS).ToString

                        e.Row.Cells(Me.GRID_COL_CERT_NUMBER).Text = dvRow(OcMessage.MessageSearchDV.COL_CERT_NUMBER).ToString
                        e.Row.Cells(Me.GRID_COL_CLAIM_NUMBER).Text = dvRow(OcMessage.MessageSearchDV.COL_CLAIM_NUMBER).ToString
                        e.Row.Cells(Me.GRID_COL_CASE_NUMBER).Text = dvRow(OcMessage.MessageSearchDV.COL_CASE_NUMBER).ToString

                        e.Row.Cells(Me.GRID_COL_OC_MESSAGE_ID).Text = GetGuidStringFromByteArray(CType(dvRow(OcMessage.MessageSearchDV.COL_MESSAGE_ID), Byte()))
                        e.Row.Cells(Me.GRID_COL_OC_TEMPLATE_ID).Text = GetGuidStringFromByteArray(CType(dvRow(OcMessage.MessageSearchDV.COL_TEMPLATE_ID), Byte()))

                        If Not dvRow.Row.IsNull(OcMessage.MessageSearchDV.COL_CERTIFICATE_ID) Then
                            e.Row.Cells(Me.GRID_COL_CERTIFICATE_ID).Text = GetGuidStringFromByteArray(CType(dvRow(OcMessage.MessageSearchDV.COL_CERTIFICATE_ID), Byte()))
                        End If
                        If Not dvRow.Row.IsNull(OcMessage.MessageSearchDV.COL_CLAIM_ID) Then
                            e.Row.Cells(Me.GRID_COL_CLAIM_ID).Text = GetGuidStringFromByteArray(CType(dvRow(OcMessage.MessageSearchDV.COL_CLAIM_ID), Byte()))
                        End If
                        If Not dvRow.Row.IsNull(OcMessage.MessageSearchDV.COL_CASE_ID) Then
                            e.Row.Cells(Me.GRID_COL_CASE_ID).Text = GetGuidStringFromByteArray(CType(dvRow(OcMessage.MessageSearchDV.COL_CASE_ID), Byte()))
                        End If
                    End If
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Public Sub RowCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)
            Try
                If e.CommandName = "SelectAction" Then
                    Dim index As Integer = CInt(e.CommandArgument)
                    Dim messageId = New Guid(Me.Grid.Rows(index).Cells(Me.GRID_COL_OC_MESSAGE_ID).Text)
                    Me.State.MessageId = messageId
                    Me.callPage(OcMessageForm.URL, New OcMessageForm.CallType(Me.State.MessageId))
                ElseIf e.CommandName = "SelectRecord" Then
                    Dim index As Integer = CInt(e.CommandArgument)
                    Grid.SelectedIndex = index
                    Select Case Me.OcMessageSearchByDropDown.SelectedValue
                        Case "CERTIFICATE"
                            Me.State.ConditionId = New Guid(Me.Grid.Rows(index).Cells(Me.GRID_COL_CERTIFICATE_ID).Text)
                        Case "CLAIM"
                            Me.State.ConditionId = New Guid(Me.Grid.Rows(index).Cells(Me.GRID_COL_CLAIM_ID).Text)
                        Case "CASE"
                            Me.State.ConditionId = New Guid(Me.Grid.Rows(index).Cells(Me.GRID_COL_CASE_ID).Text)
                    End Select
                    Me.btnSendAdhocMsg.Enabled = True
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try

        End Sub
#End Region

#Region "Button Clicks"
        Private Sub moBtnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles moBtnSearch.Click
            If Me.DealerMultipleDrop.SelectedIndex = -1 Then
                Me.moMessageController.Clear()
                Me.moMessageController.AddError(Assurant.ElitaPlus.Common.ErrorCodes.GUI_DEALER_MUST_BE_SELECTED_ERR)
                Exit Sub
            End If

            Try
                If Not State.IsGridVisible Then
                    cboPageSize.SelectedValue = CType(State.PageSize, String)
                    If Not (Grid.PageSize = DEFAULT_NEW_UI_PAGE_SIZE) Then
                        cboPageSize.SelectedValue = CType(State.PageSize, String)
                        Grid.PageSize = State.PageSize
                    End If
                    Me.State.IsGridVisible = True
                End If
                Grid.PageIndex = Me.NO_PAGE_INDEX
                Grid.DataMember = Nothing
                Me.State.SearchDV = Nothing
                SetSession()
                Me.PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub moBtnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles moBtnClear.Click
            Try
                ClearSearch()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

#End Region

#Region "State-Management"
        Private Sub SetSession()
            With Me.State
                .SearchBy = GetMessageSearchByCondition(Me.OcMessageSearchByDropDown.SelectedValue)
                .ConditionMask = txtSearchValue.Text.ToUpper
                .DealerId = DealerMultipleDrop.SelectedGuid
                .PageIndex = Grid.PageIndex
                .PageSize = Grid.PageSize
                .PageSort = Me.State.SortExpression
            End With
        End Sub

        Protected Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
            Try
                Me.ReturnToCallingPage()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

        Protected Sub btnSendAdhocMsg_Click(sender As Object, e As EventArgs) Handles btnSendAdhocMsg.Click
            Try
                Me.callPage(AdhocOcMessageForm.URL, New Tables.AdhocOcMessageForm.CallType(Me.OcMessageSearchByDropDown.SelectedValue, Me.State.ConditionId))
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

#End Region

    End Class

End Namespace