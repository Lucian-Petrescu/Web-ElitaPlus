Imports System.Globalization
Imports System.Threading
Imports Assurant.Elita.Web.Forms
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.CommonConfiguration.DataElements


Public Class ClaimByIssueSearch
    Inherits ElitaPlusSearchPage
    Implements IStateController



#Region "Constants"


    Public Const PAGE_TITLE_NAME = "CLAIM ISSUE SEARCH"

    Public Const GRID_COL_CLAIM_NUMBER_CTRL As String = "btnEditClaim"
    Public Const GRID_COL_CLAIM_NUMBER_IDX As Integer = 0
    Public Const GRID_COL_STATUS_IDX As Integer = 1
    Public Const GRID_COL_AUTHORIZATION_NUMBER_IDX As Integer = 2
    Public Const GRID_COL_SERVICE_CENTER_IDX As Integer = 3
    Public Const GRID_COL_DEALER_IDX As Integer = 4
    Public Const GRID_COL_CLAIM_ISSUE_TYPE_IDX As Integer = 5
    Public Const GRID_COL_CLAIM_ISSUE_STATUS_IDX As Integer = 6
    Public Const GRID_COL_CLAIM_ISSUE_ADDED_DATE_IDX As Integer = 7
    Public Const GRID_COL_CLAIM_ISSUE_DESCRIPTION_IDX As Integer = 8

    Public Const GRID_COL_CLAIM_ID_IDX As Integer = 9
    Public Const GRID_COL_CLAIM_AUTH_TYPE_ID_IDX As Integer = 10
    Public Const GRID_COL_CLAIM_STATUS_CODE_IDX As Integer = 11



    Public Const SESSION_KEY_BACKUP_STATE As String = "SESSION_KEY_BACKUP_STATE_CLAIM_BY_ISSUE_LIST_FORM"

#End Region


#Region "Page State"

    Private IsReturningFromChild As Boolean = False

    Class SearchCriterias
        Public IssueTypeId As Guid = Guid.Empty
        Public IssueTypeCode As String = String.Empty
        Public IssueId As Guid?
        Public DealerId As Guid?
        Public IssueStatusXcd As String
        Public ClaimStatusCode As String
        Public IssueAddedFromDate As Date?
        Public IssueAddedToDate As Date?
    End Class
    Class MyState
        Public PageIndex As Integer = 0
        Public SortExpression As String = "claim_number ASC"
        Public SelectedClaimId As Guid = Guid.Empty

        Public IsGridVisible As Boolean = False
        Public PageSize As Integer = DEFAULT_NEW_UI_PAGE_SIZE
        Public SearchDv As Claim.ClaimIssueSearchDV = Nothing
        REM ------------------------------------------------------------
        Public Criterias As SearchCriterias = New SearchCriterias

        Public Sub New()

        End Sub


    End Class

    Public Sub New()
        MyBase.New(New MyState)
    End Sub
    Protected Shadows ReadOnly Property State() As MyState
        Get
            Dim retState As MyState
            'Return CType(MyBase.State, MyState)
            If Me.NavController Is Nothing Then
                'Restart flow
                Me.StartNavControl()
                Me.NavController.State = CType(Session(SESSION_KEY_BACKUP_STATE), MyState)
            ElseIf Me.NavController.State Is Nothing Then
                Me.NavController.State = New MyState
            ElseIf (Me.GetType.BaseType.FullName <>
                    Me.NavController.State.GetType.ReflectedType.FullName) Then
                'Restart flow
                Me.StartNavControl()
                Me.NavController.State = CType(Session(SESSION_KEY_BACKUP_STATE), MyState)
            Else
                If Me.NavController.IsFlowEnded Then
                    'restart flow
                    Dim s As MyState = CType(Me.NavController.State, MyState)
                    Me.StartNavControl()
                    Me.NavController.State = s
                End If
            End If
            If Me.NavController.State Is Nothing Then
                Me.NavController.State = New MyState
            End If
            retState = CType(Me.NavController.State, MyState)
            Session(SESSION_KEY_BACKUP_STATE) = retState
            Return retState
        End Get
    End Property
