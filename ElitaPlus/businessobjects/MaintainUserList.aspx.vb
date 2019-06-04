Imports System.Threading
Imports Assurant.ElitaPlus.Security

Partial Class MaintainUserList
    Inherits ElitaPlusSearchPage

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
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

        Public Sub New(ByVal strWorkQueueName As String, ByVal oWorkQueueId As Guid, ByVal strWorkQueueCompanyCode As String, ByVal strAvailableItems As String,
                       ByVal WorkQueueReturnType As WorkQueueListForm.WorkQueueReturnType)
            WorkQueueId = oWorkQueueId
            WorkQueueName = strWorkQueueName
            WorkQueueCompanyCode = strWorkQueueCompanyCode
            AvailableItems = strAvailableItems
            Me.WorkQueueReturnType = WorkQueueReturnType
        End Sub

    End Class

#End Region

    Private Sub Page_PageCall(ByVal CallFromUrl As String, ByVal CallingPar As Object) Handles MyBase.PageCall
        Dim oParam As Parameters
        Try
            If Not Me.CallingParameters Is Nothing Then
                Me.MenuEnabled = False
                Me.State.selectedUserId = Guid.Empty
                oParam = CType(Me.CallingParameters, Parameters)
                Me.State.WorkqueueId = oParam.WorkQueueId
                Me.State.WorkqueueName = oParam.WorkQueueName
                Me.State.WorkqueueCompanyCode = oParam.WorkQueueCompanyCode
                Me.State.WorkqueueAvailableItems = oParam.AvailableItems
                Me.State.WorkQueueReturnType = oParam.WorkQueueReturnType
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Page_PageReturn(ByVal ReturnFromUrl As String, ByVal ReturnPar As Object) Handles MyBase.PageReturn
        Try
            Me.MenuEnabled = True
            Me.IsReturningFromChild = True
            Dim retObj As business.MaintainUser.ReturnType = CType(ReturnPar, business.MaintainUser.ReturnType)
            Me.State.HasDataChanged = retObj.HasDataChanged
            If Not retObj Is Nothing AndAlso retObj.HasDataChanged Then
                Me.State.searchDV = Nothing
            End If
            Me.State.IsGridVisible = True
            Select Case retObj.LastOperation
                Case ElitaPlusPage.DetailPageCommand.Back
                    If Not retObj Is Nothing Then
                        If Not retObj.EditingBo.IsNew Then
                            Me.State.selectedUserId = retObj.EditingBo.Id
                        End If
                        '     Me.State.IsGridVisible = True
                    End If
                Case ElitaPlusPage.DetailPageCommand.Delete
                    Me.AddInfoMsg(Message.DELETE_RECORD_CONFIRMATION)
            End Select
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

