Imports System.Threading
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms
Namespace Tables

    Partial Public Class NotificationListForm
        Inherits ElitaPlusSearchPage

        Public SearchHidden As Boolean = False


#Region "Constants"

        Public Const URL As String = "NotificationListForm.aspx"

        Public Const GRID_CTRL_NOTIFICATION_LIST_ID As String = "lblListID"

        Private Const GRID_COL_NOTIFICATION_NAME_IDX As Integer = 0
        Private Const GRID_COL_NOTIFICATION_TYPE_IDX As Integer = 1
        Private Const GRID_COL_AUDIANCE_TYPE_IDX As Integer = 2
        Private Const GRID_COL_BEGIN_DATE_IDX As Integer = 3
        Private Const GRID_COL_END_DATE_IDX As Integer = 4
        Private Const GRID_COL_OUTAGE_BEGIN_DATE_IDX As Integer = 5
        Private Const GRID_COL_OUTAGE_END_DATE_IDX As Integer = 6
        Private Const GRID_COL_SERIAL_NO_IDX As Integer = 7
        Private Const GRID_COL_ENABLED_IDX As Integer = 8

        Private Const GRID_COL_NOTIFICATION_LIST_ID_IDX As Integer = 9

        Public Const BTN_CONTROL_EDIT_DETAIL_LIST As String = "SelectAction"

        Private Const COL_ID_NAME As String = "id"
        Private Const COL_CODE_NAME As String = "code"
        Private Const COL_DESCRIPTION_NAME As String = "description"
        Private Const SEARCH_EXCEPTION As String = "CERTIFICATELIST_FORM001"

#End Region

#Region "Page State"
        Private IsReturningFromChild As Boolean = False

        Class MyState

            Public SelectedNotificationListId As Guid = Guid.Empty
            Public SelectedNotificationTypeId As Guid = Guid.Empty
            Public SelectedAudianceTypeId As Guid = Guid.Empty
            Public NotificationName As String
            Public NotificationDetail As String
            Public IncludeDisabled As Boolean = False

            Public selectedSortById As Guid = Guid.Empty
            Public selectedSortOrderId As Guid = Guid.Empty
            Public selectedClaimTypeId As Guid = Guid.Empty

            Public BeginDate As String
            Public EndDate As String

            Public BeginDateOutage As String
            Public EndDateOutage As String

            Public SortExpression As String = "AudianceType"

            Public SearchDV As Notification.NotificationSearchDV
            Public HasDataChanged As Boolean
            Public IsGridVisible As Boolean
            Public SelectedPageSize As Integer
            Public SearchClick As Boolean = False
            Public AllPagesVisited As Boolean = False

            Public PageIndex As Integer = 0
            Public PageSize As Integer = 30
            Public PageIndicesVisited As ArrayList

            Public IsGridInEditMode As Boolean = False
            Public SelectedGridValueToEdit As Guid
            Public ChildActionInProgress As DetailPageCommand = DetailPageCommand.Nothing_
            Public NotificationSelectedBOId As Guid = Guid.Empty

            Public MyBO As Notification
            Public UserNotificationBO As UserNotification
            Public bnoRow As Boolean = False

            Public ActionInProgress As DetailPageCommand = DetailPageCommand.Nothing_
            Public blnModalOpen As Boolean = False
            Public PageCalledFrom As String = "MENU"

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

        Private Sub Page_PageCall(CallFromUrl As String, CallingPar As Object) Handles MyBase.PageCall
            Try
                If CallingParameters IsNot Nothing Then
                    'Get the page that called from the parent
                    State.PageCalledFrom = CType(CallingParameters, String)
                Else
                    State.PageCalledFrom = "MENU"
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub

        Private Sub Page_PageReturn(ReturnFromUrl As String, ReturnPar As Object) Handles MyBase.PageReturn
            Try
                MenuEnabled = True
                IsReturningFromChild = True
                Dim retObj As PriceListDetailForm.ReturnType = CType(ReturnPar, PriceListDetailForm.ReturnType)
                State.HasDataChanged = retObj.HasDataChanged
                Select Case retObj.LastOperation
                    Case ElitaPlusPage.DetailPageCommand.Back
                        If retObj IsNot Nothing Then
                            If Not retObj.EditingBo.IsNew Then
                                State.SelectedNotificationListId = retObj.EditingBo.Id
                            End If
                            State.IsGridVisible = True
                        End If
                    Case ElitaPlusPage.DetailPageCommand.Delete
                        DisplayMessage(Message.DELETE_RECORD_CONFIRMATION, "", MSG_BTN_OK, MSG_TYPE_INFO)
                    Case ElitaPlusPage.DetailPageCommand.Expire
                        DisplayMessage(Message.EXPIRE_RECORD_CONFIRMATION, "", MSG_BTN_OK, MSG_TYPE_INFO)
                End Select

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub SaveGuiState()
            With State
                .BeginDate = txtBeginDate.Text
                .EndDate = txtEndDate.Text

                .BeginDateOutage = txtBeginDateOutage.Text
                .EndDateOutage = txtEndDateOutage.Text

                .SelectedAudianceTypeId = New Guid(ddlAudianceType.SelectedValue.ToString())
                .SelectedNotificationTypeId = New Guid(ddlNotificationType.SelectedValue.ToString())

                .IncludeDisabled = cbIncludeDisabled.Checked
                .NotificationName = txtNotificationName.Text
                .NotificationDetail = txtNotificationDetail.Text

                .selectedSortById = New Guid(cboSortBy.SelectedValue.ToString())
                .selectedSortOrderId = New Guid(cboSortOrder.SelectedValue.ToString())

            End With
        End Sub

        Private Sub RestoreGuiState()
            With State
                PopulateControlFromBOProperty(txtBeginDate, .BeginDate)
                PopulateControlFromBOProperty(txtEndDate, .EndDate)
                PopulateControlFromBOProperty(txtBeginDateOutage, .BeginDateOutage)
                PopulateControlFromBOProperty(txtEndDateOutage, .EndDateOutage)
                PopulateControlFromBOProperty(ddlAudianceType, .SelectedAudianceTypeId)
                PopulateControlFromBOProperty(ddlNotificationType, .SelectedNotificationTypeId)
                PopulateControlFromBOProperty(cbIncludeDisabled, .IncludeDisabled)
                PopulateControlFromBOProperty(txtNotificationName, .NotificationName)
                PopulateControlFromBOProperty(txtNotificationDetail, .NotificationDetail)

            End With
        End Sub

