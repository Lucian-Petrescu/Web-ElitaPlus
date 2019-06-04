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

        Private Sub Page_PageCall(ByVal CallFromUrl As String, ByVal CallingPar As Object) Handles MyBase.PageCall
            Try
                If Not Me.CallingParameters Is Nothing Then
                    'Get the page that called from the parent
                    Me.State.PageCalledFrom = CType(Me.CallingParameters, String)
                Else
                    Me.State.PageCalledFrom = "MENU"
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try

        End Sub

        Private Sub Page_PageReturn(ByVal ReturnFromUrl As String, ByVal ReturnPar As Object) Handles MyBase.PageReturn
            Try
                Me.MenuEnabled = True
                Me.IsReturningFromChild = True
                Dim retObj As PriceListDetailForm.ReturnType = CType(ReturnPar, PriceListDetailForm.ReturnType)
                Me.State.HasDataChanged = retObj.HasDataChanged
                Select Case retObj.LastOperation
                    Case ElitaPlusPage.DetailPageCommand.Back
                        If Not retObj Is Nothing Then
                            If Not retObj.EditingBo.IsNew Then
                                Me.State.SelectedNotificationListId = retObj.EditingBo.Id
                            End If
                            Me.State.IsGridVisible = True
                        End If
                    Case ElitaPlusPage.DetailPageCommand.Delete
                        Me.DisplayMessage(Message.DELETE_RECORD_CONFIRMATION, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
                    Case ElitaPlusPage.DetailPageCommand.Expire
                        Me.DisplayMessage(Message.EXPIRE_RECORD_CONFIRMATION, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
                End Select

            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub SaveGuiState()
            With Me.State
                .BeginDate = Me.txtBeginDate.Text
                .EndDate = Me.txtEndDate.Text

                .BeginDateOutage = Me.txtBeginDateOutage.Text
                .EndDateOutage = Me.txtEndDateOutage.Text

                .SelectedAudianceTypeId = New Guid(Me.ddlAudianceType.SelectedValue.ToString())
                .SelectedNotificationTypeId = New Guid(Me.ddlNotificationType.SelectedValue.ToString())

                .IncludeDisabled = Me.cbIncludeDisabled.Checked
                .NotificationName = Me.txtNotificationName.Text
                .NotificationDetail = Me.txtNotificationDetail.Text

                .selectedSortById = New Guid(Me.cboSortBy.SelectedValue.ToString())
                .selectedSortOrderId = New Guid(Me.cboSortOrder.SelectedValue.ToString())

            End With
        End Sub

        Private Sub RestoreGuiState()
            With Me.State
                PopulateControlFromBOProperty(Me.txtBeginDate, .BeginDate)
                PopulateControlFromBOProperty(Me.txtEndDate, .EndDate)
                PopulateControlFromBOProperty(Me.txtBeginDateOutage, .BeginDateOutage)
                PopulateControlFromBOProperty(Me.txtEndDateOutage, .EndDateOutage)
                PopulateControlFromBOProperty(Me.ddlAudianceType, .SelectedAudianceTypeId)
                PopulateControlFromBOProperty(Me.ddlNotificationType, .SelectedNotificationTypeId)
                PopulateControlFromBOProperty(Me.cbIncludeDisabled, .IncludeDisabled)
                PopulateControlFromBOProperty(Me.txtNotificationName, .NotificationName)
                PopulateControlFromBOProperty(Me.txtNotificationDetail, .NotificationDetail)

            End With
        End Sub

#End Region

#Region "Page_Events"

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            Try
                Me.MasterPage.MessageController.Clear()
                Me.Form.DefaultButton = btnSearch.UniqueID
                If Not Request.QueryString.Get("CALLER") Is Nothing Then Me.State.PageCalledFrom = Request.QueryString.Get("CALLER")
                Session("PageCalledFrom") = Me.State.PageCalledFrom

                If Not Me.IsPostBack Then
                    cboPageSize.SelectedValue = CType(Me.State.PageSize, String)
                    Grid.PageSize = Me.State.PageSize

                    Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("NOTIFICATION_LIST")
                    UpdateBreadCrum()
                    Me.TranslateGridHeader(Grid)

                    If Me.State.PageCalledFrom = "MENU" Or Me.State.PageCalledFrom = "NON_ADMIN_USER" Then
                        Me.AddCalendarwithTime(Me.BtnBeginDate, Me.txtBeginDate)
                        Me.AddCalendarwithTime(Me.BtnEndDate, Me.txtEndDate)
                        Me.AddCalendarwithTime(Me.btnBeginDateOutage, Me.txtBeginDateOutage)
                        Me.AddCalendarwithTime(Me.btnEndDateOutage, Me.txtEndDateOutage)
                        PopulateDropDowns()
                        ControlMgr.SetVisibleControl(Me, trPageSize, False)
                        ControlMgr.SetVisibleControl(Me, Me.BtnAcknowledge, False)
                        If Me.IsReturningFromChild Then Me.RestoreGuiState()

                        If Me.State.IsGridVisible Then
                            Me.PopulateGrid()
                        End If
                        'Set page size
                        cboPageSize.SelectedValue = Me.State.PageSize.ToString()
                        SetFocus(Me.ddlNotificationType)
                        If Me.State.PageCalledFrom = "MENU" Then
                            addBtnNewListItem.Visible = True
                        Else
                            addBtnNewListItem.Visible = False
                            HandelPageForNon_AdminUserNotifications()
                        End If

                    Else
                        If Me.State.PageCalledFrom = "MAINPAGE" Then HandelPageForUserNotifications()
                        If Me.State.PageCalledFrom = "NON_ADMIN_USER" Then HandelPageForNon_AdminUserNotifications()
                    End If
                Else

                    If Me.State.PageCalledFrom = "MAINPAGE" Then HandelPageForUserNotifications()

                End If

            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
            Me.ShowMissingTranslations(Me.MasterPage.MessageController)

        End Sub

        Protected Sub CheckIfComingFromDeleteConfirm()
            Dim confResponse As String = Me.HiddenSaveChangesPromptResponse.Value
            If Not confResponse Is Nothing AndAlso confResponse = Me.MSG_VALUE_YES Then
                If Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Delete Then
                    If Not Me.State.NotificationSelectedBOId.Equals(Guid.Empty) Then
                        '''BeginNotificationDetailChildEdit()
                    End If
                    Try

                        Me.State.MyBO.Delete()
                        Me.State.MyBO.Save()
                        Me.State.MyBO.EndEdit()
                        Me.State.NotificationSelectedBOId = Guid.Empty

                    Catch ex As Exception
                        Me.State.MyBO.RejectChanges()
                        Me.MasterPage.MessageController.AddError(ElitaPlus.ElitaPlusWebApp.Message.ERR_DELETING_DATA)
                        Throw ex
                    End Try
                    PopulateGrid()
                    'Clean after consuming the action
                    Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
                    Me.HiddenSaveChangesPromptResponse.Value = ""
                End If
            ElseIf Not confResponse Is Nothing AndAlso confResponse = Me.MSG_VALUE_NO Then
                'Clean after consuming the action
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
                Me.HiddenSaveChangesPromptResponse.Value = ""
            End If
        End Sub

        Private Sub HandelPageForUserNotifications()
            Me.MenuEnabled = False
            Me.searchTable.Visible = False
            Me.gridHeader.Visible = False
            Me.addBtnNewListItem.Visible = False
            Me.BtnAcknowledge.Visible = True
            Me.SearchHidden = True
            Me.State.IsGridVisible = True
            Me.PopulateGridForUser()

        End Sub

        Private Sub HandelPageForNon_AdminUserNotifications()

            Me.gridHeader.Visible = False
            Me.addBtnNewListItem.Visible = False
            Me.SearchHidden = False
            Me.State.IsGridVisible = True
            Me.lblIncludeDisabled.Visible = False
            Me.cbIncludeDisabled.Visible = False

        End Sub
        Private Sub UpdateBreadCrum()
            If (Not Me.State Is Nothing) Then
                If (Not Me.State Is Nothing) Then
                    If Me.State.PageCalledFrom = "MAINPAGE" Then
                        Me.MasterPage.BreadCrum = Me.MasterPage.PageTab & ElitaBase.Sperator &
                            TranslationBase.TranslateLabelOrMessage("NOTIFICATION_LIST")
                        Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("NOTIFICATION_LIST")
                    Else
                        Me.MasterPage.BreadCrum = Me.MasterPage.PageTab & ElitaBase.Sperator &
                            TranslationBase.TranslateLabelOrMessage("NOTIFICATION_LIST_SEARCH")
                        Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("NOTIFICATION_LIST_SEARCH")
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
                If Me.State.PageCalledFrom = "NON_ADMIN_USER" Then
                    FilterAudianceType = (From Audiance In AudianceTypeList
                                          Where Not Audiance.Code = Codes.AUDIANCE_TYPE__INTERNAL_USER
                                          Select Audiance).ToArray()
                Else
                    FilterAudianceType = (From Audiance In AudianceTypeList
                                          Where Not Audiance.Code = Codes.AUDIANCE_TYPE__EXTERNAL_USER
                                          Select Audiance).ToArray()
                End If

                Me.ddlAudianceType.Populate(FilterAudianceType.ToArray(),
                                New PopulateOptions() With
                                {
                                    .AddBlankItem = True
                                })


                'Me.BindListControlToDataView(Me.ddlNotificationType, LookupListNew.GetSystemNotificationTypesLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId))

                Dim NotificationTypeList As DataElements.ListItem() =
                CommonConfigManager.Current.ListManager.GetList(listCode:="NOTIFICATION",
                                                                languageCode:=Thread.CurrentPrincipal.GetLanguageCode())

                Me.ddlNotificationType.Populate(NotificationTypeList.ToArray(),
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

                Me.cboSortBy.Populate(SortByList.ToArray(),
                                New PopulateOptions() With
                                {
                                    .AddBlankItem = False
                                })

                Dim defaultSelectedSortByCodeId As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_NOTIFICATION_SEARCH_FIELDS, Codes.SYSTEM_NOTIFICATION_SORT_BY__NOTIFICATION_TYPE)
                If (Me.State.selectedSortById.Equals(Guid.Empty)) Then
                    Me.SetSelectedItem(Me.cboSortBy, defaultSelectedSortByCodeId)
                    Me.State.selectedSortById = defaultSelectedSortByCodeId
                Else
                    Me.SetSelectedItem(Me.cboSortBy, Me.State.selectedSortById)
                End If

                'Sort Order
                'Dim oSortOrderDv As DataView
                'oSortOrderDv = LookupListNew.GetSortOrderLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId)

                'Me.BindListControlToDataView(Me.cboSortOrder, oSortOrderDv, , , False)

                Dim SortOrderList As DataElements.ListItem() =
                CommonConfigManager.Current.ListManager.GetList(listCode:="SORT_ORDER",
                                                                languageCode:=Thread.CurrentPrincipal.GetLanguageCode())

                Me.cboSortOrder.Populate(SortOrderList.ToArray(),
                                New PopulateOptions() With
                                {
                                    .AddBlankItem = False
                                })

                Dim defaultSelectedSortOrderCodeId As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_SORT_ORDER, Codes.DEFAULT_SORT_ORDER_FOR_SYSTEM_NOTIFICATION)
                If (Me.State.selectedSortOrderId.Equals(Guid.Empty)) Then
                    Me.SetSelectedItem(Me.cboSortOrder, defaultSelectedSortOrderCodeId)
                    Me.State.selectedSortOrderId = defaultSelectedSortOrderCodeId
                Else
                    Me.SetSelectedItem(Me.cboSortOrder, Me.State.selectedSortOrderId)
                End If

            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
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

            Me.lblDateRange1.ForeColor = Me.lblAUDIANCE_TYPE.ForeColor
            Me.lblDateRange2.ForeColor = Me.lblAUDIANCE_TYPE.ForeColor



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

            Me.lblOutageDateRange1.ForeColor = Me.lblAUDIANCE_TYPE.ForeColor
            Me.lblOutageDateRange2.ForeColor = Me.lblAUDIANCE_TYPE.ForeColor


        End Sub

        Public Sub PopulateGrid()
            If ((Me.State.SearchDV Is Nothing) OrElse (Me.State.HasDataChanged) OrElse Me.IsReturningFromChild) Then
                Me.State.SelectedNotificationTypeId = New Guid(ddlNotificationType.SelectedValue)
                Me.State.SelectedAudianceTypeId = New Guid(ddlAudianceType.SelectedValue)
                Me.State.selectedSortById = New Guid(cboSortBy.SelectedValue)
                Me.State.selectedSortOrderId = New Guid(cboSortOrder.SelectedValue)

                Me.State.NotificationName = Me.txtNotificationName.Text.ToUpper.Trim
                Me.State.NotificationDetail = Me.txtNotificationDetail.Text.ToUpper.Trim

                Me.State.IncludeDisabled = Me.cbIncludeDisabled.Checked

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
                    Me.State.BeginDate = Nothing
                Else
                    Me.State.BeginDate = Me.txtBeginDate.Text
                End If

                If txtEndDate.Text.Trim = String.Empty Then
                    Me.State.EndDate = Nothing
                Else
                    Me.State.EndDate = Me.txtEndDate.Text
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
                    Me.State.BeginDateOutage = Nothing
                Else
                    Me.State.BeginDateOutage = Me.txtBeginDateOutage.Text
                End If

                If txtEndDateOutage.Text.Trim = String.Empty Then
                    Me.State.EndDateOutage = Nothing
                Else
                    Me.State.EndDateOutage = Me.txtEndDateOutage.Text
                End If

                Dim sortBy As String
                sortBy = LookupListNew.GetCodeFromId(LookupListNew.LK_NOTIFICATION_SEARCH_FIELDS, Me.State.selectedSortById)

                Dim SortOrderCode As String
                SortOrderCode = LookupListNew.GetCodeFromId(LookupListNew.LK_SORT_ORDER, Me.State.selectedSortOrderId)

                Dim userType As String

                If Me.State.PageCalledFrom.ToUpper.Equals("MENU") Then
                    userType = "ADMIN"
                Else
                    userType = "NON_ADMIN"
                End If
                ''''
                Me.State.SearchDV = Notification.GetList(Me.State.NotificationName,
                                                         Me.State.NotificationDetail,
                                                         Me.State.SelectedNotificationTypeId,
                                                         Me.State.SelectedAudianceTypeId,
                                                         Me.State.BeginDate,
                                                         Me.State.EndDate,
                                                         Me.State.BeginDateOutage,
                                                         Me.State.EndDateOutage,
                                                         Me.State.IncludeDisabled,
                                                         SortOrderCode,
                                                         sortBy,
                                                         userType)
            End If
            'Me.State.SearchDV.Sort = Me.State.SortExpression
            Me.Grid.AutoGenerateColumns = False
            Me.Grid.PageSize = Me.State.PageSize

            SetPageAndSelectedIndexFromGuid(Me.State.SearchDV, Me.State.SelectedNotificationListId, Me.Grid, Me.State.PageIndex)
            If Me.State.SearchClick Then
                Me.ValidSearchResultCountNew(Me.State.SearchDV.Count, True)
                Me.State.SearchClick = False
            End If
            Me.SortAndBindGrid()
        End Sub

        Public Sub PopulateGridForUser()
            If ((Me.State.SearchDV Is Nothing) OrElse (Me.State.HasDataChanged) OrElse Me.IsReturningFromChild) Then
                Me.State.SelectedNotificationTypeId = Guid.Empty
                Me.State.SelectedAudianceTypeId = Guid.Empty

                Dim sortBy As String = Codes.SYSTEM_NOTIFICATION_SORT_BY__NOTIFICATION_TYPE
                Dim SortOrderCode As String = Codes.DEFAULT_SORT_ORDER_FOR_SYSTEM_NOTIFICATION

                Me.State.SearchDV = Notification.GetListForUser(SortOrderCode,
                                                          sortBy)
            End If
            Me.Grid.AutoGenerateColumns = False
            Me.Grid.PageSize = Me.State.PageSize

            SetPageAndSelectedIndexFromGuid(Me.State.SearchDV, Me.State.SelectedNotificationListId, Me.Grid, Me.State.PageIndex)
            If Me.State.SearchClick Then
                Me.ValidSearchResultCountNew(Me.State.SearchDV.Count, True)
                Me.State.SearchClick = False
            End If
            Me.SortAndBindGrid()

            If Me.State.PageIndicesVisited Is Nothing Then Me.State.PageIndicesVisited = New ArrayList
            If Not Me.State.PageIndicesVisited.Contains(0) Then Me.State.PageIndicesVisited.Add(0)

            If Me.State.SearchDV.Count > 0 AndAlso Me.State.SearchDV.Count <= Me.State.PageSize Then
                Me.lblAcknowlage.Visible = False
                Me.BtnAcknowledge.Enabled = True
                Me.State.AllPagesVisited = True
            Else
                Me.lblAcknowlage.Visible = True
                Me.BtnAcknowledge.Enabled = False
            End If
        End Sub

        Private Sub SortAndBindGrid()

            Me.State.bnoRow = False
            Me.Grid.Enabled = True
            Me.State.PageIndex = Me.Grid.PageIndex
            Me.Grid.DataSource = Me.State.SearchDV
            HighLightSortColumn(Grid, Me.State.SortExpression)
            Me.Grid.DataBind()

            ControlMgr.SetVisibleControl(Me, Grid, Me.State.IsGridVisible)
            ControlMgr.SetVisibleControl(Me, trPageSize, Me.Grid.Visible)

            Session("recCount") = Me.State.SearchDV.Count
            Me.lblRecordCount.Text = Me.State.SearchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)

            If Me.State.PageCalledFrom = "MAINPAGE" Then
                Grid.Columns(2).Visible = False
                Grid.Columns(9).Visible = False
            End If

        End Sub

#End Region

#Region "Modal Page"

        Sub PopulateDetailFromNotificationDetailChildBO()

            Me.txtNOTIFICATION_NAME_Modal.Text = Me.State.MyBO.NotificationName
            Me.txtNotificationType_Modal.Text = LookupListNew.GetDescriptionFromId(LookupListCache.LK_SYSTEM_NOTIFICATION_TYPES, Me.State.MyBO.NotificationTypeId)
            'Me.txtAudianceType_Modal.Text = LookupListNew.GetDescriptionFromId(LookupListCache.LK_AUDIANCE_TYPES, Me.State.MyBO.AudianceTypeId)
            Me.txtBeginDate_Modal.Text = Me.State.MyBO.NotificationBeginDate.ToString
            Me.txtEndDate_Modal.Text = Me.State.MyBO.NotificationEndDate.ToString
            Me.txtOutageBeginDate_Modal.Text = Me.State.MyBO.OutageBeginDate.ToString
            Me.txtOutageEndDate_Modal.Text = Me.State.MyBO.OutageEndDate.ToString
            Me.txtContactInfo_Modal.Text = Me.State.MyBO.ContactInfo
            Me.txtNOTIFICATION_DETAILS_Modal.Text = Me.State.MyBO.NotificationDetails
            Me.txtSerialNo_Modal.Text = Me.State.MyBO.SerialNo.ToString

        End Sub


        Sub BeginNotificationDetailChildEdit()

            With Me.State

                If Not .NotificationSelectedBOId.Equals(Guid.Empty) Then
                    .MyBO = New Notification(.NotificationSelectedBOId)
                Else
                    .MyBO = New Notification
                End If
                .MyBO.BeginEdit()
            End With

            Me.AddLabelDecorations(Me.State.MyBO)

        End Sub
#End Region

#Region " GridView Related "

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
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try

        End Sub

        Private Sub cboPageSize_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
            Try
                Me.State.PageSize = CType(cboPageSize.SelectedValue, Integer)
                Me.State.SelectedPageSize = Me.State.PageSize
                Me.State.PageIndex = NewCurrentPageIndex(Grid, State.SearchDV.Count, State.PageSize)
                Me.Grid.PageIndex = Me.State.PageIndex
                If Me.State.PageCalledFrom = "MAINPAGE" Then
                    Me.PopulateGridForUser()
                    If IsAllPagesVisited(Me.State.PageIndex) Then
                        Me.lblAcknowlage.Visible = False
                        Me.BtnAcknowledge.Enabled = True
                        Me.State.AllPagesVisited = True
                    End If
                Else
                    Me.PopulateGrid()
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Function IsAllPagesVisited(thisPageNumber As Integer) As Boolean

            If Me.State.PageIndicesVisited Is Nothing Then Me.State.PageIndicesVisited = New ArrayList
            If Not Me.State.PageIndicesVisited.Contains(thisPageNumber) Then Me.State.PageIndicesVisited.Add(thisPageNumber)

            If Me.State.SearchDV.Count > 0 Then
                Dim i As Integer
                Dim pageNumbers As Integer = CType(Math.Floor(Me.State.SearchDV.Count / Me.State.PageSize), Integer)
                If (Me.State.SearchDV.Count Mod Me.State.PageSize) > 0 Then pageNumbers = pageNumbers + 1
                Me.State.AllPagesVisited = True
                For i = 0 To pageNumbers - 1
                    If Not Me.State.PageIndicesVisited.Contains(i) Then
                        Me.State.AllPagesVisited = False
                    End If
                Next
            End If

            Return Me.State.AllPagesVisited

        End Function

        Private Sub Grid_PageIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Grid.PageIndexChanged
            Try
                Me.State.PageIndex = Grid.PageIndex
                Me.State.SelectedNotificationListId = Guid.Empty
                If Me.State.PageCalledFrom = "MAINPAGE" Then
                    Me.PopulateGridForUser()
                    If IsAllPagesVisited(Me.State.PageIndex) Then
                        Me.lblAcknowlage.Visible = False
                        Me.BtnAcknowledge.Enabled = True
                        Me.State.AllPagesVisited = True
                    End If
                Else
                    Me.PopulateGrid()
                End If
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

        Private Sub Grid_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowDataBound
            Try
                Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)

                'If e.Row.RowType = DataControlRowType.DataRow Then
                If Not dvRow Is Nothing And Not Me.State.bnoRow Then
                    Dim btnEditItem As LinkButton
                    ' Assign the detail id to the command agrument
                    If (Not e.Row.Cells(Me.GRID_COL_NOTIFICATION_NAME_IDX).FindControl(BTN_CONTROL_EDIT_DETAIL_LIST) Is Nothing) Then
                        'Edit Button argument changed to id
                        btnEditItem = CType(e.Row.Cells(Me.GRID_COL_NOTIFICATION_NAME_IDX).FindControl(BTN_CONTROL_EDIT_DETAIL_LIST), LinkButton)
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
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try

        End Sub

        Private Sub Grid_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles Grid.RowCommand

            Try
                Dim NotficationId As Guid
                Dim oDataView As PriceListDetail.PriceListDetailSearchDV

                'Populate the grid with detail info
                If e.CommandName = ElitaPlusSearchPage.EDIT_COMMAND_NAME Then
                    Me.State.blnModalOpen = True
                    Me.State.IsGridInEditMode = True
                    'Me.State.SelectedGridValueToEdit = New Guid(e.CommandArgument.ToString())
                    'Me.State.ChildActionInProgress = DetailPageCommand.NewAndCopy

                    Me.State.NotificationSelectedBOId = New Guid(e.CommandArgument.ToString())

                    'Dim index As Integer = CInt(e.CommandArgument)
                    'Me.State.NotificationSelectedBOId = New Guid(Me.Grid.Rows(index).Cells(Me.GRID_COL_NOTIFICATION_LIST_ID_IDX).Text)
                    If Me.State.PageCalledFrom = "MAINPAGE" Or Me.State.PageCalledFrom = "NON_ADMIN_USER" Then

                        BeginNotificationDetailChildEdit()
                        Me.PopulateDetailFromNotificationDetailChildBO()
                        mdlPopup.Show()
                    Else
                        Me.callPage(NotificationDetailForm.URL, Me.State.NotificationSelectedBOId)
                    End If

                ElseIf e.CommandName = ElitaPlusSearchPage.DELETE_COMMAND_NAME Then
                    Me.State.IsGridInEditMode = False
                    NotficationId = New Guid(e.CommandArgument.ToString())
                    Me.State.NotificationSelectedBOId = New Guid(e.CommandArgument.ToString())
                    Me.DisplayMessage(Message.DELETE_RECORD_PROMPT, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                    Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Delete

                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try

        End Sub

#End Region

#Region "Button Clicks "

        Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
            Try
                If txtBeginDate.Text = String.Empty And txtEndDate.Text = String.Empty And txtBeginDateOutage.Text = String.Empty And
                    txtEndDateOutage.Text = String.Empty And ddlAudianceType.SelectedIndex = &H0 And ddlNotificationType.SelectedIndex = &H0 And
                    txtNotificationDetail.Text = String.Empty And txtNotificationName.Text = String.Empty Then
                    Dim errors() As ValidationError = {New ValidationError(SEARCH_EXCEPTION, GetType(Notification), Nothing, "Search", Nothing)}
                    Throw New BOValidationException(errors, GetType(Notification).FullName)

                End If
                Me.ValidateDates()
                Me.SaveGuiState()
                Me.State.PageIndex = 0
                Me.State.SelectedNotificationListId = Guid.Empty
                Me.State.IsGridVisible = True
                Me.State.SearchDV = Nothing
                Me.State.HasDataChanged = False
                Me.State.SearchClick = True
                Me.PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub addBtnNewListItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles addBtnNewListItem.Click
            Try
                Me.callPage(NotificationDetailForm.URL)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub btnClearSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClearSearch.Click
            Try
                'Clear State
                Me.txtBeginDate.Text = String.Empty
                Me.txtEndDate.Text = String.Empty
                Me.ddlAudianceType.SelectedIndex = 0
                Me.ddlNotificationType.SelectedIndex = 0

                Me.State.BeginDate = String.Empty
                Me.State.EndDate = String.Empty
                Me.State.SelectedAudianceTypeId = Nothing
                Me.State.SelectedNotificationTypeId = Nothing

            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

#End Region

#Region "Additionl Buttons and helper functions"

        Protected Sub BtnAcknowledge_Click(sender As Object, e As EventArgs) Handles BtnAcknowledge.Click
            Try

                If UserNotification.InsertUserNotifications(ElitaPlusIdentity.Current.ActiveUser.Id) Then
                    Dim MainPageUrl As String = ELPWebConstants.APPLICATION_PATH & "/Navigation/HomeForm.aspx"
                    Session("PageCalledFrom") = Nothing
                    Me.Response.Redirect(MainPageUrl)

                End If


            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try

        End Sub

        ''' 
        Private Sub btnNewItemCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNewItemCancel.Click
            Try

                Me.State.MyBO.cancelEdit()
                If Me.State.MyBO.IsSaveNew Then
                    Me.State.MyBO.Delete()
                    Me.State.MyBO.Save()
                End If

                mdlPopup.Hide()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub
#End Region

    End Class

End Namespace