#End Region
#Region "Page_Events"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Try
            Me.MasterPage.MessageController.Clear()
            If Not Me.IsPostBack Then

                Me.SortDirection = Me.State.SortExpression
                Me.State.Caller_Form = Request.QueryString.Get("CALLER")
                UpdateBreadCrum()
                'Enable Diable Add and Back Button when required
                btnBack.Visible = False
                If Me.State.Caller_Form = CALLER_QUEUE_USER OrElse Me.State.Caller_Form = CALLER_QUEUE_STAFFING Then
                    If Assurant.Elita.Configuration.ElitaConfig.Current.General.IntegrateWorkQueueImagingServices = False Then
                        PlaceHolder1.Visible = False
                        PlaceHolder2.Visible = False
                        'Throw New GUIException(Message.MSG_GUI_INVALID_VALUE, ElitaPlus.Common.ErrorCodes.GUI_INVALID_VALUE)
                        moMessageController.AddWarning(Assurant.ElitaPlus.Common.ErrorCodes.GUI_ERROR_DISABLE_FUNCTIONALITY, True)
                        Throw New GUIException("", "")
                    End If
                    btnAdd_WRITE.Visible = False
                    If Me.State.Caller_Form = CALLER_QUEUE_STAFFING Then
                        btnBack.Visible = True
                        Me.State.UserCompanyCode = Me.State.WorkqueueCompanyCode
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

                SetDefaultButton(Me.TextBoxSearchID, btnSearch)
                SetDefaultButton(Me.TextBoxSearchName, btnSearch)
                SetDefaultButton(Me.TextBoxSearchRole, btnSearch)
                SetDefaultButton(Me.TextBoxSearchCompanyCode, btnSearch)
                ControlMgr.SetVisibleControl(Me, moSearchResults, False)
                PopulateSearchFieldsFromState()
                cboPageSize.SelectedValue = CType(Me.State.selectedPageSize, String)
                Grid.PageSize = Me.State.selectedPageSize
                If Me.State.IsGridVisible Then
                    Me.PopulateGrid()
                End If
                Me.SetGridItemStyleColor(Me.Grid)


            End If
        Catch ex As GUIException

        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
        Me.ShowMissingTranslations(Me.MasterPage.MessageController)
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

        If ((Me.State.searchDV Is Nothing) OrElse (Me.State.HasDataChanged)) Then
            Me.State.searchDV = Assurant.ElitaPlus.BusinessObjectsNew.User.GetUserNewList(Me.State.UserNetworkID,
                                                      Me.State.UserName,
                                                      Me.State.UserRole,
                                                      Me.State.UserCompanyCode)

            'Ticket # 748479 - Search grids in Tables tab should not show pop-up message when number of retrieved record is over 1,000
            'If (Not (Me.State.HasDataChanged)) Then
            'Me.ValidSearchResultCount(Me.State.searchDV.Count, True)
            'End If
        End If

        If Not (Me.State.searchDV Is Nothing) Then
            Me.State.searchDV.Sort = Me.SortDirection

            Me.Grid.AutoGenerateColumns = False

            SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.selectedUserId, Me.Grid, Me.State.PageIndex)
            Me.SortAndBindGrid()
        End If

    End Sub

    Private Sub SortAndBindGrid()
        Me.State.PageIndex = Me.Grid.PageIndex

        If (Me.State.searchDV.Count = 0) Then

            Me.State.bnoRow = True
            CreateHeaderForEmptyGrid(Grid, Me.SortDirection)
        Else
            Me.State.bnoRow = False
            Me.Grid.Enabled = True
            Me.Grid.DataSource = Me.State.searchDV
            HighLightSortColumn(Grid, Me.SortDirection)
            Me.Grid.DataBind()
        End If

        If Not Grid.BottomPagerRow.Visible Then Grid.BottomPagerRow.Visible = True

        ControlMgr.SetVisibleControl(Me, moSearchResults, Me.State.IsGridVisible)

        Session("recCount") = Me.State.searchDV.Count

        If Me.State.searchDV.Count > 0 Then

            If Me.State.IsGridVisible Then
                Me.lblRecordCount.Text = Me.State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
            End If
        Else
            If Me.State.IsGridVisible Then
                Me.lblRecordCount.Text = Me.State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
            End If
        End If
    End Sub

    Public Sub PopulateSearchFieldsFromState()
        ' Me.SetSelectedItem(moCountryDrop, Me.State.SearchCountryId)
        Me.TextBoxSearchID.Text = Me.State.UserNetworkID
        Me.TextBoxSearchName.Text = Me.State.UserName
        Me.TextBoxSearchRole.Text = Me.State.UserRole
        Me.TextBoxSearchCompanyCode.Text = Me.State.UserCompanyCode
        'Me.TextBoxSearchZip.Text = Me.State.zip
    End Sub

    Public Sub PopulateStateFromSearchFields()
        'Me.State.SearchCountryId = Me.GetSelectedItem(moCountryDrop)
        Me.State.UserNetworkID = Me.TextBoxSearchID.Text
        If (Not (Me.TextBoxSearchName.Text Is Nothing)) Then
            Me.State.UserName = Me.TextBoxSearchName.Text.ToUpper
        Else
            Me.State.UserName = Nothing
        End If
        Me.State.UserRole = Me.TextBoxSearchRole.Text
        Me.State.UserCompanyCode = Me.TextBoxSearchCompanyCode.Text
        'Me.State.zip = Me.TextBoxSearchZip.Text
    End Sub
    'This method will change the Page Index and the Selected Index
    Public Function FindDVSelectedRowIndex(ByVal dv As Assurant.ElitaPlus.BusinessObjectsNew.User.UserSearchDV) As Integer
        If Me.State.selectedUserId.Equals(Guid.Empty) Then
            Return -1
        Else
            'Jump to the Right Page
            Dim i As Integer
            For i = 0 To dv.Count - 1
                If New Guid(CType(dv(i)(Assurant.ElitaPlus.BusinessObjectsNew.User.UserSearchDV.COL_USER_ID), Byte())).Equals(Me.State.selectedUserId) Then
                    Return i
                End If
            Next
        End If
        Return -1
    End Function