#End Region

#Region "Page_Events"

        Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
            Try
                MasterPage.MessageController.Clear()
                Form.DefaultButton = btnSearch.UniqueID
                If Request.QueryString.Get("CALLER") IsNot Nothing Then State.PageCalledFrom = Request.QueryString.Get("CALLER")
                Session("PageCalledFrom") = State.PageCalledFrom

                If Not IsPostBack Then
                    cboPageSize.SelectedValue = CType(State.PageSize, String)
                    Grid.PageSize = State.PageSize

                    MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("NOTIFICATION_LIST")
                    UpdateBreadCrum()
                    TranslateGridHeader(Grid)

                    If State.PageCalledFrom = "MENU" OrElse State.PageCalledFrom = "NON_ADMIN_USER" Then
                        AddCalendarwithTime(BtnBeginDate, txtBeginDate)
                        AddCalendarwithTime(BtnEndDate, txtEndDate)
                        AddCalendarwithTime(btnBeginDateOutage, txtBeginDateOutage)
                        AddCalendarwithTime(btnEndDateOutage, txtEndDateOutage)
                        PopulateDropDowns()
                        ControlMgr.SetVisibleControl(Me, trPageSize, False)
                        ControlMgr.SetVisibleControl(Me, BtnAcknowledge, False)
                        If IsReturningFromChild Then RestoreGuiState()

                        If State.IsGridVisible Then
                            PopulateGrid()
                        End If
                        'Set page size
                        cboPageSize.SelectedValue = State.PageSize.ToString()
                        SetFocus(ddlNotificationType)
                        If State.PageCalledFrom = "MENU" Then
                            addBtnNewListItem.Visible = True
                        Else
                            addBtnNewListItem.Visible = False
                            HandelPageForNon_AdminUserNotifications()
                        End If

                    Else
                        If State.PageCalledFrom = "MAINPAGE" Then HandelPageForUserNotifications()
                        If State.PageCalledFrom = "NON_ADMIN_USER" Then HandelPageForNon_AdminUserNotifications()
                    End If
                Else

                    If State.PageCalledFrom = "MAINPAGE" Then HandelPageForUserNotifications()

                End If

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
            ShowMissingTranslations(MasterPage.MessageController)

        End Sub

        Protected Sub CheckIfComingFromDeleteConfirm()
            Dim confResponse As String = HiddenSaveChangesPromptResponse.Value
            If confResponse IsNot Nothing AndAlso confResponse = MSG_VALUE_YES Then
                If Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Delete Then
                    If Not State.NotificationSelectedBOId.Equals(Guid.Empty) Then
                        '''BeginNotificationDetailChildEdit()
                    End If
                    Try

                        State.MyBO.Delete()
                        State.MyBO.Save()
                        State.MyBO.EndEdit()
                        State.NotificationSelectedBOId = Guid.Empty

                    Catch ex As Exception
                        State.MyBO.RejectChanges()
                        MasterPage.MessageController.AddError(ElitaPlus.ElitaPlusWebApp.Message.ERR_DELETING_DATA)
                        Throw ex
                    End Try
                    PopulateGrid()
                    'Clean after consuming the action
                    State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
                    HiddenSaveChangesPromptResponse.Value = ""
                End If
            ElseIf confResponse IsNot Nothing AndAlso confResponse = MSG_VALUE_NO Then
                'Clean after consuming the action
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
                HiddenSaveChangesPromptResponse.Value = ""
            End If
        End Sub

        Private Sub HandelPageForUserNotifications()
            MenuEnabled = False
            searchTable.Visible = False
            gridHeader.Visible = False
            addBtnNewListItem.Visible = False
            BtnAcknowledge.Visible = True
            SearchHidden = True
            State.IsGridVisible = True
            PopulateGridForUser()

        End Sub

        Private Sub HandelPageForNon_AdminUserNotifications()

            gridHeader.Visible = False
            addBtnNewListItem.Visible = False
            SearchHidden = False
            State.IsGridVisible = True
            lblIncludeDisabled.Visible = False
            cbIncludeDisabled.Visible = False

        End Sub
        Private Sub UpdateBreadCrum()
            If (State IsNot Nothing) Then
                If (State IsNot Nothing) Then
                    If State.PageCalledFrom = "MAINPAGE" Then
                        MasterPage.BreadCrum = MasterPage.PageTab & ElitaBase.Sperator &
                            TranslationBase.TranslateLabelOrMessage("NOTIFICATION_LIST")
                        MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("NOTIFICATION_LIST")
                    Else
                        MasterPage.BreadCrum = MasterPage.PageTab & ElitaBase.Sperator &
                            TranslationBase.TranslateLabelOrMessage("NOTIFICATION_LIST_SEARCH")
                        MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("NOTIFICATION_LIST_SEARCH")
                    End If

                End If
            End If
        End Sub

#End Region

#Region "Controlling Logic"

        Sub PopulateDropDowns()

            Try
                'Me.BindListControlToDataView(Me.ddlAudianceType, LookupListNew.GetAudianceTypeLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId))

                'Dim AudianceTypeDV As DataView = LookupListNew.GetAudianceTypeLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId)

                'If Me.State.PageCalledFrom = "NON_ADMIN_USER" Then
                '    If ElitaPlusIdentity.Current.ActiveUser.External.ToUpper.Equals("Y") Then
                '        AudianceTypeDV.RowFilter = AudianceTypeDV.RowFilter & " and code <>'" & Codes.AUDIANCE_TYPE__INTERNAL_USER & "'"
                '    Else
                '        AudianceTypeDV.RowFilter = AudianceTypeDV.RowFilter & " and code <>'" & Codes.AUDIANCE_TYPE__EXTERNAL_USER & "'"
                '    End If
                'End If

                'Me.BindListControlToDataView(Me.ddlAudianceType, AudianceTypeDV, "DESCRIPTION", , True)

                Dim AudianceTypeList As DataElements.ListItem() =
                CommonConfigManager.Current.ListManager.GetList(listCode:="AUDIANCE",
                                                                languageCode:=Thread.CurrentPrincipal.GetLanguageCode())

                Dim FilterAudianceType As DataElements.ListItem()
                If State.PageCalledFrom = "NON_ADMIN_USER" Then
                    FilterAudianceType = (From Audiance In AudianceTypeList
                                          Where Not Audiance.Code = Codes.AUDIANCE_TYPE__INTERNAL_USER
                                          Select Audiance).ToArray()
                Else
                    FilterAudianceType = (From Audiance In AudianceTypeList
                                          Where Not Audiance.Code = Codes.AUDIANCE_TYPE__EXTERNAL_USER
                                          Select Audiance).ToArray()
                End If

                ddlAudianceType.Populate(FilterAudianceType.ToArray(),
                                New PopulateOptions() With
                                {
                                    .AddBlankItem = True
                                })


                'Me.BindListControlToDataView(Me.ddlNotificationType, LookupListNew.GetSystemNotificationTypesLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId))

                Dim NotificationTypeList As DataElements.ListItem() =
                CommonConfigManager.Current.ListManager.GetList(listCode:="NOTIFICATION",
                                                                languageCode:=Thread.CurrentPrincipal.GetLanguageCode())

                ddlNotificationType.Populate(NotificationTypeList.ToArray(),
                                New PopulateOptions() With
                                {
                                    .AddBlankItem = True
                                })

                'Sort By
                'Dim oSortDv As DataView
                'oSortDv = LookupListNew.GetNotificationSearchFieldsLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId)
                ''SortSvc(oSortDv)
                'Me.BindListControlToDataView(Me.cboSortBy, oSortDv, , , False)

                Dim SortByList As DataElements.ListItem() =
                CommonConfigManager.Current.ListManager.GetList(listCode:="SNOTE_SORTBY",
                                                                languageCode:=Thread.CurrentPrincipal.GetLanguageCode())

                cboSortBy.Populate(SortByList.ToArray(),
                                New PopulateOptions() With
                                {
                                    .AddBlankItem = False
                                })

                Dim defaultSelectedSortByCodeId As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_NOTIFICATION_SEARCH_FIELDS, Codes.SYSTEM_NOTIFICATION_SORT_BY__NOTIFICATION_TYPE)
                If (State.selectedSortById.Equals(Guid.Empty)) Then
                    SetSelectedItem(cboSortBy, defaultSelectedSortByCodeId)
                    State.selectedSortById = defaultSelectedSortByCodeId
                Else
                    SetSelectedItem(cboSortBy, State.selectedSortById)
                End If

                'Sort Order
                'Dim oSortOrderDv As DataView
                'oSortOrderDv = LookupListNew.GetSortOrderLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId)

                'Me.BindListControlToDataView(Me.cboSortOrder, oSortOrderDv, , , False)

                Dim SortOrderList As DataElements.ListItem() =
                CommonConfigManager.Current.ListManager.GetList(listCode:="SORT_ORDER",
                                                                languageCode:=Thread.CurrentPrincipal.GetLanguageCode())

                cboSortOrder.Populate(SortOrderList.ToArray(),
                                New PopulateOptions() With
                                {
                                    .AddBlankItem = False
                                })

                Dim defaultSelectedSortOrderCodeId As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_SORT_ORDER, Codes.DEFAULT_SORT_ORDER_FOR_SYSTEM_NOTIFICATION)
                If (State.selectedSortOrderId.Equals(Guid.Empty)) Then
                    SetSelectedItem(cboSortOrder, defaultSelectedSortOrderCodeId)
                    State.selectedSortOrderId = defaultSelectedSortOrderCodeId
                Else
                    SetSelectedItem(cboSortOrder, State.selectedSortOrderId)
                End If

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Public Sub ValidateDates()

            If txtBeginDate.Text.Trim() <> String.Empty Then
                GUIException.ValidateDate(lblDateRange1, txtBeginDate.Text)
            End If

            If txtEndDate.Text.Trim() <> String.Empty Then
                GUIException.ValidateDate(lblDateRange1, txtEndDate.Text)

                If txtBeginDate.Text.Trim() <> String.Empty AndAlso CDate(txtBeginDate.Text) > CDate(txtEndDate.Text) Then
                    ElitaPlusPage.SetLabelError(lblDateRange1)
                    Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_BEGIN_END_DATE_ERR)
                End If
            End If

            lblDateRange1.ForeColor = lblAUDIANCE_TYPE.ForeColor
            lblDateRange2.ForeColor = lblAUDIANCE_TYPE.ForeColor



            If txtBeginDateOutage.Text.Trim() <> String.Empty Then
                GUIException.ValidateDate(lblOutageDateRange1, txtBeginDateOutage.Text)
            End If

            If txtEndDateOutage.Text.Trim() <> String.Empty Then
                GUIException.ValidateDate(lblOutageDateRange1, txtEndDateOutage.Text)

                If txtBeginDateOutage.Text.Trim() <> String.Empty AndAlso CDate(txtBeginDateOutage.Text) > CDate(txtEndDateOutage.Text) Then
                    ElitaPlusPage.SetLabelError(lblOutageDateRange1)
                    Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_BEGIN_END_DATE_ERR)
                End If
            End If

            lblOutageDateRange1.ForeColor = lblAUDIANCE_TYPE.ForeColor
            lblOutageDateRange2.ForeColor = lblAUDIANCE_TYPE.ForeColor


        End Sub

        Public Sub PopulateGrid()
            If ((State.SearchDV Is Nothing) OrElse (State.HasDataChanged) OrElse IsReturningFromChild) Then
                State.SelectedNotificationTypeId = New Guid(ddlNotificationType.SelectedValue)
                State.SelectedAudianceTypeId = New Guid(ddlAudianceType.SelectedValue)
                State.selectedSortById = New Guid(cboSortBy.SelectedValue)
                State.selectedSortOrderId = New Guid(cboSortOrder.SelectedValue)

                State.NotificationName = txtNotificationName.Text.ToUpper.Trim
                State.NotificationDetail = txtNotificationDetail.Text.ToUpper.Trim

                State.IncludeDisabled = cbIncludeDisabled.Checked

                'Notification dates
                If txtBeginDate.Text.Trim() <> String.Empty Then
                    GUIException.ValidateDate(lblDateRange1, txtBeginDate.Text)
                End If

                If txtEndDate.Text.Trim() <> String.Empty Then
                    GUIException.ValidateDate(lblDateRange1, txtEndDate.Text)

                    If txtBeginDate.Text.Trim() <> String.Empty AndAlso CDate(txtBeginDate.Text) > CDate(txtEndDate.Text) Then
                        ElitaPlusPage.SetLabelError(lblDateRange1)
                        Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_BEGIN_END_DATE_ERR)
                    End If
                End If

                If txtBeginDate.Text.Trim = String.Empty Then
                    State.BeginDate = Nothing
                Else
                    State.BeginDate = txtBeginDate.Text
                End If

                If txtEndDate.Text.Trim = String.Empty Then
                    State.EndDate = Nothing
                Else
                    State.EndDate = txtEndDate.Text
                End If

                'Outage dates
                If txtBeginDateOutage.Text.Trim() <> String.Empty Then
                    GUIException.ValidateDate(lblOutageDateRange1, txtBeginDateOutage.Text)
                End If

                If txtEndDateOutage.Text.Trim() <> String.Empty Then
                    GUIException.ValidateDate(lblOutageDateRange1, txtEndDateOutage.Text)

                    If txtBeginDateOutage.Text.Trim() <> String.Empty AndAlso CDate(txtBeginDateOutage.Text) > CDate(txtEndDateOutage.Text) Then
                        ElitaPlusPage.SetLabelError(lblOutageDateRange1)
                        Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_BEGIN_END_DATE_ERR)
                    End If
                End If

                If txtBeginDateOutage.Text.Trim = String.Empty Then
                    State.BeginDateOutage = Nothing
                Else
                    State.BeginDateOutage = txtBeginDateOutage.Text
                End If

                If txtEndDateOutage.Text.Trim = String.Empty Then
                    State.EndDateOutage = Nothing
                Else
                    State.EndDateOutage = txtEndDateOutage.Text
                End If

                Dim sortBy As String
                sortBy = LookupListNew.GetCodeFromId(LookupListNew.LK_NOTIFICATION_SEARCH_FIELDS, State.selectedSortById)

                Dim SortOrderCode As String
                SortOrderCode = LookupListNew.GetCodeFromId(LookupListNew.LK_SORT_ORDER, State.selectedSortOrderId)

                Dim userType As String

                If State.PageCalledFrom.ToUpper.Equals("MENU") Then
                    userType = "ADMIN"
                Else
                    userType = "NON_ADMIN"
                End If
                ''''
                State.SearchDV = Notification.GetList(State.NotificationName,
                                                         State.NotificationDetail,
                                                         State.SelectedNotificationTypeId,
                                                         State.SelectedAudianceTypeId,
                                                         State.BeginDate,
                                                         State.EndDate,
                                                         State.BeginDateOutage,
                                                         State.EndDateOutage,
                                                         State.IncludeDisabled,
                                                         SortOrderCode,
                                                         sortBy,
                                                         userType)
            End If
            'Me.State.SearchDV.Sort = Me.State.SortExpression
            Grid.AutoGenerateColumns = False
            Grid.PageSize = State.PageSize

            SetPageAndSelectedIndexFromGuid(State.SearchDV, State.SelectedNotificationListId, Grid, State.PageIndex)
            If State.SearchClick Then
                ValidSearchResultCountNew(State.SearchDV.Count, True)
                State.SearchClick = False
            End If
            SortAndBindGrid()
        End Sub

        Public Sub PopulateGridForUser()
            If ((State.SearchDV Is Nothing) OrElse (State.HasDataChanged) OrElse IsReturningFromChild) Then
                State.SelectedNotificationTypeId = Guid.Empty
                State.SelectedAudianceTypeId = Guid.Empty

                Dim sortBy As String = Codes.SYSTEM_NOTIFICATION_SORT_BY__NOTIFICATION_TYPE
                Dim SortOrderCode As String = Codes.DEFAULT_SORT_ORDER_FOR_SYSTEM_NOTIFICATION

                State.SearchDV = Notification.GetListForUser(SortOrderCode,
                                                          sortBy)
            End If
            Grid.AutoGenerateColumns = False
            Grid.PageSize = State.PageSize

            SetPageAndSelectedIndexFromGuid(State.SearchDV, State.SelectedNotificationListId, Grid, State.PageIndex)
            If State.SearchClick Then
                ValidSearchResultCountNew(State.SearchDV.Count, True)
                State.SearchClick = False
            End If
            SortAndBindGrid()

            If State.PageIndicesVisited Is Nothing Then State.PageIndicesVisited = New ArrayList
            If Not State.PageIndicesVisited.Contains(0) Then State.PageIndicesVisited.Add(0)

            If State.SearchDV.Count > 0 AndAlso State.SearchDV.Count <= State.PageSize Then
                lblAcknowlage.Visible = False
                BtnAcknowledge.Enabled = True
                State.AllPagesVisited = True
            Else
                lblAcknowlage.Visible = True
                BtnAcknowledge.Enabled = False
            End If
        End Sub

        Private Sub SortAndBindGrid()

            State.bnoRow = False
            Grid.Enabled = True
            State.PageIndex = Grid.PageIndex
            Grid.DataSource = State.SearchDV
            HighLightSortColumn(Grid, State.SortExpression)
            Grid.DataBind()

            ControlMgr.SetVisibleControl(Me, Grid, State.IsGridVisible)
            ControlMgr.SetVisibleControl(Me, trPageSize, Grid.Visible)

            Session("recCount") = State.SearchDV.Count
            lblRecordCount.Text = State.SearchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)

            If State.PageCalledFrom = "MAINPAGE" Then
                Grid.Columns(2).Visible = False
                Grid.Columns(9).Visible = False
            End If

        End Sub