#End Region

#Region "Navigation Handling"
    Public Sub Process(callingPage As Page, navCtrl As INavigationController) Implements IStateController.Process
        Try
            If Not Me.IsPostBack AndAlso navCtrl.CurrentFlow.Name = Me.FLOW_NAME AndAlso
                        Not navCtrl.PrevNavState Is Nothing Then
                Me.IsReturningFromChild = True
                If navCtrl.IsFlowEnded Then
                    Me.State.SearchDv = Nothing 'This will force a reload
                    'restart the flow
                    Dim savedState As MyState = CType(navCtrl.State, MyState)
                    Me.StartNavControl()
                    Me.NavController.State = savedState
                End If
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub
#End Region

#Region "Page_Events"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Put user code to initialize the page here
        'Page.RegisterHiddenField("__EVENTTARGET", Me.btnSearch.ClientID)
        ClientScript.RegisterHiddenField("__EVENTTARGET", Me.btnSearch.ClientID)

        'Dim langId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
        Me.MasterPage.MessageController.Clear_Hide()
        Try
            If Not Me.IsPostBack Then
                'Trace(Me, "Cert = " & Me.State.certificate & "@ Claim = " & Me.State.claimNumber)
                Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage(PAGE_TITLE_NAME)

                Me.AddCalendar_New(Me.ImageIssueAddedFromDate, Me.txtIssueAddedFromDate)
                Me.AddCalendar_New(Me.ImageIssueAddedToDate, Me.txtIssueAddedToDate)

                UpdateBreadCrum()
                SetDefaultButtonToControls()
                TranslateGridHeader(Me.Grid)

                PopulateDropdowns()

                If Authentication.CurrentUser.IsDealer Then
                    SetSelectedItem(cboSearchDealer, Authentication.CurrentUser.ScDealerId)
                    ControlMgr.SetEnableControl(Me, cboSearchDealer, False)
                End If

                InitializePageSize()

                If Me.IsReturningFromChild Then
                    ' It is returning from detail
                    PopulateSearchFieldsFromState()
                    Me.PopulateGrid()
                Else
                    Me.ClearState()
                    Me.ClearSearch()
                    ControlMgr.SetVisibleControl(Me, Grid, False)
                    ControlMgr.SetVisibleControl(Me, trPageSize, False)
                End If

                Me.SetGridItemStyleColor(Me.Grid)
                SetFocus(cboSearchIssueType)
            End If
            Me.DisplayNewProgressBarOnClick(Me.btnSearch, "Loading_Claims")
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
        Me.ShowMissingTranslations(Me.MasterPage.MessageController)
    End Sub

    Private Sub Page_PageReturn(ByVal ReturnFromUrl As String, ByVal ReturnPar As Object) Handles MyBase.PageReturn
        Try
            Me.MenuEnabled = True
            Me.IsReturningFromChild = True

            Me.State.SearchDv = Nothing
            Me.State.SelectedClaimId = Guid.Empty

            'If Not ReturnedValues Is Nothing then
            '    if TypeOf ReturnedValues Is ClaimForm.ReturnType Then
            '        Dim retObj As ClaimForm.ReturnType = CType(Me.ReturnedValues, ClaimForm.ReturnType)

            '        Select Case retObj.LastOperation
            '            Case ElitaPlusPage.DetailPageCommand.Back
            '                If Not retObj Is Nothing Then
            '                    If Not retObj.EditingBo.IsNew Then
            '                        Me.State.SelectedClaimId = retObj.EditingBo.Id
            '                    End If
            '                    Me.State.IsGridVisible = True
            '                End If
            '        End Select
            '    elseif TypeOf ReturnedValues Is ClaimWizardForm.ReturnType Then
            '        Dim retObj As ClaimWizardForm.ReturnType = CType(Me.ReturnedValues, ClaimWizardForm.ReturnType)

            '        Select Case retObj.LastOperation
            '            Case ElitaPlusPage.DetailPageCommand.Back
            '                If Not retObj Is Nothing Then
            '                    If Not retObj.EditingBo.IsNew Then
            '                        Me.State.SelectedClaimId = retObj.EditingBo.Id
            '                    End If
            '                    Me.State.IsGridVisible = True
            '                End If
            '        End Select
            '    End If                
            'End If

        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try

    End Sub

    Private Sub ClaimByIssueSearch_PreRender(sender As Object, e As EventArgs) Handles Me.PreRender
        'if string.IsNullOrEmpty(cboSearchIssueType.SelectedValue) = false andalso cboClaimIssue.Items.Count = 0 then 'andalso cboSearchIssueType.SelectedValue <> "00000000-0000-0000-0000-000000000000" 
        If State.Criterias.IssueTypeId.Equals(Guid.Empty) = False Then
            Dim dv As DataView = EntityIssue.GetClaimIssuesByIssueType(State.Criterias.IssueTypeId)
            Me.BindListControlToDataView(cboClaimIssue, dv, "description", "issue_id", True)
            If cboClaimIssue.Items(0).Text = String.Empty Then
                cboClaimIssue.Items(0).Text = "---------------"
            End If
            If State.Criterias.IssueId.HasValue Then
                cboClaimIssue.SelectedValue = State.Criterias.IssueId.Value.ToString()
            End If
            If Me.State.Criterias.IssueAddedFromDate.HasValue Then
                txtIssueAddedFromDate.Text = GetDateFormattedString(Me.State.Criterias.IssueAddedFromDate)
            End If
            If Me.State.Criterias.IssueAddedToDate.HasValue Then
                txtIssueAddedToDate.Text = GetDateFormattedString(Me.State.Criterias.IssueAddedToDate)
            End If
        Else
            If cboClaimIssue.Items.Count > 0 Then
                cboClaimIssue.Items.Clear()
            End If
        End If
    End Sub