#End Region

#Region "Bread Crum"
    Private Sub UpdateBreadCrum()
        Me.MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage("Admin")
        Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("USER_SEARCH")
        Me.MasterPage.UsePageTabTitleInBreadCrum = False
        Select Case Me.State.Caller_Form
            Case CALLER_QUEUE_USER
                Me.MasterPage.BreadCrum = TranslationBase.TranslateLabelOrMessage("TABLES") + ElitaBase.Sperator + TranslationBase.TranslateLabelOrMessage("WORK_QUEUE")
                Me.MasterPage.BreadCrum = Me.MasterPage.BreadCrum & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage("QUEUE_USER")
            Case CALLER_QUEUE_STAFFING
                Me.MasterPage.BreadCrum = TranslationBase.TranslateLabelOrMessage("TABLES") + ElitaBase.Sperator +
                    TranslationBase.TranslateLabelOrMessage("WORK_QUEUE_USERS") + ElitaBase.Sperator + TranslationBase.TranslateLabelOrMessage("User")
            Case Else
                Me.MasterPage.BreadCrum = TranslationBase.TranslateLabelOrMessage("Admin") + ElitaBase.Sperator + TranslationBase.TranslateLabelOrMessage("User")
        End Select
    End Sub
#End Region


#Region " Datagrid Related "
    Public Property SortDirection() As String
        Get
            Return ViewState("SortDirection").ToString
        End Get
        Set(ByVal value As String)
            ViewState("SortDirection") = value
        End Set
    End Property

    'The Binding LOgic is here
    Private Sub Grid_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowDataBound
        Dim btnEditItem As LinkButton
        Try
            Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
            Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
            If Not dvRow Is Nothing And Not Me.State.bnoRow Then
                If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then
                    e.Row.Cells(Me.GRID_COL_NETWORKID_IDX).Text = dvRow(Assurant.ElitaPlus.BusinessObjectsNew.User.UserSearchDV.COL_NETWORK_ID).ToString
                    If (Not e.Row.Cells(Me.GRID_COL_NAME_IDX).FindControl(GRID_COL_EDIT_CTRL) Is Nothing) Then
                        btnEditItem = CType(e.Row.Cells(Me.GRID_COL_NAME_IDX).FindControl(GRID_COL_EDIT_CTRL), LinkButton)
                        btnEditItem.CommandArgument = GetGuidStringFromByteArray(CType(dvRow(Assurant.ElitaPlus.BusinessObjectsNew.User.UserSearchDV.COL_USER_ID), Byte()))
                        btnEditItem.CommandName = SELECT_ACTION_COMMAND
                        btnEditItem.Text = dvRow(Assurant.ElitaPlus.BusinessObjectsNew.User.UserSearchDV.COL_USER_NAME).ToString
                    End If
                    e.Row.Cells(Me.GRID_COL_ROLE_IDX).Text = dvRow(Assurant.ElitaPlus.BusinessObjectsNew.User.UserSearchDV.COL_ROLE_CODE).ToString().Replace(",", ", ")
                    e.Row.Cells(Me.GRID_COL_COMPANY_IDX).Text = dvRow(Assurant.ElitaPlus.BusinessObjectsNew.User.UserSearchDV.COL_COMPANY_CODE).ToString().Replace(",", ", ")
                    If (dvRow(Assurant.ElitaPlus.BusinessObjectsNew.User.UserSearchDV.COL_ACTIVE).ToString = "Y") Then
                        e.Row.Cells(Me.GRID_COL_ACTIVE_IDX).Text = TranslationBase.TranslateLabelOrMessage("ACTIVE")
                        e.Row.Cells(Me.GRID_COL_ACTIVE_IDX).CssClass = "StatActive"
                    Else
                        e.Row.Cells(Me.GRID_COL_ACTIVE_IDX).Text = TranslationBase.TranslateLabelOrMessage("INACTIVE")
                        e.Row.Cells(Me.GRID_COL_ACTIVE_IDX).CssClass = "StatInactive"
                    End If

                End If
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Grid_PageSizeChanged(ByVal source As Object, ByVal e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
        Try
            Grid.PageIndex = NewCurrentPageIndex(Grid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
            Me.State.selectedPageSize = CType(cboPageSize.SelectedValue, Integer)
            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
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

    Protected Sub ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)
        Dim oUser As User
        Dim bWkQAlreadyAssigned As Boolean = False
        Try
            If e.CommandName = "SelectAction" Then
                Me.State.selectedUserId = New Guid(e.CommandArgument.ToString())
                Select Case Me.State.Caller_Form
                    Case Is = CALLER_QUEUE_USER
                        Me.callPage(business.MaintainQueueUser.URL, Me.State.selectedUserId)
                    Case Is = CALLER_QUEUE_STAFFING
                        oUser = New User(Me.State.selectedUserId)
                        For Each wkQassign As WorkQueueAssign In oUser.WorkQueueAssignChildren
                            If wkQassign.WorkqueueId = Me.State.WorkqueueId Then
                                bWkQAlreadyAssigned = True
                            End If
                        Next
                        If Not bWkQAlreadyAssigned Then
                            WorkQueue.GrantProcessWQPermission(Me.State.WorkqueueName, oUser.NetworkId)
                            Dim CompWQ As WorkQueueAssign = oUser.GetNewWorkQueueAsignChild()
                            CompWQ.BeginEdit()
                            CompWQ.WorkqueueId = New Guid(Me.State.WorkqueueId.ToString)
                            CompWQ.CompanyId = LookupListNew.GetIdFromCode("COMPANIES", Me.State.WorkqueueCompanyCode)
                            CompWQ.EndEdit()
                            CompWQ.Save()
                            oUser.UpdateWorkQueueUserAssign()
                            Me.callPage(WorkQueueUsersForm.URL, New WorkQueueUsersForm.Parameters(Me.State.WorkqueueName, Me.State.WorkqueueId,
                                        Me.State.WorkqueueCompanyCode, Me.State.WorkqueueAvailableItems, oUser.UserName, Me.State.WorkQueueReturnType))
                        Else
                            Me.MasterPage.MessageController.AddInformation(Message.MSG_USER_ALREADY_ADDED_TO_THE_WORK_QUEUE, True)
                        End If
                    Case Else
                        Me.callPage(business.MaintainUser.URL, Me.State.selectedUserId)
                End Select
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try

    End Sub

    Public Sub ItemCreated(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
        BaseItemCreated(sender, e)
    End Sub

    Private Sub Grid_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Grid.PageIndexChanging
        Try
            Me.State.PageIndex = e.NewPageIndex
            Me.State.selectedUserId = Guid.Empty
            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub
#End Region

#Region " Button Clicks "

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            Me.State.PageIndex = 0
            Me.State.selectedUserId = Guid.Empty
            Me.State.IsGridVisible = True
            Me.State.searchDV = Nothing
            Me.State.HasDataChanged = False
            Me.State.searchBtnClicked = True
            Me.PopulateGrid()
            Me.State.searchBtnClicked = False
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnAdd_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd_WRITE.Click
        Try
            Select Case Me.State.Caller_Form
                Case Is = CALLER_QUEUE_USER
                    Me.callPage(business.MaintainQueueUser.URL)
                Case Else
                    Me.callPage(business.MaintainUser.URL)
            End Select
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnClearSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearSearch.Click
        Try
            Me.TextBoxSearchID.Text = String.Empty
            Me.TextBoxSearchName.Text = String.Empty
            Me.TextBoxSearchRole.Text = String.Empty
            If Not Me.State.Caller_Form = CALLER_QUEUE_STAFFING Then
                Me.TextBoxSearchCompanyCode.Text = String.Empty
            End If
            'Me.TextBoxSearchZip.Text = String.Empty
            'moCountryDrop.SelectedIndex = Me.BLANK_ITEM_SELECTED
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Try
            If Me.State.Caller_Form = CALLER_QUEUE_STAFFING Then
                Me.callPage(WorkQueueUsersForm.URL, New WorkQueueUsersForm.Parameters(Me.State.WorkqueueName, Me.State.WorkqueueId,
                                                                   Me.State.WorkqueueCompanyCode, Me.State.WorkqueueAvailableItems, Nothing, Me.State.WorkQueueReturnType))
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

#End Region



End Class