#End Region

#Region "Modal Page"

        Sub PopulateDetailFromNotificationDetailChildBO()

            txtNOTIFICATION_NAME_Modal.Text = State.MyBO.NotificationName
            txtNotificationType_Modal.Text = LookupListNew.GetDescriptionFromId(LookupListCache.LK_SYSTEM_NOTIFICATION_TYPES, State.MyBO.NotificationTypeId)
            'Me.txtAudianceType_Modal.Text = LookupListNew.GetDescriptionFromId(LookupListCache.LK_AUDIANCE_TYPES, Me.State.MyBO.AudianceTypeId)
            txtBeginDate_Modal.Text = State.MyBO.NotificationBeginDate.ToString
            txtEndDate_Modal.Text = State.MyBO.NotificationEndDate.ToString
            txtOutageBeginDate_Modal.Text = State.MyBO.OutageBeginDate.ToString
            txtOutageEndDate_Modal.Text = State.MyBO.OutageEndDate.ToString
            txtContactInfo_Modal.Text = State.MyBO.ContactInfo
            txtNOTIFICATION_DETAILS_Modal.Text = State.MyBO.NotificationDetails
            txtSerialNo_Modal.Text = State.MyBO.SerialNo.ToString

        End Sub


        Sub BeginNotificationDetailChildEdit()

            With State

                If Not .NotificationSelectedBOId.Equals(Guid.Empty) Then
                    .MyBO = New Notification(.NotificationSelectedBOId)
                Else
                    .MyBO = New Notification
                End If
                .MyBO.BeginEdit()
            End With

            AddLabelDecorations(State.MyBO)

        End Sub