#End Region

#Region "Methods"
    Private Sub UpdateBreadCrum()
        If (Not Me.State Is Nothing) Then
            If (Not Me.State Is Nothing) Then
                Me.MasterPage.BreadCrum = Me.MasterPage.PageTab & ElitaBase.Sperator &
                    TranslationBase.TranslateLabelOrMessage(PAGE_TITLE_NAME)
                Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage(PAGE_TITLE_NAME)
            End If
        End If
    End Sub

    Private Sub SetDefaultButtonToControls()
        Try
            SetDefaultButton(txtIssueAddedFromDate, btnSearch)
            SetDefaultButton(txtIssueAddedToDate, btnSearch)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub
#End Region

#Region " Button Clicks "

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try

            ControlMgr.SetVisibleControl(Me, Grid, False)
            ControlMgr.SetVisibleControl(Me, trPageSize, False)

            If PopulateStateFromSearchFields() Then
                Me.State.PageIndex = 0
                Me.State.SelectedClaimId = Guid.Empty
                Me.State.IsGridVisible = True
                Me.State.SearchDv = Nothing
                Me.State.SortExpression = String.Empty
                Me.PopulateGrid()
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnClearSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearSearch.Click
        Try
            Me.ClearState()
            Me.ClearSearch()
            ControlMgr.SetVisibleControl(Me, Grid, False)
            ControlMgr.SetVisibleControl(Me, trPageSize, False)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

#End Region

