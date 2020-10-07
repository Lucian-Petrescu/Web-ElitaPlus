Imports System.Threading
Imports Assurant.ElitaPlus.Security

Partial Class MaintainUserList
    Inherits ElitaPlusSearchPage

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    Private Sub Page_Init(sender As System.Object, e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Constants"
    Public Const GRID_COL_NETWORKID_IDX As Integer = 0
    Public Const GRID_COL_NAME_IDX As Integer = 1
    Public Const GRID_COL_ROLE_IDX As Integer = 2
    Public Const GRID_COL_COMPANY_IDX As Integer = 3
    Public Const GRID_COL_ACTIVE_IDX As Integer = 4
    Public Const CALLER_QUEUE_USER As String = "QUEUE_USER"
    Public Const CALLER_QUEUE_STAFFING As String = "QUEUE_STAFFING"
    Public Const GRID_COL_EDIT_CTRL As String = "btnEditCertificate"
    Public Const SELECT_ACTION_COMMAND As String = "SelectAction"

    Public Const GRID_TOTAL_COLUMNS As Integer = 5

    Public Const URL As String = "~/businessobjects/MaintainUserList.aspx"
    Public Const STAFFING_QUERYSTRING As String = "?CALLER=" & CALLER_QUEUE_STAFFING
#End Region

#Region "Page State"
    Private IsReturningFromChild As Boolean = False

    Class MyState
        Public PageIndex As Integer = 0
        Public SortExpression As String = Assurant.ElitaPlus.BusinessObjectsNew.User.UserSearchDV.COL_NETWORK_ID
        Public selectedUserId As Guid = Guid.Empty
        Public NetworkUserId As Guid = Guid.Empty
        Public SearchCountryId As Guid
        Public UserNetworkID As String 'serviceCenterCode
        Public UserName As String 'serviceCenterDescription
        Public UserRole As String 'address1
        Public UserAuthLimit As String 'city
        Public UserCompanyCode As String 'zip
        Public selectedPageSize As Integer = DEFAULT_NEW_UI_PAGE_SIZE
        Public IsGridVisible As Boolean
        Public searchDV As Assurant.ElitaPlus.BusinessObjectsNew.User.UserSearchDV = Nothing
        Public HasDataChanged As Boolean
        Public searchBtnClicked As Boolean = False
        Public bnoRow As Boolean = False
        Public Caller_Form As String = String.Empty
        Public WorkqueueId As Guid = Guid.Empty
        Public WorkqueueName As String = String.Empty
        Public WorkqueueCompanyCode As String = String.Empty
        Public WorkqueueAvailableItems As String = String.Empty
        Public WorkQueueReturnType As WorkQueueListForm.WorkQueueReturnType
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


#Region "Parameters"

    Public Class Parameters
        Public WorkQueueId As Guid
        Public WorkQueueName As String
        Public WorkQueueCompanyCode As String
        Public AvailableItems As String
        Public WorkQueueReturnType As WorkQueueListForm.WorkQueueReturnType

        Public Sub New(strWorkQueueName As String, oWorkQueueId As Guid, strWorkQueueCompanyCode As String, strAvailableItems As String,
                       WorkQueueReturnType As WorkQueueListForm.WorkQueueReturnType)
            WorkQueueId = oWorkQueueId
            WorkQueueName = strWorkQueueName
            WorkQueueCompanyCode = strWorkQueueCompanyCode
            AvailableItems = strAvailableItems
            Me.WorkQueueReturnType = WorkQueueReturnType
        End Sub

    End Class

#End Region

    Private Sub Page_PageCall(CallFromUrl As String, CallingPar As Object) Handles MyBase.PageCall
        Dim oParam As Parameters
        Try
            If CallingParameters IsNot Nothing Then
                MenuEnabled = False
                State.selectedUserId = Guid.Empty
                oParam = CType(CallingParameters, Parameters)
                State.WorkqueueId = oParam.WorkQueueId
                State.WorkqueueName = oParam.WorkQueueName
                State.WorkqueueCompanyCode = oParam.WorkQueueCompanyCode
                State.WorkqueueAvailableItems = oParam.AvailableItems
                State.WorkQueueReturnType = oParam.WorkQueueReturnType
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Page_PageReturn(ReturnFromUrl As String, ReturnPar As Object) Handles MyBase.PageReturn
        Try
            MenuEnabled = True
            IsReturningFromChild = True
            Dim retObj As business.MaintainUser.ReturnType = CType(ReturnPar, business.MaintainUser.ReturnType)
            State.HasDataChanged = retObj.HasDataChanged
            If retObj IsNot Nothing AndAlso retObj.HasDataChanged Then
                State.searchDV = Nothing
            End If
            State.IsGridVisible = True
            Select Case retObj.LastOperation
                Case ElitaPlusPage.DetailPageCommand.Back
                    If retObj IsNot Nothing Then
                        If Not retObj.EditingBo.IsNew Then
                            State.selectedUserId = retObj.EditingBo.Id
                        End If
                        '     Me.State.IsGridVisible = True
                    End If
                Case ElitaPlusPage.DetailPageCommand.Delete
                    AddInfoMsg(Message.DELETE_RECORD_CONFIRMATION)
            End Select
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

#End Region
#Region "Page_Events"
    Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Try
            MasterPage.MessageController.Clear()
            If Not IsPostBack Then

                SortDirection = State.SortExpression
                State.Caller_Form = Request.QueryString.Get("CALLER")
                UpdateBreadCrum()
                'Enable Diable Add and Back Button when required
                btnBack.Visible = False
                If State.Caller_Form = CALLER_QUEUE_USER OrElse State.Caller_Form = CALLER_QUEUE_STAFFING Then
                    If Assurant.Elita.Configuration.ElitaConfig.Current.General.IntegrateWorkQueueImagingServices = False Then
                        PlaceHolder1.Visible = False
                        PlaceHolder2.Visible = False
                        'Throw New GUIException(Message.MSG_GUI_INVALID_VALUE, ElitaPlus.Common.ErrorCodes.GUI_INVALID_VALUE)
                        moMessageController.AddWarning(Assurant.ElitaPlus.Common.ErrorCodes.GUI_ERROR_DISABLE_FUNCTIONALITY, True)
                        Throw New GUIException("", "")
                    End If
                    btnAdd_WRITE.Visible = False
                    If State.Caller_Form = CALLER_QUEUE_STAFFING Then
                        btnBack.Visible = True
                        State.UserCompanyCode = State.WorkqueueCompanyCode
                        TextBoxSearchCompanyCode.Enabled = False
                    End If
                Else
                    '' Check if User is member of LDAP Group defined in Configuration
                    If (Thread.CurrentPrincipal.CanManageUsers()) Then
                        btnAdd_WRITE.Visible = True
                        moMessageController.Clear()
                    Else
                        btnAdd_WRITE.Visible = False
                        moMessageController.AddWarning(ElitaPlus.Common.ErrorCodes.GuiErrorMissingLdapPermission, True)
                    End If
                End If

                SetDefaultButton(TextBoxSearchID, btnSearch)
                SetDefaultButton(TextBoxSearchName, btnSearch)
                SetDefaultButton(TextBoxSearchRole, btnSearch)
                SetDefaultButton(TextBoxSearchCompanyCode, btnSearch)
                ControlMgr.SetVisibleControl(Me, moSearchResults, False)
                PopulateSearchFieldsFromState()
                cboPageSize.SelectedValue = CType(State.selectedPageSize, String)
                Grid.PageSize = State.selectedPageSize
                If State.IsGridVisible Then
                    PopulateGrid()
                End If
                SetGridItemStyleColor(Grid)


            End If
        Catch ex As GUIException

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
        ShowMissingTranslations(MasterPage.MessageController)
    End Sub
#End Region

#Region "Controlling Logic"

    'Private Sub PopulateCountry()
    '    Me.BindListControlToDataView(moCountryDrop, LookupListNew.GetUserCountriesLookupList())
    '    If moCountryDrop.Items.Count < 3 Then
    '        ControlMgr.SetVisibleControl(Me, moCountryLabel, False)
    '        ControlMgr.SetVisibleControl(Me, moCountryColonLabel_NO_TRANSLATE, False)
    '        ControlMgr.SetVisibleControl(Me, moCountryDrop, False)
    '    End If
    'End Sub

    Public Sub PopulateGrid()

        PopulateStateFromSearchFields()

        If ((State.searchDV Is Nothing) OrElse (State.HasDataChanged)) Then
            State.searchDV = Assurant.ElitaPlus.BusinessObjectsNew.User.GetUserNewList(State.UserNetworkID,
                                                      State.UserName,
                                                      State.UserRole,
                                                      State.UserCompanyCode)

            'Ticket # 748479 - Search grids in Tables tab should not show pop-up message when number of retrieved record is over 1,000
            'If (Not (Me.State.HasDataChanged)) Then
            'Me.ValidSearchResultCount(Me.State.searchDV.Count, True)
            'End If
        End If

        If Not (State.searchDV Is Nothing) Then
            State.searchDV.Sort = SortDirection

            Grid.AutoGenerateColumns = False

            SetPageAndSelectedIndexFromGuid(State.searchDV, State.selectedUserId, Grid, State.PageIndex)
            SortAndBindGrid()
        End If

    End Sub

    Private Sub SortAndBindGrid()
        State.PageIndex = Grid.PageIndex

        If (State.searchDV.Count = 0) Then

            State.bnoRow = True
            CreateHeaderForEmptyGrid(Grid, SortDirection)
        Else
            State.bnoRow = False
            Grid.Enabled = True
            Grid.DataSource = State.searchDV
            HighLightSortColumn(Grid, SortDirection)
            Grid.DataBind()
        End If

        If Not Grid.BottomPagerRow.Visible Then Grid.BottomPagerRow.Visible = True

        ControlMgr.SetVisibleControl(Me, moSearchResults, State.IsGridVisible)

        Session("recCount") = State.searchDV.Count

        If State.searchDV.Count > 0 Then

            If State.IsGridVisible Then
                lblRecordCount.Text = State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
            End If
        Else
            If State.IsGridVisible Then
                lblRecordCount.Text = State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
            End If
        End If
    End Sub

    Public Sub PopulateSearchFieldsFromState()
        ' Me.SetSelectedItem(moCountryDrop, Me.State.SearchCountryId)
        TextBoxSearchID.Text = State.UserNetworkID
        TextBoxSearchName.Text = State.UserName
        TextBoxSearchRole.Text = State.UserRole
        TextBoxSearchCompanyCode.Text = State.UserCompanyCode
        'Me.TextBoxSearchZip.Text = Me.State.zip
    End Sub

    Public Sub PopulateStateFromSearchFields()
        'Me.State.SearchCountryId = Me.GetSelectedItem(moCountryDrop)
        State.UserNetworkID = TextBoxSearchID.Text
        If (Not (TextBoxSearchName.Text Is Nothing)) Then
            State.UserName = TextBoxSearchName.Text.ToUpper
        Else
            State.UserName = Nothing
        End If
        State.UserRole = TextBoxSearchRole.Text
        State.UserCompanyCode = TextBoxSearchCompanyCode.Text
        'Me.State.zip = Me.TextBoxSearchZip.Text
    End Sub
    'This method will change the Page Index and the Selected Index
    Public Function FindDVSelectedRowIndex(dv As Assurant.ElitaPlus.BusinessObjectsNew.User.UserSearchDV) As Integer
        If State.selectedUserId.Equals(Guid.Empty) Then
            Return -1
        Else
            'Jump to the Right Page
            Dim i As Integer
            For i = 0 To dv.Count - 1
                If New Guid(CType(dv(i)(Assurant.ElitaPlus.BusinessObjectsNew.User.UserSearchDV.COL_USER_ID), Byte())).Equals(State.selectedUserId) Then
                    Return i
                End If
            Next
        End If
        Return -1
    End Function


#End Region

#Region "Bread Crum"
    Private Sub UpdateBreadCrum()
        MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage("Admin")
        MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("USER_SEARCH")
        MasterPage.UsePageTabTitleInBreadCrum = False
        Select Case State.Caller_Form
            Case CALLER_QUEUE_USER
                MasterPage.BreadCrum = TranslationBase.TranslateLabelOrMessage("TABLES") + ElitaBase.Sperator + TranslationBase.TranslateLabelOrMessage("WORK_QUEUE")
                MasterPage.BreadCrum = MasterPage.BreadCrum & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage("QUEUE_USER")
            Case CALLER_QUEUE_STAFFING
                MasterPage.BreadCrum = TranslationBase.TranslateLabelOrMessage("TABLES") + ElitaBase.Sperator +
                    TranslationBase.TranslateLabelOrMessage("WORK_QUEUE_USERS") + ElitaBase.Sperator + TranslationBase.TranslateLabelOrMessage("User")
            Case Else
                MasterPage.BreadCrum = TranslationBase.TranslateLabelOrMessage("Admin") + ElitaBase.Sperator + TranslationBase.TranslateLabelOrMessage("User")
        End Select
    End Sub
#End Region


#Region " Datagrid Related "
    Public Property SortDirection() As String
        Get
            Return ViewState("SortDirection").ToString
        End Get
        Set(value As String)
            ViewState("SortDirection") = value
        End Set
    End Property

    'The Binding LOgic is here
    Private Sub Grid_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowDataBound
        Dim btnEditItem As LinkButton
        Try
            Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
            Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
            If dvRow IsNot Nothing AndAlso Not State.bnoRow Then
                If itemType = ListItemType.Item OrElse itemType = ListItemType.AlternatingItem OrElse itemType = ListItemType.SelectedItem Then
                    e.Row.Cells(GRID_COL_NETWORKID_IDX).Text = dvRow(Assurant.ElitaPlus.BusinessObjectsNew.User.UserSearchDV.COL_NETWORK_ID).ToString
                    If (e.Row.Cells(GRID_COL_NAME_IDX).FindControl(GRID_COL_EDIT_CTRL) IsNot Nothing) Then
                        btnEditItem = CType(e.Row.Cells(GRID_COL_NAME_IDX).FindControl(GRID_COL_EDIT_CTRL), LinkButton)
                        btnEditItem.CommandArgument = GetGuidStringFromByteArray(CType(dvRow(Assurant.ElitaPlus.BusinessObjectsNew.User.UserSearchDV.COL_USER_ID), Byte()))
                        btnEditItem.CommandName = SELECT_ACTION_COMMAND
                        btnEditItem.Text = dvRow(Assurant.ElitaPlus.BusinessObjectsNew.User.UserSearchDV.COL_USER_NAME).ToString
                    End If
                    e.Row.Cells(GRID_COL_ROLE_IDX).Text = dvRow(Assurant.ElitaPlus.BusinessObjectsNew.User.UserSearchDV.COL_ROLE_CODE).ToString().Replace(",", ", ")
                    e.Row.Cells(GRID_COL_COMPANY_IDX).Text = dvRow(Assurant.ElitaPlus.BusinessObjectsNew.User.UserSearchDV.COL_COMPANY_CODE).ToString().Replace(",", ", ")
                    If (dvRow(Assurant.ElitaPlus.BusinessObjectsNew.User.UserSearchDV.COL_ACTIVE).ToString = "Y") Then
                        e.Row.Cells(GRID_COL_ACTIVE_IDX).Text = TranslationBase.TranslateLabelOrMessage("ACTIVE")
                        e.Row.Cells(GRID_COL_ACTIVE_IDX).CssClass = "StatActive"
                    Else
                        e.Row.Cells(GRID_COL_ACTIVE_IDX).Text = TranslationBase.TranslateLabelOrMessage("INACTIVE")
                        e.Row.Cells(GRID_COL_ACTIVE_IDX).CssClass = "StatInactive"
                    End If

                End If
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Grid_PageSizeChanged(source As Object, e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
        Try
            Grid.PageIndex = NewCurrentPageIndex(Grid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
            State.selectedPageSize = CType(cboPageSize.SelectedValue, Integer)
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
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

    Protected Sub ItemCommand(source As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs)
        Dim oUser As User
        Dim bWkQAlreadyAssigned As Boolean = False
        Try
            If e.CommandName = "SelectAction" Then
                State.selectedUserId = New Guid(e.CommandArgument.ToString())
                Select Case State.Caller_Form
                    Case Is = CALLER_QUEUE_USER
                        callPage(business.MaintainQueueUser.URL, State.selectedUserId)
                    Case Is = CALLER_QUEUE_STAFFING
                        oUser = New User(State.selectedUserId)
                        For Each wkQassign As WorkQueueAssign In oUser.WorkQueueAssignChildren
                            If wkQassign.WorkqueueId = State.WorkqueueId Then
                                bWkQAlreadyAssigned = True
                            End If
                        Next
                        If Not bWkQAlreadyAssigned Then
                            WorkQueue.GrantProcessWQPermission(State.WorkqueueName, oUser.NetworkId)
                            Dim CompWQ As WorkQueueAssign = oUser.GetNewWorkQueueAsignChild()
                            CompWQ.BeginEdit()
                            CompWQ.WorkqueueId = New Guid(State.WorkqueueId.ToString)
                            CompWQ.CompanyId = LookupListNew.GetIdFromCode("COMPANIES", State.WorkqueueCompanyCode)
                            CompWQ.EndEdit()
                            CompWQ.Save()
                            oUser.UpdateWorkQueueUserAssign()
                            callPage(WorkQueueUsersForm.URL, New WorkQueueUsersForm.Parameters(State.WorkqueueName, State.WorkqueueId,
                                        State.WorkqueueCompanyCode, State.WorkqueueAvailableItems, oUser.UserName, State.WorkQueueReturnType))
                        Else
                            MasterPage.MessageController.AddInformation(Message.MSG_USER_ALREADY_ADDED_TO_THE_WORK_QUEUE, True)
                        End If
                    Case Else
                        callPage(business.MaintainUser.URL, State.selectedUserId)
                End Select
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try

    End Sub

    Public Sub ItemCreated(sender As System.Object, e As System.Web.UI.WebControls.GridViewRowEventArgs)
        BaseItemCreated(sender, e)
    End Sub

    Private Sub Grid_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Grid.PageIndexChanging
        Try
            State.PageIndex = e.NewPageIndex
            State.selectedUserId = Guid.Empty
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
#End Region

#Region " Button Clicks "

    Private Sub btnSearch_Click(sender As System.Object, e As System.EventArgs) Handles btnSearch.Click
        Try
            State.PageIndex = 0
            State.selectedUserId = Guid.Empty
            State.IsGridVisible = True
            State.searchDV = Nothing
            State.HasDataChanged = False
            State.searchBtnClicked = True
            PopulateGrid()
            State.searchBtnClicked = False
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnAdd_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnAdd_WRITE.Click
        Try
            Select Case State.Caller_Form
                Case Is = CALLER_QUEUE_USER
                    callPage(business.MaintainQueueUser.URL)
                Case Else
                    callPage(business.MaintainUser.URL)
            End Select
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnClearSearch_Click(sender As System.Object, e As System.EventArgs) Handles btnClearSearch.Click
        Try
            TextBoxSearchID.Text = String.Empty
            TextBoxSearchName.Text = String.Empty
            TextBoxSearchRole.Text = String.Empty
            If Not State.Caller_Form = CALLER_QUEUE_STAFFING Then
                TextBoxSearchCompanyCode.Text = String.Empty
            End If
            'Me.TextBoxSearchZip.Text = String.Empty
            'moCountryDrop.SelectedIndex = Me.BLANK_ITEM_SELECTED
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnBack_Click(sender As Object, e As System.EventArgs) Handles btnBack.Click
        Try
            If State.Caller_Form = CALLER_QUEUE_STAFFING Then
                callPage(WorkQueueUsersForm.URL, New WorkQueueUsersForm.Parameters(State.WorkqueueName, State.WorkqueueId,
                                                                   State.WorkqueueCompanyCode, State.WorkqueueAvailableItems, Nothing, State.WorkQueueReturnType))
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

#End Region



End Class