#End Region

#Region " GridView Related "

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
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub

        Private Sub cboPageSize_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
            Try
                State.PageSize = CType(cboPageSize.SelectedValue, Integer)
                State.SelectedPageSize = State.PageSize
                State.PageIndex = NewCurrentPageIndex(Grid, State.SearchDV.Count, State.PageSize)
                Grid.PageIndex = State.PageIndex
                If State.PageCalledFrom = "MAINPAGE" Then
                    PopulateGridForUser()
                    If IsAllPagesVisited(State.PageIndex) Then
                        lblAcknowlage.Visible = False
                        BtnAcknowledge.Enabled = True
                        State.AllPagesVisited = True
                    End If
                Else
                    PopulateGrid()
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Function IsAllPagesVisited(thisPageNumber As Integer) As Boolean

            If State.PageIndicesVisited Is Nothing Then State.PageIndicesVisited = New ArrayList
            If Not State.PageIndicesVisited.Contains(thisPageNumber) Then State.PageIndicesVisited.Add(thisPageNumber)

            If State.SearchDV.Count > 0 Then
                Dim i As Integer
                Dim pageNumbers As Integer = CType(Math.Floor(State.SearchDV.Count / State.PageSize), Integer)
                If (State.SearchDV.Count Mod State.PageSize) > 0 Then pageNumbers = pageNumbers + 1
                State.AllPagesVisited = True
                For i = 0 To pageNumbers - 1
                    If Not State.PageIndicesVisited.Contains(i) Then
                        State.AllPagesVisited = False
                    End If
                Next
            End If

            Return State.AllPagesVisited

        End Function

        Private Sub Grid_PageIndexChanged(sender As Object, e As System.EventArgs) Handles Grid.PageIndexChanged
            Try
                State.PageIndex = Grid.PageIndex
                State.SelectedNotificationListId = Guid.Empty
                If State.PageCalledFrom = "MAINPAGE" Then
                    PopulateGridForUser()
                    If IsAllPagesVisited(State.PageIndex) Then
                        lblAcknowlage.Visible = False
                        BtnAcknowledge.Enabled = True
                        State.AllPagesVisited = True
                    End If
                Else
                    PopulateGrid()
                End If
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

        Private Sub Grid_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowDataBound
            Try
                Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)

                'If e.Row.RowType = DataControlRowType.DataRow Then
                If dvRow IsNot Nothing AndAlso Not State.bnoRow Then
                    Dim btnEditItem As LinkButton
                    ' Assign the detail id to the command agrument
                    If (e.Row.Cells(GRID_COL_NOTIFICATION_NAME_IDX).FindControl(BTN_CONTROL_EDIT_DETAIL_LIST) IsNot Nothing) Then
                        'Edit Button argument changed to id
                        btnEditItem = CType(e.Row.Cells(GRID_COL_NOTIFICATION_NAME_IDX).FindControl(BTN_CONTROL_EDIT_DETAIL_LIST), LinkButton)
                        btnEditItem.CommandArgument = GetGuidStringFromByteArray(CType(dvRow(Notification.NotificationSearchDV.COL_NAME_NOTIFICATION_ID), Byte()))
                        btnEditItem.CommandName = ElitaPlusSearchPage.EDIT_COMMAND_NAME
                        btnEditItem.Text = dvRow(Notification.NotificationSearchDV.COL_NAME_NOTIFICATION_NAME).ToString

                        'If Me.State.PageCalledFrom = "MAINPAGE" Then
                        '    If Me.State.NotificationIDs Is Nothing Then Me.State.NotificationIDs = New ArrayList
                        '    Me.State.NotificationIDs.Add(btnEditItem.CommandArgument)
                        'End If
                    End If

                    Dim activeNotification As String = e.Row.Cells(GRID_COL_ENABLED_IDX).Text
                    If activeNotification = "N" Then
                        e.Row.Cells(GRID_COL_NOTIFICATION_TYPE_IDX).Enabled = False
                        e.Row.Cells(GRID_COL_AUDIANCE_TYPE_IDX).Enabled = False
                        e.Row.Cells(GRID_COL_BEGIN_DATE_IDX).Enabled = False
                        e.Row.Cells(GRID_COL_END_DATE_IDX).Enabled = False
                        e.Row.Cells(GRID_COL_OUTAGE_BEGIN_DATE_IDX).Enabled = False
                        e.Row.Cells(GRID_COL_OUTAGE_END_DATE_IDX).Enabled = False
                        e.Row.Cells(GRID_COL_ENABLED_IDX).Enabled = False
                    End If

                End If


            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub

        Private Sub Grid_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles Grid.RowCommand

            Try
                Dim NotficationId As Guid
                Dim oDataView As PriceListDetail.PriceListDetailSearchDV

                'Populate the grid with detail info
                If e.CommandName = ElitaPlusSearchPage.EDIT_COMMAND_NAME Then
                    State.blnModalOpen = True
                    State.IsGridInEditMode = True
                    'Me.State.SelectedGridValueToEdit = New Guid(e.CommandArgument.ToString())
                    'Me.State.ChildActionInProgress = DetailPageCommand.NewAndCopy

                    State.NotificationSelectedBOId = New Guid(e.CommandArgument.ToString())

                    'Dim index As Integer = CInt(e.CommandArgument)
                    'Me.State.NotificationSelectedBOId = New Guid(Me.Grid.Rows(index).Cells(Me.GRID_COL_NOTIFICATION_LIST_ID_IDX).Text)
                    If State.PageCalledFrom = "MAINPAGE" OrElse State.PageCalledFrom = "NON_ADMIN_USER" Then

                        BeginNotificationDetailChildEdit()
                        PopulateDetailFromNotificationDetailChildBO()
                        mdlPopup.Show()
                    Else
                        callPage(NotificationDetailForm.URL, State.NotificationSelectedBOId)
                    End If

                ElseIf e.CommandName = ElitaPlusSearchPage.DELETE_COMMAND_NAME Then
                    State.IsGridInEditMode = False
                    NotficationId = New Guid(e.CommandArgument.ToString())
                    State.NotificationSelectedBOId = New Guid(e.CommandArgument.ToString())
                    DisplayMessage(Message.DELETE_RECORD_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                    State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Delete

                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub

#End Region

#Region "Button Clicks "

        Protected Sub btnSearch_Click(sender As Object, e As System.EventArgs) Handles btnSearch.Click
            Try
                If txtBeginDate.Text = String.Empty AndAlso txtEndDate.Text = String.Empty AndAlso txtBeginDateOutage.Text = String.Empty AndAlso txtEndDateOutage.Text = String.Empty AndAlso ddlAudianceType.SelectedIndex = &H0 AndAlso ddlNotificationType.SelectedIndex = &H0 AndAlso txtNotificationDetail.Text = String.Empty AndAlso txtNotificationName.Text = String.Empty Then
                    Dim errors() As ValidationError = {New ValidationError(SEARCH_EXCEPTION, GetType(Notification), Nothing, "Search", Nothing)}
                    Throw New BOValidationException(errors, GetType(Notification).FullName)

                End If
                ValidateDates()
                SaveGuiState()
                State.PageIndex = 0
                State.SelectedNotificationListId = Guid.Empty
                State.IsGridVisible = True
                State.SearchDV = Nothing
                State.HasDataChanged = False
                State.SearchClick = True
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub addBtnNewListItem_Click(sender As System.Object, e As System.EventArgs) Handles addBtnNewListItem.Click
            Try
                callPage(NotificationDetailForm.URL)
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub btnClearSearch_Click(sender As Object, e As System.EventArgs) Handles btnClearSearch.Click
            Try
                'Clear State
                txtBeginDate.Text = String.Empty
                txtEndDate.Text = String.Empty
                ddlAudianceType.SelectedIndex = 0
                ddlNotificationType.SelectedIndex = 0

                State.BeginDate = String.Empty
                State.EndDate = String.Empty
                State.SelectedAudianceTypeId = Nothing
                State.SelectedNotificationTypeId = Nothing

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

#End Region

#Region "Additionl Buttons and helper functions"

        Protected Sub BtnAcknowledge_Click(sender As Object, e As EventArgs) Handles BtnAcknowledge.Click
            Try

                If UserNotification.InsertUserNotifications(ElitaPlusIdentity.Current.ActiveUser.Id) Then
                    Dim MainPageUrl As String = ELPWebConstants.APPLICATION_PATH & "/Navigation/HomeForm.aspx"
                    Session("PageCalledFrom") = Nothing
                    Response.Redirect(MainPageUrl)

                End If


            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub

        ''' 
        Private Sub btnNewItemCancel_Click(sender As Object, e As System.EventArgs) Handles btnNewItemCancel.Click
            Try

                State.MyBO.cancelEdit()
                If State.MyBO.IsSaveNew Then
                    State.MyBO.Delete()
                    State.MyBO.Save()
                End If

                mdlPopup.Hide()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub
#End Region

    End Class

End Namespace