#Region "Controlling Logic"

    Public Sub PopulateGrid()

        Try

            If Me.State.SearchDv Is Nothing Then
                Dim strClaimStatusCode As String = State.Criterias.ClaimStatusCode
                If strClaimStatusCode = "00000000-0000-0000-0000-000000000000" Then
                    strClaimStatusCode = String.Empty
                End If

                Dim strIssueStatusXcd As String = State.Criterias.IssueStatusXcd
                If strIssueStatusXcd = "00000000-0000-0000-0000-000000000000" Then
                    strIssueStatusXcd = String.Empty
                End If

                Me.State.SearchDv = Claim.GetClaimsByIssue(
                    State.Criterias.IssueTypeCode,
                    State.Criterias.IssueTypeId,
                    State.Criterias.IssueId,
                    strIssueStatusXcd,
                    strClaimStatusCode,
                    State.Criterias.DealerId,
                    State.Criterias.IssueAddedFromDate,
                    State.Criterias.IssueAddedToDate)
            End If

            Grid.PageSize = State.PageSize

            SetPageAndSelectedIndexFromGuid(Me.State.SearchDv, Me.State.SelectedClaimId, Me.Grid, Me.State.PageIndex)
            Me.Grid.DataSource = Me.State.SearchDv
            Me.State.PageIndex = Me.Grid.PageIndex

            If String.IsNullOrEmpty(State.SortExpression) = True Then
                State.SortExpression = "claim_number ASC"
            End If

            Me.State.SearchDv.Sort = Me.State.SortExpression

            HighLightSortColumn(Me.Grid, Me.State.SortExpression, Me.IsNewUI)

            Me.Grid.DataBind()

            ControlMgr.SetVisibleControl(Me, Grid, Me.State.IsGridVisible)

            ControlMgr.SetVisibleControl(Me, trPageSize, Me.Grid.Visible)

            Session("recCount") = Me.State.SearchDv.Count

            If Me.Grid.Visible Then
                Me.lblRecordCount.Text = Me.State.SearchDv.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_CLAIMS_FOUND)
            End If


        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try

    End Sub

    Public Function PopulateStateFromSearchFields() As Boolean
        Dim dt As DateTime

        Try
            Me.State.Criterias = New SearchCriterias()
            Me.State.Criterias.IssueTypeId = GetSelectedItem(cboSearchIssueType)

            State.Criterias.IssueTypeCode = LookupListNew.GetIssueTypeCodeFromId(LookupListNew.LK_ISSUE_TYPE_CODE_LIST, Me.State.Criterias.IssueTypeId)

            Dim value As String = Request.Form(cboClaimIssue.UniqueID)
            If String.IsNullOrEmpty(value) = False Then
                State.Criterias.IssueId = New Guid(value)
            End If

            If String.IsNullOrEmpty(txtIssueAddedFromDate.Text) = False Then
                Try
                    DateTime.TryParseExact(txtIssueAddedFromDate.Text.Trim(), DATE_FORMAT, System.Threading.Thread.CurrentThread.CurrentCulture, DateTimeStyles.None, dt)
                    If (dt <> DateTime.MinValue) Then
                        Me.State.Criterias.IssueAddedFromDate = GetDateFormattedStringNullable(dt)
                    End If
                Catch ex As Exception
                    ElitaPlusPage.SetLabelError(Me.Label2)
                    Throw New GUIException(Message.MSG_INVALID_DATE, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_DATE_ERR)
                End Try
            End If

            If String.IsNullOrEmpty(txtIssueAddedToDate.Text) = False Then
                Try
                    DateTime.TryParseExact(txtIssueAddedToDate.Text.Trim(), DATE_FORMAT, System.Threading.Thread.CurrentThread.CurrentCulture, DateTimeStyles.None, dt)
                    If (dt <> DateTime.MinValue) Then
                        Me.State.Criterias.IssueAddedToDate = GetDateFormattedStringNullable(dt)
                    End If
                Catch ex As Exception
                    ElitaPlusPage.SetLabelError(Me.Label4)
                    Throw New GUIException(Message.MSG_INVALID_DATE, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_DATE_ERR)
                End Try
            End If

            If String.IsNullOrEmpty(cboSearchDealer.SelectedValue) = False AndAlso cboSearchDealer.SelectedValue <> "00000000-0000-0000-0000-000000000000" Then
                State.Criterias.DealerId = GetSelectedItem(cboSearchDealer)
            End If

            State.Criterias.ClaimStatusCode = cboSearchClaimStatus.SelectedValue
            State.Criterias.IssueStatusXcd = cboSearchClaimIssueStatus.SelectedValue

            If State.Criterias.IssueTypeId.Equals(Guid.Empty) = True Then
                MasterPage.MessageController.AddErrorAndShow(TranslationBase.TranslateLabelOrMessage("ISSUE_TYPE_REQUIRE"))
                Return False
            Else
                Return True
            End If


        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try

    End Function
    Public Sub ClearSearch()
        Try

            If Not ElitaPlusIdentity.Current.ActiveUser.IsDealer Then
                Me.cboSearchDealer.SelectedIndex = 0
            End If
            Me.cboSearchClaimStatus.SelectedIndex = 0
            Me.cboSearchClaimIssueStatus.SelectedIndex = 0
            Me.cboSearchIssueType.SelectedIndex = 0
            cboClaimIssue.SelectedIndex = 0
            txtIssueAddedToDate.Text = String.Empty
            txtIssueAddedFromDate.Text = String.Empty

        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Public Sub ClearState()

        Try
            Me.State.Criterias = New SearchCriterias
            State.SearchDv = Nothing
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Sub InitializePageSize()
        Try
            cboPageSize.SelectedValue = CType(Me.State.PageSize, String)
            Grid.PageSize = Me.State.PageSize
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try

    End Sub
#End Region

#Region "Populate Methods"

    Sub PopulateDropdowns()
        'To be updated with new list code
        BindListControlToDataView(Me.cboSearchIssueType, LookupListNew.GetIssueTypeLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId, True), "DESCRIPTION", "ID", True, True)
        PopulateDealerDropDown()

        cboSearchClaimStatus.Populate(CommonConfigManager.Current.ListManager.GetList("CLSTAT", Thread.CurrentPrincipal.GetLanguageCode()),
                                      New PopulateOptions() With {.AddBlankItem = True, .ValueFunc = AddressOf PopulateOptions.GetCode})

        cboSearchClaimIssueStatus.Populate(CommonConfigManager.Current.ListManager.GetList("CLMISSUESTATUS", Thread.CurrentPrincipal.GetLanguageCode()),
                                      New PopulateOptions() With {.AddBlankItem = True, .ValueFunc = AddressOf PopulateOptions.GetExtendedCode})

    End Sub
    Sub PopulateDealerDropDown()
        Try
            Dim DealerList As ListItem()
            If Authentication.CurrentUser.IsDealerGroup Then
                DealerList = CaseBase.GetDealerListByCompanyForExternalUser()
            Else
                DealerList = GetDealerListByCompanyForUser()
            End If

            cboSearchDealer.Populate(DealerList, New PopulateOptions() With
                                                 {
                                                   .AddBlankItem = True,
                                                   .ValueFunc = AddressOf .GetListItemId,
                                                   .TextFunc = Function(x)
                                                                   Return x.Translation + " (" + x.Code + ")"
                                                               End Function,
                                                   .SortFunc = AddressOf .GetDescription
                                                  })

            'If Not Me.State.Criterias.SelectedDealerId.IsEmpty Then
            '    SetSelectedItem(Me.cboSearchDealer, Me.State.Criterias.SelectedDealerId)
            'End If


        Catch ex As Exception
            Me.HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Function GetDealerListByCompanyForUser() As Assurant.Elita.CommonConfiguration.DataElements.ListItem()
        Dim Index As Integer
        Dim oListContext As New Assurant.Elita.CommonConfiguration.ListContext

        Dim UserCompanies As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Companies

        Dim oDealerList As New Collections.Generic.List(Of Assurant.Elita.CommonConfiguration.DataElements.ListItem)

        For Index = 0 To UserCompanies.Count - 1
            'UserCompanyList &= ",'" & GuidControl.GuidToHexString(UserCompanyies(Index))
            oListContext.CompanyId = UserCompanies(Index)
            Dim oDealerListForCompany As Assurant.Elita.CommonConfiguration.DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="DealerListByCompany", context:=oListContext)
            If oDealerListForCompany.Count > 0 Then
                If Not oDealerList Is Nothing Then
                    oDealerList.AddRange(oDealerListForCompany)
                Else
                    oDealerList = oDealerListForCompany.Clone()
                End If

            End If
        Next

        Return oDealerList.ToArray()

    End Function


    Sub PopulateSearchFieldsFromState()
        Try

            With State.Criterias
                If .DealerId.HasValue Then
                    SetSelectedItem(Me.cboSearchDealer, .DealerId)
                End If
                If .IssueAddedToDate.HasValue Then
                    txtIssueAddedToDate.Text = GetDateFormattedString(.IssueAddedToDate)
                End If
                If .IssueAddedFromDate.HasValue Then
                    txtIssueAddedFromDate.Text = GetDateFormattedString(.IssueAddedFromDate)
                End If

                SetSelectedItem(Me.cboSearchClaimStatus, .ClaimStatusCode)
                SetSelectedItem(Me.cboSearchClaimIssueStatus, .IssueStatusXcd)
                SetSelectedItem(Me.cboSearchIssueType, .IssueTypeId)
            End With

        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

#End Region

#Region "State Controller"
    Public Const FLOW_NAME As String = "AUTHORIZE_PENDING_CLAIM_CLONE"
    Sub StartNavControl()
        Dim nav As New ElitaPlusNavigation
        Me.NavController = New NavControllerBase(nav.Flow(FLOW_NAME))
        Me.NavController.State = New MyState
    End Sub

    Function IsFlowStarted() As Boolean
        Return Not Me.NavController Is Nothing AndAlso Me.NavController.CurrentFlow.Name = FLOW_NAME
    End Function
#End Region


#Region " Datagrid Related "

    Private Sub Grid_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles Grid.Sorting
        Try
            If Me.State.SortExpression.StartsWith(e.SortExpression) Then
                If Me.State.SortExpression.EndsWith(" DESC") Then
                    Me.State.SortExpression = e.SortExpression & " ASC"
                Else
                    Me.State.SortExpression = e.SortExpression & " DESC"
                End If
            Else
                Me.State.SortExpression = e.SortExpression & " ASC"
            End If
            Me.State.PageIndex = 0
            Me.PopulateGrid()
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

    'The Binding Logic is here
    Private Sub Grid_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowDataBound
        'Dim itemType As ListItemType = CType(e.Row.ItemType, ListItemType)
        Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
        Dim btnEditClaimItem As LinkButton

        Try
            If e.Row.RowType = DataControlRowType.DataRow OrElse (e.Row.RowType = DataControlRowType.Separator) Then
                If (Not e.Row.Cells(GRID_COL_CLAIM_NUMBER_IDX).FindControl(GRID_COL_CLAIM_NUMBER_CTRL) Is Nothing) Then
                    btnEditClaimItem = CType(e.Row.Cells(GRID_COL_CLAIM_NUMBER_IDX).FindControl(GRID_COL_CLAIM_NUMBER_CTRL), LinkButton)
                    btnEditClaimItem.CommandArgument = e.Row.RowIndex.ToString
                    btnEditClaimItem.CommandName = SELECT_COMMAND_NAME
                    btnEditClaimItem.Text = dvRow(Claim.ClaimIssueSearchDV.COL_CLAIM_NUMBER).ToString
                End If

                Me.PopulateControlFromBOProperty(e.Row.Cells(GRID_COL_STATUS_IDX), dvRow(Claim.ClaimIssueSearchDV.COL_STATUS_CODE))
                e.Row.Cells(GRID_COL_STATUS_IDX).Text = LookupListNew.GetDescriptionFromCode("CLSTAT", dvRow(Claim.ClaimIssueSearchDV.COL_STATUS_CODE).ToString, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
                If (dvRow(Claim.ClaimIssueSearchDV.COL_STATUS_CODE).ToString = Codes.CLAIM_STATUS__ACTIVE) Then
                    e.Row.Cells(GRID_COL_STATUS_IDX).CssClass = "StatActive"
                Else
                    e.Row.Cells(GRID_COL_STATUS_IDX).CssClass = "StatInactive"
                End If
                'Me.PopulateControlFromBOProperty(e.Row.Cells(GRID_COL_CUSTOMER_NAME_IDX), dvRow(Claim.ClaimIssueSearchDV.COL_CUSTOMER_NAME))
                'Me.PopulateControlFromBOProperty(e.Row.Cells(GRID_COL_SERVICE_CENTER_IDX), dvRow(Claim.ClaimIssueSearchDV.COL_SERVICE_CENTER_NAME))
                'Me.PopulateControlFromBOProperty(e.Row.Cells(GRID_COL_SERVICE_CENTER_REF_NUMBER_IDX), dvRow(Claim.ClaimIssueSearchDV.COL_SERVICE_CENTER_REF_NUMBER))
                'Me.PopulateControlFromBOProperty(e.Row.Cells(GRID_COL_DEALER_IDX), dvRow(Claim.ClaimIssueSearchDV.COL_DEALER_CODE))
                'Me.PopulateControlFromBOProperty(e.Row.Cells(GRID_COL_CERTIFICATE_IDX), dvRow(Claim.ClaimIssueSearchDV.COL_CERTIFICATE_NUMBER))
                Me.PopulateControlFromBOProperty(e.Row.Cells(GRID_COL_CLAIM_ISSUE_ADDED_DATE_IDX), GetLongDateFormattedString(CType(dvRow(Claim.ClaimIssueSearchDV.COL_ISSUE_ADDED_DATE), Date)))
                'Me.PopulateControlFromBOProperty(e.Row.Cells(GRID_COL_PRODUCT_CODE_IDX), dvRow(Claim.ClaimIssueSearchDV.COL_PRODUCT_CODE))
                'Me.PopulateControlFromBOProperty(e.Row.Cells(GRID_COL_AUTHORIZATION_NUMBER_IDX), dvRow(Claim.ClaimIssueSearchDV.COL_AUTHORIZATION_NUMBER))
                'Me.PopulateControlFromBOProperty(e.Row.Cells(GRID_COL_TRACKING_NUMBER_IDX), dvRow(Claim.ClaimIssueSearchDV.COL_TRACKING_NUMBER))
                'Me.PopulateControlFromBOProperty(e.Row.Cells(GRID_COL_AUTHORIZED_AMOUNT_IDX), dvRow(Claim.ClaimIssueSearchDV.COL_AUTHORIZED_AMOUNT))

                'Me.PopulateControlFromBOProperty(e.Row.Cells(GRID_COL_CLAIM_ISSUE_STATUS_IDX), dvRow(Claim.ClaimIssueSearchDV.COL_CLAIM_ISSUE_STATUS))
                'Me.PopulateControlFromBOProperty(e.Row.Cells(GRID_COL_CLAIM_ISSUE_TYPE_IDX), dvRow(Claim.ClaimIssueSearchDV.COL_CLAIM_ISSUE_TYPE))
                'Me.PopulateControlFromBOProperty(e.Row.Cells(GRID_COL_CLAIM_ISSUE_DESCRIPTION_IDX), dvRow(Claim.ClaimIssueSearchDV.COL_CLAIM_ISSUE_DESCRIPTION))

                Me.PopulateControlFromBOProperty(e.Row.Cells(GRID_COL_CLAIM_ID_IDX), dvRow(Claim.ClaimIssueSearchDV.COL_CLAIM_ID))
                Me.PopulateControlFromBOProperty(e.Row.Cells(GRID_COL_CLAIM_STATUS_CODE_IDX), dvRow(Claim.ClaimIssueSearchDV.COL_STATUS_CODE))
                Me.PopulateControlFromBOProperty(e.Row.Cells(GRID_COL_CLAIM_AUTH_TYPE_ID_IDX), dvRow(Claim.ClaimIssueSearchDV.COL_CLAIM_AUTH_TYPE_ID))

            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub cboPageSize_SelectedIndexChanged(ByVal source As Object, ByVal e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
        Try
            State.PageSize = CType(cboPageSize.SelectedValue, Integer)
            Me.State.PageIndex = NewCurrentPageIndex(Grid, State.SearchDv.Count, State.PageSize)
            Grid.PageIndex = NewCurrentPageIndex(Grid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Public Sub Grid_RowCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles Grid.RowCommand
        Dim rowIndex As Integer = 0
        Dim claimid As String = String.Empty
        Try
            If (Not e.CommandArgument.ToString().Equals(String.Empty)) And (e.CommandName = SELECT_COMMAND_NAME) Then
                rowIndex = CInt(e.CommandArgument)

                If Me.State Is Nothing Then
                    Me.Trace(Me, "Restoring State")
                    Me.RestoreState(New MyState)
                End If

                claimid = Grid.Rows(rowIndex).Cells(GRID_COL_CLAIM_ID_IDX).Text
                Me.State.SelectedClaimId = New Guid(claimid)

                If Grid.Rows(rowIndex).Cells(GRID_COL_CLAIM_STATUS_CODE_IDX).Text = "P" Then
                    Dim claim_auth_type_code As String
                    Dim ClaimAutthTypeid As Guid = New Guid(Grid.Rows(rowIndex).Cells(GRID_COL_CLAIM_AUTH_TYPE_ID_IDX).Text) ' New Guid(BusinessObjectBase.FindRow(Me.State.selectedClaimId, Claim.ClaimSearchDV.COL_CLAIM_ID, Me.State.searchDV.Table)(Claim.ClaimSearchDV.COL_NAME_CLAIM_AUTH_TYPE_ID).ToString())
                    claim_auth_type_code = LookupListNew.GetCodeFromId(LookupListNew.LK_CLAIM_AUTHORIZATION_TYPE, ClaimAutthTypeid)
                    Dim selectedClaimId As Guid = Me.State.SelectedClaimId
                    If (claim_auth_type_code = "M") Then
                        Me.NavController = Nothing
                        Me.callPage(ClaimWizardForm.URL, New ClaimWizardForm.Parameters(ClaimWizardForm.ClaimWizardSteps.Step3, Nothing, selectedClaimId, Nothing))
                    Else
                        Dim claimBo As Claim = ClaimFacade.Instance.GetClaim(Of Claim)(Me.State.SelectedClaimId)
                        Me.NavController.FlowSession(FlowSessionKeys.SESSION_CLAIM) = claimBo
                        Me.NavController.Navigate(Me, FlowEvents.EVENT_CLAIM_SELECTED)

                    End If
                Else
                    Me.NavController = Nothing
                    Me.callPage(ClaimForm.URL, Me.State.SelectedClaimId)
                End If


            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try

    End Sub

    Public Sub RowCreated(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowCreated
        Try
            BaseItemCreated(sender, e)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Grid_PageIndexChanged(ByVal source As Object, ByVal e As System.EventArgs) Handles Grid.PageIndexChanged
        Try
            Me.State.PageIndex = Grid.PageIndex
            Me.State.SelectedClaimId = Guid.Empty
            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub
#End Region

#Region "Ajax Funtions"
    <System.Web.Services.WebMethod(), System.Web.Script.Services.ScriptMethod()>
    Public Shared Function GetClaimIssueByType(ByVal issueTypestr As String) As System.Collections.Generic.List(Of WebControls.ListItem)
        Dim listitems As System.Collections.Generic.List(Of WebControls.ListItem) = New System.Collections.Generic.List(Of WebControls.ListItem)
        Dim issueTypeId As Guid
        Try
            issueTypeId = New Guid(issueTypestr)
        Catch ex As Exception
            Return listitems
        End Try

        Dim dv As DataView = EntityIssue.GetClaimIssuesByIssueType(issueTypeId)

        If Not dv Is Nothing AndAlso dv.Table.Rows.Count > 0 Then
            dv.Table.Locale = CultureInfo.CurrentCulture
            dv.Sort = "description ASC"

            Dim tempGuid As Guid

            For i As Integer = 0 To dv.Count - 1
                tempGuid = New Guid(CType(dv(i)("issue_id"), Byte()))
                listitems.Add(New WebControls.ListItem(text:=dv(i)("description").ToString(), value:=tempGuid.ToString()))
            Next
        End If
        Return listitems
    End Function


#End Region
End Class